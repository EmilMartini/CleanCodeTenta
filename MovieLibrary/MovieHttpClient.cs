using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MovieLibrary
{
    public class MovieHttpClient
    {
        private static MovieHttpClient instance = null;
        private HttpClient client = new HttpClient();
        private LogicHandler logicHandler = new LogicHandler();

        public static MovieHttpClient GetInstance()
        {
            if (instance == null)
            {
                instance = new MovieHttpClient();
            }
            return instance;
        }

        public List<Movie> GetMovies()
        {
            var movies = new List<Movie>();
            var topp100Movies = new List<Movie>();
            var detailedMovies = new List<Movie>();
            var topp100Response = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/topp100.json").Result;
            var detailedResponse = client.GetAsync("https://ithstenta2020.s3.eu-north-1.amazonaws.com/detailedMovies.json").Result;

            if (topp100Response.IsSuccessStatusCode)
            {
                topp100Movies = logicHandler.ParseTop100JsonToMovieObjects(topp100Response.Content.ReadAsStringAsync().Result);
            } else
            {
                throw new HttpRequestException();
            }

            if (detailedResponse.IsSuccessStatusCode)
            {
                detailedMovies = logicHandler.ParseDetailedJsonToMovieObjects(detailedResponse.Content.ReadAsStringAsync().Result);
            } else
            {
                throw new HttpRequestException();
            }

            movies.AddRange(topp100Movies);
            movies.AddRange(detailedMovies);
            return movies.GroupBy(i => i.Title).Select(a => a.First()).ToList();
        }
    }
}
