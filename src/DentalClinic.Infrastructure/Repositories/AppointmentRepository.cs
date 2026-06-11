using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext context;

    public AppointmentRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await context.Appointments
            .Include(a => a.patient)
            .Include(a => a.user)
            .OrderBy(a => a.appointment_date)
            .ThenBy(a => a.appointment_time)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int appointment_id)
    {
        return await context.Appointments
            .Include(a => a.patient)
            .Include(a => a.user)
            .FirstOrDefaultAsync(a => a.appointment_id == appointment_id);
    }

    public async Task<List<Appointment>> GetByDoctorAndDateAsync(
        int user_id,
        DateOnly appointment_date)
    {
        return await context.Appointments
            .Where(a =>
                a.user_id == user_id &&
                a.appointment_date == appointment_date)
            .ToListAsync();
    }

    public async Task<List<User>> GetOdontologistsAsync()
    {
        return await context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.role)
            .Where(u => u.UserRoles.Any(ur => ur.role.Name == "odontologist"))
            .OrderBy(u => u.first_name)
            .ThenBy(u => u.last_name)
            .ToListAsync();
    }

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        await context.Appointments.AddAsync(appointment);
        return appointment;
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        await Task.CompletedTask;
    }

    public Task<List<Patient>> GetPatientsByDoctorAsync(int user_id)
    {
        return context.Appointments
            .Where(a => a.user_id == user_id)
            .Select(a => a.patient!)
            .Distinct()
            .OrderBy(p => p.first_name)
            .ThenBy(p => p.last_name)
            .ToListAsync();
    }
}
