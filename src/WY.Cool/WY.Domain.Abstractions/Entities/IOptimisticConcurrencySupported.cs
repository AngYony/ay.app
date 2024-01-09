using System.ComponentModel.DataAnnotations;

namespace WY.Domain.Abstractions.Entities
{
    /// <summary>
    /// 乐观并发接口
    /// </summary>
    public interface IOptimisticConcurrencySupported
    {
        /// <summary>
        /// 行版本，乐观并发锁
        /// </summary>
        [ConcurrencyCheck]
        string ConcurrencyStamp { get; set; }
    }
}