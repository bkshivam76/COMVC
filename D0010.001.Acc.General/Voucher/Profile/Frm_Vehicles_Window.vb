Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors


Public Class Frm_Vehicles_Window

#Region "Start--> Default Variables"
    Public Tr_M_ID As String = ""
    Dim MaskType As Integer = 0 '--> 0=OTHER,1=OLD,2=NEW
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


            If Not (BE_ItemName.Text.ToString.ToUpper.Trim = "YO BIKE" Or Me.BE_ItemName.Text.ToString.ToUpper.Trim = "CYCLE" Or Me.BE_ItemName.Text.ToString.ToUpper.Trim = "GOLF CART") Then
                If Len(Trim(Me.Txt_RegNo.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("R e g i s t r a t i o n   N o .   c a n n o t   b e   B l a n k . . . !", Me.Txt_RegNo, 0, Me.Txt_RegNo.Height, 5000)
                    Me.Txt_RegNo.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_RegNo)
                End If
                If IsDate(Me.Txt_RegDate.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_RegDate, 0, Me.Txt_RegDate.Height, 5000)
                    Me.Txt_RegDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_RegDate)
                End If
                'If IsDate(Me.Txt_RegDate.Text) = True Then
                '    Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_RegDate.Text))
                '    If diff < 0 Then
                '        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                '        Me.ToolTip1.Show("D a t e   m u s t   b e   H i g h e r   /   E q u a l   t o   S t a r t   F i n a n c i a l   Y e a r . . . !", Me.Txt_RegDate, 0, Me.Txt_RegDate.Height, 5000)
                '        Me.Txt_RegDate.Focus()
                '        Me.DialogResult = Windows.Forms.DialogResult.None
                '        Exit Sub
                '    Else
                '        Me.ToolTip1.Hide(Me.Txt_RegDate)
                '    End If
                'End If
            End If

            If Len(Trim(Me.Cmd_Ownership.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("O w n e r s h i p   N o t   S e l e c t e d . . . !", Me.Cmd_Ownership, 0, Me.Cmd_Ownership.Height, 5000)
                Me.Cmd_Ownership.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmd_Ownership)
            End If

            If Cmd_Ownership.SelectedIndex <> 0 Then
                If Len(Trim(Me.Look_OwnList.Tag)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("O w n e r   N a m e   N o t   S e l e c t e d . . . !", Me.Look_OwnList, 0, Me.Look_OwnList.Height, 5000)
                    Me.Look_OwnList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_OwnList)
                End If
            End If

            If Not (Me.BE_ItemName.Text.ToString.ToUpper.Trim = "YO BIKE" Or Me.BE_ItemName.Text.ToString.ToUpper.Trim = "CYCLE" Or Me.BE_ItemName.Text.ToString.ToUpper.Trim = "GOLF CART") Then
                If Me.Look_InsList.Tag.Trim.Length <= 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("I n s u r a n c e   C o m p a n y   N o t   S e l e c t e d . . . !", Me.Look_InsList, 0, Me.Look_InsList.Height, 5000)
                    Me.Look_InsList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_InsList)
                End If
                If Len(Trim(Me.Txt_PolicyNo.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("P o l i c y   N o .   c a n n o t   b e   B l a n k . . . !", Me.Txt_PolicyNo, 0, Me.Txt_PolicyNo.Height, 5000)
                    Me.Txt_PolicyNo.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_PolicyNo)
                End If
                If IsDate(Me.Txt_E_Date.Text) = False Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_E_Date, 0, Me.Txt_E_Date.Height, 5000)
                    Me.Txt_E_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_E_Date)
                End If
                If IsDate(Me.Txt_E_Date.Text) = True Then
                    Dim diff1 As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_RegDate.Text), DateValue(Me.Txt_E_Date.Text))
                    Dim diff2 As Double = DateDiff(DateInterval.Day, Now.Date, DateValue(Me.Txt_E_Date.Text))
                    If diff1 <= 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                        Me.ToolTip1.Show("D a t e   m u s t   b e   h i g h e r   t h a n   F i r s t   D a t e   o f   R e g i s t r a t i o n . . . !", Me.Txt_E_Date, 0, Me.Txt_E_Date.Height, 5000)
                        Me.Txt_E_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_E_Date)
                    End If
                    If diff2 <= 0 Then
                        Me.ToolTip1.ToolTipTitle = "Incorrect Information . . ."
                        Me.ToolTip1.Show("D a t e   m u s t   b e   h i g h e r   t h a n   T o d a y ' s . . . !", Me.Txt_E_Date, 0, Me.Txt_E_Date.Height, 5000)
                        Me.Txt_E_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_E_Date)
                    End If

                End If
            End If

            If Len(Trim(Me.Look_LocList.Tag)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("L o c a t i o n   N o t   S e l e c t e d . . . !", Me.Look_LocList, 0, Me.Look_LocList.Height, 5000)
                Me.Look_LocList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Look_LocList)
            End If

        End If


        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
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
     

    Private Sub Set_Reg_No_Pattern(ByVal xType As String)
        Txt_RegNo.EditValue = ""
        If xType = "OTHER" Then
            lbl_Reg_No.Text = "Registration No. (Other Pattern):"
            Me.Txt_RegNo.Properties.Mask.EditMask = "[A-Z0-9\/?/-]{0,15}"
            Me.Txt_RegNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        End If
        If xType = "OLD" Then
            lbl_Reg_No.Text = "Registration No. (Old Pattern):"
            Me.Txt_RegNo.Properties.Mask.EditMask = "[A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]"
            Me.Txt_RegNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        End If
        If xType = "NEW" Then
            lbl_Reg_No.Text = "Registration No. (New Pattern):"
            Me.Txt_RegNo.Properties.Mask.EditMask = "[A-Z][A-Z]-[0-9][0-9]-[A-Z]?[A-Z]-[0-9][0-9][0-9][0-9]"
            Me.Txt_RegNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        End If
    End Sub

    Private Sub RAD_RegPattern_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RAD_RegPattern.SelectedIndexChanged
        If RAD_RegPattern.SelectedIndex = 0 Then Set_Reg_No_Pattern("NEW")
        If RAD_RegPattern.SelectedIndex = 1 Then Set_Reg_No_Pattern("OLD")
        If RAD_RegPattern.SelectedIndex = 2 Then Set_Reg_No_Pattern("OTHER")
    End Sub
    Private Sub RAD_RegPattern_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RAD_RegPattern.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Others.GotFocus, Txt_RegDate.GotFocus, Txt_E_Date.GotFocus, Txt_PolicyNo.GotFocus, Txt_RegNo.GotFocus, Txt_OtherDoc.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If Val(txt.Properties.Tag) = 0 Then SendKeys.Send("^{END}") : txt.Properties.Tag = 1
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Others.KeyPress, Txt_E_Date.KeyPress, Txt_PolicyNo.KeyPress, Cmd_Make.KeyPress, Cmd_Model.KeyPress, Txt_OtherDoc.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_RegDate.KeyDown, Txt_Others.KeyDown, Txt_E_Date.KeyDown, Txt_PolicyNo.KeyDown, Txt_RegNo.KeyDown, Txt_OtherDoc.KeyDown
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

    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Others.Validated, Txt_PolicyNo.Validated, Txt_RegNo.Validated, Cmd_Make.Validated, Cmd_Model.Validated, Txt_OtherDoc.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
    Private Sub Cmd_Make_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmd_Make.Leave
        Get_Vehicle_Model_List()
    End Sub
    Private Sub Cmd_Ownership_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Ownership.SelectedIndexChanged
        If Cmd_Ownership.SelectedIndex = 0 Then 'institution
            Me.Look_OwnList.Visible = False : Me.Look_OwnList.Enabled = False : Me.But_Owner.Enabled = False : Me.Look_OwnList.EditValue = ""
        Else  'incharge & 'free use
            Me.Look_OwnList.Visible = True : Me.Look_OwnList.Enabled = True : Me.But_Owner.Enabled = True : Me.Look_OwnList.EditValue = ""
        End If
    End Sub
    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Ownership.GotFocus, Cmd_Make.GotFocus, Cmd_Model.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_Ownership.KeyDown, Cmd_Make.KeyDown, Cmd_Model.KeyDown
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
    Private Sub ChechBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Affidavit.KeyDown, Chk_Will.KeyDown, Chk_RC_Book.KeyDown, Chk_Trf_Letter.KeyDown, Chk_FU_Letter.KeyDown, Chk_OtherDoc.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Chk_RC_Book_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_RC_Book.CheckedChanged
        If Chk_RC_Book.Checked Then Chk_RC_Book.Tag = "YES" Else Chk_RC_Book.Tag = "NO"
    End Sub
    Private Sub Chk_Affidavit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Affidavit.CheckedChanged
        If Chk_Affidavit.Checked Then Chk_Affidavit.Tag = "YES" Else Chk_Affidavit.Tag = "NO"
    End Sub
    Private Sub Chk_Will_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Will.CheckedChanged
        If Chk_Will.Checked Then Chk_Will.Tag = "YES" Else Chk_Will.Tag = "NO"
    End Sub
    Private Sub Chk_Trf_Letter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_Trf_Letter.CheckedChanged
        If Chk_Trf_Letter.Checked Then Chk_Trf_Letter.Tag = "YES" Else Chk_Trf_Letter.Tag = "NO"
    End Sub
    Private Sub Chk_FU_Letter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_FU_Letter.CheckedChanged
        If Chk_FU_Letter.Checked Then Chk_FU_Letter.Tag = "YES" Else Chk_FU_Letter.Tag = "NO"
    End Sub

    Private Sub Chk_OtherDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chk_OtherDoc.CheckedChanged
        If Chk_OtherDoc.Checked Then
            Chk_OtherDoc.Tag = "YES" : Txt_OtherDoc.Enabled = True : Txt_OtherDoc.Text = "" : Txt_OtherDoc.Focus()
        Else
            Chk_OtherDoc.Tag = "NO" : Txt_OtherDoc.Enabled = False : Txt_OtherDoc.Text = ""
        End If

    End Sub
  

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_RegDate.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_E_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TextEdit1.Text = Base._open_Ins_Name
        Me.TitleX.Text = "Vehicle"
        RAD_RegPattern.SelectedIndex = 0
        
        Me.Chk_RC_Book.Tag = "NO" : Me.Chk_Affidavit.Tag = "NO" : Me.Chk_Will.Tag = "NO" : Me.Chk_Trf_Letter.Tag = "NO" : Me.Chk_FU_Letter.Tag = "NO" : Me.Chk_OtherDoc.Tag = "NO"
        Me.Look_OwnList.Tag = "" : Me.Look_InsList.Tag = "" : Look_LocList.Tag = ""
        LookUp_GetOwnList() : LookUp_GetInsList() : Get_Vehicle_Make_List() : LookUp_GetLocList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        Me.Cmd_Make.Focus()
    End Sub
    Public VI_LOC_AL_ID, VI_MODEL, VI_OWNERSHIP, VI_OWNERSHIP_AB_ID, VI_DOC_RC_BOOK, VI_DOC_AFFIDAVIT, VI_DOC_WILL, VI_DOC_TRF_LETTER, VI_DOC_FU_LETTER, VI_DOC_OTHERS, VI_INSURANCE_ID, VI_DOC_NAME As String
    Public VI_REG_NO_PATTERN, VI_REG_NO, VI_REG_DATE, VI_E_DATE As String
    Private Sub Data_Binding()
        Look_LocList.EditValue = VI_LOC_AL_ID
        Look_LocList.Tag = Look_LocList.EditValue
        Look_LocList.Properties.Tag = "SHOW"

        Get_Vehicle_Model_List()
        Cmd_Model.Text = VI_MODEL

        RAD_RegPattern.EditValue = VI_REG_NO_PATTERN
        Txt_RegNo.Text = VI_REG_NO

        Dim xDate As DateTime = Nothing
        If IsDate(VI_REG_DATE) Then xDate = VI_REG_DATE : Txt_RegDate.DateTime = xDate

        Cmd_Ownership.Text = VI_OWNERSHIP
        If VI_OWNERSHIP_AB_ID.Trim <> "NULL" Then
            If VI_OWNERSHIP_AB_ID.Length > 0 Then
                VI_OWNERSHIP_AB_ID = IIf(VI_OWNERSHIP_AB_ID.Trim.StartsWith("'"), Mid(VI_OWNERSHIP_AB_ID.Trim.ToString, 2, VI_OWNERSHIP_AB_ID.Trim.Length), VI_OWNERSHIP_AB_ID.Trim.ToString)
                VI_OWNERSHIP_AB_ID = IIf(VI_OWNERSHIP_AB_ID.Trim.EndsWith("'"), Mid(VI_OWNERSHIP_AB_ID.Trim.ToString, 1, VI_OWNERSHIP_AB_ID.Trim.Length - 1), VI_OWNERSHIP_AB_ID.Trim.ToString)
            End If
            Look_OwnList.EditValue = VI_OWNERSHIP_AB_ID
            Look_OwnList.Tag = Look_OwnList.EditValue
            Look_OwnList.Properties.Tag = "SHOW"
        End If

        If VI_DOC_RC_BOOK.Trim.ToUpper = "YES" Then Chk_RC_Book.Checked = True Else Chk_RC_Book.Checked = False
        If VI_DOC_AFFIDAVIT.Trim.ToUpper = "YES" Then Chk_Affidavit.Checked = True Else Chk_Affidavit.Checked = False
        If VI_DOC_WILL.Trim.ToUpper = "YES" Then Chk_Will.Checked = True Else Chk_Will.Checked = False
        If VI_DOC_TRF_LETTER.Trim.ToUpper = "YES" Then Chk_Trf_Letter.Checked = True Else Chk_Trf_Letter.Checked = False
        If VI_DOC_FU_LETTER.Trim.ToUpper = "YES" Then Chk_FU_Letter.Checked = True Else Chk_FU_Letter.Checked = False
        If VI_DOC_OTHERS.Trim.ToUpper = "YES" Then Chk_OtherDoc.Checked = True : Txt_OtherDoc.Text = VI_DOC_NAME Else Chk_OtherDoc.Checked = False : Me.Txt_OtherDoc.Text = ""

        Look_InsList.EditValue = VI_INSURANCE_ID
        Look_InsList.Tag = Look_InsList.EditValue
        Look_InsList.Properties.Tag = "SHOW"

        Dim EDate As DateTime = Nothing
        If IsDate(VI_E_DATE) Then EDate = VI_E_DATE : Txt_E_Date.DateTime = EDate
    End Sub
    
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.BE_ItemName.Enabled = False : Me.BE_ItemName.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_LocList.Enabled = False : Me.Cmd_Make.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_Loc.Enabled = False

        Me.Cmd_Make.Enabled = False : Me.Cmd_Make.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Model.Enabled = False : Me.Cmd_Model.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.RAD_RegPattern.Enabled = False : Me.RAD_RegPattern.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_RegNo.Enabled = False : Me.Txt_RegNo.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_RegDate.Enabled = False : Me.Txt_RegDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Ownership.Enabled = False : Me.Cmd_Ownership.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_OwnList.Enabled = False : Me.Look_OwnList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_RC_Book.Enabled = False : Me.Chk_RC_Book.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_Affidavit.Enabled = False : Me.Chk_Affidavit.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_Will.Enabled = False : Me.Chk_Will.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_Trf_Letter.Enabled = False : Me.Chk_Trf_Letter.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_FU_Letter.Enabled = False : Me.Chk_FU_Letter.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_OtherDoc.Enabled = False : Me.Chk_OtherDoc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_OtherDoc.Enabled = False : Me.Txt_OtherDoc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Others.Enabled = False : Me.Txt_Others.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_InsList.Enabled = False : Me.Look_InsList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_PolicyNo.Enabled = False : Me.Txt_PolicyNo.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_E_Date.Enabled = False : Me.Txt_E_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Chk_FU_Letter.Enabled = False : Me.Chk_FU_Letter.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Look_OwnList)
    End Sub
    Private Sub Get_Vehicle_Make_List()
        Dim d1 As DataTable = Base._Gift_DBOps.GetVehicleMake("Name", "ID")
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Cmd_Make.Properties.Items.Clear()
        For Each currRow In d1.Rows : Cmd_Make.Properties.Items.Add(currRow("Name").ToString) : Next
    End Sub
    Private Sub Get_Vehicle_Model_List()
        Dim d1 As DataTable = Base._Gift_DBOps.GetVehicleModels(Me.Cmd_Make.Text)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Cmd_Model.Properties.Items.Clear() : Cmd_Model.EditValue = ""
        For Each currRow In d1.Rows : Cmd_Model.Properties.Items.Add(currRow("Name").ToString) : Next
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '2.Address Book
    Private Sub But_Owner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Owner.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim SaveID1 As String = Me.Look_OwnList.Tag
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Ownership)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetOwnList()
            Me.Look_OwnList.EditValue = SaveID1 : Me.Look_OwnList.Tag = SaveID1 : Look_OwnList.Properties.Tag = "SHOW"
        End If
    End Sub
    Private Sub Look_OwnList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_OwnList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_OwnList.CancelPopup()
            Hide_Properties()
            Me.Cmd_Ownership.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Ownership.Focus()
        End If
    End Sub
    Private Sub Look_OwnList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_OwnList.EditValueChanged
        If Me.Look_OwnList.Properties.Tag = "SHOW" Then
            Me.Look_OwnList.Tag = Me.Look_OwnList.GetColumnValue("ID").ToString

          
        Else
        End If
    End Sub
    Private Sub LookUp_GetOwnList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetVehicleOwners()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        dview.Sort = "ID"
        If dview.Count > 0 Then
            Me.Look_OwnList.Properties.ValueMember = "ID"
            Me.Look_OwnList.Properties.DisplayMember = "Name"
            Me.Look_OwnList.Properties.DataSource = dview
            Me.Look_OwnList.Properties.PopulateColumns()
            'Me.Look_OwnList.Properties.BestFit()
            Me.Look_OwnList.Properties.PopupWidth = 400
            Me.Look_OwnList.Properties.Columns(3).Visible = False
            Me.Look_OwnList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_OwnList.EditValue = 0
            Me.Look_OwnList.Properties.Tag = "SHOW"
        Else
            Me.Look_OwnList.Properties.Tag = "NONE"
        End If
    End Sub


    '3.Insurance Company List
    Private Sub Look_InsList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_InsList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_InsList.CancelPopup()
            Hide_Properties()
            Me.Cmd_Ownership.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_InsList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Ownership.Focus()
        End If

    End Sub
    Private Sub Look_InsList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_InsList.EditValueChanged
        If Me.Look_InsList.Properties.Tag = "SHOW" Then
            Me.Look_InsList.Tag = Me.Look_InsList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetInsList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetInsuranceItems("Name", "ID")
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.Look_InsList.Properties.ValueMember = "ID"
            Me.Look_InsList.Properties.DisplayMember = "Name"
            Me.Look_InsList.Properties.DataSource = dview
            Me.Look_InsList.Properties.PopulateColumns()
            'Me.Look_InsList.Properties.BestFit()
            Me.Look_InsList.Properties.PopupWidth = 400
            Me.Look_InsList.Properties.Columns(1).Visible = False
            Me.Look_InsList.Properties.Tag = "NONE"
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then Me.Look_InsList.EditValue = 0
            Me.Look_InsList.Properties.Tag = "SHOW"
        Else
            Me.Look_InsList.Properties.Tag = "NONE"
        End If
    End Sub


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
    Private Sub Look_LocList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_LocList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_LocList.CancelPopup()
            Hide_Properties()
            SendKeys.Send("+{TAB}")
        ElseIf e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            SendKeys.Send("+{TAB}")
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
            LayoutControlItem1.AppearanceItemCaption.ForeColor = Color.Red
        Else
            LayoutControlItem1.AppearanceItemCaption.ForeColor = Color.Black
        End If
    End Sub
#End Region


    
End Class