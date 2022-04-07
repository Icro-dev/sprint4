using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using cinema.Models;
using cinema.Repositories;
using cinema.Controllers;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing;


public class MoviesControllerTests
{

    [Fact]
    public async Task IndexTest()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    public static Movie getOneMovie()
    {
        var movie = new Movie();
        movie.Cast = "test";
        movie.Description = "test";
        movie.Director = "test";
        movie.Genre = "test";
        movie.Kijkwijzer = "test";
        movie.Language = "test";
        movie.Length = 100;
        movie.Name = "Avatar";
        movie.Poster = "test";
        movie.ReleaseYear = 1999;
        movie.ThreeD = true;
        movie.CountryOfOrigin = "test";
        return movie;
    }
    
    public static Movie EditOneMovie()
    {
        var movie = new Movie();
        movie.Cast = "edit";
        movie.Description = "edit";
        movie.Director = "edit";
        movie.Genre = "edit";
        movie.Kijkwijzer = "edit";
        movie.Language = "edit";
        movie.Length = 100;
        movie.Name = "Avatar";
        movie.Poster = "edit";
        movie.ReleaseYear = 1999;
        movie.ThreeD = true;
        movie.CountryOfOrigin = "edit";
        return movie;
    }
    
    [Fact]
    public async Task Details()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var result = await controller.Details("Avatar");
        var nullResult = await controller.Details(null);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nullResult);
    }
    
    [Fact]
    public async Task Create()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);

        // Act
        var result = controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task CreateMovie()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();

        // Act
        var result = await controller.Create(movie);
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task CreateMovieModelState()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Create(movie);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task EditGet()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var result = await controller.Edit("Avatar");
        var nullResult = await controller.Edit(null);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nullResult);
    }
    
    [Fact]
    public async Task EditIdTest()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var result = await controller.Edit("test", movie);

        // Assert
        Assert.IsType(typeof (NotFoundResult), result);
    }
    
    [Fact]
    public async Task EditModelState()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = EditOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Edit("Avatar", movie);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task EditAdd()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = EditOneMovie();
        movieRepo.Setup(s => s.Add(movie));
        
        // Act
        var result = await controller.Edit("Avatar", movie);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditMovieExists()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = EditOneMovie();
        var movieName = "test";
        movieRepo.Setup(s => s.MovieExists(movieName));
        
        // Act
        var result = await controller.Edit("nonexistant", movie);

        // Assert
        Assert.IsType(typeof (NotFoundResult), result);
    }
    
    [Fact]
    public async Task EditSucces()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        var movieEdit = EditOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var result = await controller.Edit("Avatar", movieEdit);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditFail()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        
        // Act
        var result = await controller.Edit("Avatar");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public async Task DeleteGet()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var nullResult = await controller.Delete(null);
        var result = await controller.Delete("Avatar");
       
        // Assert
        Assert.IsType(typeof (NotFoundResult), nullResult);
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task DeleteConfirmed()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        
        // Act
        var result = await controller.DeleteConfirmed("Avatar");
        movieRepo.Setup(s => s.RemoveMovie(movie));
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task MovieExists()
    {
        // Arrange
        var movieRepo = new Mock<IMovieRepository>();
        var controller = new MoviesController(movieRepo.Object);
        var movie = getOneMovie();
        movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);
        
        // Act
        var result = controller.MovieExists("Avatar");
        
        // Assert
        Assert.IsType<bool>(result);
    }
}