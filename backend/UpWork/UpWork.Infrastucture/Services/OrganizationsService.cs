using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Infrastucture.Services
{
    public class OrganizationsService : IOrganizationsService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedResult<OrganizationModel> GetOrganizations(int skip, int take)
        {
            var organizations = _context.Organizations.Skip(skip).Take(take);
            var totalItems = _context.Organizations.Count();
            var count = (int)Math.Ceiling((decimal)totalItems / (decimal)take);
            var res = new PaginatedResult<OrganizationModel>(organizations, count);
            return res;
        }
    }
}
