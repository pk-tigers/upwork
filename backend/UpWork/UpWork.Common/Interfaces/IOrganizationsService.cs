using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Common.Interfaces
{
    public interface IOrganizationsService
    {
        public IEnumerable<OrganizationModel> GetOrganizations(int skip, int take);
    }
}
