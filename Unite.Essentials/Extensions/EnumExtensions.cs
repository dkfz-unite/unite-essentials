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
        var member = type.GetMember(value.ToString()).FirstOrDefault();
        var attribute = member?.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
        var attributeValue = attribute?.Value;
        var objectValue = Convert.ChangeType(value, value.GetTypeCode()).ToString();

        return attributeValue ?? objectValue;
    }
}
