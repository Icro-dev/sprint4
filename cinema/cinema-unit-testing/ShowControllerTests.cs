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
using cinema.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class ShowControllerTests
{
    
    [Fact]
    public async Task IndexTest()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
      public static Show getOneShow()
    {
        var show = new Show();
        show.Id = 1;
        show.Movie = new Movie();
        show.Break = false;
        show.Room = 1;
        show.StartTime = new DateTime(2022, 4, 7, 19, 00, 00);
        show.ThreeD = true;
        return show;
    }
    
    public static Show EditOneShow()
    {
        var show = new Show();
        show.Id = 1;
        show.Movie = new Movie();
        show.Break = false;
        show.Room = 1;
        show.StartTime = new DateTime(2022, 4, 7, 19, 00, 00);
        show.ThreeD = false;
        return show;
    }
    
    [Fact]
    public async Task Details()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = await controller.Details(1);
        var nonExistentRoom = await controller.Details(20);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nonExistentRoom);
    }
    
    [Fact]
    public async Task Create()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        ShowService showService = new ShowService(showRepo.Object);
        var controller = new ShowsController(showRepo.Object, showService.Object );

        // Act
        var result = controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task CreateRoom()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();

        // Act
        var result = await controller.Create(show);
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task CreateRoomModelState()
    {
        // Arrange
           var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Create(show);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task EditGet()
    {
        // Arrange
           var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = await controller.Edit(1);
        var nonExistentRoom = await controller.Edit(20);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nonExistentRoom);
    }
    
    [Fact]
    public async Task EditIdTest()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = await controller.Edit(1, show);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditModelState()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = EditOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Edit(1, show);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public async Task EditAdd()
    {
        // Arrange
           var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = EditOneShow();
        showRepo.Setup(s => s.Add(show));
        
        // Act
        var result = await controller.Edit(1, show);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditMovieExists()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = EditOneShow();
        var showId = 99999;
        showRepo.Setup(s => s.ShowExists(showId));
        
        // Act
        var result = await controller.Edit(showId, show);

        // Assert
        Assert.IsType(typeof (NotFoundResult), result);
    }
    
    [Fact]
    public async Task EditSucces()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        var showEdit = EditOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = await controller.Edit(1, showEdit);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task DeleteGet()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = await controller.Delete(1);
        var nonExistentShow = await controller.Details(20);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nonExistentShow);
    }
    
    [Fact]
    public async Task DeleteConfirmed()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        
        // Act
        var result = await controller.DeleteConfirmed(1);
        showRepo.Setup(s => s.RemoveShow(show));
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task MovieExists()
    {
        // Arrange
        var showRepo = new Mock<IShowRepository>();
        var showService = new Mock<IShowService>();
        var controller = new ShowsController(showRepo.Object, showService.Object );
        var show = getOneShow();
        showRepo.Setup(s => s.FindShowById(1)).ReturnsAsync(show);
        
        // Act
        var result = controller.ShowExists(1);
        
        // Assert
        Assert.IsType<bool>(result);
    }
}