Public Class Report_CashBook
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public xFr_Date As DateTime = Nothing : Public xTo_Date As DateTime = Nothing

    Private Open_Cash_Bal, Open_Bank_Bal As Double
    Private Close_Cash_Bal, Close_Bank_Bal As Double
    Public Sub New(_MainBase As Common_Lib.Common, _FrDate As DateTime, _ToDate As DateTime)

        ' This call is required by the designer.
        InitializeComponent()
        MainBase = _MainBase
        xFr_Date = _FrDate
        xTo_Date = _ToDate
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Report_CashBook_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'Base = MainBase
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name : Xr_Version.Text = "Ver.: " & MainBase._Current_Version
        Me.Xr_Cen_Name.Text = "Centre: " & MainBase._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & MainBase._open_Zone_ID : Me.Xr_UID.Text = "UID: " & MainBase._open_UID_No & "(" & MainBase._open_PAD_No & ")"
        Me.Xr_Period.Text = "Period: Fr. " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        Me.XrLabel70.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")")
        AddHandler Me.PrintingSystem.StartPrint, AddressOf ReportOnStartPrint

        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""
        '

        '---------------------------------------------------------------


        '------------------------
        Get_Cash_Bank_Balance()
        '------------------------

        '-------------------(1) Get Data--------------------------------------------------------------------------------------------
        'Get Ledger
        Dim LED_Table As DataTable = MainBase._Voucher_DBOps.GetLedgers()
        If LED_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Item
        Dim ITEM_Table As DataTable = MainBase._Voucher_DBOps.GetItemList("ID", "ITEM_NAME")
        If ITEM_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Address Book
        Dim Unique_AB_Table As DataTable = MainBase._Voucher_DBOps.GetPastParties(xFr_Date, xTo_Date, False)
        If Unique_AB_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim AB_IDs As String = ""
        For Each xRow As DataRow In Unique_AB_Table.Rows : AB_IDs += "'" & xRow("TR_AB_ID_1").ToString & "'," : Next
        If AB_IDs.Trim.Length > 0 Then AB_IDs = IIf(AB_IDs.Trim.EndsWith(","), Mid(AB_IDs.Trim.ToString, 1, AB_IDs.Trim.Length - 1), AB_IDs.Trim.ToString)
        If AB_IDs.Trim.Length = 0 Then AB_IDs = "''"
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Party Name
        Dim AB_TABLE As DataTable = MainBase._Voucher_DBOps.GetPastPartyDetails(AB_IDs)
        If AB_TABLE Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Internal Transfer Centre List
        Dim Trf_CEN_Table As DataTable = MainBase._Voucher_DBOps.GetPastParties(xFr_Date, xTo_Date, True)
        If Trf_CEN_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim CEN_REC_IDs As String = ""
        For Each xRow As DataRow In Trf_CEN_Table.Rows : CEN_REC_IDs += "'" & xRow("TR_AB_ID_1").ToString & "'," : Next
        If CEN_REC_IDs.Trim.Length > 0 Then CEN_REC_IDs = IIf(CEN_REC_IDs.Trim.EndsWith(","), Mid(CEN_REC_IDs.Trim.ToString, 1, CEN_REC_IDs.Trim.Length - 1), CEN_REC_IDs.Trim.ToString)
        If CEN_REC_IDs.Trim.Length = 0 Then CEN_REC_IDs = "''"
        'Get Centre Name
        Dim CEN_Name_Table As DataTable = MainBase._Voucher_DBOps.GetCenterList(CEN_REC_IDs)
        If CEN_Name_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Bank List
        '(A) Bank A/c.
        Dim BA_Table As DataTable = MainBase._Voucher_DBOps.GetSavingAccountList()
        If BA_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"
        '(B) Bank Branch
        Dim BB_Table As DataTable = MainBase._Voucher_DBOps.GetBranchDetails(Branch_IDs)
        If BB_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '(C) Bank Relation (A/c --> Branch)
        Dim Bank_DS As DataSet = New DataSet() : Bank_DS.Tables.Add(BA_Table) : Bank_DS.Tables.Add(BB_Table.Copy)
        Dim BA_Relation As DataRelation = Bank_DS.Relations.Add("BANK", Bank_DS.Tables("BANK_ACCOUNT_INFO").Columns("BA_BRANCH_ID"), Bank_DS.Tables("BANK_BRANCH_INFO").Columns("BB_BRANCH_ID"), False)
        For Each XROW In Bank_DS.Tables(0).Rows : For Each _Row In XROW.GetChildRows(BA_Relation) : XROW("BI_SHORT_NAME") = _Row("BI_SHORT_NAME") : Next : Next
        Bank_DS.Dispose()
        '(D) Bank Columns Prepare
        Dim _online_BANK_COL_TR_REC As String = "" : Dim _online_BANK_COL_NB_REC As String = "" : Dim _online_BANK_COL_TR_PAY As String = "" : Dim _online_BANK_COL_NB_PAY As String = ""
        Dim _local__BANK_COL_TR_REC As String = "" : Dim _local__BANK_COL_NB_REC As String = "" : Dim _local__BANK_COL_TR_PAY As String = "" : Dim _local__BANK_COL_NB_PAY As String = ""
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Donation Status
        Dim Donation_Status_Table As DataTable = MainBase._Voucher_DBOps.GetDonationStatus()
        If Donation_Status_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub

        '-------------------------------------------------------------------------------------------------------------------------------------
        'Transaction
        Dim TR_Table As DataTable = MainBase._Voucher_DBOps.GetList(xFr_Date, xTo_Date,
                                                                _online_BANK_COL_TR_REC, _online_BANK_COL_NB_REC, _local__BANK_COL_TR_REC, _local__BANK_COL_NB_REC,
                                                                _online_BANK_COL_TR_PAY, _online_BANK_COL_NB_PAY, _local__BANK_COL_TR_PAY, _local__BANK_COL_NB_PAY)
        If TR_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '-------------------(2) Set Data Relations--------------------------------------------------------------------------------------------
        'Data Relations
        Dim Voucher_DS As DataSet = New DataSet : Voucher_DS.Tables.Add(TR_Table.Copy)
        'Item
        Voucher_DS.Tables.Add(ITEM_Table.Copy)
        Dim Item_Relation As DataRelation = Voucher_DS.Relations.Add("Item", Voucher_DS.Tables(0).Columns("iTR_ITEM_ID"), Voucher_DS.Tables(1).Columns("ID"), False)
        'Ledger
        Voucher_DS.Tables.Add(LED_Table.Copy)
        Dim LED_Relation As DataRelation = Voucher_DS.Relations.Add("Ledger", Voucher_DS.Tables("Transaction_Info").Columns("iLED_ID"), Voucher_DS.Tables("Acc_Ledger_Info").Columns("LED_ID"), False)
        'Party
        Voucher_DS.Tables.Add(AB_TABLE.Copy)
        Dim AB_Relation As DataRelation = Voucher_DS.Relations.Add("AB", Voucher_DS.Tables("Transaction_Info").Columns("iTR_AB_ID_1"), Voucher_DS.Tables("ADDRESS_BOOK").Columns("C_ID"), False)
        'Centre
        Voucher_DS.Tables.Add(CEN_Name_Table.Copy)
        Dim Centre_Relation As DataRelation = Voucher_DS.Relations.Add("CEN_NAME", Voucher_DS.Tables("Transaction_Info").Columns("iTR_AB_ID_1"), Voucher_DS.Tables("CENTRE_INFO").Columns("REC_ID"), False)
        'Bank
        Voucher_DS.Tables.Add(Bank_DS.Tables(0).Copy)
        Dim BANK_Relation As DataRelation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables("Transaction_Info").Columns("iTR_SUB_ID"), Voucher_DS.Tables("BANK_ACCOUNT_INFO").Columns("ID"), False)
        'DONATION STATUS
        Voucher_DS.Tables.Add(Donation_Status_Table.Copy)
        Dim Donation_Status As DataRelation = Voucher_DS.Relations.Add("Current Status", Voucher_DS.Tables("Transaction_Info").Columns("iREC_ID"), Voucher_DS.Tables("Donation_Status_Info").Columns("DS_TR_ID"), False)

        '-------------------(3) Update Relational Data--------------------------------------------------------------------------------------------
        For Each XROW In Voucher_DS.Tables(0).Rows
            'Item
            For Each Item_Row In XROW.GetChildRows(Item_Relation)
                If XROW("iREC_ID") = "NOTE-BOOK" Then XROW("iTR_ITEM") = "Monthly " & Item_Row("ITEM_NAME") Else XROW("iTR_ITEM") = Item_Row("ITEM_NAME")
            Next
            'Ledger
            For Each _Row In XROW.GetChildRows(LED_Relation)
                XROW("iTR_HEAD") = _Row("LED_NAME")
            Next
            'Party
            For Each _Row In XROW.GetChildRows(AB_Relation)
                XROW("iTR_PARTY_1") = _Row("C_NAME")
            Next
            'Centre
            For Each _Row In XROW.GetChildRows(Centre_Relation)
                If XROW("iTR_CODE").ToString = 8 And XROW("iTR_PARTY_1").ToString.Length <= 0 Then XROW("iTR_PARTY_1") = StrConv(_Row("CEN_NAME"), vbProperCase) & " (" & _Row("CEN_BK_PAD_NO") & ")"
            Next
            'Bank
            For Each _Row In XROW.GetChildRows(BANK_Relation)
                If XROW("iTR_PARTY_1").ToString.Length <= 0 Then XROW("iTR_PARTY_1") = _Row("BI_SHORT_NAME") & ", A/c.No.: " & _Row("BA_ACCOUNT_NO")
            Next
            'Donation Status
            'For Each _Row In XROW.GetChildRows(Donation_Status)
            '    XROW("DON_STATUS") = _Row("DS_STATUS_MISC_ID")
            'Next
        Next

        '-------------------(4) Clear Relation-----------------------------------------------------------------------------------------------------------
        Voucher_DS.Relations.Clear()

        '-------------------(5) Insert Opening Balance---------------------------------------------------------------------------------------------------
        Dim _Date_Serial As Integer = 0 : Dim _Date_Show As String = ""
        If Month(xFr_Date) > 3 Then
            _Date_Serial = Month(xFr_Date) - 3 : _Date_Show = Year(MainBase._open_Year_Sdt) & "-" & Format(Month(xFr_Date), "00") & "-01"
        Else
            _Date_Serial = Month(xFr_Date) + 9 : _Date_Show = Year(MainBase._open_Year_Edt) & "-" & Format(Month(xFr_Date), "00") & "-01"
        End If
        Dim ROW As DataRow
        ROW = Voucher_DS.Tables(0).NewRow
        ROW("iTR_DATE_SERIAL") = _Date_Serial
        ROW("iTR_DATE_SHOW") = _Date_Show
        ROW("iTR_TEMP_ID") = "OPENING BALANCE"
        ROW("iREC_ID") = "OPENING BALANCE"
        ROW("iTR_ROW_POS") = "A"
        ROW("iTR_VNO") = ""
        ROW("iTR_DATE") = Format(xFr_Date, MainBase._Date_Format_Current)
        ROW("iTR_REC_CASH") = Open_Cash_Bal
        ROW("iTR_REC_BANK") = Open_Bank_Bal
        ROW("iTR_ITEM") = "OPENING BALANCE"
        'If REC_BANK01.TAG = "YES" Then ROW("REC_BANK01") = Open_Bank01
        'If REC_BANK02.TAG = "YES" Then ROW("REC_BANK02") = Open_Bank02
        'If REC_BANK03.TAG = "YES" Then ROW("REC_BANK03") = Open_Bank03
        'If REC_BANK04.TAG = "YES" Then ROW("REC_BANK04") = Open_Bank04
        'If REC_BANK05.TAG = "YES" Then ROW("REC_BANK05") = Open_Bank05
        'If REC_BANK06.TAG = "YES" Then ROW("REC_BANK06") = Open_Bank06
        'If REC_BANK07.TAG = "YES" Then ROW("REC_BANK07") = Open_Bank07
        'If REC_BANK08.TAG = "YES" Then ROW("REC_BANK08") = Open_Bank08
        'If REC_BANK09.TAG = "YES" Then ROW("REC_BANK09") = Open_Bank09
        'If REC_BANK10.TAG = "YES" Then ROW("REC_BANK10") = Open_Bank10
        Voucher_DS.Tables(0).Rows.Add(ROW)

        '-------------------(6) Sort & Serialized Voucher Entry-------------------------------------------------------------------------------------------
        Dim DV1 As New DataView(Voucher_DS.Tables(0))
        DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iREC_ADD_ON,iTR_M_ID,iTR_SORT,iTR_SR_NO"
        Dim XTABLE As DataTable = DV1.ToTable

        Dim _TEMP As String = "" : If XTABLE.Rows.Count > 0 Then _TEMP = DV1.ToTable.Rows(0)("iTR_TEMP_ID")
        Dim _SR As Double = 1
        For Each XROW In XTABLE.Rows
            If XROW("iTR_TEMP_ID") = _TEMP Then
                XROW("iTR_REF_NO") = _SR
            Else
                _TEMP = XROW("iTR_TEMP_ID") : _SR = _SR + 1 : XROW("iTR_REF_NO") = _SR
            End If
        Next

        For Each XRow In XTABLE.Rows
            Report_Data1.Tables("Transaction_Info").ImportRow(XRow)
        Next
    End Sub

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub

    Private Sub Get_Cash_Bank_Balance()

        'Get Cash Balance..............
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        Dim Cash_Bal As DataTable = MainBase._Voucher_DBOps.GetCashBalanceSummary(xFr_Date, xTo_Date, MainBase._open_Year_Sdt, MainBase._open_Cen_ID, MainBase._open_Year_ID, MainBase._open_Ins_ID)
        If Cash_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        If Cash_Bal.Rows.Count > 0 Then
            If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Open_Cash_Bal = Cash_Bal.Rows(0)("OPENING") Else Open_Cash_Bal = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("CLOSING")) Then Close_Cash_Bal = Cash_Bal.Rows(0)("CLOSING") Else Close_Cash_Bal = 0
        Else : Open_Cash_Bal = 0 : Close_Cash_Bal = 0 : End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Bank Balance..............
        Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        Dim Bank_Bal As DataTable = MainBase._Voucher_DBOps.GetBankBalanceSummary(xFr_Date, xTo_Date, MainBase._open_Year_Sdt, MainBase._open_Cen_ID, MainBase._open_Year_ID)
        If Bank_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '
        If Bank_Bal.Rows.Count > 0 Then
            For Each XROW In Bank_Bal.Rows
                If Not IsDBNull(XROW("OPENING")) Then Open_Bank_Bal += XROW("OPENING") Else Open_Bank_Bal += 0
                If Not IsDBNull(XROW("CLOSING")) Then Close_Bank_Bal += XROW("CLOSING") Else Close_Bank_Bal += 0
            Next
        Else
            Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        End If
    End Sub

    'cash.........................
    Private Sub Xr_Closing_Cash_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles Xr_Closing_Cash.SummaryGetResult
        e.Result = Convert.ToDouble(Xr_Tot_RecCash.Summary.GetResult) - Convert.ToDouble(Xr_Tot_PayCash.Summary.GetResult)
        e.Handled = True
    End Sub

    'bank.........................
    Dim xTot_RecBank As Double = 0
    Private Sub Xr_Tot_RecBank_SummaryCalculated(sender As Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles Xr_Tot_RecBank.SummaryCalculated
        xTot_RecBank = Convert.ToDouble(Xr_Tot_RecBank.Summary.GetResult())
    End Sub
    Dim xTot_PayBank As Double = 0
    Private Sub Xr_Tot_PayBank_SummaryCalculated(sender As Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles Xr_Tot_PayBank.SummaryCalculated
        xTot_PayBank = Convert.ToDouble(Xr_Tot_PayBank.Summary.GetResult())
    End Sub
    Private Sub Xr_Closing_Bank_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles Xr_Closing_Bank.SummaryGetResult
        e.Result = xTot_RecBank - xTot_PayBank
        e.Handled = True
    End Sub

    'jv..............................
    Dim xTot_RecJournal As Double = 0
    Private Sub Xr_Tot_RecJournal_SummaryCalculated(sender As Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles Xr_Tot_RecJournal.SummaryCalculated
        xTot_RecJournal = Convert.ToDouble(Xr_Tot_RecJournal.Summary.GetResult())
    End Sub
    Dim xTot_PayJournal As Double = 0
    Private Sub Xr_Tot_PayJournal_SummaryCalculated(sender As Object, e As DevExpress.XtraReports.UI.TextFormatEventArgs) Handles Xr_Tot_PayJournal.SummaryCalculated
        xTot_PayJournal = Convert.ToDouble(Xr_Tot_PayJournal.Summary.GetResult())
    End Sub
    Private Sub Xr_Closing_Journal_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles Xr_Closing_Journal.SummaryGetResult
        e.Result = xTot_RecJournal - xTot_PayJournal
        e.Handled = True
    End Sub


End Class