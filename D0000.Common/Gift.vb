'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Return_GoldSilver_MiscList

        Public ID As String
        Public Name As String
    End Class
    <Serializable>
    Public Class Return_LocationList

        Public Location_Name As String
        Public AL_ID As String

        Public REC_EDIT_ON As Date
        Public Matched_Type As String
        Public Matched_Name As String
        Public Matched_Instt As String
        Public Final_Amount As Decimal?
    End Class
    <Serializable>
    Public Class Return_GetInsList
        Public ID As String
        Public Name As String
    End Class
    <Serializable>
    Public Class Return_Vehicles_OwnerList
        Public ID As String
        Public Name As String
        Public Organization As String
        Public Status As String
    End Class
    <Serializable>
    Public Class Return_Vehicles_MakeList
        Public ID As String
        Public Name As String
    End Class
    <Serializable>
    Public Class Return_Vehicles_ModelList
        Public ID As String
        Public Name As String
    End Class
    <Serializable>
    Public Class Voucher_Gift
            Inherits SharedVariables
            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            Public Function GetStatus(ByVal Rec_Id As String) As Object
                Return GetRecordStatus(Rec_Id, "TR_M_ID", ClientScreen.Accounts_Voucher_Gift, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
            End Function

        'Shifted
        Public Function GetAssetLocations(Optional Tr_M_ID As String = Nothing) As List(Of Return_LocationList)
            Dim _AL As AssetLocations = New AssetLocations(cBase)
            Dim ret_table As DataTable = _AL.GetList(ClientScreen.Accounts_Voucher_Gift, Nothing, Tr_M_ID)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                Dim loclist = New List(Of Return_LocationList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_LocationList
                    newdata.AL_ID = row.Field(Of String)("AL_ID")
                    newdata.Final_Amount = row.Field(Of Decimal?)("Final_Amount")
                    newdata.REC_EDIT_ON = row.Field(Of Date)("REC_EDIT_ON")
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Matched_Instt = row.Field(Of String)("Matched Instt.")
                    newdata.Matched_Name = row.Field(Of String)("Matched Name")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    loclist.Add(newdata)
                Next
                Return loclist
            End If
        End Function

        'Shifted
        Public Function GetGSItems(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As List(Of Return_GoldSilver_MiscList)

                Dim ret_table As DataTable = GetMisc("GOLD / SILVER ITEMS", ClientScreen.Accounts_Voucher_Gift, MiscNameColumnHead, RecIDColumnHead)
                If (ret_table Is Nothing) Then
                    Return Nothing
                Else
                    Dim misclist = New List(Of Return_GoldSilver_MiscList)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_GoldSilver_MiscList
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        misclist.Add(newdata)
                    Next
                    Return misclist
                End If
            End Function

            'Shifted
            Public Function GetVehicleMake(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As List(Of Return_Vehicles_MakeList)
                Dim Query As String = " SELECT distinct MISC_NAME as " & MiscNameColumnHead & " FROM MISC_INFO WHERE  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') order by misc_name "
                Dim inParam As RealTimeService.Param_GetMiscDetailsCommon = New RealTimeService.Param_GetMiscDetailsCommon()
                inParam.MiscNameColumnHead = MiscNameColumnHead
                inParam.Type = "1"
                Dim ret_table As DataTable = GetMiscDetails(Query, Query, ClientScreen.Accounts_Voucher_Gift, inParam) 'Type = 1
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim makelist = New List(Of Return_Vehicles_MakeList)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_Vehicles_MakeList
                    'newdata.ID = row.Field(Of String)("Id")
                    newdata.Name = row.Field(Of String)("Name")
                        makelist.Add(newdata)
                    Next
                    Return makelist
                End If
            End Function

        'Shifted
        Public Function GetVehicleModels(ByVal Make As String) As List(Of Return_Vehicles_ModelList)
            Dim Query As String = " SELECT MISC_REMARK1 AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') AND MISC_NAME='" & Make & "' ORDER BY MISC_REMARK1 "
            Dim inParam As RealTimeService.Param_GetMiscDetailsCommon = New RealTimeService.Param_GetMiscDetailsCommon()
            inParam.Make = Make
            inParam.Type = "2"
            Dim ret_table As DataTable = GetMiscDetails(Query, Query, ClientScreen.Accounts_Voucher_Gift, inParam) 'Type = 2
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim modellist = New List(Of Return_Vehicles_ModelList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_Vehicles_ModelList
                    'newdata.ID = row.Field(Of String)("ID")
                    newdata.Name = row.Field(Of String)("Name")
                    modellist.Add(newdata)
                Next
                Return modellist
            End If
        End Function

        'Shifted
        Public Function GetItemLedger()
                Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS  CON_LED_TYPE, I.REC_ID AS ITEM_ID  " &
                                       " FROM (Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID) LEFT JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND ((UCASE(I.ITEM_PROFILE) IN ('GOLD','SILVER','OTHER ASSETS','LIVESTOCK','VEHICLES','LAND & BUILDING') AND UCASE(I.ITEM_PROFILE_OPENING)='YES') OR (UCASE(I.ITEM_VOUCHER_TYPE) = 'LAND & BUILDING' AND ITEM_LED_ID IN ('00045','00047')) OR i.REC_ID in ('8d0f8572-2f1c-4261-b165-c1288f14f128','e7e731e3-3d31-4240-a061-60600c6de8dc')) AND UCASE(I.ITEM_TRANS_TYPE)='DEBIT' "
                Dim param As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon
                Return GetItems_Ledger(ClientScreen.Accounts_Voucher_Gift, LocalQuery, param)
            End Function

            'Shifted
            Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
                ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Gift, MiscNameColumnHead, RecIDColumnHead)
                Dim InParam As Param_GetMisc = New Param_GetMisc()
                '  InParam.MiscId = MiscId
                InParam.MiscNameColumnHead = MiscNameColumnHead
                InParam.RecIDColumnHead = RecIDColumnHead
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Gift, InParam)
            End Function

            'Shifted
            Public Function GetInsuranceItems(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As List(Of Return_GetInsList)
                Dim ret_table As DataTable = GetMisc("INSURANCE", ClientScreen.Accounts_Voucher_Gift, MiscNameColumnHead, RecIDColumnHead)
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim inslist = New List(Of Return_GetInsList)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_GetInsList
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        inslist.Add(newdata)
                    Next
                    Return inslist
                End If
            End Function

            'Shifted
            Public Function GetItemsList(ByVal RecIDs As String) As DataTable
                Dim LocalQuery As String = " Select ITEM_NAME , ITEM_TRANS_STMT, ITEM_TRANS_TYPE, ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS In (0, 1, 2) And REC_ID ='" & RecIDs & "' AND UCASE(ITEM_TRANS_TYPE)='CREDIT' "
                Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
                inParam.ItemIDs = RecIDs
                Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_Gift, inParam)
            End Function

            Public Function GetRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByColumn("TR_M_ID", Rec_ID, ClientScreen.Accounts_Voucher_Gift, RealTimeService.Tables.TRANSACTION_INFO)
            End Function

            Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Gift, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
            End Function

            ''' <summary>
            ''' Get Txn Items, Shifted
            ''' </summary>
            ''' <param name="Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Gift_GetTxnItems</remarks>
            Public Function GetTxnItems(ByVal Rec_ID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Gift_GetTxnItems, ClientScreen.Accounts_Voucher_Gift, Rec_ID)
            End Function

            'Shifted
            Public Function GetGoldSilverList(ByVal RecID As String) As DataTable
                Dim _GS As GoldSilver = New GoldSilver(cBase)
                Return _GS.GetList(ClientScreen.Accounts_Voucher_Gift, RecID)
            End Function

            'Shifted
            Public Function GetAssetList(ByVal RecID As String) As DataTable
                Dim _Assets As Assets = New Assets(cBase)
                Return _Assets.GetList(ClientScreen.Accounts_Voucher_Gift, RecID)
            End Function

            'Shifted
            Public Function GetVehiclesList(ByVal RecID As String) As DataTable
                Dim _Veh As Vehicles = New Vehicles(cBase)
                Return _Veh.GetList(ClientScreen.Accounts_Voucher_Gift, RecID)
            End Function

            'Shifted
            Public Function GetLiveStockList(ByVal RecID As String) As DataTable
                Dim _LS As LiveStock = New LiveStock(cBase)
                Return _LS.GetList(ClientScreen.Accounts_Voucher_Gift, RecID)
            End Function

            'Shifted
            Public Function GetParties(Optional Party_Rec_ID As String = Nothing) As DataTable
                Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                Param.Type = "PARTY"
                Param.Party_Rec_ID = Party_Rec_ID
                Dim _Add As Addresses = New Addresses(cBase)
                Return _Add.GetList(ClientScreen.Accounts_Voucher_Gift, Param)
                'Add extra parameter in online call stating that this is a Party List
                'String Type = "PARTY"
            End Function

            'Shifted
            Public Function GetVehicleOwners() As List(Of Return_Vehicles_OwnerList)
                Dim Query As String = " select  C_NAME AS Name,C_ORG_NAME  AS Organization,C_STATUS AS Status,REC_ID AS ID  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = '" & cBase._open_Cen_ID & "'   "
                Dim _Add As Addresses = New Addresses(cBase)

                Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                Param.Type = "VEHICLE OWNER"
                Dim ret_table As DataTable = _Add.GetList(ClientScreen.Accounts_Voucher_Gift, Param)
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim ownlist = New List(Of Return_Vehicles_OwnerList)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_Vehicles_OwnerList
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        newdata.Organization = row.Field(Of String)("Organization")
                        newdata.Status = row.Field(Of String)("Status")
                        ownlist.Add(newdata)
                    Next
                    Return ownlist
                End If
            End Function

            'Shifted
            Public Function GetPartyCities(ByVal CityIDs) As DataTable
                Return GetCitiesByID(ClientScreen.Accounts_Voucher_Gift, "CI_NAME", "REC_ID", CityIDs)
            End Function

            ''' <summary>
            ''' Insert, Shifted
            ''' </summary>
            ''' <param name="InParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Gift_Insert</remarks>
            Public Function Insert(ByVal InParam As Parameter_Insert_VoucherGift) As Boolean
                'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
                Return InsertRecord(RealTimeService.RealServiceFunctions.Gift_Insert, ClientScreen.Accounts_Voucher_Gift, InParam)
            End Function

            ''' <summary>
            ''' Insert MasterInfo, Shifted
            ''' </summary>
            ''' <param name="InMInfo"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Gift_InsertMasterInfo</remarks>
            Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherGift) As Boolean
                'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
                Return InsertRecord(RealTimeService.RealServiceFunctions.Gift_InsertMasterInfo, ClientScreen.Accounts_Voucher_Gift, InMInfo)
            End Function

            ''' <summary>
            ''' Insert Purpose, Shifted
            ''' </summary>
            ''' <param name="InPurpose"></param>
            ''' <returns></returns>
            ''' <remarks> RealServiceFunctions.Gift_InsertPurpose</remarks>
            Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherGift) As Boolean
                'InPurpose.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
                Return InsertRecord(RealTimeService.RealServiceFunctions.Gift_InsertPurpose, ClientScreen.Accounts_Voucher_Gift, InPurpose)
            End Function

            ''' <summary>
            ''' Insert Item, Shifted
            ''' </summary>
            ''' <param name="InItem"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Gift_InsertItem</remarks>
            Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherGift) As Boolean
                'InItem.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
                Return InsertRecord(RealTimeService.RealServiceFunctions.Gift_InsertItem, ClientScreen.Accounts_Voucher_Gift, InItem)
            End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Gift_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherGift) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Gift)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Gift_UpdateMaster, ClientScreen.Accounts_Voucher_Gift, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Gift)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Gift)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Gift)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_Gift)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Gift)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Gift)
        End Function

        Public Overloads Function DeleteMaster(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Gift)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Gift)
        End Function

        Public Function InsertGift_Txn(InParam As Param_Txn_Insert_VoucherGift) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.Gift_InsertGift_Txn, ClientScreen.Accounts_Voucher_Gift, InParam)
            End Function

        Public Function UpdateGift_Txn(UpParam As Param_Txn_Update_VoucherGift) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateMaster.RecID, ClientScreen.Accounts_Voucher_Gift)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Gift_UpdateGift_Txn, ClientScreen.Accounts_Voucher_Gift, UpParam)
        End Function

        Public Function DeleteGift_Txn(DelParam As Param_Txn_Delete_VoucherGift) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_Gift)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + DelParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Gift_DeleteGift_Txn, ClientScreen.Accounts_Voucher_Gift, DelParam)
        End Function
    End Class
#End Region
    End Class

