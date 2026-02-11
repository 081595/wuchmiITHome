# Research: Teach Appo Employee CRUD

**Date**: 2026-02-11

## Composite Key CRUD (EF Core + Razor Pages)

- **Decision**: Configure composite primary key with `HasKey(e => new { e.Yr, e.IdNo, e.Birthday })` and use key parts in Razor Pages routes.
- **Rationale**: EF Core requires explicit composite key configuration; routing needs all key parts for details/edit/delete.
- **Alternatives considered**: Surrogate key only (`seq_no`) for routing. Rejected because the database primary key is composite and must remain authoritative.

## Navigation Link Placement

- **Decision**: Add a single navigation link to the maintenance list page in the shared layout using `asp-page` tag helpers.
- **Rationale**: Shared layout keeps navigation consistent and avoids hard-coded URLs.
- **Alternatives considered**: Per-page local links or multiple nav links. Rejected to reduce clutter and keep the main navigation stable.

## Timestamp Handling (`create_date`, `update_date`)

- **Decision**: Treat timestamps as server-managed and read-only in UI; set values on add/modify in the data layer and do not bind from forms.
- **Rationale**: Prevents users from altering system audit fields and keeps timestamps consistent with backend writes.
- **Alternatives considered**: Manual entry in forms. Rejected because it risks inconsistent audit data.
