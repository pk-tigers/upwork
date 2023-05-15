using UpWork.Common.DTO;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface ICallendarService
    {
        public PaginatedResult<AbsenceModel> GetAbsencesForCallendar(Guid userId, int skip, int take, DateTime? fromDate = null, DateTime? toDate = null);

    }
}
