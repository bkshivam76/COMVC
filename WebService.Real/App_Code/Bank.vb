Imports System.Data

Namespace Real

    <Serializable>
    Public Class BankAccounts
#Region "Get/Input/Update Parameter Classes"
        <Serializable>
        Public Class Param_Bank_GetTransactionMaxDate
            Public xAccID As String
            Public ClosingDate As Date
        End Class
        <Serializable>
        Public Class Param_Bank_Close
            Public CloseDate As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_BankandBalance_BankAccounts
            Public BranchID As String
            Public TanNo As String
            Public AccountType As String
            Public AccountNew As String
            Public OpeningDate As String
            Public AccountNo As String
            Public CustNo As String
            Public FeraAccount As String
            Public FCRA_Utility_Account As Boolean
            Public Corpus_Account As Boolean
            Public Reloadable_Card As Boolean
            Public TelNo As String
            Public EmailID As String
            Public Sign1ABID As String
            Public Sign2ABID As String
            Public Sign3ABID As String
            Public OtherDetails As String
            Public Status_Action As String
            Public Amount As Double
            Public RecID As String
            Public YearID As Integer 'added to bring additional year Id from client for Opening Balance table
        End Class
        <Serializable>
        Public Class Parameter_Update_BankandBalance_BankAccounts
            Public BranchID As String
            Public TanNo As String
            Public AccountType As String
            Public AccountNew As String
            Public OpeningDate As String
            Public AccountNo As String
            Public CustNo As String
            Public FeraAccount As String
            Public FCRA_Utility_Account As Boolean
            Public Corpus_Account As Boolean
            Public Reloadable_Card As Boolean
            Public TelNo As String
            Public EmailID As String
            Public Sign1ABID As String
            Public Sign2ABID As String
            Public Sign3ABID As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Amount As Double
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Reopen_BankAccounts
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Bank_GetList_Common
            Public Type As String
            Public ForeignOnly As Boolean
            Public Bank_Account_Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Bank_GetValue_Common
            Public CR_LED_ID As String
            Public DR_LED_ID As String
        End Class
#End Region

        ''' <summary>
        ''' Returns bank list
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param, Optional Bank_RecID As String = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim yrStDate As String = "20" + inBasicParam.openYearID.ToString.Substring(0, 2) + "-03-31"
            'Dim onlineQuery As String = " SELECT BA_BRANCH_ID,BA_TAN_NO,BA_TEL_NOS,BA_EMAIL_ID,BA_ACCOUNT_TYPE,CASE WHEN BA_COD_YEAR_ID <>" & inBasicParam.openYearID.ToString & " THEN '' ELSE BA_ACCOUNT_NEW END AS BA_ACCOUNT_NEW ,BA_OPEN_DATE,BA_CLOSE_DATE,BA_ACCOUNT_NO,BA_CUST_NO,BA_OTHER_DETAIL, REC_ID AS ID , BA_COD_YEAR_ID as YearID,COALESCE(ATTACHMENT.ATTACHMENT_COUNT,0) AS Attachments ," & Common.Remarks_Detail("BANK_ACCOUNT_INFO", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("BANK_ACCOUNT_INFO") & "" &
            '                            " FROM BANK_ACCOUNT_INFO " &
            '                            " OUTER APPLY ( SELECT * FROM fn_get_Attachment_Count(BANK_ACCOUNT_INFO.BA_CEN_ID,BANK_ACCOUNT_INFO.BA_COD_YEAR_ID,'Bank') AS ABC WHERE BANK_ACCOUNT_INFO.REC_ID = COALESCE(ABC.REF_ID,BANK_ACCOUNT_INFO.REC_ID) ) AS ATTACHMENT " &
            '                            " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & "  and BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID= " & inBasicParam.openYearID.ToString & ") " &
            '                            " AND COALESCE(BA_CLOSE_DATE,'" & yrStDate & "') >= '" & yrStDate & "'"
            'If Not Bank_RecID Is Nothing Then 'added for multiuser check
            '    onlineQuery += " AND REC_ID = '" & Bank_RecID & "' "
            'End If
            'Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, onlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@REC_ID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Bank_RecID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 4, 36, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, "[sp_get_Bank_Profile]", ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Returns bank Record
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetRecord</remarks>
        Public Shared Function GetRecord(inBasicParam As ConnectOneWS.Basic_Param, Bank_RecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT BA_CEN_ID,BA_ACCOUNT_TYPE,BA_BRANCH_ID,BA_TAN_NO,BA_TEL_NOS,BA_EMAIL_ID,BA_ACCOUNT_NO,BA_CUST_NO,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = BA_SIGN_AB_ID_1) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") AS BA_SIGN_AB_ID_1,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = BA_SIGN_AB_ID_2) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") AS BA_SIGN_AB_ID_2,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = BA_SIGN_AB_ID_3) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") AS BA_SIGN_AB_ID_3,BA_OTHER_DETAIL,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,BA.REC_ID,BA_ACCOUNT_NEW,BA_OPEN_DATE,BA_CLOSE_DATE,BA_FERA_ACC,BA_FCRA_UTIL,BA_COD_YEAR_ID,CASE WHEN REF.REF_ID IS NULL THEN 0 ELSE 1 END AS CORPUS,CASE WHEN RELOAD_REF.REF_ID IS NULL THEN 0 ELSE 1 END AS RELOAD  FROM bank_account_info  BA LEFT JOIN Reference_Opening_Info AS REF ON BA.BA_ORG_REC_ID = REF.ORG_REF_ID AND REF.SVR_NAME = 'CORPUS' AND REF.REF_NAME='bank_account_info' LEFT JOIN  Reference_Opening_Info AS RELOAD_REF ON BA.BA_ORG_REC_ID = RELOAD_REF.ORG_REF_ID AND RELOAD_REF.SVR_NAME = 'RELOADABLE_CARD' AND RELOAD_REF.REF_NAME='bank_account_info'" &
                                        " Where BA.REC_ID = '" & Bank_RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, onlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function


        ''' <summary>
        ''' Returns bank list
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Param_Bank_GetList_Common = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_BankToBank
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_CashBank
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_CollectionBox
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Donation
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO as BANK_ACC_NO,BA.REC_ID AS BA_ID, BA.REC_EDIT_ON , B.BI_BANK_NAME AS BANK_NAME,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as BANK_BRANCH, A.REC_ID AS BB_BRANCH_ID FROM BANK_ACCOUNT_INFO BA INNER JOIN bank_branch_info AS A ON BA.BA_BRANCH_ID = A.REC_ID INNER JOIN bank_info as B on a.BI_BANK_ID = B.REC_ID " &
                                " Where   BA.REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Param.ForeignOnly Then Query += " AND BA_FERA_ACC='YES' "
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And BA.REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_FD
                    If Param.Type.ToUpper() = "SAVING" Then
                        Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='" & Common_Lib.Common.Bank_Trans_Type.SAVING.ToString & "'  and BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    Else
                        Query = " SELECT BA_BRANCH_ID,BA_CUST_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                               " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='" & Common_Lib.Common.Bank_Trans_Type.FD.ToString & "' AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))"
                    End If
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Internal_Transfer
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='" & Common_Lib.Common.Bank_Trans_Type.SAVING.ToString & "'  and BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Param.ForeignOnly Then Query += " AND BA_FERA_ACC='YES' "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership_Renewal
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  "
                    If Param.ForeignOnly Then Query += " AND BA_FERA_ACC='YES' "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID,REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " AND REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID,REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Param.ForeignOnly Then Query += " AND BA_FERA_ACC='YES' "
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Report
                    Query = " SELECT REC_ID AS ID,'' AS BI_BANK_ID,'' AS NAME,BA_BRANCH_ID,'' as BRANCH,'' AS BB_IFSC_CODE,'' AS BB_MICR_CODE,'' AS BI_BANK_PAN_NO,BA_TAN_NO,BA_TEL_NOS,BA_EMAIL_ID,BA_ACCOUNT_TYPE,BA_ACCOUNT_NO,BA_CUST_NO,0.00 as OP_AMOUNT, BA_OTHER_DETAIL  " &
                                " FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID= " & inBasicParam.openYearID.ToString & ") ; "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID, REC_EDIT_ON FROM BANK_ACCOUNT_INFO " &
                                    " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING'  AND BA_CLOSE_DATE IS NULL  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                    If Not Param.Bank_Account_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & Param.Bank_Account_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = " SELECT '' AS BI_SHORT_NAME, BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID FROM BANK_ACCOUNT_INFO " &
                                 " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='SAVING' AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") ORDER BY REC_ID  ; "
                Case ConnectOneWS.ClientScreen.Report_Transaction_Summary
                    Query = " SELECT BA_BRANCH_ID,BA_ACCOUNT_NO,REC_ID AS ID FROM BANK_ACCOUNT_INFO " &
                                " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE='" & Common_Lib.Common.Bank_Trans_Type.SAVING.ToString & "'  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                Case ConnectOneWS.ClientScreen.Accounts_Notebook
                    Query = " SELECT REC_ID AS ID FROM BANK_ACCOUNT_INFO Where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND BA_ACCOUNT_TYPE = '" & Common_Lib.Common.Bank_Trans_Type.SAVING.ToString & "'  AND BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "; "

                    'Each calling function Query shall be added here, calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
            Return Nothing
        End Function

        ''' <summary>
        ''' Common function to  Get Single Value
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetValue_Common</remarks>
        Public Shared Function GetValue_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Param_Bank_GetValue_Common = Nothing) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = " SELECT MAX(ba_account_no)  as REC_FOUND FROM BANK_ACCOUNT_INFO " &
                                    " Where    REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & "   AND  REC_ID in ('" & Param.CR_LED_ID & "','" & Param.DR_LED_ID & "') AND BA_CLOSE_DATE IS NOT NULL "
                    'Query will come here when we come across a function that calls this function. e.g. vouchers
            End Select
            Return dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetClosedBank(ByVal Tr_M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT MAX(ba_account_no)  as REC_FOUND FROM BANK_ACCOUNT_INFO " &
                                    " Where    REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & "   AND ( REC_ID in ( SELECT DISTINCT COALESCE(TR_SUB_CR_LED_ID,'') FROM TRANSACTION_INFO WHERE TR_M_ID = '" & Tr_M_ID & "') OR  REC_ID in ( SELECT DISTINCT COALESCE(TR_SUB_DR_LED_ID,'') FROM TRANSACTION_INFO WHERE TR_M_ID = '" & Tr_M_ID & "')) AND BA_CLOSE_DATE IS NOT NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets FD Account List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetFDAccountList</remarks>
        Public Shared Function GetFDAccountList(inBasicParam As ConnectOneWS.Basic_Param, Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT BA_BRANCH_ID,BA_CUST_NO,REC_ID AS BA_BANK_ID," & Common.Rec_Detail("BANK_ACCOUNT_INFO") & "" &
                                 " FROM BANK_ACCOUNT_INFO " &
                                 " Where   REC_STATUS IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND BA_ACCOUNT_TYPE = '" & Common_Lib.Common.Bank_Trans_Type.FD.ToString & "'  and BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            If Not Bank_Account_Rec_ID Is Nothing Then 'added for multi user check 
                onlineQuery += " And REC_ID = '" & Bank_Account_Rec_ID & "'"
            End If
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, onlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transactions Count By AccountID
        ''' </summary>
        ''' <param name="AccID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetTxnsCountByAccID</remarks>
        Public Shared Function GetTxnsCountByAccID(ByVal AccID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT count(Rec_id) FROM TRANSACTION_INFO WHERE (TR_SUB_CR_LED_ID ='" & AccID & "' OR TR_SUB_DR_LED_ID ='" & AccID & "') AND REC_STATUS IN (0,1,2)"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Last date of Transaction for the mentioned Bank Account
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetTransactionMaxDate</remarks>
        Public Shared Function GetTransactionMaxDate(ByVal Param As Param_Bank_GetTransactionMaxDate, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT max(tr_date) as tr_dt" &
                             " FROM Transaction_Info " &
                             " Where    REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   AND ( TR_SUB_CR_LED_ID='" & Param.xAccID & "' or TR_SUB_DR_LED_ID='" & Param.xAccID & "')  AND CAST(TR_DATE AS DATE) > '" & Format(Param.ClosingDate, Common.Server_Date_Format_Short) & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get FD Count Before Bank account deletion 
        ''' </summary>
        ''' <param name="BankAccId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetFDCount</remarks>
        Public Shared Function GetFDCount(ByVal BankAccId As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(FD_BA_ID) FROM FD_INFO  WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " AND FD_BA_ID  = '" & BankAccId & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get FD sum after Bank account addition/updation
        ''' </summary>
        ''' <param name="BankAccId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetFDSum</remarks>
        Public Shared Function GetFDSum(ByVal BankAccId As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COALESCE(SUM(FD_AMT),0) FROM FD_INFO as FI LEFT OUTER JOIN " &
                            " (SELECT distinct TR_TRF_CROSS_REF_ID AS FDID, TR_DATE, TR_M_ID FROM transaction_info WHERE  REC_STATUS  IN (0,1,2) and TR_ITEM_ID ='65730a27-e365-4195-853e-2f59225fe8f4' " &
                            " AND TR_TRF_CROSS_REF_ID IN (SELECT REC_ID FROM fd_info WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID & " ) ) AS FD_CLOSE  ON FD_CLOSE.FDID = FI.REC_ID " &
                            " WHERE REC_STATUS IN (0,1,2) AND FD_CEN_ID=" & inBasicParam.openCenID.ToString & " AND FD_BA_ID  = '" & BankAccId & "'  AND (FD_TR_ID IS NULL OR FD_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & ") AND FD_CLOSE.TR_DATE IS NULL  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID)) AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  "
            Return dbService.GetScalar(ConnectOneWS.Tables.FD_INFO, OnlineQuery, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transaction Count Before Bank account deletion 
        ''' </summary>
        ''' <param name="BankAccId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetTransCount</remarks>
        Public Shared Function GetTransCount(ByVal BankAccId As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) FROM TRANSACTION_INFO  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( TR_SUB_CR_LED_ID  = '" & BankAccId & "' or  TR_SUB_DR_LED_ID  = '" & BankAccId & "' or  TR_REF_BANK_ID  = '" & BankAccId & "' ) "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Dr. and Cr.Transaction Sum
        ''' </summary>
        ''' <param name="AccRecId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetTransSums</remarks>
        Public Shared Function GetTransSums(ByVal AccRecId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT SUM(CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS R_BANK," &
                             "        SUM(CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS P_BANK" &
                             " FROM Transaction_Info " &
                             " Where  TR_CR_LED_ID <> TR_DR_LED_ID AND  REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "   AND TR_SUB_CR_LED_ID='" & AccRecId & "' ; "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Payment Transaction Sum
        ''' </summary>
        ''' <param name="AccRecId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetPaymentTransSums</remarks>
        Public Shared Function GetPaymentTransSums(ByVal AccRecId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT SUM( CASE WHEN TR_CR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS P_BANK " &
                                " FROM Transaction_Info " &
                             " Where  TR_CR_LED_ID = TR_DR_LED_ID AND   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "    AND TR_SUB_CR_LED_ID='" & AccRecId & "' ; "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function


        ''' <summary>
        ''' Get Receipt Transaction Sum
        ''' </summary>
        ''' <param name="AccRecId"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetReceiptTransSums</remarks>
        Public Shared Function GetReceiptTransSums(ByVal AccRecId As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim OnlineQuery As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            OnlineQuery = " SELECT SUM(CASE WHEN TR_DR_LED_ID='00079' THEN TR_AMOUNT ELSE NULL END) AS R_BANK " &
                             " FROM Transaction_Info " &
                             " Where  TR_CR_LED_ID = TR_DR_LED_ID AND   REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & "    AND TR_SUB_DR_LED_ID='" & AccRecId & "' ; "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Count by AccountNo
        ''' </summary>
        ''' <param name="AccountNo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetCountByAccountNo</remarks>
        Public Shared Function GetCountByAccountNo(ByVal AccountNo As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM BANK_ACCOUNT_INFO WHERE  REC_STATUS IN (0,1,2)  AND  UPPER(BA_ACCOUNT_NO)='" & Trim(UCase(AccountNo)) & "' AND  BA_CEN_ID =" & inBasicParam.openCenID.ToString & " and BA_ACCOUNT_TYPE = 'SAVING'  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            Return dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        '''  Get Count by Customer No
        ''' </summary>
        ''' <param name="CustomerNo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetCountByCustomerNo</remarks>
        Public Shared Function GetCountByCustomerNo(ByVal CustomerNo As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM BANK_ACCOUNT_INFO WHERE  REC_STATUS IN (0,1,2)  AND  UPPER(BA_CUST_NO)='" & Trim(UCase(CustomerNo)) & "' AND  BA_CEN_ID =" & inBasicParam.openCenID.ToString & " and BA_ACCOUNT_TYPE = 'FD'  AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Return dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get FD bank Count by Customer No
        ''' </summary>
        ''' <param name="CustomerNo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_GetFDBankByCustomerNo</remarks>
        Public Shared Function GetFDBankByCustomerNo(ByVal CustomerNo As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT BA_BRANCH_ID FROM BANK_ACCOUNT_INFO WHERE  REC_STATUS IN (0,1,2)  AND  UPPER(BA_CUST_NO)='" & Trim(UCase(CustomerNo)) & "' AND  BA_CEN_ID =" & inBasicParam.openCenID.ToString & " and BA_ACCOUNT_TYPE = 'FD'"
            Return dbService.List(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Bank And Balance
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_Insert_Bank_and_Balance</remarks>
        Public Shared Function Insert_Bank_and_Balance(ByVal InParam As Parameter_Insert_BankandBalance_BankAccounts, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim result As Boolean = False
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Sign1ABID Is Nothing Then InParam.Sign1ABID = " NULL " Else InParam.Sign1ABID = "'" & InParam.Sign1ABID & "'"
            If InParam.Sign2ABID Is Nothing Then InParam.Sign2ABID = " NULL " Else InParam.Sign2ABID = "'" & InParam.Sign2ABID & "'"
            If InParam.Sign3ABID Is Nothing Then InParam.Sign3ABID = " NULL " Else InParam.Sign3ABID = "'" & InParam.Sign3ABID & "'"
            Dim OnlineQuery As String = "INSERT INTO BANK_ACCOUNT_INFO(BA_CEN_ID,BA_BRANCH_ID,BA_TAN_NO,BA_ACCOUNT_TYPE,BA_ACCOUNT_NEW,BA_OPEN_DATE,BA_ACCOUNT_NO,BA_CUST_NO,BA_FERA_ACC,BA_FCRA_UTIL,BA_TEL_NOS,BA_EMAIL_ID,BA_SIGN_AB_ID_1,BA_SIGN_AB_ID_2,BA_SIGN_AB_ID_3,BA_OTHER_DETAIL, BA_COD_YEAR_ID, " &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,BA_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "'" & InParam.BranchID & "', " &
                                                  "'" & InParam.TanNo & "', " &
                                                  "'" & InParam.AccountType & "', " &
                                                  " " & InParam.AccountNew & " , " &
                                                  " " & If(IsDate(InParam.OpeningDate), "'" & Convert.ToDateTime(InParam.OpeningDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InParam.AccountNo & "', " &
                                                  "'" & InParam.CustNo & "', " &
                                                  "'" & InParam.FeraAccount & "', " &
                                                  "" & IIf(InParam.FCRA_Utility_Account, 1, 0) & ", " &
                                                  "'" & InParam.TelNo & "', " &
                                                  "'" & InParam.EmailID & "', " &
                                                  " " & InParam.Sign1ABID & " , " &
                                                  " " & InParam.Sign2ABID & " , " &
                                                  " " & InParam.Sign3ABID & " , " &
                                                  "'" & InParam.OtherDetails & "', " &
                                                  "" & InParam.YearID.ToString & ", " &
                                              "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, inBasicParam, InParam.RecID)
            result = True
            If result Then
                Dim Param As DataFunctions.Param_AddOpeningBalance = New DataFunctions.Param_AddOpeningBalance()
                Param.Amount = InParam.Amount
                Param.RecID = InParam.RecID
                Param.openYearID = InParam.YearID
                result = DataFunctions.AddOpeningBalance(Param, inBasicParam)
            End If
            'Corpus related information
            If InParam.Corpus_Account = True Then
                Dim CorpusInsertQuery As String = "INSERT INTO [dbo].[Reference_Opening_Info] " &
                                                       " ([REF_NAME],[SVR_NAME],[REF_ID],[OPENING_VALUE],[OPENING_QTY],[YEAR_ID],[CEN_ID],[ORG_REF_ID]) " &
                                                " VALUES ('bank_account_info', 'CORPUS', '" & InParam.RecID & "', " & InParam.Amount & ", 1, " & InParam.YearID & ", " & inBasicParam.openCenID &
                                                ", '" & InParam.RecID & "')"

                dbService.Insert(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, CorpusInsertQuery, inBasicParam, InParam.RecID)
            End If
            'Reloadable Card related information
            If InParam.Reloadable_Card = True Then
                Dim ReloadableCardInsertQuery As String = "INSERT INTO [dbo].[Reference_Opening_Info] " &
                                                       " ([REF_NAME],[SVR_NAME],[REF_ID],[OPENING_VALUE],[OPENING_QTY],[YEAR_ID],[CEN_ID],[ORG_REF_ID]) " &
                                                " VALUES ('bank_account_info', 'RELOADABLE_CARD', '" & InParam.RecID & "', " & InParam.Amount & ", 1, " & InParam.YearID & ", " & inBasicParam.openCenID &
                                                ", '" & InParam.RecID & "')"

                dbService.Insert(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, ReloadableCardInsertQuery, inBasicParam, InParam.RecID)
            End If

            Return result
        End Function

        ''' <summary>
        ''' Update Bank And Balance
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_Update_Bank_and_Balance</remarks>
        Public Shared Function Update_Bank_and_Balance(ByVal UpParam As Parameter_Update_BankandBalance_BankAccounts, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim result As Boolean = False
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Sign1ABID Is Nothing Then UpParam.Sign1ABID = " NULL " Else UpParam.Sign1ABID = "'" & UpParam.Sign1ABID & "'"
            If UpParam.Sign2ABID Is Nothing Then UpParam.Sign2ABID = " NULL " Else UpParam.Sign2ABID = "'" & UpParam.Sign2ABID & "'"
            If UpParam.Sign3ABID Is Nothing Then UpParam.Sign3ABID = " NULL " Else UpParam.Sign3ABID = "'" & UpParam.Sign3ABID & "'"
            Dim OnlineQuery As String = " UPDATE BANK_ACCOUNT_INFO SET " &
                                         "BA_BRANCH_ID       ='" & UpParam.BranchID & "', " &
                                         "BA_TAN_NO          ='" & UpParam.TanNo & "', " &
                                         "BA_ACCOUNT_TYPE    ='" & UpParam.AccountType & "', " &
                                         "BA_ACCOUNT_NEW     = " & UpParam.AccountNew & " , " &
                                         "BA_OPEN_DATE       = " & If(IsDate(UpParam.OpeningDate), "'" & Convert.ToDateTime(UpParam.OpeningDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                         "BA_ACCOUNT_NO      ='" & UpParam.AccountNo & "', " &
                                         "BA_CUST_NO         ='" & UpParam.CustNo & "', " &
                                         "BA_FERA_ACC        ='" & UpParam.FeraAccount & "', " &
                                         "BA_FCRA_UTIL       =" & IIf(UpParam.FCRA_Utility_Account, 1, 0) & ", " &
                                         "BA_TEL_NOS         ='" & UpParam.TelNo & "', " &
                                         "BA_EMAIL_ID        ='" & UpParam.EmailID & "', " &
                                         "BA_SIGN_AB_ID_1    = " & UpParam.Sign1ABID & " , " &
                                         "BA_SIGN_AB_ID_2    = " & UpParam.Sign2ABID & " , " &
                                         "BA_SIGN_AB_ID_3    = " & UpParam.Sign3ABID & " , " &
                                         "BA_OTHER_DETAIL    ='" & UpParam.OtherDetails & "', " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, inBasicParam)
            result = True
            If result Then
                Dim Param As DataFunctions.Param_UpdateOpeningBalance = New DataFunctions.Param_UpdateOpeningBalance()
                Param.Amount = UpParam.Amount
                Param.RecID = UpParam.Rec_ID
                'Param.Status_Action = UpParam.Status_Action
                result = DataFunctions.UpdateOpeningBalance(Param, inBasicParam)
            End If


            'Corpus related information
            'delete before insert.
            Dim deleteCorpusQuery = "delete from Reference_Opening_Info where REF_ID = " & " and [REF_NAME] = 'bank_account_info' and [SVR_NAME] = 'CORPUS'"
            dbService.DeleteByCondition(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, "REF_ID ='" & UpParam.Rec_ID & "' and [REF_NAME] = 'bank_account_info' and [SVR_NAME] = 'CORPUS'", inBasicParam)

            'inserting
            If UpParam.Corpus_Account = True Then
                If UpParam.Amount = Nothing Then
                    UpParam.Amount = 0
                End If

                Dim CorpusInsertQuery As String = "INSERT INTO [dbo].[Reference_Opening_Info] ([REF_NAME],[SVR_NAME],[REF_ID],[OPENING_VALUE],[OPENING_QTY],[YEAR_ID],[CEN_ID],[ORG_REF_ID]) " &
                                                " VALUES ('bank_account_info', 'CORPUS', '" & UpParam.Rec_ID & "', " & UpParam.Amount & ", 1, " & inBasicParam.openYearID & ", " & inBasicParam.openCenID &
                                                ",(Select BA_ORG_REC_ID from bank_account_info where REC_ID = '" & UpParam.Rec_ID & "'))"

                dbService.Insert(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, CorpusInsertQuery, inBasicParam, UpParam.Rec_ID)
            End If

            'Reloadable Card related information
            'delete before insert.
            dbService.DeleteByCondition(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, "REF_ID ='" & UpParam.Rec_ID & "' and [REF_NAME] = 'bank_account_info' and [SVR_NAME] = 'RELOADABLE_CARD'", inBasicParam)

            'inserting
            If UpParam.Reloadable_Card = True Then
                If UpParam.Amount = Nothing Then
                    UpParam.Amount = 0
                End If

                Dim ReloadableInsertQuery As String = "INSERT INTO [dbo].[Reference_Opening_Info] ([REF_NAME],[SVR_NAME],[REF_ID],[OPENING_VALUE],[OPENING_QTY],[YEAR_ID],[CEN_ID],[ORG_REF_ID]) " &
                                                " VALUES ('bank_account_info', 'RELOADABLE_CARD', '" & UpParam.Rec_ID & "', " & UpParam.Amount & ", 1, " & inBasicParam.openYearID & ", " & inBasicParam.openCenID &
                                                ",(Select BA_ORG_REC_ID from bank_account_info where REC_ID = '" & UpParam.Rec_ID & "'))"

                dbService.Insert(ConnectOneWS.Tables.REFERENCE_OPENING_INFO, ReloadableInsertQuery, inBasicParam, UpParam.Rec_ID)
            End If


            Return result
        End Function

        ''' <summary>
        ''' Closes Bank Account
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_Close</remarks>
        Public Shared Function Close(ByVal Param As Param_Bank_Close, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE BANK_ACCOUNT_INFO SET " &
                                         "BA_CLOSE_DATE         = " & If(IsDate(Param.CloseDate), "'" & Convert.ToDateTime(Param.CloseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                         " " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," &
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                        "  WHERE REC_ID    ='" & Param.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Reopens Bank Account 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Bank_Reopen</remarks>
        Public Shared Function Reopen(ByVal Param As Param_Reopen_BankAccounts, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE BANK_ACCOUNT_INFO SET " &
                                         "BA_CLOSE_DATE         = NULL, " &
                                         " " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," &
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                        "  WHERE REC_ID    ='" & Param.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function


    End Class
End Namespace