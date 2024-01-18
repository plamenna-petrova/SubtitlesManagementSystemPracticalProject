using Data.DataAccess;
using Data.DataModels.Entities.Identity;
using SubtitlesManagementSystem.Infrastructure.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();
webApplicationBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();

webApplicationBuilder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

webApplicationBuilder.Services.AddControllersWithViews();

webApplicationBuilder.Services.AddRazorPages();

webApplicationBuilder.Services.RegisterRepositories();

webApplicationBuilder.Services.RegisterServiceLayer();

var webApplication = webApplicationBuilder.Build();

var logger = webApplication.Services.GetService<ILogger<Program>>()!;

// Configure the HTTP request pipeline.
if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseDeveloperExceptionPage();

    webApplication.UseMigrationsEndPoint();

    webApplication.MigrateDatabase(logger);

    webApplication.ApplyDatabaseSeeding(logger);
}
else
{
    webApplication.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    webApplication.UseHsts();
}

webApplication.UseHttpsRedirection();

var supportedCultures = new[]
{
   new CultureInfo("en-US")
};

webApplication.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

webApplication.UseStaticFiles();

webApplication.UseRouting();

webApplication.UseAuthentication();
webApplication.UseAuthorization();

webApplication.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
webApplication.MapRazorPages();

webApplication.Run();

public partial class Program 
{
    
}
