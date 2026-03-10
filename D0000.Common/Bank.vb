'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class BankAccounts
        Inherits SharedVariables
#Region "Retun Classes"
        <Serializable>
        Public Class Return_GetAccountList
            Public Property ID As String
            Public Property Name As String
            Public Property Branch As String
            Public Property BranchId As String
            Public Property BA_ACCOUNT_TYPE As String
            Public Property BA_ACCOUNT_NO As String
            Public Property BA_CUST_NO As String
            Public Property OP_AMOUNT As Decimal?
            Public Property CL_AMOUNT As Decimal?
            Public Property BA_OTHER_DETAIL As String
            Public Property BB_IFSC_CODE As String
            Public Property BB_MICR_CODE As String
            Public Property BI_BANK_PAN_NO As String
            Public Property BA_TAN_NO As String
            Public Property BA_TEL_NOS As String
            Public Property BA_EMAIL_ID As String
            Public Property BA_ACCOUNT_NEW As String
            Public Property BA_OPEN_DATE As DateTime?
            Public Property BA_CLOSE_DATE As DateTime?
            Public Property Add_By As String
            Public Property Add_Date As DateTime
            Public Property Edit_By As String
            Public Property Edit_Date As DateTime
            Public Property Action_Status As String
            Public Property Action_By As String
            Public Property Action_Date As DateTime
            Public Property Remarks As Boolean
            Public Property RemarkStatus As String
            Public Property OpenActions As Int32
            Public Property CrossedTimeLimit As Int32
            Public Property YearID As Int32
            Public Property iREQ_ATTACH_COUNT As Int32?
            Public Property iCOMPLETE_ATTACH_COUNT As Int32?
            Public Property iRESPONDED_COUNT As Int32?
            Public Property iREJECTED_COUNT As Int32?

            Public Property iOTHER_ATTACH_CNT As Int32?
            Public Property iALL_ATTACH_CNT As Int32?
            Public Property BA_FCRA_UTIL As Boolean?
            Public Property VOUCHING_ACCEPTED_COUNT As Int32?
            Public Property VOUCHING_PENDING_COUNT As Int32?
            Public Property VOUCHING_ACCEPTED_WITH_REMARKS_COUNT As Int32?
            Public Property VOUCHING_REJECTED_COUNT As Int32?
            Public Property VOUCHING_TOTAL_COUNT As Int32?
            Public Property AUDIT_PENDING_COUNT As Int32?
            Public Property AUDIT_ACCEPTED_COUNT As Int32?
            Public Property AUDIT_ACCEPTED_WITH_REMARKS_COUNT As Int32?
            Public Property AUDIT_REJECTED_COUNT As Int32?
            Public Property AUDIT_TOTAL_COUNT As Int32?
            Public Property iIcon As String
            Public Property Special_Ref As String
        End Class
        <Serializable>
        Public Class BankStatement_Reference
            Public Property AccountNo As String
            Public Property FromDate As String
            Public Property ToDate As String
            Public Property ReferenceNo As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            ''' <summary>
            ''' Get Bank List, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetList</remarks>
            Public Function GetList(Optional Bank_RecID As String = Nothing) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetList, ClientScreen.Profile_BankAccounts, Bank_RecID)
            End Function

            Public Function GetAccountList(Optional Bank_RecID As String = Nothing) As List(Of Return_GetAccountList)
                Dim RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetList, ClientScreen.Profile_BankAccounts, Bank_RecID)
                Dim _AccountList As List(Of Return_GetAccountList) = New List(Of Return_GetAccountList)()

                For Each row As DataRow In RetTable.Rows
                    Dim newdata = New Return_GetAccountList()
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Branch = row.Field(Of String)("Branch")
                    newdata.BranchId = row.Field(Of String)("BranchId")
                    newdata.BA_ACCOUNT_TYPE = row.Field(Of String)("BA_ACCOUNT_TYPE")
                    newdata.BA_ACCOUNT_NO = row.Field(Of String)("BA_ACCOUNT_NO")
                    newdata.BA_CUST_NO = row.Field(Of String)("BA_CUST_NO")
                    newdata.OP_AMOUNT = row.Field(Of Decimal?)("OP_AMOUNT")
                    newdata.CL_AMOUNT = row.Field(Of Decimal?)("CL_AMOUNT")
                    newdata.BA_OTHER_DETAIL = row.Field(Of String)("BA_OTHER_DETAIL")
                    newdata.BB_IFSC_CODE = row.Field(Of String)("BB_IFSC_CODE")
                    newdata.BB_MICR_CODE = row.Field(Of String)("BB_MICR_CODE")
                    newdata.BI_BANK_PAN_NO = row.Field(Of String)("BI_BANK_PAN_NO")
                    newdata.BA_TAN_NO = row.Field(Of String)("BA_TAN_NO")
                    newdata.BA_TEL_NOS = row.Field(Of String)("BA_TEL_NOS")
                    newdata.BA_EMAIL_ID = row.Field(Of String)("BA_EMAIL_ID")
                    newdata.BA_ACCOUNT_NEW = row.Field(Of String)("BA_ACCOUNT_NEW")
                    newdata.BA_OPEN_DATE = row.Field(Of DateTime?)("BA_OPEN_DATE")
                    newdata.BA_CLOSE_DATE = row.Field(Of DateTime?)("BA_CLOSE_DATE")
                    newdata.BA_FCRA_UTIL = row.Field(Of Boolean?)("BA_FCRA_UTIL")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Add_Date = row.Field(Of DateTime?)("Add Date")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Remarks = row.Field(Of Boolean)("Remarks")
                    newdata.RemarkStatus = row.Field(Of String)("RemarkStatus")
                    newdata.OpenActions = row.Field(Of Int32)("OpenActions")
                    newdata.CrossedTimeLimit = row.Field(Of Int32)("CrossedTimeLimit")
                    newdata.YearID = row.Field(Of Int32)("YearID")
                    newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    newdata.VOUCHING_PENDING_COUNT = row.Field(Of Int32?)("VOUCHING_PENDING_COUNT")
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT")
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT")
                    newdata.VOUCHING_REJECTED_COUNT = row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT")
                    newdata.VOUCHING_TOTAL_COUNT = row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT")
                    newdata.AUDIT_PENDING_COUNT = row.Field(Of Int32?)("AUDIT_PENDING_COUNT")
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT")
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT")
                    newdata.AUDIT_REJECTED_COUNT = row.Field(Of Int32?)("AUDIT_REJECTED_COUNT")
                    newdata.AUDIT_TOTAL_COUNT = row.Field(Of Int32?)("AUDIT_TOTAL_COUNT")
                    newdata.iIcon = ""
                    newdata.Special_Ref = row.Field(Of String)("Special_Ref")
                    If (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) = 0 AndAlso (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) = 0)) Then
                        newdata.iIcon += "GreenShield|"
                    ElseIf (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) > 0 AndAlso (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) < (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)))) Then
                        newdata.iIcon += "YellowShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) > 0)) Then
                        newdata.iIcon += "BlueShield|"
                    End If

                    If ((If(row.Field(Of Int32?)("REJECTED_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedFlag|"
                    End If

                    If (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) = 0) Then
                        newdata.iIcon += "RequiredAttachment|"
                    ElseIf (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) <> 0) Then
                        newdata.iIcon += "AdditionalAttachment|"
                    End If

                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT") > 0) Then newdata.iIcon += "VouchingAccepted|"
                    If (If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0) Then newdata.iIcon += "VouchingReject|"
                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newdata.iIcon += "VouchingAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("VOUCHING_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0)) Then newdata.iIcon += "VouchingPartial|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT") > 0) Then newdata.iIcon += "AuditAccepted|"
                    If (If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0) Then newdata.iIcon += "AuditReject|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newdata.iIcon += "AuditAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("AUDIT_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0)) Then newdata.iIcon += "AuditPartial|"
                    If ((row.Field(Of Int32?)("IS_AUTOVOUCHING")) > 0) Then newdata.iIcon += "AutoVouching|"
                    If ((row.Field(Of Int32?)("IS_CORRECTED_ENTRY")) > 0) Then newdata.iIcon += "CorrectedEntry|"
                    _AccountList.Add(newdata)
                Next
                Return _AccountList
            End Function

            Public Function Get_BankAccountCheckingList(Optional Checking_Status As String = "ALL", Optional BA_REC_ID As String = Nothing, Optional Vouching_View As Boolean = False) As DataTable
                Checking_Status = IIf(Checking_Status = "ALL", Nothing, Checking_Status)
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "[sp_get_Bank_Balance_Checking]"
                Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@REC_ID", "@Vouching_View", "@CHECKING_STATUS", "@USER_ID"}
                Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, BA_REC_ID, Vouching_View, Checking_Status, cBase._open_User_ID}
                Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String, DbType.Boolean, DbType.String, DbType.String}
                Dim lengths() As Integer = {4, 4, 36, 1, 50, 255}
                Return _RealService.ListFromSP(Tables.BANK_BALANCE_CHECKING_INFO, SPName, Tables.BANK_BALANCE_CHECKING_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Start_BankChecking))
            End Function

            'Public Function GetList(ByVal OnlineQuery As String, ByVal LocalQuery As String, ByVal Screen As ClientScreen) As DataTable
            '    Return GetListOfRecords(OnlineQuery, LocalQuery, Screen, RealTimeService.Tables.BANK_ACCOUNT_INFO, Common.ClientDBFolderCode.SYS)
            'End Function

            ''' <summary>
            ''' Manipulated function to get Bank List, Shifted
            ''' </summary>
            ''' <param name="Screen"></param>
            ''' <returns></returns>
            ''' <remarks>uses : RealServiceFunctions.Bank_GetList_Common</remarks>
            Public Function GetList(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetList_Common, Screen, parameter)
            End Function

            'Vouchers.vb, ClientScreen.Accounts_Vouchers
            'to be removed
            'Public Function GetValue(ByVal OnlineQuery As String, ByVal LocalQuery As String, ByVal Screen As ClientScreen) As Object
            '    Return GetSingleValue(OnlineQuery, LocalQuery, Screen, RealTimeService.Tables.BANK_ACCOUNT_INFO, Common.ClientDBFolderCode.SYS)
            'End Function

            ''' <summary>
            ''' Manipulated, Shifted, returns closed bank accounts as per parameters
            ''' </summary>
            ''' <param name="Screen"></param>
            ''' <param name="parameter"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetValue_Common</remarks>
            Public Function GetValue(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As Object
                Dim rValue As Object = GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetValue_Common, Screen, parameter)
                If IsDBNull(rValue) Then Return ""
                Return rValue
            End Function

            Public Function GetClosedBank_ByMasterID(ByVal Screen As ClientScreen, Tr_m_Id As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetClosedBank, Screen, Tr_m_Id)
            End Function

            ''' <summary>
            ''' Gets FD Account List, Shifted
            ''' </summary>
            ''' <param name="Screen"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetFDAccountList</remarks>
            Public Function GetFDAccountList(ByVal Screen As ClientScreen, Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetFDAccountList, Screen, Bank_Account_Rec_ID)
            End Function

            ''' <summary>
            ''' No need to Shift
            ''' </summary>
            ''' <param name="AccID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetFDIDsByAccID(ByVal AccID As String) As DataTable
                Dim _fd As FD = New FD(cBase)
                Return _fd.GetRecIDByAccID(AccID, ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Get Transactions Count By AccountID, Shifted
            ''' </summary>
            ''' <param name="AccID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetTxnsCountByAccID</remarks>
            Public Function GetTxnsCountByAccID(ByVal AccID As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetTxnsCountByAccID, ClientScreen.Profile_BankAccounts, AccID)
            End Function

            ''' <summary>
            ''' Query need to be handled in Service , when Address class structure is made 
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetSignatories(Optional Signatories_RecID As String = Nothing) As DataTable
                Dim _addresses As Addresses = New Addresses(cBase)
                Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                Param.Signatories_RecID = Signatories_RecID
                Return _addresses.GetList(ClientScreen.Profile_BankAccounts, Param)
            End Function

            ''' <summary>
            '''  Get Bank List, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBankList() As DataTable
                Return GetBankInfo(ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Gets Branch Details, Shifted
            ''' </summary>
            ''' <param name="Branch_IDs"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
                Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Get Branches for Bank, Shifted
            ''' </summary>
            ''' <param name="BankId"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBranchesForBank(ByVal BankId As String)
                Return GetBankBranchesByBankID(BankId, ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Get Branch Details For Id, Shifted
            ''' </summary>
            ''' <param name="BranchID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBranchDetailForID(ByVal BranchID As String) As DataTable
                Return GetBankBranches(BranchID, ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Gets Bank Opening Balance, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBankOpeningBalance() As DataTable
                Return GetOpeningBalance(ClientScreen.Profile_BankAccounts, "BA_ID")
            End Function

            ''' <summary>
            ''' Gets Bank Opening Balance, Shifted
            ''' </summary>
            ''' <param name="AccountRecID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBankOpeningBalance(ByVal AccountRecID) As DataTable
                Return GetOpeningBalance(ClientScreen.Profile_BankAccounts, "OP_AMOUNT", AccountRecID)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <param name="Rec_Id"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetStatus(ByVal Rec_Id As String) As Object
                Return GetRecordStatus(Rec_Id, ClientScreen.Profile_BankAccounts, RealTimeService.Tables.BANK_ACCOUNT_INFO, "BA_CEN_ID")
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <param name="Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetRecord(ByVal Rec_ID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetRecord, ClientScreen.Profile_BankAccounts, Rec_ID)
            End Function

            ''' <summary>
            ''' Gets Last date of Transaction for the mentioned Bank Account, Shifted
            ''' </summary>
            ''' <param name="xAccID"></param>
            ''' <param name="ClosingDate"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetTransactionMaxDate</remarks>
            Public Function GetTransactionMaxDate(ByVal xAccID As String, ByVal ClosingDate As Date) As Object
                Dim LocalQuery As String = " SELECT max(tr_date) as tr_dt" &
                             " FROM Transaction_Info " &
                             " Where    REC_STATUS IN (0,1,2) AND TR_CEN_ID='" & cBase._open_Cen_ID & "'   AND ( TR_SUB_CR_LED_ID='" & xAccID & "' or TR_SUB_DR_LED_ID='" & xAccID & "')  AND TR_DATE >= #" & Format(ClosingDate, cBase._Date_Format_Short) & "# "
                Dim Param As Param_Bank_GetTransactionMaxDate = New Param_Bank_GetTransactionMaxDate()
                Param.ClosingDate = ClosingDate
                Param.xAccID = xAccID
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetTransactionMaxDate, ClientScreen.Profile_BankAccounts, Param)
            End Function

            ''' <summary>
            ''' Get FD Count Before Bank account deletion, Shifted
            ''' </summary>
            ''' <param name="BankAccId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetFDCount</remarks>
            Public Function GetFDCount(ByVal BankAccId As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetFDCount, ClientScreen.Profile_BankAccounts, BankAccId)
            End Function

            ''' <summary>
            ''' Get FD sum after Bank account addition/updation, Shifted
            ''' </summary>
            ''' <param name="BankAccId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetFDSum</remarks>
            Public Function GetFDSum(ByVal BankAccId As String) As Object
                Dim LocalQuery As String = "SELECT SUM(FD_AMT) FROM FD_INFO  WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID='" & cBase._open_Cen_ID & "' AND FD_BA_ID  = '" & BankAccId & "'  AND FD_TR_ID IS NULL AND FD_CLOSE_DATE IS NULL"
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetFDSum, ClientScreen.Profile_BankAccounts, BankAccId)
            End Function

            ''' <summary>
            ''' Get Transaction Count Before Bank account deletion, Shifted 
            ''' </summary>
            ''' <param name="BankAccId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetTransCount</remarks>
            Public Function GetTransCount(ByVal BankAccId As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetTransCount, ClientScreen.Profile_BankAccounts, BankAccId)
            End Function

            ''' <summary>
            ''' Get Dr. and Cr.Transaction Sum, Shifted
            ''' </summary>
            ''' <param name="AccRecId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetTransSums</remarks>
            Public Function GetTransSums(ByVal AccRecId As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetTransSums, ClientScreen.Profile_BankAccounts, AccRecId)
            End Function

            ''' <summary>
            ''' Get Payment Transaction Sum, Shifted
            ''' </summary>
            ''' <param name="AccRecId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetPaymentTransSums</remarks>
            Public Function GetPaymentTransSums(ByVal AccRecId As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetPaymentTransSums, ClientScreen.Profile_BankAccounts, AccRecId)
            End Function

            ''' <summary>
            ''' Get Receipt Transaction Sum, Shifted
            ''' </summary>
            ''' <param name="AccRecId"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetReceiptTransSums</remarks>
            Public Function GetReceiptTransSums(ByVal AccRecId As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetReceiptTransSums, ClientScreen.Profile_BankAccounts, AccRecId)
            End Function

            ''' <summary>
            ''' Get Count by AccountNo,Shifted
            ''' </summary>
            ''' <param name="AccountNo"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetCountByAccountNo</remarks>
            Public Function GetCountByAccountNo(ByVal AccountNo As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetCountByAccountNo, ClientScreen.Profile_BankAccounts, AccountNo)
            End Function

            ''' <summary>
            '''  Get Count by Customer No, Shifted
            ''' </summary>
            ''' <param name="CustomerNo"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetCountByCustomerNo</remarks>
            Public Function GetCountByCustomerNo(ByVal CustomerNo As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetCountByCustomerNo, ClientScreen.Profile_BankAccounts, CustomerNo)
            End Function

            ''' <summary>
            ''' Get FD bank Count by Customer No, Shifted
            ''' </summary>
            ''' <param name="CustomerNo"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_GetFDBankByCustomerNo</remarks>
            Public Function GetFDBankByCustomerNo(ByVal CustomerNo As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Bank_GetFDBankByCustomerNo, ClientScreen.Profile_BankAccounts, CustomerNo)
            End Function

            ''' <summary>
            ''' Get Centre Task, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetCenterTask() As DataTable
                Return GetCenterTaskInfo(ClientScreen.Profile_BankAccounts)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <param name="Rec_ID"></param>
            ''' <param name="Screen"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetLastEditTime(ByVal Rec_ID As String, ByVal Screen As Common_Lib.RealTimeService.ClientScreen) As Object
                Return GetLastEditOn(Rec_ID, Screen, RealTimeService.Tables.BANK_ACCOUNT_INFO, cBase._data_ConStr_Sys)
            End Function
            Public Function GetClosedBankAccNo(ByVal Rec_ID As String, ByVal Screen As Common_Lib.RealTimeService.ClientScreen) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Bank_GetCountByCustomerNo, Screen, Rec_ID)
            End Function
            Public Function IsBankCarriedForward(ByVal Rec_ID As String, ByVal recYearID As String) As Boolean?
                Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_BankAccounts, Tables.BANK_ACCOUNT_INFO)
            End Function

            ''' <summary>
            ''' Insert Bank And Balance, Shifted
            ''' </summary>
            ''' <param name="InParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_Insert_Bank_and_Balance</remarks>
            Public Function Insert_Bank_and_Balance(ByVal InParam As Parameter_Insert_BankandBalance_BankAccounts) As Boolean
                Dim result As Boolean = False
                InParam.YearID = cBase._open_Year_ID 'used in insertion of opening balances 
                result = InsertRecord(RealTimeService.RealServiceFunctions.Bank_Insert_Bank_and_Balance, ClientScreen.Profile_BankAccounts, InParam)
                Return result
            End Function
            Public Function InsertBankPassbookBalance(ByVal BA_REC_ID As String, ByVal Passbook_Balance As Decimal, ByVal Account_Status As String, Optional Last_Txn_Date? As DateTime = Nothing) As Boolean
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "[sp_insert_bank_balance_checking_data]"
                Dim params() As String = {"@BA_REC_ID", "@PASSBOOK_BALANCE", "@ACCOUNT_STATUS", "@LAST_TXN_DATE", "@USERNAME"}
                Dim values() As Object = {BA_REC_ID, Passbook_Balance, Account_Status, Last_Txn_Date, cBase._open_User_ID}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Decimal, DbType.String, DbType.DateTime2, DbType.String}
                Dim lengths() As Integer = {36, 9, 10, 7, 255}
                _RealService.InsertBySPPublic(Tables.BANK_BALANCE_CHECKING_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Start_BankChecking))
                Return True
            End Function

            ''' <summary>
            ''' Update Bank And Balance, Shifted
            ''' </summary>
            ''' <param name="UpParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_Update_Bank_and_Balance</remarks>
            Public Function Update_Bank_and_Balance(ByVal UpParam As Parameter_Update_BankandBalance_BankAccounts) As Boolean
                Dim result As Boolean = False

                cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_BankAccounts)

                result = UpdateRecord(RealTimeService.RealServiceFunctions.Bank_Update_Bank_and_Balance, ClientScreen.Profile_BankAccounts, UpParam)
                Return result
            End Function

            ''' <summary>
            '''  Mark Bank Account as closed, Shifted
            ''' </summary>
            ''' <param name="CloseDate"></param>
            ''' <param name="Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_Close</remarks>
            Public Function Close(ByVal CloseDate As String, ByVal Rec_ID As String) As Boolean
                Dim UpParam As Param_Bank_Close = New Param_Bank_Close()
                UpParam.CloseDate = CloseDate
                UpParam.Rec_ID = Rec_ID
                Return UpdateRecord(RealTimeService.RealServiceFunctions.Bank_Close, ClientScreen.Profile_BankAccounts, UpParam)
            End Function

            ''' <summary>
            ''' Reopens Bank Account, Shifted
            ''' </summary>
            ''' <param name="Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Bank_Reopen</remarks>
            Public Function Reopen(ByVal Rec_ID As String) As Boolean
                Dim Param As Param_Reopen_BankAccounts = New Param_Reopen_BankAccounts()
                Param.Rec_ID = Rec_ID
                Return UpdateRecord(RealTimeService.RealServiceFunctions.Bank_Reopen, ClientScreen.Profile_BankAccounts, Param)
            End Function

            Public Overloads Function Delete_and_Remove_Balance(ByVal Rec_Id As String) As Boolean
                Dim Result As Boolean = False
                'Remove all Vouching posted against entry 
                DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_BankAccounts)
                DeleteByCondition("REF_ID ='" & Rec_Id & "' and [REF_NAME] = 'bank_account_info' and [SVR_NAME] = 'CORPUS'", Tables.REFERENCE_OPENING_INFO, ClientScreen.Profile_BankAccounts)
                DeleteByCondition("REF_ID ='" & Rec_Id & "' and [REF_NAME] = 'bank_account_info' and [SVR_NAME] = 'RELOADABLE_CARD'", Tables.REFERENCE_OPENING_INFO, ClientScreen.Profile_BankAccounts)
                Result = DeleteRecord(Rec_Id, Tables.BANK_ACCOUNT_INFO, ClientScreen.Profile_BankAccounts)
                If Result Then
                    Result = DeleteOpeningBalance(Rec_Id, ClientScreen.Profile_BankAccounts)
                End If
                Return Result
            End Function

            Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
                Dim Completed As Boolean = MyBase.MarkAsComplete(Rec_Id, Tables.BANK_ACCOUNT_INFO, ClientScreen.Profile_BankAccounts)
                If Completed Then
                    Completed = MyBase.MarkAsComplete(Rec_Id, Tables.OPENING_BALANCES_INFO, ClientScreen.Profile_BankAccounts)
                End If
                Return Completed
            End Function

            Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
                Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.BANK_ACCOUNT_INFO, ClientScreen.Profile_BankAccounts)
                If Locked Then
                    Return MyBase.MarkAsLocked(Rec_Id, Tables.OPENING_BALANCES_INFO, ClientScreen.Profile_BankAccounts)
                Else
                    Return False
                End If
                Return Locked
            End Function

            Public Function get_genuineAccount_not(ByVal Accountno As String) As DataTable
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim paramters As String() = {"@acc_no", "@YearID"}
                Dim values() As Object = {Accountno, cBase._open_Year_ID}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32}
                Dim lengths() As Integer = {50, 4}
                Return _RealService.ListFromSP(Tables.BANK_ACCOUNT_INFO, "[sp_get_genuineBankAccountCheck]", Tables.BANK_ACCOUNT_INFO.ToString(), paramters, values, dbTypes, lengths,
                                           GetBaseParams(ClientScreen.Profile_BankAccounts))
            End Function
            Public Function GetAPIActivatedBankAccount(Optional ByVal Accountno As String = Nothing) As DataTable
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim paramters As String() = {"@Accno"}
                Dim values() As Object = {Accountno}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Date, DbType.Date}
                Dim lengths() As Integer = {50, -1, 8, 8}
                Return _RealService.ListFromSP(Tables.API_ACCOUNTS_ACTIVATED, "[Get_Api_Accounts_Activated]", Tables.API_ACCOUNTS_ACTIVATED.ToString(), paramters, values, dbTypes, lengths,
                                           GetBaseParams(ClientScreen.Profile_BankAccounts))
            End Function
            Public Function InsertBankTransactions(ByVal Accountno As String, ByVal transaction As String, ByVal FromDate As Date, ByVal ToDate As Date) As Boolean
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim paramters As String() = {"@AccountNo", "@Transactions", "@FromDate", "@ToDate"}
                Dim values() As Object = {Accountno, transaction, FromDate, ToDate}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Date, DbType.Date}
                Dim lengths() As Integer = {50, -1, 8, 8}
                _RealService.InsertBySPPublic(Tables.BANK_ACCOUNT_INFO, "[Insert_BankTransaction_Info]", paramters, values, dbTypes, lengths,
                                           GetBaseParams(ClientScreen.Profile_BankAccounts))
                Return True
            End Function
        Public Function GetStoredbankStatementData(ByVal date_from As String, ByVal date_to As String, ByVal accountno As String, ByVal account_ID As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@AccountNo", "@FromDate", "@ToDate", "@AccountID", "@CenID"}
            Dim values() As Object = {accountno, date_from, date_to, account_ID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Date, DbType.Date, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {255, 8, 8, 36, 4}
            Return _RealService.ListDatasetFromSP(Tables.BANK_TRANSACTION_INFO, "Get_Bank_Transactions", params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_BankAccounts))
        End Function

        Public Function InsertBankStatementReference(ByVal InParam As BankStatement_Reference) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@AccountNo", "@FromDate", "@ToDate", "@ReferenceNo"}
            Dim values() As Object = {InParam.AccountNo, InParam.FromDate, InParam.ToDate, InParam.ReferenceNo}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Date, DbType.Date, DbType.String}
            Dim lengths() As Integer = {255, 8, 8, 36}
            _RealService.InsertBySPPublic(Tables.BANKSTATEMENT_REFERENCE_INFO, "Insert_BankStatement_Reference_Info", params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_BankAccounts))
            Return True
        End Function
        Public Function CheckBankStatementReference(ByVal InParam As BankStatement_Reference) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@AccountNo", "@FromDate", "@ToDate"}
            Dim values() As Object = {InParam.AccountNo, InParam.FromDate, InParam.ToDate}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Date, DbType.Date}
            Dim lengths() As Integer = {255, 8, 8}
            Return _RealService.ListFromSP(Tables.BANKSTATEMENT_REFERENCE_INFO, "Get_BankStatement_Reference", Tables.BANKSTATEMENT_REFERENCE_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_BankAccounts))
        End Function
        Public Function DeleteBankStatementReference(ByVal InParam As BankStatement_Reference) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@AccountNo", "@FromDate", "@ToDate", "@ReferenceNo"}
            Dim values() As Object = {InParam.AccountNo, InParam.FromDate, InParam.ToDate, InParam.ReferenceNo}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Date, DbType.Date, DbType.String}
            Dim lengths() As Integer = {255, 8, 8, 36}
            _RealService.UpdateBySPPublic(Tables.BANKSTATEMENT_REFERENCE_INFO, "Delete_BankStatement_Reference", params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_BankAccounts))
            Return True
        End Function

        Public Function getBankAccountToCheckMapping(ByVal ID_From As Int32, ByVal ID_To As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@ID_FROM", "@ID_TO"}
            Dim values() As Object = {ID_From, ID_To}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {8, 8}
            Return _RealService.ListFromSP(Tables.API_ACCOUNTS_ACTIVATED, "[get_bankAccountToCheckMapping]", Tables.API_ACCOUNTS_ACTIVATED.ToString(), paramters, values, dbTypes, lengths,
                                       GetBaseParams(ClientScreen.Profile_BankAccounts))
        End Function
        Public Function updateBankAccountMappingStatus(ByVal ID As Int32, ByVal MESSAGE As String, ByVal ACCOUNT_NO As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[update_bankAccountMappingStatus]"
            Dim params() As String = {"@ID", "@MESSAGE", "@ACCOUNT_NO"}
            Dim values() As Object = {ID, MESSAGE, ACCOUNT_NO}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 8000, 25}
            _RealService.InsertBySPPublic(Tables.BANK_BALANCE_CHECKING_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Start_BankChecking))
            Return True
        End Function
    End Class
    End Class
