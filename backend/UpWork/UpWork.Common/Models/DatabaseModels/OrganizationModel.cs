namespace UpWork.Common.Models.DatabaseModels
{
    public class OrganizationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
        public virtual ICollection<AbsenceTypeModel> AbsenceTypes { get; set; }
    }
}
