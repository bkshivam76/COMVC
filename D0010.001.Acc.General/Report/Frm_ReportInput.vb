Imports System.Data.OleDb
Imports DevExpress.XtraEditors
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.UI.PivotGrid
Imports DevExpress.XtraReports
Imports System.ComponentModel.Design.Serialization
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Extensions
Imports DevExpress.XtraPrinting.Preview
Public Class Frm_ReportInput

#Region "Start --> Variables"
    Private xFr_Date As DateTime = Nothing : Private xTo_Date As DateTime = Nothing
    Public MainBase As New Common_Lib.Common
    Public ReportType As String
    Public SingleDateSelection As Boolean = False
#End Region

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Alt Or Keys.F2)) Then 'CHANGE PERIOD
            Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = 20
            Return (True)
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private Sub ReportInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.Top = Me.Top - 100
        Txt_ReportTitle.Text = ReportType
        If ReportType.Replace("&", "") = "Vehicle Statement" Or ReportType.Replace("&", "") = "Gold / Silver Statement" Or ReportType.Replace("&", "") = "Movable Asset Statement" Or ReportType.Replace("&", "") = "Property Statement" Then
            SingleDateSelection = True
        End If
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = DialogResult.None
        Fill_Change_Period_Items()

        'Default View Setting..........................................
        'Dim xMM As Integer = Now.Month : Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
        Cmb_View.SelectedIndex = 19
        If SingleDateSelection Then
            Cmb_View.SelectedText = "Specified Period" : Cmb_View.Enabled = False : Change_Period()
        End If

    End Sub

    Private Sub PreviewForm_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim frm As PrintPreviewFormEx = CType(sender, PrintPreviewFormEx)
        frm.PrintingSystem.ExecCommand(PrintingSystemCommand.Scale, New Object() {1.0F})
    End Sub

    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        Dim report
        Dim ds As DataSet = New DataSet()
        Dim rprtHelper As New ReportHelper()
        'Dim DataList As IList
        Dim dt As DataTable
        'remove special character
        'Me.ReportType = "Transaction Statement"
        Dim reportName As String = Me.ReportType.Replace("&", "")
        '  Dim list As DataTable
        'Get desired datasource for report

        Select Case reportName
            Case "Notebook"
                report = New GeneralReport_Landscape()
                report.ReportType = reportName

                Dim Notebook_List As DataTable = Base._NoteBook_DBOps.GetList(4)
                report.DataSource = Notebook_List.DataSet
                report.DisplayName = report.ReportType
                report.DesignerOptions.ShowDesignerHints = True
                report.InitBands()
                report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)
                'Build report 
                report.InitTransactionDetailsBasedonXRTable()
                Dim printTool As New ReportPrintTool(report)
                printTool.Report.CreateDocument(False)
                AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                printTool.ShowPreviewDialog()
            Case "Transaction Statement"
                report = New GeneralReport_Landscape()
                report.ReportType = reportName

                Dim Transaction_List As DataTable = Base._Reports_Common_DBOps.GetTransactionList(xFr_Date.Date, xTo_Date.Date, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Statement)
                'Base._Reports_Common_DBOps.GetItemsList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Statement, list)
                'If list Is Nothing Or Transaction_List Is Nothing Then
                '    Base.HandleDBError_OnNothingReturned()
                '    Exit Sub
                'End If
                ''BUILD DATA
                'Dim BuildData = From B In Transaction_List, A In DirectCast(list, DataTable)
                '                Where (B.Field(Of String)("tr_item_id") = A.Field(Of String)("II_REC_ID")) _
                '                Select New ReportDataObjects.TransactionData With
                '                       { _
                '                       .TrDate = B.Field(Of DateTime)(ReportDataObjects.TransactionData.Properties.TrDate), _
                '                       .Particulars = A.Field(Of String)(ReportDataObjects.TransactionData.Properties.Particulars), _
                '                       .Type = A.Field(Of String)(ReportDataObjects.TransactionData.Properties.Type), _
                '                       .Head = A.Field(Of String)(ReportDataObjects.TransactionData.Properties.Head), _
                '                       .VNo = B.Field(Of String)(ReportDataObjects.TransactionData.Properties.VNo), _
                '                       .Amt = B.Field(Of Double)(ReportDataObjects.TransactionData.Properties.Amt)
                '                       } : DataList = BuildData.ToList
                '.Type = IIf(A.Field(Of String)(ReportDataObjects.TransactionData.Properties.Type) = "DEBIT", "Expense", "Receipt"), _
                If (Transaction_List.Rows.Count > 0) Then
                    '   xPleaseWait.Show("G e n e r a t i n g   T r a n s a c t i o n   S t a t e m e n t")
                    ds.Tables.Add(report.RecreateTransactionDataSet(Transaction_List))
                    report.DataSource = ds

                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End If
                report.DisplayName = report.ReportType
                report.DesignerOptions.ShowDesignerHints = True
                report.InitBands()
                report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)
                'Build report 
                report.InitTransactionDetailsBasedonXRTable()
                Dim printTool As New ReportPrintTool(report)
                printTool.Report.CreateDocument(False)
                AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                printTool.ShowPreviewDialog()

                'report.ShowPreviewDialog()
            Case "Construction / WIP Statement"
                report = New GeneralReport_Landscape()
                ' xPleaseWait.Show("G e n e r a t i n g   C o n s t r u c t i o n / W I P  S t a t e m e n t")
                report.ReportType = reportName
                Dim TxnList As DataSet = Base._Reports_Common_DBOps.GetConstructionWIPExpensesList(xFr_Date.Date, xTo_Date.Date, Common_Lib.RealTimeService.ClientScreen.Report_Construction_Statement)

                If TxnList Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'xPleaseWait.Hide()
                    Exit Sub
                End If
                report.DisplayName = report.ReportType
                report.DesignerOptions.ShowDesignerHints = True
                report.InitBands()
                report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)

                report.InitBuildingExpenseDetailsBasedonXRTable(TxnList)
                'report.ShowDesignerDialog()
                'report.ShowPreviewDialog()
                Dim printTool As New ReportPrintTool(report)
                printTool.Report.CreateDocument(False)
                AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                printTool.ShowPreviewDialog()
            Case "Collection Box Statement"
                report = New GeneralReport_Portrait()
                '  xPleaseWait.Show("G e n e r a t i n g   C o l l e c t i o n   B o x   R e p o r t")
                report.ReportType = reportName
                dt = New DataTable()
                Dim Addresses As DataTable = Base._Reports_Common_DBOps.GetAddressList(Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box)
                If Addresses Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    ' xPleaseWait.Hide()
                    Exit Sub
                End If
                Dim TxnList As DataTable = Base._Reports_Common_DBOps.GetCollectionBoxTransactionList(xFr_Date.Date, xTo_Date.Date, Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box)
                If TxnList Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    '  xPleaseWait.Hide()
                    Exit Sub
                End If
                'BUILD DATA
                Dim BuildData = From B In TxnList
                                Join A In Addresses On A.Field(Of String)("rec_id") Equals B.Field(Of String)("TR_AB_ID_1")
                                Join C In Addresses On C.Field(Of String)("rec_id") Equals B.Field(Of String)("TR_AB_ID_2")
                                Select New ReportDataObjects.CollectionBoxData With _
                                {.Person1 = A.Field(Of String)("name"), _
                                .Person2 = C.Field(Of String)("name"), _
                                .Amount = B.Field(Of Decimal)("Amt"), _
                                .ReportDate = B.Field(Of String)("tr_date")
                                } : Dim Final_Data = BuildData.ToList()
                If (Final_Data.Count > 0) Then
                    dt = ToDataTable(Of ReportDataObjects.CollectionBoxData)(Final_Data)
                    'add total row
                    Dim dr As DataRow = dt.NewRow() : dr("Person2") = "Total" : dr("Amount") = dt.Compute("Sum(Amount)", "") : dt.Rows.Add(dr)
                End If

                dt = ReplaceTableHeaders(dt)
                If (dt.Rows.Count > 0) Then
                    ds.Tables.Add(dt)
                    report.DataSource = ds
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '   xPleaseWait.Hide()
                    Exit Sub
                End If
                report.DisplayName = report.ReportType
                report.DesignerOptions.ShowDesignerHints = True
                report.InitBands()
                report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)
                'Build report 
                report.InitCollectionBox(ds)
                'report.ShowPreviewDialog()
                Dim printTool As New ReportPrintTool(report)
                printTool.Report.CreateDocument(False)
                AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                printTool.ShowPreviewDialog()
            Case "Transaction Summary (Potamel)"
                ' xPleaseWait.Show("G e n e r a t i n g   T r a n s a c t i o n   S u m m a r y")
                report = New Report_Potamel()
                report.ReportType = reportName
                ds = getTransactionsSummaryData(xFr_Date.Date, xTo_Date.Date)
                If ds Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    '  xPleaseWait.Hide()
                    Exit Sub
                End If
                If (ds.Tables(0).Rows.Count > 0) Then
                    report.DataSource = ds
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '   xPleaseWait.Hide()
                    Exit Sub
                End If
                report.DisplayName = report.ReportType
                report.DesignerOptions.ShowDesignerHints = True
                report.InitBands()
                report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)
                'Build report 
                report.InitTransactionsSummary(ds)
                Dim printTool As New ReportPrintTool(report)
                printTool.Report.CreateDocument(False)
                'printTool.Report.UpdatePageSettings(
                AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                printTool.ShowPreviewDialog()

                'report.ShowPreviewDialog()
               ' xPleaseWait.Hide()
            Case "Donation Statement"
                ' xPleaseWait.Show("G e n e r a t i n g    D o n a t i o n  / G i f t    R e p o r t")
                Dim xRep As New Donation_Gifts_Report(xFr_Date.Date, xTo_Date.Date)
                'Base.Show_ReportPreview(xRep, "Donation Statement", Me, True)
                'xPleaseWait.Hide()
            Case "Cash Book"
                '   xPleaseWait.Show("G e n e r a t i n g    C a s h   B o o k ")
                Dim xRep As New D0010._001.Report_CashBook : xRep.MainBase = Base
                xRep.xFr_Date = xFr_Date.Date : xRep.xTo_Date = xTo_Date.Date
                'Base.Show_ReportPreview(xRep, "Cash Book", Me, True)
               ' xPleaseWait.Hide()
            Case "Vehicle Statement"
                '  xPleaseWait.Show("G e n e r a t i n g    V e h i c l e   R e p o r t ")
                Dim xRep As New D0010._001.Vehicle_Report : xRep.MainBase = Base
                xRep.OnDate = xTo_Date.Date
                ' Base.Show_ReportPreview(xRep, "Vehicle Statement", Me, True)
              '  xPleaseWait.Hide()
            Case "Movable Asset Statement"
                'xPleaseWait.Show("G e n e r a t i n g   M o v a b l e   A s s e t   R e p o r t ")
                Dim xRep As New D0010._001.Asset_Report : xRep.MainBase = Base
                xRep.OnDate = xTo_Date.Date
                'Base.Show_ReportPreview(xRep, "Asset Statement", Me, True)
                'xPleaseWait.Hide()
            Case "Property Statement"
                ' xPleaseWait.Show("G e n e r a t i n g    P r o p e r t y   R e p o r t ")
                Dim xRep As New D0010._001.LB_Report : xRep.MainBase = Base
                ' xRep.FromDate = xFr_Date.Date
                xRep.OnDate = xTo_Date.Date
                '  Base.Show_ReportPreview(xRep, "Property Statement", Me, True)
                'xPleaseWait.Hide()
                'Case "Trial Balance"
                '    dt = Base._Reports_Common_DBOps.GetTSummaryList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, True, Base._open_Year_Sdt, Base._open_Year_Edt)
                '    xPleaseWait.Show("G e n e r a t i n g    T r i a l  B a l a n c e ")
                '    report = New GeneralReport_Portrait()
                '    report.ReportType = reportName
                '    ds = getTrialBalanceSummaryData(xFr_Date.Date, xTo_Date.Date)
                '    If ds Is Nothing Then
                '        Base.HandleDBError_OnNothingReturned()
                '        xPleaseWait.Hide()
                '        Exit Sub
                '    End If
                '    If (ds.Tables(0).Rows.Count > 0) Then
                '        report.DataSource = ds
                '    Else
                '        DevExpress.XtraEditors.XtraMessageBox.Show("No Data available for the choosen period", "Generation message...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        xPleaseWait.Hide()
                '        Exit Sub
                '    End If
                '    report.DisplayName = report.ReportType
                '    report.DesignerOptions.ShowDesignerHints = True
                '    report.InitBands()
                '    report.PerpareHeaderAndFooter(Me.Cmb_View.Text, xFr_Date.Date, xTo_Date.Date)
                '    'Build report 
                '    report.InitTransactionsSummary(ds)
                '    'report.ShowPreviewDialog()
                '    Dim printTool As New ReportPrintTool(report)
                '    printTool.Report.CreateDocument(False)
                '    AddHandler printTool.PreviewForm.Load, AddressOf PreviewForm_Load
                '    printTool.ShowPreviewDialog()
                '    xPleaseWait.Hide()
                '    'Dim xfrm As New Frm_Trial_Balance
                '    'xfrm.MainBase = Base
                '    'xfrm.xFr_Date = xFr_Date.Date : xfrm.xTo_Date = xTo_Date.Date
                '    'xfrm.Text = "Trial Balance"
                '    'xfrm.ShowDialog()
                '    'xfrm.Dispose()
                '    'xPleaseWait.Hide()
            Case "Gold / Silver Statement"
                '  xPleaseWait.Show("G e n e r a t i n g    G o l d / S i l v e r   R e p o r t ")
                Dim xRep As New D0010._001.GS_Report : xRep.MainBase = Base
                xRep.OnDate = xTo_Date.Date
                ' Base.Show_ReportPreview(xRep, "Gold / Silver Statement", Me, True)
                'xPleaseWait.Hide()
            Case "F. D. Statement"
                ' xPleaseWait.Show("G e n e r a t i n g    F i x e d   D e p o s i t   R e p o r t ")
                Dim xRep As New D0010._001.FD_Report : xRep.MainBase = Base
                xRep.Start_Date = xFr_Date.Date : xRep.End_Date = xTo_Date.Date
                ' Base.Show_ReportPreview(xRep, "Fixed Deposit Statement", Me, True)
                'xPleaseWait.Hide()
        End Select

        'report.ShowDesignerDialog()
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        Programming_Testing()
        InitializeComponent()
    End Sub

    Public Function ToDataTable(Of T)(ByVal value As IEnumerable(Of T)) As DataTable
        'construct datatable  from List
        Dim returnTable As New DataTable
        Dim firstRecord = value.First
        For Each pi In firstRecord.GetType.GetProperties
            returnTable.Columns.Add(pi.Name, pi.GetValue(firstRecord, Nothing).GetType)
        Next
        For Each result In value
            Dim nr = returnTable.NewRow
            For Each pi In result.GetType.GetProperties
                nr(pi.Name) = pi.GetValue(result, Nothing)
            Next
            returnTable.Rows.Add(nr)
        Next
        Return returnTable
    End Function

    Public Function ReplaceTableHeaders(ByVal dt As DataTable) As DataTable
        'Change Column Names
        Dim collectionData As New ReportDataObjects.CollectionBoxData()
        Dim dtNew As New DataTable()
        dtNew = dt.Copy()
        For i = 0 To dt.Columns.Count() - 1
            Dim newValue As String = collectionData.MapHeaders(dt.Columns(i).Caption)
            dtNew.Columns(i).ColumnName = newValue
        Next
        Return dtNew

    End Function

    Private Function getTransactionTable(ByVal IsReceipt As Boolean, ByVal FrDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Dim dt As DataTable = Base._Reports_Common_DBOps.GetTSummaryList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, IsReceipt, FrDate, ToDate)
        If dt Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
            Exit Function
        End If
        Dim CashBankTotalAmt As String = "0.00"
        If dt.Rows.Count > 0 Then
            CashBankTotalAmt = dt.Rows(0)("IGroupSum")
            'remove 1st row from the table-- this represents the total sum. This will be added as last row
            dt.Rows.RemoveAt(0)
            'make GroupSum display adjustment. 
            'Table obtained from the database will have GroupSum displayed corresponding to its groupname
            'Hence a small adjustment is made to show GroupSum at the end of subgroups
            Dim totalAmount As String = ""
            Dim tempAmount As String = ""
            For i = 0 To dt.Rows.Count - 1
                If Not String.IsNullOrEmpty(dt.Rows(i).Item("IGroupSum")) Then
                    If Not String.IsNullOrEmpty(totalAmount) Then
                        tempAmount = totalAmount
                    End If
                    totalAmount = dt.Rows(i).Item("IGroupSum")
                    dt.Rows(i).Item("IGroupSum") = ""
                    ' dt.Rows(i).Item("ITEM") = ""
                End If

                If (String.IsNullOrEmpty(dt.Rows(i).Item("IAmount")) And i > 0) Then
                    dt.Rows(i - 1).Item("IGroupSum") = tempAmount
                End If
            Next
            dt.Rows(dt.Rows.Count - 1).Item("IGroupSum") = totalAmount

            dt.AcceptChanges()
            Dim nTable As DataTable = dt.Clone
            For Each dRow As DataRow In dt.Rows
                If ((Not String.IsNullOrEmpty(dRow.Item("ITEM")))) Then
                    Dim newRow As DataRow = nTable.NewRow()
                    newRow.ItemArray = dRow.ItemArray
                    nTable.Rows.Add(newRow)
                End If
            Next
            dt = nTable
        End If
        'add Opening and closing balances for cash and bank
        'Opening Balances in Receipts table
        If (IsReceipt) Then

            'insert a dummy row after opening balances
            Dim dtReceiptsRow As DataRow = dt.NewRow()
            dt.Rows.InsertAt(dtReceiptsRow, 0)

            Dim Bankobj As DataRow = dt.NewRow()
            Bankobj("Item") = "Opening Bank Balance"
            Bankobj("IType") = "RECEIPTS"
            Bankobj("IAmount") = ""
            Bankobj("IGroupSum") = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00")
            CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetBankDetails(FrDate, ToDate).OpeningBalance)
            dt.Rows.InsertAt(Bankobj, 0)

            Dim CashRow As DataRow = dt.NewRow()
            CashRow("ITEM") = "Opening Cash Balance"
            CashRow("IType") = "RECEIPTS"
            CashRow("IAmount") = ""
            CashRow("IGroupSum") = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).OpeningBalance).ToString("#0.00")
            CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetCashDetails(FrDate, ToDate).OpeningBalance)
            dt.Rows.InsertAt(CashRow, 0)

        Else
            'insert a dummy row @ first
            Dim dtPaymentRow As DataRow = dt.NewRow()
            dt.Rows.InsertAt(dtPaymentRow, dt.Rows.Count)

            Dim Cashobj As DataRow = dt.NewRow()
            Cashobj("Item") = "Closing Cash Balance"
            Cashobj("IType") = "PAYMENTS"
            Cashobj("IAmount") = ""
            Cashobj("IGroupSum") = Convert.ToDecimal(GetCashDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00")
            CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetCashDetails(FrDate, ToDate).ClosingBalance)
            dt.Rows.InsertAt(Cashobj, dt.Rows.Count)

            Dim Bankobj As DataRow = dt.NewRow()
            Bankobj("ITEM") = "Closing Bank Balance"
            Bankobj("IType") = "PAYMENTS"
            Bankobj("IAmount") = ""
            Bankobj("IGroupSum") = Convert.ToDecimal(GetBankDetails(FrDate, ToDate).ClosingBalance).ToString("#0.00")
            CashBankTotalAmt = Decimal.Parse(CashBankTotalAmt) + Decimal.Parse(GetBankDetails(FrDate, ToDate).ClosingBalance)
            dt.Rows.InsertAt(Bankobj, dt.Rows.Count)
        End If
        Dim dr1 As DataRow = dt.NewRow()
        dr1("IGroupSum") = Convert.ToDecimal(CashBankTotalAmt).ToString("#0.00")
        dt.Rows.Add(dr1) 'add row that corresponds to total 
        dt.AcceptChanges()
        Return dt
    End Function

    Public Function GetCashDetails(ByVal FromDate As Date, ByVal ToDate As Date) As ReportDataObjects.BalanceDetails
        Dim Open_Cash As Double = 0
        Dim R_CASH As Double = 0
        Dim P_CASH As Double = 0
        Dim Close_Cash As Double = 0

        Dim dt As DataTable = Base._Voucher_DBOps.GetCashBalanceSummary(FromDate, ToDate, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID, Base._open_Ins_ID)
        'Dim dt As DataTable = Base._Reports_Common_DBOps.GetCashOpeningBalance(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        If dt Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
            Exit Function
        End If
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)("OPENING")) Then Open_Cash = dt.Rows(0)("OPENING") Else Open_Cash = 0
            If Not IsDBNull(dt.Rows(0)("CLOSING")) Then Close_Cash = dt.Rows(0)("CLOSING") Else Close_Cash = 0
            If Not IsDBNull(dt.Rows(0)("RECEIPT")) Then R_CASH = dt.Rows(0)("RECEIPT") Else R_CASH = 0
            If Not IsDBNull(dt.Rows(0)("PAYMENT")) Then P_CASH = dt.Rows(0)("PAYMENT") Else P_CASH = 0
        Else : Open_Cash = 0 : End If

        'Dim dtTrans As DataTable = Base._Reports_Common_DBOps.GetCashBankTransSum(ToDate, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        'If dtTrans Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Return Nothing
        '    Exit Function
        'End If
        'If dtTrans.Rows.Count > 0 Then
        '    If Not IsDBNull(dtTrans.Rows(0)("R_CASH")) Then R_CASH = dtTrans.Rows(0)("R_CASH") Else R_CASH = 0
        '    If Not IsDBNull(dtTrans.Rows(0)("P_CASH")) Then P_CASH = dtTrans.Rows(0)("P_CASH") Else P_CASH = 0
        'End If
        'Close_Cash = (Open_Cash + R_CASH) - P_CASH

        Dim cashDetails As New ReportDataObjects.BalanceDetails
        cashDetails.OpeningBalance = Open_Cash
        cashDetails.Receipt = R_CASH
        cashDetails.Payment = P_CASH
        cashDetails.ClosingBalance = Close_Cash

        Return cashDetails
    End Function

    Private Function GetBankDetails(ByVal FromDate As Date, ByVal ToDate As Date) As ReportDataObjects.BalanceDetails
        Dim BA_Table As DataTable = Base._Reports_Common_DBOps.GetSavingBankAccounts(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Reports_Common_DBOps.GetBranches(Branch_IDs, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End If
        Dim OP_Table As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(FromDate, ToDate, Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
        'Dim OP_Table As DataTable = Base._Reports_Common_DBOps.GetOpBalance(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        If OP_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End If
        'BUILD DATA'
        Dim BuildData = From B In BB_Table, A In BA_Table, O In OP_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
                        And (A.Field(Of String)("ID") = O.Field(Of String)("ID")) _
                        Select New With { _
                                        .NAME = B.Field(Of String)("BI_SHORT_NAME"), _
                                        .BA_ACCOUNT_NO = A.Field(Of String)("BA_ACCOUNT_NO"), _
                                        .OP_AMOUNT = O.Field(Of Decimal)("OPENING"), _
                                        .CLOSE_AMOUNT = O.Field(Of Decimal)("CLOSING"), _
                                        .REC_AMOUNT = O.Field(Of Decimal)("RECEIPT"), _
                                        .PAY_AMOUNT = O.Field(Of Decimal)("PAYMENT"), _
                                        .ID = A.Field(Of String)("ID")
                                        } : Dim Final_Data = BuildData.ToList
        Dim XCNT As Integer = 2
        Dim XOpen_Bank As Double = 0 : Dim XR_BANK As Double = 0 : Dim XP_BANK As Double = 0 : Dim XClose_Bank As Double = 0

        For Each FieldName In Final_Data
            XOpen_Bank += FieldName.OP_AMOUNT
            XClose_Bank += FieldName.CLOSE_AMOUNT
            XR_BANK += FieldName.REC_AMOUNT
            XP_BANK += FieldName.PAY_AMOUNT
        Next
        'Dim d4 As DataTable = Base._Reports_Common_DBOps.GetCashBankTransSum(ToDate, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        'If d4 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return Nothing : Exit Function
        'If d4.Rows.Count > 0 Then
        '    If Not IsDBNull(d4.Rows(0)("R_BANK")) Then XR_BANK = d4.Rows(0)("R_BANK") Else XR_BANK = 0
        '    If Not IsDBNull(d4.Rows(0)("P_BANK")) Then XP_BANK = d4.Rows(0)("P_BANK") Else XP_BANK = 0
        'End If
        '    Dim X4Table As DataTable = Base._Reports_Common_DBOps.GetBankTransSumForNormalEntries(FieldName.ID, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        '    If X4Table Is Nothing Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return Nothing
        '    End If
        '    If X4Table.Rows.Count > 0 Then
        '        If Not IsDBNull(X4Table.Rows(0)("R_BANK")) Then XR_BANK += X4Table.Rows(0)("R_BANK")
        '        If Not IsDBNull(X4Table.Rows(0)("P_BANK")) Then XP_BANK += X4Table.Rows(0)("P_BANK")
        '    End If


        '    'PAY'
        '    Dim X5Table As DataTable = Base._Reports_Common_DBOps.GetBankPaymentTransForTransferEntries(FieldName.ID, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        '    If X5Table Is Nothing Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return Nothing
        '    End If
        '    If X5Table.Rows.Count > 0 Then
        '        If Not IsDBNull(X5Table.Rows(0)("P_BANK")) Then XP_BANK += X5Table.Rows(0)("P_BANK")
        '    End If

        '    'REC'
        '    Dim X6Table As DataTable = Base._Reports_Common_DBOps.GetBankReceiptsTransForTransferEntries(FieldName.ID, Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary)
        '    If X6Table Is Nothing Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return Nothing
        '    End If
        '    If X6Table.Rows.Count > 0 Then
        '        If Not IsDBNull(X6Table.Rows(0)("R_BANK")) Then XR_BANK += X6Table.Rows(0)("R_BANK")
        '    End If
        '    XCNT += 1
        'Next
        'XClose_Bank = (XOpen_Bank + XR_BANK) - XP_BANK

        Dim BankDetails As New ReportDataObjects.BalanceDetails
        BankDetails.OpeningBalance = XOpen_Bank
        BankDetails.Receipt = XR_BANK
        BankDetails.Payment = XP_BANK
        BankDetails.ClosingBalance = XClose_Bank

        Return BankDetails
    End Function

    Public Function getTransactionsSummaryData(ByVal Fr_Date As Date, ByVal To_Date As Date) As DataSet
        Dim returnDataset As New DataSet()
        Dim receiptsDt As New DataTable
        Dim paymentsDt As New DataTable

        receiptsDt = getTransactionTable(True, Fr_Date, To_Date) ' Gets datatable for receipts
        paymentsDt = getTransactionTable(False, Fr_Date, To_Date) ' Gets datatable for Payments

        Dim TransactionSummary_Table As New DataTable : Dim ROW As DataRow
        With TransactionSummary_Table
            .Columns.Add("ITEM", Type.GetType("System.String"))
            .Columns.Add("IAmount", Type.GetType("System.String"))
            .Columns.Add("IGroupSum", Type.GetType("System.String"))
            .Columns.Add("PITEM", Type.GetType("System.String"))
            .Columns.Add("IPAmount", Type.GetType("System.String"))
            .Columns.Add("IPGroupSum", Type.GetType("System.String"))
        End With
        Dim totalRows As Integer = IIf(receiptsDt.Rows.Count > paymentsDt.Rows.Count, receiptsDt.Rows.Count, paymentsDt.Rows.Count)
        Dim testDouble As Double = 0
        For i = 0 To totalRows - 2
            ROW = TransactionSummary_Table.NewRow()
            If (receiptsDt.Rows.Count - 1 > i) Then
                ROW("ITEM") = receiptsDt.Rows(i)("ITEM")
                ROW("IAmount") = IIf(Double.TryParse(receiptsDt.Rows(i)("IAmount").ToString(), testDouble), receiptsDt.Rows(i)("IAmount"), "")
                ROW("IGroupSum") = IIf(Double.TryParse(receiptsDt.Rows(i)("IGroupSum").ToString(), testDouble), receiptsDt.Rows(i)("IGroupSum"), "")
            Else
                ROW("ITEM") = ""
                ROW("IAmount") = ""
                ROW("IGroupSum") = ""
            End If

            If (paymentsDt.Rows.Count - 1 > i) Then
                ROW("PITEM") = paymentsDt.Rows(i)("ITEM")
                ROW("IPAmount") = IIf(Double.TryParse(paymentsDt.Rows(i)("IAmount").ToString(), testDouble), paymentsDt.Rows(i)("IAmount"), "")
                ROW("IPGroupSum") = IIf(Double.TryParse(paymentsDt.Rows(i)("IGroupSum").ToString(), testDouble), paymentsDt.Rows(i)("IGroupSum"), "")
            Else
                ROW("PITEM") = ""
                ROW("IPAmount") = ""
                ROW("IPGroupSum") = ""
            End If
            If Not (IsDBNull(ROW("ITEM")) And IsDBNull(ROW("PITEM"))) Then TransactionSummary_Table.Rows.Add(ROW)
        Next
        'insert a dummy row to distinguish total from others
        ROW = TransactionSummary_Table.NewRow()

        TransactionSummary_Table.Rows.Add(ROW)
        ' add last row of each table

        ROW = TransactionSummary_Table.NewRow()
        ROW("ITEM") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("ITEM").ToString
        ROW("IAmount") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("IAmount").ToString
        ROW("IGroupSum") = receiptsDt.Rows(receiptsDt.Rows.Count - 1)("IGroupSum").ToString
        ROW("PITEM") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("ITEM").ToString
        ROW("IPAmount") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("IAmount").ToString
        ROW("IPGroupSum") = paymentsDt.Rows(paymentsDt.Rows.Count - 1)("IGroupSum").ToString


        TransactionSummary_Table.Rows.Add(ROW)

        'Replace Headers
        TransactionSummary_Table.Columns("IAmount").ColumnName = "AMOUNT"
        TransactionSummary_Table.Columns("IGroupSum").ColumnName = "TOTAL"
        TransactionSummary_Table.Columns("IPAmount").ColumnName = " AMOUNT"
        TransactionSummary_Table.Columns("IPGroupSum").ColumnName = " TOTAL"
        TransactionSummary_Table.Columns("ITEM").ColumnName = "RECEIPTS"
        TransactionSummary_Table.Columns("PITEM").ColumnName = "PAYMENTS"

        returnDataset.Tables.Add(TransactionSummary_Table)
        Return returnDataset

    End Function
    'Public Function getTrialBalanceSummaryData(ByVal Fr_Date As Date, ByVal To_Date As Date) As DataSet
    '    Dim returnDataset As New DataSet()
    '    'Dim trialBalSummaryDt As New DataTable
    '    'ReportsAll_GetTrialBalance

    '    Dim returnDatatable As New DataTable()

    '    returnDatatable = Base._Reports_Common_DBOps.GetReportTrialBalance(Common_Lib.RealTimeService.ClientScreen.Report_Potamel, Fr_Date, To_Date)
    '    returnDataset.Tables.Add(returnDatatable)
    '    returnDataset.Tables(0).Columns("LEDGER").ColumnName = "LEDGER"
    '    returnDataset.Tables(0).Columns("NATURE").ColumnName = "NATURE"
    '    returnDataset.Tables(0).Columns("PRIMARY_TYPE").ColumnName = "PRIMARY"
    '    returnDataset.Tables(0).Columns("SECONDARY_TYPE").ColumnName = "SECONDARY"
    '    returnDataset.Tables(0).Columns("Opening_Dr").ColumnName = "Opening Debit"
    '    returnDataset.Tables(0).Columns("Opening_Cr").ColumnName = "Opening Credit"
    '    returnDataset.Tables(0).Columns("DR").ColumnName = "Debit"
    '    returnDataset.Tables(0).Columns("CR").ColumnName = "Credit"
    '    returnDataset.Tables(0).Columns("Closing_Dr").ColumnName = "Closing Debit"
    '    returnDataset.Tables(0).Columns("Closing_Cr").ColumnName = "Closing Credit"
    '    'returnDataset.Tables.Add(trialBalSummaryDt)
    '    Return returnDataset

    'End Function


#Region "Start--> Change Period"

    Private Sub Fill_Change_Period_Items()
        Me.Cmb_View.Properties.Items.Clear()
        For I As Integer = Base._open_Year_Sdt.Month To 12 : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Sdt.Year) : Next
        For I As Integer = 1 To Base._open_Year_Edt.Month : Dim xMonth As String = IIf(I = 1, "JAN", IIf(I = 2, "FEB", IIf(I = 3, "MAR", IIf(I = 4, "APR", IIf(I = 5, "MAY", IIf(I = 6, "JUN", IIf(I = 7, "JUL", IIf(I = 8, "AUG", IIf(I = 9, "SEP", IIf(I = 10, "OCT", IIf(I = 11, "NOV", IIf(I = 12, "DEC", "")))))))))))) : Me.Cmb_View.Properties.Items.Add(xMonth & "-" & Base._open_Year_Edt.Year) : Next
        Me.Cmb_View.Properties.Items.Add("1st Quarter") ' : APR to JUN
        Me.Cmb_View.Properties.Items.Add("2nd Quarter") ' : JUL to SEP
        Me.Cmb_View.Properties.Items.Add("3rd Quarter") ' : OCT to DEC
        Me.Cmb_View.Properties.Items.Add("4th Quarter") ' : JAN to MAR
        Me.Cmb_View.Properties.Items.Add("1st Half Yearly") ' : APR to SEP
        Me.Cmb_View.Properties.Items.Add("2nd Half Yearly") ' : OCT to MAR
        Me.Cmb_View.Properties.Items.Add("Nine Months") ' : APR to DEC
        Me.Cmb_View.Properties.Items.Add("Financial Year")
        Me.Cmb_View.Properties.Items.Add("Specific Period")
    End Sub
    Private Sub Cmb_View_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_View.SelectedIndexChanged
        Me.Cmb_View.Properties.Buttons(1).Enabled = False

        'INPUT
        If Cmb_View.SelectedIndex >= 0 And Cmb_View.SelectedIndex <= 11 Then '12 MONTHS
            Dim Sel_Mon As String = Me.Cmb_View.Text.Substring(0, 3).ToUpper
            Dim SEL_MM As Integer = IIf(Sel_Mon = "JAN", 1, IIf(Sel_Mon = "FEB", 2, IIf(Sel_Mon = "MAR", 3, IIf(Sel_Mon = "APR", 4, IIf(Sel_Mon = "MAY", 5, IIf(Sel_Mon = "JUN", 6, IIf(Sel_Mon = "JUL", 7, IIf(Sel_Mon = "AUG", 8, IIf(Sel_Mon = "SEP", 9, IIf(Sel_Mon = "OCT", 10, IIf(Sel_Mon = "NOV", 11, IIf(Sel_Mon = "DEC", 12, 4))))))))))))
            xFr_Date = New Date(Me.Cmb_View.Text.Substring(4, 4), SEL_MM, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 12 Then 'Q1
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 13 Then 'Q2
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 7, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 14 Then 'Q3
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 10, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 15 Then 'Q4
            xFr_Date = New Date(Base._open_Year_Edt.Year, 1, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 3, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 16 Then 'H1
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 17 Then 'H2
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 10, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 18 Then 'NINE MONTHS
            xFr_Date = New Date(Base._open_Year_Sdt.Year, 4, 1) : xTo_Date = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 9, xFr_Date))
        ElseIf Cmb_View.SelectedIndex = 19 Then 'FINANCIAL YEAR
            xFr_Date = Base._open_Year_Sdt : xTo_Date = Base._open_Year_Edt
        ElseIf Cmb_View.SelectedIndex = 20 Then 'SPECIFIC PERIOD
            Me.Cmb_View.Properties.Buttons(1).Enabled = True : Change_Period() : Exit Sub
        End If

        'OUTPUT
        If Cmb_View.SelectedIndex >= 0 Then
            Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        End If
    End Sub
    Private Sub Cmb_View_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles Cmb_View.ButtonClick
        If e.Button.Index = 1 Then
            Change_Period()
            Exit Sub
        End If
    End Sub
    Private Sub Change_Period()
        Dim xfrm As New Frm_Change_Period : xfrm.MainBase = Base
        xfrm.Text = Me.Text : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        If SingleDateSelection Then
            xfrm.Txt_TitleX.Text = "Select Date" : xfrm.xFr_Date = Base._open_Year_Sdt : xfrm.Txt_Fr_Date.Enabled = False
        End If
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = DialogResult.OK Then
            xFr_Date = xfrm.xFr_Date : xTo_Date = xfrm.xTo_Date
            xfrm.Dispose()
        Else
            xfrm.Dispose()
            If SingleDateSelection Then
                Me.Close()
            End If
            Exit Sub
        End If
        Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        If SingleDateSelection Then
            BUT_OK_Click(Me, New System.EventArgs)
        End If

    End Sub

#End Region

End Class