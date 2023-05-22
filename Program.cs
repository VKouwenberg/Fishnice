using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fishnice.Data;
using Fishnice.Models;
using MvcMovie.Models;
using System.Configuration;
using MySql.Data.EntityFrameworkCore.Extensions;

/*var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FishniceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FishniceContext") ?? throw new InvalidOperationException("Connection string 'FishniceContext' not found.")));
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FishniceContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'FishniceContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

/*public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<YourDbContext>(options =>
        options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

    // Add other services and dependencies here
}*/

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
