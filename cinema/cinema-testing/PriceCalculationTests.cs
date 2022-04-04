using System;
using cinema.Data;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace cinema_testing;

public class PriceCalculationTests
{
    private IShowService _showService;
    private CinemaContext _context;
    private PriceCalculatingService _priceCalculatingService;
    private IMovieService _movieService;
    private Movie _movie;
    private Show _show;

    [SetUp]
    public void Setup()
    {
        var dbContextOptions =
            new DbContextOptionsBuilder<CinemaContext>().UseSqlServer(
                "Data Source=thuis.rijk.se;User ID=sa;Password=line-reason-riverbank-subpanel;Database=sebastiaan");
        _context = new CinemaContext(dbContextOptions.Options);
        _context.Database.EnsureCreated();

        
        _priceCalculatingService = new PriceCalculatingService(_context, _movieService);
        _showService = new ShowService(_context);
    }

    [Test]
    public void PriceDiscountCalculating()
    {
        double discount = _priceCalculatingService.Discount(2, 2, 2);
        Assert.AreEqual(9.0, discount);
    }
}