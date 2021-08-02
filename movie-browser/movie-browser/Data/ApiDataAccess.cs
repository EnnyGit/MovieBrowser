using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using movie_browser.Models;

namespace movie_browser.Data
{
    public class ApiDataAccess : IApiDataAccess
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string APIKey = "8e0c2dde51210286d3e656c9b7bc0181";

        public ApiDataAccess(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<Tuple<MovieModel, string>> GetMovie()
        {
            var client = _clientFactory.CreateClient("meta");
            string errorString;
            MovieModel movie = null; 

            try
            {
                movie = await client.GetFromJsonAsync<MovieModel>($"latest?api_key={APIKey}");
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
