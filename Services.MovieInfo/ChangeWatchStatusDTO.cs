

namespace Services.MovieTVShowInfo
{
    public class ChangeWatchStatusDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public int? Score {  get; set; }
        public string? Review { get; set; }
        public int? SeasonsWatched { get; set; }
        public int? EpisodesWatched { get; set; }

    }
}
