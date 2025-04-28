using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Post
    {
        [Key] 
        public int Id { get; set; }

        [Required]  
        public int UserId { get; set; }  

        [Required] 
        [StringLength(1000)]  
        public string Text { get; set; }

        [MaxLength(500)] 
        public string? Media { get; set; }

        [Required] 
        public DateTime Date { get; set; } 

        // İlişkiler
        [ForeignKey("UserId")] 
        public virtual User? User { get; set; } 
    }
}
