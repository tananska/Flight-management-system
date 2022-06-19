using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.ValidationAttributes;

public class Reservation
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Middle name")]
    public string MiddleName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [EGN(ErrorMessage = "EGN must be exactly 10 digits")]
    public string EGN { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Nationality { get; set; }

    public Flight Flight { get; set; }

    [Required]
    [ForeignKey("FlightId")]
    [Display(Name = "Flight")]
    public int FlightId { get; set; }

    [Required]
    [Display(Name = "Ticket type")]
    public TicketType TicketType { get; set; }

    public Reservation()
    {
    }

    public void ReserveSeat()
    {
        if (TicketType == TicketType.Business)
        {
            Flight.ReserveBusinessSeat();
        }
        else
        {
            Flight.ReserveEconomySeat();
        }
    }

    public string Price()
    {
        if (TicketType == TicketType.Business)
        {
            return Flight.BusinessPrice.ToString() + " lv.";
        }
        return Flight.EconomyPrice.ToString() + " lv.";
    }
}
