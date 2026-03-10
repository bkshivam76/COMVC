'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class NoteBook
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get Max Transaction Date, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_GetMaxTransactionDate</remarks>
        Public Function GetMaxTransactionDate() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Notebook_GetMaxTransactionDate, ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetCashBalance() As DataTable
            Return GetCashOpeningBalanceAmount(ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetSavingAccounts() As DataTable
            Return GetSavingAccountsList(ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetBankBalance(ByVal BankIDs As String) As DataTable
            Return GetBankBalanceAmount(BankIDs, ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetCashBankTransSum(ByVal FromDate As Date) As DataTable
            Return GetCashBankTransSumAmount(FromDate, ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetCashBankTransSum(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
            Return GetCashBankTransSumAmount(FromDate, ToDate, ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetItemList() As DataTable
            Return GetItems(ClientScreen.Accounts_Notebook)
        End Function

        'Shifted
        Public Function GetPettyCashItems() As DataTable
            Return GetItemDetails(ClientScreen.Accounts_Notebook)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="Month_Of_Year"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_GetList</remarks>
        Public Function GetList(ByVal Month_Of_Year As Integer) As DataTable
            Dim InParam As Parameter_Notebook_GetList = New Parameter_Notebook_GetList
            InParam.Month_Of_Year = Month_Of_Year
            InParam.CurrInsttID = cBase._open_Ins_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Notebook_GetList, ClientScreen.Accounts_Notebook, InParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Notebook, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Notebook, RealTimeService.Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_NoteBook) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Notebook_Insert, ClientScreen.Accounts_Notebook, InParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="AllEntries"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_InsertAllEntries</remarks>
        Public Function Insert(ByVal AllEntries As ArrayList) As Boolean
            ''---> NOTE: Only first entry RecID passed in this function because all entries are same month,year.
            Dim Param As Param_InsertAllEntries_Notebook = New Param_InsertAllEntries_Notebook()
            Param.AllEntries = AllEntries.ToArray
            Param.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Notebook_InsertAllEntries, ClientScreen.Accounts_Notebook, Param)
        End Function

        ''' <summary>
        ''' Updates Notebook Info, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_Update</remarks>
        'Public Function Update(ByVal UpParam As Parameter_Update_NoteBook) As Boolean
        '    Dim EntryID As String = "NOTE-BOOK-" & cBase._open_Cen_ID.ToString() & "-" & cBase._open_Year_ID & "-" & UpParam.TDate.Year.ToString() & "." & UpParam.TDate.Month.ToString() & "." & DateTime.DaysInMonth(UpParam.TDate.Year, UpParam.TDate.Month).ToString() & "'"
        '    cBase._Audit_DBOps.UnVouchEntryByReference(EntryID, ClientScreen.Accounts_Notebook)
        '    Return UpdateRecord(RealTimeService.RealServiceFunctions.Notebook_Update, ClientScreen.Accounts_Notebook, UpParam)
        'End Function

        Public Overloads Function Delete(ByVal Month_of_Year As Integer, Year As Integer) As Boolean
            DeleteByCondition(" VA_ENTRY_ID='" & "NOTE-BOOK-" & cBase._open_Cen_ID.ToString() & "-" & cBase._open_Year_ID.ToString() & "-" & Year.ToString() & "." & Month_of_Year.ToString().PadLeft(2, "0") & "." & DateTime.DaysInMonth(Year, Month_of_Year).ToString() & "'", Tables.Vouching_Audit, ClientScreen.Accounts_Notebook)
            Return DeleteByCondition(" TR_CEN_ID=" & cBase._open_Cen_ID & " AND TR_CODE = 3 AND TR_NOTEBOOK='YES' AND MONTH(TR_DATE)= " & Month_of_Year & " AND YEAR(TR_DATE)= " & Year, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Notebook)
        End Function

        Public Overloads Function MarkAsLocked(ByVal FromDate As Date, ByVal ToDate As Date) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLockedCustom(" (CAST(TR_DATE AS DATE) BETWEEN '" & FromDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') AND TR_NOTEBOOK IS NOT NULL AND TR_CEN_ID = " & cBase._open_Cen_ID.ToString & " ", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Notebook)
            Return Locked
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Notebook)
            Return Locked
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Notebook)
        End Function

    End Class
#End Region
End Class