using Microsoft.EntityFrameworkCore;
using StudyTube.Data;
using StudyTube.Data.ExternalServices.YouTube;
using StudyTube.Data.Repositories;
using StudyTube.Services.Interfaces;
using StudyTube.Services.Videos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<StudyTubeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton(new YouTubeOptions
{
    ApiKey = builder.Configuration["YouTube:ApiKey"] ?? string.Empty
});
builder.Services.AddHttpClient<IYouTubeApiClient, YouTubeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://www.googleapis.com/youtube/v3/");
});

builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontends", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StudyTubeDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseStaticFiles();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/openapi/v1.json", "StudyTube API");
        o.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseCors("Frontends");
app.UseAuthorization();
app.MapControllers();

app.Run();