namespace WifloWatchBackend.Models
{
    public class MessageDTO
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
    }

}
