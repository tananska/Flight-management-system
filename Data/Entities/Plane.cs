using System;
using System.ComponentModel.DataAnnotations;
using Data.ValidationAttributes;

public class Plane
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Plane type")]
    public PlaneType PlaneType { get; set; }

    [Required]
    [Display(Name = "Passenger capacity")]
    [Range(50, 300, ErrorMessage = "Passenger capacity must be between 50 and 300 people")]
    public int PassengerCapacity { get; set; }

    [Required]
    [Display(Name = "Business class capacity")]
    [Range(10, 150, ErrorMessage = "Business class capacity must be between 10 and 150 people")]
    [CapacityLessThan("PassengerCapacity", ErrorMessage = "Business class capacity must be less than passenger capacity")]
    public int BusinessCapacity { get; set; }

    public Plane()
    {
    }
}
