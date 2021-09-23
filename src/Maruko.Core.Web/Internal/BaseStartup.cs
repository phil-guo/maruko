using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.Web.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace Maruko.Core.Web
{
    public abstract class BaseStartup
    {
        public BaseStartup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            ServiceLocator.Configuration = configuration;
        }

        public virtual List<string> XmlFiles => new List<string>();

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceLocator.ServiceCollection = services;

            var app = new AppConfig();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Web.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,    
                    ValidIssuer = app.Web.Key,
                    ValidAudience = app.Web.Key,
                };
            });

            services.AddControllers(controller =>
                {
                    controller.Filters.Add(new AuthorizeFilter());
                    controller.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .AddNewtonsoftJson(opt => opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");


            services.AddSwaggerGen(option =>
            {
                var app = new AppConfig();

                option.SwaggerDoc(app.Swagger.Version,
                    new OpenApiInfo { Title = app.Swagger.Title, Version = app.Swagger.Version });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                XmlFiles.ForEach(file =>
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, file);
                    //添加控制器层注释，true表示显示控制器注释
                    option.IncludeXmlComments(xmlPath, true);
                });
            });

            services.AddCors(option =>
            {
                option.AddPolicy("cors",
                    policy => policy.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowAnyMethod()
                        .AllowAnyOrigin());
            });

            services.ConfigureMarukoServices(ServiceLocator.Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModules(ServiceLocator.Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseCors("cors");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            var root = Path.Combine(env.ContentRootPath, "uploads");
            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "uploads")),
                RequestPath = "/uploads"
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });

            app.UseMaruko();
        }
    }
}