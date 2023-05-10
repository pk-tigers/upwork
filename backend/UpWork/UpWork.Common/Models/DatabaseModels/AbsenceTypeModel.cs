using System.Text.Json.Serialization;

namespace UpWork.Common.Models.DatabaseModels
{
    public class AbsenceTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool NeedApproval { get; set; }
        public bool IsActive { get; set; }
        //public Guid OrganizationId { get; set; }
        //[JsonIgnore]
        //public virtual OrganizationModel Organization { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<AbsenceModel> Absences { get; set; }
    }
}
