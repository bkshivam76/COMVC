# AuditRegistrationController — Technical Design Document

> **Controller:** `AuditRegistrationController.cs`  
> **Area:** Start | **Lines:** 332

## Action Methods
| # | Method | Lines | Purpose |
|---|--------|-------|---------|
| 1 | Index | 13-16 | Empty view |
| 2 | RegisterForAudit | 18-21 | [CheckLogin] Registration view |
| 3 | SupportInfoExists | 23-29 | Check core data via _CoreDBOps.GetList() |
| 4 | CheckDocumentsPending | 30-36 | Check pending attachments via _Audit_DBOps.GetAllPendingAttachments() |
| 5 | MarkAsRegisteredForAudit | 37-68 | Register + SMS("AUDIT_REGISTRATION") + confirmation flow |
| 6 | CancelAuditRegistration | 69-100 | Cancel + SMS("AUDIT_REGISTRATION_CANCELLED") + confirmation flow |
| 7 | Check_Restrictions | 101-330 | 8-step pre-flight: cash balance, remarks, asset/internal transfers, account analysis, dynamic restrictions, attachments |

## DAL Calls
`_CoreDBOps`: GetList | `_Audit_DBOps`: GetAllPendingAttachments, RegisterForAudit, CancelAuditRegistration | `_Voucher_DBOps`: GetCashBalanceSummary, GetNegativeBalance | `_Action_Items_DBOps`: GetPendingCentreRemarkCount, GetAuditExceptions | `_AssetTransfer_DBOps`: GetUnmatchedCount_AssetTransfer | `_Internal_Tf_Voucher_DBOps`: GetCenterToCentreUnmatchedCount | `_ClientUserDBOps`: GetCenterTasks | `_Notifications_DBOps`: SendSMS | `DbOperations.SharedVariables`: GetDynaicClientRestriction

## Cross-References
- Negative balance check mirrors [AccountSubmissionController](file:///e:/Learn/ConnectOneMVC/TDD/Start/AccountSubmissionController_TDD.md)
- Transfer matching checks mirror [AccountSubmissionController > Check_Submission_Restrictions](file:///e:/Learn/ConnectOneMVC/FSD/Start/AccountSubmissionController_FSD.md)
