using Entregas.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Entities
{
    public class Item : IExcluivel
    {
        public Guid ItemId { get; set; } 

        public string PedidoId { get; set; }
        public Pedido Pedido { get; set; } = new();

        public string Descricao { get; set; }
        public int Quantidade { get; set; }

        public bool Excluido { get; set; } = false;
    }
}
