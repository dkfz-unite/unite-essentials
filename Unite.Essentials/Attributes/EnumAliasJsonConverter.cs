using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Unite.Essentials.Attributes;

public sealed class EnumAliasJsonConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    private record AliasedFieldInfo(FieldInfo Field, EnumAliasAttribute Attribute)
    {
        public static AliasedFieldInfo Create(FieldInfo field)
        {
            var attribute = field?
                .GetCustomAttributes(typeof(EnumAliasAttribute), false)
                .OfType<EnumAliasAttribute>()
                .FirstOrDefault();

           return attribute != null ? new AliasedFieldInfo(field, attribute) : null;
        }
    }

    private const StringComparison _comparison = StringComparison.OrdinalIgnoreCase;
    private const BindingFlags _bindings = BindingFlags.Public | BindingFlags.Static;
    

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();

            if (value != null)
            {
                var aliases = GetAliasedFields();

                bool compare(string name) => string.Equals(name, value, _comparison);

                var canonicalAlias = aliases.FirstOrDefault(alias => compare(alias.Attribute.Value));
                if (canonicalAlias != null)
                    return (TEnum)canonicalAlias.Field.GetValue(null);

                var synonymAlias = aliases.FirstOrDefault(alias => alias.Attribute.Synonyms.Any(synonym => compare(synonym)));
                if (synonymAlias != null)
                    return (TEnum)synonymAlias.Field.GetValue(null)!;
            }
        }

        return GetDefaultConverter(options).Read(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        var alias = GetAliasedField(value.ToString());

        if (alias != null)
            writer.WriteStringValue(alias.Attribute.Value);
        else
            GetDefaultConverter(options).Write(writer, value, options);
    }

    private static AliasedFieldInfo[] GetAliasedFields()
    {
        var fields = typeof(TEnum).GetFields(_bindings);

        return fields
            .Select(AliasedFieldInfo.Create)
            .Where(info => info != null)
            .ToArray();
    }

    private static AliasedFieldInfo GetAliasedField(string name)
    {
        var field = typeof(TEnum).GetField(name, _bindings);

        return AliasedFieldInfo.Create(field);
    }
    
    private static JsonConverter<TEnum> GetDefaultConverter(JsonSerializerOptions options)
    {
        return (JsonConverter<TEnum>)new JsonStringEnumConverter<TEnum>()
            .CreateConverter(typeof(TEnum), options);
    }
}
