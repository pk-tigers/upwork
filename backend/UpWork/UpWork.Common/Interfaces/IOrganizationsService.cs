using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IOrganizationsService
    {
        public PaginatedResult<OrganizationModel> GetOrganizations(int skip, int take);
    }
}
