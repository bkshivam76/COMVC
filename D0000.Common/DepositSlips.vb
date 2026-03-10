'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class DepositSlips
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetList(Optional SlipNo As Integer = 0, Optional Dep_BA_ID As String = Nothing) As DataTable
            Dim inparam As New RealTimeService.Param_GetDepositSlipList
            inparam.Dep_BA_ID = Dep_BA_ID
            inparam.SlipNo = SlipNo
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Deposit_Slip_GetList, ClientScreen.Profile_Deposit_Slips, inparam)
        End Function

        Public Function GetRecord(SlipNo As Integer, Dep_BA_ID As String) As DataTable
            Return GetRecordByCustom(" WHERE SL_NO = " & SlipNo.ToString & " AND SL_CEN_ID = " & cBase._open_Cen_ID.ToString & " AND SL_COD_YEAR_ID = " & cBase._open_Year_ID.ToString & " " & " AND SL_BA_REC_ID = '" & Dep_BA_ID & "'", ClientScreen.Report_Deposit_Slips, Tables.SLIP_INFO)
        End Function

        Public Function GetMaxOpenSlipNo(BA_ID As String, screen As ClientScreen) As Object
            Return GetScalarBySP(RealServiceFunctions.Deposit_Slip_GetMaxOpenSlipNo, screen, BA_ID)
        End Function

        Public Function GetSlipTxnCount(SlipNo As Integer, Dep_BA_ID As String, screen As ClientScreen, Optional TR_M_ID As String = Nothing, Optional TR_REC_ID As String = Nothing) As Object
            Dim inparam As New RealTimeService.Param_GetSlipTxnCount
            inparam.SlipNo = SlipNo
            inparam.MID = TR_M_ID
            inparam.REC_ID = TR_REC_ID
            inparam.Dep_Bank_ID = Dep_BA_ID
            Return GetScalarBySP(RealServiceFunctions.Deposit_Slip_GetSlipTxnCount, screen, inparam)
        End Function

        Public Function GetSlipReport(SlipID As String) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Deposit_Slip_GetSlipReport, ClientScreen.Report_Deposit_Slips, SlipID)
        End Function

        Public Function GetSlipAllReport(BankID As String, FromDate As DateTime, ToDate As DateTime, SlipNo As Integer) As DataSet
            Dim inParam As New Param_Get_Deposit_slip_All_Report
            inParam.Bank_ID = BankID
            inParam.From_Date = FromDate
            inParam.To_Date = ToDate
            inParam.SlipNo = SlipNo
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Deposit_Slip_GetSlipReportAll, ClientScreen.Report_Deposit_Slips, inParam)
        End Function

        Public Function GetSlipPrintStatus(TxnMID As String, screen As ClientScreen) As Boolean
            Dim Txn_slip As DataTable = GetRecordByCustom(" WHERE TR_M_ID = '" & TxnMID & "'", screen, Tables.TRANSACTION_D_SLIP_INFO)
            If Txn_slip.Rows.Count = 0 Then Return False
            Dim Slip As DataTable = GetRecordByCustom(" WHERE REC_ID = '" & Txn_slip.Rows(0)("TR_SLIP_ID").ToString & "'", screen, Tables.SLIP_INFO)
            If Slip.Rows.Count = 0 Then Return False
            If IsDBNull(Slip.Rows(0)("SL_PRINT_DATE")) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteByCondition("REC_ID = '" & Rec_Id & "'", Tables.SLIP_INFO, ClientScreen.Profile_Deposit_Slips)
        End Function

        Public Overloads Function MarkAsPrinted(ByVal Rec_Id As String, DepositDate As DateTime) As Boolean
            Dim inparam As New Param_MarkDepositSlipAsPrinted
            inparam.Deposit_Date = DepositDate
            inparam.REC_ID = Rec_Id
            Return UpdateRecord(RealServiceFunctions.Deposit_Slip_MarkAsPrinted, ClientScreen.Report_Deposit_Slips, inparam)
        End Function

        Public Overloads Function MarkAsUnprinted(ByVal Rec_Id As String) As Boolean
            Return UpdateRecord(RealServiceFunctions.Deposit_Slip_MarkAsUnPrinted, ClientScreen.Report_Deposit_Slips, Rec_Id)
        End Function
    End Class
#End Region
End Class
