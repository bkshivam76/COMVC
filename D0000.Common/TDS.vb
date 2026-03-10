Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class TDS
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetTDSRegister() As DataTable
            Dim param As Parameter_GetTDSRegister = New Parameter_GetTDSRegister
            param.InsttID = cBase._open_Ins_ID
            If Not cBase.Is_HQ_Centre Then param.CenID = cBase._open_Cen_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.TDS_GetTDSRegister, ClientScreen.Account_TDS_Register, param)
        End Function
        Public Function Insert_TDS_Deduction(ByVal InParam As Parameter_InsertTDSDeduction_VoucherInternalTransfer()) As Boolean
            For Each cParam As Parameter_InsertTDSDeduction_VoucherInternalTransfer In InParam
                If Not cParam Is Nothing Then
                    UnmapTDSRecd(cParam.TxnMID)
                    UnmapTDSPaid(cParam.TxnMID)
                End If
            Next
            Return InsertRecord(RealTimeService.RealServiceFunctions.TDS_InsertTDSDeduction, ClientScreen.Account_TDS_Register, InParam)
        End Function
        Public Function GetTDS_Deducted_Not_Sent(Tr_M_ID As String, CEN_ID As Integer) As DataTable
            Dim param As Parameter_GetTDS_Deducted_Not_Sent = New Parameter_GetTDS_Deducted_Not_Sent
            param.CEN_ID = CEN_ID
            param.Tr_M_ID = Tr_M_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.TDS_GetTDS_Deducted_Not_Sent, ClientScreen.Account_TDS_Register, param)
        End Function
        Public Function GetTDS_Deducted_Not_Paid(Tr_M_ID As String, CEN_ID As Integer, fromDate As DateTime) As DataTable
            Dim param As Parameter_GetTDS_Deducted_Not_Sent = New Parameter_GetTDS_Deducted_Not_Sent
            param.CEN_ID = CEN_ID
            param.Tr_M_ID = Tr_M_ID
            param.FromDate = fromDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.TDS_GetTDS_Deducted_Not_Paid, ClientScreen.Account_TDS_Register, param)
        End Function
        Public Function UnmapTDSRecd(Recd_M_ID As String) As Boolean
            DeleteByCondition("TR_M_ID = '" & Recd_M_ID & "'", Tables.TRANSACTION_D_TDS_INFO, ClientScreen.Account_TDS_Register)
            Return True
        End Function

        Public Function UnmapTDSPaid(Paid_M_ID As String) As Boolean
            DeleteByCondition("TR_M_ID = '" & Paid_M_ID & "'", Tables.TRANSACTION_D_TDS_INFO, ClientScreen.Account_TDS_Register)
            Return True
        End Function

        Public Function UnmapTDSDeducted(Ded_M_ID As String) As Boolean
            DeleteByCondition("REF_TR_M_ID = '" & Ded_M_ID & "'", Tables.TRANSACTION_D_TDS_INFO, ClientScreen.Account_TDS_Register)
            Return True
        End Function
    End Class
End Class
