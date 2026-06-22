using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Core.Models;

public class Booking
{
    [Required] public int Id { get; set; }
    [Required, StringLength(15)] public string BookingCode { get; set; } = string.Empty;
    [Required] public int PassengerId { get; set; }
    [Required] public Passenger Passenger { get; set; } = null!;
    [Required] public int FlightId { get; set; }
    [Required] public Flight Flight { get; set; } = null!;
    [Required, Range(1, 9)] public int PassengerCount { get; set; }
    [Required, StringLength(20)] public string TripType { get; set; } = "Еднопосочен";
    [Required] public DateTime BookingDate { get; set; }
    [Required, Range(1, 100000)] public decimal TotalPrice { get; set; }
    [Required, StringLength(25)] public string Status { get; set; } = "Потвърдена";
    [Required, StringLength(250)] public string Notes { get; set; } = "Без допълнителни бележки";
}
