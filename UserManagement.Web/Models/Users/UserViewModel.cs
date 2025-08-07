using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Forename is required.")]
    public string? Forename { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string? Email { get; set; }

    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Date of birth is required.")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public bool NewUserFlag { get; set; }
}
