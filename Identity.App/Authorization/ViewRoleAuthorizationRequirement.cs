using System;
using System.Threading.Tasks;
using Identity.DAL.Core;
using Microsoft.AspNetCore.Authorization;

namespace Identity.App.Authorization
{
    public class ViewRoleAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class ViewRoleAuthorizationHandler : AuthorizationHandler<ViewRoleAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRoleAuthorizationRequirement requirement, string roleName)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(ClaimConstants.Permission, AppPermissions.ViewRoles) || context.User.IsInRole(roleName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
