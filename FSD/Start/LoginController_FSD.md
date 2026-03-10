# LoginController — Functional Specification Document

> **Controller:** `LoginController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\LoginController.cs`  
> **Inherits:** `BaseController`  
> **Lines:** 1111 | **Methods:** 27

---

## 1. Module Overview

The **LoginController** handles all authentication, center selection, and session initialization for the ConnectOne application. It supports three distinct login flows:

1. **COD Certificate Login** — User enters a center certificate number, selects an institute/center from a grid, then logs in with username/password
2. **Auditor Login** — Auditors/SuperUsers log in with credentials, then select from assigned centers
3. **Generic Login** — Direct login via Center ID (used for SpARC and URL-based access)

All three flows result in a fully initialized session with 20+ BASE session variables set.

---

## 2. Screens / Forms

### 2.1 COD Selection Screen — `Frm_COD_Selection` (Line 83)
- **Route:** `GET /Start/Login/Frm_COD_Selection`
- **Purpose:** Entry point for certificate-based center selection
- **Parameters:** `deviceToken` (optional, for Android), `androidID` (optional), `RedirectToAndroid` (default "NO")
- **View:** Returns `Frm_COD_Selection` view with deviceToken as model

### 2.2 COD Selection Form — `Frm_COD_Selection_form` (Line 89)
- **Route:** `GET`
- **Purpose:** Returns the COD selection form partial view
- **View:** `Frm_COD_Selection_form`

### 2.3 Login Form — `Frm_Login` (Line 685)
- **Route:** `POST`
- **Purpose:** After center/institute selection, renders the username/password login form
- **Parameters:** 19 center detail strings (INS_NAME, CEN_NAME_X, CEN_ID, COD_YEAR_ID, etc.)
- **Business Logic:**
  - Reads remember-me cookie (`Frm_Login_Cookies`) to pre-populate credentials
  - Converts date strings to `dd-MM-yyyy` format
  - Populates `Center_Ins_List` model with all center details
- **View:** Returns `Frm_Login` view with `Center_Ins_List` model

### 2.4 Auditor Login Screen — `Frm_Auditor_Login` GET (Line 296)
- **Route:** `GET /Start/Login/Frm_Auditor_Login`
- **Purpose:** Auditor/SuperUser login form
- **Business Logic:**
  - Reads `Frm_Auditor_Cookies` for remember-me pre-population
  - Sets `_open_User_Remember` flag
- **Model:** `Auditor_Login` (fields: `Txt_User`, `Txt_Pass`, `DEVICE_TOKEN`, `ANDROID_ID`, `RedirectToAndroid`)

### 2.5 Generic Login Screen — `Frm_Generic_Login` GET (Line 1048)
- **Route:** `GET /Start/Login/Frm_Generic_Login?CenID=&InsID=`
- **Purpose:** Direct login page via center ID (used by SpARC route `/sparc/{CenID}`)
- **View:** Passes `InsID` and `CenID` via `ViewData`

---

## 3. Operations

### 3.1 Center Certificate Lookup — `Frm_COD_Selection_Ins_Check` (Line 124)
- **Type:** AJAX GET
- **Input:** `certificate_number` (string)
- **Validations:**
  - Certificate number must not be empty
  - Must be numeric (e.g., "1780" for ES\SC\ORI\1780\10)
  - DB connection mode must be Online or TxnsOnline
- **DAL Call:** `BASE._CenterDBOps.GetSelectCentreList(certificate_number)`
- **Output:** Partial view `frm_COD_Selection_Ins_Check` with `SelectedCenterDetails` model, or JSON error
- **Error Messages:** "Please enter certificate no.", "Please enter Valid Center Certificate No..!", "center not created yet or server down"

### 3.2 Institute List Load — `Frm_COD_Selection_Ins` (Line 193)
- **Type:** AJAX GET
- **Input:** `certificate_number`
- **Validations:** Same as 3.1 (empty, numeric check)
- **DAL Calls:**
  - `BASE._CenterDBOps.GetSelectCentreList(certificate_number)`
  - `BASE._CenterDBOps.GetCen_Ins_List(certificate_number)` → stored in `Session["CODSelectionInsExportData"]`
- **Output:** Partial view `frm_COD_Selection_Ins` with center details, or JSON error with `Return_Json_Param`

### 3.3 Institute Grid Data — `Frm_COD_Selection_Ins_Grid` (Line 96)
- **Type:** Grid callback
- **DAL Call:** `BASE._CenterDBOps.GetCen_Ins_List(certificate_no)` → caches in `Session["CODSelectionInsExportData"]`
- **Output:** DevExpress GridView

### 3.4 Grid Row Selection — `CODSelectionInsCustomDataAction` (Line 112)
- **Type:** DevExpress custom data callback
- **Input:** `key` (CEN_ID)
- **Logic:** Finds matching row from session DataTable, concatenates all fields with `![` separator
- **Fields returned:** INS_NAME, CEN_NAME_X, CEN_UID, CEN_PAD_NO, CEN_ACC_TYPE_ID, CEN_ID, COD_YEAR_EDT, COD_YEAR_ID, COD_YEAR_NAME, CEN_REC_ID, COD_YEAR_SDT, INS_ID, INS_SHORT, IS_VOLUME

### 3.5 Main Login (POST) — `Frm_Login_Post` (Line 470)
- **Type:** POST (despite commented `[HttpPost]` attribute!)
- **Input:** `Center_Ins_List` model, `UserName`, `Password`, `SessionGUID`, `chKRememberMe`
- **Business Logic (critical):**
  1. Creates session GUID and adds to `BASEClass` list (multi-center support)
  2. Fetches user info: `BASE._ClientUserDBOps.GetCenterUserInfo(CEN_ID, UserName)`
  3. Remember-me cookie: `Frm_Login_Cookies` with 365-day expiry, HttpOnly
  4. Sets 20+ BASE session variables:
     - `_open_User_ID`, `_open_User_Password`, `_open_User_Type`
     - `_open_User_User_Is_Admin`, `_open_User_Self_Posted`, `_open_User_Is_Central_Store`
     - `_open_User_PersonnelID`, `_open_User_MainDeptID`, `_open_User_SubDeptID`
     - `_open_Cen_ID`, `_open_Cen_Rec_ID`, `_open_Cen_ID_Main`, `_open_Cen_Name`
     - `_open_PAD_No_Main`, `_open_PAD_No`, `_open_Ins_ID`, `_open_Ins_Name`, `_open_Ins_Short`
     - `_open_Year_ID`, `_open_Year_Name`, `_open_Year_Sdt`, `_open_Year_Edt`
     - `_open_Trans_DB` (format: `COD_{InsID}_{CenID}_{YearID}.COT`)
     - `_open_UID_No`, `_open_Zone_ID`, `_open_Zone_SUB_ID`, `_open_Year_Acc_Type`
  5. Calls `BASE.OnChangeCenter_Year()` to initialize year-related data
  6. Registers Android device token: `BASE._AndroidDBOps.insertUsersDeviceToken()`
  7. Loads user screen preferences (mobile vs desktop): `BASE._UserPreferences_DBOps.GetSelectedScreens_DataView()`
  8. Checks volume center: `BASE._CODDBOps.CheckVolumeCenter()`
  9. Checks audit status: `BASE._CenterDBOps.Get_Base_OpenEventId()`
  10. Gets unaudited years: `BASE._CODDBOps.GetUnAuditedFinalYears()`
  11. Gets completed year count: `BASE._CODDBOps.GetCompletedtYearCount()`
  12. Gets child centers: `BASE._CenterDBOps.GetChildCenterList(CEN_PAD_NO_MAIN)`
  13. Password check: `BASE.Encrypt(Password.ToUpper()) == actualPassword.ToUpper()`
- **Output:** JSON `{result: true/false, Guid, deviceToken, androidID, RedirectToAndroid}`
- **Error Messages:** "Sorry! You are denied login by Madhuban...", "Incorrect Username (Superusers try Auditor Login)", "Incorrect Password"

### 3.6 Auditor Login (POST) — `Frm_Auditor_Login` POST (Line 316)
- **Type:** `[HttpPost]`
- **Input:** `Auditor_Login` model, `SessionGUID`, `chk_Remember`
- **Business Logic:**
  1. Remember-me cookie handling with FormsAuthentication ticket (365-day expiry)
  2. Fetches user: `BASE._ClientUserDBOps.GetUserInfo(Txt_User)`
  3. **Role validation:** Must be `Auditor` or `SuperUser` type
  4. Password check: `BASE.Encrypt(Txt_Pass.ToUpper()) == actualPassword.ToUpper()`
  5. Sets BASE vars: `_open_User_ID`, `_open_User_Password`, `_open_User_Type`, `_open_User_User_Is_Admin`, `_open_User_Is_Central_Store`, `_open_User_PersonnelID`, `_open_User_MainDeptID`, `_open_User_SubDeptID`
  6. Android device token registration
  7. Loads user preferences (mobile/desktop screens)
  8. Multi-center session via BASEClass GUID list
- **Error Messages:** "Invalid User ID", "Invalid Auditor ID", "Incorrect Password"

### 3.7 Generic Login (POST) — `Frm_Generic_Login` POST (Line 1055)
- **Type:** `[HttpPost]`
- **Input:** `cenid`, `pwd`, `username`
- **Business Logic:**
  1. Fetches center details: `BASE._CenterDBOps.GetCenterDetails_Login(cenid)`
  2. Populates `Center_Ins_List` from DB result
  3. **Delegates to** `Frm_Login_Post()` — reuses the full login flow
- **Fields from DB:** CEN_REC_ID, main_cen_id, CEN_NAME, main_cen_pad_no, CEN_PAD_NO, INS_ID, INS_NAME, INS_SHORT, COD_YEAR_ID, COD_YEAR_NAME, COD_YEAR_SDT, COD_YEAR_EDT, CEN_UID, MAIN_CEN_ZONE_ID, MAIN_CEN_ZONE_SUB_ID, CEN_ACC_TYPE_ID

### 3.8 Auditor Center Selection — `Frm_COD_Selection_Auditor` (Line 756)
- **Purpose:** After auditor login, shows centers assigned to auditor
- **DAL Call:** `BASE._CenterDBOps.GetCenterListByAuditor_Instt(UserID, InsID)`
- **Features:** Supports institute filtering, popup display

### 3.9 Auditor Center Grid — `Frm_COD_Selection_Auditor_Grid` (Line 790)
- **Type:** Grid callback with refresh/search
- **DAL Calls:**
  - `GetCenterListByAuditor_Instt()` — list by institute
  - `GetCenterListByPAD_Name()` — search by center name/PAD

### 3.10 Auditor Center Submit — `Frm_COD_Selection_Auditor_Submit` (Line 807)
- **Purpose:** Auditor selects a center to work on
- **Business Logic:**
  - **Audit status check:** If center status is "Returned" and user confirms, calls `BASE._CenterDBOps.ResumeAudit(CEN_ID, COD_YEAR_ID)`
  - Non-SuperUsers cannot login without resuming audit
  - Calls `LogOut()` on previous center, then `ChangeCenter()` to switch
- **Error Message:** "Sorry! Not allowed to Login Center without Resuming Audit!"

### 3.11 Change Center — `ChangeCenter` (Line 851)
- **Purpose:** Switches the current working center (resets all session state)
- **Parameters:** 18 parameters (CenID, CenName, YearID, YearName, dates, UID, PADNo, ZoneID, etc.)
- **Business Logic:**
  1. Saves current center/year to restore on error
  2. Calls `BASE._CenterDBOps.LogOut()` (previous center)
  3. Sets all center/year/institute BASE vars (same 20+ as in login)
  4. Calls `BASE.OnChangeCenter_Year()`
  5. Gets unaudited years and completed year count
  6. Gets event ID: `BASE._CenterDBOps.Get_Base_OpenEventId()`
  7. Checks `_IsUnderAudit` flag
  8. Clears session/rights dictionaries
  9. **SuperUser timeout:** Session timeout extended to 4320 min (3 days)
  10. On error: **rollback** — restores previous cenID, yearID, CompletedYearCount

### 3.12 Logout — `Logout` (Line 976)
- **Logic:**
  1. Determines redirect path (Generic login if came from Home, otherwise default)
  2. Finds current BASE from BASEClass list by SessionGUID
  3. Calls `BASE._CenterDBOps.LogOut()`
  4. Removes current BASE from list
  5. Clears session: `Session.RemoveAll(); Session.Clear()`
  6. Re-saves remaining BASEClass entries (multi-center support)
  7. Redirects to login page

### 3.13 Utility Methods
- **Frm_COD_Selection_Ins_Auditor** (Line 443): Loads institute list for auditor center selection
- **Frm_COD_Selection_Ins_Auditor_Grid** (Line 454): Institute grid with refresh support
- **SessionClear_SelectIns** (Line 463): Clears `_ChangeInstitute` session keys
- **clearSessionVariable** (Line 965): Removes CenID, YearID, CompletedYearCount from session dict
- **CheckResumeAudit** (Line 971): Wraps `BASE._CenterDBOps.ResumeAudit()` call
- **UnloadCookie** (Line 431): Removes auditor cookie
- **GetInstitutes** (Line 1101): JSON endpoint for DevExtreme — returns institute list
- **GetCentres** (Line 1105): JSON endpoint for DevExtreme — returns centers by institute

---

## 4. Business Rules & Validations

| # | Rule | Method | Line |
|---|------|--------|------|
| 1 | Certificate number must be non-empty and numeric | Frm_COD_Selection_Ins_Check | 137-145 |
| 2 | DB connection must be Online or TxnsOnline for center selection | Frm_COD_Selection_Ins_Check | 133-135 |
| 3 | Auditor login requires role = "Auditor" or "SuperUser" | Frm_Auditor_Login POST | 360-364 |
| 4 | Password compared as encrypted uppercase: `BASE.Encrypt(pwd.ToUpper())` | Frm_Login_Post | 661 |
| 5 | "Returned" audit status blocks non-SuperUser login unless they resume audit | Frm_COD_Selection_Auditor_Submit | 811-828 |
| 6 | SuperUser gets 3-day session timeout (4320 min) | ChangeCenter | 940-943 |
| 7 | Remember-me cookie expires in 365 days, HttpOnly | Frm_Login_Post | 506-513 |
| 8 | Multi-center support via GUID-based BASEClass session list | Frm_Login_Post | 477-485 |
| 9 | Transaction DB name format: `COD_{InsID}_{CenID}_{YearID}.COT` | Frm_Login_Post | 549 |
| 10 | Logout restores remaining centers in multi-center mode | Logout | 1032 |

---

## 5. Data Fields

### Session Properties (on LoginController class)
| Field | Type | Session Key |
|-------|------|-------------|
| InstituteData | DataTable | InstituteData_ChangeInstitute |
| CenterData | DataTable | CenterData_ChangeInstitute |
| cenID | int? | CenID_ChangeInstitute |
| yearID | int? | YearID_ChangeInstitute |
| CompletedYearCount | int? | CompletedYearCount_ChangeInstitute |

### Center_Ins_List Model Fields
CEN_NAME_X, CEN_UID, CEN_PAD_NO, CEN_ACC_TYPE_ID, CEN_ID, COD_YEAR_EDT, COD_YEAR_ID, COD_YEAR_NAME, CEN_REC_ID, COD_YEAR_SDT, INS_ID, INS_NAME, INS_SHORT, CEN_PAD_NO_MAIN, CEN_NAME, CEN_ID_MAIN, CEN_ZONE_ID, CEN_ZONE_SUB_ID, IS_VOLUME, UserName, Password, DEVICE_TOKEN, ANDROID_ID, RedirectToAndroid

---

## 6. Dependencies & Cross-References

### DAL Calls (D0000.Common via BASE)
| Object | Method | Used In |
|--------|--------|---------|
| `_CenterDBOps` | GetSelectCentreList, GetCen_Ins_List, GetCenterListByAuditor_Instt, GetCenterListByPAD_Name, GetChildCenterList, Get_Base_OpenEventId, GetCenterDetails_Login, GetIns_List, GetCenter_ByInstitute, LogOut, ResumeAudit | Multiple methods |
| `_ClientUserDBOps` | GetUserInfo, GetCenterUserInfo | Auditor login, Main login |
| `_CODDBOps` | CheckVolumeCenter, GetUnAuditedFinalYears, GetCompletedtYearCount | Login, ChangeCenter |
| `_AndroidDBOps` | insertUsersDeviceToken | Login (both flows) |
| `_UserPreferences_DBOps` | GetSelectedScreens_DataView | Login (both flows) |
