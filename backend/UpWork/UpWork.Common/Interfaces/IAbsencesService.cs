using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsencesService
    {
        PaginatedResult<AbsenceModel> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take);
        IEnumerable<UserAbsenceModel> GetUsersAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to);
        PaginatedResult<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take);
        PaginatedResult<AbsenceModelDto> GetPendingAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take);
        PaginatedResult<AbsenceModel> GetSupervisedAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take);
        int GetYearAbsenceCountForUser(Guid userId);
        PaginatedResult<AbsenceModel> GetAbsencesForUser(Guid userId, int skip, int take);

    }
}
