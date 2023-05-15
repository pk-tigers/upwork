using UpWork.Common.DTO;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface ICalendarService
    {
        public PaginatedResult<AbsenceModel> GetCalendarAbsencesByUserId(Guid userId, int skip, int take, DateTime? fromDate = null, DateTime? toDate = null);

    }
}
