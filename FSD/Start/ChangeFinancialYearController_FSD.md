# ChangeFinancialYearController — Functional Specification Document

> **Controller:** `ChangeFinancialYearController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\ChangeFinancialYearController.cs`  
> **Inherits:** `BaseController` | **Auth:** `[CheckLogin]` (class-level)  
> **Lines:** 332 | **Methods:** 6 + 1 Model Class

---

## 1. Module Overview

Allows users to switch the active **Financial Year (FY)** for the current center. After switching, all center task permissions are recalculated and session state is updated.

---

## 2. Screens / Forms

### 2.1 Change Financial Year Form — `Frm_COD_Change_Year` GET (Line 21)
- **Route:** `GET /Start/ChangeFinancialYear/Frm_COD_Change_Year`
- **Purpose:** Displays grid of all financial years for the center
- **Data Source:** `BASE._CODDBOps.GetFinancialYearList()` → mapped to `List<COD_Change_YearModel>`
- **Grid Columns:** FY ID, Account Type, Financial Year name, From date, To date, Lock status, Default flag

---

## 3. Operations

### 3.1 Switch Financial Year (POST) — `Frm_COD_Change_Year` POST (Line 38)
- **Input:** `COD_Change_YearModel` (XCOD_YEAR_ID, XCOD_YEAR_NAME, XCOD_YEAR_SDT, XCOD_YEAR_EDT, XCOD_ACC_TYPE, SetDefaultPressed_CFY)
- **Business Logic:**
  1. If `SetDefaultPressed_CFY == true` → calls `BASE._CODDBOps.UpdateDefaultFinancialYear(yearID)`
  2. Updates all BASE year session vars (Year_ID, Year_Name, Year_Sdt, Year_Edt, Year_Acc_Type)
  3. Calls `BASE.OnChangeCenter_Year()` to reinitialize year-dependent data
  4. Gets unaudited years and completed year count (same logic as LoginController)
  5. Checks audit status via `Get_Base_OpenEventId()`
  6. **Recalculates all center task permissions** (Foreign Donation, Collection Box, HQ Centre, Statements, Membership, Magazine) — identical code to HomeController.Index
  7. Gets reports-to-be-printed
  8. Clears FY grid session data

### 3.2 FY Grid — `Frm_COD_Change_Year_Grid` (Line 286)
- Refresh/callback for the financial year DevExpress grid

### 3.3 FY Grid Custom Data — `Frm_COD_Change_Year_Grid_CustomData` (Line 26)
- Returns selected row data via `![` separator (same pattern as LoginController)

### 3.4 Data Load — `Frm_COD_Change_Year_Data` (Line 294)
- Converts FY DataTable to `List<COD_Change_YearModel>` with date formatting (dd/MM/yyyy)
- "Default" flag = `Lock == "No"` (unlocked years can be set as default)

### 3.5 Session Clear — `SessionClear` (Line 310)
- Clears `_ChangeFinancialYear` session keys

---

## 4. Business Rules

| # | Rule |
|---|------|
| 1 | Only unlocked years (Lock="No") can be set as default |
| 2 | Switching FY triggers full permission recalculation |
| 3 | Unaudited year tracking reset on FY change |
| 4 | Audit status re-checked after FY switch |

---

## 5. Data Fields — `COD_Change_YearModel` (Line 316)

| Field | Type | Purpose |
|-------|------|---------|
| ACC_TYPE, COD_YEAR_ID, Financial_Year, From, To, Lock | string | Display in grid |
| Default | bool | Computed: Lock == "No" |
| XCOD_ACC_TYPE, XCOD_YEAR_NAME | string | Posted back on submit |
| XCOD_YEAR_ID | int | Posted year ID |
| XCOD_YEAR_SDT, XCOD_YEAR_EDT | DateTime | Posted dates |
| SetDefaultPressed_CFY | bool | Flag for default year operation |

---

## 6. Dependencies & Cross-References

> 📎 **Code Duplication:** Center task permission logic (Lines 128-237) is identical to [HomeController FSD > Section 3.2](file:///e:/Learn/ConnectOneMVC/FSD/Start/HomeController_FSD.md). Same 4 tasks checked via DataView.Find.

> 📎 **Shared Logic:** Unaudited year tracking logic (Lines 59-83) is identical to [LoginController FSD > Section 3.11: ChangeCenter](file:///e:/Learn/ConnectOneMVC/FSD/Start/LoginController_FSD.md).

### DAL Calls
| Object | Method | Purpose |
|--------|--------|---------|
| `_CODDBOps` | GetFinancialYearList | List all FYs |
| `_CODDBOps` | UpdateDefaultFinancialYear | Set FY as default |
| `_CODDBOps` | GetUnAuditedFinalYears | Unaudited year tracking |
| `_CODDBOps` | GetCompletedtYearCount | Completed year count |
| `_CenterDBOps` | Get_Base_OpenEventId | Audit status check |
| `_CenterDBOps` | GetReportsToBePrintedInfo | Reports pending |
| `_ClientUserDBOps` | GetCenterTasks | Center task permissions |
