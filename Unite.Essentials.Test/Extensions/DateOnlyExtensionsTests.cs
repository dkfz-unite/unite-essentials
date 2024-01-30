using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Extensions;

namespace Unite.Essentials.Test.Extensions;

[TestClass]
public class DateOnlyExtensionsTests
{
    [TestMethod("'RelativeFrom' should calculate relative day")]
    public void RelativeFrom_WithDates()
    {
        DateOnly? anchorDate = new DateOnly(2020, 01, 30);
        DateOnly? eventDate1 = new DateOnly(2020, 02, 05);
        DateOnly? eventDate2 = new DateOnly(2020, 01, 30);
        int? eventDay1 = eventDate1.RelativeFrom(anchorDate);
        int? eventDay2 = eventDate2.RelativeFrom(anchorDate);

        Assert.AreEqual(7, eventDay1);
        Assert.AreEqual(1, eventDay2);
    }

    [TestMethod("'RelativeFrom' should return null if any date is not set")]
    public void RelativeFrom_WithoutDates()
    {
        DateOnly? anchorDate1 = new DateOnly(2020, 01, 30);
        DateOnly? eventDate1a = null;
        var eventDay1a = eventDate1a.RelativeFrom(anchorDate1);
        var eventDay1b = eventDate1a?.RelativeFrom(anchorDate1);

        Assert.IsNull(eventDay1a);
        Assert.IsNull(eventDay1b);


        DateOnly? anchorDate2 = null;
        DateOnly? eventDate2 = new DateOnly(2020, 02, 05);
        var eventDay2 = eventDate2.RelativeFrom(anchorDate2);

        Assert.IsNull(eventDay2);


        DateOnly? anchorDate3 = null;
        DateOnly? eventDate3 = null;
        var eventDay3 = eventDate3.RelativeFrom(anchorDate3);

        Assert.IsNull(eventDay3);
    }
}
