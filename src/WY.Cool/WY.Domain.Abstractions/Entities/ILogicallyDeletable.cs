namespace WY.Domain.Abstractions.Entities
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ILogicallyDeletable
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool IsDeleted { get; set; }
    }
}