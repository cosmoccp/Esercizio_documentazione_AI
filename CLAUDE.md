# CLAUDE.md

## Panoramica del progetto
- Progetto ASP.NET Core Web API per la gestione CRUD degli utenti.
- Usa Entity Framework Core con database InMemory per la persistenza locale.

## Comandi utili
- Build: dotnet build UserCrudApp/UserCrudApp.csproj
- Esecuzione: dotnet run --project UserCrudApp/UserCrudApp.csproj

## Endpoint disponibili
- GET /users
- GET /users/{id}
- POST /users
- PUT /users/{id}
- DELETE /users/{id}
