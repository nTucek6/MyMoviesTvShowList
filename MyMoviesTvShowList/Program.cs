using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using MyMoviesTvShowList.Configuration;
using MyMoviesTvShowList.Extensions;
using MyMoviesTvShowList.Services;
using Services.Authentication;
using Services.Frontpage;
using Services.MoviesAdmin;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection to database -------------------------------------------------------------------------
builder.Services.AddDbContext<MyMoviesTvShowListContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

//Configuration -------------------------------------------------------------------------
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));

builder.Services.AddLogging();
builder.Services.AddTransient<Middleware>();


//Services -------------------------------------------------------------------------
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IFrontpageService, FrontpageService>();
builder.Services.AddTransient<IMoviesAdminService, MoviesAdminService>();





builder.Services.AddHostedService<CalculateScoreTimer>();

// ---------------------------------------------------------------------------------





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.UseMiddleware<Middleware>();

app.MapControllers();

app.Run();
