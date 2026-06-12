using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Facade.Mappers;

public class ConsultationMapper
{
    public static ConsultationDto ToDto(Consultation consultation)
    {
        return new ConsultationDto
        {
            consultation_id = consultation.ConsultationId,
            consultation_date = consultation.ConsultationDate,
            reason = consultation.Reason,
            observations = consultation.Observations,
            odontogram = consultation.Odontogram,
            diagnoses = consultation.Diagnoses.Select(d => new DiagnosisDto
            {
                Diagnosis_id = d.DiagnosisId,
                Description = d.Description,
                Diagnosis_date = d.DiagnosisDate
            }).ToList(),
            treatments = consultation.Treatments.Select(t => new TreatmentDto
            {
                Treatment_id = t.TreatmentId,
                Description = t.Description,
                Cost = t.Cost,
                Status = t.Status,
                Start_date = t.StartDate,
                End_date = t.EndDate
            }).ToList()
        };
    }

    public static List<ConsultationDto> ToDto(List<Consultation> consultations)
    {
        return consultations.Select(c => ToDto(c)).ToList();
    }

    public static ConsultationSummaryDto ToSummaryDto(Consultation consultation)
    {
        return new ConsultationSummaryDto
        {
            consultation_id = consultation.ConsultationId,
            consultation_date = consultation.ConsultationDate,
            reason = consultation.Reason,
            observations = consultation.Observations,
            odontogram = consultation.Odontogram,
            odontologist_first_name = consultation.User?.first_name ?? string.Empty,
            odontologist_last_name = consultation.User?.last_name ?? string.Empty,
            patient_first_name = consultation.MedicalRecord?.Patient?.first_name ?? "NO PATIENT",
            patient_last_name = consultation.MedicalRecord?.Patient?.last_name ?? "NO PATIENT",
            diagnoses = consultation.Diagnoses.Select(d => new DiagnosisDto
            {
                Diagnosis_id = d.DiagnosisId,
                Description = d.Description,
                Diagnosis_date = d.DiagnosisDate
            }).ToList(),
            treatments = consultation.Treatments.Select(t => new TreatmentDto
            {
                Treatment_id = t.TreatmentId,
                Description = t.Description,
                Cost = t.Cost,
                Status = t.Status,
                Start_date = t.StartDate,
                End_date = t.EndDate
            }).ToList()
        };
    }

    public static List<ConsultationSummaryDto> ToSummaryDto(List<Consultation> consultations)
    {
        return consultations.Select(c => ToSummaryDto(c)).ToList();
    }
}