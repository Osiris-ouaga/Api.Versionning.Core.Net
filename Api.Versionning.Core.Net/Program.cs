using Api.Versionning.Core.Net.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(config=>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    //permet de ne pas avoir de controle de version pour chaque controller
    //(sorte de protection en cas d'implémentation d'un nouveau controller rapide sans versionning)
    config.AssumeDefaultVersionWhenUnspecified = true;
    
    //permet d'utiliser le hearder d'un appel avec des paramètres de versionning d'api (ou deprecated)
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(config =>
{
    //une url sous la forme ....com/api/{vxxx}...
    config.GroupNameFormat = "'v'VVV";
    //permet de changer par mon masque et la version de l'attribut de l'api la route du controller
    config.SubstituteApiVersionInUrl = true;
});


builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //On rempli la liste de swaggerUi avec les versions listées par le service apiversiondescription
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var item in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.ApiVersion.ToString());
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
