using Entites.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entites.List
{
    public class WatchedMoviesListEntity
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public WatchStatusEnum WatchStatus { get; set; }
        public int? Score { get; set; }
        public string? StopedAt { get; set; }
        public DateTime TimeAdded { get; set; }

    }
}
