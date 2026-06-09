using DentalClinic.Dto;

namespace DentalClinic.Facade;

public interface IAppointmentFacade
{
    Task<List<AppointmentDto>> GetAllAsync();

    Task<AppointmentDto> GetByIdAsync(int appointment_id);

    Task<List<DoctorDto>> GetOdontologistsAsync();

    Task<AppointmentDto> AddAsync(SaveAppointmentRequestDto appointment);

    Task UpdateAsync(int appointment_id, UpdateAppointmentRequestDto appointment);
}
