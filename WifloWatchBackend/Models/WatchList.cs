using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class WatchList
    {
        [Required]  
        [StringLength(200)] 
        public string Name { get; set; } 

        [Required] 
        public int UserId { get; set; }  

        [Required] 
        public int MovieId { get; set; }  
        public bool IsWatched { get; set; }

        [Required] 
        [StringLength(50)]  
        public string ListType { get; set; }  

        // İlişkiler
        [ForeignKey("UserId")]  
        public User User { get; set; }  

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }  
    }
}
