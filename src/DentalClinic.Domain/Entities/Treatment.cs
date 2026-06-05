
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DentalClinic.Domain.Entities;

[Table("TREATMENT")]
public class Treatment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("treatment_id")]
    public int TreatmentId { get; set; }

    [Required]
    [Column("consultation_id")]
    public int ConsultationId { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("cost")]
    public decimal Cost { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("status")]
    public string Status { get; set; } = string.Empty;

    [Required]
    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [ForeignKey("ConsultationId")]
    public Consultation Consultation { get; set; } = null!;

}