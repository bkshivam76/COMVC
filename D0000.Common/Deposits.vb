'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

    <Serializable>
    Public Class Deposits
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets Deposits List, Shifted
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String) As DataTable
            Dim Param As Param_Deposits_GetList = New Param_Deposits_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Deposits_GetList, ClientScreen.Profile_Deposit, Param)
        End Function

        ''' <summary>
        ''' Common Function, Manipulated, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetList_Common</remarks>
        Public Function GetList(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Deposits_GetList_Common, Screen, parameter)
        End Function

        ''' <summary>
        ''' Gets Deposits Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetDepositProfileListing = Nothing) As DataTable
            parameter.Acc_Party = cBase._prefer_show_acc_party_only
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Deposits_GetProfileListing, ClientScreen.Profile_Deposit, parameter)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Deposits_GetListByCondition, screen, OtherCondition)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Party_IDs"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParties(ByVal Party_IDs As String) As DataTable
            Dim _Address As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.PartyIDs = Party_IDs
            Return _Address.GetList(ClientScreen.Profile_Deposit, Param)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetParties() As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Deposit)
        End Function

        Public Function GetPartiesByRecID(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _addresses As Addresses = New Addresses(cBase)
            Return _addresses.GetPartiesList(ClientScreen.Profile_Deposit, Party_Rec_ID)
        End Function

        ''' <summary>
        ''' Gets Payments For Parties, Shifted
        ''' </summary>
        ''' <param name="Party_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetPaymentsForParties</remarks>
        Public Function GetPaymentsForParties(ByVal Party_IDs As String, YearID As Integer, Optional NextUnauditedYear As Integer = Nothing) As DataTable
            Dim OutParam As Param_GetPaymentsForParties = New Param_GetPaymentsForParties
            OutParam.Party_IDs = Party_IDs
            OutParam.YearID = YearID
            OutParam.NextUnAuditedYearID = NextUnauditedYear
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Deposits_GetPaymentsForParties, ClientScreen.Profile_Deposit, OutParam)
        End Function

        'Shifted
        Public Function GetDepositItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetItems("OTHER DEPOSITS", ClientScreen.Profile_Deposit, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetOpeningProfileDepositItems(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetOpeningProfileItems("OTHER DEPOSITS", ClientScreen.Profile_Deposit, RecIdColumnHead, NameColumnHead)
        End Function

        'Shifted
        Public Function GetInsuranceCompanies(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("INSURANCE", ClientScreen.Profile_Deposit, NameColumnHead, RecIdColumnHead)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetTransactionCount</remarks>
        Public Function GetTransactionCount(ByVal RecID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Deposits_GetTransactionCount, ClientScreen.Profile_Deposit, RecID)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Deposit, RealTimeService.Tables.DEPOSITS_INFO, "DI_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Deposit, RealTimeService.Tables.DEPOSITS_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function IsDepositCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Deposit, Tables.DEPOSITS_INFO)
        End Function

        ''' <summary>
        ''' Checks if a Final Adjustment has been made against a Deposit 
        ''' </summary>
        ''' <param name="Di_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFinalPaymentDate(Di_Rec_ID As String, Optional Tr_M_ID As String = "") As Object
            Dim iParam As Param_CheckIfDepositFinalPaymentMade = New Param_CheckIfDepositFinalPaymentMade
            iParam.DepositID = Di_Rec_ID
            iParam.Tr_M_ID = Tr_M_ID
            Return GetSingleValue_Data(RealServiceFunctions.Deposits_CheckIfDepositFinalPaymentMade, ClientScreen.Accounts_Vouchers, iParam)
        End Function

        ''' <summary>
        ''' Inserts Deposits Info, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Deposits) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Deposits_Insert, ClientScreen.Profile_Deposit, InParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_InsertTRID</remarks>
        Public Function Insert(ByVal InParam1 As Parameter_InsertTRID_Deposits) As Boolean
            'InParam1.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return (InsertRecord(RealTimeService.RealServiceFunctions.Deposits_InsertTRID, InParam1.Screen, InParam1))
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Deposits) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Profile_Deposit)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Deposits_Update, ClientScreen.Profile_Deposit, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String)
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Deposit)
            Return DeleteRecord(Rec_Id, Tables.DEPOSITS_INFO, ClientScreen.Profile_Deposit)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Deposit)
            Return DeleteByCondition("DI_TR_ID = '" & Rec_Id & "'", Tables.DEPOSITS_INFO, Screen)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.DEPOSITS_INFO, ClientScreen.Profile_Deposit)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.DEPOSITS_INFO, ClientScreen.Profile_Deposit)
            Return Locked
        End Function
    End Class

End Class
