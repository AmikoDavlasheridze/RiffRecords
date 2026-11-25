# RiffRecords

An online vinyl store for heavy and alternative records. A static HTML/CSS/JS storefront talks to an ASP.NET Core Web API built with Clean Architecture.

## Stack

| Part | Tech |
|---|---|
| API | .NET 10, ASP.NET Core Web API, EF Core, SQL Server |
| Auth | JWT bearer tokens, `Admin` role, ASP.NET Core Identity password hasher (PBKDF2) |
| Frontend | Vanilla HTML, CSS and JavaScript (no build step) |
| Dev setup | Docker Compose: SQL Server 2022 + API + nginx for the frontend |

## Repository layout

```
RiffRecords/
├── RiffRecordsBackend/
│   ├── src/
│   │   ├── RiffRecords.Domain/          entities and business rules (Band, Vinyl, Track, User, ...)
│   │   ├── RiffRecords.Application/     services, DTOs, repository interfaces
│   │   ├── RiffRecords.Infrastructure/  EF Core, repositories, migrations, catalog seed
│   │   └── RiffRecords.Api/             controllers, JWT, Program.cs, RiffRecords.Api.http
│   ├── Dockerfile
│   ├── docker-compose.yml
│   └── .env.example
└── RiffRecordsUI/                       storefront (index, shop, band details, band bio, cart, checkout)
```

Dependencies point inward: Domain has no dependencies, Application depends on Domain, Infrastructure implements Application's interfaces, and the API wires everything together. Repositories only stage changes; `IUnitOfWork.SaveChangesAsync` is the single commit point.

## Running with Docker (recommended)

From `RiffRecordsBackend/`:

```bash
cp .env.example .env      # then fill in the values
docker compose up -d --build
```

`.env` is gitignored and needs these values:

| Variable | What it is |
|---|---|
| `MSSQL_SA_PASSWORD` | SQL Server `sa` password (8+ chars, 3 of upper/lower/digit/symbol, no `;`) |
| `JWT_SIGNING_KEY` | Base64 of 32 random bytes. PowerShell: `[Convert]::ToBase64String((1..32 \| % { Get-Random -Max 256 }))` |
| `ADMIN_EMAIL`, `ADMIN_USERNAME`, `ADMIN_PASSWORD` | The admin account created on startup |

On startup the API applies migrations, seeds the catalog (6 bands, 18 albums with full tracklists) and creates the admin user if it doesn't exist.

| Service | URL |
|---|---|
| Storefront | http://localhost:5500 |
| API | http://localhost:5298 |
| Swagger UI | http://localhost:5298/swagger |

To start over with a fresh database: `docker compose down -v`, then `docker compose up -d --build`.

## Running locally without Docker

The default connection string uses LocalDB (`(localdb)\MSSQLLocalDB`, database `RiffRecordsDB`). Secrets go in user-secrets, never in `appsettings.json`:

```bash
cd RiffRecordsBackend/src/RiffRecords.Api
dotnet user-secrets set "Jwt:SigningKey" "<base64 32 bytes>"
dotnet user-secrets set "AdminSeed:Email" "<admin email>"
dotnet user-secrets set "AdminSeed:Username" "<admin username>"
dotnet user-secrets set "AdminSeed:Password" "<admin password>"
```

Create the database and run the API:

```bash
cd RiffRecordsBackend
dotnet ef database update --project src/RiffRecords.Infrastructure --startup-project src/RiffRecords.Api
dotnet run --project src/RiffRecords.Api --launch-profile http
```

The catalog is seeded automatically in Development. Serve `RiffRecordsUI/` on port 5500 (for example with VS Code Live Server); CORS allows `http://localhost:5500` and `http://127.0.0.1:5500`. The API address the frontend uses is set in `RiffRecordsUI/js/config.js`.

`RiffRecords.Api.http` contains requests for every endpoint. It reads the admin credentials from user-secrets through `http-client.env.json`, so pick the `dev` environment in Visual Studio.

## API

| Method | Route | Access |
|---|---|---|
| GET | `/api/band` | Public |
| GET | `/api/band/{id}` | Public (bio, achievements, vinyls with tracklists) |
| POST | `/api/band` | Admin |
| PUT | `/api/band/{id}` | Admin |
| DELETE | `/api/band/{id}` | Admin |
| GET | `/api/vinyls` | Public |
| GET | `/api/vinyls/{id}` | Public |
| POST | `/api/vinyls` | Admin |
| POST | `/api/vinyls/{id}/increase-stock` | Admin |
| POST | `/api/user/register` | Public |
| POST | `/api/user/login` | Public (returns a JWT) |
| GET | `/api/user/me` | Logged in |
| GET | `/api/wishlist` | Logged in |
| POST | `/api/wishlist` | Logged in |
| DELETE | `/api/wishlist/{id}` | Logged in |

Send the token as `Authorization: Bearer <token>`. Duplicates (an email or username already registered, an existing band name, an already wishlisted vinyl) return `409 Conflict`.

## Storefront

Home shows featured bands. Shop lists every band with its starting price. The band page shows the band's vinyls; clicking a vinyl opens an album section with label, runtime, description and the full tracklist, with the album's most famous track highlighted. The band bio page has the history, achievements, discography and famous tracks. The cart lives in `localStorage`.

## Roadmap

1. ~~Vinyl details: tracklists, label, runtime, famous track~~ ✅
2. Orders and checkout: login required, pay on delivery, shipping details at checkout, status Placed → InTransit → Delivered (or Cancelled), stock protected against overselling
3. Route cleanup: plural routes, `{id:int}` constraints everywhere
4. Admin panel as a separate app: orders, catalog editing, users
