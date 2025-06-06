using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        IDestinatarioRepository DestinatarioRepository { get; }
        IItemRepository ItemRepository { get; }
        IPedidoRepository PedidoRepository { get; }
    }
}
