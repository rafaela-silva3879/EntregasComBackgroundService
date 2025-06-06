using Entregas.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Interfaces
{
    public interface IPedidoProducer
    {
        Task AddAsync(PedidoPendenteEvent @event);
    }
}
