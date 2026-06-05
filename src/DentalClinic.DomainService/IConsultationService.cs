using DentalClinic.Domain.Entities;
using DentalClinic.Dto;
namespace DentalClinic.DomainService;

public interface IConsultationService
{
    public Task<List<Consultation>> GetByRecordIdAsync(int record_id);
    public Task<Consultation> CreateConsultationAsync(CreateConsultationDto consultation, int userId);
}
