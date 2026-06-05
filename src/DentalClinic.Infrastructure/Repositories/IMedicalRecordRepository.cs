using DentalClinic.Domain.Entities;
namespace DentalClinic.Infrastructure.Repositories;

public interface IMedicalRecordRepository
{
    Task<MedicalRecord?> GetByPatientIdAsync(int patientId);
    Task<MedicalRecord?> GetByIdAsync(int recordId);
}
