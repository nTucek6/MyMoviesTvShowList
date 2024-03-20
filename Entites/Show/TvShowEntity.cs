using System.ComponentModel.DataAnnotations;

namespace Entites.Show
{
    public class TvShowEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genres { get; set; }
        public decimal? Rating { get; set; }
        public int TotalSeason {  get; set; }
        public int TotalEpisode { get; set; }
    }
}
