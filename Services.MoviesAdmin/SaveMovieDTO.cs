using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MoviesAdmin
{
    public class SaveMovieDTO
    {
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public string? Synopsis { get; set; }
        public string Genres { get; set; }
        public string Director { get; set; }
        public string Writers { get; set; }
        public string Actors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Duration { get; set; }
        public IFormFile? MovieImageData { get; set; }
    }
}
