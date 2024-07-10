using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Frontpage
{
    public interface IFrontpageService
    {
        public Task<List<MediaListDTO>> GetMoviesList(int PostPerPage, int Page, string? Search);
        public Task<List<MediaListDTO>> GetTVShowList(int PostPerPage, int Page, string? Search);

    }
}
