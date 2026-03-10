Imports System.Data

Namespace Real
    <Serializable>
    Public Class OpeningBalances

        Public Shared ProfileName As String = "OPENING" 'FOR OPENING BALANCES
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_OpeningBalances_GetList
            Public ItemID As String
            Public YearID As Integer
            Public CurrInsttID As String
            Public PrevYearID As Integer = Nothing
        End Class
        <Serializable>
        Public Class Parameter_Insert_OpeningBalances
            Public ItemID As String
            Public Amount As Double
            Public DebitCredit As String
            Public OtherDetails As String
            Public Status_Action As String
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Update_OpeningBalances
            Public ItemID As String
            Public Amount As Double
            Public DebitCredit As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
#End Region

        Public Shared Function GetList(ByVal param As Parameter_OpeningBalances_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim PrevYearChanges As String = ""
            'If param.PrevYearID.Length > 0 Then
            '    PrevYearChanges = " + (SELECT COALESCE(SUM(AMOUNT),0) FROM (	SELECT TR_DATE, CASE WHEN ti.TR_CR_LED_ID = L.LED_ID THEN CASE WHEN cr_al.LED_TYPE IN ('EXPENSE', 'ASSET') THEN -1 * TR_AMOUNT ELSE TR_AMOUNT END Else	CASE WHEN dr_al.LED_TYPE IN ('EXPENSE', 'ASSET') THEN TR_AMOUNT ELSE -1 * TR_AMOUNT END END AS AMOUNT FROM transaction_info AS ti  LEFT OUTER JOIN acc_ledger_info AS dr_al ON TR_DR_LED_ID = dr_al.led_ID AND DR_AL.LED_ID = L.LED_ID AND dr_al.rec_status IN (0,1,2) LEFT OUTER JOIN acc_ledger_info AS cr_al ON TR_CR_LED_ID = cr_al.led_ID AND CR_AL.LED_ID = L.LED_ID AND cr_al.rec_status IN (0,1,2) WHERE (TR_DR_LED_ID = L.LED_ID  OR TR_CR_LED_ID = L.LED_ID) AND ti.rec_status IN (0,1,2) AND ti.TR_COD_YEAR_ID = '1112' AND TR_CEN_ID =OP.OP_CEN_ID ) AS A )"
            'End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim Query As String = " SELECT I.ITEM_NAME as 'Item Name',L.LED_NAME as 'Head',L.LED_TYPE as 'Head Type',OP.OP_AMOUNT " & PrevYearChanges & " as 'Amount',OP.OP_DEBIT_CREDIT AS 'Type',OP.OP_REMARKS AS 'Other Details',OP.OP_YEAR_ID AS YearID, OP.REC_ID AS ID, " & Common.Remarks_Detail("OP", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("OP") & "" & _
            '                      " FROM Other_Profile_Info AS OP " & _
            '                      " INNER JOIN ITEM_INFO AS I  ON (OP.OP_ITEM_ID = I.REC_ID)" & _
            '                      " INNER JOIN Item_Mapping as IM  ON (I.REC_ID = IM.Map_Item_Rec_ID and IM.Map_Instt_ID ='" & param.CurrInsttID & "') " & _
            '                      " INNER JOIN Acc_Ledger_Info AS L ON (I.ITEM_LED_ID = L.LED_ID) " & _
            '                      " Where OP.REC_STATUS IN (0,1,2) AND OP.OP_NAME='" & ProfileName & "' AND OP.OP_YEAR_ID <='" & param.YearID & "'  and ( OP_YEAR_ID ='" & inBasicParam.openYearID & "' OR OP_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = OP_CEN_ID)) AND (OP_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = OP_CEN_ID) OR OP_YEAR_ID = '" & inBasicParam.openYearID & "') AND OP.OP_CEN_ID='" & inBasicParam.openCenID & "'  " & _
            '                      " ORDER BY  I.ITEM_NAME "
            'Return dbService.List(ConnectOneWS.Tables.OTHER_PROFILE_INFO, Query, ConnectOneWS.Tables.OTHER_PROFILE_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "@UserID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, param.PrevYearID, inBasicParam.openUserID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths As Integer() = {5, 4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.OTHER_PROFILE_INFO, "sp_get_Opening_Balances_Listing", ConnectOneWS.Tables.OTHER_PROFILE_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetDuplicateCount(ByVal param As Parameter_OpeningBalances_GetList, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM Other_Profile_Info WHERE  REC_STATUS IN (0,1,2) AND OP_ITEM_ID ='" & Trim(UCase(param.ItemID)) & "' AND  OP_CEN_ID =" & inBasicParam.openCenID.ToString & " AND OP_YEAR_ID =" & param.YearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.OTHER_PROFILE_INFO, OnlineQuery, ConnectOneWS.Tables.OTHER_PROFILE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Insert(ByVal InParam As Parameter_Insert_OpeningBalances, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO OTHER_PROFILE_INFO(OP_CEN_ID,OP_YEAR_ID,OP_NAME,OP_ITEM_ID,OP_AMOUNT,OP_DEBIT_CREDIT,OP_REMARKS," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,OP_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & InParam.openYearID.ToString & "," & _
                                                  "'" & ProfileName & "', " & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  " " & InParam.Amount & ", " & _
                                                  "'" & InParam.DebitCredit & "', " & _
                                                  "'" & InParam.OtherDetails & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.OTHER_PROFILE_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        Public Shared Function Update(ByVal UpParam As Parameter_Update_OpeningBalances, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE OTHER_PROFILE_INFO SET " & _
                                         "OP_ITEM_ID        ='" & UpParam.ItemID & "', " & _
                                         "OP_AMOUNT         = " & UpParam.Amount & ", " & _
                                         "OP_DEBIT_CREDIT   ='" & UpParam.DebitCredit & "', " & _
                                         "OP_REMARKS        ='" & UpParam.OtherDetails & "', " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.OTHER_PROFILE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

    End Class

End Namespace
