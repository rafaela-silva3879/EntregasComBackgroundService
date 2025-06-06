using Entregas.Application.Interfaces;
using Entregas.Application.Services;
using Entregas.Domain.Interfaces.Repositories;
using Entregas.Domain.Interfaces.Services;
using Entregas.Domain.Services;
using Entregas.Infra.Data.Contexts;
using Entregas.Infra.Data.Repositories;
using Entregas.Infra.EventBus.Produccers;
using Entregas.Infra.EventBus.Settings;
using Microsoft.EntityFrameworkCore;

namespace Entregas.Service.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection
        (WebApplicationBuilder builder)
        {
            builder.Services.Configure<MessageBrokerSettings>
            (builder.Configuration.GetSection("MessageBrokerSettings"));

            builder.Services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("Conexao")));

            builder.Services.AddTransient
            <IPedidoAppService, PedidoAppService>();
            builder.Services.AddTransient
            <IPedidoDomainService, PedidoDomainService>();
            builder.Services.AddTransient
            <IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient
            <IPedidoProducer, PedidoProducer>();
           }
    }
}