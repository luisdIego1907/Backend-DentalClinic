using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.DomainService;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();

    Task<Appointment?> GetByIdAsync(int appointment_id);

    Task<List<User>> GetOdontologistsAsync();

    Task<Appointment> AddAsync(SaveAppointmentRequestDto appointment);

    Task UpdateAsync(int appointment_id, UpdateAppointmentRequestDto appointment);
}
