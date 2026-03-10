'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class ServiceModule
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub


        Public Function Get_SM_EventsList(Start_Date As DateTime?, End_Date As DateTime?, CenterID As Int32?, speaker As String, ProjectID As String,
                                          ratingFrom As Int32?, ratingTo As Int32?, attachmentRootPath As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_EventsList"
            Dim paramters As String() = {"@startdate", "@enddate", "@CenterID", "@speaker", "@ProjectID", "@ratingfrom", "@ratingto", "@attachment_root_path"}
            Dim values() As Object = {Start_Date, End_Date, CenterID, speaker, ProjectID, ratingFrom, ratingTo, attachmentRootPath}
            Dim dbTypes() As System.Data.DbType = {DbType.Date, DbType.Date, DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {12, 12, 6, 255, 255, 2, 2, 255}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_REPORT_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetCenters() As DataTable
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_ServicePlaces, RealTimeService.Tables.SERVICE_PLACE_INFO, Common.ClientDBFolderCode.SYS)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceModule_GetCenters, ClientScreen.Service_Module)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_ServiceModule) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ServiceReport_Update, ClientScreen.Facility_ServiceReport, UpParam)
        End Function


        Public Function Get_Membership_Confirmation_Email(ResponseId As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@ChartResponseId", "@Emailer"}
            Dim values() As Object = {ResponseId, "ApprovalNotification"}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
            Dim lengths() As Integer = {36, 50}
            Dim _table As DataTable = _RealService.ListFromSP(Tables.SERVICE_CHART_INFO, "sp_Get_Membership_Confirmation_Email", Tables.SERVICE_CHART_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
            Return _table
        End Function


        Public Function get_roomsVisualizationData(ByVal cenid As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_RoomsVisualizationData"
            Dim paramters As String() = {"@CENID", "@FROM_DATE", "@TO_DATE"}
            Dim values() As Object = {cenid, fromdate, todate}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.DateTime2, DbType.DateTime2}
            Dim lengths() As Integer = {10, 50, 50}
            Return _RealService.ListDatasetFromSP(Tables.ASSET_LOCATION_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Sub UpdateRoomRemarks(ByVal RoomRecID As String, ByVal remarks As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_Accommodation_Short)
            '_RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "CRAD_RESPONSE_ID ='" & ChartResponseID & "'", inbasicparam)
            Dim updateRoom_Remarks As String = "UPDATE ASSET_LOCATION_INFO SET AL_ROOM_REMARKS = '" & remarks & "' WHERE REC_ID = '" & RoomRecID & "' and AL_CEN_ID = " & cBase._open_Cen_ID
            'This is to update the arrival status to unarrived .
            _RealService.List(Tables.ASSET_LOCATION_INFO, updateRoom_Remarks, Tables.ASSET_LOCATION_INFO.ToString, GetBaseParams(ClientScreen.Facility_Accommodation_Short)) ' this query is actually update call 
        End Sub
    End Class
#End Region
End Class
