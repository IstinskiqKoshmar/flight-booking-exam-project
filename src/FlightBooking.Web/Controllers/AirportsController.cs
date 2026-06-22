using FlightBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Web.Controllers;
public class AirportsController(FlightBookingDbContext db):Controller
{
    public async Task<IActionResult> Index()=>View(await db.Airports.OrderBy(x=>x.City).ToListAsync());
}
