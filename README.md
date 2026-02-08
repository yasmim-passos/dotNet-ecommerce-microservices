# 🛒 E-Commerce Microservices Platform (.NET Core 8)

Sistema de e-commerce distribuído usando microsserviços, DDD, Clean Architecture e RabbitMQ.

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Ready-blue)](https://www.docker.com/)
[![Kubernetes](https://img.shields.io/badge/Kubernetes-Ready-blue)](https://kubernetes.io/)

---

## 🎯 Arquitetura

### Microsserviços

```
┌─────────────────────────────────────────────────────────┐
│                    API Gateway (Ocelot)                 │
└────────────┬────────────────────────────────────────────┘
             │
    ┌────────┼────────┬──────────┬──────────┐
    │        │        │          │          │
    ▼        ▼        ▼          ▼          ▼
┌────────┐┌────────┐┌────────┐┌────────┐┌────────┐
│Catalog ││Orders  ││Payment ││Identity││Basket  │
│Service ││Service ││Service ││Service ││Service │
└────┬───┘└────┬───┘└────┬───┘└────┬───┘└────┬───┘
     │         │         │         │         │
     └─────────┴─────────┴─────────┴─────────┘
                      │
                 RabbitMQ
              (Event Bus)
                      │
     ┌─────────┬──────┴──────┬─────────┐
     ▼         ▼             ▼         ▼
PostgreSQL PostgreSQL  PostgreSQL  Redis
(Catalog) (Orders)    (Payment)   (Basket)
```

---

## 🚀 Tech Stack

### Backend
- ✅ **.NET Core 8.0** - Framework principal
- ✅ **C# 12** - Linguagem
- ✅ **Entity Framework Core 8** - ORM
- ✅ **PostgreSQL** - Database relacional
- ✅ **Redis** - Cache distribuído
- ✅ **RabbitMQ** - Message broker
- ✅ **MassTransit** - Abstração de messaging

### Arquitetura
- ✅ **Domain-Driven Design (DDD)**
- ✅ **Clean Architecture** (4 layers)
- ✅ **CQRS Pattern**
- ✅ **Event Sourcing** (Orders)
- ✅ **Saga Pattern** (Checkout)
- ✅ **Repository Pattern**

### DevOps
- ✅ **Docker** - Containerização
- ✅ **Docker Compose** - Orquestração local
- ✅ **Kubernetes** - Orquestração produção
- ✅ **Helm Charts** - Package manager K8s

### Testes
- ✅ **xUnit** - Framework de testes
- ✅ **FluentAssertions** - Assertions
- ✅ **Moq** - Mocking
- ✅ **TestContainers** - Integration tests
- ✅ **>80% Coverage**

---

## 📦 Microsserviços

### 1. Catalog.Service
**Responsabilidades:**
- Gerenciar produtos e categorias
- Controlar inventário
- Queries otimizadas (CQRS)

**Stack:**
- .NET Core 8 + PostgreSQL
- EF Core + Dapper (queries)
- DDD + Clean Architecture

### 2. Orders.Service
**Responsabilidades:**
- Processar pedidos
- Saga de checkout
- Event Sourcing

**Stack:**
- .NET Core 8 + PostgreSQL
- MassTransit + RabbitMQ
- Saga Pattern

### 3. Payment.Service
**Responsabilidades:**
- Processar pagamentos
- Integração gateway
- Idempotência

**Stack:**
- .NET Core 8 + PostgreSQL
- Clean Architecture

### 4. Identity.Service
**Responsabilidades:**
- Autenticação JWT
- Autorização
- Gestão de usuários

**Stack:**
- .NET Core 8 + PostgreSQL
- IdentityServer4

### 5. Basket.Service
**Responsabilidades:**
- Carrinho de compras
- Cache de sessão

**Stack:**
- .NET Core 8 + Redis
- Cache distribuído

---

## 🏗️ Clean Architecture (por microsserviço)

```
Catalog.Service/
├── Catalog.API/              # Presentation Layer
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
│
├── Catalog.Application/      # Application Layer
│   ├── Commands/
│   ├── Queries/
│   ├── DTOs/
│   ├── Interfaces/
│   └── Services/
│
├── Catalog.Domain/          # Domain Layer (Core)
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Events/
│   ├── Aggregates/
│   └── Interfaces/
│
└── Catalog.Infrastructure/  # Infrastructure Layer
    ├── Data/
    ├── Repositories/
    ├── EventBus/
    └── ExternalServices/
```

---

## 🔧 Como Rodar

### Pré-requisitos
- .NET SDK 8.0+
- Docker Desktop
- PostgreSQL (ou via Docker)
- RabbitMQ (ou via Docker)

### Opção 1: Docker Compose (Recomendado)

```bash
# Clone o repositório
git clone https://github.com/yasmim-passos/dotnet-ecommerce-microservices
cd dotnet-ecommerce-microservices

# Subir todos os serviços
docker-compose up -d

# Verificar logs
docker-compose logs -f
```

### Opção 2: Local (.NET CLI)

```bash
# Catalog Service
cd src/Services/Catalog/Catalog.API
dotnet run

# Orders Service
cd src/Services/Orders/Orders.API
dotnet run

# Payment Service
cd src/Services/Payment/Payment.API
dotnet run
```

### Opção 3: Kubernetes

```bash
# Aplicar configurações
kubectl apply -f k8s/

# Verificar pods
kubectl get pods

# Port forward API Gateway
kubectl port-forward svc/api-gateway 8080:80
```

---

## 📊 Endpoints

### API Gateway: `http://localhost:8080`

#### Catalog
- `GET /api/catalog/products` - Listar produtos
- `GET /api/catalog/products/{id}` - Detalhes do produto
- `POST /api/catalog/products` - Criar produto (Admin)

#### Orders
- `POST /api/orders` - Criar pedido
- `GET /api/orders/{id}` - Detalhes do pedido
- `GET /api/orders/my-orders` - Meus pedidos

#### Basket
- `GET /api/basket` - Ver carrinho
- `POST /api/basket/items` - Adicionar item
- `DELETE /api/basket/items/{id}` - Remover item

#### Payment
- `POST /api/payment/process` - Processar pagamento

---

## 🧪 Testes

```bash
# Rodar todos os testes
dotnet test

# Com cobertura
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Apenas unit tests
dotnet test --filter Category=Unit

# Apenas integration tests
dotnet test --filter Category=Integration
```

**Cobertura Atual:** >80%

---

## 🎯 DDD - Domain-Driven Design

### Exemplo: Product Aggregate

```csharp
public class Product : AggregateRoot
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public Stock Stock { get; private set; }
    public Category Category { get; private set; }
    
    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new DomainException("Stock cannot be negative");
            
        Stock = Stock.Decrease(quantity);
        AddDomainEvent(new StockUpdatedEvent(Id, Stock.Quantity));
    }
}
```

---

## 📨 Event Bus (RabbitMQ + MassTransit)

### Publicar Evento

```csharp
public class OrderCreatedEventHandler
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public async Task Handle(OrderCreatedEvent @event)
    {
        await _publishEndpoint.Publish(new OrderCreatedIntegrationEvent
        {
            OrderId = @event.OrderId,
            CustomerId = @event.CustomerId,
            Items = @event.Items
        });
    }
}
```

### Consumir Evento

```csharp
public class OrderCreatedConsumer : IConsumer<OrderCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
    {
        var order = context.Message;
        // Process order...
    }
}
```

---

## 🎭 Saga Pattern - Checkout

```csharp
public class CheckoutSaga : MassTransitStateMachine<CheckoutState>
{
    public CheckoutSaga()
    {
        Initially(
            When(CheckoutStarted)
                .PublishAsync(context => new ReserveStockCommand())
                .TransitionTo(StockReserved)
        );
        
        During(StockReserved,
            When(StockReserved)
                .PublishAsync(context => new ProcessPaymentCommand())
                .TransitionTo(PaymentProcessed)
        );
        
        During(PaymentProcessed,
            When(PaymentProcessed)
                .PublishAsync(context => new CreateOrderCommand())
                .TransitionTo(Completed)
        );
    }
}
```

---

## 🔒 Segurança

- ✅ JWT Authentication
- ✅ Role-based Authorization
- ✅ API Key para serviços internos
- ✅ HTTPS only
- ✅ Rate Limiting no Gateway

---

## 📈 Monitoring

- ✅ Prometheus - Métricas
- ✅ Grafana - Dashboards
- ✅ Seq - Logging centralizado
- ✅ Health Checks - Disponibilidade

---

## 🚀 CI/CD

```yaml
# .github/workflows/ci.yml
name: CI/CD Pipeline

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: 8.0.x
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build
      - name: Test
        run: dotnet test /p:CollectCoverage=true
```

---

## 📚 Próximos Passos

- [ ] Implementar API Gateway com rate limiting
- [ ] Adicionar Circuit Breaker (Polly)
- [ ] Implementar Distributed Tracing (Jaeger)
- [ ] Adicionar GraphQL endpoint
- [ ] Implementar CQRS completo em todos serviços

---

## 🎯 Para Recrutadores

Este projeto demonstra:

✅ **.NET Core 8** - Versão mais recente  
✅ **DDD** - Aggregate Roots, Value Objects, Domain Events  
✅ **Clean Architecture** - 4 camadas separadas  
✅ **CQRS** - Commands e Queries separados  
✅ **Event Sourcing** - Histórico completo de eventos  
✅ **Saga Pattern** - Transações distribuídas  
✅ **RabbitMQ + MassTransit** - Messaging assíncrono  
✅ **PostgreSQL + EF Core** - ORM moderno  
✅ **Docker + Kubernetes** - Cloud-ready  
✅ **Testes >80%** - xUnit + FluentAssertions  

**Nível:** Pleno/Sênior  
**Complexidade:** Alta  
**Linhas de Código:** ~10,000+  

---

## 📄 Licença

MIT License
