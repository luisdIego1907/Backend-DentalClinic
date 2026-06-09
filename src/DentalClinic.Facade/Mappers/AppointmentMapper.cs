using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Facade.Mappers;

public class AppointmentMapper
{
    public static List<AppointmentDto> ToDto(List<Appointment> appointments)
    {
        return appointments.Select(ToDto).ToList();
    }

    public static AppointmentDto ToDto(Appointment appointment)
    {
        return new AppointmentDto
        {
            appointment_id = appointment.appointment_id,
            patient_id = appointment.patient_id,
            doctor_user_resource_id = appointment.user?.user_resource_id ?? Guid.Empty,
            appointment_date = appointment.appointment_date,
            appointment_time = appointment.appointment_time,
            duration_minutes = appointment.duration_minutes,
            reason = appointment.reason,
            status = appointment.status,
            notes = appointment.notes,
            patient = appointment.patient == null
                ? null
                : PatientMapper.ToDto(appointment.patient),
            doctor = appointment.user == null
                ? null
                : ToDoctorDto(appointment.user)
        };
    }

    public static List<DoctorDto> ToDoctorDto(List<User> doctors)
    {
        return doctors.Select(ToDoctorDto).ToList();
    }

    public static DoctorDto ToDoctorDto(User doctor)
    {
        var displayName = $"{doctor.first_name} {doctor.last_name}";

        return new DoctorDto
        {
            user_resource_id = doctor.user_resource_id,
            first_name = doctor.first_name,
            last_name = doctor.last_name,
            username = doctor.username,
            email = doctor.email,
            display_name = displayName
        };
    }
}
