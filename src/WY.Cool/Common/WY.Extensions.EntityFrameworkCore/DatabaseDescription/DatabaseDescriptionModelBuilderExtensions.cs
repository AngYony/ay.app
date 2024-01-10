using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WY.Extensions.EntityFrameworkCore.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WY.Utilities.TypeExtensions;

namespace WY.Extensions.EntityFrameworkCore.DatabaseDescription;

public static class DatabaseDescriptionModelBuilderExtensions
{
    public const string DefaultDatabaseDescriptionAnnotationName = "DatabaseDescription";

    /// <summary>
    /// 配置数据库表和列说明
    /// </summary>
    /// <param name="modelBuilder">模型构造器</param>
    /// <returns>模型构造器</returns>
    public static ModelBuilder ConfigureDatabaseDescription(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //添加表说明
            if (entityType.FindAnnotation(DefaultDatabaseDescriptionAnnotationName) == null && entityType.ClrType?.CustomAttributes.Any(
                    attr => attr.AttributeType == typeof(DatabaseDescriptionAttribute)) == true)
            {
                entityType.AddAnnotation(DefaultDatabaseDescriptionAnnotationName,
                    (entityType.ClrType.GetCustomAttribute(typeof(DatabaseDescriptionAttribute)) as DatabaseDescriptionAttribute)?.Description);
            }

            //添加列说明
            foreach (var property in entityType.GetProperties())
            {
                if (property.FindAnnotation(DefaultDatabaseDescriptionAnnotationName) == null && property.PropertyInfo?.CustomAttributes
                        .Any(attr => attr.AttributeType == typeof(DatabaseDescriptionAttribute)) == true)
                {
                    var propertyInfo = property.PropertyInfo;
                    var propertyType = propertyInfo.PropertyType;
                    //如果该列的实体属性是枚举类型，把枚举的说明追加到列说明
                    var enumDbDescription = string.Empty;
                    if (propertyType.IsEnum
                        || propertyType.IsDerivedFrom(typeof(Nullable<>)) && propertyType.GenericTypeArguments[0].IsEnum)
                    {
                        var @enum = propertyType.IsDerivedFrom(typeof(Nullable<>))
                            ? propertyType.GenericTypeArguments[0]
                            : propertyType;

                        var descList = new List<string>();
                        foreach (var field in @enum?.GetFields() ?? new FieldInfo[0])
                        {
                            if (!field.IsSpecialName)
                            {
                                var desc = (field.GetCustomAttributes(typeof(DatabaseDescriptionAttribute), false)
                                    .FirstOrDefault() as DatabaseDescriptionAttribute)?.Description;
                                descList.Add(
                                    $@"{field.GetRawConstantValue()} : {(desc.IsNullOrWhiteSpace() ? field.Name : desc)}");
                            }
                        }

                        var isFlags = @enum?.GetCustomAttribute(typeof(FlagsAttribute)) != null;
                        var enumTypeDbDescription =
                            (@enum?.GetCustomAttributes(typeof(DatabaseDescriptionAttribute), false).FirstOrDefault() as
                                DatabaseDescriptionAttribute)?.Description;
                        enumTypeDbDescription += enumDbDescription + (isFlags ? " [是标志位枚举]" : string.Empty);
                        enumDbDescription =
                            $@"( {(enumTypeDbDescription.IsNullOrWhiteSpace() ? "" : $@"{enumTypeDbDescription}; ")}{string.Join("; ", descList)} )";
                    }

                    property.AddAnnotation(DefaultDatabaseDescriptionAnnotationName,
                        $@"{(propertyInfo.GetCustomAttribute(typeof(DatabaseDescriptionAttribute)) as DatabaseDescriptionAttribute)
                            ?.Description}{(enumDbDescription.IsNullOrWhiteSpace() ? "" : $@" {enumDbDescription}")}");
                }
            }
        }

        return modelBuilder;
    }
}
