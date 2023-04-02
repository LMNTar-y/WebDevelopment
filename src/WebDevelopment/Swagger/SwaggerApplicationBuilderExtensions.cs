using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebDevelopment.API.Swagger;

public static class SwaggerApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider versions)
    {
        if (app is null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        app.UseSwagger(opt =>
        {
            opt.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                //Clear "servers" element in swagger.json because it got the wrong port when hosted behind reverse proxy
                swagger.Servers.Clear();
            });
        });

        app.UseSwaggerUI(opt =>
        {
            opt.AssignVersionToSwaggerEndpoint(versions);
        });

        return app;
    }
}