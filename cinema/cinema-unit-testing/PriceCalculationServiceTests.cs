using System.Linq;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Stripe;
using Xunit;

namespace cinema_unit_testing;

public class PriceCalculationServiceTests
{
    [Fact]
    public void Discount_IsCalculated_whenUserSubmits()
    {
        Mock<IShowRepository> mock = new Mock<IShowRepository>();
      
        MovieService movieService = new MovieService(mock.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, mock.Object);

        double discount = priceCalculatingService.Discount(2, 2, 2);
        Assert.Equal(9.0, discount);
    }
}