using Data.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesApp.IntegrationTests
{
    public class TestingWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
            {
                var applicationDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (applicationDbContext != null)
                {
                    services.Remove(applicationDbContext);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemorySubtitlesDatabase");
                });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                using var applicationDbContextInServicesScope = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    applicationDbContextInServicesScope.Database.EnsureCreated();
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}