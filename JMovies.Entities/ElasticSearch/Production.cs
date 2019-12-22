using JMovies.IMDb.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.ElasticSearch
{
    public class Production
    {
        public long ID { get; set; }
        public long IMDbID { get; set; }
        public ProductionTypeEnum ProductionType { get; set; }

        public string OriginalTitle { get; set; }

        public string[] Genres { get; set; }

        public string[] AKAs { get; set; }

        public string[] Keywords { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }
    }
}
