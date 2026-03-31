# E-Commerce Order Management API

A RESTful API built with ASP.NET Core 10 and Entity Framework Core that handles order lifecycle management for an e-commerce platform. The project demonstrates clean layered architecture, domain-driven design principles, and the repository pattern.

---

## What it does

- Create orders with one or more items
- Add items to existing orders
- Process payments (fake gateway)
- Cancel orders with business rule enforcement
- Fetch order details with full item breakdown

---

## Architecture

The project is organized into four distinct layers, each with a single responsibility.

```
Controller → Service → Repository → Database
```

**Domain** holds the core business entities and rules. `Order`, `OrderItem`, `Product`, and `Customer` are plain C# classes with no framework dependencies. Business rules like "a cancelled order cannot be modified" or "an empty order cannot be paid" live here as methods on the entity — not in the service or controller.

**Application** holds service interfaces and implementations. `OrderService` orchestrates the full create-order flow: fetch products from inventory, build the order, process payment, persist, and send a notification. It never touches `DbContext` directly.

**Infrastructure** holds the EF Core implementation, repository classes, a fake payment gateway, and a stub email notification service.

**Contracts** holds request and response DTOs as `record` types. These separate the API surface from the internal domain model — the controller never exposes a raw entity.

---

## Key design decisions

**Repository pattern** — `IOrderRepository` and `IProductRepository` sit in the Application layer as interfaces. The service depends on the abstraction, not on EF Core. This means the data layer can be swapped without touching business logic.

**DomainException** — business rule violations throw a `DomainException` instead of a generic `Exception`. A global exception handler in `Program.cs` maps `DomainException` to HTTP 400 and everything else to 500. Controllers stay clean.

**Private setters on entities** — domain entities use private setters and expose behavior through methods (`AddItem`, `CancelOrder`, `MarkAsPaid`). Outside code cannot put an entity into an invalid state.

**DTOs over entities** — the API never serializes domain entities directly. Response records contain only what a client needs. Enum values are returned as strings so renaming an enum value does not break clients.

**FluentValidation** — request validation runs automatically before controller actions execute. Invalid requests return a structured 400 with field-level error messages before any business logic runs.

---

## Tech stack

- ASP.NET Core 10
- Entity Framework Core with SQL Server
- FluentValidation
- Scalar (OpenAPI UI)

---

## How to run locally

**Prerequisites**
- .NET 10 SDK
- SQL Server (local instance or LocalDB)

**Steps**

1. Clone the repo
```bash
git clone https://github.com/faizkhan005/e-commerce-order-management-api
cd e-commerce-order-management-api
```

2. Update the connection string in `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=OrderManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Apply migrations
```bash
dotnet ef database update
```

4. Run the project
```bash
dotnet run
```

5. Open the API explorer at `http://localhost:5066/scalar/v1`

---

## Seeding test data

Before testing, insert a product and customer directly in SSMS or via a future `ProductsController`:

```sql
INSERT INTO Customers (CustomerID, CustomerFirstName, CustomerLastName, Address, PhoneNumber, City, State, Country, Zip)
VALUES (NEWID(), 'John', 'Doe', '123 Main St', '555-1234', 'Cleveland', 'OH', 'USA', '44101')

INSERT INTO Products (ProductID, ProductName, ProductDescription, UnitPrice, ListPrice, ProductCategory, DiscountPercentage)
VALUES (NEWID(), 'Test Product', 'A test product', 9.99, 12.99, 'Electronics', 0)
```

---

## API endpoints

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/Orders/CreateOrder` | Create a new order with items |
| GET | `/api/Orders/GetOrderById/{orderId}` | Fetch order details |
| POST | `/api/Orders/AddOrderItem/{orderId}` | Add an item to an existing order |
| PATCH | `/api/Orders/CancelOrder/{orderId}` | Cancel an order |

**Sample create order request**
```json
{
  "customerID": "your-customer-guid",
  "orderedItems": [
    {
      "productID": "your-product-guid",
      "qty": 2
    }
  ]
}
```

---

## Folder structure

```
OrderManagementApi/
├── Controllers/
├── Contracts/
│   ├── Requests/
│   ├── Responses/
│   └── Validators/
├── Domain/
│   ├── Entities/
│   ├── Enums/
│   └── Exceptions/
├── Application/
│   ├── Interfaces/
│   └── Services/
└── Infrastructure/
    ├── Persistence/
    ├── Payments/
    └── Notifications/
```
