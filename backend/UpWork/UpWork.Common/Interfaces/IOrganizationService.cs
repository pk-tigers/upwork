using UpWork.Common.Dto;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationModel GetOrganization(Guid Id);
        bool DeleteOrganization(Guid Id);
        OrganizationModel CreateOrganization(CreateOrganizationDto organizationDTO);
        OrganizationModel GetOrganizationWithUsers(Guid Id);
        OrganizationModel GetOrganizationByUrlName(string urlName);
    }
}
