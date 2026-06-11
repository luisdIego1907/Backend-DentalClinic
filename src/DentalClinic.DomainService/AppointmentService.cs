using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly IPatientRepository patientRepository;
    private readonly IUserRepository userRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IUserRepository userRepository)
    {
        this.appointmentRepository = appointmentRepository;
        this.patientRepository = patientRepository;
        this.userRepository = userRepository;
    }

    public Task<List<Appointment>> GetAllAsync()
    {
        return appointmentRepository.GetAllAsync();
    }

    public Task<Appointment?> GetByIdAsync(int appointment_id)
    {
        return appointmentRepository.GetByIdAsync(appointment_id);
    }

    public Task<List<User>> GetOdontologistsAsync()
    {
        return appointmentRepository.GetOdontologistsAsync();
    }

    public async Task<Appointment> AddAsync(SaveAppointmentRequestDto appointment)
    {
        await ValidateAppointmentAsync(appointment);

        var doctor = await GetValidDoctorAsync(appointment.doctor_user_resource_id);

        var entity = new Appointment
        {
            patient_id = appointment.patient_id,
            user_id = doctor.user_id,
            appointment_date = appointment.appointment_date,
            appointment_time = appointment.appointment_time,
            duration_minutes = appointment.duration_minutes,
            reason = appointment.reason.Trim(),
            status = string.IsNullOrWhiteSpace(appointment.status)
                ? "Pendiente"
                : appointment.status.Trim(),
            notes = appointment.notes
        };

        await EnsureScheduleIsAvailableAsync(entity, null);

        return await appointmentRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(int appointment_id, UpdateAppointmentRequestDto appointment)
    {
        await ValidateAppointmentAsync(appointment);

        var existingAppointment = await appointmentRepository.GetByIdAsync(appointment_id);

        if (existingAppointment == null)
            throw new ResourceNotFoundException("La cita indicada no existe.");

        var doctor = await GetValidDoctorAsync(appointment.doctor_user_resource_id);

        existingAppointment.patient_id = appointment.patient_id;
        existingAppointment.user_id = doctor.user_id;
        existingAppointment.appointment_date = appointment.appointment_date;
        existingAppointment.appointment_time = appointment.appointment_time;
        existingAppointment.duration_minutes = appointment.duration_minutes;
        existingAppointment.reason = appointment.reason.Trim();
        existingAppointment.status = string.IsNullOrWhiteSpace(appointment.status)
            ? existingAppointment.status
            : appointment.status.Trim();
        existingAppointment.notes = appointment.notes;

        await EnsureScheduleIsAvailableAsync(existingAppointment, appointment_id);

        await appointmentRepository.UpdateAsync(existingAppointment);
    }

    private async Task ValidateAppointmentAsync(SaveAppointmentRequestDto appointment)
    {
        if (appointment.patient_id <= 0)
            throw new BadRequestResponseException("El paciente es obligatorio.");

        if (appointment.doctor_user_resource_id == Guid.Empty)
            throw new BadRequestResponseException("El odontólogo es obligatorio.");

        if (appointment.duration_minutes <= 0)
            throw new BadRequestResponseException("La duración de la cita debe ser mayor a cero.");

        if (string.IsNullOrWhiteSpace(appointment.reason))
            throw new BadRequestResponseException("El motivo de la cita es obligatorio.");

        var patient = await patientRepository.GetByIdAsync(appointment.patient_id);

        if (patient == null)
            throw new ResourceNotFoundException("El paciente asociado no existe.");

        if (patient.status.Equals("inactive", StringComparison.OrdinalIgnoreCase) ||
            patient.status.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
            throw new BadRequestResponseException("No se puede registrar una cita para un paciente inactivo.");
    }

    private async Task<User> GetValidDoctorAsync(Guid doctor_user_resource_id)
    {
        var doctor = await userRepository.GetByResourceIdAsync(doctor_user_resource_id);

        if (doctor == null)
            throw new ResourceNotFoundException("El odontólogo asociado no existe.");

        var isOdontologist = doctor.UserRoles.Any(ur =>
            ur.role.Name == RoleNames.ODONTOLOGIST);

        if (!isOdontologist)
            throw new BadRequestResponseException("El usuario seleccionado no es odontólogo.");

        return doctor;
    }

    private async Task EnsureScheduleIsAvailableAsync(
        Appointment appointment,
        int? ignoredAppointmentId)
    {
        var appointments = await appointmentRepository.GetByDoctorAndDateAsync(
            appointment.user_id,
            appointment.appointment_date);

        var newStart = ToMinutes(appointment.appointment_time);
        var newEnd = newStart + appointment.duration_minutes;

        var hasConflict = appointments.Any(existingAppointment =>
        {
            if (ignoredAppointmentId.HasValue &&
                existingAppointment.appointment_id == ignoredAppointmentId.Value)
                return false;

            var existingStart = ToMinutes(existingAppointment.appointment_time);
            var existingEnd = existingStart + existingAppointment.duration_minutes;

            return newStart < existingEnd && newEnd > existingStart;
        });

        if (hasConflict)
            throw new BadRequestResponseException(
                "Ya existe una cita registrada para ese odontólogo en ese horario.");
    }

    private static int ToMinutes(TimeOnly time)
    {
        return (time.Hour * 60) + time.Minute;
    }

    public Task<List<Patient>> GetPatientsByDoctorAsync(int user_id)
    {
        return appointmentRepository.GetPatientsByDoctorAsync(user_id);
    }
}
