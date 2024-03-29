﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unite.Essentials.Extensions;

namespace Unite.Essentials.Tests.Extensions;

[TestClass]
public class DateTimeExtensionsTests
{
    [TestMethod("'RelativeFrom' should calculate relative day")]
    public void RelativeFrom_WithDates()
    {
        DateTime? anchorDate = new DateTime(2020, 01, 30, 17, 30, 30);
        DateTime? eventDate1 = new DateTime(2020, 02, 05, 18, 10, 10);
        DateTime? eventDate2 = new DateTime(2020, 01, 30, 18, 30, 30);
        int? eventDay1 = eventDate1.RelativeFrom(anchorDate);
        int? eventDay2 = eventDate2.RelativeFrom(anchorDate);

        Assert.AreEqual(7, eventDay1);
        Assert.AreEqual(1, eventDay2);
    }

    [TestMethod("'RelativeFrom' should return null if any date is not set")]
    public void RelativeFrom_WithoutDates()
    {
        DateTime? anchorDate1 = new DateTime(2020, 01, 30, 17, 30, 30);
        DateTime? eventDate1a = null;
        var eventDay1a = eventDate1a.RelativeFrom(anchorDate1);
        var eventDay1b = eventDate1a?.RelativeFrom(anchorDate1);

        Assert.IsNull(eventDay1a);
        Assert.IsNull(eventDay1b);


        DateTime? anchorDate2 = null;
        DateTime? eventDate2 = new DateTime(2020, 02, 05, 18, 10, 10);
        var eventDay2 = eventDate2.RelativeFrom(anchorDate2);

        Assert.IsNull(eventDay2);


        DateTime? anchorDate3 = null;
        DateTime? eventDate3 = null;
        var eventDay3 = eventDate3.RelativeFrom(anchorDate3);

        Assert.IsNull(eventDay3);
    }
}
