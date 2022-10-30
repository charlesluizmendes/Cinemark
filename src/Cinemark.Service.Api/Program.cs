using Cinemark.Application.AutoMapper;
using Cinemark.Application.Middleware;
using Cinemark.Domain.Models.Commom;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Cinemark.Infrastructure.Data.Services.Option;
using Cinemark.Infrastructure.HostedServices.Consumer;
using Cinemark.Infrastructure.IoC;
using Cinemark.Infrastructure.Services.Consumer;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Option

builder.Services.AddOptions();

// Context

builder.Services.AddDbContext<SqlServerContext>(option =>
     option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
);

builder.Services.Configure<MongoConfiguration>(builder.Configuration.GetSection("MongoDbConnection"));
MongoContext.OnModelCreating();

// RabbitMQ

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<FilmeConsumer>();
builder.Services.AddHostedService<UsuarioConsumer>();

// MediatR

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

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
        return new BadRequestObjectResult(actionContext.ModelState
        .Where(modelError => modelError.Value.Errors.Count > 0)
        .Select(modelError => new HttpResult()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Message = modelError.Value.Errors.FirstOrDefault().ErrorMessage
        })
        .FirstOrDefault());
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cinemark API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "Token Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
            },
            new[] { "readAccess", "writeAccess" }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.DefaultModelsExpandDepth(-1);
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
