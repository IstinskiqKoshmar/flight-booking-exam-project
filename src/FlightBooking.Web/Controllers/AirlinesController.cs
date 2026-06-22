using FlightBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Web.Controllers;
public class AirlinesController(FlightBookingDbContext db):Controller
{
    public async Task<IActionResult> Index()=>View(await db.Airlines.OrderBy(x=>x.Name).ToListAsync());
}
