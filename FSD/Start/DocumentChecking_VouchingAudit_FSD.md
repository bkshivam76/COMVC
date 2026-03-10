# DocumentCheckingController — FSD + TDD

> **Controller:** `DocumentCheckingController.cs`  
> **Area:** Start | **Lines:** 194 | **Methods:** 11 | **Auth:** `[CheckLogin]` class-level

---

## FSD

### Module Overview
**Document verification workflow** for auditors — accept, reject, or uncheck uploaded documents. Supports filtering by category, name, status, audit status, data scope, and checked-by.

### Screens
- **Document Checking Info** — `DocumentCheckingInfo` (Line 30): Main view with Update/Delete rights
- **Document Grid** — `DocumentChecking_Grid` (Line 38): Filterable grid via `_Audit_DBOps.GetDocumentChecking()`. Supports: AuditStatus enum (All/Accepted/Rejected/Unchecked), DataScope enum (Exclusive/All), layout persistence, horizontal scroll toggle.

### Operations
| Method | Line | DAL Call | Purpose |
|--------|------|----------|---------|
| AcceptDocument | 58 | `_Attachments_DBOps.Mark_Accepted_attachment(AttachID)` | Mark doc as accepted |
| UncheckDocument | 89 | `_Attachments_DBOps.Mark_Unchecked_attachment(AttachID)` | Revert doc to unchecked |
| RejectDocument | 120 | `_Attachments_DBOps.Mark_Rejected_attachment(AttachID, Reason)` | Reject with sanitized reason |
| Frm_DC_RejectReason | 152 | — | Popup view for rejection reason |

### Lookups
- **Category** — `_Attachments_DBOps.GetDocument_Categories()` (DevExtreme DataSource)
- **Name** — `_Attachments_DBOps.GetDocument_Names(CategoryName)` (filtered by category)
- **CheckedBy** — `_ClientUserDBOps.GetAuditors_Superusers()` (auditor/superuser list)

### Utilities
- `GetFileMimeType` (Line 179): Returns MIME type for file preview
- `CheckRights` (Line 187): Sets Update/Delete rights for Help_Attachments

### Security
- Reason text sanitized: `[` → `(`, `]` → `)`, `'` → `` ` ``, `!` → `|` (SQL injection prevention)

---

# VouchingAuditController — FSD + TDD

> **Controller:** `VouchingAuditController.cs`  
> **Area:** Start | **Lines:** 127 | **Methods:** 7 + 1 Helper Class

---

## FSD

### Module Overview
**Cash donation vouching** workflow — auditors review individual donation entries, check documents, and accept/reject with structured responses (3 questions).

### Screens
- **Vouching Screen** GET — `VouchingScreen` (Line 16): Loads next vouching entry via `_Voucher_DBOps.GetVouchingAuditData(InsttID, Category)`. Returns: entry ID, attachment filename, multiple-donations flag, and donation register data.
- **Vouching Dashboard** — `VouchingAuditorDashboard` (Line 107): Overview dashboard with `_Voucher_DBOps.GetVouchingAuditDashboard(UserID)`.

### Operations
- **VouchingScreen POST** (Line 39): Submits vouching audit result via `_Voucher_DBOps.AddVouchingAudit()`:
  - **Status:** Accepted or Rejected (mapped to `Vouching_audit_Status` enum)
  - **3 structured responses:**
    1. "Donation Letter in correct name of Institute?" → YES/NO
    2. "Type of Donation?" → text value
    3. "Signed?" → YES/NO
  - After submission, auto-loads next entry

### Partial Views
- `VouchingLeftPanel`, `VouchingRightPanel`, `VouchingMidPanel`, `VouchingForm_CashDonations` — layout partials

### DAL Calls
| Object | Method |
|--------|--------|
| `_Voucher_DBOps` | GetVouchingAuditData, AddVouchingAudit, GetVouchingAuditDashboard |
| `_DonationRegister_DBOps` | GetList |

### Model
`VouchingAuditModel`: VF_Instt_Code, VF_Category, VF_ENTRY_ID, Curr_Attachment_File_Name, VF_Cash_Donation_Multiple_Cash_Donations_Same_Day_Same_Party, VF_Vouching_Status, VF_Cash_Donation_Correct_Name_Instt, VF_Cash_Donation_Donation_Type, VF_Cash_Donation_Signed, DonationData

### Helper
`WidgetsDemoHelper` (Line 120): Static list of demo widget names — likely unused/demo code
