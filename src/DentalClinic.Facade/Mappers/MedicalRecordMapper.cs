using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Facade.Mappers;

public class MedicalRecordMapper
{
    public static MedicalRecordDto ToDto(MedicalRecord record)
    {
        return new MedicalRecordDto
        {
            RecordId = record.RecordId,
            MedicalHistory = record.MedicalHistory,
            Allergies = record.Allergies,
            GeneralNotes = record.GeneralNotes,
            Status = record.Status
        };
    }
}