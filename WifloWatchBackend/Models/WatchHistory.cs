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
        public int MovieId { get; set; } 
        [Required]
        public DateTime WatchedAt { get; set; } = DateTime.UtcNow;
      
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}
