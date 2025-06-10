using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Entregas.Infra.Data;
using Entregas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Entregas.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o DbContext real
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Adiciona o DbContext InMemory
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("BancoDeTeste")
          .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });

                // (Opcional) Inicializa dados mock
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
