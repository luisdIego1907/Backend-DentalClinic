using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IConsultationRepository
{
    public Task<List<Consultation>> GetByRecordIdAsync(int record_id);
    Task AddAsync(Consultation consultation);
}
