Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq

Public Class Frm_Select_Asset

#Region "Start--> Default Variables"

    'Dim _db_Connection As New OleDbConnection
    'Dim _db_DataAdapter As OleDbDataAdapter = Nothing
    Dim _db_Table As New DataTable()

    Public FinalizedAsset_Item_ID As String
    Public REC_EDIT_ON As DateTime
    'Public Txn_M_ID As String
    Public Asset_RecID As String
    Public Tr_M_ID As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
    End Sub

#End Region

    '#Region "Start--> Form Events"

    Private Sub Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        xPleaseWait.Show("Asset List" & vbNewLine & vbNewLine & "L o a d i n g . . . !")

        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

    '#End Region

    '#Region "Start--> Button Events"

    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
            Asset_RecID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_ID")
            REC_EDIT_ON = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_EDIT_ON") 'Bug #5202 fixed
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            Me.DialogResult = Windows.Forms.DialogResult.None
            DevExpress.XtraEditors.XtraMessageBox.Show("D a t a   N o t   F o u n d . . . !", Me.Txt_TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.GridView1.Focus()
        End If
    End Sub

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

    '#End Region

    '#Region "Start--> Procedures"

    Private Sub Set_Default()
        Base.Get_Configure_Setting()
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Grid_Display() ' Prepare Grid Data
        xPleaseWait.Hide()
    End Sub

    Private Sub Grid_Display()

        '1 RUN SQL QUERY
        Dim Param As Common_Lib.RealTimeService.Param_GetExistingAssetListing = New Common_Lib.RealTimeService.Param_GetExistingAssetListing
        Param.Item_ID = FinalizedAsset_Item_ID
        Param.Prev_YearId = Base._prev_Unaudited_YearID
        Param.Next_YearID = Base._next_Unaudited_YearID
        Param.TR_M_ID = Tr_M_ID

        _db_Table = Base._WIP_Finalization_DBOps.GetExistingAssetListing(Param)
        If _db_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        If _db_Table.Rows.Count <= 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show("N o   S e l e c t a b l e   A s s e t   E x i s t s !", "Information...", MessageBoxButtons.OK)
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            'FormClosingEnable = False
            Me.Close()
            Exit Sub
        End If

        Me.GridControl1.DataSource = _db_Table
        Me.GridView1.Columns("REC_ID").Visible = False
        Me.GridView1.Columns("REC_EDIT_ON").Visible = False 'added for multiuser check
        Me.GridView1.Columns("AI_TR_ID").Visible = False
        'Me.GridView1.Columns("Org Qty").Visible = False
        'Me.GridView1.Columns("Org Value").Visible = False
        'For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
        ' col.BestFit()
        ' Next
        Me.GridView1.Columns("Item").MinWidth = 197
        Me.GridView1.Columns("Item").BestFit()
        Me.GridView1.Columns("Make").MinWidth = 60
        Me.GridView1.Columns("Make").BestFit()
        Me.GridView1.Columns("Model").MinWidth = 60
        Me.GridView1.Columns("Org Qty").MinWidth = 50
        Me.GridView1.Columns("Org Value").MinWidth = 100
        Me.GridView1.Columns("Curr Qty").MinWidth = 50
        Me.GridView1.Columns("Curr Value").MinWidth = 100
        Me.GridView1.Columns("Model").BestFit()

        ' Me.GridView1.Columns("Address").Width = 300
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()

    End Sub

    Private Sub GridControl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            BUT_OK_Click(Nothing, Nothing)
        End If
    End Sub

    '#End Region

End Class
