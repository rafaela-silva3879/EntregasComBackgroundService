using Entregas.Application.Commands;
using Entregas.Application.Events;
using Entregas.Application.Interfaces;
using Entregas.Domain.Entities;
using Entregas.Domain.Entities.Enums;
using Entregas.Domain.Interfaces.Services;
using Entregas.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoProducer? _pedidoProducer;
        private readonly IPedidoDomainService? _pedidoDomainService;
        public PedidoAppService(IPedidoProducer? pedidoProducer,
                                IPedidoDomainService pedidoDomainService)
        {
            _pedidoProducer = pedidoProducer;
            _pedidoDomainService = pedidoDomainService;
        }
        public async Task AddAsync(PedidoCreateCommand command)
        {
            if (String.IsNullOrEmpty(command.PedidoId))
                throw new ArgumentException("O PedidoId deve estar preenchido.");

            if(command.Destinatario==null)
                throw new ArgumentException("O destinatário deve estar preenchido.");

            if (command.Itens.Count == 0)
                throw new ArgumentException("A lista de itens não pode estar vazia.");

            #region Realizar o cadastro do pedido
            var p = new Pedido();

            p.PedidoId = command.PedidoId;

            p.Destinatario = new Destinatario();
            p.DestinatarioId = Guid.NewGuid();

            p.Destinatario.DestinatarioId = p.DestinatarioId;
            p.Destinatario.CEP = command.Destinatario.CEP;
            p.Destinatario.Nome = command.Destinatario.Nome;
            p.Destinatario.Endereco = command.Destinatario.Endereco;
            
            p.Itens = new List<Item>();

            foreach (var item in command.Itens)
            {
                var i = new Item();

                i.ItemId = Guid.NewGuid();
                i.PedidoId = p.PedidoId;

                i.Quantidade = item.Quantidade.Value;
                i.Descricao = item.Descricao;

                p.Itens.Add(i);
            }
           
            await _pedidoDomainService.RealizarPedido(p);

            #endregion

            #region Gerando um evento: Pedido realizado
            var @event = new PedidoPendenteEvent
            {
                EventId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                DetalhesPedido = JsonConvert.SerializeObject(command)
            };
            await _pedidoProducer.AddAsync(@event);
            #endregion
        }

        public async Task<List<Pedido>> ConsultarStatusAsync(string status)
        {
            try
            {
                var lista = new List<Pedido>();
                lista = await _pedidoDomainService.ConsultarStatusAsync(status);

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}





