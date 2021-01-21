using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary
{
    public class LogicHandler
    {
        public List<Movie> SortMovies(List<Movie> movies, bool ascending)
        {
            var orderedMovies = movies;
            if (ascending)
            {
                return orderedMovies.OrderBy(a => a.Rated).ToList();
            }
            return orderedMovies.OrderByDescending(a => a.Rated).ToList();
        }

        public List<Movie> ParseTop100JsonToMovieObjects(string json)
        {
            var movies = new List<Movie>();
            try
            {
                var parsedJson = JsonConvert.DeserializeObject<List<Topp100JsonMovieModel>>(json);
                foreach (var parsedMovie in parsedJson)
                {
                    movies.Add(new Movie()
                    {
                        Id = parsedMovie.id,
                        Title = parsedMovie.title,
                        Rated = double.Parse(parsedMovie.rated)
                    });
                }
            }
            catch (Exception ex)
            {
                if(ex is FormatException)
                {
                    throw new FormatException();
                }

                if(ex is ArgumentNullException)
                {
                    throw new ArgumentNullException();
                }
            }

            return movies;
        }

        public List<Movie> ParseDetailedJsonToMovieObjects(string json)
        {
            var movies = new List<Movie>();
            try
            {
                var parsedJson = JsonConvert.DeserializeObject<List<DetailedJsonMovieModel>>(json);
                foreach (var parsedMovie in parsedJson)
                {
                    movies.Add(new Movie()
                    {
                        Id = parsedMovie.id,
                        Title = parsedMovie.title,
                        Rated = parsedMovie.imdbRating
                    });
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    throw new FormatException();
                }

                if (ex is ArgumentNullException)
                {
                    throw new ArgumentNullException();
                }
            }

            return movies;
        }

        public Movie GetSingleMovie(List<Movie> movies, string id)
        {
            var moviesToFilterThrough = movies;
            var filteredMovie = movies.Where(i => i.Id == id).FirstOrDefault();         
            if(filteredMovie == null)
            {
                throw new ArgumentNullException();
            }
            return filteredMovie;
        }
    }
}
