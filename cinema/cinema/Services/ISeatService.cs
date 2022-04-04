using cinema.Models;

namespace cinema.Services;

public interface ISeatService
{
    public int[,]? GetSeats(Show show, int quantity);
    public List<List<int>> GetTakenSeats(Show show);
}