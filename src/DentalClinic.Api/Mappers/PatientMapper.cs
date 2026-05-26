using System;
using System.Reflection.Metadata.Ecma335;
using DentalClinic.Api.Models.Requests;
using DentalClinic.Api.Models.Responses;
using DentalClinic.Domain.Entities;
using DentalClinic.Dto;

namespace DentalClinic.Api.Mappers;

public class PatientMapper
{
    public static PatientDto ToDto(CreatePatientRequestModel model)
    {
        return new PatientDto
        {

            identification = model.identification,
            first_name = model.first_name,
            last_name = model.last_name,
            birth_date = model.birth_date,
            phone = model.phone,
            email = model.email,
            address = model.address,
            gender = model.gender,
            created_at = DateTime.UtcNow,
            status = "active"

        };
    }

    public static List<PatientResponseModel> ToModel(List<PatientDto> patients)
    {
        return patients.Select(p => ToModel(p)).ToList();
    }

    public static PatientResponseModel ToModel(PatientDto patient)
    {
        return new PatientResponseModel
        {

            patient_id = patient.patient_id,
            identification = patient.identification,
            first_name = patient.first_name,
            last_name = patient.last_name,
            birth_date = patient.birth_date,
            phone = patient.phone,
            email = patient.email,
            address = patient.address,
            gender = patient.gender,
            created_at = patient.created_at,
             status = patient.status
        };
    }
}
