'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Options"
    <Serializable>
    Public Class DocLibrary
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDocuments() As DataTable
            Dim LocalQuery As String = "SELECT MISC_NAME AS Category,MISC_REMARK1 AS Title,MISC_REMARK2 as Filename,MISC_SRNO AS Sr,REC_ID AS ID FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('DOCUMENT LIBRARY')  ORDER BY val(MISC_SRNO)"
            Dim OnlineQuery As String = "SELECT MISC_NAME AS Category,MISC_REMARK1 AS Title,MISC_REMARK2 as Filename,MISC_SRNO AS Sr,REC_ID AS ID FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('DOCUMENT LIBRARY')  ORDER BY CAST(MISC_SRNO AS DECIMAL)"
            Return GetMiscDetails(OnlineQuery, LocalQuery, ClientScreen.Options_DocLibrary)
        End Function
    End Class
#End Region
End Class
