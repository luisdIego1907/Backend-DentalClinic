
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Domain.Entities;

[Table("APPOINTMENT")]
public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("appointment_id")]
    public int AppointmentId { get; set; }

    [Required]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("appointment_datetime")]
    public DateTime AppointmentDateTime { get; set; }

    [Required]
    [MaxLength(120)]
    [Column("reason")]
    public string Reason { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; }

    [MaxLength(255)]
    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
}