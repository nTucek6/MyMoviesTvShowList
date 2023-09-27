using DatabaseContext;
using Microsoft.EntityFrameworkCore;


namespace Services.MoviesAdmin
{
    public class MoviesAdminService : IMoviesAdminService
    {
        private readonly MyMoviesTvShowListContext database;

        public MoviesAdminService(MyMoviesTvShowListContext database)
        {
            this.database = database;
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
    }
}
