using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repositories;

public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _context;

    public MedicalRecordRepository(AppDbContext context)
    {
        _context = context;
    }
    public Task<MedicalRecord?> GetByPatientIdAsync(int patientId)
    {
        return _context.MedicalRecords
           .FirstOrDefaultAsync(r => r.PatientId == patientId);
    }

}
