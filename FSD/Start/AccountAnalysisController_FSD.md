# AccountAnalysisController — FSD + TDD

> **Controller:** `AccountAnalysisController.cs`  
> **Area:** Start | **Lines:** 164 | **Methods:** 8 | **Auth:** `[CheckLogin]` on Frm_Audit_Dashboard

---

## FSD

### Module Overview
Displays an **audit exception dashboard** showing audit checkpoints and exceptions with nested detail grids. Supports export to PDF/Excel.

### Screens
- **Audit Dashboard** — `Frm_Audit_Dashboard` (Line 49): Loads `GetAuditExceptions()` DataSet with Catalog + MainTable tables. Sets export headers (UID, Year, PrintedBy).
- **Audit Checkpoints Grid** — `AuditCheckpoints_Grid` (Line 71): Grid callback with REFRESH support
- **Audit Exceptions Grid** — `AuditExceptions_Grid` (Line 85): Grid callback showing exception list
- **Exception Detail Grid** — `AuditExceptions_DetailGrid` (Line 99): Nested detail grid by ID — iterates DSET tables (excluding MainTable/Catalog) to find matching table by ID column

### Data & Export
- **NestedGridExportSettings** (static, Line 122): DevExpress grid export config
- **NestedGridExportData** (static, Line 130): Reads from `Session["AccountAnalysisData"]` for export
- **Frm_Export_Options** (Line 153): Returns either AuditExceptions or AuditCheckpoints export view based on tab index

### DAL Calls
| Object | Method |
|--------|--------|
| `_Action_Items_DBOps` | GetAuditExceptions() |

### Session Properties
`Catalog`, `MainTable`, `DSET`, `NestedData` — all DataTable/DataSet cached in session

---

## TDD Notes
- DataSet from `GetAuditExceptions()` contains dynamically-named tables: one per exception type (keyed by ID column), plus "Catalog" and "MainTable"
- Static methods access `HttpContext.Current.Session` directly for export (bypasses BASE session pattern)
- `Session["AccountAnalysisData"]` used directly (not via GetBaseSession pattern)

> 📎 **Cross-reference:** `GetAuditExceptions(true)` is also called by [AuditRegistrationController > Check_Restrictions](file:///e:/Learn/ConnectOneMVC/FSD/Start/AuditRegistrationController_FSD.md) to block registration
