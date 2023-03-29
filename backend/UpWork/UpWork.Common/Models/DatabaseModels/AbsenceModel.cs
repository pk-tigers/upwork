using UpWork.Common.Enums;

namespace UpWork.Common.Models.DatabaseModels
{
    public class AbsenceModel
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsActive { get; set; }
        public int AbsenceTypeId { get; set; }
        public virtual AbsenceTypeModel AbsenceType { get; set; }
        public Guid UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public ApprovalStateEnum ApprovalState { get; set; }
    }
}
