using Microsoft.EntityFrameworkCore;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly ApplicationDbContext _context;

        public AbsenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CancelRequestForUser(Guid absenceId, Guid userId)
        {
            AbsenceModel request = _context.Absences
                .Where(x => x.UserId == userId)
                .Where(a => a.Id == absenceId)
                .Where(a => a.FromDate > DateTime.Now)
                .FirstOrDefault();

            if (request != null)
            {
                request.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState, Guid supervisorId)
        {
            var absence = _context.Absences
                .Where(x => x.IsActive)
                .Where(x => x.Id == absenceApprovalState.AbsenceId)
                .Where(x => x.TimeOffSupervisorId == supervisorId)
                .FirstOrDefault();

            if (absence == default) return default;

            absence.ApprovalState = absenceApprovalState.ApprovalState;
            _context.SaveChanges();

            return absence;
        }

        public AbsenceModel CreateAbsenceRequestForUser(Guid userId, CreateAbsenceRequestDto requestDto)
        {
            AbsenceModel newAbsence = new AbsenceModel
            {
                Id = Guid.NewGuid(),
                FromDate = requestDto.FromDate,
                ToDate = requestDto.ToDate,
                IsActive = true,
                AbsenceType = requestDto.AbsenceType,
                UserId = userId,
                TimeOffSupervisorId = requestDto.TimeOffSupervisorId
            };

            _context.Add(newAbsence);
            _context.SaveChanges();
            return newAbsence;
        }

        public AbsenceModelDto UpdateAbsence(Guid userId, UpdateAbsenceDto updateAbsenceDto)
        {
            var absence = _context.Absences
                .Where(x => x.UserId == userId && x.Id == updateAbsenceDto.AbsenceId)
                .Include(x => x.User)
                .Include(x => x.TimeOffSupervisor)
                .First();

            if (updateAbsenceDto.NewFromDate.HasValue && updateAbsenceDto.NewFromDate.Value >= DateTime.Today)
                absence.FromDate = updateAbsenceDto.NewFromDate.Value;

            if (updateAbsenceDto.NewToDate.HasValue && updateAbsenceDto.NewToDate.Value >= absence.FromDate)
                absence.ToDate = updateAbsenceDto.NewToDate.Value;

            if (updateAbsenceDto.NewTimeOffSupervisorId.HasValue)
            {
                var supervisor = _context.Users
                    .Where(x => x.IsActive && x.Id == updateAbsenceDto.NewTimeOffSupervisorId)
                    .Where(x => x.OrganizationId == absence.User.OrganizationId)
                    .Include(x => x.Permissions)
                    .Where(x => x.Role == Role.OrganizationOwner || x.Permissions.Any(z => z.PermissionType == PermissionType.CanSupervise
                    && z.GrantDate < DateTime.UtcNow && (z.ExpirationDate == null || z.ExpirationDate > DateTime.UtcNow))).First();

                absence.TimeOffSupervisorId = supervisor.Id;
            }
            absence.AbsenceType = updateAbsenceDto.NewAbsenceType;

            _context.SaveChanges();

            return new AbsenceModelDto(absence);
        }
    }
}
