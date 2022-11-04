using Beca.CarBrandInfo.API;
using Beca.CarBrandInfo.API.DbContexts;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

/*
 * --- NuGet To install ---
 * JsonPatch        -> Para hacer el HttpPatch (Modificar solo una variable de entre varias en una petición).
 * NewtonsoftJson   -> lo mismo
 * the Microsoft.AspNetCore.Mvc.NewtonsoftJson
 * 
 * serilog.AspNetCore
 * install-package serilog.sink.file
 * install-package serilog.sink.console
 * 
 * Automapper.Extensions.Microsoft.DependencyInjection 
 * microsoft.entityframeworkcore 
 * Microsoft.EntityFrameworkCore.Sqlite v6.0.0  -> para conectar con la Db.
 * Microsoft.EntityFrameworkCore.Tools          -> para comandos como add-migration.
 * 
 * AutoMapper.Extensions.Microsoft.DependencyInjection  -> mapper
 */


builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CityInfoApiBearerAuth" }
            }, new List<string>() }
    });
});

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddDbContext<BrandInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:BrandInfoDBConnectionString"]));

builder.Services.AddScoped<IBrandInfoRepository, BrandInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<BrandDataStore>();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/brandinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
