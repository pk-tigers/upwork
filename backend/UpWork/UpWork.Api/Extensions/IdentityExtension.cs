using System.Security.Claims;
using UpWork.Common.Identity;

namespace UpWork.Api.Extensions
{
    public static class IdentityExtension
    {
        public static Guid GetUserId(this System.Security.Principal.IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(IdentityData.UserIdClaimName);
            if (claim is not null) return Guid.Parse(claim.Value);
            else throw new UnauthorizedAccessException();
        }
        public static Guid? GetOrganizationId(this System.Security.Principal.IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(IdentityData.OrganizationIdClaimName);
            Claim claimAdmin = claimsIdentity?.FindFirst(IdentityData.AdminUserClaimName);
            if (claim is not null) return Guid.Parse(claim.Value);
            else if (claimAdmin is not null && claimAdmin.Value == "true") return null;
            else throw new UnauthorizedAccessException();
        }
    }
}
