using System.ComponentModel.DataAnnotations.Schema;

namespace WifloWatchBackend.Models
{
    public class Friendship
    {
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public DateTime FriendshipDate { get; set; }

        [ForeignKey("UserId1")]
        public User User1 { get; set; }

        [ForeignKey("UserId2")]
        public User User2 { get; set; }
    }
}
