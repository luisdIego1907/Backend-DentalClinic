
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DentalClinic.Domain.Entities;

[Table("DIAGNOSIS")]
public class Diagnosis
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("diagnosis_id")]
    public int DiagnosisId { get; set; }

    [Required]
    [Column("consultation_id")]
    public int ConsultationId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("diagnosis_date")]
    public DateOnly DiagnosisDate { get; set; }

    [ForeignKey("ConsultationId")]
    public Consultation Consultation { get; set; } = null!;
}