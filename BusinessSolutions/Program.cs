using BusinessSolutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BusinessSolutions.Data;
using BusinessSolutions.Models;
using BusinessSolutions.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BusinessSolutionsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BusinessSolutionsContext") ?? throw new InvalidOperationException("Connection string 'BusinessSolutionsContext' not found.")));
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedDataProvider.Initialize(services);
    SeedDataOrder.Initialize(services);
    SeedDataOrderItem.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=Index}/{id?}");



app.Run();
