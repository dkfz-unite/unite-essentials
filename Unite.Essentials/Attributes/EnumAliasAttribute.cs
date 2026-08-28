namespace Unite.Essentials.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class EnumAliasAttribute : Attribute
{
    public string Name { get; }
    public string[] Synonyms { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumAliasAttribute"/> class.
    /// </summary>
    /// <param name="name">Canonical name for the enum value.</param>
    /// <param name="synonyms">Alternative names for the enum value.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="synonyms"/> has any null or empty values.</exception>
    public EnumAliasAttribute(string name, params string[] synonyms)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if (synonyms == null || synonyms.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentNullException(nameof(synonyms));

        Name = name;
        Synonyms = synonyms;
    }
}
