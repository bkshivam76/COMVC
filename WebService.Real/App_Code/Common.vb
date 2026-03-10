Imports System.Transactions
Namespace Real
    <Serializable>
    Public Class Common
#Region "--Variables--"
        'Uncomment the following line if using designed components 
        'InitializeComponent(); 
        Public Sub New()
        End Sub
        Public Shared Function ConnectionString() As String
            If UseSQL() Then
                Return ConfigurationManager.ConnectionStrings("SQLConnectOneConnectionString").ToString()
            End If
            Return ConfigurationManager.ConnectionStrings("dbConnectionString").ToString()
        End Function

        Public Shared DateFormatLong As String = "yyyy-MM-dd HH:mm:ss"
        Public Shared Time_Format_AM_PM As String = "hh:mm tt"
        Public Shared Date_Format_Short As String = "MM-dd-yyyy"
        Public Shared Date_Format_Long As String = "MM-dd-yyyy HH:mm:ss"
        Public Shared Server_Date_Format_Long As String = "yyyy-MM-dd HH:mm:ss"
        Public Shared Server_Date_Format_Short As String = "yyyy-MM-dd"
        Public Shared Date_Format_DD_MMM_YYYY As String = "dd-MMM-yyyy"

        Public Shared Function UseSQL() As Boolean
            Return True
        End Function

        ''' <summary>
        ''' Copied from common.cs from D0000
        ''' </summary>
        ''' <param name="DataRef"></param>
        ''' <param name="conMode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Rec_Detail(ByVal DataRef As String) As String
            Dim StatusStr As String = ""
            Dim SqlStr As String = ""

            StatusStr = "CASE CASE WHEN " & DataRef & ".REC_STATUS IS NULL THEN 99 ELSE " & DataRef & ".REC_STATUS END " &
                                           "WHEN 99 THEN 'Incorrect'  " &
                                           "WHEN -1 THEN 'Deleted' " &
                                           "WHEN 0 THEN 'Incomplete' " &
                                           "WHEN 1 THEN 'Completed'  " &
                                           "WHEN 2 THEN 'Locked' End"
            SqlStr = DataRef & ".REC_ADD_BY as 'Add By'," & DataRef & ".REC_ADD_ON as 'Add Date'," &
                                   DataRef & ".REC_EDIT_BY as 'Edit By'," & DataRef & ".REC_EDIT_ON as 'Edit Date'," &
                                  StatusStr & " as 'Action Status'," & DataRef & ".REC_STATUS_BY as 'Action By'," & DataRef & ".REC_STATUS_ON as 'Action Date'"

            Return SqlStr
        End Function

        Public Shared Function Rec_Detail_OrgField(ByVal DataRef As String) As String
            Dim StatusStr As String = ""
            Dim SqlStr As String = ""

            StatusStr = "CASE CASE WHEN " & DataRef & ".REC_STATUS IS NULL THEN 99 ELSE " & DataRef & ".REC_STATUS END " &
                            "WHEN 99 THEN 'Incorrect'  " &
                            "WHEN -1 THEN 'Deleted' " &
                            "WHEN 0 THEN 'Incomplete' " &
                            "WHEN 1 THEN 'Completed'  " &
                            "WHEN 2 THEN 'Locked' End"
            SqlStr = DataRef & ".REC_ADD_BY ," & DataRef & ".REC_ADD_ON ," &
                     DataRef & ".REC_EDIT_BY ," & DataRef & ".REC_EDIT_ON ," &
                     StatusStr & " as ACTION_STATUS," & DataRef & ".REC_STATUS_BY," & DataRef & ".REC_STATUS_ON"

            Return SqlStr
        End Function

        ''' <summary>
        ''' PROVIDES REMARKCOUNT AND STATUS FOR ACTION ITEMS REGISTERED TO BE COMPLETED IN CURRENT AUDIT ONLY 'OFFLINE FUNCTION
        ''' </summary>
        ''' <param name="DataRef"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Remarks_Detail(ByVal DataRef As String, ByVal CurrDateTime As DateTime, ByVal _Server_Date_Format_Short As String) As String
            Dim SqlStr As String = ""

            SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                        "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                        "(SELECT TOP 1 COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as OpenActions, " &
                        "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "


            Return SqlStr
        End Function


        ''' <summary>
        ''' PROVIDES REMARKCOUNT AND STATUS FOR ACTION ITEMS REGISTERED TO BE COMPLETED IN CURRENT AUDIT ONLY , for Transactions 'OFFLINE FUNCTION
        ''' </summary>
        ''' <param name="DataRef"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Remarks_Detail_Txn(ByVal DataRef As String, ByVal CurrDateTime As DateTime, ByVal _Server_Date_Format_Short As String) As String
            Dim SqlStr As String = ""

            SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID) AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                        "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                        "(SELECT TOP 1 COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as OpenActions, " &
                        "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "

            Return SqlStr
        End Function

        Public Shared Function Remarks_Detail_Txn_Grouped(ByVal DataRef As String, ByVal CurrDateTime As DateTime, ByVal _Server_Date_Format_Short As String) As String
            Dim SqlStr As String = ""
            SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID)) AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                        "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                        "(SELECT TOP 1 COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as OpenActions, " &
                        "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
            Return SqlStr
        End Function

        Public Const DateTimePlaceHolder As String = "%datetime%"

        Public Class ClientRoles
            Public Const Center_Admin As [String] = "CLIENT ROLE"
            Public Const Auditor As [String] = "AUDITOR"
            Public Const SuperUser As [String] = "SUPERUSER"
        End Class

        Public Class CenterRestrictions
            'PLEASE MAKE SURE VALUES ARE ADDED IN UPPER CASE
            Public Const ReadAllWriteNone As [String] = "READ_ALL_WRITE_NONE"
            Public Const ReadNoneWriteNone As [String] = "READ_NONE_WRITE_NONE"
            Public Const ReadAllWriteNonAccounts As [String] = "READ_ALL_WRITE_NON_ACCOUNTS"
            Public Const ReadAllWriteBlockedForSomePeriod As [String] = "READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION"
            Public Const ReadAllWriteBlockedAccountsSubmitted As [String] = "READ_ALL_WRITE_BLOCKED_ACCOUNTS_SUBMITTED"
            Public Const LoginBlocked As [String] = "LOGIN_BLOCKED"
            Public Const ProfileBlocked As [String] = "PROFILE_BLOCKED"
            Public Const MagazineDateBlocked As [String] = "MAG_DATE_BLOCKED"
            Public Const ReadAllWriteBlockedForAllUsers As [String] = "READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION_ALL_USERS"
        End Class

        Public Enum LoginExceptions
            ''' <summary>
            ''' Exception for Restrictions on centers login in version 2.0 or above
            ''' </summary>
            Sync_Before_2_0
        End Enum

        'Public Shared Function GetConnectionScope() As DbConnectionScope
        '    Return New DbConnectionScope("connectionKey", New System.Data.SqlClient.SqlConnection(Common.ConnectionString))
        'End Function

        'Public Shared Function GetTransactionScope() As TransactionScope
        '    Return New TransactionScope()
        'End Function
#End Region

#Region "--Error Messages--"
        Public Shared Function TableError(ByVal tablename As ConnectOneWS.Tables) As String
            Return "|Query does not contain specified table:" & tablename.ToString() & "|"
        End Function
        Public Shared Function InsertError() As String
            Return "|Query does not contain insertion query|"
        End Function
        Public Shared Function UpdateError() As String
            Return "|Query does not contain update query|"
        End Function
        Public Shared Function ScreenCodeError(ByVal Screen As ConnectOneWS.ClientScreen) As String
            Return "|Wrong Screen Code:" & Screen.ToString() & "|"
        End Function
        Public Shared Function PermissionError(ByVal Permission As String) As String
            Return "|User not allowed " & Permission & " on this screen.|"
        End Function

        Public Shared Function SyncBeforeLoginError() As String
            Return "|Sorry! You Need to Sync Data First... Please Click Options -> Sync Data to start sync|"
        End Function

        Public Shared Function LoginRestrictionError() As String
            Return "|Sorry! You are denied login by Madhuban. Some Administrative / Audit activity is under process.|"
        End Function

        Public Shared Function LoginBarredForAuditorFromSelectCenterError() As String
            Return "|Sorry! Auditors can login only through Start-> Auditor Login.|"
        End Function

        Public Shared Function ChangesBarredForAuditorInUnAssignedYear(CurrYear As String, AllottedYear As String) As String
            Return "|Sorry! Current Auditor not allowed to make changes in data for year " & CurrYear.Substring(0, 2) & "-" & CurrYear.Substring(2, 2) & ". Allotted Year is " & AllottedYear.Substring(0, 2) & "-" & AllottedYear.Substring(2, 2) & " |"
        End Function

        Public Shared Function ScreenRestrictionError() As String
            Return "|Sorry! Current Operation on this screen has been locked for this center...|"
        End Function

        Public Shared Function ProfileRestrictionError() As String
            Return "|Sorry! Profile Data has been locked for this center...|"
        End Function

        Public Shared Function PeriodRestrictionError(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As String
            Return "|Sorry! No Transactions are allowed between " & FromDate.ToString("dd-MM-yyyy") & " and " & ToDate.ToString("dd-MM-yyyy") & " as the period has been locked / Audited by Madhuban...|"
        End Function
        Public Shared Function PeriodRestrictionError(ByVal FromDate As DateTime, ByVal ToDate As DateTime, BlockedReference As String) As String
            Return "|Sorry! Transactions using " + BlockedReference + " are not allowed between " & FromDate.ToString("dd-MM-yyyy") & " and " & ToDate.ToString("dd-MM-yyyy") & " as the period has been locked / Audited by Auditor...|"
        End Function

        Public Shared Function AccountsSubmitted_PeriodRestrictionError(ByVal FromDate As DateTime, ByVal ToDate As DateTime) As String
            Return "|Sorry! No Transactions are allowed between " & FromDate.ToString("dd-MM-yyyy") & " and " & ToDate.ToString("dd-MM-yyyy") & " as accounts for the period has been submitted by you...!" & vbNewLine & " Please Unsubmit accounts from 'Start-> Submit Accounts' to allow this operation. |"
        End Function

        Public Shared Function VersionError(ByVal AppVersion As [String]) As String
            Return "|Sorry! You are using old version:" & AppVersion & ". Please click on OPTIONS and click CHECK FOR UPDATES. For help contact CONNECTONE TEAM on 09414146881,09667584040|"
        End Function

        Public Shared Function SpecialCentreError() As String
            Return "|Sorry! You are not allowed to login in this centre. For help contact CONNECTONE TEAM on 09414146881,09667584040|"
        End Function

        Public Shared Function UnderMaintenanceError() As String
            '  Return "|Sorry! Some Maintenance Activity is going on in Madhuban. Please try after some time.|"
            Dim DownTill As String = "some time"
            If Not ConfigurationManager.AppSettings("DownTill") Is Nothing Then
                DownTill = ConfigurationManager.AppSettings("DownTill").ToString()
            End If
            Return "|Sorry! Some Maintenance Activity is going on in Madhuban. Please try after " + DownTill + ".|"
        End Function

        Public Shared Function CSDUserError() As String
            Return "|Sorry! Current User can not login. It is meant for Software Data collection only.|"
        End Function

        Public Shared Function InvalidUser() As String
            Return "|Incorrect Username...!|" 'Redmine Bug #132577 fix
        End Function

        Public Shared Function InvalidLocation() As String
            Return "|Sorry! You seem to have entered User with Invalid location|"
        End Function

        Public Shared Function YearAlreadyAudited() As String
            Return "|Sorry! No Changes are allowed in a Center's data after prepration of final balance sheet.|"
        End Function

        Public Shared Function ReadOnlyUser() As String
            Return "|Sorry! No Changes are allowed in Current User's Login.|"
        End Function

        Public Shared Function MagazineDateRestricted(TxnDate As String) As String
            Return "|Sorry! No Changes are allowed in Current User's Login on Transaction Date :" & TxnDate & ".|"
        End Function

        Public Shared Function financial_changes_blocked() As String
            Return "|Sorry! Financial Changes have been blocked by Administration.|"
        End Function

        Public Shared Function YearAlreadyCancelled() As String
            Return "|Sorry! No Changes are allowed in a Center's data as Center has been cancelled in Current financial year.|"
        End Function

        Public Shared Function VersionNotAllowed4_3_7() As String
            Return "|Sorry! Current Version is not allowed . Please update to latest version.|"
        End Function

        Public Shared Function Restricted_User(User_ID As String) As String
            'Return "|Sorry! The User: " & User_ID & " is allowed in FY-2324 of PBKIVV or WRST Institute only.|"
            Return "|Sorry! This data is not allowed to logged-in user.|"
        End Function
#End Region
    End Class
End Namespace



