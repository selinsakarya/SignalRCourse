using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRExample.Data;
using SignalRExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true)
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<UserHub>("hubs/userCount");
app.MapHub<DeathlyHallowsHub>("hubs/deathlyHallows");
app.MapHub<HarryPotterHouseGroupHub>("hubs/harryPotterHouseGroup");
app.MapHub<NotificationHub>("hubs/notifications");
app.MapHub<ChatHub>("hubs/chat");
app.MapHub<OrderHub>("hubs/orders");

app.Run();