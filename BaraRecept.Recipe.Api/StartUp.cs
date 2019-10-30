using System.IO;
using BaraRecept.Recipe.Api.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using BaraRecept.Recipe.Contracts.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaraRecept.Recipe.Api.Services;
using BaraRecept.Recipe.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace BaraRecept.Recipe.Api
{
    /// <summary>
    /// Bootstraps the application
    /// </summary>
    public class StartUp
    {
        private readonly string _applicationName = PlatformServices.Default.Application.ApplicationName;
        private readonly string _applicationVersion = PlatformServices.Default.Application.ApplicationVersion;

        /// <summary>
        /// Configuration root
        /// </summary>
        public IConfigurationRoot Configuration { get; set; }

        private IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Creates instance of StartUp
        /// </summary>
        public StartUp(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        /// <summary>
        /// Configure services
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();

            services.AddOptions();
            services.Configure<MockOptions>(Configuration.GetSection("MockOptions"));
            services.Configure<RecipeDataOptions>(Configuration.GetSection("MockOptions"));

            var instanceId = Configuration["DieScheite:ServiceInstanceId"] ?? "01";
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RecipeService, RecipeService>();
            services.AddScoped<RecipeDataOptions, RecipeDataOptions>();

            /*
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetSection("MockOptions").GetConnectionString("ConnectionString")));

    */
            services
                .AddMvc()
                .AddMetrics();

            var pathToDoc = Configuration["Swagger:FileName"];
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "docs",
                    new Info
                    {
                        Title = _applicationName,
                        Version = _applicationVersion
                    });
                var filePath = Path.Combine(
                    PlatformServices.Default.Application.ApplicationBasePath,
                    pathToDoc);
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();
            });
        }

        /// <summary>
        /// Configure
        /// </summary>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env
        )
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/docs/swagger.json", _applicationName);
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
