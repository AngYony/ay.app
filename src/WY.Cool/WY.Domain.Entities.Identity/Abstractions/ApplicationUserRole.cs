using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationUserRole<TIdentityKey, TIdentityUser, TIdentityRole>
    : IdentityUserRole<TIdentityKey>
    where TIdentityKey : struct, IEquatable<TIdentityKey>
    where TIdentityUser : IEntity<TIdentityKey>
    where TIdentityRole : IEntity<TIdentityKey>
{
    public virtual TIdentityUser? User { get; set; }
    public virtual TIdentityRole? Role { get; set; }
}