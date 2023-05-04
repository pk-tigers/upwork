using UpWork.Common.Enums;

namespace UpWork.Common.Dto
{
    public class AbsenceApprovalStateDto
    {
        public Guid AbsenceId { get; set; }
        public ApprovalState ApprovalState { get; set; }
    }
}
