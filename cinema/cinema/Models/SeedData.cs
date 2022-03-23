using System.Text.Json;
using cinema.Controllers;
using cinema.Data;
using Microsoft.EntityFrameworkCore;

namespace cinema.Models;

public static class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CinemaContext(
            serviceProvider.GetRequiredService<DbContextOptions<CinemaContext>>());

        if (context.RoomTemplates != null && !context.RoomTemplates.Any())
            context.RoomTemplates.AddRange(
                new RoomTemplate(
                
                    JsonSerializer.Serialize(new List<int> {15, 15, 15, 15, 15, 15, 15, 15})
                ),
                new RoomTemplate
                (
                    JsonSerializer.Serialize(new List<int> {10, 10, 10, 10, 10, 10})
                ),
                new RoomTemplate
                (
                    JsonSerializer.Serialize(new List<int> {10, 10, 15, 15})
                )
            );
        if (context.Theatres != null && !context.Theatres.Any())
            context.Theatres.AddRange(
                new Theatre
                {
                    Location = 1
                },
                new Theatre
                {
                    Location = 2
                }
            );
        await context.SaveChangesAsync();

        if (context.Rooms != null && !context.Rooms.Any())
            context.Rooms.AddRange(
                new Room
                {
                    RoomNr = 1,
                    Template = context.RoomTemplates!.First(t => t.Id == 1),
                    Wheelchair = true,
                    ThreeD = true,
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                },
                new Room
                {
                    RoomNr = 2,
                    Template = context.RoomTemplates!.First(t => t.Id == 1),
                    Wheelchair = true,
                    ThreeD = true,
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                },
                new Room
                {
                    RoomNr = 3,
                    Template = context.RoomTemplates!.First(t => t.Id == 1),
                    Wheelchair = true,
                    ThreeD = false,
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                },
                new Room
                {
                    RoomNr = 4,
                    Template = context.RoomTemplates!.First(t => t.Id == 2),
                    Wheelchair = true,
                    ThreeD = false,
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                },
                new Room
                {
                    RoomNr = 5,
                    Template = context.RoomTemplates!.First(t => t.Id == 3),
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                },
                new Room
                {
                    RoomNr = 6,
                    Template = context.RoomTemplates!.First(t => t.Id == 3),
                    Wheelchair = false,
                    ThreeD = false,
                    Theatre = context.Theatres!.First(t => t.Id == 1)
                }
            );


        if (context.Movies != null && !context.Movies.Any())
            context.Movies.AddRange(
                new Movie
                {
                    Name = "When Harry Met Sally",
                    Description =
                        "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.",
                    Director = "Rob Steiner",
                    Cast = "Billy Crystal, Meg Ryan, Carrie Fisher",
                    ReleaseYear = 1989,
                    CountryOfOrigin = "United States",
                    Length = 1.35,
                    Genre = "Comedy, Drama, Romance",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BMjE0ODEwNjM2NF5BMl5BanBnXkFtZTcwMjU2Mzg3NA@@._V1_FMjpg_UX682_.jpg",
                    Language = "English",
                    ThreeD = false,
                    Kijkwijzer = "AL GT"
                },
                new Movie
                {
                    Name = "The Lord of the Rings: The Fellowship of the Ring",
                    Description =
                        "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                    Director = "Peter Jackson",
                    Cast = "Elijah Wood, Ian McKellen, Orlando Bloom",
                    ReleaseYear = 2001,
                    CountryOfOrigin = "United States, New Zealand",
                    Length = 2.58,
                    Genre = "Action, Adventure, Drama, Fantasy",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_FMjpg_UY720_.jpg",
                    Language = "English",
                    ThreeD = false,
                    Kijkwijzer = "12 G A"
                },
                new Movie
                {
                    Name = "Avatar",
                    Description =
                        "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                    Director = "James Cameron",
                    Cast = "Sam Worthington, Zoe Saldana, Sigourney Weaver",
                    ReleaseYear = 2009,
                    CountryOfOrigin = "United States",
                    Length = 2.42,
                    Genre = "Action, Adventure, Sci-Fi, Fantasy",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BMTYwOTEwNjAzMl5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1_FMjpg_UX510_.jpg",
                    Language = "English",
                    ThreeD = true,
                    Kijkwijzer = "12 G A"
                },
                new Movie
                {
                    Name = "Joker",
                    Description =
                        "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker.",
                    Length = 2.02,
                    Director = "Todd Philips",
                    Cast = "Joaquin Phoenix, Robert De Niro, Zazie Beetz",
                    ReleaseYear = 2019,
                    CountryOfOrigin = "United States",
                    Genre = "Crime, Drama, Thriller",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BNGVjNWI4ZGUtNzE0MS00YTJmLWE0ZDctN2ZiYTk2YmI3NTYyXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_FMjpg_UY720_.jpg",
                    Language = "English",
                    ThreeD = false,
                    Kijkwijzer = "16 G GT"
                },
                new Movie
                {
                    Name = "Gravity",
                    Description =
                        "Two astronauts work together to survive after an accident leaves them stranded in space.",
                    Length = 1.31,
                    Director = "Alfonso Cuarón",
                    Cast = "Sandra Bullock, George Clooney, Ed Harris",
                    ReleaseYear = 2013,
                    CountryOfOrigin = "United States, United Kingdom",
                    Genre = "Action, Drama, Sci-Fi",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BNjE5MzYwMzYxMF5BMl5BanBnXkFtZTcwOTk4MTk0OQ@@._V1_FMjpg_UX680_.jpg",
                    Language = "English",
                    ThreeD = true,
                    Kijkwijzer = "12 A GT"
                },
                new Movie
                {
                    Name = "The Dark Knight",
                    Description =
                        "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Director = "Christopher Nolan",
                    Cast = "Christian Bale, Heath Ledger, David S. Goyer",
                    ReleaseYear = 2008,
                    CountryOfOrigin = "United States, United Kingdom",
                    Length = 2.32,
                    Genre = "Action, Drama, Crime",
                    Poster =
                        "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UY720_.jpg",
                    Language = "English",
                    ThreeD = false,
                    Kijkwijzer = "16 G A"
                }
            );
        await context.SaveChangesAsync();

        if (context.Shows != null && !context.Shows.Any())
        {
            var startDate = new DateTime(2022, 3,17);
            var times = new int[] {13, 16, 19, 21};

            var movies = context.Movies!.ToList();
            var showList = new List<Show>();
            for (var di = 0; di < 14; di++)
            {
                for (var ri = 1; ri < 7; ri++)
                {
                    foreach (var hour in times)
                    {
                        var rnd = new Random();

                        var show = new Show
                        {
                            ThreeD = false,
                            Room = ri,
                            StartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day+di, hour, 00, 00),
                            Break = false,
                            Movie = movies[rnd.Next(movies.Count-1)]
                        };
                        var exists = showList.Find(s =>
                            s.StartTime.Day == show.StartTime.Day && s.StartTime.Hour == show.StartTime.Hour && s.Movie == show.Movie);
                        if (exists == null) showList.Add(show);
                    }

                }
            }
            context.Shows.AddRange(showList);
            await context.SaveChangesAsync();
        }
        
    }
}