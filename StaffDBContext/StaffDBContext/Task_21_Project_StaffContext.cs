using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StaffDBContext.StaffDBContext
{
    public partial class Task_21_Project_StaffContext : DbContext
    {
        public Task_21_Project_StaffContext()
        {
        }

        public Task_21_Project_StaffContext(DbContextOptions<Task_21_Project_StaffContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<StaffPosition> StaffPositions { get; set; }
        public virtual DbSet<VwPositionFilterByDivision> VwPositionFilterByDivisions { get; set; }
        public virtual DbSet<VwPositionSearchByTitle> VwPositionSearchByTitles { get; set; }
        public virtual DbSet<VwStaffSalary> VwStaffSalaries { get; set; }
        public virtual DbSet<VwStaffSearchByName> VwStaffSearchByNames { get; set; }
        public virtual DbSet<VwTotalWage> VwTotalWages { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("StaffDb_ConnectionString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_100_CI_AI");

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<StaffPosition>(entity =>
            {
                entity.HasKey(e => new { e.StaffNumber, e.PositionId })
                    .HasName("PK__StaffPos__74B7DF3DE13AFE82");

                entity.ToTable("StaffPosition");

                entity.Property(e => e.Bet).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.StaffPositions)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StaffPosi__Posit__2A4B4B5E");

                entity.HasOne(d => d.StaffNumberNavigation)
                    .WithMany(p => p.StaffPositions)
                    .HasForeignKey(d => d.StaffNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StaffPosi__Staff__29572725");
            });

            modelBuilder.Entity<VwPositionFilterByDivision>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPositionFilterByDivision");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VwPositionSearchByTitle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPositionSearchByTitle");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VwStaffSalary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwStaffSalary");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwStaffSearchByName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwStaffSearchByName");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VwTotalWage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTotalWage");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.ServiceNumber)
                    .HasName("PK__Staff__B8FD3A765B34AD04");

                entity.ToTable("Staff");

                entity.Property(e => e.ServiceNumber).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
