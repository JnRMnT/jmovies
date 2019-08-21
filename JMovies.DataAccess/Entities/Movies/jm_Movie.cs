using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Entity Definition for jm_Movie 
    /// </summary>
    [Table("Movie")]
    public class jm_Movie : jm_Production
    {
        /// <summary>
        /// Original Title of the movie
        /// </summary>
        [MaxLength(128)]
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Plot Summary of the movie
        /// </summary>
        [MaxLength(512)]
        public string PlotSummary { get; set; }

        /// <summary>
        /// Story Line of the movie
        /// </summary>
        public string StoryLine { get; set; }

        /// <summary>
        /// Credits of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Credit> Credits { get; set; }

        /// <summary>
        /// Tag Lines of the movie
        /// </summary>
        [MaxLength(128)]
        public ICollection<string> TagLines { get; set; }

        /// <summary>
        /// Keywords of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Keyword> Keywords { get; set; }

        /// <summary>
        /// Genres of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Genre> Genres { get; set; }

        /// <summary>
        /// Official Sites of the movie
        /// </summary>
        [MaxLength(512)]
        public ICollection<jm_OfficialSite> OfficialSites { get; set; }

        /// <summary>
        /// Countries of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Country> Countries { get; set; }

        /// <summary>
        /// Languages of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Language> Languages { get; set; }

        /// <summary>
        /// Release dates of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_ReleaseDate> ReleaseDates { get; set; }

        /// <summary>
        /// Alternative names of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_AKA> AKAs { get; set; }

        /// <summary>
        /// Filming Locations of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        [MaxLength(256)]
        public ICollection<string> FilmingLocations { get; set; }

        /// <summary>
        /// Budget of the movie
        /// </summary>
        [MaxLength(256)]
        public jm_Budget Budget { get; set; }

        /// <summary>
        /// Production Companies of the movie
        /// </summary>
        [ForeignKey("ProductionID")]
        public ICollection<jm_Company> ProductionCompanies { get; set; }

        /// <summary>
        /// Length of the movie
        /// </summary>
        public TimeSpan Runtime { get; set; }
    }
}
