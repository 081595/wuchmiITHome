---

description: "Task list for Teach Appo Employee CRUD"
---

# Tasks: Teach Appo Employee CRUD

**Input**: Design documents from `/specs/002-teach-appo-empl-crud/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: CRUD tests are REQUIRED by the constitution and included below.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create test project in Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj with xUnit and ASP.NET Core test dependencies
- [ ] T002 Add Tests/wuchmiITHome.Tests/wuchmiITHome.Tests.csproj to wuchmiITHome.sln
- [ ] T003 [P] Add SQLite test web app factory in Tests/wuchmiITHome.Tests/TestInfrastructure/SqliteWebApplicationFactory.cs

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

- [ ] T004 Create TeachAppoEmployee model with XML comments and data annotations in Models/TeachAppoEmployee.cs
- [ ] T005 Add TeachAppoEmployee service layer in Services/TeachAppoEmployeeService.cs
- [ ] T006 Update DbContext with DbSet, composite key mapping, and SQLite defaults in Data/wuchmiITHomeContext.cs
- [ ] T007 Add create/update timestamp handling in Data/wuchmiITHomeContext.cs
- [ ] T008 Add migration files in Migrations/20260211093000_AddTeachAppoEmployee.cs, Migrations/20260211093000_AddTeachAppoEmployee.Designer.cs, and update Migrations/wuchmiITHomeContextModelSnapshot.cs
- [ ] T009 Add seed data for TeachAppoEmployee in Models/SeedData.cs
- [ ] T010 Add DbContext schema/migration test in Tests/wuchmiITHome.Tests/Data/DbContextTests.cs

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Browse employee records (Priority: P1) MVP

**Goal**: Navigate from main menu to a list of records and open record details

**Independent Test**: Navigate from the main menu to the list page and open a record details page

### Tests for User Story 1

- [ ] T011 [P] [US1] Add list page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/ListPageTests.cs
- [ ] T012 [P] [US1] Add details page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/DetailsPageTests.cs

### Implementation for User Story 1

- [ ] T013 [US1] Add navigation link to the maintenance list in Pages/Shared/_Layout.cshtml
- [ ] T014 [US1] Implement list page with `AsNoTracking()` in Pages/TeachAppoEmployees/Index.cshtml and Pages/TeachAppoEmployees/Index.cshtml.cs
- [ ] T015 [US1] Implement details page with error handling in Pages/TeachAppoEmployees/Details.cshtml and Pages/TeachAppoEmployees/Details.cshtml.cs

**Checkpoint**: User Story 1 fully functional and testable independently

---

## Phase 4: User Story 2 - Create a new record (Priority: P2)

**Goal**: Create a new teach appo employee record with validation

**Independent Test**: Submit a valid record and verify it appears in the list; submit a duplicate key and see validation errors

### Tests for User Story 2

- [ ] T016 [P] [US2] Add create page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/CreatePageTests.cs

### Implementation for User Story 2

- [ ] T017 [US2] Implement create page with validation and duplicate key handling in Pages/TeachAppoEmployees/Create.cshtml and Pages/TeachAppoEmployees/Create.cshtml.cs

**Checkpoint**: User Stories 1 and 2 both work independently

---

## Phase 5: User Story 3 - Update or remove a record (Priority: P3)

**Goal**: Edit existing records and delete obsolete records with confirmation

**Independent Test**: Edit a record and verify saved changes; delete a record and confirm it is removed

### Tests for User Story 3

- [ ] T018 [P] [US3] Add edit page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/EditPageTests.cs
- [ ] T019 [P] [US3] Add delete page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/DeletePageTests.cs

### Implementation for User Story 3

- [ ] T020 [US3] Implement edit page with immutable keys, validation, and error handling in Pages/TeachAppoEmployees/Edit.cshtml and Pages/TeachAppoEmployees/Edit.cshtml.cs
- [ ] T021 [US3] Implement delete page with confirmation and error handling in Pages/TeachAppoEmployees/Delete.cshtml and Pages/TeachAppoEmployees/Delete.cshtml.cs

**Checkpoint**: All user stories independently functional

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T022 [P] Add test run instructions to specs/002-teach-appo-empl-crud/quickstart.md

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - no dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - independent of US1 (aside from shared data)
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - independent of US1/US2 (aside from shared data)

### Parallel Opportunities

- Setup: T003 can run in parallel with solution updates
- US1 tests (T011, T012) can run in parallel
- US2 test (T016) can run in parallel with other test tasks
- US3 tests (T018, T019) can run in parallel
- Polish doc update (T022) can run in parallel at the end

---

## Parallel Example: User Story 1

```bash
Task: "Add list page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/ListPageTests.cs"
Task: "Add details page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/DetailsPageTests.cs"
```

---

## Parallel Example: User Story 2

```bash
Task: "Add create page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/CreatePageTests.cs"
```

---

## Parallel Example: User Story 3

```bash
Task: "Add edit page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/EditPageTests.cs"
Task: "Add delete page tests in Tests/wuchmiITHome.Tests/TeachAppoEmployees/DeletePageTests.cs"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational
3. Complete Phase 3: User Story 1
4. Validate US1 independently (tests + manual nav)

### Incremental Delivery

1. Setup + Foundational
2. US1 -> test -> demo
3. US2 -> test -> demo
4. US3 -> test -> demo
5. Polish updates

### Parallel Team Strategy

- After Foundational completes, run US1, US2, and US3 test tasks in parallel, then implement pages per story in priority order
