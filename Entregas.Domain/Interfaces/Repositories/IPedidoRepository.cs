using Entregas.Domain.Entities;
using Entregas.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    : IBaseRepository<Pedido, string>
    {
        Task<List<Pedido>> ConsultarStatusAsync(StatusPedido status);
    }
}
