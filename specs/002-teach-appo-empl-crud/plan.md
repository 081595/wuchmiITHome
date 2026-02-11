# Implementation Plan: Teach Appo Employee CRUD

**Branch**: `002-teach-appo-empl-crud` | **Date**: 2026-02-11 | **Spec**: [specs/002-teach-appo-empl-crud/spec.md](specs/002-teach-appo-empl-crud/spec.md)
**Input**: Feature specification from `/specs/002-teach-appo-empl-crud/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Add a Razor Pages CRUD maintenance area for `teach_appo_empl_base`, including list, details, create, edit, and delete pages, and expose the entry point in the main navigation. Implement using EF Core with a composite primary key and standard Razor Pages conventions.

## Technical Context

**Language/Version**: C# 12 / .NET 8 (ASP.NET Core Razor Pages)  
**Primary Dependencies**: ASP.NET Core Razor Pages, Entity Framework Core 8 (SQLite provider)  
**Storage**: SQLite, schema managed by EF Core migrations  
**Testing**: xUnit (to add CRUD tests per constitution), manual UI verification  
**Target Platform**: Linux server/container, standard ASP.NET Core hosting  
**Project Type**: Web application (single Razor Pages project)  
**Performance Goals**: List page renders within 3 seconds for up to 1,000 records; page load <2 seconds baseline  
**Constraints**: No new dependencies without justification; use EF Core conventions and `AsNoTracking()` for reads  
**Scale/Scope**: Small internal admin surface with limited staff users

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Simplicity First**: Use Razor Pages + EF Core only; no extra dependencies. Pass.
- **Model-Driven Design**: Define EF Core model with explicit validation and migration. Pass.
- **Test Critical Paths**: Add CRUD tests (xUnit) for the new entity. Pass with planned tests.
- **Convention Over Configuration**: Follow Razor Pages folder routing and naming. Pass.
- **Progressive Enhancement**: Server-side CRUD first, no extra JS. Pass.

**Post-Design Re-check**: All gates still pass based on research and design outputs.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```text
Data/
  wuchmiITHomeContext.cs
Migrations/
Models/
Pages/
  Shared/
  Articles/
  TeachAppoEmployees/        # new CRUD pages
Program.cs
Tests/
wwwroot/
```

**Structure Decision**: Single Razor Pages web project using existing `Data/`, `Models/`, and `Pages/` folders, plus a `Tests/` project for xUnit.

## Complexity Tracking

No constitution violations identified.

## Phase 0: Research

Output: [specs/002-teach-appo-empl-crud/research.md](specs/002-teach-appo-empl-crud/research.md)

- Composite key CRUD patterns in EF Core and Razor Pages routing.
- Navigation link best practices for Razor Pages.
- Timestamp handling for `create_date` and `update_date`.

## Phase 1: Design & Contracts

Output:

- Data model: [specs/002-teach-appo-empl-crud/data-model.md](specs/002-teach-appo-empl-crud/data-model.md)
- Contracts: [specs/002-teach-appo-empl-crud/contracts/teach-appo-empl-base.openapi.yaml](specs/002-teach-appo-empl-crud/contracts/teach-appo-empl-base.openapi.yaml)
- Quickstart: [specs/002-teach-appo-empl-crud/quickstart.md](specs/002-teach-appo-empl-crud/quickstart.md)

Design decisions:

- EF Core model with composite key (`yr`, `id_no`, `birthday`).
- Razor Pages CRUD under `Pages/TeachAppoEmployees`.
- Server-managed timestamps and read-only fields in UI.

## Phase 2: Implementation Planning

Output: tasks will be defined in `tasks.md` by `/speckit.tasks`.
