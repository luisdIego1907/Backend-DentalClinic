using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Task<Patient> AddAsync(PatientDto patient)
    {
        var patientEntity = new Patient
        {
            identification = patient.identification,
            first_name = patient.first_name,
            last_name = patient.last_name,
            birth_date = patient.birth_date,
            phone = patient.phone,
            email = patient.email,
            address = patient.address,
            gender = patient.gender,
            created_at = patient.created_at
        };

        return _patientRepository.AddAsync(patientEntity);
    }

    public async Task UpdateAsync(
        int patient_id,
        UpdatePatientRequestDto patient)
    {
        var existingPatient =
            await _patientRepository.GetByIdAsync(patient_id);

        if (existingPatient == null)
            throw new ResourceNotFoundException();

        existingPatient.identification = patient.identification;
        existingPatient.first_name = patient.first_name;
        existingPatient.last_name = patient.last_name;
        existingPatient.birth_date = patient.birth_date;
        existingPatient.phone = patient.phone;
        existingPatient.email = patient.email;
        existingPatient.address = patient.address;
        existingPatient.gender = patient.gender;
        existingPatient.status = patient.status;

        await _patientRepository.UpdateAsync(existingPatient);
    }

    public async Task DeleteAsync(int patient_id)
    {
        var patient =
            await _patientRepository.GetByIdAsync(patient_id);

        if (patient == null)
            throw new ResourceNotFoundException();

        await _patientRepository.DeleteAsync(patient);
    }

    public Task<List<Patient>> GetAllAsync()
    {
        return _patientRepository.GetAllAsync();
    }

    public Task<Patient?> GetByIdAsync(int patient_id)
    {
        return _patientRepository.GetByIdAsync(patient_id);
    }
}