using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using UpWork.Common.Dto;
using UpWork.Common.Identity;

namespace UpWork.Api.Requirements.Handlers
{
    public class MatchOrganizationBodyHandler : AuthorizationHandler<MatchOrganizationBodyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MatchOrganizationBodyRequirement requirement)
        {
            var httpContext = (DefaultHttpContext)context.Resource;
            var request = httpContext.Request;

            string isAdmin = context.User.FindFirstValue(IdentityData.AdminUserClaimName);
            if (isAdmin == "true")
            {
                context.Succeed(requirement);
                return;
            }

            string orgIdToken = context.User.FindFirstValue(IdentityData.OrganizationIdClaimName);

            if (string.IsNullOrEmpty(orgIdToken))
            {
                context.Fail();
                return;
            }
            if (request.ContentType != "application/json")
            {
                context.Fail();
                return;
            }

            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            var organizationDto = JsonConvert.DeserializeObject<DefaultOrganizationAccessDto>(requestBody);

            var orgIdBody = organizationDto.OrganizationId;

            if (!orgIdBody.HasValue)
            {
                context.Fail();
                return;
            }

            if (orgIdBody.Value.ToString() == orgIdToken)
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }
    }
}
