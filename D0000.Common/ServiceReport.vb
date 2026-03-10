'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class ServiceReport
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetInstitutes() As DataTable
            Return GetInstituteList(ClientScreen.Facility_ServiceReport)
        End Function
        'Shifted
        Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            Dim dt_projectsFromMiscInfo As DataTable = GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Facility_ServiceReport, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)

            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = " SELECT SP_PROJ_NAME as " & MiscNameColumnHead & ",REC_ID as " & RecIDColumnHead & " FROM SERVICE_PROJECT_INFO WHERE  SP_CEN_ID = " & Convert.ToString(cBase._open_Cen_ID) & " ORDER BY SP_PROJ_NAME "
            Dim dt_projectsOfUID As DataTable = _RealService.List(Tables.SERVICE_PROJECT_INFO, query, Tables.SERVICE_PROJECT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
            'Dim dt_projectsOfUID As DataTable = GetDataFromTables("SERVICE_PROJECT_INFO", cBase._open_Cen_ID, cBase._open_User_ID, True)

            dt_projectsFromMiscInfo.Merge(dt_projectsOfUID)

            Return dt_projectsFromMiscInfo
        End Function
        Public Function GetMasterProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            Return GetMisc("MASTER GODLY SERVICE PROJECTS", ClientScreen.Facility_ServiceReport, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        End Function

        Public Function GetTreeDetails() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'Name',rec_id as 'ID',misc_remark1 as KP_ID from misc_info where misc_id='TREE & PLANTS' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetProgramType() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'Name',rec_id as 'ID' from misc_info where misc_id='GODLY SERVICE PROGRAM TYPE' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetProgramVenueType() As DataTable
            Return GetMisc("VENUE TYPE", ClientScreen.Facility_ServiceReport, "Name", "ID")
        End Function
        Public Function GetProgramOccasion() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'Name',rec_id as 'ID' from misc_info where misc_id='GODLY SERVICE PROGRAM OCCASION' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetParticipantCategory() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'NAME',rec_id as 'ID' from misc_info where misc_id='Participant Category' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetParticipantSubCategory() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'NAME',rec_id as 'ID' from misc_info where misc_id='Participant Sub Category' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetProgramType_Additional() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'NAME',rec_id as 'ID' from misc_info where misc_id='Program Type' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetProgramLocationType() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select misc_name as 'NAME',rec_id as 'ID' from misc_info where misc_id='Location Type' and Rec_status in (0,1,2) order by misc_name"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetParties(Optional OnlyFaculty As Boolean = False, Optional OnlyEventOrganiser As Boolean = False) As DataTable
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            If OnlyFaculty = True Then
                Param.Type = "FACULTY"
            Else
                If OnlyEventOrganiser = True Then
                    Param.Type = "EVENT ORGANIZER"
                End If
            End If
            Dim _Add As Addresses = New Addresses(cBase)
            Return _Add.GetList(ClientScreen.Facility_ServiceReport, Param)
        End Function
        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetList</remarks>
        Public Function GetList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceReport_GetList, ClientScreen.Facility_ServiceReport, cBase._Date_Format_Current)
        End Function

        'Shifted
        Public Function GetWings() As DataTable
            Return GetWingsList(ClientScreen.Facility_ServiceReport)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        ''' <summary>
        ''' Get Wings Record, Shifted
        ''' </summary>
        ''' <param name="SR_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetWingsRecord</remarks>
        Public Function GetWingsRecord(ByVal SR_Rec_ID As String) As DataTable
            Dim query As String = "Select SR_WING_ID  from SERVICE_REPORT_WING_INFO where  REC_STATUS IN (0,1,2) AND SR_REC_ID= '" & SR_Rec_ID & "'"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceReport_GetWingsRecord, ClientScreen.Facility_ServiceReport, SR_Rec_ID)
        End Function
        Public Function GetCentres() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            'Dim query As String = "select Cen_id,cen_name,cen_ins_id,cen_uid,cen_bk_pad_no from centre_info where (cen_ins_id in(00001,00009) OR CEN_ID IN ( SELECT DISTINCT SR_CEN_ID FROM service_report_info )) and cen_cancellation_date is null and rec_status in(0,1,2) ORDER BY cen_name"
            Dim query As String = "select distinct cen_name,cen_bk_pad_no from centre_info where cen_cancellation_date is null and rec_status in(0,1,2) ORDER BY cen_name"
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetInstitutesOfCentre(cen_name As String, cen_bk_pad_no As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select ci.cen_id,ii.ins_id,ii.ins_name from centre_info ci inner join institute_info ii on ci.cen_ins_id=ii.ins_id where ci.CEN_BK_PAD_NO='" + cen_bk_pad_no + "' and ci.cen_name='" + cen_name + "' and ci.cen_cancellation_date is null and ci.rec_status in(0,1,2) and ii.REC_STATUS in(0,1,2) order by ii.ins_id"
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetDocumentNameID() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select rec_id from misc_info where misc_id='Attachment Category' and MISC_REMARK1='GODLY SERVICE REPORT'"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetGuestRecord(ByVal SR_Rec_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            'Dim condition As String = "Where SR_Rec_ID='" & SR_Rec_ID & "' Order BY SR_SR_NO"
            Dim query As String = "Select SR_GUEST_NAME = STUFF((SELECT ', ' + Case WHEN LEN(COALESCE(SR_GUEST_NAME,'')) > 0 THEN SR_GUEST_NAME ELSE '' END  + Case WHEN LEN(COALESCE(SR_GUEST_DESIG,'')) > 0 THEN ' (' + SR_GUEST_DESIG + ')' ELSE '' END" &
            "" & " From SERVICE_REPORT_GUEST_INFO Where SR_Rec_ID = '" & SR_Rec_ID & "' And (LEN(COALESCE(SR_GUEST_NAME,'')) > 0 Or LEN(COALESCE(SR_GUEST_DESIG,'')) > 0) order by SR_SR_NO For Xml PATH ('')),1,2,''),'' SR_GUEST_DESIG"
            Return _RealService.List(Tables.SERVICE_REPORT_GUEST_INFO, query, Tables.SERVICE_REPORT_GUEST_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))

            'Return GetRecordByCustom(condition, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_GUEST_INFO)
        End Function
        Public Function GetActivityRecord(ByVal SR_Rec_ID As String) As DataTable
            Return GetRecordByColumn("SR_Rec_ID", SR_Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_ACTIVITY_INFO)
        End Function
        Public Function GetProgramTypeRecord(ByVal SR_Rec_ID As String) As DataTable
            Return GetRecordByColumn("SR_Rec_ID", SR_Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_PROGRAM_TYPE_INFO)
        End Function
        Public Function GetProgramOccasionRecord(ByVal SR_Rec_ID As String) As DataTable
            Return GetRecordByColumn("SR_Rec_ID", SR_Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_PROGRAM_OCCASION_INFO)
        End Function
        Public Function GetProgramCollaboratedCenters(ByVal SR_Rec_ID As String) As DataTable
            Return GetRecordByColumn("SR_Rec_ID", SR_Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_COLLABORATECENTERS_INFO)
        End Function
        Public Function GetProgramAdditionalInfoRecord(ByVal SR_Rec_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select * from Service_Report_Additional_Info where Sra_sr_rec_id='" & SR_Rec_ID & "'"
            Return _RealService.List(Tables.SERVICE_REPORT_ADDITIONAL_INFO, query, Tables.SERVICE_REPORT_ADDITIONAL_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetProgramVenueTypeRecord(ByVal SR_Rec_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "select * from SERVICE_REPORT_VENUE_TYPE_INFO where Srvt_sr_rec_id='" & SR_Rec_ID & "'"
            Return _RealService.List(Tables.SERVICE_REPORT_VENUE_TYPE_INFO, query, Tables.SERVICE_REPORT_VENUE_TYPE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetOrganiserNames(ByVal Cen_ID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT DISTINCT SP.SP_SERVICE_PLACE_NAME AS CEN_NAME FROM 
                                    SERVICE_PLACE_INFO AS SP 
                                    INNER JOIN CENTRE_INFO AS CI ON SP.SP_CEN_ID = CEN_ID
                                    WHERE CEN_BK_PAD_NO = (SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " & Cen_ID & ")
                                    ORDER BY SP.SP_SERVICE_PLACE_NAME"
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetServiceEventDetails(RecID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@REC_ID"}
            Dim values() As Object = {RecID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, "[SP_GET_SERVICE_REPORT_EVENT_DETAILS]", Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetMonthlyServiceReport() As Object
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim startDate As DateTime = New DateTime(DateTime.Now.Year, Now.Month, 1)
            Dim endDate As DateTime = startDate.AddMonths(1).AddDays(-1)
            Dim query As String = "Select Count(*) FROM SERVICE_REPORT_INFO Where SR_CEN_ID=" & cBase._open_Cen_ID & " AND SR_PROG_TO_DATE BETWEEN '" & startDate.ToString(cBase._Server_Date_Format_Long) & "' AND '" & endDate.ToString(cBase._Server_Date_Format_Long) & "'"
            Return _RealService.GetScalar(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetDataForServiceReportEmail(ByVal ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@RecID"}
            Dim values() As Object = {ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, "sp_get_Data_ServiceReport_Email", Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetSewanjaliData(activityName As String, Start_Date As DateTime?, End_Date As DateTime?, CenterID As Int32?, speaker As String, ProjectID As String,
                                         ratingFrom As Int32?, ratingTo As Int32?, cityid As String, stateid As String, attachmentRootPath As String, WingID As String,
                                         InsttID As String, ZoneID As String, SubZoneID As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@START_DATE", "@END_DATE", "@CENTER_ID", "@SPEAKER", "@PROJECT_ID", "@RATING_FROM", "@RATING_TO", "@ATTACHMENT_ROOT_PATH",
                                        "@WINGID", "@CITY_ID", "@STATE_ID", "@INS_ID", "@REPORT_ACTIVITY_NAME", "@ZONE_ID", "@SUB_ZONE_ID"}
            Dim values() As Object = {Start_Date, End_Date, CenterID, speaker, ProjectID, ratingFrom, ratingTo, attachmentRootPath, WingID, cityid, stateid, InsttID, activityName, ZoneID, SubZoneID}
            Dim dbTypes() As System.Data.DbType = {DbType.Date, DbType.Date, DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32,
                                    DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {12, 12, 6, 60, 36, 2, 2, 255, 36, 36, 36, 8, 100, 50, 50}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_REPORT_INFO, "sp_get_Sewanjali_Report", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_ServiceReport) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceReport_Insert, ClientScreen.Facility_ServiceReport, InParam)
        End Function

        ''' <summary>
        ''' Insert Wings, Shifted
        ''' </summary>
        ''' <param name="InWings"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertWings</remarks>
        Public Function InsertWings(ByVal InWings As Parameter_InsertWings_ServiceReport) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceReport_InsertWings, ClientScreen.Facility_ServiceReport, InWings)
        End Function

        ''' <summary>
        ''' Insert Guest, Shifted
        ''' </summary>
        ''' <param name="InGuest"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertGuest</remarks>
        Public Function InsertGuest(ByVal InGuest As Parameter_InsertGuest_ServiceReport) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceReport_InsertGuest, ClientScreen.Facility_ServiceReport, InGuest)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_ServiceReport) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ServiceReport_Update, ClientScreen.Facility_ServiceReport, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.SERVICE_REPORT_INFO, ClientScreen.Facility_ServiceReport)
        End Function


        Public Function DeleteWing(ByVal SR_Rec_Id As String) As Boolean
            Return DeleteByCondition("SR_REC_ID    ='" & SR_Rec_Id & "'", Tables.SERVICE_REPORT_WING_INFO, ClientScreen.Facility_ServiceReport)
        End Function

        Public Function DeleteGuest(ByVal SR_Rec_Id As String) As Boolean
            Return DeleteByCondition("SR_REC_ID    ='" & SR_Rec_Id & "'", Tables.SERVICE_REPORT_GUEST_INFO, ClientScreen.Facility_ServiceReport)
        End Function

        Public Function DeleteAttachments(ByVal SR_Rec_Id As String) As Boolean
            Return DeleteByCondition("SR_REC_ID    ='" & SR_Rec_Id & "'", Tables.SERVICE_REPORT_ATTACHMENT_INFO, ClientScreen.Facility_ServiceReport)
        End Function

        Public Function InsertServiceReport_Txn(InParam As Param_Txn_Insert_ServiceReport) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ServiceReport_InsertServiceReport_Txn, ClientScreen.Facility_ServiceReport, InParam)
        End Function

        Public Function UpdateServiceReport_Txn(UpParam As Param_Txn_Update_ServiceReport) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ServiceReport_UpdateServiceReport_Txn, ClientScreen.Facility_ServiceReport, UpParam)
        End Function

        Public Function DeleteServiceReport_Txn(DelParam As Param_Txn_Delete_ServiceReport) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ServiceReport_DeleteServiceReport_Txn, ClientScreen.Facility_ServiceReport, DelParam)
        End Function

        'Public Function GetDraftData() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim query As String = "SELECT SR_PROG_NAME, SR_PROG_VENUE, SR_PROG_FR_DATE, SR_PROG_TO_DATE, SR_PROG_FR_TIME, SR_PROG_TO_TIME, SR_PROG_SPEAKER, SR_PROG_BRIEF FROM SERVICE_REPORT_INFO"
        '    Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        'End Function
        'Public Function GetUpcomingData() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim query As String = "SELECT SR_PROG_NAME, SR_PROG_VENUE, SR_PROG_FR_DATE, SR_PROG_TO_DATE, SR_PROG_FR_TIME, SR_PROG_TO_TIME, SR_PROG_SPEAKER, SR_PROG_BRIEF FROM SERVICE_REPORT_INFO"
        '    Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        'End Function
        'Public Function GetPastData() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim query As String = "SELECT SR_PROG_NAME, SR_PROG_VENUE, SR_PROG_FR_DATE, SR_PROG_TO_DATE, SR_PROG_FR_TIME, SR_PROG_TO_TIME, SR_PROG_SPEAKER, SR_PROG_BRIEF FROM SERVICE_REPORT_INFO"
        '    Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        'End Function
        'Public Function GetCancelledData() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim query As String = "SELECT SR_PROG_NAME, SR_PROG_VENUE, SR_PROG_FR_DATE, SR_PROG_TO_DATE, SR_PROG_FR_TIME, SR_PROG_TO_TIME, SR_PROG_SPEAKER, SR_PROG_BRIEF FROM SERVICE_REPORT_INFO"
        '    Return _RealService.List(Tables.SERVICE_REPORT_INFO, query, Tables.SERVICE_REPORT_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        'End Function
        Public Function Get_SM_EventsMgtList(event_status As String, attachmentRootPath As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_EventsMgtList"
            'If Start_Date = "#1/1/0001 12:00:00 AM#" Then
            '    Start_Date = Nothing
            'End If
            'If End_Date = "#1/1/0001 12:00:00 AM#" Then
            '    End_Date = Nothing
            'End If

            Dim paramters As String() = {"@event_status", "@attachment_root_path"}
            Dim values() As Object = {event_status, attachmentRootPath}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
            Dim lengths() As Integer = {25, 100}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function UpdateMobileVerificationStatus(MobNo As String, OTP As String) As Boolean
            Try
                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim paramters As String() = {"@MOB_NO", "@OTP"}
                Dim values() As Object = {MobNo, OTP}
                Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
                Dim lengths() As Integer = {15, 6}
                Return _RealService.UpdateBySPPublic(Tables.SERVICE_REPORT_INFO, "sp_update_mobile_verification_status", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
            Catch ex As Exception
                CommonExceptionCall(ex)
                Return False
            End Try
        End Function
        Public Function GetMobileVerificationStatus(MobNo As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@MOB_NO"}
            Dim values() As Object = {MobNo}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {15}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, "sp_get_mobile_verification_status", Tables.SERVICE_REPORT_INFO.ToString, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetCenterMobileAndLocation(ByVal Cen_ID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT CASE WHEN COALESCE(NULLIF(CS.CEN_MOB_NO_1,''), NULLIF(CI.CEN_MOB_NO_1,''),NULLIF(MAIN.CEN_MOB_NO_1,'')) IS NOT NULL THEN COALESCE(CS.CEN_MOB_NO_1,CI.CEN_MOB_NO_1,MAIN.CEN_MOB_NO_1)
                                         WHEN COALESCE(NULLIF(CI.CEN_MOB_NO_2,''),NULLIF(MAIN.CEN_MOB_NO_2,'')) IS NOT NULL THEN COALESCE(CI.CEN_MOB_NO_2,MAIN.CEN_MOB_NO_2) ELSE '' END as Cen_Mobile,
                                    COALESCE(ci.CEN_LOC_LAT,MAIN.CEN_LOC_LAT) CEN_LOC_LAT, 
			                        COALESCE(ci.CEN_LOC_LONG,MAIN.CEN_LOC_LONG) CEN_LOC_LONG FROM CENTRE_INFO CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1 LEFT JOIN CENTRE_SERVICE_CONTACT_DETAILS AS CS ON CI.CEN_ID	=CS.CEN_ID Where CI.CEN_ID=" & Cen_ID
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function generateServiceReportByPeriod(ByVal cenid As Int32, ByVal fromdate As DateTime?, ByVal todate As DateTime?, ByVal locationType As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@FROMDATE", "@TODATE", "@CENTERID", "@LOCATIONTYPE"}
            Dim values() As Object = {fromdate, todate, cenid, locationType}
            Dim dbTypes() As System.Data.DbType = {DbType.DateTime2, DbType.DateTime2, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {30, 30, 8, 10}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, "[SP_GET_SEWA_REPORT]", Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetCenterDetailsForCenID(ByVal Cen_ID As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT *  from CENTRE_INFO where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = '" & Cen_ID & "'"
            Return _RealService.List(Tables.CENTRE_INFO, Query, Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
    End Class
#End Region
End Class
