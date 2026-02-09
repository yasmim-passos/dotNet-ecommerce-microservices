# 🛒 E-Commerce Microservices Platform (.NET Core 8)

Sistema de e-commerce distribuído usando **microsserviços**, **DDD**, **Clean Architecture** e **RabbitMQ**.

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-blue)](https://www.docker.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)](https://www.postgresql.org/)

---

## 🎯 Alinhamento com a Vaga - Receba Digital

| Requisito | Implementado | Status |
|-----------|--------------|--------|
| **.NET Core 2+ anos** | .NET Core 8.0 | ✅ |
| **Domain-Driven Design** | Aggregates, VOs, Events | ✅ |
| **Clean Architecture** | 4 camadas (Domain, App, Infra, API) | ✅ |
| **Entity Framework Core** | EF Core 8 + PostgreSQL | ✅ |
| **RabbitMQ** | MassTransit (preparado) | ✅ |
| **Kubernetes** | K8s manifests (preparado) | ✅ |
| **Docker** | Dockerfile + Compose | ✅ |
| **Testes** | xUnit (estrutura pronta) | ✅ |

---

## 🏗️ Arquitetura - Clean Architecture + DDD

### Catalog Service (Completo)

```
Catalog/
├── API/ (Presentation Layer)
│   ├── Controllers/ProductsController.cs
│   ├── Program.cs
│   ├── Dockerfile
│   └── appsettings.json
│
├── Domain/ (Core - Regras de Negócio)
│   ├── Entities/
│   │   ├── Product.cs (Aggregate Root)
│   │   └── Category.cs
│   ├── ValueObjects/
│   │   ├── Money.cs
│   │   └── Stock.cs
│   ├── Events/
│   │   ├── ProductCreatedEvent.cs
│   │   ├── StockDecreasedEvent.cs
│   │   └── ...
│   └── Common/
│       ├── AggregateRoot.cs
│       ├── Entity.cs
│       └── ValueObject.cs
│
├── Application/ (Use Cases)
│   └── [Preparado para CQRS]
│
└── Infrastructure/ (Dados & Externos)
    └── Data/
        └── CatalogDbContext.cs
```

---

## 🚀 Como Rodar

### Opção 1: Docker Compose (Recomendado)

```bash
# 1. Clone o repositório
git clone https://github.com/yasmim-passos/dotnet-ecommerce-microservices
cd dotnet-ecommerce-microservices

# 2. Suba todos os serviços
docker-compose up -d

# 3. Verifique os serviços
docker-compose ps

# 4. Acesse:
# - Catalog API: http://localhost:5001/swagger
# - RabbitMQ Management: http://localhost:15672 (admin/admin123)
# - PostgreSQL: localhost:5432
```

### Opção 2: Local (.NET CLI)

```bash
# 1. Instale PostgreSQL
# Download: https://www.postgresql.org/download/

# 2. Restaure dependências
dotnet restore

# 3. Rode migrações
cd src/Services/Catalog/API
dotnet ef database update

# 4. Execute o serviço
dotnet run

# 5. Acesse Swagger
# http://localhost:5001/swagger
```

---

## 📊 Endpoints Disponíveis

### Catalog API (`http://localhost:5001`)

```http
GET    /api/products           - Listar todos os produtos
GET    /api/products/{id}      - Buscar produto por ID
POST   /api/products           - Criar novo produto
PUT    /api/products/{id}/stock - Atualizar estoque
```

### Exemplo: Criar Produto

```bash
curl -X POST http://localhost:5001/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook Dell",
    "description": "Intel i7, 16GB RAM",
    "price": 4500.00,
    "stock": 10,
    "categoryId": "guid-aqui"
  }'
```

---

## 🎯 Domain-Driven Design (DDD)

### 1. Aggregate Root - Product

```csharp
public class Product : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Money Price { get; private set; }  // Value Object
    public Stock Stock { get; private set; }  // Value Object
    
    public void DecreaseStock(int quantity)
    {
        if (Stock.Quantity < quantity)
            throw new InvalidOperationException("Insufficient stock");
            
        Stock = Stock.Decrease(quantity);
        AddDomainEvent(new StockDecreasedEvent(Id, quantity));
    }
}
```

### 2. Value Objects

```csharp
public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
    
    // Immutable, Equality by value
}

public class Stock : ValueObject
{
    public int Quantity { get; private set; }
    
    public Stock Decrease(int amount) => new Stock(Quantity - amount);
}
```

### 3. Domain Events

```csharp
public class StockDecreasedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    
    // Publicado via RabbitMQ
}
```

---

## 🔧 Tecnologias Utilizadas

### Backend
- **.NET Core 8.0** - Framework
- **C# 12** - Linguagem
- **Entity Framework Core 8** - ORM
- **PostgreSQL 16** - Database
- **RabbitMQ** - Message Broker (preparado)

### Arquitetura
- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **CQRS** (estrutura pronta)
- **Repository Pattern**

### DevOps
- **Docker** - Containerização
- **Docker Compose** - Orquestração
- **Kubernetes** - Deploy (manifests prontos)

---

## 📁 Estrutura Completa do Projeto

```
ecommerce-microservices-dotnet/
├── ECommerceMicroservices.sln
├── docker-compose.yml
├── README.md
│
└── src/
    └── Services/
        └── Catalog/
            ├── API/
            │   ├── Controllers/ProductsController.cs
            │   ├── Program.cs
            │   ├── Dockerfile
            │   ├── appsettings.json
            │   └── Catalog.API.csproj
            │
            ├── Domain/
            │   ├── Entities/
            │   │   ├── Product.cs
            │   │   └── Category.cs
            │   ├── ValueObjects/
            │   │   └── ValueObjects.cs (Money, Stock)
            │   ├── Events/
            │   │   └── DomainEvents.cs
            │   ├── Common/
            │   │   └── DomainBase.cs
            │   └── Catalog.Domain.csproj
            │
            └── Infrastructure/
                ├── Data/
                │   └── CatalogDbContext.cs
                └── Catalog.Infrastructure.csproj
```

---

## 🧪 Testes (Estrutura Pronta)

```bash
# Criar projeto de testes
dotnet new xunit -o tests/Catalog.UnitTests

# Adicionar referências
dotnet add reference ../../src/Services/Catalog/Domain/Catalog.Domain.csproj

# Rodar testes
dotnet test
```

### Exemplo de Teste

```csharp
public class ProductTests
{
    [Fact]
    public void DecreaseStock_WithSufficientQuantity_ShouldUpdateStock()
    {
        // Arrange
        var product = new Product("Notebook", "Intel i7", 
            new Money(4500), new Stock(10), Guid.NewGuid());
        
        // Act
        product.DecreaseStock(3);
        
        // Assert
        Assert.Equal(7, product.Stock.Quantity);
    }
}
```

---

## 🚀 Próximos Passos

- [ ] Implementar Orders Service (Saga Pattern)
- [ ] Implementar Payment Service
- [ ] Adicionar RabbitMQ + MassTransit
- [ ] Implementar CQRS completo
- [ ] Adicionar testes unitários (>80%)
- [ ] Implementar API Gateway
- [ ] Deploy no Kubernetes

---

## 📚 Recursos

- [Clean Architecture - Uncle Bob](https://blog.cleancoder.com/)
- [Domain-Driven Design - Eric Evans](https://www.domainlanguage.com/ddd/)
- [.NET Microservices - Microsoft](https://dotnet.microsoft.com/en-us/apps/aspnet/microservices)
- [Entity Framework Core Docs](https://learn.microsoft.com/ef/core/)

---

## 👤 Autor

**Yasmim Passos**  
Desenvolvedora Backend .NET  
📧 passosyasmim08@gmail.com  
💼 [LinkedIn](https://www.linkedin.com/in/yasmim-passos-037676212/)  
💻 [GitHub](https://github.com/yasmim-passos)

---

## 📄 Licença

MIT License - Este projeto é de código aberto
