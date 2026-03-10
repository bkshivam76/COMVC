'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_SaleOfAsset
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        ''' <summary>
        ''' Get MasterID, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetMasterID</remarks>
        Public Function GetMasterID(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.SaleOfAssets_GetMasterID, ClientScreen.Accounts_Voucher_SaleOfAsset, Rec_Id)
        End Function

        'Shifted
        Public Function GetItemsListByID(ByVal Rec_Id As String) As Object
            Dim Query As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            inParam.ItemIDs = Rec_Id
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam) 'Type=1
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_SaleOfAsset, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_SaleOfAsset, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByCustom("WHERE TR_M_ID ='" & Rec_ID & "' AND TR_SR_NO = 1 AND TR_CR_LED_ID IS NOT NULL AND TR_DR_LED_ID IS NOT NULL ", ClientScreen.Accounts_Voucher_SaleOfAsset, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        ''' <summary>
        ''' Get Txn Items, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetTxnItems</remarks>
        Public Function GetTxnItems(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.SaleOfAssets_GetTxnItems, ClientScreen.Accounts_Voucher_SaleOfAsset, Rec_ID)
        End Function

        ''' <summary>
        ''' Get Bank Payments, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetBankPayments</remarks>
        Public Function GetBankPayments(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.SaleOfAssets_GetBankPayments, ClientScreen.Accounts_Voucher_SaleOfAsset, Rec_ID)
        End Function

        ''' <summary>
        ''' Get Assets Listing for all 5 Profiles with Latest Balances
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <returns></returns>
        ''' <remarks>SaleOfAssets_AssetsListingForSale</remarks>
        Public Function GetAllProfile_AssetList(ByVal inParam As Param_GetAssetListingForSale) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.SaleOfAssets_AssetsListingForSale, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam)
        End Function

        Public Function Get_AssetMaxTxnDate(ByVal inParam As Param_GetAssetMaxTxnDate) As Object
            Return GetScalarBySP(RealTimeService.RealServiceFunctions.SaleOfAssets_MaxTxnDate, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam)
        End Function

        Public Function Get_AssetEnclosingTxnDate(ByVal inParam As Param_GetAssetEnclosingTxnDate) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.SaleOfAssets_EnclosingTxnDate, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam)
        End Function

        'Shifted
        Public Function GetGoldSilverList(ByVal GS_Type As String) As DataTable
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Return _GS.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, GS_Type)
        End Function

        Public Function GetGoldSilverListByID(ByVal GS_Type As String, Optional GS_ID As String = Nothing) As DataTable
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Dim Param As Param_GoldSilver_GetList_Common = New Param_GoldSilver_GetList_Common
            Param.GS_Type = GS_Type
            Param.GS_Rec_ID = GS_ID
            Return _GS.GetListByID(ClientScreen.Accounts_Voucher_SaleOfAsset, Param)
        End Function

        'Shifted
        Public Function GetVehiclesList(Optional Vehicle_Rec_ID As String = Nothing) As DataTable
            Dim _Veh As Vehicles = New Vehicles(cBase)
            Return _Veh.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, Vehicle_Rec_ID)
        End Function

        'Shifted
        Public Function GetLiveStockList(Optional LS_Rec_ID As String = Nothing) As DataTable
            Dim _LS As LiveStock = New LiveStock(cBase)
            Return _LS.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, LS_Rec_ID)
        End Function

        'Shifted
        Public Function GetAssetList(Optional Asset_Rec_ID As String = Nothing) As DataTable
            Dim _Assets As Assets = New Assets(cBase)
            Return _Assets.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, Asset_Rec_ID)
        End Function

        'Shifted
        Public Function GetLandandBuildingList(Optional LB_Rec_ID As String = Nothing) As DataTable
            Dim _LandAndBuilding As LandAndBuilding = New LandAndBuilding(cBase)
            Dim Param As Param_LandAndBuilding_GetListByQuery = New Param_LandAndBuilding_GetListByQuery
            Param.LB_Rec_ID = LB_Rec_ID
            Return _LandAndBuilding.GetListByQuery(ClientScreen.Accounts_Voucher_SaleOfAsset, Param)
        End Function

        'Shifted
        Public Function GetGoldSilverMisc(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GOLD / SILVER ITEMS", ClientScreen.Accounts_Voucher_SaleOfAsset, MiscNameColumnHead, RecIDColumnHead)
        End Function

        'Shifted
        Public Function GetSaleofAssetItems(ByVal Profile As String) As DataTable
            Dim LocalQuery As String = " SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UCASE(I.ITEM_PROFILE) IN ('" & Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
            Dim OnlineQuery As String = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UPPER(I.ITEM_PROFILE) IN ('" & Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Profile = Profile
            inParam.Type = "3"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam)
        End Function

        ''' <summary>
        ''' Get Txn List, Shifted
        ''' </summary>
        ''' <param name="IsNew"></param>
        ''' <param name="TxnMID"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.SaleOfAssets_GetTxnList</remarks>
        Public Function GetTxnList(ByVal IsNew As Boolean, YearID As Integer, Optional ByVal TxnMID As String = Nothing, Optional NextUnAuditedYearID As Integer = Nothing) As DataTable
            Dim Param As Param_Voucher_SaleOfAsset_GetTxnList = New Param_Voucher_SaleOfAsset_GetTxnList()
            Param.IsNew = IsNew
            Param.TxnMID = TxnMID
            Param.YearID = YearID
            Param.NextUnAuditedYearID = NextUnAuditedYearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.SaleOfAssets_GetTxnList, ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Account_Rec_ID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, Param)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_SaleOfAsset, " B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID, B.REC_ID AS BANK_ID ")
        End Function

        'Shifted
        Public Function GetBanks() As DataTable
            Dim Query As String = "SELECT BI_BANK_NAME, BI_SHORT_NAME, REC_ID as BI_ID From  BANK_INFO  Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        'Shifted
        Public Function GetParties(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _Add As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            Param.Party_Rec_ID = Party_Rec_ID
            Return _Add.GetList(ClientScreen.Accounts_Voucher_SaleOfAsset, Param)
        End Function

        'Shifted
        Public Function GetPartyCities(ByVal CityIDs) As DataTable
            Return GetCitiesByID(ClientScreen.Accounts_Voucher_SaleOfAsset, "CI_NAME", "REC_ID", CityIDs)
        End Function

        'Shifted
        Public Function GetItemsList(ByVal ItemID As String) As DataTable
            Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & ItemID & "') AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "2"
            inParam.ItemIDs = ItemID
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_SaleOfAsset, inParam) 'type =2
        End Function

        'Shifted
        Public Function GetPurposes() As DataTable
            ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_SaleOfAsset, "PUR_NAME", "PUR_ID")
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = "PUR_NAME"
            InParam.RecIDColumnHead = "PUR_ID"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_SaleOfAsset, InParam)
        End Function

        'Shifted
        Public Function GetLedgerItems(ByVal Is_HQ_Centre As Boolean) As DataTable
            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " & _
                                     " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('SALE OF ASSET') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_SaleOfAsset, LocalQuery, OnlineParam)
        End Function

        'Shifted
        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_SaleOfAsset, MiscNameColumnHead, RecIDColumnHead)
        End Function

        ''' <summary>
        ''' Insert Master Info, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherSaleOfAsset) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertMasterInfo, ClientScreen.Accounts_Voucher_SaleOfAsset, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherSaleOfAsset) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_Insert, ClientScreen.Accounts_Voucher_SaleOfAsset, InParam)
        End Function

        ''' <summary>
        ''' Insert Item, Shifted
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertItem</remarks>
        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherSaleOfAsset) As Boolean
            'InItem.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertItem, ClientScreen.Accounts_Voucher_SaleOfAsset, InItem)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherSaleOfAsset) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertPurpose, ClientScreen.Accounts_Voucher_SaleOfAsset, InPurpose)
        End Function

        ''' <summary>
        ''' Advances and Liabilities, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertPayment</remarks>
        Public Function InsertPayment(ByVal InPay As Parameter_InsertAandLPayment_VoucherSaleOfAsset) As Boolean
            'InPay.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertPayment, ClientScreen.Accounts_Voucher_SaleOfAsset, InPay)
        End Function

        ''' <summary>
        ''' Bank Payment, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertBankPayment</remarks>
        Public Function InsertPayment(ByVal InBpay As Parameter_InsertBankPayment_VoucherSaleOfAsset) As Boolean
            'InBpay.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertBankPayment, ClientScreen.Accounts_Voucher_SaleOfAsset, InBpay)
        End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherSaleOfAsset) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.SaleOfAssets_UpdateMaster, ClientScreen.Accounts_Voucher_SaleOfAsset, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeletePayment(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeleteMaster(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Function InsertSaleOfAsset_Txn(ByVal InParam As Param_Txn_Insert_VoucherSaleOfAsset)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SaleOfAssets_InsertSaleOfAsset_Txn, ClientScreen.Accounts_Voucher_SaleOfAsset, InParam)
        End Function

        Public Function UpdateSaleOfAsset_Txn(UpParam As Param_Txn_Update_VoucherSaleOfAsset)
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Mid_Delete, ClientScreen.Accounts_Voucher_SaleOfAsset)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SaleOfAssets_UpdateSaleOfAsset_Txn, ClientScreen.Accounts_Voucher_SaleOfAsset, UpParam)
        End Function

        Public Function DeleteSaleOfAssets_Txn(DelParam As Param_Txn_Delete_VoucherSaleOfAsset)
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.Mid_Delete, ClientScreen.Accounts_Voucher_SaleOfAsset)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + DelParam.Mid_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SaleOfAssets_DeleteSaleOfAssets_Txn, ClientScreen.Accounts_Voucher_SaleOfAsset, DelParam)
        End Function
    End Class
#End Region
End Class
