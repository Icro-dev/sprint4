using System.Diagnostics;
using cinema.Data;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IPriceCalculatingService, PriceCalculatingService>();
builder.Services.AddScoped<IShowService, ShowService>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IPaymentAdapter, PaymentAdapter>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTemplatesRepository, RoomTemplatesRepository>();
builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();
builder.Services.AddScoped<IMovieReviewRepository, MovieReviewRepository>();
builder.Services.AddScoped<IAbonnementRepository, AbonnementRepository>();


// load .env file
var root = Directory.GetCurrentDirectory();
Debug.Print(root);
var dotenv = Path.Combine(root, ".env");
Debug.Print(dotenv);
DotEnv.Load(dotenv);

// Add DbContext

var env = Environment.GetEnvironmentVariables();

/*var connectionString = "Data Source="+env["hostname"]+";" +
                       "User ID="+env["username"]+";" +
                       "Password="+env["password"]+";" +
                       "Database="+env["database"]+"";*/
var connectionString = "Data Source=localhost;User ID=sa;Password=Wachtwoord1!;Database=patee";

builder.Services
    .AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CinemaContext>();

builder.Services.ConfigureApplicationCookie(
    options =>
    {
        options.LoginPath = "/Users/Login";
        
        options.AccessDeniedPath = "/Users/AccessDenied";
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //running migrations at startup
    var db = scope.ServiceProvider.GetRequiredService<CinemaContext>();
    db.Database.Migrate();
    //adding seeddata
   
        var services = scope.ServiceProvider;
        SeedData.Initialize(services);
    
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.MapControllerRoute(
    name: "Movies",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "RoomTemplates",
    pattern: "{controller=RoomTemplates}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Rooms",
    pattern: "{controller=Rooms}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Shows",
    pattern: "{controller=Shows}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Users",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "MovieReviews",
    pattern: "{controller=MovieReviews}/{action=Index}/{id?}");

app.Run();
