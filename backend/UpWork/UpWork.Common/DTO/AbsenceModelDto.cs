using System;
using UpWork.Common.Enums;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.DTO
{
	public class AbsenceModelDto
	{
        public AbsenceModelDto(AbsenceModel absenceModel)
        {
            Id = absenceModel.Id;
            FromDate = absenceModel.FromDate;
            ToDate = absenceModel.ToDate;
            AbsenceType = absenceModel.AbsenceType;
            UserId = absenceModel.UserId;
            UserFirstName = absenceModel.User?.FirstName;
            UserLastName = absenceModel.User?.LastName;
            SupervisorFirstName = absenceModel.TimeOffSupervisor?.FirstName;
            SupervisorLastName = absenceModel.TimeOffSupervisor?.LastName;
            ApprovalState = absenceModel.ApprovalState;
            TimeOffSupervisorId = absenceModel.TimeOffSupervisorId;
        }
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public AbsenceTypes AbsenceType { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string SupervisorFirstName { get; set; }
        public string SupervisorLastName { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid? TimeOffSupervisorId { get; set; }
	}
}

