'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class SM_GodlyServices
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function Get_Feedback_Query_EventsList(centerid As Int32?, attachmentpath As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_FeedbackQueryEventsList"
            Dim paramters As String() = {"@centerid", "@attachment_root_path"}
            Dim values() As Object = {centerid, attachmentpath}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {8, 5000}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_REPORT_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function Get_feed_Query_Contributions(eventrecid As String, attachmentpath As String, ByVal referenceName As String,
                                                       ByVal referenceRecid As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_FeedQueryContributionsList"
            Dim paramters As String() = {"@EVENT_ID", "@attachmentpath", "@reference", "@referenceRecid"}
            Dim values() As Object = {eventrecid, attachmentpath, referenceName, referenceRecid}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {36, 255, 30, 36}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_USERS_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function Get_All_Responses_FeedQuery(eventrecid As String, userid As Int32?, feed_qry_recid As String, username As String, isanonymous As Boolean, readby As String,
                                                    attachmentpath As String, userAb_ID As String, ByVal referenceName As String, ByVal referenceRecid As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            userAb_ID = If(userAb_ID Is Nothing, "", userAb_ID)
            Dim SPName As String = "sp_get_sm_AllFeedsQuerysResps"
            Dim paramters As String() = {"@eventrecid", "@userid", "@feed_query_recid", "@username", "@isanonymous", "@readby", "@attachmentpath", "@User_AB_ID",
                "@referenceName", "@referenceRecid"}
            Dim values() As Object = {eventrecid, userid, feed_qry_recid, username, isanonymous, readby, attachmentpath, userAb_ID, referenceName, referenceRecid}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.Boolean, DbType.String, DbType.String, DbType.String,
                DbType.String, DbType.String}
            Dim lengths() As Integer = {36, 8, 36, 60, 6, 50, 255, 36, 30, 36}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_FEEDBACK_QUERY_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function Get_ResponseEmail_FeedbackQuery(eventrecid As String, fqr_ref_recid As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_ResponseEmail_FeedbackQuery"
            Dim paramters As String() = {"@eventrecid", "@fqr_ref_recid"}
            Dim values() As Object = {eventrecid, fqr_ref_recid}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
            Dim lengths() As Integer = {36, 36}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_REPORT_INFO, SPName, paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function Get_SM_EventsList(Start_Date As DateTime?, End_Date As DateTime?, CenterID As Int32?, speaker As String, ProjectID As String,
                                          ratingFrom As Int32?, ratingTo As Int32?, cityid As String, stateid As String, attachmentRootPath As String, WingShortMS As String,
                                          TopicName As String, viewmode As String, searchmode As String, searchtext As String, tabchange As String, insid As String,
                                          webview As Boolean, needimages As Boolean, needvideos As Boolean) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_sm_EventsList"
            If Start_Date = "#1/1/0001 12:00:00 AM#" Then
                Start_Date = Nothing
            End If
            If End_Date = "#1/1/0001 12:00:00 AM#" Then
                End_Date = Nothing
            End If
            Dim paramters As String() = {"@startdate", "@enddate", "@CenterID", "@speaker", "@ProjectID", "@ratingfrom", "@ratingto", "@attachment_root_path",
                                        "@WingShortMS", "@cityid", "@stateid", "@topic", "@viewmode", "@searchmode", "@searchtext", "@tabchange", "@insid", "@webview", "@needimages", "@needvideos"}
            Dim values() As Object = {Start_Date, End_Date, CenterID, speaker, ProjectID, ratingFrom, ratingTo,
                attachmentRootPath, WingShortMS, cityid, stateid, TopicName, viewmode, searchmode, searchtext, tabchange, insid, webview, needimages, needvideos}
            Dim dbTypes() As System.Data.DbType = {DbType.Date, DbType.Date, DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Int32,
                                    DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String,
                                    DbType.String, DbType.Boolean, DbType.Boolean, DbType.Boolean}
            Dim lengths() As Integer = {12, 12, 6, 255, 255, 2, 2, 255, 36, 50, 50, 50, 50, 20, 100, 20, 8, 2, 2, 2}
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


        Public Function InsertFQRespopnse(ByVal feed_query_RecId As String, ByVal responseText As String,
                                                  ByVal addon As DateTime?, ByVal userid As String, ByVal isanonymous As Boolean, ByVal fqr_recid As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@feed_query_recid", "@responsetext", "@addon", "@userid", "@isanonymous", "@fqr_recid"}
            Dim values() As Object = {feed_query_RecId, responseText, Convert.ToDateTime(addon), userid, isanonymous, fqr_recid}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.DateTime, DbType.String, DbType.Boolean, DbType.String}
            Dim lengths() As Integer = {36, 8000, 30, 30, 5, 36}
            Return InsertBySPPublic(Tables.SERVICE_FEEDBACK_QUERY_INFO, "[sp_Insert_FQ_Response]", paramters, values, dbTypes, lengths, ClientScreen.Facility_ServiceReport, _RealService)
        End Function

        Public Function GetAttachments_FeedQuery(ByVal link As String, ByVal fqr_recid As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim onlineQuery As String = "SELECT AI.REC_ID," &
                        "CASE " &
                            "WHEN " &
                                " NULLIF(ari.ALI_ATTACHMENT_ID,'') IS NOT NULL   and charindex('.', reverse(AI.[AI_FILE_NAME])) >0 " &
                            "THEN " &
                                "CONCAT('" & link & "',ari.ALI_ATTACHMENT_ID,'.', Right(AI.[AI_FILE_NAME], charindex('.', reverse(AI.[AI_FILE_NAME])) - 1)) " &
                            "ELSE ''" &
                        "END " &
                        "AS 'File_Name', AI.AI_DESCRIPTION,	ari.ALI_ATTACHMENT_ID, ari.ALI_REF_REC_ID" &
                " FROM attachment_reference_info ari " &
                " INNER JOIN Attachment_info AI ON 	ari.ALI_ATTACHMENT_ID = ai.REC_ID" &
            " WHERE ari.ALI_REF_REC_ID = '" & fqr_recid & "'"
            Return _RealService.List(Tables.ATTACHMENT_REFERENCE_INFO, onlineQuery, Tables.ATTACHMENT_REFERENCE_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function GetAttachmentDocID() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "select REC_ID from MISC_INFO where MISC_NAME = 'Other Misc Document' and MISC_REMARK1 = 'GENERAL' and MISC_ID = 'Attachment Category'"
            Return _RealService.List(Tables.MISC_INFO, Query, Tables.MISC_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        Public Function UpdateShowinPublic(fqrrecid As String, trueorfalse As Boolean) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim torf As Int16
            If trueorfalse = True Then
                torf = 1
            Else
                torf = 0
            End If

            Dim Query As String = "Update SERVICE_FEEDBACK_QUERY_REF " &
                                   "Set FQR_SHOWINPUBLIC= " & torf & " Where FQR_RECID='" & fqrrecid & "'"
            _RealService.GetScalar(Tables.SERVICE_FEEDBACK_QUERY_REF, Query, Tables.SERVICE_FEEDBACK_QUERY_REF.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
            Return True
        End Function
    End Class
#End Region
End Class
