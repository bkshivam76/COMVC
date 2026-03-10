# LoginController — Technical Design Document

> **Controller:** `LoginController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\LoginController.cs`  
> **Inherits:** `BaseController`  
> **Lines:** 1111 | **Methods:** 27

---

## 1. Controller Architecture

- **Namespace:** `ConnectOneMVC.Areas.Start.Controllers`
- **Base Class:** `BaseController` (provides `BASE` object, session management, authentication)
- **Authentication:** No `[CheckLogin]` attribute — this IS the login controller
- **Session Properties:** 5 properties using `GetBaseSession`/`SetBaseSession` pattern (InstituteData, CenterData, cenID, yearID, CompletedYearCount)
- **Inner Class:** `Frm_Login_Params` (18 string fields — appears unused, likely legacy)

---

## 2. Action Methods

| # | Method | HTTP | Route Pattern | Return Type |
|---|--------|------|---------------|-------------|
| 1 | Frm_COD_Selection | GET | /Start/Login/Frm_COD_Selection | ViewResult |
| 2 | Frm_COD_Selection_form | GET | /Start/Login/Frm_COD_Selection_form | ViewResult |
| 3 | Frm_COD_Selection_Ins_Grid | GET | /Start/Login/Frm_COD_Selection_Ins_Grid | ViewResult (DevExpress Grid) |
| 4 | CODSelectionInsCustomDataAction | GET | /Start/Login/CODSelectionInsCustomDataAction | GridViewExtension callback |
| 5 | Frm_COD_Selection_Ins_Check | GET | /Start/Login/Frm_COD_Selection_Ins_Check | PartialViewResult / JSON |
| 6 | Frm_COD_Selection_Ins | GET | /Start/Login/Frm_COD_Selection_Ins | PartialViewResult / JSON |
| 7 | Frm_Auditor_Login | GET | /Start/Login/Frm_Auditor_Login | ViewResult |
| 8 | Frm_Auditor_Login | POST | /Start/Login/Frm_Auditor_Login | JSON |
| 9 | UnloadCookie | GET | /Start/Login/UnloadCookie | JSON |
| 10 | Frm_COD_Selection_Ins_Auditor | GET | /Start/Login/Frm_COD_Selection_Ins_Auditor | ViewResult |
| 11 | Frm_COD_Selection_Ins_Auditor_Grid | GET | Grid callback | ViewResult |
| 12 | SessionClear_SelectIns | - | /Start/Login/SessionClear_SelectIns | void |
| 13 | Frm_Login_Post | GET* | /Start/Login/Frm_Login_Post | JSON |
| 14 | Frm_Login | POST | /Start/Login/Frm_Login | ViewResult / JSON |
| 15 | Frm_COD_Selection_Auditor | GET | /Start/Login/Frm_COD_Selection_Auditor | PartialViewResult |
| 16 | Frm_COD_Selection_Auditor_Grid | GET | Grid callback | ViewResult |
| 17 | Frm_COD_Selection_Auditor_Submit | GET | /Start/Login/Frm_COD_Selection_Auditor_Submit | JSON |
| 18 | ChangeCenter | GET | /Start/Login/ChangeCenter | JSON |
| 19 | clearSessionVariable | - | internal | void |
| 20 | CheckResumeAudit | - | /Start/Login/CheckResumeAudit | bool |
| 21 | Logout | GET | /Start/Login/Logout | redirect |
| 22 | Frm_Generic_Login | GET | /Start/Login/Frm_Generic_Login | ViewResult |
| 23 | Frm_Generic_Login | POST | /Start/Login/Frm_Generic_Login | JSON (delegates to Frm_Login_Post) |
| 24 | GetInstitutes | GET | DevExtreme DataSource | JSON |
| 25 | GetCentres | GET | DevExtreme DataSource | JSON |

> **Note:** `Frm_Login_Post` (Line 470) has `[HttpPost]` commented out — it's effectively a GET, which is a security concern.

---

## 3. Data Flow

### Login Flow (COD Certificate)
```
Browser → Frm_COD_Selection (GET view)
       → Frm_COD_Selection_Ins_Check (validate certificate)
       → Frm_COD_Selection_Ins (load institute grid)
       → Frm_COD_Selection_Ins_Grid (grid data)
       → CODSelectionInsCustomDataAction (row selection)
       → Frm_Login (POST — render login form)
       → Frm_Login_Post (submit credentials)
            → BASE._ClientUserDBOps.GetCenterUserInfo() [D0000.Common]
            → BASE.Encrypt() — password hash comparison
            → BASE.OnChangeCenter_Year() [D0000.Common]
            → BASE._CODDBOps.CheckVolumeCenter() [D0000.Common → WebService]
            → BASE._CenterDBOps.Get_Base_OpenEventId() [D0000.Common → WebService]
            → BASE._CenterDBOps.GetChildCenterList() [D0000.Common → WebService]
       → Redirect to Home
```

### Login Flow (Auditor)
```
Browser → Frm_Auditor_Login (GET view with cookie pre-fill)
       → Frm_Auditor_Login (POST — validate credentials)
            → BASE._ClientUserDBOps.GetUserInfo() [D0000.Common]
            → Role check: Auditor or SuperUser only
            → BASE.Encrypt() — password comparison
       → Frm_COD_Selection_Ins_Auditor (select institute)
       → Frm_COD_Selection_Auditor (select center)
       → Frm_COD_Selection_Auditor_Submit (finalize)
            → Audit status check (Returned → ResumeAudit)
            → ChangeCenter() → sets all BASE vars
       → Redirect to Home
```

### Login Flow (Generic)
```
Browser → Frm_Generic_Login (GET — /sparc/{CenID} route)
       → Frm_Generic_Login (POST)
            → BASE._CenterDBOps.GetCenterDetails_Login(cenid)
            → Delegates to Frm_Login_Post() — reuses full flow
```

---

## 4. Models Used

| Model | Namespace | Usage |
|-------|-----------|-------|
| `Center_Ins_List` | ConnectOneMVC.Models | Center/institute details for login |
| `SelectedCenterDetails` | ConnectOneMVC.Models | Certificate check response |
| `Auditor_Login` | ConnectOneMVC.Areas.Start.Models | Auditor credential model |
| `BaseModel` | ConnectOneMVC.Models | Multi-center session entry (CenterGuid + BASE) |
| `Return_Json_Param` | ConnectOneMVC.Models | Standardized JSON response (result, message, title) |

---

## 5. Views / Partials Referenced

| View | Type | Method |
|------|------|--------|
| Frm_COD_Selection | Full View | Frm_COD_Selection |
| Frm_COD_Selection_form | Full View | Frm_COD_Selection_form |
| Frm_COD_Selection_Ins_Grid | Grid View | Frm_COD_Selection_Ins_Grid |
| frm_COD_Selection_Ins_Check | Partial | Frm_COD_Selection_Ins_Check |
| frm_COD_Selection_Ins | Partial | Frm_COD_Selection_Ins |
| Frm_Auditor_Login | Full View | Frm_Auditor_Login GET |
| Frm_Login | Full View | Frm_Login |
| Frm_COD_Selection_Ins_Auditor | Full View | Frm_COD_Selection_Ins_Auditor |
| Frm_COD_Selection_Auditor | Partial | Frm_COD_Selection_Auditor |
| Frm_Generic_Login | Full View | Frm_Generic_Login GET |

---

## 6. DAL / Service Methods Called

| # | DAL Object | Method | Called From | Purpose |
|---|-----------|--------|-------------|---------|
| 1 | `_CenterDBOps` | GetSelectCentreList | Frm_COD_Selection_Ins_Check, _Ins | Get center by certificate |
| 2 | `_CenterDBOps` | GetCen_Ins_List | Frm_COD_Selection_Ins_Grid, _Ins | Get institute list |
| 3 | `_CenterDBOps` | GetCenterListByAuditor_Instt | Frm_COD_Selection_Auditor | Centers assigned to auditor |
| 4 | `_CenterDBOps` | GetCenterListByPAD_Name | Frm_COD_Selection_Auditor_Grid | Search centers |
| 5 | `_CenterDBOps` | GetChildCenterList | Frm_Login_Post | Child center IDs |
| 6 | `_CenterDBOps` | Get_Base_OpenEventId | Frm_Login_Post, ChangeCenter | Audit event check |
| 7 | `_CenterDBOps` | GetCenterDetails_Login | Frm_Generic_Login POST | Center details by ID |
| 8 | `_CenterDBOps` | GetIns_List | GetInstitutes, _Ins_Auditor | All institutes |
| 9 | `_CenterDBOps` | GetCenter_ByInstitute | GetCentres | Centers by institute |
| 10 | `_CenterDBOps` | LogOut | ChangeCenter, Logout, Submit | Log center logout |
| 11 | `_CenterDBOps` | ResumeAudit | Frm_COD_Selection_Auditor_Submit, CheckResumeAudit | Resume returned audit |
| 12 | `_ClientUserDBOps` | GetUserInfo | Frm_Auditor_Login POST | Auditor user lookup |
| 13 | `_ClientUserDBOps` | GetCenterUserInfo | Frm_Login_Post | Center user lookup |
| 14 | `_CODDBOps` | CheckVolumeCenter | Frm_Login_Post | Volume center flag |
| 15 | `_CODDBOps` | GetUnAuditedFinalYears | Frm_Login_Post, ChangeCenter | Unaudited year tracking |
| 16 | `_CODDBOps` | GetCompletedtYearCount | Frm_Login_Post, ChangeCenter | Completed year count |
| 17 | `_AndroidDBOps` | insertUsersDeviceToken | Frm_Login_Post, Frm_Auditor_Login | Android push tokens |
| 18 | `_UserPreferences_DBOps` | GetSelectedScreens_DataView | Frm_Login_Post, Frm_Auditor_Login | Screen preferences |

---

## 7. Session Variables Used

### BASE Session Vars Set During Login (20+ vars)
`_open_User_ID`, `_open_User_Password`, `_open_User_Type`, `_open_User_User_Is_Admin`, `_open_User_Self_Posted`, `_open_User_Is_Central_Store`, `_open_User_PersonnelID`, `_open_User_MainDeptID`, `_open_User_SubDeptID`, `_open_Cen_ID`, `_open_Cen_Rec_ID`, `_open_Cen_ID_Main`, `_open_Cen_Name`, `_open_PAD_No_Main`, `_open_PAD_No`, `_open_Ins_ID`, `_open_Ins_Name`, `_open_Ins_Short`, `_open_Year_ID`, `_open_Year_Name`, `_open_Year_Sdt`, `_open_Year_Edt`, `_open_Trans_DB`, `_open_UID_No`, `_open_Zone_ID`, `_open_Zone_SUB_ID`, `_open_Year_Acc_Type`, `_open_Cen_ID_Child`, `_open_Event_ID`, `_Completed_Year_Count`, `_IsVolumeCenter`, `_IsUnderAudit`, `_prev_Unaudited_YearID`, `_next_Unaudited_YearID`, `_List_Of_FullData_Screen`, `_open_User_Remember`

### ASP.NET Session Keys
- `BASEClass` — `List<BaseModel>` for multi-center
- `CODSelectionInsExportData` — DataTable for grid export

---

## 8. Error Handling

- **Pattern:** Try-catch in every method with `string.Format("<b>Message:</b> {0}", ex.Message)` → JSON response
- **StackTrace/Source:** Commented out in all catch blocks (security-conscious but lost debugging info)
- **Silent failures:** Some catches return `null` (Frm_COD_Selection_Ins_Grid) or empty objects
- **No logging:** No error logging to file/DB — just returns error to client

---

## 9. Dependencies & Cross-References

- **Uses:** `Common_Lib.Common` (BASE), `Common_Lib.Messages`, `Common_Lib.RealTimeService.ClientScreen`
- **Connection mode checks:** `BASE.curr_Db_Conn_Mode(ClientScreen.Start_Select_Center)` — supports Online, Local, TxnsOnline_LocalCore modes
- **Security concern:** `Frm_Login_Post` has `[HttpPost]` commented out — accepts GET with password in URL
- **Multi-center:** System supports multiple center sessions via `List<BaseModel>` keyed by GUID
- **Cookie security:** Uses `HttpOnly` but stores password in cookie value (Frm_Login_Cookies, Frm_Auditor_Cookies)
