using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class FitAppEntities : DbContext
    {
        public FitAppEntities()
        {
        }

        public FitAppEntities(DbContextOptions<FitAppEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblActivity> tblActivities { get; set; }
        public virtual DbSet<tblDay> tblDays { get; set; }
        public virtual DbSet<tblDayActivity> TblDayActivities { get; set; }
        public virtual DbSet<tblDayItem> tblDayItems { get; set; }
        public virtual DbSet<tblItem> tblItems { get; set; }
        public virtual DbSet<tblItemType> tblItemTypes { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserAccessLevel> tblUserAccessLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=FitAppDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tblActivity>(entity =>
            {
                entity.ToTable("tblActivity");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblDay>(entity =>
            {
                entity.ToTable("tblDay");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblDays)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblDay_UserId");
            });

            modelBuilder.Entity<tblDayActivity>(entity =>
            {
                entity.ToTable("tblDayActivity");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.TblDayActivities)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblDayActivity_ActivityId");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.TblDayActivities)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblDayActivity_DayId");
            });

            modelBuilder.Entity<tblDayItem>(entity =>
            {
                entity.ToTable("tblDayItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.TblDayItems)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblDayItem_DayId");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TblDayItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblDayItem_ItemId");
            });

            modelBuilder.Entity<tblItem>(entity =>
            {
                entity.ToTable("tblItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TblItems)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblItem_TypeId");
            });

            modelBuilder.Entity<tblItemType>(entity =>
            {
                entity.ToTable("tblItemType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.HasIndex(e => e.Username, "UQ__tblUser__536C85E4BBBF95E5")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DaysInArowSucceeded).HasColumnName("DaysInARowSucceeded");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserAccessLevel)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserAccessLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblUser_UserAccessLevelId");
            });

            modelBuilder.Entity<tblUserAccessLevel>(entity =>
            {
                entity.ToTable("tblUserAccessLevel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
