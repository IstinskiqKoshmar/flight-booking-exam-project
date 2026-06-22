using FlightBooking.Core.Models;
using FlightBooking.Data;
using FlightBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Web.Controllers;

public class BookingsController(FlightBookingDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? email)
    {
        var q=Full(); if(!string.IsNullOrWhiteSpace(email)) q=q.Where(x=>x.Passenger.Email==email);
        ViewBag.Email=email; return View(await q.OrderByDescending(x=>x.BookingDate).ToListAsync());
    }
    public async Task<IActionResult> Details(int id){var item=await Full().FirstOrDefaultAsync(x=>x.Id==id);return item is null?NotFound():View(item);}
    public async Task<IActionResult> Book(int flightId){var f=await db.Flights.Include(x=>x.Airline).Include(x=>x.DepartureAirport).Include(x=>x.ArrivalAirport).FirstOrDefaultAsync(x=>x.Id==flightId);return f is null?NotFound():View(new BookingRequestViewModel{FlightId=f.Id,Flight=f});}
    [HttpPost,ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(BookingRequestViewModel model)
    {
        var flight=await db.Flights.Include(x=>x.Airline).Include(x=>x.DepartureAirport).Include(x=>x.ArrivalAirport).FirstOrDefaultAsync(x=>x.Id==model.FlightId);
        if(flight is null)return NotFound(); model.Flight=flight;
        if(string.IsNullOrWhiteSpace(model.FullName)||string.IsNullOrWhiteSpace(model.Email)||string.IsNullOrWhiteSpace(model.PassportNumber)||model.PassengerCount<1){ModelState.AddModelError(string.Empty,"Попълнете всички задължителни данни.");return View(model);}
        if(flight.AvailableSeats<model.PassengerCount){ModelState.AddModelError(string.Empty,"Няма достатъчно свободни места.");return View(model);}
        var passenger=await db.Passengers.FirstOrDefaultAsync(x=>x.Email==model.Email);
        if(passenger is null){passenger=new Passenger{FullName=model.FullName,Email=model.Email,Phone=model.Phone,PassportNumber=model.PassportNumber,Nationality=model.Nationality,BirthDate=model.BirthDate,CreatedAt=DateTime.Now};db.Passengers.Add(passenger);await db.SaveChangesAsync();}
        var booking=new Booking{BookingCode=$"SKY-{DateTime.Now:MMddHHmm}",PassengerId=passenger.Id,FlightId=flight.Id,PassengerCount=model.PassengerCount,TripType=model.TripType,BookingDate=DateTime.Now,TotalPrice=flight.Price*model.PassengerCount,Status="Потвърдена",Notes=string.IsNullOrWhiteSpace(model.Notes)?"Без допълнителни бележки":model.Notes};
        flight.AvailableSeats-=model.PassengerCount;db.Bookings.Add(booking);await db.SaveChangesAsync();return RedirectToAction(nameof(Confirmation),new{id=booking.Id});
    }
    public async Task<IActionResult> Confirmation(int id){var item=await Full().FirstOrDefaultAsync(x=>x.Id==id);return item is null?NotFound():View(item);}
    public async Task<IActionResult> Create(){await Lists();return View(new Booking{BookingCode=$"SKY-{DateTime.Now:MMddHHmm}",BookingDate=DateTime.Now,PassengerCount=1,TripType="Еднопосочен",Status="Потвърдена",Notes="Без допълнителни бележки"});}
    [HttpPost,ValidateAntiForgeryToken]public async Task<IActionResult> Create(Booking item){ModelState.Remove(nameof(item.Passenger));ModelState.Remove(nameof(item.Flight));var flight=await db.Flights.FindAsync(item.FlightId);if(flight is not null)item.TotalPrice=flight.Price*item.PassengerCount;if(!ModelState.IsValid){await Lists();return View(item);}db.Add(item);await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var item=await db.Bookings.FindAsync(id);if(item is null)return NotFound();await Lists();return View(item);}
    [HttpPost,ValidateAntiForgeryToken]public async Task<IActionResult> Edit(int id,Booking item){if(id!=item.Id)return NotFound();ModelState.Remove(nameof(item.Passenger));ModelState.Remove(nameof(item.Flight));if(!ModelState.IsValid){await Lists();return View(item);}db.Update(item);await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Delete(int id){var item=await Full().FirstOrDefaultAsync(x=>x.Id==id);return item is null?NotFound():View(item);}
    [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken]public async Task<IActionResult> DeleteConfirmed(int id){var item=await db.Bookings.FindAsync(id);if(item is not null){db.Remove(item);await db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
    private IQueryable<Booking> Full()=>db.Bookings.Include(x=>x.Passenger).Include(x=>x.Flight).ThenInclude(x=>x.Airline).Include(x=>x.Flight).ThenInclude(x=>x.DepartureAirport).Include(x=>x.Flight).ThenInclude(x=>x.ArrivalAirport);
    private async Task Lists(){ViewBag.PassengerId=new SelectList(await db.Passengers.ToListAsync(),"Id","FullName");ViewBag.FlightId=new SelectList(await db.Flights.ToListAsync(),"Id","FlightNumber");}
}
