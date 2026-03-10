'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "--Profile--"
    <Serializable>
    Public Class LiveStock
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <remarks>RealServiceFunctions.Livestock_UpdateAssetLocationIfNotPresent</remarks>
        Public Sub UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String)
            UpdateRecord(RealTimeService.RealServiceFunctions.Livestock_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        End Sub

        'Shifted
        Public Function GetLiveStockItems(ByVal IdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("LIVESTOCK", ClientScreen.Profile_LiveStock, IdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetOpeningProfileLiveStockItems(ByVal IdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("LIVESTOCK", ClientScreen.Profile_LiveStock, IdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetInsuranceCompanies(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("INSURANCE", ClientScreen.Profile_LiveStock, NameColumnHead, RecIdColumnHead)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_LiveStock, RealTimeService.Tables.LIVE_STOCK_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Get LiveStock List for Current Center , Shifted/Replaced
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_GetList_Livestock = New Param_GetList_Livestock()
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Livestock_GetList, ClientScreen.Profile_LiveStock, Param)
        End Function

        ''' <summary>
        ''' Gets Livestock Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetProfileListing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Livestock_GetProfileListing, ClientScreen.Profile_LiveStock, parameter)
        End Function

        'To Be Removed
        'Public Function GetList(ByVal onlineQuery As String, ByVal localQuery As String, ByVal Screen As ClientScreen) As DataTable
        '    Return GetListOfRecords(onlineQuery, localQuery, Screen, RealTimeService.Tables.LIVE_STOCK_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        ''' <summary>
        ''' Common Function, Manipulated, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_GetList_Common</remarks>
        Public Function GetList(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Livestock_GetList_Common, Screen, parameter)
        End Function

        Public Function GetListByID(ByVal Screen As ClientScreen, Optional ByVal parameter As Param_Livestock_GetList_Common = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Livestock_GetList_Common_ByRecID, Screen, parameter)
        End Function

        ''' <summary>
        '''  Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Livestock_GetListByCondition, screen, OtherCondition)
        End Function

        ''' <summary>
        ''' Get Transactions, Shifted
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_GetTransactions</remarks>
        Public Function GetTransactions(ByVal Rec_IDs As String, YearID As Integer) As DataTable
            Dim oParam As Param_LS_GetTransactions = New Param_LS_GetTransactions
            oParam.Rec_IDs = Rec_IDs
            oParam.YearID = YearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Livestock_GetTransactions, ClientScreen.Profile_GoldSilver, oParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_LiveStock, RealTimeService.Tables.LIVE_STOCK_INFO, "LS_CEN_ID")
        End Function

        Public Function IsLivestockCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_LiveStock, Tables.LIVE_STOCK_INFO)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_LiveStock) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Livestock_Insert, ClientScreen.Profile_LiveStock, InParam)
        End Function

        ''' <summary>
        ''' InsertTRIDAndTRSrNo, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_InsertTRIDAndTRSrNo</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_LiveStock) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Livestock_InsertTRIDAndTRSrNo, InParam1.screen, InParam1)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_LiveStock) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_LiveStock)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Livestock_Update, ClientScreen.Profile_LiveStock, UpParam)
        End Function

        Public Function UpdateInsuranceDetail(ByVal UpParam As Parameter_Update_LiveStock) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Insurance_ChangeDetail)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Livestock_UpdateInsuranceDetail, ClientScreen.Profile_Insurance_ChangeDetail, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_LiveStock)

            Return DeleteRecord(Rec_Id, Tables.LIVE_STOCK_INFO, ClientScreen.Profile_LiveStock)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_LiveStock)

            Return DeleteByCondition("LS_TR_ID    ='" & Rec_Id & "'", Tables.LIVE_STOCK_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.LIVE_STOCK_INFO, ClientScreen.Profile_LiveStock)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.LIVE_STOCK_INFO, ClientScreen.Profile_LiveStock)
            Return Locked
        End Function

    End Class
#End Region
End Class
