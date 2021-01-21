using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MovieLibrary
{
    public class MoviesClient
    {
        private static MoviesClient instance = null;
        private HttpClient client = new HttpClient();
        private LogicHandler logicHandler = new LogicHandler();

        public static MoviesClient GetInstance()
        {
            if (instance == null)
            {
                instance = new MoviesClient();
            }
            return instance;
        }

        public List<Movie> GetMovies()
        {
            var topp100Response = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var detailedResponse = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;

            List<Movie> movies = new List<Movie>();
            List<Movie> topp100Movies = new List<Movie>();
            List<Movie> detailedMovies = new List<Movie>();
            if (topp100Response.IsSuccessStatusCode)
            {
                topp100Movies = logicHandler.ParseTop100JsonToMovieObjects(topp100Response.Content.ReadAsStringAsync().Result);
            }
            if (detailedResponse.IsSuccessStatusCode)
            {
                detailedMovies = logicHandler.ParseDetailedJsonToMovieObjects(detailedResponse.Content.ReadAsStringAsync().Result);
            }

            movies.AddRange(topp100Movies);
            movies.AddRange(detailedMovies);
            return movies.GroupBy(i => i.Title).Select(a => a.First()).ToList();
        }
    }
}
