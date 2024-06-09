using DatabaseContext;
using Entites;
using Entites.Enum;
using Entites.Movie;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using MyMoviesTvShowList.Extensions;
using System.Linq.Expressions;
using System.Text.Json;


namespace Services.MoviesAdmin
{
    public class MoviesAdminService : IMoviesAdminService
    {
        private readonly MyMoviesTvShowListContext database;

        public MoviesAdminService(MyMoviesTvShowListContext database)
        {
            this.database = database;
        }

        public async Task<List<GenresSelectDTO>> GetGenres()
        {
            var genres = Enum.GetValues(typeof(GenresEnum)).Cast<GenresEnum>().ToList().Select(x => new GenresSelectDTO { value = x, label = x.GetDescription() }).OrderBy(o => o.label).ToList();
            return genres;
        }

        public async Task UpdateMoviesScore()
        {
            //var movies = await database.Movies.ToListAsync();

            //foreach (var m in movies)
            //{
            //    var ratings = await database.UsersMovieList
            //                    .Where(q => q.MovieId == m.Id)
            //                    .Select(s => s.Score)
            //                    .ToListAsync();

            //    if (ratings.Count() > 0)
            //    {
            //        int[] ri = new int[5];

            //        Array.Fill(ri, 0);

            //        foreach (var score in ratings)
            //        {
            //            if (score != null)
            //            {
            //                for (int i = 1; i <= 5; i++)
            //                {
            //                    if (i == score)
            //                    {
            //                        ri[i - 1]++;
            //                    }
            //                }
            //            }
            //        }

            //        if (ri[4] + ri[3] + ri[2] + ri[1] + ri[0] > 0)
            //        {
            //            decimal numerator = (5 * ri[4] + 4 * ri[3] + 3 * ri[2] + 2 * ri[1] + 1 * ri[0]);
            //            decimal denominator = (ri[4] + ri[3] + ri[2] + ri[1] + ri[0]);

            //            decimal rating = Decimal.Divide(numerator, denominator);

            //            m.Rating = rating;
            //        }
            //    }
            //}
            //await database.SaveChangesAsync();

        }

        public async Task<List<CrewsSelectDTO>> GetCrewSelectSearch(string search)
        {
            var people = await database.People
            .Where(q => q.FirstName.Contains(search) || q.LastName.Contains(search))
            .Select(s =>
            new CrewsSelectDTO
            {
                value = s.Id,
                label = s.FirstName + " " + s.LastName
            }).ToListAsync();

            return people;
        }

        public async Task<List<MoviesDTO>> GetMovies(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<MoviesEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.MovieName.Contains(Search);
            }

            var data = await database.Movies
                    .Where(predicate)
                    .OrderBy(m => m.MovieName)
                    .Skip((Page - 1) * PostPerPage)
                    .Take(PostPerPage).ToListAsync();

            List<MoviesDTO> movies = new List<MoviesDTO>();

            foreach (var movie in data)
            {
                var act = await database.MoviesCrew.Where(a => a.MovieId == movie.Id && a.Role == CrewRoleEnum.Actor).ToListAsync();

                var dir = await database.MoviesCrew.Where(a => a.MovieId == movie.Id && a.Role == CrewRoleEnum.Director).ToListAsync();

                var wri = await database.MoviesCrew.Where(a => a.MovieId == movie.Id && a.Role == CrewRoleEnum.Screenwriter).ToListAsync();

                List<string> g = movie.Genres.Split(",").ToList();

                List<ActorDTO> actors = new List<ActorDTO>();

                List<PeopleEntity> director = new List<PeopleEntity>();

                List<PeopleEntity> writers = new List<PeopleEntity>();

                List<GenresSelectDTO> genres = new List<GenresSelectDTO>();

                foreach (var a in act)
                {
                    var q = await database.People.Where(p => p.Id == a.PersonId)
                   .Select(s => new ActorDTO
                   {
                       Id = s.Id,
                       FirstName = s.FirstName,
                       LastName = s.LastName,
                   })
                   .FirstOrDefaultAsync();

                    var Character = await database.MoviesCharacters.Where(p => p.ActorId == a.PersonId).FirstOrDefaultAsync();

                    q.CharacterName = Character.Name;
                    q.CharacterDescription = Character.Description;

                    actors.Add(q);
                }

                foreach (var d in dir)
                {
                    var q = await database.People.Where(p => p.Id == d.PersonId)
                        .Select(s=> new PeopleEntity 
                        {   
                            Id = s.Id, 
                            FirstName = s.FirstName, 
                            LastName = s.LastName,
                        }).FirstOrDefaultAsync();
                    director.Add(q);
                }
                foreach (var w in wri)
                {
                    var q = await database.People.Where(p => p.Id == w.PersonId).Select(s => new PeopleEntity
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                    }).FirstOrDefaultAsync();
                    writers.Add(q);
                }
                foreach (string d in g)
                {
                    genres.Add(new GenresSelectDTO
                    {
                        value = (GenresEnum)(Convert.ToInt32(d)),
                        label = ((GenresEnum)(Convert.ToInt32(d))).GetDescription()

                    });
                }

                movies.Add(new MoviesDTO
                {
                    Id = movie.Id,
                    MovieName = movie.MovieName,
                    Actors = actors,
                    Writers = writers,
                    Director = director,
                    ReleaseDate = movie.ReleaseDate,
                    Duration = movie.Duration,
                    MovieImageData = movie.MovieImageData,
                    Synopsis = movie.Synopsis,
                    Genres = genres
                });
            }
            return movies;
        }
        public async Task SaveMovie(SaveMovieDTO movie)
        {
            var transaction = database.Database.BeginTransaction();

            try
            {
                if (movie.Id > 0)
                {
                    var movieDb = await database.Movies.Where(w => w.Id == movie.Id).FirstOrDefaultAsync();

                    var genres = DataActions.FormatGenres(movie.Genres);

                    movieDb.MovieName = movie.MovieName.Trim();
                    movieDb.Synopsis = movie.Synopsis.Trim();
                    movieDb.Genres = genres;
                    movieDb.Duration = movie.Duration;
                    movieDb.ReleaseDate = movie.ReleaseDate.ToUniversalTime();

                    if (movie.MovieImageData.Length > 0)
                    {
                        movieDb.MovieImageData = DataActions.ImageToByte(movie.MovieImageData);
                    }

                    await database.SaveChangesAsync();

                    var existingActorCrew = await database.MoviesCrew.Where(c => c.MovieId == movieDb.Id && c.Role == CrewRoleEnum.Actor).ToListAsync();
                    var existingCharacterCrew = await database.MoviesCharacters.Where(c => c.MovieId == movieDb.Id).ToListAsync();

                    var updatedActorCrew = JsonSerializer.Deserialize<List<ActorSelectDTO>>(movie.Actors);

                    var actorsToRemove = existingActorCrew.Where(ec => !updatedActorCrew.Any(uc => uc.value == ec.PersonId)).ToList();
                    var charactersToRemove = existingCharacterCrew.Where(ec => !updatedActorCrew.Any(uc => uc.value == ec.ActorId)).ToList();

                    foreach (var r in actorsToRemove)
                    {
                        existingActorCrew.Remove(r);
                        database.MoviesCrew.Remove(r);
                    }

                    foreach (var r in charactersToRemove)
                    {
                        existingCharacterCrew.Remove(r);
                        database.MoviesCharacters.Remove(r);
                    }

                    var charactersToUpdate = updatedActorCrew.Where(uc => !existingCharacterCrew.Any(ec => ec.ActorId == uc.value && uc.CharacterName == ec.Name && uc.Description == ec.Description)).ToList();

                    foreach (var u in charactersToUpdate)
                    {
                        int Id = existingCharacterCrew.Where(q=> q.ActorId == u.value).Select(s=> s.Id).FirstOrDefault();

                        if(Id > 0)
                        {
                            var update = await database.MoviesCharacters.Where(q => q.Id == Id).FirstOrDefaultAsync();

                            update.Description = u.Description;
                            update.Name = u.CharacterName;

                            database.MoviesCharacters.Update(update);
                        }
                    }

                    var actorsToAdd = updatedActorCrew.Where(uc => !existingActorCrew.Any(ec => ec.PersonId == uc.value)).ToList();

                    var charactersToAdd = updatedActorCrew.Where(uc => !existingCharacterCrew.Any(ec => ec.ActorId == uc.value)).ToList();

                    foreach (var a in actorsToAdd)
                    {
                        await database.MoviesCrew.AddAsync(new MoviesCrewEntity
                        {
                            MovieId = movie.Id,
                            PersonId = Convert.ToInt32(a.value),
                            Role = CrewRoleEnum.Actor
                        });
                    }

                    foreach (var a in charactersToAdd)
                    {
                        await database.MoviesCharacters.AddAsync(new MoviesCharactersEntity
                        {
                            MovieId = movie.Id,
                            ActorId = Convert.ToInt32(a.value),
                            Name = a.CharacterName.Trim(),
                            Description = a.Description.Trim(),
                        });
                    }

                    await SaveMoviesCrew(movie.Director, CrewRoleEnum.Director, movieDb.Id);
                    await SaveMoviesCrew(movie.Writers, CrewRoleEnum.Screenwriter, movieDb.Id);
        
                    await database.SaveChangesAsync();

                }
                else
                {
                    byte[] s = DataActions.ImageToByte(movie.MovieImageData);

                    var genres = DataActions.FormatGenres(movie.Genres);

                    await database.Movies.AddAsync(new MoviesEntity
                    {
                        MovieName = movie.MovieName.Trim(),
                        Synopsis = movie.Synopsis.Trim(),
                        Genres = genres,
                        Duration = movie.Duration,
                        ReleaseDate = movie.ReleaseDate.ToUniversalTime(),
                        MovieImageData = s,
                        DateTimeAdded = DateTime.Now.ToUniversalTime()
                    });

                    await database.SaveChangesAsync();

                    var m = await database.Movies.Where(w => w.MovieName == movie.MovieName).Select(s => s.Id).FirstOrDefaultAsync();

                    foreach (var a in JsonSerializer.Deserialize<List<ActorSelectDTO>>(movie.Actors))
                    {
                        await database.MoviesCrew.AddAsync(new MoviesCrewEntity
                        {
                            MovieId = m,
                            PersonId = Convert.ToInt32(a.value),
                            Role = CrewRoleEnum.Actor
                        });

                        await database.MoviesCharacters.AddAsync(new MoviesCharactersEntity
                        {
                            MovieId = m,
                            ActorId = Convert.ToInt32(a.value),
                            Name = a.CharacterName.Trim(),
                            Description = a.Description.Trim()
                        });
                    }

                    foreach (var a in movie.Director.Split(",").ToList())
                    {
                        await database.MoviesCrew.AddAsync(new MoviesCrewEntity
                        {
                            MovieId = m,
                            PersonId = Convert.ToInt32(a),
                            Role = CrewRoleEnum.Director
                        });
                    }

                    foreach (var a in movie.Writers.Split(",").ToList())
                    {
                        await database.MoviesCrew.AddAsync(new MoviesCrewEntity
                        {
                            MovieId = m,
                            PersonId = Convert.ToInt32(a),
                            Role = CrewRoleEnum.Screenwriter
                        });
                    }
                    await database.SaveChangesAsync();
                }
                transaction.Commit();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task SaveMoviesCrew(string Crew, CrewRoleEnum crewRoleEnum, int movieId)
        {

            var existingCrew = await database.MoviesCrew.Where(c => c.MovieId == movieId && c.Role == crewRoleEnum).ToListAsync();
            var updatedCrew = Crew.Split(",").ToList();

            var crewToRemove = existingCrew.Where(ec => !updatedCrew.Any(uc => Convert.ToInt32(uc) == ec.PersonId)).ToList();

            foreach (var r in crewToRemove)
            {
                existingCrew.Remove(r);
                database.MoviesCrew.Remove(r);
            }

            var creatorsToAdd = updatedCrew.Where(uc => !existingCrew.Any(ec => ec.PersonId == Convert.ToInt32(uc))).ToList();

            foreach (var a in creatorsToAdd)
            {
                await database.MoviesCrew.AddAsync(new MoviesCrewEntity
                {
                    MovieId = movieId,
                    PersonId = Convert.ToInt32(a),
                    Role = crewRoleEnum
                });
            }
        }

    }
}
