using System.Linq.Expressions;
using System.Text.Json;
using DatabaseContext;
using Entites;
using Entites.Enum;
using Entites.Show;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using MyMoviesTvShowList.Extensions;
using Services.MoviesAdmin;

namespace Services.TVShowsAdmin
{
    public class TVShowsAdminService : ITVShowsAdminService
    {
        private readonly MyMoviesTvShowListContext database;

        public TVShowsAdminService(MyMoviesTvShowListContext database)
        {
            this.database = database;
        }

        public async Task<List<TVShowDTO>> GetTVShows(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<TvShowEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.Title.Contains(Search);
            }

            var data = await database.TvShow
               .Where(predicate)
               .OrderBy(m => m.Title)
               .Skip((Page - 1) * PostPerPage)
               .Take(PostPerPage).ToListAsync();

            List<TVShowDTO> tvShows = new List<TVShowDTO>();

            foreach (var tvShow in data)
            {
                var act = await database.TvShowCrew.Where(a => a.TvShowId == tvShow.Id && a.Role == CrewRoleEnum.Actor).ToListAsync();
                var cre = await database.TvShowCrew.Where(a => a.TvShowId == tvShow.Id && a.Role == CrewRoleEnum.Creator).ToListAsync();

                List<string> g = tvShow.Genres.Split(",").ToList();

                List<ActorDTO> actors = new List<ActorDTO>();

                List<PeopleEntity> creators = new List<PeopleEntity>();

                List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

                foreach (var a in act)
                {
                    var q = await database.People.Where(p => p.Id == a.PersonId)
                   .Select(s => new ActorDTO
                   {
                       Id = s.Id,
                       FirstName = s.FirstName,
                       LastName = s.LastName,
                       PersonImageData = s.PersonImageData

                   })
                   .FirstOrDefaultAsync();

                    var Character = await database.TvShowCharacters.Where(p => p.ActorId == a.PersonId).FirstOrDefaultAsync();

                    q.CharacterName = Character.Name;
                    q.CharacterDescription = Character.Description;

                    actors.Add(q);
                }

                foreach (var d in cre)
                {
                    var q = await database.People.Where(p => p.Id == d.PersonId).FirstOrDefaultAsync();
                    creators.Add(q);
                }

                foreach (string d in g)
                {
                    genres.Add(new GenresSelectDTO
                    {
                        value = (GenresEnum)(Convert.ToInt32(d)),
                        label = ((GenresEnum)(Convert.ToInt32(d))).GetDescription()

                    });
                }
                tvShows.Add(new TVShowDTO
                {
                    Id = tvShow.Id,
                    Title = tvShow.Title,
                    Actors = actors,
                    Creators = creators,
                    Runtime = tvShow.Runtime,
                    TVShowImageData = tvShow.TVShowImageData,
                    Description = tvShow.Description,
                    TotalSeason = tvShow.TotalSeason,
                    TotalEpisode = tvShow.TotalEpisode,
                    Genres = genres
                });
            }
            return tvShows;
        }

        public async Task SaveTVShow(SaveTVShowDTO TVShow)
        {
            var transaction = database.Database.BeginTransaction();

            try
            {
                if (TVShow.Id > 0)
                {
                    var tvShowDb = await database.TvShow.Where(w => w.Id == TVShow.Id).FirstOrDefaultAsync();

                    var genres = DataActions.FormatGenres(TVShow.Genres);

                    tvShowDb.Title = TVShow.Title.Trim();
                    tvShowDb.Description = TVShow.Description.Trim();
                    tvShowDb.Genres = genres;
                    tvShowDb.Runtime = TVShow.Runtime;
                   

                    if (TVShow.TVShowImageData.Length > 0)
                    {
                        tvShowDb.TVShowImageData = DataActions.ImageToByte(TVShow.TVShowImageData);
                    }

                    await database.SaveChangesAsync();

                    var existingActorCrew = await database.TvShowCrew.Where(c => c.TvShowId == tvShowDb.Id && c.Role == CrewRoleEnum.Actor).ToListAsync();
                    var existingCharacterCrew = await database.TvShowCharacters.Where(c => c.TvShowId == tvShowDb.Id).ToListAsync();

                    var updatedActorCrew = JsonSerializer.Deserialize<List<ActorSelectDTO>>(TVShow.Actors);

                    var actorsToRemove = existingActorCrew.Where(ec => !updatedActorCrew.Any(uc => uc.value == ec.PersonId)).ToList();
                    var charactersToRemove = existingCharacterCrew.Where(ec => !updatedActorCrew.Any(uc => uc.value == ec.ActorId)).ToList();

                    foreach (var r in actorsToRemove)
                    {
                        existingActorCrew.Remove(r);
                        database.TvShowCrew.Remove(r);

                    }

                    foreach(var r in charactersToRemove)
                    {
                        existingCharacterCrew.Remove(r);
                        database.TvShowCharacters.Remove(r);
                    }

                    var charactersToUpdate = updatedActorCrew.Where(uc => !existingCharacterCrew.Any(ec => ec.ActorId == uc.value && uc.CharacterName == ec.Name && uc.Description == ec.Description)).ToList();

                    foreach (var u in charactersToUpdate)
                    {
                        int Id = existingCharacterCrew.Where(q => q.ActorId == u.value).Select(s => s.Id).FirstOrDefault();
                        if (Id > 0)
                        {
                            var update = await database.TvShowCharacters.Where(q => q.Id == Id).FirstOrDefaultAsync();
                            update.Description = u.Description;
                            update.Name = u.CharacterName;
                            database.TvShowCharacters.Update(update);
                        }
                    }

                    var crewToAdd = updatedActorCrew.Where(uc => !existingActorCrew.Any(ec => ec.PersonId == uc.value)).ToList();
                    var charactersToAdd = updatedActorCrew.Where(uc => !existingCharacterCrew.Any(ec => ec.ActorId == uc.value)).ToList();

                    foreach (var a in crewToAdd)
                    {
                      await database.TvShowCrew.AddAsync(new TvShowCrewEntity
                       {
                           TvShowId = TVShow.Id,
                           PersonId = Convert.ToInt32(a.value),
                           Role = CrewRoleEnum.Actor
                       });
                    }

                    foreach (var a in charactersToAdd)
                    {
                       await database.TvShowCharacters.AddAsync(new TvShowCharactersEntity
                        {
                            TvShowId = TVShow.Id,
                            ActorId = Convert.ToInt32(a.value),
                            Name = a.CharacterName.Trim(),
                            Description = a.Description.Trim(),
                        });
                    }

                    await SaveTVShowCrew(TVShow.Creators, CrewRoleEnum.Creator, tvShowDb.Id);
                
                    await database.SaveChangesAsync();
                }
                else
                {
                    byte[] s = DataActions.ImageToByte(TVShow.TVShowImageData);

                    var genres = DataActions.FormatGenres(TVShow.Genres);

                    var newShow = new TvShowEntity
                    {
                        Title = TVShow.Title.Trim(),
                        Description = TVShow.Description.Trim(),
                        Runtime = TVShow.Runtime,
                        Genres = genres,
                        TotalSeason = TVShow.TotalSeason,
                        TotalEpisode = TVShow.TotalEpisode,
                        TVShowImageData = s,
                    };

                    await database.TvShow.AddAsync(newShow);

                    await database.SaveChangesAsync();

                    int m = newShow.Id;

                    foreach (var a in JsonSerializer.Deserialize<List<ActorSelectDTO>>(TVShow.Actors))
                    {
                        await database.TvShowCrew.AddAsync(new TvShowCrewEntity
                        {
                            TvShowId = m,
                            PersonId = Convert.ToInt32(a.value),
                            Role = CrewRoleEnum.Actor
                        });

                        await database.TvShowCharacters.AddAsync(new TvShowCharactersEntity
                        {
                            TvShowId = m,
                            ActorId = Convert.ToInt32(a.value),
                            Name = a.CharacterName.Trim(),
                            Description = a.Description.Trim(),
                        });
                    }

                    foreach (var a in TVShow.Creators.Split(',').Reverse().ToList<string>())
                    {
                        await database.TvShowCrew.AddAsync(new TvShowCrewEntity
                        {
                            TvShowId = TVShow.Id,
                            PersonId = Convert.ToInt32(a),
                            Role = CrewRoleEnum.Creator
                        });
                    }
                }
                await database.SaveChangesAsync();

                transaction.Commit();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<int> GetTVShowCount()
        {
            return await database.TvShow.CountAsync();
        }

        private async Task SaveTVShowCrew(string Crew, CrewRoleEnum crewRoleEnum, int tvShowId)
        {

            var existingCrew = await database.TvShowCrew.Where(c => c.TvShowId == tvShowId && c.Role == crewRoleEnum).ToListAsync();
            var updatedCrew = Crew.Split(",").ToList();

            var crewToRemove = existingCrew.Where(ec => !updatedCrew.Any(uc => Convert.ToInt32(uc) == ec.PersonId)).ToList();

            foreach (var r in crewToRemove)
            {
                existingCrew.Remove(r);
                database.TvShowCrew.Remove(r);
            }

            var creatorsToAdd = updatedCrew.Where(uc => !existingCrew.Any(ec => ec.PersonId == Convert.ToInt32(uc))).ToList();

            foreach (var a in creatorsToAdd)
            {
                await database.TvShowCrew.AddAsync(new TvShowCrewEntity
                {
                    TvShowId = tvShowId,
                    PersonId = Convert.ToInt32(a),
                    Role = crewRoleEnum
                });
            }
        }



    }

 }

