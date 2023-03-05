using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Solutis.Business;
using Solutis.Business.Implementations;
using WebMySQL.Models;
using Solutis.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Solutis.Services;
using Solutis.Repositories;
using Prometheus;
using Prometheus.DotNetRuntime;

namespace Solutis
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public static IDisposable Collector;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" }
                    }, new List<string>() }
                 });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API REST with ASP NET CORE 3.0 ",
                    Description = "REST API with ASP NET CORE 3.0 and Bearer for user authentication. This API has public and private resources",
                    Contact = new OpenApiContact
                    {
                        Name = "Elayne Natália de Oliveira Argollo",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/elayneargollo"),
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);


            });

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            string stringConnection = Configuration["MySQLConnection:MySQLConnection"];

            services.AddDbContext<Contexto>(options =>
            options.UseMySQL(stringConnection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(stringConnection);
            }

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IUserBusiness, UserBusinessImplementation>();
            services.AddScoped<IPurchaseBusiness, PurchaseBusinessImplementation>();
            services.AddScoped<IBookRepository, BookClientService>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseMetricServer();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API REST with ASP NET CORE 3.1");
                c.RoutePrefix = "swagger";
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });
        }

        public void MigrateDatabase(string connection)
        {
            try
            {
                var envolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var envolve = new Evolve.Evolve(envolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = false,
                };
                envolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }

        public static IDisposable CreateCollector()
        {
            var builder = DotNetRuntimeStatsBuilder.Default();

            builder = DotNetRuntimeStatsBuilder
                        .Customize()
                        .WithContentionStats(CaptureLevel.Informational)
                        .WithGcStats(CaptureLevel.Informational)
                        .WithExceptionStats(CaptureLevel.Errors)
                        .WithThreadPoolStats(CaptureLevel.Informational)
                        .WithJitStats();

            //builder.RecycleCollectorsEvery(new TimeSpan(0, 20,0));
            return builder
                .StartCollecting();
        }
    }
}
