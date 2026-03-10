Imports System.Data
Imports ConnectOne
Namespace Real
#Region "Reports"
    <Serializable>
    Public Class Reports_All
#Region "Param Classes"
        <Serializable>
        Public Class Param_ReportsAll_GetTransactionList
            Public Fr_Date As Date
            Public To_Date As Date
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetConstructionExpensesList
            Inherits Param_ReportsAll_GetTransactionList
            'Same params
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetGiftTransactionList
            Inherits Param_ReportsAll_GetTransactionList
            Public openYearID As String
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetCollectionBoxTransactionList
            Inherits Param_ReportsAll_GetTransactionList
            'Same Params
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetAddresses
            Public ABID As String
            Public Locked As Boolean
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetTSummaryList
            Public IsReceipt As Boolean
            Public FrDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetMembershipReceipt
            Public openYearSdt As Date
            Public M_ID As String
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetTelephoneBill
            Public TP_ID As String
            Public FromDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_ReportsAll_GetTrialBalReport
            Public ZoneId As Object
            Public InsttId As Object
            Public FrDate As Date
            Public ToDate As Date
            Public openYearSdt As Date
            Public specialVoucherReference As String
        End Class
        <Serializable>
        Public Class Param_GetInsuranceLetterData
            Public YearID As Integer
            Public BK_PAD_NO As String
        End Class
        <Serializable>
        Public Class Param_UpdateClearingDate
            Public ClearingDate As DateTime = DateTime.MinValue
            Public iTrMID As String = Nothing
            Public iRecID As String = Nothing
            Public iRefNo As String = Nothing
            Public Mode As String = Nothing
            Public TxnDate As DateTime = Nothing
        End Class
        <Serializable>
        Public Class Param_GetMagazineReceipt
            Public Tr_m_ID As String
            Public Prev_Year_ID As Integer
        End Class
#End Region
        ''' <summary>
        ''' Get Donation Status ID
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetDonationStatusID</remarks>
        Public Shared Function GetDonationStatusID(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DS_STATUS_MISC_ID from donation_status_info Where REC_STATUS IN (0,1,2) and ds_tr_id = '" & RecID & "' ;"
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, Query, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transaction List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTransactionList</remarks>
        Public Shared Function GetTransactionList(ByVal Param As Param_ReportsAll_GetTransactionList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_ITEM_ID, tr_date AS TrDate, TR_VNO AS VNO, TR_AMOUNT AS Amt,item_name AS Particulars, CASE WHEN ITEM_TRANS_TYPE = 'DEBIT' THEN 'Expense' else 'Receipt' END AS Type, LED_NAME AS Head FROM TRANSACTION_INFO AS TI INNER JOIN ITEM_INFO AS II ON TR_ITEM_ID = II.REC_ID INNER JOIN ACC_LEDGER_INFO AS AL ON ITEM_LED_ID = LED_ID WHERE tr_CEN_ID = " & inBasicParam.openCenID.ToString & "" & _
                     " AND TI.REC_STATUS IN (0,1,2) AND (CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.Fr_Date, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.To_Date, Common.Server_Date_Format_Short) & "') ORDER BY TR_DATE"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Construction and WIP List for statement
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetConstructionWIPExpensesList(ByVal Param As Param_ReportsAll_GetConstructionExpensesList, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Fr_Date.ToString(Common.Server_Date_Format_Short), Param.To_Date.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.[String], DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 20, 20}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Construction_WIP_Expenses", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        ''' <summary>
        ''' Get Construction for Report
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetConstructionExpensesList(ByVal inParam As Reports_All.Param_ReportsAll_GetConstructionExpensesList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Fr_Date.ToString(Common.Server_Date_Format_Short), inParam.To_Date.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_Construction_Expenses", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Gift Transaction List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetGiftTransactionList</remarks>
        Public Shared Function GetGiftTransactionList(ByVal Param As Param_ReportsAll_GetGiftTransactionList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COALESCE(TR_DR_LED_ID,'JOURNAL') as TR_DR_LED_ID,TR_REF_BANK_ID,TR_ITEM_ID,CASE WHEN  ti.tr_code =14 AND ('00182' in (select TR_CR_LED_ID FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID) OR '00182' in (select TR_DR_LED_ID FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID) " & _
                       ") THEN (SELECT TOP 1 TR_AB_ID_1 FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID AND TR_AB_ID_1 IS NOT NULL) ELSE TR_AB_ID_1 END TR_AB_ID_1,TR_CEN_ID,TR_COD_YEAR_ID ," & _
                       " TR_DATE,CASE WHEN TR_CODE = 14 AND TR_DR_LED_ID IS NULL THEN -1 * TR_AMOUNT ELSE TR_AMOUNT END AS TR_AMOUNT,TR_MODE,TR_REF_NO,TR_REF_BRANCH,TR_REF_DATE,ti.REC_ID FROM transaction_info AS ti " & _
                       " Where ti.REC_STATUS IN (0,1,2) AND ((ti.TR_CODE IN (5,6,7) AND TR_ITEM_ID <> 'd0a33061-d679-4f21-ac12-a29541de8fcb') OR (ti.tr_code =14 AND ('00182' in (select TR_CR_LED_ID FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID) OR '00182' in (select TR_DR_LED_ID FROM transaction_info WHERE TR_M_ID = TI.TR_M_ID)) AND (COALESCE(ti.TR_CR_LED_ID,'') <> '00182' AND COALESCE(ti.TR_DR_LED_ID,'') <> '00182'))) " & _
                       " AND ti.TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND ti.TR_COD_YEAR_ID =" & Param.openYearID.ToString & " " & _
                       "AND CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.Fr_Date, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.To_Date, Common.Server_Date_Format_Short) & "'  order by TR_DATE ;"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get CollectionBox Transaction List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionList</remarks>
        Public Shared Function GetCollectionBoxTransactionList(ByVal Param As Param_ReportsAll_GetCollectionBoxTransactionList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT dbo.fn_FORMATDATE(tr_date, 'dd-mon-yyyy') AS tr_date, AB_1.C_NAME AS 'Centre-in-charge Name', AB_2.C_NAME AS 'Second Surrendered Person Name', tr_amount AS Amt, TR_AB_ID_1, TR_AB_ID_2 FROM transaction_info ti" &
                                        " LEFT OUTER JOIN ADDRESS_BOOK AS AB_1 ON TR_AB_ID_1 = AB_1.REC_ID " &
                                        " LEFT OUTER JOIN ADDRESS_BOOK AS AB_2 ON TR_AB_ID_2 = AB_2.REC_ID " &
                                        " WHERE tr_CEN_ID = " & inBasicParam.openCenID.ToString & "" &
                                        " AND TR_CODE = 9 AND ti.REC_STATUS IN (0,1,2) AND (CAST(TR_DATE AS DATE) BETWEEN '" & Format(Param.Fr_Date, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.To_Date, Common.Server_Date_Format_Short) & "')  ORDER BY CAST(TR_DATE AS DATE)"

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get CollectionBox Transaction List
        ''' </summary>
        ''' <param name="RecId"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionListWithRecID</remarks>
        Public Shared Function GetCollectionBoxTransactionList(ByVal RecId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT tr_amount AS Amt, dbo.fn_FORMATDATE(tr_date, 'dd-mon-yyyy') AS tr_date, TR_AB_ID_1, TR_AB_ID_2, TR_VNO, TR_NARRATION FROM transaction_info WHERE tr_CEN_ID = " & inBasicParam.openCenID.ToString & "" &
                                       " AND TR_CODE = 9 AND REC_STATUS IN (0,1,2) AND REC_ID = '" & RecId & "'"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Addresses
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetAddresses</remarks>
        Public Shared Function GetAddresses(ByVal Param As Param_ReportsAll_GetAddresses, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END AS C_NAME ,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_STATE_ID,C_R_COUNTRY_ID,C_R_DISTRICT_ID,C_R_PINCODE FROM address_book WHERE REC_ID ='" & Param.ABID & "' AND REC_STATUS IN (0,1,2);"
            If Param.Locked Then
                Query = "SELECT C_NAME AS C_NAME ,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_STATE_ID,C_R_COUNTRY_ID,C_R_DISTRICT_ID,C_R_PINCODE FROM donation_receipt_address_book WHERE C_TR_ID ='" & Param.ABID & "'  AND REC_STATUS IN (0,1,2);"
            End If
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get TSummary List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTSummaryList</remarks>
        Public Shared Function GetTSummaryList(ByVal Param As Param_ReportsAll_GetTSummaryList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Common.UseSQL() Then
                Return GetTSummaryList_SQL(Param, inBasicParam)
            End If
            Dim PaymentQuery As String = "SELECT " & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.Type END AS IType, " & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.Amount END AS IAmount, " & _
                                            "CASE WHEN B.DontUse IS NULL THEN B.GroupSum ELSE '' END AS IGroupSum ,  " & _
                                            "CASE WHEN B.DontUse IS NULL AND B.GROUP IS NULL THEN -1 ELSE CCODE END AS CC " & _
                                            "FROM " & _
                                            "(SELECT DISTINCT  " & _
                                            "A.GROUP,A.Item AS DontUse,	 " & _
                                            "CASE WHEN A.ITEM IS NULL THEN A.Group ELSE A.item END AS ItemFinal, " & _
                                            "A.TYPE, A.AMOUNT, " & _
                                            "SUM(A.Amount) AS GroupSum , CCODE " & _
                                            "FROM " & _
                                            "(SELECT 0 AS SrNo, SG_NAME AS 'Group', " & _
                                            "DR_AI.LED_NAME AS Item, " & _
                                            "'PAYMENTS' AS 'TYPE',  " & _
                                            "SUM(TR_AMOUNT) AS Amount ,  " & _
                                            "CASE WHEN dr_ai.LED_TYPE = 'LIABILITY' THEN 3 WHEN dr_ai.LED_TYPE = 'INCOME' THEN 2 WHEN dr_ai.LED_TYPE = 'ASSET' THEN 1 WHEN dr_ai.LED_TYPE = 'EXPENSE' THEN 0  END AS CCODE  " & _
                                            "FROM transaction_info AS ti  " & _
                                            "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                            "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
                                            "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
                                            "WHERE ti.REC_STATUS <> - 1 AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE NOT IN (5, 6, 1, 2) AND ITEM_TRANS_TYPE = 'DEBIT'  " & _
                                            "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                            "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
                                            "UNION ALL " & _
                                            "SELECT  " & _
                                            "0 AS SrNo,SG_NAME AS 'Group',CONCAT(item_name, ' BY ', TR_MODE) AS Item, " & _
                                            "'PAYMENTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
                                            "CASE WHEN dr_ai.LED_TYPE = 'LIABILITY' THEN 3 WHEN dr_ai.LED_TYPE = 'INCOME' THEN 2 WHEN dr_ai.LED_TYPE = 'ASSET' THEN 1 WHEN dr_ai.LED_TYPE = 'EXPENSE' THEN 0  END AS CCODE  " & _
                                            "FROM transaction_info AS ti  " & _
                                            "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                            "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
                                            "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
                                            "WHERE (ti.REC_STATUS <> - 1) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (5, 6) AND ITEM_TRANS_TYPE = 'DEBIT'  " & _
                                            "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                            "GROUP BY item_name,ITEM_TRANS_TYPE,SG_NAME,TR_MODE " & _
                                            ")AS A  " & _
                                            "GROUP BY A.GROUP,A.ITEM WITH ROLLUP) AS B  " & _
                                            "ORDER BY cc, b.group,b.dontuse "
            Dim ReceiptQuery As String = "SELECT " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.Type END AS IType, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.Amount END AS IAmount, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN B.GroupSum ELSE '' END AS IGroupSum ,  " & _
                                          "CASE WHEN B.DontUse IS NULL AND B.GROUP IS NULL THEN -1 ELSE CCODE END AS CC " & _
                                          "FROM " & _
                                          "(SELECT DISTINCT  " & _
                                          "A.GROUP,A.Item AS DontUse,	 " & _
                                          "CASE WHEN A.ITEM IS NULL THEN A.Group ELSE A.item END AS ItemFinal, " & _
                                          "A.TYPE, A.AMOUNT, " & _
                                          "SUM(A.Amount) AS GroupSum , CCODE " & _
                                          "FROM " & _
                                          "(SELECT 0 AS SrNo,SG_NAME AS 'Group', " & _
                                          "CR_AI.LED_NAME AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE', SUM(TR_AMOUNT) AS Amount ,  " & _
                                          "CASE WHEN Cr_ai.LED_TYPE = 'LIABILITY' THEN 2 WHEN Cr_ai.LED_TYPE = 'INCOME' THEN 3 WHEN Cr_ai.LED_TYPE = 'ASSET' THEN 0 WHEN Cr_ai.LED_TYPE = 'EXPENSE' THEN 1  END AS CCODE  " & _
                                          "FROM transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE ti.REC_STATUS <> - 1 AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE NOT IN (5, 6, 1, 2, 11) AND ITEM_TRANS_TYPE <> 'DEBIT'  " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
                                          "UNION ALL " & _
                                          "SELECT  " & _
                                          "0 AS SrNo,SG_NAME AS 'Group',CONCAT(item_name, ' BY ', TR_MODE) AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
                                          "CASE WHEN Cr_ai.LED_TYPE = 'LIABILITY' THEN 2 WHEN Cr_ai.LED_TYPE = 'INCOME' THEN 3 WHEN Cr_ai.LED_TYPE = 'ASSET' THEN 0 WHEN Cr_ai.LED_TYPE = 'EXPENSE' THEN 1  END AS CCODE   " & _
                                          "FROM " & _
                                          "transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "LEFT OUTER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE (ti.REC_STATUS <> - 1) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (5, 6) AND ITEM_TRANS_TYPE <> 'DEBIT'  " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY item_name,ITEM_TRANS_TYPE,SG_NAME,TR_MODE " & _
                                          "UNION ALL  " & _
                                          "SELECT 0 AS SrNo,'Sale of Assets' AS 'Group', " & _
                                          "CR_AI.LED_NAME AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE', SUM(TR_AMOUNT) AS Amount ,  " & _
                                          "CASE WHEN Cr_ai.LED_TYPE = 'LIABILITY' THEN 2 WHEN Cr_ai.LED_TYPE = 'INCOME' THEN 3 WHEN Cr_ai.LED_TYPE = 'ASSET' THEN 0 WHEN Cr_ai.LED_TYPE = 'EXPENSE' THEN 1  END AS CCODE  " & _
                                          "FROM transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE ti.REC_STATUS <> - 1 AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (11) AND ITEM_TRANS_TYPE <> 'DEBIT' 	 " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
                                          ")AS A  " & _
                                          "GROUP BY A.GROUP,A.ITEM WITH ROLLUP) AS B  " & _
                                          "ORDER BY cc, b.group,b.dontuse "

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, IIf(Param.IsReceipt, ReceiptQuery, PaymentQuery), ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTSummaryList_SQL(ByVal Param As Param_ReportsAll_GetTSummaryList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim PaymentQuery As String = "SELECT " & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.TYPES END AS IType," & _
                                            "CASE WHEN B.DontUse IS NULL THEN '' ELSE CAST(B.Amount AS VARCHAR) END AS IAmount," & _
                                            "CASE WHEN B.DontUse IS NULL THEN CAST(B.GroupSum AS VARCHAR) ELSE '' END AS IGroupSum , " & _
                                            "CASE WHEN B.DontUse IS NULL AND B.Groups IS NULL THEN -1 ELSE CCODE END AS CC " & _
                                            "FROM " & _
                                            "(SELECT DISTINCT  " & _
                                            "A.Groups,A.Item AS DontUse," & _
                                            "CASE WHEN A.ITEM IS NULL THEN A.Groups ELSE A.item END AS ItemFinal, " & _
                                            "MAX(A.TYPES) AS TYPES, SUM(A.AMOUNT) AS AMOUNT, " & _
                                            "SUM(A.Amount) AS GroupSum , MAX(CCODE) AS CCODE " & _
                                            "FROM " & _
                                            "(SELECT 0 AS SrNo, SG_NAME AS 'Groups'," & _
                                            "DR_AI.LED_NAME AS Item, " & _
                                            "'PAYMENTS' AS 'TYPES',  " & _
                                            "SUM(TR_AMOUNT) AS Amount ,  " & _
                                            "CASE WHEN max(dr_ai.LED_TYPE) = 'LIABILITY' THEN 3 WHEN max(dr_ai.LED_TYPE) = 'INCOME' THEN 2 WHEN max(dr_ai.LED_TYPE) = 'ASSET' THEN 1 WHEN max(dr_ai.LED_TYPE) = 'EXPENSE' THEN 0  END AS CCODE  " & _
                                            "FROM transaction_info AS ti  " & _
                                            "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                            "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
                                            "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
                                            "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE NOT IN (5, 6, 1, 2) AND ti.TR_TYPE = 'DEBIT'  " & _
                                            "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                            "GROUP BY LED_NAME, ti.TR_TYPE,SG_NAME  " & _
                                            "UNION ALL " & _
                                            "SELECT  " & _
                                            "0 AS SrNo,SG_NAME AS 'Groups',item_name+ ' BY '+ TR_MODE AS Item, " & _
                                            "'PAYMENTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
                                            "CASE WHEN max(dr_ai.LED_TYPE) = 'LIABILITY' THEN 3 WHEN max(dr_ai.LED_TYPE) = 'INCOME' THEN 2 WHEN max(dr_ai.LED_TYPE) = 'ASSET' THEN 1 WHEN max(dr_ai.LED_TYPE) = 'EXPENSE' THEN 0  END AS CCODE  " & _
                                            "FROM transaction_info AS ti  " & _
                                            "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                            "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
                                            "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
                                            "WHERE (ti.REC_STATUS IN (0,1,2)) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (5, 6) AND ti.TR_TYPE = 'DEBIT'  " & _
                                            "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                            "GROUP BY item_name,ti.TR_TYPE,SG_NAME,TR_MODE " & _
                                            ")AS A  " & _
                                            "GROUP BY A.Groups,A.ITEM WITH ROLLUP) AS B  " & _
                                            "ORDER BY cc, b.Groups,b.dontuse "
            Dim ReceiptQuery As String = "SELECT " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.TYPES END AS IType, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN '' ELSE CAST(B.Amount AS VARCHAR) END AS IAmount, " & _
                                          "CASE WHEN B.DontUse IS NULL THEN CAST( B.GroupSum AS VARCHAR) ELSE '' END AS IGroupSum ,  " & _
                                          "CASE WHEN B.DontUse IS NULL AND B.GROUPS IS NULL THEN -1 ELSE CCODE END AS CC " & _
                                          "FROM " & _
                                          "(SELECT DISTINCT  " & _
                                          "A.GROUPS,A.Item AS DontUse,	 " & _
                                          "CASE WHEN A.ITEM IS NULL THEN A.GROUPS ELSE A.item END AS ItemFinal, " & _
                                          "MAX(A.TYPES) AS TYPES, SUM(A.AMOUNT) AS AMOUNT, " & _
                                          "SUM(A.Amount) AS GroupSum , MAX(CCODE) AS CCODE " & _
                                          "FROM " & _
                                          "(SELECT 0 AS SrNo,SG_NAME AS 'GROUPS', " & _
                                          "CR_AI.LED_NAME AS Item, " & _
                                          "'RECEIPTS' AS 'TYPES', SUM(TR_AMOUNT) AS Amount ,  " & _
                                          "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE  " & _
                                          "FROM transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE NOT IN (7,9,5, 6, 1, 2, 11) AND ti.TR_TYPE <> 'DEBIT'  " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY LED_NAME, ti.TR_TYPE,SG_NAME  " & _
                                          "UNION ALL " & _
                                          "SELECT  " & _
                                          "0 AS SrNo,SG_NAME AS 'GROUPS',item_name+ ' BY '+ TR_MODE AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
                                          "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE   " & _
                                          "FROM " & _
                                          "transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "LEFT OUTER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE (ti.REC_STATUS IN (0,1,2)) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (5, 6) AND ti.TR_TYPE <> 'DEBIT'  " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY item_name,ti.TR_TYPE,SG_NAME,TR_MODE " & _
                                          "UNION ALL " & _
                                          "SELECT  " & _
                                          "0 AS SrNo,SG_NAME AS 'GROUPS',led_name AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
                                          "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE   " & _
                                          "FROM " & _
                                          "transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "LEFT OUTER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE (ti.REC_STATUS IN (0,1,2)) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (7,9) AND ti.TR_TYPE <> 'DEBIT'  " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY led_name,ti.TR_TYPE,SG_NAME,TR_MODE " & _
                                          "UNION ALL  " & _
                                          "SELECT 0 AS SrNo,'Sale of Assets' AS 'Group', " & _
                                          "CR_AI.LED_NAME  + ' (Sale)' AS Item, " & _
                                          "'RECEIPTS' AS 'TYPE', SUM(TR_AMOUNT) AS Amount ,  " & _
                                          "0 AS CCODE  " & _
                                          "FROM transaction_info AS ti  " & _
                                          "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
                                          "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
                                          "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
                                          "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND TR_CODE IN (11) AND ti.TR_TYPE <> 'DEBIT' 	 " & _
                                          "AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FrDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') " & _
                                          "GROUP BY LED_NAME, ti.TR_TYPE,SG_NAME  " & _
                                          ")AS A  " & _
                                          "GROUP BY A.GROUPS,A.ITEM WITH ROLLUP) AS B  " & _
                                          "ORDER BY cc, b.GROUPS,b.dontuse "

            Dim Dt As DataTable = dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, IIf(Param.IsReceipt, ReceiptQuery, PaymentQuery), ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
            Return Dt
        End Function

        ''' <summary>
        ''' Get Cash Bank Trans Sum
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCashBankTransSum</remarks>
        Public Shared Function GetCashBankTransSum(ByVal FromDate As Date, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT   SUM(CASE WHEN TR_DR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS R_CASH," & _
                                    "       SUM( CASE WHEN TR_CR_LED_ID='00080' THEN TR_AMOUNT ELSE 0 END) AS P_CASH," & _
                                    "        SUM( CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS R_BANK," & _
                                    "        SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE 0 END) AS P_BANK" & _
                                    " FROM Transaction_Info " & _
                                    " Where   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   " & _
                                    " AND CAST(TR_DATE AS DATE) <= '" & Format(FromDate, Common.Server_Date_Format_Short) & "' "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Membership Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipReceipt</remarks>
        Public Shared Function GetMembershipReceipt(ByVal Param As Param_ReportsAll_GetMembershipReceipt, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT CASE WHEN LEN(COALESCE(CURR_AB.C_TITLE,'')) > 0 THEN CURR_AB.C_TITLE+' ' ELSE '' END + CURR_AB.C_NAME AS MS_NAME,'Membership No.: '+ MS.MS_NO AS MS_NO,SI.SI_NAME+' Subscription Fee (Rs.)' AS MS_TYPE,SI.SI_CATEGORY AS MS_CATEGORY, 'Start Date: '+ dbo.fn_FORMATDATE(MS.MS_START_DATE,'dd mon, yyyy') AS MS_START_DATE," & _
                                        " CASE WHEN SI.SI_CATEGORY='LIFETIME' THEN (SELECT 'Life Time') ELSE (SELECT dbo.fn_FORMATDATE(MIN(MB_PERIOD_FROM),'dd mon, yyyy')+'  to  '+dbo.fn_FORMATDATE(MAX(MB_PERIOD_TO),'dd mon, yyyy') FROM MEMBERSHIP_BALANCES_INFO AS MB  WHERE MS.REC_ID=MB.MS_REC_ID AND  MB.MB_TR_ID=TM.REC_ID AND MB.REC_STATUS  IN (0,1,2) ) END AS 'MS_PERIOD'," & _
                                        " CASE WHEN COALESCE(CURR_AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END + ', UID: ' + CASE WHEN COALESCE(CURR_AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE  O.CEN_UID END AS 'MS_CENTRE_NAME','Wing: '+WS.WING_NAME AS MS_WING_NAME,TM.TR_SUB_AMT,TM.TR_CASH_AMT,TM.TR_BANK_AMT, " & _
                                        " dbo.LPAD( DATEDIFF(dd,DATEADD(dd,-1,'" & Param.openYearSdt.ToString(Common.Server_Date_Format_Long) & "'),CAST(MR_DATE AS DATE)) ,3,'0') + dbo.LPAD(COALESCE(MR.MR_NO,''),'4','0')  AS MR_NO ,MR.MR_DATE, '' AS MS_PAYMENT,'' AS MS_INWORDS,MS.MS_TR_ID " & _
                                        " FROM       MEMBERSHIP_INFO      AS MS " & _
                                        " INNER JOIN TRANSACTION_D_MASTER_INFO  AS TM ON (MS.REC_ID         = TM.TR_REF_ID AND TM.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT  JOIN MEMBERSHIP_RECEIPT_INFO    AS MR ON (TM.REC_ID         = MR.MR_TR_M_ID AND MR.MR_CANCEL IS NULL AND MR.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN ADDRESS_BOOK               AS AB ON (MS.MS_AB_ID       = AB.REC_ID AND AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN ADDRESS_BOOK               AS CURR_AB ON (AB.C_ORG_REC_ID       = CURR_AB.C_ORG_REC_ID AND CURR_AB.C_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " AND CURR_AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT OUTER JOIN WINGS_INFO            AS WS ON (MS.MS_WING_ID     = WS.REC_ID AND WS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN SUBSCRIPTION_INFO          AS SI ON (MS.MS_SI_ID       = SI.REC_ID AND SI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT JOIN CENTRE_INFO                 AS C  ON (CURR_AB.C_CLASS_CEN_ID = C.CEN_ID  AND CURR_AB.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " LEFT JOIN OVERSEAS_CENTRE_INFO        AS O  ON (CURR_AB.C_CLASS_CEN_ID = O.CEN_ID  AND CURR_AB.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " Where MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS.MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TM.REC_ID='" & Param.M_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Membership Subscription Fee
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipSubscriptionFee</remarks>
        Public Shared Function GetMembershipSubscriptionFee(ByVal M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT  " & _
                                       "  SUM(CASE WHEN MB_ITEM_ID IN ('6b4e2492-14c7-11e1-9111-00ffddbf0f50') THEN MB_AMOUNT ELSE 0 END ) AS 'ENTRANCE' " & _
                                       " ,SUM(CASE WHEN MB_ITEM_ID IN ('60966821-14c8-11e1-9111-00ffddbf0f50') THEN MB_AMOUNT ELSE 0 END ) AS 'ARREARS'  " & _
                                       " ,SUM(CASE WHEN MB_ITEM_ID IN ('45afe059-14c8-11e1-9111-00ffddbf0f50' ,'4e25ff2a-14c8-11e1-9111-00ffddbf0f50','68d917b2-14c8-11e1-9111-00ffddbf0f50') THEN MB_AMOUNT ELSE 0 END ) AS 'CURRENT' " & _
                                       " ,SUM(CASE WHEN MB_ITEM_ID IN ('5870e47d-14c8-11e1-9111-00ffddbf0f50') THEN MB_AMOUNT ELSE 0 END) AS 'ADVANCE'  " & _
                                       " FROM  MEMBERSHIP_BALANCES_INFO  AS MB " & _
                                       " Where MB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MB.MB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MB.MB_TR_ID='" & M_ID & "' "
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Membership Receipt Payment
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipReceiptPayment</remarks>
        Public Shared Function GetMembershipReceiptPayment(ByVal M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT TP.TR_MODE,BI.BI_SHORT_NAME,TP.TR_REF_NO,dbo.fn_FORMATDATE(TP.TR_REF_DATE,'dd/MM/yyyy') AS TR_REF_DATE,TP.TR_REF_AMT " & _
                                        " FROM  TRANSACTION_D_PAYMENT_INFO  AS TP " & _
                                        " INNER JOIN BANK_INFO              AS BI ON (TP.TR_REF_ID = BI.REC_ID AND BI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " Where TP.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TP.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TP.TR_M_ID='" & M_ID & "' " & _
                                        " ORDER BY TP.TR_MODE,TP.TR_SR_NO "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetPurposeReport(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "Select  CONVERT(varchar, tr_date,103) as Date,ITEM_NAME as Item, CASE WHEN UPPER(ti.TR_TYPE) = 'DEBIT' THEN dr_LED.LED_NAME ELSE cr_LED.LED_NAME END AS Ledger, COALESCE( COALESCE(mi.MISC_NAME,CPR.CPR_NAME), 'General') as Purpose, CASE WHEN UPPER(ti.TR_TYPE) = 'DEBIT'  THEN TI.TR_AMOUNT ELSE -1 * TI.TR_AMOUNT END  as Amount , CASE WHEN TI.TR_TYPE = 'DEBIT' THEN  dr_LED.LED_TYPE ELSE cr_LED.LED_TYPE END AS Nature from transaction_info as ti inner join item_info as ii on ti.TR_ITEM_ID = ii.REC_ID left outer join transaction_d_purpose_info as tp on tp.TR_REC_ID = COALESCE(ti.TR_M_ID,TI.REC_ID) and COALESCE(tp.TR_ITEM_SR_NO,1) = COALESCE(ti.TR_SR_NO,1)  AND tp.REC_STATUS IN (0,1,2) left outer join misc_info as mi on mi.REC_ID = tp.TR_PURPOSE_MISC_ID left outer join center_purpose_info as cpr on cpr.REC_ID = tp.TR_PURPOSE_MISC_ID left outer join acc_ledger_info as dr_LED on dr_led.LED_ID = ti.TR_DR_LED_ID left outer join acc_ledger_info as cr_LED on cr_led.LED_ID = ti.TR_CR_LED_ID where ti.TR_CEN_ID = " & inBasicParam.openCenID.ToString & " AND ti.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND ti.REC_STATUS IN (0,1,2) ORDER BY II.ITEM_TRANS_TYPE, TI.TR_DATE " '(dr_LED.LED_TYPE ='EXPENSE' or cr_LED.LED_TYPE='EXPENSE' or TR_ITEM_ID='08a064fe-ca32-4834-9d11-cecd56620751') and 
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetVoucherReferenceReport(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32}
            Dim lengths As Integer() = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_SPECIAL_VOUCHER_REFERENCE_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetTelephoneBillDetails(ByVal param As Reports_All.Param_ReportsAll_GetTelephoneBill, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OtherCondition As String = ""
            If param.TP_ID <> "--ALL--" Then
                OtherCondition = " AND TP.REC_ID = '" & param.TP_ID & "' "
            End If
            Dim onlineQuery As String = "SELECT * FROM (SELECT DISTINCT TP.TP_NO, TP.TP_NO + ' (' + COALESCE((SELECT MI.MISC_NAME FROM  MISC_INFO AS MI	WHERE TP.TP_TELECOM_MISC_ID	= MI.REC_ID	AND MI.REC_STATUS IN (0,1,2)),'') + ', ' + TP.TP_TYPE + ' ' + TP.TP_CATEGORY + ')' AS 'TP_DETAIL', " &
                                        " TI.TR_DATE,TII.TR_ITEM_NAME,TOI.TR_REF_BILL_NO,TOI.TR_REF_BILL_DATE,TOI.TR_PERIOD_FROM,TOI.TR_PERIOD_TO,TI.TR_MODE, " &
                                        " ( SELECT COALESCE(BI.BI_BANK_NAME,'')  + ', No.' +  COALESCE(TI.TR_REF_NO,'')   FROM BANK_ACCOUNT_INFO AS BA INNER JOIN BANK_BRANCH_INFO AS BB ON (BA.BA_BRANCH_ID = BB.REC_ID AND BB.REC_STATUS  IN (0,1,2) )  INNER JOIN BANK_INFO AS BI ON (BB.BI_BANK_ID = BI.REC_ID AND BI.REC_STATUS  IN (0,1,2) ) WHERE BA.REC_ID =TI.TR_SUB_CR_LED_ID ) as 'TR_REF_DETAIL',  " &
                                        " TI.TR_AMOUNT ,TII.TR_REMARKS " &
                                        " FROM TRANSACTION_INFO		AS TI " &
                                        " INNER JOIN TRANSACTION_D_OTHER_INFO AS TOI ON (TOI.TR_CEN_ID =" & inBasicParam.openCenID.ToString & "   AND TOI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND TOI.TR_M_ID = TI.TR_M_ID AND TOI.TR_SR_NO = TI.TR_SR_NO ) " &
                                        " INNER JOIN TRANSACTION_D_ITEM_INFO  AS TII ON (TII.TR_CEN_ID =" & inBasicParam.openCenID.ToString & "   AND TII.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND TII.TR_M_ID = TI.TR_M_ID AND TII.TR_SR_NO = TI.TR_SR_NO ) " &
                                        " INNER JOIN TELEPHONE_INFO	          AS TP	 ON (TP.REC_ID	   = TOI.TR_REF_ID		    AND TP.REC_STATUS IN (0,1,2)) " &
                                         " INNER JOIN ( SELECT TR_M_ID, TR_SR_NO, MAX(REC_ADD_ON) REC_ADD_ON from transaction_d_other_info GROUP BY TR_M_ID, TR_SR_NO ) as sans_dup on sans_dup.TR_M_ID = toi.TR_M_ID and sans_dup.TR_SR_NO = ti.TR_SR_NO and sans_dup.REC_ADD_ON = TOI.REC_ADD_ON " &
                                         " WHERE TI.REC_STATUS  IN (0,1,2)  AND TI.TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " " & OtherCondition &
                                         " AND TI.TR_DATE BETWEEN '" & param.FromDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & param.ToDate.ToString(Common.Server_Date_Format_Long) & "'" &
                                        " ) AS A ORDER BY TP_NO, CAST(TR_DATE AS DATE)"

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Trial Balance
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTrialBalance</remarks>
        Public Shared Function GetTrialBalance(ByVal param As Reports.Param_GetTrial_Balance, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT L.LED_TYPE,PG.PG_ID,PG.PG_NAME,SG.SG_ID,SG.SG_NAME,L.LED_ID,L.LED_NAME,dbo.getItemOpeningBalance( TR.TR_ITEM_ID," & inBasicParam.openCenID.ToString & ",NULL,NULL," & param.openYearID.ToString & ",'" & param.xFR_Date & "') as OPENING_BAL," & _
                                        " TR.TR_ITEM_ID,I.ITEM_NAME,TR.TR_TYPE,TR.TR_AMOUNT, TR.TR_AMOUNT_CR,TR.TR_AMOUNT_DR , ISNULL(TR.TR_AMOUNT_CR,0) + ISNULL(TR.TR_AMOUNT_DR,0) as CLOSING_BAL" & _
                                        " FROM ACC_LEDGER_INFO                  AS L " & _
                                        " INNER JOIN ACC_SECONDARY_GROUP_INFO   AS SG   ON (L.LED_SG_ID   = SG.SG_ID    AND SG.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN ACC_PRIMARY_GROUP_INFO     AS PG   ON (PG.PG_ID      = SG.SG_PG_ID AND PG.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN " & _
                                        "   (  SELECT T.TR_ITEM_ID,CASE WHEN T.TR_TYPE='CREDIT' THEN T.TR_CR_LED_ID ELSE T.TR_DR_LED_ID END AS LED_ID,MAX(T.TR_TYPE) AS TR_TYPE,SUM(T.TR_AMOUNT) AS TR_AMOUNT, SUM(CASE WHEN T.TR_TYPE='CREDIT' THEN T.TR_AMOUNT ELSE NULL END) AS TR_AMOUNT_CR,SUM(CASE WHEN T.TR_TYPE='DEBIT' THEN T.TR_AMOUNT ELSE NULL END) AS TR_AMOUNT_DR " & _
                                        "     FROM  TRANSACTION_INFO AS T " & _
                                        "     WHERE TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND T.TR_COD_YEAR_ID=" & param.openYearID.ToString & " AND CONVERT(Date, TR_DATE,103) >= CONVERT(Date,'" & param.xFR_Date & "',103) AND CONVERT(Date,TR_DATE,103) <= CONVERT(Date,'" & param.xTo_Date & "',103)  AND T.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                                        "     GROUP BY  CASE WHEN T.TR_TYPE='CREDIT' THEN T.TR_CR_LED_ID ELSE T.TR_DR_LED_ID END, T.TR_ITEM_ID    " & _
                                        "   ) AS TR ON (TR.LED_ID = L.LED_ID) " & _
                                        " INNER JOIN ITEM_INFO                  AS I    ON (TR.TR_ITEM_ID = I.REC_ID    AND I.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " ORDER BY PG.PG_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Trial Balance for report
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTrialBalanceReport(ByVal inParam As Reports_All.Param_ReportsAll_GetTrialBalReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE", "X_YEAR_START_DATE", "SPECIAL_VOUCHER_REFERENCE"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inBasicParam.openYearID, inParam.FrDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), inParam.openYearSdt.ToString(Common.Server_Date_Format_Short), inParam.specialVoucherReference}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.[DateTime], DbType.DateTime, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {36, 5, 5, 36, 20, 20, 20, 8000}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_TRIAL_BALANCE_BASIC", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        'Public Shared Function GetIncomeExpenditureReport(ByVal inParam As Reports_All.Param_ReportsAll_GetTrialBalReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim paramters As String() = {"CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
        '    Dim values As Object() = {inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inBasicParam.openYearID, inParam.FrDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short)}
        '    Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.[DateTime], DbType.DateTime}
        '    Dim lengths As Integer() = {36, 5, 5, 5, 4, 20, 20}
        '    Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_INCOME_EXPENDITURE", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        'End Function

        ''' <summary>
        ''' Gets Insurance Policy specific details for that year 
        ''' </summary>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInsurancePolicyDetails(YearId As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim yearWisePolicyNo As String = ""
            Dim DefaultPolicyNo As String = ""
            If YearId = 1516 Then
                DefaultPolicyNo = "1610001116P110131570"
                yearWisePolicyNo = "1610001116P110131570"
                Dim cen_bk_pad_no As Integer = CInt(dbService.GetScalar(ConnectOneWS.Tables.CENTRE_INFO, "SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " + inBasicParam.openCenID.ToString(), "CENTRE_INFO", inBasicParam))
                If cen_bk_pad_no >= 640 And cen_bk_pad_no < 1758 Then yearWisePolicyNo = "1610001116P110132086"
                If cen_bk_pad_no >= 1758 And cen_bk_pad_no < 3954 Then yearWisePolicyNo = "1610001116P110132661"
                If cen_bk_pad_no >= 3954 Then yearWisePolicyNo = "1610001116P110132511"
            End If
            Dim Query As String = "select CONVERT(VARCHAR,PM_DATE_OF_POLICY,4) AS DateOfPolicy,PM_P1 as P1,PM_P2 as P2,REPLACE(PM_P3,'+DefaultPolicyNo+','+yearWisePolicyNo+') as P3,PM_P4 as P4,PM_P5 as P5,PM_P6 as P6,PM_P7 as P7,PM_P8 as P8,PM_P9 as P9,PM_P10 as P10 from policy_master_info WHERE PM_COD_YEAR_ID =" & YearId.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.POLICY_MASTER_INFO, Query, ConnectOneWS.Tables.POLICY_MASTER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get InsuranceLetterData 
        ''' </summary>
        ''' <param name="BK_PAD_NO"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInsuranceLetterData(param As Param_GetInsuranceLetterData, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"YEARID", "CEN_BK_PAD_NO"}
            Dim values As Object() = {param.YearID, param.BK_PAD_NO}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {4, 5}
            Dim allDetails As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "sp_Get_INSURANCE_INFO", ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            Return allDetails
        End Function

        Public Shared Function GetWIPReport(inParam As Reports_All.Param_ReportsAll_GetConstructionExpensesList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Fr_Date.ToString(Common.Server_Date_Format_Short), inParam.To_Date.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_Get_WIP_Expenses", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Insured Years for a Center
        ''' </summary>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInsuredYears(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT '20'+ SUBSTRING(CAST(LB_COD_YEAR_ID AS VARCHAR),1,2) + ' - ' +'20'+ SUBSTRING(CAST(LB_COD_YEAR_ID AS VARCHAR),3,2) as 'Financial Year',pm.[PM_POLICY_FROM] AS 'Policy From',pm.[PM_POLICY_TO] as 'Policy To', LB_COD_YEAR_ID as YEAR_ID FROM land_building_info as LB INNER JOIN policy_master_info AS PM on LB.lb_cod_year_id = pm.PM_COD_YEAR_ID and pm.rec_status in (0,1,2) WHERE LB_INSURANCE_APPLICABLE = 1 and LB_CEN_ID in ( SELECT CEN_ID FROM CENTRE_INFO WHERE CEN_BK_PAD_NO IN ( SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " & inBasicParam.openCenID.ToString & ")) ORDER BY YEAR_ID DESC"
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function ShowInsuranceLetter(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' Get Membership Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipReceipt</remarks>
        Public Shared Function GetMagazineReceipt(ByVal inParam As Param_GetMagazineReceipt, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CEN_ID", "YEAR_ID", "TR_M_ID", "PREV_YEAR_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Tr_m_ID, inParam.Prev_Year_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String], DbType.Int32}
            Dim lengths As Integer() = {5, 4, 36, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, "sp_get_MagazineMembershipReceipt", ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Updation of Clearing dates in case of Bank Reconsilation 
        ''' </summary>
        ''' <param name="inParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UpdateClearingDate(inParam As Param_UpdateClearingDate, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim clearingDateString As String = " NULL "
            If inParam.ClearingDate <> DateTime.MinValue Then clearingDateString = "'" & inParam.ClearingDate.ToString(Common.Server_Date_Format_Long) & "'"
            Query = "UPDATE TRANSACTION_INFO SET TR_REF_CDATE = " & clearingDateString & " WHERE REC_ID = '" & inParam.iRecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, Query, inBasicParam, DateTime.Now, inParam.TxnDate)

            If Not inParam.iTrMID Is Nothing Then
                Query = "UPDATE TRANSACTION_D_PAYMENT_INFO SET TR_REF_CDATE = " & clearingDateString & " WHERE TR_M_ID = '" & inParam.iTrMID & "' AND TR_REF_NO = '" & inParam.iRefNo & "' AND TR_MODE = '" & inParam.Mode & "'"
                dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, inBasicParam, DateTime.Now, inParam.TxnDate)
            End If
            Return True
        End Function

        Public Shared Function GetBalanceSheetReport(param As Param_ReportsAll_GetTrialBalReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"INS_ID", "CEN_ID", "YEAR_ID", "FR_DATE", "TO_DATE"}
            Dim values As Object() = {param.InsttId, inBasicParam.openCenID, inBasicParam.openYearID, param.FrDate, param.ToDate}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.Int32, DbType.DateTime2, DbType.DateTime2}
            Dim lengths As Integer() = {5, 4, 4, 12, 12}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Balance_Sheet_Formatted", ConnectOneWS.Tables.TRANSACTION_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetIncomeExpenditureReport(param As Param_ReportsAll_GetTrialBalReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"INS_ID", "CEN_ID", "YEAR_ID", "FR_DATE", "TO_DATE"}
            Dim values As Object() = {param.InsttId, inBasicParam.openCenID, inBasicParam.openYearID, param.FrDate, param.ToDate}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.Int32, DbType.DateTime2, DbType.DateTime2}
            Dim lengths As Integer() = {5, 4, 4, 12, 12}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Income_Expenditure_Formatted", ConnectOneWS.Tables.TRANSACTION_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
        End Function
    End Class
#End Region
    <Serializable>
    Public Class Reports
        <Serializable>
        Public Class Param_MagSubDispatchReport
            Public MagID As String
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public YearID As Integer
        End Class
        <Serializable>
        Public Class Param_GelLedgerReport
            Public Led_ID As String
            Public InsttId As Object
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public Cross_Ref_Id As String = ""
            Public SubLedID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetLedgerOpeningBalance
            Public Led_ID As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public OnDate As DateTime
            Public SubLedID As String
        End Class
        <Serializable>
        Public Class Param_GetItemReport
            Public ItemId As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetItemOpeningBalance
            Public ItemID As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public OnDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetSecondaryGroupReport
            Public SgId As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetSecondaryGroupOpeningBalance
            Public SgId As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public OnDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetPrimaryGroupReport
            Public PgId As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetPrimaryGroupOpeningBalance
            Public PgId As String
            Public ZoneId As Object
            Public InsttId As Object
            Public YearID As Integer
            Public OnDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetPartyListing
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public PartyID As String
        End Class
        <Serializable>
        Public Class Param_GetPartyReport
            Public PartyId As String
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetPartyOpeningBalance
            Public PartyId As String
            Public YearID As Integer
            Public OnDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetFDReport
            Public YearID As Integer
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public RequiredList As FdListType
            <Serializable>
            Public Enum FdListType
                Existing_As_On
                Added_Renewed_in_Period
                Closed_in_Period
            End Enum
        End Class
        <Serializable>
        Public Class Param_GetVehicleReport
            Public PrevYearID As Integer
            Public OnDate As DateTime
            Public YearID As Integer
            Public YearStartDate As Date
        End Class
        <Serializable>
        Public Class Param_GetGSReport
            Inherits Param_GetVehicleReport
        End Class
        <Serializable>
        Public Class Param_GetAssetReport
            Inherits Param_GetVehicleReport
        End Class
        <Serializable>
        Public Class Param_GetLBReport
            Public PrevYearID As Integer
            Public OnDate As DateTime
            'Public toDate As DateTime
            Public YearID As Integer
        End Class
        <Serializable>
        Public Class Param_GetTrial_Balance
            Public openYearID As Integer
            Public xFR_Date As Date
            Public xTo_Date As Date
        End Class
        <Serializable>
        Public Class Param_GetInsurancePropertyList
            Public openBkPadNo As String
            Public openYearID As Integer
            Public xFR_Date As Date
            Public xTo_Date As Date
            Public prev_year_id As Integer = Nothing
        End Class
        <Serializable>
        Public Class Param_AssetInsuranceBreakUpReport
            Public openBkPadNo As String = ""
            Public prev_year_id As Int32 = Nothing
        End Class

#Region "new Additions "
        Public Shared Function GetTransactionDetail(ByVal TxnID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query As String = ""
            query = "SELECT DISTINCT TR_AMOUNT as Amount, 'By ' + DEBIT.LED_NAME AS DEBIT, 'To ' + CREDIT.LED_NAME AS CREDIT FROM TRANSACTION_INFO AS TI LEFT OUTER JOIN ACC_LEDGER_INFO AS CREDIT ON TR_CR_LED_ID = CREDIT.LED_ID LEFT OUTER JOIN ACC_LEDGER_INFO AS DEBIT ON TR_DR_LED_ID = DEBIT.LED_ID WHERE  CASE WHEN TR_M_ID IS NOT NULL THEN TR_M_ID =(SELECT DISTINCT TR_M_ID FROM TRANSACTION_INFO WHERE REC_ID ='" + TxnID + "' AND rec_status IN (0,1,2))  ELSE TI.REC_ID ='" + TxnID + "' END  AND TI.rec_status IN (0,1,2) ORDER BY TR_DR_LED_ID"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetBankAccountsList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query As String = ""
            query = " SELECT CI.CEN_NAME as Center, CI.CEN_UID as UID, INS_SHORT AS 'Inst', CI.CEN_ADD1 as Add1,CI.CEN_ADD2 as Add2,CI.CEN_ADD3 as Add3,COALESCE(CI.CEN_ADD4,'') + ' (Mob: ' + COALESCE(CI.CEN_MOB_NO_1,'--') + COALESCE( ', Tel: ' + CI.CEN_TEL_NO_1,'--') + ')' as Add4, " &
                   " CITY.CI_NAME  AS City, State.ST_NAME as State,CC_CI.CEN_NAME 'Connected Center', CC_CI.CEN_UID 'CC UID', " &
                   " BI.BI_BANK_NAME as Bank, BR.BB_BRANCH_NAME as Branch, BR.BB_IFSC_CODE as IFSC, BA.BA_ACCOUNT_NO  as 'Acc No.', SIGN1.C_NAME AS Sign1, SIGN2.C_NAME AS Sign2, MAIN.CEN_ZONE_ID AS 'Zone', CASE WHEN BA_CLOSE_DATE IS NULL THEN CASE WHEN BA_COD_YEAR_ID IS NULL THEN 'NO BANK' ELSE 'Running' END else 'Closed' end as Status , BA_CUST_NO AS CIF, BA_OPEN_DATE AS 'Opening Date' " &
                   " FROM CENTRE_INFO AS CI " &
                   " INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1  " &
                   " INNER JOIN INSTITUTE_INFO AS II ON CI.CEN_INS_ID = II.INS_ID " &
                   " LEFT OUTER JOIN map_city_info AS CITY ON MAIN.CEN_CITY_ID = CITY.REC_ID " &
                   " LEFT OUTER JOIN map_state_info AS STATE ON MAIN.CEN_STATE_ID = STATE.REC_ID " &
                   " LEFT OUTER JOIN centre_info AS CC_CI ON CI.CEN_CC_ID = CC_CI.CEN_ID " &
                   " LEFT OUTER JOIN bank_account_info BA ON BA_CEN_ID = CI.CEN_ID  " &
                   " LEFT OUTER JOIN bank_branch_info AS BR ON BA.BA_BRANCH_ID = BR.REC_ID  " &
                   " LEFT OUTER JOIN bank_info AS BI ON BR.BI_BANK_ID = BI.REC_ID " &
                   " LEFT OUTER JOIN ADDRESS_BOOK AS SIGN1 ON BA_SIGN_AB_ID_1 = SIGN1.REC_ID " &
                   " LEFT OUTER JOIN ADDRESS_BOOK AS SIGN2 ON BA_SIGN_AB_ID_2 = SIGN2.REC_ID " &
                   " WHERE COALESCE(BA_COD_YEAR_ID," & inBasicParam.openYearID.ToString & ") <=" & inBasicParam.openYearID.ToString & " and ( COALESCE(BA_COD_YEAR_ID," & inBasicParam.openYearID.ToString & ") =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (COALESCE(BA_COD_YEAR_ID," & inBasicParam.openYearID.ToString & ") NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR COALESCE(BA_COD_YEAR_ID," & inBasicParam.openYearID.ToString & ")= " & inBasicParam.openYearID.ToString & ") " &
                   " AND COALESCE(BA_CLOSE_DATE, GETDATE()) >= CAST('20'+SUBSTRING('" + inBasicParam.openYearID.ToString + "',1,2)+'-04-01' AS DATE) AND COALESCE(BA_ACCOUNT_TYPE,'SAVING') = 'SAVING' ORDER BY MAIN.CEN_NAME"
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTransactionDetailsForLedger(ByVal TxnMID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"TR_M_ID"}
            Dim values As Object() = {TxnMID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String]}
            Dim lengths As Integer() = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_Get_Txn_Detail", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetLedgerReport(ByVal inParam As Param_GelLedgerReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"LEDGERID", "CENID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE", "SUBLEDGERID", "USER_ID"}
            Dim values As Object() = {inParam.Led_ID, inBasicParam.openCenID, inParam.InsttId, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), IIf(inParam.SubLedID = "", Nothing, inParam.SubLedID), inBasicParam.openUserID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 5, 5, 4, 20, 20, 36, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_LEDGER_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetMagSubDispatchReport(ByVal inParam As Param_MagSubDispatchReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"cenid", "yearid", "fromdate", "TOdate", "magid"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), inParam.MagID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {5, 5, 20, 20, 36}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Magazine_Subs_Dispatch", paramters, values, dbTypes, lengths, inBasicParam)

        End Function
        Public Shared Function GetLedgerOpeningBalance(ByVal inParam As Param_GetLedgerOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"LEDGERID", "CENID", "INSTTID", "YEARID", "X_FROM_DATE", "SUBLEDGERID"}
            Dim values As Object() = {inParam.Led_ID, inBasicParam.openCenID, inParam.InsttId, inParam.YearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short), inParam.SubLedID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.Int32, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {5, 5, 5, 4, 20, 36}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_LEDGER_OPENING", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Rows.Count = 0 Then Return Nothing
            Return Data.Rows(0)(0)
        End Function

        Public Shared Function GetItemReport(ByVal inParam As Param_GetItemReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"ITEMID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {inParam.ItemId, inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_ITEM_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetItemOpeningBalance(ByVal inParam As Param_GetItemOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Double
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"ITEMID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE"}
            Dim values As Object() = {inParam.ItemID, inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inParam.YearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_ITEM_OPENING", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Rows.Count = 0 Then Return Nothing
            Return Data.Rows(0)(0)
        End Function

        Public Shared Function GetSecondaryGroupReport(ByVal InParam As Param_GetSecondaryGroupReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"SGID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {InParam.SgId, inBasicParam.openCenID, InParam.ZoneId, InParam.InsttId, InParam.YearID, InParam.FromDate.ToString(Common.Server_Date_Format_Short), InParam.ToDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_SECONDARY_GROUP_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetSecondaryGroupOpeningBalance(ByVal inParam As Param_GetSecondaryGroupOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Double
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"SGID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE"}
            Dim values As Object() = {inParam.SgId, inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inParam.YearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_SECONDARY_GROUP_OPENING", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Rows.Count = 0 Then Return Nothing
            Return Data.Rows(0)(0)
        End Function

        Public Shared Function GetPrimaryGroupReport(ByVal inParam As Param_GetPrimaryGroupReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"PGID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE"}
            Dim values As Object() = {inParam.PgId, inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_PRIMARY_GROUP_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetPrimaryGroupOpeningBalance(ByVal inParam As Param_GetPrimaryGroupOpeningBalance, inBasicParam As ConnectOneWS.Basic_Param) As Double
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"PGID", "CENID", "ZONEID", "INSTTID", "YEARID", "X_FROM_DATE"}
            Dim values As Object() = {inParam.PgId, inBasicParam.openCenID, inParam.ZoneId, inParam.InsttId, inParam.YearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.DateTime}
            Dim lengths As Integer() = {36, 5, 5, 5, 4, 20}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_PRIMARY_GROUP_OPENING", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Rows.Count = 0 Then Return Nothing
            Return Data.Rows(0)(0)
        End Function

        Public Shared Function GetPartyListing(ByVal inParam As Param_GetPartyListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "X_FROM_DATE", "X_TO_DATE", "X_YEAR_ID", "PARTY_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), inBasicParam.openYearID, inParam.PartyID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.String}
            Dim lengths As Integer() = {5, 20, 20, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Party_Listing_Client", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetPartyReport(ByVal inParam As Param_GetPartyReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"PARTYID", "YEARID", "X_FROM_DATE", "X_TO_DATE", "USERID"}
            Dim values As Object() = {inParam.PartyId, inParam.YearID, inParam.FromDate, inParam.ToDate, inBasicParam.openUserID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {36, 4, 20, 20, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_PARTY_REPORT", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetFDReport(ByVal inParam As Param_GetFDReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "X_FROM_DATE", "X_TO_DATE", "OPTION"}
            Dim Opt As String = "EXIST"
            If inParam.RequiredList = Param_GetFDReport.FdListType.Added_Renewed_in_Period Then Opt = "ADDED"
            If inParam.RequiredList = Param_GetFDReport.FdListType.Closed_in_Period Then Opt = "CLOSE"
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), Opt}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 20, 20, 5}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.FD_INFO, "sp_rpt_FD_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetVehicleReport(ByVal inParam As Param_GetVehicleReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "X_ON_DATE", "YEAR_START_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.PrevYearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short), inParam.YearStartDate}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 4, 20, 20}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.VEHICLES_INFO, "sp_rpt_vehicle_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetAssetReport(ByVal inParam As Param_GetAssetReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "X_ON_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.PrevYearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 4, 20}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.ASSET_INFO, "sp_rpt_ASSET_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetLBReport(ByVal inParam As Param_GetLBReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "X_ON_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.PrevYearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.[String], DbType.[String], DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 4, 20}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "sp_rpt_LB_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetGSReport(ByVal inParam As Param_GetGSReport, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "X_ON_DATE", "YEAR_START_DATE"}
            Dim values As Object() = {inBasicParam.openCenID, inParam.YearID, inParam.PrevYearID, inParam.OnDate.ToString(Common.Server_Date_Format_Short), inParam.YearStartDate.ToString(Common.Server_Date_Format_Short)}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths As Integer() = {5, 4, 4, 20, 20}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.GOLD_SILVER_INFO, "sp_rpt_GS_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetInsurancePropertyList(ByVal inParam As Param_GetInsurancePropertyList, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"BK_PAD_NO", "YEARID", "XFR_DATE", "XTO_DATE", "PREV_YEARID"}
            Dim values As Object() = {inParam.openBkPadNo, inParam.openYearID, inParam.xFR_Date.ToString(Common.Server_Date_Format_Short), inParam.xTo_Date.ToString(Common.Server_Date_Format_Short), inParam.prev_year_id}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Int32}
            Dim lengths As Integer() = {5, 4, 20, 20, 4}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "sp_rpt_INSURANCE_REPORT", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetAssetInsuranceBreakUpReport(param As Param_AssetInsuranceBreakUpReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"BK_PAD_NO", "YEARID", "PREV_YEARID"}
            Dim values As Object() = {param.openBkPadNo, inBasicParam.openYearID, param.prev_year_id}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.Int32}
            Dim lengths As Integer() = {15, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_INFO, "sp_get_Asset_InsuranceValue_Breakup", ConnectOneWS.Tables.ASSET_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetConsumableBreakUpReport(openBkPadNo As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"BK_PAD_NO", "YEARID"}
            Dim values As Object() = {openBkPadNo, inBasicParam.openYearID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32}
            Dim lengths As Integer() = {15, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_INFO, "sp_get_Consumables_Value_Breakup", ConnectOneWS.Tables.ASSET_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
        End Function


#End Region
    End Class
End Namespace