Imports System.Data

Namespace Real
    <Serializable>
    Public Class Deposit_Slip

#Region "Param Classes"
        <Serializable>
        Public Class Param_Get_Deposit_slip_All_Report
            Public Bank_ID As String
            Public From_Date As DateTime
            Public To_Date As DateTime
            Public SlipNo As Integer
        End Class

        <Serializable>
        Public Class Param_MarkDepositSlipAsPrinted
            Public REC_ID As String = ""
            Public Deposit_Date As DateTime
        End Class
        <Serializable>
        Public Class Param_GetSlipTxnCount
            Public SlipNo As Integer
            Public Dep_Bank_ID As String
            Public MID As String
            Public REC_ID As String
        End Class
        <Serializable>
        Public Class Param_GetDepositSlipList
            Public SlipNo As Integer
            Public Dep_BA_ID As String
        End Class
#End Region

        Public Shared Function GetList(inParam As Param_GetDepositSlipList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "SLIP_NO", "DEP_BA_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.SlipNo, inParam.Dep_BA_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 2, 36}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_Deposit_Slip", ConnectOneWS.Tables.TRANSACTION_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
            Return Data
        End Function

        Public Shared Function GetSlipReport(SlipID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "SLIPID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, SlipID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 36}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Deposit_Slip", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetSlipAllReport(inParam As Param_Get_Deposit_slip_All_Report, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "BA_ID", "FROM_DATE", "TO_DATE", "SLIPNO"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Bank_ID, inParam.From_Date, inParam.To_Date, inParam.SlipNo}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String], DbType.DateTime, DbType.DateTime, DbType.Int32}
            Dim lengths As Integer() = {5, 4, 36, 11, 11, 8}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_Deposit_Slip_All", paramters, values, dbTypes, lengths, inBasicParam)
            If Data Is Nothing Then Return Nothing
            If Data.Tables.Count = 0 Then Return Nothing
            Return Data
        End Function

        Public Shared Function GetMaxOpenSlipNo(BA_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "BA_REC_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, BA_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 36}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_Max_Deposit_Slip_No", ConnectOneWS.Tables.TRANSACTION_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
            Return Data.Rows(0)(0)
        End Function

        Public Shared Function GetSlipTxnCount(inparam As Param_GetSlipTxnCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "SLIP_NO", "TR_M_ID", "TR_REC_ID", "DEP_BA_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inparam.SlipNo, inparam.MID, inparam.REC_ID, inparam.Dep_Bank_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 15, 36, 36, 36}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_Deposit_Slip_Txn_count", ConnectOneWS.Tables.TRANSACTION_INFO.ToString, paramters, values, dbTypes, lengths, inBasicParam)
            Return Data.Rows(0)(0)
        End Function


        Public Shared Function MarkAsPrinted(inParam As Param_MarkDepositSlipAsPrinted, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " UPDATE slip_info SET SL_PRINT_DATE = '" & inParam.Deposit_Date.ToString(Common.Server_Date_Format_Long) & "' " &
                                         " Where  ID ='" & inParam.REC_ID & "' ; "
            dbService.Update(ConnectOneWS.Tables.SLIP_INFO, onlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function MarkAsUnPrinted(REC_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " UPDATE slip_info SET SL_PRINT_DATE = NULL " &
                                         " Where  REC_ID ='" & REC_ID & "' ; "
            dbService.Update(ConnectOneWS.Tables.SLIP_INFO, onlineQuery, inBasicParam)
            Return True
        End Function

    End Class
End Namespace
