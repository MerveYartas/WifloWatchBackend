using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Movie
    {
        [Key]  
        public int Id { get; set; }

        [Required]  
        [StringLength(255)]  
        public string Title { get; set; }

        [StringLength(1000)] 
        public string Description { get; set; }

        [Required]  
        [StringLength(50)]  
        public string Type { get; set; }  

        [Required]  
        [StringLength(100)] 
        public string Genre { get; set; }  

        [Required]  
        [StringLength(50)]  
        public string Language { get; set; }  

        [Required]  
        public DateTime ReleaseDate { get; set; } 

        [Required]  
        [StringLength(100)]  
        public string Country { get; set; }  

        [StringLength(500)]
        [Url]
        public string PosterUrl { get; set; }

        // İlişkiler
        public ICollection<WatchList> WatchLists { get; set; } = new List<WatchList>();
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<WatchHistory> WatchHistories { get; set; }

    }
}
