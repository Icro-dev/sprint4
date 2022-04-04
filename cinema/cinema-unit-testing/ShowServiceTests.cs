using System;
using System.Collections.Generic;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class ShowServiceTests
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

    
    public static List<Show> getShowList()
    {
        List<Show> showsList = new List<Show>();

        var movie = new Movie();
        movie.Name = "Avatar";
        movie.Length = 121;

        var show1 = new Show();
        show1.Id = 1;
        show1.Break = true;
        show1.Movie = movie;
        show1.Room = 1;
        show1.StartTime = DateTime.Now;
        show1.ThreeD = true;
        showsList.Add(show1);

        var show2 = new Show();
        show2.Id = 1;
        show2.Break = true;
        show2.Movie = movie;
        show2.Room = 1;
        show2.StartTime = DateTime.Now;
        show2.ThreeD = true;
        
        showsList.Add(show2);
        return showsList;
    }

    
    [Fact]
    public void getShowById_returns_show()
    {
        var show = getOneShow();

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        ShowService showService = new ShowService(showRepo.Object);


        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var showResult = showService.getShowById(1);
        
        Assert.NotNull(show);
        Assert.Equal(show, showResult);
    }
    
    [Fact]
    public void GetShowsPerMoviePerDay_returns_Dictionary()
    {
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        ShowService showService = new ShowService(showRepo.Object);
        var showList = getShowList();
        var shows = showService.GetShowsPerMoviePerDay(showList);
        
        Assert.NotNull(shows);
        Assert.IsType<Dictionary<DateOnly, Dictionary<Movie, List<Show>>>>(shows);
    }
}