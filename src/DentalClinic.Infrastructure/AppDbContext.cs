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
}
