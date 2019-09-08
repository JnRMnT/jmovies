using JMovies.IMDb.Entities.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.DataAccess.Helpers
{
    public class ProductionQueryHelper
    {

        public static IIncludableQueryable<Production, ICollection<TagLine>> GetResolvedProductionQuery(JMoviesEntities entities)
        {
            return entities.Production.Include(e => e.Rating).ThenInclude(e => e.DataSource)
                                .Include(e => ((Movie)e).ReleaseDates)
                                .Include(e => ((Movie)e).AKAs)
                                .Include(e => ((Movie)e).Countries)
                                .Include(e => ((Movie)e).Credits).ThenInclude(e => ((ActingCredit)e).Person).ThenInclude(e => e.PrimaryImage)
                                .Include(e => ((Movie)e).Credits).ThenInclude(e => ((ActingCredit)e).Characters)
                                .Include(e => ((Movie)e).Genres)
                                .Include(e => ((Movie)e).Keywords)
                                .Include(e => ((Movie)e).Languages)
                                .Include(e => ((Movie)e).ProductionCompanies)
                                .Include(e => ((Movie)e).ReleaseDates)
                                .Include(e => ((Movie)e).Poster)
                                .Include(e => ((Movie)e).MediaImages)
                                .Include(e => ((Movie)e).TagLines);
        }
    }
}
