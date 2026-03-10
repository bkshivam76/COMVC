'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Accounts"
    <Serializable>
    Public Class Vouchers
        Inherits SharedVariables
#Region " Return Classes"
        <Serializable>
        Public Class Return_GetSlipMasterRecord
            Public SL_CEN_ID As Integer?
            Public SL_COD_YEAR_ID As Integer?
            Public SL_PRINT_DATE As Date?
            Public SL_NO As Integer?
            Public REC_ADD_ON As Date?
            Public REC_ADD_BY As String
            Public REC_STATUS As Integer?
            Public SL_BA_REC_ID As String
            Public ID As Integer
            Public REC_ID As String
        End Class
        <Serializable>
        Public Class Return_GetVouchingAuditDashboard
            Public Property Category As String
            Public Property Instt As String
            Public Property Financial_Year As Int32
            Public Property Entries_Available As Int32
            Public Property PhotoUrl As String
            Public Property LinkURL As String
            Public Property ID As Int32
        End Class

        <Serializable>
        Public Class Return_SplVchrRefsList
            Public Property ID As String
            Public Property Task_Name As String
            Public Property PERMISSION As String
            Public Property disabled As Boolean
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub
        ''' <summary>
        ''' Gets Max Transaction Date, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Vouchers_GetMaxTransactionDate</remarks>
        Public Function GetMaxTransactionDate() As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Vouchers_GetMaxTransactionDate, ClientScreen.Accounts_Notebook)
            End Function

            ''' <summary>
            ''' Gets all adjustments made to specified RecID of entity for Debit if IsDebit = true else credit
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks> RealServiceFunctions.Vouchers_GetAdjustments</remarks>
            Public Function GetAdjustments(ByVal screen As Common_Lib.RealTimeService.ClientScreen, ByVal RefID As String, ByVal IsDebit As Boolean, YearID As Integer, Optional ByVal TableName As String = "", Optional NextUnauditedYear As Integer = Nothing, Optional Excluded_Rec_M_ID As String = Nothing) As Object
                Dim param As Parameter_GetAdjustments = New Parameter_GetAdjustments
                param.CrossRefId = RefID
                param.EntryType = IIf(IsDebit, "DEBIT", "CREDIT")
                param.TableName = TableName
                param.YearID = YearID
                param.Excluded_Rec_M_ID = Excluded_Rec_M_ID
                param.NextUnauditedYear = NextUnauditedYear
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetAdjustments, screen, param)
            End Function

            ''' <summary>
            ''' Gets Cash Balance Summary, Shifted
            ''' </summary>
            ''' <param name="FROM_DATE"></param>
            ''' <param name="TO_DATE"></param>
            ''' <param name="YEAR_START_DATE"></param>
            ''' <param name="CEN_ID"></param>
            ''' <param name="YEAR_ID"></param>
            ''' <param name="INS_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetCashBalanceSummary</remarks>
            Public Function GetCashBalanceSummary(ByVal FROM_DATE As Date, ByVal TO_DATE As Date, ByVal YEAR_START_DATE As Date, ByVal CEN_ID As Integer, ByVal YEAR_ID As Integer, ByVal INS_ID As String) As DataTable
                'Store Procedure:  CALL Get_Cash_Balance_Summary('2011-04-01','2012-03-31','2011-04-01','00207','1112','00001')
                'Dim Query As String = "Get_Cash_Balance_Summary"
                'Dim params() As String = {"X_FROM_DATE", "X_TO_DATE", "X_YEAR_START_DATE", "X_CEN_ID", "X_YEAR_ID", "X_INS_ID"}
                'Dim values() As Object = {FROM_DATE, TO_DATE, YEAR_START_DATE, CEN_ID, YEAR_ID, INS_ID}
                'Dim dbTypes() As System.Data.DbType = {Data.DbType.Date, Data.DbType.Date, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String}
                'Dim lengths() As Integer = {10, 10, 10, 5, 4, 5}
                'Return GetListOfRecordsBySP(Query, Query, ClientScreen.Accounts_Vouchers, Tables.OPENING_BALANCES_INFO, Common.ClientDBFolderCode.DATA, params, values, dbTypes, lengths)
                Dim Param As Param_Vouchers_GetCashBalanceSummary = New Param_Vouchers_GetCashBalanceSummary()
                Param.FROM_DATE = FROM_DATE
                Param.TO_DATE = TO_DATE
                Param.YEAR_START_DATE = YEAR_START_DATE
                Param.YEAR_ID = YEAR_ID
                Param.INS_ID = INS_ID
                Param.CEN_ID = CEN_ID
                Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Vouchers_GetCashBalanceSummary, ClientScreen.Accounts_Vouchers, Param)
            End Function

            ''' <summary>
            ''' Get Bank Balance Summary, Shifted
            ''' </summary>
            ''' <param name="FROM_DATE"></param>
            ''' <param name="TO_DATE"></param>
            ''' <param name="YEAR_START_DATE"></param>
            ''' <param name="CEN_ID"></param>
            ''' <param name="YEAR_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetBankBalanceSummary</remarks>
            Public Function GetBankBalanceSummary(ByVal FROM_DATE As Date, ByVal TO_DATE As Date, ByVal YEAR_START_DATE As Date, ByVal CEN_ID As Integer, ByVal YEAR_ID As Integer) As DataTable
                'Store Procedure:  CALL Get_Bank_Balance_Summary('2011-04-01','2012-03-31','2011-04-01','00640','1112')
                'Dim Query As String = "Get_Bank_Balance_Summary"
                'Dim params() As String = {"X_FROM_DATE", "X_TO_DATE", "X_YEAR_START_DATE", "X_CEN_ID", "X_YEAR_ID"}
                'Dim values() As Object = {FROM_DATE, TO_DATE, YEAR_START_DATE, CEN_ID, YEAR_ID}
                'Dim dbTypes() As System.Data.DbType = {Data.DbType.Date, Data.DbType.Date, Data.DbType.Date, Data.DbType.String, Data.DbType.String}
                'Dim lengths() As Integer = {10, 10, 10, 5, 4}
                'Return GetListOfRecordsBySP(Query, Query, ClientScreen.Accounts_Vouchers, Tables.BANK_ACCOUNT_INFO, Common.ClientDBFolderCode.DATA, params, values, dbTypes, lengths)
                Dim Param As Param_Vouchers_GetBankBalanceSummary = New Param_Vouchers_GetBankBalanceSummary()
                Param.FROM_DATE = FROM_DATE
                Param.TO_DATE = TO_DATE
                Param.YEAR_START_DATE = YEAR_START_DATE
                Param.YEAR_ID = YEAR_ID
                Param.CEN_ID = CEN_ID
                Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Vouchers_GetBankBalanceSummary, ClientScreen.Accounts_Vouchers, Param)
            End Function

            'Shifted
            Public Function GetCenterList(ByVal CEN_REC_IDs As String) As DataTable
            Dim LocalQuery As String = " SELECT C.CEN_NAME ,C.CEN_BK_PAD_NO,C.REC_ID FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                      " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=TRUE AND  C.REC_ID IN (" & CEN_REC_IDs & ")"
            Return GetCenterDetailsByQuery(LocalQuery, ClientScreen.Accounts_Vouchers, CEN_REC_IDs)
            End Function

            'Shifted
            Public Function GetCenterOpeningBalance() As DataTable
                Return GetOpeningBalance(ClientScreen.Accounts_Vouchers, "BA_ID")
            End Function

            'Shifted
            Public Function GetBankBalance(ByVal BankIDs As String) As DataTable
                Return GetBankBalanceAmountIdWise(BankIDs, ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetCashBankTransSum(ByVal FromDate As Date) As DataTable
                Return GetCashBankTransSumAmount(FromDate, ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetBankTransSumForNormalEntries(ByVal BranchID As String) As DataTable
                Dim inParam As Param_GetBankTransSumAmount_Common = New Param_GetBankTransSumAmount_Common()
                inParam.BranchID = BranchID
                inParam.Type = "1"
                Return GetBankTransSumAmount(ClientScreen.Accounts_Vouchers, inParam) 'Type=1
            End Function

            'Shifted
            Public Function GetBankPaymentTransForTransferEntries(ByVal BranchID As String) As DataTable
                Dim inParam As Param_GetBankTransSumAmount_Common = New Param_GetBankTransSumAmount_Common()
                inParam.BranchID = BranchID
                inParam.Type = "2"
                Return GetBankTransSumAmount(ClientScreen.Accounts_Vouchers, inParam) 'Type=2
            End Function

            'Shifted
            Public Function GetBankReceiptsTransForTransferEntries(ByVal BranchID As String) As DataTable
                Dim inParam As Param_GetBankTransSumAmount_Common = New Param_GetBankTransSumAmount_Common()
                inParam.BranchID = BranchID
                inParam.Type = "3"
                Return GetBankTransSumAmount(ClientScreen.Accounts_Vouchers, inParam) 'Type=3
            End Function

            'Shifted
            Public Function GetCashTransSum() As DataTable
                Return GetCashTransSumAmount(ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetCashBankTransSum(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
                Return GetCashBankTransSumAmount(FromDate, ToDate, ClientScreen.Accounts_Vouchers)
            End Function

            ''' <summary>
            ''' Returns First txn Entry where RefID has been used, Shifted
            ''' </summary>
            ''' <param name="RefID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceTxnRecord</remarks>
            Public Function GetReferenceTxnRecord(ByVal RefID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetReferenceTxnRecord, ClientScreen.Accounts_Vouchers, RefID)
            End Function

            ''' <summary>
            ''' Returns First txn Entry where RefID has been used excluding given txnMID
            ''' </summary>
            ''' <param name="RefID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceTxnRecord_ExcludeM_ID</remarks>
            Public Function GetReferenceTxnRecord_Exclude_MID(ByVal RefID As String, ByVal Excluded_MID As String) As DataTable
                Dim param As Parameter_GetReferenceTxnRecord_MID = New Parameter_GetReferenceTxnRecord_MID
                param.CrossRefId = RefID
                param.Excluded_M_ID = Excluded_MID
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetReferenceTxnRecord_ExcludeM_ID, ClientScreen.Accounts_Vouchers, param)
            End Function

            ''' <summary>
            ''' Gets Referred Asset ID in provide Txn, Shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceRecordID</remarks>
            Public Function GetReferenceRecordID(ByVal TxnMID As String) As String
                Dim value As Object = GetSingleValue_Data(RealTimeService.RealServiceFunctions.Vouchers_GetReferenceRecordID, ClientScreen.Accounts_Vouchers, TxnMID)
                If IsDBNull(value) Then
                    Return ""
                End If
                Return value.ToString()
            End Function

            ''' <summary>
            ''' Gets Referred Record IDs in a Txn, Shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetRefRecordIDS(ByVal TxnMID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetRefRecordIDS, ClientScreen.Accounts_Vouchers, TxnMID)
            End Function

            ''' <summary>
            ''' Returns First txn Entry where RefID has been used as sale of Assets, Shifted
            ''' </summary>
            ''' <param name="RefID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetSaleReferenceRecord</remarks>
            Public Function GetSaleReferenceRecord(ByVal RefID As String, Optional Exclude_PrevYears As Boolean = False) As DataTable
                Dim Param As Param_GetSaleReferenceRecord = New Param_GetSaleReferenceRecord
                Param.RefID = RefID
                Param.Exclude_PrevYears = Exclude_PrevYears
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetSaleReferenceRecord, ClientScreen.Accounts_Vouchers, Param)
            End Function

            ''' <summary>
            ''' Returns First txn Entry where RefID has been used as payment reference, Shifted
            ''' </summary>
            ''' <param name="RefID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetPaymentReferenceRecord</remarks>
            Public Function GetPaymentReferenceRecord(ByVal RefID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetPaymentReferenceRecord, ClientScreen.Accounts_Vouchers, RefID)
            End Function

            ''' <summary>
            ''' Gets ItemId of Items Selected in donation by gift entry,Shifted
            ''' </summary>
            ''' <param name="RefID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetAssetItemID</remarks>
            Public Function GetAssetItemID(ByVal RefID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetAssetItemID, ClientScreen.Accounts_Vouchers, RefID)
            End Function

            ''' <summary>
            ''' Gets RecID of the Asset from TxnMID, Shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetAssetRecID</remarks>
            Public Function GetAssetRecID(ByVal TableName As Tables, ByVal TxnMID As String, Optional TxnSrNo As Integer = 0) As String
                Dim Param As Param_Vouchers_GetAssetRecID = New Param_Vouchers_GetAssetRecID()
                Param.TableName = TableName
                Param.TxnMID = TxnMID
                If TxnSrNo > 0 Then Param.TrSrNo = TxnSrNo
                Dim value As Object = GetSingleValue_Data(RealTimeService.RealServiceFunctions.Vouchers_GetAssetRecID, ClientScreen.Accounts_Vouchers, Param)
                If IsDBNull(value) Then
                    Return ""
                End If
                Return value.ToString()
            End Function

            ''' <summary>
            ''' Gets Record Edit On time by RecID, Shifted
            ''' </summary>
            ''' <param name="Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetEditOnByRecID</remarks>
            Public Function GetEditOnByRecID(ByVal Rec_ID As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Vouchers_GetEditOnByRecID, ClientScreen.Accounts_Vouchers, Rec_ID)
            End Function

            ''' <summary>
            ''' Gets Item Voucher Type and Item Profile, shifted
            ''' </summary>
            ''' <param name="ItemID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetItemProfileRecord(ByVal ItemID As String) As DataTable
                Dim Query As String = "SELECT ITEM_VOUCHER_TYPE, ITEM_PROFILE FROM ITEM_INFO WHERE REC_ID ='" & ItemID & "' AND REC_STATUS IN (0,1,2)"
                Dim param As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
                param.ItemIDs = ItemID
                Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Vouchers, param)
            End Function

            ''' <summary>
            ''' Gets RecID of Advance created by current Txn (MasterID),shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetRaisedAdvanceRecID(ByVal TxnMID As String) As String
                'Dim Query As String = "SELECT REC_ID FROM ADVANCES_INFO WHERE AI_TR_ID ='" & TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Dim param As Param_Advances_GetList_Common = New Param_Advances_GetList_Common()
                param.TxnMID = TxnMID
                Dim adv As Advances = New Advances(cBase)
                Dim dTable As DataTable = adv.GetList(ClientScreen.Accounts_Vouchers, param)
                If Not dTable Is Nothing Then
                    If dTable.Rows.Count > 0 Then
                        Return dTable.Rows(0)(0).ToString()
                    End If
                End If
                Return ""
            End Function

            ''' <summary>
            ''' Gets RecID of Liability created by current Txn (MasterID), shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetRaisedLiabilityRecID(ByVal TxnMID As String) As String
                'Dim Query As String = "SELECT REC_ID FROM LIABILITIES_INFO WHERE LI_TR_ID ='" & TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Dim liab As Liabilities = New Liabilities(cBase)
                Dim dTable As DataTable = liab.GetList(ClientScreen.Accounts_Vouchers, TxnMID)
                If Not dTable Is Nothing Then
                    If dTable.Rows.Count > 0 Then
                        Return dTable.Rows(0)(0).ToString()
                    End If
                End If
                Return ""
            End Function

            ''' <summary>
            ''' Gets RecID of Liability created by current Txn (MasterID), Shifted
            ''' </summary>
            ''' <param name="TxnMID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetRaisedDepositRecID(ByVal TxnMID As String) As String
                Dim Query As String = "SELECT REC_ID FROM DEPOSITS_INFO WHERE DI_TR_ID ='" & TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Dim dep As Deposits = New Deposits(cBase)
                Dim Param As Param_Deposits_GetListCommon = New Param_Deposits_GetListCommon()
                Param.TxnMID = TxnMID
                Dim dTable As DataTable = dep.GetList(ClientScreen.Accounts_Vouchers, Param)
                If Not dTable Is Nothing Then
                    If dTable.Rows.Count > 0 Then
                        Return dTable.Rows(0)(0).ToString()
                    End If
                End If
                Return ""
            End Function

            ''' <summary>
            ''' Gets Past Parties, Shifted
            ''' </summary>
            ''' <param name="FromDate"></param>
            ''' <param name="ToDate"></param>
            ''' <param name="IsInternalTransfer"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetPastParties</remarks>
            Public Function GetPastParties(ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsInternalTransfer As Boolean) As DataTable
                Dim Param As Param_Vouchers_GetPastParties = New Param_Vouchers_GetPastParties()
                Param.FromDate = FromDate
                Param.ToDate = ToDate
                Param.IsInternalTransfer = IsInternalTransfer
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetPastParties, ClientScreen.Accounts_Vouchers, Param)
            End Function

            'Shifted
            Public Function GetPastPartyDetails(ByVal AB_IDs As String) As DataTable
                Dim _AB As Addresses = New Addresses(cBase)
                Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                Param.AB_IDs = AB_IDs
                Return _AB.GetList(ClientScreen.Accounts_Vouchers, Param)
            End Function

            'Shifted
            Public Function GetLedgers() As DataTable
                Return GetLedgersList(ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetItemList(ByVal IDColumnHeadName As String, ByVal NameColumnHeadName As String) As DataTable
                Return GetItems(ClientScreen.Accounts_Vouchers, IDColumnHeadName, NameColumnHeadName)
            End Function

            'Shifted
            Public Function GetItem_LedgerList(ByVal AllowForeignDonation As Boolean, ByVal ITEM_APPLICABLE As String) As DataTable
                Dim LocalQuery As String = ""
                ' Dim OnlineQuery As String = ""
                If AllowForeignDonation Then
                    LocalQuery = "  SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                        "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION','DONATION - FOREIGN','DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTSx','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") " &
                        "     Union All " &
                        "     SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                        "     FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' "
                Else
                    LocalQuery = "  SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                        "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION'                     ,'DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTSx','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") " &
                        "     Union All " &
                        "     SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                        "     FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' "
                End If

                'Type = DUALQUERY
                Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
                OnlineParam.ItemApplicable = ITEM_APPLICABLE
                OnlineParam.open_Ins_ID = cBase._open_Ins_ID
                OnlineParam.AllowForeignDonation = AllowForeignDonation
                OnlineParam.Type = "DUALQUERY"
                Return GetItems_Ledger(ClientScreen.Accounts_Vouchers, LocalQuery, OnlineParam)
            End Function

            'Shifted
            Public Function GetItem_LedgerListMain(ByVal AllowForeignDonation As Boolean, ByVal AllowMembership As Boolean, ByVal ITEM_APPLICABLE As String) As DataTable
                Dim LocalQuery As String = "" : Dim OnlineQuery As String = "" : Dim ForeignDonationVoucher As String = "" : Dim MembershipVoucher As String = "" : Dim MembershipRenewalVoucher As String = ""
                If AllowForeignDonation Then ForeignDonationVoucher = "DONATION - FOREIGN" Else ForeignDonationVoucher = ""
                If AllowMembership Then MembershipVoucher = "MEMBERSHIP" : MembershipRenewalVoucher = "MEMBERSHIP RENEWAL" Else MembershipVoucher = "" : MembershipRenewalVoucher = ""
                LocalQuery = "  SELECT DISTINCT I.ITEM_NAME ,L.LED_NAME,I.ITEM_PARTY_REQ,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                    "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID " &
                    "  WHERE I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION','" & ForeignDonationVoucher & "','DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTS','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.','FD','LAND & BUILDING','LAND & BUILDING / Gift','SALE OF ASSET','" & MembershipVoucher & "','" & MembershipRenewalVoucher & "') " &
                    "    AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") " &
                    "  Union All     " &
                    "  SELECT DISTINCT I.ITEM_NAME ,L.LED_NAME,I.ITEM_PARTY_REQ,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                    "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID " &
                    "  WHERE I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') " &
                    "    AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ")  AND ITEM_INS_ID <> '" & cBase._open_Ins_ID & "' "

                'Type = SINGLEQUERY
                Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
                OnlineParam.ItemApplicable = ITEM_APPLICABLE
                OnlineParam.ForeignDonationVoucher = ForeignDonationVoucher
                OnlineParam.open_Ins_ID = cBase._open_Ins_ID
                OnlineParam.MembershipRenewalVoucher = MembershipRenewalVoucher
                OnlineParam.MembershipVoucher = MembershipVoucher
                OnlineParam.Type = "SINGLEQUERY"
                Return GetItems_Ledger(ClientScreen.Accounts_Vouchers, LocalQuery, OnlineParam)
            End Function

            'Shifted
            Public Function GetSavingAccounts() As DataTable
                Return GetSavingAccountsList(ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetSavingAccountList() As DataTable
                Dim _BA As BankAccounts = New BankAccounts(cBase)
                Return _BA.GetList(ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
                Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Vouchers)
            End Function

            'Shifted
            Public Function GetBranchShortDetails(ByVal Branch_IDs As String) As DataTable
                Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Vouchers, " B.BI_SHORT_NAME, A.REC_ID AS BB_BRANCH_ID ")
            End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="ToDate"></param>
        ''' <param name="Online_Bank_Col_TR_Rec"></param>
        ''' <param name="Online_Bank_Col_NB_Rec"></param>
        ''' <param name="Local_Bank_Col_TR_Rec"></param>
        ''' <param name="Local_Bank_Col_NB_Rec"></param>
        ''' <param name="Online_Bank_Col_TR_Pay"></param>
        ''' <param name="Online_Bank_Col_NB_Pay"></param>
        ''' <param name="Local_Bank_Col_TR_Pay"></param>
        ''' <param name="Local_Bank_Col_NB_Pay"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetListWithMultipleParams</remarks>
        Public Function GetList(ByVal FromDate As Date, ByVal ToDate As Date,
             ByVal Online_Bank_Col_TR_Rec As String, ByVal Online_Bank_Col_NB_Rec As String, ByVal Local_Bank_Col_TR_Rec As String, ByVal Local_Bank_Col_NB_Rec As String,
           ByVal Online_Bank_Col_TR_Pay As String, ByVal Online_Bank_Col_NB_Pay As String, ByVal Local_Bank_Col_TR_Pay As String, ByVal Local_Bank_Col_NB_Pay As String, Optional Advanced_Filter_Category As String = "", Optional Advanced_Filter_Ref_ID As String = "", Optional showDynamicBankColumns As Boolean = False)
            Dim Param As Param_Vouchers_GetListWithMultipleParams = New Param_Vouchers_GetListWithMultipleParams()
            Param.FromDate = FromDate
            Param.Local_Bank_Col_NB_Pay = Local_Bank_Col_NB_Pay
            Param.Local_Bank_Col_NB_Rec = Local_Bank_Col_NB_Rec
            Param.Local_Bank_Col_TR_Pay = Local_Bank_Col_TR_Pay
            Param.Local_Bank_Col_TR_Rec = Local_Bank_Col_TR_Rec
            Param.Online_Bank_Col_NB_Pay = Online_Bank_Col_NB_Pay
            Param.Online_Bank_Col_NB_Rec = Online_Bank_Col_NB_Rec
            Param.Online_Bank_Col_TR_Pay = Online_Bank_Col_TR_Pay
            Param.Online_Bank_Col_TR_Rec = Online_Bank_Col_TR_Rec
            Param.openYearEdt = cBase._open_Year_Edt
            Param.openYearSdt = cBase._open_Year_Sdt
            Param.ToDate = ToDate
            Param.Advanced_Filter_Category = Advanced_Filter_Category
            Param.Advanced_Filter_Ref_ID = Advanced_Filter_Ref_ID
            Param.showDynamicBankColumns = showDynamicBankColumns
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetListWithMultipleParams, ClientScreen.Accounts_Vouchers, Param)
        End Function


        Public Function GetBank_Reconciliation(ByVal ReconcileDate As Date, ByVal BankAccID As String)
                Dim inParam As Param_GetBank_Reconciliation = New Param_GetBank_Reconciliation
                inParam.BankID = BankAccID
                inParam.DateofReconcile = ReconcileDate
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetBank_Reconciliation, ClientScreen.Accounts_Vouchers, inParam)
            End Function


            Public Function GetListBySP(ByVal FromDate As Date, ByVal ToDate As Date, INS_ID As String, Open_Year_Edt As Date, Open_Year_Sdt As Date, Optional Advanced_Filter_Category As String = "", Optional Advanced_Filter_Ref_ID As String = "")
                Dim Param As Param_Vouchers_GetListWithMultipleParams_SP = New Param_Vouchers_GetListWithMultipleParams_SP()
                Param.FromDate = FromDate
                Param.ToDate = ToDate
                Param.INS_ID = INS_ID
                Param.OpenYearSdt = Open_Year_Edt
                Param.OpenYearEdt = Open_Year_Sdt
                Param.Advanced_Filter_Category = Advanced_Filter_Category
                Param.Advanced_Filter_Ref_ID = Advanced_Filter_Ref_ID

                Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.Vouchers_GetListWithMultipleParams_SP, ClientScreen.Accounts_Vouchers, Param)
            End Function

            'Public Function GetList_SQL(ByVal FromDate As Date, ByVal ToDate As Date, _
            '    ByVal Online_Bank_Col_TR_Rec As String, ByVal Online_Bank_Col_NB_Rec As String, ByVal Local_Bank_Col_TR_Rec As String, ByVal Local_Bank_Col_NB_Rec As String, _
            '  ByVal Online_Bank_Col_TR_Pay As String, ByVal Online_Bank_Col_NB_Pay As String, ByVal Local_Bank_Col_TR_Pay As String, ByVal Local_Bank_Col_NB_Pay As String)

            '    'Dim OnlineQuery As String = " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  MAX(CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END) AS iLED_ID,'' AS iTR_HEAD, MAX(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME, " & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL, " & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(cBase._open_Year_Sdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(cBase._open_Year_Edt) & "-' +dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01'  END) AS iTR_DATE_SHOW, " & _
            '    '                            " MIN(CASE WHEN TR_CODE IN (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE IN (1,2,10) THEN 'B'ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE 'C'END END END END END) AS iTR_ENTRY, " & _
            '    '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH   , " & _
            '    '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_BANK   , " & Online_Bank_Col_TR_Rec & _
            '    '                            " SUM(CASE WHEN LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_REC_JOURNAL, " & _
            '    '                            " SUM(CASE WHEN TR_DR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_REC_TOTAL  , " & _
            '    '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH   , " & _
            '    '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_BANK   , " & Online_Bank_Col_TR_Pay & _
            '    '                            " SUM(CASE WHEN LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_PAY_JOURNAL, " & _
            '    '                            " SUM(CASE WHEN TR_CR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_PAY_TOTAL  , " & _
            '    '                            " MAX(TR_NARRATION) AS iTR_NARRATION,'' AS DON_STATUS,'B' AS iTR_ROW_POS,MAX(TR_CODE) AS iTR_CODE,MAX(REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) AS iTR_SR_NO,MIN(CASE WHEN TR_CODE IN (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE IN (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B'END END) AS iTR_SORT, MAX(REC_ADD_ON) AS iREC_ADD_ON, " & _
            '    '                            " MAX(CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " & _
            '    '                            " CASE COALESCE(MAX(Transaction_Info.REC_STATUS),99) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' END AS iACTION_STATUS, MAX(REC_EDIT_ON) AS iREC_EDIT_ON, MAX(REC_ADD_BY) AS iREC_ADD_BY, MAX(REC_EDIT_BY) AS iREC_EDIT_BY,MAX(TR_TRF_CROSS_REF_ID) AS iCross_Ref_ID, " & _
            '    '                            cBase.Remarks_Detail_Txn("Transaction_Info", True, GetCurrentDateTime(ClientScreen.Accounts_Vouchers)) & _
            '    '                            " FROM  Transaction_Info WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_NOTEBOOK IS NULL " & _
            '    '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(FromDate, cBase._Server_Date_Format_Short) & "' AND '" & Format(ToDate, cBase._Server_Date_Format_Short) & "' " & _
            '    '                            " GROUP BY CASE WHEN TR_M_ID IS NULL THEN REC_ID ELSE TR_M_ID END,  TR_ITEM_ID,  TR_SR_NO   " & _
            '    '                            " " & _
            '    '                            " UNION ALL " & _
            '    '                            " " & _
            '    '                            " SELECT '',DATEADD(DD,1,DATEADD(MM,1,DATEADD(DD,(-1* (DAY(MAX(tr_date))-1)) , MAX(tr_date)))), TR_ITEM_ID AS iTR_ITEM_ID,'' AS iTR_ITEM,MAX(CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END) AS iLED_ID,'' AS iTR_HEAD,NULL,NULL,'' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL, " & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(cBase._open_Year_Sdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(cBase._open_Year_Edt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END ) AS iTR_DATE_SHOW," & _
            '    '                            " 'C'             AS iTR_ENTRY      ," & _
            '    '                            " NULL            AS iTR_REC_CASH   ," & _
            '    '                            " NULL            AS iTR_REC_BANK   ," & Online_Bank_Col_NB_Rec & _
            '    '                            " NULL            AS iTR_REC_JOURNAL," & _
            '    '                            " NULL            AS iTR_REC_TOTAL  ," & _
            '    '                            " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," & _
            '    '                            " NULL            AS iTR_PAY_BANK   ," & Online_Bank_Col_NB_Pay & _
            '    '                            " NULL            AS iTR_PAY_JOURNAL," & _
            '    '                            " SUM(TR_AMOUNT)  AS iTR_PAY_TOTAL  ," & _
            '    '                            " 'Note-Book Entry' AS iTR_NARRATION, NULL AS DON_STATUS, 'C' AS iTR_ROW_POS, 3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL AS iTR_SR_NO,  NULL AS iTR_SORT,  MAX(REC_ADD_ON) AS iREC_ADD_ON," & _
            '    '                            " MAX( CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO , " & _
            '    '                            " CASE COALESCE(MAX(Transaction_Info.REC_STATUS),99) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' END AS iACTION_STATUS, MAX(REC_EDIT_ON) AS iREC_EDIT_ON, MAX(REC_ADD_BY) AS iREC_ADD_BY, MAX(REC_EDIT_BY) AS iREC_EDIT_BY,MAX(TR_TRF_CROSS_REF_ID) AS iCross_Ref_ID, " & _
            '    '                            cBase.Remarks_Detail_Txn("Transaction_Info", True, GetCurrentDateTime(ClientScreen.Accounts_Vouchers)) & _
            '    '                            " FROM    Transaction_Info  " & _
            '    '                            " WHERE REC_STATUS IN (0,1,2)  AND TR_CEN_ID = '" & cBase._open_Cen_ID & "'  AND TR_NOTEBOOK = 'YES'  " & _
            '    '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(FromDate, cBase._Server_Date_Format_Short) & "' AND '" & Format(ToDate, cBase._Server_Date_Format_Short) & "' " & _
            '    '                            " GROUP BY dbo.fn_FORMATDATE(tr_date,'yyyymm'),    TR_ITEM_ID  "

            '    'Dim LocalQuery As String = " SELECT Max(TR_VNO) as [iTR_VNO], Max(TR_DATE) as [iTR_DATE], Max(TR_ITEM_ID) as [iTR_ITEM_ID], '' as [iTR_ITEM] , Max(IIF(TR_TYPE='DEBIT',TR_DR_LED_ID,TR_CR_LED_ID)) AS [iLED_ID],'' as [iTR_HEAD],Max(IIF(TR_TYPE='DEBIT',TR_SUB_DR_LED_ID,TR_SUB_CR_LED_ID))  AS [iTR_SUB_ID],MAX(TR_AB_ID_1) AS [iTR_AB_ID_1],'' AS [iTR_PARTY_1],MAX(IIF( LEN(IIF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '    '                           " MAX(IIF( MONTH(TR_DATE)=4 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",1,IIF( MONTH(TR_DATE)=5 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",2,IIF( MONTH(TR_DATE)=6 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",3,IIF( MONTH(TR_DATE)=7 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",4,IIF( MONTH(TR_DATE)=8 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",5,IIF( MONTH(TR_DATE)=9 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",6,IIF( MONTH(TR_DATE)=10 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",7,IIF( MONTH(TR_DATE)=11 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",8,IIF( MONTH(TR_DATE)=12 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",9,IIF( MONTH(TR_DATE)=1 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",10,IIF( MONTH(TR_DATE)=2 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",11,IIF( MONTH(TR_DATE)=3 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",12,0))))))))))))) AS iTR_DATE_SERIAL," & _
            '    '                           " MAX(IIF( MONTH(TR_DATE)=4 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-04-01',IIF( MONTH(TR_DATE)=5 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-05-01',IIF( MONTH(TR_DATE)=6 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-06-01',IIF( MONTH(TR_DATE)=7 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-07-01',IIF( MONTH(TR_DATE)=8 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-08-01',IIF( MONTH(TR_DATE)=9 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-09-01',IIF( MONTH(TR_DATE)=10 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-10-01',IIF( MONTH(TR_DATE)=11 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-11-01',IIF( MONTH(TR_DATE)=12 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-12-01',IIF( MONTH(TR_DATE)=1 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-01-01',IIF( MONTH(TR_DATE)=2 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-02-01',IIF( MONTH(TR_DATE)=3 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-03-01',0)))))))))))))   AS iTR_DATE_SHOW, " & _
            '    '                           " MIN( If(TR_CODE In (4,5,6,9,11) ,'A', if(TR_CODE = 8 and TR_TYPE='CREDIT','A', if(TR_CODE = 8 and TR_TYPE='DEBIT','C', If(TR_CODE In (1,2,10), 'B', If(TR_CODE=7,'D','C')))))) AS iTR_ENTRY," & _
            '    '                           " Sum( IIF(TR_DR_LED_ID='00080',TR_AMOUNT,NULL) )                                                                       AS [iTR_REC_CASH]    ," & _
            '    '                           " Sum( IIF(TR_DR_LED_ID='00079',TR_AMOUNT,NULL) )                                                                       AS [iTR_REC_BANK]    ," & Local_Bank_Col_TR_Rec & _
            '    '                           " Sum( IIF( LEN(IIF(ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID) )=0  ,TR_AMOUNT,NULL) )                                       AS [iTR_REC_JOURNAL] ," & _
            '    '                           " Sum( IIF(TR_DR_LED_ID IN ('00079','00080') OR LEN(IIF(ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID) )=0 ,TR_AMOUNT,NULL) )    AS [iTR_REC_TOTAL]   ," & _
            '    '                           " Sum( IIF(TR_CR_LED_ID='00080',TR_AMOUNT,NULL) )                                                                       AS [iTR_PAY_CASH]    ," & _
            '    '                           " Sum( IIF(TR_CR_LED_ID='00079',TR_AMOUNT,NULL) )                                                                       AS [iTR_PAY_BANK]    ," & Local_Bank_Col_TR_Pay & _
            '    '                           " Sum( IIF( LEN(IIF(ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID) )=0  ,TR_AMOUNT,NULL) )                                       AS [iTR_PAY_JOURNAL] ," & _
            '    '                           " Sum( IIF(TR_CR_LED_ID IN ('00079','00080') OR LEN(IIF(ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID) )=0 ,TR_AMOUNT,NULL) )    AS [iTR_PAY_TOTAL]   ," & _
            '    '                           " Max( TR_NARRATION ) as [iTR_NARRATION],NULL AS [DON_STATUS], 'B' AS [iTR_ROW_POS], Max(TR_CODE)  as [iTR_CODE], Max(REC_ID) as [iREC_ID], Max(TR_M_ID) as [iTR_M_ID],MAX(TR_SR_NO) as [iTR_SR_NO],MIN(IIf(TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' ,'A',IIf(TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT','A','B'))) as [iTR_SORT],MAX(REC_ADD_ON) AS [iREC_ADD_ON],TR_TRF_CROSS_REF_ID as iCross_Ref_ID," & _
            '    '                           " MAX( IIF( LEN(IIF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO " & _
            '    '                           " FROM Transaction_Info " & _
            '    '                           " Where   REC_STATUS IN (0,1,2) AND TR_CEN_ID='" & cBase._open_Cen_ID & "'  AND TR_NOTEBOOK IS NULL " & _
            '    '                           " AND TR_DATE BETWEEN #" & Format(FromDate, cBase._Date_Format_Short) & "# AND #" & Format(ToDate, cBase._Date_Format_Short) & "# " & _
            '    '                           " GROUP BY IIF( ISNULL(TR_M_ID),REC_ID,TR_M_ID),TR_ITEM_ID, TR_SR_NO " & _
            '    '                           " " & _
            '    '                           " UNION ALL " & _
            '    '                           " " & _
            '    '                           " SELECT '',DATEADD('D',-1,DateSerial(MID(FORMAT(TR_DATE,'yyyyMM'),1,4), MID(FORMAT(TR_DATE,'yyyyMM'),5,2) + 1, 1)),TR_ITEM_ID AS [iTR_ITEM_ID],'' as iTR_ITEM ,Max(IIF(TR_TYPE='DEBIT',TR_DR_LED_ID,TR_CR_LED_ID)) AS [iLED_ID],'' as [iTR_HEAD] ,NULL,NULL,'' AS [iTR_PARTY_1],MAX(IIF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '    '                           " MAX(IIF( MONTH(TR_DATE)=4 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",1,IIF( MONTH(TR_DATE)=5 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",2,IIF( MONTH(TR_DATE)=6 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",3,IIF( MONTH(TR_DATE)=7 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",4,IIF( MONTH(TR_DATE)=8 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",5,IIF( MONTH(TR_DATE)=9 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",6,IIF( MONTH(TR_DATE)=10 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",7,IIF( MONTH(TR_DATE)=11 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",8,IIF( MONTH(TR_DATE)=12 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",9,IIF( MONTH(TR_DATE)=1 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",10,IIF( MONTH(TR_DATE)=2 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",11,IIF( MONTH(TR_DATE)=3 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",12,0))))))))))))) AS iTR_DATE_SERIAL," & _
            '    '                           " MAX(IIF( MONTH(TR_DATE)=4 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-04-01',IIF( MONTH(TR_DATE)=5 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-05-01',IIF( MONTH(TR_DATE)=6 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-06-01',IIF( MONTH(TR_DATE)=7 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-07-01',IIF( MONTH(TR_DATE)=8 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-08-01',IIF( MONTH(TR_DATE)=9 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-09-01',IIF( MONTH(TR_DATE)=10 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-10-01',IIF( MONTH(TR_DATE)=11 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-11-01',IIF( MONTH(TR_DATE)=12 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Sdt) & ",'" & Year(cBase._open_Year_Sdt) & "-12-01',IIF( MONTH(TR_DATE)=1 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-01-01',IIF( MONTH(TR_DATE)=2 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-02-01',IIF( MONTH(TR_DATE)=3 AND YEAR(TR_DATE)=" & Year(cBase._open_Year_Edt) & ",'" & Year(cBase._open_Year_Edt) & "-03-01',0)))))))))))))   AS iTR_DATE_SHOW, " & _
            '    '                           " 'C'             AS iTR_ENTRY      ," & _
            '    '                           " NULL            AS iTR_REC_CASH   ," & _
            '    '                           " NULL            AS iTR_REC_BANK   ," & Local_Bank_Col_NB_Rec & _
            '    '                           " NULL            AS iTR_REC_JOURNAL," & _
            '    '                           " NULL            AS iTR_REC_TOTAL  ," & _
            '    '                           " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," & _
            '    '                           " NULL            AS iTR_PAY_BANK   ," & Local_Bank_Col_NB_Pay & _
            '    '                           " NULL            AS iTR_PAY_JOURNAL," & _
            '    '                           " SUM(TR_AMOUNT)  AS iTR_PAY_TOTAL  ," & _
            '    '                           " 'Note-Book Entry' as [iTR_NARRATION],NULL AS [DON_STATUS],'C' AS iTR_ROW_POS,3 as [iTR_CODE], 'NOTE-BOOK' AS iREC_ID ,NULL AS iTR_M_ID, NULL as iTR_SR_NO,NULL as iTR_SORT,MAX(REC_ADD_ON) AS [iREC_ADD_ON], TR_TRF_CROSS_REF_ID as iCross_Ref_ID," & _
            '    '                           " MAX( IIF( LEN(IIF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO " & _
            '    '                           " FROM Transaction_Info " & _
            '    '                           " Where REC_STATUS IN (0,1,2) AND TR_CEN_ID='" & cBase._open_Cen_ID & "'  AND TR_NOTEBOOK ='YES' " & _
            '    '                           " AND TR_DATE BETWEEN #" & Format(FromDate, cBase._Date_Format_Short) & "# AND #" & Format(ToDate, cBase._Date_Format_Short) & "# " & _
            '    '                           " GROUP BY  FORMAT(TR_DATE,'yyyyMM'),TR_ITEM_ID"
            '    'Return GetListOfRecords(OnlineQuery, LocalQuery, ClientScreen.Accounts_Vouchers, Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
            '    Dim Param As Param_Vouchers_GetList_SQL = New Param_Vouchers_GetList_SQL()
            '    Param.FromDate = FromDate
            '    Param.Local_Bank_Col_NB_Pay = Local_Bank_Col_NB_Pay
            '    Param.Local_Bank_Col_NB_Rec = Local_Bank_Col_NB_Rec
            '    Param.Local_Bank_Col_TR_Pay = Local_Bank_Col_TR_Pay
            '    Param.Local_Bank_Col_TR_Rec = Local_Bank_Col_TR_Rec
            '    Param.Online_Bank_Col_NB_Pay = Online_Bank_Col_NB_Pay
            '    Param.Online_Bank_Col_NB_Rec = Online_Bank_Col_NB_Rec
            '    Param.Online_Bank_Col_TR_Pay = Online_Bank_Col_TR_Pay
            '    Param.Online_Bank_Col_TR_Rec = Online_Bank_Col_TR_Rec
            '    Param.openYearEdt = cBase._open_Year_Edt
            '    Param.openYearSdt = cBase._open_Year_Sdt
            '    Param.ToDate = ToDate
            '    Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetList_SQL, ClientScreen.Accounts_Vouchers, Param)
            'End Function

            ''' <summary>
            ''' Get List, Shifted
            ''' </summary>
            ''' <param name="FromDate"></param>
            ''' <param name="ToDate"></param>
            ''' <param name="Led_ID"></param>
            ''' <param name="Bank_Acc_ID"></param>
            ''' <param name="OtherCondition"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetList</remarks>
            Public Function GetList(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Led_ID As String, ByVal Bank_Acc_ID As String, ByVal OtherCondition As String)
                Dim Param As Param_Vouchers_GetList = New Param_Vouchers_GetList()
                Param.ToDate = ToDate
                Param.OtherCondition = OtherCondition
                Param.openYearSdt = cBase._open_Year_Sdt
                Param.openYearEdt = cBase._open_Year_Edt
                Param.Led_ID = Led_ID
                Param.FromDate = FromDate
                Param.Bank_Acc_ID = Bank_Acc_ID
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetList, ClientScreen.Accounts_Vouchers, Param)
            End Function

            'Shifted
            'Public Function GetList_SQL(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Led_ID As String, ByVal Bank_Acc_ID As String, ByVal OtherCondition As String)

            '    'Dim OnlineQuery As String = " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(cBase._open_Year_Sdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(cBase._open_Year_Edt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '    '                            " MIN( CASE WHEN TR_CODE In (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE In (1,2,10) THEN 'B' ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE 'C' END END END END END) AS iTR_ENTRY," & _
            '    '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH ,  " & _
            '    '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_REC_BANK ," & _
            '    '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH ," & _
            '    '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_PAY_BANK ," & _
            '    '                            " 'B' AS iTR_ROW_POS,MAX(TR_CODE) AS iTR_CODE,MAX(REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) as iTR_SR_NO,MIN(CASE WHEN TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B' END END) as iTR_SORT, MAX(REC_ADD_ON) AS iREC_ADD_ON," & _
            '    '                            " MAX( CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO  " & _
            '    '                            " FROM  Transaction_Info WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_NOTEBOOK IS NULL " & _
            '    '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(FromDate, cBase._Server_Date_Format_Short) & "' AND '" & Format(ToDate, cBase._Server_Date_Format_Short) & "' " & _
            '    '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & OtherCondition & _
            '    '                            " GROUP BY COALESCE(TR_M_ID, REC_ID),  TR_ITEM_ID,  TR_SR_NO " & _
            '    '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Led_ID & "' THEN TR_AMOUNT ELSE 0 END ) <> 0 )" & _
            '    '                            " " & _
            '    '                            " UNION ALL " & _
            '    '                            " " & _
            '    '                            " SELECT '',DATEADD(DD,-1,DATEADD(MM,1,DATEADD(DD,-1*(DAY(MAX(tr_date))-1), MAX(tr_date)))), TR_ITEM_ID AS iTR_ITEM_ID,'' AS iTR_ITEM, Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,NULL,'' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME, " & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '    '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(cBase._open_Year_Sdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01'  ELSE '" & Year(cBase._open_Year_Edt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '    '                            " 'C'             AS iTR_ENTRY      ," & _
            '    '                            " NULL            AS iTR_REC_CASH   ," & _
            '    '                            " NULL            AS iTR_REC_BANK   ," & _
            '    '                            " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," & _
            '    '                            " NULL            AS iTR_PAY_BANK   ," & _
            '    '                            " 'C' AS iTR_ROW_POS,3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL as iTR_SR_NO,  NULL as iTR_SORT,  MAX(REC_ADD_ON) AS iREC_ADD_ON,   " & _
            '    '                            "  MAX( CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO " & _
            '    '                            " FROM    Transaction_Info  " & _
            '    '                            " WHERE REC_STATUS IN (0,1,2)  AND TR_CEN_ID = '" & cBase._open_Cen_ID & "'  AND TR_NOTEBOOK = 'YES'  " & _
            '    '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(FromDate, cBase._Server_Date_Format_Short) & "' AND '" & Format(ToDate, cBase._Server_Date_Format_Short) & "' " & _
            '    '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & OtherCondition & _
            '    '                            " GROUP BY dbo.fn_FORMATDATE(tr_date,'yyyymm'),    TR_ITEM_ID" & _
            '    '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 )"

            '    'Return GetListOfRecords(OnlineQuery, OnlineQuery, ClientScreen.Accounts_Vouchers, Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)

            'End Function

            ''' <summary>
            ''' Get Negative Balance, Shifted
            ''' </summary>
            ''' <param name="Led_ID"></param>
            ''' <param name="Bank_Acc_ID"></param>
            ''' <param name="OtherCondition"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetNegativeBalance</remarks>
            Public Function GetNegativeBalance(ByVal Led_ID As String, ByVal Bank_Acc_ID As String, ByVal OtherCondition As String)
                Dim Param As Param_Vouchers_GetNegativeBalance = New Param_Vouchers_GetNegativeBalance()
                Param.Bank_Acc_ID = Bank_Acc_ID
                Param.Led_ID = Led_ID
                Param.OtherCondition = OtherCondition
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetNegativeBalance, ClientScreen.Accounts_Vouchers, Param)
            End Function

            ''' <summary>
            ''' Get Donation Status, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetDonationStatus</remarks>
            Public Function GetDonationStatus() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetDonationStatus, ClientScreen.Accounts_Vouchers)
            End Function

            ''' <summary>
            ''' Get Donation Status With RecId filter, Shifted
            ''' </summary>
            ''' <param name="RecID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetDonationStatusWithRecID</remarks>
            Public Function GetDonationStatus(ByVal RecID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetDonationStatusWithRecID, ClientScreen.Accounts_Vouchers, RecID)
            End Function

            ''' <summary>
            ''' Get Status TrCode, Shifted
            ''' </summary>
            ''' <param name="RecID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetStatus_TrCode</remarks>
            Public Function GetStatus_TrCode(ByVal RecID As String, Optional ByVal MID As String = "") As DataTable
                Dim Param As Param_GetStatusTrCode = New Param_GetStatusTrCode
                Param.Tr_RecID = RecID
                Param.Tr_MID = MID
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetStatus_TrCode, ClientScreen.Accounts_Vouchers, Param)
            End Function

            ''' <summary>
            ''' GetStatus_TrCode_OtherCentre, Shifted
            ''' </summary>
            ''' <param name="RecID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetStatus_TrCode_OtherCentre</remarks>
            Public Function GetStatus_TrCode_OtherCentre(ByVal RecID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetStatus_TrCode_OtherCentre, ClientScreen.Accounts_Vouchers, RecID)
            End Function

            ''' <summary>
            ''' Get Transaction Detail, Shifted
            ''' </summary>
            ''' <param name="RecID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Vouchers_GetTransactionDetail</remarks>
            Public Function GetTransactionDetail(ByVal RecID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetTransactionDetail, ClientScreen.Accounts_Vouchers, RecID)
            End Function

            'Shifted
            Public Function GetBankAccount(ByVal CR_LED_ID As String, ByVal DR_LED_ID As String) As Object
                Dim _BA As BankAccounts = New BankAccounts(cBase)
                Dim OnlineParam As Param_Bank_GetValue_Common = New Param_Bank_GetValue_Common()
                OnlineParam.CR_LED_ID = CR_LED_ID
                OnlineParam.DR_LED_ID = DR_LED_ID
                Return _BA.GetValue(ClientScreen.Accounts_Vouchers, OnlineParam)
            End Function

            Public Function GetBankAccountsList() As DataTable
                Dim _BA As BankAccounts = New BankAccounts(cBase)
                Return _BA.GetList(ClientScreen.Accounts_Vouchers)
            End Function

            Public Function GetAdvancedFilters(Param As Param_GetAdvancedFilters) As DataTable
                Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Vouchers_GetAdvancedFilters, ClientScreen.Accounts_Vouchers, Param)
            End Function

        Public Function GetSlipMAsterRecord(ByVal Rec_ID As String) As List(Of Return_GetSlipMasterRecord)
            Dim _retTable As DataTable = GetRecordByColumn("REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_CashBank, RealTimeService.Tables.SLIP_INFO)
            Dim data As List(Of Return_GetSlipMasterRecord) = New List(Of Return_GetSlipMasterRecord)()
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata = New Return_GetSlipMasterRecord
                    newdata.SL_CEN_ID = row.Field(Of Integer?)("SL_CEN_ID")
                    newdata.SL_COD_YEAR_ID = row.Field(Of Integer?)("SL_COD_YEAR_ID")
                    newdata.SL_PRINT_DATE = row.Field(Of Date?)("SL_PRINT_DATE")
                    newdata.SL_NO = row.Field(Of Integer?)("SL_NO")
                    newdata.REC_ADD_ON = row.Field(Of Date?)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_STATUS = row.Field(Of Integer?)("REC_STATUS")
                    newdata.SL_BA_REC_ID = row.Field(Of String)("SL_BA_REC_ID")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.REC_ID = row.Field(Of String)("REC_ID")
                    data.Add(newdata)
                Next
            End If
            Return data
        End Function

        Public Function GetTDS_Mapping(ByVal Tr_M_ID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetTDS_Mapping, ClientScreen.Accounts_Vouchers, Tr_M_ID)
            End Function

            Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
                Return MyBase.MarkAsComplete(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Vouchers)
            End Function

            Public Overloads Function MarkAsCompleteByMasterID(ByVal Rec_Id As String) As Boolean
                Return MyBase.MarkAsComplete("Tr_M_Id", Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Vouchers)
            End Function

            Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
                Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Vouchers)
                Return Locked
            End Function

            Public Overloads Function MarkAsLockedByMasterID(ByVal Txn_Master_Id As String) As Boolean
                Dim Locked As Boolean = MyBase.MarkAsLocked("Tr_M_Id", Txn_Master_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Vouchers)
                Return Locked
            End Function

            Public Function GetBankClearingData(MasterID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetBankClearingData, ClientScreen.Accounts_Vouchers, MasterID)
            End Function

            Public Function GetBankEntriesCountInNextEvent() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetBankEntriesCountInNextEvent, ClientScreen.Accounts_Vouchers)
            End Function

            Public Function GetDraftEntryList() As DataTable
                Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Vouchers_GetDraftEntryList, ClientScreen.Accounts_DraftEntryRegister)
            End Function

            Public Function GetDraftEntryCount() As Integer
                Return GetScalarBySP(RealServiceFunctions.Vouchers_GetDraftEntryCount, ClientScreen.Accounts_DraftEntryRegister, Nothing)
            End Function

            Public Function ConfirmDraftEntry(TXN_REC_ID As String) As Boolean
                Return ExecuteGroup(RealServiceFunctions.Vouchers_ConfirmDraftEntry, ClientScreen.Accounts_DraftEntryRegister, TXN_REC_ID)
            End Function

            Public Function DeleteDraftEntry(TXN_REC_ID As String) As Boolean
                Return ExecuteGroup(RealServiceFunctions.Vouchers_DeleteDraftEntry, ClientScreen.Accounts_DraftEntryRegister, TXN_REC_ID)
            End Function

            Public Function RejectDraftEntry(inparam As Param_RejectDraftEntry) As Boolean
                Return ExecuteGroup(RealServiceFunctions.Vouchers_RejectDraftEntry, ClientScreen.Accounts_DraftEntryRegister, inparam)
            End Function

        Public Function GetTxnRecord(TXN_REC_ID As String) As DataTable
            Return GetRecordByID(TXN_REC_ID, ClientScreen.Accounts_DraftEntryRegister, Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        End Function
        Public Function GetEntryDetails(Rec_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetEntryDetails, ClientScreen.Accounts_Vouchers, Rec_ID)
        End Function
        Public Function GetProfileEntryDetails(ProfileRecID As String, TR_M_ID As String, Profile_Used As RealTimeService.AssetProfiles) As DataTable
            Dim inParam As New Param_GetProfileEntryDetails()
            inParam.ProfileRecID = ProfileRecID
            inParam.TR_M_ID = TR_M_ID
            inParam.Profile_Used = Profile_Used
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetProfileEntryDetails, ClientScreen.Accounts_Vouchers, inParam)
        End Function
        Public Function GetVoucherItemDetails(ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetVoucherItemDetails, ClientScreen.Accounts_Vouchers, ID)
        End Function
        Public Function UpdateCommonDetails(inparam As Param_UpdateCommonDetails_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Vouchers_UpdateCommonDetails, ClientScreen.Accounts_Vouchers, inparam)
        End Function

#Region "BLL Functions"
        Public Function CheckPaymentEditDetails(ByVal Rec_Id As String) As String
            Return CheckPayDetails(Rec_Id)
        End Function

        Public Function GetPaymentDetails(ByVal Rec_Id As String) As Common_Lib.RealTimeService.Param_PaymentData
                Return GetPayDetails(Rec_Id)
            End Function

        Public Function GetPaymentSaveChecks(ByVal PaymentVoucherDetails As Voucher_Payment.Param_paymentVoucherDetails) As Voucher_Payment.Param_SaveButtonChecks
            Return GetPaySaveChecks(PaymentVoucherDetails)
        End Function

        ''' <summary>
        ''' Get applicable Special Voucher Referece List to the logged in centre from HO Centre_Task_Info table, eg.: FCRA
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CashWithdrawDeposit_GetSplVoucherRefs</remarks>
        Public Function GetSplVoucherRefsList(screenName As ClientScreen, Optional ActionMethod As Common.Navigation_Mode = Common.Navigation_Mode._New) As List(Of Return_SplVchrRefsList)
            Dim SplvRefs As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetSplVoucherRefsFromCenterTasks, screenName)
            Dim _User_data As List(Of Return_SplVchrRefsList) = New List(Of Return_SplVchrRefsList)
            If (Not (SplvRefs) Is Nothing) Then
                For Each row As DataRow In SplvRefs.Rows
                    If (row.Field(Of String)("PERMISSION") = "F" Or row.Field(Of String)("PERMISSION") = "V") Then
                        Dim newdata = New Return_SplVchrRefsList
                        newdata.PERMISSION = row.Field(Of String)("PERMISSION")
                        newdata.ID = row.Field(Of String)("REC_ID")
                        newdata.Task_Name = row.Field(Of String)("TASK_NAME")
                        If (ActionMethod = Common.Navigation_Mode._View Or ActionMethod = Common.Navigation_Mode._Delete) Then
                            newdata.disabled = True
                        ElseIf row.Field(Of String)("PERMISSION") = "F" Then
                            newdata.disabled = False
                        Else
                            newdata.disabled = True
                        End If
                        _User_data.Add(newdata)
                    End If
                Next
            End If
            Return _User_data
        End Function
        Public Function GetSplVchrRefsOnEdit(ByVal Rec_ID As String) As DataTable
            Dim SplvRefs As DataTable = GetRecordByCustom("WHERE COALESCE(TR_M_ID,TXN_REC_ID) ='" & Rec_ID & "' AND TR_CEN_ID='" & cBase._open_Cen_ID & "' AND TR_COD_YEAR_ID='" & cBase._open_Year_ID & "' ", ClientScreen.Accounts_Voucher_CashBank, Tables.TRANSACTION_D_REFERENCE_INFO)

            Return SplvRefs
        End Function
#End Region
    End Class
#End Region


    End Class
