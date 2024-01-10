using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationRole<TKey, TIdentityUser, TIdentityRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    : IdentityRole<TKey>
    , IEntity<TKey>
    , IOptimisticConcurrencySupported
    where TKey : struct, IEquatable<TKey>
    where TIdentityUser : ApplicationUser<TKey, TIdentityUser, TIdentityRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    where TIdentityRole : ApplicationRole<TKey, TIdentityUser, TIdentityRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    where TUserRole : ApplicationUserRole<TKey, TIdentityUser, TIdentityRole>
    where TUserClaim : ApplicationUserClaim<TKey, TIdentityUser>
    where TRoleClaim : ApplicationRoleClaim<TKey, TIdentityUser, TIdentityRole>
    where TUserLogin : ApplicationUserLogin<TKey, TIdentityUser>
    where TUserToken : ApplicationUserToken<TKey, TIdentityUser>
{
    public override TKey Id { get => base.Id; set => base.Id = value; }
    public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

    public virtual string? Description { get; set; }

    #region 导航属性

    public virtual List<TIdentityUser> Users { get; set; } = new List<TIdentityUser>();
    public virtual List<TUserRole> UserRoles { get; set; } = new List<TUserRole>();
    public virtual List<TRoleClaim> RoleClaims { get; set; } = new List<TRoleClaim>();

    #endregion 导航属性
}