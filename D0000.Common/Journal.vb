Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Voucher_Journal
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Journal, MiscNameColumnHead, RecIDColumnHead)
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = MiscNameColumnHead
            InParam.RecIDColumnHead = RecIDColumnHead
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Journal, InParam)
        End Function

        Public Function GetParties(ByVal NameColHead As String, ByVal IdColHead As String, Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _Add As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.NameColHead = NameColHead
            Param.IdColHead = IdColHead
            Param.Party_Rec_ID = Party_Rec_ID
            Return _Add.GetList(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetLedgerItems(ByVal Is_HQ_Centre As Boolean) As DataTable
            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"

            Dim LocalQuery As String = " select DISTINCT * FROM  (SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                                 " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND ITEM_IS_JV_APPLICABLE = TRUE AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            LocalQuery += "          Union All " &
                            "          SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                            "          FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND ITEM_IS_JV_APPLICABLE = TRUE  AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' )AS A order by ITEM_NAME"

            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            OnlineParam.open_Ins_ID = cBase._open_Ins_ID
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_Journal, LocalQuery, OnlineParam)
        End Function

        Public Function GetAssets(ByVal ItemID As String) As DataTable
            Dim _Assets As Assets = New Assets(cBase)
            Return _Assets.GetList(ClientScreen.Accounts_Voucher_Journal, ItemID)
        End Function

        Public Function IsTDSApplicable(ByVal ItemID As String) As Boolean
            Dim TDS As DataTable = GetItemTDSCode(ClientScreen.Accounts_Voucher_Journal, ItemID)
            If TDS.Rows(0)(0).ToString().ToUpper <> "NONE" Then Return True
            Return False
        End Function

        Public Function GetAssetsByID(ByVal ItemID As String, Optional Asset_Rec_ID As String = Nothing) As DataTable
            Dim _Assets As Assets = New Assets(cBase)
            Dim Param As Param_Assets_GetList_Common = New Param_Assets_GetList_Common
            Param.Item_ID = ItemID
            Param.AI_REC_ID = Asset_Rec_ID
            Return _Assets.GetListByID(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetGS(ByVal ItemID As String) As DataTable
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Return _GS.GetList(ClientScreen.Accounts_Voucher_Journal, ItemID)
        End Function

        Public Function GetGSByID(ByVal ItemID As String, Optional Asset_Rec_ID As String = Nothing) As DataTable
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Dim Param As Param_GoldSilver_GetList_Common = New Param_GoldSilver_GetList_Common
            Param.Item_ID = ItemID
            Param.GS_Rec_ID = Asset_Rec_ID
            Return _GS.GetListByID(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetProperty(ByVal ItemID As String, Optional LB_Rec_ID As String = Nothing) As DataTable
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Dim Param As Param_LandAndBuilding_GetListByQuery = New Param_LandAndBuilding_GetListByQuery
            Param.ItemID = ItemID
            Param.LB_Rec_ID = LB_Rec_ID
            Return _LB.GetListByQuery(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetliveStocks(ByVal ItemID As String) As DataTable
            Dim _LS As LiveStock = New LiveStock(cBase)
            Return _LS.GetList(ClientScreen.Accounts_Voucher_Journal, ItemID)
        End Function

        Public Function GetliveStocksByID(ByVal ItemID As String, Optional LS_Rec_ID As String = Nothing) As DataTable
            Dim _LS As LiveStock = New LiveStock(cBase)
            Dim Param As Param_Livestock_GetList_Common = New Param_Livestock_GetList_Common
            Param.LS_Item_ID = ItemID
            Param.LS_Rec_ID = LS_Rec_ID
            Return _LS.GetListByID(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function


        Public Function GetVehicles(ByVal ItemID As String) As DataTable
            Dim _VI As Vehicles = New Vehicles(cBase)
            Return _VI.GetList(ClientScreen.Accounts_Voucher_Journal, ItemID)
        End Function

        Public Function GetVehiclesByID(ByVal ItemID As String, Optional VI_Rec_ID As String = Nothing) As DataTable
            Dim _VI As Vehicles = New Vehicles(cBase)
            Dim Param As Param_Vehicles_GetList_Common = New Param_Vehicles_GetList_Common
            Param.VI_ItemID = ItemID
            Param.VI_Rec_ID = VI_Rec_ID
            Return _VI.GetListByID(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetAdvances(ByVal ItemID As String, ByVal PartyID As String, ByVal MasterTxnID As String, Optional Adv_Rec_ID As String = Nothing) As DataTable
            Dim _Adv As Advances = New Advances(cBase)
            Dim Param As Param_Advances_GetList_Common = New Param_Advances_GetList_Common
            Param.ItemID = ItemID
            Param.PartyID = PartyID
            Param.CreationTxnRecID = MasterTxnID
            Param.Adv_Rec_ID = Adv_Rec_ID
            Return _Adv.GetList(ClientScreen.Accounts_Voucher_Journal, Param)
        End Function

        Public Function GetDeposits(ByVal ItemID As String, ByVal PartyID As String, ByVal MasterTxnID As String, Optional Dep_Rec_ID As String = Nothing) As DataTable
            Dim _Dep As Deposits = New Deposits(cBase)
            Dim PARAM As Param_Deposits_GetListCommon = New Param_Deposits_GetListCommon
            PARAM.ItemID = ItemID
            PARAM.PartyID = PartyID
            PARAM.CreationTxnRecID = MasterTxnID
            PARAM.Dep_Rec_ID = Dep_Rec_ID
            Return _Dep.GetList(ClientScreen.Accounts_Voucher_Journal, PARAM)
        End Function

        Public Function GetLiabilities(ByVal ItemID As String, ByVal PartyID As String, ByVal MasterTxnID As String, Optional Li_Rec_ID As String = Nothing) As DataTable
            Dim _Liab As Liabilities = New Liabilities(cBase)
            Dim inParam As Param_Liabilities_GetListCommon = New Param_Liabilities_GetListCommon
            inParam.ItemID = ItemID
            inParam.PartyID = PartyID
            inParam.CreationTxnRecID = MasterTxnID
            inParam.Li_Rec_ID = Li_Rec_ID
            Return _Liab.GetList(ClientScreen.Accounts_Voucher_Journal, inParam)
        End Function

        Public Function GetRecords(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Journal_GetDataRecords, ClientScreen.Accounts_Voucher_Journal, Rec_ID)
        End Function

        Public Function GetGetCurrentRecAdjustment(ByVal inParam As Parameter_GetCurrentRecAdjustment) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Journal_GetCurrentRecAdjustment, ClientScreen.Accounts_Voucher_Journal, inParam)
        End Function

        Public Function GetJornalEntryAdjustments(ByVal inParam As Parameter_GetJornalEntryAdjustments, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Journal_GetJornalEntryAdjustments, screen, inParam)
        End Function

        Public Function Get_JV_Asset_Listing(inParam As Param_Get_JV_Asset_Listing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Journal_Get_JV_Asset_Listing, ClientScreen.Accounts_Voucher_Journal, inParam)
        End Function

        ''' <summary>
        ''' Inserts Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Journal_InsertMaster</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucheJournal) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Journal_InsertMaster, ClientScreen.Accounts_Voucher_Journal, InMInfo)
        End Function

        ''' <summary>
        ''' Insert Txn
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Journal_InsertTxn</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherJournal) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Journal_InsertTxn, ClientScreen.Accounts_Voucher_Journal, InParam)
        End Function

        ''' <summary>
        ''' Insert Txn
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertPurpose(ByVal InParam As Parameter_InsertPurpose_VoucherJournal) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Journal_InsertPurpose, ClientScreen.Accounts_Voucher_Journal, InParam)
        End Function

        Public Function InsertLiability(ByVal InLiab As Parameter_InsertTRID_Liabilities) As Boolean
            Dim _Liab As Liabilities = New Liabilities(cBase)
            Return _Liab.Insert(InLiab)
        End Function

        Public Function InsertAdvances(ByVal InAdv As Parameter_InsertTRID_Advances)
            Dim _Adv As Advances = New Advances(cBase)
            Return _Adv.Insert(InAdv)
        End Function

        Public Function InsertDeposits(ByVal InDeposits As Parameter_InsertTRID_Deposits)
            Dim _Dep As Deposits = New Deposits(cBase)
            Return _Dep.Insert(InDeposits)
        End Function

        ''' <summary>
        ''' Update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Journal_InsertTxn</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherJournal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Journal)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Journal_UpdateMaster, ClientScreen.Accounts_Voucher_Journal, UpParam)
        End Function

        Public Overloads Function DeleteMaster(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteRecord(Master_Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Overloads Function DeletePurpose(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteByCondition("TR_REC_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Overloads Function Delete(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Overloads Function DeleteAdvances(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteByCondition("AI_TR_ID = '" & Master_Rec_Id & "'", Tables.ADVANCES_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Overloads Function DeleteDeposits(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteByCondition("DI_TR_ID = '" & Master_Rec_Id & "'", Tables.DEPOSITS_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Overloads Function DeleteLiabilities(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Journal)
            Return DeleteByCondition("LI_TR_ID = '" & Master_Rec_Id & "'", Tables.LIABILITIES_INFO, ClientScreen.Accounts_Voucher_Journal)
        End Function

        Public Function InsertJournal_Txn(InParam As Param_Txn_Insert_VoucherJournal) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Journal_InsertJournal_Txn, ClientScreen.Accounts_Voucher_Journal, InParam)
        End Function

        Public Function UpdateJournal_Txn(UpParam As Param_Txn_Update_VoucherJournal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID_Delete, ClientScreen.Accounts_Voucher_Journal)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Journal_UpdateJournal_Txn, ClientScreen.Accounts_Voucher_Journal, UpParam)
        End Function

        Public Function DeleteJournal_Txn(delParam As Param_Txn_Delete_VoucherJournal) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_Journal)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Journal_DeleteJournal_Txn, ClientScreen.Accounts_Voucher_Journal, delParam)
        End Function

    End Class
End Class
