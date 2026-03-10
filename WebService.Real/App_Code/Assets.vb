Imports System.Data

Namespace Real
    <Serializable>
    Public Class Assets
#Region "Param Classes"
        <Serializable>
        Public Class Param_Assets_GetList_Common
            Public AI_TR_ID As String
            Public AI_REC_ID As String
            Public Item_ID As String
            Public Cen_ID As String
        End Class
        <Serializable>
        Public Class Param_GetProfileListing
            Public Asset_Profile As ConnectOneWS.AssetProfiles
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TableName As ConnectOneWS.Tables
        End Class
        <Serializable>
        Public Class Param_Assets_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Assets
            Public AssetType As String
            Public ItemID As String
            Public Make As String
            Public Model As String
            Public SrNo As String
            Public Rate As Double
            Public InsAmount As Double
            Public PurchaseDate As String
            Public PurchaseAmount As Double
            Public Warranty As Double
            Public Quantity As Double
            Public LocationId As String
            Public OtherDetails As String
            Public Status_Action As String
            Public Image As Byte()
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRIDAndTRSrNo_Assets : Inherits Parameter_Insert_Assets
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdAsstID As String ' Contains old RecId of Asset that is being re-posted(updated)
            Public Screen As ConnectOneWS.ClientScreen
            Public RecID As String = Guid.NewGuid.ToString
        End Class
        <Serializable>
        Public Class Parameter_Update_Assets
            Public ItemID As String
            Public Make As String
            Public Model As String
            Public SrNo As String
            Public Rate As Double
            Public InsAmount As Double
            Public PurchaseDate As String
            Public PurchaseAmount As Double
            Public Warranty As Double
            Public Quantity As Double
            Public LocationId As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
            Public Image As Byte()
        End Class
        <Serializable>
        Public Class Param_Assets_GetTransactions
            Public Rec_IDs As String
            Public YearID As Integer
            Public NextUnauditedYearID As String = ""
        End Class
        <Serializable>
        Public Class Param_Assets_GetTransactions_ByTable
            Public Table As ConnectOneWS.Tables
            Public YearID As String = ""
            Public NextUnauditedYearID As String = ""
        End Class
        <Serializable>
        Public Class Parameter_Update_Assets_Location
            Public Asset_Rec_ID As String = ""
            Public Location_From_ID As String = ""
            Public Location_To_ID As String = ""
            Public ChangeRemarks As String = ""
            Public changeDatetime As DateTime
        End Class
#End Region

        Public Shared Function GetRecord(ByVal RecId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT *" & _
                                        " FROM ASSET_INFO as ai left outer join asset_images as asi on ai.REC_ID = asi.asset_id Where ai.REC_ID = '" & RecId & "' "
            Return dbService.List(ConnectOneWS.Tables.ASSET_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetRecId_From_OrgID(ByVal OrgRecId As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT REC_ID" & _
                                        " FROM ASSET_INFO Where AI_ORG_REC_ID = '" & OrgRecId & "' AND AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List, Replaced
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Assets_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT CASE WHEN AI_TYPE='ASSET' THEN 'Capitalised Asset' ELSE 'Insurance Asset' END as 'ASSET_TYPE' ,'' AS 'ITEM_NAME',AI_ITEM_ID ,'' AS ITEM_LED_ID,'' AS Head, AI_MAKE AS Make,AI_MODEL as Model,AI_SERIAL_NO,AI_PUR_DATE,AI_AMT_FOR_INS,AI_ADJ_FOR_INS, 0 as  AI_CLOSE_FOR_INS,AI_PUR_AMT, AI_PUR_AMT as 'Curr Value',AI_WARRANTY as Warranty,AI_QTY AS Quantity,AI_QTY AS 'Curr Qty',AI_RATE AS Rate,al.AL_LOC_NAME,AI_LOC_AL_ID,AI_OTHER_DETAIL  ,'Un-Sold' AS 'Sale Status','' as 'Sale Date',  0 as 'Sale Quantity',AI_QTY as 'Balance Quantity',CASE WHEN AI_TR_ID IS NULL OR AI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '' ELSE AI_TR_ID END AS TR_ID,AI_COD_YEAR_ID AS YearID,ai.REC_ID AS ID ,CASE WHEN AI_TR_ID IS NULL OR AI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as 'Entry Type' , " & Common.Remarks_Detail("ai", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("ai") & "" & _
                                        " FROM ASSET_INFO as ai inner join asset_location_info as al on ai.AI_LOC_AL_ID = al.REC_ID Where   ai.REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            Return dbService.List(ConnectOneWS.Tables.ASSET_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Asset Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Param_GetProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Asset_Profile"
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
        ''' <param name="screen"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  REC_ID AS ID,AI_ITEM_ID ,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_AMT_FOR_INS,AI_PUR_AMT,AI_PUR_DATE,AI_WARRANTY,AI_QTY,AI_OTHER_DETAIL,AI_LOC_AL_ID    FROM Asset_Info " & _
                                  " Where   REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.ASSET_INFO, Query, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List Common Function
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Object = Nothing) As DataTable
            Dim nextParam As Param_Assets_GetList_Common = New Param_Assets_GetList_Common()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    nextParam.AI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    nextParam.AI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    nextParam.AI_REC_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    nextParam.AI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    nextParam.Item_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    nextParam = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_WIP_Finalization
                    nextParam.AI_TR_ID = Param
            End Select
            Return GetList_Common_ByRecID(inBasicParam, nextParam)
        End Function

        Public Shared Function GetList_Common_ByRecID(inBasicParam As ConnectOneWS.Basic_Param, ByVal Param As Param_Assets_GetList_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = "select AI_TR_ID,AI_TR_ITEM_SRNO,AI_LOC_AL_ID,AI_TYPE,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_PUR_DATE,AI_WARRANTY, AI_RATE,AI_IMG.IMAGE AS AI_IMAGE,AI_AMT_FOR_INS from ASSET_Info AI LEFT OUTER JOIN ASSET_IMAGES AS AI_IMG ON AI.REC_ID = AI_IMG.ASSET_ID where  REC_STATUS IN (0,1,2) AND AI_TR_ID= '" & Param.AI_TR_ID & "' ORDER BY AI_TR_ITEM_SRNO"  'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = "select AI_TR_ID,AI_TR_ITEM_SRNO,AI_LOC_AL_ID,AI_TYPE,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_PUR_DATE,AI_WARRANTY,AI_IMG.IMAGE AS AI_IMAGE,AI.REC_ID from ASSET_Info AI LEFT OUTER JOIN ASSET_IMAGES AS AI_IMG ON AI.REC_ID = AI_IMG.ASSET_ID where  REC_STATUS IN (0,1,2) AND AI_TR_ID= '" & Param.AI_TR_ID & "' ORDER BY AI_TR_ITEM_SRNO"  'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "select ii.ITEM_NAME AS Item, COALESCE(AI_MAKE,'') AS Make, COALESCE(AI_MODEL,'') AS Model, COALESCE(AI_QTY,0) AS 'Org Qty', COALESCE(AI_QTY,0) AS 'Curr Qty', COALESCE(AI_PUR_AMT,0) as 'Org Value', COALESCE(AI_PUR_AMT,0) as 'Curr Value', ai.REC_ID FROM ASSET_INFO as ai INNER JOIN ITEM_INFO as ii on ai.AI_ITEM_ID = ii.REC_ID WHERE AI_TYPE='ASSET' AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ai.AI_ITEM_ID= '" & Param.Item_ID & "' and ai.REC_STATUS IN (0,1,2)  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID)  OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  )"
                    If Not Param.AI_REC_ID Is Nothing Then 'added for multiuser check
                        Query += " AND ai.REC_ID = '" & Param.AI_REC_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT  AI_TYPE,'' AS 'REF_ITEM',AI_ITEM_ID AS 'REF_ITEM_ID',AI_QTY AS 'REF_QTY',AI_MAKE + ', ' + AI_MODEL + ', S.No.: ' + AI_SERIAL_NO         as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as ITEM_CON_LED_ID,'' as ITEM_CON_MIN_VALUE,'' as ITEM_CON_MAX_VALUE,'' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',REC_EDIT_ON  FROM Asset_Info  Where   REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))   AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " )"
                    If Not Param.AI_REC_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.AI_REC_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = " SELECT  AI_TYPE,'' AS 'REF_ITEM',AI_ITEM_ID AS 'REF_ITEM_ID',AI_QTY AS 'REF_QTY',AI_MAKE + ', ' + AI_MODEL + ', S.No.: ' + AI_SERIAL_NO         as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as ITEM_CON_LED_ID,'' as ITEM_CON_MIN_VALUE,'' as ITEM_CON_MAX_VALUE,'' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',AI_LOC_AL_ID AS REF_LOC_ID, REC_EDIT_ON  FROM Asset_Info  Where   REC_STATUS IN (0,1,2) AND AI_CEN_ID=" & Param.Cen_ID.ToString & " and  AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID)  OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  )"
                    If Not Param.AI_REC_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.AI_REC_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_WIP_Finalization
                    Query = "select AI_TR_ID,AI_ITEM_ID,AI_LOC_AL_ID, COALESCE(AI_QTY,0) AS QTY, AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_WARRANTY,ai_rate,AI_OTHER_DETAIL,REC_ID from ASSET_Info where  REC_STATUS IN (0,1,2) AND AI_TR_ID= '" & Param.AI_TR_ID & "'"  'RecID

            End Select
            Return dbService.List(ConnectOneWS.Tables.ASSET_INFO, Query, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transactions
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetTransactions</remarks>
        Public Shared Function GetTransactions(ByVal inParam As Param_Assets_GetTransactions, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NextYear As String = ""
            If inParam.NextUnauditedYearID.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & inParam.NextUnauditedYearID.ToString & ""
            Dim OnlineQuery As String = " SELECT TP.TR_REF_ID, SUM(TM.TR_SALE_QTY) AS 'Sale Quantity',MAX(TM.TR_SALE_DATE) AS 'Sale Date'  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN (" & inParam.Rec_IDs & ") AND TM.TR_CODE=11  and (TM.TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transactions by table name 
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_GetTransactions</remarks>
        Public Shared Function GetTransactions_TableName(ByVal inParam As Param_Assets_GetTransactions_ByTable, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim NextYear As String = ""
            If inParam.NextUnauditedYearID.Length > 0 Then NextYear = " or TM.TR_COD_YEAR_ID =" & inParam.NextUnauditedYearID.ToString & ""
            Dim OnlineQuery As String = " SELECT TP.TR_REF_ID, SUM(TM.TR_SALE_QTY) AS 'Sale Quantity',MAX(TM.TR_SALE_DATE) AS 'Sale Date'  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM " & inParam.Table.ToString() & " WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and (TM.TR_COD_YEAR_ID =" & inParam.YearID.ToString & " " & NextYear & " ) GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Asset_Ref_MaxEditOn(ByVal Asset_RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Asset_Ref_MaxEditOn"
            Dim params() As String = {"CENID", "ASSET_RECID"}
            Dim values() As Object = {inBasicParam.openCenID, Asset_RecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 36}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

      

        ''' <summary>
        ''' Updates AssetLocation Where not Present
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_UpdateAssetLocationIfNotPresent</remarks>
        Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " UPDATE ASSET_INFO SET AI_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(AI_LOC_AL_ID,'') ='' "
            dbService.Update(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Assets_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Assets, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO ASSET_INFO(AI_CEN_ID,AI_COD_YEAR_ID,AI_ITEM_ID,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_RATE,AI_AMT_FOR_INS,AI_PUR_DATE,AI_PUR_AMT,AI_WARRANTY,AI_QTY,AI_LOC_AL_ID,AI_OTHER_DETAIL,AI_TYPE," & _
                                              "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,AI_ORG_REC_ID" & _
                                              ") VALUES(" & _
                                              "" & inBasicParam.openCenID.ToString & "," & _
                                              "" & inBasicParam.openYearID.ToString & "," & _
                                              "'" & InParam.ItemID & "', " & _
                                              "'" & InParam.Make & "', " & _
                                              "'" & InParam.Model & "', " & _
                                              "'" & InParam.SrNo & "', " & _
                                              " " & InParam.Rate & ", " & _
                                              " " & IIf(InParam.ItemID = "ebe50824-8112-4d99-ac3c-8dd2f9e69860", 0, InParam.InsAmount) & ", " & _
                                              " " & If(IsDate(InParam.PurchaseDate), "'" & Convert.ToDateTime(InParam.PurchaseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                              " " & InParam.PurchaseAmount & ", " & _
                                              " " & InParam.Warranty & ", " & _
                                              " " & InParam.Quantity & ", " & _
                                              "'" & InParam.LocationId & "', " & _
                                              "'" & InParam.OtherDetails & "', " & _
                                              "'" & InParam.AssetType & "', " & _
                                              "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam, RecID)

            If Not InParam.Image Is Nothing Then
                OnlineQuery = "INSERT INTO asset_images(rec_id, asset_id, Image" & _
                                                  ") VALUES(" & _
                                                  " NEWID()," & _
                                                  "'" & RecID & "'," & _
                                                  "@Image" & ")"
                dbService.Insert(ConnectOneWS.Tables.ASSET_IMAGES, OnlineQuery, inBasicParam, RecID, Nothing, Nothing, "@Image", SqlDbType.Binary, InParam.Image)
            End If
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
        ''' <remarks>RealServiceFunctions.Assets_InsertTRIDAndTRSrNo</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_Assets, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RecID As String = Guid.NewGuid.ToString
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO ASSET_INFO(AI_CEN_ID,AI_COD_YEAR_ID,AI_ITEM_ID,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_RATE,AI_AMT_FOR_INS,AI_PUR_DATE,AI_PUR_AMT,AI_WARRANTY,AI_QTY,AI_LOC_AL_ID,AI_OTHER_DETAIL,AI_TYPE,AI_TR_ID,AI_TR_ITEM_SRNO," &
                                              "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,AI_ORG_REC_ID" &
                                              ") VALUES(" &
                                              "" & inBasicParam.openCenID.ToString & "," &
                                              "" & inBasicParam.openYearID.ToString & "," &
                                              "'" & InParam1.ItemID & "', " &
                                              "'" & InParam1.Make & "', " &
                                              "'" & InParam1.Model & "', " &
                                              "'" & InParam1.SrNo & "', " &
                                              " " & InParam1.Rate & ", " &
                                              " " & IIf(InParam1.ItemID = "ebe50824-8112-4d99-ac3c-8dd2f9e69860", 0, InParam1.InsAmount) & ", " &
                                              " " & If(IsDate(InParam1.PurchaseDate), "'" & Convert.ToDateTime(InParam1.PurchaseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                              " " & InParam1.PurchaseAmount & ", " &
                                              " " & InParam1.Warranty & ", " &
                                              " " & InParam1.Quantity & ", " &
                                              "'" & InParam1.LocationId & "', " &
                                              "'" & InParam1.OtherDetails & "', " &
                                              "'" & InParam1.AssetType & "', " &
                                              "'" & InParam1.TxnID & "', " &
                                              " " & InParam1.TxnSrNo & ", " &
                                              "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)

            If Not InParam1.Image Is Nothing Then
                OnlineQuery = "INSERT INTO asset_images(rec_id, asset_id, Image" & _
                                                 ") VALUES(" & _
                                                 " NEWID()," & _
                                                 "'" & InParam1.RecID & "'," & _
                                                 "@Image" & ")"
                dbService.Insert(ConnectOneWS.Tables.ASSET_IMAGES, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, Nothing, "@Image", SqlDbType.Binary, InParam1.Image)
            End If
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>    
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Assets_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Assets, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ASSET_INFO SET " & _
                                     "AI_ITEM_ID        ='" & UpParam.ItemID & "', " & _
                                     "AI_MAKE           ='" & UpParam.Make & "', " & _
                                     "AI_MODEL          ='" & UpParam.Model & "', " & _
                                     "AI_SERIAL_NO      ='" & UpParam.SrNo & "', " & _
                                     "AI_RATE           = " & UpParam.Rate & ", " & _
                                     "AI_AMT_FOR_INS    = " & UpParam.InsAmount & ", " & _
                                     "AI_PUR_DATE       = " & If(IsDate(UpParam.PurchaseDate), "'" & Convert.ToDateTime(UpParam.PurchaseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                     "AI_PUR_AMT        = " & UpParam.PurchaseAmount & ", " & _
                                     "AI_WARRANTY       = " & UpParam.Warranty & ", " & _
                                     "AI_QTY            = " & UpParam.Quantity & ", " & _
                                     "AI_LOC_AL_ID      ='" & UpParam.LocationId & "', " & _
                                     "AI_OTHER_DETAIL   ='" & UpParam.OtherDetails & "', " & _
                                    "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                    "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                    "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam)

            OnlineQuery = " asset_id = '" & UpParam.Rec_ID & "'"
            dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_IMAGES, OnlineQuery, inBasicParam)

            OnlineQuery = "INSERT INTO asset_images(rec_id, asset_id, Image" & _
                                             ") VALUES(" & _
                                             " NEWID()," & _
                                             "'" & UpParam.Rec_ID & "'," & _
                                             "@Image" & ")"
            dbService.Insert(ConnectOneWS.Tables.ASSET_IMAGES, OnlineQuery, inBasicParam, UpParam.Rec_ID, Nothing, Nothing, "@Image", SqlDbType.Binary, UpParam.Image)


            Return True
        End Function

        Public Shared Function UpdateLocation(ByVal UpParam As Parameter_Update_Assets_Location, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ASSET_INFO SET " & _
                                        " AI_LOC_AL_ID      ='" & UpParam.Location_To_ID & "', " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                        " WHERE REC_ID      ='" & UpParam.Asset_Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam)
            Dim RecID As String = Guid.NewGuid.ToString
            Dim InsertQuery As String = " INSERT INTO asset_location_changes_info (ALC_CEN_ID, ALC_COD_YEAR_ID, ALC_DATE_TIME, ALC_LOC_FROM, ALC_LOC_TO, ALC_REMARKS, REC_ADD_ON, REC_ADD_BY, REC_ID, ALC_AI_ID) VALUES (" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        " " & If(IsDate(UpParam.changeDatetime), "'" & Convert.ToDateTime(UpParam.changeDatetime).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "'" & UpParam.Location_From_ID & "'," & _
                                        "'" & UpParam.Location_To_ID & "'," & _
                                        "'" & UpParam.ChangeRemarks & "'," & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & UpParam.Asset_Rec_ID & "')"
            dbService.Insert(ConnectOneWS.Tables.ASSET_LOCATION_CHANGES_INFO, InsertQuery, inBasicParam, RecID)
            Return True
        End Function

        Public Shared Function UpdateInsuranceValue(ByVal UpParam As Parameter_Update_Assets, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ASSET_INFO SET " & _
                                        " AI_ADJ_FOR_INS    ='" & UpParam.InsAmount & "', " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                        " WHERE REC_ID      ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.ASSET_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class AssetLocations
#Region "Param Classes"
        <Serializable>
        Public Class Param_AssetLoc_Insert
            Public name As String
            Public OtherDetails As String
            Public Status_Action As String
            Public Match_LB_ID As String
            Public Match_SP_ID As String
            Public max_Capacity As Int32
            Public ac_nonacField As String
            Public categoryField As String
            Public floorField As String
        End Class
        <Serializable>
        Public Class Param_AssetLoc_Update
            Public name As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public RecId As String

            Public Matching As String
            Public Match_LB_ID As String
            Public Match_SP_ID As String
            Public max_Capacity As Int32
            Public ac_nonacField As String
            Public categoryField As String
            Public floorField As String
        End Class
        <Serializable>
        Public Class Param_GetRecordCountByName
            Public LocationName As String = ""
            Public TrID As String = ""
            Public CEN_BK_PAD_NO As String = ""
        End Class
        <Serializable>
        Public Class Param_GetRecordCountByName_CurrUID
            Public LocationName As String = ""
            Public TrID As String = ""
        End Class
        <Serializable>
        Public Class Param_Check_Location_Count
            Public LocationID As String
            Public Exclude_Sold_TF As Boolean = True
        End Class
        <Serializable>
        Public Class Param_AssetLocation_GetList
            Public Location_Rec_ID As String = Nothing
            Public Tr_M_ID As String = Nothing
            Public Prev_Year_ID As Integer = Nothing
            Public Store_Dept_ID As Integer = Nothing
            Public LB_REC_ID As String = Nothing
            Public SP_REC_ID As String = Nothing
            Public PassCenID As Boolean = True
        End Class
#End Region
        ''' <summary>
        ''' Gets Assets location for a CenId
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetList</remarks>
        Public Shared Function GetList(inParam As Param_AssetLocation_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim onlineQuery As String = " SELECT AL_LOC_NAME as 'Location Name',AL.REC_ID as AL_ID,AL.REC_EDIT_ON,   CASE WHEN AL.LB_REC_ID IS NOT NULL THEN  'Property'  ELSE CASE WHEN AL.SP_REC_ID IS NOT NULL THEN 'Service Place' else NULL END  END AS 'Matched Type',  CASE WHEN L.LB_PRO_NAME IS NOT NULL THEN L.LB_PRO_NAME ELSE CASE WHEN  SP.SP_SERVICE_PLACE_NAME IS NOT NULL  THEN SP.SP_SERVICE_PLACE_NAME ELSE NULL END END AS  'Matched Name', COALESCE(L_INST.INS_SHORT ,SP_INST.INS_SHORT) AS 'Matched Instt.'  from Asset_Location_Info as AL " & _
            '                            " LEFT JOIN  Land_Building_Info AS L ON (AL.LB_REC_ID=L.REC_ID) " & _
            '                            " LEFT JOIN  Service_place_info AS SP ON (AL.SP_REC_ID=SP.REC_ID) " & _
            '                            " LEFT OUTER JOIN CENTRE_INFO AS L_CEN ON L_CEN.CEN_ID = L.LB_CEN_ID " & _
            '                            " LEFT OUTER JOIN CENTRE_INFO AS SP_CEN ON SP_CEN.CEN_ID = SP.SP_CEN_ID " & _
            '                            " LEFT OUTER JOIN INSTITUTE_INFO AS L_INST ON L_CEN.CEN_INS_ID = L_INST.INS_ID " & _
            '                            " LEFT OUTER JOIN INSTITUTE_INFO AS SP_INST ON SP_CEN.CEN_INS_ID = SP_INST.INS_ID " & _
            '                            " where AL.REC_STATUS IN (0,1,2) AND AL_CEN_ID ='" & inBasicParam.openCenID & "' " & _
            '                            " AND (AL.LB_REC_ID IS NULL OR AL.LB_REC_ID IN (SELECT REC_ID FROM land_building_info WHERE LB_COD_YEAR_ID <='" & inBasicParam.openYearID & "' and ( LB_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = '" & inBasicParam.openYearID & "' )))  " & _
            '                            " AND (AL.SP_REC_ID IS NULL OR AL.SP_REC_ID IN (SELECT REC_ID FROM service_place_info WHERE SP_COD_YEAR_ID <= '" & inBasicParam.openYearID & "' and ( SP_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR SP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = SP_CEN_ID))  AND (SP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = SP_CEN_ID) OR SP_COD_YEAR_ID = '" & inBasicParam.openYearID & "' ))) " & _
            '                            " AND COALESCE(AL.LB_REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 ) " & _
            '                            " AND COALESCE(AL.LB_REC_ID,'') NOT IN (select TR_REF_OTHERS from transaction_info where TR_CODE =15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' AND TR_REF_OTHERS IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2))) " & _
            '                            " AND (AL_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AL_CEN_ID) OR AL_COD_YEAR_ID = '" & inBasicParam.openYearID & "') "
            'If Not inParam Is Nothing Then
            '    If Not inParam.Location_Rec_ID Is Nothing Then 'added for multiuser check
            '        onlineQuery += " And AL.REC_ID = '" & inParam.Location_Rec_ID & "' "
            '    End If
            '    If Not inParam.Tr_M_ID Is Nothing Then
            '        onlineQuery += " And COALESCE(L.LB_TR_ID,'a' ) <> '" & inParam.Tr_M_ID & "' "
            '    End If
            'End If
            'Return dbService.List(ConnectOneWS.Tables.ASSET_LOCATION_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "TR_M_ID", "LOC_RECID", "STORE_DEPT_ID", "LB_REC_ID", "SP_REC_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Prev_Year_ID, inParam.Tr_M_ID, inParam.Location_Rec_ID, IIf(inParam.Store_Dept_ID = 0, DBNull.Value, inParam.Store_Dept_ID), inParam.LB_REC_ID, inParam.SP_REC_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String], DbType.Int32, DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 4, 36, 36, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_LOCATION_INFO, "sp_get_Asset_Location_Listing", ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStockLocationList(inParam As Param_AssetLocation_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "LOC_RECID", "STORE_DEPT_ID", "LB_REC_ID", "SP_REC_ID"}
            Dim values As Object() = {IIf(inParam.PassCenID = True, inBasicParam.openCenID, DBNull.Value), inParam.Location_Rec_ID, IIf(inParam.Store_Dept_ID = 0, DBNull.Value, inParam.Store_Dept_ID), inParam.LB_REC_ID, inParam.SP_REC_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.[String], DbType.Int32, DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {4, 36, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ASSET_LOCATION_INFO, "Get_SM_Locations", ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function
        ''' <summary>
        ''' Gets Assets location for a Property ID
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetList</remarks>
        Public Shared Function GetListByLBID(inBasicParam As ConnectOneWS.Basic_Param, LB_ID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT REC_ID as AL_ID,REC_EDIT_ON  from Asset_Location_Info where  REC_STATUS IN (0,1,2) AND LB_REC_ID ='" & LB_ID & "'  "
            Return dbService.List(ConnectOneWS.Tables.ASSET_LOCATION_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Assets location for a Service Place ID
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetList</remarks>
        Public Shared Function GetListBySPID(inBasicParam As ConnectOneWS.Basic_Param, SP_ID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT REC_ID as AL_ID, AL_LOC_NAME  from Asset_Location_Info where  REC_STATUS IN (0,1,2) AND SP_REC_ID ='" & SP_ID & "'  "
            Return dbService.List(ConnectOneWS.Tables.ASSET_LOCATION_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Location Name, Othe Details, Location Main and Rec ID
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetFullList</remarks>
        Public Shared Function GetFullList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT  AL_LOC_NAME AS 'Location Name',AL_OTHER_DETAIL AS 'Other Detail',AL_LOC_MAIN AS System, AL_AC_OR_NON_AC, AL_CATEGORY, AL_ROOM_FLOOR, COALESCE(AL_MAXCAPACITY,0) as Capacity, AL.REC_ID AS ID , CASE WHEN AL.LB_REC_ID IS NOT NULL THEN  'Property'  ELSE CASE WHEN AL.SP_REC_ID IS NOT NULL THEN 'Service Place' else NULL END  END AS 'Matched Type'  ,  CASE WHEN L.LB_PRO_NAME IS NOT NULL THEN L.LB_PRO_NAME ELSE CASE WHEN  SP.SP_SERVICE_PLACE_NAME IS NOT NULL  THEN SP.SP_SERVICE_PLACE_NAME ELSE NULL END END AS  'Matched Name',COALESCE(L_INST.INS_SHORT ,SP_INST.INS_SHORT) AS 'Matched Instt.', AL_COD_YEAR_ID AS YEARID, AL_ORG_REC_ID AS ORG_REC, " & Common.Rec_Detail("AL") & "" &
                                  " FROM Asset_Location_Info as AL " &
                                  " LEFT JOIN  Land_Building_Info AS L ON (AL.LB_REC_ID=L.REC_ID) " &
                                  " LEFT JOIN  Service_place_info AS SP ON (AL.SP_REC_ID=SP.REC_ID) " &
                                  " LEFT OUTER JOIN CENTRE_INFO AS L_CEN ON L_CEN.CEN_ID = L.LB_CEN_ID " &
                                  " LEFT OUTER JOIN CENTRE_INFO AS SP_CEN ON SP_CEN.CEN_ID = SP.SP_CEN_ID " &
                                  " LEFT OUTER JOIN INSTITUTE_INFO AS L_INST ON L_CEN.CEN_INS_ID = L_INST.INS_ID " &
                                  " LEFT OUTER JOIN INSTITUTE_INFO AS SP_INST ON SP_CEN.CEN_INS_ID = SP_INST.INS_ID " &
                                  " Where  AL.REC_STATUS IN (0,1,2) AND AL_CEN_ID=" & inBasicParam.openCenID.ToString & "" &
                                  " AND (AL.LB_REC_ID IS NULL OR AL.LB_REC_ID IN (SELECT REC_ID FROM land_building_info WHERE LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID)) AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  ))) " &
                                  " AND (AL.SP_REC_ID IS NULL OR AL.SP_REC_ID IN (SELECT REC_ID FROM service_place_info WHERE SP_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & " and ( SP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR SP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = SP_CEN_ID)) AND (SP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = SP_CEN_ID) OR SP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ))) " &
                                  " AND COALESCE(AL.LB_REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 AND TM.TR_COD_YEAR_ID < " & inBasicParam.openYearID.ToString & ") " &
                                  " AND COALESCE(AL.LB_REC_ID,'') NOT IN (select TR_REF_OTHERS from transaction_info where TR_CODE =15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT'  AND TR_COD_YEAR_ID < " & inBasicParam.openYearID.ToString & " AND TR_REF_OTHERS IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2))) " &
                                  " AND (AL_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AL_CEN_ID) OR AL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            Return dbService.List(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetLocationMatching(ByVal BK_PAD_NO As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT  ROW_NUMBER() OVER (ORDER BY C.CEN_INS_ID,C.CEN_UID,AL.AL_LOC_MAIN DESC) AS 'Sr.',I.INS_SHORT AS 'Institute',C.CEN_UID AS 'UID' ,AL.AL_LOC_NAME AS 'Location Name',AL.AL_OTHER_DETAIL as 'Location Remarks', " & _
                                        " CASE WHEN AL.LB_REC_ID IS NOT NULL THEN CASE WHEN TR_REF_AMT IS noT NULL  THEN NULL ELSE 'Property' END ELSE CASE WHEN AL.SP_REC_ID IS NOT NULL THEN 'Service Place' else NULL END  END AS 'Matched Type', " & _
                                        " L.LB_PRO_NAME as 'Property Name',dbo.fn_GET_PRO_ADDRESS(L.REC_ID) as 'Property Address',L.LB_PRO_CATEGORY as 'Property Category',L.LB_PRO_TYPE as 'Property Type',L.LB_PRO_USE as 'Property Use', L_INS.INS_SHORT as 'Property Instt', AL.LB_REC_ID as 'Pro.ID', CASE WHEN TR_REF_AMT IS NULL AND AL.LB_REC_ID IS NOT NULL THEN 'UNSOLD' ELSE CASE WHEN AL.LB_REC_ID IS NOT NULL THEN 'SOLD' ELSE '' END END AS 'Property Status', " & _
                                        " SP.SP_SERVICE_PLACE_NAME AS 'Service Place Name',SP.SP_SERVICE_PLACE_TYPE AS 'Service Place Type', SP_INS.INS_SHORT as 'Service Place Instt', SP.REC_ID AS 'Ser.ID',AL.REC_ID as 'ID' " & _
                                        " FROM Asset_Location_Info AS AL " & _
                                        " INNER JOIN Centre_info AS C ON (AL.AL_CEN_ID=C.CEN_ID) " & _
                                        " INNER JOIN Institute_info AS I ON (C.CEN_INS_ID=I.INS_ID)" & _
                                        " LEFT JOIN  Land_Building_Info AS L ON (AL.LB_REC_ID=L.REC_ID) " & _
                                        " LEFT JOIN  Service_place_info AS SP ON (AL.SP_REC_ID=SP.REC_ID) " & _
                                        " LEFT OUTER JOIN  CENTRE_INFO AS L_CEN ON L_CEN.CEN_ID = L.LB_CEN_ID " & _
                                        " LEFT OUTER JOIN  CENTRE_INFO AS SP_CEN ON SP_CEN.CEN_ID = SP.SP_CEN_ID " & _
                                        " LEFT OUTER JOIN INSTITUTE_INFO AS L_INS ON L_INS.INS_ID = L_CEN.CEN_INS_ID " & _
                                        " LEFT OUTER JOIN INSTITUTE_INFO AS SP_INS ON SP_INS.INS_ID = SP_CEN.CEN_INS_ID " & _
                                        " LEFT OUTER JOIN Transaction_D_Payment_Info AS TP ON TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID = L.REC_ID " & _
                                        " Where   AL.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & BK_PAD_NO & "' " & _
                                        " AND (LB_REC_ID IS NULL OR LB_REC_ID IN (SELECT REC_ID FROM land_building_info WHERE LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID)) AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  ))) " & _
                                        " AND (SP_REC_ID IS NULL OR SP_REC_ID IN (SELECT REC_ID FROM service_place_info WHERE SP_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & " and ( SP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR SP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = SP_CEN_ID)) AND (SP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = SP_CEN_ID) OR SP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ))) " & _
                                        " AND COALESCE(AL.LB_REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 AND TM.TR_COD_YEAR_ID < " & inBasicParam.openYearID.ToString & ") " & _
                                        " AND COALESCE(AL.LB_REC_ID,'') NOT IN (select TR_REF_OTHERS from transaction_info where TR_CODE =15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT'  AND TR_COD_YEAR_ID < " & inBasicParam.openYearID.ToString & " AND TR_REF_OTHERS IN ( SELECT REC_ID FROM land_building_info WHERE REC_STATUS IN (0,1,2))) " & _
                                        " AND (AL_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AL_CEN_ID) OR AL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            Return dbService.List(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetLocationMatchingCount(ByVal BK_PAD_NO As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(AL.REC_ID)  FROM Asset_Location_Info AS AL  INNER JOIN Centre_info AS C ON (AL.AL_CEN_ID=C.CEN_ID)  " & _
                                  " INNER JOIN Institute_info AS I ON (C.CEN_INS_ID=I.INS_ID) " & _
                                  " LEFT OUTER JOIN Transaction_D_Payment_Info AS TP ON TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID = AL.LB_REC_ID " & _
                                  " Where   AL.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & BK_PAD_NO & "'  AND ((AL.LB_REC_ID IS NULL AND AL.SP_REC_ID IS NULL) OR TR_REF_AMT IS NOT NULL ) "
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, Query, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetLocationsMatchedCount(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(AL.REC_ID)  FROM Asset_Location_Info AS AL " & _
                                  " Where   AL.REC_STATUS IN (0,1,2) AND (AL.LB_REC_ID = '" & Rec_ID & "' OR AL.SP_REC_ID = '" & Rec_ID & "') "
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, Query, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Create AssetLocation Form : checks if there is a same named asset location already in all sisterUIDS
        ''' </summary>
        ''' <param name="LocationName"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetRecordCountByName</remarks>
        Public Shared Function GetRecordCountByName(ByVal param As Param_GetRecordCountByName, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT COUNT(*) FROM ASSET_LOCATION_INFO WHERE  REC_STATUS IN (0,1,2) AND UPPER(AL_LOC_NAME)='" & Trim(UCase(param.LocationName)) & "' AND  AL_CEN_ID IN (SELECT CEN_ID FROM CENTRE_INFO WHERE CEN_BK_PAD_NO = '" & param.CEN_BK_PAD_NO & "')  and ( AL_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AL_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AL_CEN_ID))  AND (AL_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AL_CEN_ID) OR AL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            If param.TrID.Length > 0 Then
                OnlineQuery += " AND COALESCE(LB_REC_ID,'') NOT IN (SELECT REC_ID FROM LAND_BUILDING_INFO WHERE LB_TR_ID ='" & param.TrID & "')"
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        '''  Create AssetLocation Form : checks if there is a same named asset location already in current UID
        ''' </summary>
        ''' <param name="param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetRecordCountByName_CurrentUID</remarks>
        Public Shared Function GetRecordCountByName_CurrentUID(ByVal param As Param_GetRecordCountByName_CurrUID, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT COUNT(*) FROM ASSET_LOCATION_INFO WHERE  REC_STATUS IN (0,1,2) AND UPPER(AL_LOC_NAME)='" & Trim(UCase(param.LocationName)) & "' AND  AL_CEN_ID = " & inBasicParam.openCenID.ToString & " and ( AL_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AL_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AL_CEN_ID)) AND (AL_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AL_CEN_ID) OR AL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            If param.TrID.Length > 0 Then
                OnlineQuery += " AND COALESCE(LB_REC_ID,'') NOT IN (SELECT REC_ID FROM LAND_BUILDING_INFO WHERE LB_TR_ID ='" & param.TrID & "')"
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ' ''' <summary>
        ' ''' Inserts Default Asset Location if not already present 
        ' ''' </summary>
        ' ''' <param name="Screen"></param>
        ' ''' <param name="openUserID"></param>
        ' ''' <param name="openCenID"></param>
        ' ''' <param name="PCID"></param>
        ' ''' <param name="version"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.AssetLocations_InsertIfDefaultNotPresent</remarks>
        'Public Shared Function InsertIfDefaultNotPresent(ByVal openCenName As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Query As String = "SELECT COUNT(REC_ID) FROM Asset_Location_Info  WHERE REC_STATUS IN (0,1,2) AND AL_LOC_MAIN='YES' AND AL_CEN_ID ='" & inBasicParam.openCenID & "' "
        '    Dim XNEWID As String = System.Guid.NewGuid.ToString
        '    Dim LocCount As Int32 = dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, Query, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        '    If LocCount = 0 Then
        '        Dim OnlineQuery As String = "INSERT INTO Asset_Location_Info(AL_CEN_ID,AL_LOC_NAME,AL_LOC_MAIN,AL_OTHER_DETAIL,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) " & _
        '                                        "VALUES ('" & inBasicParam.openCenID & "','" & openCenName & "','YES','System Created.','" & Common.DateTimePlaceHolder & "','" & inBasicParam.openUserID & "','" & Common.DateTimePlaceHolder & _
        '                                        "','" & inBasicParam.openUserID & "'," & Common_Lib.Common.Record_Status._Completed & ",'" & Common.DateTimePlaceHolder & "','" & inBasicParam.openUserID & "','" & XNEWID & "')"
        '        dbService.Insert(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam, XNEWID)
        '        Return True
        '    End If
        '    Return True 'As default location is already present 
        'End Function

        ''' <summary>
        ''' Returns Defualt Asset Location For Current Center 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetDefaultLocation</remarks>
        Public Shared Function GetDefaultLocation(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ID FROM Asset_Location_Info  WHERE REC_STATUS IN (0,1,2) AND AL_LOC_MAIN='YES' AND AL_CEN_ID =" & inBasicParam.openCenID.ToString & " "
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, Query, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetPropertyID(LocID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "select LB_REC_ID from Asset_Location_Info where rec_id = '" & LocID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.ASSET_LOCATION_INFO, onlineQuery, ConnectOneWS.Tables.ASSET_LOCATION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Add new Asset Location in current UID - used for custom location creation
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_Insert</remarks>
        Public Shared Function Insert(ByVal Param As Param_AssetLoc_Insert, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim LB_ID As String = " NULL " : Dim SP_ID As String = " NULL "
            If Param.Match_LB_ID.Length > 0 Then LB_ID = "'" & Param.Match_LB_ID & "'"
            If Param.Match_SP_ID.Length > 0 Then SP_ID = "'" & Param.Match_SP_ID & "'"
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO ASSET_LOCATION_INFO(AL_CEN_ID,AL_LOC_NAME,AL_OTHER_DETAIL,AL_LOC_MAIN,LB_REC_ID,SP_REC_ID," &
                                               "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,AL_ORG_REC_ID,AL_COD_YEAR_ID,AL_MAXCAPACITY," &
                                               "AL_AC_OR_NON_AC,AL_CATEGORY, AL_ROOM_FLOOR) VALUES(" &
                                               "" & inBasicParam.openCenID.ToString & "," &
                                               "'" & Param.name & "', " &
                                               "'" & Param.OtherDetails & "','NO', " &
                                               " " & LB_ID & ", " &
                                               " " & SP_ID & ", " &
                                               "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Param.Status_Action & ", '" &
                                               Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "', " & inBasicParam.openYearID.ToString &
                                               ", " & Param.max_Capacity & ", '" & Param.ac_nonacField & "', '" & Param.categoryField & "', '" & Param.floorField & "')"
            dbService.Insert(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam, RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Add new Asset Location in all sisterUIDS
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <param name="AddTime"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Insert_AllSisterUIDs(ByVal Param As Param_AssetLoc_Insert, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim LB_ID As String = " NULL " : Dim SP_ID As String = " NULL "
            If Param.Match_LB_ID.Length > 0 Then LB_ID = "'" & Param.Match_LB_ID & "'"
            If Param.Match_SP_ID.Length > 0 Then SP_ID = "'" & Param.Match_SP_ID & "'"
            Dim SiblingCentres As DataTable = Center.GetSiblingCenterList(inBasicParam.openCenID, inBasicParam)
            For Each cRow As DataRow In SiblingCentres.Rows
                Dim RecID As String = Guid.NewGuid.ToString
                Dim OnlineQuery As String = "INSERT INTO ASSET_LOCATION_INFO(AL_CEN_ID,AL_LOC_NAME,AL_OTHER_DETAIL,AL_LOC_MAIN,LB_REC_ID,SP_REC_ID," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,AL_ORG_REC_ID,AL_COD_YEAR_ID " &
                                                 ") VALUES(" &
                                                 "" & cRow(0).ToString & "," &
                                                 "'" & Param.name & "', " &
                                                 "'" & Param.OtherDetails & "','NO', " &
                                                 " " & LB_ID & ", " &
                                                 " " & SP_ID & ", " &
                                                 "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', " & Param.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & RecID & "', '" & RecID & "', " & inBasicParam.openYearID.ToString & "" & ")"
                dbService.Insert(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam, RecID, Nothing, AddTime)
            Next
            Return True
        End Function


        ''' <summary>
        ''' Update Asset Location 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_Update</remarks>
        Public Shared Function UpdateByReference(ByVal Param As Param_AssetLoc_Update, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ASSET_LOCATION_INFO SET " &
                                         "AL_LOC_NAME       ='" & Param.name & "', " &
                                         "AL_OTHER_DETAIL   ='" & Param.OtherDetails & "', " &
                                         " " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' "
            If Param.RecId = Nothing Then
                OnlineQuery += "  WHERE (LB_REC_ID    ='" & Param.Match_LB_ID & "' OR SP_REC_ID = '" & Param.Match_SP_ID & "' )"
            Else : OnlineQuery += " WHERE REC_ID    ='" & Param.RecId & "'"
            End If


            dbService.Update(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function UpdateMatching(ByVal Param As Param_AssetLoc_Update, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim LB_ID As String = "" : Dim SP_ID As String = ""
            If Param.Matching = "PROPERTY" Then
                LB_ID = "'" & Param.Match_LB_ID & "'"
                SP_ID = " NULL "
            Else
                LB_ID = " NULL "
                SP_ID = "'" & Param.Match_SP_ID & "'"
            End If
            Dim OnlineQuery As String = " UPDATE ASSET_LOCATION_INFO SET " & _
                                         " LB_REC_ID         = " & LB_ID & " , " & _
                                         " SP_REC_ID         = " & SP_ID & " , " & _
                                         "  " & _
                                         " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         " REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         " WHERE REC_ID    ='" & Param.RecId & "'"
            dbService.Update(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Global(ByVal Param As Param_AssetLoc_Update, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim LB_ID As String = "" : Dim SP_ID As String = ""
            If Param.Matching = "PROPERTY" Then
                LB_ID = "'" & Param.Match_LB_ID & "'"
                SP_ID = " NULL "
            Else
                LB_ID = " NULL "
                SP_ID = "'" & Param.Match_SP_ID & "'"
            End If
            Dim OnlineQuery As String = "UPDATE ASSET_LOCATION_INFO SET " &
                                         "AL_LOC_NAME       ='" & Param.name & "', " &
                                         "AL_OTHER_DETAIL   ='" & Param.OtherDetails & "', " &
                                         "AL_MAXCAPACITY    = " & Param.max_Capacity & ", " &
                                         "AL_AC_OR_NON_AC    = '" & Param.ac_nonacField & "', " &
                                         "AL_CATEGORY    = '" & Param.categoryField & "', " &
                                         "AL_ROOM_FLOOR    = '" & Param.floorField & "', " &
                                         " LB_REC_ID         = " & LB_ID & " , " &
                                         " SP_REC_ID         = " & SP_ID & " , " &
                                         " " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                         " WHERE REC_ID    ='" & Param.RecId & "'"
            dbService.Update(ConnectOneWS.Tables.ASSET_LOCATION_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Gets Location Count In Assets
        ''' </summary>
        ''' <param name="LocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInAssets</remarks>
        Public Shared Function GetLocationCountInAssets(ByVal inParam As Param_Check_Location_Count, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN CEN_ID = " & inBasicParam.openCenID.ToString & " THEN 0 ELSE 1 END AS SR, COUNT(AI_LOC_AL_ID), MAX(CEN_UID) AS CEN_UID FROM ASSET_INFO  AS AI INNER JOIN CENTRE_INFO AS CI ON AI_CEN_ID = CEN_ID  WHERE AI.REC_STATUS IN (0,1,2) AND AI_LOC_AL_ID  = '" & inParam.LocationID & "' "
            If inParam.Exclude_Sold_TF Then Query += " AND COALESCE(AI.REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM ASSET_INFO WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and ( TM.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TM.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TM.TR_CEN_ID))) " & _
                                                                       " AND COALESCE(AI.REC_ID,'') NOT IN (SELECT TR_REF_OTHERS FROM Transaction_Info AS TI WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_REF_OTHERS IN ( SELECT REC_ID FROM ASSET_INFO WHERE REC_STATUS IN (0,1,2)) AND TI.TR_CODE=15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' and ( TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TI.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TI.TR_CEN_ID))) "
            Query += " GROUP BY  CEN_ID ORDER BY SR "
            Return dbService.List(ConnectOneWS.Tables.ASSET_INFO, Query, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Location Count In Gold Silver
        ''' </summary>
        ''' <param name="LocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInGS</remarks>
        Public Shared Function GetLocationCountInGS(ByVal inParam As Param_Check_Location_Count, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN CEN_ID = " & inBasicParam.openCenID.ToString & " THEN 0 ELSE 1 END AS SR, COUNT(GS_LOC_AL_ID), MAX(CEN_UID) AS CEN_UID FROM GOLD_SILVER_INFO  AS GS INNER JOIN CENTRE_INFO AS CI ON GS_CEN_ID = CEN_ID  WHERE GS.REC_STATUS IN (0,1,2) AND GS_LOC_AL_ID  = '" & inParam.LocationID & "' "
            If inParam.Exclude_Sold_TF Then Query += " AND COALESCE(GS.REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM GOLD_SILVER_INFO WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and ( TM.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TM.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TM.TR_CEN_ID))) " & _
                                                                       " AND COALESCE(GS.REC_ID,'') NOT IN (SELECT TR_REF_OTHERS FROM Transaction_Info AS TI WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_REF_OTHERS IN ( SELECT REC_ID FROM GOLD_SILVER_INFO WHERE REC_STATUS IN (0,1,2)) AND TI.TR_CODE=15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' and ( TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TI.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TI.TR_CEN_ID))) "
            Query += " GROUP BY  CEN_ID ORDER BY SR "
            Return dbService.List(ConnectOneWS.Tables.GOLD_SILVER_INFO, Query, ConnectOneWS.Tables.GOLD_SILVER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Location Count In Consumables
        ''' </summary>
        ''' <param name="LocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInConsumables</remarks>
        Public Shared Function GetLocationCountInConsumables(ByVal inParam As Param_Check_Location_Count, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN CEN_ID = " & inBasicParam.openCenID.ToString & " THEN 0 ELSE 1 END AS SR, COUNT(CS_LOC_AL_ID), MAX(CEN_UID) AS CEN_UID FROM CONSUMABLES_STOCK_INFO  AS CS INNER JOIN CENTRE_INFO AS CI ON CS_CEN_ID = CEN_ID  WHERE CS.REC_STATUS IN (0,1,2) AND CS_LOC_AL_ID  = '" & inParam.LocationID & "' AND CS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " "
            If inParam.Exclude_Sold_TF Then Query += " AND COALESCE(CS.REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM CONSUMABLES_STOCK_INFO WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and ( TM.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TM.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TM.TR_CEN_ID))) " & _
                                                                       " AND COALESCE(CS.REC_ID,'') NOT IN (SELECT TR_REF_OTHERS FROM Transaction_Info AS TI WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TI.TR_REF_OTHERS IN ( SELECT REC_ID FROM CONSUMABLES_STOCK_INFO WHERE REC_STATUS IN (0,1,2)) AND TI.TR_CODE=15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' and ( TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TI.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TI.TR_CEN_ID))) "
            Query += " GROUP BY  CEN_ID ORDER BY SR "
            Return dbService.List(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, Query, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Location Count In LiveStock
        ''' </summary>
        ''' <param name="LocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInLiveStock</remarks>
        Public Shared Function GetLocationCountInLiveStock(ByVal inParam As Param_Check_Location_Count, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN CEN_ID = " & inBasicParam.openCenID.ToString & " THEN 0 ELSE 1 END AS SR, COUNT(LS_LOC_AL_ID), MAX(CEN_UID) AS CEN_UID FROM Live_Stock_Info  AS LS INNER JOIN CENTRE_INFO AS CI ON LS_CEN_ID = CEN_ID  WHERE LS.REC_STATUS IN (0,1,2) AND LS_LOC_AL_ID  = '" & inParam.LocationID & "' "
            If inParam.Exclude_Sold_TF Then Query += " AND COALESCE(LS.REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM Live_Stock_Info WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and ( TM.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TM.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TM.TR_CEN_ID))) " & _
                                                                       " AND COALESCE(LS.REC_ID,'') NOT IN (SELECT TR_REF_OTHERS FROM Transaction_Info AS TI WHERE TI.REC_STATUS IN (0,1,2) AND TI.TR_REF_OTHERS IN ( SELECT REC_ID FROM Live_Stock_Info WHERE REC_STATUS IN (0,1,2)) AND TI.TR_CODE=15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' and ( TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TI.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TI.TR_CEN_ID))) "
            Query += " GROUP BY  CEN_ID ORDER BY SR "
            Return dbService.List(ConnectOneWS.Tables.LIVE_STOCK_INFO, Query, ConnectOneWS.Tables.LIVE_STOCK_INFO.ToString(), inBasicParam)
        End Function

        ' ''' <summary>
        ' ''' Gets Location Count In Telephones
        ' ''' </summary>
        ' ''' <param name="LocationID"></param>
        ' ''' <param name="Screen"></param>
        ' ''' <param name="openUserID"></param>
        ' ''' <param name="openCenID"></param>
        ' ''' <param name="PCID"></param>
        ' ''' <param name="version"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInTelephones</remarks>
        'Public Shared Function GetLocationCountInTelephones(ByVal LocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Query As String = "SELECT COUNT(TP_LOC_AL_ID) FROM Telephone_Info  WHERE REC_STATUS IN (0,1,2) AND TP_CEN_ID='" & inBasicParam.openCenID & "' AND TP_LOC_AL_ID  = '" & LocationID & "' "
        '    Return dbService.GetScalar(ConnectOneWS.Tables.TELEPHONE_INFO, Query, ConnectOneWS.Tables.TELEPHONE_INFO.ToString(), inBasicParam)
        'End Function

        ''' <summary>
        ''' Gets Location Count In Vehicles
        ''' </summary>
        ''' <param name="LocationID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AssetLocations_GetLocationCountInVehicles</remarks>
        Public Shared Function GetLocationCountInVehicles(ByVal inParam As Param_Check_Location_Count, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN CEN_ID = " & inBasicParam.openCenID.ToString & " THEN 0 ELSE 1 END AS SR, COUNT(VI_LOC_AL_ID), MAX(CEN_UID) AS CEN_UID FROM Vehicles_Info  AS VI INNER JOIN CENTRE_INFO AS CI ON VI_CEN_ID = CEN_ID  WHERE VI_LOC_AL_ID  = '" & inParam.LocationID & "' "
            If inParam.Exclude_Sold_TF Then Query += " AND COALESCE(VI.REC_ID,'') NOT IN (SELECT TP.TR_REF_ID FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN ( SELECT REC_ID FROM Vehicles_Info WHERE REC_STATUS IN (0,1,2)) AND TM.TR_CODE=11 and ( TM.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TM.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TM.TR_CEN_ID))) " & _
                                                                       " AND COALESCE(VI.REC_ID,'') NOT IN (SELECT TR_REF_OTHERS FROM Transaction_Info AS TI WHERE TI.REC_STATUS IN (0,1,2)  AND TI.TR_REF_OTHERS IN ( SELECT REC_ID FROM Vehicles_Info WHERE REC_STATUS IN (0,1,2)) AND TI.TR_CODE=15 AND TR_SR_NO =2 AND TR_TYPE='CREDIT' and ( TI.TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR TI.TR_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = TI.TR_CEN_ID))) "
            Query += " GROUP BY  CEN_ID ORDER BY SR "
            Return dbService.List(ConnectOneWS.Tables.VEHICLES_INFO, Query, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Asset_Movement_Logs(ByVal AssetRecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "AI_RECID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, IIf(AssetRecID.Length > 0, AssetRecID, DBNull.Value)}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.[String]}
            Dim lengths As Integer() = {5, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_ChangeAssetLocation_Logs", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

    End Class
End Namespace
