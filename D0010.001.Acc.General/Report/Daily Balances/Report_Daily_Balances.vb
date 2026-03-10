Public Class Report_Daily_Balances

    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public xFr_Date As Date = Nothing : Public xTo_Date As Date = Nothing
    Public DisplayType As String = "BANK" : Private Led_ID As String = "" : Public Bank_Acc_ID As String = ""
    Private Open_Cash_Bal, Open_Bank_Bal As Double
    Private Close_Cash_Bal, Close_Bank_Bal As Double

    Private Sub Report_DailyBalances_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Base = MainBase
        'xPleaseWait.Show("G e n e r a t i n g   D a i l y   B a l a n c e s")
        Me.Xr_Ins_Name.Text = Base._open_Ins_Name : Xr_Version.Text = "Ver.: " & Base._Current_Version
        Me.Xr_Cen_Name.Text = "Centre: " & Base._open_Cen_Name : Me.Xr_Zone_Name.Text = "Zone: " & Base._open_Zone_ID : Me.Xr_UID.Text = "UID: " & Base._open_UID_No & "(" & Base._open_PAD_No & ")"
        Me.Xr_Period.Text = "Period: Fr. " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        Me.XrLabel70.Text = "Date: " & Format(Now.Date, "dd-MMM, yyyy")
        Me.Xr_Printable.Text = IIf(Base._ReportsToBePrinted = String.Empty, "", "(" & Base._ReportsToBePrinted & ")")
        Dim OtherCondition As String = ""

        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = Base._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""

        If DisplayType.ToUpper.Trim = "BANK" Then Me.Xr_Title.Text = "Daily Bank Balances" : Led_ID = "00079" : OtherCondition = " AND ( TR_SUB_CR_LED_ID ='" & Bank_Acc_ID & "' OR TR_SUB_DR_LED_ID ='" & Bank_Acc_ID & "' ) "
        If DisplayType.ToUpper.Trim = "CASH" Then Me.Xr_Title.Text = "Daily Cash Balances" : Led_ID = "00080" : Bank_Acc_ID = "" : OtherCondition = ""

        AddHandler Me.PrintingSystem.StartPrint, AddressOf ReportOnStartPrint

        Dim XTABLE As DataTable = CreateData(Bank_Acc_ID, xFr_Date, xTo_Date, OtherCondition, Led_ID)

        For Each XRow In XTABLE.Rows
            Report_Daily_Balances_Data1.Tables("Transaction_Info").ImportRow(XRow)
        Next

        'xPleaseWait.Hide()
    End Sub

    Public Function CreateData(Optional _Bank_Acc_ID As String = "", Optional _xFr_Date As Date = Nothing, Optional _xTo_Date As Date = Nothing, Optional _OtherCondition As String = "", Optional _Led_ID As String = "") As DataTable
        '------------------------
        Get_Cash_Bank_Balance(_Bank_Acc_ID, _xFr_Date, _xTo_Date)
        '------------------------

        '-------------------(1) Get Data--------------------------------------------------------------------------------------------
        'Get Item
        Dim ITEM_Table As DataTable = Base._Voucher_DBOps.GetItemList("ID", "ITEM_NAME")
        If ITEM_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Address Book
        Dim Unique_AB_Table As DataTable = Base._Voucher_DBOps.GetPastParties(_xFr_Date, _xTo_Date, False)
        If Unique_AB_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        Dim AB_IDs As String = ""
        For Each xRow As DataRow In Unique_AB_Table.Rows : AB_IDs += "'" & xRow("TR_AB_ID_1").ToString & "'," : Next
        If AB_IDs.Trim.Length > 0 Then AB_IDs = IIf(AB_IDs.Trim.EndsWith(","), Mid(AB_IDs.Trim.ToString, 1, AB_IDs.Trim.Length - 1), AB_IDs.Trim.ToString)
        If AB_IDs.Trim.Length = 0 Then AB_IDs = "''"
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Party Name
        Dim AB_TABLE As DataTable = Base._Voucher_DBOps.GetPastPartyDetails(AB_IDs)
        If AB_TABLE Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Internal Transfer Centre List
        Dim Trf_CEN_Table As DataTable = Base._Voucher_DBOps.GetPastParties(_xFr_Date, _xTo_Date, True)
        If Trf_CEN_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        Dim CEN_REC_IDs As String = ""
        For Each xRow As DataRow In Trf_CEN_Table.Rows : CEN_REC_IDs += "'" & xRow("TR_AB_ID_1").ToString & "'," : Next
        If CEN_REC_IDs.Trim.Length > 0 Then CEN_REC_IDs = IIf(CEN_REC_IDs.Trim.EndsWith(","), Mid(CEN_REC_IDs.Trim.ToString, 1, CEN_REC_IDs.Trim.Length - 1), CEN_REC_IDs.Trim.ToString)
        If CEN_REC_IDs.Trim.Length = 0 Then CEN_REC_IDs = "''"
        'Get Centre Name
        Dim CEN_Name_Table As DataTable = Base._Voucher_DBOps.GetCenterList(CEN_REC_IDs)
        If CEN_Name_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Bank List
        '(A) Bank A/c.
        Dim BA_Table As DataTable = Base._Voucher_DBOps.GetSavingAccountList()
        If BA_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"
        '(B) Bank Branch
        Dim BB_Table As DataTable = Base._Voucher_DBOps.GetBranchDetails(Branch_IDs)
        If BB_Table Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Function
        '(C) Bank Relation (A/c --> Branch)
        Dim Bank_DS As DataSet = New DataSet() : Bank_DS.Tables.Add(BA_Table) : Bank_DS.Tables.Add(BB_Table.Copy)
        Dim BA_Relation As DataRelation = Bank_DS.Relations.Add("BANK", Bank_DS.Tables("BANK_ACCOUNT_INFO").Columns("BA_BRANCH_ID"), Bank_DS.Tables("BANK_BRANCH_INFO").Columns("BB_BRANCH_ID"), False)
        For Each XROW In Bank_DS.Tables(0).Rows : For Each _Row In XROW.GetChildRows(BA_Relation) : XROW("BI_SHORT_NAME") = _Row("BI_SHORT_NAME") : Next : Next
        Bank_DS.Dispose()

        '-------------------------------------------------------------------------------------------------------------------------------------
        'Transaction
        Dim TR_Table As DataTable = Base._Voucher_DBOps.GetList(_xFr_Date, _xTo_Date, _Led_ID, _Bank_Acc_ID, _OtherCondition)
        If TR_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If

        '-------------------(2) Set Data Relations--------------------------------------------------------------------------------------------
        'Data Relations
        Dim Voucher_DS As DataSet = New DataSet : Voucher_DS.Tables.Add(TR_Table.Copy)
        'Item
        Voucher_DS.Tables.Add(ITEM_Table.Copy)
        Dim Item_Relation As DataRelation = Voucher_DS.Relations.Add("Item", Voucher_DS.Tables(0).Columns("iTR_ITEM_ID"), Voucher_DS.Tables(1).Columns("ID"), False)
        'Party
        Voucher_DS.Tables.Add(AB_TABLE.Copy)
        Dim AB_Relation As DataRelation = Voucher_DS.Relations.Add("AB", Voucher_DS.Tables("Transaction_Info").Columns("iTR_AB_ID_1"), Voucher_DS.Tables("ADDRESS_BOOK").Columns("C_ID"), False)
        'Centre
        Voucher_DS.Tables.Add(CEN_Name_Table.Copy)
        Dim Centre_Relation As DataRelation = Voucher_DS.Relations.Add("CEN_NAME", Voucher_DS.Tables("Transaction_Info").Columns("iTR_AB_ID_1"), Voucher_DS.Tables("CENTRE_INFO").Columns("REC_ID"), False)
        'Bank
        Voucher_DS.Tables.Add(Bank_DS.Tables(0).Copy)
        Dim BANK_Relation As DataRelation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables("Transaction_Info").Columns("iTR_SUB_ID"), Voucher_DS.Tables("BANK_ACCOUNT_INFO").Columns("ID"), False)
        Dim BANK_Relation2 As DataRelation = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables("Transaction_Info").Columns("iTR_CR_ID"), Voucher_DS.Tables("BANK_ACCOUNT_INFO").Columns("ID"), False)

        '-------------------(3) Update Relational Data--------------------------------------------------------------------------------------------
        For Each XROW In Voucher_DS.Tables(0).Rows
            'Item
            For Each Item_Row In XROW.GetChildRows(Item_Relation)
                If XROW("iREC_ID") = "NOTE-BOOK" Then XROW("iTR_ITEM") = "Monthly " & Item_Row("ITEM_NAME") Else XROW("iTR_ITEM") = Item_Row("ITEM_NAME")
                If XROW("Ref").ToString.Length > 0 Then XROW("iTR_ITEM") = XROW("iTR_ITEM") '+ "(" + XROW("Ref") + ")"
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
            For Each _Row In XROW.GetChildRows(BANK_Relation2)
                If XROW("iTR_CR_NAME").ToString.Length <= 0 Then XROW("iTR_CR_NAME") = _Row("BI_SHORT_NAME") & ", A/c.No.: " & _Row("BA_ACCOUNT_NO")
            Next

        Next

        '-------------------(4) Clear Relation-----------------------------------------------------------------------------------------------------------
        Voucher_DS.Relations.Clear()

        '-------------------(5) Insert Opening Balance---------------------------------------------------------------------------------------------------
        Dim _Date_Serial As Integer = 0 : Dim _Date_Show As String = ""
        If Month(_xFr_Date) > 3 Then
            _Date_Serial = Month(_xFr_Date) - 3 : _Date_Show = Year(Base._open_Year_Sdt) & "-" & Format(Month(_xFr_Date), "00") & "-01"
        Else
            _Date_Serial = Month(_xFr_Date) + 9 : _Date_Show = Year(Base._open_Year_Edt) & "-" & Format(Month(_xFr_Date), "00") & "-01"
        End If
        Dim ROW As DataRow
        ROW = Voucher_DS.Tables(0).NewRow
        ROW("iTR_CODE") = 0
        ROW("iTR_DATE_SERIAL") = _Date_Serial
        ROW("iTR_DATE_SHOW") = _Date_Show
        ROW("iTR_TEMP_ID") = "OPENING BALANCE"
        ROW("iREC_ID") = "OPENING BALANCE"
        ROW("iTR_ROW_POS") = "A"
        ROW("iTR_VNO") = ""
        ROW("iTR_DATE") = Format(_xFr_Date, Base._Date_Format_Current)
        ROW("iTR_REC_CASH") = Open_Cash_Bal
        ROW("iTR_REC_BANK") = Open_Bank_Bal
        ROW("iTR_ITEM") = "OPENING BALANCE"
        ROW("iTR_REF_CDATE") = Format(_xFr_Date, Base._Date_Format_Current)
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
        DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iTR_M_ID,iTR_SORT,iTR_SR_NO,iREC_ADD_ON"
        Dim XTABLE As DataTable = DV1.ToTable


        Dim _TEMP As String = "" : If XTABLE.Rows.Count > 0 Then _TEMP = DV1.ToTable.Rows(0)("iTR_TEMP_ID")
        Dim _SR As Double = 1
        Dim _Temp_Balance_C, _Temp_Receipt_C, _Temp_Payment_C As Double : _Temp_Balance_C = 0 : _Temp_Receipt_C = 0 : _Temp_Payment_C = 0
        Dim _Temp_Balance_B, _Temp_Receipt_B, _Temp_Payment_B As Double : _Temp_Balance_B = 0 : _Temp_Receipt_B = 0 : _Temp_Payment_B = 0

        XTABLE.Columns.Add("iTR_RECEIPT", Type.GetType("System.Double"))
        XTABLE.Columns.Add("iTR_PAYMENT", Type.GetType("System.Double"))
        XTABLE.Columns.Add("iTR_BALANCE", Type.GetType("System.Double"))
        XTABLE.Columns.Add("iTR_VOUCHER", Type.GetType("System.String"))
        For Each XROW In XTABLE.Rows
            If XROW("iTR_TEMP_ID") = _TEMP Then
                XROW("iTR_REF_NO") = _SR
            Else
                _TEMP = XROW("iTR_TEMP_ID") : _SR = _SR + 1 : XROW("iTR_REF_NO") = _SR
            End If

            If DisplayType.ToUpper.Trim = "CASH" Then
                If Not IsDBNull(XROW("iTR_REC_CASH")) Then _Temp_Receipt_C = XROW("iTR_REC_CASH") Else _Temp_Receipt_C = 0
                If Not IsDBNull(XROW("iTR_PAY_CASH")) Then _Temp_Payment_C = XROW("iTR_PAY_CASH") Else _Temp_Payment_C = 0
                If _Temp_Receipt_C <= 0 And _Temp_Payment_C <= 0 Then : Else
                    _Temp_Balance_C = (_Temp_Balance_C + _Temp_Receipt_C) - _Temp_Payment_C
                    If _Temp_Receipt_C > 0 Then XROW("iTR_RECEIPT") = _Temp_Receipt_C
                    If _Temp_Payment_C > 0 Then XROW("iTR_PAYMENT") = _Temp_Payment_C
                    XROW("iTR_BALANCE") = _Temp_Balance_C
                End If
            End If

            If DisplayType.ToUpper.Trim = "BANK" Then
                If Not IsDBNull(XROW("iTR_REC_BANK")) Then _Temp_Receipt_B = XROW("iTR_REC_BANK") Else _Temp_Receipt_B = 0
                If Not IsDBNull(XROW("iTR_PAY_BANK")) Then _Temp_Payment_B = XROW("iTR_PAY_BANK") Else _Temp_Payment_B = 0
                If _Temp_Receipt_B <= 0 And _Temp_Payment_B <= 0 Then : Else
                    _Temp_Balance_B = (_Temp_Balance_B + _Temp_Receipt_B) - _Temp_Payment_B
                    If _Temp_Receipt_B > 0 Then XROW("iTR_RECEIPT") = _Temp_Receipt_B
                    If _Temp_Payment_B > 0 Then XROW("iTR_PAYMENT") = _Temp_Payment_B
                    XROW("iTR_BALANCE") = _Temp_Balance_B
                End If
                If Not IsDBNull(XROW("iTR_SUB_ID")) Then
                    If XROW("iTR_SUB_ID") = Bank_Acc_ID Then
                        XROW("iTR_PARTY_1") = XROW("iTR_CR_NAME")
                    End If
                End If
            End If

            If XROW("iTR_CODE") = 1 Then XROW("iTR_VOUCHER") = "Cash Deposit / Withdrawn"
            If XROW("iTR_CODE") = 2 Then XROW("iTR_VOUCHER") = "Bank to Bank Transfer"
            If XROW("iTR_CODE") = 3 Then XROW("iTR_VOUCHER") = "Payment"
            If XROW("iTR_CODE") = 4 Then XROW("iTR_VOUCHER") = "Receipt"
            If XROW("iTR_CODE") = 5 Then XROW("iTR_VOUCHER") = "Donation - Regular"
            If XROW("iTR_CODE") = 6 Then XROW("iTR_VOUCHER") = "Donation - Foreign"
            If XROW("iTR_CODE") = 7 Then XROW("iTR_VOUCHER") = "Donation - Gift"
            If XROW("iTR_CODE") = 8 Then XROW("iTR_VOUCHER") = "Internal Transfer"
            If XROW("iTR_CODE") = 9 Then XROW("iTR_VOUCHER") = "Collection Box"
            If XROW("iTR_CODE") = 10 Then XROW("iTR_VOUCHER") = "Fixed Deposits (F.D.)"
            If XROW("iTR_CODE") = 11 Then XROW("iTR_VOUCHER") = "Sale Asset"
            If XROW("iTR_CODE") = 12 Then XROW("iTR_VOUCHER") = "Membership - New"
            If XROW("iTR_CODE") = 13 Then XROW("iTR_VOUCHER") = "Membership - Renewal"
            If XROW("iTR_CODE") = 14 Then XROW("iTR_VOUCHER") = "Journal"
            If XROW("iTR_CODE") = 15 Then XROW("iTR_VOUCHER") = "Asset Transfer"
        Next
        Return XTABLE
    End Function

    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub

    Private Sub Get_Cash_Bank_Balance(ByVal _ID As String, _xFr_Date As Date, _xTo_Date As Date)
        'Get Cash Balance..............
        Open_Cash_Bal = 0 : Close_Cash_Bal = 0
        Dim Cash_Bal As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date, _xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
        If Cash_Bal Is Nothing Then Base.HandleDBError_OnNothingReturned() : Exit Sub
        If Cash_Bal.Rows.Count > 0 Then
            If Not IsDBNull(Cash_Bal.Rows(0)("OPENING")) Then Open_Cash_Bal = Cash_Bal.Rows(0)("OPENING") Else Open_Cash_Bal = 0
            If Not IsDBNull(Cash_Bal.Rows(0)("CLOSING")) Then Close_Cash_Bal = Cash_Bal.Rows(0)("CLOSING") Else Close_Cash_Bal = 0
        Else : Open_Cash_Bal = 0 : Close_Cash_Bal = 0 : End If
        '-------------------------------------------------------------------------------------------------------------------------------------
        'Get Bank Balance..............
        Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        Dim Bank_Bal As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date, _xTo_Date, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        If Bank_Bal Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        '
        If Bank_Bal.Rows.Count > 0 Then
            For Each XROW In Bank_Bal.Rows
                If XROW("ID") = _ID Then
                    If Not IsDBNull(XROW("OPENING")) Then Open_Bank_Bal += XROW("OPENING") Else Open_Bank_Bal += 0
                    If Not IsDBNull(XROW("CLOSING")) Then Close_Bank_Bal += XROW("CLOSING") Else Close_Bank_Bal += 0
                    Exit For
                End If
            Next
        Else
            Open_Bank_Bal = 0 : Close_Bank_Bal = 0
        End If
    End Sub

    'Private Sub Xr_Bal_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles Xr_Bal.SummaryGetResult
    '    e.Result = Convert.ToDouble(Xr_Rec.Summary.GetResult) - Convert.ToDouble(Xr_Pay.Summary.GetResult)
    '    e.Handled = True
    'End Sub

End Class