using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quartz;
using WebDevelopment.API.Middleware;
using WebDevelopment.API.Security;
using WebDevelopment.API.Extensions;
using WebDevelopment.Common.Requests.User.Validators;
using WebDevelopment.HostClient;
using WebDevelopment.HostClient.Implementation;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Settings;
using WebDevelopment.Infrastructure;
using WebDevelopment.Domain;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<WebDevelopmentContext>(
    options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
    );

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebDevelopment API",
        Version = "v1"
    });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert ApiKey into the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(ApiKeyAuthOptions.DefaultScheme)
    .AddApiKeyAuth(autoOptions => { autoOptions.ApiKey = builder.Configuration["Authorization:ApiKey"]; });

builder.Services.AddValidatorsFromAssemblyContaining<BaseUserValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<ITaskExpirationWorker, TaskExpirationWorker>();
builder.Services.AddTransient<EmailProviderSetupFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.AddJobAndTrigger<TaskExpirationNotificationJob>(builder.Configuration);
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);



builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
    loggingBuilder.AddLog4Net("log4net.config");
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();