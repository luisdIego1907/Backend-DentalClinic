using DentalClinic.Dto;
namespace DentalClinic.Facade;

public interface IConsultationFacade
{
    Task<ConsultationDto> CreateAsync(CreateConsultationDto dto, int userId);
    Task<List<ConsultationDto>> GetByRecordIdAsync(int recordId);
}
