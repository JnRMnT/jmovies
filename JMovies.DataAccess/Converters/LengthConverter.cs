using JetBrains.Annotations;
using JMovies.IMDb.Entities.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JMovies.DataAccess.Converters
{
    public class LengthConverter : ValueConverter<Length, int>
    {
        public LengthConverter(ConverterMappingHints mappingHints = default)
        : base(ConvertTo, ConvertFrom, mappingHints)
        {
        }

        static Expression<Func<Length, int>> ConvertTo = x => x.Metric;
        static Expression<Func<int, Length>> ConvertFrom = x => new Length(x);
    }
}
