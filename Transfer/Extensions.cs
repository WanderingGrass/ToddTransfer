using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Transfer
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
             where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }

        public static TModel GetOptions<TModel>(this ITransferBuilder builder, string settingsSectionName)
            where TModel : new()
        {
            using var serviceProvider = builder.Services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<TModel>(settingsSectionName);
        }
        public static string Underscore(this string value)
          => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
              .ToLowerInvariant();
    }
}
