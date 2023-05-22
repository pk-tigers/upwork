using UpWork.Common.Models;

namespace UpWork.Common.Interfaces
{
    public interface ICalendarService
    {
        public IEnumerable<UserAbsenceModel> GetCalendarAbsencesByUserId(Guid userId, DateTime fromDate, DateTime toDate);

    }
}
