using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Commands
{
    public class ItemCreateCommand
    {
        public string? Descricao { get; set; }
        public int? Quantidade { get; set; }
    }
}
