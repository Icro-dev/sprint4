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
        var timeService = new TimeService().GetNameForDayOfWeek(2);
        Assert.Equal("Dinsdag", timeService);
    }
}