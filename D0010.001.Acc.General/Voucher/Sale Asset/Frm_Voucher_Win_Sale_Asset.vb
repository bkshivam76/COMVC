Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Voucher_Win_Sale_Asset

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iRef_ItemID As String = ""
    Private iTrans_Type As String = ""
    Private iAsset_Type As String = ""
    Private iLed_ID As String = ""
    Public iCon_Led_ID As String = ""
    'Public iMinValue As Double = 0
    'Public iMaxValue As Double = 0

    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer

    Public iParty_Req As String = ""
    Public iProfile As String = ""

    Public iTDS_CODE As String = ""
    Public iOffSet_ID As String = ""
    Public iOffSet_Item As String = ""
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Public Info_MaxEditedOn As DateTime = DateTime.MinValue
    Private SaleProfit_ItemID As String = "F298C1EF-29F4-4B52-BFCD-EC120E9404F9"
    Private SaleLoss_ItemID As String = "ADA231AD-390F-4665-B866-ACA0DAA43055"
    Private SaleProfit_Loss_LedID As String = "00060"
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
        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
            If Cmb_Asset_Type.SelectedIndex = 4 Or Cmb_Asset_Type.SelectedIndex = 5 Then 'Movable Assets or Property
                If Base.IsInsuranceAudited() Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Changes cannot be done after the completion of Insurance Audit", "Information..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        Dim xPromptWindow As New Common_Lib.Prompt_Window
        If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then Exit Sub
        End If

        If Base.AllowMultiuser() Then
            If Me.GLookUp_BankList.Tag.ToString.Length > 0 Then 'BUG #5004 FIXED
                'Closed Bank Acc Check #G42
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
                Dim saleofasset_DbOps As DataTable = Base._SaleOfAsset_DBOps.GetRecord(Me.xMID.Text)
                If saleofasset_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If saleofasset_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Sale"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(saleofasset_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Sale"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'ED/L
                Dim MaxValue As Object = 0
                MaxValue = Base._SaleOfAsset_DBOps.GetStatus(Me.xID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'Special Checks

                Dim xTemp_AdvID As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(Me.xMID.Text) 'Get advance cretaed by current Txn
                If xTemp_AdvID.Length > 0 Then
                    Dim xTemp_RefRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AdvID) 'Advance is referred in a transaction
                    If Not xTemp_RefRecord Is Nothing Then
                        If xTemp_RefRecord.Rows.Count > 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Some Refunds were made on " & Convert.ToDateTime(xTemp_RefRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & xTemp_RefRecord.Rows(0)("TR_AMOUNT").ToString() & " for the Sale amount Receivable raised by current entry." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
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
            If iParty_Req.ToString.Trim.ToUpper = "YES" Then
                If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("P a r t y   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
                    Me.GLookUp_PartyList1.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
                End If
            End If
            If Me.Cmb_Asset_Type.SelectedIndex < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A s s e t   T y p e   N o t   S e l e c t e d . . . !", Me.Cmb_Asset_Type, 0, Me.Cmb_Asset_Type.Height, 5000)
                Me.Cmb_Asset_Type.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Cmb_Asset_Type)
            End If

            If Len(Trim(Me.GLookUp_AssetList.Tag)) = 0 Or Len(Trim(Me.GLookUp_AssetList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A s s e t   I t e m   N o t   S e l e c t e d . . . !", Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                Me.GLookUp_AssetList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_AssetList)
            End If
            If IsDate(Me.Txt_SDate.Text) = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D a t e   I n c o r r e c t   /   B l a n k . . . !", Me.Txt_SDate, 0, Me.Txt_SDate.Height, 5000)
                Me.Txt_SDate.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_SDate)
            End If
            If IsDate(Me.Txt_SDate.Text) = True Then
                '1
                Dim diff As Double = DateDiff(DateInterval.Day, Base._open_Year_Sdt, DateValue(Me.Txt_SDate.Text))
                If diff < 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_SDate, 0, Me.Txt_SDate.Height, 5000)
                    Me.Txt_SDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_SDate)
                End If
                '2
                diff = DateDiff(DateInterval.Day, Base._open_Year_Edt, DateValue(Me.Txt_SDate.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("D a t e   n o t   a s   p e r   F i n a n c i a l   Y e a r . . . !", Me.Txt_SDate, 0, Me.Txt_SDate.Height, 5000)
                    Me.Txt_SDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_SDate)
                End If
                '3
                diff = DateDiff(DateInterval.Day, DateValue(Me.Txt_V_Date.Text), DateValue(Me.Txt_SDate.Text))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("S a l e   D a t e   c a n n o t   b e   g r e a t e r   t h a n   V o u c h e r   D a t e  . . . !", Me.Txt_SDate, 0, Me.Txt_SDate.Height, 5000)
                    Me.Txt_SDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_SDate)
                End If
                '4
                diff = DateDiff(DateInterval.Day, DateValue(Me.Txt_SDate.Text), DateValue(IIf(IsDBNull(Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE")), Base._open_Year_Sdt, Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE"))))
                If diff > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("A s s e t   S a l e    D a t e   c a n n o t   b e    l e s s    t h a n   P u r c h a s e   D a t e  . . . !" & vbNewLine & vbNewLine & "Please Update Voucher Date to Change Sale Date.", Me.Txt_SDate, 0, Me.Txt_SDate.Height, 5000)
                    Me.Txt_SDate.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_SDate)
                End If
            End If
            If Val(Me.Txt_Qty.Text) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Q t y .   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If
            If Val(Me.Txt_Qty.Text) > Val(Me.Txt_CurQty.Text) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("Qty. cannot be greater than " & Val(Me.Txt_CurQty.Text) & "...!", Me.Txt_Qty, 0, Me.Txt_Qty.Height, 5000)
                Me.Txt_Qty.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Qty)
            End If
            If Val(Me.Txt_SaleAmt.Text) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_SaleAmt, 0, Me.Txt_SaleAmt.Height, 5000)
                Me.Txt_SaleAmt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_SaleAmt)
            End If

            'check property references , as location, before selling 
            If Cmb_Asset_Type.SelectedIndex = 5 Then
                Dim Message As String = FindLocationUsage(GLookUp_AssetList.Tag, True) 'excludes sold/tf assets
                If Message.Length > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(Message, Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                    Me.GLookUp_AssetList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_AssetList)
                End If
            Else
                Me.ToolTip1.Hide(Me.GLookUp_AssetList)
            End If

            If Val(Me.Txt_Amount.Text) > Val(Me.Txt_SaleAmt.Text) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   g r e a t e r   t h a n  " & Val(Me.Txt_SaleAmt.Text) & "...!", Me.Txt_Amount, 0, Me.Txt_Amount.Height, 5000)
                Me.Txt_Amount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Amount)
            End If

            If (Len(Trim(Me.GLookUp_BankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_BankList.Text)) = 0) And (Cmd_Mode.Text.ToUpper <> "CASH") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_BankList, 0, Me.GLookUp_BankList.Height, 5000)
                Me.GLookUp_BankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_BankList)
            End If
            If Len(Trim(Me.GLookUp_BankList.Text)) = 0 Then Me.GLookUp_BankList.Tag = ""

            If (Len(Trim(Me.GLookUp_RefBankList.Tag)) = 0 Or Len(Trim(Me.GLookUp_RefBankList.Text)) = 0) And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   N o t   S e l e c t e d . . . !", Me.GLookUp_RefBankList, 0, Me.GLookUp_RefBankList.Height, 5000)
                Me.GLookUp_RefBankList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_RefBankList)
            End If
            If Len(Trim(Me.GLookUp_RefBankList.Text)) = 0 Then Me.GLookUp_RefBankList.Tag = ""

            If Len(Trim(Me.Txt_Ref_Branch.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("B a n k   B r a n c h   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_Branch, 0, Me.Txt_Ref_Branch.Height, 5000)
                Me.Txt_Ref_Branch.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_Branch)
            End If
            If Len(Trim(Me.Txt_Ref_No.Text)) = 0 And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("N o .   N o t   S p e c i f i e d . . . !", Me.Txt_Ref_No, 0, Me.Txt_Ref_No.Height, 5000)
                Me.Txt_Ref_No.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_Ref_No)
            End If
            If IsDate(Me.Txt_Ref_Date.Text) = False And (Cmd_Mode.Text.ToUpper <> "CASH" And Cmd_Mode.Text.ToUpper <> "BANK ACCOUNT") Then
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


            If Len(Trim(Me.GLookUp_PurList.Tag)) = 0 Or Len(Trim(Me.GLookUp_PurList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("P u r p o s e   N o t   S e l e c t e d . . . !", Me.GLookUp_PurList, 0, Me.GLookUp_PurList.Height, 5000)
                Me.GLookUp_PurList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PurList)
            End If

            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim inparam As Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate = New Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate()
                inparam.Creation_Date = DateValue(IIf(IsDBNull(GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE")), Base._open_Year_Sdt, GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE")))
                inparam.Asset_RecID = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_ID")
                inparam.YearID = Base._open_Year_ID
                inparam.Tr_M_ID = Me.xMID.Text
                Dim MxDate As Date = DateValue(Base._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam))
                If MxDate = Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If DateValue(Me.Txt_V_Date.Text) < MxDate Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   p r e v o i u s    t r a n s a c t i o n    o n   s a m e   a s s e t   d a t e d  " & MxDate.ToLongDateString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
                'Else
                '    Dim inparam As Common_Lib.RealTimeService.Param_GetAssetEnclosingTxnDate = New Common_Lib.RealTimeService.Param_GetAssetEnclosingTxnDate()
                '    inparam.Creation_Date = DateValue(IIf(IsDBNull(GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE")), Base._open_Year_Sdt, GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_CREATION_DATE")))
                '    inparam.Asset_RecID = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_ID")
                '    inparam.YearID = Base._open_Year_ID
                '    inparam.Tr_M_ID = Me.xMID.Text
                '    inparam.Year_End_Date = Base._open_Year_Edt
                '    Dim EndDates As DataTable = Base._SaleOfAsset_DBOps.Get_AssetEnclosingTxnDate(inparam)
                '    If EndDates Is Nothing Then
                '        Base.HandleDBError_OnNothingReturned()
                '        Exit Sub
                '    End If
                '    If DateValue(Me.Txt_V_Date.Text) < DateValue(EndDates.Rows(0)(0)) Then
                '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                '        Me.ToolTip1.Show("V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   p r e v o i u s    t r a n s a c t i o n    o n   s a m e   a s s e t    d a t e d   " & DateValue(EndDates.Rows(0)(0)).ToLongDateString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                '        Me.Txt_V_Date.Focus()
                '        Me.DialogResult = Windows.Forms.DialogResult.None
                '        Exit Sub
                '    Else
                '        Me.ToolTip1.Hide(Me.Txt_V_Date)
                '    End If

                '    If DateValue(Me.Txt_V_Date.Text) > DateValue(EndDates.Rows(0)(1)) Then
                '        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                '        Me.ToolTip1.Show("V o u c h e r   D a t e   c a n n o t   b e   g r e a t e r   t h a n   n e x t   t r a n s a c t i o n    o n   s a m e   a s s e t  d a t e d  " & DateValue(EndDates.Rows(0)(1)).ToLongDateString() & "  . . .  !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                '        Me.Txt_V_Date.Focus()
                '        Me.DialogResult = Windows.Forms.DialogResult.None
                '        Exit Sub
                '    Else
                '        Me.ToolTip1.Hide(Me.Txt_V_Date)
                '    End If
            End If

        End If

        '-------------------------// Start Dependencies // ------------------------
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                'Bug #5697
                If Not IsDBNull(Info_MaxEditedOn) Then
                    If Info_MaxEditedOn <> DateTime.MinValue Then 'Record has been opened on basis of this being a last edited record for referred asset
                        Dim Lastest_MaxEdit_On As Object = Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(GLookUp_AssetList.Tag)
                        If IsDate(Lastest_MaxEdit_On) Then
                            If Lastest_MaxEdit_On > Info_MaxEditedOn Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CustomChanges("Sorry ! Current Voucher is no Longer Latest Edited Voucher referring the Current Asset. Another Record has been Added/Edited in background which refers the same Asset.", "Last Edited Reference Entry"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                Dim OldEditOn, NewEditOn As DateTime
                If GLookUp_BankList.Tag.ToString.Length > 0 Then
                    Dim d1 As DataTable = Base._SaleOfAsset_DBOps.GetBankAccounts(GLookUp_BankList.Tag)
                    If d1 Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    OldEditOn = GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "REC_EDIT_ON")
                    If d1.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = d1.Rows(0)("REC_EDIT_ON")
                        If NewEditOn <> OldEditOn Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank Account"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If
                If GLookUp_PartyList1.Tag.ToString.Length > 0 Then
                    Dim add_book As DataTable = Base._SaleOfAsset_DBOps.GetParties(GLookUp_PartyList1.Tag)
                    If add_book Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    OldEditOn = GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
                    If add_book.Rows.Count <= 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    Else
                        NewEditOn = add_book.Rows(0)("REC_EDIT_ON")
                        If NewEditOn <> OldEditOn Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

                'Code to Check Dependencies of Sale Qty for multi user 
                Dim cnt2 As Integer
                Dim AssetParam As Common_Lib.RealTimeService.Param_GetAssetListingForSale = New Common_Lib.RealTimeService.Param_GetAssetListingForSale()
                AssetParam.Next_YearID = Base._next_Unaudited_YearID : AssetParam.Prev_YearId = Base._prev_Unaudited_YearID
                If Cmb_Asset_Type.SelectedIndex = 0 Then 'Gold
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD
                ElseIf Cmb_Asset_Type.SelectedIndex = 1 Then 'Silver
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER
                ElseIf Cmb_Asset_Type.SelectedIndex = 2 Then 'VEHICLES
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES
                ElseIf Cmb_Asset_Type.SelectedIndex = 3 Then 'LIVESTOCK
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK
                ElseIf Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS
                ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then 'LAND & BUILDING
                    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING
                End If
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then AssetParam.TR_M_ID = xMID.Text
                AssetParam.Asset_RecID = GLookUp_AssetList.Tag
                Dim ASSET_TABLE As DataTable = Base._SaleOfAsset_DBOps.GetAllProfile_AssetList(AssetParam) 'Fetch asset data as per selection
                If ASSET_TABLE Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                cnt2 = ASSET_TABLE.Rows.Count
                OldEditOn = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REC_EDIT_ON")
                If cnt2 <= 0 Then ' if the user - selected asset is not qualified for sale anymore ,as the same has been changed by other user
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Deleted / Not Available for sale/transfer any more!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                Else
                    NewEditOn = ASSET_TABLE.Rows(0)("REC_EDIT_ON")
                    If OldEditOn <> NewEditOn Then 'A/E,E/E
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
                '' #Ref E42
                'If isProperty Then
                '    Dim d1 As DataTable = Base._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_AssetTransfer, ASSET_TABLE.Rows(0)("REF_ID"))
                '    If Not d1 Is Nothing Then
                '        If d1.Rows.Count > 0 Then
                '            For Each row As DataRow In d1.Rows
                '                Dim AssetCnt As Integer = 0
                '                AssetCnt = Base._AssetLocDBOps.GetLocationCountInGS(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                AssetCnt += Base._AssetLocDBOps.GetLocationCountInConsumables(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                AssetCnt += Base._AssetLocDBOps.GetLocationCountInLiveStock(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                AssetCnt += Base._AssetLocDBOps.GetLocationCountInTelephones(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                AssetCnt += Base._AssetLocDBOps.GetLocationCountInVehicles(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                AssetCnt += Base._AssetLocDBOps.GetLocationCountInAssets(ClientScreen.Accounts_Voucher_AssetTransfer, row("AL_ID"))
                '                If AssetCnt > 0 Then
                '                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Property"), "Referred Record has some assets mapped to it!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                '                    FormClosingEnable = False : Me.Close()
                '                    Exit Sub
                '                End If
                '            Next
                '        End If
                '    End If
                'End If

                'Code Commented Due to Shifting of Logic To SPs
                'Dim _TR_TABLE As DataTable = Nothing 'Sale Entries 
                'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                '    If Base._prev_Unaudited_YearID.Length > 0 Then
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._prev_Unaudited_YearID, xMID.Text)
                '    Else
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._open_Year_ID, xMID.Text, Base._next_Unaudited_YearID)
                '    End If
                'Else
                '    If Base._prev_Unaudited_YearID.Length > 0 Then
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._prev_Unaudited_YearID)
                '    Else
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._open_Year_ID, Nothing, Base._next_Unaudited_YearID)
                '    End If
                'End If
                'If _TR_TABLE Is Nothing Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                'Dim JointData As DataSet = New DataSet() 'Fetch and assign sold qty for each asset
                'JointData.Tables.Add(ASSET_TABLE.Copy)
                'JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointData.Relations.Add("Received", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
                'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation) : XRow("SALE_QTY") = _Row("SALE_QTY") : Next : Next

                ''Transfers
                'Dim Transfers As DataTable = Nothing
                'If Base._prev_Unaudited_YearID.Length > 0 Then
                '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._prev_Unaudited_YearID)
                'Else
                '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._open_Year_ID)
                'End If
                'JointData.Tables.Add(Transfers.Copy) : Dim Transfer_Relation As DataRelation = JointData.Relations.Add("Transfer", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("TRANSACTION_INFO").Columns("Reference"), False)
                'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Transfer_Relation)
                '        XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
                '    Next : Next

                ''Clear Relations
                'JointData.Relations.Clear()

                ''j.v.adjustment 
                'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
                '    'DEBIT
                '    If Cmb_Asset_Type.SelectedIndex = 4 Then
                '        If Base._prev_Unaudited_YearID.Length > 0 Then
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "asset_info"))
                '        Else
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "asset_info"))
                '        End If
                '    End If
                '    If Cmb_Asset_Type.SelectedIndex <> 4 Then
                '        If Base._prev_Unaudited_YearID.Length > 0 Then
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "gold_silver_info"))
                '        Else
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "gold_silver_info"))
                '        End If
                '    End If

                '    'Fetch JV additions if GS/Asset item  and add in org qty
                '    Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
                '    For Each XRow In JointData.Tables(0).Rows
                '        For Each _Row In XRow.GetChildRows(JV_Relation)
                '            XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
                '        Next
                '    Next

                '    'CREDIT
                '    If Cmb_Asset_Type.SelectedIndex = 4 Then
                '        If Base._prev_Unaudited_YearID.Length > 0 Then
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "asset_info"))
                '        Else
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "asset_info", Base._next_Unaudited_YearID))
                '        End If
                '    End If
                '    If Cmb_Asset_Type.SelectedIndex <> 4 Then
                '        If Base._prev_Unaudited_YearID.Length > 0 Then
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "gold_silver_info"))
                '        Else
                '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "gold_silver_info", Base._next_Unaudited_YearID))
                '        End If
                '    End If

                '    'Fetch JV deductions if GS/Asset item, and remove from org qty
                '    Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
                '    For Each XRow In JointData.Tables(0).Rows
                '        For Each _Row In XRow.GetChildRows(JV_CR_Relation)
                '            XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
                '        Next
                '    Next
                'End If

                ''Clear Relations
                'JointData.Relations.Clear()

                ''Changes for Year Ending Process
                'If Base._prev_Unaudited_YearID.Length > 0 Then

                '    JointData.Tables.Remove("Transaction_D_Payment_Info")
                '    JointData.Tables.Remove("TRANSACTION_INFO")

                '    'Transaction Table...
                '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._open_Year_ID, xMID.Text)
                '    Else
                '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._open_Year_ID, xMID.Text)
                '    End If

                '    JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointData.Relations.Add("Received", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
                '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation1) : XRow("SALE_QTY") = XRow("SALE_QTY") + _Row("SALE_QTY") : Next : Next

                '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._open_Year_ID)
                '    JointData.Tables.Add(Transfers.Copy) : Dim Transfer_Relation1 As DataRelation = JointData.Relations.Add("Transfer", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("TRANSACTION_INFO").Columns("Reference"), False)
                '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Transfer_Relation1)
                '            XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
                '        Next : Next

                '    'j.v.adjustment 
                '    If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
                '        JointData.Tables.Remove("DEBIT")
                '        JointData.Tables.Remove("CREDIT")
                '        'DEBIT
                '        If Cmb_Asset_Type.SelectedIndex = 4 Then
                '            If Base._prev_Unaudited_YearID.Length > 0 Then
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "asset_info"))
                '            Else
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "asset_info"))
                '            End If
                '        End If

                '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
                '            If Base._prev_Unaudited_YearID.Length > 0 Then
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "gold_silver_info"))
                '            Else
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "gold_silver_info"))
                '            End If
                '        End If

                '        Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
                '        For Each XRow In JointData.Tables(0).Rows
                '            For Each _Row In XRow.GetChildRows(JV_Relation)
                '                XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
                '            Next
                '        Next

                '        'CREDIT
                '        If Cmb_Asset_Type.SelectedIndex = 4 Then
                '            If Base._prev_Unaudited_YearID.Length > 0 Then
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "asset_info"))
                '            Else
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "asset_info"))
                '            End If

                '        End If
                '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
                '            If Base._prev_Unaudited_YearID.Length > 0 Then
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "gold_silver_info"))
                '            Else
                '                JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "gold_silver_info"))
                '            End If
                '        End If

                '        Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
                '        For Each XRow In JointData.Tables(0).Rows
                '            For Each _Row In XRow.GetChildRows(JV_CR_Relation)
                '                XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
                '            Next
                '        Next
                '    End If
                'End If

                ''Delete Out-Standing Zero, and subtract sold qty from org qty adjusted by JV effects......................................
                'Dim AI_Temp As DataTable = New DataTable
                'With AI_Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
                'For Each XRow In JointData.Tables(0).Rows
                '    ' If XRow("REF_QTY") - XRow("SALE_QTY") <= 0 Then
                '    'xNrow = AI_Temp.NewRow : xNrow("_Rid") = JointData.Tables(0).Rows.IndexOf(XRow) : AI_Temp.Rows.Add(xNrow)
                '    ' Else
                '    XRow("REF_QTY") = XRow("REF_QTY") - XRow("SALE_QTY")
                '    ' End If
                'Next
                ''For Each XRow In AI_Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next

                If ASSET_TABLE.Rows(0)("REF_QTY") < Txt_Qty.Text Then ' if the weight/qty remaining is less then the weight/qty demanded for sale 
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset List"), "weight/qty remaining is less then the weight/qty demanded for sale !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                If Val(ASSET_TABLE.Rows(0)("REF_AMT")) <> Val(GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_AMT").ToString) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Value"), "Value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

            End If
        End If
        '--------------------------------// End Dependencies //------------------------------------------------

        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._SaleOfAsset_DBOps.GetStatus(Me.xID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        If Cmd_Mode.Text.ToUpper = "CASH" Then
            GLookUp_BankList.EditValue = "" : Me.GLookUp_BankList.Tag = ""
            GLookUp_RefBankList.EditValue = "" : Me.GLookUp_RefBankList.Tag = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
        End If
        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted


        '+----JV LEDGER DETAIL----+
        Dim JV_TRANS_TYPE As String = "" : Dim JV_Cr_Led_id As String = "" : Dim JV_Dr_Led_id As String = ""
        If Val(Txt_Diff.Text) > 0 Then
            If iOffSet_ID.ToString.Length > 0 Then
                Dim JV_DT As DataTable = Base._SaleOfAsset_DBOps.GetItemsListByID(iOffSet_ID)
                If JV_DT Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If JV_DT.Rows.Count > 0 Then
                    JV_TRANS_TYPE = JV_DT.Rows(0)("Item_Trans_Type")
                    If JV_DT.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                        JV_Dr_Led_id = JV_DT.Rows(0)("ITEM_LED_ID")
                    Else
                        JV_Cr_Led_id = JV_DT.Rows(0)("ITEM_LED_ID")
                    End If
                Else
                    DevExpress.XtraEditors.XtraMessageBox.Show("Sale Amount Receivable Item Not Found..!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            Else
                DevExpress.XtraEditors.XtraMessageBox.Show("Sale Amount Receivable Item Not Define..!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If
        '+----------END-----------+

        Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
        Dim Sub_Dr_Led_ID As String = "" : Dim Sub_Cr_Led_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                If iAsset_Type = "ASSET" Then Dr_Led_id = iLed_ID Else Dr_Led_id = iCon_Led_ID
            Else
                Dr_Led_id = iLed_ID
            End If
            If Cmd_Mode.Text.ToUpper = "CASH" Then
                Cr_Led_id = "00080" 'Cash A/c.
            Else
                Cr_Led_id = "00079" 'Bank A/c.
                Sub_Cr_Led_ID = Me.GLookUp_BankList.Tag
            End If
        Else
            If Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                If iAsset_Type = "ASSET" Then Cr_Led_id = iLed_ID Else Cr_Led_id = iCon_Led_ID
            Else
                Cr_Led_id = iLed_ID
            End If
            If Cmd_Mode.Text.ToUpper = "CASH" Then
                Dr_Led_id = "00080" 'Cash A/c.
            Else
                Dr_Led_id = "00079" 'Bank A/c.
                Sub_Dr_Led_ID = Me.GLookUp_BankList.Tag
            End If
        End If

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherSaleOfAsset
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Me.xID.Text = System.Guid.NewGuid().ToString()

            Dim STR2 As String = "" : Dim XCASH As Double = 0.0 : Dim XBANK As Double = 0.0
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Then
                XCASH = Val(Me.Txt_Amount.Text)
            Else
                XBANK = Val(Me.Txt_Amount.Text)
            End If
            'MASTER ENTRY
            Dim InMinfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherSaleOfAsset()
            InMinfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
            InMinfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMinfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMinfo.TDate = Txt_V_Date.Text
            'InMinfo.TDate = Me.Txt_V_Date.Text
            InMinfo.PartyID = Me.GLookUp_PartyList1.Tag
            InMinfo.SubTotal = Val(Me.Txt_Amount.Text)
            InMinfo.Cash = XCASH
            InMinfo.Bank = XBANK
            InMinfo.Advance = 0
            InMinfo.Liability = 0
            InMinfo.Credit = 0
            InMinfo.TDS = 0
            InMinfo.SaleAmt = Val(Me.Txt_SaleAmt.Text)
            InMinfo.SaleQty = Val(Me.Txt_Qty.Text)
            If IsDate(Txt_SDate.Text) Then InMinfo.SaleDate = Convert.ToDateTime(Txt_SDate.Text).ToString(Base._Server_Date_Format_Short) Else InMinfo.SaleDate = Txt_SDate.Text
            'InMinfo.SaleDate = Me.Txt_SDate.Text
            InMinfo.SaleType = Me.Cmb_Asset_Type.Text
            InMinfo.Status_Action = Status_Action
            InMinfo.RecID = Me.xMID.Text

            'If Not Base._SaleOfAsset_DBOps.InsertMasterInfo(InMinfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMasterEntry = InMinfo

            'Profit/Loss Calculation
            Dim BalanceQty As Decimal = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_QTY")
            Dim BalanceCost As Decimal = ((GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_AMT") * Val(Txt_Qty.Text)) / BalanceQty)
            If BalanceQty <> Val(Txt_Qty.Text) Then ' if this is not final sale 
                BalanceCost = Math.Round(BalanceCost, 0) 'Bug #5098
            End If
            Dim DiffOfSale As Decimal = Val(Txt_SaleAmt.Text) - BalanceCost
            If Cmb_Asset_Type.SelectedIndex = 4 Then
                If iAsset_Type <> "ASSET" Then DiffOfSale = 0 ' Insurance Assets wont generate profit/loss
            End If

            Dim Receipt_Unposted As Decimal = Val(Txt_Amount.Text)
            Dim Vehicle_Unposted As Decimal = BalanceCost
            Dim TxnAmount As Decimal = 0
            If Val(Me.Txt_Amount.Text) < BalanceCost Then
                TxnAmount = Val(Me.Txt_Amount.Text)
            Else
                TxnAmount = BalanceCost
            End If
            If Cmb_Asset_Type.SelectedIndex = 4 Then
                If iAsset_Type <> "ASSET" Then
                    TxnAmount = Val(Me.Txt_Amount.Text)
                    Vehicle_Unposted = Val(Txt_SaleAmt.Text)
                End If
            End If

            'Transaction info
            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
            InParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
            'InParam.TDate = Me.Txt_V_Date.Text
            InParam.ItemID = Me.GLookUp_ItemList.Tag
            InParam.Type = iTrans_Type
            InParam.Cr_Led_ID = Cr_Led_id
            InParam.Dr_Led_ID = Dr_Led_id
            InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            InParam.Mode = Me.Cmd_Mode.Text
            InParam.Ref_Bank = Me.GLookUp_RefBankList.Tag
            InParam.Ref_Branch = Me.Txt_Ref_Branch.Text
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefDate = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefCDate = Txt_Ref_CDate.Text
            'InParam.RefDate = Me.Txt_Ref_Date.Text
            'InParam.RefCDate = Me.Txt_Ref_CDate.Text
            InParam.Amount = TxnAmount
            InParam.PartyID = Me.GLookUp_PartyList1.Tag
            InParam.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.Tr_M_ID = Me.xMID.Text
            InParam.TxnSrNo = 1
            InParam.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID.Text

            'If Not Base._SaleOfAsset_DBOps.Insert(InParam) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            InNewParam.param_InsertTransactionInfo = InParam
            Vehicle_Unposted = Vehicle_Unposted - InParam.Amount
            Receipt_Unposted = Receipt_Unposted - InParam.Amount

            'PAYMENT  
            Dim InPay As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset()
            InPay.TxnMID = Me.xMID.Text
            InPay.Type = "SALE"
            InPay.SrNo = "1"
            InPay.RefID = Me.GLookUp_AssetList.Tag
            InPay.RefAmount = Val(Me.Txt_Amount.Text)
            InPay.Status_Action = Status_Action
            InPay.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._SaleOfAsset_DBOps.InsertPayment(InPay) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            InNewParam.param_InsertAandLPayment = InPay


            'JV ENTRY IF difference found
            If iOffSet_ID.ToString.Length > 0 And Val(Txt_Diff.Text) > 0 Then
                'CREDIT ENTRY
                If (Vehicle_Unposted > 0 And Receipt_Unposted = 0) Or DiffOfSale < 0 Then
                    Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                    InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                    InParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Txt_V_Date.Text
                    'InParams.TDate = Me.Txt_V_Date.Text
                    InParams.ItemID = Me.GLookUp_ItemList.Tag
                    InParams.Type = iTrans_Type
                    InParams.Cr_Led_ID = Cr_Led_id
                    InParams.Dr_Led_ID = ""
                    InParams.Sub_Cr_Led_ID = ""
                    InParams.Sub_Dr_Led_ID = ""
                    InParams.Mode = Me.Cmd_Mode.Text
                    InParams.Ref_Bank = ""
                    InParams.Ref_Branch = ""
                    InParams.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InParams.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.RefDate = Txt_Ref_Date.Text
                    'InParams.RefDate = Me.Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InParams.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParams.RefCDate = Txt_Ref_CDate.Text
                    'InParams.RefCDate = Me.Txt_Ref_CDate.Text
                    If DiffOfSale > 0 Then
                        InParams.Amount = Vehicle_Unposted
                    Else
                        InParams.Amount = Val(Me.Txt_Diff.Text) 'Credit Vehicle by Advance Amount
                    End If
                    InParams.PartyID = Me.GLookUp_PartyList1.Tag
                    InParams.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                    InParams.Remarks = Me.Txt_Remarks.Text
                    InParams.Reference = Me.Txt_Reference.Text
                    InParams.Tr_M_ID = Me.xMID.Text
                    InParams.TxnSrNo = 1
                    InParams.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                    InParams.Status_Action = Status_Action
                    InParams.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._SaleOfAsset_DBOps.Insert(InParams) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                    '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InNewParam.param_InsertCreditJV = InParams
                    Vehicle_Unposted = Vehicle_Unposted - InParams.Amount
                End If

                'DEBIT ENTRY
                Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                InParam1.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                'InParam1.TDate = Me.Txt_V_Date.Text
                InParam1.ItemID = iOffSet_ID
                InParam1.Type = JV_TRANS_TYPE
                InParam1.Cr_Led_ID = ""
                InParam1.Dr_Led_ID = JV_Dr_Led_id
                InParam1.Sub_Cr_Led_ID = ""
                InParam1.Sub_Dr_Led_ID = ""
                InParam1.Mode = Me.Cmd_Mode.Text
                InParam1.Ref_Bank = ""
                InParam1.Ref_Branch = ""
                InParam1.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InParam1.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefDate = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InParam1.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.RefCDate = Txt_Ref_CDate.Text
                'InParam1.RefDate = Me.Txt_Ref_Date.Text
                'InParam1.RefCDate = Me.Txt_Ref_CDate.Text
                InParam1.Amount = Val(Me.Txt_Diff.Text)
                InParam1.PartyID = Me.GLookUp_PartyList1.Tag
                InParam1.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                InParam1.Remarks = Me.Txt_Remarks.Text
                InParam1.Reference = Me.Txt_Reference.Text
                InParam1.Tr_M_ID = Me.xMID.Text
                InParam1.TxnSrNo = 2
                InParam1.Cross_Ref_ID = "" 'Bug #4083  Me.GLookUp_AssetList.Tag
                InParam1.Status_Action = Status_Action
                InParam1.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._SaleOfAsset_DBOps.Insert(InParam1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertDebitJV = InParam1

                'PAYMENT - ADJUSTMENT
                Dim InPmt As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset()
                InPmt.TxnMID = Me.xMID.Text
                InPmt.Type = "SALE-RECEIVABLE"
                InPmt.SrNo = "2"
                InPmt.RefID = Me.GLookUp_AssetList.Tag
                InPmt.RefAmount = Val(Me.Txt_Diff.Text)
                InPmt.Status_Action = Status_Action
                InPmt.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._SaleOfAsset_DBOps.InsertPayment(InPmt) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertPaymentAdjustment = InPmt

                'Profile - Advances
                Dim InPara As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances = New Common_Lib.RealTimeService.Parameter_InsertTRID_Advances()
                InPara.ItemID = iOffSet_ID
                InPara.PartyID = Me.GLookUp_PartyList1.Tag
                If IsDate(Txt_V_Date.Text) Then InPara.AdvanceDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InPara.AdvanceDate = Txt_V_Date.Text
                'InPara.AdvanceDate = Me.Txt_V_Date.Text
                InPara.Amount = Val(Me.Txt_Diff.Text)
                InPara.Purpose = Me.Txt_Narration.Text
                InPara.Remarks = "Sale " & Me.GLookUp_AssetList.Text & ": " & Me.Txt_Desc.Text & ", Sale Date: " & Txt_SDate.Text
                InPara.TxnID = Me.xMID.Text
                InPara.Status_Action = Status_Action
                InPara.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset

                'If Not Base._AdvanceDBOps.Insert(InPara) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertProfileAdvances = InPara

                'purpose
                Dim InPurpose_JV As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
                InPurpose_JV.TxnID = Me.xMID.Text
                InPurpose_JV.PurposeID = Me.GLookUp_PurList.Tag
                InPurpose_JV.Amount = Val(Me.Txt_Diff.Text)
                InPurpose_JV.Status_Action = Status_Action
                InPurpose_JV.RecID = System.Guid.NewGuid().ToString()
                InPurpose_JV.SrNo = 2

                'If Not Base._SaleOfAsset_DBOps.InsertPurpose(InPurpose_JV) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertPurposeJV = InPurpose_JV
            End If

            'Profit/Loss Posting , if Sale Amount <> Balance Cost(Sold Qty)

            If DiffOfSale <> 0 Then 'Profit if +ve , else Loss

                'Debit jv for Profit/Loss Ledger
                Dim PL_Unposted As Decimal = DiffOfSale
                Dim InProfit_Loss As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                InProfit_Loss.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                InProfit_Loss.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InProfit_Loss.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InProfit_Loss.TDate = Txt_V_Date.Text
                If DiffOfSale > 0 Then ' Profit
                    InProfit_Loss.ItemID = SaleProfit_ItemID
                    InProfit_Loss.Type = "CREDIT"
                    InProfit_Loss.Cr_Led_ID = SaleProfit_Loss_LedID
                    If DiffOfSale >= (Receipt_Unposted) Then
                        InProfit_Loss.Amount = Receipt_Unposted
                        InProfit_Loss.Dr_Led_ID = Dr_Led_id
                        InProfit_Loss.Sub_Dr_Led_ID = Sub_Dr_Led_ID
                    Else    'Receipt has already been settled, as Receipt Left + cost <= Total Sale Amount
                        InProfit_Loss.Amount = DiffOfSale
                        InProfit_Loss.Dr_Led_ID = ""
                        InProfit_Loss.Sub_Dr_Led_ID = "" 'Fix #5097
                    End If
                Else 'Loss
                    InProfit_Loss.ItemID = SaleLoss_ItemID
                    InProfit_Loss.Type = "DEBIT"
                    InProfit_Loss.Cr_Led_ID = ""
                    InProfit_Loss.Dr_Led_ID = SaleProfit_Loss_LedID
                    InProfit_Loss.Amount = -1 * DiffOfSale
                    InProfit_Loss.Sub_Dr_Led_ID = "" 'Fix #5122
                End If
                InProfit_Loss.Sub_Cr_Led_ID = ""
                InProfit_Loss.Mode = Me.Cmd_Mode.Text
                InProfit_Loss.Ref_Bank = ""
                InProfit_Loss.Ref_Branch = ""
                InProfit_Loss.Ref_No = ""
                InProfit_Loss.PartyID = Me.GLookUp_PartyList1.Tag
                InProfit_Loss.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                InProfit_Loss.Remarks = Me.Txt_Remarks.Text
                InProfit_Loss.Reference = Me.Txt_Reference.Text
                InProfit_Loss.Tr_M_ID = Me.xMID.Text
                InProfit_Loss.TxnSrNo = 3
                InProfit_Loss.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                InProfit_Loss.Status_Action = Status_Action
                InProfit_Loss.RecID = System.Guid.NewGuid().ToString()
                InNewParam.param_InsertPL = InProfit_Loss
                PL_Unposted = PL_Unposted - InProfit_Loss.Amount

                If PL_Unposted > 0 Or DiffOfSale < 0 Then
                    Dim InProfit_Loss_2 As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                    InProfit_Loss_2.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                    InProfit_Loss_2.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InProfit_Loss_2.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InProfit_Loss_2.TDate = Txt_V_Date.Text
                    InProfit_Loss_2.Cr_Led_ID = SaleProfit_Loss_LedID
                    If DiffOfSale > 0 Then ' Profit
                        InProfit_Loss_2.ItemID = SaleProfit_ItemID
                        InProfit_Loss_2.Type = "CREDIT"
                        InProfit_Loss_2.Cr_Led_ID = SaleProfit_Loss_LedID
                        InProfit_Loss_2.Amount = PL_Unposted
                        InProfit_Loss_2.Dr_Led_ID = ""
                    Else 'Loss
                        InProfit_Loss_2.ItemID = Me.GLookUp_ItemList.Tag
                        InProfit_Loss_2.Type = "CREDIT"
                        InProfit_Loss_2.Cr_Led_ID = Cr_Led_id
                        InProfit_Loss_2.Dr_Led_ID = ""
                        InProfit_Loss_2.Amount = -1 * DiffOfSale
                    End If
                    InProfit_Loss_2.Sub_Cr_Led_ID = ""
                    InProfit_Loss_2.Sub_Dr_Led_ID = ""
                    InProfit_Loss_2.Mode = Me.Cmd_Mode.Text
                    InProfit_Loss_2.Ref_Bank = ""
                    InProfit_Loss_2.Ref_Branch = ""
                    InProfit_Loss_2.Ref_No = ""
                    InProfit_Loss_2.PartyID = Me.GLookUp_PartyList1.Tag
                    InProfit_Loss_2.Narration = Me.Txt_Narration.Text
                    InProfit_Loss_2.Remarks = Me.Txt_Remarks.Text
                    InProfit_Loss_2.Reference = Me.Txt_Reference.Text
                    InProfit_Loss_2.Tr_M_ID = Me.xMID.Text
                    InProfit_Loss_2.TxnSrNo = 3
                    InProfit_Loss_2.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                    InProfit_Loss_2.Status_Action = Status_Action
                    InProfit_Loss_2.RecID = System.Guid.NewGuid().ToString()
                    InNewParam.param_InsertPL_2 = InProfit_Loss_2
                End If

                'purpose
                Dim InPurpose_PL As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
                InPurpose_PL.TxnID = Me.xMID.Text
                InPurpose_PL.PurposeID = Me.GLookUp_PurList.Tag
                If DiffOfSale > 0 Then InPurpose_PL.Amount = DiffOfSale Else InPurpose_PL.Amount = -1 * DiffOfSale
                InPurpose_PL.Status_Action = Status_Action
                InPurpose_PL.RecID = System.Guid.NewGuid().ToString()
                InPurpose_PL.SrNo = 3
                InNewParam.param_InsertPurposePL = InPurpose_PL
            End If
            'End If

            'purpose
            Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
            InPurpose.TxnID = Me.xMID.Text
            InPurpose.PurposeID = Me.GLookUp_PurList.Tag
            InPurpose.Amount = Val(Me.Txt_Amount.Text)
            InPurpose.Status_Action = Status_Action
            InPurpose.RecID = System.Guid.NewGuid().ToString()
            InPurpose.SrNo = 1

            'If Not Base._SaleOfAsset_DBOps.InsertPurpose(InPurpose) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            InNewParam.param_InsertPurpose = InPurpose

            If Not Base._SaleOfAsset_DBOps.InsertSaleOfAsset_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim xSaved As New Common_Lib.Prompt_Window
            xSaved.ShowDialog(Me.TitleX.Text, Common_Lib.Messages.SaveSuccess, Common_Lib.Prompt_Window.ButtonType._Exclamation, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue)

            'DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherSaleOfAsset
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            Dim STR2 As String = "" : Dim XCASH As Double = 0.0 : Dim XBANK As Double = 0.0
            If Me.Cmd_Mode.Text.ToUpper = "CASH" Then
                XCASH = Val(Me.Txt_Amount.Text)
            Else
                XBANK = Val(Me.Txt_Amount.Text)
            End If

            'MASTER ENTRY
            Dim UpMaster As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherSaleOfAsset()
            UpMaster.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMaster.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.TDate = Txt_V_Date.Text
            'UpMaster.TDate = Me.Txt_V_Date.Text
            UpMaster.PartyID = Me.GLookUp_PartyList1.Tag
            UpMaster.SubTotal = Val(Me.Txt_Amount.Text)
            UpMaster.Cash = XCASH
            UpMaster.Bank = XBANK
            UpMaster.SaleAmt = Val(Me.Txt_SaleAmt.Text)
            UpMaster.SaleQty = Val(Me.Txt_Qty.Text)
            If IsDate(Txt_SDate.Text) Then UpMaster.SaleDate = Convert.ToDateTime(Txt_SDate.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.SaleDate = Txt_SDate.Text
            'UpMaster.SaleDate = Me.Txt_SDate.Text
            UpMaster.SaleType = Me.Cmb_Asset_Type.Text
            ''UpMaster.Status_Action = Status_Action
            UpMaster.RecID = Me.xMID.Text

            'If Not Base._SaleOfAsset_DBOps.UpdateMaster(UpMaster) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMaster

            'If Not Base._SaleOfAsset_DBOps.DeleteItems(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.Mid_DeleteItems = Me.xMID.Text

            'If Not Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.Mid_DeletePayment = Me.xMID.Text

            'If Not Base._SaleOfAsset_DBOps.DeletePurpose(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.Mid_DeletePurpose = Me.xMID.Text

            'If Not Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.Mid_Delete = Me.xMID.Text

            'PROFILE
            'If Not Base._AdvanceDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.Mid_DeleteAdvances = Me.xMID.Text

            'Profit/Loss Calculation
            Dim BalanceQty As Decimal = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_QTY")
            Dim BalanceCost As Decimal = ((GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_AMT") * Val(Txt_Qty.Text)) / BalanceQty)
            If BalanceQty <> Val(Txt_Qty.Text) Then ' if this is not final sale 
                BalanceCost = Math.Round(BalanceCost, 0) 'Bug #5098
            End If
            Dim DiffOfSale As Decimal = Val(Txt_SaleAmt.Text) - BalanceCost
            If Cmb_Asset_Type.SelectedIndex = 4 Then
                If iAsset_Type <> "ASSET" Then DiffOfSale = 0 ' Insurance Assets wont generate profit/loss
            End If

            Dim Receipt_Unposted As Decimal = Val(Txt_Amount.Text)
            Dim Vehicle_Unposted As Decimal = BalanceCost
            Dim TxnAmount As Decimal = 0
            If Val(Me.Txt_Amount.Text) < BalanceCost Then
                TxnAmount = Val(Me.Txt_Amount.Text)
            Else
                TxnAmount = BalanceCost
            End If
            If Cmb_Asset_Type.SelectedIndex = 4 Then
                If iAsset_Type <> "ASSET" Then
                    TxnAmount = Val(Me.Txt_Amount.Text)
                    Vehicle_Unposted = Val(Txt_SaleAmt.Text)
                End If
            End If

            'Transaction info 1: 
            'Cash / Bank A/c Dr         Amt Received
            '               To Vehicles                 Amt Received
            Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
            InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
            InParam.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
            'InParam.TDate = Me.Txt_V_Date.Text
            InParam.ItemID = Me.GLookUp_ItemList.Tag
            InParam.Type = iTrans_Type
            InParam.Cr_Led_ID = Cr_Led_id
            InParam.Dr_Led_ID = Dr_Led_id
            InParam.Sub_Cr_Led_ID = Sub_Cr_Led_ID
            InParam.Sub_Dr_Led_ID = Sub_Dr_Led_ID
            InParam.Mode = Me.Cmd_Mode.Text
            InParam.Ref_Bank = Me.GLookUp_RefBankList.Tag
            InParam.Ref_Branch = Me.Txt_Ref_Branch.Text
            InParam.Ref_No = Me.Txt_Ref_No.Text
            If IsDate(Txt_Ref_Date.Text) Then InParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefDate = Txt_Ref_Date.Text
            If IsDate(Txt_Ref_CDate.Text) Then InParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InParam.RefCDate = Txt_Ref_CDate.Text
            'InParam.RefDate = Me.Txt_Ref_Date.Text
            'InParam.RefCDate = Me.Txt_Ref_CDate.Text
            InParam.Amount = TxnAmount
            InParam.PartyID = Me.GLookUp_PartyList1.Tag
            InParam.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
            InParam.Remarks = Me.Txt_Remarks.Text
            InParam.Reference = Me.Txt_Reference.Text
            InParam.Tr_M_ID = Me.xMID.Text
            InParam.TxnSrNo = 1
            InParam.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
            InParam.Status_Action = Status_Action
            InParam.RecID = Me.xID.Text

            'If Not Base._SaleOfAsset_DBOps.Insert(InParam) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            EditParam.param_InsertTransactionInfo = InParam
            Vehicle_Unposted = Vehicle_Unposted - InParam.Amount
            Receipt_Unposted = Receipt_Unposted - InParam.Amount

            'PAYMENT  
            Dim InPay As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset()

            InPay.TxnMID = Me.xMID.Text
            InPay.Type = "SALE"
            InPay.SrNo = "1"
            InPay.RefID = Me.GLookUp_AssetList.Tag
            InPay.RefAmount = Val(Me.Txt_Amount.Text)
            InPay.Status_Action = Status_Action
            InPay.RecID = System.Guid.NewGuid().ToString()

            'If Not Base._SaleOfAsset_DBOps.InsertPayment(InPay) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            EditParam.param_InsertAandLPayment = InPay


            'Transaction Info 2
            'Advance --         Diff of Sale and Recd
            '       To Vehicle 39860                Diff of Sale and Recd
            If iOffSet_ID.ToString.Length > 0 And Val(Txt_Diff.Text) > 0 Then
                'CREDIT ENTRY
                If (Vehicle_Unposted > 0 And Receipt_Unposted = 0) Or DiffOfSale < 0 Then
                    Dim InsParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                    InsParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                    InsParams.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InsParams.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsParams.TDate = Txt_V_Date.Text
                    'InsParams.TDate = Me.Txt_V_Date.Text
                    InsParams.ItemID = Me.GLookUp_ItemList.Tag
                    InsParams.Type = iTrans_Type
                    InsParams.Cr_Led_ID = Cr_Led_id
                    InsParams.Dr_Led_ID = ""
                    InsParams.Sub_Cr_Led_ID = ""
                    InsParams.Sub_Dr_Led_ID = ""
                    InsParams.Mode = Me.Cmd_Mode.Text
                    InsParams.Ref_Bank = ""
                    InsParams.Ref_Branch = ""
                    InsParams.Ref_No = Me.Txt_Ref_No.Text
                    If IsDate(Txt_Ref_Date.Text) Then InsParams.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsParams.RefDate = Txt_Ref_Date.Text
                    If IsDate(Txt_Ref_CDate.Text) Then InsParams.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InsParams.RefCDate = Txt_Ref_CDate.Text
                    'InsParams.RefDate = Me.Txt_Ref_Date.Text
                    'InsParams.RefCDate = Me.Txt_Ref_CDate.Text
                    If DiffOfSale > 0 Then
                        InsParams.Amount = Vehicle_Unposted
                    Else
                        InsParams.Amount = Val(Me.Txt_Diff.Text) 'Credit Vehicle by Advance Amount
                    End If

                    InsParams.PartyID = Me.GLookUp_PartyList1.Tag
                    InsParams.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                    InsParams.Remarks = Me.Txt_Remarks.Text
                    InsParams.Reference = Me.Txt_Reference.Text
                    InsParams.Tr_M_ID = Me.xMID.Text
                    InsParams.TxnSrNo = 1
                    InsParams.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                    InsParams.Status_Action = Status_Action
                    InsParams.RecID = System.Guid.NewGuid().ToString()

                    EditParam.param_InsertCreditJV = InsParams
                    Vehicle_Unposted = Vehicle_Unposted - InsParams.Amount
                End If

                'DEBIT ENTRY
                Dim InsParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                InsParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                InsParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InsParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsParam.TDate = Txt_V_Date.Text
                'InsParam.TDate = Me.Txt_V_Date.Text
                InsParam.ItemID = iOffSet_ID
                InsParam.Type = JV_TRANS_TYPE
                InsParam.Cr_Led_ID = ""
                InsParam.Dr_Led_ID = JV_Dr_Led_id
                InsParam.Sub_Cr_Led_ID = ""
                InsParam.Sub_Dr_Led_ID = ""
                InsParam.Mode = Me.Cmd_Mode.Text
                InsParam.Ref_Bank = ""
                InsParam.Ref_Branch = ""
                InsParam.Ref_No = Me.Txt_Ref_No.Text
                If IsDate(Txt_Ref_Date.Text) Then InsParam.RefDate = Convert.ToDateTime(Txt_Ref_Date.Text).ToString(Base._Server_Date_Format_Short) Else InsParam.RefDate = Txt_Ref_Date.Text
                If IsDate(Txt_Ref_CDate.Text) Then InsParam.RefCDate = Convert.ToDateTime(Txt_Ref_CDate.Text).ToString(Base._Server_Date_Format_Short) Else InsParam.RefCDate = Txt_Ref_CDate.Text
                'InsParam.RefDate = Me.Txt_Ref_Date.Text
                'InsParam.RefCDate = Me.Txt_Ref_CDate.Text
                InsParam.Amount = Val(Me.Txt_Diff.Text)
                InsParam.PartyID = Me.GLookUp_PartyList1.Tag
                InsParam.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                InsParam.Remarks = Me.Txt_Remarks.Text
                InsParam.Reference = Me.Txt_Reference.Text
                InsParam.Tr_M_ID = Me.xMID.Text
                InsParam.TxnSrNo = 2
                InsParam.Cross_Ref_ID = "" 'Bug #4083  Me.GLookUp_AssetList.Tag
                InsParam.Status_Action = Status_Action
                InsParam.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._SaleOfAsset_DBOps.Insert(InsParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertDebitJV = InsParam

                'PAYMENT - ADJUSTMENT
                Dim InsPmt As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherSaleOfAsset()
                InsPmt.TxnMID = Me.xMID.Text
                InsPmt.Type = "SALE-RECEIVABLE"
                InsPmt.SrNo = "2"
                InsPmt.RefID = Me.GLookUp_AssetList.Tag
                InsPmt.RefAmount = Val(Me.Txt_Diff.Text)
                InsPmt.Status_Action = Status_Action
                InsPmt.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._SaleOfAsset_DBOps.InsertPayment(InsPmt) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertPaymentAdjustment = InsPmt

                'Profile - Advances
                Dim InParam1 As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances = New Common_Lib.RealTimeService.Parameter_InsertTRID_Advances()
                InParam1.ItemID = iOffSet_ID
                InParam1.PartyID = Me.GLookUp_PartyList1.Tag
                If IsDate(Txt_V_Date.Text) Then InParam1.AdvanceDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.AdvanceDate = Txt_V_Date.Text
                'InParam1.AdvanceDate = Me.Txt_V_Date.Text
                InParam1.Amount = Val(Me.Txt_Diff.Text)
                InParam1.Purpose = Me.Txt_Narration.Text
                InParam1.Remarks = "Sale " & Me.GLookUp_AssetList.Text & ": " & Me.Txt_Desc.Text & ", Sale Date: " & Txt_SDate.Text
                InParam1.TxnID = Me.xMID.Text
                InParam1.Status_Action = Status_Action
                InParam1.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset

                'If Not Base._AdvanceDBOps.Insert(InParam1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_InsertProfileAdvances = InParam1

                'purpose
                Dim InPurp_JV As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
                InPurp_JV.TxnID = Me.xMID.Text
                InPurp_JV.PurposeID = Me.GLookUp_PurList.Tag
                InPurp_JV.Amount = Val(Me.Txt_Diff.Text)
                InPurp_JV.Status_Action = Status_Action
                InPurp_JV.RecID = System.Guid.NewGuid().ToString()
                InPurp_JV.SrNo = 2

                'If Not Base._SaleOfAsset_DBOps.InsertPurpose(InPurp_JV) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
                '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertPurposeJV = InPurp_JV
            End If

            'Profit/Loss Posting , if Sale Amount <> Balance Cost(Sold Qty)

            If DiffOfSale <> 0 Then 'Profit if +ve , else Loss
                ''Credit jv for Asset Ledger
                'Dim InProfit_Asset As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                'InProfit_Asset.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                'InProfit_Asset.VNo = Me.Txt_V_NO.Text
                'If IsDate(Txt_V_Date.Text) Then InProfit_Asset.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InProfit_Asset.TDate = Txt_V_Date.Text
                'InProfit_Asset.ItemID = Me.GLookUp_ItemList.Tag
                'InProfit_Asset.Type = iTrans_Type
                'InProfit_Asset.Cr_Led_ID = Cr_Led_id ' Asset Led Id
                'If Val(Me.Txt_Amount.Text) > BalanceCost Then
                '    InProfit_Asset.Dr_Led_ID = Dr_Led_id
                'Else
                '    InProfit_Asset.Dr_Led_ID = ""
                'End If
                'If BalanceCost > Val(Me.Txt_Amount.Text) Then
                '    InProfit_Asset.Amount = BalanceCost - Val(Me.Txt_Amount.Text)
                'Else
                '    InProfit_Asset.Amount = Val(Me.Txt_Amount.Text) - BalanceCost
                'End If
                'InProfit_Asset.Sub_Cr_Led_ID = ""
                'InProfit_Asset.Sub_Dr_Led_ID = ""
                'InProfit_Asset.Mode = Me.Cmd_Mode.Text
                'InProfit_Asset.Ref_Bank = ""
                'InProfit_Asset.Ref_Branch = ""
                'InProfit_Asset.Ref_No = ""
                'InProfit_Asset.PartyID = ""
                'InProfit_Asset.Narration = Me.Txt_Narration.Text
                'InProfit_Asset.Remarks = Me.Txt_Remarks.Text
                'InProfit_Asset.Reference = Me.Txt_Reference.Text
                'InProfit_Asset.Tr_M_ID = Me.xMID.Text
                'InProfit_Asset.TxnSrNo = 3
                'InProfit_Asset.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                'InProfit_Asset.Status_Action = Status_Action
                'InProfit_Asset.RecID = System.Guid.NewGuid().ToString()
                'EditParam.param_InsertAssetPL = InProfit_Asset

                'Debit jv for Profit/Loss Ledger
                Dim PL_Unposted As Decimal = DiffOfSale
                Dim InProfit_Loss As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                InProfit_Loss.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                InProfit_Loss.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InProfit_Loss.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InProfit_Loss.TDate = Txt_V_Date.Text
                If DiffOfSale > 0 Then ' Profit
                    InProfit_Loss.ItemID = SaleProfit_ItemID
                    InProfit_Loss.Type = "CREDIT"
                    InProfit_Loss.Cr_Led_ID = SaleProfit_Loss_LedID
                    If DiffOfSale >= (Receipt_Unposted) Then
                        InProfit_Loss.Amount = Receipt_Unposted
                        InProfit_Loss.Dr_Led_ID = Dr_Led_id
                        InProfit_Loss.Sub_Dr_Led_ID = Sub_Dr_Led_ID
                    Else    'Receipt has already been settled, as Receipt Left + cost <= Total Sale Amount
                        InProfit_Loss.Amount = DiffOfSale
                        InProfit_Loss.Dr_Led_ID = ""
                        InProfit_Loss.Sub_Dr_Led_ID = ""
                    End If
                Else 'Loss
                    InProfit_Loss.ItemID = SaleLoss_ItemID
                    InProfit_Loss.Type = "DEBIT"
                    InProfit_Loss.Cr_Led_ID = ""
                    InProfit_Loss.Dr_Led_ID = SaleProfit_Loss_LedID
                    InProfit_Loss.Amount = -1 * DiffOfSale
                    InProfit_Loss.Sub_Dr_Led_ID = ""
                End If
                InProfit_Loss.Sub_Cr_Led_ID = ""
                InProfit_Loss.Mode = Me.Cmd_Mode.Text
                InProfit_Loss.Ref_Bank = ""
                InProfit_Loss.Ref_Branch = ""
                InProfit_Loss.Ref_No = ""
                InProfit_Loss.PartyID = Me.GLookUp_PartyList1.Tag

                InProfit_Loss.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                InProfit_Loss.Remarks = Me.Txt_Remarks.Text
                InProfit_Loss.Reference = Me.Txt_Reference.Text
                InProfit_Loss.Tr_M_ID = Me.xMID.Text
                InProfit_Loss.TxnSrNo = 3
                InProfit_Loss.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                InProfit_Loss.Status_Action = Status_Action
                InProfit_Loss.RecID = System.Guid.NewGuid().ToString()
                EditParam.param_InsertPL = InProfit_Loss
                PL_Unposted = PL_Unposted - InProfit_Loss.Amount

                If PL_Unposted > 0 Or DiffOfSale < 0 Then
                    Dim InProfit_Loss_2 As Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_Insert_VoucherSaleOfAsset()
                    InProfit_Loss_2.TransCode = Common_Lib.Common.Voucher_Screen_Code.Sale_Asset
                    InProfit_Loss_2.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InProfit_Loss_2.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InProfit_Loss_2.TDate = Txt_V_Date.Text
                    InProfit_Loss_2.Cr_Led_ID = SaleProfit_Loss_LedID
                    If DiffOfSale > 0 Then ' Profit
                        InProfit_Loss_2.ItemID = SaleProfit_ItemID
                        InProfit_Loss_2.Type = "CREDIT"
                        InProfit_Loss_2.Cr_Led_ID = SaleProfit_Loss_LedID
                        InProfit_Loss_2.Amount = PL_Unposted
                        InProfit_Loss_2.Dr_Led_ID = ""
                    Else 'Loss
                        InProfit_Loss_2.ItemID = Me.GLookUp_ItemList.Tag
                        InProfit_Loss_2.Type = "CREDIT"
                        InProfit_Loss_2.Cr_Led_ID = Cr_Led_id
                        InProfit_Loss_2.Dr_Led_ID = ""
                        InProfit_Loss_2.Amount = -1 * DiffOfSale
                    End If
                    InProfit_Loss_2.Sub_Cr_Led_ID = ""
                    InProfit_Loss_2.Sub_Dr_Led_ID = ""
                    InProfit_Loss_2.Mode = Me.Cmd_Mode.Text
                    InProfit_Loss_2.Ref_Bank = ""
                    InProfit_Loss_2.Ref_Branch = ""
                    InProfit_Loss_2.Ref_No = ""
                    InProfit_Loss_2.PartyID = Me.GLookUp_PartyList1.Tag
                    InProfit_Loss_2.Narration = "Sale of " & Me.GLookUp_AssetList.Text & ". Cost = " & BalanceCost & ", Sale Amount = " & Txt_SaleAmt.Text & " and Profit (Or Loss) = " & DiffOfSale & "" 'changes as per pm.bkinfo.in/issues/5308#note-6
                    InProfit_Loss_2.Remarks = Me.Txt_Remarks.Text
                    InProfit_Loss_2.Reference = Me.Txt_Reference.Text
                    InProfit_Loss_2.Tr_M_ID = Me.xMID.Text
                    InProfit_Loss_2.TxnSrNo = 3
                    InProfit_Loss_2.Cross_Ref_ID = Me.GLookUp_AssetList.Tag
                    InProfit_Loss_2.Status_Action = Status_Action
                    InProfit_Loss_2.RecID = System.Guid.NewGuid().ToString()
                    EditParam.param_InsertPL_2 = InProfit_Loss_2
                End If

                'purpose
                Dim InPurpose_PL As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
                InPurpose_PL.TxnID = Me.xMID.Text
                InPurpose_PL.PurposeID = Me.GLookUp_PurList.Tag
                If DiffOfSale > 0 Then InPurpose_PL.Amount = DiffOfSale Else InPurpose_PL.Amount = -1 * DiffOfSale
                InPurpose_PL.Status_Action = Status_Action
                InPurpose_PL.RecID = System.Guid.NewGuid().ToString()
                InPurpose_PL.SrNo = 3
                EditParam.param_InsertPurposePL = InPurpose_PL
            End If

            'purpose
            Dim InPurp As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherSaleOfAsset()
            InPurp.TxnID = Me.xMID.Text
            InPurp.PurposeID = Me.GLookUp_PurList.Tag
            InPurp.Amount = Val(Me.Txt_Amount.Text)
            InPurp.Status_Action = Status_Action
            InPurp.RecID = System.Guid.NewGuid().ToString()
            InPurp.SrNo = 1

            'If Not Base._SaleOfAsset_DBOps.InsertPurpose(InPurp) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text)
            '    Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text)
            '    Exit Sub
            'End If
            EditParam.param_InsertPurpose = InPurp

            If Not Base._SaleOfAsset_DBOps.UpdateSaleOfAsset_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherSaleOfAsset = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherSaleOfAsset
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            If Cmb_Asset_Type.SelectedIndex = 4 Or Cmb_Asset_Type.SelectedIndex = 5 Then 'Movable Assets or Property
                If Base.IsInsuranceAudited() Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Changes cannot be done after the completion of Insurance Audit", "Information..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            DelParam.Mid_DeleteItems = Me.xMID.Text
            'If Not Base._SaleOfAsset_DBOps.DeleteItems(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            DelParam.Mid_DeletePayment = Me.xMID.Text
            'If Not Base._SaleOfAsset_DBOps.DeletePayment(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            DelParam.Mid_DeletePurpose = Me.xMID.Text
            'If Not Base._SaleOfAsset_DBOps.DeletePurpose(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            'PROFILE ENTRIES
            DelParam.Mid_DeleteAdvances = Me.xMID.Text
            'If Not Base._AdvanceDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_SaleOfAsset) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            DelParam.Mid_Delete = Me.xMID.Text
            'If Not Base._SaleOfAsset_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            DelParam.Mid_DeleteMaster = Me.xMID.Text
            'If Not Base._SaleOfAsset_DBOps.DeleteMaster(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            If Not Base._SaleOfAsset_DBOps.DeleteSaleOfAssets_Txn(DelParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If
        If Not xPromptWindow Is Nothing Then xPromptWindow.Dispose()


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
    Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Party)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Party Detail)..." : xfrm.TitleX.Text = "Party Detail"
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

#End Region

#Region "Start--> TextBox Events"

    Private Sub Txt_Amount_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.EditValueChanged, Txt_SaleAmt.EditValueChanged
        Me.Txt_Diff.Text = Format(Val(Txt_SaleAmt.Text) - Val(Txt_Amount.Text), "#0.00")
    End Sub
    Private Sub Txt_V_Date_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_V_Date.EditValueChanged
        Me.Txt_SDate.EditValue = Txt_V_Date.EditValue
    End Sub

    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Amount.GotFocus, Txt_Amount.Click, Txt_SaleAmt.GotFocus, Txt_SaleAmt.Click, Txt_Ref_No.GotFocus, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus, Txt_Ref_Branch.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_Amount.Name Or txt.Name = Txt_SaleAmt.Name Or txt.Name = Txt_Qty.Name Then
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
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_Amount.KeyDown, Txt_V_Date.KeyDown, Txt_Ref_No.KeyDown, Txt_Ref_CDate.KeyDown, Txt_Ref_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_Ref_Branch.KeyDown, Txt_SaleAmt.KeyDown, Txt_Qty.KeyDown
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

    Private Sub Cmb_Asset_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Asset_Type.SelectedIndexChanged
        'GLookUp_AssetList.EditValue = ""
        Me.GLookUp_AssetList.Tag = "" : Me.Txt_Desc.Text = ""
        Me.Txt_Amount.Text = "" : Txt_Diff.Text = "" : Txt_SaleAmt.Text = "" : Txt_SDate.EditValue = Txt_V_Date.EditValue : Txt_Qty.Text = ""
        Me.GLookUp_AssetList.Properties.DataSource = Nothing
        If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then 'ONLY FOR GOLD / SILVER
            Me.lbl_CurQty.Text = "Current Weight:" : Me.lbl_SaleQty.Text = "Sale Weight:"
            Me.Txt_Qty.Properties.Mask.EditMask = "f3"
        Else
            Me.lbl_CurQty.Text = "Current Qty:" : Me.lbl_SaleQty.Text = "Sale Qty:"
            Me.Txt_Qty.Properties.Mask.EditMask = "d"
        End If
        Get_Asset_Items()
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
        ElseIf Cmd_Mode.Text.ToUpper = "BANK ACCOUNT" Then
            LayoutControlItem1.Control.Enabled = True
            LayoutControlItem3.Control.Enabled = False
            If Cnt_BankAccount > 1 Then
                Me.GLookUp_BankList.Tag = "" : GLookUp_BankList.EditValue = "" : BE_Bank_Branch.Text = "" : BE_Bank_Acc_No.Text = ""
            Else
                Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
                Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy
            End If
            Me.GLookUp_RefBankList.Tag = "" : GLookUp_RefBankList.EditValue = "" : Txt_Ref_Branch.Text = "" : Txt_Ref_No.Text = "" : Txt_Ref_Date.EditValue = "" : Txt_Ref_CDate.EditValue = ""
            lbl_Bank_Title.ForeColor = Color.Black : lbl_Bank_Name.ForeColor = Color.Red : lbl_Branch_Name.ForeColor = Color.Black : lbl_Acc_No.ForeColor = Color.Black
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
        End If


        If Cmd_Mode.Text.ToUpper = "CHEQUE" Then lbl_Ref_Title.Text = "Cheque Detail:" : lbl_Ref_No.Text = "Cheque No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Cheque No..." : lbl_Ref_Date.Text = "Cheque Date:"
        If Cmd_Mode.Text.ToUpper = "DD" Then lbl_Ref_Title.Text = "D.D. Detail:" : lbl_Ref_No.Text = "D.D. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type D.D. No..." : lbl_Ref_Date.Text = "D.D. Date:"
        If Cmd_Mode.Text.ToUpper = "BANK ACCOUNT" Then
            lbl_Ref_Title.Text = "Reference Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type No..." : lbl_Ref_Date.Text = "Date:"
        End If
        If Cmd_Mode.Text.ToUpper = "CBS" Or Cmd_Mode.Text.ToUpper = "RTGS" Or Cmd_Mode.Text.ToUpper = "NEFT" Or Cmd_Mode.Text.ToUpper = "ECS" Then
            lbl_Ref_Title.Text = Cmd_Mode.Text.ToUpper & " Detail:" : lbl_Ref_No.Text = "Ref. No.:" : Txt_Ref_No.Properties.NullValuePrompt = "Type Ref. No..." : lbl_Ref_Date.Text = "Date:"
        End If
        If Cmd_Mode.Text = "CBS" And (Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection) Then
            Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
            Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", Me.lbl_Ref_No.Tag)
            Me.GLookUp_RefBankList.EditValue = Me.lbl_Ref_No.Tag
            Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
            Me.Cmd_Mode.Focus()
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

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Sale of Asset Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Sale of Asset"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_SDate.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_SDate.Properties.ReadOnly = True
        Me.Txt_Ref_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.Txt_Ref_CDate.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_BankList.Tag = "" : LookUp_GetBankList()
        GLookUp_RefBankList.Tag = "" : LookUp_GetRefBankList()
        GLookUp_PartyList1.Tag = "" : LookUp_GetPartyList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()
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
        If Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.SuperUser.ToUpper AndAlso Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
            GLookUp_AssetListView.Columns("REF_AMT").Visible = False
        End If
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        Me.xMID.Text = Base._SaleOfAsset_DBOps.GetMasterID(Me.xID.Text)
        Dim d1 As DataTable = Base._SaleOfAsset_DBOps.GetMasterRecord(Me.xMID.Text)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim d2 As DataTable = Base._SaleOfAsset_DBOps.GetPurposeRecord(Me.xMID.Text)
        Dim d3 As DataTable = Base._SaleOfAsset_DBOps.GetRecord(Me.xMID.Text)
        If d2 Is Nothing Or d3 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim xDate As DateTime = Nothing
        xDate = d3.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate
        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Or Me.Tag = Common_Lib.Common.Navigation_Mode._View Then
                Dim viewstr As String = ""
                If Me.Tag = Common_Lib.Common.Navigation_Mode._View Then viewstr = "view"
                If Info_LastEditedOn <> Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Sale", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+

        LastEditedOn = Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON"))

        Txt_V_NO.DataBindings.Add("TEXT", d3, "TR_VNO")

        Cmd_Mode.DataBindings.Add("text", d3, "TR_MODE")

        If Not IsDBNull(d3.Rows(0)("TR_ITEM_ID")) Then
            If d3.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d3.Rows(0)("TR_ITEM_ID"))
                Me.GLookUp_ItemList.EditValue = d3.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_ItemList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_AB_ID_1")) Then
            If d3.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", d3.Rows(0)("TR_AB_ID_1")))
                Me.GLookUp_PartyList1.EditValue = d3.Rows(0)("TR_AB_ID_1")
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PartyList1.Properties.ReadOnly = False

        Cmb_Asset_Type.DataBindings.Add("EditValue", d1, "TR_SALE_TYPE")

        xDate = d1.Rows(0)("TR_SALE_DATE") : Txt_SDate.DateTime = xDate
        Txt_SaleAmt.DataBindings.Add("EditValue", d1, "TR_SALE_AMT")
        Txt_Amount.DataBindings.Add("EditValue", d1, "TR_SUB_AMT")

        If Not IsDBNull(d3.Rows(0)("TR_TRF_CROSS_REF_ID")) Then
            If d3.Rows(0)("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                Me.GLookUp_AssetList.ShowPopup() : Me.GLookUp_AssetList.ClosePopup()
                Me.GLookUp_AssetListView.MoveBy(Me.GLookUp_AssetListView.LocateByValue("REF_ID", d3.Rows(0)("TR_TRF_CROSS_REF_ID")))
                Me.GLookUp_AssetList.EditValue = d3.Rows(0)("TR_TRF_CROSS_REF_ID")
                Me.GLookUp_AssetList.Tag = Me.GLookUp_AssetList.EditValue
                Me.GLookUp_AssetList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_AssetList.Properties.ReadOnly = False

        Txt_Qty.DataBindings.Add("EditValue", d1, "TR_SALE_QTY")
        Dim Bank_ID As String = ""
        If iTrans_Type.ToUpper = "DEBIT" Then
            If (Not IsDBNull(d3.Rows(0)("TR_SUB_CR_LED_ID")) And IIf(IsDBNull(d3.Rows(0)("TR_CR_LED_ID")), "", d3.Rows(0)("TR_CR_LED_ID")) = "00079") Then Bank_ID = d3.Rows(0)("TR_SUB_CR_LED_ID")
        Else
            If (Not IsDBNull(d3.Rows(0)("TR_SUB_DR_LED_ID")) And IIf(IsDBNull(d3.Rows(0)("TR_DR_LED_ID")), "", d3.Rows(0)("TR_DR_LED_ID")) = "00079") Then Bank_ID = d3.Rows(0)("TR_SUB_DR_LED_ID")
        End If

        If Bank_ID.Length > 0 Then
            Me.GLookUp_BankList.ShowPopup() : Me.GLookUp_BankList.ClosePopup()
            Me.GLookUp_BankListView.FocusedRowHandle = Me.GLookUp_BankListView.LocateByValue("BA_ID", Bank_ID)
            Me.GLookUp_BankList.EditValue = Bank_ID
            Me.GLookUp_BankList.Tag = Me.GLookUp_BankList.EditValue
            Me.GLookUp_BankList.Properties.Tag = "SHOW"
        End If : Me.GLookUp_BankList.Properties.ReadOnly = False

        If Not IsDBNull(d3.Rows(0)("TR_REF_BANK_ID")) Then
            If d3.Rows(0)("TR_REF_BANK_ID").ToString.Length > 0 Then
                Me.GLookUp_RefBankList.ShowPopup() : Me.GLookUp_RefBankList.ClosePopup()
                Me.GLookUp_RefBankListView.FocusedRowHandle = Me.GLookUp_RefBankListView.LocateByValue("BI_ID", d3.Rows(0)("TR_REF_BANK_ID"))
                Me.GLookUp_RefBankList.EditValue = d3.Rows(0)("TR_REF_BANK_ID")
                Me.GLookUp_RefBankList.Tag = Me.GLookUp_RefBankList.EditValue
                Me.GLookUp_RefBankList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_RefBankList.Properties.ReadOnly = False

        Txt_Ref_Branch.DataBindings.Add("text", d3, "TR_REF_BRANCH")
        Txt_Ref_No.DataBindings.Add("text", d3, "TR_REF_NO")
        If Not IsDBNull(d3.Rows(0)("TR_REF_DATE")) Then
            xDate = d3.Rows(0)("TR_REF_DATE") : Txt_Ref_Date.DateTime = xDate
        End If
        If Not IsDBNull(d3.Rows(0)("TR_REF_CDATE")) Then
            xDate = d3.Rows(0)("TR_REF_CDATE") : Txt_Ref_CDate.DateTime = xDate
        End If


        If Not IsDBNull(d2.Rows(0)("TR_PURPOSE_MISC_ID")) Then
            If d2.Rows(0)("TR_PURPOSE_MISC_ID").ToString.Length > 0 Then
                Me.GLookUp_PurList.ShowPopup() : Me.GLookUp_PurList.ClosePopup()
                Me.GLookUp_PurListView.MoveBy(Me.GLookUp_PurListView.LocateByValue("PUR_ID", d2.Rows(0)("TR_PURPOSE_MISC_ID")))
                Me.GLookUp_PurList.EditValue = d2.Rows(0)("TR_PURPOSE_MISC_ID")
                Me.GLookUp_PurList.Tag = Me.GLookUp_PurList.EditValue
                Me.GLookUp_PurList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_PurList.Properties.ReadOnly = False


        Txt_Narration.DataBindings.Add("text", d3, "TR_NARRATION")
        Txt_Remarks.DataBindings.Add("text", d3, "TR_REMARKS")
        Txt_Reference.DataBindings.Add("text", d3, "TR_REFERENCE")

        'RAD_Receipt.DataBindings.Add("SelectedIndex", d1, "TR_RECEIPT_TYPE")

        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_Mode.Enabled = False : Me.Cmd_Mode.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Item_Head.Enabled = False : Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList1.Enabled = False : Me.GLookUp_PartyList1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_City.Enabled = False : Me.BE_City.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_PAN_No.Enabled = False : Me.BE_PAN_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Cmb_Asset_Type.Enabled = False : Me.Cmb_Asset_Type.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_AssetList.Enabled = False : Me.GLookUp_AssetList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Desc.Enabled = False : Me.Txt_Desc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_CurQty.Enabled = False : Me.Txt_CurQty.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_RefBankList.Enabled = False : Me.GLookUp_RefBankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Branch.Enabled = False : Me.Txt_Ref_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.GLookUp_BankList.Enabled = False : Me.GLookUp_BankList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Branch.Enabled = False : Me.BE_Bank_Branch.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_Bank_Acc_No.Enabled = False : Me.BE_Bank_Acc_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Ref_No.Enabled = False : Me.Txt_Ref_No.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_Date.Enabled = False : Me.Txt_Ref_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Ref_CDate.Enabled = False : Me.Txt_Ref_CDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Amount.Enabled = False : Me.Txt_Amount.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SaleAmt.Enabled = False : Me.Txt_SaleAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SDate.Enabled = False : Me.Txt_SDate.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Diff.Enabled = False : Me.Txt_Diff.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.But_PersAdd.Enabled = False
        Me.But_PersManage.Enabled = False
    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.GLookUp_BankList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
        Me.ToolTip1.Hide(Me.Txt_Amount)
    End Sub
    Private Function FindLocationUsage(PropertyID As String, Optional Exclude_Sold_TF As Boolean = True) As String
        Dim Message As String = ""
        Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID)
        For Each cRow As DataRow In Locations.Rows
            Dim LocationID As String = cRow(0).ToString()
            Dim UsedPage As String = Base._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF)
            Dim DeleteAllow As Boolean = True
            If UsedPage.Length > 0 Then DeleteAllow = False
            If Not DeleteAllow Then
                Message = "P r o p e r t y   b e i n g   s o l d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                Exit For
            End If
        Next
        Return Message
    End Function

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

                iParty_Req = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PARTY_REQ").ToString
                If iParty_Req.ToString.ToUpper = "YES" Then lbl_Party.ForeColor = Color.Red Else lbl_Party.ForeColor = Color.Black

                iProfile = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PROFILE").ToString

                iTDS_CODE = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_TDS_CODE").ToString

                iOffSet_ID = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_OFFSET_REC_ID").ToString
                iOffSet_Item = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_OFFSET_NAME").ToString

            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._SaleOfAsset_DBOps.GetLedgerItems(Base.Is_HQ_Centre)
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

                Me.lbl_Ref_No.Tag = Me.GLookUp_BankListView.GetRowCellValue(Me.GLookUp_BankListView.FocusedRowHandle, "BANK_ID").ToString
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetBankList()
        'bank
        Dim BA_Table As DataTable = Base._SaleOfAsset_DBOps.GetBankAccounts()
        If BA_Table Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim Branch_IDs As String = ""
        For Each xRow As DataRow In BA_Table.Rows : Branch_IDs += "'" & xRow("BA_BRANCH_ID").ToString & "'," : Next
        If Branch_IDs.Trim.Length > 0 Then Branch_IDs = IIf(Branch_IDs.Trim.EndsWith(","), Mid(Branch_IDs.Trim.ToString, 1, Branch_IDs.Trim.Length - 1), Branch_IDs.Trim.ToString)
        If Branch_IDs.Trim.Length = 0 Then Branch_IDs = "''"

        Dim BB_Table As DataTable = Base._SaleOfAsset_DBOps.GetBranchDetails(Branch_IDs)
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
        Dim B2 As DataTable = Base._SaleOfAsset_DBOps.GetBanks()
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
    Private Sub GLookUp_PartyList1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList1View.RowCount > 0 And Val(Me.GLookUp_PartyList1View.FocusedRowHandle) >= 0) Then
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_ID").ToString
                Me.BE_PAN_No.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_PAN_NO").ToString
                Me.BE_City.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_CITY").ToString
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_PartyList1.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_PartyList_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_PartyList_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator(CriteriaOperator.Parse("Replace(Replace(Replace(Replace([C_NAME],'.',''),' ',''),',',''),'-','')", Nothing), "%" + edit.AutoSearchText.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("-", "") + "%", BinaryOperatorType.Like)
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._SaleOfAsset_DBOps.GetParties()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim ROW As DataRow : ROW = d1.NewRow() : ROW("C_NAME") = "" : d1.Rows.Add(ROW)

        Dim dview As New DataView(d1) : dview.Sort = "C_NAME"
        If dview.Count > 0 Then
            Me.GLookUp_PartyList1.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList1.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList1.Properties.DataSource = dview
            Me.GLookUp_PartyList1View.RefreshData()
            Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            Me.GLookUp_PartyList1.Text = ""
        Else
            Me.GLookUp_PartyList1.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList1.Properties.ReadOnly = False
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
        Dim d1 As DataTable = Base._SaleOfAsset_DBOps.GetPurposes()
        Dim dview As New DataView(d1)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
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

    '5.GLookUp_AssetList
    Private Sub GLookUp_Get_AssetList(ByVal xDView As DataView)
        If xDView.Count > 0 Then
            Me.GLookUp_AssetList.Properties.ValueMember = "REF_ID"
            Me.GLookUp_AssetList.Properties.DisplayMember = "REF_ITEM"
            Me.GLookUp_AssetList.Properties.DataSource = xDView
            Me.GLookUp_AssetListView.RefreshData()
            Me.GLookUp_AssetList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_AssetList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_AssetList.Properties.ReadOnly = False
    End Sub
    Private Sub GLookUp_AssetList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_AssetList.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_AssetListView.RowCount > 0 And Val(Me.GLookUp_AssetListView.FocusedRowHandle) >= 0) Then
                Me.GLookUp_AssetList.Tag = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_ID").ToString
                Me.Txt_Desc.Text = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_DESC").ToString
                Me.Txt_CurQty.Text = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_QTY").ToString
                Me.Txt_Qty.Text = ""
                iLed_ID = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_LED_ID").ToString
                If Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                    iAsset_Type = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "AI_TYPE").ToString
                    iCon_Led_ID = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "ITEM_CON_LED_ID").ToString
                    'iMinValue = Val(Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "ITEM_CON_MIN_VALUE").ToString)
                    'iMaxValue = Val(Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "ITEM_CON_MAX_VALUE").ToString)
                End If
                iTrans_Type = "CREDIT"
                If Val(Me.Txt_CurQty.Text) = 1 Then
                    Me.Txt_CurQty.Enabled = False
                    Me.Txt_Qty.Enabled = False
                    Me.Txt_Qty.Text = Me.Txt_CurQty.Text
                Else
                    Me.Txt_CurQty.Enabled = True
                    Me.Txt_Qty.Enabled = True
                End If
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_AssetList_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_AssetList.EditValueChanging
        Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() FilterLookup(sender)))
    End Sub
    Private Sub FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator("REF_ITEM", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op2 As New BinaryOperator("REF_AMT", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op3 As New BinaryOperator("REF_DESC", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        Dim op4 As New BinaryOperator("REF_CREATION_DATE", "%" + edit.AutoSearchText + "%", BinaryOperatorType.[Like])
        filterCondition = New GroupOperator(GroupOperatorType.[Or], New CriteriaOperator() {op1, op2, op3, op4}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub

    'GET ASSET ITEMS
    Private Sub Get_Asset_Items()
        'Dim _SQL_1 As String = ""
        Dim _Profile As String = "GOLD" 'Dim _DataFrom As String = "DATA"

        Dim AssetParam As Common_Lib.RealTimeService.Param_GetAssetListingForSale = New Common_Lib.RealTimeService.Param_GetAssetListingForSale()
        AssetParam.Next_YearID = Base._next_Unaudited_YearID : AssetParam.Prev_YearId = Base._prev_Unaudited_YearID
        If Cmb_Asset_Type.SelectedIndex = 0 Then 'Gold
            _Profile = "GOLD" 'ASSET_TABLE = Base._SaleOfAsset_DBOps.GetGoldSilverList("GOLD")
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD
        ElseIf Cmb_Asset_Type.SelectedIndex = 1 Then 'Silver
            _Profile = "SILVER" ': ASSET_TABLE = Base._SaleOfAsset_DBOps.GetGoldSilverList("SILVER")
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER
        ElseIf Cmb_Asset_Type.SelectedIndex = 2 Then 'VEHICLES
            _Profile = "VEHICLES" ': ASSET_TABLE = Base._SaleOfAsset_DBOps.GetVehiclesList()
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES
        ElseIf Cmb_Asset_Type.SelectedIndex = 3 Then 'LIVESTOCK
            _Profile = "LIVESTOCK" ': ASSET_TABLE = Base._SaleOfAsset_DBOps.GetLiveStockList()
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK
        ElseIf Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
            _Profile = "OTHER ASSETS" ': ASSET_TABLE = Base._SaleOfAsset_DBOps.GetAssetList()
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS
        ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then 'LAND & BUILDING
            _Profile = "LAND & BUILDING" ': ASSET_TABLE = Base._SaleOfAsset_DBOps.GetLandandBuildingList()
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING
            '_DataFrom = "SYS"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then AssetParam.TR_M_ID = xMID.Text
        Dim ASSET_TABLE As DataTable = Base._SaleOfAsset_DBOps.GetAllProfile_AssetList(AssetParam)
        If ASSET_TABLE Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If

        'Code Commented Due to conversion of Code to SPs
        ''Misc. Table...
        'Dim MISC_Table As DataTable = Nothing
        'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then
        '    MISC_Table = Base._SaleOfAsset_DBOps.GetGoldSilverMisc("MISC_NAME", "MISC_ID")
        '    If MISC_Table Is Nothing Then
        '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If
        'End If

        ''Item Table...
        ''Dim Item_SQL As String = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UCASE(I.ITEM_PROFILE) IN ('" & _Profile & "')  AND  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
        'Dim ITEM_Table As DataTable = Base._SaleOfAsset_DBOps.GetSaleofAssetItems(_Profile)
        'If ITEM_Table Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If
        ''Transaction Table...
        'Dim _TR_TABLE As DataTable = Nothing
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
        '    If Base._prev_Unaudited_YearID.Length > 0 Then
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._prev_Unaudited_YearID, xMID.Text)
        '    Else
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._open_Year_ID, xMID.Text, Base._next_Unaudited_YearID)
        '    End If
        'Else
        '    If Base._prev_Unaudited_YearID.Length > 0 Then
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._prev_Unaudited_YearID)
        '    Else
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._open_Year_ID, Nothing, Base._next_Unaudited_YearID)
        '    End If
        'End If

        'If _TR_TABLE Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        ''Relationship...
        'Dim JointData As DataSet = New DataSet()
        'JointData.Tables.Add(ASSET_TABLE.Copy)
        'JointData.Tables.Add(ITEM_Table.Copy)

        'Dim Item_Relation1 As DataRelation = JointData.Relations.Add("Item_1", JointData.Tables(0).Columns("REF_ITEM_ID"), JointData.Tables("ITEM_INFO").Columns("ITEM_ID"), False)
        'For Each XRow In JointData.Tables(0).Rows : For Each Item_Row In XRow.GetChildRows(Item_Relation1)
        '        XRow("REF_ITEM") = Item_Row("ITEM_NAME")
        '        XRow("REF_LED_ID") = Item_Row("ITEM_LED_ID")
        '        XRow("REF_TRANS_TYPE") = Item_Row("ITEM_TRANS_TYPE")
        '        If Cmb_Asset_Type.SelectedIndex = 4 Then
        '            XRow("ITEM_CON_LED_ID") = Item_Row("ITEM_CON_LED_ID")
        '            XRow("ITEM_CON_MIN_VALUE") = Item_Row("ITEM_CON_MIN_VALUE")
        '            XRow("ITEM_CON_MAX_VALUE") = Item_Row("ITEM_CON_MAX_VALUE")
        '        End If
        '    Next : Next
        ''2
        'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then 'ONLY FOR GOLD / SILVER
        '    JointData.Tables.Add(MISC_Table.Copy)
        '    Dim Misc_Relation As DataRelation = JointData.Relations.Add("Misc_1", JointData.Tables(0).Columns("REF_MISC_ID"), JointData.Tables("MISC_INFO").Columns("MISC_ID"), False)
        '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Misc_Relation) : XRow("REF_DESC") = _Row("MISC_NAME") : Next : Next
        'End If

        'JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation As DataRelation = JointData.Relations.Add("Received", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
        'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation) : XRow("SALE_QTY") = _Row("SALE_QTY") : Next : Next

        ''Clear Relations
        'JointData.Relations.Clear()

        ''j.v.adjustment 
        'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
        '    'DEBIT
        '    If Cmb_Asset_Type.SelectedIndex = 4 Then
        '        If Base._prev_Unaudited_YearID.Length > 0 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "asset_info"))
        '        Else
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "asset_info"))
        '        End If
        '    End If

        '    If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '        If Base._prev_Unaudited_YearID.Length > 0 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._prev_Unaudited_YearID, "gold_silver_info"))
        '        Else
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "gold_silver_info"))
        '        End If
        '    End If

        '    Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '    For Each XRow In JointData.Tables(0).Rows
        '        For Each _Row In XRow.GetChildRows(JV_Relation)
        '            XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
        '        Next
        '    Next

        '    'CREDIT
        '    If Cmb_Asset_Type.SelectedIndex = 4 Then
        '        If Base._prev_Unaudited_YearID.Length > 0 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "asset_info"))
        '        Else
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "asset_info", Base._next_Unaudited_YearID))
        '        End If

        '    End If
        '    If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '        If Base._prev_Unaudited_YearID.Length > 0 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._prev_Unaudited_YearID, "gold_silver_info"))
        '        Else
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "gold_silver_info", Base._next_Unaudited_YearID))
        '        End If
        '    End If

        '    Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '    For Each XRow In JointData.Tables(0).Rows
        '        For Each _Row In XRow.GetChildRows(JV_CR_Relation)
        '            XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
        '        Next
        '    Next
        'End If

        ''Asset Transfers 
        'Dim Transfers As DataTable = Nothing
        'If Base._prev_Unaudited_YearID.Length > 0 Then
        '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._prev_Unaudited_YearID)
        'Else
        '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._open_Year_ID)
        'End If

        'JointData.Tables.Add(Transfers.Copy) : Dim Transfer_Relation As DataRelation = JointData.Relations.Add("Transfer", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("TRANSACTION_INFO").Columns("Reference"), False)
        'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Transfer_Relation)
        '        XRow("REF_QTY") = Convert.ToInt32(XRow("REF_QTY") - _Row("QTY"))
        '    Next : Next

        ''Clear Relations
        'JointData.Relations.Clear()

        'JointData.Tables.Remove("TRANSACTION_D_PAYMENT_INFO")
        'JointData.Tables.Remove("TRANSACTION_INFO")

        ''Changes for Year Ending Process
        'If Base._prev_Unaudited_YearID.Length > 0 Then
        '    'Transaction Table...
        '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(False, Base._open_Year_ID, xMID.Text)
        '    Else
        '        _TR_TABLE = Base._SaleOfAsset_DBOps.GetTxnList(True, Base._open_Year_ID, xMID.Text)
        '    End If

        '    JointData.Tables.Add(_TR_TABLE.Copy) : Dim Adv_Relation1 As DataRelation = JointData.Relations.Add("Received", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
        '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Adv_Relation1) : XRow("SALE_QTY") = XRow("SALE_QTY") + _Row("SALE_QTY") : Next : Next

        '    'j.v.adjustment 
        '    If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
        '        JointData.Tables.Remove("DEBIT")
        '        JointData.Tables.Remove("CREDIT")
        '        'DEBIT
        '        If Cmb_Asset_Type.SelectedIndex = 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "asset_info"))
        '        End If

        '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._open_Year_ID, "gold_silver_info"))
        '        End If

        '        Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '        For Each XRow In JointData.Tables(0).Rows
        '            For Each _Row In XRow.GetChildRows(JV_Relation)
        '                XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
        '            Next
        '        Next

        '        'CREDIT
        '        If Cmb_Asset_Type.SelectedIndex = 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "asset_info"))
        '        End If
        '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._open_Year_ID, "gold_silver_info"))
        '        End If

        '        Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '        For Each XRow In JointData.Tables(0).Rows
        '            For Each _Row In XRow.GetChildRows(JV_CR_Relation)
        '                XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
        '            Next
        '        Next
        '    End If

        '    'Asset t/f
        '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._open_Year_ID)
        '    JointData.Tables.Add(Transfers.Copy) : Dim Transfer_Relation1 As DataRelation = JointData.Relations.Add("Transfer", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("TRANSACTION_INFO").Columns("Reference"), False)
        '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Transfer_Relation1)
        '            XRow("REF_QTY") = Convert.ToInt32(XRow("REF_QTY") - _Row("QTY"))
        '        Next : Next

        '    'Clear Relations
        '    JointData.Relations.Clear()
        '    JointData.Tables.Remove("TRANSACTION_D_PAYMENT_INFO")
        'End If

        'If Base._next_Unaudited_YearID.Length > 0 Then
        '    'j.v.adjustment 
        '    If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
        '        JointData.Tables.Remove("DEBIT")
        '        JointData.Tables.Remove("CREDIT")
        '        'DEBIT
        '        If Cmb_Asset_Type.SelectedIndex = 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._next_Unaudited_YearID, "asset_info"))
        '        End If

        '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, True, Base._next_Unaudited_YearID, "gold_silver_info"))
        '        End If

        '        Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '        For Each XRow In JointData.Tables(0).Rows
        '            For Each _Row In XRow.GetChildRows(JV_Relation)
        '                XRow("REF_QTY") = 0 ' If any jv has been passed in next year, then the asset cant be sold in current year 
        '            Next
        '        Next

        '        'CREDIT
        '        If Cmb_Asset_Type.SelectedIndex = 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._next_Unaudited_YearID, "asset_info"))
        '        End If
        '        If Cmb_Asset_Type.SelectedIndex <> 4 Then
        '            JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_SaleOfAsset, Nothing, False, Base._next_Unaudited_YearID, "gold_silver_info"))
        '        End If

        '        Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
        '        For Each XRow In JointData.Tables(0).Rows
        '            For Each _Row In XRow.GetChildRows(JV_CR_Relation)
        '                XRow("REF_QTY") = 0 ' If any jv has been passed in next year, then the asset cant be sold in current year 
        '            Next
        '        Next
        '    End If

        '    'Asset t/f
        '    Transfers = Base._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Voucher_SaleOfAsset, Base._next_Unaudited_YearID)
        '    JointData.Tables.Add(Transfers.Copy) : Dim Transfer_Relation1 As DataRelation = JointData.Relations.Add("Transfer", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("TRANSACTION_INFO").Columns("Reference"), False)
        '    For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Transfer_Relation1)
        '            XRow("REF_QTY") = Convert.ToInt32(XRow("REF_QTY") - _Row("QTY"))
        '        Next : Next

        'End If

        ''Delete Out-Standing Zero......................................
        'Dim AI_Temp As DataTable = New DataTable : Dim xNrow As DataRow
        'With AI_Temp : .Columns.Add("_Rid", Type.GetType("System.Int32")) : End With
        'For Each XRow In JointData.Tables(0).Rows
        '    If XRow("REF_QTY") - XRow("SALE_QTY") <= 0 Then
        '        xNrow = AI_Temp.NewRow : xNrow("_Rid") = JointData.Tables(0).Rows.IndexOf(XRow) : AI_Temp.Rows.Add(xNrow)
        '    Else
        '        XRow("REF_QTY") = XRow("REF_QTY") - XRow("SALE_QTY")
        '    End If
        'Next
        ''end reqd ' use XRow("REF_QTY") in end to compare 
        'For Each XRow In AI_Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next
        ''..............................................................
        '
        Dim dview As New DataView(ASSET_TABLE) ': dview.Sort = "REF_ITEM"

        GLookUp_Get_AssetList(dview)
    End Sub

#End Region

End Class