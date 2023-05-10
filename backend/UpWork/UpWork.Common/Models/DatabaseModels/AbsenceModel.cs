using System.Text.Json.Serialization;
using UpWork.Common.Enums;

namespace UpWork.Common.Models.DatabaseModels
{
    public class AbsenceModel
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        //public Guid AbsenceTypeId { get; set; }
        //[JsonIgnore]
        //public virtual AbsenceTypeModel AbsenceType { get; set; }
        public AbsenceTypes AbsenceType { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual UserModel User { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid TimeOffSupervisorId { get; set; }
        [JsonIgnore]
        public virtual UserModel TimeOffSupervisor { get; set; }
    }
}
