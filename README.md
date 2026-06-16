# Design Patterns Demo — WebApi

Proyecto de demostración en .NET 10 que implementa una API REST de registro de ventas. El objetivo es mostrar patrones de diseño clásicos (Chain of Responsibility, Strategy, Repository, Use Case) aplicados de forma práctica sobre una arquitectura en capas.

---

## Stack tecnológico

| Componente | Tecnología |
|---|---|
| Framework | .NET 10 / ASP.NET Core |
| ORM | Entity Framework Core 10 |
| Base de datos | SQLite |
| Versionado de API | Asp.Versioning 10 (URL segment) |
| Documentación | Swagger + Scalar UI |
| Diagramas | PlantUML (generados vía Kroki) |

---

## Arquitectura en capas

```
┌─────────────────────────────────┐
│         Presentación            │  Controllers, Action/Result DTOs
├─────────────────────────────────┤
│         Aplicación              │  Use Cases, Handlers, Strategies
├─────────────────────────────────┤
│           Dominio               │  Entidades, Value Objects, Interfaces
├─────────────────────────────────┤
│        Infraestructura          │  EF Core, Repositorios, AppDbContext
└─────────────────────────────────┘
                 │
              SQLite
```

Las dependencias apuntan siempre hacia el Dominio. La capa de Dominio no referencia ninguna capa externa.

---

## Patrones de diseño implementados

### 1. Chain of Responsibility

El registro de una venta ejecuta una cadena de cuatro handlers. Cada handler resuelve una responsabilidad y cede el control al siguiente.

```
SaleCreationUseCase
        │
        ▼
SearchOrCreateCustomerHandler   → Busca o crea el cliente por NIT
        │
        ▼
CreateSaleHandler               → Construye la venta, valida productos
        │
        ▼
ApplyDiscountHandler            → Selecciona y aplica la estrategia de descuento
        │
        ▼
PersistSaleHandler              → Persiste cliente y venta en la base de datos
```

El endpoint `POST /v1/sales/discount` reutiliza los tres primeros handlers sin ejecutar `PersistSaleHandler`, lo que permite calcular un descuento sin registrar la venta.

### 2. Strategy

El cálculo de descuento delega en una estrategia intercambiable en tiempo de ejecución. `ApplyDiscountHandler` elige la estrategia y la inyecta en `DiscountStrategyContext`.

| Estrategia | Condición | Descuento |
|---|---|---|
| `NewCustomerDiscountStrategy` | Cliente nuevo (TotalSales = 0) | 5 % |
| `VipClientDiscountStrategy` | TotalSales ≥ 10 000 | 8 % |
| `SaleAmountDiscountStrategy` | Monto de venta ≥ 1 000 | 2 % |

Solo se aplica una estrategia por venta; la prioridad sigue el orden de la tabla.

### 3. Repository

El Dominio define interfaces (`ICustomersRepository`, `IProductsRepository`, `ISalesRepository`) sin conocer EF Core. La Infraestructura provee las implementaciones (`EfCustomersRepository`, etc.) registradas por inyección de dependencias.

### 4. Use Case / Action-Result

Cada caso de uso expone un método `RealizeAsync(Action)` que recibe un objeto de entrada (Action) y retorna un objeto de salida (Result). Los controladores solo conocen estos contratos; no acceden a repositorios ni a handlers directamente.

```
Controller  →  UseCase.RealizeAsync(Action)  →  Result
```

---

## Estructura del proyecto

```
DesignPatternsDemo/
├── WebApi/
│   ├── Controllers/
│   │   ├── CustomersController.cs
│   │   ├── ProductsController.cs
│   │   └── SalesController.cs
│   │
│   ├── Customers/                      # Módulo de clientes
│   │   ├── Actions/
│   │   │   ├── GetCustomerAction.cs
│   │   │   └── SearchCustomerByNitAction.cs
│   │   ├── Results/
│   │   │   └── CustomerResult.cs
│   │   └── UseCases/
│   │       ├── GetCustomerUseCase.cs
│   │       ├── ListCustomersUseCase.cs
│   │       └── SearchCustomerByNitUseCase.cs
│   │
│   ├── Products/                       # Módulo de productos
│   │   ├── Actions/
│   │   │   └── GetProductAction.cs
│   │   ├── Results/
│   │   │   └── ProductResult.cs
│   │   └── UseCases/
│   │       ├── GetProductUseCase.cs
│   │       └── ListProductsUseCase.cs
│   │
│   ├── Sales/                          # Módulo de ventas
│   │   ├── Handlers/                   # Chain of Responsibility
│   │   │   ├── SaleBaseHandler.cs
│   │   │   ├── SearchOrCreateCustomerHandler.cs
│   │   │   ├── CreateSaleHandler.cs
│   │   │   ├── ApplyDiscountHandler.cs
│   │   │   └── PersistSaleHandler.cs
│   │   ├── Strategies/
│   │   │   └── Discounts/              # Strategy Pattern
│   │   │       ├── IDiscountStrategy.cs
│   │   │       ├── DiscountStrategyContext.cs
│   │   │       ├── NewCustomerDiscountStrategy.cs
│   │   │       ├── VipClientDiscountStrategy.cs
│   │   │       └── SaleAmountDiscountStrategy.cs
│   │   └── UseCases/
│   │       ├── SaleCreationUseCase.cs
│   │       ├── CalculateDiscountUseCase.cs
│   │       └── ...
│   │
│   ├── Domain/                         # Dominio (sin dependencias externas)
│   │   ├── Entities/
│   │   │   ├── Customer.cs
│   │   │   ├── Product.cs
│   │   │   └── Sale.cs
│   │   ├── ValueObjects/
│   │   │   ├── Discount.cs
│   │   │   └── SaleItem.cs
│   │   ├── Repositories/
│   │   │   ├── ICustomersRepository.cs
│   │   │   ├── IProductsRepository.cs
│   │   │   └── ISalesRepository.cs
│   │   └── Exceptions/
│   │       └── ProductNotFoundException.cs
│   │
│   └── Infrastructure/                 # EF Core, repositorios, DI
│       ├── Data/
│       │   ├── AppDbContext.cs
│       │   ├── Mapping/               # Entity Type Configurations
│       │   └── Migrations/
│       ├── Repositories/
│       └── ServiceCollectionExtensions.cs
│
├── docs/
│   └── WebApi/
│       ├── architecture-overview.puml
│       ├── class-001-controllers.puml
│       ├── class-002-chain-of-responsability.puml
│       ├── class-003-repositories.puml
│       ├── class-004-discount-strategy.puml
│       ├── seq-001-register-sale.puml
│       ├── seq-002-calculate-discount.puml
│       ├── seq-003-handler-search-or-create-customer.puml
│       ├── seq-004-handler-create-sale.puml
│       ├── seq-005-handler-apply-discount.puml
│       ├── seq-006-handler-persist-sale.puml
│       └── classes/                   # Diagramas individuales por clase
│
└── WebApp/                            # Frontend Angular (cliente de la API)
```

---

## Cómo ejecutar

### Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Ejecución

```bash
cd WebApi
dotnet run
```

La base de datos SQLite (`app.db`) se crea y migra automáticamente al arrancar. La Scalar UI estará disponible en `https://localhost:{puerto}/scalar/v1`.

### CORS

La API permite peticiones desde `http://localhost:4200` (Angular dev server). Para cambiar el origen edita `ServiceCollectionExtensions.cs`.

---

## Endpoints

### Ventas — `v1/sales`

| Método | Ruta | Descripción |
|---|---|---|
| `POST` | `/v1/sales` | Registra una venta completa y la persiste |
| `POST` | `/v1/sales/discount` | Calcula el descuento sin registrar la venta |

**Body (ambos endpoints):**

```json
{
  "customerFullName": "Juan Pérez",
  "customerNit": "12345678",
  "items": [
    { "productId": "11111111-1111-1111-1111-111111111111", "quantity": 2 },
    { "productId": "33333333-3333-3333-3333-333333333333", "quantity": 1 }
  ]
}
```

**Respuesta `POST /v1/sales` — 201 Created:**

```json
{
  "saleId": "...",
  "customerName": "Juan Pérez",
  "total": 1480.00,
  "discount": { "message": "Descuento por monto de venta", "amount": 29.60 }
}
```

### Productos — `v1/products`

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/v1/products` | Lista todos los productos |
| `GET` | `/v1/products/{id}` | Obtiene el detalle de un producto |

### Clientes — `v1/customers`

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/v1/customers` | Lista todos los clientes |
| `GET` | `/v1/customers?nit={nit}` | Busca un cliente por NIT |
| `GET` | `/v1/customers/{id}` | Obtiene el detalle de un cliente |

---

## Datos de prueba (seed)

La migración inicial inserta cuatro productos listos para usar:

| ID | Nombre | Precio unitario |
|---|---|---|
| `11111111-...` | Teclado mecánico | 250.00 |
| `22222222-...` | Mouse inalámbrico | 120.00 |
| `33333333-...` | Monitor 24 pulgadas | 980.00 |
| `44444444-...` | Audífonos con micrófono | 340.00 |

---

## Diagramas

Los diagramas PlantUML están en `docs/WebApi/`. Pueden renderizarse con [Kroki](https://kroki.io) o cualquier plugin compatible con PlantUML.

| Archivo | Contenido |
|---|---|
| `architecture-overview.puml` | Vista de capas de la arquitectura |
| `class-001-controllers.puml` | Dependencias de controladores hacia Use Cases |
| `class-002-chain-of-responsability.puml` | Cadena de handlers para registro y cálculo de ventas |
| `class-003-repositories.puml` | Entidades, interfaces de repositorio e implementaciones EF |
| `class-004-discount-strategy.puml` | Contexto de estrategia y tres implementaciones de descuento |
| `seq-001-register-sale.puml` | Flujo completo de registro de venta |
| `seq-002-calculate-discount.puml` | Flujo de cálculo de descuento sin persistencia |
| `seq-003-handler-search-or-create-customer.puml` | Lógica de búsqueda o creación de cliente |
| `seq-004-handler-create-sale.puml` | Construcción de la venta e iteración sobre productos |
| `seq-005-handler-apply-discount.puml` | Selección de estrategia y cálculo de descuento |
| `seq-006-handler-persist-sale.puml` | Actualización de TotalSales y persistencia |
