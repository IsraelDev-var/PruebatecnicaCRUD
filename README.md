# 📚 Prueba Técnica – Biblioteca Digital

## 🎯 Objetivo
API RESTful para la **gestión de una biblioteca digital**.  
Permite administrar **Autores, Libros y Préstamos**, con validaciones, seguridad basada en **JWT** y control de acceso por **roles (Admin / Usuario)**.

---

## 🏗 Arquitectura
Se implementa **Onion Architecture**, separando responsabilidades:

- **Core.Domain** → Entidades y reglas de negocio.  
- **Core.Application** → Servicios, DTOs y lógica de aplicación.  
- **Infrastructure.Persistence** → Persistencia con EF Core, repositorios y migraciones.  
- **Presentation.WebAPI** → Controladores, configuración de JWT y Swagger.  

✅ Beneficios:
- Separación clara de responsabilidades.  
- Independencia de frameworks.  
- Testeable con xUnit/Moq/FluentAssertions.  
- Cumple con **SOLID (DIP, SRP)**.  

---

## 🗄 Base de Datos
Relaciones:
- **Author → Book**: 1:N  
- **Book → Loan**: 1:N  

Índices:
- `Book.YearPublished` (optimiza consultas por año).  
- `Loan.ReturnDate` (optimiza préstamos no devueltos).  

---

## 🔐 Autenticación y Autorización
- Implementación con **JWT Bearer** (`Microsoft.AspNetCore.Authentication.JwtBearer`).  
- Roles:
  - **Admin** → Puede crear, actualizar y eliminar.  
  - **User** → Solo puede consultar.  

Ejemplo de **payload**:
```json
{
  "sub": "usuario1",
  "role": "Admin",
  "exp": 1712345678
}
```

---

## 🛠 Tecnologías
- **.NET 9**  
- **Entity Framework Core (SQL Server / MySQL / InMemory)**  
- **Swagger (Swashbuckle)**  
- **JWT Authentication**  
- **xUnit, Moq, FluentAssertions** para pruebas unitarias  

---

## 📑 Endpoints principales

### 🔹 Author
| Método | Ruta | Rol | Descripción |
|--------|------|-----|-------------|
| POST   | `/api/Author` | Admin | Crear autor |
| GET    | `/api/Author` | User | Listar autores |
| GET    | `/api/Author/{id}` | Admin/User | Obtener autor por id |
| GET    | `/api/Author/with-book` | Admin/User | Listar autores con libros |
| PUT    | `/api/Author/{id}` | Admin | Actualizar autor |
| DELETE | `/api/Author/{id}` | Admin | Eliminar autor |

### 🔹 Book
| Método | Ruta | Rol | Descripción |
|--------|------|-----|-------------|
| POST   | `/api/Book` | Admin | Crear libro |
| GET    | `/api/Book` | Admin/User | Listar libros |
| GET    | `/api/Book/{id}` | Admin/User | Obtener libro por id |
| GET    | `/api/Book/before-year-2000` | Público | Libros antes del 2000 |
| GET    | `/api/Book/with-authors-loans` | Admin/User | Libros con préstamos |
| PUT    | `/api/Book/{id}` | Admin | Actualizar libro |
| DELETE | `/api/Book/{id}` | Admin | Eliminar libro |

### 🔹 Loan
| Método | Ruta | Rol | Descripción |
|--------|------|-----|-------------|
| POST   | `/api/Loan` | Admin | Crear préstamo |
| GET    | `/api/Loan` | Admin/User | Listar préstamos |
| GET    | `/api/Loan/{id}` | Admin/User | Obtener préstamo por id |
| GET    | `/api/Loan/not-returned` | Admin | Préstamos no devueltos |
| GET    | `/api/Loan/with-books` | Admin/User | Préstamos con libro |
| PUT    | `/api/Loan/{id}` | Admin/User | Actualizar préstamo |
| PUT    | `/api/Loan/return` | Admin/User | Devolver libro |
| DELETE | `/api/Loan/{id}` | Admin | Eliminar préstamo |

### 🔹 Auth
| Método | Ruta | Rol | Descripción |
|--------|------|-----|-------------|
| POST   | `/api/Auth/login` | Público | Generar token JWT |

---

## ⚠️ Manejo de errores
- **400 BadRequest** → Datos inválidos.  
- **404 NotFound** → Recurso no encontrado.  
- **403 Forbidden** → Acceso denegado (rol insuficiente).  
- **500 InternalServerError** → Error inesperado (middleware global).  

Logs con `ILogger<T>` para:
- Validaciones fallidas.  
- Reglas de negocio incumplidas.  
- Excepciones inesperadas.  

---

## 🚀 Pruebas Unitarias
Frameworks: **xUnit + Moq + FluentAssertions**  
- `GET /books/before-year-2000`  
- `POST /books`  
Cobertura de casos de éxito, fallos y validaciones.

---

## ▶️ Ejecución
1. Configurar `appsettings.json` con tu cadena de conexión y JWT.  
2. Ejecutar migraciones:
   ```bash
   dotnet ef database update --project PruebatecnicaCRUD.Infrastructure.Persistence --startup-project PruebaTecnicaWebAPP
   ```
3. Levantar la API:
   ```bash
   dotnet run --project PruebaTecnicaWebAPP
   ```
4. Abrir Swagger en:
   ```
   https://localhost:7281/swagger
   ```

---

## ✨ Autor
Desarrollado por **Israel Vargas** como prueba técnica. 🚀
