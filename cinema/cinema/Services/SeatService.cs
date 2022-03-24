using System.Diagnostics;
using System.Text.Json;
using cinema.Data;
using cinema.Models;

namespace cinema.Services;

public class SeatService : ISeatService
{
    private readonly CinemaContext _context;
    private readonly IRoomService _roomService;
    private readonly Random _rng = Random.Shared; 

    public SeatService(IRoomService roomService, CinemaContext context)
    {
        _roomService = roomService;
        _context = context;
    }

    public int[,]? GetSeats(Show show, int quantity)
    {
        if (show == null || !(quantity > 0))
        {
            string error = show == null ? "No show selected" : "Zero tickets selected";
            throw new MissingFieldException(error);
        }

        string? template = null;
        //get the roomtemplate for the show
        Room room = _roomService.GetShowRoom(show);
        template = room.Template.Setting;
        var templateArray = JsonSerializer.Deserialize<int[]>(template);

        // get the sold tickets for the show
        var tickets = _context.Tickets.Where(t => t.show.Equals(show));
        var seatsNum = 0;
        foreach ( var seats in templateArray)
        {
            seatsNum += seats;
        }
        //if show is sold out, throw exception
        if (!(tickets.Count() < seatsNum))
        {
            throw new Exception("Show is sold out");
        }

        // create a seatmap from the template
        var seatMap = new List<int[]>();
        if (templateArray != null)
        {
            foreach (var row in templateArray)
            {
                seatMap.Add(new int[row]);
            }
        }
        // fill in the sold tickets in the seatmap
        foreach (Ticket ticket in tickets)
        {
            seatMap[ticket.SeatRow - 1][ticket.SeatNr - 1] = 1;
        }
        //TODO create real algorithm to find seats
        // call the random number goddess to find a seat for these poor souls
        bool seatFound = false;
        int luckyRow = 1;
        int luckySeat = 1;
        int counter = 0;
        while (seatFound == false && counter < 100)
        {
            luckyRow = _rng.Next(1, seatMap.Count + 1);
            luckySeat = _rng.Next(1, seatMap[luckyRow - 1].Length - quantity + 1);
            seatFound = checkAvailableAdjacentSeats(seatMap, luckyRow, luckySeat, quantity);
            counter++;
        }

        //create an array with seats and return it 
        int[,] theSeats = new int[quantity,2];
        for (int i = 0; i < quantity; i++)
        {
            theSeats[i, 0] = luckyRow;
            theSeats[i, 1] = luckySeat + i;
        }
        return theSeats;
    }

    public List<List<int>> GetTakenSeats(Show show)
    {
        Room room = _roomService.GetShowRoom(show);
        int[] roomtemplate = _roomService.GetRoomTemplate(room);
        List<List<int>> takenseats = new List<List<int>>();
        for(int row = 0; row < roomtemplate.Count(); row++)
        {
            List<int> takenrowseats = new List<int>();
            List<Ticket> rowtickets = _context.Tickets.Where(t => t.show.Equals(show)).Where(t => t.SeatRow.Equals(row + 1)).ToList();
            foreach (Ticket ticket in rowtickets)
                takenrowseats.Add(ticket.SeatNr);
            takenseats.Add(takenrowseats);
        }
        return takenseats;
    }

    private bool checkAvailableAdjacentSeats(List<int[]> map, int seatRow, int seatNum, int quantity)
    {
        if (seatNum - 1 + quantity > map[seatRow - 1].Length) return false;
        
        bool available = true;
        for (int i = 0; i < quantity; i++)
        {
            if (map[seatRow - 1][seatNum - 1 + i] != 0) available = false;
        }

        return available;
    }
}