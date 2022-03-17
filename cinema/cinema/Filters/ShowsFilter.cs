using cinema.Models;

namespace cinema.Filters
{
    public class ShowsFilter
    {
        public string? input;
        public double maxlength;
        public double maxselectablelength;
        public DateTime? date;
        public bool threed;
        public Dictionary<string, bool> languages;
        public Dictionary<string, bool> genres;
        public Dictionary<string, bool> kijkwijzers;

        public ShowsFilter(List<Show> shows)
        {
            languages = new Dictionary<string, bool>();
            genres = new Dictionary<string, bool>();
            kijkwijzers = new Dictionary<string, bool>();
            maxselectablelength = 0;
            foreach (Show show in shows)
            {
                if (!genres.ContainsKey(show.Movie.Genre))
                    genres.Add(show.Movie.Genre, true);
                if (!kijkwijzers.ContainsKey(show.Movie.Kijkwijzer))
                    kijkwijzers.Add(show.Movie.Kijkwijzer, true);
                if (!languages.ContainsKey(show.Movie.Language))
                    languages.Add(show.Movie.Language, true);
                if (show.Movie.Length > maxselectablelength)
                    maxselectablelength = show.Movie.Length;
            }
            date = null;
        }

        public List<Show> Apply(List<Show> shows)
        {
            List<Show> filteredshows = shows;
            foreach(Show show in shows)
            {
                if(input != null)
                    if (!show.Movie.Name.Contains(input) || !show.Movie.Description.Contains(input) || !show.Movie.Director.Contains(input) || !show.Movie.Cast.Contains(input))
                        if (filteredshows.Contains(show))
                            filteredshows.Remove(show);
                if (show.Movie.Length > maxlength)
                    if (filteredshows.Contains(show))
                        filteredshows.Remove(show);
                if(date != null)
                {
                    DateTime selecteddate = (DateTime) date;
                    if (show.StartTime.Date != selecteddate.Date)
                        if (filteredshows.Contains(show))
                            filteredshows.Remove(show);
                }                   
                if(threed && !show.ThreeD)                   
                    if (filteredshows.Contains(show))
                        filteredshows.Remove(show);
                if (languages.ContainsKey(show.Movie.Language) && !genres[show.Movie.Language])
                    if (filteredshows.Contains(show))
                        filteredshows.Remove(show);
                if (genres.ContainsKey(show.Movie.Genre) && !genres[show.Movie.Genre])
                    if (filteredshows.Contains(show))
                        filteredshows.Remove(show);
                if (kijkwijzers.ContainsKey(show.Movie.Kijkwijzer) && !kijkwijzers[show.Movie.Kijkwijzer])
                    if (filteredshows.Contains(show))
                        filteredshows.Remove(show);
            }
            return filteredshows;
        }
    }
}
