using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{

    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        return patient;
    }

    public async Task DeleteAsync(Patient patient)
    {
        _context.Patients.Remove(patient);
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int patient_id)
    {
        return await _context.Patients.FirstOrDefaultAsync(p => p.patient_id == patient_id);
    }

}
