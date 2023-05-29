using System.ComponentModel.DataAnnotations;

namespace UpWork.Common.Dto
{
    public class CreateOrganizationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlName { get; set; }

    }
}
