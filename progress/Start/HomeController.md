# HomeController.cs — Method-Level Progress

> **Source:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\HomeController.cs`  
> **Area:** Start | **Total Lines:** 498 | **Lines Read:** 498 / 498  
> **Status:** ✅ COMPLETED  
> **Last Updated:** 2026-03-06  
> **FSD:** [HomeController_FSD.md](file:///e:/Learn/ConnectOneMVC/FSD/Start/HomeController_FSD.md)  
> **TDD:** [HomeController_TDD.md](file:///e:/Learn/ConnectOneMVC/TDD/Start/HomeController_TDD.md)

---

## Method Progress: 17 / 17 completed ✅

| # | Method | Lines | Size | Status | Summary |
|---|--------|-------|------|--------|---------|
| 1 | AutoOpenScreenIDs (property) | 18-22 | 5 | ✅ | Session property for auto-open screen IDs |
| 2 | UserAuthorization | 23-50 | 28 | ✅ | [CheckLogin] Iterates _Auth_Rights_Listing, sets per-task-permission session rights, adds isAdmin and SuperUser_Auditor flags |
| 3 | Index | 51-186 | 136 | ✅ | [CheckLogin] Main dashboard: calls UserAuthorization, sets 4 center task flags (Foreign Donation, Collection Box, HQ, Statements), sets Membership/Magazine by acc type, loads reports to print, sets ViewBag with date format/user info/WebConfigSettings |
| 4 | Page | 187-192 | 6 | ✅ | [CheckLogin] Generic login dashboard — sets IsGenericLogin=true, delegates to Index() |
| 5 | IndexDashboard | 193-316 | 124 | ✅ | [CheckLogin] Near-duplicate of Index permission logic without UserAuthorization call and ViewBag setup. Code duplication. |
| 6 | Home | 317-345 | 29 | ✅ | [CheckLogin] Loads auto-open screens list, center news HTML, help videos with categories |
| 7 | Circulars | 346-349 | 4 | ✅ | Returns Circulars view (no auth) |
| 8 | Videos | 350-353 | 4 | ✅ | Returns Videos view (no auth) |
| 9 | HelpVideos | 354-364 | 11 | ✅ | Loads help videos DataTable + distinct categories list (no auth) |
| 10 | GSRVideos | 365-368 | 4 | ✅ | Returns GSR Videos view (no auth) |
| 11 | Circulars_Page | 369-372 | 4 | ✅ | Returns Circulars_Page view (no auth) |
| 12 | Allow_Superuser_Auditor_Operations | 373-387 | 15 | ✅ | [CheckLogin] Returns true if user is AUDITOR or SUPERUSER |
| 13 | Allow_HQ_Operations | 388-402 | 15 | ✅ | [CheckLogin] Returns true if Is_HQ_Centre flag set |
| 14 | get_Footer_Data | 403-435 | 33 | ✅ | [CheckLogin] Returns JSON: CenID, UserType, CompletedYear, Version, User, prev year unaudited status |
| 15 | get_Alerts_Data | 436-462 | 27 | ✅ | [CheckLogin] Returns 6 alert counts: overdue actions, center remarks, asset/internal transfers, unread requests, service report |
| 16 | UserAuthMenu | 463-487 | 25 | ✅ | Returns menu visibility list + user role + dynamic menu JSON |
| 17 | GetAttachmentRights | 488-495 | 8 | ✅ | Sets ViewData for Help attachment CRUD rights (Add/Delete/View/Update/List) |

---

## DAL / Service Calls Traced

| # | Called From | External Method | Object | Status |
|---|-----------|-----------------|--------|--------|
| 1 | Index | GetCenterTasks | _ClientUserDBOps | ✅ |
| 2 | Index | GetReportsToBePrintedInfo | _CenterDBOps | ✅ |
| 3 | Index | GetServerDateTime | _Action_Items_DBOps | ✅ |
| 4 | get_Alerts_Data | GetOverDueCount | _Action_Items_DBOps | ✅ |
| 5 | get_Alerts_Data | GetPendingCentreRemarkCount | _Action_Items_DBOps | ✅ |
| 6 | get_Alerts_Data | GetUnmatchedCount_AssetTransfer | _AssetTransfer_DBOps | ✅ |
| 7 | get_Alerts_Data | GetCenterToCentreUnmatchedCount | _Internal_Tf_Voucher_DBOps | ✅ |
| 8 | get_Alerts_Data | GetUnreadCount | _Req_DBOps | ✅ |
| 9 | get_Alerts_Data | GetMonthlyServiceReport | _SR_DBOps | ✅ |
| 10 | Home | AutoOpenScreen_GetList | _UserPreferences_DBOps | ✅ |
| 11 | Home | GetCenterNews | _News_DBOps | ✅ |
| 12 | Home, HelpVideos | get_HelpVideos | _HelpVideos_dbops | ✅ |

## Failed Methods: None ✅

## Notes
- Index and IndexDashboard have near-identical code (DRY violation)
- Circulars/Videos/GSRVideos lack [CheckLogin] — accessible without auth
- WebConfigSettings exposes ALL AppSettings to client — security concern
- Empty catch for GetServerDateTime — silent failure
