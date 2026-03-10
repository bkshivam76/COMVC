Imports System.Data

Namespace Real
    <Serializable>
    Public Class Consumables
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetSummary
            Public _YearID As Integer
        End Class
        <Serializable>
        Public Class Param_GetYearEndingCount
            Public BK_Pad_No As String
            Public Year_EDT As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_ConsumableStock
            Public ItemID As String
            Public CSDate As String
            Public Amount As Double
            Public LocationID As String
            Public OtherDetails As String
            Public Status_Action As String
            Public openYearID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_ConsumableStock
            Public ItemID As String
            Public CSDate As String
            Public Amount As Double
            Public LocationID As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
#End Region
       
        'Public Shared Function GetList(ByVal Screen As ConnectOneWS.ClientScreen, ByVal openUserID As String, ByVal openCenID As String, ByVal PCID As String, ByVal version As String) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim OnlineQuery As String = " SELECT  CS_AMOUNT AS Amount,CS_DATE ,CS_MISC_ID,CS_OTHER_DETAIL,CS_LOC_AL_ID,REC_ID AS ID ," & Common.Remarks_Detail("Consumables_Stock_Info", DataFunctions.GetCurrentDateTime(ConnectOneWS.ClientScreen.Profile_StockOfConsumables, openUserID, openCenID, PCID, version), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Consumables_Stock_Info") & "" & _
        '                         " FROM Consumables_Stock_Info " & _
        '                         " Where   REC_STATUS IN (0,1,2) AND CS_CEN_ID='" & openCenID & "' ; "
        '    'Dim LocalQuery As String = " SELECT  CS_AMOUNT AS Amount,CS_DATE ,CS_MISC_ID,CS_OTHER_DETAIL,CS_LOC_AL_ID,REC_ID AS ID ," & cBase.Remarks_Detail("Consumables_Stock_Info", True, GetCurrentDateTime(ClientScreen.Profile_StockOfConsumables)) & "," & cBase.Rec_Detail("Consumables_Stock_Info", Common.DbConnectionMode.Local) & "" & _
        '    '                   " FROM Consumables_Stock_Info " & _
        '    '                   " Where   REC_STATUS IN (0,1,2) AND CS_CEN_ID='" & cBase._open_Cen_ID & "' ; "
        '    'Return GetListOfRecords(OnlineQuery, LocalQuery, ClientScreen.Profile_StockOfConsumables, RealTimeService.Tables.CONSUMABLES_STOCK_INFO, Common.ClientDBFolderCode.DATA)
        '    Return dbService.List(openUserID, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, openCenID, OnlineQuery, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO.ToString(), Screen, PCID, version)
        'End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  AL.AL_LOC_NAME AS 'Location', MI.MISC_NAME AS 'Item Name', CS.CS_OTHER_DETAIL as 'Other Details',CS_DATE AS 'Date' ,CS_AMOUNT AS 'Amount',CS.REC_ID AS ID, " & Common.Remarks_Detail("CS", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("CS") & "" & _
                                  " , CS_COD_YEAR_ID AS YearID FROM Consumables_Stock_Info    AS CS    " & _
                                  " INNER JOIN Asset_Location_Info AS AL ON (CS.CS_LOC_AL_ID = AL.REC_ID AND AL.AL_CEN_ID =" & inBasicParam.openCenID.ToString & " )" & _
                                  " INNER JOIN Misc_Info           AS MI ON (CS.CS_MISC_ID = MI.REC_ID    )" & _
                                  " Where   CS.REC_STATUS IN (0,1,2) AND CS.CS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND CS_COD_YEAR_ID IN (" & inBasicParam.openYearID.ToString & "," & Convert.ToString(Convert.ToInt32(inBasicParam.openYearID.ToString.Substring(0, 2)) - 1) & Convert.ToString(Convert.ToInt32(inBasicParam.openYearID.ToString.Substring(2, 2)) - 1) & ") " & _
                                  " AND CS_DATE > '20" + Convert.ToString(Convert.ToInt32(inBasicParam.openYearID.ToString.Substring(0, 2)) - 1) & "-03-31' AND (CS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = CS_CEN_ID) OR CS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " )" & _
                                  " ORDER BY  AL.AL_LOC_NAME, MI.MISC_NAME,CS.CS_OTHER_DETAIL,CS.CS_DATE "
            Return dbService.List(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, Query, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_Summary(ByVal param As Consumables.Param_GetSummary, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            'Dim Query As String = " SELECT  MAX(CS.CS_LOC_AL_ID) AS 'LOC_ID',AL.AL_LOC_NAME AS 'LOC_NAME',MAX(CS.CS_MISC_ID) AS 'MISC_ID',MI.MISC_NAME AS 'Item Name', CS.CS_OTHER_DETAIL as 'Other Details', " & _
            '                      " SUM(CASE WHEN CAST(CS_DATE AS DATE)  = '" & param._Start_Fin_Date.Year & "-03-31' THEN CS_AMOUNT ELSE 0 END) as 'Amount " & param._Start_Fin_Date.Year - 1 & "-" & Format(param._Start_Fin_Date, "yy") & "'," & _
            '                      " SUM(CASE WHEN CAST(CS_DATE AS DATE)  = '" & param._End_Fin_Date.Year & "-03-31'   THEN CS_AMOUNT ELSE 0 END) as 'Amount " & param._Start_Fin_Date.Year & "-" & Format(param._End_Fin_Date, "yy") & "' " & _
            '                      " FROM Consumables_Stock_Info    AS CS    " & _
            '                      " INNER JOIN Asset_Location_Info AS AL ON (CS.CS_LOC_AL_ID = AL.REC_ID AND AL.AL_CEN_ID ='" & openCenID & "' )" & _
            '                      " INNER JOIN Misc_Info           AS MI ON (CS.CS_MISC_ID = MI.REC_ID    )" & _
            '                      " Where   CS.REC_STATUS IN (0,1,2) AND CS.CS_CEN_ID='" & openCenID & "' AND CS.CS_COD_YEAR_ID ='" & param._YearID & "' " & _
            '                      " GROUP BY  AL.AL_LOC_NAME, MI.MISC_NAME,CS.CS_OTHER_DETAIL "

            ''                     " SUM(CASE WHEN CAST(CS_DATE AS DATE)  BETWEEN '" & param._Start_Fin_Date.Year - 1 & "-04-01' AND '" & param._Start_Fin_Date.Year & "-03-31' THEN CS_AMOUNT ELSE 0 END) as 'Amount " & param._Start_Fin_Date.Year - 1 & "-" & Format(param._Start_Fin_Date, "yy") & "'," & _
            ''                     " SUM(CASE WHEN CAST(CS_DATE AS DATE)  BETWEEN '" & param._Start_Fin_Date.Year & "-04-01' AND '" & param._End_Fin_Date.Year & "-03-31' THEN CS_AMOUNT ELSE 0 END) as 'Amount " & param._Start_Fin_Date.Year & "-" & Format(param._End_Fin_Date, "yy") & "' " & _



            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Store Procedure:  EXEC	Get_Stock_of_Consumables_Summary	@X_CEN_ID = '00207', @X_YEAR_ID = '1112'
            Dim SPName As String = "Get_Stock_of_Consumables_Summary"
            Dim params() As String = {"X_CEN_ID", "X_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, param._YearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, SPName, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetYearEndingCount(ByVal Param As Param_GetYearEndingCount, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COUNT(CS_DATE) FROM consumables_stock_info AS CS INNER JOIN centre_info AS CI ON CS.CS_CEN_ID = CI.CEN_ID WHERE CAST(CS_DATE AS DATE) = '" & Param.Year_EDT & "' AND CEN_BK_PAD_NO ='" & Param.BK_Pad_No & "' AND CEN_INS_ID = '00001'"
            Return dbService.GetScalar(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, Query, "consumables_stock_info", inBasicParam)
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_UpdateAssetLocationIfNotPresent</remarks>
        Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE CONSUMABLES_STOCK_INFO SET CS_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(CS_LOC_AL_ID,'') =''"
            dbService.Update(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_ConsumableStock, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Consumables_Stock_Info(CS_CEN_ID,CS_COD_YEAR_ID,CS_MISC_ID,CS_DATE,CS_AMOUNT,CS_LOC_AL_ID,CS_OTHER_DETAIL," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & InParam.openYearID.ToString & "," & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  " " & If(IsDate(InParam.CSDate), "'" & Convert.ToDateTime(InParam.CSDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                  " " & InParam.Amount & ", " & _
                                                  "'" & InParam.LocationID & "', " & _
                                                  "'" & InParam.OtherDetails & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Updates Consumables Stock Info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_ConsumableStock, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Consumables_Stock_Info SET " & _
                                         "CS_MISC_ID        ='" & UpParam.ItemID & "', " & _
                                         "CS_DATE           = " & If(IsDate(UpParam.CSDate), " '" & Convert.ToDateTime(UpParam.CSDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "CS_AMOUNT         = " & UpParam.Amount & ", " & _
                                         "CS_LOC_AL_ID      ='" & UpParam.LocationID & "', " & _
                                         "CS_OTHER_DETAIL   ='" & UpParam.OtherDetails & "', " & _
                                         " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace
