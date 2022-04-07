using System.Collections.Generic;
using cinema;
using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Moq;
using Xunit;

namespace cinema_unit_testing;

public class TicketServiceTests
{
    [Fact]
    public void GetAllTicketsTest()
    {
        var show = new Show();
        show.Id = 1;
        var ticket = new Ticket();
        ticket.Id = 1;
        ticket.show = show;
        var ticket2 = new Ticket();
        ticket.Id = 1;
        ticket.show = show;

        Mock<ITicketRepository> ticketRepo = new Mock<ITicketRepository>();
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepo.Object);
        var seatService = new SeatService(roomService, ticketRepo.Object);


        ticketRepo.Setup(s => s.FindAllTickets()).Returns(new List<Ticket>
            {
                ticket, ticket2
            }
        );

        TicketService ticketService = new TicketService(seatService, ticketRepo.Object, showRepo.Object);
        var tickets = ticketService.GetAllTickets();

        Assert.IsAssignableFrom<IEnumerable<Ticket>?>(tickets);
    }

    [Fact]
    public void CreateTicketsTest()
    {
        
        var roomTemplate = new RoomTemplate("[15,15,15,15,15,15,15,15]");
        
        var room = new Room()
        {
            Id = 1,
            Template = roomTemplate
        };
        
        var show = new Show
        {
            Id = 1,
            Break = false,
            Movie = new Movie(),
            Room = 1
        };

        Mock<ITicketRepository> ticketRepo = new Mock<ITicketRepository>();
        Mock<IShowRepository> showRepo = new Mock<IShowRepository>();
        Mock<IRoomRepository> roomRepo = new Mock<IRoomRepository>();
        var roomService = new RoomService(roomRepo.Object);
        var seatService = new SeatService(roomService,ticketRepo.Object);
        
        // seatServiceMock.Setup(s => s.GetSeats(show,1)).Returns()
        TicketService ticketService = new TicketService(seatService,ticketRepo.Object,showRepo.Object);

        showRepo.Setup(s => s.FindShowByIdIncludeMovie(1)).Returns(show);
        roomRepo.Setup(r => r.findRoomByShow(show)).Returns(room);
        
        var newTicket = ticketService.CreateTickets(1, 10, 1, 1, 1, 1, Arrangements.kinderfeestje);

        Assert.IsAssignableFrom<List<Ticket>>(newTicket);
    }
}