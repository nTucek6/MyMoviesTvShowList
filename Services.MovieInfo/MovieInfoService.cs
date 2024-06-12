using DatabaseContext;
using Entites.Enum;
using Entites;
using Microsoft.EntityFrameworkCore;
using Services.MoviesAdmin;
using Services.ExternalApiCalls;

namespace Services.MovieInfo
{
    public class MovieInfoService : IMovieInfoService
    {
        private readonly MyMoviesTvShowListContext database;
        private readonly IExternalApiCallsService externalApiCalls;

        public MovieInfoService(MyMoviesTvShowListContext database, IExternalApiCallsService externalApiCalls)
        {
            this.database = database;
            this.externalApiCalls = externalApiCalls;
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
                    var d = externalApiCalls.SearchGenre(g);
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

    }
}
