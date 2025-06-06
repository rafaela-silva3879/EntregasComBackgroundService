using Entregas.Domain.Entities;
using Entregas.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        // Construtor para injeção de dependência
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DestinatarioConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        }

        // DbSets para representar as tabelas no banco de dados
        public DbSet<Destinatario>? Destinatarios { get; set; }
        public DbSet<Item>? Itens { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }
    }
}
