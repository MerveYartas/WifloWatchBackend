using System;
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
        public string Genre { get; set; }

        public int Duration { get; set; }  // Dakika cinsinden süre
        [StringLength(500)]
        public string Poster { get; set; }  // Afiş URL'si, yerel dosya yolunu da tutabilir

        public bool Watched { get; set; }  // Film izlendi mi?

        // İzleme geçmişi ve yorumlar
        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();  // Filmle ilgili yorumlar
        public ICollection<WatchList> WatchLists { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Like> Likes { get; set; }



    }
}
