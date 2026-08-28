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

    private enum TestEnum
    {
        [EnumAlias("aliased-value", "alias")]
        Aliased = 1,

        [EnumMember(Value = "defined-value")]
        Defined = 2,

        Plain = 3,

        [EnumMember(Value = "member-value")]
        [EnumAlias("alias-value")]
        Both = 4
    }
}