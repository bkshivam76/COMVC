Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Options"
    <Serializable>
    Public Class Maintenance
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function AllSyncLogs() As DataTable
            Return GetSyncLogs()
        End Function

        Public Function RemoveSyncLogs() As Boolean
            Return CleanSyncLogs()
        End Function
    End Class
#End Region
End Class
