using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class TimeServiceTest
{
    [Fact]
    public void GetNameForDayOfWeek()
    {
        var timeService0 = new TimeService().GetNameForDayOfWeek(0);
        Assert.Equal("Zondag", timeService0);
        var timeService1 = new TimeService().GetNameForDayOfWeek(1);
        Assert.Equal("Maandag", timeService1);
        var timeService2 = new TimeService().GetNameForDayOfWeek(2);
        Assert.Equal("Dinsdag", timeService2);
        var timeService3 = new TimeService().GetNameForDayOfWeek(3);
        Assert.Equal("Woensdag", timeService3);
        var timeService4 = new TimeService().GetNameForDayOfWeek(4);
        Assert.Equal("Donderdag", timeService4);
        var timeService5 = new TimeService().GetNameForDayOfWeek(5);
        Assert.Equal("Vrijdag", timeService5);
        var timeService6 = new TimeService().GetNameForDayOfWeek(6);
        Assert.Equal("Zaterdag", timeService6);
        var timeService7 = new TimeService().GetNameForDayOfWeek(7);
        Assert.Equal("NODAY", timeService7);
    }
}