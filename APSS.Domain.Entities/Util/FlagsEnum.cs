namespace APSS.Domain.Entities.Util;

public static class FlagsEnum
{
    /// <summary>
    /// Gets set flags' values of a flags enum
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum</typeparam>
    /// <param name="self"></param>
    /// <returns>Set flags' values</returns>
    public static IEnumerable<TEnum> GetSetValues<TEnum>(this TEnum self)
        where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>().Where(v => self.HasFlag(v));
    }

    /// <summary>
    /// Gets set flags' names of a flags enum
    /// </summary>
    /// <typeparam name="TEnum">The type of the enum</typeparam>
    /// <param name="self"></param>
    /// <returns>Set flags' names</returns>
    public static IEnumerable<string> GetSetNames<TEnum>(this TEnum self)
        where TEnum : struct, Enum
    {
        return self.GetSetValues().Select(v => Enum.GetName(v)!);
    }

    /// <summary>
    /// Gets display string of set enum values
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string ToSetFormattedString<TEnum>(this TEnum self)
        where TEnum : struct, Enum
    {
        return $"{{ {string.Join(", ", self.GetSetNames())} }}";
    }
}