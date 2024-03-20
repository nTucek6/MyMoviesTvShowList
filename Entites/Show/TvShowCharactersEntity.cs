using System.ComponentModel.DataAnnotations;

namespace Entites.Show
{
    public class TvShowCharactersEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TvShowId { get; set; }
        public int ActorId { get; set; }
        public string Description { get; set; }
    }
}
