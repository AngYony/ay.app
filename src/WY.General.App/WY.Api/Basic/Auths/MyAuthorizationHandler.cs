using Microsoft.AspNetCore.Authorization;

namespace WY.Api.Authorizations
{
    public class MyAuthorizationHandler : AuthorizationHandler<MyAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MyAuthorizationRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
