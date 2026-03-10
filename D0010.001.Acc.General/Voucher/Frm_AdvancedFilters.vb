Imports DevExpress.XtraEditors
Imports System.Data.OleDb
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_AdvancedFilters

#Region "Start--> Default Variables"
    Dim AssetProfile As String = ""
    Public Advanced_Filter_Category As String = ""
    Public Advanced_Filter_RefId As String = ""
    Public FilterType As String = ""
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
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        GLookUp_FilterCriteria.Enabled = False
        If Advanced_Filter_Category.Length > 0 Then
            'LookUp_GetFilterCriteria(Advanced_Filter_Category)
            DataBinding()
        End If
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
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub BUT_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_OK.Click
        If Len(Trim(Me.Cmb_FilterTypes.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("F i l t e r  T y p e   N o t   S e l e c t e d . . . !", Me.Cmb_FilterTypes, 0, Me.Cmb_FilterTypes.Height, 5000)
            Me.Cmb_FilterTypes.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.Cmb_FilterTypes)
        End If


        If Len(Trim(Me.GLookUp_FilterCriteria.Text)) = 0 Then
            Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            Me.ToolTip1.Show("F i l t e r  C r i t e r i a   N o t   S e l e c t e d . . . !", Me.GLookUp_FilterCriteria, 0, Me.GLookUp_FilterCriteria.Height, 5000)
            Me.GLookUp_FilterCriteria.Focus()
            Me.DialogResult = DialogResult.None
            Exit Sub
        Else
            Me.ToolTip1.Hide(Me.GLookUp_FilterCriteria)
        End If

        FilterType = Cmb_FilterTypes.Text
        Advanced_Filter_Category = AssetProfile
        Advanced_Filter_RefId = GLookUp_FilterCriteria.Tag

        Me.DialogResult = DialogResult.OK
    End Sub

#End Region

    Private Sub DataBinding()
        AssetProfile = Advanced_Filter_Category
        Cmb_FilterTypes.Text = FilterType
        Me.GLookUp_FilterCriteria.EditValue = Advanced_Filter_RefId
        Me.GLookUp_FilterCriteria.Tag = Me.GLookUp_FilterCriteria.EditValue
        GLookUp_FilterCriteria.Enabled = True
    End Sub

#Region "Start--> LookupEdit Events"
    Private Sub GLookUp_FilterCriteria_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_FilterCriteria.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_FilterCriteriaView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_FilterCriteriaView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_FilterCriteria.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_FilterCriteriaView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_FilterCriteriaView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_FilterCriteria.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_FilterCriteria_EditValueChanged(sender As Object, e As EventArgs) Handles GLookUp_FilterCriteria.EditValueChanged
        If Me.GLookUp_FilterCriteria.Properties.Tag = "SHOW" Then
            GLookUp_FilterCriteria.Tag = GLookUp_FilterCriteriaView.GetRowCellValue(GLookUp_FilterCriteriaView.FocusedRowHandle, "REC_ID")
        End If
    End Sub
    Private Sub LookUp_GetFilterCriteria(ByVal AssetProfile As String)
        Dim ctr As Int32 = 0
        Dim Param As Common_Lib.RealTimeService.Param_GetAdvancedFilters = New Common_Lib.RealTimeService.Param_GetAdvancedFilters
        Param.Asset_Profile = AssetProfile
        Param.Prev_YearID = Base._prev_Unaudited_YearID
        Dim d1 As DataTable = Base._Voucher_DBOps.GetAdvancedFilters(Param)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        GLookUp_FilterCriteriaView.Columns.Clear()

        For Each col As DataColumn In d1.Columns
            Dim cCol As DevExpress.XtraGrid.Columns.GridColumn = New DevExpress.XtraGrid.Columns.GridColumn
            cCol.FieldName = col.ColumnName
            cCol.Name = col.ColumnName
            cCol.Visible = True
            cCol.Caption = col.ColumnName
            cCol.VisibleIndex = ctr
            GLookUp_FilterCriteriaView.Columns.Add(cCol)
            ctr += 1
        Next
        Dim dView As New DataView(d1)

        If dView.Count > 0 Then
            Me.GLookUp_FilterCriteria.Properties.ValueMember = "REC_ID"
            Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Item"
            If AssetProfile = "FD" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "FD No."
            If AssetProfile = "PURPOSE" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Purpose"
            If AssetProfile = "BANK ACCOUNTS" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Bank"
            Me.GLookUp_FilterCriteria.Properties.DataSource = dView
            Me.GLookUp_FilterCriteriaView.RefreshData()
            Me.GLookUp_FilterCriteria.Properties.Tag = "SHOW"
            Me.GLookUp_FilterCriteriaView.Columns("REC_ID").Visible = False
        Else
            Me.GLookUp_FilterCriteria.Properties.ValueMember = "REC_ID"
            Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Item"
            If AssetProfile = "FD" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "FD No."
            If AssetProfile = "PURPOSE" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Purpose"
            If AssetProfile = "BANK ACCOUNTS" Then Me.GLookUp_FilterCriteria.Properties.DisplayMember = "Bank"
            Me.GLookUp_FilterCriteria.Properties.DataSource = dView
            Me.GLookUp_FilterCriteriaView.RefreshData()
            Me.GLookUp_FilterCriteriaView.Columns("REC_ID").Visible = False
            Me.GLookUp_FilterCriteria.Properties.Tag = "NONE"
        End If
    End Sub

    Private Sub Cmb_FilterTypes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_FilterTypes.SelectedIndexChanged
        If Cmb_FilterTypes.Text.ToUpper = "MOVABLE ASSETS" Then AssetProfile = "OTHER ASSETS"
        If Cmb_FilterTypes.Text.ToUpper = "SILVER" Then AssetProfile = "SILVER"
        If Cmb_FilterTypes.Text.ToUpper = "GOLD" Then AssetProfile = "GOLD"
        If Cmb_FilterTypes.Text.ToUpper = "VEHICLES" Then AssetProfile = "VEHICLES"
        If Cmb_FilterTypes.Text.ToUpper = "LIVESTOCK" Then AssetProfile = "LIVESTOCK"
        If Cmb_FilterTypes.Text.ToUpper = "ADVANCES" Then AssetProfile = "ADVANCES"
        If Cmb_FilterTypes.Text.ToUpper = "LIABILITIES" Then AssetProfile = "OTHER LIABILITIES"
        If Cmb_FilterTypes.Text.ToUpper = "OTHER DEPOSITS" Then AssetProfile = "OTHER DEPOSITS"
        If Cmb_FilterTypes.Text.ToUpper = "LAND AND BUILDING" Then AssetProfile = "LAND & BUILDING"
        If Cmb_FilterTypes.Text.ToUpper = "FD" Then AssetProfile = "FD"
        If Cmb_FilterTypes.SelectedItem = "BANK ACCOUNTS" Then AssetProfile = "BANK ACCOUNTS"
        If Cmb_FilterTypes.Text.ToUpper = "PURPOSE" Then AssetProfile = "PURPOSE"
        'If Cmb_FilterTypes.SelectedItem = "Party" Then AssetProfile = "OTHER ASSETS"
        GLookUp_FilterCriteria.Properties.Enabled = True
        LookUp_GetFilterCriteria(AssetProfile)
    End Sub

    Private Sub GLookUp_FilterCriteria_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GLookUp_FilterCriteria.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub

    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        '  Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Select Case Cmb_FilterTypes.Text.ToUpper
            Case "MOVABLE ASSETS"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Make", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Model", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "ADVANCES"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "SILVER"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("DESC", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2}).ToString()
            Case "GOLD"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("DESC", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2}).ToString()
            Case "LAND AND BUILDING"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Category", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Type", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op5 As New BinaryOperator("Use", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op4, op5}).ToString()
            Case "LIVESTOCK"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Name", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("BIRTH YEAR", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "OTHER DEPOSITS"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "LIABILITIES"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Party", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Date", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
            Case "VEHICLES"
                Dim op1 As New BinaryOperator("Item", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("Make", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("Model", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op4 As New BinaryOperator("Reg No", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
            Case "FD"
                Dim op1 As New BinaryOperator("Bank", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op2 As New BinaryOperator("FD No.", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                Dim op3 As New BinaryOperator("FD Date", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
                filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
        End Select
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
#End Region
End Class
