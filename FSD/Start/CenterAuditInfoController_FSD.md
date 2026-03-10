# CenterAuditInfoController — FSD + TDD

> **Controller:** `CenterAuditInfoController.cs`  
> **Area:** Start | **Lines:** 125 | **Methods:** 6 | **Auth:** `[CheckLogin]` class-level

---

## FSD

### Module Overview
Read-only dashboard showing **center audit history**: grading history, audit event history, and auditor allocation history.

### Screens
- **Audit Info** — `Frm_Audit_Info` (Line 30): Loads `Client_Audit_Info()` DataSet with 6 tables:
  - Tables[0] = Gradings, Tables[1] = Grading Header text
  - Tables[2] = Audit History, Tables[3] = Audit Header text
  - Tables[4] = Allocations, Tables[5] = Allocations Header text
- **Grading Grid** — `GradingInfo_Grid` (Line 70): Refresh callback
- **Audit History Grid** — `AuditHistory_Grid` (Line 85): Refresh callback
- **Allocations Grid** — `AuditAllocations_Grid` (Line 100): Refresh callback
- **Export Options** — `Frm_Export_Options` (Line 119): Tab-based export (Grading/History/Allocations)

### DAL Calls
| Object | Method |
|--------|--------|
| `_CenterDBOps` | Client_Audit_Info() → returns 6-table DataSet |

### Notes
- Export headers follow standard pattern: UID, Year, PrintedBy
- Bug: `AuditAllocations_Grid` refresh loads `Tables[0]` instead of `Tables[4]` (Line 110)
- Model: `AuditInfo` (Start.Models) with grdGradings, grdAuditHistory, grdAllocations DataTables
