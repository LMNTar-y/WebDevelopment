using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebDevelopment.API.Middleware;
using WebDevelopment.API.Model;
using WebDevelopment.API.Model.Validators;
using WebDevelopment.API.Security;
using WebDevelopment.API.Services;
using WebDevelopment.HostClient;
using WebDevelopment.HostClient.Implementation;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.HostClient.Model;
using WebDevelopment.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<NewUserRequest>, BaseUserValidator>();
builder.Services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISenderClient, EmailClient>();
builder.Services.AddTransient<ITaskExpirationWorker, TaskExpirationWorker>();
builder.Services.AddCronJob<TaskExpirationNotificationService>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = builder.Configuration["Crone:SendEmailPeriod"];
});

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
    loggingBuilder.AddLog4Net("log4net.config");
});

builder.Services.Configure<SmtpClientSetups>(builder.Configuration.GetSection(nameof(SmtpClientSetups)));

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