using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class WatchHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }  // İzlenen film ID'si

        [Required]
        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;

        // İlişkiler
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
