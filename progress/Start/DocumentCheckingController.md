# DocumentCheckingController.cs — Method-Level Progress

> **Source:** `e:\Learn\ConnectOneMVC\ConnectOneMVC\Areas\Start\Controllers\DocumentCheckingController.cs`  
> **Area:** Start | **Total Lines:** 194 | **Lines Read:** 194 / 194  
> **Status:** ✅ COMPLETED | **Last Updated:** 2026-03-06  
> **FSD:** [DocumentChecking_VouchingAudit_FSD.md](file:///e:/Learn/ConnectOneMVC/FSD/Start/DocumentChecking_VouchingAudit_FSD.md)

## Method Progress: 11 / 11 completed ✅

| # | Method | Lines | Status | Summary |
|---|--------|-------|--------|---------|
| 1 | DocumentCheckingInfo | 30-37 | ✅ | Main view with Update/Delete rights check |
| 2 | DocumentChecking_Grid | 38-57 | ✅ | Filterable grid: AuditStatus enum, DataScope enum, layout, category/name/status/checkedBy filters |
| 3 | AcceptDocument | 58-88 | ✅ | Mark_Accepted_attachment(AttachID) |
| 4 | UncheckDocument | 89-119 | ✅ | Mark_Unchecked_attachment(AttachID) |
| 5 | RejectDocument | 120-151 | ✅ | Mark_Rejected_attachment(AttachID, Reason) — reason sanitized for SQL safety |
| 6 | Frm_DC_RejectReason | 152-155 | ✅ | Popup view for rejection reason input |
| 7 | LookUp_GetDocument_Category | 156-160 | ✅ | DevExtreme DataSource: document categories |
| 8 | LookUp_GetDocument_Name | 161-169 | ✅ | DevExtreme DataSource: document names by category |
| 9 | LookUp_GetCheckedBy | 170-174 | ✅ | DevExtreme DataSource: auditors/superusers list |
| 10 | SessionClear | 175-178 | ✅ | Clears _DocumentChecking session keys |
| 11 | GetFileMimeType | 179-186 | ✅ | Returns MIME type for file preview |

## DAL Calls: `_Audit_DBOps.GetDocumentChecking()`, `_Attachments_DBOps` (Mark_Accepted/Rejected/Unchecked, GetDocument_Categories/Names), `_ClientUserDBOps.GetAuditors_Superusers()`
## Failed Methods: None ✅
