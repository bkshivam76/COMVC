'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "--Profile--"
    <Serializable>
    Public Class GoldSilver
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetGoldSilverItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("GOLD", "SILVER", ClientScreen.Profile_GoldSilver, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetGoldSilverOpeningProfileItems(ByVal ProfileItem As String, ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems(ProfileItem, ClientScreen.Profile_GoldSilver, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetGoldSilverMisc(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GOLD / SILVER ITEMS", ClientScreen.Profile_GoldSilver, MiscNameColumnHead, RecIDColumnHead)
        End Function

        ''' <summary>
        ''' Get List, Shifted/Replaced
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_GoldSilver_GetList = New Param_GoldSilver_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.GoldSilver_GetList, ClientScreen.Profile_GoldSilver, Param)
        End Function

        ''' <summary>
        ''' Gets GoldSilver Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetProfileListing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.GoldSilver_GetProfileListing, ClientScreen.Profile_GoldSilver, parameter)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.GoldSilver_GetListByCondition, screen, OtherCondition)
        End Function

        'Common Function, To Be Removed
        'Public Function GetList(ByVal onlineQuery As String, ByVal LocalQuery As String, ByVal screen As ClientScreen) As DataTable
        '    Return GetListOfRecords(onlineQuery, LocalQuery, screen, RealTimeService.Tables.GOLD_SILVER_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        'Manipulated
        ''' <summary>
        ''' Common Function, Manipulated, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetList_Common</remarks>
        Public Function GetList(ByVal screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.GoldSilver_GetList_Common, screen, parameter)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>GoldSilver_GetList_Common_ByRecID</remarks>
        ''' 
        Public Function GetListByID(ByVal screen As ClientScreen, Optional ByVal parameter As Param_GoldSilver_GetList_Common = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.GoldSilver_GetList_Common_ByRecID, screen, parameter)
        End Function

        ''' <summary>
        ''' Get Transactions, Shifted
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetTransactions</remarks>
        Public Function GetTransactions(ByVal Rec_IDs As String, YearID As String) As DataTable
            Dim oParam As Param_GS_GetTransactions = New Param_GS_GetTransactions
            oParam.Rec_IDs = Rec_IDs
            oParam.YearID = YearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.GoldSilver_GetTransactions, ClientScreen.Profile_GoldSilver, oParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_GoldSilver, RealTimeService.Tables.GOLD_SILVER_INFO, "GS_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_GoldSilver, RealTimeService.Tables.GOLD_SILVER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function IsGSCarriedForward(ByVal Rec_ID As String, ByVal recYearID As String) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_GoldSilver, Tables.GOLD_SILVER_INFO)
        End Function

        Public Function IsTBImportedCentre() As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.Data_IsTBImportedCentre, ClientScreen.Profile_GoldSilver)
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <remarks>RealServiceFunctions.GoldSilver_UpdateAssetLocationIfNotPresent</remarks>
        Public Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.GoldSilver_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_GoldSilver) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.GoldSilver_Insert, ClientScreen.Profile_GoldSilver, InParam)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_InsertTRIDAndTRSRNo</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSRNo_GoldSilver) As Boolean
            Dim RecID As String = Guid.NewGuid.ToString
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.GoldSilver_InsertTRIDAndTRSRNo, InParam1.Screen, InParam1)
        End Function

        ''' <summary>
        ''' Updates GoldSilver, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_GoldSilver) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_GoldSilver)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.GoldSilver_Update, ClientScreen.Profile_GoldSilver, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_GoldSilver)

            Return DeleteRecord(Rec_Id, Tables.GOLD_SILVER_INFO, ClientScreen.Profile_GoldSilver)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_GoldSilver)

            Return DeleteByCondition("GS_TR_ID    ='" & Rec_Id & "'", Tables.GOLD_SILVER_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.GOLD_SILVER_INFO, ClientScreen.Profile_GoldSilver)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.GOLD_SILVER_INFO, ClientScreen.Profile_GoldSilver)
            Return Locked
        End Function

    End Class

#End Region
End Class
