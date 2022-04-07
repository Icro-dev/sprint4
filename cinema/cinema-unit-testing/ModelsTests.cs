using cinema.Models;
using Xunit;

namespace cinema_unit_testing;

public class ModelsTests
{
    [Fact]
    public void Room()
    {
        var theatre = new Theatre();
        var template = new RoomTemplate();
        var room = new Room
        {  
            Id = 1,
            RoomNr = 1,
            ThreeD = true,
            Wheelchair = true,
            Theatre = theatre,
            Template = template
        };
        Assert.Equal(1,room.Id);
        Assert.Equal(1,room.RoomNr);
        Assert.True(room.ThreeD);
        Assert.True(room.Wheelchair);
        Assert.Equal(theatre, room.Theatre);
        Assert.Equal(template, room.Template);
    }
    
    
    [Fact]
    public void ArrangementTest()
    {
        var arrangement = new Arrangement()
        {  
            Id = 1,
            Price = 10,
            Description = "test"

        };
        Assert.Equal("test", arrangement.Description);
        Assert.Equal(10, arrangement.Price);
        Assert.Equal(1, arrangement.Id);

    }
    
    [Fact]
    public void TheatreTest()
    {
        var theatre = new Theatre()
        {
            Id = 1,
            Location = 134234234
        };
        Assert.Equal(1, theatre.Id);
        Assert.Equal(134234234, theatre.Location);
    }
    
    [Fact]
    public void Subscriber()
    {
        var subscriber = new Subscriber()
        {
            Id = 1,
            Email = "test@test.nl"

        };
        Assert.Equal(1, subscriber.Id);
        Assert.Equal("test@test.nl", subscriber.Email);
    }

    [Fact]
    public void TicketTest()
    {
        var show = new Show();
        var arrangements = new cinema.Arrangements();
        var ticket = new Ticket()
        {
            Id = 1,
            show = show,
            SeatRow = 1,
            SeatNr = 2,
            ChildDiscount = true,
            StudentDiscount = true,
            SeniorDiscount = true,
            Code = 1213131,
            CodeUsed = false,
            Popcorn = true,
            Arrangements = arrangements
        };
        Assert.Equal(1, ticket.Id);
        Assert.Equal(show, ticket.show);
        Assert.Equal(1, ticket.SeatRow);
        Assert.Equal(2, ticket.SeatNr);
        Assert.True(ticket.ChildDiscount);
        Assert.True(ticket.StudentDiscount);
        Assert.True(ticket.SeniorDiscount);
        Assert.Equal(1213131, ticket.Code);
        Assert.False(ticket.CodeUsed);
        Assert.True(ticket.Popcorn);
        Assert.Equal(arrangements, ticket.Arrangements);
    }
    
    [Fact]
    public void MovieTest()
    {
        var movie = new Movie()
        {
            Name = "avatar",
            Description = "test",
            Director = "testDirector",
            Cast = "testCast",
            ReleaseYear = 1999,
            CountryOfOrigin = "nl",
            Length = 120,
            Genre = "testGenre",
            Poster = "testPoster",
            Language = "nl",
            ThreeD = true,
            Kijkwijzer = "testKijkwijzer"

        };
        Assert.Equal( "avatar", movie.Name);
        
        Assert.Equal( "test", movie.Description);
        Assert.Equal( "testDirector", movie.Director);
        Assert.Equal( "testCast", movie.Cast);
        Assert.Equal( 1999, movie.ReleaseYear);
        Assert.Equal( "nl", movie.CountryOfOrigin);
        Assert.Equal( 120, movie.Length);
        Assert.Equal( "testGenre", movie.Genre);
        
        Assert.Equal( "testPoster", movie.Poster);
        Assert.Equal( "nl", movie.Language);
        Assert.True( movie.ThreeD);
        Assert.Equal( "testKijkwijzer", movie.Kijkwijzer);
    }
    
    [Fact]
    public void ShowTest()
    {
        var show = new Show()
        {
            Break = true,
        };
        Assert.True(show.Break);
    }
    
    [Fact]
    public void TicketOrderTest()
    {
        var ticketOrder = new TicketOrder()
        {
            Id = 1,
            SerializedTicketIds = "[1,2,3]",
            Cost = 10,
            IsPayed = true,
            ShowId = 1
            
        };
        Assert.Equal(1, ticketOrder.Id);
        Assert.Equal("[1,2,3]", ticketOrder.SerializedTicketIds);
        Assert.Equal(10, ticketOrder.Cost);
        Assert.True(ticketOrder.IsPayed);
        Assert.Equal(1, ticketOrder.ShowId);

    }
}