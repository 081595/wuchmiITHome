# wuchmiITHome Constitution
<!--
Sync Impact Report:
- Version: 1.0.0 (Initial Constitution)
- Ratification Date: 2026-02-11
- Template placeholders filled: PROJECT_NAME, all 5 PRINCIPLE sections, GOVERNANCE sections
- Target Project: Personal IT Blog/Learning Management System (ASP.NET Core Razor Pages)
- Templates Status:
  ✅ constitution-template.md (base)
  ⚠️  spec-template.md (should reference principles in requirements validation)
  ⚠️  plan-template.md (should include constitution check in architecture review)
  ⚠️  tasks-template.md (should reference testing requirements from Principle III)
-->

A personal learning and IT article management system built with ASP.NET Core Razor Pages. This constitution defines the non-negotiable principles guiding feature development, code quality, and architectural decisions.

---

## Core Principles

### I. Simplicity First (KISS Principle)
**Every feature must justify its complexity.**

- Start with the simplest solution that solves the problem
- Avoid premature optimization and over-engineering
- Prefer built-in ASP.NET Core features over custom abstractions
- New dependencies require explicit justification in planning phase
- If a feature can be implemented without a package, do so first

**Rationale**: As a personal learning project, maintainability and clarity are more valuable than architectural sophistication. Complex solutions make learning harder and maintenance costly.

### II. Model-Driven Design
**Data models define feature boundaries.**

- Every feature starts with defining or extending Entity Framework models
- Model validation rules must be explicit (data annotations or fluent API)
- Database schema changes require migration scripts
- Models must be documented with XML comments for key properties
- Business logic resides in service classes, not in Razor Pages code-behind

**Rationale**: Clear data models provide a stable foundation for UI and business logic. This aligns with ASP.NET Core MVC/Razor Pages architecture patterns.

### III. Test Critical Paths
**Core CRUD operations and business logic require automated tests.**

- **MUST TEST**: Create, Read, Update, Delete operations for all entities
- **MUST TEST**: Custom business logic and data validation
- **MUST TEST**: Database context configuration and migrations
- **SHOULD TEST**: Razor Page models for complex scenarios
- **OPTIONAL**: UI integration tests (manual testing acceptable for personal projects)

**Testing Workflow**:
1. Write test cases during planning phase
2. Implement feature with test-driven mindset
3. Verify test coverage before marking feature complete

**Rationale**: Automated testing prevents regressions and documents expected behavior, critical for long-term learning projects.

### IV. Convention Over Configuration
**Follow ASP.NET Core Razor Pages conventions rigorously.**

- Razor Pages follow folder-based routing (e.g., `/Pages/Articles/Index.cshtml` → `/Articles`)
- Page models use standard naming (`Index.cshtml.cs` contains `IndexModel`)
- Dependency injection via constructor injection (never service locator pattern)
- Configuration in `appsettings.json` with environment overrides
- Respect MVC pattern: Models for data, Pages for UI, Services for logic

**Rationale**: Conventions reduce cognitive load, improve project navigability, and align with .NET ecosystem best practices.

### V. Progressive Enhancement
**Build incrementally; optimize later.**

- Start with server-side rendering (Razor Pages) before adding JavaScript
- Add client-side validation only after server-side validation works
- Database indexes added when performance issues are measured, not anticipated
- Logging and monitoring added when needed, not preemptively
- Feature flags or sophisticated deployment strategies only when actually required

**Development Sequence**:
1. Core functionality (data model + CRUD)
2. User experience polish (validation, error handling)
3. Performance optimization (caching, indexing)
4. Advanced features (search, filtering, pagination)

**Rationale**: Early optimization wastes time on problems that may never occur. Incremental development enables faster learning cycles.

---

## Technology Standards

### Mandatory Stack
- **Framework**: ASP.NET Core 6.0+ (Razor Pages)
- **ORM**: Entity Framework Core
- **Database**: SQL Server (LocalDB for development, Azure SQL/SQL Server for production)
- **Testing**: xUnit or NUnit for unit tests
- **Dependency Management**: NuGet packages only

### Code Quality Requirements
- C# language features: Use modern C# syntax (nullable reference types, pattern matching, etc.)
- Code formatting: Follow standard .editorconfig conventions
- Security: No hardcoded connection strings or secrets (use User Secrets or environment variables)
- Exception handling: All controller actions must handle exceptions gracefully

### Performance Baselines
- Page load time: < 2 seconds for standard CRUD pages (measured with browser DevTools)
- Database queries: Use `.AsNoTracking()` for read-only operations
- N+1 query prevention: Eager loading required for related data (`.Include()`)

---

## Development Workflow

### Feature Development Process
1. **Specify**: Create `spec.md` with requirements and user stories
2. **Plan**: Create `plan.md` with data model, architecture, and dependencies
3. **Tasks**: Break down into `tasks.md` with explicit test requirements
4. **Analyze**: Run `/speckit.analyze` to verify consistency with constitution
5. **Implement**: Build feature incrementally, commit frequently
6. **Review**: Manual testing + run test suite before merging

### Branch Strategy
- Feature branches: `NNN-feature-name` (e.g., `001-add-comments`, `002-tag-system`)
- Develop branch: Integration branch for completed features
- Master branch: Production-ready code (merge from develop when stable)

### Code Review Checklist (Self-Review)
- [ ] Data model changes include migrations
- [ ] Critical paths have unit tests
- [ ] No hardcoded secrets or connection strings
- [ ] Error handling implemented
- [ ] Code follows ASP.NET Core conventions
- [ ] Feature aligns with simplicity principle (no over-engineering)

---

## Governance

### Amendment Process
This constitution is a living document. Amendments follow this process:

1. **Propose**: Document the amendment with rationale (create issue or discussion)
2. **Version Bump**: 
   - **MAJOR** (X.0.0): Removing or fundamentally changing core principles
   - **MINOR** (1.X.0): Adding new principles or sections
   - **PATCH** (1.0.X): Clarifications, wording improvements, non-semantic changes
3. **Update**: Modify this file with version increment and amendment date
4. **Propagate**: Update dependent templates (`spec-template.md`, `plan-template.md`, `tasks-template.md`) to reflect changes
5. **Document**: Add sync impact report as HTML comment at the top of this file

### Compliance Verification
- `/speckit.analyze` command automatically checks feature specs/plans/tasks against principles
- Violations must be either:
  - **Fixed**: Adjust feature to comply
  - **Justified**: Document explicit exception with rationale in `spec.md`
  - **Escalated**: If principle is wrong, amend constitution first

### Breaking the Rules
Sometimes principles must be violated for good reasons. Document exceptions:

```markdown
## Constitution Exceptions
- **Principle III Violation**: Skipping tests for proof-of-concept spike
  - Rationale: Exploring unfamiliar API, will rewrite with tests
  - Remediation: Feature flagged; must add tests before production deployment
```

### Related Documentation
- Feature specifications: `.specify/features/NNN-feature-name/spec.md`
- Implementation plans: `.specify/features/NNN-feature-name/plan.md`
- Task breakdowns: `.specify/features/NNN-feature-name/tasks.md`

---

**Version**: 1.0.0 | **Ratified**: 2026-02-11 | **Last Amended**: 2026-02-11
