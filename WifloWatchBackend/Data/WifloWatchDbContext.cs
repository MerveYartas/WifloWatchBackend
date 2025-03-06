using Microsoft.EntityFrameworkCore;
using WifloWatchBackend.Models;

namespace WifloWatchBackend.Data
{
    public class WifloWatchDbContext : DbContext
    {
        public WifloWatchDbContext(DbContextOptions<WifloWatchDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SmartFriendRecommendation> SmartFriendsList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Arkadaşlık ilişkisi
            modelBuilder.Entity<Friendship>()
            .HasKey(f => new { f.UserId1, f.UserId2 });

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany(u => u.Friendships1)
                .HasForeignKey(f => f.UserId1)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany(u => u.Friendships2)
                .HasForeignKey(f => f.UserId2)
                .OnDelete(DeleteBehavior.NoAction);

            // İzleme listesi ilişkisi
            modelBuilder.Entity<WatchList>()
                .HasKey(wl => new { wl.UserId, wl.MovieId });

            modelBuilder.Entity<WatchList>()
                .HasOne(wl => wl.User)
                .WithMany(u => u.WatchLists)
                .HasForeignKey(wl => wl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WatchList>()
                .HasOne(wl => wl.Movie)
                .WithMany(m => m.WatchLists)
                .HasForeignKey(wl => wl.MovieId)
                .OnDelete(DeleteBehavior.NoAction);

            //Mesaş ilişkisi
            modelBuilder.Entity<Message>()
                 .HasOne(m => m.Sender)
                 .WithMany(u => u.SentMessages)
                 .HasForeignKey(m => m.SenderId)
                 .OnDelete(DeleteBehavior.NoAction); // veya DeleteBehavior.ClientSetNull

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction); // veya DeleteBehavior.ClientSetNull

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Recommendations)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Likes)
                .WithOne(l => l.Movie)
                .HasForeignKey(l => l.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Movie)
                .WithMany(m => m.Likes)
                .HasForeignKey(l => l.ContentId);


            modelBuilder.Entity<SmartFriendRecommendation>()
                .HasOne(sfr => sfr.User)
                .WithMany()
                .HasForeignKey(sfr => sfr.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartFriendRecommendation>()
                .HasOne(sfr => sfr.RecommendedUser)
                .WithMany()
                .HasForeignKey(sfr => sfr.RecommendedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SmartFriendRecommendation>()
                .HasIndex(sfr => sfr.UserId);

            modelBuilder.Entity<SmartFriendRecommendation>()
                .HasIndex(sfr => sfr.RecommendedUserId);

            modelBuilder.Entity<Comment>()
            .HasKey(c => new { c.ContentId, c.ContentType });

            modelBuilder.Entity<Comment>()
                .Property(c => c.ContentType)
                .HasConversion<string>();

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.ContentId, l.ContentType });

            modelBuilder.Entity<Like>()
                .Property(l => l.ContentType)
                .HasConversion<string>();
        }
    }
}
