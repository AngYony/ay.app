using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationRoleClaim<TIdentityKey, TIdentityUser, TIdentityRole>
    : IdentityRoleClaim<TIdentityKey>, IEntity<int>
    where TIdentityKey : struct, IEquatable<TIdentityKey>
    where TIdentityUser : IEntity<TIdentityKey>
    where TIdentityRole : IEntity<TIdentityKey>
{
    public override int Id { get => base.Id; set => base.Id = value; }

    public virtual TIdentityRole? Role { get; set; }
}