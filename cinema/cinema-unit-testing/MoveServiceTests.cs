using System;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class MoveServiceTests
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
    public void GetMovieFromShow_returns_MovieFromTheRightShow()
    {
        var show = getOneShow();

        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        MovieService movieService = new MovieService(showRepo.Object);


        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(
            show
        );

        var movie = movieService.GetMovieFromShow(1);
        
        Assert.NotNull(movie);
        Assert.Equal(show.Movie, movie);
    }
}