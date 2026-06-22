using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Core.Models;

public class Airport
{
    [Required] public int Id { get; set; }
    [Required, StringLength(3, MinimumLength = 3)] public string Code { get; set; } = string.Empty;
    [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(70)] public string City { get; set; } = string.Empty;
    [Required, StringLength(70)] public string Country { get; set; } = string.Empty;
    [Required, StringLength(20)] public string Terminal { get; set; } = string.Empty;
    [Required] public bool IsActive { get; set; }
}
