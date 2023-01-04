using Microsoft.EntityFrameworkCore;
using News.API.Services;
using News.Persistence;
using News.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<TopHeadlineService>();

ConfigurePersistance(builder);

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
    });

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}
