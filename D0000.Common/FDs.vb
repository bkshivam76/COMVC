'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "--Profile--"
    <Serializable>
    Public Class FD
        Inherits SharedVariables

        'Shifted
        Private Function UpdateBankAccountBalance(ByVal Amount As Double, ByVal BankAccountID As String) As Boolean
            Return UpdateOpeningBalance(Amount, BankAccountID, ClientScreen.Profile_FD, Common.Record_Status._Completed) 'status specified here 
        End Function

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim _BankAccounts As BankAccounts = New BankAccounts(cBase)
            Return _BankAccounts.GetFDAccountList(ClientScreen.Profile_FD, Bank_Account_Rec_ID)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Profile_FD, "B.BI_BANK_NAME as Name ,A.BB_BRANCH_NAME as Branch,A.REC_ID AS BB_BRANCH_ID ")
        End Function

        ''' <summary>
        ''' Get Expense Income Count, Shifted
        ''' </summary>
        ''' <param name="FDID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetExpense_IncomeCount</remarks>
        Public Function GetExpense_IncomeCount(ByVal FDID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_GetExpense_IncomeCount, ClientScreen.Profile_FD, FDID)
        End Function

        ''' <summary>
        ''' Get TDS, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetTDS</remarks>
        Public Function GetTDS() As Object
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetTDS, ClientScreen.Profile_FD)
        End Function

        ''' <summary>
        ''' Gets TDS Reversals, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetTDSReversals</remarks>
        Public Function GetTDSReversals() As Object
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetTDSReversals, ClientScreen.Profile_FD)
        End Function

        ''' <summary>
        ''' Gets Interest, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetInterest</remarks>
        Public Function GetInterest() As Object
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetInterest, ClientScreen.Profile_FD)
        End Function

        ''' <summary>
        ''' Get Interest Overheads, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetInterestOverheads</remarks>
        Public Function GetInterestOverheads() As Object
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetInterestOverheads, ClientScreen.Profile_FD)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal OtherCondition As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetListByCondition, Screen, OtherCondition)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetList</remarks>
        Public Function GetList(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal VoucherEntry As String, ByVal ProfileEntry As String, Optional FD_ID As String = Nothing) As DataTable
            Dim Param As Param_FDs_GetList = New Param_FDs_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Param.FD_ID = FD_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetList, Screen, Param)
        End Function

        Public Function GetProfileList() As DataTable
            Dim Param As Param_GetFDProfileListing = New Param_GetFDProfileListing()
            Param.Year_Start_Date = cBase._open_Year_Sdt
            Param.Year_End_Date = cBase._open_Year_Edt
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.FD_GetProfileListing, ClientScreen.Profile_FD, Param)
        End Function

        'No Need To Shift
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, Optional FD_ID As String = Nothing) As DataTable
            Return GetList(ClientScreen.Profile_FD, VoucherEntry, ProfileEntry, FD_ID)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_FD, RealTimeService.Tables.FD_INFO, "FD_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.FDs_GetRecord, ClientScreen.Accounts_Voucher_FD, "FI.REC_ID = '" & Rec_ID & "'")
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_FD, RealTimeService.Tables.FD_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Gets RecordID By AccountID, Shifted
        ''' </summary>
        ''' <param name="AccID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetRecIDByAccID, ClientScreen.Profile_BankAccounts</remarks>
        Public Function GetRecIDByAccID(ByVal AccID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_GetRecIDByAccID, screen, AccID)
        End Function

        'Shifted
        Public Function GetFDSum(ByVal AccountID As String)
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Return _BA.GetFDSum(AccountID)
        End Function

        Public Function IsFDCarriedForward(ByVal Rec_ID As String, ByVal recYearID As String) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_FD, Tables.FD_INFO)
        End Function

        ''' <summary>
        ''' Inserts And Updates Balance, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_Insert_and_Update_Balance</remarks>
        Public Function Insert_and_Update_Balance(ByVal InParam As Parameter_InsertandUpdateBalance_FD) As Boolean
            Dim Result As Boolean = False
            InParam.openYearID = cBase._open_Year_ID
            Result = InsertRecord(RealTimeService.RealServiceFunctions.FDs_Insert_and_Update_Balance, ClientScreen.Profile_FD, InParam)

            Return Result
        End Function

        ''' <summary>
        '''  Updates FD and Updates FD Bank Balance, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_Update_and_Update_Balance</remarks>
        Public Function Update_and_Update_Balance(ByVal UpParam As Parameter_UpdateandUpdateBalance_FD) As Boolean
            Dim Result As Boolean = False
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Profile_FD)
            Result = UpdateRecord(RealTimeService.RealServiceFunctions.FDs_Update_and_Update_Balance, ClientScreen.Profile_FD, UpParam)
            Return Result
        End Function

        ''' <summary>
        ''' Deletes FD and Updates FD Bank Balance, No Need To Shift 
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="BankAccountID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function Delete_and_Update_Balance(ByVal Rec_Id As String, ByVal BankAccountID As String) As Boolean
            Dim Result As Boolean = False

            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_FD)

            Result = DeleteRecord(Rec_Id, Tables.FD_INFO, ClientScreen.Profile_FD)
            If Result Then
                Dim MaxValue As Object = 0
                MaxValue = GetFDSum(BankAccountID)
                If IsDBNull(MaxValue) Then MaxValue = 0
                Result = UpdateBankAccountBalance(MaxValue, BankAccountID)
            End If
            Return Result
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsComplete(Rec_Id, Tables.FD_INFO, ClientScreen.Profile_FD)
            If Locked Then
                'Locked = MyBase.MarkAsComplete("FDH_FD_ID", Rec_Id, Tables.FD_HISTORY_INFO, ClientScreen.Profile_FD, cBase._data_ConStr_Data)
                Locked = MyBase.MarkAsComplete("FDH_FD_ID", Rec_Id, Tables.FD_HISTORY_INFO, ClientScreen.Profile_FD)
            Else
                Locked = False
            End If
            Return Locked
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.FD_INFO, ClientScreen.Profile_FD)
            If Locked Then
                Return MyBase.MarkAsLocked("FDH_FD_ID", Rec_Id, Tables.FD_HISTORY_INFO, ClientScreen.Profile_FD)
            Else
                Return False
            End If
        End Function

    End Class
#End Region

#Region "--Accounts--"
    <Serializable>
    Public Class Voucher_FD
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, "FD_TR_ID", ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.FD_INFO, "FD_CEN_ID")
        End Function

        Public Function GetStatusByID(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.FD_INFO, "FD_CEN_ID")
        End Function

        Public Function GetTxnStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, "TR_M_ID", ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Master_Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Master_Rec_ID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        ''' <summary>
        ''' Gets Payment Records, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetPaymentRecords</remarks>
        Public Function GetPaymentRecords(ByVal Master_Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetPaymentRecords, ClientScreen.Accounts_Voucher_FD, Master_Rec_ID)
        End Function

        ''' <summary>
        ''' Get Interest Records, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="ItemID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetInterestRecords</remarks>
        Public Function GetInterestRecords(ByVal Master_Rec_ID As String, ByVal ItemID As String) As DataTable
            Dim Param As Param_VoucherFD_GetInterestRecords = New Param_VoucherFD_GetInterestRecords()
            Param.ItemID = ItemID
            Param.Master_Rec_ID = Master_Rec_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetInterestRecords, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        ''' <summary>
        ''' Get TDS, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="FDID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetTDS</remarks>
        Public Function GetTDS(ByVal Master_Rec_ID As String, ByVal FDID As String) As Object
            Dim Param As Param_VoucherFD_GetTDS = New Param_VoucherFD_GetTDS()
            Param.FDID = FDID
            Param.Master_Rec_ID = Master_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetTDS, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        ''' <summary>
        ''' Gets TDS Reversal, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="FDID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetTDSReversal</remarks>
        Public Function GetTDSReversal(ByVal Master_Rec_ID As String, ByVal FDID As String) As Object
            Dim Param As Param_VoucherFD_GetTDSReversal = New Param_VoucherFD_GetTDSReversal()
            Param.FDID = FDID
            Param.Master_Rec_ID = Master_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetTDSReversal, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        ''' <summary>
        ''' Gets FD Close Date, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDCloseDate</remarks>
        Public Function GetFDCloseDate(ByVal Master_Rec_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetFDCloseDate, ClientScreen.Accounts_Voucher_FD, Master_Rec_ID)
        End Function

        ''' <summary>
        ''' Gets Close Date of FD by FD Rec ID(SELECT FD_CLOSE_DATE FROM FD_INFO WHERE REC_ID =''), Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns> Close Date of FD</returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDCloseDateByFdID, Gets' Close Date of FD by FD Rec ID</remarks>
        Public Function GetFDCloseDateByFdID(ByVal Rec_ID As String) As Object
            Dim Query As String = "SELECT FD_CLOSE_DATE FROM FD_INFO WHERE REC_ID ='" & Rec_ID & "' "
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetFDCloseDateByFdID, ClientScreen.Accounts_Voucher_FD, Rec_ID)
        End Function

        ''' <summary>
        ''' Gets New FD ID From Closed, Shifted
        ''' </summary>
        ''' <param name="FD_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetNewFDIdFromClosed</remarks>
        Public Function GetNewFDIdFromClosed(ByVal FD_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetNewFDIdFromClosed, ClientScreen.Accounts_Voucher_FD, FD_ID)
        End Function

        ''' <summary>
        ''' Get Count, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="FDID"></param>
        ''' <param name="QCase"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetCount</remarks>
        Public Function GetCount(ByVal Master_Rec_ID As String, ByVal FDID As String, ByVal QCase As Int32) As Object
            Dim Param As Param_VoucherFD_GetCount = New Param_VoucherFD_GetCount()
            Param.FDID = FDID
            Param.Master_Rec_ID = Master_Rec_ID
            Param.QCase = QCase
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetCount, ClientScreen.Accounts_Voucher_FD, Param)

        End Function

        'Shifted
        Public Function GetFdItemCount() As Object
            Dim LocalQuery As String = "SELECT DISTINCT ITEM_NAME AS [FD Activity], IIF(ITEM_NAME = 'FD New', 1, IIF(ITEM_NAME = 'FD Renewed', 2, 4)) AS [Sr] , REC_ID AS ITEMID FROM Item_Info WHERE ITEM_VOUCHER_TYPE = 'fd' and REC_ID NOT IN('65730a27-e365-4195-853e-2f59225fe8f4','1ed5cbe4-c8aa-4583-af44-eba3db08e117') UNION ALL SELECT DISTINCT 'FD Close', 3,'65730a27-e365-4195-853e-2f59225fe8f4' from ITEM_INFO ;"
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT DISTINCT ITEM_NAME AS 'FD Activity', CASE WHEN ITEM_NAME = 'FD New' THEN  1 ELSE CASE WHEN ITEM_NAME = 'FD Renewed' THEN 2 ELSE  4 END END  AS Sr , REC_ID AS ITEMID FROM Item_Info WHERE ITEM_VOUCHER_TYPE = 'fd' and REC_ID NOT IN('65730a27-e365-4195-853e-2f59225fe8f4','1ed5cbe4-c8aa-4583-af44-eba3db08e117') UNION ALL SELECT DISTINCT 'FD Close', 3,'65730a27-e365-4195-853e-2f59225fe8f4' from ITEM_INFO ;"

            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        Public Function GetSelectedColumns(ByVal Master_Rec_ID As String, ByVal ItemID As String) As DataTable
            Return GetRecordByColumn(" TR_AMOUNT, TR_TRF_CROSS_REF_ID ", "TR_M_ID", Master_Rec_ID, "TR_ITEM_ID", ItemID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetRecord(ByVal Master_Rec_ID As String, ByVal ItemID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Master_Rec_ID, "TR_ITEM_ID", ItemID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetFDRecordByID(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.FDs_GetRecord, ClientScreen.Accounts_Voucher_FD, "FI.REC_ID = '" & Rec_ID & "'")
            '  Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.FD_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Gets Prev FD Status, Shifted
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="FDID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetPrevFDStatus</remarks>
        Public Function GetPrevFDStatus(ByVal Master_Rec_ID As String, ByVal FDID As String) As Object
            Dim Param As Param_VoucherFD_GetPrevFDStatus = New Param_VoucherFD_GetPrevFDStatus()
            Param.FDID = FDID
            Param.Master_Rec_ID = Master_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetPrevFDStatus, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        'Public Function GetFDRecordByMID(ByVal TxnMID As String) As DataTable
        '    Return GetDataListOfRecords(RealServiceFunctions.FDs_GetRecord, ClientScreen.Accounts_Voucher_FD, "FI.FD_TR_ID = '" & TxnMID & "'")
        '    'Return GetRecordByColumn("TR_M_ID", TxnMID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.FD_INFO)
        'End Function

        Public Function GetFDRecordByTxnID(ByVal TxnID As String) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.FDs_GetRecord, ClientScreen.Accounts_Voucher_FD, "FI.FD_TR_ID = '" & TxnID & "'")
            ' Return GetRecordByColumn("FD_TR_ID", TxnID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.FD_INFO)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_Rec_Id As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
            Param.Type = "SAVING"
            Param.Bank_Account_Rec_ID = Bank_Account_Rec_Id
            Return _BA.GetList(ClientScreen.Accounts_Voucher_FD, Param) 'Same ScreenCode with different parameter set 
        End Function

        'Shifted
        Public Function GetFDBankAccounts(Optional Rec_ID As String = "") As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
            Param.Type = "FD"
            If Rec_ID.Length > 0 Then Param.Bank_Account_Rec_ID = Rec_ID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_FD, Param) 'Same ScreenCode with different parameter set 
        End Function

        ''' <summary>
        ''' Get FDs, Shifted
        ''' </summary>
        ''' <param name="IncludeClosedFDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDs</remarks>
        Public Function GetFDs(ByVal IncludeClosedFDs As Boolean, Optional FD_ID As String = Nothing, Optional IncludeAllYears As Boolean = False) As DataTable
            Dim Param As Param_VoucherFD_GetFDs = New Param_VoucherFD_GetFDs
            Param.FD_ID = FD_ID
            Param.IncludeClosedFDs = IncludeClosedFDs
            Param.IncludePrevYearFDs = IncludeAllYears
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetFDs, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        'Shifted
        Public Function GetBankChargesItemDetail() As DataTable
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "2"
            Return GetItemsByQuery_Common("SELECT ITEM_TRANS_TYPE, ITEM_LED_ID FROM ITEM_INFO WHERE REC_ID ='290063bc-a1a1-43af-bedb-f51b7a30c4f4';", ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        'Shifted
        Public Function GetBranches(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_FD)
        End Function

        Public Function GetItemList() As DataTable
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_Donation, "FD")
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="TxnMID"></param>
        ''' <param name="itemID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetItemCountInSameMaster</remarks>
        Public Function GetItemCountInSameMaster(ByVal TxnMID As String, ByVal itemID As String) As Object
            Dim Param As Param_VoucherFD_GetItemCountInSameMaster = New Param_VoucherFD_GetItemCountInSameMaster()
            Param.TxnMID = TxnMID
            Param.itemID = itemID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetItemCountInSameMaster, ClientScreen.Accounts_Voucher_FD, Param)
        End Function

        ''' <summary>
        ''' Get Account No. count, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="FDNo"></param>
        ''' <param name="BankAccID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetAccountNoCount</remarks>
        Public Function GetAccountNoCount(ByVal Rec_Id As String, ByVal FDNo As String, ByVal BankAccID As String) As Object
            Dim Param As Param_VoucherFD_GetAccountNoCount = New Param_VoucherFD_GetAccountNoCount()
            Param.BankAccID = BankAccID
            Param.FDNo = FDNo
            Param.Rec_Id = Rec_Id
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.FDs_VoucherFD_GetAccountNoCount, ClientScreen.Accounts_Voucher_FD, Param)
        End Function
        Public Function GetPurposeID(ByVal M_ID As String) As String
            Dim _Table As DataTable = GetRecordByColumn("TR_REC_ID", M_ID, ClientScreen.Accounts_Voucher_FD, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
            If _Table.Rows.Count > 0 Then
                Return _Table.Rows(0)("TR_PURPOSE_MISC_ID")
            End If
            Return ""
        End Function
        ''' <summary>
        ''' InsertFD, Shifted
        ''' </summary>
        ''' <param name="InFDParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertFD</remarks>
        Public Function InsertFD(ByVal InFDParam As Parameter_InsertFD_VoucherFD) As Boolean
            'InFDParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertFD, ClientScreen.Accounts_Voucher_FD, InFDParam)
        End Function

        ''' <summary>
        ''' Insert FD History, Shifted
        ''' </summary>
        ''' <param name="InFDHty"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertFDHistory</remarks>
        Public Function InsertFDHistory(ByVal InFDHty As Parameter_InsertFDHistory_VoucherFD) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertFDHistory, ClientScreen.Accounts_Voucher_FD, InFDHty)
        End Function

        ''' <summary>
        ''' Insert Master Info, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherFD) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertMasterInfo, ClientScreen.Accounts_Voucher_FD, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherFD) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_Insert, ClientScreen.Accounts_Voucher_FD, InParam)
        End Function

        ''' <summary>
        ''' UpdateFD, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateFD</remarks>
        Public Function UpdateFD(ByVal UpParam As Parameter_UpdateFD_VoucherFD) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.TxnID, ClientScreen.Accounts_Voucher_FD)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateFD, ClientScreen.Accounts_Voucher_FD, UpParam)
        End Function

        ''' <summary>
        ''' Updates FD History, Shifted
        ''' </summary>
        ''' <param name="UpFDHty"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateFDHistory</remarks>
        Public Function UpdateFDHistory(ByVal UpFDHty As Parameter_UpdateFDHistory_VoucherFD) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateFDHistory, ClientScreen.Accounts_Voucher_FD, UpFDHty)
        End Function

        ''' <summary>
        ''' Update Master Info, Shifted
        ''' </summary>
        ''' <param name="UpMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateMasterInfo</remarks>
        Public Function UpdateMasterInfo(ByVal UpMInfo As Parameter_UpdateMasterInfo_VoucherFD) As Boolean
            'UpMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead 
            cBase._Audit_DBOps.UnVouchEntryByReference(UpMInfo.RecID, ClientScreen.Accounts_Voucher_FD)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateMasterInfo, ClientScreen.Accounts_Voucher_FD, UpMInfo)
        End Function

        Public Overloads Function Delete(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_FD)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_FD)
        End Function

        Public Overloads Function DeleteFD(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_FD)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + Master_Rec_Id + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return DeleteByCondition("FD_TR_ID = '" & Master_Rec_Id & "'", Tables.FD_INFO, ClientScreen.Accounts_Voucher_FD)
        End Function

        Public Overloads Function DeleteFDHistory(ByVal Master_Rec_Id As String) As Boolean
            Return DeleteByCondition("FDH_TR_ID = '" & Master_Rec_Id & "'", Tables.FD_HISTORY_INFO, ClientScreen.Accounts_Voucher_FD)
        End Function

        Public Overloads Function DeleteMaster(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_FD)
            Return DeleteRecord(Master_Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_FD)
        End Function
        Public Overloads Function DeletePurpose(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_FD)
            DeleteByCondition("TR_REC_ID = '" + Master_Rec_Id + "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_FD)
        End Function
        Public Function InsertNewFD_Txn(inParam As Param_Txn_NewFD_InsertVoucherFD) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertNewFD_Txn, ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        Public Function UpdateNewFD_Txn(upParam As Param_Txn_NewFD_UpdateVoucherFD) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(upParam.param_DeleteVoucher_Txn_MID, ClientScreen.Accounts_Voucher_FD)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateNewFD_Txn, ClientScreen.Accounts_Voucher_FD, upParam)
        End Function

        Public Function DeleteNewFD_Txn(delParam As Param_Txn_NewFD_DeleteVoucherFD) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_FD)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_DeleteNewFD_Txn, ClientScreen.Accounts_Voucher_FD, delParam)
        End Function

        Public Function InsertRenewFD_Txn(inParam As Param_Txn_RenewFD_InsertVoucherFD) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertRenewFD_Txn, ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        Public Function UpdateRenewFD_Txn(upParam As Param_Txn_RenewFD_UpdateVoucherFD) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(upParam.MID_DeleteTxns, ClientScreen.Accounts_Voucher_FD)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateRenewFD_Txn, ClientScreen.Accounts_Voucher_FD, upParam)
        End Function

        Public Function DeleteRenewFD_Txn(delParam As Param_Txn_RenewFD_DeleteVoucherFD) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_FD)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_DeleteRenewFD_Txn, ClientScreen.Accounts_Voucher_FD, delParam)
        End Function

        Public Function InsertCloseFD_Txn(inParam As Param_Txn_CloseFD_InsertVoucherFD) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertCloseFD_Txn, ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        Public Function UpdateCloseFD_Txn(upParam As Param_Txn_CloseFD_UpdateVoucherFD) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(upParam.MID_DeleteTxns, ClientScreen.Accounts_Voucher_FD)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateCloseFD_Txn, ClientScreen.Accounts_Voucher_FD, upParam)
        End Function

        Public Function DeleteCloseFD_Txn(delParam As Param_Txn_CloseFD_DeleteVoucherFD) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_FD)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_DeleteCloseFD_Txn, ClientScreen.Accounts_Voucher_FD, delParam)
        End Function

        Public Function InsertIncomeAndExpenses_Txn(inParam As Param_Txn_IncomeExpenses_InsertVoucherFD) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_InsertIncomeAndExpenses_Txn, ClientScreen.Accounts_Voucher_FD, inParam)
        End Function

        Public Function UpdateIncomeAndExpenses_Txn(upParam As Param_Txn_IncomeExpenses_UpdateVoucherFD) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(upParam.MID_DeleteTxns, ClientScreen.Accounts_Voucher_FD)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_UpdateIncomeAndExpenses_Txn, ClientScreen.Accounts_Voucher_FD, upParam)
        End Function

        Public Function DeleteIncomeAndExpenses_Txn(delParam As Param_Txn_IncomeExpenses_DeleteVoucherFD) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_FD)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.FDs_VoucherFD_DeleteIncomeAndExpenses_Txn, ClientScreen.Accounts_Voucher_FD, delParam)
        End Function
    End Class
#End Region
End Class
