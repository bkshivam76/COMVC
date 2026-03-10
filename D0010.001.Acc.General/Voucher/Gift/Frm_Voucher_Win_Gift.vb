Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering

Public Class Frm_Voucher_Win_Gift

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iTrans_Type As String = ""
    Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""
    Dim Cnt_BankAccount As Integer
    Private iParty_Req As Boolean = False
    Private LB_DOCS_ARRAY As DataTable
    Private LB_EXTENDED_PROPERTY_TABLE As DataTable
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
            Me.DataNavigation("NEW")
        End If
        Me.GLookUp_PartyList1.Focus()
    End Sub
#End Region

#Region "Start--> Button Events"

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If (keyData = (Keys.Control Or Keys.A)) Then ' Add Party / Person
                But_PersAdd_Click(Nothing, Nothing)
                Return (True)
            End If
            If (keyData = (Keys.Control Or Keys.M)) Then ' Manage Party / Person
                But_PersManage_Click(Nothing, Nothing)
                Return (True)
            End If
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

        '.................................................................................
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            For Each XRow In DT.Rows
                If XRow("Item_Profile") = "LAND & BUILDING" Or XRow("Item_Profile") = "OTHER ASSETS" Then
                    If Base.IsInsuranceAudited() Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s    C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If

                If XRow("ITEM_VOUCHER_TYPE").Trim.ToUpper = "LAND & BUILDING" And Not XRow("Item_Profile").ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
                    If Base.IsInsuranceAudited() Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y   R e l a t e d   E x p e n s e s   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
                '--------Check for Creation date of WIP---------
                'If Not XRow("WIP_REF_TYPE") Is Nothing And Not IsDBNull(XRow("WIP_REF_TYPE")) Then
                '    If XRow("WIP_REF_TYPE") = "EXISTING" Then
                '        Dim WIP_ID = IIf(IsDBNull(XRow("REF_REC_ID")), Nothing, XRow("REF_REC_ID"))
                '        Dim creationStats As DataTable = Base._WIPCretionVouchers.GetRefCreationDateByWIPID(WIP_ID)
                '        If Not creationStats Is Nothing Then
                '            If creationStats.Rows.Count > 0 Then
                '                If creationStats.Rows(0)("TR_DATE") > Txt_V_Date.DateTime Then
                '                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                '                    Me.ToolTip1.Show("Referencing Voucher Date must be greater than creation Date(" & Convert.ToDateTime(creationStats.Rows(0)("TR_DATE")).ToString(Base._Date_Format_DD_MMM_YYYY) & ") of WIP namely " & creationStats.Rows(0)("WIP_REF").ToString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                '                    Me.GridView1.Focus()
                '                    Me.DialogResult = Windows.Forms.DialogResult.None
                '                    Exit Sub
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
            Next
        End If
        '...................................................................................

        '-----------------------------+
        'Start : Check if entry already changed 
        '-----------------------------+
        If Base.AllowMultiuser() Then
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim gift_DbOps As DataTable = Base._Gift_DBOps.GetRecord(Me.xMID.Text)
                If gift_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If gift_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Gift"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(gift_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Gift"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If


                Dim MaxValue As Object = 0
                MaxValue = Base._Gift_DBOps.GetStatus(Me.xMID.Text)
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
                    Else 'Non Profile Entries 
                        Dim RefID As String = Base._Voucher_DBOps.GetReferenceRecordID(Me.xMID.Text)
                        If Not RefID Is Nothing Then
                            If RefID.Length > 0 Then
                                Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(RefID) 'checks if the referred property for constt items has been sold 
                                If Not SaleRecord Is Nothing Then
                                    If SaleRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                                Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, RefID) 'checks if the referred property for constt items has been transfered 
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for editing this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

            End If
        End If
        '-----------------------------+
        'End : Check if entry already changed 
        '-----------------------------+


        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If

            If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D o n o r   N o t   S e l e c t e d . . . !", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
                Me.GLookUp_PartyList1.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
            End If
            If Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then Me.GLookUp_PartyList1.Tag = ""
            If (BE_ADD1.Trim.Length <= 0) Or (Me.BE_City.Text.Trim.Length <= 0) Or (BE_DISTRICT.Trim.Length <= 0) Or (BE_STATE.Trim.Length <= 0) Or (BE_COUNTRY.Trim.Length <= 0) Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("D o n o r   A d d r e s s   I n c o m p l e t e . . . !" & vbNewLine & "Mandatory: Address Line.1, City, District, State & Country...", Me.GLookUp_PartyList1, 0, Me.GLookUp_PartyList1.Height, 5000)
                Me.GLookUp_PartyList1.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
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

            If Me.GridView1.RowCount <= 0 Then
                DevExpress.XtraEditors.XtraMessageBox.Show("I t e m   D e t a i l   N o t   S p e c i f i e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.GridView1.Focus()
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

                Dim d1 As DataTable = Base._Gift_DBOps.GetParties(GLookUp_PartyList1.Tag) 'party not deleted yet
                If d1 Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If d1.Rows.Count <= 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'Check for full address of donor
                If d1.Rows(0)("C_R_ADD1").ToString.Length <= 0 Or d1.Rows(0)("CI_NAME").ToString.Length <= 0 Or d1.Rows(0)("ST_NAME").ToString.Length <= 0 Or d1.Rows(0)("DI_NAME").ToString.Length <= 0 Or d1.Rows(0)("CO_NAME").ToString.Length <= 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Address Book"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

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
                    If XRow("Item_Profile") = "LAND & BUILDING" Then
                        For Each cRow In DT.Rows
                            If IIf(IsDBNull(XRow("LB_PRO_NAME")), "", XRow("LB_PRO_NAME")) = IIf(IsDBNull(cRow("LB_PRO_NAME")), "", cRow("LB_PRO_NAME")) And IIf(IsDBNull(XRow("LB_REC_ID")), "", XRow("LB_REC_ID")) <> IIf(IsDBNull(cRow("LB_REC_ID")), "", cRow("LB_REC_ID")) Then
                                DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y / L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e   i n   s a m e   v o u c h e r. . . !", "Property Name Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                'Me.DialogResult = Windows.Forms.DialogResult.Retry
                                'FormClosingEnable = False : Me.Close()
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

        'CHECKING LOCK STATUS
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
        '    Dim MaxValue As Object = 0
        '    MaxValue = Base._Gift_DBOps.GetStatus(Me.xMID.Text)
        '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
        'End If

        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted


        '+----JV LEDGER DETAIL----+
        Dim JV_Item_ID As String = "d0a33061-d679-4f21-ac12-a29541de8fcb" : Dim JV_Cr_Led_id As String = ""
        'Dim GIFT_SQL_1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & JV_Item_ID & "' AND UCASE(ITEM_TRANS_TYPE)='CREDIT' "
        Dim GIFT_DT As DataTable = Base._Gift_DBOps.GetItemsList(JV_Item_ID)
        If GIFT_DT Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If GIFT_DT.Rows.Count > 0 Then
            JV_Cr_Led_id = GIFT_DT.Rows(0)("ITEM_LED_ID")
        Else
            DevExpress.XtraEditors.XtraMessageBox.Show("Donation - Gift Item Not Found..!", "Donation By Gift...", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Exit Sub
        End If
        '+----------END-----------+

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherGift = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherGift

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            'Dim xDate As DateTime = Nothing : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Dim STR1 As String = "" : Dim xCnt As Integer = 1
            Dim InMInfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherGift = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherGift()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
            'InMInfo.TDate = Me.Txt_V_Date.Text
            InMInfo.PartyID = Me.GLookUp_PartyList1.Tag
            InMInfo.SubTotal = Val(Me.Txt_SubTotal.Text)
            InMInfo.Cash = 0
            InMInfo.Bank = 0
            InMInfo.Advance = 0
            InMInfo.Liability = 0
            InMInfo.Credit = 0
            InMInfo.TDS = 0
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            'If Not Base._Gift_DBOps.InsertMasterInfo(InMInfo) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertMaster = InMInfo

            Dim Insert(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift
            Dim InsertItem(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift
            Dim InsertPurpose(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift
            Dim InsertGS(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver
            Dim InsertAssets(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets
            Dim InsertLivestock(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock
            Dim InsertVehicles(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles
            Dim InsertProperty(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding
            Dim InsertReferencesWIP(DT.Rows.Count) As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile

            For Each XRow In DT.Rows
                Dim Cross_Ref_ID As String = ""
                Dim ScreenCode As Common_Lib.Common.Voucher_Screen_Code = Common_Lib.Common.Voucher_Screen_Code.Payment

                If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then 'Bug #5637
                    Cross_Ref_ID = XRow("LB_REC_ID")
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

                If XRow("REF_REC_ID").ToString.Length > 0 Then
                    Cross_Ref_ID = "'" & XRow("REF_REC_ID") & "'"
                End If

                Me.xID.Text = System.Guid.NewGuid().ToString()
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift = New Common_Lib.RealTimeService.Parameter_Insert_VoucherGift()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text
                InParam.ItemID = XRow("Item_ID")
                InParam.Type = "DEBIT"
                InParam.Cr_Led_ID = ""
                InParam.Dr_Led_ID = XRow("Item_Led_ID")
                InParam.Sub_Cr_Led_ID = ""
                InParam.Mode = "GIFT"
                InParam.Ref_No = ""
                InParam.Amount = Val(XRow("Amount"))
                InParam.PartyID = Me.GLookUp_PartyList1.Tag
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = XRow("Remarks")
                InParam.Reference = Me.Txt_Reference.Text
                InParam.Tr_M_ID = Me.xMID.Text
                InParam.TxnSrNo = XRow("Sr.")
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text
                InParam.Cross_Ref_Id = Cross_Ref_ID

                'If Not Base._Gift_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                Insert(xCnt) = InParam
                xCnt += 1
            Next
            InNewParam.Insert = Insert

            'JV Entry
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift = New Common_Lib.RealTimeService.Parameter_Insert_VoucherGift()
            InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
            InParam1.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
            'InParam1.TDate = Me.Txt_V_Date.Text
            InParam1.ItemID = JV_Item_ID
            InParam1.Type = "CREDIT"
            InParam1.Cr_Led_ID = JV_Cr_Led_id
            InParam1.Dr_Led_ID = ""
            InParam1.Sub_Cr_Led_ID = ""
            InParam1.Mode = "GIFT"
            InParam1.Ref_No = ""
            InParam1.Amount = Val(Txt_SubTotal.Text)
            InParam1.PartyID = Me.GLookUp_PartyList1.Tag
            InParam1.Narration = Me.Txt_Narration.Text
            InParam1.Remarks = ""
            InParam1.Reference = Me.Txt_Reference.Text
            InParam1.Tr_M_ID = Me.xMID.Text
            InParam1.TxnSrNo = xCnt
            InParam1.Status_Action = Status_Action
            InParam1.RecID = Me.xID.Text
            InParam1.Cross_Ref_Id = ""

            'If Not Base._Gift_DBOps.Insert(InParam1) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            InNewParam.param_InsertJV = InParam1
            STR1 = ""


            'Main Items
            Dim cnt As Integer = 0

            For Each XRow In DT.Rows
                Dim InItem As Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift = New Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift()
                InItem.Txn_M_ID = Me.xMID.Text
                InItem.TxnSrNo = XRow("Sr.")
                InItem.ItemID = XRow("Item_ID")
                InItem.LedID = XRow("Item_Led_ID")
                InItem.Type = XRow("Item_Trans_Type")
                InItem.PartyReq = XRow("Item_Party_Req")
                InItem.Profile = XRow("Item_Profile")
                InItem.ItemName = XRow("Item Name")
                InItem.Head = XRow("Head")
                InItem.Qty = Val(XRow("Qty."))
                InItem.Unit = XRow("Unit")
                InItem.Rate = Val(XRow("Rate"))
                InItem.Amount = Val(XRow("Amount"))
                InItem.Remarks = XRow("Remarks")
                InItem.Status_Action = Status_Action
                InItem.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Gift_DBOps.InsertItem(InItem) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InsertItem(cnt) = InItem

                'Purpose.........

                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift()
                InPurpose.TxnID = Me.xMID.Text
                InPurpose.PurposeID = XRow("PUR_ID")
                InPurpose.Amount = Val(XRow("Amount"))
                InPurpose.ItemSrNo = XRow("Sr.")
                InPurpose.Status_Action = Status_Action
                InPurpose.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Gift_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InsertPurpose(cnt) = InPurpose

                If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver()
                    InParam.Type = IIf(IsDBNull(XRow("Item_Profile")), Nothing, XRow("Item_Profile"))
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.DescMiscID = IIf(IsDBNull(XRow("GS_DESC_MISC_ID")), Nothing, XRow("GS_DESC_MISC_ID"))
                    InParam.Weight = IIf(IsDBNull(XRow("GS_ITEM_WEIGHT")), Nothing, Val(XRow("GS_ITEM_WEIGHT")))
                    InParam.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InParam.TxnID = Me.xMID.Text
                    InParam.TxnSrno = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                    InParam.Amount = Val(XRow("Amount"))
                    InParam.Status_Action = Status_Action
                    InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._GoldSilverDBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertGS(cnt) = InParam
                End If
                If XRow("Item_Profile") = "OTHER ASSETS" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                    InParam.AssetType = IIf(IsDBNull(XRow("AI_TYPE")), Nothing, XRow("AI_TYPE"))
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.Make = IIf(IsDBNull(XRow("AI_MAKE")), Nothing, XRow("AI_MAKE"))
                    InParam.Model = IIf(IsDBNull(XRow("AI_MODEL")), Nothing, XRow("AI_MODEL"))
                    InParam.SrNo = IIf(IsDBNull(XRow("AI_SERIAL_NO")), Nothing, XRow("AI_SERIAL_NO"))
                    InParam.Rate = IIf(IsDBNull(XRow("Rate")), Nothing, Val(XRow("Rate")))
                    InParam.InsAmount = IIf(IsDBNull(XRow("AI_INS_AMT")), Nothing, Val(XRow("AI_INS_AMT")))
                    If IsDate(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))) Then InParam.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))).ToString(Base._Server_Date_Format_Short) Else InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                    'InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                    InParam.PurchaseAmount = IIf(InParam.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))), 0) 'http://pm.bkinfo.in/issues/5345#note-12
                    InParam.Warranty = IIf(IsDBNull(XRow("AI_WARRANTY")), Nothing, Val(XRow("AI_WARRANTY")))
                    InParam.Image = IIf(IsDBNull(XRow("AI_IMAGE")), Nothing, XRow("AI_IMAGE"))
                    InParam.Quantity = IIf(IsDBNull(XRow("Qty.")), Nothing, Val(XRow("Qty.")))
                    InParam.LocationId = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InParam.TxnID = Me.xMID.Text
                    InParam.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InParam.Status_Action = Status_Action
                    InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._AssetDBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertAssets(cnt) = InParam
                End If
                If XRow("Item_Profile") = "LIVESTOCK" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock()
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.Name = IIf(IsDBNull(XRow("LS_NAME")), Nothing, XRow("LS_NAME"))
                    InParam.Year = IIf(IsDBNull(XRow("LS_BIRTH_YEAR")), Nothing, XRow("LS_BIRTH_YEAR"))
                    InParam.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InParam.Insurance = IIf(IsDBNull(XRow("LS_INSURANCE")), Nothing, XRow("LS_INSURANCE"))
                    InParam.InsuranceID = IIf(IsDBNull(XRow("LS_INSURANCE_ID")), Nothing, XRow("LS_INSURANCE_ID"))
                    InParam.PolicyNo = IIf(IsDBNull(XRow("LS_INS_POLICY_NO")), Nothing, XRow("LS_INS_POLICY_NO"))
                    InParam.InsAmount = IIf(IsDBNull(XRow("LS_INS_AMT")), Nothing, Val(XRow("LS_INS_AMT")))
                    If IsDate(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))) Then InParam.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))).ToString(Base._Server_Date_Format_Short) Else InParam.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                    'InParam.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                    InParam.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InParam.TxnID = Me.xMID.Text
                    InParam.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                    InParam.Status_Action = Status_Action
                    InParam.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._LiveStockDBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertLivestock(cnt) = InParam
                End If
                If XRow("Item_Profile") = "VEHICLES" Then
                    Dim InPms As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles()
                    InPms.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InPms.Make = IIf(IsDBNull(XRow("VI_MAKE")), Nothing, XRow("VI_MAKE"))
                    InPms.Model = IIf(IsDBNull(XRow("VI_MODEL")), Nothing, XRow("VI_MODEL"))
                    InPms.Reg_No_Pattern = IIf(IsDBNull(XRow("VI_REG_NO_PATTERN")), Nothing, XRow("VI_REG_NO_PATTERN"))
                    InPms.Reg_No = IIf(IsDBNull(XRow("VI_REG_NO")), Nothing, XRow("VI_REG_NO"))
                    If IsDate(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))) Then InPms.RegDate = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))).ToString(Base._Server_Date_Format_Short) Else InPms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                    'InPms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                    InPms.Ownership = IIf(IsDBNull(XRow("VI_OWNERSHIP")), Nothing, XRow("VI_OWNERSHIP"))
                    If Not XRow("VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(XRow("VI_OWNERSHIP_AB_ID")) Then
                        InPms.Ownership_AB_ID = IIf(Len(XRow("VI_OWNERSHIP_AB_ID")) = 0, Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                    End If
                    InPms.Doc_RC_Book = IIf(IsDBNull(XRow("VI_DOC_RC_BOOK")), Nothing, XRow("VI_DOC_RC_BOOK"))
                    InPms.Doc_Affidavit = IIf(IsDBNull(XRow("VI_DOC_AFFIDAVIT")), Nothing, XRow("VI_DOC_AFFIDAVIT"))
                    InPms.Doc_Will = IIf(IsDBNull(XRow("VI_DOC_WILL")), Nothing, XRow("VI_DOC_WILL"))
                    InPms.Doc_TRF_Letter = IIf(IsDBNull(XRow("VI_DOC_TRF_LETTER")), Nothing, XRow("VI_DOC_TRF_LETTER"))
                    InPms.DOC_FU_Letter = IIf(IsDBNull(XRow("VI_DOC_FU_LETTER")), Nothing, XRow("VI_DOC_FU_LETTER"))
                    InPms.Doc_Is_Others = IIf(IsDBNull(XRow("VI_DOC_OTHERS")), Nothing, XRow("VI_DOC_OTHERS"))
                    InPms.Doc_Others_Name = IIf(IsDBNull(XRow("VI_DOC_NAME")), Nothing, XRow("VI_DOC_NAME"))
                    If IsDBNull(XRow("VI_INSURANCE_ID")) Then
                        InPms.Insurance_ID = Nothing
                    ElseIf XRow("VI_INSURANCE_ID") = Nothing Then
                        InPms.Insurance_ID = Nothing
                    ElseIf XRow("VI_INSURANCE_ID").ToString.Length = 0 Then
                        InPms.Insurance_ID = Nothing
                    Else
                        InPms.Insurance_ID = XRow("VI_INSURANCE_ID")
                    End If
                    InPms.Ins_Policy_No = IIf(IsDBNull(XRow("VI_INS_POLICY_NO")), Nothing, XRow("VI_INS_POLICY_NO"))
                    If IsDate(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))) Then InPms.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))).ToString(Base._Server_Date_Format_Short) Else InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                    'InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                    InPms.Location_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InPms.Other_Details = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InPms.TxnID = Me.xMID.Text
                    InPms.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InPms.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InPms.Status_Action = Status_Action
                    InPms.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._VehicleDBOps.Insert(InPms) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertVehicles(cnt) = InPms
                End If
                If XRow("Item_Profile") = "LAND & BUILDING" Then
                    'Dim P_Date As DateTime = Nothing : Dim P_Date_Str As String = "" : If IsDate(XRow("LB_PAID_DATE")) Then : P_Date = XRow("LB_PAID_DATE") : P_Date_Str = "#" & P_Date.ToString(Base._Date_Format_Short) & "#" : Else : P_Date_Str = " NULL " : End If
                    'Dim From_Date As DateTime = Nothing : Dim From_Date_Str As String = "" : If IsDate(XRow("LB_PERIOD_FROM")) Then : From_Date = XRow("LB_PERIOD_FROM") : From_Date_Str = "#" & From_Date.ToString(Base._Date_Format_Short) & "#" : Else : From_Date_Str = " NULL " : End If
                    'Dim To_Date As DateTime = Nothing : Dim To_Date_Str As String = "" : If IsDate(XRow("LB_PERIOD_TO")) Then : To_Date = XRow("LB_PERIOD_TO") : To_Date_Str = "#" & To_Date.ToString(Base._Date_Format_Short) & "#" : Else : To_Date_Str = " NULL " : End If
                    Dim PartyID As String = "NULL"
                    If Not XRow("LB_OWNERSHIP_PARTY_ID").ToString = "NULL" Then PartyID = "'" & XRow("LB_OWNERSHIP_PARTY_ID") & "'"
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding()
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.PropertyType = IIf(IsDBNull(XRow("LB_PRO_TYPE")), Nothing, XRow("LB_PRO_TYPE"))
                    InParam.Category = IIf(IsDBNull(XRow("LB_PRO_CATEGORY")), Nothing, XRow("LB_PRO_CATEGORY"))
                    InParam.Use = IIf(IsDBNull(XRow("LB_PRO_USE")), Nothing, XRow("LB_PRO_USE"))
                    InParam.Name = IIf(IsDBNull(XRow("LB_PRO_NAME")), Nothing, Trim(XRow("LB_PRO_NAME")))
                    InParam.Address = IIf(IsDBNull(XRow("LB_PRO_ADDRESS")), Nothing, XRow("LB_PRO_ADDRESS"))
                    InParam.LB_Add1 = IIf(IsDBNull(XRow("LB_ADDRESS1")), Nothing, XRow("LB_ADDRESS1"))
                    InParam.LB_Add2 = IIf(IsDBNull(XRow("LB_ADDRESS2")), Nothing, XRow("LB_ADDRESS2"))
                    InParam.LB_Add3 = IIf(IsDBNull(XRow("LB_ADDRESS3")), Nothing, XRow("LB_ADDRESS3"))
                    InParam.LB_Add4 = IIf(IsDBNull(XRow("LB_ADDRESS4")), Nothing, XRow("LB_ADDRESS4"))
                    InParam.LB_CityID = IIf(IsDBNull(XRow("LB_CITY_ID")), Nothing, XRow("LB_CITY_ID"))
                    InParam.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                    InParam.LB_DisttID = IIf(IsDBNull(XRow("LB_DISTRICT_ID")), Nothing, XRow("LB_DISTRICT_ID"))
                    InParam.LB_PinCode = IIf(IsDBNull(XRow("LB_PINCODE")), Nothing, XRow("LB_PINCODE"))
                    InParam.LB_StateID = IIf(IsDBNull(XRow("LB_STATE_ID")), Nothing, XRow("LB_STATE_ID"))
                    InParam.Ownership = IIf(IsDBNull(XRow("LB_OWNERSHIP")), Nothing, XRow("LB_OWNERSHIP"))
                    InParam.Owner_Party_ID = PartyID
                    InParam.SurveyNo = IIf(IsDBNull(XRow("LB_SURVEY_NO")), Nothing, XRow("LB_SURVEY_NO"))
                    InParam.TotalArea = IIf(IsDBNull(XRow("LB_TOT_P_AREA")), Nothing, XRow("LB_TOT_P_AREA"))
                    InParam.ConstructedArea = IIf(IsDBNull(XRow("LB_CON_AREA")), Nothing, XRow("LB_CON_AREA"))
                    InParam.ConstructionYear = IIf(IsDBNull(XRow("LB_CON_YEAR")), Nothing, XRow("LB_CON_YEAR"))
                    InParam.RCCRoof = IIf(IsDBNull(XRow("LB_RCC_ROOF")), Nothing, XRow("LB_RCC_ROOF"))
                    InParam.DepositAmount = IIf(IsDBNull(XRow("LB_DEPOSIT_AMT")), Nothing, XRow("LB_DEPOSIT_AMT"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))) Then InParam.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))).ToString(Base._Server_Date_Format_Short) Else InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                    'InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                    InParam.MonthlyRent = IIf(IsDBNull(XRow("LB_MONTH_RENT")), Nothing, XRow("LB_MONTH_RENT"))
                    InParam.MonthlyOtherExpenses = IIf(IsDBNull(XRow("LB_MONTH_O_PAYMENTS")), Nothing, XRow("LB_MONTH_O_PAYMENTS"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))) Then InParam.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))).ToString(Base._Server_Date_Format_Short) Else InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                    'InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))) Then InParam.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))).ToString(Base._Server_Date_Format_Short) Else InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                    'InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                    InParam.OtherDocs = IIf(IsDBNull(XRow("LB_DOC_OTHERS")), Nothing, XRow("LB_DOC_OTHERS"))
                    InParam.DocNames = IIf(IsDBNull(XRow("LB_DOC_NAME")), Nothing, XRow("LB_DOC_NAME"))
                    InParam.Value = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("LB_OTHER_DETAIL")), Nothing, XRow("LB_OTHER_DETAIL"))
                    InParam.MasterID = Me.xMID.Text
                    InParam.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InParam.Status_Action = Status_Action
                    InParam.RecID = IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID"))

                    'If Not Base._L_B_DBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If

                    'EXTENSIONS 
                    Dim ExtInfo(LB_EXTENDED_PROPERTY_TABLE.Rows.Count - 1) As Common_Lib.RealTimeService.Parameter_InsertExtendedInfo_LandAndBuilding
                    Dim cnt1 As Integer = 0
                    If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                        For Each _Ext_Row As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                            If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
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
                    InParam.InsertExtInfo = ExtInfo
                    'DOCS
                    Dim DocInfo(LB_DOCS_ARRAY.Rows.Count - 1) As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding
                    cnt1 = 0
                    If Not LB_DOCS_ARRAY Is Nothing Then
                        For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                            If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
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
                    InParam.InsertDocInfo = DocInfo

                    'Add Location
                    Dim InAssetLoc As Common_Lib.RealTimeService.Param_AssetLoc_Insert = New Common_Lib.RealTimeService.Param_AssetLoc_Insert()
                    InAssetLoc.name = Trim(InParam.Name)
                    InAssetLoc.OtherDetails = "Use Type: " & InParam.PropertyType
                    InAssetLoc.Status_Action = Status_Action
                    InAssetLoc.Match_LB_ID = InParam.RecID
                    InAssetLoc.Match_SP_ID = ""
                    'If Not Base._AssetLocDBOps.Insert(ClientScreen.Accounts_Voucher_Gift, InParam.Name, "Use Type: " & InParam.PropertyType, Status_Action, "", InParam.RecID) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InParam.param_InsertAssetLoc = InAssetLoc
                    InsertProperty(cnt) = InParam
                End If

                '----------WIP References------------

                If XRow("Item_Profile").Equals("WIP") Then
                    If Not XRow("WIP_REF_TYPE") Is Nothing Then
                        If XRow("WIP_REF_TYPE") = "NEW" Then
                            Dim InReference As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile = New Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile()
                            InReference.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), Nothing, XRow("Item_Led_ID"))
                            InReference.Reference = IIf(IsDBNull(XRow("REFERENCE")), Nothing, XRow("REFERENCE"))
                            InReference.Amount = Val(XRow("AMOUNT"))
                            InReference.Status_Action = Status_Action
                            InReference.TxnID = Me.xMID.Text
                            InReference.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                            InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment
                            InsertReferencesWIP(cnt) = InReference
                        End If
                    End If
                End If

                cnt += 1
            Next
            InNewParam.InsertAssets = InsertAssets
            InNewParam.InsertGS = InsertGS
            InNewParam.InsertItem = InsertItem
            InNewParam.InsertLivestock = InsertLivestock
            InNewParam.InsertProperty = InsertProperty
            InNewParam.InsertVehicles = InsertVehicles
            InNewParam.InsertPurpose = InsertPurpose
            InNewParam.InsertReferencesWIP = InsertReferencesWIP

            If Not Base._Gift_DBOps.InsertGift_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim Message As String = "" : Dim IsLBIncluded As Boolean = False
        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherGift = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherGift
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            'Dim xDate As DateTime = Nothing : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
            Dim xCnt As Integer = 1
            Dim UpMaster As Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherGift = New Common_Lib.RealTimeService.Parameter_UpdateMaster_VoucherGift()
            UpMaster.VNo = Me.Txt_V_NO.Text
            If IsDate(Me.Txt_V_Date.Text) Then UpMaster.TDate = Convert.ToDateTime(Me.Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.TDate = Me.Txt_V_Date.Text
            'UpMaster.TDate = Me.Txt_V_Date.Text
            UpMaster.PartyID = Me.GLookUp_PartyList1.Tag
            UpMaster.SubTotal = Val(Me.Txt_SubTotal.Text)
            UpMaster.Cash = 0
            UpMaster.Bank = 0
            UpMaster.Advance = 0
            UpMaster.Liability = 0
            UpMaster.Credit = 0
            UpMaster.TDS = 0
            'UpMaster.Status_Action = Status_Action
            UpMaster.RecID = Me.xMID.Text

            'If Not Base._Gift_DBOps.UpdateMaster(UpMaster) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.param_UpdateMaster = UpMaster

            'If Not Base._Gift_DBOps.Delete(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_Delete = Me.xMID.Text

            EditParam.MID_ReferenceDelete = Me.xMID.Text

            '-------------

            Dim Insert(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift
            Dim InsertItem(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift
            Dim InsertPurpose(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift
            Dim InsertGS(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver
            Dim InsertAssets(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets
            Dim InsertLivestock(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock
            Dim InsertVehicles(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles
            Dim InsertProperty(DT.Rows.Count) As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding
            Dim InsertReferencesWIP(DT.Rows.Count) As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile

            For Each XRow In DT.Rows
                Dim Cross_Ref_ID As String = ""
                If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                    Cross_Ref_ID = XRow("LB_REC_ID")
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

                If XRow("REF_REC_ID").ToString.Length > 0 Then
                    Cross_Ref_ID = "'" & XRow("REF_REC_ID") & "'"
                End If

                Me.xID.Text = System.Guid.NewGuid().ToString()
                Dim InParam As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift = New Common_Lib.RealTimeService.Parameter_Insert_VoucherGift()
                InParam.TransCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
                InParam.VNo = Me.Txt_V_NO.Text
                If IsDate(Me.Txt_V_Date.Text) Then InParam.TDate = Convert.ToDateTime(Me.Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.TDate = Me.Txt_V_Date.Text
                'InParam.TDate = Me.Txt_V_Date.Text
                InParam.ItemID = XRow("Item_ID")
                InParam.Type = "DEBIT"
                InParam.Cr_Led_ID = ""
                InParam.Dr_Led_ID = XRow("Item_Led_ID")
                InParam.Sub_Cr_Led_ID = ""
                InParam.Mode = "GIFT"
                InParam.Ref_No = ""
                InParam.Amount = Val(XRow("Amount"))
                InParam.PartyID = Me.GLookUp_PartyList1.Tag
                InParam.Narration = Me.Txt_Narration.Text
                InParam.Remarks = XRow("Remarks")
                InParam.Reference = Me.Txt_Reference.Text
                InParam.Tr_M_ID = Me.xMID.Text
                InParam.TxnSrNo = XRow("Sr.")
                InParam.Status_Action = Status_Action
                InParam.RecID = Me.xID.Text
                InParam.Cross_Ref_Id = Cross_Ref_ID

                'If Not Base._Gift_DBOps.Insert(InParam) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                Insert(xCnt) = InParam
                xCnt += 1
            Next
            EditParam.Insert = Insert



            'JV Entry
            Me.xID.Text = System.Guid.NewGuid().ToString()
            Dim InParams As Common_Lib.RealTimeService.Parameter_Insert_VoucherGift = New Common_Lib.RealTimeService.Parameter_Insert_VoucherGift()
            InParams.TransCode = Common_Lib.Common.Voucher_Screen_Code.Donation_Gift
            InParams.VNo = Me.Txt_V_NO.Text
            If IsDate(Me.Txt_V_Date.Text) Then InParams.TDate = Convert.ToDateTime(Me.Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParams.TDate = Me.Txt_V_Date.Text
            'InParams.TDate = Me.Txt_V_Date.Text
            InParams.ItemID = JV_Item_ID
            InParams.Type = "CREDIT"
            InParams.Cr_Led_ID = JV_Cr_Led_id
            InParams.Dr_Led_ID = ""
            InParams.Sub_Cr_Led_ID = ""
            InParams.Mode = "GIFT"
            InParams.Ref_No = ""
            InParams.Amount = Val(Txt_SubTotal.Text)
            InParams.PartyID = Me.GLookUp_PartyList1.Tag
            InParams.Narration = Me.Txt_Narration.Text
            InParams.Remarks = ""
            InParams.Reference = Me.Txt_Reference.Text
            InParams.Tr_M_ID = Me.xMID.Text
            InParams.TxnSrNo = xCnt
            InParams.Status_Action = Status_Action
            InParams.RecID = Me.xID.Text
            InParams.Cross_Ref_Id = ""

            'If Not Base._Gift_DBOps.Insert(InParams) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.InsertJV = InParams
            'If Not Base._Gift_DBOps.DeleteItems(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteItems = Me.xMID.Text
            'If Not Base._Gift_DBOps.DeletePurpose(Me.xMID.Text) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeletePurpose = Me.xMID.Text
            'PROFILE ENTRIES DELETE
            Dim SQL_DEL As String = ""
            'If Not Base._GoldSilverDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteGS = Me.xMID.Text
            'If Not Base._AssetDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteAssets = Me.xMID.Text
            'If Not Base._LiveStockDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteLS = Me.xMID.Text
            'If Not Base._VehicleDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteVehicle = Me.xMID.Text

            Dim d1 As DataTable = Base._L_B_DBOps.GetIDsBytxnID(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift)


            Dim DelExtInfo(d1.Rows.Count - 1) As String
            Dim DelDocInfo(d1.Rows.Count - 1) As String
            Dim DelByLB(d1.Rows.Count - 1) As String
            Dim DelComplexBuildings(d1.Rows.Count - 1) As String
            Dim ctr As Integer = 0
            For Each cRow As DataRow In d1.Rows
                DelComplexBuildings(ctr) = cRow(0)
                'Remove existing Extensions
                'If Not Base._L_B_DBOps.DeleteExtendedInfo(cRow(0)) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelExtInfo(ctr) = cRow(0)
                'Remove existing docs
                'If Not Base._L_B_DBOps.DeleteDocumentInfo(cRow(0)) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelDocInfo(ctr) = cRow(0)

                'Remove Location
                'If Not Base._AssetLocDBOps.DeleteByLB(cRow(0), ClientScreen.Accounts_Voucher_Gift) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelByLB(ctr) = cRow(0)
                ctr += 1
            Next
            EditParam.DeleteComplexBuilding = DelComplexBuildings
            EditParam.DeleteDocumentInfo = DelDocInfo
            EditParam.DeleteExtendedInfo = DelExtInfo
            EditParam.DeleteByLB = DelByLB

            'Remove existing Land_Building_Info
            'If Not Base._L_B_DBOps.Delete(Me.xMID.Text, ClientScreen.Accounts_Voucher_Gift) Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            EditParam.MID_DeleteLandB = Me.xMID.Text

            'Main Items
            Dim cnt As Integer = 0
            For Each XRow In DT.Rows
                Dim InItem As Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift = New Common_Lib.RealTimeService.Parameter_InsertItem_VoucherGift()
                InItem.Txn_M_ID = Me.xMID.Text
                InItem.TxnSrNo = XRow("Sr.")
                InItem.ItemID = XRow("Item_ID")
                InItem.LedID = XRow("Item_Led_ID")
                InItem.Type = XRow("Item_Trans_Type")
                InItem.PartyReq = XRow("Item_Party_Req")
                InItem.Profile = XRow("Item_Profile")
                InItem.ItemName = XRow("Item Name")
                InItem.Head = XRow("Head")
                InItem.Qty = Val(XRow("Qty."))
                InItem.Unit = XRow("Unit")
                InItem.Rate = Val(XRow("Rate"))
                InItem.Amount = Val(XRow("Amount"))
                InItem.Remarks = XRow("Remarks")
                InItem.Status_Action = Status_Action
                InItem.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Gift_DBOps.InsertItem(InItem) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InsertItem(cnt) = InItem

                'Purpose.........
                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherGift()
                InPurpose.TxnID = Me.xMID.Text
                InPurpose.PurposeID = XRow("PUR_ID")
                InPurpose.Amount = Val(XRow("Amount"))
                InPurpose.ItemSrNo = XRow("Sr.")
                InPurpose.Status_Action = Status_Action
                InPurpose.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._Gift_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InsertPurpose(cnt) = InPurpose

                If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSRNo_GoldSilver()
                    InParam.Type = IIf(IsDBNull(XRow("Item_Profile")), Nothing, XRow("Item_Profile"))
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.DescMiscID = IIf(IsDBNull(XRow("GS_DESC_MISC_ID")), Nothing, XRow("GS_DESC_MISC_ID"))
                    InParam.Weight = IIf(IsDBNull(XRow("GS_ITEM_WEIGHT")), Nothing, Val(XRow("GS_ITEM_WEIGHT")))
                    InParam.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InParam.TxnID = Me.xMID.Text
                    InParam.TxnSrno = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                    InParam.Amount = Val(XRow("Amount"))
                    InParam.Status_Action = Status_Action
                    InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._GoldSilverDBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertGS(cnt) = InParam
                End If
                If XRow("Item_Profile") = "OTHER ASSETS" Then
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                    InParam.AssetType = IIf(IsDBNull(XRow("AI_TYPE")), Nothing, XRow("AI_TYPE"))
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.Make = IIf(IsDBNull(XRow("AI_MAKE")), Nothing, XRow("AI_MAKE"))
                    InParam.Model = IIf(IsDBNull(XRow("AI_MODEL")), Nothing, XRow("AI_MODEL"))
                    InParam.SrNo = IIf(IsDBNull(XRow("AI_SERIAL_NO")), Nothing, XRow("AI_SERIAL_NO"))
                    InParam.Rate = IIf(IsDBNull(XRow("Rate")), Nothing, Val(XRow("Rate")))
                    InParam.InsAmount = IIf(IsDBNull(XRow("AI_INS_AMT")), Nothing, Val(XRow("AI_INS_AMT")))
                    If IsDate(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))) Then InParam.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))).ToString(Base._Server_Date_Format_Short) Else InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                    'InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                    InParam.PurchaseAmount = IIf(InParam.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))), 0) 'http://pm.bkinfo.in/issues/5345#note-12
                    InParam.Warranty = IIf(IsDBNull(XRow("AI_WARRANTY")), Nothing, Val(XRow("AI_WARRANTY")))
                    InParam.Image = IIf(IsDBNull(XRow("AI_IMAGE")), Nothing, XRow("AI_IMAGE"))
                    InParam.Quantity = IIf(IsDBNull(XRow("Qty.")), Nothing, Val(XRow("Qty.")))
                    InParam.LocationId = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InParam.TxnID = Me.xMID.Text
                    InParam.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InParam.Status_Action = Status_Action
                    InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._AssetDBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertAssets(cnt) = InParam
                End If
                If XRow("Item_Profile") = "LIVESTOCK" Then
                    Dim InPms As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_LiveStock()
                    InPms.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InPms.Name = IIf(IsDBNull(XRow("LS_NAME")), Nothing, XRow("LS_NAME"))
                    InPms.Year = IIf(IsDBNull(XRow("LS_BIRTH_YEAR")), Nothing, XRow("LS_BIRTH_YEAR"))
                    InPms.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InPms.Insurance = IIf(IsDBNull(XRow("LS_INSURANCE")), Nothing, XRow("LS_INSURANCE"))
                    InPms.InsuranceID = IIf(IsDBNull(XRow("LS_INSURANCE_ID")), Nothing, XRow("LS_INSURANCE_ID"))
                    InPms.PolicyNo = IIf(IsDBNull(XRow("LS_INS_POLICY_NO")), Nothing, XRow("LS_INS_POLICY_NO"))
                    InPms.InsAmount = IIf(IsDBNull(XRow("LS_INS_AMT")), Nothing, Val(XRow("LS_INS_AMT")))
                    If IsDate(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))) Then InPms.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))).ToString(Base._Server_Date_Format_Short) Else InPms.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                    'InPms.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                    InPms.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InPms.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InPms.TxnID = Me.xMID.Text
                    InPms.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                    InPms.Status_Action = Status_Action
                    InPms.screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._LiveStockDBOps.Insert(InPms) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertLivestock(cnt) = InPms
                End If
                If XRow("Item_Profile") = "VEHICLES" Then
                    Dim InPrms As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Vehicles()

                    InPrms.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InPrms.Make = IIf(IsDBNull(XRow("VI_MAKE")), Nothing, XRow("VI_MAKE"))
                    InPrms.Model = IIf(IsDBNull(XRow("VI_MODEL")), Nothing, XRow("VI_MODEL"))
                    InPrms.Reg_No_Pattern = IIf(IsDBNull(XRow("VI_REG_NO_PATTERN")), Nothing, XRow("VI_REG_NO_PATTERN"))
                    InPrms.Reg_No = IIf(IsDBNull(XRow("VI_REG_NO")), Nothing, XRow("VI_REG_NO"))
                    If IsDate(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))) Then InPrms.RegDate = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))).ToString(Base._Server_Date_Format_Short) Else InPrms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                    'InPrms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                    InPrms.Ownership = IIf(IsDBNull(XRow("VI_OWNERSHIP")), Nothing, XRow("VI_OWNERSHIP"))
                    If Not XRow("VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(XRow("VI_OWNERSHIP_AB_ID")) Then
                        InPrms.Ownership_AB_ID = IIf(Len(XRow("VI_OWNERSHIP_AB_ID")) = 0, Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                    End If
                    InPrms.Doc_RC_Book = IIf(IsDBNull(XRow("VI_DOC_RC_BOOK")), Nothing, XRow("VI_DOC_RC_BOOK"))
                    InPrms.Doc_Affidavit = IIf(IsDBNull(XRow("VI_DOC_AFFIDAVIT")), Nothing, XRow("VI_DOC_AFFIDAVIT"))
                    InPrms.Doc_Will = IIf(IsDBNull(XRow("VI_DOC_WILL")), Nothing, XRow("VI_DOC_WILL"))
                    InPrms.Doc_TRF_Letter = IIf(IsDBNull(XRow("VI_DOC_TRF_LETTER")), Nothing, XRow("VI_DOC_TRF_LETTER"))
                    InPrms.DOC_FU_Letter = IIf(IsDBNull(XRow("VI_DOC_FU_LETTER")), Nothing, XRow("VI_DOC_FU_LETTER"))
                    InPrms.Doc_Is_Others = IIf(IsDBNull(XRow("VI_DOC_OTHERS")), Nothing, XRow("VI_DOC_OTHERS"))
                    InPrms.Doc_Others_Name = IIf(IsDBNull(XRow("VI_DOC_NAME")), Nothing, XRow("VI_DOC_NAME"))
                    If IsDBNull(XRow("VI_INSURANCE_ID")) Then
                        InPrms.Insurance_ID = Nothing
                    ElseIf XRow("VI_INSURANCE_ID") = Nothing Then
                        InPrms.Insurance_ID = Nothing
                    ElseIf XRow("VI_INSURANCE_ID").ToString.Length = 0 Then
                        InPrms.Insurance_ID = Nothing
                    Else
                        InPrms.Insurance_ID = XRow("VI_INSURANCE_ID")
                    End If
                    InPrms.Ins_Policy_No = IIf(IsDBNull(XRow("VI_INS_POLICY_NO")), Nothing, XRow("VI_INS_POLICY_NO"))
                    If IsDate(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))) Then InPrms.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))).ToString(Base._Server_Date_Format_Short) Else InPrms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                    'InPrms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                    InPrms.Location_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                    InPrms.Other_Details = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                    InPrms.TxnID = Me.xMID.Text
                    InPrms.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InPrms.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InPrms.Status_Action = Status_Action
                    InPrms.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift

                    'If Not Base._VehicleDBOps.Insert(InPrms) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertVehicles(cnt) = InPrms
                End If

                If XRow("Item_Profile") = "LAND & BUILDING" Then
                    Dim PartyID As String = "NULL"
                    If Not XRow("LB_OWNERSHIP_PARTY_ID").ToString = "NULL" Then PartyID = "'" & XRow("LB_OWNERSHIP_PARTY_ID") & "'"
                    If XRow("LB_OWNERSHIP_PARTY_ID").ToString.Length = 0 Then PartyID = "NULL"
                    Dim InParam As Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Common_Lib.RealTimeService.Parameter_InsertMasterIDAndSrNo_LandAndBuilding()
                    InParam.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                    InParam.PropertyType = IIf(IsDBNull(XRow("LB_PRO_TYPE")), Nothing, XRow("LB_PRO_TYPE"))
                    InParam.Category = IIf(IsDBNull(XRow("LB_PRO_CATEGORY")), Nothing, XRow("LB_PRO_CATEGORY"))
                    InParam.Use = IIf(IsDBNull(XRow("LB_PRO_USE")), Nothing, XRow("LB_PRO_USE"))
                    InParam.Name = IIf(IsDBNull(XRow("LB_PRO_NAME")), Nothing, Trim(XRow("LB_PRO_NAME")))
                    InParam.Address = IIf(IsDBNull(XRow("LB_PRO_ADDRESS")), Nothing, XRow("LB_PRO_ADDRESS"))
                    InParam.LB_Add1 = IIf(IsDBNull(XRow("LB_ADDRESS1")), Nothing, XRow("LB_ADDRESS1"))
                    InParam.LB_Add2 = IIf(IsDBNull(XRow("LB_ADDRESS2")), Nothing, XRow("LB_ADDRESS2"))
                    InParam.LB_Add3 = IIf(IsDBNull(XRow("LB_ADDRESS3")), Nothing, XRow("LB_ADDRESS3"))
                    InParam.LB_Add4 = IIf(IsDBNull(XRow("LB_ADDRESS4")), Nothing, XRow("LB_ADDRESS4"))
                    InParam.LB_CityID = IIf(IsDBNull(XRow("LB_CITY_ID")), Nothing, XRow("LB_CITY_ID"))
                    InParam.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                    InParam.LB_DisttID = IIf(IsDBNull(XRow("LB_DISTRICT_ID")), Nothing, XRow("LB_DISTRICT_ID"))
                    InParam.LB_PinCode = IIf(IsDBNull(XRow("LB_PINCODE")), Nothing, XRow("LB_PINCODE"))
                    InParam.LB_StateID = IIf(IsDBNull(XRow("LB_STATE_ID")), Nothing, XRow("LB_STATE_ID"))
                    InParam.Ownership = IIf(IsDBNull(XRow("LB_OWNERSHIP")), Nothing, XRow("LB_OWNERSHIP"))
                    InParam.Owner_Party_ID = PartyID
                    InParam.SurveyNo = IIf(IsDBNull(XRow("LB_SURVEY_NO")), Nothing, XRow("LB_SURVEY_NO"))
                    InParam.TotalArea = IIf(IsDBNull(XRow("LB_TOT_P_AREA")), Nothing, XRow("LB_TOT_P_AREA"))
                    InParam.ConstructedArea = IIf(IsDBNull(XRow("LB_CON_AREA")), Nothing, XRow("LB_CON_AREA"))
                    InParam.ConstructionYear = IIf(IsDBNull(XRow("LB_CON_YEAR")), Nothing, XRow("LB_CON_YEAR"))
                    InParam.RCCRoof = IIf(IsDBNull(XRow("LB_RCC_ROOF")), Nothing, XRow("LB_RCC_ROOF"))
                    InParam.DepositAmount = IIf(IsDBNull(XRow("LB_DEPOSIT_AMT")), Nothing, XRow("LB_DEPOSIT_AMT"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))) Then InParam.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))).ToString(Base._Server_Date_Format_Short) Else InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                    'InParam.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                    InParam.MonthlyRent = IIf(IsDBNull(XRow("LB_MONTH_RENT")), Nothing, XRow("LB_MONTH_RENT"))
                    InParam.MonthlyOtherExpenses = IIf(IsDBNull(XRow("LB_MONTH_O_PAYMENTS")), Nothing, XRow("LB_MONTH_O_PAYMENTS"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))) Then InParam.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))).ToString(Base._Server_Date_Format_Short) Else InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                    'InParam.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                    If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))) Then InParam.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))).ToString(Base._Server_Date_Format_Short) Else InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                    'InParam.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                    InParam.OtherDocs = IIf(IsDBNull(XRow("LB_DOC_OTHERS")), Nothing, XRow("LB_DOC_OTHERS"))
                    InParam.DocNames = IIf(IsDBNull(XRow("LB_DOC_NAME")), Nothing, XRow("LB_DOC_NAME"))
                    InParam.Value = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount")))
                    InParam.OtherDetails = IIf(IsDBNull(XRow("LB_OTHER_DETAIL")), Nothing, XRow("LB_OTHER_DETAIL"))
                    InParam.MasterID = Me.xMID.Text
                    InParam.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                    InParam.Status_Action = Status_Action
                    InParam.RecID = IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID"))

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
                            If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
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
                        InParam.InsertExtInfo = ExtInfo
                    End If


                    'DOCS
                    Dim DocInfo() As Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding
                    ctr1 = 0
                    If Not LB_DOCS_ARRAY Is Nothing Then
                        DocInfo = New Common_Lib.RealTimeService.Parameter_InsertDocInfo_LandAndBuilding(LB_DOCS_ARRAY.Rows.Count) {}
                        For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                            If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
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
                        InParam.InsertDocInfo = DocInfo
                    End If


                    'Add Location
                    'Dim InAssetLoc As Common_Lib.RealTimeService.Param_AssetLoc_Insert = New Common_Lib.RealTimeService.Param_AssetLoc_Insert()
                    'InAssetLoc.name = InParam.Name
                    'InAssetLoc.OtherDetails = "Use Type: " & InParam.PropertyType
                    'InAssetLoc.Status_Action = Status_Action
                    'InAssetLoc.Match_LB_ID = InParam.RecID
                    'InAssetLoc.Match_SP_ID = ""
                    'If Not Base._AssetLocDBOps.Insert(ClientScreen.Accounts_Voucher_Gift, InParam.Name, "Use Type: " & InParam.PropertyType, Status_Action, "", InParam.RecID) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    'InParam.param_InsertAssetLoc = InAssetLoc


                    'Add Complex Building
                    'Dim InComplexBuilding As Common_Lib.RealTimeService.Param_InsertBuilding_Complexes = New Common_Lib.RealTimeService.Param_InsertBuilding_Complexes
                    'InComplexBuilding.
                    Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID")))
                    If Locations Is Nothing Then
                        Base.HandleDBError_OnNothingReturned()
                        Exit Sub
                    End If
                    If Locations.Rows.Count > 0 Then
                        IsLBIncluded = True
                    End If

                    InsertProperty(cnt) = InParam
                End If

                '----------WIP References------------
                If XRow("Item_Profile").ToString.Equals("WIP") Then
                    If XRow("WIP_REF_TYPE") = "NEW" Then
                        Dim InReference As Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile = New Common_Lib.RealTimeService.Param_InsertTRIDAndTRSrNo_WIP_Profile()
                        InReference.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), Nothing, XRow("Item_Led_ID"))
                        InReference.Reference = IIf(IsDBNull(XRow("REFERENCE")), Nothing, XRow("REFERENCE"))
                        InReference.Amount = Val(XRow("AMOUNT"))
                        InReference.Status_Action = Status_Action
                        InReference.TxnID = Me.xMID.Text
                        InReference.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InReference.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment
                        InsertReferencesWIP(cnt) = InReference
                    End If
                End If
                cnt += 1
            Next
            EditParam.InsertAssets = InsertAssets
            EditParam.InsertGS = InsertGS
            EditParam.InsertItem = InsertItem
            EditParam.InsertLivestock = InsertLivestock
            EditParam.InsertProperty = InsertProperty
            EditParam.InsertPurpose = InsertPurpose
            EditParam.InsertVehicles = InsertVehicles
            EditParam.InsertReferencesWIP = InsertReferencesWIP

            If Not Base._Gift_DBOps.UpdateGift_Txn(EditParam) Then
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

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherGift = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherGift
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
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

                'PROFILE ENTRIES DELETE
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
                    'Remove existing Extensions
                    'If Not Base._L_B_DBOps.DeleteExtendedInfo(_RECROW("REC_ID")) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelComplexBuildings(ctr1) = _RECROW("REC_ID")
                    DelExtInfo(ctr1) = _RECROW("REC_ID")
                    'Remove existing docs
                    'If Not Base._L_B_DBOps.DeleteDocumentInfo(_RECROW("REC_ID")) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelDocInfo(ctr1) = _RECROW("REC_ID")
                    'Remove Location
                    'If Not Base._AssetLocDBOps.DeleteByLB(_RECROW("REC_ID"), ClientScreen.Accounts_Voucher_Gift) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelByLB(ctr1) = _RECROW("REC_ID")
                    ctr1 += 1
                Next
                DelParam.DeleteComplexBuilding = DelComplexBuildings
                DelParam.DeleteDocumentInfo = DelDocInfo
                DelParam.DeleteExtendedInfo = DelExtInfo
                DelParam.DeleteByLB = DelByLB

                DelParam.MID_DeleteLandB = xMID.Text
                'If Not Base._L_B_DBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                DelParam.MID_Delete = Me.xMID.Text
                'If Not Base._Gift_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteItems = Me.xMID.Text
                'If Not Base._Gift_DBOps.DeleteItems(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePurpose = Me.xMID.Text
                'If Not Base._Gift_DBOps.DeletePurpose(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Me.xMID.Text
                'If Not Base._Gift_DBOps.DeleteMaster(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                If Not Base._Gift_DBOps.DeleteGift_Txn(DelParam) Then
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
    Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
            xfrm.Text = "Address Book (Manage Donor)..."
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
            xfrm.Text = "Address Book (Donor Detail)..." : xfrm.TitleX.Text = "Party Detail"
            xfrm.ShowDialog(Me) : xfrm.Dispose()
            LookUp_GetPartyList()
        End If
    End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, But_PersManage.GotFocus, But_PersAdd.GotFocus, BUT_NEW.GotFocus, BUT_EDIT.GotFocus, BUT_DELETE.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, But_PersManage.LostFocus, But_PersAdd.LostFocus, BUT_NEW.LostFocus, BUT_EDIT.LostFocus, BUT_DELETE.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, But_PersManage.KeyDown, But_PersAdd.KeyDown, BUT_NEW.KeyDown, BUT_EDIT.KeyDown, BUT_DELETE.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_NEW.Click, BUT_EDIT.Click, BUT_DELETE.Click, T_New.Click, T_Edit.Click, T_Delete.Click, T_VIEW.Click, BUT_VIEW.Click
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
        'Else
        If Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
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

#End Region

#Region "Start--> Custom Grid Setting"

    Friend ROW As DataRow
    Friend DS As New DataSet() : Friend DT As DataTable = DS.Tables.Add("Item_Detail")
    Private Sub SetGridData()

        '--------------------ITEM DETAIL
        With DT
            .Columns.Add("Sr.", Type.GetType("System.Int32"))
            .Columns.Add("Item_ID", Type.GetType("System.String"))
            .Columns.Add("Item_Led_ID", Type.GetType("System.String"))
            .Columns.Add("Item_Trans_Type", Type.GetType("System.String"))
            .Columns.Add("Item_Party_Req", Type.GetType("System.String"))
            .Columns.Add("Item_Profile", Type.GetType("System.String"))
            .Columns.Add("ITEM_VOUCHER_TYPE", Type.GetType("System.String"))
            .Columns.Add("Item Name", Type.GetType("System.String"))
            .Columns.Add("Head", Type.GetType("System.String"))
            .Columns.Add("Qty.", Type.GetType("System.Double"))
            .Columns.Add("Unit", Type.GetType("System.String"))
            .Columns.Add("Rate", Type.GetType("System.Double"))
            .Columns.Add("Amount", Type.GetType("System.Double"))
            .Columns.Add("Remarks", Type.GetType("System.String"))
            .Columns.Add("Pur_ID", Type.GetType("System.String"))
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
            .Columns.Add("AI_IMAGE", Type.GetType("System.Byte[]"))
            .Columns.Add("AI_INS_AMT", Type.GetType("System.Double"))
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
        Me.GridView1.Columns("Item Name").Width = 200
        Me.GridView1.Columns("Head").Width = 164
        Me.GridView1.Columns("Qty.").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Qty.").Width = 50
        Me.GridView1.Columns("Unit").Width = 50
        Me.GridView1.Columns("Rate").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Rate").Width = 100
        Me.GridView1.Columns("Amount").AppearanceCell.GetTextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridView1.Columns("Amount").Width = 120
        Me.GridView1.Columns("Item_ID").Visible = False
        Me.GridView1.Columns("Item_Led_ID").Visible = False
        Me.GridView1.Columns("Item_Trans_Type").Visible = False
        Me.GridView1.Columns("Item_Party_Req").Visible = False
        Me.GridView1.Columns("Item_Profile").Visible = False
        Me.GridView1.Columns("ITEM_VOUCHER_TYPE").Visible = False
        Me.GridView1.Columns("Remarks").Visible = False
        Me.GridView1.Columns("Pur_ID").Visible = False
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
        Me.GridView1.Columns("AI_IMAGE").Visible = False
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

        Me.GridView1.PreviewFieldName = "Remarks"

        Me.GridView1.Columns("Amount").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Amount").DisplayFormat.FormatString = "#0.00"

        Me.GridView1.Columns("Rate").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridView1.Columns("Rate").DisplayFormat.FormatString = "#0.00"
        '
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
                    '

                    Dim xfrm As New Frm_Voucher_Win_Gift_Item
                    xfrm.Text = "New ~ Item Detail" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm.iTxnM_ID = Me.xMID.Text
                    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.Tag = Common_Lib.Common.Navigation_Mode._New : xfrm.iSpecific_Allow = True : xfrm.iSpecific_ItemID = iSpecific_ItemID
                    If IsDate(Me.Txt_V_Date.Text) Then xfrm.Vdt = Format(Me.Txt_V_Date.DateTime, Base._Date_Format_Current)
                    xfrm.ShowDialog(Me)
                    If xfrm.DialogResult = DialogResult.OK Then
                        Me.GridView1.ClearSorting()
                        If Not Me.GridView1.Columns("Sr.") Is Nothing Then Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
                        ROW = DT.NewRow
                        If GridView1.RowCount <= 0 Then
                            ROW("Sr.") = 1
                        Else
                            Me.GridView1.MoveLast()
                            ROW("Sr.") = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Sr.").ToString()) + 1
                        End If
                        ROW("Item Name") = xfrm.GLookUp_ItemList.Text
                        ROW("Item_ID") = xfrm.GLookUp_ItemList.Tag
                        If xfrm.iCond_Ledger_ID <> "00000" Then
                            If Val(xfrm.Txt_Amount.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amount.Text) <= Val(xfrm.iMaxValue) Then
                                ROW("Item_Led_ID") = xfrm.iCond_Ledger_ID
                            Else
                                ROW("Item_Led_ID") = xfrm.iLed_ID
                            End If
                        Else
                            ROW("Item_Led_ID") = xfrm.iLed_ID
                        End If
                        ROW("Item_Trans_Type") = xfrm.iTrans_Type
                        ROW("Item_Profile") = xfrm.iProfile
                        ROW("ITEM_VOUCHER_TYPE") = xfrm.iVoucher_Type
                        ROW("Item_Party_Req") = xfrm.iParty_Req
                        ROW("Head") = xfrm.BE_Item_Head.Text
                        ROW("Qty.") = Val(xfrm.Txt_Qty.Text)
                        ROW("Unit") = xfrm.Cmd_UOM.Text
                        ROW("Rate") = Val(xfrm.Txt_Rate.Text)
                        ROW("Amount") = Val(xfrm.Txt_Amount.Text)
                        ROW("Remarks") = xfrm.Txt_Remarks.Text
                        ROW("Pur_ID") = xfrm.GLookUp_PurList.Tag
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
                            If Not IsDBNull(xfrm.AI_IMAGE) Then ROW("AI_IMAGE") = xfrm.AI_IMAGE
                            ROW("LOC_ID") = xfrm.X_LOC_ID
                            ROW("AI_INS_AMT") = xfrm.AI_INS_AMT
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
                                For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                    Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                    Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                    Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                    LB_DOCS_ARRAY.Rows.Add(Row)
                                Next
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
                        Dim xfrm As New Frm_Voucher_Win_Gift_Item
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If
                        xfrm.iTxnM_ID = Me.xMID.Text
                        xfrm.iSpecific_ItemID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_ID").ToString()
                        xfrm.iProfile_OLD = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString()
                        xfrm.iPur_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID").ToString()
                        xfrm.Txt_Qty.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.").ToString()
                        xfrm.Txt_Rate.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Rate").ToString()
                        xfrm.Txt_Amount.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Amount").ToString()
                        xfrm.Txt_Remarks.Text = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks").ToString()
                        xfrm.LB_REC_ID = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID").ToString()

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
                            xfrm.AI_INS_AMT = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_INS_AMT").ToString())
                            xfrm.AI_WARRANTY = Val(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_WARRANTY").ToString())
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE")) Then xfrm.AI_IMAGE = Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE")
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
                            If xfrm.iCond_Ledger_ID <> "00000" Then
                                If Val(xfrm.Txt_Amount.Text) > Val(xfrm.iMinValue) And Val(xfrm.Txt_Amount.Text) <= Val(xfrm.iMaxValue) Then
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iCond_Ledger_ID)
                                Else
                                    Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                                End If
                            Else
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Led_ID", xfrm.iLed_ID)
                            End If
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Trans_Type", xfrm.iTrans_Type)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "ITEM_VOUCHER_TYPE", xfrm.iVoucher_Type)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile", xfrm.iProfile)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Party_Req", xfrm.iParty_Req)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Head", xfrm.BE_Item_Head.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Qty.", Val(xfrm.Txt_Qty.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Unit", xfrm.Cmd_UOM.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Rate", Val(xfrm.Txt_Rate.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Amount", Val(xfrm.Txt_Amount.Text))
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Remarks", xfrm.Txt_Remarks.Text)
                            Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Pur_ID", xfrm.GLookUp_PurList.Tag)
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
                                If Not IsDBNull(xfrm.AI_IMAGE) Then Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_IMAGE", xfrm.AI_IMAGE)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "LOC_ID", xfrm.X_LOC_ID)
                                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "AI_INS_AMT", xfrm.AI_INS_AMT)
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
                                    For Each XROW As DataRow In xfrm.LB_DOCS_ARRAY.Rows
                                        Dim Row As DataRow = LB_DOCS_ARRAY.NewRow()
                                        Row("LB_MISC_ID") = XROW("LB_MISC_ID").ToString()
                                        Row("LB_REC_ID") = XROW("LB_REC_ID").ToString()
                                        LB_DOCS_ARRAY.Rows.Add(Row) 'add docs checked in current selection
                                    Next
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
                    If Me.GridView1.RowCount > 0 And Val(Me.GridView1.FocusedRowHandle) >= 0 Then
                        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "LAND & BUILDING" Or Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "OTHER ASSETS" Then
                                If Base.IsInsuranceAudited() Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("I n s u r a n c e   R e l a t e d   A s s e t s   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End If

                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "ITEM_VOUCHER_TYPE").ToString().Trim.ToUpper = "LAND & BUILDING" And Not Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString().ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
                                If Base.IsInsuranceAudited() Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show("P r o p e r t y   R e l a t e d   E x p e n s e s   C a n n o t   b e   A d d e d / E d i t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End If

                            Dim Msg As String = FindLocationUsage(Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "LB_REC_ID").ToString(), False) 'sold/tf assets not excluded
                            If Msg.Length > 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Msg, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.GridView1.Focus()
                                Me.DialogResult = Windows.Forms.DialogResult.None
                                Exit Sub
                            End If

                            If Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "Item_Profile").ToString() = "WIP" And Me.GridView1.GetRowCellValue(Me.GridView1.FocusedRowHandle, "WIP_REF_TYPE").ToString() = "EXISTING" Then
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
                        DT.Rows(Me.GridView1.FocusedRowHandle).Delete()
                        Me.GridView1.RefreshData() : Me.GridView1.UpdateCurrentRow()
                        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
                        Delete_Action = True
                    End If
            End Select

            If Action = "NEW" Or Action = "EDIT" Or Action = "DELETE" Then
                Sub_Amt_Calculation(Delete_Action)
            End If
        End If
        Me.GridView1.Focus()
    End Sub
    Private Sub Sub_Amt_Calculation(ByVal Delete_Action As Boolean)
        Me.GridView1.ClearSorting()
        Me.GridView1.SortInfo.Add(Me.GridView1.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Double = 0
        For I As Integer = 0 To Me.GridView1.RowCount - 1
            If Delete_Action Then Me.GridView1.SetRowCellValue(I, "Sr.", I + 1)
            xAmt += Val(Me.GridView1.GetRowCellValue(I, "Amount").ToString())

            If Me.GridView1.GetRowCellValue(I, "Item_Party_Req").ToString.ToUpper.Trim = "YES" Then
                iParty_Req = True
            End If
        Next
        Txt_SubTotal.Text = Format(xAmt, "#0.00")
        Me.GridView1.RefreshData()
        Me.GridView1.BestFitMaxRowCount = 10 : Me.GridView1.BestFitColumns()
        '---------------

        If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Donation in Kind Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Donation in Kind"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        GLookUp_PartyList1.Tag = "" : LookUp_GetPartyList()
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

        Dim d1 As DataTable = Base._Gift_DBOps.GetMasterRecord(Me.xMID.Text)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim d3 As DataTable = Base._Gift_DBOps.GetRecord(Me.xMID.Text)
        If d3 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim d4 As DataTable = Base._Gift_DBOps.GetTxnItems(Me.xMID.Text)
        If d1 Is Nothing Or d4 Is Nothing Then
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
                If Info_LastEditedOn <> Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Gift", viewstr), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")


        If Not IsDBNull(d1.Rows(0)("TR_AB_ID_1")) Then
            If d1.Rows(0)("TR_AB_ID_1").ToString.Length > 0 Then
                Me.GLookUp_PartyList1.ShowPopup() : Me.GLookUp_PartyList1.ClosePopup()
                Me.GLookUp_PartyList1View.MoveBy(Me.GLookUp_PartyList1View.LocateByValue("C_ID", d1.Rows(0)("TR_AB_ID_1")))
                Me.GLookUp_PartyList1.EditValue = d1.Rows(0)("TR_AB_ID_1")
                Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1.EditValue
                Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
            End If
        End If
        Me.GLookUp_PartyList1.Properties.ReadOnly = False

        Txt_Narration.Text = d3.Rows(0)("TR_NARRATION")
        Txt_Reference.Text = d3.Rows(0)("TR_REFERENCE")

        Dim JointData As DataSet = New DataSet()
        JointData.Tables.Add(d4.Copy)
        'GOLD/SILVER FOR ITEM_DETAIL
        JointData.Tables.Add(GS.Copy)
        Dim GS_Relation As DataRelation = JointData.Relations.Add("GS", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("Gold_Silver_Info").Columns("GS_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(GS_Relation)
                If XROW("TR_SR_NO") = _Row("GS_TR_ITEM_SRNO") Then
                    XROW("GS_DESC_MISC_ID") = _Row("GS_DESC_MISC_ID") : XROW("GS_ITEM_WEIGHT") = _Row("GS_ITEM_WEIGHT") : XROW("LOC_ID") = _Row("GS_LOC_AL_ID")
                End If

            Next
        Next
        'OTHER ASSETS 
        JointData.Tables.Add(AI.Copy)
        Dim AI_Relation As DataRelation = JointData.Relations.Add("AI", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("Asset_Info").Columns("AI_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(AI_Relation)
                If XROW("TR_SR_NO") = _Row("AI_TR_ITEM_SRNO") Then
                    XROW("AI_TYPE") = _Row("AI_TYPE") : XROW("AI_MAKE") = _Row("AI_MAKE") : XROW("AI_MODEL") = _Row("AI_MODEL") : XROW("AI_SERIAL_NO") = _Row("AI_SERIAL_NO") : XROW("AI_WARRANTY") = _Row("AI_WARRANTY") : XROW("AI_PUR_DATE") = _Row("AI_PUR_DATE") : XROW("LOC_ID") = _Row("AI_LOC_AL_ID")
                    If Not _Row("AI_IMAGE") Is Nothing Then XROW("AI_IMAGE") = _Row("AI_IMAGE")
                    XROW("AI_INS_AMT") = _Row("AI_AMT_FOR_INS")
                End If

            Next
        Next
        'For Vehicles
        JointData.Tables.Add(VI.Copy)
        Dim VI_Relation As DataRelation = JointData.Relations.Add("VI", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("Vehicles_Info").Columns("VI_TR_ID"), False)
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
        Dim LS_Relation As DataRelation = JointData.Relations.Add("LS", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("Live_Stock_Info").Columns("LS_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(LS_Relation)
                If XROW("TR_SR_NO") = _Row("LS_TR_ITEM_SRNO") Then
                    XROW("LS_NAME") = _Row("LS_NAME") : XROW("LS_BIRTH_YEAR") = _Row("LS_BIRTH_YEAR") : XROW("LS_INSURANCE") = _Row("LS_INSURANCE") : XROW("LS_INSURANCE_ID") = _Row("LS_INSURANCE_ID") : XROW("LS_INS_POLICY_NO") = _Row("LS_INS_POLICY_NO") : XROW("LS_INS_AMT") = _Row("LS_INS_AMT") : XROW("LS_INS_DATE") = _Row("LS_INS_DATE") : XROW("LOC_ID") = _Row("LS_LOC_AL_ID")
                End If
            Next
        Next

        'FOR WIP
        JointData.Tables.Add(WIP.Copy)
        Dim WIP_Relation As DataRelation = JointData.Relations.Add("WIP", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("WIP_INFO").Columns("WIP_TR_ID"), False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(WIP_Relation)
                If XROW("TR_SR_NO") = _Row("WIP_TR_ITEM_SRNO") Then
                    XROW("WIP_REF") = _Row("WIP_REF") : XROW("WIP_REC_ID") = _Row("REC_ID") : XROW("WIP_REF_TYPE") = "NEW"
                End If
            Next
        Next

        'FOR Land&Building
        JointData.Tables.Add(LB.Copy)
        Dim LB_Relation As DataRelation = JointData.Relations.Add("LB", JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("Land_Building_info").Columns("LB_TR_ID"), False)
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
                    'If _Row("LB_ADDRESS1").ToString.Length > 0 Then
                    '    XROW("LB_ADDRESS") = _Row("LB_ADDRESS1") + IIf(Len(Trim(_Row("LB_ADDRESS2").ToString)) > 0, "," + _Row("LB_ADDRESS2").ToString, "") +
                    '                            IIf(Len(Trim(_Row("LB_ADDRESS3").ToString)) > 0, "," + _Row("LB_ADDRESS3").ToString, "") +
                    '                            IIf(Len(Trim(_Row("LB_ADDRESS4").ToString)) > 0, "," + _Row("LB_ADDRESS4").ToString, "") + "," + _Row("LB_CITY_ID").ToString + ", Distt." + _Row("LB_DISTRICT_ID").ToString() + "," + _Row("LB_STATE_ID").ToString + "-" + _Row("LB_PINCODE").ToString
                    'Else
                    '    XROW("LB_ADDRESS") = _Row("LB_PRO_ADDRESS")
                    'End If

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
        JointData.Tables.Add(d3.Copy)
        Dim parentColumns As DataColumn() = New DataColumn() {JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_M_ID"), JointData.Tables("TRANSACTION_D_ITEM_INFO").Columns("TR_SR_NO")}
        Dim childColumns As DataColumn() = New DataColumn() {JointData.Tables("TRANSACTION_INFO").Columns("TR_M_ID"), JointData.Tables("TRANSACTION_INFO").Columns("TR_SR_NO")}
        Dim LB_Exp_Relation As DataRelation = JointData.Relations.Add("LB_Exp", parentColumns, childColumns, False)
        For Each XROW In JointData.Tables(0).Rows
            For Each _Row In XROW.GetChildRows(LB_Exp_Relation)
                If XROW("ITEM_PROFILE") = "WIP" Then
                    If XROW("WIP_REC_ID").ToString.Length = 0 Then
                        XROW("WIP_REC_ID") = _Row("TR_TRF_CROSS_REF_ID")
                        XROW("WIP_REF_TYPE") = "EXISTING"
                    End If
                Else
                    If XROW("LB_REC_ID").ToString.Length = 0 Then
                        XROW("LB_REC_ID") = _Row("TR_TRF_CROSS_REF_ID") 'Reference to Land and Building entry for which the expense has been incurred 
                    End If
                End If
            Next
        Next

        'ITEM DETAIL
        For Each XRow In JointData.Tables(0).Rows
            ROW = DT.NewRow
            ROW("Sr.") = XRow("TR_SR_NO")
            ROW("Item_ID") = XRow("TR_ITEM_ID")
            ROW("Item_Led_ID") = XRow("TR_LED_ID")
            ROW("Item_Trans_Type") = XRow("TR_TRANS_TYPE")
            ROW("Item_Party_Req") = XRow("TR_PARTY_REQ")
            ROW("Item_Profile") = XRow("TR_PROFILE")
            ROW("ITEM_VOUCHER_TYPE") = XRow("ITEM_VOUCHER_TYPE")
            ROW("Item Name") = XRow("TR_ITEM_NAME")
            ROW("Head") = XRow("TR_ITEM_HEAD")
            ROW("Qty.") = XRow("TR_QTY")
            ROW("Unit") = XRow("TR_UNIT")
            ROW("Rate") = XRow("TR_RATE")
            ROW("Amount") = XRow("TR_AMOUNT")
            ROW("Remarks") = XRow("TR_REMARKS")
            'Purpose
            ROW("Pur_ID") = XRow("Pur_ID")
            ROW("LOC_ID") = XRow("LOC_ID")
            'Gold/Silver
            ROW("GS_DESC_MISC_ID") = XRow("GS_DESC_MISC_ID")
            ROW("GS_ITEM_WEIGHT") = XRow("GS_ITEM_WEIGHT")
            'OTHER ASSET
            ROW("AI_TYPE") = XRow("AI_TYPE") 'Bug #5125 FIX
            ROW("AI_MAKE") = XRow("AI_MAKE")
            ROW("AI_MODEL") = XRow("AI_MODEL")
            ROW("AI_SERIAL_NO") = XRow("AI_SERIAL_NO")
            ROW("AI_WARRANTY") = XRow("AI_WARRANTY")
            If Not XRow("AI_IMAGE") Is Nothing Then ROW("AI_IMAGE") = XRow("AI_IMAGE")
            ROW("AI_PUR_DATE") = XRow("AI_PUR_DATE")
            ROW("AI_INS_AMT") = XRow("AI_INS_AMT")
            'LIVE STOCK
            ROW("LS_NAME") = XRow("LS_NAME")
            ROW("LS_BIRTH_YEAR") = XRow("LS_BIRTH_YEAR")
            ROW("LS_INSURANCE") = XRow("LS_INSURANCE")
            ROW("LS_INSURANCE_ID") = XRow("LS_INSURANCE_ID")
            ROW("LS_INS_POLICY_NO") = XRow("LS_INS_POLICY_NO")
            ROW("LS_INS_AMT") = XRow("LS_INS_AMT")
            ROW("LS_INS_DATE") = XRow("LS_INS_DATE")
            'VEHICLES
            ROW("VI_MAKE") = XRow("VI_MAKE")
            ROW("VI_MODEL") = XRow("VI_MODEL")
            ROW("VI_REG_NO_PATTERN") = XRow("VI_REG_NO_PATTERN")
            ROW("VI_REG_NO") = XRow("VI_REG_NO")
            ROW("VI_REG_DATE") = XRow("VI_REG_DATE")
            ROW("VI_OWNERSHIP") = XRow("VI_OWNERSHIP")
            ROW("VI_OWNERSHIP_AB_ID") = XRow("VI_OWNERSHIP_AB_ID")
            ROW("VI_DOC_RC_BOOK") = XRow("VI_DOC_RC_BOOK")
            ROW("VI_DOC_AFFIDAVIT") = XRow("VI_DOC_AFFIDAVIT")
            ROW("VI_DOC_WILL") = XRow("VI_DOC_WILL")
            ROW("VI_DOC_TRF_LETTER") = XRow("VI_DOC_TRF_LETTER")
            ROW("VI_DOC_FU_LETTER") = XRow("VI_DOC_FU_LETTER")
            ROW("VI_DOC_OTHERS") = XRow("VI_DOC_OTHERS")
            ROW("VI_DOC_NAME") = XRow("VI_DOC_NAME")
            ROW("VI_INSURANCE_ID") = XRow("VI_INSURANCE_ID")
            ROW("VI_INS_POLICY_NO") = XRow("VI_INS_POLICY_NO")
            ROW("VI_INS_EXPIRY_DATE") = XRow("VI_INS_EXPIRY_DATE")
            'Land & Building
            ROW("LB_PRO_TYPE") = XRow("LB_PRO_TYPE")
            ROW("LB_PRO_CATEGORY") = XRow("LB_PRO_CATEGORY")
            ROW("LB_PRO_USE") = XRow("LB_PRO_USE")
            ROW("LB_PRO_NAME") = XRow("LB_PRO_NAME")
            ROW("LB_PRO_ADDRESS") = XRow("LB_PRO_ADDRESS")
            ROW("LB_OWNERSHIP") = XRow("LB_OWNERSHIP")
            ROW("LB_OWNERSHIP_PARTY_ID") = XRow("LB_OWNERSHIP_PARTY_ID")
            ROW("LB_SURVEY_NO") = XRow("LB_SURVEY_NO")
            ROW("LB_CON_YEAR") = XRow("LB_CON_YEAR")
            ROW("LB_RCC_ROOF") = XRow("LB_RCC_ROOF")
            ROW("LB_PAID_DATE") = XRow("LB_PAID_DATE")
            ROW("LB_PERIOD_FROM") = XRow("LB_PERIOD_FROM")
            ROW("LB_PERIOD_TO") = XRow("LB_PERIOD_TO")
            ROW("LB_DOC_OTHERS") = XRow("LB_DOC_OTHERS")
            ROW("LB_DOC_NAME") = XRow("LB_DOC_NAME")
            ROW("LB_OTHER_DETAIL") = XRow("LB_OTHER_DETAIL")
            ROW("LB_TOT_P_AREA") = XRow("LB_TOT_P_AREA")
            ROW("LB_CON_AREA") = XRow("LB_CON_AREA")
            ROW("LB_DEPOSIT_AMT") = XRow("LB_DEPOSIT_AMT")
            ROW("LB_MONTH_RENT") = XRow("LB_MONTH_RENT")
            ROW("LB_MONTH_O_PAYMENTS") = XRow("LB_MONTH_O_PAYMENTS")
            ROW("LB_REC_ID") = XRow("LB_REC_ID")
            ROW("LB_ADDRESS1") = XRow("LB_ADDRESS1")
            ROW("LB_ADDRESS2") = XRow("LB_ADDRESS2")
            ROW("LB_ADDRESS3") = XRow("LB_ADDRESS3")
            ROW("LB_ADDRESS4") = XRow("LB_ADDRESS4")
            ROW("LB_STATE_ID") = XRow("LB_STATE_ID")
            ROW("LB_DISTRICT_ID") = XRow("LB_DISTRICT_ID")
            ROW("LB_CITY_ID") = XRow("LB_CITY_ID")
            ROW("LB_PINCODE") = XRow("LB_PINCODE")
            'ROW("LB_ADDRESS") = XRow("LB_ADDRESS")
            'WIP
            ROW("REF_REC_ID") = IIf(XRow("WIP_REF_TYPE") = "NEW", "", XRow("WIP_REC_ID"))
            ROW("REFERENCE") = XRow("WIP_REF")
            ROW("WIP_REF_TYPE") = XRow("WIP_REF_TYPE")

            DT.Rows.Add(ROW)
        Next
        Sub_Amt_Calculation(False)
        xPleaseWait.Hide()
    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PartyList1.Enabled = False : Me.GLookUp_PartyList1.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_City.Enabled = False : Me.BE_City.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.BE_PAN_No.Enabled = False : Me.BE_PAN_No.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SubTotal.Enabled = False : Me.Txt_SubTotal.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.But_PersAdd.Enabled = False : Me.But_PersManage.Enabled = False
        Me.BUT_NEW.Enabled = False : Me.BUT_EDIT.Enabled = False : Me.BUT_DELETE.Enabled = False : Me.BUT_VIEW.Enabled = False
        Me.T_New.Enabled = False : Me.T_Edit.Enabled = False : Me.T_Delete.Enabled = False : Me.T_VIEW.Enabled = False

    End Sub
    Private Sub Hide_Properties()

        Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
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
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                    Message = "C a n ' t   D e l e t e . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                Else
                    Message = "C a n ' t   E d i t . . . !" & vbNewLine & vbNewLine & "P r o p e r t y   C r e a t e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
                End If
                Exit For
            End If
        Next
        Return Message
    End Function
#End Region

#Region "Start--> LookupEdit Events"

    '1.GLookUp_PartyList1
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
    Dim BE_ADD1, BE_ADD2, BE_ADD3, BE_ADD4, BE_STATE, BE_DISTRICT, BE_COUNTRY, BE_PINCODE As String
    Private Sub GLookUp_PartyList1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_PartyList1.EditValueChanged
        If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_PartyList1View.RowCount > 0 And Val(Me.GLookUp_PartyList1View.FocusedRowHandle) > 0) Then
                If Me.GLookUp_PartyList1.Text.Trim.Length > 0 Then
                    Me.GLookUp_PartyList1.Tag = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_ID").ToString
                    Me.BE_PAN_No.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_PAN_NO").ToString
                    Me.BE_City.Text = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "CI_NAME").ToString


                    BE_ADD1 = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_R_ADD1").ToString
                    BE_ADD2 = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_R_ADD2").ToString
                    BE_ADD3 = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_R_ADD3").ToString
                    BE_ADD4 = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_R_ADD4").ToString

                    BE_STATE = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "ST_NAME").ToString
                    BE_DISTRICT = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "DI_NAME").ToString
                    BE_COUNTRY = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "CO_NAME").ToString
                    BE_PINCODE = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "C_R_PINCODE").ToString
                Else
                    Me.GLookUp_PartyList1.Tag = Nothing : Me.GLookUp_PartyList1.Text = ""
                End If
            Else
                Me.GLookUp_PartyList1.Tag = Nothing : Me.GLookUp_PartyList1.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub GLookUp_PartyList1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_PartyList1.EditValueChanging
        If Me.IsHandleCreated Then
            Me.BeginInvoke(New System.Windows.Forms.MethodInvoker(Sub() GLookUp_PartyList1_FilterLookup(sender)))
        End If
    End Sub
    Private Sub GLookUp_PartyList1_FilterLookup(sender As Object)
        'Text += " ! "
        Dim edit As GridLookUpEdit = TryCast(sender, GridLookUpEdit)
        Dim gridView As GridView = TryCast(edit.Properties.View, GridView)
        Dim fi As FieldInfo = gridView.[GetType]().GetField("extraFilter", BindingFlags.NonPublic Or BindingFlags.Instance)
        'Text = edit.AutoSearchText

        Dim filterCondition As String = Nothing
        Dim op1 As New BinaryOperator(CriteriaOperator.Parse("Replace(Replace(Replace(Replace([C_NAME],'.',''),' ',''),',',''),'-','')", Nothing), "%" + edit.AutoSearchText.Replace(" ", "").Replace(".", "").Replace(",", "").Replace("-", "") + "%", BinaryOperatorType.Like)
        filterCondition = New GroupOperator(GroupOperatorType.[Or], {op1}).ToString()
        fi.SetValue(gridView, filterCondition)
        Dim mi As MethodInfo = gridView.[GetType]().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic Or BindingFlags.Instance)
        If Not gridView Is Nothing Then mi.Invoke(gridView, Nothing)
    End Sub
    Private Sub LookUp_GetPartyList()
        Dim d1 As DataTable = Base._Gift_DBOps.GetParties()
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "C_NAME"
        If dview.Count > 0 Then
            Dim ROW As DataRow : ROW = d1.NewRow() : ROW("C_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_PartyList1.Properties.ValueMember = "C_ID"
            Me.GLookUp_PartyList1.Properties.DisplayMember = "C_NAME"
            Me.GLookUp_PartyList1.Properties.DataSource = dview
            Me.GLookUp_PartyList1View.RefreshData()
            Me.GLookUp_PartyList1.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_PartyList1.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_PartyList1.Properties.ReadOnly = False
    End Sub


#End Region

End Class