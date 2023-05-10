using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsencesService
    {
        PaginatedResult<AbsenceModel> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take);
        PaginatedResult<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take);
        PaginatedResult<AbsenceModel> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take);
        PaginatedResult<AbsenceModel> GetSupervisedAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take);
        int GetCurrentYearAbsenceDays(Guid userId);
        PaginatedResult<AbsenceTypeModel> GetAbsenceTypesForUser(Guid userId, int skip, int take);
        PaginatedResult<AbsenceModel> GetUserAbsences(Guid userId, int skip, int take);

    }
}
