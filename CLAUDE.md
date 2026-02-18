# CLAUDE.md - gesn.v1

## Project Overview

ASP.NET Core 8.0 MVC web application for business management (offers, sales, production, HR, financial).
Uses SQLite with Dapper ORM, FluentMigrator for migrations, and Razor views.

**Language:** C# (.NET 8.0) | **Default locale:** pt-BR | **Comments/messages:** Portuguese

## Commands

```bash
dotnet build gesn.webApp/gesn.webApp.csproj    # Build
dotnet run --project gesn.webApp                # Run (dev)
dotnet publish gesn.webApp -c Release           # Publish
```

Migrations run automatically on startup via `DbInit.Initialize()` in `Program.cs`.
Database: `gesn.webApp/Data/gesn.db` (SQLite, auto-created on first run).

## Architecture

Layered architecture inside a single project (`gesn.webApp`):

```
gesn.webApp/
├── Controllers/                    # MVC controllers
├── Areas/Admin/                    # Admin area (users, roles, claims)
├── Areas/Identity/                 # Authentication (ASP.NET Identity + Dapper)
├── Models/
│   ├── Entities/Base/Entity.cs     # Base entity (all entities inherit this)
│   ├── Entities/{Domain}/          # Domain entities (Global, Offer, Sales, Production, etc.)
│   ├── ViewModels/{Domain}/        # ViewModels per domain
│   └── Enums/                      # Enumerations
├── Interfaces/
│   ├── Repositories/Base/IRepositoryBase.cs
│   ├── Repositories/{Domain}/      # Repository interfaces
│   └── Services/{Domain}/          # Service interfaces
├── Infrastructure/
│   ├── Configuration/DependecyInjection.cs  # DI registration (note: typo in filename)
│   ├── FluentValidation/           # Validator registration
│   ├── Mappers/MappingConfiguration.cs  # Mapster setup (assembly scan)
│   ├── Mappings/                   # Mapster mapping configs (IRegister)
│   ├── Repositories/Base/RepositoryBase.cs  # Generic Dapper repository
│   ├── Repositories/Templates/     # QueryTemplate for dynamic SQL
│   ├── Services/{Domain}/          # Service implementations
│   └── Middleware/                  # Custom middleware (login rate limiting)
├── Validators/{Domain}/            # FluentValidation validators
├── Data/
│   ├── Migrations/                 # FluentMigrator migration classes
│   └── Seeds/                      # Seed data
├── Views/                          # Razor views (.cshtml)
├── Resources/                      # Localization resources
└── wwwroot/                        # Static files (CSS, JS, client libs via LibMan)
```

## Key Patterns

### Entity Base Class (`Models/Entities/Base/Entity.cs`)
All entities inherit from `Entity` with common properties:
- `Id` (string, Guid) | `Name` | `Description`
- `CreatedAt`, `CreatedBy`, `LastModifiedAt`, `LastModifiedBy`
- `StateCode` (EObjectState: ACTIVE/INACTIVE) - used for **soft delete**
- Methods: `IsActive()`, `Activate()`, `Deactivate()`, `UpdateModification()`

### Repository Pattern (`Infrastructure/Repositories/Base/RepositoryBase.cs`)
- Generic `RepositoryBase<T>` with Dapper: `AddAsync`, `GetAsync`, `ReadAsync`, `UpdateAsync`, `DeleteAsync`
- `DeleteAsync` performs soft delete (sets `StateCode = INACTIVE`)
- `ReadAsync` uses `QueryTemplate` for dynamic SQL (joins, where, order by, group by)
- Table name derived from entity class name: `typeof(T).Name`

### ViewModel Convention
Each entity has up to 5 ViewModels:
- `{Entity}BaseViewModel` - shared properties
- `{Entity}InsertViewModel` - for creation
- `{Entity}UpdateViewModel` - for editing
- `{Entity}SummaryViewModel` - for list views
- `{Entity}DetailsViewModel` - for detail views

### Service Layer
Services accept/return ViewModels and use Mapster for entity mapping:
- Interface: `I{Entity}Services` or `I{Entity}Service`
- Implementation: `{Entity}Services`
- Uses `IMapper` (Mapster) and the entity's repository

### Mapping (Mapster)
- Config classes implement `IRegister` in `Infrastructure/Mappings/`
- Naming: `{Entity}MappingConfig.cs`
- Auto-registered via assembly scan in `MappingConfiguration.RegisterMaps()`

### Validation (FluentValidation)
- Validators in `Validators/{Domain}/`
- Naming: `{ViewModel}Validator` (e.g., `CategoryInsertViewModelValidator`)
- Registered in `Infrastructure/FluentValidation/FluentValidationConfiguration.cs`

### Migrations (FluentMigrator)
- Located in `Data/Migrations/`
- Naming: `Migration_{timestamp}.cs`
- Auto-run on startup via `DbInit`
- SQLite does not enforce foreign keys via FluentMigrator

## Adding a New Entity (Checklist)

1. **Entity** - `Models/Entities/{Domain}/{Entity}.cs` (inherit from `Entity`)
2. **ViewModels** - `Models/ViewModels/{Domain}/` (Base, Insert, Update, Summary, Details)
3. **Interface (Repo)** - `Interfaces/Repositories/{Domain}/I{Entity}Repository.cs`
4. **Interface (Service)** - `Interfaces/Services/{Domain}/I{Entity}Service.cs`
5. **Repository** - `Infrastructure/Repositories/{Domain}/{Entity}Repository.cs`
6. **Query Template** - `Infrastructure/Repositories/Templates/{Domain}/{Entity}Template.cs`
7. **Service** - `Infrastructure/Services/{Domain}/{Entity}Services.cs`
8. **Mapping** - `Infrastructure/Mappings/{Entity}MappingConfig.cs` (implement `IRegister`)
9. **Validators** - `Validators/{Domain}/{ViewModel}Validator.cs`
10. **Register validators** in `Infrastructure/FluentValidation/FluentValidationConfiguration.cs`
11. **Register DI** in `Infrastructure/Configuration/DependecyInjection.cs`
12. **Migration** - `Data/Migrations/Migration_{timestamp}.cs`
13. **Controller** - `Controllers/{Entity}Controller.cs`
14. **Views** - `Views/{Entity}/` (Index, Create, Edit, Details, Delete)

## Key Dependencies

| Package | Purpose |
|---------|---------|
| Dapper + Dapper.SqlBuilder | Micro ORM + dynamic SQL |
| Microsoft.Data.Sqlite.Core | SQLite provider |
| FluentMigrator | Database migrations |
| FluentValidation | Input validation |
| Mapster | Object-to-object mapping |
| AspNetCore.Identity.DapperOrm | Identity with Dapper |
| BCrypt.Net-Next | Password hashing |

Client-side libs managed via LibMan (`libman.json`): DataTables, Select2, Toastr, jQuery Mask.

## Git Conventions

- Commit messages in **Portuguese**
- Common prefixes: "Implementação", "Correções", "Ajustes", "Inclusão de"
- Main branch: `master`

## Notes

- No test project exists yet
- Google Workspace integration is configured but mostly commented out
- Several domain modules (Sales, Production) have entities defined but services are commented out in DI
- Authorization uses claim-based policies (e.g., `"usuarios:gerenciar"`, `"clientes:gerenciar"`)
