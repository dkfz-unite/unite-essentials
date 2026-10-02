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

        return attribute?.Value ?? value.ToString();
    }

    /// <summary>
    /// Parses an enum alias, synonym or field name, ignoring case.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="value">Enum alias, synonym or field name.</param>
    /// <returns>Matching enum value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    /// <exception cref="FormatException">Thrown when <paramref name="value"/> does not match an enum value.</exception>
    public static TEnum FromAliasString<TEnum>(this string value) where TEnum : struct, Enum
    {
        ArgumentNullException.ThrowIfNull(value);

        var option = value.Trim();
        var fields = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static);
        var aliases = fields
            .Select(field => new { Field = field, Attribute = field.GetCustomAttribute<EnumAliasAttribute>() })
            .Where(alias => alias.Attribute != null)
            .ToArray();

        bool compare(string name) => string.Equals(name, option, StringComparison.OrdinalIgnoreCase);

        var field = aliases.FirstOrDefault(alias => compare(alias.Attribute.Value))?.Field
            ?? aliases.FirstOrDefault(alias => alias.Attribute.Synonyms.Any(compare))?.Field
            ?? fields.FirstOrDefault(field => compare(field.Name));

        if (field == null)
            throw new FormatException($"'{value}' is not a valid alias or name for {typeof(TEnum).Name}.");

        return (TEnum)field.GetValue(null);
    }
}
