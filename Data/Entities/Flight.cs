using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.ValidationAttributes;

public class Flight
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string From { get; set; }

    [Required]
    public string To { get; set; }

    [Required]
    public DateTime Departure { get; set; }

    [Required]
    [ArrivalAfterDeparture("Departure", ErrorMessage = "Arrival musr be after departure!")]
    public DateTime Arrival { get; set; }

    public User Pilot { get; set; }

    [Required]
    [ForeignKey("PilotId")]
    [Display(Name = "Pilot")]
    public int PilotId { get; set; }

    public Plane Plane { get; set; }

    [Required]
    [ForeignKey("PlaneId")]
    [Display(Name = "Plane")]
    public int PlaneId { get; set; }

    [Required]
    [Display(Name = "Business class price")]
    public double BusinessPrice { get; set; }

    [Required]
    [Display(Name = "Economy class price")]
    public double EconomyPrice { get; set; }

    [Display(Name = "Available business class seats")]
    public int AvailableBusinessSeats { get; set; }

    [Display(Name = "Available economy class seats")]
    public int AvailableEconomySeats { get; set; }
    
    public Flight()
    {
 
    }

    public string GetDurationHours()
    {
        int time = (int)(Arrival - Departure).TotalMinutes;
        int _hours = time / 60;
        int _minutes = time % 60;
        string hours = "";
        string minutes = "";

        switch (_minutes)
        {
            case 0:
                minutes = "";
                break;
            case 1:
                minutes = " 1 minute";
                break;
            default:
                minutes = " " + _minutes + " minutes";
                break;
        }

        switch (_hours)
        {
            case 0:
                hours = "";
                break;
            case 1:
                hours = "1 hour";
                break;
            default:
                hours = _hours + " hours";
                break;
        }

        return hours + minutes;
    }
    public override string ToString() => $"Flight {Id} {From}-{To}, {Departure}";
    public void ReserveBusinessSeat() => AvailableBusinessSeats--;
    public void ReserveEconomySeat() => AvailableEconomySeats--;
}
