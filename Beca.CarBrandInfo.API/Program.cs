using Beca.CarBrandInfo.API.DbContexts;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
 */



// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddDbContext<BrandInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:BrandInfoDBConnectionString"]));

builder.Services.AddScoped<IBrandInfoRepository, BrandInfoRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

app.UseAuthorization();

app.MapControllers();

app.Run();
