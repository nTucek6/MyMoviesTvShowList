using Entites.Enum;

using System.ComponentModel.DataAnnotations;


namespace Entites.List
{
    public class WatchedTvShowListEntity
    {
        [Key]
        public int Id { get; set; }
        public int TVShowId { get; set; }
        public int UserId { get; set; }
        public WatchStatusEnum WatchStatus { get; set; }
        public int SeasonsWatched { get; set; }
        public int EpisodesWatched { get; set; }
        public int? Score { get; set; }
        public DateTime TimeAdded { get; set; }

    }
}
