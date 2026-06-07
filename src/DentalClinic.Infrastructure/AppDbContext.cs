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

         modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USER");

            entity.HasKey(u => u.user_id);

            entity.Property(u => u.user_id)
                .HasColumnName("user_id");

            entity.Property(u => u.user_resource_id)
                .HasColumnName("user_resource_id");

            entity.Property(u => u.first_name)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.last_name)
                .HasColumnName("last_name")
                .HasMaxLength(80)
                .IsRequired();

            entity.Property(u => u.email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.username)
                .HasColumnName("username")
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(u => u.password_hash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("ROLE");

            entity.HasKey(r => r.Id);

            entity.Property(r => r.Id)
                .HasColumnName("Id");

            entity.Property(r => r.role_resource_id)
                .HasColumnName("RoleResourceId");

            entity.Property(r => r.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(r => r.Name)
                .IsUnique();

            entity.HasIndex(r => r.role_resource_id)
                .IsUnique();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("USER_ROLE");

            entity.HasKey(ur => new { ur.user_id, ur.role_id });

            entity.Property(ur => ur.user_id)
                .HasColumnName("user_id");

            entity.Property(ur => ur.role_id)
                .HasColumnName("RoleId");

            entity.Property(ur => ur.user_role_resource_id)
                .HasColumnName("UserRoleResourceId");

            entity.HasIndex(ur => ur.user_role_resource_id)
                .IsUnique();

            entity.HasOne(ur => ur.user)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.user_id);

            entity.HasOne(ur => ur.role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.role_id);
        });

/*
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.user_id, ur.role_id });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.user)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.user_id);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.role_id);*/
    }
}
