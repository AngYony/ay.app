using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationUser<TKey, TIdentityUser, TIdentityRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    : IdentityUser<TKey>
    , IEntity<TKey>
    , ILogicallyDeletable
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

    public virtual bool IsDeleted { get; set; }

    #region 导航属性

    public virtual List<TIdentityRole> Roles { get; set; } = new List<TIdentityRole>();
    public virtual List<TUserRole> UserRoles { get; set; } = new List<TUserRole>();
    public virtual List<TUserClaim> Claims { get; set; } = new List<TUserClaim>();
    public virtual List<TUserLogin> Logins { get; set; } = new List<TUserLogin>();
    public virtual List<TUserToken> Tokens { get; set; } = new List<TUserToken>();

    #endregion 导航属性
}