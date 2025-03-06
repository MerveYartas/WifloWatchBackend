using Microsoft.Extensions.Hosting;

namespace WifloWatchBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // Admin, User gibi
        public DateTime RegisteredAt { get; set; }

        // İlişkiler
        public ICollection<WatchList> WatchLists { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Following { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}
