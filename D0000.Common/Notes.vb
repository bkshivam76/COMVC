'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class Notes
        Inherits SharedVariables

#Region "Parameter Classes"
        <Serializable>
        Public Class Return_NotesInfo
            Inherits CommonReturnFields
            Public Property Notes As String
            Public Property Status As String
            Public Property iREQ_ATTACH_COUNT As Int32?
            Public Property iCOMPLETE_ATTACH_COUNT As Int32?
            Public Property iRESPONDED_COUNT As Int32?
            Public Property iREJECTED_COUNT As Int32?
            Public Property iOTHER_ATTACH_CNT As Int32?
            Public Property iALL_ATTACH_CNT As Int32?

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
        ''' <remarks>RealServiceFunctions.Notes_GetList</remarks>
        Public Function GetList() As List(Of Return_NotesInfo)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Notes_GetList, ClientScreen.Facility_Notes)
            Dim _notes As List(Of Return_NotesInfo) = New List(Of Return_NotesInfo)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_NotesInfo
                    newdata.Notes = row.Field(Of String)("Notes")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
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

        ''' <summary>
        ''' Gets ShortList, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_GetShortList</remarks>
        Public Function GetShortList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Notes_GetShortList, ClientScreen.Facility_Notes)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Facility_Notes, RealTimeService.Tables.NOTES_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        ''' <summary>
        ''' Add Quick Note, Shifted
        ''' </summary>
        ''' <param name="Note"></param>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_AddQuickNote</remarks>
        Public Function AddQuickNote(ByVal Note As String, ByVal Rec_ID As String) As Boolean
            Dim Param As Param_AddQuickNote_Notes = New Param_AddQuickNote_Notes()
            Param.Note = Note
            Param.Rec_ID = Rec_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Notes_AddQuickNote, ClientScreen.Facility_Notes, Param)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Notes) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Notes_Insert, ClientScreen.Facility_Notes, InParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Complete</remarks>
        Public Function Complete(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Notes_Complete, ClientScreen.Facility_Notes, Rec_ID)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Incomplete</remarks>
        Public Function Incomplete(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Notes_Incomplete, ClientScreen.Facility_Notes, Rec_ID)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Notes) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Notes_Update, ClientScreen.Facility_Notes, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.NOTES_INFO, ClientScreen.Facility_Notes)
        End Function

    End Class
#End Region
End Class
