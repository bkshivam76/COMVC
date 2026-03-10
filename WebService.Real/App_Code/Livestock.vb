Imports System.Data

Namespace Real
    <Serializable>
    Public Class Livestock
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetList_Livestock
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_LiveStock
            Public ItemID As String
            Public Name As String
            Public Year As String
            Public Amount As Double
            Public Insurance As String
            Public InsuranceID As String = ""
            Public PolicyNo As String
            Public InsAmount As Double
            Public InsuranceDate As String
            Public LocationID As String
            Public OtherDetails As String
            Public Status_Action As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRIDAndTRSrNo_LiveStock : Inherits Parameter_Insert_LiveStock
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdLsID As String ' Contains old RecId of LS that is being re-posted(updated)
            Public screen As ConnectOneWS.ClientScreen
            Public RecID As String = Guid.NewGuid.ToString
        End Class
        <Serializable>
        Public Class Parameter_Update_LiveStock
            Public ItemID As String
            Public Name As String
            Public Year As String
            Public Amount As Double
            Public Insurance As String
            Public InsuranceID As String
            Public PolicyNo As String
            Public InsAmount As Double
            Public InsuranceDate As String
            Public LocationID As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Livestock_GetList_Common
            Public LS_Rec_ID As String
            Public LS_TR_ID As String
            Public LS_Item_ID As String
            Public Cen_ID As Integer
        End Class
        <Serializable>
        Public Class Param_LS_GetTransactions
            Public Rec_IDs As String = ""
            Public YearID As Integer
        End Class

#End Region
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
        ''' <remarks>RealServiceFunctions.Livestock_UpdateAssetLocationIfNotPresent</remarks>
        Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Live_Stock_Info SET LS_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(LS_LOC_AL_ID,'')=''"
            dbService.Update(ConnectOneWS.Tables.LIVE_STOCK_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Get LiveStock List for Current Center 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_GetList_Livestock, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT '' AS 'ITEM_NAME',LS_ITEM_ID,LS_NAME AS 'Livestock Name' ,LS_BIRTH_YEAR AS 'Birth Year' ,  LS_INSURANCE as Insurance,'' AS 'Insurance Company',COALESCE(LS_INSURANCE_ID,'') AS INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_DATE,LS_INS_AMT,COALESCE(LS_AMT,0) as LS_AMT,COALESCE(LS_AMT,0) as 'Curr Value','' AS AL_LOC_NAME,LS_LOC_AL_ID,LS_OTHER_DETAIL,'Un-Sold' AS 'Sale Status','' as 'Sale Date',CASE WHEN LS_TR_ID IS NULL  OR LS_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE LS_TR_ID END AS TR_ID,LS_COD_YEAR_ID AS YearID,REC_ID AS ID , CASE WHEN LS_TR_ID IS NULL OR LS_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as 'Entry Type'," & Common.Remarks_Detail("Live_Stock_Info", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Live_Stock_Info") & "" & _
                            " FROM Live_Stock_Info " & _
                            " Where   REC_STATUS IN (0,1,2) AND LS_CEN_ID=" & inBasicParam.openCenID.ToString & "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID)) AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") ; "
            Return dbService.List(ConnectOneWS.Tables.LIVE_STOCK_INFO, onlineQuery, ConnectOneWS.Tables.LIVE_STOCK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Livestock Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Assets.Param_GetProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Livestock_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 255}
            Return dbService.ListFromSP(Param.TableName, SPName, Param.TableName.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        'CommonFunction
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Object = Nothing) As DataTable
            Dim nextParam As Param_Livestock_GetList_Common = New Param_Livestock_GetList_Common()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    nextParam.LS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    nextParam.LS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    nextParam.LS_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    nextParam.LS_Rec_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    nextParam.LS_Item_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    nextParam = Param
            End Select
            Return GetList_Common_ByRecID(inBasicParam, nextParam)
        End Function

        Public Shared Function GetList_Common_ByRecID(inBasicParam As ConnectOneWS.Basic_Param, ByVal Param As Param_Livestock_GetList_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = "SELECT LS_TR_ID,LS_TR_ITEM_SRNO,LS_LOC_AL_ID,LS_NAME,LS_BIRTH_YEAR,LS_INSURANCE,LS_INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_AMT,LS_INS_DATE FROM Live_Stock_Info WHERE REC_STATUS IN (0,1,2) AND LS_TR_ID  ='" & Param.LS_TR_ID & "' ORDER BY LS_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = "SELECT LS_TR_ID,LS_TR_ITEM_SRNO,LS_LOC_AL_ID,LS_NAME,LS_BIRTH_YEAR,LS_INSURANCE,LS_INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_AMT,LS_INS_DATE,REC_ID FROM Live_Stock_Info WHERE REC_STATUS IN (0,1,2) AND LS_TR_ID  ='" & Param.LS_TR_ID & "' ORDER BY LS_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "SELECT ITEM_NAME AS Item, LS_NAME as Name, LS_BIRTH_YEAR AS 'BIRTH YEAR',COALESCE(LS_AMT,0) AS 'Org Value' ,COALESCE(LS_AMT,0) AS 'Curr Value' ,LS.REC_ID FROM live_stock_info AS LS INNER JOIN item_info as ii on LS.LS_ITEM_ID = ii.REC_ID WHERE LS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LS_ITEM_ID  ='" & Param.LS_Item_ID & "' and LS.REC_STATUS IN (0,1,2)  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID)) AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.LS_Rec_ID Is Nothing Then
                        Query += " AND LS.REC_ID = '" & Param.LS_Rec_ID & "'"
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT  '' AS 'REF_ITEM',LS_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',LS_NAME + ', Birth Year: ' + LS_BIRTH_YEAR as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0 AS 'SALE_QTY',REC_EDIT_ON  FROM Live_Stock_Info  Where   REC_STATUS IN (0,1,2) AND LS_CEN_ID=" & inBasicParam.openCenID.ToString & "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID)) AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.LS_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.LS_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = " SELECT  '' AS 'REF_ITEM',LS_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',LS_NAME + ', Birth Year: ' + LS_BIRTH_YEAR as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0 AS 'SALE_QTY',LS_LOC_AL_ID AS REF_LOC_ID, REC_EDIT_ON  FROM Live_Stock_Info  Where   REC_STATUS IN (0,1,2) AND LS_CEN_ID=" & Param.Cen_ID.ToString & "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & "  and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID)) AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.LS_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.LS_Rec_ID & "' "
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.LIVE_STOCK_INFO, Query, ConnectOneWS.Tables.LIVE_STOCK_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Livestock_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  REC_ID AS ID,LS_ITEM_ID,LS_NAME,LS_BIRTH_YEAR,LS_AMT,LS_INSURANCE,LS_INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_DATE,LS_OTHER_DETAIL,LS_LOC_AL_ID FROM Live_Stock_Info " & _
                    " Where   REC_STATUS IN (0,1,2) AND LS_CEN_ID=" & inBasicParam.openCenID.ToString & "  " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.LIVE_STOCK_INFO, Query, ConnectOneWS.Tables.LIVE_STOCK_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Livestock_GetTransactions</remarks>
        Public Shared Function GetTransactions(ByVal inParam As Param_LS_GetTransactions, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT TP.TR_REF_ID, SUM(TM.TR_SALE_QTY) AS 'Sale Quantity',MAX(TM.TR_SALE_DATE) AS 'Sale Date'  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN (" & inParam.Rec_IDs & ") AND TM.TR_CODE=11 AND TM.TR_COD_YEAR_ID = " & inParam.YearID.ToString & "   GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Livestock_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_LiveStock, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            If InParam.InsuranceID.Trim.Length = 0 Then InParam.InsuranceID = "NULL" Else InParam.InsuranceID = "'" & InParam.InsuranceID & "'"
            Dim OnlineQuery As String = "INSERT INTO Live_Stock_Info(LS_CEN_ID,LS_COD_YEAR_ID,LS_ITEM_ID,LS_NAME,LS_BIRTH_YEAR,LS_AMT,LS_INSURANCE,LS_INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_AMT,LS_INS_DATE,LS_LOC_AL_ID,LS_OTHER_DETAIL," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,LS_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  "'" & InParam.Name & "', " & _
                                                  " '" & InParam.Year & "' , " & _
                                                  " " & InParam.Amount.ToString() & " , " & _
                                                  "'" & InParam.Insurance & "', " & _
                                                  " " & InParam.InsuranceID & " , " & _
                                                  "'" & InParam.PolicyNo & "', " & _
                                                  " " & InParam.InsAmount.ToString() & ", " & _
                                                  " " & If(IsDate(InParam.InsuranceDate), "'" & Convert.ToDateTime(InParam.InsuranceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InParam.LocationID & "', " & _
                                                  "'" & InParam.OtherDetails & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.LIVE_STOCK_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' InsertTRIDAndTRSrNo
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Livestock_InsertTRIDAndTRSrNo</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_LiveStock, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RecID As String = Guid.NewGuid.ToString
            If InParam1.InsuranceID Is Nothing Then
                InParam1.InsuranceID = "NULL"
            Else
                If InParam1.InsuranceID.Trim.Length = 0 Then InParam1.InsuranceID = "NULL" Else InParam1.InsuranceID = "'" & InParam1.InsuranceID & "'"
            End If
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Live_Stock_Info(LS_CEN_ID,LS_COD_YEAR_ID,LS_ITEM_ID,LS_NAME,LS_BIRTH_YEAR,LS_AMT,LS_INSURANCE,LS_INSURANCE_ID,LS_INS_POLICY_NO,LS_INS_AMT,LS_INS_DATE,LS_LOC_AL_ID,LS_OTHER_DETAIL,LS_TR_ID,LS_TR_ITEM_SRNO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,LS_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.ItemID & "', " &
                                                  "'" & InParam1.Name & "', " &
                                                  " '" & InParam1.Year & "' , " &
                                                  " " & InParam1.Amount.ToString() & " , " &
                                                  "'" & InParam1.Insurance & "', " &
                                                  " " & InParam1.InsuranceID & " , " &
                                                  "'" & InParam1.PolicyNo & "', " &
                                                  " " & InParam1.InsAmount.ToString() & ", " &
                                                  " " & If(IsDate(InParam1.InsuranceDate), "'" & Convert.ToDateTime(InParam1.InsuranceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InParam1.LocationID & "', " &
                                                  "'" & InParam1.OtherDetails & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  " " & InParam1.TxnSrNo & " , " &
                                                  "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.LIVE_STOCK_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
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
        ''' <remarks>RealServiceFunctions.Livestock_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_LiveStock, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.InsuranceID.Trim.Length = 0 Then UpParam.InsuranceID = "NULL" Else UpParam.InsuranceID = "'" & UpParam.InsuranceID & "'"
            Dim OnlineQuery As String = " UPDATE Live_Stock_Info SET " & _
                                         "LS_ITEM_ID         ='" & UpParam.ItemID & "', " & _
                                         "LS_NAME            ='" & UpParam.Name & "', " & _
                                         "LS_BIRTH_YEAR      = '" & UpParam.Year & "', " & _
                                         "LS_AMT             = " & UpParam.Amount.ToString() & ", " & _
                                         "LS_INSURANCE       ='" & UpParam.Insurance & "', " & _
                                         "LS_INSURANCE_ID    = " & UpParam.InsuranceID & " , " & _
                                         "LS_INS_POLICY_NO   ='" & UpParam.PolicyNo & "', " & _
                                         "LS_INS_AMT         = " & UpParam.InsAmount.ToString() & ", " & _
                                         "LS_INS_DATE        = " & If(IsDate(UpParam.InsuranceDate), "'" & Convert.ToDateTime(UpParam.InsuranceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "LS_LOC_AL_ID      ='" & UpParam.LocationID & "', " & _
                                         "LS_OTHER_DETAIL    ='" & UpParam.OtherDetails & "', " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.LIVE_STOCK_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateInsuranceDetail(ByVal UpParam As Parameter_Update_LiveStock, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.InsuranceID.Trim.Length = 0 Then UpParam.InsuranceID = "NULL" Else UpParam.InsuranceID = "'" & UpParam.InsuranceID & "'"
            Dim OnlineQuery As String = " UPDATE Live_Stock_Info SET " & _
                                         "LS_INSURANCE       ='" & UpParam.Insurance & "', " & _
                                         "LS_INSURANCE_ID    = " & UpParam.InsuranceID & " , " & _
                                         "LS_INS_POLICY_NO   ='" & UpParam.PolicyNo & "', " & _
                                         "LS_INS_AMT         = " & UpParam.InsAmount.ToString() & ", " & _
                                         "LS_INS_DATE        = " & If(IsDate(UpParam.InsuranceDate), "'" & Convert.ToDateTime(UpParam.InsuranceDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.LIVE_STOCK_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
End Namespace
