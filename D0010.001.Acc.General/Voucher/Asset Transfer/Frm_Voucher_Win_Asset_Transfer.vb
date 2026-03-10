Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls
Imports System.Reflection
Public Class Frm_Voucher_Win_Asset_Transfer

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    Private iVoucher_Type As String = ""
    Private iRef_ItemID As String = ""
    Private iTrans_Type As String = ""
    Private iAsset_Type As String = ""
    Private iLed_ID As String = ""
    Public iCon_Led_ID As String = ""
    Public iMinValue As Double = 0
    Public iMaxValue As Double = 0

    Private aItem_ID As String = ""
    Private aLed_ID As String = ""

    Public iSpecific_ItemID As String = ""
    Dim FR_REC_EDIT_ON As DateTime
    Dim iTO_CEN_ID As String = ""
    Dim iFR_CEN_ID As String = ""
    Dim HQ_IDs As String = ""

    Dim USE_CROSS_REF As Boolean = False
    Dim CROSS_REF_ID As String = ""
    Dim CROSS_M_ID As String = ""

    Public iParty_Req As String = ""
    Public iProfile As String = ""

    Dim LastEditedOn As DateTime
    Public Info_LastEditedOn As DateTime
    Public Info_MaxEditedOn As DateTime = DateTime.MinValue
    Dim OwnershipRequire As Boolean = False
    Dim Property_Name As String = ""
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

        If (keyData = (Keys.Control Or Keys.S)) Then ' save
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
               Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                BUT_SAVE_Click(BUT_SAVE_COM, Nothing)
            End If
            Return (True)
        End If
        If (keyData = (Keys.F2)) Then
            Txt_V_Date.Focus()
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

        If Me.Tag = Common_Lib.Common.Navigation_Mode._New Or Me.Tag = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Then
            If Cmb_Asset_Type.SelectedIndex = 4 Or Cmb_Asset_Type.SelectedIndex = 5 Then 'Movable Assets or Property
                If Base.IsInsuranceAudited() Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Addition / Changes cannot be done after the completion of Insurance Audit", "Information..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            If Me.Tag = Common_Lib.Common.Navigation_Mode._Edit Or Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                Dim assetTrf_DbOps As DataTable = Base._AssetTransfer_DBOps.GetRecord(Me.xMID.Text, 1)
                If assetTrf_DbOps Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If assetTrf_DbOps.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transfer"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                If LastEditedOn <> Convert.ToDateTime(assetTrf_DbOps.Rows(0)("REC_EDIT_ON")) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.RecordChanged("Current Transfer"), "Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If
                'ED/L 
                Dim MaxValue As Object = 0
                MaxValue = Base._AssetTransfer_DBOps.GetStatus(Me.xID1.Text)
                If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
                If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                End If

                'Special Checks
                Dim Status As DataTable = Base._Voucher_DBOps.GetStatus_TrCode(Me.xID1.Text)
                Dim xCross_Ref_Id As String = ""
                If Status Is Nothing Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If Status.Rows.Count > 0 Then ' checks for record existence here 
                    If Not IsDBNull(Status.Rows(0)("TR_TRF_CROSS_REF_ID")) Then xCross_Ref_Id = Status.Rows(0)("TR_TRF_CROSS_REF_ID")
                End If


                If Me.Tag = Common_Lib.Common.Navigation_Mode._Delete Then
                    Dim _RowHandle As Integer = 0
                    If Val(iTrans_Type) = 2 Then
                        _RowHandle = 1
                    End If
                    If (iTrans_Type.ToString()) = "CREDIT" And Not IsDBNull(xCross_Ref_Id) Then
                        Dim isProperty As Boolean = False
                        If xCross_Ref_Id.Length > 0 Then
                            ' Dim xPromptWindow As New Common_Lib.Prompt_Window
                            'If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to <color=red><u>Delete</u></color> Matched Asset Transfer...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                            'xPromptWindow.Dispose()
                            For Each cRow As DataRow In Base._Voucher_DBOps.GetAssetItemID(Me.xMID.Text).Rows ' Get Actual Item IDs from Selected Transaction
                                Dim xTemp_ItemID As String = cRow(0).ToString()
                                Dim ProfileTable As DataTable = Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID) 'Gets Asset Profile
                                Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
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
                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.LAND_BUILDING_INFO, Me.xMID.Text) : isProperty = True
                                    Case "FIXED DEPOSITS"
                                        xTemp_AssetID = Base._Voucher_DBOps.GetAssetRecID(Common_Lib.RealTimeService.Tables.FD_INFO, Me.xMID.Text)
                                End Select
                                If xTemp_AssetID.Length > 0 Then
                                    Dim SaleRecord As DataTable = Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                    If Not SaleRecord Is Nothing Then
                                        If SaleRecord.Rows.Count > 0 Then
                                            'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Selected Entry contains a asset which was already sold "), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                                            FormClosingEnable = False : Me.Close()
                                            Exit Sub
                                        End If
                                    End If
                                    Dim AssetTrfRecord As DataTable = Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID) 'checks if the referred property for constt items has been transferred 
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        If AssetTrfRecord.Rows.Count > 0 Then
                                            'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for intitial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Selected Entry contains a asset which was already Transfered"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                                            FormClosingEnable = False : Me.Close()
                                            Exit Sub
                                        End If
                                    End If
                                    'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    Dim ReferenceRecord As DataTable = Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                    If Not ReferenceRecord Is Nothing Then
                                        If ReferenceRecord.Rows.Count > 0 Then
                                            'DevExpress.XtraEditors.XtraMessageBox.Show("Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & "." & vbNewLine & vbNewLine & " Please delete the record for deleting this Entry.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Selected Entry contains a asset which was referred in a Dependent Entry"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                                            FormClosingEnable = False : Me.Close()
                                            Exit Sub
                                        End If
                                    End If
                                    ''Ref D/AE in cell E41
                                    'If isProperty Then
                                    '    Dim d1 As DataTable = Base._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_AssetTransfer, xTemp_AssetID)
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
                                    '                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record has some assets mapped to it!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                                    '                    FormClosingEnable = False : Me.Close()
                                    '                    Exit Sub
                                    '                End If
                                    '            Next
                                    '        End If
                                    '    End If
                                    'End If

                                End If
                            Next
                            'Else
                            '    xPromptWindow.Dispose()
                            '    Exit Sub
                            'End If
                        End If
                    ElseIf Not IsDBNull(xCross_Ref_Id) Then
                        If xCross_Ref_Id.Length > 0 Then
                            DevExpress.XtraEditors.XtraMessageBox.Show("M a t c h e d   A s s e t   T r a n s f e r   c a n n o t   b e   D e l e t e d . . . !", "Referred Record Already Matched!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

            If Len(Trim(Me.GLookUp_FrCen_List.Tag)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F r o m   C e n t r e   N o t   S e l e c t e d . . . !", Me.GLookUp_FrCen_List, 0, Me.GLookUp_FrCen_List.Height, 5000)
                Me.GLookUp_FrCen_List.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FrCen_List)
            End If

            If Len(Trim(Me.GLookUp_ToCen_List.Tag)) = 0 Then
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

            If Val(Me.Txt_SaleAmt.Text) < 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   N e g a t i v e . . . !", Me.Txt_SaleAmt, 0, Me.Txt_SaleAmt.Height, 5000)
                Me.Txt_SaleAmt.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_SaleAmt)
            End If

            If USE_CROSS_REF And OwnershipRequire Then
                If Len(Trim(Me.Look_OwnList.Tag)) = 0 Or Len(Trim(Me.Look_OwnList.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("O w n e r   N a m e   N o t   S e l e c t e d . . . !", Me.Look_OwnList, 0, Me.Look_OwnList.Height, 5000)
                    Me.Look_OwnList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_OwnList)
                End If
            End If



            If Cmb_Asset_Type.SelectedIndex < 5 Then
                If Len(Trim(Me.Look_LocList.Tag)) = 0 Or Len(Trim(Me.Look_LocList.Text)) = 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("L o c a t i o n   N o t   S e l e c t e d . . . !", Me.Look_LocList, 0, Me.Look_LocList.Height, 5000)
                    Me.Look_LocList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Look_LocList)
                End If
            End If

            If Cmb_Asset_Type.SelectedIndex = 5 Then
                Dim MaxValue As Object = 0
                MaxValue = Base._L_B_Voucher_DBOps.CheckDuplicatePropertyName(Me.Tag, "", Property_Name, iTO_CEN_ID)
                If MaxValue Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If MaxValue > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Duplicate Information . . ."
                    Me.ToolTip1.Show("P r o p e r t y   w i t h   s a m e   n a m e   a l r e a d y   E x i s t s   i n   R e c e i v i n g   C e n t e r . . . ! " & vbNewLine & vbNewLine & "Please alter Property name before Transferring the same.", Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                    Me.GLookUp_AssetList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If

                If Me.Cmd_PUse.EditValue.ToString.ToUpper = "MAIN CENTRE" Then
                    Me.ToolTip1.ToolTipTitle = "Duplicate Information . . ."
                    Me.ToolTip1.Show("M a i n   C e n t e r   c a n ' t    b e    t r a n s f e r r e d   . . . ! " & vbNewLine & vbNewLine & "Please alter Property Use before Transferring the same.", Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                    Me.GLookUp_AssetList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If

                'Checking Duplicate Location....
                Dim MaxValue_Loc As Object = 0
                MaxValue_Loc = Base._AssetLocDBOps.GetRecordCountByName(Trim(Property_Name), Common_Lib.RealTimeService.ClientScreen.Profile_LandAndBuilding, Me.BE_To_Pad_No.Text)
                If MaxValue_Loc Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If MaxValue_Loc <> 0 And Me.Tag = Common_Lib.Common.Navigation_Mode._New Then
                    Me.ToolTip1.ToolTipTitle = "Duplicate Information . . ."
                    Me.ToolTip1.Show("L o c a t i o n   W i t h   S a m e   N a m e   A l r e a d y   A v a i l a b l e   i n   R e c e i v i n g   C e n t e r. . . !", Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                    Me.GLookUp_AssetList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
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
        End If

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or _
          Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
         Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
         Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If Cmb_Asset_Type.SelectedIndex = 5 Then 'Property
                Dim UsageMessage As String = FindLocationUsage(GLookUp_AssetList.Tag, True) ' exclude sold / tf assets
                If UsageMessage.Length > 0 Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show(UsageMessage, Me.GLookUp_AssetList, 0, Me.GLookUp_AssetList.Height, 5000)
                    Me.GLookUp_AssetList.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.GLookUp_AssetList)
                End If
            End If
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
        End If

        '================================================Dependencies ==============================================
        If Base.AllowMultiuser() Then

            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then
                ' Dim xCen_ID As String = Base._open_Cen_ID : If USE_CROSS_REF Then xCen_ID = iFR_CEN_ID

                'Dim AssetParam As Common_Lib.RealTimeService.Param_Get_AssetTf_Asset_Listing = New Common_Lib.RealTimeService.Param_Get_AssetTf_Asset_Listing()
                'AssetParam.Next_YearID = Base._next_Unaudited_YearID : AssetParam.Prev_YearId = Base._prev_Unaudited_YearID
                'AssetParam.Cen_Id = xCen_ID
                'If Cmb_Asset_Type.SelectedIndex = 0 Then 'Gold
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD
                'ElseIf Cmb_Asset_Type.SelectedIndex = 1 Then 'Silver
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER
                'ElseIf Cmb_Asset_Type.SelectedIndex = 2 Then 'VEHICLES
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES
                'ElseIf Cmb_Asset_Type.SelectedIndex = 3 Then 'LIVESTOCK
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK
                'ElseIf Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS
                'ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then 'LAND & BUILDING
                '    AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING
                'End If

                '#Ref A/A, E/A IN AC 40
                'If Cmb_Asset_Type.SelectedIndex = 5 Then
                '    If (iTrans_Type.ToString()) = "DEBIT" Then 'bug #5003 fixed
                '        If GLookUp_AssetList.Tag.ToString.Length > 0 Then
                '            Dim PropList As DataTable = Base._L_B_DBOps.Get_PropertyListingBySP(Base._open_Year_ID, Base._prev_Unaudited_YearID, xCen_ID, GLookUp_AssetList.Tag)
                '            If Not PropList Is Nothing Then
                '                If PropList.Rows.Count > 0 Then
                '                    If Txt_SaleAmt.Text <> PropList.Rows(0)("Final Amount") Then
                '                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Value"), "Referred Record Value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                '                        FormClosingEnable = False : Me.Close()
                '                        Exit Sub
                '                    End If
                '                End If
                '            End If
                '        End If
                '    End If
                'End If

                'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then AssetParam.TR_M_ID = xMID.Text
                'AssetParam.Asset_RecID = GLookUp_AssetList.Tag
                Dim ASSET_TABLE As DataTable = Get_Asset_Items(GLookUp_AssetList.Tag) 'Fetch asset data as per selection
                Dim cnt2 As Integer = ASSET_TABLE.Rows.Count
                Dim oldEditOn As DateTime = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REC_EDIT_ON")
                If cnt2 <= 0 Then ' if the user - selected asset is not qualified for sale anymore ,as the same has been changed by other user
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                Else
                    Dim newEditOn As DateTime = ASSET_TABLE.Rows(0)("REC_EDIT_ON")
                    If oldEditOn <> newEditOn Then 'A/E,E/E
                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
                If ASSET_TABLE.Rows(0)("REF_QTY") < Txt_Qty.Text Then ' if the weight/qty remaining is less then the weight/qty demanded for sale 
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset List"), "weight/qty remaining is less then the weight/qty demanded for Transfer !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                'Asset Location Dependency Check #Ref U41
                If Cmb_Asset_Type.Text.ToLower <> "fd" Then
                    If Look_LocList.Tag.ToString.Length > 0 Then
                        Dim d2 As DataTable = Base._AssetTransfer_DBOps.GetAssetLocationByID(Look_LocList.Tag)
                        If Not d2 Is Nothing Then
                            Dim Old_EditOn As DateTime = Look_LocList.GetColumnValue("REC_EDIT_ON")
                            If d2.Rows.Count <= 0 Then
                                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Me.DialogResult = Windows.Forms.DialogResult.Retry
                                FormClosingEnable = False : Me.Close()
                                Exit Sub
                            Else
                                Dim NewEditOn As DateTime = d2.Rows(0)("REC_EDIT_ON")
                                If NewEditOn <> Old_EditOn Then
                                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Location"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

            'Dim _Profile As String = "GOLD"
            'Dim xCen_ID As String = Base._open_Cen_ID : If USE_CROSS_REF Then xCen_ID = iFR_CEN_ID
            'Dim ASSET_TABLE As DataTable = Nothing
            'If Cmb_Asset_Type.SelectedIndex = 0 Then 'Gold
            '    _Profile = "GOLD" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetGoldSilverList("GOLD", xCen_ID, GLookUp_AssetList.Tag)
            'ElseIf Cmb_Asset_Type.SelectedIndex = 1 Then 'Silver
            '    _Profile = "SILVER" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetGoldSilverList("SILVER", xCen_ID, GLookUp_AssetList.Tag)
            'ElseIf Cmb_Asset_Type.SelectedIndex = 2 Then 'VEHICLES
            '    _Profile = "VEHICLES" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetVehiclesList(xCen_ID, GLookUp_AssetList.Tag)
            'ElseIf Cmb_Asset_Type.SelectedIndex = 3 Then 'LIVESTOCK
            '    _Profile = "LIVESTOCK" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetLiveStockList(xCen_ID, GLookUp_AssetList.Tag)
            'ElseIf Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
            '    _Profile = "OTHER ASSETS" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetAssetList(xCen_ID, GLookUp_AssetList.Tag)
            'ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then 'LAND & BUILDING
            '    _Profile = "LAND & BUILDING" : ASSET_TABLE = Base._AssetTransfer_DBOps.GetLandandBuildingList(xCen_ID, GLookUp_AssetList.Tag)
            'End If
            'If ASSET_TABLE Is Nothing Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            ''#Ref A/A, E/A IN AC 40
            'If Cmb_Asset_Type.SelectedIndex = 5 Then
            '    If (iTrans_Type.ToString()) = "DEBIT" Then 'bug #5003 fixed
            '        If GLookUp_AssetList.Tag.ToString.Length > 0 Then
            '            Dim PropList As DataTable = Base._L_B_DBOps.Get_PropertyListingBySP(Base._open_Year_ID, Base._prev_Unaudited_YearID, xCen_ID, GLookUp_AssetList.Tag)
            '            If Not PropList Is Nothing Then
            '                If PropList.Rows.Count > 0 Then
            '                    If Txt_SaleAmt.Text <> PropList.Rows(0)("Final Amount") Then
            '                        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Value"), "Referred Record Value Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        Me.DialogResult = Windows.Forms.DialogResult.Retry
            '                        FormClosingEnable = False : Me.Close()
            '                        Exit Sub
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If

            'End If


            ''Profile Assets Dependency check #Ref K40 TO O40
            'Dim oldEditOn As DateTime = GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REC_EDIT_ON")
            'If ASSET_TABLE.Rows.Count <= 0 Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Deleted!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Me.DialogResult = Windows.Forms.DialogResult.Retry
            '    FormClosingEnable = False : Me.Close()
            '    Exit Sub
            'Else
            '    Dim newEditOn As DateTime = ASSET_TABLE.Rows(0)("REC_EDIT_ON")
            '    If oldEditOn <> newEditOn Then 'A/E,E/E
            '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Profile Assets"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Me.DialogResult = Windows.Forms.DialogResult.Retry
            '        FormClosingEnable = False : Me.Close()
            '        Exit Sub
            '    End If
            'End If



            ''Misc. Table...
            'Dim MISC_Table As DataTable = Nothing
            'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then
            '    MISC_Table = Base._AssetTransfer_DBOps.GetGoldSilverMisc("MISC_NAME", "MISC_ID")
            '    If MISC_Table Is Nothing Then
            '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If

            ''Item Table...
            ''Dim Item_SQL As String = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UCASE(I.ITEM_PROFILE) IN ('" & _Profile & "')  AND  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
            'Dim ITEM_Table As DataTable = Base._AssetTransfer_DBOps.GetAssetTransferItems(_Profile)
            'If ITEM_Table Is Nothing Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            ''Transaction Table... (get sale detail)
            'Dim _TR_TABLE As DataTable = Nothing
            'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            '    _TR_TABLE = Base._AssetTransfer_DBOps.GetTxnList_Sale(False, xMID.Text)
            'Else
            '    _TR_TABLE = Base._AssetTransfer_DBOps.GetTxnList_Sale(True)
            'End If
            'If _TR_TABLE Is Nothing Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            ''Transaction Table... (get asset transfer detail)
            'Dim _TR_TABLE_1 As DataTable = Nothing
            'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            '    _TR_TABLE_1 = Base._AssetTransfer_DBOps.GetTxnList_AssetTrf(False, xMID.Text)
            'Else
            '    _TR_TABLE_1 = Base._AssetTransfer_DBOps.GetTxnList_AssetTrf(True)
            'End If
            'If _TR_TABLE_1 Is Nothing Then
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

            ''SALE
            'JointData.Tables.Add(_TR_TABLE.Copy) : Dim Sale_Relation As DataRelation = JointData.Relations.Add("Sale_Entry", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
            'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Sale_Relation) : XRow("SALE_QTY") = _Row("SALE_QTY") : Next : Next

            ''ASSET TRANSFER
            'JointData.Tables.Add(_TR_TABLE_1.Copy) : Dim AssetTrf_Relation As DataRelation = JointData.Relations.Add("AssetTrf_Entry", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Master_Info").Columns("TR_REF_ID"), False)
            'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(AssetTrf_Relation) : XRow("REF_QTY") = XRow("REF_QTY") - _Row("TRF_QTY") : Next : Next

            ''Clear Relations
            'JointData.Relations.Clear()

            ''j.v.adjustment 
            'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
            '    'DEBIT
            '    If Cmb_Asset_Type.SelectedIndex = 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, True, Base._open_Year_ID, "asset_info"))
            '    If Cmb_Asset_Type.SelectedIndex <> 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, True, Base._open_Year_ID, "gold_silver_info"))

            '    Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
            '    For Each XRow In JointData.Tables(0).Rows
            '        For Each _Row In XRow.GetChildRows(JV_Relation)
            '            XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
            '        Next
            '    Next

            '    'CREDIT
            '    If Cmb_Asset_Type.SelectedIndex = 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, False, Base._open_Year_ID, "asset_info"))
            '    If Cmb_Asset_Type.SelectedIndex <> 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, False, Base._open_Year_ID, "gold_silver_info"))

            '    Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
            '    For Each XRow In JointData.Tables(0).Rows
            '        For Each _Row In XRow.GetChildRows(JV_CR_Relation)
            '            XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
            '        Next
            '    Next
            'End If

            ''Clear Relations
            'JointData.Relations.Clear()

            ''Delete Out-Standing Zero......................................
            'For Each XRow In JointData.Tables(0).Rows
            '    XRow("REF_QTY") = XRow("REF_QTY") - XRow("SALE_QTY")
            'Next
            ''Dependency check for sold, transfers #Ref AO 40, A/A IN AM40
            'If JointData.Tables(0).Rows(0)("REF_QTY") < Txt_Qty.Text Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset List"), "Asset Transfer not possible since some adjustments/changes have been made meanwhile by some other user !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Me.DialogResult = Windows.Forms.DialogResult.Retry
            '    FormClosingEnable = False : Me.Close()
            '    Exit Sub
            'End If

            '===========================================================================================================


            ''CHECKING LOCK STATUS
            'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            '    Dim MaxValue As Object = 0
            '    MaxValue = Base._AssetTransfer_DBOps.GetStatus(Me.xID1.Text)
            '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t e d  /  D e l e t e d . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            'End If


            Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
            If Chk_Incompleted.Checked Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
            If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

            Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
            Dim Dr_Led_id As String = "" : Dim Cr_Led_id As String = ""
            If iTrans_Type.ToUpper = "DEBIT" Then
                Dr_Led_id = iLed_ID 'Transfer Item Ledger
                Cr_Led_id = aLed_ID 'Asset Item Ledger
                Tr_AB_ID_1 = Me.GLookUp_ToCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_FrCen_List.Tag
            Else
                Cr_Led_id = iLed_ID 'Transfer Item Ledger
                Dr_Led_id = aLed_ID 'Asset Item Ledger
                Tr_AB_ID_1 = Me.GLookUp_FrCen_List.Tag
                Tr_AB_ID_2 = Me.GLookUp_ToCen_List.Tag
            End If

            ''Transfer To entry has been changed by other centre
            If CROSS_M_ID.ToString.Length > 0 Then
                'Dim AssetTf As DataTable = Base._AssetTransfer_DBOps.GetRecord(CROSS_M_ID, 1)
            Dim AssetTf As DataTable = Base._AssetTransfer_DBOps.GetUnMatchedList(0, Nothing, CROSS_M_ID).Tables(0) 'Bug 5092 fixed
                If AssetTf Is Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                Dim oldEditOn As DateTime = FR_REC_EDIT_ON
                If AssetTf.Rows.Count = 0 Then
                    DevExpress.XtraEditors.XtraMessageBox.Show("Transfer To Entry has been deleted in other centre", "Information...!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Me.DialogResult = Windows.Forms.DialogResult.Retry
                    FormClosingEnable = False : Me.Close()
                    Exit Sub
                Else
                    Dim NewEditOn As DateTime = AssetTf.Rows(0)("REC_EDIT_ON")
                    If NewEditOn <> oldEditOn Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Transfer To Entry has been Changed in other centre", "Information...!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Me.DialogResult = Windows.Forms.DialogResult.Retry
                        FormClosingEnable = False : Me.Close()
                        Exit Sub
                    End If
                End If
            End If

            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
                Me.xMID.Text = System.Guid.NewGuid().ToString()
                Me.xID1.Text = System.Guid.NewGuid().ToString()
                Me.xID2.Text = System.Guid.NewGuid().ToString()
                Dim xAssetRefID As String = System.Guid.NewGuid().ToString()


                Dim xCROSS_REF_ID As String = Nothing
                If USE_CROSS_REF Then
                    xCROSS_REF_ID = "'" & CROSS_M_ID & "'"
                    xAssetRefID = xAssetRefID
                Else
                    ' xCROSS_REF_ID = " NULL "
                    xAssetRefID = Me.GLookUp_AssetList.Tag
                End If

                '#Ref AN41
                If Base.AllowMultiuser() Then
                    If (iTrans_Type.ToString()) = "CREDIT" Then
                        Dim assetTfFrom As DataTable = Base._Voucher_DBOps.GetAdjustments(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, xCROSS_REF_ID, False, Base._open_Year_ID)
                        If assetTfFrom Is Nothing Then
                            Base.HandleDBError_OnNothingReturned()
                            Exit Sub
                        End If
                        If assetTfFrom.Rows.Count > 0 Then 'A/A
                            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Asset Transfer From Entry"), "Referred Record Already Changed !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Me.DialogResult = Windows.Forms.DialogResult.Retry
                            FormClosingEnable = False : Me.Close()
                            Exit Sub
                        End If
                    End If
                End If

                'MASTER ENTRY
                Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_VoucherAssetTransfer = New Common_Lib.RealTimeService.Param_Txn_Insert_VoucherAssetTransfer
                Dim InMinfo As Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_InsertMasterInfo_VoucherAssetTransfer()
                InMinfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                InMinfo.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InMinfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMinfo.TDate = Txt_V_Date.Text
                InMinfo.SubTotal = Val(Me.Txt_SaleAmt.Text)
                InMinfo.Cash = 0
                InMinfo.Bank = 0
                InMinfo.AssetRef_ID = xAssetRefID
                InMinfo.AssetTrf_Amt = Val(Me.Txt_SaleAmt.Text)
                InMinfo.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                If IsDate(Txt_V_Date.Text) Then InMinfo.AssetTrf_Date = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMinfo.AssetTrf_Date = Txt_V_Date.Text
                InMinfo.AssetTrf_Type = Me.Cmb_Asset_Type.Text

                InMinfo.Status_Action = Status_Action
                InMinfo.RecID = Me.xMID.Text

                'If Not Base._AssetTransfer_DBOps.InsertMasterInfo(InMinfo) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertMaster = InMinfo

                'Transaction info
                '1st Entry
                Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer()
                InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                InParam1.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                InParam1.ItemID = Me.GLookUp_ItemList.Tag
                InParam1.Type = iTrans_Type
                If iTrans_Type = "DEBIT" Then InParam1.Cr_Led_ID = "" Else InParam1.Cr_Led_ID = Cr_Led_id
                If iTrans_Type = "CREDIT" Then InParam1.Dr_Led_ID = "" Else InParam1.Dr_Led_ID = Dr_Led_id
                InParam1.Sub_Cr_Led_ID = ""
                InParam1.Sub_Dr_Led_ID = ""
                InParam1.Mode = "JOURNAL"
                InParam1.Amount = Val(Me.Txt_SaleAmt.Text)
                InParam1.AB_ID_1 = Tr_AB_ID_1
                InParam1.AB_ID_2 = Tr_AB_ID_2
                InParam1.Narration = Me.Txt_Narration.Text
                InParam1.Remarks = Me.Txt_Remarks.Text
                InParam1.Reference = Me.Txt_Reference.Text
                InParam1.Tr_M_ID = Me.xMID.Text
                InParam1.TxnSrNo = 1
                InParam1.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                InParam1.AssetTrf_RefItemID = xAssetRefID
                InParam1.Status_Action = Status_Action
                InParam1.RecID = Me.xID1.Text
                InParam1.Cross_Ref_ID = xCROSS_REF_ID

                'If Not Base._AssetTransfer_DBOps.Insert(InParam1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertTxnInfo1 = InParam1

                'Transaction info
                '2nd Entry
                Dim InParam2 As Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer()
                InParam2.TransCode = Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                InParam2.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam2.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam2.TDate = Txt_V_Date.Text
                InParam2.ItemID = aItem_ID
                If iTrans_Type = "DEBIT" Then InParam2.Type = "CREDIT" Else InParam2.Type = "DEBIT"

                If iTrans_Type = "DEBIT" Then InParam2.Cr_Led_ID = Cr_Led_id Else InParam2.Cr_Led_ID = ""
                If iTrans_Type = "CREDIT" Then InParam2.Dr_Led_ID = Dr_Led_id Else InParam2.Dr_Led_ID = ""
                InParam2.Sub_Cr_Led_ID = ""
                InParam2.Sub_Dr_Led_ID = ""
                InParam2.Mode = "JOURNAL"
                InParam2.Amount = Val(Me.Txt_SaleAmt.Text)
                InParam2.AB_ID_1 = Tr_AB_ID_1
                InParam2.AB_ID_2 = Tr_AB_ID_2
                InParam2.Narration = Me.Txt_Narration.Text
                InParam2.Remarks = Me.Txt_Remarks.Text
                InParam2.Reference = Me.Txt_Reference.Text
                InParam2.Tr_M_ID = Me.xMID.Text
                InParam2.TxnSrNo = 2
                InParam2.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                InParam2.AssetTrf_RefItemID = xAssetRefID
                InParam2.Status_Action = Status_Action
                InParam2.RecID = Me.xID2.Text
                InParam2.Cross_Ref_ID = xCROSS_REF_ID

                'If Not Base._AssetTransfer_DBOps.Insert(InParam2) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertTxnInfo2 = InParam2

                'PAYMENT  
                Dim InPay As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherAssetTransfer()
                InPay.TxnMID = Me.xMID.Text
                InPay.Type = "TRANSFER"
                InPay.SrNo = "1"
                InPay.RefID = xAssetRefID
                InPay.RefAmount = Val(Me.Txt_SaleAmt.Text)
                InPay.Status_Action = Status_Action
                InPay.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._AssetTransfer_DBOps.InsertPayment(InPay) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertAandLPayment = InPay

                'purpose()
                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherAssetTransfer()
                InPurpose.TxnID = Me.xMID.Text
                InPurpose.PurposeID = Me.GLookUp_PurList.Tag
                InPurpose.Amount = Val(Me.Txt_SaleAmt.Text)
                InPurpose.Status_Action = Status_Action
                InPurpose.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._AssetTransfer_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                InNewParam.param_InsertPurpose = InPurpose

                If USE_CROSS_REF Then
                    '{1}.Update Cross Reference
                    Dim UpCrossRef As Common_Lib.RealTimeService.Param_VoucherAssetTransfer_Update_CrossReference = New Common_Lib.RealTimeService.Param_VoucherAssetTransfer_Update_CrossReference()
                    UpCrossRef.Cross_Ref_ID = Me.xMID.Text
                    UpCrossRef.RecID = CROSS_M_ID
                    'If Not Base._AssetTransfer_DBOps.Update_CrossReference(Me.xMID.Text, CROSS_M_ID) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InNewParam.param_UpdateCrossRef = UpCrossRef
                    '{2}.Insert Profile Entry
                    If Cmb_Asset_Type.SelectedIndex = 2 Then
                        If Me.TextEdit1.Tag = "INCHARGE" Then

                        End If
                    End If

                    'Insert Profile
                    Dim ParamProfile As Common_Lib.RealTimeService.Parameter_Insert_Profile = New Common_Lib.RealTimeService.Parameter_Insert_Profile
                    ParamProfile.AssetType = Me.Cmb_Asset_Type.Text
                    ParamProfile.AssetRefID = Me.GLookUp_AssetList.Tag
                    ParamProfile.AssetNewID = xAssetRefID
                    ParamProfile.AssetLocID = Me.Look_LocList.Tag
                    ParamProfile.AssetOwner = Me.TextEdit1.Tag
                    ParamProfile.AssetOwnerID = Me.Look_OwnList.Tag
                    ParamProfile.AssetUse = Me.Cmd_PUse.Text
                    ParamProfile.AssetQty = Val(Me.Txt_Qty.Text)
                    ParamProfile.AssetAmt = Val(Me.Txt_SaleAmt.Text)

                    ParamProfile.CenID = Base._open_Cen_ID
                    ParamProfile.TrID = Me.xMID.Text
                    ' Dim InsertResult As DataTable = Base._AssetTransfer_DBOps.Insert_ProfileBySP(, , , , , , , , , , )
                    InNewParam.param_InsertProfile = ParamProfile
                End If

                If Not Base._AssetTransfer_DBOps.Insert_AssetTransfer_Txn(InNewParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If

            Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_VoucherAssetTransfer = New Common_Lib.RealTimeService.Param_Txn_Update_VoucherAssetTransfer
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
                'MASTER ENTRY
                Dim UpMaster As Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_UpdateMasterInfo_VoucherAssetTransfer()
                UpMaster.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then UpMaster.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.TDate = Txt_V_Date.Text
                UpMaster.SubTotal = Val(Me.Txt_SaleAmt.Text)
                UpMaster.Cash = 0
                UpMaster.Bank = 0
                UpMaster.AssetRef_ID = Me.GLookUp_AssetList.Tag
                UpMaster.AssetTrf_Amt = Val(Me.Txt_SaleAmt.Text)
                UpMaster.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                If IsDate(Txt_V_Date.Text) Then UpMaster.AssetTrf_Date = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMaster.AssetTrf_Date = Txt_V_Date.Text
                UpMaster.AssetTrf_Type = Me.Cmb_Asset_Type.Text
                'UpMaster.Status_Action = Status_Action
                UpMaster.RecID = Me.xMID.Text

                'If Not Base._AssetTransfer_DBOps.UpdateMaster(UpMaster) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_UpdateMaster = UpMaster

                'If Not Base._AssetTransfer_DBOps.DeleteItems(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeleteItems = Me.xMID.Text

                'If Not Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeletePayment = Me.xMID.Text

                'If Not Base._AssetTransfer_DBOps.DeletePurpose(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeletePurpose = Me.xMID.Text
                'If Not Base._AssetTransfer_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_Delete = Me.xMID.Text
                'Transaction info
                '1st Entry
                Dim InParam1 As Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer()
                InParam1.TransCode = Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                InParam1.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam1.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam1.TDate = Txt_V_Date.Text
                InParam1.ItemID = Me.GLookUp_ItemList.Tag
                InParam1.Type = iTrans_Type
                If iTrans_Type = "DEBIT" Then InParam1.Cr_Led_ID = "" Else InParam1.Cr_Led_ID = Cr_Led_id
                If iTrans_Type = "CREDIT" Then InParam1.Dr_Led_ID = "" Else InParam1.Dr_Led_ID = Dr_Led_id
                InParam1.Sub_Cr_Led_ID = ""
                InParam1.Sub_Dr_Led_ID = ""
                InParam1.Mode = "JOURNAL"
                InParam1.Amount = Val(Me.Txt_SaleAmt.Text)
                InParam1.AB_ID_1 = Tr_AB_ID_1
                InParam1.AB_ID_2 = Tr_AB_ID_2
                InParam1.Narration = Me.Txt_Narration.Text
                InParam1.Remarks = Me.Txt_Remarks.Text
                InParam1.Reference = Me.Txt_Reference.Text
                InParam1.Tr_M_ID = Me.xMID.Text
                InParam1.TxnSrNo = 1
                InParam1.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                InParam1.AssetTrf_RefItemID = Me.GLookUp_AssetList.Tag
                InParam1.Status_Action = Status_Action
                InParam1.RecID = Me.xID1.Text

                'If Not Base._AssetTransfer_DBOps.Insert(InParam1) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertTxnInfo1 = InParam1

                'Transaction info
                '2nd Entry
                Dim InParam2 As Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_Insert_VoucherAssetTransfer()
                InParam2.TransCode = Common_Lib.Common.Voucher_Screen_Code.Asset_Transfer
                InParam2.VNo = Me.Txt_V_NO.Text
                If IsDate(Txt_V_Date.Text) Then InParam2.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam2.TDate = Txt_V_Date.Text
                InParam2.ItemID = aItem_ID
                If iTrans_Type = "DEBIT" Then InParam2.Type = "CREDIT" Else InParam2.Type = "DEBIT"

                If iTrans_Type = "DEBIT" Then InParam2.Cr_Led_ID = Cr_Led_id Else InParam2.Cr_Led_ID = ""
                If iTrans_Type = "CREDIT" Then InParam2.Dr_Led_ID = Dr_Led_id Else InParam2.Dr_Led_ID = ""
                InParam2.Sub_Cr_Led_ID = ""
                InParam2.Sub_Dr_Led_ID = ""
                InParam2.Mode = "JOURNAL"
                InParam2.Amount = Val(Me.Txt_SaleAmt.Text)
                InParam2.AB_ID_1 = Tr_AB_ID_1
                InParam2.AB_ID_2 = Tr_AB_ID_2
                InParam2.Narration = Me.Txt_Narration.Text
                InParam2.Remarks = Me.Txt_Remarks.Text
                InParam2.Reference = Me.Txt_Reference.Text
                InParam2.Tr_M_ID = Me.xMID.Text
                InParam2.TxnSrNo = 2
                InParam2.AssetTrf_Qty = Val(Me.Txt_Qty.Text)
                InParam2.AssetTrf_RefItemID = Me.GLookUp_AssetList.Tag
                InParam2.Status_Action = Status_Action
                InParam2.RecID = Me.xID2.Text

                'If Not Base._AssetTransfer_DBOps.Insert(InParam2) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertTxnInfo2 = InParam2

                'PAYMENT  
                Dim InPay As Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_InsertAandLPayment_VoucherAssetTransfer()
                InPay.TxnMID = Me.xMID.Text
                InPay.Type = "TRANSFER"
                InPay.SrNo = "1"
                InPay.RefID = Me.GLookUp_AssetList.Tag
                InPay.RefAmount = Val(Me.Txt_SaleAmt.Text)
                InPay.Status_Action = Status_Action
                InPay.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._AssetTransfer_DBOps.InsertPayment(InPay) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertAandLPayment = InPay



                'purpose()
                Dim InPurpose As Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherAssetTransfer = New Common_Lib.RealTimeService.Parameter_InsertPurpose_VoucherAssetTransfer()
                InPurpose.TxnID = Me.xMID.Text
                InPurpose.PurposeID = Me.GLookUp_PurList.Tag
                InPurpose.Amount = Val(Me.Txt_SaleAmt.Text)
                InPurpose.Status_Action = Status_Action
                InPurpose.RecID = System.Guid.NewGuid().ToString()

                'If Not Base._AssetTransfer_DBOps.InsertPurpose(InPurpose) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.Delete(Me.xMID.Text)
                '    Base._AssetTransfer_DBOps.DeleteMaster(Me.xMID.Text)
                '    Exit Sub
                'End If
                EditParam.param_InsertPurpose = InPurpose

                If Not Base._AssetTransfer_DBOps.Update_AssetTransfer_Txn(EditParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show("Updated Successfully", Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If

            Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_VoucherAssetTransfer = New Common_Lib.RealTimeService.Param_Txn_Delete_VoucherAssetTransfer
            ' BOOKMARK : SAVE_CLICK - DELETION BEGINS
            If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE

                If Cmb_Asset_Type.SelectedIndex = 4 Or Cmb_Asset_Type.SelectedIndex = 5 Then 'Movable Assets or Property
                    If Base.IsInsuranceAudited() Then
                        DevExpress.XtraEditors.XtraMessageBox.Show("Sorry! Deletion cannot be done after the completion of Insurance Audit", "Information..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If

                DelParam.MID_DeletePayment = Me.xMID.Text
                'If Not Base._AssetTransfer_DBOps.DeletePayment(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePurpose = Me.xMID.Text
                'If Not Base._AssetTransfer_DBOps.DeletePurpose(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                If iTrans_Type = "CREDIT" Then  'Asset creation happens only in case of "Transfer From" entry 'Bug #5535
                    DelParam.MID_DeleteGS = xMID.Text

                    DelParam.MID_DeleteAssets = xMID.Text

                    DelParam.MID_DeleteLS = xMID.Text

                    DelParam.MID_DeleteVehicle = xMID.Text

                    DelParam.MID_DeleteFD = xMID.Text

                    DelParam.DeleteComplexBuilding = Me.GLookUp_AssetList.Tag

                    DelParam.DelExtInfo = Me.GLookUp_AssetList.Tag

                    DelParam.DelDocInfo = Me.GLookUp_AssetList.Tag

                    DelParam.MID_DeleteLB = xMID.Text
                End If

                DelParam.MID_Delete = Me.xMID.Text
                DelParam.MID_DeleteMaster = Me.xMID.Text
                DelParam.Txn_Date = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short)

                If Not Base._AssetTransfer_DBOps.Delete_AssetTransfer_Txn(DelParam) Then
                    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DeleteSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                FormClosingEnable = False : Me.Close()
            End If
            If Not xPromptWindow Is Nothing Then xPromptWindow.Dispose()
            ' End If


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

    Private Sub Txt_Amount_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_SaleAmt.EditValueChanged
        '  Me.Txt_Diff.Text = Format(Val(Txt_SaleAmt.Text) - Val(Txt_Amount.Text), "#0.00")
    End Sub

    Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Chk_Incompleted.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    End Sub
    Private Sub TxtGotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_SaleAmt.GotFocus, Txt_SaleAmt.Click, Txt_Narration.GotFocus, Txt_Reference.GotFocus, Txt_Remarks.GotFocus
        Dim txt As TextEdit
        txt = CType(sender, TextEdit)
        If txt.Name = Txt_SaleAmt.Name Or txt.Name = Txt_Qty.Name Then
            txt.SelectAll()
        ElseIf Val(txt.Properties.Tag) = 0 Then
            SendKeys.Send("^{END}") : txt.Properties.Tag = 1
        End If
    End Sub
    Private Sub TextKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Txt_Narration.KeyPress, Txt_Remarks.KeyPress, Txt_Reference.KeyPress
        If e.KeyChar = "[" Then e.KeyChar = "("
        If e.KeyChar = "]" Then e.KeyChar = ")"
        If e.KeyChar = "'" Then e.KeyChar = "`"
    End Sub
    Private Sub TextKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_V_Date.KeyDown, Txt_Reference.KeyDown, Txt_Remarks.KeyDown, Txt_Narration.KeyDown, Txt_SaleAmt.KeyDown, Txt_Qty.KeyDown
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
    Private Sub TxtValidated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_Narration.Validated, Txt_Reference.Validated, Txt_Remarks.Validated
        Dim txt As DevExpress.XtraEditors.TextEdit
        txt = CType(sender, DevExpress.XtraEditors.TextEdit)
        txt.Text = Base.Single_Qoates(txt.Text)
    End Sub

    Private Sub Cmb_Asset_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Asset_Type.SelectedIndexChanged
        'GLookUp_AssetList.EditValue = ""
        Me.GLookUp_AssetList.Tag = "" : Me.Txt_Desc.Text = ""
        Txt_SaleAmt.Text = "" : Txt_Qty.Text = ""
        Me.GLookUp_AssetList.Properties.DataSource = Nothing
        If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then 'ONLY FOR GOLD / SILVER
            Me.lbl_CurQty.Text = "Asset Weight:" : Me.lbl_TrfQty.Text = "Transfer Weight:"
            Me.Txt_Qty.Properties.Mask.EditMask = "f3"
        Else
            Me.lbl_CurQty.Text = "Asset Qty:" : Me.lbl_TrfQty.Text = "Transfer Qty:"
            Me.Txt_Qty.Properties.Mask.EditMask = "d"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
            Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
            Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            If Cmb_Asset_Type.SelectedIndex < 5 Then
                Look_LocList.Enabled = True
            Else
                Look_LocList.Enabled = False
            End If
        End If
        'Load Asset Details 
        xPleaseWait.Show("L o a d i n g   A s s e t   D e t a i l")
        Dim ASSET_TABLE As DataTable = Get_Asset_Items()
        If ASSET_TABLE Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim dview As New DataView(ASSET_TABLE)
        GLookUp_Get_AssetList(dview)
        xPleaseWait.Hide()
    End Sub

    Private Sub Txt_Qty_EditValueChanged(sender As Object, e As EventArgs) Handles Txt_Qty.EditValueChanged
        If Val(Me.Txt_SaleAmt.Tag) > 0 Then 'fix:Bug #5325
            If Val(Me.Txt_Qty.Text) <> Val(Me.Txt_CurQty.Text) Then
                Me.Txt_SaleAmt.Text = Math.Round(Val(Me.Txt_SaleAmt.Tag) * Val(Me.Txt_Qty.Text), 0)
            Else
                Me.Txt_SaleAmt.Text = Val(Me.Txt_SaleAmt.Tag) * Val(Me.Txt_Qty.Text)
            End If
        End If
    End Sub

#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        xPleaseWait.Show("Asset Transfer Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "Asset Transfer"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Me.TextEdit1.Text = Base._open_Ins_Name : Me.TextEdit1.Tag = ""
        GLookUp_FrCen_List.Tag = "" ' LookUp_Get_Fr_Centre()
        GLookUp_ToCen_List.Tag = "" ' LookUp_Get_To_Centre()
        GLookUp_ItemList.Tag = "" : LookUp_GetItemList()
        GLookUp_PurList.Tag = "" : LookUp_GetPurposeList()
        Look_OwnList.Tag = "" : LookUp_GetOwnList()
        Look_LocList.Tag = "" : LookUp_GetLocList()
        '--- H.Q. CENTRE IDs-----------------------
        Dim HQ_DT As DataTable = Base._AssetTransfer_DBOps.GetHQCenters()
        For Each xRow As DataRow In HQ_DT.Rows : HQ_IDs += "'" & xRow("HQ_CEN_ID").ToString & "'," : Next
        If HQ_IDs.Trim.Length > 0 Then HQ_IDs = IIf(HQ_IDs.Trim.EndsWith(","), Mid(HQ_IDs.Trim.ToString, 1, HQ_IDs.Trim.Length - 1), HQ_IDs.Trim.ToString)
        If HQ_IDs.Trim.Length = 0 Then HQ_IDs = "''"
        '-------------------------------------------

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

        'Check Pending Asset Transfer Entries
        If Val(Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Pending_List()
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
        Else
            Me.GLookUp_ItemList.Focus()
        End If
        If Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.SuperUser.ToUpper AndAlso Not Base._open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.Auditor.ToUpper Then
            Txt_SaleAmt.Visible = False : LabelControl27.Visible = False
            GLookUp_AssetListView.Columns("REF_AMT").Visible = False
        End If
        xPleaseWait.Hide()
    End Sub

    Private Sub Pending_List()
        USE_CROSS_REF = False
        Dim d1 As DataSet = Base._AssetTransfer_DBOps.GetUnMatchedList(1, Nothing)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim P1 As DataTable = d1.Tables(0)
        If P1 Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Exit Sub
        xPleaseWait.Hide()
        If P1.Rows.Count > 0 Then
            Dim xfrm As New Frm_Asset_Transfer_Pending : xfrm.MainBase = Base
            xfrm.Text = "Pending Asset Transfer Entries"
            xfrm.ShowDialog(Me)
            If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                USE_CROSS_REF = True : Me.Text = "Match ~ " & Me.TitleX.Text
                CROSS_REF_ID = xfrm._ID
                CROSS_M_ID = xfrm._M_ID
                iSpecific_ItemID = xfrm._Item_ID
                FR_REC_EDIT_ON = xfrm._EDIT_DATE

                LookUp_GetItemList()
                If iSpecific_ItemID.ToString.Length > 0 Then
                    Me.GLookUp_ItemList.Properties.ReadOnly = True
                    Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                    Me.GLookUp_ItemListView.MoveBy(Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", iSpecific_ItemID))
                    Me.GLookUp_ItemList.EditValue = iSpecific_ItemID
                    Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                    Me.GLookUp_ItemList.Properties.Tag = "SHOW"

                    Me.GLookUp_ItemList.Enabled = False
                    Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.BE_Item_Head.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

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

                Me.Cmb_Asset_Type.EditValue = xfrm._ASSET_TYPE : Me.Cmb_Asset_Type.Enabled = False : Me.Cmb_Asset_Type.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                Me.Txt_SaleAmt.EditValue = xfrm._ASSET_SALE_AMT : Me.Txt_SaleAmt.Enabled = False : Me.Txt_SaleAmt.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green


                Dim xAsset_Item_ID As String = xfrm._ASSET_REF_ID
                If xAsset_Item_ID.ToString.Length > 0 Then
                    Me.GLookUp_AssetList.Properties.ReadOnly = True
                    Me.GLookUp_AssetList.ShowPopup() : Me.GLookUp_AssetList.ClosePopup()
                    Me.GLookUp_AssetListView.MoveBy(Me.GLookUp_AssetListView.LocateByValue("REF_ID", xAsset_Item_ID))
                    Me.GLookUp_AssetList.EditValue = xAsset_Item_ID
                    Me.GLookUp_AssetList.Tag = Me.GLookUp_AssetList.EditValue
                    Me.GLookUp_AssetList.Properties.Tag = "SHOW"

                    Me.GLookUp_AssetList.Enabled = False
                    Me.GLookUp_AssetList.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.Txt_Desc.Enabled = False : Me.Txt_Desc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                    Me.Txt_CurQty.Enabled = False : Me.Txt_CurQty.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green
                End If
                If iTrans_Type.ToString.ToUpper = "CREDIT" Then Txt_CurQty.EditValue = xfrm._ASSET_QTY '#6320 fix
                Me.Txt_Qty.EditValue = xfrm._ASSET_QTY : Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Green

                LookUp_GetLocList()


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

        End If
    End Sub

    Private Sub Data_Binding()

        Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetMasterRecord(Me.xMID.Text)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim d2 As DataTable = Base._AssetTransfer_DBOps.GetPurposeRecord(Me.xMID.Text)
        Dim d3 As DataTable = Base._AssetTransfer_DBOps.GetRecord(Me.xMID.Text, 1)
        Dim d4 As DataTable = Base._AssetTransfer_DBOps.GetRecord(Me.xMID.Text, 2)
        If d1 Is Nothing Or d2 Is Nothing Or d3 Is Nothing Or d4 Is Nothing Then
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


        Me.xID1.Text = d3.Rows(0)("REC_ID")
        Me.xID2.Text = d4.Rows(0)("REC_ID")

        If d3.Rows(0)("TR_TYPE") = "CREDIT" Then
            USE_CROSS_REF = True
            LookUp_GetItemList()
            USE_CROSS_REF = False
            'Dim _AssetType As String = d1.Rows(0)("TR_SALE_TYPE")
            'Dim AssetData As DataTable = Nothing
            'AssetData = Base._GoldSilverDBOps.GetRecord(d1.Rows(0)("TR_REF_ID"))

        End If
        LastEditedOn = Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON"))
        Txt_V_NO.DataBindings.Add("TEXT", d3, "TR_VNO")


        If Not IsDBNull(d3.Rows(0)("TR_ITEM_ID")) Then
            If d3.Rows(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_ItemList.ShowPopup() : Me.GLookUp_ItemList.ClosePopup()
                Me.GLookUp_ItemListView.FocusedRowHandle = Me.GLookUp_ItemListView.LocateByValue("ITEM_ID", d3.Rows(0)("TR_ITEM_ID"))
                Me.GLookUp_ItemList.EditValue = d3.Rows(0)("TR_ITEM_ID")
                Me.GLookUp_ItemList.Tag = Me.GLookUp_ItemList.EditValue
                Me.GLookUp_ItemList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_ItemList.Properties.ReadOnly = False

        Dim Tr_AB_ID_1 As String = "" : Dim Tr_AB_ID_2 As String = ""
        If d3.Rows(0)("TR_TYPE").ToString.ToUpper = "DEBIT" Then
            Tr_AB_ID_1 = d3.Rows(0)("Tr_AB_ID_1").ToString
            Tr_AB_ID_2 = d3.Rows(0)("Tr_AB_ID_2").ToString
        Else
            Tr_AB_ID_1 = d3.Rows(0)("Tr_AB_ID_2").ToString
            Tr_AB_ID_2 = d3.Rows(0)("Tr_AB_ID_1").ToString
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

        Cmb_Asset_Type.DataBindings.Add("EditValue", d1, "TR_SALE_TYPE")

        If Not IsDBNull(d4.Rows(0)("TR_REF_OTHERS")) Then
            If d4.Rows(0)("TR_REF_OTHERS").ToString.Length > 0 Then
                Me.GLookUp_AssetList.ShowPopup() : Me.GLookUp_AssetList.ClosePopup()
                Me.GLookUp_AssetListView.MoveBy(Me.GLookUp_AssetListView.LocateByValue("REF_ID", d4.Rows(0)("TR_REF_OTHERS")))
                Me.GLookUp_AssetList.EditValue = d4.Rows(0)("TR_REF_OTHERS")
                Me.GLookUp_AssetList.Tag = Me.GLookUp_AssetList.EditValue
                Me.GLookUp_AssetList.Properties.Tag = "SHOW"
            End If
        End If : Me.GLookUp_AssetList.Properties.ReadOnly = False

        Txt_Qty.DataBindings.Add("EditValue", d1, "TR_SALE_QTY")
        Txt_SaleAmt.DataBindings.Add("EditValue", d1, "TR_SALE_AMT")

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


        xPleaseWait.Hide()
    End Sub

    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_ItemList.Enabled = False : Me.GLookUp_ItemList.Properties.AppearanceDisabled.ForeColor = SetColor
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

        Me.Cmb_Asset_Type.Enabled = False : Me.Cmb_Asset_Type.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_AssetList.Enabled = False : Me.GLookUp_AssetList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Desc.Enabled = False : Me.Txt_Desc.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_CurQty.Enabled = False : Me.Txt_CurQty.Properties.AppearanceDisabled.ForeColor = SetColor

        Me.Txt_Remarks.Enabled = False : Me.Txt_Remarks.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_SaleAmt.Enabled = False : Me.Txt_SaleAmt.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Qty.Enabled = False : Me.Txt_Qty.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_LocList.Enabled = False : Me.Look_LocList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Look_OwnList.Enabled = False : Me.Look_OwnList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Cmd_PUse.Enabled = False : Me.Cmd_PUse.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_PurList.Enabled = False : Me.GLookUp_PurList.Properties.AppearanceDisabled.ForeColor = SetColor
    End Sub

    Private Sub Hide_Properties()
        Me.ToolTip1.Hide(Me.GLookUp_ItemList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
    End Sub

    Private Function FindLocationUsage(PropertyID As String, Optional Exclude_Sold_TF As Boolean = True) As String
        Dim MaxValue As Object = 0
        Dim Message As String = ""
        Dim Locations As DataTable = Base._AssetLocDBOps.GetListByLBID(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Gift, PropertyID)
        For Each cRow As DataRow In Locations.Rows
            Dim LocationID As String = cRow(0).ToString()
            Dim UsedPage As String = Base._AssetLocDBOps.CheckLocationUsage(LocationID, Exclude_Sold_TF)
            Dim DeleteAllow As Boolean = True
            If UsedPage.Length > 0 Then DeleteAllow = False
            If Not DeleteAllow Then
                Message = "P r o p e r t y   b e i n g    t r a n s f e r r e d   i n   t h i s   V o u c h e r   i s   b e i n g   u s e d   i n   A n o t h e r   P a g e   a s  L o c a t i o n. . . !" & vbNewLine & vbNewLine & "Name : " & UsedPage
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
                iProfile = Me.GLookUp_ItemListView.GetRowCellValue(Me.GLookUp_ItemListView.FocusedRowHandle, "ITEM_PROFILE").ToString
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
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetItemList()
        Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetLedgerItems(Base.Is_HQ_Centre)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : If USE_CROSS_REF = False Then dview.RowFilter = " [ITEM_TRANS_TYPE]='DEBIT' "
        dview.Sort = "ITEM_NAME"
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

    '2.GLookUp_FrCen_List - From Centre
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
            Me.GLookUp_ItemList.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_FrCen_List.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.GLookUp_ItemList.Focus()
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

            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_Fr_Centre()
        Me.GLookUp_FrCen_List.Properties.ReadOnly = True
        Dim SQL_1 As String = ""
        Dim D1 As DataTable = Nothing
        If iTrans_Type = "DEBIT" Then
            D1 = Base._AssetTransfer_DBOps.GetFrCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
        Else
            D1 = Base._AssetTransfer_DBOps.GetFrCenterList("", "", "", "'" & Base._open_Cen_Rec_ID & "'") 'Bug #7662
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
    Private Sub GLookUp_FrCen_List_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_FrCen_List.EditValueChanging
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

    '3.GLookUp_ToCen_List - To Centre
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

            End If
        Else
        End If
    End Sub
    Private Sub LookUp_Get_To_Centre()
        Me.GLookUp_ToCen_List.Properties.ReadOnly = True
        Dim SQL_1 As String = ""
        Dim D1 As DataTable = Nothing
        If iTrans_Type = "DEBIT" Then
            D1 = Base._AssetTransfer_DBOps.GetToCenterList("", "", "", "'" & Base._open_Cen_Rec_ID & "'") 'Bug #5434 fix
        Else
            D1 = Base._AssetTransfer_DBOps.GetToCenterList("", "", "'" & Base._open_Cen_Rec_ID & "'", "")
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
    Private Sub GLookUp_ToCen_List_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles GLookUp_ToCen_List.EditValueChanging
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

    '4.Location
    Private Sub Look_LocList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_LocList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_LocList.CancelPopup()
            Hide_Properties()
            Me.Txt_SaleAmt.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_LocList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_SaleAmt.Focus()
        End If

    End Sub
    'Private Sub But_Loc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Loc.Click
    '    Dim xfrm As New D0009.Frm_Location_Window
    '    xfrm.Text = "New ~ Location" : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
    '    xfrm.ShowDialog(Me)
    '    If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
    '        xfrm.Dispose()
    '        LookUp_GetLocList()
    '        Me.Look_LocList.EditValue = "" : Me.Look_LocList.Tag = "" : Look_LocList.Properties.Tag = "SHOW"
    '    Else
    '        xfrm.Dispose()
    '    End If
    'End Sub
    Private Sub Look_LocList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_LocList.EditValueChanged
        If Me.Look_LocList.Properties.Tag = "SHOW" Then
            Me.Look_LocList.Tag = Me.Look_LocList.GetColumnValue("AL_ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetLocList()
        Dim d2 As DataTable = Base._AssetTransfer_DBOps.GetAssetLocations(Base._open_Cen_ID)
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

        If USE_CROSS_REF Then
            If dview.Count = 1 Then Me.Look_LocList.EditValue = dview.Item(0).Row("AL_ID").ToString
            Me.Look_LocList.Properties.ReadOnly = False
        Else
            Me.Look_LocList.Properties.ReadOnly = True
        End If

    End Sub

    '5.Address Book - Owner Name
    Private Sub Look_OwnList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Look_OwnList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")

        If e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            Look_OwnList.CancelPopup()
            Hide_Properties()
            Me.Cmd_PUse.Focus()
        ElseIf e.KeyCode = Keys.PageUp And Look_OwnList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Cmd_PUse.Focus()
        End If

    End Sub
    'Private Sub But_Owner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Owner.Click
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
    '        Dim SaveID1 As String = Me.Look_OwnList.Tag
    '        Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
    '        xfrm.Text = "Address Book (Ownership)..."
    '        xfrm.ShowDialog(Me) : xfrm.Dispose()
    '        LookUp_GetOwnList()
    '        Me.Look_OwnList.EditValue = SaveID1 : Me.Look_OwnList.Tag = SaveID1 : Look_OwnList.Properties.Tag = "SHOW"
    '    End If
    'End Sub
    Private Sub Look_OwnList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Look_OwnList.EditValueChanged
        If Me.Look_OwnList.Properties.Tag = "SHOW" Then
            Me.Look_OwnList.Tag = Me.Look_OwnList.GetColumnValue("ID").ToString
        Else
        End If
    End Sub
    Private Sub LookUp_GetOwnList()
        Dim d1 As DataTable = Base._L_B_Voucher_DBOps.GetOwners()
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
            Me.Txt_Qty.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_PurList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_Qty.Focus()
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
        Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetPurposes()
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

    '7.GLookUp_AssetList
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
        ' If Me.GLookUp_PartyList1.Properties.Tag = "SHOW" Then
        If (Me.GLookUp_AssetListView.RowCount > 0 And Val(Me.GLookUp_AssetListView.FocusedRowHandle) >= 0) Then
            Me.GLookUp_AssetList.Tag = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_ID").ToString
            Me.Txt_Desc.Text = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_DESC").ToString
            Me.Txt_CurQty.Text = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_QTY").ToString
            Me.Txt_Qty.Text = ""
            aItem_ID = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_ITEM_ID").ToString
            aLed_ID = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_LED_ID").ToString
            If Cmb_Asset_Type.SelectedIndex = 4 Then 'Movable Assets 
                If Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "AI_TYPE").ToString() = "EXPENSE" Then
                    aLed_ID = "00194" 'Asset Transfer (Revenue)
                End If
            End If
            OwnershipRequire = False

            'Vehicle ownership......................................
            If Cmb_Asset_Type.SelectedIndex = 2 Then
                Me.TextEdit1.Tag = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_OWNERSHIP").ToString()
                If Me.TextEdit1.Tag = "INCHARGE" Then
                    OwnershipRequire = True
                    'if INCHARGE
                    Me.Look_OwnList.Visible = True : Me.Look_OwnList.Enabled = True : Me.Look_OwnList.EditValue = ""
                    Me.Look_OwnList.EditValue = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_OWNERSHIP_ID").ToString()
                Else 'if INSTITUTION
                    Me.Look_OwnList.Visible = False : Me.Look_OwnList.Enabled = False : Me.Look_OwnList.EditValue = ""
                End If
                If USE_CROSS_REF Then
                    Me.lbl_owner.Appearance.ForeColor = Color.Red
                    Me.Look_OwnList.Properties.ReadOnly = False : Me.Look_OwnList.Enabled = True
                Else
                    Me.lbl_owner.Appearance.ForeColor = Color.Black
                    Me.Look_OwnList.Properties.ReadOnly = True : Me.Look_OwnList.Enabled = False
                End If
                Me.Cmd_PUse.Properties.ReadOnly = False : Me.Cmd_PUse.Enabled = False : Me.Cmd_PUse.SelectedIndex = -1

                'Land & Building ownership......................................
            ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then
                Me.TextEdit1.Tag = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_OWNERSHIP").ToString()
                If Me.TextEdit1.Tag = "THIRD PARTY" Then
                    OwnershipRequire = True
                    'if INCHARGE
                    Me.Look_OwnList.Visible = True : Me.Look_OwnList.Enabled = True : Me.Look_OwnList.EditValue = ""
                    Me.Look_OwnList.EditValue = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_OWNERSHIP_ID").ToString()
                Else 'if INSTITUTION
                    Me.Look_OwnList.Visible = False : Me.Look_OwnList.Enabled = False : Me.Look_OwnList.EditValue = ""
                End If
                Me.Cmd_PUse.EditValue = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_USE").ToString()
                Me.Property_Name = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_NAME").ToString()

                If USE_CROSS_REF Then
                    Me.lbl_owner.Appearance.ForeColor = Color.Red : Me.lbl_Use.Appearance.ForeColor = Color.Red
                    Me.Look_OwnList.Properties.ReadOnly = False : Me.Look_OwnList.Enabled = True
                    Me.Cmd_PUse.Properties.ReadOnly = False : Me.Cmd_PUse.Enabled = True
                Else
                    Me.lbl_owner.Appearance.ForeColor = Color.Black : Me.lbl_Use.Appearance.ForeColor = Color.Black
                    Me.Look_OwnList.Properties.ReadOnly = True : Me.Look_OwnList.Enabled = False
                    Me.Cmd_PUse.Properties.ReadOnly = True : Me.Cmd_PUse.Enabled = False
                End If
            Else
                Me.lbl_owner.Appearance.ForeColor = Color.Black : Me.lbl_Use.Appearance.ForeColor = Color.Black
                Me.Look_OwnList.Properties.ReadOnly = True : Me.Look_OwnList.Enabled = False : Me.Look_OwnList.EditValue = ""
                Me.Cmd_PUse.Properties.ReadOnly = False : Me.Cmd_PUse.Enabled = False : Me.Cmd_PUse.SelectedIndex = -1
            End If



            If USE_CROSS_REF Then
                If Cmb_Asset_Type.SelectedIndex < 5 Then
                    Me.lbl_location.Appearance.ForeColor = Color.Red
                Else
                    Me.lbl_location.Appearance.ForeColor = Color.Black
                End If
            Else 'FALSE
                Me.lbl_location.Appearance.ForeColor = Color.Black

                'Set Location except land & building......................................
                If Cmb_Asset_Type.SelectedIndex < 5 And Not IsDBNull(Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_LOC_ID")) Then
                    Me.Look_LocList.EditValue = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_LOC_ID").ToString()
                Else
                    Me.Look_LocList.EditValue = ""
                End If


                'Set Quantity......................................
                Me.lbl_TrfQty.Appearance.ForeColor = Color.Black
                Me.Txt_Qty.Text = Me.Txt_CurQty.Text

                If Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
                    If Val(Me.Txt_CurQty.Text) = 1 Then
                        Me.Txt_CurQty.Enabled = False : Me.Txt_Qty.Enabled = False
                    Else
                        Me.Txt_CurQty.Enabled = True : Me.Txt_Qty.Enabled = True
                        Me.lbl_TrfQty.Appearance.ForeColor = Color.Red
                    End If
                Else
                    Me.Txt_CurQty.Enabled = False : Me.Txt_Qty.Enabled = False
                End If


                'Get Asset Value......................................
                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

                    Me.Txt_SaleAmt.Tag = 0 : Me.Txt_SaleAmt.Text = 0

                    'Dim AI_ITEM As DataTable = Base._AssetDBOps.Get_Asset_Item_Closing_Detail(Me.GLookUp_AssetList.Tag, Me.Cmb_Asset_Type.Text, Me.xMID.Text)
                    'If AI_ITEM Is Nothing Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    'If AI_ITEM.Rows.Count > 0 Then
                    ' If Not IsDBNull(AI_ITEM.Rows(0)("BAL_AMT")) Then
                    Me.Txt_SaleAmt.Tag = Me.GLookUp_AssetListView.GetRowCellValue(Me.GLookUp_AssetListView.FocusedRowHandle, "REF_AMT").ToString ' Val(AI_ITEM.Rows(0)("BAL_AMT"))
                    'End If --Commented to pick value from common sp only , fix Bug #5211
                    'End If

                    If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'MOVABLE ASSETS + GS [added Fix: Bug #5020] 
                        If Val(Me.Txt_CurQty.Text) > 1 Or Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then ' In all GS Cases 
                            If Val(Me.Txt_SaleAmt.Tag) > 0 Then
                                Me.Txt_SaleAmt.Tag = Val(Me.Txt_SaleAmt.Tag) / Val(Me.Txt_CurQty.Text)
                            End If
                        End If
                    End If

                    Txt_Qty_EditValueChanged(Nothing, Nothing)
                End If
            End If

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
        '  Text = edit.AutoSearchText

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
    Private Function Get_Asset_Items(Optional AssetRecID As String = Nothing) As DataTable
        ' Dim _SQL_1 As String = ""
        'Dim _Profile As String = "GOLD" ': Dim _DataFrom As String = "DATA"
        'Dim ASSET_TABLE As DataTable = Nothing
        Dim AssetParam As Common_Lib.RealTimeService.Param_Get_AssetTf_Asset_Listing = New Common_Lib.RealTimeService.Param_Get_AssetTf_Asset_Listing()
        AssetParam.Next_YearID = Base._next_Unaudited_YearID : AssetParam.Prev_YearId = Base._prev_Unaudited_YearID
        Dim xCen_ID As Integer = Base._open_Cen_ID
        If USE_CROSS_REF Then
            xCen_ID = iFR_CEN_ID
            AssetParam.TR_M_ID = CROSS_M_ID ' TO ALLOW ASSETS AND ITS VALUES TO BE SHOWN WITHOUT EFFECT OF 'TRANSFER TO' VOUCHER IN SENDING CENTRE
            Dim d1 As DataTable = Base._AssetTransfer_DBOps.GetUnAuditedFinancialYearOfTransferorCentre(xCen_ID)
            If d1.Rows.Count > 0 Then
                If d1.Rows(0)("COD_YEAR_ID") > Base._open_Year_ID Then
                    AssetParam.Next_YearID = d1.Rows(0)("COD_YEAR_ID")
                Else
                    AssetParam.Prev_YearId = d1.Rows(0)("COD_YEAR_ID")
                End If
            End If
            If d1.Rows.Count > 1 Then
                If d1.Rows(1)("COD_YEAR_ID") > Base._open_Year_ID Then
                    AssetParam.Next_YearID = d1.Rows(1)("COD_YEAR_ID")
                Else
                    AssetParam.Prev_YearId = d1.Rows(1)("COD_YEAR_ID")
                End If
            End If
        End If
        AssetParam.Cen_Id = xCen_ID
        If (Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View) And iTrans_Type.ToString() = "DEBIT" Then
            AssetParam.TR_M_ID = xMID.Text
        End If
        If Cmb_Asset_Type.SelectedIndex = 0 Then 'Gold
            '_Profile = "GOLD" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetGoldSilverList("GOLD", xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.GOLD
        ElseIf Cmb_Asset_Type.SelectedIndex = 1 Then 'Silver
            '_Profile = "SILVER" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetGoldSilverList("SILVER", xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.SILVER
        ElseIf Cmb_Asset_Type.SelectedIndex = 2 Then 'VEHICLES
            '_Profile = "VEHICLES" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetVehiclesList(xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.VEHICLES
        ElseIf Cmb_Asset_Type.SelectedIndex = 3 Then 'LIVESTOCK
            '_Profile = "LIVESTOCK" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetLiveStockList(xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LIVESTOCK
        ElseIf Cmb_Asset_Type.SelectedIndex = 4 Then 'OTHER ASSET
            '_Profile = "OTHER ASSETS" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetAssetList(xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.OTHER_ASSETS
        ElseIf Cmb_Asset_Type.SelectedIndex = 5 Then 'LAND & BUILDING
            '_Profile = "LAND & BUILDING" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetLandandBuildingList(xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.LAND_BUILDING
            '_DataFrom = "SYS"
        ElseIf Cmb_Asset_Type.SelectedIndex = 6 Then 'FD
            '_Profile = "LAND & BUILDING" ': ASSET_TABLE = Base._AssetTransfer_DBOps.GetLandandBuildingList(xCen_ID)
            AssetParam.Asset_Profile = Common_Lib.RealTimeService.AssetProfiles.FD
        End If
        'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then AssetParam.TR_M_ID = xMID.Text
        If Not AssetRecID Is Nothing Then AssetParam.Asset_RecID = AssetRecID
        Dim ASSET_TABLE As DataTable = Base._AssetTransfer_DBOps.Get_AssetTf_Asset_Listing(AssetParam)
        Return ASSET_TABLE
    End Function
    ''Misc. Table...
    'Dim MISC_Table As DataTable = Nothing
    'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Then
    '    MISC_Table = Base._AssetTransfer_DBOps.GetGoldSilverMisc("MISC_NAME", "MISC_ID")
    '    If MISC_Table Is Nothing Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If
    'End If

    ''Item Table...
    ''Dim Item_SQL As String = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UCASE(I.ITEM_PROFILE) IN ('" & _Profile & "')  AND  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
    'Dim ITEM_Table As DataTable = Base._AssetTransfer_DBOps.GetAssetTransferItems(_Profile)
    'If ITEM_Table Is Nothing Then
    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Exit Sub
    'End If
    ''Transaction Table... (get sale detail)
    'Dim _TR_TABLE As DataTable = Nothing
    'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
    '_TR_TABLE = Base._AssetTransfer_DBOps.GetTxnList_Sale(False, xMID.Text)
    'Else
    '    _TR_TABLE = Base._AssetTransfer_DBOps.GetTxnList_Sale(True)
    'End If
    'If _TR_TABLE Is Nothing Then
    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    Exit Sub
    'End If

    ''Transaction Table... (get asset transfer detail)
    'Dim _TR_TABLE_1 As DataTable = Nothing
    'If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
    '    _TR_TABLE_1 = Base._AssetTransfer_DBOps.GetTxnList_AssetTrf(False, xMID.Text)
    'Else
    '    _TR_TABLE_1 = Base._AssetTransfer_DBOps.GetTxnList_AssetTrf(True)
    'End If
    'If _TR_TABLE_1 Is Nothing Then
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

    ''SALE
    'JointData.Tables.Add(_TR_TABLE.Copy) : Dim Sale_Relation As DataRelation = JointData.Relations.Add("Sale_Entry", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Payment_Info").Columns("TR_REF_ID"), False)
    'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(Sale_Relation) : XRow("SALE_QTY") = _Row("SALE_QTY") : Next : Next

    ''ASSET TRANSFER
    'JointData.Tables.Add(_TR_TABLE_1.Copy) : Dim AssetTrf_Relation As DataRelation = JointData.Relations.Add("AssetTrf_Entry", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("Transaction_D_Master_Info").Columns("TR_REF_ID"), False)
    'For Each XRow In JointData.Tables(0).Rows : For Each _Row In XRow.GetChildRows(AssetTrf_Relation) : XRow("REF_QTY") = XRow("REF_QTY") - _Row("TRF_QTY") : Next : Next

    ''Clear Relations
    'JointData.Relations.Clear()

    ''j.v.adjustment 
    'If Cmb_Asset_Type.SelectedIndex = 0 Or Cmb_Asset_Type.SelectedIndex = 1 Or Cmb_Asset_Type.SelectedIndex = 4 Then 'GS / Assets
    '    'DEBIT
    '    If Cmb_Asset_Type.SelectedIndex = 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, True, Base._open_Year_ID, "asset_info"))
    '    If Cmb_Asset_Type.SelectedIndex <> 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, True, Base._open_Year_ID, "gold_silver_info"))

    '    Dim JV_Relation As DataRelation = JointData.Relations.Add("journal", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("DEBIT").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows
    '        For Each _Row In XRow.GetChildRows(JV_Relation)
    '            XRow("REF_QTY") = XRow("REF_QTY") + _Row("QTY")
    '        Next
    '    Next

    '    'CREDIT
    '    If Cmb_Asset_Type.SelectedIndex = 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, False, Base._open_Year_ID, "asset_info"))
    '    If Cmb_Asset_Type.SelectedIndex <> 4 Then JointData.Tables.Add(Base._Voucher_DBOps.GetAdjustments(ClientScreen.Accounts_Voucher_AssetTransfer, Nothing, False, Base._open_Year_ID, "gold_silver_info"))

    '    Dim JV_CR_Relation As DataRelation = JointData.Relations.Add("journal_Cr", JointData.Tables(0).Columns("REF_ID"), JointData.Tables("CREDIT").Columns("TR_TRF_CROSS_REF_ID"), False)
    '    For Each XRow In JointData.Tables(0).Rows
    '        For Each _Row In XRow.GetChildRows(JV_CR_Relation)
    '            XRow("REF_QTY") = XRow("REF_QTY") - _Row("QTY")
    '        Next
    '    Next
    'End If

    ''Clear Relations
    'JointData.Relations.Clear()

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

    'For Each XRow In AI_Temp.Rows : JointData.Tables(0).Rows(XRow("_Rid")).Delete() : Next
    ''..............................................................
    ''
    'Dim dview As New DataView(JointData.Tables(0)) : dview.Sort = "REF_ITEM"

#End Region

End Class