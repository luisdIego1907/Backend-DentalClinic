using System;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Patient> Patients{get; set;}

    public DbSet<User> Users {get;set;}

    public DbSet<Role> Roles {get;set;}

    public DbSet<UserRole> UserRoles {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.user_id, ur.role_id });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.user)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.user_id);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.role_id);
    }
}
