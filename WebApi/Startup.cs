using Application.Items.Queries;
using Application.Core;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Services;
using Microsoft.IdentityModel.Logging;
using System.Net;
using WebApi.Filters;
using Hangfire;
using Hangfire.SqlServer;
using WebApi.Middleware;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Events;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IdentityModelEventSource.ShowPII = true;

            services.AddControllers();

            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = _configuration["Auth0:Authority"];
                options.Audience = _configuration["Auth0:Audience"];
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            services.AddDbContext<ItemDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(Mapper).Assembly);


            Account account = new Account(_configuration["Cloudinary:Name"], _configuration["Cloudinary:ApiKey"], _configuration["Cloudinary:ApiSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            services.AddSingleton<IImageUpload, ImageUpload>(_ => new ImageUpload(cloudinary));


            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_configuration.GetConnectionString("HangFire"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();

            services.AddScoped<Redeemer>();

            services.AddScoped<PointGiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            app.UseHangfireDashboard();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSerilogRequestLogging(options => {
                options.MessageTemplate = "Handled {RequestPath}";
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard(); ///localhost:44360/hangfire
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("API is working");
                });
            });
        }
    }
}
