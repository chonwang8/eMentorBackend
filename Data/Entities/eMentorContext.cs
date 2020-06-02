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

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Majors> Majors { get; set; }
        public virtual DbSet<Sharings> Sharings { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<UserChannels> UserChannels { get; set; }
        public virtual DbSet<UserSharings> UserSharings { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\;Database=eMentor;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId)
                    .HasName("PK_tblAdmin");

                entity.HasIndex(e => e.Email)
                    .HasName("Unique_Admin_Email")
                    .IsUnique();

                entity.Property(e => e.AdminId)
                    .HasColumnName("adminId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Channels>(entity =>
            {
                entity.HasKey(e => e.ChannelId)
                    .HasName("PK_Channel");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_Comment");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasColumnName("commentContent")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.UserSharingId).HasColumnName("userSharingId");

                entity.HasOne(d => d.UserSharing)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserSharingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_UserSharing");
            });

            modelBuilder.Entity<Majors>(entity =>
            {
                entity.HasKey(e => e.MajorId)
                    .HasName("PK_Major");

                entity.Property(e => e.MajorId)
                    .HasColumnName("majorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MajorName)
                    .IsRequired()
                    .HasColumnName("majorName")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Sharings>(entity =>
            {
                entity.HasKey(e => e.SharingId)
                    .HasName("PK_Sharing");

                entity.Property(e => e.SharingId)
                    .HasColumnName("sharingId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.EndTime)
                    .HasColumnName("endTime")
                    .HasColumnType("date");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.SharingName)
                    .IsRequired()
                    .HasColumnName("sharingName")
                    .HasMaxLength(50);

                entity.Property(e => e.StartTime)
                    .HasColumnName("startTime")
                    .HasColumnType("date");

                entity.Property(e => e.TopicId).HasColumnName("topicId");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Sharings)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sharing_Topic");
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.HasKey(e => e.TopicId)
                    .HasName("PK_Topic");

                entity.Property(e => e.TopicId)
                    .HasColumnName("topicId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.MajorId).HasColumnName("majorId");

                entity.Property(e => e.TopicDescription)
                    .IsRequired()
                    .HasColumnName("topicDescription")
                    .HasMaxLength(200);

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasColumnName("topicName")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic_Major");
            });

            modelBuilder.Entity<UserChannels>(entity =>
            {
                entity.HasKey(e => e.UserChannelId)
                    .HasName("PK_UserChannel");

                entity.Property(e => e.UserChannelId)
                    .HasColumnName("userChannelId")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChannelId).HasColumnName("channelId");

                entity.Property(e => e.IsSubcriber).HasColumnName("isSubcriber");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.UserChannels)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChannel_Channel");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserChannels)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChannel_User");
            });

            modelBuilder.Entity<UserSharings>(entity =>
            {
                entity.HasKey(e => e.UserSharingId)
                    .HasName("PK_UserSharing");

                entity.Property(e => e.UserSharingId)
                    .HasColumnName("userSharingId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IsAccepted).HasColumnName("isAccepted");

                entity.Property(e => e.IsAttended).HasColumnName("isAttended");

                entity.Property(e => e.SharingId).HasColumnName("sharingId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Sharing)
                    .WithMany(p => p.UserSharings)
                    .HasForeignKey(d => d.SharingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSharing_Sharing");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSharings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSharing_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatarUrl")
                    .HasMaxLength(100);

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasColumnName("fullname")
                    .HasMaxLength(50);

                entity.Property(e => e.IsDisable).HasColumnName("isDisable");

                entity.Property(e => e.IsMentor).HasColumnName("isMentor");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(14);

                entity.Property(e => e.YearOfBirth).HasColumnName("yearOfBirth");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
