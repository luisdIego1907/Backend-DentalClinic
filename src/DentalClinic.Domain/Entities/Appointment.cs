using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Domain.Entities;

[Table("APPOINTMENT")]
public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int appointment_id { get; private set; }

    [Required]
    public int patient_id { get; set; }

    [Required]
    public int user_id { get; set; }

    [Required]
    public DateOnly appointment_date { get; set; }

    [Required]
    public TimeOnly appointment_time { get; set; }

    [Required]
    public int duration_minutes { get; set; }

    [Required]
    [StringLength(120)]
    public string reason { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string status { get; set; } = "Pendiente";

    [StringLength(255)]
    public string? notes { get; set; }

    public Patient? patient { get; set; }

    public User? user { get; set; }
}
