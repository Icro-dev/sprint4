using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using cinema.Data;

namespace cinema.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<DbContextOptions<CinemaContext>>()))
            {

                if (!context.RoomTemplates.Any()) 
                {
                    context.RoomTemplates.AddRange(
                        new RoomTemplate
                        {
                            Setting = "8 rijen van 15 stoelen"
                        },
                        new RoomTemplate
                        {
                            Setting = "6 rijen van 10 stoelen"
                        },
                        new RoomTemplate
                        {
                            Setting = "2 rijen met 10 stoelen voor, 2 rijen met 15 stoelen achterin"
                        }
                        );
                }

                if (!context.Theatres.Any())
                {
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
                }

                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(
                        new Room
                        {
                            RoomNr = 1,
                            Template = new RoomTemplate{ Id = 1},
                            Wheelchair = true,
                            ThreeD = true,
                            Theatre = new Theatre { Id = 1}
                        },
                         new Room
                         {
                             RoomNr = 2,
                             Template = new RoomTemplate { Id = 1 },
                             Wheelchair = true,
                             ThreeD = true,
                             Theatre = new Theatre { Id = 1 }
                         },
                         new Room
                         {
                             RoomNr = 3,
                             Template = new RoomTemplate { Id = 1 },
                             Wheelchair = true,
                             ThreeD = false,
                             Theatre = new Theatre { Id = 1 }
                         },
                         new Room
                         {
                             RoomNr = 4,
                             Template = new RoomTemplate { Id = 2 },
                             Wheelchair = true,
                             ThreeD = false,
                             Theatre = new Theatre { Id = 1 }
                         },
                         new Room
                         {
                             RoomNr = 5,
                             Template = new RoomTemplate { Id = 3 },
                             Wheelchair = false,
                             ThreeD = false,
                             Theatre = new Theatre { Id = 1 }
                         },
                         new Room
                         {
                             RoomNr = 6,
                             Template = new RoomTemplate { Id = 3 },
                             Wheelchair = false,
                             ThreeD = false,
                             Theatre = new Theatre { Id = 1 }
                         }
                        );
                }
                


                if (!context.Movies.Any())
                {

                    context.Movies.AddRange(
                        new Movie
                        {
                            Name = "When Harry Met Sally",
                            Description = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.",
                            Director = "Rob Steiner",
                            Cast = "Billy Crystal, Meg Ryan, Carrie Fisher",
                            ReleaseYear = 1989,
                            CountryOfOrigin = "United States",
                            Length = 1.35,
                            Genre = "Comedy, Drama, Romance",
                            Poster = "https://m.media-amazon.com/images/M/MV5BMjE0ODEwNjM2NF5BMl5BanBnXkFtZTcwMjU2Mzg3NA@@._V1_FMjpg_UX682_.jpg",
                            Language = "English",
                            ThreeD = false,
                            Kijkwijzer = "AL GT"
                        },

                        new Movie
                        {
                            Name = "The Lord of the Rings: The Fellowship of the Ring",
                            Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                            Director = "Peter Jackson",
                            Cast = "Elijah Wood, Ian McKellen, Orlando Bloom",
                            ReleaseYear = 2001,
                            CountryOfOrigin = "United States, New Zealand",
                            Length = 2.58,
                            Genre = "Action, Adventure, Drama, Fantasy",
                            Poster = "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_FMjpg_UY720_.jpg",
                            Language = "English",
                            ThreeD = false,
                            Kijkwijzer =  "12 G A" 
                        },

                        new Movie
                        {
                            Name = "Avatar",
                            Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                            Director = "James Cameron",
                            Cast = "Sam Worthington, Zoe Saldana, Sigourney Weaver",
                            ReleaseYear = 2009,
                            CountryOfOrigin = "United States",
                            Length = 2.42,
                            Genre = "Action, Adventure, Sci-Fi, Fantasy",
                            Poster = "https://m.media-amazon.com/images/M/MV5BMTYwOTEwNjAzMl5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1_FMjpg_UX510_.jpg",
                            Language = "English",
                            ThreeD = true,
                            Kijkwijzer = "12 G A" 
                        },

                        new Movie
                        {
                            Name = "Joker",
                            Description = "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker.",
                            Length = 2.02,
                            Director = "Todd Philips",
                            Cast = "Joaquin Phoenix, Robert De Niro, Zazie Beetz",
                            ReleaseYear = 2019,
                            CountryOfOrigin = "United States",
                            Genre = "Crime, Drama, Thriller",
                            Poster = "https://m.media-amazon.com/images/M/MV5BNGVjNWI4ZGUtNzE0MS00YTJmLWE0ZDctN2ZiYTk2YmI3NTYyXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_FMjpg_UY720_.jpg",
                            Language = "English",
                            ThreeD = false,
                            Kijkwijzer = "16 G GT" 
                        },

                        new Movie
                        {
                            Name = "Gravity",
                            Description = "Two astronauts work together to survive after an accident leaves them stranded in space.",
                            Length = 1.31,
                            Director = "Alfonso Cuarón",
                            Cast = "Sandra Bullock, George Clooney, Ed Harris",
                            ReleaseYear = 2013,
                            CountryOfOrigin = "United States, United Kingdom",
                            Genre = "Action, Drama, Sci-Fi",
                            Poster = "https://m.media-amazon.com/images/M/MV5BNjE5MzYwMzYxMF5BMl5BanBnXkFtZTcwOTk4MTk0OQ@@._V1_FMjpg_UX680_.jpg",
                            Language = "English",
                            ThreeD = true,
                            Kijkwijzer = "12 A GT" 
                        },

                       new Movie
                       {
                           Name = "The Dark Knight",
                           Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                           Director = "Christopher Nolan",
                           Cast = "Christian Bale, Heath Ledger, David S. Goyer",
                           ReleaseYear = 2008,
                           CountryOfOrigin = "United States, United Kingdom",
                           Length = 2.32,
                           Genre = "Action, Drama, Crime",
                           Poster = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UY720_.jpg",
                           Language = "English",
                           ThreeD = false,
                           Kijkwijzer = "16 G A"
                       }

                        );
                }
                context.SaveChanges();

                if (!context.Shows.Any())
                {
                    context.Shows.AddRange(
                        new Show
                        {
                            ThreeD = false,
                            Room = 3,
                            StartTime = new DateTime(2022, 10, 3, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "Joker")

                        },
                       new Show
                       {
                           ThreeD = true,
                           Room = 1,
                           StartTime = new DateTime(2022, 10, 3, 19, 15, 00),
                           Break = false,
                           Movie = (Movie)context.Movies.First(b => b.Name == "Avatar")
                       },
                      new Show
                      {
                          ThreeD = true,
                          Room = 2,
                          StartTime = new DateTime(2022, 10, 3, 17, 00, 00),
                          Break = false,
                          Movie = (Movie)context.Movies.First(b => b.Name == "Gravity")
                      },
                       new Show
                       {
                           ThreeD = false,
                           Room = 4,
                           StartTime = new DateTime(2022, 10, 3, 20, 00, 00),
                           Break = false,
                           Movie = (Movie)context.Movies.First(b => b.Name == "When Harry Met Sally")
                       },
                     new Show
                     {
                         ThreeD = false,
                         Room = 5,
                         StartTime = new DateTime(2022, 10, 3, 18, 00, 00),
                         Break = false,
                         Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                     },
                     
                       new Show
                       {
                           ThreeD = false,
                           Room = 6,
                           StartTime = new DateTime(2022, 10, 3, 21, 00, 00),
                           Break = false,
                           Movie = (Movie)context.Movies.First(b => b.Name == "The Dark Knight")
                       },
                           new Show
                        {
                            ThreeD = false,
                            Room = 1,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        },
                        new Show
                        {
                            ThreeD = false,
                            Room = 2,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        },
                        new Show
                        {
                            ThreeD = false,
                            Room = 3,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        },
                        new Show
                        {
                            ThreeD = false,
                            Room = 4,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        },
                        new Show
                        {
                            ThreeD = false,
                            Room = 5,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        },
                        new Show
                        {
                            ThreeD = false,
                            Room = 6,
                            StartTime = new DateTime(2022, 3, 310, 19, 00, 00),
                            Break = false,
                            Movie = (Movie)context.Movies.First(b => b.Name == "The Lord of the Rings: The Fellowship of the Ring")
                        }
                        
                        
                        
                        );
                }
                context.SaveChanges();

            }
        }
    }
}
