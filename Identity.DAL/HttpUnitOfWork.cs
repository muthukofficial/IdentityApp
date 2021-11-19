using System;
using Microsoft.AspNetCore.Http;
using Identity.DAL.Core;

namespace Identity.DAL
{
    public class HttpUnitOfWork : UnitOfWork
    {
        public HttpUnitOfWork(AppDbContext context, IHttpContextAccessor httpAccessor) : base(context)
        {
            context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
        }
    }
}
