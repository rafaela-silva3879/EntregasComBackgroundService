using Entregas.Domain.Entities;
using Entregas.Domain.Entities.Enums;
using Entregas.Domain.Interfaces.Repositories;
using Entregas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Infra.Data.Repositories
{
    public class PedidoRepository
    : BaseRepository<Pedido, string>, IPedidoRepository //PedidoRepository does not implement interface member IBaseRepository<Pedido, string>.GetByIdAsync(string)
    {
        private readonly DataContext? _dataContext;
        public PedidoRepository(DataContext? dataContext)
        : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Pedido>> ConsultarStatusAsync(StatusPedido status)
        {
            try
            {
                var lista = new List<Pedido>();

                lista =await _dataContext.Pedidos.Where(p=>p.Status== status).ToListAsync();

                return lista;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
