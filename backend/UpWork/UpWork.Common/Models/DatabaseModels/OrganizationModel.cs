using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UpWork.Common.Models.DatabaseModels
{
    [Index(nameof(UrlName), IsUnique = true)]
    public class OrganizationModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string UrlName { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<UserModel> Users { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<AbsenceTypeModel> AbsenceTypes { get; set; }
    }
}
