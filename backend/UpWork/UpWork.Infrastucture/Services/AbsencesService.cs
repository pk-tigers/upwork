using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UpWork.Common.DTO;
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

        public PaginatedResult<AbsenceModelDto> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .Where(x => x.ApprovalState == ApprovalState.Pending)
                .Join(_context.Users, absences => absences.UserId, user => user.Id, (absences, user) => new AbsenceModelDto
                {
                      Id = absences.Id,
                      FromDate = absences.FromDate,
                      ToDate = absences.ToDate,
                      UserId = absences.UserId,
                      UserName = string.Join(' ', user.FirstName, user.LastName),
                      AbsenceType = absences.AbsenceType,
                      ApprovalState = absences.ApprovalState
                });

            var res = new PaginatedResult<AbsenceModelDto>(absences.Skip(skip).Take(take), absences.Count(), take);
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

        public int GetCurrentYearAbsenceDays(Guid userId)
        {

            DateTime currentDate = DateTime.Now;
            DateTime currentYearStart = new DateTime(currentDate.Year, 1, 1);
            DateTime nextYearStart = currentYearStart.AddYears(1);

            var absences = _context.Absences
                .Where(a => a.UserId == userId && a.IsActive &&
                    ((a.FromDate >= currentYearStart && a.FromDate < nextYearStart) ||
                    (a.FromDate < currentYearStart && a.ToDate >= currentYearStart)));
                

            int absenceDays = 0;
            foreach (var absence in absences)
            {
                DateTime absenceStartDate = (absence.FromDate > currentYearStart) ? absence.FromDate : currentYearStart;
                DateTime absenceEndDate = (absence.ToDate < nextYearStart) ? absence.ToDate : nextYearStart.AddDays(-1);
                absenceDays += (absenceEndDate - absenceStartDate).Days + 1;
            }

            return absenceDays;
        }

        public PaginatedResult<AbsenceModel> GetUserAbsences(Guid userId, int skip, int take)
        {
            var absences = _context.Absences.Where(x => x.UserId == userId);
            return new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take); 
        }
    }
}
