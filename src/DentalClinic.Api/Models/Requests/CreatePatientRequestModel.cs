using System;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.Api.Models.Requests;

public class CreatePatientRequestModel
{
    [Required]
    [MaxLength(20)]
    public string identification { get; set; }

    [Required]
    [MaxLength(50)]
    public string first_name { get; set; }

    [Required]
    [MaxLength(80)]
    public string last_name { get; set; }

    [Required]
    public DateOnly birth_date { get; set; }

    [MaxLength(20)]
    public string phone { get; set; }

    [MaxLength(100)]
    [EmailAddress]
    public string email { get; set; }

    [MaxLength(150)]
    public string address { get; set; }

    [MaxLength(15)]
    public string gender { get; set; }
}
