Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class ServiceChart
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function get_karunaSankalpChart_ParticipantDetails(AB_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@AB_ID"}
            Dim values() As Object = {AB_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.ADDRESS_BOOK, "[sp_get_karunaSankalpChart_ParticipantDetails]", Tables.ADDRESS_BOOK.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function insert_karunaSankalpChart_Responses(AB_ID As String, ChartSrNo As Integer, Response As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@AB_ID", "@CHARTSRNO", "@RESPONSE"}
            Dim values() As Object = {AB_ID, ChartSrNo, Response}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {36, 4, 8000}
            _RealService.InsertBySPPublic(Tables.SERVICE_CHART_RESPONSES, "[sp_insert_karunaSankalpChart_Responses]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.KarunaSankalpChart))
            Return True
        End Function

        Public Function GetKarunaChartEmailIDs(Optional StartAlphabet As String = "1", Optional EndAlphabet As String = "Z") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.ADDRESS_BOOK, "SELECT C_EMAIL_ID_1, ab.REC_ID as ab_id FROM tbl_karunasankalp karuna INNER JOIN address_book AS AB ON KARUNA.KS_AB_ID = AB.REC_ID WHERE LEN(COALESCE(C_EMAIL_ID_1,''))>0 AND LEFT(LTRIM(C_EMAIL_ID_1),1) >= '" & StartAlphabet & "' AND LEFT(LTRIM(C_EMAIL_ID_1),1) <= '" & EndAlphabet & "' ORDER BY LEFT(LTRIM(C_EMAIL_ID_1),1)", Tables.ADDRESS_BOOK.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Return DataBase_xls
        End Function
        Public Function GetResponseForChartInstance(ChartInstanceID As Integer, CenterID As Integer) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.SERVICE_CHART_RESPONSES, "SELECT DISTINCT CR_RESPONSE_ID FROM service_chart_responses WHERE CR_CHART_SR_ID = " + ChartInstanceID.ToString() + " AND CR_CONE_CEN_ID = " + CenterID.ToString() + "", Tables.SERVICE_CHART_RESPONSES.ToString(), GetBaseParams(ClientScreen.Options_FormResponse))
            Return DataBase_xls
        End Function
    End Class

End Class
