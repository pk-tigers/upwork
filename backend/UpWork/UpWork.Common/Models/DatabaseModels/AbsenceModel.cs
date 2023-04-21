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
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual AbsenceTypeModel AbsenceType { get; set; }
        public Guid UserId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual UserModel User { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid TimeOffSupervisorId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual UserModel TimeOffSupervisor { get; set; }
    }
}
