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

public class RoomControllerTests
{
    
    [Fact]
    public async Task IndexTest()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    public static Room getOneRoom()
    {
        string setting = "Setting";
        var room = new Room();
        room.Id = 1;
        room.Template = new RoomTemplate(setting);
        room.Theatre = new Theatre();
        room.Wheelchair = true;
        room.RoomNr = 1;
        room.ThreeD = true;
        return room;
    }
    
    public static Room EditOneRoom()
    {
        string setting = "Setting";
        var room = new Room();
        room.Id = 2;
        room.Template = new RoomTemplate(setting);
        room.Theatre = new Theatre();
        room.Wheelchair = false;
        room.RoomNr = 2;
        room.ThreeD = false;
        return room;
    }
    
    [Fact]
    public async Task Details()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
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
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);

        // Act
        var result = controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public async Task CreateRoom()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();

        // Act
        var result = await controller.Create(room);
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task CreateRoomModelState()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
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
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
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
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
        // Act
        var result = await controller.Edit(1, room);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditIdFailedTest()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);

        // Act
        var result = await controller.Edit(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    [Fact]
    public async Task EditModelState()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = EditOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        controller.ModelState.AddModelError("test", "test");
        
        // Act
        var result = await controller.Edit(2, room);
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public async Task EditAdd()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = EditOneRoom();
        roomRepo.Setup(s => s.Add(room));
        
        // Act
        var result = await controller.Edit(2, room);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task EditMovieExists()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = EditOneRoom();
        var roomId = 20;
        roomRepo.Setup(s => s.RoomExists(roomId));
        
        // Act
        var result = await controller.Edit(roomId, room);

        // Assert
        Assert.IsType(typeof (NotFoundResult), result);
    }
    
    [Fact]
    public async Task EditSucces()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        var roomEdit = EditOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
        // Act
        var result = await controller.Edit(2, roomEdit);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task DeleteGet()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
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
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
        
        // Act
        var result = await controller.DeleteConfirmed(1);
        roomRepo.Setup(s => s.RemoveRoom(room));
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
    
    [Fact]
    public async Task MovieExists()
    {
        // Arrange
        var roomRepo = new Mock<IRoomRepository>();
        var controller = new RoomsController(roomRepo.Object);
        var room = getOneRoom();
        roomRepo.Setup(s => s.FindRoomById(1)).ReturnsAsync(room);
        
        // Act
        var result = controller.RoomExists(1);
        
        // Assert
        Assert.IsType<bool>(result);
    }
}
