using System.Data;

namespace DentalClinic.Api.Security;

public static class AuthorizationPolicies
{
    public const string CanSearchUsers = "CanSearchUsers";
    public const string CanManageUsers = "CanManageUsers";
    public const string CanManagePatients = "CanManagePatients";

    public const string CanManageMedicalRecords = "CanManageMedicalRecords";

    public const string CanManageAppointments = "CanManageAppointments";

    public const string CanManageConsultations = "CanManageConsultations";

    public const string CanManageDiagnosis = "CanManageDiagnosis";

    public const string CanManageTreatment = "CanManageTreatment";
}
