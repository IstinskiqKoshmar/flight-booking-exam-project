using FlightBooking.Core.Models;
using FlightBooking.Data;
using FlightBooking.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Tests;

public class BookingRepositoryTests
{
    private FlightBookingDbContext context = null!;
    private IRepository<Booking> repository = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FlightBookingDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        context = new FlightBookingDbContext(options);
        repository = new EfRepository<Booking>(context);
    }

    private static Booking NewBooking() => new() { BookingCode="TEST-001", PassengerId=1, FlightId=1, PassengerCount=1, TripType="Еднопосочен", BookingDate=DateTime.Now, TotalPrice=100, Status="Потвърдена", Notes="Тест" };

    [Test] public async Task Create_AddsBooking() { var item=await repository.AddAsync(NewBooking()); Assert.That(item.Id,Is.GreaterThan(0)); Assert.That(await context.Bookings.CountAsync(),Is.EqualTo(1)); }
    [Test] public async Task ReadById_ReturnsBooking() { var item=await repository.AddAsync(NewBooking()); var found=await repository.GetByIdAsync(item.Id); Assert.That(found?.BookingCode,Is.EqualTo("TEST-001")); }
    [Test] public async Task ReadAll_ReturnsAllBookings() { await repository.AddAsync(NewBooking()); var second=NewBooking();second.BookingCode="TEST-002";await repository.AddAsync(second); Assert.That((await repository.GetAllAsync()).Count,Is.EqualTo(2)); }
    [Test] public async Task Update_ChangesStatus() { var item=await repository.AddAsync(NewBooking());item.Status="Отменена";await repository.UpdateAsync(item);Assert.That((await repository.GetByIdAsync(item.Id))?.Status,Is.EqualTo("Отменена")); }
    [Test] public async Task Delete_RemovesBooking() { var item=await repository.AddAsync(NewBooking());await repository.DeleteAsync(item.Id);Assert.That(await repository.GetByIdAsync(item.Id),Is.Null); }

    [TearDown] public void Cleanup()=>context.Dispose();
}
