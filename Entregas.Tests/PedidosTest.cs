using Entregas.Application.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Net.Http;
using Entregas.Tests; // importante se estiver em outro namespace

namespace Entregas.Tests
{
    public class PedidosTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PedidosTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private static StringContent CriarContent(PedidoCreateCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static PedidoCreateCommand CriarPedidoValido()
        {
            return new PedidoCreateCommand
            {
                PedidoId = Guid.NewGuid().ToString(),
                Destinatario = new DestinatarioCreateCommand
                {
                    Nome = "João da Silva",
                    Endereco = "Rua das Flores, 1000",
                    CEP = "01010-000"
                },
                Itens = new List<ItemCreateCommand>
                {
                    new ItemCreateCommand { Descricao = "Geladeira", Quantidade = 1 },
                    new ItemCreateCommand { Descricao = "Fogão", Quantidade = 1 }
                }
            };
        }

        [Fact]
        public async Task Post_DeveRetornarCreated_QuandoPedidoValido()
        {
            var pedido = CriarPedidoValido();

            var response = await _client.PostAsync("/api/pedidos", CriarContent(pedido));

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("Pedido adicionado com sucesso");
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoPedidoIdForNulo()
        {
            var pedido = CriarPedidoValido();
            pedido.PedidoId = null;

            var response = await _client.PostAsync("/api/pedidos", CriarContent(pedido));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("O PedidoId deve estar preenchido");
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoDestinatarioForNulo()
        {
            var pedido = CriarPedidoValido();
            pedido.Destinatario = null;

            var response = await _client.PostAsync("/api/pedidos", CriarContent(pedido));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("O destinatário deve estar preenchido");
        }

        [Fact]
        public async Task Post_DeveRetornarBadRequest_QuandoItensEstiverVazio()
        {
            var pedido = CriarPedidoValido();
            pedido.Itens = new List<ItemCreateCommand>(); // vazio

            var response = await _client.PostAsync("/api/pedidos", CriarContent(pedido));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("A lista de itens não pode estar vazia");
        }

        [Fact]
        public async Task Get_DeveRetornarOk_QuandoStatusValido()
        {
            var status = "Pendente";

            var response = await _client.GetAsync($"/api/pedidos/status/{status}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("success");
            body.Should().Contain("pedidos");
        }

        [Fact]
        public async Task Get_DeveRetornarBadRequest_QuandoStatusInvalido()
        {
            var status = "StatusInexistente";

            var response = await _client.GetAsync($"/api/pedidos/status/{status}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("Status inválido");
            body.Should().Contain("error");
        }
    }
}
