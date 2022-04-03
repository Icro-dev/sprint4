using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class PriceCalculationServiceTests
{
    [Fact]
    public void Discount_IsCalculated_whenUserSubmits()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
      
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        double discount = priceCalculatingService.Discount(2, 2, 2);
        Assert.Equal(9.0, discount);
    }
}