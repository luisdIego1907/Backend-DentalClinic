using System;
using DentalClinic.Domain.Entities;
using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Exceptions;
using DentalClinic.Facade.Mappers;
using DentalClinic.Infrastructure;
using Microsoft.Identity.Client;

namespace DentalClinic.Facade;

public class PatientFacade : IPatientFacade
{
    private readonly IPatientService patientService;
    private readonly AppDbContext context;

    public PatientFacade(IPatientService patientService, AppDbContext context)
    {
        this.patientService = patientService;
        this.context = context;
    }
    public async Task<PatientDto> AddAsync(PatientDto patient)
    {
        var entity = await patientService.AddAsync(patient);

        await context.SaveChangesAsync();

        return PatientMapper.ToDto(entity);
    }

    public async Task DeleteAsync(int patient_id)
    {
        await patientService.DeleteAsync(patient_id);
        
        await context.SaveChangesAsync();
    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        var entities = await patientService.GetAllAsync();

        return PatientMapper.ToDto(entities);
    }

    public async Task<PatientDto> GetByIdAsync(int patient_id)
    {
        var entity = await patientService.GetByIdAsync(patient_id);

        if (entity == null) throw new ResourceNotFoundException();

        return PatientMapper.ToDto(entity);
    }

}
