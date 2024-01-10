using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Entities.Identity;
using WY.Extensions.EntityFrameworkCore.DatabaseDescription;

namespace WY.Application.Data.Identity;

public class ApplicationIdentityDbContext : ApplicationIdentityDbContext<ApplicationUser, ApplicationRole, IdentityKey, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
{
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.UseOpenIddict();
        builder.ConfigureDatabaseDescription();
    }
}
