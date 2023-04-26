using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UpWork.Common.Identity;

namespace UpWork.Api.Requirements.Handlers
{
    public class MatchOrganizationQueryHandler : AuthorizationHandler<MatchOrganizationQueryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MatchOrganizationQueryRequirement requirement)
        {
            var httpContext = (DefaultHttpContext)context.Resource;
            var request = httpContext.Request;

            string isAdmin = context.User.FindFirstValue(IdentityData.AdminUserClaimName);
            if (isAdmin == "true")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            string orgIdToken = context.User.FindFirstValue(IdentityData.OrganizationIdClaimName);

            if (string.IsNullOrEmpty(orgIdToken))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var orgIdQuery = request.Query[IdentityData.OrganizationIdClaimName].ToString();

            if (string.IsNullOrEmpty(orgIdQuery))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (orgIdQuery == orgIdToken)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
