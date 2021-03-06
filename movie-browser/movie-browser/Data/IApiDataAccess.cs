using movie_browser.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace movie_browser.Data
{
    public interface IApiDataAccess
    {
        Task<Tuple<MovieModel, string>> GetLatestMovie();
        Task<Tuple<List<MovieModel>, string>> GetTopratedMovies(int pageNum);
        Task<Tuple<List<MovieModel>, string>> GetPopularMovies(int pageNum);
        Task<Tuple<MovieModel, string>> GetMovieById(int id);
    }
}