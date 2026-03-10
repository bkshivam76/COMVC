'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_CollectionBox
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Notebook, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        ''' <summary>
        ''' Get Past Witness, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CollectionBox_GetPastWitness</remarks>
        Public Function GetPastWitness() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CollectionBox_GetPastWitness, ClientScreen.Facility_AddressBook)
        End Function

        'Shifted
        Public Function GetItemList() As DataTable
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_CollectionBox, "COLLECTION BOX")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_CollectionBox, RealTimeService.Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeID(ByVal Rec_ID As String) As String
            Dim _Table As DataTable = GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_CollectionBox, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
            If _Table.Rows.Count > 0 Then
                Return _Table.Rows(0)("TR_PURPOSE_MISC_ID")
            End If
            Return ""
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_RecID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Account_RecID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_CollectionBox, Param)
        End Function

        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBanks() As DataTable
            Dim Query As String = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID " & _
                                " From  BANK_INFO  Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_CollectionBox)
        End Function

        'Shifted
        Public Function GetAddresses(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _AB As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            Param.Party_Rec_ID = Party_Rec_ID
            Return _AB.GetList(ClientScreen.Accounts_Voucher_CollectionBox, Param)
        End Function

        'Shifted
        Public Function GetBranches(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_CollectionBox)
        End Function

        ''' <summary>
        ''' Gets Dates of Transactions where referred Address Book Id is used as Witness(1/2)
        ''' </summary>
        ''' <param name="ABRecID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUsageAsPastWitness(ByVal ABRecID As String) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.CollectionBox_CheckUsageAsPastWitness, ClientScreen.Accounts_Voucher_CollectionBox, ABRecID)
        End Function

        Public Function GetDenominations(TxnID As String) As RealTimeService.Parameter_CollectionBox_Denomination
            Dim dTable As DataTable = GetDataListOfRecords(RealServiceFunctions.CollectionBox_GetDenominations, ClientScreen.Accounts_Voucher_CollectionBox, TxnID)
            Dim _Denominations As New Parameter_CollectionBox_Denomination()
            For Each _row As DataRow In dTable.Rows
                Select Case _row("TR_DENOMINATION")
                    Case 2000
                        _Denominations.Count_2000 = _row("TR_COUNT")
                    Case 1000
                        _Denominations.Count_1000 = _row("TR_COUNT")
                    Case 500
                        _Denominations.Count_500 = _row("TR_COUNT")
                    Case 200
                        _Denominations.Count_200 = _row("TR_COUNT")
                    Case 100
                        _Denominations.Count_100 = _row("TR_COUNT")
                    Case 50
                        _Denominations.Count_50 = _row("TR_COUNT")
                    Case 20
                        _Denominations.Count_20 = _row("TR_COUNT")
                    Case 10
                        _Denominations.Count_10 = _row("TR_COUNT")
                    Case 5
                        _Denominations.Count_5 = _row("TR_COUNT")
                    Case 2
                        _Denominations.Count_2 = _row("TR_COUNT")
                    Case 1
                        _Denominations.Count_1 = _row("TR_COUNT")
                End Select
            Next
            Return _Denominations
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.CollectionBox_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Voucher_CollectionBox) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.CollectionBox_Insert, ClientScreen.Accounts_Voucher_CollectionBox, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CollectionBox_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Voucher_CollectionBox) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_CollectionBox)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CollectionBox_Update, ClientScreen.Accounts_Voucher_CollectionBox, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_CollectionBox)
            DeleteByCondition("TXN_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_DENOMINATION_INFO, ClientScreen.Accounts_Voucher_CollectionBox)
            DeleteByCondition("TR_REC_ID = '" + Rec_Id + "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_CollectionBox)
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + Rec_Id + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CollectionBox)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_CollectionBox)
        End Function
    End Class
#End Region
End Class
