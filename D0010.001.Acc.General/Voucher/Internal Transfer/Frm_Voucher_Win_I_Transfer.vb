Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Voucher_Win_I_Transfer

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Public iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""

    Dim HQ_IDs As String = ""
    Public USE_CROSS_REF As Boolean = False
    Public CROSS_REF_ID As String = ""
    Public CROSS_M_ID As String = ""
    Public iTO_CEN_ID As String = ""
    Public iFR_CEN_ID As String = ""
    Public FR_REC_EDIT_ON As DateTime
    Public Ref_Bank_ID As String = ""
    Public Ref_Branch As String = ""
    Public _a_Item_ID As String = ""
    Public _Date As String = ""
    Public _Mode As String = ""
    Public _CEN_ID As String = ""
    Public _BI_ID As String = ""
    Public _REF_BI_ID As String = ""
    Public _BI_ACC_NO As String = ""
    Public _REF_BRANCH As String = ""
    Public _REF_BANK_ACC_NO As String = ""
    Public _BI_BRANCH As String = ""
    Public _BI_REF_NO As String = ""
    Public _REF_OTHERS As String = ""
    Public _BI_REF_DT As String = ""
    Public _Amount As String = ""
    Public _a_PUR_ID As String = ""
    Public _Accepted_From_Register As Boolean = False

    Dim SuperToolTip5 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
    Dim ToolTipTitleItem_01 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
    Dim ToolTipSeparatorItem_01 As DevExpress.Utils.ToolTipSeparatorItem = New DevExpress.Utils.ToolTipSeparatorItem()
    Dim ToolTipTitleItem_02 As DevExpress.Utils.ToolTipTitleItem = New DevExpress.Utils.ToolTipTitleItem()
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Public ShowChangeItemEffects As Boolean = True

    Public Selected_Bank_ID As String = ""
    Public Selected_Mode As String = ""
    Public Selected_Item_ID As String = ""
    Public Selected_V_Date As DateTime
    Public Selected_Trans_Type As String = ""

    Public Selected_Amount As Double = 0
    Public Selected_Ref_Date As DateTime
    Public Selected_RefC_Date As DateTime
    Public Selected_Drawee_Bank_ID As String = ""
    Public Selected_Drawee_Branch As String = ""
    Public Selected_RefNo As String = ""
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()

        SuperToolTip5.AllowHtmlText = DevExpress.Utils.DefaultBoolean.[True]
        ToolTipTitleItem_01.Text = "If Cheque / D.D. Number is less than six characters please type zero in the beginning."
        ToolTipTitleItem_02.LeftIndent = 6
        ToolTipTitleItem_02.Text = "<b></b>Example: <b>Cheque / D.D.No. 123</b> has to be entered as <b>000123</b>"

        SuperToolTip5.Items.Add(ToolTipTitleItem_01)
        SuperToolTip5.Items.Add(ToolTipSeparatorItem_01)
        SuperToolTip5.Items.Add(ToolTipTitleItem_02)
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
            If Selected_Item_ID.Length = 0 Then
                Me.GLookUp_ItemList.Focus()
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then ShowChangeItemEffects = False
            Else
                ShowChangeItemEffects = False
                If iTrans_Type.ToUpper = "DEBIT" Then Me.GLookUp_ToCen_List.Focus() Else Me.GLookUp_FrCen_List.Focus()
                ShowChangeItemEffects = True
            End If
        End If
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
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
        If (keyData = (Keys.Control Or Keys.C) And Base._IsVolumeCenter And Me.iTrans_Type = "CREDIT") Then 'Convert to Donation
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                BUT_CONVERT_Click(BUT_CONVERT, Nothing)
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
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then
                'Closed Bank Acc Check #g39
                Dim AccNo As Object = Base._Voucher_DBOps.GetBankAccount(Me.GLookUp_BankList.Tag, "")
                If AccNo Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If IsDBNull(AccNo) Then AccNo = ""
                If AccNo.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If

            
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim internalTf_DbOps As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(Me.xMID.Text, 1)
                If internalTf_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If internalTf_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transfer"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(internalTf_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transfer"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'CHECKING LOCK STATUS

                Dim MaxValue As Object = 0
                MaxValue = Base._Internal_Tf_Voucher_DBOps.GetStatus(Me.xID_1.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'Special Checks

                Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.xID_1.Text)
                If Status Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                Dim xCross_Ref_Id As String = ""
                If Status Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If Status.Rows.Count > 0 Then
                    If Not IsDBNull(Status.Rows(0)("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = Status.Rows(0)("TR_TRF_CROSS_REF_ID")
                End If
                If Not IsDBNull(xCross_Ref_Id) Then
                    If xCross_Ref_Id.Length > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   I n t e r n a l  T r a n s f e r   c a n n o t   b e    E d i t e d / D e l e t e d. . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

            If Len(Trim(Me.GLookUp_ItemList.Tag)) = 0 Then
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

            If Len(Trim(Me.GLookUp_FrCen_List.Tag)) = 0 Or Me.GLookUp_FrCen_List.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F r o m   C e n t r e   N o t   S e l e c t e d . . . !", Me.GLookUp_FrCen_List, 0, Me.GLookUp_FrCen_List.Height, 5000)
                Me.GLookUp_FrCen_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FrCen_List)
            End If

            If Len(Trim(Me.GLookUp_ToCen_List.Tag)) = 0 Or Me.GLookUp_ToCen_List.Text.Length = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("T o   C e n t r e   N o t   S e l e c t e d . . . !", Me.GLookUp_ToCen_List, 0, Me.GLookUp_ToCen_List.Height, 5000)
                Me.GLookUp_ToCen_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ToCen_List)
            End If

            If Me.GLookUp_FrCen_List.Tag = Me.GLookUp_ToCen_List.Tag Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B o t h   C e n t r e   a r e   S a m e . . . !", Me.GLookUp_ToCen_List, 0, Me.GLookUp_ToCen_List.Height, 5000)
                Me.GLookUp_ToCen_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_ToCen_List)
            End If

            Dim BankSelect As Boolean = False
            If Cmd_Mode.Text.ToUpper = "CASH" Then
                BankSelect = False
            ElseIf Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False Then
                BankSelect = False
            ElseIf Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF Then 'Entry Accept form Pending List
                BankSelect = True
            ElseIf Cmd_Mode.Text.ToUpper = "CASH TO BANK" And iTrans_Type.ToUpper = "DEBIT" Then
                BankSelect = False
            Else
                BankSelect = True
            End If

            If BankSelect Then
                If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                    Me.GLookUp_BankList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_BankList)
                End If
            End If

            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""


            Dim BANK_FLAG As Boolean = False
            Dim BRANCH_FLAG As Boolean = False
            Dim ACC_FLAG As Boolean = False

            If iTrans_Type.ToUpper = "DEBIT" Then
                If (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT") Then
                    BANK_FLAG = True : BRANCH_FLAG = True : ACC_FLAG = True
                ElseIf Cmd_Mode.Text.ToUpper = "CASH TO DD" Or Cmd_Mode.Text.ToUpper = "DD" Then
                    BANK_FLAG = True : BRANCH_FLAG = True : ACC_FLAG = False
                ElseIf Cmd_Mode.Text.ToUpper = "CASH TO BANK" And iTrans_Type.ToUpper = "DEBIT" Then
                    BANK_FLAG = True : BRANCH_FLAG = True : ACC_FLAG = True
                Else
                    BANK_FLAG = False : BRANCH_FLAG = False : ACC_FLAG = False
                End If
            Else
                If Cmd_Mode.Text.ToUpper = "DD" Then ''Cmd_Mode.Text.ToUpper = "CHEQUE" Or 
                    BANK_FLAG = True : BRANCH_FLAG = True : ACC_FLAG = False
                Else
                    BANK_FLAG = False : BRANCH_FLAG = False : ACC_FLAG = False
                End If
            End If


            If BANK_FLAG Then
                If (Len(Trim(Me.GLookUp_TrfBankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_TrfBankList.Text)) = 0) And Cmd_Mode.Text.ToUpper <> "CASH" Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_TrfBankList, 0, Me.GLookUp_TrfBankList.Height, 5000)
                    Me.GLookUp_TrfBankList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_TrfBankList)
                End If
                If Len(Trim(Me.GLookUp_TrfBankList.Text)) = 0 Then Me.GLookUp_TrfBankList.Tag = ""
            End If

            If BRANCH_FLAG Then
                If Len(Trim(Me.Txt_Trf_Branch.Text)) = 0 And Cmd_Mode.Text.ToUpper <> "CASH" Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("B a n k   B r a n c h   N o t   S p e c i f i e d . . . !", Me.Txt_Trf_Branch, 0, Me.Txt_Trf_Branch.Height, 5000)
                    Me.Txt_Trf_Branch.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Trf_Branch)
                End If
            End If
            If ACC_FLAG Then
                If Len(Trim(Me.Txt_Trf_ANo.Text)) = 0 And Cmd_Mode.Text.ToUpper <> "CASH" Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("B a n k   A / c .   N o .   S p e c i f i e d . . . !", Me.Txt_Trf_ANo, 0, Me.Txt_Trf_ANo.Height, 5000)
                    Me.Txt_Trf_ANo.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_Trf_ANo)
                End If
            End If

            If iTrans_Type.ToUpper = "DEBIT" And Len(Trim(Me.Txt_DD_Fr_Chq_No.Text)) = 0 And Cmd_Mode.Text.ToUpper = "DD" Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("C h e q u e   N o .   N o t   S p e c i f i e d . . . !", Me.Txt_DD_Fr_Chq_No, 0, Me.Txt_DD_Fr_Chq_No.Height, 5000)
                Me.Txt_DD_Fr_Chq_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_DD_Fr_Chq_No)
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

            If Val(Trim(Me.Txt_Amount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If Val(Trim(Me.Txt_Bank_Chg.Text)) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   C h a r g e s   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_Bank_Chg, 0, Me.Txt_Bank_Chg.Height, 5000)
                Me.Txt_Bank_Chg.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Bank_Chg)
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

            'If Me.GLookUp_BankListView.FocusedRowHandle >= 0 Then
            '    If Len(Ref_Bank_ID) > 0 And (Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ID").ToString() <> Ref_Bank_ID Or BE_Bank_Branch.Text <> Ref_Branch) Then
            '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
            '        Me.ToolTip1.Show("R e c e i v i n g   B a n k    A c c o u n t   d i f f e r e n t   f r o m   o n e    S e l e c t e d   b y   s e n d i n g   c e n t r e. . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
            '        Me.GLookUp_BankList.Focus()
            '        Me.DialogResult = Windows.Forms.DialogResult.None
            '        Exit Sub
            '    Else
            '        Me.ToolTip1.Hide(Me.GLookUp_BankList)
            '    End If
            'End If
            '------------------------------------------------------------------
            If USE_CROSS_REF = False Then
                Dim MaxValue As Object = 0 : Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
                Dim IsEdit As Boolean = False : If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then IsEdit = True
                If iTrans_Type.ToUpper = "DEBIT" Then
                    Tr_AB_ID_1 = Me.GLookUp_ToCen_List.Tag : Tr_AB_ID_2 = Me.GLookUp_FrCen_List.Tag
                Else
                    Tr_AB_ID_1 = Me.GLookUp_FrCen_List.Tag : Tr_AB_ID_2 = Me.GLookUp_ToCen_List.Tag
                End If
                If Cmd_Mode.Text.ToUpper = "CASH" Then
                    '--------------------
                    'CHECKING CASH ENTRY
                    '--------------------

                    '---> CASE {1} Duplicate cash entry in same item + centre + date..?
                    MaxValue = Base._Internal_Tf_Voucher_DBOps.GetCashTxnCount(Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer, Me.GLookUp_ItemList.Tag, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short), IsEdit, Me.xMID.Text)
                    If MaxValue Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If MaxValue <> 0 Then
                        If Not Base.AllowMultiuser() Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Cash Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  on  " & Format(Txt_V_Date.DateTime, Base._Date_Format_Current) & "  already Exists...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   E d i t   t h e   E x i s t i n g   E n t r y . . . !", "Internal Transfer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show("Cash Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  on  " & Format(Txt_V_Date.DateTime, Base._Date_Format_Current) & "  already Exists...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   E d i t   t h e   E x i s t i n g   E n t r y . . . !", "Referred Record Already Changed...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If

                    End If

                    '---> CASE {2} Pending cash entry of same item + centre + date..?
                    MaxValue = Base._Internal_Tf_Voucher_DBOps.GetCashPendingTxnCount(Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer, Me.GLookUp_ItemList.Tag, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short), IsEdit, Me.xMID.Text)
                    If MaxValue Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If MaxValue <> 0 Then
                        If Not Base.AllowMultiuser() Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Cash Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  on  " & Format(Txt_V_Date.DateTime, Base._Date_Format_Current) & "  already Exists in Pending List...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   S e l e c t   S a m e   E n t r y   f r o m   P e n d i n g   L i s t . . . !", "Internal Transfer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show("Cash Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  on  " & Format(Txt_V_Date.DateTime, Base._Date_Format_Current) & "  already Exists in Pending List...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   S e l e c t   S a m e   E n t r y   f r o m   P e n d i n g   L i s t . . . !", "Referred Record Already Changed...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                Else
                    '-----------------------
                    'CHECKING NON-CASH ENTRY
                    '-----------------------
                    Dim _RefBankID As String = ""
                    If iTrans_Type.ToUpper = "DEBIT" Then
                        _RefBankID = Me.lbl_Trf_ANo.Tag
                    Else
                        _RefBankID = GLookUp_TrfBankList.Tag
                    End If
                    '---> CASE {1} Duplicate non cash entry of same mode + item + centre + ref.no..?
                    MaxValue = Base._Internal_Tf_Voucher_DBOps.GetNonCashTxnCount(Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer, Me.GLookUp_ItemList.Tag, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short), Me.Cmd_Mode.Text.ToUpper, _RefBankID, Me.Txt_Ref_No.Text, IsEdit, Me.xMID.Text)
                    If MaxValue Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If MaxValue <> 0 Then
                        If Not Base.AllowMultiuser() Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Me.Cmd_Mode.Text & " Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  of  " & lbl_Ref_No.Text & " " & Txt_Ref_No.Text & " already Exists...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   E d i t   t h e   E x i s t i n g   E n t r y . . . !", "Internal Transfer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(Me.Cmd_Mode.Text & " Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  of  " & lbl_Ref_No.Text & " " & Txt_Ref_No.Text & " already Exists...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   E d i t   t h e   E x i s t i n g   E n t r y . . . !", "Referred Record Already Changed...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                    '---> CASE {2} Pending non cash entry of same mode + item + centre + ref.no..?
                    MaxValue = Base._Internal_Tf_Voucher_DBOps.GetNonCashPendingTxnCount(Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer, Me.GLookUp_ItemList.Tag, Tr_AB_ID_1, Tr_AB_ID_2, Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short), Me.Cmd_Mode.Text.ToUpper, _RefBankID, Me.Txt_Ref_No.Text, IsEdit, Me.xMID.Text)
                    If MaxValue Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If MaxValue <> 0 Then
                        If Not Base.AllowMultiuser() Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Me.Cmd_Mode.Text & " Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  of  " & lbl_Ref_No.Text & " " & Txt_Ref_No.Text & " already Exists in Pending List...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   S e l e c t   S a m e   E n t r y   f r o m   P e n d i n g   L i s t . . . !", "Internal Transfer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.None
                            Exit Sub
                        Else
                            DevExpress.XtraEditors.XtraMessageBox.Show(Me.Cmd_Mode.Text & " Transfer Entry from  " & GLookUp_FrCen_List.Text & "  to  " & GLookUp_ToCen_List.Text & "  of  " & lbl_Ref_No.Text & " " & Txt_Ref_No.Text & " already Exists in Pending List...!" & vbNewLine & vbNewLine & "N o t e :   P l e a s e   S e l e c t   S a m e   E n t r y   f r o m   P e n d i n g   L i s t . . . !", "Referred Record Already Changed...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
            End If
            '------------------------------------------------------------------
            If iTrans_Type.ToUpper = "CREDIT" Then
                If BankSelect Then
                    If Val(Txt_Slip_No.Text) > 0 Then
                        Dim BankAccIDTable As DataTable = Base._DepositSlipsDBOps.GetList(Val(Txt_Slip_No.Text), GLookUp_BankList.Tag)
                        If BankAccIDTable.Rows.Count > 0 Then '--Slip Exists
                            If Not IsDBNull(BankAccIDTable.Rows(0)("BA_REC_ID")) Then
                                If BankAccIDTable.Rows(0)("BA_REC_ID") <> GLookUp_BankList.Tag Then
                                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                    Me.ToolTip1.Show("S e l e c t e d   s l i p   h a s   t r a n s a c t i o n s   o f   o t h e r   b a n k . . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                                    Me.Txt_Slip_No.Focus()
                                    Me.DialogResult = Windows.Forms.DialogResult.None
                                    Exit Sub
                                Else
                                    Me.ToolTip1.Hide(Me.Txt_Slip_No)
                                End If
                            End If

                            If Not IsDBNull(BankAccIDTable.Rows(0)("Date of Print")) Then
                                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                Me.ToolTip1.Show("S e l e c t e d   s l i p   i s    a l r e a d y   p r i n t e d . . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                                Me.Txt_Slip_No.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            Else
                                Me.ToolTip1.Hide(Me.Txt_Slip_No)
                            End If
                        End If
                    ElseIf Cmd_Mode.Text.ToUpper <> "CASH" Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("S l i p   n o.  c a n n o t   b e   n e g a t i v e   o r  Z e r o. . . !", Me.Txt_Slip_No, 0, Me.Txt_Slip_No.Height, 5000)
                        Me.Txt_Slip_No.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    End If
                End If
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

        '----------------------------- // Start Dependencies //-------------------------------

        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim oldEditOn As DateTime
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    oldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                        If NewEditOn <> oldEditOn Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If


                If GLookUp_TrfBankList.Tag.ToString.Length > 0 Then
                    Dim Not_App As Boolean
                    If iTrans_Type = "DEBIT" And Cmd_Mode.Text.ToUpper = "CHEQUE" Then
                        Not_App = True
                    Else
                        Not_App = False
                    End If
                    If Not Not_App Then
                        If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CHEQUE" Then
                            Dim XCEN_ID As String = "" : If iTrans_Type.ToUpper = "DEBIT" Then XCEN_ID = iTO_CEN_ID Else XCEN_ID = iFR_CEN_ID
                            Dim d2 As DataTable = Base._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(True, XCEN_ID, Txt_Trf_ANo.Text)
                            'Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BA_ACCOUNT_NO", Txt_Trf_ANo.Text) '#5539 fix
                            oldEditOn = GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "REC_EDIT_ON")
                            'Else
                            '    B2 = Base._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(False)
                            If d2.Rows.Count <= 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Transfer Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            Else
                                Dim NewEditOn As DateTime = d2.Rows(0)("REC_EDIT_ON")
                                If NewEditOn <> oldEditOn Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Transfer Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If

            End If
        End If

        '------------------------------- // End Dependencies // -----------------------------------

        'Entry made by other centre has been changed/deleted in other centre
        If CROSS_M_ID.ToString.Length > 0 Then 'bug #4944
            Dim AccTf As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(CROSS_M_ID, 1)
            If AccTf Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            Dim oldEditOn As DateTime = FR_REC_EDIT_ON
            If AccTf.Rows.Count = 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Internal Transfer entry made by other centre has been deleted"), "Information....!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.DialogResult = Windows.Forms.DialogResult.Retry
                FormClosingEnable = False : Me.Close()
                Exit Sub
            Else
                Dim NewEditOn As DateTime = AccTf.Rows(0)("REC_EDIT_ON")
                If NewEditOn <> oldEditOn Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Internal Transfer entry made by other centre has been changed"), "Information....!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If

        'CHECKING LOCK STATUS

        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._Internal_Tf_Voucher_DBOps.GetStatus(Me.xID_1.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted


        '+----LEDGER DETAIL FOR BANK CHARGES----+
        Dim BC_Item_ID As String = "a044fa70-5398-483f-9e63-47ba9386da4b" : Dim BC_Dr_Led_id As String = "" : Dim BC_Cr_Led_id As String = ""
        Dim BC_Sub_Dr_Led_ID As String = "" : Dim BC_Sub_Cr_Led_ID As String = ""

        If (Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection) And _
           Val(Me.Txt_Bank_Chg.Text) > 0 Then

            Dim BC_DT As DataTable = Base._Internal_Tf_Voucher_DBOps.GetItemsByID(BC_Item_ID)
            If BC_DT Is Nothing Then
                Base.HandleDBError_OnNothingReturned()
                Exit Sub
            End If
            If BC_DT.Rows.Count > 0 Then
                BC_Dr_Led_id = BC_DT.Rows(0)("ITEM_LED_ID")
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("Bank Charges Item Not Found..!", "Internal Transfer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
            If Cmd_Mode.Text.ToUpper = "CASH" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then
                BC_Cr_Led_id = "00080" 'Cash A/c.
            Else
                BC_Cr_Led_id = "00079" 'Bank A/c.
                BC_Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
            End If
        End If
        '+-----------------END------------------+

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_InternalTransfer = New Common_Lib.RealTimeService.Param_Txn_Insert_InternalTransfer
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then
                    Cr_Led_id = "00080" 'Cash A/c.
                Else
                    Cr_Led_id = "00079" 'Bank A/c.
                    Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
                End If
                Tr_AB_ID_1 = Me.GLookUp_ToCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_FrCen_List.Tag
            Else
                Cr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then
                    Dr_Led_id = "00080" 'Cash A/c.
                Else
                    Dr_Led_id = "00079" 'Bank A/c.
                    Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag
                End If
                Tr_AB_ID_1 = Me.GLookUp_FrCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_ToCen_List.Tag
            End If

            '+------------------TDS Deduction Reference-------------------+
            If GLookUp_ItemList.Tag = "d611ce4a-756a-42d0-b7ee-51dd7add7263" And Base._open_Year_ID > "1415" Then
                Dim xfrom_TDs_ded As New Frm_I_Transfer_Tds_Sent
                xfrom_TDs_ded.MainBase = Base : xfrom_TDs_ded.xMID = Me.xMID.Text : xfrom_TDs_ded.lblMentionedInvoucher.EditValue = Me.Txt_Amount.EditValue
                xfrom_TDs_ded.ShowDialog()
                If xfrom_TDs_ded.DialogResult = Windows.Forms.DialogResult.Retry Then
                    xfrom_TDs_ded.Dispose() : Exit Sub
                End If

                Dim inTDSParam(xfrom_TDs_ded.TDS_Deduction_List.Count) As Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer
                Dim ctr As Integer = 0
                For Each cParam As Frm_I_Transfer_Tds_Sent.Out_TDS In xfrom_TDs_ded.TDS_Deduction_List
                    Dim inTDSDeduct As New Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer()
                    inTDSDeduct.RecID = Guid.NewGuid.ToString
                    inTDSDeduct.RefTxnID = cParam.RefMID
                    inTDSDeduct.TDS_Sent = cParam.TDS_Ded
                    inTDSDeduct.TxnMID = Me.xMID.Text
                    inTDSParam(ctr) = inTDSDeduct
                    ctr += 1
                Next
                InNewParam.param_Insert_TDSDed = inTDSParam : xfrom_TDs_ded.Dispose()
            End If
            '+-----------------END------------------+

            'Master Entry
            Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherInternalTransfer()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
            'InMInfo.TDate = Me.Txt_V_Date.Text
            InMInfo.PartyID = Tr_AB_ID_1
            InMInfo.SubTotal = Val(Me.Txt_Amount.Text)
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then InMInfo.Cash = Val(Me.Txt_Amount.Text) Else InMInfo.Cash = 0
            If Me.Cmd_Mode.Text.ToUpper <> "CASH" Then
                If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False Then
                    InMInfo.Bank = 0
                ElseIf Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                    InMInfo.Bank = Val(Me.Txt_Amount.Text)
                Else
                    InMInfo.Bank = Val(Me.Txt_Amount.Text)
                End If
            Else
                InMInfo.Bank = 0
            End If
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            'If Not Base._Internal_Tf_Voucher_DBOps.InsertMaster(InMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMInfo

            'ENTRY PART #1
            Me.xID_1.Text = System.Guid.NewGuid().ToString()

            Dim xCROSS_REF_ID As String = ""
            If USE_CROSS_REF Then
                xCROSS_REF_ID = " '" & CROSS_REF_ID & "' "
            Else
                xCROSS_REF_ID = " NULL "
            End If

            If Base.AllowMultiuser() Then
                If (iTrans_Type.ToString()) = "CREDIT" Then
                    Dim IntTf As DataTable = Base._Voucher_DBOps.GetAdjustments(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Internal_Transfer, CROSS_REF_ID, False, Base._open_Year_ID)
                    If IntTf.Rows.Count > 0 Then 'A/A
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Internal Transfer"), "Referred Record Already Matched !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
            End If

            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
            InParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
            'InParam.TDate = Me.Txt_V_Date.Text
            InParam.ItemID = Me.GLookUp_ItemList.Tag
            InParam.Type = iTrans_Type
            InParam.Cr_Led_ID = Cr_Led_id
            InParam.Dr_Led_ID = Dr_Led_id
            InParam.SUB_Cr_Led_ID = Sub_Cr_Led_ID
            InParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID
            InParam.Amount = Val(Me.Txt_Amount.Text)
            If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                InParam.Mode = "DD"
            Else
                InParam.Mode = Me.Cmd_Mode.Text
            End If
            ''testing for Cheque issue in debit entries
            Dim RefBank As String = Me.GLookUp_TrfBankList.Tag
            ' If iTrans_Type.ToUpper = "DEBIT" Then RefBank = GLookUp_BankList.Tag   'commented for testing
            InParam.Ref_BANK_ID = RefBank
            Dim RefBranch As String = Me.Txt_Trf_Branch.Text
            ' If iTrans_Type.ToUpper = "DEBIT" Then RefBranch = BE_Bank_Branch.Text  'commented for testing
            InParam.Ref_Branch = RefBranch
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.Ref_CDate = Txt_Ref_CDate.Text
            'InParam.Ref_Date = Me.Txt_Ref_Date.Text
            'InParam.Ref_CDate = Me.Txt_Ref_CDate.Text
            InParam.Ref_Others = Me.Txt_DD_Fr_Chq_No.Text
            InParam.MTBankID = Me.GLookUp_TrfBankList.Tag
            Dim MTAccNo As String = Me.Txt_Trf_ANo.Text
            'If iTrans_Type.ToUpper = "DEBIT" Then MTAccNo = BE_Bank_Acc_No.Text
            InParam.MTAccNo = MTAccNo
            InParam.AB_ID_1 = Tr_AB_ID_1
            InParam.AB_ID_2 = Tr_AB_ID_2
            InParam.Narration = Me.Txt_Narration.Text
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.MasterTxnID = Me.xMID.Text
            InParam.Sr_No = 1
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID_1.Text
            InParam.Cross_Ref_ID = xCROSS_REF_ID

            'If Not Base._Internal_Tf_Voucher_DBOps.Insert(InParam) Then
            '    Base._Internal_Tf_Voucher_DBOps.DeleteByMasterID(Me.xID_1.Text)
            '    Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_1.Text)
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertEP1 = InParam

            'Purpose
            Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer()
            InPurpose.TxnID = Me.xID_1.Text
            InPurpose.PurposeID = Me.GLookUp_PurList.Tag
            InPurpose.Amount = Val(Me.Txt_Amount.Text)
            InPurpose.Status_Action = Status_Action
            InPurpose.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._Internal_Tf_Voucher_DBOps.InsertPurpose(InPurpose) Then
            '    Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_1.Text)
            '    Base._Internal_Tf_Voucher_DBOps.DeleteByMasterID(Me.xID_1.Text)
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertPurposeEp1 = InPurpose

            'ENTRY PART #2
            If Val(Me.Txt_Bank_Chg.Text) > 0 Then
                Me.xID_2.Text = System.Guid.NewGuid().ToString()
                Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer()
                InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
                InParams.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                'InParams.TDate = Me.Txt_V_Date.Text
                InParams.ItemID = BC_Item_ID
                InParams.Type = "DEBIT"
                InParams.Cr_Led_ID = BC_Cr_Led_id
                InParams.Dr_Led_ID = BC_Dr_Led_id
                InParams.SUB_Cr_Led_ID = BC_Sub_Cr_Led_ID
                InParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID
                InParams.Amount = Val(Me.Txt_Bank_Chg.Text)
                If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                    InParams.Mode = "DD"
                Else
                    InParams.Mode = Me.Cmd_Mode.Text
                End If
                InParams.Ref_BANK_ID = ""
                InParams.Ref_Branch = ""
                InParams.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParams.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_Date = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParams.Ref_CDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParams.Ref_CDate = Txt_Ref_CDate.Text
                'InParams.Ref_Date = Me.Txt_Ref_Date.Text
                'InParams.Ref_CDate = Me.Txt_Ref_CDate.Text
                InParams.Ref_Others = Me.Txt_DD_Fr_Chq_No.Text
                InParams.MTBankID = ""
                InParams.MTAccNo = ""
                InParams.AB_ID_1 = ""
                InParams.AB_ID_2 = ""
                InParams.Narration = "Auto Generated entry from Internal Transfer."
                InParams.Remarks = ""
                InParams.Reference = ""
                InParams.MasterTxnID = Me.xMID.Text
                InParams.Sr_No = 2
                InParams.Status_Action = Status_Action
                InParams.RecID = Me.xID_2.Text
                InParams.Cross_Ref_ID = xCROSS_REF_ID

                'If Not Base._Internal_Tf_Voucher_DBOps.Insert(InParams) Then

                '    Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_2.Text)
                '    Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_2.Text)
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertEP2 = InParams

                'Purpose
                Dim InPurp As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer()
                InPurp.TxnID = Me.xID_2.Text
                InPurp.PurposeID = Me.GLookUp_PurList.Tag
                InPurp.Amount = Val(Me.Txt_Bank_Chg.Text)
                InPurp.Status_Action = Status_Action
                InPurp.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Internal_Tf_Voucher_DBOps.InsertPurpose(InPurp) Then
                '    Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_2.Text)
                '    Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_2.Text)
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.parma_InsertPurposeEP2 = InPurp
            End If

            If USE_CROSS_REF Then
                'If Not Base._Internal_Tf_Voucher_DBOps.Update_CrossReference(Me.xID_1.Text, CROSS_M_ID) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                Dim UpCrossRef As Common_Lib.RealTimeService.Param_Voucher_InternalTransfer_Update_CrossReference = New Common_Lib.RealTimeService.Param_Voucher_InternalTransfer_Update_CrossReference()
                UpCrossRef.Cross_Ref_ID = Me.xID_1.Text
                UpCrossRef.RecID = CROSS_M_ID
                InNewParam.param_UpdateCrossRef = UpCrossRef
            End If

            If Val(Txt_Slip_No.Text) > 0 Then
                Dim inSlip As New Common_Lib.RealTimeService.Parameter_InsertSlip_VoucherInternalTransfer
                inSlip.RecID = Guid.NewGuid.ToString
                inSlip.SlipNo = Val(Txt_Slip_No.Text)
                inSlip.TxnID = Me.xMID.Text
                inSlip.Dep_BA_ID = Sub_Dr_Led_ID
                InNewParam.param_InsertSlip = inSlip
            End If

            If Not Base._Internal_Tf_Voucher_DBOps.Insert_InternalTransfer_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()


        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_InternalTransfer = New Common_Lib.RealTimeService.Param_Txn_Update_InternalTransfer
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
            Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then
                    Cr_Led_id = "00080" 'Cash A/c.
                Else
                    Cr_Led_id = "00079" 'Bank A/c.
                    Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
                End If
                Tr_AB_ID_1 = Me.GLookUp_ToCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_FrCen_List.Tag
            Else
                Cr_Led_id = iLed_ID
                If Cmd_Mode.Text.ToUpper = "CASH" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then
                    Dr_Led_id = "00080" 'Cash A/c.
                Else
                    Dr_Led_id = "00079" 'Bank A/c.
                    Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag
                End If
                Tr_AB_ID_1 = Me.GLookUp_FrCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_ToCen_List.Tag
            End If

            '+------------------TDS Deduction Reference-------------------+
            If GLookUp_ItemList.Tag = "d611ce4a-756a-42d0-b7ee-51dd7add7263" And Base._open_Year_ID > "1415" Then
                Dim xfrom_TDs_ded As New Frm_I_Transfer_Tds_Sent
                xfrom_TDs_ded.MainBase = Base : xfrom_TDs_ded.xMID = Me.xMID.Text : xfrom_TDs_ded.lblMentionedInvoucher.EditValue = Me.Txt_Amount.EditValue
                xfrom_TDs_ded.ShowDialog()
                If xfrom_TDs_ded.DialogResult = Windows.Forms.DialogResult.Retry Then
                    xfrom_TDs_ded.Dispose() : Exit Sub
                End If

                EditParam.ID_DeleteTDSDeduction = Me.xMID.Text
                Dim inTDSParam(xfrom_TDs_ded.TDS_Deduction_List.Count) As Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer
                Dim ctr As Integer = 0
                For Each cParam As Frm_I_Transfer_Tds_Sent.Out_TDS In xfrom_TDs_ded.TDS_Deduction_List
                    Dim inTDSDeduct As New Common_Lib.RealTimeService.Parameter_InsertTDSDeduction_VoucherInternalTransfer()
                    inTDSDeduct.RecID = Guid.NewGuid.ToString
                    inTDSDeduct.RefTxnID = cParam.RefMID
                    inTDSDeduct.TDS_Sent = cParam.TDS_Ded
                    inTDSDeduct.TxnMID = Me.xMID.Text
                    inTDSParam(ctr) = inTDSDeduct
                    ctr += 1
                Next
                EditParam.param_Insert_TDSDed = inTDSParam : xfrom_TDs_ded.Dispose()
            End If
            '+-----------------END------------------+

            'Master Entry
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherInternalTransfer()
            UpMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            'InMInfo.TDate = Me.Txt_V_Date.Text
            UpMInfo.PartyID = Tr_AB_ID_1
            UpMInfo.SubTotal = Val(Me.Txt_Amount.Text)
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Or (Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False) Then UpMInfo.Cash = Val(Me.Txt_Amount.Text) Else UpMInfo.Cash = 0
            If Me.Cmd_Mode.Text.ToUpper <> "CASH" Then
                If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = False Then
                    UpMInfo.Bank = 0
                ElseIf Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                    UpMInfo.Bank = Val(Me.Txt_Amount.Text)
                Else
                    UpMInfo.Bank = Val(Me.Txt_Amount.Text)
                End If
            Else
                UpMInfo.Bank = 0
            End If

            'UpMInfo.Status_Action = Status_Action
            UpMInfo.RecID = Me.xMID.Text

            'If Not Base._Internal_Tf_Voucher_DBOps.UpdateMaster(UpMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMInfo

            'ENTRY PART #1
            Dim xCROSS_REF_ID As String = ""
            If USE_CROSS_REF Then
                xCROSS_REF_ID = " '" & CROSS_REF_ID & "' "
            Else
                xCROSS_REF_ID = " NULL "
            End If
            Dim UpParam As Common_Lib.RealTimeService.Parameter_Update_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_Update_VoucherInternalTransfer()
            UpParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.TDate = Txt_V_Date.Text
            'UpParam.TDate = Me.Txt_V_Date.Text
            UpParam.ItemID = Me.GLookUp_ItemList.Tag
            UpParam.Type = iTrans_Type
            UpParam.Cr_Led_ID = Cr_Led_id
            UpParam.Dr_Led_ID = Dr_Led_id
            UpParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            UpParam.SUB_Dr_Led_ID = Sub_Dr_Led_ID
            If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                UpParam.Mode = "DD"
            Else
                UpParam.Mode = Me.Cmd_Mode.Text
            End If
            Dim RefBank As String = Me.GLookUp_TrfBankList.Tag
            'If iTrans_Type.ToUpper = "DEBIT" Then RefBank = GLookUp_BankList.Tag
            UpParam.RefBankID = RefBank
            Dim RefBranch As String = Me.Txt_Trf_Branch.Text
            'If iTrans_Type.ToUpper = "DEBIT" Then RefBranch = BE_Bank_Branch.Text
            UpParam.RefBranch = RefBranch
            UpParam.Ref_No = Me.Txt_Ref_No.Text
            UpParam.Ref_Others = Me.Txt_DD_Fr_Chq_No.Text
            If IsDate(Txt_Ref_Date.Text) Then UpParam.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_Date = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then UpParam.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else UpParam.Ref_ChequeDate = Txt_Ref_CDate.Text
            'UpParam.Ref_Date = Me.Txt_Ref_Date.Text
            'UpParam.Ref_ChequeDate = Me.Txt_Ref_CDate.Text
            UpParam.Amount = Val(Me.Txt_Amount.Text)
            UpParam.AB_ID_1 = Tr_AB_ID_1
            UpParam.AB_ID_2 = Tr_AB_ID_2
            UpParam.MT_Bank_ID = Me.GLookUp_TrfBankList.Tag
            Dim RefAccNo As String = Me.Txt_Trf_ANo.Text
            'If iTrans_Type.ToUpper = "DEBIT" Then RefAccNo = BE_Bank_Acc_No.Text
            UpParam.MT_AccNo = RefAccNo
            UpParam.Narration = Me.Txt_Narration.Text
            UpParam.Remarks = Me.Txt_Remarks.Text
            UpParam.Reference = Me.Txt_Reference.Text
            'UpParam.Status_Action = Status_Action
            UpParam.RecID = Me.xID_1.Text

            'If Not Base._Internal_Tf_Voucher_DBOps.Update(UpParam) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateEP1 = UpParam


            'Purpose
            Dim UpPurpose As Common_Lib.RealTimeService.Parameter_UpdatePurpose_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_UpdatePurpose_VoucherInternalTransfer()
            UpPurpose.PurposeID = Me.GLookUp_PurList.Tag
            UpPurpose.Amount = Val(Me.Txt_Bank_Chg.Text)
            'UpPurpose.Status_Action = Status_Action
            UpPurpose.RecID = Me.xID_1.Text

            'If Not Base._Internal_Tf_Voucher_DBOps.UpdatePurpose(UpPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdatePurposeEP1 = UpPurpose

            'ENTRY PART #2
            If Val(Me.Txt_Bank_Chg.Text) > 0 Then
                If Me.xID_2.Text.Trim.Length > 0 Then
                    Dim UpParams As Common_Lib.RealTimeService.Parameter_Update_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_Update_VoucherInternalTransfer()
                    UpParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then UpParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParams.TDate = Txt_V_Date.Text
                    'UpParams.TDate = Me.Txt_V_Date.Text
                    UpParams.ItemID = BC_Item_ID
                    UpParams.Type = "DEBIT"
                    UpParams.Cr_Led_ID = BC_Cr_Led_id
                    UpParams.Dr_Led_ID = BC_Dr_Led_id
                    UpParams.Sub_Cr_Led_ID = BC_Sub_Cr_Led_ID
                    UpParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID
                    If Me.Cmd_Mode.Text.ToUpper = "CASH TO DD" And USE_CROSS_REF = True Then
                        UpParams.Mode = "DD"
                    Else
                        UpParams.Mode = Me.Cmd_Mode.Text
                    End If
                    UpParams.RefBankID = ""
                    UpParams.RefBranch = ""
                    UpParams.Ref_No = Me.Txt_Ref_No.Text
                    UpParams.Ref_Others = Me.Txt_DD_Fr_Chq_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then UpParams.Ref_Date = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpParams.Ref_Date = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then UpParams.Ref_ChequeDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else UpParams.Ref_ChequeDate = Txt_Ref_CDate.Text
                    'UpParams.Ref_Date = Me.Txt_Ref_Date.Text
                    'UpParams.Ref_ChequeDate = Me.Txt_Ref_CDate.Text
                    UpParams.Amount = Val(Me.Txt_Bank_Chg.Text)
                    UpParams.AB_ID_1 = ""
                    UpParams.AB_ID_2 = ""
                    UpParams.MT_Bank_ID = ""
                    UpParams.MT_AccNo = ""
                    UpParams.Narration = "Auto Generated entry from Internal Transfer."
                    UpParams.Remarks = ""
                    UpParams.Reference = ""
                    'UpParams.Status_Action = Status_Action
                    UpParams.RecID = Me.xID_2.Text

                    'If Not Base._Internal_Tf_Voucher_DBOps.Update(UpParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_UpdateEP2 = UpParams

                    'Purpose
                    Dim UpPurp As Common_Lib.RealTimeService.Parameter_UpdatePurpose_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_UpdatePurpose_VoucherInternalTransfer()
                    UpPurp.PurposeID = Me.GLookUp_PurList.Tag
                    UpPurp.Amount = Val(Me.Txt_Amount.Text)
                    'UpPurp.Status_Action = Status_Action
                    UpPurp.RecID = Me.xID_2.Text

                    'If Not Base._Internal_Tf_Voucher_DBOps.UpdatePurpose(UpPurp) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_UpdatePurposeEP2 = UpPurp
                Else
                    Me.xID_2.Text = System.Guid.NewGuid().ToString()
                    Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherInternalTransfer()
                    InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Internal_Transfer
                    InParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                    'InParams.TDate = Me.Txt_V_Date.Text
                    InParams.ItemID = BC_Item_ID
                    InParams.Type = "DEBIT"
                    InParams.Cr_Led_ID = BC_Cr_Led_id
                    InParams.Dr_Led_ID = BC_Dr_Led_id
                    InParams.SUB_Cr_Led_ID = BC_Sub_Cr_Led_ID
                    InParams.SUB_Dr_Led_ID = BC_Sub_Dr_Led_ID
                    InParams.Amount = Val(Me.Txt_Bank_Chg.Text)
                    InParams.Mode = "OTHERS"
                    InParams.Ref_BANK_ID = ""
                    InParams.Ref_Branch = ""
                    InParams.Ref_No = ""
                    InParams.Ref_Date = ""
                    InParams.Ref_CDate = ""
                    InParams.Ref_Others = ""
                    InParams.MTBankID = ""
                    InParams.MTAccNo = ""
                    InParams.AB_ID_1 = ""
                    InParams.AB_ID_2 = ""
                    InParams.Narration = "Auto Generated entry from Internal Transfer."
                    InParams.Remarks = ""
                    InParams.Reference = ""
                    InParams.MasterTxnID = Me.xMID.Text
                    InParams.Sr_No = 2
                    InParams.Status_Action = Status_Action
                    InParams.RecID = Me.xID_2.Text
                    InParams.Cross_Ref_ID = xCROSS_REF_ID

                    'If Not Base._Internal_Tf_Voucher_DBOps.Insert(InParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InsertEP2 = InParams

                    'Purpose
                    Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherInternalTransfer()
                    InPurpose.TxnID = Me.xID_2.Text
                    InPurpose.PurposeID = Me.GLookUp_PurList.Tag
                    InPurpose.Amount = Val(Me.Txt_Bank_Chg.Text)
                    InPurpose.Status_Action = Status_Action
                    InPurpose.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Internal_Tf_Voucher_DBOps.InsertPurpose(InPurpose) Then
                    '    Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_2.Text)
                    '    Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_2.Text)
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    EditParam.param_InsertPurposeEP2 = InPurpose
                End If
            Else
                If Me.xID_2.Text.Trim.Length > 0 Then
                    '    If Not Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_2.Text) Then
                    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    EditParam.ID2_DeletePurpose = Me.xID_2.Text
                    '    If Not Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_2.Text) Then
                    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    EditParam.ID2_Delete = Me.xID_2.Text
                End If
            End If

            EditParam.ID_DeleteSlip = Me.xMID.Text

            If Val(Txt_Slip_No.Text) > 0 Then
                Dim inSlip As New Common_Lib.RealTimeService.Parameter_InsertSlip_VoucherInternalTransfer
                inSlip.RecID = Guid.NewGuid.ToString
                inSlip.SlipNo = Val(Txt_Slip_No.Text)
                inSlip.TxnID = Me.xMID.Text
                inSlip.Dep_BA_ID = Sub_Dr_Led_ID
                EditParam.param_InsertSlip = inSlip
            End If

            If Not Base._Internal_Tf_Voucher_DBOps.Update_InternalTransfer_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()

        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_InternalTransfer = New Common_Lib.RealTimeService.Param_Txn_Delete_InternalTransfer
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then

                'If Not Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_1.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.ID1_DeletePurpose = Me.xID_1.Text
                'If Not Base._Internal_Tf_Voucher_DBOps.DeletePurpose(Me.xID_2.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.ID2_DeletePurpose = Me.xID_2.Text
                'If Not Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_1.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.ID1_Delete = Me.xID_1.Text
                'If Not Base._Internal_Tf_Voucher_DBOps.Delete(Me.xID_2.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.ID2_Delete = Me.xID_2.Text
                'If Not Base._Internal_Tf_Voucher_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text

                DelParam.ID_DeleteSlip = Me.xMID.Text

                DelParam.ID_DeleteTDSDeduction = Me.xMID.Text

                If Not Base._Internal_Tf_Voucher_DBOps.Delete_InternalTransfer_Txn(DelParam) Then
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
    Private Sub BUT_CONVERT_Click(sender As Object, e As EventArgs) Handles BUT_CONVERT.Click
        'Deletion related checks here 
        Dim zfrm As New Frm_Voucher_Win_Donation_R : zfrm.MainBase = Base
        zfrm.Tag = Common_Lib.Common.Navigation_Mode._New : zfrm.Txt_V_Date.DateTime = Me.Txt_V_Date.DateTime : zfrm.Cmd_Mode.Text = IIf(zfrm.Cmd_Mode.Properties.Items.Contains(Cmd_Mode.Text), Cmd_Mode.Text, "CHEQUE") : zfrm.SelectedBankID = GLookUp_BankList.Tag
        zfrm.Txt_Amount.Text = Txt_Amount.Text : zfrm.SelectedRefBankID = GLookUp_TrfBankList.Tag : zfrm.Txt_Ref_Branch.Text = Txt_Trf_Branch.Text
        If Txt_Ref_Date.DateTime <> DateTime.MinValue Then zfrm.Txt_Ref_Date.DateTime = Txt_Ref_Date.DateTime
        If Txt_Ref_CDate.DateTime <> DateTime.MinValue Then zfrm.Txt_Ref_CDate.DateTime = Txt_Ref_CDate.DateTime
        zfrm.Txt_Ref_No.Text = Txt_Ref_No.Text
        zfrm.Top = Me.Top + 200 : zfrm.Left = Me.Left + 200
        zfrm.ShowDialog(Me)
        If zfrm.DialogResult = Windows.Forms.DialogResult.OK Then
            Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_InternalTransfer = New Common_Lib.RealTimeService.Param_Txn_Delete_InternalTransfer
            DelParam.ID1_DeletePurpose = Me.xID_1.Text
            DelParam.ID2_DeletePurpose = Me.xID_2.Text
            DelParam.ID1_Delete = Me.xID_1.Text
            DelParam.ID2_Delete = Me.xID_2.Text
            DelParam.MID_DeleteMaster = Me.xMID.Text
            DelParam.ID_DeleteSlip = Me.xMID.Text
            If Not Base._Internal_Tf_Voucher_DBOps.Delete_InternalTransfer_Txn(DelParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
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
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub Txt_Total_Calc(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.EditValueChanged, Txt_Bank_Chg.EditValueChanged
        Me.Txt_Total.Text = Format(Val(Me.Txt_Amount.Text) + Val(Me.Txt_Bank_Chg.Text), "#0.00")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Narration.GotFocus, Txt_Amount.GotFocus, Txt_Amount.Click, Txt_Ref_No.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Bank_Chg.GotFocus, Txt_Bank_Chg.Click, Txt_Trf_ANo.GotFocus, Txt_DD_Fr_Chq_No.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Or txt.Name = Txt_Bank_Chg.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Narration.KeyPress, Txt_Amount.KeyPress, Txt_Ref_No.KeyPress, Txt_Reference.KeyPress, Txt_Remarks.KeyPress, Txt_Trf_ANo.KeyPress, Txt_Trf_Branch.KeyPress, Txt_DD_Fr_Chq_No.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Narration.KeyDown, Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Trf_ANo.KeyDown, Txt_Bank_Chg.KeyDown, Txt_Ref_Date.KeyDown, Txt_Ref_CDate.KeyDown, Txt_DD_Fr_Chq_No.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Narration.Validated, Txt_Ref_No.Validated, Txt_Reference.Validated, Txt_Remarks.Validated, Txt_Trf_ANo.Validated, Txt_Trf_Branch.Validated, Txt_DD_Fr_Chq_No.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub Cmd_Mode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmd_Mode.SelectedIndexChanged
        Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None : Me.Txt_Ref_No.Properties.Mask.EditMask = ""
        Me.Txt_Ref_No.SuperTip = Nothing
        lbl_ref_for_dd.Visible = False : Me.Txt_DD_Fr_Chq_No.Visible = False
        Me.Txt_Ref_CDate.Properties.ReadOnly = False : Me.Txt_Ref_CDate.Enabled = True
        If Cmd_Mode.Text.ToUpper = "CASH" Then
            Layout_Ref_Detail.Control.Enabled = False : Layout_Transfer.Control.Enabled = False
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_CDate.Text = "Clearing Date:"
            Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            lbl_Bank_Name.ForeColor = Color.Gray : lbl_Branch_Name.ForeColor = Color.Gray : lbl_Acc_No.ForeColor = Color.Gray
            lbl_Ref_Title.ForeColor = Color.Gray : lbl_Ref_No.ForeColor = Color.Gray : lbl_Ref_Date.ForeColor = Color.Gray : lbl_Ref_CDate.ForeColor = Color.Gray
            Txt_DD_Fr_Chq_No.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""

            lbl_Trf_Bank.ForeColor = Color.Gray : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
            lbl_Trf_ANo.ForeColor = Color.Gray : Txt_Trf_ANo.Text = ""
            lbl_Trf_Branch.ForeColor = Color.Gray : Txt_Trf_Branch.Text = ""

            lbl_Bank_Charges.ForeColor = Color.Gray : Txt_Bank_Chg.Enabled = False : Txt_Bank_Chg.Text = ""
            Me.Txt_Slip_No.Enabled = False : Me.Txt_Slip_Count.Enabled = False
        Else
            Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = "" 'added to check issues 
            Layout_Ref_Detail.Control.Enabled = True : Layout_Transfer.Control.Enabled = True

            If Cmd_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Cheque Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:" : lbl_Ref_CDate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]" : Me.Txt_Ref_No.SuperTip = SuperToolTip5
            If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_CDate.Text = "Clearing Date:" : Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]?[0-9]?[0-9]" : Me.Txt_Ref_No.SuperTip = SuperToolTip5 Else Txt_DD_Fr_Chq_No.Text = ""

            If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
                lbl_Ref_Title.Text = StrConv(Cmd_Mode.Text, vbProperCase) & " Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Ref. No..." : lbl_Ref_Date.Text = "Date:" : lbl_Ref_CDate.Text = "Clearing Date:"
            End If

            If iTrans_Type.ToUpper = "CREDIT" Then
                If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Trf_ANo.ForeColor = Color.Black
            End If
            If iTrans_Type.ToUpper = "DEBIT" Then
                If Cmd_Mode.Text.ToUpper = "DD" Then
                    Me.lbl_ref_for_dd.Visible = True : Me.Txt_DD_Fr_Chq_No.Visible = True
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_ANo.Enabled = True : Txt_Trf_Branch.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Black : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                ElseIf (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "DD") Then
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_ANo.Enabled = True : Txt_Trf_Branch.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Red : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                ElseIf Cmd_Mode.Text.ToUpper = "CASH TO DD" Then
                    Layout_Ref_Detail.Control.Enabled = False
                    Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
                    Layout_Transfer.Control.Enabled = True
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_ANo.Enabled = False : Txt_Trf_Branch.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Black : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                ElseIf Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
                    Layout_Ref_Detail.Control.Enabled = False : lbl_Bank_Name.ForeColor = Color.Gray : lbl_Branch_Name.ForeColor = Color.Gray : lbl_Acc_No.ForeColor = Color.Gray
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_ANo.Enabled = True : Txt_Trf_Branch.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Red : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                Else
                    GLookUp_TrfBankList.Enabled = False : Txt_Trf_ANo.Enabled = False : Txt_Trf_Branch.Enabled = False
                    lbl_Trf_Bank.ForeColor = Color.Gray : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Gray : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Gray : Txt_Trf_Branch.Text = ""
                End If
            Else
                If Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
                    Layout_Ref_Detail.Control.Enabled = True : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Red : lbl_Acc_No.ForeColor = Color.Red
                    GLookUp_TrfBankList.Enabled = False : Txt_Trf_Branch.Enabled = False : Txt_Trf_ANo.Enabled = False
                    lbl_Trf_Bank.ForeColor = Color.Gray : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Gray : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Gray : Txt_Trf_Branch.Text = ""
                ElseIf Cmd_Mode.Text.ToUpper = "CHEQUE" Then
                    Layout_Ref_Detail.Control.Enabled = True : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Red : lbl_Acc_No.ForeColor = Color.Red
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_Branch.Enabled = True : Txt_Trf_ANo.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Black : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Black : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Black : Txt_Trf_Branch.Text = ""
                ElseIf Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "NEFT" Then
                    lbl_Trf_Bank.ForeColor = Color.Black : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Black : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Black : Txt_Trf_Branch.Text = ""
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_Branch.Enabled = True : Txt_Trf_ANo.Enabled = True
                Else
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Red : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_Branch.Enabled = True : Txt_Trf_ANo.Enabled = True
                End If
            End If
            If iTrans_Type.ToUpper = "CREDIT" Then
                If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Trf_ANo.ForeColor = Color.Black
            End If
            If Cmd_Mode.Text.ToUpper = "CASH TO DD" Or (Cmd_Mode.Text.ToUpper = "CASH TO BANK" And iTrans_Type.ToUpper = "DEBIT") Then lbl_Bank_Name.ForeColor = Color.Black Else lbl_Bank_Name.ForeColor = Color.Red
            lbl_Branch_Name.ForeColor = Color.Black : lbl_Acc_No.ForeColor = Color.Black
            lbl_Ref_Title.ForeColor = Color.Black : lbl_Ref_No.ForeColor = Color.Red : lbl_Ref_Date.ForeColor = Color.Red : lbl_Ref_CDate.ForeColor = Color.Black
            Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""

            lbl_Bank_Charges.ForeColor = Color.Black : Txt_Bank_Chg.Enabled = True : Txt_Bank_Chg.Text = ""
            Me.Txt_Slip_No.Enabled = True : Me.Txt_Slip_Count.Enabled = True
        End If
        Txt_Slip_No.Text = "" : Txt_Slip_Count.Text = ""
        If Cmd_Mode.Text.ToUpper = "CHEQUE" Or Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or (Cmd_Mode.Text.ToUpper = "CASH TO BANK" And iTrans_Type.ToUpper = "DEBIT") Then
            Me.TRF_BA_ACCOUNT_NO.Visible = True
            Me.TRF_BB_BRANCH_NAME.Visible = True
            Me.TRF_IFSC_CODE.Visible = True
            Me.TRF_STATUS.Visible = True
            Me.Txt_Trf_ANo.Properties.ReadOnly = True
            Me.Txt_Trf_Branch.Properties.ReadOnly = True
            LookUp_GetTrfBankList()
        ElseIf Cmd_Mode.Text.ToUpper = "CASH TO DD" Then
            Me.Txt_Ref_No.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx : Me.Txt_Ref_No.Properties.Mask.EditMask = "[0-9][0-9][0-9][0-9][0-9][0-9]" : Me.Txt_Ref_No.SuperTip = SuperToolTip5
            Me.Txt_Trf_ANo.Properties.ReadOnly = True
            Me.Txt_Ref_CDate.Enabled = False
            lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:" : lbl_Ref_CDate.Text = "Clearing Date:"
        Else
            If Cmd_Mode.Text.ToUpper = "DD" Then LookUp_GetTrfBankList()
            Me.TRF_BA_ACCOUNT_NO.Visible = False
            Me.TRF_BB_BRANCH_NAME.Visible = False
            Me.TRF_IFSC_CODE.Visible = False
            Me.TRF_STATUS.Visible = False
            Me.Txt_Trf_ANo.Properties.ReadOnly = False
            Me.Txt_Trf_Branch.Properties.ReadOnly = False
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
    Private Sub Txt_Slip_No_EditValueChanged(sender As Object, e As EventArgs) Handles Txt_Slip_No.EditValueChanged
        If Val(Me.Txt_Slip_No.Text) > 0 Then
            Me.Txt_Slip_Count.Text = Base._DepositSlipsDBOps.GetSlipTxnCount(Val(Me.Txt_Slip_No.Text), Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Donation, Me.xMID.Text)
        End If
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Internal Transfer Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Internal Transfer"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_FrCen_List.Tag = "" ' LookUp_Get_Fr_Centre()
        GLookUp_ToCen_List.Tag = "" ' LookUp_Get_To_Centre()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        GLookUp_TrfBankList.Tag = "" : LookUp_GetTrfBankList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()

        '--- H.Q. CENTRE IDs-----------------------
        Dim HQ_DT As DataTable = Base._Internal_Tf_Voucher_DBOps.GetHQCenters()
        For Each xRow As DataRow In HQ_DT.Rows : HQ_IDs += "'" & xRow("HQ_CEN_ID").ToString & "'," : Next
        If HQ_IDs.Trim.Length > 0 Then HQ_IDs = IIf(HQ_IDs.Trim.EndsWith(","), Mid(HQ_IDs.Trim.ToString, 1, HQ_IDs.Trim.Length - 1), HQ_IDs.Trim.ToString)
        If HQ_IDs.Trim.Length = 0 Then HQ_IDs = "''"
        '-------------------------------------------

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.Text = "New ~ " & Me.TitleX.Text
            Me.Txt_V_NO.Text = ""
            If Base._IsVolumeCenter Then ' Load purpose by default for Volume Centers 
                Me.GLookUp_PurList.Properties.ReadOnly = True
                Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", "8f6b3279-166a-4cd9-8497-ca9fc6283b25"))
                Me.GLookUp_PurList.EditValue = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                Me.GLookUp_PurList.Properties.Tag = "SHOW"
            End If
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "Edit ~ " & Me.TitleX.Text : Me.GLookUp_ItemList.Properties.ReadOnly = True : Me.GLookUp_ItemList.Enabled = False
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

        'Check Pending Internal Transfer Entries
        If Val(Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If _Accepted_From_Register Then
                AcceptFromRegister()
            Else
                If Selected_Item_ID.Length = 0 Then Pending_List()
            End If
        Else
            USE_CROSS_REF = False
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection And USE_CROSS_REF = False Then 'By Item-wise Selection
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID)
            Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            Me.GLookUp_ItemList.Properties.ReadOnly = False
            Me.Txt_V_Date.Focus()
        End If

        Load_from_Last_voucher()

        xPleaseWait.Hide()
    End Sub
    Private Sub AcceptFromRegister()
        USE_CROSS_REF = True : Me.Text = "Match ~ " & Me.TitleX.Text
        'CROSS_REF_ID = xfrm._ID
        'CROSS_M_ID = xfrm._M_ID
        'FR_REC_EDIT_ON = xfrm._EDIT_DATE
        Dim xItem_ID As String = _a_Item_ID
        If xItem_ID.ToString.Length > 0 Then
            Me.GLookUp_ItemList.Properties.ReadOnly = True
            Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
            Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", xItem_ID))
            Me.GLookUp_ItemList.EditValue = xItem_ID
            Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
            Me.GLookUp_ItemList.Properties.Tag = "SHOW"

            Me.GLookUp_ItemList.Enabled = False
            Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

            GLookUp_ItemList_Leave(Nothing, Nothing)
        End If

        Dim xDate As DateTime = Nothing : xDate = _Date : Txt_V_Date.DateTime = xDate : Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        Me.Cmd_Mode.Text = _Mode : Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

        Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
        If iTrans_Type.ToString.ToUpper = "DEBIT" Then
            Tr_AB_ID_1 = _CEN_ID
            Tr_AB_ID_2 = Base._open_Cen_Rec_ID
        Else
            Tr_AB_ID_1 = Base._open_Cen_Rec_ID
            Tr_AB_ID_2 = _CEN_ID
        End If

        If Tr_AB_ID_1.Length > 0 Then
            Me.GLookUp_ToCen_List.ShowPopup() : Me.GLookUp_ToCen_List.ClosePopup()
            Me.GLookUp_ToCen_ListView.MoveBy(Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1))
            Me.GLookUp_ToCen_ListView.FocusedRowHandle = Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1)
            Me.GLookUp_ToCen_List.EditValue = Tr_AB_ID_1
            Me.GLookUp_ToCen_List.Tag = Me.GLookUp_ToCen_List.EditValue
            Me.GLookUp_ToCen_List.Properties.Tag = "SHOW"

            Me.GLookUp_ToCen_List.Enabled = False
            Me.GLookUp_ToCen_List.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_To_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        End If

        If Tr_AB_ID_2.Length > 0 Then
            Me.GLookUp_FrCen_List.ShowPopup() : Me.GLookUp_FrCen_List.ClosePopup()
            Me.GLookUp_FrCen_ListView.MoveBy(Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2))
            Me.GLookUp_FrCen_ListView.FocusedRowHandle = Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2)
            Me.GLookUp_FrCen_List.EditValue = Tr_AB_ID_2
            Me.GLookUp_FrCen_List.Tag = Me.GLookUp_FrCen_List.EditValue
            Me.GLookUp_FrCen_List.Properties.Tag = "SHOW"

            Me.GLookUp_FrCen_List.Enabled = False
            Me.GLookUp_FrCen_List.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            Me.BE_Fr_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        End If

        Ref_Bank_ID = "" : Ref_Branch = "" : LookUp_GetTrfBankList()

        If Cmd_Mode.Text.ToUpper <> "CASH" Then
            Me.Txt_V_Date.Enabled = True
            Dim xBANK_ID As String = _BI_ID
            Dim xREF_BANK_ID As String = _REF_BI_ID
            Dim xREF_ACC_NO As String = _BI_ACC_NO
            If xBANK_ID.ToString.Length > 0 Then
                Me.GLookUp_TrfBankList.Properties.ReadOnly = True
                Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()

                If iTrans_Type.ToUpper = "CREDIT" Then
                    If Cmd_Mode.Text.ToUpper = "DD" Then '#5539 fix
                        Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                        Me.lbl_Trf_ANo.Tag = xREF_BANK_ID
                        Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID

                        Me.Txt_Trf_Branch.Text = _REF_BRANCH
                        Me.Txt_Trf_ANo.Text = _REF_BANK_ACC_NO
                    Else
                        Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("BA_REC_ID", xBANK_ID) '#5539 fix
                        xBANK_ID = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BI_ID").ToString()
                        Me.lbl_Trf_ANo.Tag = xBANK_ID
                        Me.GLookUp_TrfBankList.EditValue = xBANK_ID

                        Me.Txt_Trf_Branch.Text = _BI_BRANCH
                        Me.Txt_Trf_ANo.Text = _BI_ACC_NO
                        Ref_Bank_ID = _REF_BI_ID : Ref_Branch = _REF_BRANCH
                    End If
                End If
                If iTrans_Type.ToUpper = "DEBIT" Then
                    If Cmd_Mode.Text.ToUpper = "DD" Or Cmd_Mode.Text.ToUpper = "CHEQUE" Then '#4037 FIX
                        Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                        Me.lbl_Trf_ANo.Tag = xREF_BANK_ID
                        Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID

                        Me.Txt_Trf_Branch.Text = _BI_BRANCH
                        Me.Txt_Trf_ANo.Text = _BI_ACC_NO
                    Else
                        Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("BA_REC_ID", xBANK_ID)
                        xBANK_ID = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BI_ID").ToString()
                        Me.lbl_Trf_ANo.Tag = xBANK_ID
                        Me.GLookUp_TrfBankList.EditValue = xBANK_ID
                    End If
                End If
                Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
                Me.GLookUp_TrfBankList.Properties.ReadOnly = False
                Me.GLookUp_TrfBankList.Enabled = False
                Me.GLookUp_TrfBankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                Me.Txt_Trf_Branch.Enabled = False : Me.Txt_Trf_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                Me.Txt_Trf_ANo.Enabled = False : Me.Txt_Trf_ANo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                If BankCount = 1 Then
                    If (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT") Then 'Or (iTrans_Type.ToUpper = "DEBIT" And Cmd_Mode.Text.ToUpper = "CHEQUE")
                        If xREF_BANK_ID.ToString.Length > 0 Then
                            'Me.GLookUp_BankList.Properties.ReadOnly = True
                            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BANK_ACC_NO", xREF_ACC_NO)
                            Dim xRef_BA_ID As String = ""
                            If Me.GLookUp_BankListView.FocusedRowHandle >= 0 Then
                                xRef_BA_ID = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString()
                                Me.GLookUp_BankList.EditValue = xRef_BA_ID
                                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                                Me.GLookUp_BankList.Properties.Tag = "SHOW"
                                Me.GLookUp_BankList.Properties.ReadOnly = False
                                Me.GLookUp_BankList.Enabled = False
                                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                            End If
                        End If
                    End If
                End If
            End If

            If Cmd_Mode.Text.ToUpper = "CASH TO DD" Then
                If xREF_BANK_ID.ToString.Length > 0 Then
                    Me.GLookUp_TrfBankList.Properties.ReadOnly = True
                    Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()
                    Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                    Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID
                    Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                    Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
                    Me.GLookUp_TrfBankList.Properties.ReadOnly = False
                    Me.GLookUp_TrfBankList.Enabled = False
                    Me.GLookUp_TrfBankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                    Me.Txt_Trf_Branch.Text = _REF_BRANCH : Me.Txt_Trf_Branch.Enabled = False : Me.Txt_Trf_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_ANo.Enabled = False : Me.Txt_Trf_ANo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                End If
            End If

            Me.Txt_Ref_No.Text = _BI_REF_NO : Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

            If iTrans_Type.ToUpper = "DEBIT" And Cmd_Mode.Text.ToUpper = "DD" Then
                Me.Txt_DD_Fr_Chq_No.Text = _REF_OTHERS : Me.Txt_DD_Fr_Chq_No.Enabled = True
            End If

            If Not IsDBNull(_BI_REF_DT) Then
                xDate = _BI_REF_DT : Me.Txt_Ref_Date.DateTime = xDate : Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
            End If
        End If

        Me.Txt_Amount.Text = Format(CDec(_Amount), "#0.00") : Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        Me.Txt_Bank_Chg.Text = "0.00" : Me.Txt_Bank_Chg.Enabled = False : Me.Txt_Bank_Chg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        Me.Txt_Total.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

        Dim xPUR_ID As String = _a_PUR_ID
        If xPUR_ID.ToString.Length > 0 Then
            Me.GLookUp_PurList.Properties.ReadOnly = True
            Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
            Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", xPUR_ID))
            Me.GLookUp_PurList.EditValue = xPUR_ID
            Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
            Me.GLookUp_PurList.Properties.Tag = "SHOW"

            Me.GLookUp_PurList.Enabled = False
            Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
        End If

    End Sub
    Private Sub Pending_List()
        USE_CROSS_REF = False
        Dim d1 As DataSet = Base._Internal_Tf_Voucher_DBOps.GetUnMatchedList(1, Nothing)
        Dim P1 As DataTable = d1.Tables(0)
        If P1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        xPleaseWait.Hide()
        If P1.Rows.Count > 0 Then
            Dim xfrm As New Frm_I_Transfer_Pending : xfrm.MainBase = Base
            xfrm.Text = "Pending Internal Transfer Entries"
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                USE_CROSS_REF = True : Me.Text = "Match ~ " & Me.TitleX.Text
                CROSS_REF_ID = xfrm._ID
                CROSS_M_ID = xfrm._M_ID
                FR_REC_EDIT_ON = xfrm._EDIT_DATE
                Dim xItem_ID As String = xfrm._Item_ID
                If xItem_ID.ToString.Length > 0 Then
                    Me.GLookUp_ItemList.Properties.ReadOnly = True
                    Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                    Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", xItem_ID))
                    Me.GLookUp_ItemList.EditValue = xItem_ID
                    Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                    Me.GLookUp_ItemList.Properties.Tag = "SHOW"

                    Me.GLookUp_ItemList.Enabled = False
                    Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                    GLookUp_ItemList_Leave(Nothing, Nothing)
                End If
                
                Dim xDate As DateTime = Nothing : xDate = xfrm._Date : Txt_V_Date.DateTime = xDate : Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                
                Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
                If iTrans_Type.ToString.ToUpper = "DEBIT" Then
                    Tr_AB_ID_1 = xfrm._CEN_ID
                    Tr_AB_ID_2 = Base._open_Cen_Rec_ID
                Else
                    Tr_AB_ID_1 = Base._open_Cen_Rec_ID
                    Tr_AB_ID_2 = xfrm._CEN_ID
                End If

                If Tr_AB_ID_1.Length > 0 Then
                    Me.GLookUp_ToCen_List.ShowPopup() : Me.GLookUp_ToCen_List.ClosePopup()
                    Me.GLookUp_ToCen_ListView.MoveBy(Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1))
                    Me.GLookUp_ToCen_ListView.FocusedRowHandle = Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1)
                    Me.GLookUp_ToCen_List.EditValue = Tr_AB_ID_1
                    Me.GLookUp_ToCen_List.Tag = Me.GLookUp_ToCen_List.EditValue
                    Me.GLookUp_ToCen_List.Properties.Tag = "SHOW"

                    Me.GLookUp_ToCen_List.Enabled = False
                    Me.GLookUp_ToCen_List.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_To_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                End If

                If Tr_AB_ID_2.Length > 0 Then
                    Me.GLookUp_FrCen_List.ShowPopup() : Me.GLookUp_FrCen_List.ClosePopup()
                    Me.GLookUp_FrCen_ListView.MoveBy(Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2))
                    Me.GLookUp_FrCen_ListView.FocusedRowHandle = Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2)
                    Me.GLookUp_FrCen_List.EditValue = Tr_AB_ID_2
                    Me.GLookUp_FrCen_List.Tag = Me.GLookUp_FrCen_List.EditValue
                    Me.GLookUp_FrCen_List.Properties.Tag = "SHOW"

                    Me.GLookUp_FrCen_List.Enabled = False
                    Me.GLookUp_FrCen_List.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Fr_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                End If

                Me.Cmd_Mode.Text = xfrm._Mode : Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                Ref_Bank_ID = "" : Ref_Branch = ""

                If Cmd_Mode.Text.ToUpper <> "CASH" Then
                    Me.Txt_V_Date.Enabled = True
                    Dim xBANK_ID As String = xfrm._BI_ID
                    Dim xREF_BANK_ID As String = xfrm._REF_BI_ID
                    Dim xREF_ACC_NO As String = xfrm._BI_ACC_NO
                    If xBANK_ID.ToString.Length > 0 Then
                        Me.GLookUp_TrfBankList.Properties.ReadOnly = True
                        Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()

                        If iTrans_Type.ToUpper = "CREDIT" Then
                            If Cmd_Mode.Text.ToUpper = "DD" Then '#5539 fix
                                Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                                Me.lbl_Trf_ANo.Tag = xREF_BANK_ID
                                Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID

                                Me.Txt_Trf_Branch.Text = xfrm._REF_BRANCH
                                Me.Txt_Trf_ANo.Text = xfrm._REF_BANK_ACC_NO
                            Else
                                Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("BA_REC_ID", xBANK_ID) '#5539 fix
                                xBANK_ID = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BI_ID").ToString()
                                Me.lbl_Trf_ANo.Tag = xBANK_ID
                                Me.GLookUp_TrfBankList.EditValue = xBANK_ID

                                Me.Txt_Trf_Branch.Text = xfrm._BI_BRANCH
                                Me.Txt_Trf_ANo.Text = xfrm._BI_ACC_NO
                                Ref_Bank_ID = xfrm._REF_BI_ID : Ref_Branch = xfrm._REF_BRANCH
                            End If
                        End If
                        If iTrans_Type.ToUpper = "DEBIT" Then
                            If Cmd_Mode.Text.ToUpper = "DD" Or Cmd_Mode.Text.ToUpper = "CHEQUE" Then '#4037 FIX
                                Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                                Me.lbl_Trf_ANo.Tag = xREF_BANK_ID
                                Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID

                                Me.Txt_Trf_Branch.Text = xfrm._BI_BRANCH
                                Me.Txt_Trf_ANo.Text = xfrm._BI_ACC_NO
                            Else
                                Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("BA_REC_ID", xBANK_ID)
                                xBANK_ID = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BI_ID").ToString()
                                Me.lbl_Trf_ANo.Tag = xBANK_ID
                                Me.GLookUp_TrfBankList.EditValue = xBANK_ID
                            End If
                        End If
                        Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                        Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
                        Me.GLookUp_TrfBankList.Properties.ReadOnly = False
                        Me.GLookUp_TrfBankList.Enabled = False
                        Me.GLookUp_TrfBankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                        Me.Txt_Trf_Branch.Enabled = False : Me.Txt_Trf_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                        Me.Txt_Trf_ANo.Enabled = False : Me.Txt_Trf_ANo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                        If BankCount = 1 Then
                            If (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT") Then 'Or (iTrans_Type.ToUpper = "DEBIT" And Cmd_Mode.Text.ToUpper = "CHEQUE")
                                If xREF_BANK_ID.ToString.Length > 0 Then
                                    'Me.GLookUp_BankList.Properties.ReadOnly = True
                                    Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                                    Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BANK_ACC_NO", xREF_ACC_NO)
                                    Dim xRef_BA_ID As String = ""
                                    If Me.GLookUp_BankListView.FocusedRowHandle >= 0 Then
                                        xRef_BA_ID = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString()
                                        Me.GLookUp_BankList.EditValue = xRef_BA_ID
                                        Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                                        Me.GLookUp_BankList.Properties.Tag = "SHOW"
                                        Me.GLookUp_BankList.Properties.ReadOnly = False
                                        Me.GLookUp_BankList.Enabled = False
                                        Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                        Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                        Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If (Cmd_Mode.Text.ToUpper = "CASH TO BANK" And iTrans_Type.ToUpper = "CREDIT") Then
                        If xREF_BANK_ID.ToString.Length > 0 Then
                            'Me.GLookUp_BankList.Properties.ReadOnly = True
                            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BANK_ACC_NO", xREF_ACC_NO)
                            Dim xRef_BA_ID As String = ""
                            If Me.GLookUp_BankListView.FocusedRowHandle >= 0 Then
                                xRef_BA_ID = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BA_ID").ToString()
                                Me.GLookUp_BankList.EditValue = xRef_BA_ID
                                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                                Me.GLookUp_BankList.Properties.Tag = "SHOW"
                                Me.GLookUp_BankList.Properties.ReadOnly = False
                                Me.GLookUp_BankList.Enabled = False
                                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                            End If
                        End If
                    End If

                    If Cmd_Mode.Text.ToUpper = "CASH TO DD" Then
                        If xREF_BANK_ID.ToString.Length > 0 Then
                            Me.GLookUp_TrfBankList.Properties.ReadOnly = True
                            Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()
                            Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", xREF_BANK_ID)
                            Me.GLookUp_TrfBankList.EditValue = xREF_BANK_ID
                            Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                            Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
                            Me.GLookUp_TrfBankList.Properties.ReadOnly = False
                            Me.GLookUp_TrfBankList.Enabled = False
                            Me.GLookUp_TrfBankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                            Me.Txt_Trf_Branch.Text = xfrm._REF_BRANCH : Me.Txt_Trf_Branch.Enabled = False : Me.Txt_Trf_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                            Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_ANo.Enabled = False : Me.Txt_Trf_ANo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                        End If
                    End If

                    Me.Txt_Ref_No.Text = xfrm._BI_REF_NO : Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                    If iTrans_Type.ToUpper = "DEBIT" And Cmd_Mode.Text.ToUpper = "DD" Then
                        Me.Txt_DD_Fr_Chq_No.Text = xfrm._REF_OTHERS : Me.Txt_DD_Fr_Chq_No.Enabled = True
                    End If

                    If Not IsDBNull(xfrm._BI_REF_DT) Then
                        xDate = xfrm._BI_REF_DT : Me.Txt_Ref_Date.DateTime = xDate : Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    End If
                End If

                Me.Txt_Amount.Text = Format(xfrm._Amount, "#0.00") : Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                Me.Txt_Bank_Chg.Text = "0.00" : Me.Txt_Bank_Chg.Enabled = False : Me.Txt_Bank_Chg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                Me.Txt_Total.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                Dim xPUR_ID As String = xfrm._PUR_ID
                If xPUR_ID.ToString.Length > 0 Then
                    Me.GLookUp_PurList.Properties.ReadOnly = True
                    Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                    Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", xPUR_ID))
                    Me.GLookUp_PurList.EditValue = xPUR_ID
                    Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                    Me.GLookUp_PurList.Properties.Tag = "SHOW"

                    Me.GLookUp_PurList.Enabled = False
                    Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                End If

            End If
            xfrm.Dispose()
        End If
    End Sub

    Private Sub Load_from_Last_voucher()
        If Selected_Mode.Length > 0 Then
            If Selected_Item_ID.ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", Selected_Item_ID))
                Me.GLookUp_ItemList.EditValue = Selected_Item_ID
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
                GLookUp_ItemList_Leave(Nothing, Nothing)
            End If

            Dim xDate As DateTime = Nothing : xDate = Selected_V_Date : Txt_V_Date.DateTime = xDate : Me.Cmd_Mode.Text = Selected_Mode

            If Cmd_Mode.Text.ToUpper <> "CASH" Then
                Me.Txt_V_Date.Enabled = True
                Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
                Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Selected_Bank_ID)
                Me.GLookUp_BankList.EditValue = Selected_Bank_ID
                Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
                Me.GLookUp_BankList.Properties.Tag = "SHOW"
                Me.GLookUp_BankList.Properties.ReadOnly = False
                Me.GLookUp_BankList.Enabled = True
            End If
        End If
        If Selected_Amount > 0 Then
            Txt_Amount.Text = Selected_Amount
            If Selected_Ref_Date <> DateTime.MinValue Then Txt_Ref_Date.DateTime = Selected_Ref_Date
            If Selected_RefC_Date <> DateTime.MinValue Then Txt_Ref_CDate.DateTime = Selected_RefC_Date
            Txt_Ref_No.Text = Selected_RefNo
            If Selected_Drawee_Bank_ID.ToString.Length > 0 Then
                Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()
                Me.GLookUp_TrfBankListView.MoveBy(Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", Selected_Drawee_Bank_ID))
                Me.GLookUp_TrfBankList.EditValue = Selected_Drawee_Bank_ID
                Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
            End If
            Txt_Trf_Branch.Text = Selected_Drawee_Branch
        End If
    End Sub
    Private Sub Data_Binding()
        '1
        Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(Me.xMID.Text, 1)
        Dim d4 As DataTable = Base._Rect_DBOps.GetSlipRecord(Me.xMID.Text)
        Dim d5 As DataTable = Nothing
        If Not d4 Is Nothing Then
            If d4.Rows.Count > 0 Then
                d5 = Base._Voucher_DBOps.GetSlipMAsterRecord(d4.Rows(0)("TR_SLIP_ID").ToString)
            End If
        End If
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d1.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transfer", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        If d1 Is Nothing Or d1.Rows.Count <= 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Me.xID_1.Text = d1.Rows(0)("REC_ID")
        '2
        Dim d2 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetRecord(Me.xMID.Text, 2)
        If d2 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If d2.Rows.Count > 0 Then
            Me.xID_2.Text = d2.Rows(0)("REC_ID")
        Else
            Me.xID_2.Text = ""
        End If
        '3
        Dim d3 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetPurposeRecord(Me.xID_1.Text)
        If d3 Is Nothing Or d3.Rows.Count <= 0 Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        Cmd_Mode.DataBindings.Add("text", d1, "TR_MODE")
        Me.Cmd_Mode.Properties.AccessibleDescription = Cmd_Mode.Text

        If Not IsDBNull(d1.Rows(0)("TR_ITEM_ID")) Then
            If d1.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d1.Rows(0)("TR_ITEM_ID")))
                Me.GLookUp_ItemList.EditValue = d1.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If
        End If
        Me.GLookUp_ItemList.Properties.ReadOnly = False
        GLookUp_ItemList_Leave(Nothing, Nothing)

        Dim xDate As DateTime = Nothing
        xDate = d1.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate

        Cmd_Mode.Text = d1.Rows(0)("TR_MODE")
        Me.Cmd_Mode.Properties.AccessibleDescription = Cmd_Mode.Text

        Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
        If d1.Rows(0)("TR_TYPE").ToString.ToUpper = "DEBIT" Then
            Tr_AB_ID_1 = d1.Rows(0)("Tr_AB_ID_1").ToString
            Tr_AB_ID_2 = d1.Rows(0)("Tr_AB_ID_2").ToString
        Else
            Tr_AB_ID_1 = d1.Rows(0)("Tr_AB_ID_2").ToString
            Tr_AB_ID_2 = d1.Rows(0)("Tr_AB_ID_1").ToString
        End If

        If Tr_AB_ID_1.Length > 0 Then
            Me.GLookUp_ToCen_List.ShowPopup() : Me.GLookUp_ToCen_List.ClosePopup()
            Me.GLookUp_ToCen_ListView.MoveBy(Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1))
            Me.GLookUp_ToCen_ListView.FocusedRowHandle = Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", Tr_AB_ID_1)
            Me.GLookUp_ToCen_List.EditValue = Tr_AB_ID_1
            Me.GLookUp_ToCen_List.Tag = Me.GLookUp_ToCen_List.EditValue
            Me.GLookUp_ToCen_List.Properties.Tag = "SHOW"
        End If : Me.GLookUp_ToCen_List.Properties.ReadOnly = False

        If Tr_AB_ID_2.Length > 0 Then
            Me.GLookUp_FrCen_List.ShowPopup() : Me.GLookUp_FrCen_List.ClosePopup()
            Me.GLookUp_FrCen_ListView.MoveBy(Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2))
            Me.GLookUp_FrCen_ListView.FocusedRowHandle = Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", Tr_AB_ID_2)
            Me.GLookUp_FrCen_List.EditValue = Tr_AB_ID_2
            Me.GLookUp_FrCen_List.Tag = Me.GLookUp_FrCen_List.EditValue
            Me.GLookUp_FrCen_List.Properties.Tag = "SHOW"
        End If : Me.GLookUp_FrCen_List.Properties.ReadOnly = False

        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If (Not IsDBNull(d1.Rows(0)("TR_SUB_CR_LED_ID")) And IIf(IsDBNull(d1.Rows(0)("TR_CR_LED_ID")), "", d1.Rows(0)("TR_CR_LED_ID")) = "00079") Then Bank_ID = d1.Rows(0)("TR_SUB_CR_LED_ID")
        Else
            If Not IsDBNull(d1.Rows(0)("TR_SUB_DR_LED_ID")) And IIf(IsDBNull(d1.Rows(0)("TR_DR_LED_ID")), "", d1.Rows(0)("TR_DR_LED_ID")) = "00079" Then Bank_ID = d1.Rows(0)("TR_SUB_DR_LED_ID")
        End If

        If Bank_ID.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID)
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        Txt_Ref_No.DataBindings.Add("text", d1, "TR_REF_NO")
        If Not IsDBNull(d1.Rows(0)("TR_REF_DATE")) Then
            xDate = d1.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d1.Rows(0)("TR_REF_CDATE")) Then
            xDate = d1.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If
        If Not IsDBNull(d1.Rows(0)("TR_REF_OTHERS")) Then
            Txt_DD_Fr_Chq_No.DataBindings.Add("text", d1, "TR_REF_OTHERS")
        End If

        If Not IsDBNull(d1.Rows(0)("TR_REF_BANK_ID")) Then
            If d1.Rows(0)("TR_REF_BANK_ID").ToString.Length > 0 Then
                Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()
                Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", d1.Rows(0)("TR_REF_BANK_ID"))
                'If iTrans_Type = "DEBIT" Then
                'Bug 5091 fixed
                If (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CHEQUE" Or (Cmd_Mode.Text.ToUpper = "CASH TO BANK")) Then
                    If Not IsDBNull(d1.Rows(0)("TR_MT_ACC_NO")) Then
                        If d1.Rows(0)("TR_MT_ACC_NO").ToString.Length > 0 Then
                            Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BA_ACCOUNT_NO", d1.Rows(0)("TR_MT_ACC_NO"))
                        End If
                    End If
                End If
                'End If
                Me.GLookUp_TrfBankList.EditValue = d1.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankList.EditValue
                Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"

                'Shifted for testing 
                Txt_Trf_Branch.DataBindings.Add("text", d1, "TR_REF_BRANCH")
                Txt_Trf_ANo.DataBindings.Add("text", d1, "TR_MT_ACC_NO")
            End If
        End If

        Me.GLookUp_TrfBankList.Properties.ReadOnly = False

        Txt_Amount.DataBindings.Add("EditValue", d1, "TR_AMOUNT")
        If d2.Rows.Count > 0 Then
            Txt_Bank_Chg.DataBindings.Add("EditValue", d2, "TR_AMOUNT")
        Else
            Txt_Bank_Chg.Text = "0.00"
        End If

        If Not IsDBNull(d3.Rows(0)("TR_PURPOSE_MISC_ID")) Then
            If d3.Rows(0)("TR_PURPOSE_MISC_ID").ToString.Length > 0 Then
                Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", d3.Rows(0)("TR_PURPOSE_MISC_ID")))
                Me.GLookUp_PurList.EditValue = d3.Rows(0)("TR_PURPOSE_MISC_ID")
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                Me.GLookUp_PurList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PurList.Properties.ReadOnly = False


        Txt_Narration.DataBindings.Add("text", d1, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d1, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d1, "TR_REFERENCE")
        If Not d5 Is Nothing Then Txt_Slip_No.DataBindings.Add("text", d5, "SL_NO")
        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_FrCen_List.Enabled = False : Me.GLookUp_FrCen_List.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_Pad_No.Enabled = False : Me.BE_Fr_Pad_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_Incharge.Enabled = False : Me.BE_Fr_Incharge.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_Institute.Enabled = False : Me.BE_Fr_Institute.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_Tel_No.Enabled = False : Me.BE_Fr_Tel_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_UID.Enabled = False : Me.BE_Fr_UID.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Fr_Zone.Enabled = False : Me.BE_Fr_Zone.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_ToCen_List.Enabled = False : Me.GLookUp_ToCen_List.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_Pad_No.Enabled = False : Me.BE_To_Pad_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_Incharge.Enabled = False : Me.BE_To_Incharge.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_Institute.Enabled = False : Me.BE_To_Institute.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_Tel_No.Enabled = False : Me.BE_To_Tel_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_UID.Enabled = False : Me.BE_To_UID.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_To_Zone.Enabled = False : Me.BE_To_Zone.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Branch.Enabled = False : Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Acc_No.Enabled = False : Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_TrfBankList.Enabled = False : Me.GLookUp_TrfBankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Trf_Branch.Enabled = False : Me.Txt_Trf_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Trf_ANo.Enabled = False : Me.Txt_Trf_ANo.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_DD_Fr_Chq_No.Enabled = False : Me.Txt_DD_Fr_Chq_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Bank_Chg.Enabled = False : Me.Txt_Bank_Chg.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Total.Enabled = False : Me.Txt_Total.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.GLookUp_FrCen_List)
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

                'NOTE: OTHER PART WRITTEN IN GLookUp_ItemList_Leave Procedure
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_ItemList_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles GLookUp_ItemList.Leave
        If Not ShowChangeItemEffects Then ShowChangeItemEffects = True : Exit Sub

        If Me.GLookUp_ItemList.Properties.Tag = "SHOW" And Me.GLookUp_ItemList.Tag.ToString.Length > 0 Then
            If iTrans_Type.ToUpper = "DEBIT" Then
                Me.lbl_bank_title.Text = "Bank Reference Detail:"
                Me.Cmd_Mode.Properties.Items.Clear() : Me.Cmd_Mode.Properties.Items.AddRange(New Object() {"CASH", "CASH TO DD", "CHEQUE", "DD", "CBS", "RTGS", "NEFT", "CASH TO BANK"}) : Me.Cmd_Mode.SelectedIndex = 0
                If Val(Me.Tag) <> Common_Lib.Common.Navigation_Mode._New And Val(Me.Tag) <> Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                    Me.Cmd_Mode.Text = Me.Cmd_Mode.Properties.AccessibleDescription
                End If
                If (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "DD") Then
                    GLookUp_TrfBankList.Enabled = True : Txt_Trf_ANo.Enabled = True : Txt_Trf_Branch.Enabled = True
                    lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Red : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                Else
                    GLookUp_TrfBankList.Enabled = False : Txt_Trf_ANo.Enabled = False : Txt_Trf_Branch.Enabled = False
                    lbl_Trf_Bank.ForeColor = Color.Gray : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                    lbl_Trf_ANo.ForeColor = Color.Gray : Txt_Trf_ANo.Text = ""
                    lbl_Trf_Branch.ForeColor = Color.Gray : Txt_Trf_Branch.Text = ""
                End If
            Else
                Me.lbl_bank_title.Text = "Deposited Bank Detail:"
                lbl_Trf_Bank.ForeColor = Color.Red : Me.GLookUp_TrfBankList.Tag = "" : GLookUp_TrfBankList.EditValue = ""
                lbl_Trf_ANo.ForeColor = Color.Red : Txt_Trf_ANo.Text = ""
                lbl_Trf_Branch.ForeColor = Color.Red : Txt_Trf_Branch.Text = ""
                Me.Cmd_Mode.Properties.Items.Clear() : Me.Cmd_Mode.Properties.Items.AddRange(New Object() {"CASH", "CHEQUE", "DD", "CBS", "RTGS", "NEFT", "CASH TO BANK"}) : Me.Cmd_Mode.SelectedIndex = 0
            End If

            '
            Me.GLookUp_FrCen_List.EditValue = ""
            Me.GLookUp_FrCen_List.Tag = ""
            Me.BE_Fr_Pad_No.Text = ""
            Me.BE_Fr_UID.Text = ""
            Me.BE_Fr_Incharge.Text = ""
            Me.BE_Fr_Zone.Text = ""
            Me.BE_Fr_Tel_No.Text = ""
            Me.BE_Fr_Institute.Text = ""
            '
            Me.GLookUp_ToCen_List.EditValue = ""
            Me.GLookUp_ToCen_List.Tag = ""
            Me.BE_To_Pad_No.Text = ""
            Me.BE_To_UID.Text = ""
            Me.BE_To_Incharge.Text = ""
            Me.BE_To_Zone.Text = ""
            Me.BE_To_Tel_No.Text = ""
            Me.BE_To_Institute.Text = ""
            '
            LookUp_Get_Fr_Centre()
            LookUp_Get_To_Centre()
            'GLookUp_ItemList.Focus()
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim ITEM_APPLICABLE As String = "" : If Base.Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"

        'Dim SQL_STR1 As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS [ITEM_ID]  " & _
        '                         " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.')             AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "

        Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetItemList(ITEM_APPLICABLE)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("ITEM_NAME") = "" : d1.Rows.Add(ROW)

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

    '1.GLookUp_FrCen_List - From Centre
    Private Sub GLookUp_FrCen_List_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_FrCen_List.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_FrCen_ListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_FrCen_ListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_FrCen_List.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_FrCen_ListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_FrCen_ListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_FrCen_List.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_FrCen_List_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_FrCen_List.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_FrCen_List.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_FrCen_List.CancelPopup()
            Hide_Properties()
            Me.Cmd_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_FrCen_List.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Mode.Focus()
        End If

    End Sub
    Private Sub GLookUp_FrCen_List_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_FrCen_List.EditValueChanged
        If Me.GLookUp_FrCen_List.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_FrCen_ListView.RowCount > 0 And Val(Me.GLookUp_FrCen_ListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_FrCen_List.Tag = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_ID").ToString
                iFR_CEN_ID = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_CEN_ID").ToString
                Me.BE_Fr_Pad_No.Text = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_PAD_NO").ToString
                Me.BE_Fr_UID.Text = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_UID").ToString
                Me.BE_Fr_Incharge.Text = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_INCHARGE").ToString
                Me.BE_Fr_Zone.Text = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_ZONE").ToString
                Me.BE_Fr_Tel_No.Text = Me.GLookUp_FrCen_ListView.GetRowCellValue(Me.GLookUp_FrCen_ListView.FocusedRowHandle, "FR_TEL_NO").ToString
                Me.BE_Fr_Institute.Text = Base._open_Ins_Name
                '
                If iTrans_Type.ToUpper = "CREDIT" And (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CHEQUE") Then
                    Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_Branch.Text = ""
                    LookUp_GetTrfBankList()
                End If
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_Fr_Centre()
        Me.GLookUp_FrCen_List.Properties.ReadOnly = True
        Dim SQL_1 As String = ""
        Dim D1 As DataTable = Nothing
        If iVoucher_Type.ToUpper.Trim = "INTERNAL TRANSFER WITH H.Q." Then
            If iTrans_Type = "DEBIT" Then
                D1 = Base._Internal_Tf_Voucher_DBOps.GetFrCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
            Else
                D1 = Base._Internal_Tf_Voucher_DBOps.GetFrCenterList(HQ_IDs, "", "", "")
            End If
        Else
            If iTrans_Type = "DEBIT" Then
                D1 = Base._Internal_Tf_Voucher_DBOps.GetFrCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
            Else
                D1 = Base._Internal_Tf_Voucher_DBOps.GetFrCenterList("", HQ_IDs, "", "'" & Base._open_Cen_Rec_ID & "'")
            End If
        End If
        If D1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(D1) : dview.Sort = "FR_CEN_NAME,FR_UID"
        Me.GLookUp_FrCen_List.Properties.ValueMember = "FR_ID"
        Me.GLookUp_FrCen_List.Properties.DisplayMember = "FR_CEN_NAME"
        Me.GLookUp_FrCen_List.Properties.DataSource = dview
        Me.GLookUp_FrCen_ListView.RefreshData()
        Me.GLookUp_FrCen_List.Properties.Tag = "SHOW"
        If dview.Count <= 0 Then
            Me.GLookUp_FrCen_List.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_FrCen_List.ShowPopup() : Me.GLookUp_FrCen_List.ClosePopup() : Me.GLookUp_FrCen_ListView.FocusedRowHandle = Me.GLookUp_FrCen_ListView.LocateByValue("FR_ID", dview.Item(0).Row("FR_ID").ToString) : Me.GLookUp_FrCen_List.EditValue = dview.Item(0).Row("FR_ID").ToString : Me.GLookUp_FrCen_List.Enabled = False
            BE_Fr_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_Fr_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_Fr_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_Fr_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_Fr_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_Fr_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_FrCen_List.Properties.ReadOnly = False : Me.GLookUp_FrCen_List.Enabled = True
            BE_Fr_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_Fr_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_Fr_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_Fr_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_Fr_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_Fr_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        End If

    End Sub
    Private Sub GLookUp_FrCen_List_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GLookUp_FrCen_List.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_FrCen(sender)))
    End Sub
    Private Sub FilterLookup_FrCen(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        ' Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("FR_CEN_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("FR_UID", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("FR_INCHARGE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("FR_ZONE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub

    '2.GLookUp_ToCen_List - To Centre
    Private Sub GLookUp_ToCen_List_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_ToCen_List.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_ToCen_ListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_ToCen_ListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_ToCen_List.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_ToCen_ListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_ToCen_ListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_ToCen_List.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_ToCen_List_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_ToCen_List.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_ToCen_List.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_ToCen_List.CancelPopup()
            Hide_Properties()
            Me.GLookUp_FrCen_List.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_ToCen_List.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_FrCen_List.Focus()
        End If

    End Sub
    Private Sub GLookUp_ToCen_List_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_ToCen_List.EditValueChanged
        If Me.GLookUp_ToCen_List.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_ToCen_ListView.RowCount > 0 And Val(Me.GLookUp_ToCen_ListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_ToCen_List.Tag = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_ID").ToString
                iTO_CEN_ID = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_CEN_ID").ToString
                Me.BE_To_Pad_No.Text = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_PAD_NO").ToString
                Me.BE_To_UID.Text = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_UID").ToString
                Me.BE_To_Incharge.Text = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_INCHARGE").ToString
                Me.BE_To_Zone.Text = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_ZONE").ToString
                Me.BE_To_Tel_No.Text = Me.GLookUp_ToCen_ListView.GetRowCellValue(Me.GLookUp_ToCen_ListView.FocusedRowHandle, "TO_TEL_NO").ToString
                Me.BE_To_Institute.Text = Base._open_Ins_Name

                If iTrans_Type.ToUpper = "DEBIT" And (Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK") Then
                    Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_Branch.Text = ""
                    LookUp_GetTrfBankList()
                End If
                If USE_CROSS_REF = True Then
                    If iTrans_Type.ToUpper = "DEBIT" And (Cmd_Mode.Text.ToUpper = "CHEQUE" Or Cmd_Mode.Text.ToUpper = "DD") Then
                        Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_Branch.Text = ""
                        LookUp_GetTrfBankList()
                    End If
                End If
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_To_Centre()
        Me.GLookUp_ToCen_List.Properties.ReadOnly = True
        Dim SQL_1 As String = ""
        Dim D1 As DataTable = Nothing
        If iVoucher_Type.ToUpper.Trim = "INTERNAL TRANSFER WITH H.Q." Then
            If iTrans_Type = "DEBIT" Then
                D1 = Base._Internal_Tf_Voucher_DBOps.GetToCenterList(HQ_IDs, "", "", "")
            Else
                D1 = Base._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
            End If
        Else
            If iTrans_Type = "DEBIT" Then
                D1 = Base._Internal_Tf_Voucher_DBOps.GetToCenterList("", HQ_IDs, "", "'" & Base._open_Cen_Rec_ID & "'")
            Else
                D1 = Base._Internal_Tf_Voucher_DBOps.GetToCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
            End If
        End If
        If D1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(D1) : dview.Sort = "TO_CEN_NAME,TO_UID"
        Me.GLookUp_ToCen_List.Properties.ValueMember = "TO_ID"
        Me.GLookUp_ToCen_List.Properties.DisplayMember = "TO_CEN_NAME"
        Me.GLookUp_ToCen_List.Properties.DataSource = dview
        Me.GLookUp_ToCen_ListView.RefreshData()
        Me.GLookUp_ToCen_List.Properties.Tag = "SHOW"
        If dview.Count <= 0 Then
            Me.GLookUp_ToCen_List.Properties.Tag = "NONE"
        End If

        If dview.Count = 1 Then
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ToCen_List.ShowPopup() : Me.GLookUp_ToCen_List.ClosePopup() : Me.GLookUp_ToCen_ListView.FocusedRowHandle = Me.GLookUp_ToCen_ListView.LocateByValue("TO_ID", dview.Item(0).Row("TO_ID").ToString) : Me.GLookUp_ToCen_List.EditValue = dview.Item(0).Row("TO_ID").ToString : Me.GLookUp_ToCen_List.Enabled = False
            BE_To_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_To_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_To_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_To_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_To_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
            BE_To_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.DimGray
        Else
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_ToCen_List.Properties.ReadOnly = False : Me.GLookUp_ToCen_List.Enabled = True
            BE_To_Pad_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_To_UID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_To_Incharge.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_To_Zone.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_To_Tel_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            BE_To_Institute.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
        End If

    End Sub
    Private Sub GLookUp_ToCen_List_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles GLookUp_ToCen_List.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_ToCen(sender)))
    End Sub
    Private Sub FilterLookup_ToCen(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        ' Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("TO_CEN_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("TO_UID", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("TO_INCHARGE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("TO_ZONE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub

    '3.GLookUp_BankList
    Dim BankCount As Integer = 0
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

                If USE_CROSS_REF = False Then
                    Me.lbl_Trf_ANo.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ID").ToString
                End If
                If Cmd_Mode.Text = "CBS" And (Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection) Then
                    Me.GLookUp_TrfBankList.ShowPopup() : Me.GLookUp_TrfBankList.ClosePopup()
                    Me.GLookUp_TrfBankListView.FocusedRowHandle = Me.GLookUp_TrfBankListView.LocateByValue("TRF_BI_ID", Me.lbl_Trf_ANo.Tag)
                    Me.GLookUp_TrfBankList.EditValue = Me.lbl_Trf_ANo.Tag
                    Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
                    ' Me.GLookUp_RefBankList.Properties.ReadOnly = False
                    GLookUp_BankList.Focus()
                End If
                If iTrans_Type = "CREDIT" Then
                    Me.Txt_Slip_No.Text = Base._DepositSlipsDBOps.GetMaxOpenSlipNo(Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Donation)
                    Me.Txt_Slip_Count.Text = Base._DepositSlipsDBOps.GetSlipTxnCount(Val(Me.Txt_Slip_No.Text), Me.GLookUp_BankList.Tag, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Donation, Me.xMID.Text)
                    Me.Txt_Slip_No.Enabled = True
                Else
                    Me.Txt_Slip_No.Enabled = False
                End If
            Else
                Me.Txt_Slip_No.Enabled = False
            End If
        Else
            Me.Txt_Slip_No.Enabled = False
        End If
    End Sub
    Private Sub GLookUp_BankList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_BankList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup_bank(sender)))
    End Sub
    Private Sub FilterLookup_bank(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        ' Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("BANK_NAME", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("BANK_BRANCH", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("BANK_ACC_NO", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._Internal_Tf_Voucher_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._Internal_Tf_Voucher_DBOps.GetBranches(Branch_IDs)
        If BB_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'BUILD DATA
        Dim BuildData = From B In BB_Table, A In BA_Table _
                        Where (B.Field(Of String)("BB_BRANCH_ID") = A.Field(Of String)("BA_BRANCH_ID")) _
                        Select New With { _
                                        .BANK_NAME = B.Field(Of String)("Name"), _
                                        .BI_SHORT_NAME = B.Field(Of String)("BI_SHORT_NAME"), _
                                        .BANK_BRANCH = B.Field(Of String)("Branch"), _
                                        .BANK_ACC_NO = A.Field(Of String)("BA_ACCOUNT_NO"), _
                                        .BA_ID = A.Field(Of String)("ID"), _
                                        .BANK_ID = B.Field(Of String)("BANK_ID"), _
                                        .REC_EDIT_ON = A.Field(Of DateTime)("REC_EDIT_ON")
                                        } : Dim Final_Data = BuildData.ToList

        BankCount = Final_Data.Count
        If Final_Data.Count > 0 Then
            Me.GLookUp_BankList.Properties.ValueMember = "BA_ID"
            Me.GLookUp_BankList.Properties.DisplayMember = "BANK_NAME"
            Me.GLookUp_BankList.Properties.DataSource = Final_Data
            Me.GLookUp_BankListView.RefreshData()
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        Else
            'Me.Cmd_Mode.Enabled = False
            Me.GLookUp_BankList.Properties.Tag = "NONE"
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_BankList.Properties.ReadOnly = False

    End Sub

    '5.GLookUp_TrfBankList
    Private Sub GLookUp_TrfBankList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_TrfBankList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_TrfBankListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_TrfBankListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_TrfBankList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_TrfBankListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_TrfBankListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_TrfBankList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_TrfBankList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_TrfBankList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_TrfBankList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_TrfBankList.CancelPopup()
            Hide_Properties()
            Me.Cmd_Mode.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_TrfBankList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_Mode.Focus()
        End If

    End Sub
    Private Sub GLookUp_TrfBankList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_TrfBankList.EditValueChanged
        If Me.GLookUp_TrfBankList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_TrfBankListView.RowCount > 0 And Val(Me.GLookUp_TrfBankListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_TrfBankList.Tag = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BI_ID").ToString

                Try
                    If Cmd_Mode.Text.ToUpper = "CHEQUE" Or Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
                        Me.Txt_Trf_ANo.Text = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BA_ACCOUNT_NO").ToString
                        Me.Txt_Trf_Branch.Text = Me.GLookUp_TrfBankListView.GetRowCellValue(Me.GLookUp_TrfBankListView.FocusedRowHandle, "TRF_BB_BRANCH_NAME").ToString
                    End If
                Catch ex As Exception
                End Try
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetTrfBankList()
        Dim B2 As DataTable = Nothing
        If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "CHEQUE" Or Cmd_Mode.Text.ToUpper = "CASH TO BANK" Then
            Dim XCEN_ID As String = "" : If iTrans_Type.ToUpper = "DEBIT" Then XCEN_ID = iTO_CEN_ID Else XCEN_ID = iFR_CEN_ID
            B2 = Base._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(True, XCEN_ID)
        Else
            B2 = Base._Internal_Tf_Voucher_DBOps.Get_Tf_Banks(False)
        End If
        If B2 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(B2)
        'If iTrans_Type.ToUpper = "DEBIT" Then dview.RowFilter = "BA_FERA_ACC = 'NO'" 'FCRA banks not to be shown in Debit transactions 
        Me.GLookUp_TrfBankList.Properties.ValueMember = "TRF_BI_ID"
        Me.GLookUp_TrfBankList.Properties.DisplayMember = "TRF_BI_BANK_NAME"
        Me.GLookUp_TrfBankList.Properties.DataSource = dview
        Me.GLookUp_TrfBankListView.RefreshData()
        Me.GLookUp_TrfBankList.Properties.Tag = "SHOW"
        If dview.Count <= 0 Then
            Me.GLookUp_TrfBankList.Properties.Tag = "NONE"
            Me.Txt_Trf_ANo.Text = "" : Me.Txt_Trf_Branch.Text = ""
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_TrfBankList.Properties.ReadOnly = False


    End Sub

    '6.GLookUp_PurList
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
            Me.Txt_Amount.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Amount.Focus()
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
        Dim d1 As DataTable = Base._Internal_Tf_Voucher_DBOps.GetPurposes()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
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
#End Region

End Class