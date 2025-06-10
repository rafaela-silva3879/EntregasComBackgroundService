using Entregas.Domain.Entities;
using Entregas.Domain.Entities.Enums;
using Entregas.Domain.Interfaces.Repositories;
using Entregas.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entregas.Domain.Services
{
    public class PedidoDomainService : IPedidoDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;
        public PedidoDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task RealizarPedido(Pedido pedido)
        {
            await _unitOfWork?.BeginTransactionAsync();
            try
            {
                #region Cadastro

                await _unitOfWork?.DestinatarioRepository.AddAsync(pedido.Destinatario);

                // Ao cadastrar, salva como pendente
                pedido.Status = StatusPedido.Pendente;
                await _unitOfWork?.PedidoRepository.AddAsync(pedido);


                foreach (var item in pedido.Itens)
                {
                    await _unitOfWork?.ItemRepository.AddAsync(item);
                }

                #endregion


                await _unitOfWork?.CommitAsync();
            }
            catch (Exception)
            {
                if (_unitOfWork != null)
                    await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<List<Pedido>> ConsultarStatusAsync(string status)
        {
            try
            {
                StatusPedido statusConvertido;

                bool converteu = Enum.TryParse(status, true, out statusConvertido);

                //se não converteu ou se não está definido no enum
                if (!converteu || !Enum.IsDefined(typeof(StatusPedido), statusConvertido))
                    throw new ArgumentException("Status inválido.");

                var lista = new List<Pedido>();
                lista = await _unitOfWork.PedidoRepository.ConsultarStatusAsync(statusConvertido);

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async ValueTask DisposeAsync()
        {
            if (_unitOfWork != null)
                await _unitOfWork.DisposeAsync();
        }
    }
}
