using Microsoft.EntityFrameworkCore;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
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

        public bool CancelRequestIfNotStarted(Guid requestId)
        {
            AbsenceModel request = _context.Absences.FirstOrDefault(a => a.Id == requestId && !a.IsActive && a.FromDate > DateTime.Now);

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

        public AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState)
        {
            var absence = _context.Absences
                .Where(x => x.Id == absenceApprovalState.AbsenceId)
                .FirstOrDefault();

            if (absence == default) return default;

            absence.ApprovalState = absenceApprovalState.ApprovalState;
            _context.SaveChanges();

            return absence;
        }

        public AbsenceModel CreateAbsenceRequest(Guid userId, CreateAbsenceRequestDto requestDto)
        {
            var superVisiorId = _context.Users?.First(u => u.Id == userId).CurrentTimeOffSupervisorId;


            AbsenceModel newAbsence = new AbsenceModel
            {
                Id = Guid.NewGuid(),
                FromDate = requestDto.FromDate,
                ToDate = requestDto.ToDate,
                IsActive = true,
                AbsenceType = requestDto.AbsenceType,
                UserId = userId,
                TimeOffSupervisorId = superVisiorId
            };

            _context.Add(newAbsence);
            _context.SaveChanges();
            return newAbsence;
        }
    }
}
