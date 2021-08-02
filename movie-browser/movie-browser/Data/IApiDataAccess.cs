using movie_browser.Models;
using System;
using System.Threading.Tasks;

namespace movie_browser.Data
{
    public interface IApiDataAccess
    {
        Task<Tuple<MovieModel, string>> GetMovie();
    }
}