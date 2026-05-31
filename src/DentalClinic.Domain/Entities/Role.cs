using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Domain.Entities;

[Table("ROLE")]
[Index(nameof(Name), IsUnique = true)]
[Index(nameof(role_resource_id), IsUnique = true)]
public class Role
{
    [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; }

   [Column("RoleResourceId")]
   public Guid role_resource_id { get; set; } = Guid.NewGuid();
   
   [MaxLength(100)]
   [Required]
   public required string Name { get; set; }
   public List<UserRole> UserRoles { get; set; } = [];
}
