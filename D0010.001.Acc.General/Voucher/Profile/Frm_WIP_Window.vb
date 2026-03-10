Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_WIP_Window

#Region "Start--> Default Variables"
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Dim YearID As String = ""
    Public Amount As Double
    Public LedID As String
    Public Reference As String
    Public iTxnM_ID As String
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
    Dim FormClosingEnable As Boolean = True
    Private Sub Form_Closing_Window_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then BUT_CANCEL_Click(New Object, New EventArgs)
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub

#End Region

#Region "Start--> Button Events"
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.D)) Then 'delete
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                BUT_SAVE_Click(BUT_DEL, Nothing)
            End If
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If
            If Len(Trim(Me.GLookUp_WIP_LedgerList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("L e d g e r   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_WIP_LedgerList, 0, Me.GLookUp_WIP_LedgerList.Height, 5000)
                Me.GLookUp_WIP_LedgerList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_WIP_LedgerList)
            End If

            If Len(Trim(Me.Txt_Reference.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Please enter relevant reference which you may remember while converting WIP to assets  . . . !", Me.Txt_Reference, 0, Me.Txt_Reference.Height, 5000)
                Me.Txt_Reference.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Reference)
            End If

            'Check Duplicate Reference
            Dim Param As Common_Lib.RealTimeService.Param_GetDuplicateReferenceCount = New Common_Lib.RealTimeService.Param_GetDuplicateReferenceCount
            Param.Reference = Txt_Reference.Text
            Param.Tag = Val(Me.Tag)
            Param.iTxnM_ID = iTxnM_ID
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then Param.RecID = xID.Text
            Dim cnt As Integer = Base._WIPCretionVouchers.GetDuplicateReferenceCount(Param)
            If cnt > 0 Then
                Base.ShowMessagebox("Information...", "S a m e   R e f e r e n c e   A l r e a d y   E x i s t s", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                'DevExpress.XtraEditors.XtraMessageBox.Show(Base._main_form, "S a m e   R e f e r e n c e   A l r e a d y   E x i s t s", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
                'FormClosingEnable = False : Me.Close()
                Exit Sub
            End If
        End If


        'Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        'Status_Action = Common_Lib.Common.Record_Status._Completed
        'If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then 'new

        '    ' Dim xDate As DateTime = Nothing : If IsDate(Me.Txt_I_Date.Text) Then : xDate = New Date(Me.Txt_I_Date.DateTime.Year, Me.Txt_I_Date.DateTime.Month, Me.Txt_I_Date.DateTime.Day) : Me.Txt_I_Date.Tag = "#" & xDate.ToString(Base._Date_Format_Short) & "#" : Else : Me.Txt_I_Date.Tag = " NULL " : End If
        '    Dim InParam As Common_Lib.RealTimeService.Param_Insert_WIP_Profile = New Common_Lib.RealTimeService.Param_Insert_WIP_Profile()
        '    InParam.LedID = GLookUp_WIP_LedgerList.Tag
        '    InParam.Reference = Txt_Reference.Text
        '    InParam.Amount = Val(Txt_Amount.Text)
        '    InParam.Status_Action = Status_Action

        '    If Base._WIPDBOps.Insert(InParam) Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Me.DialogResult = DialogResult.OK
        '        FormClosingEnable = False : Me.Close()
        '    Else
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        'End If

        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit

        '    ' Dim xDate As DateTime = Nothing : If IsDate(Me.Txt_I_Date.Text) Then : xDate = New Date(Me.Txt_I_Date.DateTime.Year, Me.Txt_I_Date.DateTime.Month, Me.Txt_I_Date.DateTime.Day) : Me.Txt_I_Date.Tag = "#" & xDate.ToString(Base._Date_Format_Short) & "#" : Else : Me.Txt_I_Date.Tag = " NULL " : End If

        '    Dim UpParam As Common_Lib.RealTimeService.Parameter_Update_WIP_Profile = New Common_Lib.RealTimeService.Parameter_Update_WIP_Profile()
        '    UpParam.LedID = GLookUp_WIP_LedgerList.Tag
        '    UpParam.Reference = Txt_Reference.Text
        '    UpParam.Amount = Val(Txt_Amount.Text)
        '    UpParam.RecID = Me.xID.Text

        '    If Base._WIPDBOps.Update(UpParam) Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Me.DialogResult = DialogResult.OK
        '        FormClosingEnable = False : Me.Close()
        '    Else
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        'End If

        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
        '    Dim xPromptWindow As New Common_Lib.Prompt_Window
        '    If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
        '        If Base._LiveStockDBOps.Delete(Me.xID.Text) Then
        '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            Me.DialogResult = DialogResult.OK
        '            FormClosingEnable = False : Me.Close()
        '        Else
        '            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End If
        '    xPromptWindow.Dispose()
        'End If
        Me.DialogResult = DialogResult.OK
        FormClosingEnable = False : Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Cancel this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                Hide_Properties()
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                FormClosingEnable = False : Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.None
            End If
            xPromptWindow.Dispose()
        End If
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

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Reference.GotFocus, Txt_Amount.GotFocus, Txt_Amount.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Reference.Name Or txt.Name = Txt_Reference.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If

    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Reference.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Reference.KeyDown, Txt_Amount.KeyDown
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        Hide_Properties()

        If UCase(txt.Name) = "TXT_SEARCH" Then
            'If e.KeyCode = Keys.Enter Then DGrid1.Focus()
            'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        Else
            If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
        End If
    End Sub

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Reference.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        If e.KeyCode = Keys.Escape And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.PageUp And txt.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            txt.CancelPopup()
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageUp And txt.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            SendKeys.Send("+{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Rad_Insurance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
   
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("WIP" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        ' Me.Txt_I_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TitleX.Text = "Reference"
    
        GLookUp_WIP_LedgerList.Tag = "" : LookUp_GetWIPLedgerList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        'If Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.SuperUser.ToUpper AndAlso Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
        '    'LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'End If
        
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.GLookUp_WIP_LedgerList.Focus()
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        Txt_Amount.Text = Amount : Me.Txt_Amount.Enabled = False

        'Me.GLookUp_WIP_LedgerList.EditValue = LedID
        'Me.GLookUp_WIP_LedgerList.Tag = Me.GLookUp_WIP_LedgerList.EditValue
        'Me.GLookUp_WIP_LedgerList.Properties.Tag = "SHOW"
        ' If Not IsDBNull(d1.Rows(0)("WIP_LED_ID")) Then
        If LedID.Length > 0 Then
            Me.GLookUp_WIP_LedgerList.ShowPopup() : Me.GLookUp_WIP_LedgerList.ClosePopup()
            Me.GLookUp_WIP_LedgerListView.FocusedRowHandle = (Me.GLookUp_WIP_LedgerListView.LocateByValue("WIP_LED_ID", LedID))
            Me.GLookUp_WIP_LedgerList.EditValue = LedID
            Me.GLookUp_WIP_LedgerList.Tag = Me.GLookUp_WIP_LedgerList.EditValue
            Me.GLookUp_WIP_LedgerList.Properties.Tag = "SHOW"
        End If
        'End If
        Me.GLookUp_WIP_LedgerList.Enabled = False
        If Val(Me.Tag) <> Common_Lib.Common.Navigation_Mode._New Then Txt_Reference.Text = Reference

    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.GLookUp_WIP_LedgerList.Enabled = False : Me.GLookUp_WIP_LedgerList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_WIP_LedgerList)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_WIP_LedgerList
    Private Sub GLookUp_WIP_LedgerList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_WIP_LedgerList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_WIP_LedgerListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_WIP_LedgerListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_WIP_LedgerList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_WIP_LedgerListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_WIP_LedgerListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_WIP_LedgerList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_WIP_LedgerList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_WIP_LedgerList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_WIP_LedgerList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_WIP_LedgerList.CancelPopup()
            Hide_Properties()
            'Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_WIP_LedgerList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_WIP_LedgerList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_WIP_LedgerList.EditValueChanged
        If Me.GLookUp_WIP_LedgerList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_WIP_LedgerListView.RowCount > 0 And Val(Me.GLookUp_WIP_LedgerListView.FocusedRowHandle) >= 0) Then
                If Me.GLookUp_WIP_LedgerList.Text.Trim.Length > 0 Then
                    Me.GLookUp_WIP_LedgerList.Tag = Me.GLookUp_WIP_LedgerListView.GetRowCellValue(Me.GLookUp_WIP_LedgerListView.FocusedRowHandle, "WIP_LED_ID").ToString
                Else
                    Me.GLookUp_WIP_LedgerList.Tag = Nothing : Me.GLookUp_WIP_LedgerList.Text = ""
                End If
            Else
                Me.GLookUp_WIP_LedgerList.Tag = Nothing : Me.GLookUp_WIP_LedgerList.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetWIPLedgerList()
        Dim d1 As DataTable = Base._WIP_Finalization_DBOps.GetListOfWIPLedgers(Base.Is_HQ_Centre)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "WIP LEDGER"
        If dview.Count > 0 Then
            'Dim ROW As DataRow : ROW = d1.NewRow() : ROW("LED_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_WIP_LedgerList.Properties.ValueMember = "WIP_LED_ID"
            Me.GLookUp_WIP_LedgerList.Properties.DisplayMember = "WIP LEDGER"
            Me.GLookUp_WIP_LedgerList.Properties.DataSource = dview
            GLookUp_WIP_LedgerListView.PopulateColumns(dview)
            Me.GLookUp_WIP_LedgerListView.Columns("Txn_Cr_ItemId").Visible = False
            Me.GLookUp_WIP_LedgerListView.Columns("WIP_LED_ID").Visible = False
            Me.GLookUp_WIP_LedgerListView.Columns("Txn_Cr_ItemName").Visible = False
            Me.GLookUp_WIP_LedgerListView.RefreshData()
            Me.GLookUp_WIP_LedgerList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_WIP_LedgerList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_WIP_LedgerList.Properties.ReadOnly = False
    End Sub

#End Region


End Class