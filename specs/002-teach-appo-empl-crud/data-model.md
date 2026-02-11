# Data Model: Teach Appo Employee CRUD

## Entity: TeachAppoEmployee

Represents a single record in `teach_appo_empl_base`.

### Fields

- `Yr` (int, required): Academic year; part of composite primary key.
- `IdNo` (string, required, max 60): Identity number; part of composite primary key.
- `Birthday` (DateTime, required): Birth date; part of composite primary key.
- `EmplNo` (string, required, length 6): Employee number.
- `ChName` (string, required, max 60): Chinese name.
- `EnName` (string, required, max 60): English name.
- `Email` (string, required, max 60): Email address.
- `RefreshToken` (string, required, max 60): Refresh token value.
- `RefreshTokenExpired` (DateTime?, optional): Token expiration time.
- `SeqNo` (int, identity, required): Database identity for internal ordering.
- `CreateDate` (DateTime, required): Server-managed creation timestamp.
- `UpdateDate` (DateTime, required): Server-managed update timestamp.

### Keys and Indexes

- Primary key: composite (`Yr`, `IdNo`, `Birthday`).
- `SeqNo` is identity but not used as the primary key.

### Validation Rules

- Required: `Yr`, `IdNo`, `Birthday`, `EmplNo`, `ChName`, `EnName`, `Email`, `RefreshToken`.
- Max length: `IdNo`, `ChName`, `EnName`, `Email`, `RefreshToken` = 60; `EmplNo` = 6.
- `RefreshTokenExpired` is optional.
- Primary key fields are immutable after creation.

### Relationships

- None defined in the current schema.

### State Transitions

- Create: system sets `CreateDate` and `UpdateDate` on insert.
- Update: system refreshes `UpdateDate` on update.
- Delete: record removed.
