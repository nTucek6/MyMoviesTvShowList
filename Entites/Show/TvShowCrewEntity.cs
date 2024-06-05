using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites.Show
{
    public class TvShowCrewEntity
    {
        [Key]
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public int PersonId { get; set; }
        public CrewRoleEnum Role { get; set; }
    }
}
