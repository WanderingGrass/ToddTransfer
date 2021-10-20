using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Docs.Swagger
{
    public class SwaggerPackBase
    {
        public override IServiceCollection AddServices(IServiceCollection services)
        {
            IConfiguration configuration = services.GetConfiguration();

            string url = configuration["OSharp:Swagger:Url"];
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("配置文件中Swagger节点的Url不能为空");
            }

            string title = configuration["OSharp:Swagger:Title"];
            int version = configuration["OSharp:Swagger:Version"].CastTo(1);

            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{version}", new OpenApiInfo() { Title = title, Version = $"{version}" });

                Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
                {
                    options.IncludeXmlComments(file);
                });
                //权限Token
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "请输入带有Bearer的Token，形如 “Bearer {Token}” ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// 应用AspNetCore的服务业务
        /// </summary>
        /// <param name="app">Asp应用程序构建器</param>
        public  void UsePack(IApplicationBuilder app)
        {
            IConfiguration configuration = app.ApplicationServices.GetService<IConfiguration>();

            app.UseSwagger().UseSwaggerUI(options =>
            {
                string url = configuration["OSharp:Swagger:Url"];
                string title = configuration["OSharp:Swagger:Title"];
                int version = configuration["OSharp:Swagger:Version"].CastTo(1);
                options.SwaggerEndpoint(url, $"{title} V{version}");
                bool miniProfilerEnabled = configuration["OSharp:Swagger:MiniProfiler"].CastTo(false);
                if (miniProfilerEnabled)
                {
                    options.IndexStream = () => GetType().Assembly.GetManifestResourceStream("Transfer.Docs.Swagger.index.html");
                }
            });
        }
    }
}
