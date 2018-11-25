using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.Entities.IMDB
{
    public class TVCharacter : Character
    {
        public int EpisodeCount { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
    }
}
