using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebDevelopment.API.Swagger.Filters;

public class AddSchemaToParamsWithoutItOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var apiParameter in operation.Parameters)
        {
            apiParameter.Schema ??= new OpenApiSchema{Type = "String"};
        }
    }
}