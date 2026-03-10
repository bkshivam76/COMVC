Imports System
Imports System.Data
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class Receipts
#Region "Param Classes"
        <Serializable>
        Public Class Param_Receipts_GetAdvancesRefundList
            Public PartyIDs As String
            Public IsNew As Boolean
            Public TxnMID As String
            Public YearID As Integer
            Public NextUnAuditedYearID As Integer
        End Class
        <Serializable>
        Public Class Param_Receipts_GetDepositsRefundList
            Public PartyIDs As String
            Public IsNew As Boolean
            Public TxnMID As String
            Public YearID As Integer
            Public NextUnauditedYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherReceipt
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Advance As Double
            Public Liability As Double
            Public Credit As Double
            Public TDS As Double
            Public ReceiptType As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherReceipt
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public Ref_Bank As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public RefDate As String
            Public RefCDate As String
            Public Amount As Double
            Public PartyID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public Tr_M_ID As String
            Public TxnSrNo As Integer
            Public Cross_Ref_ID As String
            Public Status_Action As String
            Public RecID As String
            ' Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherReceipt
            Public Txn_M_ID As String
            Public TxnSrNo As Integer
            Public ItemID As String
            Public LedID As String
            Public Type As String
            Public PartyReq As String
            Public Profile As String
            Public ItemName As String
            Public Head As String
            Public Qty As Double
            Public Unit As String
            Public Rate As Double
            Public Amount As Double
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherReceipt
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
            Public SrNo As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertSlip_VoucherReceipt
            Public TxnID As String
            Public SlipNo As Integer
            Public RecID As String
            Public Dep_BA_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertAandLPayment_VoucherReceipt
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public RefID As String
            Public RefAmount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertBankPayment_VoucherReceipt
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public Mode As String
            Public RefID As String
            Public RefNo As String
            Public RefDate As String
            Public RefCDate As String
            Public RefAmount As Double
            Public BankID As String
            Public AccountNo As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_VoucherReceipt
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public ReceiptType As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherReceipt
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherReceipt
            Public param_Insert As Parameter_Insert_VoucherReceipt = Nothing
            Public param_InsertCreditJV As Parameter_Insert_VoucherReceipt = Nothing
            Public param_InsertDebitJV As Parameter_Insert_VoucherReceipt = Nothing
            Public InsertLiability() As Liabilities.Parameter_InsertTRID_Liabilities = Nothing
            Public param_InPmtAdjusted As Parameter_InsertAandLPayment_VoucherReceipt = Nothing
            Public param_InPmtRefund As Parameter_InsertAandLPayment_VoucherReceipt = Nothing
            Public param_InsertPurposedr As Parameter_InsertPurpose_VoucherReceipt = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherReceipt = Nothing
            Public param_InsertSlip As Parameter_InsertSlip_VoucherReceipt = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherReceipt
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherReceipt
            Public MID_DeletePurpose As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteSlip As String = Nothing
            Public param_Insert As Parameter_Insert_VoucherReceipt = Nothing
            Public param_InsertCreditJV As Parameter_Insert_VoucherReceipt = Nothing
            Public param_InsertDebitJV As Parameter_Insert_VoucherReceipt = Nothing
            Public param_InPmtAdjusted As Parameter_InsertAandLPayment_VoucherReceipt = Nothing
            Public param_InPmtRefund As Parameter_InsertAandLPayment_VoucherReceipt = Nothing
            Public param_InsertPurposedr As Parameter_InsertPurpose_VoucherReceipt = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherReceipt = Nothing
            Public param_InsertSlip As Parameter_InsertSlip_VoucherReceipt = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherReceipt
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeleteSlip As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class

#End Region
        ''' <summary>
        ''' Get MasterID
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetMasterID</remarks>
        Public Shared Function GetMasterID(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_M_ID FROM transaction_info WHERE REC_ID ='" & Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Txn Items
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetTxnItems</remarks>
        Public Shared Function GetTxnItems(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " Select I.*,P.TR_PURPOSE_MISC_ID  AS  PUR_ID,'' AS LOC_ID," & _
                                 " '' AS GS_DESC_MISC_ID,0 AS GS_ITEM_WEIGHT,  " & _
                                 " '' AS AI_MAKE,'' AS AI_MODEL,'' AS AI_SERIAL_NO,0 AS AI_WARRANTY, '' AS AI_PUR_DATE ,   " & _
                                 " '' AS LS_NAME,'' AS LS_BIRTH_YEAR,'' AS LS_INSURANCE,'' AS LS_INSURANCE_ID, '' AS LS_INS_POLICY_NO, 0 AS LS_INS_AMT, '' AS LS_INS_DATE ,   " & _
                                 " '' AS VI_MAKE, '' AS VI_MODEL, '' AS VI_REG_NO_PATTERN, '' AS VI_REG_NO, '' AS VI_REG_DATE, '' AS VI_OWNERSHIP, '' AS VI_OWNERSHIP_AB_ID, '' AS VI_DOC_RC_BOOK, '' AS VI_DOC_AFFIDAVIT, '' AS VI_DOC_WILL, '' AS VI_DOC_TRF_LETTER, '' AS VI_DOC_FU_LETTER, '' AS VI_DOC_OTHERS, '' AS VI_DOC_NAME, '' AS VI_INSURANCE_ID, '' AS VI_INS_POLICY_NO, '' AS VI_INS_EXPIRY_DATE " & _
                                 " FROM Transaction_D_Item_Info AS I  INNER  JOIN Transaction_D_Purpose_Info AS P ON (P.TR_REC_ID = I.TR_M_ID) AND (P.TR_ITEM_SR_NO = I.TR_SR_NO)" & _
                                 " Where I.REC_STATUS IN (0,1,2) " & _
                                 " AND   P.REC_STATUS IN (0,1,2) " & _
                                 " AND   I.TR_M_ID= '" & Rec_ID & "' " & _
                                 " ORDER BY I.TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Bank Payments
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetBankPayments</remarks>
        Public Shared Function GetBankPayments(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select *,'' AS BANK_NAME,'' AS BRANCH_NAME,'' AS ACC_NO,'' AS MT_BANK_NAME  from TRANSACTION_D_PAYMENT_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Rec_ID & "' AND TR_PAY_TYPE='BANK' ORDER BY TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''Commented as not being used 
        ' ''' <summary>
        ' ''' Get Advances
        ' ''' </summary>
        ' ''' <param name="Rec_ID"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.Receipts_GetAdvances</remarks>
        'Public Shared Function GetAdvances(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Query As String = "select *  from TRANSACTION_D_PAYMENT_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Rec_ID & "' AND TR_PAY_TYPE='ADVANCE' ORDER BY TR_SR_NO"
        '    Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        'End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetAdvancesList</remarks>
        Public Shared Function GetAdvancesRefundList(ByVal Param As Param_Receipts_GetAdvancesRefundList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            Dim NextYear As String = ""
            If Param.NextUnAuditedYearID.ToString.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & Param.NextUnAuditedYearID.ToString & ""
            If Not Param.IsNew Then
                OnlineQuery = "SELECT TP.TR_REF_ID,  SUM(CASE WHEN TP.TR_PAY_TYPE IN ('ADVANCE') THEN TP.TR_REF_AMT ELSE 0 END) AS Adjusted, SUM( CASE WHEN TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','SALE-RECEIVABLE') THEN TP.TR_REF_AMT ELSE 0 END ) AS Refund FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','ADVANCE','SALE-RECEIVABLE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND (TM.TR_COD_YEAR_ID =" & Param.YearID.ToString & " " & NextYear & ") AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN ('" & Param.PartyIDs & "'))) AND TP.TR_M_ID  <> '" & Param.TxnMID & "'  GROUP BY TP.TR_REF_ID "
            Else
                OnlineQuery = "SELECT TP.TR_REF_ID,  SUM( CASE WHEN TP.TR_PAY_TYPE IN ('ADVANCE') THEN TP.TR_REF_AMT ELSE 0 END) AS Adjusted, SUM( CASE WHEN TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','SALE-RECEIVABLE') THEN TP.TR_REF_AMT ELSE 0 END) AS Refund FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','ADVANCE','SALE-RECEIVABLE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND (TM.TR_COD_YEAR_ID =" & Param.YearID.ToString & " " & NextYear & ") AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN ('" & Param.PartyIDs & "'))) GROUP BY TP.TR_REF_ID "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Deposits List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_GetDepositsList</remarks>
        Public Shared Function GetDepositsRefundList(ByVal Param As Param_Receipts_GetDepositsRefundList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim NextYear As String = ""
            If Param.NextUnauditedYearID.ToString.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & Param.NextUnauditedYearID.ToString & ""
            If Param.YearID = "" Then Param.YearID = inBasicParam.openYearID
            If Not Param.IsNew Then
                Query = "SELECT TP.TR_REF_ID, SUM(TP.TR_REF_AMT) AS Refund FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN ('" & Param.PartyIDs & "'))) AND TM.TR_CODE=4 AND (TM.TR_COD_YEAR_ID = " & Param.YearID.ToString & " " & NextYear & ") AND TP.TR_M_ID  <> '" & Param.TxnMID & "'  GROUP BY TP.TR_REF_ID "
            Else
                Query = "SELECT TP.TR_REF_ID, SUM(TP.TR_REF_AMT) AS Refund FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN ('" & Param.PartyIDs & "'))) AND TM.TR_CODE=4 AND (TM.TR_COD_YEAR_ID = " & Param.YearID.ToString & " " & NextYear & ")  GROUP BY TP.TR_REF_ID "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Liabilities
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Receipts_GetLiabilities</remarks>
        Public Shared Function GetLiabilities(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select *  from TRANSACTION_D_PAYMENT_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Rec_ID & "' AND TR_PAY_TYPE='LIABILITIES' ORDER BY TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert MasterInfo
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Receipts_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT,TR_RECEIPT_TYPE," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InMInfo.TxnCode & "'," & _
                                                  "'" & InMInfo.VNo & "', " & _
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InMInfo.PartyID & "'," & _
                                                  " " & InMInfo.SubTotal & "," & _
                                                  " " & InMInfo.Cash & "," & _
                                                  " " & InMInfo.Bank & "," & _
                                                  " " & InMInfo.Advance & "," & _
                                                  " " & InMInfo.Liability & "," & _
                                                  " " & InMInfo.Credit & "," & _
                                                  " " & InMInfo.TDS & "," & _
                                                  "'" & InMInfo.ReceiptType & "', " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.Sub_Cr_Led_ID.Length = 0 Then InParam.Sub_Cr_Led_ID = "NULL" Else If Not InParam.Sub_Cr_Led_ID.StartsWith("'") Then InParam.Sub_Cr_Led_ID = "'" & InParam.Sub_Cr_Led_ID & "'"
            If InParam.Sub_Dr_Led_ID.Length = 0 Then InParam.Sub_Dr_Led_ID = "NULL" Else If Not InParam.Sub_Dr_Led_ID.StartsWith("'") Then InParam.Sub_Dr_Led_ID = "'" & InParam.Sub_Dr_Led_ID & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.Tr_M_ID.Length = 0 Then InParam.Tr_M_ID = "NULL" Else If Not InParam.Tr_M_ID.StartsWith("'") Then InParam.Tr_M_ID = "'" & InParam.Tr_M_ID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.PartyID.Length = 0 Then InParam.PartyID = "NULL" Else If Not InParam.PartyID.StartsWith("'") Then InParam.PartyID = "'" & InParam.PartyID & "'"
            If InParam.Cross_Ref_ID.Length = 0 Then InParam.Cross_Ref_ID = "NULL" Else If Not InParam.Cross_Ref_ID.StartsWith("'") Then InParam.Cross_Ref_ID = "'" & InParam.Cross_Ref_ID & "'"
            If InParam.Ref_Bank.Length = 0 Then InParam.Ref_Bank = "NULL" Else If Not InParam.Ref_Bank.StartsWith("'") Then InParam.Ref_Bank = "'" & InParam.Ref_Bank & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID,TR_MODE,TR_REF_BANK_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  " " & InParam.TransCode & "," & _
                                                      "'" & InParam.VNo & "', " & _
                                                      "" & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                      "'" & InParam.ItemID & "', " & _
                                                      "'" & InParam.Type & "', " & _
                                                      " " & InParam.Cr_Led_ID & " , " & _
                                                      " " & InParam.Dr_Led_ID & " , " & _
                                                      " " & InParam.Sub_Cr_Led_ID & " , " & _
                                                      " " & InParam.Sub_Dr_Led_ID & " , " & _
                                                      " " & InParam.Mode & " , " & _
                                                      " " & InParam.Ref_Bank & " , " & _
                                                      " " & InParam.Ref_Branch & " , " & _
                                                      " " & InParam.Ref_No & " , " & _
                                                      "" & If(IsDate(InParam.RefDate), "'" & Convert.ToDateTime(InParam.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                      "" & If(IsDate(InParam.RefCDate), "'" & Convert.ToDateTime(InParam.RefCDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                      " " & InParam.Amount & ", " & _
                                                      " " & InParam.PartyID & " , " & _
                                                      "'" & InParam.Narration & "', " & _
                                                      "'" & InParam.Remarks & "', " & _
                                                      "'" & InParam.Reference & "', " & _
                                                      " " & InParam.Tr_M_ID & " , " & _
                                                      "'" & InParam.TxnSrNo & "', " & _
                                                      " " & InParam.Cross_Ref_ID & " , " & _
                                            "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," & _
                                            "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                            ") VALUES(" & _
                                            "" & inBasicParam.openCenID.ToString & "," & _
                                            "" & inBasicParam.openYearID.ToString & "," & _
                                            "'" & InItem.Txn_M_ID & "', " & _
                                            " " & InItem.TxnSrNo & " , " & _
                                            "'" & InItem.ItemID & "', " & _
                                            "'" & InItem.LedID & "', " & _
                                            "'" & InItem.Type & "', " & _
                                            "'" & InItem.PartyReq & "', " & _
                                            "'" & InItem.Profile & "', " & _
                                            "'" & InItem.ItemName & "', " & _
                                            "'" & InItem.Head & "', " & _
                                            " " & InItem.Qty & ", " & _
                                            "'" & InItem.Unit & "', " & _
                                            " " & InItem.Rate & ", " & _
                                            " " & InItem.Amount & ", " & _
                                            "'" & InItem.Remarks & "', " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO, " & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InPurpose.TxnID & "'," & _
                                                  "'" & InPurpose.PurposeID & "', " & _
                                                  " " & InPurpose.Amount & ", " & _
                                                  " " & InPurpose.SrNo & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertSlip(ByVal InSlip As Parameter_InsertSlip_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = '" & inBasicParam.openCenID & "' AND SL_COD_YEAR_ID = '" & inBasicParam.openYearID & "' AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
            Dim Slip_ID As Object = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            If Slip_ID Is Nothing Then
                Dim rec_ID As String = Guid.NewGuid.ToString
                Query = "INSERT INTO slip_info ([SL_CEN_ID],[SL_COD_YEAR_ID],[SL_PRINT_DATE],[SL_NO],[REC_ID],[SL_BA_REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" & _
                                                 ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  " NULL," & _
                                                  " " & InSlip.SlipNo & "," & _
                                                  "'" & rec_ID & "'," & _
                                                  " '" & InSlip.Dep_BA_ID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1 " & ")"
                dbService.Insert(ConnectOneWS.Tables.SLIP_INFO, Query, inBasicParam, Nothing)

                Query = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = " & inBasicParam.openCenID.ToString & " AND SL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
                Slip_ID = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            End If

            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_SLIP_INFO([TR_CEN_ID],[TR_COD_YEAR_ID],[TR_M_ID],[TR_SR_NO],[TR_SLIP_ID],[REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InSlip.TxnID & "',0," & _
                                                  "'" & Slip_ID & "', " & _
                                                  " '" & InSlip.RecID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1 " & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, OnlineQuery, inBasicParam, InSlip.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Advance And Liabilities Payment
        ''' </summary>
        ''' <param name="InPayment"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertPayment</remarks>
        Public Shared Function InsertPayment(ByVal InPayment As Parameter_InsertAandLPayment_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPayment.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_REF_ID,TR_REF_AMT," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InPayment.TxnMID & "', " & _
                                        "'" & InPayment.Type & "', " & _
                                        " " & InPayment.SrNo & " , " & _
                                        "'" & InPayment.RefID & "', " & _
                                        " " & InPayment.RefAmount & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPayment.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPayment.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Bank Payment
        ''' </summary>
        ''' <param name="InBpayment"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_InsertBankPayment</remarks>
        Public Shared Function InsertPayment(ByVal InBpayment As Parameter_InsertBankPayment_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_MT_BANK_ID,TR_MT_ACC_NO," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InBpayment.TxnMID & "', " & _
                                        "'" & InBpayment.Type & "', " & _
                                        " " & InBpayment.SrNo & " , " & _
                                        "'" & InBpayment.Mode & "', " & _
                                        "'" & InBpayment.RefID & "', " & _
                                        "'" & InBpayment.RefNo & "', " & _
                                        " " & If(IsDate(InBpayment.RefDate), "'" & Convert.ToDateTime(InBpayment.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        " " & If(IsDate(InBpayment.RefCDate), "'" & Convert.ToDateTime(InBpayment.RefCDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        " " & InBpayment.RefAmount & ", " & _
                                        " '" & InBpayment.BankID & "', " & _
                                        " '" & InBpayment.AccountNo & "', " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InBpayment.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InBpayment.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InBpayment.RecID)
            Return True
        End Function

        ''' <summary>
        ''' update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Receipts_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " & _
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " & _
                                            " TR_RECEIPT_TYPE = '" & UpParam.ReceiptType & "' , " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        Public Shared Function InsertReceipt_Txn(inParam As Param_Txn_Insert_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_Insert Is Nothing Then
                If Not inParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Dr_Led_ID)
                If Not inParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Cr_Led_ID)
                If Not inParam.param_Insert.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Sub_Cr_Led_ID)
                If Not inParam.param_Insert.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Sub_Dr_Led_ID)
                If Not inParam.param_Insert.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.PartyID)
            End If

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not Insert(inParam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertCreditJV Is Nothing Then
                If Not Insert(inParam.param_InsertCreditJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertDebitJV Is Nothing Then
                If Not Insert(inParam.param_InsertDebitJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each inLiab_Insert As Liabilities.Parameter_InsertTRID_Liabilities In inParam.InsertLiability
                If Not inLiab_Insert Is Nothing Then Liabilities.Insert(inLiab_Insert, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertPurposedr Is Nothing Then
                If Not InsertPurpose(inParam.param_InsertPurposedr, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InPmtAdjusted Is Nothing Then
                If Not InsertPayment(inParam.param_InPmtAdjusted, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InPmtRefund Is Nothing Then
                If Not InsertPayment(inParam.param_InPmtRefund, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertPurpose Is Nothing Then
                If Not InsertPurpose(inParam.param_InsertPurpose, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(inParam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            'End Using

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(inParam.InsertSplVchrRefs, inParam.param_InsertMaster.RecID, Nothing, Nothing, inBasicParam)
            'If inParam.InsertSplVchrRefs IsNot Nothing Then
            '    If inParam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In inParam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, inParam.param_InsertMaster.RecID, inParam.param_InsertMaster.SubTotal, inBasicParam)
            '        Next
            '    End If
            'End If

            Return True
        End Function
        Public Shared Function InsertReference(Param As Parameter_InsertSplVchrRef_Vouchers, ByVal Tr_M_ID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'"
            Dim ID As String = Guid.NewGuid.ToString()
            Dim OnlineQuery As String = "INSERT INTO transaction_d_reference_info(TR_CEN_ID, TR_COD_YEAR_ID, TR_M_ID, TXN_REC_ID, TR_SR_NO, " &
                                                  "TR_VOUCHER_REF, TR_AMOUNT, REC_ID, REC_ADD_ON, REC_ADD_BY)" &
                                                  " VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & Tr_M_ID & "'," &
                                                  " NULL, " &
                                                  " NULL, " &
                                                  "'" & Param.Task_Name & "', " &
                                                  " " & Amount.ToString() & ", '" & ID & "', " & Str & ")"
            'MsgBox(OnlineQuery)
            'Console.WriteLine(OnlineQuery)
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, OnlineQuery, inBasicParam, ID, )
            Return True
        End Function
        Public Shared Function UpdateReceipt_Txn(upParam As Param_Txn_Update_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_Insert Is Nothing Then
                If Not upParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Dr_Led_ID)
                If Not upParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Cr_Led_ID)
                If Not upParam.param_Insert.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Sub_Cr_Led_ID)
                If Not upParam.param_Insert.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Sub_Dr_Led_ID)
                If Not upParam.param_Insert.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.PartyID)
            End If

            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_M_ID = '" & upParam.MID_DeleteSlip & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.param_Insert Is Nothing Then 'Actual Txn Re-Inserted
                If Not Insert(upParam.param_Insert, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertCreditJV Is Nothing Then 'JV Credit Re-Inserted
                If Not Insert(upParam.param_InsertCreditJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertDebitJV Is Nothing Then 'JV Debit Re-Inserted
                If Not Insert(upParam.param_InsertDebitJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurposedr Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertPurposedr, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InPmtAdjusted Is Nothing Then 'Payment Re-Inserted
                If Not InsertPayment(upParam.param_InPmtAdjusted, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InPmtRefund Is Nothing Then 'Payment Re-Inserted
                If Not InsertPayment(upParam.param_InPmtRefund, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurpose Is Nothing Then 'Purpose Re-Inserted
                If Not InsertPurpose(upParam.param_InsertPurpose, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(upParam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '  End Using
            'Commit here 
            ' txn.Complete()
            ' End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            InsertSpecialVoucherReference(upParam.UpdateSplVchrRefs, upParam.param_UpdateMaster.RecID, Nothing, Nothing, inBasicParam)
            'If upParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If upParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In upParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, upParam.param_UpdateMaster.RecID, upParam.param_UpdateMaster.SubTotal, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function DeleteReceipt_Txn(delParam As Param_Txn_Delete_VoucherReceipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & delParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & delParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_M_ID = '" & delParam.MID_DeleteSlip & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            '  End Using
            ' txn.Complete()
            '  End Using
            Return True
        End Function
    End Class
End Namespace
