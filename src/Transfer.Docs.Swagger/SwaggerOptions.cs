using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Docs.Swagger
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; }
        public bool ReDocEnabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool IncludeSecurity { get; set; }
        public bool SerializeAsOpenApiV2 { get; set; }
        public bool MiniProfiler { get; set; }
    }
}
