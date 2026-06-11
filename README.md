# SmartEstoque API

SmartEstoque é uma API REST para controle de estoque desenvolvida em .NET e SQL Server.

O projeto foi criado com objetivo de estudo, praticando conceitos de arquitetura em camadas, Entity Framework Core, migrations, AutoMapper, DTOs, CRUDs e regras de negócio para movimentação de estoque.

---

# Tecnologias Utilizadas

- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger / OpenAPI

---

# Arquitetura do Projeto

```text
SmartEstoqueAPI
│
├── SmartEstoqueAPI.Api
│
├── SmartEstoqueAPI.Domain
│   ├── Entities
│   └── Enums
│
└── SmartEstoqueAPI.Infrastructure
    ├── Configurations
    ├── Data
    └── Migrations
```

---

# Estrutura da Solução

```text
SmartEstoqueAPI.sln

├── SmartEstoqueAPI.Api
├── SmartEstoqueAPI.Domain
└── SmartEstoqueAPI.Infrastructure
```

---

# Funcionalidades

## Categorias

- Cadastro de categorias
- Alteração de categorias
- Exclusão de categorias
- Consulta de categorias

## Fornecedores

- Cadastro de fornecedores
- Alteração de fornecedores
- Exclusão de fornecedores
- Consulta de fornecedores

## Produtos

- Cadastro de produtos
- Alteração de produtos
- Exclusão de produtos
- Consulta de produtos
- Consulta de saldo de estoque
- Consulta de produtos com estoque baixo

## Movimentações de Estoque

- Entrada de estoque
- Saída de estoque
- Ajuste de estoque
- Histórico de movimentações
- Consulta por período

## Dashboard

- Total de categorias
- Total de fornecedores
- Total de produtos
- Produtos ativos
- Produtos com estoque baixo
- Valor total do estoque
- Total de movimentações

---

# Modelo de Dados

## Categoria

```text
Id
Nome
Descricao
Ativo
```

## Fornecedor

```text
Id
RazaoSocial
NomeFantasia
Cnpj
Email
Telefone
Ativo
```

## Produto

```text
Id
CategoriaId
FornecedorId
Nome
Descricao
QuantidadeAtual
EstoqueMinimo
ValorUnitario
Ativo
```

## MovimentacaoEstoque

```text
Id
TipoMovimentacao
DataMovimentacao
Observacao
```

## ItemMovimentacaoEstoque

```text
Id
MovimentacaoEstoqueId
ProdutoId
Quantidade
ValorUnitario
```

---

# Tipos de Movimentação

```text
1 = Entrada
2 = Saída
3 = Ajuste
```

---

# Regras de Negócio

## Entrada

A quantidade informada é somada ao estoque atual do produto.

Exemplo:

```text
Estoque Atual: 10

Entrada: +5

Novo Estoque: 15
```

---

## Saída

A quantidade informada é subtraída do estoque atual do produto.

Exemplo:

```text
Estoque Atual: 15

Saída: -3

Novo Estoque: 12
```

A saída não é permitida quando a quantidade solicitada for maior que o estoque disponível.

---

## Ajuste

O ajuste define o estoque final do produto.

Exemplo:

```text
Estoque Atual: 100

Ajuste Informado: 80

Novo Estoque: 80
```

---

# Configuração do Banco de Dados

O projeto utiliza SQL Server.

A string de conexão deve ser configurada no arquivo:

```text
SmartEstoqueAPI.Api/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SmartEstoque;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

# Schema Utilizado

Todas as tabelas do projeto são criadas no schema:

```text
estoque
```

Exemplo:

```text
estoque.Categoria
estoque.Fornecedor
estoque.Produto
estoque.MovimentacaoEstoque
estoque.ItemMovimentacaoEstoque
```

---

# Histórico das Migrations

O histórico das migrations é armazenado no próprio schema do projeto:

```text
estoque.__EFMigrationsHistory
```

Configuração utilizada:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable(
                "__EFMigrationsHistory",
                "estoque");
        });
});
```

---

# Como Executar as Migrations

Acesse a pasta:

```bash
cd src
```

---

## Criar uma Migration

```bash
dotnet ef migrations add InitialCreate --project SmartEstoqueAPI.Infrastructure --startup-project SmartEstoqueAPI.Api
```

Exemplo:

```bash
dotnet ef migrations add AdicionarTabelaProduto --project SmartEstoqueAPI.Infrastructure --startup-project SmartEstoqueAPI.Api
```

---

## Aplicar Migration no Banco

```bash
dotnet ef database update --project SmartEstoqueAPI.Infrastructure --startup-project SmartEstoqueAPI.Api
```

---

## Remover Última Migration

Caso a migration ainda não tenha sido aplicada ao banco:

```bash
dotnet ef migrations remove --project SmartEstoqueAPI.Infrastructure --startup-project SmartEstoqueAPI.Api
```

---

# Como Executar o Projeto

Acesse a pasta da API:

```bash
cd src/SmartEstoqueAPI.Api
```

Execute:

```bash
dotnet run
```

---

# Swagger

Após executar o projeto, acesse:

```text
https://localhost:{porta}/swagger
```

---

# Endpoints

## Categorias

```http
GET    /api/categorias
GET    /api/categorias/{id}
POST   /api/categorias
PUT    /api/categorias/{id}
DELETE /api/categorias/{id}
```

---

## Fornecedores

```http
GET    /api/fornecedores
GET    /api/fornecedores/{id}
POST   /api/fornecedores
PUT    /api/fornecedores/{id}
DELETE /api/fornecedores/{id}
```

---

## Produtos

```http
GET    /api/produtos
GET    /api/produtos/{id}
POST   /api/produtos
PUT    /api/produtos/{id}
DELETE /api/produtos/{id}
```

### Produtos com Estoque Baixo

```http
GET /api/produtos/estoque-baixo
```

### Saldo do Estoque

```http
GET /api/produtos/saldo
```

---

## Tipos de Movimentação

```http
GET /api/tiposmovimentacao
```

Retorno:

```json
[
  {
    "id": 1,
    "nome": "Entrada"
  },
  {
    "id": 2,
    "nome": "Saida"
  },
  {
    "id": 3,
    "nome": "Ajuste"
  }
]
```

---

## Movimentações de Estoque

```http
GET    /api/movimentacoesestoque
GET    /api/movimentacoesestoque/{id}
POST   /api/movimentacoesestoque
```

### Consulta por Período

```http
GET /api/movimentacoesestoque?dataInicial=2026-06-01&dataFinal=2026-06-30
```

---

## Dashboard

```http
GET /api/dashboard
```

Exemplo de retorno:

```json
{
  "totalCategorias": 5,
  "totalFornecedores": 12,
  "totalProdutos": 150,
  "produtosAtivos": 145,
  "produtosComEstoqueBaixo": 8,
  "valorTotalEstoque": 25630.50,
  "totalMovimentacoes": 321
}
```

---

# Exemplo de Entrada de Estoque

```json
{
  "tipoMovimentacao": 1,
  "observacao": "Entrada inicial",
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 10,
      "valorUnitario": 25.00
    }
  ]
}
```

---

# Exemplo de Saída de Estoque

```json
{
  "tipoMovimentacao": 2,
  "observacao": "Venda",
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 2,
      "valorUnitario": 25.00
    }
  ]
}
```

---

# Exemplo de Ajuste de Estoque

```json
{
  "tipoMovimentacao": 3,
  "observacao": "Inventário",
  "itens": [
    {
      "produtoId": 1,
      "quantidade": 80,
      "valorUnitario": 25.00
    }
  ]
}
```

---

# Objetivo do Projeto

Este projeto foi desenvolvido com finalidade educacional para reforçar conhecimentos em:

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Migrations
- AutoMapper
- DTOs
- CRUD
- Relacionamentos
- Regras de Negócio
- Dashboard
- Arquitetura em Camadas
- Controle de Estoque

---
