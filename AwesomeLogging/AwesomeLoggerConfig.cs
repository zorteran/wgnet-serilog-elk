using System.Collections.Generic;

namespace AwesomeLogging
{
    public class AwesomeLoggerConfig
    {
        public string FilePathFormat { get; set; }
        public ICollection<string> ElasticSearchUris { get; set; }
        public bool WriteDiagnostics { get; set; }
        public string ElasticSearchPassword { get; set; }
        public string ElasticSearchUsername { get; set; }
        public bool WriteToConsole { get; set; }
    } 
}
