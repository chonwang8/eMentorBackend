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
        public virtual DbSet<Sharing> Sharing { get; set; }
        public virtual DbSet<Subscription> Subcription { get; set; }
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

                entity.Property(e => e.MentorId).HasColumnName("mentorId");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Mentor)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.MentorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Channel_Mentor");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Channel)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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

                entity.Property(e => e.ParentCommendId).HasColumnName("parentCommendId");

                entity.Property(e => e.SharingId).HasColumnName("sharingId");

                entity.HasOne(d => d.Sharing)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.SharingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Sharing");
            });

            modelBuilder.Entity<Enroll>(entity =>
            {
                entity.Property(e => e.EnrollId)
                    .HasColumnName("enrollId")
                    .ValueGeneratedNever();

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enroll_Subcription");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.Property(e => e.MajorId)
                    .HasColumnName("majorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.MajorName)
                    .IsRequired()
                    .HasColumnName("majorName")
                    .HasMaxLength(20);
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mentor_User");
            });

            modelBuilder.Entity<Sharing>(entity =>
            {
                entity.Property(e => e.SharingId)
                    .HasColumnName("sharingId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(150);

                entity.Property(e => e.EndTime)
                    .HasColumnName("endTime")
                    .HasColumnType("date");

                entity.Property(e => e.Maximum).HasColumnName("maximum");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SharingName)
                    .IsRequired()
                    .HasColumnName("sharingName")
                    .HasMaxLength(50);

                entity.Property(e => e.StartTime)
                    .HasColumnName("startTime")
                    .HasColumnType("date");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Sharing)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sharing_Channel");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubcriptionId)
                    .HasColumnName("subcriptionId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MenteeId).HasColumnName("menteeId");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Subcription)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcription_Channel");

                entity.HasOne(d => d.Mentee)
                    .WithMany(p => p.Subcription)
                    .HasForeignKey(d => d.MenteeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcription_Mentee");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId)
                    .HasColumnName("topicId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.MajorId).HasColumnName("majorId");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasColumnName("topicName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Topic)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic_Major");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasColumnName("avatarUrl")
                    .HasMaxLength(50);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(60);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("fullname")
                    .HasMaxLength(60);

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(14);

                entity.Property(e => e.YearOfBirth).HasColumnName("yearOfBirth");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
