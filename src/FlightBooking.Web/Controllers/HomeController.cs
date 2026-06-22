using FlightBooking.Data;
using FlightBooking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Web.Controllers;

public class HomeController(FlightBookingDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Airports = await db.Airports.OrderBy(x => x.City).ToListAsync(),
            Airlines = await db.Airlines.OrderBy(x => x.Name).ToListAsync(),
            FeaturedFlights = await db.Flights.Include(x => x.Airline).Include(x => x.DepartureAirport).Include(x => x.ArrivalAirport).OrderBy(x => (double)x.Price).Take(5).ToListAsync(),
            FlightCount = await db.Flights.CountAsync(), BookingCount = await db.Bookings.CountAsync()
        };
        return View(model);
    }

    public async Task<IActionResult> Search(string from, string to, DateTime date, DateTime? returnDate, int passengers = 1, string tripType = "oneway", string sort = "price", int? airlineId = null, bool directOnly = false)
    {
        var query = db.Flights.Include(x => x.Airline).Include(x => x.DepartureAirport).Include(x => x.ArrivalAirport).AsQueryable();
        query = query.Where(x => (x.DepartureAirport.Code == from || x.DepartureAirport.City == from) && (x.ArrivalAirport.Code == to || x.ArrivalAirport.City == to) && x.DepartureTime.Date == date.Date && x.AvailableSeats >= passengers);
        if (airlineId.HasValue) query = query.Where(x => x.AirlineId == airlineId);
        if (directOnly) query = query.Where(x => !x.HasStopover);
        query = sort switch { "duration" => query.OrderBy(x => x.ArrivalTime).ThenByDescending(x => x.DepartureTime), "departure" => query.OrderBy(x => x.DepartureTime), _ => query.OrderBy(x => (double)x.Price) };
        var outbound = await query.ToListAsync();
        var returns = new List<Core.Models.Flight>();
        if (tripType == "roundtrip" && returnDate.HasValue)
        {
            var returnQuery = db.Flights.Include(x => x.Airline).Include(x => x.DepartureAirport).Include(x => x.ArrivalAirport)
                .Where(x => (x.DepartureAirport.Code == to || x.DepartureAirport.City == to) && (x.ArrivalAirport.Code == from || x.ArrivalAirport.City == from) && x.DepartureTime.Date == returnDate.Value.Date && x.AvailableSeats >= passengers);
            if (airlineId.HasValue) returnQuery = returnQuery.Where(x => x.AirlineId == airlineId);
            if (directOnly) returnQuery = returnQuery.Where(x => !x.HasStopover);
            returns = await returnQuery.OrderBy(x => (double)x.Price).ToListAsync();
        }
        return View("SearchResults", new SearchResultsViewModel { From=from, To=to, Date=date, ReturnDate=returnDate, Passengers=passengers, TripType=tripType, Sort=sort, AirlineId=airlineId, DirectOnly=directOnly, Airlines=await db.Airlines.OrderBy(x=>x.Name).ToListAsync(), OutboundFlights=outbound, ReturnFlights=returns });
    }

    public IActionResult Error() => View();
}
