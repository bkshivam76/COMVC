Imports System.Configuration
Imports System.Net
Imports Common_Lib.RealTimeService
Imports System.Net.Mail
Imports Newtonsoft.Json
Partial Public Class DbOperations
    <Serializable>
    Public Class Android_Notifications
        Inherits SharedVariables
        <Serializable>
        Public Class Datum
            Public Property id As String
            Public Property mobile As String
            Public Property status As String
        End Class
        <Serializable>
        Public Class Root
            Public Property status As String
            Public Property data As List(Of Datum)
            Public Property msgid As String
            Public Property message As String
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function SendOTP_BySMS(ByVal OTP_SMS As String, ByVal RegisteredMobileNo As String) As Boolean
            Dim SMSDeliveryStatus As Boolean = False
            Dim SmsSend As String = ""
            Dim OTPMessage As String = OTP_SMS
            Dim SenderID As String = "AcctBK"

            If ConfigurationManager.AppSettings("SendSms") <> "" Then
                SmsSend = ConfigurationManager.AppSettings("SendSms")
            End If

            If SmsSend.ToUpper() = "TRUE" OrElse SmsSend.ToUpper() = "TEST" Then

                Try

                    If SmsSend.ToUpper() = "TEST" Then
                        RegisteredMobileNo = ConfigurationManager.AppSettings("TestMobile")
                    End If

                    Dim result As String = ""

                    Try
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                        Dim str As String = New WebClient().DownloadString("https://msg.mtalkz.com/V2/http-api.php?apikey=MeZKtmUg004zsepU&senderid=" & SenderID & "&format=json&number=" & RegisteredMobileNo & "&message=" & OTPMessage)
                        Dim SMS_Response = JsonConvert.DeserializeObject(Of Root)(str)

                        If SMS_Response.message.ToLower().Contains("message submitted successfully") Then
                            result = "Message has sent successfully"
                            SMSDeliveryStatus = True
                        Else
                            result = "Failed to send message"
                        End If

                    Catch ex As Exception
                        Throw ex
                    End Try

                Catch ex As Exception
                    Throw ex
                End Try
            End If

            Return SMSDeliveryStatus
        End Function
        Public Function insertUsersDeviceToken(ByVal deviceToken As String, ByVal username As String, ByVal androidID As String, Optional ByVal moduleName As String = "CONNECTONE") As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_usersDeviceToken"
            Dim params() As String = {"@DEVICE_TOKEN", "@USERNAME", "@MODULE_NAME", "@ANDROID_ID"}
            Dim values() As Object = {deviceToken, username, moduleName, androidID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {8000, 255, 255, 16}
            'used public insert function as there are no transactional data involved 
            Return _RealService.InsertBySPPublic(Tables.ANDROID_DEVICE_TOKEN, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function insertAndroidNotificationLog(ByVal notificationID As String, ByVal userRecID As String, ByVal androidID As String, ByVal deviceToken As String, ByVal status As String, ByVal remarks As String, ByVal sentTime As String, ByVal recAddBy As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_android_notification_log"
            Dim params() As String = {"@NL_NOTIFICATION_ID", "@NL_USER_REC_ID", "@NL_ANDROID_ID", "@NL_DEVICE_TOKEN", "@NL_STATUS", "@NL_REMARKS", "@NL_SENT_TIME", "@REC_ADD_BY"}
            Dim values() As Object = {notificationID, userRecID, androidID, deviceToken, status, remarks, sentTime, recAddBy}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {-1, -1, -1, -1, -1, -1, -1, -1}
            Return _RealService.InsertBySPPublic(Tables.ANDROID_NOTIFICATION_LOG, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Function refreshDeviceToken(ByVal androidID As String, ByVal deviceToken As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_deviceToken"
            Dim params() As String = {"@DEVICE_TOKEN", "@ANDROID_ID"}
            Dim values() As Object = {deviceToken, androidID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {8000, 16}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.ANDROID_DEVICE_TOKEN, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        'Public Function Get_sparcSubjectsAndProjects(ByVal androidID As String) As DataSet
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim paramters As String() = {"@ANDROID_ID"}
        '    Dim values() As Object = {androidID}
        '    Dim dbTypes() As System.Data.DbType = {DbType.String}
        '    Dim lengths() As Integer = {16}
        '    Return _RealService.ListDatasetFromSP(Tables.ANDROID_DEVICE_TOKEN, "[sp_get_sparcSubjectsAndProjects]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        'End Function

        Public Function get_registeredUsersForModule(ByVal androidID As String, ByVal moduleName As String, Optional CEN_ID As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@ANDROID_ID", "@MODULE_NAME", "@CEN_ID"}
            Dim values() As Object = {androidID, moduleName, CEN_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {16, 255, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_INFO, "[sp_get_registeredUsersForMobileAppModules]", Tables.SERVICE_USERS_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function

        Public Function get_projectsMappedToSubject(ByVal User_ID As String, Optional CEN_ID As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@USER_ID", "@CEN_ID"}
            Dim values() As Object = {User_ID, CEN_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return _RealService.ListFromSP(Tables.SERVICE_USERS_INFO, "[sp_get_projectsMappedToSubject]", Tables.SERVICE_USERS_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceProject))
        End Function
        Public Function update_AndroidDefaultUser(ByVal registrationRecID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_AndroidDefaultUser"
            Dim params() As String = {"@REC_ID_OF_REGISTRATION_TBL"}
            Dim values() As Object = {registrationRecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.ANDROID_DEVICE_TOKEN, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function unregisterAndroidUser(ByVal registrationRecID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_unregisterAndroidUser"
            Dim params() As String = {"@REC_ID_OF_REGISTRATION_TBL"}
            Dim values() As Object = {registrationRecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.ANDROID_DEVICE_TOKEN, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function UpdateAndroidTheme(Theme As String, AndroidID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_Update_Android_Theme"
            Dim params() As String = {"@Theme", "@AndroidID"}
            Dim values() As Object = {Theme, AndroidID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 16}
            Return _RealService.UpdateBySPPublic(Tables.ANDROID_DEVICE_TOKEN, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function GetAndroidTheme(AndroidID As String) As DataTable
            Dim query As String = "Select ANDROID_THEME FROM USERS_ANDROID_DEVICE_TOKEN Where ANDROID_ID='" & AndroidID & "' and ANDROID_THEME is not null"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Return _RealService.List(Tables.USERS_ANDROID_DEVICE_TOKEN, query, Tables.USERS_ANDROID_DEVICE_TOKEN.ToString, inbasicparam)
        End Function
        Public Function GetCentres(Optional Onlymain As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_createForm)
            Dim query As String = "select cen_id,cen_uid,cen_name,ii.ins_name,ii.ins_short from centre_info ci inner join institute_info ii on ci.cen_ins_id=ii.ins_id where ci.rec_status in(0,1,2)"
            If Onlymain = True Then
                query = query & " and cen_main=1"
            End If
            Return _RealService.List(Tables.CENTRE_INFO, query, Tables.CENTRE_INFO.ToString, inbasicparam)
        End Function
    End Class
End Class

