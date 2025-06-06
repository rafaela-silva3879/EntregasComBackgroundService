using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entregas.Domain.Entities;

namespace Entregas.Infra.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.PedidoId);

            builder.Property(p => p.Status).IsRequired();

            // 1:N Pedido -> Itens - Não se pode excluir itens associados a pedidos
            builder.HasMany(p => p.Itens)
                   .WithOne(i => i.Pedido)
                   .HasForeignKey(i => i.PedidoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
