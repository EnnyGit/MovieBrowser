using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using movie_browser.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace movie_browser.Data
{
    public class ApiDataAccess : IApiDataAccess
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string APIKey = "8e0c2dde51210286d3e656c9b7bc0181";
        private readonly HttpClient _client;

        public ApiDataAccess(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("meta");
        }
        public async Task<Tuple<MovieModel, string>> GetLatestMovie()
        {
            string errorString;
            MovieModel movie = null; 

            try
            {
                movie = await _client.GetFromJsonAsync<MovieModel>($"latest?api_key={APIKey}");
                errorString = null;
                return Tuple.Create(movie, errorString);
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movie: { e.Message }";
            }

            return Tuple.Create(movie, errorString);
        }

        public async Task<Tuple<List<MovieModel>, string>> GetTopratedMovies(int pageNum = 2)
        {
            string errorString;
            List<MovieModel> movies = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"top_rated?api_key={APIKey}&page={pageNum}");
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    movies = JObject.Parse(jsonString)["results"].ToObject<List<MovieModel>>();
                    return Tuple.Create(movies, "");
                }
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movie: { e.Message }";
                return Tuple.Create(movies, errorString);
            }
            return null;
        }

        public async Task<Tuple<List<MovieModel>, string>> GetPopularMovies(int pageNum = 1)
        {
            string errorString;
            List<MovieModel> movies = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"popular?api_key={APIKey}&page={pageNum}");
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    movies = JObject.Parse(jsonString)["results"].ToObject<List<MovieModel>>();
                    return Tuple.Create(movies, "");
                }
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movie: { e.Message }";
                return Tuple.Create(movies, errorString);
            }
            return null;
        }

        public async Task<Tuple<List<MovieModel>, string>> SearchMovies(string param)
        {
            string errorString;
            List<MovieModel> movies = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"movie?api_key={APIKey}&query={param}");
                //HttpResponseMessage response = await _client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key=8e0c2dde51210286d3e656c9b7bc0181&query={param}");
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    movies = JObject.Parse(jsonString)["results"].ToObject<List<MovieModel>>();
                    return Tuple.Create(movies, "");
                }
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movie: { e.Message }";
                return Tuple.Create(movies, errorString);
            }
            return null;
        }

        public async Task<Tuple<MovieModel, string>> GetMovieById(int id)
        {
            string errorString;
            MovieModel movie = null;

            try
            {
                movie = await _client.GetFromJsonAsync<MovieModel>($"{id}?api_key={APIKey}&language=en-US");
                errorString = null;
                return Tuple.Create(movie, errorString);
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movie: { e.Message }";
            }

            return Tuple.Create(movie, errorString);
        }
    }
}
