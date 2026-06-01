using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Domain.Entities;

[Table("USER_ROLE")]

[Index(nameof(user_role_resource_id), IsUnique = true)]
public class UserRole
{
   
   public int user_id {get;private set;}

   [Column("RoleId")]
   public int role_id {get; private set;}

   [Column("UserRoleResourceId")]
   public Guid user_role_resource_id {get; set;} = Guid.NewGuid();

   public required User user {get;set;}

   public required Role role {get;set;}
}
