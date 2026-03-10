'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Assets
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted/ Replaced
        Public Function GetAssetList(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Dim LocalQuery As String = "SELECT REC_ID AS " & RecIdColumnHead & " ,ITEM_NAME as " & NameColumnHead & ", ITEM_LED_ID from ITEM_INFO where UCASE(ITEM_PROFILE)='OTHER ASSETS' AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.RecIdColumnHead = RecIdColumnHead
            inParam.NameColumnHead = NameColumnHead
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Profile_Assets, inParam)
        End Function

        ''' <summary>
        ''' Gets Asset Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetProfileListing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Assets_GetProfileListing, ClientScreen.Profile_Assets, parameter)
        End Function
        'Shifted
        Public Function GetOpeningProfileAssetList(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("OTHER ASSETS", ClientScreen.Profile_Assets, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetLedgers() As DataTable
            Return GetLedgersList(ClientScreen.Profile_Assets)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_Assets_GetList = New Param_Assets_GetList()
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetList, ClientScreen.Profile_Assets, Param)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetListByCondition, screen, OtherCondition)
        End Function

        'Common Function, To Be Removed
        'Commented As not called from anywhere
        'Public Function GetList(ByVal Query As String, ByVal screen As ClientScreen) As DataTable
        '    Return GetListOfRecords(Query, Query, screen, RealTimeService.Tables.ASSET_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        'Common Function, Manipulated
        ''' <summary>
        ''' Get List Common Function, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetList_Common</remarks>
        Public Function GetList(ByVal screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetList_Common, screen, parameter)
        End Function

        Public Function GetListByID(ByVal screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetList_Common_ByRecID, screen, parameter)
        End Function

        'Public Function Get_Asset_Item_Closing_Detail(ByVal Asset_ID As String, ByVal Profile As String, Optional ByVal M_ID As String = "") As DataTable
        '    Dim Param As Param_Asset_Common = New Param_Asset_Common()
        '    Param.CEN_ID = cBase._open_Cen_ID
        '    Param.YEAR_ID = cBase._open_Year_ID
        '    Param.ASSET_ID = Asset_ID
        '    Param.PROFILE = Profile
        '    Param.M_ID = M_ID
        '    Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Data_Get_Asset_Item_Closing_Detail, ClientScreen.Profile_Assets, Param)
        'End Function

        ''' <summary>
        ''' Get Transactions, Shifted
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetTransactions</remarks>
        Public Function GetTransactions(ByVal Rec_IDs As String, YearID As Integer, Optional NextUnauditedYearId As Integer = Nothing) As DataTable
            Dim oParam As Param_Assets_GetTransactions = New Param_Assets_GetTransactions
            oParam.Rec_IDs = Rec_IDs
            oParam.YearID = YearID
            oParam.NextUnauditedYearID = NextUnauditedYearId
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetTransactions, ClientScreen.Profile_GoldSilver, oParam)
        End Function

        Public Function GetTransactions(ByVal cTable As RealTimeService.Tables, YearID As Integer, Optional NextUnauditedYearId As Integer = Nothing) As DataTable
            Dim oParam As Param_Assets_GetTransactions_ByTable = New Param_Assets_GetTransactions_ByTable
            oParam.Table = cTable
            oParam.YearID = YearID
            oParam.NextUnauditedYearID = NextUnauditedYearId
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetTransactions_ByTable, ClientScreen.Profile_GoldSilver, oParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Assets, RealTimeService.Tables.ASSET_INFO, "AI_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_Assets, RealTimeService.Tables.ASSET_INFO, Common.ClientDBFolderCode.DATA)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Assets_GetRecord, ClientScreen.Profile_Assets, Rec_ID)
        End Function

        Public Function GetRecId_From_OrgID(ByVal Org_Rec_ID As String) As Object
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_Assets, RealTimeService.Tables.ASSET_INFO, Common.ClientDBFolderCode.DATA)
            Return GetScalarBySP(RealTimeService.RealServiceFunctions.Assets_GetRecId_From_OrgID, ClientScreen.Profile_Assets, Org_Rec_ID)
        End Function

        Public Function IsAssetCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Assets, Tables.ASSET_INFO)
        End Function

        Public Function Get_Asset_Ref_MaxEditOn(ByVal Asset_RecID As String) As Object
            Return GetScalarBySP(RealTimeService.RealServiceFunctions.Assets_Get_Asset_Ref_MaxEditOn, ClientScreen.Accounts_Vouchers, Asset_RecID)
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <remarks>RealServiceFunctions.Assets_UpdateAssetLocationIfNotPresent</remarks>
        Public Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Assets_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Assets) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Assets_Insert, ClientScreen.Profile_Assets, InParam)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_InsertTRIDAndTRSrNo</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_Assets) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Assets_InsertTRIDAndTRSrNo, InParam1.Screen, InParam1)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Assets) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Assets)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Assets_Update, ClientScreen.Profile_Assets, UpParam)
        End Function

        Public Function UpdateLocation(ByVal UpParam As Parameter_Update_Assets_Location) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Assets_UpdateLocation, ClientScreen.Profile_ChangeLocation, UpParam)
        End Function

        Public Function UpdateInsuranceValue(ByVal UpParam As Parameter_Update_Assets) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Insurance_ChangeValue)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Assets_UpdateInsuranceValue, ClientScreen.Profile_Insurance_ChangeValue, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Assets)

            DeleteByCondition("asset_id    ='" & Rec_Id & "'", Tables.ASSET_IMAGES, ClientScreen.Profile_Assets)
            Return DeleteRecord(Rec_Id, Tables.ASSET_INFO, ClientScreen.Profile_Assets)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Assets)

            DeleteByCondition("asset_id    ='" & Rec_Id & "'", Tables.ASSET_IMAGES, ClientScreen.Profile_Assets)
            Return DeleteByCondition("AI_TR_ID    ='" & Rec_Id & "'", Tables.ASSET_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.ASSET_INFO, ClientScreen.Profile_Assets)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.ASSET_INFO, ClientScreen.Profile_Assets)
            Return Locked
        End Function
    End Class
    <Serializable>
    Public Class AssetLocations
        Inherits SharedVariables
#Region "Report Classes"
        <Serializable>
        Public Class LocationInfoReport
            ''' <summary>
            ''' Original code field name is Sr.
            ''' </summary>
            Public Property Sr As Integer
            Public Property Institute As String
            Public Property UID As String
            ''' <summary>
            ''' Original Column name is Location Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Location_Name As String
            ''' <summary>
            ''' Original Column Name is Location Remarks
            ''' </summary>
            ''' <returns></returns>
            Public Property Location_Remarks As String
            ''' <summary>
            ''' Original Column Name is Matched Type
            ''' </summary>
            ''' <returns></returns>
            Public Property Matched_Type As String
            ''' <summary>
            ''' Original field Name is Property Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Name As String
            ''' <summary>
            ''' Original Column name is Property Address
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Address As String
            ''' <summary>
            ''' Original column name is Property Category
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Category As String
            ''' <summary>
            ''' Original Column name is Property Type
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Type As String
            ''' <summary>
            ''' Original Column name is Property Use
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Use As String
            ''' <summary>
            ''' Original Column Name is Property Instt
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Instt As String
            ''' <summary>
            ''' Original Column name is Pro.ID
            ''' </summary>
            ''' <returns></returns>
            Public Property Pro_ID As String
            ''' <summary>
            ''' Original Column Name is Property Status
            ''' </summary>
            ''' <returns></returns>
            Public Property Property_Status As String
            ''' <summary>
            ''' Original Column Name is Service Place Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Service_Place_Name As String
            ''' <summary>
            ''' Original column name is Service Place Type
            ''' </summary>
            ''' <returns></returns>
            Public Property Service_Place_Type As String
            ''' <summary>
            ''' Original column name is Service Place Instt
            ''' </summary>
            ''' <returns></returns>
            Public Property Service_Place_Instt As String
            ''' <summary>
            ''' Original column name is Ser.ID
            ''' </summary>
            ''' <returns></returns>
            Public Property Ser_ID As String
            Public Property ID As String

        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Checks Usage of Location
        ''' </summary>
        ''' <param name="Loc_ID">Rec_ID of Location</param>
        ''' <returns>Page where current location is used</returns>
        ''' <remarks></remarks>
        Public Function CheckLocationUsage(Loc_ID As String, Optional Exclude_sold_Tf As Boolean = True) As String
            Dim MaxValue As DataTable = Nothing
            Dim DeleteAllow As Boolean = True : Dim UsedPage As String = ""
            If DeleteAllow Then
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInAssets(ClientScreen.Profile_Assets, Loc_ID, Exclude_sold_Tf)
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Movable Asset Information..." : If MaxValue.Rows(0)("CEN_UID") <> cBase._open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '2
            If DeleteAllow Then
                'MaxValue = 0
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInGS(ClientScreen.Profile_GoldSilver, Loc_ID, Exclude_sold_Tf)
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Gold / Silver Information..." : If MaxValue.Rows(0)("CEN_UID") <> cBase._open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '3
            If DeleteAllow Then
                'MaxValue = 0
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInConsumables(ClientScreen.Profile_StockOfConsumables, Loc_ID, Exclude_sold_Tf)
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Stock of Consumables..." : If MaxValue.Rows(0)("CEN_UID") <> cBase._open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '4
            If DeleteAllow Then
                ' MaxValue = 0
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInLiveStock(ClientScreen.Profile_LiveStock, Loc_ID, Exclude_sold_Tf)
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Livestock..." : If MaxValue.Rows(0)("CEN_UID") <> cBase._open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            '5
            If DeleteAllow Then
                ' MaxValue = 0
                MaxValue = cBase._AssetLocDBOps.GetLocationCountInVehicles(ClientScreen.Profile_Vehicles, Loc_ID, Exclude_sold_Tf)
                If MaxValue Is Nothing Then Throw New Exception(Common_Lib.Messages.SomeError)
                If MaxValue.Rows.Count > 0 Then DeleteAllow = False : UsedPage = "Vehicle Information..." : If MaxValue.Rows(0)("CEN_UID") <> cBase._open_UID_No Then UsedPage = UsedPage & "(center:" & MaxValue.Rows(0)("CEN_UID") & ")"
            End If
            Return UsedPage
        End Function

        Public Function Delete(ByVal RecID As String, ByVal screen As ClientScreen) As Boolean
            Return DeleteRecord(RecID, RealTimeService.Tables.ASSET_LOCATION_INFO, screen)
        End Function

        Public Function DeleteBySP(ByVal SP_RecID As String, ByVal screen As ClientScreen) As Boolean
            Return DeleteByCondition(" SP_REC_ID = '" & SP_RecID & "' ", RealTimeService.Tables.ASSET_LOCATION_INFO, screen)
        End Function

        Public Function DeleteByLB(ByVal LB_RecID As String, ByVal screen As ClientScreen) As Boolean
            Return DeleteByCondition(" LB_REC_ID = '" & LB_RecID & "' ", RealTimeService.Tables.ASSET_LOCATION_INFO, screen)
        End Function

        ''' <summary>
        ''' Gets Assets location for a CenId, Shifted
        ''' Value Member :AL_ID, Display Member :Location Name
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetList</remarks>
        Public Function GetList(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, Location_Rec_ID As String, Tr_M_ID As String, Optional Store_Dept_ID As Int32 = 0, Optional LB_ID As String = "", Optional SP_ID As String = "") As DataTable
            Dim param As Param_AssetLocation_GetList = New Param_AssetLocation_GetList
            param.Location_Rec_ID = Location_Rec_ID
            param.Tr_M_ID = Tr_M_ID
            param.Prev_Year_ID = cBase._prev_Unaudited_YearID
            If Store_Dept_ID > 0 Then param.Store_Dept_ID = Store_Dept_ID
            If LB_ID.Length > 0 Then param.LB_REC_ID = LB_ID
            If SP_ID.Length > 0 Then param.SP_REC_ID = SP_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetList, Screen, param)
        End Function
        Public Function GetStockLocationList(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, Location_Rec_ID As String, Tr_M_ID As String, Optional Store_Dept_ID As Int32 = 0, Optional LB_ID As String = "", Optional SP_ID As String = "", Optional PassCenID As Boolean = True) As DataTable
            Dim param As Param_AssetLocation_GetList = New Param_AssetLocation_GetList
            param.Location_Rec_ID = Location_Rec_ID
            param.Tr_M_ID = Tr_M_ID
            param.Prev_Year_ID = cBase._prev_Unaudited_YearID
            If Store_Dept_ID > 0 Then param.Store_Dept_ID = Store_Dept_ID
            If LB_ID.Length > 0 Then param.LB_REC_ID = LB_ID
            If SP_ID.Length > 0 Then param.SP_REC_ID = SP_ID
            param.PassCenID = PassCenID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetStockLocationList, Screen, param)
        End Function
        Public Function GetListByCenID(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal Cen_ID As Integer) As DataTable
            Dim param As Param_AssetLocation_GetList = New Param_AssetLocation_GetList
            param.Prev_Year_ID = cBase._prev_Unaudited_YearID
            Return GetDataListOfRecordsWithCenID(RealTimeService.RealServiceFunctions.AssetLocations_GetList, Screen, Cen_ID, param)
        End Function

        Public Function GetListByLBID(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal LB_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetListByLBID, Screen, LB_ID)
        End Function

        Public Function GetListBySPID(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal SP_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetListBySPID, Screen, SP_ID)
        End Function

        ''' <summary>
        ''' Returns Location Name, Othe Details, Location Main and Rec ID, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetFullList</remarks>
        Public Function GetFullList(ByVal Screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetFullList, Screen)
        End Function

        Public Function GetLocationMapping(ByVal screen As ClientScreen, ByVal BK_PAD_NO As String) As List(Of LocationInfoReport)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationMatching, screen, BK_PAD_NO)
            Dim _Locations As List(Of LocationInfoReport) = New List(Of LocationInfoReport)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New LocationInfoReport
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Institute = row.Field(Of String)("Institute")
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Location_Remarks = row.Field(Of String)("Location Remarks")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    newdata.Property_Address = row.Field(Of String)("Property Address")
                    newdata.Property_Category = row.Field(Of String)("Property Category")
                    newdata.Property_Instt = row.Field(Of String)("Property Instt")
                    newdata.Property_Name = row.Field(Of String)("Property Name")
                    newdata.Property_Status = row.Field(Of String)("Property Status")
                    newdata.Property_Type = row.Field(Of String)("Property Type")
                    newdata.Property_Use = row.Field(Of String)("Property Use")
                    newdata.Pro_ID = row.Field(Of String)("Pro.ID")
                    newdata.Service_Place_Instt = row.Field(Of String)("Service Place Instt")
                    newdata.Service_Place_Name = row.Field(Of String)("Service Place Name")
                    newdata.Service_Place_Type = row.Field(Of String)("Service Place Type")
                    newdata.Ser_ID = row.Field(Of String)("Ser.ID")
                    newdata.Sr = row.Field(Of Int64)("Sr.")
                    newdata.UID = row.Field(Of String)("UID")
                    _Locations.Add(newdata)
                Next
            End If
            Return _Locations
        End Function

        Public Function GetLocationMappedCount(ByVal screen As ClientScreen, ByVal RecID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationsMatchedCount, screen, RecID)
        End Function

        Public Function GetLocationMappingCount(ByVal BK_PAD_NO As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationMatchingCount, ClientScreen.Profile_Core, BK_PAD_NO)
        End Function
        ''' <summary>
        ''' Create AssetLocation Form : checks if there is a same named asset location already, Shifted
        ''' </summary>
        ''' <param name="LocationName"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetRecordCountByName</remarks>
        Public Function GetRecordCountByName(ByVal LocationName As String, ByVal screen As ClientScreen, CEN_BK_PAD_NO As String, Optional TrID As String = "") As Object
            Dim param As Param_GetRecordCountByName = New Param_GetRecordCountByName
            param.LocationName = LocationName
            param.TrID = TrID
            param.CEN_BK_PAD_NO = CEN_BK_PAD_NO
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetRecordCountByName, screen, param)
        End Function

        Public Function GetRecordCountByName_CurrentUID(ByVal LocationName As String, ByVal screen As ClientScreen, Optional TrID As String = "") As Object
            Dim param As Param_GetRecordCountByName_CurrUID = New Param_GetRecordCountByName_CurrUID
            param.LocationName = LocationName
            param.TrID = TrID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetRecordCountByName_CurrentUID, screen, param)
        End Function

        ''' <summary>
        ''' Create AssetLocation Form : Bind 
        ''' </summary>
        ''' <param name="RecId"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetRecord(ByVal RecId As String, ByVal screen As ClientScreen) As DataTable
            Return GetRecordByID(RecId, screen, RealTimeService.Tables.ASSET_LOCATION_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        ''' <summary>
        ''' Create AssetLocation Form : checks if record is locked
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetStatus(ByVal RecID As String, ByVal screen As ClientScreen) As Object
            Return GetRecordStatus(RecID, ClientScreen.Core_Add_AssetLocation, RealTimeService.Tables.ASSET_LOCATION_INFO, "AL_CEN_ID")
        End Function

        'Gets Property ID mapped to given Location Id
        Public Function GetPropertyID(ByVal LocID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetPropertyID, ClientScreen.Core_Add_AssetLocation, LocID)
        End Function

        ' ''' <summary>
        ' ''' Inserts Default Asset Location if not already present, Shifted
        ' ''' </summary>
        ' ''' <param name="Screen"></param>
        ' ''' <remarks>RealServiceFunctions.AssetLocations_InsertIfDefaultNotPresent</remarks>
        'Public Function InsertIfDefaultNotPresent(ByVal Screen As Common_Lib.Common_Lib.RealTimeService.ClientScreen) As Boolean
        '    Return InsertRecord(RealTimeService.RealServiceFunctions.AssetLocations_InsertIfDefaultNotPresent, Screen, cBase._open_Cen_Name)
        '    Return True 'As default location is already present 
        'End Function

        ''' <summary>
        ''' Returns Defualt Asset Location For Current Center , Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetDefaultLocation</remarks>
        Public Function GetDefaultLocation(ByVal Screen As Common_Lib.RealTimeService.ClientScreen) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetDefaultLocation, Screen)
        End Function

        ''' <summary>
        ''' Add new Asset Location, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="name"></param>
        ''' <param name="OtherDetails"></param>
        ''' <param name="Status_Action"></param>
        ''' <remarks>RealServiceFunctions.AssetLocations_Insert</remarks>
        Public Function Insert(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal name As String, ByVal OtherDetails As String, ByVal Status_Action As String,
                               Optional SP_ID As String = "", Optional LB_ID As String = "", Optional ByVal maxCapacity As Int32 = 0, Optional ByVal ac_nonac As String = "",
                               Optional ByVal category As String = "", Optional ByVal floor As String = "") As Boolean
            Dim Param As Param_AssetLoc_Insert = New Param_AssetLoc_Insert()
            Param.name = name
            Param.OtherDetails = OtherDetails
            Param.Status_Action = Status_Action
            Param.Match_LB_ID = LB_ID
            Param.Match_SP_ID = SP_ID
            Param.max_Capacity = maxCapacity
            Param.ac_nonacField = ac_nonac
            Param.categoryField = category
            Param.floorField = floor
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetLocations_Insert, screen, Param)
        End Function

        Public Function Insert_AllSisterUIDs(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal name As String, ByVal OtherDetails As String, ByVal Status_Action As String, Optional SP_ID As String = "", Optional LB_ID As String = "") As Boolean
            Dim Param As Param_AssetLoc_Insert = New Param_AssetLoc_Insert()
            Param.name = name
            Param.OtherDetails = OtherDetails
            Param.Status_Action = Status_Action
            Param.Match_LB_ID = LB_ID
            Param.Match_SP_ID = SP_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.AssetLocations_Insert_AllSisterUIDs, screen, Param)
        End Function

        ''' <summary>
        ''' Update Asset Location, Shifted 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="name"></param>
        ''' <param name="OtherDetails"></param>
        ''' <param name="Status_Action"></param>
        ''' <remarks>RealServiceFunctions.AssetLocations_Update</remarks>
        Public Function UpdateByReference(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal name As String, ByVal OtherDetails As String, ByVal Status_Action As String, ByVal LB_Rec_ID As String, ByVal SP_Rec_ID As String) As Boolean
            Dim Param As Param_AssetLoc_Update = New Param_AssetLoc_Update()
            Param.name = name
            Param.OtherDetails = OtherDetails
            Param.Match_LB_ID = LB_Rec_ID
            Param.Match_SP_ID = SP_Rec_ID
            '  Param.Status_Action = Status_Action
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetLocations_UpdateByReference, screen, Param)
        End Function


        Public Function Update(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal name As String, ByVal OtherDetails As String, ByVal Status_Action As String, ByVal Rec_ID As String) As Boolean
            Dim Param As Param_AssetLoc_Update = New Param_AssetLoc_Update()
            Param.name = name
            Param.OtherDetails = OtherDetails
            Param.RecId = Rec_ID
            'Param.Status_Action = Status_Action
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetLocations_UpdateByReference, screen, Param)
        End Function

        Public Function UpdateMapping(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal Mapping As String, ByVal Map_LB_ID As String, ByVal Map_SP_ID As String, ByVal RecId As String) As Boolean
            Dim Param As Param_AssetLoc_Update = New Param_AssetLoc_Update()
            Param.Matching = Mapping
            Param.Match_LB_ID = Map_LB_ID
            Param.Match_SP_ID = Map_SP_ID
            Param.RecId = RecId
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetLocations_UpdateMatching, screen, Param)
        End Function

        Public Function Update_Global(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal name As String, ByVal OtherDetails As String, ByVal Mapping As String,
                                      ByVal Map_LB_ID As String, ByVal Map_SP_ID As String, ByVal RecId As String, Optional ByVal max_Capacity As Int32 = 0,
                                      Optional ByVal ac_nonac As String = "", Optional ByVal category As String = "", Optional ByVal floor As String = "") As Boolean
            Dim Param As Param_AssetLoc_Update = New Param_AssetLoc_Update()
            Param.name = name
            Param.OtherDetails = OtherDetails
            Param.Matching = Mapping
            Param.Match_LB_ID = Map_LB_ID
            Param.Match_SP_ID = Map_SP_ID
            Param.RecId = RecId
            Param.max_Capacity = max_Capacity
            Param.ac_nonacField = ac_nonac
            Param.categoryField = category
            Param.floorField = floor
            'Param.Status_Action = Status_Action
            Return UpdateRecord(RealTimeService.RealServiceFunctions.AssetLocations_Update, screen, Param)
        End Function

        ''' <summary>
        ''' Gets Location Count In Assets, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="LocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInAssets</remarks>
        Public Function GetLocationCountInAssets(ByVal screen As ClientScreen, ByVal LocationID As String, Optional Exclude_sold_Tf As Boolean = True) As DataTable
            Dim param As Param_Check_Location_Count = New Param_Check_Location_Count
            param.LocationID = LocationID
            param.Exclude_Sold_TF = Exclude_sold_Tf
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInAssets, screen, param)
        End Function

        ''' <summary>
        ''' Gets Location Count In Gold Silver, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="LocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInGS</remarks>
        Public Function GetLocationCountInGS(ByVal screen As ClientScreen, ByVal LocationID As String, Optional Exclude_sold_Tf As Boolean = True) As DataTable
            Dim param As Param_Check_Location_Count = New Param_Check_Location_Count
            param.LocationID = LocationID
            param.Exclude_Sold_TF = Exclude_sold_Tf
            '  Dim Query As String = "SELECT COUNT(GS_LOC_AL_ID), CEN_UID FROM GOLD_SILVER_INFO AS GS INNER JOIN CENTRE_INFO AS CI ON GS_CEN_ID = CEN_ID  WHERE REC_STATUS IN (0,1,2) AND GS_CEN_ID='" & cBase._open_Cen_ID & "' AND GS_LOC_AL_ID  = '" & LocationID & "' "
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInGS, screen, param)
        End Function

        ''' <summary>
        ''' Gets Location Count In Consumables, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="LocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInConsumables</remarks>
        Public Function GetLocationCountInConsumables(ByVal screen As ClientScreen, ByVal LocationID As String, Optional Exclude_sold_Tf As Boolean = True) As DataTable
            Dim param As Param_Check_Location_Count = New Param_Check_Location_Count
            param.LocationID = LocationID
            param.Exclude_Sold_TF = Exclude_sold_Tf
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInConsumables, screen, param)
        End Function

        ''' <summary>
        ''' Gets Location Count In LiveStock, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="LocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInLiveStock</remarks>
        Public Function GetLocationCountInLiveStock(ByVal screen As ClientScreen, ByVal LocationID As String, Optional Exclude_sold_Tf As Boolean = True) As DataTable
            Dim param As Param_Check_Location_Count = New Param_Check_Location_Count
            param.LocationID = LocationID
            param.Exclude_Sold_TF = Exclude_sold_Tf
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInLiveStock, screen, param)
        End Function

        ''Removed alongside the removal of TP_LOC_AL_ID FROM TELEPHONE_INFO
        ' ''' <summary>
        ' ''' Gets Location Count In Telephones, Shifted
        ' ''' </summary>
        ' ''' <param name="screen"></param>
        ' ''' <param name="LocationID"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInTelephones</remarks>
        'Public Function GetLocationCountInTelephones(ByVal screen As ClientScreen, ByVal LocationID As String) As Object
        '    Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInTelephones, screen, LocationID)
        'End Function

        ''' <summary>
        ''' Gets Location Count In Vehicles, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="LocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInVehicles</remarks>
        Public Function GetLocationCountInVehicles(ByVal screen As ClientScreen, ByVal LocationID As String, Optional Exclude_sold_Tf As Boolean = True) As DataTable
            Dim param As Param_Check_Location_Count = New Param_Check_Location_Count
            param.LocationID = LocationID
            param.Exclude_Sold_TF = Exclude_sold_Tf
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_GetLocationCountInVehicles, screen, param)
        End Function
    End Class
End Class
