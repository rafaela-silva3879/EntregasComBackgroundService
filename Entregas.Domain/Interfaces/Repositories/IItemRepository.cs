using Entregas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Interfaces.Repositories
{
    public interface IItemRepository
    : IBaseRepository<Item, Guid>
    {
    }
}
