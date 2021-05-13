using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JdShops.Entities;
using Microsoft.AspNetCore.Authorization;

namespace JdShops.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Tickets>

    {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        ResourceOperationRequirement requirement,
        Tickets ticket)
    {
        if (requirement.ResourceOperation == ResourceOperation.Create)
        {
            context.Succeed(requirement);
        }

        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
        if (ticket.UserId == int.Parse(userId))
        {
            context.Succeed(requirement);
        }

        if (userRole == "AdvancedUser")
        {
            context.Succeed(requirement);
        }

        if (userRole == "Admin")
        {
            context.Succeed(requirement);
        }


        return Task.CompletedTask;
    }
    }
}
