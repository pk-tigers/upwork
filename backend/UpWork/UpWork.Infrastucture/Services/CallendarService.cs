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

        public PaginatedResult<AbsenceModel> GetAbsencesForCallendar(Guid userId, int skip, int take, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var user = _context.Users
                .Include(u => u.SupervisedEmployees)
                .FirstOrDefault(u => u.Id == userId);


            if (user.SupervisedEmployees.Count > 0)
            {
                var absences = _context.Absences
                    .Where(a => a.TimeOffSupervisorId == user.Id);

                if (fromDate.HasValue)
                {
                    absences = absences.Where(a => a.FromDate >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    absences = absences.Where(a => a.ToDate <= toDate.Value);
                }

                var sortedAbsences = absences.OrderBy(a => a.FromDate);
                var res = new PaginatedResult<AbsenceModel>(sortedAbsences.Skip(skip).Take(take), sortedAbsences.Count(), take);
                return res;
            }
            else
            {
                var userAllAbsences = _context.Absences
                    .Where(a => a.UserId == userId);

                if (fromDate.HasValue)
                {
                    userAllAbsences = userAllAbsences.Where(a => a.FromDate >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    userAllAbsences = userAllAbsences.Where(a => a.ToDate <= toDate.Value);
                }

                var organization = _context.Organizations
                    .Include(o => o.Users)
                    .FirstOrDefault(o => o.Id == user.OrganizationId);

                var usersApprovedAbsences = _context.Absences
                    .Where(a => a.ApprovalState == ApprovalState.Approved)
                    .Where(a => organization.Users.Any(u => u.Id == a.UserId));

                if (fromDate.HasValue)
                {
                    usersApprovedAbsences = usersApprovedAbsences.Where(a => a.FromDate >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    usersApprovedAbsences = usersApprovedAbsences.Where(a => a.ToDate <= toDate.Value);
                }

                var allAbsences = userAllAbsences.Union(usersApprovedAbsences);

                var sortedAbsences = allAbsences.OrderBy(a => a.FromDate);
                var res = new PaginatedResult<AbsenceModel>(sortedAbsences.Skip(skip).Take(take), sortedAbsences.Count(), take);
                return res;
            }
        }

    }
}
