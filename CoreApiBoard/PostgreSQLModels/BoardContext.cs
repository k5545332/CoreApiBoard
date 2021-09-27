using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CoreApiBoard.PostgreSQLModels
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
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<TotalViewNum> TotalViewNums { get; set; }
        public virtual DbSet<User> Users { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese (Traditional)_Taiwan.950");

            modelBuilder.Entity<AccessLevel>(entity =>
            {
                entity.ToTable("AccessLevel");

                entity.Property(e => e.AccessLevelid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event");

                entity.Property(e => e.Eventid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Title).IsRequired();

                entity.Property(e => e.UpdateTime).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Themeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_Themeid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_Userid_fkey");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("Theme");

                entity.Property(e => e.Themeid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UpdateTime).HasColumnType("timestamp with time zone");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Themes)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("Theme_Userid_fkey");
            });

            modelBuilder.Entity<TotalViewNum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TotalViewNum");

                entity.Property(e => e.TotalViewNum1).HasColumnName("TotalViewNum");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Userid).UseIdentityAlwaysColumn();

                entity.Property(e => e.Account).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.UpdateTime).HasColumnType("timestamp with time zone");

                entity.Property(e => e.UserName).IsRequired();

                entity.Property(e => e.Visible).HasDefaultValueSql("false");

                entity.HasOne(d => d.AccessLevel)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccessLevelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_AccessLevelid_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
