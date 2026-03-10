'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Advances
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets Advances List, Shifted
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_Advances_GetList = New Param_Advances_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Advances_GetList, ClientScreen.Profile_Advances, Param)
        End Function
         
        'Public Function GetList(ByVal onlineQuery As String, ByVal LocalQuery As String, ByVal Screen As ClientScreen) As DataTable
        '    Return GetListOfRecords(onlineQuery, LocalQuery, Screen, RealTimeService.Tables.ADVANCES_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        ''' <summary>
        ''' Manipulated Function to get Advances List, Common Function, Shifted 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetList_Common</remarks>
        Public Function GetList(ByVal Screen As ClientScreen, Optional ByVal parameter As Param_Advances_GetList_Common = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Advances_GetList_Common, Screen, parameter)
        End Function

        ''' <summary>
        ''' Gets Advances Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetAdvProfileListing = Nothing) As DataTable
            parameter.Acc_Party = cBase._prefer_show_acc_party_only
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Advances_GetProfileListing, ClientScreen.Profile_Advances, parameter)
        End Function

        ''' <summary>
        ''' Gets List by condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Advances_GetListByCondition, screen, OtherCondition)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook  for selected IDs
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParties(ByVal Party_IDs As String) As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.PartyIDs = Party_IDs
            Return _addresses.GetList(ClientScreen.Profile_Advances, Param)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParties() As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Advances)
        End Function

        Public Function GetPartiesByRecID(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Advances, Party_Rec_ID)
        End Function

        'Shifted
        Public Function GetAdvanceItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("ADVANCES", ClientScreen.Profile_Advances, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetOpeningProfileAdvanceItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("ADVANCES", ClientScreen.Profile_Advances, RecIdColumnHead, NameColumnHead)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Party_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetPayments</remarks>
        Public Function GetPayments(ByVal Party_IDs As String, yearID As Integer, Optional NextUnauditedYearID As Integer = Nothing) As DataTable
            Dim inParam As Param_AdvGetPayments = New Param_AdvGetPayments
            inParam.Party_IDs = Party_IDs
            inParam.YearID = yearID
            inParam.NextUnAuditedYearID = NextUnauditedYearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Advances_GetPayments, ClientScreen.Profile_Advances, inParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Advances, RealTimeService.Tables.ADVANCES_INFO, "AI_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Advances, RealTimeService.Tables.ADVANCES_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Gets Advance Payment count, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetAdvancePaymentCount</remarks>
        Public Function GetAdvancePaymentCount(ByVal RecID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Advances_GetAdvancePaymentCount, ClientScreen.Profile_Advances, RecID)
        End Function

        Public Function IsAdvRecordCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Advances, Tables.ADVANCES_INFO)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Advances) As Boolean
            Dim RecID As String = Guid.NewGuid.ToString
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Advances_Insert, ClientScreen.Profile_Advances, InParam)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_InsertTRID</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRID_Advances) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Advances_InsertTRID, InParam1.Screen, InParam1)
        End Function

        ''' <summary>
        ''' Updates Advances Info, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Advances) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Advances)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Advances_Update, ClientScreen.Profile_Advances, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Advances)
            Return DeleteRecord(Rec_Id, Tables.ADVANCES_INFO, ClientScreen.Profile_Advances)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Advances)
            Return DeleteByCondition("AI_TR_ID = '" & Rec_Id & "'", Tables.ADVANCES_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.ADVANCES_INFO, ClientScreen.Profile_Advances)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.ADVANCES_INFO, ClientScreen.Profile_Advances)
            Return Locked
        End Function
    End Class
End Class
