using Entites.Enum;

namespace Entites
{
    public class MoviesCrewEntity
    {
        public Guid Id { get; set; }

        public Guid MovieId { get; set; }

        public Guid PersonId { get; set; }

        public CrewEnum PersonnelType { get; set; }

        public string? CharacterName { get; set; }
    }
}
