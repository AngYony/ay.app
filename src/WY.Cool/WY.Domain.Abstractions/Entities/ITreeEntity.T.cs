using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WY.Domain.Abstractions.Entities
{
    /// <summary>
    /// 树形实体接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface ITreeEntity<T> : IEntity, ITree<T>
    {
    }


    /// <summary>
    /// 树形实体接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface ITreeEntity<TKey, TEntity> : ITreeEntity<TEntity>, IEntity<TKey>
    where TKey : struct, IEquatable<TKey>
    where TEntity : ITreeEntity<TKey, TEntity>
    {
        /// <summary>
        /// 父节点Id
        /// </summary>
        TKey? ParentId { get; set; }
    }
}
