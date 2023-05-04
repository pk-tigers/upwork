using UpWork.Common.Dto;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IOrganizationService
    {
        public OrganizationModel GetOrganization(Guid Id);
        public bool DeleteOrganization(Guid Id);
        public OrganizationModel CreateOrganization(CreateOrganizationDto organizationDTO);
        public OrganizationModel GetOrganizationWithUsers(Guid Id);
    }
}
