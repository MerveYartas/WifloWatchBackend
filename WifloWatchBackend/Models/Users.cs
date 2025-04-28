using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using WifloWatchBackend.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(255)]
    public string ProfilePicture { get; set; }

    [MaxLength(20)]
    public string Role { get; set; } = "User"; 


    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<WatchList> WatchLists { get; set; } = new List<WatchList>();
    public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    public virtual ICollection<Comment>? Comments { get; set; } 
    public ICollection<Friendship>? Friendships1 { get; set; }
    public ICollection<Friendship>? Friendships2 { get; set; }
    public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();

}