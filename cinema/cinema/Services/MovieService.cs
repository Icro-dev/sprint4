using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class MovieService : IMovieService
{
    private readonly IShowRepository _showRepository;

    public MovieService(IShowRepository showRepository)
    {
        _showRepository = showRepository;
    }

    public Movie GetMovieFromShow(int showId)
    {
        var show = _showRepository.FindShowByIdIncludeMovie(showId);
        return show.Movie;
    }
}