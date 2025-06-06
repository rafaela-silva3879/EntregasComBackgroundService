using Entregas.Application.Commands;

namespace Entregas.Tests
{
    internal class DestinatarioCommand : DestinatarioCreateCommand
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
    }
}