Imports System.Data

Namespace Real
    <Serializable>
    Public Class Murli
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Murli_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select *  from MURLI_INFO WHERE  REC_STATUS IN (0,1,2) AND day(M_DATE) = " & Now.Day & " AND Month(M_DATE) = " & Now.Month & " AND YEAR(M_DATE) = " & Now.Year
            Return dbService.List(ConnectOneWS.Tables.MURLI_INFO, Query, ConnectOneWS.Tables.MURLI_INFO.ToString(), inBasicParam)
        End Function
    End Class
End Namespace
