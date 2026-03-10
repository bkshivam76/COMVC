Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Attachments
        Inherits SharedVariables
#Region "Report/Return classes"
        <Serializable>
        Public Class Attachment_List
            Public Property Screen As String
            Public Property Attachment As String
            Public Property File_Name As String
            Public Property Description As String
            Public Property Only_Description As String
            Public Property Applicable_From As Nullable(Of DateTime)
            Public Property Applicable_To As Nullable(Of DateTime)
            Public Property Checked As Boolean?
            Public Property Checking_Status As String
            Public Property Document_Rejection_Reason As String
            Public Property ID As String
            Public Property UniqueID As String
            Public Property IsBanner As Boolean?
            Public Property RefID As String
            Public Property Misc_Rec_ID As String
            Public Property AI_RATING As Integer
            Public Property AI_CEN_RATING As Integer
            ''Common Columns 
            ''' <summary>
            ''' Original Column name : Add By
            ''' </summary>
            Public Property Add_By As String
            ''' <summary>
            ''' Original Column name : Add Date
            ''' </summary>
            Public Property Add_Date As String
            ''' <summary>
            ''' Original Column name : Edit By
            ''' </summary>
            Public Property Edit_By As String
            ''' <summary>
            ''' Original Column name : Edit Date
            ''' </summary>
            Public Property Edit_Date As String
            ''' <summary>
            ''' Original Column name : Action Status 
            ''' </summary>
            Public Property Action_Status As String
            ''' <summary>
            ''' Original Column name : Action By
            ''' </summary>
            Public Property Action_By As String
            ''' <summary>
            ''' Original Column name : Action Date
            ''' </summary>
            Public Property Action_Date As String
        End Class
        <Serializable>
        Public Class Return_Attachment_Record
            Public Property File_Name As String
            Public Property Description As String
            Public Property NameID As String
            Public Property Applicable_From As Nullable(Of DateTime)
            Public Property Applicable_To As Nullable(Of DateTime)
            Public Property Category As String
            Public Property ID As String
            Public Property Checked As Boolean
            Public Property EditOn As String

        End Class
        <Serializable>
        Public Class Return_GetDocument_Categories
            Public Category As String
            Public ID As String
        End Class
        <Serializable>
        Public Class Return_GetDocument_Names
            Public Name As String
            Public ID As String
            Public Category As String
            Public Param_Mandatory As Boolean?
            Public From_Date_Label As String
            Public To_Date_Label As String
            Public Description_Label As String
        End Class
        <Serializable>
        Public Class Return_GetAttachmentCount
            Public PENDING As Int32
            Public ACCEPTED As Int32
            Public REJECTED As Int32
            Public TOTAL_ATTACHED As Int32
            Public TOTAL_REQD As Int32
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function Insert(ByVal InParam As Parameter_Insert_Attachment, Optional ClientScreen As ClientScreen = ClientScreen.Help_Attachments) As String
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Attachments_Insert, ClientScreen, InParam)
        End Function

        Public Function InsertLink(ByVal InParam As Parameter_Insert_Attachment_Link) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Attachments_InsertLink, ClientScreen.Help_Attachments, InParam)
        End Function

        Public Function GetList(Optional RefID As String = "", Optional CenID As String = "") As List(Of Attachment_List)
            Dim param = New Parameter_Attachment_GetList()
            param.RefId = RefID
            If CenID.Length > 0 Then
                param.openCenID = CenID
            Else
                param.openCenID = cBase._open_Cen_ID
            End If

            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Attachments_GetList, ClientScreen.Help_Attachments, param)
            Dim _List As List(Of Attachment_List) = New List(Of Attachment_List)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Attachment_List
                    If row.Table.Columns.Contains("Screen") Then
                        newdata.Screen = row.Field(Of String)("Screen")
                    End If
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Date = row.Field(Of Nullable(Of DateTime))("Action Date")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Add_Date = row.Field(Of Nullable(Of DateTime))("Add Date")
                    newdata.Applicable_From = row.Field(Of Nullable(Of DateTime))("Applicable From")
                    newdata.Applicable_To = row.Field(Of Nullable(Of DateTime))("Applicable To")
                    newdata.Attachment = row.Field(Of String)("Attachment")
                    newdata.Checked = row.Field(Of Boolean?)("Checked")
                    newdata.Checking_Status = row.Field(Of String)("Checking_Status")
                    newdata.Document_Rejection_Reason = row.Field(Of String)("Rejection_Reason")
                    newdata.Description = row.Field(Of String)("Description")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Edit_Date = row.Field(Of Nullable(Of DateTime))("Edit Date")
                    newdata.File_Name = row.Field(Of String)("File Name")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.IsBanner = row.Field(Of Boolean?)("Isbanner")
                    newdata.UniqueID = row.Field(Of String)("UniqueID")
                    newdata.Only_Description = row.Field(Of String)("Only Description")
                    newdata.Misc_Rec_ID = row.Field(Of String)("AI_CATEGORY")
                    newdata.AI_RATING = row.Field(Of Integer)("AI_RATING")
                    newdata.AI_CEN_RATING = row.Field(Of Integer)("AI_CEN_RATING")
                    If row.Table.Columns.Contains("REF_ID") Then
                        newdata.RefID = row.Field(Of String)("REF_ID")
                    End If

                    _List.Add(newdata)
                Next
            End If
            Return _List
        End Function
        ''' <summary>
        ''' Returns name of screen where Attachment has been linked
        ''' </summary>
        ''' <param name="inparam"></param>
        ''' <returns></returns>
        Public Function GetAttachmentLinkScreen(inparam As Parameter_GetAttachmentLinkCount) As String
            Return CStr(GetScalarBySP(RealServiceFunctions.Attachments_GetAttachmentLinkCount, ClientScreen.Help_Attachments, inparam))
        End Function

        Public Function GetDocument_Categories() As List(Of Return_GetDocument_Categories)
            Dim retTable As DataTable = GetMisc_Common("", "", ClientScreen.Help_Attachments, "category") ' Fetches categories
            Dim _List As List(Of Return_GetDocument_Categories) = New List(Of Return_GetDocument_Categories)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocument_Categories
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.ID = row.Field(Of String)("ID")
                    _List.Add(newdata)
                Next
            End If
            Return _List
        End Function

        Public Function GetDocument_Names(ByVal Category As String) As List(Of Return_GetDocument_Names)
            Dim retTable As DataTable = GetMisc_Common("", "", ClientScreen.Help_Attachments, Category)
            Dim _List As List(Of Return_GetDocument_Names) = New List(Of Return_GetDocument_Names)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocument_Names
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.Param_Mandatory = row.Field(Of Boolean?)("AD_PROP_LABEL_MANDATORY")
                    newdata.From_Date_Label = row.Field(Of String)("AD_PROP_LABEL_FROM_DATE")
                    newdata.To_Date_Label = row.Field(Of String)("AD_PROP_LABEL_TO_DATE")
                    newdata.Description_Label = row.Field(Of String)("AD_PROP_LABEL_DESCRIPTION")
                    _List.Add(newdata)
                Next
            End If
            Return _List
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As Return_Attachment_Record
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Attachments_GetRecord, ClientScreen.Help_Attachments, Rec_ID)
            Dim newdata = New Return_Attachment_Record
            If (Not (retTable) Is Nothing) Then
                Dim row As DataRow = retTable.Rows(0)
                newdata.NameID = row.Field(Of String)("AI_CATEGORY")
                newdata.Category = row.Field(Of String)("CATEGORY")
                newdata.Applicable_From = row.Field(Of Nullable(Of DateTime))("AI_APPLICABLE_FROM")
                newdata.Applicable_To = row.Field(Of Nullable(Of DateTime))("AI_APPLICABLE_TO")
                newdata.Description = row.Field(Of String)("AI_DESCRIPTION")
                newdata.File_Name = row.Field(Of String)("AI_FILE_NAME")
                newdata.ID = row.Field(Of String)("REC_ID")
                If IsDBNull(retTable.Rows(0)("AI_CHECKED")) Then
                    newdata.Checked = False
                Else
                    newdata.Checked = row.Field(Of Boolean)("AI_CHECKED")
                End If



                newdata.EditOn = row.Field(Of Nullable(Of DateTime))("REC_EDIT_ON")
            End If
            Return newdata
        End Function

        Public Function GetAttachmentCount() As Return_GetAttachmentCount
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Attachments_GetAttachmentCount, ClientScreen.Help_Attachments)
            Dim newdata = New Return_GetAttachmentCount
            If (Not (retTable) Is Nothing) Then
                Dim row As DataRow = retTable.Rows(0)
                newdata.PENDING = row.Field(Of Int32)("PENDING")
                newdata.REJECTED = row.Field(Of Int32)("REJECTED")
                newdata.ACCEPTED = row.Field(Of Int32)("ACCEPTED")
                newdata.TOTAL_REQD = row.Field(Of Int32)("TOTAL_REQD")
                newdata.TOTAL_ATTACHED = newdata.ACCEPTED + newdata.REJECTED + newdata.PENDING
            End If
            Return newdata
        End Function
        Public Function GetAttachmentIDBySrno(SrNo As String) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim AttachID As String = ""
            Dim _table As DataTable = _RealService.List(Tables.ATTACHMENT_INFO, "SELECT REC_ID FROM attachment_info WHERE AI_YR_SRNO = " + SrNo + "  AND AI_CEN_ID = " + cBase._open_Cen_ID.ToString() + " AND AI_COD_YEAR_ID =  " + cBase._open_Year_ID.ToString() + "", Tables.ATTACHMENT_INFO.ToString(), GetBaseParams(ClientScreen.Help_Attachments))
            If _table.Rows.Count > 0 Then
                AttachID = _table.Rows(0)(0).ToString()
            End If
            Return AttachID
        End Function
        Public Function GetAttachmentIDByFileName(Filename As String) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim AttachID As String = ""
            Dim _table As DataTable = _RealService.List(Tables.ATTACHMENT_INFO, "SELECT REC_ID FROM attachment_info WHERE AI_FILE_NAME = '" + Filename + "'  AND AI_CEN_ID = " + cBase._open_Cen_ID.ToString() + " AND AI_COD_YEAR_ID =  " + cBase._open_Year_ID.ToString() + "", Tables.ATTACHMENT_INFO.ToString(), GetBaseParams(ClientScreen.Help_Attachments))
            If _table.Rows.Count > 0 Then
                AttachID = _table.Rows(0)(0).ToString()
            End If
            Return AttachID
        End Function
        Public Function Delete_attachment(ByVal FileName As String) As Boolean
            ExecuteGroup(RealServiceFunctions.Attachments_Delete, ClientScreen.Help_Attachments, FileName)
            Return True
        End Function

        Public Function Unlink_attachment(ByVal Ref_Id As String, ByVal Attachment_Id As String) As Boolean
            Dim InParam As New RealTimeService.Parameter_Attachment_Unlink
            InParam.AttachmentID = Attachment_Id
            InParam.Ref_Rec_ID = Ref_Id
            ExecuteGroup(RealServiceFunctions.Attachments_Unlink, ClientScreen.Help_Attachments, InParam)
            Return True
        End Function

        Public Function Unlink_aLL(ByVal Attachment_Id As String) As Boolean
            'DeleteByCondition("ALI_ATTACHMENT_ID = '" & Attachment_Id & "'", Tables.ATTACHMENT_REFERENCE_INFO, ClientScreen.Help_Attachments)

            Dim InParam As New RealTimeService.Parameter_Attachment_Unlink
            InParam.AttachmentID = Attachment_Id
            InParam.Ref_Rec_ID = "" 'if this is blank then function unlinks all attachments for given attachment ID
            ExecuteGroup(RealServiceFunctions.Attachments_Unlink, ClientScreen.Help_Attachments, InParam)
            Return True
        End Function
        Public Function Delete_By_Description(ByVal Description As String) As Boolean
            'DeleteByCondition("ALI_ATTACHMENT_ID = '" & Attachment_Id & "'", Tables.ATTACHMENT_REFERENCE_INFO, ClientScreen.Help_Attachments)
            ExecuteGroup(RealServiceFunctions.Attachments_Delete_Attachment_ByDescription, ClientScreen.Help_Attachments, Description)
            Return True
        End Function
        Public Function Mark_Unchecked_attachment(ByVal ID As String) As Boolean
            Return UpdateRecord(RealServiceFunctions.Attachments_Mark_as_Unchecked, ClientScreen.Help_Attachments, ID)
        End Function

        Public Function Mark_Accepted_attachment(ByVal ID As String) As Boolean
            Return UpdateRecord(RealServiceFunctions.Attachments_Mark_as_Checked, ClientScreen.Help_Attachments, ID)
        End Function
        Public Function Mark_Rejected_attachment(ByVal ID As String, ByVal RejectionReason As String) As Boolean
            Dim Param As New Parameter_Attachment_Mark_as_Rejected()
            Param.AttachmentID = ID
            Param.RejectionReason = RejectionReason
            Return UpdateRecord(RealServiceFunctions.Attachments_Mark_as_Rejected, ClientScreen.Help_Attachments, Param)
        End Function
        Public Overloads Function MarkAsUnlocked(ByVal Rec_Id As String) As Boolean
            Dim Completed As Boolean = MyBase.MarkAsComplete(Rec_Id, Tables.ATTACHMENT_INFO, ClientScreen.Help_Attachments)
            Return Completed
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.ATTACHMENT_INFO, ClientScreen.Help_Attachments)
            Return Locked
        End Function
        Public Function Update(ByVal Inparam As Parameter_Update_Attachment, Optional ClientScreen As ClientScreen = ClientScreen.Help_Attachments) As Boolean
            Return UpdateRecord(RealServiceFunctions.Attachments_Update, ClientScreen, Inparam)
        End Function

        Public Function Download_File(ByVal DownloadFileName As String) As Byte()
            Return GetSingleValue_Data(RealServiceFunctions.Attachments_DownloadFile, ClientScreen.Help_Attachments, DownloadFileName)
        End Function
        Public Function Update_Attachment_Caption_And_CentreRating(Recid As String, Caption As String, Rating As Int32) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_Attachment_Rating_Caption"
            Dim params() As String = {"@REC_ID", "@CAPTION", "@RATING", "@RATING_TYPE"}
            Dim values() As Object = {Recid, Caption, Rating, "CENTRE_RATING"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {255, 4000, 4, 100}
            Return _RealService.UpdateBySPPublic(Tables.ATTACHMENT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function

        Public Function Update_Attachment_Caption_And_ExaminerRating(Recid As String, Caption As String, Rating As Int32) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_Attachment_Rating_Caption"
            Dim params() As String = {"@REC_ID", "@CAPTION", "@RATING", "@RATING_TYPE"}
            Dim values() As Object = {Recid, Caption, Rating, "NON_CENTRE_RATING"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {255, 4000, 4, 100}
            Return _RealService.UpdateBySPPublic(Tables.ATTACHMENT_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
    End Class
    End Class