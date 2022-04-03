using System;
using System.Reflection;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class PriceCalculationServiceTests
{
    public static Show getOneShow()
    {
        var movie = new Movie();
        movie.Name = "Avatar";
        movie.Length = 121;

        var show = new Show();
        show.Id = 1;
        show.Break = true;
        show.Movie = movie;
        show.Room = 1;
        show.StartTime = DateTime.Now;
        show.ThreeD = true;
        return show;
    }

    [Fact]
    public void Discount_IsCalculated_whenUserSubmits()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();

        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        double discount = priceCalculatingService.Discount(2, 2, 2);
        Assert.Equal(9.0, discount);
    }

    [Fact]
    public void pricePerTicket_isCalculated_whenTreeDIsTrueAndLenghtIsMoreThen120()
    {
        var show = getOneShow();

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);


        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var totalprice = priceCalculatingService.pricePerTicket(1);

        Assert.Equal(12.5, totalprice);
    }
}