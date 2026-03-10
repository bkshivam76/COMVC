Imports System.Data
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class Voucher_SaleOfAsset
#Region "Param Classes"
        <Serializable>
        Public Class Param_Voucher_SaleOfAsset_GetTxnList
            Public IsNew As Boolean
            Public TxnMID As String
            Public YearID As Integer
            Public NextUnAuditedYearID As Integer
        End Class
        <Serializable>
        Public Class Param_GetAssetListingForSale
            Public Asset_Profile As ConnectOneWS.AssetProfiles
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TR_M_ID As String
        End Class
        <Serializable>
        Public Class Param_GetAssetMaxTxnDate
            Public YearID As Integer
            Public Asset_RecID As String
            Public Creation_Date As Date
            Public Tr_M_ID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetAssetEnclosingTxnDate
            Public YearID As Integer
            Public Asset_RecID As String
            Public Creation_Date As Date
            Public Tr_M_ID As String
            Public Year_End_Date As Date
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherSaleOfAsset
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
            Public SaleAmt As Double
            Public SaleQty As Double
            Public SaleDate As String
            Public SaleType As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherSaleOfAsset
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
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherSaleOfAsset
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
        Public Class Parameter_InsertPurpose_VoucherSaleOfAsset
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
            Public SrNo As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertAandLPayment_VoucherSaleOfAsset
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
        Public Class Parameter_InsertBankPayment_VoucherSaleOfAsset
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
        Public Class Parameter_UpdateMasterInfo_VoucherSaleOfAsset
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public SaleAmt As Double
            Public SaleQty As Double
            Public SaleDate As String
            Public SaleType As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherSaleOfAsset
            Public param_InsertMasterEntry As Parameter_InsertMasterInfo_VoucherSaleOfAsset
            Public param_InsertTransactionInfo As Parameter_Insert_VoucherSaleOfAsset
            Public param_InsertAandLPayment As Parameter_InsertAandLPayment_VoucherSaleOfAsset
            Public param_InsertCreditJV As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertDebitJV As Parameter_Insert_VoucherSaleOfAsset = Nothing
            'Public param_InsertAssetPL As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPL As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPL_2 As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPaymentAdjustment As Parameter_InsertAandLPayment_VoucherSaleOfAsset = Nothing
            Public param_InsertProfileAdvances As Advances.Parameter_InsertTRID_Advances = Nothing
            Public param_InsertPurposeJV As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public param_InsertPurposePL As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherSaleOfAsset
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherSaleOfAsset
            Public Mid_DeleteItems As String = Nothing
            Public Mid_DeletePayment As String = Nothing
            Public Mid_DeletePurpose As String = Nothing
            Public Mid_Delete As String = Nothing
            Public Mid_DeleteAdvances As String = Nothing
            Public param_InsertTransactionInfo As Parameter_Insert_VoucherSaleOfAsset
            Public param_InsertAandLPayment As Parameter_InsertAandLPayment_VoucherSaleOfAsset
            Public param_InsertCreditJV As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertDebitJV As Parameter_Insert_VoucherSaleOfAsset = Nothing
            ' Public param_InsertAssetPL As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPL As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPL_2 As Parameter_Insert_VoucherSaleOfAsset = Nothing
            Public param_InsertPaymentAdjustment As Parameter_InsertAandLPayment_VoucherSaleOfAsset = Nothing
            Public param_InsertProfileAdvances As Advances.Parameter_InsertTRID_Advances = Nothing
            Public param_InsertPurposeJV As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public param_InsertPurposePL As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherSaleOfAsset = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherSaleOfAsset
            Public Mid_DeleteItems As String = Nothing
            Public Mid_DeletePayment As String = Nothing
            Public Mid_DeletePurpose As String = Nothing
            Public Mid_DeleteAdvances As String = Nothing
            Public Mid_Delete As String = Nothing
            Public Mid_DeleteMaster As String = Nothing
        End Class
#End Region

        ''' <summary>
        ''' Get MasterID
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetMasterID</remarks>
        Public Shared Function GetMasterID(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_M_ID FROM transaction_info WHERE REC_ID ='" & Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Txn Items, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetTxnItems</remarks>
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
        ''' <remarks>RealServiceFunctions.SaleOfAssets_GetBankPayments</remarks>
        Public Shared Function GetBankPayments(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select *,'' AS BANK_NAME,'' AS BRANCH_NAME,'' AS ACC_NO,'' AS MT_BANK_NAME  from TRANSACTION_D_PAYMENT_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Rec_ID & "' AND TR_PAY_TYPE='BANK' ORDER BY TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Txn List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.SaleOfAssets_GetTxnList</remarks>
        Public Shared Function GetTxnList(ByVal Param As Param_Voucher_SaleOfAsset_GetTxnList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim NextYear As String = ""
            If Param.NextUnAuditedYearID.ToString.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & Param.NextUnAuditedYearID.ToString & ""
            If Not Param.IsNew Then
                Query = "SELECT TP.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'SALE_QTY' FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('SALE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=11  AND TP.TR_M_ID  <> '" & Param.TxnMID & "' AND (TM.TR_COD_YEAR_ID = " & Param.YearID.ToString & " " & NextYear & ") GROUP BY TP.TR_REF_ID "
            Else
                Query = "SELECT TP.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'SALE_QTY' FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('SALE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=11  AND (TM.TR_COD_YEAR_ID = " & Param.YearID.ToString & " " & NextYear & ") GROUP BY TP.TR_REF_ID "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_AssetsListingForSale(ByVal Param As Param_GetAssetListingForSale, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Sale_Asset_Listing"
            Dim params() As String = {"CENID", "YEARID", "ASSET_PROFILE", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, dbService.GetAssetProfileFromEnum(Param.Asset_Profile), Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, Param.TR_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_AssetsMaxTxnDate(ByVal Param As Param_GetAssetMaxTxnDate, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Asset_MaxTxnDate"
            Dim params() As String = {"YEARID", "ASSET_RECID", "ASSET_CREATION_DATE", "CENID", "TR_M_ID"}
            Dim values() As Object = {inBasicParam.openYearID, Param.Asset_RecID, Param.Creation_Date, inBasicParam.openCenID, Param.Tr_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 36, 5, 36}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_AssetsEnclosingTxnDate(ByVal Param As Param_GetAssetEnclosingTxnDate, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Asset_EnclosingReferenceDate"
            Dim params() As String = {"YEARID", "ASSET_RECID", "ASSET_CREATION_DATE", "TR_M_ID", "YEAR_END_DATE"}
            Dim values() As Object = {inBasicParam.openYearID, Param.Asset_RecID, Param.Creation_Date, Param.Tr_M_ID, Param.Year_End_Date}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.Date}
            Dim lengths() As Integer = {4, 36, 36, 36, 50}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT,TR_SALE_AMT,TR_SALE_QTY,TR_SALE_DATE,TR_SALE_TYPE," & _
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
                                                  " " & InMInfo.SaleAmt & "," & _
                                                  " " & InMInfo.SaleQty & "," & _
                                                  "" & If(IsDate(InMInfo.SaleDate), "'" & Convert.ToDateTime(InMInfo.SaleDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InMInfo.SaleType & "', " & _
                                                   "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert 
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.Sub_Cr_Led_ID.Length = 0 Then InParam.Sub_Cr_Led_ID = "NULL" Else If Not InParam.Sub_Cr_Led_ID.StartsWith("'") Then InParam.Sub_Cr_Led_ID = "'" & InParam.Sub_Cr_Led_ID & "'"
            If InParam.Sub_Dr_Led_ID.Length = 0 Then InParam.Sub_Dr_Led_ID = "NULL" Else If Not InParam.Sub_Dr_Led_ID.StartsWith("'") Then InParam.Sub_Dr_Led_ID = "'" & InParam.Sub_Dr_Led_ID & "'"
            If InParam.Ref_Bank.Length = 0 Then InParam.Ref_Bank = "NULL" Else If Not InParam.Ref_Bank.StartsWith("'") Then InParam.Ref_Bank = "'" & InParam.Ref_Bank & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.Tr_M_ID.Length = 0 Then InParam.Tr_M_ID = "NULL" Else If Not InParam.Tr_M_ID.StartsWith("'") Then InParam.Tr_M_ID = "'" & InParam.Tr_M_ID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.PartyID.Length = 0 Then InParam.PartyID = "NULL" Else If Not InParam.PartyID.StartsWith("'") Then InParam.PartyID = "'" & InParam.PartyID & "'"
            If InParam.Cross_Ref_ID.Length = 0 Then InParam.Cross_Ref_ID = "NULL" Else If Not InParam.Cross_Ref_ID.StartsWith("'") Then InParam.Cross_Ref_ID = "'" & InParam.Cross_Ref_ID & "'"
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
                                            "" & Str & "  '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
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
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," & _
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

        ''' <summary>
        ''' Insert Advances And Liabilities Payment
        ''' </summary>
        ''' <param name="InPay"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertPayment</remarks>
        Public Shared Function InsertPayment(ByVal InPay As Parameter_InsertAandLPayment_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPay.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_REF_ID,TR_REF_AMT," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InPay.TxnMID & "', " & _
                                        "'" & InPay.Type & "', " & _
                                        " " & InPay.SrNo & " , " & _
                                        "'" & InPay.RefID & "', " & _
                                        " " & InPay.RefAmount & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPay.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPay.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        '''  Bank Payments
        ''' </summary>
        ''' <param name="InBpay"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_InsertBankPayment</remarks>
        Public Shared Function InsertPayment(ByVal InBpay As Parameter_InsertBankPayment_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_MT_BANK_ID,TR_MT_ACC_NO," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InBpay.TxnMID & "', " & _
                                        "'" & InBpay.Type & "', " & _
                                        " " & InBpay.SrNo & " , " & _
                                        "'" & InBpay.Mode & "', " & _
                                        "'" & InBpay.RefID & "', " & _
                                        "'" & InBpay.RefNo & "', " & _
                                        " " & If(IsDate(InBpay.RefDate), "'" & Convert.ToDateTime(InBpay.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        " " & If(IsDate(InBpay.RefCDate), "'" & Convert.ToDateTime(InBpay.RefCDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        " " & InBpay.RefAmount & ", " & _
                                        " '" & InBpay.BankID & "', " & _
                                        " '" & InBpay.AccountNo & "', " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InBpay.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InBpay.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InBpay.RecID)
            Return True
        End Function

        ''' <summary>
        ''' Update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.SaleOfAssets_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " & _
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " & _
                                            " TR_SALE_AMT    = " & UpParam.SaleAmt & "  ," & _
                                            " TR_SALE_QTY    = " & UpParam.SaleQty & "  ," & _
                                            " TR_SALE_DATE   = " & If(IsDate(UpParam.SaleDate), "'" & Convert.ToDateTime(UpParam.SaleDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_SALE_TYPE   ='" & UpParam.SaleType & "', " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        Public Shared Function InsertSaleOfAsset_Txn(InParam As Param_Txn_Insert_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean

            If Not InParam.param_InsertCreditJV Is Nothing Then
                If Not InParam.param_InsertCreditJV.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertCreditJV.TDate), InParam.param_InsertCreditJV.Dr_Led_ID)
                If Not InParam.param_InsertCreditJV.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertCreditJV.TDate), InParam.param_InsertCreditJV.Cr_Led_ID)
                If Not InParam.param_InsertCreditJV.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertCreditJV.TDate), InParam.param_InsertCreditJV.Sub_Cr_Led_ID)
                If Not InParam.param_InsertCreditJV.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertCreditJV.TDate), InParam.param_InsertCreditJV.Sub_Dr_Led_ID)
                If Not InParam.param_InsertCreditJV.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertCreditJV.TDate), InParam.param_InsertCreditJV.PartyID)
            End If
            If Not InParam.param_InsertDebitJV Is Nothing Then
                If Not InParam.param_InsertDebitJV.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertDebitJV.TDate), InParam.param_InsertDebitJV.Dr_Led_ID)
                If Not InParam.param_InsertDebitJV.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertDebitJV.TDate), InParam.param_InsertDebitJV.Cr_Led_ID)
                If Not InParam.param_InsertDebitJV.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertDebitJV.TDate), InParam.param_InsertDebitJV.Sub_Cr_Led_ID)
                If Not InParam.param_InsertDebitJV.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertDebitJV.TDate), InParam.param_InsertDebitJV.Sub_Dr_Led_ID)
                If Not InParam.param_InsertDebitJV.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertDebitJV.TDate), InParam.param_InsertDebitJV.PartyID)
            End If
            If Not InParam.param_InsertPL Is Nothing Then
                If Not InParam.param_InsertPL.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL.TDate), InParam.param_InsertPL.Dr_Led_ID)
                If Not InParam.param_InsertPL.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL.TDate), InParam.param_InsertPL.Cr_Led_ID)
                If Not InParam.param_InsertPL.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL.TDate), InParam.param_InsertPL.Sub_Cr_Led_ID)
                If Not InParam.param_InsertPL.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL.TDate), InParam.param_InsertPL.Sub_Dr_Led_ID)
                If Not InParam.param_InsertPL.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL.TDate), InParam.param_InsertPL.PartyID)
            End If
            If Not InParam.param_InsertPL_2 Is Nothing Then
                If Not InParam.param_InsertPL_2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL_2.TDate), InParam.param_InsertPL_2.Dr_Led_ID)
                If Not InParam.param_InsertPL_2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL_2.TDate), InParam.param_InsertPL_2.Cr_Led_ID)
                If Not InParam.param_InsertPL_2.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL_2.TDate), InParam.param_InsertPL_2.Sub_Cr_Led_ID)
                If Not InParam.param_InsertPL_2.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL_2.TDate), InParam.param_InsertPL_2.Sub_Dr_Led_ID)
                If Not InParam.param_InsertPL_2.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.param_InsertPL_2.TDate), InParam.param_InsertPL_2.PartyID)
            End If

            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            ' Using Common.GetConnectionScope()
            If Not InParam.param_InsertMasterEntry Is Nothing Then
                If Not InsertMasterInfo(InParam.param_InsertMasterEntry, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertTransactionInfo Is Nothing Then
                If Not Insert(InParam.param_InsertTransactionInfo, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertAandLPayment Is Nothing Then
                If Not InsertPayment(InParam.param_InsertAandLPayment, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertCreditJV Is Nothing Then
                If Not Insert(InParam.param_InsertCreditJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertDebitJV Is Nothing Then
                If Not Insert(InParam.param_InsertDebitJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not InParam.param_InsertAssetPL Is Nothing Then
            '    If Not Insert(InParam.param_InsertAssetPL, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If
            If Not InParam.param_InsertPL Is Nothing Then
                If Not Insert(InParam.param_InsertPL, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertPL_2 Is Nothing Then
                If Not Insert(InParam.param_InsertPL_2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertPaymentAdjustment Is Nothing Then
                If Not InsertPayment(InParam.param_InsertPaymentAdjustment, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertProfileAdvances Is Nothing Then ' Insert in profile Advances
                If Not Advances.Insert(InParam.param_InsertProfileAdvances, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertPurposeJV Is Nothing Then
                If Not InsertPurpose(InParam.param_InsertPurposeJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertPurposePL Is Nothing Then
                If Not InsertPurpose(InParam.param_InsertPurposePL, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not InParam.param_InsertPurpose Is Nothing Then
                If Not InsertPurpose(InParam.param_InsertPurpose, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '   End Using
            'Commit here 
            '    txn.Complete()
            '   End Using

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(InParam.InsertSplVchrRefs, InParam.param_InsertMasterEntry.RecID, Nothing, Nothing, inBasicParam)
            'If InParam.InsertSplVchrRefs IsNot Nothing Then
            '    If InParam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In InParam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, InParam.param_InsertPurpose.TxnID, InParam.param_InsertMasterEntry.SubTotal, inBasicParam)
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
        Public Shared Function UpdateSaleOfAsset_Txn(upParam As Param_Txn_Update_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_InsertCreditJV Is Nothing Then
                If Not upParam.param_InsertCreditJV.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCreditJV.TDate), upParam.param_InsertCreditJV.Dr_Led_ID)
                If Not upParam.param_InsertCreditJV.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCreditJV.TDate), upParam.param_InsertCreditJV.Cr_Led_ID)
                If Not upParam.param_InsertCreditJV.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCreditJV.TDate), upParam.param_InsertCreditJV.Sub_Cr_Led_ID)
                If Not upParam.param_InsertCreditJV.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCreditJV.TDate), upParam.param_InsertCreditJV.Sub_Dr_Led_ID)
                If Not upParam.param_InsertCreditJV.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCreditJV.TDate), upParam.param_InsertCreditJV.PartyID)
            End If
            If Not upParam.param_InsertDebitJV Is Nothing Then
                If Not upParam.param_InsertDebitJV.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertDebitJV.TDate), upParam.param_InsertDebitJV.Dr_Led_ID)
                If Not upParam.param_InsertDebitJV.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertDebitJV.TDate), upParam.param_InsertDebitJV.Cr_Led_ID)
                If Not upParam.param_InsertDebitJV.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertDebitJV.TDate), upParam.param_InsertDebitJV.Sub_Cr_Led_ID)
                If Not upParam.param_InsertDebitJV.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertDebitJV.TDate), upParam.param_InsertDebitJV.Sub_Dr_Led_ID)
                If Not upParam.param_InsertDebitJV.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertDebitJV.TDate), upParam.param_InsertDebitJV.PartyID)
            End If
            If Not upParam.param_InsertPL Is Nothing Then
                If Not upParam.param_InsertPL.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL.TDate), upParam.param_InsertPL.Dr_Led_ID)
                If Not upParam.param_InsertPL.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL.TDate), upParam.param_InsertPL.Cr_Led_ID)
                If Not upParam.param_InsertPL.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL.TDate), upParam.param_InsertPL.Sub_Cr_Led_ID)
                If Not upParam.param_InsertPL.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL.TDate), upParam.param_InsertPL.Sub_Dr_Led_ID)
                If Not upParam.param_InsertPL.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL.TDate), upParam.param_InsertPL.PartyID)
            End If
            If Not upParam.param_InsertPL_2 Is Nothing Then
                If Not upParam.param_InsertPL_2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL_2.TDate), upParam.param_InsertPL_2.Dr_Led_ID)
                If Not upParam.param_InsertPL_2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL_2.TDate), upParam.param_InsertPL_2.Cr_Led_ID)
                If Not upParam.param_InsertPL_2.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL_2.TDate), upParam.param_InsertPL_2.Sub_Cr_Led_ID)
                If Not upParam.param_InsertPL_2.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL_2.TDate), upParam.param_InsertPL_2.Sub_Dr_Led_ID)
                If Not upParam.param_InsertPL_2.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertPL_2.TDate), upParam.param_InsertPL_2.PartyID)
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
            If Not upParam.Mid_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & upParam.Mid_DeleteItems & "'", inBasicParam)
            End If
            If Not upParam.Mid_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.Mid_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.Mid_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.Mid_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.Mid_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.Mid_Delete & "'", inBasicParam)
            End If
            If Not upParam.Mid_DeleteAdvances Is Nothing Then 'delete from profile
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADVANCES_INFO, "AI_TR_ID = '" & upParam.Mid_DeleteAdvances & "'", inBasicParam)
            End If
            If Not upParam.param_InsertTransactionInfo Is Nothing Then
                If Not Insert(upParam.param_InsertTransactionInfo, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertAandLPayment Is Nothing Then
                If Not InsertPayment(upParam.param_InsertAandLPayment, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertCreditJV Is Nothing Then
                If Not Insert(upParam.param_InsertCreditJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertDebitJV Is Nothing Then
                If Not Insert(upParam.param_InsertDebitJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not upParam.param_InsertAssetPL Is Nothing Then
            '    If Not Insert(upParam.param_InsertAssetPL, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If
            If Not upParam.param_InsertPL Is Nothing Then
                If Not Insert(upParam.param_InsertPL, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPL_2 Is Nothing Then
                If Not Insert(upParam.param_InsertPL_2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPaymentAdjustment Is Nothing Then
                If Not InsertPayment(upParam.param_InsertPaymentAdjustment, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertProfileAdvances Is Nothing Then
                If Not Advances.Insert(upParam.param_InsertProfileAdvances, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurposeJV Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertPurposeJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurposePL Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertPurposePL, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurpose Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertPurpose, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.Mid_DeletePurpose & "'", inBasicParam)
            InsertSpecialVoucherReference(upParam.UpdateSplVchrRefs, upParam.param_UpdateMaster.RecID, Nothing, Nothing, inBasicParam)
            'If upParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If upParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In upParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, upParam.param_InsertPurpose.TxnID, upParam.param_UpdateMaster.SubTotal, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function DeleteSaleOfAssets_Txn(DelParam As Param_Txn_Delete_VoucherSaleOfAsset, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.Mid_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & DelParam.Mid_Delete & "'", inBasicParam)
            End If
            If Not DelParam.Mid_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & DelParam.Mid_DeleteItems & "'", inBasicParam)
            End If
            If Not DelParam.Mid_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & DelParam.Mid_DeletePayment & "'", inBasicParam)
            End If
            If Not DelParam.Mid_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & DelParam.Mid_DeletePurpose & "'", inBasicParam)
            End If
            If Not DelParam.Mid_DeleteAdvances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADVANCES_INFO, "AI_TR_ID = '" & DelParam.Mid_DeleteAdvances & "'", inBasicParam)
            End If
            If Not DelParam.Mid_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, DelParam.Mid_DeleteMaster, inBasicParam)
            End If
            '   End Using
            'Commit here 
            '   txn.Complete()
            '  End Using
            Return True
        End Function


    End Class
End Namespace
