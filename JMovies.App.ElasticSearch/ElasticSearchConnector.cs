using JMovies.App.ElasticSearch.Interfaces;
using JMovies.Entities.Framework;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.App.ElasticSearch
{
    public class ElasticSearchConnector: IElasticSearchConnectionProvider
    {
        private Dictionary<string, ElasticClient> clients;
        private CustomConfiguration configuration;

        public ElasticSearchConnector(IOptions<CustomConfiguration> configuration)
        {
            this.configuration = configuration.Value;
            clients = new Dictionary<string, ElasticClient>();
        }

        public ElasticClient GetElasticClient(string indexName)
        {
            if (!clients.ContainsKey(indexName))
            {
                var settings = new ConnectionSettings(new Uri(configuration.ElasticSearchConnectionURL)).EnableHttpCompression();
                settings.DefaultIndex(indexName);
                ElasticClient client = new ElasticClient(settings);
                clients.Add(indexName, client);
            }
            return clients.GetValueOrDefault(indexName);
        }
    }
}
