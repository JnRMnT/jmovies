using JM.Entities.Framework;
using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using Microsoft.Extensions.DependencyInjection;
using JMovies.Entities.Responses;
using System;
using JMovies.App.ElasticSearch.Interfaces;
using JMovies.Common.Constants;
using Nest;
using SearchRequest = JMovies.Entities.Requests.SearchRequest;
using System.Linq;
using JMovies.Entities.Misc;
using JMovies.Entities.ElasticSearch;

namespace JMovies.App.Business.Actions
{
    public class Search : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            SearchRequest requestMessage = request as SearchRequest;
            SearchResponse responseMessage = response as SearchResponse;

            IElasticSearchConnectionProvider elasticSearchConnectionProvider = serviceProvider.GetRequiredService<IElasticSearchConnectionProvider>();
            ElasticClient productionsClient = elasticSearchConnectionProvider.GetElasticClient(ElasticSearchIndexNameConstants.Productions);
            var searchResponse =
                 productionsClient.Search<Production>(s => s
                 .From(0).Take(10).Query(q => q.MatchPhrase(e => e.Query(requestMessage.SearchKey))));
            if (searchResponse.IsValid)
            {
                responseMessage.SearchResults = searchResponse.Hits.Where(e => e.Score >= searchResponse.MaxScore * 0.7).Select(e => new
                 SearchResult
                {
                    Source = new Production
                    {
                        ID = e.Source.ID,
                        IMDbID = e.Source.IMDbID,
                        Title = e.Source.Title,
                        Year = e.Source.Year,
                        ProductionType = e.Source.ProductionType
                    }
                }).ToArray();
            }
        }
    }
}
