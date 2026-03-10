'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class Liabilities
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common_Lib.Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets Liabilities List, Shifted
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_Liabilities_GetList = New Param_Liabilities_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Liabilities_GetList, ClientScreen.Profile_Liabilities, Param)
        End Function

        ''' <summary>
        ''' Manipulated, Common Function, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetList_Common</remarks>
        Public Function GetList(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            'ClientScreen.Accounts_Voucher_Payment
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Liabilities_GetList_Common, Screen, parameter)
        End Function

        ''' <summary>
        ''' Gets Liabilities Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetLiabProfileListing = Nothing) As DataTable
            parameter.Acc_Party = cBase._prefer_show_acc_party_only
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Liabilities_GetProfileListing, ClientScreen.Profile_Liabilities, parameter)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Liabilities_GetListByCondition, screen, OtherCondition)
        End Function

        Public Function GetParties(ByVal Party_IDs As String) As DataTable
            Dim _Address As Addresses = New Addresses(cBase)
            Dim inPAram As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            inPAram.PartyIDs = Party_IDs
            Return _Address.GetList(ClientScreen.Profile_Deposit, inPAram)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParties() As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Liabilities)
        End Function

        Public Function GetPartiesByRecID(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Liabilities, Party_Rec_ID)
        End Function

        ''' <summary>
        ''' Get Payments For Parties, Shifted
        ''' </summary>
        ''' <param name="Party_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetPaymentsForParties</remarks>
        Public Function GetPaymentsForParties(ByVal Party_IDs As String, YearID As Integer, Optional NextUnauditedYear As Integer = Nothing) As DataTable
            Dim oParam As Param_GetLiabPaymentsForParties = New Param_GetLiabPaymentsForParties
            oParam.PartyID = Party_IDs
            oParam.YearID = YearID
            oParam.NextUnAuditedYearID = NextUnauditedYear
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Liabilities_GetPaymentsForParties, ClientScreen.Profile_Liabilities, oParam)
        End Function

        'Shifted
        Public Function GetLiabilityItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("OTHER LIABILITIES", ClientScreen.Profile_Liabilities, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetOpeningProfileLiabilityItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("OTHER LIABILITIES", ClientScreen.Profile_Liabilities, RecIdColumnHead, NameColumnHead)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Liabilities, RealTimeService.Tables.LIABILITIES_INFO, "LI_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Liabilities, RealTimeService.Tables.LIABILITIES_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Gets Transaction Count, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetTransactionCount</remarks>
        Public Function GetTransactionCount(ByVal RecID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Liabilities_GetTransactionCount, ClientScreen.Profile_Liabilities, RecID)
        End Function

        Public Function IsLiabCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Liabilities, Tables.LIABILITIES_INFO)
        End Function

        ''' <summary>
        ''' Inserts Liabilities Info, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Liabilities) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Liabilities_Insert, ClientScreen.Profile_Liabilities, InParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_InsertTRID</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRID_Liabilities) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Liabilities_InsertTRID, InParam1.Screen, InParam1)
        End Function

        ''' <summary>
        ''' Updates Liabilities Info, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Liabilities) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Profile_Liabilities)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Liabilities_Update, ClientScreen.Profile_Liabilities, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Liabilities)

            Return DeleteRecord(Rec_Id, Tables.LIABILITIES_INFO, ClientScreen.Profile_Liabilities)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Liabilities)

            Return DeleteByCondition("LI_TR_ID = '" & Rec_Id & "'", Tables.LIABILITIES_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.LIABILITIES_INFO, ClientScreen.Profile_Liabilities)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.LIABILITIES_INFO, ClientScreen.Profile_Liabilities)
            Return Locked
        End Function
    End Class

#End Region
End Class
