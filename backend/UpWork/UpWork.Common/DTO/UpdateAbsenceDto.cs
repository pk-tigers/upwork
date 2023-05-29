using UpWork.Common.Enums;

namespace UpWork.Common.Dto
{
    public class UpdateAbsenceDto
    {
        public Guid AbsenceId { get; set; }
        public DateTime? NewFromDate { get; set; }
        public DateTime? NewToDate { get; set; }
        public AbsenceTypes NewAbsenceType { get; set; }
        public Guid? NewTimeOffSupervisorId { get; set; }
        
    }
}
