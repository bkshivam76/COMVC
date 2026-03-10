'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Help"
    <Serializable>
    Public Class Request
        Inherits SharedVariables

#Region "Parameter Classes"
        <Serializable>
        Public Class Return_RequestInfo
            Inherits CommonReturnFields
            Public Property Sr As Integer
            ''' <summary>
            ''' Actual Field name is Request Detail
            ''' </summary>
            ''' <returns></returns>
            Public Property Request_Detail As String
            ''' <summary>
            ''' Actual Field name is Administrator Remarks
            ''' </summary>
            ''' <returns></returns>
            Public Property Administrator_Remarks As String
            Public Property Status As String
            Public Property Read As String
            ''' <summary>
            ''' Actual Field name is Send From
            ''' </summary>
            ''' <returns></returns>
            Public Property Send_From As String
            Public Property REQ_ATTACH_COUNT As Int32?
            Public Property COMPLETE_ATTACH_COUNT As Int32?
            Public Property RESPONDED_COUNT As Int32?
            Public Property REJECTED_COUNT As Int32?
            Public Property OTHER_ATTACH_CNT As Int32?
            Public Property ALL_ATTACH_CNT As Int32?

            'Added for Audit Icon Filter
            Public Property iIcon As String
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_GetList</remarks>
        Public Function GetList() As List(Of Return_RequestInfo)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Request_GetList, ClientScreen.Help_Request_Box)
            Dim _notes As List(Of Return_RequestInfo) = New List(Of Return_RequestInfo)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_RequestInfo
                    newdata.Sr = row.Field(Of Integer)("Sr")
                    newdata.Request_Detail = row.Field(Of String)("Request Detail")
                    newdata.Administrator_Remarks = row.Field(Of String)("Administrator Remarks")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.Read = row.Field(Of String)("Read")
                    newdata.Send_From = row.Field(Of String)("Send From")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.REQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.COMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.RESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.REJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newdata.OTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.ALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    newdata.iIcon = ""

                    If (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) = 0 AndAlso (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) = 0)) Then
                        newdata.iIcon += "GreenShield|"
                    ElseIf (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) > 0 AndAlso (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) < (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)))) Then
                        newdata.iIcon += "YellowShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) > 0)) Then
                        newdata.iIcon += "BlueShield|"
                    End If

                    If ((If(row.Field(Of Int32?)("REJECTED_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedFlag|"
                    End If

                    If (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) = 0) Then
                        newdata.iIcon += "RequiredAttachment|"
                    ElseIf (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) <> 0) Then
                        newdata.iIcon += "AdditionalAttachment|"
                    End If
                    _notes.Add(newdata)
                Next
            End If
            Return _notes
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Help_Request_Box, RealTimeService.Tables.REQUEST_BOX, Common.ClientDBFolderCode.SYS)
        End Function

        ''' <summary>
        ''' Get Unread Count, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_GetUnreadCount</remarks>
        Public Function GetUnreadCount(ByVal screen As ClientScreen) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Request_GetUnreadCount, screen)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_MarkAsRead</remarks>
        Public Function MarkAsRead(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Request_MarkAsRead, ClientScreen.Help_Request_Box, Rec_ID)
        End Function

        ''' <summary>
        ''' Marks Request As UnRead, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_MarkAsUnread</remarks>
        Public Function MarkAsUnread(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Request_MarkAsUnread, ClientScreen.Help_Request_Box, Rec_ID)
        End Function

        ''' <summary>
        ''' Inserts into request Box, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Request) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Request_Insert, ClientScreen.Help_Request_Box, InParam)
        End Function

        ''' <summary>
        ''' Updates RequestBox, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Request) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Request_Update, ClientScreen.Help_Request_Box, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.REQUEST_BOX, ClientScreen.Help_Request_Box)
        End Function
    End Class
#End Region
End Class

