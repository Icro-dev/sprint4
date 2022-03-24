using cinema.Models;

namespace cinema.Services;

public interface IMovieService
{
    public Movie GetMovieFromShow(int showId);
}