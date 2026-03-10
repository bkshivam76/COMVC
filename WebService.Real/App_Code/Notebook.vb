Imports System.Data

Namespace Real
    <Serializable>
    Public Class Notebook
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Notebook_GetList
            Public Month_Of_Year As Integer
            Public CurrInsttID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_NoteBook
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Amount As Double
            Public Narration As String
            Public Status_Action As String
            Public RecID As String
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Param_InsertAllEntries_Notebook
            Public AllEntries As ArrayList
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Update_NoteBook
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Amount As Double
            Public Narration As String
            'Public Status_Action As String
            Public RecID As String
        End Class
#End Region
        ''' <summary>
        ''' Get Max Transaction Date
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_GetMaxTransactionDate</remarks>
        Public Shared Function GetMaxTransactionDate(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT MAX(TR_DATE) AS MAXDATE FROM Transaction_Info WHERE  REC_STATUS IN (0,1,2)  AND TR_CEN_ID='" & inBasicParam.openCenID & "'  AND UPPER(TR_NOTEBOOK) = 'YES'  AND TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_GetList</remarks>
        Public Shared Function GetList(ByVal param As Parameter_Notebook_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,ITEM_TRANS_TYPE,I.REC_ID,TI.AMT_DT_01,TI.AMT_DT_02,TI.AMT_DT_03,TI.AMT_DT_04,TI.AMT_DT_05,TI.AMT_DT_06,TI.AMT_DT_07,TI.AMT_DT_08,TI.AMT_DT_09,TI.AMT_DT_10,TI.AMT_DT_11,TI.AMT_DT_12,TI.AMT_DT_13,TI.AMT_DT_14,TI.AMT_DT_15,TI.AMT_DT_16,TI.AMT_DT_17,TI.AMT_DT_18,TI.AMT_DT_19,TI.AMT_DT_20,TI.AMT_DT_21,TI.AMT_DT_22,TI.AMT_DT_23,TI.AMT_DT_24,TI.AMT_DT_25,TI.AMT_DT_26,TI.AMT_DT_27,TI.AMT_DT_28,TI.AMT_DT_29,TI.AMT_DT_30,TI.AMT_DT_31, COALESCE( TI.TR_AMOUNT , 0 ) AS AMT_TOTAL " & _
                                        " FROM ITEM_INFO AS I  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & param.CurrInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " " & _
                                        " INNER JOIN ACC_LEDGER_INFO   AS L ON (I.ITEM_LED_ID=L.LED_ID AND L.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT OUTER JOIN  " & _
                                        "            ( SELECT TR_ITEM_ID,SUM(CASE WHEN DAY(TI.TR_DATE)=01 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_01',SUM(CASE WHEN DAY(TI.TR_DATE)=02 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_02',SUM(CASE WHEN DAY(TI.TR_DATE)=03 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_03',SUM(CASE WHEN DAY(TI.TR_DATE)=04 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_04',SUM(CASE WHEN DAY(TI.TR_DATE)=05 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_05',SUM(CASE WHEN DAY(TI.TR_DATE)=06 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_06',SUM(CASE WHEN DAY(TI.TR_DATE)=07 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_07',SUM(CASE WHEN DAY(TI.TR_DATE)=08 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_08',SUM(CASE WHEN DAY(TI.TR_DATE)=09 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_09',SUM(CASE WHEN  DAY(TI.TR_DATE)=10 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_10',SUM(CASE WHEN DAY(TI.TR_DATE)=11 THEN TI.TR_AMOUNT ELSE NULL END ) AS 'AMT_DT_11',SUM(CASE WHEN DAY(TI.TR_DATE)=12 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_12',SUM(CASE WHEN DAY(TI.TR_DATE)=13 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_13',SUM(CASE WHEN DAY(TI.TR_DATE)=14 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_14',SUM(CASE WHEN DAY(TI.TR_DATE)=15 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_15',SUM(CASE WHEN DAY(TI.TR_DATE)=16 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_16',SUM(CASE WHEN DAY(TI.TR_DATE)=17 THEN TI.TR_AMOUNT ELSE NULL END ) AS 'AMT_DT_17',SUM(CASE WHEN DAY(TI.TR_DATE)=18 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_18',SUM(CASE WHEN DAY(TI.TR_DATE)=19 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_19',SUM(CASE WHEN DAY(TI.TR_DATE)=20 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_20',SUM(CASE WHEN DAY(TI.TR_DATE)=21 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_21',SUM(CASE WHEN DAY(TI.TR_DATE)=22 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_22',SUM(CASE WHEN DAY(TI.TR_DATE)=23 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_23',SUM(CASE WHEN DAY(TI.TR_DATE)=24 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_24',SUM(CASE WHEN DAY(TI.TR_DATE)=25 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_25',SUM(CASE WHEN DAY(TI.TR_DATE)=26 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_26',SUM(CASE WHEN DAY(TI.TR_DATE)=27 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_27',SUM(CASE WHEN DAY(TI.TR_DATE)=28 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_28',SUM(CASE WHEN DAY(TI.TR_DATE)=29 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_29',SUM(CASE WHEN DAY(TI.TR_DATE)=30 THEN TI.TR_AMOUNT ELSE NULL END ) AS 'AMT_DT_30',SUM(CASE WHEN DAY(TI.TR_DATE)=31 THEN TI.TR_AMOUNT ELSE NULL END) AS 'AMT_DT_31',SUM(TI.TR_AMOUNT) AS TR_AMOUNT" & _
                                        "              FROM TRANSACTION_INFO AS TI" & _
                                        "              WHERE TI.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TI.REC_STATUS IN (0,1,2) AND TI.TR_CODE=3 AND UPPER(TI.TR_NOTEBOOK) = 'YES' AND TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND MONTH(TI.TR_DATE)= " & param.Month_Of_Year & _
                                        "              GROUP BY TI.TR_ITEM_ID " & _
                                        "            ) AS TI ON (TI.TR_ITEM_ID=I.REC_ID)" & _
                                        " WHERE I.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT') AND UPPER(I.ITEM_PETTY_CASH)='YES' " & _
                                        " ORDER BY I.ITEM_NAME"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Notebook_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_NoteBook, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_MODE,TR_AMOUNT,TR_NOTEBOOK,TR_NARRATION,TR_REMARKS,TR_REFERENCE," & _
                                                    "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                    ") VALUES(" & _
                                                    "" & inBasicParam.openCenID.ToString & "," & _
                                                    "" & InParam.openYearID.ToString & "," & _
                                                    " " & InParam.TransCode & "," & _
                                                    "'" & InParam.VNo & "', " & _
                                                    " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Short) & "'", " NULL ") & " , " & _
                                                    "'" & InParam.ItemID & "', " & _
                                                    "'" & InParam.Type & "', " & _
                                                    "'" & InParam.Cr_Led_ID & "', " & _
                                                    "'" & InParam.Dr_Led_ID & "', " & _
                                                    " " & "NULL" & " , " & _
                                                    "'" & "CASH" & "', " & _
                                                    " " & InParam.Amount & ", " & _
                                                     "'YES'," & _
                                                    "'" & InParam.Narration & "', " & _
                                                    "'" & "" & "', " & _
                                                    "'" & "" & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notebook_InsertAllEntries</remarks>
        Public Shared Function Insert(ByVal Param As Param_InsertAllEntries_Notebook, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each xEntry In Param.AllEntries.ToArray
                If Not xEntry Is Nothing Then
                    If Not xEntry.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(xEntry.TDate), xEntry.Dr_Led_ID)
                    If Not xEntry.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(xEntry.TDate), xEntry.Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                    'If Not upVoucher_Update.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
                End If
            Next

            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_MODE,TR_AMOUNT,TR_NOTEBOOK,TR_NARRATION,TR_REMARKS,TR_REFERENCE," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                        ") VALUES "
            Dim RecID As String = ""
            Dim LastTxnDate As Date = Nothing
            For Each xEntry In Param.AllEntries.ToArray
                If RecID.Trim.Length = 0 Then RecID = xEntry.RecID
                OnlineQuery = OnlineQuery & _
                              "(" & _
                              "" & inBasicParam.openCenID.ToString & "," & _
                              "" & Param.openYearID.ToString & "," & _
                              " " & xEntry.TransCode & "," & _
                              "'" & xEntry.VNo & "', " & _
                              " " & If(IsDate(xEntry.TDate), "'" & Convert.ToDateTime(xEntry.TDate).ToString(Common.Server_Date_Format_Short) & "'", " NULL ") & " , " & _
                              "'" & xEntry.ItemID & "', " & _
                              "'" & xEntry.Type & "', " & _
                              "'" & xEntry.Cr_Led_ID & "', " & _
                              "'" & xEntry.Dr_Led_ID & "', " & _
                              " " & "NULL" & " , " & _
                              "'" & "CASH" & "', " & _
                              " " & xEntry.Amount & ", " & _
                              "'YES'," & _
                              "'" & xEntry.Narration & "', " & _
                              "'" & "" & "', " & _
                              "'" & "" & "', " & _
                              "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & xEntry.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & xEntry.RecID & "'" & _
                              "),"
                LastTxnDate = xEntry.TDate
            Next
            If OnlineQuery.Trim.Length > 0 Then OnlineQuery = IIf(OnlineQuery.Trim.EndsWith(","), Mid(OnlineQuery.Trim.ToString, 1, OnlineQuery.Trim.Length - 1), OnlineQuery.Trim.ToString)

            '---> NOTE: Only first entry RecID passed in this function because all entries are same month,year.
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, RecID, LastTxnDate)
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
        ''' <remarks>RealServiceFunctions.Notebook_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_NoteBook, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_ITEM_ID     ='" & UpParam.ItemID & "', " & _
                                            " TR_TYPE        ='" & UpParam.Type & "', " & _
                                            " TR_CR_LED_ID   ='" & UpParam.Cr_Led_ID & "', " & _
                                            " TR_DR_LED_ID   ='" & UpParam.Dr_Led_ID & "', " & _
                                            " TR_SUB_CR_LED_ID  = NULL, " & _
                                            " TR_MODE        ='CASH', " & _
                                            " TR_AMOUNT      = " & UpParam.Amount & ", " & _
                                            " TR_NARRATION   ='" & UpParam.Narration & "', " & _
                                            " TR_REMARKS     ='', " & _
                                            " TR_REFERENCE   ='', " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, Nothing, UpParam.TDate)
            Return True
        End Function
    End Class
End Namespace
