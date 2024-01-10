using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationUserToken<TIdentityKey, TIdentityUser>
    : IdentityUserToken<TIdentityKey>, IEntity
    where TIdentityKey : struct, IEquatable<TIdentityKey>
    where TIdentityUser : IEntity<TIdentityKey>
{
    public virtual TIdentityUser? User { get; set; }
}