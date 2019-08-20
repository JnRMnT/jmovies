using JMovies.DataAccess.Entities.Common;
using JMovies.IMDb.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.DataAccess.Entities.Movies
{
    /// <summary>
    /// Class definition of Budget of a Title
    /// </summary>
    public class jm_Budget
    {
        /// <summary>
        /// Description related to the budget information
        /// </summary>
        [MaxLength(128)]
        public string Description { get; set; }

        /// <summary>
        /// Amount of the budget of the title
        /// </summary>
        public jm_Amount Amount { get; set; }
        
    }
}
