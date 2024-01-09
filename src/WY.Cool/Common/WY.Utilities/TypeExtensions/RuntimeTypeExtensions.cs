namespace WY.Utilities.TypeExtensions;

public static class RuntimeTypeExtensions
{
    /// <summary>
    /// 判断 <paramref name="type"/> 指定的类型是否派生自 <typeparamref name="T"/> 类型，或实现了 <typeparamref name="T"/> 接口
    /// </summary>
    /// <typeparam name="T">要匹配的类型</typeparam>
    /// <param name="type">需要测试的类型</param>
    /// <returns>如果 <paramref name="type"/> 指定的类型派生自 <typeparamref name="T"/> 类型，或实现了 <typeparamref name="T"/> 接口，则返回 <see langword="true"/>，否则返回 <see langword="false"/>。</returns>
    public static bool IsDerivedFrom<T>(this Type type)
    {
        return IsDerivedFrom(type, typeof(T));
    }

    /// <summary>
    /// 判断 <paramref name="type"/> 指定的类型是否继承自 <paramref name="pattern"/> 指定的类型，或实现了 <paramref name="pattern"/> 指定的接口
    /// <para>支持开放式泛型，如<see cref="List{T}" /></para>
    /// </summary>
    /// <param name="type">需要测试的类型</param>
    /// <param name="pattern">要匹配的类型，如 <c>typeof(int)</c>，<c>typeof(IEnumerable)</c>，<c>typeof(List&lt;&gt;)</c>，<c>typeof(List&lt;int&gt;)</c>，<c>typeof(IDictionary&lt;,&gt;)</c></param>
    /// <returns>如果 <paramref name="type"/> 指定的类型继承自 <paramref name="pattern"/> 指定的类型，或实现了 <paramref name="pattern"/> 指定的接口，则返回 <see langword="true"/>，否则返回 <see langword="false"/>。</returns>
    public static bool IsDerivedFrom(this Type type, Type pattern)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        if (pattern == null) throw new ArgumentNullException(nameof(pattern));

        // 测试非泛型类型（如ArrayList）或确定类型参数的泛型类型（如List<int>，类型参数T已经确定为int）
        if (type.IsSubclassOf(pattern)) return true;

        // 测试非泛型接口（如IEnumerable）或确定类型参数的泛型接口（如IEnumerable<int>，类型参数T已经确定为int）
        if (pattern.IsAssignableFrom(type)) return true;

        // 测试泛型接口（如IEnumerable<>，IDictionary<,>，未知类型参数，留空）
        var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
        if (isTheRawGenericType) return true;

        // 测试泛型类型（如List<>，Dictionary<,>，未知类型参数，留空）
        while (type != null && type != typeof(object))
        {
            isTheRawGenericType = IsTheRawGenericType(type);
            if (isTheRawGenericType) return true;
            type = type.BaseType!;
        }

        // 没有找到任何匹配的接口或类型。
        return false;

        // 测试某个类型是否是指定的原始接口。
        bool IsTheRawGenericType(Type test)
            => pattern == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
    }
}