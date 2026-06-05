using DentalClinic.Dto;
namespace DentalClinic.Facade;

public interface IMedicalRecordFacade
{
    Task<MedicalRecordDto> GetByPatientIdAsync(int patientId);
}
