using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Movie
    {
        [Key]  // Bu özellik, Movie sınıfının birincil anahtarını belirtir
        public int Id { get; set; }

        [Required]  // Başlık zorunlu
        [StringLength(255)]  // Başlık uzunluğu 255 karakteri geçmemeli
        public string Title { get; set; }

        [StringLength(1000)]  // Açıklama maksimum 1000 karakter olmalı
        public string Description { get; set; }

        [Required]  // Tür zorunlu
        [StringLength(50)]  // Tür uzunluğu 50 karakteri geçmemeli
        public string Type { get; set; }  // Film ya da dizi

        [Required]  // Tür zorunlu
        [StringLength(100)]  // Tür uzunluğu 100 karakteri geçmemeli
        public string Genre { get; set; }  // Tür (aksiyon, drama vb.)

        [Required]  // Dil zorunlu
        [StringLength(50)]  // Dil uzunluğu 50 karakteri geçmemeli
        public string Language { get; set; }  // Dil (Türkçe, İngilizce vb.)

        [Required]  // Çıkış tarihi zorunlu
        public DateTime ReleaseDate { get; set; }  // Çıkış tarihi

        [Required]  // Ülke zorunlu
        [StringLength(100)]  // Ülke uzunluğu 100 karakteri geçmemeli
        public string Country { get; set; }  // Ülke

        [StringLength(500)]  // Poster URL maksimum 500 karakter olabilir
        [Url]
        public string PosterUrl { get; set; }  // Poster URL

        // İlişkiler
        public ICollection<WatchList> WatchLists { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Like> Likes { get; set; }  // Likes koleksiyonunu ekleyin
    }
}
