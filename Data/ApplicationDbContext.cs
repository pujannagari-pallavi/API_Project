using API_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TourPackage)
                .WithMany(tp => tp.Bookings)
                .HasForeignKey(b => b.TourPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .HasDefaultValue("Pending");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TourPackage>()
                .Property(tp => tp.Price)
                .HasPrecision(18, 2);

           
            // Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "Customer" }
            );

            // Users (✅ use hashed passwords here!)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    Email = "admin@test.com",
                    PasswordHash = "Admin@123",
                    RoleId = 1
                },
                new User
                {
                    UserId = 2,
                    UserName = "john",
                    Email = "john@test.com",
                    PasswordHash = "John@123",
                    RoleId = 2
                },
                new User
                {
                    UserId = 3,
                    UserName = "pallavi",
                    Email = "pallavi@test.com",
                    PasswordHash = "pallavi@123",
                    RoleId = 2
                }
            );

            // TourPackages
            modelBuilder.Entity<TourPackage>().HasData(
                new TourPackage { TourPackageId = 1, PackName = "Beach Paradise", Location = "Goa", Price = 1500, Days = 3 },
                new TourPackage { TourPackageId = 2, PackName = "Mountain Adventure", Location = "Manali", Price = 2500, Days = 5 },
                new TourPackage { TourPackageId = 3, PackName = "Heritage Tour", Location = "Jaipur", Price = 2000, Days = 4 }
            );

            // Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    BookingDate = new DateTime(2025, 8, 20),
                    NumberOfPersons = 2,
                    TourPackageId = 1,
                    UserId = 2
                },
                new Booking
                {
                    BookingId = 2,
                    BookingDate = new DateTime(2025, 8, 19),
                    NumberOfPersons = 4,
                    TourPackageId = 2,
                    UserId = 2
                }
            );

            // Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = 1,
                    Amount = 3000,
                    PaidOn = new DateTime(2025, 8, 21),
                    PaymentMethod = "Credit Card",
                    Status = "Completed",
                    BookingId = 1
                },
                new Payment
                {
                    PaymentId = 2,
                    Amount = 10000,
                    PaidOn = new DateTime(2025, 8, 20),
                    PaymentMethod = "UPI",
                    Status = "Pending",
                    BookingId = 2
                }
            );
        }
    }


    }

