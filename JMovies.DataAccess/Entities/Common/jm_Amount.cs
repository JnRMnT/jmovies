using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMovies.DataAccess.Entities.Common
{
    /// <summary>
    /// Entity definition of Money related Amounts
    /// </summary>
    public class jm_Amount
    {
        /// <summary>
        /// Decimal value of the amount
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Currency of the amount
        /// </summary>
        [MaxLength(3)]
        public string Currency { get; set; }
    }
}
