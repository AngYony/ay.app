using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Entities.Identity.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WY.Application.Data.Identity;



/// <summary>
/// 身份数据上下文
/// </summary>
/// <typeparam name="TUser">用户类型</typeparam>
/// <typeparam name="TRole">角色类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TUserClaim">用户声明类型</typeparam>
/// <typeparam name="TUserRole">用户角色关系类型</typeparam>
/// <typeparam name="TUserLogin">用户登录信息类型</typeparam>
/// <typeparam name="TRoleClaim">角色声明类型</typeparam>
/// <typeparam name="TUserToken">用户令牌类型</typeparam>
public abstract class ApplicationIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    where TUser : ApplicationUser<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    where TRole : ApplicationRole<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
    where TKey : struct, IEquatable<TKey>
    where TUserClaim : ApplicationUserClaim<TKey, TUser>
    where TUserRole : ApplicationUserRole<TKey, TUser, TRole>
    where TUserLogin : ApplicationUserLogin<TKey, TUser>
    where TRoleClaim : ApplicationRoleClaim<TKey, TUser, TRole>
    where TUserToken : ApplicationUserToken<TKey, TUser>
{
    private const string _delMark = "!del";

    public ApplicationIdentityDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TUser>().ConfiguerUser<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>();
        builder.Entity<TRole>().ConfiguerRole<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>();
        builder.Entity<TUserClaim>().ConfiguerUserClaim<TUserClaim, TUser, TRole, TKey>();
        builder.Entity<TUserRole>().ConfiguerUserRole<TUserRole, TUser, TRole, TKey>();
        builder.Entity<TUserLogin>().ConfiguerUserLogin<TUserLogin, TUser, TKey>();
        builder.Entity<TRoleClaim>().ConfiguerRoleClaim<TRoleClaim, TUser, TRole, TKey>();
        builder.Entity<TUserToken>().ConfiguerUserToken<TUserToken, TUser, TKey>();
    }

    public override int SaveChanges()
    {
        return SaveChanges(true);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ProcessDetededUserToSoftDelete();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(true, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ProcessDetededUserToSoftDelete();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 把删除用户处理成软删除
    /// </summary>
    private void ProcessDetededUserToSoftDelete()
    {
        var users = ChangeTracker.Entries<TUser>()
            .Where(u => u.State is EntityState.Deleted);

        foreach (var user in users)
        {
            user.State = EntityState.Modified;
            var entity = user.Entity;

            entity.IsDeleted = true;

            entity.Email += _delMark;
            entity.NormalizedEmail += _delMark;
            entity.UserName += _delMark;
            entity.NormalizedUserName += _delMark;
        }
    }
}