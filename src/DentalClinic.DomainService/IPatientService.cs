using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.DomainService;

public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int patient_id);

    Task<Patient> AddAsync(PatientDto patient);

    Task DeleteAsync(int patient_id);
}
