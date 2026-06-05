using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DentalClinic.Domain.Entities;

[Table("MEDICAL_RECORD")]
public class MedicalRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("record_id")]
    public int RecordId { get; set; }

    [Required]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Required]
    [Column("created_date")]
    public DateOnly CreatedDate { get; set; }

    [Column("medical_history")]

    public string? MedicalHistory { get; set; }

    [Column("allergies")]
    public string? Allergies { get; set; }

    [Column("general_notes")]
    public string? GeneralNotes { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = String.Empty;

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
}
