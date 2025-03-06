using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Like
    {
        [Key]  // Bu özellik, Like sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Beğenilen içerik zorunlu
        public int ContentId { get; set; }  // Beğenilen içerik (film/dizi)
        [Required]
        public string ContentType { get; set; }

        [Required]  // Beğeniyi yapan kullanıcı zorunlu
        public int UserId { get; set; }  // Beğeniyi yapan kullanıcı

        [Required]  // Beğeninin yapıldığı zaman zorunlu
        public DateTime LikedAt { get; set; }  // Beğeninin yapıldığı zaman

        // Beğenilen içerik ile ilişkili Movie modeline işaret eder
        [ForeignKey("ContentId")]  // ContentId'yi Movie ile ilişkilendirir
        public virtual Movie Movie { get; set; }  // Beğenilen içerik

        // Beğeniyi yapan kullanıcı ile ilişkili User modeline işaret eder
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public virtual User User { get; set; }  // Beğeniyi yapan kullanıcı
    }
}
