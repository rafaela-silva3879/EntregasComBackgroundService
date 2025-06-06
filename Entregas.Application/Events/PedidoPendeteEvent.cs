using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Events
{
    public class PedidoPendenteEvent : BaseEvent
    {
        public string? DetalhesPedido { get; set; }
    }
}
