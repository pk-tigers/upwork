using System.ComponentModel.DataAnnotations;

namespace UpWork.Common.Models.DatabaseModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [MaxLength(102400)]
        public byte[] Avatar { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
        public virtual OrganizationModel Organization { get; set; }
        public Guid? TimeOffSupervisorId { get; set; }
        public virtual UserModel TimeOffSupervisor { get; set; }
        public virtual ICollection<UserModel> SupervisedEmployees { get; set; }
    }
}
