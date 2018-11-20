using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.IMDB
{
    public class Movie
    {
        public long IMDbID { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string PlotSummary { get; set; }
        public string StoryLine { get; set; }
        public Credit[] Credits { get; set; }
        public int Year { get; set; }
    }
}
