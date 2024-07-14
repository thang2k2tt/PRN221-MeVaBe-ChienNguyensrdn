using PRN221_MeVaBe_Repo.Interfaces;
using PRN221_MeVaBe_Repo.Repositories;
using Services.Interface;
using Services;
using Microsoft.EntityFrameworkCore;
using PRN221_MeVaBe_Repo.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSession();
builder.Services.AddDbContext<DBContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("appsettings.json")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddRazorPages().AddMvcOptions(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    // Add a default route to the HomePage
    endpoints.MapFallbackToPage("/HomePage");
});

app.Run();
