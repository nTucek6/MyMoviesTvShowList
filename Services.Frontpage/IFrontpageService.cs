﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Frontpage
{
    public interface IFrontpageService
    {
        public Task<List<MoviesListDTO>> GetMoviesList(int PostPerPage, int Page, string? Search);

    }
}
