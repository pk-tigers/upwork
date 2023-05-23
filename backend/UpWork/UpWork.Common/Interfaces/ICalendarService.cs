using UpWork.Common.DTO;
using UpWork.Common.Models;

namespace UpWork.Common.Interfaces
{
    public interface ICalendarService
    {
        public IEnumerable<AbsenceModelDto> GetCalendarAbsencesByUserId(Guid userId, DateTime fromDate, DateTime toDate);

    }
}
