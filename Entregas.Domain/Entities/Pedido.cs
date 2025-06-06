using Entregas.Domain.Entities.Enums;
using Entregas.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Entities
{
    public class Pedido : IExcluivel
    {
        public string PedidoId { get; set; }

        public Guid DestinatarioId { get; set; }
        public Destinatario Destinatario { get; set; }=new();

        public List<Item> Itens { get; set; } = new();

        public StatusPedido Status { get; set; }

        public bool Excluido { get; set; } = false;
    }

}
