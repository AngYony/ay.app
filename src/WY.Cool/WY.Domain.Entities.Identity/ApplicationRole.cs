using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WY.Domain.Entities.Identity.Abstractions;

namespace WY.Domain.Entities.Identity;

public class ApplicationRole : ApplicationRole<IdentityKey, ApplicationUser, ApplicationRole, ApplicationUserRole, ApplicationUserClaim, ApplicationRoleClaim, ApplicationUserLogin, ApplicationUserToken>
{
    public ApplicationRole() { }
    public ApplicationRole(string roleName) => Name = roleName;
}