using Microsoft.EntityFrameworkCore;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
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

        public IEnumerable<AbsenceModel> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip, int take)
        {
            var res = _context.Absences
                .Include(x => x.User)
                .Where(x => x.User.OrganizationId == organizationId)
                .Where(x => (from >= x.FromDate && from <= x.ToDate) || to >= x.FromDate && to <= x.ToDate);
            return res;
        }

        public IEnumerable<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take)
        {
            var res = _context.Absences
                .Where(x => x.UserId == userId)
                .Where(x => (from >= x.FromDate && from <= x.ToDate) || to >= x.FromDate && to <= x.ToDate);
            return res;
        }

        public IEnumerable<AbsenceModel> GetAbsencesRequestsBySupervisorId(Guid supervisorId, ApprovalState? approvalState, int skip, int take)
        {
            var res = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId);

            if (approvalState is not null)
                res = res.Where(x => x.ApprovalState == approvalState);

            res = res.Skip(skip).Take(take);

            return res;                
        }
    }
}
