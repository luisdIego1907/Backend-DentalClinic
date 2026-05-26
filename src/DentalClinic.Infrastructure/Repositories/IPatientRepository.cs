using System.Net.Http.Headers;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int patient_id);

    Task<Patient> AddAsync(Patient patient);

    Task DeleteAsync(Patient patient);
}
