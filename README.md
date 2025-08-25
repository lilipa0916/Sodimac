# 📦 Sodimac Orders API

Sistema de gestión de pedidos desarrollado con .NET 8 siguiendo **Clean Architecture**, implementando patrones **SOLID**, **CQRS** con **MediatR**, **Entity Framework Core** y validaciones con **FluentValidation**.

## 🎯 Descripción del Proyecto

API RESTful para la gestión completa de pedidos, clientes y rutas de entrega. El sistema permite realizar operaciones CRUD, consultas específicas, actualización de estados de pedidos y seguimiento de rutas de entrega.

## 🏗️ Arquitectura

La solución implementa **Clean Architecture** con la siguiente estructura:

```
📂 Sodimac/
├── 📂 Sodimac.Orders.Domain/          # Entidades de dominio, enums
├── 📂 Sodimac.Orders.Application/     # CQRS, DTOs, Validadores, Interfaces
├── 📂 Sodimac.Orders.Infrastructure/  # EF Core, Repositorios, Persistencia
└── 📂 Sodimac.Orders.Api/            # Controladores, Configuración, Swagger
```

### Principios Aplicados
- ✅ **Clean Architecture** - Separación clara de responsabilidades
- ✅ **SOLID Principles** - Código mantenible y extensible
- ✅ **CQRS Pattern** - Separación de comandos y consultas
- ✅ **Repository Pattern** - Abstracción de acceso a datos
- ✅ **Unit of Work Pattern** - Transacciones consistentes

## 📊 Modelo de Datos

### Entidades Principales

| Entidad | Descripción | Campos Principales |
|---------|-------------|-------------------|
| **Cliente** | Información del cliente | Id, Nombre, Email, Dirección, Activo |
| **Pedido** | Pedido principal | Id, ClienteId, FechaPedido, FechaEntrega, MontoTotal |
| **Producto** | Items del pedido | Id, PedidoId, Código, Nombre, Cantidad, PrecioUnitario |
| **DeliveryRoute** | Ruta de entrega | Id, PedidoId, EstadoId, NombreRuta, FechaAsignación |
| **DeliveryStatus** | Estados de entrega | Id, Nombre, Descripción, EsEstadoFinal |

### Estados de Entrega

| Id | Estado | Descripción | Final |
|----|---------|-------------|-------|
| 1 | Pendiente | Pedido creado, pendiente de asignación | ❌ |
| 2 | Asignado | Pedido asignado a ruta de entrega | ❌ |
| 3 | En Tránsito | Pedido en camino al cliente | ❌ |
| 4 | Entregado | Pedido entregado exitosamente | ✅ |
| 5 | Cancelado | Pedido cancelado | ✅ |

## 🚀 Tecnologías y Paquetes

### Backend (.NET 8)
- **MediatR** v12.5.0 - Patrón CQRS
- **AutoMapper** v12.0.1 - Mapeo objeto-objeto
- **FluentValidation** v11.11.0 - Validaciones
- **Entity Framework Core** v8.0.19 - ORM
- **SQL Server** - Base de datos
- **Swagger/OpenAPI** - Documentación de API

## ⚙️ Configuración e Instalación

### Prerrequisitos
- .NET 8 SDK
- SQL Server (LocalDB o completo)
- Visual Studio 2022 / VS Code
- SQL Server Management Studio (opcional)

### 1. Clonar el Repositorio
```bash
git clone https://github.com/lilipa0916/Sodimac.git
cd Sodimac
```

### 2. Configurar Base de Datos

#### Opción A: LocalDB (Recomendado para desarrollo)
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SodimacOrdersDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

#### Opción B: SQL Server Completo
```json
// appsettings.json  
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SodimacOrdersDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Ejecutar Migraciones
```bash
# Desde la raíz del proyecto
cd Sodimac.Orders.Infrastructure
dotnet ef database update --startup-project ../Sodimac.Orders.Api
```

### 4. Ejecutar la Aplicación
```bash
# Desde la raíz del proyecto
cd Sodimac.Orders.Api
dotnet run
```

La API estará disponible en:
- **HTTP**: `http://localhost:5138`
- **HTTPS**: `https://localhost:7203`
- **Swagger UI**: `https://localhost:7203/swagger`

## 📚 Endpoints de la API

### 🧑‍💼 Clientes
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/cliente` | Obtener todos los clientes |
| `POST` | `/api/cliente` | Crear nuevo cliente |

### 📦 Pedidos
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/pedido` | Obtener todos los pedidos |
| `GET` | `/api/pedido/{id}` | Obtener pedido por ID |
| `GET` | `/api/pedido/cliente/{clienteId}` | Obtener pedidos por cliente |
| `POST` | `/api/pedido` | Crear nuevo pedido |
| `PUT` | `/api/pedido/{id}` | Actualizar pedido completo |
| `DELETE` | `/api/pedido/{id}` | Eliminar pedido |
| `PATCH` | `/api/pedido/{id}/estado` | Actualizar estado del pedido |

## 📋 Ejemplos de Uso

### Crear Cliente
```json
POST /api/cliente
{
  "nombre": "Juan Pérez",
  "email": "juan.perez@email.com",
  "direccion": "Av. Providencia 123, Santiago"
}
```

### Crear Pedido
```json
POST /api/pedido
{
  "pedido": {
    "clienteId": 1,
    "fechaEntrega": "2025-08-30T10:00:00",
    "observaciones": "Entrega en horario de oficina",
    "productos": [
      {
        "producto": "Taladro Eléctrico",
        "cantidad": 2,
        "precioUnitario": 45990.00
      },
      {
        "producto": "Destornillador Set",
        "cantidad": 1,
        "precioUnitario": 12990.00
      }
    ],
    "rutaEntrega": {
      "nombreRuta": "Ruta Santiago Centro",
      "deliveryStatusId": 1,
      "observaciones": "Contactar 30 min antes"
    }
 }
}
```

### Actualizar Estado de Pedido
```json
PATCH /api/pedido/1/estado
Content-Type: application/json

3  // Estado: En Tránsito
```

## 🧪 Validaciones Implementadas

### Pedidos
- ✅ ClienteId requerido y válido
- ✅ FechaEntrega debe ser mayor a fecha actual
- ✅ Al menos un producto requerido
- ✅ Monto total calculado automáticamente

### Productos
- ✅ Nombre requerido (máx. 100 caracteres)
- ✅ Cantidad mayor a 0
- ✅ Precio unitario mayor a 0
- ✅ Código generado automáticamente

### Clientes
- ✅ Nombre requerido (máx. 100 caracteres)
- ✅ Email válido y único
- ✅ Dirección requerida

## 🗄️ Script de Base de Datos

Si prefieres crear la base de datos manualmente, utiliza el archivo `SodimacBD.sql` incluido en el proyecto:

```bash
sqlcmd -S (localdb)\mssqllocaldb -i SodimacBD.sql
```

## 🧩 Patrones y Estructura

### CQRS con MediatR
```csharp
// Comando
public record CreatePedidoCommand(CreatePedidoDto Pedido) : IRequest<PedidoDto>;

// Handler
public class CreatePedidoHandler : IRequestHandler<CreatePedidoCommand, PedidoDto>
{
    // Implementación...
}

// Uso en Controller
var result = await _mediator.Send(new CreatePedidoCommand(pedidoDto));
```

### Repository Pattern
```csharp
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
```

## 🔧 Configuración Adicional

### Logging
```json
// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

### CORS (Para desarrollo con Angular)
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

## 🚀 Próximos Pasos - Frontend Angular

### Estructura Sugerida
```
📂 sodimac-frontend/
├── 📂 src/app/
│   ├── 📂 core/          # Guards, Interceptors, Services
│   ├── 📂 shared/        # Componentes compartidos
│   ├── 📂 features/      # Módulos de funcionalidades
│   │   ├── 📂 clientes/
│   │   ├── 📂 pedidos/
│   │   └── 📂 dashboard/
│   └── 📂 models/        # Interfaces TypeScript
```

### Tecnologías Frontend Recomendadas
- **Angular 17+** con Standalone Components
- **Angular Material** para UI
- **NgRx** para gestión de estado
- **Angular HTTP Client** para consumo de API
- **Chart.js** para dashboards

## 👨‍💻 Información del Desarrollador

**Liliana Paola Muñoz Cortés**  
Desarrolladora Full Stack .NET | Clean Architecture | CQRS | Entity Framework Core | Angular

---

## 📄 Licencia

Este proyecto fue desarrollado como prueba técnica y está disponible para fines educativos y de evaluación.

---

⭐ **¡No olvides dar una estrella si te gustó el proyecto!**
