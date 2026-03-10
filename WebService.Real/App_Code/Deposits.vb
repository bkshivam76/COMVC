Imports System.Data

Namespace Real
    <Serializable>
    Public Class Deposits
#Region "Param Classes"
        <Serializable>
        Public Class Param_Deposits_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Deposits
            Public ItemID As String
            Public AgainstInsurance As String
            Public PartyID As String
            Public InsCompanyMiscID As String
            Public DepositDate As String
            Public DepositPeriod As Double
            Public Amount As Double
            Public InterestRate As Double
            Public MaturityDate As String
            Public Purpose As String
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRID_Deposits : Inherits Parameter_Insert_Deposits
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdDepID As String  ' Contains old RecId of Deposit that is being re-posted(updated)
            Public Screen As ConnectOneWS.ClientScreen
        End Class
        <Serializable>
        Public Class Parameter_Update_Deposits
            Public ItemID As String
            Public AgainstInsurance As String
            Public PartyID As String
            Public InsCompanyMiscID As String
            Public DepositDate As String
            Public DepositPeriod As Double
            Public Amount As Double
            Public InterestRate As Double
            Public MaturityDate As String
            Public Purpose As String
            Public Remarks As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Deposits_GetListCommon
            Public PartyID As String
            Public ItemID As String
            Public TxnMID As String
            Public TxnSrNo As Integer = 0
            Public CreationTxnRecID As String
            Public Dep_Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_GetPaymentsForParties
            Public Party_IDs As String = ""
            Public YearID As String
            Public NextUnAuditedYearID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetDepositProfileListing
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Deposit_RecID As String = ""
            Public Acc_Party As Boolean? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetReceiptDeposits
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Dep_RecID As String = ""
            Public Dep_PartyID As String = ""
            Public Dep_ItemID As String = ""
            Public TR_M_ID As String = ""
        End Class
        <Serializable>
        Public Class Param_CheckIfDepositFinalPaymentMade
            Public DepositID As String
            Public Tr_M_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Gets Deposits List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Deposits_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT '' AS ITEM_NAME, DI_AGAINST_INSURANCE,DI_ITEM_ID ,DI_PARTY_ID, '' AS PARTY_NAME,DI_DEP_DATE,DI_DEP_PERIOD,DI_INT_RATE,DI_MAT_DATE,DI_DEP_AMT as Deposit, 0 as Addition,0 as Adjusted,0 as Refund,0 AS 'Out-Standing',DI_PURPOSE as Reason,DI_OTHER_DETAIL,CASE WHEN DI_TR_ID IS NULL  OR DI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE DI_TR_ID END AS TR_ID, DI_COD_YEAR_ID AS YearID , REC_ID AS ID  , CASE WHEN DI_TR_ID IS NULL OR DI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END  as Type, " & Common.Remarks_Detail("DEPOSITS_INFO", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("DEPOSITS_INFO") & "" &
                                 " FROM DEPOSITS_INFO " &
                                 " Where   REC_STATUS IN (0,1,2) AND DI_CEN_ID=" & inBasicParam.openCenID.ToString & "  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID)) AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            Return dbService.List(ConnectOneWS.Tables.DEPOSITS_INFO, onlineQuery, ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Deposits Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Param_GetDepositProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Deposits_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "DEPOSIT_RECID", "ACC_PARTY", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Deposit_RecID = "" Then Param.Deposit_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Deposit_RecID, Param.Acc_Party, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 1, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.DEPOSITS_INFO, SPName, ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function To Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Param_Deposits_GetListCommon = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    Query = " SELECT 0 AS Sr,DI_DEP_DATE  AS REF_DATE  ,DI_DEP_AMT AS REF_AMT ,0 as REF_ADDITION, 0 as REF_ADJUSTED,0 as REF_REFUND,0 as REF_OUTSTAND,'' AS REF_ITEM, DI_ITEM_ID AS REF_ITEM_ID,DI_PURPOSE AS REF_PURPOSE ,DI_OTHER_DETAIL AS REF_OTHER_DETAIL ,REC_ID AS REF_ID FROM DEPOSITS_INFO WHERE REC_STATUS IN (0,1,2) AND DI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND DI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Param.PartyID & "')) AND DI_ITEM_ID='" & Param.ItemID & "'  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR  DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID)) AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") order by DI_DEP_DATE "
                    If Not Param.Dep_Rec_ID Is Nothing Then 'added for multiuser check
                        Query = "SELECT 0 AS Sr,DI_DEP_DATE  AS REF_DATE  ,DI_DEP_AMT AS REF_AMT ,0 as REF_ADDITION, 0 as REF_ADJUSTED,0 as REF_REFUND,0 as REF_OUTSTAND,'' AS REF_ITEM, DI_ITEM_ID AS REF_ITEM_ID,DI_PURPOSE AS REF_PURPOSE ,DI_OTHER_DETAIL AS REF_OTHER_DETAIL ,REC_ID AS REF_ID FROM DEPOSITS_INFO WHERE REC_STATUS IN (0,1,2) AND DI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND DI_PARTY_ID IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Param.PartyID & "')) AND DI_ITEM_ID='" & Param.ItemID & "' AND REC_ID = '" & Param.Dep_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = "SELECT REC_ID, DI_TR_ID, DI_TR_SR_NO FROM DEPOSITS_INFO WHERE DI_TR_ID ='" & Param.TxnMID & "' AND REC_STATUS IN (0,1,2) and DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " "
                    If Param.TxnSrNo > 0 Then Query += " AND DI_TR_SR_NO = " & Param.TxnSrNo
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "SELECT ITEM_NAME AS Item, C_NAME AS Party, DI_DEP_AMT  as 'Org Value', DI_DEP_AMT as 'Curr Value',CONVERT(VARCHAR, DI_DEP_DATE , 107)  as Date, di.REC_ID, DI_DEP_PERIOD AS Period, CONVERT(VARCHAR, DI_MAT_DATE , 107)  AS Due ,COALESCE(DI_PURPOSE,'') as Reason, COALESCE(DI_OTHER_DETAIL,'') as Detail FROM deposits_info AS DI INNER JOIN item_info as ii on di.DI_ITEM_ID = ii.REC_ID INNER JOIN address_book AS AB  ON AB.REC_ID = dI.DI_PARTY_ID AND c_CEN_ID =" & inBasicParam.openCenID.ToString & " WHERE DI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND DI_ITEM_ID='" & Param.ItemID & "' AND DI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Param.PartyID & "')) and di.REC_STATUS IN (0,1,2)  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR  DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID)) AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.CreationTxnRecID Is Nothing Then
                        If Param.CreationTxnRecID.Length > 0 Then Query += " and (COALESCE(DI.DI_TR_ID,'') <> '" & Param.CreationTxnRecID & "')"
                    End If
                    If Not Param.Dep_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND di.REC_ID = '" & Param.Dep_Rec_ID & "'"
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.DEPOSITS_INFO, Query, ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List By Condition
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT  REC_ID AS ID,DI_ITEM_ID,CASE WHEN DI_AGAINST_INSURANCE ='YES' THEN DI_INSURANCE_MISC_ID ELSE DI_PARTY_ID END AS PARTY_ID,DI_DEP_DATE,DI_DEP_PERIOD,DI_DEP_AMT,DI_INT_RATE,DI_MAT_DATE,DI_PURPOSE,DI_OTHER_DETAIL FROM Deposits_Info " &
                    " Where   REC_STATUS IN (0,1,2) AND DI_CEN_ID=" & inBasicParam.openCenID.ToString & "  " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.DEPOSITS_INFO, onlineQuery, ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function CheckIfDepositFinalPaymentMade(ByVal inParam As Param_CheckIfDepositFinalPaymentMade, inBasicParam As ConnectOneWS.Basic_Param) As DateTime
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT ti.TR_DATE FROM transaction_info AS TI INNER JOIN transaction_d_master_info  AS TM ON TI.TR_M_ID = TM.REC_ID WHERE TI.TR_CODE = 4 AND TR_TRF_CROSS_REF_ID = '" & inParam.DepositID & "' AND TM.TR_RECEIPT_TYPE =1 AND TM.REC_ID <> '" & inParam.Tr_M_ID & "' AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND COALESCE(TR_TRF_CROSS_REF_ID,'') IN (SELECT REC_ID FROM DEPOSITS_INFO)"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Advance Listing for a specific party and Item for Receipt Voucher 
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetReceiptDeposits(ByVal inParam As Param_GetReceiptDeposits, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "DI_REC_ID", "DI_PARTY_ID", "DI_ITEM_ID", "TR_M_ID"}
            'If inParam.Next_YearID = "" Then inParam.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If inParam.Prev_YearId = "" Then inParam.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Dep_RecID = "" Then inParam.Dep_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Dep_PartyID = "" Then inParam.Dep_PartyID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Dep_ItemID = "" Then inParam.Dep_ItemID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.TR_M_ID = "" Then inParam.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Prev_YearId, inParam.Next_YearID, inParam.Dep_RecID, inParam.Dep_PartyID, inParam.Dep_ItemID, inParam.TR_M_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String], DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 4, 4, 36, 36, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.DEPOSITS_INFO, "sp_get_Receipt_Deposits", ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Payments For Parties
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetPaymentsForParties</remarks>
        Public Shared Function GetPaymentsForParties(ByVal InParam As Param_GetPaymentsForParties, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NextYear As String = ""
            If InParam.NextUnAuditedYearID.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID = " & InParam.NextUnAuditedYearID.ToString & " "
            Dim Query As String = " SELECT TP.TR_REF_ID, SUM( CASE WHEN TP.TR_PAY_TYPE IN ('ADJUSTMENT') THEN TP.TR_REF_AMT ELSE 0 END) AS Adjusted, SUM( CASE WHEN TP.TR_PAY_TYPE IN ('REFUND') THEN TP.TR_REF_AMT ELSE 0 END) AS Refund FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT') AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN (" & InParam.Party_IDs & ")))  AND TM.TR_CODE=4  and (TM.TR_COD_YEAR_ID =" & InParam.YearID.ToString & " " & NextYear & " ) GROUP BY TP.TR_REF_ID " ' user will explicitly send prev year and then current year in case the prev year is unaudited 
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Transaction Count
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_GetTransactionCount</remarks>
        Public Shared Function GetTransactionCount(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(TR_REF_ID) FROM Transaction_D_Payment_Info  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_REF_ID  = '" & RecID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Deposits Info
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Deposits, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.InsCompanyMiscID.Trim.Length = 0 Then InParam.InsCompanyMiscID = "NULL" Else InParam.InsCompanyMiscID = "'" & InParam.InsCompanyMiscID & "'"
            Dim OnlineQuery As String = "INSERT INTO Deposits_Info(DI_CEN_ID,DI_COD_YEAR_ID,DI_ITEM_ID,DI_AGAINST_INSURANCE,DI_PARTY_ID,DI_INSURANCE_MISC_ID,DI_DEP_DATE,DI_DEP_PERIOD,DI_DEP_AMT,DI_INT_RATE,DI_MAT_DATE,DI_PURPOSE,DI_OTHER_DETAIL," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,DI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam.ItemID & "', " &
                                                  "'" & InParam.AgainstInsurance & "', " &
                                                  "'" & InParam.PartyID & "', " &
                                                  " " & InParam.InsCompanyMiscID & " , " &
                                                  " " & If(IsDate(InParam.DepositDate), "'" & Convert.ToDateTime(InParam.DepositDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InParam.DepositPeriod & " , " &
                                                  " " & InParam.Amount & " , " &
                                                  " " & InParam.InterestRate & " , " &
                                                  " " & If(IsDate(InParam.MaturityDate), "'" & Convert.ToDateTime(InParam.MaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InParam.Purpose & "', " &
                                                  "'" & InParam.Remarks & "', " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.DEPOSITS_INFO, OnlineQuery, inBasicParam, InParam.RecID)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Deposits_InsertTRID</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRID_Deposits, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam1.InsCompanyMiscID.Trim.Length = 0 Then InParam1.InsCompanyMiscID = "NULL" Else InParam1.InsCompanyMiscID = "'" & InParam1.InsCompanyMiscID & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Deposits_Info(DI_CEN_ID,DI_COD_YEAR_ID,DI_ITEM_ID,DI_AGAINST_INSURANCE,DI_PARTY_ID,DI_INSURANCE_MISC_ID,DI_DEP_DATE,DI_DEP_PERIOD,DI_DEP_AMT,DI_INT_RATE,DI_PURPOSE,DI_OTHER_DETAIL,DI_TR_ID,DI_TR_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,DI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.ItemID & "', " &
                                                  "'" & InParam1.AgainstInsurance & "', " &
                                                  "'" & InParam1.PartyID & "', " &
                                                  " " & InParam1.InsCompanyMiscID & " , " &
                                                  " " & If(IsDate(InParam1.DepositDate), "'" & Convert.ToDateTime(InParam1.DepositDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InParam1.DepositPeriod & " , " &
                                                  " " & InParam1.Amount & " , " &
                                                  " " & InParam1.InterestRate & " , " &
                                                  "'" & InParam1.Purpose & "', " &
                                                  "'" & InParam1.Remarks & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  "" & InParam1.TxnSrNo & ", " &
                                                  "" & Str & "  '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.DEPOSITS_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
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
        ''' <remarks>RealServiceFunctions.Deposits_Update, uses Parameter_Update_Deposits </remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Deposits, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.InsCompanyMiscID.Trim.Length = 0 Then UpParam.InsCompanyMiscID = "NULL" Else UpParam.InsCompanyMiscID = "'" & UpParam.InsCompanyMiscID & "'"
            Dim OnlineQuery As String = " UPDATE Deposits_Info SET " &
                                         "DI_ITEM_ID           ='" & UpParam.ItemID & "', " &
                                         "DI_AGAINST_INSURANCE ='" & UpParam.AgainstInsurance & "', " &
                                         "DI_PARTY_ID          ='" & UpParam.PartyID & "', " &
                                         "DI_INSURANCE_MISC_ID = " & UpParam.InsCompanyMiscID & " , " &
                                         "DI_DEP_DATE          =" & If(IsDate(UpParam.DepositDate), "'" & Convert.ToDateTime(UpParam.DepositDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                         "DI_DEP_PERIOD        =" & UpParam.DepositPeriod & ", " &
                                         "DI_DEP_AMT           =" & UpParam.Amount & " ,  " &
                                         "DI_INT_RATE          =" & UpParam.InterestRate & " ,  " &
                                         "DI_MAT_DATE          = " & If(IsDate(UpParam.MaturityDate), "'" & Convert.ToDateTime(UpParam.MaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                         "DI_PURPOSE           ='" & UpParam.Purpose & "',  " &
                                         "DI_OTHER_DETAIL      ='" & UpParam.Remarks & "',  " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.DEPOSITS_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace
