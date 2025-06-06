using Entregas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Interfaces.Services
{
    public interface IPedidoDomainService : IAsyncDisposable
    {
        Task RealizarPedido(Pedido pedido);

        Task<List<Pedido>> ConsultarStatusAsync(string status);
    }
}
