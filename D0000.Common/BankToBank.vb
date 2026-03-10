'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_BankToBank
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_BankToBank, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_BankToBank, RealTimeService.Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeID(ByVal Rec_ID As String) As String
            Dim _Table As DataTable = GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_BankToBank, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
            If _Table.Rows.Count > 0 Then
                Return _Table.Rows(0)("TR_PURPOSE_MISC_ID")
            End If
            Return ""
        End Function

        'Shifted
        Public Function GetItemList() As DataTable
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_BankToBank, "BANK TRANSFER")
        End Function


        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Account_Rec_ID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_BankToBank, Param)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Voucher_BankToBank, "  B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID ")
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.BankToBank_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Voucher_BankToBank) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.BankToBank_Insert, ClientScreen.Accounts_Voucher_BankToBank, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.BankToBank_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Voucher_BankToBank) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_BankToBank)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.BankToBank_Update, ClientScreen.Accounts_Voucher_BankToBank, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_BankToBank)

            'The Below line is only to delete purpose
            DeleteByCondition("TR_REC_ID = '" + Rec_Id + "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_BankToBank)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + Rec_Id + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_BankToBank)
        End Function
    End Class
#End Region
End Class