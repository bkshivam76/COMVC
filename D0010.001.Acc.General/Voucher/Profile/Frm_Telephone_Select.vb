Imports DevExpress.XtraEditors

Public Class Frm_Telephone_Select

#Region "Start--> Default Variables"

    Public xLeft As Integer
    Public xTop As Integer
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
        Me.Left = xLeft : Me.Top = xTop
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
        GLookUp_TeleList.Focus()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.GLookUp_TeleList.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.O)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If Len(Trim(Me.GLookUp_TeleList.Tag)) = 0 Or Len(Trim(Me.GLookUp_TeleList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T e l e p h o n e   N o .   N o t   S e l e c t e d . . . !", Me.GLookUp_TeleList, 0, Me.GLookUp_TeleList.Height, 5000)
                Me.GLookUp_TeleList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_TeleList)
            End If

            If BE_Plantype.Text.ToUpper.Trim = "POST PAID" Then
                If IsDate(Me.Txt_Fr_Date.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Fr_Date, 0, Me.Txt_Fr_Date.Height, 5000)
                    Me.Txt_Fr_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Fr_Date)
                End If

                If IsDate(Me.Txt_To_Date.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_To_Date, 0, Me.Txt_To_Date.Height, 5000)
                    Me.Txt_To_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_To_Date)
                End If

                If IsDate(Me.Txt_Fr_Date.Text) = True And IsDate(Me.Txt_To_Date.Text) = True Then
                    If Me.Txt_To_Date.DateTime < Me.Txt_Fr_Date.DateTime Then
                        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                        Me.ToolTip1.Show("D a t e   m u s t   b e   H i g h e r   /   E q u a l   t o   F r o m   D a t e . . . !", Me.Txt_To_Date, 0, Me.Txt_To_Date.Height, 5000)
                        Me.Txt_To_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_To_Date)
                    End If
                End If

                If Len(Trim(Me.Txt_Bill_No.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("B i l l   N o .   c a n n o t   b e   B l a n k . . . !", Me.Txt_Bill_No, 0, Me.Txt_Bill_No.Height, 5000)
                    Me.Txt_Bill_No.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Bill_No)
                End If

                If IsDate(Me.Txt_Bill_Date.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Bill_Date, 0, Me.Txt_Bill_Date.Height, 5000)
                    Me.Txt_Bill_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Bill_Date)
                End If


            End If
        End If


        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Hide_Properties()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
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

#Region "Start--> TextBox Events"

 
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Bill_No.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Bill_No.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Bill_No.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Bill_No.KeyDown, Txt_Bill_Date.KeyDown, Txt_To_Date.KeyDown, Txt_Fr_Date.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Bill_No.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
  
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Telephone Detail"
        Me.Txt_Fr_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_To_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Bill_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_TeleList.Tag = "" : LookUp_GetTeleList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.GLookUp_TeleList.Focus()
    End Sub

    Public TP_BILL_NO, TP_BILL_DATE, TP_PERIOD_FROM, TP_PERIOD_TO As String
    Private Sub Data_Binding()
        Me.GLookUp_TeleList.ShowPopup() : Me.GLookUp_TeleList.ClosePopup()
        Me.GLookUp_TeleListView.FocusedRowHandle = Me.GLookUp_TeleListView.LocateByValue("TP_ID", Me.xID.Text)
        Me.GLookUp_TeleList.EditValue = Me.xID.Text
        Me.GLookUp_TeleList.Properties.Tag = "SHOW"
        Me.GLookUp_TeleList.Properties.ReadOnly = False

        Dim xDate As DateTime = Nothing
        If Not IsDBNull(TP_BILL_DATE) And IsDate(TP_BILL_DATE) Then
            xDate = TP_BILL_DATE : Txt_Bill_Date.DateTime = xDate
        End If
        If Not IsDBNull(TP_PERIOD_FROM) And IsDate(TP_PERIOD_FROM) Then
            xDate = TP_PERIOD_FROM : Txt_Fr_Date.DateTime = xDate
        End If
        If Not IsDBNull(TP_PERIOD_TO) And IsDate(TP_PERIOD_TO) Then
            xDate = TP_PERIOD_TO : Txt_To_Date.DateTime = xDate
        End If

        Txt_Bill_No.Text = TP_BILL_NO
    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.GLookUp_TeleList.Enabled = False : Me.GLookUp_TeleList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Company.Enabled = False : Me.BE_Company.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Category.Enabled = False : Me.BE_Category.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Plantype.Enabled = False : Me.BE_Plantype.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Bill_Date.Enabled = False : Me.Txt_Bill_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Bill_No.Enabled = False : Me.Txt_Bill_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Fr_Date.Enabled = False : Me.Txt_Fr_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_To_Date.Enabled = False : Me.Txt_To_Date.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_TeleList)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_TeleList
    Private Sub GLookUp_TeleList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_TeleList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_TeleListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_TeleListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_TeleList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_TeleListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_TeleListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_TeleList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_TeleList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_TeleList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_TeleList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_TeleList.CancelPopup()
            Hide_Properties()
            Me.BUT_SAVE_COM.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_TeleList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.BUT_SAVE_COM.Focus()
        End If

    End Sub
    Private Sub GLookUp_TeleList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_TeleList.EditValueChanged
        If Me.GLookUp_TeleList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_TeleListView.RowCount > 0 And Val(Me.GLookUp_TeleListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_TeleList.Tag = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_ID").ToString
                Me.BE_Company.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_COMPANY").ToString
                Me.BE_Category.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_CATEGORY").ToString
                Me.BE_Plantype.Text = Me.GLookUp_TeleListView.GetRowCellValue(Me.GLookUp_TeleListView.FocusedRowHandle, "TP_TYPE").ToString

                If BE_Plantype.Text.ToUpper.Trim = "POST PAID" Then
                    Me.Txt_Bill_No.Enabled = True : lbl_billno.AppearanceItemCaption.ForeColor = Color.Red
                    Me.Txt_Bill_Date.Enabled = True : lbl_billdate.AppearanceItemCaption.ForeColor = Color.Red
                    Me.Txt_Fr_Date.Enabled = True : lbl_periodfr.AppearanceItemCaption.ForeColor = Color.Red
                    Me.Txt_To_Date.Enabled = True : lbl_periodto.AppearanceItemCaption.ForeColor = Color.Red
                Else
                    Me.Txt_Bill_No.Enabled = False : Me.Txt_Bill_No.Text = "" : lbl_billno.AppearanceItemCaption.ForeColor = Color.Black
                    Me.Txt_Bill_Date.Enabled = False : Me.Txt_Bill_Date.Text = "" : Me.Txt_Bill_Date.EditValue = "" : lbl_billdate.AppearanceItemCaption.ForeColor = Color.Black
                    Me.Txt_Fr_Date.Enabled = False : Me.Txt_Fr_Date.Text = "" : Me.Txt_Fr_Date.EditValue = "" : lbl_periodfr.AppearanceItemCaption.ForeColor = Color.Black
                    Me.Txt_To_Date.Enabled = False : Me.Txt_To_Date.Text = "" : Me.Txt_To_Date.EditValue = "" : lbl_periodto.AppearanceItemCaption.ForeColor = Color.Black
                End If
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetTeleList()
        Dim d1 As DataTable = Base._telephoneDBOps.GetListByCondition(Common_Lib.RealTimeService.ClientScreen.Profile_Telephone, "")
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "TP_NO"
        If dview.Count > 0 Then
            Me.GLookUp_TeleList.Properties.ValueMember = "TP_ID"
            Me.GLookUp_TeleList.Properties.DisplayMember = "TP_NO"
            Me.GLookUp_TeleList.Properties.DataSource = dview
            Me.GLookUp_TeleListView.RefreshData()
            Me.GLookUp_TeleList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_TeleList.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            Me.GLookUp_TeleList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_TeleList.ShowPopup() : Me.GLookUp_TeleList.ClosePopup() : Me.GLookUp_TeleList.EditValue = dview.Item(0).Row("TP_ID").ToString : Me.GLookUp_TeleList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_TeleList.Properties.ReadOnly = False
        End If
    End Sub

#End Region

End Class