# HomeController — Technical Design Document

> **Controller:** `HomeController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\HomeController.cs`  
> **Inherits:** `BaseController`  
> **Lines:** 498 | **Methods:** 17

---

## 1. Controller Architecture

- **Namespace:** `ConnectOneMVC.Areas.Start.Controllers`
- **Base Class:** `BaseController`
- **Auth:** Most methods decorated with `[CheckLogin]`
- **Session Property:** `AutoOpenScreenIDs` (string[]) via `GetBaseSession("AutoOpenScreenIDs_AutoOpenScreen")`
- **Key Role:** Post-login initialization of user rights and center permissions

---

## 2. Action Methods

| # | Method | HTTP | Auth | Return | Lines |
|---|--------|------|------|--------|-------|
| 1 | UserAuthorization | GET | ✅ | JsonResult | 23-50 |
| 2 | Index | GET | ✅ | ViewResult | 51-186 |
| 3 | Page | GET | ✅ | ViewResult | 187-192 |
| 4 | IndexDashboard | GET | ✅ | ViewResult | 193-316 |
| 5 | Home | GET | ✅ | ViewResult | 317-345 |
| 6 | Circulars | GET | ❌ | ViewResult | 346-349 |
| 7 | Videos | GET | ❌ | ViewResult | 350-353 |
| 8 | HelpVideos | GET | ❌ | ViewResult | 354-364 |
| 9 | GSRVideos | GET | ❌ | ViewResult | 365-368 |
| 10 | Circulars_Page | GET | ❌ | ViewResult | 369-372 |
| 11 | Allow_Superuser_Auditor_Operations | GET | ✅ | JsonResult | 373-387 |
| 12 | Allow_HQ_Operations | GET | ✅ | JsonResult | 388-402 |
| 13 | get_Footer_Data | GET | ✅ | JsonResult | 403-435 |
| 14 | get_Alerts_Data | GET | ✅ | JsonResult | 436-462 |
| 15 | UserAuthMenu | GET | ❌ | JsonResult | 463-487 |
| 16 | GetAttachmentRights | - | ❌ | void | 488-495 |

---

## 3. Data Flow

### Dashboard Load Sequence
```
Login Success → Redirect to /Start/Home/Index
  → UserAuthorization() — set rights in session
  → GetCenterTasks() — check center permissions
  → GetReportsToBePrintedInfo() — pending reports
  → GetAttachmentRights() — CRUD rights for attachments
  → Return Index view (with all ViewBag data)

Client-side AJAX (after page load):
  → get_Footer_Data() — footer bar info
  → get_Alerts_Data() — badge counts
  → UserAuthMenu() — show/hide menu items
  → Home() — dashboard content + auto-open screens + center news + help videos
```

---

## 4. Models Used

| Model | Usage |
|-------|-------|
| No dedicated models | Uses `DataTable` for all data, `ViewBag`/`ViewData` for passing to views |

---

## 5. Views / Partials Referenced

| View | Method |
|------|--------|
| Index | Index, Page |
| IndexDashboard | IndexDashboard |
| Home | Home |
| Circulars | Circulars |
| Videos | Videos |
| HelpVideos | HelpVideos |
| GSRVideos | GSRVideos |
| Circulars_Page | Circulars_Page |

---

## 6. DAL / Service Methods Called

| # | DAL Object | Method | Called By |
|---|-----------|--------|----------|
| 1 | `_ClientUserDBOps` | GetCenterTasks | Index, IndexDashboard |
| 2 | `_CenterDBOps` | GetReportsToBePrintedInfo | Index, IndexDashboard |
| 3 | `_Action_Items_DBOps` | GetServerDateTime | Index, IndexDashboard |
| 4 | `_Action_Items_DBOps` | GetOverDueCount | get_Alerts_Data |
| 5 | `_Action_Items_DBOps` | GetPendingCentreRemarkCount | get_Alerts_Data |
| 6 | `_AssetTransfer_DBOps` | GetUnmatchedCount_AssetTransfer | get_Alerts_Data |
| 7 | `_Internal_Tf_Voucher_DBOps` | GetCenterToCentreUnmatchedCount | get_Alerts_Data |
| 8 | `_Req_DBOps` | GetUnreadCount | get_Alerts_Data |
| 9 | `_SR_DBOps` | GetMonthlyServiceReport | get_Alerts_Data |
| 10 | `_UserPreferences_DBOps` | AutoOpenScreen_GetList | Home |
| 11 | `_News_DBOps` | GetCenterNews | Home |
| 12 | `_HelpVideos_dbops` | get_HelpVideos | Home, HelpVideos |

---

## 7. Session Variables Used

### Read
`_Auth_Rights_Listing`, `_Menu_vibilities_Listing`, `_Dynamic_Menu_Listing`, `_open_User_User_Is_Admin`, `_open_User_Type`, `_open_User_ID`, `_open_Cen_ID`, `_open_Ins_Name`, `_open_Ins_ID`, `_open_Cen_Name`, `_open_UID_No`, `_open_Year_ID`, `_open_Year_Acc_Type`, `_prev_Unaudited_YearID`, `_Completed_Year_Count`, `_Server_Date_Format_Short`, `_Date_Format_Current`

### Written
`Allow_Foreign_Donation`, `Allow_Bank_In_C_Box`, `Is_HQ_Centre`, `Allow_Membership`, `Allow_Magazine`, `Allow_Statements_Without_Restrictions`, `_open_ClientUser`, `Refresh_Notes_List`, `_ReportsToBePrinted`

---

## 8. Error Handling

- Silent catch in `GetServerDateTime()` call (Line 58-64) — empty catch block
- No error handling in UserAuthorization, get_Alerts_Data, etc.
- Potential NullReferenceException if `GetCenterTasks()` returns null (Line 76 — DataView on null)

---

## 9. Dependencies & Cross-References

- **`Index` vs `IndexDashboard`:** Code duplication — same permission logic repeated (Lines 51-186 mirrors 193-316)
- **Security concern:** `WebConfigSettings` passes ALL AppSettings to view (Line 183) — could expose sensitive config values
- **`UserAuthMenu`:** Critical for menu visibility — consumed by client-side JS to show/hide navigation items
- **Circulars, Videos, GSRVideos** have no `[CheckLogin]` — accessible without authentication
