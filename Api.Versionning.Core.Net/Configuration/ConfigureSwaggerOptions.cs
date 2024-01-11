using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Versionning.Core.Net.Configuration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            //configuration du swagger pour la découverte des versions des apis (dynamiquement)
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                //on balaye tous les controllers avec l'apiVersion pour les lister dans le swagger ui
                options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));
            }

        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "TEST de ma version API",
                Version = description.ApiVersion.ToString()
            };

            //on peut gérer la fin de vie d'une version d'api (deprecated)
            if (description.IsDeprecated)
            {
                info.Description = "Fin de vie pour cette API";
            }

            return info;
        }
    }
}
