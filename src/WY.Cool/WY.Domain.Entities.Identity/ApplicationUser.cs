using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Domain.Entities.Identity.Abstractions;
using WY.Extensions.EntityFrameworkCore.DataAnnotations;

namespace WY.Domain.Entities.Identity;

/// <summary>
/// 实际使用的用户类，添加自己的属性存储自定义信息
/// </summary>
public class ApplicationUser : ApplicationUser<IdentityKey, ApplicationUser, ApplicationRole, ApplicationUserRole, ApplicationUserClaim, ApplicationRoleClaim, ApplicationUserLogin, ApplicationUserToken>
{
    [DatabaseDescription("性别")]
    [EnumDataType(typeof(Gender))]
    public virtual Gender? Gender { get; set; }
}