namespace Unite.Essentials.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumAliasAttribute : Attribute
{
    public string Value { get; }
    public string[] Synonyms { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumAliasAttribute"/> class.
    /// </summary>
    /// <param name="value">Canonical alias for the enum value.</param>
    /// <param name="synonyms">Alternative aliases for the enum value.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="synonyms"/> has any null or empty values.</exception>
    public EnumAliasAttribute(string value, params string[] synonyms)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (synonyms == null || synonyms.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentNullException(nameof(synonyms));

        Value = value;
        Synonyms = synonyms;
    }
}
