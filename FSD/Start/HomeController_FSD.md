# HomeController — Functional Specification Document

> **Controller:** `HomeController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\HomeController.cs`  
> **Inherits:** `BaseController`  
> **Lines:** 498 | **Methods:** 17

---

## 1. Module Overview

The **HomeController** serves as the application's main dashboard after successful login. It handles:
- User authorization and rights initialization
- Center task permission checks (Foreign Donation, Collection Box, HQ Centre, Membership, Magazine)
- Dashboard alerts and notification counts
- Menu visibility based on user roles
- Footer data with session info and app version
- Help videos and circulars

---

## 2. Screens / Forms

### 2.1 Main Dashboard — `Index` (Line 51)
- **Route:** `GET /Start/Home/Index`
- **Auth:** `[CheckLogin]`
- **Purpose:** Primary landing page after login — loads all permissions, reports, and dashboard config
- **ViewBag Data Passed:**
  - `ServerDateFormat`, `DateFormatCurrent`
  - `LoginUserType`, `InsName`, `CenName`, `CenUID`
  - `AllowForeignDonation`, `Open_User_ID`, `_open_Ins_ID`, `_open_FY`
  - `WebConfigSettings` — full AppSettings dictionary passed to view

### 2.2 Generic Login Dashboard — `Page` (Line 187)
- **Route:** `GET /Start/Home/Page`
- **Auth:** `[CheckLogin]`
- **Purpose:** Same as Index but sets `IsGenericLogin = true`
- **Delegates to:** `Index()`

### 2.3 Dashboard Panel — `IndexDashboard` (Line 193)
- **Route:** `GET /Start/Home/IndexDashboard`
- **Auth:** `[CheckLogin]`
- **Purpose:** Dashboard content without the full layout — same permission logic as Index but returns `IndexDashboard` view
- **Note:** Near-duplicate of Index logic (Code DRY violation)

### 2.4 Home View — `Home` (Line 317)
- **Route:** `GET /Start/Home/Home`
- **Auth:** `[CheckLogin]`
- **Purpose:** Loads auto-open screens, center news, help videos, and video categories
- **ViewBag Data:**
  - `AutoOpenScreenIDs` — screens to open automatically on dashboard load
  - `CenterNews` — HTML content of center news
  - `helpVideos` — DataTable of help videos
  - `categories` — distinct video categories list

### 2.5 Static Content Pages
- **Circulars** (Line 346) — returns Circulars view
- **Videos** (Line 350) — returns Videos view
- **HelpVideos** (Line 354) — loads help videos DataTable + category list
- **GSRVideos** (Line 365) — returns GSR Videos view
- **Circulars_Page** (Line 369) — returns Circulars_Page view

---

## 3. Operations

### 3.1 User Authorization — `UserAuthorization` (Line 23)
- **Auth:** `[CheckLogin]`
- **Purpose:** Reads `BASE._Auth_Rights_Listing` and maps each Task+Permission combo into session
- **Logic:**
  1. Iterates `_Auth_Rights_Listing` rows → sets `SetRightSession(Task_Permission, true)` for each
  2. If user is admin → sets `isAdmin` right
  3. If user type is SuperUser or Auditor → sets `SuperUser_Auditor` right
- **Returns:** JSON with all session keys and session count (debugging info)

### 3.2 Center Task Permissions — within `Index` and `IndexDashboard` (Line 66-169)
- **DAL Call:** `BASE._ClientUserDBOps.GetCenterTasks()`
- **Permissions checked via DataView.Find:**

| Task Name | BASE Flag | Purpose |
|-----------|-----------|---------|
| FOREIGN DONATION | `Allow_Foreign_Donation` | Enables foreign donation vouchers |
| COLLECTION BOX - BANK | `Allow_Bank_In_C_Box` | Allows bank in collection box |
| H.Q. CENTRE | `Is_HQ_Centre` | Marks center as headquarters |
| PRINT STATEMENTS | `Allow_Statements_Without_Restrictions` | Unrestricted statement printing |

- **Account Type Checks:**
  - `_open_Year_Acc_Type == "MEMBERSHIP"` → `Allow_Membership = true`
  - `_open_Year_Acc_Type == "MAGAZINE"` → `Allow_Magazine = true`

### 3.3 Reports to Print — within `Index` and `IndexDashboard` (Line 167-169)
- **DAL Call:** `BASE._CenterDBOps.GetReportsToBePrintedInfo(YearID)`
- **Sets:** `BASE._ReportsToBePrinted`

### 3.4 Alerts / Dashboard Data — `get_Alerts_Data` (Line 436)
- **Auth:** `[CheckLogin]`
- **Returns JSON with counts:**

| Alert | DAL Call | Purpose |
|-------|----------|---------|
| Action_Item_Count | `_Action_Items_DBOps.GetOverDueCount()` | Overdue action items |
| Center_Remark_Count | `_Action_Items_DBOps.GetPendingCentreRemarkCount()` | Pending center remarks |
| Asset_Transfer_Count | `_AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer()` | Unmatched asset transfers |
| Internal_Transfer_Count | `_Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount()` | Unmatched internal transfers |
| Unread_Request_Count | `_Req_DBOps.GetUnreadCount()` | Unread requests |
| MonthlyServiceReport | `_SR_DBOps.GetMonthlyServiceReport()` | Service report status |

### 3.5 Role-Based Checks
- **Allow_Superuser_Auditor_Operations** (Line 373): Returns true if user is AUDITOR or SUPERUSER
- **Allow_HQ_Operations** (Line 388): Returns true if `Is_HQ_Centre` is set

### 3.6 Footer Data — `get_Footer_Data` (Line 403)
- **Auth:** `[CheckLogin]`
- **Returns JSON:** CenID, UserType, CompletedYear number, app Version, User ID
- **Includes:** Previous year unaudited status check via `BASE.CheckPrevYearID()`

### 3.7 Menu Visibility — `UserAuthMenu` (Line 463)
- **Purpose:** Returns list of visible menu items + user role + dynamic menu JSON
- **Logic:**
  1. Serializes `BASE._Dynamic_Menu_Listing` to JSON
  2. Collects menu IDs from `BASE._Menu_vibilities_Listing`
  3. Returns role type: "CLIENT ROLE", "SUPERUSER", or "Not_ClientRole"

### 3.8 Attachment Rights — `GetAttachmentRights` (Line 488)
- **Purpose:** Sets ViewData for Help attachment CRUD rights (Add, Delete, View, Update, List)
- **Uses:** `CheckRights(BASE, ClientScreen.Help_Attachments, permission)`

---

## 4. Business Rules & Validations

| # | Rule | Location |
|---|------|----------|
| 1 | Center task permissions are refreshed on every Index load | Index, Line 66-146 |
| 2 | Membership mode enabled only if `_open_Year_Acc_Type == "MEMBERSHIP"` | Index, Line 149-156 |
| 3 | Magazine mode enabled only if `_open_Year_Acc_Type == "MAGAZINE"` | Index, Line 158-165 |
| 4 | Full AppSettings dictionary exposed to view (potential security concern) | Index, Line 183 |
| 5 | Index and IndexDashboard share near-identical permission logic (code duplication) | Lines 51-186 vs 193-316 |

---

## 5. Dependencies & Cross-References

### DAL Calls
| Object | Method | Purpose |
|--------|--------|---------|
| `_ClientUserDBOps` | GetCenterTasks | Center task permissions |
| `_CenterDBOps` | GetReportsToBePrintedInfo | Pending reports |
| `_Action_Items_DBOps` | GetServerDateTime, GetOverDueCount, GetPendingCentreRemarkCount | Server time, alerts |
| `_AssetTransfer_DBOps` | GetUnmatchedCount_AssetTransfer | Asset transfer alerts |
| `_Internal_Tf_Voucher_DBOps` | GetCenterToCentreUnmatchedCount | Internal transfer alerts |
| `_Req_DBOps` | GetUnreadCount | Unread requests |
| `_SR_DBOps` | GetMonthlyServiceReport | Service report |
| `_UserPreferences_DBOps` | AutoOpenScreen_GetList | Auto-open screens |
| `_News_DBOps` | GetCenterNews | Center news HTML |
| `_HelpVideos_dbops` | get_HelpVideos | Help videos list |

### Cross-References
> 📎 **User Authorization:** Rights set here in `UserAuthorization()` are consumed by ALL other controllers via `CheckRights()` in `BaseController`. See [BaseController TDD](file:///e:/Learn/ConnectOneMVC/TDD/TopLevel/BaseController_TDD.md) (when completed).
