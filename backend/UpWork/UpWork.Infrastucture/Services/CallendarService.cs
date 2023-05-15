using Microsoft.EntityFrameworkCore;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class CallendarService : ICallendarService
    {
        private readonly ApplicationDbContext _context;

        public CallendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedResult<AbsenceModel> GetCalendarAbsencesByUserId(Guid userId, int skip, int take, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var user = _context.Users.Where(x => x.Id == userId).First();

            var orgAcceptedAbsences = _context.Absences
                .Where(x => x.IsActive && x.ApprovalState == ApprovalState.Approved)
                .Include(x => x.User)
                .Where(x => x.User.OrganizationId == user.OrganizationId);

            var ownAndSupervisedAbsences = _context.Absences
                .Where(x => x.IsActive)
                .Include(x => x.User)
                .Where(x => x.UserId == user.Id || x.TimeOffSupervisorId == userId);

            var absences = orgAcceptedAbsences.Concat(ownAndSupervisedAbsences)
                .Where(x => (x.FromDate >= fromDate.GetValueOrDefault(DateTime.MinValue) && x.FromDate <= toDate.GetValueOrDefault(DateTime.MaxValue))
                || x.ToDate >= fromDate.GetValueOrDefault(DateTime.MinValue) && x.ToDate <= toDate.GetValueOrDefault(DateTime.MaxValue))
                .OrderBy(x => x.FromDate)
                .Distinct();


            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

    }
}
