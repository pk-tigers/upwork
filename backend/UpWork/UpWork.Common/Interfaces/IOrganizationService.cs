using UpWork.Common.DTO;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IOrganizationService
    {
        public OrganizationModel GetOrganization(Guid Id);
        public void DeleteOrganization(Guid Id);
        public void AddUserToOrganization(UserOrganizationDto userOrganizationDto);
        public OrganizationModel CreateOrganization(CreateOrganizationDTO organizationDTO);
        public void DeleteUserFromOrganization(UserOrganizationDto userOrganizationDto);
        public OrganizationModel GetOrganizationWithUsers(Guid Id);
    }
}
