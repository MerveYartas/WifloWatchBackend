using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class WatchList
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public bool IsWatched { get; set; }

        // İlişkiler
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }
    }
}
