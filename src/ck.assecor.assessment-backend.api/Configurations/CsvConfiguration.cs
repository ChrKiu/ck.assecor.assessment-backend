using ck.assecor.assessment_backend.data.CsvContext;
using Microsoft.Extensions.Configuration;
using System;

namespace ck.assecor.assessment_backend.api.Configurations
{
    public class CsvConfiguration : ICsvConfiguration
    {
        public string Path { get; private set; }

        public CsvConfiguration(IConfiguration configuration)
        {
            var location = configuration["DataSources:PersonCsv:Location"];
            if(location == "")
            {
                location = AppDomain.CurrentDomain.BaseDirectory;
            }
            Path = $"{location}\\{configuration["DataSources:PersonCsv:Filename"]}";
        }
    }
}
