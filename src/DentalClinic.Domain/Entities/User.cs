using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalClinic.Domain.Entities;

[Table("USER")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int user_id { get; private set; }

    [Required]
    public Guid user_resource_id { get; set; }

    [StringLength(50)]
    [Required]
    public string first_name { get; set; }

    [StringLength(50)]
    [Required]
    public string last_name { get; set; }

    [StringLength(100)]
    [Required]
    public string email { get; set; }

    [StringLength(50)]
    [Required]
    public string username { get; set; }

    [StringLength(255)]
    [Required]
    public string password_hash { get; set; } = string.Empty;

    public List<UserRole> UserRoles { get; set; } = [];

    public void ClearRoles()
    {
        UserRoles.Clear();
    }
}
