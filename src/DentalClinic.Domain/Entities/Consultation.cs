using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Domain.Entities;

[Table("CONSULTATION")]
public class Consultation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("consultation_id")]
    public int ConsultationId { get; set; }

    [Required]
    [Column("record_id")]
    public int RecordId { get; set; }

    [Column("appointment_id")]
    public int? AppointmentId { get; set; }

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("consultation_date")]
    public DateOnly ConsultationDate { get; set; }

    [Required]
    [MaxLength(120)]
    [Column("reason")]
    public string Reason { get; set; } = string.Empty;

    [Column("observations")]
    public string? Observations { get; set; }

    [Column("odontogram")]
    public string? Odontogram { get; set; }

    [ForeignKey("RecordId")]
    public MedicalRecord MedicalRecord { get; set; } = null!;

    [ForeignKey("AppointmentId")]
    public Appointment? Appointment { get; set; }

    [ForeignKey("UserId")]
#pragma warning disable CS9035
    public User User { get; set; } = null!;
#pragma warning restore CS9035

    public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
}