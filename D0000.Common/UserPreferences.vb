Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class UserPreferences
        Inherits SharedVariables
        ''' <summary>
        ''' These are Types of possible Data Restrictions that can be put in Connectone 
        ''' </summary>

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function AutoOpenScreen_GetList() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@USERID", "@LoggedInUserTtype", "@YEAR_ID", "@CEN_ID"}
            Dim values() As Object = {cBase._open_User_ID, cBase._open_User_Type, cBase._open_Year_ID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {255, 50, 4, 4}
            Return _RealService.ListFromSP(Tables.SO_USER_AUTO_OPEN_SCREENS, "[sp_get_Auto_Open_Screens]", Tables.SO_USER_AUTO_OPEN_SCREENS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function GetSelectedScreens_DataView() As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@USER_ID"}
            Dim values() As Object = {cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {255}
            Return _RealService.ListDatasetFromSP(Tables.SO_USER_LISTING_SCREEN_PREFERENCE, "[sp_get_listing_screen_preference]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function InsertDataViewOption(ByVal ScreenID As String, ByVal ScreenPreference As String, ByVal DeviceType As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_listing_screen_preference"
            Dim params() As String = {"@USER_ID", "@SCREEN_ID", "@SCREEN_PREFERENCE", "@DEVICE_TYPE"}
            Dim values() As Object = {cBase._open_User_ID, ScreenID, ScreenPreference, DeviceType}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 255, 255, 255}
            'used public insert function as there are no transactional data involved 
            Return _RealService.InsertBySPPublic(Tables.SO_USER_LISTING_SCREEN_PREFERENCE, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function Delete_DataView() As Boolean
            Return DeleteByCondition("LSP_USER_ID  = '" + cBase._open_User_ID + "'", Tables.SO_USER_LISTING_SCREEN_PREFERENCE, ClientScreen.Option_UserPreferences)
        End Function
        Public Function InsertAutoOpenScreen(ByVal ScreenId As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_Insert_Auto_Open_Screens"
            Dim params() As String = {"@USERID", "@SCREENID"}
            Dim values() As Object = {cBase._open_User_ID, ScreenId}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 100}
            'used public insert function as there are no transactional data involved 
            Return _RealService.InsertBySPPublic(Tables.SO_USER_AUTO_OPEN_SCREENS, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function Delete() As Boolean
            Return DeleteByCondition("USERID  = '" + cBase._open_User_ID + "'", Tables.SO_USER_AUTO_OPEN_SCREENS, ClientScreen.Option_UserPreferences)
        End Function

    End Class
End Class