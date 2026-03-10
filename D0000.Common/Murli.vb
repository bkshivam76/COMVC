'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Murli
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Murli_GetList</remarks>
        Public Function GetList() As DataTable
            Dim Query As String = "select *  from MURLI_INFO WHERE  REC_STATUS IN (0,1,2) AND day(M_DATE) = " & Now.Day & " AND Month(M_DATE) = " & Now.Month & " AND YEAR(M_DATE) = " & Now.Year
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Murli_GetList, Query, ClientScreen.Home_Murli, RealTimeService.Tables.MURLI_INFO)
        End Function
    End Class
End Class
