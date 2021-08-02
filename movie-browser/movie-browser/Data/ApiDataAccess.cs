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

        public async Task<Tuple<List<MovieModel>, string>> GetTopratedMovies()
        {
            string errorString;
            List<MovieModel> movies = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"top_rated?api_key={APIKey}");
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

        public async Task<Tuple<MovieModel, string>> GetPopularMovies(int pageNum = 1)
        {
            string errorString;
            MovieModel movie = null;

            try
            {
                movie = await _client.GetFromJsonAsync<MovieModel>($"popular?api_key={APIKey}&page={pageNum}");
                errorString = null;
                return Tuple.Create(movie, errorString);
            }
            catch (Exception e)
            {
                errorString = $"There was an error getting the movies: { e.Message }";
            }

            return Tuple.Create(movie, errorString);
        }
    }
}
