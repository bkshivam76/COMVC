Imports System.Data
Imports System.Xml
Namespace Real
    <Serializable>
    Public Class DataFunctions
#Region "Parameter Classes"
        <Serializable>
        Public Class Param_Asset_Common
            Public CEN_ID As String
            Public YEAR_ID As String
            Public PROFILE As String
            Public ASSET_ID As String
            Public M_ID As String
        End Class
        <Serializable>
        Public Class Param_GetOpeningBalance
            Public RecIDColHeader As String
            Public AccountRecID As String
        End Class
        <Serializable>
        Public Class Param_GetCashBankTransSumAmountTwoDates
            Public FromDate As Date
            Public ToDate As Date
        End Class
        <Serializable>
        Public Class Param_AddOpeningBalance
            Public Amount As Double
            Public RecID As String
            Public openYearID As String
        End Class
        <Serializable>
        Public Class Param_UpdateOpeningBalance
            Public Amount As Double
            Public RecID As String
            'Public Status_Action As String
        End Class
        <Serializable>
        Public Class Param_GetLastEditOn
            Public Rec_ID As String
            Public cTable As ConnectOneWS.Tables
        End Class
        <Serializable>
        Public Class Param_GetBankTransSumAmount_Common
            Public BranchID As String
            Public Type As String
        End Class
        <Serializable>
        Public Class Param_GetOpeningBalance_Common
            Public open_Year_Sdt As DateTime
            Public Date_Format_Current As String
        End Class
        <Serializable>
        Public Class Param_IsRecordCarriedForward
            Public cTable As Real.ConnectOneWS.Tables
            Public recYearID As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_GetYearPeriod
            Public YrStartDate As DateTime
            Public YrEndDate As DateTime
        End Class
#End Region

        'Public Shared Function Get_Asset_Item_Closing_Detail(ByVal Param As Param_Asset_Common, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim SPName As String = "Get_Asset_Item_Closing_Detail"
        '    Dim params() As String = {"X_CEN_ID", "X_YEAR_ID", "X_ASSET_ID", "X_PROFILE", "X_M_ID"}
        '    Dim values() As Object = {Param.CEN_ID, Param.YEAR_ID, Param.ASSET_ID, Param.PROFILE, Param.M_ID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
        '    Dim lengths() As Integer = {5, 4, 36, 50, 36}
        '    Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_INFO, SPName, ConnectOneWS.Tables.ASSET_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        'End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, called by Data_GetOpeningBalance
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="RecIDColHeader"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetOpeningBalance</remarks>
        Public Shared Function GetOpeningBalance(ByVal RecIDColHeader As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT OP_AMOUNT,REC_ID AS " & RecIDColHeader & " FROM Opening_Balances_Info " &
                                " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & " ; "
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, called by Data_GetOpeningBalance_Common
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetOpeningBalance_Common</remarks>
        Public Shared Function GetOpeningBalance_Common(inBasicParam As ConnectOneWS.Basic_Param, ByVal param As Param_GetOpeningBalance_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Profile_Cash
                    'Query = " SELECT '" & Format(DateAdd(DateInterval.Day, -1, param.open_Year_Sdt), param.Date_Format_Current) & "' as Date,OP_AMOUNT,REC_ID AS ID  ," & Common.Remarks_Detail("Opening_Balances_Info", GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Opening_Balances_Info") & "" &
                    '             " FROM Opening_Balances_Info " &
                    '             " Where   REC_STATUS IN (0,1,2) AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & " AND OP_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & "  AND  REC_ID LIKE '%CASH%' ; "
                    Dim SPName As String = "sp_get_Cash_Profile"
                    Dim params() As String = {"CENID", "YEARID", "@UserID"}
                    Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
                    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
                    Dim lengths() As Integer = {4, 4, 255}
                    Return dbService.ListFromSP(ConnectOneWS.Tables.OPENING_BALANCES_INFO, SPName, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)

                Case ConnectOneWS.ClientScreen.Profile_Report
                    Query = " SELECT '" & Format(DateAdd(DateInterval.Day, -1, param.open_Year_Sdt), param.Date_Format_Current) & "' as Date,OP_AMOUNT FROM Opening_Balances_Info " &
                                 " Where   REC_STATUS IN (0,1,2) AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & " AND OP_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " AND  REC_ID LIKE '%CASH%' ; "
                    'Each calling function Query shall be added here, calling function need not provide the query, it will be identified by ClientScreen
                    Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
            End Select

        End Function

        ''' <summary>
        ''' Returns opening Balance for Current Center, called by Data_GetOpeningBalance
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetOpeningBalanceRecIDWithCustomColHeadName</remarks>
        Public Shared Function GetOpeningBalance(ByVal Param As Param_GetOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT OP_AMOUNT AS " & Param.RecIDColHeader &
                              " FROM Opening_Balances_Info " &
                              " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & " and REC_ID='" & Param.AccountRecID & "'; "
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns opening Balance Rows count For a RecID, called by Data_GetOpeningBalanceRowCount
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetOpeningBalanceRowCount</remarks>
        Public Shared Function GetOpeningBalanceRowCount(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) FROM Opening_Balances_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UPPER(REC_ID)='" & RecID & "' AND  OP_CEN_ID =" & inBasicParam.openCenID.ToString & " "
            Return dbService.GetScalar(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Cash Opening Balance for Open Center, called by Data_GetCashOpeningBalanceAmount
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Data_GetCashOpeningBalanceAmount</remarks>
        Public Shared Function GetCashOpeningBalanceAmount(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT OP_AMOUNT FROM Opening_Balances_Info " &
                                        " Where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND  REC_ID LIKE '%CASH%' ; "
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to get Cash, Bank Cr , Dr Sum since a particular date, called by Data_GetCashBankTransSumAmount
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetCashBankTransSumAmount, Param_GetCashBankTransSumAmount</remarks>
        Public Shared Function GetCashBankTransSumAmount(ByVal FromDate As Date, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT   SUM(CASE WHEN TR_DR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS R_CASH," &
                                    "       SUM( CASE WHEN TR_CR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS P_CASH," &
                                    "        SUM( CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS R_BANK," &
                                    "        SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS P_BANK" &
                                    " FROM Transaction_Info " &
                                    " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   " &
                                    " AND CAST(TR_DATE AS DATE) < '" & Format(FromDate, Common.Server_Date_Format_Short) & "' "
            '" AND CAST(TR_DATE AS DATE) < '" & Format(Param.FromDate, Param.ServerDateFormatShort) & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to get Cash Cr , Dr Sum, called by Data_GetCashTransSumAmount
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetCashTransSumAmount</remarks>
        Public Shared Function GetCashTransSumAmount(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT   SUM( CASE WHEN TR_DR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS R_CASH," &
                                    "       SUM( CASE WHEN TR_CR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS P_CASH" &
                                    " FROM Transaction_Info " &
                                    " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to get Bank Sum with custom Query, called by Data_GetBankTransSumAmount_Common
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetBankTransSumAmount_Common</remarks>
        Public Shared Function GetBankTransSumAmount_Common(inBasicParam As ConnectOneWS.Basic_Param, ByVal inParam As Param_GetBankTransSumAmount_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    If inParam.Type = "1" Then
                        Query = " SELECT SUM( CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS R_BANK," &
                                 "        SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS P_BANK" &
                                 " FROM Transaction_Info " &
                                 " Where  TR_CR_LED_ID <> TR_DR_LED_ID AND  REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   AND (TR_SUB_CR_LED_ID='" & inParam.BranchID & "' OR TR_SUB_DR_LED_ID='" & inParam.BranchID & "'); "
                    ElseIf inParam.Type = "2" Then
                        Query = " SELECT SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS P_BANK " &
                                 " FROM Transaction_Info " &
                                 " Where  TR_CR_LED_ID = TR_DR_LED_ID AND   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "    AND TR_SUB_CR_LED_ID='" & inParam.BranchID & "' ; "
                    ElseIf inParam.Type = "3" Then
                        Query = " SELECT SUM( CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS R_BANK " &
                                 " FROM Transaction_Info " &
                                 " Where  TR_CR_LED_ID = TR_DR_LED_ID AND   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "    AND TR_SUB_DR_LED_ID='" & inParam.BranchID & "' ; "
                    End If
                    'Each calling function Query shall be added here, calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to get Cash, Bank Cr , Dr Sum between two particular dates, called by Data_GetCashBankTransSumAmount
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Data_GetCashBankTransSumAmountBetweenTwoDates</remarks>
        Public Shared Function GetCashBankTransSumAmount(ByVal Param As Param_GetCashBankTransSumAmountTwoDates, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT   SUM( CASE WHEN TR_DR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS R_CASH," &
                                    "       SUM( CASE WHEN TR_CR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS P_CASH," &
                                    "        SUM( CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS R_BANK," &
                                    "        SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS P_BANK" &
                                    " FROM Transaction_Info " &
                                    " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   " &
                                    " AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' "
            '" AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Param.ServerDateFormatShort) & "' AND '" & Format(Param.ToDate, Param.ServerDateFormatShort) & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Total Bank Balance for selected Bank IDs, called by Data_GetBankBalanceAmount
        ''' </summary>
        ''' <param name="BankIDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetBankBalanceAmount</remarks>
        Public Shared Function GetBankBalanceAmount(ByVal BankIDs As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT SUM(OP_AMOUNT) AS BALANCE FROM Opening_Balances_Info " &
                                    " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND  REC_ID in (" & BankIDs & ")"
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Bank Balance for selected Bank IDs, called by Data_GetBankBalanceAmountIdWise
        ''' </summary>
        ''' <param name="BankIDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetBankBalanceAmountIdWise</remarks>
        Public Shared Function GetBankBalanceAmountIdWise(ByVal BankIDs As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT REC_ID,SUM(OP_AMOUNT) AS BALANCE FROM Opening_Balances_Info " &
                                    " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND  REC_ID in (" & BankIDs & ") GROUP BY REC_ID ORDER BY REC_ID"
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Add Opening Balance, called by Data_AddOpeningBalance
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks> RealServiceFunctions.Data_AddOpeningBalance, Param_AddOpeningBalance</remarks>
        Public Shared Function AddOpeningBalance(ByVal Param As Param_AddOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Opening_Balances_Info(OP_CEN_ID,OP_COD_YEAR_ID,OP_AMOUNT," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,OP_ORG_REC_ID" &
                                                 ") VALUES(" &
                                                 "" & inBasicParam.openCenID.ToString & "," & Param.openYearID.ToString & "," & Param.Amount & "," &
                                                 "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Param.RecID & "', '" & Param.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.OPENING_BALANCES_INFO, OnlineQuery, inBasicParam, Param.RecID)
            Return True
        End Function

        ''' <summary>
        ''' Common Function to Update Opening Balance, called by Data_UpdateOpeningBalance, Param_UpdateOpeningBalance
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <remarks>RealServiceFunctions.Data_UpdateOpeningBalance</remarks>
        Public Shared Function UpdateOpeningBalance(ByVal Param As Param_UpdateOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Opening_Balances_Info SET " &
                                                 "OP_AMOUNT       =" & Param.Amount & ", " &
                                                 " " &
                                                 "REC_EDIT_ON       ='" & Now.ToString(Common.Server_Date_Format_Long) & "'," &
                                                 "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                                 "  WHERE REC_ID    ='" & Param.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.OPENING_BALANCES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Common Function to Get Used PartyIDs from various Tables, called by Data_GetPartyIDList
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetPartyIDList </remarks>
        Public Shared Function GetPartyIDList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT  DISTINCT AI_PARTY_ID AS PARTY_ID FROM ADVANCES_INFO " &
                                       " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                       " UNION ALL " &
                                       " SELECT  DISTINCT LI_PARTY_ID AS PARTY_ID FROM LIABILITIES_INFO " &
                                       " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                       " UNION ALL " &
                                       " SELECT  DISTINCT DI_PARTY_ID AS PARTY_ID FROM DEPOSITS_INFO " &
                                       " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND DI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LEN(DI_PARTY_ID) > 0 " &
                                       " UNION ALL " &
                                       " SELECT  DISTINCT VI_OWNERSHIP_AB_ID AS PARTY_ID FROM VEHICLES_INFO " &
                                       " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LEN(VI_OWNERSHIP_AB_ID) > 0 "
            Return dbService.List(ConnectOneWS.Tables.ADVANCES_INFO, OnlineQuery, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Used ItemIDs from various Tables, called by Data_GetUsedItemIDList
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Data_GetUsedItemIDList</remarks>
        Public Shared Function GetUsedItemIDList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  DISTINCT GS_ITEM_ID AS ITEM_ID FROM Gold_Silver_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND GS_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT AI_ITEM_ID AS ITEM_ID FROM ASSET_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT VI_ITEM_ID AS ITEM_ID FROM VEHICLES_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT LS_ITEM_ID AS ITEM_ID FROM LIVE_STOCK_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LS_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT DI_ITEM_ID AS ITEM_ID FROM Deposits_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND DI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT AI_ITEM_ID AS ITEM_ID FROM Advances_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " " &
                                        " UNION ALL " &
                                        " SELECT  DISTINCT LI_ITEM_ID AS ITEM_ID FROM Liabilities_Info " &
                                        " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LI_CEN_ID=" & inBasicParam.openCenID.ToString & " "

            Return dbService.List(ConnectOneWS.Tables.GOLD_SILVER_INFO, Query, ConnectOneWS.Tables.GOLD_SILVER_INFO.ToString(), inBasicParam)
        End Function

        'RealServiceFunctions.Data_GetLastEditOn
        Public Shared Function GetLastEditOn(ByVal Param As Param_GetLastEditOn, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT REC_EDIT_ON FROM " & Param.cTable.ToString & " WHERE REC_ID ='" & Param.Rec_ID & "' AND REC_STATUS IN (0,1,2)"
            If inBasicParam.screen = ConnectOneWS.ClientScreen.Profile_Membership And Param.cTable = ConnectOneWS.Tables.ADDRESS_BOOK Then
                Query = " SELECT REC_EDIT_ON FROM " & Param.cTable.ToString & " WHERE C_ORG_REC_ID ='" & Param.Rec_ID & "' AND C_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND REC_STATUS IN (0,1,2)"
            End If
            Return dbService.GetScalar(Param.cTable, Query, Param.cTable.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Server Current DateTime, Copied from common region in DBOps
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Data_GetCurrentDateTime</remarks>
        Public Shared Function GetCurrentDateTime(inBasicParam As ConnectOneWS.Basic_Param) As DateTime
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT GETDATE() FROM SO_LAST_USER_SESSION "
            Return dbService.GetScalar(ConnectOneWS.Tables.SO_LAST_USER_SESSION, Query, ConnectOneWS.Tables.SO_LAST_USER_SESSION.ToString(), inBasicParam)
        End Function

        'Checks whether a record is Carried Forward from Last year  
        Public Shared Function IsRecordCarriedForward(param As Param_IsRecordCarriedForward, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT "
            Select Case param.cTable
                Case ConnectOneWS.Tables.ADVANCES_INFO
                    Query += "AI_ORG_REC_ID"
                Case ConnectOneWS.Tables.ASSET_INFO
                    Query += "AI_ORG_REC_ID"
                Case ConnectOneWS.Tables.LAND_BUILDING_INFO
                    Query += "LB_ORG_REC_ID"
                Case ConnectOneWS.Tables.LIABILITIES_INFO
                    Query += "LI_ORG_REC_ID"
                Case ConnectOneWS.Tables.DEPOSITS_INFO
                    Query += "DI_ORG_REC_ID"
                Case ConnectOneWS.Tables.VEHICLES_INFO
                    Query += "VI_ORG_REC_ID"
                Case ConnectOneWS.Tables.OPENING_BALANCES_INFO
                    Query += "OP_ORG_REC_ID"
                Case ConnectOneWS.Tables.BANK_ACCOUNT_INFO
                    Query += "BA_ORG_REC_ID"
                Case ConnectOneWS.Tables.FD_INFO
                    Query += "FD_ORG_REC_ID"
                Case ConnectOneWS.Tables.OTHER_PROFILE_INFO
                    Query += "OP_ORG_REC_ID"
                Case ConnectOneWS.Tables.GOLD_SILVER_INFO
                    Query += "GS_ORG_REC_ID"
                Case ConnectOneWS.Tables.LIVE_STOCK_INFO
                    Query += "LS_ORG_REC_ID"
                Case ConnectOneWS.Tables.STOCK_INFO
                    Query += "STOCK_ORG_REC_ID"
                Case ConnectOneWS.Tables.SERVICE_PLACE_INFO
                    Query += "SP_ORG_REC_ID"
                Case ConnectOneWS.Tables.COMPLEX_INFO
                    Query += "CM_ORG_REC_ID"
                Case ConnectOneWS.Tables.WIP_INFO
                    Query += "WIP_ORG_REC_ID"
            End Select
            Query += " FROM " & param.cTable.ToString() & " WHERE REC_ID = '" & param.RecID & "'"
            Dim Org_Rec_ID As Object = dbService.GetScalar(param.cTable, Query, param.cTable.ToString(), inBasicParam)
            If Org_Rec_ID Is Nothing Then Return False
            If Org_Rec_ID = param.RecID And param.recYearID = inBasicParam.openYearID Then Return False
            '   If  param.recYearID = inBasicParam.openYearID  Then Return False
            Return True
        End Function

        Public Shared Function GetCommonParamsById(Table As ConnectOneWS.Tables, RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY FROM " & Table.ToString() & " WHERE REC_ID = '" & RecID & "'"
            Return dbService.List(Table, Query, Table.ToString(), inBasicParam)
        End Function

        Public Shared Function IsTBImportedCentre(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) AS CNT FROM so_trial_balance WHERE CEN_ID = " & inBasicParam.openCenID.ToString & " AND YEAR_ID = (SELECT MAX(COD_YEAR_ID) FROM cod_info WHERE REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & " AND COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " AND COD_YEAR_ID <" & inBasicParam.openYearID.ToString & ")"
            Dim cnt As Object = dbService.GetScalar(ConnectOneWS.Tables.SO_TRIAL_BALANCE, Query, ConnectOneWS.Tables.SO_TRIAL_BALANCE.ToString(), inBasicParam)
            If cnt > 0 Then Return True
            Return False
        End Function

        Public Shared Function IsMultiuserAllowed(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Return True
        End Function

        Public Shared Function IsInsuranceAuditor(UserId As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim Array = ConfigurationManager.AppSettings("InsuranceAuditor").Split(",")
            For Each Str As String In Array
                If Str.ToLower() = UserId.ToLower() Then Return True
            Next
            Return False
        End Function

        Public Shared Function IsInsuranceAudited(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(AV_CEN_ID) FROM so_audit_verifications  AS SAV WHERE AV_VERIFICATION_MISC_ID = '87D5B255-ED66-423A-90F9-BE48D52C7539' and AV_YEAR_ID = " & inBasicParam.openYearID.ToString & " and AV_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            Dim I As Integer = dbService.GetScalar(ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS, Query, ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS.ToString(), inBasicParam)
            If I > 0 Then Return True
            Return False
        End Function

        ''' <summary>
        ''' Get Audit Exception List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.News_GetList</remarks>
        Public Shared Function GetAuditExceptionList(inBasicParam As ConnectOneWS.Basic_Param, Optional ShowBlockRegistrationOnly As Boolean = False) As DataSet

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "USERID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 255}
            Dim Ds As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_Get_Audit_Exceptions", paramters, values, dbTypes, lengths, inBasicParam)

            Dim dTable As DataTable = New DataTable
            dTable.Columns.Add("Priority", Type.GetType("System.String")) : dTable.Columns.Add("ID", Type.GetType("System.Int32")) : dTable.Columns.Add("Name", Type.GetType("System.String"))
            'dTable.Columns.Add("AddedOn", Type.GetType("System.String"))
            dTable.TableName = "MainTable"
            Ds.Tables.Add(dTable)

            Dim ctr As Int32 = 0
            For Each cTable As DataTable In Ds.Tables
                If ctr = 0 Then cTable.TableName = "Catalog" : ctr += 1 : Continue For
                If cTable.TableName = "MainTable" Then ctr += 1 : Continue For
                If cTable.Rows.Count > 0 Then
                    Dim _var As String = ""
                    If Ds.Tables("Catalog").Rows(ctr - 1)("Blocks Audit Registration").ToString().ToLower() = "true" Or ShowBlockRegistrationOnly = False Then
                        Dim dRow2 As DataRow = Ds.Tables("MainTable").NewRow
                        dRow2("Priority") = Ds.Tables(0).Rows(ctr - 1)("Priority")
                        dRow2("ID") = Ds.Tables(0).Rows(ctr - 1)("ID")
                        dRow2("Name") = Ds.Tables(0).Rows(ctr - 1)("Name") + _var
                        'dRow2("AddedOn") = Ds.Tables(0).Rows(ctr - 1)("AddedOn")
                        Ds.Tables("MainTable").Rows.Add(dRow2)
                        Ds.Relations.Add(Ds.Tables("Catalog").Rows(ctr - 1)("ID").ToString(), Ds.Tables("MainTable").Columns("ID"), Ds.Tables(ctr).Columns("ID"))
                    End If
                End If
                ctr += 1
            Next
            Return Ds

        End Function

        Public Shared Function GetClientAuthorizations(LoggedInUserTtype As String, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "sp_get_Client_Authorization"
            Dim params() As String = {"USER_ID", "YEAR_ID", "CEN_ID", "@LoggedInUserTtype"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openYearID, inBasicParam.openCenID, LoggedInUserTtype}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 4, 50}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetDynamicClientRestriction(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "sp_get_Dynamic_Client_Restriction"
            Dim params() As String = {"USER_ID", "YEAR_ID", "CEN_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openYearID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam).Rows(0)(0)
        End Function

        Public Shared Function GetAuditedPeriod(inparam As Param_GetYearPeriod, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.GetRestrictedPeriod(inBasicParam.openCenID, "", inparam.YrStartDate, inparam.YrEndDate)
        End Function
        Public Shared Function GetAccountsSubmittedPeriod(inparam As Param_GetYearPeriod, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.GetAccountSubmittedPeriod(inBasicParam.openCenID, "", inparam.YrStartDate, inparam.YrEndDate)
        End Function
    End Class
End Namespace
