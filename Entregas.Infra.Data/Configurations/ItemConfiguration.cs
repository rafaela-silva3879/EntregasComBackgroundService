using Entregas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Infra.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.ItemId);

            builder.Property(i => i.Quantidade).IsRequired();
            builder.Property(i => i.Descricao).IsRequired().HasMaxLength(100);

            // Se eu excluir um pedido, os itens somem
            builder.HasOne(i => i.Pedido)
                      .WithMany(p => p.Itens)
                      .HasForeignKey(i => i.PedidoId)
                      .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(i => i.PedidoId);
        }
    }
}
