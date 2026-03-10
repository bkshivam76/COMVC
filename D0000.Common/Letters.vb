'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class Letters
        Inherits SharedVariables
#Region "Parameter Classes"
        <Serializable>
        Public Class Return_LetterInfo
            Inherits CommonReturnFields
            Public Property Institute As String
            ''' <summary>
            ''' Original Column Name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property LetterDate As Date
            Public Property Reference As String
            Public Property Language As String
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

        'Shifted
        Public Function GetInstitutes() As DataTable
            Return GetInstituteList(ClientScreen.Facility_Letter)
        End Function

        'Shifted
        Public Function GetInstDetails(ByVal InsID As String) As DataTable
            Return GetInstituteDetails(InsID, ClientScreen.Facility_Letter)
        End Function

        'Shifted
        Public Function GetCenterDetails() As DataTable
            Return GetCenterDetailsForLetterPAD(ClientScreen.Facility_Letter)
        End Function


        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Facility_Letter, RealTimeService.Tables.LETTER_INFO, Common.ClientDBFolderCode.SYS)
        End Function
        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Letters_GetList</remarks>
        Public Function GetList() As List(Of Return_LetterInfo)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Letters_GetList, ClientScreen.Facility_Letter)
            Dim _Letter As List(Of Return_LetterInfo) = New List(Of Return_LetterInfo)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_LetterInfo
                    newdata.Institute = row.Field(Of String)("Institute")
                    newdata.LetterDate = row.Field(Of Date)("Date")
                    newdata.Reference = row.Field(Of String)("Reference")
                    newdata.Language = row.Field(Of String)("Language")
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

                    _Letter.Add(newdata)
                Next
            End If
            Return _Letter
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Letters_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Letters) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Letters_Insert, ClientScreen.Facility_Letter, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Letters_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Letters) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Letters_Update, ClientScreen.Facility_Letter, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.LETTER_INFO, ClientScreen.Facility_Letter)
        End Function
    End Class
#End Region
End Class
