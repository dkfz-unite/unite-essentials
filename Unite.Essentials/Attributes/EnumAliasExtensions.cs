using System.Reflection;

namespace Unite.Essentials.Attributes;

public static class EnumAliasExtensions
{
    /// <summary>
    /// Extracts EnumAlias attribute value.
    /// </summary>
    /// <param name="value">Enum value.</param>
    /// <returns>EnumAlias attribute value if not null. Otherwise Enum value string representation.</returns>
    public static string ToAliasString(this Enum value)
    {
        var type = value.GetType();

        var field = type.GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

        var attribute = field?
            .GetCustomAttributes(typeof(EnumAliasAttribute), false)
            .OfType<EnumAliasAttribute>()
            .FirstOrDefault();

        return attribute?.Name ?? value.ToString();
        
    }
}
