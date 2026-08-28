using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Attributes;

namespace Unite.Essentials.Test.Converters;

[TestClass]
public class EnumAliasJsonConverterTests
{
    [DataTestMethod]
    [DataRow("alpha", TestEnum.First)]
    [DataRow("ALTERNATE", TestEnum.First)]
    [DataRow("second", TestEnum.Second)]
    [DataRow("beta", TestEnum.Second)]
    public void Read_WithCanonicalNameOrSynonym_ReturnsAliasedValue(string jsonValue, TestEnum expected)
    {
        var result = JsonSerializer.Deserialize<TestEnum>($"\"{jsonValue}\"");

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Read_WithoutAlias_ReturnsValueUsingDefaultEnumConversion()
    {
        var result = JsonSerializer.Deserialize<TestEnum>("\"Plain\"");

        Assert.AreEqual(TestEnum.Plain, result);
    }

    [TestMethod]
    public void Write_WithAlias_WritesCanonicalName()
    {
        var result = JsonSerializer.Serialize(TestEnum.First);

        Assert.AreEqual("\"alpha\"", result);
    }

    [TestMethod]
    public void Write_WithoutAlias_UsesDefaultEnumConversion()
    {
        var result = JsonSerializer.Serialize(TestEnum.Plain);

        Assert.AreEqual("\"Plain\"", result);
    }

    [JsonConverter(typeof(EnumAliasJsonConverter<TestEnum>))]
    public enum TestEnum
    {
        [EnumAlias("alpha", "alternate", "beta")]
        First,

        [EnumAlias("beta", "second")]
        Second,

        Plain
    }
}