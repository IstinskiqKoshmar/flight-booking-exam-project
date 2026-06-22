using FlightBooking.Core.Models;

namespace FlightBooking.Web.ViewModels;

public class HomeViewModel
{
    public IReadOnlyList<Airport> Airports { get; set; } = [];
    public IReadOnlyList<Airline> Airlines { get; set; } = [];
    public IReadOnlyList<Flight> FeaturedFlights { get; set; } = [];
    public int FlightCount { get; set; }
    public int BookingCount { get; set; }
}

public class SearchResultsViewModel
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int Passengers { get; set; }
    public string TripType { get; set; } = "oneway";
    public string Sort { get; set; } = "price";
    public int? AirlineId { get; set; }
    public bool DirectOnly { get; set; }
    public IReadOnlyList<Airline> Airlines { get; set; } = [];
    public IReadOnlyList<Flight> OutboundFlights { get; set; } = [];
    public IReadOnlyList<Flight> ReturnFlights { get; set; } = [];
}

public class BookingRequestViewModel
{
    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public string Nationality { get; set; } = "България";
    public DateTime BirthDate { get; set; } = new(2000, 1, 1);
    public int PassengerCount { get; set; } = 1;
    public string TripType { get; set; } = "Еднопосочен";
    public string Notes { get; set; } = "Без допълнителни бележки";
}
