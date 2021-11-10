using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Docs.Swagger
{
    public interface ISwaggerOptionsBuilder
    {
        ISwaggerOptionsBuilder Enable(bool enabled);
        ISwaggerOptionsBuilder ReDocEnable(bool reDocEnabled);
        ISwaggerOptionsBuilder WithName(string name);
        ISwaggerOptionsBuilder WithTitle(string title);
        ISwaggerOptionsBuilder WithVersion(string version);
        ISwaggerOptionsBuilder WithRoutePrefix(string routePrefix);
        ISwaggerOptionsBuilder IncludeSecurity(bool includeSecurity);
        ISwaggerOptionsBuilder SerializeAsOpenApiV2(bool serializeAsOpenApiV2);
        ISwaggerOptionsBuilder WithMiniProfiler(bool miniProfiler);
        SwaggerOptions Build();
    }
}
