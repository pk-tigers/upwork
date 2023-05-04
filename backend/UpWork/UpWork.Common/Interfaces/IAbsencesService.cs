using UpWork.Common.Enums;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IAbsencesService
    {
        IEnumerable<AbsenceModel> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take);
        IEnumerable<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take);
        IEnumerable<AbsenceModel> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take);
        IEnumerable<AbsenceModel> GetSupervisedAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take);
    }
}
