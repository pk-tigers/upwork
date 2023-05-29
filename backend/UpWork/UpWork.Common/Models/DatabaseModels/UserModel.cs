using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UpWork.Common.Enums;

namespace UpWork.Common.Models.DatabaseModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public byte[] Password { get; set; }
        [MaxLength(102400)]
        public byte[] Avatar { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; } = Role.User;
        public Guid? OrganizationId { get; set; }
        [JsonIgnore]
        public virtual OrganizationModel Organization { get; set; }
        public Guid? CurrentTimeOffSupervisorId { get; set; }
        [JsonIgnore]
        public virtual UserModel CurrentTimeOffSupervisor { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserModel> SupervisedEmployees { get; set; }
        [JsonIgnore]
        public virtual ICollection<PermissionModel> Permissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<AbsenceModel> Absences { get; set; }
        [JsonIgnore]
        public virtual ICollection<AbsenceModel> AbsencesSupervised { get; set; }
    }
}
