using System.ComponentModel.DataAnnotations;

namespace Entites.Movie
{
    public class MoviesCharactersEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public string Description { get; set; }

    }
}
