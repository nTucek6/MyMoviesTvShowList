using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites
{
    public class MoviesCrewEntity
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int PersonId { get; set; }

        public CrewEnum PersonnelType { get; set; }

        public string? CharacterName { get; set; }
    }
}
