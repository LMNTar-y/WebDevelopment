using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using WebDevelopment.Infrastructure.Repos;
using WebDevelopment.API.Services;
using WebDevelopment.API.Swagger;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<WebDevelopmentContext>(
    options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
    );

builder.Services.AddVersioning();
builder.Services.AddSwagger();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddApiKeyAuth(ApiKeyAuthOptions.DefaultScheme, autoOptions => { autoOptions.ApiKey = builder.Configuration["Authorization:ApiKey"]; })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddValidatorsFromAssemblyContaining<BaseUserValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<ITaskExpirationWorker, TaskExpirationWorker>();
builder.Services.AddTransient<EmailProviderSetupFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthUserModelRepo, AuthUserModelRepo>();
builder.Services.AddTransient<ILoginService, LoginService>();


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
    var versions = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger(versions);
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();