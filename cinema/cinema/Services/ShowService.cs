using System.Diagnostics;
using cinema.Data;
using cinema.Migrations;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class ShowService : IShowService
{
    private readonly CinemaContext _context;

    public ShowService(CinemaContext context)
    {
        _context = context;
    }

    public Dictionary<DateOnly, Dictionary<Movie, List<Show>>> GetShowsPerMoviePerDay(List<Show> showList)
    {

        
        showList.Sort((a,b) => DateTime.Compare(a.StartTime,b.StartTime));
        var dateList = new List<DateOnly>();
        var showDict = new Dictionary<DateOnly, List<Show>>();
        var showPerMoviePerDateDict = new Dictionary<DateOnly, Dictionary<Movie, List<Show>>>();
        foreach (Show show in showList)
        {
            var date = DateOnly.FromDateTime(show.StartTime);
            if(!(dateList.Contains(date)))
            {
                dateList.Add(date);
            }
        }
        dateList.Sort((a, b) => (a.CompareTo(b)));
        foreach (var date in dateList)
        {
            showDict.Add(date,new List<Show>());
        }

        foreach (Show show in showList)
        {
            var date = DateOnly.FromDateTime(show.StartTime);
            showDict[date].Add(show);
        }


        foreach (var date in showDict.Keys)
        {
            var showsPerMovieDict = new Dictionary<Movie, List<Show>>();
            var showsPerDate = showDict[date];
            foreach (var show in showsPerDate)
            {
                if (!showsPerMovieDict.ContainsKey(show.Movie))
                {
                    showsPerMovieDict.Add(show.Movie,new List<Show>());
                }
                showsPerMovieDict[show.Movie].Add(show);
            }
            showPerMoviePerDateDict[date] = showsPerMovieDict;
        }

        return showPerMoviePerDateDict;
    }
    
}