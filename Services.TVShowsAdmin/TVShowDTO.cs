using Entites;
using Services.MoviesAdmin;

namespace Services.TVShowsAdmin
{
    public class TVShowDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal? Rating { get; set; }

        public List<GenresSelectDTO> Genres { get; set; }

        public List<PeopleEntity> Creators { get; set; }

        public List<ActorDTO> Actors { get; set; }
        public string Runtime { get; set; }
        public decimal TotalSeasons { get; set; }
        public decimal TotalEpisodes { get; set; }
        public int? RatingsCount { get; set; }
        public byte[] TVShowImageData { get; set; }

    }
}
