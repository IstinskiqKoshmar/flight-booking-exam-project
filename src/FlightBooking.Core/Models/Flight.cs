using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Core.Models;

public class Flight
{
    [Required] public int Id { get; set; }
    [Required, StringLength(12)] public string FlightNumber { get; set; } = string.Empty;
    [Required] public int AirlineId { get; set; }
    [Required] public Airline Airline { get; set; } = null!;
    [Required] public int DepartureAirportId { get; set; }
    [Required] public Airport DepartureAirport { get; set; } = null!;
    [Required] public int ArrivalAirportId { get; set; }
    [Required] public Airport ArrivalAirport { get; set; } = null!;
    [Required] public DateTime DepartureTime { get; set; }
    [Required] public DateTime ArrivalTime { get; set; }
    [Required, Range(1, 10000)] public decimal Price { get; set; }
    [Required, Range(0, 500)] public int AvailableSeats { get; set; }
    [Required] public bool HasStopover { get; set; }
    [Required, StringLength(25)] public string Status { get; set; } = "Навреме";
    [Required, StringLength(10)] public string Gate { get; set; } = string.Empty;
    public TimeSpan Duration => ArrivalTime - DepartureTime;
}
