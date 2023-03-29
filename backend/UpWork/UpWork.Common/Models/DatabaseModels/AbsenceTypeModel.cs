﻿namespace UpWork.Common.Models.DatabaseModels
{
    public class AbsenceTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool NeedApproval { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual OrganizationModel Organization { get; set; }
        public virtual ICollection<AbsenceModel> Absences { get; set; }
    }
}