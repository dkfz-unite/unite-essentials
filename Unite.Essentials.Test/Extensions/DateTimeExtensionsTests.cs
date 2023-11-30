using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Extensions;

namespace Unite.Essentials.Tests.Extensions;

[TestClass]
public class DateTimeExtensionsTests
{
    [TestMethod("'RelativeFrom' should calculate relative day")]
    public void RelativeFrom_WithDates()
    {
        DateTime? anchorDate = new DateTime(2020, 01, 01, 17, 32, 26);
        DateTime? eventDate = new DateTime(2020, 01, 07, 09, 10, 31);
        int? eventDay = eventDate.RelativeFrom(anchorDate);

        Assert.AreEqual(6, eventDay);
    }

    [TestMethod("'RelativeFrom' should return null if any date is not set")]
    public void RelativeFrom_WithoutDates()
    {
        DateTime? anchorDate1 = new DateTime(2020, 01, 01, 17, 32, 26);
        DateTime? eventDate1 = null;
        var eventDay1 = eventDate1.RelativeFrom(anchorDate1);

        Assert.IsNull(eventDay1);


        DateTime? anchorDate2 = null;
        DateTime? eventDate2 = new DateTime(2020, 01, 07, 09, 10, 31);
        var eventDay2 = eventDate2.RelativeFrom(anchorDate2);

        Assert.IsNull(eventDay2);


        DateTime? anchorDate3 = null;
        DateTime? eventDate3 = null;
        var eventDay3 = eventDate3.RelativeFrom(anchorDate3);

        Assert.IsNull(eventDay3);
    }
}
