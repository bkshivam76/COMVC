Imports System.Data

Namespace Real
#Region "Facility"
    <Serializable>
    Public Class ServiceReport
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_ServiceReport
            Public ProjectID As String
            Public ProgType As String
            Public Name As String
            Public Venue As String
            Public FromDate As String
            Public ToDate As String
            Public FromTime As String
            Public ToTime As String
            Public Period As String
            Public Brief As String
            Public SpecialMoments As String
            Public Beneficiaries As Double
            Public Followup As String
            Public Speaker As String
            Public Feedback As String
            Public Rec_ID As String
            Public VVIP_Testimonial As String
            Public ProgCategory As String
            Public NewsLink As String
            Public Cultural As String
            Public ReporterEmail As String
            Public ReporterMobile As String
            Public CenID As String
            Public MediaLink As String
            Public MasterProjID As String
            Public Offline_Beneficiaries As Double
            Public Online_Beneficiaries As Double
            Public NoOfEvent As Integer
            Public ProgramOccasion As String
            Public ProgramOrganiser As String
            Public Institute_ID As String
            Public Child_Beneficiaries As Int32?
            Public Youth_Beneficiaries As Int32?
            Public Male_Beneficiaries As Int32?
            Public Female_Beneficiaries As Int32?
            Public Women_Beneficiaries As Int32?
            Public SeniorCitizen_Beneficiaries As Int32?
        End Class
        <Serializable>
        Public Class Parameter_InsertWings_ServiceReport
            Public Sr_ID As String
            Public WingID As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertGuest_ServiceReport
            Public Sr_ID As String
            Public Sr_No As Integer
            Public GusetName As String
            Public GuestDesignation As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertAcivityDetail_ServiceReport
            Public Sr_ID As String
            Public ActivityName As String
            Public ActivitySpecialDetail As String
            Public ActivityCount As Int32
            Public Rec_ID As String
            Public Latitude As String
            Public Longitude As String
        End Class
        <Serializable>
        Public Class Parameter_InsertProgramType_ServiceReport
            Public Sr_ID As String
            Public ProgTypeID As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertCollaborateCenters_ServiceReport
            Public Sr_ID As String
            Public CEN_ID As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertProgramOccasion_ServiceReport
            Public Sr_ID As String
            Public ProgOccasionID As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertAdditionalInfo_ServiceReport
            Public Sr_ID As String
            Public OrganizedFor As String
            Public ParticipantType As String
            Public ParticipantCategory As String
            Public ParticipantSubCategory As String
            Public ProgramType As String
            Public ProgramLocationType As String
            Public ProgramCoordinator_1 As String
            Public ProgramCoordinator_2 As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertVenueType_ServiceReport
            Public Sr_ID As String
            Public Venue_misc_id As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_ServiceReport
            Public ProjectID As String
            Public ProgType As String
            Public Name As String
            Public Venue As String
            Public FromDate As String
            Public ToDate As String
            Public FromTime As String
            Public ToTime As String
            Public Period As String
            Public Brief As String
            Public SpecialMoments As String
            Public Beneficiaries As Double
            Public Followup As String
            Public Speaker As String
            Public Feedback As String
            Public Rec_ID As String
            Public VVIP_Testimonial As String
            Public ProgCategory As String
            Public NewsLink As String
            Public Cultural As String
            Public ReporterEmail As String
            Public ReporterMobile As String
            Public CenID As String
            Public MediaLink As String
            Public MasterProjID As String
            Public Offline_Beneficiaries As Double
            Public Online_Beneficiaries As Double
            Public NoOfEvent As Integer
            Public ProgramOccasion As String
            Public ProgramOrganiser As String
            Public Institute_ID As String
            Public Child_Beneficiaries As Int32?
            Public Youth_Beneficiaries As Int32?
            Public Male_Beneficiaries As Int32?
            Public Female_Beneficiaries As Int32?
            Public Women_Beneficiaries As Int32?
            Public SeniorCitizen_Beneficiaries As Int32?
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_ServiceReport
            Public param_Insert_ServiceReport As Parameter_Insert_ServiceReport
            Public InsertWings() As Parameter_InsertWings_ServiceReport = Nothing
            Public InsertActivityDetails() As Parameter_InsertAcivityDetail_ServiceReport = Nothing
            Public InsertProgType() As Parameter_InsertProgramType_ServiceReport = Nothing
            Public InsertProgOccasion() As Parameter_InsertProgramOccasion_ServiceReport = Nothing
            Public InsertCollaborateCenters() As Parameter_InsertCollaborateCenters_ServiceReport = Nothing
            Public param_InsertGuest1 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest2 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest3 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest4 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest5 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertAdditonalInfo As Parameter_InsertAdditionalInfo_ServiceReport = Nothing
            Public InsertVenueType() As Parameter_InsertVenueType_ServiceReport = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_ServiceReport
            Public param_Update_ServiceReport As Parameter_Update_ServiceReport
            Public RecID_DeleteWing As String = Nothing
            Public InsertWings() As Parameter_InsertWings_ServiceReport = Nothing
            Public RecID_DeleteAcitvity As String = Nothing
            Public InsertActivityDetails() As Parameter_InsertAcivityDetail_ServiceReport = Nothing
            Public RecID_DeleteProgType As String = Nothing
            Public InsertProgType() As Parameter_InsertProgramType_ServiceReport = Nothing
            Public RecID_DeleteProgOccasion As String = Nothing
            Public InsertProgOccasion() As Parameter_InsertProgramOccasion_ServiceReport = Nothing
            Public RecID_DeleteCollaborateCenters As String = Nothing
            Public InsertCollaborateCenters() As Parameter_InsertCollaborateCenters_ServiceReport = Nothing
            Public RecID_DeleteGuest As String = Nothing
            Public param_InsertGuest1 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest2 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest3 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest4 As Parameter_InsertGuest_ServiceReport = Nothing
            Public param_InsertGuest5 As Parameter_InsertGuest_ServiceReport = Nothing
            Public RecID_DeleteAdditonalInfo As String = Nothing
            Public param_InsertAdditonalInfo As Parameter_InsertAdditionalInfo_ServiceReport = Nothing
            Public RecID_DeleteVenueType As String = Nothing
            Public InsertVenueType() As Parameter_InsertVenueType_ServiceReport = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_ServiceReport
            Public RecID_DeleteAcitvity As String = Nothing
            Public RecID_DeleteProgType As String = Nothing
            Public RecID_DeleteProgOccasion As String = Nothing
            Public RecID_DeleteCollaborateCenters As String = Nothing
            Public RecID_DeleteWing As String = Nothing
            Public RecID_DeleteGuest As String = Nothing
            Public RecID_DeleteAttachments As String = Nothing
            Public RecID_DeleteAdditonalInfo As String = Nothing
            Public RecID_DeleteVenueType As String = Nothing
            Public RecID_Delete As String = Nothing
        End Class
#End Region
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="DateFormatCurrent"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetList</remarks>
        Public Shared Function GetList(ByVal DateFormatCurrent As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Current_Format As String = "'"
            For Each DatePart As String In DateFormatCurrent.Split("/")
                If DatePart.ToLower().Contains("y") Then
                    If (DatePart.Length > 2) Then
                        Current_Format = Current_Format & "%Y/"
                    Else
                        Current_Format = Current_Format & "%y/"
                    End If
                Else
                    Current_Format = Current_Format & "%" & DatePart.Substring(0, 1) & "/"
                End If

            Next
            Current_Format = Current_Format.Substring(0, Current_Format.Length - 1) & "'"

            'Dim OnlineQuery As String = "SELECT SR_PROG_NAME ,SR_PROG_VENUE, " & _
            '                        "dbo.fn_FORMATDATE(SR_PROG_FR_DATE , 'dd-MON-yyyy')  + '  to  ' +  dbo.fn_FORMATDATE(SR_PROG_TO_DATE , 'dd-MON-yyyy') AS  SR_DATE," & _
            '                        "DATEDIFF (dd, SR_PROG_FR_DATE , SR_PROG_TO_DATE )+1 AS SR_PERIOD," & _
            '                        "SR_PROG_FR_TIME + '  to  ' + SR_PROG_TO_TIME AS SR_TIME," & _
            '                        "SR_PROG_PD_TIME AS SR_TIME_PER," & _
            '                        "SR_PROG_BRIEF AS SR_BRIEF," & _
            '                        "SR_PROG_SPEAKER AS SR_SPEAKER," & _
            '                        "SR_PROG_SPL AS SR_SPL," & _
            '                        "SR_PROG_BENEFIT AS SR_BENEFIT," & _
            '                        "SR_PROG_FOLLOWUP AS SR_FOLLOW," & _
            '                        "SR_PROG_FEEDBACK AS SR_FEEDBACK," & _
            '                        "SR_PROJ_ID  AS 'Project ID'," & _
            '                        "REC_ID AS ID  ," & Common.Rec_Detail("SERVICE_REPORT_INFO") & "   " & _
            '                        "FROM SERVICE_REPORT_INFO " & _
            '                   " Where   REC_STATUS IN (0,1,2) AND SR_CEN_ID = " & inBasicParam.openCenID.ToString & " ; "
            'Return dbService.List(ConnectOneWS.Tables.SERVICE_REPORT_INFO, OnlineQuery, ConnectOneWS.Tables.SERVICE_REPORT_INFO.ToString(), inBasicParam)

            Dim SPName As String = "[sp_get_Service_Report_Listing]"
            Dim params() As String = {"@CENID", "@YEARID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Wings Record
        ''' </summary>
        ''' <param name="SR_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetWingsRecord</remarks>
        Public Shared Function GetWingsRecord(ByVal SR_Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query As String = "Select SR_WING_ID,WI.WING_NAME  from SERVICE_REPORT_WING_INFO SRWI INNER JOIN WINGS_INFO WI ON SR_WING_ID=WI.WING_ID where  SRWI.REC_STATUS IN (0,1,2) AND SR_REC_ID= '" & SR_Rec_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.SERVICE_REPORT_WING_INFO, query, ConnectOneWS.Tables.SERVICE_REPORT_WING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If String.IsNullOrEmpty(InParam.ProjectID) Then
                InParam.ProjectID = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            End If
            Dim OnlineQuery As String = "INSERT INTO SERVICE_REPORT_INFO(SR_CEN_ID,SR_PROJ_ID,SR_PROG_TYPE,SR_PROG_NAME,SR_PROG_VENUE, SR_PROG_FR_DATE,SR_PROG_TO_DATE,SR_PROG_FR_TIME,SR_PROG_TO_TIME,SR_PROG_PD_TIME,SR_PROG_BRIEF,SR_PROG_SPL,SR_PROG_BENEFIT,SR_PROG_FOLLOWUP,SR_PROG_SPEAKER,SR_PROG_FEEDBACK," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,SR_PROG_CATEGORY,SR_PROG_NEWS_LINK,SR_PROG_VVIP_TESTIMONIAL,SR_PROG_CULTURAL,REPORTER_EMAIL,REPORTER_MOBILE,SR_MEDIA_LINK,SR_MASTER_PROJ_ID,SR_OFFLINE_PROG_BENEFIT,SR_ONLINE_PROG_BENEFIT,SR_NO_OF_EVENT,SR_PROG_OCCASION,SR_PROG_ORGANISER,SR_INS_ID," &
                                                 "SR_CHILD_BENEFIT,SR_YOUTH_BENEFIT,SR_MALE_BENEFIT,SR_FEMALE_BENEFIT,SR_WOMEN_BENEFIT,SR_SENIOR_CITIZEN_BENEFIT" &
                                                 ") VALUES(" &
                                                "" & InParam.CenID & "," &
                                                "'" & InParam.ProjectID & "'," &
                                                "" & InParam.ProgType & "," &
                                                "N'" & InParam.Name & "'," &
                                                "N'" & InParam.Venue & "'," &
                                                " " & If(IsDate(InParam.FromDate), "'" & Convert.ToDateTime(InParam.FromDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " " & If(IsDate(InParam.ToDate), "'" & Convert.ToDateTime(InParam.ToDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                "'" & InParam.FromTime & "', " &
                                                "'" & InParam.ToTime & "', " &
                                                "'" & InParam.Period & "', " &
                                                "N'" & InParam.Brief & "', " &
                                                "N'" & InParam.SpecialMoments & "', " &
                                                "" & InParam.Beneficiaries & ", " &
                                                "N'" & InParam.Followup & "', " &
                                                "N'" & InParam.Speaker & "', " &
                                                "N'" & InParam.Feedback & "', " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.Rec_ID & "','" & InParam.ProgCategory & "',N'" & InParam.NewsLink & "',N'" & InParam.VVIP_Testimonial & "',N'" & InParam.Cultural & "','" & InParam.ReporterEmail & "','" & InParam.ReporterMobile & "',N'" & InParam.MediaLink & "','" & InParam.MasterProjID & "'," & InParam.Offline_Beneficiaries & "," & InParam.Online_Beneficiaries & "," & InParam.NoOfEvent & ",N'" & InParam.ProgramOccasion & "',N'" & InParam.ProgramOrganiser & "'," &
                                          "" & If(String.IsNullOrWhiteSpace(InParam.Institute_ID) = False, "N'" & InParam.Institute_ID & "'", " NULL ") & "," &
                                          "" & If(InParam.Child_Beneficiaries Is Nothing, "NULL", InParam.Child_Beneficiaries) & "," &
                                          "" & If(InParam.Youth_Beneficiaries Is Nothing, "NULL", InParam.Youth_Beneficiaries) & "," &
                                          "" & If(InParam.Male_Beneficiaries Is Nothing, "NULL", InParam.Male_Beneficiaries) & "," &
                                          "" & If(InParam.Female_Beneficiaries Is Nothing, "NULL", InParam.Female_Beneficiaries) & "," &
                                          "" & If(InParam.Women_Beneficiaries Is Nothing, "NULL", InParam.Women_Beneficiaries) & "," &
                                          "" & If(InParam.SeniorCitizen_Beneficiaries Is Nothing, "NULL", InParam.SeniorCitizen_Beneficiaries) & ")"

            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertAdditionalInfo(ByVal InAdditional As Parameter_InsertAdditionalInfo_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_ADDITIONAL_INFO(SRA_SR_REC_ID,SRA_ORGANIZED_FOR_AB_ID,SRA_PARTICIPANT_TYPE,SRA_PARTICIPANT_CATEGORY_ID,SRA_PROGRAM_TYPE_ID,SRA_PROGRAM_LOCATION_TYPE_ID,SRA_PROGRAM_COORDINATOR1_AB_ID,SRA_PROGRAM_COORDINATOR2_AB_ID,REC_ID,SRA_PARTICIPANT_SUB_CATEGORY_ID) VALUES(" &
                        "'" & InAdditional.Sr_ID & "'," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.OrganizedFor) = False, "'" & InAdditional.OrganizedFor & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ParticipantType) = False, "'" & InAdditional.ParticipantType & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ParticipantCategory) = False, "'" & InAdditional.ParticipantCategory & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ProgramType) = False, "'" & InAdditional.ProgramType & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ProgramLocationType) = False, "'" & InAdditional.ProgramLocationType & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ProgramCoordinator_1) = False, "'" & InAdditional.ProgramCoordinator_1 & "'", "NULL") & "," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ProgramCoordinator_2) = False, "'" & InAdditional.ProgramCoordinator_2 & "'", "NULL") & "," &
                        "'" & InAdditional.Rec_ID & "'," &
                        "" & If(String.IsNullOrWhiteSpace(InAdditional.ParticipantSubCategory) = False, "'" & InAdditional.ParticipantSubCategory & "'", "NULL") & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_ADDITIONAL_INFO, query, inBasicParam, InAdditional.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertActivityDetails(ByVal InActivity As Parameter_InsertAcivityDetail_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_ACTIVITY_INFO(SR_REC_ID,SR_ACTIVITY_NAME,SR_ACTIVITY_DETAIL_MISC_ID,SR_ACTIVITY_COUNT,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,SR_ACTIVITY_LATITUDE,SR_ACTIVITY_LONGITUDE) VALUES(" &
                        "'" & InActivity.Sr_ID & "'," &
                        "N'" & InActivity.ActivityName & "'," &
                        "'" & InActivity.ActivitySpecialDetail & "'," &
                        "" & InActivity.ActivityCount & "," &
                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InActivity.Rec_ID & "','" & InActivity.Latitude & "','" & InActivity.Longitude & "')"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_ACTIVITY_INFO, query, inBasicParam, InActivity.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertProgramType(ByVal InProg As Parameter_InsertProgramType_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_PROGRAM_TYPE_INFO(SR_REC_ID,SR_PROGRAM_TYPE_ID,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) VALUES(" &
                        "'" & InProg.Sr_ID & "'," &
                        "'" & InProg.ProgTypeID & "'," &
                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InProg.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_TYPE_INFO, query, inBasicParam, InProg.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertProgramOccasion(ByVal InProg As Parameter_InsertProgramOccasion_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_PROGRAM_OCCASION_INFO(SR_REC_ID,SR_PROGRAM_OCCASION_ID,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) VALUES(" &
                        "'" & InProg.Sr_ID & "'," &
                        "'" & InProg.ProgOccasionID & "'," &
                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InProg.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_OCCASION_INFO, query, inBasicParam, InProg.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertCollaborateCenters(ByVal InCollCen As Parameter_InsertCollaborateCenters_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_COLLABORATECENTERS_INFO(SR_REC_ID,SR_CEN_ID,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) VALUES(" &
                        "'" & InCollCen.Sr_ID & "'," &
                        "'" & InCollCen.CEN_ID & "'," &
                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InCollCen.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_COLLABORATECENTERS_INFO, query, inBasicParam, InCollCen.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertVenueType(ByVal InVenue As Parameter_InsertVenueType_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query = "INSERT INTO SERVICE_REPORT_VENUE_TYPE_INFO(SRVT_SR_REC_ID,SRVT_VENUE_ID,REC_ID) VALUES(" &
                        "'" & InVenue.Sr_ID & "'," &
                        "'" & InVenue.Venue_misc_id & "'," &
                        "'" & InVenue.Rec_ID & "')"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_VENUE_TYPE_INFO, query, inBasicParam, InVenue.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        ''' <summary>
        ''' Insert Wings
        ''' </summary>
        ''' <param name="InWings"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertWings</remarks>
        Public Shared Function InsertWings(ByVal InWings As Parameter_InsertWings_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO SERVICE_REPORT_WING_INFO(SR_CEN_ID,SR_REC_ID,SR_WING_ID," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                 ") VALUES(" &
                                                    "" & inBasicParam.openCenID.ToString & "," &
                                                    "'" & InWings.Sr_ID & "'," &
                                                    "'" & InWings.WingID & "'," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InWings.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_WING_INFO, OnlineQuery, inBasicParam, InWings.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Guest
        ''' </summary>
        ''' <param name="InGuest"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertGuest</remarks>
        Public Shared Function InsertGuest(ByVal InGuest As Parameter_InsertGuest_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO SERVICE_REPORT_GUEST_INFO(SR_CEN_ID,SR_REC_ID,SR_SR_NO,SR_GUEST_NAME,SR_GUEST_DESIG," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                 ") VALUES(" &
                                            "" & inBasicParam.openCenID.ToString & "," &
                                            "'" & InGuest.Sr_ID & "'," &
                                            InGuest.Sr_No & "," &
                                            "N'" & InGuest.GusetName & "'," &
                                            "N'" & InGuest.GuestDesignation & "'," &
                                        "" & Str & "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InGuest.Rec_ID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.SERVICE_REPORT_GUEST_INFO, OnlineQuery, inBasicParam, InGuest.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim updateReporter As String = ""
            If String.IsNullOrEmpty(UpParam.ProjectID) Then
                UpParam.ProjectID = "8f6b3279-166a-4cd9-8497-ca9fc6283b25"
            End If
            If UpParam.ReporterEmail.Length > 0 Then
                updateReporter = "REPORTER_EMAIL       ='" & UpParam.ReporterEmail & "',"
            End If
            If UpParam.ReporterMobile.Length>0  Then
                updateReporter += "REPORTER_MOBILE       ='" & UpParam.ReporterMobile & "',"
            End If
            Dim OnlineQuery As String = " UPDATE SERVICE_REPORT_INFO SET " &
                                        "SR_CEN_ID        =" & UpParam.CenID & ", " &
                                        "SR_PROJ_ID       ='" & UpParam.ProjectID & "', " &
                                        "SR_PROG_TYPE     =" & UpParam.ProgType & ", " &
                                        "SR_PROG_NAME     =N'" & UpParam.Name & "', " &
                                        "SR_PROG_VENUE           =N'" & UpParam.Venue & "', " &
                                        "SR_PROG_FR_DATE         =" & If(IsDate(UpParam.FromDate), "'" & Convert.ToDateTime(UpParam.FromDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                        "SR_PROG_TO_DATE         =" & If(IsDate(UpParam.ToDate), "'" & Convert.ToDateTime(UpParam.ToDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                        "SR_PROG_FR_TIME         ='" & UpParam.FromTime & "',  " &
                                        "SR_PROG_TO_TIME         ='" & UpParam.ToTime & "',  " &
                                        "SR_PROG_PD_TIME         ='" & UpParam.Period & "',  " &
                                        "SR_PROG_BRIEF           =N'" & UpParam.Brief & "',  " &
                                        "SR_PROG_SPL             =N'" & UpParam.SpecialMoments & "',  " &
                                        "SR_PROG_BENEFIT         =" & UpParam.Beneficiaries & ",  " &
                                        "SR_PROG_FOLLOWUP        =N'" & UpParam.Followup & "',  " &
                                        "SR_PROG_SPEAKER         =N'" & UpParam.Speaker & "',  " &
                                        "SR_PROG_FEEDBACK        =N'" & UpParam.Feedback & "',  " &
                                         " " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'," &
                                         "SR_PROG_CATEGORY       ='" & UpParam.ProgCategory & "'," &
                                         "SR_PROG_NEWS_LINK       =N'" & UpParam.NewsLink & "'," &
                                         "SR_PROG_CULTURAL       =N'" & UpParam.Cultural & "'," &
                                         "SR_PROG_VVIP_TESTIMONIAL       =N'" & UpParam.VVIP_Testimonial & "'," &
                                         updateReporter &
                                         "SR_MEDIA_LINK       =N'" & UpParam.MediaLink & "', " &
                                         "SR_MASTER_PROJ_ID       ='" & UpParam.MasterProjID & "', " &
                                         "SR_OFFLINE_PROG_BENEFIT       =" & UpParam.Offline_Beneficiaries & ", " &
                                         "SR_NO_OF_EVENT       =" & UpParam.NoOfEvent & ", " &
                                         "SR_ONLINE_PROG_BENEFIT       =" & UpParam.Online_Beneficiaries & ", " &
                                          "SR_PROG_OCCASION       =N'" & UpParam.ProgramOccasion & "'," &
                                          "SR_PROG_ORGANISER       =N'" & UpParam.ProgramOrganiser & "'," &
                                          "SR_INS_ID       =" & If(String.IsNullOrWhiteSpace(UpParam.Institute_ID) = False, "N'" & UpParam.Institute_ID & "'", " NULL ") & "," &
                                          "SR_CHILD_BENEFIT =" & If(UpParam.Child_Beneficiaries Is Nothing, "NULL", UpParam.Child_Beneficiaries) & "," &
                                          "SR_YOUTH_BENEFIT =" & If(UpParam.Youth_Beneficiaries Is Nothing, "NULL", UpParam.Youth_Beneficiaries) & "," &
                                          "SR_MALE_BENEFIT =" & If(UpParam.Male_Beneficiaries Is Nothing, "NULL", UpParam.Male_Beneficiaries) & "," &
                                          "SR_FEMALE_BENEFIT =" & If(UpParam.Female_Beneficiaries Is Nothing, "NULL", UpParam.Female_Beneficiaries) & "," &
                                          "SR_WOMEN_BENEFIT =" & If(UpParam.Women_Beneficiaries Is Nothing, "NULL", UpParam.Women_Beneficiaries) & "," &
                                          "SR_SENIOR_CITIZEN_BENEFIT =" & If(UpParam.SeniorCitizen_Beneficiaries Is Nothing, "NULL", UpParam.SeniorCitizen_Beneficiaries) &
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.SERVICE_REPORT_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function InsertServiceReport_Txn(inParam As Param_Txn_Insert_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_Insert_ServiceReport Is Nothing Then
                If Not Insert(inParam.param_Insert_ServiceReport, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertWings_ServiceReport In inParam.InsertWings
                If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertGuest1 Is Nothing Then
                If Not InsertGuest(inParam.param_InsertGuest1, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertGuest2 Is Nothing Then
                If Not InsertGuest(inParam.param_InsertGuest2, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertGuest3 Is Nothing Then
                If Not InsertGuest(inParam.param_InsertGuest3, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertGuest4 Is Nothing Then
                If Not InsertGuest(inParam.param_InsertGuest4, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertGuest5 Is Nothing Then
                If Not InsertGuest(inParam.param_InsertGuest5, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If inParam.InsertActivityDetails IsNot Nothing Then
                If inParam.InsertActivityDetails.Length > 0 Then
                    For Each Param As Parameter_InsertAcivityDetail_ServiceReport In inParam.InsertActivityDetails
                        If Not Param Is Nothing Then InsertActivityDetails(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If inParam.InsertProgType IsNot Nothing Then
                If inParam.InsertProgType.Length > 0 Then
                    For Each Param As Parameter_InsertProgramType_ServiceReport In inParam.InsertProgType
                        If Not Param Is Nothing Then InsertProgramType(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If inParam.InsertProgOccasion IsNot Nothing Then
                If inParam.InsertProgOccasion.Length > 0 Then
                    For Each Param As Parameter_InsertProgramOccasion_ServiceReport In inParam.InsertProgOccasion
                        If Not Param Is Nothing Then InsertProgramOccasion(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If inParam.InsertCollaborateCenters IsNot Nothing Then
                If inParam.InsertCollaborateCenters.Length > 0 Then
                    For Each Param As Parameter_InsertCollaborateCenters_ServiceReport In inParam.InsertCollaborateCenters
                        If Not Param Is Nothing Then InsertCollaborateCenters(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If inParam.param_InsertAdditonalInfo IsNot Nothing Then
                If String.IsNullOrWhiteSpace(inParam.param_InsertAdditonalInfo.Rec_ID) = False Then
                    InsertAdditionalInfo(inParam.param_InsertAdditonalInfo, inBasicParam, RequestTime)
                End If
            End If
            If inParam.InsertVenueType IsNot Nothing Then
                If inParam.InsertVenueType.Length > 0 Then
                    For Each Param As Parameter_InsertVenueType_ServiceReport In inParam.InsertVenueType
                        If Not Param Is Nothing Then InsertVenueType(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            '  End Using
            '  txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function UpdateServiceReport_Txn(UpParam As Param_Txn_Update_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.SERVICE_REPORT_INFO, UpParam.param_Update_ServiceReport.Rec_ID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not UpParam.param_Update_ServiceReport Is Nothing Then
                If Not Update(UpParam.param_Update_ServiceReport, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.RecID_DeleteWing Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_WING_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteWing & "'", inBasicParam)
            End If
            For Each inWings_Update As Parameter_InsertWings_ServiceReport In UpParam.InsertWings
                If Not inWings_Update Is Nothing Then InsertWings(inWings_Update, inBasicParam, RequestTime)
            Next
            If Not UpParam.RecID_DeleteGuest Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_GUEST_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteGuest & "'", inBasicParam)
            End If
            If Not UpParam.param_InsertGuest1 Is Nothing Then
                If Not InsertGuest(UpParam.param_InsertGuest1, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.param_InsertGuest2 Is Nothing Then
                If Not InsertGuest(UpParam.param_InsertGuest2, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.param_InsertGuest3 Is Nothing Then
                If Not InsertGuest(UpParam.param_InsertGuest3, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.param_InsertGuest4 Is Nothing Then
                If Not InsertGuest(UpParam.param_InsertGuest4, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.param_InsertGuest5 Is Nothing Then
                If Not InsertGuest(UpParam.param_InsertGuest5, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.RecID_DeleteAcitvity Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_ACTIVITY_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteAcitvity & "'", inBasicParam)
            End If
            If UpParam.InsertActivityDetails IsNot Nothing Then
                If UpParam.InsertActivityDetails.Length > 0 Then
                    For Each Param As Parameter_InsertAcivityDetail_ServiceReport In UpParam.InsertActivityDetails
                        If Not Param Is Nothing Then InsertActivityDetails(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If Not UpParam.RecID_DeleteProgType Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_TYPE_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteProgType & "'", inBasicParam)
            End If
            If UpParam.InsertProgType IsNot Nothing Then
                If UpParam.InsertProgType.Length > 0 Then
                    For Each Param As Parameter_InsertProgramType_ServiceReport In UpParam.InsertProgType
                        If Not Param Is Nothing Then InsertProgramType(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If Not UpParam.RecID_DeleteProgOccasion Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_OCCASION_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteProgOccasion & "'", inBasicParam)
            End If
            If UpParam.InsertProgOccasion IsNot Nothing Then
                If UpParam.InsertProgOccasion.Length > 0 Then
                    For Each Param As Parameter_InsertProgramOccasion_ServiceReport In UpParam.InsertProgOccasion
                        If Not Param Is Nothing Then InsertProgramOccasion(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If Not UpParam.RecID_DeleteCollaborateCenters Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_COLLABORATECENTERS_INFO, "SR_REC_ID    ='" & UpParam.RecID_DeleteCollaborateCenters & "'", inBasicParam)
            End If
            If UpParam.InsertCollaborateCenters IsNot Nothing Then
                If UpParam.InsertCollaborateCenters.Length > 0 Then
                    For Each Param As Parameter_InsertCollaborateCenters_ServiceReport In UpParam.InsertCollaborateCenters
                        If Not Param Is Nothing Then InsertCollaborateCenters(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            If Not UpParam.RecID_DeleteAdditonalInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_ADDITIONAL_INFO, "SRA_SR_REC_ID  ='" & UpParam.RecID_DeleteAdditonalInfo & "'", inBasicParam)
            End If
            If UpParam.param_InsertAdditonalInfo IsNot Nothing Then
                If String.IsNullOrWhiteSpace(UpParam.param_InsertAdditonalInfo.Rec_ID) = False Then
                    InsertAdditionalInfo(UpParam.param_InsertAdditonalInfo, inBasicParam, RequestTime)
                End If
            End If
            If Not UpParam.RecID_DeleteVenueType Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_VENUE_TYPE_INFO, "SRVT_SR_REC_ID  ='" & UpParam.RecID_DeleteVenueType & "'", inBasicParam)
            End If
            If UpParam.InsertVenueType IsNot Nothing Then
                If UpParam.InsertVenueType.Length > 0 Then
                    For Each Param As Parameter_InsertVenueType_ServiceReport In UpParam.InsertVenueType
                        If Not Param Is Nothing Then InsertVenueType(Param, inBasicParam, RequestTime)
                    Next
                End If
            End If
            ' End Using
            '  txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function DeleteServiceReport_Txn(DelParam As Param_Txn_Delete_ServiceReport, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.RecID_DeleteProgType Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_TYPE_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteProgType & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteProgOccasion Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_PROGRAM_OCCASION_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteProgOccasion & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteCollaborateCenters Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_COLLABORATECENTERS_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteCollaborateCenters & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteAcitvity Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_ACTIVITY_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteAcitvity & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteWing Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_WING_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteWing & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteGuest Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_GUEST_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteGuest & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteAttachments Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_ATTACHMENT_INFO, "SR_REC_ID    ='" & DelParam.RecID_DeleteAttachments & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteAdditonalInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_ADDITIONAL_INFO, "SRA_SR_REC_ID    ='" & DelParam.RecID_DeleteAdditonalInfo & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteVenueType Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_REPORT_VENUE_TYPE_INFO, "SRVT_SR_REC_ID    ='" & DelParam.RecID_DeleteVenueType & "'", inBasicParam)
            End If
            If Not DelParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.SERVICE_REPORT_INFO, DelParam.RecID_Delete, inBasicParam)
            End If
            '  End Using
            ' txn.Complete()
            '  End Using
            Return True
        End Function
    End Class
#End Region
End Namespace
