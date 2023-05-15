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
                .Where(x => x.IsActive)
                .Where(x => (x.FromDate >= from && x.FromDate <= to) || x.ToDate >= from && x.ToDate <= to);


            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public PaginatedResult<AbsenceModel> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.UserId == userId)
                .Where(x => x.IsActive)
                .Where(x => (x.FromDate >= from && x.FromDate <= to) || x.ToDate >= from && x.ToDate <= to);

            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public PaginatedResult<AbsenceModelDto> GetPendingAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .Where(x => x.ApprovalState == ApprovalState.Pending)
                .Where(x => x.IsActive)
                .Include(x => x.User)
                .Select(x => new AbsenceModelDto
                {
                      Id = x.Id,
                      FromDate = x.FromDate,
                      ToDate = x.ToDate,
                      UserId = x.UserId,
                      UserName = string.Join(' ', x.User.FirstName, x.User.LastName),
                      AbsenceType = x.AbsenceType,
                      ApprovalState = x.ApprovalState
                });

            var res = new PaginatedResult<AbsenceModelDto>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;                
        }

        public PaginatedResult<AbsenceModel> GetSupervisedAbsencesRequestsForSupervisor(Guid supervisorId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .Where(x => x.IsActive)
                .Where(x => x.ApprovalState != ApprovalState.Pending);

            var res = new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take);
            return res;
        }

        public int GetYearAbsenceCountForUser(Guid userId)
        {

            DateTime currentDate = DateTime.Now;
            DateTime currentYearStart = new DateTime(currentDate.Year, 1, 1);
            DateTime nextYearStart = currentYearStart.AddYears(1);

            var absences = _context.Absences
                .Where(x => x.IsActive)
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

        public PaginatedResult<AbsenceModel> GetAbsencesForUser(Guid userId, int skip, int take)
        {
            var absences = _context.Absences
                .Where(x => x.IsActive)
                .Where(x => x.UserId == userId);
            return new PaginatedResult<AbsenceModel>(absences.Skip(skip).Take(take), absences.Count(), take); 
        }
    }
}
