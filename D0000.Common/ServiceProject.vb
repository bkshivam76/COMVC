'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class ServiceProject
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        <Serializable>
        Public Class Param_Insert_Service_Project
            Public ProjName As String
            Public FromDate As String
            Public ToDate As String
            Public Admin As String
            Public Status As Boolean
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Update_Service_Project
            Public ProjName As String
            Public FromDate As String
            Public ToDate As String
            Public Admin As String
            Public Status As Boolean
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Insert_Notification
            Public Title As String
            Public SubTitle As String
            Public URL As String
            Public ImageURL As String
            Public ProjectID As String
            Public ToUsers As String
        End Class

        'Shifted

        Public Function get_projectInfo() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@UserID"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 255}
            Return _RealService.ListFromSP(Tables.SERVICE_PROJECT_INFO, "[sp_get_serviceProjects]", Tables.SERVICE_PROJECT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function get_project_users_Info(projid As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@PROJECT_ID", "@CEN_ID"}
            Dim values() As Object = {projid, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_PROJECT_INFO, "[sp_get_mappedSubjectsToProject]", Tables.SERVICE_PROJECT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function get_subjectsForMapping(ByVal Project_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@PROJECT_ID"}
            Dim values() As Object = {cBase._open_Cen_ID, Project_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 36}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_INFO, "[sp_get_subjectsForMapping]", Tables.SERVICE_USERS_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function get_allResponsesForProject(ByVal Project_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@PROJECT_ID"}
            Dim values() As Object = {Project_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_get_sparcSurveyFormResponses]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function get_allResponsesTimeForProject(ByVal Project_ID As String, Optional Chart_ID As Integer? = Nothing, Optional UserID As Integer? = Nothing, Optional User_AB_ID As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@PROJECT_ID", "@CHART_ID", "@UserID", "@User_AB_ID"}
            Dim values() As Object = {Project_ID, Chart_ID, UserID, User_AB_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {36, 4, 4, 36}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_get_sparcSurveyFormResponsesTime]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function get_DeviceTokenOfUsers(ByVal CommaSeperatedUserIDs As String) As DataTable
            Dim userid_split As String() = CommaSeperatedUserIDs.Split(",")
            Dim convertedUserid As String = ""
            If userid_split IsNot Nothing And userid_split.Length > 0 Then
                For i = 0 To userid_split.Length - 1
                    If i = 0 Then
                        convertedUserid = convertedUserid & "'" & userid_split(i) & "'"
                    Else
                        convertedUserid = convertedUserid & ",'" & userid_split(i) & "'"
                    End If
                Next
            End If
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@USER_Ids"}
            Dim values() As Object = {convertedUserid}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {-1}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_INFO, "[sp_get_deviceTokenForUsers]", Tables.SERVICE_USERS_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function Delete_subjectsMappedToProject(ByVal Project_ID As String) As Boolean
            Return DeleteByCondition("SUCM_PROJECT_ID  = '" + Project_ID + "'", Tables.SERVICE_USERS_PROJECT_MAPPING, ClientScreen.Facility_ServiceProject)
        End Function
        Public Function insert_subjectMappingToProject(ByVal User_ID As String, ByVal Project_ID As String, Optional UnmapUsers As Boolean = True) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_subjectMappingToProject"
            Dim params() As String = {"@USER_ID", "@PROJECT_ID", "@UNMAP_USER"}
            Dim values() As Object = {User_ID, Project_ID, UnmapUsers}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, DbType.Boolean}
            Dim lengths() As Integer = {8000, 36, 2}
            'used public insert function as there are no transactional data involved 
            _RealService.InsertBySPPublic(Tables.SERVICE_USERS_PROJECT_MAPPING, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
            Return True
        End Function
        Public Function GetMappedUsersWithChartResponsesForProject(projid As String, Optional ExcludechartID As String = Nothing) As DataTable
            ExcludechartID = If(String.IsNullOrWhiteSpace(ExcludechartID), Nothing, ExcludechartID)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Get_MappedUsers_WithChartResponses_ForProject]"
            Dim params() As String = {"@ProjID", "@ExcludeChartID"}
            Dim values() As Object = {projid, ExcludechartID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_PROJECT_INFO, SPName, Tables.SERVICE_PROJECT_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function GetMappedUsersWithChartForProject(projid As String, Optional ExcludechartID As String = Nothing) As DataTable
            ExcludechartID = If(String.IsNullOrWhiteSpace(ExcludechartID), Nothing, ExcludechartID)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Get_MappedUsers_WithChart_ForProject]"
            Dim params() As String = {"@ProjID", "@ExcludeChartID"}
            Dim values() As Object = {projid, ExcludechartID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_PROJECT_INFO, SPName, Tables.SERVICE_PROJECT_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function DeleteSubject(projid As String, userID As String, user_ab_id As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_delete_user_project_chart_mapping]"
            Dim params() As String = {"@SERVICE_USER_ID", "@SERVICE_USER_AB_ID", "@PROJECT_ID"}
            Dim values() As Object = {Convert.ToInt32(userID), user_ab_id, projid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 36}
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_USERS_PROJECT_MAPPING, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function GetProjectAdmins() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.CLIENT_USER_INFO, "select USER_ID as NAME,USER_ID as ID from client_user_info where REC_STATUS<>-1 and CEN_ID= " & cBase._open_Cen_ID & " order by NAME", Tables.CLIENT_USER_INFO.ToString, inbasicparam)
            'Return _RealService.List(Tables.ADDRESS_BOOK, "select C_NAME as NAME,REC_ID as ID from address_book where REC_STATUS<>-1 and C_CEN_ID= " & cBase._open_Cen_ID & " and C_COD_YEAR_ID= " & cBase._open_Year_ID & " order by NAME", Tables.ADDRESS_BOOK.ToString, inbasicparam)
        End Function
        Public Function CheckIfProjectNameIsUniqueInUID(ByVal ProjectName As String, Optional ByVal ProjID As String = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_service_project_uniqueness]"
            Dim params() As String = {"@ProjName", "@projID", "@cen_id"}
            Dim values() As Object = {ProjectName, ProjID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {-1, 36, 4}
            Return _RealService.ScalarFromSP(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function InsertServiceProject(Inparam As Param_Insert_Service_Project) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_Service_Project]"
            Dim params() As String = {"@ProjName", "@FromDate", "@ToDate", "@Admin", "@Status", "@Rec_ID", "@Cen_ID", "@user_id"}
            Dim values() As Object = {Inparam.ProjName, Inparam.FromDate, Inparam.ToDate, Inparam.Admin, Inparam.Status, Inparam.Rec_ID, cBase._open_Cen_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.DateTime, DbType.DateTime, DbType.String, DbType.Boolean, DbType.String, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {-1, 12, 255, 36, 2, 36, 4, 255}

            _RealService.InsertBySPPublic(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
            Return True
        End Function
        Public Function UpdateServiceProject(Inparam As Param_Update_Service_Project) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Update_Service_Project]"
            Dim params() As String = {"@ProjName", "@FromDate", "@ToDate", "@Admin", "@Status", "@Rec_ID", "@Cen_ID", "@user_id"}
            Dim values() As Object = {Inparam.ProjName, Inparam.FromDate, Inparam.ToDate, Inparam.Admin, Inparam.Status, Inparam.Rec_ID, cBase._open_Cen_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.DateTime, DbType.DateTime, DbType.String, DbType.Boolean, DbType.String, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {-1, 12, 255, 36, 2, 36, 4, 255}

            _RealService.UpdateBySPPublic(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
            Return True
        End Function
        Public Function DeleteServiceProject(projID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ServiceProject)
            _RealService.DeleteByCondition(Tables.SERVICE_USERS_PROJECT_MAPPING, "SUCM_PROJECT_ID ='" & projID & "'", inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_PROJECT_INFO, "REC_ID='" & projID & "'", inbasicparam)
            Return True
        End Function
        Public Function DeleteFullServiceProject(ByVal PROJECT_ID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_delete_full_service_project"
            Dim params() As String = {"@PROJECT_ID"}
            Dim values() As Object = {PROJECT_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function GetServiceProjectLinked(projID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_service_project_linked]"
            Dim params() As String = {"@projID"}
            Dim values() As Object = {projID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.SERVICE_PROJECT_INFO, SPName, Tables.SERVICE_PROJECT_INFO.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function GetServiceProjectRecord(projID As String) As DataTable
            Dim condition As String = "WHERE rec_id = '" & projID & "'"
            Return GetRecordByCustom(condition, ClientScreen.Facility_ServiceProject, RealTimeService.Tables.SERVICE_PROJECT_INFO)
        End Function
        Public Function InsertNotification(Inparam As Param_Insert_Notification) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_notification]"
            Dim params() As String = {"@NI_CEN_ID", "@NI_PROJECT_ID", "@NI_TITLE", "@NI_SUBTITLE", "@NI_URL", "@NI_IMAGE_URL", "@NI_USERS", "@REC_ADD_BY", "@REC_EDIT_BY"}
            Dim values() As Object = {cBase._open_Cen_ID, Inparam.ProjectID, Inparam.Title, Inparam.SubTitle, Inparam.URL, Inparam.ImageURL, Inparam.ToUsers, cBase._open_User_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 8000, 8000, 2500, 2500, 8000, 255, 255}
            'Return _RealService.InsertBySPPublic(Tables.NOTIFICATION_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
            Return _RealService.ListFromSP(Tables.NOTIFICATION_INFO, SPName, Tables.NOTIFICATION_INFO.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function Get_ServiceProjects_For_DD(Optional FilterByUser As Boolean = True) As DataTable
            Return GetDataFromTables("SERVICE_PROJECT_INFO", cBase._open_Cen_ID, cBase._open_User_ID, FilterByUser)
        End Function
        Public Function Get_Forms_For_DD(Optional ProjectID As String = Nothing, Optional FilterByUser As Boolean = True) As DataTable
            Return GetDataFromTables("SERVICE_CHART_INFO", cBase._open_Cen_ID, cBase._open_User_ID, FilterByUser, ProjectID)
        End Function
        Public Function UpdateChartProject(ChartID As Integer, ProjectID As String, ChartName As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Update_Chart_Project]"
            Dim params() As String = {"@Chart_ID", "@Project_ID", "@UserID", "@CHARTNAME"}
            Dim values() As Object = {ChartID, ProjectID, cBase._open_User_ID, ChartName}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 255}
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function Get_Subjects_From_Project_Form_DD(Optional ProjectID As String = Nothing, Optional FormId As Integer = Nothing, Optional FilterByUser As Boolean = True) As DataTable
            If String.IsNullOrWhiteSpace(ProjectID) = False Then
                Return GetDataFromTables("SERVICE_USERS_PROJECT_MAPPING", cBase._open_Cen_ID, cBase._open_User_ID, FilterByUser, ProjectID)
            End If
            If String.IsNullOrWhiteSpace(FormId) = False Then
                Return GetDataFromTables("SERVICE_USERS_CHART_MAPPING", cBase._open_Cen_ID, cBase._open_User_ID, FilterByUser, Nothing, FormId)
            End If
            Return Nothing
        End Function
        Public Function UpdateFormFrequency(FormID As Int32, StartDate As Nullable(Of DateTime), EndDate As Nullable(Of DateTime), ChartEndDatePrevious As Nullable(Of DateTime), ChartFrequencyFrevious As String, ScheduleId As Int32, Optional StartDateChange As Boolean = False, Optional Frequency As String = "Custom") As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Update_Chart_Frequency]"
            Dim params() As String = {"@FREQUENCY", "@Chart_REC_ID", "@STARTDATE", "@ENDDATE", "@START_DATE_CHANGE", "@CHART_END_DATE_PREVIOUS", "@CHART_FREQUENCY_PREVIOUS", "@SCHEDULE_ID", "@CEN_ID", "@USER_ID"}
            Dim values() As Object = {Frequency, FormID, StartDate, EndDate, StartDateChange, ChartEndDatePrevious, ChartFrequencyFrevious, ScheduleId, cBase._open_Cen_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Boolean, DbType.DateTime2, DbType.String, DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {255, 4, 11, 11, 4, 12, 255, 4, 4, 255}
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_PROJECT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
    End Class
#End Region
End Class
