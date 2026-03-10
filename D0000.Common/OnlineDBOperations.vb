Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class OnlineDBOperations

        ''' <summary>
        ''' New private function to call Core List Functions
        ''' </summary>
        ''' <param name="rFunction"></param>
        ''' <param name="param"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CallCoreListFunctions(ByVal RealService As Common_Lib.RealTimeService.ConnectOneWS, ByVal cBase As Common_Lib.Common, ByVal rFunction As RealTimeService.RealServiceFunctions, ByVal param As Object, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Dim _dbOps As SharedVariables = New SharedVariables(cBase)
            If param Is Nothing Then
                Return _dbOps.Decompress_Data(RealService.Wrap_List(rFunction, _dbOps.GetBaseParams(screen)))
            Else
                Return _dbOps.Decompress_Data(RealService.Wrap_List(rFunction, _dbOps.GetBaseParams(screen), param))
            End If

        End Function


        ''' <summary>
        ''' New private function to call Core Single Value Functions
        ''' </summary>
        ''' <param name="rFunction"></param>
        ''' <param name="param"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CallCoreSingleValueFunctions(ByVal RealService As Common_Lib.RealTimeService.ConnectOneWS, ByVal cBase As Common_Lib.Common, ByVal rFunction As RealTimeService.RealServiceFunctions, ByVal param As Object, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Object
            Dim _dbOps As SharedVariables = New SharedVariables(cBase)
            Return RealService.Wrap_GetSingleValue(rFunction, _dbOps.GetBaseParams(screen), param)
        End Function

    End Class
End Class
