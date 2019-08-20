using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMovies.DataAccess.Entities.Movies
{
    public class jm_TVCharacter: jm_Character
    {

        /// <summary>
        /// Count of episodes the character was in
        /// </summary>
        [MaxLength(4)]
        public int EpisodeCount { get; set; }

        /// <summary>
        /// Character first appereance year
        /// </summary>
        [MaxLength(4)]
        public int? StartYear { get; set; }

        /// <summary>
        /// Character last appereance year
        /// </summary>
        [MaxLength(4)]
        public int? EndYear { get; set; }
    }
}
