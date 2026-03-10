# ChangeFinancialYearController — Technical Design Document

> **Controller:** `ChangeFinancialYearController.cs`  
> **Area:** Start | **Lines:** 332 | **Auth:** `[CheckLogin]` class-level

---

## 1. Controller Architecture
- Class-level `[CheckLogin]` — all methods require authentication
- Session Property: `CFY_Data` (`List<COD_Change_YearModel>`) cached in session
- Inline model class: `COD_Change_YearModel` (Line 316-331)

## 2. Action Methods

| # | Method | HTTP | Lines | Purpose |
|---|--------|------|-------|---------|
| 1 | Frm_COD_Change_Year | GET | 21-25 | Load FY grid view |
| 2 | Frm_COD_Change_Year_Grid_CustomData | GET | 26-36 | Grid row selection callback |
| 3 | Frm_COD_Change_Year | POST | 38-285 | Switch FY + recalculate permissions |
| 4 | Frm_COD_Change_Year_Grid | GET | 286-293 | Grid refresh callback |
| 5 | Frm_COD_Change_Year_Data | - | 294-309 | Data load (LINQ DataTable→Model) |
| 6 | SessionClear | - | 310-313 | Clear session |

## 3. Data Flow
```
Grid Load: Frm_COD_Change_Year → Frm_COD_Change_Year_Data → _CODDBOps.GetFinancialYearList → Grid
FY Switch: POST Frm_COD_Change_Year → UpdateDefaultFinancialYear → OnChangeCenter_Year
  → GetUnAuditedFinalYears → Get_Base_OpenEventId → GetCenterTasks → GetReportsToBePrintedInfo
```

## 4. DAL Calls
| Object | Method |
|--------|--------|
| `_CODDBOps` | GetFinancialYearList, UpdateDefaultFinancialYear, GetUnAuditedFinalYears, GetCompletedtYearCount |
| `_CenterDBOps` | Get_Base_OpenEventId, GetReportsToBePrintedInfo |
| `_ClientUserDBOps` | GetCenterTasks |

## 5. Session Variables
**Written:** `_open_Year_ID`, `_open_Year_Name`, `_open_Year_Sdt`, `_open_Year_Edt`, `_open_Year_Acc_Type`, `_prev_Unaudited_YearID`, `_next_Unaudited_YearID`, `_Completed_Year_Count`, `_IsUnderAudit`, `Allow_Foreign_Donation`, `Allow_Bank_In_C_Box`, `Is_HQ_Centre`, `Allow_Membership`, `Allow_Magazine`, `Allow_Statements_Without_Restrictions`, `_open_ClientUser`, `Refresh_Notes_List`, `_ReportsToBePrinted`

## 6. Cross-References
- Center task permission logic mirrors [HomeController TDD](file:///e:/Learn/ConnectOneMVC/TDD/Start/HomeController_TDD.md)
- Unaudited year logic mirrors [LoginController TDD > ChangeCenter](file:///e:/Learn/ConnectOneMVC/TDD/Start/LoginController_TDD.md)
- Commented-out VB.NET code throughout suggests desktop-to-web migration
