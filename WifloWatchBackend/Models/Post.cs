using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Post
    {
        [Key]  // Bu özellik, Post sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Kullanıcı ID'si zorunlu
        public int UserId { get; set; }  // Gönderen kullanıcı

        [Required]  // Post metni zorunlu
        [StringLength(1000)]  // Metin en fazla 1000 karakter uzunluğunda olabilir
        public string Text { get; set; }

        [MaxLength(500)]  // Resim/Video URL'si en fazla 500 karakter uzunluğunda olabilir
        public string Media { get; set; }  // Resim/Video URL

        [Required]  // Tarih zorunlu
        public DateTime Date { get; set; }  // Post tarihi

        // İlişkiler
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public virtual User User { get; set; }  // Gönderen kullanıcı
    }
}
