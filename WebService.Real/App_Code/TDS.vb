Imports Microsoft.VisualBasic
Imports System.Data

Namespace Real
    <Serializable>
    Public Class TDS
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_GetTDSRegister
            Public InsttID As String
            Public CenID As Integer
        End Class
        <Serializable>
        Public Class Parameter_GetTDS_Deducted_Not_Sent
            ' Public InsttID As String
            Public Tr_M_ID As String
            Public CEN_ID As Integer
            Public FromDate As Date
        End Class
#End Region

        Public Shared Function GetTDSRegister(ByVal inParam As Parameter_GetTDSRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_tds_register"
            Dim params() As String = {"CENID", "YEARID", "INSID"}
            Dim values() As Object = {IIf(inParam.CenID = 0, Nothing, inParam.CenID), inBasicParam.openYearID, inParam.InsttID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 5}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function InsertTDSDeductionMapping(ByVal InParam As Voucher_Internal_Transfer.Parameter_InsertTDSDeduction_VoucherInternalTransfer(), inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            For Each cParam As Voucher_Internal_Transfer.Parameter_InsertTDSDeduction_VoucherInternalTransfer In InParam
                If Not cParam Is Nothing Then If Not Voucher_Internal_Transfer.InsertTDSDeduction(cParam, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            Next
            Return True
        End Function

        Public Shared Function GetTDS_Deducted_Not_Sent(ByVal InParam As Parameter_GetTDS_Deducted_Not_Sent, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_TDS_Deducted_Not_Sent"
            Dim params() As String = {"CENID", "YEARID", "SENT_TR_M_ID"}
            Dim values() As Object = {InParam.CEN_ID, inBasicParam.openYearID, InParam.Tr_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)

            'Dim Query As String = ""
            'Query = " SELECT Txn_Date,Party,Bill_Amount as 'Dr Amount', TDS_Deducted, Bill_Amount - TDS_Deducted AS Remaining_Amount, TDS_Already_Sent, TDS_Send, REC_ID  FROM ( SELECT TR_DATE as Txn_Date , C_NAME as Party, (SELECT SUM(TR_AMOUNT) FROM transaction_info WHERE TR_M_ID = TM.REC_ID AND TR_DR_LED_ID IS NOT NULL) as Bill_Amount, (SELECT SUM(TR_AMOUNT) FROM transaction_info WHERE TR_CR_LED_ID = '00075' AND TR_M_ID = TM.REC_ID) as TDS_Deducted,  " & _
            '" COALESCE(TDS_AL_SENT.TDS_SENT,0) as TDS_Already_Sent, COALESCE(TDS.TDS_SENT,0) as TDS_Send, TM.REC_ID " & _
            '" FROM TRANSACTION_D_MASTER_INFO AS TM " & _
            '" INNER JOIN centre_info AS CI ON TM.TR_CEN_ID = CEN_ID " & _
            '" LEFT OUTER JOIN ( SELECT TR_M_ID, MAX(TR_AB_ID_1)AS AB_ID FROM transaction_info ti  WHERE TR_CEN_ID IN ('" & InParam.CEN_ID & "') AND TR_AB_ID_1 IS NOT NULL GROUP BY TR_M_ID) AS AB_ID ON TM.REC_ID = AB_ID.TR_M_ID " & _
            '" LEFT OUTER JOIN ADDRESS_BOOK AS AB ON AB_ID.AB_ID = AB.REC_ID	" & _
            '" LEFT OUTER JOIN transaction_d_tds_info AS TDS ON TDS.REF_TR_M_ID = TM.REC_ID AND TDS.TR_M_ID = '" & InParam.Tr_M_ID & "' " & _
            '" LEFT OUTER JOIN " & _
            '" (	SELECT REF_TR_M_ID, SUM(TDS_SENT) AS TDS_SENT  FROM transaction_d_tds_info ti WHERE TR_M_ID <> '" & InParam.Tr_M_ID & "' AND TR_CEN_ID IN ('" & InParam.CEN_ID & "')  GROUP BY REF_TR_M_ID ) AS TDS_AL_SENT ON TM.REC_ID = TDS_AL_SENT.REF_TR_M_ID " & _
            '" WHERE (COALESCE(TDS_AL_SENT.TDS_SENT,0) <> TR_SUB_AMT OR TDS.TR_M_ID = '" & InParam.Tr_M_ID & "') AND TM.TR_CEN_ID IN ('" & InParam.CEN_ID & "')  " & _
            '" AND TM.REC_ID IN (SELECT TR_M_ID FROM transaction_info WHERE COALESCE(TR_CR_LED_ID,'') = '00075')) AS A WHERE (TDS_Deducted - TDS_Already_Sent) <> 0 AND TR_COD_YEAR_ID BETWEEN (" & inBasicParam.openYearID & " - 101) AND (" & inBasicParam.openYearID & " + 101)  ORDER BY Txn_Date "
            'Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetTDS_Deducted_Not_Paid(ByVal InParam As Parameter_GetTDS_Deducted_Not_Sent, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_TDS_Deducted_Not_Paid"
            Dim params() As String = {"CENID", "YEARID", "PAID_TR_M_ID", "FROM_DATE"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.Tr_M_ID, InParam.FromDate}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime}
            Dim lengths() As Integer = {5, 4, 36, 30}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)


            'Dim Query As String = ""
            'Query = " SELECT Txn_Date,declared_date,Center, Cen_UID, Party,Bill_Amount as 'Dr Amount', TDS_Deducted, Bill_Amount - TDS_Deducted AS Remaining_Amount, TDS_Already_Paid, TDS_Paid, REC_ID  FROM ( SELECT TR_DATE as Txn_Date ,TM.TR_COD_YEAR_ID, C_NAME as Party,Cen_Name as Center, Cen_UID, (SELECT SUM(TR_AMOUNT) FROM transaction_info WHERE TR_M_ID = TM.REC_ID AND TR_DR_LED_ID IS NOT NULL) as Bill_Amount,COALESCE(TDS.TDS_DECLARED_DATE,TR_DATE) as declared_date, (SELECT SUM(TR_AMOUNT) FROM transaction_info WHERE TR_CR_LED_ID = '00075' AND TR_M_ID = TM.REC_ID) as TDS_Deducted,  " & _
            '" COALESCE(TDS_AL_PAID.TDS_PAID,0) as TDS_Already_Paid, COALESCE(TDS.TDS_PAID_GOVT,0) as TDS_Paid, TM.REC_ID " & _
            '" FROM TRANSACTION_D_MASTER_INFO AS TM " & _
            '" INNER JOIN centre_info AS CI ON TM.TR_CEN_ID = CEN_ID " & _
            '" LEFT OUTER JOIN ( SELECT TR_M_ID, MAX(TR_AB_ID_1)AS AB_ID FROM transaction_info ti  WHERE TR_CEN_ID IN (SELECT CEN_ID FROM centre_info WHERE CEN_INS_ID IN (SELECT CEN_INS_ID FROM CENTRE_INFO WHERE CEN_ID =  '" & inBasicParam.openCenID & "')) AND TR_AB_ID_1 IS NOT NULL GROUP BY TR_M_ID) AS AB_ID ON TM.REC_ID = AB_ID.TR_M_ID " & _
            '" LEFT OUTER JOIN ADDRESS_BOOK AS AB ON AB_ID.AB_ID = AB.REC_ID	" & _
            '" LEFT OUTER JOIN transaction_d_tds_info AS TDS ON TDS.REF_TR_M_ID = TM.REC_ID AND TDS.TR_M_ID = '" & InParam.Tr_M_ID & "' " & _
            '" LEFT OUTER JOIN " & _
            '" (	SELECT REF_TR_M_ID, SUM(TDS_PAID_GOVT) AS TDS_PAID  FROM transaction_d_tds_info ti WHERE TR_M_ID <> '" & InParam.Tr_M_ID & "' AND TR_CEN_ID IN (SELECT CEN_ID FROM centre_info WHERE CEN_INS_ID IN (SELECT CEN_INS_ID FROM CENTRE_INFO WHERE CEN_ID =  '" & inBasicParam.openCenID & "')) GROUP BY REF_TR_M_ID ) AS TDS_AL_PAID ON TM.REC_ID = TDS_AL_PAID.REF_TR_M_ID " & _
            '" WHERE (COALESCE(TDS_AL_PAID.TDS_PAID,0) <> TR_SUB_AMT OR TDS.TR_M_ID = '" & InParam.Tr_M_ID & "') AND TM.TR_CEN_ID  IN (SELECT CEN_ID FROM centre_info WHERE CEN_INS_ID IN (SELECT CEN_INS_ID FROM CENTRE_INFO WHERE CEN_ID =  '" & inBasicParam.openCenID & "'))  " & _
            '" AND TM.REC_ID IN (SELECT TR_M_ID FROM transaction_info WHERE COALESCE(TR_CR_LED_ID,'') = '00075')) AS A WHERE (TDS_Deducted - TDS_Already_Paid) <> 0 AND TR_COD_YEAR_ID BETWEEN (" & inBasicParam.openYearID & " - 101) AND (" & inBasicParam.openYearID & " + 101)  ORDER BY Txn_Date "
            'Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function
    End Class
End Namespace