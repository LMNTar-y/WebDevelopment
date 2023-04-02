using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebDevelopment.API.Swagger;

public static class SwaggerUiExtensions
{
    private const string Route = "/swagger/{0}/swagger.json";
    private const string Name = "{0}";

    private const string PathToUi = "swagger";

    public static void AssignVersionToSwaggerEndpoint(this SwaggerUIOptions setupAction,
        IApiVersionDescriptionProvider versions)
    {
        if (setupAction is null)
        {
            throw new ArgumentNullException(nameof(setupAction));
        }

        if (versions is null)
        {
            throw new ArgumentNullException(nameof(versions));
        }

        setupAction.RoutePrefix = PathToUi;

        foreach (var groupName in versions.ApiVersionDescriptions.Select(x => x.GroupName))
        {
            setupAction.SwaggerEndpoint(
                string.Format(Route, groupName),
                string.Format(Name, groupName.ToUpperInvariant())
                );
        }
    }
}