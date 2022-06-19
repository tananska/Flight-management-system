using System;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class FlightManagerDbContext : DbContext
    {
        public FlightManagerDbContext() : base()
        {
        }

        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> contextOptions)
            : base (contextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-H0E2E641\SQLEXPRESS;Database=ManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
        

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Plane> Planes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().HasData(

            new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin123",
                Email = "admin@abv.bg",
                FirstName = "adm",
                LastName = "admin",
                EGN = "0000000000",
                Address = "Bulgaria",
                Phone = "0000000000",
                Role = Role.Admin
            }
                );
        }

    }

}
