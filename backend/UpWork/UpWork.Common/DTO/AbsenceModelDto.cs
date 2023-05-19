using System;
using UpWork.Common.Enums;

namespace UpWork.Common.DTO
{
	public class AbsenceModelDto
	{
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public AbsenceTypes AbsenceType { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid? TimeOffSupervisorId { get; set; }
	}
}

