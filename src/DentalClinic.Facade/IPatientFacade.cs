using System;
using DentalClinic.Dto;

namespace DentalClinic.Facade;

public interface IPatientFacade
{
    Task<List<PatientDto>> GetAllAsync();

    Task<PatientDto> GetByIdAsync(int patient_id);

    Task<PatientDto> AddAsync(PatientDto patient);

    Task UpdateAsync(int patient_id, UpdatePatientRequestDto patient);

    Task DeleteAsync(int patient_id);
}
