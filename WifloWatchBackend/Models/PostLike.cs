using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WifloWatchBackend.Models;

public class PostLike
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int PostId { get; set; }
    [Required]
    public int UserId { get; set; }
    public DateTime LikedAt { get; set; } = DateTime.Now;

    // Navigation Properties


    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
