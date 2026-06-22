using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Core.Models;

public class Airline
{
    [Required] public int Id { get; set; }
    [Required, StringLength(80)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(3)] public string IataCode { get; set; } = string.Empty;
    [Required, StringLength(70)] public string Country { get; set; } = string.Empty;
    [Required, StringLength(30)] public string Phone { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Website { get; set; } = string.Empty;
    [Required] public bool IsActive { get; set; }
}
