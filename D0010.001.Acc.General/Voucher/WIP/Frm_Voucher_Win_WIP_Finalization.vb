Imports System.Data.OleDb
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraEditors

Public Class Frm_Voucher_Win_WIP_Finalization

#Region "Start--> Default Variables"
    Public MainBase As New Common_Lib.Common
    'Private iVoucher_Type As String = ""
    'Private iTrans_Type As String = ""
    'Private iLed_ID As String = ""
    Public iSpecific_ItemID As String = ""

    Public iCond_Ledger_ID As String = ""
    Public iMinValue As Double = 0
    Public iMaxValue As Double = 0
    Public iLed_Type As String = ""
    Public iCon_Led_Type As String = ""

    Public AI_SERIAL_NO As String : Public AI_WARRANTY As Double : Public X_LOC_ID As String : Public Other_Details As String : Public QTY As String : Public AI_RATE As String
    Public AI_MAKE As String
    Public AI_MODEL As String
    Public AI_Type As String = ""
    Public AI_LED_ID As String
    Public Txn_Cr_ItemId As String
    Public Existing_Asset_RecID_Profile As String = ""
    ' Public Existing_Asset_creation_date As DateTime

    Public Asset_ItemID As String = ""
    'Private iParty_Req As Boolean = False
    'Private LB_DOCS_ARRAY As DataTable
    'Private LB_EXTENDED_PROPERTY_TABLE As DataTable
    Dim LastEditedOn As DateTime
    Public DT As DataTable
    Public Info_LastEditedOn As DateTime
    Public Info_MaxEditedOn As DateTime

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
            'Me.DataNavigation("NEW")
        End If
        Me.GLookUp_WIPLedgerList.Focus()
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

            'If (keyData = Keys.Insert) Then
            '    Me.DataNavigation("NEW")
            '    Return (True)
            'End If
            If GridControl2.Focused Then
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

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
           Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            'If Base.DateTime_Mismatch Then
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If

            If Len(Trim(Me.GLookUp_WIPLedgerList.Tag)) = 0 Or Len(Trim(Me.GLookUp_WIPLedgerList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("WIP   L e d g e r   N o t   S e l e c t e d . . . !", Me.GLookUp_WIPLedgerList, 0, Me.GLookUp_WIPLedgerList.Height, 5000)
                Me.GLookUp_WIPLedgerList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_WIPLedgerList)
            End If
            If Len(Trim(Me.GLookUp_WIPLedgerList.Text)) = 0 Then Me.GLookUp_WIPLedgerList.Tag = ""


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

            'If Me.GLookUp_FinalizedAssetListView.RowCount <= 0 Then
            '    DevExpress.XtraEditors.XtraMessageBox.Show("F i n a l i z e d   A s s e t   N o t   S p e c i f i e d . . . !", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Me.GLookUp_FinalizedAssetListView.Focus()
            '    Me.DialogResult = Windows.Forms.DialogResult.None
            '    Exit Sub
            'End If

            If Len(Trim(Me.GLookUp_FinalizedAssetList.Tag)) = 0 Or Len(Trim(Me.GLookUp_FinalizedAssetList.Text)) = 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("F i n a l i z e d   A s s e t   N o t   S e l e c t e d . . . !", Me.GLookUp_FinalizedAssetList, 0, Me.GLookUp_FinalizedAssetList.Height, 5000)
                Me.GLookUp_FinalizedAssetList.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.GLookUp_FinalizedAssetList)
            End If

            If Val(Trim(Me.Txt_TotalAmount.Text)) <= 0 Then
                Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                Me.ToolTip1.Show("A m o u n t   c a n n o t   b e   Z e r o  /  N e g a t i v e . . . !", Me.Txt_TotalAmount, 0, Me.Txt_TotalAmount.Height, 5000)
                Me.Txt_TotalAmount.Focus()
                Me.DialogResult = Windows.Forms.DialogResult.None
                Exit Sub
            Else
                Me.ToolTip1.Hide(Me.Txt_TotalAmount)
            End If
        End If


        Dim btn As SimpleButton : btn = CType(sender, SimpleButton) : Dim Status_Action As String = ""
        Status_Action = Common_Lib.Common.Record_Status._Completed
        If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
          Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
          Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

            Dim jrnl_Item As Frm_Voucher_Win_Journal_Item = New Frm_Voucher_Win_Journal_Item
            'check wip creation date 
            For I = 0 To GridView2.RowCount - 1 ' Insert one row for each reference in txn_info
                If Me.GridView2.GetRowCellValue(I, "Finalized Amount") > 0 Then
                    Dim PROFILE_TABLE As DataTable = jrnl_Item.GetReferenceData("WIP", Txn_Cr_ItemId, "", Me.xMID.Text, Me.Tag, Me.GridView2.GetRowCellValue(I, "Profile_WIP_RecID"))
                    Dim CreationDate As Date = DateValue(IIf(IsDBNull(PROFILE_TABLE.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, PROFILE_TABLE.Rows(0)("REF_CREATION_DATE")))
                    If DateValue(Me.Txt_V_Date.Text) < CreationDate Then
                        Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                        Me.ToolTip1.Show("C u r r e n t   R e f e r e n c e   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   C r e a t i o n   V o u c h e r   d a t e d  " & CreationDate.ToLongDateString() & " for " & Me.GridView2.GetRowCellValue(I, "Reference") & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                        Me.Txt_V_Date.Focus()
                        Me.DialogResult = Windows.Forms.DialogResult.None
                        Exit Sub
                    Else
                        Me.ToolTip1.Hide(Me.Txt_V_Date)
                    End If
                End If
            Next

            If Rad_AssetType.SelectedIndex = 0 Then 'New Asset 
                Dim xfrm As New Frm_Asset_Window
                xfrm.Text = Me.Text & " (Movable Asset Detail)..."
                xfrm.BE_ItemName.Text = Me.GLookUp_FinalizedAssetList.Text
                xfrm.Txt_Make.Text = "--" : xfrm.Txt_Model.Text = "--"
                Dim xDate As DateTime = Nothing : If IsDate(Txt_V_Date) Then xDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else xDate = Txt_V_Date.Text : xfrm.Txt_Date.DateTime = xDate
                xfrm.Txt_Amt.Enabled = False : xfrm.Txt_Date.Enabled = False 'Purchase date ' xfrm.Txt_Make.Enabled = False : xfrm.Txt_Model.Enabled = False :

                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Then
                    xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
                    xfrm.Txt_Amt.Text = Me.Txt_TotalAmount.Text
                End If

                If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
                    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                    Else
                        xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                    End If

                    If Asset_ItemID = Me.GLookUp_FinalizedAssetList.Tag Then 'Same item getting updated
                        If DT.Rows.Count > 0 Then
                            xfrm.Txt_Amt.Text = Txt_TotalAmount.Text
                            xfrm.Txt_Make.Text = DT.Rows(0)("AI_MAKE").ToString
                            xfrm.Txt_Model.Text = DT.Rows(0)("AI_MODEL").ToString
                            xfrm.Txt_Serial.Text = DT.Rows(0)("AI_SERIAL_NO").ToString
                            xfrm.Txt_Warranty.Text = DT.Rows(0)("AI_WARRANTY").ToString
                            xfrm.AI_PUR_DATE = Txt_V_Date.Text
                            xfrm.Txt_Rate.Text = DT.Rows(0)("ai_rate").ToString
                            xfrm.Txt_Qty.Text = DT.Rows(0)("QTY").ToString
                            xfrm.Txt_Others.Text = DT.Rows(0)("AI_OTHER_DETAIL").ToString
                            xfrm.AI_LOC_AL_ID = DT.Rows(0)("AI_LOC_AL_ID").ToString
                        End If
                    Else 'item has been changed by user
                        xfrm.Txt_Amt.Text = Txt_TotalAmount.Text
                    End If

                End If
                xfrm.xLeft = Me.Left + 40 : xfrm.xTop = Me.Top + 55
                xfrm.Txt_Qty.TabIndex = 0
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then

                    If iCond_Ledger_ID <> "00000" Then
                        If Val(Txt_TotalAmount.Text) > Val(iMinValue) And Val(xfrm.Txt_Amt.Text) <= Val(iMaxValue) Then
                            If iCon_Led_Type = "ASSET" Then AI_Type = "ASSET" Else AI_Type = "EXPENSE"
                        Else
                            If iLed_Type = "ASSET" Then AI_Type = "ASSET" Else AI_Type = "EXPENSE"
                        End If
                    Else
                        If iLed_Type = "ASSET" Then AI_Type = "ASSET" Else AI_Type = "EXPENSE"
                    End If

                    AI_MAKE = xfrm.Txt_Make.Text
                    AI_MODEL = xfrm.Txt_Model.Text
                    AI_SERIAL_NO = xfrm.Txt_Serial.Text
                    AI_WARRANTY = Val(xfrm.Txt_Warranty.Text)
                    Txt_TotalAmount.Text = Val(xfrm.Txt_Amt.Text)
                    QTY = xfrm.Txt_Qty.Text
                    AI_RATE = xfrm.Txt_Rate.Text
                    Other_Details = xfrm.Txt_Others.Text
                    X_LOC_ID = xfrm.Look_LocList.Tag

                    If Not xfrm Is Nothing Then xfrm.Dispose()
                Else
                    If Not xfrm Is Nothing Then xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.None

                    Exit Sub
                End If
            Else

                Dim xfrm As New Frm_Select_Asset
                xfrm.FinalizedAsset_Item_ID = Me.GLookUp_FinalizedAssetList.Tag
                xfrm.Tr_M_ID = Me.xMID.Text
                xfrm.ShowDialog(Me)
                If xfrm.DialogResult = Windows.Forms.DialogResult.OK Then
                    Existing_Asset_RecID_Profile = xfrm.Asset_RecID
                    ' Existing_Asset_creation_date = xfrm.REF_CREATION_DATE
                    xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    xfrm.Dispose()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                End If


            End If

            If IIf(Me.Rad_AssetType.SelectedIndex = 0, "", Existing_Asset_RecID_Profile).ToString.Length > 0 Then
                Dim PROFILE_TABLE1 As DataTable = jrnl_Item.GetReferenceData("OTHER ASSETS", Me.GLookUp_FinalizedAssetList.Tag, "", Me.xMID.Text, Me.Tag, Existing_Asset_RecID_Profile)
                Dim CreationDate1 As Date = DateValue(IIf(IsDBNull(PROFILE_TABLE1.Rows(0)("REF_CREATION_DATE")), Base._open_Year_Sdt, PROFILE_TABLE1.Rows(0)("REF_CREATION_DATE")))

                Dim inparam As Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate = New Common_Lib.RealTimeService.Param_GetAssetMaxTxnDate()
                inparam.Creation_Date = DateValue(IIf(IsDBNull(CreationDate1), Base._open_Year_Sdt, CreationDate1))
                inparam.Asset_RecID = Existing_Asset_RecID_Profile
                inparam.YearID = Base._open_Year_ID
                inparam.Tr_M_ID = Me.xMID.Text
                Dim MxDate As Date = DateValue(Base._SaleOfAsset_DBOps.Get_AssetMaxTxnDate(inparam))
                If MxDate = Nothing Then
                    Base.HandleDBError_OnNothingReturned()
                    Exit Sub
                End If
                If DateValue(Me.Txt_V_Date.Text) < MxDate Then
                    Me.ToolTip1.ToolTipTitle = "Incomplete Information . . ."
                    Me.ToolTip1.Show("F i n a l i z a t i o n   V o u c h e r   D a t e   c a n n o t   b e   l e s s   t h a n   p r e v i o u s    t r a n s a c t i o n    o n   R e f e r e n c e d    a s s e t   d a t e d  " & MxDate.ToLongDateString() & "  . . . !", Me.Txt_V_Date, 0, Me.Txt_V_Date.Height, 5000)
                    Me.Txt_V_Date.Focus()
                    Me.DialogResult = Windows.Forms.DialogResult.None
                    Exit Sub
                Else
                    Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
            End If

            jrnl_Item.Dispose()

        End If

        Dim InNewParam As Common_Lib.RealTimeService.Param_Txn_Insert_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Txn_Insert_Voucher_WIPFinalization

        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new
            'Dim xDate As DateTime = Nothing : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
            Me.xMID.Text = System.Guid.NewGuid().ToString()
            Dim InMInfo As Common_Lib.RealTimeService.Param_InsertMasterInfo_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_InsertMasterInfo_Voucher_WIPFinalization()
            InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization
            InMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date.Text
            'InMInfo.TDate = Me.Txt_V_Date.Text
            InMInfo.TotalAmount = Val(Me.Txt_TotalAmount.Text)
            InMInfo.Status_Action = Status_Action
            InMInfo.RecID = Me.xMID.Text

            InNewParam.param_InsertMaster = InMInfo


            If Rad_AssetType.SelectedIndex = 0 Then 'New Asset

                'Insert New Asset in Profile
                Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                InParam.AssetType = AI_Type
                InParam.ItemID = Me.GLookUp_FinalizedAssetList.Tag
                InParam.Make = AI_MAKE  'xfrm.Txt_Make.Text
                InParam.Model = AI_MODEL  'xfrm.Txt_Model.Text
                InParam.SrNo = AI_SERIAL_NO
                InParam.Rate = Val(AI_RATE)
                InParam.InsAmount = Val(Txt_TotalAmount.Text)              'IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                If IsDate(Txt_V_Date.Text) Then InParam.PurchaseDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.PurchaseDate = Txt_V_Date.Text
                'InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE")) 
                InParam.PurchaseAmount = IIf(AI_Type = "ASSET", Val(Txt_TotalAmount.Text), 0) 'IIf(InParam.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")), 0) 'Bug #5046 fix, http://pm.bkinfo.in/issues/5345#note-12
                InParam.Warranty = Val(AI_WARRANTY)
                InParam.Quantity = Val(QTY)
                InParam.LocationId = X_LOC_ID
                InParam.OtherDetails = Other_Details
                InParam.TxnID = Me.xMID.Text
                InParam.TxnSrNo = 1 ' IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr.")) 'PENDING
                InParam.Status_Action = Status_Action
                InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_WIP_Finalization

                InNewParam.inAssets_Insert = InParam

            End If

            Dim InsertReferences(GridView2.RowCount) As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization
            Dim I As Integer
            Dim cnt As Integer = 0
            'Credit References
            For I = 0 To GridView2.RowCount - 1 ' Insert one row for each reference in txn_info
                If Val(Me.GridView2.GetRowCellValue(I, "Finalized Amount").ToString()) > 0 Then
                    Dim InParamCr As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization()
                    InParamCr.TransCode = Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization
                    InParamCr.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParamCr.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamCr.TDate = Txt_V_Date.Text
                    InParamCr.ItemID = Txn_Cr_ItemId
                    InParamCr.Type = "CREDIT"
                    InParamCr.Dr_Led_ID = ""
                    InParamCr.Cr_Led_ID = Me.GLookUp_WIPLedgerList.Tag
                    InParamCr.Finalized_Amount = Val(Me.GridView2.GetRowCellValue(I, "Finalized Amount").ToString())
                    InParamCr.Mode = "JOURNAL"
                    InParamCr.Narration = Me.Txt_Narration.Text
                    InParamCr.Reference = Me.Txt_Reference.Text
                    InParamCr.CrossRefID = Me.GridView2.GetRowCellValue(I, "Profile_WIP_RecID").ToString()
                    InParamCr.MasterTxnID = Me.xMID.Text
                    InParamCr.SrNo = 1
                    InParamCr.Status_Action = Status_Action
                    InParamCr.RecID = Guid.NewGuid.ToString()

                    InsertReferences(cnt) = InParamCr
                    cnt += 1

                End If

            Next
            InNewParam.InsertCr = InsertReferences

            'Debit asset ledger
            Dim InParamDr As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization()
            InParamDr.TransCode = Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization
            InParamDr.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParamDr.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamDr.TDate = Txt_V_Date.Text
            InParamDr.ItemID = Me.GLookUp_FinalizedAssetList.Tag
            InParamDr.Type = "DEBIT"
            If Val(Txt_TotalAmount.Text) > iMinValue And Val(Txt_TotalAmount.Text) <= iMaxValue Then
                InParamDr.Dr_Led_ID = iCond_Ledger_ID
            Else
                InParamDr.Dr_Led_ID = AI_LED_ID
            End If

            If Rad_AssetType.SelectedIndex = 1 Then
                InParamDr.Dr_Led_ID = AI_LED_ID
            End If

            InParamDr.Cr_Led_ID = ""
            InParamDr.Finalized_Amount = Val(Txt_TotalAmount.Text)
            InParamDr.Mode = "JOURNAL"
            InParamDr.Narration = Me.Txt_Narration.Text
            InParamDr.Reference = Me.Txt_Reference.Text
            InParamDr.CrossRefID = IIf(Me.Rad_AssetType.SelectedIndex = 0, "", Existing_Asset_RecID_Profile) 'Profile_asset_rec_id
            InParamDr.MasterTxnID = Me.xMID.Text
            InParamDr.SrNo = 2
            InParamDr.Status_Action = Status_Action
            InParamDr.RecID = Guid.NewGuid.ToString()

            InNewParam.InsertDr = InParamDr

            If Not Base._WIP_Finalization_DBOps.Insert_WIP_Finalization_Txn(InNewParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SaveSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If
        'End If


        Dim EditParam As Common_Lib.RealTimeService.Param_Txn_Update_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Txn_Update_Voucher_WIPFinalization
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
            '    'Dim xDate As DateTime = Nothing : xDate = New Date(Me.Txt_V_Date.DateTime.Year, Me.Txt_V_Date.DateTime.Month, Me.Txt_V_Date.DateTime.Day)
            '    Dim xCnt As Integer = 1
            Dim UpMInfo As Common_Lib.RealTimeService.Parameter_UpdateMaster_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Parameter_UpdateMaster_Voucher_WIPFinalization()
            UpMInfo.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then UpMInfo.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else UpMInfo.TDate = Txt_V_Date.Text
            UpMInfo.Finalized_Amount = Val(Me.Txt_TotalAmount.Text)
            'UpMInfo.Status_Action = Status_Action
            UpMInfo.RecID = Me.xMID.Text

            EditParam.Udpate_Master = UpMInfo

            EditParam.MID_DeleteAssets = Me.xMID.Text

            If Rad_AssetType.SelectedIndex = 0 Then 'New Asset
                'Insert New Asset in Profile
                Dim InParam As Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets = New Common_Lib.RealTimeService.Parameter_InsertTRIDAndTRSrNo_Assets()
                InParam.AssetType = AI_Type
                InParam.ItemID = Me.GLookUp_FinalizedAssetList.Tag
                InParam.Make = AI_MAKE    'xfrm.Txt_Make.Text
                InParam.Model = AI_MODEL  'xfrm.Txt_Model.Text
                InParam.SrNo = AI_SERIAL_NO
                InParam.Rate = Val(AI_RATE)
                InParam.InsAmount = Val(Txt_TotalAmount.Text)              'IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                If IsDate(Txt_V_Date.Text) Then InParam.PurchaseDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParam.PurchaseDate = Txt_V_Date.Text
                'InParam.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE")) 
                InParam.PurchaseAmount = IIf(AI_Type = "ASSET", Val(Txt_TotalAmount.Text), 0) 'IIf(InParam.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")), 0) 'Bug #5046 fix, http://pm.bkinfo.in/issues/5345#note-12
                InParam.Warranty = Val(AI_WARRANTY)
                InParam.Quantity = Val(QTY)
                InParam.LocationId = X_LOC_ID
                InParam.OtherDetails = Other_Details
                InParam.TxnID = Me.xMID.Text
                InParam.TxnSrNo = 1
                InParam.Status_Action = Status_Action
                InParam.Screen = Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_WIP_Finalization

                EditParam.inAssets_Insert = InParam


            End If

            'Delete Txn Info
            EditParam.MID_Delete = Me.xMID.Text


            Dim InsertReferences(GridView2.RowCount) As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization
            Dim I As Integer
            Dim cnt As Integer = 0
            'Credit References
            For I = 0 To GridView2.RowCount - 1 ' Insert one row for each reference in txn_info
                If Val(Me.GridView2.GetRowCellValue(I, "Finalized Amount").ToString()) > 0 Then
                    Dim InParamCr As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization()
                    InParamCr.TransCode = Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization
                    InParamCr.VNo = Me.Txt_V_NO.Text
                    If IsDate(Txt_V_Date.Text) Then InParamCr.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamCr.TDate = Txt_V_Date.Text
                    InParamCr.ItemID = Txn_Cr_ItemId
                    InParamCr.Type = "CREDIT"
                    InParamCr.Dr_Led_ID = ""
                    InParamCr.Cr_Led_ID = Me.GLookUp_WIPLedgerList.Tag
                    InParamCr.Finalized_Amount = Val(Me.GridView2.GetRowCellValue(I, "Finalized Amount").ToString())
                    InParamCr.Mode = "JOURNAL"
                    InParamCr.Narration = Me.Txt_Narration.Text
                    InParamCr.Reference = Me.Txt_Reference.Text
                    InParamCr.CrossRefID = Me.GridView2.GetRowCellValue(I, "Profile_WIP_RecID").ToString()
                    InParamCr.MasterTxnID = Me.xMID.Text
                    InParamCr.SrNo = 1
                    InParamCr.Status_Action = Status_Action
                    InParamCr.RecID = Guid.NewGuid.ToString()

                    InsertReferences(cnt) = InParamCr
                    cnt += 1

                End If

            Next
            EditParam.InsertCr = InsertReferences

            'Debit asset ledger
            Dim InParamDr As Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Insert_Voucher_WIPFinalization()
            InParamDr.TransCode = Common_Lib.Common.Voucher_Screen_Code.WIP_Finalization
            InParamDr.VNo = Me.Txt_V_NO.Text
            If IsDate(Txt_V_Date.Text) Then InParamDr.TDate = Convert.ToDateTime(Txt_V_Date.Text).ToString(Base._Server_Date_Format_Short) Else InParamDr.TDate = Txt_V_Date.Text
            InParamDr.ItemID = Me.GLookUp_FinalizedAssetList.Tag
            InParamDr.Type = "DEBIT"
            If Val(Txt_TotalAmount.Text) > iMinValue And Val(Txt_TotalAmount.Text) <= iMaxValue Then
                InParamDr.Dr_Led_ID = iCond_Ledger_ID
            Else
                InParamDr.Dr_Led_ID = AI_LED_ID
            End If
            If Rad_AssetType.SelectedIndex = 1 Then
                InParamDr.Dr_Led_ID = AI_LED_ID
            End If
            InParamDr.Cr_Led_ID = ""
            InParamDr.Finalized_Amount = Val(Txt_TotalAmount.Text)
            InParamDr.Mode = "JOURNAL"
            InParamDr.Narration = Me.Txt_Narration.Text
            InParamDr.Reference = Me.Txt_Reference.Text
            InParamDr.CrossRefID = IIf(Me.Rad_AssetType.SelectedIndex = 0, "", Existing_Asset_RecID_Profile) 'Profile_asset_rec_id
            InParamDr.MasterTxnID = Me.xMID.Text
            InParamDr.SrNo = 2
            InParamDr.Status_Action = Status_Action
            InParamDr.RecID = Guid.NewGuid.ToString()

            EditParam.InsertDr = InParamDr


            If Not Base._WIP_Finalization_DBOps.Update_WIP_Finalization_Txn(EditParam) Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.UpdateSuccess, Me.TitleX.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            FormClosingEnable = False : Me.Close()
        End If

        Dim DelParam As Common_Lib.RealTimeService.Param_Txn_Delete_Voucher_WIPFinalization = New Common_Lib.RealTimeService.Param_Txn_Delete_Voucher_WIPFinalization
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
            Dim xPromptWindow As New Common_Lib.Prompt_Window
            If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then

                'PROFILE ENTRIES DELETE
                DelParam.MID_DeleteAssets = Me.xMID.Text
                DelParam.MID_Delete = Me.xMID.Text
                DelParam.MID_DeleteMaster = Me.xMID.Text

                If Not Base._WIP_Finalization_DBOps.Delete_WIP_Finalization_Txn(DelParam) Then
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
    'Private Sub But_PersManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersManage.Click
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
    '       Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
    '       Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
    '        Dim xfrm As New D0006.Frm_Address_Info : xfrm.MainBase = Base
    '        xfrm.Text = "Address Book (Manage Donor)..."
    '        xfrm.ShowDialog(Me) : xfrm.Dispose()
    '        LookUp_GetPartyList()
    '    End If
    'End Sub
    'Private Sub But_PersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_PersAdd.Click
    '    If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or _
    '       Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or _
    '       Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
    '        Dim xfrm As New D0006.Frm_Address_Info_Window : xfrm.MainBase = Base : xfrm.Tag = Common_Lib.Common.Navigation_Mode._New
    '        xfrm.Text = "Address Book (Donor Detail)..." : xfrm.TitleX.Text = "Party Detail"
    '        xfrm.ShowDialog(Me) : xfrm.Dispose()
    '        LookUp_GetPartyList()
    '    End If
    'End Sub
    Private Sub ButtonGotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.GotFocus, BUT_SAVE_COM.GotFocus, BUT_DEL.GotFocus, BUT_EDIT.GotFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Bold)
    End Sub
    Private Sub ButtonLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_CANCEL.LostFocus, BUT_SAVE_COM.LostFocus, BUT_DEL.LostFocus, BUT_EDIT.LostFocus
        Dim btn As SimpleButton
        btn = CType(sender, SimpleButton)
        btn.Font = New Font("Tahoma", 8, FontStyle.Regular)
    End Sub
    Private Sub ButtonKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BUT_SAVE_COM.KeyDown, BUT_CANCEL.KeyDown, BUT_DEL.KeyDown, BUT_EDIT.KeyDown
        If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
    End Sub
    Private Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUT_EDIT.Click, T_Edit.Click, T_TxnReport.Click, BUT_TxnReport.Click
        If Trim(UCase(sender.GetType.ToString)) = UCase("DevExpress.XtraEditors.SimpleButton") Then
            Dim btn As SimpleButton
            btn = CType(sender, SimpleButton)
            '    'If UCase(btn.Name) = "BUT_NEW" Then Me.DataNavigation("NEW")
            If UCase(btn.Name) = "BUT_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(btn.Name) = "BUT_TXNREPORT" Then Me.DataNavigation("Txn. Report")
            ' If UCase(btn.Name) = "BUT_VIEW" Then Me.DataNavigation("VIEW")
        Else
            Dim T_btn As ToolStripMenuItem
            T_btn = CType(sender, ToolStripMenuItem)
            If UCase(T_btn.Name) = "T_EDIT" Then Me.DataNavigation("EDIT")
            If UCase(T_btn.Name) = "T_TXNREPORT" Then Me.DataNavigation("Txn. Report")
            'If UCase(T_btn.Name) = "T_VIEW" Then Me.DataNavigation("VIEW")
        End If

    End Sub
#End Region

#Region "Start--> TextBox Events"
    'Private Sub Item_Msg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Item_Msg.Click
    '    Me.GridView1.Focus()
    'End Sub
    'Private Sub Chk_Incompleted_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CheckEdit2.KeyDown
    '    If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    '    If e.KeyCode = Keys.PageUp Then e.SuppressKeyPress = True : SendKeys.Send("+{TAB}")
    'End Sub
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
    'Private Sub CheckEdit2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckEdit2.CheckedChanged
    '    If Me.GridView1.OptionsView.ShowPreview Then
    '        Me.GridView1.OptionsView.ShowPreview = False
    '    Else
    '        Me.GridView1.OptionsView.ShowPreview = True
    '    End If
    'End Sub

#End Region

#Region "Start--> Custom Grid Setting"

    Private Sub GridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView2.DoubleClick
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
            Me.DataNavigation("EDIT")
        End If
    End Sub
    Private Sub GridView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView2.KeyDown
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then

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
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Then
            Dim Delete_Action As Boolean = False
            Select Case Action

                Case "EDIT", "VIEW"
                    If Me.GridView2.RowCount > 0 And Val(Me.GridView2.FocusedRowHandle) >= 0 Then
                        Dim xfrm As New Frm_Voucher_Win_WIP_Item
                        If Action = "VIEW" Then
                            xfrm.Text = "View ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._View
                        Else
                            xfrm.Text = "Edit ~ Item Detail"
                            xfrm.Tag = Common_Lib.Common.Navigation_Mode._Edit
                        End If
                        xfrm.iTxnM_ID = Me.xMID.Text
                        xfrm.Reference = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Reference").ToString
                        xfrm.WIP_Amount = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "WIP_Amount").ToString
                        xfrm.Next_year_WIP_Amount = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Next_Year_WIP_Amount").ToString
                        xfrm.Txt_Finalized_Amount.Text = Me.GridView2.GetRowCellValue(Me.GridView2.FocusedRowHandle, "Finalized Amount").ToString
                        xfrm.ShowDialog(Me)
                        If xfrm.DialogResult = DialogResult.OK Then
                            Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Finalized Amount", Format(Val(xfrm.Txt_Finalized_Amount.Text), "#0.00"))
                            Me.GridView2.RefreshData() : Me.GridView2.UpdateCurrentRow()
                            Sub_Amt_Calculation()
                            'Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
                        End If
                        If Not xfrm Is Nothing Then xfrm.Dispose()
                    End If
                Case "Txn. Report"
                    If (Me.GridView2.RowCount > 0 And Val(Me.GridView2.FocusedRowHandle) >= 0) Then
                        Dim xFrm As New D0009.Frm_Txn_Report
                        xFrm.MainBase = MainBase
                        xFrm.WIP_ID = GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "Profile_WIP_RecID").ToString
                        xFrm.Opening = Val(GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "OPENING").ToString)
                        xFrm.LedgerID = Me.GLookUp_WIPLedgerList.Tag
                        xFrm.LedgerName = Me.GLookUp_WIPLedgerList.Text
                        xFrm.Reference = Me.GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "Reference").ToString
                        xFrm.OpeningDate = IIf(Me.GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "Date of Creation").ToString.Length > 0, Me.GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "Date of Creation"), Base._open_Year_Sdt)
                        xFrm.ShowDialog(Me)
                        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
                            xFrm.Dispose()
                        End If

                    End If


            End Select
        End If
    End Sub
#End Region

#Region "Start--> Procedures"

    Private Sub SetDefault()
        'xPleaseWait.Show("WIP Finalization Voucher" & vbNewLine & vbNewLine & "L o a d i n g . . . !")
        Me.CancelButton = Me.BUT_CANCEL
        Me.DialogResult = Windows.Forms.DialogResult.None
        Me.TitleX.Text = "WIP Finalization"
        Me.Txt_V_Date.Properties.NullValuePrompt = Base._Date_Format_Current
        Rad_AssetType.SelectedIndex = 0 : AI_Type = "ASSET"
        GLookUp_WIPLedgerList.Tag = "" : LookUp_GetWIPLedgerList()
        GridView2.HorzScrollVisibility = False
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
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = True : Me.BUT_DEL.Text = "Delete"
            Set_Disable(Color.DimGray)
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Then
            Me.Text = "View ~ " & Me.TitleX.Text
            Me.BUT_SAVE_COM.Visible = False : Me.BUT_DEL.Visible = False : Me.BUT_CANCEL.Text = "Close"
            Set_Disable(Color.Navy)
            'Me.T_VIEW.Enabled = True : Me.BUT_VIEW.Enabled = True
        End If
        xPleaseWait.Hide()
    End Sub
    Private Sub Data_Binding()

        Dim d1 As DataTable = Base._WIP_Finalization_DBOps.GetMasterRecord(Me.xMID.Text)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim d2 As DataTable = Base._WIP_Finalization_DBOps.GetRecord(Me.xMID.Text)
        If d2 Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim d3 As DataTable = Base._WIP_Finalization_DBOps.GetFinalizedAmounts(Me.xMID.Text)
        'If d3 Is Nothing Then
        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If
        Dim xDate As DateTime = Nothing
        xDate = d1.Rows(0)("TR_DATE") : Txt_V_Date.DateTime = xDate

        '    LastEditedOn = Convert.ToDateTime(d3.Rows(0)("REC_EDIT_ON"))
        Txt_V_NO.DataBindings.Add("TEXT", d1, "TR_VNO")

        DT = Base._WIP_Finalization_DBOps.GetAssetList(xMID.Text)

        If DT.Rows.Count > 0 Then
            Rad_AssetType.SelectedIndex = 0 ' New_Asset_RecID_Profile = DT.Rows(0)("REC_ID").ToString
            Asset_ItemID = DT.Rows(0)("AI_ITEM_ID")
        Else
            Rad_AssetType.SelectedIndex = 1
        End If


        If d2.Rows.Count > 0 Then
            Dim DataRowDr() As DataRow = d2.Select("TR_TYPE = 'DEBIT'")
            Dim DataRowCr() As DataRow = d2.Select("TR_TYPE = 'CREDIT'")

            For Each Row In DataRowCr
                'If Not IsDBNull(Row("TR_CR_LED_ID")) Then
                If Row("TR_CR_LED_ID").ToString.Length > 0 Then
                    Me.GLookUp_WIPLedgerList.ShowPopup() : Me.GLookUp_WIPLedgerList.ClosePopup()
                    Me.GLookUp_WIPLedgerListView.FocusedRowHandle = Me.GLookUp_WIPLedgerListView.LocateByValue("WIP_LED_ID", Row("TR_CR_LED_ID"))
                    Me.GLookUp_WIPLedgerList.EditValue = Row("TR_CR_LED_ID")
                    Me.GLookUp_WIPLedgerList.Tag = Me.GLookUp_WIPLedgerList.EditValue
                    Me.GLookUp_WIPLedgerList.Properties.Tag = "SHOW"
                    Exit For
                End If
                'End If
            Next

            'If Not IsDBNull(DataRowDr(0)("TR_ITEM_ID")) Then
            If DataRowDr(0)("TR_ITEM_ID").ToString.Length > 0 Then
                Me.GLookUp_FinalizedAssetList.ShowPopup() : Me.GLookUp_FinalizedAssetList.ClosePopup()
                Me.GLookUp_FinalizedAssetListView.FocusedRowHandle = Me.GLookUp_FinalizedAssetListView.LocateByValue("Asset_Item_ID", DataRowDr(0)("TR_ITEM_ID"))
                Me.GLookUp_FinalizedAssetList.EditValue = DataRowDr(0)("TR_ITEM_ID")
                Me.GLookUp_FinalizedAssetList.Tag = Me.GLookUp_FinalizedAssetList.EditValue
                Me.GLookUp_FinalizedAssetList.Properties.Tag = "SHOW"
            End If
            'End If
        End If

        Ledger_Outstanding_References(Me.GLookUp_WIPLedgerList.Tag)

        If GridView2.RowCount > 0 Then
            For I As Integer = 0 To Me.GridView2.RowCount - 1
                For Each XRow In d3.Rows
                    If Me.GridView2.GetRowCellValue(I, "Profile_WIP_RecID").ToString() = XRow("TR_TRF_CROSS_REF_ID") Then
                        Me.GridView2.SetRowCellValue(I, "Finalized Amount", XRow("TR_AMOUNT"))
                    End If
                Next
            Next
            Me.GridView2.RefreshData() : Me.GridView2.UpdateCurrentRow()
        End If

        Txt_Narration.Text = d2.Rows(0)("TR_NARRATION")
        Txt_Reference.Text = d2.Rows(0)("TR_REFERENCE")
        Sub_Amt_Calculation()
        '    xPleaseWait.Hide()
    End Sub
    Private Sub Sub_Amt_Calculation()
        Me.GridView2.ClearSorting()
        Me.GridView2.SortInfo.Add(Me.GridView2.Columns("Sr."), DevExpress.Data.ColumnSortOrder.Ascending)
        Dim xAmt As Double = 0
        For I As Integer = 0 To Me.GridView2.RowCount - 1
            xAmt += Val(Me.GridView2.GetRowCellValue(I, "Finalized Amount").ToString())
        Next
        Txt_TotalAmount.Text = Format(xAmt, "#0.00")
        Me.GridView2.RefreshData()
        ' Me.GridView2.BestFitMaxRowCount = 10 : Me.GridView2.BestFitColumns()
        'If Me.GridView1.RowCount > 0 Then Item_Msg.Visible = False Else Item_Msg.Visible = True
    End Sub
    Private Sub Ledger_Outstanding_References(WIP_LED_ID As String)
        Dim Param As Common_Lib.RealTimeService.Param_Get_WIP_Outstanding_References = New Common_Lib.RealTimeService.Param_Get_WIP_Outstanding_References
        Param.Prev_YearId = Base._prev_Unaudited_YearID
        Param.Next_YearID = Base._next_Unaudited_YearID
        Param.WIP_LED_ID = WIP_LED_ID
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Edit Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._View Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._Delete Then Param.TR_M_ID = xMID.Text
        Dim d1 As DataTable = Base._WIP_Finalization_DBOps.Get_WIP_Outstanding_References(Param)

        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        'Dim Sno As Integer = 1
        'For Each Row As DataRow In d1.Rows
        '    Row("Sr.") = Sno
        '    Sno += 1
        'Next

        Dim dview = New DataView(d1)
        'If dview.Count > 0 Then
        dview.Sort = "Sr."
        GridControl2.DataSource = dview
        Me.GridView2.Columns("Sr.").Width = 43
        Me.GridView2.Columns("Reference").Width = 325
        Me.GridView2.Columns("WIP_Amount").Width = 110
        GridView2.Columns("WIP_Amount").Caption = "WIP Amount"
        Me.GridView2.Columns("Finalized Amount").Width = 103
        GridView2.Columns("Profile_WIP_RecID").Visible = False
        GridView2.Columns("Next_Year_WIP_Amount").Visible = False
        GridView2.Columns("WIP_LED_ID").Visible = False
        GridView2.Columns("OPENING").Visible = False
        GridView2.Columns("Date of Creation").Visible = False
        GridView2.BestFitMaxRowCount = 5
        Me.GridView2.Focus()
        'End If


    End Sub
    Private Sub Set_Disable(ByVal SetColor As Color)
        Me.Txt_V_Date.Enabled = False : Me.Txt_V_Date.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_V_NO.Enabled = False : Me.Txt_V_NO.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_WIPLedgerList.Enabled = False : Me.GLookUp_WIPLedgerList.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.GLookUp_FinalizedAssetList.Enabled = False : Me.GLookUp_FinalizedAssetList.Properties.AppearanceDisabled.ForeColor = SetColor
        BUT_EDIT.Enabled = False ': BUT_VIEW.Enabled = False
        BUT_TxnReport.Enabled = False
        Rad_AssetType.Enabled = False
        Me.T_Edit.Enabled = False : T_TxnReport.Enabled = False 'Me.T_VIEW.Enabled = False :
        Me.Txt_Reference.Enabled = False : Me.Txt_Reference.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_Narration.Enabled = False : Me.Txt_Narration.Properties.AppearanceDisabled.ForeColor = SetColor
        Me.Txt_TotalAmount.Enabled = False : Me.Txt_TotalAmount.Properties.AppearanceDisabled.ForeColor = SetColor

        ' Me.T_New.Enabled = False : Me.T_Edit.Enabled = False : Me.T_Delete.Enabled = False : Me.T_VIEW.Enabled = False

    End Sub
    Private Sub Hide_Properties()

        Me.ToolTip1.Hide(Me.GLookUp_WIPLedgerList)
        Me.ToolTip1.Hide(Me.Txt_V_Date)
    End Sub

#End Region

#Region "Start--> LookupEdit Events"
    Private Sub GLookUp_FinalizedAssetList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_FinalizedAssetList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_FinalizedAssetListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_FinalizedAssetListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_FinalizedAssetList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_FinalizedAssetListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_FinalizedAssetListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_FinalizedAssetList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_FinalizedAssetList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_FinalizedAssetList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_FinalizedAssetList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_FinalizedAssetList.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_FinalizedAssetList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_FinalizedAssetList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_FinalizedAssetList.EditValueChanged
        If Me.GLookUp_FinalizedAssetList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_FinalizedAssetListView.RowCount > 0 And Val(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle) >= 0) Then
                If Me.GLookUp_FinalizedAssetList.Text.Trim.Length > 0 Then
                    Me.GLookUp_FinalizedAssetList.Tag = Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "Asset_Item_ID").ToString
                    AI_LED_ID = Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "Asset_LED_ID").ToString
                    iLed_Type = Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "LED_TYPE").ToString
                    iCon_Led_Type = Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "CON_LED_TYPE").ToString
                    iCond_Ledger_ID = Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "ITEM_CON_LED_ID").ToString
                    iMinValue = Val(Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "ITEM_CON_MIN_VALUE").ToString)
                    iMaxValue = Val(Me.GLookUp_FinalizedAssetListView.GetRowCellValue(Me.GLookUp_FinalizedAssetListView.FocusedRowHandle, "ITEM_CON_MAX_VALUE").ToString)
                Else
                    Me.GLookUp_FinalizedAssetList.Tag = Nothing : Me.GLookUp_FinalizedAssetList.Text = ""
                End If
            Else
                Me.GLookUp_FinalizedAssetList.Tag = Nothing : Me.GLookUp_FinalizedAssetList.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetFinalizedAssetList()
        Dim d1 As DataTable = Base._WIP_Finalization_DBOps.GetListOfFinalizedAssets(Base._open_Ins_ID, GLookUp_WIPLedgerList.Tag)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) ': dview.Sort = "LED_NAME"
        If dview.Count > 0 Then
            'Dim ROW As DataRow : ROW = d1.NewRow() : ROW("LED_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_FinalizedAssetList.Properties.ValueMember = "Asset_Item_ID"
            Me.GLookUp_FinalizedAssetList.Properties.DisplayMember = "Finalized_Asset"
            Me.GLookUp_FinalizedAssetList.Properties.DataSource = dview
            GLookUp_FinalizedAssetListView.PopulateColumns(dview)
            Me.GLookUp_FinalizedAssetListView.Columns("Asset_LED_ID").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("Asset_Item_ID").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("ITEM_CON_LED_ID").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("ITEM_CON_MAX_VALUE").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("ITEM_CON_MIN_VALUE").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("CON_LED_TYPE").Visible = False
            Me.GLookUp_FinalizedAssetListView.Columns("LED_TYPE").Visible = False
            Me.GLookUp_FinalizedAssetListView.RefreshData()
            Me.GLookUp_FinalizedAssetList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_FinalizedAssetList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_FinalizedAssetList.Properties.ReadOnly = False
    End Sub

    '1.GLookUp_WIPLedgerList
    Private Sub GLookUp_WIPLedgerList_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles GLookUp_WIPLedgerList.ButtonClick
        If e.Button.Index = 1 Then
            If e.Button.Tag = "ON" Then
                e.Button.Tag = "OFF" : e.Button.Caption = "Filter : Off" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Redo
                Me.GLookUp_WIPLedgerListView.OptionsView.ShowAutoFilterRow = False
                Me.GLookUp_WIPLedgerListView.OptionsCustomization.AllowFilter = False
                Me.GLookUp_WIPLedgerList.ShowPopup()
            Else
                e.Button.Tag = "ON" : e.Button.Caption = "Filter : On" : e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Undo
                Me.GLookUp_WIPLedgerListView.OptionsView.ShowAutoFilterRow = True
                Me.GLookUp_WIPLedgerListView.OptionsCustomization.AllowFilter = True
                Me.GLookUp_WIPLedgerList.ShowPopup()
            End If
        End If
    End Sub
    Private Sub GLookUp_WIPLedgerList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GLookUp_WIPLedgerList.KeyDown
        'If e.KeyCode = Keys.PageUp Then SendKeys.Send("+{TAB}")
        If e.KeyCode = Keys.PageUp And GLookUp_WIPLedgerList.IsPopupOpen = True Then
            e.SuppressKeyPress = True
            GLookUp_WIPLedgerList.CancelPopup()
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        ElseIf e.KeyCode = Keys.PageUp And GLookUp_WIPLedgerList.IsPopupOpen = False Then
            e.SuppressKeyPress = True
            Hide_Properties()
            Me.Txt_V_Date.Focus()
        End If
    End Sub
    Private Sub GLookUp_WIPLedgerList_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GLookUp_WIPLedgerList.EditValueChanged
        If Me.GLookUp_WIPLedgerList.Properties.Tag = "SHOW" Then
            If (Me.GLookUp_WIPLedgerListView.RowCount > 0 And Val(Me.GLookUp_WIPLedgerListView.FocusedRowHandle) >= 0) Then
                If Me.GLookUp_WIPLedgerList.Text.Trim.Length > 0 Then
                    Me.GLookUp_WIPLedgerList.Tag = Me.GLookUp_WIPLedgerListView.GetRowCellValue(Me.GLookUp_WIPLedgerListView.FocusedRowHandle, "WIP_LED_ID").ToString
                    Txn_Cr_ItemId = Me.GLookUp_WIPLedgerListView.GetRowCellValue(Me.GLookUp_WIPLedgerListView.FocusedRowHandle, "Txn_Cr_ItemId").ToString 'WIP Finalization ItemID
                    LookUp_GetFinalizedAssetList()
                    Ledger_Outstanding_References(Me.GLookUp_WIPLedgerList.Tag)
                    If GridView2.RowCount > 0 Then Sub_Amt_Calculation() Else Txt_TotalAmount.Text = "0.00"
                Else
                    Me.GLookUp_WIPLedgerList.Tag = Nothing : Me.GLookUp_WIPLedgerList.Text = ""
                End If
            Else
                Me.GLookUp_WIPLedgerList.Tag = Nothing : Me.GLookUp_WIPLedgerList.Text = ""
            End If
        Else
        End If
    End Sub
    Private Sub LookUp_GetWIPLedgerList()
        Dim d1 As DataTable = Base._WIP_Finalization_DBOps.GetListOfWIPLedgers(Base.Is_HQ_Centre)
        If d1 Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Dim dview As New DataView(d1) : dview.Sort = "WIP LEDGER"
        If dview.Count > 0 Then
            'Dim ROW As DataRow : ROW = d1.NewRow() : ROW("LED_NAME") = "" : d1.Rows.InsertAt(ROW, 0)
            Me.GLookUp_WIPLedgerList.Properties.ValueMember = "WIP_LED_ID"
            Me.GLookUp_WIPLedgerList.Properties.DisplayMember = "WIP LEDGER"
            Me.GLookUp_WIPLedgerList.Properties.DataSource = dview
            GLookUp_WIPLedgerListView.PopulateColumns(dview)
            Me.GLookUp_WIPLedgerListView.Columns("Txn_Cr_ItemId").Visible = False
            Me.GLookUp_WIPLedgerListView.Columns("WIP_LED_ID").Visible = False
            Me.GLookUp_WIPLedgerListView.Columns("Txn_Cr_ItemName").Visible = False
            Me.GLookUp_WIPLedgerListView.RefreshData()
            Me.GLookUp_WIPLedgerList.Properties.Tag = "SHOW"
        Else
            Me.GLookUp_WIPLedgerList.Properties.Tag = "NONE"
        End If
        If Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New Or Val(Me.Tag) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then Me.GLookUp_WIPLedgerList.Properties.ReadOnly = False
    End Sub
#End Region

End Class