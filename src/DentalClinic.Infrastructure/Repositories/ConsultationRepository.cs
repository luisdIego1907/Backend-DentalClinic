using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace DentalClinic.Infrastructure.Repositories;

public class ConsultationRepository : IConsultationRepository
{
    private readonly AppDbContext _context;

    public ConsultationRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Consultation>> GetByRecordIdAsync(int record_id)
    {
        return _context.Consultations
            .Include(c => c.Diagnoses)
            .Include(c => c.Treatments)
            .Where(c => c.RecordId == record_id)
            .ToListAsync();
    }

    public async Task AddAsync(Consultation consultation)
    {
        await _context.Consultations.AddAsync(consultation);
    }

    public Task<List<Consultation>> GetAllAsync()
    {
        return _context.Consultations
            .Include(c => c.Diagnoses)
            .Include(c => c.Treatments)
            .Include(c => c.User)
            .Include(c => c.MedicalRecord)
            .ThenInclude(mr => mr.Patient)
            .ToListAsync();
    }

    public Task<Consultation?> GetAppointmentByIdAsync(int appointmentId)
    {
        return _context.Consultations
            .FirstOrDefaultAsync(c => c.AppointmentId == appointmentId);
    }
}
