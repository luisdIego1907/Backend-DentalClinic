using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
using DentalClinic.Infrastructure.Repositories;

namespace DentalClinic.DomainService;

public class ConsultationService : IConsultationService
{
    private readonly IConsultationRepository _consultaitionRepository;
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    public ConsultationService(IConsultationRepository consultationRepository, IMedicalRecordRepository medicalRecordRepository)
    {
        _consultaitionRepository = consultationRepository;
    }

    public async Task<Consultation> CreateConsultationAsync(CreateConsultationDto dto, int userId)
    {
        if (dto.diagnoses?.Any() is not true)
        {
            throw new Exceptions.BadRequestResponseException("Consultation must have at least one diagnosis.");
        }

        if (dto.treatments?.Any() is not true)
        {
            throw new Exceptions.BadRequestResponseException("Consultation must have at least one treatment.");
        }

        var medicalRecord = await _medicalRecordRepository.GetByIdAsync(dto.record_id);

        if (medicalRecord == null)
        {
            throw new Exceptions.ResourceNotFoundException(
                $"Medical record {dto.record_id} not found."
            );
        }

        var entity = new Consultation
        {
            RecordId = dto.record_id,
            AppointmentId = dto.appointment_id,
            UserId = userId,
            ConsultationDate = dto.consultation_date,
            Reason = dto.reason,
            Observations = dto.observations,
            Odontogram = dto.odontogram,
            Diagnoses = dto.diagnoses.Select(d => new Diagnosis
            {
                Description = d.Description,
                DiagnosisDate = d.Diagnosis_date
            }).ToList(),
            Treatments = dto.treatments.Select(t => new Treatment
            {
                Description = t.Description,
                Cost = t.Cost,
                Status = t.Status,
                StartDate = t.Start_date,
                EndDate = t.End_date
            }).ToList()
        };


        await _consultaitionRepository.AddAsync(entity);
        return entity;

    }



    public Task<List<Consultation>> GetByRecordIdAsync(int record_id)
    {
        return _consultaitionRepository.GetByRecordIdAsync(record_id);
    }

}
