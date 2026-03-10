'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Param Classes"
    <Serializable>
    Public Class Parameter_InsertLiability_VoucherPayment
        Public ItemID As String
        Public PartyID As String
        Public LiabilityDate As String
        Public PaymentDate As String
        Public Amount As Double
        Public Purpose As String
        Public Remarks As String
        Public TxnID As String
        Public Status_Action As String
        Public RecID As String
    End Class
    <Serializable>
    Public Class Parameter_InsertAdvances_VoucherPayment
        Public ItemID As String
        Public PartyID As String
        Public AdvanceDate As String
        Public Amount As Double
        Public Purpose As String
        Public Remarks As String
        Public TxnID As String
        Public Status_Action As String
    End Class
    <Serializable>
    Public Class Parameter_InsertDeposits_VoucherPayment
        Public ItemID As String
        Public AgainstInsurance As String
        Public PartyID As String
        Public InsCompanyMiscID As String
        Public DepositDate As String
        Public DepositPeriod As Double
        Public Amount As Double
        Public InterestRate As Double
        Public Purpose As String
        Public Remarks As String
        Public TxnID As String
        Public Status_Action As String
        Public RecID As String
    End Class
    <Serializable>
    Public Class Parameter_InsertPaymentEntryGroup
        Public MasterRecord As Parameter_InsertMasterInfo_VoucherPayment
        Public TxnRecord As Parameter_Insert_VoucherPayment
        Public AdvanceRecord As Parameter_Insert_VoucherPayment
        Public CreditRecord As Parameter_Insert_VoucherPayment
        Public TdsJVRecord As Parameter_Insert_VoucherPayment
        Public TdsJVRecord2 As Parameter_Insert_VoucherPayment
        Public ProfileLiability As Parameter_InsertLiability_VoucherPayment
        Public ProfileAdvance As Parameter_InsertAdvances_VoucherPayment
        Public ProfileDeposit As Parameter_InsertDeposits_VoucherPayment
        Public ProfileGoldSiver As Parameter_InsertTRIDAndTRSRNo_GoldSilver
        Public ProfileAssets As Parameter_InsertTRIDAndTRSrNo_Assets
        Public ProfileLivestock As Parameter_InsertTRIDAndTRSrNo_LiveStock
        Public ProfileVehicles As Parameter_InsertTRIDAndTRSrNo_Vehicles
        Public ProfileProperty As Parameter_InsertMasterIDAndSrNo_LandAndBuilding
        Public ProfilePropertyExtended As Parameter_InsertExtendedInfo_LandAndBuilding()
        Public ProfilePropertyDocs As Parameter_InsertDocInfo_LandAndBuilding()
        Public TxnItems As Parameter_InsertItem_VoucherPayment()
        Public TxnPurpose As Parameter_InsertPurpose_VoucherPayment()
        Public TxnAdvancePayment As Parameter_InsertPayment_VoucherPayment()
        Public TxnLiablityPayment As Parameter_InsertPayment_VoucherPayment()
        Public TxnBankPayment As Parameter_InsertPaymentMT_VoucherPayment()
    End Class
#End Region
    <Serializable>
    Public Class Return_RefBank
        Public BI_ID As String
        Public BI_BANK_NAME As String
        Public BI_SHORT_NAME As String
    End Class
    <Serializable>
    Public Class Return_PaymentVoucherPartyList
        Public Property C_NAME As String
        Public Property C_PAN_NO As String
        Public Property C_AADHAR_NO As String
        Public Property C_CITY As String
        Public Property C_ID As String
        Public Property C_CATEGORY As String
        Public Property REC_EDIT_ON As Date

    End Class
    <Serializable>
    Public Class Return_AdvanceAdjustment_Grid_Datatable
        Public Property Sr As Int64
        Public Property GivenDate As Date?
        Public Property Item As String
        Public Property AI_ITEM_ID As String
        Public Property OFFSET_ID As String
        Public Property Advance As Decimal?
        Public Property Addition As Decimal?
        Public Property Adjusted As Decimal?
        Public Property Refund As Decimal?
        Public Property Out_Standing As Decimal?
        Public Property Next_Year_Out_Standing As Decimal?
        Public Property Payment As Decimal?
        Public Property Purpose As String
        Public Property Other_Details As String
        Public Property AI_ID As String
        Public Property REC_EDIT_ON As Date?
        Public Property REF_CREATION_DATE As Date?
        Public Property AI_Party_ID As String
    End Class
    <Serializable>
    Public Class Return_PendingLiabilities_Grid

        Public Property Sr As Int64
        Public Property xDate As Date?
        Public Property Given_Date As Date?
        Public Property Item As String
        Public Property OFFSET_ID As String
        Public Property LI_ITEM_ID As String
        Public Property Amount As Decimal?
        Public Property Addition As Decimal?
        Public Property Adjusted As Decimal?
        Public Property Paid As Decimal?
        Public Property OutStanding As Decimal?
        Public Property Next_Year_OutStanding As Decimal?
        Public Property Payment As Decimal?
        Public Property Purpose As String
        Public Property Other_Details As String
        Public Property LI_ID As String
        Public Property REC_EDIT_ON As Date?
        Public Property REF_CREATION_DATE As Date?
        Public Property LI_PARTY_ID As String

    End Class
    <Serializable>
    Public Class Return_Items

        Public ITEMID As String
        Public ITEMNAME As String
        Public HEAD As String
        Public PARTY As String
        Public ITEM_NAME As String
        Public LED_NAME As String
        Public LED_TYPE As String
        Public ITEM_TRANS_TYPE As String
        Public ITEM_LED_ID As String
        Public ITEM_VOUCHER_TYPE As String
        Public ITEM_PARTY_REQ As String
        Public ITEM_PROFILE As String
        Public ITEM_CON_LED_ID As String
        Public ITEM_CON_MIN_VALUE As Integer
        Public ITEM_CON_MAX_VALUE As Integer
        Public CON_LED_TYPE As String
        Public ITEM_TDS_CODE As String
        Public ITEM_ID As String
        Public TDS_RATE As Integer

    End Class
    <Serializable>
    Public Class Return_Party
        Public ID As String
        Public Name As String
        Public PAN As String
        Public Aadhar As String
    End Class
    <Serializable>
    Public Class Return_Purpose
        Public PUR_ID As String
        Public PUR_NAME As String
    End Class
#Region "Accounts"
    <Serializable>
    Public Class Voucher_Payment
        Inherits SharedVariables
#Region "Param Classes"
        'Public Class Param_BasicData
        '    Public next_Unaudited_YearID As Integer
        '    Public open_Ins_ID As String
        '    Public open_UID_No As String
        '    Public open_PAD_No_Main As String
        '    Public open_Cen_Rec_ID As String
        '    Public open_Year_Sdt As Date
        '    Public open_Year_Edt As Date
        '    Public prev_Unaudited_YearID As Integer
        '    Public IsMultiUserAllowed As Boolean 'Base.AllowMultiuser()
        '    Public IsInsuranceAudited As Boolean 'Base.IsInsuranceAudited()
        'End Class
        <Serializable>
        Public Class Param_paymentVoucherFormData
            Public NavMode As Integer
            Public Rec_ID As String 'Me.xMID.Text
            Public Txt_AdvAmt As String
            Public Txt_CreditAmt As String
            Public Txt_LB_Amt As String
            Public Txt_CashAmt As String
            Public Txt_V_Date As String
            Public Txt_V_NO As String
            Public Txt_Inv_No As String
            Public Txt_Inv_Date As String
            Public Txt_SubTotal As String
            Public Txt_BankAmt As String
            Public Txt_TDS_Amt As String
            Public Txt_DueDate As String
            Public Txt_Narration As String
            Public Txt_Reference As String
            Public GLookUp_PartyList1_Tag As String 'Me.GLookUp_PartyList1.Text 'Me.GLookUp_PartyList1.Tag
            Public GLookUp_PartyList1_Txt As String 'Me.GLookUp_PartyList1.Text 'Me.GLookUp_PartyList1.Tag
            Public oldREC_EDIT_ON As System.DateTime 'Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle "REC_EDIT_ON")
            Public GridView1 As DataTable = Nothing
            Public DTGridView2 As DataTable = Nothing 'Me.GridView2
            Public GridView3 As DataTable = Nothing
            Public GridView4 As DataTable = Nothing
            Public IsChk_Incompleted As Boolean 'Chk_Incompleted.Checked
            Public xID As String 'Me.xID.Text
            Public TitleX As String 'Me.TitleX.Text
            Public WindowText As String 'Me.Text
        End Class
        <Serializable>
        Public Class Param_paymentVoucherGlobalData
            Public Bank_Detail As DataTable
            Public LastEditedOn As DateTime
            Public DT As DataTable
            Public LB_EXTENDED_PROPERTY_TABLE As DataTable
            Public LB_DOCS_ARRAY As DataTable
            Public iParty_Req As Boolean  'private
        End Class
        <Serializable>
        Public Class Param_paymentVoucherDetails
            Public GlobalData As Param_paymentVoucherGlobalData
            Public FormData As Param_paymentVoucherFormData
            ' Public BasicData As Param_BasicData
            Public Pmt_SplVchrReferenceSelected As String
        End Class
        <Serializable>
        Public Class Param_SaveButtonChecks
            Public messageBoxText As String = ""
            Public messageCaption As String = ""
            Public messageIcon As String = ""
            Public dialogResult As String = ""
            Public focusControlId As String = ""
            Public ToolTipTitle As String = "" 'Need to check if this can be hardcoded
            Public ToolTipText As String = ""
            Public ToolTipWindow As String = ""
            Public SelectedTabPage As String = ""
            Public CashbookGridPK As String = ""
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatusByMID(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, "TR_M_ID", ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        'Shifted
        Public Function GetItemsByID(ByVal Item_Rec_Id As String) As DataTable
            Dim Query As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & Item_Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            inParam.ItemIDs = Item_Rec_Id
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Payment, inParam) 'Type=1
        End Function

        'Shifted
        Public Function GetAdvanceItems() As DataTable
            Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME,ITEM_OFFSET_REC_ID    from ITEM_INFO where  ITEM_PROFILE='ADVANCES' AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "2"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Payment, inParam) 'type=2
        End Function

        'Shifted
        Public Function GetLiabilitiesItems() As DataTable
            Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME,ITEM_OFFSET_REC_ID    from ITEM_INFO where  ITEM_PROFILE='OTHER LIABILITIES' AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "3"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Payment, inParam) 'type=3
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Acc_Rec_ID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Acc_Rec_ID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        Public Function GetWIP_Ref_Count(ByVal Led_ID As String) As DataTable
            Dim _WIP As WIP_Creation_Vouchers = New WIP_Creation_Vouchers(cBase)
            Return _WIP.GetCountOfReferencesByLedID(Led_ID)
        End Function

        'Shifted
        Public Function GetBranches(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_Payment, " B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID, B.REC_ID AS BANK_ID ")
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBanks() As List(Of Return_RefBank)
            Dim Query As String = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID  From  BANK_INFO Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Dim ret_table As DataTable = GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Payment)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim banklist = New List(Of Return_RefBank)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_RefBank()
                    newdata.BI_ID = row.Field(Of String)("BI_ID")
                    newdata.BI_BANK_NAME = row.Field(Of String)("BI_BANK_NAME")
                    newdata.BI_SHORT_NAME = row.Field(Of String)("BI_SHORT_NAME")
                    banklist.Add(newdata)
                Next
                Return banklist
            End If
        End Function

        'Shifted
        Public Function GetBanks(ByVal Bank_IDs As String) As DataTable
            Dim Query As String = "SELECT REC_ID,BI_BANK_NAME  From  BANK_INFO  Where  REC_STATUS IN (0,1,2) AND REC_ID IN (" & Bank_IDs & ")  ;"
            Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Payment, Bank_IDs)
        End Function

        ''' <summary>
        ''' Get Transaction Details By RefID, Shifted
        ''' </summary>
        ''' <param name="RefID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetTxnDetailsByRefID</remarks>
        Public Function GetTxnDetailsByRefID(ByVal RefID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTxnDetailsByRefID, ClientScreen.Accounts_Voucher_Payment, RefID)
        End Function

        'Shifted
        Public Function GetLedgerItems(ByVal Is_HQ_Centre As Boolean) As List(Of Return_Items)

            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS CON_LED_TYPE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                               " FROM (Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID) LEFT JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT','LAND & BUILDING','LAND & BUILDING / Gift')              AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            LocalQuery += "              Union All " &
                  "              SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS CON_LED_TYPE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                  "              FROM (Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID) LEFT JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE')  AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            OnlineParam.open_Ins_ID = cBase._open_Ins_ID
            Dim ret_table As DataTable = GetItems_Ledger(ClientScreen.Accounts_Voucher_Payment, LocalQuery, OnlineParam)
            Dim itemlist As List(Of Return_Items) = New List(Of Return_Items)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_Items = New Return_Items()
                    newdata.ITEM_ID = row.Field(Of String)("ITEM_ID")
                    newdata.ITEM_NAME = row.Field(Of String)("ITEM_NAME")
                    newdata.LED_NAME = row.Field(Of String)("LED_NAME")
                    newdata.ITEM_PARTY_REQ = row.Field(Of String)("ITEM_PARTY_REQ")
                    newdata.CON_LED_TYPE = row.Field(Of String)("CON_LED_TYPE")
                    'newdata.HEAD = row.Field(Of String)("HEAD")
                    'newdata.ITEMID = row.Field(Of String)("ITEMID")
                    'newdata.ITEMNAME = row.Field(Of String)("ITEMNAME")
                    newdata.ITEM_CON_LED_ID = row.Field(Of String)("ITEM_CON_LED_ID")
                    newdata.ITEM_CON_MAX_VALUE = row.Field(Of Integer)("ITEM_CON_MAX_VALUE")
                    newdata.ITEM_CON_MIN_VALUE = row.Field(Of Integer)("ITEM_CON_MIN_VALUE")
                    newdata.ITEM_LED_ID = row.Field(Of String)("ITEM_LED_ID")
                    newdata.ITEM_PROFILE = row.Field(Of String)("ITEM_PROFILE")
                    newdata.ITEM_TDS_CODE = row.Field(Of String)("ITEM_TDS_CODE")
                    newdata.ITEM_TRANS_TYPE = row.Field(Of String)("ITEM_TRANS_TYPE")
                    newdata.ITEM_VOUCHER_TYPE = row.Field(Of String)("ITEM_VOUCHER_TYPE")
                    newdata.LED_TYPE = row.Field(Of String)("LED_TYPE")
                    'newdata.PARTY = row.Field(Of String)("PARTY")
                    newdata.TDS_RATE = row.Field(Of Integer)("TDS_RATE")
                    itemlist.Add(newdata)
                Next
                Return itemlist
            End If
        End Function

        'Shifted
        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As List(Of Return_Purpose)

            '  Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Receipt, MiscNameColumnHead, RecIDColumnHead)
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = MiscNameColumnHead
            InParam.RecIDColumnHead = RecIDColumnHead
            Dim ret_table As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Payment, InParam)
            Dim purposelist As List(Of Return_Purpose) = New List(Of Return_Purpose)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_Purpose = New Return_Purpose()
                    newdata.PUR_ID = row.Field(Of String)("PUR_ID")
                    newdata.PUR_NAME = row.Field(Of String)("PUR_NAME")
                    purposelist.Add(newdata)
                Next
                Return purposelist
            End If
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Rec_ID, "TR_SR_NO", "1", ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String, ByVal TableName As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Rec_ID, ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_INFO, TableName)
        End Function

        Public Function GetOtherRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByCustom("WHERE TR_M_ID ='" & Rec_ID & "' ", ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_D_OTHER_INFO)
        End Function

        ''' <summary>
        ''' Get Txn Items Detail, Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetTxnItemsDetail</remarks>
        Public Function GetTxnItemsDetail(ByVal MasterID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTxnItemsDetail, ClientScreen.Accounts_Voucher_Payment, MasterID)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetTxnBankPaymentDetail</remarks>
        Public Function GetTxnBankPaymentDetail(ByVal MasterID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTxnBankPaymentDetail, ClientScreen.Accounts_Voucher_Payment, MasterID)
        End Function

        ''' <summary>
        ''' Get Txn Payment Detail, Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="Type"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetTxnPaymentDetail</remarks>
        Public Function GetTxnPaymentDetail(ByVal MasterID As String, ByVal Type As String) As DataTable
            Dim Param As Param_Payments_GetTxnPaymentDetail = New Param_Payments_GetTxnPaymentDetail()
            Param.MasterID = MasterID
            Param.Type = Type
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTxnPaymentDetail, ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        'Shifted
        Public Function GetGoldSilverList(ByVal RecID As String) As DataTable
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Return _GS.GetList(ClientScreen.Accounts_Voucher_Payment, RecID)
        End Function

        'Shifted
        Public Function GetAssetList(ByVal RecID As String) As DataTable
            Dim _Assets As Assets = New Assets(cBase)
            Return _Assets.GetList(ClientScreen.Accounts_Voucher_Payment, RecID)
        End Function

        'Shifted
        Public Function GetVehiclesList(ByVal RecID As String) As DataTable
            Dim _Veh As Vehicles = New Vehicles(cBase)
            Return _Veh.GetList(ClientScreen.Accounts_Voucher_Payment, RecID)
        End Function

        'Shifted
        Public Function GetLandBuilingList(ByVal RecID As String) As DataTable
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Dim Param As Param_LandAndBuilding_GetListByQuery = New Param_LandAndBuilding_GetListByQuery()
            Param.RecID = RecID
            Param.Type = "1"
            Return _LB.GetListByQuery(ClientScreen.Accounts_Voucher_Payment, Param) 'Type 1
        End Function

        Public Function Get_WIP_List(ByVal RecID As String) As DataTable
            Dim _WIP As WIP_Profile = New WIP_Profile(cBase)
            Return _WIP.Get_WIP_List(ClientScreen.Accounts_Voucher_Payment, RecID)
        End Function

        'shifted
        Public Function GetLandBuildingNames(ByVal MasterID As String)
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Dim Param As Param_LandAndBuilding_GetListByQuery = New Param_LandAndBuilding_GetListByQuery()
            Param.MasterID = MasterID
            Param.Type = "2"
            Return _LB.GetListByQuery(ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        'No need to shift
        Public Function GetLandBuildingExtensions(ByVal LB_ID As String)
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Return _LB.GetExtendedRecord(LB_ID)
        End Function

        'No need to shift
        Public Function GetLandBuildingDocs(ByVal LB_ID As String)
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Return _LB.GetDocumentRecord(LB_ID)
        End Function

        'Shifted
        Public Function GetTDSRate(ByVal PAN As String, ByVal TDSCode As String) As DataTable
            Dim Query As String = ""
            If PAN.Length > 4 Then
                Query = "SELECT TDS_RATE FROM tds_info WHERE TDS_PAN_CODE ='" & PAN.Substring(3, 1) & "' AND TDS_CODE='" & TDSCode & "'"
            Else
                Query = "SELECT TDS_RATE FROM tds_info WHERE TDS_PAN_CODE ='NONE' AND TDS_CODE='" & TDSCode & "'"
            End If
            Dim Param As Param_GetTDSRecords_Common = New Param_GetTDSRecords_Common()
            Param.PAN = PAN
            Param.TDSCode = TDSCode
            Return GetTDSRecords(Query, ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        'Shifted
        Public Function GetLiveStockList(ByVal RecID As String) As DataTable
            Dim _LS As LiveStock = New LiveStock(cBase)
            Return _LS.GetList(ClientScreen.Accounts_Voucher_Payment, RecID)
        End Function

        'Shifted
        Public Function GetParties(Optional Party_Rec_ID As String = Nothing) As List(Of Return_PaymentVoucherPartyList)

            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.Type = "PARTY"
            Param.Party_Rec_ID = Party_Rec_ID
            Dim _Add As Addresses = New Addresses(cBase)
            Dim ret_table As DataTable = _Add.GetList(ClientScreen.Accounts_Voucher_Payment, Param)
            'Add extra parameter in online call stating that this is a Party List
            'String Type = "PARTY"
            Dim partylist As List(Of Return_PaymentVoucherPartyList) = New List(Of Return_PaymentVoucherPartyList)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_PaymentVoucherPartyList = New Return_PaymentVoucherPartyList()
                    newdata.C_CITY = row.Field(Of String)("C_CITY")
                    newdata.C_ID = row.Field(Of String)("C_ID")
                    newdata.C_NAME = row.Field(Of String)("C_NAME")
                    newdata.C_PAN_NO = row.Field(Of String)("C_PAN_NO")
                    newdata.C_AADHAR_NO = row.Field(Of String)("C_AADHAR_NO")
                    newdata.REC_EDIT_ON = row.Field(Of Date)("REC_EDIT_ON")
                    If ret_table.Columns.IndexOf("CATEGORY") > -1 Then
                        newdata.C_CATEGORY = row.Field(Of String)("CATEGORY")
                    End If
                    partylist.Add(newdata)
                Next
                Return partylist
            End If
        End Function

        'Shifted
        Public Function GetParties(ByVal NameColHead As String, ByVal IdColHead As String) As List(Of Return_Party)

            Dim _Add As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.Type = "PARTY_WITHH_CUSTOM_COL_HEADS"
            Param.NameColHead = NameColHead
            Param.IdColHead = IdColHead
            Dim ret_table As DataTable = _Add.GetList(ClientScreen.Accounts_Voucher_Payment, Param)
            'Add extra parameter in online call stating that this is a Party List
            'String Type = "PARTY_WITHH_CUSTOM_COL_HEADS"
            Dim partylist As List(Of Return_Party) = New List(Of Return_Party)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_Party = New Return_Party()
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Name = row.Field(Of String)("NAME")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.Aadhar = row.Field(Of String)("Aadhar")
                    partylist.Add(newdata)
                Next
                Return partylist
            End If
        End Function

        'Shifted
        Public Function GetPartyCities(ByVal CityIDs) As DataTable
            Return GetCitiesByID(ClientScreen.Accounts_Voucher_Gift, "CI_NAME", "REC_ID", CityIDs)
        End Function

        'Shifted
        Public Function GetPendingAdvances(ByVal Param As Param_GetPaymentAdvances) As List(Of Return_AdvanceAdjustment_Grid_Datatable)
            'Dim _Adv As Advances = New Advances(cBase)
            'Dim Param As Param_Advances_GetList_Common = New Param_Advances_GetList_Common()
            'Param.PartyID = PartyID
            'Return _Adv.GetList(ClientScreen.Accounts_Voucher_Payment, Param)
            Dim ret_table As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Advances_GetPaymentAdvances, ClientScreen.Accounts_Voucher_Payment, Param)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                Dim gridData = New List(Of Return_AdvanceAdjustment_Grid_Datatable)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_AdvanceAdjustment_Grid_Datatable
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.GivenDate = row.Field(Of Date?)("Given Date")
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.AI_ITEM_ID = row.Field(Of String)("AI_ITEM_ID")
                    newdata.OFFSET_ID = row.Field(Of String)("OFFSET_ID")
                    newdata.Advance = row.Field(Of Decimal?)("Advance")
                    newdata.Addition = row.Field(Of Decimal?)("Addition")
                    newdata.Adjusted = row.Field(Of Decimal?)("Adjusted")
                    newdata.Refund = row.Field(Of Decimal?)("Refund")
                    newdata.Out_Standing = row.Field(Of Decimal?)("Out-Standing")
                    newdata.Next_Year_Out_Standing = row.Field(Of Decimal)("Next Year Out-Standing")
                    newdata.Payment = row.Field(Of Decimal?)("Payment")
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    newdata.Other_Details = row.Field(Of String)("Other Details")
                    newdata.AI_ID = row.Field(Of String)("AI_ID")
                    newdata.REC_EDIT_ON = row.Field(Of Date?)("REC_EDIT_ON")
                    newdata.REF_CREATION_DATE = row.Field(Of Date?)("REF_CREATION_DATE")
                    newdata.AI_Party_ID = Param.Adv_Party_ID
                    gridData.Add(newdata)

                Next
                Return gridData

            End If
        End Function

        'Shifted
        Public Function GetPendingLiabilities(Param As Param_GetPaymentLiabilities) As List(Of Return_PendingLiabilities_Grid)
            'Dim _Liab As Liabilities = New Liabilities(cBase)
            'Dim param As Param_Liabilities_GetListCommon = New Param_Liabilities_GetListCommon
            'param.PartyID = PartyID
            'Return _Liab.GetList(ClientScreen.Accounts_Voucher_Payment, param)
            Dim ret_table As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Liabilities_GetPaymentLiabilities, ClientScreen.Accounts_Voucher_Payment, Param)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                Dim datalist = New List(Of Return_PendingLiabilities_Grid)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_PendingLiabilities_Grid()
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Addition = row.Field(Of Decimal?)("Addition")
                    newdata.Adjusted = row.Field(Of Decimal?)("Adjusted")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    newdata.xDate = row.Field(Of DateTime?)("Date")
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.LI_ID = row.Field(Of String)("LI_ID")
                    newdata.LI_ITEM_ID = row.Field(Of String)("LI_ITEM_ID")
                    newdata.Next_Year_OutStanding = row.Field(Of Decimal?)("Next Year Out-Standing")
                    newdata.OFFSET_ID = row.Field(Of String)("OFFSET_ID")
                    newdata.Other_Details = row.Field(Of String)("Other Details")
                    newdata.OutStanding = row.Field(Of Decimal?)("Out-Standing")
                    newdata.Paid = row.Field(Of Decimal?)("Paid")
                    newdata.Payment = row.Field(Of Decimal?)("Payment")
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    newdata.REC_EDIT_ON = row.Field(Of Date?)("REC_EDIT_ON")
                    newdata.REF_CREATION_DATE = row.Field(Of Date?)("REF_CREATION_DATE")
                    newdata.LI_PARTY_ID = Param.LI_Party_ID
                    If ret_table.Columns.Contains("Given Date") Then
                        newdata.Given_Date = row.Field(Of Date?)("Given Date")
                    End If
                    datalist.Add(newdata)
                Next
                Return datalist
            End If


        End Function

        Public Function GetPendingTDSList(TR_M_ID As String) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Payments_GetPendingTDSList, ClientScreen.Accounts_Voucher_Payment, TR_M_ID)
        End Function

        ''' <summary>
        ''' Gets Advances Paid, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="PartyID"></param>
        ''' <param name="ExcludeCurrent"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetAdvancesPaid</remarks>
        Public Function GetAdvancesPaid(ByVal Rec_ID As String, ByVal PartyID As String, ByVal ExcludeCurrent As Boolean, YearID As Integer, Optional NextUnauditedYear As Integer = Nothing) As DataTable
            Dim Param As Param_Payments_GetAdvancesPaid = New Param_Payments_GetAdvancesPaid()
            Param.Rec_ID = Rec_ID
            Param.PartyID = PartyID
            Param.ExcludeCurrent = ExcludeCurrent
            Param.YearID = YearID
            Param.NextUnauditedYearID = NextUnauditedYear
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetAdvancesPaid, ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        ''' <summary>
        ''' Gets Liabilities Paid, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="PartyID"></param>
        ''' <param name="ExcludeCurrent"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_GetLiabilitiesPaid</remarks>
        Public Function GetLiabilitiesPaid(ByVal Rec_ID As String, ByVal PartyID As String, ByVal ExcludeCurrent As Boolean, YearID As Integer, Optional NextUnauditedYear As Integer = Nothing) As DataTable
            Dim Param As Param_Payments_GetLiabilitiesPaid = New Param_Payments_GetLiabilitiesPaid()
            Param.Rec_ID = Rec_ID
            Param.PartyID = PartyID
            Param.ExcludeCurrent = ExcludeCurrent
            Param.YearId = YearID
            Param.NextUnauditedYearID = NextUnauditedYear
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetLiabilitiesPaid, ClientScreen.Accounts_Voucher_Payment, Param)
        End Function

        ''Payments_GetTDS_Deducted_Not_Sent
        Public Function GetTDS_Deducted_Not_Sent(ByVal Tr_M_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTDS_Deducted_Not_Sent, ClientScreen.Accounts_Voucher_Payment, Tr_M_ID)
        End Function

        Public Function GetTDS_Deducted_Not_Paid(ByVal Tr_M_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Payments_GetTDS_Deducted_Not_Paid, ClientScreen.Accounts_Voucher_Payment, Tr_M_ID)
        End Function

        Public Function Get_CreatedAssets_MinTxnDate(ByVal Tr_M_ID As String, VDate As DateTime) As DataTable
            Dim Param As New RealTimeService.Param_GetCreatedAsset_MinTxnDate
            Param.Tr_M_ID = Tr_M_ID
            Param.Voucher_date = VDate
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Payments_Get_CreatedAssets_MinTxnDate, ClientScreen.Accounts_Voucher_Payment, Param)
        End Function


        ''' <summary>
        ''' Inserts Master Info, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherPayment) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertMasterInfo, ClientScreen.Accounts_Voucher_Payment, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherPayment) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_Insert, ClientScreen.Accounts_Voucher_Payment, InParam)
        End Function

        ''' <summary>
        ''' Insert Item, Shifted
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertItem</remarks>
        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherPayment) As Boolean
            'InItem.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertItem, ClientScreen.Accounts_Voucher_Payment, InItem)
        End Function

        ''' <summary>
        ''' Insert Payment, Shifted
        ''' </summary>
        ''' <param name="InPayment"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertPayment</remarks>
        Public Function InsertPayment(ByVal InPayment As Parameter_InsertPayment_VoucherPayment) As Boolean
            'InPayment.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertPayment, ClientScreen.Accounts_Voucher_Payment, InPayment)
        End Function

        'Public Function InsertPayment(ByVal InPay1 As Parameter_InsertPaymentMT_VoucherPayment) As Boolean
        '    Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_MT_BANK_ID,TR_MT_ACC_NO," & _
        '                                "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
        '                                ") VALUES(" & _
        '                                "'" & cBase._open_Cen_ID & "'," & _
        '                                "'" & cBase._open_Year_ID & "'," & _
        '                                "'" & InPay1.TxnMID & "', " & _
        '                                "'" & InPay1.Type & "', " & _
        '                                " " & InPay1.SrNo & " , " & _
        '                                "'" & InPay1.Mode & "', " & _
        '                                "'" & InPay1.RefID & "', " & _
        '                                "'" & InPay1.RefNo & "', " & _
        '                                " " & If(IsDate(InPay1.RefDate), "'" & Convert.ToDateTime(InPay1.RefDate).ToString(cBase._Server_Date_Format_Long) & "'", " NULL ") & " , " & _
        '                                " " & If(IsDate(InPay1.ClearingDate), "'" & Convert.ToDateTime(InPay1.ClearingDate).ToString(cBase._Server_Date_Format_Long) & "'", " NULL ") & " , " & _
        '                                " " & InPay1.RefAmount & ", " & _
        '                                "'" & InPay1.MT_Bank_Id & "', " & _
        '                                "'" & InPay1.MT_AccNo & "', " & _
        '                                "'" & DateTimePlaceHolder & "', '" & cBase._open_User_ID & "', '" & DateTimePlaceHolder & "', '" & cBase._open_User_ID & "', " & InPay1.Status_Action & ", '" & DateTimePlaceHolder & "', '" & cBase._open_User_ID & "', '" & InPay1.RecID & "'" & ")"

        '    Dim LocalQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_MT_BANK_ID,TR_MT_ACC_NO," & _
        '                                "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
        '                                ") VALUES(" & _
        '                                "'" & cBase._open_Cen_ID & "'," & _
        '                                "'" & cBase._open_Year_ID & "'," & _
        '                                "'" & InPay1.TxnMID & "', " & _
        '                                "'" & InPay1.Type & "', " & _
        '                                " " & InPay1.SrNo & " , " & _
        '                                "'" & InPay1.Mode & "', " & _
        '                                "'" & InPay1.RefID & "', " & _
        '                                "'" & InPay1.RefNo & "', " & _
        '                                " " & If(IsDate(InPay1.RefDate), "#" & Convert.ToDateTime(InPay1.RefDate).ToString(cBase._Date_Format_Long) & "#", " NULL ") & " , " & _
        '                                " " & If(IsDate(InPay1.ClearingDate), "#" & Convert.ToDateTime(InPay1.ClearingDate).ToString(cBase._Date_Format_Long) & "#", " NULL ") & " , " & _
        '                                " " & InPay1.RefAmount & ", " & _
        '                                "'" & InPay1.MT_Bank_Id & "', " & _
        '                                "'" & InPay1.MT_AccNo & "', " & _
        '                                "#" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', #" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', " & InPay1.Status_Action & ", #" & Now.ToString(cBase._Date_Format_Long) & "#, '" & cBase._open_User_ID & "', '" & InPay1.RecID & "'" & _
        '                                ")"
        '    Return InsertRecord(OnlineQuery, LocalQuery, ClientScreen.Accounts_Voucher_Payment, RealTimeService.Tables.TRANSACTION_D_PAYMENT_INFO, cBase._data_ConStr_Data, InPay1.RecID)
        'End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="InPay1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertPaymentMT</remarks>
        Public Function InsertPayment(ByVal InPay1 As Parameter_InsertPaymentMT_VoucherPayment) As Boolean
            'InPay1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertPaymentMT, ClientScreen.Accounts_Voucher_Payment, InPay1)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherPayment) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertPurpose, ClientScreen.Accounts_Voucher_Payment, InPurpose)
        End Function

        Public Function InsertOther(ByVal InOther As Parameter_InsertOther_VoucherPayment) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Payments_InsertOther, ClientScreen.Accounts_Voucher_Payment, InOther)
        End Function

        'Shifted
        Public Function InsertLiability(ByVal InLiab As Parameter_InsertLiability_VoucherPayment) As Boolean
            Dim _Liab As Liabilities = New Liabilities(cBase)
            Dim inParam As Parameter_InsertTRID_Liabilities = New Parameter_InsertTRID_Liabilities()
            inParam.ItemID = InLiab.ItemID
            inParam.PartyID = InLiab.PartyID
            inParam.LiabilityDate = InLiab.LiabilityDate
            inParam.PaymentDate = InLiab.PaymentDate
            inParam.Amount = InLiab.Amount
            inParam.Purpose = InLiab.Purpose
            inParam.Remarks = InLiab.Remarks
            inParam.TxnID = InLiab.TxnID
            inParam.Status_Action = InLiab.Status_Action
            inParam.RecID = InLiab.RecID
            inParam.Screen = ClientScreen.Accounts_Voucher_Payment
            Return _Liab.Insert(inParam)
        End Function

        'Shifted
        Public Function InsertAdvances(ByVal InAdv As Parameter_InsertAdvances_VoucherPayment)
            Dim _Adv As Advances = New Advances(cBase)
            Dim inParam As Parameter_InsertTRID_Advances = New Parameter_InsertTRID_Advances()
            inParam.ItemID = InAdv.ItemID
            inParam.PartyID = InAdv.PartyID
            inParam.AdvanceDate = InAdv.AdvanceDate
            inParam.Amount = InAdv.Amount
            inParam.Purpose = InAdv.Purpose
            inParam.Remarks = InAdv.Remarks
            inParam.TxnID = InAdv.TxnID
            inParam.Status_Action = InAdv.Status_Action
            inParam.Screen = ClientScreen.Accounts_Voucher_Payment
            Return _Adv.Insert(inParam)
        End Function

        'Shifted
        Public Function InsertDeposits(ByVal InDeposits As Parameter_InsertDeposits_VoucherPayment)
            Dim _Dep As Deposits = New Deposits(cBase)
            Dim inParam As Parameter_InsertTRID_Deposits = New Parameter_InsertTRID_Deposits()
            inParam.ItemID = InDeposits.ItemID
            inParam.AgainstInsurance = InDeposits.AgainstInsurance
            inParam.PartyID = InDeposits.PartyID
            inParam.InsCompanyMiscID = InDeposits.InsCompanyMiscID
            inParam.DepositDate = InDeposits.DepositDate
            inParam.DepositPeriod = InDeposits.DepositPeriod
            inParam.Amount = InDeposits.Amount
            inParam.InterestRate = InDeposits.InterestRate
            inParam.Purpose = InDeposits.Purpose
            inParam.Remarks = InDeposits.Remarks
            inParam.TxnID = InDeposits.TxnID
            inParam.Status_Action = InDeposits.Status_Action
            inParam.RecID = InDeposits.RecID
            inParam.Screen = ClientScreen.Accounts_Voucher_Payment
            Return _Dep.Insert(inParam)
        End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherPayment) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Payment)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Payments_UpdateMaster, ClientScreen.Accounts_Voucher_Payment, UpParam)
        End Function

        Public Overloads Function DeleteMaster(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteRecord(Master_Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Overloads Function Delete(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Overloads Function DeleteItems(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Overloads Function DeletePurpose(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteByCondition("TR_REC_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Overloads Function DeleteOther(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_OTHER_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Overloads Function DeletePayment(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Payment)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_Payment)
        End Function

        Public Function InsertPayment_Txn(InParam As Param_Txn_Insert_VoucherPayment) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Payments_InsertPayment_Txn, ClientScreen.Accounts_Voucher_Payment, InParam)
        End Function

        Public Function UpdatePayment_Txn(UpParam As Param_Txn_Update_VoucherPayment) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateMaster.RecID, ClientScreen.Accounts_Voucher_Payment)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Payments_UpdatePayment_Txn, ClientScreen.Accounts_Voucher_Payment, UpParam)
        End Function

        Public Function DeletePayment_Txn(DelParam As Param_Txn_Delete_VoucherPayment) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_Payment)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Payments_DeletePayment_Txn, ClientScreen.Accounts_Voucher_Payment, DelParam)
        End Function

        Public Function Payment_GetRecordByColumn(ByVal TableName As Tables, ColumnName As String, Value As Object) As DataTable
            Return GetRecordByColumn(ColumnName, Value, ClientScreen.Accounts_Voucher_Payment, TableName)
        End Function
        Public Function Payment_GetRecordByColumn(ByVal TableName As Tables, ColumnName As String, Value As Object, Column2Name As String, Value2 As Object) As DataTable
            Return GetRecordByColumn(ColumnName, Value, Column2Name, Value2, ClientScreen.Accounts_Voucher_Payment, TableName)
        End Function
        Public Function Payment_GetRecordStatus(ByVal ID As String, Tablename As Tables, CenIDColName As String, RecIDColName As String) As DataTable
            Return GetRecordStatus(ID, ClientScreen.Accounts_Voucher_Payment, Tablename, CenIDColName, RecIDColName)
        End Function
        Public Function FindLocationUsage(PropertyID As String, Open_UID_No As String, Nav_mode As Common_Lib.Common.Navigation_Mode, Optional Exclude_Sold_TF As Boolean = True) As String
            Dim Message As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            ' basic_param.screen = ClientScreen.Accounts_Voucher_Gift
            Dim Locations As DataTable = cBase._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Payment, PropertyID) ' Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetListByLBID, _BasicParam, PropertyID))
            For Each cRow As DataRow In Locations.Rows
                Dim LocationID As String = cRow(0).ToString()
                Dim UsedPage As String = CheckLocationUsage(LocationID, Open_UID_No, Exclude_Sold_TF)
                Dim DeleteAllow As Boolean = True
                If UsedPage.Length > 0 Then DeleteAllow = False
                If Not DeleteAllow Then
                    If Val(Nav_mode) = Common_Lib.Common.Navigation_Mode._Delete Then
                        Message = "Can't Delete...!<br><br>Property Created in this Voucher is being used in Another Page as Location...!<br><br>Name : " & UsedPage
                    Else
                        Message = "Can't Edit...!<br><br>Property Created in this Voucher is being used in Another Page as Location...!<br><br>Name : " & UsedPage
                    End If
                    Exit For
                End If
            Next
            Return Message
        End Function
        Public Function CheckLocationUsage(Loc_ID As String, Open_UID_No As String, Optional Exclude_sold_Tf As Boolean = True) As String
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim MaxValue As DataTable = Nothing
            Dim DeleteAllow As Boolean = True : Dim UsedPage As String = ""
            'Dim paramCheck_Location_Count As Param_Check_Location_Count = New Param_Check_Location_Count
            'paramCheck_Location_Count.LocationID = Loc_ID
            'paramCheck_Location_Count.Exclude_Sold_TF = Exclude_sold_Tf
            If DeleteAllow Then
                'basic_param.screen = ClientScreen.Profile_Assets

                MaxValue = cBase._AssetLocDBOps.GetLocationCountInAssets(ClientScreen.Profile_Assets, Loc_ID, Exclude_sold_Tf)
                ' MaxValue = Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetLocationCountInAssets, _BasicParam, paramCheck_Location_Count))
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError) 'Vasu: Need to change exception
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Movable Asset Information..." : If MaxValue.Rows(0)("CEN_UID") <> Open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '2
            If DeleteAllow Then
                'MaxValue = 0
                'basic_param.screen = ClientScreen.Profile_GoldSilver
                '  Dim Query As String = "SELECT COUNT(GS_LOC_AL_ID), CEN_UID FROM GOLD_SILVER_INFO AS GS INNER JOIN CENTRE_INFO AS CI ON GS_CEN_ID = CEN_ID  WHERE REC_STATUS IN (0,1,2) AND GS_CEN_ID='" & cBase._open_Cen_ID & "' AND GS_LOC_AL_ID  = '" & LocationID & "' "
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInGS(ClientScreen.Profile_GoldSilver, Loc_ID, Exclude_sold_Tf)
                'MaxValue = AssetLocations.GetLocationCountInGS(CType(paramCheck_Location_Count, AssetLocations.Param_Check_Location_Count), basic_param)
                'MaxValue = Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetLocationCountInGS, _BasicParam, paramCheck_Location_Count))
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Gold / Silver Information..." : If MaxValue.Rows(0)("CEN_UID") <> Open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '3
            If DeleteAllow Then
                'MaxValue = 0
                'basic_param.screen = ClientScreen.Profile_StockOfConsumables
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInConsumables(ClientScreen.Profile_StockOfConsumables, Loc_ID, Exclude_sold_Tf)
                ' MaxValue = AssetLocations.GetLocationCountInConsumables(CType(paramCheck_Location_Count, AssetLocations.Param_Check_Location_Count), basic_param)
                'MaxValue = Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetLocationCountInConsumables, _BasicParam, paramCheck_Location_Count))
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Stock of Consumables..." : If MaxValue.Rows(0)("CEN_UID") <> Open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '4
            If DeleteAllow Then
                ' MaxValue = 0
                ' basic_param.screen = ClientScreen.Profile_LiveStock
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInLiveStock(ClientScreen.Profile_LiveStock, Loc_ID, Exclude_sold_Tf)
                'MaxValue = AssetLocations.GetLocationCountInLiveStock(CType(paramCheck_Location_Count, AssetLocations.Param_Check_Location_Count), basic_param)
                'MaxValue = Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetLocationCountInLiveStock, _BasicParam, paramCheck_Location_Count))
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Livestock..." : If MaxValue.Rows(0)("CEN_UID") <> Open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '5
            If DeleteAllow Then
                ' MaxValue = 0
                ' basic_param.screen = ClientScreen.Profile_Vehicles
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInVehicles(ClientScreen.Profile_Vehicles, Loc_ID, Exclude_sold_Tf)
                ' MaxValue = AssetLocations.GetLocationCountInVehicles(CType(paramCheck_Location_Count, AssetLocations.Param_Check_Location_Count), basic_param)
                'MaxValue = Decompress_Data(dbService.Wrap_List(RealServiceFunctions.AssetLocations_GetLocationCountInVehicles, _BasicParam, paramCheck_Location_Count))
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Vehicle Information..." : If MaxValue.Rows(0)("CEN_UID") <> Open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            Return UsedPage
        End Function
        Public Function Get_Inv_No_Count(ByVal PartyID As String, InvNo As String, tr_M_Id As String) As Object
            Dim inparam As New PAram_Get_Inv_No_Count
            inparam.InvNo = InvNo
            inparam.PartyID = PartyID
            inparam.tr_M_Id = tr_M_Id
            Dim result = GetSingleValue_Data(RealTimeService.RealServiceFunctions.Payments_Get_Inv_No_Count, ClientScreen.Accounts_Voucher_Payment, inparam)
            If result Is System.DBNull.Value Then
                Return 0
            Else
                Return CInt(result)
            End If
        End Function
        Private Shared Function ConvertAsDecimal(Val As String) As Decimal
            If Val.Length = 0 Then Return 0
            Return Convert.ToDecimal(Val)
        End Function
        Public Function SavePaymentDetails(ByVal input_Param As Param_paymentVoucherDetails) As Param_SaveButtonChecks
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Set values
            Dim open_Ins_ID As String = cBase._open_Ins_ID
            Dim open_UID_No As String = cBase._open_UID_No
            Dim open_PAD_No_Main As String = cBase._open_PAD_No_Main
            Dim open_Cen_Rec_ID As String = cBase._open_Cen_Rec_ID
            Dim open_Year_Sdt As Date = cBase._open_Year_Sdt
            Dim open_Year_Edt As Date = cBase._open_Year_Edt
            Dim prev_Unaudited_YearID As String = cBase._prev_Unaudited_YearID
            Dim next_Unaudited_YearID As String = cBase._next_Unaudited_YearID
            Dim IsMultiUserAllowed As Boolean = cBase.AllowMultiuser 'Base.AllowMultiuser()
            Dim IsInsuranceAudited As Boolean = cBase.IsInsuranceAudited 'Base.IsInsuranceAudited()

            'Filled Form Values

            Dim NavMode As Common_Lib.Common.Navigation_Mode = DirectCast(input_Param.FormData.NavMode, Common_Lib.Common.Navigation_Mode)
            Dim Rec_ID As String = input_Param.FormData.Rec_ID
            Dim Txt_AdvAmt As String = input_Param.FormData.Txt_AdvAmt
            Dim Txt_CreditAmt As String = input_Param.FormData.Txt_CreditAmt
            Dim Txt_LB_Amt As String = input_Param.FormData.Txt_LB_Amt
            Dim Txt_CashAmt As String = input_Param.FormData.Txt_CashAmt
            Dim Txt_V_Date As String = input_Param.FormData.Txt_V_Date
            Dim Txt_V_NO As String = input_Param.FormData.Txt_V_NO
            Dim Txt_Inv_No As String = input_Param.FormData.Txt_Inv_No
            Dim Txt_Inv_Date As String = input_Param.FormData.Txt_Inv_Date
            Dim Txt_SubTotal As String = input_Param.FormData.Txt_SubTotal
            Dim Txt_BankAmt As String = input_Param.FormData.Txt_BankAmt
            Dim Txt_TDS_Amt As String = input_Param.FormData.Txt_TDS_Amt
            Dim Txt_DueDate As String = input_Param.FormData.Txt_DueDate
            Dim Txt_Narration As String = input_Param.FormData.Txt_Narration
            Dim Txt_Reference As String = input_Param.FormData.Txt_Reference

            Dim GLookUp_PartyList1_Tag As String = input_Param.FormData.GLookUp_PartyList1_Tag 'Me.GLookUp_PartyList1.Text, 'Me.GLookUp_PartyList1.Tag
            Dim GLookUp_PartyList1_Txt As String = input_Param.FormData.GLookUp_PartyList1_Txt 'Me.GLookUp_PartyList1.Text, 'Me.GLookUp_PartyList1.Tag
            Dim oldREC_EDIT_ON As DateTime = input_Param.FormData.oldREC_EDIT_ON 'Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
            Dim GridView1 As DataTable = IIf(input_Param.FormData.GridView1 Is Nothing, New DataTable, input_Param.FormData.GridView1)
            Dim DTGridView2 As DataTable = IIf(input_Param.FormData.DTGridView2 Is Nothing, New DataTable, input_Param.FormData.DTGridView2) 'Me.GridView2
            Dim GridView3 As DataTable = IIf(input_Param.FormData.GridView3 Is Nothing, New DataTable, input_Param.FormData.GridView3)
            Dim GridView4 As DataTable = IIf(input_Param.FormData.GridView4 Is Nothing, New DataTable, input_Param.FormData.GridView4)
            Dim IsChk_Incompleted As Boolean = input_Param.FormData.IsChk_Incompleted 'Chk_Incompleted.Checked
            Dim xID As String = input_Param.FormData.xID 'Me.xID.Text
            Dim TitleX As String = input_Param.FormData.TitleX 'Me.TitleX.Text
            Dim WindowText As String = input_Param.FormData.WindowText 'Me.Text

            'Form's global variables
            Dim Bank_Detail As DataTable = IIf(input_Param.GlobalData.Bank_Detail Is Nothing, New DataTable(), input_Param.GlobalData.Bank_Detail)
            Dim LastEditedOn As DateTime = input_Param.GlobalData.LastEditedOn
            Dim DT As DataTable = IIf(input_Param.GlobalData.DT Is Nothing, New DataTable(), input_Param.GlobalData.DT)
            Dim LB_EXTENDED_PROPERTY_TABLE As DataTable = input_Param.GlobalData.LB_EXTENDED_PROPERTY_TABLE
            Dim LB_DOCS_ARRAY As DataTable = input_Param.GlobalData.LB_DOCS_ARRAY
            Dim iParty_Req As Boolean = input_Param.GlobalData.iParty_Req 'private


            '_BasicParam = Basic_Param
            'Basic_Param.screen = ClientScreen.Accounts_Voucher_Payment
            Dim SaveButtonChecksParam As New Param_SaveButtonChecks()

            Dim _Date_Format_DD_MMM_YYYY As String = "dd-MMM-yyyy"


            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._New Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                For Each XRow In DT.Rows
                    If XRow("Item_Profile") = "LAND & BUILDING" Or XRow("Item_Profile") = "OTHER ASSETS" Then
                        If IsInsuranceAudited Then
                            SaveButtonChecksParam.messageBoxText = "Insurance Related Assets Cannot be Added/Edited After The Completion of Insurance Audit"
                            SaveButtonChecksParam.messageCaption = "Information..."
                            SaveButtonChecksParam.messageIcon = "Information"
                            SaveButtonChecksParam.dialogResult = "None"
                            Return SaveButtonChecksParam
                        End If
                    End If

                    If XRow("ITEM_VOUCHER_TYPE").Trim.ToUpper = "LAND & BUILDING" And Not XRow("Item_Profile").ToUpper = "LAND & BUILDING" Then ' L&B Expense Item
                        If IsInsuranceAudited Then
                            SaveButtonChecksParam.messageBoxText = "Property Related Expenses Cannot be Added/Edited After The Completion of Insurance Audit"
                            SaveButtonChecksParam.messageCaption = "Information..."
                            SaveButtonChecksParam.messageIcon = "Information"
                            SaveButtonChecksParam.dialogResult = "None"
                            Return SaveButtonChecksParam
                        End If
                    End If
                Next
            End If

            '-----------------------------+
            'Start : Check if entry already changed 
            '-----------------------------+

            If IsMultiUserAllowed Then
                '  Basic_Param.screen = ClientScreen.Accounts_Vouchers
                If Not Bank_Detail Is Nothing Then

                    For Each XRow In Bank_Detail.Rows
                        Dim inBankParam As New Param_Bank_GetValue_Common
                        inBankParam.CR_LED_ID = XRow("ID")
                        inBankParam.DR_LED_ID = ""
                        Dim AccNo As String = cBase._BankAccountDBOps.GetValue(ClientScreen.Accounts_Vouchers, inBankParam)
                        If AccNo Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        If AccNo.Length > 0 Then
                            SaveButtonChecksParam.messageBoxText = "Entry cannot be Added/Edited/Deleted...!<br><br>In this entry, Used Bank A/c No.: " & AccNo & " was closed...!!!"
                            SaveButtonChecksParam.messageCaption = "Information..."
                            SaveButtonChecksParam.messageIcon = "Information"
                            SaveButtonChecksParam.dialogResult = "Retry"
                            Return SaveButtonChecksParam
                        End If
                    Next
                End If
                ' Basic_Param.screen = ClientScreen.Accounts_Voucher_Payment

                If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._Delete Then
                    'Dim payment_DbOps As DataTable = Base._Payment_DBOps.GetRecord(Me.xMID.Text)

                    Dim payment_DbOps As DataTable = Payment_GetRecordByColumn(Tables.TRANSACTION_INFO, "TR_M_ID", Rec_ID, "TR_SR_NO", "1")

                    If payment_DbOps Is Nothing Then
                        Throw New Exception("|SomeError happened during current operation!!|")
                        Exit Function
                    End If
                    If payment_DbOps.Rows.Count = 0 Then
                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.RecordChanged("Current Payment")
                        SaveButtonChecksParam.messageCaption = "Record Already Changed!!"
                        SaveButtonChecksParam.messageIcon = "Exclamation"
                        SaveButtonChecksParam.dialogResult = "Retry"
                        Return SaveButtonChecksParam
                    End If

                    If LastEditedOn <> Convert.ToDateTime(payment_DbOps.Rows(0)("REC_EDIT_ON")) Then
                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.RecordChanged("Current Payment")
                        SaveButtonChecksParam.messageCaption = "Record Already Changed!!"
                        SaveButtonChecksParam.messageIcon = "Exclamation"
                        SaveButtonChecksParam.dialogResult = "Retry"
                        Return SaveButtonChecksParam
                    End If
                    'CHECKING LOCK STATUS

                    Dim MaxValue As Object = 0
                    MaxValue = cBase._Payment_DBOps.GetStatusByMID(Rec_ID)
                    'MaxValue = Payment_GetRecordStatus(Rec_ID, Tables.TRANSACTION_INFO, "TR_CEN_ID", "TR_M_ID")
                    If MaxValue Is Nothing Then
                        SaveButtonChecksParam.messageBoxText = "Entry Not Found...!"
                        SaveButtonChecksParam.messageCaption = "Information..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        Return SaveButtonChecksParam
                    End If
                    If MaxValue = Common_Lib.Common.Record_Status._Locked Then
                        SaveButtonChecksParam.messageBoxText = "Locked Entry cannot be Edited/Deleted...!<br><br>Note:<br>-------<br>Drop your Request to Madhuban for Unlock this Entry,<br>If you really want to do some action...!"
                        SaveButtonChecksParam.messageCaption = "Information..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "Retry"
                        Return SaveButtonChecksParam
                    End If

                    'End If


                    If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Delete Then
                        'Special Checks
                        '  Dim BasicParam_1 As Basic_Param = Basic_Param.Clone()
                        'BasicParam_1.screen = ClientScreen.Accounts_Vouchers
                        Dim paramJornalEntry As Parameter_GetJornalEntryAdjustments = New Parameter_GetJornalEntryAdjustments()

                        '#################################

                        'Liabilities Raised
                        Dim xTemp_LiabID As String = ""
                        'Get Liab creted by current Txn
                        'Dim xTemp_LiabID As String = Base._Voucher_DBOps.GetRaisedLiabilityRecID(Me.xMID.Text) 'Get Liab creted by current Txn
                        Dim dTable As DataTable = cBase._LiabilityDBOps.GetList(ClientScreen.Accounts_Vouchers, Rec_ID)
                        ' Liabilities.GetList_Common(BasicParam_1, Rec_ID)
                        If Not dTable Is Nothing Then
                            If dTable.Rows.Count > 0 Then
                                xTemp_LiabID = dTable.Rows(0)(0).ToString()
                            End If
                        End If
                        If xTemp_LiabID Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        If xTemp_LiabID.Length > 0 Then
                            'Payment has been made againt the liability raised
                            Dim txnReferLiab As DataTable = cBase._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_LiabID)
                            'Dim txnReferLiab As DataTable = Vouchers.GetPaymentReferenceRecord(xTemp_LiabID, BasicParam_1)
                            If Not txnReferLiab Is Nothing Then
                                If txnReferLiab.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry ! Some payment has been made against the liability raised in current transaction on " & Convert.ToDateTime(txnReferLiab.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferLiab.Rows(0)("TR_AMOUNT").ToString() & ".<br><br>Please delete the record for deleting this Entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                End If
                            End If

                            'Deposits Raised
                            paramJornalEntry.CrossRefId = xTemp_LiabID
                            paramJornalEntry.Excluded_Rec_M_ID = Rec_ID
                            paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                            paramJornalEntry.NextUnauditedYearID = next_Unaudited_YearID
                            'Vasu need to check paramJornalEntry.YearID

                            txnReferLiab = cBase._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
                            'txnReferLiab = Voucher_Journal.GetJornalEntryAdjustments(paramJornalEntry, BasicParam_1)
                            If Not txnReferLiab Is Nothing Then
                                If txnReferLiab.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry! Liability created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                End If
                            End If
                        End If


                        'Deposits Raised
                        'Get Liab creted by current Txn
                        Dim xTemp_DepID As String = ""
                        'Dim ParamDeposits As Param_Deposits_GetListCommon = New Param_Deposits_GetListCommon()
                        'ParamDeposits.TxnMID = Rec_ID
                        xTemp_DepID = cBase._Voucher_DBOps.GetRaisedDepositRecID(Rec_ID) 'Get Liab creted by current Txn
                        'Dim dTableDeposits As DataTable = Deposits.GetList_Common(BasicParam_1, ParamDeposits)
                        'If Not dTableDeposits Is Nothing Then
                        '    If dTableDeposits.Rows.Count > 0 Then
                        '        xTemp_DepID = dTableDeposits.Rows(0)(0).ToString()
                        '    End If
                        'End If
                        If xTemp_DepID Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        If xTemp_DepID.Length > 0 Then
                            'Adjustments/ Refund has been made againt the deposit raised
                            Dim txnReferDeposits As DataTable = cBase._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_DepID) 'Adjustments/ Refund has been made againt the deposit raised
                            'Dim txnReferDeposits As DataTable = Vouchers.GetPaymentReferenceRecord(xTemp_DepID, BasicParam_1)
                            If Not txnReferDeposits Is Nothing Then
                                If txnReferDeposits.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry ! Some adjustment / refund has been made against the deposit raised in current transaction on " & Convert.ToDateTime(txnReferDeposits.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferDeposits.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for deleting this Entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                End If
                            End If

                            paramJornalEntry.CrossRefId = xTemp_DepID
                            paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                            paramJornalEntry.Excluded_Rec_M_ID = Rec_ID
                            paramJornalEntry.NextUnauditedYearID = next_Unaudited_YearID
                            'vasu need to check paramJornalEntry.YearID
                            txnReferDeposits = cBase._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers)
                            'txnReferDeposits = Voucher_Journal.GetJornalEntryAdjustments(paramJornalEntry, BasicParam_1)
                            If Not txnReferDeposits Is Nothing Then
                                If txnReferDeposits.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry! Deposit created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                End If
                            End If
                        End If

                        'Advance Raised
                        'Get Advances creted by current Txn
                        Dim xTemp_AdvID As String = ""
                        'Dim paramAdvances_GetList As Param_Advances_GetList_Common = New Param_Advances_GetList_Common()
                        'paramAdvances_GetList.TxnMID = Rec_ID
                        xTemp_AdvID = cBase._Voucher_DBOps.GetRaisedAdvanceRecID(Rec_ID) 'Get Advances creted by current Txn
                        'Dim dTableAdvances As DataTable = Advances.GetList_Common(BasicParam_1, paramAdvances_GetList)
                        'If Not dTableAdvances Is Nothing Then
                        '    If dTableAdvances.Rows.Count > 0 Then
                        '        xTemp_AdvID = dTableAdvances.Rows(0)(0).ToString()
                        '    End If
                        'End If
                        If xTemp_AdvID Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        'Adjustments/ Refund has been made againt the Advance raised
                        If xTemp_AdvID.Length > 0 Then
                            Dim txnReferAdvance As DataTable = cBase._Voucher_DBOps.GetPaymentReferenceRecord(xTemp_AdvID) 'Adjustments/ Refund has been made againt the Advance raised
                            'Dim txnReferAdvance As DataTable = Vouchers.GetPaymentReferenceRecord(xTemp_AdvID, BasicParam_1)
                            If Not txnReferAdvance Is Nothing Then
                                If txnReferAdvance.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry ! Some adjustment / refund has been made against the advance raised in current transaction on " & Convert.ToDateTime(txnReferAdvance.Rows(0)("TR_DATE")).ToLongDateString() & " for Rs." & txnReferAdvance.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for deleting this Entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                End If
                            End If

                            paramJornalEntry.CrossRefId = xTemp_AdvID
                            paramJornalEntry.SpecifiedEntryType = Common_Lib.RealTimeService.EntryType.Both
                            paramJornalEntry.Excluded_Rec_M_ID = Rec_ID
                            paramJornalEntry.NextUnauditedYearID = next_Unaudited_YearID
                            'vasu need to check paramJornalEntry.YearID
                            txnReferAdvance = cBase._Journal_voucher_DBOps.GetJornalEntryAdjustments(paramJornalEntry, ClientScreen.Accounts_Voucher_Payment)
                            'txnReferAdvance = Voucher_Journal.GetJornalEntryAdjustments(paramJornalEntry, BasicParam_1)
                            If Not txnReferAdvance Is Nothing Then
                                If txnReferAdvance.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry! Advance created by current journal entry is used in some other entry...!<br><br>Please delete that dependency to delete this entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                        End If


                        'Get Actual Item IDs from Selected Transaction
                        'Base._Voucher_DBOps.GetAssetItemID(Me.xMID.Text).Rows()
                        Dim assetItems As DataTable = cBase._Voucher_DBOps.GetAssetItemID(Rec_ID)
                        'Vouchers.GetAssetItemID(Rec_ID, BasicParam_1)
                        For Each cRow As DataRow In assetItems.Rows
                            Dim xTemp_ItemID As String = cRow(0).ToString()

                            'Dim paramGetItems As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
                            'paramGetItems.ItemIDs = xTemp_ItemID
                            'paramGetItems.currInsttID = open_Ins_ID
                            'Base._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID)
                            Dim ProfileTable As DataTable = cBase._Voucher_DBOps.GetItemProfileRecord(xTemp_ItemID)
                            'CoreFunctions.GetItemsByQuery_Common(BasicParam_1, paramGetItems) 'Gets Asset Profile

                            Dim xTemp_AssetProfile As String = ProfileTable.Rows(0)("ITEM_PROFILE").ToString()
                            If xTemp_AssetProfile.ToUpper <> "NOT APPLICABLE" Then ' Leaving Constuction Items
                                Dim xTemp_AssetID As String = ""
                                Dim isProperty As Boolean = False
                                'Dim ParamAssetRecID As Param_Vouchers_GetAssetRecID = New Param_Vouchers_GetAssetRecID()
                                Select Case xTemp_AssetProfile ' Get Asset RecID from Particular Table 
                                    Case "GOLD", "SILVER"
                                        'ParamAssetRecID.TableName = Tables.GOLD_SILVER_INFO
                                        'ParamAssetRecID.TxnMID = Rec_ID
                                        'Base._Voucher_DBOps.GetAssetRecID
                                        Dim value As Object = cBase._Voucher_DBOps.GetAssetRecID(Tables.GOLD_SILVER_INFO, Rec_ID)
                                        'Vouchers.GetAssetRecID(ParamAssetRecID, BasicParam_1)
                                        If Not value Is Nothing Then
                                            xTemp_AssetID = value.ToString()
                                        End If
                                    Case "OTHER ASSETS"
                                        'ParamAssetRecID.TableName = Tables.ASSET_INFO
                                        'ParamAssetRecID.TxnMID = Rec_ID
                                        Dim value As Object = cBase._Voucher_DBOps.GetAssetRecID(Tables.ASSET_INFO, Rec_ID)
                                        'Vouchers.GetAssetRecID(ParamAssetRecID, BasicParam_1)
                                        If Not value Is Nothing Then
                                            xTemp_AssetID = value.ToString()
                                        End If
                                    Case "LIVESTOCK"
                                        'ParamAssetRecID.TableName = Tables.LIVE_STOCK_INFO
                                        'ParamAssetRecID.TxnMID = Rec_ID
                                        Dim value As Object = cBase._Voucher_DBOps.GetAssetRecID(Tables.LIVE_STOCK_INFO, Rec_ID)
                                        'Vouchers.GetAssetRecID(ParamAssetRecID, BasicParam_1)
                                        If Not value Is Nothing Then
                                            xTemp_AssetID = value.ToString()
                                        End If
                                    Case "VEHICLES"
                                        'ParamAssetRecID.TableName = Tables.VEHICLES_INFO
                                        'ParamAssetRecID.TxnMID = Rec_ID
                                        Dim value As Object = cBase._Voucher_DBOps.GetAssetRecID(Tables.VEHICLES_INFO, Rec_ID)
                                        'Vouchers.GetAssetRecID(ParamAssetRecID, BasicParam_1)
                                        If Not value Is Nothing Then
                                            xTemp_AssetID = value.ToString()
                                        End If
                                    Case "LAND & BUILDING"
                                        'ParamAssetRecID.TableName = Tables.LAND_BUILDING_INFO
                                        'ParamAssetRecID.TxnMID = Rec_ID
                                        Dim value As Object = cBase._Voucher_DBOps.GetAssetRecID(Tables.LAND_BUILDING_INFO, Rec_ID)
                                        'Vouchers.GetAssetRecID(ParamAssetRecID, BasicParam_1)
                                        If Not value Is Nothing Then
                                            xTemp_AssetID = value.ToString()
                                        End If
                                        isProperty = True
                                End Select


                                If xTemp_AssetID.Length > 0 Then
                                    'Dim ParamSaleReference As Param_GetSaleReferenceRecord = New Param_GetSaleReferenceRecord
                                    'ParamSaleReference.RefID = xTemp_AssetID
                                    'ParamSaleReference.Exclude_PrevYears = False
                                    'Base._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID)
                                    Dim SaleRecord As DataTable = cBase._Voucher_DBOps.GetSaleReferenceRecord(xTemp_AssetID, False)
                                    'Vouchers.GetSaleReferenceRecord(ParamSaleReference, BasicParam_1)
                                    If Not SaleRecord Is Nothing Then
                                        If SaleRecord.Rows.Count > 0 Then
                                            SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry contains a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for completing this Entry."
                                            SaveButtonChecksParam.messageCaption = "Error!!"
                                            SaveButtonChecksParam.messageIcon = "Exclamation"
                                            SaveButtonChecksParam.dialogResult = "Retry"
                                            Return SaveButtonChecksParam
                                        End If
                                    End If

                                    'Gets any Txn where Current Asset is referenced, mostly in case of L&B
                                    'Base._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                    Dim ReferenceRecord As DataTable = cBase._Voucher_DBOps.GetReferenceTxnRecord(xTemp_AssetID)
                                    'Vouchers.GetReferenceTxnRecord(xTemp_AssetID, BasicParam_1)
                                    If Not ReferenceRecord Is Nothing Then
                                        If ReferenceRecord.Rows.Count > 0 Then
                                            SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry contains a asset which was referred in a Dependent Entry Dated " & Convert.ToDateTime(ReferenceRecord.Rows(0)("TR_DATE")).ToLongDateString() & " of Rs." & ReferenceRecord.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for completing this Entry."
                                            SaveButtonChecksParam.messageCaption = "Item selected has been used by some other user...."
                                            SaveButtonChecksParam.messageIcon = "Exclamation"
                                            SaveButtonChecksParam.dialogResult = "Retry"
                                            Return SaveButtonChecksParam
                                        End If
                                    End If

                                    'Dim paramAssetTransfers As Param_GetAssetTransfers = New Param_GetAssetTransfers
                                    'paramAssetTransfers.refAssetID = xTemp_AssetID
                                    'paramAssetTransfers.YearID = Nothing
                                    'paramAssetTransfers.Exclude_PrevYears = False
                                    'Base._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, "", xTemp_AssetID)
                                    Dim AssetTrfRecord As DataTable = cBase._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, xTemp_AssetID)
                                    'Voucher_AssetTransfer.GetAssetTransfers(paramAssetTransfers, BasicParam_1)
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        If AssetTrfRecord.Rows.Count > 0 Then
                                            SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry contains a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & ".<br><br> Please delete the record for editing this Entry."
                                            SaveButtonChecksParam.messageCaption = "Error!!"
                                            SaveButtonChecksParam.messageIcon = "Exclamation"
                                            SaveButtonChecksParam.dialogResult = "Retry"
                                            Return SaveButtonChecksParam
                                        End If
                                    End If
                                End If
                            Else 'Contt Items 
                                'checks if the referred property for constt items has been sold 
                                Dim ParamSaleReference As Param_GetSaleReferenceRecord = New Param_GetSaleReferenceRecord
                                ParamSaleReference.RefID = Rec_ID
                                ParamSaleReference.Exclude_PrevYears = False
                                Dim SaleRecord As DataTable = cBase._Voucher_DBOps.GetSaleReferenceRecord(cBase._Voucher_DBOps.GetReferenceRecordID(Rec_ID)) 'checks if the referred property for constt items has been sold 
                                'Dim SaleRecord As DataTable = Vouchers.GetSaleReferenceRecord(ParamSaleReference, BasicParam_1)
                                If Not SaleRecord Is Nothing Then
                                    If SaleRecord.Rows.Count > 0 Then
                                        SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for completing this Entry."
                                        SaveButtonChecksParam.messageCaption = "Error!!"
                                        SaveButtonChecksParam.messageIcon = "Exclamation"
                                        SaveButtonChecksParam.dialogResult = "Retry"
                                        Return SaveButtonChecksParam
                                        Exit Function
                                    End If
                                End If

                                'checks if the referred property for constt items has been transferred 
                                Dim Asset_ID As String = ""
                                Dim value As Object = cBase._Voucher_DBOps.GetReferenceRecordID(Rec_ID)
                                'Vouchers.GetReferenceRecordID(Rec_ID, BasicParam_1)
                                If Not value Is Nothing Then
                                    Asset_ID = value.ToString()
                                End If

                                'Dim paramAssetTransfers As Param_GetAssetTransfers = New Param_GetAssetTransfers
                                'paramAssetTransfers.refAssetID = Asset_ID
                                'paramAssetTransfers.YearID = Nothing
                                'paramAssetTransfers.Exclude_PrevYears = False
                                Dim AssetTrfRecord As DataTable = cBase._AssetTransfer_DBOps.GetAssetTransfers(Common_Lib.RealTimeService.ClientScreen.Accounts_Vouchers, Nothing, Asset_ID) 'checks if the referred property for constt items has been transferred 
                                'Dim AssetTrfRecord As DataTable = Voucher_AssetTransfer.GetAssetTransfers(paramAssetTransfers, BasicParam_1)

                                If AssetTrfRecord Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                If AssetTrfRecord.Rows.Count > 0 Then
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & ".<br><br> Please delete the record for editing this Entry."
                                        SaveButtonChecksParam.messageCaption = "Error!!"
                                        SaveButtonChecksParam.messageIcon = "Exclamation"
                                        SaveButtonChecksParam.dialogResult = "Retry"
                                        Return SaveButtonChecksParam
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            '#################################
            '-----------------------------+
            'End : Check if entry already changed 
            '-----------------------------+
            '  Dim BasicParam_2 As Basic_Param = Basic_Param.Clone()
            ';BasicParam_2.screen = ClientScreen.Accounts_Vouchers
            ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
            If NavMode = Common_Lib.Common.Navigation_Mode._Delete Then '  Or NavMode = Common_Lib.Common.Navigation_Mode._Edit ' Removed as no longer there will be re-creation of location on updation of property voucher 
                'Properties Created in Current Voucher
                Dim d1 As DataTable = cBase._L_B_DBOps.GetIDsBytxnID(Rec_ID, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment)
                'Dim d1 As DataTable = LandAndBuilding.GetIDsBytxnID(Rec_ID, BasicParam_2)
                If d1 Is Nothing Then
                    Throw New Exception("|SomeError happened during current operation!!|")
                    Exit Function
                End If
                For Each cRow As DataRow In d1.Rows
                    Dim Msg As String = FindLocationUsage(cRow(0), open_UID_No, NavMode, False) 'dont exclude sol/tf assets from location check
                    If Msg.Length > 0 Then
                        SaveButtonChecksParam.messageBoxText = Msg
                        SaveButtonChecksParam.messageCaption = WindowText
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                Next
            End If

            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._New Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then

                If Txt_Inv_No.Trim.Length > 0 Then
                    If Get_Inv_No_Count(GLookUp_PartyList1_Tag, Txt_Inv_No.Trim, Rec_ID) > 0 Then
                        SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                        SaveButtonChecksParam.ToolTipText = "Invoice No./Bill No. already used for same party. . . !"
                        SaveButtonChecksParam.ToolTipWindow = "Txt_Inv_No"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "Txt_Inv_No"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                Else
                    ' Me.ToolTip1.Hide(Me.Txt_CashAmt)
                End If

                If Val(Txt_CashAmt) < 0 Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Cash Can Not be Negative...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_CashAmt"
                    SaveButtonChecksParam.dialogResult = "None"
                    SaveButtonChecksParam.focusControlId = "Txt_CashAmt"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    ' Me.ToolTip1.Hide(Me.Txt_CashAmt)
                End If

                If Val(Txt_CreditAmt) < 0 Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Credit Can Not be Negative...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_CreditAmt"
                    SaveButtonChecksParam.dialogResult = "None"
                    SaveButtonChecksParam.focusControlId = "Txt_CreditAmt"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    ' Me.ToolTip1.Hide(Me.Txt_CreditAmt)
                End If
                ''START THE BELOW CODE IS HANDLED IN CLIENT SIDE
                'Need to check
                'If Val(Txt_CashAmt) > 20000 Then
                '    Dim xPromptWindow As New Common_Lib.Prompt_Window
                '    If DialogResult.No = xPromptWindow.ShowDialog("Message...", "<size=13><b>Cash Payment more than <color=red>Rs. 20,000</color> is not allowed...!</b></size>" & vbNewLine & vbNewLine & "Do you still want to Save...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._499x180, Common_Lib.Prompt_Window.FocusButton._Button1, Color.AliceBlue) Then
                '        xPromptWindow.Dispose()
                '        Me.Txt_CashAmt.Focus()
                '        Me.DialogResult = Windows.Forms.DialogResult.None
                '        Exit Function
                '    Else
                '        xPromptWindow.Dispose()
                '    End If
                'End If
                '' END THE BELOW CODE IS HANDLED IN CLIENT SIDE
                'Check Unique Entries..............................................................\
                Dim UniqueItemProfile As DataTable = New DataTable()
                If DT.Rows.Count > 0 Then
                    UniqueItemProfile = DT.DefaultView.ToTable(True, "Item_Profile")
                End If
                Dim CNT_DEP As Integer = 0 : Dim CNT_ADV As Integer = 0 : Dim CNT_NA As Integer = 0 : Dim CNT_LB As Integer = 0 : Dim CNT_OTHER As Integer = 0 : Dim xProfile As String = "" : Dim EntryAllowed As Boolean = True : Dim Deposit_Found As Boolean = False : Dim Payable_Found As Boolean = False : Dim Advances_Found As Boolean = False
                For Each XRow In UniqueItemProfile.Rows
                    If XRow("Item_Profile").ToString.ToUpper = "ADVANCES" Then : CNT_ADV += 1 : xProfile += XRow("Item_Profile").ToString.ToUpper & ", " : Advances_Found = True
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "OTHER LIABILITIES" Then : CNT_LB += 1 : xProfile += XRow("Item_Profile").ToString.ToUpper & ", " : Payable_Found = True
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "OTHER DEPOSITS" Then : CNT_DEP += 1 : xProfile += XRow("Item_Profile").ToString.ToUpper & ", " : Deposit_Found = True
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "GOLD" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "SILVER" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "LIVE STOCK" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "VEHICLES" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "OTHER ASSETS" Then : CNT_OTHER += 1
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "TELEPHONE BILL" Then
                    ElseIf XRow("Item_Profile").ToString.ToUpper = "NOT APPLICABLE" Then : CNT_NA += 1
                    Else : End If
                Next
                If CNT_OTHER > 0 Then xProfile += "OTHERS, "
                If xProfile.Trim.Length > 0 Then xProfile = IIf(xProfile.Trim.EndsWith(","), Mid(xProfile.Trim.ToString, 1, xProfile.Trim.Length - 1), xProfile.Trim.ToString)

                If (CNT_DEP + CNT_ADV + CNT_LB + CNT_OTHER) >= 4 Then : EntryAllowed = False
                ElseIf (CNT_DEP + CNT_ADV + CNT_LB) >= 3 Then : EntryAllowed = False
                ElseIf (CNT_DEP + CNT_ADV) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_ADV + CNT_LB + CNT_OTHER) >= 3 Then : EntryAllowed = False
                ElseIf (CNT_ADV + CNT_LB) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_ADV + CNT_OTHER) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_LB + CNT_OTHER) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_DEP + CNT_LB) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_LB + CNT_OTHER) >= 2 Then : EntryAllowed = False
                ElseIf (CNT_DEP + CNT_OTHER) >= 2 Then : EntryAllowed = False
                End If
                If EntryAllowed = False Then
                    SaveButtonChecksParam.messageBoxText = "Multiple Profile based Items not allowed...!<br><br>LIKE :  " & xProfile & " etc.<br><br>Note: Make separate Entries...!"
                    SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                    SaveButtonChecksParam.messageIcon = "Information"
                    SaveButtonChecksParam.dialogResult = "None"
                    SaveButtonChecksParam.focusControlId = "GridView1"
                    Return SaveButtonChecksParam
                    Exit Function
                End If
                '................................................................................../

                If Deposit_Found And EntryAllowed Then
                    If Val(Txt_AdvAmt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Advance Payment not allowed with Other Deposit Payment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    If Val(Txt_CreditAmt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Credit Payment not allowed with Other Deposit Payment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    If Val(Txt_LB_Amt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Payable Adjustment not allowed with Other Deposit Payment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                End If
                If Advances_Found And EntryAllowed Then
                    If Val(Txt_AdvAmt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Advance Payment not allowed with Advances Adjustment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    If Val(Txt_CreditAmt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Credit Payment not allowed with Advances Payment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    If Val(Txt_LB_Amt) > 0 Then
                        SaveButtonChecksParam.messageBoxText = "Payable Adjustment not allowed with Advances Payment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "GridView1"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                End If
                If Payable_Found Then
                    If Val(Txt_AdvAmt) + Val(Txt_CreditAmt) <> 0 Then
                        SaveButtonChecksParam.messageBoxText = "Advance Adjustment / Credit Amount not allowed with Payable Adjustment...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        SaveButtonChecksParam.focusControlId = "Txt_CreditAmt"
                        ' SaveButtonChecksParam.SelectedTabPage = "Tab_Page_Advance"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    'http://pm.bkinfo.in/issues/4348#note-6 pt.2
                    'If Val(Me.Txt_LB_Amt.Text) <> (Val(Me.Txt_SubTotal.Text)) Or _
                    '    Val(Me.Txt_LB_Amt.Text) <> (Val(Me.Txt_CashAmt.Text) + Val(Me.Txt_BankAmt.Text) + Val(Me.Txt_AdvAmt.Text) + Val(Me.Txt_CreditAmt.Text)) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show("Total Amount / Payment not matching with Payable Adjustment...!", "Payment Voucher...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    XtraTabControl1.SelectedTabPage = Tab_Page_Liabilities
                    '    Me.Txt_CashAmt.Focus()
                    '    Me.DialogResult = Windows.Forms.DialogResult.None
                    '    Exit Sub
                    'End If
                Else
                    If Val(Txt_LB_Amt) > 0 Then
                        'If Val(Me.Txt_AdvAmt.Text) + Val(Me.Txt_CreditAmt.Text) <> 0 Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show("Advance Adjustment / Credit Amount not allowed with Payable Adjustment...!", "Payment Voucher...", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    XtraTabControl1.SelectedTabPage = Tab_Page_Advance
                        '    Me.Txt_CreditAmt.Focus()
                        '    Me.DialogResult = Windows.Forms.DialogResult.None
                        '    Exit Sub
                        'End If

                        SaveButtonChecksParam.messageBoxText = "Payable Adjustment not allowed with Other Types of Item(s)...!"
                        SaveButtonChecksParam.messageCaption = "Payment Voucher..."
                        SaveButtonChecksParam.messageIcon = "Information"
                        SaveButtonChecksParam.dialogResult = "None"
                        'SaveButtonChecksParam.SelectedTabPage = "Tab_Page_Liabilities"
                        Return SaveButtonChecksParam
                        'XtraTabControl1.SelectedTabPage = Tab_Page_Liabilities
                        Exit Function
                    End If
                End If

                If iParty_Req Or (Not GridView3 Is Nothing AndAlso GridView3.Rows.Count > 0) Or Val(Txt_CreditAmt) > 0 Then
                    'Me.lbl_PartyName.ForeColor = Color.Red ' Need to check
                    'If Len(Trim(Me.GLookUp_PartyList1.Tag)) = 0 Or Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then
                    If Len(Trim(GLookUp_PartyList1_Tag)) = 0 Or Len(Trim(GLookUp_PartyList1_Txt)) = 0 Then
                        SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                        SaveButtonChecksParam.ToolTipText = "Party Not Selected...!"
                        SaveButtonChecksParam.ToolTipWindow = "GLookUp_PartyList1_Tag"
                        SaveButtonChecksParam.focusControlId = "GLookUp_PartyList1_Tag"
                        SaveButtonChecksParam.dialogResult = "None"
                        Return SaveButtonChecksParam
                        Exit Function
                    Else
                        ' Me.ToolTip1.Hide(Me.GLookUp_PartyList1)
                    End If
                Else ' Need to check
                    'Needs to be handled in client
                    'Me.lbl_PartyName.ForeColor = Color.Black
                    'If Len(Trim(Me.GLookUp_PartyList1.Text)) = 0 Then Me.GLookUp_PartyList1.Tag = ""
                End If

                If IsDate(Txt_V_Date) = False Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Date Incorrect/Blank...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                    SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                    SaveButtonChecksParam.dialogResult = "None"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    'Me.ToolTip1.Hide(Me.Txt_V_Date)
                End If
                If IsDate(Txt_V_Date) = True Then
                    '1
                    Dim diff As Double = DateDiff(DateInterval.Day, open_Year_Sdt, DateValue(Txt_V_Date))
                    If diff < 0 Then
                        SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                        SaveButtonChecksParam.ToolTipText = "Date not as per Financial Year...!"
                        SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                        SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                        SaveButtonChecksParam.dialogResult = "None"
                        Return SaveButtonChecksParam
                        Exit Function
                    Else
                        'Me.ToolTip1.Hide(Me.Txt_V_Date)
                    End If
                    '2
                    diff = DateDiff(DateInterval.Day, open_Year_Edt, DateValue(Txt_V_Date))
                    If diff > 0 Then
                        SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                        SaveButtonChecksParam.ToolTipText = "Date not as per Financial Year...!"
                        SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                        SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                        SaveButtonChecksParam.dialogResult = "None"
                        Return SaveButtonChecksParam
                        Exit Function
                    Else
                        'Me.ToolTip1.Hide(Me.Txt_V_Date)
                    End If
                End If


                If Val(Trim(Txt_CashAmt)) < 0 Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Amount cannot be Zero/Negative...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_CashAmt"
                    SaveButtonChecksParam.focusControlId = "Txt_CashAmt"
                    SaveButtonChecksParam.dialogResult = "None"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    ' Me.ToolTip1.Hide(Me.Txt_CashAmt)
                End If

                If GridView1.Rows.Count <= 0 Then
                    SaveButtonChecksParam.messageBoxText = "Item Detail Not Specified...!"
                    SaveButtonChecksParam.messageCaption = WindowText
                    SaveButtonChecksParam.messageIcon = "Information"
                    SaveButtonChecksParam.dialogResult = "None"
                    SaveButtonChecksParam.focusControlId = "GridView1"
                    Return SaveButtonChecksParam
                    Exit Function
                End If

                If Val(Trim(Txt_SubTotal)) <= 0 Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Amount cannot be Zero...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_SubTotal"
                    SaveButtonChecksParam.focusControlId = "Txt_SubTotal" 'redmine Bug #133107 fixed
                    SaveButtonChecksParam.dialogResult = "None"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    'Me.ToolTip1.Hide(Me.Txt_SubTotal)
                End If

            End If

            'check of Locations 
            For Each XRow In DT.Rows
                If XRow("Item_Profile") = "LAND & BUILDING" Then
                    For Each cRow In DT.Rows
                        'If XRow("LB_PRO_NAME") = cRow("LB_PRO_NAME") And XRow("LB_REC_ID") <> cRow("LB_REC_ID") Then
                        If IIf(IsDBNull(XRow("LB_PRO_NAME")), "", XRow("LB_PRO_NAME")) = IIf(IsDBNull(cRow("LB_PRO_NAME")), "", cRow("LB_PRO_NAME")) And IIf(IsDBNull(XRow("LB_REC_ID")), "", XRow("LB_REC_ID")) <> IIf(IsDBNull(cRow("LB_REC_ID")), "", cRow("LB_REC_ID")) Then
                            'DevExpress.XtraEditors.XtraMessageBox.Show("", "Property Name Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            'Me.DialogResult = Windows.Forms.DialogResult.Retry
                            'FormClosingEnable = False : Me.Close()
                            SaveButtonChecksParam.messageBoxText = "Property/Location With Same Name Already Available in same voucher...!"
                            SaveButtonChecksParam.messageCaption = "Property Name Duplicate"
                            SaveButtonChecksParam.messageIcon = "Exclamation"
                            SaveButtonChecksParam.dialogResult = "None"
                            Return SaveButtonChecksParam
                            Exit Function
                        End If
                    Next

                    If NavMode = Common_Lib.Common.Navigation_Mode._New Or NavMode = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                        'BasicParam_2.screen = ClientScreen.Profile_LandAndBuilding
                        Dim MaxValue_Loc As Object = 0
                        Dim paramRecordCount As Param_GetRecordCountByName = New Param_GetRecordCountByName
                        paramRecordCount.LocationName = XRow("LB_PRO_NAME")
                        paramRecordCount.TrID = Rec_ID
                        paramRecordCount.CEN_BK_PAD_NO = open_PAD_No_Main
                        MaxValue_Loc = cBase._AssetLocDBOps.GetRecordCountByName(XRow("LB_PRO_NAME"), ClientScreen.Profile_LandAndBuilding, open_PAD_No_Main, Rec_ID)
                        'dbService.Wrap_GetSingleValue(RealServiceFunctions.AssetLocations_GetRecordCountByName, BasicParam_2, paramRecordCount)
                        If MaxValue_Loc Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        If MaxValue_Loc <> 0 Then
                            SaveButtonChecksParam.messageBoxText = "Location With Same Name Already Available...!"
                            SaveButtonChecksParam.messageCaption = "Property Name Duplicate"
                            SaveButtonChecksParam.messageIcon = "Exclamation"
                            SaveButtonChecksParam.dialogResult = "Retry"
                            Return SaveButtonChecksParam
                            Exit Function
                        End If
                    End If

                    If XRow("LB_PRO_NAME").ToString.Length > 0 Then
                        'Dim BasicParam_lb As Basic_Param = Basic_Param.Clone()
                        'BasicParam_lb.screen = ClientScreen.Profile_LandAndBuilding
                        Dim LocNames As DataTable = cBase._L_B_DBOps.GetPendingTfs_LocNames(open_Cen_Rec_ID)
                        If Not LocNames Is Nothing Then
                            If LocNames.Rows.Count > 0 Then
                                For I = 0 To LocNames.Rows.Count - 1
                                    If XRow("LB_PRO_NAME").ToString.ToUpper = LocNames.Rows(I)(0).ToString.ToUpper Then
                                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Location name")
                                        SaveButtonChecksParam.messageCaption = "Duplication in Referred Record, Location with same name already exists in pending Transfers!!"
                                        SaveButtonChecksParam.messageIcon = "Exclamation"
                                        SaveButtonChecksParam.dialogResult = "Retry"
                                        Return SaveButtonChecksParam
                                        Exit Function
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
                '--------Check for Creation date of WIP---------
                'If Not XRow("WIP_REF_TYPE") Is Nothing And Not IsDBNull(XRow("WIP_REF_TYPE")) Then
                '    If XRow("WIP_REF_TYPE") = "EXISTING" Then
                '        'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                '        Dim WIP_ID = IIf(IsDBNull(XRow("REF_REC_ID")), Nothing, XRow("REF_REC_ID"))
                '        Dim creationStats As DataTable = WIP_Creation_Vouchers.GetRefCreationDateByWIPID(WIP_ID, BasicParam_2)
                '        If Not creationStats Is Nothing Then
                '            If creationStats.Rows.Count > 0 Then
                '                If creationStats.Rows(0)("TR_DATE") > Convert.ToDateTime(Txt_V_Date) Then
                '                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                '                    SaveButtonChecksParam.ToolTipText = "Referencing Voucher Date must be greater than creation Date(" & Convert.ToDateTime(creationStats.Rows(0)("TR_DATE")).ToString(_Date_Format_DD_MMM_YYYY) & ") of WIP namely " & creationStats.Rows(0)("WIP_REF").ToString() & "  . . . !"
                '                    SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                '                    SaveButtonChecksParam.dialogResult = "None"
                '                    SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                '                    Return SaveButtonChecksParam
                '                    Exit Function
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
            Next

            '----------------------------------// Start Dependencies //-----------------------------
            ' Dim BasicParam_3 As Basic_Param = Basic_Param.Clone()
            ' BasicParam_3.screen = ClientScreen.Accounts_Voucher_Payment

            If IsMultiUserAllowed Then
                If NavMode = Common_Lib.Common.Navigation_Mode._New Or NavMode = Common_Lib.Common.Navigation_Mode._New_From_Selection Or NavMode = Common_Lib.Common.Navigation_Mode._Edit Then
                    If GLookUp_PartyList1_Tag.Length > 0 Then 'Is party record Changed?
                        Dim ParamAddresses As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                        ParamAddresses.Type = "PARTY"
                        ParamAddresses.Party_Rec_ID = GLookUp_PartyList1_Tag

                        ' _BasicParam.screen = ClientScreen.Accounts_Voucher_Payment
                        Dim addresses As DataTable = cBase._Address_DBOps.GetList(ClientScreen.Accounts_Voucher_Payment, ParamAddresses)
                        'Real.Addresses.GetList_Common(BasicParam_3, ParamAddresses)
                        If addresses Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        ' Dim oldEditOn As DateTime = Me.GLookUp_PartyList1View.GetRowCellValue(Me.GLookUp_PartyList1View.FocusedRowHandle, "REC_EDIT_ON")
                        Dim oldEditOn As DateTime = oldREC_EDIT_ON

                        If addresses.Rows.Count <= 0 Then  ' Referred Address Book Record deleted 
                            SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Address Book(Party)")
                            SaveButtonChecksParam.messageCaption = "Referred Record Already Deleted!!"
                            SaveButtonChecksParam.messageIcon = "Exclamation"
                            SaveButtonChecksParam.dialogResult = "Retry"
                            Return SaveButtonChecksParam
                            Exit Function
                        Else
                            Dim NewEditOn As DateTime = addresses.Rows(0)("REC_EDIT_ON")
                            If oldEditOn <> NewEditOn Then 'A/E,E/E
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Address Book(Party)")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Deleted!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If
                    End If

                    'Bank Account dependency check #Ref G30
                    'If Me.GridView2.RowCount > 0 Then
                    If DTGridView2.Rows.Count > 0 Then

                        For Each dtRow As DataRow In DTGridView2.Rows
                            Dim ParamBank As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
                            ParamBank.Bank_Account_Rec_ID = dtRow("ID")

                            '_BasicParam.screen = ClientScreen.Accounts_Voucher_Payment
                            Dim d1 As DataTable = cBase._BankAccountDBOps.GetList(ClientScreen.Accounts_Voucher_Payment, ParamBank)
                            'BankAccounts.GetList_Common(BasicParam_3, ParamBank)
                            If d1 Is Nothing Then
                                Throw New Exception("|SomeError happened during current operation!!|")
                                Exit Function
                            End If
                            Dim oldEditOn As DateTime = dtRow("Edit Time")
                            If d1.Rows.Count <= 0 Then ' Referred Bank A/c in payment deleted 
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Bank Account")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Deleted!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            Else
                                Dim NewEditOn As DateTime = d1.Rows(0)("REC_EDIT_ON")
                                Dim diff As Int32 = DateTime.Compare(oldEditOn, NewEditOn)
                                If diff > 1 Or diff < -1 Then 'A/E,E/E <>
                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Bank Account")
                                    SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                        Next
                    End If

                    'Bank Balance less than the amount paid

                    'Dim d1 As DataTable = Base._Voucher_DBOps.GetBankBalanceSummary(Base._open_Year_Sdt, Convert.ToDateTime(Txt_V_Date.Text), Base._open_Year_Sdt, Base._open_Cen_ID, Base._open_Year_ID)
                    'If Me.GridView2.RowCount > 0 Then
                    '    For Each xROW In d1.Rows
                    '        Dim amt = 0
                    '        Dim match = False
                    '        For I As Integer = 0 To Me.GridView2.RowCount - 1
                    '            Dim acc_no = Me.GridView2.GetRowCellValue(I, "A/c. No.")
                    '            If acc_no = xROW("BA_ACCOUNT_NO") Then
                    '                amt += Me.GridView2.GetRowCellValue(I, "Amount")
                    '                match = True
                    '            End If
                    '        Next
                    '        If match Then
                    '            If Convert.ToDecimal(xROW("CLOSING")) < Convert.ToDecimal(amt) Then
                    '                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.DependencyChanged("Bank balance is less than the payment"), "Referred Record Already Changed!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '                Me.DialogResult = Windows.Forms.DialogResult.Retry
                    '                FormClosingEnable = False : Me.Close()
                    '                Exit Sub
                    '            End If
                    '        End If
                    '    Next
                    'End If

                    For Each XRow In DT.Rows

                        Dim Loc_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        If Not Loc_ID Is Nothing Then
                            If Len(Loc_ID) > 0 Then
                                'Dim BasicParam_loc As Basic_Param = Basic_Param.Clone()
                                'BasicParam_loc.screen = ClientScreen.Core_Add_AssetLocation
                                Dim PropertyId As Object = cBase._AssetLocDBOps.GetPropertyID(Loc_ID)
                                If PropertyId Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                If Not PropertyId Is Nothing And Not IsDBNull(PropertyId) Then
                                    'checks if the referred property for constt items has been sold 
                                    ' BasicParam_2.screen = ClientScreen.Accounts_Vouchers
                                    'Dim ParamSale As Param_GetSaleReferenceRecord = New Param_GetSaleReferenceRecord
                                    'ParamSale.RefID = PropertyId
                                    'ParamSale.Exclude_PrevYears = False
                                    Dim SaleRecord As DataTable = cBase._Voucher_DBOps.GetSaleReferenceRecord(PropertyId)
                                    If Not SaleRecord Is Nothing Then
                                        If SaleRecord.Rows.Count > 0 Then
                                            SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a property which was sold on " & Convert.ToDateTime(SaleRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & SaleRecord.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for completing this Entry."
                                            SaveButtonChecksParam.messageCaption = "Error!!"
                                            SaveButtonChecksParam.messageIcon = "Exclamation"
                                            SaveButtonChecksParam.dialogResult = "Retry"
                                            Return SaveButtonChecksParam
                                            Exit Function
                                        End If
                                    End If
                                    'checks if the referred property for constt items has been transferred 
                                    'BasicParam_2.screen = ClientScreen.Accounts_Vouchers
                                    'Dim paramAssetTransfers As Param_GetAssetTransfers = New Param_GetAssetTransfers
                                    'paramAssetTransfers.refAssetID = PropertyId
                                    'paramAssetTransfers.YearID = Nothing
                                    'paramAssetTransfers.Exclude_PrevYears = False
                                    Dim AssetTrfRecord As DataTable = cBase._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, Nothing, PropertyId)
                                    If AssetTrfRecord.Rows.Count > 0 Then
                                        If AssetTrfRecord.Rows.Count > 0 Then
                                            SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a property which was Transfered on " & Convert.ToDateTime(AssetTrfRecord.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & AssetTrfRecord.Rows(0)("AMOUNT").ToString() & ".<br><br> Please delete the record for editing this Entry."
                                            SaveButtonChecksParam.messageCaption = "Error!!"
                                            SaveButtonChecksParam.messageIcon = "Exclamation"
                                            SaveButtonChecksParam.dialogResult = "Retry"
                                            Return SaveButtonChecksParam
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        'Dim param As Param_AssetLocation_GetList = New Param_AssetLocation_GetList
                        'param.Location_Rec_ID = Loc_ID
                        'param.Prev_Year_ID = input_Param.BasicData.prev_Unaudited_YearID

                        Dim cnt As Object
                        '--------Check for deletion of location in assets---------
                        ' Dim BasicParam_asset As Basic_Param = Basic_Param.Clone()
                        If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                            ' BasicParam_asset.screen = ClientScreen.Profile_GoldSilver
                            Dim d1 As DataTable = cBase._AssetLocDBOps.GetList(ClientScreen.Profile_GoldSilver, Loc_ID, Nothing)
                            ' AssetLocations.GetList(param, BasicParam_asset)
                            If d1.Rows.Count <= 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Asset Location")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If
                        If XRow("Item_Profile") = "OTHER ASSETS" Then
                            ' BasicParam_asset.screen = ClientScreen.Profile_Assets
                            cnt = (cBase._AssetLocDBOps.GetList(ClientScreen.Profile_Assets, Loc_ID, Nothing)).Rows.Count
                            If cnt <= 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Asset Location")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If
                        If XRow("Item_Profile") = "LIVESTOCK" Then
                            ' BasicParam_asset.screen = ClientScreen.Profile_LiveStock
                            cnt = (cBase._AssetLocDBOps.GetList(ClientScreen.Profile_LiveStock, Loc_ID, Nothing)).Rows.Count
                            If cnt <= 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Asset Location")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If
                        If XRow("Item_Profile") = "VEHICLES" Then
                            ' BasicParam_asset.screen = ClientScreen.Profile_Vehicles
                            cnt = (cBase._AssetLocDBOps.GetList(ClientScreen.Profile_Vehicles, Loc_ID, Nothing)).Rows.Count
                            If cnt <= 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Asset Location")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If

                            If Not XRow("VI_OWNERSHIP_AB_ID") Is Nothing And Not IsDBNull(XRow("VI_OWNERSHIP_AB_ID")) Then
                                Dim Ownership_ID = IIf(XRow("VI_OWNERSHIP_AB_ID").ToString.Length = 0, Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                                'BasicParam_asset.screen = ClientScreen.Profile_Vehicles
                                Dim ParamAddresses As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
                                ParamAddresses.Party_Rec_ID = Ownership_ID
                                Dim cnt1 = cBase._Address_DBOps.GetList(ClientScreen.Profile_Vehicles, ParamAddresses).Rows.Count()
                                'Addresses.GetList_Common(BasicParam_asset, ParamAddresses).Rows.Count()
                                If cnt1 <= 0 Then
                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Owner Record")
                                    SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                        End If
                        If XRow("Item_Profile") = "LAND & BUILDING" Then
                            Dim PartyID = IIf(XRow("LB_OWNERSHIP_PARTY_ID").ToString.Length = 0, Nothing, XRow("LB_OWNERSHIP_PARTY_ID"))
                            If Not PartyID Is Nothing Then  'In case of Owner type as Instt , no selection will be there 
                                ' BasicParam_asset.screen = ClientScreen.Profile_LandAndBuilding
                                cnt = (cBase._Address_DBOps.GetPartiesList(ClientScreen.Profile_LandAndBuilding, PartyID)).Rows.Count()
                                If cnt <= 0 Then 'Referred Address book record deleted 
                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Party Record")
                                    SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                        End If


                        If XRow("Item_Profile") = "TELEPHONE BILL" Then
                            Dim MaxValue As Object = 0
                            ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                            MaxValue = (cBase._telephoneDBOps.GetListByCondition(ClientScreen.Accounts_Voucher_Payment, "AND TP.REC_ID ='" & XRow("TP_ID") & "'")).Count
                            If MaxValue = 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Telephone Profile")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Deleted!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If

                        'Select Property screen
                        If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                            Dim Cross_Ref_ID As String = ""
                            Cross_Ref_ID = XRow("LB_REC_ID")
                            'checks if the referred property for constt items has been sold 
                            ' BasicParam_2.screen = ClientScreen.Accounts_Vouchers
                            Dim ParamSale As Param_GetSaleReferenceRecord = New Param_GetSaleReferenceRecord
                            ParamSale.RefID = Cross_Ref_ID
                            ParamSale.Exclude_PrevYears = False
                            Dim SaleRecordLocation As DataTable = cBase._Voucher_DBOps.GetSaleReferenceRecord(Cross_Ref_ID)

                            If Not SaleRecordLocation Is Nothing Then
                                If SaleRecordLocation.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a asset which was sold on " & Convert.ToDateTime(SaleRecordLocation.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & SaleRecordLocation.Rows(0)("TR_AMOUNT").ToString() & ".<br><br> Please delete the record for editing this Entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                            'checks if the referred property for constt items has been transferred 
                            'BasicParam_2.screen = ClientScreen.Accounts_Vouchers
                            'Dim paramAssetTransfers As Param_GetAssetTransfers = New Param_GetAssetTransfers
                            'paramAssetTransfers.refAssetID = Cross_Ref_ID
                            'paramAssetTransfers.YearID = Nothing
                            'paramAssetTransfers.Exclude_PrevYears = False
                            Dim AssetTrfRecordLocation As DataTable = cBase._AssetTransfer_DBOps.GetAssetTransfers(ClientScreen.Accounts_Vouchers, Nothing, Cross_Ref_ID)
                            'Voucher_AssetTransfer.GetAssetTransfers(paramAssetTransfers, BasicParam_2)
                            If AssetTrfRecordLocation.Rows.Count > 0 Then
                                If AssetTrfRecordLocation.Rows.Count > 0 Then
                                    SaveButtonChecksParam.messageBoxText = "Sorry ! Selected Entry refers a asset which was Transfered on " & Convert.ToDateTime(AssetTrfRecordLocation.Rows(0)("TR_DATE")).ToLongDateString() & " for initial payment of Rs." & AssetTrfRecordLocation.Rows(0)("AMOUNT").ToString() & ".<br><br> Please delete the record for editing this Entry."
                                    SaveButtonChecksParam.messageCaption = "Error!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            End If
                            'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                            'Dim ParamLandAndBuilding As Param_LandAndBuilding_GetListForExpenses = New Param_LandAndBuilding_GetListForExpenses
                            '' Me.xMID.Text
                            'ParamLandAndBuilding.MasterID = Rec_ID
                            'ParamLandAndBuilding.LB_Rec_ID = Cross_Ref_ID
                            'ParamLandAndBuilding.Next_Year_ID = next_Unaudited_YearID
                            'ParamLandAndBuilding.Prev_Year_ID = prev_Unaudited_YearID
                            cnt = (cBase._L_B_DBOps.GetListForExpenses(Rec_ID, ClientScreen.Accounts_Voucher_Payment, Cross_Ref_ID)).Count()
                            If cnt <= 0 Then
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Property")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                            ' BasicParam_asset.screen = ClientScreen.Profile_LandAndBuilding
                            Dim LB_RECORD As DataTable = cBase._L_B_DBOps.GetRecord(XRow("LB_REC_ID").ToString())
                            Dim Rec_Edit_On_1 As DateTime = LB_RECORD.Rows(0)("REC_EDIT_ON")
                            Dim Rec_Edit_On_2 As DateTime = Convert.ToDateTime(XRow("LB_REC_EDIT_ON"))
                            If ((Rec_Edit_On_1 - Rec_Edit_On_2).TotalSeconds > 1 Or (Rec_Edit_On_1 - Rec_Edit_On_2).TotalSeconds < -1) Then
                                'If Convert.ToDateTime(LB_RECORD.Rows(0)("REC_EDIT_ON")) <> Convert.ToDateTime(XRow("LB_REC_EDIT_ON")) Then 'Bug #5167 fixed
                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Asset Location")
                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed!!"
                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                SaveButtonChecksParam.dialogResult = "Retry"
                                Return SaveButtonChecksParam
                                Exit Function
                            End If
                        End If

                        '------------------------------------------------------------



                    Next
                    'Advances
                    '  BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                    Dim Adv_Param As Param_GetPaymentAdvances = New Param_GetPaymentAdvances
                    Adv_Param.Tr_M_Id = Rec_ID
                    Adv_Param.Prev_YearId = prev_Unaudited_YearID
                    Adv_Param.Next_YearID = next_Unaudited_YearID
                    Adv_Param.Adv_Party_ID = GLookUp_PartyList1_Tag
                    Dim ADV_TABLE As List(Of Return_AdvanceAdjustment_Grid_Datatable) = GetPendingAdvances(Adv_Param)
                    If ADV_TABLE Is Nothing Then
                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.SomeError
                        SaveButtonChecksParam.messageCaption = "Error!!"
                        SaveButtonChecksParam.messageIcon = "Exclamation"
                        SaveButtonChecksParam.dialogResult = "Retry"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If

                    If ADV_TABLE.Count > 0 Then
                        'Dim JointData As DataSet = GetAdvanceAdjustments(GLookUp_PartyList1.Tag, ADV_TABLE)
                        'Updating Out-Standing...
                        Dim xCnt As Integer = 1

                        'For I = 0 To GridView3.RowCount - 1
                        If (Not GridView3 Is Nothing) Then
                            For Each row3 As DataRow In GridView3.Rows
                                If Val(row3("Payment")) > 0 Then
                                    Dim ai_id = row3("AI_ID")
                                    Dim AI_EDIT_TIME_OLD = row3("REC_EDIT_ON")
                                    Dim AI_EDIT_TIME_NEW As DateTime
                                    Dim advFound As Boolean = False
                                    For Each XRow As Return_AdvanceAdjustment_Grid_Datatable In ADV_TABLE
                                        If XRow.AI_ID = ai_id Then
                                            advFound = True
                                            AI_EDIT_TIME_NEW = XRow.REC_EDIT_ON
                                            '#Ref A/E, E/E in cell Q30
                                            If AI_EDIT_TIME_NEW <> AI_EDIT_TIME_OLD Then 'Bugs 4855,4856 fixed
                                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Advances")
                                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed"
                                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                                SaveButtonChecksParam.dialogResult = "Retry"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            End If
                                            ' XRow("Out-Standing") = XRow("Advance") + Val(XRow("Addition")) - (XRow("Refund") + Val(XRow("Adjusted"))) : XRow("Sr") = xCnt
                                            'if outstanding amount is less than  payment 
                                            If XRow.Out_Standing < Val(row3("Payment")) Then
                                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Advances")
                                                SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                                SaveButtonChecksParam.dialogResult = "Retry"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            End If

                                            'if next year outstanding amount is less than  payment 
                                            If IsNumeric(XRow.Next_Year_Out_Standing) Then 'Bug #5611
                                                If XRow.Next_Year_Out_Standing < Val(row3("Payment")) Then
                                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Advances")
                                                    SaveButtonChecksParam.messageCaption = "Out-Standing at end of Next Financial amount is less than the payment being adjusted!!"
                                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                                    SaveButtonChecksParam.dialogResult = "Retry"
                                                    Return SaveButtonChecksParam
                                                    Exit Function
                                                End If
                                            End If

                                            Dim CreationDate_adv As Date = DateValue(IIf(IsDBNull(XRow.REF_CREATION_DATE) Or XRow.REF_CREATION_DATE Is Nothing, open_Year_Sdt, XRow.REF_CREATION_DATE))
                                            If DateValue(Txt_V_Date) < CreationDate_adv Then
                                                SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                                                SaveButtonChecksParam.ToolTipText = "Current Reference Voucher Date cannot be less than Advance Creation Voucher dated " & CreationDate_adv.ToLongDateString() & "...!"
                                                SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                                                SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                                                SaveButtonChecksParam.dialogResult = "None"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            Else
                                                'Me.ToolTip1.Hide(Me.Txt_V_Date)
                                            End If
                                        End If
                                        xCnt += 1
                                    Next
                                    If Not advFound Then
                                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Advances")
                                        SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                        SaveButtonChecksParam.messageIcon = "Exclamation"
                                        SaveButtonChecksParam.dialogResult = "Retry"
                                        Return SaveButtonChecksParam
                                        Exit Function
                                    End If
                                End If
                            Next
                        End If

                    Else
                        If (Not GridView3 Is Nothing) Then
                            For Each row3 As DataRow In GridView3.Rows
                                If Val(row3("Payment")) > 0 Then
                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Advances")
                                    SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            Next
                        End If
                    End If
                    'Liabilities
                    ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                    Dim Liab_Param As Param_GetPaymentLiabilities = New Param_GetPaymentLiabilities
                    Liab_Param.LI_Party_ID = GLookUp_PartyList1_Tag
                    Liab_Param.Next_YearID = next_Unaudited_YearID
                    Liab_Param.Prev_YearId = prev_Unaudited_YearID
                    Liab_Param.Tr_M_Id = Rec_ID
                    Dim LI_TABLE As List(Of Return_PendingLiabilities_Grid) = GetPendingLiabilities(Liab_Param)
                    'Liabilities.GetPaymentLiabilities(Liab_Param, BasicParam_2)
                    If LI_TABLE Is Nothing Then
                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.SomeError
                        SaveButtonChecksParam.messageCaption = "Error!!"
                        SaveButtonChecksParam.messageIcon = "Exclamation"
                        SaveButtonChecksParam.dialogResult = "Retry"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                    If LI_TABLE.Count > 0 Then
                        'Dim JointData1 As DataSet = GetLiabilityAdjustments(GLookUp_PartyList1.Tag, LI_TABLE)

                        'Updating Out-Standing..............................
                        Dim xCnt1 As Integer = 1
                        If (Not GridView4 Is Nothing) Then

                            For Each row4 As DataRow In GridView4.Rows
                                If Val(row4("Payment")) > 0 Then
                                    Dim li_id = row4("LI_ID")
                                    Dim LI_EDIT_TIME_OLD = row4("REC_EDIT_ON")
                                    Dim LI_EDIT_TIME_NEW As DateTime
                                    Dim liabFound As Boolean = False
                                    For Each XRow As Return_PendingLiabilities_Grid In LI_TABLE
                                        If XRow.LI_ID = li_id Then
                                            liabFound = True
                                            LI_EDIT_TIME_NEW = XRow.REC_EDIT_ON
                                            '#Ref A/E, E/E IN CELL R30
                                            If LI_EDIT_TIME_NEW <> LI_EDIT_TIME_OLD Then 'Bugs 4859,4860 fixed
                                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Profile Liabilities")
                                                SaveButtonChecksParam.messageCaption = "Referred Record Already Changed"
                                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                                SaveButtonChecksParam.dialogResult = "Retry"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            End If
                                            ' XRow("Out-Standing") = Convert.ToInt32(XRow("Amount") + Val(XRow("Addition")) - (Val(XRow("Paid")) + Val(XRow("Adjusted"))))
                                            'if outstanding amount is less than  payment 
                                            If XRow.OutStanding < Val(row4("Payment")) Then
                                                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Liabilities")
                                                SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                                SaveButtonChecksParam.messageIcon = "Exclamation"
                                                SaveButtonChecksParam.dialogResult = "Retry"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            End If

                                            'if next year outstanding amount is less than  payment 
                                            If IsNumeric(XRow.Next_Year_OutStanding) Then 'Bug #5611
                                                If XRow.Next_Year_OutStanding < Val(row4("Payment")) Then
                                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Liabilities")
                                                    SaveButtonChecksParam.messageCaption = "Out-Standing at end of Next Financial amount is less than the payment being adjusted!!"
                                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                                    SaveButtonChecksParam.dialogResult = "Retry"
                                                    Return SaveButtonChecksParam
                                                    Exit Function
                                                End If
                                            End If

                                            Dim CreationDate_adv As Date = DateValue(IIf(IsDBNull(XRow.REF_CREATION_DATE) Or XRow.REF_CREATION_DATE Is Nothing, open_Year_Sdt, XRow.REF_CREATION_DATE))
                                            If DateValue(Txt_V_Date) < CreationDate_adv Then
                                                SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                                                SaveButtonChecksParam.ToolTipText = "Current Reference Voucher Date cannot be less than Liability Creation Voucher dated " & CreationDate_adv.ToLongDateString() & "...!"
                                                SaveButtonChecksParam.ToolTipWindow = "Txt_V_Date"
                                                SaveButtonChecksParam.focusControlId = "Txt_V_Date"
                                                SaveButtonChecksParam.dialogResult = "None"
                                                Return SaveButtonChecksParam
                                                Exit Function
                                            Else
                                                'Me.ToolTip1.Hide(Me.Txt_V_Date)
                                            End If
                                        End If
                                        xCnt1 += 1
                                    Next
                                    If Not liabFound Then
                                        SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Liabilities")
                                        SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                        SaveButtonChecksParam.messageIcon = "Exclamation"
                                        SaveButtonChecksParam.dialogResult = "Retry"
                                        Return SaveButtonChecksParam
                                        Exit Function
                                    End If
                                End If
                            Next
                        End If
                    Else
                        If (Not GridView4 Is Nothing) Then

                            For Each row4 As DataRow In GridView4.Rows
                                If Val(row4("Payment")) > 0 Then
                                    SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DependencyChanged("Liabilities")
                                    SaveButtonChecksParam.messageCaption = "Out-Standing amount is less than the payment being adjusted!!"
                                    SaveButtonChecksParam.messageIcon = "Exclamation"
                                    SaveButtonChecksParam.dialogResult = "Retry"
                                    Return SaveButtonChecksParam
                                    Exit Function
                                End If
                            Next
                        End If
                    End If
                End If
            End If
            '---------------------------------// End Dependencies //------------------------------------
            ''CHECKING LOCK STATUS
            'If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Then
            '    Dim MaxValue As Object = 0
            '    MaxValue = Base._Payment_DBOps.GetStatusByMID(Me.xMID.Text)
            '    If MaxValue Is Nothing Then DevExpress.XtraEditors.XtraMessageBox.Show("E n t r y   N o t   F o u n d . . . !", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            '    If MaxValue = Common_Lib.Common.Record_Status._Locked Then DevExpress.XtraEditors.XtraMessageBox.Show("L o c k e d   E n t r y   c a n n o t   b e   E d i t  /  D e l e t e . . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!", "Information...", MessageBoxButtons.OK, MessageBoxIcon.Information) : Me.DialogResult = Windows.Forms.DialogResult.None : Exit Sub
            'End If

            'START - FOLLOWING IS  IN CLIENT 'Need to check
            ' Dim btn As SimpleButton : btn = CType(sender, SimpleButton)
            Dim Status_Action As String = ""
            If IsChk_Incompleted Then Status_Action = Common_Lib.Common.Record_Status._Incomplete Else Status_Action = Common_Lib.Common.Record_Status._Completed
            ' If btn.Name = BUT_DEL.Name Then Status_Action = Common_Lib.Common.Record_Status._Deleted
            ' END - FOLLOWING IS  IN CLIENT

            '-----------------------------+
            'Split Entry Data Table
            '-----------------------------+
            '.....Start.......\
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            'Dim InParamVoucherInsert As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment()

            Dim ROW As DataRow
            Dim Link_DS As New DataSet() : Dim Link_DT As DataTable = Link_DS.Tables.Add("Link_Item")
            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._New Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then
                Dim Payment_DS As New DataSet() : Dim Payment_DT As DataTable = Payment_DS.Tables.Add("Data")
                With Payment_DT
                    .Columns.Add("Sr.", Type.GetType("System.Int32"))
                    .Columns.Add("Amount", Type.GetType("System.Decimal"))
                    .Columns.Add("Mode", Type.GetType("System.String"))
                    .Columns.Add("No.", Type.GetType("System.String"))
                    .Columns.Add("Date", Type.GetType("System.String"))
                    .Columns.Add("CDate", Type.GetType("System.String"))
                    .Columns.Add("Ref_ID", Type.GetType("System.String"))
                    .Columns.Add("OFFSET_ID", Type.GetType("System.String"))
                End With
                Dim xCnt As Integer = 1

                If (Not GridView3 Is Nothing) Then
                    For Each row3 As DataRow In GridView3.Rows
                        If Val(row3("Payment")) > 0 Then
                            ROW = Payment_DT.NewRow
                            ROW("Sr.") = xCnt
                            ROW("Amount") = ConvertAsDecimal(row3("Payment"))
                            ROW("Mode") = "ADVANCE"
                            ROW("No.") = ""
                            ROW("Ref_ID") = ""
                            ROW("OFFSET_ID") = row3("OFFSET_ID")
                            Payment_DT.Rows.Add(ROW)
                            xCnt += 1
                        End If
                    Next
                End If
                If Not Bank_Detail Is Nothing Then
                    For Each XRow In Bank_Detail.Rows
                        ROW = Payment_DT.NewRow
                        ROW("Sr.") = xCnt
                        ROW("Amount") = XRow("Amount")
                        ROW("Mode") = XRow("Mode")
                        ROW("No.") = XRow("No.")
                        ROW("Date") = XRow("Date")
                        ROW("CDate") = XRow("Clearing Date")
                        ROW("Ref_ID") = XRow("ID")
                        Payment_DT.Rows.Add(ROW)
                        xCnt += 1
                    Next
                End If
                If Val(Txt_CreditAmt) > 0 Then
                    ROW = Payment_DT.NewRow
                    ROW("Sr.") = xCnt
                    ROW("Amount") = ConvertAsDecimal(Txt_CreditAmt)
                    ROW("Mode") = "CREDIT"
                    ROW("No.") = ""
                    ROW("Ref_ID") = ""
                    Payment_DT.Rows.Add(ROW)
                End If
                If Val(Txt_CashAmt) > 0 Then
                    ROW = Payment_DT.NewRow
                    ROW("Sr.") = xCnt
                    ROW("Amount") = ConvertAsDecimal(Txt_CashAmt)
                    ROW("Mode") = "CASH"
                    ROW("No.") = ""
                    ROW("Ref_ID") = ""
                    Payment_DT.Rows.Add(ROW)
                End If

                With Link_DT
                    .Columns.Add("SrNo", Type.GetType("System.Int32"))
                    .Columns.Add("Item_ID", Type.GetType("System.String"))
                    .Columns.Add("Item_Trans_Type", Type.GetType("System.String"))
                    .Columns.Add("Item_Profile", Type.GetType("System.String"))
                    .Columns.Add("Dr_Led_id", Type.GetType("System.String"))
                    .Columns.Add("Cr_Led_id", Type.GetType("System.String"))
                    .Columns.Add("JV_Trans_Type", Type.GetType("System.String"))
                    .Columns.Add("JV_Dr_Led_id", Type.GetType("System.String"))
                    .Columns.Add("JV_Cr_Led_id", Type.GetType("System.String"))
                    .Columns.Add("Mode", Type.GetType("System.String"))
                    .Columns.Add("Ref_ID", Type.GetType("System.String"))
                    .Columns.Add("Ref_No", Type.GetType("System.String"))
                    .Columns.Add("Ref_Date", Type.GetType("System.String"))
                    .Columns.Add("Ref_CDate", Type.GetType("System.String"))
                    .Columns.Add("Amount", Type.GetType("System.Decimal"))
                    .Columns.Add("TDS", Type.GetType("System.Decimal"))
                    .Columns.Add("Remarks", Type.GetType("System.String"))
                    .Columns.Add("OFFSET_ID", Type.GetType("System.String"))
                    .Columns.Add("LB_REC_ID", Type.GetType("System.String"))
                    .Columns.Add("TP_ID", Type.GetType("System.String")) 'Telephone ID
                    .Columns.Add("REF_REC_ID", Type.GetType("System.String"))  'wip Reference rec_id
                    .Columns.Add("OLD_CREATION_PROF_REC_ID", Type.GetType("System.String"))  'in case of update this rec id represents the profile rec_id which was present and is being reposted
                End With
                '
                xCnt = 0 : Dim zID As Integer = 1 : Dim AdjustAmt As Decimal = 0 : Dim PayAmt As Decimal = 0
                PayAmt = Payment_DT.Rows(xCnt)("Amount")
                Dim TDSCounter As Integer = 0
                Dim LiabPayment As Decimal = 0
                For Each XRow In DT.Rows
                    TDSCounter = 0 'For each item TDS needs to be counted only once , whatever the number of payment modes
                    If XRow("Item_Led_ID") = "00083" Then LiabPayment = LiabPayment + XRow("Amount") ' Payment to Creditors 
                    Dim xAmt As Decimal = XRow("Amount")
                    While xAmt <> 0
                        If xAmt >= PayAmt Then
                            AdjustAmt = PayAmt : xAmt = xAmt - PayAmt : PayAmt = 0
                        Else
                            AdjustAmt = xAmt : PayAmt = Math.Round(ConvertAsDecimal(PayAmt) - ConvertAsDecimal(xAmt), 2) : xAmt = 0
                        End If
                        '---------------------------------+
                        ROW = Link_DT.NewRow
                        ROW("SrNo") = zID
                        ROW("OLD_CREATION_PROF_REC_ID") = XRow("CREATION_PROF_REC_ID")
                        ROW("Amount") = AdjustAmt
                        ROW("Item_ID") = XRow("Item_ID")
                        ROW("Item_Trans_Type") = XRow("Item_Trans_Type")
                        ROW("Item_Profile") = XRow("Item_Profile")
                        ROW("LB_REC_ID") = XRow("LB_REC_ID")
                        If TDSCounter = 0 Then 'For each item TDS needs to be counted only once , whatever the number of payment modes
                            ROW("TDS") = XRow("TDS") : TDSCounter += 1
                        Else
                            ROW("TDS") = 0
                        End If
                        If XRow("Item_Trans_Type").ToString.ToUpper = "DEBIT" Then
                            ROW("Dr_Led_id") = XRow("Item_Led_ID")
                            If Payment_DT.Rows(xCnt)("Mode") = "CASH" Then
                                ROW("Cr_Led_id") = "00080"
                            ElseIf Payment_DT.Rows(xCnt)("Mode") = "ADVANCE" Then
                                ROW("Cr_Led_id") = ""
                                'For JV Purpose..........................
                                'Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS [ITEM_ID]   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & Payment_DT.Rows(xCnt)("OFFSET_ID") & "'  "
                                ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                                Dim inParamItems As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
                                inParamItems.Type = "1"
                                inParamItems.ItemIDs = Payment_DT.Rows(xCnt)("OFFSET_ID")
                                inParamItems.currInsttID = open_Ins_ID
                                Dim d1 As DataTable = cBase._CoreDBOps.GetItemsByQuery_Common("", ClientScreen.Accounts_Voucher_Payment, inParamItems)
                                'CoreFunctions.GetItemsByQuery_Common(BasicParam_2, inParamItems)
                                If d1 Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                ROW("JV_Trans_Type") = d1.Rows(0)("Item_Trans_Type")
                                If d1.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                                    ROW("JV_Dr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                Else
                                    ROW("JV_Cr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                End If
                                '........................................
                            ElseIf Payment_DT.Rows(xCnt)("Mode") = "CREDIT" Then
                                ROW("Cr_Led_id") = ""
                                'For JV Purpose..........................
                                'Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS [ITEM_ID]   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='faa69e85-8e55-4b2f-9cbf-207cec96ba14'  "
                                ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment

                                inParam.Type = "1"
                                inParam.ItemIDs = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                                inParam.currInsttID = open_Ins_ID
                                Dim d1 As DataTable = cBase._CoreDBOps.GetItemsByQuery_Common("", ClientScreen.Accounts_Voucher_Payment, inParam)
                                'CoreFunctions.GetItemsByQuery_Common(BasicParam_2, inParam)
                                If d1 Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                ROW("JV_Trans_Type") = d1.Rows(0)("Item_Trans_Type")
                                If d1.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                                    ROW("JV_Dr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                Else
                                    ROW("JV_Cr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                End If
                                '........................................
                            Else
                                ROW("Cr_Led_id") = "00079"
                            End If
                        Else
                            ROW("Cr_Led_id") = XRow("Item_Led_ID")
                            If Payment_DT.Rows(xCnt)("Mode") = "CASH" Then
                                ROW("Dr_Led_id") = "00080"
                            ElseIf Payment_DT.Rows(xCnt)("Mode") = "ADVANCE" Then
                                ROW("Dr_Led_id") = ""
                                'For JV Purpose..........................
                                'Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS [ITEM_ID]   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='" & Payment_DT.Rows(xCnt)("OFFSET_ID") & "'  "
                                '  BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                                inParam.Type = "1"
                                inParam.ItemIDs = "OFFSET_ID"
                                inParam.currInsttID = open_Ins_ID
                                Dim d1 As DataTable = cBase._CoreDBOps.GetItemsByQuery_Common("", ClientScreen.Accounts_Voucher_Payment, inParam)
                                'CoreFunctions.GetItemsByQuery_Common(BasicParam_2, inParam)
                                If d1 Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                ROW("JV_Trans_Type") = d1.Rows(0)("Item_Trans_Type")
                                If d1.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                                    ROW("JV_Dr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                Else
                                    ROW("JV_Cr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                End If
                                '........................................
                            ElseIf Payment_DT.Rows(xCnt)("Mode") = "CREDIT" Then
                                ROW("Dr_Led_id") = ""
                                'For JV Purpose..........................
                                'Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS [ITEM_ID]   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='faa69e85-8e55-4b2f-9cbf-207cec96ba14'  "
                                '  BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                                inParam.Type = "1"
                                inParam.ItemIDs = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                                inParam.currInsttID = open_Ins_ID
                                Dim d1 As DataTable = cBase._CoreDBOps.GetItemsByQuery_Common("", ClientScreen.Accounts_Voucher_Payment, inParam)
                                'CoreFunctions.GetItemsByQuery_Common(BasicParam_2, inParam)
                                If d1 Is Nothing Then
                                    Throw New Exception("|SomeError happened during current operation!!|")
                                    Exit Function
                                End If
                                ROW("JV_Trans_Type") = d1.Rows(0)("Item_Trans_Type")
                                If d1.Rows(0)("Item_Trans_Type") = "DEBIT" Then
                                    ROW("JV_Dr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                Else
                                    ROW("JV_Cr_Led_id") = d1.Rows(0)("ITEM_LED_ID")
                                End If
                                '........................................
                            Else
                                ROW("Dr_Led_id") = "00079"
                            End If
                        End If
                        ROW("Mode") = Payment_DT.Rows(xCnt)("Mode")
                        ROW("Ref_ID") = Payment_DT.Rows(xCnt)("Ref_ID")
                        ROW("Ref_No") = Payment_DT.Rows(xCnt)("No.")
                        ROW("Ref_Date") = Payment_DT.Rows(xCnt)("Date")
                        ROW("Ref_CDate") = Payment_DT.Rows(xCnt)("CDate")
                        ROW("Remarks") = XRow("Remarks")
                        ROW("OFFSET_ID") = Payment_DT.Rows(xCnt)("OFFSET_ID")
                        ROW("TP_ID") = XRow("TP_ID")
                        ROW("REF_REC_ID") = XRow("REF_REC_ID")
                        Link_DT.Rows.Add(ROW)
                        '---------------------------------+
                        If PayAmt = 0 Then xCnt += 1 : If xCnt <= (Payment_DT.Rows.Count - 1) Then PayAmt = Payment_DT.Rows(xCnt)("Amount")
                    End While
                    If PayAmt = 0 Then xCnt += 1 : If xCnt <= (Payment_DT.Rows.Count - 1) Then PayAmt = Payment_DT.Rows(xCnt)("Amount")
                    zID += 1
                Next
                If Val(Txt_LB_Amt) <> LiabPayment Then
                    SaveButtonChecksParam.ToolTipTitle = "Incomplete Information . . ."
                    SaveButtonChecksParam.ToolTipText = "Amount of Liability adjusted is not equal to Payment to Creditors...!"
                    SaveButtonChecksParam.ToolTipWindow = "Txt_LB_Amt"
                    SaveButtonChecksParam.focusControlId = "Txt_LB_Amt"
                    SaveButtonChecksParam.dialogResult = "None"
                    Return SaveButtonChecksParam
                    Exit Function
                Else
                    'Me.ToolTip1.Hide(Me.Txt_LB_Amt)
                End If
            End If
            '......End......../
            '-----------------------------+

            'TDS
            Dim TDS_DT As DataTable = New DataTable
            With TDS_DT
                .Columns.Add("ITEM_ID", Type.GetType("System.String"))
                .Columns.Add("JV_Trans_Type", Type.GetType("System.String"))
                .Columns.Add("JV_Dr_Led_id", Type.GetType("System.String"))
                .Columns.Add("JV_Cr_Led_id", Type.GetType("System.String"))
            End With

            Dim TDS_ROW As DataRow = TDS_DT.NewRow

            'Dim SQL_STR2 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS [ITEM_ID]   FROM ITEM_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID='f2fd022c-3811-4bd9-94f8-382987f2a260'  "
            ' BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
            inParam.Type = "1"
            inParam.ItemIDs = "f2fd022c-3811-4bd9-94f8-382987f2a260"
            inParam.currInsttID = open_Ins_ID
            Dim dTDS As DataTable = cBase._CoreDBOps.GetItemsByQuery_Common("", ClientScreen.Accounts_Voucher_Payment, inParam)
            'CoreFunctions.GetItemsByQuery_Common(BasicParam_2, inParam)
            If dTDS Is Nothing Then
                Throw New Exception("|SomeError happened during current operation!!|")
                Exit Function
            End If
            TDS_ROW("ITEM_ID") = dTDS.Rows(0)("ITEM_ID")
            TDS_ROW("JV_Trans_Type") = dTDS.Rows(0)("Item_Trans_Type")
            If dTDS.Rows(0)("Item_Trans_Type").ToString.ToUpper = "DEBIT" Then
                TDS_ROW("JV_Dr_Led_id") = dTDS.Rows(0)("ITEM_LED_ID")
            Else
                TDS_ROW("JV_Cr_Led_id") = dTDS.Rows(0)("ITEM_LED_ID")
            End If
            TDS_DT.Rows.Add(TDS_ROW)
            'END TDS

            Dim InNewParam As Param_Txn_Insert_VoucherPayment = New Param_Txn_Insert_VoucherPayment
            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._New Or Val(NavMode) = Common_Lib.Common.Navigation_Mode._New_From_Selection Then 'new

                Rec_ID = System.Guid.NewGuid().ToString()
                Dim STR1 As String = ""

                'Master Record 
                Dim InMInfo As Parameter_InsertMasterInfo_VoucherPayment = New Parameter_InsertMasterInfo_VoucherPayment()
                InMInfo.TxnCode = Common_Lib.Common.Voucher_Screen_Code.Payment
                InMInfo.VNo = Txt_V_NO
                If IsDate(Txt_V_Date) Then InMInfo.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InMInfo.TDate = Txt_V_Date
                'InMInfo.TDate = Txt_V_Date.Text
                InMInfo.InvoiceNo = Txt_Inv_No
                If IsDate(Txt_Inv_Date) Then InMInfo.InvDate = Convert.ToDateTime(Txt_Inv_Date).ToString(cBase._Server_Date_Format_Short) Else InMInfo.InvDate = Txt_Inv_Date
                'InMInfo.InvDate = Me.Txt_Inv_Date.Text

                InMInfo.PartyID = GLookUp_PartyList1_Tag
                InMInfo.SubTotal = ConvertAsDecimal(Txt_SubTotal)
                InMInfo.Cash = ConvertAsDecimal(Txt_CashAmt)
                InMInfo.Bank = ConvertAsDecimal(Txt_BankAmt)
                InMInfo.Advance = ConvertAsDecimal(Txt_AdvAmt)
                InMInfo.Liability = ConvertAsDecimal(Txt_LB_Amt)
                InMInfo.Credit = ConvertAsDecimal(Txt_CreditAmt)
                InMInfo.TDS = ConvertAsDecimal(Txt_TDS_Amt)
                If IsDate(Txt_DueDate) Then InMInfo.CreditDueDate = Convert.ToDateTime(Txt_DueDate).ToString(cBase._Server_Date_Format_Short) Else InMInfo.CreditDueDate = Txt_DueDate
                'InMInfo.CreditDueDate = Me.Txt_DueDate.Text
                InMInfo.Status_Action = Status_Action
                InMInfo.RecID = Rec_ID

                'If Not Base._Payment_DBOps.InsertMasterInfo(InMInfo) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                InNewParam.param_InsertMaster = InMInfo

                Dim ctr As Integer = 0
                '-------------
                Dim cnt As Integer = 0
                Dim Insert(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertAdvJV(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertCreditJV(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertTDSJV1(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertTDSJV2(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertLiability(Link_DT.Rows.Count) As Parameter_InsertTRID_Liabilities
                Dim InsertAdvances(Link_DT.Rows.Count) As Parameter_InsertTRID_Advances
                Dim InsertDeposits(Link_DT.Rows.Count) As Parameter_InsertTRID_Deposits

                For Each XRow In Link_DT.Rows
                    Dim ID As String = System.Guid.NewGuid().ToString()
                    If ctr = 0 Then xID = ID
                    ctr += 1
                    'Dim R_Date As DateTime = Nothing : Dim R_Date_Str As String = "" : If IsDate(XRow("Ref_Date")) Then : R_Date = XRow("Ref_Date") : R_Date_Str = "#" & R_Date.ToString(Base._Date_Format_Short) & "#" : Else : R_Date_Str = " NULL " : End If
                    'Dim C_Date As DateTime = Nothing : Dim C_Date_Str As String = "" : If IsDate(XRow("Ref_CDate")) Then : C_Date = XRow("Ref_CDate") : C_Date_Str = "#" & C_Date.ToString(Base._Date_Format_Short) & "#" : Else : C_Date_Str = " NULL " : End If
                    Dim Cross_Ref_ID As String = "NULL"
                    Dim ScreenCode As Common_Lib.Common.Voucher_Screen_Code = Common_Lib.Common.Voucher_Screen_Code.Payment

                    If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                        Cross_Ref_ID = "'" & XRow("LB_REC_ID") & "'"
                    End If
                    If XRow("REF_REC_ID").ToString.Length > 0 Then
                        Cross_Ref_ID = "'" & XRow("REF_REC_ID") & "'"
                    End If
                    Dim Sub_Cr_Led_ID As String = ""
                    If IIf(IsDBNull(XRow("Mode")), "", XRow("Mode")).ToString.ToUpper <> "CASH" Then
                        Sub_Cr_Led_ID = IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID"))
                    End If
                    Dim InParamVoucherInsert1 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                    InParamVoucherInsert1.TransCode = ScreenCode
                    InParamVoucherInsert1.VNo = Txt_V_NO
                    If IsDate(Txt_V_Date) Then InParamVoucherInsert1.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.TDate = Txt_V_Date
                    'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                    InParamVoucherInsert1.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                    InParamVoucherInsert1.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), "", XRow("Item_Trans_Type"))
                    InParamVoucherInsert1.Cr_Led_ID = IIf(IsDBNull(XRow("Cr_Led_id")), "", XRow("Cr_Led_id"))
                    InParamVoucherInsert1.Dr_Led_ID = IIf(IsDBNull(XRow("Dr_Led_id")), "", XRow("Dr_Led_id"))
                    InParamVoucherInsert1.SUB_Cr_Led_ID = Sub_Cr_Led_ID
                    InParamVoucherInsert1.SUB_Dr_Led_ID = ""
                    InParamVoucherInsert1.Amount = IIf(IsDBNull(XRow("Amount")), "", XRow("Amount"))
                    InParamVoucherInsert1.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                    InParamVoucherInsert1.Ref_BANK_ID = ""
                    InParamVoucherInsert1.Ref_Branch = ""
                    InParamVoucherInsert1.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))
                    If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsert1.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                    If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsert1.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                    'InParam.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                    'InParam.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                    InParamVoucherInsert1.Party1 = GLookUp_PartyList1_Tag
                    InParamVoucherInsert1.Narration = Txt_Narration
                    InParamVoucherInsert1.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                    InParamVoucherInsert1.Reference = Txt_Reference
                    InParamVoucherInsert1.CrossRefID = Cross_Ref_ID
                    InParamVoucherInsert1.MasterTxnID = Rec_ID
                    InParamVoucherInsert1.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                    InParamVoucherInsert1.Status_Action = Status_Action
                    InParamVoucherInsert1.RecID = Guid.NewGuid.ToString()

                    'If Not Base._Payment_DBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                    '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    Insert(cnt) = InParamVoucherInsert1

                    If XRow("Mode") = "ADVANCE" Then 'JV Entry
                        'Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim Amount As Decimal = ConvertAsDecimal(XRow("Amount"))
                        Dim InParamVoucherInsertAdv As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertAdv.TransCode = ScreenCode
                        InParamVoucherInsertAdv.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertAdv.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.TDate = Txt_V_Date
                        'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertAdv.ItemID = IIf(IsDBNull(XRow("OFFSET_ID")), "", XRow("OFFSET_ID"))
                        InParamVoucherInsertAdv.Type = IIf(IsDBNull(XRow("JV_Trans_Type")), "", XRow("JV_Trans_Type"))
                        InParamVoucherInsertAdv.Cr_Led_ID = IIf(IsDBNull(XRow("JV_Cr_Led_id")), "", XRow("JV_Cr_Led_id"))
                        InParamVoucherInsertAdv.Dr_Led_ID = IIf(IsDBNull(XRow("JV_Dr_Led_id")), "", XRow("JV_Dr_Led_id"))
                        InParamVoucherInsertAdv.SUB_Cr_Led_ID = ""
                        InParamVoucherInsertAdv.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertAdv.Amount = Amount
                        InParamVoucherInsertAdv.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertAdv.Ref_BANK_ID = ""
                        InParamVoucherInsertAdv.Ref_Branch = ""
                        InParamVoucherInsertAdv.Ref_No = ""

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertAdv.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertAdv.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParams.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParams.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertAdv.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertAdv.Narration = Txt_Narration
                        InParamVoucherInsertAdv.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertAdv.Reference = Txt_Reference
                        InParamVoucherInsertAdv.CrossRefID = "" 'Cross_Ref_ID'# bug 4082
                        InParamVoucherInsertAdv.MasterTxnID = Rec_ID
                        InParamVoucherInsertAdv.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertAdv.Status_Action = Status_Action
                        InParamVoucherInsertAdv.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertAdvJV(cnt) = InParamVoucherInsertAdv
                    End If
                    If XRow("Mode") = "CREDIT" Then 'JV Entry
                        ' Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim InParamVoucherInsertCr As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertCr.TransCode = ScreenCode
                        InParamVoucherInsertCr.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertCr.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCr.TDate = Txt_V_Date
                        'InPms.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertCr.ItemID = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                        InParamVoucherInsertCr.Type = IIf(IsDBNull(XRow("JV_Trans_Type")), "", XRow("JV_Trans_Type"))
                        InParamVoucherInsertCr.Cr_Led_ID = IIf(IsDBNull(XRow("JV_Cr_Led_id")), "", XRow("JV_Cr_Led_id"))
                        InParamVoucherInsertCr.Dr_Led_ID = IIf(IsDBNull(XRow("JV_Dr_Led_id")), "", XRow("JV_Dr_Led_id"))
                        InParamVoucherInsertCr.SUB_Cr_Led_ID = IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID"))
                        InParamVoucherInsertCr.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertCr.Amount = ConvertAsDecimal(XRow("Amount"))
                        InParamVoucherInsertCr.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertCr.Ref_BANK_ID = ""
                        InParamVoucherInsertCr.Ref_Branch = ""
                        InParamVoucherInsertCr.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertCr.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCr.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertCr.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCr.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InPms.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InPms.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertCr.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertCr.Narration = Txt_Narration
                        InParamVoucherInsertCr.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertCr.Reference = Txt_Reference
                        InParamVoucherInsertCr.CrossRefID = "" 'Cross_Ref_ID '# bug 4082
                        InParamVoucherInsertCr.MasterTxnID = Rec_ID
                        InParamVoucherInsertCr.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertCr.Status_Action = Status_Action
                        InParamVoucherInsertCr.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InPms) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertCreditJV(cnt) = InParamVoucherInsertCr
                    End If
                    If Val(XRow("TDS")) > 0 Then 'TDS JV Entry
                        'Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim Dr_Led_ID As String = ""
                        If Not IsDBNull(TDS_DT.Rows(0)("JV_Dr_Led_id")) Then Dr_Led_ID = TDS_DT.Rows(0)("JV_Dr_Led_id")
                        Dim Cr_Led_ID As String = ""
                        If Not IsDBNull(TDS_DT.Rows(0)("JV_Cr_Led_id")) Then Cr_Led_ID = TDS_DT.Rows(0)("JV_Cr_Led_id")
                        If Dr_Led_ID.Length > 0 Then
                            Cr_Led_ID = XRow("Cr_Led_id").ToString
                        Else
                            Dr_Led_ID = XRow("Dr_Led_id").ToString
                        End If
                        Dim InParamVoucherInsertTDSJV1 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertTDSJV1.TransCode = ScreenCode
                        InParamVoucherInsertTDSJV1.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertTDSJV1.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.TDate = Txt_V_Date
                        'InPrms.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertTDSJV1.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InParamVoucherInsertTDSJV1.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), Nothing, XRow("Item_Trans_Type"))
                        InParamVoucherInsertTDSJV1.Cr_Led_ID = ""
                        InParamVoucherInsertTDSJV1.Dr_Led_ID = Dr_Led_ID
                        InParamVoucherInsertTDSJV1.SUB_Cr_Led_ID = "" 'IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID")) #Bug 4440
                        InParamVoucherInsertTDSJV1.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV1.Amount = ConvertAsDecimal(XRow("TDS"))
                        InParamVoucherInsertTDSJV1.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertTDSJV1.Ref_BANK_ID = ""
                        InParamVoucherInsertTDSJV1.Ref_Branch = ""
                        InParamVoucherInsertTDSJV1.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertTDSJV1.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertTDSJV1.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InPrms.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InPrms.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertTDSJV1.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertTDSJV1.Narration = Txt_Narration
                        InParamVoucherInsertTDSJV1.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertTDSJV1.Reference = Txt_Reference
                        InParamVoucherInsertTDSJV1.CrossRefID = Cross_Ref_ID '# bug 4082
                        InParamVoucherInsertTDSJV1.MasterTxnID = Rec_ID
                        InParamVoucherInsertTDSJV1.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertTDSJV1.Status_Action = Status_Action
                        InParamVoucherInsertTDSJV1.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InPrms) Then 'InsertTDSJV1 - txn scope
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertTDSJV1(cnt) = InParamVoucherInsertTDSJV1

                        Dim InParamVoucherInsertTDSJV2 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertTDSJV2.TransCode = ScreenCode
                        InParamVoucherInsertTDSJV2.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertTDSJV2.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.TDate = Txt_V_Date
                        'InParameter.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertTDSJV2.ItemID = TDS_DT.Rows(0)("ITEM_ID")
                        InParamVoucherInsertTDSJV2.Type = TDS_DT.Rows(0)("JV_Trans_Type")
                        InParamVoucherInsertTDSJV2.Cr_Led_ID = Cr_Led_ID
                        InParamVoucherInsertTDSJV2.Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV2.SUB_Cr_Led_ID = "" ' IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID")) #Bug 4440
                        InParamVoucherInsertTDSJV2.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV2.Amount = ConvertAsDecimal(XRow("TDS"))
                        InParamVoucherInsertTDSJV2.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertTDSJV2.Ref_BANK_ID = ""
                        InParamVoucherInsertTDSJV2.Ref_Branch = ""
                        InParamVoucherInsertTDSJV2.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertTDSJV2.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertTDSJV2.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParameter.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParameter.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertTDSJV2.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertTDSJV2.Narration = Txt_Narration
                        InParamVoucherInsertTDSJV2.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertTDSJV2.Reference = Txt_Reference
                        InParamVoucherInsertTDSJV2.CrossRefID = "" 'Cross_Ref_ID'# bug 4082
                        InParamVoucherInsertTDSJV2.MasterTxnID = Rec_ID
                        InParamVoucherInsertTDSJV2.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertTDSJV2.Status_Action = Status_Action
                        InParamVoucherInsertTDSJV2.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParameter) Then 'InsertTDSJV2 - txn scope
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertTDSJV2(cnt) = InParamVoucherInsertTDSJV2
                    End If

                    If XRow("Mode") = "CREDIT" Then ''Profile : Other Liabilities Entry
                        Dim InLiab As Parameter_InsertTRID_Liabilities = New Parameter_InsertTRID_Liabilities()
                        InLiab.ItemID = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                        InLiab.PartyID = GLookUp_PartyList1_Tag
                        If IsDate(Txt_V_Date) Then InLiab.LiabilityDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InLiab.LiabilityDate = Txt_V_Date
                        'InLiab.LiabilityDate = Me.Txt_V_Date.Text.Trim()
                        If IsDate(Txt_DueDate) Then InLiab.PaymentDate = Convert.ToDateTime(Txt_DueDate).ToString(cBase._Server_Date_Format_Short) Else InLiab.PaymentDate = Txt_DueDate
                        'InLiab.PaymentDate = Me.Txt_DueDate.Text.Trim()
                        InLiab.Amount = Val(XRow("Amount"))
                        InLiab.Purpose = Txt_Narration
                        InLiab.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InLiab.TxnID = Rec_ID
                        InLiab.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InLiab.Status_Action = Status_Action
                        InLiab.RecID = System.Guid.NewGuid().ToString()
                        InLiab.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertLiability(InLiab) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If

                        InsertLiability(cnt) = InLiab
                    End If
                    If XRow("Item_Profile") = "ADVANCES" Then 'Profile : Advance Entry
                        Dim Amount As Double = Val(XRow("Amount"))
                        If Not XRow("TDS") Is Nothing Then
                            Amount = Amount + Val(XRow("TDS"))
                        End If
                        Dim InAdv As Parameter_InsertTRID_Advances = New Parameter_InsertTRID_Advances()
                        InAdv.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InAdv.PartyID = GLookUp_PartyList1_Tag
                        If IsDate(Txt_V_Date) Then InAdv.AdvanceDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InAdv.AdvanceDate = Txt_V_Date
                        'InAdv.AdvanceDate = Me.Txt_V_Date.Text.Trim()
                        InAdv.Amount = Amount 'bug 4019 ' Val(XRow("Amount"))
                        InAdv.Purpose = Txt_Narration
                        InAdv.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InAdv.TxnID = Rec_ID
                        InAdv.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InAdv.Status_Action = Status_Action
                        InAdv.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertAdvances(InAdv) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertAdvances(cnt) = InAdv
                    End If
                    If XRow("Item_Profile") = "OTHER DEPOSITS" Then 'Profile : Deposit Entry
                        Dim InDept As Parameter_InsertTRID_Deposits = New Parameter_InsertTRID_Deposits()
                        InDept.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InDept.AgainstInsurance = "NO"
                        InDept.PartyID = GLookUp_PartyList1_Tag
                        InDept.InsCompanyMiscID = ""
                        If IsDate(Txt_V_Date) Then InDept.DepositDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InDept.DepositDate = Txt_V_Date
                        'InDept.DepositDate = Txt_V_Date.Text
                        InDept.DepositPeriod = 0
                        InDept.Amount = Val(XRow("Amount"))
                        InDept.InterestRate = 0
                        InDept.Purpose = Txt_Narration
                        InDept.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InDept.TxnID = Rec_ID
                        InDept.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InDept.Status_Action = Status_Action
                        InDept.RecID = System.Guid.NewGuid().ToString()
                        InDept.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertDeposits(InDept) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertDeposits(cnt) = InDept
                    End If
                    STR1 = ""
                    cnt += 1
                Next

                InNewParam.Insert = Insert
                InNewParam.InsertAdvanceJV = InsertAdvJV
                InNewParam.InsertCreditJV = InsertCreditJV
                InNewParam.InsertTDSJV1 = InsertTDSJV1
                InNewParam.InsertTDSJV2 = InsertTDSJV2
                InNewParam.InsertAdvances = InsertAdvances
                InNewParam.InsertDeposits = InsertDeposits
                InNewParam.InsertLiability = InsertLiability


                Dim InsertItem(DT.Rows.Count) As Parameter_InsertItem_VoucherPayment
                Dim InsertPurpose(DT.Rows.Count) As Parameter_InsertPurpose_VoucherPayment
                Dim InsertOthers(DT.Rows.Count) As Parameter_InsertOther_VoucherPayment
                Dim InsertGS(DT.Rows.Count) As Parameter_InsertTRIDAndTRSRNo_GoldSilver
                Dim InsertAssets(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_Assets
                Dim InsertLS(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_LiveStock
                Dim InsertVehicles(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_Vehicles
                Dim InsertProperty(DT.Rows.Count) As Parameter_InsertMasterIDAndSrNo_LandAndBuilding
                Dim InsertReferencesWIP(DT.Rows.Count) As Param_InsertTRIDAndTRSrNo_WIP_Profile
                Dim InsertTDSPaidToGovt(DT.Rows.Count) As Parameter_InsertTDSDeduction_VoucherPayment

                'Main Items
                Dim cntMI As Integer = 0
                For Each XRow In DT.Rows
                    Dim InItem As Parameter_InsertItem_VoucherPayment = New Parameter_InsertItem_VoucherPayment()
                    InItem.Txn_M_ID = Rec_ID
                    InItem.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                    InItem.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                    InItem.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), "", XRow("Item_Led_ID"))
                    InItem.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), "", XRow("Item_Trans_Type"))
                    InItem.PartyReq = IIf(IsDBNull(XRow("Item_Party_Req")), "", XRow("Item_Party_Req"))
                    InItem.Profile = IIf(IsDBNull(XRow("Item_Profile")), "", XRow("Item_Profile"))
                    InItem.ItemName = IIf(IsDBNull(XRow("Item Name")), "", XRow("Item Name"))
                    InItem.Head = IIf(IsDBNull(XRow("Head")), "", XRow("Head"))
                    InItem.Qty = Val(XRow("Qty."))
                    InItem.Unit = IIf(IsDBNull(XRow("Unit")), "", XRow("Unit"))
                    InItem.Rate = Val(XRow("Rate"))
                    InItem.Amount = ConvertAsDecimal(XRow("Amount"))
                    InItem.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                    InItem.TDS = IIf(IsDBNull(XRow("TDS")), "", XRow("TDS"))
                    InItem.Status_Action = Status_Action
                    InItem.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Payment_DBOps.InsertItem(InItem) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                    '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InsertItem(cntMI) = InItem

                    'Purpose.........
                    Dim InPurpose As Parameter_InsertPurpose_VoucherPayment = New Parameter_InsertPurpose_VoucherPayment()
                    InPurpose.TxnID = Rec_ID
                    InPurpose.PurposeID = IIf(IsDBNull(XRow("PUR_ID")), "", XRow("PUR_ID"))
                    InPurpose.Amount = ConvertAsDecimal(XRow("Amount"))
                    InPurpose.SrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                    InPurpose.Status_Action = Status_Action
                    InPurpose.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Payment_DBOps.InsertPurpose(InPurpose) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                    '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                    '    Exit Sub
                    'End If
                    InsertPurpose(cntMI) = InPurpose

                    If XRow("Item_Profile") = "TELEPHONE BILL" Then
                        Dim InOthers As Parameter_InsertOther_VoucherPayment = New Parameter_InsertOther_VoucherPayment()
                        InOthers.TxnMID = Rec_ID
                        InOthers.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                        InOthers.Ref_ID = IIf(IsDBNull(XRow("TP_ID")), Nothing, XRow("TP_ID"))
                        InOthers.Ref_Bill_No = IIf(IsDBNull(XRow("TP_BILL_NO")), Nothing, XRow("TP_BILL_NO"))
                        InOthers.Ref_Bill_Date = IIf(IsDBNull(XRow("TP_BILL_DATE")), Nothing, XRow("TP_BILL_DATE"))
                        InOthers.Period_From = IIf(IsDBNull(XRow("TP_PERIOD_FROM")), Nothing, XRow("TP_PERIOD_FROM"))
                        InOthers.Period_To = IIf(IsDBNull(XRow("TP_PERIOD_TO")), Nothing, XRow("TP_PERIOD_TO"))
                        'If Not Base._Payment_DBOps.InsertOther(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertOthers(cntMI) = InOthers
                    End If

                    If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                        Dim InGoldSilver As Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Parameter_InsertTRIDAndTRSRNo_GoldSilver()
                        InGoldSilver.Type = IIf(IsDBNull(XRow("Item_Profile")), Nothing, XRow("Item_Profile"))
                        InGoldSilver.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InGoldSilver.DescMiscID = IIf(IsDBNull(XRow("GS_DESC_MISC_ID")), Nothing, XRow("GS_DESC_MISC_ID"))
                        InGoldSilver.Weight = IIf(IsDBNull(XRow("GS_ITEM_WEIGHT")), Nothing, Val(XRow("GS_ITEM_WEIGHT")))
                        InGoldSilver.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InGoldSilver.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InGoldSilver.TxnID = Rec_ID
                        InGoldSilver.TxnSrno = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                        InGoldSilver.Status_Action = Status_Action
                        InGoldSilver.Screen = ClientScreen.Accounts_Voucher_Payment
                        InGoldSilver.Amount = IIf(IsDBNull(XRow("Amount")), "", XRow("Amount")) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        'If Not Base._GoldSilverDBOps.Insert(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertGS(cntMI) = InGoldSilver
                    End If
                    If XRow("Item_Profile") = "OTHER ASSETS" Then
                        ' Dim P_Date As DateTime = Nothing : Dim P_Date_Str As String = "" : If IsDate(XRow("AI_PUR_DATE")) Then : P_Date = XRow("AI_PUR_DATE") : P_Date_Str = "#" & P_Date.ToString(Base._Date_Format_Short) & "#" : Else : P_Date_Str = " NULL " : End If
                        Dim InAssets As Parameter_InsertTRIDAndTRSrNo_Assets = New Parameter_InsertTRIDAndTRSrNo_Assets()
                        InAssets.AssetType = IIf(IsDBNull(XRow("AI_TYPE")), Nothing, XRow("AI_TYPE"))
                        InAssets.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InAssets.Make = IIf(IsDBNull(XRow("AI_MAKE")), Nothing, XRow("AI_MAKE"))
                        InAssets.Model = IIf(IsDBNull(XRow("AI_MODEL")), Nothing, XRow("AI_MODEL"))
                        InAssets.SrNo = IIf(IsDBNull(XRow("AI_SERIAL_NO")), Nothing, XRow("AI_SERIAL_NO"))
                        InAssets.Rate = IIf(IsDBNull(XRow("Rate")), Nothing, Val(XRow("Rate")))
                        InAssets.InsAmount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        'If IsDate(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))) Then InAssets.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))).ToString(_Server_Date_Format_Short) Else InAssets.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                        InAssets.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                        InAssets.PurchaseAmount = IIf(InAssets.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")), 0) 'Bug #5046 fix, http://pm.bkinfo.in/issues/5345#note-12
                        InAssets.Warranty = IIf(IsDBNull(XRow("AI_WARRANTY")), Nothing, Val(XRow("AI_WARRANTY")))
                        InAssets.Quantity = IIf(IsDBNull(XRow("Qty.")), Nothing, Val(XRow("Qty.")))
                        InAssets.LocationId = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InAssets.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InAssets.TxnID = Rec_ID
                        InAssets.Image = IIf(IsDBNull(XRow("AI_IMAGE")), Nothing, XRow("AI_IMAGE"))
                        InAssets.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InAssets.Status_Action = Status_Action
                        InAssets.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._AssetDBOps.Insert(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertAssets(cntMI) = InAssets
                    End If

                    If XRow("Item_Profile") = "LIVESTOCK" Then
                        Dim InLiveStock As Parameter_InsertTRIDAndTRSrNo_LiveStock = New Parameter_InsertTRIDAndTRSrNo_LiveStock()
                        InLiveStock.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InLiveStock.Name = IIf(IsDBNull(XRow("LS_NAME")), Nothing, XRow("LS_NAME"))
                        InLiveStock.Year = IIf(IsDBNull(XRow("LS_BIRTH_YEAR")), Nothing, XRow("LS_BIRTH_YEAR"))
                        InLiveStock.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InLiveStock.Insurance = IIf(IsDBNull(XRow("LS_INSURANCE")), Nothing, XRow("LS_INSURANCE"))
                        InLiveStock.InsuranceID = IIf(IsDBNull(XRow("LS_INSURANCE_ID")), Nothing, XRow("LS_INSURANCE_ID"))
                        InLiveStock.PolicyNo = IIf(IsDBNull(XRow("LS_INS_POLICY_NO")), Nothing, XRow("LS_INS_POLICY_NO"))
                        If (IsDBNull(XRow("LS_INS_AMT"))) Then  'redmine Bug #133173 fixed
                            InLiveStock.InsAmount = Nothing
                        Else
                            InLiveStock.InsAmount = Val(XRow("LS_INS_AMT"))
                        End If

                        'InLiveStock.InsAmount = IIf(IsDBNull(XRow("LS_INS_AMT")), Nothing, Val(XRow("LS_INS_AMT")))
                        'If IsDate(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))) Then InLiveStock.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))).ToString(_Server_Date_Format_Short) Else InLiveStock.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                        InLiveStock.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                            InLiveStock.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                            InLiveStock.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                            InLiveStock.TxnID = Rec_ID
                            InLiveStock.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                            InLiveStock.Status_Action = Status_Action
                            InLiveStock.screen = ClientScreen.Accounts_Voucher_Payment

                            'If Not Base._LiveStockDBOps.Insert(InParam) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InsertLS(cntMI) = InLiveStock
                        End If
                        If XRow("Item_Profile") = "VEHICLES" Then
                        Dim InPms As Parameter_InsertTRIDAndTRSrNo_Vehicles = New Parameter_InsertTRIDAndTRSrNo_Vehicles()

                        InPms.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InPms.Make = IIf(IsDBNull(XRow("VI_MAKE")), Nothing, XRow("VI_MAKE"))
                        InPms.Model = IIf(IsDBNull(XRow("VI_MODEL")), Nothing, XRow("VI_MODEL"))
                        InPms.Reg_No_Pattern = IIf(IsDBNull(XRow("VI_REG_NO_PATTERN")), Nothing, XRow("VI_REG_NO_PATTERN"))
                        InPms.Reg_No = IIf(IsDBNull(XRow("VI_REG_NO")), Nothing, XRow("VI_REG_NO"))
                        'If IsDate(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))) Then InPms.RegDate = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))).ToString(_Server_Date_Format_Short) Else InPms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                        InPms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                        InPms.Ownership = IIf(IsDBNull(XRow("VI_OWNERSHIP")), Nothing, XRow("VI_OWNERSHIP"))
                        InPms.Ownership_AB_ID = IIf(IsDBNull(XRow("VI_OWNERSHIP_AB_ID")), Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                        If Not InPms.Ownership_AB_ID Is Nothing Then
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
                        'If IsDate(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))) Then InPms.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))).ToString(_Server_Date_Format_Short) Else InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                        InPms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                        InPms.Location_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InPms.Other_Details = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InPms.TxnID = Rec_ID
                        InPms.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InPms.Status_Action = Status_Action
                        InPms.Screen = ClientScreen.Accounts_Voucher_Payment
                        InPms.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        'If Not Base._VehicleDBOps.Insert(InPms) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertVehicles(cntMI) = InPms
                    End If


                    If XRow("Item_Profile") = "LAND & BUILDING" Then
                        Dim PartyID As String = Nothing
                        Dim InLandAndBuild As Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Parameter_InsertMasterIDAndSrNo_LandAndBuilding()

                        If XRow("LB_OWNERSHIP_PARTY_ID").ToString.Length = 36 Then PartyID = "'" & XRow("LB_OWNERSHIP_PARTY_ID") & "'"

                        InLandAndBuild.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InLandAndBuild.PropertyType = IIf(IsDBNull(XRow("LB_PRO_TYPE")), Nothing, XRow("LB_PRO_TYPE"))
                        InLandAndBuild.Category = IIf(IsDBNull(XRow("LB_PRO_CATEGORY")), Nothing, XRow("LB_PRO_CATEGORY"))
                        InLandAndBuild.Use = IIf(IsDBNull(XRow("LB_PRO_USE")), Nothing, XRow("LB_PRO_USE"))
                        InLandAndBuild.Name = IIf(IsDBNull(XRow("LB_PRO_NAME")), Nothing, Trim(XRow("LB_PRO_NAME")))
                        InLandAndBuild.Address = IIf(IsDBNull(XRow("LB_PRO_ADDRESS")), Nothing, XRow("LB_PRO_ADDRESS"))
                        InLandAndBuild.LB_Add1 = IIf(IsDBNull(XRow("LB_ADDRESS1")), Nothing, XRow("LB_ADDRESS1"))
                        InLandAndBuild.LB_Add2 = IIf(IsDBNull(XRow("LB_ADDRESS2")), Nothing, XRow("LB_ADDRESS2"))
                        InLandAndBuild.LB_Add3 = IIf(IsDBNull(XRow("LB_ADDRESS3")), Nothing, XRow("LB_ADDRESS3"))
                        InLandAndBuild.LB_Add4 = IIf(IsDBNull(XRow("LB_ADDRESS4")), Nothing, XRow("LB_ADDRESS4"))
                        InLandAndBuild.LB_CityID = IIf(IsDBNull(XRow("LB_CITY_ID")), Nothing, XRow("LB_CITY_ID"))
                        InLandAndBuild.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                        InLandAndBuild.LB_DisttID = IIf(IsDBNull(XRow("LB_DISTRICT_ID")), Nothing, XRow("LB_DISTRICT_ID"))
                        InLandAndBuild.LB_PinCode = IIf(IsDBNull(XRow("LB_PINCODE")), Nothing, XRow("LB_PINCODE"))
                        InLandAndBuild.LB_StateID = IIf(IsDBNull(XRow("LB_STATE_ID")), Nothing, XRow("LB_STATE_ID"))
                        InLandAndBuild.Ownership = IIf(IsDBNull(XRow("LB_OWNERSHIP")), Nothing, XRow("LB_OWNERSHIP"))
                        InLandAndBuild.Owner_Party_ID = PartyID
                        InLandAndBuild.SurveyNo = IIf(IsDBNull(XRow("LB_SURVEY_NO")), Nothing, XRow("LB_SURVEY_NO"))
                        InLandAndBuild.TotalArea = IIf(IsDBNull(XRow("LB_TOT_P_AREA")), Nothing, XRow("LB_TOT_P_AREA"))
                        InLandAndBuild.ConstructedArea = IIf(IsDBNull(XRow("LB_CON_AREA")), Nothing, XRow("LB_CON_AREA"))
                        InLandAndBuild.ConstructionYear = IIf(IsDBNull(XRow("LB_CON_YEAR")), Nothing, XRow("LB_CON_YEAR"))
                        InLandAndBuild.RCCRoof = IIf(IsDBNull(XRow("LB_RCC_ROOF")), Nothing, XRow("LB_RCC_ROOF"))
                        InLandAndBuild.DepositAmount = IIf(IsDBNull(XRow("LB_DEPOSIT_AMT")), Nothing, XRow("LB_DEPOSIT_AMT"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))) Then InLandAndBuild.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                        InLandAndBuild.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                        InLandAndBuild.MonthlyRent = IIf(IsDBNull(XRow("LB_MONTH_RENT")), Nothing, XRow("LB_MONTH_RENT"))
                        InLandAndBuild.MonthlyOtherExpenses = IIf(IsDBNull(XRow("LB_MONTH_O_PAYMENTS")), Nothing, XRow("LB_MONTH_O_PAYMENTS"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))) Then InLandAndBuild.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                        InLandAndBuild.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))) Then InLandAndBuild.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                        InLandAndBuild.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                        InLandAndBuild.OtherDocs = IIf(IsDBNull(XRow("LB_DOC_OTHERS")), Nothing, XRow("LB_DOC_OTHERS"))
                        InLandAndBuild.DocNames = IIf(IsDBNull(XRow("LB_DOC_NAME")), Nothing, XRow("LB_DOC_NAME"))
                        InLandAndBuild.Value = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InLandAndBuild.OtherDetails = IIf(IsDBNull(XRow("LB_OTHER_DETAIL")), Nothing, XRow("LB_OTHER_DETAIL"))
                        InLandAndBuild.MasterID = Rec_ID
                        InLandAndBuild.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InLandAndBuild.Status_Action = Status_Action
                        InLandAndBuild.RecID = IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID"))

                        'If Not Base._L_B_DBOps.Insert(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If

                        'EXTENDED
                        Dim InExtInfo(LB_EXTENDED_PROPERTY_TABLE.Rows.Count - 1) As Parameter_InsertExtendedInfo_LandAndBuilding
                        Dim cntExt As Integer = 0
                        If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                            For Each _Ext_Row As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
                                    Dim InEInfo As Parameter_InsertExtendedInfo_LandAndBuilding = New Parameter_InsertExtendedInfo_LandAndBuilding()
                                    InEInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                    InEInfo.SrNo = IIf(IsDBNull(_Ext_Row("LB_SR_NO")), Nothing, _Ext_Row("LB_SR_NO").ToString)
                                    InEInfo.Inst_ID = IIf(IsDBNull(_Ext_Row("LB_INS_ID")), Nothing, _Ext_Row("LB_INS_ID").ToString)
                                    InEInfo.TotalArea = Val(_Ext_Row("LB_TOT_P_AREA").ToString)
                                    InEInfo.ConstructedArea = Val(_Ext_Row("LB_CON_AREA").ToString)
                                    InEInfo.ConYear = IIf(IsDBNull(_Ext_Row("LB_CON_YEAR")), Nothing, _Ext_Row("LB_CON_YEAR").ToString)
                                    If IsDate(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))) Then InEInfo.MOU_Date = Convert.ToDateTime(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))).ToString(cBase._Server_Date_Format_Short) Else InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                    'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                    InEInfo.Value = Val(_Ext_Row("LB_VALUE").ToString)
                                    InEInfo.OtherDetails = IIf(IsDBNull(_Ext_Row("LB_OTHER_DETAIL")), Nothing, _Ext_Row("LB_OTHER_DETAIL").ToString)
                                    InEInfo.Status_Action = Status_Action
                                    InEInfo.RecID = System.Guid.NewGuid().ToString()

                                    'If Not Base._L_B_DBOps.InsertExtendedInfo(InEInfo) Then
                                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                    'End If
                                    InExtInfo(cntExt) = InEInfo
                                    cntExt += 1
                                End If
                            Next
                        End If
                        InLandAndBuild.InsertExtInfo = InExtInfo
                        'DOCS
                        Dim cntDoc As Integer = 0
                        Dim DocInfo(LB_DOCS_ARRAY.Rows.Count - 1) As Parameter_InsertDocInfo_LandAndBuilding
                        If Not LB_DOCS_ARRAY Is Nothing Then
                            For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                                If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
                                    Dim InDInfo As Parameter_InsertDocInfo_LandAndBuilding = New Parameter_InsertDocInfo_LandAndBuilding()
                                    InDInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                    InDInfo.Doc_Misc_ID = IIf(IsDBNull(_Ext_Row("LB_MISC_ID")), Nothing, _Ext_Row("LB_MISC_ID").ToString)
                                    InDInfo.Status_Action = Status_Action
                                    InDInfo.RecID = System.Guid.NewGuid().ToString()

                                    'If Not Base._L_B_DBOps.InsertDocumentsInfo(InDInfo) Then
                                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                    'End If
                                    DocInfo(cntDoc) = InDInfo
                                    cntDoc += 1
                                End If
                            Next
                        End If
                        InLandAndBuild.InsertDocInfo = DocInfo

                        'Add Location
                        Dim InAssetLoc As Param_AssetLoc_Insert = New Param_AssetLoc_Insert()
                        InAssetLoc.name = Trim(InLandAndBuild.Name)
                        InAssetLoc.OtherDetails = "Use Type: " & InLandAndBuild.PropertyType
                        InAssetLoc.Status_Action = Status_Action
                        InAssetLoc.Match_LB_ID = InLandAndBuild.RecID
                        InAssetLoc.Match_SP_ID = ""
                        'If Not Base._AssetLocDBOps.Insert(ClientScreen.Accounts_Voucher_Payment, InParam.Name, "Use Type: " & InParam.PropertyType, Status_Action, "", InParam.RecID) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InLandAndBuild.param_InsertAssetLoc = InAssetLoc

                        InsertProperty(cntMI) = InLandAndBuild

                        '------------------LAND & BUILDING
                    End If

                    '----------WIP References------------

                    If XRow("Item_Profile") = "WIP" Then
                        If XRow("WIP_REF_TYPE").ToString.Length > 0 Then
                            If XRow("WIP_REF_TYPE") = "NEW" Then 'XRow("Head").ToString.Contains("(WIP)") And
                                Dim InReference As Param_InsertTRIDAndTRSrNo_WIP_Profile = New Param_InsertTRIDAndTRSrNo_WIP_Profile()
                                InReference.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), Nothing, XRow("Item_Led_ID"))
                                InReference.Reference = IIf(IsDBNull(XRow("REFERENCE")), Nothing, XRow("REFERENCE"))
                                InReference.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, ConvertAsDecimal(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, ConvertAsDecimal(XRow("TDS")))
                                InReference.Status_Action = Status_Action
                                InReference.TxnID = Rec_ID
                                InReference.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                                InReference.Screen = ClientScreen.Accounts_Voucher_Payment

                                InsertReferencesWIP(cntMI) = InReference

                            End If
                        End If
                    End If

                    If Not XRow("REF_TDS_DED") Is Nothing Then
                        If XRow("REF_TDS_DED").ToString.Length > 0 Then
                            Dim InTdsPAid As Parameter_InsertTDSDeduction_VoucherPayment = New Parameter_InsertTDSDeduction_VoucherPayment()
                            InTdsPAid.RefTxnID_TDS_Paid_string = XRow("REF_TDS_DED").ToString
                            InTdsPAid.TxnSrNo = XRow("Sr.")
                            InsertTDSPaidToGovt(cntMI) = InTdsPAid
                        End If
                    End If

                    cntMI += 1
                Next

                InNewParam.InsertItem = InsertItem
                InNewParam.InsertPurpose = InsertPurpose
                InNewParam.InsertOther = InsertOthers
                InNewParam.InsertGS = InsertGS
                InNewParam.InsertLS = InsertLS
                InNewParam.InsertVehicles = InsertVehicles
                InNewParam.InsertAssets = InsertAssets
                InNewParam.InsertProperty = InsertProperty
                InNewParam.InsertReferencesWIP = InsertReferencesWIP
                InNewParam.InsertTdsPAid = InsertTDSPaidToGovt

                Dim InAdvancePmt(GridView3.Rows.Count - 1) As Parameter_InsertPayment_VoucherPayment
                Dim InLiabPmt(GridView4.Rows.Count - 1) As Parameter_InsertPayment_VoucherPayment
                Dim InBankPmt(0) As Parameter_InsertPaymentMT_VoucherPayment
                If Not Bank_Detail Is Nothing Then
                    Array.Resize(InBankPmt, Bank_Detail.Rows.Count)
                End If

                'Advance Payment
                Dim I As Integer = 0
                If (Not GridView3 Is Nothing) Then
                    For Each ROW3 As DataRow In GridView3.Rows
                        If Val(ROW3("Payment")) > 0 Then
                            Dim InPay As Parameter_InsertPayment_VoucherPayment = New Parameter_InsertPayment_VoucherPayment()
                            InPay.TxnMID = Rec_ID
                            InPay.Type = "ADVANCE"
                            InPay.SrNo = ROW3("Sr")
                            InPay.RefID = ROW3("AI_ID")
                            InPay.RefAmount = ConvertAsDecimal(ROW3("Payment"))
                            InPay.Status_Action = Status_Action
                            InPay.RecID = System.Guid.NewGuid().ToString()

                            'If Not Base._Payment_DBOps.InsertPayment(InPay) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InAdvancePmt(I) = InPay
                            I = I + 1
                        End If
                    Next
                End If
                InNewParam.InAdvancePmt = InAdvancePmt

                'Liabilities Payment
                I = 0
                If (Not GridView4 Is Nothing) Then
                    For Each ROW4 As DataRow In GridView4.Rows
                        If Val(ROW4("Payment")) > 0 Then
                            Dim InPayment As Parameter_InsertPayment_VoucherPayment = New Parameter_InsertPayment_VoucherPayment()
                            InPayment.TxnMID = Rec_ID
                            InPayment.Type = "LIABILITIES"
                            InPayment.SrNo = ROW4("Sr")
                            InPayment.RefID = ROW4("LI_ID")
                            InPayment.RefAmount = ConvertAsDecimal(ROW4("Payment"))
                            InPayment.Status_Action = Status_Action
                            InPayment.RecID = System.Guid.NewGuid().ToString()

                            'If Not Base._Payment_DBOps.InsertPayment(InPayment) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InLiabPmt(I) = InPayment
                            I = I + 1
                        End If
                    Next
                End If
                InNewParam.InLiabPmt = InLiabPmt

                'Bank Payment
                cnt = 0
                If Not Bank_Detail Is Nothing Then
                    For Each XRow In Bank_Detail.Rows
                        Dim InPay1 As Parameter_InsertPaymentMT_VoucherPayment = New Parameter_InsertPaymentMT_VoucherPayment()
                        InPay1.TxnMID = Rec_ID
                        InPay1.Type = "BANK"
                        InPay1.SrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                        InPay1.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InPay1.RefID = IIf(IsDBNull(XRow("ID")), "", XRow("ID"))
                        InPay1.RefNo = IIf(IsDBNull(XRow("No.")), "", XRow("No."))
                        'If IsDate(IIf(IsDBNull(XRow("Date")), "", XRow("Date"))) Then InPay1.RefDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Date")), "", XRow("Date"))).ToString(_Server_Date_Format_Short) Else InPay1.RefDate = IIf(IsDBNull(XRow("Date")), "", XRow("Date"))
                        'If IsDate(IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))) Then InPay1.ClearingDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))).ToString(_Server_Date_Format_Short) Else InPay1.ClearingDate = IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))
                        InPay1.RefDate = IIf(IsDBNull(XRow("Date")), Nothing, XRow("Date"))
                        InPay1.ClearingDate = IIf(IsDBNull(XRow("Clearing Date")), Nothing, XRow("Clearing Date"))
                        InPay1.RefAmount = ConvertAsDecimal(XRow("Amount"))
                        InPay1.MT_Bank_Id = IIf(IsDBNull(XRow("MT_BANK_ID")), "", XRow("MT_BANK_ID"))
                        InPay1.MT_AccNo = IIf(IsDBNull(XRow("Ref. A/c. No.")), "", XRow("Ref. A/c. No."))
                        InPay1.Status_Action = Status_Action
                        InPay1.RecID = System.Guid.NewGuid().ToString()

                        'If Not Base._Payment_DBOps.InsertPayment(InPay1) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InBankPmt(cnt) = InPay1
                        cnt += 1
                    Next
                End If
                InNewParam.InBankPmt = InBankPmt
                'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment

                'FCRA Insert Process
                If input_Param.Pmt_SplVchrReferenceSelected IsNot Nothing Then
                    Dim SplVchrRefsSplit = input_Param.Pmt_SplVchrReferenceSelected.Split(","c)
                    Dim splitLength = SplVchrRefsSplit.Length

                    If splitLength > 0 Then
                        Dim InsertSplVchrRefs As Parameter_InsertSplVchrRef_Vouchers() = New Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers(splitLength - 1) {}

                        For j As Integer = 0 To splitLength - 1
                            Dim _SplVchr As Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers = New Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers()
                            _SplVchr.Task_Name = SplVchrRefsSplit(j)
                            InsertSplVchrRefs(j) = _SplVchr
                        Next
                        InNewParam.InsertSplVchrRefs = InsertSplVchrRefs
                    End If
                End If

                If Not InsertPayment_Txn(InNewParam) Then
                    ' dbService.Wrap_ExecuteFunction(RealServiceFunctions.Payments_InsertPayment_Txn, BasicParam_2, InNewParam) Then
                    Throw New Exception("|SomeError happened during current operation!!|")
                    Exit Function
                End If
                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.SaveSuccess
                SaveButtonChecksParam.messageCaption = TitleX
                SaveButtonChecksParam.messageIcon = "Information"
                SaveButtonChecksParam.dialogResult = "OK"
                SaveButtonChecksParam.CashbookGridPK = InNewParam.param_InsertMaster.RecID & InNewParam.Insert(0).RecID
                Return SaveButtonChecksParam
            End If

            Dim Message As String = "" : Dim IsLBIncluded As Boolean = False
            Dim EditParam As Param_Txn_Update_VoucherPayment = New Param_Txn_Update_VoucherPayment
            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Edit Then 'edit
                Dim UpMaster As Parameter_UpdateMaster_VoucherPayment = New Parameter_UpdateMaster_VoucherPayment()
                UpMaster.VNo = Txt_V_NO
                If IsDate(Txt_V_Date) Then UpMaster.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else UpMaster.TDate = Txt_V_Date
                'UpMaster.TDate = Txt_V_Date.Text
                UpMaster.InvNo = Txt_Inv_No
                If IsDate(Txt_Inv_Date) Then UpMaster.InvDate = Convert.ToDateTime(Txt_Inv_Date).ToString(cBase._Server_Date_Format_Short) Else UpMaster.InvDate = Txt_Inv_Date
                'UpMaster.InvDate = Me.Txt_Inv_Date.Text
                UpMaster.PartyID = GLookUp_PartyList1_Tag
                UpMaster.SubTotal = ConvertAsDecimal(Txt_SubTotal)
                UpMaster.Cash = ConvertAsDecimal(Txt_CashAmt)
                UpMaster.Bank = ConvertAsDecimal(Txt_BankAmt)
                UpMaster.Advance = ConvertAsDecimal(Txt_AdvAmt)
                UpMaster.Liability = ConvertAsDecimal(Txt_LB_Amt)
                UpMaster.Credit = ConvertAsDecimal(Txt_CreditAmt)
                UpMaster.TDS = ConvertAsDecimal(Txt_TDS_Amt)
                If IsDate(Txt_DueDate) Then UpMaster.CreditDueDate = Convert.ToDateTime(Txt_DueDate).ToString(cBase._Server_Date_Format_Short) Else UpMaster.CreditDueDate = Txt_DueDate
                'UpMaster.CreditDueDate = Me.Txt_DueDate.Text
                'UpMaster.Status_Action = Status_Action
                UpMaster.RecID = Rec_ID

                'If Not Base._Payment_DBOps.UpdateMaster(UpMaster) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.param_UpdateMaster = UpMaster

                'If Not Base._Payment_DBOps.Delete(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_Delete = Rec_ID

                'PROFILE ENTRIES DELETE
                'If Not Base._AdvanceDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_AdvanceDelete = Rec_ID
                'If Not Base._DepositsDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DepostisDelete = Rec_ID
                EditParam.MID_LiabilityDelete = Rec_ID
                'If Not Base._LiabilityDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                'If Not Base._GoldSilverDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_GSDelete = Rec_ID

                'If Not Base._AssetDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_AssetsDelete = Rec_ID

                'If Not Base._LiveStockDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_LivestockDelete = Rec_ID

                'If Not Base._VehicleDBOps.Delete(Me.xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_VehicleDelete = Rec_ID

                EditParam.MID_ReferenceDelete = Rec_ID

                Dim ctr As Integer = 0

                Dim Insert(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertAdvJV(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertCreditJV(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertTDSJV1(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertTDSJV2(Link_DT.Rows.Count) As Parameter_Insert_VoucherPayment
                Dim InsertLiability(Link_DT.Rows.Count) As Parameter_InsertTRID_Liabilities
                Dim InsertAdvances(Link_DT.Rows.Count) As Parameter_InsertTRID_Advances
                Dim InsertDeposits(Link_DT.Rows.Count) As Parameter_InsertTRID_Deposits

                Dim cnt As Integer = 0
                '-------------
                For Each XRow In Link_DT.Rows
                    Dim ID As String = System.Guid.NewGuid().ToString()
                    If ctr = 0 Then xID = ID
                    ctr += 1
                    Dim R_Date As DateTime = Nothing : Dim R_Date_Str As String = "" : If IsDate(XRow("Ref_Date")) Then : R_Date = XRow("Ref_Date") : R_Date_Str = "#" & R_Date.ToString(cBase._Date_Format_Short) & "#" : Else : R_Date_Str = " NULL " : End If
                    Dim C_Date As DateTime = Nothing : Dim C_Date_Str As String = "" : If IsDate(XRow("Ref_CDate")) Then : C_Date = XRow("Ref_CDate") : C_Date_Str = "#" & C_Date.ToString(cBase._Date_Format_Short) & "#" : Else : C_Date_Str = " NULL " : End If
                    Dim Cross_Ref_ID As String = "NULL"
                    Dim ScreenCode As Common_Lib.Common.Voucher_Screen_Code = Common.Voucher_Screen_Code.Payment
                    If XRow("LB_REC_ID").ToString.Length > 0 And Not XRow("Item_Profile").ToString.ToUpper = "LAND & BUILDING" Then
                        Cross_Ref_ID = "'" & XRow("LB_REC_ID") & "'"
                    End If
                    If XRow("REF_REC_ID").ToString.Length > 0 Then
                        Cross_Ref_ID = "'" & XRow("REF_REC_ID") & "'"
                    End If
                    Dim Sub_Cr_Led_ID As String = ""
                    If IIf(IsDBNull(XRow("Mode")), "", XRow("Mode")).ToString.ToUpper <> "CASH" Then
                        Sub_Cr_Led_ID = IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID"))
                    End If

                    Dim InParamVoucherInsert1 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment()
                    InParamVoucherInsert1.TransCode = ScreenCode
                    InParamVoucherInsert1.VNo = Txt_V_NO
                    If IsDate(Txt_V_Date) Then InParamVoucherInsert1.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.TDate = Txt_V_Date
                    'InParam.TDate = Me.Txt_V_Date.Text.Trim()
                    InParamVoucherInsert1.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                    InParamVoucherInsert1.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), Nothing, XRow("Item_Trans_Type"))
                    InParamVoucherInsert1.Cr_Led_ID = IIf(IsDBNull(XRow("Cr_Led_id")), "", XRow("Cr_Led_id"))
                    InParamVoucherInsert1.Dr_Led_ID = IIf(IsDBNull(XRow("Dr_Led_id")), "", XRow("Dr_Led_id"))
                    InParamVoucherInsert1.SUB_Cr_Led_ID = Sub_Cr_Led_ID
                    InParamVoucherInsert1.SUB_Dr_Led_ID = ""
                    InParamVoucherInsert1.Amount = ConvertAsDecimal(XRow("Amount"))
                    InParamVoucherInsert1.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                    InParamVoucherInsert1.Ref_BANK_ID = ""
                    InParamVoucherInsert1.Ref_Branch = ""
                    InParamVoucherInsert1.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                    If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsert1.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                    If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsert1.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsert1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                    'InParam.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                    'InParam.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                    InParamVoucherInsert1.Party1 = GLookUp_PartyList1_Tag
                    InParamVoucherInsert1.Narration = Txt_Narration
                    InParamVoucherInsert1.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                    InParamVoucherInsert1.Reference = Txt_Reference
                    InParamVoucherInsert1.CrossRefID = Cross_Ref_ID
                    InParamVoucherInsert1.MasterTxnID = Rec_ID
                    InParamVoucherInsert1.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                    InParamVoucherInsert1.Status_Action = Status_Action
                    InParamVoucherInsert1.RecID = Guid.NewGuid.ToString()

                    'If Not Base._Payment_DBOps.Insert(InParam) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    Insert(cnt) = InParamVoucherInsert1

                    If XRow("Mode") = "ADVANCE" Then 'JV Entry
                        ' Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim Amount As Decimal = ConvertAsDecimal(XRow("Amount"))
                        Dim InParamVoucherInsertAdv As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertAdv.TransCode = ScreenCode
                        InParamVoucherInsertAdv.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertAdv.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.TDate = Txt_V_Date
                        'InParam1.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertAdv.ItemID = IIf(IsDBNull(XRow("OFFSET_ID")), "", XRow("OFFSET_ID"))
                        InParamVoucherInsertAdv.Type = IIf(IsDBNull(XRow("JV_Trans_Type")), "", XRow("JV_Trans_Type"))
                        InParamVoucherInsertAdv.Cr_Led_ID = IIf(IsDBNull(XRow("JV_Cr_Led_id")), "", XRow("JV_Cr_Led_id"))
                        InParamVoucherInsertAdv.Dr_Led_ID = IIf(IsDBNull(XRow("JV_Dr_Led_id")), "", XRow("JV_Dr_Led_id"))
                        InParamVoucherInsertAdv.SUB_Cr_Led_ID = ""
                        InParamVoucherInsertAdv.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertAdv.Amount = Amount
                        InParamVoucherInsertAdv.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertAdv.Ref_BANK_ID = ""
                        InParamVoucherInsertAdv.Ref_Branch = ""
                        InParamVoucherInsertAdv.Ref_No = ""

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertAdv.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertAdv.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertAdv.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParam1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParam1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertAdv.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertAdv.Narration = Txt_Narration
                        InParamVoucherInsertAdv.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertAdv.Reference = Txt_Reference
                        InParamVoucherInsertAdv.CrossRefID = "" 'Cross_Ref_ID'# bug 4082
                        InParamVoucherInsertAdv.MasterTxnID = Rec_ID
                        InParamVoucherInsertAdv.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertAdv.Status_Action = Status_Action
                        InParamVoucherInsertAdv.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParam1) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertAdvJV(cnt) = InParamVoucherInsertAdv
                    End If
                    If XRow("Mode") = "CREDIT" Then 'JV Entry
                        ' Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim InParamVoucherInsertCrJV As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertCrJV.TransCode = ScreenCode
                        InParamVoucherInsertCrJV.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertCrJV.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCrJV.TDate = Txt_V_Date
                        'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertCrJV.ItemID = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                        InParamVoucherInsertCrJV.Type = IIf(IsDBNull(XRow("JV_Trans_Type")), "", XRow("JV_Trans_Type"))
                        InParamVoucherInsertCrJV.Cr_Led_ID = IIf(IsDBNull(XRow("JV_Cr_Led_id")), "", XRow("JV_Cr_Led_id"))
                        InParamVoucherInsertCrJV.Dr_Led_ID = IIf(IsDBNull(XRow("JV_Dr_Led_id")), "", XRow("JV_Dr_Led_id"))
                        InParamVoucherInsertCrJV.SUB_Cr_Led_ID = IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID"))
                        InParamVoucherInsertCrJV.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertCrJV.Amount = ConvertAsDecimal(XRow("Amount"))
                        InParamVoucherInsertCrJV.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertCrJV.Ref_BANK_ID = ""
                        InParamVoucherInsertCrJV.Ref_Branch = ""
                        InParamVoucherInsertCrJV.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertCrJV.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCrJV.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertCrJV.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertCrJV.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParams.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParams.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertCrJV.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertCrJV.Narration = Txt_Narration
                        InParamVoucherInsertCrJV.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertCrJV.Reference = Txt_Reference
                        InParamVoucherInsertCrJV.CrossRefID = "" 'Cross_Ref_ID'# bug 4082
                        InParamVoucherInsertCrJV.MasterTxnID = Rec_ID
                        InParamVoucherInsertCrJV.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertCrJV.Status_Action = Status_Action
                        InParamVoucherInsertCrJV.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    'Base._Payment_DBOps.DeleteMaster(Me.xMID.Text)
                        '    'Base._Payment_DBOps.Delete(Me.xMID.Text)
                        '    Exit Sub
                        'End If
                        InsertCreditJV(cnt) = InParamVoucherInsertCrJV
                    End If

                    If Val(XRow("TDS")) > 0 Then 'TDS JV Entry
                        'Me.xID.Text = System.Guid.NewGuid().ToString()
                        Dim Dr_Led_ID As String = ""
                        If Not IsDBNull(TDS_DT.Rows(0)("JV_Dr_Led_id")) Then Dr_Led_ID = TDS_DT.Rows(0)("JV_Dr_Led_id")
                        Dim Cr_Led_ID As String = ""
                        If Not IsDBNull(TDS_DT.Rows(0)("JV_Cr_Led_id")) Then Cr_Led_ID = TDS_DT.Rows(0)("JV_Cr_Led_id")
                        If Dr_Led_ID.Length > 0 Then
                            Cr_Led_ID = XRow("Cr_Led_id").ToString
                        Else
                            Dr_Led_ID = XRow("Dr_Led_id").ToString
                        End If
                        Dim InParamVoucherInsertTDSJV1 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertTDSJV1.TransCode = ScreenCode
                        InParamVoucherInsertTDSJV1.VNo = Txt_V_NO
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertTDSJV1.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.TDate = Txt_V_Date
                        'InParam1.TDate = Me.Txt_V_Date.Text.Trim()
                        InParamVoucherInsertTDSJV1.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InParamVoucherInsertTDSJV1.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), Nothing, XRow("Item_Trans_Type"))
                        InParamVoucherInsertTDSJV1.Cr_Led_ID = ""
                        InParamVoucherInsertTDSJV1.Dr_Led_ID = Dr_Led_ID
                        InParamVoucherInsertTDSJV1.SUB_Cr_Led_ID = "" ' IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID")) Bug 4440
                        InParamVoucherInsertTDSJV1.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV1.Amount = ConvertAsDecimal(XRow("TDS"))
                        InParamVoucherInsertTDSJV1.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertTDSJV1.Ref_BANK_ID = ""
                        InParamVoucherInsertTDSJV1.Ref_Branch = ""
                        InParamVoucherInsertTDSJV1.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))

                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertTDSJV1.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertTDSJV1.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParam1.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParam1.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertTDSJV1.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertTDSJV1.Narration = Txt_Narration
                        InParamVoucherInsertTDSJV1.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertTDSJV1.Reference = Txt_Reference
                        InParamVoucherInsertTDSJV1.CrossRefID = Cross_Ref_ID '# bug 4082
                        InParamVoucherInsertTDSJV1.MasterTxnID = Rec_ID
                        InParamVoucherInsertTDSJV1.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertTDSJV1.Status_Action = Status_Action
                        InParamVoucherInsertTDSJV1.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParam1) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertTDSJV1(cnt) = InParamVoucherInsertTDSJV1

                        Dim InParamVoucherInsertTDSJV2 As Parameter_Insert_VoucherPayment = New Parameter_Insert_VoucherPayment
                        InParamVoucherInsertTDSJV2.TransCode = ScreenCode
                        InParamVoucherInsertTDSJV2.VNo = Txt_V_NO
                        'InParams.TDate = Me.Txt_V_Date.Text.Trim()
                        If IsDate(Txt_V_Date) Then InParamVoucherInsertTDSJV2.TDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.TDate = Txt_V_Date
                        InParamVoucherInsertTDSJV2.ItemID = TDS_DT.Rows(0)("ITEM_ID")
                        InParamVoucherInsertTDSJV2.Type = TDS_DT.Rows(0)("JV_Trans_Type")
                        InParamVoucherInsertTDSJV2.Cr_Led_ID = Cr_Led_ID
                        InParamVoucherInsertTDSJV2.Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV2.SUB_Cr_Led_ID = "" 'IIf(IsDBNull(XRow("Ref_ID")), "", XRow("Ref_ID")) Bug 4440
                        InParamVoucherInsertTDSJV2.SUB_Dr_Led_ID = ""
                        InParamVoucherInsertTDSJV2.Amount = ConvertAsDecimal(XRow("TDS"))
                        InParamVoucherInsertTDSJV2.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InParamVoucherInsertTDSJV2.Ref_BANK_ID = ""
                        InParamVoucherInsertTDSJV2.Ref_Branch = ""
                        InParamVoucherInsertTDSJV2.Ref_No = IIf(IsDBNull(XRow("Ref_No")), "", XRow("Ref_No"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))) Then InParamVoucherInsertTDSJV2.Ref_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        If IsDate(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))) Then InParamVoucherInsertTDSJV2.Ref_CDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))).ToString(cBase._Server_Date_Format_Short) Else InParamVoucherInsertTDSJV2.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        'InParams.Ref_Date = IIf(IsDBNull(XRow("Ref_Date")), "", XRow("Ref_Date"))
                        'InParams.Ref_CDate = IIf(IsDBNull(XRow("Ref_CDate")), "", XRow("Ref_CDate"))
                        InParamVoucherInsertTDSJV2.Party1 = GLookUp_PartyList1_Tag
                        InParamVoucherInsertTDSJV2.Narration = Txt_Narration
                        InParamVoucherInsertTDSJV2.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InParamVoucherInsertTDSJV2.Reference = Txt_Reference
                        InParamVoucherInsertTDSJV2.CrossRefID = "" 'Cross_Ref_ID'# bug 4082
                        InParamVoucherInsertTDSJV2.MasterTxnID = Rec_ID
                        InParamVoucherInsertTDSJV2.SrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InParamVoucherInsertTDSJV2.Status_Action = Status_Action
                        InParamVoucherInsertTDSJV2.RecID = Guid.NewGuid.ToString()

                        'If Not Base._Payment_DBOps.Insert(InParams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertTDSJV2(cnt) = InParamVoucherInsertTDSJV2
                    End If


                    If XRow("Mode") = "CREDIT" Then ''Profile : Other Liabilities Entry
                        'Dim _basic As New Basic_Param()
                        '_basic.PCID = Basic_Param.PCID : _basic.version = Basic_Param.version : _basic.openCenID = Basic_Param.openCenID : _basic.openUserID = Basic_Param.openUserID : _basic.openYearID = Basic_Param.openYearID : _basic.screen = ClientScreen.Accounts_Vouchers

                        Dim _liabTbl As DataTable = cBase._LiabilityDBOps.GetList(ClientScreen.Accounts_Vouchers, Rec_ID)
                        'Liabilities.GetList_Common(_basic, Rec_ID)

                        Dim Created_Liab_ID As Object = Nothing
                        If _liabTbl.Rows.Count > 0 Then Created_Liab_ID = _liabTbl.Rows(0)(0)

                        Dim InLbty As Parameter_InsertTRID_Liabilities = New Parameter_InsertTRID_Liabilities()
                        InLbty.ItemID = "faa69e85-8e55-4b2f-9cbf-207cec96ba14"
                        InLbty.PartyID = GLookUp_PartyList1_Tag
                        If IsDate(Txt_V_Date) Then InLbty.LiabilityDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InLbty.LiabilityDate = Txt_V_Date
                        'InLbty.LiabilityDate = Me.Txt_V_Date.Text.Trim()
                        If IsDate(Txt_DueDate) Then InLbty.PaymentDate = Convert.ToDateTime(Txt_DueDate).ToString(cBase._Server_Date_Format_Short) Else InLbty.PaymentDate = Txt_DueDate
                        'InLbty.PaymentDate = Txt_DueDate.Text.Trim()
                        InLbty.Amount = Val(XRow("Amount"))
                        InLbty.Purpose = Txt_Narration
                        InLbty.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InLbty.TxnID = Rec_ID
                        InLbty.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InLbty.UpdLiabID = Created_Liab_ID ' IIf(IsDBNull(XRow("OLD_CREATION_PROF_REC_ID")), Nothing, XRow("OLD_CREATION_PROF_REC_ID"))
                        InLbty.Status_Action = Status_Action
                        InLbty.RecID = System.Guid.NewGuid().ToString()
                        InLbty.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertLiability(InLbty) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertLiability(cnt) = InLbty
                    End If
                    If XRow("Item_Profile") = "ADVANCES" Then 'Profile : Advance Entry
                        Dim Amount As Double = Val(XRow("Amount"))
                        If Not XRow("TDS") Is Nothing Then
                            Amount = Amount + Val(XRow("TDS"))
                        End If
                        Dim InAdv As Parameter_InsertTRID_Advances = New Parameter_InsertTRID_Advances()
                        InAdv.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InAdv.PartyID = GLookUp_PartyList1_Tag
                        If IsDate(Txt_V_Date) Then InAdv.AdvanceDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InAdv.AdvanceDate = Txt_V_Date
                        'InAdv.AdvanceDate = Me.Txt_V_Date.Text.Trim()
                        InAdv.Amount = Amount 'Bug#4109 'Val(XRow("Amount"))
                        InAdv.Purpose = Txt_Narration
                        InAdv.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InAdv.TxnID = Rec_ID
                        InAdv.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InAdv.UpdAdvID = IIf(IsDBNull(XRow("OLD_CREATION_PROF_REC_ID")), Nothing, XRow("OLD_CREATION_PROF_REC_ID"))
                        InAdv.Status_Action = Status_Action
                        InAdv.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertAdvances(InAdv) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertAdvances(cnt) = InAdv
                    End If
                    If XRow("Item_Profile") = "OTHER DEPOSITS" Then 'Profile : Deposit Entry
                        Dim InDept As Parameter_InsertTRID_Deposits = New Parameter_InsertTRID_Deposits()
                        InDept.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                        InDept.AgainstInsurance = "NO"
                        InDept.PartyID = GLookUp_PartyList1_Tag
                        InDept.InsCompanyMiscID = ""
                        If IsDate(Txt_V_Date) Then InDept.DepositDate = Convert.ToDateTime(Txt_V_Date).ToString(cBase._Server_Date_Format_Short) Else InDept.DepositDate = Txt_V_Date
                        'InDeposit.DepositDate = Txt_V_Date.Text
                        InDept.DepositPeriod = 0
                        InDept.Amount = Val(XRow("Amount"))
                        InDept.InterestRate = 0
                        InDept.Purpose = Txt_Narration
                        InDept.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                        InDept.TxnID = Rec_ID
                        InDept.TxnSrNo = IIf(IsDBNull(XRow("SrNo")), "", XRow("SrNo"))
                        InDept.UpdDepID = IIf(IsDBNull(XRow("OLD_CREATION_PROF_REC_ID")), Nothing, XRow("OLD_CREATION_PROF_REC_ID"))
                        InDept.Status_Action = Status_Action
                        InDept.RecID = System.Guid.NewGuid().ToString()
                        InDept.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._Payment_DBOps.InsertDeposits(InDeposit) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertDeposits(cnt) = InDept
                    End If
                    cnt += 1
                Next

                EditParam.Insert = Insert
                EditParam.InsertCreditJV = InsertCreditJV
                EditParam.InsertAdvanceJV = InsertAdvJV
                EditParam.InsertTDSJV1 = InsertTDSJV1
                EditParam.InsertTDSJV2 = InsertTDSJV2
                EditParam.InsertAdvances = InsertAdvances
                EditParam.InsertDeposits = InsertDeposits
                EditParam.InsertLiability = InsertLiability

                EditParam.MID_DeleteItems = Rec_ID
                'If Not Base._Payment_DBOps.DeleteItems(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                EditParam.MID_DeletePurpose = Rec_ID
                'If Not Base._Payment_DBOps.DeletePurpose(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeleteOther = Rec_ID
                '  BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment

                Dim d1 As DataTable = cBase._L_B_DBOps.GetIDsBytxnID(Rec_ID, ClientScreen.Accounts_Voucher_Payment)
                Dim DelExtInfo(d1.Rows.Count - 1) As String
                Dim DelDocInfo(d1.Rows.Count - 1) As String
                Dim DelByLB(d1.Rows.Count - 1) As String
                Dim DelComplexBuildings(d1.Rows.Count - 1) As String

                Dim cntDel As Integer = 0

                For Each cRow As DataRow In d1.Rows
                    DelComplexBuildings(cntDel) = cRow(0)
                    'Remove existing Extensions
                    'If Not Base._L_B_DBOps.DeleteExtendedInfo(cRow(0)) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelExtInfo(cntDel) = cRow(0)
                    ''Remove existing docs
                    'If Not Base._L_B_DBOps.DeleteDocumentInfo(cRow(0)) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelDocInfo(cntDel) = cRow(0)
                    ''Remove Location
                    'If Not Base._AssetLocDBOps.DeleteByLB(cRow(0), ClientScreen.Accounts_Voucher_Payment) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelByLB(cntDel) = cRow(0)

                    cntDel += 1
                Next
                EditParam.DeleteComplexBuilding = DelComplexBuildings
                EditParam.DeleteDocumentInfo = DelDocInfo
                EditParam.DeleteExtendedInfo = DelExtInfo
                EditParam.DeleteByLB = DelByLB

                'Remove existing Land_Building_Info
                'If Not Base._L_B_DBOps.Delete(Me.xMID.Text, ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeleteLB = Rec_ID

                Dim InsertItem(DT.Rows.Count) As Parameter_InsertItem_VoucherPayment
                Dim InsertPurpose(DT.Rows.Count) As Parameter_InsertPurpose_VoucherPayment
                Dim InsertOthers(DT.Rows.Count) As Parameter_InsertOther_VoucherPayment
                Dim InsertGS(DT.Rows.Count) As Parameter_InsertTRIDAndTRSRNo_GoldSilver
                Dim InsertAssets(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_Assets
                Dim InsertLS(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_LiveStock
                Dim InsertVehicles(DT.Rows.Count) As Parameter_InsertTRIDAndTRSrNo_Vehicles
                Dim InsertProperty(DT.Rows.Count) As Parameter_InsertMasterIDAndSrNo_LandAndBuilding
                Dim InsertReferencesWIP(DT.Rows.Count) As Param_InsertTRIDAndTRSrNo_WIP_Profile
                'Dim UpComplexbuilding(DT.Rows.Count) As String

                Dim cntMI As Integer = 0

                'Main Items
                For Each XRow In DT.Rows
                    Dim InItem As Parameter_InsertItem_VoucherPayment = New Parameter_InsertItem_VoucherPayment()
                    InItem.Txn_M_ID = Rec_ID
                    InItem.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                    InItem.ItemID = IIf(IsDBNull(XRow("Item_ID")), "", XRow("Item_ID"))
                    InItem.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), "", XRow("Item_Led_ID"))
                    InItem.Type = IIf(IsDBNull(XRow("Item_Trans_Type")), "", XRow("Item_Trans_Type"))
                    InItem.PartyReq = IIf(IsDBNull(XRow("Item_Party_Req")), "", XRow("Item_Party_Req"))
                    InItem.Profile = IIf(IsDBNull(XRow("Item_Profile")), "", XRow("Item_Profile"))
                    InItem.ItemName = IIf(IsDBNull(XRow("Item Name")), "", XRow("Item Name"))
                    InItem.Head = IIf(IsDBNull(XRow("Head")), "", XRow("Head"))
                    InItem.Qty = Val(XRow("Qty."))
                    InItem.Unit = IIf(IsDBNull(XRow("Unit")), "", XRow("Unit"))
                    InItem.Rate = Val(XRow("Rate"))
                    InItem.Amount = ConvertAsDecimal(XRow("Amount"))
                    InItem.Remarks = IIf(IsDBNull(XRow("Remarks")), "", XRow("Remarks"))
                    InItem.TDS = IIf(IsDBNull(XRow("TDS")), "", XRow("TDS"))
                    InItem.Status_Action = Status_Action
                    InItem.RecID = System.Guid.NewGuid().ToString()

                    'If Not Base._Payment_DBOps.InsertItem(InItem) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertItem(cntMI) = InItem

                    'Purpose.........
                    Dim InPurpose As Parameter_InsertPurpose_VoucherPayment = New Parameter_InsertPurpose_VoucherPayment()
                    InPurpose.TxnID = Rec_ID
                    InPurpose.PurposeID = IIf(IsDBNull(XRow("PUR_ID")), "", XRow("PUR_ID"))
                    InPurpose.Amount = ConvertAsDecimal(XRow("Amount"))
                    InPurpose.SrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                    InPurpose.Status_Action = Status_Action
                    InPurpose.RecID = System.Guid.NewGuid().ToString()
                    'If Not Base._Payment_DBOps.InsertPurpose(InPurpose) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    InsertPurpose(cntMI) = InPurpose

                    If XRow("Item_Profile") = "TELEPHONE BILL" Then
                        Dim InOthers As Parameter_InsertOther_VoucherPayment = New Parameter_InsertOther_VoucherPayment
                        InOthers.TxnMID = Rec_ID
                        InOthers.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                        InOthers.Ref_ID = IIf(IsDBNull(XRow("TP_ID")), Nothing, XRow("TP_ID"))
                        InOthers.Ref_Bill_No = IIf(IsDBNull(XRow("TP_BILL_NO")), Nothing, XRow("TP_BILL_NO"))
                        'If IsDate(IIf(IsDBNull(XRow("TP_BILL_DATE")), Nothing, XRow("TP_BILL_DATE"))) Then InOthers.Ref_Bill_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("TP_BILL_DATE")), Nothing, XRow("TP_BILL_DATE"))).ToString(_Server_Date_Format_Short) Else InOthers.Ref_Bill_Date = IIf(IsDBNull(XRow("TP_BILL_DATE")), Nothing, XRow("TP_BILL_DATE"))
                        'If IsDate(IIf(IsDBNull(XRow("TP_PERIOD_FROM")), Nothing, XRow("TP_PERIOD_FROM"))) Then InOthers.Period_From = Convert.ToDateTime(IIf(IsDBNull(XRow("TP_PERIOD_FROM")), Nothing, XRow("TP_PERIOD_FROM"))).ToString(_Server_Date_Format_Short) Else InOthers.Period_From = IIf(IsDBNull(XRow("TP_PERIOD_FROM")), Nothing, XRow("TP_PERIOD_FROM"))
                        'If IsDate(IIf(IsDBNull(XRow("TP_PERIOD_TO")), Nothing, XRow("TP_PERIOD_TO"))) Then InOthers.Period_To = Convert.ToDateTime(IIf(IsDBNull(XRow("TP_PERIOD_TO")), Nothing, XRow("TP_PERIOD_TO"))).ToString(_Server_Date_Format_Short) Else InOthers.Period_To = IIf(IsDBNull(XRow("TP_PERIOD_TO")), Nothing, XRow("TP_PERIOD_TO"))
                        InOthers.Ref_Bill_Date = IIf(IsDBNull(XRow("TP_BILL_DATE")), Nothing, XRow("TP_BILL_DATE"))
                        InOthers.Period_From = IIf(IsDBNull(XRow("TP_PERIOD_FROM")), Nothing, XRow("TP_PERIOD_FROM"))
                        InOthers.Period_To = IIf(IsDBNull(XRow("TP_PERIOD_TO")), Nothing, XRow("TP_PERIOD_TO"))
                        'If Not Base._Payment_DBOps.InsertOther(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertOthers(cntMI) = InOthers
                    End If

                    If XRow("Item_Profile") = "GOLD" Or XRow("Item_Profile") = "SILVER" Then
                        Dim InGoldSilver As Parameter_InsertTRIDAndTRSRNo_GoldSilver = New Parameter_InsertTRIDAndTRSRNo_GoldSilver
                        InGoldSilver.Type = IIf(IsDBNull(XRow("Item_Profile")), Nothing, XRow("Item_Profile"))
                        InGoldSilver.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InGoldSilver.DescMiscID = IIf(IsDBNull(XRow("GS_DESC_MISC_ID")), Nothing, IIf(Len(XRow("GS_DESC_MISC_ID")) = 0, Nothing, XRow("GS_DESC_MISC_ID")))
                        InGoldSilver.Weight = IIf(IsDBNull(XRow("GS_ITEM_WEIGHT")), Nothing, Val(XRow("GS_ITEM_WEIGHT")))
                        InGoldSilver.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InGoldSilver.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InGoldSilver.TxnID = Rec_ID
                        InGoldSilver.TxnSrno = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                        InGoldSilver.UpdGsID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                        InGoldSilver.Amount = Val(XRow("Amount")) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InGoldSilver.Status_Action = Status_Action
                        InGoldSilver.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._GoldSilverDBOps.Insert(InParams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertGS(cntMI) = InGoldSilver
                    End If

                    If XRow("Item_Profile") = "OTHER ASSETS" Then
                        Dim InAssets As Parameter_InsertTRIDAndTRSrNo_Assets = New Parameter_InsertTRIDAndTRSrNo_Assets
                        InAssets.AssetType = IIf(IsDBNull(XRow("AI_TYPE")), Nothing, XRow("AI_TYPE"))
                        InAssets.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InAssets.Make = IIf(IsDBNull(XRow("AI_MAKE")), Nothing, XRow("AI_MAKE"))
                        InAssets.Model = IIf(IsDBNull(XRow("AI_MODEL")), Nothing, XRow("AI_MODEL"))
                        InAssets.SrNo = IIf(IsDBNull(XRow("AI_SERIAL_NO")), Nothing, XRow("AI_SERIAL_NO"))
                        InAssets.Rate = IIf(IsDBNull(XRow("Rate")), Nothing, Val(XRow("Rate")))
                        InAssets.InsAmount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        'If IsDate(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))) Then InAssets.PurchaseDate = Convert.ToDateTime(IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))).ToString(_Server_Date_Format_Short) Else InAssets.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                        InAssets.PurchaseDate = IIf(IsDBNull(XRow("AI_PUR_DATE")), Nothing, XRow("AI_PUR_DATE"))
                        InAssets.PurchaseAmount = IIf(InAssets.AssetType.ToUpper = "ASSET", IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")), 0) 'Bug #5046 fix, http://pm.bkinfo.in/issues/5345#note-12
                        InAssets.Warranty = IIf(IsDBNull(XRow("AI_WARRANTY")), Nothing, Val(XRow("AI_WARRANTY")))
                        InAssets.Image = IIf(IsDBNull(XRow("AI_IMAGE")), Nothing, XRow("AI_IMAGE"))
                        InAssets.Quantity = IIf(IsDBNull(XRow("Qty.")), Nothing, Val(XRow("Qty.")))
                        InAssets.LocationId = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InAssets.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InAssets.TxnID = Rec_ID
                        InAssets.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InAssets.UpdAsstID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                        InAssets.Status_Action = Status_Action
                        InAssets.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._AssetDBOps.Insert(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertAssets(cntMI) = InAssets
                    End If

                    If XRow("Item_Profile") = "LIVESTOCK" Then
                        Dim InLiveStock As Parameter_InsertTRIDAndTRSrNo_LiveStock = New Parameter_InsertTRIDAndTRSrNo_LiveStock
                        InLiveStock.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InLiveStock.Name = IIf(IsDBNull(XRow("LS_NAME")), Nothing, XRow("LS_NAME"))
                        InLiveStock.Year = IIf(IsDBNull(XRow("LS_BIRTH_YEAR")), Nothing, XRow("LS_BIRTH_YEAR"))
                        InLiveStock.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InLiveStock.Insurance = IIf(IsDBNull(XRow("LS_INSURANCE")), Nothing, XRow("LS_INSURANCE"))
                        InLiveStock.InsuranceID = IIf(IsDBNull(XRow("LS_INSURANCE_ID")), Nothing, XRow("LS_INSURANCE_ID"))
                        InLiveStock.PolicyNo = IIf(IsDBNull(XRow("LS_INS_POLICY_NO")), Nothing, XRow("LS_INS_POLICY_NO"))
                        InLiveStock.InsAmount = IIf(IsDBNull(XRow("LS_INS_AMT")), Nothing, Val(XRow("LS_INS_AMT")))
                        'If IsDate(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))) Then InLiveStock.InsuranceDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))).ToString(_Server_Date_Format_Short) Else InLiveStock.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                        InLiveStock.InsuranceDate = IIf(IsDBNull(XRow("LS_INS_DATE")), Nothing, XRow("LS_INS_DATE"))
                        InLiveStock.LocationID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InLiveStock.OtherDetails = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InLiveStock.TxnID = Rec_ID
                        InLiveStock.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, CInt(XRow("Sr.")))
                        InLiveStock.UpdLsID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                        InLiveStock.Status_Action = Status_Action
                        InLiveStock.screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._LiveStockDBOps.Insert(InParams) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertLS(cntMI) = InLiveStock
                    End If
                    If XRow("Item_Profile") = "VEHICLES" Then
                        Dim InPrms As Parameter_InsertTRIDAndTRSrNo_Vehicles = New Parameter_InsertTRIDAndTRSrNo_Vehicles()

                        InPrms.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InPrms.Make = IIf(IsDBNull(XRow("VI_MAKE")), Nothing, XRow("VI_MAKE"))
                        InPrms.Model = IIf(IsDBNull(XRow("VI_MODEL")), Nothing, XRow("VI_MODEL"))
                        InPrms.Reg_No_Pattern = IIf(IsDBNull(XRow("VI_REG_NO_PATTERN")), Nothing, XRow("VI_REG_NO_PATTERN"))
                        InPrms.Reg_No = IIf(IsDBNull(XRow("VI_REG_NO")), Nothing, XRow("VI_REG_NO"))
                        'If IsDate(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))) Then InPrms.RegDate = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))).ToString(_Server_Date_Format_Short) Else InPrms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                        InPrms.RegDate = IIf(IsDBNull(XRow("VI_REG_DATE")), Nothing, XRow("VI_REG_DATE"))
                        InPrms.Ownership = IIf(IsDBNull(XRow("VI_OWNERSHIP")), Nothing, XRow("VI_OWNERSHIP"))
                        InPrms.Ownership_AB_ID = IIf(IsDBNull(XRow("VI_OWNERSHIP_AB_ID")), Nothing, XRow("VI_OWNERSHIP_AB_ID"))
                        If Not InPrms.Ownership_AB_ID Is Nothing Then
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
                        'If IsDate(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))) Then InPrms.Ins_Expiry_Date = Convert.ToDateTime(IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))).ToString(_Server_Date_Format_Short) Else InPrms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                        InPrms.Ins_Expiry_Date = IIf(IsDBNull(XRow("VI_INS_EXPIRY_DATE")), Nothing, XRow("VI_INS_EXPIRY_DATE"))
                        InPrms.Location_ID = IIf(IsDBNull(XRow("LOC_ID")), Nothing, XRow("LOC_ID"))
                        InPrms.Other_Details = IIf(IsDBNull(XRow("Remarks")), Nothing, XRow("Remarks"))
                        InPrms.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InPrms.TxnID = Rec_ID
                        InPrms.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InPrms.UpdVehID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                        InPrms.Status_Action = Status_Action
                        InPrms.Screen = ClientScreen.Accounts_Voucher_Payment

                        'If Not Base._VehicleDBOps.Insert(InPrms) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InsertVehicles(cntMI) = InPrms
                    End If


                    If XRow("Item_Profile") = "LAND & BUILDING" Then
                        Dim PartyID As String = Nothing
                        If XRow("LB_OWNERSHIP_PARTY_ID").ToString.Length = 36 Then PartyID = "'" & XRow("LB_OWNERSHIP_PARTY_ID") & "'"
                        Dim InLandAndBuild As Parameter_InsertMasterIDAndSrNo_LandAndBuilding = New Parameter_InsertMasterIDAndSrNo_LandAndBuilding
                        InLandAndBuild.ItemID = IIf(IsDBNull(XRow("Item_ID")), Nothing, XRow("Item_ID"))
                        InLandAndBuild.PropertyType = IIf(IsDBNull(XRow("LB_PRO_TYPE")), Nothing, XRow("LB_PRO_TYPE"))
                        InLandAndBuild.Category = IIf(IsDBNull(XRow("LB_PRO_CATEGORY")), Nothing, XRow("LB_PRO_CATEGORY"))
                        InLandAndBuild.Use = IIf(IsDBNull(XRow("LB_PRO_USE")), Nothing, XRow("LB_PRO_USE"))
                        InLandAndBuild.Name = IIf(IsDBNull(XRow("LB_PRO_NAME")), Nothing, Trim(XRow("LB_PRO_NAME")))
                        InLandAndBuild.Address = IIf(IsDBNull(XRow("LB_PRO_ADDRESS")), Nothing, XRow("LB_PRO_ADDRESS"))
                        InLandAndBuild.LB_Add1 = IIf(IsDBNull(XRow("LB_ADDRESS1")), Nothing, XRow("LB_ADDRESS1"))
                        InLandAndBuild.LB_Add2 = IIf(IsDBNull(XRow("LB_ADDRESS2")), Nothing, XRow("LB_ADDRESS2"))
                        InLandAndBuild.LB_Add3 = IIf(IsDBNull(XRow("LB_ADDRESS3")), Nothing, XRow("LB_ADDRESS3"))
                        InLandAndBuild.LB_Add4 = IIf(IsDBNull(XRow("LB_ADDRESS4")), Nothing, XRow("LB_ADDRESS4"))
                        InLandAndBuild.LB_CityID = IIf(IsDBNull(XRow("LB_CITY_ID")), Nothing, XRow("LB_CITY_ID"))
                        InLandAndBuild.LB_CountryID = "f9970249-121c-4b8f-86f9-2b53e850809e"
                        InLandAndBuild.LB_DisttID = IIf(IsDBNull(XRow("LB_DISTRICT_ID")), Nothing, XRow("LB_DISTRICT_ID"))
                        InLandAndBuild.LB_PinCode = IIf(IsDBNull(XRow("LB_PINCODE")), Nothing, XRow("LB_PINCODE"))
                        InLandAndBuild.LB_StateID = IIf(IsDBNull(XRow("LB_STATE_ID")), Nothing, XRow("LB_STATE_ID"))
                        InLandAndBuild.Ownership = IIf(IsDBNull(XRow("LB_OWNERSHIP")), Nothing, XRow("LB_OWNERSHIP"))
                        InLandAndBuild.Owner_Party_ID = PartyID
                        InLandAndBuild.SurveyNo = IIf(IsDBNull(XRow("LB_SURVEY_NO")), Nothing, XRow("LB_SURVEY_NO"))
                        InLandAndBuild.TotalArea = IIf(IsDBNull(XRow("LB_TOT_P_AREA")), Nothing, XRow("LB_TOT_P_AREA"))
                        InLandAndBuild.ConstructedArea = IIf(IsDBNull(XRow("LB_CON_AREA")), Nothing, XRow("LB_CON_AREA"))
                        InLandAndBuild.ConstructionYear = IIf(IsDBNull(XRow("LB_CON_YEAR")), Nothing, XRow("LB_CON_YEAR"))
                        InLandAndBuild.RCCRoof = IIf(IsDBNull(XRow("LB_RCC_ROOF")), Nothing, XRow("LB_RCC_ROOF"))
                        InLandAndBuild.DepositAmount = IIf(IsDBNull(XRow("LB_DEPOSIT_AMT")), Nothing, XRow("LB_DEPOSIT_AMT"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))) Then InLandAndBuild.PaymentDate = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                        InLandAndBuild.PaymentDate = IIf(IsDBNull(XRow("LB_PAID_DATE")), Nothing, XRow("LB_PAID_DATE"))
                        InLandAndBuild.MonthlyRent = IIf(IsDBNull(XRow("LB_MONTH_RENT")), Nothing, XRow("LB_MONTH_RENT"))
                        InLandAndBuild.MonthlyOtherExpenses = IIf(IsDBNull(XRow("LB_MONTH_O_PAYMENTS")), Nothing, XRow("LB_MONTH_O_PAYMENTS"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))) Then InLandAndBuild.PeriodFrom = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                        InLandAndBuild.PeriodFrom = IIf(IsDBNull(XRow("LB_PERIOD_FROM")), Nothing, XRow("LB_PERIOD_FROM"))
                        'If IsDate(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))) Then InLandAndBuild.PeriodTo = Convert.ToDateTime(IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))).ToString(_Server_Date_Format_Short) Else InLandAndBuild.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                        InLandAndBuild.PeriodTo = IIf(IsDBNull(XRow("LB_PERIOD_TO")), Nothing, XRow("LB_PERIOD_TO"))
                        InLandAndBuild.OtherDocs = IIf(IsDBNull(XRow("LB_DOC_OTHERS")), Nothing, XRow("LB_DOC_OTHERS"))
                        InLandAndBuild.DocNames = IIf(IsDBNull(XRow("LB_DOC_NAME")), Nothing, XRow("LB_DOC_NAME"))
                        InLandAndBuild.Value = IIf(IsDBNull(XRow("Amount")), Nothing, Val(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS")) 'Bug #5046 fix
                        InLandAndBuild.OtherDetails = IIf(IsDBNull(XRow("LB_OTHER_DETAIL")), Nothing, XRow("LB_OTHER_DETAIL"))
                        InLandAndBuild.MasterID = Rec_ID
                        InLandAndBuild.SrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                        InLandAndBuild.UpdLbID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                        InLandAndBuild.Status_Action = Status_Action
                        InLandAndBuild.RecID = IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID"))

                        'If Not Base._L_B_DBOps.Insert(InParam) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If

                        'EXTENSIONS 
                        Dim cntExt As Integer = 0
                        Dim ExtInfo() As Parameter_InsertExtendedInfo_LandAndBuilding
                        If Not LB_EXTENDED_PROPERTY_TABLE Is Nothing Then
                            ExtInfo = New Parameter_InsertExtendedInfo_LandAndBuilding(LB_EXTENDED_PROPERTY_TABLE.Rows.Count - 1) {}
                            For Each _Ext_Row As DataRow In LB_EXTENDED_PROPERTY_TABLE.Rows
                                If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
                                    Dim InEInfo As Parameter_InsertExtendedInfo_LandAndBuilding = New Parameter_InsertExtendedInfo_LandAndBuilding()
                                    InEInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                    InEInfo.SrNo = IIf(IsDBNull(_Ext_Row("LB_SR_NO")), Nothing, _Ext_Row("LB_SR_NO").ToString)
                                    InEInfo.Inst_ID = IIf(IsDBNull(_Ext_Row("LB_INS_ID")), Nothing, _Ext_Row("LB_INS_ID").ToString)
                                    InEInfo.TotalArea = Val(_Ext_Row("LB_TOT_P_AREA").ToString)
                                    InEInfo.ConstructedArea = Val(_Ext_Row("LB_CON_AREA").ToString)
                                    InEInfo.ConYear = IIf(IsDBNull(_Ext_Row("LB_CON_YEAR")), Nothing, _Ext_Row("LB_CON_YEAR").ToString)
                                    If IsDate(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))) Then InEInfo.MOU_Date = Convert.ToDateTime(IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))).ToString(cBase._Server_Date_Format_Short) Else InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                    'InEInfo.MOU_Date = IIf(IsDBNull(_Ext_Row("LB_MOU_DATE")), Nothing, _Ext_Row("LB_MOU_DATE"))
                                    InEInfo.Value = Val(_Ext_Row("LB_VALUE").ToString)
                                    InEInfo.OtherDetails = IIf(IsDBNull(_Ext_Row("LB_OTHER_DETAIL")), Nothing, _Ext_Row("LB_OTHER_DETAIL").ToString)
                                    InEInfo.Status_Action = Status_Action
                                    InEInfo.RecID = System.Guid.NewGuid().ToString()

                                    'If Not Base._L_B_DBOps.InsertExtendedInfo(InEInfo) Then
                                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                    'End If
                                    ExtInfo(cntExt) = InEInfo
                                End If
                                cntExt += 1
                            Next
                            InLandAndBuild.InsertExtInfo = ExtInfo
                        End If


                        'DOCS
                        Dim cntDoc As Integer = 0
                        Dim DocInfo() As Parameter_InsertDocInfo_LandAndBuilding
                        If Not LB_DOCS_ARRAY Is Nothing Then
                            DocInfo = New Parameter_InsertDocInfo_LandAndBuilding(LB_DOCS_ARRAY.Rows.Count) {}
                            For Each _Ext_Row As DataRow In LB_DOCS_ARRAY.Rows
                                If _Ext_Row("LB_REC_ID") = XRow("LB_REC_ID") Then
                                    Dim InDocInfo As Parameter_InsertDocInfo_LandAndBuilding = New Parameter_InsertDocInfo_LandAndBuilding()
                                    InDocInfo.LB_Rec_ID = IIf(IsDBNull(_Ext_Row("LB_REC_ID")), Nothing, _Ext_Row("LB_REC_ID").ToString)
                                    InDocInfo.Doc_Misc_ID = IIf(IsDBNull(_Ext_Row("LB_MISC_ID")), Nothing, _Ext_Row("LB_MISC_ID").ToString)
                                    InDocInfo.Status_Action = Status_Action
                                    InDocInfo.RecID = System.Guid.NewGuid().ToString()

                                    'If Not Base._L_B_DBOps.InsertDocumentsInfo(InDocInfo) Then
                                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                    'End If
                                    DocInfo(cntDoc) = InDocInfo
                                End If
                                cntDoc += 1
                            Next
                            InLandAndBuild.InsertDocInfo = DocInfo
                        End If



                        'Dim InAssetLoc As Common_Lib.RealTimeService.Param_AssetLoc_Insert = New Common_Lib.RealTimeService.Param_AssetLoc_Insert()
                        'InAssetLoc.name = InParam.Name
                        'InAssetLoc.OtherDetails = "Use Type: " & InParam.PropertyType
                        'InAssetLoc.Status_Action = Status_Action
                        'InAssetLoc.Match_LB_ID = InParam.RecID
                        'InAssetLoc.Match_SP_ID = "" 'Removed as locations wont be changed on update of property creation voucher 
                        'Add Location
                        'If Not Base._AssetLocDBOps.Insert(ClientScreen.Accounts_Voucher_Payment, InParam.Name, "Use Type: " & InParam.PropertyType, Status_Action, "", InParam.RecID) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        'InParam.param_InsertAssetLoc = InAssetLoc 'Commented to avoid updating the name and details of location on editing the property name
                        Dim Locations As DataTable = cBase._AssetLocDBOps.GetListByLBID(ClientScreen.Accounts_Voucher_Payment, IIf(IsDBNull(XRow("LB_REC_ID")), Nothing, XRow("LB_REC_ID")))
                        If Locations Is Nothing Then
                            Throw New Exception("|SomeError happened during current operation!!|")
                            Exit Function
                        End If
                        If Locations.Rows.Count > 0 Then
                            IsLBIncluded = True
                        End If

                        InsertProperty(cntMI) = InLandAndBuild
                    End If


                    '----------WIP References------------
                    If XRow("Item_Profile") = "WIP" Then
                        If XRow("WIP_REF_TYPE").ToString.Length > 0 Then
                            If XRow("WIP_REF_TYPE") = "NEW" Then 'XRow("Head").ToString.Contains("(WIP)") And
                                Dim InReference As Param_InsertTRIDAndTRSrNo_WIP_Profile = New Param_InsertTRIDAndTRSrNo_WIP_Profile()
                                InReference.LedID = IIf(IsDBNull(XRow("Item_Led_ID")), Nothing, XRow("Item_Led_ID"))
                                InReference.Reference = IIf(IsDBNull(XRow("REFERENCE")), Nothing, XRow("REFERENCE"))
                                InReference.Amount = IIf(IsDBNull(XRow("Amount")), Nothing, ConvertAsDecimal(XRow("Amount"))) + IIf(IsDBNull(XRow("TDS")), 0, XRow("TDS"))
                                InReference.Status_Action = Status_Action
                                InReference.TxnID = Rec_ID
                                InReference.TxnSrNo = IIf(IsDBNull(XRow("Sr.")), Nothing, XRow("Sr."))
                                InReference.UpdWipID = IIf(IsDBNull(XRow("CREATION_PROF_REC_ID")), Nothing, XRow("CREATION_PROF_REC_ID"))
                                InReference.Screen = ClientScreen.Accounts_Voucher_Payment

                                InsertReferencesWIP(cntMI) = InReference
                            End If
                        End If
                    End If

                    cntMI += 1
                Next

                EditParam.InsertItem = InsertItem
                EditParam.InsertPurpose = InsertPurpose
                EditParam.InsertOther = InsertOthers
                EditParam.InsertGS = InsertGS
                EditParam.InsertAssets = InsertAssets
                EditParam.InsertVehicles = InsertVehicles
                EditParam.InsertProperty = InsertProperty
                EditParam.InsertLS = InsertLS
                EditParam.InsertReferencesWIP = InsertReferencesWIP
                'If Not Base._Payment_DBOps.DeletePayment(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                EditParam.MID_DeletePayment = Rec_ID

                Dim InAdvancePmt(GridView3.Rows.Count - 1) As Parameter_InsertPayment_VoucherPayment
                Dim InLiabPmt(GridView4.Rows.Count - 1) As Parameter_InsertPayment_VoucherPayment
                Dim InBankPmt(0) As Parameter_InsertPaymentMT_VoucherPayment
                If Not Bank_Detail Is Nothing Then
                    Array.Resize(InBankPmt, Bank_Detail.Rows.Count)
                End If


                'Advance Payment
                Dim I As Integer = 0
                For Each row3 As DataRow In GridView3.Rows
                    If Val(row3("Payment")) > 0 Then
                        Dim InPmt As Parameter_InsertPayment_VoucherPayment = New Parameter_InsertPayment_VoucherPayment()
                        InPmt.TxnMID = Rec_ID
                        InPmt.Type = "ADVANCE"
                        InPmt.SrNo = row3("Sr")
                        InPmt.RefID = row3("AI_ID")
                        InPmt.RefAmount = ConvertAsDecimal(row3("Payment"))
                        InPmt.Status_Action = Status_Action
                        InPmt.RecID = System.Guid.NewGuid().ToString()

                        'If Not Base._Payment_DBOps.InsertPayment(InPmt) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InAdvancePmt(I) = InPmt
                        I = I + 1
                    End If
                Next
                'Liabilities Payment
                I = 0
                If (Not GridView4 Is Nothing) Then
                    For Each row4 As DataRow In GridView4.Rows
                        If Val(row4("Payment")) > 0 Then
                            Dim InPmt1 As Parameter_InsertPayment_VoucherPayment = New Parameter_InsertPayment_VoucherPayment()
                            InPmt1.TxnMID = Rec_ID
                            InPmt1.Type = "LIABILITIES"
                            InPmt1.SrNo = row4("Sr")
                            InPmt1.RefID = row4("LI_ID")
                            InPmt1.RefAmount = ConvertAsDecimal(row4("Payment"))
                            InPmt1.Status_Action = Status_Action
                            InPmt1.RecID = System.Guid.NewGuid().ToString()

                            'If Not Base._Payment_DBOps.InsertPayment(InPmt1) Then
                            '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    Exit Sub
                            'End If
                            InLiabPmt(I) = InPmt1
                            I = I + 1
                        End If
                    Next
                End If
                'Bank Payment
                cnt = 0
                If Not Bank_Detail Is Nothing Then

                    For Each XRow In Bank_Detail.Rows
                        Dim InPayment As Parameter_InsertPaymentMT_VoucherPayment = New Parameter_InsertPaymentMT_VoucherPayment()
                        InPayment.TxnMID = Rec_ID
                        InPayment.Type = "BANK"
                        InPayment.SrNo = IIf(IsDBNull(XRow("Sr.")), "", XRow("Sr."))
                        InPayment.Mode = IIf(IsDBNull(XRow("Mode")), "", XRow("Mode"))
                        InPayment.RefID = IIf(IsDBNull(XRow("ID")), "", XRow("ID"))
                        InPayment.RefNo = IIf(IsDBNull(XRow("No.")), "", XRow("No."))
                        'If IsDate(IIf(IsDBNull(XRow("Date")), "", XRow("Date"))) Then InPayment.RefDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Date")), "", XRow("Date"))).ToString(_Server_Date_Format_Short) Else InPayment.RefDate = IIf(IsDBNull(XRow("Date")), "", XRow("Date"))
                        'If IsDate(IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))) Then InPayment.ClearingDate = Convert.ToDateTime(IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))).ToString(_Server_Date_Format_Short) Else InPayment.ClearingDate = IIf(IsDBNull(XRow("Clearing Date")), "", XRow("Clearing Date"))
                        InPayment.RefDate = IIf(IsDBNull(XRow("Date")), Nothing, XRow("Date"))
                        InPayment.ClearingDate = IIf(IsDBNull(XRow("Clearing Date")), Nothing, XRow("Clearing Date"))
                        InPayment.RefAmount = ConvertAsDecimal(XRow("Amount"))
                        InPayment.MT_Bank_Id = IIf(IsDBNull(XRow("MT_BANK_ID")), "", XRow("MT_BANK_ID"))
                        InPayment.MT_AccNo = IIf(IsDBNull(XRow("Ref. A/c. No.")), "", XRow("Ref. A/c. No."))
                        InPayment.Status_Action = Status_Action
                        InPayment.RecID = System.Guid.NewGuid().ToString()

                        'If Not Base._Payment_DBOps.InsertPayment(InPayment) Then
                        '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                        InBankPmt(cnt) = InPayment
                        cnt += 1
                    Next
                End If
                EditParam.InAdvancePmt = InAdvancePmt
                EditParam.InLiabPmt = InLiabPmt
                EditParam.InBankPmt = InBankPmt
                'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment

                If input_Param.Pmt_SplVchrReferenceSelected IsNot Nothing Then
                    Dim SplVchrRefsSplit = input_Param.Pmt_SplVchrReferenceSelected.Split(","c)
                    Dim splitLength = SplVchrRefsSplit.Length

                    If splitLength > 0 Then
                        Dim UpdateSplVchrRefs As Parameter_InsertSplVchrRef_Vouchers() = New Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers(splitLength - 1) {}

                        For j As Integer = 0 To splitLength - 1
                            Dim _SplVchr As Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers = New Common_Lib.RealTimeService.Parameter_InsertSplVchrRef_Vouchers()
                            _SplVchr.Task_Name = SplVchrRefsSplit(j)
                            UpdateSplVchrRefs(j) = _SplVchr
                        Next

                        EditParam.UpdateSplVchrRefs = UpdateSplVchrRefs
                    End If
                End If

                If Not UpdatePayment_Txn(EditParam) Then
                    Throw New Exception("|SomeError happened during current operation!!|")
                    Exit Function
                End If

                If IsLBIncluded Then
                    Message = "<br><br>No Subsequent Changes have been made in Location(s) mapped to the property/properties mentioned in the current voucher.<br>User may make the required changes manually from Profile - > Core - > Locations."
                End If
                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.UpdateSuccess & Message
                SaveButtonChecksParam.messageCaption = TitleX
                SaveButtonChecksParam.messageIcon = "Information"
                SaveButtonChecksParam.dialogResult = "OK"
                SaveButtonChecksParam.CashbookGridPK = EditParam.param_UpdateMaster.RecID & EditParam.Insert(0).RecID
                'Return SaveButtonChecksParam
            End If

            If Val(NavMode) = Common_Lib.Common.Navigation_Mode._Delete Then 'DELETE
                Dim DelParam As Param_Txn_Delete_VoucherPayment = New Param_Txn_Delete_VoucherPayment

                'Dim xPromptWindow As New Common_Lib.Prompt_Window
                'If DialogResult.Yes = xPromptWindow.ShowDialog(Me.Text, "Sure you want to Delete this Entry...?", Common_Lib.Prompt_Window.ButtonType._Question, Common_Lib.Prompt_Window.WindowSize._399x140, Common_Lib.Prompt_Window.FocusButton._Button2, Color.AliceBlue) Then
                For Each XRow In DT.Rows
                    If XRow("Item_Profile") = "LAND & BUILDING" Or XRow("Item_Profile") = "OTHER ASSETS" Or (XRow("ITEM_VOUCHER_TYPE").Trim.ToUpper = "LAND & BUILDING" And Not XRow("Item_Profile").ToUpper = "LAND & BUILDING") Then
                        If IsInsuranceAudited Then
                            SaveButtonChecksParam.messageBoxText = "I n s u r a n c e   R e l a t e d   A s s e t s   C a n n o t   b e   D e l e t e d   A f t e r   T h e   C o m p l e t i o n   o f   I n s u r a n c e   A u d i t"
                            SaveButtonChecksParam.messageCaption = "Information..."
                            SaveButtonChecksParam.messageIcon = "Information"
                            Return SaveButtonChecksParam
                            Exit Function
                        End If
                    End If
                Next
                'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                Dim LB_REC_ID As DataTable = cBase._L_B_DBOps.GetIDsBytxnID(Rec_ID, ClientScreen.Accounts_Voucher_Payment)
                Dim TblRecID As DataTable = LB_REC_ID.Copy
                'check any L&B Expenses done on basis of requested deletion entries 
                For Each _RECROW As DataRow In TblRecID.Rows
                    'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                    Dim Dependent_payments As DataTable = GetTxnDetailsByRefID(_RECROW(0))
                    If Dependent_payments.Rows.Count > 0 Then
                        Dim TrDate As DateTime = Convert.ToDateTime(Dependent_payments.Rows(0)("TR_Date"))
                        SaveButtonChecksParam.messageBoxText = "A Construction Expense Entry of Rs." & Dependent_payments.Rows(0)("TR_AMOUNT") & " with date " & TrDate.ToString("dd-MMM-yyy") & " is dependednt on this voucher. Please delete that entry first!!"
                        SaveButtonChecksParam.messageCaption = TitleX
                        SaveButtonChecksParam.messageIcon = "Information"
                        Return SaveButtonChecksParam
                        Exit Function
                    End If
                Next

                DelParam.MID_DeleteItems = Rec_ID
                'If Not Base._Payment_DBOps.DeleteItems(xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePayment = Rec_ID
                'If Not Base._Payment_DBOps.DeletePayment(xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeletePurpose = Rec_ID
                'If Not Base._Payment_DBOps.DeletePurpose(xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                DelParam.MID_DeleteOther = Rec_ID
                'If Not Base._Payment_DBOps.DeleteOther(Me.xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If

                'PROFILE ENTRIES
                DelParam.MID_AdvanceDelete = Rec_ID
                'If Not Base._AdvanceDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DepostisDelete = Rec_ID
                'If Not Base._DepositsDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_LiabilityDelete = Rec_ID
                'If Not Base._LiabilityDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_GSDelete = Rec_ID
                'If Not Base._GoldSilverDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_AssetsDelete = Rec_ID
                'If Not Base._AssetDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_LivestockDelete = Rec_ID
                'If Not Base._LiveStockDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_VehicleDelete = Rec_ID
                'If Not Base._VehicleDBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_ReferenceDelete = Rec_ID

                'Get Rec ID for Curr TxnMaster ID
                Dim DelComplexInfo(TblRecID.Rows.Count - 1) As String
                Dim DelExtInfo(TblRecID.Rows.Count - 1) As String
                Dim DelDocInfo(TblRecID.Rows.Count - 1) As String
                Dim DelByLB(TblRecID.Rows.Count - 1) As String
                Dim cntDel As Integer = 0

                For Each _RECROW As DataRow In TblRecID.Rows
                    'Remove existing Extensions
                    'If Not Base._L_B_DBOps.DeleteExtendedInfo(_RECROW("REC_ID")) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelComplexInfo(cntDel) = _RECROW("REC_ID")
                    DelExtInfo(cntDel) = _RECROW("REC_ID")
                    'Remove existing docs
                    'If Not Base._L_B_DBOps.DeleteDocumentInfo(_RECROW("REC_ID")) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelDocInfo(cntDel) = _RECROW("REC_ID")
                    ''Remove Location
                    'If Not Base._AssetLocDBOps.DeleteByLB(_RECROW("REC_ID"), ClientScreen.Accounts_Voucher_Gift) Then
                    '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If
                    DelByLB(cntDel) = _RECROW("REC_ID")
                    cntDel += 1
                Next

                DelParam.DeleteComplexBuilding = DelComplexInfo
                DelParam.DeleteDocumentInfo = DelDocInfo
                DelParam.DeleteExtendedInfo = DelExtInfo
                DelParam.DeleteByLB = DelByLB

                'If Not Base._L_B_DBOps.Delete(xMID.Text, Common_Lib.RealTimeService.ClientScreen.Accounts_Voucher_Payment) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteLB = Rec_ID
                'If Not Base._Payment_DBOps.Delete(xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_Delete = Rec_ID
                'If Not Base._Payment_DBOps.DeleteMaster(xMID.Text) Then
                '    DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                DelParam.MID_DeleteMaster = Rec_ID
                'BasicParam_2.screen = ClientScreen.Accounts_Voucher_Payment
                If Not DeletePayment_Txn(DelParam) Then
                    Throw New Exception("|SomeError happened during current operation!!|")
                    Exit Function
                End If

                SaveButtonChecksParam.messageBoxText = Common_Lib.Messages.DeleteSuccess
                SaveButtonChecksParam.messageCaption = TitleX
                SaveButtonChecksParam.messageIcon = "Information"
                SaveButtonChecksParam.dialogResult = "OK"
                'End If
                ' xPromptWindow.Dispose()
            End If
            Return SaveButtonChecksParam
        End Function
    End Class
#End Region
End Class
