using System;
using UpWork.Common.Enums;

namespace UpWork.Common.Models
{
	public class UserAbsenceModel
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid OrganizationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public AbsenceTypes AbsenceType { get; set; }
    }
}

