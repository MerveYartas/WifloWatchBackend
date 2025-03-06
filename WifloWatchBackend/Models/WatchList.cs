using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class WatchList
    {
        [Required]  // Liste adı zorunlu
        [StringLength(200)]  // Liste adı maksimum 200 karakter olmalı
        public string Name { get; set; }  // Liste adı

        [Required]  // Kullanıcı ID'si zorunlu
        public int UserId { get; set; }  // Listenin sahibi

        [Required]  // Film ID'si zorunlu
        public int MovieId { get; set; }  // İzlenen film
        public bool IsWatched { get; set; }

        [Required]  // Liste türü zorunlu
        [StringLength(50)]  // Liste türü maksimum 50 karakter olmalı
        public string ListType { get; set; }  // Halen İzlediklerim, Bitirdiklerim vb.

        // İlişkiler
        [ForeignKey("UserId")]  // UserId'yi User ile ilişkilendirir
        public User User { get; set; }  // Kullanıcı

        [ForeignKey("MovieId")]  // MovieId'yi Movie ile ilişkilendirir
        public Movie Movie { get; set; }  // Film
    }
}
