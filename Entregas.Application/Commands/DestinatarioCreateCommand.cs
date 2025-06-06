using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Commands
{
    public class DestinatarioCreateCommand
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
    }
}
