using Cinemark.Application.AutoMapper;
using Cinemark.Application.Middleware;
using Cinemark.Domain.Core.Commom;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.EventBus.Option;
using Cinemark.Infrastructure.HostedServices;
using Cinemark.Infrastructure.Identity.Option;
using Cinemark.Infrastructure.IoC;
using Cinemark.Service.Api.Swagger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Option

builder.Services.AddOptions();

// SqlServer

builder.Services.AddDbContext<SqlServerContext>(option =>
     option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
);

// MongoDB

builder.Services.Configure<MongoConfiguration>(builder.Configuration.GetSection("MongoDbConnection"));

MongoContext.OnModelCreating();

// RabbitMQ

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<FilmeCreatedSubscriber>();
builder.Services.AddHostedService<FilmeUpdatedSubscriber>();
builder.Services.AddHostedService<FilmeRemovedSubscriber>();

// MediatR

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// IoC

InjectorDependency.Register(builder.Services);

// AutoMapper

builder.Services.AddAutoMapper(typeof(DtoToEntityMappingProfile), typeof(EntityToDtoMappingProfile));

// JWT

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});

// Fluent Validation

builder.Services.AddControllers().AddFluentValidation();

// ModelState

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressMapClientErrors = true;
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        return new BadRequestObjectResult(actionContext
            .ModelState
            .Where(modelError => modelError.Value.Errors.Count > 0)
            .Select(modelError => new ResultData(false, modelError.Value.Errors.FirstOrDefault().ErrorMessage))
            .FirstOrDefault());
    };
});

// Version

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
    config.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});

// Swagger

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        // Version

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();

// Exception

app.UseMiddleware<ExceptionMiddleware>();

// JWT

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
