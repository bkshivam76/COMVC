Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_Voucher_Win_Donation_F

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer
    Dim Foreign_Donation_Allow As Boolean = False
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
    Private Sub Form_Closing_Window_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormClosingEnable Then BUT_CANCEL_Click(New Object, New EventArgs)
    End Sub
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
        Base = MainBase
        Programming_Testing()
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
        If (keyData = (Keys.Control Or Keys.A)) Then ' Add DONOR
            But_DonAdd_Click(Nothing, Nothing)
            Return (True)
        End If
        If (keyData = (Keys.Control Or Keys.M)) Then ' Manage DONOR
            But_DonManage_Click(Nothing, Nothing)
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
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                'Closed Bank Acc Check #g32
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim donationF_DbOps As DataTable = Base._Donation_DBOps.GetRecord(Me.xID.Text)
                If donationF_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If donationF_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(donationF_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                Dim MaxValue As Object = 0
                MaxValue = Base._Donation_DBOps.GetStatus(Me.xID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'Special Checks 

                Dim xTemp_D_Status As String = ""
                Dim Status As DataTable = Base._Voucher_DBOps.GetDonationStatus(Me.xID.Text)
                If Status Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If Status.Rows.Count > 0 Then
                    xTemp_D_Status = Status.Rows(0)("DS_STATUS")
                End If
                If (xTemp_D_Status <> "") AndAlso (xTemp_D_Status <> "42189485-9b6b-430a-8112-0e8882596f3c") AndAlso (xTemp_D_Status <> "3a99fadc-b336-480d-8116-fbd144bd7671") AndAlso (xTemp_D_Status <> "6a7c38ba-5779-4e21-acc7-c1829b7ec933") Then '--> "Donation Accepted" or "Receipt Request Rejected" andalso "Receipt Cancelled"
                    DevExpress.XtraEditors.XtraMessageBox.Show("D o n a t i o n   E n t r y   c a n n o t   b e   E d i t e d  /   D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note: Donation Status Changed, Check Donation Register..!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            If Len(Txt_INR_Amt.Text) > 20 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   t o o   B i g . . . !", Me.Txt_INR_Amt, 0, Me.Txt_INR_Amt.Height, 5000)
                Me.Txt_INR_Amt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If

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

            If Me.GLookUp_PartyListView.FocusedRowHandle < 1 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D o n o r   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList, 0, Me.GLookUp_PartyList.Height, 5000)
                Me.GLookUp_PartyList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList)
            End If
            If (Me.BE_Add1.Text.Trim.Length <= 0) Or (Me.BE_Country.Text.Trim.Length <= 0) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D o n o r   A d d r e s s   I n c o m p l e t e . . . !" & vbNewLine & "Mandatory: Address Line.1 & Country...", Me.GLookUp_PartyList, 0, Me.GLookUp_PartyList.Height, 5000)
                Me.GLookUp_PartyList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList)
            End If

            If Len(Trim(Me.GLookUp_CatList.Tag)) = 0 Or Len(Trim(Me.GLookUp_CatList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D o n o r   C a t e g o r y   N o t   S e l e c t e d . . . !", Me.GLookUp_CatList, 0, Me.GLookUp_CatList.Height, 5000)
                Me.GLookUp_CatList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_CatList)
            End If

            If Len(Trim(Me.GLookUp_CurList.Tag)) = 0 Or Len(Trim(Me.GLookUp_CurList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("C u r r e n c y   N o t   S e l e c t e d . . . !", Me.GLookUp_CurList, 0, Me.GLookUp_CurList.Height, 5000)
                Me.GLookUp_CurList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_CurList)
            End If


            '#7152 fix
            'If (Len(Trim(Me.GLookUp_RefBankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_RefBankList.Text)) = 0) And (Cmd_Mode.Text.ToUpper <> "CASH") Then
            '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '    Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_RefBankList, 0, Me.GLookUp_RefBankList.Height, 5000)
            '    Me.GLookUp_RefBankList.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'Else
            '    Me.ToolTip1.Hide(Me.GLookUp_RefBankList)
            'End If

            'If Len(Trim(Me.Txt_Ref_Branch.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH") Then
            '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '    Me.ToolTip1.Show("B a n k   B r a n c h   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_Branch, 0, Me.Txt_Ref_Branch.Height, 5000)
            '    Me.Txt_Ref_Branch.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'Else
            '    Me.ToolTip1.Hide(Me.Txt_Ref_Branch)
            'End If

            'If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH") Then
            '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '    Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
            '    Me.Txt_Ref_No.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'Else
            '    Me.ToolTip1.Hide(Me.Txt_Ref_No)
            'End If

            'If IsDate(Me.Txt_Ref_Date.Text) = False And (Cmd_Mode.Text.ToUpper <> "CASH") Then
            '    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '    Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_Ref_Date, 0, Me.Txt_Ref_Date.Height, 5000)
            '    Me.Txt_Ref_Date.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'Else
            '    Me.ToolTip1.Hide(Me.Txt_Ref_Date)
            'End If

            If Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If

            If Val(Trim(Me.Txt_Foreign_Amt.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F o r e i g n   C u r r e n c y   A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Foreign_Amt, 0, Me.Txt_Foreign_Amt.Height, 5000)
                Me.Txt_Foreign_Amt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Foreign_Amt)
            End If

            If Val(Trim(Me.Txt_Cur_Rate.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("C u r r e n c y   R a t e   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Cur_Rate, 0, Me.Txt_Cur_Rate.Height, 5000)
                Me.Txt_Cur_Rate.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Cur_Rate)
            End If

            If Val(Trim(Me.Txt_Bank_Charges.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   C h a r g e s   A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Bank_Charges, 0, Me.Txt_Bank_Charges.Height, 5000)
                Me.Txt_Bank_Charges.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Bank_Charges)
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
            If Len(Trim(Me.GLookUp_PurList.Tag)) = 0 Or Len(Trim(Me.GLookUp_PurList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P u r p o s e   N o t   S e l e c t e d . . . !", Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                Me.GLookUp_PurList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PurList)
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

        '--------------------------// Start Dependencies //--------------------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                'Address Book(Donor) dependency check #Ref Z32
                If GLookUp_PartyList.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._Donation_DBOps.GetPartyDetails(False, GLookUp_PartyList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    Dim oldEditOn As DateTime = GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                    'Check for full address of donor
                    If d1.Rows(0)("C_R_ADD1").ToString.Length <= 0 Or d1.Rows(0)("CI_NAME").ToString.Length <= 0 Or d1.Rows(0)("CO_NAME").ToString.Length <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim BankAcc As DataTable = Base._Donation_DBOps.GetBankAccounts(True, GLookUp_BankList.Tag)
                    If BankAcc Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    Dim oldEditOn As DateTime = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If BankAcc.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = BankAcc.Rows(0)("REC_EDIT_ON")
                        If oldEditOn <> NewEditOn Then 'A/E,E/E
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
        '---------------------------// End Dependencies //---------------------------------

        'CHECKING LOCK STATUS
        Dim Old_Status_ID As String = ""
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim MaxValue As Object = 0
            MaxValue = Base._Donation_DBOps.GetStatus(Me.xID.Text)
            If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            'If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub

            'CHECKING OLD STATUS ID
            MaxValue = 0
            MaxValue = Base._Donation_DBOps.GetOldStatusID(Me.xID.Text)
            If IsDBNull(MaxValue) Then
                Old_Status_ID = ""
            Else
                Old_Status_ID = MaxValue
            End If
        End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            Dr_Led_id = iLed_ID : Cr_Led_id = "00079" : Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag 'Bank A/c.
        Else
            Cr_Led_id = iLed_ID : Dr_Led_id = "00079" : Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag  'Bank A/c.
        End If


        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherDonation = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherDonation
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new

            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_Insert_Voucher_Donation()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Foreign
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
            InParam.RefBankID = Me.GLookUp_RefBankList.Tag
            InParam.RefBranch = Me.Txt_Ref_Branch.Text
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'InParam.Ref_Date = Txt_Ref_Date.Text
            'InParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            InParam.Amount = cDecimal(Me.Txt_INR_Amt.Text)
            InParam.DonorID = Me.GLookUp_PartyList.Tag
            InParam.Narration = Me.Txt_Narration.Text
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID.Text

            'If Not Base._Donation_DBOps.Insert(InParam) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_Insert = InParam

            Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_InsertPurpose_Voucher_Donation()
            InPurpose.TxnID = Me.xID.Text
            InPurpose.PurposeID = Me.GLookUp_PurList.Tag
            InPurpose.Amount = cDecimal(Me.Txt_Amount.Text)
            InPurpose.Status_Action = Status_Action
            InPurpose.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._Donation_DBOps.InsertPurpose(InPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertPurpose = InPurpose

            Dim InFgnInfo As Common_Lib.RealTimeService.Parameter_InsertForeignInfo_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_InsertForeignInfo_Voucher_Donation()
            InFgnInfo.TxnID = Me.xID.Text
            InFgnInfo.CoBank = Me.Txt_Co_Bank.Text
            InFgnInfo.CoBranch = Me.Txt_Co_Branch.Text
            InFgnInfo.CurrID = Me.GLookUp_CurList.Tag
            InFgnInfo.CurrRate = cDecimal(Me.Txt_Cur_Rate.Text)
            InFgnInfo.ForeignAmount = cDecimal(Me.Txt_Foreign_Amt.Text)
            InFgnInfo.INR = cDecimal(Me.Txt_INR_Amt.Text)
            InFgnInfo.Bankcharges = cDecimal(Me.Txt_Bank_Charges.Text)
            InFgnInfo.NettAmt = cDecimal(Me.Txt_Amount.Text)
            InFgnInfo.CatID = Me.GLookUp_CatList.Tag
            InFgnInfo.Status_Action = Status_Action
            InFgnInfo.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._Donation_DBOps.InsertForeignInfo(InFgnInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertFgnInfo = InFgnInfo

            Dim InDStatus As Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation()
            InDStatus.TxnID = Me.xID.Text
            InDStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c"
            InDStatus.Status_Action = Status_Action
            InDStatus.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._Donation_DBOps.InsertDonationStatus(InDStatus) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertDonStatus = InDStatus

            If Not Base._Donation_DBOps.InsertDonation_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherDonation = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherDonation
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim UpParam As Common_Lib.RealTimeService.Parameter_Update_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_Update_Voucher_Donation()
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
            UpParam.RefBankID = Me.GLookUp_RefBankList.Tag
            UpParam.RefBranch = Me.Txt_Ref_Branch.Text
            UpParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then UpParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then UpParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'UpParam.Ref_Date = Txt_Ref_Date.Text
            'UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            UpParam.Amount = cDecimal(Me.Txt_INR_Amt.Text)
            UpParam.DonorID = Me.GLookUp_PartyList.Tag
            UpParam.Narration = Me.Txt_Narration.Text
            UpParam.Remarks = Me.Txt_Remarks.Text
            UpParam.Reference = Me.Txt_Reference.Text
            'UpParam.Status_Action = Status_Action
            UpParam.RecID = Me.xID.Text

            'If Not Base._Donation_DBOps.Update(UpParam) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_Update = UpParam

            Dim UpPurpose As Common_Lib.RealTimeService.Parameter_UpdatePurpose_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_UpdatePurpose_Voucher_Donation()
            UpPurpose.PurposeID = Me.GLookUp_PurList.Tag
            UpPurpose.Amount = cDecimal(Me.Txt_Amount.Text)
            'UpPurpose.Status_Action = Status_Action
            UpPurpose.RecID = Me.xID.Text

            'If Not Base._Donation_DBOps.UpdatePurpose(UpPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdatePurpose = UpPurpose

            Dim UpFgnInfo As Common_Lib.RealTimeService.Parameter_UpdateForeignInfo_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_UpdateForeignInfo_Voucher_Donation()
            UpFgnInfo.TxnID = Me.xID.Text
            UpFgnInfo.CoBank = Me.Txt_Co_Bank.Text
            UpFgnInfo.CoBranch = Me.Txt_Co_Branch.Text
            UpFgnInfo.CurrID = Me.GLookUp_CurList.Tag
            UpFgnInfo.CurrRate = cDecimal(Me.Txt_Cur_Rate.Text)
            UpFgnInfo.ForeignAmount = cDecimal(Me.Txt_Foreign_Amt.Text)
            UpFgnInfo.INR = cDecimal(Me.Txt_INR_Amt.Text)
            UpFgnInfo.Bankcharges = cDecimal(Me.Txt_Bank_Charges.Text)
            UpFgnInfo.NettAmt = cDecimal(Me.Txt_Amount.Text)
            UpFgnInfo.CatID = Me.GLookUp_CatList.Tag
            'UpFgnInfo.Status_Action = Status_Action

            'If Not Base._Donation_DBOps.UpdateForeignInfo(UpFgnInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateFgnInfo = UpFgnInfo

            If Old_Status_ID.Length <= 0 Then
                Dim InStatus As Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_InsertDonStatus_Voucher_Donation()
                InStatus.TxnID = Me.xID.Text
                InStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c"
                InStatus.Status_Action = Status_Action
                InStatus.RecID = System.Guid.NewGuid().ToString()
                'If Not Base._Donation_DBOps.InsertDonationStatus(InStatus) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InsertDonSattus = InStatus
            Else

                Dim UpStatus As Common_Lib.RealTimeService.Parameter_UpdateStatus_Voucher_Donation = New Common_Lib.RealTimeService.Parameter_UpdateStatus_Voucher_Donation()
                UpStatus.StatusID = "42189485-9b6b-430a-8112-0e8882596f3c"
                'UpStatus.Status_Action = Status_Action
                UpStatus.RecID = Me.xID.Text

                'If Not Base._Donation_DBOps.UpdateStatus(UpStatus) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_UpdateStatus = UpStatus
            End If

            If Not Base._Donation_DBOps.UpdateDonation_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherDonation = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherDonation
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                DelParam.RecID_DeletePurpose = Me.xID.Text
                'If Not Base._Donation_DBOps.DeletePurpose(Me.xID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.RecID_DeleteFgnInfo = Me.xID.Text
                'If Not Base._Donation_DBOps.DeleteForeignInfo(Me.xID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.RecID_DeleteStatus = Me.xID.Text
                'If Not Base._Donation_DBOps.DeleteStatus(Me.xID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.RecID_Delete = Me.xID.Text
                'If Not Base._Donation_DBOps.Delete(Me.xID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                If Not Base._Donation_DBOps.DeleteDonation_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If
            xPromptWindow.Dispose()
        End If
    End Sub
    Private Sub BUT_CANCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
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

    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, But_DonManage.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, But_DonManage.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, But_DonManage.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub

    Private Sub But_DonManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_DonManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Donor)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()

        End If

    End Sub
    Private Sub But_DonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_DonAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Donor)..." : xfrm.TitleX.Text = "Donor"
            xfrm.ShowDialog(Me) : If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then LookUp_GetPartyList()
            xfrm.Dispose()
        End If
    End Sub

    Private Sub Cmd_Mode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.SelectedIndexChanged
        If Cmd_Mode.Text.ToUpper = "CASH" Then
            Ref_Item.Control.Enabled = False : Corresp_Bank_Item.Control.Enabled = False
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_Bank.ForeColor = Color.Gray : lbl_Ref_Branch.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_Cdate.ForeColor = Color.Gray
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Ref. No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            lbl_Corres_Title.ForeColor = Color.Gray : lbl_Corres_Bank.ForeColor = Color.Gray : lbl_Corres_Branch.ForeColor = Color.Gray
        Else
            Ref_Item.Control.Enabled = True : Corresp_Bank_Item.Control.Enabled = True
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Ref_Title.ForeColor = Color.Black 'lbl_Ref_Bank.ForeColor = Color.Red : lbl_Ref_Branch.ForeColor = Color.Red : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red
            lbl_Ref_Bank.ForeColor = Color.Black : lbl_Ref_Branch.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Black : lbl_Ref_Date.ForeColor = Color.Black '#7152 fix
            lbl_Ref_Cdate.ForeColor = Color.Black
            If Cmd_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Cheque Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "MO" Then
                lbl_Ref_Title.Text = Cmd_Mode.Text & " Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Ref. No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_Cdate.Text = "Clearing Date:"
            End If
            lbl_Corres_Title.ForeColor = Color.Black : lbl_Corres_Bank.ForeColor = Color.Black : lbl_Corres_Branch.ForeColor = Color.Black
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
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Ref_Branch.GotFocus, Txt_Foreign_Amt.GotFocus, Txt_Foreign_Amt.Click, Txt_Bank_Charges.GotFocus, Txt_Bank_Charges.Click, Txt_Cur_Rate.GotFocus, Txt_Cur_Rate.Click
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Or txt.Name = Txt_Foreign_Amt.Name Or txt.Name = Txt_Bank_Charges.Name Or txt.Name = Txt_Cur_Rate.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Ref_No.KeyPress, Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress, Txt_Ref_Branch.KeyPress, Txt_Co_Bank.KeyPress, Txt_Co_Branch.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_Ref_Branch.KeyDown, Txt_Co_Bank.KeyDown, Txt_Co_Branch.KeyDown, Txt_Foreign_Amt.KeyDown, Txt_Cur_Rate.KeyDown, Txt_Bank_Charges.KeyDown
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
    Private Sub Txt_Foreign_Amt_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Foreign_Amt.EditValueChanged, Txt_Cur_Rate.EditValueChanged
        If Val(Txt_Foreign_Amt.Text) > 0 And Val(Txt_Cur_Rate.Text) > 0 Then
            Me.Txt_INR_Amt.Text = Format(Convert.ToDecimal(Txt_Foreign_Amt.Text) * Convert.ToDecimal(Txt_Cur_Rate.Text), "#0.00")
        Else
            Txt_INR_Amt.Text = "0.00"
        End If

        Me.Txt_Amount.Text = Format(cDecimal(Txt_INR_Amt.Text) - cDecimal(Txt_Bank_Charges.Text), "#0.00")
    End Sub
    Private Sub Txt_INR_Amt_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_INR_Amt.EditValueChanged
        Me.Txt_Amount.Text = Format(cDecimal(Txt_INR_Amt.Text) - cDecimal(Txt_Bank_Charges.Text), "#0.00")
    End Sub
    Private Sub Txt_Bank_Charges_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Bank_Charges.EditValueChanged
        Me.Txt_Amount.Text = Format(cDecimal(Txt_INR_Amt.Text) - cDecimal(Txt_Bank_Charges.Text), "#0.00")
    End Sub
    Private Function cDecimal(val As String) As Decimal
        If val.Length > 0 Then
            Return Convert.ToDecimal(val)
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Foreign Donation Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Foreign Donation"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_PartyList.Tag = "" : LookUp_GetPartyList()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        GLookUp_RefBankList.Tag = "" : LookUp_GetRefBankList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()
        GLookUp_CurList.Tag = "" : LookUp_GetCurrencyList()
        GLookUp_CatList.Tag = "" : LookUp_GetCategoryList()
        Me.Cmd_Mode.SelectedIndex = 1
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
        xPleaseWait.Hide()
    End Sub


    Private Sub Data_Binding()

        Dim d1 As DataTable = Base._Donation_DBOps.GetRecord(Me.xID.Text)
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

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
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Donation", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+
        Dim d2 As DataTable = Base._Donation_DBOps.GetPurposeRecord(Me.xID.Text)
        Dim d3 As DataTable = Base._Donation_DBOps.GetForeignRecord(Me.xID.Text)
        Dim d4 As DataTable = Base._Donation_DBOps.GetReceiptPrintRecord(Me.xID.Text)
        If d2 Is Nothing Or d3 Is Nothing Or d4 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        LastEditedOn = Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON"))
        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        If d4.Rows.Count > 0 Then
            If Not IsDBNull(d4.Rows(0)("DR_NO")) Then
                Me.BE_Receipt_No.Text = d4.Rows(0)("DR_NO")
            End If
        End If


        Cmd_Mode.DataBindings.Add("text", d1, "TR_MODE")

        If d1.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d1.Rows(0)("TR_ITEM_ID")))
            Me.GLookUp_ItemList.EditValue = d1.Rows(0)("TR_ITEM_ID")
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_ItemList.Properties.ReadOnly = False

        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If (Not IsDBNull(d1.Rows(0)("TR_SUB_CR_LED_ID")) And d1.Rows(0)("TR_CR_LED_ID") = "00079") Then Bank_ID = d1.Rows(0)("TR_SUB_CR_LED_ID")
        Else
            If Not IsDBNull(d1.Rows(0)("TR_SUB_DR_LED_ID")) And d1.Rows(0)("TR_DR_LED_ID") = "00079" Then Bank_ID = d1.Rows(0)("TR_SUB_DR_LED_ID")
        End If

        If Bank_ID.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.MoveBy(Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID))
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        If d1.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
            Me.GLookUp_PartyList.ShowPopup() : Me.GLookUp_PartyList.ClosePopup()
            Me.GLookUp_PartyListView.MoveBy(Me.GLookUp_PartyListView.LocateByValue("C_ID", d1.Rows(0)("TR_AB_ID_1")))
            Me.GLookUp_PartyList.EditValue = d1.Rows(0)("TR_AB_ID_1")
            Me.GLookUp_PartyList.Tag = Me.GLookUp_PartyList.EditValue
            Me.GLookUp_PartyList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_PartyList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_D_CAT_MISC_ID")) Then
            If d3.Rows(0)("TR_D_CAT_MISC_ID").ToString.Length > 0 Then
                Me.GLookUp_CatList.ShowPopup() : Me.GLookUp_CatList.ClosePopup()
                Me.GLookUp_CatListView.MoveBy(Me.GLookUp_CatListView.LocateByValue("CAT_ID", d3.Rows(0)("TR_D_CAT_MISC_ID")))
                Me.GLookUp_CatList.EditValue = d3.Rows(0)("TR_D_CAT_MISC_ID")
                Me.GLookUp_CatList.Tag = Me.GLookUp_CatList.EditValue
                Me.GLookUp_CatList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_CatList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_CUR_ID")) Then
            If d3.Rows(0)("TR_CUR_ID").ToString.Length > 0 Then
                Me.GLookUp_CurList.ShowPopup() : Me.GLookUp_CurList.ClosePopup()
                Me.GLookUp_CurListView.MoveBy(Me.GLookUp_CurListView.LocateByValue("CUR_ID", d3.Rows(0)("TR_CUR_ID")))
                Me.GLookUp_CurList.EditValue = d3.Rows(0)("TR_CUR_ID")
                Me.GLookUp_CurList.Tag = Me.GLookUp_CurList.EditValue
                Me.GLookUp_CurList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_CurList.Properties.ReadOnly = False


        If d1.Rows(0)("TR_REF_BANK_ID").ToString.Length > 0 Then
            Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
            Me.GLookUp_RefBankListView.MoveBy(Me.GLookUp_RefBankListView.LocateByValue("BI_ID", d1.Rows(0)("TR_REF_BANK_ID")))
            Me.GLookUp_RefBankList.EditValue = d1.Rows(0)("TR_REF_BANK_ID")
            Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankList.EditValue
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_RefBankList.Properties.ReadOnly = False

        Txt_Ref_Branch.DataBindings.Add("text", d1, "TR_REF_BRANCH")
        Txt_Ref_No.DataBindings.Add("text", d1, "TR_REF_NO")
        If Not IsDBNull(d1.Rows(0)("TR_REF_DATE")) Then
            xDate = d1.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d1.Rows(0)("TR_REF_CDATE")) Then
            xDate = d1.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If
        Txt_Co_Bank.DataBindings.Add("text", d3, "TR_CO_BANK")
        Txt_Co_Branch.DataBindings.Add("text", d3, "TR_CO_BRANCH")

        If Not IsDBNull(d2.Rows(0)("TR_PURPOSE_MISC_ID")) Then
            If d2.Rows(0)("TR_PURPOSE_MISC_ID").ToString.Length > 0 Then
                Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", d2.Rows(0)("TR_PURPOSE_MISC_ID")))
                Me.GLookUp_PurList.EditValue = d2.Rows(0)("TR_PURPOSE_MISC_ID")
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                Me.GLookUp_PurList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PurList.Properties.ReadOnly = False

        Txt_Narration.DataBindings.Add("text", d1, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d1, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d1, "TR_REFERENCE")

        Txt_Foreign_Amt.DataBindings.Add("EditValue", d3, "TR_FOREIGN_AMT")
        Txt_Cur_Rate.DataBindings.Add("EditValue", d3, "TR_CUR_RATE")
        Txt_INR_Amt.DataBindings.Add("EditValue", d3, "TR_INR_AMT")
        Txt_Bank_Charges.DataBindings.Add("EditValue", d3, "TR_BANK_CHARGES")
        Txt_Amount.DataBindings.Add("EditValue", d3, "TR_NET_AMT")
        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList.Enabled = False : Me.GLookUp_PartyList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Add1.Enabled = False : Me.BE_Add1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Add2.Enabled = False : Me.BE_Add2.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Add3.Enabled = False : Me.BE_Add3.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Add4.Enabled = False : Me.BE_Add4.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_City.Enabled = False : Me.BE_City.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Pincode.Enabled = False : Me.BE_Pincode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_State.Enabled = False : Me.BE_State.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_District.Enabled = False : Me.BE_District.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Country.Enabled = False : Me.BE_Country.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_PAN_No.Enabled = False : Me.BE_PAN_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Passport_No.Enabled = False : Me.BE_Passport_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.BE_Receipt_No.Enabled = False : Me.BE_Receipt_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_CurList.Enabled = False : Me.GLookUp_CurList.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Acc_No.Enabled = False : Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_RefBankList.Enabled = False : Me.GLookUp_RefBankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Branch.Enabled = False : Me.Txt_Ref_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Co_Bank.Enabled = False : Me.Txt_Co_Bank.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Co_Branch.Enabled = False : Me.Txt_Co_Branch.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_CatList.Enabled = False : Me.GLookUp_CatList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Foreign_Amt.Enabled = False : Me.Txt_Foreign_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Cur_Rate.Enabled = False : Me.Txt_Cur_Rate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_INR_Amt.Enabled = False : Me.Txt_INR_Amt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Bank_Charges.Enabled = False : Me.Txt_Bank_Charges.Properties.AppearanceDisabled.ForeColor = SetColor


        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_DonAdd.Enabled = False
        Me.But_DonManage.Enabled = False
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
                Me.BE_Item_Head.Text = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "LED_NAME").ToString
                iVoucher_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString
                iLed_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_LED_ID").ToString
                iTrans_Type = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TRANS_TYPE").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._Donation_DBOps.GetItemList(True)
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

    '2.GLookUp_PartyList <- FOR DONOR
    Private Sub GLookUp_PartyList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PartyList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PartyListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PartyListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PartyList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PartyListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PartyListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PartyList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PartyList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PartyList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PartyList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PartyList.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PartyList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_PartyList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList.EditValueChanged
        If Me.GLookUp_PartyList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyListView.RowCount > 0 And Val(Me.GLookUp_PartyListView.FocusedRowHandle) > 0) Then
                Me.GLookUp_PartyList.Tag = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_ID").ToString
                Me.BE_Add1.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_R_ADD1").ToString
                Me.BE_Add2.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_R_ADD2").ToString
                Me.BE_Add3.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_R_ADD3").ToString
                Me.BE_Add4.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_R_ADD4").ToString

                Me.BE_City.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "CI_NAME").ToString
                Me.BE_State.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "ST_NAME").ToString
                Me.BE_District.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "DI_NAME").ToString
                Me.BE_Country.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "CO_NAME").ToString
                Me.BE_Pincode.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_R_PINCODE").ToString
                Me.BE_PAN_No.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_PAN_NO").ToString
                Me.BE_Passport_No.Text = Me.GLookUp_PartyListView.GetRowCellValue(Me.GLookUp_PartyListView.FocusedRowHandle, "C_PASSPORT_NO").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._Donation_DBOps.GetPartyDetails(False)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "C_NAME"
        If dview.Count > 0 Then
            Dim ROW As DataRow : ROW = d1.NewRow() : ROW("C_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_PartyList.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList.Properties.DataSource = dview
            Me.GLookUp_PartyListView.RefreshData()
            Me.GLookUp_PartyList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PartyList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList.Properties.ReadOnly = False
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
            Me.Cmd_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_RefBankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Mode.Focus()
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
        Dim B2 As DataTable = Base._Donation_DBOps.GetBankList()
        If B2 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    '4.GLookUp_BankList
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
            Me.Txt_Co_Branch.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_BankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Co_Branch.Focus()
        End If

    End Sub
    Private Sub GLookUp_BankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_BankList.EditValueChanged
        If Me.GLookUp_BankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_BankListView.RowCount > 0 And Val(Me.GLookUp_BankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString
                Me.BE_Bank_Acc_No.Text = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ACC_NO").ToString
                Me.BE_Bank_Acc_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BI_SHORT_NAME").ToString

            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._Donation_DBOps.GetBankAccounts(True)
        If BA_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Donation_DBOps.GetBranchDetails(Branch_IDs)
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
                                        .REC_EDIT_ON = A.Field(Of DateTime)("REC_EDIT_ON")
                                        } : Dim Final_Data = BuildData.ToList

        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If

        If Final_Data.Count = 1 Then
            Cnt_BankAccount = 1
            Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray

            Dim DEFAULT_ID As String = "" : For Each FieldName In Final_Data : DEFAULT_ID = FieldName.BA_ID : Next
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup() : Me.GLookUp_BankList.EditValue = DEFAULT_ID : Me.GLookUp_BankList.Enabled = False
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False
        End If
    End Sub

    '5.GLookUp_PurList
    Private Sub GLookUp_PurList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_PurList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_PurList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_PurListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_PurListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_PurList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_PurList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_PurList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_PurList.CancelPopup()
            Hide_Properties()
            Me.Txt_Bank_Charges.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Bank_Charges.Focus()
        End If

    End Sub
    Private Sub GLookUp_PurList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PurList.EditValueChanged
        If Me.GLookUp_PurList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_PurListView.RowCount > 0 And Val(Me.GLookUp_PurListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurListView.GetRowCellValue(Me.GLookUp_PurListView.FocusedRowHandle, "PUR_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetPurposeList()
        Dim d1 As DataTable = Base._Donation_DBOps.GetPurposes()
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.GLookUp_PurList.Properties.ValueMember = "PUR_ID"
            Me.GLookUp_PurList.Properties.DisplayMember = "PUR_NAME"
            Me.GLookUp_PurList.Properties.DataSource = dview
            Me.GLookUp_PurListView.RefreshData()
            Me.GLookUp_PurList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PurList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            ''1
            'Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
            'Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
            'Me.GLookUp_PurList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            'Me.GLookUp_PurList.Properties.Tag = "SHOW"
            Me.GLookUp_PurList.Properties.ReadOnly = False
        End If

    End Sub


    '5.GLookUp_CurList
    Private Sub GLookUp_CurList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_CurList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_CurListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_CurListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_CurList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_CurListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_CurListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_CurList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_CurList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_CurList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_CurList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_CurList.CancelPopup()
            Hide_Properties()
            Me.GLookUp_CatList.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_CurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_CatList.Focus()
        End If

    End Sub
    Private Sub GLookUp_CurList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_CurList.EditValueChanged
        If Me.GLookUp_CurList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_CurListView.RowCount > 0 And Val(Me.GLookUp_CurListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_CurList.Tag = Me.GLookUp_CurListView.GetRowCellValue(Me.GLookUp_CurListView.FocusedRowHandle, "CUR_ID").ToString
                Me.lbl_AmtRecd.Text = "(" & Me.GLookUp_CurListView.GetRowCellValue(Me.GLookUp_CurListView.FocusedRowHandle, "CUR_CODE").ToString & ") Recd. In:"
                Me.lbl_For_Amt.Text = "(" & Me.GLookUp_CurListView.GetRowCellValue(Me.GLookUp_CurListView.FocusedRowHandle, "CUR_CODE").ToString & ") Amount:"
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetCurrencyList()
        Dim d1 As DataTable = Base._Donation_DBOps.GetCurrencies()
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.GLookUp_CurList.Properties.ValueMember = "CUR_ID"
            Me.GLookUp_CurList.Properties.DisplayMember = "CUR_NAME"
            Me.GLookUp_CurList.Properties.DataSource = dview
            Me.GLookUp_CurListView.RefreshData()
            Me.GLookUp_CurList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_CurList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            '1
            'Me.GLookUp_CurList.ShowPopup() : Me.GLookUp_CurList.ClosePopup()
            'Me.GLookUp_CurListView.MoveBy(Me.GLookUp_CurListView.LocateByValue("CUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
            'Me.GLookUp_CurList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            'Me.GLookUp_CurList.Properties.Tag = "SHOW"
            Me.GLookUp_CurList.Properties.ReadOnly = False
        End If

    End Sub


    '6.GLookUp_catList
    Private Sub GLookUp_catList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_CatList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_CatListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_CatListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_CatList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_CatListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_CatListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_CatList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_catList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_CatList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_CatList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_CatList.CancelPopup()
            Hide_Properties()
            Me.GLookUp_PartyList.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_CatList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_PartyList.Focus()
        End If

    End Sub
    Private Sub GLookUp_catList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_CatList.EditValueChanged
        If Me.GLookUp_CatList.Properties.Tag = "SHOW" Then
            If Me.GLookUp_CatListView.RowCount > 0 And Val(Me.GLookUp_CatListView.FocusedRowHandle) >= 0 Then
                Me.GLookUp_CatList.Tag = Me.GLookUp_CatListView.GetRowCellValue(Me.GLookUp_CatListView.FocusedRowHandle, "CAT_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetCategoryList()
        Dim d1 As DataTable = Base._Donation_DBOps.GetCategories()
        If d1 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(d1)
        If dview.Count > 0 Then
            Me.GLookUp_CatList.Properties.ValueMember = "CAT_ID"
            Me.GLookUp_CatList.Properties.DisplayMember = "CAT_NAME"
            Me.GLookUp_CatList.Properties.DataSource = dview
            Me.GLookUp_CatListView.RefreshData()
            Me.GLookUp_CatList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_CatList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            '1
            'Me.GLookUp_catList.ShowPopup() : Me.GLookUp_catList.ClosePopup()
            'Me.GLookUp_catListView.MoveBy(Me.GLookUp_catListView.LocateByValue("CUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
            'Me.GLookUp_catList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            'Me.GLookUp_catList.Properties.Tag = "SHOW"
            Me.GLookUp_CatList.Properties.ReadOnly = False
        End If

    End Sub
#End Region


End Class