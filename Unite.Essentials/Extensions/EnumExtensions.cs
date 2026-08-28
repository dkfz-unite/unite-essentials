using System.Reflection;
using System.Runtime.Serialization;

namespace Unite.Essentials.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Extracts EnumMember attribute value.
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <returns>EnumMember attribute value if not null. Otherwise Enum value string representation.</returns>
    public static string ToDefinitionString(this Enum value)
    {
        var type = value.GetType();

        var field = type.GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

        if (TryGetEnumMemberAttibuteValue(field, out var enumMemberAttributeValue))
            return enumMemberAttributeValue;
        else if (TryGetEnumAliasAttributeValue(field, out var enumAliasAttributeValue))
            return enumAliasAttributeValue;
        else
            return Convert.ChangeType(value, value.GetTypeCode()).ToString();
    }

    private static bool TryGetEnumMemberAttibuteValue(FieldInfo field, out string value)
    {
        var attribute = field?.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;

        value = !string.IsNullOrWhiteSpace(attribute?.Value) ? attribute.Value : null;

        return value != null;
    }

    private static bool TryGetEnumAliasAttributeValue(FieldInfo field, out string value)
    {
        var attribute = field?.GetCustomAttributes(typeof(Attributes.EnumAliasAttribute), false).FirstOrDefault() as Attributes.EnumAliasAttribute;

        value = !string.IsNullOrWhiteSpace(attribute?.Value) ? attribute.Value : null;

        return value != null;
    }
}
