using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AwesomeLogging
{
    public static class FloggerConfigurationExtensions
    {
        public static LoggerConfiguration ConfigureLogFile(this LoggerConfiguration config, string rollingFilePath, string file)
        {
            if (String.IsNullOrWhiteSpace(rollingFilePath))
            {
                return config;
            }

            return config.WriteTo.File(rollingFilePath + file + ".txt", rollingInterval: RollingInterval.Day, shared: true);
        }

        public static LoggerConfiguration ConfigureElasticSearch(this LoggerConfiguration config, ICollection<string> elasticSearchUris, string elasticSearchUsername, string elasticSearchPassword, string indexPrefix)
        {
            if (elasticSearchUris == null || elasticSearchUris.Count == 0)
            {
                return config;
            }
            var uris = elasticSearchUris.Select(x => new Uri(x));
            return config.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(uris)
            {
                IndexFormat = indexPrefix + "-{0:yyyy.MM.dd}",
                InlineFields = true,
                ModifyConnectionSettings = x => x.BasicAuthentication(elasticSearchUsername, elasticSearchPassword)
            });
        }

        public static LoggerConfiguration ConfigureConsole(this LoggerConfiguration config, bool writeToConsole)
        {
            if (!writeToConsole)
            {
                return config;
            }
            return config.WriteTo.Console();
        }
    }
}
