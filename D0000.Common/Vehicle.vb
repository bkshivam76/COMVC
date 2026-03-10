'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class Vehicles
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetVehicles(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("VEHICLES", ClientScreen.Profile_Vehicles, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetOpeningProfile_Vehicles(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("VEHICLES", ClientScreen.Profile_Vehicles, RecIdColumnHead, NameColumnHead)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetOwners_List(Optional Owner_Rec_ID As String = Nothing) As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            Param.Party_Rec_ID = Owner_Rec_ID
            Return _addresses.GetList(ClientScreen.Profile_Vehicles, Param)
        End Function

        'Shifted
        Public Function GetVehicle_Makes() As DataTable
            Dim Query As String = " SELECT DISTINCT MISC_NAME AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') ORDER BY MISC_NAME "
            Return GetMisc_Common(Query, Query, ClientScreen.Profile_Vehicles)
        End Function

        'Shifted
        Public Function GetVehicle_Models(ByVal Make As String) As DataTable
            Dim Query As String = " SELECT MISC_REMARK1 AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') AND MISC_NAME='" & Make & "' ORDER BY MISC_REMARK1 "
            Return GetMisc_Common(Query, Query, ClientScreen.Profile_Vehicles, Make)
        End Function

        'Shifted
        Public Function GetInsuranceCompanies(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("INSURANCE", ClientScreen.Profile_Vehicles, NameColumnHead, RecIdColumnHead)
        End Function

        ''' <summary>
        ''' Get Vehicle List, Shifted/Replaced
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_Vehicles_GetList = New Param_Vehicles_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vehicles_GetList, ClientScreen.Profile_Vehicles, Param)
        End Function

        ''' <summary>
        ''' Gets vehicles Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetProfileListing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Vehicles_GetProfileListing, ClientScreen.Profile_Vehicles, parameter)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Dim Param As Param_Vehicles_GetListByCondition = New Param_Vehicles_GetListByCondition()
            Param.openInsID = cBase._open_Ins_ID
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vehicles_GetListByCondition, screen, Param)
        End Function

        'Common Function to be removed
        'Public Function GetList(ByVal onlineQuery As String, ByVal LocalQuery As String, ByVal screen As ClientScreen) As DataTable
        '    Return GetListOfRecords(onlineQuery, LocalQuery, screen, RealTimeService.Tables.VEHICLES_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        ''' <summary>
        '''   Common Function, Manipulated, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetList_Common</remarks>
        Public Function GetList(ByVal screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vehicles_GetList_Common, screen, parameter)
        End Function

        Public Function GetListByID(ByVal screen As ClientScreen, Optional ByVal parameter As Param_Vehicles_GetList_Common = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vehicles_GetList_Common_ByRecID, screen, parameter)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Vehicles, RealTimeService.Tables.VEHICLES_INFO, "VI_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_Vehicles, RealTimeService.Tables.VEHICLES_INFO, Common.ClientDBFolderCode.DATA)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vehicles_GetRecord, ClientScreen.Profile_Vehicles, Rec_ID)
        End Function

        Public Function IsVehicleCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Vehicles, Tables.VEHICLES_INFO)
        End Function

        ''' <summary>
        ''' Inserts Vehicle Info,Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Vehicles) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Vehicles_Insert, ClientScreen.Profile_Vehicles, InParam)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_InsertTRIDAndTRSrNo</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_Vehicles) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Vehicles_InsertTRIDAndTRSrNo, InParam1.Screen, InParam1)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Vehicles_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Vehicles) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Vehicles)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Vehicles_Update, ClientScreen.Profile_Vehicles, UpParam)
        End Function
        Public Function UpdateInsuranceDetail(ByVal UpParam As Parameter_Update_Vehicles) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Insurance_ChangeDetail)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Vehicles_UpdateInsuranceDetail, ClientScreen.Profile_Insurance_ChangeDetail, UpParam)
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <remarks>RealServiceFunctions.Vehicles_UpdateAssetLocationIfNotPresent</remarks>
        Public Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Vehicles_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Vehicles)

            Return DeleteRecord(Rec_Id, Tables.VEHICLES_INFO, ClientScreen.Profile_Vehicles)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Vehicles)

            Return DeleteByCondition("VI_TR_ID    ='" & Rec_Id & "'", Tables.VEHICLES_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.VEHICLES_INFO, ClientScreen.Profile_Vehicles)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.VEHICLES_INFO, RealTimeService.ClientScreen.Profile_Vehicles)
            Return Locked
        End Function

    End Class
#End Region
End Class
