using cinema.Models;

namespace cinema.Services;

public interface IShowService
{
    public Dictionary<DateOnly, Dictionary<Movie, List<Show>>> GetShowsPerMoviePerDay(List<Show> showList);
}