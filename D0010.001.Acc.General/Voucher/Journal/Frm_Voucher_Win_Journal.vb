Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_Voucher_Win_Journal

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Public iSpecific_ItemID As String
    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Private LB_DOCS_ARRAY As DataTable
    Private LB_EXTENDED_PROPERTY_TABLE As DataTable
    Public Info_MaxEditedOn As Dictionary(Of String, DateTime) = New Dictionary(Of String, DateTime)()
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
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then 'By Item-wise Selection
            Me.DataNavigation("NEW")
        End If
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If (keyData = (Keys.Control Or Keys.S)) Then ' save
                If Me.BUT_SAVE_COM.Enabled Then
                    BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
                    Return (True)
                End If
            End If

            If (keyData = Keys.Insert) Then
                Me.DataNavigation("NEW")
                Return (True)
            End If
            If GridControl1.Focused Then
                If (keyData = Keys.Enter) Then
                    Me.DataNavigation("EDIT")
                    Return (True)
                End If
                If (keyData = Keys.Delete) Then
                    Me.DataNavigation("DELETE")
                    Return (True)
                End If
            End If
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
            If (keyData = (Keys.Control Or Keys.D)) Then 'delete
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
        Dim xPromptWindow As New Common_Lib.Prompt_Window
        If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then Exit Sub
        End If

        For I As Integer = 0 To Me.GridView1.RowCount - 1
            If Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "OTHER ASSETS" Or (Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString().ToUpper = "LAND & BUILDING" And Not Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "LAND & BUILDING") Then
                If Base.IsInsuranceAudited() Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s  V a l u e   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    Exit Sub
                End If
            End If

            If GridView1.GetRowCellValue(I, "Item_Voucher_Type").Trim.ToUpper = "LAND & BUILDING" And Not GridView1.GetRowCellValue(I, "Item_Profile").ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
                If Base.IsInsuranceAudited() Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y   R e l a t e d   E x p e n s e s   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            '--------Check for Creation date of WIP---------
            If Not GridView1.GetRowCellValue(I, "WIP_REF_TYPE") Is Nothing And Not IsDBNull(GridView1.GetRowCellValue(I, "WIP_REF_TYPE")) Then
                If GridView1.GetRowCellValue(I, "WIP_REF_TYPE") = "EXISTING" Then
                    Dim WIP_ID = IIf(IsDBNull(GridView1.GetRowCellValue(I, "REF_REC_ID")), Nothing, GridView1.GetRowCellValue(I, "REF_REC_ID"))
                    Dim creationStats As DataTable = Base._WIPCretionVouchers.GetRefCreationDateByWIPID(WIP_ID)
                    If Not creationStats Is Nothing Then
                        If creationStats.Rows.Count > 0 Then
                            If creationStats.Rows(0)("TR_DATE") > Txt_V_Date.DateTime Then
                                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                Me.ToolTip1.Show("Referencing Voucher Date must be greater than creation Date(" & Convert.ToDateTime(creationStats.Rows(0)("TR_DATE")).ToString(Base._Date_Format_DD_MMM_YYYY) & ") of WIP namely " & creationStats.Rows(0)("WIP_REF").ToString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                                Me.GridView1.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        Next
       

        If Not Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then ' #5407 fixed
            Dim DistinctCol As String() = New String() {"PartyID"}
            For Each _Row In DT.Rows 'Bug #5065 fixed
                If IsDBNull(_Row("PartyID")) Then _Row("PartyID") = ""
                _Row("PartyID").ToString.Trim()
            Next
            Dim TDS_Present As Boolean = False
            Dim PartyCount As Int16 = DT.Rows.Count
            For I As Integer = 0 To Me.GridView1.RowCount - 1
                'If Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() <> "ADVANCES" And Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() <> "OTHER DEPOSITS" And Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() <> "OTHER LIABILITIES" And Me.GridView1.GetRowCellValue(I, "Party").ToString().Length > 0 Then nonPartyProfileCount += 1
                'If Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "ADVANCES" Or Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "OTHER DEPOSITS" Or Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString() = "OTHER LIABILITIES" Then PartyProfileCount += 1
                If Me.GridView1.GetRowCellValue(I, "Party").ToString() = "" Then PartyCount = PartyCount - 1
                If Me.GridView1.GetRowCellValue(I, "Head").ToString().ToUpper.Contains("TDS") Then TDS_Present = True
            Next

            'If BlankPartyPresent Then PartyCount = PartyCount - 1

            If PartyCount = 0 And TDS_Present Then
                If DialogResult.No = xPromptWindow.ShowDialog("Message...", "<size=13><b>This Voucher contains TDS item(s) but no Party has been selected. </b></size>" & vbNewLine & vbNewLine & "Do you still want to Save Voucher...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                    'DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  O n l y   O n e   p a r t y   c a n   b e   u s e d   i n   V o u c h e r s   c o n t a i n i n g   I t e m s   o t h e r   t h a n   A d v a n c e / L i a b i l i t y / D e p o s i t . . . !" & vbNewLine & vbNewLine & "Please use same party in all lines of Journal entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim journal_DbOps As DataTable = Base._Journal_voucher_DBOps.GetRecords(Me.xMID.Text)
                If journal_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If journal_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Journal Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(journal_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Journal Voucher"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'End If
                'ED/L
                Dim MaxValue As Object = 0
                MaxValue = Base._Payment_DBOps.GetStatusByMID(Me.xMID.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                Dim AssetItems As DataTable = Base._Voucher_DBOps.GetAssetItemID(Me.xMID.Text)
                If AssetItems Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                For Each cRow As DataRow In AssetItems.Rows ' Get Actual Item IDs from Selected Transaction
                    Dim xTemp_ItemID As String = cRow(0).ToString()
                    Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                    Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                    If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                        Dim xTemp_AssetID As String = ""
                        Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                            Case "GOLD", "SILVER"
                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, Me.xMID.Text)
                            Case "OTHER ASSETS"
                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, Me.xMID.Text)
                            Case "LIVESTOCK"
                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, Me.xMID.Text)
                            Case "VEHICLES"
                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, Me.xMID.Text)
                            Case "LAND & BUILDING"
                                xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, Me.xMID.Text)
                        End Select
                        If xTemp_AssetID.Length > 0 Then
                            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                            If Not SaleRecord Is Nothing Then
                                If SaleRecord.Rows.Count > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                            Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord_Exclude_MID(xTemp_AssetID, Me.xMID.Text)
                            If Not ReferenceRecord Is Nothing Then
                                If ReferenceRecord.Rows.Count > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID)
                            If AssetTrfRecord.Rows.Count > 0 Then
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                        End If
                        'Else 'Non Profile Entries 
                        '    Dim _RefID As String = Base._Voucher_DBOps.GetReferenceRecordID(Me.xMID.Text)
                        '    If Not _RefID Is Nothing Then
                        '        If _RefID.Length > 0 Then
                        '            Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(_RefID) 'checks if the referred property for constt items has been sold 
                        '            If Not SaleRecord Is Nothing Then
                        '                If SaleRecord.Rows.Count > 0 Then
                        '                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '                    Exit Sub
                        '                End If
                        '            End If
                        '            Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, "", _RefID) 'checks if the referred property for constt items has been transfered 
                        '            If AssetTrfRecord.Rows.Count > 0 Then
                        '                If AssetTrfRecord.Rows.Count > 0 Then
                        '                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '                    Exit Sub
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                    End If
                Next

                'Special Checks

                Dim UseCount As Object = 0
                Dim RefId As String = Base._Voucher_DBOps.GetRaisedAdvanceRecID(Me.xMID.Text)
                If RefId Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If RefId.Length > 0 Then
                    UseCount = Base._AdvanceDBOps.GetAdvancePaymentCount(RefId)
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    param.CrossRefId = RefId
                    param.Excluded_Rec_M_ID = Me.xMID.Text
                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  A d v a n c e   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If

                RefId = Base._Voucher_DBOps.GetRaisedDepositRecID(Me.xMID.Text)
                If RefId Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If RefId.Length > 0 Then
                    UseCount = Base._DepositsDBOps.GetTransactionCount(RefId)
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    param.CrossRefId = RefId
                    param.Excluded_Rec_M_ID = Me.xMID.Text
                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  d e p o s i t   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If

                RefId = Base._Voucher_DBOps.GetRaisedLiabilityRecID(Me.xMID.Text)
                If RefId Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If RefId.Length > 0 Then
                    UseCount = Base._LiabilityDBOps.GetTransactionCount(RefId)
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                    Dim param As Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments = New Common_Lib.RealTimeService.Parameter_GetJornalEntryAdjustments()
                    param.CrossRefId = RefId
                    param.Excluded_Rec_M_ID = Me.xMID.Text
                    param.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                    param.NextUnauditedYearID = Base._next_Unaudited_YearID
                    UseCount = Base._Journal_voucher_DBOps.GetJornalEntryAdjustments(param, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers).Rows.Count
                    If UseCount > 0 Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y!  l i a b i l i t y   c r e a t e d   b y   c u r r e n t   j o u r n a l   e n t r y   i s   u s e d   i n   s o m e   o t h e r   e n t r y. . . !" & vbNewLine & vbNewLine & "Please delete that dependency to edit this entry.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then


            If GridView1.RowCount = 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("P l e a s e   i n s e r t   i t e m s . . . !", "Items...", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.GridView1.Focus()
                Exit Sub
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

            'Check TDS postings 
            Dim TDS_Led_Credits As Boolean = False : Dim TDS_Debits As Boolean = False : Dim Credits As Boolean = False : Dim debits As Boolean = False
            For I As Integer = 0 To Me.GridView1.RowCount - 1
                If Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() = "00075" And Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString().ToUpper = "CREDIT" Then TDS_Led_Credits = True
                If Base._Journal_voucher_DBOps.IsTDSApplicable(Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()) And Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString().ToUpper = "DEBIT" Then TDS_Debits = True
                If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString().ToUpper = "CREDIT" Then Credits = True
                If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString().ToUpper = "DEBIT" Then debits = True
            Next

            If TDS_Debits = True And TDS_Led_Credits = False Then
                '  Dim xPromptWindow As New Common_Lib.Prompt_Window
                If DialogResult.No = xPromptWindow.ShowDialog(Me.Text, "<u><b><color=maroon>Items involving TDS are debited. But No TDS Entry is found.!</color></b></u><size=10>" & vbNewLine & vbNewLine & "<b><color=red>Do you still want to save... ?</color></b></size>", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                    Exit Sub
                End If
            End If

            If debits = False Or Credits = False Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("J V    m u s t   h a v e   b o t h   C r e d i t   a n d   D e b i t   P o s t i n g s . . . !", Me.Txt_DiffAmt, 0, Me.Txt_DiffAmt.Height, 5000)
                Me.Txt_DiffAmt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            End If
        End If

        If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then ' Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit ' Removed this check on updation as we dont recreate location on updation of property creation voucher 
            'Properties Created in Current Voucher
            Dim d1 As DataTable = Base._L_B_DBOps.GetIDsBytxnID(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)
            For Each cRow As DataRow In d1.Rows
                Dim Msg As String = FindLocationUsage(cRow(0), False) 'sold/tf assets not excluded
                If Msg.Length > 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.GridView1.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If
            Next
        End If

        '---------------------------// Start Dependencies //------------------------------------------

        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then

                'Checks for deletion of asset location in background in all assets
                For Each XRow In DT.Rows
                    Dim cnt As Object
                    Dim Loc_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    If Not Loc_ID Is Nothing Then
                        If Len(Loc_ID) > 0 Then
                            Dim PropertyId As Object = Base._AssetLocDBOps.GetPropertyID(Loc_ID)
                            If Not PropertyId Is Nothing And Not IsDBNull(PropertyId) Then
                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(PropertyId) 'checks if the referred property for constt items has been sold 
                                If Not SaleRecord Is Nothing Then
                                    If SaleRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a property which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for completing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, PropertyId) 'checks if the referred property for constt items has been transferred 
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a property which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                            End If

                            If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                                cnt = Base._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_GoldSilver, Loc_ID, Nothing).Rows.Count
                                If cnt <= 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            If XRow("Item_Profile") = "OTHER ASSETS" Then
                                cnt = Base._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Assets, Loc_ID, Nothing).Rows.Count
                                If cnt <= 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            If XRow("Item_Profile") = "LIVESTOCK" Then
                                cnt = Base._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_LiveStock, Loc_ID, Nothing).Rows.Count
                                If cnt <= 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            If XRow("Item_Profile") = "VEHICLES" Then
                                cnt = Base._AssetLocDBOps.GetList(Common_Lib.RealTimeService.ClientScreen.Profile_Vehicles, Loc_ID, Nothing).Rows.Count
                                If cnt <= 0 Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If

                    If XRow("Item_Profile") = "VEHICLES" Then
                        If Not XRow("VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(XRow("VI_OWNERSHIP_AB_ID")) Then
                            Dim Ownership_ID = IIf(Len(XRow("VI_OWNERSHIP_AB_ID")) = 0, Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                            Dim cnt1 = Base._VehicleDBOps.GetOwners_List(Ownership_ID).Rows.Count
                            If cnt1 <= 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If

                    If XRow("Item_Profile") = "LAND & BUILDING" Then
                        Dim PartyID = IIf(XRow("LB_OWNERSHIP_PARTY_ID").ToString.Length = 0, Nothing, XRow("LB_OWNERSHIP_PARTY_ID"))
                        If Not PartyID Is Nothing Then
                            cnt = Base._L_B_DBOps.GetOwners(PartyID).Rows.Count
                            If cnt <= 0 And PartyID <> "NULL" Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                    If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then 'Select Property screen
                        Dim Cross_Ref_ID As String = ""
                        Cross_Ref_ID = XRow("LB_REC_ID")
                        cnt = Base._L_B_DBOps.GetListForExpenses(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment, Cross_Ref_ID).Rows.Count
                        If cnt <= 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Property"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If

                    'Location Specific Checks
                    If XRow("Item_Profile") = "LAND & BUILDING" And Convert.ToBoolean(XRow("Addition")) Then
                        For Each cRow In DT.Rows
                            If IIf(IsDBNull(XRow("LB_PRO_NAME")), "", XRow("LB_PRO_NAME")) = IIf(IsDBNull(cRow("LB_PRO_NAME")), "", cRow("LB_PRO_NAME")) And IIf(IsDBNull(XRow("LB_REC_ID")), "", XRow("LB_REC_ID")) <> IIf(IsDBNull(cRow("LB_REC_ID")), "", cRow("LB_REC_ID")) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y / L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e   i n   s a m e   v o u c h e r. . . !", "Property Name Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Next

                        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                            Dim MaxValue_Loc As Object = 0
                            MaxValue_Loc = Base._AssetLocDBOps.GetRecordCountByName(XRow("LB_PRO_NAME"), Common_Lib.RealTimeService.ClientScreen.Profile_LandAndBuilding, Base._open_PAD_No_Main, Me.xMID.Text)
                            If MaxValue_Loc Is Nothing Then
                                Base.HandleDBError_OnNothingReturned()
                                Exit Sub
                            End If
                            If MaxValue_Loc <> 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e . . . !", "Property Name Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If

                        Dim LocNames As DataTable = Base._L_B_DBOps.GetPendingTfs_LocNames(Base._open_Cen_Rec_ID)
                        If Not LocNames Is Nothing Then
                            If LocNames.Rows.Count > 0 Then
                                If XRow("LB_PRO_NAME").ToString.Length > 0 Then
                                    For I = 0 To LocNames.Rows.Count - 1
                                        If XRow("LB_PRO_NAME").ToString.ToUpper = LocNames.Rows(I)(0).ToString.ToUpper Then
                                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Location name"), "Duplication in Referred Record, Location with same name already exists in pending Transfers!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                                            FormClosingEnable = False : Me.Close()
                                            Exit Sub
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If

                Next


            End If
        End If

        '------------------------------// End Dependencies //--------------------------------

        Dim ProfilesData = New Dictionary(Of Integer, DataTable)()

        '-------------------------------------// Start Dependencies // ----------------------------------
        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            For I As Integer = 0 To Me.GridView1.RowCount - 1 ' Bugs #4902, #4903, #4904 fixed
                If (Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper <> "NOT APPLICABLE" Or Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING") And Me.GridView1.GetRowCellValue(I, "Addition") = False Then 'Allows Profile or Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                    If Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString.Length > 0 Then ' Entries With Profile Reference 
                        If Base.AllowMultiuser() Then
                            'Bug #5697
                            Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile")
                                Case "GOLD", "SILVER", "OTHER ASSETS", "VEHICLES", "LIVESTOCK", "LAND & BUILDING" 'Bug #5695 fix
                                    If Info_MaxEditedOn.ContainsKey(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) Then
                                        If Info_MaxEditedOn(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) <> DateTime.MinValue Then 'Record has been opened on basis of this being a last edited record for referred asset
                                            If Base._AssetDBOps.Get_Asset_Ref_MaxEditOn(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) > Info_MaxEditedOn(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) Then
                                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CustomChanges("Sorry ! Current Voucher is no Longer Latest Edited Voucher referring the Current Asset. Another Record has been Added/Edited in background which refers the same Asset.", "Last Edited Reference Entry"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        End If
                                    End If
                                Case "OTHER DEPOSITS"
                                    Dim FinalPayDate As DateTime = Base._DepositsDBOps.GetFinalPaymentDate(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) 'Bug #5683
                                    If IsDate(FinalPayDate) And FinalPayDate <> DateTime.MinValue Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deposit Referred in Current voucher has already been Finally Adjusted in Receipt Voucher Dated " & FinalPayDate.ToLongDateString(), "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                            End Select
                        End If


                        Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                        ' Dim PROFILE_TABLE As DataTable = ProfilesData(I)  'jrnl_Item.GetReferenceData(Me.GridView1.GetRowCellValue(I, "Item_Profile"), Me.GridView1.GetRowCellValue(I, "Item_ID"), Me.GridView1.GetRowCellValue(I, "PartyID").ToString(), Me.xMID.Text, Me.Tag, Me.GridView1.GetRowCellValue(I, "CrossRefID"))
                        Dim PROFILE_TABLE As DataTable = jrnl_Item.GetReferenceData(Me.GridView1.GetRowCellValue(I, "Item_Profile"), Me.GridView1.GetRowCellValue(I, "Item_ID"), Me.GridView1.GetRowCellValue(I, "PartyID").ToString(), Me.xMID.Text, Me.Tag, Me.GridView1.GetRowCellValue(I, "CrossRefID"))
                        ' Base._Journal_voucher_DBOps.Get_JV_Asset_Listing(AssetParam) 'Fetch asset data as per selection
                        ProfilesData.Add(I, PROFILE_TABLE) ' adding in dictionary control to be used in date checks too
                        If PROFILE_TABLE Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        If Base.AllowMultiuser() Then
                            If PROFILE_TABLE.Rows.Count <= 0 Then ' Profile existence check ...(Removed Due to Sale/Tf/Deletion/Discard)
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile" + Me.GridView1.GetRowCellValue(I, "Item_Profile")), "Referred Record Already Deleted/Transferred/Sold!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            Else
                                Dim oldEditOn As DateTime = GridView1.GetRowCellValue(I, "RefItem_RecEditOn")
                                Dim newEditOn As DateTime = PROFILE_TABLE.Rows(0)("REC_EDIT_ON")
                                If oldEditOn <> newEditOn Then 'assets changed in profile
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile" + " " + Me.GridView1.GetRowCellValue(I, "Item_Profile")), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            Dim MessageOfComparision As String = ""
                            Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile")
                                Case "GOLD", "SILVER"
                                    'Bug #5071 fixed
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF QTY
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Weight")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()))) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                                            MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the qty/weight entered for deduction !!"
                                        End If
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value credited !!"
                                        End If
                                    Else
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Weight")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())) < 0) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                                            MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than the 0!!"
                                        End If
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                                        End If
                                    End If
                                Case "OTHER ASSETS"
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF QTY
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Qty")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()))) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                                            MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less then the qty/weight entered for deduction !!"
                                        End If
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value credited !!"
                                        End If
                                    Else
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Qty")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())) < 0) Then ' Weight/qty remaining is less then 0
                                            MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                                        End If
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                                            MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than the Value credited !!"
                                        End If
                                    End If
                                Case "VEHICLES", "LIVESTOCK", "LAND & BUILDING", "ADVANCES", "OTHER DEPOSITS", "WIP"
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF VALUE
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value credited !!"
                                        End If
                                    Else
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                                            MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                                        End If
                                    End If
                                Case "OTHER LIABILITIES"
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "DEBIT" Then 'DEDUCTION OF VALUE
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Current Value for selected liability is less than the Value debited !!"
                                        End If
                                    Else
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit"))) < 0) Then ' Value remaining is less then 0
                                            MessageOfComparision = "Adjusted Value for selected liability becomes less than 0 !!"
                                        End If
                                    End If
                                Case "OPENING"
                                    Exit Select
                                Case Else 'Construction Items
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF VALUE
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                            MessageOfComparision = "Current Value for referred property is less than the Value credited !!"
                                        End If
                                    Else
                                        If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                                            MessageOfComparision = "Adjusted Value for referred property becomes less than 0 !!"
                                        End If
                                    End If
                            End Select

                            If MessageOfComparision.Length > 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CustomChanges(MessageOfComparision, Me.GridView1.GetRowCellValue(I, "Item_Profile")), "Action not allowed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                If Base.AllowMultiuser() Then
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                        If Me.GridView1.GetRowCellValue(I, "PartyID").ToString.Length > 0 Then
                            Dim PartyDetail As DataTable = Base._Address_DBOps.GetRecord(Me.GridView1.GetRowCellValue(I, "PartyID").ToString())
                            Dim oldEditOn As DateTime = Me.GridView1.GetRowCellValue(I, "Party_RecEditOn")
                            If PartyDetail.Rows.Count <= 0 Then 'Party already changed/deleted
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address book"), "Record Already Deleted !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            Else
                                Dim newEditOn As DateTime = PartyDetail.Rows(0)("REC_EDIT_ON")
                                If newEditOn <> oldEditOn Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address book"), "Record Already Changed !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    FormClosingEnable = False : Me.Close()
                                    Exit Sub
                                End If
                            End If
                            'Check for Full Party Address , if a Gift Item has been referred 
                            If Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() = "00182" Then ' Contributions in Kind Ledger 'Bug #4731
                                If (PartyDetail.Rows(0)("C_R_ADD1").ToString.Trim.Length <= 0) Or (PartyDetail.Rows(0)("C_R_CITY_ID").ToString.Trim.Length <= 0) Or (PartyDetail.Rows(0)("C_R_DISTRICT_ID").ToString.Trim.Length <= 0) Or (PartyDetail.Rows(0)("C_R_STATE_ID").ToString.Trim.Length <= 0) Or (PartyDetail.Rows(0)("C_R_COUNTRY_ID").ToString.Trim.Length <= 0) Then
                                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                    Me.ToolTip1.Show("Donor(" & PartyDetail.Rows(0)("C_NAME") & ") Address Incomplete . . . !" & vbNewLine & "Mandatory: Address Line.1, City, District, State & Country...", Me.GridControl1, 0, Me.GridControl1.Height, 5000)
                                    Me.GridView1.Focus()
                                    Me.DialogResult = Windows.Forms.DialogResult.None
                                    Exit Sub
                                Else
                                    Me.ToolTip1.Hide(Me.GridControl1) ' fix 4731#note-3
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If

        'Location check 
        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
            For I As Integer = 0 To Me.GridView1.RowCount - 1
                If (Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper = "LAND & BUILDING") And Me.GridView1.GetRowCellValue(I, "Addition") = False Then
                    If Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString.Length > 0 Then ' Entries With Profile Reference 
                        Dim PROFILE_TABLE As DataTable = ProfilesData(I)  'jrnl_Item.GetReferenceData(Me.GridView1.GetRowCellValue(I, "Item_Profile"), Me.GridView1.GetRowCellValue(I, "Item_ID"), Me.GridView1.GetRowCellValue(I, "PartyID").ToString(), Me.xMID.Text, Me.Tag, Me.GridView1.GetRowCellValue(I, "CrossRefID"))
                        If PROFILE_TABLE Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        ' If Base.AllowMultiuser() Then
                        Dim MessageOfComparision As String = ""
                        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then
                            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) <= Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                                MessageOfComparision = "V a l u e   f o r   s e l e c t e d   p r o p e r t y ( " & PROFILE_TABLE.Rows(0)("Item") & " )  b e c o m e s   l e s s   t h a n   o r  e q u a l  t o  0 !!"
                            End If
                        Else
                            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) <= 0) Then ' Value remaining is less then the amount demanded for deduction 
                                MessageOfComparision = "V a l u e   f o r   s e l e c t e d   p r o p e r t y ( " & PROFILE_TABLE.Rows(0)("Item") & " )  b e c o m e s   l e s s   t h a n   o r  e q u a l  t o  0 !!"
                            End If
                        End If
                        If MessageOfComparision.Length > 0 Then
                            Dim Msg As String = FindLocationUsage(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString, True, True)
                            If Msg.Length > 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(MessageOfComparision & vbNewLine & vbNewLine & Msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'Me.DialogResult = Windows.Forms.DialogResult.Retry
                                'FormClosingEnable = False : Me.Close()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End If

        'Deletion Related cases
        If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            For I As Integer = 0 To Me.GridView1.RowCount - 1 ' Bugs #4902, #4903, #4904 fixed
                If (Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper <> "NOT APPLICABLE" Or Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING") And Me.GridView1.GetRowCellValue(I, "Addition") = False Then 'Allows Profile or Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                    If Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString.Length > 0 Then ' Entries With Profile Reference 
                        Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                        Dim PROFILE_TABLE As DataTable = ProfilesData(I)  'jrnl_Item.GetReferenceData(Me.GridView1.GetRowCellValue(I, "Item_Profile"), Me.GridView1.GetRowCellValue(I, "Item_ID"), Me.GridView1.GetRowCellValue(I, "PartyID").ToString(), Me.xMID.Text, Me.Tag, Me.GridView1.GetRowCellValue(I, "CrossRefID"))
                        If PROFILE_TABLE Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If

                        'Negative Balance on Deletion 
                        'Dim MessageOfComparision As String = ""
                        'Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile")
                        '    Case "GOLD", "SILVER"
                        '        Bug #5071 fixed
                        '        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "DEBIT" Then 'Addition OF QTY
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Weight")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()))) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                        '                MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the qty/weight deducted due to deletion of addition !!"
                        '            End If
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit")))) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value debited !!"
                        '            End If
                        '        Else
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Weight")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())) < 0) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                        '                MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than the 0!!"
                        '            End If
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                        '            End If
                        '        End If
                        '    Case "OTHER ASSETS"
                        '        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF QTY
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Qty")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()))) Then ' Weight/qty remaining is less then the weight/qty demanded for deduction 
                        '                MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less then the qty/weight entered for deduction !!"
                        '            End If
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value credited !!"
                        '            End If
                        '        Else
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Qty")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())) < 0) Then ' Weight/qty remaining is less then 0
                        '                MessageOfComparision = "Qty/Weight remaining  for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                        '            End If
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                        '                MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than the Value credited !!"
                        '            End If
                        '        End If
                        '    Case "VEHICLES", "LIVESTOCK", "LAND & BUILDING", "ADVANCES", "OTHER DEPOSITS", "WIP"
                        '        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF VALUE
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Current Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " is less than the Value credited !!"
                        '            End If
                        '        Else
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                        '                MessageOfComparision = "Adjusted Value for selected " & Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToLower & " becomes less than 0 !!"
                        '            End If
                        '        End If
                        '    Case "OTHER LIABILITIES"
                        '        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "DEBIT" Then 'DEDUCTION OF VALUE
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit")))) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Current Value for selected liability is less than the Value debited !!"
                        '            End If
                        '        Else
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit"))) < 0) Then ' Value remaining is less then 0
                        '                MessageOfComparision = "Adjusted Value for selected liability becomes less than 0 !!"
                        '            End If
                        '        End If
                        '    Case "OPENING"
                        '        Exit Select
                        '    Case Else 'Construction Items
                        '        If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "CREDIT" Then 'DEDUCTION OF VALUE
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) < Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Credit")))) Then ' Value remaining is less then the amount demanded for deduction 
                        '                MessageOfComparision = "Current Value for referred property is less than the Value credited !!"
                        '            End If
                        '        Else
                        '            If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) + Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit"))) < 0) Then ' Value remaining is less then 0
                        '                MessageOfComparision = "Adjusted Value for referred property becomes less than 0 !!"
                        '            End If
                        '        End If
                        'End Select

                        'If MessageOfComparision.Length > 0 Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.CustomChanges(MessageOfComparision, Me.GridView1.GetRowCellValue(I, "Item_Profile")), "Action not allowed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Me.DialogResult = Windows.Forms.DialogResult.Retry
                        '    FormClosingEnable = False : Me.Close()
                        '    Exit Sub
                        'End If

                        'Property Value Reduced to 0 on deletion
                        Dim MsgLoc As String = ""
                        If (Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper = "LAND & BUILDING") And Me.GridView1.GetRowCellValue(I, "Addition") = False Then
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type") = "DEBIT" Then 'DEDUCTION OF VALUE
                                If (Convert.ToDecimal(PROFILE_TABLE.Rows(0)("Curr Value")) = Convert.ToDecimal(Val(Me.GridView1.GetRowCellValue(I, "Debit")))) Then ' 
                                    MsgLoc = FindLocationUsage(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString, False, True)
                                End If
                            End If
                        End If
                        If MsgLoc.Length > 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show(MsgLoc, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    End If
                End If
            Next
        End If

        '--------------------------------- // End Dependencies //-------------------------------------



        Dim ReferenceRepetitionCheck As ArrayList = New ArrayList
        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
            For I As Integer = 0 To Me.GridView1.RowCount - 1 ' Bugs #4902, #4903, #4904 fixed
                If (Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper <> "NOT APPLICABLE" Or Me.GridView1.GetRowCellValue(I, "Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING") And Me.GridView1.GetRowCellValue(I, "Addition") = False Then 'Allows Profile or Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                    If Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString.Length > 0 Then ' Entries With Profile Reference 
                        'Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                        'Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData(Me.GridView1.GetRowCellValue(I, "Item_Profile"), Me.GridView1.GetRowCellValue(I, "Item_ID"), Me.GridView1.GetRowCellValue(I, "PartyID").ToString(), Me.xMID.Text, Me.Tag, Me.GridView1.GetRowCellValue(I, "CrossRefID"))
                        'ProfilesData.Add(I, PROF_TABLE) ' adding in dictionary control to be used in multiuser code too
                        Dim PROF_TABLE As DataTable = ProfilesData(I)
                        If Me.Tag <> Common_Lib.Common.Navigation_Mode._Delete Then
                            Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile")
                                Case "GOLD", "SILVER", "OTHER ASSETS", "VEHICLES", "LIVESTOCK", "LAND & BUILDING" 'http://pm.bkinfo.in/issues/5124#note-7 pt.1
                                    Dim inparam As Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate = New Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate()
                                    inparam.Creation_Date = DateValue(IIf(IsDBNull(PROF_TABLE.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, PROF_TABLE.Rows(0)("REF_CREATION_DATE")))
                                    inparam.Asset_RecID = PROF_TABLE.Rows(0)("REC_ID")
                                    inparam.YearID = Base._open_Year_ID
                                    inparam.Tr_M_ID = Me.xMID.Text
                                    Dim MxDate As Date = DateValue(Base._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam))
                                    If MxDate = Nothing Then
                                        Base.HandleDBError_OnNothingReturned()
                                        Exit Sub
                                    End If
                                    If DateValue(Me.Txt_V_Date.Text) < MxDate Then
                                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                        Me.ToolTip1.Show("V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   p r e v i o u s    t r a n s a c t i o n    o n   s a m e   a s s e t   d a t e d  " & MxDate.ToLongDateString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                                        Me.Txt_V_Date.Focus()
                                        Me.DialogResult = Windows.Forms.DialogResult.None
                                        Exit Sub
                                    Else
                                        Me.ToolTip1.Hide(Me.Txt_V_Date)
                                    End If
                                Case "ADVANCES", "OTHER DEPOSITS", "OTHER LIABILITIES" ''  'http://pm.bkinfo.in/issues/5124#note-7 pt.6, , "WIP"
                                    Dim CreationDate As Date = DateValue(IIf(IsDBNull(PROF_TABLE.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, PROF_TABLE.Rows(0)("REF_CREATION_DATE")))
                                    If DateValue(Me.Txt_V_Date.Text) < CreationDate Then
                                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                                        Me.ToolTip1.Show("C u r r e n t   R e f e r e n c e   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   C r e a t i o n   V o u c h e r   d a t e d  " & CreationDate.ToLongDateString() & " for " & Me.GridView1.GetRowCellValue(I, "Item_Profile") & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                                        Me.Txt_V_Date.Focus()
                                        Me.DialogResult = Windows.Forms.DialogResult.None
                                        Exit Sub
                                    Else
                                        Me.ToolTip1.Hide(Me.Txt_V_Date)
                                    End If
                            End Select
                            'Bug #5684
                            If Me.GridView1.GetRowCellValue(I, "Item_Profile") <> "NOT APPLICABLE" Then 'Repetition check skipped for Constt Entries 
                                If ReferenceRepetitionCheck.Contains(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString) Then
                                    Dim Reference As String = ""
                                    Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile")
                                        Case "GOLD", "SILVER", "OTHER ASSETS", "LIVESTOCK", "LAND & BUILDING", "ADVANCES", "OTHER DEPOSITS"
                                            Reference = PROF_TABLE.Rows(0)("Item").ToString
                                        Case "VEHICLES"
                                            Reference = PROF_TABLE.Rows(0)("Vehicle").ToString
                                        Case "WIP"
                                            Reference = PROF_TABLE.Rows(0)("Reference").ToString
                                        Case "OTHER LIABILITIES"
                                            Reference = "Org. Value : " & PROF_TABLE.Rows(0)("Org Value").ToString
                                    End Select
                                    DevExpress.XtraEditors.XtraMessageBox.Show("S o r r y  !  E a c h   P r o f i l e   R e c o r d    C a n   b e   r e f e r r e d   O n l y   O n c e   i n   a   J o u r n a l   V o u c h e r  ." & vbNewLine & vbNewLine & "Please remove duplicate references to same " & Me.GridView1.GetRowCellValue(I, "Item_Profile") & " Record (" & Reference & ")", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    ReferenceRepetitionCheck.Add(Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If



        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._Payment_DBOps.GetStatusByMID(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherJournal = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherJournal
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            'Master Record 
            Me.xMID.Text = Guid.NewGuid().ToString()
            Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucheJournal = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucheJournal()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Payment
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
            InMInfo.SubTotal = Val(Me.Txt_DrTotal.Text)
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            'If Not Base._Journal_voucher_DBOps.InsertMasterInfo(InMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMInfo
            Dim Ref_Rec_ID As String = ""

            Dim cnt As Integer = 0
            Dim InAdv(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances
            Dim InLiab(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities
            Dim InDep(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits
            Dim Insert(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal
            Dim InsertPurpose(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal
            Dim InsertGS(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver
            Dim InsertAssets(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets
            Dim InsertLivestock(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock
            Dim InsertVehicles(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles
            Dim InsertProperty(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding
            Dim InsertReferencesWIP(DT.Rows.Count) As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile

            For I As Integer = 0 To Me.GridView1.RowCount - 1 'Add Txn
                Dim Cross_Ref_ID As String = ""
                If Me.GridView1.GetRowCellValue(I, "LB_REC_ID").ToString.Length > 0 And Not Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then 'Bug #5637
                    Cross_Ref_ID = Me.GridView1.GetRowCellValue(I, "LB_REC_ID")
                    If Base.AllowMultiuser() Then ' Ref A/AE in AO33
                        If Not Cross_Ref_ID Is Nothing Then
                            If Cross_Ref_ID.Length > 0 Then
                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID) 'checks if the referred property for constt items has been sold 
                                If Not SaleRecord Is Nothing Then
                                    If SaleRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, Cross_Ref_ID) 'checks if the referred property for constt items has been transferred 
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                'check for new additions
                If Convert.ToBoolean(Me.GridView1.GetRowCellValue(I, "Addition")) Then
                    Ref_Rec_ID = Guid.NewGuid.ToString
                    Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString().ToUpper
                        Case "ADVANCES"
                            Dim _Adv As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances = New Common_Lib.RealTimeService.Parameter_InsertTRID_Advances
                            If IsDate(Txt_V_Date.Text) Then _Adv.AdvanceDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else _Adv.AdvanceDate = Txt_V_Date.Text
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then _Adv.Amount = 0 Else _Adv.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then _Adv.Amount = 0 Else _Adv.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            _Adv.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            '_Adv.openYearID = Base._open_Year_ID ' Removed and used from basicParam instead 
                            _Adv.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            '_Adv.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            _Adv.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            _Adv.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal
                            _Adv.Status_Action = Status_Action
                            _Adv.TxnID = Me.xMID.Text
                            _Adv.RecID = Ref_Rec_ID

                            InAdv(cnt) = _Adv
                            'If Not Base._Journal_voucher_DBOps.InsertAdvances(_Adv) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Base._Journal_voucher_DBOps.Delete(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeletePurpose(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeleteMaster(Me.xMID.Text)
                            '    Exit Sub
                            'End If
                        Case "OTHER LIABILITIES"
                            Dim _Liab As Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities = New Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then _Liab.Amount = 0 Else _Liab.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then _Liab.Amount = 0 Else _Liab.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            _Liab.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            If IsDate(Txt_V_Date.Text) Then _Liab.LiabilityDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else _Liab.LiabilityDate = Txt_V_Date.Text
                            '_Liab.openYearID = Base._open_Year_ID ' Removed and used from basicParam instead 
                            _Liab.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            '_Liab.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            _Liab.RecID = Ref_Rec_ID
                            _Liab.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            _Liab.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal
                            _Liab.Status_Action = Status_Action
                            _Liab.TxnID = Me.xMID.Text

                            InLiab(cnt) = _Liab

                            'If Not Base._Journal_voucher_DBOps.InsertLiability(_Liab) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Base._Journal_voucher_DBOps.Delete(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeletePurpose(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeleteMaster(Me.xMID.Text)
                            '    Exit Sub
                            'End If
                        Case "OTHER DEPOSITS"
                            Dim InDept As Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits = New Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits
                            InDept.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            InDept.AgainstInsurance = "NO"
                            InDept.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            InDept.InsCompanyMiscID = ""
                            If IsDate(Txt_V_Date.Text) Then InDept.DepositDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InDept.DepositDate = Txt_V_Date.Text
                            InDept.DepositPeriod = 0
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InDept.Amount = 0 Else InDept.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InDept.Amount = 0 Else InDept.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InDept.InterestRate = 0
                            ' InDept.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            InDept.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            InDept.TxnID = Me.xMID.Text
                            InDept.Status_Action = Status_Action
                            InDept.RecID = Ref_Rec_ID

                            InDep(cnt) = InDept
                            'If Not Base._Journal_voucher_DBOps.InsertDeposits(InDept) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Base._Journal_voucher_DBOps.Delete(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeletePurpose(Me.xMID.Text)
                            '    Base._Journal_voucher_DBOps.DeleteMaster(Me.xMID.Text)
                            '    Exit Sub
                            'End If
                        Case "GOLD", "SILVER"
                            Dim InGS As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver()
                            InGS.Type = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_Profile")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_Profile"))
                            InGS.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InGS.DescMiscID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "GS_DESC_MISC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "GS_DESC_MISC_ID"))
                            InGS.Weight = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "GS_ITEM_WEIGHT")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "GS_ITEM_WEIGHT")))
                            InGS.LocationID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InGS.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InGS.TxnID = Me.xMID.Text
                            InGS.TxnSrno = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, CInt(Me.GridView1.GetRowCellValue(I, "Sr.")))
                            'InGS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Amount"))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InGS.Amount = 0 Else InGS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InGS.Amount = 0 Else InGS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InGS.Status_Action = Status_Action
                            InGS.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._GoldSilverDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertGS(cnt) = InGS

                        Case "OTHER ASSETS"
                            Dim InAsset As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                            InAsset.AssetType = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_TYPE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_TYPE"))
                            InAsset.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InAsset.Make = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_MAKE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_MAKE"))
                            InAsset.Model = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_MODEL")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_MODEL"))
                            InAsset.SrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_SERIAL_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_SERIAL_NO"))
                            InAsset.Rate = 0 'IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Rate")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Rate")))

                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))) Then InAsset.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))).ToString(Base._Server_Date_Format_Short) Else InAsset.PurchaseDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))
                            'InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                            'InAsset.PurchaseAmount = IIf(InAsset.AssetType.ToUpper = "ASSET", IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount"))), 0) 'http://pm.bkinfo.in/issues/5345#note-12
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InAsset.InsAmount = 0 Else InAsset.InsAmount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InAsset.InsAmount = 0 Else InAsset.InsAmount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InAsset.PurchaseAmount = IIf(InAsset.AssetType.ToUpper = "ASSET", InAsset.InsAmount, 0)
                            InAsset.Warranty = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_WARRANTY")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "AI_WARRANTY")))
                            InAsset.Quantity = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Qty.")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Qty.")))
                            InAsset.LocationId = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InAsset.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InAsset.TxnID = Me.xMID.Text
                            InAsset.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            InAsset.Status_Action = Status_Action
                            InAsset.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._AssetDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertAssets(cnt) = InAsset
                        Case "LIVESTOCK"
                            Dim InLS As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock()
                            InLS.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InLS.Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_NAME"))
                            InLS.Year = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_BIRTH_YEAR")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_BIRTH_YEAR"))
                            ' InLS.Amount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InLS.Amount = 0 Else InLS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InLS.Amount = 0 Else InLS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InLS.Insurance = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INSURANCE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INSURANCE"))
                            InLS.InsuranceID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INSURANCE_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INSURANCE_ID"))
                            InLS.PolicyNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_POLICY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_POLICY_NO"))
                            InLS.InsAmount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_AMT")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "LS_INS_AMT")))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))) Then InLS.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))).ToString(Base._Server_Date_Format_Short) Else InLS.InsuranceDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))
                            'InParam.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                            InLS.LocationID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InLS.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InLS.TxnID = Me.xMID.Text
                            InLS.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, CInt(Me.GridView1.GetRowCellValue(I, "Sr.")))
                            InLS.Status_Action = Status_Action
                            InLS.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._LiveStockDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertLivestock(cnt) = InLS
                        Case "VEHICLES"
                            Dim InVeh As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles()
                            InVeh.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InVeh.Make = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_MAKE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_MAKE"))
                            InVeh.Model = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_MODEL")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_MODEL"))
                            InVeh.Reg_No_Pattern = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_NO_PATTERN")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_NO_PATTERN"))
                            InVeh.Reg_No = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_NO"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))) Then InVeh.RegDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))).ToString(Base._Server_Date_Format_Short) Else InVeh.RegDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))
                            'InPms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                            InVeh.Ownership = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP"))
                            If Not Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID")) Then
                                InVeh.Ownership_AB_ID = IIf(Len(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID")) = 0, Nothing, Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID"))
                            End If
                            InVeh.Doc_RC_Book = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_RC_BOOK")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_RC_BOOK"))
                            InVeh.Doc_Affidavit = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_AFFIDAVIT")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_AFFIDAVIT"))
                            InVeh.Doc_Will = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_WILL")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_WILL"))
                            InVeh.Doc_TRF_Letter = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_TRF_LETTER")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_TRF_LETTER"))
                            InVeh.DOC_FU_Letter = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_FU_LETTER")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_FU_LETTER"))
                            InVeh.Doc_Is_Others = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_OTHERS")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_OTHERS"))
                            InVeh.Doc_Others_Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_NAME"))
                            If IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID")) Then
                                InVeh.Insurance_ID = Nothing
                            ElseIf Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID") = Nothing Then
                                InVeh.Insurance_ID = Nothing
                            ElseIf Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID").ToString.Length = 0 Then
                                InVeh.Insurance_ID = Nothing
                            Else
                                InVeh.Insurance_ID = Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID")
                            End If
                            InVeh.Ins_Policy_No = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_POLICY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_POLICY_NO"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))) Then InVeh.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))).ToString(Base._Server_Date_Format_Short) Else InVeh.Ins_Expiry_Date = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))
                            'InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                            InVeh.Location_ID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InVeh.Other_Details = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InVeh.TxnID = Me.xMID.Text
                            InVeh.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            InVeh.Amount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InVeh.Amount = 0 Else InVeh.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InVeh.Amount = 0 Else InVeh.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InVeh.Status_Action = Status_Action
                            InVeh.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._VehicleDBOps.Insert(InPms) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertVehicles(cnt) = InVeh
                        Case "LAND & BUILDING"
                            'Dim P_Date As DateTime = Nothing : Dim P_Date_Str As String = "" : If IsDate(XRow("LB_PAID_DATE")) Then : P_Date = XRow("LB_PAID_DATE") : P_Date_Str = "#" & P_Date.ToString(Base._Date_Format_Short) & "#" : Else : P_Date_Str = " NULL " : End If
                            'Dim From_Date As DateTime = Nothing : Dim From_Date_Str As String = "" : If IsDate(XRow("LB_PERIOD_FROM")) Then : From_Date = XRow("LB_PERIOD_FROM") : From_Date_Str = "#" & From_Date.ToString(Base._Date_Format_Short) & "#" : Else : From_Date_Str = " NULL " : End If
                            'Dim To_Date As DateTime = Nothing : Dim To_Date_Str As String = "" : If IsDate(XRow("LB_PERIOD_TO")) Then : To_Date = XRow("LB_PERIOD_TO") : To_Date_Str = "#" & To_Date.ToString(Base._Date_Format_Short) & "#" : Else : To_Date_Str = " NULL " : End If
                            Dim PartyID As String = "NULL"
                            If Not Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP_PARTY_ID").ToString = "NULL" Then PartyID = "'" & Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP_PARTY_ID") & "'"
                            Dim InLB As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding()
                            InLB.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InLB.PropertyType = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_TYPE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_TYPE"))
                            InLB.Category = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_CATEGORY")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_CATEGORY"))
                            InLB.Use = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_USE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_USE"))
                            InLB.Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_NAME")), Nothing, Trim(Me.GridView1.GetRowCellValue(I, "LB_PRO_NAME")))
                            InLB.Address = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_ADDRESS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_ADDRESS"))
                            InLB.LB_Add1 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS1")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS1"))
                            InLB.LB_Add2 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS2")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS2"))
                            InLB.LB_Add3 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS3")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS3"))
                            InLB.LB_Add4 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS4")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS4"))
                            InLB.LB_CityID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CITY_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CITY_ID"))
                            InLB.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                            InLB.LB_DisttID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DISTRICT_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DISTRICT_ID"))
                            InLB.LB_PinCode = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PINCODE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PINCODE"))
                            InLB.LB_StateID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_STATE_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_STATE_ID"))
                            InLB.Ownership = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP"))
                            InLB.Owner_Party_ID = PartyID
                            InLB.SurveyNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_SURVEY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_SURVEY_NO"))
                            InLB.TotalArea = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_TOT_P_AREA")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_TOT_P_AREA"))
                            InLB.ConstructedArea = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CON_AREA")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CON_AREA"))
                            InLB.ConstructionYear = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CON_YEAR")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CON_YEAR"))
                            InLB.RCCRoof = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_RCC_ROOF")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_RCC_ROOF"))
                            InLB.DepositAmount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DEPOSIT_AMT")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DEPOSIT_AMT"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))) Then InLB.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))).ToString(Base._Server_Date_Format_Short) Else InLB.PaymentDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))
                            'InParam.PaymentDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PAID_DATE"))
                            InLB.MonthlyRent = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_MONTH_RENT")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_MONTH_RENT"))
                            InLB.MonthlyOtherExpenses = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_MONTH_O_PAYMENTS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_MONTH_O_PAYMENTS"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))) Then InLB.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))).ToString(Base._Server_Date_Format_Short) Else InLB.PeriodFrom = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))
                            'InParam.PeriodFrom = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PERIOD_FROM"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))) Then InLB.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))).ToString(Base._Server_Date_Format_Short) Else InLB.PeriodTo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))
                            'InParam.PeriodTo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PERIOD_TO"))
                            InLB.OtherDocs = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DOC_OTHERS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DOC_OTHERS"))
                            InLB.DocNames = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DOC_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DOC_NAME"))
                            'InLB.Value = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InLB.Value = 0 Else InLB.Value = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InLB.Value = 0 Else InLB.Value = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InLB.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_OTHER_DETAIL")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_OTHER_DETAIL"))
                            InLB.MasterID = Me.xMID.Text
                            InLB.SrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            InLB.Status_Action = Status_Action
                            InLB.RecID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_REC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_REC_ID"))

                            'If Not Base._L_B_DBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If

                            'EXTENSIONS 
                            Dim ExtInfo(LB_EXTENDED_PROPERTY_TABLE.Rows.Count - 1) As Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding
                            Dim cnt1 As Integer = 0
                            If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                For Each _Ext_Row As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                    If _Ext_Row("LB_REC_ID") = Me.GridView1.GetRowCellValue(I, "LB_REC_ID") Then
                                        Dim InEInfo As Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding()
                                        InEInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                        InEInfo.SrNo = IIf(IsDBNull(_Ext_Row("LB_SR_NO")), Nothing, _Ext_Row("LB_SR_NO").ToString)
                                        InEInfo.Inst_ID = IIf(IsDBNull(_Ext_Row("LB_INS_ID")), Nothing, _Ext_Row("LB_INS_ID").ToString)
                                        InEInfo.TotalArea = Val(_Ext_Row("LB_TOT_P_AREA").ToString)
                                        InEInfo.ConstructedArea = Val(_Ext_Row("LB_CON_AREA").ToString)
                                        InEInfo.ConYear = IIf(IsDBNull(_Ext_Row("LB_CON_YEAR")), Nothing, _Ext_Row("LB_CON_YEAR").ToString)
                                        If IsDate(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))) Then InEInfo.MOU_Date = Convert.ToDateTime(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))).ToString(Base._Server_Date_Format_Short) Else InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                        'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                        InEInfo.Value = Val(_Ext_Row("LB_VALUE").ToString)
                                        InEInfo.OtherDetails = IIf(IsDBNull(_Ext_Row("LB_OTHER_DETAIL")), Nothing, _Ext_Row("LB_OTHER_DETAIL").ToString)
                                        InEInfo.Status_Action = Status_Action
                                        InEInfo.RecID = System.Guid.NewGuid().ToString()

                                        'If Not Base._L_B_DBOps.InsertExtendedInfo(InEInfo) Then
                                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '    Exit Sub
                                        'End If
                                        ExtInfo(cnt1) = InEInfo
                                        cnt1 += 1
                                    End If
                                Next
                            End If
                            InLB.InsertExtInfo = ExtInfo
                            'DOCS
                            Dim DocInfo(LB_DOCS_ARRAY.Rows.Count - 1) As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding
                            cnt1 = 0
                            If Not LB_DOCS_ARRAY Is Nothing Then
                                For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                                    If _Ext_Row("LB_REC_ID") = Me.GridView1.GetRowCellValue(I, "LB_REC_ID") Then
                                        Dim InDocInfo As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding()
                                        InDocInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                        InDocInfo.Doc_Misc_ID = IIf(IsDBNull(_Ext_Row("LB_MISC_ID")), Nothing, _Ext_Row("LB_MISC_ID").ToString)
                                        InDocInfo.Status_Action = Status_Action
                                        InDocInfo.RecID = System.Guid.NewGuid().ToString()

                                        'If Not Base._L_B_DBOps.InsertDocumentsInfo(InDocInfo) Then
                                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '    Exit Sub
                                        'End If
                                        DocInfo(cnt1) = InDocInfo
                                        cnt1 += 1
                                    End If
                                Next
                            End If
                            InLB.InsertDocInfo = DocInfo

                            'Add Location
                            Dim InAssetLoc As Common_Lib.RealTimeService.Param_AssetLoc_Insert = New Common_Lib.RealTimeService.Param_AssetLoc_Insert()
                            InAssetLoc.name = Trim(InLB.Name)
                            InAssetLoc.OtherDetails = "Use Type: " & InLB.PropertyType
                            InAssetLoc.Status_Action = Status_Action
                            InAssetLoc.Match_LB_ID = InLB.RecID
                            InAssetLoc.Match_SP_ID = ""
                            'If Not Base._AssetLocDBOps.Insert(ClientScreen.Accounts_Voucher_Gift, InParam.Name, "Use Type: " & InParam.PropertyType, Status_Action, "", InParam.RecID) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InLB.param_InsertAssetLoc = InAssetLoc
                            InsertProperty(cnt) = InLB

                            '----------WIP References------------

                        Case "WIP"
                            If Not Me.GridView1.GetRowCellValue(I, "WIP_REF_TYPE") Is Nothing Then
                                If Me.GridView1.GetRowCellValue(I, "WIP_REF_TYPE") = "NEW" Then
                                    Dim InReference As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile = New Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile()
                                    InReference.LedID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_Led_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_Led_ID"))
                                    InReference.Reference = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "REFERENCE")), Nothing, Me.GridView1.GetRowCellValue(I, "REFERENCE"))
                                    ' InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "AMOUNT"))
                                    If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                        If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InReference.Amount = 0 Else InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                                    Else
                                        If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InReference.Amount = 0 Else InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                                    End If
                                    InReference.Status_Action = Status_Action
                                    InReference.TxnID = Me.xMID.Text
                                    InReference.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                                    InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment
                                    InsertReferencesWIP(cnt) = InReference
                                End If
                            End If
                    End Select
                End If

                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal = New Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Journal
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                InParam.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                InParam.Type = Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString()
                If InParam.Type.ToUpper = "DEBIT" Then InParam.Dr_Led_ID = Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() Else InParam.Dr_Led_ID = ""
                If InParam.Type.ToUpper = "CREDIT" Then InParam.Cr_Led_ID = Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() Else InParam.Cr_Led_ID = ""
                If InParam.Type.ToUpper = "DEBIT" Then
                    If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InParam.Amount = 0 Else InParam.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                Else
                    If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InParam.Amount = 0 Else InParam.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                End If
                InParam.Mode = "JOURNAL"
                If Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()) > 0 Then
                    InParam.Qty = Convert.ToDecimal(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())
                Else
                    InParam.Qty = 0
                End If
                InParam.Party1 = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                InParam.Reference = Me.Txt_Reference.Text
                If Not Convert.ToBoolean(Me.GridView1.GetRowCellValue(I, "Addition")) Then InParam.CrossRefID = Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString() Else InParam.CrossRefID = ""
                InParam.MasterTxnID = Me.xMID.Text
                InParam.SrNo = Me.GridView1.GetRowCellValue(I, "Sr.").ToString()
                InParam.Status_Action = Status_Action
                InParam.RecID = Guid.NewGuid.ToString()

                Insert(cnt) = InParam

                'If Not Base._Journal_voucher_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._Journal_voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._Journal_voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If

                'Add purpose
                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal()
                InPurpose.Amount = InParam.Amount
                InPurpose.PurposeID = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                InPurpose.RecID = Guid.NewGuid.ToString
                InPurpose.SrNo = Me.GridView1.GetRowCellValue(I, "Sr.").ToString()
                InPurpose.Status_Action = Status_Action
                InPurpose.TxnID = Me.xMID.Text

                InsertPurpose(cnt) = InPurpose

                'If Not Base._Journal_voucher_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._Journal_voucher_DBOps.Delete(Me.xMID.Text)
                '    Base._Journal_voucher_DBOps.DeletePurpose(Me.xMID.Text)
                '    Base._Journal_voucher_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                cnt += 1

            Next ' End Add Txn

            InNewParam.Insert = Insert
            InNewParam.InsertAdvances = InAdv
            InNewParam.InsertDeposits = InDep
            InNewParam.InsertLiabilities = InLiab
            InNewParam.InsertPurpose = InsertPurpose
            InNewParam.InsertAssets = InsertAssets
            InNewParam.InsertGS = InsertGS
            InNewParam.InsertLivestock = InsertLivestock
            InNewParam.InsertProperty = InsertProperty
            InNewParam.InsertVehicles = InsertVehicles
            InNewParam.InsertReferencesWIP = InsertReferencesWIP

            If Not Base._Journal_voucher_DBOps.InsertJournal_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim Message As String = "" : Dim IsLBIncluded As Boolean = False
        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherJournal = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherJournal
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            'Master Record 
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherJournal = New Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherJournal()
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.SubTotal = Val(Me.Txt_DrTotal.Text)
            'UpMInfo.Status_Action = Status_Action
            UpMInfo.RecID = Me.xMID.Text

            EditParam.param_UpdateMaster = UpMInfo

            EditParam.MID_Delete = Me.xMID.Text

            EditParam.MID_DeletePurpose = Me.xMID.Text

            EditParam.MID_DeleteAdvances = Me.xMID.Text

            EditParam.MID_DeleteLiabilities = Me.xMID.Text

            EditParam.MID_DeleteDeposits = Me.xMID.Text

            EditParam.MID_ReferenceDelete = Me.xMID.Text

            EditParam.MID_DeleteGS = Me.xMID.Text

            EditParam.MID_DeleteAssets = Me.xMID.Text

            EditParam.MID_DeleteLS = Me.xMID.Text

            EditParam.MID_DeleteVehicle = Me.xMID.Text

            Dim d1 As DataTable = Base._L_B_DBOps.GetIDsBytxnID(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)

            Dim DelExtInfo(d1.Rows.Count - 1) As String
            Dim DelDocInfo(d1.Rows.Count - 1) As String
            Dim DelByLB(d1.Rows.Count - 1) As String
            Dim DelComplexBuildings(d1.Rows.Count - 1) As String
            Dim ctr As Integer = 0
            For Each cRow As DataRow In d1.Rows
                DelComplexBuildings(ctr) = cRow(0)
                DelExtInfo(ctr) = cRow(0)
                DelDocInfo(ctr) = cRow(0)
                DelByLB(ctr) = cRow(0)
                ctr += 1
            Next
            EditParam.DeleteComplexBuilding = DelComplexBuildings
            EditParam.DeleteDocumentInfo = DelDocInfo
            EditParam.DeleteExtendedInfo = DelExtInfo
            EditParam.DeleteByLB = DelByLB

            EditParam.MID_DeleteLandB = Me.xMID.Text

            Dim cnt As Integer = 0
            Dim InAdvances(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances
            Dim InLiabilities(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities
            Dim InDeposits(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits
            Dim Insert(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal
            Dim InsertPurpose(Me.GridView1.RowCount - 1) As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal
            Dim InsertGS(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver
            Dim InsertAssets(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets
            Dim InsertLivestock(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock
            Dim InsertVehicles(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles
            Dim InsertProperty(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding
            Dim InsertReferencesWIP(DT.Rows.Count) As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile

            Dim Ref_Rec_ID As String = ""
            For I As Integer = 0 To Me.GridView1.RowCount - 1 'Edit Txn
                Dim Cross_Ref_ID As String = ""
                If Me.GridView1.GetRowCellValue(I, "LB_REC_ID").ToString.Length > 0 And Not Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                    Cross_Ref_ID = Me.GridView1.GetRowCellValue(I, "LB_REC_ID")
                    If Base.AllowMultiuser() Then
                        If Not Cross_Ref_ID Is Nothing Then
                            If Cross_Ref_ID.Length > 0 Then
                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID) 'checks if the referred property for constt items has been sold 
                                If Not SaleRecord Is Nothing Then
                                    If SaleRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, Cross_Ref_ID) 'checks if the referred property for constt items has been sold 
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                                        FormClosingEnable = False : Me.Close()
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If Me.GridView1.GetRowCellValue(I, "REF_REC_ID").ToString.Length > 0 Then
                    Cross_Ref_ID = "'" & Me.GridView1.GetRowCellValue(I, "REF_REC_ID") & "'"
                End If

                'Add 
                If Convert.ToBoolean(Me.GridView1.GetRowCellValue(I, "Addition")) Then
                    Ref_Rec_ID = Guid.NewGuid.ToString
                    Select Case Me.GridView1.GetRowCellValue(I, "Item_Profile").ToString().ToUpper
                        Case "ADVANCES"
                            Dim _Adv As Common_Lib.RealTimeService.Parameter_InsertTRID_Advances = New Common_Lib.RealTimeService.Parameter_InsertTRID_Advances
                            If IsDate(Txt_V_Date.Text) Then _Adv.AdvanceDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else _Adv.AdvanceDate = Txt_V_Date.Text
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then _Adv.Amount = 0 Else _Adv.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then _Adv.Amount = 0 Else _Adv.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            _Adv.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            '_Adv.openYearID = Base._open_Year_ID ' Removed and used from basicParam instead 
                            _Adv.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            _Adv.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            _Adv.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            _Adv.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal
                            _Adv.Status_Action = Status_Action
                            _Adv.TxnID = Me.xMID.Text
                            _Adv.RecID = Ref_Rec_ID

                            InAdvances(cnt) = _Adv

                            'If Not Base._Journal_voucher_DBOps.InsertAdvances(_Adv) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If

                        Case "OTHER LIABILITIES"
                            Dim _Liab As Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities = New Common_Lib.RealTimeService.Parameter_InsertTRID_Liabilities
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then _Liab.Amount = 0 Else _Liab.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then _Liab.Amount = 0 Else _Liab.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            _Liab.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            If IsDate(Txt_V_Date.Text) Then _Liab.LiabilityDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else _Liab.LiabilityDate = Txt_V_Date.Text
                            '_Liab.openYearID = Base._open_Year_ID ' Removed and used from basicParam instead 
                            _Liab.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            _Liab.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            _Liab.RecID = Ref_Rec_ID
                            _Liab.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            _Liab.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Journal
                            _Liab.Status_Action = Status_Action
                            _Liab.TxnID = Me.xMID.Text

                            InLiabilities(cnt) = _Liab
                            'If Not Base._Journal_voucher_DBOps.InsertLiability(_Liab) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                        Case "OTHER DEPOSITS"
                            Dim InDept As Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits = New Common_Lib.RealTimeService.Parameter_InsertTRID_Deposits
                            InDept.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                            InDept.AgainstInsurance = "NO"
                            InDept.PartyID = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                            InDept.InsCompanyMiscID = ""
                            If IsDate(Txt_V_Date.Text) Then InDept.DepositDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InDept.DepositDate = Txt_V_Date.Text
                            InDept.DepositPeriod = 0
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InDept.Amount = 0 Else InDept.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InDept.Amount = 0 Else InDept.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InDept.InterestRate = 0
                            InDept.Purpose = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                            InDept.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                            InDept.TxnID = Me.xMID.Text
                            InDept.Status_Action = Status_Action
                            InDept.RecID = Ref_Rec_ID

                            InDeposits(cnt) = InDept
                            'If Not Base._Journal_voucher_DBOps.InsertDeposits(InDept) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If

                        Case "GOLD", "SILVER"
                            Dim InGS As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver()
                            InGS.Type = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_Profile")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_Profile"))
                            InGS.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InGS.DescMiscID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "GS_DESC_MISC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "GS_DESC_MISC_ID"))
                            InGS.Weight = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "GS_ITEM_WEIGHT")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "GS_ITEM_WEIGHT")))
                            InGS.LocationID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InGS.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InGS.TxnID = Me.xMID.Text
                            InGS.TxnSrno = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, CInt(Me.GridView1.GetRowCellValue(I, "Sr.")))
                            ' InParam.Amount = Val(Me.GridView1.GetRowCellValue(I, "Amount"))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InGS.Amount = 0 Else InGS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InGS.Amount = 0 Else InGS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InGS.Status_Action = Status_Action
                            InGS.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._GoldSilverDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertGS(cnt) = InGS

                        Case "OTHER ASSETS"
                            Dim InAsset As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                            InAsset.AssetType = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_TYPE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_TYPE"))
                            InAsset.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InAsset.Make = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_MAKE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_MAKE"))
                            InAsset.Model = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_MODEL")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_MODEL"))
                            InAsset.SrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_SERIAL_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_SERIAL_NO"))
                            If IsDBNull(Me.GridView1.GetRowCellValue(I, "Rate")) Then
                                InAsset.Rate = Nothing
                            Else
                                InAsset.Rate = Val(Me.GridView1.GetRowCellValue(I, "Rate"))
                            End If
                            'InParam.InsAmount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))) Then InAsset.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))).ToString(Base._Server_Date_Format_Short) Else InAsset.PurchaseDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "AI_PUR_DATE"))
                            'InParam.PurchaseDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"AI_PUR_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"AI_PUR_DATE"))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InAsset.InsAmount = 0 Else InAsset.InsAmount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InAsset.InsAmount = 0 Else InAsset.InsAmount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InAsset.PurchaseAmount = IIf(InAsset.AssetType.ToUpper = "ASSET", InAsset.InsAmount, 0)
                            InAsset.Warranty = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "AI_WARRANTY")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "AI_WARRANTY")))
                            InAsset.Quantity = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Qty.")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Qty.")))
                            InAsset.LocationId = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InAsset.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InAsset.TxnID = Me.xMID.Text
                            InAsset.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            InAsset.Status_Action = Status_Action
                            InAsset.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._AssetDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertAssets(cnt) = InAsset

                        Case "LIVESTOCK"
                            Dim InLS As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock()
                            InLS.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InLS.Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_NAME"))
                            InLS.Year = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_BIRTH_YEAR")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_BIRTH_YEAR"))
                            ' InPms.Amount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InLS.Amount = 0 Else InLS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InLS.Amount = 0 Else InLS.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InLS.Insurance = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INSURANCE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INSURANCE"))
                            InLS.InsuranceID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INSURANCE_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INSURANCE_ID"))
                            InLS.PolicyNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_POLICY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_POLICY_NO"))
                            InLS.InsAmount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_AMT")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "LS_INS_AMT")))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))) Then InLS.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))).ToString(Base._Server_Date_Format_Short) Else InLS.InsuranceDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LS_INS_DATE"))
                            'InPms.InsuranceDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LS_INS_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"LS_INS_DATE"))
                            InLS.LocationID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InLS.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InLS.TxnID = Me.xMID.Text
                            InLS.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, CInt(Me.GridView1.GetRowCellValue(I, "Sr.")))
                            InLS.Status_Action = Status_Action
                            InLS.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._LiveStockDBOps.Insert(InPms) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertLivestock(cnt) = InLS

                        Case "VEHICLES"
                            Dim InVeh As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles()

                            InVeh.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InVeh.Make = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_MAKE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_MAKE"))
                            InVeh.Model = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_MODEL")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_MODEL"))
                            InVeh.Reg_No_Pattern = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_NO_PATTERN")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_NO_PATTERN"))
                            InVeh.Reg_No = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_NO"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))) Then InVeh.RegDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))).ToString(Base._Server_Date_Format_Short) Else InVeh.RegDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_REG_DATE"))
                            'InPrms.RegDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"VI_REG_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"VI_REG_DATE"))
                            InVeh.Ownership = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP"))
                            If Not Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID")) Then
                                InVeh.Ownership_AB_ID = IIf(Len(Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID")) = 0, Nothing, Me.GridView1.GetRowCellValue(I, "VI_OWNERSHIP_AB_ID"))
                            End If
                            InVeh.Doc_RC_Book = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_RC_BOOK")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_RC_BOOK"))
                            InVeh.Doc_Affidavit = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_AFFIDAVIT")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_AFFIDAVIT"))
                            InVeh.Doc_Will = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_WILL")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_WILL"))
                            InVeh.Doc_TRF_Letter = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_TRF_LETTER")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_TRF_LETTER"))
                            InVeh.DOC_FU_Letter = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_FU_LETTER")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_FU_LETTER"))
                            InVeh.Doc_Is_Others = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_OTHERS")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_OTHERS"))
                            InVeh.Doc_Others_Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_DOC_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_DOC_NAME"))
                            If IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID")) Then
                                InVeh.Insurance_ID = Nothing
                            ElseIf Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID") = Nothing Then
                                InVeh.Insurance_ID = Nothing
                            ElseIf Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID").ToString.Length = 0 Then
                                InVeh.Insurance_ID = Nothing
                            Else
                                InVeh.Insurance_ID = Me.GridView1.GetRowCellValue(I, "VI_INSURANCE_ID")
                            End If
                            InVeh.Ins_Policy_No = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_POLICY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_POLICY_NO"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))) Then InVeh.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))).ToString(Base._Server_Date_Format_Short) Else InVeh.Ins_Expiry_Date = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "VI_INS_EXPIRY_DATE"))
                            'InPrms.Ins_Expiry_Date = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"VI_INS_EXPIRY_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"VI_INS_EXPIRY_DATE"))
                            InVeh.Location_ID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LOC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LOC_ID"))
                            InVeh.Other_Details = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Remarks")), Nothing, Me.GridView1.GetRowCellValue(I, "Remarks"))
                            InVeh.TxnID = Me.xMID.Text
                            InVeh.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            'InPrms.Amount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InVeh.Amount = 0 Else InVeh.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InVeh.Amount = 0 Else InVeh.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InVeh.Status_Action = Status_Action
                            InVeh.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                            'If Not Base._VehicleDBOps.Insert(InPrms) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertVehicles(cnt) = InVeh

                        Case "LAND & BUILDING"
                            Dim PartyID As String = "NULL"
                            If Not Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP_PARTY_ID").ToString = "NULL" Then PartyID = "'" & Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP_PARTY_ID") & "'"
                            If Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP_PARTY_ID").ToString.Length = 0 Then PartyID = "NULL"
                            Dim InLB As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding()
                            InLB.ItemID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_ID"))
                            InLB.PropertyType = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_TYPE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_TYPE"))
                            InLB.Category = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_CATEGORY")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_CATEGORY"))
                            InLB.Use = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_USE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_USE"))
                            InLB.Name = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_NAME")), Nothing, Trim(Me.GridView1.GetRowCellValue(I, "LB_PRO_NAME")))
                            InLB.Address = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PRO_ADDRESS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PRO_ADDRESS"))
                            InLB.LB_Add1 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS1")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS1"))
                            InLB.LB_Add2 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS2")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS2"))
                            InLB.LB_Add3 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS3")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS3"))
                            InLB.LB_Add4 = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_ADDRESS4")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_ADDRESS4"))
                            InLB.LB_CityID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CITY_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CITY_ID"))
                            InLB.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                            InLB.LB_DisttID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DISTRICT_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DISTRICT_ID"))
                            InLB.LB_PinCode = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PINCODE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PINCODE"))
                            InLB.LB_StateID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_STATE_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_STATE_ID"))
                            InLB.Ownership = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_OWNERSHIP"))
                            InLB.Owner_Party_ID = PartyID
                            InLB.SurveyNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_SURVEY_NO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_SURVEY_NO"))
                            InLB.TotalArea = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_TOT_P_AREA")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_TOT_P_AREA"))
                            InLB.ConstructedArea = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CON_AREA")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CON_AREA"))
                            InLB.ConstructionYear = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_CON_YEAR")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_CON_YEAR"))
                            InLB.RCCRoof = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_RCC_ROOF")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_RCC_ROOF"))
                            InLB.DepositAmount = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DEPOSIT_AMT")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DEPOSIT_AMT"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))) Then InLB.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))).ToString(Base._Server_Date_Format_Short) Else InLB.PaymentDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PAID_DATE"))
                            'InParam.PaymentDate = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PAID_DATE")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PAID_DATE"))
                            InLB.MonthlyRent = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_MONTH_RENT")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_MONTH_RENT"))
                            InLB.MonthlyOtherExpenses = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_MONTH_O_PAYMENTS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_MONTH_O_PAYMENTS"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))) Then InLB.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))).ToString(Base._Server_Date_Format_Short) Else InLB.PeriodFrom = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_FROM"))
                            'InParam.PeriodFrom = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PERIOD_FROM")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PERIOD_FROM"))
                            If IsDate(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))) Then InLB.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))).ToString(Base._Server_Date_Format_Short) Else InLB.PeriodTo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_PERIOD_TO"))
                            'InParam.PeriodTo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I,"LB_PERIOD_TO")), Nothing, Me.GridView1.GetRowCellValue(I,"LB_PERIOD_TO"))
                            InLB.OtherDocs = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DOC_OTHERS")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DOC_OTHERS"))
                            InLB.DocNames = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_DOC_NAME")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_DOC_NAME"))
                            ' InParam.Value = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Amount")), Nothing, Val(Me.GridView1.GetRowCellValue(I, "Amount")))
                            If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InLB.Value = 0 Else InLB.Value = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                            Else
                                If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InLB.Value = 0 Else InLB.Value = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                            End If
                            InLB.OtherDetails = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_OTHER_DETAIL")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_OTHER_DETAIL"))
                            InLB.MasterID = Me.xMID.Text
                            InLB.SrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                            InLB.Status_Action = Status_Action
                            InLB.RecID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_REC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_REC_ID"))

                            'If Not Base._L_B_DBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If

                            Dim ExtInfo() As Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding
                            'EXTENSIONS 
                            Dim ctr1 As Integer = 0
                            If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                ExtInfo = New Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding(LB_EXTENDED_PROPERTY_TABLE.Rows.Count) {}
                                For Each _Ext_Row As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                    If _Ext_Row("LB_REC_ID") = Me.GridView1.GetRowCellValue(I, "LB_REC_ID") Then
                                        Dim InEInfo As Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding()
                                        InEInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                        InEInfo.SrNo = IIf(IsDBNull(_Ext_Row("LB_SR_NO")), Nothing, _Ext_Row("LB_SR_NO").ToString)
                                        InEInfo.Inst_ID = IIf(IsDBNull(_Ext_Row("LB_INS_ID")), Nothing, _Ext_Row("LB_INS_ID").ToString)
                                        InEInfo.TotalArea = Val(_Ext_Row("LB_TOT_P_AREA").ToString)
                                        InEInfo.ConstructedArea = Val(_Ext_Row("LB_CON_AREA").ToString)
                                        InEInfo.ConYear = IIf(IsDBNull(_Ext_Row("LB_CON_YEAR")), Nothing, _Ext_Row("LB_CON_YEAR").ToString)
                                        If IsDate(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))) Then InEInfo.MOU_Date = Convert.ToDateTime(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))).ToString(Base._Server_Date_Format_Short) Else InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                        'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                        InEInfo.Value = Val(_Ext_Row("LB_VALUE").ToString)
                                        InEInfo.OtherDetails = IIf(IsDBNull(_Ext_Row("LB_OTHER_DETAIL")), Nothing, _Ext_Row("LB_OTHER_DETAIL").ToString)
                                        InEInfo.Status_Action = Status_Action
                                        InEInfo.RecID = System.Guid.NewGuid().ToString()

                                        'If Not Base._L_B_DBOps.InsertExtendedInfo(InEInfo) Then
                                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '    Exit Sub
                                        'End If
                                        ExtInfo(ctr1) = InEInfo
                                        ctr1 += 1
                                    End If
                                Next
                                InLB.InsertExtInfo = ExtInfo
                            End If


                            'DOCS
                            Dim DocInfo() As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding
                            ctr1 = 0
                            If Not LB_DOCS_ARRAY Is Nothing Then
                                DocInfo = New Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding(LB_DOCS_ARRAY.Rows.Count) {}
                                For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                                    If _Ext_Row("LB_REC_ID") = Me.GridView1.GetRowCellValue(I, "LB_REC_ID") Then
                                        Dim InDInfo As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding()
                                        InDInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                        InDInfo.Doc_Misc_ID = IIf(IsDBNull(_Ext_Row("LB_MISC_ID")), Nothing, _Ext_Row("LB_MISC_ID").ToString)
                                        InDInfo.Status_Action = Status_Action
                                        InDInfo.RecID = System.Guid.NewGuid().ToString()

                                        'If Not Base._L_B_DBOps.InsertDocumentsInfo(InDInfo) Then
                                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '    Exit Sub
                                        'End If
                                        DocInfo(ctr1) = InDInfo
                                        ctr1 += 1
                                    End If
                                Next
                                InLB.InsertDocInfo = DocInfo
                            End If

                            Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "LB_REC_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "LB_REC_ID")))
                            If Locations Is Nothing Then
                                Base.HandleDBError_OnNothingReturned()
                                Exit Sub
                            End If
                            If Locations.Rows.Count > 0 Then
                                IsLBIncluded = True
                            End If

                            InsertProperty(cnt) = InLB

                            '----------WIP References------------
                        Case "WIP"
                            If Me.GridView1.GetRowCellValue(I, "WIP_REF_TYPE") = "NEW" Then
                                Dim InReference As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile = New Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile()
                                InReference.LedID = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Item_Led_ID")), Nothing, Me.GridView1.GetRowCellValue(I, "Item_Led_ID"))
                                InReference.Reference = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "REFERENCE")), Nothing, Me.GridView1.GetRowCellValue(I, "REFERENCE"))
                                InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "AMOUNT"))
                                If Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString() = "DEBIT" Then
                                    If Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing Then InReference.Amount = 0 Else InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
                                Else
                                    If Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing Then InReference.Amount = 0 Else InReference.Amount = Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
                                End If
                                InReference.Status_Action = Status_Action
                                InReference.TxnID = Me.xMID.Text
                                InReference.TxnSrNo = IIf(IsDBNull(Me.GridView1.GetRowCellValue(I, "Sr.")), Nothing, Me.GridView1.GetRowCellValue(I, "Sr."))
                                InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment
                                InsertReferencesWIP(cnt) = InReference
                            End If

                    End Select
                End If
                'End Addition

                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal = New Common_Lib.RealTimeService.Parameter_Insert_VoucherJournal()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Journal
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                InParam.ItemID = Me.GridView1.GetRowCellValue(I, "Item_ID").ToString()
                InParam.Type = Me.GridView1.GetRowCellValue(I, "Trans_Type").ToString()
                If InParam.Type.ToUpper = "DEBIT" Then InParam.Dr_Led_ID = Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() Else InParam.Dr_Led_ID = ""
                If InParam.Type.ToUpper = "CREDIT" Then InParam.Cr_Led_ID = Me.GridView1.GetRowCellValue(I, "Item_Led_ID").ToString() Else InParam.Cr_Led_ID = ""
                If InParam.Type.ToUpper = "DEBIT" Then InParam.Amount = IIf(Me.GridView1.GetRowCellValue(I, "Debit") Is Nothing, 0, Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())) Else InParam.Amount = IIf(Me.GridView1.GetRowCellValue(I, "Credit") Is Nothing, 0, Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString()))
                InParam.Mode = "JOURNAL"
                If Val(Me.GridView1.GetRowCellValue(I, "Qty.").ToString()) > 0 Then
                    InParam.Qty = Convert.ToDecimal(Me.GridView1.GetRowCellValue(I, "Qty.").ToString())
                Else
                    InParam.Qty = 0
                End If
                InParam.Party1 = Me.GridView1.GetRowCellValue(I, "PartyID").ToString()
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = Me.GridView1.GetRowCellValue(I, "Remarks").ToString()
                InParam.Reference = Me.Txt_Reference.Text
                If Not Convert.ToBoolean(Me.GridView1.GetRowCellValue(I, "Addition")) Then InParam.CrossRefID = Me.GridView1.GetRowCellValue(I, "CrossRefID").ToString() Else InParam.CrossRefID = ""
                InParam.MasterTxnID = Me.xMID.Text
                InParam.SrNo = Me.GridView1.GetRowCellValue(I, "Sr.").ToString()
                InParam.Status_Action = Status_Action
                InParam.RecID = Guid.NewGuid.ToString()

                'If Not Base._Journal_voucher_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                Insert(cnt) = InParam

                'Add purpose
                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherJournal()
                InPurpose.Amount = InParam.Amount
                InPurpose.PurposeID = Me.GridView1.GetRowCellValue(I, "Pur_ID").ToString()
                InPurpose.RecID = Guid.NewGuid.ToString
                InPurpose.SrNo = Me.GridView1.GetRowCellValue(I, "Sr.").ToString()
                InPurpose.Status_Action = Status_Action
                InPurpose.TxnID = Me.xMID.Text

                InsertPurpose(cnt) = InPurpose

                'If Not Base._Journal_voucher_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                cnt += 1
            Next ' End Add Txn

            EditParam.InsertAdvances = InAdvances
            EditParam.InsertDeposits = InDeposits
            EditParam.InsertLiabilities = InLiabilities
            EditParam.Insert = Insert
            EditParam.InsertPurpose = InsertPurpose
            EditParam.InsertAssets = InsertAssets
            EditParam.InsertGS = InsertGS
            EditParam.InsertLivestock = InsertLivestock
            EditParam.InsertProperty = InsertProperty
            EditParam.InsertVehicles = InsertVehicles
            EditParam.InsertReferencesWIP = InsertReferencesWIP

            If Not Base._Journal_voucher_DBOps.UpdateJournal_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If IsLBIncluded Then
                Message = vbNewLine & vbNewLine & "No Subsequent Changes have been made in Location(s) mapped to the property/properties mentioned in the current voucher." & vbNewLine & "User may make the required changes manually from Profile - > Core - > Locations."
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess & Message, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherJournal = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherJournal
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE

            For Each XRow In DT.Rows
                If XRow("Item_Profile") = "LAND & BUILDING" Or XRow("Item_Profile") = "OTHER ASSETS" Or (XRow("ITEM_VOUCHER_TYPE").Trim.ToUpper = "LAND & BUILDING" And Not XRow("Item_Profile").ToUpper = "LAND & BUILDING") Then
                    If Base.IsInsuranceAudited() Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s   C a n n o t   b e   D e l e t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            Next

            Dim LB_REC_ID As DataTable = Base._L_B_DBOps.GetIDsBytxnID(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
            Dim TblRecID As DataTable = LB_REC_ID.Copy
            'check any L&B Expenses done on basis of requested deletion entries 
            For Each _RECROW As DataRow In TblRecID.Rows
                Dim Dependent_payments As DataTable = Base._Payment_DBOps.GetTxnDetailsByRefID(_RECROW(0))
                If Dependent_payments.Rows.Count > 0 Then
                    Dim TrDate As DateTime = Convert.ToDateTime(Dependent_payments.Rows(0)("TR_Date"))
                    DevExpress.XtraEditors.XtraMessageBox.Show("A Construction Expense Entry of Rs." & Dependent_payments.Rows(0)("TR_AMOUNT") & " with date " & TrDate.ToString("dd-MMM-yyy") & " is dependednt on this voucher. Please delete that entry first!!", Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next

            DelParam.MID_Delete = Me.xMID.Text
            DelParam.MID_DeleteGS = Me.xMID.Text
            DelParam.MID_DeleteAssets = Me.xMID.Text
            DelParam.MID_DeleteLS = Me.xMID.Text
            DelParam.MID_DeleteVehicle = Me.xMID.Text
            DelParam.MID_ReferenceDelete = xMID.Text

            'Get Rec ID for Curr TxnMaster ID
            Dim DelComplexBuildings(TblRecID.Rows.Count) As String
            Dim DelExtInfo(TblRecID.Rows.Count) As String
            Dim DelDocInfo(TblRecID.Rows.Count) As String
            Dim DelByLB(TblRecID.Rows.Count) As String
            Dim ctr1 As Integer = 1
            For Each _RECROW As DataRow In TblRecID.Rows
                DelComplexBuildings(ctr1) = _RECROW("REC_ID")
                DelExtInfo(ctr1) = _RECROW("REC_ID")
                DelDocInfo(ctr1) = _RECROW("REC_ID")
                DelByLB(ctr1) = _RECROW("REC_ID")
                ctr1 += 1
            Next
            DelParam.DeleteComplexBuilding = DelComplexBuildings
            DelParam.DeleteDocumentInfo = DelDocInfo
            DelParam.DeleteExtendedInfo = DelExtInfo
            DelParam.DeleteByLB = DelByLB
            DelParam.MID_DeleteLandB = xMID.Text
            DelParam.MID_DeleteAdvances = Me.xMID.Text
            DelParam.MID_DeleteLiabilities = Me.xMID.Text
            DelParam.MID_DeleteDeposits = Me.xMID.Text
            DelParam.MID_DeletePurpose = Me.xMID.Text
            DelParam.MID_DeleteMaster = Me.xMID.Text

            If Not Base._Journal_voucher_DBOps.DeleteJournal_Txn(DelParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If
        If Not xPromptWindow Is Nothing Then xPromptWindow.Dispose()
        'End If
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
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, BUT_NEW.GotFocus, BUT_EDIT.GotFocus, BUT_DELETE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, BUT_NEW.LostFocus, BUT_EDIT.LostFocus, BUT_DELETE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, BUT_NEW.KeyDown, BUT_EDIT.KeyDown, BUT_DELETE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click, BUT_DELETE.Click, T_New.Click, T_Edit.Click, T_Delete.Click, BUT_VIEW.Click, T_VIEW.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(btn.Name) = "BUT_VIEW" Then Me.DataNavigation("VIEW")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_NEW" Then Me.DataNavigation("NEW")
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_DELETE" Then Me.DataNavigation("DELETE")
            If UCase(T_btn.Name) = "T_VIEW" Then Me.DataNavigation("VIEW")
        End If

    End Sub
#End Region

#Region "Start--> TextBox Events"

    Private Sub Item_Msg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Item_Msg.Click
        Me.GridView1.Focus()
    End Sub
    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown, CheckEdit2.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub

    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_Narration.GotFocus, Txt_Reference.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        'If txt.Name = Txt_CashAmt.Name Or txt.Name = Txt_CreditAmt.Name Then
        '    txt.SelectAll()
        'ElseIf Val(txt.Properties.Tag) = 0 Then
        '    SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        'End If
    End Sub

    Private Sub TxtLostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        'If txt.Name = Txt_CashAmt.Name Or txt.Name = Txt_CreditAmt.Name Then
        '    txt.Text = Format(Val(txt.Text), "#0.00")
        'End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Narration.KeyPress, Txt_Reference.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_V_Date.KeyDown, Txt_Reference.KeyDown, Txt_Narration.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Narration.Validated, Txt_Reference.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub
    Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
        If Me.GridView1.OptionsView.ShowPreview Then
            Me.GridView1.OptionsView.ShowPreview = False
        Else
            Me.GridView1.OptionsView.ShowPreview = True
        End If
    End Sub

    Private Sub Difference_Calculation()
        Txt_DiffAmt.Text = Val(Txt_DrTotal.Text) - Val(Txt_CrTotal.Text)
    End Sub

    Private Sub Calculation_Check()
        If Val(Txt_DiffAmt.Text) <> 0 Then
            Me.BUT_SAVE_COM.Enabled = False
            Txt_DiffAmt.Properties.AppearanceDisabled.BackColor = Color.LightSalmon
        Else
            Me.BUT_SAVE_COM.Enabled = True
            Txt_DiffAmt.Properties.AppearanceDisabled.BackColor = Color.AliceBlue
        End If

    End Sub

    Private Sub Sub_Amt_Calculation(ByVal Delete_Action As Boolean)
        Me.GridView1.ClearSorting()
        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xDebit As Double = 0 : Dim xCredit As Double = 0
        For I As Integer = 0 To Me.GridView1.RowCount - 1
            If Delete_Action Then Me.GridView1.SetRowCellValue(I, "Sr.", I + 1)
            xDebit += Val(Me.GridView1.GetRowCellValue(I, "Debit").ToString())
            xCredit += Val(Me.GridView1.GetRowCellValue(I, "Credit").ToString())
        Next
        Me.GridView1.RefreshData()
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        '---------------
        Txt_DrTotal.Text = xDebit.ToString()
        Txt_CrTotal.Text = xCredit.ToString()
        Difference_Calculation()
        Calculation_Check()
        If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
    End Sub

#End Region

#Region "Start--> Custom Grid Setting"

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("Item_Detail")
    Friend Bank As New DataSet() : Friend Bank_Detail As DataTable = Bank.Tables.Add("Bank_Detail")
    Private Sub SetGridData()

        '--------------------ITEM DETAIL
        With DT
            .Columns.Add("Sr.", Type.GetType("System.Int32"))
            .Columns.Add("Item_ID", Type.GetType("System.String"))
            .Columns.Add("Item_Led_ID", Type.GetType("System.String"))
            .Columns.Add("Trans_Type", Type.GetType("System.String"))
            .Columns.Add("Item_Voucher_Type", Type.GetType("System.String"))
            .Columns.Add("Item_Party_Req", Type.GetType("System.String"))
            .Columns.Add("Item_Profile", Type.GetType("System.String"))
            .Columns.Add("Item Name", Type.GetType("System.String"))
            .Columns.Add("Head", Type.GetType("System.String"))
            .Columns.Add("Party", Type.GetType("System.String"))
            .Columns.Add("Purpose", Type.GetType("System.String"))
            .Columns.Add("Debit", Type.GetType("System.String"))
            .Columns.Add("Credit", Type.GetType("System.String"))
            .Columns.Add("Remarks", Type.GetType("System.String"))
            .Columns.Add("Pur_ID", Type.GetType("System.String")) 'Purpose ID
            .Columns.Add("PartyID", Type.GetType("System.String"))
            .Columns.Add("Addition", Type.GetType("System.Boolean"))
            .Columns.Add("CrossRefID", Type.GetType("System.String"))
            .Columns.Add("CrossReference", Type.GetType("System.String"))
            .Columns.Add("Qty.", Type.GetType("System.Decimal"))
            .Columns.Add("RefItem_RecEditOn", Type.GetType("System.DateTime"))
            .Columns.Add("Party_RecEditOn", Type.GetType("System.DateTime"))
            .Columns.Add("Unit", Type.GetType("System.String"))
            .Columns.Add("Rate", Type.GetType("System.Double"))
            .Columns.Add("LOC_ID", Type.GetType("System.String"))
            '---Gold/Silver-----
            .Columns.Add("GS_DESC_MISC_ID", Type.GetType("System.String"))
            .Columns.Add("GS_ITEM_WEIGHT", Type.GetType("System.Decimal"))
            '---Other Asset-----
            .Columns.Add("AI_TYPE", Type.GetType("System.String"))
            .Columns.Add("AI_MAKE", Type.GetType("System.String"))
            .Columns.Add("AI_MODEL", Type.GetType("System.String"))
            .Columns.Add("AI_SERIAL_NO", Type.GetType("System.String"))
            .Columns.Add("AI_PUR_DATE", Type.GetType("System.String"))
            .Columns.Add("AI_WARRANTY", Type.GetType("System.Double"))
            '---LIVE STOCK----
            .Columns.Add("LS_NAME", Type.GetType("System.String"))
            .Columns.Add("LS_BIRTH_YEAR", Type.GetType("System.String"))
            .Columns.Add("LS_INSURANCE", Type.GetType("System.String"))
            .Columns.Add("LS_INSURANCE_ID", Type.GetType("System.String"))
            .Columns.Add("LS_INS_POLICY_NO", Type.GetType("System.String"))
            .Columns.Add("LS_INS_AMT", Type.GetType("System.Double"))
            .Columns.Add("LS_INS_DATE", Type.GetType("System.String"))
            '---Vehicles------
            .Columns.Add("VI_MAKE", Type.GetType("System.String"))
            .Columns.Add("VI_MODEL", Type.GetType("System.String"))
            .Columns.Add("VI_REG_NO_PATTERN", Type.GetType("System.String"))
            .Columns.Add("VI_REG_NO", Type.GetType("System.String"))
            .Columns.Add("VI_REG_DATE", Type.GetType("System.String"))
            .Columns.Add("VI_OWNERSHIP", Type.GetType("System.String"))
            .Columns.Add("VI_OWNERSHIP_AB_ID", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_RC_BOOK", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_AFFIDAVIT", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_WILL", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_TRF_LETTER", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_FU_LETTER", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_OTHERS", Type.GetType("System.String"))
            .Columns.Add("VI_DOC_NAME", Type.GetType("System.String"))
            .Columns.Add("VI_INSURANCE_ID", Type.GetType("System.String"))
            .Columns.Add("VI_INS_POLICY_NO", Type.GetType("System.String"))
            .Columns.Add("VI_INS_EXPIRY_DATE", Type.GetType("System.String"))
            '-----Land&Building-----
            .Columns.Add("LB_PRO_TYPE", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_CATEGORY", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_USE", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_NAME", Type.GetType("System.String"))
            .Columns.Add("LB_PRO_ADDRESS", Type.GetType("System.String"))
            .Columns.Add("LB_OWNERSHIP", Type.GetType("System.String"))
            .Columns.Add("LB_OWNERSHIP_PARTY_ID", Type.GetType("System.String"))
            .Columns.Add("LB_SURVEY_NO", Type.GetType("System.String"))
            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
            .Columns.Add("LB_RCC_ROOF", Type.GetType("System.String"))
            .Columns.Add("LB_DEPOSIT_AMT", Type.GetType("System.Double"))
            .Columns.Add("LB_PAID_DATE", Type.GetType("System.String"))
            .Columns.Add("LB_MONTH_RENT", Type.GetType("System.Double"))
            .Columns.Add("LB_MONTH_O_PAYMENTS", Type.GetType("System.Double"))
            .Columns.Add("LB_PERIOD_FROM", Type.GetType("System.String"))
            .Columns.Add("LB_PERIOD_TO", Type.GetType("System.String"))
            .Columns.Add("LB_DOC_OTHERS", Type.GetType("System.String"))
            .Columns.Add("LB_DOC_NAME", Type.GetType("System.String"))
            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS1", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS2", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS3", Type.GetType("System.String"))
            .Columns.Add("LB_ADDRESS4", Type.GetType("System.String"))
            .Columns.Add("LB_STATE_ID", Type.GetType("System.String"))
            .Columns.Add("LB_DISTRICT_ID", Type.GetType("System.String"))
            .Columns.Add("LB_CITY_ID", Type.GetType("System.String"))
            .Columns.Add("LB_PINCODE", Type.GetType("System.String"))
            '.Columns.Add("LB_ADDRESS", Type.GetType("System.String"))
            '--WIP--
            .Columns.Add("REF_REC_ID", Type.GetType("System.String"))
            .Columns.Add("REFERENCE", Type.GetType("System.String"))
            .Columns.Add("WIP_REF_TYPE", Type.GetType("System.String"))
        End With
        Me.GridControl1.DataSource = DS : Me.GridControl1.DataMember = "Item_Detail"
        Me.GridView1.Columns("Sr.").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        Me.GridView1.Columns("Sr.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GridView1.Columns("Sr.").Width = 40
        Me.GridView1.Columns("Item Name").Width = 185
        Me.GridView1.Columns("Head").Width = 150
        Me.GridView1.Columns("Qty.").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Qty.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Qty.").Width = 40
        Me.GridView1.Columns("Debit").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Debit").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Debit").Width = 75
        Me.GridView1.Columns("Credit").AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Credit").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Credit").Width = 75
        Me.GridView1.Columns("Party").Width = 115
        Me.GridView1.Columns("Purpose").Width = 100
        Me.GridView1.Columns("Item_ID").Visible = False
        Me.GridView1.Columns("Item_Led_ID").Visible = False
        Me.GridView1.Columns("Trans_Type").Visible = False
        Me.GridView1.Columns("Item_Party_Req").Visible = False
        Me.GridView1.Columns("Item_Profile").Visible = False
        Me.GridView1.Columns("Item_Voucher_Type").Visible = False
        Me.GridView1.Columns("Remarks").Visible = False
        Me.GridView1.Columns("Pur_ID").Visible = False
        Me.GridView1.Columns("PartyID").Visible = False
        Me.GridView1.Columns("Addition").Visible = False
        Me.GridView1.Columns("CrossRefID").Visible = False
        Me.GridView1.Columns("CrossReference").Visible = False
        Me.GridView1.Columns("RefItem_RecEditOn").Visible = False
        Me.GridView1.Columns("Party_RecEditOn").Visible = False
        Me.GridView1.Columns("LOC_ID").Visible = False
        '---Gold/Silver-----
        Me.GridView1.Columns("GS_DESC_MISC_ID").Visible = False
        Me.GridView1.Columns("GS_ITEM_WEIGHT").Visible = False
        '---Other Asset-----
        Me.GridView1.Columns("AI_TYPE").Visible = False
        Me.GridView1.Columns("AI_MAKE").Visible = False
        Me.GridView1.Columns("AI_MODEL").Visible = False
        Me.GridView1.Columns("AI_SERIAL_NO").Visible = False
        Me.GridView1.Columns("AI_PUR_DATE").Visible = False
        Me.GridView1.Columns("AI_WARRANTY").Visible = False
        '---Livestock-----
        Me.GridView1.Columns("LS_NAME").Visible = False
        Me.GridView1.Columns("LS_BIRTH_YEAR").Visible = False
        Me.GridView1.Columns("LS_INSURANCE").Visible = False
        Me.GridView1.Columns("LS_INSURANCE_ID").Visible = False
        Me.GridView1.Columns("LS_INS_POLICY_NO").Visible = False
        Me.GridView1.Columns("LS_INS_AMT").Visible = False
        Me.GridView1.Columns("LS_INS_DATE").Visible = False
        '---Vehicles------
        Me.GridView1.Columns("VI_MAKE").Visible = False
        Me.GridView1.Columns("VI_MODEL").Visible = False
        Me.GridView1.Columns("VI_REG_NO_PATTERN").Visible = False
        Me.GridView1.Columns("VI_REG_NO").Visible = False
        Me.GridView1.Columns("VI_REG_DATE").Visible = False
        Me.GridView1.Columns("VI_OWNERSHIP").Visible = False
        Me.GridView1.Columns("VI_OWNERSHIP_AB_ID").Visible = False
        Me.GridView1.Columns("VI_DOC_RC_BOOK").Visible = False
        Me.GridView1.Columns("VI_DOC_AFFIDAVIT").Visible = False
        Me.GridView1.Columns("VI_DOC_WILL").Visible = False
        Me.GridView1.Columns("VI_DOC_TRF_LETTER").Visible = False
        Me.GridView1.Columns("VI_DOC_FU_LETTER").Visible = False
        Me.GridView1.Columns("VI_DOC_OTHERS").Visible = False
        Me.GridView1.Columns("VI_DOC_NAME").Visible = False
        Me.GridView1.Columns("VI_INSURANCE_ID").Visible = False
        Me.GridView1.Columns("VI_INS_POLICY_NO").Visible = False
        Me.GridView1.Columns("VI_INS_EXPIRY_DATE").Visible = False
        '----Land&Building-----
        Me.GridView1.Columns("LB_PRO_TYPE").Visible = False
        Me.GridView1.Columns("LB_PRO_CATEGORY").Visible = False
        Me.GridView1.Columns("LB_PRO_USE").Visible = False
        Me.GridView1.Columns("LB_PRO_NAME").Visible = False
        Me.GridView1.Columns("LB_PRO_ADDRESS").Visible = False
        Me.GridView1.Columns("LB_OWNERSHIP").Visible = False
        Me.GridView1.Columns("LB_OWNERSHIP_PARTY_ID").Visible = False
        Me.GridView1.Columns("LB_SURVEY_NO").Visible = False
        Me.GridView1.Columns("LB_TOT_P_AREA").Visible = False
        Me.GridView1.Columns("LB_CON_AREA").Visible = False
        Me.GridView1.Columns("LB_CON_YEAR").Visible = False
        Me.GridView1.Columns("LB_RCC_ROOF").Visible = False
        Me.GridView1.Columns("LB_DEPOSIT_AMT").Visible = False
        Me.GridView1.Columns("LB_PAID_DATE").Visible = False
        Me.GridView1.Columns("LB_MONTH_RENT").Visible = False
        Me.GridView1.Columns("LB_MONTH_O_PAYMENTS").Visible = False
        Me.GridView1.Columns("LB_PERIOD_FROM").Visible = False
        Me.GridView1.Columns("LB_PERIOD_TO").Visible = False
        Me.GridView1.Columns("LB_DOC_OTHERS").Visible = False
        Me.GridView1.Columns("LB_DOC_NAME").Visible = False
        Me.GridView1.Columns("LB_OTHER_DETAIL").Visible = False
        Me.GridView1.Columns("LB_REC_ID").Visible = False
        Me.GridView1.Columns("LB_ADDRESS1").Visible = False
        Me.GridView1.Columns("LB_ADDRESS2").Visible = False
        Me.GridView1.Columns("LB_ADDRESS3").Visible = False
        Me.GridView1.Columns("LB_ADDRESS4").Visible = False
        Me.GridView1.Columns("LB_STATE_ID").Visible = False
        Me.GridView1.Columns("LB_DISTRICT_ID").Visible = False
        Me.GridView1.Columns("LB_CITY_ID").Visible = False
        Me.GridView1.Columns("LB_PINCODE").Visible = False
        'Me.GridView1.Columns("LB_ADDRESS").Visible = False
        '--WIP--
        Me.GridView1.Columns("REF_REC_ID").Visible = False
        Me.GridView1.Columns("REFERENCE").Visible = False
        Me.GridView1.Columns("WIP_REF_TYPE").Visible = False

        Me.GridView1.PreviewFieldName = "Remarks"

        Me.GridView1.Columns("Debit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Debit").DisplayFormat.FormatString = "#0.00"

        Me.GridView1.Columns("Credit").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Credit").DisplayFormat.FormatString = "#0.00"
    End Sub

    Private Sub GridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation("EDIT")
        End If
    End Sub

    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If e.KeyCode = Keys.Insert Then
                e.SuppressKeyPress = True
                Me.DataNavigation("NEW")
            End If
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                Me.DataNavigation("EDIT")
            End If
            If e.KeyCode = Keys.Delete Then
                e.SuppressKeyPress = True
                Me.DataNavigation("DELETE")
            End If
        End If
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
            Me.DataNavigation("VIEW")
        End If
        If e.KeyCode = Keys.PageUp Then
            e.SuppressKeyPress = True
            Me.Txt_V_Date.Focus()
        End If
        If e.KeyCode = Keys.PageDown Then
            e.SuppressKeyPress = False
            Me.Txt_Narration.Focus()
        End If
    End Sub

    Public Sub DataNavigation(ByVal Action As String)

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Dim Delete_Action As Boolean = False
            Select Case Action
                Case "NEW"
                    Dim xfrm As New Frm_Voucher_Win_Journal_Item
                    xfrm.Text = "New ~ Item Detail" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then xfrm.iTxnM_ID = Me.xMID.Text
                    If IsDate(Me.Txt_V_Date.Text) Then xfrm.Vdt = Format(Me.Txt_V_Date.DateTime, Base._Date_Format_Current)
                    If Val(Txt_DiffAmt.Text) > 0 Then xfrm.RdAction.SelectedIndex = 1 Else xfrm.RdAction.SelectedIndex = 0
                    xfrm.iTop = Me.Top
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        Me.GridView1.ClearSorting()
                        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
                        ROW = DT.NewRow
                        If GridView1.RowCount <= 0 Then
                            ROW("Sr.") = 1
                        Else
                            Me.GridView1.MoveLast()
                            ROW("Sr.") = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) + 1
                        End If
                        ROW("Item Name") = xfrm.GLookUp_ItemList.Text
                        ROW("Item_ID") = xfrm.GLookUp_ItemList.Tag
                        If xfrm.iCond_Ledger_ID <> "00000" And xfrm.Cmb_RefType.SelectedIndex = 1 Then 'New
                            If Val(xfrm.Txt_Amt.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(xfrm.iMaxValue) Then
                                ROW("Item_Led_ID") = xfrm.iCond_Ledger_ID
                            Else
                                ROW("Item_Led_ID") = xfrm.iLed_ID
                            End If
                        Else
                            ROW("Item_Led_ID") = xfrm.iLed_ID
                        End If
                        'ROW("Item_Led_ID") = xfrm.iLed_ID
                        If xfrm.RdAction.SelectedIndex = 0 Then ROW("Trans_Type") = "DEBIT" Else ROW("Trans_Type") = "CREDIT"
                        ROW("Item_Profile") = xfrm.iProfile
                        ROW("Item_Party_Req") = xfrm.iParty_Req
                        ROW("Head") = xfrm.BE_Item_Head.Text
                        If xfrm.GLookUp_PartyList.Text.Length > 0 Then ROW("Party") = xfrm.GLookUp_PartyList.Text
                        If Val(xfrm.Txt_Qty.Text) > 0 Then ROW("Qty.") = Convert.ToDecimal(xfrm.Txt_Qty.Text)
                        If xfrm.RdAction.SelectedIndex = 0 Then ROW("Debit") = Val(xfrm.Txt_Amt.Text).ToString("0.00") : ROW("Credit") = ""
                        If xfrm.RdAction.SelectedIndex = 1 Then ROW("Credit") = Val(xfrm.Txt_Amt.Text).ToString("0.00") : ROW("Debit") = ""
                        ROW("Remarks") = xfrm.Txt_Remarks.Text
                        ROW("PUR_ID") = xfrm.GLookUp_PurList.Tag
                        ROW("Purpose") = xfrm.GLookUp_PurList.Text
                        ROW("PartyID") = xfrm.GLookUp_PartyList.Tag
                        ROW("Item_Voucher_Type") = xfrm.Txt_ItemNature.Text
                        ROW("CrossReference") = xfrm.TXT_Reference.Text

                        ROW("RefItem_RecEditOn") = xfrm.RefItem_RecEditOn
                        ROW("Party_RecEditOn") = xfrm.Party_RecEditOn

                        If xfrm.Cmb_RefType.SelectedIndex = 1 Then ROW("Addition") = True Else ROW("Addition") = False
                        If xfrm.Cmb_RefType.SelectedIndex = 0 Then
                            If Not xfrm.Cross_RefID Is Nothing Then ROW("CrossRefID") = xfrm.Cross_RefID
                        Else : ROW("CrossRefID") = ""
                        End If
                        ROW("LB_REC_ID") = xfrm.LB_REC_ID
                        If xfrm.iProfile = "GOLD" Or xfrm.iProfile = "SILVER" Then
                            ROW("GS_DESC_MISC_ID") = xfrm.GS_DESC_MISC_ID
                            ROW("GS_ITEM_WEIGHT") = xfrm.GS_ITEM_WEIGHT
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "OTHER ASSETS" Then
                            ROW("AI_TYPE") = xfrm.AI_TYPE
                            ROW("AI_MAKE") = xfrm.AI_MAKE
                            ROW("AI_MODEL") = xfrm.AI_MODEL
                            ROW("AI_SERIAL_NO") = xfrm.AI_SERIAL_NO
                            If IsDate(xfrm.AI_PUR_DATE) Then
                                ROW("AI_PUR_DATE") = xfrm.AI_PUR_DATE
                            End If
                            ROW("AI_WARRANTY") = xfrm.AI_WARRANTY
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "LIVESTOCK" Then
                            ROW("LS_NAME") = xfrm.LS_NAME
                            ROW("LS_BIRTH_YEAR") = xfrm.LS_BIRTH_YEAR
                            ROW("LS_INSURANCE") = xfrm.LS_INSURANCE
                            ROW("LS_INSURANCE_ID") = xfrm.LS_INSURANCE_ID
                            ROW("LS_INS_POLICY_NO") = xfrm.LS_INS_POLICY_NO
                            ROW("LS_INS_AMT") = xfrm.LS_INS_AMT
                            If IsDate(xfrm.LS_INS_DATE) Then
                                ROW("LS_INS_DATE") = xfrm.LS_INS_DATE
                            End If
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "VEHICLES" Then
                            ROW("VI_MAKE") = xfrm.VI_MAKE
                            ROW("VI_MODEL") = xfrm.VI_MODEL
                            ROW("VI_REG_NO_PATTERN") = xfrm.VI_REG_NO_PATTERN
                            ROW("VI_REG_NO") = xfrm.VI_REG_NO
                            ROW("VI_REG_DATE") = xfrm.VI_REG_DATE
                            ROW("VI_OWNERSHIP") = xfrm.VI_OWNERSHIP
                            ROW("VI_OWNERSHIP_AB_ID") = xfrm.VI_OWNERSHIP_AB_ID
                            ROW("VI_DOC_RC_BOOK") = xfrm.VI_DOC_RC_BOOK
                            ROW("VI_DOC_AFFIDAVIT") = xfrm.VI_DOC_AFFIDAVIT
                            ROW("VI_DOC_WILL") = xfrm.VI_DOC_WILL
                            ROW("VI_DOC_TRF_LETTER") = xfrm.VI_DOC_TRF_LETTER
                            ROW("VI_DOC_FU_LETTER") = xfrm.VI_DOC_FU_LETTER
                            ROW("VI_DOC_OTHERS") = xfrm.VI_DOC_OTHERS
                            ROW("VI_DOC_NAME") = xfrm.VI_DOC_NAME
                            ROW("VI_INSURANCE_ID") = xfrm.VI_INSURANCE_ID
                            ROW("VI_INS_POLICY_NO") = xfrm.VI_INS_POLICY_NO
                            ROW("VI_INS_EXPIRY_DATE") = xfrm.VI_INS_EXPIRY_DATE
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                        End If
                        If xfrm.iProfile = "LAND & BUILDING" Then
                            ROW("LB_PRO_TYPE") = xfrm.LB_PRO_TYPE
                            ROW("LB_PRO_CATEGORY") = xfrm.LB_PRO_CATEGORY
                            ROW("LB_PRO_USE") = xfrm.LB_PRO_USE
                            ROW("LB_PRO_NAME") = xfrm.LB_PRO_NAME
                            ROW("LB_PRO_ADDRESS") = xfrm.LB_PRO_ADDRESS
                            'ROW("LB_ADDRESS") = xfrm.LB_ADDRESS
                            ROW("LB_ADDRESS1") = xfrm.LB_ADDRESS1
                            ROW("LB_ADDRESS2") = xfrm.LB_ADDRESS2
                            ROW("LB_ADDRESS3") = xfrm.LB_ADDRESS3
                            ROW("LB_ADDRESS4") = xfrm.LB_ADDRESS4
                            ROW("LB_CITY_ID") = xfrm.LB_CITY_ID
                            ROW("LB_DISTRICT_ID") = xfrm.LB_DISTRICT_ID
                            ROW("LB_STATE_ID") = xfrm.LB_STATE_ID
                            ROW("LB_PINCODE") = xfrm.LB_PINCODE
                            ROW("LB_OWNERSHIP") = xfrm.LB_OWNERSHIP
                            ROW("LB_OWNERSHIP_PARTY_ID") = xfrm.LB_OWNERSHIP_PARTY_ID
                            ROW("LB_SURVEY_NO") = xfrm.LB_SURVEY_NO
                            ROW("LB_CON_YEAR") = xfrm.LB_CON_YEAR
                            ROW("LB_RCC_ROOF") = xfrm.LB_RCC_ROOF
                            ROW("LB_PAID_DATE") = xfrm.LB_PAID_DATE
                            ROW("LB_PERIOD_FROM") = xfrm.LB_PERIOD_FROM
                            ROW("LB_PERIOD_TO") = xfrm.LB_PERIOD_TO
                            ROW("LB_DOC_OTHERS") = xfrm.LB_DOC_OTHERS
                            ROW("LB_DOC_NAME") = xfrm.LB_DOC_NAME
                            ROW("LB_OTHER_DETAIL") = xfrm.LB_OTHER_DETAIL
                            ROW("LB_TOT_P_AREA") = xfrm.LB_TOT_P_AREA
                            ROW("LB_CON_AREA") = xfrm.LB_CON_AREA
                            ROW("LB_DEPOSIT_AMT") = xfrm.LB_DEPOSIT_AMT
                            ROW("LB_MONTH_RENT") = xfrm.LB_MONTH_RENT
                            ROW("LB_MONTH_O_PAYMENTS") = xfrm.LB_MONTH_O_PAYMENTS
                            If LB_DOCS_ARRAY Is Nothing Then
                                LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY
                            Else
                                If LB_DOCS_ARRAY.Rows.Count <= 0 Then
                                    LB_DOCS_ARRAY = New DataTable
                                    With LB_DOCS_ARRAY
                                        .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                        .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                    End With
                                End If
                                If Not xfrm.LB_DOCS_ARRAY Is Nothing Then
                                    For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                        Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                        Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                        Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                        LB_DOCS_ARRAY.Rows.Add(Row)
                                    Next
                                End If
                                End If
                                If LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                    LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE
                                Else
                                    If LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0 Then
                                        LB_EXTENDED_PROPERTY_TABLE = New DataTable
                                        With LB_EXTENDED_PROPERTY_TABLE
                                            .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                            .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                            .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                            .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                        End With
                                End If
                                If Not xfrm.LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                    For Each XROW As DataRow In xfrm.LB_EXTENDED_PROPERTY_TABLE.Rows
                                        Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                        Row("LB_MOU_DATE") = XROW("LB_MOU_DATE").ToString
                                        Row("LB_SR_NO") = XROW("LB_SR_NO").ToString
                                        Row("LB_INS_ID") = XROW("LB_INS_ID").ToString
                                        Row("LB_TOT_P_AREA") = Val(XROW("LB_TOT_P_AREA").ToString())
                                        Row("LB_CON_AREA") = Val(XROW("LB_CON_AREA").ToString())
                                        Row("LB_CON_YEAR") = XROW("LB_CON_YEAR").ToString()
                                        Row("LB_VALUE") = Val(XROW("LB_VALUE"))
                                        Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL").ToString()
                                        Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                        LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                    Next
                                End If
                                End If
                        End If

                        If xfrm.iProfile.Equals("WIP") Then
                            If xfrm.iRefType = "NEW" Then
                                ROW("REFERENCE") = xfrm.iReference
                                ROW("WIP_REF_TYPE") = xfrm.iRefType
                            Else
                                ROW("REF_REC_ID") = xfrm.Ref_RecID
                                ROW("WIP_REF_TYPE") = xfrm.iRefType
                            End If
                        End If

                        DT.Rows.Add(ROW)
                        'xID = xfrm.xID.Text
                        xfrm.Dispose()
                        If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
                    Else
                        xfrm.Dispose()
                    End If


                Case "EDIT", "VIEW"
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        Dim xfrm As New Frm_Voucher_Win_Journal_Item
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If
                        xfrm.iProfile_OLD = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString()
                        xfrm.iSpecific_ItemID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID").ToString()
                        xfrm.iPartyID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "PartyID").ToString()
                        xfrm.iPur_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID").ToString()
                        xfrm.Txt_Qty.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.").ToString()
                        If Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Debit").ToString()) > 0 Then
                            xfrm.Txt_Amt.Text = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Debit").ToString())
                            xfrm.RdAction.SelectedIndex = 0
                        End If
                        If Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Credit").ToString()) > 0 Then
                            xfrm.Txt_Amt.Text = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Credit").ToString())
                            xfrm.RdAction.SelectedIndex = 1
                        End If
                        xfrm.Txt_Remarks.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks").ToString()
                        xfrm.Cross_RefID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CrossRefID").ToString()
                        xfrm.TXT_Reference.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "CrossReference").ToString()
                        If Convert.ToBoolean(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Addition")) Then xfrm.Cmb_RefType.SelectedIndex = 1
                        xfrm.iTxnM_ID = Me.xMID.Text
                        xfrm.iTop = Me.Top
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "RefItem_RecEditOn").ToString.Length > 0 Then
                            xfrm.RefItem_RecEditOn = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "RefItem_RecEditOn")
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Party_RecEditOn").ToString.Length > 0 Then
                            xfrm.Party_RecEditOn = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Party_RecEditOn")
                        End If
                        If Not Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID") Is Nothing Then xfrm.LB_REC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID").ToString()
                        'If xfrm.iProfile = "LAND & BUILDING" Then xfrm.LB_REC_ID = Guid.NewGuid.ToString
                        xfrm.GLookUp_ItemList.Enabled = False

                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "GOLD" Or _
                                                   Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "SILVER" Then
                            xfrm.GS_DESC_MISC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_DESC_MISC_ID").ToString()
                            xfrm.GS_ITEM_WEIGHT = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_ITEM_WEIGHT").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "OTHER ASSETS" Then
                            xfrm.AI_TYPE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_TYPE").ToString()
                            xfrm.AI_MAKE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MAKE").ToString()
                            xfrm.AI_MODEL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MODEL").ToString()
                            xfrm.AI_SERIAL_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_SERIAL_NO").ToString()
                            xfrm.AI_PUR_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_PUR_DATE").ToString()
                            xfrm.AI_WARRANTY = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_WARRANTY").ToString())
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LIVESTOCK" Then
                            xfrm.LS_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_NAME").ToString()
                            xfrm.LS_BIRTH_YEAR = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_BIRTH_YEAR").ToString()
                            xfrm.LS_INSURANCE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE").ToString()
                            xfrm.LS_INSURANCE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE_ID").ToString()
                            xfrm.LS_INS_POLICY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_POLICY_NO").ToString()
                            xfrm.LS_INS_AMT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_AMT").ToString())
                            xfrm.LS_INS_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_DATE").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "VEHICLES" Then
                            xfrm.VI_MAKE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MAKE").ToString()
                            xfrm.VI_MODEL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MODEL").ToString()
                            xfrm.VI_REG_NO_PATTERN = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO_PATTERN").ToString()
                            xfrm.VI_REG_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO").ToString()
                            xfrm.VI_REG_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_DATE").ToString()
                            xfrm.VI_OWNERSHIP = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP").ToString()
                            xfrm.VI_OWNERSHIP_AB_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP_AB_ID").ToString()
                            xfrm.VI_DOC_RC_BOOK = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_RC_BOOK").ToString()
                            xfrm.VI_DOC_AFFIDAVIT = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_AFFIDAVIT").ToString()
                            xfrm.VI_DOC_WILL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_WILL").ToString()
                            xfrm.VI_DOC_TRF_LETTER = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_TRF_LETTER").ToString()
                            xfrm.VI_DOC_FU_LETTER = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_FU_LETTER").ToString()
                            xfrm.VI_DOC_OTHERS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_OTHERS").ToString()
                            xfrm.VI_DOC_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_NAME").ToString()
                            xfrm.VI_INSURANCE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INSURANCE_ID").ToString()
                            xfrm.VI_INS_POLICY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_POLICY_NO").ToString()
                            xfrm.VI_INS_EXPIRY_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_EXPIRY_DATE").ToString()
                            xfrm.X_LOC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID").ToString()
                        End If
                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LAND & BUILDING" Then
                            xfrm.LB_PRO_TYPE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_TYPE").ToString()
                            xfrm.LB_PRO_CATEGORY = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_CATEGORY").ToString()
                            xfrm.LB_PRO_USE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_USE").ToString()
                            xfrm.LB_PRO_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_NAME").ToString()
                            xfrm.LB_PRO_ADDRESS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_ADDRESS").ToString()
                            'xfrm.LB_ADDRESS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS").ToString()
                            xfrm.LB_ADDRESS1 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS1").ToString()
                            xfrm.LB_ADDRESS2 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS2").ToString()
                            xfrm.LB_ADDRESS3 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS3").ToString()
                            xfrm.LB_ADDRESS4 = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS4").ToString()
                            xfrm.LB_CITY_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CITY_ID").ToString()
                            xfrm.LB_DISTRICT_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DISTRICT_ID").ToString()
                            xfrm.LB_STATE_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_STATE_ID").ToString()
                            xfrm.LB_PINCODE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PINCODE").ToString()
                            xfrm.LB_OWNERSHIP = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP").ToString()
                            xfrm.LB_OWNERSHIP_PARTY_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP_PARTY_ID").ToString()
                            xfrm.LB_SURVEY_NO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_SURVEY_NO").ToString()
                            xfrm.LB_CON_YEAR = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_YEAR").ToString()
                            xfrm.LB_RCC_ROOF = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_RCC_ROOF").ToString()
                            xfrm.LB_PAID_DATE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PAID_DATE").ToString()
                            xfrm.LB_PERIOD_FROM = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_FROM").ToString()
                            xfrm.LB_PERIOD_TO = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_TO").ToString()
                            xfrm.LB_DOC_OTHERS = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_OTHERS").ToString()
                            xfrm.LB_DOC_NAME = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_NAME").ToString()
                            xfrm.LB_OTHER_DETAIL = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OTHER_DETAIL").ToString()
                            xfrm.LB_TOT_P_AREA = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_TOT_P_AREA").ToString())
                            xfrm.LB_CON_AREA = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_AREA").ToString())
                            xfrm.LB_DEPOSIT_AMT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DEPOSIT_AMT").ToString())
                            xfrm.LB_MONTH_RENT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_RENT").ToString())
                            xfrm.LB_MONTH_O_PAYMENTS = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_O_PAYMENTS").ToString())


                            Dim EDIT_LB_EXTENDED_PROPERTY_TABLE As DataTable = New DataTable
                            With EDIT_LB_EXTENDED_PROPERTY_TABLE
                                .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                            End With
                            If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then 'LB Item screen already opened in same instance 
                                For Each XROW As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                    If XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                        Dim Row As DataRow = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                        Row("LB_MOU_DATE") = XROW("LB_MOU_DATE")
                                        Row("LB_SR_NO") = XROW("LB_SR_NO")
                                        Row("LB_INS_ID") = XROW("LB_INS_ID")
                                        Row("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
                                        Row("LB_CON_AREA") = XROW("LB_CON_AREA")
                                        Row("LB_CON_YEAR") = XROW("LB_CON_YEAR")
                                        Row("LB_VALUE") = XROW("LB_VALUE")
                                        Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
                                        Row("LB_REC_ID") = xfrm.LB_REC_ID
                                        EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                    End If
                                Next
                            Else
                                Dim LB_Ext As DataTable = Base._L_B_DBOps.GetExtensionDetails(xfrm.LB_REC_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)
                                For Each XROW As DataRow In LB_Ext.Rows
                                    Dim Row As DataRow = EDIT_LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                    Row("LB_MOU_DATE") = XROW("LB_MOU_DATE")
                                    Row("LB_SR_NO") = XROW("LB_SR_NO")
                                    Row("LB_INS_ID") = XROW("LB_INS_ID")
                                    Row("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
                                    Row("LB_CON_AREA") = XROW("LB_CON_AREA")
                                    Row("LB_CON_YEAR") = XROW("LB_CON_YEAR")
                                    Row("LB_VALUE") = XROW("LB_VALUE")
                                    Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
                                    Row("LB_REC_ID") = xfrm.LB_REC_ID
                                    EDIT_LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                Next
                            End If
                            xfrm.LB_EXTENDED_PROPERTY_TABLE = EDIT_LB_EXTENDED_PROPERTY_TABLE

                            Dim EDIT_LB_DOCS_ARRAY As DataTable = New DataTable
                            With EDIT_LB_DOCS_ARRAY
                                .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                            End With
                            If Not LB_DOCS_ARRAY Is Nothing Then
                                For Each XROW As DataRow In LB_DOCS_ARRAY.Rows
                                    If XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                        Dim Row As DataRow = EDIT_LB_DOCS_ARRAY.NewRow()
                                        Row("LB_MISC_ID") = XROW("LB_MISC_ID")
                                        Row("LB_REC_ID") = xfrm.LB_REC_ID
                                        EDIT_LB_DOCS_ARRAY.Rows.Add(Row)
                                    End If
                                Next
                            Else
                                Dim LB_DOC As DataTable = Base._L_B_DBOps.GetDocsDetails(xfrm.LB_REC_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)
                                For Each XROW As DataRow In LB_DOC.Rows
                                    Dim Row As DataRow = EDIT_LB_DOCS_ARRAY.NewRow()
                                    Row("LB_MISC_ID") = XROW("LB_MISC_ID")
                                    Row("LB_REC_ID") = xfrm.LB_REC_ID
                                    EDIT_LB_DOCS_ARRAY.Rows.Add(Row)
                                Next
                            End If
                            xfrm.LB_DOCS_ARRAY = EDIT_LB_DOCS_ARRAY
                        End If

                        If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "WIP" Then
                            xfrm.iReference = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REFERENCE").ToString()
                            xfrm.Ref_RecID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID").ToString()
                            xfrm.iRefType = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE").ToString()
                        End If

                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK And xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item Name", xfrm.GLookUp_ItemList.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID", xfrm.GLookUp_ItemList.Tag)
                            If xfrm.iCond_Ledger_ID <> "00000" And xfrm.Cmb_RefType.SelectedIndex = 1 Then
                                If Val(xfrm.Txt_Amt.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(xfrm.iMaxValue) Then
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iCond_Ledger_ID)
                                Else
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                                End If
                            Else
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                            End If
                            'Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                            Dim txnType As String = "CREDIT" : If xfrm.RdAction.SelectedIndex = 0 Then txnType = "DEBIT"
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Trans_Type", txnType)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile", xfrm.iProfile)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Party_Req", xfrm.iParty_Req)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Head", xfrm.BE_Item_Head.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Party", xfrm.GLookUp_PartyList.Text)
                            If Val(xfrm.Txt_Qty.Text) > 0 Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.", Convert.ToDecimal(xfrm.Txt_Qty.Text))
                            ElseIf Val(xfrm.Txt_Qty.Text) = 0 Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.", Val(xfrm.Txt_Qty.Text))
                            End If
                            If xfrm.RdAction.SelectedIndex = 0 Then Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Debit", Val(xfrm.Txt_Amt.Text).ToString("0.00")) : Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Credit", "")
                            If xfrm.RdAction.SelectedIndex = 1 Then Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Credit", Val(xfrm.Txt_Amt.Text).ToString("0.00")) : Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Debit", "")
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks", xfrm.Txt_Remarks.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID", xfrm.GLookUp_PurList.Tag)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Purpose", xfrm.GLookUp_PurList.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "PartyID", xfrm.GLookUp_PartyList.Tag)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Voucher_Type", xfrm.Txt_ItemNature.Text)
                            If xfrm.Cmb_RefType.SelectedIndex = 1 Then Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Addition", True) Else Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Addition", False)
                            If xfrm.Cmb_RefType.SelectedIndex = 1 Then Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "CrossRefID", "") Else Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "CrossRefID", xfrm.Cross_RefID)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "CrossReference", xfrm.TXT_Reference.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "RefItem_RecEditOn", xfrm.RefItem_RecEditOn)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Party_RecEditOn", xfrm.Party_RecEditOn)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID", xfrm.LB_REC_ID)

                            If xfrm.iProfile = "GOLD" Or xfrm.iProfile = "SILVER" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_DESC_MISC_ID", xfrm.GS_DESC_MISC_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "GS_ITEM_WEIGHT", xfrm.GS_ITEM_WEIGHT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "OTHER ASSETS" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_TYPE", xfrm.AI_TYPE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MAKE", xfrm.AI_MAKE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_MODEL", xfrm.AI_MODEL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_SERIAL_NO", xfrm.AI_SERIAL_NO)
                                If IsDate(xfrm.AI_PUR_DATE) Then
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_PUR_DATE", xfrm.AI_PUR_DATE)
                                Else
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_PUR_DATE", "")
                                End If
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_WARRANTY", xfrm.AI_WARRANTY)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "LIVESTOCK" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_NAME", xfrm.LS_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_BIRTH_YEAR", xfrm.LS_BIRTH_YEAR)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE", xfrm.LS_INSURANCE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INSURANCE_ID", xfrm.LS_INSURANCE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_POLICY_NO", xfrm.LS_INS_POLICY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_AMT", Val(xfrm.LS_INS_AMT))
                                If IsDate(xfrm.LS_INS_DATE) Then
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_DATE", xfrm.LS_INS_DATE)
                                Else
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LS_INS_DATE", "")
                                End If
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "VEHICLES" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MAKE", xfrm.VI_MAKE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_MODEL", xfrm.VI_MODEL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO_PATTERN", xfrm.VI_REG_NO_PATTERN)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_NO", xfrm.VI_REG_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_REG_DATE", xfrm.VI_REG_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP", xfrm.VI_OWNERSHIP)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_OWNERSHIP_AB_ID", xfrm.VI_OWNERSHIP_AB_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_RC_BOOK", xfrm.VI_DOC_RC_BOOK)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_AFFIDAVIT", xfrm.VI_DOC_AFFIDAVIT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_WILL", xfrm.VI_DOC_WILL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_TRF_LETTER", xfrm.VI_DOC_TRF_LETTER)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_FU_LETTER", xfrm.VI_DOC_FU_LETTER)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_OTHERS", xfrm.VI_DOC_OTHERS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_DOC_NAME", xfrm.VI_DOC_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INSURANCE_ID", xfrm.VI_INSURANCE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_POLICY_NO", xfrm.VI_INS_POLICY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "VI_INS_EXPIRY_DATE", xfrm.VI_INS_EXPIRY_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                            End If
                            If xfrm.iProfile = "LAND & BUILDING" Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_TYPE", xfrm.LB_PRO_TYPE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_CATEGORY", xfrm.LB_PRO_CATEGORY)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_USE", xfrm.LB_PRO_USE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_NAME", xfrm.LB_PRO_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PRO_ADDRESS", xfrm.LB_PRO_ADDRESS)
                                'Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS", xfrm.LB_ADDRESS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS1", xfrm.LB_ADDRESS1)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS2", xfrm.LB_ADDRESS2)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS3", xfrm.LB_ADDRESS3)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_ADDRESS4", xfrm.LB_ADDRESS4)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CITY_ID", xfrm.LB_CITY_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DISTRICT_ID", xfrm.LB_DISTRICT_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_STATE_ID", xfrm.LB_STATE_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PINCODE", xfrm.LB_PINCODE)

                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP", xfrm.LB_OWNERSHIP)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OWNERSHIP_PARTY_ID", xfrm.LB_OWNERSHIP_PARTY_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_SURVEY_NO", xfrm.LB_SURVEY_NO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_YEAR", xfrm.LB_CON_YEAR)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_RCC_ROOF", xfrm.LB_RCC_ROOF)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PAID_DATE", xfrm.LB_PAID_DATE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_FROM", xfrm.LB_PERIOD_FROM)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_PERIOD_TO", xfrm.LB_PERIOD_TO)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_OTHERS", xfrm.LB_DOC_OTHERS)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DOC_NAME", xfrm.LB_DOC_NAME)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_OTHER_DETAIL", xfrm.LB_OTHER_DETAIL)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_TOT_P_AREA", xfrm.LB_TOT_P_AREA)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_CON_AREA", xfrm.LB_CON_AREA)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_DEPOSIT_AMT", xfrm.LB_DEPOSIT_AMT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_RENT", xfrm.LB_MONTH_RENT)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_MONTH_O_PAYMENTS", xfrm.LB_MONTH_O_PAYMENTS)

                                If LB_DOCS_ARRAY Is Nothing Then
                                    LB_DOCS_ARRAY = xfrm.LB_DOCS_ARRAY
                                Else
                                    If LB_DOCS_ARRAY.Rows.Count <= 0 Then
                                        LB_DOCS_ARRAY = New DataTable
                                        With LB_DOCS_ARRAY
                                            .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
                                            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                        End With
                                    End If
                                    'delete any previously added docs for same l&b
                                    Dim New_LB_DOCS_ARRAY As DataTable = LB_DOCS_ARRAY.Clone()
                                    For Each XROW As DataRow In LB_DOCS_ARRAY.Rows
                                        If Not XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                            New_LB_DOCS_ARRAY.ImportRow(XROW)
                                        End If
                                    Next
                                    LB_DOCS_ARRAY = New_LB_DOCS_ARRAY
                                    If Not xfrm.LB_DOCS_ARRAY Is Nothing Then
                                        For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                            Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                            Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                            Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                            LB_DOCS_ARRAY.Rows.Add(Row) 'add docs checked in current selection
                                        Next
                                    End If
                                End If

                                If LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                    LB_EXTENDED_PROPERTY_TABLE = xfrm.LB_EXTENDED_PROPERTY_TABLE
                                Else
                                    If LB_EXTENDED_PROPERTY_TABLE.Rows.Count <= 0 Then
                                        LB_EXTENDED_PROPERTY_TABLE = New DataTable
                                        With LB_EXTENDED_PROPERTY_TABLE
                                            .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
                                            .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
                                            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
                                            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
                                            .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
                                            .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
                                            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
                                            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                                        End With
                                    End If
                                    'delete any previously added extensions for same l&b
                                    Dim New_LB_EXTENDED_PROPERTY_TABLE As DataTable = LB_EXTENDED_PROPERTY_TABLE.Clone()
                                    For Each XROW As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                        If Not XROW("LB_REC_ID") = xfrm.LB_REC_ID Then
                                            New_LB_EXTENDED_PROPERTY_TABLE.ImportRow(XROW)
                                        End If
                                    Next
                                    LB_EXTENDED_PROPERTY_TABLE = New_LB_EXTENDED_PROPERTY_TABLE
                                    New_LB_EXTENDED_PROPERTY_TABLE.Dispose()
                                    If Not xfrm.LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                                        For Each XROW As DataRow In xfrm.LB_EXTENDED_PROPERTY_TABLE.Rows
                                            Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                                            Row("LB_MOU_DATE") = XROW("LB_MOU_DATE").ToString
                                            Row("LB_SR_NO") = XROW("LB_SR_NO").ToString
                                            Row("LB_INS_ID") = XROW("LB_INS_ID").ToString
                                            Row("LB_TOT_P_AREA") = Val(XROW("LB_TOT_P_AREA").ToString())
                                            Row("LB_CON_AREA") = Val(XROW("LB_CON_AREA").ToString())
                                            Row("LB_CON_YEAR") = XROW("LB_CON_YEAR").ToString()
                                            Row("LB_VALUE") = Val(XROW("LB_VALUE"))
                                            Row("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL").ToString()
                                            Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                            LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
                                        Next
                                    End If
                                End If
                            End If

                            If xfrm.iProfile.Equals("WIP") Then
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID", xfrm.Ref_RecID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "REFERENCE", xfrm.iReference)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE", xfrm.iRefType)
                            End If

                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                        End If
                        xfrm.Dispose()
                    End If
                Case "DELETE"
                    Dim xPromptWindow As New Common_Lib.Prompt_Window
                    If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                        If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                            Dim dView As DataView = DT.DefaultView
                            dView.Sort = "Sr." '#4643 fix
                            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
                                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "OTHER ASSETS" Or (Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Voucher_Type").ToString().ToUpper = "LAND & BUILDING" And Not Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "LAND & BUILDING") Then
                                    If Base.IsInsuranceAudited() Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s  V a l u e   C a n n o t   b e   C h a n g e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                                        Exit Sub
                                    End If
                                End If

                                If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "WIP" Then
                                    If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE").ToString() = "EXISTING" Then
                                        Dim RefId As Object = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "REF_REC_ID").ToString()
                                        If Not RefId Is Nothing Then
                                            If RefId.Length > 0 Then
                                                Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                                                Dim PROF_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID").ToString(), Nothing, Me.xMID.Text, Common_Lib.Common.Navigation_Mode._Delete, RefId)
                                                If Base._next_Unaudited_YearID <> Nothing Then
                                                    If PROF_TABLE.Rows(0)("Next Year Closing Value") < 0 Then
                                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Next Year for " & ROW("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End If

                                                If PROF_TABLE.Rows(0)("Curr Value") < 0 Then
                                                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry !  Deletion of Selected Payment Entry creates a Negative Closing Balance in Current Year for " & ROW("ITEM_PROFILE").ToString.ToLower & " with Original Value " & PROF_TABLE.Rows(0)("Org Value").ToString, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                              
                            End If
                           
                            dView(Me.GridView1.FocusedRowHandle).Delete()
                            Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                            Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                            Delete_Action = True
                        End If
                    End If
            End Select

            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                Sub_Amt_Calculation(Delete_Action)
            End If

        End If

        Me.GridView1.Focus()
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Journal Entry" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.TitleX.Text = "Journal Entry"
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        SetGridData()
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
            Me.T_VIEW.Enabled = True : Me.BUT_VIEW.Enabled = True
        End If

        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        ' Dim d1 As DataTable = Base._Payment_DBOps.GetMasterRecord(Me.xMID.Text)
        Dim d1 As DataTable = Base._Journal_voucher_DBOps.GetRecords(Me.xMID.Text)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Journal Voucher", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Dim GS As DataTable = Base._Gift_DBOps.GetGoldSilverList(Me.xMID.Text)
        Dim AI As DataTable = Base._Gift_DBOps.GetAssetList(Me.xMID.Text)
        Dim VI As DataTable = Base._Gift_DBOps.GetVehiclesList(Me.xMID.Text)
        Dim LS As DataTable = Base._Gift_DBOps.GetLiveStockList(Me.xMID.Text)
        Dim LB As DataTable = Base._Payment_DBOps.GetLandBuilingList(Me.xMID.Text)
        Dim WIP As DataTable = Base._Payment_DBOps.Get_WIP_List(Me.xMID.Text)
        If GS Is Nothing Or AI Is Nothing Or VI Is Nothing Or LS Is Nothing Or LB Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim JointData As DataSet = New DataSet()
        JointData.Tables.Add(d1.Copy)
        'GOLD/SILVER FOR ITEM_DETAIL
        JointData.Tables.Add(GS.Copy)
        Dim GS_Relation As DataRelation = JointData.Relations.Add("GS", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("Gold_Silver_Info").Columns("GS_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(GS_Relation)
                If XROW("TR_SR_NO") = _Row("GS_TR_ITEM_SRNO") Then
                    XROW("GS_DESC_MISC_ID") = _Row("GS_DESC_MISC_ID") : XROW("GS_ITEM_WEIGHT") = _Row("GS_ITEM_WEIGHT") : XROW("LOC_ID") = _Row("GS_LOC_AL_ID")
                End If
            Next
        Next
        'OTHER ASSETS 
        JointData.Tables.Add(AI.Copy)
        Dim AI_Relation As DataRelation = JointData.Relations.Add("AI", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("Asset_Info").Columns("AI_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(AI_Relation)
                If XROW("TR_SR_NO") = _Row("AI_TR_ITEM_SRNO") Then
                    XROW("AI_TYPE") = _Row("AI_TYPE") : XROW("AI_MAKE") = _Row("AI_MAKE") : XROW("AI_MODEL") = _Row("AI_MODEL") : XROW("AI_SERIAL_NO") = _Row("AI_SERIAL_NO") : XROW("AI_WARRANTY") = _Row("AI_WARRANTY") : XROW("AI_PUR_DATE") = _Row("AI_PUR_DATE") : XROW("Rate") = _Row("AI_RATE") : XROW("LOC_ID") = _Row("AI_LOC_AL_ID")
                End If
            Next
        Next
        'For Vehicles
        JointData.Tables.Add(VI.Copy)
        Dim VI_Relation As DataRelation = JointData.Relations.Add("VI", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("Vehicles_Info").Columns("VI_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(VI_Relation)
                If XROW("TR_SR_NO") = _Row("VI_TR_ITEM_SRNO") Then
                    XROW("VI_MAKE") = _Row("VI_MAKE") : XROW("VI_MODEL") = _Row("VI_MODEL") : XROW("VI_REG_NO_PATTERN") = _Row("VI_REG_NO_PATTERN") : XROW("VI_REG_NO") = _Row("VI_REG_NO")
                    XROW("VI_REG_DATE") = _Row("VI_REG_DATE") : XROW("VI_OWNERSHIP") = _Row("VI_OWNERSHIP") : XROW("VI_OWNERSHIP_AB_ID") = _Row("VI_OWNERSHIP_AB_ID") : XROW("VI_DOC_RC_BOOK") = _Row("VI_DOC_RC_BOOK") : XROW("VI_DOC_AFFIDAVIT") = _Row("VI_DOC_AFFIDAVIT")
                    XROW("VI_DOC_WILL") = _Row("VI_DOC_WILL") : XROW("VI_DOC_TRF_LETTER") = _Row("VI_DOC_TRF_LETTER") : XROW("VI_DOC_FU_LETTER") = _Row("VI_DOC_FU_LETTER") : XROW("VI_DOC_OTHERS") = _Row("VI_DOC_OTHERS") : XROW("VI_DOC_NAME") = _Row("VI_DOC_NAME") : XROW("VI_INSURANCE_ID") = _Row("VI_INSURANCE_ID")
                    XROW("VI_INS_POLICY_NO") = _Row("VI_INS_POLICY_NO") : XROW("VI_INS_EXPIRY_DATE") = _Row("VI_INS_EXPIRY_DATE") : XROW("LOC_ID") = _Row("VI_LOC_AL_ID")
                End If
            Next
        Next
        'FOR LiveStock
        JointData.Tables.Add(LS.Copy)
        Dim LS_Relation As DataRelation = JointData.Relations.Add("LS", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("Live_Stock_Info").Columns("LS_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(LS_Relation)
                If XROW("TR_SR_NO") = _Row("LS_TR_ITEM_SRNO") Then
                    XROW("LS_NAME") = _Row("LS_NAME") : XROW("LS_BIRTH_YEAR") = _Row("LS_BIRTH_YEAR") : XROW("LS_INSURANCE") = _Row("LS_INSURANCE") : XROW("LS_INSURANCE_ID") = _Row("LS_INSURANCE_ID") : XROW("LS_INS_POLICY_NO") = _Row("LS_INS_POLICY_NO") : XROW("LS_INS_AMT") = _Row("LS_INS_AMT") : XROW("LS_INS_DATE") = _Row("LS_INS_DATE") : XROW("LOC_ID") = _Row("LS_LOC_AL_ID")
                End If
            Next
        Next

        'FOR WIP
        JointData.Tables.Add(WIP.Copy)
        Dim WIP_Relation As DataRelation = JointData.Relations.Add("WIP", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("WIP_INFO").Columns("WIP_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(WIP_Relation)
                If XROW("TR_SR_NO") = _Row("WIP_TR_ITEM_SRNO") Then
                    XROW("WIP_REF") = _Row("WIP_REF") : XROW("WIP_REC_ID") = _Row("REC_ID") : XROW("WIP_REF_TYPE") = "NEW"
                End If
            Next
        Next

        'FOR Land&Building
        JointData.Tables.Add(LB.Copy)
        Dim LB_Relation As DataRelation = JointData.Relations.Add("LB", JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("Land_Building_info").Columns("LB_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(LB_Relation)
                If XROW("TR_SR_NO") = _Row("LB_TR_ITEM_SRNO") Then
                    XROW("LB_PRO_TYPE") = _Row("LB_PRO_TYPE") : XROW("LB_PRO_CATEGORY") = _Row("LB_PRO_CATEGORY") : XROW("LB_PRO_USE") = _Row("LB_PRO_USE") : XROW("LB_PRO_NAME") = _Row("LB_PRO_NAME")
                    XROW("LB_PRO_ADDRESS") = _Row("LB_PRO_ADDRESS") : XROW("LB_OWNERSHIP") = _Row("LB_OWNERSHIP") : XROW("LB_OWNERSHIP_PARTY_ID") = _Row("LB_OWNERSHIP_PARTY_ID") : XROW("LB_SURVEY_NO") = _Row("LB_SURVEY_NO")
                    XROW("LB_CON_YEAR") = _Row("LB_CON_YEAR") : XROW("LB_RCC_ROOF") = _Row("LB_RCC_ROOF") : XROW("LB_PAID_DATE") = _Row("LB_PAID_DATE") : XROW("LB_PERIOD_FROM") = _Row("LB_PERIOD_FROM")
                    XROW("LB_PERIOD_TO") = _Row("LB_PERIOD_TO") : XROW("LB_DOC_OTHERS") = _Row("LB_DOC_OTHERS") : XROW("LB_DOC_NAME") = _Row("LB_DOC_NAME") : XROW("LB_OTHER_DETAIL") = _Row("LB_OTHER_DETAIL")
                    XROW("LB_TOT_P_AREA") = _Row("LB_TOT_P_AREA") : XROW("LB_CON_AREA") = _Row("LB_CON_AREA") : XROW("LB_DEPOSIT_AMT") = _Row("LB_DEPOSIT_AMT") : XROW("LB_MONTH_RENT") = _Row("LB_MONTH_RENT") : XROW("LB_MONTH_O_PAYMENTS") = _Row("LB_MONTH_O_PAYMENTS") : XROW("LB_REC_ID") = _Row("LB_REC_ID")
                    XROW("LB_ADDRESS1") = _Row("LB_ADDRESS1") : XROW("LB_ADDRESS2") = _Row("LB_ADDRESS2") : XROW("LB_ADDRESS3") = _Row("LB_ADDRESS3") : XROW("LB_ADDRESS4") = _Row("LB_ADDRESS4") : XROW("LB_COUNTRY_ID") = _Row("LB_COUNTRY_ID") : XROW("LB_STATE_ID") = _Row("LB_STATE_ID")
                    XROW("LB_DISTRICT_ID") = _Row("LB_DISTRICT_ID") : XROW("LB_CITY_ID") = _Row("LB_CITY_ID") : XROW("LB_PINCODE") = _Row("LB_PINCODE")
                End If
            Next
        Next

        'Docs 
        LB_DOCS_ARRAY = New DataTable
        With LB_DOCS_ARRAY
            .Columns.Add("LB_MISC_ID", Type.GetType("System.String"))
            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
        End With

        For Each LBRow As DataRow In LB.Rows
            Dim docs As DataTable = Base._L_B_DBOps.GetDocumentRecord(LBRow("LB_REC_ID").ToString(), Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)
            For Each docRow As DataRow In docs.Rows
                Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                Row("LB_MISC_ID") = docRow("LB_MISC_ID").ToString()
                Row("LB_REC_ID") = docRow("LB_REC_ID").ToString()
                LB_DOCS_ARRAY.Rows.Add(Row)
            Next
        Next

        'Extended Property 
        LB_EXTENDED_PROPERTY_TABLE = New DataTable
        With LB_EXTENDED_PROPERTY_TABLE
            .Columns.Add("LB_SR_NO", Type.GetType("System.Double"))
            .Columns.Add("LB_INS_ID", Type.GetType("System.String"))
            .Columns.Add("LB_TOT_P_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_AREA", Type.GetType("System.Double"))
            .Columns.Add("LB_CON_YEAR", Type.GetType("System.String"))
            .Columns.Add("LB_MOU_DATE", Type.GetType("System.String"))
            .Columns.Add("LB_VALUE", Type.GetType("System.Double"))
            .Columns.Add("LB_OTHER_DETAIL", Type.GetType("System.String"))
            .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
        End With

        For Each LBRow As DataRow In LB.Rows
            Dim extensions As DataTable = Base._L_B_DBOps.GetExtendedRecord(LBRow("LB_REC_ID").ToString(), Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)
            For Each extensionsRow As DataRow In extensions.Rows
                Dim Row As DataRow = LB_EXTENDED_PROPERTY_TABLE.NewRow()
                Row("LB_MOU_DATE") = extensionsRow("LB_MOU_DATE").ToString
                Row("LB_SR_NO") = extensionsRow("LB_SR_NO").ToString
                Row("LB_INS_ID") = extensionsRow("LB_INS_ID").ToString
                Row("LB_TOT_P_AREA") = Val(extensionsRow("LB_TOT_P_AREA").ToString())
                Row("LB_CON_AREA") = Val(extensionsRow("LB_CON_AREA").ToString())
                Row("LB_CON_YEAR") = extensionsRow("LB_CON_YEAR").ToString()
                Row("LB_VALUE") = Val(extensionsRow("LB_VALUE"))
                Row("LB_OTHER_DETAIL") = extensionsRow("LB_OTHER_DETAIL").ToString()
                Row("LB_REC_ID") = extensionsRow("LB_REC_ID").ToString()
                LB_EXTENDED_PROPERTY_TABLE.Rows.Add(Row)
            Next
        Next

        'FOR Land&Building Expenses
        ' JointData.Tables.Add(d3.Copy)
        'Dim parentColumns As DataColumn() = New DataColumn() {JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_SR_NO")}
        'Dim childColumns As DataColumn() = New DataColumn() {JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("TRANSACTION_INFO").Columns("TR_SR_NO")}
        'Dim LB_Exp_Relation As DataRelation = JointData.Relations.Add("LB_Exp", parentColumns, childColumns, False)
        'For Each XROW In JointData.Tables(0).Rows
        '    For Each _Row In XROW.GetChildRows(LB_Exp_Relation)
        '        If XROW("ITEM_PROFILE") = "WIP" Then
        '            If XROW("WIP_REC_ID").ToString.Length = 0 Then
        '                XROW("WIP_REC_ID") = _Row("TR_TRF_CROSS_REF_ID")
        '                XROW("WIP_REF_TYPE") = "EXISTING"
        '            End If
        '        Else
        '            If XROW("LB_REC_ID").ToString.Length = 0 Then
        '                XROW("LB_REC_ID") = _Row("TR_TRF_CROSS_REF_ID") 'Reference to Land and Building entry for which the expense has been incurred 
        '            End If
        '        End If
        '    Next
        'Next

        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        For Each XROW In JointData.Tables(0).Rows
            Dim ROW As DataRow = DT.NewRow()
            ROW("Sr.") = XROW("TR_SR_NO")
            ROW("Item_ID") = XROW("TR_ITEM_ID")
            ROW("Trans_Type") = XROW("TR_TYPE")
            If ROW("Trans_Type").ToString.ToUpper = "DEBIT" Then ROW("Item_Led_ID") = XROW("TR_DR_LED_ID") Else ROW("Item_Led_ID") = XROW("TR_CR_LED_ID")
            ROW("Item_Voucher_Type") = XROW("Item_Voucher_Type")
            ROW("Item_Party_Req") = XROW("Item_Party_Req")
            ROW("Item_Profile") = XROW("Item_Profile")
            ROW("Item Name") = XROW("Item_Name")
            ROW("Head") = XROW("LED_NAME")
            ROW("Party") = XROW("PARTY")
            ROW("Qty.") = XROW("Qty")
            ROW("Rate") = XROW("Rate")
            ROW("Debit") = XROW("Debit")
            ROW("Credit") = XROW("Credit")
            ROW("Remarks") = XROW("TR_REMARKS")
            ROW("Pur_ID") = XROW("Pur_ID") 'Purpose ID
            ROW("Purpose") = XROW("Purpose") 'Purpose ID
            ROW("PartyID") = XROW("PartyID")
            ROW("Party_RecEditOn") = XROW("Party_RecEditOn")
            ROW("LOC_ID") = XROW("LOC_ID")
            'Gold/Silver
            ROW("GS_DESC_MISC_ID") = XROW("GS_DESC_MISC_ID")
            ROW("GS_ITEM_WEIGHT") = XROW("GS_ITEM_WEIGHT")
            'OTHER ASSET
            ROW("AI_TYPE") = XROW("AI_TYPE") 'Bug #5125 FIX
            ROW("AI_MAKE") = XROW("AI_MAKE")
            ROW("AI_MODEL") = XROW("AI_MODEL")
            ROW("AI_SERIAL_NO") = XROW("AI_SERIAL_NO")
            ROW("AI_WARRANTY") = XROW("AI_WARRANTY")
            ROW("AI_PUR_DATE") = XROW("AI_PUR_DATE")
            'LIVE STOCK
            ROW("LS_NAME") = XROW("LS_NAME")
            ROW("LS_BIRTH_YEAR") = XROW("LS_BIRTH_YEAR")
            ROW("LS_INSURANCE") = XROW("LS_INSURANCE")
            ROW("LS_INSURANCE_ID") = XROW("LS_INSURANCE_ID")
            ROW("LS_INS_POLICY_NO") = XROW("LS_INS_POLICY_NO")
            ROW("LS_INS_AMT") = XROW("LS_INS_AMT")
            ROW("LS_INS_DATE") = XROW("LS_INS_DATE")
            'VEHICLES
            ROW("VI_MAKE") = XROW("VI_MAKE")
            ROW("VI_MODEL") = XROW("VI_MODEL")
            ROW("VI_REG_NO_PATTERN") = XROW("VI_REG_NO_PATTERN")
            ROW("VI_REG_NO") = XROW("VI_REG_NO")
            ROW("VI_REG_DATE") = XROW("VI_REG_DATE")
            ROW("VI_OWNERSHIP") = XROW("VI_OWNERSHIP")
            ROW("VI_OWNERSHIP_AB_ID") = XROW("VI_OWNERSHIP_AB_ID")
            ROW("VI_DOC_RC_BOOK") = XROW("VI_DOC_RC_BOOK")
            ROW("VI_DOC_AFFIDAVIT") = XROW("VI_DOC_AFFIDAVIT")
            ROW("VI_DOC_WILL") = XROW("VI_DOC_WILL")
            ROW("VI_DOC_TRF_LETTER") = XROW("VI_DOC_TRF_LETTER")
            ROW("VI_DOC_FU_LETTER") = XROW("VI_DOC_FU_LETTER")
            ROW("VI_DOC_OTHERS") = XROW("VI_DOC_OTHERS")
            ROW("VI_DOC_NAME") = XROW("VI_DOC_NAME")
            ROW("VI_INSURANCE_ID") = XROW("VI_INSURANCE_ID")
            ROW("VI_INS_POLICY_NO") = XROW("VI_INS_POLICY_NO")
            ROW("VI_INS_EXPIRY_DATE") = XROW("VI_INS_EXPIRY_DATE")
            'Land & Building
            ROW("LB_PRO_TYPE") = XROW("LB_PRO_TYPE")
            ROW("LB_PRO_CATEGORY") = XROW("LB_PRO_CATEGORY")
            ROW("LB_PRO_USE") = XROW("LB_PRO_USE")
            ROW("LB_PRO_NAME") = XROW("LB_PRO_NAME")
            ROW("LB_PRO_ADDRESS") = XROW("LB_PRO_ADDRESS")
            ROW("LB_OWNERSHIP") = XROW("LB_OWNERSHIP")
            ROW("LB_OWNERSHIP_PARTY_ID") = XROW("LB_OWNERSHIP_PARTY_ID")
            ROW("LB_SURVEY_NO") = XROW("LB_SURVEY_NO")
            ROW("LB_CON_YEAR") = XROW("LB_CON_YEAR")
            ROW("LB_RCC_ROOF") = XROW("LB_RCC_ROOF")
            ROW("LB_PAID_DATE") = XROW("LB_PAID_DATE")
            ROW("LB_PERIOD_FROM") = XROW("LB_PERIOD_FROM")
            ROW("LB_PERIOD_TO") = XROW("LB_PERIOD_TO")
            ROW("LB_DOC_OTHERS") = XROW("LB_DOC_OTHERS")
            ROW("LB_DOC_NAME") = XROW("LB_DOC_NAME")
            ROW("LB_OTHER_DETAIL") = XROW("LB_OTHER_DETAIL")
            ROW("LB_TOT_P_AREA") = XROW("LB_TOT_P_AREA")
            ROW("LB_CON_AREA") = XROW("LB_CON_AREA")
            ROW("LB_DEPOSIT_AMT") = XROW("LB_DEPOSIT_AMT")
            ROW("LB_MONTH_RENT") = XROW("LB_MONTH_RENT")
            ROW("LB_MONTH_O_PAYMENTS") = XROW("LB_MONTH_O_PAYMENTS")
            ROW("LB_REC_ID") = XROW("LB_REC_ID")
            ROW("LB_ADDRESS1") = XROW("LB_ADDRESS1")
            ROW("LB_ADDRESS2") = XROW("LB_ADDRESS2")
            ROW("LB_ADDRESS3") = XROW("LB_ADDRESS3")
            ROW("LB_ADDRESS4") = XROW("LB_ADDRESS4")
            ROW("LB_STATE_ID") = XROW("LB_STATE_ID")
            ROW("LB_DISTRICT_ID") = XROW("LB_DISTRICT_ID")
            ROW("LB_CITY_ID") = XROW("LB_CITY_ID")
            ROW("LB_PINCODE") = XROW("LB_PINCODE")
            'ROW("LB_ADDRESS") = XRow("LB_ADDRESS")
            'WIP
            ROW("REF_REC_ID") = IIf(XROW("WIP_REF_TYPE") = "NEW", "", XROW("WIP_REC_ID"))
            ROW("REFERENCE") = XROW("WIP_REF")
            ROW("WIP_REF_TYPE") = XROW("WIP_REF_TYPE")

            Dim ReferencePresent As Boolean = True
            If XROW("TR_TRF_CROSS_REF_ID") Is Nothing Or IsDBNull(XROW("TR_TRF_CROSS_REF_ID")) Then
                ReferencePresent = False
            Else
                If XROW("TR_TRF_CROSS_REF_ID").ToString.Length = 0 Then ReferencePresent = False
            End If

            Dim xTemp_AssetID As String = ""
            Dim RefId As String = ""
            Dim Response As String
            Select Case ROW("Item_Profile").ToString.ToUpper

                Case "ADVANCES"
                    Response = Base._Voucher_DBOps.GetRaisedAdvanceRecID(Me.xMID.Text)
                    If Response Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If Response.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "OTHER DEPOSITS"
                    Response = Base._Voucher_DBOps.GetRaisedDepositRecID(Me.xMID.Text)
                    If Response Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If Response.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "OTHER LIABILITIES"
                    Response = Base._Voucher_DBOps.GetRaisedLiabilityRecID(Me.xMID.Text)
                    If Response Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If Response.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "GOLD", "SILVER"
                    xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.GOLD_SILVER_INFO, Me.xMID.Text)
                    If xTemp_AssetID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If xTemp_AssetID.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "OTHER ASSETS"
                    xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.ASSET_INFO, Me.xMID.Text)
                    If xTemp_AssetID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If xTemp_AssetID.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "LIVESTOCK"
                    xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LIVE_STOCK_INFO, Me.xMID.Text)
                    If xTemp_AssetID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If xTemp_AssetID.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "VEHICLES"
                    xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.VEHICLES_INFO, Me.xMID.Text)
                    If xTemp_AssetID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If xTemp_AssetID.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "LAND & BUILDING"
                    xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, Me.xMID.Text)
                    If xTemp_AssetID Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If xTemp_AssetID.Length > 0 And Not ReferencePresent Then ROW("Addition") = True Else ROW("Addition") = False
                Case "WIP"
                    If ROW("WIP_REF_TYPE") = "NEW" Then ROW("Addition") = True Else ROW("Addition") = False
                Case Else
                    ROW("Addition") = False
            End Select
            ROW("CrossRefID") = XROW("TR_TRF_CROSS_REF_ID")
            ROW("CrossReference") = XROW("CROSS_REFERENCE")

            If Base.AllowMultiuser() Then

                If (XROW("Item_Profile").ToString.ToUpper <> "NOT APPLICABLE" Or XROW("Item_Voucher_Type").ToString.ToUpper = "LAND & BUILDING") And ROW("Addition") = False Then 'Allows Profile or Construction Adjustment Entries only and leaves out Profile Creation / Expense / Income Entries
                    If XROW("TR_TRF_CROSS_REF_ID").ToString.Length > 0 Then
                        Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
                        Dim PROFILE_TABLE As DataTable = jrnl_Item.GetReferenceData(XROW("Item_Profile"), XROW("TR_ITEM_ID"), XROW("PartyID").ToString(), Me.xMID.Text, Me.Tag, XROW("TR_TRF_CROSS_REF_ID"))
                        If PROFILE_TABLE Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        If PROFILE_TABLE.Rows.Count > 0 Then
                            ROW("RefItem_RecEditOn") = PROFILE_TABLE.Rows(0)("REC_EDIT_ON")
                        End If
                    End If
                End If
                'Else   Bug #5931 fix
                '    ROW("RefItem_RecEditOn") = ""
            End If
            DT.Rows.Add(ROW)
        Next

        Txt_Narration.Text = d1.Rows(0)("TR_NARRATION")
        Txt_Reference.Text = d1.Rows(0)("TR_REFERENCE")
        Sub_Amt_Calculation(False)
        Calculation_Check()
        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_DiffAmt.Enabled = False : Me.Txt_DiffAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_DrTotal.Enabled = False : Me.Txt_DrTotal.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_DrTotal.Enabled = False : Me.Txt_DrTotal.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.BUT_NEW.Enabled = False : Me.BUT_EDIT.Enabled = False : Me.BUT_DELETE.Enabled = False : Me.BUT_VIEW.Enabled = False
        Me.T_New.Enabled = False : Me.T_Edit.Enabled = False : Me.T_Delete.Enabled = False : Me.T_VIEW.Enabled = False

    End Sub
    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.Txt_V_Date)
    End Sub
    Private Function FindLocationUsage(PropertyID As String, Optional Exclude_Sold_TF As Boolean = True, Optional Adjusted As Boolean = False) As String
        Dim Message As String = ""
        Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID)
        For Each cRow As DataRow In Locations.Rows
            Dim LocationID As String = cRow(0).ToString()
            Dim UsedPage As String = Base._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF)
            Dim DeleteAllow As Boolean = True
            If UsedPage.Length > 0 Then DeleteAllow = False
            If Not DeleteAllow Then
                If Adjusted Then
                    Message = "P r o p e r t y   A d j u s t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                Else
                    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                        Message = "C a n ' t   D e l e t e . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                    Else
                        Message = "C a n ' t   E d i t . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                    End If
                End If
                Exit For
            End If
        Next
        Return Message
    End Function
    Private Function CloneRow(dRow As DataRow) As DataRow
        Dim dtNew As DataTable = dRow.Table.Clone()
        Dim new_DRow As DataRow = dtNew.NewRow()
        new_DRow.ItemArray = dRow.ItemArray
        Return new_DRow
    End Function

#End Region

#Region "Start--> LookupEdit Events"

#End Region

End Class