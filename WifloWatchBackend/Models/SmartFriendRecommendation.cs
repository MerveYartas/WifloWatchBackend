using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class SmartFriendRecommendation
    {
        [Key]  
        public int Id { get; set; }

        [Required] 
        public int UserId { get; set; }  

        [Required]  
        public int RecommendedUserId { get; set; }  // Önerilen kullanıcı

        [MaxLength(1000)]   
        public string Reason { get; set; }  // Öneri nedeni 

        [Required]  
        public DateTime CreatedAt { get; set; } 

        // İlişkiler
        [ForeignKey("UserId")] 
        public virtual User User { get; set; }  // Öneriyi alan kullanıcı

        [ForeignKey("RecommendedUserId")] 
        public virtual User RecommendedUser { get; set; }  // Önerilen kullanıcı
    }
}
