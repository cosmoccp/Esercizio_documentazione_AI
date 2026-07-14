# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Panoramica del progetto

Web API ASP.NET Core (Minimal API, .NET 10) per la gestione CRUD di utenti, sviluppata come esercizio di documentazione tecnica. Il codice sorgente vive interamente nella sottocartella `UserCrudApp/`; la root del repo contiene solo `UserCrudApp.slnx`, `README.md` e questo file.

## Comandi

- Build: `dotnet build UserCrudApp/UserCrudApp.csproj`
- Esecuzione: `dotnet run --project UserCrudApp/UserCrudApp.csproj` (in ambiente `Development` espone Swagger UI su `/swagger`)
- Test: `dotnet test UserCrudApp.Tests/UserCrudApp.Tests.csproj` (progetto xUnit con test di integrazione via `WebApplicationFactory` sui 5 endpoint CRUD)

## Architettura

Tutta la logica applicativa è concentrata in [Program.cs](UserCrudApp/Program.cs): non ci sono controller, servizi applicativi o layer di repository separati. Il flusso è:

1. `Program.cs` registra `UserDbContext` con provider **EF Core InMemory** (database `UsersDb`) e definisce i 5 endpoint CRUD direttamente come lambda su `WebApplication` (`MapGet`/`MapPost`/`MapPut`/`MapDelete`).
2. [Data/UserDbContext.cs](UserCrudApp/Data/UserDbContext.cs) espone un solo `DbSet<User>`.
3. [Models/User.cs](UserCrudApp/Models/User.cs) è l'entità persistita (Id, Name, Email); [Models/UserDtos.cs](UserCrudApp/Models/UserDtos.cs) definisce i record `CreateUserRequest`/`UpdateUserRequest` usati come payload delle richieste.

Punti da tenere presenti quando si modifica il codice:

- Il database è **in memoria**: i dati non sopravvivono al riavvio del processo; non ci sono migrazioni EF Core da gestire.
- Non ci sono validazioni sui DTO in ingresso né autenticazione/autorizzazione: qualsiasi aggiunta in questo senso va introdotta ex novo.
- [UserCrudApp.http](UserCrudApp/UserCrudApp.http) contiene ancora la richiesta di scaffolding `/weatherforecast/`, non coerente con gli endpoint CRUD reali: va aggiornato se si usa questo file per test manuali.
