using DentalClinic.Domain.Entities;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
    {
        _medicalRecordRepository = medicalRecordRepository;
    }
    public async Task<MedicalRecord?> GetByPatientIdAsync(int patientId)
    {
        var record = await _medicalRecordRepository.GetByPatientIdAsync(patientId);

        if (record == null)
        {
            throw new Exceptions.ResourceNotFoundException("Medical record not found for this patient.");
        }

        return record;
    }

}
