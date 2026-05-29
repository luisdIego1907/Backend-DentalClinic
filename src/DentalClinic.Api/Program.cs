using DentalClinic.DomainService;
using DentalClinic.Facade;
using DentalClinic.Infrastructure;
using DentalClinic.Infrastructure.Repositories;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

/*
    Conectar BE con FE
*/
var allowedOrigins = builder.Configuration
 .GetSection("Cors:AllowedOrigins")
 .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOriginsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
     .AllowAnyHeader()
     .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IPatientFacade, PatientFacade>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

/*
    Conectar BE con FE
*/
app.UseCors("AllowedOriginsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
