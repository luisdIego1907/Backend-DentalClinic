using System.Text.Json.Serialization;
using DentalClinic.Api.Filters;
using DentalClinic.DomainService;
using DentalClinic.Facade;
using DentalClinic.Infrastructure;
using DentalClinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;
using DentalClinic.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
    Seguridad
*/
//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<MessageExceptionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthorizationPolicies.CanSearchUsers, policy =>
        policy.RequireRole(RoleNames.ADMINISTRATOR));

    options.AddPolicy(AuthorizationPolicies.CanManageUsers, policy =>
        policy.RequireRole(RoleNames.ADMINISTRATOR));

    options.AddPolicy(AuthorizationPolicies.CanManagePatients, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST,
            RoleNames.ASSISTANT
        ));

    options.AddPolicy(AuthorizationPolicies.CanManageMedicalRecords, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST
        ));

    options.AddPolicy(AuthorizationPolicies.CanManageAppointments, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST,
            RoleNames.ASSISTANT
        ));

    options.AddPolicy(AuthorizationPolicies.CanManageConsultations, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST
        ));

    options.AddPolicy(AuthorizationPolicies.CanManageDiagnosis, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST
        ));

    options.AddPolicy(AuthorizationPolicies.CanManageTreatment, policy =>
        policy.RequireRole(
            RoleNames.ADMINISTRATOR,
            RoleNames.ODONTOLOGIST
        ));
});

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

// Repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// Facades
builder.Services.AddScoped<IPatientFacade, PatientFacade>();
builder.Services.AddScoped<IUserFacade, UserFacade>();
builder.Services.AddScoped<IAuthorizationFacade, AuthorizationFacade>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("/****************************************/\n\n\n");
string password = "123456";

string hash = BCrypt.Net.BCrypt.HashPassword(password);

Console.WriteLine(hash);

app.Run();
