using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebDevelopment.API.Swagger.Filters;

public class RemoveDefaultApiVersionRouteDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var apiDescription in context.ApiDescriptions)
        {
            var versionParam = apiDescription.ParameterDescriptions
                .FirstOrDefault(p => p.Name == "api-version" && 
                                     p.Source.Id.Equals("Query", StringComparison.InvariantCultureIgnoreCase));

            if (versionParam == null) { continue;}

            var route = "/" + apiDescription.RelativePath?.TrimEnd('/');
            swaggerDoc.Paths.Remove(route);
        }
    }
}