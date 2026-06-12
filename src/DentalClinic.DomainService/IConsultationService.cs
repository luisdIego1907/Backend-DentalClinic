using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
namespace DentalClinic.DomainService;

public interface IConsultationService
{
    Task<List<Consultation>> GetByRecordIdAsync(int record_id);
    Task<Consultation> CreateConsultationAsync(CreateConsultationDto consultation, int userId);
    Task<List<Consultation>> GetAllConsultations();
}
