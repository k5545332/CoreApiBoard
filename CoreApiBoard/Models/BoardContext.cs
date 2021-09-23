using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CoreApiBoard.Models
{
    public partial class BoardContext : DbContext
    {
        public BoardContext()
        {
        }

        public BoardContext(DbContextOptions<BoardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<TotalViewNum> TotalViewNums { get; set; }
        public virtual DbSet<User> Users { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<AccessLevel>(entity =>
            {
                entity.ToTable("AccessLevel");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Themeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Theme");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_User");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.PicDetail).HasMaxLength(255);

                entity.Property(e => e.PicUrl).HasMaxLength(255);

                entity.Property(e => e.Sort).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.Eventid)
                    .HasConstraintName("FK_Photo_Event");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("Theme");

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Themes)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_Theme_User");
            });

            modelBuilder.Entity<TotalViewNum>(entity =>
            {
                entity.HasKey(e => e.TotalViewNum1)
                    .HasName("PK_TotalViewNum_1");

                entity.ToTable("TotalViewNum");

                entity.Property(e => e.TotalViewNum1)
                    .ValueGeneratedNever()
                    .HasColumnName("TotalViewNum");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Account).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Enabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Salt).HasMaxLength(255);

                entity.Property(e => e.Tel).HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.Visible).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AccessLevel)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccessLevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_AccessLevel");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
