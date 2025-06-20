namespace WifloWatchBackend.Models
{
    public class WatchListDto
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public bool IsWatched { get; set; }
    }
}
