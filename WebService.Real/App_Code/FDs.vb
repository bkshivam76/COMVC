Imports System.Data
Imports Real.Vouchers

Namespace Real
#Region "Profile"
    <Serializable>
    Public Class FD
#Region "Param Classes"
        <Serializable>
        Public Class Param_FDs_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
            Public FD_ID As String
        End Class
        <Serializable>
        Public Class Param_UpdateBankAccountBalance
            Public Amount As Double
            Public BankAccountID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertandUpdateBalance_FD
            Public BankAccountID As String
            Public FDNo As String
            Public FDDate As String
            Public AsOnDate As String
            Public Amount As Double
            Public InterestRate As Double
            Public IntCondition As String
            Public MaturityDate As String
            Public MaturityAmount As Double
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            Public openYearID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateandUpdateBalance_FD
            Public BankAccountID As String
            Public FDNo As String
            Public FDDate As String
            Public AsOnDate As String
            Public Amount As Double
            Public InterestRate As Double
            Public IntCondition As String
            Public MaturityDate As String
            Public MaturityAmount As Double
            Public Remarks As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_GetFDProfileListing
            Public Year_Start_Date As DateTime
            Public Year_End_Date As DateTime
        End Class
#End Region

        Private Shared Function UpdateBankAccountBalance(ByVal Amount As Double, ByVal BankAccountID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim Param As DataFunctions.Param_UpdateOpeningBalance = New DataFunctions.Param_UpdateOpeningBalance()
            Param.Amount = Amount
            Param.RecID = BankAccountID
            'Param.Status_Action = Common_Lib.Common.Record_Status._Completed
            Return DataFunctions.UpdateOpeningBalance(Param, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Expense Income Count
        ''' </summary>
        ''' <param name="FDID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetExpense_IncomeCount</remarks>
        Public Shared Function GetExpense_IncomeCount(ByVal FDID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(TR_TRF_CROSS_REF_ID) FROM TRANSACTION_INFO WHERE REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID ='" & FDID & "' AND TR_ITEM_ID IN ('d0219173-45ff-4284-ae0e-89ba0e8d76b4','c92da5ab-082d-45d9-b6b7-78752625c715','802f285d-2773-4f80-80c9-0278bafb3ec4','53adb9d4-d991-4d51-9d55-fbeafe1b4c57','290063bc-a1a1-43af-bedb-f51b7a30c4f4','df575c74-40f4-417a-8295-de3488db4305') AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get TDS
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetTDS</remarks>
        Public Shared Function GetTDS(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_TRF_CROSS_REF_ID as FD_ID,  COALESCE(SUM(tr_amount),0.0) as TDS FROM transaction_info WHERE TR_ITEM_ID = 'd0219173-45ff-4284-ae0e-89ba0e8d76b4'  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) and TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  GROUP BY TR_TRF_CROSS_REF_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets TDS Reversals
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetTDSReversals</remarks>
        Public Shared Function GetTDSReversals(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_TRF_CROSS_REF_ID as FD_ID,  COALESCE(SUM(tr_amount),0.0) as TDS_Reverse FROM transaction_info WHERE TR_ITEM_ID = 'df575c74-40f4-417a-8295-de3488db4305'  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) and TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") GROUP BY TR_TRF_CROSS_REF_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Interest
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetInterest</remarks>
        Public Shared Function GetInterest(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_TRF_CROSS_REF_ID as FD_ID,  COALESCE(SUM(tr_amount),0.0) as IntRecd FROM transaction_info WHERE TR_ITEM_ID IN ('c92da5ab-082d-45d9-b6b7-78752625c715','53adb9d4-d991-4d51-9d55-fbeafe1b4c57','802f285d-2773-4f80-80c9-0278bafb3ec4','1ed5cbe4-c8aa-4583-af44-eba3db08e117')  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) and TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") GROUP BY TR_TRF_CROSS_REF_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Interest Overheads
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetInterestOverheads</remarks>
        Public Shared Function GetInterestOverheads(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_TRF_CROSS_REF_ID as FD_ID,  COALESCE(SUM(tr_amount),0.0) as IntOverHead FROM transaction_info WHERE TR_ITEM_ID IN ('290063bc-a1a1-43af-bedb-f51b7a30c4f4')  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) and TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  GROUP BY TR_TRF_CROSS_REF_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List By Condition
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT FD_NO ,FD_DATE ,FD_AS_DATE, FD_AMT , FD_INT_RATE ,FD_INT_PAY_COND, FD_MAT_DATE , FD_MAT_AMT , FD_BA_ID ,REC_ID AS ID,CASE WHEN FD_TR_ID IS NULL  OR FD_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE FD_TR_ID END AS FD_TR_ID,FD_CLOSE.TR_DATE FD_CLOSE_DATE,CASE WHEN FD_CLOSE.TR_DATE IS NULL THEN 'New' ELSE CASE WHEN FD_CLOSE.TR_DATE >= FD_INFO.FD_MAT_DATE THEN CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Matured Renewed' ELSE 'Matured Closed' END ELSE CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Pre-Matured Renewed' ELSE 'Pre-Matured Closed' END END END AS FD_STATUS, CASE WHEN FD_TR_ID IS NULL OR FD_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN 'Profile Entry' ELSE 'Voucher Entry' END as Type, 0.0 AS InterestRecd, 0.0 as TDSPaid, 0.0 as NettInterest, " & Common.Rec_Detail("FD_INFO") & " " & _
                                        " FROM FD_INFO LEFT OUTER JOIN (SELECT distinct TR_TRF_CROSS_REF_ID AS FDID, TR_DATE, TR_M_ID FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " & _
                                        " AND TR_TRF_CROSS_REF_ID IN (SELECT REC_ID FROM fd_info WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " ) ) AS FD_CLOSE  ON FD_CLOSE.FDID = FD_INFO.REC_ID " & _
                                        " LEFT OUTER JOIN (SELECT FD_RENEWFROM_ID, COUNT(FD_RENEWFROM_ID) AS CNT FROM fd_info as fd WHERE REC_STATUS IN (0,1,2)  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " GROUP BY FD_RENEWFROM_ID) AS RENEWALS ON FD_INFO.REC_ID = RENEWALS.FD_RENEWFROM_ID " & _
                                        " WHERE  REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.FD_INFO, onlineQuery, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_FDs_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = ""
            onlineQuery = " SELECT FD_CEN_ID, FD_NO ,FD_DATE ,FD_AS_DATE, FD_AMT , FD_INT_RATE ,FD_INT_PAY_COND, FD_MAT_DATE , FD_MAT_AMT , FD_BA_ID ,REC_ID AS ID, CASE WHEN FD_TR_ID IS NULL  OR FD_COD_YEAR_ID <> '" & inBasicParam.openYearID & "' THEN '' ELSE FD_TR_ID END AS FD_TR_ID,FD_CLOSE.TR_DATE as FD_CLOSE_DATE, CASE WHEN FD_CLOSE.TR_DATE IS NULL THEN 'New' ELSE CASE WHEN FD_CLOSE.TR_DATE >= FI.FD_MAT_DATE THEN CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Matured Renewed' ELSE 'Matured Closed' END ELSE CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Pre-Matured Renewed' ELSE 'Pre-Matured Closed' END END END AS FD_STATUS, CASE WHEN FD_TR_ID IS NULL OR FD_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as Type, FD_COD_YEAR_ID AS YearID, 0.0 AS InterestRecd, 0.0 as TDSPaid, 0.0 as NettInterest, " & Common.Remarks_Detail("FI", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("FI") & " " & _
                                        "FROM FD_INFO as FI LEFT OUTER JOIN " & _
                            " (SELECT distinct TR_TRF_CROSS_REF_ID AS FDID, TR_DATE, TR_M_ID FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " & _
                            " AND TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " AND ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AND TR_TRF_CROSS_REF_ID IN (SELECT REC_ID FROM fd_info WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " ) ) AS FD_CLOSE  ON FD_CLOSE.FDID = FI.REC_ID " & _
                            " LEFT OUTER JOIN (SELECT FD_RENEWFROM_ID, COUNT(FD_RENEWFROM_ID) AS CNT FROM fd_info WHERE REC_STATUS IN (0,1,2)  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " GROUP BY FD_RENEWFROM_ID) AS RENEWALS ON FI.REC_ID = RENEWALS.FD_RENEWFROM_ID " & _
                            " WHERE  REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & "  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            If Not Param.FD_ID Is Nothing Then
                onlineQuery += " AND REC_ID = '" & Param.FD_ID & "'"
            End If
            Return dbService.List(ConnectOneWS.Tables.FD_INFO, onlineQuery, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns FD Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Param_GetFDProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_FD_Profile"
            Dim params() As String = {"CENID", "YEARID", "X_YEAR_FROM_DATE", "X_YEAR_TO_DATE", "UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Year_Start_Date, Param.Year_End_Date, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 20, 20, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.FD_INFO, SPName, ConnectOneWS.Tables.FD_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets RecordID By AccountID
        ''' </summary>
        ''' <param name="AccID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_GetRecIDByAccID</remarks>
        Public Shared Function GetRecIDByAccID(ByVal AccID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT Rec_id FROM FD_INFO WHERE FD_BA_ID ='" & AccID & "' AND REC_STATUS IN (0,1,2) "
            Return dbService.List(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetFDSum(ByVal AccountID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            'Dim _BA As BankAccounts = New BankAccounts()
            Return BankAccounts.GetFDSum(AccountID, inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts And Updates Balance
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_Insert_and_Update_Balance</remarks>
        Public Shared Function Insert_and_Update_Balance(ByVal InParam As Parameter_InsertandUpdateBalance_FD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Result As Boolean = False
            Dim OnlineQuery As String = "INSERT INTO FD_INFO(FD_CEN_ID, FD_COD_YEAR_ID,FD_BA_ID,FD_NO, FD_DATE,  FD_AS_DATE, FD_AMT, FD_INT_RATE,FD_INT_PAY_COND, FD_MAT_DATE, FD_MAT_AMT,FD_OTHER_DETAIL, " & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,FD_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & InParam.openYearID.ToString & "," & _
                                                  "'" & InParam.BankAccountID & "', " & _
                                                  "'" & InParam.FDNo & "', " & _
                                                  " " & If(IsDate(InParam.FDDate), "'" & Convert.ToDateTime(InParam.FDDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & If(IsDate(InParam.AsOnDate), "'" & Convert.ToDateTime(InParam.AsOnDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.Amount & " , " & _
                                                  " " & InParam.InterestRate & " , " & _
                                                  "'" & InParam.IntCondition & "', " & _
                                                  " " & If(IsDate(InParam.MaturityDate), "'" & Convert.ToDateTime(InParam.MaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.MaturityAmount & ", " & _
                                                  "'" & InParam.Remarks & "', " & _
                                              "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.FD_INFO, OnlineQuery, inBasicParam, InParam.RecID)
            Result = True
            If Result Then
                Dim MaxValue As Object = 0
                MaxValue = GetFDSum(InParam.BankAccountID, inBasicParam)
                If IsDBNull(MaxValue) Then MaxValue = 0
                Result = UpdateBankAccountBalance(MaxValue, InParam.BankAccountID, inBasicParam)
            End If

            Return Result
        End Function

        ''' <summary>
        ''' Updates FD and Updates FD Bank Balance
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_Update_and_Update_Balance</remarks>
        Public Shared Function Update_and_Update_Balance(ByVal UpParam As Parameter_UpdateandUpdateBalance_FD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Result As Boolean = False
            Dim OnlineQuery As String = " UPDATE FD_INFO SET " & _
                                        "FD_BA_ID        ='" & UpParam.BankAccountID & "' ," & _
                                        "FD_NO           ='" & UpParam.FDNo & "', " & _
                                        "FD_DATE         =" & If(IsDate(UpParam.FDDate), "'" & Convert.ToDateTime(UpParam.FDDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        "FD_AS_DATE      =" & If(IsDate(UpParam.AsOnDate), "'" & Convert.ToDateTime(UpParam.AsOnDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "FD_AMT          =" & UpParam.Amount & ", " & _
                                        "FD_INT_RATE     =" & UpParam.InterestRate & ", " & _
                                        "FD_INT_PAY_COND ='" & UpParam.IntCondition & "', " & _
                                        "FD_MAT_DATE     =" & If(IsDate(UpParam.MaturityDate), "'" & Convert.ToDateTime(UpParam.MaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "FD_MAT_AMT      =" & UpParam.MaturityAmount & ", " & _
                                        "FD_OTHER_DETAIL ='" & UpParam.Remarks & "', " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'" & _
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.FD_INFO, OnlineQuery, inBasicParam)
            Result = True
            If Result Then
                Dim MaxValue As Object = 0
                MaxValue = GetFDSum(UpParam.BankAccountID, inBasicParam)
                If IsDBNull(MaxValue) Then MaxValue = 0
                Result = UpdateBankAccountBalance(MaxValue, UpParam.BankAccountID, inBasicParam)
            End If
            Return Result
        End Function
    End Class
#End Region

#Region "--Accounts--"
    <Serializable>
    Public Class Voucher_FD
#Region "Param Classes"
        <Serializable>
        Public Class Param_VoucherFD_GetInterestRecords
            Public Master_Rec_ID As String
            Public ItemID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetTDS
            Public Master_Rec_ID As String
            Public FDID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetTDSReversal
            Public Master_Rec_ID As String
            Public FDID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetCount
            Public Master_Rec_ID As String
            Public FDID As String
            Public QCase As Int32
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetPrevFDStatus
            Public Master_Rec_ID As String
            Public FDID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetItemCountInSameMaster
            Public TxnMID As String
            Public itemID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetAccountNoCount
            Public Rec_Id As String
            Public FDNo As String
            Public BankAccID As String
        End Class
        <Serializable>
        Public Class Param_VoucherFD_GetFDs
            Public IncludeClosedFDs As Boolean
            Public IncludePrevYearFDs As Boolean = False
            Public FD_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertFD_VoucherFD
            Public BankAccID As String
            Public FDNo As String
            Public FDDate As String
            Public FDAsDate As String
            Public FDAmount As Double
            Public FDIntRate As Double
            Public PaymentCondition As String
            Public FDMaturityDate As String
            Public FDMaturityAmount As Double
            Public Remarks As String
            Public TxnID As String
            Public RenewFrom_ID As String
            'Public FDStatus As Common_Lib.Common.FDStatus
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertFDHistory_VoucherFD
            Public FDID As String
            Public FDAction As String
            Public FDStatus As String
            Public TxnID As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherFD
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public TDS As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherFD
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
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public FDID As String
            Public MasterTxnID As String
            Public Status_Action As String
            Public RecID As String
            Public PurposeID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateFD_VoucherFD
            Public BankAccID As String
            Public FDNo As String
            Public FDDate As String
            Public FDAsDate As String
            Public FDAmount As Double
            Public FDIntRate As Double
            Public PaymentCondition As String
            Public FDMaturityDate As String
            Public FDMaturityAmount As Double
            Public Remarks As String
            Public TxnID As String
            Public RenewFrom_ID As String
            Public PurposeID As String
            'Public FDStatus As String
            'Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateFDHistory_VoucherFD
            Public FDAction As String
            Public FDStatus As String
            Public TxnId As String
            Public FDID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateMasterInfo_VoucherFD
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public TDS As Double
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Param_Txn_NewFD_InsertVoucherFD
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherFD
            Public param_InsertFD As Parameter_InsertFD_VoucherFD = Nothing
            Public param_InsertFDHistory As Parameter_InsertFDHistory_VoucherFD = Nothing
            Public param_Insert As Parameter_Insert_VoucherFD = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_NewFD_UpdateVoucherFD
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherFD
            Public param_UpdateFD As Parameter_UpdateFD_VoucherFD = Nothing
            Public param_UpdateFDHistory As Parameter_UpdateFDHistory_VoucherFD = Nothing
            Public param_DeleteVoucher_Txn_MID As String = Nothing
            Public param_Insert As Parameter_Insert_VoucherFD = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_NewFD_DeleteVoucherFD
            Public MID_DeleteFDHistory As String = Nothing
            Public MID_DeleteFD As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_RenewFD_InsertVoucherFD
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherFD
            Public param_InsertFD As Parameter_InsertFD_VoucherFD = Nothing
            Public param_InFDHistory As Parameter_InsertFDHistory_VoucherFD = Nothing
            Public param_InRenewFDHistory As Parameter_InsertFDHistory_VoucherFD = Nothing
            ' Public param_CloseoldFD As Parameter_CloseFD_VoucherFD = Nothing 'Removed from Client Side
            Public param_InsertNewFD As Parameter_Insert_VoucherFD = Nothing
            Public param_InCloseFDJournal As Parameter_Insert_VoucherFD = Nothing
            Public param_InAdjExcessAmtRec As Parameter_Insert_VoucherFD = Nothing
            Public param_InBalRec_Notcash As Parameter_Insert_VoucherFD = Nothing
            Public param_InBalRec_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InJournalInterest As Parameter_Insert_VoucherFD = Nothing
            Public param_InCreditedInterest_Notcash As Parameter_Insert_VoucherFD = Nothing
            Public param_InCreditedInterest_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InPmt_NotCash As Parameter_Insert_VoucherFD = Nothing
            Public param_InPmt_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InInterestOverhead As Parameter_Insert_VoucherFD = Nothing
            Public param_InTDS_Deducted1 As Parameter_Insert_VoucherFD = Nothing
            Public param_InTDS_Deducted2 As Parameter_Insert_VoucherFD = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_RenewFD_UpdateVoucherFD
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherFD
            Public param_UpdateFD As Parameter_UpdateFD_VoucherFD = Nothing
            Public param_UpFDHistory As Parameter_UpdateFDHistory_VoucherFD = Nothing
            Public param_UpRenFDHistory As Parameter_UpdateFDHistory_VoucherFD = Nothing
            'Public param_CloseFD As Parameter_CloseFD_VoucherFD = Nothing 'Removed from Client Side
            Public MID_DeleteTxns As String = Nothing
            Public param_InsertNewFD As Parameter_Insert_VoucherFD = Nothing
            Public param_InCloseFDJournal As Parameter_Insert_VoucherFD = Nothing
            Public param_InAdjExcessAmtRec As Parameter_Insert_VoucherFD = Nothing
            Public param_InBalRec_Notcash As Parameter_Insert_VoucherFD = Nothing
            Public param_InBalRec_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InJournalInterest As Parameter_Insert_VoucherFD = Nothing
            Public param_InCreditedInterest_Notcash As Parameter_Insert_VoucherFD = Nothing
            Public param_InCreditedInterest_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InPmt_NotCash As Parameter_Insert_VoucherFD = Nothing
            Public param_InPmt_Cash As Parameter_Insert_VoucherFD = Nothing
            Public param_InInterestOverhead As Parameter_Insert_VoucherFD = Nothing
            Public param_InTDS_Deducted1 As Parameter_Insert_VoucherFD = Nothing
            Public param_InTDS_Deducted2 As Parameter_Insert_VoucherFD = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_RenewFD_DeleteVoucherFD
            Public MID_DeleteFDHistory As String = Nothing
            Public MID_DeleteFD As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_CloseFD_InsertVoucherFD
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherFD
            Public param_Insert As Parameter_Insert_VoucherFD = Nothing
            Public param_InterestOverhead As Parameter_Insert_VoucherFD = Nothing
            Public param_InterestOverhead_BankCharges As Parameter_Insert_VoucherFD = Nothing
            'Public param_CloseFD As Parameter_CloseFD_VoucherFD = Nothing 'Removed from Client Side
            Public param_InsertFDHistory As Parameter_InsertFDHistory_VoucherFD = Nothing
            Public param_InFDCloseInterest As Parameter_Insert_VoucherFD = Nothing
            Public param_TDSDeducted1 As Parameter_Insert_VoucherFD = Nothing
            Public param_TDSDeducted2 As Parameter_Insert_VoucherFD = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_CloseFD_UpdateVoucherFD
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherFD
            Public MID_DeleteTxns As String = Nothing
            Public param_InsertCloseAmt As Parameter_Insert_VoucherFD = Nothing
            Public param_InterestOverhead As Parameter_Insert_VoucherFD = Nothing
            Public param_InterestOverhead_BankCharges As Parameter_Insert_VoucherFD = Nothing
            'Public param_CloseFD As Parameter_CloseFD_VoucherFD = Nothing 'Removed from Client Side
            Public param_InsertFDHistory As Parameter_InsertFDHistory_VoucherFD = Nothing
            Public param_InFDCloseInterest As Parameter_Insert_VoucherFD = Nothing
            Public param_TDSDeducted1 As Parameter_Insert_VoucherFD = Nothing
            Public param_TDSDeducted2 As Parameter_Insert_VoucherFD = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_CloseFD_DeleteVoucherFD
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
            Public MID_DeleteFDHistory As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_IncomeExpenses_InsertVoucherFD
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherFD
            Public param_TDSbyBank As Parameter_Insert_VoucherFD = Nothing
            Public param_InsertIntRec As Parameter_Insert_VoucherFD = Nothing
            Public param_Insert As Parameter_Insert_VoucherFD = Nothing
            Public PurposeID As String = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_IncomeExpenses_UpdateVoucherFD
            Public param_UpdateMaster As Parameter_UpdateMasterInfo_VoucherFD
            Public MID_DeleteTxns As String = Nothing
            Public param_TDSbyBank As Parameter_Insert_VoucherFD = Nothing
            Public param_InsertIntRec As Parameter_Insert_VoucherFD = Nothing
            Public param_Insert As Parameter_Insert_VoucherFD = Nothing
            Public PurposeID As String = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_IncomeExpenses_DeleteVoucherFD
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class
#End Region
        ''' <summary>
        ''' Gets Payment Records
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetPaymentRecords</remarks>
        Public Shared Function GetPaymentRecords(ByVal Master_Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "select TR_MODE,  TR_REF_BANK_ID, TR_REF_NO,  TR_REF_DATE,  TR_REF_CDATE  from TRANSACTION_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Master_Rec_ID & "' and LEN(TR_REF_BANK_ID) > 0"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Interest Records
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetInterestRecords</remarks>
        Public Shared Function GetInterestRecords(ByVal Param As Param_VoucherFD_GetInterestRecords, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select TR_AMOUNT from TRANSACTION_INFO where  REC_STATUS IN (0,1,2) AND TR_M_ID= '" & Param.Master_Rec_ID & "' and TR_ITEM_ID='" & Param.ItemID & "' AND TR_TYPE = 'CREDIT'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get TDS
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetTDS</remarks>
        Public Shared Function GetTDS(ByVal Param As Param_VoucherFD_GetTDS, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COALESCE(SUM(tr_amount),0) FROM transaction_info WHERE TR_ITEM_ID = 'd0219173-45ff-4284-ae0e-89ba0e8d76b4'  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID = '" & Param.FDID & "' and TR_M_ID <> '" & Param.Master_Rec_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetTDSReversal</remarks>
        Public Shared Function GetTDSReversal(ByVal Param As Param_VoucherFD_GetTDSReversal, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COALESCE(SUM(tr_amount),0) FROM transaction_info WHERE TR_ITEM_ID = 'df575c74-40f4-417a-8295-de3488db4305'  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID = '" & Param.FDID & "' and TR_M_ID <> '" & Param.Master_Rec_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets FD Close Date
        ''' </summary>
        ''' <param name="Master_Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDCloseDate</remarks>
        Public Shared Function GetFDCloseDate(ByVal Master_Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_DATE as FD_CLOSE_DATE FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID IN ('65730a27-e365-4195-853e-2f59225fe8f4','F565AD31-82D4-45BF-A3DC-F7AD5F297C06') " & _
                                  " AND (TR_TRF_CROSS_REF_ID IN ( SELECT REC_ID FROM FD_INFO WHERE FD_TR_ID ='" & Master_Rec_ID & "') OR TR_REF_OTHERS IN ( SELECT REC_ID FROM FD_INFO WHERE FD_TR_ID ='" & Master_Rec_ID & "') )"
            '"SELECT FD_CLOSE_DATE FROM FD_INFO WHERE FD_TR_ID ='" & Master_Rec_ID & "' AND REC_STATUS IN (0,1,2)"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Close Date of FD by FD Rec ID
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDCloseDateByFdID</remarks>
        Public Shared Function GetFDCloseDateByFdID(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_DATE as FD_CLOSE_DATE FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " & _
                                  " AND TR_TRF_CROSS_REF_ID ='" & Rec_ID & "' "
            '"SELECT FD_CLOSE_DATE FROM FD_INFO WHERE REC_ID ='" & Rec_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets New FD ID From Closed
        ''' </summary>
        ''' <param name="FD_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetNewFDIdFromClosed</remarks>
        Public Shared Function GetNewFDIdFromClosed(ByVal FD_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ID FROM FD_INFO WHERE FD_RENEWFROM_ID ='" & FD_ID & "' AND REC_STATUS IN (0,1,2)"
            Return dbService.GetScalar(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetCount</remarks>
        Public Shared Function GetCount(ByVal Param As Param_VoucherFD_GetCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case Param.QCase
                Case 1
                    Query = "SELECT COUNT(*) FROM TRANSACTION_INFO WHERE TR_M_ID <>'" & Param.Master_Rec_ID & "' AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID ='" & Param.FDID & "' AND TR_ITEM_ID IN ('d0219173-45ff-4284-ae0e-89ba0e8d76b4','c92da5ab-082d-45d9-b6b7-78752625c715','802f285d-2773-4f80-80c9-0278bafb3ec4','53adb9d4-d991-4d51-9d55-fbeafe1b4c57','290063bc-a1a1-43af-bedb-f51b7a30c4f4','df575c74-40f4-417a-8295-de3488db4305')"
                Case 2
                    Query = "SELECT COUNT(*) FROM TRANSACTION_INFO WHERE TR_M_ID ='" & Param.Master_Rec_ID & "' AND TR_ITEM_ID='4eb60d78-ce90-4a9f-891b-7a82d79dc84b'  AND REC_STATUS IN (0,1,2)"
                Case 3
                    Query = "SELECT COUNT(*) FROM TRANSACTION_INFO WHERE TR_M_ID ='" & Param.Master_Rec_ID & "' AND TR_ITEM_ID='65730a27-e365-4195-853e-2f59225fe8f4'" ' fdclose principle
            End Select
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetFDRecord(ByVal Filter As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select FI.*, FD_CLOSE.TR_DATE AS FD_CLOSE_DATE,  CASE WHEN FD_CLOSE.TR_DATE IS NULL THEN 'New' ELSE CASE WHEN FD_CLOSE.TR_DATE >= FI.FD_MAT_DATE THEN CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Matured Renewed' ELSE 'Matured Closed' END ELSE CASE WHEN COALESCE(RENEWALS.CNT,0) > 0 THEN 'Pre-Matured Renewed' ELSE 'Pre-Matured Closed' END END END AS FD_STATUS from fd_info as FI LEFT OUTER JOIN " & _
                            " (SELECT distinct TR_TRF_CROSS_REF_ID AS FDID, TR_DATE, TR_M_ID FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " & _
                            " AND TR_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " AND ( TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TR_CEN_ID)) AND (TR_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = TR_CEN_ID) OR TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AND TR_TRF_CROSS_REF_ID IN (SELECT REC_ID FROM fd_info WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " ) ) AS FD_CLOSE  ON FD_CLOSE.FDID = FI.REC_ID " & _
                            " LEFT OUTER JOIN (SELECT FD_RENEWFROM_ID, COUNT(FD_RENEWFROM_ID) AS CNT FROM fd_info WHERE REC_STATUS IN (0,1,2)  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " GROUP BY FD_RENEWFROM_ID) AS RENEWALS ON FI.REC_ID = RENEWALS.FD_RENEWFROM_ID " & _
                            " WHERE " & Filter
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Prev FD Status
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetPrevFDStatus</remarks>
        Public Shared Function GetPrevFDStatus(ByVal Param As Param_VoucherFD_GetPrevFDStatus, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT TOP 1 FDH_STATUS FROM FD_HISTORY_INFO  WHERE REC_STATUS IN (0,1,2) AND FDH_TR_ID <> '" & Param.Master_Rec_ID & "' and FDH_FD_ID = '" & Param.FDID & "' ORDER BY REC_ADD_ON DESC"
            Return dbService.GetScalar(ConnectOneWS.Tables.FD_HISTORY_INFO, OnlineQuery, ConnectOneWS.Tables.FD_HISTORY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get FDs
        ''' </summary>
        ''' <param name="IncludeClosedFDs"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetFDs</remarks>
        Public Shared Function GetFDs(ByVal Param As Param_VoucherFD_GetFDs, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_FDList_Voucher"
            Dim params() As String = {"IncludePrevYearFDs", "IncludeClosedFDs", "YearID", "CenID", "FD_ID"}
            Dim values() As Object = {Param.IncludePrevYearFDs, Param.IncludeClosedFDs, inBasicParam.openYearID, inBasicParam.openCenID, Param.FD_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {1, 1, 4, 5, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.FD_INFO, SPName, "FD_INFO", params, values, dbTypes, lengths, inBasicParam)

            'Dim Query As String = " SELECT FD_NO, FD_AMT,FD_BA_ID,FD_MAT_AMT, FD_MAT_DATE, FD_AS_DATE, FD_INT_RATE,FD_DATE,CASE WHEN FD_CLOSE.TR_DATE IS NULL THEN 'RUNNING' ELSE 'CLOSED' END AS FD_STATUS,REC_ID,REC_EDIT_ON FROM fd_info FI  LEFT OUTER JOIN " & _
            '                      " (SELECT distinct TR_TRF_CROSS_REF_ID AS FDID, TR_DATE, TR_M_ID FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " & _
            '                      " AND TR_TRF_CROSS_REF_ID IN (SELECT REC_ID FROM fd_info WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID='" & inBasicParam.openCenID & "' ) ) AS FD_CLOSE  ON FD_CLOSE.FDID = FI.REC_ID " & _
            '                      " Where   REC_STATUS IN (0,1,2) AND FD_CEN_ID='" & inBasicParam.openCenID & "' "
            'If Not Param.IncludePrevYearFDs Then Query += " and FD_COD_YEAR_ID <='" & inBasicParam.openYearID & "' and ( FD_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = '" & inBasicParam.openYearID & "')"
            'If Not Param.FD_ID Is Nothing Then Query += " And REC_ID = '" & Param.FD_ID & "' "
            'If Not Param.IncludeClosedFDs Then Query += " AND FD_CLOSE.TR_DATE IS NULL ORDER BY FD_NO , FD_AS_DATE "
            'Return dbService.List(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' GetItemCountInSameMaster
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetItemCountInSameMaster</remarks>
        Public Shared Function GetItemCountInSameMaster(ByVal Param As Param_VoucherFD_GetItemCountInSameMaster, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT count(rec_id) FROM TRANSACTION_INFO WHERE TR_M_ID ='" & Param.TxnMID & "' AND TR_ITEM_ID='" & Param.itemID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Account No. count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_GetAccountNoCount</remarks>
        Public Shared Function GetAccountNoCount(ByVal Param As Param_VoucherFD_GetAccountNoCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) FROM FD_Info WHERE FD_NO ='" & Param.FDNo & "' AND  REC_STATUS IN (0,1,2)  AND REC_ID <> '" & Param.Rec_Id & "' AND FD_BA_ID ='" & Param.BankAccID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert FD
        ''' </summary>
        ''' <param name="InFDParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertFD</remarks>
        Public Shared Function InsertFD(ByVal InFDParam As Parameter_InsertFD_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO FD_INFO(FD_CEN_ID, FD_COD_YEAR_ID,FD_BA_ID,FD_NO, FD_DATE,  FD_AS_DATE, FD_AMT, FD_INT_RATE,FD_INT_PAY_COND, FD_MAT_DATE, FD_MAT_AMT,FD_OTHER_DETAIL,FD_TR_ID, FD_RENEWFROM_ID ," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,FD_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InFDParam.BankAccID & "', " & _
                                                  "'" & InFDParam.FDNo & "', " & _
                                                  " " & If(IsDate(InFDParam.FDDate), "'" & Convert.ToDateTime(InFDParam.FDDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & If(IsDate(InFDParam.FDAsDate), "'" & Convert.ToDateTime(InFDParam.FDAsDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InFDParam.FDAmount & " , " & _
                                                  " " & InFDParam.FDIntRate & " , " & _
                                                  "'" & InFDParam.PaymentCondition & "', " & _
                                                  " " & If(IsDate(InFDParam.FDMaturityDate), "'" & Convert.ToDateTime(InFDParam.FDMaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InFDParam.FDMaturityAmount & ", " & _
                                                  "'" & InFDParam.Remarks & "', " & _
                                                  "'" & InFDParam.TxnID & "', " & _
                                                  " " & InFDParam.RenewFrom_ID & " , " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InFDParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InFDParam.RecID & "', '" & InFDParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.FD_INFO, OnlineQuery, inBasicParam, InFDParam.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert FD History
        ''' </summary>
        ''' <param name="InFDHty"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertFDHistory</remarks>
        Public Shared Function InsertFDHistory(ByVal InFDHty As Parameter_InsertFDHistory_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InFDHty.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO FD_HISTORY_INFO(FDH_FD_ID , FDH_ACTION, FDH_STATUS , FDH_TR_ID, " & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "'" & InFDHty.FDID & "'," & _
                                                  "'" & InFDHty.FDAction & "'," & _
                                                  "'" & InFDHty.FDStatus & "'," & _
                                                  "'" & InFDHty.TxnID & "', " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InFDHty.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.FD_HISTORY_INFO, OnlineQuery, inBasicParam, InFDHty.RecID, Nothing, AddTime)
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
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_TDS_AMT," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InMInfo.TxnCode & "'," & _
                                                  "'" & InMInfo.VNo & "', " & _
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InMInfo.SubTotal & "," & _
                                                  " " & InMInfo.Cash & "," & _
                                                  " " & InMInfo.Bank & "," & _
                                                  " " & InMInfo.TDS & "," & _
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
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = "NULL" Else If Not InParam.SUB_Cr_Led_ID.StartsWith("'") Then InParam.SUB_Cr_Led_ID = "'" & InParam.SUB_Cr_Led_ID & "'"
            If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = "NULL" Else If Not InParam.SUB_Dr_Led_ID.StartsWith("'") Then InParam.SUB_Dr_Led_ID = "'" & InParam.SUB_Dr_Led_ID & "'"
            If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = "NULL" Else If Not InParam.Ref_BANK_ID.StartsWith("'") Then InParam.Ref_BANK_ID = "'" & InParam.Ref_BANK_ID & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.FDID.Length = 0 Then InParam.FDID = "NULL" Else If Not InParam.FDID.StartsWith("'") Then InParam.FDID = "'" & InParam.FDID & "'"
            If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID," & _
                                        "TR_MODE,TR_REF_BANK_ID, TR_REF_BRANCH, TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_TRF_CROSS_REF_ID,TR_M_ID," & _
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
                                                            " " & InParam.Amount & ", " & _
                                                            "'" & InParam.Narration & "', " & _
                                                            "'" & InParam.Remarks & "', " & _
                                                            "'" & InParam.Reference & "', " & _
                                                            " " & InParam.FDID & " , " & _
                                                            " " & InParam.MasterTxnID & " , " & _
                                                          "" & Str & "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function
        Public Shared Function InsertPurpose(ByVal TxnMID As String, PurposeID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1, '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Dim ID As String = Guid.NewGuid.ToString()
            If Not TxnMID.StartsWith("'") Then TxnMID = "'" & TxnMID & "'"
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO, " &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "" & TxnMID & "," &
                                                  "'" & PurposeID & "', " &
                                                  " " & Amount.ToString() & ", " &
                                                  " NULL, " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & ID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, ID)
            Return True
        End Function
        ''' <summary>
        ''' UpdateFD
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateFD</remarks>
        Public Shared Function UpdateFD(ByVal UpParam As Parameter_UpdateFD_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.RenewFrom_ID.Trim().Length = 0 Then UpParam.RenewFrom_ID = "NULL" Else If Not UpParam.RenewFrom_ID.StartsWith("'") Then UpParam.RenewFrom_ID = "'" & UpParam.RenewFrom_ID & "'"
            Dim OnlineQuery As String = " UPDATE FD_INFO SET " & _
                                                "FD_BA_ID        ='" & UpParam.BankAccID & "' ," & _
                                                "FD_NO           ='" & UpParam.FDNo & "', " & _
                                                "FD_DATE         = " & If(IsDate(UpParam.FDDate), "'" & Convert.ToDateTime(UpParam.FDDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                "FD_AS_DATE      =" & If(IsDate(UpParam.FDAsDate), "'" & Convert.ToDateTime(UpParam.FDAsDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                "FD_AMT          =" & UpParam.FDAmount & ", " & _
                                                "FD_INT_RATE     =" & UpParam.FDIntRate & ", " & _
                                                "FD_INT_PAY_COND ='" & UpParam.PaymentCondition & "', " & _
                                                "FD_MAT_DATE     =" & If(IsDate(UpParam.FDMaturityDate), "'" & Convert.ToDateTime(UpParam.FDMaturityDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                "FD_MAT_AMT      =" & UpParam.FDMaturityAmount & ", " & _
                                                "FD_OTHER_DETAIL ='" & UpParam.Remarks & "', " & _
                                                "FD_RENEWFROM_ID = " & UpParam.RenewFrom_ID & " ," & _
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                                "  WHERE FD_TR_ID    ='" & UpParam.TxnID & "'"
            dbService.Update(ConnectOneWS.Tables.FD_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates FD History
        ''' </summary>
        ''' <param name="UpFDHty"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateFDHistory</remarks>
        Public Shared Function UpdateFDHistory(ByVal UpFDHty As Parameter_UpdateFDHistory_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "UPDATE FD_HISTORY_INFO SET " & _
                                                "FDH_ACTION = '" & UpFDHty.FDAction & "'," & _
                                                "FDH_STATUS = '" & UpFDHty.FDStatus & "', " & _
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                                " WHERE FDH_TR_ID = '" & UpFDHty.TxnId & "' AND  FDH_FD_ID = '" & UpFDHty.FDID & "'"
            dbService.Update(ConnectOneWS.Tables.FD_HISTORY_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Master Info
        ''' </summary>
        ''' <param name="UpMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.FDs_VoucherFD_UpdateMasterInfo</remarks>
        Public Shared Function UpdateMasterInfo(ByVal UpMInfo As Parameter_UpdateMasterInfo_VoucherFD, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                        "TR_CEN_ID = " & inBasicParam.openCenID.ToString & "," & _
                                        "TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "," & _
                                        "TR_CODE = " & UpMInfo.TxnCode & "," & _
                                        "TR_VNO = '" & UpMInfo.VNo & "', " & _
                                        "TR_DATE = " & If(IsDate(UpMInfo.TDate), "'" & Convert.ToDateTime(UpMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "TR_SUB_AMT = " & UpMInfo.SubTotal & " , " & _
                                        "TR_CASH_AMT = " & UpMInfo.Cash & " , " & _
                                        "TR_BANK_AMT = " & UpMInfo.Bank & " , " & _
                                        "TR_TDS_AMT = " & UpMInfo.TDS & " , " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpMInfo.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpMInfo.TDate)
            Return True
        End Function

        Public Shared Function InsertNewFD_Txn(inParam As Param_Txn_NewFD_InsertVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_Insert Is Nothing Then
                If Not inParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Dr_Led_ID)
                If Not inParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If


            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            ' Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertFD Is Nothing Then
                If Not InsertFD(inParam.param_InsertFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertFDHistory Is Nothing Then
                If Not InsertFDHistory(inParam.param_InsertFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not Insert(inParam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not InsertPurpose(inParam.param_Insert.MasterTxnID, inParam.param_Insert.PurposeID, inParam.param_Insert.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If

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

        Public Shared Function UpdateNewFD_Txn(upParam As Param_Txn_NewFD_UpdateVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_Insert Is Nothing Then
                If Not upParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Dr_Led_ID)
                If Not upParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Cr_Led_ID)
                If Not upParam.param_Insert.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.SUB_Cr_Led_ID)
                If Not upParam.param_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
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
            If Not upParam.param_UpdateMaster Is Nothing Then 'Master Updated
                If Not UpdateMasterInfo(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateFD Is Nothing Then 'Actual Txn Updated
                If Not UpdateFD(upParam.param_UpdateFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateFDHistory Is Nothing Then
                If Not UpdateFDHistory(upParam.param_UpdateFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_DeleteVoucher_Txn_MID Is Nothing Then 'Txn Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.param_DeleteVoucher_Txn_MID & "'", inBasicParam)
            End If
            If Not upParam.param_Insert Is Nothing Then 'Actual Txn Re-Inserted
                If Not Insert(upParam.param_Insert, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_Insert Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.param_DeleteVoucher_Txn_MID & "'", inBasicParam)
            End If
            If Not upParam.param_Insert Is Nothing Then
                If Not InsertPurpose(upParam.param_Insert.MasterTxnID, upParam.param_Insert.PurposeID, upParam.param_Insert.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.param_DeleteVoucher_Txn_MID & "'", inBasicParam)
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

        Public Shared Function DeleteNewFD_Txn(delParam As Param_Txn_NewFD_DeleteVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteFDHistory Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_HISTORY_INFO, "FDH_TR_ID = '" & delParam.MID_DeleteFDHistory & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteFD Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_INFO, "FD_TR_ID = '" & delParam.MID_DeleteFD & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function InsertRenewFD_Txn(inParam As Param_Txn_RenewFD_InsertVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_InsertNewFD Is Nothing Then
                If Not inParam.param_InsertNewFD.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.Dr_Led_ID)
                If Not inParam.param_InsertNewFD.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.Cr_Led_ID)
                If Not inParam.param_InsertNewFD.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.SUB_Cr_Led_ID)
                If Not inParam.param_InsertNewFD.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InAdjExcessAmtRec Is Nothing Then
                If Not inParam.param_InAdjExcessAmtRec.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InAdjExcessAmtRec.TDate), inParam.param_InAdjExcessAmtRec.Dr_Led_ID)
                If Not inParam.param_InAdjExcessAmtRec.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InAdjExcessAmtRec.TDate), inParam.param_InAdjExcessAmtRec.Cr_Led_ID)
                If Not inParam.param_InAdjExcessAmtRec.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InAdjExcessAmtRec.TDate), inParam.param_InAdjExcessAmtRec.SUB_Cr_Led_ID)
                If Not inParam.param_InAdjExcessAmtRec.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InAdjExcessAmtRec.TDate), inParam.param_InAdjExcessAmtRec.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InBalRec_Cash Is Nothing Then
                If Not inParam.param_InBalRec_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Cash.TDate), inParam.param_InBalRec_Cash.Dr_Led_ID)
                If Not inParam.param_InBalRec_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Cash.TDate), inParam.param_InBalRec_Cash.Cr_Led_ID)
                If Not inParam.param_InBalRec_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Cash.TDate), inParam.param_InBalRec_Cash.SUB_Cr_Led_ID)
                If Not inParam.param_InBalRec_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Cash.TDate), inParam.param_InBalRec_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InBalRec_Notcash Is Nothing Then
                If Not inParam.param_InBalRec_Notcash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Notcash.TDate), inParam.param_InBalRec_Notcash.Dr_Led_ID)
                If Not inParam.param_InBalRec_Notcash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Notcash.TDate), inParam.param_InBalRec_Notcash.Cr_Led_ID)
                If Not inParam.param_InBalRec_Notcash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Notcash.TDate), inParam.param_InBalRec_Notcash.SUB_Cr_Led_ID)
                If Not inParam.param_InBalRec_Notcash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InBalRec_Notcash.TDate), inParam.param_InBalRec_Notcash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InCloseFDJournal Is Nothing Then
                If Not inParam.param_InCloseFDJournal.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCloseFDJournal.TDate), inParam.param_InCloseFDJournal.Dr_Led_ID)
                If Not inParam.param_InCloseFDJournal.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCloseFDJournal.TDate), inParam.param_InCloseFDJournal.Cr_Led_ID)
                If Not inParam.param_InCloseFDJournal.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCloseFDJournal.TDate), inParam.param_InCloseFDJournal.SUB_Cr_Led_ID)
                If Not inParam.param_InCloseFDJournal.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCloseFDJournal.TDate), inParam.param_InCloseFDJournal.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InCreditedInterest_Cash Is Nothing Then
                If Not inParam.param_InCreditedInterest_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Cash.TDate), inParam.param_InCreditedInterest_Cash.Dr_Led_ID)
                If Not inParam.param_InCreditedInterest_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Cash.TDate), inParam.param_InCreditedInterest_Cash.Cr_Led_ID)
                If Not inParam.param_InCreditedInterest_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Cash.TDate), inParam.param_InCreditedInterest_Cash.SUB_Cr_Led_ID)
                If Not inParam.param_InCreditedInterest_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Cash.TDate), inParam.param_InCreditedInterest_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InCreditedInterest_Notcash Is Nothing Then
                If Not inParam.param_InCreditedInterest_Notcash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Notcash.TDate), inParam.param_InCreditedInterest_Notcash.Dr_Led_ID)
                If Not inParam.param_InCreditedInterest_Notcash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Notcash.TDate), inParam.param_InCreditedInterest_Notcash.Cr_Led_ID)
                If Not inParam.param_InCreditedInterest_Notcash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Notcash.TDate), inParam.param_InCreditedInterest_Notcash.SUB_Cr_Led_ID)
                If Not inParam.param_InCreditedInterest_Notcash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InCreditedInterest_Notcash.TDate), inParam.param_InCreditedInterest_Notcash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InInterestOverhead Is Nothing Then
                If Not inParam.param_InInterestOverhead.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InInterestOverhead.TDate), inParam.param_InInterestOverhead.Dr_Led_ID)
                If Not inParam.param_InInterestOverhead.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InInterestOverhead.TDate), inParam.param_InInterestOverhead.Cr_Led_ID)
                If Not inParam.param_InInterestOverhead.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InInterestOverhead.TDate), inParam.param_InInterestOverhead.SUB_Cr_Led_ID)
                If Not inParam.param_InInterestOverhead.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InInterestOverhead.TDate), inParam.param_InInterestOverhead.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InJournalInterest Is Nothing Then
                If Not inParam.param_InJournalInterest.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InJournalInterest.TDate), inParam.param_InJournalInterest.Dr_Led_ID)
                If Not inParam.param_InJournalInterest.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InJournalInterest.TDate), inParam.param_InJournalInterest.Cr_Led_ID)
                If Not inParam.param_InJournalInterest.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InJournalInterest.TDate), inParam.param_InJournalInterest.SUB_Cr_Led_ID)
                If Not inParam.param_InJournalInterest.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InJournalInterest.TDate), inParam.param_InJournalInterest.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InPmt_Cash Is Nothing Then
                If Not inParam.param_InPmt_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_Cash.TDate), inParam.param_InPmt_Cash.Dr_Led_ID)
                If Not inParam.param_InPmt_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_Cash.TDate), inParam.param_InPmt_Cash.Cr_Led_ID)
                If Not inParam.param_InPmt_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_Cash.TDate), inParam.param_InPmt_Cash.SUB_Cr_Led_ID)
                If Not inParam.param_InPmt_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_Cash.TDate), inParam.param_InPmt_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InPmt_NotCash Is Nothing Then
                If Not inParam.param_InPmt_NotCash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_NotCash.TDate), inParam.param_InPmt_NotCash.Dr_Led_ID)
                If Not inParam.param_InPmt_NotCash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_NotCash.TDate), inParam.param_InPmt_NotCash.Cr_Led_ID)
                If Not inParam.param_InPmt_NotCash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_NotCash.TDate), inParam.param_InPmt_NotCash.SUB_Cr_Led_ID)
                If Not inParam.param_InPmt_NotCash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InPmt_NotCash.TDate), inParam.param_InPmt_NotCash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InsertNewFD Is Nothing Then
                If Not inParam.param_InsertNewFD.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.Dr_Led_ID)
                If Not inParam.param_InsertNewFD.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.Cr_Led_ID)
                If Not inParam.param_InsertNewFD.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.SUB_Cr_Led_ID)
                If Not inParam.param_InsertNewFD.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertNewFD.TDate), inParam.param_InsertNewFD.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InTDS_Deducted1 Is Nothing Then
                If Not inParam.param_InTDS_Deducted1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted1.TDate), inParam.param_InTDS_Deducted1.Dr_Led_ID)
                If Not inParam.param_InTDS_Deducted1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted1.TDate), inParam.param_InTDS_Deducted1.Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted1.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted1.TDate), inParam.param_InTDS_Deducted1.SUB_Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted1.TDate), inParam.param_InTDS_Deducted1.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InTDS_Deducted2 Is Nothing Then
                If Not inParam.param_InTDS_Deducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.Dr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.SUB_Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InTDS_Deducted2 Is Nothing Then
                If Not inParam.param_InTDS_Deducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.Dr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.SUB_Cr_Led_ID)
                If Not inParam.param_InTDS_Deducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InTDS_Deducted2.TDate), inParam.param_InTDS_Deducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If


            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertFD Is Nothing Then
                If Not InsertFD(inParam.param_InsertFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InFDHistory Is Nothing Then
                If Not InsertFDHistory(inParam.param_InFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InRenewFDHistory Is Nothing Then
                If Not InsertFDHistory(inParam.param_InRenewFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not inParam.param_CloseoldFD Is Nothing Then
            ' If Not CloseFD(inParam.param_CloseoldFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            ' End If 'Removed from Client Side
            If Not inParam.param_InsertNewFD Is Nothing Then
                If Not Insert(inParam.param_InsertNewFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InCloseFDJournal Is Nothing Then
                If Not Insert(inParam.param_InCloseFDJournal, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InAdjExcessAmtRec Is Nothing Then
                If Not Insert(inParam.param_InAdjExcessAmtRec, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InBalRec_Notcash Is Nothing Then
                If Not Insert(inParam.param_InBalRec_Notcash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InBalRec_Cash Is Nothing Then
                If Not Insert(inParam.param_InBalRec_Cash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InJournalInterest Is Nothing Then
                If Not Insert(inParam.param_InJournalInterest, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InCreditedInterest_Notcash Is Nothing Then
                If Not Insert(inParam.param_InCreditedInterest_Notcash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InCreditedInterest_Cash Is Nothing Then
                If Not Insert(inParam.param_InCreditedInterest_Cash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InPmt_NotCash Is Nothing Then
                If Not Insert(inParam.param_InPmt_NotCash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InPmt_Cash Is Nothing Then
                If Not Insert(inParam.param_InPmt_Cash, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InInterestOverhead Is Nothing Then
                If Not Insert(inParam.param_InInterestOverhead, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InTDS_Deducted1 Is Nothing Then
                If Not Insert(inParam.param_InTDS_Deducted1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InTDS_Deducted2 Is Nothing Then
                If Not Insert(inParam.param_InTDS_Deducted2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertNewFD Is Nothing Then
                If Not InsertPurpose(inParam.param_InsertNewFD.MasterTxnID, inParam.param_InsertNewFD.PurposeID, inParam.param_InsertNewFD.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'Commit here 
            ' txn.Complete()
            '   End Using

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

        Public Shared Function UpdateRenewFD_Txn(upParam As Param_Txn_RenewFD_UpdateVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_InsertNewFD Is Nothing Then
                If Not upParam.param_InsertNewFD.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.Dr_Led_ID)
                If Not upParam.param_InsertNewFD.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.Cr_Led_ID)
                If Not upParam.param_InsertNewFD.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.SUB_Cr_Led_ID)
                If Not upParam.param_InsertNewFD.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InAdjExcessAmtRec Is Nothing Then
                If Not upParam.param_InAdjExcessAmtRec.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InAdjExcessAmtRec.TDate), upParam.param_InAdjExcessAmtRec.Dr_Led_ID)
                If Not upParam.param_InAdjExcessAmtRec.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InAdjExcessAmtRec.TDate), upParam.param_InAdjExcessAmtRec.Cr_Led_ID)
                If Not upParam.param_InAdjExcessAmtRec.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InAdjExcessAmtRec.TDate), upParam.param_InAdjExcessAmtRec.SUB_Cr_Led_ID)
                If Not upParam.param_InAdjExcessAmtRec.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InAdjExcessAmtRec.TDate), upParam.param_InAdjExcessAmtRec.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InBalRec_Cash Is Nothing Then
                If Not upParam.param_InBalRec_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Cash.TDate), upParam.param_InBalRec_Cash.Dr_Led_ID)
                If Not upParam.param_InBalRec_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Cash.TDate), upParam.param_InBalRec_Cash.Cr_Led_ID)
                If Not upParam.param_InBalRec_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Cash.TDate), upParam.param_InBalRec_Cash.SUB_Cr_Led_ID)
                If Not upParam.param_InBalRec_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Cash.TDate), upParam.param_InBalRec_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InBalRec_Notcash Is Nothing Then
                If Not upParam.param_InBalRec_Notcash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Notcash.TDate), upParam.param_InBalRec_Notcash.Dr_Led_ID)
                If Not upParam.param_InBalRec_Notcash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Notcash.TDate), upParam.param_InBalRec_Notcash.Cr_Led_ID)
                If Not upParam.param_InBalRec_Notcash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Notcash.TDate), upParam.param_InBalRec_Notcash.SUB_Cr_Led_ID)
                If Not upParam.param_InBalRec_Notcash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InBalRec_Notcash.TDate), upParam.param_InBalRec_Notcash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InCloseFDJournal Is Nothing Then
                If Not upParam.param_InCloseFDJournal.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCloseFDJournal.TDate), upParam.param_InCloseFDJournal.Dr_Led_ID)
                If Not upParam.param_InCloseFDJournal.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCloseFDJournal.TDate), upParam.param_InCloseFDJournal.Cr_Led_ID)
                If Not upParam.param_InCloseFDJournal.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCloseFDJournal.TDate), upParam.param_InCloseFDJournal.SUB_Cr_Led_ID)
                If Not upParam.param_InCloseFDJournal.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCloseFDJournal.TDate), upParam.param_InCloseFDJournal.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InCreditedInterest_Cash Is Nothing Then
                If Not upParam.param_InCreditedInterest_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Cash.TDate), upParam.param_InCreditedInterest_Cash.Dr_Led_ID)
                If Not upParam.param_InCreditedInterest_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Cash.TDate), upParam.param_InCreditedInterest_Cash.Cr_Led_ID)
                If Not upParam.param_InCreditedInterest_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Cash.TDate), upParam.param_InCreditedInterest_Cash.SUB_Cr_Led_ID)
                If Not upParam.param_InCreditedInterest_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Cash.TDate), upParam.param_InCreditedInterest_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InCreditedInterest_Notcash Is Nothing Then
                If Not upParam.param_InCreditedInterest_Notcash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Notcash.TDate), upParam.param_InCreditedInterest_Notcash.Dr_Led_ID)
                If Not upParam.param_InCreditedInterest_Notcash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Notcash.TDate), upParam.param_InCreditedInterest_Notcash.Cr_Led_ID)
                If Not upParam.param_InCreditedInterest_Notcash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Notcash.TDate), upParam.param_InCreditedInterest_Notcash.SUB_Cr_Led_ID)
                If Not upParam.param_InCreditedInterest_Notcash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InCreditedInterest_Notcash.TDate), upParam.param_InCreditedInterest_Notcash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InInterestOverhead Is Nothing Then
                If Not upParam.param_InInterestOverhead.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InInterestOverhead.TDate), upParam.param_InInterestOverhead.Dr_Led_ID)
                If Not upParam.param_InInterestOverhead.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InInterestOverhead.TDate), upParam.param_InInterestOverhead.Cr_Led_ID)
                If Not upParam.param_InInterestOverhead.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InInterestOverhead.TDate), upParam.param_InInterestOverhead.SUB_Cr_Led_ID)
                If Not upParam.param_InInterestOverhead.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InInterestOverhead.TDate), upParam.param_InInterestOverhead.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InJournalInterest Is Nothing Then
                If Not upParam.param_InJournalInterest.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InJournalInterest.TDate), upParam.param_InJournalInterest.Dr_Led_ID)
                If Not upParam.param_InJournalInterest.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InJournalInterest.TDate), upParam.param_InJournalInterest.Cr_Led_ID)
                If Not upParam.param_InJournalInterest.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InJournalInterest.TDate), upParam.param_InJournalInterest.SUB_Cr_Led_ID)
                If Not upParam.param_InJournalInterest.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InJournalInterest.TDate), upParam.param_InJournalInterest.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InPmt_Cash Is Nothing Then
                If Not upParam.param_InPmt_Cash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_Cash.TDate), upParam.param_InPmt_Cash.Dr_Led_ID)
                If Not upParam.param_InPmt_Cash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_Cash.TDate), upParam.param_InPmt_Cash.Cr_Led_ID)
                If Not upParam.param_InPmt_Cash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_Cash.TDate), upParam.param_InPmt_Cash.SUB_Cr_Led_ID)
                If Not upParam.param_InPmt_Cash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_Cash.TDate), upParam.param_InPmt_Cash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InPmt_NotCash Is Nothing Then
                If Not upParam.param_InPmt_NotCash.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_NotCash.TDate), upParam.param_InPmt_NotCash.Dr_Led_ID)
                If Not upParam.param_InPmt_NotCash.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_NotCash.TDate), upParam.param_InPmt_NotCash.Cr_Led_ID)
                If Not upParam.param_InPmt_NotCash.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_NotCash.TDate), upParam.param_InPmt_NotCash.SUB_Cr_Led_ID)
                If Not upParam.param_InPmt_NotCash.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InPmt_NotCash.TDate), upParam.param_InPmt_NotCash.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InsertNewFD Is Nothing Then
                If Not upParam.param_InsertNewFD.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.Dr_Led_ID)
                If Not upParam.param_InsertNewFD.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.Cr_Led_ID)
                If Not upParam.param_InsertNewFD.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.SUB_Cr_Led_ID)
                If Not upParam.param_InsertNewFD.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertNewFD.TDate), upParam.param_InsertNewFD.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InTDS_Deducted1 Is Nothing Then
                If Not upParam.param_InTDS_Deducted1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted1.TDate), upParam.param_InTDS_Deducted1.Dr_Led_ID)
                If Not upParam.param_InTDS_Deducted1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted1.TDate), upParam.param_InTDS_Deducted1.Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted1.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted1.TDate), upParam.param_InTDS_Deducted1.SUB_Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted1.TDate), upParam.param_InTDS_Deducted1.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InTDS_Deducted2 Is Nothing Then
                If Not upParam.param_InTDS_Deducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.Dr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.SUB_Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InTDS_Deducted2 Is Nothing Then
                If Not upParam.param_InTDS_Deducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.Dr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.SUB_Cr_Led_ID)
                If Not upParam.param_InTDS_Deducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InTDS_Deducted2.TDate), upParam.param_InTDS_Deducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then 'Master Updated
                If Not UpdateMasterInfo(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateFD Is Nothing Then 'FD updated
                If Not UpdateFD(upParam.param_UpdateFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpFDHistory Is Nothing Then 'FD History Updated
                If Not UpdateFDHistory(upParam.param_UpFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpRenFDHistory Is Nothing Then 'Renew FD History Updated
                If Not UpdateFDHistory(upParam.param_UpRenFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not upParam.param_CloseFD Is Nothing Then
            ' If Not CloseFD(upParam.param_CloseFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If 'Removed from Client Side
            If Not upParam.MID_DeleteTxns Is Nothing Then 'Txns Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteTxns Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.param_InsertNewFD Is Nothing Then 'New FD Re-Inserted
                If Not Insert(upParam.param_InsertNewFD, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InCloseFDJournal Is Nothing Then
                If Not Insert(upParam.param_InCloseFDJournal, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InAdjExcessAmtRec Is Nothing Then
                If Not Insert(upParam.param_InAdjExcessAmtRec, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InBalRec_Notcash Is Nothing Then
                If Not Insert(upParam.param_InBalRec_Notcash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InBalRec_Cash Is Nothing Then
                If Not Insert(upParam.param_InBalRec_Cash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InJournalInterest Is Nothing Then
                If Not Insert(upParam.param_InJournalInterest, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InCreditedInterest_Notcash Is Nothing Then
                If Not Insert(upParam.param_InCreditedInterest_Notcash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InCreditedInterest_Cash Is Nothing Then
                If Not Insert(upParam.param_InCreditedInterest_Cash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InPmt_NotCash Is Nothing Then
                If Not Insert(upParam.param_InPmt_NotCash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InPmt_Cash Is Nothing Then
                If Not Insert(upParam.param_InPmt_Cash, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InInterestOverhead Is Nothing Then
                If Not Insert(upParam.param_InInterestOverhead, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InTDS_Deducted1 Is Nothing Then
                If Not Insert(upParam.param_InTDS_Deducted1, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InTDS_Deducted2 Is Nothing Then
                If Not Insert(upParam.param_InTDS_Deducted2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertNewFD Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertNewFD.MasterTxnID, upParam.param_InsertNewFD.PurposeID, upParam.param_InsertNewFD.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
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

        Public Shared Function DeleteRenewFD_Txn(delParam As Param_Txn_RenewFD_DeleteVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteFDHistory Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_HISTORY_INFO, "FDH_TR_ID = '" & delParam.MID_DeleteFDHistory & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteFD Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_INFO, "FD_TR_ID = '" & delParam.MID_DeleteFD & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            '   txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function InsertCloseFD_Txn(inParam As Param_Txn_CloseFD_InsertVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_Insert Is Nothing Then
                If Not inParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Dr_Led_ID)
                If Not inParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InterestOverhead Is Nothing Then
                If Not inParam.param_InterestOverhead.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead.TDate), inParam.param_InterestOverhead.Dr_Led_ID)
                If Not inParam.param_InterestOverhead.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead.TDate), inParam.param_InterestOverhead.Cr_Led_ID)
                If Not inParam.param_InterestOverhead.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead.TDate), inParam.param_InterestOverhead.SUB_Cr_Led_ID)
                If Not inParam.param_InterestOverhead.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead.TDate), inParam.param_InterestOverhead.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InFDCloseInterest Is Nothing Then
                If Not inParam.param_InFDCloseInterest.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InFDCloseInterest.TDate), inParam.param_InFDCloseInterest.Dr_Led_ID)
                If Not inParam.param_InFDCloseInterest.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InFDCloseInterest.TDate), inParam.param_InFDCloseInterest.Cr_Led_ID)
                If Not inParam.param_InFDCloseInterest.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InFDCloseInterest.TDate), inParam.param_InFDCloseInterest.SUB_Cr_Led_ID)
                If Not inParam.param_InFDCloseInterest.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InFDCloseInterest.TDate), inParam.param_InFDCloseInterest.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InterestOverhead_BankCharges Is Nothing Then
                If Not inParam.param_InterestOverhead_BankCharges.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead_BankCharges.TDate), inParam.param_InterestOverhead_BankCharges.Dr_Led_ID)
                If Not inParam.param_InterestOverhead_BankCharges.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead_BankCharges.TDate), inParam.param_InterestOverhead_BankCharges.Cr_Led_ID)
                If Not inParam.param_InterestOverhead_BankCharges.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead_BankCharges.TDate), inParam.param_InterestOverhead_BankCharges.SUB_Cr_Led_ID)
                If Not inParam.param_InterestOverhead_BankCharges.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InterestOverhead_BankCharges.TDate), inParam.param_InterestOverhead_BankCharges.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_TDSDeducted1 Is Nothing Then
                If Not inParam.param_TDSDeducted1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted1.TDate), inParam.param_TDSDeducted1.Dr_Led_ID)
                If Not inParam.param_TDSDeducted1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted1.TDate), inParam.param_TDSDeducted1.Cr_Led_ID)
                If Not inParam.param_TDSDeducted1.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted1.TDate), inParam.param_TDSDeducted1.SUB_Cr_Led_ID)
                If Not inParam.param_TDSDeducted1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted1.TDate), inParam.param_TDSDeducted1.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_TDSDeducted2 Is Nothing Then
                If Not inParam.param_TDSDeducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted2.TDate), inParam.param_TDSDeducted2.Dr_Led_ID)
                If Not inParam.param_TDSDeducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted2.TDate), inParam.param_TDSDeducted2.Cr_Led_ID)
                If Not inParam.param_TDSDeducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted2.TDate), inParam.param_TDSDeducted2.SUB_Cr_Led_ID)
                If Not inParam.param_TDSDeducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSDeducted2.TDate), inParam.param_TDSDeducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If

            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not Insert(inParam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InterestOverhead Is Nothing Then
                If Not Insert(inParam.param_InterestOverhead, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InterestOverhead_BankCharges Is Nothing Then
                If Not Insert(inParam.param_InterestOverhead_BankCharges, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not inParam.param_CloseFD Is Nothing Then
            ' If Not CloseFD(inParam.param_CloseFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            ' End If 'Removed from Client Side
            If Not inParam.param_InsertFDHistory Is Nothing Then
                If Not InsertFDHistory(inParam.param_InsertFDHistory, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InFDCloseInterest Is Nothing Then
                If Not Insert(inParam.param_InFDCloseInterest, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_TDSDeducted1 Is Nothing Then
                If Not Insert(inParam.param_TDSDeducted1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_TDSDeducted2 Is Nothing Then
                If Not Insert(inParam.param_TDSDeducted2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not InsertPurpose(inParam.param_Insert.MasterTxnID, inParam.param_Insert.PurposeID, inParam.param_Insert.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '  End Using
            'Commit here 
            '  txn.Complete()
            '     End Using

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

        Public Shared Function UpdateCloseFD_Txn(upParam As Param_Txn_CloseFD_UpdateVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_InsertCloseAmt Is Nothing Then
                If Not upParam.param_InsertCloseAmt.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCloseAmt.TDate), upParam.param_InsertCloseAmt.Dr_Led_ID)
                If Not upParam.param_InsertCloseAmt.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCloseAmt.TDate), upParam.param_InsertCloseAmt.Cr_Led_ID)
                If Not upParam.param_InsertCloseAmt.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCloseAmt.TDate), upParam.param_InsertCloseAmt.SUB_Cr_Led_ID)
                If Not upParam.param_InsertCloseAmt.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertCloseAmt.TDate), upParam.param_InsertCloseAmt.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InterestOverhead Is Nothing Then
                If Not upParam.param_InterestOverhead.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead.TDate), upParam.param_InterestOverhead.Dr_Led_ID)
                If Not upParam.param_InterestOverhead.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead.TDate), upParam.param_InterestOverhead.Cr_Led_ID)
                If Not upParam.param_InterestOverhead.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead.TDate), upParam.param_InterestOverhead.SUB_Cr_Led_ID)
                If Not upParam.param_InterestOverhead.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead.TDate), upParam.param_InterestOverhead.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InFDCloseInterest Is Nothing Then
                If Not upParam.param_InFDCloseInterest.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InFDCloseInterest.TDate), upParam.param_InFDCloseInterest.Dr_Led_ID)
                If Not upParam.param_InFDCloseInterest.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InFDCloseInterest.TDate), upParam.param_InFDCloseInterest.Cr_Led_ID)
                If Not upParam.param_InFDCloseInterest.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InFDCloseInterest.TDate), upParam.param_InFDCloseInterest.SUB_Cr_Led_ID)
                If Not upParam.param_InFDCloseInterest.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InFDCloseInterest.TDate), upParam.param_InFDCloseInterest.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InterestOverhead_BankCharges Is Nothing Then
                If Not upParam.param_InterestOverhead_BankCharges.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead_BankCharges.TDate), upParam.param_InterestOverhead_BankCharges.Dr_Led_ID)
                If Not upParam.param_InterestOverhead_BankCharges.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead_BankCharges.TDate), upParam.param_InterestOverhead_BankCharges.Cr_Led_ID)
                If Not upParam.param_InterestOverhead_BankCharges.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead_BankCharges.TDate), upParam.param_InterestOverhead_BankCharges.SUB_Cr_Led_ID)
                If Not upParam.param_InterestOverhead_BankCharges.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InterestOverhead_BankCharges.TDate), upParam.param_InterestOverhead_BankCharges.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_TDSDeducted1 Is Nothing Then
                If Not upParam.param_TDSDeducted1.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted1.TDate), upParam.param_TDSDeducted1.Dr_Led_ID)
                If Not upParam.param_TDSDeducted1.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted1.TDate), upParam.param_TDSDeducted1.Cr_Led_ID)
                If Not upParam.param_TDSDeducted1.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted1.TDate), upParam.param_TDSDeducted1.SUB_Cr_Led_ID)
                If Not upParam.param_TDSDeducted1.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted1.TDate), upParam.param_TDSDeducted1.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_TDSDeducted2 Is Nothing Then
                If Not upParam.param_TDSDeducted2.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted2.TDate), upParam.param_TDSDeducted2.Dr_Led_ID)
                If Not upParam.param_TDSDeducted2.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted2.TDate), upParam.param_TDSDeducted2.Cr_Led_ID)
                If Not upParam.param_TDSDeducted2.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted2.TDate), upParam.param_TDSDeducted2.SUB_Cr_Led_ID)
                If Not upParam.param_TDSDeducted2.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSDeducted2.TDate), upParam.param_TDSDeducted2.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If


            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMasterInfo(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If

            If Not upParam.MID_DeleteTxns Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteTxns Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.param_InsertCloseAmt Is Nothing Then
                If Not Insert(upParam.param_InsertCloseAmt, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InterestOverhead Is Nothing Then
                If Not Insert(upParam.param_InterestOverhead, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InterestOverhead_BankCharges Is Nothing Then
                If Not Insert(upParam.param_InterestOverhead_BankCharges, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not upParam.param_CloseFD Is Nothing Then
            ' If Not CloseFD(upParam.param_CloseFD, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If 'Removed from Client Side
            If Not upParam.param_InsertFDHistory Is Nothing Then
                If Not InsertFDHistory(upParam.param_InsertFDHistory, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InFDCloseInterest Is Nothing Then
                If Not Insert(upParam.param_InFDCloseInterest, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_TDSDeducted1 Is Nothing Then
                If Not Insert(upParam.param_TDSDeducted1, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_TDSDeducted2 Is Nothing Then
                If Not Insert(upParam.param_TDSDeducted2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertCloseAmt Is Nothing Then
                If Not InsertPurpose(upParam.param_InsertCloseAmt.MasterTxnID, upParam.param_InsertCloseAmt.PurposeID, upParam.param_InsertCloseAmt.Amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
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

        Public Shared Function DeleteCloseFD_Txn(delParam As Param_Txn_CloseFD_DeleteVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            If Not delParam.MID_DeleteFDHistory Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.FD_HISTORY_INFO, "FDH_TR_ID = '" & delParam.MID_DeleteFDHistory & "'", inBasicParam)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function InsertIncomeAndExpenses_Txn(inParam As Param_Txn_IncomeExpenses_InsertVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not inParam.param_Insert Is Nothing Then
                If Not inParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Dr_Led_ID)
                If Not inParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Cr_Led_ID)
                If Not inParam.param_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_Insert.TDate), inParam.param_Insert.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_InsertIntRec Is Nothing Then
                If Not inParam.param_InsertIntRec.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertIntRec.TDate), inParam.param_InsertIntRec.Dr_Led_ID)
                If Not inParam.param_InsertIntRec.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertIntRec.TDate), inParam.param_InsertIntRec.Cr_Led_ID)
                If Not inParam.param_InsertIntRec.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertIntRec.TDate), inParam.param_InsertIntRec.SUB_Cr_Led_ID)
                If Not inParam.param_InsertIntRec.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_InsertIntRec.TDate), inParam.param_InsertIntRec.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not inParam.param_TDSbyBank Is Nothing Then
                If Not inParam.param_TDSbyBank.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSbyBank.TDate), inParam.param_TDSbyBank.Dr_Led_ID)
                If Not inParam.param_TDSbyBank.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSbyBank.TDate), inParam.param_TDSbyBank.Cr_Led_ID)
                If Not inParam.param_TDSbyBank.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSbyBank.TDate), inParam.param_TDSbyBank.SUB_Cr_Led_ID)
                If Not inParam.param_TDSbyBank.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.param_TDSbyBank.TDate), inParam.param_TDSbyBank.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            ''  Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_TDSbyBank Is Nothing Then
                If Not Insert(inParam.param_TDSbyBank, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertIntRec Is Nothing Then
                If Not Insert(inParam.param_InsertIntRec, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_Insert Is Nothing Then
                If Not Insert(inParam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.PurposeID Is Nothing Then
                Dim amount As Double
                If inParam.param_Insert Is Nothing Then
                    amount = inParam.param_TDSbyBank.Amount
                Else
                    amount = inParam.param_Insert.Amount
                End If
                If Not InsertPurpose(inParam.param_InsertMaster.RecID, inParam.PurposeID, amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '   End Using
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

        Public Shared Function UpdateIncomeAndExpenses_Txn(upParam As Param_Txn_IncomeExpenses_UpdateVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_Insert Is Nothing Then
                If Not upParam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Dr_Led_ID)
                If Not upParam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.Cr_Led_ID)
                If Not upParam.param_Insert.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.SUB_Cr_Led_ID)
                If Not upParam.param_Insert.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Insert.TDate), upParam.param_Insert.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_InsertIntRec Is Nothing Then
                If Not upParam.param_InsertIntRec.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertIntRec.TDate), upParam.param_InsertIntRec.Dr_Led_ID)
                If Not upParam.param_InsertIntRec.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertIntRec.TDate), upParam.param_InsertIntRec.Cr_Led_ID)
                If Not upParam.param_InsertIntRec.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertIntRec.TDate), upParam.param_InsertIntRec.SUB_Cr_Led_ID)
                If Not upParam.param_InsertIntRec.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_InsertIntRec.TDate), upParam.param_InsertIntRec.SUB_Dr_Led_ID)
                'If Not upParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If
            If Not upParam.param_TDSbyBank Is Nothing Then
                If Not upParam.param_TDSbyBank.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSbyBank.TDate), upParam.param_TDSbyBank.Dr_Led_ID)
                If Not upParam.param_TDSbyBank.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSbyBank.TDate), upParam.param_TDSbyBank.Cr_Led_ID)
                If Not upParam.param_TDSbyBank.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSbyBank.TDate), upParam.param_TDSbyBank.SUB_Cr_Led_ID)
                If Not upParam.param_TDSbyBank.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_TDSbyBank.TDate), upParam.param_TDSbyBank.SUB_Dr_Led_ID)
                'If Not inParam.param_Insert.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(Voucher_Insert.TDate), Voucher_Insert.Party1)
            End If

            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMasterInfo(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_DeleteTxns Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteTxns Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
            End If
            If Not upParam.param_TDSbyBank Is Nothing Then
                If Not Insert(upParam.param_TDSbyBank, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertIntRec Is Nothing Then
                If Not Insert(upParam.param_InsertIntRec, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_Insert Is Nothing Then
                If Not Insert(upParam.param_Insert, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.PurposeID Is Nothing Then
                Dim amount As Double
                If upParam.param_Insert Is Nothing Then
                    amount = upParam.param_TDSbyBank.Amount
                Else
                    amount = upParam.param_Insert.Amount
                End If
                If Not InsertPurpose(upParam.param_UpdateMaster.RecID, upParam.PurposeID, amount, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_DeleteTxns & "'", inBasicParam)
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

        Public Shared Function DeleteIncomeAndExpenses_Txn(delParam As Param_Txn_IncomeExpenses_DeleteVoucherFD, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then 'Purpose Deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '  End Using
            Return True
        End Function

    End Class
#End Region

End Namespace
