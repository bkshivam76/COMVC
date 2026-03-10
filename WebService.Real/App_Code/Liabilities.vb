Imports System.Data

Namespace Real
    <Serializable>
    Public Class Liabilities
#Region "Param Classes"
        <Serializable>
        Public Class Param_Liabilities_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Param_Liabilities_GetListCommon
            Public ItemID As String
            Public PartyID As String
            Public CreationTxnRecID As String
            Public Li_Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Liabilities
            Public ItemID As String
            Public PartyID As String
            Public LiabilityDate As String
            Public PaymentDate As String
            Public Amount As Double
            Public Purpose As String
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRID_Liabilities : Inherits Parameter_Insert_Liabilities
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdLiabID As String ' Contains old RecId of Liability that is being re-posted(updated)
            Public Screen As ConnectOneWS.ClientScreen
        End Class
        <Serializable>
        Public Class Parameter_Update_Liabilities
            Public ItemID As String
            Public PartyID As String
            Public LiabilityDate As String
            Public PaymentDate As String
            Public Amount As Double
            Public Purpose As String
            Public Remarks As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_GetLiabPaymentsForParties
            Public PartyID As String = ""
            Public YearID As Integer
            Public NextUnAuditedYearID As Integer
        End Class
        <Serializable>
        Public Class Param_GetLiabProfileListing
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public Liab_RecID As String = ""
            Public Acc_Party As Boolean? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetPaymentLiabilities
            Public LI_RecId As String = ""
            Public Prev_YearId As Integer = Nothing
            Public Next_YearID As Integer = Nothing
            Public LI_Party_ID As String = ""
            Public Tr_M_Id As String = ""
        End Class
#End Region

        ''' <summary>
        ''' Gets Liabilities List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Liabilities_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT '' AS ITEM_NAME, LI_ITEM_ID ,LI_PARTY_ID, '' AS PARTY_NAME,LI_DATE,LI_PAY_DATE,LI_AMT AS Amount,0 as Addition, 0 as Adjusted, 0 as Paid,0 AS 'Out-Standing',LI_PURPOSE AS Reason,LI_OTHER_DETAIL,CASE WHEN LI_TR_ID IS NULL OR LI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE LI_TR_ID END AS TR_ID,LI_COD_YEAR_ID AS YearID, REC_ID  AS ID  , CASE WHEN LI_TR_ID IS NULL OR LI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as Type ," & Common.Remarks_Detail("Liabilities_Info", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Liabilities_Info") & "" & _
                                 " FROM Liabilities_Info " & _
                                 " Where   REC_STATUS IN (0,1,2) AND LI_CEN_ID='" & inBasicParam.openCenID & "'  and LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID)) AND (LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            Return dbService.List(ConnectOneWS.Tables.LIABILITIES_INFO, onlineQuery, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Liabilities Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Param_GetLiabProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Liabilities_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "LIAB_RECID", "ACC_PARTY", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Liab_RecID = "" Then Param.Liab_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Liab_RecID, Param.Acc_Party, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 1, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LIABILITIES_INFO, SPName, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="Parameter"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Parameter As Object = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Dim inParam As Param_Liabilities_GetListCommon = CType(Parameter, Param_Liabilities_GetListCommon)
                    Query = " SELECT 0 AS Sr,LI_DATE as 'Date','' AS Item,'' as OFFSET_ID, LI_ITEM_ID,LI_AMT as Amount,0 as Addition, 0 as Adjusted,0 as Paid,0 AS 'Out-Standing',0 as Payment,LI_PURPOSE as Purpose,LI_OTHER_DETAIL as 'Other Details',REC_ID AS LI_ID,REC_EDIT_ON FROM Liabilities_Info WHERE  REC_STATUS IN (0,1,2) AND LI_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND LI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "')) and LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID)) AND (LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") order by LI_DATE " 'PartyID
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = "SELECT REC_ID FROM LIABILITIES_INFO WHERE LI_TR_ID ='" & CType(Parameter, String) & "' AND REC_STATUS IN (0,1,2)  and LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " " 'TxnMID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Dim inParam As Param_Liabilities_GetListCommon = CType(Parameter, Param_Liabilities_GetListCommon)
                    Query = "SELECT C_NAME AS Party, LI_AMT  as 'Org Value', LI_AMT  as 'Curr Value', CONVERT(VARCHAR, LI_DATE , 107) as Date, LI.REC_ID, CONVERT(VARCHAR, LI_PAY_DATE , 107) AS Due,COALESCE(LI_PURPOSE,'') as Reason, COALESCE(LI_OTHER_DETAIL,'') as Detail  FROM liabilities_info AS LI INNER JOIN address_book AS AB  ON AB.REC_ID = LI.LI_PARTY_ID AND c_CEN_ID =" & inBasicParam.openCenID.ToString & " WHERE LI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LI_ITEM_ID='" & inParam.ItemID & "' AND LI.LI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & Parameter.PartyID & "')) and LI.REC_STATUS IN (0,1,2)  and LI.LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI.LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID)) AND (LI.LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI.LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Parameter.CreationTxnRecID Is Nothing Then
                        If Parameter.CreationTxnRecID.Length > 0 Then Query += " and (COALESCE(LI.LI_TR_ID,'') <> '" & inParam.CreationTxnRecID & "')"
                    End If
                    If Not inParam.Li_Rec_ID Is Nothing Then 'Added for multiuser check
                        Query += " AND LI.REC_ID = '" & inParam.Li_Rec_ID & "'"
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.LIABILITIES_INFO, Query, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Liabilities_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  REC_ID AS ID,LI_ITEM_ID,LI_PARTY_ID,LI_DATE,LI_PAY_DATE,LI_AMT,LI_PURPOSE,LI_OTHER_DETAIL FROM Liabilities_Info " & _
                                        " Where   REC_STATUS IN (0,1,2) AND LI_CEN_ID=" & inBasicParam.openCenID.ToString & "  " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.LIABILITIES_INFO, Query, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Payments For Parties
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetPaymentsForParties</remarks>
        Public Shared Function GetPaymentsForParties(ByVal InParam As Param_GetLiabPaymentsForParties, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.YearID = "" Then InParam.YearID = inBasicParam.openYearID
            Dim NextYear As String = ""
            If InParam.NextUnAuditedYearID.ToString.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID = " & InParam.NextUnAuditedYearID.ToString & " "
            Dim Query As String = " SELECT TP.TR_REF_ID, SUM(TP.TR_REF_AMT) AS Paid FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE='LIABILITIES' AND TM.TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID IN ( SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID IN (" & InParam.PartyID & "))) and (TM.TR_COD_YEAR_ID =" & InParam.YearID.ToString & " " & NextYear & " ) GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Transaction Count
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetTransactionCount</remarks>
        Public Shared Function GetTransactionCount(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(TR_REF_ID) FROM Transaction_D_Payment_Info  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_REF_ID  = '" & RecID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Liabilities Listing for a specific party and Item for Payment Voucher 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_GetPaymentLiabilities</remarks>
        Public Shared Function GetPaymentLiabilities(Param As Param_GetPaymentLiabilities, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_payment_liabilities"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "LI_REC_ID", "LI_PARTY_ID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.LI_RecId = "" Then Param.LI_RecId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.LI_Party_ID = "" Then Param.LI_Party_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Tr_M_Id = "" Then Param.Tr_M_Id = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.LI_RecId, Param.LI_Party_ID, Param.Tr_M_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LIABILITIES_INFO, SPName, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function


        ''' <summary>
        ''' Inserts Liabilities Info
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Liabilities, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Liabilities_Info(LI_CEN_ID,LI_COD_YEAR_ID,LI_ITEM_ID,LI_PARTY_ID,LI_DATE,LI_PAY_DATE,LI_AMT,LI_PURPOSE,LI_OTHER_DETAIL," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,LI_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  "'" & InParam.PartyID & "', " & _
                                                  " " & If(IsDate(InParam.LiabilityDate), "'" & Convert.ToDateTime(InParam.LiabilityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & If(IsDate(InParam.PaymentDate), "'" & Convert.ToDateTime(InParam.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.Amount & " , " & _
                                                  "'" & InParam.Purpose & "', " & _
                                                  "'" & InParam.Remarks & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.LIABILITIES_INFO, OnlineQuery, inBasicParam, InParam.RecID)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_InsertTRID</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRID_Liabilities, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Liabilities_Info(LI_CEN_ID,LI_COD_YEAR_ID,LI_ITEM_ID,LI_PARTY_ID,LI_DATE,LI_PAY_DATE,LI_AMT,LI_PURPOSE,LI_OTHER_DETAIL,LI_TR_ID,LI_TR_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,LI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.ItemID & "', " &
                                                  "'" & InParam1.PartyID & "', " &
                                                  " " & If(IsDate(InParam1.LiabilityDate), "'" & Convert.ToDateTime(InParam1.LiabilityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & If(IsDate(InParam1.PaymentDate), "'" & Convert.ToDateTime(InParam1.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InParam1.Amount & " , " &
                                                  "'" & InParam1.Purpose & "', " &
                                                  "'" & InParam1.Remarks & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  "" & InParam1.TxnSrNo & ", " &
                                                  "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.LIABILITIES_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates Liabilities Info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Liabilities_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Liabilities, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Liabilities_Info SET " & _
                                         "LI_ITEM_ID         ='" & UpParam.ItemID & "', " & _
                                         "LI_PARTY_ID        ='" & UpParam.PartyID & "', " & _
                                         "LI_DATE            = " & If(IsDate(UpParam.LiabilityDate), "'" & Convert.ToDateTime(UpParam.LiabilityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                         "LI_PAY_DATE        = " & If(IsDate(UpParam.PaymentDate), "'" & Convert.ToDateTime(UpParam.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                         "LI_AMT             =" & UpParam.Amount & " ,  " & _
                                         "LI_PURPOSE         ='" & UpParam.Purpose & "',  " & _
                                         "LI_OTHER_DETAIL    ='" & UpParam.Remarks & "',  " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.LIABILITIES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace

