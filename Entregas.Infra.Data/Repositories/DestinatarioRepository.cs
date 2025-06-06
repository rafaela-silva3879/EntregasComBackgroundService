using Entregas.Domain.Entities;
using Entregas.Domain.Interfaces.Repositories;
using Entregas.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Infra.Data.Repositories
{
    public class DestinatarioRepository
    : BaseRepository<Destinatario, Guid>, IDestinatarioRepository
    {
        private readonly DataContext? _dataContext;
        public DestinatarioRepository(DataContext? dataContext)
        : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
