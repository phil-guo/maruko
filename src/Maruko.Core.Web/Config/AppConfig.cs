using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Maruko.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Maruko.Core.Web.Config
{
    public class AppConfig
    {
        public WebOption Web { get; set; }
        public SwaggerOption Swagger { get; set; }

        private IConfiguration configuration => ServiceLocator.Current?.Resolve<IConfiguration>() ?? ServiceLocator.Configuration;

        public AppConfig()
        {
            var section = configuration?.GetSection(nameof(Web));
            Web = section.Exists()
                ? section.Get<WebOption>()
                : new WebOption();

            Swagger = section.Exists()
                ? section.Get<SwaggerOption>()
                : new SwaggerOption();
        }
    }
}
