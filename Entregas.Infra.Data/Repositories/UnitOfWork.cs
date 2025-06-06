using Entregas.Domain.Interfaces.Repositories;
using Entregas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Entregas.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly DataContext _dataContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dataContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public IDestinatarioRepository DestinatarioRepository => new DestinatarioRepository(_dataContext);
        public IItemRepository ItemRepository => new ItemRepository(_dataContext);
        public IPedidoRepository PedidoRepository => new PedidoRepository(_dataContext);

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
                await _transaction.DisposeAsync();
            await _dataContext.DisposeAsync();
        }
    }
}
