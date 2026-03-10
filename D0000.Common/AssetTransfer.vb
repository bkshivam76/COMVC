'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_AssetTransfer
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetHQCenters() As DataTable
            Return GetHQCentersForCurrInstt(ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetMasterID(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetTransfer_GetMasterID, ClientScreen.Accounts_Voucher_AssetTransfer, Rec_Id)
        End Function

        Public Function GetItemsListByID(ByVal Rec_Id As String) As Object
            Dim Query As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            inParam.ItemIDs = Rec_Id
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_AssetTransfer, inParam) 'Type=1
        End Function

        Public Function GetAssetLocations(ByVal Cen_ID As Integer) As DataTable
            Dim _AL As AssetLocations = New AssetLocations(cBase)
            Return _AL.GetListByCenID(ClientScreen.Accounts_Voucher_AssetTransfer, Cen_ID)
        End Function

        Public Function GetAssetLocationByID(Optional Loc_Id As String = Nothing) As DataTable
            Dim _AL As AssetLocations = New AssetLocations(cBase)
            Return _AL.GetList(ClientScreen.Accounts_Voucher_AssetTransfer, Loc_Id, Nothing)
        End Function

        Public Function GetFrCenterList(ByVal IncludeCenIDs As String, ByVal ExcludeCenIDs As String, ByVal IncludeRecIDs As String, ByVal ExcludeRecIDs As String) As DataTable
            Dim LocalQuery As String = " SELECT IIF( LEN(IIF(ISNULL(C.CEN_NAME),'',C.CEN_NAME)) > 0, C.CEN_NAME, M.CEN_NAME) AS [FR_CEN_NAME],C.CEN_PAD_NO AS [FR_PAD_NO], C.CEN_UID AS [FR_UID],   M.CEN_INCHARGE AS [FR_INCHARGE], M.CEN_ZONE_ID AS [FR_ZONE], C.CEN_ID AS [FR_CEN_ID], C.REC_ID AS [FR_ID], " & _
                                    " IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 ,M.CEN_TEL_NO_1,'') +  IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 AND LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 , ', ','') + IIF( LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 ,M.CEN_MOB_NO_1,'') AS [FR_TEL_NO]" & _
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " & _
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=TRUE AND C.[CEN_INS_ID]='" & cBase._open_Ins_ID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If ExcludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID NOT IN (" & ExcludeRecIDs & ")"
            If IncludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID IN (" & IncludeRecIDs & ")"
            If ExcludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID NOT IN (" & ExcludeCenIDs & ")"
            If IncludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID IN (" & IncludeCenIDs & ")"

            Dim Param As Param_Voucher_AssetTransfer_GetFRCenterList = New Param_Voucher_AssetTransfer_GetFRCenterList()
            Param.ExcludeCenIDs = ExcludeCenIDs
            Param.ExcludeRecIDs = ExcludeRecIDs
            Param.IncludeCenIDs = IncludeCenIDs
            Param.IncludeRecIDs = IncludeRecIDs
            Param.openInsID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetFrCenterList, LocalQuery, ClientScreen.Accounts_Voucher_AssetTransfer, Tables.CENTRE_INFO, Param)
        End Function

        Public Function GetToCenterList(ByVal IncludeCenIDs As String, ByVal ExcludeCenIDs As String, ByVal IncludeRecIDs As String, ByVal ExcludeRecIDs As String) As DataTable
            Dim LocalQuery As String = " SELECT IIF( LEN(IIF(ISNULL(C.CEN_NAME),'',C.CEN_NAME)) > 0, C.CEN_NAME, M.CEN_NAME) AS [TO_CEN_NAME],C.CEN_PAD_NO AS [TO_PAD_NO], C.CEN_UID AS [TO_UID],   M.CEN_INCHARGE AS [TO_INCHARGE], M.CEN_ZONE_ID AS [TO_ZONE], C.CEN_ID AS [TO_CEN_ID], C.REC_ID AS [TO_ID], " & _
                                    " IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 ,M.CEN_TEL_NO_1,'') +  IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 AND LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 , ', ','') + IIF( LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 ,M.CEN_MOB_NO_1,'') AS [TO_TEL_NO]" & _
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " & _
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=TRUE AND C.[CEN_INS_ID]='" & cBase._open_Ins_ID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If ExcludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID NOT IN (" & ExcludeRecIDs & ")"
            If IncludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID IN (" & IncludeRecIDs & ")"
            If ExcludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID NOT IN (" & ExcludeCenIDs & ")"
            If IncludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID IN (" & IncludeCenIDs & ")"
            Dim Param As Param_Voucher_AssetTransfer_GetToCenterList = New Param_Voucher_AssetTransfer_GetToCenterList()
            Param.ExcludeCenIDs = ExcludeCenIDs
            Param.ExcludeRecIDs = ExcludeRecIDs
            Param.IncludeCenIDs = IncludeCenIDs
            Param.IncludeRecIDs = IncludeRecIDs
            Param.openInsID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetToCenterList, LocalQuery, ClientScreen.Accounts_Voucher_AssetTransfer, Tables.CENTRE_INFO, Param)
        End Function

        Public Function GetUnMatchedList(ByVal RowCount As Int32, ByVal CenID As Integer, Optional RecID As String = Nothing) As DataSet
            Dim param As Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount = New Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount
            param.OpenCenRecID = cBase._open_Cen_Rec_ID
            param.TopCount = RowCount
            param.EnteringCenID = CenID
            param.RecID = RecID
            Return (GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.AssetTransfer_GetUnMatchedList_LimitedCount, ClientScreen.Accounts_Voucher_AssetTransfer, param))
        End Function

        Public Function GetUnmatchedCount_AssetTransfer() As Integer
            Dim InParam As Param_VoucherAssetTransfer_GetUnmatchedCount = New Param_VoucherAssetTransfer_GetUnmatchedCount
            InParam.CurrInsttID = cBase._open_Ins_ID
            InParam.OpenCenRecID = cBase._open_Cen_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetTransfer_GetUnmatchedEntriesCount, ClientScreen.Home_StartUp, InParam)
        End Function

        Public Function GetUnmatchedCount_AssetTransfer(fromDate As DateTime, ToDate As DateTime) As Integer
            Dim InParam As Param_VoucherAssetTransfer_GetUnmatchedCount = New Param_VoucherAssetTransfer_GetUnmatchedCount
            InParam.CurrInsttID = cBase._open_Ins_ID
            InParam.OpenCenRecID = cBase._open_Cen_Rec_ID
            InParam.FromDate = fromDate
            InParam.ToDate = ToDate
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetTransfer_GetUnmatchedEntriesCount, ClientScreen.Home_StartUp, InParam)
        End Function

        Public Function GetUnmatchedCount_AssetTransferFrom() As Integer
            Dim InParam As Param_VoucherAssetTransfer_GetUnmatchedFromCount = New Param_VoucherAssetTransfer_GetUnmatchedFromCount
            InParam.CurrInsttID = cBase._open_Ins_ID
            InParam.OpenCenRecID = cBase._open_Cen_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetTransfer_GetUnmatchedFromEntriesCount, ClientScreen.Home_StartUp, InParam)
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_AssetTransfer, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_AssetTransfer, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByCustom("WHERE TR_M_ID ='" & Rec_ID & "' AND TR_SR_NO = 1 AND TR_CR_LED_ID IS NOT NULL AND TR_DR_LED_ID IS NOT NULL ", ClientScreen.Accounts_Voucher_AssetTransfer, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetRecord(ByVal Master_Rec_ID As String, ByVal SrNo As Int16) As DataTable
            Return GetRecordByColumn("TR_M_ID", Master_Rec_ID, "TR_SR_NO", SrNo.ToString, ClientScreen.Accounts_Voucher_AssetTransfer, RealTimeService.Tables.TRANSACTION_INFO)
        End Function


        'Public Function GetTxnItems(ByVal Rec_ID As String) As DataTable
        '    Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetTxnItems, ClientScreen.Accounts_Voucher_AssetTransfer, Rec_ID)
        'End Function

        Public Function GetGoldSilverList(ByVal GS_Type As String, ByVal Cen_ID As Integer, Optional GS_ID As String = Nothing) As DataTable
            Dim Param As Param_GoldSilver_GetList_Common = New Param_GoldSilver_GetList_Common
            Param.GS_Type = GS_Type
            Param.Cen_ID = Cen_ID
            Param.GS_Rec_ID = GS_ID
            Dim _GS As GoldSilver = New GoldSilver(cBase)
            Return _GS.GetList(ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetVehiclesList(ByVal Cen_ID As Integer, Optional Vehicle_Rec_ID As String = Nothing) As DataTable
            Dim Param As Param_Vehicles_GetList_Common = New Param_Vehicles_GetList_Common
            Param.Cen_ID = Cen_ID
            Param.VI_Rec_ID = Vehicle_Rec_ID
            Dim _Veh As Vehicles = New Vehicles(cBase)
            Return _Veh.GetList(ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetLiveStockList(ByVal Cen_ID As Integer, Optional LS_Rec_ID As String = Nothing) As DataTable
            Dim Param As Param_Livestock_GetList_Common = New Param_Livestock_GetList_Common
            Param.Cen_ID = Cen_ID
            Param.LS_Rec_ID = LS_Rec_ID
            Dim _LS As LiveStock = New LiveStock(cBase)
            Return _LS.GetList(ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetAssetList(ByVal Cen_ID As Integer, Optional Asset_Rec_ID As String = Nothing) As DataTable
            Dim Param As Param_Assets_GetList_Common = New Param_Assets_GetList_Common
            Param.Cen_ID = Cen_ID
            Param.AI_REC_ID = Asset_Rec_ID
            Dim _Assets As Assets = New Assets(cBase)
            Return _Assets.GetList(ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetAssetTransfers(screen As ClientScreen, Optional YearID As Integer = Nothing, Optional Asset_ID As String = Nothing, Optional Exclude_PrevYears As Boolean = False) As DataTable
            Dim param As Param_GetAssetTransfers = New Param_GetAssetTransfers
            param.refAssetID = Asset_ID
            param.YearID = YearID
            param.Exclude_PrevYears = Exclude_PrevYears
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetAssetTransfers, screen, param)
        End Function

        Public Function GetLandandBuildingList(ByVal Cen_ID As Integer, Optional LB_Rec_ID As String = Nothing) As DataTable
            Dim _LandAndBuilding As LandAndBuilding = New LandAndBuilding(cBase)
            Dim Param As Param_LandAndBuilding_GetListByQuery = New Param_LandAndBuilding_GetListByQuery
            Param.LB_Rec_ID = LB_Rec_ID
            Param.Cen_ID = Cen_ID
            Return _LandAndBuilding.GetListByQuery(ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetGoldSilverMisc(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GOLD / SILVER ITEMS", ClientScreen.Accounts_Voucher_AssetTransfer, MiscNameColumnHead, RecIDColumnHead)
        End Function

        Public Function GetAssetTransferItems(ByVal Profile As String) As DataTable
            Dim LocalQuery As String = " SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UCASE(I.ITEM_PROFILE) IN ('" & Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
            Dim OnlineQuery As String = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  where  UPPER(I.ITEM_PROFILE) IN ('" & Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Profile = Profile
            inParam.Type = "3"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_AssetTransfer, inParam)
        End Function

        Public Function GetTxnList_Sale(ByVal IsNew As Boolean, Optional ByVal TxnMID As String = Nothing) As DataTable
            Dim Param As Param_Voucher_AssetTransfer_GetTxnList = New Param_Voucher_AssetTransfer_GetTxnList()
            Param.IsNew = IsNew
            Param.TxnMID = TxnMID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetTxnList_Sale, ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function GetTxnList_AssetTrf(ByVal IsNew As Boolean, Optional ByVal TxnMID As String = Nothing) As DataTable
            Dim Param As Param_Voucher_AssetTransfer_GetTxnList = New Param_Voucher_AssetTransfer_GetTxnList()
            Param.IsNew = IsNew
            Param.TxnMID = TxnMID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetTransfer_GetTxnList_AssetTrf, ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        Public Function GetItemsList(ByVal ItemID As String) As DataTable
            Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & ItemID & "') AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "2"
            inParam.ItemIDs = ItemID
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_AssetTransfer, inParam) 'type =2
        End Function

        Public Function GetPurposes() As DataTable
            ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_AssetTransfer, "PUR_NAME", "PUR_ID")
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = "PUR_NAME"
            InParam.RecIDColumnHead = "PUR_ID"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_AssetTransfer, InParam)
        End Function

        Public Function GetLedgerItems(ByVal Is_HQ_Centre As Boolean) As DataTable
            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
            Dim LocalQuery As String = ""
            LocalQuery = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " & _
                         " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('ASSET TRANSFER') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_AssetTransfer, LocalQuery, OnlineParam)
        End Function

        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_AssetTransfer, MiscNameColumnHead, RecIDColumnHead)
        End Function

        Public Function Get_AssetTf_Asset_Listing(ByVal inParam As Param_Get_AssetTf_Asset_Listing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.AssetTransfer_Get_AssetTf_Asset_Listing, ClientScreen.Accounts_Voucher_AssetTransfer, inParam)
        End Function

        Public Function GetUnAuditedFinancialYearOfTransferorCentre(CenId As Integer) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetUnAuditedFinancialYearOfTransferorCentre, ClientScreen.Accounts_Voucher_AssetTransfer, CenId)
        End Function

        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherAssetTransfer) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetTransfer_InsertMasterInfo, ClientScreen.Accounts_Voucher_AssetTransfer, InMInfo)
        End Function

        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherAssetTransfer) As Boolean
            'InParam.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetTransfer_Insert, ClientScreen.Accounts_Voucher_AssetTransfer, InParam)
        End Function

        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherAssetTransfer) As Boolean
            'InItem.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetTransfer_InsertItem, ClientScreen.Accounts_Voucher_AssetTransfer, InItem)
        End Function

        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherAssetTransfer) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetTransfer_InsertPurpose, ClientScreen.Accounts_Voucher_AssetTransfer, InPurpose)
        End Function

        Public Function InsertPayment(ByVal InPay As Parameter_InsertAandLPayment_VoucherAssetTransfer) As Boolean
            'InPay.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetTransfer_InsertPayment, ClientScreen.Accounts_Voucher_AssetTransfer, InPay)
        End Function

        Public Function Insert_ProfileBySP(ByVal AssetType As String, ByVal AssetRefID As String, ByVal AssetNewID As String, ByVal AssetLocID As String, ByVal AssetOwner As String, ByVal AssetOwnerID As String, ByVal AssetUse As String, ByVal AssetQty As Double, ByVal AssetAmt As Double, ByVal CenID As Integer, ByVal TrID As String) As DataTable
            Dim Param As Parameter_Insert_Profile = New Parameter_Insert_Profile
            Param.AssetType = AssetType
            Param.AssetRefID = AssetRefID
            Param.AssetNewID = AssetNewID
            Param.AssetLocID = AssetLocID
            Param.AssetOwner = AssetOwner
            Param.AssetOwnerID = AssetOwnerID
            Param.AssetUse = AssetUse
            Param.AssetQty = AssetQty
            Param.AssetAmt = AssetAmt

            Param.CenID = CenID
            Param.TrID = TrID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.AssetTransfer_Insert_Profiles, ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherAssetTransfer) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetTransfer_UpdateMaster, ClientScreen.Accounts_Voucher_AssetTransfer, UpParam)
        End Function

        Public Function Update_CrossReference(ByVal Cross_Ref_ID As String, ByVal RecID As String) As Boolean
            Dim Param As Param_VoucherAssetTransfer_Update_CrossReference = New Param_VoucherAssetTransfer_Update_CrossReference()
            Param.Cross_Ref_ID = Cross_Ref_ID
            Param.RecID = RecID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetTransfer_Update_CrossReference, ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Function Delete_CrossReference(ByVal Cross_Ref_ID As String) As Boolean
            Dim Param As Param_VoucherAssetTransfer_Update_CrossReference = New Param_VoucherAssetTransfer_Update_CrossReference()
            Param.Cross_Ref_ID = Cross_Ref_ID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetTransfer_Delete_CrossReference, ClientScreen.Accounts_Voucher_AssetTransfer, Param)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Overloads Function DeletePayment(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Overloads Function DeleteMaster(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_AssetTransfer)
        End Function

        Public Function Insert_AssetTransfer_Txn(InParam As Param_Txn_Insert_VoucherAssetTransfer)
            InParam.prev_year_id = cBase._prev_Unaudited_YearID
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.AssetTransfer_Insert_AssetTransfer_Txn, ClientScreen.Accounts_Voucher_AssetTransfer, InParam)
        End Function

        Public Function Update_AssetTransfer_Txn(UpParam As Param_Txn_Update_VoucherAssetTransfer)
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.AssetTransfer_Update_AssetTransfer_Txn, ClientScreen.Accounts_Voucher_AssetTransfer, UpParam)
        End Function

        Public Function Delete_AssetTransfer_Txn(DelParam As Param_Txn_Delete_VoucherAssetTransfer)
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_AssetTransfer)
            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("TR_M_ID ='" + DelParam.MID_Delete + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.AssetTransfer_Delete_AssetTransfer_Txn, ClientScreen.Accounts_Voucher_AssetTransfer, DelParam)
        End Function

    End Class
#End Region
End Class
