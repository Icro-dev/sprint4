using System;
using System.Collections.Generic;
using cinema.Controllers;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class RoomServiceTests
{
    public static Room getOneRoom()
    {
        var room = new Room();
        var roomTemplate = new RoomTemplate("[15,15,15,15,15,15,15,15]");
        var theatre = new Theatre();
        room.Id = 1;
        room.Template = roomTemplate;
        room.Theatre = theatre;
        room.Wheelchair = false;
        room.RoomNr = 1;
        room.ThreeD = false;
        return room;
    }
    
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
    public void GetRoomTemplate_listWithRoomTemplates()
    {
        var room = getOneRoom();
        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        RoomService roomService = new RoomService(roomRepo.Object);

        List<int> template = new List<int>(new int[]{ 15,15,15,15,15,15,15,15 });

        var resultTemplate = roomService.GetRoomTemplate(room);
        Assert.Equal(template, resultTemplate);
    }
    
     
    [Fact]
    public void GetShowRoom_ByRoom()
    {
        var room = getOneRoom();

        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        RoomService roomService = new RoomService(roomRepo.Object);

        var show = getOneShow();
        var currentRoom = roomService.GetShowRoom(show);
        Assert.Equal(show.Room, room.Id);
    }
}