# Task Manager API

API REST para gerenciamento de tarefas, desenvolvida em .NET 8.

O projeto foi desenvolvido como parte de um desafio técnico, utilizando uma arquitetura com separação de responsabilidades entre Controller, Service e Repository, além de testes automatizados para as principais regras de negócio.

## Tecnologias

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* Entity Framework Core InMemory
* Swagger
* xUnit

## Funcionalidades

* Criação de tarefas
* Listagem de tarefas
* Busca de tarefa por ID
* Filtro por status
* Filtro por data de vencimento
* Atualização de tarefas
* Exclusão de tarefas
* Validação dos dados de entrada
* Tratamento de exceções
* Logging
* Testes automatizados

## Status das tarefas

| Status     | Valor |
| ---------- | ----: |
| Pending    |     1 |
| InProgress |     2 |
| Completed  |     3 |

## Estrutura

```text
TaskManager
├── TaskManager.API
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── TaskManager.Core
│   ├── Data
│   ├── DTOs
│   ├── Entities
│   ├── Enums
│   ├── Interfaces
│   ├── Repositories
│   └── Services
│
└── TaskManager.Tests
```

O fluxo principal da aplicação segue a seguinte estrutura:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
Entity Framework Core
    ↓
InMemory Database
```

## Execução

Para restaurar as dependências:

```bash
dotnet restore
```

Para executar a API:

```bash
dotnet run --project TaskManager.API
```

A aplicação será iniciada no endereço informado pelo terminal.

## Swagger

Após iniciar a aplicação, o Swagger pode ser acessado em:

```text
http://localhost:5100/swagger
```

Através dele é possível visualizar e testar os endpoints disponíveis.

## Endpoints

### Criar tarefa

```http
POST /api/tasks
```

Exemplo de requisição:

```json
{
  "title": "Estudar C#",
  "description": "Estudar testes automatizados",
  "dueDate": "2026-10-05T18:00:00",
  "status": 1
}
```

### Listar tarefas

```http
GET /api/tasks
```

### Filtrar tarefas por status

```http
GET /api/tasks?status=1
```

### Filtrar tarefas por data

```http
GET /api/tasks?dueDate=2026-10-05
```

### Buscar tarefa por ID

```http
GET /api/tasks/{id}
```

### Atualizar tarefa

```http
PUT /api/tasks/{id}
```

### Excluir tarefa

```http
DELETE /api/tasks/{id}
```

## Testes

Os testes foram implementados utilizando xUnit e têm como objetivo validar as principais regras de negócio da aplicação.

Para executar os testes:

```bash
dotnet test
```

Entre os cenários testados estão criação, consulta, filtros, atualização, exclusão e validação de status.

## Banco de dados

O projeto utiliza o Entity Framework Core com o provider InMemory.

Os dados são mantidos somente durante a execução da aplicação e não possuem persistência após o encerramento.

## Arquitetura

A aplicação utiliza injeção de dependência e separação de responsabilidades.

O Controller é responsável pelo recebimento das requisições HTTP e retorno das respostas.

O Service concentra as regras de negócio.

O Repository é responsável pelo acesso aos dados através do Entity Framework Core.

Os DTOs são utilizados para definir os dados de entrada e saída da API, evitando a exposição direta das entidades.
