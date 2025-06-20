using System.ComponentModel.DataAnnotations;
using WifloWatchBackend.Models;

public class PostComment
{
    [Key]
    public int Id { get; set; }

    public int PostId { get; set; }      // Yorumun ait olduğu post
    public int UserId { get; set; }      // Yorumu yazan kullanıcı
    public string CommentText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation Properties
    public Post Post { get; set; }
    public User User { get; set; }
}
