using Microsoft.AspNetCore.Identity;
using WY.Domain.Abstractions.Entities;

namespace WY.Domain.Entities.Identity.Abstractions;

public abstract class ApplicationUserLogin<TKey, TIdentityUser>
    : IdentityUserLogin<TKey>, IEntity
    where TKey : struct, IEquatable<TKey>
    where TIdentityUser : IEntity<TKey>
{
    public virtual TIdentityUser? User { get; set; }
}