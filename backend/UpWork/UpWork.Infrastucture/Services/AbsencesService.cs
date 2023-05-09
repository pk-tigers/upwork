using Microsoft.EntityFrameworkCore;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class AbsencesService : IAbsencesService
    {
        private readonly ApplicationDbContext _context;

        public AbsencesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedResult<AbsenceModel> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take)
        {
            var absences = _context.Absences
                .Include(x => x.User)
                .Where(x => x.User.OrganizationId == organizationId)
                .Where(x => (from >= x.FromDate && from <= x.ToDate) || to >= x.FromDate && to <= x.ToDate);


            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public PaginatedResult<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.UserId == userId)
                .Where(x => (from >= x.FromDate && from <= x.ToDate) || to >= x.FromDate && to <= x.ToDate);

            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public PaginatedResult<AbsenceModel> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .Where(x => x.ApprovalState == ApprovalState.Pending);

            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public PaginatedResult<AbsenceModel> GetSupervisedAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .Where(x => x.ApprovalState != ApprovalState.Pending);

            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }
    }
}
