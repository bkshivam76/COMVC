# AccountSubmissionController — Functional Specification Document

> **Controller:** `AccountSubmissionController.cs`  
> **Area:** Start  
> **Path:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\AccountSubmissionController.cs`  
> **Inherits:** `BaseController`  
> **Lines:** 573 | **Methods:** 12

---

## 1. Module Overview

Handles **submitting, verifying, and returning accounts** for audit. This is a critical workflow where center users submit their books of accounts, and auditors verify and approve them. Includes **negative cash balance validation** and **unmatched transfer checks** before submission is allowed.

---

## 2. Screens / Forms

### 2.1 Submit Accounts Form — `Frm_Submit_Accounts` (Line 25)
- **Purpose:** Monthly account submission form showing FY dates and last submission info
- **ViewBag:** YearName, OpenYearOfFY, CloseYearofFY, EndDate, lblLastCompletionOn
- **DAL:** `_CenterDBOps.GetAccountsSubmittedPeriod()` → reads CSA_SUBMIT_ON, CSA_SUBMIT_TO

### 2.2 Submit Report Form — `Frm_Submit_Report` GET (Line 232)
- **Purpose:** Report submission form with center grade and verification checkbox
- **Logic:** Grade A/B → verification checkbox disabled (already high quality)
- **DAL:** `_CenterDBOps.GetlatestCenterGrade()`

### 2.3 Submission History — `Frm_Report_Submit_Accounts` (Line 562)
- **Purpose:** View/grid for past account submission records
- **Grid Data:** `_CenterDBOps.GetAccountsSubmissionReport()` (JSON endpoint)

---

## 3. Operations

### 3.1 Pending Submission Alert — `Pending_Accounts_Submission` (Line 50)
- **Purpose:** Dashboard alert if accounts not submitted up to last month
- **Rule:** Only alerts CLIENT ROLE users (not auditors/superusers)
- **DAL:** `_CenterDBOps.GetLatestAccountsSubmittedPeriod()`
- **Logic:** If `LastSubmissionTillDate` (1st of current month - 1 day) > `PrevSubmittedTill`, show alert

### 3.2 Submit Accounts — `Submit_Success` POST (Line 76)
- **Input:** `ToDate`, `PrevSubmittedTill`
- **Business Logic:**
  1. **Negative Cash Balance Check** via `NegativeBalanceCheck()` — blocks submission if cash goes negative
  2. Creates `Param_AddAccountsSubmissionPeriod` with FromDate (FY start) and ToDate
  3. Calls `_CenterDBOps.AddAccountsSubmissionPeriod()`
- **NegativeBalanceCheck (Lines 137-226):**
  - Gets cash balance summary: `_Voucher_DBOps.GetCashBalanceSummary()`
  - Gets all transactions: `_Voucher_DBOps.GetNegativeBalance("00080")`
  - Inserts opening balance row, sorts by date
  - Iterates all transactions calculating running balance
  - If balance goes negative on any date → blocks submission with detailed message

### 3.3 Submit Report — `Frm_Submit_Report` POST (Line 262)
- **Input:** `chk_Verify_ReportSubmit` (bool)
- **Business Logic:**
  1. Calls `_CenterDBOps.SubmitReport()`
  2. If verify checkbox checked → calls `_CenterDBOps.VerifyAudit()`
  3. On success → sends SMS via `_Notifications_DBOps.SendSMS(eventID, "AUDIT_VERIFICATION")`
  4. Without verification → sends SMS with type "REPORT_SUBMISSION"
  5. Returns `logout: true` — user is logged out after submission

### 3.4 Check Submission Restrictions — `Check_Submission_Restrictions` (Line 367)
- **Purpose:** Pre-flight check before allowing report submission
- **Checks (in order):**
  1. Audit transaction period must exist: `_CenterDBOps.GetAuditTxnPeriod()`
  2. Audit period must be valid: `_CenterDBOps.GetAuditPeriod()`
  3. **Negative cash balance** during audit period → block
  4. **Unmatched asset transfers** → block: `_AssetTransfer_DBOps.GetUnmatchedCount_AssetTransfer(from, to)`
  5. **Unmatched internal transfers** → block: `_Internal_Tf_Voucher_DBOps.GetCenterToCentreUnmatchedCount(from, to)`
  6. Special message for institute "00001" (main institute) mentioning TDS and non-cash transfers

### 3.5 Return For Correction — `ReturnForCorrection` (Line 474)
- **Purpose:** Auditor returns submitted accounts back to center for corrections
- **DAL:** `_CenterDBOps.ReturnedForCorrection()`

### 3.6 Verify Audit — `VerifyAudit` (Line 502)
- **Purpose:** Standalone audit verification (separate from Submit Report)
- **DAL:** `_CenterDBOps.VerifyAudit()` → returns eventID
- **SMS:** Sends "AUDIT_VERIFICATION" SMS on success

---

## 4. Business Rules

| # | Rule | Impact |
|---|------|--------|
| 1 | Cannot submit accounts if cash balance is negative on any date | Submission blocked |
| 2 | Cannot submit report if unmatched asset transfer entries exist | Report blocked |
| 3 | Cannot submit report if unmatched internal transfer entries exist | Report blocked |
| 4 | Only CLIENT ROLE users get pending submission alerts | Dashboard alert |
| 5 | Grade A/B centers cannot verify during report submission | Checkbox disabled |
| 6 | User is **logged out** after successful report submission/verification | Forced logout |
| 7 | SMS notification sent on audit verification and report submission | Automated comms |

---

## 5. Dependencies & Cross-References

### DAL Calls
| Object | Method | Purpose |
|--------|--------|---------|
| `_CenterDBOps` | GetAccountsSubmittedPeriod | Last submission date |
| `_CenterDBOps` | GetLatestAccountsSubmittedPeriod | Pending check |
| `_CenterDBOps` | AddAccountsSubmissionPeriod | Submit accounts |
| `_CenterDBOps` | GetlatestCenterGrade | Center grade |
| `_CenterDBOps` | SubmitReport | Submit report |
| `_CenterDBOps` | VerifyAudit | Verify audit (returns eventID) |
| `_CenterDBOps` | ReturnedForCorrection | Return for correction |
| `_CenterDBOps` | GetAuditTxnPeriod, GetAuditPeriod | Audit period validation |
| `_CenterDBOps` | GetAccountsSubmissionReport | Submission history |
| `_Voucher_DBOps` | GetCashBalanceSummary | Cash opening balance |
| `_Voucher_DBOps` | GetNegativeBalance | Cash transactions for balance check |
| `_AssetTransfer_DBOps` | GetUnmatchedCount_AssetTransfer | Unmatched asset transfers |
| `_Internal_Tf_Voucher_DBOps` | GetCenterToCentreUnmatchedCount | Unmatched internal transfers |
| `_Notifications_DBOps` | SendSMS | SMS on audit events |
