using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationUserClaim<TIdentityKey, TIdentityUser>
    : IdentityUserClaim<TIdentityKey>, IEntity<int>
    where TIdentityKey : struct, IEquatable<TIdentityKey>
    where TIdentityUser : IEntity<TIdentityKey>
{
    public override int Id { get => base.Id; set => base.Id = value; }

    public virtual TIdentityUser? User { get; set; }
}