
using Microsoft.AspNetCore.Http;

namespace Services.TVShowsAdmin
{
    public class SaveTVShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genres { get; set; }
        public string Director { get; set; }
        public string Writers { get; set; }
        public string Actors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TotalSeason {  get; set; }
        public int TotalEpisode { get; set; }
        public IFormFile? MovieImageData { get; set; }

    }
}
