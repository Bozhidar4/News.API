using AutoMapper;
using Microsoft.EntityFrameworkCore;
using News.API.Mapping;
using News.API.Services;
using News.API.Services.Interfaces;
using News.Domain.Countries;
using News.Domain.Sources;
using News.Domain.TopHeadlines;
using News.Persistence;
using News.Persistence.Repositories;
using News.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<NewsBackgroundService>();

builder.Services.AddSingleton<ICountryService, CountryService>();
builder.Services.AddScoped<ITopHeadlineService, TopHeadlineService>();
builder.Services.AddScoped<ISourceService, SourceService>();

ConfigureRepositories(builder.Services);
ConfigurePersistance(builder);
ConfigureAutoMapper(builder.Services);

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

void ConfigurePersistance(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<NewsDBContext>(option =>
    {
        option = new DbContextOptionsBuilder<NewsDBContext>()
            .UseSqlServer(builder.Configuration.GetConnectionString("NewsConnection"), 
            builder => builder.MigrationsAssembly("News.API"));
    }, ServiceLifetime.Singleton);

    builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
}

void ConfigureRepositories(IServiceCollection services)
{
    services.AddSingleton<ITopHeadlineRepository, TopHeadlineRepository>();
    services.AddSingleton<ICountryRepository, CountryRepository>();
    services.AddSingleton<ISourceRepository, SourceRepository>();
}

void ConfigureAutoMapper(IServiceCollection services)
{
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new TopHeadlineMapping());
        mc.AddProfile(new SourceMapping());
        mc.AddProfile(new CountryMapping());
    });

    IMapper mapper = mappingConfig.CreateMapper();
    services.AddSingleton(mapper);
}
