using cinema.Models;

namespace cinema.Services;

public interface ISeatService
{
    public Array GetSeats(string show, int quantity);
}