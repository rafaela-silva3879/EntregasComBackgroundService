using Entregas.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Entities
{
    public class Destinatario : IExcluivel
    {
        public Guid DestinatarioId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }

        // Para o caso de o usuário ter mais de um pedido
        public List<Pedido> Pedidos { get; set; } = new();
        public bool Excluido { get; set; } = false;
    }
}
