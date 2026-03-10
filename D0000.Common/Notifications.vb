Imports System.Configuration
Imports System.Net
Imports Common_Lib.RealTimeService
Imports System.Net.Mail
Imports Newtonsoft.Json
Imports System.IO
Imports System.Text

Partial Public Class DbOperations
    <Serializable>
    Public Class Notifications
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
        <Serializable>
        Public Class Root_SpringEdge
            Public Property groupID As String
            Public Property MessageIDs As String
            Public Property status As String
        End Class
        <Serializable>
        Public Class Root_EmailVerifier
            Public Property code As String
            Public Property message As String
            Public Property data As Data_EmailVerifier
        End Class
        <Serializable>
        Public Class Data_EmailVerifier
            Public Property status As String
            Public Property cost As String
            Public Property msg As String

        End Class
        <Serializable>
        Public Class Return_EmailValidationStatus
            Public Property status As String = Nothing
            Public Property remarks As String = Nothing
            Public Property verificationCount As Integer = 0
            Public Property result As Boolean = False
            Public Property userMessage As String = Nothing
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function SendSMS(ByVal EventID? As Int32, ByVal InfoType As String, ByRef ResultString As String, Optional RecID As String = Nothing, Optional CenID As String = Nothing, Optional MobNo As String = Nothing, Optional AB_ID As String = Nothing, Optional Day_Time As String = Nothing) As Boolean
            Dim dtSms As DataTable = GetSmsDetails(InfoType, EventID, RecID, CenID, MobNo, AB_ID, Day_Time)

            If dtSms.Rows.Count = 0 Then
                ResultString += (cBase._open_UID_No & ": SMS Details are not Available!") & "<br>"
                Return False
            End If
            Dim result As Boolean = False

            For i As Integer = 0 To dtSms.Rows.Count - 1
                Dim smsBody As String = dtSms.Rows(i)("Message").ToString()
                Dim SenderID As String = dtSms.Rows(i)("SenderID").ToString()
                Dim MobString As String = dtSms.Rows(i)("MobileNo").ToString()
                Dim Receiver As String = dtSms.Rows(i)("Receiver").ToString()
                Dim SMSresult As Boolean = False

                For Each st As String In MobString.Split(","c)

                    If st.TrimStart("0"c).Length = 10 Then
                        SMSresult = SendSms(st.TrimStart("0"c), smsBody, SenderID)
                    End If
                Next
                If result = False Then result = SMSresult
                If SMSresult Then
                    ResultString += IIf(cBase._open_UID_No Is Nothing, "", cBase._open_UID_No & ":") & "SMS sent successfully to " & Receiver & "!" & "<br>"
                    Continue For
                End If

                If Not SMSresult Then
                    ResultString += IIf(cBase._open_UID_No Is Nothing, "", cBase._open_UID_No & ":") & "SMS not Sent to " & Receiver & "." & "<br>"
                    Continue For
                End If
            Next
            Return result
        End Function
        Public Function NotifyDonationGeneratebyWhatsapp(ByVal DonationID As String, ByVal Optional SenderPhone As String = Nothing)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT COALESCE(AB.C_MOB_NO_1,'') MOB, TR_AMOUNT AMT, TR_DATE DT, DBO.CamelCase(INS_NAME) INS, COALESCE(CS_AB.C_MOB_NO_1,'+91 94140 52546') AS DONATION_MOBILE FROM transaction_info TI  INNER JOIN address_book AS AB ON TR_AB_ID_1 = AB.REC_ID INNER JOIN CENTRE_INFO AS CI ON TR_CEN_ID = CEN_ID INNER JOIN institute_info AS INS ON CEN_INS_ID = INS_ID LEFT JOIN centre_support_info CS ON TI.TR_CEN_ID = CS.CEN_ID LEFT JOIN address_book AS CS_AB ON COALESCE(CEN_DONATION_RES_PERSON_AB_ID,CEN_ACC_RES_PERSON_AB_ID) = CS_AB.REC_ID WHERE TI.REC_ID = '" & DonationID & "'"
            Dim _table As DataTable = _RealService.List(Tables.TRANSACTION_INFO, Query, Tables.TRANSACTION_INFO.ToString, GetBaseParams(ClientScreen.Options_FormResponse))
            Dim mob As String = _table.Rows(0)("MOB").ToString()
            If mob.Length > 9 Then
                Dim Message As String = "Thanks for Donating Rs. " + _table.Rows(0)("AMT").ToString() + " in " + _table.Rows(0)("INS").ToString() + ".%0aKindly download your donation Acknowledgement from https://omshanti.in/Acknowledgement . For any queries please contact Centre/Person you gave donation to or call " + _table.Rows(0)("DONATION_MOBILE").ToString() + ". %0aRegards, %0aManagement %0a" + _table.Rows(0)("INS").ToString() + "%0a%0aIf you are not able to click on above link then please save this number in your contacts as `Madhuban Accounts Office` or Reply back, and then try."
                InsertWhatsappQueue(mob, Message, "DonationAcknowledgement", Nothing, Nothing, SenderPhone)
            End If
        End Function

        Public Sub InsertWhatsappQueue(ByVal Phone As String, ByVal Message As String, Optional ByVal BatchName As String = Nothing, Optional ByVal ResponseID As String = Nothing, Optional ByVal MediaFileURL As String = Nothing, ByVal Optional SenderPhone As String = Nothing, ByVal Optional Priority As Int32? = Nothing)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@PHONE", "@MESSAGE", "@RESPONSE_ID", "@BATCH_NAME", "@CEN_ID", "@USER_ID", "@MEDIA_FILE_URL", "@SENDER_PHONE", "@SENDING_PRIORITY"}
            Dim values As Object() = {Phone, Message, ResponseID, BatchName, cBase._open_Cen_ID, cBase._open_User_ID, MediaFileURL, SenderPhone, Priority}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32}
            Dim lengths As Integer() = {15, -1, 36, -1, 4, 255, 8000, 10, 4}
            _RealService.InsertBySPPublic(Tables.WHATSAPP_MESSAGE_QUEUE, "[sp_Insert_WhatsApp_Queue]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))
        End Sub

        Public Function SendSms(ByVal MobNo As String, ByVal Sms As String, ByVal SenderID As String) As Boolean 'Function That Sends SMS 
            Dim retBoolean As Boolean = False
            Dim SmsSend As String = ""
            If ConfigurationManager.AppSettings("SendSms") <> "" Then
                SmsSend = ConfigurationManager.AppSettings("SendSms")
            End If

            If SmsSend.ToUpper() = "TRUE" OrElse SmsSend.ToUpper() = "TEST" Then

                Try

                    If SmsSend.ToUpper() = "TEST" Then
                        MobNo = ConfigurationManager.AppSettings("TestMobile")
                    End If

                    Dim result As String = ""

                    Try
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                        Dim str As String
                        Dim Provider As String = ConfigurationManager.AppSettings("SMSProvider").ToLower()
                        Dim Status As String = ""
                        Select Case Provider
                            Case "mtalkz"
                                str = New WebClient().DownloadString("https://msg.mtalkz.com/V2/http-api.php?apikey=MeZKtmUg004zsepU&senderid=" & SenderID & "&format=json&number=" & MobNo & "&message=" & Sms)
                                Dim SMS_Response = JsonConvert.DeserializeObject(Of Root)(str)
                                Status = If(SMS_Response.message = Nothing, "", SMS_Response.message)
                            Case "springedge"
                                Dim UpdatedSms = Sms.Replace("\\n", " \n")
                                str = New WebClient().DownloadString("https://instantalerts.co/api/web/send?apikey=102623u417hx4754vg02w95j60r9yf81p197&sender=" & SenderID & "&format=json&to=" & MobNo & "&message=" & UpdatedSms)
                                Dim SMS_Response = JsonConvert.DeserializeObject(Of Root_SpringEdge)(str)
                                Status = If(SMS_Response.status = Nothing, "", SMS_Response.status)
                            Case Else
                                str = ""
                                Status = "invalid service"
                        End Select

                        If Status.ToLower().Contains("message submitted successfully") Or Status.ToUpper().Contains("AWAITED-DLR") Then
                            result = "Message has sent successfully"
                            retBoolean = True
                        ElseIf (Status.ToLower().Contains("invalid service")) Then
                            result = "Invalid Service"
                            retBoolean = False
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

            Return retBoolean
        End Function
        Public Sub SendEmail(ByVal EventID As Int32, ByVal [Event] As String, ByRef ResultString As String)
            Dim dtEmailSmsData As DataTable = GetEmailSmsData()

            If dtEmailSmsData.Rows.Count = 0 Then
                ResultString += (cBase._open_UID_No & ":Email not available for Center!") & "<br>"
                Return
            End If

            Dim EmailString As String = dtEmailSmsData.Rows(0)(1).ToString()
            Dim dtEmail As DataTable = GetEmailBody([Event])
            Dim body As String = dtEmail.Rows(0)(1).ToString()
            Dim subject As String = dtEmail.Rows(0)(0).ToString()
            Dim Emailresult As Boolean = False

            For Each st As String In EmailString.Split(","c)
                Emailresult = SendEmail(st, subject, body)
            Next

            If Emailresult Then
                ResultString += (cBase._open_UID_No & ":Email sent successfully!") & "<br>"
                Return
            End If

            If Not Emailresult Then
                ResultString += (cBase._open_UID_No & ":Email not Sent.") & "<br>"
                Return
            End If
        End Sub
        Public Function SendEmail(ByVal ToEmailId As String, ByVal subject As String, ByVal body As String, Optional ByVal EmailerName As String = "", Optional ByVal CcEmailId As String = "", Optional ByVal BccEmailId As String = "", Optional ByVal ReplyToEmailId As String = "", Optional ByVal senderIDtoBeUsed As String = "", Optional ByVal attachment_filename As String = "", Optional FromEmail As String = "", Optional FromPwd As String = "", Optional AdminEmail As String = "") As Boolean

            Dim SendEmails As String = ""
            Dim emailSentResult As String = ""
            Dim senderID As String
            Dim senderPassword As String
            Dim DataAdminEmailID As String

            If senderIDtoBeUsed = "KarunaSankalp" Then
                senderID = "karunasankalp@brahmakumaris.com"
                senderPassword = "Karunadaya"
                DataAdminEmailID = "karunasankalp@brahmakumaris.com"
            Else
                If String.IsNullOrWhiteSpace(FromEmail) = False Then
                    senderID = FromEmail
                    senderPassword = FromPwd
                Else
                    senderID = ConfigurationManager.AppSettings("SenderId")
                    senderPassword = ConfigurationManager.AppSettings("senderPassword")
                End If
                If String.IsNullOrWhiteSpace(AdminEmail) = False Then
                    DataAdminEmailID = AdminEmail
                Else
                    DataAdminEmailID = ConfigurationManager.AppSettings("DataAdminEmailID")
                End If
            End If

            If ConfigurationManager.AppSettings("SendEmail") <> "" Then
                SendEmails = ConfigurationManager.AppSettings("SendEmail")
            End If

            If SendEmails.ToUpper() = "TRUE" OrElse SendEmails.ToUpper() = "TEST" Then

                Try

                    If SendEmails.ToUpper() = "TEST" Then
                        ToEmailId = ConfigurationManager.AppSettings("TestEmail")
                        If CcEmailId.Length > 0 Then CcEmailId = ConfigurationManager.AppSettings("TestEmail_CC")
                        If BccEmailId.Length > 0 Then BccEmailId = ConfigurationManager.AppSettings("TestEmail_BCC")
                        If ReplyToEmailId.Length > 0 Then ReplyToEmailId = ConfigurationManager.AppSettings("TestEmail_ReplyTo")
                    End If

                    Dim smtp As SmtpClient = New SmtpClient With {
                .Host = "smtp.gmail.com",
                .Port = 587,
                .EnableSsl = True,
                .DeliveryMethod = SmtpDeliveryMethod.Network,
                .Credentials = New System.Net.NetworkCredential(senderID, senderPassword),
                .Timeout = 30000
            }
                    Dim message As MailMessage = New MailMessage(senderID, ToEmailId, subject, body)
                    message.IsBodyHtml = True
                    'message.ReplyToList.Add(DataAdminEmailID)
                    If ReplyToEmailId.Length > 0 Then
                        message.ReplyToList.Add(ReplyToEmailId)
                    Else
                        message.ReplyToList.Add(DataAdminEmailID)
                    End If
                    'If (attachment_filename <> "" And attachment_filename <> vbNull) Then
                    '    Dim attachment As System.Net.Mail.Attachment
                    '    attachment = New System.Net.Mail.Attachment(attachment_filename)
                    '    message.Attachments.Add(attachment)
                    'End If
                    If CcEmailId.Length > 0 Then message.CC.Add(CcEmailId)
                    If BccEmailId.Length > 0 Then message.Bcc.Add(BccEmailId)

                    smtp.Send(message)
                Catch ex As Exception
                    emailSentResult = "Please contact administrator.Error sending email  to " & ToEmailId & ".!!!" & ex.Message
                    If EmailerName.Length > 0 Then InsertEmailLog(EmailerName, "FAIL", emailSentResult, ToEmailId, CcEmailId, BccEmailId, ReplyToEmailId, senderID)
                    Return False
                End Try
                If EmailerName.Length > 0 Then InsertEmailLog(EmailerName, "SUCCESS", emailSentResult, ToEmailId, CcEmailId, BccEmailId, ReplyToEmailId, senderID)

                Return True
            Else
                Return False
            End If
        End Function
        Public Function GetSmsDetails(ByVal AuditStatus As String, ByVal EventID? As Int32, Optional RecID As String = Nothing, Optional CenID As String = Nothing, Optional MobNo As String = Nothing, Optional AB_ID As String = Nothing, Optional Day_Time As String = Nothing) As DataTable
            Dim centerid As String = cBase._open_Cen_ID
            If String.IsNullOrWhiteSpace(CenID) = False Then
                centerid = CenID
            End If
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@REMINDER_TYPE", "@EVENT_ID", "@RECID", "@MOBILENO", "@AB_ID", "@DAY_TIME"}
            Dim values As Object() = {centerid, AuditStatus, EventID, RecID, MobNo, AB_ID, Day_Time}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.DateTime2}
            Dim lengths As Integer() = {8, 100, 8, 36, 10, 36, 7}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_rpt_SMS_Dynamic_Reminders]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function GetEmailSmsData() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@C_COD_YEAR_ID"}
            Dim values As Object() = {cBase._open_Cen_ID, cBase._open_UID_No}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths As Integer() = {8, 8}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_get_Email_Mob_Info]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function GetEmailBody(ByVal ReminderType As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@REMINDER_TYPE"}
            Dim values As Object() = {cBase._open_Cen_ID, ReminderType}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths As Integer() = {8, 100}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_rpt_email_Dynamic_Reminders]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Sub InsertEmailLog(ByVal EMAILER_NAME As String, ByVal STATUS As String, ByVal DETAILS As String, ByVal TO_ADDRESS As String, ByVal CC As String, ByVal BCC As String, ByVal REPLYTO As String, ByVal SENDERID As String)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@EMAILER_NAME", "@STATUS", "@DETAILS", "@TO", "@CC", "@BCC", "@REPLYTO", "@SENDERID"}
            Dim values As Object() = {EMAILER_NAME, STATUS, DETAILS, TO_ADDRESS, CC, BCC, REPLYTO, SENDERID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths As Integer() = {-1, 255, 8000, 8000, 8000, 8000, 8000, 8000}
            _RealService.InsertBySPPublic(Tables.EMAIL_SCHEDULER_LOG, "[sp_insert_emailLog]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_DonationRegister))
        End Sub
        Public Function getEmailValidationStatus_API(ByVal email_ID As String, Optional reverify As Boolean = False) As Return_EmailValidationStatus

            Dim emailValidationStatus As Return_EmailValidationStatus = New Return_EmailValidationStatus()

            Dim dt_emailStatus As DataTable = getEmailValidationStatus_DB(email_ID)

            If (dt_emailStatus IsNot Nothing AndAlso dt_emailStatus.Rows.Count > 0) Then
                emailValidationStatus.status = dt_emailStatus.Rows(0)("EVS_STATUS").ToString()
                emailValidationStatus.remarks = dt_emailStatus.Rows(0)("EVS_REMARKS").ToString()
                emailValidationStatus.verificationCount = Convert.ToInt32(dt_emailStatus.Rows(0)("EVS_VERIFICATION_COUNT"))
                If (emailValidationStatus.status = "DELIVERABLE") Then emailValidationStatus.result = True
                emailValidationStatus.userMessage = "The status of the email is " & emailValidationStatus.status & "."
            End If

            If reverify AndAlso emailValidationStatus.verificationCount >= 5 Then

                emailValidationStatus.userMessage = emailValidationStatus.userMessage & " It has been verified for 5 times and cannot be re-verified again."

                Return emailValidationStatus
            End If

            If String.IsNullOrWhiteSpace(emailValidationStatus.status) Or reverify Then
                Try
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                    Dim request As WebRequest = WebRequest.Create("https://insprl.com/api/email/validate")
                    request.Method = "POST"
                    request.Headers.Add("Authorization", "6h9a1*!sc[eZfJL0KbvE2Tyw")

                    Dim postData As String = "client_id=Csprl_WC5M40NLHQIRE6D&mode=everifier&email=" & email_ID
                    Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
                    request.ContentType = "application/x-www-form-urlencoded"
                    request.ContentLength = byteArray.Length
                    Dim dataStream As Stream = request.GetRequestStream()
                    dataStream.Write(byteArray, 0, byteArray.Length)
                    dataStream.Close()
                    Dim response As WebResponse = request.GetResponse()
                    dataStream = response.GetResponseStream()
                    Dim reader As StreamReader = New StreamReader(dataStream)
                    Dim responseFromServer As String = reader.ReadToEnd()
                    reader.Close()
                    dataStream.Close()
                    response.Close()


                    If String.IsNullOrWhiteSpace(responseFromServer) = False Then
                        Dim root = JsonConvert.DeserializeObject(Of Root_EmailVerifier)(responseFromServer)

                        If root.code = "100" AndAlso root.data IsNot Nothing Then

                            emailValidationStatus.status = root.data.status.ToUpper
                            emailValidationStatus.remarks = root.data.msg
                            If emailValidationStatus.status = "DELIVERABLE" Then emailValidationStatus.result = True

                            If reverify Then
                                updateEmailValidationStatus(email_ID, emailValidationStatus.status, emailValidationStatus.remarks)

                                emailValidationStatus.verificationCount = emailValidationStatus.verificationCount + 1
                                emailValidationStatus.userMessage = "The status of the email after re-verification is " & emailValidationStatus.status

                            Else
                                insertEmailValidationStatus(email_ID, emailValidationStatus.status, emailValidationStatus.remarks)

                                emailValidationStatus.verificationCount = 1
                                emailValidationStatus.userMessage = "The status of the email is " & emailValidationStatus.status
                            End If

                        Else
                            emailValidationStatus.userMessage = "Please contact ConnectOne team with this screenshot or Try Again Later. Message from API Server: " & root.message & ". Status Code from API Server: " & root.code
                            emailValidationStatus.result = False
                        End If

                    End If

                    Return emailValidationStatus

                Catch ex As Exception
                    Throw ex
                End Try

            End If

            Return emailValidationStatus

        End Function
        Public Function getEmailValidationStatus_DB(ByVal email_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim Query As String = "SELECT * FROM EMAIL_VALIDATION_STATUS WHERE EVS_EMAIL_ID = LTRIM(RTRIM('" & email_ID & "'))"
            Return _RealService.List(Tables.EMAIL_VALIDATION_STATUS, Query, Tables.EMAIL_VALIDATION_STATUS.ToString, inbasicparam)
        End Function

        Public Function insertEmailValidationStatus(ByVal email_ID As String, ByVal status As String, ByVal remarks As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_email_validation_status]"
            Dim params() As String = {"@EMAIL_ID", "@STATUS", "@REMARKS", "@REC_ADD_BY"}
            Dim values() As Object = {email_ID, status, remarks, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {255, 50, 255, 255}
            _RealService.InsertBySPPublic(Tables.EMAIL_VALIDATION_STATUS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_CashBook))
            Return True
        End Function
        Public Function updateEmailValidationStatus(ByVal email_ID As String, ByVal status As String, ByVal remarks As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_update_email_validation_status]"
            Dim params() As String = {"@EMAIL_ID", "@STATUS", "@REMARKS", "@REC_EDIT_BY"}
            Dim values() As Object = {email_ID, status, remarks, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {255, 50, 255, 255}
            _RealService.InsertBySPPublic(Tables.EMAIL_VALIDATION_STATUS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_CashBook))
            Return True
        End Function
        Public Function GenericEmailer(ByVal cen_ID As String, ByVal FyID As String, ByVal EmailerType As String) As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_email_Generaic_Emailer]"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@EMAILER_TYPE"}
            Dim values() As Object = {Convert.ToInt32(cen_ID), Convert.ToInt32(FyID), EmailerType}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {255, 50, 255, 255}
            Return _RealService.ListDatasetFromSP(Tables.EMAIL_VALIDATION_STATUS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_CashBook))
        End Function
        Public Function SendEmailOnNotificationEvent(ResponseID As String, Notification_Event As String, Optional ChartInstanceID As Integer = 0, Optional Notification_Event_Param_Names As String = "", Optional Notification_Event_Param_Values As String = "") As String
            Dim ReturnMessage As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Service_Chart_Notification"
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RESPONSE_ID", "@EVENT", "@MODE"}
            Dim values() As Object = {iif(ChartInstanceID = 0, Nothing, ChartInstanceID), ResponseID, Notification_Event, "EMAIL"}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 20}
            Dim _RetTable As Datatable = _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
            Dim html As String = ""

            If _RetTable.Rows.Count > 0 Then
                Dim sent As Boolean = SendEmail(_RetTable.Rows(0)("TO_EMAIL").ToString(), _RetTable.Rows(0)("SUBJECT").ToString(), _RetTable.Rows(0)("HTML").ToString(), _RetTable.Rows(0)("EMAILER_NAME").ToString(), _RetTable.Rows(0)("CC_EMAIL").ToString(), _RetTable.Rows(0)("BCC_EMAIL").ToString(), _RetTable.Rows(0)("REPLY_TO").ToString())
                If sent Then
                    ReturnMessage += _RetTable.Rows(0)("TO_EMAIL").ToString() + " :Email sent Successfully.<br/>"
                Else
                    ReturnMessage += _RetTable.Rows(0)("TO_EMAIL").ToString() + " :Email sending failed.<br/>"
                End If
                System.Threading.Thread.Sleep(100)
            Else
                ReturnMessage += "Email Sending Failed (Email Template not Configured for this Event / Chart).<br/>"
            End If

            Return ReturnMessage
        End Function
        Public Function SendWhatsAppOnNotificationEvent(ResponseID As String, Notification_Event As String, Optional ChartInstanceID As Integer = 0, Optional Notification_Event_Param_Names As String = "", Optional Notification_Event_Param_Values As String = "") As String
            Dim ReturnMessage As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Service_Chart_Notification"
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RESPONSE_ID", "@EVENT", "@MODE"}
            Dim values() As Object = {IIf(ChartInstanceID = 0, Nothing, ChartInstanceID), ResponseID, Notification_Event, "WHATSAPP"}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 20}
            Dim _RetTable As DataTable = _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
            Dim html As String = ""

            If _RetTable.Rows.Count > 0 Then
                InsertWhatsappQueue(_RetTable.Rows(0)("MOBILE").ToString(), _RetTable.Rows(0)("TEXT").ToString(), _RetTable.Rows(0)("TEMPLATE_NAME").ToString(), ResponseID, IIf(IsDBNull(_RetTable.Rows(0)("FILE_PATH")), Nothing, _RetTable.Rows(0)("FILE_PATH").ToString()))
                ReturnMessage += _RetTable.Rows(0)("MOBILE").ToString() + " :Whatsapp Scheduled.<br/>"
                System.Threading.Thread.Sleep(100)
            Else
                ReturnMessage += "Email Sending Failed (Email Template not Configured for this Event / Chart).<br/>"
            End If

            Return ReturnMessage
        End Function
        Public Function SendNotificationOnEvent(ResponseID As String, Notification_Event As String, Optional ChartInstanceID As Integer = 0, Optional Notification_Event_Param_Names As String = "", Optional Notification_Event_Param_Values As String = "") As String
            Dim ReturnMessage As String = ""
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Service_Chart_Notification"
#Region "Email"
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RESPONSE_ID", "@EVENT", "@MODE"}
            Dim values() As Object = {IIf(ChartInstanceID = 0, Nothing, ChartInstanceID), ResponseID, Notification_Event, "EMAIL"}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 20}
            Dim _RetTable As DataTable = _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
            Dim html As String = ""

            If _RetTable.Rows.Count > 0 Then
                'Dim sent As Boolean = SendEmail(_RetTable.Rows(0)("TO_EMAIL").ToString(), _RetTable.Rows(0)("SUBJECT").ToString(), _RetTable.Rows(0)("HTML").ToString(), _RetTable.Rows(0)("EMAILER_NAME").ToString(), _RetTable.Rows(0)("CC_EMAIL").ToString(), _RetTable.Rows(0)("BCC_EMAIL").ToString(), _RetTable.Rows(0)("REPLY_TO").ToString())
                Dim sent As Boolean = True
                Dim senderID As String = ConfigurationManager.AppSettings("SenderId")
                Dim senderPassword As String = ConfigurationManager.AppSettings("senderPassword")
                If String.IsNullOrWhiteSpace(_RetTable.Rows(0)("FROM_EMAIL").ToString()) = False Then
                    senderID = _RetTable.Rows(0)("FROM_EMAIL").ToString()
                End If
                If String.IsNullOrWhiteSpace(_RetTable.Rows(0)("PASSWORD").ToString()) = False Then
                    senderPassword = _RetTable.Rows(0)("PASSWORD").ToString()
                End If
                InsertEmailQueue(_RetTable.Rows(0)("TO_EMAIL").ToString(), _RetTable.Rows(0)("SUBJECT").ToString(), _RetTable.Rows(0)("HTML").ToString(), _RetTable.Rows(0)("CC_EMAIL").ToString(), _RetTable.Rows(0)("BCC_EMAIL").ToString(), _RetTable.Rows(0)("EMAILER_NAME").ToString(), ResponseID, _RetTable.Rows(0)("REPLY_TO").ToString(), senderID, senderPassword)

                If sent Then
                    ReturnMessage += _RetTable.Rows(0)("TO_EMAIL").ToString() + " :Email sent Successfully.<br/>"
                Else
                    ReturnMessage += _RetTable.Rows(0)("TO_EMAIL").ToString() + " :Email sending failed.<br/>"
                End If
                System.Threading.Thread.Sleep(100)
            Else
                ReturnMessage += "Email Not Sent (Template not Configured for this Event / Chart).<br/>"
            End If
#End Region
#Region "Whatsapp"
            paramters = {"@CHART_INSTANCE_ID", "@RESPONSE_ID", "@EVENT", "@MODE"}
            values = {IIf(ChartInstanceID = 0, Nothing, ChartInstanceID), ResponseID, Notification_Event, "WHATSAPP"}
            dbTypes = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            lengths = {4, 36, 255, 20}
            _RetTable = _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))

            If _RetTable.Rows.Count > 0 Then
                'make entry in whatsapp queue here 
                InsertWhatsappQueue(_RetTable.Rows(0)("MOBILE").ToString(), _RetTable.Rows(0)("TEXT").ToString(), _RetTable.Rows(0)("TEMPLATE_NAME").ToString(), ResponseID, IIf(IsDBNull(_RetTable.Rows(0)("FILE_PATH")), Nothing, _RetTable.Rows(0)("FILE_PATH").ToString()))
                ReturnMessage += _RetTable.Rows(0)("MOBILE").ToString() + " :WhatsApp Sending Scheduled.<br/>"
                System.Threading.Thread.Sleep(100)
            Else
                ReturnMessage += "WhatsApp Not Sent (Template not Configured for this Event / Chart).<br/>"
            End If
#End Region
            Return ReturnMessage
        End Function
        Public Sub InsertEmailQueue(ByVal ToEmail As String, ByVal Subject As String, Optional ByVal Message_HMTL As String = Nothing, Optional ByVal CCEmail As String = Nothing, Optional ByVal BCCEmail As String = Nothing, Optional ByVal BatchName As String = Nothing, Optional ByVal ResponseID As String = Nothing, Optional ByVal ReplyToEmail As String = Nothing, Optional ByVal SenderIDEmail As String = Nothing, Optional ByVal SenderIDPwd As String = Nothing, Optional ByVal Message_HMTL_URL As String = Nothing, Optional CenID As Int32? = Nothing)
            Dim Center_ID As Int32 = cBase._open_Cen_ID
            If Not CenID Is Nothing Then
                Center_ID = CenID
            End If
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@TO", "@CC", "@BCC", "@REPLYTO", "@EMAILER_NAME", "@SUBJECT", "@URL", "@SENDERID", "@SENDERPWD", "@HTML", "@RESPONSE_ID", "@CEN_ID"}
            Dim values As Object() = {ToEmail, CCEmail, BCCEmail, ReplyToEmail, BatchName, Subject, Message_HMTL_URL, SenderIDEmail, SenderIDPwd, Message_HMTL, ResponseID, Center_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32}
            Dim lengths As Integer() = {255, 255, 255, 255, -1, -1, -1, 255, 50, -1, 36, 4}
            _RealService.InsertBySPPublic(Tables.EMAIL_SCHEDULER_QUEUE, "[sp_Insert_Email_Queue]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Options_FormResponse))
        End Sub
        Public Function GetSMSNotificationOnEvent(ResponseID As String, Notification_Event As String, Optional ChartInstanceID As Integer = 0, Optional Notification_Event_Param_Names As String = "", Optional Notification_Event_Param_Values As String = "") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Service_Chart_Notification"
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RESPONSE_ID", "@EVENT", "@MODE"}
            Dim values() As Object = {IIf(ChartInstanceID = 0, Nothing, ChartInstanceID), ResponseID, Notification_Event, "SMS"}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 20}
            Return _RealService.ListFromSP(Tables.SERVICE_REPORT_INFO, SPName, Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
    End Class
End Class