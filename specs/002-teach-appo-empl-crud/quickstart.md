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

## Run Tests

### Unit & Integration Tests

Run all tests in the test project:

```bash
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj
```

Run specific test suites:

```bash
# Database schema tests
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj --filter "DbContextTests"

# User Story 1 - List and Details pages
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj --filter "ListPageTests OR DetailsPageTests"

# User Story 2 - Create page
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj --filter "CreatePageTests"

# User Story 3 - Edit and Delete pages
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj --filter "EditPageTests OR DeletePageTests"
```

### Manual UI Testing

1. Navigate to the main list: `/TeachAppoEmployees`
2. Test browsing: Click "Details" on any seeded record
3. Test creating: Click "Create New" and fill in the form with a unique composite key (Yr, IdNo, Birthday)
4. Test editing: Click "Edit" on a record and modify employee information
5. Test deleting: Click "Delete" and confirm the action
6. Verify timestamps are displayed correctly on detail page

### Validation Testing

- **Duplicate Key**: Try creating a record with existing Year + IdNo + Birthday combination → expect error
- **Email Format**: Try creating with invalid email format → expect validation error
- **Date Format**: Try providing invalid date → expect date validation error
- **Required Fields**: Try submitting with missing required fields → expect validation errors
- **Already Deleted**: Try deleting the same record twice → expect "record not found" error

## Notes

- The app runs `dbContext.Database.Migrate()` on startup.
- `create_date`, `update_date`, and `seq_no` are system-managed and read-only in the UI.
- The composite primary key (Yr, IdNo, Birthday) is immutable after creation.
- Seeded test data includes 3 sample employees for quick testing.

## Troubleshooting

### Tests Fail to Connect to Database

Ensure SQLite is properly installed and the in-memory database is being created:

```bash
# Run a single test in verbose mode
dotnet test Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj --logger "console;verbosity=detailed" --filter "Database_CanCreateAndQueryTeachAppoEmployees"
```

### Migration Not Applied

If you encounter "no such table" errors, ensure the database is initialized:

```bash
dotnet ef database update --project wuchmiITHome.csproj
```

### Port Already in Use

If port 5001 is already in use, specify a different port:

```bash
dotnet run --project wuchmiITHome.csproj -- --urls "https://localhost:7001"
```
