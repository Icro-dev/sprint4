using cinema.Data;
using cinema.Identity;
using cinema.Models;
using cinema.Services;
using Microsoft.EntityFrameworkCore;
using cinema.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<CinemaIdentityUser, CinemaIdentityRole>(
        options => {
            options.SignIn.RequireConfirmedAccount = false;
 
            //Other options go here
        }
    )
    .AddEntityFrameworkStores<CinemaContext>();

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

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    //options.Cookie.Expiration 
 
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = "/";
});


// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("CinemaDbContext");
builder.Services
    .AddDbContext<CinemaContext>(options => options.UseSqlServer(connectionString));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
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

app.UseHttpsRedirection();
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
    name: "Room",
    pattern: "{controller=Rooms}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Shows",
    pattern: "{controller=Shows}/{action=Index}/{id?}");

app.Run();
