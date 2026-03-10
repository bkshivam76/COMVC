Imports System.Data
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class Voucher_Journal
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucheJournal
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public SubTotal As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherJournal
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Qty As Decimal
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Party1 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public CrossRefID As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            ' Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherJournal
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public SrNo As Integer
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_VoucherJournal
            Public VNo As String
            Public TDate As String
            Public SubTotal As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_GetJornalEntryAdjustments
            Public CrossRefId As String
            Public SpecifiedEntryType As EntryType
            Public Enum EntryType
                Debit_Only
                Credit_Only
                Both
            End Enum
            Public Excluded_Rec_M_ID As String
            Public YearID As Integer
            Public NextUnauditedYearID As Integer
        End Class

        <Serializable>
        Public Class Parameter_GetCurrentRecAdjustment
            Public CrossRefId As String
            Public Rec_M_ID As String
        End Class
        <Serializable>
        Public Class Param_Get_JV_Asset_Listing
            Public Asset_Profile As ConnectOneWS.AssetProfiles
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TR_M_ID As String
            Public Item_ID As String
            Public PartyID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherJournal
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucheJournal
            Public InsertAdvances() As Advances.Parameter_InsertTRID_Advances = Nothing
            Public InsertLiabilities() As Liabilities.Parameter_InsertTRID_Liabilities = Nothing
            Public InsertDeposits() As Deposits.Parameter_InsertTRID_Deposits = Nothing
            Public Insert() As Parameter_Insert_VoucherJournal = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherJournal = Nothing
            Public InsertGS() As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver = Nothing
            Public InsertAssets() As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public InsertLivestock() As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock = Nothing
            Public InsertVehicles() As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles = Nothing
            Public InsertProperty() As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = Nothing
            Public InsertReferencesWIP() As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherJournal
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherJournal
            Public MID_Delete As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeleteAdvances As String = Nothing
            Public MID_DeleteLiabilities As String = Nothing
            Public MID_DeleteDeposits As String = Nothing
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
            Public InsertAdvances() As Advances.Parameter_InsertTRID_Advances = Nothing
            Public InsertLiabilities() As Liabilities.Parameter_InsertTRID_Liabilities = Nothing
            Public InsertDeposits() As Deposits.Parameter_InsertTRID_Deposits = Nothing
            Public Insert() As Parameter_Insert_VoucherJournal = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherJournal = Nothing
            Public InsertGS() As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver = Nothing
            Public InsertAssets() As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public InsertLivestock() As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock = Nothing
            Public InsertVehicles() As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles = Nothing
            Public InsertProperty() As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding = Nothing
            Public InsertReferencesWIP() As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherJournal
            Public MID_Delete As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeleteAdvances As String = Nothing
            Public MID_DeleteLiabilities As String = Nothing
            Public MID_DeleteDeposits As String = Nothing
            Public MID_DeleteMaster As String = Nothing
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
        End Class
#End Region
        'RealServiceFunctions.Journal_GetDataRecords
        Public Shared Function GetRecordData(ByVal TxnMId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT TR_DATE, TR_VNO,TR_SR_NO,TR_ITEM_ID,TR_TYPE,TR_DR_LED_ID,TR_CR_LED_ID, TR_M_ID, II.Item_Voucher_Type,II.Item_Party_Req,II.Item_Profile,II.Item_Name , " &
                                    "LED_NAME,TR_AB_ID_1 AS PartyID,AB.C_NAME AS PARTY, TP.TR_PURPOSE_MISC_ID AS Pur_ID, TR_QTY AS Qty,0.00 as Rate,  " &
                                     "CASE WHEN UPPER(TI.TR_TYPE)='DEBIT' THEN CAST(TI.TR_AMOUNT AS VARCHAR) ELSE '' END AS Debit,  " &
                                     "CASE WHEN UPPER(TI.TR_TYPE)='CREDIT' THEN CAST(TI.TR_AMOUNT AS VARCHAR) ELSE '' END AS Credit,  " &
                                     "TI.TR_REMARKS,TI.TR_TRF_CROSS_REF_ID , " &
                                     "CASE WHEN II2.ITEM_NAME IS NOT NULL THEN II2.ITEM_NAME " &
                                    "			ELSE CASE WHEN AVI.AI_ADV_AMT IS NOT NULL THEN CAST(AVI.AI_ADV_AMT  AS VARCHAR) " &
                                    "				ELSE CASE WHEN II4.ITEM_NAME IS NOT NULL THEN II4.ITEM_NAME " &
                                    "					ELSE CASE WHEN II5.ITEM_NAME IS NOT NULL THEN II5.ITEM_NAME	" &
                                    "						ELSE CASE WHEN LS.LS_NAME IS NOT NULL THEN LS.LS_NAME " &
                                    "							ELSE CASE WHEN DI.DI_DEP_AMT IS NOT NULL THEN CAST(DI.DI_DEP_AMT  AS VARCHAR) " &
                                    "								ELSE CASE WHEN LI.LI_AMT IS NOT NULL THEN CAST(LI.LI_AMT  AS VARCHAR) " &
                                    "									ELSE CASE WHEN II6.ITEM_NAME IS NOT NULL THEN II6.ITEM_NAME END " &
                                    "            End " &
                                    "            End " &
                                    "            End " &
                                    "            End " &
                                    "            End " &
                                    "            End " &
                                    "	  END AS CROSS_REFERENCE , TI.TR_NARRATION, TI.TR_REFERENCE, MSI.MISC_NAME AS Purpose,(SELECT MAX(REC_EDIT_ON) FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID AND TR_SR_NO =1) as REC_EDIT_ON, CASE WHEN AB.REC_ID IS NOT NULL THEN AB.REC_EDIT_ON ELSE '' END AS Party_RecEditOn" &
                                     ",'' AS LOC_ID, '' AS GS_DESC_MISC_ID,0.000 AS GS_ITEM_WEIGHT,  " &
                                     " '' AS AI_TYPE,'' AS AI_MAKE,'' AS AI_MODEL,'' AS AI_SERIAL_NO,0 AS AI_WARRANTY, '' AS AI_PUR_DATE ,   " &
                                     " '' AS LS_NAME,'' AS LS_BIRTH_YEAR,'' AS LS_INSURANCE,'' AS LS_INSURANCE_ID, '' AS LS_INS_POLICY_NO, 0 AS LS_INS_AMT, '' AS LS_INS_DATE ,   " &
                                     " '' AS VI_MAKE, '' AS VI_MODEL, '' AS VI_REG_NO_PATTERN, '' AS VI_REG_NO, '' AS VI_REG_DATE, '' AS VI_OWNERSHIP, '' AS VI_OWNERSHIP_AB_ID, '' AS VI_DOC_RC_BOOK, '' AS VI_DOC_AFFIDAVIT, '' AS VI_DOC_WILL, '' AS VI_DOC_TRF_LETTER, '' AS VI_DOC_FU_LETTER, '' AS VI_DOC_OTHERS, '' AS VI_DOC_NAME, '' AS VI_INSURANCE_ID, '' AS VI_INS_POLICY_NO, '' AS VI_INS_EXPIRY_DATE, " &
                                     " '' AS LB_PRO_TYPE, '' AS LB_PRO_CATEGORY, '' AS LB_PRO_USE, '' AS LB_PRO_NAME, '' AS LB_PRO_ADDRESS, '' AS LB_OWNERSHIP, '' AS LB_OWNERSHIP_PARTY_ID, '' AS LB_SURVEY_NO, '' AS LB_CON_YEAR, '' AS LB_RCC_ROOF, '' AS LB_PAID_DATE, '' AS LB_PERIOD_FROM, '' AS LB_PERIOD_TO, '' AS LB_DOC_OTHERS, '' AS LB_DOC_NAME, '' AS LB_OTHER_DETAIL , 0.000 AS LB_TOT_P_AREA, 0.000 AS LB_CON_AREA, 0.000 AS LB_DEPOSIT_AMT, 0.000 AS LB_MONTH_RENT, 0.000 AS LB_MONTH_O_PAYMENTS, '' AS LB_REC_ID, " &
                                     " '' AS LB_ADDRESS1,'' AS LB_ADDRESS2,'' AS LB_ADDRESS3,'' AS LB_ADDRESS4,'' AS LB_COUNTRY_ID,'' AS LB_STATE_ID,'' AS LB_DISTRICT_ID,'' AS LB_CITY_ID,'' AS LB_PINCODE, " &
                                     " '' AS WIP_REF, '' AS WIP_REC_ID, '' AS WIP_REF_TYPE " &
                                    " from transaction_info AS TI " &
                                    " LEFT OUTER JOIN transaction_d_purpose_info AS TP ON TI.TR_M_ID = TP.TR_REC_ID AND TI.TR_SR_NO = TP.TR_ITEM_SR_NO " &
                                    " LEFT OUTER JOIN MISC_INFO AS MSI ON MSI.REC_ID = TP.TR_PURPOSE_MISC_ID " &
                                    " LEFT OUTER JOIN item_info AS II ON II.REC_ID = TI.TR_ITEM_ID " &
                                    " LEFT OUTER JOIN acc_ledger_info  AS AL ON II.ITEM_LED_ID = AL.LED_ID " &
                                    " LEFT OUTER JOIN address_book AS AB ON TI.TR_AB_ID_1=AB.REC_ID " &
                                    " LEFT OUTER JOIN asset_info AS AI ON AI.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN item_info AS II2 ON AI.AI_ITEM_ID = II2.REC_ID " &
                                    " LEFT OUTER JOIN advances_info AS AVI ON AVI.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN gold_silver_info AS GS ON GS.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN item_info AS II4 ON GS.GS_ITEM_ID = II4.REC_ID " &
                                    " LEFT OUTER JOIN land_building_info AS LB ON LB.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN item_info AS II5 ON LB.LB_ITEM_ID = II5.REC_ID " &
                                    " LEFT OUTER JOIN live_stock_info AS LS ON LS.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN deposits_info AS DI ON DI.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN liabilities_info AS LI ON LI.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN vehicles_info AS VI ON VI.REC_ID = TI.TR_TRF_CROSS_REF_ID " &
                                    " LEFT OUTER JOIN item_info AS II6 ON VI.VI_ITEM_ID = II6.REC_ID " &
                                    " WHERE TR_M_ID ='" & TxnMId & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)

        End Function

        'RealServiceFunctions.Journal_GetJornalEntryAdjustments
        Public Shared Function GetJornalEntryAdjustments(ByVal inParam As Parameter_GetJornalEntryAdjustments, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim TypeString As String = ""
            If inParam.SpecifiedEntryType = Parameter_GetJornalEntryAdjustments.EntryType.Credit_Only Then TypeString = " AND UPPER(TR_TYPE) = 'CREDIT' "
            If inParam.SpecifiedEntryType = Parameter_GetJornalEntryAdjustments.EntryType.Debit_Only Then TypeString = " AND UPPER(TR_TYPE) = 'DEBIT' "

            Dim ExcludeMasterID As String = ""
            If Not inParam.Excluded_Rec_M_ID Is Nothing Then
                ExcludeMasterID = " AND TR_M_ID <> '" & inParam.Excluded_Rec_M_ID & "' "
            End If
            If inParam.YearID = Nothing Then inParam.YearID = inBasicParam.openYearID ' Get data for Current Year If not specified

            Dim NextYear As String = ""
            If inParam.NextUnauditedYearID.ToString.Length > 0 Then NextYear = " or TR_COD_YEAR_ID =" & inParam.NextUnauditedYearID.ToString & ""

            Dim Query As String = ""

            If Not inParam.CrossRefId Is Nothing Then
                Query = " select SUM(TR_AMOUNT) AS AMOUNT, TR_TYPE, TR_TRF_CROSS_REF_ID,SUM(TR_QTY) AS QTY from transaction_info where TR_TRF_CROSS_REF_ID ='" & inParam.CrossRefId & "' AND TR_CODE=14 " & TypeString & " AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) " & ExcludeMasterID & " and (TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & ") GROUP BY TR_TYPE, TR_TRF_CROSS_REF_ID"
            Else
                Query = " select SUM(TR_AMOUNT) AS AMOUNT, TR_TYPE, TR_TRF_CROSS_REF_ID,SUM(TR_QTY) AS QTY from transaction_info where TR_TRF_CROSS_REF_ID IS NOT NULL AND TR_CODE=14 " & TypeString & " AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) " & ExcludeMasterID & " and (TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & ") GROUP BY TR_TYPE, TR_TRF_CROSS_REF_ID"
            End If

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inParam.SpecifiedEntryType.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gives Adjustment made to selected item in current record
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>Journal_GetCurrentRecAdjustment</remarks>
        Public Shared Function GetGetCurrentRecAdjustment(ByVal inParam As Parameter_GetCurrentRecAdjustment, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Query = " select SUM(TR_AMOUNT) AS AMOUNT,SUM(TR_QTY) AS QTY,TR_TYPE from transaction_info where TR_TRF_CROSS_REF_ID ='" & inParam.CrossRefId & "' AND TR_CODE=14 AND TR_M_ID ='" & inParam.Rec_M_ID & "' AND REC_STATUS IN (0,1,2)  GROUP BY TR_TYPE"

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_JV_Asset_Listing(Param As Param_Get_JV_Asset_Listing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_JV_Asset_Listing"
            Dim params() As String = {"CENID", "YEARID", "ASSET_PROFILE", "ITEM_ID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "TR_M_ID", "PARTY_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Item_ID = "" Then Param.Item_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.PartyID = "" Then Param.PartyID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, dbService.GetAssetProfileFromEnum(Param.Asset_Profile), Param.Item_ID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, Param.TR_M_ID, Param.PartyID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 36, 4, 4, 36, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucheJournal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_SUB_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InMInfo.TxnCode & "'," &
                                                  "'" & InMInfo.VNo & "', " &
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InMInfo.SubTotal & "," &
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
        ''' <remarks>RealServiceFunctions.Payments_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Or InParam.Cr_Led_ID = "NULL" Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Or InParam.Dr_Led_ID = "NULL" Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.CrossRefID.Length = 0 Or InParam.CrossRefID = "NULL" Then InParam.CrossRefID = "NULL" Else If Not InParam.CrossRefID.StartsWith("'") Then InParam.CrossRefID = "'" & InParam.CrossRefID & "'"
            If InParam.MasterTxnID.Length = 0 Or InParam.MasterTxnID = "NULL" Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Or InParam.Mode = "NULL" Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.Party1.Length = 0 Or InParam.Party1 = "NULL" Then InParam.Party1 = "NULL" Else If Not InParam.Party1.StartsWith("'") Then InParam.Party1 = "'" & InParam.Party1 & "'"
            If InParam.SrNo.Length = 0 Or InParam.SrNo = "NULL" Then InParam.SrNo = "NULL"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID," &
                                        "TR_MODE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_TRF_CROSS_REF_ID,TR_M_ID,TR_SR_NO,TR_QTY," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                        ") VALUES(" &
                                                        "" & inBasicParam.openCenID.ToString & "," &
                                                        "" & inBasicParam.openYearID.ToString & "," &
                                                        " " & InParam.TransCode & "," &
                                                            "'" & InParam.VNo & "', " &
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                            "'" & InParam.ItemID & "', " &
                                                            "'" & InParam.Type & "', " &
                                                            " " & InParam.Cr_Led_ID & " , " &
                                                            " " & InParam.Dr_Led_ID & " , " &
                                                            " " & InParam.Mode & " , " &
                                                            " " & InParam.Amount & ", " &
                                                            " " & InParam.Party1 & ", " &
                                                            "'" & InParam.Narration & "', " &
                                                            "'" & InParam.Remarks & "', " &
                                                            "'" & InParam.Reference & "', " &
                                                            " " & InParam.CrossRefID & " , " &
                                                            " " & InParam.MasterTxnID & " , " &
                                                            " " & InParam.SrNo & " , " &
                                                            " " & InParam.Qty & " , " &
                                                          "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        '''  Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Payments_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InPurpose.TxnID & "'," &
                                                  "'" & InPurpose.PurposeID & "', " &
                                                  " " & InPurpose.Amount & ", " &
                                                  " " & InPurpose.SrNo & ", " &
                                        "" & Str & "  '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
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
        ''' <remarks>RealServiceFunctions.Payments_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " &
                                            " TR_VNO         ='" & UpParam.VNo & "', " &
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        Public Shared Function InsertJournal_Txn(inParam As Param_Txn_Insert_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each upVoucher_Update As Parameter_Insert_VoucherJournal In inParam.Insert
                If Not upVoucher_Update Is Nothing Then
                    If Not upVoucher_Update.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Dr_Led_ID)
                    If Not upVoucher_Update.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                    If Not upVoucher_Update.Party1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
                End If
            Next

            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Advances.Parameter_InsertTRID_Advances In inParam.InsertAdvances
                If Not Param Is Nothing Then Advances.Insert(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Liabilities.Parameter_InsertTRID_Liabilities In inParam.InsertLiabilities
                If Not Param Is Nothing Then Liabilities.Insert(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Deposits.Parameter_InsertTRID_Deposits In inParam.InsertDeposits
                If Not Param Is Nothing Then Deposits.Insert(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_Insert_VoucherJournal In inParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherJournal In inParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime)
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
            '  txn.Complete()
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

        Public Shared Function UpdateJournal_Txn(upParam As Param_Txn_Update_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each upVoucher_Update As Parameter_Insert_VoucherJournal In upParam.Insert
                If Not upVoucher_Update Is Nothing Then
                    If Not upVoucher_Update.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Dr_Led_ID)
                    If Not upVoucher_Update.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                    If Not upVoucher_Update.Party1 Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
                End If
            Next

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            'Using txn
            ' Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then 'Master Updated
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_DeleteAdvances Is Nothing Then 'Advances Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADVANCES_INFO, "AI_TR_ID = '" & upParam.MID_DeleteAdvances & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteDeposits Is Nothing Then 'Deposits Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.DEPOSITS_INFO, "DI_TR_ID = '" & upParam.MID_DeleteDeposits & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteLiabilities Is Nothing Then 'Liabilities deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIABILITIES_INFO, "LI_TR_ID = '" & upParam.MID_DeleteLiabilities & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then 'Purpose deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_Delete Is Nothing Then 'Actual txn deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteGS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.GOLD_SILVER_INFO, "GS_TR_ID    ='" & upParam.MID_DeleteGS & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & upParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteVehicle Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.VEHICLES_INFO, "VI_TR_ID    ='" & upParam.MID_DeleteVehicle & "'", inBasicParam)
            End If
            If Not upParam.MID_ReferenceDelete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.WIP_INFO, "WIP_TR_ID    ='" & upParam.MID_ReferenceDelete & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteLS Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIVE_STOCK_INFO, "LS_TR_ID    ='" & upParam.MID_DeleteLS & "'", inBasicParam)
            End If
            For Each Param As String In upParam.DeleteComplexBuilding
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID    ='" & Param & "'", inBasicParam)
            Next
            For Each delExtInfo As String In upParam.DeleteExtendedInfo
                If Not delExtInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & delExtInfo & "'", inBasicParam)
            Next
            For Each delDocInfo As String In upParam.DeleteDocumentInfo
                If Not delDocInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & delDocInfo & "'", inBasicParam)
            Next
            'For Each delByLB As String In UpParam.DeleteByLB
            ' If Not delByLB Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & delByLB & "' ", inBasicParam)
            ' Next 'Commented as location wont be updated on update of property creation voucher
            If Not upParam.MID_DeleteLandB Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_INFO, "LB_TR_ID    ='" & upParam.MID_DeleteLandB & "'", inBasicParam)
            End If
            For Each Param As Advances.Parameter_InsertTRID_Advances In upParam.InsertAdvances 'Advances Re-Inserted
                If Not Param Is Nothing Then Advances.Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Liabilities.Parameter_InsertTRID_Liabilities In upParam.InsertLiabilities 'Liabilities Re-Inserted
                If Not Param Is Nothing Then Liabilities.Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Deposits.Parameter_InsertTRID_Deposits In upParam.InsertDeposits 'Deposits Re-Inserted
                If Not Param Is Nothing Then Deposits.Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_Insert_VoucherJournal In upParam.Insert 'Actual Txn Re-Inserted
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherJournal In upParam.InsertPurpose 'Purpose Re-Inserted
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inGS_Update As GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver In upParam.InsertGS 'GS Re-Inserted
                If Not inGS_Update Is Nothing Then GoldSilver.Insert(inGS_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inAssets_Update As Assets.Parameter_InsertTRIDAndTRSrNo_Assets In upParam.InsertAssets 'Assets Re-Inserted
                If Not inAssets_Update Is Nothing Then Assets.Insert(inAssets_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inLS_Update As Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock In upParam.InsertLivestock 'Livestock Re-Inserted
                If Not inLS_Update Is Nothing Then Livestock.Insert(inLS_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each inVeh_Update As Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles In upParam.InsertVehicles 'Vehicles Re-Inserted
                If Not inVeh_Update Is Nothing Then Vehicles.Insert(inVeh_Update, inBasicParam, RequestTime, CommonParam)
            Next
            For Each upLb_Update As LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding In upParam.InsertProperty 'Property Re-Inserted
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
            For Each inRef_Insert As WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile In upParam.InsertReferencesWIP
                If Not inRef_Insert Is Nothing Then WIP_Profile.Insert(inRef_Insert, inBasicParam, RequestTime, CommonParam)
            Next
            ' End Using
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

        Public Shared Function DeleteJournal_Txn(delParam As Param_Txn_Delete_VoucherJournal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            ' Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
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
            If Not delParam.MID_ReferenceDelete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.WIP_INFO, "WIP_TR_ID    ='" & delParam.MID_ReferenceDelete & "'", inBasicParam)
            End If
            For Each delComplexInfo As String In delParam.DeleteComplexBuilding
                If Not delComplexInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID   ='" & delComplexInfo & "'", inBasicParam)
            Next
            For Each delExtInfo As String In delParam.DeleteExtendedInfo
                If Not delExtInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & delExtInfo & "'", inBasicParam)
            Next
            For Each delDocInfo As String In delParam.DeleteDocumentInfo
                If Not delDocInfo Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & delDocInfo & "'", inBasicParam)
            Next
            For Each delByLB As String In delParam.DeleteByLB
                If Not delByLB Is Nothing Then dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & delByLB & "' ", inBasicParam)
            Next
            If Not delParam.MID_DeleteLandB Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_INFO, "LB_TR_ID    ='" & delParam.MID_DeleteLandB & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteAdvances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADVANCES_INFO, "AI_TR_ID = '" & delParam.MID_DeleteAdvances & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteDeposits Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.DEPOSITS_INFO, "DI_TR_ID = '" & delParam.MID_DeleteDeposits & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteLiabilities Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LIABILITIES_INFO, "LI_TR_ID = '" & delParam.MID_DeleteLiabilities & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            ' End Using
            Return True
        End Function

    End Class
End Namespace
