using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StaffDBContext_Code_first.Connection;
using StaffDBContext_Code_first.Model.Configure;
using StaffDBContext_Code_first.Model.DTO;

namespace StaffDBContext_Code_first
{
    public class StaffContext : DbContext
    {
        public StaffContext() : base() { }
        public StaffContext(DbContextOptions options) : base(options) { }

        public DbSet<StaffDbDto> Staff { get; set; }
        public DbSet<PositionDbDto> Positions { get; set; }
        public DbSet<StaffPositionDbDto> StaffPositions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringConfiguration().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<StaffPositionDbDto>()
                .HasKey(staffPosition => new {
                    staffPosition.StaffNumber,
                    staffPosition.PositionId
                });

            modelBuilder.Entity<StaffPositionDbDto>()
                .HasCheckConstraint("CK_StaffPosition_Bet", "Bet >= 0 AND Bet <= 1");

            modelBuilder.ApplyConfiguration(new StaffPositionConfiguration());

        }
    }
}
