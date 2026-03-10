Imports System.Data

Namespace Real

    <Serializable>
    Public Class GoldSilver

#Region "Param Classes"
        <Serializable>
        Public Class Param_GoldSilver_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Param_GoldSilver_GetList_Common
            Public GS_Type As String
            Public Item_ID As String
            Public GS_TR_ID As String
            Public GS_Rec_ID As String
            Public Cen_ID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Insert_GoldSilver
            Public Type As String
            Public ItemID As String
            Public DescMiscID As String
            Public Weight As Double
            Public Amount As Double
            Public LocationID As String
            Public OtherDetails As String
            Public Status_Action As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRIDAndTRSRNo_GoldSilver : Inherits Parameter_Insert_GoldSilver
            Public TxnID As String
            Public TxnSrno As Integer
            Public Screen As ConnectOneWS.ClientScreen
            Public RecID As String = Guid.NewGuid.ToString
            Public UpdGsID As String ' Contains old RecId of GS that is being re-posted(updated)
        End Class
        <Serializable>
        Public Class Parameter_Update_GoldSilver
            Public Type As String
            Public ItemID As String
            Public DescMiscID As String
            Public Weight As Double
            Public LocationID As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
            Public Amount As Double = 0
        End Class
        <Serializable>
        Public Class Param_GS_GetTransactions
            Public Rec_IDs As String = ""
            Public YearID As String = ""
        End Class
#End Region

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_GoldSilver_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT '' AS 'ITEM_NAME',GS_ITEM_ID  ,GS_TYPE AS Type,'' AS MISC_NAME, GS_DESC_MISC_ID , GS_AMT, GS_ITEM_WEIGHT,GS_ITEM_WEIGHT as 'Curr Weight' ,'' AS AL_LOC_NAME, GS_LOC_AL_ID,GS_OTHER_DETAIL ,'Un-Sold' AS 'Sale Status','' as 'Sale Date',  0.0 as 'Sale Weight',GS_ITEM_WEIGHT as 'Balance Weight',  CASE WHEN  GS_TR_ID IS NULL  OR GS_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE GS_TR_ID END AS TR_ID ,GS_COD_YEAR_ID AS YearID,REC_ID AS ID , CASE WHEN GS_TR_ID IS NULL OR GS_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as 'Entry Type'  ," & Common.Remarks_Detail("GOLD_SILVER_INFO", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("GOLD_SILVER_INFO") & "" &
                                  " FROM GOLD_SILVER_INFO " &
                                  " Where   REC_STATUS IN (0,1,2) AND GS_CEN_ID=" & inBasicParam.openCenID.ToString & "  and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID)) AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            Return dbService.List(ConnectOneWS.Tables.GOLD_SILVER_INFO, onlineQuery, ConnectOneWS.Tables.GOLD_SILVER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns GoldSilver Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Assets.Param_GetProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Goldsilver_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 255}
            Return dbService.ListFromSP(Param.TableName, SPName, Param.TableName.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get List By Condition
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT GS_ITEM_ID, GS_TYPE,  GS_DESC_MISC_ID, GS_OTHER_DETAIL,GS_ITEM_WEIGHT, '' As ITEM_WEIGHT_TYPE, " &
                                 "CAST(0.00 AS DECIMAL) as TOTAL_WEIGHT, 'kg' AS UNIT_KG, 'gm' AS UNIT_GRAM, 'mg' AS UNIT_MG, '' as TOTAL_WEIGHT_UNIT,GS_LOC_AL_ID " &
                                 "FROM Gold_Silver_Info " &
                                 "Where REC_STATUS IN (0,1,2) AND GS_CEN_ID=" & inBasicParam.openCenID.ToString & "  " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.GOLD_SILVER_INFO, onlineQuery, ConnectOneWS.Tables.GOLD_SILVER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Object = Nothing) As DataTable
            Dim nextParam As Param_GoldSilver_GetList_Common = New Param_GoldSilver_GetList_Common()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    nextParam.GS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    nextParam.GS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    nextParam.GS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    nextParam.GS_Type = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    nextParam.Item_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    nextParam = Param
            End Select
            Return GetList_Common_ByRecID(inBasicParam, nextParam)
        End Function

        Public Shared Function GetList_Common_ByRecID(inBasicParam As ConnectOneWS.Basic_Param, ByVal Param As Param_GoldSilver_GetList_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = "select GS_TR_ID,GS_TR_ITEM_SRNO,GS_DESC_MISC_ID, GS_ITEM_WEIGHT, GS_LOC_AL_ID from Gold_Silver_Info where  REC_STATUS IN (0,1,2) AND GS_TR_ID= '" & Param.GS_TR_ID & "' ORDER BY GS_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = "select GS_TR_ID,GS_TR_ITEM_SRNO,GS_DESC_MISC_ID, GS_ITEM_WEIGHT, GS_LOC_AL_ID, REC_ID from Gold_Silver_Info where  REC_STATUS IN (0,1,2) AND GS_TR_ID= '" & Param.GS_TR_ID & "' ORDER BY GS_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = "SELECT  '' AS 'REF_ITEM',GS_ITEM_ID AS 'REF_ITEM_ID',GS_ITEM_WEIGHT AS 'REF_QTY','' as 'REF_DESC',GS_DESC_MISC_ID AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',REC_EDIT_ON  FROM GOLD_SILVER_INFO  Where   REC_STATUS IN (0,1,2) AND GS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND GS_TYPE in ('" & Param.GS_Type & "')  and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID)) AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") " 'GS_Type
                    If Not Param.GS_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.GS_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "SELECT GS_TYPE as 'Item', COALESCE(GS_ITEM_WEIGHT,0)  as 'Org Weight', COALESCE(GS_ITEM_WEIGHT,0)  as 'Curr Weight',MI.MISC_NAME AS 'DESC', GS_OTHER_DETAIL AS DETAILS,COALESCE(GS_AMT,0) AS 'Org Value' ,COALESCE(GS_AMT,0) AS 'Curr Value' ,GS.REC_ID FROM gold_silver_info AS GS INNER JOIN misc_info AS MI ON GS.GS_DESC_MISC_ID = MI.REC_ID WHERE GS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND GS_ITEM_ID = '" & Param.Item_ID & "' and GS.REC_STATUS IN (0,1,2)  and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID)) AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.GS_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND GS.REC_ID = '" & Param.GS_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = "SELECT  '' AS 'REF_ITEM',GS_ITEM_ID AS 'REF_ITEM_ID',GS_ITEM_WEIGHT AS 'REF_QTY','' as 'REF_DESC',GS_DESC_MISC_ID AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',GS_LOC_AL_ID AS REF_LOC_ID, REC_EDIT_ON  FROM GOLD_SILVER_INFO  Where   REC_STATUS IN (0,1,2) AND GS_CEN_ID=" & Param.Cen_ID.ToString & " AND GS_TYPE in ('" & Param.GS_Type & "') and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID)) AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.GS_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.GS_Rec_ID & "' "
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.GOLD_SILVER_INFO, Query, ConnectOneWS.Tables.GOLD_SILVER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transactions
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_GetTransactions</remarks>
        Public Shared Function GetTransactions(ByVal inParam As Param_GS_GetTransactions, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT TP.TR_REF_ID, SUM(TM.TR_SALE_QTY) AS 'Sale Weight',MAX(TM.TR_SALE_DATE) AS 'Sale Date'  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN (" & inParam.Rec_IDs & ") AND TM.TR_COD_YEAR_ID = " & inParam.YearID.ToString & " AND TM.TR_CODE=11   GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.GoldSilver_UpdateAssetLocationIfNotPresent</remarks>
        Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim localQuery As String = " UPDATE GOLD_SILVER_INFO       SET GS_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = #" & Now.ToString(Common.Date_Format_Long) & "#  WHERE IIF(ISNULL(GS_LOC_AL_ID),'',GS_LOC_AL_ID)=''"
            Dim OnlineQuery As String = " UPDATE GOLD_SILVER_INFO SET GS_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(GS_LOC_AL_ID,'')=''"
            dbService.Update(ConnectOneWS.Tables.GOLD_SILVER_INFO, OnlineQuery, inBasicParam)
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
        ''' <remarks>RealServiceFunctions.GoldSilver_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_GoldSilver, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            If InParam.DescMiscID.Trim.Length = 0 Then InParam.DescMiscID = "NULL" Else InParam.DescMiscID = "'" & InParam.DescMiscID & "'"
            Dim OnlineQuery As String = "INSERT INTO GOLD_SILVER_INFO(GS_CEN_ID,GS_COD_YEAR_ID,GS_TYPE,GS_ITEM_ID,GS_DESC_MISC_ID,GS_ITEM_WEIGHT,GS_LOC_AL_ID,GS_OTHER_DETAIL,GS_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,GS_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam.Type & "', " &
                                                  "'" & InParam.ItemID & "', " &
                                                  " " & InParam.DescMiscID & " , " &
                                                  " " & InParam.Weight.ToString() & ", " &
                                                  "'" & InParam.LocationID & "', " &
                                                  "'" & InParam.OtherDetails & "', " &
                                                  " " & InParam.Amount & ", " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.GOLD_SILVER_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_InsertTRIDAndTRSRNo</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSRNo_GoldSilver, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RecID As String = Guid.NewGuid.ToString
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            If InParam1.DescMiscID.Trim.Length = 0 Then InParam1.DescMiscID = "NULL" Else InParam1.DescMiscID = "'" & InParam1.DescMiscID & "'"
            Dim OnlineQuery As String = "INSERT INTO GOLD_SILVER_INFO(GS_CEN_ID,GS_COD_YEAR_ID,GS_TYPE,GS_ITEM_ID,GS_DESC_MISC_ID,GS_ITEM_WEIGHT,GS_LOC_AL_ID,GS_OTHER_DETAIL,GS_TR_ID,GS_TR_ITEM_SRNO,GS_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,GS_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.Type & "', " &
                                                  "'" & InParam1.ItemID & "', " &
                                                  " " & InParam1.DescMiscID & " , " &
                                                  " " & InParam1.Weight.ToString() & ", " &
                                                  "'" & InParam1.LocationID & "', " &
                                                  "'" & InParam1.OtherDetails & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  " " & InParam1.TxnSrno & ", " &
                                                  " " & InParam1.Amount & ", " &
                                                  "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.GOLD_SILVER_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates GoldSilver Info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.GoldSilver_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_GoldSilver, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.DescMiscID.Trim.Length = 0 Then UpParam.DescMiscID = "NULL" Else UpParam.DescMiscID = "'" & UpParam.DescMiscID & "'"
            Dim OnlineQuery As String = " UPDATE GOLD_SILVER_INFO SET " &
                                         "GS_TYPE           ='" & UpParam.Type & "', " &
                                         "GS_ITEM_ID        ='" & UpParam.ItemID & "', " &
                                         "GS_DESC_MISC_ID   = " & UpParam.DescMiscID & " , " &
                                         "GS_ITEM_WEIGHT    = " & UpParam.Weight & ", " &
                                         "GS_LOC_AL_ID      ='" & UpParam.LocationID & "', " &
                                         "GS_OTHER_DETAIL   ='" & UpParam.OtherDetails & "', " &
                                         "GS_AMT   =" & UpParam.Amount & ", " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.GOLD_SILVER_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace
