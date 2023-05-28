using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsencesService
    {
        PaginatedResult<AbsenceModelDto> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take);
        PaginatedResult<AbsenceModelDto> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take);
        PaginatedResult<AbsenceModelDto> GetPendingAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take);
        PaginatedResult<AbsenceModelDto> GetSupervisedAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take);
        int GetYearAbsenceCountForUser(Guid userId);
        PaginatedResult<AbsenceModelDto> GetAbsencesForUser(Guid userId, int skip, int take);

    }
}
