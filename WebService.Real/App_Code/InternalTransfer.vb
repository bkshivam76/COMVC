Imports System.Data
Imports Real.Vouchers

Namespace Real
#Region "Accounts"
    <Serializable>
    Public Class Voucher_Internal_Transfer
#Region "Param Classes"
        <Serializable>
        Public Class Param_Voucher_Internal_Transfer_GetList
            Public To_Match_RecID As String
            Public To_Cen_ID As Integer
            Public openCenRecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_Internal_Transfer_GetListWithMultipleParams
            Public Mode As String
            Public Amount As Double
            Public RefNo As String
            Public TRDate As Date
            Public openCenRecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_Internal_Transfer_GetFrCenterList
            Public IncludeCenIDs As String
            Public ExcludeCenIDs As String
            Public IncludeRecIDs As String
            Public ExcludeRecIDs As String
            Public openInsID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransfer_GetToCenterList : Inherits Param_Voucher_Internal_Transfer_GetFrCenterList
            'Same Params
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransfer_GetTxnCount
            Public VoucherTxnCode As String
            Public ItemID As String
            Public AB_ID_1 As String
            Public AB_ID_2 As String
            Public VoucherDate As String
            Public Mode As String
            Public RefNo As String
            Public BankRefID As String
            Public IsEdit As Boolean
            Public Master_Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherInternalTransfer
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherInternalTransfer
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public SUB_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Ref_BANK_ID As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_CDate As String
            Public Ref_Others As String
            Public MTBankID As String
            Public MTAccNo As String
            Public AB_ID_1 As String
            Public AB_ID_2 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public MasterTxnID As String
            Public Sr_No As Integer
            Public Status_Action As String
            Public RecID As String
            Public Cross_Ref_ID As String
            'Public openYearID As String ' Removed and used from basicParam instead
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherInternalTransfer
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead
        End Class
        <Serializable>
        Public Class Parameter_InsertTDSDeduction_VoucherInternalTransfer
            Public RefTxnID As String
            Public TxnMID As String
            Public TDS_Sent As Decimal
            Public TDS_Paid_Govt As Decimal
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateMasterInfo_VoucherInternalTransfer
            'Inherits Parameter_InsertMasterInfo_VoucherInternalTransfer
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_VoucherInternalTransfer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Ref_Others As String
            Public Amount As Double
            Public AB_ID_1 As String
            Public AB_ID_2 As String
            Public MT_Bank_ID As String
            Public MT_AccNo As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransfer_Update_CrossReference
            Public Cross_Ref_ID As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransferGet_Tf_Banks
            Public CenId As String
            Public TrfBank_Account_Acc_No As String
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransfer_MatchTransfers
            Public MatchingRecID As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_InternalTransfer_UnMatchTransfers
            Public MatchingRecID As String
            Public RecID As String
            Public TxnDate As DateTime
        End Class
        <Serializable>
        Public Class Parameter_UpdatePurpose_VoucherInternalTransfer
            Public PurposeID As String
            Public Amount As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount
            Public OpenCenRecID As String
            Public TopCount As Int32
            Public EnteringCenID As Integer? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetUnmatchedEntriesCentreToCentreCount
            Public CurrInsttID As String
            Public OpenCenRecID As String
            Public FromDate As DateTime = DateTime.MinValue
            Public ToDate As DateTime = DateTime.MinValue
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_InternalTransfer
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherInternalTransfer
            Public param_InsertEP1 As Parameter_Insert_VoucherInternalTransfer = Nothing
            Public param_InsertPurposeEp1 As Parameter_InsertPurpose_VoucherInternalTransfer = Nothing
            Public param_InsertEP2 As Parameter_Insert_VoucherInternalTransfer = Nothing
            Public parma_InsertPurposeEP2 As Parameter_InsertPurpose_VoucherInternalTransfer = Nothing
            Public param_UpdateCrossRef As Param_Voucher_InternalTransfer_Update_CrossReference = Nothing
            Public param_InsertSlip As Parameter_InsertSlip_VoucherInternalTransfer = Nothing
            Public param_Insert_TDSDed() As Parameter_InsertTDSDeduction_VoucherInternalTransfer = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_InternalTransfer
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherInternalTransfer
            Public param_UpdateEP1 As Parameter_Update_VoucherInternalTransfer = Nothing
            Public param_UpdatePurposeEP1 As Parameter_UpdatePurpose_VoucherInternalTransfer = Nothing
            Public param_UpdateEP2 As Parameter_Update_VoucherInternalTransfer = Nothing
            Public param_UpdatePurposeEP2 As Parameter_UpdatePurpose_VoucherInternalTransfer = Nothing
            Public param_InsertEP2 As Parameter_Insert_VoucherInternalTransfer = Nothing
            Public param_InsertPurposeEP2 As Parameter_InsertPurpose_VoucherInternalTransfer = Nothing
            Public param_InsertSlip As Parameter_InsertSlip_VoucherInternalTransfer = Nothing
            Public param_Insert_TDSDed() As Parameter_InsertTDSDeduction_VoucherInternalTransfer = Nothing
            Public ID2_DeletePurpose As String = Nothing
            Public ID2_Delete As String = Nothing
            Public ID_DeleteSlip As String = Nothing
            Public ID_DeleteTDSDeduction As String = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_InternalTransfer
            Public ID1_DeletePurpose As String = Nothing
            Public ID2_DeletePurpose As String = Nothing
            Public ID1_Delete As String = Nothing
            Public ID2_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
            Public ID_DeleteSlip As String = Nothing
            Public ID_DeleteTDSDeduction As String = Nothing
        End Class
        <Serializable>
        Public Class Parameter_InsertSlip_VoucherInternalTransfer
            Public TxnID As String
            Public SlipNo As Integer
            Public RecID As String
            Public ID_DeleteSlip As String = Nothing
            Public Dep_BA_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim Query As String = " SELECT CASE WHEN TI.TR_TRF_CROSS_REF_ID IS NULL THEN 'Unmatched' ELSE 'Matched' END AS 'Status', C.CEN_NAME AS 'Centre Name',C.CEN_UID  AS 'Centre UID',C.CEN_PAD_NO AS 'No.',MAIN.CEN_ZONE_ID AS Zone, MAIN.CEN_ZONE_SUB_ID AS 'Sub Zone', I.ITEM_NAME AS 'Description',TI.TR_DATE AS 'Date',TI.TR_MODE AS 'Mode',TI.TR_AMOUNT AS 'Amount', " & _
            '                      " CASE WHEN TI.TR_TYPE='DEBIT' THEN BI.BI_BANK_NAME  ELSE BI2.BI_BANK_NAME END AS 'Bank Name'    , " & _
            '                      " CASE WHEN TI.TR_TYPE='DEBIT' THEN BB.BB_BRANCH_NAME ELSE TI.TR_REF_BRANCH END AS 'Branch Name'  , " & _
            '                      " CASE WHEN TI.TR_TYPE='DEBIT' THEN BA.BA_ACCOUNT_NO  ELSE TI.TR_MT_ACC_NO END AS 'Bank A/c. No.', " & _
            '                      " CASE WHEN TI.TR_TYPE='CREDIT' THEN COALESCE(BI_DR.BI_SHORT_NAME+ '-' ,'') ELSE COALESCE(BI2.BI_SHORT_NAME+ '-' ,'')  END + CASE WHEN TI.TR_TYPE='CREDIT' THEN COALESCE(BA_DR.BA_ACCOUNT_NO,'') ELSE COALESCE(TI.TR_MT_ACC_NO,'') END AS 'Bank 2'," & _
            '                      " TI.TR_REF_NO as 'Ref.No.',TR_REF_DATE AS 'Ref.Date',MP.MISC_NAME AS 'Purpose',TI.REC_ID AS ID,TI.TR_M_ID AS 'M.ID' ," & Common.Rec_Detail("TI") & "" & _
            '                      " FROM  TRANSACTION_INFO       AS TI      " & _
            '                      " INNER JOIN ITEM_INFO         AS I       ON (I.REC_ID        = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " INNER JOIN CENTRE_INFO       AS C       ON (C.REC_ID        = TI.TR_AB_ID_1      AND C.REC_STATUS    IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " INNER JOIN CENTRE_INFO AS MAIN          ON C.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN =1 " & _
            '                      " LEFT  JOIN BANK_ACCOUNT_INFO AS BA      ON (BA.BA_CEN_ID    = TI.TR_CEN_ID       AND BA.REC_ID = TI.TR_SUB_CR_LED_ID AND BA.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_INFO         AS BI2     ON (BI2.REC_ID      = TI.TR_MT_BANK_ID   AND BI2.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_ACCOUNT_INFO AS BA_DR      ON (BA_DR.BA_CEN_ID    = TI.TR_CEN_ID       AND BA_DR.REC_ID = TI.TR_SUB_DR_LED_ID AND BA_DR.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_BRANCH_INFO  AS BB_DR      ON (BB_DR.REC_ID       = BA_DR.BA_BRANCH_ID    AND BB_DR.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN BANK_INFO         AS BI_DR      ON (BI_DR.REC_ID       = BB_DR.BI_BANK_ID      AND BI_DR.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                      " LEFT  JOIN Transaction_D_Purpose_Info AS TP  ON (TI.REC_ID               = TP.TR_REC_ID  AND TP.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
            '                      " LEFT  JOIN MISC_INFO                  AS MP  ON (TP.TR_PURPOSE_MISC_ID   = MP.REC_ID     AND MP.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MP.MISC_ID='GODLY SERVICE PROJECTS'  )" & _
            '                      " WHERE TI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
            '                      "   AND TI.TR_CODE = 8 AND TI.TR_SR_NO = 1 AND TI.TR_CEN_ID =" & inBasicParam.openCenID.ToString & " and TI.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & "" & _
            '                      " ORDER BY C.CEN_NAME,TI.TR_DATE "
            'Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CENID", "@YEARID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "[sp_get_InternalTransferRegister]", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' GetList 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetListWithTwoParams</remarks>
        Public Shared Function GetList(ByVal Param As Param_Voucher_Internal_Transfer_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Query = "SELECT CASE WHEN TI.TR_TRF_CROSS_REF_ID = '" & Param.To_Match_RecID & "' THEN '***' ELSE '' END AS Matched, ci_main.cen_name+'('+ci.cen_uid+')' as 'Entering Centre', item_name as Item,TR_AMOUNT AS Amount,tr_date AS 'Date', TR_MODE as Mode, TR_REF_NO as 'RefNo', " & _
                                "CASE WHEN ITEM_TRANS_TYPE = 'CREDIT' THEN ci_2_main.cen_name ELSE  ci_1_main.cen_name END AS 'Receiving Center', " & _
                                "CASE WHEN ITEM_TRANS_TYPE = 'DEBIT' THEN ci_2_main.cen_name ELSE ci_1_main.cen_name END AS 'Paying Center', " & _
                                "ti.REC_ID AS REC_ID " & _
                                 "FROM transaction_info AS ti INNER JOIN item_info AS ii ON ii.rec_id =ti. TR_ITEM_ID " & _
                                "INNER JOIN centre_info AS ci ON ci.cen_id = ti.TR_cen_ID INNER JOIN centre_info as ci_main on ci.cen_bk_pad_no = ci_main.cen_pad_no and ci_main.cen_main =1 " & _
                                "INNER JOIN centre_info AS ci_1 ON ci_1.rec_id = ti.TR_ab_ID_1  INNER JOIN centre_info as ci_1_main on ci_1.cen_bk_pad_no = ci_1_main.cen_pad_no and ci_1_main.cen_main =1 " & _
                                "INNER JOIN centre_info AS ci_2 ON ci_2.rec_id = ti.TR_ab_ID_2  INNER JOIN centre_info as ci_2_main on ci_2.cen_bk_pad_no = ci_2_main.cen_pad_no and ci_2_main.cen_main =1 " & _
                                "WHERE ti.rec_status IN (0,1,2) AND TR_CODE = 8 AND (TR_AB_ID_1 = '" & Param.openCenRecID & "' OR TR_AB_ID_2='" & Param.openCenRecID & "') " & _
                                "AND TR_CEN_ID <> " & inBasicParam.openCenID.ToString & " AND TR_CEN_ID =" & Param.To_Cen_ID.ToString & " AND (TI.TR_TRF_CROSS_REF_ID IS NULL OR TI.TR_TRF_CROSS_REF_ID = '" & Param.To_Match_RecID & "') "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' GetListWithMultipleParams
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetListWithMultipleParams</remarks>
        Public Shared Function GetList(ByVal Param As Param_Voucher_Internal_Transfer_GetListWithMultipleParams, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Query = "SELECT ci_1.CEN_ID, ci.cen_uid, item_name,TR_AMOUNT AS Amount,tr_date AS 'Date', TR_MODE, TR_REF_NO, " & _
                                    "CASE WHEN ITEM_TRANS_TYPE = 'CREDIT' THEN ci_2.cen_uid ELSE ci_1.cen_uid END AS Rec_Center, " & _
                                    "CASE WHEN ITEM_TRANS_TYPE = 'DEBIT' THEN ci_2.cen_uid ELSE ci_1.cen_uid END AS Pay_Center ," & _
                                    "CASE WHEN UPPER(TR_MODE) = UPPER('" & Param.Mode & "') " & _
                                    "THEN CASE WHEN TR_AMOUNT = " & Param.Amount & " THEN " & _
                                    "CASE WHEN (LEN(COALESCE(TR_REF_NO,'')) > 0 AND TR_REF_NO ='" & Param.RefNo & "') OR UPPER(TR_MODE)='" & Param.Mode & "' THEN " & _
                                    "CASE WHEN CAST(TR_Date AS DATE) ='" & Param.TRDate.Date.ToShortDateString(Common.Server_Date_Format_Short) & "' THEN 1 ELSE 1.2 END " & _
                                    "ELSE 1.3 END " & _
                                    "ELSE 1.4 END  " & _
                                    "ELSE CASE WHEN TR_AMOUNT = " & Param.Amount & " THEN " & _
                                    "CASE WHEN (LEN(COALESCE(TR_REF_NO,'')) > 0 AND TR_REF_NO ='" & Param.RefNo & "') OR UPPER(TR_MODE)='" & Param.Mode & "' THEN " & _
                                    "CASE WHEN CAST(TR_Date AS DATE) ='" & Param.TRDate.Date.ToShortDateString(Common.Server_Date_Format_Short) & "' THEN 2 ELSE 2.2 END " & _
                                    "ELSE 2.4 END " & _
                                    "ELSE CASE WHEN (LEN(COALESCE(TR_REF_NO,'')) > 0 AND TR_REF_NO ='" & Param.RefNo & "') OR UPPER(TR_MODE)='" & Param.Mode & "' THEN " & _
                                    "CASE WHEN CAST(TR_Date AS DATE) ='" & Param.TRDate.Date.ToShortDateString(Common.Server_Date_Format_Short) & "' THEN 3 ELSE 3.2 END" & _
                                    "ELSE CASE WHEN CAST(TR_Date AS DATE) ='" & Param.TRDate.Date.ToShortDateString(Common.Server_Date_Format_Short) & "' THEN 4 ELSE 5 END " & _
                                    "END END END AS Sr" & _
                                    "FROM transaction_info AS ti INNER JOIN item_info AS ii ON ii.rec_id =ti. TR_ITEM_ID " & _
                                    "INNER JOIN centre_info AS ci ON ci.cen_id = ti.TR_cen_ID " & _
                                    "INNER JOIN centre_info AS ci_1 ON ci_1.rec_id = ti.TR_ab_ID_1 " & _
                                    "INNER JOIN centre_info AS ci_2 ON ci_2.rec_id = ti.TR_ab_ID_2 " & _
                                    "WHERE ti.rec_status IN (0,1,2) AND TR_CODE = 8 " & _
                                    "AND (TR_AB_ID_1 = '" & Param.openCenRecID & "' OR TR_AB_ID_2='" & Param.openCenRecID & "') " & _
                                    "AND TR_CEN_ID <> " & inBasicParam.openCenID.ToString & " ORDER BY sr, tr_amount, tr_date DESC "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets UnMatched List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetUnMatchedList</remarks>
        Public Shared Function GetUnMatchedList(ByVal openCenRecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim param As Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount = New Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount
            param.OpenCenRecID = openCenRecID
            param.TopCount = 0
            Return GetUnMatchedList_LimitedCount(param, inBasicParam).Tables(0)
        End Function


        Public Shared Function GetUnMatchedList_LimitedCount(ByVal inParam As Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CentreFilter As String = ""
            If inParam.EnteringCenID IsNot Nothing Then CentreFilter = " AND TI.TR_CEN_ID = " & inParam.EnteringCenID.ToString & ""
            Dim Query As String = "SELECT "
            If inParam.TopCount > 0 Then Query += " TOP " + inParam.TopCount.ToString + " "
            'Changing for sake of testing Debit to credit 
            Query += " CASE WHEN TI.TR_TRF_CROSS_REF_ID IS NULL THEN 'Unmatched' ELSE 'Matched' END AS 'Status',C2.CEN_NAME AS 'Centre Name',C2.CEN_UID  AS 'Centre UID',C2.CEN_PAD_NO AS 'No.',MAIN.CEN_ZONE_ID AS Zone, MAIN.CEN_ZONE_SUB_ID AS 'Sub Zone', TI.TR_AB_ID_2 AS 'CEN_ID',I_LINK.ITEM_NAME AS 'Description',I_LINK.REC_ID as 'ITEM_ID',TI.TR_DATE AS 'Date',TI.TR_MODE AS 'Mode',TI.TR_AMOUNT AS 'Amount', " & _
                                  " CASE WHEN TI.TR_TYPE='DEBIT' THEN TI.TR_SUB_CR_LED_ID  ELSE TI.TR_SUB_DR_LED_ID END AS 'BI_ID'        ,  " & _
                                  " CASE WHEN TI.TR_TYPE='DEBIT' THEN BI.BI_BANK_NAME ELSE  BI2.BI_BANK_NAME END AS 'Bank Name'    , " & _
                                  " CASE WHEN TI.TR_TYPE='DEBIT' THEN BB.BB_BRANCH_NAME ELSE TI.TR_REF_BRANCH END AS 'Branch Name'  , " & _
                                  " CASE WHEN TI.TR_TYPE='DEBIT' AND TI.TR_MODE <>'CASH TO BANK' THEN BA.BA_ACCOUNT_NO ELSE TI.TR_MT_ACC_NO END AS 'Bank A/c. No.', " & _
                                  " MAIN.CEN_INCHARGE AS 'Incharge',COALESCE(MAIN.CEN_TEL_NO_1+',','')+COALESCE(MAIN.CEN_TEL_NO_2+',','')+COALESCE(MAIN.CEN_MOB_NO_1+',','')+COALESCE(MAIN.CEN_MOB_NO_2,'')as 'Contact No.' ,MP.MISC_NAME AS 'Purpose',MP.REC_ID AS PUR_ID, " & _
                                  " TI.TR_REF_BANK_ID AS 'REF_BI_ID',TI.TR_REF_BRANCH AS 'Ref.Branch',TI.TR_REF_NO as 'Ref.No.',TR_REF_DATE AS 'Ref.Date',TI.TR_REF_OTHERS as 'Ref.Others',TI.REC_ID AS ID,TI.TR_M_ID AS M_ID , TI.TR_MT_ACC_NO as 'Ref_Bank_AccNo',TI.TR_NARRATION AS Narration, " & Common.Rec_Detail("TI") & "" & _
                                  " FROM  TRANSACTION_INFO       AS TI      " & _
                                  " INNER JOIN ITEM_INFO         AS I       ON (I.REC_ID        = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " INNER JOIN ITEM_INFO         AS I_LINK  ON (I_LINK.REC_ID   = I.ITEM_LINK_REC_ID AND UPPER(I_LINK.ITEM_VOUCHER_TYPE) IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I_LINK.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " INNER JOIN CENTRE_INFO       AS C2      ON (C2.REC_ID       = TI.TR_AB_ID_2      AND C2.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " INNER JOIN CENTRE_INFO       AS MAIN    ON (C2.CEN_BK_PAD_NO  = MAIN.CEN_BK_PAD_NO AND MAIN.CEN_MAIN = 1  AND MAIN.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN BANK_ACCOUNT_INFO AS BA      ON (BA.BA_CEN_ID    = TI.TR_CEN_ID       AND BA.REC_ID = TI.TR_SUB_CR_LED_ID AND BA.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN BANK_INFO         AS BI2     ON (BI2.REC_ID      = TI.TR_MT_BANK_ID   AND BI2.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN Transaction_D_Purpose_Info AS TP  ON (TI.REC_ID               = TP.TR_REC_ID  AND TP.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MISC_INFO                  AS MP  ON (TP.TR_PURPOSE_MISC_ID   = MP.REC_ID     AND MP.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MP.MISC_ID='GODLY SERVICE PROJECTS'  )" & _
                                  " WHERE TI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                                  "   AND TI.TR_CODE = 8 AND TI.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND TI.TR_TRF_CROSS_REF_ID IS NULL AND TI.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " " & CentreFilter & _
                                  " ORDER BY C2.CEN_NAME,TI.TR_DATE "
            Dim dSet As DataSet = New DataSet
            dSet.Tables.Add(dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam).Copy)

            Query = "SELECT COUNT(*) FROM  TRANSACTION_INFO       AS TI      " & _
                                      " INNER JOIN ITEM_INFO         AS I       ON (I.REC_ID        = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                      " INNER JOIN ITEM_INFO         AS I_LINK  ON (I_LINK.REC_ID   = I.ITEM_LINK_REC_ID AND UPPER(I_LINK.ITEM_VOUCHER_TYPE) IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I_LINK.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                      " INNER JOIN CENTRE_INFO       AS C2      ON (C2.REC_ID       = TI.TR_AB_ID_2      AND C2.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                      " WHERE TI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                                      "   AND TI.TR_CODE = 8 AND TI.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND TI.TR_TRF_CROSS_REF_ID IS NULL AND TI.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " "
            dSet.Tables.Add(dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, "COUNT", inBasicParam).Copy)
            Return dSet
        End Function

        Public Shared Function GetUnmatchedEntriesCentreToCentreCount(ByVal Param As Param_GetUnmatchedEntriesCentreToCentreCount, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select COUNT(TR_CODE) from transaction_info AS TI INNER JOIN centre_info AS CI ON TI.TR_CEN_ID = CI.CEN_ID INNER JOIN item_info AS I ON TI.TR_ITEM_ID = I.REC_ID  INNER JOIN Item_Mapping AS IM ON I.REC_ID = IM.Map_Item_Rec_ID AND IM.Map_Instt_ID='" & Param.CurrInsttID & "'  AND (COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " OR COALESCE(Map_Cen_ID,0) = TR_CEN_ID) AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE ((TR_CEN_ID =" & inBasicParam.openCenID.ToString & " and TI.REC_STATUS in (0,1,2) AND TR_CODE = 8 AND TR_TRF_CROSS_REF_ID IS NULL) OR (TR_CODE = 8 AND TR_TRF_CROSS_REF_ID IS NULL AND TR_AB_ID_1 = '" & Param.OpenCenRecID & "' and TI.REC_STATUS in (0,1,2)))  AND (UPPER(ITEM_VOUCHER_TYPE)='INTERNAL TRANSFER' AND ITEM_APPLICABLE <> 'H.Q.'  	OR 	((UPPER(ITEM_VOUCHER_TYPE)='INTERNAL TRANSFER WITH H.Q.' OR ITEM_APPLICABLE = 'H.Q.') AND (TR_MODE <> 'CASH' OR CI.CEN_INS_ID <> '00001' OR I.REC_ID in ('d611ce4a-756a-42d0-b7ee-51dd7add7263','f167f8c6-d260-42f7-8020-9a37133d9e4d','92c81219-0c79-457d-a551-5d6ff11e6248','d4d824b8-dde2-40f8-a819-e3ec14338686','6ee520f5-3633-4e55-8f37-d1d122f3ebc1','522b1717-800a-4da4-9285-e593005eeb60'))))  and ti.tr_cod_year_id = " & inBasicParam.openYearID.ToString & ""
            If Param.FromDate <> DateTime.MinValue Then
                Query += " AND TR_DATE BETWEEN '" & Convert.ToDateTime(Param.FromDate).ToString(Common.Server_Date_Format_Long) & "' AND  '" & Convert.ToDateTime(Param.ToDate).ToString(Common.Server_Date_Format_Long) & "'"
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, "TRANSACTION_INFO", inBasicParam)
        End Function

        ''' <summary>
        ''' Get Fr Center List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetFrCenterList</remarks>
        Public Shared Function GetFrCenterList(ByVal Param As Param_Voucher_Internal_Transfer_GetFrCenterList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT CASE WHEN LEN(COALESCE(C.CEN_NAME,'')) > 0 THEN C.CEN_NAME ELSE M.CEN_NAME END AS FR_CEN_NAME,C.CEN_PAD_NO AS FR_PAD_NO, C.CEN_UID AS FR_UID,   M.CEN_INCHARGE AS FR_INCHARGE, M.CEN_ZONE_ID AS FR_ZONE, C.CEN_ID AS FR_CEN_ID, C.REC_ID AS FR_ID, " & _
                                    " CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 THEN M.CEN_TEL_NO_1 ELSE '' END +  CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 AND LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN M.CEN_MOB_NO_1 ELSE '' END AS FR_TEL_NO" & _
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " & _
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='" & Param.openInsID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"
            If Param.ExcludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID NOT IN (" & Param.ExcludeRecIDs & ")"
            If Param.IncludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID IN (" & Param.IncludeRecIDs & ")"
            If Param.ExcludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID NOT IN (" & Param.ExcludeCenIDs & ")"
            If Param.IncludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID IN (" & Param.IncludeCenIDs & ")"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get To Center List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetToCenterList</remarks>
        Public Shared Function GetToCenterList(ByVal Param As Param_Voucher_InternalTransfer_GetToCenterList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT CASE WHEN LEN(COALESCE(C.CEN_NAME,'')) > 0 THEN C.CEN_NAME ELSE M.CEN_NAME END AS TO_CEN_NAME,C.CEN_PAD_NO AS TO_PAD_NO, C.CEN_UID AS TO_UID,   M.CEN_INCHARGE AS TO_INCHARGE, M.CEN_ZONE_ID AS TO_ZONE, C.CEN_ID AS TO_CEN_ID, C.REC_ID AS TO_ID, " & _
                                    " CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0  THEN M.CEN_TEL_NO_1 ELSE '' END +  CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 AND LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN ', ' ELSE '' END + CASE WHEN LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN M.CEN_MOB_NO_1 ELSE '' END AS TO_TEL_NO" & _
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " & _
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='" & Param.openInsID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If Param.ExcludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID NOT IN (" & Param.ExcludeRecIDs & ")"
            If Param.IncludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID IN (" & Param.IncludeRecIDs & ")"
            If Param.ExcludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID NOT IN (" & Param.ExcludeCenIDs & ")"
            If Param.IncludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID IN (" & Param.IncludeCenIDs & ")"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Cash Txn Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetCashTxnCount</remarks>
        Public Shared Function GetCashTxnCount(ByVal Param As Param_Voucher_InternalTransfer_GetTxnCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT count(tr_date) FROM TRANSACTION_INFO  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_CODE = " & Param.VoucherTxnCode & " AND TR_SR_NO=1 " & _
                                        " AND TR_MODE='CASH' AND TR_ITEM_ID='" & Param.ItemID & "' AND TR_AB_ID_1='" & Param.AB_ID_1 & "' AND TR_AB_ID_2='" & Param.AB_ID_2 & "' AND CAST(TR_DATE AS DATE) = '" & Format(Convert.ToDateTime(Param.VoucherDate), Common.Server_Date_Format_Short) & "' "
            If Param.IsEdit Then OnlineQuery += " AND TR_M_ID <> '" & Param.Master_Rec_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Cash Pending Txn Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetCashPendingTxnCount</remarks>
        Public Shared Function GetCashPendingTxnCount(ByVal Param As Param_Voucher_InternalTransfer_GetTxnCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT count(TI.REC_ID)          " & _
                                        " FROM TRANSACTION_INFO AS TI      " & _
                                        " INNER JOIN ITEM_INFO  AS I       ON (I.REC_ID      = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (0,1,2) )  " & _
                                        " INNER JOIN ITEM_INFO  AS I_LINK  ON (I_LINK.REC_ID = I.ITEM_LINK_REC_ID AND UPPER(I_LINK.ITEM_VOUCHER_TYPE) IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I_LINK.REC_STATUS IN (0,1,2) )  " & _
                                        " WHERE TI.REC_STATUS IN (0,1,2) AND  TI.TR_CODE = " & Param.VoucherTxnCode & " AND TI.TR_MODE='CASH' AND I_LINK.REC_ID='" & Param.ItemID & "' AND TI.TR_AB_ID_1='" & Param.AB_ID_2 & "' AND TI.TR_AB_ID_2='" & Param.AB_ID_1 & "' AND CAST(TI.TR_DATE AS DATE) = '" & Format(Convert.ToDateTime(Param.VoucherDate), Common.Server_Date_Format_Short) & "' AND TI.TR_TRF_CROSS_REF_ID IS NULL " & _
                                        "  "
            If Param.IsEdit Then OnlineQuery += " AND TI.TR_M_ID <> '" & Param.Master_Rec_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Non Cash Txn Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetCashTxnCount</remarks>
        Public Shared Function GetNonCashTxnCount(ByVal Param As Param_Voucher_InternalTransfer_GetTxnCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT count(TI.REC_ID)                 " & _
                                        " FROM TRANSACTION_INFO        AS TI      " & _
                                        " INNER JOIN ITEM_INFO         AS I       ON (I.REC_ID        = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (0,1,2) )  " & _
                                        " INNER JOIN ITEM_INFO         AS I_LINK  ON (I_LINK.REC_ID   = I.ITEM_LINK_REC_ID AND UPPER(I_LINK.ITEM_VOUCHER_TYPE) IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I_LINK.REC_STATUS IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_ACCOUNT_INFO AS BA      ON (BA.BA_CEN_ID    = TI.TR_CEN_ID       AND BA.REC_ID = TI.TR_SUB_CR_LED_ID AND BA.REC_STATUS IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (0,1,2) )  " & _
                                        " WHERE TI.REC_STATUS IN (0,1,2) AND  TI.TR_CODE = " & Param.VoucherTxnCode & " AND TI.TR_ITEM_ID='" & Param.ItemID & "' AND TI.TR_AB_ID_1='" & Param.AB_ID_1 & "' AND TI.TR_AB_ID_2='" & Param.AB_ID_2 & "' AND TI.TR_MODE='" & Param.Mode & "' AND (CASE WHEN TI.TR_TYPE='DEBIT' THEN BI.REC_ID ELSE TI.TR_MT_BANK_ID END) ='" & Param.BankRefID & "' AND TI.TR_REF_NO ='" & Param.RefNo & "'  AND TI.TR_TRF_CROSS_REF_ID IS NULL " & _
                                        " AND TI.TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString
            If Param.IsEdit Then OnlineQuery += " AND TI.TR_M_ID <> '" & Param.Master_Rec_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Non Cash Pending Txn Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetCashPendingTxnCount</remarks>
        Public Shared Function GetNonCashPendingTxnCount(ByVal Param As Param_Voucher_InternalTransfer_GetTxnCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT count(TI.REC_ID)                 " & _
                                        " FROM TRANSACTION_INFO        AS TI      " & _
                                        " INNER JOIN ITEM_INFO         AS I       ON (I.REC_ID        = TI.TR_ITEM_ID      AND UPPER(I.ITEM_VOUCHER_TYPE)      IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I.REC_STATUS      IN (0,1,2) )  " & _
                                        " INNER JOIN ITEM_INFO         AS I_LINK  ON (I_LINK.REC_ID   = I.ITEM_LINK_REC_ID AND UPPER(I_LINK.ITEM_VOUCHER_TYPE) IN('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND I_LINK.REC_STATUS IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_ACCOUNT_INFO AS BA      ON (BA.BA_CEN_ID    = TI.TR_CEN_ID       AND BA.REC_ID = TI.TR_SUB_CR_LED_ID AND BA.REC_STATUS IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (0,1,2) )  " & _
                                        " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (0,1,2) )  " & _
                                        " WHERE TI.REC_STATUS IN (0,1,2) AND  TI.TR_CODE = " & Param.VoucherTxnCode & " AND I_LINK.REC_ID='" & Param.ItemID & "' AND TI.TR_AB_ID_1='" & Param.AB_ID_2 & "' AND TI.TR_AB_ID_2='" & Param.AB_ID_1 & "' AND TI.TR_MODE='" & Param.Mode & "' AND (CASE WHEN TI.TR_TYPE='DEBIT' THEN BI.REC_ID ELSE TI.TR_MT_BANK_ID END) ='" & Param.BankRefID & "' AND TI.TR_REF_NO ='" & Param.RefNo & "'  AND TI.TR_TRF_CROSS_REF_ID IS NULL " & _
                                        "  "
            If Param.IsEdit Then OnlineQuery += " AND TI.TR_M_ID <> '" & Param.Master_Rec_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get_Tf_Banks
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Get_Tf_Banks</remarks>
        Public Shared Function Get_Tf_Banks(ByVal Param As Param_Voucher_InternalTransferGet_Tf_Banks, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            'If Param.CentreWise Then 'to be Shifted
            Query = " SELECT BI.BI_BANK_NAME AS 'TRF_BI_BANK_NAME',BI.BI_SHORT_NAME AS 'TRF_BI_SHORT_NAME',BA.BA_FERA_ACC, BB.BB_BRANCH_NAME AS 'TRF_BB_BRANCH_NAME' ,BA.BA_ACCOUNT_NO AS 'TRF_BA_ACCOUNT_NO',BA.REC_ID AS 'BA_REC_ID',BI.REC_ID AS 'TRF_BI_ID',BB.BB_IFSC_CODE AS TRF_IFSC_CODE,CASE WHEN BA.BA_CLOSE_DATE IS NULL THEN 'Open' ELSE 'Closed' END AS 'TRF_STATUS', BA.REC_EDIT_ON " & _
                    " FROM       BANK_ACCOUNT_INFO AS BA      " & _
                    " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                    " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                    " WHERE BA.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                    "   AND UPPER(BA.BA_ACCOUNT_TYPE )='SAVING' AND BA.BA_CEN_ID =" & IIf(Param.CenId.Length > 0, Param.CenId, 0) & " " & _
                    " AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))  AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")" & _
                    " ORDER BY BI.BI_BANK_NAME "
            If Not Param.TrfBank_Account_Acc_No Is Nothing Then
                Query = " SELECT BI.BI_BANK_NAME AS 'TRF_BI_BANK_NAME',BI.BI_SHORT_NAME AS 'TRF_BI_SHORT_NAME',BA.BA_FERA_ACC, BB.BB_BRANCH_NAME AS 'TRF_BB_BRANCH_NAME' ,BA.BA_ACCOUNT_NO AS 'TRF_BA_ACCOUNT_NO',BA.REC_ID AS 'BA_REC_ID',BI.REC_ID AS 'TRF_BI_ID',BB.BB_IFSC_CODE AS TRF_IFSC_CODE,CASE WHEN BA.BA_CLOSE_DATE IS NULL THEN 'Open' ELSE 'Closed' END AS 'TRF_STATUS', BA.REC_EDIT_ON " & _
                    " FROM       BANK_ACCOUNT_INFO AS BA      " & _
                    " LEFT  JOIN BANK_BRANCH_INFO  AS BB      ON (BB.REC_ID       = BA.BA_BRANCH_ID    AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                    " LEFT  JOIN BANK_INFO         AS BI      ON (BI.REC_ID       = BB.BI_BANK_ID      AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                    " WHERE BA.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                    "   AND UPPER(BA.BA_ACCOUNT_TYPE )='SAVING' AND BA.BA_CEN_ID =" & IIf(Param.CenId.Length > 0, Param.CenId, 0) & " AND BA.BA_ACCOUNT_NO = '" & Param.TrfBank_Account_Acc_No & "' " & _
                    " AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))  AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")" & _
                    " ORDER BY BI.BI_BANK_NAME "
            End If
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Master
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_InsertMaster</remarks>
        Public Shared Function InsertMaster(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            If AddTime = Nothing Or AddTime = DateTime.MinValue Then AddTime = DateTime.Now
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT," & _
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
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = "NULL" Else If Not InParam.SUB_Cr_Led_ID.StartsWith("'") Then InParam.SUB_Cr_Led_ID = "'" & InParam.SUB_Cr_Led_ID & "'"
            If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = "NULL" Else If Not InParam.SUB_Dr_Led_ID.StartsWith("'") Then InParam.SUB_Dr_Led_ID = "'" & InParam.SUB_Dr_Led_ID & "'"
            If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = "NULL" Else If Not InParam.Ref_BANK_ID.StartsWith("'") Then InParam.Ref_BANK_ID = "'" & InParam.Ref_BANK_ID & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.Ref_Others.Length = 0 Then InParam.Ref_Others = "NULL" Else If Not InParam.Ref_Others.StartsWith("'") Then InParam.Ref_Others = "'" & InParam.Ref_Others & "'"
            If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.MTBankID.Length = 0 Then InParam.MTBankID = "NULL" Else If Not InParam.MTBankID.StartsWith("'") Then InParam.MTBankID = "'" & InParam.MTBankID & "'"
            If InParam.MTAccNo.Length = 0 Then InParam.MTAccNo = "NULL" Else If Not InParam.MTAccNo.StartsWith("'") Then InParam.MTAccNo = "'" & InParam.MTAccNo & "'"
            If InParam.AB_ID_1.Length = 0 Then InParam.AB_ID_1 = "NULL" Else If Not InParam.AB_ID_1.StartsWith("'") Then InParam.AB_ID_1 = "'" & InParam.AB_ID_1 & "'"
            If InParam.AB_ID_2.Length = 0 Then InParam.AB_ID_2 = "NULL" Else If Not InParam.AB_ID_2.StartsWith("'") Then InParam.AB_ID_2 = "'" & InParam.AB_ID_2 & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID," & _
                                        "TR_MODE,TR_REF_BANK_ID, TR_REF_BRANCH, TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_OTHERS,TR_AMOUNT,TR_MT_BANK_ID,TR_MT_ACC_NO,TR_AB_ID_1,TR_AB_ID_2,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                        ") VALUES(" & _
                                                        "" & inBasicParam.openCenID.ToString & "," & _
                                                        "" & inBasicParam.openYearID.ToString & "," & _
                                                        " " & InParam.TransCode & "," & _
                                                            "'" & InParam.VNo & "', " & _
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            "'" & InParam.ItemID & "', " & _
                                                            "'" & InParam.Type & "', " & _
                                                            " " & InParam.Cr_Led_ID & " , " & _
                                                            " " & InParam.Dr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Cr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Dr_Led_ID & " , " & _
                                                            " " & InParam.Mode & " , " & _
                                                            " " & InParam.Ref_BANK_ID & " , " & _
                                                            " " & InParam.Ref_Branch & " , " & _
                                                            " " & InParam.Ref_No & " , " & _
                                                            " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & If(IsDate(InParam.Ref_CDate), "'" & Convert.ToDateTime(InParam.Ref_CDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & InParam.Ref_Others & " , " & _
                                                            " " & InParam.Amount & ", " & _
                                                            " " & InParam.MTBankID & ", " & _
                                                            " " & InParam.MTAccNo & ", " & _
                                                            " " & InParam.AB_ID_1 & ", " & _
                                                            " " & InParam.AB_ID_2 & ", " & _
                                                            "'" & InParam.Narration & "', " & _
                                                            "'" & InParam.Remarks & "', " & _
                                                            "'" & InParam.Reference & "', " & _
                                                            " " & InParam.MasterTxnID & " , " & _
                                                            " " & InParam.Sr_No & " , " & _
                                                            " " & InParam.Cross_Ref_ID & " , " & _
                                                          "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InPurpose.TxnID & "'," & _
                                                  "'" & InPurpose.PurposeID & "', " & _
                                                  " " & InPurpose.Amount & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertTDSDeduction(ByVal InParam As Parameter_InsertTDSDeduction_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1, '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Dim OnlineQuery As String = "INSERT INTO transaction_d_tds_info(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,REF_TR_M_ID,TDS_SENT,TDS_PAID_GOVT," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.TxnMID & "'," & _
                                                  "'" & InParam.RefTxnID & "', " & _
                                                  " " & IIf(InParam.TDS_Sent = Nothing, "NULL", InParam.TDS_Sent) & ", " & _
                                                  " " & IIf(InParam.TDS_Paid_Govt = Nothing, "NULL", InParam.TDS_Paid_Govt) & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_TDS_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertSlip(ByVal InSlip As Parameter_InsertSlip_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = '" & inBasicParam.openCenID & "' AND SL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
            Dim Slip_ID As Object = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            If Slip_ID Is Nothing Then
                Dim rec_ID As String = Guid.NewGuid.ToString
                Query = "INSERT INTO [dbo].[slip_info] ([SL_CEN_ID],[SL_COD_YEAR_ID],[SL_PRINT_DATE],[SL_NO],[REC_ID],[SL_BA_REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" & _
                                                 ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  " NULL," & _
                                                  " " & InSlip.SlipNo & "," & _
                                                  "'" & rec_ID & "'," & _
                                                  " '" & InSlip.Dep_BA_ID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1 " & ")"
                dbService.Insert(ConnectOneWS.Tables.SLIP_INFO, Query, inBasicParam, Nothing)

                Query = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = '" & inBasicParam.openCenID & "' AND SL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
                Slip_ID = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            End If

            Dim OnlineQuery As String = "INSERT INTO [dbo].[TRANSACTION_D_SLIP_INFO]([TR_CEN_ID],[TR_COD_YEAR_ID],[TR_M_ID],[TR_SR_NO],[TR_SLIP_ID],[REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" & _
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
        ''' Update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " & _
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Cr_Led_ID.Length = 0 Then UpParam.Cr_Led_ID = "NULL" Else If Not UpParam.Cr_Led_ID.StartsWith("'") Then UpParam.Cr_Led_ID = "'" & UpParam.Cr_Led_ID & "'"
            If UpParam.Dr_Led_ID.Length = 0 Then UpParam.Dr_Led_ID = "NULL" Else If Not UpParam.Dr_Led_ID.StartsWith("'") Then UpParam.Dr_Led_ID = "'" & UpParam.Dr_Led_ID & "'"
            If UpParam.Sub_Cr_Led_ID.Length = 0 Then UpParam.Sub_Cr_Led_ID = "NULL" Else If Not UpParam.Sub_Cr_Led_ID.StartsWith("'") Then UpParam.Sub_Cr_Led_ID = "'" & UpParam.Sub_Cr_Led_ID & "'"
            If UpParam.SUB_Dr_Led_ID.Length = 0 Then UpParam.SUB_Dr_Led_ID = "NULL" Else If Not UpParam.SUB_Dr_Led_ID.StartsWith("'") Then UpParam.SUB_Dr_Led_ID = "'" & UpParam.SUB_Dr_Led_ID & "'"
            If UpParam.RefBankID.Length = 0 Then UpParam.RefBankID = "NULL" Else If Not UpParam.RefBankID.StartsWith("'") Then UpParam.RefBankID = "'" & UpParam.RefBankID & "'"
            If UpParam.RefBranch.Length = 0 Then UpParam.RefBranch = "NULL" Else If Not UpParam.RefBranch.StartsWith("'") Then UpParam.RefBranch = "'" & UpParam.RefBranch & "'"
            If UpParam.Ref_No.Length = 0 Then UpParam.Ref_No = "NULL" Else If Not UpParam.Ref_No.StartsWith("'") Then UpParam.Ref_No = "'" & UpParam.Ref_No & "'"
            If UpParam.Ref_Others.Length = 0 Then UpParam.Ref_Others = "NULL" Else If Not UpParam.Ref_Others.StartsWith("'") Then UpParam.Ref_Others = "'" & UpParam.Ref_Others & "'"
            If UpParam.Mode.Length = 0 Then UpParam.Mode = "NULL" Else If Not UpParam.Mode.StartsWith("'") Then UpParam.Mode = "'" & UpParam.Mode & "'"
            If UpParam.MT_Bank_ID.Length = 0 Then UpParam.MT_Bank_ID = "NULL" Else If Not UpParam.MT_Bank_ID.StartsWith("'") Then UpParam.MT_Bank_ID = "'" & UpParam.MT_Bank_ID & "'"
            If UpParam.MT_AccNo.Length = 0 Then UpParam.MT_AccNo = "NULL" Else If Not UpParam.MT_AccNo.StartsWith("'") Then UpParam.MT_AccNo = "'" & UpParam.MT_AccNo & "'"
            If UpParam.AB_ID_1.Length = 0 Then UpParam.AB_ID_1 = "NULL" Else If Not UpParam.AB_ID_1.StartsWith("'") Then UpParam.AB_ID_1 = "'" & UpParam.AB_ID_1 & "'"
            If UpParam.AB_ID_2.Length = 0 Then UpParam.AB_ID_2 = "NULL" Else If Not UpParam.AB_ID_2.StartsWith("'") Then UpParam.AB_ID_2 = "'" & UpParam.AB_ID_2 & "'"
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                                " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_ITEM_ID     ='" & UpParam.ItemID & "', " & _
                                                " TR_TYPE        ='" & UpParam.Type & "', " & _
                                                " TR_CR_LED_ID   = " & UpParam.Cr_Led_ID & " , " & _
                                                " TR_DR_LED_ID   = " & UpParam.Dr_Led_ID & " , " & _
                                                " TR_SUB_CR_LED_ID  =" & UpParam.Sub_Cr_Led_ID & ", " & _
                                                " TR_SUB_DR_LED_ID  =" & UpParam.SUB_Dr_Led_ID & ", " & _
                                                " TR_MODE        = " & UpParam.Mode & " , " & _
                                                " TR_REF_BANK_ID = " & UpParam.RefBankID & " , " & _
                                                " TR_REF_BRANCH  = " & UpParam.RefBranch & " , " & _
                                                " TR_REF_NO      = " & UpParam.Ref_No & " , " & _
                                                " TR_REF_DATE    = " & If(IsDate(UpParam.Ref_Date), "'" & Convert.ToDateTime(UpParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_REF_CDATE   = " & If(IsDate(UpParam.Ref_ChequeDate), "'" & Convert.ToDateTime(UpParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_REF_OTHERS  = " & UpParam.Ref_Others & " , " & _
                                                " TR_AMOUNT      = " & UpParam.Amount & ", " & _
                                                " TR_MT_BANK_ID  = " & UpParam.MT_Bank_ID & " , " & _
                                                " TR_MT_ACC_NO   = " & UpParam.MT_AccNo & " , " & _
                                                " TR_AB_ID_1     = " & UpParam.AB_ID_1 & " , " & _
                                                " TR_AB_ID_2     = " & UpParam.AB_ID_2 & " , " & _
                                                " TR_NARRATION   ='" & UpParam.Narration & "', " & _
                                                " TR_REMARKS     ='" & UpParam.Remarks & "', " & _
                                                " TR_REFERENCE   ='" & UpParam.Reference & "', " & _
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                                "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns> 
        ''' <remarks>RealServiceFunctions.InternalTransfer_Update_CrossReference</remarks>
        Public Shared Function Update_CrossReference(ByVal Param As Param_Voucher_InternalTransfer_Update_CrossReference, inBasicParam As ConnectOneWS.Basic_Param, Txn_Date As Date, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
                                        " TR_TRF_CROSS_REF_ID   ='" & Param.Cross_Ref_ID & "', " & _
                                        " REC_EDIT_ON           ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY           ='" & inBasicParam.openUserID & "' " & _
                                        " WHERE TR_M_ID          ='" & Param.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, EditTime, Txn_Date)
            Return True
        End Function

        ''' <summary>
        ''' Match Transfers
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_MatchTransfers</remarks>
        Public Shared Function MatchTransfers(ByVal Param As Param_Voucher_InternalTransfer_MatchTransfers, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Result As Boolean = True
            'Add mapping
            Dim Query As String = "UPDATE TRANSACTION_INFO SET " & _
                                    "TR_TRF_CROSS_REF_ID   = NULL, REC_EDIT_ON  ='" & Common.DateTimePlaceHolder & "' " & _
                                    "WHERE TR_TRF_CROSS_REF_ID    ='" & Param.MatchingRecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inBasicParam) 'Txn Date is not specified and checked , as Matching / Unmatching is allowed in restricted period to auditors only 
            Result = True

            'Remove Prev mappings
            If Result Then
                Query = "UPDATE TRANSACTION_INFO SET " & _
                        "TR_TRF_CROSS_REF_ID   ='" & Param.MatchingRecID & "', " & _
                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                        "WHERE REC_ID    ='" & Param.RecID & "'"
                dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inBasicParam)
                Result = True
            End If

            'add reverse mapping
            If Result Then
                Query = "UPDATE TRANSACTION_INFO SET " & _
                        "TR_TRF_CROSS_REF_ID   ='" & Param.RecID & "', " & _
                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                        "WHERE REC_ID    ='" & Param.MatchingRecID & "'"
                dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inBasicParam)
                Result = True
            End If
            Return Result
        End Function

        ''' <summary>
        ''' UnMatch Transfers
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UnMatchTransfers</remarks>
        Public Shared Function UnMatchTransfers(ByVal Param As Param_Voucher_InternalTransfer_UnMatchTransfers, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Result As Boolean = True
            'Add mapping
            Dim Query As String = "UPDATE TRANSACTION_INFO SET " & _
                                    "TR_TRF_CROSS_REF_ID   = NULL, REC_EDIT_ON = '" & Common.DateTimePlaceHolder & "',REC_EDIT_BY ='" & inBasicParam.openUserID & "' " & _
                                    "WHERE TR_TRF_CROSS_REF_ID   IN  ('" & Param.MatchingRecID & "','" & Param.RecID & "')"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inBasicParam, DateTime.Now, Param.TxnDate) 'Txn Date is not specified and checked , as Matching / Unmatching is allowed in restricted period to auditors only 
            Result = True
            Return Result
        End Function

        ''' <summary>
        ''' Update Purpose
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UpdatePurpose</remarks>
        Public Shared Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherInternalTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Transaction_D_Purpose_Info SET " & _
                                         " TR_PURPOSE_MISC_ID    ='" & UpPurpose.PurposeID & "', " & _
                                         " TR_AMOUNT             =" & UpPurpose.Amount & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                            "  WHERE TR_REC_ID    ='" & UpPurpose.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function Insert_InternalTransfer_Txn(inParam As Param_Txn_Insert_InternalTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean

            If Not inParam.param_InsertEP1 Is Nothing Then
                If Not inParam.param_InsertEP1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.Dr_Led_ID)
                If Not inParam.param_InsertEP1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.Cr_Led_ID)
                If Not inParam.param_InsertEP1.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.SUB_Cr_Led_ID)
                If Not inParam.param_InsertEP1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.SUB_Dr_Led_ID)
                If Not inParam.param_InsertEP1.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.AB_ID_1)
                If Not inParam.param_InsertEP1.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP1.TDate), inParam.param_InsertEP1.AB_ID_2)
            End If
            If Not inParam.param_InsertEP2 Is Nothing Then
                If Not inParam.param_InsertEP2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.Dr_Led_ID)
                If Not inParam.param_InsertEP2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.Cr_Led_ID)
                If Not inParam.param_InsertEP2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.SUB_Cr_Led_ID)
                If Not inParam.param_InsertEP2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.SUB_Dr_Led_ID)
                If Not inParam.param_InsertEP2.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.AB_ID_1)
                If Not inParam.param_InsertEP2.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertEP2.TDate), inParam.param_InsertEP2.AB_ID_2)
            End If

            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            '  Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMaster(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertEP1 Is Nothing Then
                If Not Insert(inParam.param_InsertEP1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertPurposeEp1 Is Nothing Then
                If Not InsertPurpose(inParam.param_InsertPurposeEp1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertEP2 Is Nothing Then
                If Not Insert(inParam.param_InsertEP2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.parma_InsertPurposeEP2 Is Nothing Then
                If Not InsertPurpose(inParam.parma_InsertPurposeEP2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(inParam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert_TDSDed Is Nothing Then
                For Each cParam As Parameter_InsertTDSDeduction_VoucherInternalTransfer In inParam.param_Insert_TDSDed
                    If Not cParam Is Nothing Then If Not InsertTDSDeduction(cParam, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
                Next
            End If
            If Not inParam.param_UpdateCrossRef Is Nothing Then
                If Not Update_CrossReference(inParam.param_UpdateCrossRef, inBasicParam, inParam.param_InsertMaster.TDate, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '  End Using
            'Commit here 
            ' txn.Complete()
            ' End Using

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

        Public Shared Function Update_InternalTransfer_Txn(upParam As Param_Txn_Update_InternalTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Res As Boolean
            If Not upParam.param_UpdateEP1 Is Nothing Then
                If Not upParam.param_UpdateEP1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.Dr_Led_ID)
                If Not upParam.param_UpdateEP1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.Cr_Led_ID)
                If Not upParam.param_UpdateEP1.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.Sub_Cr_Led_ID)
                If Not upParam.param_UpdateEP1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.SUB_Dr_Led_ID)
                If Not upParam.param_UpdateEP1.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.AB_ID_1)
                If Not upParam.param_UpdateEP1.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP1.TDate), upParam.param_UpdateEP1.AB_ID_2)
            End If
            If Not upParam.param_UpdateEP2 Is Nothing Then
                If Not upParam.param_UpdateEP2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.Dr_Led_ID)
                If Not upParam.param_UpdateEP2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.Cr_Led_ID)
                If Not upParam.param_UpdateEP2.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.Sub_Cr_Led_ID)
                If Not upParam.param_UpdateEP2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.SUB_Dr_Led_ID)
                If Not upParam.param_UpdateEP2.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.AB_ID_1)
                If Not upParam.param_UpdateEP2.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_UpdateEP2.TDate), upParam.param_UpdateEP2.AB_ID_2)
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
            'Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateEP1 Is Nothing Then
                If Not Update(upParam.param_UpdateEP1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdatePurposeEP1 Is Nothing Then
                If Not UpdatePurpose(upParam.param_UpdatePurposeEP1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateEP2 Is Nothing Then
                If Not Update(upParam.param_UpdateEP2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdatePurposeEP2 Is Nothing Then
                If Not UpdatePurpose(upParam.param_UpdatePurposeEP2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertEP2 Is Nothing Then
                If Not Insert(upParam.param_InsertEP2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurposeEP2 Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertPurposeEP2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.ID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_M_ID = '" & upParam.ID_DeleteSlip & "'", inBasicParam)
            End If
            If Not upParam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(upParam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.ID2_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID ='" & upParam.ID2_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.ID2_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_INFO, upParam.ID2_Delete, inBasicParam)
            End If
            If Not upParam.ID_DeleteTDSDeduction Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_TDS_INFO, "TR_M_ID = '" & upParam.ID_DeleteTDSDeduction & "'", inBasicParam)
            End If
            If Not upParam.param_Insert_TDSDed Is Nothing Then
                For Each cParam As Parameter_InsertTDSDeduction_VoucherInternalTransfer In upParam.param_Insert_TDSDed
                    If Not cParam Is Nothing Then If Not InsertTDSDeduction(cParam, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
                Next
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.ID_DeleteSlip & "'", inBasicParam)
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

        Public Shared Function Delete_InternalTransfer_Txn(delParam As Param_Txn_Delete_InternalTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.ID1_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_INFO, delParam.ID1_Delete, inBasicParam)
            End If
            If Not delParam.ID2_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_INFO, delParam.ID2_Delete, inBasicParam)
            End If
            If Not delParam.ID1_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID ='" & delParam.ID1_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.ID2_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID ='" & delParam.ID2_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            If Not delParam.ID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_M_ID = '" & delParam.ID_DeleteSlip & "'", inBasicParam)
            End If
            If Not delParam.ID_DeleteTDSDeduction Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_TDS_INFO, "TR_M_ID = '" & delParam.ID_DeleteTDSDeduction & "'", inBasicParam)
            End If
            '  End Using
            'Commit here 
            '  txn.Complete()
            ' End Using
            Return True
        End Function


    End Class
#End Region

End Namespace


