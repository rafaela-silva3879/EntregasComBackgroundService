using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Commands
{
    public class PedidoCreateCommand
    {
        public string? PedidoId { get; set; }
        public DestinatarioCreateCommand? Destinatario { get; set; }

        public List<ItemCreateCommand>? Itens { get; set; } = new();
    }
}
