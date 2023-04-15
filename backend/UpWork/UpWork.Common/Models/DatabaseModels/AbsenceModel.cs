using UpWork.Common.Enums;

namespace UpWork.Common.Models.DatabaseModels
{
    public class AbsenceModel
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        public Guid AbsenceTypeId { get; set; }
        public virtual AbsenceTypeModel AbsenceType { get; set; }
        public Guid UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid TimeOffSupervisorId { get; set; }
        public virtual UserModel TimeOffSupervisor { get; set; }
    }
}
