using Entregas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entregas.Infra.Data.Configurations
{
    public class DestinatarioConfiguration : IEntityTypeConfiguration<Destinatario>
    {
        public void Configure(EntityTypeBuilder<Destinatario> builder)
        {
            builder.HasKey(d => d.DestinatarioId);

            builder.Property(d => d.Nome).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Endereco).IsRequired().HasMaxLength(200);
            builder.Property(d => d.CEP).IsRequired().HasMaxLength(10);

            // Relacionamento 1:N Destinatario -> Pedidos
            builder.HasMany(d => d.Pedidos)
                   .WithOne(p => p.Destinatario)
                   .HasForeignKey(p => p.DestinatarioId)
                   .OnDelete(DeleteBehavior.Restrict); // Não permite excluir destinatário se existirem pedidos
        }
    }
}
