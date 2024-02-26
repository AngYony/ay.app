using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WY.Api.Controllers.Account
{
    public class LoginController : BaseController
    {
        public async Task Login(string username, string password)
        {
            ClaimsIdentity claimsIdentity = new("Ctm");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, username));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
        }
    }
}
