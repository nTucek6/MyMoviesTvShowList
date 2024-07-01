using DatabaseContext;
using Entites;
using Entites.List;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MovieTVShowList
{
    public class MovieTVShowListService : IMovieTVShowListService
    {
        private readonly MyMoviesTvShowListContext database;

        public MovieTVShowListService(MyMoviesTvShowListContext myMoviesTvShow)
        {
            database = myMoviesTvShow;
        }

        public async Task<List<UserListDTO>> GetUserMovieList(string Username)
        {
            int user = await database.Users.Where(q => q.Username == Username).Select(s => s.Id ).FirstOrDefaultAsync();

            if (user != 0) 
            {
                List<UserListDTO> list = new List<UserListDTO>();

                var data = await database.WatchedMoviesLists.Where(q=> q.UserId == user).Select(s => new WatchedMoviesListEntity { MovieId = s.MovieId, Score = s.Score}).ToListAsync();
                   
                foreach (var item in data) 
                {
                    var m = await database.Movies.Where(q=> q.Id == item.MovieId).Select(s => s.MovieName).FirstOrDefaultAsync();

                    if(m!= null)
                    {
                        list.Add(new UserListDTO { Id = item.MovieId, Title = m, Score = item.Score });
                    }
                }
                return list;
            }
            else
            {
                throw new Exception("No user found!");
            }
        }

        public async Task<List<UserListDTO>> GetUserTVShowList(string Username)
        {
            int user = await database.Users.Where(q => q.Username == Username).Select(s => s.Id).FirstOrDefaultAsync();

            if (user != 0)
            {
                List<UserListDTO> list = new List<UserListDTO>();

                var data = await database.WatchedTvShowList.Where(q => q.UserId == user).Select(s => new WatchedMoviesListEntity { MovieId = s.TVShowId, Score = s.Score }).ToListAsync();

                foreach (var item in data)
                {
                    var m = await database.TvShow.Where(q => q.Id == item.MovieId).Select(s => s.Title).FirstOrDefaultAsync();

                    if (m != null)
                    {
                        list.Add(new UserListDTO { Id = item.MovieId, Title = m, Score = item.Score });
                    }
                }
                return list;
            }
            else
            {
                throw new Exception("No user found!");
            }
        }
    }
}
