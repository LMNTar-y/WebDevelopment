using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using WebDevelopment.API.Swagger.Filters;

namespace WebDevelopment.API.Swagger;

public static class ServiceCollectionSwaggerExtensions
{
    ///<summary>
    /// Setup API controller Versioning
    /// </summary>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(
            opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, default);
            });

        services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            }
        );

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var desc = "API created for educational purpose.";
        var url = "https://github.com/LMNTar-y";
        var apiVersionProvider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

        services.AddSwaggerGen(opt =>
        {
            foreach (var description in apiVersionProvider.ApiVersionDescriptions)
            {
                opt.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = "WebDevelopmentEdu API",
                    Description = description.IsDeprecated ? desc + " This API version has been deprecated" : desc,
                    Version = description.ApiVersion.ToString(),
                    Contact = new OpenApiContact
                    {
                        Email = "a.balvanovich@gmail.com",
                        Name = "Aliaksei Balvanovich",
                        Url = new Uri(url)
                    }
                });
            }
            opt.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert ApiKey into the field",
                Name = "Authorization",
                Scheme = "ApiKey",
                Type = SecuritySchemeType.ApiKey
            });

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input bearer token to access this API",
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    new string[] {}
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new[] { "WebDevelopment API" }
                }
            });

            opt.OperationFilter<RemoveQueryApiVersionParamOperationFilter>();
            opt.OperationFilter<AddSchemaToParamsWithoutItOperationFilter>();
            opt.DocumentFilter<RemoveDefaultApiVersionRouteDocumentFilter>();
            opt.DocumentFilter<TagDescriptionsDocumentFilter>();
            opt.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return services;
    }
}