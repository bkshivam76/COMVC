# AuditRegistrationController — Functional Specification Document

> **Controller:** `AuditRegistrationController.cs`  
> **Area:** Start | **Lines:** 332 | **Methods:** 7

---

## 1. Module Overview

Handles **registering a center for audit** and **cancelling audit registration**. Includes an extensive 8-step pre-flight restriction check with **bilingual (English + Hindi)** error messages.

---

## 2. Operations

### 2.1 Register For Audit View — `RegisterForAudit` (Line 18)
- `[CheckLogin]` — Returns the register-for-audit view

### 2.2 Support Info Check — `SupportInfoExists` (Line 23)
- **DAL:** `BASE._CoreDBOps.GetList()` — checks if core support data exists

### 2.3 Check Pending Documents — `CheckDocumentsPending` (Line 30)
- **DAL:** `BASE._Audit_DBOps.GetAllPendingAttachments()` — checks for pending document attachments

### 2.4 Mark As Registered — `MarkAsRegisteredForAudit` (Line 37)
- **Input:** `confirmationTaken` (bool, default false)
- **DAL:** `BASE._Audit_DBOps.RegisterForAudit(confirmationTaken)` → returns DataTable with [Message, Success, EventID]
- **On success:** Sends SMS via `_Notifications_DBOps.SendSMS(eventID, "AUDIT_REGISTRATION")`
- **Flow:** If `success == false` → returns `takeConfirmation: true` for user confirmation dialog

### 2.5 Cancel Registration — `CancelAuditRegistration` (Line 69)
- Same pattern as register — calls `BASE._Audit_DBOps.CancelAuditRegistration(confirmationTaken)`
- **SMS type:** `"AUDIT_REGISTRATION_CANCELLED"`

### 2.6 Pre-Flight Restriction Checks — `Check_Restrictions` (Line 101)
**8 sequential checks (all must pass to allow registration):**

| # | Check | DAL Method | Blocker |
|---|-------|------------|---------|
| 1 | Negative cash balance | `_Voucher_DBOps.GetCashBalanceSummary` + `GetNegativeBalance("00080")` | Running balance < 0 |
| 2 | Pending centre remarks | `_Action_Items_DBOps.GetPendingCentreRemarkCount()` | Count > 0 |
| 3 | Unmatched asset transfers | `_AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer()` | Count > 0 |
| 4 | Unmatched internal transfers | `_Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount()` | Count > 0 (special message for INS_ID "00001") |
| 5 | Account Analysis blocking | `_ClientUserDBOps.GetCenterTasks()` → check "Bypass_Account_Analysis_Audit_Registration_Block" | If NOT bypassed, `_Action_Items_DBOps.GetAuditExceptions(true)` checks for uncorrected exceptions |
| 6 | Dynamic client restrictions | `DbOperations.SharedVariables.GetDynaicClientRestriction()` | Non-empty restriction string |
| 7 | Rejected attachments | `_Audit_DBOps.GetAllPendingAttachments()` → Tables[1] | Rejected count > 0 |
| 8 | Missing attachments | Same as #7 | Pending entries with no document or reason |

> 📎 **Cross-reference:** Negative balance check logic is identical to [AccountSubmissionController FSD > NegativeBalanceCheck](file:///e:/Learn/ConnectOneMVC/FSD/Start/AccountSubmissionController_FSD.md)

---

## 3. Business Rules

| # | Rule |
|---|------|
| 1 | All 8 restriction checks must pass before audit registration |
| 2 | SMS sent on both registration and cancellation |
| 3 | Confirmation dialog flow for uncertain states |
| 4 | Bilingual messages (English + Hindi) throughout |
| 5 | Account Analysis blocking can be bypassed via center task "Bypass_Account_Analysis_Audit_Registration_Block" |
