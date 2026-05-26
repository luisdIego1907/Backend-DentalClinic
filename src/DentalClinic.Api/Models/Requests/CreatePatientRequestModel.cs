using System;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class CreatePatientRequestModel
{
    [Required]
    [MaxLength(20)]
    public string identification { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string first_name { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string last_name { get; set; } = string.Empty;

    [Required]
    public DateOnly birth_date { get; set; }

    [MaxLength(20)]
    public string phone { get; set; } = string.Empty;

    [MaxLength(100)]
    [EmailAddress]
    public string email { get; set; } = string.Empty;

    [MaxLength(150)]
    public string address { get; set; } = string.Empty;

    [MaxLength(15)]
    public string gender { get; set; } = string.Empty;

   
}
