using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UpWork.Common.Enums;
using UpWork.Common.Identity;

namespace UpWork.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly PermissionType _claimValue;

        public RequireClaimAttribute(string claimName, PermissionType claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_claimName, _claimValue.ToString())
                && !context.HttpContext.User.HasClaim(IdentityData.AdminUserClaimName, "true")
                && !context.HttpContext.User.HasClaim(IdentityData.OwnerUserClaimName, "true"))
                context.Result = new ForbidResult();
        }
    }
}
