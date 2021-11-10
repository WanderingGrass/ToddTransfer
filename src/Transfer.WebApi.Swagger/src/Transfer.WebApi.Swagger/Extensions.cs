using System;
using Transfer.Docs.Swagger;
using Transfer.WebApi.Swagger.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.WebApi.Swagger
{
    public static class Extensions
    {
        private const string SectionName = "swagger";

        public static ITransferBuilder AddWebApiSwaggerDocs(this ITransferBuilder builder, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            return builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(sectionName));
        }
        
        public static ITransferBuilder AddWebApiSwaggerDocs(this ITransferBuilder builder, 
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(buildOptions));
        
        public static ITransferBuilder AddWebApiSwaggerDocs(this ITransferBuilder builder, SwaggerOptions options)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(options));
        
        private static ITransferBuilder AddWebApiSwaggerDocs(this ITransferBuilder builder, Action<ITransferBuilder> registerSwagger)
        {
            registerSwagger(builder);
            builder.Services.AddSwaggerGen(c => c.DocumentFilter<WebApiDocumentFilter>());
            return builder;
        }
    }
}