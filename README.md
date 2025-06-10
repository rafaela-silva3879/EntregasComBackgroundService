# API em .NET 7 com Entity Framework Code First, Mensageria com RabbitMQ, DDD, TDD e Arquitetura de Eventos

## Descrição

Esta API foi desenvolvida em .NET 7 e utiliza o padrão de arquitetura DDD (Domain-Driven Design), TDD (Test-Driven Development) e a arquitetura de eventos para garantir um código limpo, organizado e escalável.

A API também utiliza o Entity Framework Code First para o gerenciamento de banco de dados e o RabbitMQ como serviço de mensageria.

## Estrutura do Projeto
Este repositório contém dois projetos que se complementam:

## Entregas (API Principal)

Projeto ASP.NET Core RESTful responsável por expor os endpoints da aplicação.

Possui as regras de negócio, controladores, serviços, repositórios e camada de domínio.

Usa Entity Framework Core (Code First) para persistência no banco de dados.

Envia as entregas para uma fila no RabbitMQ para processamento assíncrono.

TDD

## Entregas_WorkerService (Serviço em segundo plano)
Projeto Worker (BackgroundService) que consome a fila do RabbitMQ.

Responsável por processar as entregas de forma assíncrona.

Atualiza o status da entrega com base em regras definidas (simulação com Random).

## Requisitos

- .NET SDK 7.0 ou superior
- RabbitMQ instalado e configurado
- SQL Server instalado e configurado
- Visual Studio 2022

## Como executar

1. Clone o repositório
2. Abra a solução `ApiPedidos.sln` no Visual Studio
3. No arquivo `appsettings.json`, configure a conexão com o banco de dados e o servidor do RabbitMQ
4. Abra o Package Manager Console escolhendo omo default project o projeto Entregas.Infra.Data, execute o comando `Add-Migration nome-migration`, e depois, execute o comando `Update-Database` para criar as tabelas no banco de dados
5. Execute a aplicação



Requisitos:
# 📦 Desafio Técnico – (Logística e Entregas)

## 📝 Descrição

Este projeto tem como objetivo avaliar habilidades de um desenvolvedor C# sênior na construção de uma aplicação RESTful robusta, com foco em **processamento assíncrono, filas, integração com banco de dados** e boas práticas de arquitetura.

Você deve implementar um sistema que simula o fluxo de **entregas de pedidos** no contexto de uma empresa de logística.

---

## 🎯 Objetivos

Desenvolver uma **REST API** em ASP.NET Core que permita:

- Cadastrar uma nova entrega
- Salvar os dados em um banco de dados
- Colocar a entrega em uma fila para processamento assíncrono
- Processar a entrega via um serviço em segundo plano
- Atualizar o status da entrega com base em regras de negócio
- Consultar o status atual de uma entrega

---

## 📦 Requisitos funcionais

### 📍 Cadastro de Entrega
- Endpoint: `POST /entregas`
- Payload:
```json
{
  "pedidoId": "ABC123",
  "destinatario": {
    "nome": "João da Silva",
    "endereco": "Rua das Flores, 1000",
    "cep": "01010-000"
  },
  "itens": [
    { "descricao": "Geladeira", "quantidade": 1 },
    { "descricao": "Fogão", "quantidade": 1 }
  ]
}
```

A entrega é salva no banco de dados com status inicial Pendente.

## 🛠️ Processamento em Fila
A entrega é enfileirada para ser processada por um serviço em segundo plano.

## O serviço:

Atualiza o status para Saiu para entrega

Aguarda 3 segundos (simulação)

Atualiza o status para:

Entregue com sucesso ou

Falha na entrega (regra aleatória usando Random)

## 🔍 Consulta de Entrega

Endpoint: GET /entregas/{id}

Deve retornar os dados e o status atual da entrega

## 🧠 Critérios de Avaliação

Organização do código / Camadas	25%

Clareza e lógica de processamento	20%

Modelagem de dados	15%

Uso de filas e serviços	15%

Boas práticas (REST, SOLID)	15%

Testes ou simulações adicionais	10%



## Swagger para documentação da API

Injeção de dependência e separação por camadas (Controller, Service, Repository, Domain, etc.)

## 📬 Entrega

Envie o link do repositório público (GitHub/GitLab)

O código deve conter instruções claras de execução

Adicione comentários explicando suas decisões de arquitetura, se necessário

