using Entites;

namespace Services.MoviesAdmin
{
    public class MoviesDTO
    {
        public int Id { get; set; }

        public string? MovieName { get; set; }

        public string? Synopsis { get; set; }

        public decimal? Rating { get; set; }

        public List<GenresSelectDTO> Genres { get; set; }

        public List<PeopleEntity> Director { get; set; }

        public List<PeopleEntity> Writers { get; set; }

        public List<ActorDTO> Actors { get; set; }

        public DateTime ReleaseDate { get; set; }
        public decimal Duration { get; set; }
        public int? RatingsCount { get; set; }
        public byte[] MovieImageData { get; set; }
    }
}
