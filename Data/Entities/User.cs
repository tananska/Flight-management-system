using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.ValidationAttributes;

public class User 
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(4)]
    public string Username { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [EGN(ErrorMessage = "EGN must be exactly 10 digits")]
    public string EGN { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    public Role Role { get; set; }

    public User()
    {
    }

    public string Name() => FirstName + " " + LastName;
}
