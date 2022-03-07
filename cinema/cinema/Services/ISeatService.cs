using cinema.Models;

namespace cinema.Services;

public interface ISeatService
{
    public Array GetSeats(Show show, int quantity);
}