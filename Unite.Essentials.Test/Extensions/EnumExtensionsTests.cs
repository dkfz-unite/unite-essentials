using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Attributes;
using Unite.Essentials.Extensions;

namespace Unite.Essentials.Test.Extensions;

[TestClass]
public class EnumExtensionsTests
{
    [TestMethod]
    public void ToAliasString_WithAlias_ReturnsCanonicalAlias()
    {
        var result = TestEnum.Aliased.ToAliasString();

        Assert.AreEqual("aliased-value", result);
    }

    [TestMethod]
    public void ToAliasString_WithoutAlias_ReturnsEnumName()
    {
        var result = TestEnum.Plain.ToAliasString();

        Assert.AreEqual(nameof(TestEnum.Plain), result);
    }

    [TestMethod]
    public void ToAliasString_WithUndefinedValue_ReturnsNumericValue()
    {
        var result = ((TestEnum)99).ToAliasString();

        Assert.AreEqual("99", result);
    }

    [DataTestMethod]
    [DataRow("aliased-value", TestEnum.Aliased)]
    [DataRow("ALIASED-VALUE", TestEnum.Aliased)]
    [DataRow("alias", TestEnum.Aliased)]
    [DataRow("ALIAS", TestEnum.Aliased)]
    [DataRow(" alias ", TestEnum.Aliased)]
    [DataRow("Plain", TestEnum.Plain)]
    [DataRow("plain", TestEnum.Plain)]
    [DataRow("Defined", TestEnum.Defined)]
    public void FromAliasString_WithAliasSynonymOrName_ReturnsEnumValue(string value, TestEnum expected)
    {
        var result = value.FromAliasString<TestEnum>();

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void FromAliasString_WithAliasMatchingAnotherSynonym_PrefersCanonicalAlias()
    {
        var result = "alias-value".FromAliasString<TestEnum>();

        Assert.AreEqual(TestEnum.Both, result);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("unknown")]
    [DataRow("1")]
    [DataRow("99")]
    [DataRow("Aliased, Both")]
    public void FromAliasString_WithInvalidValue_ThrowsFormatException(string value)
    {
        Assert.ThrowsException<FormatException>(() => value.FromAliasString<TestEnum>());
    }

    [TestMethod]
    public void FromAliasString_WithNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ((string)null).FromAliasString<TestEnum>());
    }

    [TestMethod]
    public void ToDefinitionString_WithEnumMember_ReturnsEnumMemberValue()
    {
        var result = TestEnum.Defined.ToDefinitionString();

        Assert.AreEqual("defined-value", result);
    }

    [TestMethod]
    public void ToDefinitionString_WithEnumMemberAndAlias_PrefersEnumMemberValue()
    {
        var result = TestEnum.Both.ToDefinitionString();

        Assert.AreEqual("member-value", result);
    }

    [TestMethod]
    public void ToDefinitionString_WithAlias_ReturnsCanonicalAlias()
    {
        var result = TestEnum.Aliased.ToDefinitionString();

        Assert.AreEqual("aliased-value", result);
    }

    [TestMethod]
    public void ToDefinitionString_WithoutAttribute_ReturnsNumericValue()
    {
        var result = TestEnum.Plain.ToDefinitionString();

        Assert.AreEqual("3", result);
    }

    [TestMethod]
    public void ToDefinitionString_WithUndefinedValue_ReturnsNumericValue()
    {
        var result = ((TestEnum)99).ToDefinitionString();

        Assert.AreEqual("99", result);
    }

    public enum TestEnum
    {
        [EnumAlias("aliased-value", "alias", "alias-value")]
        Aliased = 1,

        [EnumMember(Value = "defined-value")]
        Defined = 2,

        Plain = 3,

        [EnumMember(Value = "member-value")]
        [EnumAlias("alias-value")]
        Both = 4
    }
}
