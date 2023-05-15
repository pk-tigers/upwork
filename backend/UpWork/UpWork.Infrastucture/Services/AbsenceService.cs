<<<<<<< HEAD
﻿using UpWork.Common.Dto;
=======
﻿using Microsoft.EntityFrameworkCore;
using UpWork.Common.Dto;
>>>>>>> dev
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

<<<<<<< HEAD
        public bool CancelRequestIfNotStarted(Guid requestId)
        {
            AbsenceModel request = _context.Absences.FirstOrDefault(a => a.Id == requestId && !a.IsActive && a.FromDate > DateTime.Now);

            if (request != null)
            {
                _context.Absences.Remove(request);
=======
        public bool CancelRequestForUser(Guid requestId, Guid userId)
        {
            AbsenceModel request = _context.Absences
                .Where(x => x.UserId == userId)
                .Where(a => a.Id == requestId && a.IsActive && a.FromDate >= DateTime.Now)
                .FirstOrDefault();

            if (request != null)
            {
                request.IsActive = false;
>>>>>>> dev
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

<<<<<<< HEAD
        public AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState)
=======
        public AbsenceModel SetAbsenceApprovalState(AbsenceApprovalStateDto absenceApprovalState, Guid supervisorId)
>>>>>>> dev
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

<<<<<<< HEAD
        public AbsenceModel CreateAbsenceRequest(Guid userId, CreateAbsenceRequestDto requestDto)
        {
=======
        public AbsenceModel CreateAbsenceRequestForUser(Guid userId, CreateAbsenceRequestDto requestDto)
        {
            var superVisiorId = _context.Users?.First(u => u.Id == userId).CurrentTimeOffSupervisorId;


>>>>>>> dev
            AbsenceModel newAbsence = new AbsenceModel
            {
                Id = Guid.NewGuid(),
                FromDate = requestDto.FromDate,
                ToDate = requestDto.ToDate,
                IsActive = true,
<<<<<<< HEAD
                AbsenceTypeId = requestDto.AbsenceTypeId,
                UserId = userId
=======
                AbsenceType = requestDto.AbsenceType,
                UserId = userId,
                TimeOffSupervisorId = superVisiorId
>>>>>>> dev
            };

            _context.Add(newAbsence);
            _context.SaveChanges();
            return newAbsence;
        }
    }
}
