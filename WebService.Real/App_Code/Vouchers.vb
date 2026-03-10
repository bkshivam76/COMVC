Imports System.Data

Namespace Real
    <Serializable>
    Public Class Vouchers
        Public Shared _Server_Date_Format_Short As String = "yyyy-MM-dd"
        Public Shared _Date_Format_Short As String = "MM-dd-yyyy"
#Region "Param Classes"
        <Serializable>
        Public Class Param_Vouchers_GetCashBalanceSummary
            Public FROM_DATE As Date
            Public TO_DATE As Date
            Public YEAR_START_DATE As Date
            Public CEN_ID As Integer
            Public YEAR_ID As Integer
            Public INS_ID As String
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetBankBalanceSummary
            Public FROM_DATE As Date
            Public TO_DATE As Date
            Public YEAR_START_DATE As Date
            Public CEN_ID As Integer
            Public YEAR_ID As Integer
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetAssetRecID
            Public TableName As ConnectOneWS.Tables
            Public TxnMID As String
            Public TrSrNo As Integer = 0
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetPastParties
            Public FromDate As Date
            Public ToDate As Date
            Public IsInternalTransfer As Boolean
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetListWithMultipleParams
            Public FromDate As Date
            Public ToDate As Date
            Public Online_Bank_Col_TR_Rec As String
            Public Online_Bank_Col_NB_Rec As String
            Public Local_Bank_Col_TR_Rec As String
            Public Local_Bank_Col_NB_Rec As String
            Public Online_Bank_Col_TR_Pay As String
            Public Online_Bank_Col_NB_Pay As String
            Public Local_Bank_Col_TR_Pay As String
            Public Local_Bank_Col_NB_Pay As String
            Public openYearSdt As Date
            Public openYearEdt As Date
            Public Advanced_Filter_Category As String
            Public Advanced_Filter_Ref_ID As String
            Public showDynamicBankColumns As Boolean
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetListWithMultipleParams_SP
            Public FromDate As Date
            Public ToDate As Date
            Public INS_ID As String
            Public OpenYearSdt As Date
            Public OpenYearEdt As Date
            Public Advanced_Filter_Category As String
            Public Advanced_Filter_Ref_ID As String
        End Class

        'Public Class Param_Vouchers_GetList_SQL
        '    Public FromDate As Date
        '    Public ToDate As Date
        '    Public Online_Bank_Col_TR_Rec As String
        '    Public Online_Bank_Col_NB_Rec As String
        '    Public Local_Bank_Col_TR_Rec As String
        '    Public Local_Bank_Col_NB_Rec As String
        '    Public Online_Bank_Col_TR_Pay As String
        '    Public Online_Bank_Col_NB_Pay As String
        '    Public Local_Bank_Col_TR_Pay As String
        '    Public Local_Bank_Col_NB_Pay As String
        '    Public openYearSdt As Date
        '    Public openYearEdt As Date
        'End Class
        <Serializable>
        Public Class Param_Vouchers_GetList
            Public FromDate As Date
            Public ToDate As Date
            Public Led_ID As String
            Public Bank_Acc_ID As String
            Public OtherCondition As String
            Public openYearSdt As Date
            Public openYearEdt As Date
        End Class
        <Serializable>
        Public Class Param_Vouchers_GetNegativeBalance
            Public Led_ID As String
            Public Bank_Acc_ID As String
            Public OtherCondition As String
        End Class
        <Serializable>
        Public Class Parameter_GetAdjustments
            Public CrossRefId As String
            Public EntryType As String
            Public TableName As String
            Public Excluded_Rec_M_ID As String
            Public YearID As Integer
            Public NextUnauditedYear As Integer
        End Class
        <Serializable>
        Public Class Parameter_GetReferenceTxnRecord_MID
            Public CrossRefId As String
            Public Excluded_M_ID As String
        End Class
        <Serializable>
        Public Class Param_GetStatusTrCode
            Public Tr_RecID As String
            Public Tr_MID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetSaleReferenceRecord
            Public RefID As String
            Public Exclude_PrevYears As Boolean = False
        End Class
        <Serializable>
        Public Class Param_GetAdvancedFilters
            Public Asset_Profile As String
            Public Prev_YearID As Integer
        End Class
        <Serializable>
        Public Class Param_GetBank_Reconciliation
            Public DateofReconcile As Date
            Public BankID As String
        End Class
        <Serializable>
        Public Class Param_RejectDraftEntry
            Public TXN_REC_ID As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_GetProfileEntryDetails
            Public Profile_Used As ConnectOneWS.AssetProfiles
            Public ProfileRecID As String
            Public TR_M_ID As String
        End Class
        <Serializable>
        Public Class Param_UpdateCommonDetails_Txn
            Public TR_M_ID As String
            Public ID As String
            Public Purpose_ID As String
            Public Narration As String
            Public Reference As String
            Public InsertSplVchrRefs As Parameter_InsertSplVchrRef_Vouchers()
        End Class
        <Serializable>
        Public Class Param_GetVouchingAuditData
            Public Curr_Institution_ID As String
            Public Curr_Vouching_Category As String
        End Class
        <Serializable>
        Public Enum Vouching_audit_Status
            Accepted
            Rejected
            Skipped
        End Enum
        <Serializable>
        Public Class Param_AddVouchingAudit
            Public Entry_ID As String
            Public Vouching_Category As String
            Public Vouching_Status As Vouching_audit_Status
            Public Responses As List(Of Param_AddVouchingAuditResponse)
        End Class
        <Serializable>
        Public Class Param_AddVouchingAuditResponse
            Public Question As String
            Public Response As String
        End Class
        <Serializable>
        Public Class Param_Offer_Entry_for_Audit
            Public REF_ID As String
            Public REF_TABLE As ConnectOneWS.Tables
            Public REF_SCREEN As ConnectOneWS.ClientScreen
        End Class
#End Region
        ''' <summary>
        ''' Gets Max Transaction Date
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Case RealServiceFunctions.Vouchers_GetMaxTransactionDate</remarks>
        Public Shared Function GetMaxTransactionDate(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT MAX(TR_DATE) AS MAXDATE FROM Transaction_Info WHERE  REC_STATUS IN (0,1,2)  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        'RealServiceFunctions.Vouchers_GetAdjustments
        Public Shared Function GetAdjustments(ByVal inParam As Parameter_GetAdjustments, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NextYear As String = ""
            If inParam.NextUnauditedYear.ToString.Length > 0 Then NextYear = " or TR_COD_YEAR_ID =" & inParam.NextUnauditedYear.ToString & ""
            Dim ExcludeMasterID As String = ""
            If Not inParam.Excluded_Rec_M_ID Is Nothing Then
                ExcludeMasterID = " AND TR_M_ID <> '" & inParam.Excluded_Rec_M_ID & "' "
            End If
            Dim Query As String = ""
            If inParam.CrossRefId Is Nothing Then
                Query = " select SUM(TR_AMOUNT) AS AMOUNT,SUM(TR_QTY) as QTY, TR_TYPE, TR_TRF_CROSS_REF_ID from transaction_info where TR_TRF_CROSS_REF_ID IS NOT NULL AND UPPER(TR_TYPE) = '" & inParam.EntryType.ToUpper() & "' AND TR_TRF_CROSS_REF_ID IN ( SELECT REC_ID FROM " & inParam.TableName & " WHERE REC_STATUS IN (0,1,2))  AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) " & ExcludeMasterID & "  and (TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TR_TYPE, TR_TRF_CROSS_REF_ID"
            Else
                If Not inParam.CrossRefId.StartsWith("'") Then
                    Query = " select SUM(TR_AMOUNT) AS AMOUNT,SUM(TR_QTY) as QTY, TR_TYPE, TR_TRF_CROSS_REF_ID from transaction_info where TR_TRF_CROSS_REF_ID IS NOT NULL AND UPPER(TR_TYPE) = '" & inParam.EntryType.ToUpper() & "' AND TR_TRF_CROSS_REF_ID ='" & inParam.CrossRefId & "' AND REC_STATUS IN (0,1,2) " & ExcludeMasterID & "  AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & "  and (TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TR_TYPE, TR_TRF_CROSS_REF_ID"
                Else
                    Query = " select SUM(TR_AMOUNT) AS AMOUNT,SUM(TR_QTY) as QTY, TR_TYPE, TR_TRF_CROSS_REF_ID from transaction_info where TR_TRF_CROSS_REF_ID IS NOT NULL AND UPPER(TR_TYPE) = '" & inParam.EntryType.ToUpper() & "' AND TR_TRF_CROSS_REF_ID IN (" & inParam.CrossRefId & ") AND REC_STATUS IN (0,1,2) " & ExcludeMasterID & "  AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & "  and (TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TR_TYPE, TR_TRF_CROSS_REF_ID"
                End If
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inParam.EntryType, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Cash Balance Summary
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetCashBalanceSummary</remarks>
        Public Shared Function GetCashBalanceSummary(ByVal Param As Param_Vouchers_GetCashBalanceSummary, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Store Procedure:  CALL Get_Cash_Balance_Summary('2011-04-01','2012-03-31','2011-04-01','00207','1112','00001')
            Dim SPName As String = "Get_Cash_Balance_Summary"
            Dim params() As String = {"X_FROM_DATE", "X_TO_DATE", "X_YEAR_START_DATE", "X_CEN_ID", "X_YEAR_ID", "X_INS_ID"}
            Dim values() As Object = {Param.FROM_DATE, Param.TO_DATE, Param.YEAR_START_DATE, Param.CEN_ID, Param.YEAR_ID, Param.INS_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Date, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {10, 10, 10, 5, 4, 5}
            Return dbService.ListFromSP(ConnectOneWS.Tables.OPENING_BALANCES_INFO, SPName, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get BankBalance Summary
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetBankBalanceSummary</remarks>
        Public Shared Function GetBankBalanceSummary(ByVal Param As Param_Vouchers_GetBankBalanceSummary, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Store Procedure:  CALL Get_Bank_Balance_Summary('2011-04-01','2012-03-31','2011-04-01','00640','1112')
            Dim SPName As String = "Get_Bank_Balance_Summary"
            Dim params() As String = {"X_FROM_DATE", "X_TO_DATE", "X_YEAR_START_DATE", "X_CEN_ID", "X_YEAR_ID"}
            Dim values() As Object = {Param.FROM_DATE, Param.TO_DATE, Param.YEAR_START_DATE, Param.CEN_ID, Param.YEAR_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Date, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {10, 10, 10, 5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, SPName, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Returns First txn Entry where RefID has been used 
        ''' </summary>
        ''' <param name="RefID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceTxnRecord</remarks>
        Public Shared Function GetReferenceTxnRecord(ByVal RefID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TOP 1 TR_DATE, TR_AMOUNT FROM transaction_info WHERE TR_TRF_CROSS_REF_ID = '" & RefID & "' AND rec_status IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns First txn Entry where RefID has been used , excluding a particular TrMId
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceTxnRecord</remarks>
        Public Shared Function GetReferenceTxnRecord_ExcludeM_ID(ByVal inParam As Parameter_GetReferenceTxnRecord_MID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TOP 1 TR_DATE, TR_AMOUNT FROM transaction_info WHERE TR_TRF_CROSS_REF_ID = '" & inParam.CrossRefId & "' AND rec_status IN (0,1,2) AND TR_M_ID <> '" & inParam.Excluded_M_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Referred Asset ID in provide Txn
        ''' </summary>
        ''' <param name="TxnMID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetReferenceRecordID</remarks>
        Public Shared Function GetReferenceRecordID(ByVal TxnMID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT TOP 1 TR_TRF_CROSS_REF_ID FROM transaction_info WHERE TR_M_ID = '" & TxnMID & "' AND rec_status IN (0,1,2) AND LEN(COALESCE(TR_TRF_CROSS_REF_ID,'')) > 0"

            Dim value As Object = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
            Return value
        End Function

        ''' <summary>
        ''' Gets Referred Record IDs in a Txn
        ''' </summary>
        ''' <param name="TxnMID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetRefRecordIDS(ByVal TxnMID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT distinct TR_TRF_CROSS_REF_ID, TR_ITEM_ID, ITEM_PROFILE, COALESCE(TR_AB_ID_1,'') TR_AB_ID_1, ITEM_VOUCHER_TYPE FROM dbo.transaction_info AS ti INNER JOIN dbo.item_info AS II ON TR_ITEM_ID = ii.REC_ID WHERE tr_m_id ='" & TxnMID & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns First txn Entry where RefID has been used as sale of Assets 
        ''' </summary>
        ''' <param name="RefID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetSaleReferenceRecord</remarks>
        Public Shared Function GetSaleReferenceRecord(ByVal param As Param_GetSaleReferenceRecord, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT TOP 1 TR_DATE, TR_AMOUNT FROM transaction_info as ti INNER JOIN  transaction_d_payment_info as TP on ti.TR_M_ID = TP.TR_M_ID WHERE TP.REC_STATUS IN (0,1,2) AND TP.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ('" & param.RefID & "') "
            If param.Exclude_PrevYears Then 'in jvs
                Query += " AND TP.TR_COD_YEAR_ID >= " & inBasicParam.openYearID.ToString & " "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns First txn Entry where RefID has been used as payment reference
        ''' </summary>
        ''' <param name="RefID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetPaymentReferenceRecord</remarks>
        Public Shared Function GetPaymentReferenceRecord(ByVal RefID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT TOP 1 TR_DATE, TR_AMOUNT FROM transaction_info as ti INNER JOIN  transaction_d_payment_info as TP on ti.TR_M_ID = TP.TR_M_ID WHERE TP.REC_STATUS IN (0,1,2) AND TI.REC_STATUS IN (0,1,2) AND TP.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_REF_ID IN ('" & RefID & "') "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets ItemId of Items Selected in donation by gift entry
        ''' </summary>
        ''' <param name="RefID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetAssetItemID</remarks>
        Public Shared Function GetAssetItemID(ByVal RefID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_ITEM_ID FROM transaction_info WHERE TR_M_ID ='" & RefID & "'  AND TR_ITEM_ID <> 'd0a33061-d679-4f21-ac12-a29541de8fcb' AND REC_STATUS IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets RecID of the Asset from TxnMID
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetAssetRecID</remarks>
        Public Shared Function GetAssetRecID(ByVal Param As Param_Vouchers_GetAssetRecID, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case Param.TableName
                Case ConnectOneWS.Tables.ASSET_INFO
                    Query = "SELECT rec_id FROM ASSET_INFO WHERE AI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND AI_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.GOLD_SILVER_INFO
                    Query = "SELECT rec_id FROM GOLD_SILVER_INFO WHERE GS_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND GS_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.LIVE_STOCK_INFO
                    Query = "SELECT rec_id FROM LIVE_STOCK_INFO WHERE LS_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND LS_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.VEHICLES_INFO
                    Query = "SELECT rec_id FROM VEHICLES_INFO WHERE VI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND VI_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.LAND_BUILDING_INFO
                    Query = "SELECT rec_id FROM LAND_BUILDING_INFO WHERE LB_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND LB_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.WIP_INFO
                    Query = "SELECT rec_id FROM WIP_INFO WHERE WIP_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                    If Param.TrSrNo > 0 Then Query += " AND WIP_TR_ITEM_SRNO = " & Param.TrSrNo
                Case ConnectOneWS.Tables.FD_INFO
                    Query = "SELECT rec_id FROM FD_INFO WHERE FD_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Case ConnectOneWS.Tables.ADVANCES_INFO
                    Query = "SELECT rec_id FROM ADVANCES_INFO WHERE AI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Case ConnectOneWS.Tables.DEPOSITS_INFO
                    Query = "SELECT rec_id FROM DEPOSITS_INFO WHERE DI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Case ConnectOneWS.Tables.LIABILITIES_INFO
                    Query = "SELECT rec_id FROM LIABILITIES_INFO WHERE LI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
                Case ConnectOneWS.Tables.WIP_INFO
                    Query = "SELECT rec_id FROM WIP_INFO WHERE WIP_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2)"
            End Select
            Dim value As Object = dbService.GetScalar(Param.TableName, Query, Param.TableName.ToString(), inBasicParam)
            Return value
        End Function

        ''' <summary>
        ''' Gets Record Edit On time by RecID
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetEditOnByRecID</remarks>
        Public Shared Function GetEditOnByRecID(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT REC_EDIT_ON FROM transaction_info WHERE REC_ID ='" & Rec_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Past Parties
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetPastParties</remarks>
        Public Shared Function GetPastParties(ByVal Param As Param_Vouchers_GetPastParties, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Tr_Code_Condition As String = ""
            If Param.IsInternalTransfer Then
                Tr_Code_Condition = "= 8"
            Else
                Tr_Code_Condition = "<> 8"
            End If
            Dim OnlineQuery As String = "SELECT DISTINCT TR_AB_ID_1 FROM TRANSACTION_INFO WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND TR_NOTEBOOK IS NULL  AND TR_CODE " & Tr_Code_Condition &
                                   " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List With Multiple Params
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetListWithMultipleParams</remarks>'Rename it as GetList WithMultipleParams here and in call from service.vb
        Public Shared Function GetListWithMultipleParams(ByVal Param As Param_Vouchers_GetListWithMultipleParams, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Common.UseSQL() Then
                Return GetListWithMultipleParams_SQL(Param, inBasicParam)
            End If
            Dim OnlineQuery As String = " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  MAX(IF (TR_TYPE = 'DEBIT',TR_DR_LED_ID,TR_CR_LED_ID)) AS iLED_ID,'' AS iTR_HEAD, Max(IF(TR_TYPE='DEBIT',TR_SUB_DR_LED_ID,TR_SUB_CR_LED_ID))  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(IF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, MONTH(TR_DATE)-3, MONTH(TR_DATE)+9)) AS iTR_DATE_SERIAL," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, CONCAT('" & Year(Param.openYearSdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ), CONCAT('" & Year(Param.openYearEdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ))) AS iTR_DATE_SHOW, " &
                                        " MIN( If(TR_CODE In (4,5,6,9,11) ,'A', if(TR_CODE = 8 and TR_TYPE='CREDIT','A', if(TR_CODE = 8 and TR_TYPE='DEBIT','C', If(TR_CODE In (1,2,10), 'B', If(TR_CODE=7,'D','C')))))) AS iTR_ENTRY," &
                                        " SUM(IF (TR_DR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_REC_CASH   ," &
                                        " SUM(IF (TR_DR_LED_ID = '00079',TR_AMOUNT,NULL))                                                                      AS iTR_REC_BANK   ," & Param.Online_Bank_Col_TR_Rec &
                                        " SUM(IF (LENGTH(IF (ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID)) = 0,TR_AMOUNT,NULL))                                       AS iTR_REC_JOURNAL," &
                                        " SUM(IF (TR_DR_LED_ID IN ('00079', '00080') OR LENGTH(IF (ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID)) = 0,TR_AMOUNT,NULL)) AS iTR_REC_TOTAL  ," &
                                        " SUM(IF (TR_CR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_PAY_CASH   ," &
                                        " SUM(IF (TR_CR_LED_ID = '00079',TR_AMOUNT,NULL))                                                                      AS iTR_PAY_BANK   ," & Param.Online_Bank_Col_TR_Pay &
                                        " SUM(IF (LENGTH(IF (ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID)) = 0,TR_AMOUNT,NULL))                                       AS iTR_PAY_JOURNAL," &
                                        " SUM(IF (TR_CR_LED_ID IN ('00079', '00080') OR LENGTH(IF (ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID)) = 0,TR_AMOUNT,NULL)) AS iTR_PAY_TOTAL  ," &
                                        " MAX(TR_NARRATION) AS iTR_NARRATION,'' AS DON_STATUS,'B' AS iTR_ROW_POS,MAX(TR_TYPE) AS iTR_TYPE,MAX(TR_CODE) AS iTR_CODE,MAX(REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) as iTR_SR_NO,MIN(If(TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' ,'A',If(TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT','A','B'))) as iTR_SORT, MAX(REC_ADD_ON) AS iREC_ADD_ON, " &
                                        " MAX( IF( LENGTH(IF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
                                        " CASE IF(ISNULL(Transaction_Info.REC_STATUS),99,Transaction_Info.REC_STATUS) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End as iACTION_STATUS, MAX(REC_EDIT_ON) AS iREC_EDIT_ON, MAX(REC_ADD_BY) AS iREC_ADD_BY, MAX(REC_EDIT_BY) AS iREC_EDIT_BY,TR_TRF_CROSS_REF_ID as iCross_Ref_ID," &
                                        Common.Remarks_Detail_Txn("Transaction_Info", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) &
                                        " FROM  Transaction_Info WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL " &
                                        " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' "
            OnlineQuery = OnlineQuery & GetAdvancedFilterQuery(Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openCenID)
            OnlineQuery = OnlineQuery & " GROUP BY IF(ISNULL(TR_M_ID), REC_ID, TR_M_ID),  TR_ITEM_ID,  TR_SR_NO " &
                                        " " &
                                        " UNION ALL " &
                                        " " &
                                        " SELECT '',DATE_SUB(DATE_ADD(DATE_SUB(tr_date,INTERVAL DAYOFMONTH(tr_date)-1 DAY),INTERVAL 1 MONTH),INTERVAL 1 DAY), TR_ITEM_ID AS iTR_ITEM_ID,'' AS iTR_ITEM,MAX(IF(TR_TYPE = 'DEBIT',TR_DR_LED_ID,TR_CR_LED_ID)) AS iLED_ID,'' AS iTR_HEAD,NULL,NULL,'' AS iTR_PARTY_1,MAX(IF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, MONTH(TR_DATE)-3, MONTH(TR_DATE)+9)) AS iTR_DATE_SERIAL," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, CONCAT('" & Year(Param.openYearSdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ), CONCAT('" & Year(Param.openYearEdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ))) AS iTR_DATE_SHOW, " &
                                        " 'C'             AS iTR_ENTRY      ," &
                                        " NULL            AS iTR_REC_CASH   ," &
                                        " NULL            AS iTR_REC_BANK   ," & Param.Online_Bank_Col_NB_Rec &
                                        " NULL            AS iTR_REC_JOURNAL," &
                                        " NULL            AS iTR_REC_TOTAL  ," &
                                        " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," &
                                        " NULL            AS iTR_PAY_BANK   ," & Param.Online_Bank_Col_NB_Pay &
                                        " NULL            AS iTR_PAY_JOURNAL," &
                                        " SUM(TR_AMOUNT)  AS iTR_PAY_TOTAL  ," &
                                        " 'Note-Book Entry' AS iTR_NARRATION, NULL AS DON_STATUS, 'C' AS iTR_ROW_POS,MAX(TR_TYPE) AS iTR_TYPE, 3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL as iTR_SR_NO,  NULL as iTR_SORT,  MAX(REC_ADD_ON) AS iREC_ADD_ON,   " &
                                        " MAX( IF( LENGTH(IF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO , " &
                                        " CASE IF(ISNULL(Transaction_Info.REC_STATUS),99,Transaction_Info.REC_STATUS) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End as iACTION_STATUS, MAX(REC_EDIT_ON) AS iREC_EDIT_ON, MAX(REC_ADD_BY) AS iREC_ADD_BY, MAX(REC_EDIT_BY) AS iREC_EDIT_BY,TR_TRF_CROSS_REF_ID as iCross_Ref_ID," &
                                        Common.Remarks_Detail_Txn("Transaction_Info", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) &
                                        " FROM    Transaction_Info  " &
                                        " WHERE REC_STATUS IN (0,1,2)  AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND TR_NOTEBOOK = 'YES'  " &
                                        " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' "
            OnlineQuery = OnlineQuery & GetAdvancedFilterQuery(Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openCenID)
            OnlineQuery = OnlineQuery & " GROUP BY DATE_FORMAT(tr_date,'%Y%m'),    TR_ITEM_ID "

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAdvancedFilterQuery(Advanced_Filter_Category As String, Advanced_Filter_Ref_ID As String, CenID As Integer) As String
            Select Case Advanced_Filter_Category
                Case "GOLD", "SILVER"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_REF_OTHERS =  '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT GS_TR_ID FROM gold_silver_info WHERE REC_ID =  '" & Advanced_Filter_Ref_ID & "'   AND GS_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "OTHER ASSETS"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_REF_OTHERS =  '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT AI_TR_ID FROM asset_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'   AND AI_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN  '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "VEHICLES"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_REF_OTHERS =  '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT VI_TR_ID FROM vehicles_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'   AND VI_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN  '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "LIVESTOCK"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_REF_OTHERS =  '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT LS_TR_ID FROM live_stock_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'   AND LS_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN  '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "LAND & BUILDING"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_REF_OTHERS =  '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT LB_TR_ID FROM land_building_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'   AND LB_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "FD"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "'  AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2)) " &
                                    " OR MAX(TR_M_ID) IN (SELECT FD_TR_ID FROM fd_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'   AND FD_CEN_ID = " & CenID.ToString & ") " &
                                " ) THEN  '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "PURPOSE"
                    Return ", CASE WHEN  " &
                                " ( " &
                                    " MAX(TR_M_ID) IN (SELECT TR_REC_ID FROM transaction_d_purpose_info TP WHERE TR_PURPOSE_MISC_ID = '" & Advanced_Filter_Ref_ID & "' AND TP.TR_CEN_ID = " & CenID.ToString & " AND TR_REC_ID = MAX(TR_M_ID) AND TP.REC_STATUS IN (0,1,2)) " &
                                    " AND MAX(TR_SR_NO) in (SELECT TR_ITEM_SR_NO FROM transaction_d_purpose_info TP WHERE TR_PURPOSE_MISC_ID = '" & Advanced_Filter_Ref_ID & "' AND TP.TR_CEN_ID = " & CenID.ToString & " AND TR_REC_ID = MAX(TR_M_ID) AND TP.REC_STATUS IN (0,1,2)) " &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "ADVANCES"
                    Return ", CASE WHEN  " &
                                " ( " &
                                        " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2))" &
                                        " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM transaction_d_payment_info TP WHERE TR_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TP.TR_CEN_ID =  " & CenID.ToString & " AND TP.REC_STATUS IN (0,1,2)	AND TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','ADVANCE'))" &
                                        " OR MAX(TR_M_ID) IN (SELECT AI_TR_ID FROM advances_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'  AND AI_CEN_ID = " & CenID.ToString & ")" &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "OTHER DEPOSITS"
                    Return ", CASE WHEN  " &
                                " ( " &
                                        " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2))" &
                                        " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM transaction_d_payment_info TP WHERE TR_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TP.TR_CEN_ID =  " & CenID.ToString & " AND TP.REC_STATUS IN (0,1,2)	AND TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT'))" &
                                        " OR MAX(TR_M_ID) IN (SELECT DI_TR_ID FROM deposits_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'  AND DI_CEN_ID = " & CenID.ToString & ")" &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "OTHER LIABILITIES"
                    Return ", CASE WHEN  " &
                                " ( " &
                                        " MAX(TR_M_ID) IN (SELECT TR_M_ID FROM TRANSACTION_INFO TI WHERE TR_TRF_CROSS_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2))" &
                                        " OR MAX(TR_M_ID) IN (SELECT TR_M_ID FROM transaction_d_payment_info TP WHERE TR_REF_ID = '" & Advanced_Filter_Ref_ID & "' AND TP.TR_CEN_ID =  " & CenID.ToString & " AND TP.REC_STATUS IN (0,1,2)	AND TP.TR_PAY_TYPE IN ('LIABILITIES'))" &
                                        " OR MAX(TR_M_ID) IN (SELECT LI_TR_ID FROM liabilities_info WHERE REC_ID = '" & Advanced_Filter_Ref_ID & "'  AND LI_CEN_ID = " & CenID.ToString & ")" &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case "BANK ACCOUNTS"
                    Return ", CASE WHEN  " &
                                " ( " &
                                        " MAX(COALESCE(TR_M_ID,Transaction_Info.REC_ID)) IN (SELECT COALESCE(TR_M_ID,TI.REC_ID) FROM TRANSACTION_INFO TI WHERE COALESCE(TR_SUB_CR_LED_ID,'') = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2))" &
                                        " OR MAX(COALESCE(TR_M_ID,Transaction_Info.REC_ID)) IN (SELECT COALESCE(TR_M_ID,TI.REC_ID) FROM TRANSACTION_INFO TI WHERE COALESCE(TR_SUB_DR_LED_ID,'') = '" & Advanced_Filter_Ref_ID & "' AND TI.TR_CEN_ID = " & CenID.ToString & " AND TI.REC_STATUS IN (0,1,2))" &
                                " ) THEN '" & Advanced_Filter_Category & " = " & Advanced_Filter_Ref_ID & "' ELSE '' END AS 'Advanced_Filter'"
                Case Else
                    Return ",''  AS 'Advanced_Filter'"
            End Select
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListWithMultipleParams_SP(ByVal inParam As Param_Vouchers_GetListWithMultipleParams_SP, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"X_FROM_DATE", "X_TO_DATE", "X_CEN_ID", "X_YEAR_ID", "X_INS_ID", "Open_Year_Sdt", "Open_Year_Edt", "X_ADVANCED_FILTER_CATEGORY", "X_ADVANCED_FILTER_REF_ID"}
            Dim values() As Object = {inParam.FromDate, inParam.ToDate, inBasicParam.openCenID, inBasicParam.openYearID, inParam.INS_ID, inParam.OpenYearSdt, inParam.OpenYearEdt, inParam.Advanced_Filter_Category, inParam.Advanced_Filter_Ref_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String}
            Dim lengths() As Integer = {20, 20, 5, 4, 5, 20, 20, 20, 36}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "SP_get_CashBook_Transactions", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function
        ''' <summary>
        ''' Get List With Multiple Params 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetListWithMultipleParams_SQL(ByVal Param As Param_Vouchers_GetListWithMultipleParams, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"@FROM_DATE", "@TO_DATE", "@CENID", "@YEARID", "@Advanced_Filter_Category", "@Advanced_Filter_Ref_ID", "@UserID", "@Show_Vouch_Status", "@Show_Attachment_Status", "@SHOW_BANK_COLUMNS"}
            Dim values() As Object = {Param.FromDate, Param.ToDate, inBasicParam.openCenID, inBasicParam.openYearID, Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openUserID, inBasicParam.ShowVouchingIndicator, inBasicParam.ShowAttachmentIndicator, Param.showDynamicBankColumns}
            Dim dbTypes() As System.Data.DbType = {DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Boolean, DbType.Boolean, DbType.Boolean}
            Dim lengths() As Integer = {20, 20, 4, 4, 8000, 36, 255, 1, 1, 1}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "get_Cashbook_Listing", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            Return Data

            'commented after " FROM  Transaction_Info in first part of query :: '"," & Common.Remarks_Detail_Txn_Grouped("Transaction_Info", DataFunctions.GetCurrentDateTime(ConnectOneWS.ClientScreen.Accounts_Vouchers, openUserID, openCenID, PCID, version), Common.Server_Date_Format_Short) & _
            'Dim OnlineQuery As String = " DECLARE @ATTACHMENT_MAPPING TABLE (	ID VARCHAR(36), 	TOT_CNT INT, 	COMP_CNT INT, 	RESP_CNT INT, 	REJECT_CNT INT ,OTHER_ATTACH_CNT INT ,ALL_ATTACH_CNT INT ); "
            'OnlineQuery = OnlineQuery + " DECLARE @ITEMS TABLE (	TR_ITEM_ID VARCHAR(36), 	REC_ID  VARCHAR(36), 	TR_M_ID VARCHAR(36) , 	TR_CODE INT); "
            'OnlineQuery = OnlineQuery + " INSERT INTO @ITEMS " &
            '                            " SELECT TR_ITEM_ID, REC_ID, TR_M_ID, TR_CODE  FROM transaction_info TI " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE NOT IN (11) " &
            '                            " UNION ALL  " &
            '                            " SELECT AI_ITEM_ID, TI.REC_ID, TR_M_ID , TR_CODE  FROM transaction_info TI " &
            '                            " INNER Join asset_info AI ON TI.TR_TRF_CROSS_REF_ID = AI.REC_ID " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE IN (11) " &
            '                            " UNION ALL " &
            '                            " SELECT LS_ITEM_ID, TI.REC_ID, TR_M_ID, TR_CODE   FROM transaction_info TI " &
            '                            " INNER Join live_stock_info AI ON TI.TR_TRF_CROSS_REF_ID = AI.REC_ID " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE IN (11) " &
            '                            " UNION ALL " &
            '                            " SELECT GS_ITEM_ID, TI.REC_ID, TR_M_ID, TR_CODE   FROM transaction_info TI " &
            '                            " INNER Join gold_silver_info AI ON TI.TR_TRF_CROSS_REF_ID = AI.REC_ID " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE IN (11) " &
            '                            " UNION ALL " &
            '                            " SELECT LB_ITEM_ID, TI.REC_ID, TR_M_ID, TR_CODE   FROM transaction_info TI " &
            '                            " INNER Join land_building_info AI ON TI.TR_TRF_CROSS_REF_ID = AI.REC_ID " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE IN (11) " &
            '                            " UNION ALL " &
            '                            " SELECT VI_ITEM_ID, TI.REC_ID, TR_M_ID, TR_CODE   FROM transaction_info TI " &
            '                            " INNER Join vehicles_info AI ON TI.TR_TRF_CROSS_REF_ID = AI.REC_ID " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'  AND TR_CODE IN (11) ; "

            'OnlineQuery = OnlineQuery + " INSERT INTO @ATTACHMENT_MAPPING " &
            '                            " SELECT  COALESCE(TI.TR_M_ID, TI.REC_ID) ID ,0,0,0, COALESCE(SUM(COALESCE(ARI_ALL.REJECT_CNT,0)),0) AS REJECT_CNT,COALESCE(SUM(COALESCE(ARI_ALL.CNT,0)),0) AS OTHER_ATTACH_CNT,COALESCE(SUM(COALESCE(ARI_ALL.CNT,0)),0) AS ALL_ATTACH_CNT " &
            '                            " FROM @ITEMS TI   LEFT JOIN (" &
            '                            " SELECT ARI.ALI_REF_REC_ID, AI.AI_CATEGORY,1 AS CNT, CASE WHEN AI_CHECKED = 0 THEN 1 ELSE 0 END AS REJECT_CNT " &
            '                            " FROM attachment_reference_info  AS ARI  " &
            '                            " Left JOIN attachment_info AS AI ON ARI.ALI_ATTACHMENT_ID = AI.REC_ID " &
            '                            " ) AS ARI_ALL ON COALESCE(TI.TR_M_ID, TI.REC_ID) = ARI_ALL.ALI_REF_REC_ID " &
            '                            " GROUP BY COALESCE(TI.TR_M_ID, TI.REC_ID); "

            'OnlineQuery = OnlineQuery + " UPDATE MAP SET TOT_CNT= B.TOT_CNT, COMP_CNT = B.COMP_CNT, RESP_CNT = B.RESP_CNT, OTHER_ATTACH_CNT = ALL_ATTACH_CNT - B.COMP_CNT FROM @ATTACHMENT_MAPPING MAP INNER JOIN ( " &
            '                            " SELECT  COALESCE(TI.TR_M_ID, TI.REC_ID) ID, COUNT(TI.REC_ID) AS TOT_CNT, SUM(COALESCE(ARI.CNT,0)) AS COMP_CNT, SUM(COALESCE(doc_resp.CNT,0)) AS RESP_CNT   " &
            '                            " FROM transaction_info TI  INNER Join item_info AS II ON TR_ITEM_ID = II.REC_ID   " &
            '                            " INNER JOIN transaction_doc_mapping AS MAP ON MAP.TR_DOC_ITEM_ID = TI.TR_ITEM_ID AND TI.TR_CODE = MAP.TR_DOC_TR_CODE AND MAP.TR_DOC_TR_CODE IS NOT NULL  " &
            '                            " Left Join ( SELECT TR_DOC_RESP_MAP_ID, TR_DOC_RESP_REF_REC_ID, COUNT(TR_DOC_RESP_REASON) CNT FROM transaction_doc_mapping_Response GROUP BY TR_DOC_RESP_MAP_ID, TR_DOC_RESP_REF_REC_ID)  doc_resp on MAP.TR_DOC_ID = doc_resp.TR_DOC_RESP_MAP_ID AND doc_resp.TR_DOC_RESP_REF_REC_ID = COALESCE(TI.TR_M_ID, TI.REC_ID)  " &
            '                            " LEFT JOIN (SELECT DISTINCT ARI.ALI_REF_REC_ID, AI.AI_CATEGORY,1 AS CNT FROM attachment_reference_info  AS ARI  Left JOIN attachment_info AS AI ON ARI.ALI_ATTACHMENT_ID = AI.REC_ID) AS ARI ON COALESCE(TI.TR_M_ID,TI.REC_ID) = ARI.ALI_REF_REC_ID  AND MAP.TR_DOC_MISC_ID = ARI.AI_CATEGORY  " &
            '                            " WHERE TR_CEN_ID =  " & inBasicParam.openCenID.ToString & " AND TR_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " And tr_date between '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "'" &
            '                             " GROUP BY  COALESCE(TI.TR_M_ID, TI.REC_ID) ) AS B ON MAP.ID = B.ID ;"

            'OnlineQuery = OnlineQuery + " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  MAX(II.ITEM_NAME) AS iTR_ITEM,  MAX(CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END) AS iLED_ID,MAX(LED_NAME) AS iTR_HEAD, MAX(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  CASE WHEN MAX(AB1.C_NAME) IS NOT NULL THEN MAX(AB1.C_NAME)  + CASE WHEN MAX(AB1.C_NAME_DUP_ID) IS NOT NULL THEN '('+CAST(MAX(AB1.C_NAME_DUP_ID) AS VARCHAR)+')' ELSE '' END  ELSE CASE WHEN MAX(TR_CODE) IN (8,15,18,19,20) THEN MAX(CI.CEN_NAME) ELSE '' END END AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME, " &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL, " &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-' +dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01'  END) AS iTR_DATE_SHOW, " &
            '                            " MIN(CASE WHEN TR_CODE IN (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE IN (1,2,10) THEN 'B'ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE CASE WHEN TR_CODE=14 THEN 'E' ELSE 'C'END END END END END END) AS iTR_ENTRY, " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH   , " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_BANK   , " & Param.Online_Bank_Col_TR_Rec &
            '                            " SUM(CASE WHEN LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_REC_JOURNAL, " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_REC_TOTAL  , " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH   , " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_BANK   , " & Param.Online_Bank_Col_TR_Pay &
            '                            " SUM(CASE WHEN LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_PAY_JOURNAL, " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_PAY_TOTAL  , " &
            '                            " MAX(TR_NARRATION) AS iTR_NARRATION,'B' AS iTR_ROW_POS,MAX(TR_TYPE) AS iTR_TYPE,MAX(TR_CODE) AS iTR_CODE,MAX(Transaction_Info.REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) AS iTR_SR_NO,MIN(CASE WHEN TR_CODE IN (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE IN (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B'END END) AS iTR_SORT, MAX(Transaction_Info.REC_ADD_ON) AS iREC_ADD_ON, " &
            '                            " MAX(CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE Transaction_Info.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
            '                            " CASE COALESCE(MAX(Transaction_Info.REC_STATUS),99) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' END AS iACTION_STATUS, MAX(transaction_info.REC_EDIT_ON) AS iREC_EDIT_ON,MAX(transaction_info.REC_STATUS_ON) AS iREC_STATUS_ON, MAX(Transaction_Info.REC_ADD_BY) AS iREC_ADD_BY, MAX(Transaction_Info.REC_EDIT_BY) AS iREC_EDIT_BY,MAX(transaction_info.REC_STATUS_BY) AS iREC_STATUS_BY,MAX(TR_TRF_CROSS_REF_ID) AS iCross_Ref_ID, MAX(TR_REF_NO) AS iRef_no,REPLACE(MAX(ATTACH.CONCAT_ATTACH_IDs),' ','') Attachment_IDs,COALESCE(MAX(MAP.TOT_CNT),0) AS REQ_ATTACH_COUNT ,COALESCE(MAX(MAP.COMP_CNT),0) AS COMPLETE_ATTACH_COUNT,COALESCE(MAX(MAP.RESP_CNT),0) AS RESPONDED_COUNT,MAX(MAP.REJECT_CNT) AS REJECTED_COUNT,MAX(MAP.OTHER_ATTACH_CNT) AS OTHER_ATTACH_CNT "
            'OnlineQuery = OnlineQuery & GetAdvancedFilterQuery(Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openCenID)
            'OnlineQuery = OnlineQuery & " FROM  Transaction_Info " &
            '                            " LEFT OUTER JOIN address_book AS AB1 ON AB1.REC_ID = Transaction_Info.TR_AB_ID_1 AND AB1.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "" &
            '                            " LEFT OUTER JOIN centre_info AS CI ON CI.REC_ID = Transaction_Info.TR_AB_ID_1 " &
            '                            " LEFT OUTER JOIN item_info AS II ON II.REC_ID = TR_ITEM_ID " &
            '                            " LEFT OUTER JOIN acc_ledger_info AS AL ON AL.LED_ID = CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END " &
            '                            " Left OUTER JOIN ( Select ARI_OUTER.ALI_REF_REC_ID REC_ID, STUFF((SELECT DISTINCT ', ' + REC_ID + substring(AI_FILE_NAME, charindex('.',AI_FILE_NAME),len(AI_FILE_NAME)-charindex('.',AI_FILE_NAME)+1)  From attachment_reference_info ari INNER JOIN attachment_info AS AI ON ARI.ALI_ATTACHMENT_ID =AI.REC_ID WHERE ARI.ALI_REF_REC_ID = ari_OUTER.ALI_REF_REC_ID For XML PATH ('')) ,1,2,'') AS CONCAT_ATTACH_IDs From attachment_reference_info ari_OUTER inner Join transaction_info ti On coalesce(ti.tr_m_id, ti.rec_id) = ari_OUTER.ALI_REF_REC_ID And ti.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " And TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " Group BY ari_OUTER.ALI_REF_REC_ID) AS ATTACH ON coalesce(transaction_info.tr_m_id, transaction_info.rec_id) = ATTACH.REC_ID " &
            '                            " LEFT OUTER JOIN @ATTACHMENT_MAPPING AS MAP ON COALESCE(Transaction_Info.TR_M_ID, Transaction_Info.REC_ID) = MAP.ID " &
            '                            " WHERE Transaction_Info.REC_STATUS In (0,1,2) And TR_CEN_ID = " & inBasicParam.openCenID.ToString & " And TR_NOTEBOOK Is NULL " &
            '                            " And CAST(TR_DATE As Date) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
            '                            " AND TR_CODE NOT IN (18,19,20,21) " &
            '                            " GROUP BY CASE WHEN TR_M_ID IS NULL THEN Transaction_Info.REC_ID ELSE TR_M_ID END,  TR_ITEM_ID,  TR_SR_NO   " &
            '                            " " &
            '                            " UNION ALL " &
            '                            " " &
            '                            " SELECT MAX(TR_VNO) AS iTR_VNO,  TR_DATE AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  MAX(II.ITEM_NAME) AS iTR_ITEM,  MAX(CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END) AS iLED_ID,MAX(LED_NAME) AS iTR_HEAD, MAX(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  'Magazine Members' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME, " &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL, " &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-' +dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01'  END) AS iTR_DATE_SHOW, " &
            '                            " MIN(CASE WHEN TR_CODE IN (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 AND TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE IN (1,2,10) THEN 'B'ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE CASE WHEN TR_CODE=14 THEN 'E' ELSE 'C'END END END END END END) AS iTR_ENTRY, " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH   , " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_BANK   , " & Param.Online_Bank_Col_TR_Rec &
            '                            " SUM(CASE WHEN LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_REC_JOURNAL, " &
            '                            " SUM(CASE WHEN TR_DR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_DR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_REC_TOTAL  , " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH   , " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_BANK   , " & Param.Online_Bank_Col_TR_Pay &
            '                            " SUM(CASE WHEN LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END)                                       AS iTR_PAY_JOURNAL, " &
            '                            " SUM(CASE WHEN TR_CR_LED_ID IN ('00079', '00080') OR LEN(COALESCE(TR_CR_LED_ID,'')) = 0 THEN TR_AMOUNT ELSE NULL END) AS iTR_PAY_TOTAL  , " &
            '                            " MAX(TR_NARRATION) AS iTR_NARRATION,'B' AS iTR_ROW_POS,MAX(TR_TYPE) AS iTR_TYPE,MAX(TR_CODE) AS iTR_CODE,MAX(Transaction_Info.REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) AS iTR_SR_NO,MIN(CASE WHEN TR_CODE IN (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE IN (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B'END END) AS iTR_SORT, MAX(Transaction_Info.REC_ADD_ON) AS iREC_ADD_ON, " &
            '                            " MAX(CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE Transaction_Info.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
            '                            " CASE COALESCE(MAX(Transaction_Info.REC_STATUS),99) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' END AS iACTION_STATUS, MAX(transaction_info.REC_EDIT_ON) AS iREC_EDIT_ON,MAX(transaction_info.REC_STATUS_ON) AS iREC_STATUS_ON, MAX(Transaction_Info.REC_ADD_BY) AS iREC_ADD_BY, MAX(Transaction_Info.REC_EDIT_BY) AS iREC_EDIT_BY,MAX(transaction_info.REC_STATUS_BY) AS iREC_STATUS_BY,MAX(TR_TRF_CROSS_REF_ID) AS iCross_Ref_ID, MAX(TR_REF_NO) AS iRef_no ,REPLACE(MAX(ATTACH.CONCAT_ATTACH_IDs),' ','') Attachment_IDs,COALESCE(MAX(MAP.TOT_CNT),0) AS REQ_ATTACH_COUNT ,COALESCE(MAX(MAP.COMP_CNT),0) AS COMPLETE_ATTACH_COUNT,COALESCE(MAX(MAP.RESP_CNT),0) AS RESPONDED_COUNT,MAX(MAP.REJECT_CNT) AS REJECTED_COUNT,MAX(MAP.OTHER_ATTACH_CNT) AS OTHER_ATTACH_CNT  "
            'OnlineQuery = OnlineQuery & GetAdvancedFilterQuery(Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openCenID)
            'OnlineQuery = OnlineQuery & " FROM  Transaction_Info " &
            '                            " LEFT OUTER JOIN address_book AS AB1 ON AB1.REC_ID = Transaction_Info.TR_AB_ID_1 AND AB1.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "" &
            '                            " LEFT OUTER JOIN centre_info AS CI ON CI.REC_ID = Transaction_Info.TR_AB_ID_1 " &
            '                            " LEFT OUTER JOIN item_info AS II ON II.REC_ID = TR_ITEM_ID " &
            '                            " LEFT OUTER JOIN acc_ledger_info AS AL ON AL.LED_ID = CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END " &
            '                            " Left OUTER JOIN ( Select ARI_OUTER.ALI_REF_REC_ID REC_ID, STUFF((SELECT DISTINCT ', ' + REC_ID + substring(AI_FILE_NAME, charindex('.',AI_FILE_NAME),len(AI_FILE_NAME)-charindex('.',AI_FILE_NAME)+1)  From attachment_reference_info ari INNER JOIN attachment_info AS AI ON ARI.ALI_ATTACHMENT_ID =AI.REC_ID WHERE ARI.ALI_REF_REC_ID = ari_OUTER.ALI_REF_REC_ID For XML PATH ('')) ,1,2,'') AS CONCAT_ATTACH_IDs From attachment_reference_info ari_OUTER inner Join transaction_info ti On coalesce(ti.tr_m_id, ti.rec_id) = ari_OUTER.ALI_REF_REC_ID And ti.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " And TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " Group BY ari_OUTER.ALI_REF_REC_ID) AS ATTACH ON coalesce(transaction_info.tr_m_id, transaction_info.rec_id) = ATTACH.REC_ID " &
            '                            " LEFT OUTER JOIN @ATTACHMENT_MAPPING AS MAP ON COALESCE(Transaction_Info.TR_M_ID, Transaction_Info.REC_ID) = MAP.ID " &
            '                            " WHERE Transaction_Info.REC_STATUS IN (0,1,2) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL " &
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
            '                            " AND TR_CODE IN (18,19,20,21) " &
            '                            " GROUP BY tr_date,    TR_ITEM_ID  " &
            '                            " " &
            '                            " UNION ALL " &
            '                            " " &
            '                            " SELECT MAX(TR_VNO) AS iTR_VNO ,DATEADD(dd,-1,DATEADD(mm, DATEDIFF(mm,0,MAX(tr_Date))+1,0)), TR_ITEM_ID AS iTR_ITEM_ID,'Monthly '+ MAX(II.ITEM_NAME) AS iTR_ITEM,MAX(CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END) AS iLED_ID,MAX(LED_NAME) AS iTR_HEAD,NULL,NULL,CASE WHEN MAX(AB1.C_NAME) IS NOT NULL THEN MAX(AB1.C_NAME) + CASE WHEN MAX(AB1.C_NAME_DUP_ID) IS NOT NULL THEN '('+CAST(MAX(AB1.C_NAME_DUP_ID) AS VARCHAR)+')' ELSE '' END ELSE '' END AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL, " &
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-'+ dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END ) AS iTR_DATE_SHOW," &
            '                            " 'C'             AS iTR_ENTRY      ," &
            '                            " NULL            AS iTR_REC_CASH   ," &
            '                            " NULL            AS iTR_REC_BANK   ," & Param.Online_Bank_Col_NB_Rec &
            '                            " NULL            AS iTR_REC_JOURNAL," &
            '                            " NULL            AS iTR_REC_TOTAL  ," &
            '                            " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," &
            '                            " NULL            AS iTR_PAY_BANK   ," & Param.Online_Bank_Col_NB_Pay &
            '                            " NULL            AS iTR_PAY_JOURNAL," &
            '                            " SUM(TR_AMOUNT)  AS iTR_PAY_TOTAL  ," &
            '                            " 'Note-Book Entry' AS iTR_NARRATION, 'C' AS iTR_ROW_POS,MAX(TR_TYPE) AS iTR_TYPE, 3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL AS iTR_SR_NO,  NULL AS iTR_SORT,  MAX(Transaction_Info.REC_ADD_ON) AS iREC_ADD_ON," &
            '                            " MAX( CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE Transaction_Info.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO , " &
            '                            " CASE COALESCE(MAX(Transaction_Info.REC_STATUS),99) WHEN 99 THEN 'Incorrect' WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' END AS iACTION_STATUS, MAX(Transaction_Info.REC_EDIT_ON) AS iREC_EDIT_ON,MAX(transaction_info.REC_STATUS_ON) AS iREC_STATUS_ON, MAX(Transaction_Info.REC_ADD_BY) AS iREC_ADD_BY, MAX(Transaction_Info.REC_EDIT_BY) AS iREC_EDIT_BY,MAX(transaction_info.REC_STATUS_BY) AS iREC_STATUS_BY,MAX(TR_TRF_CROSS_REF_ID) AS iCross_Ref_ID , MAX(TR_REF_NO) AS iRef_no ,REPLACE(MAX(ATTACH.CONCAT_ATTACH_IDs),' ','') Attachment_IDs,0 AS REQ_ATTACH_COUNT ,0 AS COMPLETE_ATTACH_COUNT,0 AS RESPONDED_COUNT,0 AS REJECTED_COUNT,0 AS OTHER_ATTACH_CNT  "
            'OnlineQuery = OnlineQuery & GetAdvancedFilterQuery(Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, inBasicParam.openCenID)
            'OnlineQuery = OnlineQuery & " FROM    Transaction_Info  " &
            '                            " LEFT OUTER JOIN address_book AS AB1 ON AB1.REC_ID = Transaction_Info.TR_AB_ID_1  AND AB1.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " &
            '                            " LEFT OUTER JOIN item_info AS II ON II.REC_ID = TR_ITEM_ID  " &
            '                            " LEFT OUTER JOIN acc_ledger_info AS AL ON AL.LED_ID = CASE WHEN TR_TYPE = 'DEBIT' THEN TR_DR_LED_ID ELSE TR_CR_LED_ID END  " &
            '                            " Left OUTER JOIN ( Select ARI_OUTER.ALI_REF_REC_ID REC_ID, STUFF((SELECT DISTINCT ', ' + REC_ID + substring(AI_FILE_NAME, charindex('.',AI_FILE_NAME),len(AI_FILE_NAME)-charindex('.',AI_FILE_NAME)+1)  From attachment_reference_info ari INNER JOIN attachment_info AS AI ON ARI.ALI_ATTACHMENT_ID =AI.REC_ID WHERE ARI.ALI_REF_REC_ID = ari_OUTER.ALI_REF_REC_ID For XML PATH ('')) ,1,2,'') AS CONCAT_ATTACH_IDs From attachment_reference_info ari_OUTER inner Join transaction_info ti On coalesce(ti.tr_m_id, ti.rec_id) = ari_OUTER.ALI_REF_REC_ID And ti.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " And TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " Group BY ari_OUTER.ALI_REF_REC_ID) AS ATTACH ON coalesce(transaction_info.tr_m_id, transaction_info.rec_id) = ATTACH.REC_ID " &
            '                            " WHERE Transaction_Info.REC_STATUS IN (0,1,2)  AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND TR_NOTEBOOK = 'YES'  " &
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
            '                            " GROUP BY dbo.fn_FORMATDATE(tr_date,'yyyymm'),    TR_ITEM_ID  "

            'Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Vouchers_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Common.UseSQL() Then
                Return GetList_SQL(Param, inBasicParam)
            End If
            Dim OnlineQuery As String = " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(IF(TR_TYPE='DEBIT',TR_SUB_DR_LED_ID,TR_SUB_CR_LED_ID))  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(IF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, MONTH(TR_DATE)-3, MONTH(TR_DATE)+9)) AS iTR_DATE_SERIAL," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, CONCAT('" & Year(Param.openYearSdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ), CONCAT('" & Year(Param.openYearEdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ))) AS iTR_DATE_SHOW, " &
                                        " MIN( If(TR_CODE In (4,5,6,9,11) ,'A', if(TR_CODE = 8 and TR_TYPE='CREDIT','A', if(TR_CODE = 8 and TR_TYPE='DEBIT','C', If(TR_CODE In (1,2,10), 'B', If(TR_CODE=7,'D','C')))))) AS iTR_ENTRY," &
                                        " SUM(IF (TR_DR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_REC_CASH   ," &
                                        " SUM(IF (TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "',TR_AMOUNT,NULL))          AS iTR_REC_BANK   ," &
                                        " SUM(IF (TR_CR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_PAY_CASH   ," &
                                        " SUM(IF (TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "',TR_AMOUNT,NULL))          AS iTR_PAY_BANK   ," &
                                        " 'B' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,MAX(TR_CODE) AS iTR_CODE,MAX(REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) as iTR_SR_NO,MIN(If(TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' ,'A',If(TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT','A','B'))) as iTR_SORT, MAX(REC_ADD_ON) AS iREC_ADD_ON, " &
                                        " MAX( IF( LENGTH(IF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
                                        " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " &
                                        " FROM  Transaction_Info TI " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " &
                                        " WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL AND (TR_CODE NOT IN (12,13,16) OR TI.TR_MODE <> 'CASH') " &
                                        " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
                                        " AND LENGTH(IF(ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID)) <> 0 AND LENGTH(IF(ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID)) <> 0 " & Param.OtherCondition &
                                        " GROUP BY IF(ISNULL(TR_M_ID), REC_ID, TR_M_ID),  TR_ITEM_ID,  TR_SR_NO " &
                                        " HAVING  ( SUM(IF (TR_DR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 OR SUM(IF (TR_CR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 ) " &
                                        " " &
                                        " UNION ALL " &
                                        " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE,  MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(IF(TR_TYPE='DEBIT',TR_SUB_DR_LED_ID,TR_SUB_CR_LED_ID))  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(IF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, MONTH(TR_DATE)-3, MONTH(TR_DATE)+9)) AS iTR_DATE_SERIAL," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, CONCAT('" & Year(Param.openYearSdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ), CONCAT('" & Year(Param.openYearEdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ))) AS iTR_DATE_SHOW, " &
                                        " MIN( If(TR_CODE In (4,5,6,9,11) ,'A', if(TR_CODE = 8 and TR_TYPE='CREDIT','A', if(TR_CODE = 8 and TR_TYPE='DEBIT','C', If(TR_CODE In (1,2,10), 'B', If(TR_CODE=7,'D','C')))))) AS iTR_ENTRY," &
                                        " SUM(IF (TR_DR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_REC_CASH   ," &
                                        " SUM(IF (TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "',TR_AMOUNT,NULL))          AS iTR_REC_BANK   ," &
                                        " SUM(IF (TR_CR_LED_ID = '00080',TR_AMOUNT,NULL))                                                                      AS iTR_PAY_CASH   ," &
                                        " SUM(IF (TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "',TR_AMOUNT,NULL))          AS iTR_PAY_BANK   ," &
                                        " 'B' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,MAX(TR_CODE) AS iTR_CODE,MAX(REC_ID) AS iREC_ID,MAX(TR_M_ID) AS iTR_M_ID,MAX(TR_SR_NO) as iTR_SR_NO,MIN(If(TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' ,'A',If(TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT','A','B'))) as iTR_SORT, MAX(REC_ADD_ON) AS iREC_ADD_ON, " &
                                        " MAX( IF( LENGTH(IF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
                                        " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " &
                                        " FROM  Transaction_Info " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " &
                                        " WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL AND (TR_CODE IN (12,13,16) AND TI.TR_MODE = 'CASH') " &
                                        " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
                                        " AND LENGTH(IF(ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID)) <> 0 AND LENGTH(IF(ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID)) <> 0 " & Param.OtherCondition &
                                        " GROUP BY IF(ISNULL(TR_M_ID), REC_ID, TR_M_ID),  TR_ITEM_ID,  TR_SR_NO " &
                                        " HAVING  ( SUM(IF (TR_DR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 OR SUM(IF (TR_CR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 ) " &
                                        " " &
                                        " UNION ALL " &
                                        " " &
                                        " SELECT '',DATE_SUB(DATE_ADD(DATE_SUB(tr_date,INTERVAL DAYOFMONTH(tr_date)-1 DAY),INTERVAL 1 MONTH),INTERVAL 1 DAY), TR_ITEM_ID AS iTR_ITEM_ID,'' AS iTR_ITEM, Max(IF(TR_TYPE='DEBIT',TR_SUB_DR_LED_ID,TR_SUB_CR_LED_ID))  AS iTR_SUB_ID,NULL,'' AS iTR_PARTY_1,MAX(IF( LENGTH(IF(ISNULL(TR_SUB_CR_LED_ID),'',TR_SUB_CR_LED_ID)) > 0 ,TR_SUB_CR_LED_ID ,TR_SUB_DR_LED_ID )) AS iTR_CR_ID,'' AS iTR_CR_NAME," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, MONTH(TR_DATE)-3, MONTH(TR_DATE)+9)) AS iTR_DATE_SERIAL," &
                                        " MAX(IF(MONTH(TR_DATE) > 3, CONCAT('" & Year(Param.openYearSdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ), CONCAT('" & Year(Param.openYearEdt) & "-',LPAD(MONTH(TR_DATE),2,'0'),'-01' ))) AS iTR_DATE_SHOW, " &
                                        " 'C'             AS iTR_ENTRY      ," &
                                        " NULL            AS iTR_REC_CASH   ," &
                                        " NULL            AS iTR_REC_BANK   ," &
                                        " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," &
                                        " NULL            AS iTR_PAY_BANK   ," &
                                        " 'C' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL as iTR_SR_NO,  NULL as iTR_SORT,  MAX(REC_ADD_ON) AS iREC_ADD_ON,   " &
                                        " MAX( IF( LENGTH(IF(ISNULL(TR_M_ID),'',TR_M_ID)) > 0 , TR_M_ID, REC_ID )) AS iTR_TEMP_ID,0 AS iTR_REF_NO, " &
                                        " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " &
                                        " FROM    Transaction_Info  " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " &
                                        " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " &
                                        " WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND TR_NOTEBOOK = 'YES'  " &
                                        " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " &
                                        " AND LENGTH(IF(ISNULL(TR_CR_LED_ID),'',TR_CR_LED_ID)) <> 0 AND LENGTH(IF(ISNULL(TR_DR_LED_ID),'',TR_DR_LED_ID)) <> 0 " & Param.OtherCondition &
                                        " GROUP BY DATE_FORMAT(tr_date,'%Y%m'),    TR_ITEM_ID " &
                                        " HAVING  ( SUM(IF (TR_DR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 OR SUM(IF (TR_CR_LED_ID = '" & Param.Led_ID & "',TR_AMOUNT,0)) <> 0 ) "

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_SQL(ByVal Param As Param_Vouchers_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Dim OnlineQuery As String = " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE, MAX(TI.TR_REF_CDATE) AS iTR_REF_CDATE, MAX(TI.TR_REF_DATE) AS iTR_REF_DATE, MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  MAX(TR_AB_ID_1) AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '                            " MIN( CASE WHEN TR_CODE In (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE In (1,2,10) THEN 'B' ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE 'C' END END END END END) AS iTR_ENTRY," & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH ,  " & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_REC_BANK ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_PAY_BANK ," & _
            '                            " 'B' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,MAX(TR_CODE) AS iTR_CODE,MAX(TI.REC_ID) AS iREC_ID,MAX(TI.TR_M_ID) AS iTR_M_ID,MAX(TI.TR_SR_NO) as iTR_SR_NO,MIN(CASE WHEN TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B' END END) as iTR_SORT, MAX(TI.REC_ADD_ON) AS iREC_ADD_ON," & _
            '                            " MAX( CASE WHEN LEN(COALESCE(TI.TR_M_ID,'')) > 0 THEN TI.TR_M_ID ELSE TI.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO,CASE WHEN LEN (COALESCE(max(TI.TR_REF_NO),'')) >0 THEN max(TI.TR_REF_NO) ELSE '' END as Ref, max(TI.TR_MODE) as iTR_MODE,  " & _
            '                            " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " & _
            '                            " FROM  Transaction_Info  TI " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " & _
            '                            " WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL AND (TR_CODE NOT IN (12,13,16) OR TI.TR_MODE <> 'CASH') AND TR_CODE NOT IN (18,19,20,21) " & _
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " & _
            '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & Param.OtherCondition & _
            '                            " GROUP BY COALESCE(TI.TR_M_ID, TI.REC_ID),  TR_ITEM_ID,  TI.TR_SR_NO, COALESCE(TI.TR_REF_NO,'')  " & _
            '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END ) <> 0 )" & _
            '                            " " & _
            '                            " UNION ALL " & _
            '                            " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE, MAX(TI.TR_REF_CDATE)  AS iTR_REF_CDATE, MAX(TI.TR_REF_DATE) AS iTR_REF_DATE, MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,  NULL AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '                            " MIN( CASE WHEN TR_CODE In (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE In (1,2,10) THEN 'B' ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE 'C' END END END END END) AS iTR_ENTRY," & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH ,  " & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_REC_BANK ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_PAY_BANK ," & _
            '                            " 'B' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,MAX(TR_CODE) AS iTR_CODE,MAX(TI.REC_ID) AS iREC_ID,MAX(TI.TR_M_ID) AS iTR_M_ID,MAX(TI.TR_SR_NO) as iTR_SR_NO,MIN(CASE WHEN TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B' END END) as iTR_SORT, MAX(TI.REC_ADD_ON) AS iREC_ADD_ON," & _
            '                            " MAX( CASE WHEN LEN(COALESCE(TI.TR_M_ID,'')) > 0 THEN TI.TR_M_ID ELSE TI.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO,CASE WHEN LEN (COALESCE(max(TI.TR_REF_NO),'')) >0 THEN max(TI.TR_REF_NO) ELSE '' END as Ref, max(TI.TR_MODE) as iTR_MODE,  " & _
            '                            " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " & _
            '                            " FROM  Transaction_Info  TI " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " & _
            '                            " WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_NOTEBOOK IS NULL AND (TR_CODE IN (12,13,16) AND TI.TR_MODE = 'CASH') " & _
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " & _
            '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & Param.OtherCondition & _
            '                            " GROUP BY TR_DATE,  TR_CR_LED_ID, COALESCE(TI.TR_REF_NO,'') " & _
            '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END ) <> 0 )" & _
            '                            " " & _
            '                            " UNION ALL " & _
            '                            " SELECT MAX(TR_VNO) AS iTR_VNO,  MAX(TR_DATE) AS iTR_DATE, MAX(TI.TR_REF_CDATE) AS iTR_REF_CDATE, MAX(TI.TR_REF_DATE) AS iTR_REF_DATE, MAX(TR_ITEM_ID) AS iTR_ITEM_ID,  '' AS iTR_ITEM,  Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID, NULL AS iTR_AB_ID_1,  '' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' ELSE '" & Year(Param.openYearEdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '                            " MIN( CASE WHEN TR_CODE In (4,5,6,9,11) THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE = 8 and TR_TYPE='DEBIT' THEN 'C' ELSE CASE WHEN TR_CODE In (1,2,10) THEN 'B' ELSE CASE WHEN TR_CODE=7 THEN 'D' ELSE 'C' END END END END END) AS iTR_ENTRY," & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_REC_CASH ,  " & _
            '                            " SUM(CASE WHEN TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_REC_BANK ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END)                                                                      AS iTR_PAY_CASH ," & _
            '                            " SUM(CASE WHEN TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_PAY_BANK ," & _
            '                            " 'B' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,MAX(TR_CODE) AS iTR_CODE,MAX(TI.REC_ID) AS iREC_ID,MAX(TI.TR_M_ID) AS iTR_M_ID,MAX(TI.TR_SR_NO) as iTR_SR_NO,MIN(CASE WHEN TR_CODE In (1,2,4,5,6,7,8,9,10,11) AND TR_TYPE='CREDIT' THEN 'A' ELSE CASE WHEN TR_CODE In (1,2,3,8) AND TR_TYPE='DEBIT' THEN 'A' ELSE 'B' END END) as iTR_SORT, MAX(TI.REC_ADD_ON) AS iREC_ADD_ON," & _
            '                            " MAX( CASE WHEN LEN(COALESCE(TI.TR_M_ID,'')) > 0 THEN TI.TR_M_ID ELSE TI.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO,CASE WHEN LEN (COALESCE(max(TI.TR_REF_NO),'')) >0 THEN max(TI.TR_REF_NO) ELSE '' END as Ref, max(TI.TR_MODE) as iTR_MODE,  " & _
            '                            " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE DR_AL.LED_NAME END) AS iLEDGER " & _
            '                            " FROM  Transaction_Info  TI " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " & _
            '                            " WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (18,19,20,21) " & _
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " & _
            '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & Param.OtherCondition & _
            '                            " GROUP BY TR_DATE,  TR_ITEM_ID " & _
            '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END ) <> 0 )" & _
            '                            " " & _
            '                            " UNION ALL " & _
            '                            " " & _
            '                            " SELECT '',DATEADD(dd,-1,DATEADD(mm, DATEDIFF(mm,0,MAX(tr_Date))+1,0)),NULL AS iTR_REF_CDATE, NULL AS iTR_REF_DATE, TR_ITEM_ID AS iTR_ITEM_ID,'' AS iTR_ITEM, Max(CASE WHEN TR_TYPE='DEBIT' THEN TR_SUB_DR_LED_ID ELSE TR_SUB_CR_LED_ID END)  AS iTR_SUB_ID,NULL,'' AS iTR_PARTY_1,MAX(CASE WHEN LEN(COALESCE(TR_SUB_CR_LED_ID,'')) > 0 THEN TR_SUB_CR_LED_ID ELSE TR_SUB_DR_LED_ID END) AS iTR_CR_ID,'' AS iTR_CR_NAME, " & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN MONTH(TR_DATE)-3 ELSE MONTH(TR_DATE)+9 END) AS iTR_DATE_SERIAL," & _
            '                            " MAX(CASE WHEN MONTH(TR_DATE) > 3 THEN '" & Year(Param.openYearSdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01'  ELSE '" & Year(Param.openYearEdt) & "-'+dbo.LPAD(MONTH(TR_DATE),2,'0')+'-01' END) AS iTR_DATE_SHOW," & _
            '                            " 'C'             AS iTR_ENTRY      ," & _
            '                            " NULL            AS iTR_REC_CASH   ," & _
            '                            " NULL            AS iTR_REC_BANK   ," & _
            '                            " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," & _
            '                            " NULL            AS iTR_PAY_BANK   ," & _
            '                            " 'C' AS iTR_ROW_POS,MAX(TR_NARRATION) AS iTR_NARRATION,3 AS iTR_CODE, 'NOTE-BOOK' AS iREC_ID, NULL AS iTR_M_ID,  NULL as iTR_SR_NO,  NULL as iTR_SORT,  MAX(TI.REC_ADD_ON) AS iREC_ADD_ON,   " & _
            '                            " MAX( CASE WHEN LEN(COALESCE(TR_M_ID,'')) > 0 THEN TR_M_ID ELSE TI.REC_ID END) AS iTR_TEMP_ID,0 AS iTR_REF_NO,CASE WHEN LEN (COALESCE(max(TR_REF_NO),'')) >0 THEN max(TR_REF_NO) ELSE '' END as Ref, NULL as iTR_MODE, " & _
            '                            " MAX(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN CR_AL.LED_NAME ELSE CR_AL.LED_NAME END) AS iLEDGER " & _
            '                            " FROM    Transaction_Info TI " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO DR_AL ON TR_DR_LED_ID = DR_AL.LED_ID " & _
            '                            " LEFT OUTER JOIN ACC_LEDGER_INFO CR_AL ON TR_CR_LED_ID = CR_AL.LED_ID " & _
            '                            " WHERE TI.REC_STATUS IN (0,1,2)  AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND TR_NOTEBOOK = 'YES'  " & _
            '                            " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' " & _
            '                            " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & Param.OtherCondition & _
            '                            " GROUP BY dbo.fn_FORMATDATE(tr_date,'yyyymm'),    TR_ITEM_ID" & _
            '                            " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 )"

            'Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

            Dim SPName As String = "[sp_get_Daily_Balances]"
            Dim params() As String = {"@CENID", "@YEARID", "@userid", "@Bank_Acc_ID", "@Led_ID", "@FromDate", "@ToDate", "@OtherCondition"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, Param.Bank_Acc_ID, Param.Led_ID, Param.FromDate, Param.ToDate, Param.OtherCondition}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 36, 36, 12, 12, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)

        End Function

        Public Shared Function GetVoucherItemDetails(ByVal ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim params() As String = {"@CEN_ID", "@YEARID", "@ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36}
            'Dim OnlineQuery As String = " SELECT ITEM_NAME Item, MISC_NAME Purpose, ti.TR_AMOUNT Amount, TR_NARRATION, TR_REFERENCE FROM transaction_info TI INNER JOIN ITEM_INFO AS ITEM ON TI.TR_ITEM_ID = ITEM.REC_ID LEFT OUTER JOIN transaction_d_purpose_info AS TP ON COALESCE(TI.TR_M_ID,TI.REC_ID) = TP.TR_REC_ID AND COALESCE(TI.TR_SR_NO,0) = COALESCE(TP.TR_ITEM_SR_NO ,0) LEFT OUTER JOIN misc_info AS MI ON TP.TR_PURPOSE_MISC_ID = MI.REC_ID WHERE TI.TR_M_ID = '" + ID + "' OR TI.REC_ID = '" + ID + "'"
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "[sp_Get_Voucher_Item_Details]", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetBank_Reconciliation(ByVal inParam As Param_GetBank_Reconciliation, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " SELECT TI.TR_DATE AS 'Voucher Date', TI.TR_REF_CDATE AS 'Clearing Date', TI.TR_MODE AS 'Mode', CASE WHEN COALESCE(TR_SUB_CR_LED_ID,'') = '" & inParam.BankID & "' THEN 'Add: cheque(s) issued but not cleared' ELSE 'Less: cheque(s) received but not cleared' END as 'Status', " &
                                        " COALESCE(TI.TR_REF_NO, '') AS 'Ref No.', TI.TR_AMOUNT AS Amount " &
                                        " FROM Transaction_Info  TI  " &
                                        " WHERE TI.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " " &
                                        " AND TI.TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " &
                                        " AND ( (TR_DATE <= '" & inParam.DateofReconcile.ToString(_Server_Date_Format_Short) & "' AND (COALESCE(TI.TR_REF_CDATE,'') > '" & inParam.DateofReconcile.ToString(_Server_Date_Format_Short) & "' OR COALESCE(TI.TR_REF_CDATE,'') = '1900-01-01')) " &
                                        " OR (TR_DATE > '" & inParam.DateofReconcile.ToString(_Server_Date_Format_Short) & "' AND (COALESCE(TI.TR_REF_CDATE,'') <= '" & inParam.DateofReconcile.ToString(_Server_Date_Format_Short) & "' AND COALESCE(TI.TR_REF_CDATE,'') > '1900-01-01')) " &
                                        " ) " &
                                        " AND (COALESCE(TR_SUB_DR_LED_ID,'') = '" & inParam.BankID & "' OR  COALESCE(TR_SUB_CR_LED_ID,'') ='" & inParam.BankID & "' )" &
                                        "" &
                                        " AND  ( CASE WHEN TR_DR_LED_ID = '00079' THEN TR_AMOUNT ELSE 0 END <> 0 OR CASE WHEN TR_CR_LED_ID = '00079' THEN TR_AMOUNT ELSE 0 END  <> 0 )  " &
                                        " ORDER BY TI.TR_DATE "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Negative Balance
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetNegativeBalance</remarks>
        Public Shared Function GetNegativeBalance(ByVal Param As Param_Vouchers_GetNegativeBalance, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT 'B' AS iTR_SORT_REC,MAX(TR_DATE) AS iTR_DATE, " &
                                        " SUM(CASE WHEN TR_DR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END) AS iTR_REC_CASH   ," &
                                        " SUM(CASE WHEN TR_DR_LED_ID = '00079' and TR_SUB_DR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_REC_BANK   ," &
                                        " SUM(CASE WHEN TR_CR_LED_ID = '00080' THEN TR_AMOUNT ELSE NULL END) AS iTR_PAY_CASH   ," &
                                        " SUM(CASE WHEN TR_CR_LED_ID = '00079' and TR_SUB_CR_LED_ID='" & Param.Bank_Acc_ID & "' THEN TR_AMOUNT ELSE NULL END)          AS iTR_PAY_BANK    " &
                                        " FROM  Transaction_Info WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & inBasicParam.openCenID & "'  AND TR_COD_YEAR_ID ='" & inBasicParam.openYearID & "' AND TR_NOTEBOOK IS NULL " &
                                        " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0 " & Param.OtherCondition &
                                        " GROUP BY CAST(TR_DATE AS DATE) " &
                                        " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END ) <> 0 ) " &
                                        " " &
                                        " UNION ALL " &
                                        " " &
                                        " SELECT 'C' AS iTR_SORT_REC,DATEADD(DD,-1,DATEADD(MM,1,DATEADD(DD,-1 * (DAY(MAX(tr_date))-1),MAX(tr_date)))) AS iTR_DATE," &
                                        " NULL            AS iTR_REC_CASH   ," &
                                        " NULL            AS iTR_REC_BANK   ," &
                                        " SUM(TR_AMOUNT)  AS iTR_PAY_CASH   ," &
                                        " NULL            AS iTR_PAY_BANK    " &
                                        " FROM  Transaction_Info WHERE REC_STATUS IN (0,1,2)  AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "  AND TR_NOTEBOOK = 'YES'  " &
                                        " AND LEN(COALESCE(TR_CR_LED_ID,'')) <> 0 AND LEN(COALESCE(TR_DR_LED_ID,'')) <> 0  " & Param.OtherCondition &
                                        " GROUP BY CAST(TR_DATE AS DATE) " &
                                        " HAVING  ( SUM(CASE WHEN TR_DR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 OR SUM(CASE WHEN TR_CR_LED_ID = '" & Param.Led_ID & "' THEN TR_AMOUNT ELSE 0 END) <> 0 ) "

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Status
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetDonationStatus</remarks>
        Public Shared Function GetDonationStatus(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " Select DS_STATUS_MISC_ID,DS_TR_ID   From Donation_Status_Info  Where  REC_STATUS IN (0,1,2) AND DS_CEN_ID=" & inBasicParam.openCenID.ToString & "  "
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, Query, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Status With RecId filter
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetDonationStatusWithRecID</remarks>
        Public Shared Function GetDonationStatus(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COALESCE(DS_STATUS_MISC_ID,'') AS DS_STATUS FROM Donation_Status_Info  WHERE REC_STATUS IN (0,1,2) AND DS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND DS_TR_ID  = '" & RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Status TrCode
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetStatus_TrCode</remarks>
        Public Shared Function GetStatus_TrCode(ByVal Param As Param_GetStatusTrCode, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT ti.REC_STATUS,TR_CODE,COALESCE(TR_TRF_CROSS_REF_ID,'') TR_TRF_CROSS_REF_ID FROM TRANSACTION_INFO as ti LEFT OUTER JOIN item_info AS II ON TR_ITEM_ID = II.REC_ID  WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND (ti.REC_ID  = '" & Param.Tr_RecID & "' OR TR_M_ID = '" & Param.Tr_MID & "') AND (TR_CODE <> 10 OR (TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' OR '65730a27-e365-4195-853e-2f59225fe8f4' NOT IN (SELECT TR_ITEM_ID FROM TRANSACTION_INFO WHERE (REC_ID  = '" & Param.Tr_RecID & "' OR TR_M_ID = '" & Param.Tr_MID & "'))) ) AND (TR_TRF_CROSS_REF_ID is NOT NULL OR TR_CODE <> 17) ORDER BY ITEM_PROFILE" 'Bug #5153 fixed
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' GetStatus_TrCode_OtherCentre
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetStatus_TrCode_OtherCentre</remarks>
        Public Shared Function GetStatus_TrCode_OtherCentre(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_STATUS,TR_CODE,TR_TRF_CROSS_REF_ID FROM TRANSACTION_INFO  WHERE REC_STATUS IN (0,1,2) AND REC_ID  = '" & RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetPurpose(ByVal Param As CoreFunctions.Param_GetMisc, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT MISC_NAME as " & Param.MiscNameColumnHead & ",REC_ID as " & Param.RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN ('GODLY SERVICE PROJECTS') UNION ALL SELECT CPR_NAME as " & Param.MiscNameColumnHead & ",REC_ID as " & Param.RecIDColumnHead & " FROM center_purpose_info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CPR_ACTIVE = 1 AND CPR_CEN_ID = " & inBasicParam.openCenID & " order by misc_name "
            If inBasicParam.screen = ConnectOneWS.ClientScreen.Account_CashbookAuditor Then
                SQL_STR = " SELECT MISC_NAME as " & Param.MiscNameColumnHead & ",REC_ID as " & Param.RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN ('GODLY SERVICE PROJECTS') UNION ALL SELECT CPR_NAME as " & Param.MiscNameColumnHead & ",REC_ID as " & Param.RecIDColumnHead & " FROM center_purpose_info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CPR_ACTIVE = 1 order by misc_name "
            End If
            Return dbService.List(ConnectOneWS.Tables.MISC_INFO, SQL_STR, ConnectOneWS.Tables.MISC_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transaction Detail
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vouchers_GetTransactionDetail</remarks>
        Public Shared Function GetTransactionDetail(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT   TR_CODE,TR_MODE,TR_SUB_CR_LED_ID, TR_SUB_DR_LED_ID FROM Transaction_Info " &
                                    " Where   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND (REC_ID='" & RecID & "' OR TR_M_ID='" & RecID & "')"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAdvancedFilters(Param As Param_GetAdvancedFilters, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Advanced_filter_Listing"
            Dim params() As String = {"CENID", "YEARID", "ASSET_PROFILE", "PREV_YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Asset_Profile, Param.Prev_YearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 36, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetBankClearingData(ByVal MasterID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select BI.BI_BANK_NAME AS Bank, BBR.BB_BRANCH_NAME AS Branch,BA.BA_ACCOUNT_NO AS 'Acc No.',TR_REF_AMT as Amount, TR_MODE as Mode, TR_REF_NO as 'Ref No.', TR_REF_DATE as 'Ref Date', TR_REF_CDATE as 'Clearing Date', MT_BI.BI_BANK_NAME AS 'Money Transfer Bank' , TR_MT_ACC_NO as 'Money Transfer A/c', TR_MT_BANK_ID, TR_SR_NO, TR_REF_ID from TRANSACTION_D_PAYMENT_INFO TP INNER JOIN bank_account_info AS BA ON TR_REF_ID = BA.REC_ID INNER JOIN bank_branch_info AS BBR ON BA.BA_BRANCH_ID = BBR.REC_ID INNER JOIN bank_info AS BI ON BBR.BI_BANK_ID = BI.REC_ID LEFT OUTER JOIN bank_info AS MT_BI ON TR_MT_BANK_ID = MT_BI.REC_ID where  TP.REC_STATUS IN (0,1,2) AND TR_M_ID= '" & MasterID & "' AND TR_PAY_TYPE='BANK' " &
            " union all " &
            " select BI.BI_BANK_NAME AS Bank, BBR.BB_BRANCH_NAME AS Branch,BA.BA_ACCOUNT_NO AS ACC_NO,TR_AMOUNT as Amount, TR_MODE as Mode, TR_REF_NO as RefNo, TR_REF_DATE as RefDate, TR_REF_CDATE as ClearingDate,MT_BI.BI_BANK_NAME AS MT_BANK_NAME , TR_MT_ACC_NO, TR_MT_BANK_ID, TR_SR_NO, TR_sub_dr_led_id TR_REF_ID from TRANSACTION_INFO Ti INNER JOIN bank_account_info AS BA ON TR_sub_dr_led_id = BA.REC_ID INNER JOIN bank_branch_info AS BBR ON BA.BA_BRANCH_ID = BBR.REC_ID INNER JOIN bank_info AS BI ON BBR.BI_BANK_ID = BI.REC_ID LEFT OUTER JOIN bank_info AS MT_BI ON TR_MT_BANK_ID = MT_BI.REC_ID where  Ti.REC_STATUS IN (0,1,2) AND TR_M_ID= '" & MasterID & "' AND TR_mode not in ('CASH')" &
            " union all " &
            " select BI.BI_BANK_NAME AS Bank, BBR.BB_BRANCH_NAME AS Branch,BA.BA_ACCOUNT_NO AS ACC_NO,TR_AMOUNT as Amount, TR_MODE as Mode, TR_REF_NO as RefNo, TR_REF_DATE as RefDate, TR_REF_CDATE as ClearingDate,MT_BI.BI_BANK_NAME AS MT_BANK_NAME , TR_MT_ACC_NO, TR_MT_BANK_ID, TR_SR_NO, TR_sub_dr_led_id TR_REF_ID from TRANSACTION_INFO Ti INNER JOIN bank_account_info AS BA ON TR_sub_dr_led_id = BA.REC_ID INNER JOIN bank_branch_info AS BBR ON BA.BA_BRANCH_ID = BBR.REC_ID INNER JOIN bank_info AS BI ON BBR.BI_BANK_ID = BI.REC_ID LEFT OUTER JOIN bank_info AS MT_BI ON TR_MT_BANK_ID = MT_BI.REC_ID where  Ti.REC_STATUS IN (0,1,2) AND TI.REC_ID= '" & MasterID & "' AND TR_mode not in ('CASH') ORDER BY TR_SR_NO "

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetBankEntriesCountInNextEvent(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select MAX(eve.HE_TNXS_TO) as Txn_max from Centre_Events ce inner join so_ho_event_info as eve on ce.EVENTID = eve.HE_EVENT_ID where CEN_ID =" & inBasicParam.openCenID.ToString & ""
            Dim AuditAssignedPeriodMaxTxnDate As DateTime = Date.MinValue
            AuditAssignedPeriodMaxTxnDate = dbService.GetScalar(ConnectOneWS.Tables.SO_HO_EVENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

            Dim Query As String = " SELECT BA_ACCOUNT_NO,BI_SHORT_NAME,'" & AuditAssignedPeriodMaxTxnDate.ToString(Common.Server_Date_Format_Short) & "' as Txn_To_Date FROM bank_account_info AS BA " &
               " LEFT OUTER JOIN " &
               " (" &
               "     SELECT BA_ID, SUM(CNT) CNT FROM (" &
               "     SELECT TR_SUB_DR_LED_ID AS BA_ID, COUNT(TR_SUB_DR_LED_ID) AS CNT FROM TRANSACTION_INFO WHERE TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND TR_DATE > '" & AuditAssignedPeriodMaxTxnDate.ToString(Common.Server_Date_Format_Short) & "' GROUP BY TR_SUB_DR_LED_ID" &
               "  UNION ALL" &
               "     SELECT TR_SUB_CR_LED_ID AS BA_ID, COUNT(TR_SUB_CR_LED_ID) FROM TRANSACTION_INFO WHERE TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND TR_DATE > '" & AuditAssignedPeriodMaxTxnDate.ToString(Common.Server_Date_Format_Short) & "' GROUP BY TR_SUB_CR_LED_ID) AS B group by ba_id " &
               " ) AS A ON BA.REC_ID = A.BA_ID " &
               " LEFT OUTER JOIN bank_branch_info AS BR ON BA.BA_BRANCH_ID = BR.REC_ID " &
               " LEFT OUTER JOIN bank_info AS BI ON BR.BI_BANK_ID = BI.REC_ID " &
               " WHERE COALESCE(A.CNT,0) = 0 AND BA_CEN_ID = " & inBasicParam.openCenID.ToString & " AND BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND BA.BA_CLOSE_DATE IS NULL AND BA.BA_ACCOUNT_TYPE='SAVING' AND BA.REC_STATUS IN (0,1,2) AND BA_FERA_ACC = 'NO' AND COALESCE(BA_FCRA_UTIL,0) = 0 "

            '" LEFT OUTER JOIN so_center_audit_stats AS CAS ON CAS.CAS_CEN_ID = BA_CEN_ID AND CAS.CAS_STATUS =1 " & _
            '" AND CAS_EVENT_ID IN (SELECT HE_EVENT_ID FROM so_ho_event_info WHERE HE_COD_YEAR_ID = '" & inBasicParam.openYearID & "' AND HE_CATEGORY='CONTINUOUS AUDIT')  " & _
            '" AND CAS_AUDIT_STATUS_ID IN ('00351e0d-d326-4cd0-b1f1-ba42752b2242','0685db2b-26ae-40ec-85f8-5d43488c02b4','5a0a96ab-c127-4df2-9843-0082f452ecf9','43a34be0-127b-4f92-bd52-65f0e983e517') " & _

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetBankEntriesCountInNextEvent(ToDate As DateTime, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = "select MAX(eve.HE_TNXS_TO) as Txn_max from Centre_Events ce inner join so_ho_event_info as eve on ce.EVENTID = eve.HE_EVENT_ID where CEN_ID =" & inBasicParam.openCenID.ToString & ""
            'Dim AuditAssignedPeriodMaxTxnDate As String = Date.MinValue
            'AuditAssignedPeriodMaxTxnDate = dbService.GetScalar(ConnectOneWS.Tables.SO_HO_EVENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

            Dim Query As String = " SELECT BA_ACCOUNT_NO,BI_SHORT_NAME FROM bank_account_info AS BA " &
               " LEFT OUTER JOIN " &
               " (" &
               "     SELECT BA.BA_ORG_REC_ID AS BA_ID, SUM(CNT) CNT FROM (" &
               "     SELECT TR_SUB_DR_LED_ID AS BA_ID, COUNT(TR_SUB_DR_LED_ID) AS CNT FROM TRANSACTION_INFO WHERE TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND TR_DATE > '" & ToDate & "' GROUP BY TR_SUB_DR_LED_ID" &
               "  UNION ALL" &
               "     SELECT TR_SUB_CR_LED_ID AS BA_ID, COUNT(TR_SUB_CR_LED_ID) FROM TRANSACTION_INFO WHERE TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND TR_DATE > '" & ToDate & "' GROUP BY TR_SUB_CR_LED_ID) AS B INNER JOIN bank_account_info AS BA ON BA_ID = BA.REC_ID group by BA.BA_ORG_REC_ID " &
               " ) AS A ON BA.BA_ORG_REC_ID = A.BA_ID AND BA.BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " &
               " LEFT OUTER JOIN bank_branch_info AS BR ON BA.BA_BRANCH_ID = BR.REC_ID " &
               " LEFT OUTER JOIN bank_info AS BI ON BR.BI_BANK_ID = BI.REC_ID " &
               " WHERE COALESCE(A.CNT,0) = 0 AND BA_CEN_ID = " & inBasicParam.openCenID.ToString & " AND BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND BA.BA_CLOSE_DATE IS NULL AND BA.BA_ACCOUNT_TYPE='SAVING' AND BA.BA_FCRA_UTIL = 0 AND BA.REC_STATUS IN (0,1,2)"

            '" LEFT OUTER JOIN so_center_audit_stats AS CAS ON CAS.CAS_CEN_ID = BA_CEN_ID AND CAS.CAS_STATUS =1 " & _
            '" AND CAS_EVENT_ID IN (SELECT HE_EVENT_ID FROM so_ho_event_info WHERE HE_COD_YEAR_ID = '" & inBasicParam.openYearID & "' AND HE_CATEGORY='CONTINUOUS AUDIT')  " & _
            '" AND CAS_AUDIT_STATUS_ID IN ('00351e0d-d326-4cd0-b1f1-ba42752b2242','0685db2b-26ae-40ec-85f8-5d43488c02b4','5a0a96ab-c127-4df2-9843-0082f452ecf9','43a34be0-127b-4f92-bd52-65f0e983e517') " & _

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetDraftEntryList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Store Procedure:  CALL Get_Cash_Balance_Summary('2011-04-01','2012-03-31','2011-04-01','00207','1112','00001')
            Dim SPName As String = "sp_get_DraftEntryRegister"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@USER_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetDraftEntryCount(inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select COUNT(DISTINCT COALESCE(TR_M_ID,REC_ID)) as Txn_max from TRANSACTION_INFO where TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND  TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND REC_STATUS = 0"
            Dim AuditAssignedPeriodMaxTxnDate As String = Date.MinValue
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTDS_Mapping(TR_M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select TR_M_ID, REF_TR_M_ID from transaction_d_tds_info where TR_M_ID ='" & TR_M_ID & "' OR REF_TR_M_ID='" & TR_M_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_TDS_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_TDS_INFO.ToString(), inBasicParam)
        End Function


        Public Shared Function ConfirmDraftEntry(TXN_REC_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_ConfirmDraftEntry"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@USER_ID", "@TXN_REC_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, TXN_REC_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 36}
            dbService.UpdateBySP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function DeleteDraftEntry(TXN_REC_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_DeleteDraftEntry"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@USER_ID", "@TXN_REC_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, TXN_REC_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 36}
            dbService.DeleteFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function RejectDraftEntry(inparam As Param_RejectDraftEntry, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = ""
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@USER_ID", "@TXN_REC_ID", "@REMARKS"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, inparam.TXN_REC_ID, inparam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 36, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function GetEntryDetails(REC_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "Select TR_M_ID, TR_DATE, REC_ADD_ON, REC_EDIT_ON, Case Case When REC_STATUS Is NULL Then 99 Else REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End 'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info WHERE REC_ID = '" + REC_ID + "'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetProfileEntryDetails(InParam As Param_GetProfileEntryDetails, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            Select Case InParam.Profile_Used
                Case ConnectOneWS.AssetProfiles.ADVANCES
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID, TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN ADVANCES_INFO AS AI ON TR_M_ID = AI_TR_ID AND TR_SR_NO = AI_TR_SR_NO WHERE AI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.FD
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID, TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN FD_INFO AS FD ON TR_M_ID = FD_TR_ID WHERE FD.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.GOLD, ConnectOneWS.AssetProfiles.SILVER
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN GOLD_SILVER_INFO AS GS ON TR_M_ID = GS_TR_ID AND TR_SR_NO = gs_TR_ITEM_SRNO WHERE GS.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.LAND_BUILDING
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN LAND_BUILDING_INFO AS LB ON TR_M_ID = LB_TR_ID AND TR_SR_NO = LB_TR_ITEM_SRNO WHERE LB.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.LIVESTOCK
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN LIVE_STOCK_INFO AS LS ON TR_M_ID = LS_TR_ID AND TR_SR_NO = LS_TR_ITEM_SRNO WHERE LS.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.OTHER_ASSETS
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN ASSET_INFO AS AI ON TR_M_ID = AI_TR_ID AND TR_SR_NO = AI_TR_ITEM_SRNO WHERE AI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.OTHER_DEPOSITS
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN DEPOSITS_INFO AS DI ON TR_M_ID = DI_TR_ID AND TR_SR_NO = DI_TR_SR_NO WHERE DI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.OTHER_LIABILITIES
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN LIABILITIES_INFO AS LI ON TR_M_ID = LI_TR_ID AND TR_SR_NO = LI_TR_SR_NO WHERE LI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.VEHICLES
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN VEHICLES_INFO AS VI ON TR_M_ID = VI_TR_ID AND TR_SR_NO = VI_TR_ITEM_SRNO WHERE VI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
                Case ConnectOneWS.AssetProfiles.WIP
                    OnlineQuery = "Select TI.REC_ID TXN_REC_ID,  TR_M_ID, TR_DATE, TI.REC_ADD_ON, TI.REC_EDIT_ON, Case Case When TI.REC_STATUS Is NULL Then 99 Else TI.REC_STATUS End When 99 Then 'Incorrect'  WHEN -1 THEN 'Deleted' WHEN 0 THEN 'Incomplete' WHEN 1 THEN 'Completed' WHEN 2 THEN 'Locked' End  'ACTION STATUS',TR_ITEM_ID, TR_SR_NO, TR_TYPE,TR_CODE FROM transaction_info TI INNER JOIN WIP_INFO AS WI ON TR_M_ID = WIP_TR_ID AND TR_SR_NO = WIP_TR_ITEM_SRNO WHERE WI.REC_ID = '" + InParam.ProfileRecID + "' AND TR_M_ID ='" + InParam.TR_M_ID + "'"
            End Select

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function UpdateCommonDetails_Txn(upParam As Param_UpdateCommonDetails_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String
            If upParam.Purpose_ID.Length > 0 Then
                OnlineQuery = " UPDATE transaction_d_purpose_info set TR_PURPOSE_MISC_ID = '" & upParam.Purpose_ID & "' WHERE TR_REC_ID = '" & upParam.TR_M_ID & "'"
                dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)

                OnlineQuery = " UPDATE transaction_d_purpose_info set TR_PURPOSE_MISC_ID = '" & upParam.Purpose_ID & "' WHERE TR_REC_ID = '" & upParam.ID & "'"
                dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)

                OnlineQuery = "SELECT COUNT(*) FROM transaction_d_purpose_info WHERE TR_REC_ID =  '" & upParam.TR_M_ID & "' OR TR_REC_ID = '" & upParam.ID & "'"
                Dim PurposeCount As Int32 = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
                If PurposeCount = 0 Then
                    Dim ID As String = upParam.TR_M_ID
                    If String.IsNullOrWhiteSpace(ID) = True Then
                        ID = upParam.ID
                    End If
                    OnlineQuery = " TR_REC_ID = '" & ID & "'" & " OR TR_REC_ID = '" & upParam.ID & "'"
                    dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)

                    Dim RecID As String = Guid.NewGuid().ToString()
                    OnlineQuery = " INSERT INTO [dbo].[transaction_d_purpose_info] ([TR_CEN_ID],[TR_COD_YEAR_ID],[TR_REC_ID],[TR_PURPOSE_MISC_ID],[TR_AMOUNT], [REC_ADD_ON],[REC_ADD_BY],[REC_EDIT_ON],[REC_EDIT_BY],[REC_STATUS],[REC_STATUS_ON],[REC_STATUS_BY],[REC_ID],[TR_ITEM_SR_NO]) VALUES " &
                                       "(" & inBasicParam.openCenID.ToString() & "," & inBasicParam.openYearID.ToString() & ",'" & ID & "','" & upParam.Purpose_ID & "' " &
                                       ",(SELECT SUM(TR_AMOUNT) FROM transaction_info WHERE (TR_M_ID = '" & ID & "' OR REC_ID = '" & ID & "') AND TR_CR_LED_ID IS NOT NULL) " &
                                       ",GETDATE(),'" & inBasicParam.openUserID & "',GETDATE(),'" & inBasicParam.openUserID & "',1,GETDATE(),'" & inBasicParam.openUserID & "','" & RecID & "',NULL)"
                    dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, RecID)
                End If

            End If
            OnlineQuery = "UPDATE TRANSACTION_INFO SET TR_NARRATION = '" & upParam.Narration & "' , TR_REFERENCE ='" & upParam.Reference & "' WHERE TR_M_ID = '" & upParam.TR_M_ID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam)

            OnlineQuery = "UPDATE TRANSACTION_INFO SET TR_NARRATION = '" & upParam.Narration & "' , TR_REFERENCE ='" & upParam.Reference & "' WHERE REC_ID = '" & upParam.ID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam)

            'Special Voucher References (FCRA)
            Dim SpecialReferenceDeleteCondition = ""
            If (String.IsNullOrWhiteSpace(upParam.TR_M_ID) = False) Then
                SpecialReferenceDeleteCondition = "TR_M_ID='" & upParam.TR_M_ID & "'"
            Else
                SpecialReferenceDeleteCondition = "TXN_REC_ID='" & upParam.ID & "'"
            End If
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, SpecialReferenceDeleteCondition, inBasicParam)
            InsertSpecialVoucherReference(upParam.InsertSplVchrRefs, upParam.TR_M_ID, upParam.ID, Nothing, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertSpecialVoucherReference(Param As Parameter_InsertSplVchrRef_Vouchers(), ByVal MID As String, ByVal XID As String, Amount? As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'"
            Dim MidXid_Str = " NULL, '" & XID & "', "
            If (String.IsNullOrWhiteSpace(MID) = False) Then
                MidXid_Str = " '" & MID & "', NULL, "
            End If
            Dim Amount_Str = Amount.ToString()
            If Amount Is Nothing Then
                Amount_Str = "NULL"
            End If
            If Param IsNot Nothing Then
                If Param.Length > 0 Then
                    For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In Param
                        If Not Svr_Param Is Nothing Then
                            Dim RECID As String = Guid.NewGuid.ToString()
                            Dim OnlineQuery = "INSERT INTO transaction_d_reference_info(TR_CEN_ID, TR_COD_YEAR_ID, TR_M_ID, TXN_REC_ID, TR_SR_NO, " &
                                                                  "TR_VOUCHER_REF, TR_AMOUNT, REC_ID, REC_ADD_ON, REC_ADD_BY)" &
                                                                  " VALUES(" &
                                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                                  MidXid_Str &
                                                                  " NULL, " &
                                                                  "'" & Svr_Param.Task_Name & "', " &
                                                                  " " & Amount_Str & ", '" & RECID & "', " & Str & ")"
                            'MsgBox(OnlineQuery)
                            'Console.WriteLine(OnlineQuery)
                            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, OnlineQuery, inBasicParam, RECID)
                        End If
                    Next
                End If
            End If
            Return True
        End Function
        ''' <summary>
        ''' Get Txn Items, Shifted
        ''' </summary>
        ''' <param name="CEN_ID"></param>
        ''' <param name="TASK_Type"></param>
        ''' <param name="TASK_COD_YEAR_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetTxnItems</remarks>
        Public Shared Function GetSplVoucherRefsFromCenterTasks(Param As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TASK_NAME, REC_ID, PERMISSION, CEN_ID from CENTRE_TASK_INFO" &
                                    " where " &
                                        "COALESCE(CEN_ID," & Param.openCenID & ") = " & Param.openCenID & " and " &
                                        "TASK_TYPE = 'Special Voucher Reference' and " &
                                        "TASK_COD_YEAR_ID = " & Param.openYearID & "order by PERMISSION, TASK_NAME"
            'MsgBox(Query)
            Return dbService.List(ConnectOneWS.Tables.CENTRE_TASK_INFO, Query, ConnectOneWS.Tables.CENTRE_TASK_INFO.ToString(), Param)
        End Function

        <Serializable>
        Public Class Parameter_InsertSplVchrRef_Vouchers
            Public Task_Name As String
            Public ChkStatus As Boolean
        End Class
    End Class
End Namespace
