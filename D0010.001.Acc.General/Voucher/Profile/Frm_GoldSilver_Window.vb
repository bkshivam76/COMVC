Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_GoldSilver_Window

#Region "Start--> Default Variables"

    Public Tr_M_ID As String = ""
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
       
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click
        Hide_Properties()

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If
            If Len(Trim(Me.Look_MiscList.Tag)) = 0 Or Len(Trim(Me.Look_MiscList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D e s c r i p t i o n   N o t   S e l e c t e d . . . !", Me.Look_MiscList, 0, Me.Look_MiscList.Height, 5000)
                Me.Look_MiscList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_MiscList)
            End If

            If Val(Trim(Me.Txt_Weight.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   W e i g h t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Weight, 0, Me.Txt_Weight.Height, 5000)
                Me.Txt_Weight.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Weight)
            End If

            If Len(Trim(Me.Look_LocList.Tag)) = 0 Or Len(Trim(Me.Look_LocList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("L o c a t i o n   N o t   S e l e c t e d . . . !", Me.Look_LocList, 0, Me.Look_LocList.Height, 5000)
                Me.Look_LocList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_LocList)
            End If

            If Me.Look_MiscList.Text.ToUpper.Trim = "OTHERS" Then
                If Me.Txt_Others.Text.Trim.Length <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("O t h e r   D e t a i l   N o t   S p e c i f i e d . . . !", Me.Txt_Others, 0, Me.Txt_Others.Height, 5000)
                    Me.Txt_Others.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Others)
                End If
            End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

#End Region

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Others.GotFocus, Txt_Weight.GotFocus, Txt_Weight.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Weight.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Others.KeyPress, Txt_Weight.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Others.KeyDown, Txt_Weight.KeyDown
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

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Others.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub Txt_Weight_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Weight.EditValueChanged
        If Val(Me.Txt_Weight.EditValue) > 0 Then
            Txt_Weight_Desc.ContentVisible = True
            Dim kg_part, gm_part, mg_part As String : kg_part = "kg" : gm_part = "gm" : mg_part = "mg"

            Dim gm_and_mg() As String = Split(CStr(Format(Val(Me.Txt_Weight.EditValue), "0.000")), ".")
            If Val(gm_and_mg(0)) > 0 Then
                Dim kg_and_gm() As String = Split(CStr(Format(Val(gm_and_mg(0)) / 1000, "0.000")), ".")
                If Val(kg_and_gm(0)) > 0 Then
                    Txt_Weight_Desc.Text = Format(Val(kg_and_gm(0)), "#,0") & kg_part & IIf(Val(kg_and_gm(1)) > 0, ", " & Val(kg_and_gm(1)) & gm_part, "") & IIf(Val(gm_and_mg(1)) > 0, " && " & Val(gm_and_mg(1)) & mg_part, "")
                Else
                    Txt_Weight_Desc.Text = IIf(Val(kg_and_gm(1)) > 0, Val(kg_and_gm(1)) & gm_part, "") & IIf(Val(gm_and_mg(1)) > 0, " && " & Val(gm_and_mg(1)) & mg_part, "")
                End If
            Else
                Txt_Weight_Desc.Text = Val(gm_and_mg(1)) & " " & mg_part
            End If

        Else
            Txt_Weight_Desc.ContentVisible = False
        End If

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

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Gold / Silver Detail"
        Look_MiscList.Tag = "" : LookUp_GetMiscList()
        Look_LocList.Tag = "" : LookUp_GetLocList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.Look_MiscList.Focus()
    End Sub
    Public GS_DESC_MISC_ID As String : Public GS_LOC_AL_ID As String
    Private Sub Data_Binding()

        Look_MiscList.EditValue = GS_DESC_MISC_ID
        Look_MiscList.Tag = Look_MiscList.EditValue
        Look_MiscList.Properties.Tag = "SHOW"

        Look_LocList.EditValue = GS_LOC_AL_ID
        Look_LocList.Tag = Look_LocList.EditValue
        Look_LocList.Properties.Tag = "SHOW"

    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.BE_ItemName.Enabled = False : Me.BE_ItemName.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Type.Enabled = False : Me.Cmd_Type.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_MiscList.Enabled = False : Me.Look_MiscList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Weight.Enabled = False : Me.Txt_Weight.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Others.Enabled = False : Me.Txt_Others.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_LocList.Enabled = False : Me.Look_LocList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_Loc.Enabled = False
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Txt_Weight)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.Look_MiscList
    Private Sub But_Loc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Loc.Click
        Dim xfrm As New D0009.Frm_Location_Window
        xfrm.Text = "New ~ Location" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm._Base = Base
        xfrm.ShowDialog(Me)
        If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
            xfrm.Dispose()
            LookUp_GetLocList()
            Me.Look_LocList.EditValue = "" : Me.Look_LocList.Tag = "" : Look_LocList.Properties.Tag = "SHOW"
        Else
            xfrm.Dispose()
        End If
    End Sub
    Private Sub Look_MiscList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_MiscList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_MiscList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_MiscList.CancelPopup()
            Hide_Properties()
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageUp And Look_MiscList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            SendKeys.Send("+{TAB}")
        End If

    End Sub
    Private Sub Look_MiscList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_MiscList.EditValueChanged
        If Me.Look_MiscList.Properties.Tag = "SHOW" Then
            Me.Look_MiscList.Tag = Me.Look_MiscList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetMiscList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetGSItems("Name", "ID")
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.Look_MiscList.Properties.ValueMember = "ID"
            Me.Look_MiscList.Properties.DisplayMember = "Name"
            Me.Look_MiscList.Properties.DataSource = dview
            Me.Look_MiscList.Properties.PopulateColumns()
            'Me.Look_MiscList.Properties.BestFit()
            'Me.Look_MiscList.Properties.PopupWidth = 280
            Me.Look_MiscList.Properties.Columns(1).Visible = False
            Me.Look_MiscList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_MiscList.EditValue = 0
            Me.Look_MiscList.Properties.Tag = "SHOW"
        Else
            Me.Look_MiscList.Properties.Tag = "NONE"
        End If

    End Sub

    Private Sub Look_LocList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_LocList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_LocList.CancelPopup()
            Hide_Properties()
            Me.Txt_Weight.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Weight.Focus()
        End If

    End Sub
    Private Sub Look_LocList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_LocList.EditValueChanged
        If Me.Look_LocList.Properties.Tag = "SHOW" Then
            Me.Look_LocList.Tag = Me.Look_LocList.GetColumnValue("AL_ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetLocList()
        Dim d2 As DataTable = Base._Gift_DBOps.GetAssetLocations(Tr_M_ID)
        If d2 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d2)
        If dview.Count > 0 Then
            Me.Look_LocList.Properties.ValueMember = "AL_ID"
            Me.Look_LocList.Properties.DisplayMember = "Location Name"
            Me.Look_LocList.Properties.DataSource = dview
            Me.Look_LocList.Properties.PopulateColumns()
            'Me.Look_LocList.Properties.BestFit()
            Me.Look_LocList.Properties.PopupWidth = Me.Look_LocList.Width
            Me.Look_LocList.Properties.Columns(1).Visible = False
            Me.Look_LocList.Properties.Columns(2).Visible = False
            Me.Look_LocList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_LocList.EditValue = 0
            Me.Look_LocList.Properties.Tag = "SHOW"
        Else
            Me.Look_LocList.Properties.Tag = "NONE"
        End If
        If dview.Count = 1 Then
            Me.Look_LocList.EditValue = dview.Item(0).Row("AL_ID").ToString
            Me.Look_LocList.Enabled = False
        Else
            Me.Look_LocList.Enabled = True
        End If
        If dview.Count > 1 Then
            LayoutControlItem2.AppearanceItemCaption.ForeColor = Color.Red
        Else
            LayoutControlItem2.AppearanceItemCaption.ForeColor = Color.Black
        End If
    End Sub

#End Region

End Class