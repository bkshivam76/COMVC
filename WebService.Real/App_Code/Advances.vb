Imports System.Data

Namespace Real
    <Serializable>
    Public Class Advances
#Region "Param Classes"
        <Serializable>
        Public Class Param_Advances_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Advances
            Public ItemID As String
            Public PartyID As String
            Public AdvanceDate As String
            Public Amount As Double
            Public Purpose As String
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRID_Advances : Inherits Parameter_Insert_Advances
            Public TxnID As String
            Public TxnSrNo As Integer
            Public Screen As ConnectOneWS.ClientScreen
            Public UpdAdvID As String  ' Contains old RecId of Advance that is being re-posted(updated)
        End Class
        <Serializable>
        Public Class Parameter_Update_Advances
            Public ItemID As String
            Public PartyID As String
            Public AdvanceDate As String
            Public Amount As Double
            Public Purpose As String
            Public Remarks As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Advances_GetList_Common
            Public PartyID As String
            Public ItemID As String
            Public TxnMID As String
            Public TxnSrNo As Integer = 0
            Public CreationTxnRecID As String
            Public Adv_Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_AdvGetPayments
            Public Party_IDs As String = ""
            Public YearID As String = ""
            Public NextUnAuditedYearID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetAdvProfileListing
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Advance_RecID As String = ""
            Public Acc_Party As Boolean? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetReceiptAdvances
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Advance_RecID As String = ""
            Public Advance_PartyID As String = ""
            Public Advance_ItemID As String = ""
            Public Tr_M_Id As String = ""
        End Class
        <Serializable>
        Public Class Param_GetPaymentAdvances
            Public Adv_RecId As String = ""
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Adv_Party_ID As String = ""
            Public Tr_M_Id As String = ""
        End Class
#End Region
        ''' <summary>
        ''' Gets Advances List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Advances_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim onlineQuery As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            onlineQuery = " SELECT '' AS ITEM_NAME, AI_ITEM_ID ,AI_PARTY_ID, '' AS PARTY_NAME ,AI_ADV_DATE,AI_ADV_AMT as Advance,0.00 as Addition,0 as Adjusted,0 as Refund,0 AS 'Out-Standing',AI_PURPOSE as Reason,AI_OTHER_DETAIL,CASE WHEN AI_TR_ID IS NULL OR AI_COD_YEAR_ID <> '" & inBasicParam.openYearID & "'  THEN '' ELSE AI_TR_ID END AS TR_ID ,AI_COD_YEAR_ID AS YearID, REC_ID AS ID  , CASE WHEN AI_TR_ID IS NULL OR AI_COD_YEAR_ID <> '" & inBasicParam.openYearID & "'  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as Type , " & Common.Remarks_Detail("ADVANCES_INFO", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("ADVANCES_INFO") & "" & _
                                 " FROM ADVANCES_INFO " & _
                                 " Where   REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID)) AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            Return dbService.List(ConnectOneWS.Tables.ADVANCES_INFO, onlineQuery, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Advances Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Param_GetAdvProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Advances_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ADVANCE_RECID", "ACC_PARTY", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Advance_RecID = "" Then Param.Advance_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Advance_RecID, Param.Acc_Party, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 1, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ADVANCES_INFO, SPName, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function, commented as not called anywhere 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="Parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Parameter As Param_Advances_GetList_Common = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = " SELECT 0 AS Sr,AI_ADV_DATE as 'Given Date','' AS Item, AI_ITEM_ID,'' as OFFSET_ID,AI_ADV_AMT as Advance, 0.00 as Addition,0 as 'Adjusted',0 as 'Refund',0 AS 'Out-Standing',0 as Payment,AI_PURPOSE as Purpose,AI_OTHER_DETAIL as 'Other Details',REC_ID AS AI_ID,REC_EDIT_ON FROM ADVANCES_INFO WHERE  REC_STATUS IN (0,1,2) AND AI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND AI_PARTY_ID IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "'))  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & "  and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID)) AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") order by AI_ADV_DATE "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    Query = " SELECT 0 AS Sr,AI_ADV_DATE  AS REF_DATE  ,AI_ADV_AMT AS REF_AMT, 0 AS REF_ADDITION,0 as REF_ADJUSTED,0 as REF_REFUND,0 as REF_OUTSTAND,'' AS REF_ITEM, AI_ITEM_ID AS REF_ITEM_ID,AI_PURPOSE AS REF_PURPOSE ,AI_OTHER_DETAIL AS REF_OTHER_DETAIL ,REC_ID AS REF_ID FROM ADVANCES_INFO WHERE REC_STATUS IN (0,1,2) AND AI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND AI_PARTY_ID IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "')) AND AI_ITEM_ID='" & Parameter.ItemID & "' and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & "  and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID)) AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") order by AI_ADV_DATE "
                    If Not Parameter.Adv_Rec_ID Is Nothing Then 'added for multiuser check
                        Query = " SELECT 0 AS Sr,AI_ADV_DATE  AS REF_DATE  ,AI_ADV_AMT AS REF_AMT, 0 AS REF_ADDITION,0 as REF_ADJUSTED,0 as REF_REFUND,0 as REF_OUTSTAND,'' AS REF_ITEM, AI_ITEM_ID AS REF_ITEM_ID,AI_PURPOSE AS REF_PURPOSE ,AI_OTHER_DETAIL AS REF_OTHER_DETAIL ,REC_ID AS REF_ID FROM ADVANCES_INFO WHERE REC_STATUS IN (0,1,2) AND AI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND AI_PARTY_ID IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "')) AND AI_ITEM_ID='" & Parameter.ItemID & "' AND REC_ID = '" & Parameter.Adv_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = "SELECT REC_ID,AI_TR_ID,AI_TR_SR_NO FROM ADVANCES_INFO WHERE AI_TR_ID ='" & Parameter.TxnMID & "' AND REC_STATUS IN (0,1,2) and AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " "
                    If Parameter.TxnSrNo > 0 Then Query += " AND AI_TR_SR_NO = " & Parameter.TxnSrNo
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "SELECT ITEM_NAME AS Item, C_NAME AS Party, AI_ADV_AMT  as 'Org Value', AI_ADV_AMT as 'Curr Value', CONVERT(VARCHAR, AI_ADV_DATE , 107) AS Date, Ai.REC_ID,COALESCE(AI_PURPOSE,'') as Reason, COALESCE(AI_OTHER_DETAIL,'') as Detail FROM advances_info AS AI INNER JOIN item_info as ii on ai.AI_ITEM_ID = ii.REC_ID INNER JOIN address_book AS AB  ON AB.REC_ID = AI.AI_PARTY_ID AND C_CEN_ID =" & inBasicParam.openCenID.ToString & " WHERE AI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND  AI_ITEM_ID='" & Parameter.ItemID & "' AND AI.AI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "'))  and ai.REC_STATUS IN (0,1,2)  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & "  and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID)) AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Parameter.CreationTxnRecID Is Nothing Then
                        If Parameter.CreationTxnRecID.Length > 0 Then Query += " and (COALESCE(AI.AI_TR_ID,'') <> '" & Parameter.CreationTxnRecID & "')"
                    End If
                    If Not Parameter.Adv_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND Ai.REC_ID = '" & Parameter.Adv_Rec_ID & "'"
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.ADVANCES_INFO, Query, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets List by condition
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetList_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  REC_ID AS ID,AI_ITEM_ID,AI_PARTY_ID,AI_ADV_DATE,AI_ADV_AMT,AI_PURPOSE,AI_OTHER_DETAIL FROM Advances_Info " & _
                                    " Where   REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.ADVANCES_INFO, Query, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Payments
        ''' </summary>
        ''' <param name="Party_IDs"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetPayments</remarks>
        Public Shared Function GetPayments(ByVal inParam As Param_AdvGetPayments, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            Dim NextYear As String = ""
            If inParam.NextUnAuditedYearID.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & inParam.NextUnAuditedYearID.ToString & ""
            OnlineQuery = " SELECT TP.TR_REF_ID, SUM( CASE WHEN TP.TR_PAY_TYPE ='ADVANCE' THEN TP.TR_REF_AMT ELSE 0 END ) AS Adjusted, SUM( CASE WHEN TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT') THEN TP.TR_REF_AMT ELSE 0 END) AS  Refund  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = '" & inBasicParam.openCenID & "' AND TP.TR_PAY_TYPE IN ('REFUND','ADJUSTMENT','ADVANCE') AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN (" & inParam.Party_IDs & ")))  and (TM.TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Advance Listing for a specific party and Item for Receipt Voucher 
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetReceiptAdvances(ByVal inParam As Param_GetReceiptAdvances, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ADVANCE_RECID", "AI_PARTY_ID", "AI_ITEM_ID", "TR_M_ID"}
            'If inParam.Next_YearID = "" Then inParam.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If inParam.Prev_YearId = "" Then inParam.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Advance_RecID = "" Then inParam.Advance_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Advance_PartyID = "" Then inParam.Advance_PartyID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Advance_ItemID = "" Then inParam.Advance_ItemID = Nothing ' Nullable Parameters , passed NULL if Empty
            If inParam.Tr_M_Id = "" Then inParam.Tr_M_Id = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Prev_YearId, inParam.Next_YearID, inParam.Advance_RecID, inParam.Advance_PartyID, inParam.Advance_ItemID, inParam.Tr_M_Id}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String], DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 4, 4, 36, 36, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ADVANCES_INFO, "sp_get_Receipt_Advances", ConnectOneWS.Tables.ADVANCES_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Advance Listing for a specific party and Item for Payment Voucher 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetPaymentAdvances</remarks>
        Public Shared Function GetPaymentAdvances(Param As Param_GetPaymentAdvances, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_payment_Advances"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ADVANCE_RECID", "AI_PARTY_ID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Adv_RecId = "" Then Param.Adv_RecId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Adv_Party_ID = "" Then Param.Adv_Party_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Tr_M_Id = "" Then Param.Tr_M_Id = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Adv_RecId, Param.Adv_Party_ID, Param.Tr_M_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ADVANCES_INFO, SPName, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Advance Payment count
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_GetAdvancePaymentCount</remarks>
        Public Shared Function GetAdvancePaymentCount(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(TR_REF_ID) FROM Transaction_D_Payment_Info  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_REF_ID  = '" & RecID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="openYearID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Advances, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim RecID As String = Guid.NewGuid.ToString
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Advances_Info(AI_CEN_ID,AI_COD_YEAR_ID,AI_ITEM_ID,AI_PARTY_ID,AI_ADV_DATE,AI_ADV_AMT,AI_PURPOSE,AI_OTHER_DETAIL," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,AI_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  "'" & InParam.PartyID & "', " & _
                                                  " " & If(IsDate(InParam.AdvanceDate), "'" & Convert.ToDateTime(InParam.AdvanceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.Amount & " , " & _
                                                  "'" & InParam.Purpose & "', " & _
                                                  "'" & InParam.Remarks & "', " & _
                                              "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.ADVANCES_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' Called from sale of asset voucher - AddTime is added for txn scope
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="openYearID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_InsertTRID, uses Parameter_InsertTRID_Advances</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRID_Advances, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            If InParam1.RecID Is Nothing Then
                InParam1.RecID = Guid.NewGuid.ToString
            Else
                If InParam1.RecID.Length = 0 Then InParam1.RecID = Guid.NewGuid.ToString
            End If

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Advances_Info(AI_CEN_ID,AI_COD_YEAR_ID,AI_ITEM_ID,AI_PARTY_ID,AI_ADV_DATE,AI_ADV_AMT,AI_PURPOSE,AI_OTHER_DETAIL,AI_TR_ID,AI_TR_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,AI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.ItemID & "', " &
                                                  "'" & InParam1.PartyID & "', " &
                                                  " " & If(IsDate(InParam1.AdvanceDate), "'" & Convert.ToDateTime(InParam1.AdvanceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InParam1.Amount & " , " &
                                                  "'" & InParam1.Purpose & "', " &
                                                  "'" & InParam1.Remarks & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  "" & InParam1.TxnSrNo & ", " &
                                              "" & Str & "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.ADVANCES_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates Advances Info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Advances_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Advances, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Advances_Info SET " & _
                                         "AI_ITEM_ID         ='" & UpParam.ItemID & "', " & _
                                         "AI_PARTY_ID        ='" & UpParam.PartyID & "', " & _
                                         "AI_ADV_DATE        = " & If(IsDate(UpParam.AdvanceDate), "'" & Convert.ToDateTime(UpParam.AdvanceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "AI_ADV_AMT         =" & UpParam.Amount & " ,  " & _
                                         "AI_PURPOSE         ='" & UpParam.Purpose & "',  " & _
                                         "AI_OTHER_DETAIL    ='" & UpParam.Remarks & "',  " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.ADVANCES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace
