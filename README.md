# Esercizio_documentazione_AI

## Scopo del progetto

`Esercizio_documentazione_AI` è un progetto ASP.NET Core Web API sviluppato come esercizio di documentazione tecnica.

Il progetto contiene un back-end denominato `UserCrudApp`, il cui scopo è esporre un modulo CRUD per la gestione di utenti tramite API HTTP.

Il modulo consente di:

- recuperare l’elenco degli utenti;
- recuperare un singolo utente tramite identificativo;
- creare un nuovo utente;
- aggiornare un utente esistente;
- eliminare un utente.

## Descrizione funzionale sintetica

L’applicazione è implementata come Minimal API ASP.NET Core.

La risorsa principale gestita dal sistema è `User`, composta dai seguenti campi:

- `Id`: identificativo numerico dell’utente;
- `Name`: nome dell’utente;
- `Email`: indirizzo email dell’utente.

La persistenza dei dati è gestita tramite Entity Framework Core con provider InMemory. Il database configurato nel codice si chiama `UsersDb`.

> Nota: usando il provider InMemory, i dati sono gestiti in memoria e non risultano persistenti su un database esterno. Al riavvio dell’applicazione i dati possono essere persi.

## Endpoint disponibili

| Metodo HTTP | Endpoint | Descrizione |
|---|---|---|
| `GET` | `/users` | Restituisce l’elenco degli utenti |
| `GET` | `/users/{id}` | Restituisce un utente in base all’identificativo |
| `POST` | `/users` | Crea un nuovo utente |
| `PUT` | `/users/{id}` | Aggiorna un utente esistente |
| `DELETE` | `/users/{id}` | Elimina un utente esistente |

### Esempio di payload per creazione utente

```json
{
  "name": "Mario Rossi",
  "email": "mario.rossi@example.com"
}
```

### Esempio di payload per aggiornamento utente

```json
{
  "name": "Mario Bianchi",
  "email": "mario.bianchi@example.com"
}
```

## Tecnologie principali

Il progetto utilizza le seguenti tecnologie:

- C#;
- ASP.NET Core Web API;
- .NET 10;
- Minimal API;
- Entity Framework Core;
- Entity Framework Core InMemory;
- Swagger / Swashbuckle per la documentazione interattiva delle API in ambiente di sviluppo.

Dipendenze principali dichiarate nel file `UserCrudApp.csproj`:

- `Microsoft.AspNetCore.OpenApi`;
- `Microsoft.EntityFrameworkCore.InMemory`;
- `Swashbuckle.AspNetCore`.

## Requisiti di installazione

Per compilare ed eseguire il progetto sono necessari:

- .NET SDK 10.0 o superiore compatibile con `net10.0`;
- Git, per clonare il repository;
- un editor o IDE compatibile con progetti ASP.NET Core, ad esempio Visual Studio, Visual Studio Code o Rider.

Da completare:

- versione minima esatta dell’IDE consigliato;
- eventuali prerequisiti specifici dell’ambiente di sviluppo locale.

## Configurazione dell’ambiente

Clonare il repository:

```bash
git clone https://github.com/cosmoccp/Esercizio_documentazione_AI.git
```

Entrare nella cartella del repository:

```bash
cd Esercizio_documentazione_AI
```

Ripristinare le dipendenze NuGet:

```bash
dotnet restore UserCrudApp/UserCrudApp.csproj
```

L’applicazione usa le configurazioni standard ASP.NET Core presenti nei file:

```text
UserCrudApp/appsettings.json
UserCrudApp/appsettings.Development.json
UserCrudApp/Properties/launchSettings.json
```

Nel profilo di sviluppo è configurata la variabile:

```text
ASPNETCORE_ENVIRONMENT=Development
```

In ambiente `Development` l’applicazione abilita Swagger e Swagger UI.

URL configurati nei profili di avvio:

```text
HTTP:  http://localhost:5183
HTTPS: https://localhost:7132
```

Swagger UI è disponibile, in ambiente di sviluppo, all’indirizzo:

```text
http://localhost:5183/swagger
```

oppure:

```text
https://localhost:7132/swagger
```

## Compilazione del progetto

Per compilare il progetto:

```bash
dotnet build UserCrudApp/UserCrudApp.csproj
```

Da completare:

- eventuale configurazione di build per ambienti diversi da `Development`;
- eventuale pipeline CI/CD, se prevista.

## Esecuzione dei test

Nel repository non risulta presente un progetto di test automatico dedicato.

Da completare:

- aggiungere un progetto di test, ad esempio con xUnit, NUnit o MSTest;
- definire test unitari e/o test di integrazione per gli endpoint CRUD;
- documentare il comando effettivo di esecuzione dei test dopo l’aggiunta del progetto di test.

Comando standard da utilizzare quando sarà presente almeno un progetto di test:

```bash
dotnet test
```

## Avvio dell’applicazione

Per avviare l’applicazione:

```bash
dotnet run --project UserCrudApp/UserCrudApp.csproj
```

Dopo l’avvio, l’API sarà disponibile sugli URL configurati dal profilo di lancio.

Esempio:

```text
http://localhost:5183
```

In ambiente di sviluppo è possibile accedere alla documentazione Swagger:

```text
http://localhost:5183/swagger
```

## Struttura delle cartelle principali

```text
Esercizio_documentazione_AI/
├── CLAUDE.md
├── README.md
├── UserCrudApp.slnx
└── UserCrudApp/
    ├── Data/
    │   └── UserDbContext.cs
    ├── Models/
    │   ├── User.cs
    │   └── UserDtos.cs
    ├── Properties/
    │   └── launchSettings.json
    ├── Program.cs
    ├── UserCrudApp.csproj
    ├── UserCrudApp.http
    ├── appsettings.Development.json
    └── appsettings.json
```

### Descrizione delle cartelle e dei file principali

| Percorso | Descrizione |
|---|---|
| `UserCrudApp/Program.cs` | Punto di ingresso dell’applicazione. Configura servizi, Swagger, database InMemory ed endpoint CRUD |
| `UserCrudApp/Data/UserDbContext.cs` | Contesto Entity Framework Core con `DbSet<User>` |
| `UserCrudApp/Models/User.cs` | Modello dati dell’utente |
| `UserCrudApp/Models/UserDtos.cs` | Record usati come payload per creazione e aggiornamento utente |
| `UserCrudApp/UserCrudApp.csproj` | File di progetto .NET con target framework e dipendenze NuGet |
| `UserCrudApp/Properties/launchSettings.json` | Profili di avvio locali per HTTP, HTTPS e IIS Express |
| `UserCrudApp/appsettings.json` | Configurazione generale dell’applicazione |
| `UserCrudApp/appsettings.Development.json` | Configurazione specifica per ambiente di sviluppo |
| `UserCrudApp/UserCrudApp.http` | File per test manuali HTTP. Da verificare perché contiene un endpoint `/weatherforecast/` non coerente con gli endpoint CRUD presenti nel codice |
| `CLAUDE.md` | Documento sintetico con note sul progetto, comandi utili ed endpoint disponibili |

## Note su qualità, test e manutenzione

### Stato attuale

Il progetto implementa un modulo CRUD semplice per la gestione utenti tramite Minimal API.

Sono presenti:

- separazione minima tra modello dati, DTO e contesto dati;
- Swagger in ambiente di sviluppo;
- Entity Framework Core InMemory per esecuzione locale rapida;
- endpoint CRUD definiti direttamente in `Program.cs`.

### Aspetti da completare o verificare

- Non risultano test automatici nel repository.
- Non risultano validazioni esplicite sui dati ricevuti in input.
- Non risultano meccanismi di autenticazione o autorizzazione.
- Non risulta configurato un database persistente.
- Non risultano migrazioni Entity Framework Core.
- Il file `UserCrudApp.http` contiene un riferimento a `/weatherforecast/`, ma nel codice non risulta definito un endpoint corrispondente.
- Il file `UserCrudApp.slnx` risulta presente, ma la compilazione documentata usa direttamente il file `.csproj`.

### Indicazioni di manutenzione

Per mantenere il progetto documentato e verificabile nel tempo, si consiglia di aggiornare questa documentazione quando vengono aggiunti:

- nuovi endpoint;
- nuove proprietà del modello `User`;
- validazioni sui DTO;
- persistenza su database reale;
- autenticazione o autorizzazione;
- test automatici;
- pipeline di build o rilascio.

## Informazioni da completare

Le seguenti informazioni non sono deducibili dal codice attualmente presente nel repository:

- ambiente di deploy previsto;
- database di produzione previsto;
- strategia di autenticazione;
- strategia di validazione degli input;
- convenzioni di logging oltre alla configurazione standard;
- strategia di test;
- pipeline CI/CD;
- regole di versionamento;
- licenza del progetto.
