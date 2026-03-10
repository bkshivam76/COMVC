# AccountSubmissionController — Technical Design Document

> **Controller:** `AccountSubmissionController.cs`  
> **Area:** Start | **Lines:** 573

---

## 1. Action Methods
| # | Method | HTTP | Lines | Purpose |
|---|--------|------|-------|---------|
| 1 | Index | GET | 18-22 | Empty index view |
| 2 | Frm_Submit_Accounts | GET | 25-49 | Load submission form with last submission date |
| 3 | Pending_Accounts_Submission | GET | 50-74 | Alert if accounts overdue (CLIENT ROLE only) |
| 4 | Submit_Success | POST | 76-128 | Submit accounts with negative balance check |
| 5 | IsDate | private | 129-136 | DateTime.TryParse helper |
| 6 | NegativeBalanceCheck | private | 137-226 | Running cash balance calculation — blocks if negative |
| 7 | Frm_Submit_Report | GET | 232-260 | Load report submission form with center grade |
| 8 | Frm_Submit_Report | POST | 262-366 | Submit report + optional verify + SMS notification + logout |
| 9 | Check_Submission_Restrictions | GET | 367-473 | Pre-flight: audit period, cash balance, unmatched transfers |
| 10 | ReturnForCorrection | GET | 474-501 | Return accounts for correction |
| 11 | VerifyAudit | GET | 502-537 | Standalone audit verification + SMS |
| 12 | SessionClear | - | 557-560 | Clear submission history grid data |
| 13 | Frm_Report_Submit_Accounts | GET | 562-565 | Submission history view |
| 14 | Frm_Report_Submit_Accounts_Grid | GET | 566-569 | Submission history JSON grid data |

## 2. Data Flow
```
Account Submission: Frm_Submit_Accounts → NegativeBalanceCheck → AddAccountsSubmissionPeriod
Report Submission: Check_Submission_Restrictions (pre-flight)
  → GetAuditTxnPeriod → GetAuditPeriod → NegativeBalance → Unmatched checks
  → Frm_Submit_Report POST → SubmitReport → VerifyAudit → SendSMS → Logout
Return: ReturnForCorrection → ReturnedForCorrection()
```

## 3. DAL Calls
`_CenterDBOps`: GetAccountsSubmittedPeriod, GetLatestAccountsSubmittedPeriod, AddAccountsSubmissionPeriod, GetlatestCenterGrade, SubmitReport, VerifyAudit, ReturnedForCorrection, GetAuditTxnPeriod, GetAuditPeriod, GetAccountsSubmissionReport  
`_Voucher_DBOps`: GetCashBalanceSummary, GetNegativeBalance  
`_AssetTransfer_DBOps`: GetUnmatchedCount_AssetTransfer  
`_Internal_Tf_Voucher_DBOps`: GetCenterToCentreUnmatchedCount  
`_Notifications_DBOps`: SendSMS

## 4. Models
`AccountSubmissionModel` (Start.Models) — has `PrevSubmittedTill_SubmitAccounts` (DateTime)  
`Return_Json_Param` — standard JSON response  
`Param_AddAccountsSubmissionPeriod` (Common_Lib) — FromDate, ToDate, PrevSubmittedTill

## 5. Notes
- NegativeBalanceCheck uses account head "00080" (Cash account)
- Institute "00001" has special transfer message (mentions TDS and non-cash transfers)
- `Frm_Submit_Report` POST returns `logout: true` — forces client-side logout
- Running balance calculated with `Math.Round(..., 2)` for precision
