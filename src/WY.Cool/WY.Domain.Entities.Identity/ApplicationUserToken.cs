using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Entities.Identity.Abstractions;

namespace WY.Domain.Entities.Identity;

public class ApplicationUserToken : ApplicationUserToken<IdentityKey, ApplicationUser>
{
}
