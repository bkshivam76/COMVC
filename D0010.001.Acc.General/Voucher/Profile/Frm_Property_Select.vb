Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq

Public Class Frm_Property_Select

#Region "Start--> Default Variables"

    Dim _db_Dataset As New DataSet()
    Dim _db_Connection As New OleDbConnection
    Dim _db_DataAdapter As OleDbDataAdapter = Nothing
    Dim _db_Table As New DataTable()
    Public closing_value As Decimal
    Public lb_name As String
    Public LB_REC_ID As String
    Public REC_EDIT_ON As DateTime
    Public Txn_M_ID As String
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
        xPleaseWait.Show("Property List" & vbNewLine & vbNewLine & "L o a d i n g . . . !")

        Set_Default() ' Prepare Status-bar help text of all objects
        Me.Focus()
    End Sub

#End Region

#Region "Start--> Button Events"

    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
            LB_REC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_ID").ToString()
            REC_EDIT_ON = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REC_EDIT_ON") 'Bug #5202 fixed
            closing_value = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Final_Amount")
            lb_name = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Name")
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

#End Region

#Region "Start--> Procedures"

    Private Sub Set_Default()
        ' Base.Get_Configure_Setting()
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        Grid_Display() ' Prepare Grid Data
        xPleaseWait.Hide()
    End Sub

    Private Sub Grid_Display()

        _db_Dataset = New DataSet
        '1 RUN SQL QUERY
        _db_Table = Base._L_B_DBOps.GetListForExpenses(Txn_M_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
        If _db_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        'Dim keys(1) As DataColumn
        'keys(0) = _db_Table.Columns("REC_ID")
        '_db_Table.PrimaryKey = keys
        'Dim _IDs As String = ""
        'For Each xRow As DataRow In _db_Table.Rows : _IDs += "'" & xRow("REC_ID").ToString & "'," : Next
        'If _IDs.Trim.Length > 0 Then _IDs = IIf(_IDs.Trim.EndsWith(","), Mid(_IDs.Trim.ToString, 1, _IDs.Trim.Length - 1), _IDs.Trim.ToString)
        'If _IDs.Trim.Length = 0 Then _IDs = "''"

        'Dim TR_TABLE As DataTable = Nothing
        'If Base._prev_Unaudited_YearID.Length > 0 Then
        '    TR_TABLE = Base._L_B_DBOps.GetTransactions(_IDs, Base._prev_Unaudited_YearID)
        'Else
        '    TR_TABLE = Base._L_B_DBOps.GetTransactions(_IDs, Base._open_Year_ID)
        'End If

        'If _db_Table Is Nothing Or TR_TABLE Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim JointDS As DataSet = New DataSet()
        'JointDS.Tables.Add(_db_Table.Copy)
        ''Remove Sold Properties from list 
        'JointDS.Tables.Add(TR_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointDS.Relations.Add("Transaction", JointDS.Tables("Land_Building_Info").Columns("REC_ID"), JointDS.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
        'For Each XRow In JointDS.Tables(0).Rows
        '    For Each _Row In XRow.GetChildRows(Adv_Relation)
        '        Dim xDate As DateTime = Nothing
        '        If Not IsDBNull(_Row("Sale Date")) Then
        '            _db_Table.Rows.Remove(_db_Table.Rows.Find(XRow("REC_ID")))
        '        End If
        '    Next
        'Next

        'If Base._prev_Unaudited_YearID.Length > 0 Then
        '    JointDS = New DataSet()
        '    JointDS.Tables.Add(_db_Table.Copy)
        '    TR_TABLE = Base._L_B_DBOps.GetTransactions(_IDs, Base._open_Year_ID)
        '    'Remove Sold Properties from list 
        '    JointDS.Tables.Add(TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointDS.Relations.Add("Transaction", JointDS.Tables("Land_Building_Info").Columns("REC_ID"), JointDS.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
        '    For Each XRow In JointDS.Tables(0).Rows
        '        For Each _Row In XRow.GetChildRows(Adv_Relation1)
        '            Dim xDate As DateTime = Nothing
        '            If Not IsDBNull(_Row("Sale Date")) Then
        '                _db_Table.Rows.Remove(_db_Table.Rows.Find(XRow("REC_ID")))
        '            End If
        '        Next
        '    Next
        'End If


        'If Base._next_Unaudited_YearID.Length > 0 Then 'Bug 5209
        '    JointDS = New DataSet()
        '    JointDS.Tables.Add(_db_Table.Copy)
        '    TR_TABLE = Base._L_B_DBOps.GetTransactions(_IDs, Base._next_Unaudited_YearID)
        '    'Remove Sold Properties from list 
        '    JointDS.Tables.Add(TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointDS.Relations.Add("Transaction", JointDS.Tables("Land_Building_Info").Columns("REC_ID"), JointDS.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
        '    For Each XRow In JointDS.Tables(0).Rows
        '        For Each _Row In XRow.GetChildRows(Adv_Relation1)
        '            Dim xDate As DateTime = Nothing
        '            If Not IsDBNull(_Row("Sale Date")) Then
        '                _db_Table.Rows.Remove(_db_Table.Rows.Find(XRow("REC_ID")))
        '            End If
        '        Next
        '    Next
        'End If

        'JointDS.Dispose()

        Me.GridControl1.DataSource = _db_Table
        Me.GridView1.Columns("REC_ID").Visible = False
        Me.GridView1.Columns("REC_EDIT_ON").Visible = False 'added for multiuser check
        Me.GridView1.Columns("Final_Amount").Visible = False
        'For Each col As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
        ' col.BestFit()
        ' Next
        Me.GridView1.Columns("Name").MinWidth = 200
        Me.GridView1.Columns("Name").BestFit()
        Me.GridView1.Columns("Category").MinWidth = 200
        Me.GridView1.Columns("Category").BestFit()
        Me.GridView1.Columns("Use").BestFit()
        Me.GridView1.Columns("Type").BestFit()
        Me.GridView1.Columns("Address").Width = 300
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        If Not LB_REC_ID Is Nothing Then
            For ctr As Integer = 0 To _db_Table.Rows.Count - 1
                If Me.GridView1.GetRowCellValue(ctr, "REC_ID").ToString = LB_REC_ID Then
                    Me.GridView1.FocusedRowHandle = ctr
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub GridControl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridControl1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            BUT_OK_Click(Nothing, Nothing)
        End If
    End Sub

#End Region

End Class
