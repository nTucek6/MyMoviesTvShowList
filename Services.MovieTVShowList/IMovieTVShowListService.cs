using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MovieTVShowList
{
    public interface IMovieTVShowListService
    {
        public Task<List<UserListDTO>> GetUserMovieList(string Username);
        public Task<List<UserListDTO>> GetUserTVShowList(string Username);

    }
}
