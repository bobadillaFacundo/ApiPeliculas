![image](https://github.com/user-attachments/assets/f22b1181-65fd-4723-ae17-5a98d15cc5b5)

# 🎬 API de Películas – ASP.NET Core 7

![Banner](banner.png)

API RESTful para la gestión de películas, categorías y usuarios, construida con **ASP.NET Core 7**, **Entity Framework Core** y **SQL Server**.

---

## 🚀 Stack tecnológico

| Tecnología              | Uso                                                    |
|-------------------------|--------------------------------------------------------|
| **C# / ASP.NET Core 7** | Framework web y controlador de dependencias            |
| **Entity Framework Core** | ORM para SQL Server                                   |
| **SQL Server**          | Base de datos relacional                               |
| **AutoMapper**          | Mapeo entre entidades y DTOs                           |
| **JWT Bearer**          | Autenticación y autorización                           |
| **Swagger / OpenAPI**   | Documentación interactiva de la API                    |

---

## 📚 Características

- CRUD completo para **Películas** y **Categorías**  
- Registro y login de **Usuarios** con emisión de **JWT**  
- Patrón **Repositorio** y uso de **DTOs**  
- Middleware de CORS y validación global de modelos  
- Documentación automática con Swagger (`/swagger`)

---

## ⚙️ Instalación rápida

```bash
git clone https://github.com/bobadillaFacundo/ApiPeliculas.git
cd ApiPeliculas
dotnet restore
```

1. Configura tu `appsettings.json` o variables de entorno:

```json
"ConnectionStrings": {
  "ConexionSql": "Server=.;Database=ApiPeliculas;Trusted_Connection=True;TrustServerCertificate=True"
},
"AppiSettings": {
  "key": "TU_CLAVE_SUPER_SECRETA_DE_32+_CARACTERES"
}
```

2. Aplica las migraciones y levanta la API:

```bash
dotnet ef database update      # crea la BD
dotnet run --project ApiPeliculas/ApiPeliculas.csproj
```

La API quedará disponible en **https://localhost:5001** (HTTPS) y **http://localhost:5000** (HTTP).

---

## 🛠 Endpoints principales

| Método | Ruta                       | Descripción                       | Autenticación |
|--------|--------------------------- |-----------------------------------|---------------|
| POST   | `/api/usuarios/CrearUsuario` | Registro de usuario               | 🔓            |
| POST   | `/api/usuarios/Login`        | Login y generación de token JWT   | 🔓            |
| GET    | `/api/categorias`            | Listar categorías                 | 🔒            |
| GET    | `/api/categorias/{id}`       | Detalle de categoría              | 🔒            |
| POST   | `/api/categorias`            | Crear categoría                   | 🔒            |
| PUT    | `/api/categorias/{id}`       | Reemplazar categoría              | 🔒            |
| PATCH  | `/api/categorias/{id}`       | Modificar nombre                  | 🔒            |
| DELETE | `/api/categorias/{id}`       | Eliminar categoría                | 🔒            |
| GET    | `/api/peliculas`             | Listar películas                  | 🔒            |
| ...    | ...                          | (Más en Swagger)                  | 🔒            |

> 🔒 Los endpoints protegidos requieren el encabezado  
> `Authorization: Bearer <tu-token>` que obtienes en el login.

---

## 🐳 Docker (opcional)

```bash
docker build -t apipeliculas .
docker run -e "ConnectionStrings__ConexionSql=Server=sql;Database=ApiPeliculas;User=sa;Password=Pass123!"            -e "AppiSettings__key=TU_CLAVE_SUPER_SECRETA"            -p 80:8080 apipeliculas
```

---

## 👨‍💻 Autor

**Facundo Bobadilla** – [GitHub](https://github.com/bobadillaFacundo)

¡Contribuciones, issues y estrellas son bienvenidas! ✨
