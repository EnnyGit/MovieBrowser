using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace movie_browser.Models
{
    public class MovieModel
    {
        public object Backdrop_path { get; set; }
        public GenreModel[] Genres { get; set; }
        public int Id { get; set; }
        public object Imdb_id { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public float Popularity { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public int Runtime { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public float Vote_average { get; set; }
        public int Vote_count { get; set; }
    }
}
