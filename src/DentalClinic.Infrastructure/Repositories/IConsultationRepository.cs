using System.Data.Common;
using DentalClinic.Domain.Entities;

namespace DentalClinic.Infrastructure.Repositories;

public interface IConsultationRepository
{
    Task<List<Consultation>> GetAllAsync();
    Task<List<Consultation>> GetByRecordIdAsync(int record_id);
    Task AddAsync(Consultation consultation);
    Task<Consultation?> GetAppointmentByIdAsync(int appointment_id);

}
