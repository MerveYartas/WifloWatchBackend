﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<PostComment> PostComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PostComment>()
                .HasOne(pc => pc.User)
                .WithMany()
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostComment>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostComments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Cascade); // sadece birine Cascade uygula
            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany()
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Restrict); // veya DeleteBehavior.NoAction

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Bu kalabilir


            // Post ilişkisi
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            // Arkadaşlık ilişkisi
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

            // WatchList (tek liste yaklaşımı)
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

            // Mesajlaşma
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            // Öneriler
            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recommendations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Recommendations)
                .HasForeignKey(r => r.MovieId);

            // Beğeniler
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Likes)
                .WithOne(l => l.Movie)
                .HasForeignKey(l => l.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.ContentId, l.ContentType });

            modelBuilder.Entity<Like>()
                .Property(l => l.ContentType)
                .HasConversion<string>();

            // Yorumlar
            modelBuilder.Entity<Comment>()
                .HasKey(c => new { c.ContentId, c.ContentType });

            modelBuilder.Entity<Comment>()
                .Property(c => c.ContentType)
                .HasConversion<string>();

            // Akıllı arkadaş önerileri
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

            // İzleme geçmişi
            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchHistories)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Movie)
                .WithMany(m => m.WatchHistories)
                .HasForeignKey(w => w.MovieId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
