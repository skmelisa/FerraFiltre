using ferraFiltre.Services;
using ferraFiltre.IServices;
using ferraFiltre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ferraFiltre.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DB connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the services
builder.Services.AddScoped<IProductService, ProductService>();

// Manually add lists for DI
builder.Services.AddScoped<List<FerraOrjinalMuadil>>(provider =>
    new List<FerraOrjinalMuadil>());  // This will create an empty list of FerraOrjinalMuadil

builder.Services.AddScoped<List<Filtreler>>(provider =>
    new List<Filtreler>());  // Similarly for Filtreler
builder.Services.AddSingleton<List<FerraOrjinalMuadil>>(/* veri kaynaðýnýz */);
builder.Services.AddScoped<CrossReferenceService>();

// Add FilterSearchService to DI container
builder.Services.AddScoped<FilterSearchService>();

var app = builder.Build();

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
