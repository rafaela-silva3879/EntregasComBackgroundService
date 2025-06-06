using Entregas.Application.Commands;
using Entregas.Application.Interfaces;
using Entregas.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Entregas.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoAppService? _pedidoAppService;
        public PedidosController(IPedidoAppService? pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        /// <summary>
        /// Serviço para criar um pedido e adicioná-lo na fila do RabbitMQ
        /// </summary> 
        [HttpPost]
        public async Task<IActionResult> Post(PedidoCreateCommand command)
        {
            try
            {
                await _pedidoAppService.AddAsync(command);
                return Created("api/pedidos", new { message = "Pedido adicionado com sucesso" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Erro inesperado ao criar o pedido. Tente novamente mais tarde."
                });
            }
        }

        /// <summary>
        /// Serviço para consultar pedidos por status
        /// </summary> 
        [HttpGet("status/{status}")]
        public async Task<IActionResult> ConsultarStatusAsync(string status)
        {
            try
            {
                var lista = await _pedidoAppService.ConsultarStatusAsync(status);

                return Ok(new
                {
                    status = "success",
                    pedidos = lista
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Erro inesperado ao consultar pedidos. Tente novamente mais tarde."
                });
            }
        }

    }
}
