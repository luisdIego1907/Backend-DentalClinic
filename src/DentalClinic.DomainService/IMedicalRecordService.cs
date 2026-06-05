using DentalClinic.Domain.Entities;
namespace DentalClinic.DomainService;

public interface IMedicalRecordService
{
    Task<MedicalRecord?> GetByPatientIdAsync(int patientId);
}
