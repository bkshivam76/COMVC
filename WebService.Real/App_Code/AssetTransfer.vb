Imports System.Data
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class Voucher_AssetTransfer
#Region "Param Classes"
        <Serializable>
        Public Class Param_Voucher_AssetTransfer_GetTxnList
            Public IsNew As Boolean
            Public TxnMID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_AssetTransfer_GetFrCenterList
            Public IncludeCenIDs As String
            Public ExcludeCenIDs As String
            Public IncludeRecIDs As String
            Public ExcludeRecIDs As String
            Public openInsID As String
        End Class

        <Serializable>
        Public Class Param_Voucher_AssetTransfer_GetToCenterList : Inherits Param_Voucher_AssetTransfer_GetFrCenterList
            'Same Params
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherAssetTransfer
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public AssetRef_ID As String
            Public AssetTrf_Amt As Double
            Public AssetTrf_Qty As Double
            Public AssetTrf_Date As String
            Public AssetTrf_Type As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherAssetTransfer
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
            Public AB_ID_1 As String
            Public AB_ID_2 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public Tr_M_ID As String
            Public TxnSrNo As Integer
            Public Cross_Ref_ID As String
            Public AssetTrf_Qty As Double
            Public AssetTrf_RefItemID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherAssetTransfer
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
            'Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherAssetTransfer
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertAandLPayment_VoucherAssetTransfer
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public RefID As String
            Public RefAmount As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateMasterInfo_VoucherAssetTransfer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public AssetRef_ID As String
            Public AssetTrf_Amt As Double
            Public AssetTrf_Qty As Double
            Public AssetTrf_Date As String
            Public AssetTrf_Type As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount
            Public OpenCenRecID As String
            Public TopCount As Int32
            Public EnteringCenID As Integer = Nothing
            Public RecID As String = Nothing
        End Class
        <Serializable>
        Public Class Param_VoucherAssetTransfer_GetUnmatchedCount
            Public CurrInsttID As String
            Public OpenCenRecID As String
            Public FromDate As DateTime = DateTime.MinValue
            Public ToDate As DateTime = DateTime.MinValue
        End Class
        <Serializable>
        Public Class Param_VoucherAssetTransfer_GetUnmatchedFromCount
            Inherits Param_VoucherAssetTransfer_GetUnmatchedCount
        End Class
        <Serializable>
        Public Class Param_Get_AssetTf_Asset_Listing
            Public Asset_Profile As ConnectOneWS.AssetProfiles
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TR_M_ID As String
            Public Cen_Id As Integer
        End Class
        <Serializable>
        Public Class Param_VoucherAssetTransfer_Update_CrossReference
            Public Cross_Ref_ID As String
            Public RecID As String
            'COMMENTS
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherAssetTransfer
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherAssetTransfer
            Public param_InsertTxnInfo1 As Parameter_Insert_VoucherAssetTransfer
            Public param_InsertTxnInfo2 As Parameter_Insert_VoucherAssetTransfer
            Public param_InsertAandLPayment As Parameter_InsertAandLPayment_VoucherAssetTransfer = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherAssetTransfer = Nothing
            Public param_UpdateCrossRef As Param_VoucherAssetTransfer_Update_CrossReference = Nothing
            Public param_InsertProfile As Parameter_Insert_Profile = Nothing
            Public prev_year_id As Integer = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherAssetTransfer
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherAssetTransfer
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_Delete As String = Nothing
            Public param_InsertTxnInfo1 As Parameter_Insert_VoucherAssetTransfer = Nothing
            Public param_InsertTxnInfo2 As Parameter_Insert_VoucherAssetTransfer = Nothing
            Public param_InsertAandLPayment As Parameter_InsertAandLPayment_VoucherAssetTransfer = Nothing
            Public param_InsertPurpose As Parameter_InsertPurpose_VoucherAssetTransfer = Nothing
            Public MID As String = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherAssetTransfer
            Public MID_DeletePayment As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
            Public MID_DeleteGS As String = Nothing
            Public MID_DeleteAssets As String = Nothing
            Public MID_DeleteLS As String = Nothing
            Public MID_DeleteVehicle As String = Nothing
            Public MID_DeleteFD As String = Nothing
            Public DeleteComplexBuilding As String = Nothing
            Public DelExtInfo As String = Nothing
            Public DelDocInfo As String = Nothing
            Public MID_DeleteLB As String = Nothing
            Public MID_DeleteAdvance As String = Nothing
            Public MID_DeleteDeposit As String = Nothing
            Public MID_DeleteLiability As String = Nothing
            Public MID_DeleteWIP As String = Nothing
            Public MID_DeleteOpening As String = Nothing
            Public Txn_Date As String
        End Class

        <Serializable>
        Public Class Parameter_Insert_Profile
            Public AssetType As String
            Public AssetRefID As String
            Public AssetNewID As String
            Public AssetLocID As String
            Public AssetOwner As String
            Public AssetOwnerID As String
            Public AssetUse As String
            Public AssetQty As Double
            Public AssetAmt As Double

            Public CenID As Integer
            Public TrID As String
        End Class
        <Serializable>
        Public Class Param_GetAssetTransfers
            Public refAssetID As String
            Public YearID As Integer = Nothing
            Public Exclude_PrevYears As Boolean = False
        End Class

#End Region

        Public Shared Function GetUnMatchedList(ByVal openCenRecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim param As Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount = New Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount
            param.OpenCenRecID = openCenRecID
            param.TopCount = 0
            Return GetUnMatchedList_LimitedCount(param, inBasicParam).Tables(0)
        End Function

        Public Shared Function GetUnMatchedList_LimitedCount(ByVal inParam As Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CentreFilter As String = ""
            If inParam.EnteringCenID.ToString.Length > 0 Then CentreFilter = " AND TI.TR_CEN_ID = " & inParam.EnteringCenID.ToString & ""
            Dim Query As String = "SELECT "

            If inParam.TopCount > 0 Then Query += " TOP " + inParam.TopCount.ToString + " "

            If Not inParam.RecID Is Nothing Then 'added for multiuser check
                Query += " (SELECT ITEM_LINK_REC_ID  FROM ITEM_INFO WHERE REC_STATUS IN (0,1,2) AND REC_ID = T1.TR_ITEM_ID ) AS 'ITEM_ID'," &
                     " (SELECT L.ITEM_NAME FROM ITEM_INFO AS I INNER JOIN  ITEM_INFO AS L  ON (L.REC_ID   = I.ITEM_LINK_REC_ID AND L.REC_STATUS IN (0,1,2) ) WHERE  I.REC_STATUS IN (0,1,2) AND I.REC_ID = T1.TR_ITEM_ID ) AS 'Description'," &
                     " CI.CEN_NAME AS 'Centre Name',MAIN.CEN_INCHARGE AS 'Incharge',COALESCE(MAIN.CEN_TEL_NO_1+',','')+COALESCE(MAIN.CEN_TEL_NO_2+',','')+COALESCE(MAIN.CEN_MOB_NO_1+',','')+COALESCE(MAIN.CEN_MOB_NO_2,'')as 'Contact No.' ,CI.CEN_UID  AS 'Centre UID',CI.CEN_PAD_NO AS 'No.',T1.TR_AB_ID_2 AS 'CEN_ID', " &
                     " TM.TR_DATE AS 'Date', TM.TR_SALE_TYPE as 'Asset Type',T2.TR_ITEM_ID AS 'ASSET_ITEM_ID'    ,(SELECT ITEM_NAME FROM ITEM_INFO WHERE REC_STATUS IN (0,1,2) AND REC_ID = T2.TR_ITEM_ID ) AS 'Asset' ,TM.TR_SALE_QTY AS 'Qty / Weight',TM.TR_SALE_AMT AS 'Amount',T1.TR_REF_OTHERS AS 'ASSET_REF_ID',MP.MISC_NAME AS 'Purpose',MP.REC_ID AS PUR_ID,T1.REC_ID AS ID,T1.TR_M_ID AS M_ID, TM.REC_EDIT_ON " &
                     " FROM transaction_d_master_info AS TM" &
                     " INNER JOIN transaction_info AS T1 ON (TM.REC_ID  = T1.TR_M_ID    AND T1.REC_STATUS IN (0,1,2) AND T1.TR_SR_NO=1 AND T1.TR_CODE=15 AND T1.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T1.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND T1.TR_TRF_CROSS_REF_ID IS NULL )" &
                     " INNER JOIN transaction_info AS T2 ON (TM.REC_ID  = T2.TR_M_ID    AND T2.REC_STATUS IN (0,1,2) AND T2.TR_SR_NO=2 AND T2.TR_CODE=15 AND T2.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T2.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND T2.TR_TRF_CROSS_REF_ID IS NULL AND T2.TR_TYPE = 'CREDIT' )" &
                     " INNER JOIN CENTRE_INFO      AS CI ON (CI.REC_ID  = T1.TR_AB_ID_2 AND CI.REC_STATUS IN (0,1,2) )  " &
                     " INNER JOIN CENTRE_INFO      AS MAIN ON (CI.CEN_BK_PAD_NO   = MAIN.CEN_BK_PAD_NO AND MAIN.CEN_MAIN = 1  AND MAIN.REC_STATUS   IN (0,1,2) )  " &
                     " LEFT  JOIN Transaction_D_Purpose_Info AS TP  ON (T1.TR_M_ID = TP.TR_REC_ID  AND TP.REC_STATUS IN (0,1,2) )" &
                     " LEFT  JOIN MISC_INFO                  AS MP  ON (TP.TR_PURPOSE_MISC_ID   = MP.REC_ID     AND MP.REC_STATUS IN (0,1,2) AND MP.MISC_ID='GODLY SERVICE PROJECTS'  )" &
                     " WHERE TM.REC_STATUS IN (0,1,2) AND TM.TR_CODE=15 AND TM.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND TM.REC_ID = '" & inParam.RecID & "' "
            Else
                Query += " (SELECT ITEM_LINK_REC_ID  FROM ITEM_INFO WHERE REC_STATUS IN (0,1,2) AND REC_ID = T1.TR_ITEM_ID ) AS 'ITEM_ID'," &
                         " (SELECT L.ITEM_NAME FROM ITEM_INFO AS I INNER JOIN  ITEM_INFO AS L  ON (L.REC_ID   = I.ITEM_LINK_REC_ID AND L.REC_STATUS IN (0,1,2) ) WHERE  I.REC_STATUS IN (0,1,2) AND I.REC_ID = T1.TR_ITEM_ID ) AS 'Description'," &
                         " CI.CEN_NAME AS 'Centre Name',MAIN.CEN_INCHARGE AS 'Incharge',COALESCE(MAIN.CEN_TEL_NO_1+',','')+COALESCE(MAIN.CEN_TEL_NO_2+',','')+COALESCE(MAIN.CEN_MOB_NO_1+',','')+COALESCE(MAIN.CEN_MOB_NO_2,'')as 'Contact No.' ,CI.CEN_UID  AS 'Centre UID',CI.CEN_PAD_NO AS 'No.',T1.TR_AB_ID_2 AS 'CEN_ID', " &
                         " TM.TR_DATE AS 'Date', TM.TR_SALE_TYPE as 'Asset Type',T2.TR_ITEM_ID AS 'ASSET_ITEM_ID'    ,(SELECT ITEM_NAME FROM ITEM_INFO WHERE REC_STATUS IN (0,1,2) AND REC_ID = T2.TR_ITEM_ID ) AS 'Asset' ,TM.TR_SALE_QTY AS 'Qty / Weight',TM.TR_SALE_AMT AS 'Amount',T1.TR_REF_OTHERS AS 'ASSET_REF_ID',MP.MISC_NAME AS 'Purpose',MP.REC_ID AS PUR_ID,T1.REC_ID AS ID,T1.TR_M_ID AS M_ID, TM.REC_EDIT_ON, T1.TR_NARRATION AS 'Narration' " &
                         " FROM transaction_d_master_info AS TM" &
                         " INNER JOIN transaction_info AS T1 ON (TM.REC_ID  = T1.TR_M_ID    AND T1.REC_STATUS IN (0,1,2) AND T1.TR_SR_NO=1 AND T1.TR_CODE=15 AND T1.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T1.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND T1.TR_TRF_CROSS_REF_ID IS NULL )" &
                         " INNER JOIN transaction_info AS T2 ON (TM.REC_ID  = T2.TR_M_ID    AND T2.REC_STATUS IN (0,1,2) AND T2.TR_SR_NO=2 AND T2.TR_CODE=15 AND T2.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T2.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND T2.TR_TRF_CROSS_REF_ID IS NULL AND T2.TR_TYPE = 'CREDIT' )" &
                         " INNER JOIN CENTRE_INFO      AS CI ON (CI.REC_ID  = T1.TR_AB_ID_2 AND CI.REC_STATUS IN (0,1,2) )  " &
                         " INNER JOIN CENTRE_INFO      AS MAIN ON (CI.CEN_BK_PAD_NO   = MAIN.CEN_BK_PAD_NO AND MAIN.CEN_MAIN = 1  AND MAIN.REC_STATUS   IN (0,1,2) )  " &
                         " LEFT  JOIN Transaction_D_Purpose_Info AS TP  ON (T1.TR_M_ID = TP.TR_REC_ID  AND TP.REC_STATUS IN (0,1,2) )" &
                         " LEFT  JOIN MISC_INFO                  AS MP  ON (TP.TR_PURPOSE_MISC_ID   = MP.REC_ID     AND MP.REC_STATUS IN (0,1,2) AND MP.MISC_ID='GODLY SERVICE PROJECTS'  )" &
                         " WHERE TM.REC_STATUS IN (0,1,2) AND TM.TR_CODE=15 AND TM.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & "" &
                         " ORDER BY CI.CEN_NAME,TM.TR_DATE "
            End If

            Dim dSet As DataSet = New DataSet
            dSet.Tables.Add(dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam).Copy)

            Query = "SELECT COUNT(T2.REC_ID) " &
                     " FROM transaction_info AS T2 WHERE  T2.REC_STATUS IN (0,1,2) AND T2.TR_SR_NO=2 AND T2.TR_CODE=15 AND T2.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T2.TR_AB_ID_1 = '" & inParam.OpenCenRecID & "' AND T2.TR_TRF_CROSS_REF_ID IS NULL AND T2.TR_TYPE = 'CREDIT' "


            dSet.Tables.Add(dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, "COUNT", inBasicParam).Copy)
            Return dSet
        End Function

        Public Shared Function GetUnmatchedCount_AssetTransfer(ByVal Param As Param_VoucherAssetTransfer_GetUnmatchedCount, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COUNT(T2.REC_ID) " &
                                  " FROM transaction_info AS T2 WHERE  T2.REC_STATUS IN (0,1,2) AND T2.TR_SR_NO=2 AND T2.TR_CODE=15 AND T2.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND (T2.TR_AB_ID_1 = '" & Param.OpenCenRecID & "' OR T2.TR_CEN_ID =  " & inBasicParam.openCenID.ToString & ") AND T2.TR_TRF_CROSS_REF_ID IS NULL "
            If Param.FromDate <> DateTime.MinValue Then
                Query += " AND TR_DATE BETWEEN '" & Convert.ToDateTime(Param.FromDate).ToString(Common.Server_Date_Format_Long) & "' AND  '" & Convert.ToDateTime(Param.ToDate).ToString(Common.Server_Date_Format_Long) & "'"
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, "TRANSACTION_INFO", inBasicParam)
        End Function

        Public Shared Function GetUnmatchedCount_AssetTransferFrom(ByVal Param As Param_VoucherAssetTransfer_GetUnmatchedFromCount, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COUNT(T2.REC_ID) " &
                                  " FROM transaction_info AS T2 WHERE  T2.REC_STATUS IN (0,1,2) AND T2.TR_SR_NO=2 AND T2.TR_CODE=15 AND T2.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND T2.TR_AB_ID_1 = '" & Param.OpenCenRecID & "' AND T2.TR_TRF_CROSS_REF_ID IS NULL AND T2.TR_TYPE = 'CREDIT' "

            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, "TRANSACTION_INFO", inBasicParam)
        End Function

        Public Shared Function GetFrCenterList(ByVal Param As Param_Voucher_AssetTransfer_GetFrCenterList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT CASE WHEN LEN(COALESCE(C.CEN_NAME,'')) > 0 THEN C.CEN_NAME ELSE M.CEN_NAME END AS FR_CEN_NAME,C.CEN_PAD_NO AS FR_PAD_NO, C.CEN_UID AS FR_UID,   M.CEN_INCHARGE AS FR_INCHARGE, M.CEN_ZONE_ID AS FR_ZONE, C.CEN_ID AS FR_CEN_ID, C.REC_ID AS FR_ID, " &
                                    " CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 THEN M.CEN_TEL_NO_1 ELSE '' END +  CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 AND LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN M.CEN_MOB_NO_1 ELSE '' END AS FR_TEL_NO" &
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='" & Param.openInsID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If Param.ExcludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID NOT IN (" & Param.ExcludeRecIDs & ")"
            If Param.IncludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID IN (" & Param.IncludeRecIDs & ")"
            If Param.ExcludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID NOT IN (" & Param.ExcludeCenIDs & ")"
            If Param.IncludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID IN (" & Param.IncludeCenIDs & ")"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetToCenterList(ByVal Param As Param_Voucher_AssetTransfer_GetToCenterList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT CASE WHEN LEN(COALESCE(C.CEN_NAME,'')) > 0 THEN C.CEN_NAME ELSE M.CEN_NAME END AS TO_CEN_NAME,C.CEN_PAD_NO AS TO_PAD_NO, C.CEN_UID AS TO_UID,   M.CEN_INCHARGE AS TO_INCHARGE, M.CEN_ZONE_ID AS TO_ZONE, C.CEN_ID AS TO_CEN_ID, C.REC_ID AS TO_ID, " &
                                    " CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0  THEN M.CEN_TEL_NO_1 ELSE '' END +  CASE WHEN LEN(COALESCE(M.CEN_TEL_NO_1,'')) > 0 AND LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN ', ' ELSE '' END + CASE WHEN LEN(COALESCE(M.CEN_MOB_NO_1,'')) > 0 THEN M.CEN_MOB_NO_1 ELSE '' END AS TO_TEL_NO" &
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='" & Param.openInsID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If Param.ExcludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID NOT IN (" & Param.ExcludeRecIDs & ")"
            If Param.IncludeRecIDs.Length > 0 Then OnlineQuery += " AND C.REC_ID IN (" & Param.IncludeRecIDs & ")"
            If Param.ExcludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID NOT IN (" & Param.ExcludeCenIDs & ")"
            If Param.IncludeCenIDs.Length > 0 Then OnlineQuery += " AND C.CEN_ID IN (" & Param.IncludeCenIDs & ")"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMasterID(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_M_ID FROM transaction_info WHERE REC_ID ='" & Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTxnItems(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " Select I.*,P.TR_PURPOSE_MISC_ID  AS  PUR_ID,'' AS LOC_ID," &
                                 " '' AS GS_DESC_MISC_ID,0 AS GS_ITEM_WEIGHT,  " &
                                 " '' AS AI_MAKE,'' AS AI_MODEL,'' AS AI_SERIAL_NO,0 AS AI_WARRANTY, '' AS AI_PUR_DATE ,   " &
                                 " '' AS LS_NAME,'' AS LS_BIRTH_YEAR,'' AS LS_INSURANCE,'' AS LS_INSURANCE_ID, '' AS LS_INS_POLICY_NO, 0 AS LS_INS_AMT, '' AS LS_INS_DATE ,   " &
                                 " '' AS VI_MAKE, '' AS VI_MODEL, '' AS VI_REG_NO_PATTERN, '' AS VI_REG_NO, '' AS VI_REG_DATE, '' AS VI_OWNERSHIP, '' AS VI_OWNERSHIP_AB_ID, '' AS VI_DOC_RC_BOOK, '' AS VI_DOC_AFFIDAVIT, '' AS VI_DOC_WILL, '' AS VI_DOC_TRF_LETTER, '' AS VI_DOC_FU_LETTER, '' AS VI_DOC_OTHERS, '' AS VI_DOC_NAME, '' AS VI_INSURANCE_ID, '' AS VI_INS_POLICY_NO, '' AS VI_INS_EXPIRY_DATE " &
                                 " FROM Transaction_D_Item_Info AS I  INNER  JOIN Transaction_D_Purpose_Info AS P ON (P.TR_REC_ID = I.TR_M_ID) AND (P.TR_ITEM_SR_NO = I.TR_SR_NO)" &
                                 " Where I.REC_STATUS IN (0,1,2) " &
                                 " AND   P.REC_STATUS IN (0,1,2) " &
                                 " AND   I.TR_M_ID= '" & Rec_ID & "' " &
                                 " ORDER BY I.TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTxnList_Sale(ByVal Param As Param_Voucher_AssetTransfer_GetTxnList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            If Not Param.IsNew Then
                Query = "SELECT TP.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'SALE_QTY' FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('SALE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=11  AND TP.TR_M_ID  <> '" & Param.TxnMID & "'  GROUP BY TP.TR_REF_ID "
            Else
                Query = "SELECT TP.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'SALE_QTY' FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND  TP.TR_PAY_TYPE IN ('SALE') AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=11  GROUP BY TP.TR_REF_ID "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTxnList_AssetTrf(ByVal Param As Param_Voucher_AssetTransfer_GetTxnList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            If Not Param.IsNew Then
                Query = " SELECT TM.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'TRF_QTY' " &
                        " FROM Transaction_D_Master_Info AS TM " &
                        " INNER JOIN transaction_info AS TI ON (TI.TR_M_ID = TM.REC_ID AND TI.REC_STATUS IN (0,1,2) AND TI.TR_TYPE ='DEBIT' AND TI.TR_SR_NO =1) " &
                        " WHERE  TM.REC_STATUS IN (0,1,2) AND  TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=15  AND TM.REC_ID  <> '" & Param.TxnMID & "'  GROUP BY TM.TR_REF_ID "
            Else
                Query = " SELECT TM.TR_REF_ID,  SUM(TM.TR_SALE_QTY) AS 'TRF_QTY' " &
                        " FROM Transaction_D_Master_Info AS TM " &
                        " INNER JOIN transaction_info AS TI ON (TI.TR_M_ID = TM.REC_ID AND TI.REC_STATUS IN (0,1,2) AND TI.TR_TYPE ='DEBIT' AND TI.TR_SR_NO =1) " &
                        " WHERE  TM.REC_STATUS IN (0,1,2) AND  TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND TM.TR_CODE=15  GROUP BY TM.TR_REF_ID "
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAssetTransfers(param As Param_GetAssetTransfers, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select TR_REF_OTHERS as Reference, TR_QTY as QTY, TR_AMOUNT as AMOUNT, TR_DATE from transaction_info where TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE =15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT'"
            If Not param.YearID = Nothing Then Query += " AND TR_COD_YEAR_ID = " & param.YearID.ToString & " "
            If param.Exclude_PrevYears Then Query += " AND TR_COD_YEAR_ID >= " & param.YearID & " " ' IN JVs
            If Not param.refAssetID Is Nothing Then Query += "AND TR_REF_OTHERS = '" & param.refAssetID & "'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_AssetTf_Asset_Listing(Param As Param_Get_AssetTf_Asset_Listing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_AssetTf_Asset_Listing"
            Dim params() As String = {"CENID", "YEARID", "ASSET_PROFILE", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.Cen_Id, inBasicParam.openYearID, dbService.GetAssetProfileFromEnum(Param.Asset_Profile), Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, Param.TR_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_REF_ID,TR_SALE_AMT,TR_SALE_QTY,TR_SALE_DATE,TR_SALE_TYPE," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InMInfo.TxnCode & "'," &
                                                  "'" & InMInfo.VNo & "', " &
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InMInfo.PartyID & "'," &
                                                  " " & InMInfo.SubTotal & "," &
                                                  " " & InMInfo.Cash & "," &
                                                  " " & InMInfo.Bank & "," &
                                                  "'" & InMInfo.AssetRef_ID & "', " &
                                                  " " & InMInfo.AssetTrf_Amt & "," &
                                                  " " & InMInfo.AssetTrf_Qty & "," &
                                                  "" & If(IsDate(InMInfo.AssetTrf_Date), "'" & Convert.ToDateTime(InMInfo.AssetTrf_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InMInfo.AssetTrf_Type & "', " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.Sub_Cr_Led_ID.Length = 0 Then InParam.Sub_Cr_Led_ID = "NULL" Else If Not InParam.Sub_Cr_Led_ID.StartsWith("'") Then InParam.Sub_Cr_Led_ID = "'" & InParam.Sub_Cr_Led_ID & "'"
            If InParam.Sub_Dr_Led_ID.Length = 0 Then InParam.Sub_Dr_Led_ID = "NULL" Else If Not InParam.Sub_Dr_Led_ID.StartsWith("'") Then InParam.Sub_Dr_Led_ID = "'" & InParam.Sub_Dr_Led_ID & "'"
            If InParam.Tr_M_ID.Length = 0 Then InParam.Tr_M_ID = "NULL" Else If Not InParam.Tr_M_ID.StartsWith("'") Then InParam.Tr_M_ID = "'" & InParam.Tr_M_ID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.AB_ID_1.Length = 0 Then InParam.AB_ID_1 = "NULL" Else If Not InParam.AB_ID_1.StartsWith("'") Then InParam.AB_ID_1 = "'" & InParam.AB_ID_1 & "'"
            If InParam.AB_ID_2.Length = 0 Then InParam.AB_ID_2 = "NULL" Else If Not InParam.AB_ID_2.StartsWith("'") Then InParam.AB_ID_2 = "'" & InParam.AB_ID_2 & "'"
            If InParam.Cross_Ref_ID Is Nothing Then InParam.Cross_Ref_ID = "NULL" Else If Not InParam.Cross_Ref_ID.StartsWith("'") Then InParam.Cross_Ref_ID = "'" & InParam.Cross_Ref_ID & "'"
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID,TR_MODE,TR_AMOUNT,TR_AB_ID_1,TR_AB_ID_2,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID,TR_QTY,TR_REF_OTHERS," &
                                            "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                            ") VALUES(" &
                                            "" & inBasicParam.openCenID.ToString & "," &
                                            "" & inBasicParam.openYearID.ToString & "," &
                                            " " & InParam.TransCode & "," &
                                            "'" & InParam.VNo & "', " &
                                            "" & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            "'" & InParam.ItemID & "', " &
                                            "'" & InParam.Type & "', " &
                                            " " & InParam.Cr_Led_ID & " , " &
                                            " " & InParam.Dr_Led_ID & " , " &
                                            " " & InParam.Sub_Cr_Led_ID & " , " &
                                            " " & InParam.Sub_Dr_Led_ID & " , " &
                                            " " & InParam.Mode & " , " &
                                            " " & InParam.Amount & ", " &
                                            " " & InParam.AB_ID_1 & ", " &
                                            " " & InParam.AB_ID_2 & ", " &
                                            "'" & InParam.Narration & "', " &
                                            "'" & InParam.Remarks & "', " &
                                            "'" & InParam.Reference & "', " &
                                            " " & InParam.Tr_M_ID & " , " &
                                            "'" & InParam.TxnSrNo & "', " &
                                            " " & InParam.Cross_Ref_ID & " , " &
                                            " " & InParam.AssetTrf_Qty & ", " &
                                            "'" & InParam.AssetTrf_RefItemID & "', " & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',   '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," &
                                            "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                            ") VALUES(" &
                                            "" & inBasicParam.openCenID.ToString & "," &
                                            "" & inBasicParam.openYearID.ToString & "," &
                                            "'" & InItem.Txn_M_ID & "', " &
                                            " " & InItem.TxnSrNo & " , " &
                                            "'" & InItem.ItemID & "', " &
                                            "'" & InItem.LedID & "', " &
                                            "'" & InItem.Type & "', " &
                                            "'" & InItem.PartyReq & "', " &
                                            "'" & InItem.Profile & "', " &
                                            "'" & InItem.ItemName & "', " &
                                            "'" & InItem.Head & "', " &
                                            " " & InItem.Qty & ", " &
                                            "'" & InItem.Unit & "', " &
                                            " " & InItem.Rate & ", " &
                                            " " & InItem.Amount & ", " &
                                            "'" & InItem.Remarks & "', " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID)
            Return True
        End Function

        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InPurpose.TxnID & "'," &
                                                  "'" & InPurpose.PurposeID & "', " &
                                                  " " & InPurpose.Amount & ", " &
                                        "" & Str & "  '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertPayment(ByVal InPay As Parameter_InsertAandLPayment_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPay.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_REF_ID,TR_REF_AMT," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & "," &
                                        "" & inBasicParam.openYearID.ToString & "," &
                                        "'" & InPay.TxnMID & "', " &
                                        "'" & InPay.Type & "', " &
                                        " " & InPay.SrNo & " , " &
                                        "'" & InPay.RefID & "', " &
                                        " " & InPay.RefAmount & ", " &
                                        "" & Str & "  '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPay.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPay.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing, Optional TxnTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " &
                                            " TR_VNO         ='" & UpParam.VNo & "', " &
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " &
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " &
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " &
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " &
                                            " TR_REF_ID      ='" & UpParam.AssetRef_ID & "', " &
                                            " TR_SALE_AMT    = " & UpParam.AssetTrf_Amt & "  ," &
                                            " TR_SALE_QTY    = " & UpParam.AssetTrf_Qty & "  ," &
                                            " TR_SALE_DATE   = " & If(IsDate(UpParam.AssetTrf_Date), "'" & Convert.ToDateTime(UpParam.AssetTrf_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            " TR_SALE_TYPE   ='" & UpParam.AssetTrf_Type & "', " &
                                            " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            " REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                            " WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, TxnTime)
            Return True
        End Function

        Public Shared Function Update_CrossReference(ByVal Param As Param_VoucherAssetTransfer_Update_CrossReference, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing, Optional TxnTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " &
                                        " TR_TRF_CROSS_REF_ID   ='" & Param.Cross_Ref_ID & "', " &
                                        " REC_EDIT_ON           ='" & Common.DateTimePlaceHolder & "'," &
                                        " REC_EDIT_BY           ='" & inBasicParam.openUserID & "' " &
                                        " WHERE TR_M_ID          ='" & Param.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, EditTime, TxnTime)
            Return True
        End Function

        Public Shared Function Delete_CrossReference(ByVal Param As Param_VoucherAssetTransfer_Update_CrossReference, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing, Optional TxnTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " &
                                        " TR_TRF_CROSS_REF_ID   = NULL, " &
                                        " REC_EDIT_ON           ='" & Common.DateTimePlaceHolder & "'," &
                                        " REC_EDIT_BY           ='" & inBasicParam.openUserID & "' " &
                                        " WHERE TR_TRF_CROSS_REF_ID ='" & Param.Cross_Ref_ID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, EditTime, TxnTime)
            Return True
        End Function

        Public Shared Function isFDBankPresent(xCenID As Integer, FDID As String, prev_year_id As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'If prev_year_id Is Nothing Then prev_year_id = ""
            Dim Query As String = "SELECT count(IN_BA.REC_ID) FROM bank_account_info IN_BA " &
                                  "WHERE BA_CEN_ID = " + xCenID.ToString + " AND BA_COD_YEAR_ID IN (" + inBasicParam.openYearID.ToString + "," + prev_year_id.ToString + ") " &
                                  "AND IN_BA.BA_BRANCH_ID IN (SELECT BA_BRANCH_ID FROM FD_INFO AS FI INNER JOIN BANK_ACCOUNT_INFO AS BA ON FD_BA_ID = BA.REC_ID WHERE FI.REC_ID = '" + FDID + "') " &
                                  "AND IN_BA.BA_CUST_NO IN (SELECT BA_CUST_NO FROM FD_INFO AS FI INNER JOIN BANK_ACCOUNT_INFO AS BA ON FD_BA_ID = BA.REC_ID WHERE FI.REC_ID = '" + FDID + "') AND IN_BA.BA_ACCOUNT_TYPE ='FD'"
            If dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Function Insert_AssetTransfer_Txn(inParam As Param_Txn_Insert_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertProfile Is Nothing Then
                If inParam.param_InsertProfile.AssetType.ToUpper = "FD" Then
                    If Not isFDBankPresent(inParam.param_InsertProfile.CenID, inParam.param_InsertProfile.AssetRefID, inParam.prev_year_id, inBasicParam) Then
                        Throw New Exception("|FD Bank Account for Transferred FD not present. Please Create the FD bank first..|")
                    End If
                End If
            End If

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_InsertTxnInfo1 Is Nothing Then
                If Not inParam.param_InsertTxnInfo1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.Dr_Led_ID)
                If Not inParam.param_InsertTxnInfo1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.Cr_Led_ID)
                If Not inParam.param_InsertTxnInfo1.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.Sub_Cr_Led_ID)
                If Not inParam.param_InsertTxnInfo1.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.Sub_Dr_Led_ID)
                If Not inParam.param_InsertTxnInfo1.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.AB_ID_1)
                If Not inParam.param_InsertTxnInfo1.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo1.TDate), inParam.param_InsertTxnInfo1.AB_ID_2)
            End If
            If Not inParam.param_InsertTxnInfo2 Is Nothing Then
                If Not inParam.param_InsertTxnInfo2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.Dr_Led_ID)
                If Not inParam.param_InsertTxnInfo2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.Cr_Led_ID)
                If Not inParam.param_InsertTxnInfo2.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.Sub_Cr_Led_ID)
                If Not inParam.param_InsertTxnInfo2.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.Sub_Dr_Led_ID)
                If Not inParam.param_InsertTxnInfo2.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.AB_ID_1)
                If Not inParam.param_InsertTxnInfo2.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertTxnInfo2.TDate), inParam.param_InsertTxnInfo2.AB_ID_2)
            End If

            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope

            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertTxnInfo1 Is Nothing Then
                If Not Insert(inParam.param_InsertTxnInfo1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertTxnInfo2 Is Nothing Then
                If Not Insert(inParam.param_InsertTxnInfo2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertAandLPayment Is Nothing Then
                If Not InsertPayment(inParam.param_InsertAandLPayment, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertPurpose Is Nothing Then
                If Not InsertPurpose(inParam.param_InsertPurpose, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_UpdateCrossRef Is Nothing Then
                If Not Update_CrossReference(inParam.param_UpdateCrossRef, inBasicParam, RequestTime, inParam.param_InsertMaster.TDate) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertProfile Is Nothing Then
                'Try
                Insert_ProfileBySP(inParam.param_InsertProfile, inBasicParam)
                'Catch ex As Exception
                '    Throw New Exception(Common_Lib.Messages.SomeError)
                'End Try
            End If
            '  End Using
            'Commit here 
            '   txn.Complete()
            '   End Using

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(inParam.InsertSplVchrRefs, inParam.param_InsertMaster.RecID, Nothing, Nothing, inBasicParam)
            'If inParam.InsertSplVchrRefs IsNot Nothing Then
            '    If inParam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In inParam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, inParam.param_InsertPurpose.TxnID, inParam.param_InsertPurpose.Amount, inBasicParam)
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

        Public Shared Function Update_AssetTransfer_Txn(upParam As Param_Txn_Update_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_InsertTxnInfo1 Is Nothing Then
                If Not upParam.param_InsertTxnInfo1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.Dr_Led_ID)
                If Not upParam.param_InsertTxnInfo1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.Cr_Led_ID)
                If Not upParam.param_InsertTxnInfo1.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.Sub_Cr_Led_ID)
                If Not upParam.param_InsertTxnInfo1.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.Sub_Dr_Led_ID)
                If Not upParam.param_InsertTxnInfo1.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.AB_ID_1)
                If Not upParam.param_InsertTxnInfo1.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo1.TDate), upParam.param_InsertTxnInfo1.AB_ID_2)
            End If
            If Not upParam.param_InsertTxnInfo2 Is Nothing Then
                If Not upParam.param_InsertTxnInfo2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.Dr_Led_ID)
                If Not upParam.param_InsertTxnInfo2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.Cr_Led_ID)
                If Not upParam.param_InsertTxnInfo2.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.Sub_Cr_Led_ID)
                If Not upParam.param_InsertTxnInfo2.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.Sub_Dr_Led_ID)
                If Not upParam.param_InsertTxnInfo2.AB_ID_1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.AB_ID_1)
                If Not upParam.param_InsertTxnInfo2.AB_ID_2 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertTxnInfo2.TDate), upParam.param_InsertTxnInfo2.AB_ID_2)
            End If

            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
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
            If Not upParam.param_UpdateMaster Is Nothing Then 'master updated 
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime, upParam.param_UpdateMaster.TDate) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_DeleteItems Is Nothing Then 'item deleted 
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & upParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePayment Is Nothing Then 'payment deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then 'purpose deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_Delete Is Nothing Then 'actual txn deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.param_InsertTxnInfo1 Is Nothing Then 'actual txn re-inserted
                If Not Insert(upParam.param_InsertTxnInfo1, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertTxnInfo2 Is Nothing Then 'actual txn2 re-inserted
                If Not Insert(upParam.param_InsertTxnInfo2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertAandLPayment Is Nothing Then 'paymentre-inserted
                If Not InsertPayment(upParam.param_InsertAandLPayment, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertPurpose Is Nothing Then 'purpose re-inserted
                If Not InsertPurpose(upParam.param_InsertPurpose, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            ' End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            InsertSpecialVoucherReference(upParam.UpdateSplVchrRefs, upParam.param_UpdateMaster.RecID, Nothing, Nothing, inBasicParam)
            'If upParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If upParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In upParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, upParam.param_InsertPurpose.TxnID, upParam.param_InsertPurpose.Amount, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function Delete_AssetTransfer_Txn(delParam As Param_Txn_Delete_VoucherAssetTransfer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            Dim delRefParam As Param_VoucherAssetTransfer_Update_CrossReference = New Param_VoucherAssetTransfer_Update_CrossReference()
                delRefParam.Cross_Ref_ID = delParam.MID_Delete
                Delete_CrossReference(delRefParam, inBasicParam, Nothing, Convert.ToDateTime(delParam.Txn_Date).ToString(Common.Server_Date_Format_Long))
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & delParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteGS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.GOLD_SILVER_INFO, "GS_TR_ID    ='" & delParam.MID_DeleteGS & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & delParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteLS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIVE_STOCK_INFO, "LS_TR_ID    ='" & delParam.MID_DeleteLS & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteVehicle Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.VEHICLES_INFO, "VI_TR_ID    ='" & delParam.MID_DeleteVehicle & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteFD Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_INFO, "FD_TR_ID    ='" & delParam.MID_DeleteFD & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteAdvance Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADVANCES_INFO, "AI_TR_ID    ='" & delParam.MID_DeleteAdvance & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteDeposit Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.DEPOSITS_INFO, "DI_TR_ID    ='" & delParam.MID_DeleteDeposit & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteLiability Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIABILITIES_INFO, "LI_TR_ID    ='" & delParam.MID_DeleteLiability & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteWIP Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.WIP_INFO, "WIP_TR_ID    ='" & delParam.MID_DeleteWIP & "'", inBasicParam)
            End If
            If Not delParam.DeleteComplexBuilding Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID   ='" & delParam.DeleteComplexBuilding & "'", inBasicParam)
            End If
            If Not delParam.DelExtInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & delParam.DelExtInfo & "'", inBasicParam)
            End If
            If Not delParam.DelDocInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & delParam.DelDocInfo & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteLB Is Nothing Then
                'Delete Property Locations
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & delParam.DelExtInfo & "' ", inBasicParam)
                'Delete Property
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_INFO, "LB_TR_ID    ='" & delParam.MID_DeleteLB & "'", inBasicParam)
            End If

            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function Insert_ProfileBySP(ByVal Param As Parameter_Insert_Profile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Dim Query As String = "SELECT IN_BA.REC_ID FROM bank_account_info IN_BA " & _
            '                       " WHERE BA_CEN_ID = @xCenID AND BA_COD_YEAR_ID=@xYear_ID " & _
            '                       " AND IN_BA.BA_BRANCH_ID IN (SELECT BA_BRANCH_ID FROM FD_INFO AS FI INNER JOIN BANK_ACCOUNT_INFO AS BA ON FD_BA_ID = BA.REC_ID WHERE FI.REC_ID = @xAssetRefID) " & _
            '                       " AND IN_BA.BA_ACCOUNT_NO IN (SELECT BA_ACCOUNT_NO FROM FD_INFO AS FI INNER JOIN BANK_ACCOUNT_INFO AS BA ON FD_BA_ID = BA.REC_ID WHERE FI.REC_ID = @xAssetRefID) AND IN_BA.BA_ACCOUNT_TYPE ='FD' "
            'dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

            Dim SPName As String = "sp_Insert_AssetTransferProfile"
            Dim params() As String = {"xAssetType", "xAssetRefID", "xAssetNewID", "xAssetLocID", "xAssetOwner", "xAssetOwnerID", "xAssetUse", "xAssetQty", "xAssetAmt", "xCenID", "xTr_ID", "Add_By", "xYear_ID"}
            Dim values() As Object = {Param.AssetType, Param.AssetRefID, Param.AssetNewID, Param.AssetLocID, Param.AssetOwner, Param.AssetOwnerID, Param.AssetUse, Param.AssetQty, Param.AssetAmt, Param.CenID, Param.TrID, inBasicParam.openUserID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Double, Data.DbType.Double, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {50, 36, 36, 36, 50, 36, 50, 20, 20, 5, 36, 50, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_INFO, SPName, ConnectOneWS.Tables.ASSET_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

    End Class
End Namespace
