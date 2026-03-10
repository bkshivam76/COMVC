'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class Return_Payment_Telephone_Select
        Public TP_ID As String
        Public TP_TYPE As String
        Public TP_CATEGORY As String
        Public TP_COMPANY As String
        Public TP_NO As String
    End Class
    <Serializable>
    Public Class Telephones
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetTelecomCompanies(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As DataTable
            Return GetMisc("TELECOM COMPANY", ClientScreen.Profile_Telephone, NameColumnHead, RecIdColumnHead)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_GetList</remarks>
        Public Function GetList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Telephone_GetList, ClientScreen.Profile_Telephone)
        End Function

        Public Function GetListByCondition(ByVal screen As ClientScreen, ByVal OtherCondition As String) As List(Of Return_Payment_Telephone_Select)
            Dim ret_table As DataTable= GetDataListOfRecords(RealTimeService.RealServiceFunctions.Telephone_GetListByCondition, screen, OtherCondition)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                Dim telelist = New List(Of Return_Payment_Telephone_Select)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_Payment_Telephone_Select
                    newdata.TP_ID = row.Field(Of String)("TP_ID")
                    newdata.TP_TYPE = row.Field(Of String)("TP_TYPE")
                    newdata.TP_CATEGORY = row.Field(Of String)("TP_CATEGORY")
                    newdata.TP_COMPANY = row.Field(Of String)("TP_COMPANY")
                    newdata.TP_NO = row.Field(Of String)("TP_NO")
                    telelist.Add(newdata)
                Next
                Return telelist
            End If
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Telephone, RealTimeService.Tables.TELEPHONE_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        Public Function GetLastEntryDate(ByVal Tel_Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Telephone_GetLastEntryDate, ClientScreen.Profile_Telephone, Tel_Rec_Id)
        End Function

        ''' <summary>
        ''' Get Record By TeleNumber, Shifted
        ''' </summary>
        ''' <param name="Tel_No"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_GetRecordByTeleNumber</remarks>
        Public Function GetRecordByTeleNumber(ByVal Tel_No As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Telephone_GetRecordByTeleNumber, ClientScreen.Profile_Telephone, Tel_No)
        End Function

        Public Function GetCountInTxn(ByVal screen As ClientScreen, ByVal TP_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Telephone_GetCountInTxn, screen, TP_ID)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Telephone, RealTimeService.Tables.TELEPHONE_INFO, "TP_CEN_ID")
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Telephones) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Telephone_Insert, ClientScreen.Profile_Telephone, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Telephones) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Telephone)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Telephone_Update, ClientScreen.Profile_Telephone, UpParam)
        End Function

        Public Function Close(ByVal UpParam As Parameter_Close_Telephones) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Telephone_Close, ClientScreen.Profile_Telephone, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Telephone)

            Return DeleteRecord(Rec_Id, Tables.TELEPHONE_INFO, ClientScreen.Profile_Telephone)
        End Function

        ' ''' <summary>
        ' ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ' ''' </summary>
        ' ''' <param name="defaultLocationID"></param>
        ' ''' <remarks>RealServiceFunctions.Telephone_UpdateAssetLocationIfNotPresent</remarks>
        'Public Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String) As Boolean
        '    Return UpdateRecord(RealTimeService.RealServiceFunctions.Telephone_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        'End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.TELEPHONE_INFO, ClientScreen.Profile_Telephone)
        End Function

    End Class
#End Region
End Class
