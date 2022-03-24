using cinema.Models;

namespace cinema.Services;

public interface ISeatService
{
    public int[,]? GetSeats(Show show, int quantity);
}