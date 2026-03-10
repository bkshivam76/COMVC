'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class Chart
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        'Public Function updateChartResponseStatus(ByVal responseID As String, ByVal recStatus As Integer) As Boolean
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim SPName As String = "sp_update_chartResponseStatus"
        '    Dim params() As String = {"@REC_STATUS", "@RESPONSE_ID"}
        '    Dim values() As Object = {recStatus, responseID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
        '    Dim lengths() As Integer = {4, 36}
        '    'used public update function as there are no transactional data involved 
        '    Return _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        'End Function
        'Public Function get_chartInfo() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim paramters As String() = {"@CEN_ID"}
        '    Dim values() As Object = {cBase._open_Cen_ID}
        '    Dim dbTypes() As System.Data.DbType = {DbType.Int32}
        '    Dim lengths() As Integer = {4}
        '    Return _RealService.ListFromSP(Tables.SERVICE_CHART_INFO, "[sp_get_serviceCharts]", Tables.SERVICE_CHART_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        'End Function

        'Public Function get_chartResponses(ByVal chartInstanceID As Int32) As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim paramters As String() = {"@CHART_INSTANCE_ID"}
        '    Dim values() As Object = {chartInstanceID}
        '    Dim dbTypes() As System.Data.DbType = {DbType.Int32}
        '    Dim lengths() As Integer = {4}
        '    Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartResponses]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        'End Function
        'Public Function GetFormSubmissionConfirmation(ByVal FormInstanceId As Int32, ByVal ResponseId As String) As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)

        '    Dim SPName As String = "[sp_get_Chart_Response_confirmation]"
        '    Dim params() As String = {"@RESPONSE_ID", "@CHART_INSTANCE_ID"}
        '    Dim values() As Object = {ResponseId, FormInstanceId}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
        '    Dim lengths() As Integer = {36, 4}

        '    Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_INFO, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)


        '    'Return GetRecordByCustom("WHERE CQ_CHART_ID ='" & FormId & "' ", ClientScreen.Options_FormResponse, RealTimeService.Tables.SERVICE_CHART_QUESTIONS)
        'End Function

        'Public Function GetFormRegistrationSlip(ByVal FormInstanceId As Int32, ByVal ResponseId As String) As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
        '    Dim SPName As String = "[sp_get_Registration_Slip]"
        '    Dim params() As String = {"@RESPONSE_ID", "@CHART_INSTANCE_ID"}
        '    Dim values() As Object = {ResponseId, FormInstanceId}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
        '    Dim lengths() As Integer = {36, 4}
        '    Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_INFO, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)
        'End Function
    End Class
#End Region
End Class
