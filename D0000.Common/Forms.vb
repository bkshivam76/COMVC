'SQL, Shifted
Imports System.Configuration
Imports System.IO
Imports Common_Lib.RealTimeService
Imports DevExpress.CodeParser

Partial Public Class DbOperations
#Region "Option"
    <Serializable>
    Public Class Forms
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
#Region "Parameter Classes"
        <Serializable>
        Public Class Param_Form_Questions
            Public Form_ID As String = Nothing
            Public Question As String
            Public Mode As String
            Public Required As Boolean
            Public Type As String
            Public Min As Int64?
            Public Max As Int64?
            Public SrNo As Int32
            Public File_Name As String
            Public File As Byte()
            Public Rec_ID As String = Nothing
            Public Formula As String
            Public ImageWidth As Int32?
            Public ImageHeight As Int32?
            Public RowNo As Int32
            Public ColumnSpan As Int32
            Public GroupName As String
            Public DefaultValue As String
            Public Description As String
            Public DefaultVisibility As Boolean
            Public Tag As String
            Public Options As Param_Form_Question_Options() = Nothing
        End Class
        <Serializable>
        Public Class Param_Form_ProfileSettings
            Public Rec_ID As String = Nothing
            Public Field As String
            Public Visible As Boolean
            Public Enable As Boolean
            Public Mandatory As Boolean
            Public Form_ID As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Form_Question_Options
            Public Question_ID As String = Nothing
            Public Options As String
            Public OptionSrNo As Int32
            Public File_Name As String
            Public File As Byte()
            Public Rec_ID As String = Nothing
            Public Form_ID As String
            Public Dependent_Questions As String
            Public Dependent_Questions_Visibility As Boolean? = Nothing
            Public Points As Double? = Nothing
        End Class
        <Serializable>
        Public Class Param_Form_Master
            Public Title As String
            Public Description As String
            ''' <summary>
            ''' MOBILE / EMAIL / MOBILE Or EMAIL / NONE
            ''' </summary>
            Public LoginRequired As String
            Public Rec_ID As String = Nothing
            Public File_Name As String
            Public File As Byte()
            Public ImageWidth As Int32?
            Public ImageHeight As Int32?
            Public Project_ID As String = Nothing
            Public Frequency As String = "Once"
            Public Purpose As String = "REGISTRATION"
            Public Approval_Required As Boolean
            Public Generate_Reg_No As Boolean
            Public Confirmation_Message As String
            Public Reg_No_Format As String
            Public AllowResubmission As Boolean
            Public Login_File_Name As String
            Public Login_File As Byte()
            Public Thumbnail_File_Name As String
            Public Thumbnail_File As Byte()
            Public Responsive_File_Name As String
            Public Responsive_File As Byte()
            ''' <summary>
            ''' To be provided only if created in reference of a event
            ''' </summary>
            Public EventID As String = Nothing
            ''' <summary>
            ''' To be provided only if created in reference of a ServiceReport
            ''' </summary>
            Public ServiceReportID As String
            Public ApprovalMsg As String
            Public RejectionMsg As String
            Public PreRequiredSrnNo As String
            Public Generate_Reg_No_Approval As Boolean
            Public Name As String
            Public StartDate As String
            Public EndDate As String
            Public EndDateMsg As String
            Public StartDateMsg As String
            Public DisplayMode As String
            Public FormBgColor As String
            Public QuestionBgColor As String
            Public QuestionFgColor As String
            Public QuestionFontsize As String
            Public FormBgImagePath As String
            Public GrpTitleFontsize As String
            Public GrpTitleBgColor As String
            Public GrpTitleFgColor As String
            Public ActiveFrom As String
            Public ActiveTo As String
            Public CustomScheduleID As String
            Public NotificationOnInstanceCreation As Boolean
            Public MaxEntries As Int32?
            Public StartDateChange As Boolean = False
            Public MaxGroupRegistrations As Int32
            Public FormInstanceId As Int32 'because NotificationSettings obj can be set to null which will cause problem for instanceId assignment while deletion
        End Class
        <Serializable>
        Public Class Param_Insert_Form
            Public Master As Param_Form_Master
            Public ProfileSettings As Param_Form_ProfileSettings()
            Public GroupProfileSettings As Param_Form_ProfileSettings()
            Public Questions As Param_Form_Questions()
            Public NotificationSettings As Param_Form_Notification
        End Class
        <Serializable>
        Public Class Param_Update_Form
            Public Master As Param_Form_Master
            Public ProfileSettings As Param_Form_ProfileSettings()
            Public GroupProfileSettings As Param_Form_ProfileSettings()
            Public Questions As Param_Form_Questions()
            Public NotificationSettings As Param_Form_Notification
            Public DeleteQuestions_RecID As String() = Nothing
            Public DeleteOptions_RecID As String() = Nothing
        End Class

        <Serializable>
        Public Class Param_Form_Response
            Public Question_ID As String
            Public Response As String
            Public File_Name As String
            Public File As Byte()
            ' Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Insert_Form_Response
            Public AddBy As String
            Public Response_ID As String
            Public FormInstanceID As String
            Public ServiceUserID As Int32
            Public InFormResponse As Param_Form_Response()
        End Class
        <Serializable>
        Public Class Param_Insert_Chart_Visibility_Details
            Public Chart_ID As String
            Public Instt_ID As String
            Public Cen_ID As Int32?
            Public FY_From As Int32?
            Public FY_To As Int32?
            Public Acc_Type As String
            Public User_Type As String
            Public Menu As String
            Public Ins_All As Boolean
            Public Cen_All As Boolean
            Public AccType_All As Boolean
        End Class
        <Serializable>
        Public Class Param_Update_Chart_Visibility_Details
            Public Chart_ID As String
            Public Instt_ID As String
            Public Cen_ID As Int32?
            Public FY_From As Int32?
            Public FY_To As Int32?
            Public Acc_Type As String
            Public User_Type As String
            Public Menu As String
            Public Rec_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Create_Copy
            Public chart_name As String
            Public Title As String
            Public Description As String
            Public cenid As Integer
            Public startDate As String
            Public endDate As String
            Public serviceReportID As String
            Public eventID As String
            Public ProjectID As String
            Public ChartID As String
        End Class
        <Serializable>
        Public Class Param_Form_Notification
            Public Approval_Required As Boolean
            Public Name As String
            Public FormInstanceId As String
            Public IsEmailNotificationSelected As Boolean
            Public IsWhatsappNotificationSelected As Boolean
            Public IsSMSNotificationSelected As Boolean
            Public IsEmailConfirmSelected As Boolean
            Public IsEmailApproveSelected As Boolean
            Public IsEmailRejectSelected As Boolean
            Public IsWhatsAppConfirmSelected As Boolean
            Public IsWhatsAppApproveSelected As Boolean
            Public IsWhatsAppRejectSelected As Boolean
            Public IsSMSConfirmSelected As Boolean
            Public IsSMSApproveSelected As Boolean
            Public IsSMSRejectSelected As Boolean
            Public ConfirmationEmail_Message As String
            Public ApprovalEmail_Message As String
            Public RejectionEmail_Message As String
            Public ConfirmationWhatsApp_Message As String
            Public ConfirmationWhatsApp_Message_Template As String
            Public ApprovalWhatsApp_Message As String
            Public ApprovalWhatsApp_Message_Template As String
            Public RejectionWhatsApp_Message As String
            Public RejectionWhatsApp_Message_Template As String
            Public ConfirmationSMS_Message As String
            Public ApprovalSMS_Message As String
            Public RejectionSMS_Message As String
            Public Category As String
            Public AdminWhatsAppNo As String
            Public AdminEmail As String
            Public BatchName As String 'FormName with Category and Status
            Public CC As String
            Public BCC As String
            Public ReplyToEmail As String
            Public SenderEmailType As String
            Public Email As String
            Public Password As String
            Public SendNotifictionTypeSampleOrApply As String
            Public SenderWhatsappNoType As String
            Public Whatsappno As String
            Public Mobile As String
            Public DeliverySpeed As String
            Public Mode As String
        End Class
        <Serializable>
        Public Class Param_FormResponse_Notification
            Public ChartInstanceId As String
            Public ResponseStatus As String
            Public AdminWhatsAppNo As String
            Public AdminEmail As String
            Public Content As String
            Public BatchName As String
            Public Subject As String
            Public CC As String
            Public BCC As String
            Public ReplyToEmail As String
            Public SenderEmailType As String
            Public Email As String
            Public Password As String
            Public SendNotifictionTypeSampleOrNow As String
            Public AttachmentPath As String
            Public SenderWhatsappNoType As String
            Public Whatsappno As String
            Public DeliverySpeed As String
            Public Mode As String
            Public File_Name As String
            Public File As Byte()
            Public SelectedDesignationIds As String
            Public SelectedOccupationIds As String
            Public SelectedCategoryIds As String
            Public SelectedMagazineIds As String
            Public SelectedSpecialitiesIds As String
            Public SelectedEventsIds As String
            Public SelectedTitleIds As String
            Public SelectedWingIds As String
            Public SelectedCityIds As String
            Public SelectedDistrictIds As String
            Public SelectedNotificationRadio As String
            Public selectedFormResponses_group As String
            Public selectedAddresses_group As String
            Public selectedFormResponses_str As String
            Public selectedContacts_str As String
            Public enteredPhoneNumbersOrEmails_str As String
            Public CenID As Int32
            Public AddBy As String
        End Class
        <Serializable>
        Public Class Param_Insert_additional_info_address
            Public Mobile3 As String
            Public Mobile4 As String
            Public Mobile5 As String
            Public Email3 As String
            Public Email4 As String
            Public Email5 As String
            Public Rec_ID As String
            Public AB_Rec_ID As String
            Public File As Byte()
            Public File_Name As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_UserProfile
            Public param_InsertAdditionalUserProfileData As Param_Insert_additional_info_address
            Public param_InsertAddresses As Parameter_Insert_Addresses = Nothing
            Public param_UpdateAddresses As Parameter_Update_Addresses = Nothing
            Public InsertSpecialities() As Parameter_InsertSpecialities_Addresses = Nothing
            Public InsertEvents() As Parameter_InsertEvents_Addresses = Nothing
            Public AB_ID As String = Nothing
            Public serviceUserID As Int32? = Nothing
            Public LoginMode As String = Nothing
            Public Relation As String = Nothing
            Public DeleteSpecialities As Boolean
            Public DeleteEvents As Boolean
            Public ProjectID As String
            Public CenID As String
            Public YearID As String
            Public UserID As String
        End Class
#End Region
        Public Function Get_MinStart_And_MaxTo_Date_Of_Form_Instances(chartID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT Min(CSN_FROM) as 'MIN_CSN_FROM',MAX(CSN_TO) as 'MAX_CSN_TO' from service_chart_srno where CSN_CHART_ID=" & chartID
            Return _RealService.List(Tables.SERVICE_CHART_SRNO, query, Tables.SERVICE_CHART_SRNO.ToString, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function GetRegistrationFormsCreatedInProjectOnly(ProjID As String, Optional ExcludeChartID As String = Nothing) As DataTable
            Dim query As String = "SELECT * FROM service_chart_info CI INNER JOIN service_chart_srno AS INST ON CI.REC_ID = INST.CSN_CHART_ID WHERE CI_PROJECT_ID = '" & ProjID & "' AND LEN(COALESCE(CSN_EVENT_ID,''))=0 AND LEN(COALESCE(CSN_SERVICE_REPORT_ID,''))=0 AND CI_PURPOSE = 'REGISTRATION'"
            If String.IsNullOrWhiteSpace(ExcludeChartID) = False Then
                query = query & " AND CI.Rec_ID <> " & ExcludeChartID
            End If
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_CHART_INFO, query, Tables.SERVICE_CHART_INFO.ToString, inbasicparam)
        End Function
        Public Function GetBasicDetailsFormsCreatedInProjectOnly(ProjID As String, Optional ExcludeChartID As String = Nothing) As DataTable
            Dim query As String = "SELECT * FROM service_chart_info CI INNER JOIN service_chart_srno AS INST ON CI.REC_ID = INST.CSN_CHART_ID WHERE CI_PROJECT_ID = '" & ProjID & "' AND LEN(COALESCE(CSN_EVENT_ID,''))=0 AND LEN(COALESCE(CSN_SERVICE_REPORT_ID,''))=0 AND CI_PURPOSE = 'BASIC DETAILS'"
            If String.IsNullOrWhiteSpace(ExcludeChartID) = False Then
                query = query & " AND CI.Rec_ID <> " & ExcludeChartID
            End If
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_CHART_INFO, query, Tables.SERVICE_CHART_INFO.ToString, inbasicparam)
        End Function
        Public Function CheckIfUserAlreadyRegistered(ByVal ChartInstanceID As String, ByVal UserID As String) As DataTable
            Dim query As String = "Select distinct CR_RESPONSE_ID from service_chart_responses where CR_CHART_SR_ID= " & ChartInstanceID & " AND CR_SERV_USER_ID= " & UserID
            'Dim query As String = "Select distinct rec_id from service_chart_responses_master_info where CRMI_CHART_SR_ID= " & ChartInstanceID & " AND CRMI_SERV_USER_ID= " & UserID
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_CHART_RESPONSES, query, Tables.SERVICE_CHART_RESPONSES.ToString, inbasicparam)
        End Function
        Public Function GetFormReponseCount(ByVal FormId As String) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim Query As String = "SELECT Count(*) FROM service_chart_responses WHERE CR_CHART_ID ='" + FormId + "'"
            'Dim Query As String = "SELECT Count(*) FROM service_chart_responses_master_info WHERE CRMI_CHART_ID ='" + FormId + "'"
            Return _RealService.GetScalar(Tables.SERVICE_CHART_RESPONSES, Query, Tables.SERVICE_CHART_RESPONSES.ToString, inbasicparam)
        End Function
        Public Function GetQuestionReponseCount(ByVal QuestionID As String) As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim Query As String = "SELECT Count(*) FROM service_chart_responses WHERE CR_QUESTION_ID ='" + QuestionID + "'"
            Return _RealService.GetScalar(Tables.SERVICE_CHART_RESPONSES, Query, Tables.SERVICE_CHART_RESPONSES.ToString, inbasicparam)
        End Function
        'Public Function GetQuestionReponseCount(ByVal QuestionID As Int32, ByVal ChartID As Int32) As Object
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim SPName As String = "sp_Get_ChartQuestion_Response_Count"
        '    Dim params() As String = {"@QuestionID", "@CHART_ID"}
        '    Dim values() As Object = {QuestionID, ChartID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
        '    Dim lengths() As Integer = {4, 4}
        '    Dim resp As DataTable = _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, SPName, Tables.SERVICE_CHART_RESPONSES_MASTER_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        '    If Not resp Is Nothing Then
        '        If resp.Rows.Count > 0 Then
        '            If Not resp.Rows(0)(0) Is Nothing Then
        '                Return resp.Rows(0)(0)
        '            End If
        '        End If
        '    End If
        '    Return 0
        'End Function
        Public Function GetPreviousChartInstances(ByVal EventID As String, ByVal ServiceReportID As String, ByVal ChartID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim Query As String = "Select CI_CHARTNAME,scsrno.REC_ID From service_chart_info sci inner Join service_chart_srno scsrno on sci.rec_id=scsrno.CSN_CHART_ID" &
                                  "" & " Where Rec_status In (0,1,2) And csn_srno=1"
            If String.IsNullOrWhiteSpace(ServiceReportID) = False Then
                Query = Query & " And CSN_SERVICE_REPORT_ID ='" & ServiceReportID & "'"
            End If
            If String.IsNullOrWhiteSpace(EventID) = False Then
                Query = Query & " And CSN_EVENT_ID ='" & EventID & "'"
            End If
            If String.IsNullOrWhiteSpace(ChartID) = False Then
                Query = Query & " And sci.rec_id not in(" & ChartID & ")"
            End If
            Return _RealService.List(Tables.SERVICE_CHART_INFO, Query, Tables.SERVICE_CHART_INFO.ToString, inbasicparam)
        End Function
        Public Function GetAllChartInstance(ByVal chartid As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "Select * from service_chart_srno where CSN_CHART_ID='" & chartid & "' order by CSN_SRNO desc"
            Return _RealService.List(Tables.SERVICE_CHART_SRNO, query, Tables.SERVICE_CHART_SRNO.ToString, inbasicparam)
        End Function
        Public Function GetQuestionGroups(ByVal chartid As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "Select distinct CQ_GroupName from SERVICE_CHART_QUESTIONS where CQ_CHART_ID='" & chartid & "'"
            Return _RealService.List(Tables.SERVICE_CHART_QUESTIONS, query, Tables.SERVICE_CHART_QUESTIONS.ToString, inbasicparam)
        End Function
        Public Function GetBuildingsList(ByVal cenid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT SP_SERVICE_PLACE_NAME as BuildingName, REC_ID as ID, SP_SERVICE_PLACE_TYPE FROM SERVICE_PLACE_INFO
                                    WHERE SP_SERVICE_PLACE_TYPE = 'ACCOMMODATION' 
	                                    and SP_CEN_ID = " & cenid
            Return _RealService.List(Tables.SERVICE_PLACE_INFO, query, Tables.SERVICE_PLACE_INFO.ToString, inbasicparam)
        End Function
        Public Function GetPreDefinedOptionList(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            Return GetMisc("CHART PREDEFINED OPTIONS", ClientScreen.Options_createForm, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        End Function
        Public Function GetUserProfileVisibleFields(ByVal FormId As String) As DataTable
            Dim query As String = "Select * from SERVICE_CHART_PROFILE_SETTINGS where SCPS_CI_ID= " & FormId & " AND SCPS_FIELD_VISIBLE= 1"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_CHART_PROFILE_SETTINGS, query, Tables.SERVICE_CHART_PROFILE_SETTINGS.ToString, inbasicparam)
        End Function
        Public Function Get_ServiceReports_And_Events() As DataTable
            Dim query As String = "Select SR_PROG_NAME as 'Name',SRI.REC_ID as 'ID',SR_PROJ_ID as 'Proj_ID','Service Report' as 'Type', SR_PROG_FR_DATE as FromDate, SR_PROG_TO_DATE as ToDate, SR_PROG_VENUE as Venue,SR_CEN_ID,CEN_NAME " &
                                    " from service_report_info SRI inner join Centre_info ci on SRI.SR_CEN_ID=ci.cen_id  where SR_CEN_ID IN (SELECT CEN_ID FROM CENTRE_INFO WHERE CEN_BK_PAD_NO = (SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID =  " & cBase._open_Cen_ID & ")) AND SRI.REC_STATUS in (0,1,2) ORDER BY FromDate desc"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString, inbasicparam)
        End Function
        Public Function Get_ServiceReports_And_Events_ThreeMonths() As DataTable

            Dim query As String = "Select SR_PROG_NAME as 'Name',SRI.REC_ID as 'ID',SR_PROJ_ID as 'Proj_ID','Service Report' as 'Type', SR_PROG_FR_DATE as FromDate, " &
                                    " SR_PROG_TO_DATE as ToDate, SR_PROG_VENUE as Venue,SR_CEN_ID,CEN_NAME " &
                                    " FROM service_report_info SRI inner join Centre_info ci on SRI.SR_CEN_ID=ci.cen_id  " &
                                    " WHERE  " &
                                    " SR_CEN_ID IN (SELECT CEN_ID FROM CENTRE_INFO WHERE CEN_BK_PAD_NO = (SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID =  " & cBase._open_Cen_ID & ")) " &
                                    " AND SRI.REC_STATUS in (0,1,2) " &
                                    " AND SR_PROG_FR_DATE >= DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(GETDATE()), CAST(GETDATE() AS DATE))) " &
                                    " AND SR_PROG_FR_DATE <= DATEADD(DAY, 1, EOMONTH(DATEADD(MONTH, 1, GETDATE()))) " &
                                    " ORDER BY FromDate desc "

            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString, inbasicparam)
        End Function

        Public Function GetQuestionTags(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            Return GetMisc("CHART QUESTION TAG", ClientScreen.Options_createForm, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        End Function
#Region "Insert/Update/Delete Chart"
        Public Function InsertFormMaster(ByVal Inparam As Param_Form_Master, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Int32
            Dim ChartID As Int32 = 0
            Try
                Dim SPName As String = "[sp_Insert_Chart]"
                Dim params() As String = {"@CEN_ID", "@PROJECT_ID", "@CHARTNAME", "@FREQUENCY", "@USER_ID", "@PURPOSE", "@LOGIN_REQUIRED", "@APPROVAL_REQUIREMENT",
                                          "@CONFIRMATION_MESSAGE", "@GENERATE_REG_NO", "@REG_NO_FORMAT", "@DESCRIPTION", "@EVENT_ID", "@SERVICE_REPORT_ID",
                                          "@HEADER_IMAGE_FILE_NAME", "@HEADER_IMAGE_WIDTH", "@HEADER_IMAGE_HEIGHT", "@LOGIN_IMAGE_FILE_NAME", "@LOGIN_RESPONSIVE_IMAGE_FILE_NAME",
                                          "@THUMB_IMAGE_FILE_NAME", "@ALLOW_RESUBMISSION", "@ApprovalMessage", "@RejectionMessage", "@GENERATE_REG_NO_Approval",
                                          "@CHARTTITLE", "@PRE_REQUIRED_CHART_SR_ID", "@STARTMESSAGE", "@ENDMESSAGE", "@STARTDATE", "@ENDDATE", "@DISPLAYMODE",
                                          "@CHART_BG_COLOR", "@SECTION_BG_COLOR", "@SECTION_FG_COLOR", "@SECTION_FONT_SIZE", "@Active_From", "@Active_To", "@SCHEDULE_ID", "@NOTIFICATION_INST_CREATION",
                                          "@MAXENTRIES", "@MAX_GROUP_REGISTRATIONS", "@CHART_BG_IMAGE", "@GRPTITLE_BG_COLOR", "@GRPTITLE_FG_COLOR", "@GRPTITLE_FONT_SIZE"}
                Dim values() As Object = {inBasicParam.openCenID, Inparam.Project_ID, Inparam.Name, Inparam.Frequency, inBasicParam.openUserID, Inparam.Purpose, Inparam.LoginRequired, Inparam.Approval_Required,
                                          Inparam.Confirmation_Message, Inparam.Generate_Reg_No, Inparam.Reg_No_Format, Inparam.Description, Inparam.EventID, Inparam.ServiceReportID,
                                          Inparam.File_Name, Inparam.ImageWidth, Inparam.ImageHeight, Inparam.Login_File_Name, Inparam.Responsive_File_Name,
                                          Inparam.Thumbnail_File_Name, Inparam.AllowResubmission, Inparam.ApprovalMsg, Inparam.RejectionMsg, Inparam.Generate_Reg_No_Approval,
                                          Inparam.Title, Inparam.PreRequiredSrnNo, Inparam.StartDateMsg, Inparam.EndDateMsg, Inparam.StartDate, Inparam.EndDate, Inparam.DisplayMode,
                                          Inparam.FormBgColor, Inparam.QuestionBgColor, Inparam.QuestionFgColor, Inparam.QuestionFontsize, Inparam.ActiveFrom, Inparam.ActiveTo, Inparam.CustomScheduleID, Inparam.NotificationOnInstanceCreation,
                                          Inparam.MaxEntries, Inparam.MaxGroupRegistrations, Inparam.FormBgImagePath, Inparam.GrpTitleBgColor, Inparam.GrpTitleFgColor, Inparam.GrpTitleFontsize}
                Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Boolean,
                                                       DbType.String, DbType.Boolean, DbType.String, DbType.String, DbType.String, DbType.String,
                                                       DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.String,
                                                       DbType.String, DbType.Boolean, DbType.String, DbType.String, DbType.Boolean,
                                                       DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.DateTime, DbType.DateTime, DbType.String,
                                                       DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Boolean,
                                                       DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String}
                Dim lengths() As Integer = {4, 36, 255, 255, 255, 25, 50, 2,
                                           -1, 2, 50, -1, 36, 36,
                                           8000, 4, 4, 8000, 8000,
                                           8000, 2, -1, -1, 2,
                                           255, 4, -1, -1, 12, 12, 15,
                                           50, 50, 50, 50, 12, 12, 4, 2,
                                           4, 4, 8000, 50, 50, 50}
                ChartID = _RealService.ScalarFromSP(Tables.SERVICE_CHART_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))
                If Inparam.File IsNot Nothing Then
                    If Inparam.File.Length > 0 Then
                        UploadFile(Inparam.File, Inparam.File_Name, ChartID, True)
                    End If
                End If
                If Inparam.Login_File IsNot Nothing Then
                    If Inparam.Login_File.Length > 0 Then
                        UploadFile(Inparam.Login_File, Inparam.Login_File_Name, ChartID, True)
                    End If
                End If
                If Inparam.Responsive_File IsNot Nothing Then
                    If Inparam.Responsive_File.Length > 0 Then
                        UploadFile(Inparam.Responsive_File, Inparam.Responsive_File_Name, ChartID, True)
                    End If
                End If
                If Inparam.Thumbnail_File IsNot Nothing Then
                    If Inparam.Thumbnail_File.Length > 0 Then
                        UploadFile(Inparam.Thumbnail_File, Inparam.Thumbnail_File_Name, ChartID, True)
                    End If
                End If
                Return ChartID
            Catch Ex As Exception
                Return 0
            End Try
        End Function
        Public Function InsertFormQuestions(ByVal Inparam As Param_Form_Questions, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean

            Dim SPName As String = "[sp_Insert_Chart_Question]"
            Dim params() As String = {"@CHARTQUESTION", "@CHART_ID", "@userid", "@MODE", "@REQUIRED", "@TYPE", "@MIN", "@MAX", "@SRNO", "@image_FILE_NAME", "@IMAGE_WIDTH", "@IMAGE_HEIGHT", "@FORMULA", "@RowNo", "@ColSpan", "@GroupName", "@Description", "@DefaultValue", "@DefaultVisibility", "@Tag"}
            Dim values() As Object = {Inparam.Question, Inparam.Form_ID, inBasicParam.openUserID, Inparam.Mode, Inparam.Required, Inparam.Type, Inparam.Min, Inparam.Max, Inparam.SrNo, Inparam.File_Name, Inparam.ImageWidth, Inparam.ImageHeight, Inparam.Formula, Inparam.RowNo, Inparam.ColumnSpan, Inparam.GroupName, Inparam.Description, Inparam.DefaultValue, Inparam.DefaultVisibility, Inparam.Tag}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.Int64, Data.DbType.Int64, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {-1, 4, 255, 255, 2, 255, 4, 4, 4, 8000, 4, 4, 255, 4, 4, -1, -1, -1, 2, 36}

            Dim QuestionID As Int32 = _RealService.ScalarFromSP(Tables.SERVICE_CHART_QUESTIONS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))

            If Inparam.File IsNot Nothing Then
                If Inparam.File.Length > 0 Then
                    UploadFile(Inparam.File, Inparam.File_Name, QuestionID, True)
                End If
            End If
            If Not Inparam.Options Is Nothing Then
                For Each Param As Param_Form_Question_Options In Inparam.Options
                    If Not Param Is Nothing Then
                        Param.Question_ID = QuestionID
                        Param.Form_ID = Inparam.Form_ID
                        If Not InsertFormOptions(Param, _RealService, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                Next
            End If
            Return True
        End Function
        Public Function InsertFormOptions(ByVal Inparam As Param_Form_Question_Options, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Insert_Chart_Question_Option"
            Dim params() As String = {"@QUESTION_ID", "@CHART_ID", "@userid", "@SRNO", "@image_FILE_NAME", "@OPTION", "@DependentQuestion", "@DependentQuestionVisibility", "@Points"}
            Dim values() As Object = {Inparam.Question_ID, Inparam.Form_ID, inBasicParam.openUserID, Inparam.OptionSrNo, Inparam.File_Name, Inparam.Options, Inparam.Dependent_Questions, Inparam.Dependent_Questions_Visibility, Inparam.Points}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, DbType.Boolean, DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 255, 4, 8000, -1, 255, 2, 5}

            Dim OptionID As Int32 = _RealService.ScalarFromSP(Tables.SERVICE_CHART_QUESTION_OPTIONS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))


            If Inparam.File IsNot Nothing Then
                If Inparam.File.Length > 0 Then
                    UploadFile(Inparam.File, Inparam.File_Name, OptionID, True)
                End If
            End If
            Return True
        End Function
        Public Function InsertFormProfileSettings(ByVal Inparam As Param_Form_ProfileSettings, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Insert_Chart_Profile_Settings"
            Dim params() As String = {"@USER_ID", "@CHART_ID", "@FIELD", "@VISIBLE", "@ENABLE", "@MANDATORY", "@TYPE"}
            Dim values() As Object = {inBasicParam.openUserID, Inparam.Form_ID, Inparam.Field, Inparam.Visible, Inparam.Enable, Inparam.Mandatory, "MAIN"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 50, 2, 2, 2, 15}
            _RealService.InsertBySPPublic(Tables.SERVICE_CHART_PROFILE_SETTINGS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            Return True
        End Function
        Public Function InsertFormGroupProfileSettings(ByVal Inparam As Param_Form_ProfileSettings, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Insert_Chart_Profile_Settings"
            Dim params() As String = {"@USER_ID", "@CHART_ID", "@FIELD", "@VISIBLE", "@ENABLE", "@MANDATORY", "@TYPE"}
            Dim values() As Object = {inBasicParam.openUserID, Inparam.Form_ID, Inparam.Field, Inparam.Visible, Inparam.Enable, Inparam.Mandatory, "GROUP"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 50, 2, 2, 2, 15}
            _RealService.InsertBySPPublic(Tables.SERVICE_CHART_GROUP_PROFILE_SETTINGS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            Return True
        End Function
        Public Function InsertFormNotifications(ByVal param As Param_Form_Notification, ChartID As Int32, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param) As Int32
            Dim SPName As String = "[sp_Insert_Chart_Notification]"
            Dim params() As String =
            {
                "@APPROVAL_REQUIREMENT", "@IS_NOTIFICATION_MODE_EMAIL_SELECTED", "@IS_NOTIFICATION_MODE_WHATSAPP_SELECTED",
                "@IS_NOTIFICATION_MODE_SMS_SELECTED", "@IS_EMAIL_CONFIRM_SELECTED", "@IS_EMAIL_APPROVE_SELECTED",
                "@IS_EMAIL_REJECT_SELECTED", "@IS_WHATSAPP_CONFIRM_SELECTED", "@IS_WHATSAPP_APPROVE_SELECTED",
                "@IS_WHATSAPP_REJECT_SELECTED", "@IS_SMS_CONFIRM_SELECTED", "@IS_SMS_APPROVE_SELECTED",
                "@IS_SMS_REJECT_SELECTED", "@EMAIL_CONFIRMATION_MSG", "@EMAIL_APPROVAL_MSG",
                "@EMAIL_REJECTION_MSG", "@WHATSAPP_CONFIRMATION_MSG", "@WHATSAPP_APPROVAL_MSG",
                "@WHATSAPP_REJECTION_MSG", "@SMS_CONFIRMATION_MSG", "@SMS_APPROVAL_MSG", "@SMS_REJECTION_MSG",
                "@TEMPLATE_NAME", "@SCNS_CHART_ID", "@CATEGORY", "@CC", "@BCC",
                "@REPLY_TO_EMAIL", "@SENDER_EMAIL_TYPE", "@EMAIL", "@PASSWORD", "@SENDER_WHATSAPP_NO_TYPE", "@WHATSAPPNO",
                "@DELIVERY_SPEED", "@ADMIN_WHATSAPP_NO", "@ADMIN_EMAIL", "@NOTIFICATION_MODE", "@LOGGED_IN_USER",
                "@META_CONFIRMATION_TEMPLATE_NAME", "@META_APPROVAL_TEMPLATE_NAME", "@META_REJECTION_TEMPLATE_NAME"
            } '@ATTACHMENT_ID is not added
            Dim values() As Object =
            {
                param.Approval_Required, param.IsEmailNotificationSelected, param.IsWhatsappNotificationSelected,
                param.IsSMSNotificationSelected, param.IsEmailConfirmSelected, param.IsEmailApproveSelected,
                param.IsEmailRejectSelected, param.IsWhatsAppConfirmSelected, param.IsWhatsAppApproveSelected,
                param.IsWhatsAppRejectSelected, param.IsSMSConfirmSelected, param.IsSMSApproveSelected,
                param.IsSMSRejectSelected, param.ConfirmationEmail_Message, param.ApprovalEmail_Message,
                param.RejectionEmail_Message, param.ConfirmationWhatsApp_Message, param.ApprovalWhatsApp_Message,
                param.RejectionWhatsApp_Message, param.ConfirmationSMS_Message, param.ApprovalSMS_Message, param.RejectionSMS_Message,
                param.Name, ChartID, param.Category, param.CC, param.BCC,
                param.ReplyToEmail, param.SenderEmailType, param.Email, param.Password, param.SenderWhatsappNoType, param.Whatsappno,
                param.DeliverySpeed, param.AdminWhatsAppNo, param.AdminEmail, param.Mode, cBase._open_User_ID,
                param.ConfirmationWhatsApp_Message_Template, param.ApprovalWhatsApp_Message_Template, param.RejectionWhatsApp_Message_Template
            }
            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean,
                Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean,
                Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean,
                Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean,
                Data.DbType.Boolean, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String
            }
            Dim lengths() As Integer =
            {
                 2, 2, 2,
                 2, 2, 2,
                 2, 2, 2,
                 2, 2, 2,
                 2, -1, -1,
                 -1, -1, -1,
                -1, -1, -1, -1,
                255, 4, 15, 2000, 2000,
                2000, 10, 2000, 50, 10, 15,
                15, 15, 2000, 10, 20,
                255, 255, 255
            }
            Return _RealService.InsertBySPPublic(Tables.NOTIFICATION_TEMPLATE_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function InsertForm_Txn(inParam As Param_Insert_Form) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim ChartID As Int32 = 0
            If Not inParam.Master Is Nothing Then
                ChartID = InsertFormMaster(inParam.Master, _RealService, inbasicparam)
                If ChartID = 0 Then
                    Throw New Exception(Common_Lib.Messages.SomeError)
                End If
            End If
            For Each Param As Param_Form_ProfileSettings In inParam.ProfileSettings
                If Not Param Is Nothing Then
                    Param.Form_ID = ChartID
                    If Not InsertFormProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                End If
            Next
            For Each Param As Param_Form_ProfileSettings In inParam.GroupProfileSettings
                If Not Param Is Nothing Then
                    Param.Form_ID = ChartID
                    If Not InsertFormGroupProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                End If
            Next
            For Each Param As Param_Form_Questions In inParam.Questions   'inserting both question and options
                If Not Param Is Nothing Then
                    Param.Form_ID = ChartID
                    If Not InsertFormQuestions(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                End If
            Next
            If Not inParam.NotificationSettings Is Nothing Then
                InsertFormNotifications(inParam.NotificationSettings, ChartID, _RealService, inbasicparam)
            End If
            Return ChartID
        End Function

        Public Function UpdateFormMaster(ByVal Inparam As Param_Form_Master, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Try
                Dim SPName As String = "[sp_Update_Chart]"
                Dim params() As String = {"@CEN_ID", "@PROJECT_ID", "@CHARTNAME", "@FREQUENCY", "@USER_ID", "@PURPOSE", "@LOGIN_REQUIRED",
                                          "@APPROVAL_REQUIREMENT", "@CONFIRMATION_MESSAGE", "@GENERATE_REG_NO", "@REG_NO_FORMAT", "@DESCRIPTION",
                                          "@EVENT_ID", "@SERVICE_REPORT_ID", "@HEADER_IMAGE_FILE_NAME", "@HEADER_IMAGE_WIDTH", "@HEADER_IMAGE_HEIGHT",
                                          "@LOGIN_IMAGE_FILE_NAME", "@LOGIN_RESPONSIVE_IMAGE_FILE_NAME", "@THUMB_IMAGE_FILE_NAME", "@ALLOW_RESUBMISSION",
                                          "@ApprovalMessage", "@RejectionMessage", "@GENERATE_REG_NO_Approval", "@CHARTTITLE", "@PRE_REQUIRED_CHART_SR_ID",
                                          "@Chart_REC_ID", "@STARTMESSAGE", "@ENDMESSAGE", "@STARTDATE", "@ENDDATE", "@DISPLAYMODE", "@CHART_BG_COLOR",
                                          "@SECTION_BG_COLOR", "@SECTION_FG_COLOR", "@SECTION_FONT_SIZE", "@Active_From", "@Active_To", "@SCHEDULE_ID",
                                          "@NOTIFICATION_INST_CREATION", "@MAXENTRIES", "@START_DATE_CHANGE", "@MAX_GROUP_REGISTRATIONS",
                                           "@CHART_BG_IMAGE", "@GRPTITLE_BG_COLOR", "@GRPTITLE_FG_COLOR", "@GRPTITLE_FONT_SIZE"}
                Dim values() As Object = {inBasicParam.openCenID, Inparam.Project_ID, Inparam.Name, Inparam.Frequency, inBasicParam.openUserID, Inparam.Purpose, Inparam.LoginRequired,
                                          Inparam.Approval_Required, Inparam.Confirmation_Message, Inparam.Generate_Reg_No, Inparam.Reg_No_Format, Inparam.Description,
                                          Inparam.EventID, Inparam.ServiceReportID, Inparam.File_Name, Inparam.ImageWidth, Inparam.ImageHeight,
                                          Inparam.Login_File_Name, Inparam.Responsive_File_Name, Inparam.Thumbnail_File_Name, Inparam.AllowResubmission,
                                          Inparam.ApprovalMsg, Inparam.RejectionMsg, Inparam.Generate_Reg_No_Approval, Inparam.Title, Inparam.PreRequiredSrnNo,
                                          Inparam.Rec_ID, Inparam.StartDateMsg, Inparam.EndDateMsg, Inparam.StartDate, Inparam.EndDate, Inparam.DisplayMode, Inparam.FormBgColor,
                                          Inparam.QuestionBgColor, Inparam.QuestionFgColor, Inparam.QuestionFontsize, Inparam.ActiveFrom, Inparam.ActiveTo, Inparam.CustomScheduleID,
                                          Inparam.NotificationOnInstanceCreation, Inparam.MaxEntries, Inparam.StartDateChange, Inparam.MaxGroupRegistrations,
                                          Inparam.FormBgImagePath, Inparam.GrpTitleBgColor, Inparam.GrpTitleFgColor, Inparam.GrpTitleFontsize}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                                                       Data.DbType.Boolean, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String,
                                                       Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32,
                                                       Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean,
                                                       Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, DbType.String, Data.DbType.Int32,
                                                       Data.DbType.Int32, DbType.String, DbType.String, DbType.DateTime, DbType.DateTime, DbType.String, DbType.String,
                                                       DbType.String, DbType.String, DbType.String, DbType.Time, DbType.Time, DbType.String,
                                                       DbType.Boolean, DbType.Int32, Data.DbType.Boolean, DbType.Int32,
                                                       DbType.String, DbType.String, DbType.String, DbType.String}
                Dim lengths() As Integer = {4, 36, 255, 255, 255, 25, 50,
                                            2, -1, 2, 50, -1,
                                            36, 36, 8000, 4, 4,
                                            8000, 8000, 8000, 2,
                                            -1, -1, 2, -1, 4,
                                             4, -1, -1, 12, 12, 15, 50,
                                            50, 50, 50, 12, 12, 4,
                                             2, 4, 2, 4,
                                             8000, 50, 50, 50}
                _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))
                If Inparam.File IsNot Nothing Then
                    If Inparam.File.Length > 0 Then
                        UploadFile(Inparam.File, Inparam.File_Name, Inparam.Rec_ID, True)
                    End If
                End If
                If Inparam.Login_File IsNot Nothing Then
                    If Inparam.Login_File.Length > 0 Then
                        UploadFile(Inparam.Login_File, Inparam.Login_File_Name, Inparam.Rec_ID, True)
                    End If
                End If
                If Inparam.Responsive_File IsNot Nothing Then
                    If Inparam.Responsive_File.Length > 0 Then
                        UploadFile(Inparam.Responsive_File, Inparam.Responsive_File_Name, Inparam.Rec_ID, True)
                    End If
                End If
                If Inparam.Thumbnail_File IsNot Nothing Then
                    If Inparam.Thumbnail_File.Length > 0 Then
                        UploadFile(Inparam.Thumbnail_File, Inparam.Thumbnail_File_Name, Inparam.Rec_ID, True)
                    End If
                End If
                Return True
            Catch Ex As Exception
                Return 0
            End Try
        End Function
        Public Function UpdateFormProfileSettings(ByVal Inparam As Param_Form_ProfileSettings, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Update_Chart_Profile_Settings"
            Dim params() As String = {"@USER_ID", "@CHART_ID", "@FIELD", "@VISIBLE", "@ENABLE", "@MANDATORY", "@REC_ID", "@TYPE"}
            Dim values() As Object = {inBasicParam.openUserID, Inparam.Form_ID, Inparam.Field, Inparam.Visible, Inparam.Enable, Inparam.Mandatory, Inparam.Rec_ID, "MAIN"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 50, 2, 2, 2, 4, 15}
            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_PROFILE_SETTINGS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            Return True
        End Function
        Public Function UpdateFormGroupProfileSettings(ByVal Inparam As Param_Form_ProfileSettings, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Update_Chart_Profile_Settings"
            Dim params() As String = {"@USER_ID", "@CHART_ID", "@FIELD", "@VISIBLE", "@ENABLE", "@MANDATORY", "@REC_ID", "@TYPE"}
            Dim values() As Object = {inBasicParam.openUserID, Inparam.Form_ID, Inparam.Field, Inparam.Visible, Inparam.Enable, Inparam.Mandatory, Inparam.Rec_ID, "GROUP"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 50, 2, 2, 2, 4, 15}
            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_GROUP_PROFILE_SETTINGS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            Return True
        End Function
        Public Function UpdateFormQuestions(ByVal Inparam As Param_Form_Questions, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean

            Dim SPName As String = "[sp_Update_Chart_Question]"
            Dim params() As String = {"@CHARTQUESTION", "@CHART_ID", "@userid", "@MODE", "@REQUIRED", "@TYPE", "@MIN", "@MAX", "@SRNO", "@image_FILE_NAME", "@IMAGE_WIDTH", "@IMAGE_HEIGHT", "@FORMULA", "@QuestionRecID", "@RowNo", "@ColSpan", "@GroupName", "@Description", "@DefaultValue", "@DefaultVisibility", "@Tag"}
            Dim values() As Object = {Inparam.Question, Inparam.Form_ID, inBasicParam.openUserID, Inparam.Mode, Inparam.Required, Inparam.Type, Inparam.Min, Inparam.Max, Inparam.SrNo, Inparam.File_Name, Inparam.ImageWidth, Inparam.ImageHeight, Inparam.Formula, Inparam.Rec_ID, Inparam.RowNo, Inparam.ColumnSpan, Inparam.GroupName, Inparam.Description, Inparam.DefaultValue, Inparam.DefaultVisibility, Inparam.Tag}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.Int64, Data.DbType.Int64, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, DbType.Boolean, DbType.String}
            Dim lengths() As Integer = {-1, 4, 255, 255, 2, 255, 4, 4, 4, 8000, 4, 4, 255, 4, 4, 4, -1, -1, -1, 2, 36}

            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_QUESTIONS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))

            If Inparam.File IsNot Nothing Then
                If Inparam.File.Length > 0 Then
                    UploadFile(Inparam.File, Inparam.File_Name, Inparam.Rec_ID, True)
                End If
            End If
            If Not Inparam.Options Is Nothing Then
                For Each Param As Param_Form_Question_Options In Inparam.Options
                    If Not Param Is Nothing Then
                        Param.Question_ID = Inparam.Rec_ID
                        Param.Form_ID = Inparam.Form_ID
                        If String.IsNullOrWhiteSpace(Param.Rec_ID) Then 'option rec id is not there so insert new option
                            If Not InsertFormOptions(Param, _RealService, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                        Else
                            If Not UpdateFormOptions(Param, _RealService, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                        End If
                    End If
                Next
            End If
            Return True
        End Function
        Public Sub DeleteFormQuestions(ByVal QuestionRecID As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_QUESTION_OPTIONS, "CQO_QUESTION_ID =" & QuestionRecID, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_QUESTIONS, "REC_ID=" & QuestionRecID, inbasicparam)
        End Sub
        Public Function UpdateFormOptions(ByVal Inparam As Param_Form_Question_Options, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "sp_Update_Chart_Question_Option"
            Dim params() As String = {"@QUESTION_ID", "@CHART_ID", "@userid", "@SRNO", "@image_FILE_NAME", "@OPTION", "@Option_REC_ID", "@DependentQuestion", "@DependentQuestionVisibility", "@Points"}
            Dim values() As Object = {Inparam.Question_ID, Inparam.Form_ID, inBasicParam.openUserID, Inparam.OptionSrNo, Inparam.File_Name, Inparam.Options, Inparam.Rec_ID, Inparam.Dependent_Questions, Inparam.Dependent_Questions_Visibility, Inparam.Points}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, DbType.Boolean, DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 255, 4, 8000, -1, 4, 255, -1, 5}

            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_QUESTION_OPTIONS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))


            If Inparam.File IsNot Nothing Then
                If Inparam.File.Length > 0 Then
                    UploadFile(Inparam.File, Inparam.File_Name, Inparam.Rec_ID, True)
                End If
            End If
            Return True
        End Function
        Public Sub DeleteFormOptions(ByVal OptionRecID As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_QUESTION_OPTIONS, "REC_ID=" & OptionRecID, inbasicparam)
        End Sub
        Public Function UpdateForm_Txn(UpParam As Param_Update_Form) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            If Not UpParam.Master Is Nothing Then
                If Not UpdateFormMaster(UpParam.Master, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Param_Form_ProfileSettings In UpParam.ProfileSettings
                If Not Param Is Nothing Then
                    Param.Form_ID = UpParam.Master.Rec_ID
                    If String.IsNullOrWhiteSpace(Param.Rec_ID) Then
                        If Not InsertFormProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    Else
                        If Not UpdateFormProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                End If
            Next
            For Each Param As Param_Form_ProfileSettings In UpParam.GroupProfileSettings
                If Not Param Is Nothing Then
                    Param.Form_ID = UpParam.Master.Rec_ID
                    If String.IsNullOrWhiteSpace(Param.Rec_ID) Then
                        If Not InsertFormGroupProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    Else
                        If Not UpdateFormGroupProfileSettings(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                End If
            Next
            For Each Param As Param_Form_Questions In UpParam.Questions   'inserting both question and options
                If Not Param Is Nothing Then
                    Param.Form_ID = UpParam.Master.Rec_ID
                    If String.IsNullOrWhiteSpace(Param.Rec_ID) Then 'question rec id is not there so insert new question
                        If Not InsertFormQuestions(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    Else
                        If Not UpdateFormQuestions(Param, _RealService, inbasicparam) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                End If
            Next
            If UpParam.DeleteQuestions_RecID IsNot Nothing Then
                If UpParam.DeleteQuestions_RecID.Length > 0 Then
                    For Each recid As String In UpParam.DeleteQuestions_RecID
                        DeleteFormQuestions(recid)
                    Next
                End If
            End If
            If UpParam.DeleteOptions_RecID IsNot Nothing Then
                If UpParam.DeleteOptions_RecID.Length > 0 Then
                    For Each recid As String In UpParam.DeleteOptions_RecID
                        DeleteFormOptions(recid)
                    Next
                End If
            End If
            DeleteFormNotifications(UpParam.Master.Rec_ID)
            If Not UpParam.NotificationSettings Is Nothing Then
                InsertFormNotifications(UpParam.NotificationSettings, UpParam.Master.Rec_ID, _RealService, inbasicparam)
            End If
            Return True
        End Function
        ' This function is not used. Refer the function delete chart. deleteChart
        Public Function DeleteForm_Txn(ByVal FormId As String, Optional FormInstanceId As Int32 = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_QUESTION_OPTIONS, "CQO_CHART_ID=" & FormId, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_QUESTIONS, "CQ_CHART_ID=" & FormId, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_PROFILE_SETTINGS, "SCPS_CI_ID=" & FormId, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_GROUP_PROFILE_SETTINGS, "SCGPS_CI_ID=" & FormId, inbasicparam)
            DeleteFormNotifications(FormId, FormInstanceId)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_SRNO, "CSN_CHART_ID=" & FormId, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_IMAGE_AND_STYLE_INFO, "CI_CHART_ID=" & FormId, inbasicparam)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_INFO, "REC_ID=" & FormId, inbasicparam)
            'Dim Schedule As Object = New Common_Lib.DbOperations.Schedule(cBase)
            'Schedule.DeleteScheduleInstanceAndMapping(FormId, "CHART")
            Return True
        End Function
#End Region
#Region "Form Response"
        Public Function GetFormRecord(ByVal FormInstanceId As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_INSTANCE_ID"}
            Dim values() As Object = {FormInstanceId}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_INFO, "[sp_get_Chart_Record]", Tables.SERVICE_CHART_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))
        End Function
        Public Function GetFormQuestions(ByVal FormInstanceId As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
            Return _RealService.List(RealTimeService.Tables.SERVICE_CHART_QUESTIONS, "SELECT QUES.* FROM service_chart_questions AS QUES INNER JOIN service_chart_srno AS SR ON QUES.CQ_CHART_ID = SR.CSN_CHART_ID WHERE SR.REC_ID = " + FormInstanceId, RealTimeService.Tables.SERVICE_CHART_QUESTIONS.ToString(), inbasicparam)
        End Function
        Public Function GetFormQuestion_Options(Optional ByVal QuestionID As String = "", Optional ByVal FormID As String = "", Optional ByVal PredefinedOptionMiscID As String = "") As DataTable
            If (String.IsNullOrWhiteSpace(PredefinedOptionMiscID) = True) Then
                Dim condition As String = ""
                If QuestionID IsNot Nothing And QuestionID.Length > 0 Then
                    condition = "WHERE CQO_QUESTION_ID='" & QuestionID & "' "
                End If
                If FormID IsNot Nothing And FormID.Length > 0 Then
                    If condition IsNot Nothing And condition.Length > 0 Then
                        condition = condition & " AND CQO_CHART_ID='" & FormID & "' "
                    Else
                        condition = "WHERE CQO_CHART_ID='" & FormID & "' "
                    End If
                End If
                Return GetRecordByCustom(condition, ClientScreen.Options_FormResponse, RealTimeService.Tables.SERVICE_CHART_QUESTION_OPTIONS)
            Else
                Return GetFormQuestion_PreDefinedOptions(PredefinedOptionMiscID)
            End If
        End Function
        Public Function GetFormQuestion_PreDefinedOptions(ByVal MISC_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim SPName As String = "[sp_get_chartPreDefinedOptions]"
            Dim params() As String = {"@MISCID"}
            Dim values() As Object = {MISC_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(RealTimeService.Tables.MISC_INFO, SPName, RealTimeService.Tables.MISC_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function
        Public Function Get_chartProfileSettings(ByVal CHART_ID As Int32, Optional Type As String = "MAIN") As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_ID", "@TYPE"}
            Dim values() As Object = {CHART_ID, Type}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {8, 5}
            Return _RealService.ListDatasetFromSP(Tables.SO_USER_LISTING_SCREEN_PREFERENCE, "[sp_get_chartProfileSettings]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function InsertFormResponse(ByVal inParam As Param_Form_Response, _RealService As RealTimeService.ConnectOneWS, inBasicParam As RealTimeService.Basic_Param, ByVal FormInstanceID As String, ByVal ResponseID As String, ByVal ServiceUserID As Int32, Optional ByVal Rec_Add_By As String = Nothing, Optional AddTime As DateTime = Nothing) As Boolean
            Dim SPName As String = "[sp_Insert_Chart_Response]"
            Dim params() As String = {"@CHARTRESPONSE", "@CHART_INSTANCE_ID", "@QUESTION_ID", "@FILE_NAME", "@SERV_USER_ID", "@RESPONSE_ID"}
            Dim values() As Object = {inParam.Response, FormInstanceID, inParam.Question_ID, inParam.File_Name, ServiceUserID, ResponseID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {-1, 255, 255, 8000, 255, 36}

            Dim ResponsePKID As Int32 = _RealService.ScalarFromSP(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))

            If inParam.File IsNot Nothing Then
                If inParam.File.Length > 0 Then
                    UploadFile(inParam.File, inParam.File_Name, ResponsePKID, True)
                End If
            End If
            Return True
        End Function
        Public Function InsertFormResponse_Txn(ByVal inParam As Param_Insert_Form_Response) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
            For Each Param As Param_Form_Response In inParam.InFormResponse
                If Not Param Is Nothing Then
                    If Not InsertFormResponse(Param, _RealService, inbasicparam, inParam.FormInstanceID, inParam.Response_ID, inParam.ServiceUserID, inParam.AddBy) Then Throw New Exception(Common_Lib.Messages.SomeError)
                End If
            Next
            Return True
        End Function
        Public Function InsertGuideSession(ByVal GuideSessionID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim query As String = "INSERT INTO Temp_Service_Chart_Guide_Link VALUES('" & GuideSessionID & "')"
            _RealService.InsertPublic(Tables.TEMP_SERVICE_CHART_GUIDE_LINK, query, inbasicparam, GuideSessionID)
            Return True
        End Function
        Public Function GetFormSubmissionConfirmation(ByVal FormInstanceId As Int32, ByVal ResponseId As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)

            Dim SPName As String = "[sp_get_Chart_Response_confirmation]"
            Dim params() As String = {"@RESPONSE_ID", "@CHART_INSTANCE_ID"}
            Dim values() As Object = {ResponseId, FormInstanceId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_INFO, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)

        End Function
        Public Function GetFormRegistrationSlip(ByVal FormInstanceId As Int32, ByVal ResponseId As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
            Dim SPName As String = "[sp_get_Registration_Slip]"
            Dim params() As String = {"@RESPONSE_ID", "@CHART_INSTANCE_ID"}
            Dim values() As Object = {ResponseId, FormInstanceId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_INFO, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function

#End Region
#Region "Chart Info - Management/Centre Side"
        Public Function HighlightResponse(ByVal chartResponseID As String, ByVal Highlight As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_chartResponse_Highlight"
            Dim params() As String = {"@RESPONSE_IDS", "@HIGHLIGHT"}
            Dim values() As Object = {chartResponseID, Highlight}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {-1, 50}
            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))

            Return True
        End Function
        Public Function MarkResponseCategory(ByVal chartResponseID As String, ByVal Category As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_chartResponder_Category"
            Dim params() As String = {"@RESPONSE_IDS", "@CATEGORY"}
            Dim values() As Object = {chartResponseID, Category}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {-1, 15}
            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))

            Return True
        End Function
        Public Function GetAccomodationList(ByVal cenid As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?, Optional ByVal ChartResponseID As String = Nothing, Optional ByVal RoomID As String = Nothing) As DataSet
            'Dim query As String = "select CASE WHEN (SP.SP_SERVICE_PLACE_NAME = AL.AL_LOC_NAME) THEN '' ELSE AL_LOC_NAME + ' - ' END + SP.SP_SERVICE_PLACE_NAME + '(' + AL_OTHER_DETAIL + ')' AS NAME, AL.REC_ID 
            'from service_place_info as SP 
            'INNER JOIN asset_location_info AS AL ON SP.REC_ID = AL.SP_REC_ID
            'INNER JOIN CENTRE_INFO AS CI ON AL_CEN_ID = CEN_ID 
            'where CEN_BK_PAD_NO = (SELECT CEN_BK_PAD_NO FROM centre_info WHERE CEN_ID = " & cenid & ")
            'AND UPPER(SP.SP_SERVICE_PLACE_TYPE) = 'ACCOMMODATION'
            'AND (SP.SP_SERVICE_PLACE_NAME <> AL.AL_LOC_NAME OR SP.SP_CEN_ID = AL.AL_CEN_ID)
            'ORDER BY SP.SP_SERVICE_PLACE_NAME"
            'Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            'Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartResponsesInfo)
            'Return _RealService.List(Tables.SERVICE_PLACE_INFO, query, Tables.SERVICE_PLACE_INFO.ToString, inbasicparam)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Accommodation_List"
            Dim params() As String = {"@CEN_ID", "@FROM_DATE", "@TO_DATE", "@RESP_ID", "@ROOM_ID"}
            Dim values() As Object = {cenid, fromdate, todate, ChartResponseID, RoomID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 20, 20, 8000, 36}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))

        End Function
        Public Function GetResponseMiscDetails(ByVal chartResponseID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "select * from SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS WHERE CRAD_RESPONSE_ID ='" + chartResponseID + "' ", Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS.ToString, inbasicparam)
        End Function
        Public Sub DeleteAccommodationByRespID(ByVal ChartResponseID As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "CRAD_RESPONSE_ID ='" & ChartResponseID & "'", inbasicparam)
            Dim RemoveArrivalStatus As String = ("UPDATE SERVICE_CHART_RESPONSES_MASTER_INFO SET CRMI_ARRIVED = 0 WHERE REC_ID = '" + ChartResponseID + "'")
            'This is to update the arrival status to unarrived .
            _RealService.List(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, RemoveArrivalStatus, Tables.SERVICE_CHART_RESPONSES_MASTER_INFO.ToString, GetBaseParams(ClientScreen.Facility_Accommodation_Register)) ' this query is actually update call 
        End Sub
        Public Function updateChartResponseAccomodation(ByVal chartResponseID As String, ByVal AccomodationMiscRecID As String, ByVal bedcount As Integer?, ByVal remarks As String,
                                                        ByVal fromdate As DateTime?, ByVal todate As DateTime?) As Boolean
            Dim ResponseID As String() = chartResponseID.Split(",")
            AccomodationMiscRecID = If(String.IsNullOrWhiteSpace(AccomodationMiscRecID), Nothing, AccomodationMiscRecID)
            For Each ID As String In ResponseID
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "sp_update_chartResponse_Accomodation"
                Dim params() As String = {"@RESPONSE_ID", "@AccomodationMiscRecID", "@UserID", "@BedCount", "@Remarks", "@fromdate", "@todate", "@cenid"}
                Dim values() As Object = {ID, AccomodationMiscRecID, cBase._open_User_ID, bedcount, remarks, fromdate, todate, cBase._open_Cen_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Int32}
                Dim lengths() As Integer = {36, 36, 255, 4, 8000, 6}
                _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
            Next
            Return True
        End Function
        Public Function updateChartResponseArrivalStatus(ByVal chartResponseID As String, ByVal _Action As String) As Boolean
            Dim ResponseID As String() = chartResponseID.Split(",")
            For Each ID As String In ResponseID
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "sp_update_chartResponse_ArrivalStatus"
                Dim params() As String = {"@RESPONSE_ID", "@Action", "@UserID"}
                Dim values() As Object = {ID, _Action, cBase._open_User_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String}
                Dim lengths() As Integer = {36, 15, 255}
                _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
            Next
            Return True
        End Function
        Public Function updateChartResponseStatus(ByVal chartInstanceID As String, ByVal chartResponseID As String, ByVal recStatus As Integer, Optional responseRejectReason As String = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_chartResponseStatus"
            Dim params() As String = {"@REC_STATUS", "@RESPONSE_ID", "@INSANCE_ID", "@UserID", "@REJECT_REASON"}
            Dim values() As Object = {recStatus, chartResponseID, chartInstanceID, cBase._open_User_ID, responseRejectReason}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 4, 255, 8000}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function updateChartResponseRemarks(ByVal chartResponseID As String, ByVal remarks As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_chartResponseStatus"
            Dim params() As String = {"@RESPONSE_ID", "@REMARKS", "@UPDATE_FIELD"}
            Dim values() As Object = {chartResponseID, remarks, "REMARKS"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 8000, 255}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function deleteChartResponse(ByVal chartResponseID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartResponsesInfo)
            '_RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES_MISC_DETAIL, "CRAD_RESPONSE_ID='" & chartResponseID & "'", inbasicparam)
            '_RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "CRAD_RESPONSE_ID IN ('" & chartResponseID & "')", inbasicparam)
            '_RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES, "CR_RESPONSE_ID IN ('" & chartResponseID & "')", inbasicparam)
            '_RealService.DeleteByCondition(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, "REC_ID IN ('" & chartResponseID & "')", inbasicparam)
            'Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_delete_service_chart_responses"
            Dim params() As String = {"@RESPONSE_ID"}
            Dim values() As Object = {chartResponseID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {8000}
            _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
            Return True
        End Function
        Public Function get_chartInfo(Optional project_ID As String = Nothing, Optional AllInstance As Boolean = True, Optional FormType As String = "Open") As DataTable
            'Dim originPath As String = System.Configuration.ConfigurationManager.AppSettings("OriginPath").ToString()
            'Dim AttachmentPath As String = System.Configuration.ConfigurationManager.AppSettings("thumbnailpath").ToString()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@PROJECT_ID", "@ServicePath", "@AttachmentPath", "@UserID", "@AllInstance", "@FormType"}
            Dim values() As Object = {cBase._open_Cen_ID, project_ID, ServicesPath, AttachmentPath, cBase._open_User_ID, AllInstance, FormType}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Boolean, DbType.String}
            Dim lengths() As Integer = {4, 36, 100, 100, 255, 4, 36}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_INFO, "[sp_get_serviceCharts]", Tables.SERVICE_CHART_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function
        Public Function get_chartResponses(ByVal chartInstanceID As Int32, Optional ByVal regCenId As Int32? = Nothing, Optional ByVal questionFilter As String = "") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim _base As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartResponsesInfo)
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RegCenID", "@QuestionFilter", "@USER_ID"}
            Dim values() As Object = {chartInstanceID, regCenId, questionFilter, _base.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 4, -1, 255}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartResponses]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_chartResponses_GroupDetails(ByVal ResponseID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@RESPONSEID"}
            Dim values() As Object = {ResponseID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[SP_GET_CHARTRESPONSES_GROUPDETAILS]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_chartResponsesummary(ByVal cenid As Int32, ByVal chartInstanceID As Int32?, ByVal summary_type As String, ByVal fromdate As DateTime?, ByVal todate As DateTime?,
                                                 Optional ByVal buildingid As String = Nothing, Optional ByVal eventid As String = Nothing) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@SUMMARY_TYPE", "@FROM_DATE", "@TO_DATE", "@BUILDING_ID", "@EVENT_ID", "@CENID"}
            Dim values() As Object = {chartInstanceID, summary_type, fromdate, todate, buildingid, eventid, cenid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.DateTime2, DbType.DateTime2, DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {4, 30, 20, 20, 36, 36, 4}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartResponseSummary]", paramters, values, dbTypes, lengths,
                                                  GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_AccommodationRegister(ByVal cenid As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?, ByVal eventid As String, ByVal buildingid As String,
                                                 Optional ByVal reg_no As String = Nothing, Optional ByVal filter_by As String = "Event Start And End Dates", Optional ChartInstID As Int32? = Nothing, Optional ConciseMode As Boolean = True) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@fromdate", "@todate", "@eventid", "@buildingid", "@reg_Number", "@cenid", "@filterBy", "@chart_INST_id", "@compactmode"}
            Dim values() As Object = {fromdate, todate, eventid, buildingid, reg_no, cenid, filter_by, ChartInstID, ConciseMode}
            Dim dbTypes() As System.Data.DbType = {DbType.DateTime2, DbType.DateTime2, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.Int32, DbType.Boolean}
            Dim lengths() As Integer = {20, 20, 36, 36, 800, 8, 200, 4, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "[sp_get_Accommodation_Register]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_AccommodationSlipDetails(ByVal cenid As Int32, ResponseID As String, Optional ConciseMode As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@cenid", "@compactmode", "@responseID"}
            Dim values() As Object = {cenid, ConciseMode, ResponseID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Boolean, DbType.String}
            Dim lengths() As Integer = {4, 4, -1}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS, "[sp_get_Accommodation_Slip_Details]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_LiveFormsList(ByVal cenid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@cenid"}
            Dim values() As Object = {cenid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {8}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_SRNO, "[sp_get_LiveChartInstances]", Tables.SERVICE_CHART_SRNO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_AllFormsList(ByVal cenid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@cenid"}
            Dim values() As Object = {cenid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {8}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_SRNO, "[sp_get_AllChartInstances]", Tables.SERVICE_CHART_SRNO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_chartCenterwiseConfirmationList(ByVal chartInstanceID As Int32, ByVal cenid As Int32) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@CEN_ID"}
            Dim values() As Object = {chartInstanceID, cenid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 10}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartCntrWiseEvntConfirmationList]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function

        Public Function CheckIfChartNameIsUniqueInUID(ByVal ChartName As String, Optional ByVal ChartID As String = Nothing, Optional ByVal CenID_ShiftForm As Int32? = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            CenID_ShiftForm = If(CenID_ShiftForm = 0 Or CenID_ShiftForm Is Nothing, cBase._open_Cen_ID, CenID_ShiftForm)
            Dim query As String = "Select Count(*) from service_chart_info where CI_CHARTNAME='" & ChartName & "' And CI_CEN_ID='" & CenID_ShiftForm & "'"
            If String.IsNullOrWhiteSpace(ChartID) = False Then
                query = query & " And REC_ID <>" & ChartID
            End If
            Dim count As Integer = _RealService.GetScalar(Tables.SERVICE_CHART_INFO, query, Tables.SERVICE_CHART_INFO.ToString, inbasicparam)
            If (count > 0) Then
                Return False
            Else
                Return True
            End If
        End Function
        Public Function createChartCopy(Inparam As Param_Create_Copy) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_chartCopy"
            Dim params() As String = {"@CHART_ID", "@CHART_NAME", "@CHART_TITLE", "@CHART_DESCRIPTION", "@USERNAME", "@CENID", "@PROJECT_ID", "@SERVICE_REPORT_ID", "@EVENT_ID", "@START_DATE", "@END_DATE"}
            Dim values() As Object = {Inparam.ChartID, Inparam.chart_name, Inparam.Title, Inparam.Description, cBase._open_User_ID, Inparam.cenid, Inparam.ProjectID, Inparam.serviceReportID, Inparam.eventID, Inparam.startDate, Inparam.endDate}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.DateTime, DbType.DateTime}
            Dim lengths() As Integer = {4, 255, 8000, 8000, 255, 4, 36, 36, 36, 20, 20}
            _RealService.InsertBySPPublic(Tables.SERVICE_CHART_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
            Return True
        End Function
        Public Function IsChartHavingResponses(ByVal ChartID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim query As String = "SELECT COUNT(*) FROM SERVICE_CHART_RESPONSES WHERE CR_CHART_ID =" & ChartID
            Dim count As Integer = _RealService.GetScalar(Tables.SERVICE_CHART_RESPONSES, query, Tables.SERVICE_CHART_RESPONSES.ToString, inbasicparam)
            'Dim query As String = "SELECT COUNT(*) FROM SERVICE_CHART_RESPONSES_MASTER_INFO WHERE CRMI_CHART_ID =" & ChartID
            'Dim count As Integer = _RealService.GetScalar(Tables.SERVICE_CHART_RESPONSES_MASTER_INFO, query, Tables.SERVICE_CHART_RESPONSES_MASTER_INFO.ToString, inbasicparam)
            If (count > 0) Then
                Return True
            Else
                Return False
            End If
        End Function
        Public Function deleteChart(ByVal ChartID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_delete_Service_Chart"
            Dim params() As String = {"@CHART_ID"}
            Dim values() As Object = {Convert.ToInt32(ChartID)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            DeleteFormNotifications(ChartID)
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function
        Public Function GetChartVisibilityDetails(ByVal ChartID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Chart_Visibility_Info"
            Dim params() As String = {"@CHART_ID"}
            Dim values() As Object = {ChartID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_VISIBILITY_INFO, SPName, Tables.SERVICE_CHART_VISIBILITY_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function
        Public Function InsertChartVisibilityDetails(ByVal Inparam As Param_Insert_Chart_Visibility_Details) As Boolean
            Try
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "sp_Insert_Chart_Visibility_Details"
                Dim params() As String = {"@CHART_ID", "@INSTT_ID", "@CEN_ID", "@FY_FROM", "@FY_TO", "@ACC_TYPE", "@USER_TYPE", "@USER_ID", "@MENU", "@Ins_All", "@Cen_All", "@AccType_All"}
                Dim values() As Object = {Inparam.Chart_ID, Inparam.Instt_ID, Inparam.Cen_ID, Inparam.FY_From, Inparam.FY_To, Inparam.Acc_Type, Inparam.User_Type, cBase._open_User_ID, Inparam.Menu, Inparam.Ins_All, Inparam.Cen_All, Inparam.AccType_All}
                Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Boolean, DbType.Boolean, DbType.Boolean}
                Dim lengths() As Integer = {4, 5, 4, 4, 4, 36, 36, 255, 36, 2, 2, 2}
                _RealService.InsertBySPPublic(Tables.SERVICE_CHART_VISIBILITY_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
                Return True
            Catch Ex As Exception
                CommonExceptionCall(Ex)
                Return False
            End Try

        End Function
        Public Function EditChartVisibilityDetails(ByVal Inparam As Param_Update_Chart_Visibility_Details) As Boolean
            Try
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "sp_Update_Chart_Visibility_Details"
                Dim params() As String = {"@CHART_ID", "@INSTT_ID", "@CEN_ID", "@FY_FROM", "@FY_TO", "@ACC_TYPE", "@USER_TYPE", "@USER_ID", "@MENU", "@Rec_ID"}
                Dim values() As Object = {Inparam.Chart_ID, Inparam.Instt_ID, Inparam.Cen_ID, Inparam.FY_From, Inparam.FY_To, Inparam.Acc_Type, Inparam.User_Type, cBase._open_User_ID, Inparam.Menu, Inparam.Rec_ID}
                Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32}
                Dim lengths() As Integer = {4, 5, 4, 4, 4, 36, 36, 255, 36, 4}
                _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_VISIBILITY_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
                Return True
            Catch Ex As Exception
                CommonExceptionCall(Ex)
                Return False
            End Try
        End Function
        Public Function DeleteChartVisibilityDetails(ByVal Rec_id As Int32) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            _RealService.DeleteByCondition(Tables.SERVICE_CHART_VISIBILITY_INFO, "REC_ID =" & Rec_id, inbasicparam)
            Return True
        End Function
        Public Function GetChartVisibilityMenu(ChartID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT * fROM service_chart_visibility_info WHERE len(coalesce(SCVI_MENU_IN_WHICH_VISIBLE,''))>0 AND SCVI_CHART_ID=" & ChartID
            Return _RealService.List(Tables.SERVICE_CHART_VISIBILITY_INFO, query, Tables.SERVICE_CHART_VISIBILITY_INFO.ToString, inbasicparam)
        End Function
        Public Function GetServicePlacesByCenid(cenid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT SP_SERVICE_PLACE_NAME, REC_ID FROM SERVICE_PLACE_INFO
                                        WHERE SP_SERVICE_PLACE_TYPE = 'ACCOMMODATION' and SP_CEN_ID = " & cenid &
                                        " ORDER BY SP_SERVICE_PLACE_NAME ASC"
            Return _RealService.List(Tables.SERVICE_PLACE_INFO, query, Tables.SERVICE_PLACE_INFO.ToString, inbasicparam)
        End Function

        Public Function Get_From_ToDateOfEventsByServiceReportId(instanceid As Int64) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT SRI.SR_PROG_FR_DATE, SRI.SR_PROG_TO_DATE FROM SERVICE_CHART_SRNO SCSRNO
                                INNER JOIN SERVICE_REPORT_INFO SRI ON SRI.REC_ID = SCSRNO.CSN_SERVICE_REPORT_ID
                                WHERE SCSRNO.REC_ID = '" & instanceid & "'"
            Return _RealService.List(Tables.SERVICE_CHART_SRNO, query, Tables.SERVICE_CHART_SRNO.ToString, inbasicparam)
        End Function
#End Region
        Public Function GetInstitutes() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.INSTITUTE_INFO, "select INS_ID as ID,INS_SHORT as NAME from INSTITUTE_INFO where REC_STATUS IN (0,1,2)", Tables.INSTITUTE_INFO.ToString, inbasicparam)
        End Function
        Public Function GetAccType() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.CENTRE_ACC_TYPE_INFO, "select ACC_TYPE as NAME,REC_ID as ID From centre_acc_type_info where REC_STATUS<>-1", Tables.CENTRE_ACC_TYPE_INFO.ToString, inbasicparam)
        End Function
        Public Function GetFinancialYears() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.COD_INFO, "select distinct cod_year_id,replace(cod_year_name,' ','') as cod_year_name,cast(cod_year_sdt as date) as cod_year_sdt,cast(cod_year_edt as date) as cod_year_edt From cod_info where REC_STATUS<>-1", Tables.COD_INFO.ToString, inbasicparam)
        End Function
        Public Function GetCentres(ByVal insID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "select cen_id,cen_uid,cen_name,ii.ins_name,ii.ins_short from centre_info ci inner join institute_info ii on ci.cen_ins_id=ii.ins_id where ci.rec_status in(0,1,2)"
            If (String.IsNullOrWhiteSpace(insID) = False) Then
                query = query & " and ci.cen_ins_id=" & insID
            End If
            query = query & "  order by cen_name"
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString, inbasicparam)
        End Function
        Public Function get_ColumnNamesOfTable(ByVal table_name As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_ColumnNamesOfAnyTable"
            Dim paramters As String() = {"@Table_Name"}
            Dim values() As Object = {table_name}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {150}
            Return _RealService.ListDatasetFromSP(Tables.ADDRESS_BOOK, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function GetidFromFk(ByVal table_name As String, ByVal col_value As String, ByVal col_name As String, ByVal ref_table_name As String, ByVal ref_col_name As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim query As String = ""
            Dim tbl As Common_Lib.RealTimeService.Tables
            Select Case table_name.ToUpper()
                Case "ADDRESS_BOOK"
                    Select Case ref_table_name
                        Case "map_city_info"
                            query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where CI_NAME = '" & col_value & "'"
                            tbl = Tables.MAP_CITY_INFO
                        Case "map_country_info"
                            query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where CO_NAME = '" & col_value & "'"
                            tbl = Tables.MAP_COUNTRY_INFO
                        Case "map_state_info"
                            query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where ST_NAME = '" & col_value & "'"
                            tbl = Tables.MAP_STATE_INFO
                        Case "map_district_info"
                            query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where DI_NAME = '" & col_value & "'"
                            tbl = Tables.MAP_DISTRICT_INFO
                        Case "misc_info"
                            If col_name = "C_OCCUPATION_ID" Then
                                query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where MISC_ID = 'OCCUPATION' and MISC_NAME = '" & col_value & "'"
                            ElseIf col_name = "C_CONTACT_MODE_ID" Then
                                query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                            " where MISC_ID = 'CONTACT MODE' and MISC_NAME = '" & col_value & "'"
                            End If
                            tbl = Tables.MISC_INFO
                    End Select
                Case "ACTION_ITEM_INFO"
                    query = "Select count(" & ref_col_name & ") As cnt, max(" & ref_col_name & ") As ID from " & ref_table_name &
                        " where HE_NAME = '" & col_value & "'"
                    tbl = Tables.SO_HO_EVENT_INFO
            End Select
            Return _RealService.List(tbl, query, tbl.ToString, inbasicparam)
        End Function

        Public Function get_usersForMappingtoChart(ByVal Chart_ID As Int32?) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@CHART_ID"}
            Dim values() As Object = {cBase._open_Cen_ID, Chart_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 10}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_INFO, "[sp_get_usersforMappingtoChart]", Tables.SERVICE_USERS_INFO.ToString(), paramters, values, dbTypes, lengths,
                                           GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function Delete_usersMappedToChart(ByVal Chart_ID As Int32) As Boolean
            Dim qry As String
            qry = "SUCM_CHART_ID=" & Chart_ID
            Return DeleteByCondition(qry, Tables.SERVICE_USERS_CHART_MAPPING, ClientScreen.Facility_ServiceProject)
        End Function
        Public Function Delete_usersMappedToChartByRecid(ByVal Rec_ID As Int32) As Boolean
            Dim qry As String
            qry = "REC_ID=" & Rec_ID
            Return DeleteByCondition(qry, Tables.SERVICE_USERS_CHART_MAPPING, ClientScreen.Facility_ServiceProject)
        End Function
        Public Function Insert_usersMappingToChart(ByVal User_ID As Int32, ByVal User_ab_id As String, ByVal Chart_ID As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_subjectMappingToChart"
            Dim params() As String = {"@SERVICE_USER_ID", "@CHART_ID", "@FROM", "@TO", "@REC_ADD_BY", "@SERVICE_USER_AB_ID"}
            Dim values() As Object = {User_ID, Chart_ID, fromdate, todate, cBase._open_Cen_ID, User_ab_id}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {8, 8, 20, 20, 6, 36}
            'used public insert function as there are no transactional data involved 
            Return _RealService.InsertBySPPublic(Tables.SERVICE_USERS_CHART_MAPPING, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function Get_ChartMappedUsersRec_IDWithChartResponsesForChart(ChartID) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Get_ChartMappedUsersRec_ID_WithChartResponses_ForChart]"
            Dim params() As String = {"@ChartID"}
            Dim values() As Object = {ChartID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {10}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, SPName, Tables.SERVICE_CHART_RESPONSES.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function

        Public Function GetMappedUsersofChart(ByVal ChartID As Int32?) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_MappedUserstoChart]"
            Dim params() As String = {"@Chart_ID"}
            Dim values() As Object = {ChartID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {10}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_CHART_MAPPING, SPName, Tables.SERVICE_USERS_CHART_MAPPING.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function

        Public Function UpdateMappedUserOfChart(ByVal recid As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_MappedUsersofChart"
            Dim params() As String = {"@REC_ID", "@fromdate", "@todate"}
            Dim values() As Object = {recid, fromdate, todate}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime, Data.DbType.DateTime}
            Dim lengths() As Integer = {8, 25, 25}
            Return _RealService.InsertBySPPublic(Tables.SERVICE_USERS_CHART_MAPPING, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function

        Public Function get_BulkAllotments_List_of_Instance(ByVal instanceid As Int64) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@INSTANCEID"}
            Dim values() As Object = {instanceid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int64}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO, "[SP_GET_BULKALLOTMENT_OF_INSTANCE]", Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function Delete_BulkAllottmentByRecID(ByVal Rec_id As Int64) As Boolean
            Dim qry As String
            qry = "RECID_BLK=" & Rec_id
            Return DeleteByCondition(qry, Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO, ClientScreen.Facility_ChartInfo)
        End Function
        Public Function getAvailableRoomsCount_Bulk(ByVal instanceid As Int64, ByVal buildingid As String, ByVal bedcount As Int32, ByVal ac_nonac As String,
                                                      ByVal category As String, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "SP_GET_BULK_AVAILABLEROOMSCOUNT"
            Dim params() As String = {"@CENID", "@INSTANCEID", "@BUILDINGID", "@ROOMCAPACITY", "@AC_NONAC", "@CATEGORY", "@FROM_DATE", "@TO_DATE"}
            Dim values() As Object = {cBase._open_Cen_ID, instanceid, buildingid, bedcount, ac_nonac, category, fromdate, todate}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int64, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.DateTime, DbType.DateTime}
            Dim lengths() As Integer = {6, 10, 36, 4, 10, 30, 25, 25}
            Return _RealService.ListFromSP(Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO, SPName, Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function InsertBulkAllotmentToInstance(ByVal instanceid As Int64, ByVal buildingid As String, ByVal RoomCapacity As Int32, ByVal ac_nonac As String,
                                                      ByVal category As String, ByVal BedCount As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As Boolean
            Try
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "SP_INSERT_BULKALLOTMENT_TO_INSTANCE"
                Dim params() As String = {"@INSTANCEID", "@BUILDINGID", "@ROOMCAPACITY", "@AC_NONAC", "@CATEGORY", "@USER_ID", "@BEDCOUNT", "@fromdate", "@todate"}
                Dim values() As Object = {instanceid, buildingid, RoomCapacity, ac_nonac, category, cBase._open_User_ID, BedCount, fromdate, todate}
                Dim dbTypes() As System.Data.DbType = {DbType.Int64, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime}
                Dim lengths() As Integer = {10, 36, 4, 10, 30, 100, 4, 25, 25}
                _RealService.InsertBySPPublic(Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
                Return True
            Catch Ex As Exception
                CommonExceptionCall(Ex)
                Return False
            End Try
        End Function

        Public Function Update_BulkAllotment(ByVal instanceid As Int64, ByVal buildingid As String, ByVal RoomCapacity As Int32, ByVal ac_nonac As String,
                                                      ByVal category As String, ByVal BedCount As Int32, ByVal recid_blk As Int64, ByVal fromdate As DateTime?, ByVal todate As DateTime?) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "SP_UPDATE_BULKALLOTMENT_TO_INSTANCE"
            Dim params() As String = {"@INSTANCEID", "@BUILDINGID", "@ROOMCAPACITY", "@AC_NONAC", "@CATEGORY", "@USER_ID", "@BEDCOUNT", "@recid_blk", "@fromdate", "@todate"}
            Dim values() As Object = {instanceid, buildingid, RoomCapacity, ac_nonac, category, cBase._open_User_ID, BedCount, recid_blk, fromdate, todate}
            Dim dbTypes() As System.Data.DbType = {DbType.Int64, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Int64, DbType.DateTime, DbType.DateTime}
            Dim lengths() As Integer = {18, 36, 4, 10, 30, 100, 4, 18, 25, 25}
            Return _RealService.InsertBySPPublic(Tables.BULK_ALLOTTMENT_ACCOMMODATION_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function GetEventDatesOfInstance(ByVal insID As Int64) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT SCRNO.*, SRI.SR_PROG_FR_DATE, SRI.SR_PROG_TO_DATE  
			                         FROM 
				                        SERVICE_CHART_SRNO  SCRNO
			                        INNER JOIN SERVICE_REPORT_INFO SRI ON SRI.REC_ID = SCRNO.CSN_SERVICE_REPORT_ID
                                    WHERE SCRNO.REC_ID = " & insID

            Return _RealService.List(Tables.SERVICE_CHART_SRNO, query, Tables.SERVICE_CHART_SRNO.ToString, inbasicparam)
        End Function

        Public Function get_BulkAllotmentListForRecommendationOfInstance(ByVal instanceid As Int64, ByVal responseids As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@INSTANCEID", "@RESPIDSLIST"}
            Dim values() As Object = {instanceid, responseids}
            Dim dbTypes() As System.Data.DbType = {DbType.Int64, DbType.String}
            Dim lengths() As Integer = {36, 8000}
            Return _RealService.ListFromSP(Tables.SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION, "[SP_GET_BULKALLOTMENTFORRECOMMENDATION_OF_INSTANCE]", Tables.SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function Insert_RecommendationToResponce_Bulk(ByVal responseid As String, ByVal buildingid As String, ByVal RoomCapacity As Int32, ByVal ac_nonac As String,
                                                      ByVal category As String, ByVal BedCount As Int32, ByVal remarks As String, Optional ByVal chartid As Int32 = 0,
                                                             Optional ByVal instanceid As Int32 = 0, Optional ByVal roomNumber As String = Nothing) As Boolean
            Try
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim SPName As String = "SP_INSERT_RECOMMENDATION_TO_RESPONSE"
                Dim params() As String = {"@RESPONSEID", "@BUILDINGID", "@ROOMCAPACITY", "@AC_NONAC", "@CATEGORY", "@USER_ID", "@BEDCOUNT", "@REMARKS", "@CHARTID", "@INSTANCEID", "@ROOMNUMBER"}
                Dim values() As Object = {responseid, buildingid, RoomCapacity, ac_nonac, category, cBase._open_User_ID, BedCount, remarks, chartid, instanceid, roomNumber}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.String}
                Dim lengths() As Integer = {36, 36, 4, 10, 30, 100, 4, 8000, 20, 20, 60}
                _RealService.InsertBySPPublic(Tables.SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
                Return True
            Catch Ex As Exception
                CommonExceptionCall(Ex)
                Return False
            End Try
        End Function

        Public Function GetRoomNumbersByBuildingID(ByVal BuildingID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String
            If BuildingID Is Nothing Or BuildingID = "" Then
                query = "SELECT * FROM ASSET_LOCATION_INFO ALI
                            INNER JOIN SERVICE_PLACE_INFO SPI ON SPI.REC_ID = ALI.SP_REC_ID
                            WHERE SPI.SP_SERVICE_PLACE_TYPE = 'ACCOMMODATION' AND SPI.SP_CEN_ID =" & cBase._open_Cen_ID
            Else
                query = "SELECT AL_LOC_NAME, REC_ID FROM ASSET_LOCATION_INFO  WHERE SP_REC_ID = '" & BuildingID & "'"
            End If
            Return _RealService.List(Tables.ASSET_LOCATION_INFO, query, Tables.ASSET_LOCATION_INFO.ToString, inbasicparam)
        End Function
        Public Function GetBulkAllottedBuildingsListByInstanceID(ByVal InstanceID As Int64) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT distinct SPI.SP_SERVICE_PLACE_NAME, SPI.REC_ID FROM BULK_ALLOTTMENT_ACCOMMODATION_INFO blk
                                    INNER JOIN SERVICE_PLACE_INFO SPI ON SPI.REC_ID = blk.BUILDINGID_BLK
                                    WHERE INSTANCEID_BLK =  " & InstanceID
            Return _RealService.List(Tables.SERVICE_PLACE_INFO, query, Tables.SERVICE_PLACE_INFO.ToString, inbasicparam)
        End Function
        Public Function GetRoomsList_AccomShort(ByVal cenid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "SELECT AL_LOC_NAME, ALI.REC_ID, ALI.SP_REC_ID, SPI.SP_SERVICE_PLACE_NAME,AL_MAXCAPACITY, 0 as CURRENT_ALLOTMENT
                                    FROM ASSET_LOCATION_INFO ALI
                                    INNER JOIN SERVICE_PLACE_INFO SPI ON SPI.REC_ID = ALI.SP_REC_ID
                                    WHERE SPI.SP_SERVICE_PLACE_TYPE = 'ACCOMMODATION' AND AL_MAXCAPACITY >0 AND SPI.SP_CEN_ID = " & cenid
            Return _RealService.List(Tables.ASSET_LOCATION_INFO, query, Tables.ASSET_LOCATION_INFO.ToString, inbasicparam)
        End Function

        Public Function get_recommendationSummaryForSelectedRespids(ByVal responseids As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@RESPIDSLIST"}
            Dim values() As Object = {responseids}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {8000}
            Return _RealService.ListFromSP(Tables.SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION, "[SP_GET_RECOMMENDATIONSUMMARY_SELECTEDRESPIDS]", Tables.SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function
        Public Function get_personNamesByRegNumbers(ByVal regOrPh As String, ByVal reg_Ph_Nums As String, ByVal sel_eventid As String, ByVal sel_formid As Int64, ByVal cenid As Int64) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@REG_OR_PHONE", "@REG_OR_PH_NUMS", "@EVENTID", "@FORMID", "@CENID"}
            Dim values() As Object = {regOrPh, reg_Ph_Nums, sel_eventid, sel_formid, cenid}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.Int64, DbType.Int64}
            Dim lengths() As Integer = {20, 8000, 36, 8, 8}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_get_personnames_by_reg_or_ph_numbers]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function

        Public Function GetFormNotifications(ByVal ChartinstanceId As Int32) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_Chart_Notification_Record]"
            Dim params() As String = {"@CHART_INSTANCE_ID"}
            Dim values() As Object = {ChartinstanceId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListDatasetFromSP(Tables.NOTIFICATION_TEMPLATE_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function DeleteFormNotifications(ByVal ChartID As Int32, Optional ChartinstanceId As Int32 = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Delete_Chart_Notification]"
            Dim params() As String = {"@CHART_ID", "@CHART_INSTANCE_ID"}
            Dim values() As Object = {ChartID, ChartinstanceId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return _RealService.UpdateBySPPublic(Tables.NOTIFICATION_TEMPLATE_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function get_chartResponses_Medical(ByVal chartInstanceID As Int32, Optional ByVal regCenId As Int32? = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RegCenID"}
            Dim values() As Object = {chartInstanceID, regCenId}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartResponses_Medical]", Tables.SERVICE_CHART_RESPONSES.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function Insert_QuizResult_Whatsapp_Notifications_Queue(ByVal WhatsappNo As String, BatchName As String, Content As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            'Need to create different sp for Send Sample.
            Dim SPName As String = "[sp_Insert_WhatsApp_Queue]"
            Dim params() As String =
            {'@SENDING_PRIORITY is not is sp add it first before executing
                "@PHONE", "@BATCH_NAME", "@MESSAGE", "@CEN_ID", "@USER_ID", "@MEDIA_FILE_URL", "@SENDER_PHONE", "@SENDING_PRIORITY"
            }
            Dim values() As Object =
            {
                WhatsappNo, BatchName, Content, Nothing, cBase._open_User_ID, Nothing, Nothing, Nothing
            }

            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String
            }
            Dim lengths() As Integer =
            {
                 15, 80, -1, 4, 255, 8000, 10, 10
            }
            _RealService.InsertBySPPublic(Tables.WHATSAPP_MESSAGE_QUEUE, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            'If NotificationSettings.File IsNot Nothing Then
            '    UploadFile(NotificationSettings.File, NotificationSettings.File_Name.Replace(ConfigurationManager.AppSettings("FilePhysicalPath"), "").Trim(), 0, True)
            'End If
        End Function
        Public Function Insert_Form_Response_Notifications(ByVal NotificationSettings As Param_FormResponse_Notification, ChartInstanceID As Int32) As Int32
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_Notification_Batches]"
            Dim cenID As Int32 = IIf(NotificationSettings.CenID = 0, cBase._open_Cen_ID, NotificationSettings.CenID)
            Dim AddBy As String = IIf(String.IsNullOrWhiteSpace(NotificationSettings.AddBy), cBase._open_User_ID, NotificationSettings.AddBy)

            Dim params() As String =
            {
                "@CONTENT", "@BATCH_NAME", "@CHART_INSTANCE_ID", "@RESPONSE_STATUS_OR_CATEGORY",
                "@SUBJECT", "@CC", "@BCC", "@REPLY_TO_EMAIL", "@SENDER_EMAIL_TYPE",
                "@EMAIL", "@PASSWORD", "@SENDER_WHATSAPP_NO_TYPE", "@ATTACHMENT_ID",
                "@WHATSAPPNO", "@DELIVERY_SPEED", "@ADMIN_WHATSAPP_NO", "@ADMIN_EMAIL",
                "@NOTIFICATION_MODE", "@LOGGED_IN_USER", "@CENID"
            }
            Dim values() As Object =
            {
                NotificationSettings.Content, NotificationSettings.BatchName, NotificationSettings.ChartInstanceId, NotificationSettings.ResponseStatus,
                NotificationSettings.Subject, NotificationSettings.CC, NotificationSettings.BCC, NotificationSettings.ReplyToEmail, NotificationSettings.SenderEmailType,
                NotificationSettings.Email, NotificationSettings.Password, NotificationSettings.SenderWhatsappNoType, NotificationSettings.File_Name, NotificationSettings.Whatsappno,
                NotificationSettings.DeliverySpeed, NotificationSettings.AdminWhatsAppNo, NotificationSettings.AdminEmail, NotificationSettings.Mode, AddBy,
                cenID
            }
            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32
            }
            Dim lengths() As Integer =
            {
                 -1, -1, 4, 15,
                 -1, 2000, 2000, 2000, 10,
                 2000, 50, 10, 2000,
                 15, 15, 15, 2000,
                 10, 20, 4
            }
            Dim Id As Integer = _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            If NotificationSettings.File IsNot Nothing Then
                UploadFile(NotificationSettings.File, NotificationSettings.File_Name.Replace(ConfigurationManager.AppSettings("attachmentpath"), "").Trim(), Id, True)
            End If

            If NotificationSettings.SelectedNotificationRadio = "formresponses" Then
                Insert_In_Notification_Queue(NotificationSettings.BatchName, AddBy, cenID, NotificationSettings.selectedFormResponses_group, NotificationSettings.selectedFormResponses_str)

            ElseIf NotificationSettings.SelectedNotificationRadio = "addressbook" Then
                SPName = "[sp_Insert_NotificationBatch_To_Queue_addressbook]"
                params = {"@BATCH_NAME", "@LOGIN_USERID", "@CENID", "@designationIds", "@occupationIds", "@categoryIds", "@magazineIds",
                                "@specialityIds", "@eventIds", "@titleIds", "@wingIds", "@cityIds", "@districtIds", "@selected_group", "@selectedids_str", "@financial_year"}
                values = {NotificationSettings.BatchName, AddBy, cenID, NotificationSettings.SelectedDesignationIds,
                    NotificationSettings.SelectedOccupationIds, NotificationSettings.SelectedCategoryIds, NotificationSettings.SelectedMagazineIds,
                    NotificationSettings.SelectedSpecialitiesIds, NotificationSettings.SelectedEventsIds, NotificationSettings.SelectedTitleIds,
                    NotificationSettings.SelectedWingIds, NotificationSettings.SelectedCityIds, NotificationSettings.SelectedDistrictIds,
                    NotificationSettings.selectedAddresses_group, NotificationSettings.selectedContacts_str, cBase._open_Year_ID
                }
                dbTypes = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                    Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                    Data.DbType.String, Data.DbType.String, Data.DbType.String
                    }
                lengths = {-1, 80, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 50, -1, 4}
                _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))

            ElseIf NotificationSettings.SelectedNotificationRadio = "phonenumbers" Or NotificationSettings.SelectedNotificationRadio = "googlesheet" Then
                SPName = "[sp_Insert_NotificationBatch_To_Queue_phoneNumbers]"
                params = {"@BATCH_NAME", "@LOGIN_USERID", "@CENID", "@enteredPhNos_emails"}
                values = {NotificationSettings.BatchName, AddBy, cenID, NotificationSettings.enteredPhoneNumbersOrEmails_str}
                dbTypes = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
                lengths = {-1, 80, 4, -1}
                _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            End If
            'Return Id
            'This is to get count of the created queue
            Dim Query As String = "SELECT	
	                                    CASE 
                                            WHEN NBI_NOTIFICATION_MODE = 'Whatsapp' THEN 
                                                (SELECT COUNT(*) FROM WHATSAPP_MESSAGE_QUEUE WHERE NAME = NBI_BATCH_NAME)
                                            WHEN NBI_NOTIFICATION_MODE = 'Email' THEN 
                                                (SELECT COUNT(*) FROM EMAIL_SCHEDULER_QUEUE WHERE EMAILER_NAME = NBI_BATCH_NAME)
                                            ELSE 0
                                        END AS QUEUE_COUNT 
                                    FROM 
	                                    NOTIFICATION_BATCHES_INFO
                                    WHERE
	                                    NBI_BATCH_NAME ='" & NotificationSettings.BatchName & "'"
            Return _RealService.GetScalar(Tables.NOTIFICATION_BATCHES_INFO, Query, Tables.NOTIFICATION_BATCHES_INFO.ToString(), GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function Insert_Sample_Form_Response_NotificationSettings(ByVal NotificationSettings As Param_FormResponse_Notification, ChartInstanceID As Int32) As Int32
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_SampleSend_NotificationBatch_To_Queue]"
            Dim cenID As Int32 = IIf(NotificationSettings.CenID = 0, cBase._open_Cen_ID, NotificationSettings.CenID)
            Dim AddBy As String = IIf(String.IsNullOrWhiteSpace(NotificationSettings.AddBy), cBase._open_User_ID, NotificationSettings.AddBy)
            Dim params() As String =
            {
                "@BATCH_NAME", "@LOGIN_USERID", "@CHART_INSTANCE_ID", "@ADMIN_PHONE",
                "@NOTIFICATION_MODE", "@MSG", "@CENTER_ID", "@SCHEDULED_SENT_ON", "@MEDIA_PATH",
                "@SENDER_PHONE", "@SEND_PRIORITY", "@ADMIN_EMAIL",
                "@CC", "@BCC", "@REPLYTO",
                "@SENDER_EMAIL", "@SENDER_PASSWORD", "@SUBJECT", "@RESPONSE_STATUS", "@NOTIFICATION_TO"
            }
            Dim values() As Object =
            {
                NotificationSettings.BatchName, AddBy, ChartInstanceID, NotificationSettings.AdminWhatsAppNo, NotificationSettings.Mode, NotificationSettings.Content,
               cenID, Nothing, NotificationSettings.File_Name, NotificationSettings.Whatsappno, Nothing, NotificationSettings.AdminEmail,
                NotificationSettings.CC, NotificationSettings.BCC, NotificationSettings.ReplyToEmail, NotificationSettings.Email, NotificationSettings.Password,
                NotificationSettings.Subject, NotificationSettings.ResponseStatus, NotificationSettings.SelectedNotificationRadio
            }
            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String
            }
            Dim size As Int32
            If NotificationSettings.Mode.Equals("Email") Then
                size = 255
            ElseIf NotificationSettings.Mode.Equals("Whatsapp") Then
                size = 80
            End If

            Dim lengths() As Integer =
            {
                 -1, 255, 4, 15,
                 15, -1, 4, 12, 500,
                 15, 4, 255,
                 255, 255, 255,
                 2000, 2000, -1, 10, 30
            }

            Dim Id As Integer = _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            If NotificationSettings.File IsNot Nothing Then
                UploadFile(NotificationSettings.File, NotificationSettings.File_Name.Replace(ConfigurationManager.AppSettings("attachmentpath"), "").Trim(), Id, True)
            End If
            Return Id
        End Function
        Public Function GetAdminEmailAndWhatsApp(ByVal Cen_ID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT CASE WHEN COALESCE(NULLIF(CS.CEN_MOB_NO_1,''), NULLIF(CI.CEN_MOB_NO_1,''),NULLIF(MAIN.CEN_MOB_NO_1,'')) IS NOT NULL THEN COALESCE(CS.CEN_MOB_NO_1,CI.CEN_MOB_NO_1,MAIN.CEN_MOB_NO_1)
                                         WHEN COALESCE(NULLIF(CI.CEN_MOB_NO_2,''),NULLIF(MAIN.CEN_MOB_NO_2,'')) IS NOT NULL THEN COALESCE(CI.CEN_MOB_NO_2,MAIN.CEN_MOB_NO_2) ELSE '' END as AdminMobile,
                                         CASE WHEN COALESCE(NULLIF(CS.CEN_EMAIL_ID_1,''), NULLIF(CI.CEN_EMAIL_ID_1,''),NULLIF(MAIN.CEN_EMAIL_ID_1,'')) IS NOT NULL THEN COALESCE(CS.CEN_EMAIL_ID_1,CI.CEN_EMAIL_ID_1,MAIN.CEN_EMAIL_ID_1)
                                         WHEN COALESCE(NULLIF(CI.CEN_EMAIL_ID_2,''),NULLIF(MAIN.CEN_EMAIL_ID_2,'')) IS NOT NULL THEN COALESCE(CI.CEN_EMAIL_ID_2,MAIN.CEN_EMAIL_ID_2) ELSE '' END as AdminEmail                                
                                  FROM CENTRE_INFO CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1 LEFT JOIN CENTRE_SERVICE_CONTACT_DETAILS AS CS ON CI.CEN_ID	=CS.CEN_ID Where CI.CEN_ID=" & Cen_ID
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function InsertMobileLoginOTPFormNotifications(ByVal Mobile As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@MOBILE"}
            Dim values() As Object = {Mobile}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {15}
            Return _RealService.ListFromSP(Tables.MOBILE_LOGIN_OTP_INFO, "[sp_Insert_Mobile_Login_OTP]", Tables.MOBILE_LOGIN_OTP_INFO.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_RefreshCallForOTP(ByVal Mobile As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@MOBILE"}
            Dim values() As Object = {Mobile}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {15}
            Return _RealService.ListFromSP(Tables.MOBILE_LOGIN_OTP_INFO, "[sp_Check_Records_Exists_For_Mobile_OTP]", Tables.MOBILE_LOGIN_OTP_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function Insert_In_Notification_Queue(ByVal BatchName As String, LoginUserId As String, ByVal cenid As Int32, ByVal selected_group As String, ByVal selected_str As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_NotificationBatch_To_Queue]"
            Dim params() As String = {"@BATCH_NAME", "@LOGIN_USERID", "@CENID", "@selected_group", "@selectedids_str"}
            Dim values() As Object = {BatchName, LoginUserId, cenid, selected_group, selected_str}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {-1, 80, 4, 50, -1}
            _RealService.InsertBySPPublic(Tables.EMAIL_SCHEDULER_QUEUE, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function get_Form_Response_Notifications(ByVal chartInstanceID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@CENID"}
            Dim values() As Object = {chartInstanceID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return _RealService.ListFromSP(Tables.NOTIFICATION_BATCHES_INFO, "[sp_get_Notification_Batches_Info]", Tables.NOTIFICATION_BATCHES_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function Check_Unique_BatchName(ByVal BatchName As String, BatchType As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Check_Unique_BatchName]"
            Dim paramters As String() = {"@BATCH_NAME", "@NOTIFICATION_TYPE"}
            Dim values() As Object = {BatchName, BatchType}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
            Dim lengths() As Integer = {-1, 10}
            Return _RealService.ListFromSP(Tables.WHATSAPP_MESSAGE_QUEUE, SPName, Tables.WHATSAPP_MESSAGE_QUEUE.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function Delete_Form_Response_Notification_Setting(ByVal RecId As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[SP_DELETE_BATCH_QUEUE]"
            Dim params() As String = {"@QUEUEID"}
            Dim values() As Object = {RecId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.UpdateBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function
        Public Function sendEmailLogsToEmailQueue(ByVal period As String, ByVal recid_str As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim cenid As Integer
            cenid = cBase._open_Cen_ID
            Dim SPName As String = "[sp_sendToQueue_Email_Notifications_Delivery_Log]"
            Dim params() As String = {"@period", "@recids", "@cenid"}
            Dim values() As Object = {period, recid_str, cenid}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, DbType.Int32}
            Dim lengths() As Integer = {50, -1, 4}
            Return _RealService.UpdateBySPPublic(Tables.EMAIL_SCHEDULER_LOG, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
        End Function


        Public Function Get_chartCentreAndYearID(ByVal CHART_ID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)

            Dim SPName As String = "[sp_get_chartCentreAndYearID]"
            Dim params() As String = {"@CHART_ID"}
            Dim values() As Object = {CHART_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}

            Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_INFO, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)

        End Function
        Public Function Get_AddressBook_AdditionalInfo(ByVal Ab_id As String) As DataTable
            Return GetRecordByColumn("C_REC_ID", Ab_id, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK_ADDITIONAL_INFO)
        End Function
        'Public Function InsertUserProfile(inParam As Param_Txn_Insert_UserProfile, Optional Cenid As String = Nothing, Optional yearid As String = Nothing, Optional userid As String = Nothing) As Object
        '    Return ExecuteGroup(RealTimeService.RealServiceFunctions.Addresses_InsertUserProfile, ClientScreen.Facility_AddressBook, inParam, False)
        'End Function
        Public Function get_Email_Notifications_Queue() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT * from EMAIL_SCHEDULER_QUEUE WHERE CENTER_ID =" & cBase._open_Cen_ID
            Return _RealService.List(Tables.EMAIL_SCHEDULER_QUEUE, query, Tables.EMAIL_SCHEDULER_QUEUE.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo)
            )
        End Function
        Public Function get_WhatsApp_Notifications_Queue() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT WQ.REC_ID, WQ.PHONE, WQ.NAME, WQ.MESSAGE,  WQ.CENTER_ID, WQ.ADD_ON, WQ.ADD_BY, WQ.SCHEDULED_SENT_ON, WQ.MEDIA_FILE_PATH, WQ.MEDIA_CAPTION, WQ.SENDER_PHONE, WQ.SENDING_PRIORITY, CI.CEN_NAME, CI.CEN_UID FROM WHATSAPP_MESSAGE_QUEUE WQ LEFT OUTER JOIN  centre_info CI ON WQ.CENTER_ID =  CI.CEN_ID WHERE WQ.CENTER_ID =" & cBase._open_Cen_ID & " ORDER BY ADD_ON DESC"
            Return _RealService.List(Tables.WHATSAPP_MESSAGE_QUEUE, query, Tables.WHATSAPP_MESSAGE_QUEUE.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_All_WhatsApp_Notifications_Queue() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT WQ.REC_ID, WQ.PHONE, WQ.NAME, WQ.MESSAGE,  WQ.CENTER_ID, WQ.ADD_ON, WQ.ADD_BY, WQ.SCHEDULED_SENT_ON, WQ.MEDIA_FILE_PATH, WQ.MEDIA_CAPTION, WQ.SENDER_PHONE, WQ.SENDING_PRIORITY, CI.CEN_NAME, CI.CEN_UID FROM WHATSAPP_MESSAGE_QUEUE WQ LEFT OUTER JOIN  centre_info CI ON WQ.CENTER_ID =  CI.CEN_ID ORDER BY ADD_ON DESC"
            Return _RealService.List(Tables.WHATSAPP_MESSAGE_QUEUE, query, Tables.WHATSAPP_MESSAGE_QUEUE.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_Cities() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT CI_NAME, REC_ID FROM MAP_CITY_INFO WHERE REC_STATUS <>" & Common_Lib.Common.Record_Status._Deleted & " ORDER BY CI_NAME"
            Return _RealService.List(Tables.MAP_CITY_INFO, query, Tables.MAP_CITY_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_Districts() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT DI_NAME, REC_ID FROM MAP_DISTRICT_INFO WHERE REC_STATUS <>" & Common_Lib.Common.Record_Status._Deleted & " ORDER BY DI_NAME"
            Return _RealService.List(Tables.MAP_DISTRICT_INFO, query, Tables.MAP_DISTRICT_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_Email_Notifications_Delivery_Log(ByVal FromDate? As DateTime, ToDate? As DateTime) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
            Dim SPName As String = "[sp_get_Email_Notifications_Delivery_Log]"
            Dim params() As String = {"@FROM_DATE", "@TO_DATE", "@CEN_ID"}
            Dim values() As Object = {FromDate, ToDate, cBase._open_Cen_ID.ToString}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String}
            Dim lengths() As Integer = {12, 12, 6}
            Return _RealService.ListFromSP(RealTimeService.Tables.EMAIL_SCHEDULER_LOG, SPName, RealTimeService.Tables.EMAIL_SCHEDULER_LOG.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function
        Public Function get_Whatsapp_Notifications_Delivery_Log(ByVal FromDate? As DateTime, ToDate? As DateTime) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)

            Dim SPName As String = "[sp_get_Whatsapp_Notifications_Delivery_Log]"
            Dim params() As String = {"@FROM_DATE", "@TO_DATE", "@CEN_ID"}
            Dim values() As Object = {FromDate, ToDate, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 4}

            Return _RealService.ListFromSP(RealTimeService.Tables.WHATSAPP_MSG_DELIVERY_LOG, SPName, RealTimeService.Tables.SERVICE_CHART_INFO.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function
        Public Function ExportData2GoogleSheet_RemoveSheetId(Form_Instance_Id As Int32)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "Delete from TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET where Form_Instance_Id = " & Form_Instance_Id
            Return _RealService.List(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, query, Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET.ToString, GetBaseParams(ClientScreen.Facility_ChartInfo))
        End Function
        Public Function ExportData2GoogleSheet(Form_Instance_Id As Int32, email As String, overwrite As Boolean) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.Export2GoogleSheet(Form_Instance_Id, email, overwrite, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function OverwriteFormResponsesOfGoogleSheet() As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.OverwriteFormResponsesOfGoogleSheet()
        End Function
        Public Function ImportDataFromGoogleSheet(url As String, email As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.ImportDataFromGoogleSheet(url, email, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
            'Return Nothing
        End Function
        Public Sub Delete_Form_Response_Notification_Queue(ByVal RecId As String, Notification_Type As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            If Notification_Type.Contains("Whatsapp") Then
                _RealService.DeleteByCondition(Tables.WHATSAPP_MESSAGE_QUEUE, "REC_ID ='" & RecId & "'", inbasicparam)
            ElseIf Notification_Type.Contains("Email") Then
                _RealService.DeleteByCondition(Tables.EMAIL_SCHEDULER_QUEUE, "REC_ID ='" & RecId & "'", inbasicparam)
            ElseIf Notification_Type.Contains("SMS") Then
                '_RealService.DeleteByCondition(Tables.NOTIFICATION_BATCHES_INFO, "REC_ID ='" & RecId & "'", inbasicparam)
            End If
        End Sub
        Public Function get_UserName_Category_From_RegNo(ByVal Reg_No As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim SPName As String = "[sp_get_UserName_Category_From_RegNo]"
            Dim params() As String = {"@REG_NO"}
            Dim values() As Object = {Reg_No}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {50}
            Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_RESPONSES, SPName, RealTimeService.Tables.SERVICE_CHART_RESPONSES.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function
        Public Function ListPreviewData2GoogleSheet(dt As DataTable, email As String) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.ListPreviewData2GoogleSheet(dt, email)
        End Function
        Public Function get_QuizResult(ByVal Optional FormId As Integer? = Nothing, Optional EventId As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim SPName As String = "[sp_rpt_Quiz_Results]"
            Dim params() As String = {"@EVENTID", "@FORMID"}
            Dim values() As Object = {EventId, FormId}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 4}
            Return _RealService.ListFromSP(RealTimeService.Tables.SERVICE_CHART_RESPONSES, SPName, RealTimeService.Tables.SERVICE_CHART_RESPONSES.ToString(), params, values, dbTypes, lengths, inbasicparam)
        End Function
        Public Sub ShiftFormToAnotherCentre(ByVal ChartID As String, CenId As Int32)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Dim Query As String = "UPDATE service_chart_info SET CI_CEN_ID = '" & CenId & "' WHERE REC_ID=" & ChartID
            _RealService.List(Tables.SERVICE_CHART_INFO, Query, Tables.SERVICE_CHART_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartInfo)) ' this query is actually update call 
            Query = "UPDATE service_chart_srno SET CSN_CEN_ID ='" & CenId & "' WHERE CSN_CHART_ID=" & ChartID
            _RealService.List(Tables.SERVICE_CHART_SRNO, Query, Tables.SERVICE_CHART_SRNO.ToString, GetBaseParams(ClientScreen.Facility_ChartInfo)) ' this query is actually update call 
        End Sub
        Public Function get_ChartResponseInGridProfileEdit_Occupation() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = " Select MISC_NAME As Name, MISC_ID As MASTERID, MISC_SRNO As SR, REC_ID As ID FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  And MISC_ID ='OCCUPATION' ORDER BY MISC_NAME"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_ChartResponseInGridProfileEdit_SpecialityOrHobby() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = " Select MISC_NAME As Name, MISC_ID As MASTERID, MISC_SRNO As SR, REC_ID As ID FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  And MISC_ID ='SPECIALTIES' ORDER BY MISC_NAME"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
        Public Function get_ChartResponseInGridProfileEdit_Qualification() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = " Select MISC_NAME As Name, MISC_ID As MASTERID, MISC_SRNO As SR, REC_ID As ID FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  And MISC_ID ='QUALIFICATIONS' ORDER BY MISC_NAME"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function

        Public Function get_phonenos_ofBatch_Wpaid(ByVal NotificationSettings As Param_FormResponse_Notification, ChartInstanceID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_Notification_Batches]"
            Dim cenID As Int32 = IIf(NotificationSettings.CenID = 0, cBase._open_Cen_ID, NotificationSettings.CenID)
            Dim AddBy As String = IIf(String.IsNullOrWhiteSpace(NotificationSettings.AddBy), cBase._open_User_ID, NotificationSettings.AddBy)

            Dim params() As String =
            {
                "@CONTENT", "@BATCH_NAME", "@CHART_INSTANCE_ID", "@RESPONSE_STATUS_OR_CATEGORY",
                "@SUBJECT", "@CC", "@BCC", "@REPLY_TO_EMAIL", "@SENDER_EMAIL_TYPE",
                "@EMAIL", "@PASSWORD", "@SENDER_WHATSAPP_NO_TYPE", "@ATTACHMENT_ID",
                "@WHATSAPPNO", "@DELIVERY_SPEED", "@ADMIN_WHATSAPP_NO", "@ADMIN_EMAIL",
                "@NOTIFICATION_MODE", "@LOGGED_IN_USER", "@CENID"
            }
            Dim values() As Object =
            {
                NotificationSettings.Content, NotificationSettings.BatchName, NotificationSettings.ChartInstanceId, NotificationSettings.ResponseStatus,
                NotificationSettings.Subject, NotificationSettings.CC, NotificationSettings.BCC, NotificationSettings.ReplyToEmail, NotificationSettings.SenderEmailType,
                NotificationSettings.Email, NotificationSettings.Password, NotificationSettings.SenderWhatsappNoType, NotificationSettings.File_Name, NotificationSettings.Whatsappno,
                NotificationSettings.DeliverySpeed, NotificationSettings.AdminWhatsAppNo, NotificationSettings.AdminEmail, NotificationSettings.Mode, AddBy,
                cenID
            }
            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32
            }
            Dim lengths() As Integer =
            {
                 -1, -1, 4, 15,
                 -1, 2000, 2000, 2000, 10,
                 2000, 50, 10, 2000,
                 15, 15, 15, 2000,
                 10, 20, 4
            }
            Dim Id As Integer = _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_FormResponse)
            If NotificationSettings.SelectedNotificationRadio = "formresponses" Then
                SPName = "[get_phonenos_ofbatch_formresponses_wpaid]"
                params = {"@BATCH_NAME", "@LOGIN_USERID", "@CENID", "@selected_group", "@selectedids_str"}
                values = {NotificationSettings.BatchName, AddBy, cenID, NotificationSettings.selectedFormResponses_group, NotificationSettings.selectedFormResponses_str,
                    cBase._open_Year_ID
                }
                dbTypes = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
                lengths = {-1, 80, 4, 50, -1}
            ElseIf NotificationSettings.SelectedNotificationRadio = "addressbook" Then
                'Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                SPName = "[get_phonenos_ofbatch_addressbook_wpaid]"
                params = {"@BATCH_NAME", "@LOGIN_USERID", "@CENID", "@designationIds", "@occupationIds", "@categoryIds", "@magazineIds",
                                "@specialityIds", "@eventIds", "@titleIds", "@wingIds", "@cityIds", "@districtIds", "@selected_group", "@selectedids_str", "@financial_year"}
                values = {NotificationSettings.BatchName, AddBy, cenID, NotificationSettings.SelectedDesignationIds,
                    NotificationSettings.SelectedOccupationIds, NotificationSettings.SelectedCategoryIds, NotificationSettings.SelectedMagazineIds,
                    NotificationSettings.SelectedSpecialitiesIds, NotificationSettings.SelectedEventsIds, NotificationSettings.SelectedTitleIds,
                    NotificationSettings.SelectedWingIds, NotificationSettings.SelectedCityIds, NotificationSettings.SelectedDistrictIds,
                    NotificationSettings.selectedAddresses_group, NotificationSettings.selectedContacts_str, cBase._open_Year_ID
                }
                dbTypes = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                    Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                    Data.DbType.String, Data.DbType.String, Data.DbType.String
                    }
                lengths = {-1, 80, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 50, -1, 4}
            End If
            Return _RealService.ListFromSP(RealTimeService.Tables.NOTIFICATION_BATCHES_INFO, SPName, RealTimeService.Tables.NOTIFICATION_BATCHES_INFO.ToString(),
                params, values, dbTypes, lengths, inbasicparam)
        End Function

        Public Function Insert_BatchName_givenphoneNumbers_Wpaid(ByVal NotificationSettings As Param_FormResponse_Notification, ChartInstanceID As Int32) As Int32
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_Notification_Batches]"
            Dim cenID As Int32 = IIf(NotificationSettings.CenID = 0, cBase._open_Cen_ID, NotificationSettings.CenID)
            Dim AddBy As String = IIf(String.IsNullOrWhiteSpace(NotificationSettings.AddBy), cBase._open_User_ID, NotificationSettings.AddBy)

            Dim params() As String =
            {
                "@CONTENT", "@BATCH_NAME", "@CHART_INSTANCE_ID", "@RESPONSE_STATUS_OR_CATEGORY",
                "@SUBJECT", "@CC", "@BCC", "@REPLY_TO_EMAIL", "@SENDER_EMAIL_TYPE",
                "@EMAIL", "@PASSWORD", "@SENDER_WHATSAPP_NO_TYPE", "@ATTACHMENT_ID",
                "@WHATSAPPNO", "@DELIVERY_SPEED", "@ADMIN_WHATSAPP_NO", "@ADMIN_EMAIL",
                "@NOTIFICATION_MODE", "@LOGGED_IN_USER", "@CENID"
            }
            Dim values() As Object =
            {
                NotificationSettings.Content, NotificationSettings.BatchName, NotificationSettings.ChartInstanceId, NotificationSettings.ResponseStatus,
                NotificationSettings.Subject, NotificationSettings.CC, NotificationSettings.BCC, NotificationSettings.ReplyToEmail, NotificationSettings.SenderEmailType,
                NotificationSettings.Email, NotificationSettings.Password, NotificationSettings.SenderWhatsappNoType, NotificationSettings.File_Name, NotificationSettings.Whatsappno,
                NotificationSettings.DeliverySpeed, NotificationSettings.AdminWhatsAppNo, NotificationSettings.AdminEmail, NotificationSettings.Mode, AddBy,
                cenID
            }
            Dim dbTypes() As System.Data.DbType =
            {
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                Data.DbType.String, Data.DbType.String, Data.DbType.Int32
            }
            Dim lengths() As Integer =
            {
                 -1, -1, 4, 15,
                 -1, 2000, 2000, 2000, 10,
                 2000, 50, 10, 2000,
                 15, 15, 15, 2000,
                 10, 20, 4
            }
            Dim Id As Integer = _RealService.InsertBySPPublic(Tables.NOTIFICATION_BATCHES_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_createForm))

            Return Id

        End Function

        Public Function wpaidDeliveryLog(ByVal recid As String, ByVal phonenum As String, ByVal batchName As String, ByVal mediapath As String,
                                         ByVal message As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim params() As String = {"@RECID", "@PHONE", "@CENID", "@BATCHNAME", "@MEDIA_PATH", "@MEDIA_CAPTION", "@MESSAGE", "@SENT_STATUS", "@REASON", "@SENDER"}
            Dim values() As Object = {recid, phonenum, cBase._open_Cen_ID, batchName, mediapath, "", message, False, "initiated", "whatsapp_api"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                                                    Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 15, 4, 1000, 500, -1, -1, 2, 50, 50}
            Return _RealService.ListFromSP(Tables.MOBILE_LOGIN_OTP_INFO, "[whatsapp_paid_delivery_log]", Tables.MOBILE_LOGIN_OTP_INFO.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
    End Class

#End Region
End Class
