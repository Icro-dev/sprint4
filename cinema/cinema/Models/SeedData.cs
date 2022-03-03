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
                if(context.Movies.Any())
                {
                    return; //DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Name = "When Harry Met Sally",
                        Description = "Harry and Sally have known each other for years, and are very good friends, but they fear sex would ruin the friendship.",
                        Length = 1.35,
                        Genre = "Comedy, Drama, Romance",
                        AdvisedAge = 18,
                        Poster = "https://m.media-amazon.com/images/M/MV5BMjE0ODEwNjM2NF5BMl5BanBnXkFtZTcwMjU2Mzg3NA@@._V1_FMjpg_UX682_.jpg",
                        Language = "English",
                        ThreeD = false
                    },

                    new Movie
                    {
                        Name = "The Lord of the Rings: The Fellowship of the Ring",
                        Description = "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
                        Length = 2.58,
                        Genre = "Action, Adventure, Drama, Fantasy",
                        AdvisedAge = 13,
                        Poster = "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_FMjpg_UY720_.jpg",
                        Language = "English",
                        ThreeD = false
                    },

                    new Movie
                    {
                        Name = "Avatar",
                        Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                        Length = 2.42,
                        Genre = "Action, Adventure, Sci-Fi, Fantasy",
                        AdvisedAge = 13,
                        Poster = "https://m.media-amazon.com/images/M/MV5BMTYwOTEwNjAzMl5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1_FMjpg_UX510_.jpg",
                        Language = "English",
                        ThreeD = true
                    },

                    new Movie
                    {
                        Name = "Joker",
                        Description = "In Gotham City, mentally troubled comedian Arthur Fleck is disregarded and mistreated by society. He then embarks on a downward spiral of revolution and bloody crime. This path brings him face-to-face with his alter-ego: the Joker.",
                        Length = 2.02,
                        Genre = "Crime, Drama, Thriller",
                        AdvisedAge = 18,
                        Poster = "https://m.media-amazon.com/images/M/MV5BNGVjNWI4ZGUtNzE0MS00YTJmLWE0ZDctN2ZiYTk2YmI3NTYyXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_FMjpg_UY720_.jpg",
                        Language = "English",
                        ThreeD = false
                    },

                    new Movie
                    {
                        Name = "Gravity",
                        Description = "Two astronauts work together to survive after an accident leaves them stranded in space.",
                        Length = 1.31,
                        Genre = "Action, Drama, Sci-Fi",
                        AdvisedAge = 13,
                        Poster = "https://m.media-amazon.com/images/M/MV5BNjE5MzYwMzYxMF5BMl5BanBnXkFtZTcwOTk4MTk0OQ@@._V1_FMjpg_UX680_.jpg",
                        Language = "English",
                        ThreeD = true
                    },

                   new Movie
                   {
                       Name = "The Dark Knight",
                       Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                       Length = 2.32,
                       Genre = "Action, Drama, Crime",
                       AdvisedAge = 13,
                       Poster = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_FMjpg_UY720_.jpg",
                       Language = "English",
                       ThreeD = false
                   }

                    );
                context.SaveChanges();

            }
        }
    }
}
