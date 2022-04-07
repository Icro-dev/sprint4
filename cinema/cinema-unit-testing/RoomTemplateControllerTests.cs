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

public class RoomTemplateControllerTests
{
    
    
    [Fact]
    public async Task IndexTest()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    public static RoomTemplate getOneRoomTemplate()
    {
        var roomTemplate = new RoomTemplate();
        string setting = "[10, 10, 10, 10, 10]";
        roomTemplate.Id = 1;
        roomTemplate.Setting = setting;
        return roomTemplate;
    }
    
    public static RoomTemplate EditOneRoomTemplate()
    {
        var roomTemplate = new RoomTemplate();
        string setting = "[15, 15, 15, 15, 15, 15]";
        roomTemplate.Id = 1;
        roomTemplate.Setting = setting;
        return roomTemplate;
    }
    
    [Fact]
    public async Task Details()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
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
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);

        // Act
        var result = controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task CreateRoom()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();

        // Act
        var result = await controller.Create(room);
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task CreateRoomModelState()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Create(room);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task EditGet()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
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
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
        // Act
        var result = await controller.Edit(1, room);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditModelState()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = EditOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Edit(1, room);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public async Task EditAdd()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = EditOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.Add(room));
        
        // Act
        var result = await controller.Edit(1, room);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditMovieExists()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = EditOneRoomTemplate();
        var roomId = 20;
        roomTemplateRepo.Setup(s => s.RoomTemplateExists(roomId));
        
        // Act
        var result = await controller.Edit(roomId, room);

        // Assert
        Assert.IsType(typeof (NotFoundResult), result);
    }
    
    [Fact]
    public async Task EditSucces()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        var roomEdit = EditOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
        // Act
        var result = await controller.Edit(1, roomEdit);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task DeleteGet()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var roomTemplate = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(roomTemplate);
        
        // Act
        var result = await controller.Delete(1);
        var nonExistentRoom = await controller.Details(20);
       
        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.IsType(typeof (NotFoundResult), nonExistentRoom);
    }
    
    [Fact]
    public async Task DeleteConfirmed()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
        
        // Act
        var result = await controller.DeleteConfirmed(1);
        roomTemplateRepo.Setup(s => s.RemoveRoomTemplate(room));
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task MovieExists()
    {
        // Arrange
        var roomTemplateRepo = new Mock<IRoomTemplatesRepository>();
        var controller = new RoomTemplatesController(roomTemplateRepo.Object);
        var room = getOneRoomTemplate();
        roomTemplateRepo.Setup(s => s.FindRoomTemplatesById(1)).ReturnsAsync(room);
        
        // Act
        var result = controller.RoomTemplateExists(1);
        
        // Assert
        Assert.IsType<bool>(result);
    }
}

