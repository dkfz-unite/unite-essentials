using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Extensions;

namespace Unite.Essentials.Test.Extensions;

[TestClass]
public class EnumerableExtensionsTests
{
    [TestMethod("'IsEmpty' should return 'true' if collection is null or empty")]
    public void IsEmpty_WithEmptyCollection()
    {
        int[] a = [];
        int[] b = null;

        Assert.IsTrue(a.IsEmpty());
        Assert.IsTrue(b.IsEmpty());
    }

    [TestMethod("'IsEmpty' should return 'false' if collection is not empty")]
    public void IsEmpty_WithNotEmptyCollection()
    {
        int[] a = [1, 2, 3];

        Assert.IsFalse(a.IsEmpty());
    }

    [TestMethod("'IsNotEpmty' should return 'false' if collection is null or empty")]
    public void IsNotEmpty_WithEmptyCollection()
    {
        int[] a = [];
        int[] b = null;

        Assert.IsFalse(a.IsNotEmpty());
        Assert.IsFalse(b.IsNotEmpty());
    }

    [TestMethod("'IsNotEmpty' should return 'true' if collection is not empty")]
    public void IsNotEmpty_WithNotEmptyCollection()
    {
        int[] a = [1, 2, 3];

        Assert.IsTrue(a.IsNotEmpty());
    }

    [TestMethod("'ToArrayOrNull' should return an array if collection is not empty")]
    public void ToArrayOrNull_WithNotEmptyCollection()
    {
        int[] a = [1, 2, 3];

        Assert.IsNotNull(a.ToArrayOrNull());
        Assert.AreEqual(1, a[0]);
        Assert.AreEqual(2, a[1]);
        Assert.AreEqual(3, a[2]);
    }

    [TestMethod("'ToArrayOrNull' should return null if collection is empty")]
    public void ToArrayOrNull_WithEmptyCollection()
    {
        int[] a = [];
        int[] b = null;

        Assert.IsNull(a.ToArrayOrNull());
        Assert.IsNull(b.ToArrayOrNull());
    }
}
