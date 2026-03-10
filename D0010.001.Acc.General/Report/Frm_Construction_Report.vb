Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Linq
Imports DevExpress.XtraPrinting

Public Class Frm_Construction_Report

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private RowFlag1 As Boolean
    Private ColumnFormVisibleFlag1 As Boolean = False
    Public LedgerID As String = ""
    Dim xid As String = ""

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
        AddHandler PivotGridControl1.CustomFieldSort, AddressOf Me.PivotGridControl1_CustomFieldSort

    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Construction/WIP Report" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Base = MainBase
        ''/...FOR PROGRAMMING MODE ONLY......\
        Programming_Testing()
        ''\................................../
        Set_Default() ' Prepare Status-bar help text of all objects
        Me.PivotGridControl1.Focus()
    End Sub

    Private Sub PivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventArgs)
        If e.Field.FieldName = "Month" Then
            'Custom sorting w.r.t MonthNo
            Dim orderValue1 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "MonthNo"), orderValue2 As Object = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "MonthNo")
            e.Result = Comparer.Default.Compare(orderValue1, orderValue2)
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_PRINT.Click, BUT_CLOSE.Click, T_Close.Click, T_Refresh.Click, T_Print.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(btn.Name) = "BUT_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(btn.Name) = "BUT_APPROVE" Then Me.DataNavigation("APPROVE")
            If UCase(btn.Name) = "BUT_FILTER" Then Me.DataNavigation("FILTER")
            If UCase(btn.Name) = "BUT_FIND" Then Me.DataNavigation("FIND")
            If UCase(btn.Name) = "BUT_CLOSE" Then Me.DataNavigation("CLOSE")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_LEDGER" Then Me.DataNavigation("LEDGER")
            If UCase(T_btn.Name) = "T_PRINT" Then Me.DataNavigation("PRINT-LIST")
            If UCase(T_btn.Name) = "T_REFRESH" Then Me.DataNavigation("REFRESH")
            If UCase(T_btn.Name) = "T_COMPLETED" Then Me.DataNavigation("COMPLETED")
            If UCase(T_btn.Name) = "T_CLOSE" Then Me.DataNavigation("CLOSE")
        End If

    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        Me.CancelButton = Me.BUT_CLOSE
        Grid_Display()
        xPleaseWait.Hide()
    End Sub

    Public Sub Grid_Display()
        Dim TxnList As DataTable = Base._Reports_Common_DBOps.GetConstructionReport(Base._open_Year_Sdt, Base._open_Year_Edt, Common_Lib.RealTimeService.ClientScreen.Report_Construction_List)
        If TxnList Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        PivotGridControl1.DataSource = TxnList

        xPleaseWait.Hide()
    End Sub

    Public Sub DataNavigation(ByVal Action As String)
        Select Case Action
            Case "REFRESH"
                Grid_Display()
            Case "PRINT-LIST"
                If CType(Me.PivotGridControl1.DataSource, DataTable).Rows.Count > 0 Then
                    Base.Show_ListPreview(PivotGridControl1, Me.Text, Me, True, Printing.PaperKind.Legal, Me.Text, "", "", True)
                End If
                Me.PivotGridControl1.Focus()
            Case "CLOSE"
                Me.Close()
        End Select
    End Sub
#End Region

End Class
