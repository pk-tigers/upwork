using System.ComponentModel.DataAnnotations;

namespace UpWork.Common.DTO
{
    public class CreateOrganizationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlName { get; set; }

    }
}
