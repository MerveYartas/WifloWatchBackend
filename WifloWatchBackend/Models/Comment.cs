using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        
        public int ContentId { get; set; }  // Yorumun yapıldığı içerik (film/dizi)
        
        public string ContentType { get; set; }

        
        public int UserId { get; set; }  // Yorum yapan kullanıcı

        
        [MaxLength(500)]  // Yorum uzunluğunu 500 karakterle sınırla
        public string CommentText { get; set; }  // Yorum metni

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Varsayılan olarak otomatik tarih ekle
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // İlişkiler
        [ForeignKey("ContentId")]
        public virtual Movie Movie { get; set; }  // Yorum yapılan film

        [ForeignKey("UserId")]
        public virtual User User { get; set; }  // Yorum yapan kullanıcı
    }
}
