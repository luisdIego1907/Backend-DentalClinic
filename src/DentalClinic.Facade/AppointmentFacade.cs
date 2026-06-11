using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using DentalClinic.Facade.Mappers;
using DentalClinic.Infrastructure;

namespace DentalClinic.Facade;

public class AppointmentFacade : IAppointmentFacade
{
    private readonly IAppointmentService appointmentService;
    private readonly AppDbContext context;

    public AppointmentFacade(
        IAppointmentService appointmentService,
        AppDbContext context)
    {
        this.appointmentService = appointmentService;
        this.context = context;
    }

    public async Task<List<AppointmentDto>> GetAllAsync()
    {
        var entities = await appointmentService.GetAllAsync();

        return AppointmentMapper.ToDto(entities);
    }

    public async Task<AppointmentDto> GetByIdAsync(int appointment_id)
    {
        var entity = await appointmentService.GetByIdAsync(appointment_id);

        if (entity == null)
            throw new ResourceNotFoundException("La cita indicada no existe.");

        return AppointmentMapper.ToDto(entity);
    }

    public async Task<List<DoctorDto>> GetOdontologistsAsync()
    {
        var entities = await appointmentService.GetOdontologistsAsync();

        return AppointmentMapper.ToDoctorDto(entities);
    }

    public async Task<AppointmentDto> AddAsync(SaveAppointmentRequestDto appointment)
    {
        var entity = await appointmentService.AddAsync(appointment);

        await context.SaveChangesAsync();

        var savedEntity = await appointmentService.GetByIdAsync(entity.appointment_id);

        if (savedEntity == null)
            throw new ResourceNotFoundException("La cita registrada no pudo ser consultada.");

        return AppointmentMapper.ToDto(savedEntity);
    }

    public async Task UpdateAsync(int appointment_id, UpdateAppointmentRequestDto appointment)
    {
        await appointmentService.UpdateAsync(appointment_id, appointment);

        await context.SaveChangesAsync();
    }

    public async Task<List<PatientDto>> GetPatientsByDoctorAsync(int user_id)
    {
        var entities = await appointmentService.GetPatientsByDoctorAsync(user_id);

        return PatientMapper.ToDto(entities);
    }
}
