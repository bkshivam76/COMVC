# ConnectOneMVC — FSD/TDD Progress Tracker

> **Purpose:** Resume from exact METHOD without re-reading code. Read this file FIRST in every new session.  
> **Last Updated:** 2026-03-06  
> **References:** `codebase_context.md` (architecture), per-controller files in `progress/` folder

---

## Resume Instructions

1. Read THIS file → find first ⏳/🔄 Area
2. Find first ⏳/🔄 controller in that Area
3. Open that controller's **progress file** (linked below)
4. Find first ⏳/🔄 method → restart from its **start line** (full method, not mid-line)
5. Do NOT re-read ✅ methods — summaries are in the progress file
6. **Failed reads:** Retry 3 times, then mark ❌ with reason and move to next method

---

## Status Legend

| Symbol | Meaning |
|--------|---------|
| ✅ | DONE — all methods read, all summaries captured |
| 🔄 | IN PROGRESS — some methods done, some pending |
| ⏳ | PENDING — not started yet |
| ❌ | FAILED — could not process (reason noted) |

---

## File Structure

```
e:\Learn\ConnectOneMVC\
├── progress_tracker.md          ← THIS FILE (Area + Controller overview)
├── progress/                    ← Per-controller method-level files
│   ├── Start/
│   │   ├── LoginController.md
│   │   ├── HomeController.md
│   │   └── ...
│   ├── Account/
│   │   ├── PaymentVoucherController.md
│   │   ├── JournalVoucherController.md
│   │   └── ...
│   └── ... (one folder per Area)
├── FSD.md                       ← Output: Functional Spec
└── TDD.md                       ← Output: Technical Design
```

> Each controller gets its own progress file with method-level tracking.

---

## Overall Area Progress

| # | Area | Total Controllers | ✅ Done | 🔄 In Progress | ⏳ Pending | Status |
|---|------|-------------------|---------|----------------|-----------|--------|
| 1 | Start | 10 | 10 | 0 | 0 | ✅ COMPLETED |
| 2 | Account | 24 | 0 | 0 | 24 | ⏳ PENDING |
| 3 | Profile | 24 | 0 | 0 | 24 | ⏳ PENDING |
| 4 | Facility | 25 | 0 | 0 | 25 | ⏳ PENDING |
| 5 | Stock | 12 | 0 | 0 | 12 | ⏳ PENDING |
| 6 | Reports | 23 | 0 | 0 | 23 | ⏳ PENDING |
| 7 | Magazine | 5 | 0 | 0 | 5 | ⏳ PENDING |
| 8 | Membership | 2 | 0 | 0 | 2 | ⏳ PENDING |
| 9 | Options | 6 | 0 | 0 | 6 | ⏳ PENDING |
| 10 | Help | 8 | 0 | 0 | 8 | ⏳ PENDING |
| 11 | Statements | 4 | 0 | 0 | 4 | ⏳ PENDING |
| 12 | Top-Level | 6 | 0 | 0 | 6 | ⏳ PENDING |

---

# Area 1: Start

| # | Controller | Size | Methods | Status | Progress File |
|---|-----------|------|---------|--------|---------------|
| 1 | LoginController.cs | 59KB / 1111 lines | 27 | ✅ | [progress/Start/LoginController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/LoginController.md) |
| 2 | HomeController.cs | 20KB | ? | ⏳ | [progress/Start/HomeController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/HomeController.md) |
| 3 | ChangeFinancialYearController.cs | 15KB | ? | ⏳ | [progress/Start/ChangeFinancialYearController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/ChangeFinancialYearController.md) |
| 4 | AccountSubmissionController.cs | 24KB | ? | ⏳ | [progress/Start/AccountSubmissionController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/AccountSubmissionController.md) |
| 5 | AuditRegistrationController.cs | 22KB | ? | ⏳ | [progress/Start/AuditRegistrationController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/AuditRegistrationController.md) |
| 6 | AccountAnalysisController.cs | 7KB | ? | ⏳ | [progress/Start/AccountAnalysisController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/AccountAnalysisController.md) |
| 7 | BankBalanceCheckingController.cs | 11KB | ? | ⏳ | [progress/Start/BankBalanceCheckingController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/BankBalanceCheckingController.md) |
| 8 | CenterAuditInfoController.cs | 5KB | ? | ⏳ | [progress/Start/CenterAuditInfoController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/CenterAuditInfoController.md) |
| 9 | DocumentCheckingController.cs | 8KB | ? | ⏳ | [progress/Start/DocumentCheckingController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/DocumentCheckingController.md) |
| 10 | VouchingAuditController.cs | 6KB | ? | ⏳ | [progress/Start/VouchingAuditController.md](file:///e:/Learn/ConnectOneMVC/progress/Start/VouchingAuditController.md) |

---

# Area 2: Account
_(24 controllers — per-controller files created when this Area begins)_

| # | Controller | Size | Status |
|---|-----------|------|--------|
| 1 | CommonCallsController.cs | 299KB | ⏳ |
| 2 | JournalVoucherController.cs | 382KB | ⏳ |
| 3 | VoucherController.cs | 358KB | ⏳ |
| 4 | FDVoucherController.cs | 325KB | ⏳ |
| 5 | DonationInKindVoucherController.cs | 261KB | ⏳ |
| 6 | InternalTransferController.cs | 221KB | ⏳ |
| 7 | PaymentVoucherController.cs | 203KB | ⏳ |
| 8 | ProfilePaymentVoucherController.cs | 161KB | ⏳ |
| 9 | SaleAssetController.cs | 135KB | ⏳ |
| 10 | DonationVoucherController.cs | 130KB | ⏳ |
| 11 | NoteBookController.cs | 113KB | ⏳ |
| 12 | ReceiptVoucherController.cs | 106KB | ⏳ |
| 13 | AssetTransferVoucherController.cs | 105KB | ⏳ |
| 14 | DonationRegisterController.cs | 95KB | ⏳ |
| 15 | CashBookAuditorController.cs | 65KB | ⏳ |
| 16 | WIPFinalizationController.cs | 59KB | ⏳ |
| 17 | ProfileFDVoucherController.cs | 58KB | ⏳ |
| 18 | CollectionBoxVoucherController.cs | 50KB | ⏳ |
| 19 | CashDepositAndWithdrawnVoucherController.cs | 42KB | ⏳ |
| 20 | BankToBankTransferVoucherController.cs | 42KB | ⏳ |
| 21 | VoucherInfoController.cs | 26KB | ⏳ |
| 22 | DocumentsLibraryController.cs | 8KB | ⏳ |
| 23 | MembershipDispatchRegisterController.cs | 2KB | ⏳ |
| 24 | TDSRegisterController.cs | 1KB | ⏳ |

---

# Area 3–12: Pending
_(Controller tables will be populated when each Area begins)_

| Area | Controllers | Status |
|------|-------------|--------|
| Profile | 24 | ⏳ |
| Facility | 25 | ⏳ |
| Stock | 12 | ⏳ |
| Reports | 23 | ⏳ |
| Magazine | 5 | ⏳ |
| Membership | 2 | ⏳ |
| Options | 6 | ⏳ |
| Help | 8 | ⏳ |
| Statements | 4 | ⏳ |
| Top-Level | 6 | ⏳ |
