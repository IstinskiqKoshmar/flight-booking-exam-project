using FlightBooking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Data;

public class FlightBookingDbContext(DbContextOptions<FlightBookingDbContext> options) : DbContext(options)
{
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Airline> Airlines => Set<Airline>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flight>().Property(x => x.Price).HasPrecision(10, 2);
        modelBuilder.Entity<Booking>().Property(x => x.TotalPrice).HasPrecision(10, 2);
        modelBuilder.Entity<Flight>().HasOne(x => x.DepartureAirport).WithMany().HasForeignKey(x => x.DepartureAirportId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Flight>().HasOne(x => x.ArrivalAirport).WithMany().HasForeignKey(x => x.ArrivalAirportId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Flight>().HasOne(x => x.Airline).WithMany().HasForeignKey(x => x.AirlineId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Booking>().HasOne(x => x.Passenger).WithMany().HasForeignKey(x => x.PassengerId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Booking>().HasOne(x => x.Flight).WithMany().HasForeignKey(x => x.FlightId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Airport>().HasIndex(x => x.Code).IsUnique();
        modelBuilder.Entity<Airline>().HasIndex(x => x.IataCode).IsUnique();
        modelBuilder.Entity<Flight>().HasIndex(x => x.FlightNumber);
        modelBuilder.Entity<Passenger>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Booking>().HasIndex(x => x.BookingCode).IsUnique();
        FlightBookingSeed.Configure(modelBuilder);
    }
}
