using Entregas.Application.Commands;
using Entregas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task AddAsync(PedidoCreateCommand command);
        Task<List<Pedido>> ConsultarStatusAsync(string status);
    }
}
