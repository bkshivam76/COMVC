Imports DevExpress.XtraEditors
Imports System.Data.OleDb

Public Class Frm_Voucher_Win_FD_Type

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public SelectedActivityID As String = ""
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
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
        Base = MainBase
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Programming_Testing()
        Grid_Display()
    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.GotFocus, BUT_CANCEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_OK.LostFocus, BUT_CANCEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_OK.KeyDown, BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If GridView1.SelectedRowsCount = 1 Then
            SelectedActivityID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ITEMID").ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show("Please Select One FD Activity you want to perform...!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End If
    End Sub

#End Region

#Region "Start--> Text Events"

    Private Sub Grid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Enter Then e.SuppressKeyPress = True : BUT_OK_Click(sender, New System.EventArgs) 'SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Private Events"
    Private Sub Grid_Display()
        'Dim SQL_STR As String = "SELECT DISTINCT ITEM_NAME AS [FD Activity], IIF(ITEM_NAME = 'FD New', 1, IIF(ITEM_NAME = 'FD Renewed', 2, 4)) AS [Sr] , REC_ID AS ITEMID FROM Item_Info WHERE ITEM_VOUCHER_TYPE = 'fd' and REC_ID NOT IN('65730a27-e365-4195-853e-2f59225fe8f4','1ed5cbe4-c8aa-4583-af44-eba3db08e117') UNION ALL SELECT DISTINCT 'FD Close', 3,'65730a27-e365-4195-853e-2f59225fe8f4' from ITEM_INFO ;"
        Dim d1 As DataTable = Base._FD_Voucher_DBOps.GetFdItemCount()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.GridControl1.DataSource = d1
        Me.GridView1.Columns("ITEMID").Visible = False
        Me.GridView1.Columns("FD Activity").Width = 327
        Me.GridView1.Columns("Sr").Visible = False
        Me.GridView1.Columns("Sr").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        Me.GridView1.Columns("FD Activity").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        Me.GridView1.FocusedRowHandle = 0
        Me.GridControl1.Focus()
    End Sub
#End Region
End Class
