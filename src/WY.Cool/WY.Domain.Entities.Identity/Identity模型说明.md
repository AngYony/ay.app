## Identity 模型说明

详细说明请参考：([ASP.NET Core 中的 Identity 模型自定义 | Microsoft Learn](https://learn.microsoft.com/zh-cn/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-8.0))。

### 实体类型

Identity 模型包含以下实体类型。

展开表

| 实体类型    | 说明                             |
| :---------- | :------------------------------- |
| `User`      | 表示用户。                       |
| `Role`      | 表示一个角色。                   |
| `UserClaim` | 表示用户拥有的声明。             |
| `UserToken` | 表示用户的身份验证令牌。         |
| `UserLogin` | 将用户与登录名相关联。           |
| `RoleClaim` | 表示向角色中所有用户授予的声明。 |
| `UserRole`  | 关联用户和角色的联接实体。       |



### 实体类型关系

实体类型通过以下方式相互相关：

- 每个 `User` 可以有多个 `UserClaims`。
- 每个 `User` 可以有多个 `UserLogins`。
- 每个 `User` 可以有多个 `UserTokens`。
- 每个 `Role` 可以有多个关联的`RoleClaims`。
- 每个 `User` 可以有多个关联的 `Roles`，并且每个 `Role` 可以与多个 `Users` 关联。 这是一种多对多关系，需要数据库中的联接表。 联接表由 `UserRole` 实体表示。



### 模型泛型类型

Identity 为上面列出的每种实体类型定义默认[公共语言运行时](https://learn.microsoft.com/zh-cn/dotnet/standard/glossary#clr) (CLR) 类型。 这些类型都带有前缀 *Identity*：

- `IdentityUser`
- `IdentityRole`
- `IdentityUserClaim`
- `IdentityUserToken`
- `IdentityUserLogin`
- `IdentityRoleClaim`
- `IdentityUserRole`

可以将这些类型用作应用自己类型的基类，而不是直接使用这些类型。 Identity 定义的 `DbContext` 类是泛型类，因此，不同的 CLR 类型可用于模型中的一个或多个实体类型。 这些泛型类型还允许更改 `User` 主键 (PK) 数据类型。