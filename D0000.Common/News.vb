'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Help"
    <Serializable>
    Public Class News
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.News_GetList</remarks>
        Public Function GetList() As DataTable
            Dim LocalQuery As String = " SELECT NEWS_TITLE ,NEWS_TEXT ,NEWS_FROM,REC_ID AS ID " & _
                                    " FROM News_Info R WHERE REC_STATUS IN (0,1,2) and UCASE(NEWS_STATUS)='ON'  order by NEWS_FROM DESC"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.News_GetList, LocalQuery, ClientScreen.Help_News, Tables.NEWS_INFO)
        End Function

        Public Function GetCenterNews() As DataTable
            Dim LocalQuery As String = " SELECT NEWS_TITLE ,NEWS_TEXT ,NEWS_FROM,REC_ID AS ID " & _
                                    " FROM News_Info R WHERE REC_STATUS IN (0,1,2) and UCASE(NEWS_STATUS)='ON'  order by NEWS_FROM DESC"
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.News_GetCenterNews, ClientScreen.Help_News, Nothing)
        End Function

        ''' <summary>
        ''' Get Count, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.News_GetCount</remarks>
        Public Function GetCount() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.News_GetCount, ClientScreen.Help_News)
        End Function

        'Shifted
        Public Function GetUnreadRequestCount() As Object
            Dim _req As Request = New Request(cBase)
            Return _req.GetUnreadCount(ClientScreen.Help_News)
        End Function
    End Class
#End Region
End Class

