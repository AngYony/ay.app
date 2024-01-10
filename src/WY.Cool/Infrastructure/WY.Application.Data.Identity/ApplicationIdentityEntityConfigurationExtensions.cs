using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Abstractions.Entities;
using WY.Domain.Entities.Identity.Abstractions;
using WY.Extensions.EntityFrameworkCore.Entities;

namespace WY.Application.Data.Identity;


public static class ApplicationIdentityEntityConfigurationExtensions
{
    /// <summary>
    /// 配置用户实体
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TUserRole">用户角色关系类型</typeparam>
    /// <typeparam name="TUserClaim">用户声明类型</typeparam>
    /// <typeparam name="TRoleClaim">角色声明类型</typeparam>
    /// <typeparam name="TUserLogin">用户登录信息类型</typeparam>
    /// <typeparam name="TUserToken">用户令牌类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerUser<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        (this EntityTypeBuilder<TUser> builder)
        where TKey : struct, IEquatable<TKey>
        where TUser : ApplicationUser<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        where TRole : ApplicationRole<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        where TUserRole : ApplicationUserRole<TKey, TUser, TRole>
        where TUserClaim : ApplicationUserClaim<TKey, TUser>
        where TRoleClaim : ApplicationRoleClaim<TKey, TUser, TRole>
        where TUserLogin : ApplicationUserLogin<TKey, TUser>
        where TUserToken : ApplicationUserToken<TKey, TUser>
    {
        builder.ConfigureQueryFilterForILogicallyDelete();
        builder.ConfigureForIOptimisticConcurrencySupported();

        // 每个用户可能有多个用户声明
        builder.HasMany(u => u.Claims)
            .WithOne(uc => uc.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        // 每个用户可能有多个登录方式
        builder.HasMany(u => u.Logins)
            .WithOne(ul => ul.User)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // 每个用户可能有多个用户令牌
        builder.HasMany(u => u.Tokens)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // 每个用户可能有多个角色
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<TUserRole>(
                ur => ur.HasOne(ur1 => ur1.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired(),
                ur => ur.HasOne(ur1 => ur1.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired(),
                ur => ur.HasKey(ur1 => new { ur1.UserId, ur1.RoleId }));

        builder.ToTable("WyUsers");
    }

    /// <summary>
    /// 配置角色实体
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TUserRole">用户角色关系类型</typeparam>
    /// <typeparam name="TUserClaim">用户声明类型</typeparam>
    /// <typeparam name="TRoleClaim">角色声明类型</typeparam>
    /// <typeparam name="TUserLogin">用户登录信息类型</typeparam>
    /// <typeparam name="TUserToken">用户令牌类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerRole<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        (this EntityTypeBuilder<TRole> builder)
        where TKey : struct, IEquatable<TKey>
        where TUser : ApplicationUser<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        where TRole : ApplicationRole<TKey, TUser, TRole, TUserRole, TUserClaim, TRoleClaim, TUserLogin, TUserToken>
        where TUserRole : ApplicationUserRole<TKey, TUser, TRole>
        where TUserClaim : ApplicationUserClaim<TKey, TUser>
        where TRoleClaim : ApplicationRoleClaim<TKey, TUser, TRole>
        where TUserLogin : ApplicationUserLogin<TKey, TUser>
        where TUserToken : ApplicationUserToken<TKey, TUser>
    {
        builder.ConfigureForIOptimisticConcurrencySupported();

        // 每个角色可能有多个用户，已经在用户中配置过，无需重复配置
        //builder.HasMany(u => u.Users)
        //    .WithMany(r => r.Roles)
        //    .UsingEntity<TUserRole>(
        //        ur => ur.HasOne(ur1 => ur1.User)
        //            .WithMany(r => r.UserRoles)
        //            .HasForeignKey(ur => ur.UserId)
        //            .IsRequired(),
        //        ur => ur.HasOne(ur1 => ur1.Role)
        //            .WithMany(u => u.UserRoles)
        //            .HasForeignKey(ur => ur.RoleId)
        //            .IsRequired(),
        //        ur => ur.HasKey(ur1 => new { ur1.UserId, ur1.RoleId }));

        // 每个角色可能有多个角色声明
        builder.HasMany(e => e.RoleClaims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();

        builder.ToTable("WyRoles");
    }

    /// <summary>
    /// 配置用户声明
    /// </summary>
    /// <typeparam name="TUserClaim">用户声明类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerUserClaim<TUserClaim, TUser, TRole, TKey>(
        this EntityTypeBuilder<TUserClaim> builder)
        where TKey : struct, IEquatable<TKey>
        where TUserClaim : ApplicationUserClaim<TKey, TUser>
        where TUser : class, IEntity<TKey>
        where TRole : IEntity<TKey>
    {
        builder.ToTable("WyUserClaims");
    }

    /// <summary>
    /// 配置用户角色关系
    /// </summary>
    /// <typeparam name="TUserRole">用户角色关系类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerUserRole<TUserRole, TUser, TRole, TKey>(
        this EntityTypeBuilder<TUserRole> builder)
        where TKey : struct, IEquatable<TKey>
        where TUserRole : ApplicationUserRole<TKey, TUser, TRole>
        where TUser : class, IEntity<TKey>
        where TRole : class, IEntity<TKey>
    {
        builder.ToTable("WyUserRoles");
    }

    /// <summary>
    /// 配置用户登录信息
    /// </summary>
    /// <typeparam name="TUserLogin">用户登录信息类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerUserLogin<TUserLogin, TUser, TKey>(
        this EntityTypeBuilder<TUserLogin> builder)
    where TKey : struct, IEquatable<TKey>
        where TUserLogin : ApplicationUserLogin<TKey, TUser>
        where TUser : class, IEntity<TKey>
    {
        builder.ToTable("WyUserLogins");
    }

    /// <summary>
    /// 配置角色声明
    /// </summary>
    /// <typeparam name="TRoleClaim">角色声明类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TRole">角色类型</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerRoleClaim<TRoleClaim, TUser, TRole, TKey>(
        this EntityTypeBuilder<TRoleClaim> builder)
        where TKey : struct, IEquatable<TKey>
        where TRoleClaim : ApplicationRoleClaim<TKey, TUser, TRole>
        where TUser : class, IEntity<TKey>
        where TRole : class, IEntity<TKey>
    {
        builder.ToTable("WyRoleClaims");
    }

    /// <summary>
    /// 配置用户令牌
    /// </summary>
    /// <typeparam name="TUserToken">用户令牌类型</typeparam>
    /// <typeparam name="TUser">用户类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <param name="builder">传入的实体构造器</param>
    public static void ConfiguerUserToken<TUserToken, TUser, TKey>(
        this EntityTypeBuilder<TUserToken> builder)
        where TKey : struct, IEquatable<TKey>
        where TUserToken : ApplicationUserToken<TKey, TUser>
        where TUser : class, IEntity<TKey>
    {
        builder.ToTable("WyUserTokens");
    }
}
