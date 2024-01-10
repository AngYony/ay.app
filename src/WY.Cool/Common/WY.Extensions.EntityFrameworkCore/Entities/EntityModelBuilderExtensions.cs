using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using WY.Domain.Abstractions.Entities;
using WY.Utilities.TypeExtensions;

namespace WY.Extensions.EntityFrameworkCore.Entities;

public static class EntityModelBuilderExtensions
{
    /// <summary>
    /// 配置可软删除实体的查询过滤器让ef core自动添加查询条件过滤已被软删除的记录
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="builder">实体类型构造器</param>
    /// <returns>实体类型构造器</returns>
    public static EntityTypeBuilder<TEntity> ConfigureQueryFilterForILogicallyDelete<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ILogicallyDeletable
    {
        return builder.HasQueryFilter(e => e.IsDeleted == false);
    }

    /// <summary>
    /// 批量配置可软删除实体的查询过滤器让ef core自动添加查询条件过滤已被软删除的记录
    /// </summary>
    /// <param name="modelBuilder">模型构造器</param>
    /// <returns>模型构造器</returns>
    public static ModelBuilder ConfigureQueryFilterForILogicallyDelete(this ModelBuilder modelBuilder)
    {
        foreach (var entity
            in modelBuilder.Model.GetEntityTypes()
                .Where(e => e.ClrType.IsDerivedFrom<ILogicallyDeletable>()))
        {
            modelBuilder.Entity(entity.ClrType, b =>
            {
                var parameter = Expression.Parameter(entity.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ILogicallyDeletable.IsDeleted));
                var equal = Expression.Equal(property, Expression.Constant(false, typeof(bool)));
                var lambda = Expression.Lambda(equal, parameter);

                b.HasQueryFilter(lambda);
            });
        }

        return modelBuilder;
    }

    /// <summary>
    /// 配置乐观并发实体的并发检查字段
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="builder">实体类型构造器</param>
    /// <returns>属性构造器</returns>
    public static PropertyBuilder<string> ConfigureForIOptimisticConcurrencySupported<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IOptimisticConcurrencySupported
    {
        return builder.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
    }

    /// <summary>
    /// 批量配置乐观并发实体的并发检查字段
    /// </summary>
    /// <param name="modelBuilder">模型构造器</param>
    /// <returns>模型构造器</returns>
    public static ModelBuilder ConfigureForIOptimisticConcurrencySupported(this ModelBuilder modelBuilder)
    {
        foreach (var entity
            in modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.HasSharedClrType)
                .Where(e => e.ClrType.IsDerivedFrom<IOptimisticConcurrencySupported>()))
        {
            modelBuilder.Entity(entity.ClrType, b =>
            {
                b.Property(nameof(IOptimisticConcurrencySupported.ConcurrencyStamp))
                    .IsConcurrencyToken();
            });
        }

        return modelBuilder;
    }
}