Imports System.Data
Imports System
Imports Real.Vouchers

Namespace Real
    <Serializable>
    Public Class WIP_Profile
#Region "Parameter Classes"
        <Serializable>
        Public Class Param_GetProfileListing_WIP
            Public Next_YearID As Integer
            Public Prev_YearId As Integer
            Public WIP_RecID As String
            Public WIP_LED_ID As String
            Public TR_M_ID As String
        End Class
        <Serializable>
        Public Class Param_WIP_GetList_Common
            Public WIP_TR_ID As String
            Public WIP_REC_ID As String
            'Public Item_ID As String
            'Public Cen_ID As String
        End Class
        <Serializable>
        Public Class Param_Insert_WIP_Profile
            Public Reference As String
            Public TDate As String
            Public Amount As Decimal
            Public Status_Action As String
            Public RecID As String
            Public LedID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_WIP_Profile
            Public Reference As String
            Public LedID As String
            Public Amount As Double
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_InsertTRIDAndTRSrNo_WIP_Profile : Inherits Param_Insert_WIP_Profile
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdWipID As String ' Contains old RecId of WIP that is being re-posted(updated)
            Public Screen As ConnectOneWS.ClientScreen
        End Class
        <Serializable>
        Public Class Param_GetTxnReport
            Public Led_ID As String
            Public InsttId As Object
            Public YearID As Integer
            Public FromDate As Date
            Public ToDate As Date
            Public WIP_ID As String = ""
        End Class

#End Region

        Public Shared Function GetProfileListing(Param As Param_GetProfileListing_WIP, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_WIP_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "WIP_RECID", "WIP_LED_ID", "TR_M_ID", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.WIP_RecID = "" Then Param.WIP_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.WIP_LED_ID = "" Then Param.WIP_LED_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.WIP_RecID, Param.WIP_LED_ID, Param.TR_M_ID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 5, 36, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.WIP_INFO, SPName, ConnectOneWS.Tables.WIP_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Object = Nothing) As DataTable
            Dim nextParam As Param_WIP_GetList_Common = New Param_WIP_GetList_Common()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                '    nextParam.AI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    nextParam.WIP_TR_ID = Param
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    '    nextParam.AI_REC_ID = Param
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    '    nextParam.AI_TR_ID = Param
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    '    nextParam.Item_ID = Param
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    '    nextParam = Param
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_WIP_Finalization
                    '    nextParam.AI_TR_ID = Param
            End Select
            Return GetList_Common_ByRecID(inBasicParam, nextParam)
        End Function

        Public Shared Function GetTxn_Report(ByVal inParam As Param_GetTxnReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"LEDGERID", "CENID", "INSTTID", "YEARID", "X_FROM_DATE", "X_TO_DATE", "WIP_ID"}
            Dim values As Object() = {inParam.Led_ID, inBasicParam.openCenID, inParam.InsttId, inParam.YearID, inParam.FromDate.ToString(Common.Server_Date_Format_Short), inParam.ToDate.ToString(Common.Server_Date_Format_Short), inParam.WIP_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.[String], DbType.Int32, DbType.[String], DbType.Int32, DbType.DateTime, DbType.DateTime, DbType.[String]}
            Dim lengths As Integer() = {5, 5, 5, 4, 20, 20, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_rpt_WIP_Txn_Report", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function


        Public Shared Function GetList_Common_ByRecID(inBasicParam As ConnectOneWS.Basic_Param, ByVal Param As Param_WIP_GetList_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                '    Query = "select AI_TR_ID,AI_TR_ITEM_SRNO,AI_LOC_AL_ID,AI_TYPE,AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_PUR_DATE,AI_WARRANTY from ASSET_Info where  REC_STATUS IN (0,1,2) AND AI_TR_ID= '" & Param.AI_TR_ID & "' ORDER BY AI_TR_ITEM_SRNO"  'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = "SELECT WIP_TR_ID, WIP_TR_ITEM_SRNO, WIP_LED_ID, WIP_REF, WIP_AMT,REC_ID FROM WIP_INFO WHERE REC_STATUS IN (0,1,2) AND WIP_TR_ID = '" & Param.WIP_TR_ID & "' ORDER BY WIP_TR_ITEM_SRNO"  'RecID
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    '    Query = "select ii.ITEM_NAME AS Item, COALESCE(AI_MAKE,'') AS Make, COALESCE(AI_MODEL,'') AS Model, COALESCE(AI_QTY,0) AS 'Org Qty', COALESCE(AI_QTY,0) AS 'Curr Qty', COALESCE(AI_PUR_AMT,0) as 'Org Value', COALESCE(AI_PUR_AMT,0) as 'Curr Value', ai.REC_ID FROM ASSET_INFO as ai INNER JOIN ITEM_INFO as ii on ai.AI_ITEM_ID = ii.REC_ID WHERE AI_TYPE='ASSET' AND AI_CEN_ID='" & inBasicParam.openCenID & "' AND ai.AI_ITEM_ID= '" & Param.Item_ID & "' and ai.REC_STATUS IN (0,1,2)  and AI_COD_YEAR_ID <='" & inBasicParam.openYearID & "' and ( AI_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID)  OR AI_COD_YEAR_ID = '" & inBasicParam.openYearID & "'  )"
                    '    If Not Param.AI_REC_ID Is Nothing Then 'added for multiuser check
                    '        Query += " AND ai.REC_ID = '" & Param.AI_REC_ID & "' "
                    '    End If

                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_WIP_Finalization
                    '    Query = "select AI_TR_ID,AI_ITEM_ID,AI_LOC_AL_ID, COALESCE(AI_QTY,0) AS QTY, AI_MAKE,AI_MODEL,AI_SERIAL_NO,AI_WARRANTY,ai_rate,AI_OTHER_DETAIL,REC_ID from ASSET_Info where  REC_STATUS IN (0,1,2) AND AI_TR_ID= '" & Param.AI_TR_ID & "'"  'RecID

            End Select
            Return dbService.List(ConnectOneWS.Tables.WIP_INFO, Query, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Insert(ByVal InParam As Param_Insert_WIP_Profile, inBasicParam As ConnectOneWS.Basic_Param, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            If String.IsNullOrWhiteSpace(InParam.RecID) = False Then
                RecID = InParam.RecID
            End If
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO WIP_INFO(WIP_CEN_ID,WIP_LED_ID,WIP_COD_YEAR_ID,WIP_REF,WIP_AMT,REC_ADD_ON,REC_ADD_BY," &
                                        "REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,WIP_ORG_REC_ID" &
                                                ") VALUES(" &
                                                "" & inBasicParam.openCenID.ToString & "," &
                                                "'" & InParam.LedID & "'," &
                                                "" & inBasicParam.openYearID.ToString & "," &
                                                "'" & InParam.Reference & "'," &
                                                " " & InParam.Amount & ", " &
                                                "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "','" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, inBasicParam, InParam.RecID)
            Return True
        End Function

        Public Shared Function Insert(ByVal InParam As Param_InsertTRIDAndTRSrNo_WIP_Profile, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO WIP_INFO(WIP_CEN_ID,WIP_LED_ID,WIP_COD_YEAR_ID,WIP_REF,WIP_AMT,WIP_TR_ID, WIP_TR_ITEM_SRNO,REC_ADD_ON,REC_ADD_BY," &
                                        "REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,WIP_ORG_REC_ID" &
                                                ") VALUES(" &
                                                "" & inBasicParam.openCenID.ToString & "," &
                                                "'" & InParam.LedID & "'," &
                                                "" & inBasicParam.openYearID.ToString & "," &
                                                "'" & InParam.Reference & "'," &
                                                " " & InParam.Amount & ", " &
                                                "'" & InParam.TxnID & "', " &
                                                " " & InParam.TxnSrNo & ", " &
                                                "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & RecID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function Update(ByVal UpParam As Parameter_Update_WIP_Profile, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE WIP_INFO SET " &
                                     "WIP_LED_ID        ='" & UpParam.LedID & "', " &
                                     "WIP_REF           ='" & UpParam.Reference & "', " &
                                     "WIP_AMT          ='" & UpParam.Amount & "', " &
                                    "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                    "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                    "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

    End Class
    <Serializable>
    Public Class WIP_Creation_Vouchers

#Region "Param Classes"
        <Serializable>
        Public Class Param_GetDuplicateReferenceCount
            Public Tag As Integer
            Public Reference As String
            Public RecID As String
            Public iTxnM_ID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetWIP_Dependencies
            Public Txn_M_ID As String
            Public WIP_ID As String = ""
            Public Next_YearID As Integer
        End Class
#End Region
        Public Shared Function GetCountOfReferencesByLedID(ByVal Led_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM WIP_INFO WHERE WIP_LED_ID = '" & Led_ID & "' AND REC_STATUS IN (0,1,2) AND WIP_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetRefCreationDateByWIPID(ByVal WIP_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT TR_DATE, WIP_REF FROM TRANSACTION_D_MASTER_INFO AS TI INNER JOIN WIP_INFO AS WI ON WIP_TR_ID = TI.REC_ID AND  WI.REC_ID = '" & WIP_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetDuplicateReferenceCount(Param As Param_GetDuplicateReferenceCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            If Val(Param.Tag) = 1 Then OnlineQuery = "SELECT COUNT(REC_ID ) from WIP_INFO  WHERE REC_STATUS IN (0,1,2) AND WIP_CEN_ID =" & inBasicParam.openCenID.ToString & " AND UPPER(WIP_REF )  = '" & Param.Reference.ToUpper & "' and ( WIP_COD_YEAR_ID  =" & inBasicParam.openYearID.ToString & " OR WIP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = WIP_CEN_ID)) AND (WIP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = WIP_CEN_ID) OR WIP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  "
            If Val(Param.Tag) = 2 Then OnlineQuery = "SELECT COUNT(REC_ID) FROM WIP_INFO   WHERE REC_STATUS IN (0,1,2) AND WIP_CEN_ID =" & inBasicParam.openCenID.ToString & " AND UPPER(WIP_REF )  = '" & Param.Reference.ToUpper & "' AND ( WIP_COD_YEAR_ID  =" & inBasicParam.openYearID.ToString & " OR WIP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = WIP_CEN_ID)) AND REC_ID <> '" & Param.RecID & "' AND (WIP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = WIP_CEN_ID) OR WIP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            If Param.iTxnM_ID.Length > 0 Then OnlineQuery = OnlineQuery + " AND WIP_TR_ID <> '" & Param.iTxnM_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetWIP_Dependencies(ByVal Param As Param_GetWIP_Dependencies, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Param.WIP_ID = "" Then Param.WIP_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim paramters As String() = {"CENID", "YEARID", "NEXT_YEARID", "TR_M_ID", "WIP_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Next_YearID, Param.Txn_M_ID, Param.WIP_ID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "sp_get_WIP_Dependencies_Txn", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

    End Class
    <Serializable>
    Public Class Voucher_WIP_Finalization
#Region "Param Classes"
        <Serializable>
        Public Class Param_InsertMasterInfo_Voucher_WIPFinalization
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public TotalAmount As Double
            Public Status_Action As String
            Public RecID As String
            'Public PurposeID As String
        End Class
        <Serializable>
        Public Class Param_Insert_Voucher_WIPFinalization
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Finalized_Amount As Double
            Public Mode As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public CrossRefID As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            'Public PurposeID As String
        End Class
        <Serializable>
        Public Class Param_GetExistingAssetListing
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TR_M_ID As String
            Public Item_ID As String
        End Class
        <Serializable>
        Public Class Param_IsUpdatingSameAsset
            Public Asset_ItemId As String
            Public MasterID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_Voucher_WIPFinalization
            Public VNo As String
            Public TDate As String
            Public Finalized_Amount As Double
            'Public Status_Action As String
            'Public PurposeID As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_Voucher_WIPFinalization
            Public param_InsertMaster As Param_InsertMasterInfo_Voucher_WIPFinalization
            Public InsertCr() As Param_Insert_Voucher_WIPFinalization = Nothing
            Public InsertDr As Param_Insert_Voucher_WIPFinalization = Nothing
            Public inAssets_Insert As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public PurposeID As String
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_Voucher_WIPFinalization
            Public Udpate_Master As Parameter_UpdateMaster_Voucher_WIPFinalization
            Public Update_Assets As Assets.Parameter_Update_Assets
            Public MID_DeleteAssets As String
            Public MID_Delete As String
            Public inAssets_Insert As Assets.Parameter_InsertTRIDAndTRSrNo_Assets = Nothing
            Public InsertCr() As Param_Insert_Voucher_WIPFinalization = Nothing
            Public InsertDr As Param_Insert_Voucher_WIPFinalization = Nothing
            Public PurposeID As String
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_Voucher_WIPFinalization
            Public MID_DeleteAssets As String
            Public MID_Delete As String
            Public MID_DeleteMaster As String
        End Class
        <Serializable>
        Public Class Param_Get_WIP_Outstanding_References
            Public Next_YearID As Integer
            Public Prev_YearId As Integer
            Public WIP_RecID As String
            Public WIP_LED_ID As String
            Public TR_M_ID As String
        End Class
        <Serializable>
        Public Class Param_GetListOfFinalizedAssets
            Public WIP_Led_ID As String
            Public Curr_Ins_Id As String
        End Class


#End Region

        'Not used anywhere
        Public Shared Function GetListOfReferences(WIP_Led_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ', " & Common.Remarks_Detail("wi", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("wi") & "
            Dim OnlineQuery As String = " SELECT ROW_NUMBER() OVER (ORDER BY wi.WIP_REF) As 'Sr.', [WIP_REF] AS 'Reference', [WIP_AMT] as 'WIP_Amount', REC_ID  AS 'WIP_RecID' FROM WIP_INFO as wi " &
                                        " Where   wi.REC_STATUS IN (0,1,2) AND WIP_CEN_ID=" & inBasicParam.openCenID.ToString & " and WIP_LED_ID = '" & WIP_Led_ID & "' " &
                                        " and WIP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( WIP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR WIP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = WIP_CEN_ID)) " &
                                        " AND (WIP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = WIP_CEN_ID) OR WIP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Return dbService.List(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        End Function

        'Public Shared Function GetListOfWIPLedgers(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    'Dim OnlineQuery As String = "SELECT LED_ID,LED_NAME FROM acc_ledger_info WHERE LED_NAME LIKE '%WIP%'"
        '    Dim OnlineQuery As String = "select i.rec_id as Txn_Cr_ItemId, i.item_name as 'Txn_ItemName', acc.LED_ID,acc.LED_NAME  from acc_ledger_info as acc left outer join item_info as i on i.ITEM_LED_ID = acc.LED_ID where acc.LED_NAME like '%(wip)%' and i.item_name like '%(WIP) Finalization%'"
        '    Return dbService.List(ConnectOneWS.Tables.ACC_LEDGER_INFO, OnlineQuery, ConnectOneWS.Tables.ACC_LEDGER_INFO.ToString(), inBasicParam)
        'End Function

        Public Shared Function GetListOfFinalizedAssets(Param As Param_GetListOfFinalizedAssets, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = "select i.rec_id as Asset_Item_ID, wim.* from WIP_Item_Mapping as wim left outer join item_info as i on i.item_name = wim.Finalized_Asset  where WIP_LED_ID =  '" & WIP_Led_ID & "'"

            Dim OnlineQuery As String = "SELECT I.ITEM_NAME AS 'Finalized_Asset', ac1.led_type AS LED_TYPE, ac2.led_type AS 'CON_LED_TYPE', I.ITEM_CON_MIN_VALUE, I.ITEM_CON_MAX_VALUE, I.rec_id AS 'Asset_Item_ID', I.item_LED_ID as 'Asset_LED_ID', I.ITEM_CON_LED_ID " &
                                        "FROM   item_info AS i " &
                                        "INNER JOIN wip_item_mapping AS wi ON wi.[ASSET_ITEM_REC_ID] = i.rec_id " &
                                        "INNER JOIN Item_Mapping as im on i.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & Param.Curr_Ins_Id & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "" &
                                        "INNER JOIN acc_ledger_info AS ac1 ON ac1.led_id = i.item_led_id " &
                                        "LEFT OUTER JOIN acc_ledger_info AS ac2 ON ac2.led_id = i.item_con_led_id  where wi.WIP_LED_ID = '" & Param.WIP_Led_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.WIP_ITEM_MAPPING, OnlineQuery, ConnectOneWS.Tables.WIP_ITEM_MAPPING.ToString(), inBasicParam)
        End Function

        Public Shared Function GetExistingAssetsListing(Param As Param_GetExistingAssetListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Existing_Asset_Listing_WIP"
            Dim params() As String = {"CENID", "YEARID", "ITEM_ID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Item_ID = "" Then Param.Item_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Item_ID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, Param.TR_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_WIP_Outstanding_References(Param As Param_Get_WIP_Outstanding_References, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_WIP_Outstanding_References"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "WIP_LED_ID", "WIP_RECID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.WIP_RecID = "" Then Param.WIP_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.WIP_LED_ID = "" Then Param.WIP_LED_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.WIP_LED_ID, Param.WIP_RecID, Param.TR_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 5, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.WIP_INFO, SPName, ConnectOneWS.Tables.WIP_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetFinalizedAmounts(MasterID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select TR_AMOUNT,TR_TRF_CROSS_REF_ID from transaction_info where REC_STATUS IN (0,1,2)  and  TR_M_ID = '" & MasterID & "' and tr_type = 'CREDIT' AND TR_CODE = 17"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        'Public Shared Function GetCountOfExistingAssetsByItemID(ByVal ItemID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim OnlineQuery As String = ""
        '    Return dbService.GetScalar(ConnectOneWS.Tables.WIP_INFO, OnlineQuery, ConnectOneWS.Tables.WIP_INFO.ToString(), inBasicParam)
        'End Function

        Private Shared Function InsertMasterInfo(ByVal InMInfo As Param_InsertMasterInfo_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_SUB_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InMInfo.TxnCode & "'," &
                                                  "'" & InMInfo.VNo & "', " &
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InMInfo.TotalAmount & "," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        Private Shared Function Insert(ByVal InParam As Param_Insert_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim MasterTxnID As String = InParam.MasterTxnID
            If InParam.Cr_Led_ID.Length = 0 Or InParam.Cr_Led_ID = "NULL" Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Or InParam.Dr_Led_ID = "NULL" Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.CrossRefID.Length = 0 Or InParam.CrossRefID = "NULL" Then InParam.CrossRefID = "NULL" Else If Not InParam.CrossRefID.StartsWith("'") Then InParam.CrossRefID = "'" & InParam.CrossRefID & "'"
            If MasterTxnID.Length = 0 Or MasterTxnID = "NULL" Then MasterTxnID = "NULL" Else If Not MasterTxnID.StartsWith("'") Then MasterTxnID = "'" & MasterTxnID & "'"
            If InParam.Mode.Length = 0 Or InParam.Mode = "NULL" Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.SrNo.Length = 0 Or InParam.SrNo = "NULL" Then InParam.SrNo = "NULL"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID," &
                                        "TR_MODE,TR_AMOUNT,TR_NARRATION,TR_REFERENCE,TR_TRF_CROSS_REF_ID,TR_M_ID,TR_SR_NO," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                        ") VALUES(" &
                                                            "" & inBasicParam.openCenID.ToString & "," &
                                                            "" & inBasicParam.openYearID.ToString & "," &
                                                            " " & InParam.TransCode & "," &
                                                            "'" & InParam.VNo & "', " &
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                            "'" & InParam.ItemID & "', " &
                                                            "'" & InParam.Type & "', " &
                                                            " " & InParam.Cr_Led_ID & " , " &
                                                            " " & InParam.Dr_Led_ID & " , " &
                                                            " " & InParam.Mode & " , " &
                                                            " " & InParam.Finalized_Amount & ", " &
                                                            "'" & InParam.Narration & "', " &
                                                            "'" & InParam.Reference & "', " &
                                                            " " & InParam.CrossRefID & " , " &
                                                            " " & MasterTxnID & " , " &
                                                            " " & InParam.SrNo & " , " &
                                                            "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            'If InParam.PurposeID.Length > 10 Then
            '    InsertPurpose(InParam.RecID, InParam.PurposeID, InParam.Finalized_Amount, inBasicParam)
            'End If
            Return True
        End Function
        Public Shared Function InsertPurpose(ByVal TxnID As String, PurposeID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',1, '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Dim ID As String = Guid.NewGuid.ToString()
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO, " &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & TxnID & "'," &
                                                  "'" & PurposeID & "', " &
                                                  " " & Amount.ToString() & ", " &
                                                  " NULL, " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & ID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, ID, )
            Return True
        End Function
        'Public Shared Function 
        Private Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " &
                                            " TR_VNO         ='" & UpParam.VNo & "', " &
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            " TR_SUB_AMT     = " & UpParam.Finalized_Amount & ", " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            ''Purpose
            'dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & UpParam.RecID & "'", inBasicParam)
            'If UpParam.PurposeID.Length > 10 Then
            '    InsertPurpose(UpParam.RecID, UpParam.PurposeID, UpParam.Finalized_Amount, inBasicParam)
            'End If

            Return True
        End Function

        Public Shared Function Insert_WIP_Finalization_Txn(inParam As Param_Txn_Insert_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each upVoucher_Update As Param_Insert_Voucher_WIPFinalization In inParam.InsertCr
                If Not upVoucher_Update Is Nothing Then
                    If Not upVoucher_Update.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Dr_Led_ID)
                    If Not upVoucher_Update.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                    'If Not upVoucher_Update.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
                End If
            Next

            If Not inParam.InsertDr Is Nothing Then
                If Not inParam.InsertDr.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.InsertDr.TDate), inParam.InsertDr.Dr_Led_ID)
                If Not inParam.InsertDr.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inParam.InsertDr.TDate), inParam.InsertDr.Cr_Led_ID)
                'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                'If Not upVoucher_Update.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
            End If

            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.inAssets_Insert Is Nothing Then
                If Not Assets.Insert(inParam.inAssets_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Param_Insert_Voucher_WIPFinalization In inParam.InsertCr
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.InsertDr Is Nothing Then
                If Not Insert(inParam.InsertDr, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If inParam.PurposeID.Length > 0 Then
                InsertPurpose(inParam.param_InsertMaster.RecID, inParam.PurposeID, inParam.param_InsertMaster.TotalAmount, inBasicParam)
            End If

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(inParam.InsertSplVchrRefs, inParam.param_InsertMaster.RecID, Nothing, Nothing, inBasicParam)
            'If inParam.InsertSplVchrRefs IsNot Nothing Then
            '    If inParam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In inParam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, inParam.param_InsertMaster.RecID, inParam.param_InsertMaster.TotalAmount, inBasicParam)
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function InsertReference(Param As Parameter_InsertSplVchrRef_Vouchers, ByVal Tr_M_ID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'"
            Dim ID As String = Guid.NewGuid.ToString()
            Dim OnlineQuery As String = "INSERT INTO transaction_d_reference_info(TR_CEN_ID, TR_COD_YEAR_ID, TR_M_ID, TXN_REC_ID, TR_SR_NO, " &
                                                  "TR_VOUCHER_REF, TR_AMOUNT, REC_ID, REC_ADD_ON, REC_ADD_BY)" &
                                                  " VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & Tr_M_ID & "'," &
                                                  " NULL, " &
                                                  " NULL, " &
                                                  "'" & Param.Task_Name & "', " &
                                                  " " & Amount.ToString() & ", '" & ID & "', " & Str & ")"
            'MsgBox(OnlineQuery)
            'Console.WriteLine(OnlineQuery)
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, OnlineQuery, inBasicParam, ID, )
            Return True
        End Function

        Public Shared Function Update_WIP_Finalization_Txn(upParam As Param_Txn_Update_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            For Each upVoucher_Update As Param_Insert_Voucher_WIPFinalization In upParam.InsertCr
                If Not upVoucher_Update Is Nothing Then
                    If Not upVoucher_Update.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Dr_Led_ID)
                    If Not upVoucher_Update.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                    'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                    'If Not upVoucher_Update.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
                End If
            Next

            If Not upParam.InsertDr Is Nothing Then
                If Not upParam.InsertDr.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.InsertDr.TDate), upParam.InsertDr.Dr_Led_ID)
                If Not upParam.InsertDr.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.InsertDr.TDate), upParam.InsertDr.Cr_Led_ID)
                'If Not upVoucher_Update.SUB_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Cr_Led_ID)
                'If Not upVoucher_Update.SUB_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.SUB_Dr_Led_ID)
                'If Not upVoucher_Update.Party1 <> Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upVoucher_Update.TDate), upVoucher_Update.Party1)
            End If
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.Udpate_Master.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            ''Using txn
            '' Using Common.GetConnectionScope()
            If Not upParam.Udpate_Master Is Nothing Then 'Master Updated
                If Not UpdateMaster(upParam.Udpate_Master, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & upParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not upParam.MID_Delete Is Nothing Then 'Actual txn deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.inAssets_Insert Is Nothing Then
                If Not Assets.Insert(upParam.inAssets_Insert, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Param_Insert_Voucher_WIPFinalization In upParam.InsertCr
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.InsertDr Is Nothing Then
                If Not Insert(upParam.InsertDr, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If

            'Purpose
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            If upParam.PurposeID.Length > 10 Then
                InsertPurpose(upParam.Udpate_Master.RecID, upParam.PurposeID, upParam.Udpate_Master.Finalized_Amount, inBasicParam)
                'InsertPurpose(inParam.param_InsertMaster.RecID, inParam.PurposeID, inParam.param_InsertMaster.TotalAmount, inBasicParam)
            End If

            '' End Using
            'Commit here 
            ' txn.Complete()
            ' End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            InsertSpecialVoucherReference(upParam.UpdateSplVchrRefs, upParam.Udpate_Master.RecID, Nothing, Nothing, inBasicParam)
            'If upParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If upParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In upParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, upParam.Udpate_Master.RecID, upParam.Udpate_Master.Finalized_Amount, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function Delete_WIP_Finalization_Txn(delParam As Param_Txn_Delete_Voucher_WIPFinalization, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            ' Using Common.GetConnectionScope()
            If Not delParam.MID_DeleteAssets Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_INFO, "AI_TR_ID    ='" & delParam.MID_DeleteAssets & "'", inBasicParam)
            End If
            If Not delParam.MID_Delete Is Nothing Then 'Actual txn deleted
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If


            'Purpose Deletion code
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_Delete & "'", inBasicParam)

            ' End Using
            'Commit here 
            '  txn.Complete()
            ' End Using
            Return True
        End Function
    End Class
End Namespace

