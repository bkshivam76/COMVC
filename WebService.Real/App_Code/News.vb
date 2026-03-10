Imports System.Data
Imports System.Xml


Namespace Real
#Region "Help"
    <Serializable>
    Public Class News
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.News_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NewsHtml As XmlDocument = New XmlDocument()
            NewsHtml.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/News.xml"))
            Dim addlst As XmlNodeList = NewsHtml.SelectNodes("News/Record")
            Dim OnlineQuery As String = " SELECT NEWS_TITLE ,NEWS_TEXT ,NEWS_FROM,REC_ID AS ID " & _
                                    " FROM News_Info R WHERE REC_STATUS IN (0,1,2) and UPPER(NEWS_STATUS)='ON'  order by NEWS_FROM DESC"
            Dim d1 As DataTable = dbService.List(ConnectOneWS.Tables.NEWS_INFO, OnlineQuery, ConnectOneWS.Tables.NEWS_INFO.ToString(), inBasicParam)
            Dim newClm As New DataColumn("NEWS_HTML", GetType(System.String))
            d1.Columns.Add(newClm)
            For Each Row As DataRow In d1.Rows
                For Each add2nd As XmlNode In addlst
                    If Row("ID").ToString = add2nd.Attributes("RecID").Value.ToString() Then
                        Row("NEWS_HTML") = UnescapeXML(add2nd.InnerXml)
                    End If
                Next
            Next
           
            Return d1
        End Function

        Public Shared Function GetCenterNews(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Center_News_Web"
            Dim params() As String = {"YearID", "CenID"}
            Dim values() As Object = {inBasicParam.openYearID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 5}
            Return dbService.ListFromSP(ConnectOneWS.Tables.NEWS_INFO, SPName, "NEWS_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Count
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.News_GetCount</remarks>
        Public Shared Function GetCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM News_Info R WHERE REC_STATUS IN (0,1,2) and UPPER(NEWS_STATUS)='ON' "
            Return dbService.GetScalar(ConnectOneWS.Tables.NEWS_INFO, OnlineQuery, ConnectOneWS.Tables.NEWS_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function UnescapeXML(str) As String
            If (String.IsNullOrEmpty(str)) Then Return str
            Dim returnString As String = str
            returnString = returnString.Replace("&apos;", "'")
            returnString = returnString.Replace("&quot;", "\")
            returnString = returnString.Replace("&gt;", ">")
            returnString = returnString.Replace("&lt;", "<")
            returnString = returnString.Replace("&amp;", "&")
            Return returnString
        End Function


    End Class
#End Region
End Namespace
