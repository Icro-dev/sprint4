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

    public void GetSeatsTest()
    {
        Mock<ITicketRepository> ticketRepo = new Mock<ITicketRepository>();
        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        IRoomService roomService = new RoomService(roomRepo.Object); 
        var show = getOneShow();

        var seatService = new SeatService(roomService, ticketRepo.Object);


        Assert.Throws<MissingFieldException>(() => seatService.GetSeats(show, 0));
    }

}