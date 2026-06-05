using System;
using System.Net.Http.Headers;
using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

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

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Consultation> Consultations { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
}
