# LoginController.cs — Method-Level Progress

> **Source:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\LoginController.cs`  
> **Area:** Start | **Total Lines:** 1111 | **Lines Read:** 1111 / 1111  
> **Status:** ✅ COMPLETED  
> **Last Updated:** 2026-03-06  
> **FSD:** [LoginController_FSD.md](file:///e:/Learn/ConnectOneMVC/FSD/Start/LoginController_FSD.md)  
> **TDD:** [LoginController_TDD.md](file:///e:/Learn/ConnectOneMVC/TDD/Start/LoginController_TDD.md)

---

## Method Progress: 27 / 27 completed ✅

| # | Method | Lines | Size | Status | Summary |
|---|--------|-------|------|--------|---------|
| 1 | Frm_Login_Params (inner class) | 51-72 | 22 | ✅ | Inner class with 18 string fields (INS_NAME, CEN_NAME_X, CEN_ID, etc.) — appears unused/legacy |
| 2 | LoginController() constructor | 76-79 | 4 | ✅ | Empty constructor with commented-out Programming_Mode call |
| 3 | Frm_COD_Selection | 83-88 | 6 | ✅ | GET: Returns COD selection view with deviceToken, androidID, RedirectToAndroid |
| 4 | Frm_COD_Selection_form | 89-92 | 4 | ✅ | GET: Returns empty COD selection form view |
| 5 | Frm_COD_Selection_Ins_Grid | 96-111 | 16 | ✅ | Grid callback: loads institute list via GetCen_Ins_List, caches in session |
| 6 | CODSelectionInsCustomDataAction | 112-123 | 12 | ✅ | Custom data callback: returns selected row fields concatenated with `![` separator |
| 7 | Frm_COD_Selection_Ins_Check | 124-192 | 69 | ✅ | AJAX: validates certificate (non-empty, numeric), calls GetSelectCentreList, returns PartialView or JSON error |
| 8 | Frm_COD_Selection_Ins | 193-291 | 99 | ✅ | AJAX: validates certificate, loads center+institute lists, returns PartialView with grid |
| 9 | Frm_Auditor_Login (GET) | 296-314 | 19 | ✅ | GET: Auditor login form with cookie pre-fill for remember-me |
| 10 | Frm_Auditor_Login (POST) | 316-429 | 114 | ✅ | POST: validates credentials (Auditor/SuperUser role only), encrypts password, sets 8+ BASE vars, registers Android token, loads screen preferences, multi-center GUID session |
| 11 | UnloadCookie | 431-439 | 9 | ✅ | GET: removes Frm_Auditor_Cookies by setting expiry to -1 day |
| 12 | Frm_COD_Selection_Ins_Auditor | 443-453 | 11 | ✅ | GET: loads institute list for auditor via GetIns_List |
| 13 | Frm_COD_Selection_Ins_Auditor_Grid | 454-462 | 9 | ✅ | Grid callback: refreshes institute list |
| 14 | SessionClear_SelectIns | 463-466 | 4 | ✅ | Clears _ChangeInstitute session keys via ClearBaseSession |
| 15 | Frm_Login_Post | 470-683 | 214 | ✅ | Main login: validates user via GetCenterUserInfo, sets 20+ BASE session vars, remember-me cookie (365 days), Android token, screen prefs, volume/audit checks, child centers, password check via BASE.Encrypt(). Note: [HttpPost] is COMMENTED OUT |
| 16 | Frm_Login | 685-751 | 67 | ✅ | POST: receives 19 center params, reads login cookie, populates Center_Ins_List model, returns login form view |
| 17 | Frm_COD_Selection_Auditor | 756-789 | 34 | ✅ | GET: loads centers assigned to auditor by institute via GetCenterListByAuditor_Instt |
| 18 | Frm_COD_Selection_Auditor_Grid | 790-806 | 17 | ✅ | Grid callback: refresh/search centers by PAD name or auditor institute |
| 19 | Frm_COD_Selection_Auditor_Submit | 807-849 | 43 | ✅ | Submit: checks audit status (Returned → ResumeAudit), blocks non-SuperUser without resume, calls LogOut + ChangeCenter |
| 20 | ChangeCenter | 851-963 | 113 | ✅ | Switches center: LogOut prev, sets 20+ BASE vars, OnChangeCenter_Year, unaudited years, event ID, audit check, clears dictionaries, SuperUser gets 4320 min timeout. On error: rollback to previous center |
| 21 | clearSessionVariable | 965-970 | 6 | ✅ | Removes CenID, YearID, CompletedYearCount from session dictionary |
| 22 | CheckResumeAudit | 971-974 | 4 | ✅ | Wraps BASE._CenterDBOps.ResumeAudit() call |
| 23 | Logout | 976-1045 | 70 | ✅ | Determines redirect (Generic if from Home), finds BASE by GUID, calls LogOut, removes from BASEClass list, clears session, redirects |
| 24 | Frm_Generic_Login (GET) | 1048-1054 | 7 | ✅ | GET: generic login view with InsID/CenID via ViewData |
| 25 | Frm_Generic_Login (POST) | 1055-1100 | 46 | ✅ | POST: fetches center details via GetCenterDetails_Login(cenid), delegates to Frm_Login_Post |
| 26 | GetInstitutes | 1101-1104 | 4 | ✅ | DevExtreme JSON endpoint: returns all institutes |
| 27 | GetCentres | 1105-1108 | 4 | ✅ | DevExtreme JSON endpoint: returns centers by InsID |

---

## DAL / Service Calls Traced

| # | Called From Method | External Method | Object | Status |
|---|-------------------|-----------------|--------|--------|
| 1 | Frm_COD_Selection_Ins_Check | GetSelectCentreList | _CenterDBOps | ✅ Identified |
| 2 | Frm_COD_Selection_Ins_Grid | GetCen_Ins_List | _CenterDBOps | ✅ Identified |
| 3 | Frm_COD_Selection_Ins | GetSelectCentreList + GetCen_Ins_List | _CenterDBOps | ✅ Identified |
| 4 | Frm_Auditor_Login POST | GetUserInfo | _ClientUserDBOps | ✅ Identified |
| 5 | Frm_Login_Post | GetCenterUserInfo | _ClientUserDBOps | ✅ Identified |
| 6 | Frm_Login_Post | CheckVolumeCenter | _CODDBOps | ✅ Identified |
| 7 | Frm_Login_Post | GetUnAuditedFinalYears | _CODDBOps | ✅ Identified |
| 8 | Frm_Login_Post | GetCompletedtYearCount | _CODDBOps | ✅ Identified |
| 9 | Frm_Login_Post | Get_Base_OpenEventId | _CenterDBOps | ✅ Identified |
| 10 | Frm_Login_Post | GetChildCenterList | _CenterDBOps | ✅ Identified |
| 11 | Frm_Login_Post | insertUsersDeviceToken | _AndroidDBOps | ✅ Identified |
| 12 | Frm_Login_Post | GetSelectedScreens_DataView | _UserPreferences_DBOps | ✅ Identified |
| 13 | ChangeCenter | LogOut + OnChangeCenter_Year + GetUnAuditedFinalYears + GetCompletedtYearCount + Get_Base_OpenEventId | Multiple | ✅ Identified |
| 14 | Frm_COD_Selection_Auditor | GetCenterListByAuditor_Instt | _CenterDBOps | ✅ Identified |
| 15 | Frm_COD_Selection_Auditor_Grid | GetCenterListByPAD_Name | _CenterDBOps | ✅ Identified |
| 16 | Frm_COD_Selection_Auditor_Submit | ResumeAudit + LogOut | _CenterDBOps | ✅ Identified |
| 17 | Frm_Generic_Login POST | GetCenterDetails_Login | _CenterDBOps | ✅ Identified |
| 18 | GetInstitutes | GetIns_List | _CenterDBOps | ✅ Identified |
| 19 | GetCentres | GetCenter_ByInstitute | _CenterDBOps | ✅ Identified |

---

## Failed Methods: None ✅

## Notes
- `Frm_Login_Post` has `[HttpPost]` attribute commented out — security concern (password in URL)
- `Frm_Login_Params` inner class appears unused — legacy code
- Password stored in cookies (Frm_Login_Cookies, Frm_Auditor_Cookies) — security concern
- Multi-center support via GUID-based `List<BaseModel>` in session
- Commented-out code for Local DB mode throughout — suggests migration from desktop to web app
