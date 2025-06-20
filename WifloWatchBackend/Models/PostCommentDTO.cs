namespace WifloWatchBackend.Models
{
    public class PostCommentDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
    }

}
