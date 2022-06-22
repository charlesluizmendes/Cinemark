using Cinemark.Application.AutoMapper;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.Context.Option;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Cinemark.Infrastructure.Identity.Services.Option;
using Cinemark.Infrastructure.IoC;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

// MediatR

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// IoC

InjectorDependency.Register(builder.Services);

// AutoMapper

builder.Services.AddAutoMapper(typeof(DtoToEntityMappingProfile), typeof(EntityToDtoMappingProfile));

// JWT

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));

var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = "Token";
})
.AddJwtBearer("Token", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Jwt:Issuer"],
        ValidAudience = jwt["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true
    };
});

// Fluent Validation

builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
