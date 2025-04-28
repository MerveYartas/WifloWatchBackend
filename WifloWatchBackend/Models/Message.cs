using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Message
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int SenderId { get; set; }

    [Required]
    public int ReceiverId { get; set; }

    [Required]
    [StringLength(1000)]
    public string MessageText { get; set; }

    [Required]
    public bool IsRead { get; set; }

    [Required]
    public DateTime SentAt { get; set; }

    // İlişkiler
    [ForeignKey("SenderId")]
    public virtual User Sender { get; set; }

    [ForeignKey("ReceiverId")]
    public virtual User Receiver { get; set; }
}

