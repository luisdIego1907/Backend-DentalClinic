using System;
using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Facade.Mappers;

public class PatientMapper
{
    public static PatientDto ToDto(Patient patient)
    {
        return new PatientDto
        {
          patient_id = patient.patient_id,
          identification = patient.identification,
          first_name = patient.first_name,
          last_name = patient.last_name,
          phone = patient.phone,
          address = patient.address  
        };
    }

    public static List<PatientDto> ToDto(List<Patient> patients)
    {
        return patients.Select(p => ToDto(p)).ToList();
    }
}
