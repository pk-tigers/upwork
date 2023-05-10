using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<UserModel> Users { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<AbsenceTypeModel> AbsenceTypes { get; set; }
    }
}
