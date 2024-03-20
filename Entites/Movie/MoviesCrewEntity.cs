using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites.Movie
{
    public class MoviesCrewEntity
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int PersonId { get; set; }

        public MovieRoleEnum Role { get; set; }

    }
}
