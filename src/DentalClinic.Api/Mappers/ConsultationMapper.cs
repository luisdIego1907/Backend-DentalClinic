using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Models.Responses;
using DentalClinic.Dto;
namespace DentalClinic.Api.Mappers;

public class ConsultationMapper
{
    public static CreateConsultationDto ToDto(CreateConsultationRequest request)
    {
        return new CreateConsultationDto
        {
            record_id = request.record_id,
            appointment_id = request.appointment_id,
            consultation_date = request.consultation_date,
            reason = request.reason,
            observations = request.observations,
            odontogram = request.odontogram,
            diagnoses = request.diagnoses.Select(d => new DiagnosisDto
            {
                Description = d.description,
                Diagnosis_date = d.diagnosis_date
            }).ToList(),
            treatments = request.treatments.Select(t => new TreatmentDto
            {
                Description = t.description,
                Cost = t.cost,
                Status = t.status,
                Start_date = t.start_date,
                End_date = t.end_date
            }).ToList()
        };
    }

    public static ConsultationResponse ToResponse(ConsultationDto dto)
    {
        return new ConsultationResponse
        {
            consultation_id = dto.consultation_id,
            consultation_date = dto.consultation_date,
            reason = dto.reason,
            observations = dto.observations,
            odontogram = dto.odontogram,
            diagnoses = dto.diagnoses.Select(d => new DiagnosisResponse
            {
                diagnosis_id = d.Diagnosis_id,
                description = d.Description,
                diagnosis_date = d.Diagnosis_date
            }).ToList(),
            treatments = dto.treatments.Select(t => new TreatmentResponse
            {
                treatment_id = t.Treatment_id,
                description = t.Description,
                cost = t.Cost,
                status = t.Status,
                start_date = t.Start_date,
                end_date = t.End_date
            }).ToList()
        };
    }

    public static List<ConsultationResponse> ToResponse(List<ConsultationDto> dtos)
    {
        return dtos.Select(d => ToResponse(d)).ToList();
    }

    public static ConsultationSummaryResponse ToSummaryResponse(ConsultationSummaryDto dto)
    {
        return new ConsultationSummaryResponse
        {
            consultation_id = dto.consultation_id,
            consultation_date = dto.consultation_date,
            reason = dto.reason,
            observations = dto.observations,
            odontogram = dto.odontogram,
            odontologist_first_name = dto.odontologist_first_name,
            odontologist_last_name = dto.odontologist_last_name,
            diagnoses = dto.diagnoses.Select(d => new DiagnosisResponse
            {
                diagnosis_id = d.Diagnosis_id,
                description = d.Description,
                diagnosis_date = d.Diagnosis_date
            }).ToList(),
            treatments = dto.treatments.Select(t => new TreatmentResponse
            {
                treatment_id = t.Treatment_id,
                description = t.Description,
                cost = t.Cost,
                status = t.Status,
                start_date = t.Start_date,
                end_date = t.End_date
            }).ToList()
        };
    }

    public static List<ConsultationSummaryResponse> ToSummaryResponse(List<ConsultationSummaryDto> dtos)
    {
        return dtos.Select(d => ToSummaryResponse(d)).ToList();
    }
}
