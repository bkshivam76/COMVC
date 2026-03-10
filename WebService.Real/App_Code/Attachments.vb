Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports Common_Lib.RealTimeService
Imports System.Activities.Expressions
Imports System.Net
Namespace Real
    <Serializable>
    Public Class Attachments
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Attachment_GetList
            Public RefId As String
            Public openCenID As Int32
        End Class
        <Serializable>
        Public Class Parameter_Insert_Attachment
            Public FileName As String
            Public Description As String
            Public NameID As String
            'Public Checked As Boolean
            Public Ref_Screen As String
            Public Ref_Rec_ID As String
            Public RecID As String
            Public File As Byte()
            Public Applicable_From As DateTime
            Public Applicable_To As DateTime
            Public Checked As Boolean = False
            Public Vouching_Category As String
            Public AI_CEN_RATING As Integer = 0
        End Class
        <Serializable>
        Public Class Parameter_Insert_Attachment_Link
            Public Ref_Screen As String
            Public Ref_Rec_ID As String
            Public AttachmentID As String
        End Class

        <Serializable>
        Public Class Parameter_Attachment_Unlink
            Public Ref_Rec_ID As String
            Public AttachmentID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Attachment
            Public FileName As String
            Public Description As String
            Public CategoryID As String
            'Public Checked As Boolean
            Public Ref_Screen As String
            Public Ref_Rec_ID As String
            Public RecID As String
            Public File As Byte()
            Public Applicable_From As DateTime
            Public Applicable_To As DateTime
            Public Checked As Boolean = False
            Public Vouching_Category As String
        End Class
        <Serializable>
        Public Class Parameter_GetAttachmentLinkCount
            Public AttachmentID As String
            ''' <summary>
            ''' Screen where attachment is linked Currently and is to be excluded in count
            ''' </summary>
            Public RefScreen As String
            ''' <summary>
            ''' ID of Record where attachment is linked Currently and is to be excluded in count
            ''' </summary>
            Public RefRecordID As String

        End Class
        Public Class Parameter_Attachment_Mark_as_Rejected
            Public AttachmentID As String
            Public RejectionReason As String
        End Class
#End Region
        Public Shared Function GetList(inParam As Parameter_Attachment_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT [MISC_NAME] + '('+ MISC_REMARK1 + ')' 'Attachment',[AI_FILE_NAME] 'File Name',[AI_DESCRIPTION]+ COALESCE('('+ CAST(AI_YR_SRNO AS VARCHAR)+')','') 'Description',[AI_DESCRIPTION] 'Only Description',[AI_APPLICABLE_FROM] 'Applicable From',[AI_APPLICABLE_TO] 'Applicable To',[AI_CHECKED] 'Checked', [AI_IS_BANNER] as 'Isbanner', CASE WHEN COALESCE(AI_CHECKED,-1) = -1 THEN 'PENDING' WHEN COALESCE(AI_CHECKED,-1) = 0 THEN 'REJECTED'  WHEN COALESCE(AI_CHECKED,-1) = 1 THEN  'ACCEPTED' END AS Checking_Status, AI_REJECT_REASON as Rejection_Reason, A.rec_id as ID,A.rec_id AS UniqueID,AI_CATEGORY,AI_RATING,AI_CEN_RATING, " & Common.Rec_Detail("A") &
                                  " FROM [attachment_info] A LEFT OUTER JOIN MISC_INFO AS MI ON AI_CATEGORY = MI.REC_ID	  WHERE [AI_CEN_ID] = " & inParam.openCenID.ToString & " " &
                          " ORDER BY CASE WHEN COALESCE(AI_CHECKED,-1) = -1 THEN 'PENDING' WHEN COALESCE(AI_CHECKED,-1) = 0 THEN 'REJECTED'  WHEN COALESCE(AI_CHECKED,-1) = 1 THEN  'ACCEPTED' END desc, A.REC_ADD_ON DESC  "

            If inParam.RefId.Length > 0 Then
                Dim RefString As String = "'" & inParam.RefId & "'"
                If inParam.RefId = "ALL" Then RefString = "NULL"
                OnlineQuery = "SELECT [ALI_REF_SCREEN] Screen,[ALI_REF_REC_ID] REF_ID,[MISC_NAME] + '('+ MISC_REMARK1 + ')' 'Attachment',[AI_FILE_NAME] 'File Name',[AI_DESCRIPTION]+ COALESCE('('+ CAST(AI_YR_SRNO AS VARCHAR)+')','') 'Description',[AI_DESCRIPTION] 'Only Description',[AI_APPLICABLE_FROM] 'Applicable From',[AI_APPLICABLE_TO] 'Applicable To',[AI_CHECKED] 'Checked', [AI_IS_BANNER] as 'Isbanner', CASE WHEN COALESCE(AI_CHECKED,-1) = -1 THEN 'PENDING' WHEN COALESCE(AI_CHECKED,-1) = 0 THEN 'REJECTED'  WHEN COALESCE(AI_CHECKED,-1) = 1 THEN  'ACCEPTED' END AS Checking_Status,AI_REJECT_REASON as Rejection_Reason, A.rec_id as ID,A.rec_id+ COALESCE(ALI_REF_REC_ID,'') AS UniqueID,AI_CATEGORY,AI_RATING,AI_CEN_RATING, " & Common.Rec_Detail("A") &
                                  " FROM [attachment_info] A LEFT OUTER JOIN attachment_reference_info REF ON A.REC_ID = REF.ALI_ATTACHMENT_ID LEFT OUTER JOIN MISC_INFO AS MI ON AI_CATEGORY = MI.REC_ID	  WHERE [AI_CEN_ID] = " & inParam.openCenID.ToString & " "
                OnlineQuery += " AND COALESCE(ALI_REF_REC_ID,'') =COALESCE(" & RefString & ",COALESCE(ALI_REF_REC_ID,''))"
                OnlineQuery += " ORDER BY CASE WHEN COALESCE(AI_CHECKED,-1) = -1 THEN 'PENDING' WHEN COALESCE(AI_CHECKED,-1) = 0 THEN 'REJECTED'  WHEN COALESCE(AI_CHECKED,-1) = 1 THEN  'ACCEPTED' END desc, A.REC_ADD_ON DESC  "
            End If
            Return dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_Nested(RefId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = ""
            Dim RefString As String = "'" & RefId & "'"
            If RefId = "ALL" Then RefString = "NULL"
            OnlineQuery = "SELECT DISTINCT [MISC_NAME] + '('+ MISC_REMARK1 + ')' 'Attachment',[AI_FILE_NAME] 'File Name',[AI_DESCRIPTION] 'Description',[AI_APPLICABLE_FROM] 'Applicable From',[AI_APPLICABLE_TO] 'Applicable To',[AI_CHECKED] 'Checked', A.rec_id as ID, " & Common.Rec_Detail("A") &
                                " FROM [attachment_info] A LEFT OUTER JOIN attachment_reference_info REF ON A.REC_ID = REF.ALI_ATTACHMENT_ID LEFT OUTER JOIN MISC_INFO AS MI ON AI_CATEGORY = MI.REC_ID	  WHERE [AI_CEN_ID] = " & inBasicParam.openCenID.ToString & " "
            OnlineQuery += " AND COALESCE(ALI_REF_REC_ID,'') = COALESCE(" & RefString & ",COALESCE(ALI_REF_REC_ID,''))"
            OnlineQuery += " ORDER BY A.REC_ADD_ON DESC"
            Return dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetRecord(RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT A.*, MISC_REMARK1 CATEGORY " &
                                  " FROM [attachment_info] A LEFT OUTER JOIN MISC_INFO AS MI ON AI_CATEGORY = MI.REC_ID	  WHERE A.rec_id = '" & RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAttachmentLinkCount(inParam As Parameter_GetAttachmentLinkCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT ALI_REF_SCREEN Screen FROM attachment_reference_info WHERE ALI_ATTACHMENT_ID='" & inParam.AttachmentID & "' AND (ALI_REF_REC_ID <> '" & inParam.RefRecordID & "' OR ALI_REF_SCREEN <> '" & inParam.RefScreen & "') AND LEN(COALESCE(ALI_REF_SCREEN,''))>0 "
            Return dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAttachmentCount(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = ""
            'OnlineQuery = "SELECT * FROM ( SELECT STATUS, COALESCE(count(ai.REC_ID),0) CNT FROM (SELECT -1 CHECKED,'PENDING' STATUS, " & inBasicParam.openCenID.ToString() & " CEN_ID UNION ALL SELECT 1,'ACCEPTED', " & inBasicParam.openCenID.ToString() & " CEN_ID UNION ALL SELECT 0,'REJECTED', " & inBasicParam.openCenID.ToString() & " CEN_ID )AS A  LEFT JOIN attachment_info AI ON A.CHECKED = coalesce(AI_CHECKED,-1) AND AI_CEN_ID = A.CEN_ID AND AI_COD_YEAR_ID = " & inBasicParam.openYearID & " group by STATUS ) AS B PIVOT( SUM(CNT) FOR STATUS IN ( [PENDING], [ACCEPTED], [REJECTED])) AS pivot_table"
            'Return dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@SCREEN", "@CEN_ID", "@YEAR_ID", "@TYPE", "@USER_ID"}
            Dim values() As Object = {"ALL", inBasicParam.openCenID, inBasicParam.openYearID, "DASHBOARD", inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {System.Data.DbType.String, System.Data.DbType.Int32, System.Data.DbType.Int32, System.Data.DbType.String, System.Data.DbType.String}
            Dim lengths() As Integer = {255, 8, 8, 255, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ATTACHMENT_INFO, "[sp_get_Document_stats]", ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)

        End Function

        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Attachment, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As String
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NameID As String = "Null"
            If InParam.NameID.Length > 0 Then
                NameID = "'" & InParam.NameID & "'"
            End If

            If InParam.Ref_Screen = "Help_Action_Items" Then
                inBasicParam.screen = ConnectOneWS.ClientScreen.Help_Action_Items
            End If

            UploadFile(InParam.File, InParam.FileName, InParam.RecID)
            Dim OnlineQuery As String = "INSERT INTO [dbo].[attachment_info]([AI_CEN_ID],[AI_COD_YEAR_ID],[AI_FILE_NAME],[AI_DESCRIPTION],[AI_CATEGORY],[AI_APPLICABLE_FROM],[AI_APPLICABLE_TO],[AI_CHECKED]," &
                                            "[REC_ADD_ON],[REC_ADD_BY],[REC_EDIT_ON],[REC_EDIT_BY],[REC_STATUS],[REC_STATUS_ON],[REC_STATUS_BY],[REC_ID],[AI_YR_SRNO],[AI_CEN_RATING]" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam.FileName & "', " &
                                                  "N'" & InParam.Description & "', " &
                                                  "" & NameID & ", " &
                                                 " " & If(IsDate(InParam.Applicable_From) And InParam.Applicable_From <> DateTime.MinValue, "'" & Convert.ToDateTime(InParam.Applicable_From).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  " " & If(IsDate(InParam.Applicable_To) And InParam.Applicable_To <> DateTime.MinValue, "'" & Convert.ToDateTime(InParam.Applicable_To).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  " NULL, " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'," &
                                                   "(SELECT COALESCE(MAX(AI_YR_SRNO),0)+1 FROM attachment_info WHERE AI_CEN_ID = " + inBasicParam.openCenID.ToString() + " AND AI_COD_YEAR_ID = " + inBasicParam.openYearID.ToString() + ")," & InParam.AI_CEN_RATING & ")"
            dbService.Insert(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, inBasicParam, InParam.Ref_Rec_ID, Nothing, AddTime)


            OnlineQuery = "INSERT INTO [dbo].[attachment_reference_info] ([ALI_ATTACHMENT_ID],[ALI_REF_SCREEN],[ALI_REF_REC_ID],[REC_ADD_ON],[REC_ADD_BY],ALI_REF_COD_YEAR_ID,ALI_CEN_ID" &
                                                  ") VALUES(" &
                                                  "'" & InParam.RecID & "'," &
                                                  "'" & InParam.Ref_Screen & "', " &
                                                  "'" & InParam.Ref_Rec_ID & "', GETDATE(), '" & inBasicParam.openUserID & "', " & inBasicParam.openYearID.ToString() & ", " & inBasicParam.openCenID.ToString() & ")"
            dbService.Insert(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, inBasicParam, InParam.Ref_Rec_ID, Nothing, AddTime)
            'Removed remarks against no attachment posting for same mapping 

            OnlineQuery = "SELECT TR_DOC_RESP_ID FROM transaction_doc_mapping_Response WHERE TR_DOC_RESP_REF_REC_ID = '" & InParam.Ref_Rec_ID & "' AND TR_DOC_RESP_MAP_ID  IN (SELECT TR_DOC_ID FROM TRANSACTION_DOC_MAPPING  WHERE TR_DOC_MISC_ID = '" & InParam.NameID & "')"
            Dim Mapping_ID As String = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE.ToString(), inBasicParam)

            'Audit.UnVouchEntryByReference(InParam.Ref_Rec_ID, inBasicParam, Nothing, Mapping_ID)
            If (Mapping_ID Is Nothing) Then Mapping_ID = "1"

            dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, " COALESCE(VA_ATTACHMENT_RESP_ID,0) = " & Mapping_ID & " ", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, "TR_DOC_RESP_REF_REC_ID = '" & InParam.Ref_Rec_ID & "' AND TR_DOC_RESP_MAP_ID  IN (SELECT TR_DOC_ID FROM TRANSACTION_DOC_MAPPING  WHERE TR_DOC_MISC_ID = '" & InParam.NameID & "')", inBasicParam)

            'Pending vouching posted against referred entry 
            'Audit.UnVouchEntryByReference(InParam.Ref_Rec_ID, inBasicParam)//Removed as per Task Request #658

            'UploadFile(InParam.File, InParam.FileName, InParam.RecID)
            'Mark as checked 
            If (InParam.Checked) Then
                Mark_as_Checked(InParam.RecID, inBasicParam)
            End If

            OnlineQuery = "SELECT AI_YR_SRNO FROM attachment_info WHERE REC_ID = '" + InParam.RecID + "'"
            Dim SrNo As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)

            Return SrNo
        End Function

        Public Shared Function Insert_Link(ByVal InParam As Parameter_Insert_Attachment_Link, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = "INSERT INTO [dbo].[attachment_reference_info] ([ALI_ATTACHMENT_ID],[ALI_REF_SCREEN],[ALI_REF_REC_ID],[REC_ADD_ON],[REC_ADD_BY],ALI_REF_COD_YEAR_ID,ALI_CEN_ID" &
                                                  ") VALUES(" &
                                                  "'" & InParam.AttachmentID & "'," &
                                                  "'" & InParam.Ref_Screen & "', " &
                                                  "'" & InParam.Ref_Rec_ID & "', GETDATE(), '" & inBasicParam.openUserID & "', " & inBasicParam.openYearID.ToString() & ", " & inBasicParam.openCenID.ToString() & ")"
            dbService.Insert(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, inBasicParam, InParam.Ref_Rec_ID, Nothing, AddTime)

            OnlineQuery = "SELECT AI_CATEGORY FROM attachment_info WHERE REC_ID ='" & InParam.AttachmentID & "' "
            Dim DOC_ID As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, "TR_DOC_RESP_REF_REC_ID = '" & InParam.Ref_Rec_ID & "' AND TR_DOC_RESP_MAP_ID  IN (SELECT TR_DOC_ID FROM TRANSACTION_DOC_MAPPING  WHERE TR_DOC_MISC_ID = '" & DOC_ID & "')", inBasicParam)

            'Pending vouching posted against referred entry 
            'Audit.UnVouchEntryByReference(InParam.Ref_Rec_ID, inBasicParam)//Removed as per Task Request #658
            Return True
        End Function

        Private Shared Function UploadFile(f As Byte(), fileName As String, ID As String) As String
            ' the byte array argument contains the content of the file
            ' the string argument contains the name and extension
            ' of the file passed in the byte array
            Try
                'use the original file name
                ' to name the resulting file

                Dim separator() As Char = {"."}
                Dim Fileparts As String() = fileName.Split(separator)

                Dim fileType As String = ""
                If Fileparts.Length > 1 Then fileType = "." & Fileparts(Fileparts.Length - 1)

                If (ConfigurationManager.AppSettings("UploadAttachmentByFTP")).ToUpper = "FALSE" Then 'check if we need to use ftp for saving file to disk
                    ' instance a memory stream and pass the
                    ' byte array to its constructor
                    Dim ms As New MemoryStream(f)

                    ' instance a filestream pointing to the
                    ' storage folder,

                    Dim fs As New FileStream(ConfigurationManager.AppSettings("FilePhysicalPath") & ID & fileType, FileMode.Create)

                    ' write the memory stream containing the original
                    ' file as a byte array to the filestream
                    ms.WriteTo(fs)

                    ' clean up
                    ms.Close()
                    ms.Dispose()
                    fs.Close()
                    fs.Dispose()

                Else 'saving file to disk by FTP
                    'FTP Server URL
                    Dim ftp As String = ConfigurationManager.AppSettings("FTPServer")
                    'FTP Folder name. Leave blank if you want to upload to root folder
                    '(really blank, Not "/" !)
                    Dim ftpFolder As String = ""
                    'ftp username and password
                    Dim ftpUserName As String = ConfigurationManager.AppSettings("FTPUser")
                    Dim ftpPassword As String = ConfigurationManager.AppSettings("FTPPassword")
                    'create FTP Request
                    Dim Request As FtpWebRequest = DirectCast(WebRequest.Create(ftp & ftpFolder & ID & fileType), FtpWebRequest)
                    Request.Method = WebRequestMethods.Ftp.UploadFile
                    ' enter FTP Server credentials
                    Request.Credentials = New NetworkCredential(ftpUserName, ftpPassword)
                    Request.ContentLength = f.Length
                    Request.UsePassive = True
                    Request.UseBinary = True  ' Or False For ASCII files
                    Request.ServicePoint.ConnectionLimit = f.Length
                    Request.EnableSsl = False
                    Using requestStream As Stream = Request.GetRequestStream()
                        requestStream.Write(f, 0, f.Length)
                        requestStream.Close()
                        requestStream.Dispose()
                    End Using
                End If
                f = Nothing
                ' return OK if we made it this far
                Return "OK"
            Catch ex As Exception
                Common_Lib.Log.Write(Common_Lib.Log.LogType.Error, "Attachment", "uploadfile", ex.Message, Common_Lib.Log.LogSuffix.ClientApplication)
                ' return the error message if the operation fails
                Return ex.Message.ToString()
            End Try
        End Function

        Public Shared Function Download_file(fileName As String) As Byte()
            Dim fs1 As System.IO.FileStream = Nothing
            fs1 = System.IO.File.Open(ConfigurationManager.AppSettings("FilePhysicalPath") & fileName, FileMode.Open, FileAccess.Read)
            Dim b1() As Byte = New Byte(fs1.Length) {}
            fs1.Read(b1, 0, CType(fs1.Length, Integer))
            fs1.Close()
            Return b1
        End Function

        Public Shared Function Delete_Attachment(FileName As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim separator() As Char = {"."}
            Dim ID As String = FileName.Split(separator, 2)(0)
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Remove vouching posted against referred entries
            Dim OnlineQuery As String = "SELECT ALI_REF_REC_ID  FROM attachment_reference_info WHERE ALI_ATTACHMENT_ID = '" & ID & "' "
            Dim linksToAttachmentTbl As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO.ToString(), inBasicParam)
            OnlineQuery = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_INFO AI INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE AI.REC_ID = '" & ID & "' "
            Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
            For Each cRow As DataRow In linksToAttachmentTbl.Rows
                'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & cRow(0)(0).ToString() & "' ", inBasicParam)
                If (AttachmentVouchingRequirement <> "NO VOUCHING REQUIRED") Then 'whole entry is not to be unvvouched on deletion of No Vouching Attachments 
                    'Pending vouching posted against referred entry 
                    Audit.UnVouchEntryByReference(cRow(0)(0).ToString(), inBasicParam)
                End If
            Next

            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_ATTACHMENT_ID = '" & ID & "'", inBasicParam)

            dbService.Delete(ConnectOneWS.Tables.ATTACHMENT_INFO, ID, inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ATTACHMENT_ID = '" & ID & "'", inBasicParam)

            File.Delete(ConfigurationManager.AppSettings("FilePhysicalPath") & FileName)

            Return True
        End Function

        Public Shared Function Delete_Attachment_ByID(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()


            Dim OnlineQuery As String = "select AI_FILE_NAME, REC_ID AS ATTACHMENT_ID from attachment_info WHERE REC_ID = '" & Rec_ID & "'"
            Dim _attachmentsTable As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)

            If _attachmentsTable.Rows.Count > 0 Then
                dbService.Delete(ConnectOneWS.Tables.ATTACHMENT_INFO, _attachmentsTable.Rows(0)("ATTACHMENT_ID").ToString(), inBasicParam)
                Dim separator() As Char = {"."}
                Dim Actual_File_Name As String = _attachmentsTable.Rows(0)("AI_FILE_NAME").ToString()
                Dim FileType As String = Actual_File_Name.Split(separator, 2)(Actual_File_Name.Split(separator, 2).Length - 1)
                Dim Delete_File_Name As String = _attachmentsTable.Rows(0)("ATTACHMENT_ID").ToString() & "." & FileType
                File.Delete(ConfigurationManager.AppSettings("FilePhysicalPath") & Delete_File_Name)

                'Remove vouching posted against referred entries
                OnlineQuery = "SELECT ALI_REF_REC_ID FROM attachment_reference_info WHERE ALI_ATTACHMENT_ID = '" & Rec_ID & "' "
                Dim linksToAttachmentTbl As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO.ToString(), inBasicParam)
                OnlineQuery = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_INFO AI INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE AI.REC_ID = '" & Rec_ID & "' "
                Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
                For Each cRow As DataRow In linksToAttachmentTbl.Rows
                    'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & cRow(0)(0).ToString() & "' ", inBasicParam)
                    If (AttachmentVouchingRequirement <> "NO VOUCHING REQUIRED") Then 'whole entry is not to be unvvouched on deletion of No Vouching Attachments 
                        'Pending vouching posted against referred entry 
                        Audit.UnVouchEntryByReference(cRow(0)(0).ToString(), inBasicParam)
                    End If
                Next

            End If
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_ATTACHMENT_ID = '" & Rec_ID & "'", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_INFO, "REC_ID = '" & Rec_ID & "'", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ATTACHMENT_ID = '" & Rec_ID & "'", inBasicParam)
            Return True
        End Function

        Public Shared Function Delete_Attachment_ByReference(Reference_Rec_ID As String, Reference_Screen As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim UnVouchEntry As Boolean = False
            Dim OnlineQuery As String = "select AI_FILE_NAME, AI.REC_ID AS ATTACHMENT_ID from attachment_info ai INNER JOIN attachment_reference_info AS AIR ON AI.REC_ID = AIR.ALI_ATTACHMENT_ID WHERE AIR.ALI_REF_REC_ID = '" & Reference_Rec_ID & "' AND ALI_REF_SCREEN = '" + Reference_Screen + "'"
            Dim _attachmentsTable As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_REF_REC_ID = '" & Reference_Rec_ID & "'", inBasicParam)
            For Each _drow As DataRow In _attachmentsTable.Rows
                Dim separator() As Char = {"."}
                Dim Actual_File_Name As String = _drow("AI_FILE_NAME").ToString()
                Dim FileType As String = Actual_File_Name.Split(separator, 2)(Actual_File_Name.Split(separator, 2).Length - 1)
                Dim Delete_File_Name As String = _drow("ATTACHMENT_ID").ToString() & "." & FileType
                File.Delete(ConfigurationManager.AppSettings("FilePhysicalPath") & Delete_File_Name)
                OnlineQuery = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_INFO AI INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE AI.REC_ID = '" & _drow("ATTACHMENT_ID").ToString() & "' "
                Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
                If AttachmentVouchingRequirement <> "NO VOUCHING REQUIRED" Then
                    UnVouchEntry = True
                End If
                dbService.Delete(ConnectOneWS.Tables.ATTACHMENT_INFO, _drow("ATTACHMENT_ID").ToString(), inBasicParam)
                dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ATTACHMENT_ID = '" & _drow("ATTACHMENT_ID").ToString() & "'", inBasicParam)
            Next

            ''Remove vouching posted against referred entry 
            'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & Reference_Rec_ID & "' ", inBasicParam)
            If UnVouchEntry Then 'whole entry is not to be unvvouched on deletion of No Vouching Attachments 
                'Pending vouching posted against referred entry 
                Audit.UnVouchEntryByReference(Reference_Rec_ID, inBasicParam)
            End If
            Return True
        End Function
        Public Shared Function Delete_Attachment_ByDescription(attachmentDescription As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select AI_FILE_NAME, AI.REC_ID AS ATTACHMENT_ID from attachment_info ai WHERE AI_DESCRIPTION = '" & attachmentDescription & "'"
            Dim _attachmentsTable As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
            Dim UnVouchEntry As Boolean = False
            For Each _drow As DataRow In _attachmentsTable.Rows
                OnlineQuery = "SELECT ALI_REF_REC_ID FROM attachment_reference_info WHERE ALI_ATTACHMENT_ID='" & _drow("ATTACHMENT_ID").ToString() & "'"
                Dim _attachmentsRefTable As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
                OnlineQuery = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_INFO AI INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE AI.REC_ID = '" & _drow("ATTACHMENT_ID").ToString() & "' "
                Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
                If AttachmentVouchingRequirement <> "NO VOUCHING REQUIRED" Then
                    UnVouchEntry = True
                End If

                For Each _dRowRef As DataRow In _attachmentsRefTable.Rows
                    ''Remove vouching posted against referred entry 
                    'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & _dRowRef("ALI_REF_REC_ID").ToString() & "' ", inBasicParam)
                    If UnVouchEntry Then 'whole entry is not to be unvvouched on deletion of No Vouching Attachments 
                        'Pending vouching posted against referred entry 
                        Audit.UnVouchEntryByReference(_dRowRef("ALI_REF_REC_ID").ToString(), inBasicParam)
                    End If
                Next

                Dim separator() As Char = {"."}
                Dim Actual_File_Name As String = _drow("AI_FILE_NAME").ToString()
                Dim FileType As String = Actual_File_Name.Split(separator, 2)(Actual_File_Name.Split(separator, 2).Length - 1)
                Dim Delete_File_Name As String = _drow("ATTACHMENT_ID").ToString() & "." & FileType
                File.Delete(ConfigurationManager.AppSettings("FilePhysicalPath") & Delete_File_Name)
                dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ATTACHMENT_ID = '" & _drow("ATTACHMENT_ID").ToString() & "'", inBasicParam)
                dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_ATTACHMENT_ID = '" & _drow("ATTACHMENT_ID").ToString() & "'", inBasicParam)
                dbService.Delete(ConnectOneWS.Tables.ATTACHMENT_INFO, _drow("ATTACHMENT_ID").ToString(), inBasicParam)
            Next

            Return True
        End Function

        Public Shared Function Unlink_Attachment(Inparam As Parameter_Attachment_Unlink, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Remove vouching posted against referred entry 
            If Inparam.Ref_Rec_ID.Length > 0 Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & Inparam.Ref_Rec_ID & "' ", inBasicParam)
            Else
                'Remove vouching posted against referred entries
                Dim OnlineQuery As String = "SELECT ALI_REF_REC_ID FROM attachment_reference_info WHERE ALI_ATTACHMENT_ID = '" & Inparam.AttachmentID & "' "
                Dim linksToAttachmentTbl As DataTable = dbService.List(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO.ToString(), inBasicParam)
                OnlineQuery = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_INFO AI INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE AI.REC_ID = '" & Inparam.AttachmentID & "' "
                Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
                For Each cRow As DataRow In linksToAttachmentTbl.Rows
                    ' dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & cRow(0).ToString() & "' ", inBasicParam)
                    If AttachmentVouchingRequirement <> "NO VOUCHING REQUIRED" Then
                        'Pending vouching posted against referred entry 
                        Audit.UnVouchEntryByReference(cRow(0).ToString(), inBasicParam)
                    End If
                Next
            End If

            If Inparam.Ref_Rec_ID.Length > 0 Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_ATTACHMENT_ID = '" & Inparam.AttachmentID & "' AND ALI_REF_REC_ID = '" & Inparam.Ref_Rec_ID & "'", inBasicParam)
            Else
                dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_ATTACHMENT_ID = '" & Inparam.AttachmentID & "' ", inBasicParam)
            End If
            Return True
        End Function

        Public Shared Function Unlink_Attachment_ByReference(Ref_Rec_ID As String, Ref_Screen As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_REFERENCE_INFO, "ALI_REF_REC_ID = '" & Ref_Rec_ID & "' AND ALI_REF_SCREEN = '" + Ref_Screen + "'", inBasicParam)

            ''Remove vouching posted against referred entry 
            'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & Ref_Rec_ID & "' ", inBasicParam)
            'Pending vouching posted against referred entry 
            Dim OnlineQuery As String = "SELECT COALESCE(DOC.MISC_REMARK2,'') FROM ATTACHMENT_REFERENCE_INFO ARI INNER JOIN ATTACHMENT_INFO AI ON ARI.ALI_ATTACHMENT_ID = AI.REC_ID INNER JOIN misc_info AS DOC ON AI.AI_CATEGORY=DOC.REC_ID WHERE DOC.MISC_REMARK2 <> 'NO VOUCHING REQUIRED' AND ALI_REF_REC_ID = '" & Ref_Rec_ID & "' AND ALI_REF_SCREEN = '" + Ref_Screen + "' "
            Dim AttachmentVouchingRequirement As String = dbService.GetScalar(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)
            If AttachmentVouchingRequirement.Length = 0 Then ' Attachments needing vouching exist
                Audit.UnVouchEntryByReference(Ref_Rec_ID, inBasicParam)
            End If

            Return True
        End Function

        Public Overloads Shared Function Update(ByVal InParam As Parameter_Update_Attachment, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE attachment_info SET " &
                                        " [AI_FILE_NAME] = '" & InParam.FileName & "'," &
                                        " [AI_DESCRIPTION] = N'" & InParam.Description & "'," &
                                        " [AI_CATEGORY] = '" & InParam.CategoryID & "'," &
                                        " [AI_APPLICABLE_FROM] =" & If(IsDate(InParam.Applicable_From) And InParam.Applicable_From <> DateTime.MinValue, "'" & Convert.ToDateTime(InParam.Applicable_From).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " ," &
                                        " [AI_APPLICABLE_TO] =" & If(IsDate(InParam.Applicable_To) And InParam.Applicable_To <> DateTime.MinValue, "'" & Convert.ToDateTime(InParam.Applicable_To).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " ," &
                                         "AI_CHECKED             = NULL, AI_REJECT_REASON       ='', " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                         "  WHERE REC_ID    ='" & InParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, inBasicParam, EditTime)
            If Not InParam.File Is Nothing Then
                Dim separator() As Char = {"."}
                Dim fileType As String = InParam.FileName.Split(separator, 2)(InParam.FileName.Split(separator, 2).Length - 1)
                File.Delete(ConfigurationManager.AppSettings("FilePhysicalPath") & InParam.RecID & "." & fileType)
                UploadFile(InParam.File, InParam.FileName, InParam.RecID)
            End If
            'Mark as checked 
            If (InParam.Checked) Then
                Mark_as_Checked(InParam.RecID, inBasicParam)
            End If
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, "TR_DOC_RESP_REF_REC_ID = '" & InParam.Ref_Rec_ID & "' AND TR_DOC_RESP_MAP_ID  IN (SELECT TR_DOC_ID FROM TRANSACTION_DOC_MAPPING  WHERE TR_DOC_MISC_ID = '" & InParam.CategoryID & "')", inBasicParam)
            ''Remove vouching posted against referred entry 
            'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & InParam.Ref_Rec_ID & "' ", inBasicParam)
            'Pending vouching posted against referred entry for that attachment only
            Audit.UnVouchEntryByReference(InParam.Ref_Rec_ID, inBasicParam, InParam.RecID)
            Return True
        End Function

        Public Overloads Shared Function Mark_as_Checked(ByVal ID As String, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE attachment_info SET " &
                                         "AI_CHECKED             = 1, " &
                                         "AI_REJECT_REASON       =N'', " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Incomplete & "," &
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                         "  WHERE REC_ID    ='" & ID & "'"
            dbService.Update(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, inBasicParam, EditTime)
            'Delete allotment
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_CHECKING_ALLOTTMENT, "ATTACHMENT_ID='" & ID & "'", inBasicParam)
            Return True
        End Function

        Public Overloads Shared Function Mark_as_Rejected(ByVal inparam As Parameter_Attachment_Mark_as_Rejected, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE attachment_info SET " &
                                         "AI_CHECKED             =0, " &
                                         "AI_REJECT_REASON       =N'" & inparam.RejectionReason & "', " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Incomplete & "," &
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                         "  WHERE REC_ID    ='" & inparam.AttachmentID & "'"
            dbService.Update(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, inBasicParam, EditTime)
            'Delete allotment
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_CHECKING_ALLOTTMENT, "ATTACHMENT_ID='" & inparam.AttachmentID & "'", inBasicParam)
            Return True
        End Function

        Public Overloads Shared Function Mark_as_UnChecked(ByVal ID As String, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE attachment_info SET " &
                                         "AI_CHECKED             = NULL, " &
                                         "AI_REJECT_REASON       =N'', " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Incomplete & "," &
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                         "  WHERE REC_ID    ='" & ID & "'"
            dbService.Update(ConnectOneWS.Tables.ATTACHMENT_INFO, OnlineQuery, inBasicParam, EditTime)
            'Delete allotment
            dbService.DeleteByCondition(ConnectOneWS.Tables.ATTACHMENT_CHECKING_ALLOTTMENT, "ATTACHMENT_ID='" & ID & "'", inBasicParam)

            Return True
        End Function
    End Class
End Namespace

