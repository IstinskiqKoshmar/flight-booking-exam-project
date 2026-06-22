using FlightBooking.Core.Models;
using FlightBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Web.Controllers;

public class FlightsController(FlightBookingDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? status) { var q=db.Flights.Include(x=>x.Airline).Include(x=>x.DepartureAirport).Include(x=>x.ArrivalAirport).AsQueryable(); if(!string.IsNullOrWhiteSpace(status)) q=q.Where(x=>x.Status==status); return View(await q.OrderBy(x=>x.DepartureTime).ToListAsync()); }
    public async Task<IActionResult> Details(int id) { var item=await Full().FirstOrDefaultAsync(x=>x.Id==id); return item is null?NotFound():View(item); }
    public async Task<IActionResult> Create() { await Lists(); return View(new Flight { DepartureTime=DateTime.Today.AddDays(7).AddHours(9), ArrivalTime=DateTime.Today.AddDays(7).AddHours(11), Status="По разписание", Gate="A01", AvailableSeats=100 }); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(Flight item) { ModelState.Remove(nameof(item.Airline)); ModelState.Remove(nameof(item.DepartureAirport)); ModelState.Remove(nameof(item.ArrivalAirport)); if(!ModelState.IsValid){await Lists();return View(item);} db.Add(item); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Edit(int id) { var item=await db.Flights.FindAsync(id); if(item is null)return NotFound(); await Lists(); return View(item); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, Flight item) { if(id!=item.Id)return NotFound(); ModelState.Remove(nameof(item.Airline)); ModelState.Remove(nameof(item.DepartureAirport)); ModelState.Remove(nameof(item.ArrivalAirport)); if(!ModelState.IsValid){await Lists();return View(item);} db.Update(item); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Delete(int id) { var item=await Full().FirstOrDefaultAsync(x=>x.Id==id); return item is null?NotFound():View(item); }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id) { var item=await db.Flights.FindAsync(id); if(item is not null){db.Remove(item);await db.SaveChangesAsync();} return RedirectToAction(nameof(Index)); }
    private IQueryable<Flight> Full()=>db.Flights.Include(x=>x.Airline).Include(x=>x.DepartureAirport).Include(x=>x.ArrivalAirport);
    private async Task Lists(){ViewBag.AirlineId=new SelectList(await db.Airlines.ToListAsync(),"Id","Name");ViewBag.DepartureAirportId=new SelectList(await db.Airports.ToListAsync(),"Id","Code");ViewBag.ArrivalAirportId=new SelectList(await db.Airports.ToListAsync(),"Id","Code");}
}
