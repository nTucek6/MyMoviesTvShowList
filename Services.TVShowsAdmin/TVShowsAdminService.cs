using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using DatabaseContext;
using Entites;
using Entites.Enum;
using Entites.Movie;
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
                    tvShowDb.Description = TVShow.Description;
                    tvShowDb.Genres = genres;
                    tvShowDb.Runtime = TVShow.Runtime;
                   

                    if (TVShow.TVShowImageData != null)
                    {
                        tvShowDb.TVShowImageData = DataActions.ImageToByte(TVShow.TVShowImageData);
                    }

                    await database.SaveChangesAsync();

                    await database.TvShowCrew.Where(c => c.TvShowId == tvShowDb.Id).ExecuteDeleteAsync();
                    await database.TvShowCharacters.Where(c => c.TvShowId == tvShowDb.Id).ExecuteDeleteAsync();

                    foreach (var a in JsonSerializer.Deserialize<List<ActorSelectDTO>>(TVShow.Actors))
                    {
                        await database.TvShowCrew.AddAsync(new TvShowCrewEntity
                        {
                            TvShowId = TVShow.Id,
                            PersonId = Convert.ToInt32(a.value),
                            Role = CrewRoleEnum.Actor
                        });

                        await database.TvShowCharacters.AddAsync(new TvShowCharactersEntity
                        {
                            TvShowId = TVShow.Id,
                            ActorId = Convert.ToInt32(a.value),
                            Name = a.CharacterName,
                            Description = a.Description,
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

                    await database.SaveChangesAsync();

                }
                else
                {
                    byte[] s = DataActions.ImageToByte(TVShow.TVShowImageData);

                    var genres = DataActions.FormatGenres(TVShow.Genres);

                    var newShow = new TvShowEntity
                    {
                        Title = TVShow.Title,
                        Description = TVShow.Description,
                        Runtime = TVShow.Runtime,
                        Genres = genres,
                        TotalSeason = TVShow.TotalSeason,
                        TotalEpisode = TVShow.TotalEpisode,
                        TVShowImageData = s,

                    };

                    await database.TvShow.AddAsync(newShow);

                    await database.SaveChangesAsync();

                    //var m = await database.TvShow.Where(w => w.Title == TVShow.Title).Select(s => s.Id).FirstOrDefaultAsync();

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
                            Name = a.CharacterName,
                            Description = a.Description,
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
          


    }

 }

