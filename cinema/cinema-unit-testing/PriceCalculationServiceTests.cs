using System;
using System.Reflection;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using cinema;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class PriceCalculationServiceTests
{
    public static Show getOneShow(int lenght, bool threeD)
    {
        var movie = new Movie();
        movie.Name = "Avatar";
        movie.Length = lenght;

        var show = new Show();
        show.Id = 1;
        show.Break = true;
        show.Movie = movie;
        show.Room = 1;
        show.StartTime = DateTime.Now;
        show.ThreeD = threeD;
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
        var show = getOneShow(121,true);

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);


        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var totalprice = priceCalculatingService.pricePerTicket(1);

        Assert.Equal(12.5, totalprice);
    }
    
    
    [Fact]
    public void pricePerTicket_isCalculated_whenTreeDIsTFalseAndLenghtIsLessThen120()
    {
        var show = getOneShow(100,false);

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);


        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var totalprice = priceCalculatingService.pricePerTicket(1);

        Assert.Equal(8.5, totalprice);
    }

    [Fact]
    public void ticketCost_isEqualTo_pricePerTicketMultipliedByTicketQuantity()
    {
        
        var show = getOneShow(100,false);

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var ticketPrice = priceCalculatingService.pricePerTicket(1);
        var totalPrice = priceCalculatingService.ticketCost(10,1);
        Assert.Equal(85, totalPrice);
    }

    [Fact]
    public void premium_isEqualTo_amountOfPopcornOptions()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        Assert.Equal(25,priceCalculatingService.Premium(10));
    }
    
    
    [Fact]
    public void ArrangementCost_isEqualTo_ArrangementTypeChildParty()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        var arrangementCost = priceCalculatingService.ArrangementCost(Arrangements.kinderfeestje);
        Assert.Equal(5.0,arrangementCost);
    }
    
    
    [Fact]
    public void ArrangementCost_isEqualTo_ArrangementTypeVIP()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        var arrangementCost = priceCalculatingService.ArrangementCost(Arrangements.vip);
        Assert.Equal(4.0,arrangementCost);
    }
    
    [Fact]
    public void ArrangementCost_isEqualTo_ArrangementTypeNormal()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        var arrangementCost = priceCalculatingService.ArrangementCost(Arrangements.normale);
        Assert.Equal(0,arrangementCost);
    }
    
    [Fact]
    public void OrderCost_isEqualTo_totalOrderCost()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);
        PriceCalculatingService priceCalculatingService = new PriceCalculatingService(movieService, showRepo.Object);

        var orderCost = priceCalculatingService.OrderCost(10,20,100, 10);
        Assert.Equal(110,orderCost);
    }
}