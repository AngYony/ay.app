using WY.Extensions.EntityFrameworkCore.DataAnnotations;

namespace WY.Domain.Entities.Identity;

/// <summary>
/// 性别
/// </summary>
[DatabaseDescription("性别枚举")]
public enum Gender : byte
{
    /// <summary>
    /// 保密
    /// </summary>
    [DatabaseDescription("保密")]
    Unknown = 0,
    /// <summary>
    /// 男
    /// </summary>
    [DatabaseDescription("男")]
    Male = 1,
    /// <summary>
    /// 女
    /// </summary>
    [DatabaseDescription("女")]
    Female = 2
}