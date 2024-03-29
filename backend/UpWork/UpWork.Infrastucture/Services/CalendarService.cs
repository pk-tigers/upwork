﻿using Microsoft.EntityFrameworkCore;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ApplicationDbContext _context;

        public CalendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AbsenceModelDto> GetCalendarAbsencesByUserId(Guid userId, DateTime fromDate, DateTime toDate)
        {
            var user = _context.Users.Where(x => x.Id == userId).First();

            var orgAcceptedAbsences = _context.Absences
                .Where(x => x.IsActive && x.ApprovalState == ApprovalState.Approved)
                .Include(x => x.User)
                .Where(x => x.User.OrganizationId == user.OrganizationId);

            var ownAndSupervisedAbsences = _context.Absences
                .Where(x => x.IsActive)
                .Include(x => x.User)
                .Where(x => x.User.OrganizationId == user.OrganizationId)
                .Where(x => x.UserId == user.Id || x.TimeOffSupervisorId == userId || user.Role == Role.OrganizationOwner);

            var absences = orgAcceptedAbsences.Concat(ownAndSupervisedAbsences)
                .Where(x => (x.FromDate >= fromDate && x.FromDate <= toDate)
                || x.ToDate >= fromDate && x.ToDate <= toDate)
                .OrderBy(x => x.FromDate)
                .Distinct()
                .Select(x => new AbsenceModelDto(x));

            return absences;
        }

    }
}
