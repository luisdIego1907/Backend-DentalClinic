using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllAsync();

    Task<Appointment?> GetByIdAsync(int appointment_id);

    Task<List<Appointment>> GetByDoctorAndDateAsync(
        int user_id,
        DateOnly appointment_date);

    Task<List<User>> GetOdontologistsAsync();

    Task<Appointment> AddAsync(Appointment appointment);

    Task UpdateAsync(Appointment appointment);
}
