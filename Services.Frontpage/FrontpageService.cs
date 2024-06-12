using DatabaseContext;
using Entites.Movie;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Frontpage
{
    public class FrontpageService : IFrontpageService
    {
        private readonly MyMoviesTvShowListContext database;

        public FrontpageService(MyMoviesTvShowListContext database)
        {
            this.database = database;
        }

        public async Task<List<MoviesListDTO>> GetMoviesList(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<MoviesEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.MovieName.Contains(Search);
            }

            List<MoviesListDTO> movies = await database.Movies
                    .Where(predicate)
                    .Select(s => new MoviesListDTO 
                    { 
                        Id = s.Id, 
                        MovieName = s.MovieName
                    })
                    .OrderBy(m => m.MovieName)
                    .Skip((Page - 1) * PostPerPage)
                    .Take(PostPerPage).ToListAsync();

            return movies;            
        }
    }
}