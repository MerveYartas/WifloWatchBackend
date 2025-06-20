namespace WifloWatchBackend.Models
{
    public class PostLikeDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime LikedAt { get; set; }
    }

}
