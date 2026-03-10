Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection

Public Class Dialog_DailyBalances

#Region "Start--> Default Variables"

    Public MainBase As New Common_Lib.Common
    Public xFr_Date As Date = Nothing : Public xTo_Date As Date = Nothing
    Public xSelViewIndex As Integer = -1
    Public xSelModeIndex As Integer = -1
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()

    End Sub

#End Region

#Region "Start--> Form Events"

    Private Sub Form_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Hide_Properties()
    End Sub
    Private Sub Form_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.DialogResult = DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.GLookUp_BankList.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Alt Or Keys.F2)) Then 'CHANGE PERIOD
            Me.Cmb_View.SelectedIndex = -1 : Me.Cmb_View.SelectedIndex = 20
            Return (True)
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()
        If rdo_Balances_Mode.SelectedIndex = 1 Then
            If Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
        End If

        Me.Hide()

        If RadioGroup2.SelectedIndex = 0 Then '-- On sceen 
            Dim xFrm As New Frm_Daily_Balances_Report : xFrm.MainBase = Base
            xFrm.DisplayType = IIf(Me.rdo_Balances_Mode.SelectedIndex = 0, "CASH", "BANK")
            If Me.rdo_Balances_Mode.SelectedIndex = 1 Then
                xFrm.Bank_Acc_ID = Me.GLookUp_BankList.Tag
                xFrm.bankName = Me.GLookUp_BankList.Text & ", A/c.No.: " & Me.BE_Bank_Acc_No.Text
            Else
                xFrm.Bank_Acc_ID = ""
                xFrm.bankName = ""
            End If
            xFrm.xFr_Date = xFr_Date : xFrm.xTo_Date = xTo_Date : xFrm.xStatus_Choice = IIf(RadioGroup3.SelectedIndex = 0, "unreconciled", "all")
            xFrm.xView_Sel_Id = Cmb_View.SelectedIndex
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            Dim xRep As New Report_Daily_Balances : xRep.MainBase = Base
            xRep.DisplayType = IIf(Me.rdo_Balances_Mode.SelectedIndex = 0, "CASH", "BANK")
            If Me.rdo_Balances_Mode.SelectedIndex = 1 Then
                xRep.Bank_Acc_ID = Me.GLookUp_BankList.Tag
                xRep.Xr_BankName.Text = Me.GLookUp_BankList.Text & ", A/c.No.: " & Me.BE_Bank_Acc_No.Text
            Else
                xRep.Bank_Acc_ID = ""
                xRep.Xr_BankName.Text = ""
            End If
            xRep.xFr_Date = xFr_Date : xRep.xTo_Date = xTo_Date
            'Base.Show_ReportPreview(xRep, "Daily Balances", Me, True)
            xRep.Dispose()
        End If
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Hide_Properties()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> Other Events"

    Private Sub RadioGroup1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles rdo_Balances_Mode.KeyDown, RadioGroup2.KeyDown, RadioGroup3.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub RadioGroup1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles rdo_Balances_Mode.SelectedIndexChanged, RadioGroup3.SelectedIndexChanged
        If rdo_Balances_Mode.SelectedIndex = 0 Then
            GLookUp_BankList.Enabled = False
            BE_Bank_Branch.Enabled = False
            BE_Bank_Acc_No.Enabled = False

            Me.GLookUp_BankList.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.DimGray

            Me.GLookUp_BankList.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Branch.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray

            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Else
            GLookUp_BankList.Enabled = True
            BE_Bank_Branch.Enabled = True
            BE_Bank_Acc_No.Enabled = True

            Me.GLookUp_BankList.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Navy

            Me.GLookUp_BankList.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Branch.Properties.Appearance.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Acc_No.Properties.Appearance.ForeColor = System.Drawing.Color.Navy

            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy

        End If
    End Sub

    Private Sub RadioGroup2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadioGroup2.SelectedIndexChanged
        If RadioGroup2.SelectedIndex = 0 Then ' On Screen
            RadioGroup3.Enabled = True
        Else
            RadioGroup3.Enabled = False : RadioGroup3.SelectedIndex = 1
        End If
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = DialogResult.None

        Fill_Change_Period_Items()

        'Default View Setting..........................................
        Dim xMM As Integer = Now.Month
        If xSelViewIndex > -1 Then
            Cmb_View.SelectedIndex = xSelViewIndex
        Else
            Me.Cmb_View.SelectedIndex = IIf(xMM = 4, 0, IIf(xMM = 5, 1, IIf(xMM = 6, 2, IIf(xMM = 7, 3, IIf(xMM = 8, 4, IIf(xMM = 9, 5, IIf(xMM = 10, 6, IIf(xMM = 11, 7, IIf(xMM = 12, 8, IIf(xMM = 1, 9, IIf(xMM = 2, 10, IIf(xMM = 3, 11, 0))))))))))))
        End If

        LookUp_GetBankList()
        If GLookUp_BankList.Tag <> "" Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Me.GLookUp_BankList.Tag)
            GLookUp_BankList.EditValue = GLookUp_BankList.Tag
            GLookUp_BankList.Properties.Tag = "SHOW"
        End If
        If xSelModeIndex = -1 Then rdo_Balances_Mode.SelectedIndex = 1 Else rdo_Balances_Mode.SelectedIndex = xSelModeIndex
        RadioGroup1_SelectedIndexChanged(Nothing, Nothing)
        Me.GLookUp_BankList.Focus()
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_BankList)

    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_BankList
    Private Sub GLookUp_BankList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_BankList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_BankListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_BankListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_BankList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_BankListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_BankListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_BankList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_BankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_BankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_BankList.CancelPopup()
            Hide_Properties()
            Me.rdo_Balances_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.rdo_Balances_Mode.Focus()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.BE_Bank_Branch.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_BRANCH").ToString
                Me.BE_Bank_Acc_No.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
                Me.BE_Bank_Acc_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BI_SHORT_NAME").ToString
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_FilterCriteria_GLookUp_BankList(sender As Object, e As ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BI_SHORT_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._Voucher_DBOps.GetBankAccountsList()
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Payment_DBOps.GetBranches(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        '
        'BUILD DATA
        Dim BuildData = From B In BB_Table, A In BA_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
                        Select New With { _
                                        .BANK_NAME = B.Field(Of String)("Name"), _
                                        .BI_SHORT_NAME = B.Field(Of String)("BI_SHORT_NAME"), _
                                        .BANK_BRANCH = B.Field(Of String)("Branch"), _
                                        .BANK_ACC_NO = A.Field(Of String)("BA_ACCOUNT_NO"), _
                                        .BA_ID = A.Field(Of String)("ID"), _
                                        .BANK_ID = B.Field(Of String)("BANK_ID")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        If Final_Data.Count = 1 Then
            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False

            Me.GLookUp_BankList.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Branch.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.Appearance.ForeColor = System.Drawing.Color.DimGray

        Else
            Me.GLookUp_BankList.Properties.ReadOnly = False
        End If

    End Sub

#End Region

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
    Private Sub Cmb_View_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Cmb_View.SelectedIndexChanged
        Me.Cmb_View.Properties.Buttons(1).Enabled = False

        If xSelViewIndex = -1 Then
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
        End If
        If xSelViewIndex <> -1 And Cmb_View.SelectedIndex = 20 Then
            Me.Cmb_View.Properties.Buttons(1).Enabled = True
        End If
        xSelViewIndex = -1

        'OUTPUT
        If Cmb_View.SelectedIndex >= 0 Then
            Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
        End If
    End Sub
    Private Sub Cmb_View_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles Cmb_View.ButtonClick
        If e.Button.Index = 1 Then
            Change_Period()
            Exit Sub
        End If
    End Sub
    Private Sub Change_Period()
        Dim xfrm As New Frm_Change_Period : xfrm.MainBase = Base
        xfrm.Text = Me.Text : xfrm.xFr_Date = xFr_Date : xfrm.xTo_Date = xTo_Date
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = DialogResult.OK Then
            xFr_Date = xfrm.xFr_Date : xTo_Date = xfrm.xTo_Date
            xfrm.Dispose()
        Else
            xfrm.Dispose()
            Exit Sub
        End If
        Me.BE_View_Period.Text = "Fr.: " & Format(xFr_Date, "dd-MMM, yyyy") & "  to  " & Format(xTo_Date, "dd-MMM, yyyy")
    End Sub

#End Region

End Class