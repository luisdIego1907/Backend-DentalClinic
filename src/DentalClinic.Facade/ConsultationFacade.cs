using DentalClinic.DomainService;
using DentalClinic.Dto;
using DentalClinic.Facade.Mappers;
using DentalClinic.Infrastructure;

namespace DentalClinic.Facade;

public class ConsultationFacade : IConsultationFacade
{
    private readonly IConsultationService _consultationService;
    private readonly AppDbContext _context;


    public ConsultationFacade(IConsultationService consultationService, AppDbContext context)
    {
        _consultationService = consultationService;
        _context = context;
    }
    public async Task<ConsultationDto> CreateAsync(CreateConsultationDto dto, int userId)
    {
        var entity = await _consultationService.CreateConsultationAsync(dto, userId);
        await _context.SaveChangesAsync();
        return ConsultationMapper.ToDto(entity);
    }

    public async Task<List<ConsultationDto>> GetByRecordIdAsync(int recordId)
    {
        var entities = await _consultationService.GetByRecordIdAsync(recordId);
        return ConsultationMapper.ToDto(entities);
    }

    public async Task<List<ConsultationSummaryDto>> GetAllConsultations()
    {
        var entities = await _consultationService.GetAllConsultations();
        return ConsultationMapper.ToSummaryDto(entities);
    }
}


