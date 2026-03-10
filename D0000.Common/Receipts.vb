'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Voucher_Receipt
        Inherits SharedVariables
        <Serializable>
        Public Class Return_RefBank
            Public BI_ID As String
            Public BI_BANK_NAME As String
            Public BI_SHORT_NAME As String
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        ''' <summary>
        ''' Get MasterID, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetMasterID</remarks>
        Public Function GetMasterID(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Receipts_GetMasterID, ClientScreen.Accounts_Voucher_Receipt, Rec_Id)
        End Function

        'Shifted
        Public Function GetItemsListByID(ByVal Rec_Id As String) As Object
            Dim Query As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.ItemIDs = Rec_Id
            inParam.Type = "1"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Receipt, inParam) 'Type=1
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        Public Function GetSlipRecord(ByVal TR_M_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", TR_M_ID, ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_D_SLIP_INFO)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByCustom("WHERE TR_M_ID ='" & Rec_ID & "' AND TR_SR_NO = 1 AND TR_CR_LED_ID IS NOT NULL AND TR_DR_LED_ID IS NOT NULL ", ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetJournalRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByCustom("WHERE TR_M_ID ='" & Rec_ID & "' AND TR_SR_NO = 1 ", ClientScreen.Accounts_Voucher_Receipt, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        ''' <summary>
        ''' Get Txn Items, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetTxnItems</remarks>
        Public Function GetTxnItems(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Receipts_GetTxnItems, ClientScreen.Accounts_Voucher_Receipt, Rec_ID)
        End Function

        ''' <summary>
        ''' Get Bank Payments, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetBankPayments</remarks>
        Public Function GetBankPayments(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Receipts_GetBankPayments, ClientScreen.Accounts_Voucher_Receipt, Rec_ID)
        End Function

        ''' <summary>
        ''' Get Advances, Shifted
        ''' </summary>
        ''' <param name="inparam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetAdvances</remarks>
        Public Function GetAdvances(ByVal inparam As Param_GetReceiptAdvances) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Receipts_GetAdvances, ClientScreen.Accounts_Voucher_Receipt, inparam)
        End Function

        ''' <summary>
        ''' Get Advances List, shifted
        ''' </summary>
        ''' <param name="PartyIDs"></param>
        ''' <param name="IsNew"></param>
        ''' <param name="TxnMID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetAdvancesList</remarks>
        Public Function GetAdvancesRefundList(ByVal PartyIDs As String, ByVal IsNew As Boolean, YearID As Integer, Optional ByVal TxnMID As String = Nothing, Optional NextYearId As Integer = Nothing) As DataTable
            Dim Param As Param_Receipts_GetAdvancesRefundList = New Param_Receipts_GetAdvancesRefundList()
            Param.IsNew = IsNew
            Param.PartyIDs = PartyIDs
            Param.TxnMID = TxnMID
            Param.YearID = YearID
            Param.NextUnAuditedYearID = NextYearId
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Receipts_GetAdvancesRefundList, ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        'Shifted
        Public Function GetPendingAdvances(ByVal PartyID As String, ByVal ItemID As String, Optional Adv_Rec_Id As String = Nothing) As DataTable
            Dim _Adv As Advances = New Advances(cBase)
            Dim Param As Param_Advances_GetList_Common = New Param_Advances_GetList_Common()
            Param.PartyID = PartyID
            Param.ItemID = ItemID
            Param.Adv_Rec_ID = Adv_Rec_Id
            Return _Adv.GetList(ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        'Public Function GetLiabilitiesList(ByVal PartyIDs As String, Optional ByVal TxnMID As String = Nothing) As DataTable
        '    Dim Query As String = ""
        '    If Not TxnMID Is Nothing Then
        '        Query = " SELECT TP.TR_REF_ID, SUM(TP.TR_REF_AMT) AS Paid FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE='LIABILITIES' AND TM.TR_CEN_ID  = '" & cBase._open_Cen_ID & "' AND TM.TR_AB_ID_1='" & PartyIDs & "' AND TP.TR_M_ID  <> '" & TxnMID & "'  GROUP BY TP.TR_REF_ID "
        '    Else
        '        Query = " SELECT TP.TR_REF_ID, SUM(TP.TR_REF_AMT) AS Paid FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE='LIABILITIES' AND TM.TR_CEN_ID  = '" & cBase._open_Cen_ID & "' AND TM.TR_AB_ID_1='" & PartyIDs & "'  GROUP BY TP.TR_REF_ID "
        '    End If
        '    Return GetListOfRecords(Query, Query, ClientScreen.Accounts_Voucher_Receipt, Tables.TRANSACTION_D_PAYMENT_INFO, Common.ClientDBFolderCode.DATA)
        'End Function

        'Public Function GetPendingLiabilities(ByVal PartyID As String) As DataTable
        '    Dim LocalQuery As String = " SELECT 0 AS [Sr],LI_DATE as [Date],'' AS [Item],'' as OFFSET_ID, LI_ITEM_ID,LI_AMT as [Amount],0 as [Paid],0 AS [Out-Standing],0 as [Payment],LI_PURPOSE as [Purpose],LI_OTHER_DETAIL as [Other Details],REC_ID AS [LI_ID] FROM Liabilities_Info WHERE  REC_STATUS IN (0,1,2) AND LI_CEN_ID  = '" & cBase._open_Cen_ID & "' AND LI_PARTY_ID='" & PartyID & "' order by LI_DATE "
        '    Dim OnlineQuery As String = " SELECT 0 AS Sr,LI_DATE as 'Date','' AS Item,'' as OFFSET_ID, LI_ITEM_ID,LI_AMT as Amount,0 as Paid,0 AS 'Out-Standing',0 as Payment,LI_PURPOSE as Purpose,LI_OTHER_DETAIL as 'Other Details',REC_ID AS LI_ID FROM Liabilities_Info WHERE  REC_STATUS IN (0,1,2) AND LI_CEN_ID  = '" & cBase._open_Cen_ID & "' AND LI_PARTY_ID='" & PartyID & "' order by LI_DATE "
        '    Dim _Liab As Liabilities = New Liabilities(cBase)
        '    Return _Liab.GetList(OnlineQuery, LocalQuery, ClientScreen.Accounts_Voucher_Receipt)
        'End Function

        'Shifted
        Public Function GetPendingDeposits(ByVal inparam As Param_GetReceiptDeposits) As DataTable
            'Dim LocalQuery As String = " SELECT 0 AS [Sr],DI_DEP_DATE  AS [REF_DATE]  ,DI_DEP_AMT AS [REF_AMT] ,0 AS REF_ADDITION ,0 as [REF_ADJUSTED],0 as [REF_REFUND],0 as [REF_OUTSTAND],'' AS [REF_ITEM], DI_ITEM_ID AS [REF_ITEM_ID],DI_PURPOSE AS [REF_PURPOSE] ,DI_OTHER_DETAIL AS [REF_OTHER_DETAIL] ,REC_ID AS [REF_ID] FROM DEPOSITS_INFO WHERE REC_STATUS IN (0,1,2) AND DI_CEN_ID  = '" & cBase._open_Cen_ID & "' AND DI_PARTY_ID='" & PartyID & "' AND DI_ITEM_ID='" & ItemID & "' order by DI_DEP_DATE "
            'Dim _dep As Deposits = New Deposits(cBase)
            'Dim Param As Param_Deposits_GetListCommon = New Param_Deposits_GetListCommon()
            'Param.ItemID = ItemID
            'Param.PartyID = PartyID
            'Param.Dep_Rec_ID = Dep_Rec_Id
            'Return _dep.GetList(ClientScreen.Accounts_Voucher_Receipt, Param)
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Receipts_GetDeposits, ClientScreen.Accounts_Voucher_Receipt, inparam)
        End Function

        ''' <summary>
        ''' Get Deposits List, Shifted
        ''' </summary>
        ''' <param name="PartyIDs"></param>
        ''' <param name="IsNew"></param>
        ''' <param name="TxnMID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetDepositsList</remarks>
        Public Function GetDepositsRefundList(ByVal PartyIDs As String, ByVal IsNew As Boolean, YearID As Integer, Optional ByVal TxnMID As String = Nothing, Optional NextUnauditedYearID As Integer = Nothing) As DataTable
            Dim Param As Param_Receipts_GetDepositsRefundList = New Param_Receipts_GetDepositsRefundList()
            Param.PartyIDs = PartyIDs
            Param.IsNew = IsNew
            Param.TxnMID = TxnMID
            Param.YearID = YearID
            Param.NextUnauditedYearID = NextUnauditedYearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Receipts_GetDepositsRefundList, ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        ''' <summary>
        ''' Get Liabilities, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Receipts_GetLiabilities</remarks>
        Public Function GetLiabilities(ByVal Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Receipts_GetLiabilities, ClientScreen.Accounts_Voucher_Receipt, Rec_ID)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional ByVal ForeignOnly As Boolean = False, Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
            Param.ForeignOnly = ForeignOnly
            Param.Bank_Account_Rec_ID = Bank_Account_Rec_ID
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Return _BA.GetList(ClientScreen.Accounts_Voucher_Receipt, Param)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_Receipt, " B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID, B.REC_ID AS BANK_ID ")
        End Function
        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBanks() As List(Of Return_RefBank)
            Dim Query As String = "SELECT BI_BANK_NAME, BI_SHORT_NAME, REC_ID as BI_ID From  BANK_INFO  Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Dim ret_table As DataTable = GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Receipt)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim banklist = New List(Of Return_RefBank)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_RefBank()
                    newdata.BI_ID = row.Field(Of String)("BI_ID")
                    newdata.BI_BANK_NAME = row.Field(Of String)("BI_BANK_NAME")
                    newdata.BI_SHORT_NAME = row.Field(Of String)("BI_SHORT_NAME")
                    banklist.Add(newdata)
                Next
                Return banklist
            End If
        End Function

        'Shifted
        Public Function GetParties(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _Add As Addresses = New Addresses(cBase)
            Dim param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            param.Party_Rec_ID = Party_Rec_ID
            Return _Add.GetList(ClientScreen.Accounts_Voucher_Receipt, param)
        End Function

        'Shifted
        Public Function GetPartyCities(ByVal CityIDs) As DataTable
            Return GetCitiesByID(ClientScreen.Accounts_Voucher_Gift, "CI_NAME", "REC_ID", CityIDs)
        End Function

        'Shifted
        Public Function GetItemsList(ByVal ItemID As String) As DataTable
            Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & ItemID & "') AND  REC_STATUS IN (0,1,2)  ;"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.ItemIDs = ItemID
            inParam.Type = "2"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Receipt) 'Type=2
        End Function

        'Shifted
        Public Function GetPurposes() As DataTable
            ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Internal_Transfer, "PUR_NAME", "PUR_ID")
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = "PUR_NAME"
            InParam.RecIDColumnHead = "PUR_ID"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Internal_Transfer, InParam)
        End Function

        'Public Function GetLiabilityItems() As DataTable
        '    Dim Query As String = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME,ITEM_OFFSET_REC_ID    from ITEM_INFO where  ITEM_PROFILE='OTHER LIABILITIES' AND  REC_STATUS IN (0,1,2)  ;"
        '    Return GetItemsByQuery(Query, ClientScreen.Accounts_Voucher_Receipt)
        'End Function   

        'Shifted
        Public Function GetLedgerItems(ByVal Is_HQ_Centre As Boolean) As DataTable
            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
            Dim SQL_STR1 As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " & _
                                 " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('RECEIPTS') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            SQL_STR1 += "  Union All " & _
                                     " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " & _
                                     " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('RECEIPTS - INSTITUTE')  AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND I.ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            OnlineParam.open_Ins_ID = cBase._open_Ins_ID
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_Receipt, SQL_STR1, OnlineParam)
        End Function

        'Shifted
        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Receipt, MiscNameColumnHead, RecIDColumnHead)
        End Function

        ''' <summary>
        ''' Insert MasterInfo, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Receipts_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherReceipt) As Boolean
            ' InMInfo.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_InsertMasterInfo, ClientScreen.Accounts_Voucher_Receipt, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherReceipt) As Boolean
            ' InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_Insert, ClientScreen.Accounts_Voucher_Receipt, InParam)
        End Function

        ''' <summary>
        ''' Insert Item, Shifted
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertItem</remarks>
        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherReceipt) As Boolean
            ' InItem.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_InsertItem, ClientScreen.Accounts_Voucher_Receipt, InItem)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherReceipt) As Boolean
            ' InPurpose.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_InsertPurpose, ClientScreen.Accounts_Voucher_Receipt, InPurpose)
        End Function

        ''' <summary>
        ''' Advances and Liabilities, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertPayment</remarks>
        ''' 
        Public Function InsertPayment(ByVal InPayment As Parameter_InsertAandLPayment_VoucherReceipt) As Boolean
            ' InPayment.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_InsertPayment, ClientScreen.Accounts_Voucher_Receipt, InPayment)
        End Function

        ''' <summary>
        ''' Bank Payment, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertBankPayment</remarks>
        Public Function InsertPayment(ByVal InBpayment As Parameter_InsertBankPayment_VoucherReceipt) As Boolean
            ' InBpayment.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Receipts_InsertBankPayment, ClientScreen.Accounts_Voucher_Receipt, InBpayment)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherReceipt) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Receipt)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Receipts_UpdateMaster, ClientScreen.Accounts_Voucher_Gift, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Receipt)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Receipt)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Receipt)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_Receipt)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Receipt)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Receipt)
        End Function

        Public Overloads Function DeletePayment(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Receipt)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_Receipt)
        End Function

        Public Overloads Function DeleteMaster(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Receipt)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Receipt)
        End Function

        Public Function InsertReceipt_Txn(InParam As Param_Txn_Insert_VoucherReceipt) As Boolean
            'InParam.param_Insert.openYearID = cBase._open_Year_ID
            'InParam.param_InsertCreditJV.openYearID = cBase._open_Year_ID
            'InParam.param_InsertDebitJV.openYearID = cBase._open_Year_ID
            'InParam.param_InPmtAdjusted.openYearID = cBase._open_Year_ID
            'InParam.param_InPmtRefund.openYearID = cBase._open_Year_ID
            'InParam.param_InsertMaster.openYearID = cBase._open_Year_ID
            'InParam.param_InsertPurpose.openYearID = cBase._open_Year_ID
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Receipts_InsertReceipt_Txn, ClientScreen.Accounts_Voucher_Receipt, InParam)
        End Function

        Public Function UpdateReceipt_Txn(UpParam As Param_Txn_Update_VoucherReceipt) As Boolean
            'UpParam.param_InPmtAdjusted.openYearID = cBase._open_Year_ID
            'UpParam.param_InPmtRefund.openYearID = cBase._open_Year_ID
            'UpParam.param_Insert.openYearID = cBase._open_Year_ID
            'UpParam.param_InsertCreditJV.openYearID = cBase._open_Year_ID
            'UpParam.param_InsertDebitJV.openYearID = cBase._open_Year_ID
            'UpParam.param_InsertPurpose.openYearID = cBase._open_Year_ID
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateMaster.RecID, ClientScreen.Accounts_Voucher_Receipt)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Receipts_UpdateReceipt_Txn, ClientScreen.Accounts_Voucher_Receipt, UpParam)
        End Function

        Public Function DeleteReceipt_Txn(delParam As Param_Txn_Delete_VoucherReceipt) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(delParam.MID_Delete, ClientScreen.Accounts_Voucher_Payment)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + delParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Receipts_DeleteReceipt_Txn, ClientScreen.Accounts_Voucher_Receipt, delParam)
        End Function

    End Class
#End Region
End Class
