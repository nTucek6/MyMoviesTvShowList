using DatabaseContext;
using Entites.Enum;
using Entites;
using Microsoft.EntityFrameworkCore;
using Services.MoviesAdmin;
using Services.ExternalApiCalls;
using Entites.List;
using Entities.Enum;
using MyMoviesTvShowList.Extensions;
using Services.TVShowsAdmin;

namespace Services.MovieTVShowInfo
{
    public class MovieTVShowInfoService : IMovieTVShowInfoService
    {
        private readonly MyMoviesTvShowListContext database;
        private readonly IExternalApiCallsService externalApiCalls;

        public MovieTVShowInfoService(MyMoviesTvShowListContext database, IExternalApiCallsService externalApiCalls)
        {
            this.database = database;
            this.externalApiCalls = externalApiCalls;
        }
        public async Task<List<WatchStatusSelectDTO>> GetMovieWatchStatus()
        {
            var status = Enum.GetValues(typeof(WatchStatusEnum)).Cast<WatchStatusEnum>().ToList().Where(q=> q > 0).Select(x => new WatchStatusSelectDTO { value = x, label = x.GetDescription() }).OrderBy(o => o.value).ToList();
            return status;
        }
        public async Task ChangeMovieListStatus(ChangeWatchStatusDTO statusDTO)
        {
            var transaction = database.Database.BeginTransaction();
            try
            {
                var data = await database.WatchedMoviesLists.Where(q => q.MovieId == statusDTO.Id && q.UserId == statusDTO.UserId).FirstOrDefaultAsync();

                if (data != null)
                {
                    data.WatchStatus = (WatchStatusEnum)statusDTO.Status;
                    data.Score = statusDTO.Score;
                    data.Review = statusDTO.Review;
                    database.Update(data);
                }
                else
                {
                    await database.AddAsync(new WatchedMoviesListEntity
                    {
                        MovieId = statusDTO.Id,
                        UserId = statusDTO.UserId,
                        Score = statusDTO.Score,
                        WatchStatus = (WatchStatusEnum)statusDTO.Status,
                        TimeAdded = DateTime.Now.ToUniversalTime(),
                        Review = statusDTO.Review,
                    });

                }
                await database.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public async Task ChangeTVShowListStatus(ChangeWatchStatusDTO statusDTO)
        {
            var transaction = database.Database.BeginTransaction();
            try
            {
                var data = await database.WatchedTvShowList.Where(q => q.TVShowId == statusDTO.Id && q.UserId == statusDTO.UserId).FirstOrDefaultAsync();

                if (data != null)
                {
                    data.WatchStatus = (WatchStatusEnum)statusDTO.Status;
                    data.Score = statusDTO.Score;
                    data.Review = statusDTO.Review;
                    data.SeasonsWatched = statusDTO.SeasonsWatched;
                    data.EpisodesWatched = statusDTO.EpisodesWatched;

                    if ((WatchStatusEnum)statusDTO.Status == WatchStatusEnum.Completed)
                    {
                        var totalSeasonsEpisodes = await database.TvShow.Where(q => q.Id == statusDTO.Id).Select(s => new { TotalSeasons = s.TotalSeason, TotalEpisodes = s.TotalEpisode }).FirstOrDefaultAsync();
                        data.SeasonsWatched = totalSeasonsEpisodes.TotalSeasons;
                        data.EpisodesWatched = totalSeasonsEpisodes.TotalEpisodes;
                    }

                    database.Update(data);
                }
                else
                {
                    var watchedShow = new WatchedTvShowListEntity
                    {
                        TVShowId = statusDTO.Id,
                        UserId = statusDTO.UserId,
                        Score = statusDTO.Score,
                        WatchStatus = (WatchStatusEnum)statusDTO.Status,
                        TimeAdded = DateTime.Now.ToUniversalTime(),
                        Review = statusDTO.Review,
                        SeasonsWatched = statusDTO.SeasonsWatched,
                        EpisodesWatched = statusDTO.EpisodesWatched,
                    };

                    if((WatchStatusEnum)statusDTO.Status == WatchStatusEnum.Completed)
                    {
                        var totalSeasonsEpisodes = await database.TvShow.Where(q => q.Id == statusDTO.Id).Select(s => new { TotalSeasons = s.TotalSeason, TotalEpisodes = s.TotalEpisode }).FirstOrDefaultAsync();
                        watchedShow.SeasonsWatched = totalSeasonsEpisodes.TotalSeasons;
                        watchedShow.EpisodesWatched = totalSeasonsEpisodes.TotalEpisodes;
                    }

                    await database.AddAsync(watchedShow);
                }
                await database.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<MoviesDTO> GetMovieInfo(int Id)
        {
            var data = await database.Movies.Where(q=> q.Id == Id).FirstOrDefaultAsync();

            if(data != null)
            {
                List<PeopleEntity> directors = new List<PeopleEntity>();
                List<PeopleEntity> writers = new List<PeopleEntity>();
                List<ActorDTO> actors = new List<ActorDTO>();

                List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

                var dir = await database.MoviesCrew.Where(q => q.MovieId == data.Id && q.Role == CrewRoleEnum.Director).ToListAsync();
                var wri = await database.MoviesCrew.Where(q => q.MovieId == data.Id && q.Role == CrewRoleEnum.Screenwriter).ToListAsync();
                var act = await database.MoviesCrew.Where(q => q.MovieId == data.Id && q.Role == CrewRoleEnum.Actor).ToListAsync();

                var gen = data.Genres.Split(",").ToList();

                foreach(var d in dir)
                {
                    var p = await database.People.Where(q=> q.Id == d.PersonId).FirstOrDefaultAsync();
                    if(p != null)
                    {
                        directors.Add(p);
                    }
                }
                foreach (var w in wri)
                {
                    var p = await database.People.Where(q => q.Id == w.PersonId).FirstOrDefaultAsync();
                    if (p != null)
                    {
                        writers.Add(p);
                    }
                }
                foreach (var a in act)
                {
                    var p = await database.People.Where(q => q.Id == a.PersonId).FirstOrDefaultAsync();
                    var character = await database.MoviesCharacters.Where(q => q.ActorId == a.PersonId).FirstOrDefaultAsync();
                    if(p != null && character != null)
                    {
                        actors.Add(new ActorDTO
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            CharacterName = character.Name,
                            CharacterDescription = character.Description,
                            PersonImageData = p.PersonImageData,
                        });
                    }
                }
                foreach(string g in gen)
                {
                    var d = GetGenre(Convert.ToInt32(g));
                    if (d != null)
                    {
                        genres.Add(d);
                    }
                }

                return new MoviesDTO 
                {
                    Id = data.Id,
                    MovieName = data.MovieName,
                    Synopsis = data.Synopsis,
                    Rating = data.Rating,
                    Genres = genres,
                    Director = directors,
                    Writers = writers,
                    Actors = actors,
                    ReleaseDate = data.ReleaseDate,
                    Duration = data.Duration,
                    MovieImageData = data.MovieImageData,
                };
            }
             return null;
        }
        public async Task<TVShowDTO> GetTVShowInfo(int Id)
        {
            var data = await database.TvShow.Where(q => q.Id == Id).FirstOrDefaultAsync();

            if (data != null)
            {
                List<PeopleEntity> creators = new List<PeopleEntity>();
                List<ActorDTO> actors = new List<ActorDTO>();

                List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

               
                var cre = await database.TvShowCrew.Where(q => q.TvShowId == data.Id && q.Role == CrewRoleEnum.Creator).ToListAsync();
                var act = await database.TvShowCrew.Where(q => q.TvShowId == data.Id && q.Role == CrewRoleEnum.Actor).ToListAsync();

                var gen = data.Genres.Split(",").ToList();

                foreach (var d in cre)
                {
                    var p = await database.People.Where(q => q.Id == d.PersonId).FirstOrDefaultAsync();
                    if (p != null)
                    {
                        creators.Add(p);
                    }
                }
                foreach (var a in act)
                {
                    var p = await database.People.Where(q => q.Id == a.PersonId).FirstOrDefaultAsync();
                    var character = await database.TvShowCharacters.Where(q => q.ActorId == a.PersonId).FirstOrDefaultAsync();
                    if (p != null && character != null)
                    {
                        actors.Add(new ActorDTO
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            CharacterName = character.Name,
                            CharacterDescription = character.Description,
                            PersonImageData = p.PersonImageData,
                        });
                    }
                }
                foreach (string g in gen)
                {
                    var d = GetGenre(Convert.ToInt32(g));
                    if (d != null)
                    {
                        genres.Add(d);
                    }
                }

                return new TVShowDTO
                {
                    Id = data.Id,
                    Title = data.Title,
                    Description = data.Description,
                    Rating = data.Rating,
                    Genres = genres,
                    Creators = creators,
                    Actors = actors,
                    Runtime = data.Runtime,
                    //RatingsCount = data.RatingCount,
                    TotalSeason = data.TotalSeason,
                    TotalEpisode = data.TotalEpisode,
                    TVShowImageData = data.TVShowImageData,
                };
            }
            return null;
        }

        public async Task<WatchStatusSelectDTO> CheckUserMovieStatus(int UserId, int MovieId)
        {
            var userStatus = await database.WatchedMoviesLists.Where(q=> q.UserId == UserId && q.MovieId == MovieId).FirstOrDefaultAsync();

            if(userStatus != null)
            {
                var status = Enum.GetValues(typeof(WatchStatusEnum)).Cast<WatchStatusEnum>().ToList().Where(q=> q == userStatus.WatchStatus).Select(x => new WatchStatusSelectDTO { value = x, label = x.GetDescription() }).FirstOrDefault();
                return status;
            }
            return null;
        }
        public async Task<WatchStatusSelectDTO> CheckUserTVShowStatus(int UserId, int TVShowId)
        {
            var userStatus = await database.WatchedTvShowList.Where(q => q.UserId == UserId && q.TVShowId == TVShowId).FirstOrDefaultAsync();

            if (userStatus != null)
            {
                var status = Enum.GetValues(typeof(WatchStatusEnum)).Cast<WatchStatusEnum>().ToList().Where(q => q == userStatus.WatchStatus).Select(x => new WatchStatusSelectDTO { value = x, label = x.GetDescription() }).FirstOrDefault();
                return status;
            }
            return null;
        }


        private GenresSelectDTO GetGenre(int Genre)
        {
            var genre = Enum.GetValues(typeof(GenresEnum)).Cast<GenresEnum>().ToList().Where(q => q == (GenresEnum)Genre).Select(x => new GenresSelectDTO { value = x, label = x.GetDescription() }).FirstOrDefault();
            return genre;
        }


    }
}
