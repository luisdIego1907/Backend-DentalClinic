using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Facade.Mappers;

namespace DentalClinic.Facade;

public class MedicalRecordFacade : IMedicalRecordFacade
{
    IMedicalRecordService _medicalRecordService;

    public MedicalRecordFacade(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }
    public async Task<MedicalRecordDto> GetByPatientIdAsync(int patientId)
    {
        var entity = await _medicalRecordService.GetByPatientIdAsync(patientId);
        return MedicalRecordMapper.ToDto(entity!);
    }

}
