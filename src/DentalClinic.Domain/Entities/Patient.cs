using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Domain.Entities;

[Table("Patients")]
public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int patient_id {get; private set;}

    [Required]
    [StringLength(20)]
    public string identification{get;  set;} = string.Empty;

    [Required]
    [StringLength(50)]
    public string first_name{get;  set;} = string.Empty; 

    [Required]
    [StringLength(50)]
    public string last_name{get; set;} = string.Empty;

    [Required]
    public DateOnly birth_date{get;  set;}
    
    [Required]
    [StringLength(20)]
    public string phone{get;  set;} = string.Empty;

    [Required]
     [StringLength(100)]
    public string email{get;  set;} = string.Empty;

    [Required]
     [StringLength(150)]
    public string address{get ;  set;} = string.Empty;


    [Required]
    [StringLength(15)]
    public string gender{get ;  set;} = string.Empty;


    [Required]
    public DateTime created_at{get;  set;} 

    [Required]
    public string status{get ;  set;} = "active";
}
