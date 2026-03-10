Imports System.Data

Namespace Real

#Region "Profile"
    <Serializable>
    Public Class Vehicles
#Region "Param Classes"
        <Serializable>
        Public Class Param_Vehicles_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Param_Vehicles_GetListByCondition
            Public OtherCondition As String
            Public openInsID As String
        End Class
        <Serializable>
        Public Class Param_Vehicles_GetList_Common
            Public VI_TR_ID As String
            Public VI_Rec_ID As String
            Public VI_ItemID As String
            Public Cen_ID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Insert_Vehicles
            Public ItemID As String
            Public Make As String
            Public Model As String
            Public Reg_No_Pattern As String
            Public Reg_No As String
            Public RegDate As String
            Public Amount As Double
            Public Ownership As String
            Public Ownership_AB_ID As String
            Public Doc_RC_Book As String
            Public Doc_Affidavit As String
            Public Doc_Will As String
            Public Doc_TRF_Letter As String
            Public DOC_FU_Letter As String
            Public Doc_Is_Others As String
            Public Doc_Others_Name As String
            Public Insurance_ID As String
            Public Ins_Policy_No As String
            Public Ins_Expiry_Date As String
            Public Location_ID As String
            Public Other_Details As String
            Public Status_Action As String
            Public Rec_ID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertTRIDAndTRSrNo_Vehicles : Inherits Parameter_Insert_Vehicles
            Public TxnID As String
            Public TxnSrNo As Integer
            Public UpdVehID As String ' Contains old RecId of Vehicle that is being re-posted(updated)
            Public Screen As ConnectOneWS.ClientScreen
            Public RecID As String = Guid.NewGuid.ToString
        End Class
        <Serializable>
        Public Class Parameter_Update_Vehicles
            Public ItemID As String
            Public Make As String
            Public Model As String
            Public Reg_No_Pattern As String
            Public Reg_No As String
            Public RegDate As String
            Public Amount As Double
            Public Ownership As String
            Public Ownership_AB_ID As String
            Public Doc_RC_Book As String
            Public Doc_Affidavit As String
            Public Doc_Will As String
            Public Doc_TRF_Letter As String
            Public DOC_FU_Letter As String
            Public Doc_Is_Others As String
            Public Doc_Others_Name As String
            Public Insurance_ID As String
            Public Ins_Policy_No As String
            Public Ins_Expiry_Date As String
            Public Location_ID As String
            Public Other_Details As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Get Vehicle List
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_Vehicles_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT '' AS 'ITEM_NAME',VI_ITEM_ID , VI_MAKE AS Make,VI_MODEL as Model,VI_REG_NO,VI_REG_DATE AS 'Date of First Registration',COALESCE(VI_AMOUNT,0) AS 'Opening Value',COALESCE(VI_AMOUNT,0) AS 'Curr Value',VI_OWNERSHIP as Ownership,VI_DOC_RC_BOOK,VI_DOC_AFFIDAVIT AS Affidavit ,VI_DOC_WILL AS Will,VI_DOC_TRF_LETTER AS 'Transfer Letter',VI_DOC_FU_LETTER AS 'Free Use Letter',VI_DOC_NAME AS 'Other Documents','' AS 'Insurance Company',COALESCE(VI_INSURANCE_ID,'') AS INSURANCE_ID,VI_INS_POLICY_NO,VI_INS_EXPIRY_DATE AS 'Expiry Date',VI_OTHER_DETAIL ,'' AS AL_LOC_NAME,VI_LOC_AL_ID ,VI_COD_YEAR_ID AS YearID,CASE WHEN VI_TR_ID IS NULL  OR VI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '' ELSE VI_TR_ID END AS TR_ID,REC_ID AS ID ,'Un-Sold' AS 'Sale Status','' as 'Sale Date',CASE WHEN VI_TR_ID IS NULL OR VI_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as 'Entry Type'," & Common.Remarks_Detail("Vehicles_Info", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Vehicles_Info") & "" & _
                                        " FROM Vehicles_Info " & _
                                        " Where   REC_STATUS IN (0,1,2) AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & "  and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") ; "
            Return dbService.List(ConnectOneWS.Tables.VEHICLES_INFO, onlineQuery, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Vehicle Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Assets.Param_GetProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_vehicle_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 255}
            Return dbService.ListFromSP(Param.TableName, SPName, Param.TableName.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Vehicle Record
        Public Shared Function GetRecord(inBasicParam As ConnectOneWS.Basic_Param, VI_RecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT VI_CEN_ID,VI_COD_YEAR_ID,VI_ITEM_ID,VI_MAKE,VI_MODEL,VI_MFG_DATE,VI_REG_NO,VI_REG_DATE,VI_AMOUNT,VI_OWNERSHIP," & _
                                        " (SELECT	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = VI_OWNERSHIP_AB_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") VI_OWNERSHIP_AB_ID," & _
                                        " VI_DOC_RC_BOOK, VI_DOC_AFFIDAVIT, VI_DOC_WILL, VI_DOC_TRF_LETTER, VI_DOC_FU_LETTER, VI_DOC_OTHERS, VI_DOC_NAME, VI_INSURANCE, VI_INSURANCE_ID, VI_INS_POLICY_NO " & _
                                        ",VI_INS_EXPIRY_DATE,VI_OTHER_DETAIL,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,VI_LOC_AL_ID,VI_REG_NO_PATTERN,VI_TR_ID,VI_TR_ITEM_SRNO,VI_PUR_DATE" & _
                                        " FROM vehicles_info" & _
                                        " Where REC_ID = '" & VI_RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.VEHICLES_INFO, onlineQuery, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List By Condition
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal Param As Param_Vehicles_GetListByCondition, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT  REC_ID AS ID,VI_ITEM_ID,VI_MAKE , VI_MODEL ,VI_REG_NO,VI_REG_DATE,VI_AMOUNT,VI_OWNERSHIP, CASE WHEN VI_OWNERSHIP='INSTITUTION' THEN '" & Param.openInsID & "' ELSE VI_OWNERSHIP_AB_ID END as  VI_OWNERSHIP_AB_ID ,VI_INSURANCE,VI_INSURANCE_ID,VI_INS_POLICY_NO,VI_INS_EXPIRY_DATE,VI_OTHER_DETAIL,VI_LOC_AL_ID    FROM Vehicles_Info " & _
                    " Where   REC_STATUS IN (0,1,2) AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & " " & Param.OtherCondition
            Return dbService.List(ConnectOneWS.Tables.VEHICLES_INFO, onlineQuery, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary> 
        ''' Common Function to get list
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Object = Nothing) As DataTable
            Dim nextParam As Param_Vehicles_GetList_Common = New Param_Vehicles_GetList_Common()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    nextParam.VI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    nextParam.VI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    nextParam.VI_TR_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    nextParam.VI_Rec_ID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    nextParam.VI_ItemID = Param
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    nextParam = Param
            End Select
            Return GetList_Common_ByRecID(inBasicParam, nextParam)
        End Function

        Public Shared Function GetList_Common_ByRecID(inBasicParam As ConnectOneWS.Basic_Param, ByVal Param As Param_Vehicles_GetList_Common) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = "SELECT VI_TR_ID,VI_TR_ITEM_SRNO,VI_LOC_AL_ID,VI_MAKE, VI_MODEL, VI_REG_NO_PATTERN, VI_REG_NO, VI_REG_DATE, VI_OWNERSHIP, VI_OWNERSHIP_AB_ID, VI_DOC_RC_BOOK, VI_DOC_AFFIDAVIT, VI_DOC_WILL, VI_DOC_TRF_LETTER, VI_DOC_FU_LETTER, VI_DOC_OTHERS, VI_DOC_NAME, VI_INSURANCE_ID, VI_INS_POLICY_NO, VI_INS_EXPIRY_DATE FROM Vehicles_Info WHERE REC_STATUS IN (0,1,2) AND VI_TR_ID ='" & Param.VI_TR_ID & "' ORDER BY VI_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = "SELECT VI_TR_ID,VI_TR_ITEM_SRNO,VI_LOC_AL_ID,VI_MAKE, VI_MODEL, VI_REG_NO_PATTERN, VI_REG_NO, VI_REG_DATE, VI_OWNERSHIP, VI_OWNERSHIP_AB_ID, VI_DOC_RC_BOOK, VI_DOC_AFFIDAVIT, VI_DOC_WILL, VI_DOC_TRF_LETTER, VI_DOC_FU_LETTER, VI_DOC_OTHERS, VI_DOC_NAME, VI_INSURANCE_ID, VI_INS_POLICY_NO, VI_INS_EXPIRY_DATE,REC_ID FROM Vehicles_Info WHERE REC_STATUS IN (0,1,2) AND VI_TR_ID ='" & Param.VI_TR_ID & "' ORDER BY VI_TR_ITEM_SRNO" 'RecID
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT  '' AS 'REF_ITEM',VI_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',VI_MAKE + ',' + VI_MODEL + ', Reg.No.: ' + VI_REG_NO as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0 AS 'SALE_QTY',REC_EDIT_ON  FROM Vehicles_Info  Where   REC_STATUS IN (0,1,2) AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND VI_OWNERSHIP <> 'FREE USE'  and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.VI_Rec_ID Is Nothing Then 'added for multiuser check 
                        Query += " AND REC_ID = '" & Param.VI_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = "SELECT ITEM_NAME AS Vehicle, VI_MAKE as Make, VI_MODEL  AS Model,VI_REG_NO AS 'Reg No', COALESCE(VI_AMOUNT,0) as 'Org Value', COALESCE(VI_AMOUNT,0) as 'Curr Value' ,VI.REC_ID FROM vehicles_info AS VI INNER JOIN item_info as ii on VI.VI_ITEM_ID = ii.REC_ID WHERE VI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND VI_ITEM_ID ='" & Param.VI_ItemID & "' and VI.REC_STATUS IN (0,1,2) AND UPPER(VI_OWNERSHIP) IN ('INCHARGE','INSTITUTION')  and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.VI_Rec_ID Is Nothing Then 'added for multiuser check 
                        Query += " AND VI.REC_ID = '" & Param.VI_Rec_ID & "'"
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = " SELECT  '' AS 'REF_ITEM',VI_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',VI_MAKE + ',' + VI_MODEL + ', Reg.No.: ' + VI_REG_NO as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0 AS 'SALE_QTY',VI_LOC_AL_ID AS REF_LOC_ID,VI_OWNERSHIP AS 'REF_OWNERSHIP',(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = VI_OWNERSHIP_AB_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")   AS 'REF_OWNERSHIP_ID','' AS 'REF_USE',REC_EDIT_ON  FROM Vehicles_Info  Where   REC_STATUS IN (0,1,2) AND VI_CEN_ID=" & Param.Cen_ID.ToString & "  AND VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") AND  VI_OWNERSHIP <> 'FREE USE'   "
                    If Not Param.VI_Rec_ID Is Nothing Then 'added for multiuser check 
                        Query += " AND REC_ID = '" & Param.VI_Rec_ID & "' "
                    End If
            End Select
            Return dbService.List(ConnectOneWS.Tables.VEHICLES_INFO, Query, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Vehicle Info
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Vehicles, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Amount_Text = ""
            If InParam.Amount = Nothing Then Amount_Text = "NULL" Else Amount_Text = InParam.Amount
            If InParam.Ownership_AB_ID = Nothing Then InParam.Ownership_AB_ID = "NULL" Else InParam.Ownership_AB_ID = "'" & InParam.Ownership_AB_ID & "'"
            If InParam.Insurance_ID = Nothing Then
                InParam.Insurance_ID = "NULL"
            ElseIf InParam.Insurance_ID.Trim.Length = 0 Then
                InParam.Insurance_ID = "NULL"
            Else
                InParam.Insurance_ID = "'" & InParam.Insurance_ID & "'"
            End If
            Dim RecID As String = Guid.NewGuid.ToString
            If String.IsNullOrWhiteSpace(InParam.Rec_ID) = False Then
                RecID = InParam.Rec_ID 
            End If
            Dim OnlineQuery As String = "INSERT INTO Vehicles_Info(VI_CEN_ID,VI_COD_YEAR_ID,VI_ITEM_ID,VI_MAKE,VI_MODEL,VI_REG_NO_PATTERN,VI_REG_NO,VI_REG_DATE,VI_AMOUNT,VI_OWNERSHIP,VI_OWNERSHIP_AB_ID," &
                                                  "VI_DOC_RC_BOOK,VI_DOC_AFFIDAVIT,VI_DOC_WILL,VI_DOC_TRF_LETTER,VI_DOC_FU_LETTER,VI_DOC_OTHERS,VI_DOC_NAME,VI_INSURANCE_ID,VI_INS_POLICY_NO,VI_INS_EXPIRY_DATE,VI_LOC_AL_ID,VI_OTHER_DETAIL," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,VI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam.ItemID & "', " &
                                                      "'" & InParam.Make & "', " &
                                                      "'" & InParam.Model & "', " &
                                                      "'" & InParam.Reg_No_Pattern & "' , " &
                                                      "'" & InParam.Reg_No & "', " &
                                                      " " & If(IsDate(InParam.RegDate), "'" & Convert.ToDateTime(InParam.RegDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                      " " & Amount_Text & " , " &
                                                      "'" & InParam.Ownership & "', " &
                                                      " " & InParam.Ownership_AB_ID & " , " &
                                                      "'" & InParam.Doc_RC_Book & "', " &
                                                      "'" & InParam.Doc_Affidavit & "', " &
                                                      "'" & InParam.Doc_Will & "', " &
                                                      "'" & InParam.Doc_TRF_Letter & "', " &
                                                      "'" & InParam.DOC_FU_Letter & "', " &
                                                      "'" & InParam.Doc_Is_Others & "', " &
                                                      "'" & InParam.Doc_Others_Name & "', " &
                                                      " " & InParam.Insurance_ID & " , " &
                                                      "'" & InParam.Ins_Policy_No & "', " &
                                                      " " & If(IsDate(InParam.Ins_Expiry_Date), "'" & Convert.ToDateTime(InParam.Ins_Expiry_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                      "'" & InParam.Location_ID & "', " &
                                                      "'" & InParam.Other_Details & "', " &
                                                      "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.VEHICLES_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_InsertTRIDAndTRSrNo</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertTRIDAndTRSrNo_Vehicles, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RecID As String = Guid.NewGuid.ToString
            If InParam1.Ownership_AB_ID = Nothing Then InParam1.Ownership_AB_ID = "NULL" Else InParam1.Ownership_AB_ID = "'" & InParam1.Ownership_AB_ID & "'"
            If InParam1.Insurance_ID = Nothing Then
                InParam1.Insurance_ID = "NULL"
            ElseIf InParam1.Insurance_ID.Trim.Length = 0 Then
                InParam1.Insurance_ID = "NULL"
            Else
                InParam1.Insurance_ID = "'" & InParam1.Insurance_ID & "'"
            End If
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Vehicles_Info(VI_CEN_ID,VI_COD_YEAR_ID,VI_AMOUNT,VI_ITEM_ID,VI_MAKE,VI_MODEL,VI_REG_NO_PATTERN,VI_REG_NO,VI_REG_DATE,VI_OWNERSHIP,VI_OWNERSHIP_AB_ID," &
                                                  "VI_DOC_RC_BOOK,VI_DOC_AFFIDAVIT,VI_DOC_WILL,VI_DOC_TRF_LETTER,VI_DOC_FU_LETTER,VI_DOC_OTHERS,VI_DOC_NAME,VI_INSURANCE_ID,VI_INS_POLICY_NO,VI_INS_EXPIRY_DATE,VI_LOC_AL_ID,VI_OTHER_DETAIL,VI_TR_ID,VI_TR_ITEM_SRNO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,VI_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.Amount & "'," &
                                                  "'" & If(InParam1.ItemID Is Nothing, "", InParam1.ItemID) & "', " &
                                                  "'" & If(InParam1.Make Is Nothing, "", InParam1.Make) & "', " &
                                                  "'" & If(InParam1.Model Is Nothing, "", InParam1.Model) & "', " &
                                                  "'" & If(InParam1.Reg_No_Pattern Is Nothing, "", InParam1.Reg_No_Pattern) & "' , " &
                                                  "'" & If(InParam1.Reg_No Is Nothing, "", InParam1.Reg_No) & "', " &
                                                  " " & If(IsDate(InParam1.RegDate), "'" & Convert.ToDateTime(InParam1.RegDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & If(InParam1.Ownership Is Nothing, "", InParam1.Ownership) & "', " &
                                                  " " & InParam1.Ownership_AB_ID & " , " &
                                                  "'" & If(InParam1.Doc_RC_Book Is Nothing, "", InParam1.Doc_RC_Book) & "', " &
                                                  "'" & If(InParam1.Doc_Affidavit Is Nothing, "", InParam1.Doc_Affidavit) & "', " &
                                                  "'" & If(InParam1.Doc_Will Is Nothing, "", InParam1.Doc_Will) & "', " &
                                                  "'" & If(InParam1.Doc_TRF_Letter Is Nothing, "", InParam1.Doc_TRF_Letter) & "', " &
                                                  "'" & If(InParam1.DOC_FU_Letter Is Nothing, "", InParam1.DOC_FU_Letter) & "', " &
                                                  "'" & If(InParam1.Doc_Is_Others Is Nothing, "", InParam1.Doc_Is_Others) & "', " &
                                                  "'" & If(InParam1.Doc_Others_Name Is Nothing, "", InParam1.Doc_Others_Name) & "', " &
                                                  " " & InParam1.Insurance_ID & ", " &
                                                  "'" & If(InParam1.Ins_Policy_No Is Nothing, "", InParam1.Ins_Policy_No) & "', " &
                                                  " " & If(IsDate(InParam1.Ins_Expiry_Date), "'" & Convert.ToDateTime(InParam1.Ins_Expiry_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & If(InParam1.Location_ID Is Nothing, "", InParam1.Location_ID) & "', " &
                                                  "'" & If(InParam1.Other_Details Is Nothing, "", InParam1.Other_Details) & "', " &
                                                  "'" & InParam1.TxnID & "', " &
                                                  " " & InParam1.TxnSrNo & ", " &
                                                  "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.VEHICLES_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Vehicles_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Vehicles, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Amount_Text = ""
            If UpParam.Amount = Nothing Then Amount_Text = "NULL" Else Amount_Text = UpParam.Amount
            If UpParam.Ownership_AB_ID = Nothing Then UpParam.Ownership_AB_ID = "NULL" Else UpParam.Ownership_AB_ID = "'" & UpParam.Ownership_AB_ID & "'"
            If UpParam.Insurance_ID = Nothing Then
                UpParam.Insurance_ID = "NULL"
            ElseIf UpParam.Insurance_ID.Trim.Length = 0 Then
                UpParam.Insurance_ID = "NULL"
            Else
                UpParam.Insurance_ID = "'" & UpParam.Insurance_ID & "'"
            End If
            Dim OnlineQuery As String = " UPDATE Vehicles_Info SET " & _
                                        "VI_ITEM_ID         ='" & UpParam.ItemID & "', " & _
                                            "VI_MAKE            ='" & UpParam.Make & "', " & _
                                            "VI_MODEL           ='" & UpParam.Model & "', " & _
                                            "VI_REG_NO_PATTERN  ='" & UpParam.Reg_No_Pattern & "' , " & _
                                            "VI_REG_NO          ='" & UpParam.Reg_No & "', " & _
                                            "VI_REG_DATE        = " & If(IsDate(UpParam.RegDate), "'" & Convert.ToDateTime(UpParam.RegDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            "VI_AMOUNT       =" & Amount_Text & ", " & _
                                            "VI_OWNERSHIP       ='" & UpParam.Ownership & "', " & _
                                            "VI_OWNERSHIP_AB_ID = " & UpParam.Ownership_AB_ID & " , " & _
                                            "VI_DOC_RC_BOOK     ='" & UpParam.Doc_RC_Book & "', " & _
                                            "VI_DOC_AFFIDAVIT   ='" & UpParam.Doc_Affidavit & "', " & _
                                            "VI_DOC_WILL        ='" & UpParam.Doc_Will & "', " & _
                                            "VI_DOC_TRF_LETTER  ='" & UpParam.Doc_TRF_Letter & "', " & _
                                            "VI_DOC_FU_LETTER   ='" & UpParam.DOC_FU_Letter & "', " & _
                                            "VI_DOC_OTHERS      ='" & UpParam.Doc_Is_Others & "', " & _
                                            "VI_DOC_NAME        ='" & UpParam.Doc_Others_Name & "', " & _
                                            "VI_INSURANCE_ID    = " & UpParam.Insurance_ID & " , " & _
                                            "VI_INS_POLICY_NO   ='" & UpParam.Ins_Policy_No & "', " & _
                                            "VI_INS_EXPIRY_DATE = " & If(IsDate(UpParam.Ins_Expiry_Date), "'" & Convert.ToDateTime(UpParam.Ins_Expiry_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            "VI_LOC_AL_ID      ='" & UpParam.Location_ID & "', " & _
                                            "VI_OTHER_DETAIL    ='" & UpParam.Other_Details & "', " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.VEHICLES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateInsuranceDetail(ByVal UpParam As Parameter_Update_Vehicles, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Insurance_ID = Nothing Then
                UpParam.Insurance_ID = "NULL"
            ElseIf UpParam.Insurance_ID.Trim.Length = 0 Then
                UpParam.Insurance_ID = "NULL"
            Else
                UpParam.Insurance_ID = "'" & UpParam.Insurance_ID & "'"
            End If
            Dim OnlineQuery As String = " UPDATE Vehicles_Info SET " & _
                                            "VI_INSURANCE_ID    = " & UpParam.Insurance_ID & " , " & _
                                            "VI_INS_POLICY_NO   ='" & UpParam.Ins_Policy_No & "', " & _
                                            "VI_INS_EXPIRY_DATE = " & If(IsDate(UpParam.Ins_Expiry_Date), "'" & Convert.ToDateTime(UpParam.Ins_Expiry_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.VEHICLES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Vehicles_UpdateAssetLocationIfNotPresent</remarks>
        Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Vehicles_Info SET VI_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(VI_LOC_AL_ID,'')=''"
            dbService.Update(ConnectOneWS.Tables.VEHICLES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
#End Region
End Namespace
