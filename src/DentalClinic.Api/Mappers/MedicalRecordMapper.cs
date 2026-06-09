using DentalClinic.Api.Models.Responses;
using DentalClinic.Dto;
namespace DentalClinic.Api.Mappers;

public class MedicalRecordMapper
{
     public static MedicalRecordResponse ToResponse(MedicalRecordDto dto)
    {
        return new MedicalRecordResponse
        {
            record_id = dto.RecordId,
            medical_history = dto.MedicalHistory,
            allergies = dto.Allergies,
            general_notes = dto.GeneralNotes,
            status = dto.Status
        };
    }
}
