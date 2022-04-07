using cinema.Services;
using Xunit;

namespace cinema_unit_testing;

public class PaymentAdapterTests
{
    [Fact]
    public void verifyPayment_returns_true()
    {
        var verifyPayment = new PaymentAdapter();
        Assert.True(verifyPayment.verifyPayment(10));
    }
}