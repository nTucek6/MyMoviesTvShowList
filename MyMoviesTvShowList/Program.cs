using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MyMoviesTvShowList.Configuration;
using MyMoviesTvShowList.Extensions;
using MyMoviesTvShowList.Services;
using Services.Authentication;
using Services.CrewsAdmin;
using Services.Frontpage;
using Services.MoviesAdmin;
using Services.TVShowsAdmin;
using Services.ExternalApiCalls;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.FileProviders;
using Services.MovieTVShowList;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Services.MovieTVShowInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

//builder.WebHost.UseUrls("https://192.168.1.2:7169");

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; //Api request doesn't lowercase object atrribute
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection to database -------------------------------------------------------------------------
builder.Services.AddDbContext<MyMoviesTvShowListContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

//Configuration -------------------------------------------------------------------------

var jwtConfig = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));


if (jwtConfig != null)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});
}


builder.Services.AddAuthorization();


builder.Services.AddLogging();
builder.Services.AddTransient<Middleware>();


//Services -------------------------------------------------------------------------
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IFrontpageService, FrontpageService>();
builder.Services.AddTransient<IMoviesAdminService, MoviesAdminService>();
builder.Services.AddTransient<ICrewsAdminService, CrewsAdminService>();
builder.Services.AddTransient<ITVShowsAdminService, TVShowsAdminService>();
builder.Services.AddTransient<IExternalApiCallsService, ExternalApiCallsService>();
builder.Services.AddTransient<IMovieTVShowInfoService, MovieTVShowInfoService>();
builder.Services.AddTransient<IMovieTVShowListService, MovieTVShowListService>();


builder.Services.AddHostedService<CalculateScoreTimer>();

// ---------------------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<Middleware>();

app.MapControllers();

app.Run();
