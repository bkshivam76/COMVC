Imports System.Data
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class Voucher_Gift
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_VoucherGift
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Mode As String
            Public Ref_No As String
            Public Amount As Double
            Public PartyID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public Tr_M_ID As String
            Public TxnSrNo As Integer
            Public Status_Action As String
            Public RecID As String
            Public Cross_Ref_Id As String
            ' Public openYearID As String  ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherGift
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
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherGift
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public ItemSrNo As Integer
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherGift
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
        Public Class Parameter_UpdateMaster_VoucherGift
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
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherGift
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherGift
            Public Insert() As Parameter_Insert_VoucherGift = Nothing
            Public param_InsertJV As Parameter_Insert_VoucherGift = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherGift = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherGift = Nothing
            Public InsertGS() As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver = Nothing
            Public InsertAssets() As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public InsertLivestock() As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock = Nothing
            Public InsertVehicles() As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles = Nothing
            Public InsertProperty() As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = Nothing
            Public InsertReferencesWIP() As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherGift
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherGift
            Public MID_Delete As String = Nothing
            Public Insert() As Parameter_Insert_VoucherGift = Nothing
            Public InsertJV As Parameter_Insert_VoucherGift = Nothing
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeleteGS As String = Nothing
            Public MID_DeleteAssets As String = Nothing
            Public MID_DeleteLS As String = Nothing
            Public MID_DeleteVehicle As String = Nothing
            Public MID_DeleteLandB As String = Nothing
            Public MID_ReferenceDelete As String = Nothing
            Public DeleteComplexBuilding() As String = Nothing
            Public DeleteExtendedInfo() As String = Nothing
            Public DeleteDocumentInfo() As String = Nothing
            Public DeleteByLB() As String = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherGift = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherGift = Nothing
            Public InsertGS() As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver = Nothing
            Public InsertAssets() As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public InsertLivestock() As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock = Nothing
            Public InsertVehicles() As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles = Nothing
            Public InsertProperty() As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = Nothing
            Public InsertReferencesWIP() As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherGift
            Public MID_DeleteGS As String = Nothing
            Public MID_DeleteAssets As String = Nothing
            Public MID_DeleteLS As String = Nothing
            Public MID_DeleteVehicle As String = Nothing
            Public MID_DeleteLandB As String = Nothing
            Public MID_ReferenceDelete As String = Nothing
            Public DeleteComplexBuilding() As String = Nothing
            Public DeleteExtendedInfo() As String = Nothing
            Public DeleteDocumentInfo() As String = Nothing
            Public DeleteByLB() As String = Nothing
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class
#End Region
        ''' <summary>
        ''' Get Txn Items
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Gift_GetTxnItems</remarks>
        Public Shared Function GetTxnItems(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " Select I.*,II.ITEM_VOUCHER_TYPE ,II.ITEM_PROFILE ,P.TR_PURPOSE_MISC_ID  AS  PUR_ID,'' AS LOC_ID," &
                                 " '' AS GS_DESC_MISC_ID,0.000 AS GS_ITEM_WEIGHT,  " &
                                 " '' AS AI_TYPE,'' AS AI_MAKE,'' AS AI_MODEL,'' AS AI_SERIAL_NO,0 AS AI_WARRANTY, '' AS AI_PUR_DATE ,  IMAGE AS AI_IMAGE , 0 AS AI_INS_AMT, " &
                                 " '' AS LS_NAME,'' AS LS_BIRTH_YEAR,'' AS LS_INSURANCE,'' AS LS_INSURANCE_ID, '' AS LS_INS_POLICY_NO, 0 AS LS_INS_AMT, '' AS LS_INS_DATE ,   " &
                                 " '' AS VI_MAKE, '' AS VI_MODEL, '' AS VI_REG_NO_PATTERN, '' AS VI_REG_NO, '' AS VI_REG_DATE, '' AS VI_OWNERSHIP, '' AS VI_OWNERSHIP_AB_ID, '' AS VI_DOC_RC_BOOK, '' AS VI_DOC_AFFIDAVIT, '' AS VI_DOC_WILL, '' AS VI_DOC_TRF_LETTER, '' AS VI_DOC_FU_LETTER, '' AS VI_DOC_OTHERS, '' AS VI_DOC_NAME, '' AS VI_INSURANCE_ID, '' AS VI_INS_POLICY_NO, '' AS VI_INS_EXPIRY_DATE, " &
                                 " '' AS LB_PRO_TYPE, '' AS LB_PRO_CATEGORY, '' AS LB_PRO_USE, '' AS LB_PRO_NAME, '' AS LB_PRO_ADDRESS, '' AS LB_OWNERSHIP, '' AS LB_OWNERSHIP_PARTY_ID, '' AS LB_SURVEY_NO, '' AS LB_CON_YEAR, '' AS LB_RCC_ROOF, '' AS LB_PAID_DATE, '' AS LB_PERIOD_FROM, '' AS LB_PERIOD_TO, '' AS LB_DOC_OTHERS, '' AS LB_DOC_NAME, '' AS LB_OTHER_DETAIL , 0.000 AS LB_TOT_P_AREA, 0.000 AS LB_CON_AREA, 0.000 AS LB_DEPOSIT_AMT, 0.000 AS LB_MONTH_RENT, 0.000 AS LB_MONTH_O_PAYMENTS, '' AS LB_REC_ID, " &
                                 " '' AS LB_ADDRESS1,'' AS LB_ADDRESS2,'' AS LB_ADDRESS3,'' AS LB_ADDRESS4,'' AS LB_COUNTRY_ID,'' AS LB_STATE_ID,'' AS LB_DISTRICT_ID,'' AS LB_CITY_ID,'' AS LB_PINCODE, " &
                                 " '' AS WIP_REF, '' AS WIP_REC_ID, '' AS WIP_REF_TYPE " &
                                 " FROM Transaction_D_Item_Info AS I  INNER  JOIN Transaction_D_Purpose_Info AS P ON (P.TR_REC_ID = I.TR_M_ID) AND (P.TR_ITEM_SR_NO = I.TR_SR_NO)" &
                                 "INNER JOIN item_info AS II ON II.REC_ID = I.TR_ITEM_ID " &
                                 " LEFT OUTER JOIN ASSET_INFO AS AI ON I.TR_M_ID = AI.AI_TR_ID AND AI_TR_ITEM_SRNO = I.TR_SR_NO " &
                                 " LEFT OUTER JOIN ASSET_IMAGES AS AI_IMG ON AI.REC_ID = AI_IMG.ASSET_ID " &
                                 " Where I.REC_STATUS IN (0,1,2) " &
                                 " AND   P.REC_STATUS IN (0,1,2) " &
                                 " AND   I.TR_M_ID= '" & Rec_ID & "' " &
                                 " ORDER BY I.TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Gift_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cross_Ref_Id.Length = 0 Then InParam.Cross_Ref_Id = "NULL" Else If Not InParam.Cross_Ref_Id.StartsWith("'") Then InParam.Cross_Ref_Id = "'" & InParam.Cross_Ref_Id & "'"
            If InParam.Sub_Cr_Led_ID.Trim.Length = 0 Then InParam.Sub_Cr_Led_ID = "NULL" Else InParam.Sub_Cr_Led_ID = "'" & InParam.Sub_Cr_Led_ID & "'"
            If InParam.Cr_Led_ID.Trim.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Trim.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_MODE,TR_REF_NO,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID," & _
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
                                                  " " & InParam.Sub_Cr_Led_ID & ", " & _
                                                  "'" & InParam.Mode & "', " & _
                                                  "'" & InParam.Ref_No & "', " & _
                                                  " " & InParam.Amount & ", " & _
                                                  "'" & InParam.PartyID & "', " & _
                                                  "'" & InParam.Narration & "', " & _
                                                  "'" & InParam.Remarks & "', " & _
                                                  "'" & InParam.Reference & "', " & _
                                                  "'" & InParam.Tr_M_ID & "', " & _
                                                  "'" & InParam.TxnSrNo & "', " & _
                                                  " " & InParam.Cross_Ref_Id & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Gift_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT," & _
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
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
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
        ''' <remarks> RealServiceFunctions.Gift_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
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
                                                  " " & InPurpose.ItemSrNo & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Gift_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," & _
                                            "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
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
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID, Nothing, AddTime)
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
        ''' <remarks>RealServiceFunctions.Gift_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " & _
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " & _
                                            " TR_ADVANCE_AMT = " & UpParam.Advance & ", " & _
                                            " TR_LB_AMT      = " & UpParam.Liability & ", " & _
                                            " TR_CREDIT_AMT  = " & UpParam.Credit & ", " & _
                                            " TR_TDS_AMT     = " & UpParam.TDS & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        Public Shared Function InsertGift_Txn(inParam As Param_Txn_Insert_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each Voucher_Insert As Parameter_Insert_VoucherGift In inParam.Insert
                If Not Voucher_Insert Is Nothing Then
                    If Not Voucher_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Dr_Led_ID)
                    If Not Voucher_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Cr_Led_ID)
                    If Not Voucher_Insert.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Sub_Cr_Led_ID)
                    'If Not Voucher_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.SUB_Dr_Led_ID)
                    If Not Voucher_Insert.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.PartyID)
                End If
            Next

            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Dim isProperty As Boolean = False
            'Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each inVoucher_Insert As Parameter_Insert_VoucherGift In inParam.Insert
                If Not inVoucher_Insert Is Nothing Then Insert(inVoucher_Insert, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertJV Is Nothing Then
                If Not Insert(inParam.param_InsertJV, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each inItem_Insert As Parameter_InsertItem_VoucherGift In inParam.InsertItem
                If Not inItem_Insert Is Nothing Then InsertItem(inItem_Insert, inBasicParam, RequestTime)
            Next
            For Each inPurpose_Insert As Parameter_InsertPurpose_VoucherGift In inParam.InsertPurpose
                If Not inPurpose_Insert Is Nothing Then InsertPurpose(inPurpose_Insert, inBasicParam, RequestTime)
            Next
            For Each inGS_Insert As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver In inParam.InsertGS
                If Not inGS_Insert Is Nothing Then GoldSilver.Insert(inGS_Insert, inBasicParam, RequestTime)
            Next
            For Each inAssets_Insert As Assets.Parameter_InsertTRIDAndTRSrNo_Assets In inParam.InsertAssets
                If Not inAssets_Insert Is Nothing Then Assets.Insert(inAssets_Insert, inBasicParam, RequestTime)
            Next
            For Each inLS_Insert As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock In inParam.InsertLivestock
                If Not inLS_Insert Is Nothing Then Livestock.Insert(inLS_Insert, inBasicParam, RequestTime)
            Next
            For Each inVeh_Insert As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles In inParam.InsertVehicles
                If Not inVeh_Insert Is Nothing Then Vehicles.Insert(inVeh_Insert, inBasicParam, RequestTime)
            Next
            For Each inLb_Insert As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding In inParam.InsertProperty
                If Not inLb_Insert Is Nothing Then
                    LandAndBuilding.Insert(inLb_Insert, inBasicParam, RequestTime)
                    'add child entries 
                    For Each inExtInfo As LandAndBuilding.Parameter_InsertExtendedInfo_LandAndBuilding In inLb_Insert.InsertExtInfo
                        If Not inExtInfo Is Nothing Then LandAndBuilding.InsertExtendedInfo(inExtInfo, inBasicParam, RequestTime)
                    Next
                    For Each inDocInfo As LandAndBuilding.Parameter_InsertDocInfo_LandAndBuilding In inLb_Insert.InsertDocInfo
                        If Not inDocInfo Is Nothing Then LandAndBuilding.InsertDocumentsInfo(inDocInfo, inBasicParam, RequestTime)
                    Next
                    If Not inLb_Insert.param_InsertAssetLoc Is Nothing Then
                        If Not AssetLocations.Insert_AllSisterUIDs(inLb_Insert.param_InsertAssetLoc, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                End If
            Next
            For Each inRef_Insert As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile In inParam.InsertReferencesWIP
                If Not inRef_Insert Is Nothing Then WIP_Profile.Insert(inRef_Insert, inBasicParam, RequestTime)
            Next
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

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

        Public Shared Function UpdateGift_Txn(UpParam As Param_Txn_Update_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each Voucher_Insert As Parameter_Insert_VoucherGift In UpParam.Insert
                If Not Voucher_Insert Is Nothing Then
                    If Not Voucher_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Dr_Led_ID)
                    If Not Voucher_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Cr_Led_ID)
                    If Not Voucher_Insert.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Sub_Cr_Led_ID)
                    'If Not Voucher_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.SUB_Dr_Led_ID)
                    If Not Voucher_Insert.PartyID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.PartyID)
                End If
            Next

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, UpParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not UpParam.param_UpdateMaster Is Nothing Then 'master updated 
                If Not UpdateMaster(UpParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.MID_Delete Is Nothing Then 'Txn Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & UpParam.MID_Delete & "'", inBasicParam)
            End If
            For Each inVoucher_Update As Parameter_Insert_VoucherGift In UpParam.Insert 'Actual Txn Re-Inserted
                If Not inVoucher_Update Is Nothing Then Insert(inVoucher_Update, inBasicParam, RequestTime, CommonParam)
            Next
            If Not UpParam.InsertJV Is Nothing Then 'JV Reinserted
                If Not Insert(UpParam.InsertJV, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & UpParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not UpParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & UpParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not UpParam.MID_DeleteGS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.GOLD_SILVER_INFO, "GS_TR_ID    ='" & UpParam.MID_DeleteGS & "'", inBasicParam)
            End If
            If Not UpParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & UpParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not UpParam.MID_DeleteVehicle Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.VEHICLES_INFO, "VI_TR_ID    ='" & UpParam.MID_DeleteVehicle & "'", inBasicParam)
            End If
            If Not UpParam.MID_ReferenceDelete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.WIP_INFO, "WIP_TR_ID    ='" & UpParam.MID_ReferenceDelete & "'", inBasicParam)
            End If
            If Not UpParam.MID_DeleteLS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIVE_STOCK_INFO, "LS_TR_ID    ='" & UpParam.MID_DeleteLS & "'", inBasicParam)
            End If
            For Each Param As String In UpParam.DeleteComplexBuilding
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID    ='" & Param & "'", inBasicParam)
            Next
            For Each delExtInfo As String In UpParam.DeleteExtendedInfo
                If Not delExtInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & delExtInfo & "'", inBasicParam)
            Next
            For Each delDocInfo As String In UpParam.DeleteDocumentInfo
                If Not delDocInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & delDocInfo & "'", inBasicParam)
            Next
            'For Each delByLB As String In UpParam.DeleteByLB
            ' If Not delByLB Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & delByLB & "' ", inBasicParam)
            ' Next 'Commented as location wont be updated on update of property creation voucher
            If Not UpParam.MID_DeleteLandB Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_INFO, "LB_TR_ID    ='" & UpParam.MID_DeleteLandB & "'", inBasicParam)
            End If
            For Each inItem_Update As Parameter_InsertItem_VoucherGift In UpParam.InsertItem 'Item Re-Inserted
                If Not inItem_Update Is Nothing Then InsertItem(inItem_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inPurpose_Update As Parameter_InsertPurpose_VoucherGift In UpParam.InsertPurpose 'Purpose Re-Inserted
                If Not inPurpose_Update Is Nothing Then InsertPurpose(inPurpose_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inGS_Update As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver In UpParam.InsertGS 'GS Re-Inserted
                If Not inGS_Update Is Nothing Then GoldSilver.Insert(inGS_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inAssets_Update As Assets.Parameter_InsertTRIDAndTRSrNo_Assets In UpParam.InsertAssets 'Assets Re-Inserted
                If Not inAssets_Update Is Nothing Then Assets.Insert(inAssets_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inLS_Update As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock In UpParam.InsertLivestock 'Livestock Re-Inserted
                If Not inLS_Update Is Nothing Then Livestock.Insert(inLS_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inVeh_Update As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles In UpParam.InsertVehicles 'Vehicles Re-Inserted
                If Not inVeh_Update Is Nothing Then Vehicles.Insert(inVeh_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each upLb_Update As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding In UpParam.InsertProperty 'Property Re-Inserted
                If Not upLb_Update Is Nothing Then 'lb PRESENT ?
                    LandAndBuilding.Insert(upLb_Update, inBasicParam, RequestTime, CommonParam)

                    'Add Extended Info
                    If Not upLb_Update.InsertExtInfo Is Nothing Then 'Extended Info Re-Inserted
                        For Each upExtInfo As LandAndBuilding.Parameter_InsertExtendedInfo_LandAndBuilding In upLb_Update.InsertExtInfo
                            If Not upExtInfo Is Nothing Then LandAndBuilding.InsertExtendedInfo(upExtInfo, inBasicParam, RequestTime, CommonParam)
                        Next
                    End If
                    'Add LB docs 
                    If Not upLb_Update.InsertDocInfo Is Nothing Then 'Documents Info Re-Inserted
                        For Each upDocInfo As LandAndBuilding.Parameter_InsertDocInfo_LandAndBuilding In upLb_Update.InsertDocInfo
                            If Not upDocInfo Is Nothing Then LandAndBuilding.InsertDocumentsInfo(upDocInfo, inBasicParam, RequestTime, CommonParam)
                        Next
                    End If
                    'Add Location
                    'If Not upLb_Update.param_InsertAssetLoc Is Nothing Then
                    'If Not AssetLocations.Insert(upLb_Update.param_InsertAssetLoc, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    'End If'Commented as location wont be updated on update of property creation voucher
                End If
            Next
            For Each inRef_Insert As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile In UpParam.InsertReferencesWIP
                If Not inRef_Insert Is Nothing Then WIP_Profile.Insert(inRef_Insert, inBasicParam, RequestTime, CommonParam)
            Next
            '   End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & UpParam.MID_Delete & "'", inBasicParam)
            InsertSpecialVoucherReference(UpParam.UpdateSplVchrRefs, UpParam.param_UpdateMaster.RecID, Nothing, Nothing, inBasicParam)
            'If UpParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If UpParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In UpParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, UpParam.param_UpdateMaster.RecID, UpParam.param_UpdateMaster.SubTotal, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function DeleteGift_Txn(DelParam As Param_Txn_Delete_VoucherGift, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            '   Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & DelParam.MID_Delete & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteGS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.GOLD_SILVER_INFO, "GS_TR_ID    ='" & DelParam.MID_DeleteGS & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & DelParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteLS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIVE_STOCK_INFO, "LS_TR_ID    ='" & DelParam.MID_DeleteLS & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteVehicle Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.VEHICLES_INFO, "VI_TR_ID    ='" & DelParam.MID_DeleteVehicle & "'", inBasicParam)
            End If
            If Not DelParam.MID_ReferenceDelete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.WIP_INFO, "WIP_TR_ID    ='" & DelParam.MID_ReferenceDelete & "'", inBasicParam)
            End If
            For Each delComplexInfo As String In DelParam.DeleteComplexBuilding
                If Not delComplexInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID   ='" & delComplexInfo & "'", inBasicParam)
            Next
            For Each delExtInfo As String In DelParam.DeleteExtendedInfo
                If Not delExtInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & delExtInfo & "'", inBasicParam)
            Next
            For Each delDocInfo As String In DelParam.DeleteDocumentInfo
                If Not delDocInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & delDocInfo & "'", inBasicParam)
            Next
            For Each delByLB As String In DelParam.DeleteByLB
                If Not delByLB Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & delByLB & "' ", inBasicParam)
            Next
            If Not DelParam.MID_DeleteLandB Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_INFO, "LB_TR_ID    ='" & DelParam.MID_DeleteLandB & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & DelParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & DelParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not DelParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, DelParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            Return True
        End Function

    End Class
End Namespace
