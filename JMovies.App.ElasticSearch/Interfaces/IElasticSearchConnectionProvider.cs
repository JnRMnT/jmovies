using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.App.ElasticSearch.Interfaces
{
    public interface IElasticSearchConnectionProvider
    {
        ElasticClient GetElasticClient(string indexName);
    }
}
