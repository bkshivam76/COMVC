Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Reflection
Imports DevExpress.Data.Filtering

Public Class Frm_Voucher_Win_C_Box

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
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
    Private Sub Form_Closing_Window_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then BUT_CANCEL_Click(New Object, New EventArgs)
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.CancelButton = Me.BUT_CANCEL
        SetDefault()
    End Sub
    Private Sub Form_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'By Item-wise Selection
            Me.Txt_V_Date.Focus()
        Else
            Me.GLookUp_ItemList.Focus()
        End If
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If (keyData = (Keys.Control Or Keys.A)) Then ' Add Party / Person
            But_PersAdd_Click(Nothing, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.M)) Then ' Manage Party / Person
            But_PersManage_Click(Nothing, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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
        If (keyData = (Keys.F2)) Then
            Txt_V_Date.Focus()
            Return (True)
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub Hyper_Request_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Hyper_Request.Click
        Drop_Request("Send Your Request to Madhuban for (" & Me.Text & ")...", Me.Text, Me)
    End Sub
    Private Sub BUT_SAVE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_SAVE_COM.Click, BUT_DEL.Click
        Hide_Properties()

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim collectionBox_DbOps As DataTable = Base._CollectionBox_DBOps.GetRecord(Me.xID.Text)
                If collectionBox_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If collectionBox_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contribution"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Contribution"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'CHECKING LOCK STATUS
                Dim MaxValue As Object = 0
                MaxValue = Base._CollectionBox_DBOps.GetStatus(Me.xID.Text)
                If MaxValue Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox("Information...", "E n t r y   N o t   F o u n d . . . !", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                End If

                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox("Information...", "L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._599x220, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(collectionBox_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contribution"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record already Changed!!", Common_Lib.Messages.RecordChanged("Current Contribution"), Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Or Len(Trim(Me.GLookUp_ItemList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("I t e m   N a m e   N o t   S e l e c t e d . . . !", Me.GLookUp_ItemList, 0, Me.GLookUp_ItemList.Height, 5000)
                Me.GLookUp_ItemList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ItemList)
            End If

            If IsDate(Me.Txt_V_Date.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                Me.Txt_V_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_V_Date)
            End If
            If IsDate(Me.Txt_V_Date.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_V_Date.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_V_Date.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F i r s t   P e r s o n   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
                Me.GLookUp_PartyList1.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
            End If

            If Len(Trim(Me.GLookUp_PartyList2.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList2.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("S e c o n d   P e r s o n   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList2, 0, Me.GLookUp_PartyList2.Height, 5000)
                Me.GLookUp_PartyList2.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList2)
            End If

            If Trim(Me.GLookUp_PartyList1.Tag) = Trim(Me.GLookUp_PartyList2.Tag) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B o t h   P e r s o  n   N a m e   c a n n o t   b e   S a m e . . . !", Me.GLookUp_PartyList2, 0, Me.GLookUp_PartyList2.Height, 5000)
                Me.GLookUp_PartyList2.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList2)
            End If

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If (Len(Trim(Me.GLookUp_RefBankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_RefBankList.Text)) = 0) And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_RefBankList, 0, Me.GLookUp_RefBankList.Height, 5000)
                Me.GLookUp_RefBankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_RefBankList)
            End If
            If Len(Trim(Me.GLookUp_RefBankList.Text)) = 0 Then Me.GLookUp_RefBankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_Branch.Text)) = 0 And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   B r a n c h   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_Branch, 0, Me.Txt_Ref_Branch.Height, 5000)
                Me.Txt_Ref_Branch.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Branch)
            End If
            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
                Me.Txt_Ref_Date.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            End If
            If IsDate(Me.Txt_Ref_CDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, DateValue(Me.Txt_Ref_CDate.Text), DateValue(Me.Txt_Ref_Date.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("C l e a r i n g   D a t e  C a n n o t   b e   l e s s   t h a n   R e f e r e n c e   D a t e!!", Me.Txt_Ref_CDate, 0, Me.Txt_Ref_CDate.Height, 5000)
                    Me.Txt_Ref_CDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Ref_CDate)
                End If
            End If
        End If

        '---------------------------// Start Dependencies //-----------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                '    Dim cnt = Base._CollectionBox_DBOps.GetBankAccounts(GLookUp_BankList.Tag).Rows.Count
                '    If cnt <= 0 Then
                '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Me.DialogResult = Windows.Forms.DialogResult.Retry
                '        FormClosingEnable = False : Me.Close()
                '        Exit Sub
                '    End If
                Dim NewEditOn As DateTime
                Dim oldEditOn As DateTime
                If GLookUp_PartyList1.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._CollectionBox_DBOps.GetAddresses(GLookUp_PartyList1.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    For I = 0 To GLookUp_PartyList1View.RowCount
                        If GLookUp_PartyList1View.GetRowCellValue(I, "C_ID") = d1.Rows(0)("C_ID") Then
                            oldEditOn = GLookUp_PartyList1View.GetRowCellValue(I, "REC_EDIT_ON")
                        End If
                    Next
                    If d1.Rows.Count <= 0 Then 'A/D,E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Base.ShowMessagebox("Referred Record Already Deleted!!", Common_Lib.Messages.DependencyChanged("Address Book"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                        : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            'Base.ShowMessagebox("Referred Record Already changed!!", Common_Lib.Messages.DependencyChanged("Address Book"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                            : Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                If GLookUp_PartyList2.Tag.ToString.Length > 0 Then
                    Dim d2 As DataTable = Base._CollectionBox_DBOps.GetAddresses(GLookUp_PartyList2.Tag)
                    For I = 0 To GLookUp_PartyList2View.RowCount
                        If GLookUp_PartyList2View.GetRowCellValue(I, "C_ID") = d2.Rows(0)("C_ID") Then
                            oldEditOn = GLookUp_PartyList2View.GetRowCellValue(I, "REC_EDIT_ON")
                        End If
                    Next
                    If d2.Rows.Count <= 0 Then 'A/D,E/D
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Base.ShowMessagebox("Referred Record Already Deleted!!", Common_Lib.Messages.DependencyChanged("Address Book"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                        : Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = d2.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            'Base.ShowMessagebox("Referred Record Already changed!!", Common_Lib.Messages.DependencyChanged("Address Book"), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                            : Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
        '--------------------------// End Dependencies //----------------------

        ''CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._CollectionBox_DBOps.GetStatus(Me.xID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Then
                    Cr_Led_id = "00080" 'Cash A/c.
                Else
                    Cr_Led_id = "00079" 'Bank A/c.
                    Sub_Cr_Led_ID = "" ' Me.GLookUp_BankList.Tag 'Bank A/c. # Bug 4440
                End If

            Else
                Cr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Then
                    Dr_Led_id = "00080" 'Cash A/c.
                Else
                    Dr_Led_id = "00079" 'Bank A/c.
                    Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag 'Bank A/c.
                End If
            End If
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_Voucher_CollectionBox = New Common_Lib.RealTimeService.Parameter_Insert_Voucher_CollectionBox()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Collection_Box
            InParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
            'InParam.TDate = Txt_V_Date.Text
            InParam.ItemID = Me.GLookUp_ItemList.Tag
            InParam.Type = iTrans_Type
            InParam.Cr_Led_ID = Cr_Led_id
            InParam.Dr_Led_ID = Dr_Led_id
            InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            InParam.Mode = Me.Cmd_Mode.Text
            InParam.Ref_Bank_ID = Me.GLookUp_RefBankList.Tag
            InParam.Ref_Branch = Me.Txt_Ref_Branch.Text
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'InParam.Ref_Date = Txt_Ref_Date.Text
            'InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            InParam.Amount = Val(Me.Txt_Amount.Text)
            InParam.Witness1 = Me.GLookUp_PartyList1.Tag
            InParam.Witness2 = Me.GLookUp_PartyList2.Tag
            InParam.Narration = Me.Txt_Narration.Text
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID.Text

            If Base._CollectionBox_DBOps.Insert(InParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.SaveSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Exit Sub
            End If

        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Then
                    Cr_Led_id = "00080" 'Cash A/c.
                Else
                    Cr_Led_id = "00079" 'Bank A/c.
                    Sub_Cr_Led_ID = "" 'Me.GLookUp_BankList.Tag 'Bank A/c.  # Bug 4440
                End If
            Else
                Cr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Then
                    Dr_Led_id = "00080" 'Cash A/c.
                Else
                    Dr_Led_id = "00079" 'Bank A/c.
                    Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag 'Bank A/c.
                End If
            End If
            Dim UpParam As Common_Lib.RealTimeService.Parameter_Update_Voucher_CollectionBox = New Common_Lib.RealTimeService.Parameter_Update_Voucher_CollectionBox()
            UpParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.TDate = Txt_V_Date.Text
            'UpParam.TDate = Txt_V_Date.Text
            UpParam.ItemID = Me.GLookUp_ItemList.Tag
            UpParam.Type = iTrans_Type
            UpParam.Cr_Led_ID = Cr_Led_id
            UpParam.Dr_Led_ID = Dr_Led_id
            UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            UpParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            UpParam.Mode = Me.Cmd_Mode.Text
            UpParam.Ref_Bank_ID = Me.GLookUp_RefBankList.Tag
            UpParam.Ref_Branch = Me.Txt_Ref_Branch.Text
            UpParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then UpParam.Ref_Date = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_Date = Txt_V_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then UpParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'UpParam.Ref_Date = Txt_Ref_Date.Text
            'UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            UpParam.Amount = Val(Me.Txt_Amount.Text)
            UpParam.Witness1 = Me.GLookUp_PartyList1.Tag
            UpParam.Witness2 = Me.GLookUp_PartyList2.Tag
            UpParam.Narration = Me.Txt_Narration.Text
            UpParam.Remarks = Me.Txt_Remarks.Text
            UpParam.Reference = Me.Txt_Reference.Text
            'UpParam.Status_Action = Status_Action
            UpParam.RecID = Me.xID.Text

            If Base._CollectionBox_DBOps.Update(UpParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.UpdateSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Exit Sub
            End If
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                If Base._CollectionBox_DBOps.Delete(Me.xID.Text) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Base.ShowMessagebox(Me.TitleX.Text, Common_Lib.Messages.DeleteSuccess, Common_Lib.Prompt_Window.ButtonType._Information, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = DialogResult.OK
                    FormClosingEnable = False : Me.Close()
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Exit Sub
                End If
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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
    Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Person)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Person Detail)..." : xfrm.TitleX.Text = "Person Detail"
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, But_PersManage.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, But_PersManage.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, But_PersManage.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub Cmd_Mode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.SelectedIndexChanged
        If Cmd_Mode.Text.ToUpper = "CASH" Then
            LayoutControlItem1.Control.Enabled = False
            LayoutControlItem3.Control.Enabled = False
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            End If
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Gray : lbl_Bank_Name.ForeColor = Color.Gray : lbl_Branch_Name.ForeColor = Color.Gray : lbl_Acc_No.ForeColor = Color.Gray
            lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_Branch.ForeColor = Color.Gray : lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Cdate.ForeColor = Color.Gray
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
        Else
            LayoutControlItem1.Control.Enabled = True
            LayoutControlItem3.Control.Enabled = True
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            End If

            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Black : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Black : lbl_Acc_No.ForeColor = Color.Black
            lbl_Ref_Bank.ForeColor = Color.Red : lbl_Ref_Branch.ForeColor = Color.Red : lbl_Ref_Title.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red : lbl_Ref_Cdate.ForeColor = Color.Black

            If Cmd_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Cheque Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
        End If
    End Sub
    Private Sub TxtComboEditGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.GotFocus
        Dim txt As ComboBoxEdit
        txt = CType(sender, ComboBoxEdit)
        'txt.ShowPopup()
        txt.SelectionStart = txt.Text.Length
    End Sub
    Private Sub TxtComboEditKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Cmd_Mode.KeyDown
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

#Region "Start--> TextBox Events"
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Ref_Branch.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Amount.KeyPress, Txt_Ref_No.KeyPress, Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress, Txt_Ref_Branch.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_Ref_Branch.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Ref_No.Validated, Txt_Narration.Validated, Txt_Reference.Validated, Txt_Remarks.Validated, Txt_Ref_Branch.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Collection Box Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Collection Box"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        GLookUp_RefBankList.Tag = "" : LookUp_GetRefBankList()
        GLookUp_PartyList1.Tag = "" : GLookUp_PartyList2.Tag = "" : LookUp_GetPartyList()
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.Text = "New ~ " & Me.TitleX.Text
            Me.Txt_V_NO.Text = ""
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "Edit ~ " & Me.TitleX.Text
            Data_Binding()
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            Me.Text = "Delete ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "View ~ " & Me.TitleX.Text
            Me.Chk_Incompleted.Visible = False : Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'By Item-wise Selection
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
            Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            Me.GLookUp_ItemList.Properties.ReadOnly = False
            Me.Txt_V_Date.Focus()
        Else
            Me.GLookUp_ItemList.Focus()
        End If


        'ALLOW BANK ENTRY PERMISSION...............
        If Base.Allow_Bank_In_C_Box = False Then Cmd_Mode.SelectedIndex = 0 : Cmd_Mode.Enabled = False
        '...........................................

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'CHECK PERVIOUS ENTRY
            Dim TR_AB_ID_1 As String = "" : Dim TR_AB_ID_2 As String = ""
            Dim Witnesses As DataTable = Base._CollectionBox_DBOps.GetPastWitness()
            If Witnesses Is Nothing Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                Exit Sub
            End If
            If Witnesses.Rows.Count > 0 Then
                TR_AB_ID_1 = Witnesses.Rows(0)("TR_AB_ID_1")
                TR_AB_ID_2 = Witnesses.Rows(0)("TR_AB_ID_2")
            End If
            If TR_AB_ID_1.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", TR_AB_ID_1))
                Me.GLookUp_PartyList1.EditValue = TR_AB_ID_1
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
                Me.GLookUp_PartyList1.Properties.ReadOnly = False
            End If
            If TR_AB_ID_2.Length > 0 Then
                Me.GLookUp_PartyList2.ShowPopup() : Me.GLookUp_PartyList2.ClosePopup()
                Me.GLookUp_PartyList2View.MoveBy(Me.GLookUp_PartyList2View.LocateByValue("C_ID", TR_AB_ID_2))
                Me.GLookUp_PartyList2.EditValue = TR_AB_ID_2
                Me.GLookUp_PartyList2.Tag = Me.GLookUp_PartyList2.EditValue
                Me.GLookUp_PartyList2.Properties.Tag = "SHOW"
                Me.GLookUp_PartyList2.Properties.ReadOnly = False
            End If
        End If

        xPleaseWait.Hide()
    End Sub

    Private Sub Data_Binding()

        Dim d1 As DataTable = Base._CollectionBox_DBOps.GetRecord(Me.xID.Text)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
            Exit Sub
        End If
        ' Dim d2 As New Common_Lib.Get_Data(Base, "DATA", "TRANSACTION_CBOX_INFO", "select *  from TRANSACTION_CBOX_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_REC_ID= '" & Me.xID.Text & "'") : d2._dc_Connection.Close()

        Dim xDate As DateTime = Nothing
        xDate = d1.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Contribution", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'Base.ShowMessagebox("Record Already Changed!!", Common_Lib.Messages.RecordChanged("Current Contribution", viewstr), Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        LastEditedOn = Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON"))

        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        Cmd_Mode.DataBindings.Add("text", d1, "TR_MODE")

        If Not IsDBNull(d1.Rows(0)("TR_ITEM_ID")) Then
            If d1.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                'Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d1.Rows(0)("TR_ITEM_ID")))
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d1.Rows(0)("TR_ITEM_ID"))
                Me.GLookUp_ItemList.EditValue = d1.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If : Me.GLookUp_ItemList.Properties.ReadOnly = False
        End If

        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If (Not IsDBNull(d1.Rows(0)("TR_SUB_CR_LED_ID")) And IIf(IsDBNull(d1.Rows(0)("TR_CR_LED_ID")), "", d1.Rows(0)("TR_CR_LED_ID")) = "00079") Then Bank_ID = d1.Rows(0)("TR_SUB_CR_LED_ID")
        Else
            If Not IsDBNull(d1.Rows(0)("TR_SUB_DR_LED_ID")) And IIf(IsDBNull(d1.Rows(0)("TR_DR_LED_ID")), "", d1.Rows(0)("TR_DR_LED_ID")) = "00079" Then Bank_ID = d1.Rows(0)("TR_SUB_DR_LED_ID")
        End If

        If Bank_ID.ToString.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            'Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", d1.Rows(0)("TR_SUB_CR_LED_ID")))
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID)
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        If Not IsDBNull(d1.Rows(0)("TR_REF_BANK_ID")) Then
            If d1.Rows(0)("TR_REF_BANK_ID").ToString.Length > 0 Then
                Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
                'Me.GLookUp_RefBankListView.MoveBy(Me.GLookUp_RefBankListView.LocateByValue("BI_ID", d1.Rows(0)("TR_REF_BANK_ID")))
                Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", d1.Rows(0)("TR_REF_BANK_ID"))
                Me.GLookUp_RefBankList.EditValue = d1.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankList.EditValue
                Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
            End If : Me.GLookUp_RefBankList.Properties.ReadOnly = False
        End If
        Txt_Ref_Branch.DataBindings.Add("text", d1, "TR_REF_BRANCH")
        Txt_Ref_No.DataBindings.Add("text", d1, "TR_REF_NO")
        If Not IsDBNull(d1.Rows(0)("TR_REF_DATE")) Then
            xDate = d1.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d1.Rows(0)("TR_REF_CDATE")) Then
            xDate = d1.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If

        If Not IsDBNull(d1.Rows(0)("TR_AB_ID_1")) Then
            If d1.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", d1.Rows(0)("TR_AB_ID_1")))
                Me.GLookUp_PartyList1.EditValue = d1.Rows(0)("TR_AB_ID_1")
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            End If : Me.GLookUp_PartyList1.Properties.ReadOnly = False
        End If

        If Not IsDBNull(d1.Rows(0)("TR_AB_ID_2")) Then
            If d1.Rows(0)("TR_AB_ID_2").ToString.Length > 0 Then
                Me.GLookUp_PartyList2.ShowPopup() : Me.GLookUp_PartyList2.ClosePopup()
                Me.GLookUp_PartyList2View.MoveBy(Me.GLookUp_PartyList2View.LocateByValue("C_ID", d1.Rows(0)("TR_AB_ID_2")))
                Me.GLookUp_PartyList2.EditValue = d1.Rows(0)("TR_AB_ID_2")
                Me.GLookUp_PartyList2.Tag = Me.GLookUp_PartyList2.EditValue
                Me.GLookUp_PartyList2.Properties.Tag = "SHOW"
            End If : Me.GLookUp_PartyList2.Properties.ReadOnly = False
        End If
        Txt_Amount.DataBindings.Add("EditValue", d1, "TR_AMOUNT")
        Txt_Narration.DataBindings.Add("text", d1, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d1, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d1, "TR_REFERENCE")

        'D_1000 = d2._dc_DataTable.Rows(0)("TR_D_1000").ToString
        'D_500 = d2._dc_DataTable.Rows(0)("TR_D_500").ToString
        'D_100 = d2._dc_DataTable.Rows(0)("TR_D_100").ToString
        'D_50 = d2._dc_DataTable.Rows(0)("TR_D_50").ToString
        'D_20 = d2._dc_DataTable.Rows(0)("TR_D_20").ToString
        'D_10 = d2._dc_DataTable.Rows(0)("TR_D_10").ToString
        'D_5 = d2._dc_DataTable.Rows(0)("TR_D_5").ToString
        'D_2 = d2._dc_DataTable.Rows(0)("TR_D_2").ToString
        'D_1 = d2._dc_DataTable.Rows(0)("TR_D_1").ToString

        'BE_Denomination.Text = Format((D_1000 * 1000) + (D_500 * 500) + (D_100 * 100) + (D_50 * 50) + (D_20 * 20) + (D_10 * 10) + (D_5 * 5) + (D_2 * 2) + (D_1 * 1), "#0.00")
        xPleaseWait.Hide()
    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList1.Enabled = False : Me.GLookUp_PartyList1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList2.Enabled = False : Me.GLookUp_PartyList2.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Branch.Enabled = False : Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Acc_No.Enabled = False : Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_RefBankList.Enabled = False : Me.GLookUp_RefBankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Branch.Enabled = False : Me.Txt_Ref_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_PersAdd.Enabled = False
        Me.But_PersManage.Enabled = False
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.GLookUp_BankList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
        Me.ToolTip1.Hide(Me.Txt_Amount)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_ItemList
    Private Sub GLookUp_ItemList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_ItemList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_ItemList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_ItemListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_ItemListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_ItemList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_ItemList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_ItemList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_ItemList.CancelPopup()
            Hide_Properties()
            'Me.Txt_No.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_ItemList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            'Me.List_Lang.Focus()
        End If

    End Sub
    Private Sub GLookUp_ItemList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.EditValueChanged
        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_ItemListView.RowCount > 0 And Val(Me.GLookUp_ItemListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_ID").ToString
                BE_Item_Head.Text = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_NAME").ToString
                iVoucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                iLed_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LED_ID").ToString
                iTrans_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TRANS_TYPE").ToString

            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._CollectionBox_DBOps.GetItemList()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "ITEM_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_ItemList.Properties.ValueMember = "ITEM_ID"
            Me.GLookUp_ItemList.Properties.DisplayMember = "ITEM_NAME"
            Me.GLookUp_ItemList.Properties.DataSource = dview
            Me.GLookUp_ItemListView.RefreshData()
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_ItemList.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup() : Me.GLookUp_ItemList.EditValue = dview.Item(0).Row("ITEM_ID").ToString : Me.GLookUp_ItemList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ItemList.Properties.ReadOnly = False
        End If
    End Sub

    '2.GLookUp_BankList
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
            Me.Cmd_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Mode.Focus()
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
    Private Sub GLookUp_BankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BI_SHORT_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._CollectionBox_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._CollectionBox_DBOps.GetBranches(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Base.ShowMessagebox("Error!!", Common_Lib.Messages.SomeError, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue, "OK", "OK")
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
                                        .BA_ID = A.Field(Of String)("ID")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            Me.Cmd_Mode.Enabled = False
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        If Final_Data.Count = 1 Then
            Cnt_BankAccount = 1
            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray

            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False
        Else
            Cnt_BankAccount = Final_Data.Count
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
        End If
    End Sub

    '3.GLookUp_RefBankList
    Private Sub GLookUp_RefBankList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_RefBankList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_RefBankListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_RefBankListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_RefBankList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_RefBankListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_RefBankListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_RefBankList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_RefBankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_RefBankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_RefBankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_RefBankList.CancelPopup()
            Hide_Properties()
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_RefBankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
        End If

    End Sub
    Private Sub GLookUp_RefBankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_RefBankList.EditValueChanged
        If Me.GLookUp_RefBankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_RefBankListView.RowCount > 0 And Val(Me.GLookUp_RefBankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankListView.GetRowCellValue(Me.GLookUp_RefBankListView.FocusedRowHandle, "BI_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetRefBankList()
        'bank
        Dim B2 As DataTable = Base._CollectionBox_DBOps.GetBanks()
        If B2 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(B2)
        If dview.Count > 0 Then
            Me.GLookUp_RefBankList.Properties.ValueMember = "BI_ID"
            Me.GLookUp_RefBankList.Properties.DisplayMember = "BI_BANK_NAME"
            Me.GLookUp_RefBankList.Properties.DataSource = dview
            Me.GLookUp_RefBankListView.RefreshData()
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_RefBankList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_RefBankList.Properties.ReadOnly = False
    End Sub

    '4.GLookUp_PartyList1
    Private Sub GLookUp_PartyList1_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList1.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList1.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyList1View.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyList1View.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList1.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList2_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList2.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyList2View.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyList2View.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList2.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyList2View.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyList2View.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList2.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList1.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList1.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList1.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_PartyList2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList2.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList2.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList2.CancelPopup()
            Hide_Properties()
            Me.GLookUp_PartyList1.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList2.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_PartyList1.Focus()
        End If
    End Sub
    Private Sub GLookUp_PartyList1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList1View.RowCount > 0 And Val(Me.GLookUp_PartyList1View.FocusedRowHandle) >= 0) Then
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList2_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList2.EditValueChanged
        If Me.GLookUp_PartyList2.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList2View.RowCount > 0 And Val(Me.GLookUp_PartyList2View.FocusedRowHandle) >= 0) Then
                Me.GLookUp_PartyList2.Tag = Me.GLookUp_PartyList2View.GetRowCellValue(Me.GLookUp_PartyList2View.FocusedRowHandle, "C_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._CollectionBox_DBOps.GetAddresses()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.GLookUp_PartyList1.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList1.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList1.Properties.DataSource = dview
            Me.GLookUp_PartyList1View.RefreshData()
            Me.GLookUp_PartyList1.Properties.Tag = "SHOW"

            Me.GLookUp_PartyList2.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList2.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList2.Properties.DataSource = dview
            Me.GLookUp_PartyList2View.RefreshData()
            Me.GLookUp_PartyList2.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PartyList1.Properties.Tag = "NONE"
            Me.GLookUp_PartyList2.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList1.Properties.ReadOnly = False : Me.GLookUp_PartyList2.Properties.ReadOnly = False
    End Sub

#End Region

End Class