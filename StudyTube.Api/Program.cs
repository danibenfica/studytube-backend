using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StudyTube.Data;
using StudyTube.Data.ExternalServices.YouTube;
using StudyTube.Data.Repositories;
using StudyTube.Services.Interfaces;
using StudyTube.Services.Videos;

var builder = WebApplication.CreateBuilder(args);

// Hospedagem: plataformas como o Render informam a porta pela variável PORT.
// Localmente ela não existe, então continua valendo o launchSettings.json.
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port))
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

// Hospedagem: o Render recebe o acesso em https e repassa para a API em http,
// avisando o endereço original nos cabeçalhos X-Forwarded-*. Isso faz a API
// (e o Swagger) enxergar o endereço certo, com https.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

// O caminho do banco vem de ConnectionStrings:Default
// (na hospedagem: variável de ambiente ConnectionStrings__Default).
var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Data Source=studytube.db";
var dataSource = new SqliteConnectionStringBuilder(connectionString).DataSource;
var dataDirectory = Path.GetDirectoryName(Path.GetFullPath(dataSource));
if (!string.IsNullOrEmpty(dataDirectory))
    Directory.CreateDirectory(dataDirectory);

builder.Services.AddDbContext<StudyTubeDbContext>(options =>
    options.UseSqlite(connectionString));

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

app.UseForwardedHeaders();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StudyTubeDbContext>();
    db.Database.Migrate();
}

// Swagger: sempre em Development; na hospedagem só se Swagger:Enabled = true
// (variável de ambiente Swagger__Enabled=true).
if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("Swagger:Enabled"))
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

app.MapGet("/", () => Results.Ok(new { name = "StudyTube API", status = "online" }));
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
