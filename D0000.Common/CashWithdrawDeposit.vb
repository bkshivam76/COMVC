'SQL, Shifted
Imports Common_Lib.DbOperations.Vouchers
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_CashBank
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_CashBank, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_CashBank, RealTimeService.Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        End Function
        Public Function GetPurposeID(ByVal Rec_ID As String) As String
            Dim _Table As DataTable = GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_CashBank, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
            If _Table.Rows.Count > 0 Then
                Return _Table.Rows(0)("TR_PURPOSE_MISC_ID")
            End If
            Return ""
        End Function

        'Shifted
        Public Function GetItemList() As DataTable
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_CashBank, "'CASH DEPOSITED','CASH WITHDRAWN'")
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_RecID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Account_RecID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_CashBank, Param)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Voucher_CashBank, "  B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID ")
        End Function
        Public Function GetDenominations(Txn_ID As String) As RealTimeService.Parameter_CashBank_Denomination
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT TR_DENOMINATION,TR_COUNT FROM transaction_d_denomination_info WHERE TXN_ID ='" & Txn_ID & "' "
            Dim dTable As DataTable = _RealService.List(Tables.TRANSACTION_D_DENOMINATION_INFO, Query, Tables.TRANSACTION_D_DENOMINATION_INFO.ToString(), GetBaseParams(ClientScreen.Accounts_Voucher_CashBank))
            Dim _Denominations As New Parameter_CashBank_Denomination()
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
        ''' <remarks>RealServiceFunctions.CashWithdrawDeposit_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Voucher_CashBank) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.CashWithdrawDeposit_Insert, ClientScreen.Accounts_Voucher_CashBank, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CashWithdrawDeposit_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Voucher_CashBank) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_CashBank)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CashWithdrawDeposit_Update, ClientScreen.Accounts_Voucher_CashBank, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_CashBank)
            DeleteByCondition("TXN_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_DENOMINATION_INFO, ClientScreen.Accounts_Voucher_CashBank)
            DeleteByCondition("TR_REC_ID = '" + Rec_Id + "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_CashBank)
            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) = '" + Rec_Id + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_CashBank)
        End Function

        Public Overloads Function DeleteSplVchrRef(ByVal Txn_Rec_Id As String)
            DeleteByCondition("TXN_REC_ID = '" + Txn_Rec_Id + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)
        End Function



        '<Serializable>
        'Public Class Return_SplVchrRefsList
        '    Public Property ID As String
        '    Public Property Task_Type As String
        '    Public Property PERMISSION As String
        '    Public Property disabled As Boolean
        'End Class
    End Class
#End Region
End Class
