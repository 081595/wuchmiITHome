# Quickstart: Teach Appo Employee CRUD

## Prerequisites

- .NET SDK 8
- SQLite (via EF Core provider)

## Run Locally

1. Restore and run the app:

```bash
dotnet restore
dotnet run --project /workspaces/wuchmiITHome/wuchmiITHome.csproj
```

2. Open the CRUD list page in a browser:

- `https://localhost:5001/TeachAppoEmployees`

## Notes

- The app runs `dbContext.Database.Migrate()` on startup.
- `create_date`, `update_date`, and `seq_no` are system-managed and read-only in the UI.
