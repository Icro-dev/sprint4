using System.Diagnostics;
using System.Text.Json;
using cinema.Data;
using cinema.Models;

namespace cinema.Services;

public class SeatService : ISeatService
{
    private readonly CinemaContext _context;
    private readonly Random _rng = Random.Shared; 

    public SeatService(CinemaContext context)
    {
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
        if (_context.RoomTemplates != null && _context.RoomTemplates.Any())
            template = _context.RoomTemplates.First(t => t.Id == 1).Setting;
        var templateArray = JsonSerializer.Deserialize<int[]>(template);

        // get the sold tickets for the show
        var tickets = _context.Tickets.Where(t => t.show.Equals(show));
        //if show is sold out, throw exception
        if (!(tickets.Count() < quantity))
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
            seatMap[ticket.SeatRow][ticket.SeatNr] = 1;
        }
        //TODO create real algorithm to find seats
        // call the random number goddess to find a seat for these poor souls
        bool seatFound = false;
        int luckyRow = 0;
        int luckySeat = 0;
        int counter = 0;
        while (seatFound == false && counter < 100)
        {
            luckyRow = _rng.Next(0, seatMap.Count);
            luckySeat = _rng.Next(0, seatMap[luckyRow].Length-quantity);
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

    private bool checkAvailableAdjacentSeats(List<int[]> map, int seatRow, int seatNum, int quantity)
    {
        if (seatNum + quantity > map[seatRow].Length) return false;
        
        bool available = true;
        for (int i = 0; i < quantity; i++)
        {
            if (map[seatRow][seatNum + i] != 0) available = false;
        }

        return available;
    }
}