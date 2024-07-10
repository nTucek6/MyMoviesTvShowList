using DatabaseContext;
using Entites.Movie;
using Entites.Show;
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

        public async Task<List<MediaListDTO>> GetMoviesList(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<MoviesEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.MovieName.Contains(Search);
            }

            List<MediaListDTO> movies = await database.Movies
                    .Where(predicate)
                    .Select(s => new MediaListDTO
                    { 
                        Id = s.Id, 
                        Title = s.MovieName,
                        Type = "movie",
                    })
                    .OrderBy(m => m.Title)
                    .Skip((Page - 1) * PostPerPage)
                    .Take(PostPerPage).ToListAsync();

            return movies;            
        }

        public async Task<List<MediaListDTO>> GetTVShowList(int PostPerPage, int Page, string? Search)
        {
            Expression<Func<TvShowEntity, bool>> predicate = x => true;

            if (!String.IsNullOrEmpty(Search))
            {
                predicate = x => x.Title.Contains(Search);
            }

            List<MediaListDTO> tvshow = await database.TvShow
                    .Where(predicate)
                    .Select(s => new MediaListDTO
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Type = "tvshow",
                    })
                    .OrderBy(m => m.Title)
                    .Skip((Page - 1) * PostPerPage)
                    .Take(PostPerPage).ToListAsync();

            return tvshow;
        }

    }
}