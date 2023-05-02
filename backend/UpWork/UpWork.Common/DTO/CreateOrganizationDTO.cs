using System.ComponentModel.DataAnnotations;

namespace UpWork.Common.DTO
{
    public class CreateOrganizationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlName { get; set; }

    }
}
