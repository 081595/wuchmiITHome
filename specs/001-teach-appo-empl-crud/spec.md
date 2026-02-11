# Feature Specification: Teach Appo Employee CRUD

**Feature Branch**: `001-teach-appo-empl-crud`  
**Created**: 2026-02-11  
**Status**: Draft  
**Input**: User description: "TABLE [dbo].[teach_appo_empl_base](...); 使用以上內容,新增crud維護頁面,頁面連結放入導覽列"

## User Scenarios & Testing *(mandatory)*

<!--
  IMPORTANT: User stories should be PRIORITIZED as user journeys ordered by importance.
  Each user story/journey must be INDEPENDENTLY TESTABLE - meaning if you implement just ONE of them,
  you should still have a viable MVP (Minimum Viable Product) that delivers value.
  
  Assign priorities (P1, P2, P3, etc.) to each story, where P1 is the most critical.
  Think of each story as a standalone slice of functionality that can be:
  - Developed independently
  - Tested independently
  - Deployed independently
  - Demonstrated to users independently
-->

### User Story 1 - Browse employee records (Priority: P1)

As a staff user, I can open the teach appo employee maintenance page from the main navigation and browse a list of records with key details so I can find a specific employee record quickly.

**Why this priority**: Without list access and navigation, the page is not discoverable and the data cannot be used.

**Independent Test**: Can be fully tested by navigating from the main menu to the list page and confirming records are visible.

**Acceptance Scenarios**:

1. **Given** I am an authorized staff user, **When** I click the navigation link for the maintenance page, **Then** I see the list of teach appo employee records.
2. **Given** the list page is open, **When** I select a record, **Then** I can view its full details.

---

### User Story 2 - Create a new record (Priority: P2)

As a staff user, I can create a new teach appo employee record by entering required fields so the dataset stays complete and current.

**Why this priority**: Creating new entries is essential to keep the list accurate and actionable.

**Independent Test**: Can be tested by submitting a new record and verifying it appears in the list.

**Acceptance Scenarios**:

1. **Given** I am on the create form, **When** I submit valid required fields, **Then** the record is saved and appears in the list.
2. **Given** I am on the create form, **When** I submit with missing required fields or a duplicate primary key, **Then** I see a validation message and the record is not saved.

---

### User Story 3 - Update or remove a record (Priority: P3)

As a staff user, I can edit existing record details or delete a record that should no longer exist so the data remains accurate.

**Why this priority**: Maintenance requires correcting and removing records over time.

**Independent Test**: Can be tested by editing a record and by deleting a record with confirmation.

**Acceptance Scenarios**:

1. **Given** I am viewing an existing record, **When** I edit allowed fields and save, **Then** the changes are stored and visible in details and list views.
2. **Given** I am viewing an existing record, **When** I confirm deletion, **Then** the record is removed from the list.

---

[Add more user stories as needed, each with an assigned priority]

### Edge Cases

- Duplicate primary key values for `yr`, `id_no`, and `birthday` on create.
- Attempting to edit primary key fields after creation.
- Invalid date values for `birthday` or `refresh_token_expired`.
- Deleting a record that was already removed in another session.

## Requirements *(mandatory)*

<!--
  ACTION REQUIRED: The content in this section represents placeholders.
  Fill them out with the right functional requirements.
-->

### Functional Requirements

- **FR-001**: System MUST provide a navigation link to the teach appo employee maintenance page for authorized staff users.
- **FR-002**: System MUST display a list of teach appo employee records with the key identifiers (`yr`, `id_no`, `birthday`) and basic details (`empl_no`, `ch_name`, `en_name`, `email`).
- **FR-003**: Users MUST be able to open a detail view for a selected record.
- **FR-004**: Users MUST be able to create a new record with all required fields and the system MUST prevent duplicate primary keys.
- **FR-005**: Users MUST be able to edit allowed fields on an existing record while keeping primary key fields immutable.
- **FR-006**: Users MUST be able to delete a record only after confirming the action.
- **FR-007**: System MUST show clear validation messages for missing or invalid required fields and must not save invalid records.
- **FR-008**: System MUST maintain `create_date` and `update_date` values for records and display them in the detail view.

### Key Entities *(include if feature involves data)*

- **Teach Appo Employee Record**: Represents a single employee appointment record with `yr`, `id_no`, `birthday`, `empl_no`, `ch_name`, `en_name`, `email`, `refresh_token`, `refresh_token_expired`, `seq_no`, `create_date`, `update_date`.

## Success Criteria *(mandatory)*

<!--
  ACTION REQUIRED: Define measurable success criteria.
  These must be technology-agnostic and measurable.
-->

### Measurable Outcomes

- **SC-001**: Authorized users can reach the list page from the main navigation in 2 clicks or fewer.
- **SC-002**: Users can create a valid record in under 3 minutes on average during acceptance testing.
- **SC-003**: The list page shows the first page of results within 3 seconds for datasets up to 1,000 records.
- **SC-004**: At least 95% of edit and delete actions during testing persist correctly without requiring rework.

## Assumptions

- Access to the maintenance page is limited to existing authorized staff users.
- Primary key fields (`yr`, `id_no`, `birthday`) are set on creation and cannot be changed later.
- `seq_no`, `create_date`, and `update_date` are system-managed and read-only in forms.
- `refresh_token` and `refresh_token_expired` are maintained by staff as needed.

## Dependencies

- Existing navigation layout can accommodate a new menu link.
- The teach appo employee dataset is available for maintenance operations.
