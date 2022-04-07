using System;
using System.Runtime.InteropServices;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class SeatServiceTests
{
    
    public static Show getOneShow()
    {
        var room = new Room();
        room.Id = 1;
        room.Template = new RoomTemplate("[15, 15, 15, 15, 15, 15, 15. 15]") ;
        room.Theatre = new Theatre();
        room.Wheelchair = true;
        room.RoomNr = 1;
        room.ThreeD = true;
        
        var movie = new Movie();
        movie.Name = "Avatar";
        movie.Length = 121;

        var show = new Show();
        show.Id = 1;
        show.Break = true;
        show.Movie = movie;
        show.Room = room.Id;
        show.StartTime = DateTime.Now;
        show.ThreeD = true;
        return show;
    }
    
    public static Room getOneRoom()
    {
        var room = new Room();
        room.Id = 1;
        room.Template = new RoomTemplate("[15, 15, 15, 15, 15, 15, 15. 15]") ;
        room.Theatre = new Theatre();
        room.Wheelchair = true;
        room.RoomNr = 1;
        room.ThreeD = true;
        return room;
    }
    
    [Fact]

    public void GetSeatsTest()
    {
        Mock<ITicketRepository> ticketRepo = new Mock<ITicketRepository>();
        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        RoomService roomService = new RoomService(roomRepo.Object); 
        var show = getOneShow();
        var room = getOneRoom();

        var seatService = new SeatService(roomService, ticketRepo.Object);
        
        Assert.Throws<MissingFieldException>(() => seatService.GetSeats(show, 0));

        roomRepo.Setup(r =>r.findRoomByShow(show)).Returns(new Room());

        var expected = typeof(Room);
        var roomTest = roomService.GetShowRoom(show);

        Assert.IsType(expected, roomTest);
        
    }

}