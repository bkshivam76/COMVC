# BankBalanceCheckingController — FSD + TDD

> **Controller:** `BankBalanceCheckingController.cs`  
> **Area:** Start | **Lines:** 203 | **Methods:** 11 | **Auth:** `[CheckLogin]` class-level

---

## FSD

### Module Overview
Manages **bank account balance verification** — auditors verify passbook balances against system records. Supports vouching mode (auditor view) with document mapping and additional info.

### Screens
- **Bank Checking Info** — `Frm_BankChecking_Info` (Line 37): Main form with grid of bank accounts requiring checking. Loads `Get_BankAccountCheckingList("PENDING")`. Sets auto-vouching mode for auditors/superusers when center is under audit.
- **Bank Checking Grid** — `Frm_BankChecking_Info_Grid` (Line 59): Grid callback with layout persistence, show/hide columns, checking status filter (PENDING/ALL), and row key focus
- **Detail Grid** — `Frm_BankChecking_Info_DetailGrid` (Line 85): Nested detail with document mapping. In vouching mode, includes additional info grid.
- **Left Pane** — `LeftPaneContent` (Line 107): Left panel partial for split view

### Operations
- **SaveBankBalance** POST (Line 140): Saves passbook balance, account status, and last txn date via `_BankAccountDBOps.InsertBankPassbookBalance()`. Updates session data in-memory.
- **User Rights** — `BankChecking_User_Rights` (Line 180): Sets Add/Update/View/Delete/Export/List + LockUnlock + Attachment rights

### Action Rights Checked
- `Special_Groupings`, `Manage_Remarks`, `Lock_Unlock` via `BASE.CheckActionRights()`

### DAL Calls
| Object | Method |
|--------|--------|
| `_BankAccountDBOps` | Get_BankAccountCheckingList, InsertBankPassbookBalance |
| `_Audit_DBOps` | GetDocumentMapping, GetDocumentMapping_With_Additional_Info |

### Session Properties
`BankCheckingInfo_GridData` (DataTable), `BankCheckingInfo_DetailGridData` (List<Return_GetDocumentMapping>), `BankChecking_AdditionalInfoGrid` (List<Return_GetEntry_AdditionalInfo>)
