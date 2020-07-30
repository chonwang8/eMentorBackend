using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public partial class eMentorContext : DbContext
    {
        public eMentorContext()
        {
        }

        public eMentorContext(DbContextOptions<eMentorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Enroll> Enroll { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Mentee> Mentee { get; set; }
        public virtual DbSet<Mentor> Mentor { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Sharing> Sharing { get; set; }
        public virtual DbSet<Subscription> Subscription { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=eMentor;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasIndex(e => e.AdminUsername)
                    .HasName("Unique_Admin_Email")
                    .IsUnique();

                entity.Property(e => e.AdminId)
                    .HasColumnName("adminId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AdminUsername)
                    .IsRequired()
                    .HasColumnName("adminUsername")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelId")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MentorId).HasColumnName("mentorId");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.MentorId)
                    .HasConstraintName("FK_Channel_Mentor");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK_Channel_Topic");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasColumnName("commentContent")
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.ParentCommendId).HasColumnName("parentCommendId");

                entity.Property(e => e.SharingId).HasColumnName("sharingId");

                entity.HasOne(d => d.Sharing)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.SharingId)
                    .HasConstraintName("FK_Comment_Sharing");
            });

            modelBuilder.Entity<Enroll>(entity =>
            {
                entity.Property(e => e.EnrollId)
                    .HasColumnName("enrollId")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.SharingId).HasColumnName("sharingId");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");

                entity.HasOne(d => d.Sharing)
                    .WithMany(p => p.Enroll)
                    .HasForeignKey(d => d.SharingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enroll_Sharing");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Enroll)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("FK_Enroll_Subcription");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.MajorId)
                    .HasColumnName("majorId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MajorName)
                    .IsRequired()
                    .HasColumnName("majorName");
            });

            modelBuilder.Entity<Mentee>(entity =>
            {
                entity.Property(e => e.MenteeId)
                    .HasColumnName("menteeId")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mentee)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Mentee_User");
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.Property(e => e.MentorId)
                    .HasColumnName("mentorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mentor)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Mentor_User");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.MentorId);

                entity.Property(e => e.MentorId)
                    .HasColumnName("mentorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.RatingCount).HasColumnName("ratingCount");

                entity.Property(e => e.RatingScore).HasColumnName("ratingScore");

                entity.HasOne(d => d.Mentor)
                    .WithOne(p => p.Rating)
                    .HasForeignKey<Rating>(d => d.MentorId)
                    .HasConstraintName("FK_Rating_Mentor");
            });

            modelBuilder.Entity<Sharing>(entity =>
            {
                entity.Property(e => e.SharingId)
                    .HasColumnName("sharingId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApprovedTime).HasColumnType("datetime");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.EndTime)
                    .HasColumnName("endTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("imageUrl")
                    .IsUnicode(false);

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Maximum).HasColumnName("maximum");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SharingName)
                    .IsRequired()
                    .HasColumnName("sharingName")
                    .HasMaxLength(50);

                entity.Property(e => e.StartTime)
                    .HasColumnName("startTime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Sharing)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_Sharing_Channel");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscriptionId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MenteeId).HasColumnName("menteeId");

                entity.Property(e => e.TimeSubscripted)
                    .HasColumnName("timeSubscripted")
                    .HasColumnType("date");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscription_Channel");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.Subscription)
                    .HasForeignKey(d => d.MenteeId)
                    .HasConstraintName("FK_Subcription_Mentee");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId)
                    .HasColumnName("topicId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MajorId).HasColumnName("majorId");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasColumnName("topicName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK_Topic_Major");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AvatarUrl).HasColumnName("avatarUrl");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Fullname).HasColumnName("fullname");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(80);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.YearOfBirth).HasColumnName("yearOfBirth");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
