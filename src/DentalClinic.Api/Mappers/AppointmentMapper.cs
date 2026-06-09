using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Models.Responses;
using DentalClinic.Dto;
using DentalClinic.Exceptions;

namespace DentalClinic.Api.Mappers;

public class AppointmentMapper
{
    public static SaveAppointmentRequestDto ToDto(SaveAppointmentRequestModel model)
    {
        if (!DateOnly.TryParse(model.appointment_date, out var appointmentDate))
            throw new BadRequestResponseException("La fecha de la cita no tiene un formato válido.");

        if (!TimeOnly.TryParse(model.appointment_time, out var appointmentTime))
            throw new BadRequestResponseException("La hora de la cita no tiene un formato válido.");

        return new SaveAppointmentRequestDto
        {
            patient_id = model.patient_id,
            doctor_user_resource_id = model.doctor_user_resource_id,
            appointment_date = appointmentDate,
            appointment_time = appointmentTime,
            duration_minutes = model.duration_minutes,
            reason = model.reason,
            status = model.status,
            notes = model.notes
        };
    }



    public static UpdateAppointmentRequestDto ToDto(UpdateAppointmentRequestModel model)
    {
        if (!DateOnly.TryParse(model.appointment_date, out var appointmentDate))
            throw new BadRequestResponseException("La fecha de la cita no tiene un formato válido.");

        if (!TimeOnly.TryParse(model.appointment_time, out var appointmentTime))
            throw new BadRequestResponseException("La hora de la cita no tiene un formato válido.");

        return new UpdateAppointmentRequestDto
        {
            patient_id = model.patient_id,
            doctor_user_resource_id = model.doctor_user_resource_id,
            appointment_date = appointmentDate,
            appointment_time = appointmentTime,
            duration_minutes = model.duration_minutes,
            reason = model.reason,
            status = model.status,
            notes = model.notes
        };
    }

    public static List<AppointmentResponseModel> ToModel(List<AppointmentDto> appointments)
    {
        return appointments.Select(ToModel).ToList();
    }

    public static AppointmentResponseModel ToModel(AppointmentDto appointment)
    {
        return new AppointmentResponseModel
        {
            id = appointment.appointment_id,
            patient = appointment.patient == null
                ? null
                : PatientMapper.ToModel(appointment.patient),
            doctor = appointment.doctor?.display_name ?? string.Empty,
            doctorUserResourceId = appointment.doctor?.user_resource_id.ToString() ?? string.Empty,
            date = appointment.appointment_date.ToString("yyyy-MM-dd"),
            time = appointment.appointment_time.ToString("HH:mm"),
            durationMinutes = appointment.duration_minutes,
            reason = appointment.reason,
            status = appointment.status
        };
    }

    public static List<DoctorResponseModel> ToModel(List<DoctorDto> doctors)
    {
        return doctors.Select(ToModel).ToList();
    }

    public static DoctorResponseModel ToModel(DoctorDto doctor)
    {
        return new DoctorResponseModel
        {
            user_resource_id = doctor.user_resource_id,
            first_name = doctor.first_name,
            last_name = doctor.last_name,
            username = doctor.username,
            email = doctor.email,
            display_name = doctor.display_name
        };
    }
}
