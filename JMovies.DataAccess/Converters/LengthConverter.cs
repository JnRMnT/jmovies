using JetBrains.Annotations;
using JMovies.DataAccess.Entities.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JMovies.DataAccess.Converters
{
    public class LengthConverter : ValueConverter<jm_Length, int>
    {
        public LengthConverter(ConverterMappingHints mappingHints = default)
        : base(ConvertTo, ConvertFrom, mappingHints)
        {
        }

        static Expression<Func<jm_Length, int>> ConvertTo = x => x.Metric;
        static Expression<Func<int, jm_Length>> ConvertFrom = x => new jm_Length(x);
    }
}
