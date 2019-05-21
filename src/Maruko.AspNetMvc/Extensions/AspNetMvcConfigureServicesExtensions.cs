using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Maruko.AspNetMvc.Filters;
using Maruko.AspNetMvc.Jwt;
using Maruko.AspNetMvc.Jwt.Model;
using Maruko.AspNetMvc.Models;
using Maruko.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Maruko.AspNetMvc.Extensions
{
    public static class AspNetMvcConfigureServicesExtensions
    {
        public static void AddConfigureServices(this IServiceCollection service,
            [CanBeNull] Action<IServiceCollection> action = null)
        {
            service.AddConfigureServices(action, true, true, true, true);
        }

        public static void AddConfigureServices(this IServiceCollection service,
            Action<IServiceCollection> action,
            bool isEnableMvc = true,
            bool isEnableSwagger = false,
            bool isEnableJwt = false,
            bool isEnableCors = false
            )
        {
            if (isEnableJwt)
                AddJwt(service);
            if (isEnableMvc)
                AddMvc(service);
            if (isEnableSwagger)
                AddSwagger(service);
            if (isEnableCors)
                AddCors(service);

            action(service);
        }

        private static void AddJwt(IServiceCollection service)
        {
            service.Configure<JwtSettings>(ConfigurationSetting.DefaultConfiguration.GetSection(nameof(JwtSettings)));
            var setting = new JwtSettings();
            ConfigurationSetting.DefaultConfiguration.Bind(nameof(JwtSettings), setting);

            //todo 添加jwt认证
            service.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {

                config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudience = setting.Audience,
                    ValidIssuer = setting.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(1),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };

                config.SecurityTokenValidators.Clear();
                config.SecurityTokenValidators.Add(new TokenValidate());
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Headers["Authorization"];
                        context.Response.Headers.Add("Token-Expired", "true");
                        context.Token = token.FirstOrDefault();
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void AddMvc(IServiceCollection service)
        {
            service.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
                options.Filters.Add(new ValidateModelAttribute());
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private static void AddSwagger(IServiceCollection service)
        {
            const string projectName = "小说/漫画站群";

            service.AddSwaggerGen(c =>
            {
                //设置版本信息
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        Version = version,
                        Title = $"{projectName}.{version} 接口文档",
                        Description = $"HTTP API for {projectName}.{version}",
                        TermsOfService = "None",
                    });
                });
                //全局Token验证说明
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                //Json Token认证方式，此方式为全局添加
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() }
                });

                c.OperationFilter<SwaggerFileUploadFilter>();

                //设置说明 
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Wl.Station.Baby.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
                var xmlAppPath = Path.Combine(basePath, "Wl.Station.Baby.Application.xml");
                c.IncludeXmlComments(xmlAppPath);
            });
        }

        private static void AddCors(IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("cors", p => p.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
        }
    }
}
