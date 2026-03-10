Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Xml.Serialization
Imports System.IO.Compression
Imports System.IO
Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Hosting
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Requests
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Services
Imports System.Threading

Namespace Real
    '<WebService(Namespace:="http://accountserver.bkinfo.in/v4.0.45.0/")> _
    <Serializable>
    Partial Public Class ConnectOneWS
        Inherits System.Web.Services.WebService

#Region "--Variables & Functions--"
        Dim _Wrap_ExecuteFunctionDB As New Wrap_ExecuteFunctionDB
        Public Enum Tables
            ACC_LEDGER_INFO
            ACTION_ITEM_INFO
            ADDRESS_BOOK
            ADDRESS_BOOK_MAGAZINE_INFO
            ADDRESS_BOOK_WING_INFO
            ADDRESS_BOOK_SPECIAL_INFO
            ADDRESS_BOOK_EVENTS_INFO
            ADVANCES_INFO
            ASSET_INFO
            ASSET_IMAGES
            ASSET_LOCATION_INFO
            ASSET_LOCATION_CHANGES_INFO
            ATTACHMENT_INFO
            ATTACHMENT_REFERENCE_INFO
            BANK_ACCOUNT_INFO
            BANK_BRANCH_INFO
            BANK_INFO
            CENTRE_INFO
            CENTRE_TASK_INFO
            CENTRE_SUPPORT_INFO
            CENTER_PURPOSE_INFO
            CLIENT_USER_INFO
            CLIENT_USER_GROUP
            CLIENT_USER_TASK
            CLIENT_GROUP_INFO
            COD_INFO
            COMPLEX_INFO
            COMPLEX_BUILDING_INFO
            CONSUMABLES_STOCK_INFO
            CURRENCY_INFO
            DEPOSITS_INFO
            DONATION_STATUS_INFO
            DONATION_RECEIPT_INFO
            DONATION_RECEIPT_ADDRESS_BOOK
            DONATION_RECEIPT_PRINT_INFO
            DONATION_RECEIPT_DISPATCH_INFO
            FD_INFO
            FD_HISTORY_INFO
            GODLY_SERVICE_MATERIAL_INFO
            GOLD_SILVER_INFO
            INSTITUTE_INFO
            ITEM_INFO
            JOB_INFO
            JOB_ASSIGNEE_INFO
            JOB_ITEM_ESTIMATION
            JOB_MANPOWER_ESTIMATION
            JOB_MANPOWER_USAGE
            JOB_MACHINE_USAGE
            JOB_EXPENSES_INCURRED
            LAND_BUILDING_INFO
            LAND_BUILDING_EXTENDED_INFO
            LAND_BUILDING_DOCUMENTS_INFO
            LIABILITIES_INFO
            LETTER_INFO
            LIVE_STOCK_INFO
            MAP_CITY_INFO
            MAP_COUNTRY_INFO
            MAP_STATE_INFO
            MAP_DISTRICT_INFO
            MAP_SUB_CITY_INFO
            MISC_INFO
            MURLI_INFO
            MEMBERSHIP_INFO
            MEMBERSHIP_BALANCES_INFO
            MEMBERSHIP_RECEIPT_INFO
            MEMBERSHIP_RECEIPT_PRINT_INFO
            MAGAZINE_INFO
            MAGAZINE_SUBS_TYPE
            MAGAZINE_SUBS_TYPE_FEE
            MAGAZINE_DISPATCH_INFO
            MAGAZINE_DISPATCH_TYPE
            MAGAZINE_DISPATCH_TYPE_CHARGES
            MAGAZINE_GP_INFO
            MAGAZINE_MEMBERSHIP_INFO
            MAGAZINE_MEMBERSHIP_BALANCES_INFO
            MAGAZINE_MEMBERSHIP_REQUEST_INFO
            MAGAZINE_MEMBERSHIP_CC_INFO
            MAGAZINE_RECEIPT_INFO
            MAGAZINE_ISSUE_INFO
            MAGAZINE_DISPATCH_BUNDLES
            NEWS_INFO
            NOTES_INFO
            OPENING_BALANCES_INFO
            OVERSEAS_CENTRE_INFO
            OTHER_PROFILE_INFO
            POLICY_MASTER_INFO
            PROJECT_INFO
            PROJECT_ASSIGNEE_INFO
            PROJECT_ESTIMATION
            PURCHASE_ORDER_INFO
            PURCHASE_ORDER_ITEM
            PURCHASE_ORDER_ITEM_RECEIVED
            PURCHASE_ORDER_ITEM_RETURNED
            PURCHASE_ORDER_ITEM_TAXES
            PURCHASE_ORDER_PAYMENTS_MADE
            PURCHASE_ORDER_PRICE_HISTORY
            REMINDER_INFO
            REQUEST_BOX
            REQUISITION_REQUEST_INFO
            REQUISITION_REQUEST_ITEM
            REQUISITION_REQUEST_ITEM_TAXES
            RR_PO_MAPPING
            SERVICE_PLACE_INFO
            SERVICE_REPORT_INFO
            SERVICE_REPORT_ATTACHMENT_INFO
            SERVICE_REPORT_LOG_INFO
            SERVICE_USERS_INFO
            SERVICE_REPORT_WING_INFO
            SERVICE_REPORT_GUEST_INFO
            SERVICE_MATERIAL_WING_INFO
            SLIP_INFO
            SO_CENTER_AUDIT_STATS
            SO_CLIENT_USER_TASK
            SO_CLIENT_TASK_INFO
            SO_CLIENT_PERMISSION_LIST
            SO_CLIENT_RESTRICTIONS
            SO_CENTER_SUBMISSION_ACCOUNTS
            SO_HO_EVENT_INFO
            SO_LAST_USER_SESSION
            SO_AUDIT_VERIFICATIONS
            SO_CENTRE_NOTIFICATIONS
            SO_TRIAL_BALANCE
            SO_USER_AUTO_OPEN_SCREENS
            SO_USER_LISTING_SCREEN_PREFERENCE
            STOCK_INFO
            Get_Stock_Curr_Qty_fn
            STOCK_DOCUMENT_INFO
            STOCK_TRANSFER_ORDER
            STOCK_PERSONNEL_INFO
            STOCK_PERSONNEL_CHARGES
            STOCK_PERS_SKILLS_MAPPING
            STOCK_PRODUCTION_INFO
            STOCK_PRODUCTION_EXPENSES_INCURRED
            STOCK_PRODUCTION_ITEMS_CONSUMED
            STOCK_PRODUCTION_ITEMS_PRODUCED
            STOCK_PRODUCTION_MACHINE_USAGE
            STOCK_PRODUCTION_MANPOWER_USAGE
            STOCK_REMARKS_INFO
            STOCK_SUPPLIER_INFO
            STOCK_SUPPLIER_BANK
            STOCK_TOOLS_ISSUE_INFO
            STOCK_TOOLS_ISSUE_ITEMS
            STOCK_TOOLS_RETURN_INFO
            STORE_DEPT_INFO
            STORE_DEPT_LOCATION_INFO
            SUB_ITEM_INFO
            SUB_ITEM_STORE_MAPPING
            SUB_ITEM_UNIT_CONVERSION
            SUB_ITEM_PROPERTIES
            SUBSCRIPTION_INFO
            SUBSCRIPTION_FEE_INFO
            SUPPLIER_ITEM_MAPPING
            TDS_INFO
            TELEPHONE_INFO
            TRANSACTION_INFO
            TRANSACTION_D_FOREIGN_INFO
            TRANSACTION_D_ITEM_INFO
            TRANSACTION_D_REFERENCE_INFO
            TRANSACTION_D_PAYMENT_INFO
            TRANSACTION_D_PURPOSE_INFO
            TRANSACTION_D_MASTER_INFO
            TRANSACTION_D_OTHER_INFO
            TRANSACTION_D_SLIP_INFO
            TRANSACTION_D_TDS_INFO
            TRANSACTION_D_DENOMINATION_INFO
            TRANSACTION_DOC_MAPPING
            TRANSACTION_DOC_MAPPING_RESPONSE
            USER_ORDER
            USER_ORDER_ITEM
            USER_ORDER_ITEM_RECEIVED
            USER_ORDER_ITEM_DELIVERED
            USER_ORDER_ITEM_DELIVERED_STOCKS
            USER_ORDER_ITEM_RETURNED
            USER_ORDER_ITEM_RETURN_RECEIVED
            USER_ORDER_UO_RR_MAPPING
            USER_ORDER_CENTER_INFO
            USER_ORDER_STORE_LOCATION_INFO
            USER_ORDER_STOCK_INFO
            VEHICLES_INFO
            VOUCHING_AUDIT
            VOUCHING_AUDIT_RESPONSES
            VOUCHING_DOCUMENT_CHECKING
            WINGS_INFO
            WIP_INFO
            WIP_ITEM_MAPPING
            ATTACHMENT_CHECKING_ALLOTTMENT
            ZONE_INFO
            ZONE_SUB_INFO
            VOUCHING_AUDIT_ALLOTTMENT
            DataBase_xls
            IndRateChart_xls
            LastDate_xls
            OthRateChart_xls
            PersonStatus_xls
            TermsNConditions_xls
            SERVICE_USER_ASKQUERY
            SERVICE_USERS_FEEDBACK
            SERVICE_REPORT_ACTIVITY_INFO
            SERVICE_FEEDBACK_QUERY_INFO
            SERVICE_FEEDBACK_QUERY_REF
            SERVICE_REPORT_PROGRAM_TYPE_INFO
            FORM_INFO
            FORM_RESPONSE_INFO
            FORM_QUESTION_INFO
            FORM_QUESTION_OPTION_INFO
            SERVICE_CHART_INFO
            SERVICE_CHART_QUESTIONS
            SERVICE_CHART_RESPONSES
            SERVICE_CHART_SRNO
            EMAIL_SCHEDULER_LOG
            SERVICE_REPORT_PROGRAM_OCCASION_INFO
            CLIENT_USER_DEVICE_DETAILS
            CLIENT_USER_DEVICE_IDENTITY
            ANDROID_DEVICE_TOKEN
            SERVICE_CHART_QUESTION_OPTIONS
            SERVICE_CHART_PROFILE_SETTINGS
            SERVICE_CHART_IMAGE_AND_STYLE_INFO
            TEMP_SERVICE_CHART_GUIDE_LINK
            SERVICE_CHART_RESPONSES_MISC_DETAIL
            SERVICE_CHART_VISIBILITY_INFO
            CENTRE_ACC_TYPE_INFO
            SERVICE_PROJECT_INFO
            SERVICE_USERS_PROJECT_MAPPING
            NOTIFICATION_SCHEDULE_INFO
            SCHEDULER_INFO
            NOTIFICATION_INFO
            SCHEDULER_MAPPING
            SCHEDULER_QUEUE
            SCHEDULER_LOG
            USERS_ANDROID_DEVICE_TOKEN
            SERVICE_USERS_CHART_MAPPING
            SERVICE_REPORT_ADDITIONAL_INFO
            SERVICE_REPORT_COLLABORATECENTERS_INFO
            SCHEDULER_TIMEBAND_INFO
            SCHEDULER_INSTANCE_INFO
            SCHEDULER_INSTANCE_QUEUE
            SCHEDULER_INSTANCE_MAPPING
            SCHEDULER_INSTANCE_LOG
            SERVICE_CHART_RESPONSES_ACCOMMODATION_DETAILS
            SERVICE_CHART_RESPONSES_MASTER_INFO
            EMAIL_VALIDATION_STATUS
            ANDROID_NOTIFICATION_LOG
            BULK_ALLOTTMENT_ACCOMMODATION_INFO
            SERVICE_RESPONSE_BULKALLOTTMENT_RECOMMENDATION
            SERVICE_CHART_GROUP_PROFILE_SETTINGS
            REFERENCE_OPENING_INFO
            SERVICE_REPORT_VENUE_TYPE_INFO
            BANK_BALANCE_CHECKING_INFO
            WHATSAPP_MESSAGE_QUEUE
            NOTIFICATION_TEMPLATE_INFO
            SERVICE_CHART_NOTIFICATION_SETTINGS
            NOTIFICATION_BATCHES_INFO
            EMAIL_SCHEDULER_QUEUE
            WHATSAPP_MSG_DELIVERY_LOG
            ADDRESS_BOOK_ADDITIONAL_INFO
            MOBILE_LOGIN_OTP_INFO
            TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET
            PAYMENT_GATEWAY_LOG
            PROPERTY_TYPE_CHANGE_LOG
            API_ACCOUNTS_ACTIVATED
            BANK_TRANSACTION_INFO
            BANKSTATEMENT_REFERENCE_INFO
        End Enum
        Public Enum ClientScreen
            Start_Create_Center
            Start_Select_Center
            Start_Login
            Start_Auditor_Login
            Start_Auditor_change_Center
            Start_Auditor_change_Instt
            Start_Auditor_Notify
            Start_Auditor_Remove_Notification
            Start_Auditor_Return_Correction
            Start_Auditor_Submit_Report
            Start_Auditor_Audit_Verification
            Account_CashbookAuditor
            Start_Auditor_Audit_Info
            Start_Submission_History
            Start_Submit_Accounts
            Start_DocumentChecking
            Start_Audit_Registration
            Profile_Telephone
            Profile_LiveStock
            Profile_Vehicles
            Profile_GoldSilver
            Profile_StockOfConsumables
            Profile_Assets
            Profile_Advances
            Profile_BankAccounts
            Profile_Cash
            Profile_Core
            Profile_Deposit
            Profile_Deposit_Slips
            Profile_Liabilities
            Profile_ServicePlaces
            Profile_FD
            Profile_LandAndBuilding
            Profile_ServicableSouls
            Profile_Students
            Profile_Report
            Profile_Membership
            Profile_OpeningBalances
            Profile_Insurance_ChangeDetail
            Profile_Insurance_ChangeValue
            Profile_ChangeLocation
            Profile_Complexes
            Profile_WIP
            Profile_Magazine
            Profile_Stock
            Facility_AddressBook
            Facility_Letter
            Facility_Notes
            Facility_ServiceReport
            Facility_GodlyServiceMaterial
            Facility_Reminder
            Facility_Magazine_Request
            Facility_Center_Purpose
            Options_ChangePassword
            Options_ResetPassword
            Options_Maintenance
            Options_DocLibrary
            Options_SyncData
            Options_GroupMaster
            Options_UserMaster
            Options_UserRegister
            Options_Manage_Privileges
            Accounts_Vouchers
            Accounts_DonationRegister
            Accounts_DraftEntryRegister
            Account_TDS_Register
            Accounts_Notebook
            Accounts_Voucher_CollectionBox
            Accounts_Voucher_BankToBank
            Accounts_Voucher_CashBank
            Accounts_Voucher_Donation
            Accounts_Voucher_Gift
            Accounts_Voucher_Receipt
            Accounts_Voucher_Payment
            Accounts_Voucher_FD
            Accounts_Voucher_Property
            Accounts_Voucher_Internal_Transfer
            Accounts_Voucher_SaleOfAsset
            Magazine_Receipt_Register
            Accounts_Voucher_Membership
            Accounts_Voucher_Membership_Renewal
            Accounts_Voucher_Membership_Conversion
            Accounts_Voucher_Journal
            Accounts_Voucher_AssetTransfer
            Accounts_Voucher_WIP_Finalization
            Accounts_CashBook
            Membership_Receipt_Register
            Report_Items
            Report_Items_Documents
            Report_Landscape
            Report_Potrait
            Report_Transaction_Summary
            Report_Construction_Statement
            Report_Collection_Box
            Report_Transaction_Statement
            Report_Collection_Box_Voucher
            Report_Payment_Voucher
            Report_Gift
            Report_PartyListing
            Report_PartyReport
            Report_ItemReport
            Report_LedgerReport
            Report_SecondaryGroupReport
            Report_PrimaryGroupReport
            Report_FDReport
            Report_Vehicles
            Report_Assets
            Report_GS
            Report_LB
            Report_Purpose
            Report_Potamail
            Report_InsuranceLetter
            Report_AssetInsurance_Breakup
            Report_ConsumableValue_Breakup
            Report_WIPInfo
            Report_Construction_List
            Report_Asset_Movement_Logs
            Report_Daily_Balances
            Report_Deposit_Slips
            Report_Bank_Account_List
            Report_Income_Expenditure
            Report_MagSubDispatch
            Report_VoucherReference
            Stock_Sub_Item
            Stock_Project
            Stock_Job
            Stock_UO
            Stock_Personnel_Master
            Stock_Dept_Store_Master
            Stock_Supplier_Master
            Stock_RR
            Stock_PO
            Stock_Production
            Stock_Machine_Tool_Issue
            Help_News
            Help_Request_Box
            Help_Action_Items
            Help_Attachments
            Home_Murli
            Home_StartUp
            Options_ClientUser
            Global_Set
            Core_Add_AssetLocation
            CommonFunctions
            Profile_Existing_Mag_member
            Account_InternalTrf_Register
            Report_Location
            Report_BS
            Report_TB
            Report_Utilization
            Statement_Main
            Statement_CashBook
            Statement_Potamel
            Statement_CollectionBox
            Statement_Donation
            Statement_Property
            Statement_Movable
            Statement_Vehicle
            Statement_GS
            Statement_FD
            Statement_Telaphonebill
            Magazine_Membership
            Magazine_SubCity
            Magazine_Dispatch_Register
            Magazine_Dispatch_Type_Master
            Account_Document_Library
            Auditor_DataRestriction
            Option_UserPreferences
            Report_User_Rights
            Report_AddressBook_Search
            Service_Module
            Report_SpecialVoucherReference
            KarunaSankalpChart
            Options_createForm
            Options_FormResponse
            Facility_ChartInfo
            Facility_ChartResponsesInfo
            Facility_ServiceProject
            Facility_SparcProject
            Help_Scheduler
            Facility_Accommodation_Register
            Start_BankChecking
            Facility_Accommodation_Short
            Report_TDS_Applicability
            Report_MultiBank_CashBook
        End Enum
        Public Enum Permissions
            ''' <summary>
            ''' Listing-L
            ''' </summary>
            Listing
            ''' <summary>
            '''Delete-D
            ''' </summary>
            Delete
            ''' <summary>
            '''Add New-N
            ''' </summary>
            Add
            ''' <summary>
            ''' Edit-E
            ''' </summary>
            Edit
            ''' <summary>
            ''' Complete-C
            ''' </summary>
            Complete
            ''' <summary>
            ''' Detail-d
            ''' </summary>
            Detail
            ''' <summary>
            ''' Login-g
            ''' </summary>
            Login
            ''' <summary>
            ''' Lock-l
            ''' </summary>
            Lock
        End Enum
        Public Enum AssetProfiles
            ''' <summary>
            ''' GOLD
            ''' </summary>
            ''' <remarks></remarks>
            GOLD
            ''' <summary>
            ''' SILVER
            ''' </summary>
            ''' <remarks></remarks>
            SILVER
            ''' <summary>
            ''' OTHER ASSETS
            ''' </summary>
            ''' <remarks></remarks>
            OTHER_ASSETS
            ''' <summary>
            ''' VEHICLES
            ''' </summary>
            ''' <remarks></remarks>
            VEHICLES
            ''' <summary>
            ''' LIVESTOCK
            ''' </summary>
            ''' <remarks></remarks>
            LIVESTOCK
            ''' <summary>
            ''' LAND & BUILDING
            ''' </summary>
            ''' <remarks></remarks>
            LAND_BUILDING
            ''' <summary>
            ''' ADVANCES
            ''' </summary>
            ''' <remarks></remarks>
            ADVANCES
            ''' <summary>
            ''' OTHER DEPOSITS
            ''' </summary>
            ''' <remarks></remarks>
            OTHER_DEPOSITS
            ''' <summary>
            ''' OTHER LIABILITIES
            ''' </summary>
            ''' <remarks></remarks>
            OTHER_LIABILITIES
            ''' <summary>
            ''' OTHER PROFILES
            ''' </summary>
            ''' <remarks></remarks>
            OTHER_OPENING_BALANCES
            ''' <summary>
            ''' WORK IN PROGRESS
            ''' </summary>
            ''' <remarks></remarks>
            WIP
            ''' <summary>
            ''' Fixed Deposits
            ''' </summary>
            ''' <remarks></remarks>
            FD
        End Enum
        Public Class BasicInputParams
            Public UserID As String
            Public tablename As Tables
            Public CenID As String
        End Class
        Public Class Basic_Param
            Public screen As ClientScreen
            Public openUserID As String
            Public openCenID As Integer
            Public PCID As String
            Public version As String
            Public openYearID As Integer
            Public ShowVouchingIndicator As Boolean
            Public ShowAttachmentIndicator As Boolean

            Public Function Clone() As Basic_Param
                Dim new_Basic_Param As New Basic_Param
                new_Basic_Param.openCenID = Me.openCenID
                new_Basic_Param.openUserID = Me.openUserID
                new_Basic_Param.openYearID = Me.openYearID
                new_Basic_Param.PCID = Me.PCID
                new_Basic_Param.screen = Me.screen
                new_Basic_Param.version = Me.version
                new_Basic_Param.ShowVouchingIndicator = Me.ShowVouchingIndicator
                new_Basic_Param.ShowAttachmentIndicator = Me.ShowAttachmentIndicator
                Return new_Basic_Param
            End Function
        End Class

        Public Class PrevRec_ParamsForReInsertion
            Public LastAddOn As DateTime
            Public LastStatus As Integer
            Public LastStatusOn As DateTime
            Public LastAddBy As String
            Public LastStatusBy As String
        End Class

        Public Enum ScreenAreas
            Top_Buttons
            Right_Click_Menu
            Footer_Buttons
        End Enum

        Public Enum RealServiceFunctions
            ActionItems_GetOverDueCount
            ActionItems_GetPendingCentreRemarkCount
            ActionItems_GetList
            ActionItems_GetOpenActions_Common
            ActionItems_GetRemarksStatus
            ActionItems_Insert
            ActionItems_Update
            ActionItems_UpdateCentreRemarks
            ActionItems_Close
            ActionItems_InsertActionItems_Txn
            ActionItems_UpdateActionItems_Txn
            AddressBookRelated_Addresses_GetList
            AddressBookRelated_Addresses_GetList_Common
            AddressBookRelated_Addresses_GetAddressUsageCount
            AddressBookRelated_Addresses_GetAddressRecID
            AddressBookRelated_Addresses_GetAddressRecID_FromOrgID
            AddressBookRelated_Addresses_GetPartiesList
            AddressBookRelated_Addresses_GetPartiesListForSpecifiedRecIds
            AddressBookRelated_Addresses_GetCenterList
            AddressBookRelated_Addresses_GetOverseasCenterList
            AddressBookRelated_Addresses_GetDuplicateCount
            AddressBookRelated_Addresses_GetAddressRecIDs_ForAllYears
            AddressBookRelated_Addresses_Insert
            AddressBookRelated_Addresses_Update
            AddressBookRelated_Addresses_InsertMagazine
            AddressBookRelated_Addresses_InsertWings
            AddressBookRelated_Addresses_InsertSpecialities
            AddressBookRelated_Addresses_InsertEvents
            AddressBookRelated_Addresses_GetAddressesForLabels
            AddressBookRelated_ServiceableSouls_GetListOfSouls
            AddressBookRelated_Students_GetListOfStudents
            AddressBookRelated_GetOrgList
            Addresses_InsertAddresses_Txn
            Addresses_UpdateAddresses_Txn
            Addresses_DeleteAddresses_Txn
            Addresses_MergeParties
            Advances_GetList
            Advances_GetProfileListing
            Advances_GetList_Common
            Advances_GetListByCondition
            Advances_GetPayments
            Advances_GetAdvancePaymentCount
            Advances_GetPaymentAdvances
            Advances_Insert
            Advances_InsertTRID
            Advances_Update
            Assets_GetList
            Assets_GetRecord
            Assets_GetRecId_From_OrgID
            Assets_GetProfileListing
            Assets_GetListByCondition
            Assets_GetList_Common
            Assets_GetList_Common_ByRecID
            Assets_GetTransactions
            Assets_GetTransactions_ByTable
            Assets_Get_Asset_Ref_MaxEditOn
            Assets_UpdateAssetLocationIfNotPresent
            Assets_Insert
            Assets_InsertTRIDAndTRSrNo
            Assets_Update
            Assets_UpdateLocation
            Assets_UpdateInsuranceValue
            AssetLocations_GetList
            AssetLocations_GetStockLocationList
            AssetLocations_GetListByLBID
            AssetLocations_GetListBySPID
            AssetLocations_GetFullList
            AssetLocations_GetRecordCountByName
            AssetLocations_GetRecordCountByName_CurrentUID
            'AssetLocations_InsertIfDefaultNotPresent
            AssetLocations_GetDefaultLocation
            AssetLocations_Insert
            AssetLocations_Insert_AllSisterUIDs
            AssetLocations_UpdateByReference
            AssetLocations_UpdateMatching
            AssetLocations_Update
            AssetLocations_GetLocationCountInAssets
            AssetLocations_GetLocationCountInGS
            AssetLocations_GetLocationCountInConsumables
            AssetLocations_GetLocationCountInLiveStock
            'AssetLocations_GetLocationCountInTelephones
            AssetLocations_GetLocationCountInVehicles
            AssetLocations_GetLocationMatching
            AssetLocations_GetLocationMatchingCount
            AssetLocations_GetLocationsMatchedCount
            AssetLocations_GetPropertyID
            AssetLocations_Get_Asset_Movement_Logs
            Attachments_Insert
            Attachments_InsertLink
            Attachments_GetList
            Attachments_Delete
            Attachments_Unlink
            Attachments_Mark_as_Checked
            Attachments_Mark_as_Unchecked
            Attachments_Mark_as_Rejected
            Attachments_Update
            Attachments_GetRecord
            Attachments_DownloadFile
            Attachments_GetAttachmentLinkCount
            Attachments_GetAttachmentCount
            Attachments_Delete_Attachment_ByDescription
            Audit_GetExceptions
            Audit_GetDocumentMapping
            Audit_GetDoc_AdditionalInfo
            Audit_GetDocumentchecking
            Audit_InsertDocumentMapResponse
            Audit_InsertVouchingStatus
            Audit_UpdtaeDocumentMapResponse
            Audit_DeleteAllVouchingAgainstEntry
            Audit_DeleteAllVouchingAgainstReference
            Bank_GetList
            Bank_GetList_Common
            Bank_GetValue_Common
            Bank_GetFDAccountList
            Bank_GetTxnsCountByAccID
            Bank_GetTransactionMaxDate
            Bank_GetFDCount
            Bank_GetFDSum
            Bank_GetTransCount
            Bank_GetTransSums
            Bank_GetPaymentTransSums
            Bank_GetReceiptTransSums
            Bank_GetCountByAccountNo
            Bank_GetCountByCustomerNo
            Bank_GetFDBankByCustomerNo
            Bank_Insert_Bank_and_Balance
            Bank_Update_Bank_and_Balance
            Bank_Close
            Bank_Reopen
            Bank_GetRecord
            Bank_GetClosedBank
            BankToBank_Insert
            BankToBank_Update
            Bank_GetClosedBankAccNo
            Cash_AddDefault
            Cash_Update
            Cash_GetCashOpeningBalance
            CashWithdrawDeposit_Insert
            CashWithdrawDeposit_Update
            Vouchers_GetSplVoucherRefsFromCenterTasks
            CentreRelated_Core_GetCenSupportInfo
            CentreRelated_Core_GetSupportRowCount
            CentreRelated_Core_GetList
            CentreRelated_Core_Insert
            CentreRelated_Core_Update
            CentreRelated_Center_GetChildCenterList
            CentreRelated_Center_GetCenterListByAuditor_Instt
            CentreRelated_Center_GetCenterListByPAD_Name
            CentreRelated_Center_GetCenterListByCenID
            CentreRelated_Center_GetCenterDetailsByCertNo
            CentreRelated_Center_GetCenterListByBKCertNo
            CentreRelated_Center_GetAuditTxnPeriod
            CentreRelated_Center_GetTxnStatusCount
            CentreRelated_Center_LogOut
            CentreRelated_Center_SubmitReport
            CentreRelated_Center_GetTransfersStatus
            CentreRelated_Center_GetUnlockedTxnCount
            CentreRelated_Center_IsFinalAuditCompleted
            CentreRelated_Center_IsFinalReportSubmitted
            CentreRelated_Center_Get_Base_OpenEventId
            CentreRelated_Center_IsReportSubmitted
            CentreRelated_Center_GetlatestCenterGrade
            CentreRelated_Center_Get_Contact_Info
            CentreRelated_ClientUserInfo_GetControlVisibility
            CentreRelated_ClientUserInfo_GetUserInfo
            CentreRelated_ClientUserInfo_GetUsersList
            CentreRelated_ClientUserInfo_GetUserDetails
            CentreRelated_ClientUserInfo_GetUserInfoCustomCenId
            CentreRelated_ClientUserInfo_AddNew
            CentreRelated_ClientUserInfo_ChangePassword
            CentreRelated_ClientUserInfo_GetList
            CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId
            CentreRelated_ClientUserInfo_GetListByCenID
            CentreRelated_ClientUserInfo_GetUserCount
            CentreRelated_ClientUserInfo_GetMaxUserID
            CentreRelated_ClientUserInfo_GetUserNameCount
            CentreRelated_ClientUserInfo_Get_ClientUser_EntriesCount
            CentreRelated_ClientUserInfo_Get_ClientUser_GetGroupDetails
            CentreRelated_CodInfo_AddNew
            CentreRelated_CodInfo_GetCreatedCentersFromSelected
            CentreRelated_CodInfo_GetYearCount
            CentreRelated_CodInfo_GetCompletedYearCount
            CentreRelated_CodInfo_GetFinancialYearList
            CentreRelated_CodInfo_GetUnAuditedYearList
            CentreRelated_CodInfo_Base_GetCen_Ins_List
            CentreRelated_CodInfo_Base_GetSelectCentreList
            CentreRelated_CodInfo_GetUnAuditedFinancialYearOfTransferorCentre
            CentreRelated_CodInfo_IsYearOpenForCenterCreation
            CentreRelated_CodInfo_GetReportsToBePrinted
            CentreRelated_AddAuditVerifications
            CentreRelated_AddInsuranceVerification
            CentreRelated_GetCompletedAuditVerifications
            CentreRelated_GetNotifications
            CentreRelated_AddNotification
            CentreRelated_GetAuditPeriod
            CentreRelated_GetAddressesForLabels
            CentreRelated_VerifyAudit
            CentreRelated_ReturnForCorrection
            CentreRelated_ResumeAudit
            CentreRelated_get_Client_Audit_Info
            CentreRelated_CheckVolumeCenter
            CentreRelated_GetAccountsSubmittedPeriod
            CentreRelated_AddAccountsSubmissionPeriod
            CentreRelated_GetAccountsSubmissionReport
            CentreRelated_Get_CSD_sync_count

            CentreRelated_ClientUserInfo_GetRegister
            CentreRelated_ClientUserInfo_InsertClientUser
            CentreRelated_ClientUserInfo_UpdateClientUser
            CentreRelated_ClientGroupInfo_GetGroupRegister
            CentreRelated_ClientGroupInfo_InsertClientUserGroup
            CentreRelated_ClientGroupInfo_UpdateClientUserGroup
            CentreRelated_ClientGroupInfo_GetGroupNameCount
            CentreRelated_ClientUserInfo_SaveClientUserGroupMapping
            CentreRelated_ClientUserInfo_GetPrivilegeRegister
            CentreRelated_ClientUserInfo_GetScreens
            CentreRelated_ClientUserInfo_GetPrivileges
            CentreRelated_ClientUserInfo_InsertClientUserPrivileges
            CentreRelated_ClientUserInfo_UpdateClientUserPrivileges


            CenterPurpose_GetList
            CenterPurpose_CheckUsageCount
            CenterPurpose_Insert
            CenterPurpose_Update
            CenterPurpose_Activate
            CenterPurpose_DeActivate
            Complexes_GetList
            Complexes_GetInstList
            Complexes_GetBuildingList
            Complexes_Get_LB_Listing_Profile
            Complexes_GetAllComplexBuildings
            Complexes_GetRecordCountByName
            Complexes_GetMaxEditOn
            Complexes_IsPropertyAlreadyMapped
            Complexes_Insert
            Complexes_Insert_Building
            Complexes_Update
            Complexes_Insert_Complexes_Txn
            Complexes_DeleteComplexBuilding_Txn
            Complexes_Update_ManageBuildings_Txn
            Consumables_GetList
            Consumables_GetList_Summary
            Consumables_UpdateAssetLocationIfNotPresent
            Consumables_Insert
            Consumables_Update
            Consumables_GetYearEnding
            CollectionBox_GetPastWitness
            CollectionBox_CheckUsageAsPastWitness
            CollectionBox_Insert
            CollectionBox_Update
            CollectionBox_GetDenominations
            Core_GetHOEvents
            Core_GetTDSRecords_Common
            Core_GetBankInfo
            Core_GetBankInfo_Common
            Core_GetBankBranchesForMultipleIDs
            Core_GetBankBranchesForMultipleIDsWithCustomColumnNames
            Core_GetBankBranchesByBankID
            Core_GetBankBranches
            Core_GetMisc
            Core_GetMiscDetails
            Core_GetMiscDetails_Common
            Core_GetMisc_Common
            Core_GetItemsByItemProfile
            Core_GetItemsAllItems
            Core_GetItemsByMultipleItemIDs
            Core_GetItemsByQuery_Common
            Core_GetItemsByMultipleItemProfiles
            Core_GetItems
            Core_GetItems_Ledger
            Core_GetItems_Ledger_Common
            Core_GetLedgersList
            Core_GetItemDetails
            Core_GetOpeningProfileItems
            Core_GetCenterTaskInfo
            Core_GetWingsCenterTaskInfo
            Core_GetCurrencyName
            Core_GetCurrencyList
            Core_GetHQCentersForCurrInstt
            Core_GetCenterDetailsForCenID
            Core_GetCenterDetailsForLetterPAD
            Core_GetCenterAddress
            Core_GetOrgPasswordForCenID
            Core_GetCenIDForBKPad
            Core_GetCenterDetailsByQuery_Common
            Core_GetCentersByBKPAD
            Core_GetCenterIDByCenRecID
            Core_GetInstituteList
            Core_GetInstituteListNameAndID
            Core_GetInstituteListNameInShort
            Core_GetInstituteDetails
            Core_GetWingsList
            Core_GetCountriesList
            Core_GetCountriesByID
            Core_GetStatesList
            Core_GetStatesByID
            Core_GetDistrictsList
            Core_GetDistrictsByID
            Core_GetCitiesList
            Core_GetCitiesListByCountry
            Core_GetCitiesByID
            Core_GetCities
            Core_GetLedgerDetails
            Core_GetItemTDSCode
            Core_GetItemDocuments

            Data_GetOpeningBalance
            Data_GetOpeningBalance_Common
            Data_GetOpeningBalanceRecIDWithCustomColHeadName
            Data_GetOpeningBalanceRowCount
            Data_GetCashOpeningBalanceAmount
            Data_GetCashBankTransSumAmount
            Data_GetCashTransSumAmount
            Data_GetBankTransSumAmount_Common
            Data_GetCashBankTransSumAmountBetweenTwoDates
            Data_GetBankBalanceAmount
            Data_GetBankBalanceAmountIdWise
            Data_AddOpeningBalance
            Data_UpdateOpeningBalance
            Data_DeleteOpeningBalance
            Data_GetPartyIDList
            Data_GetUsedItemIDList
            Data_GetLastEditOn
            Data_GetCurrentDateTime
            Data_IsRecordCarriedForward
            Data_IsTBImportedCentre
            Data_IsMultiuserAllowed
            Data_IsInsuranceAuditor
            Data_IsInsuranceAudited
            Data_GetClientAuthorizations
            Data_GetPurpose
            Data_GetDynamicClientRestriction
            Data_GetAuditedPeriod
            Data_GetAccountsSubmittedPeriod

            'Data_Get_Asset_Item_Closing_Detail

            Deposits_GetList
            Deposits_GetProfileListing
            Deposits_GetListByCondition
            Deposits_GetPaymentsForParties
            Deposits_GetTransactionCount
            Deposits_Insert
            Deposits_InsertTRID
            Deposits_Update
            Deposits_GetList_Common
            Deposits_CheckIfDepositFinalPaymentMade

            Deposit_Slip_GetList
            Deposit_Slip_GetSlipReport
            Deposit_Slip_GetSlipReportAll
            Deposit_Slip_GetMaxOpenSlipNo
            Deposit_Slip_GetSlipTxnCount
            Deposit_Slip_MarkAsPrinted
            Deposit_Slip_MarkAsUnPrinted

            Donation_GetAddresses
            Donation_GetAddressDetail
            Donation_GetOfficeAddressDetail
            Donation_GetForeignDonationDetail
            Donation_GetAddressDetail_Form
            Donation_GetList
            Donation_GetRecDetail
            Donation_GetDonationStatus
            Donation_GetDonationPurposes
            Donation_GetDonationPrints
            Donation_GetDonationRejections
            Donation_GetDonationDispatches
            Donation_RequestReceipt
            Donation_CheckUsageAsPastDonor
            Donation_CheckUsageAsPastForeignDonor
            Donation_GetDonationReceiptDetails
            Donation_GenerateReceipt
            Donation_PrintReceipt
            Donation_GetReceiptDetails


            VoucherDonation_GetOldStatusID
            VoucherDonation_Insert
            VoucherDonation_InsertForeignInfo
            VoucherDonation_InsertPurpose
            VoucherDonation_InsertDonationStatus
            VoucherDonation_Update
            VoucherDonation_UpdatePurpose
            VoucherDonation_UpdateForeignInfo
            VoucherDonation_UpdateStatus
            VoucherDonation_InsertDonation_Txn
            VoucherDonation_UpdateDonation_Txn
            VoucherDonation_DeleteDonation_Txn

            FDs_GetExpense_IncomeCount
            FDs_GetTDS
            FDs_GetTDSReversals
            FDs_GetInterest
            FDs_GetInterestOverheads
            FDs_GetListByCondition
            FDs_GetList
            FD_GetProfileListing
            FDs_GetRecIDByAccID
            FDs_GetRecord
            FDs_Insert_and_Update_Balance
            FDs_Update_and_Update_Balance
            FDs_VoucherFD_GetPaymentRecords
            FDs_VoucherFD_GetInterestRecords
            FDs_VoucherFD_GetTDS
            FDs_VoucherFD_GetTDSReversal
            FDs_VoucherFD_GetFDCloseDate
            FDs_VoucherFD_GetFDCloseDateByFdID
            FDs_VoucherFD_GetNewFDIdFromClosed
            FDs_VoucherFD_GetCount
            FDs_VoucherFD_GetPrevFDStatus
            FDs_VoucherFD_GetFDs
            FDs_VoucherFD_GetItemCountInSameMaster
            FDs_VoucherFD_GetAccountNoCount
            FDs_VoucherFD_InsertFD
            FDs_VoucherFD_InsertFDHistory
            FDs_VoucherFD_InsertMasterInfo
            FDs_VoucherFD_Insert
            FDs_VoucherFD_UpdateFD
            FDs_VoucherFD_UpdateFDHistory
            FDs_VoucherFD_UpdateMasterInfo
            FDs_VoucherFD_InsertNewFD_Txn
            FDs_VoucherFD_UpdateNewFD_Txn
            FDs_VoucherFD_DeleteNewFD_Txn
            FDs_VoucherFD_InsertRenewFD_Txn
            FDs_VoucherFD_UpdateRenewFD_Txn
            FDs_VoucherFD_DeleteRenewFD_Txn
            FDs_VoucherFD_InsertCloseFD_Txn
            FDs_VoucherFD_UpdateCloseFD_Txn
            FDs_VoucherFD_DeleteCloseFD_Txn
            FDs_VoucherFD_InsertIncomeAndExpenses_Txn
            FDs_VoucherFD_UpdateIncomeAndExpenses_Txn
            FDs_VoucherFD_DeleteIncomeAndExpenses_Txn

            Gift_GetTxnItems
            Gift_Insert
            Gift_InsertMasterInfo
            Gift_InsertPurpose
            Gift_InsertItem
            Gift_UpdateMaster
            Gift_InsertGift_Txn
            Gift_UpdateGift_Txn
            Gift_DeleteGift_Txn

            GoldSilver_GetList
            GoldSilver_GetProfileListing
            GoldSilver_GetListByCondition
            GoldSilver_GetList_Common
            GoldSilver_GetList_Common_ByRecID
            GoldSilver_GetTransactions
            GoldSilver_UpdateAssetLocationIfNotPresent
            GoldSilver_Insert
            GoldSilver_InsertTRIDAndTRSRNo
            GoldSilver_Update
            InternalTransfer_GetList
            InternalTransfer_GetListWithTwoParams
            InternalTransfer_GetListWithMultipleParams
            InternalTransfer_GetUnMatchedList
            InternalTransfer_GetUnMatchedList_LimitedCount
            InternalTransfer_GetFrCenterList
            InternalTransfer_GetToCenterList
            InternalTransfer_GetCashTxnCount
            InternalTransfer_GetCashPendingTxnCount
            InternalTransfer_GetNonCashTxnCount
            InternalTransfer_GetNonCashPendingTxnCount
            InternalTransfer_GetUnmatchedEntriesCentreToCentreCount
            InternalTransfer_Get_Tf_Banks
            InternalTransfer_InsertMaster
            InternalTransfer_Insert
            InternalTransfer_InsertPurpose
            InternalTransfer_UpdateMaster
            InternalTransfer_Update
            InternalTransfer_Update_CrossReference
            InternalTransfer_MatchTransfers
            InternalTransfer_UnMatchTransfers
            InternalTransfer_UpdatePurpose
            InternalTransfer_Insert_InternalTransfer_Txn
            InternalTransfer_Update_InternalTransfer_Txn
            InternalTransfer_Delete_InternalTransfer_Txn

            AssetTransfer_GetMasterID
            AssetTransfer_GetTxnItems
            AssetTransfer_GetBankPayments
            AssetTransfer_GetTxnList_Sale
            AssetTransfer_GetTxnList_AssetTrf
            AssetTransfer_InsertMasterInfo
            AssetTransfer_Insert
            AssetTransfer_InsertItem
            AssetTransfer_InsertPurpose
            AssetTransfer_InsertPayment
            AssetTransfer_InsertBankPayment
            AssetTransfer_Insert_Profiles
            AssetTransfer_UpdateMaster
            AssetTransfer_Update_CrossReference
            AssetTransfer_Delete_CrossReference
            AssetTransfer_GetFrCenterList
            AssetTransfer_GetToCenterList
            AssetTransfer_GetUnMatchedList
            AssetTransfer_GetUnMatchedList_LimitedCount
            AssetTransfer_GetUnmatchedEntriesCount
            AssetTransfer_GetUnmatchedFromEntriesCount
            AssetTransfer_Insert_AssetTransfer_Txn
            AssetTransfer_Update_AssetTransfer_Txn
            AssetTransfer_Delete_AssetTransfer_Txn
            AssetTransfer_GetAssetTransfers
            AssetTransfer_Get_AssetTf_Asset_Listing

            Journal_InsertMaster
            Journal_InsertTxn
            Journal_InsertPurpose
            Journal_UpdateMaster
            Journal_GetDataRecords
            Journal_GetJornalEntryAdjustments
            Journal_GetCurrentRecAdjustment
            Journal_Get_JV_Asset_Listing
            Journal_InsertJournal_Txn
            Journal_UpdateJournal_Txn
            Journal_DeleteJournal_Txn

            Jobs_GetRegister
            Jobs_UpdateJobStatus
            Jobs_GetJob_UO_Count
            Jobs_GetJob_UO_Pending_Count
            Jobs_GetJob_RR_Count
            Jobs_GetJob_RR_Pending_Count
            Jobs_GetRecord
            Jobs_GetJob_Total_Usage_Count
            Jobs_GetJob_Project_Main_Assignee
            Jobs_GetJobItemEstimates
            Jobs_GetJobManpowerEstimates
            Jobs_GetJobManpowerUsage
            Jobs_GetJobMachineUsage
            Jobs_GetJobExpensesIncurred
            Jobs_Get_Job_Expenses_For_Mapping
            Jobs_Insert_Txn
            Jobs_Delete_Txn
            Jobs_Update
            Jobs_InsertJobManpowerUsage
            Jobs_InsertJobExpensesIncurred
            Jobs_InsertJobMachineUsage
            Jobs_GetList
            Jobs_InsertJobRemarks


            Letters_GetList
            Letters_Insert
            Letters_Update

            Liabilities_GetList
            Liabilities_GetProfileListing
            Liabilities_GetList_Common
            Liabilities_GetListByCondition
            Liabilities_GetPaymentsForParties
            Liabilities_GetTransactionCount
            Liabilities_GetPaymentLiabilities
            Liabilities_Insert
            Liabilities_InsertTRID
            Liabilities_Update

            Livestock_UpdateAssetLocationIfNotPresent
            Livestock_GetList
            Livestock_GetProfileListing
            Livestock_GetList_Common
            Livestock_GetList_Common_ByRecID
            Livestock_GetListByCondition
            Livestock_GetTransactions
            Livestock_Insert
            Livestock_InsertTRIDAndTRSrNo
            Livestock_Update
            Livestock_UpdateInsuranceDetail


            Magazine_GetList
            Magazine_GetList_SubcriptionType
            Magazine_GetList_SubcriptionTypeFee
            Magazine_GetSubcriptionTypeFee
            Magazine_GetList_DispatchType
            Magazine_GetList_DispatchTypeCharges
            Magazine_GetDispatchTypeCharges
            Magazine_GetList_GeetaPathshala
            Magazine_GetList_Centres
            Magazine_GetMembers
            Magazine_GetExistingMembers
            Magazine_GetList_Membership
            Magazine_GetList_MembershipRequests
            Magazine_GetList_ConnectedMembership
            Magazine_GetList_RelatedMembership
            Magazine_GetList_Address_Magazine
            Magazine_GetList_Dispatches
            Magazine_Get_Dispatch_Details
            Magazine_GetList_MagazineDispatchRegister
            Magazine_GetList_Issues
            Magazine_GetList_SubCities
            Magazine_GetMapping_SubCities
            Magazine_GetRecord_MembershipProfile
            Magazine_GetNewMembershipNo
            Magazine_GetList_ReceiptRegister
            Magazine_GetMagazineCountByName
            Magazine_GetMagazineCountByShortName
            Magazine_GetMagazineSubsTypeCountByName
            Magazine_GetMagazineSubsTypeCountByShortName
            Magazine_GetMagazineSubsFeeCountByEffDate
            Magazine_GetMagazineDispatchCountByName
            Magazine_GetMagazineDispatchFeeCountByEffDate
            Magazine_GetMembershipCountByMemberID
            Magazine_GetPayeeLedger
            Magazine_GetMagazineAccLedger
            Magazine_GetCancelledReceipts
            Magazine_GetDues
            Magazine_GetIncome_Breakup
            Magazine_GetParty_Balances
            Magazine_GetOpeningAccountingBalances_Bkp
            Magazine_Receivables_Ledger
            Magazine_Advances_Ledger

            Magazine_Insert
            Magazine_Update
            Magazine_Insert_Subs_Type
            Magazine_Update_Subs_Type
            Magazine_Insert_Subs_Type_Fee
            Magazine_Update_Subs_Type_Fee
            Magazine_Insert_Dispatch_Type
            Magazine_Update_Dispatch_Type
            Magazine_Insert_Dispatch_Type_Charges
            Magazine_Update_Dispatch_Type_Charges
            Magazine_Insert_Membership
            Magazine_Insert_Membership_Request
            Magazine_Insert_SubCity
            Magazine_Update_SubCity
            Magazine_Update_Membership
            Magazine_Update_Membership_Request
            Magazine_Update_Membership_Identity
            Magazine_Delete_SubCity
            Magazine_Delete_Dispatch_Type
            Magazine_Delete_Dispatch_Type_Charges
            Magazine_Delete_Magazine
            Magazine_Delete_Subscription_Type
            Magazine_Delete_Subscription_Type_Fee
            Magazine_Delete_Magazine_Membership_Profile
            Magazine_Delete_Membership_Request
            Magazine_Update_Request_Status

            Magazine_Insert_Dispatch
            Magazine_Update_Dispatch
            Magazine_Delete_Dispatch
            Magazine_Delete_Issues

            Magazine_Insert_Dispatch_bundles
            Magazine_Insert_Dispatch_New_Voucher

            Magazine_Subscription_Set_Default
            Magazine_Dispatch_Set_Default
            Magazine_Issue_Set_Default
            Magazine_Subscription_Remove_Default
            Magazine_Dispatch_Remove_Default
            Magazine_Issue_Remove_Default
            Magazine_Add_Client_Restrictions

            Magazine_Insert_Magazine_Issue
            Magazine_Update_Magazine_Issue
            Magazine_Insert_Magazine_Similar_Issues
            Magazine_Settle_Magazine_Ledgers

            Voucher_Magazine_Register_GetVoucherDetailsOnMemberSelection
            Voucher_Magazine_Register_GetMembers
            'Voucher_Magazine_Register_Insert
            'Voucher_Magazine_Register_InsertBalances
            'Voucher_Magazine_Register_InsertPayment
            'Voucher_Magazine_Register_InsertMaster
            'Voucher_Magazine_Register_InsertPurpose
            Voucher_Magazine_Register_InsertMagazine_Txn
            Voucher_Magazine_Register_InsertReceipt
            Voucher_Magazine_Register_GetDiscontinued
            Voucher_Magazine_Register_GetReceiptCount
            Voucher_Magazine_Register_DeleteReceipt
            Voucher_Magazine_Update_Magazine_Disp_CC

            Magazine_Membership_Close
            Magazine_Membership_Reopen
            Magazine_GetLastEntryDate
            Magazine_GetIssueCount
            Magazine_GetRestriction_Date
            Magazine_Consider_ForAutoRenewal
            Magazine_GetMagazinesIssues

            Membership_GetWings
            Membership_GetSubscriptionList
            Membership_GetSubscriptionFee
            Membership_GetDuplicateCount
            Membership_GetCountForContinue
            Membership_GetDuplicateOldNoCount
            Membership_GetMembershipNo
            Membership_GetNewMembershipNo
            Membership_GetLastPeriod
            Membership_GetLastEntryDate
            Membership_GetLastTransactionDate
            Membership_GetMasterTransactionList
            Membership_GetDiscontinued
            Membership_GetList
            Membership_GetBalancesList
            Membership_GetCountForWing2WingConversion
            Membership_GetDiscontinued_Date
            Membership_GetSubscritonList_Master
            Memebership_GetSubscritonFeeList_Master
            Membership_Insert
            Membership_InsertBalances
            Membership_InsertSubscriptionList
            Membership_InsertSubscriptionFee
            Membership_Update
            Membership_Close
            Membership_Reopen
            Membership_CheckUsageAsPastMember
            Membership_InsertMembership_Txn
            Membership_UpdateMembership_Txn
            Membership_UpdateSubscriptionList
            Membership_UpdateSubscriptionFee
            Membership_DeleteMembership_Txn
            Membership_DeleteSubscriptionList
            Membership_DeleteSubscriptionFee

            MembershipReceiptRegister_GetMaxTransactionDate
            MembershipReceiptRegister_GetList
            MembershipReceiptRegister_GetReceiptCount
            MembershipReceiptRegister_InsertReceipt
            MembershipReceiptRegister_DeleteReceipt
            MembershipReceiptRegister_DeleteReceiptRef


            OpeningBalances_GetList
            OpeningBalances_GetDuplicateCount
            OpeningBalances_Insert
            OpeningBalances_Update

            Report_UpdateClearingDate

            VoucherMembership_GetMasterID
            VoucherMembership_GetTxnBankPaymentDetail
            VoucherMembership_GetLastPeriod
            VoucherMembership_InsertMasterInfo
            VoucherMembership_Insert
            VoucherMembership_InsertItem
            VoucherMembership_InsertPayment
            VoucherMembership_InsertPurpose
            VoucherMembership_UpdateMaster
            VoucherMembership_Update
            VoucherMembership_UpdatePurpose
            VoucherMembership_InsertMembershipVoucher_Txn
            VoucherMembership_UpdateMembershipVoucher_Txn
            VoucherMembership_DeleteMembershipVoucher_Txn

            VoucherMembershipRenewal_GetMasterID
            VoucherMembershipRenewal_GetTxnBankPaymentDetail
            VoucherMembershipRenewal_GetPartyDetails
            VoucherMembershipConversion_GetPartyDetails
            VoucherMembershipRenewal_GetLastPeriod
            VoucherMembershipRenewal_InsertMasterInfo
            VoucherMembershipRenewal_Insert
            VoucherMembershipRenewal_InsertItem
            VoucherMembershipRenewal_InsertPayment
            VoucherMembershipRenewal_InsertPurpose
            VoucherMembershipRenewal_UpdateMaster
            VoucherMembershipRenewal_Update
            VoucherMembershipRenewal_UpdatePurpose
            VoucherMembershipRenewal_InsertMemRenewalVoucher_Txn
            VoucherMembershipRenewal_UpdateMemRenewalVoucher_Txn
            VoucherMembershipRenewal_DeleteMemRenewalVoucher_Txn

            VoucherMembershipConversion_InsertMembershipVoucherConversion_Txn
            VoucherMembershipConversion_UpdateMembershipVoucherConversion_Txn
            VoucherMembershipConversion_DeleteMembershipVoucherConversion_Txn

            Murli_GetList

            News_GetList
            News_GetCount
            News_GetCenterNews

            Notebook_GetMaxTransactionDate
            Notebook_GetList
            Notebook_Insert
            Notebook_InsertAllEntries
            Notebook_Update

            Notes_GetList
            Notes_GetShortList
            Notes_AddQuickNote
            Notes_Insert
            Notes_Complete
            Notes_Incomplete
            Notes_Update

            Payments_GetTxnDetailsByRefID
            Payments_GetTxnItemsDetail
            Payments_GetTxnBankPaymentDetail
            Payments_GetTxnPaymentDetail
            Payments_GetAdvancesPaid
            Payments_GetLiabilitiesPaid
            Payments_InsertMasterInfo
            Payments_Insert
            Payments_InsertItem
            Payments_InsertPayment
            Payments_InsertPaymentMT
            Payments_InsertPurpose
            Payments_InsertOther
            Payments_UpdateMaster
            Payments_InsertPayment_Txn
            Payments_UpdatePayment_Txn
            Payments_DeletePayment_Txn
            Payments_GetPendingTDSList
            Payments_GetTDS_Deducted_Not_Sent
            Payments_GetTDS_Deducted_Not_Paid
            Payments_Get_CreatedAssets_MinTxnDate
            Payments_Get_Inv_No_Count

            Personnels_GetRequestorList
            Personnels_GetPersonnelCharges
            Personnels_GetRegister
            Personnels_GetPersonnelRecord
            Personnels_GetPersonnelChargesRecord
            Personnels_Get_StockPersonnel_Usage_Count
            Personnels_Get_PersonnelCharges_UsageCount
            Personnels_Mark_Personnel_asLeft
            Personnels_Mark_Personnel_asReopen
            Personnels_InsertPersonnel
            Personnels_InsertPersonnelCharges
            Personnels_UpdatePersonnel
            Personnels_UpdatePersonnelCharges
            Personnels_Get_Personnel_Count
            Personnels_Get_PFNo_Count
            Personnels_Get_Personnel_Usage_Period

            Projects_GetList
            Projects_GetOpenProjectList
            Projects_GetRegister
            Projects_UpdateProjectStatus
            Projects_GetProject_Open_Jobs_Count
            Projects_GetProject_Jobs_Count
            ' Projects_GetStockMainDept
            ' Projects_GetStockSubDept
            Projects_GetStockPersonnel
            Projects_GetStockDocuments
            Projects_GetStockRemarks
            Projects_GetProjectEstimates
            Projects_GetRecord
            Projects_GetProjCnt_ForGivenSanctionNo_CurrInstt
            Projects_Insert
            Projects_Delete
            Projects_Update
            Projects_InsertProjectRemarks

            Property_GetList
            Property_GetProfileListing
            Property_Get_Property_Closing
            Property_GetAllPropertyList
            Property_GetListForExpenses
            Property_GetListForMOU
            Property_GetExtensionDetails
            Property_GetDocsDetails
            Property_GetListByCondition
            Property_GetTransactions
            Property_GetCenterCountByName
            Property_GetPropertyByName
            Property_GetMainCenters
            Property_GetTransactionCount
            Property_GetIDsBytxnID
            Property_Insert
            Property_InsertMasterIdSrNo
            Property_InsertExtendedInfo
            Property_InsertDocumentsInfo
            Property_Update
            Property_CheckDuplicatePropertyName
            Property_GetListByQuery_Common
            Property_InsertProperty_Txn
            Property_UpdateProperty_Txn
            Property_DeleteProperty_Txn
            Property_GetRecord
            Property_GetPendingTfs_LocNames
            Property_Get_PropertyListingBySP
            Property_Get_Location_Property_ListingBySP
            Property_UpdateRentDetails_Txn

            Receipts_GetMasterID
            Receipts_GetTxnItems
            Receipts_GetBankPayments
            Receipts_GetAdvances
            Receipts_GetAdvancesRefundList
            Receipts_GetDeposits
            Receipts_GetDepositsRefundList
            Receipts_GetLiabilities
            Receipts_InsertMasterInfo
            Receipts_Insert
            Receipts_InsertItem
            Receipts_InsertPurpose
            Receipts_InsertPayment
            Receipts_InsertBankPayment
            Receipts_UpdateMaster
            Receipts_InsertReceipt_Txn
            Receipts_UpdateReceipt_Txn
            Receipts_DeleteReceipt_Txn


            ReportsAll_GetConstructionWIPExpensesList
            ReportsAll_GetTransactionList
            ReportsAll_GetDonationStatusID
            ReportsAll_GetGiftTransactionList
            ReportsAll_GetCollectionBoxTransactionList
            ReportsAll_GetCollectionBoxTransactionListWithRecID
            ReportsAll_GetAddresses
            ReportsAll_GetTSummaryList
            ReportsAll_GetCashBankTransSum
            ReportsAll_GetMembershipReceipt
            ReportsAll_GetMagazineReceipt
            ReportsAll_GetMembershipSubscriptionFee
            ReportsAll_GetMembershipReceiptPayment
            ReportsAll_GetTrialBalance
            ReportsAll_GetTrialBalanceRaport
            ReportsAll_GetIncomeExpenditureReport
            ReportsAll_GetBalanceSheetReport
            ReportsAll_GetTelephoneBillDetails
            ReportsAll_ShowInsuranceLetter
            ReportsAll_GetConstructionList
            Reports_GetLedgerReport
            Reports_GetTransactionDetailsForLedger
            Reports_GetLedgerOpeningBalance
            Reports_GetItemReport
            Reports_GetItemOpeningBalance
            Reports_GetSecondaryGroupReport
            Reports_GetSecondaryGroupOpeningBalance
            Reports_GetPrimaryGroupReport
            Reports_GetPrimaryGroupOpeningBalance
            Reports_GetPartyListing
            Reports_GetPartyReport
            Reports_GetVehicleReportData
            Reports_GetAssetReportData
            Reports_GetGSReportData
            Reports_GetFDReportData
            Reports_GetLBReportData
            Reports_GetInsurancePropertyList
            Reports_Purpose
            Report_GetInsurancePolicyDetails
            Report_GetInsuranceLetterData
            Report_GetInsuredYears
            Reports_GetAssetInsuranceBreakUpReport
            Reports_GetConsumableBreakUpReport
            Reports_GetWIPReport
            Reports_Update_Insurance_Register
            Reports_GetBankAccountsList
            Reports_GetMagSubDispatchReport
            Reports_VoucherReference

            Request_GetList
            Request_GetUnreadCount
            Request_MarkAsRead
            Request_MarkAsUnread
            Request_Insert
            Request_Update

            SaleOfAssets_GetMasterID
            SaleOfAssets_GetTxnItems
            SaleOfAssets_GetBankPayments
            SaleOfAssets_GetTxnList
            SaleOfAssets_AssetsListingForSale
            SaleOfAssets_MaxTxnDate
            SaleOfAssets_EnclosingTxnDate
            SaleOfAssets_InsertMasterInfo
            SaleOfAssets_Insert
            SaleOfAssets_InsertItem
            SaleOfAssets_InsertPurpose
            SaleOfAssets_InsertPayment
            SaleOfAssets_InsertBankPayment
            SaleOfAssets_UpdateMaster
            SaleOfAssets_InsertSaleOfAsset_Txn
            SaleOfAssets_UpdateSaleOfAsset_Txn
            SaleOfAssets_DeleteSaleOfAssets_Txn

            ServicePlaces_GetList
            ServicePlaces_GetAllServicePlaceList
            ServicePlaces_GetCountByPlaceName
            ServicePlaces_Insert
            ServicePlaces_Update
            ServicePlaces_GetRecord
            ServicePlaces_InsertServicePlaces_Txn
            ServicePlaces_UpdateServicePlaces_Txn
            ServicePlaces_DeleteServicePlaces_Txn

            ServiceReport_GetList
            ServiceReport_GetWingsRecord
            ServiceReport_Insert
            ServiceReport_InsertWings
            ServiceReport_InsertGuest
            ServiceReport_Update
            ServiceReport_InsertServiceReport_Txn
            ServiceReport_UpdateServiceReport_Txn
            ServiceReport_DeleteServiceReport_Txn

            ServiceMaterial_Insert
            ServiceMaterial_GetWingsRecord
            ServiceMaterial_InsertServiceMaterial_Txn
            ServiceMaterial_UpdateServiceMaterial_Txn
            ServiceMaterial_DeleteServiceMaterial_Txn

            StockProfile_AddStockProfile
            StockProfile_DeleteStockProfile
            StockDeptStores_GetStoreList
            StockDeptStores_GetStoreList_SA
            StockDeptStores_GetRegister
            StockDeptStores_GetStoreUsageCount
            StockDeptStores_GetStorePremesis
            StockDeptStores_Get_StoreDept_Detail
            StockDeptStores_Get_MainSubDept_Personnels
            StockDeptStores_GetStoreNoUsageCountInstt
            StockDeptStores_GetStockCountForLocation
            StockDeptStores_InsertStoreDept
            StockDeptStores_updateStoreDept
            StockDeptStores_CloseStoreDept
            StockDeptStores_ReopenStoreDept
            StockDeptStores_Get_CentralStorespecific_usage_count
            StockDeptStores_DeleteStoreDept
            StockDeptStores_GetStoreDept
            StockDeptStores_GetStoresForPersonnel

            StockProfile_GetProfiledata
            StockProfile_GetStockDuplication
            StockProfile_GetStockItems
            StockProfile_GetFilteredStockItems
            'StockProfile_GetUnits
            'StockProfile_IsStockCarriedForward
            StockProfile_UpdateStockProfile
            StockProfile_UpdateStockProject
            StockProfile_UpdateStockLocation
            StockProfile_Get_Stocks_Listing
            StockProfile_GetStockUsage
            StockProfile_AddStockAddition
            StockProduction_GetProductions
            StockProduction_GetRegister
            StockProduction_GetUsedLeftManpowerCount
            StockProduction_GetProdItemsConsumed
            StockProduction_GetProdExpensesIncurred
            StockProduction_GetProdManpowerUsage
            StockProduction_GetProdMachineUsage
            StockProduction_GetProdItemProduced
            StockProduction_GetProdScrapProduced
            StockProduction_Get_Prod_Expenses_For_Mapping
            StockProduction_GetProdDetails
            StockProduction_InsertProduction_Txn
            StockProduction_UpdateProduction_Txn
            StockProduction_DeleteProduction_Txn

            StockMachineToolAllocation_GetRegister
            StockMachineToolAllocation_GetRecord
            StockMachineToolAllocation_GetPendingReturns
            StockMachineToolAllocation_GetMachineToolIssueCount
            StockMachineToolAllocation_InsertMachineToolIssue
            StockMachineToolAllocation_InsertMachineToolReturn
            StockMachineToolAllocation_UpdateMachineToolIssue
            StockMachineToolAllocation_UpdateMachineToolReturn
            StockMachineToolAllocation_DeleteMachineToolIssue
            StockMachineToolAllocation_DeleteMachineToolReturn

            StockTransferOrders_GetTransferOrders

            StockPurchaseOrder_GetRegister
            StockPurchaseOrder_GetPOItemsOrdered
            StockPurchaseOrder_GetPOLinkedUserOrders
            StockPurchaseOrder_GetPOLinkedRequisitions
            StockPurchaseOrder_GetPOPayments
            StockPurchaseOrder_GetPOGoodsReceived
            StockPurchaseOrder_GetPOGoodsReturned
            StockPurchaseOrder_Get_PO_Detail
            StockPurchaseOrder_GetItemsPriceHistory
            StockPurchaseOrder_Get_Dept_for_POItem_DestLoc
            StockPurchaseOrder_GetPOPaymentsForMapping
            StockPurchaseOrder_Get_PriceHistory
            StockPurchaseOrder_UpdatePurchaseOrder_Txn
            StockPurchaseOrder_DeletePurchaseOrder_Txn
            StockPurchaseOrder_UpdatePurchaseOrderStatus
            StockPurchaseOrder_InsertPurchaseOrderGoodsReceived
            StockPurchaseOrder_InsertPurchaseOrderGoodsReturned
            StockPurchaseOrder_InsertPurchaseOrderPayment
            StockPurchaseOrder_Get_PO_Latest_RR_ID
            StockPurchaseOrder_Get_PO_Latest_UO_ID
            StockPurchaseOrder_Get_PO_Job_Project_Completed
            StockPurchaseOrder_Get_PO_Non_Rate_Items
            StockPurchaseOrder_Get_PO_Pending_Due
            StockPurchaseOrder_Get_Stock_Current_Quantity_Count
            StockPurchaseOrder_Get_PO_Related_ClosedDept_Count
            StockPurchaseOrder_Get_PO_Duplicate_LotNo_Count
            StockPurchaseOrder_GetPOItem_Received_EntryCount
            StockPurchaseOrder_GetPOReceipt_Return_EntryCount
            StockPurchaseOrder_GetSupplier_PriceHistory
            StockPurchaseOrder_Get_PO_Tax_Detail
            StockRequisitionRequest_GetRegister
            StockRequisitionRequest_Get_RR_Detail
            StockRequisitionRequest_Get_RR_Items
            StockRequisitionRequest_Insert_RR
            StockRequisitionRequest_Update_RR
            StockRequisitionRequest_Delete_RR
            StockRequisitionRequest_Get_RR_Usage_Count
            StockRequisitionRequest_Get_PO_Incomplete_Count
            StockRequisitionRequest_Update_RR_Status
            StockRequisitionRequest_Get_RR_Linked_UO
            StockRequisitionRequest_Get_RR_Tax_Detail
            StockApprovalRequired_Get_Approval_Required

            SubItem_GetList
            SubItem_GetUsageList
            SubItem_GetMainCategoriesMaster
            SubItem_GetSubCategoriesMaster
            SubItem_GetItemPropertiesMaster
            SubItem_GetPropertiesList_SubItem
            SubItem_GetUnitConversionList_SubItem
            SubItem_InsertSubItem_Txn
            SubItem_InsertItemProperties
            SubItem_InsertItemUnitconversion
            SubItem_UpdateSubItem_Txn
            SubItem_DeleteSubItem_Txn
            SubItem_Reopen
            SubItem_Close
            SubItem_UpdateSubItem_Store_Mapping
            SubItem_GetRecord
            SubItem_GetStoreItems

            Suppliers_GetRegister
            Suppliers_GetSupplierBanks
            Suppliers_GetAllSuppliers
            Suppliers_GetItemMappedSuppliers

            Suppliers_GetItemSupplierMapping
            Suppliers_InsertItemSupplierMapping
            Suppliers_UpdateItemSupplierMapping
            Suppliers_InsertSupplierBank
            Suppliers_InsertSupplier_Txn
            Suppliers_UpdateSupplier_Txn
            Suppliers_DeleteSupplier_Txn
            Suppliers_GetSupplierRecord
            Suppliers_GetSupplierUsage
            Suppliers_GetSupplierBankAccUsageCount
            Suppliers_GetSupplier_Party_Duplication_Check

            Telephone_GetList
            Telephone_GetListByCondition
            Telephone_GetRecordByTeleNumber
            Telephone_Insert
            Telephone_Update
            'Telephone_UpdateAssetLocationIfNotPresent
            Telephone_GetCountInTxn
            Telephone_GetLastEntryDate
            Telephone_Close

            TDS_GetTDSRegister
            TDS_InsertTDSDeduction
            TDS_GetTDS_Deducted_Not_Sent
            TDS_GetTDS_Deducted_Not_Paid

            UserOrder_GetDeliveries
            UserOrder_GetRegister
            UserOrder_UpdateUOStatus
            UserOrder_GetUO_RR_Count
            UserOrder_GetRecord
            UserOrder_GetUO_DependentEntry_Count
            UserOrder_GetUO_Job_Status
            UserOrder_GetUO_RR_Status
            UserOrder_GetUO_Deliveries_notReturned_Count
            UserOrder_GetUO_Goods_In_Transit
            UserOrder_Get_RR_Details_ForUOmapping
            UserOrder_Get_UO_Items_Detail_For_RR
            UserOrder_Update_Scheduled_Delivery
            UserOrder_GetUO_Scrap_Creation_Count
            UserOrder_GetUOItems
            UserOrder_GetUODetails
            UserOrder_GetUOGoodsReceived
            UserOrder_GetUOGoodsReturned
            UserOrder_GetUORetReceivableItems
            UserOrder_GetUOScrapCreated
            UserOrder_GetUOGoodsDelivered
            UserOrder_GetUOGoodsReturnReceived
            UserOrder_GetUOReqItem_Delivered_EntryCount
            UserOrder_GetUODelivery_Received_EntryCount
            UserOrder_GetUOReceipt_Return_EntryCount
            UserOrder_GetUOReturn_Received_EntryCount
            UserOrder_Insert_UO
            UserOrder_Insert_UO_Item
            UserOrder_Insert_UO_Item_Delivered
            'UserOrder_Insert_UO_Item_Delivered_Stocks
            UserOrder_Insert_UO_Item_Received
            UserOrder_Insert_UO_Item_Returned
            UserOrder_Insert_UO_Item_Return_Received
            UserOrder_InsertUORemarks
            UserOrder_InsertUORRMapping
            UserOrder_Update_UO
            UserOrder_Delete_UO
            UserOrder_UORRUnmapping
            UserOrder_Get_CenterDetails_StockAvailability
            UserOrder_Get_Store_Locations_StockAvailability
            UserOrder_GetUOGoodsDeliverAllPending
            UserOrder_GetUOGoodsDeliverSelectedItems
            UserOrder_GetUOGoodsReceiveAllPending
            UserOrder_GetUOGoodsReceiveSelectedItems
            UserOrder_GetUOGoodsReturnAllPending
            UserOrder_Get_UO_Goods_Delivery_Stocks
            UserOrder_Get_UODeliveredItems
            UserOrder_InsertUOGoodsDeliverAllPending
            UserOrder_InsertUOGoodsReceiveAllPending
            UserOrder_InsertUOGoodsReturnAllPending
            UserOrder_Get_Stock_Availability

            Vehicles_GetList
            Vehicles_GetProfileListing
            Vehicles_GetListByCondition
            Vehicles_GetList_Common
            Vehicles_GetList_Common_ByRecID
            Vehicles_Insert
            Vehicles_InsertTRIDAndTRSrNo
            Vehicles_Update
            Vehicles_UpdateInsuranceDetail
            Vehicles_UpdateAssetLocationIfNotPresent
            Vehicles_GetRecord

            Vouchers_GetMaxTransactionDate
            Vouchers_GetCashBalanceSummary
            Vouchers_GetBankBalanceSummary
            Vouchers_GetReferenceTxnRecord
            Vouchers_GetReferenceRecordID
            Vouchers_GetSaleReferenceRecord
            Vouchers_GetPaymentReferenceRecord
            Vouchers_GetAssetItemID
            Vouchers_GetAssetRecID
            Vouchers_GetEditOnByRecID
            Vouchers_GetPastParties
            Vouchers_GetListWithMultipleParams
            Vouchers_GetListWithMultipleParams_SP
            Vouchers_GetList
            Vouchers_GetNegativeBalance
            Vouchers_GetDonationStatus
            Vouchers_GetDonationStatusWithRecID
            Vouchers_GetStatus_TrCode
            Vouchers_GetStatus_TrCode_OtherCentre
            Vouchers_GetTransactionDetail
            Vouchers_GetAdjustments
            Vouchers_GetReferenceTxnRecord_ExcludeM_ID
            Vouchers_GetRefRecordIDS
            Vouchers_GetAdvancedFilters
            Vouchers_GetBank_Reconciliation
            Vouchers_GetBankClearingData
            Vouchers_GetBankEntriesCountInNextEvent
            Vouchers_GetDraftEntryList
            Vouchers_GetDraftEntryCount
            Vouchers_ConfirmDraftEntry
            Vouchers_DeleteDraftEntry
            Vouchers_RejectDraftEntry
            Vouchers_GetTDS_Mapping
            Vouchers_GetEntryDetails
            Vouchers_GetProfileEntryDetails
            Vouchers_GetVoucherItemDetails
            Vouchers_UpdateCommonDetails
            Vouchers_DeleteAllVouchingAgainstReference
            Vouchers_UnVouchEntryByReference

            Voucher_WIP_Finalization_GetListOfReferences
            Voucher_WIP_Finalization_GetListOfWIPLedgers
            Voucher_WIP_Finalization_GetListOfFinalizedAssets
            Voucher_WIP_Finalization_GetExistingAssetListing
            Voucher_WIP_Finalization_Get_WIP_Outstanding_References
            Voucher_WIP_Finalization_GetFinalizedAmounts
            GetTxn_Report
            Voucher_WIP_Finalization_InsertMasterInfo
            Voucher_WIP_Finalization_Insert
            Voucher_WIP_Finalization_Insert_WIP_Finalization_Txn
            Voucher_WIP_Finalization_Update_WIP_Finalization_Txn
            Voucher_WIP_Finalization_Delete_WIP_Finalization_Txn


            WIP_Creation_Vouchers_GetDuplicateReferenceCount
            WIP_Creation_Vouchers_GetCountOfReferencesByLedID
            WIP_Creation_Vouchers_GetWIP_Dependencies
            WIP_Creation_Vouchers_GetWIP_GetRefCreationDateByWIPID

            WIP_Profile_GetProfileListing
            WIP_Profile_GetList_Common
            WIP_Profile_InsertTRIDAndTRSrNo
            WIP_Profile_Insert
            WIP_Profile_GetTxn_Report
            WIP_Profile_Update
            Options_DataRestrictions_GetList
            Options_DataRestrictions_GetRecord
            Options_AddDataRestriction
            Options_UpdateDataRestriction
            Options_DeleteDataRestriction

            ServiceModule_GetCenters
            ServiceModule_Update
            AddressBookRelated_Excel_Insert_Query
            Addresses_InsertUserProfile
        End Enum

        <WebMethod()>
        Public Function SessionInProgressError(ByVal PCID As String) As String
            If PCID = "MAINTAIN" Then
                Return "|Sorry! Your Session has been terminated due to some maintenance activity in Madhuban...|"
            ElseIf PCID = "UNDER AUDIT" Then
                Return "|Sorry! Your Session has been terminated due to start of some audit activity in Madhuban...|"
            ElseIf PCID = "AUDIT CLOSED" Then
                Return "|Sorry! Your Session has been terminated due to termination of present audit activity by administrators...|"
            Else
                Return "|Sorry! Already Same User Logged In from Some Other PC(ID:" & PCID.ToString() & ")...|"
            End If

        End Function

        ''' <summary>
        ''' Returns Text Equalent for Enum AssetProfiles Selection
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetAssetProfileFromEnum(InParam As AssetProfiles) As String
            Select Case InParam
                Case AssetProfiles.GOLD
                    Return "GOLD"
                Case AssetProfiles.SILVER
                    Return "SILVER"
                Case AssetProfiles.VEHICLES
                    Return "VEHICLES"
                Case AssetProfiles.OTHER_ASSETS
                    Return "OTHER ASSETS"
                Case AssetProfiles.LIVESTOCK
                    Return "LIVESTOCK"
                Case AssetProfiles.LAND_BUILDING
                    Return "LAND & BUILDING"
                Case AssetProfiles.ADVANCES
                    Return "ADVANCES"
                Case AssetProfiles.OTHER_DEPOSITS
                    Return "OTHER DEPOSITS"
                Case AssetProfiles.OTHER_LIABILITIES
                    Return "OTHER LIABILITIES"
                Case AssetProfiles.OTHER_OPENING_BALANCES
                    Return "OPENING"
                Case AssetProfiles.WIP
                    Return "WIP"
                Case AssetProfiles.FD
                    Return "FD"
                Case Else
                    Return Nothing
            End Select
        End Function
#End Region

#Region "--Public DBService Functions--"
        <WebMethod(MessageName:="Get List Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function List(ByVal tablename As Tables, ByVal query As String, ByVal retTableName As String, inBasicParam As Basic_Param) As DataTable
            Dim Condition As [String] = ""
            If query.ToLower().Contains("where") Then
                Condition = query.ToLower().Split(New String() {"where"}, StringSplitOptions.None)(1)
            End If
            CheckAuthorization(Permissions.Listing, "Listing", Condition, Nothing, tablename, inBasicParam)
            Dim dtResult As New DataTable()
            If Not query.ToLower().Contains(tablename.ToString().ToLower()) And Not tablename.ToString().ToLower().Contains("xls") Then
                Throw New Exception(Common.TableError(tablename))
            End If
            ' To ensure that user is calling the same table as it has rights and is being verified for 
            'TODO: create another check to verify that the table is same as declared in tablename
            query = query.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim dsResult As New DataSet()
            If Common.UseSQL Then
                Dim con As New SqlConnection(Common.ConnectionString)
                Dim dAp As SqlDataAdapter
                Using con
                    con.Open()
                    dAp = New SqlDataAdapter(query, con)
                    dAp.SelectCommand.CommandTimeout = 0
                    dAp.Fill(dsResult, retTableName)
                    dtResult = dsResult.Tables(retTableName)
                End Using
            Else
            End If

            Return dtResult
        End Function

        <WebMethod(MessageName:="Get List From SP Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function ListFromSP(ByVal tablename As Tables, ByVal SPName As String, ByVal retTableName As String,
    ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param) As DataTable
            Dim Condition As [String] = ""
            'If SPName.ToLower().Contains("where") Then
            'Condition = SPName.ToLower().Split(New String() {"where"}, StringSplitOptions.None)(1)
            'End If
            CheckAuthorization(Permissions.Listing, "Listing", Condition, Nothing, tablename, inBasicParam)
            Dim dtResult As New DataTable()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim con As New SqlConnection(Common.ConnectionString)
            Dim dAp As SqlDataAdapter
            Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
            Dim cParam As New SqlParameter()
            For index As Int16 = 0 To parameters.Length - 1
                cParam = New SqlParameter()
                cParam.DbType = types(index)
                cParam.ParameterName = parameters(index)
                If cParam.DbType = DbType.[String] Then
                    cParam.Size = length(index)
                End If
                If Not values(index) Is Nothing Then
                    cParam.Value = values(index)
                Else
                    cParam.Value = DBNull.Value
                End If
                oParams(index) = cParam
            Next
            Using con
                con.Open()
                dAp = New SqlDataAdapter(SPName, con)
                dAp.SelectCommand.CommandType = CommandType.StoredProcedure
                dAp.SelectCommand.CommandTimeout = 0
                For Each currParam As SqlParameter In oParams
                    dAp.SelectCommand.Parameters.Add(currParam)
                Next
                dAp.Fill(dtResult)
                dtResult.TableName = tablename.ToString
            End Using

            Return dtResult
        End Function

        <WebMethod(MessageName:="Get Scalar From SP Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function ScalarFromSP(ByVal tablename As Tables, ByVal SPName As String,
    ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param) As Object
            Dim Condition As [String] = ""
            CheckAuthorization(Permissions.Listing, "Listing", Condition, Nothing, tablename, inBasicParam)
            Dim dtResult As New DataTable()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim con As New SqlConnection(Common.ConnectionString)
            Dim dAp As SqlDataAdapter
            Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
            Dim cParam As New SqlParameter()
            For index As Int16 = 0 To parameters.Length - 1
                cParam = New SqlParameter()
                cParam.DbType = types(index)
                cParam.ParameterName = parameters(index)
                If cParam.DbType = DbType.[String] Then
                    cParam.Size = length(index)
                End If
                If Not values(index) Is Nothing Then
                    cParam.Value = values(index)
                Else
                    cParam.Value = DBNull.Value
                End If
                oParams(index) = cParam
            Next
            Using con
                con.Open()
                dAp = New SqlDataAdapter(SPName, con)
                dAp.SelectCommand.CommandType = CommandType.StoredProcedure
                dAp.SelectCommand.CommandTimeout = 0
                For Each currParam As SqlParameter In oParams
                    dAp.SelectCommand.Parameters.Add(currParam)
                Next
                dAp.Fill(dtResult)
            End Using

            Return dtResult.Rows(0)(0)
        End Function

        <WebMethod(MessageName:="Insert By SP Function")>
        Public Function InsertBySPPublic(ByVal tablename As Tables, ByVal SPName As String, ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param) As Object
            Return InsertBySP(tablename, SPName, parameters, values, types, length, inBasicParam)
        End Function

        Public Function InsertBySP(ByVal tablename As Tables, ByVal SPName As String,
    ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param, Optional Txn_Date As Date = Nothing, Optional Ret_Value_Param As String = Nothing) As Object
            Dim Ret_Value As Object = Nothing
            Dim Condition As [String] = ""
            CheckAuthorization(Permissions.Add, "Add", Condition, Nothing, tablename, inBasicParam, Txn_Date)
            Dim dtResult As New DataTable()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim con As SqlConnection = Nothing
            OpenSqlConnection(con)
            Try
                Dim command As SqlCommand = con.CreateCommand()
                command.CommandTimeout = 0
                command.CommandText = SPName
                command.CommandType = CommandType.StoredProcedure
                Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
                Dim cParam As New SqlParameter()
                For index As Int16 = 0 To parameters.Length - 1
                    cParam = New SqlParameter()
                    cParam.DbType = types(index)
                    cParam.ParameterName = parameters(index)
                    If cParam.DbType = DbType.[String] Then
                        cParam.Size = length(index)
                    End If
                    If Not values(index) Is Nothing Then
                        cParam.Value = values(index)
                    Else
                        cParam.Value = DBNull.Value
                    End If
                    '  oParams(index) = cParam
                    command.Parameters.Add(cParam)
                Next
                Dim returnParameter As SqlParameter = command.Parameters.Add("@RETURN_VALUE", SqlDbType.Int)
                returnParameter.Direction = ParameterDirection.ReturnValue

                If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                    command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                End If
                command.ExecuteNonQuery()
                Close_NonTransaction_SqlConnection(con)
                Return returnParameter.Value
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Function

        <WebMethod(MessageName:="Update By SP Function")>
        Public Function UpdateBySPPublic(ByVal tablename As Tables, ByVal SPName As String, ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param) As Object
            UpdateBySP(tablename, SPName, parameters, values, types, length, inBasicParam)
            Return True
        End Function
        Public Sub UpdateBySP(ByVal tablename As Tables, ByVal SPName As String,
   ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param, Optional Txn_Date As Date = Nothing)
            Dim Condition As [String] = ""
            CheckAuthorization(Permissions.Edit, "Edit", Condition, Nothing, tablename, inBasicParam, Txn_Date)
            Dim dtResult As New DataTable()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim con As SqlConnection = Nothing
            OpenSqlConnection(con)
            Try
                Dim command As SqlCommand = con.CreateCommand()
                command.CommandTimeout = 0
                command.CommandText = SPName
                command.CommandType = CommandType.StoredProcedure
                Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
                Dim cParam As New SqlParameter()
                For index As Int16 = 0 To parameters.Length - 1
                    cParam = New SqlParameter()
                    cParam.DbType = types(index)
                    cParam.ParameterName = parameters(index)
                    If cParam.DbType = DbType.[String] Then
                        cParam.Size = length(index)
                    End If
                    If Not values(index) Is Nothing Then
                        cParam.Value = values(index)
                    Else
                        cParam.Value = DBNull.Value
                    End If
                    '  oParams(index) = cParam
                    command.Parameters.Add(cParam)
                Next
                If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                    command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                End If
                command.ExecuteNonQuery()
                Close_NonTransaction_SqlConnection(con)
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Sub

        Public Sub DeleteFromSP(ByVal tablename As Tables, ByVal SPName As String,
    ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param, Optional Txn_Date As Date = Nothing, Optional ConditionalQuery As String = "")
            Dim Condition As [String] = ConditionalQuery
            CheckAuthorization(Permissions.Delete, "Delete", Condition, Nothing, tablename, inBasicParam, Txn_Date)
            Dim dtResult As New DataTable()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client

            Dim con As SqlConnection = Nothing
            OpenSqlConnection(con)
            Try
                Dim command As SqlCommand = con.CreateCommand()
                command.CommandTimeout = 0
                command.CommandText = SPName
                command.CommandType = CommandType.StoredProcedure
                Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
                Dim cParam As New SqlParameter()
                For index As Int16 = 0 To parameters.Length - 1
                    cParam = New SqlParameter()
                    cParam.DbType = types(index)
                    cParam.ParameterName = parameters(index)
                    If cParam.DbType = DbType.[String] Then
                        cParam.Size = length(index)
                    End If
                    If Not values(index) Is Nothing Then
                        cParam.Value = values(index)
                    Else
                        cParam.Value = DBNull.Value
                    End If
                    '  oParams(index) = cParam
                    command.Parameters.Add(cParam)
                Next

                If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                    command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                End If
                command.ExecuteNonQuery()
                Close_NonTransaction_SqlConnection(con)
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Sub

        <WebMethod(MessageName:="Get Dataset From SP Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function ListDatasetFromSP(ByVal tablename As Tables, ByVal SPName As String,
    ByVal parameters As [String](), ByVal values As [Object](), ByVal types As DbType(), ByVal length As Int32(), inBasicParam As Basic_Param) As DataSet
            Dim Condition As [String] = ""
            CheckAuthorization(Permissions.Listing, "Listing", Condition, Nothing, tablename, inBasicParam)
            Dim dtResult As New DataSet()
            'TODO: create another check to verify that the table is same as declared in tablename
            'SPName = SPName.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim dsResult As New DataSet()
            Dim con As New SqlConnection(Common.ConnectionString)
            Dim dAp As SqlDataAdapter
            Dim oParams As SqlParameter() = New SqlParameter(parameters.Length - 1) {}
            Dim cParam As New SqlParameter()
            For index As Int16 = 0 To parameters.Length - 1
                cParam = New SqlParameter()
                cParam.DbType = types(index)
                cParam.ParameterName = parameters(index)
                If cParam.DbType = DbType.[String] Then
                    cParam.Size = length(index)
                End If
                cParam.Value = values(index)
                oParams(index) = cParam
            Next
            Using con
                con.Open()
                dAp = New SqlDataAdapter(SPName, con)
                dAp.SelectCommand.CommandType = CommandType.StoredProcedure
                dAp.SelectCommand.CommandTimeout = 0
                For Each currParam As SqlParameter In oParams
                    dAp.SelectCommand.Parameters.Add(currParam)
                Next
                dAp.Fill(dsResult)
                dtResult = dsResult
            End Using
            Return dtResult
        End Function

        <WebMethod(MessageName:="Get Single Record Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function GetSingleRecord(ByVal tablename As Tables, ByVal query As String, ByVal retTableName As String, inBasicParam As Basic_Param) As DataTable
            Dim Condition As [String] = ""
            If query.ToLower().Contains("where") Then
                Condition = query.ToLower().Split(New String() {"where"}, StringSplitOptions.None)(1)
            End If
            CheckAuthorization(Permissions.Detail, "Detailed Information", Condition, Nothing, tablename, inBasicParam)
            Dim dtResult As New DataTable()
            If Not query.ToLower().Contains(tablename.ToString().ToLower()) Then
                Throw New Exception(Common.TableError(tablename))
            End If
            ' To ensure that user is calling the same table as it has rights and is being verified for 
            'TODO: create another check to verify that the table is same as declared in tablename
            query = query.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim dsResult As New DataSet()
            If Common.UseSQL Then
                Dim con As New SqlConnection(Common.ConnectionString)
                Dim dAp As SqlDataAdapter
                Using con
                    con.Open()
                    dAp = New SqlDataAdapter(query, con)
                    dAp.SelectCommand.CommandTimeout = 0
                    dAp.Fill(dsResult, retTableName)
                    dtResult = dsResult.Tables(retTableName)
                End Using
            Else

            End If

            Return dtResult
        End Function

        <WebMethod(MessageName:="Get Scalar Function")>
        <XmlInclude(GetType(DBNull))>
        Public Function GetScalar(ByVal tablename As Tables, ByVal query As String, ByVal retTableName As String, inBasicParam As Basic_Param) As Object
            Dim Condition As [String] = ""
            If query.ToLower().Contains("where") Then
                Condition = query.ToLower().Split(New String() {"where"}, StringSplitOptions.None)(1)
            End If
            CheckAuthorization(Permissions.Detail, "Detailed Information", Condition, Nothing, tablename, inBasicParam)
            If Not query.ToLower().Contains(tablename.ToString().ToLower()) Then
                Throw New Exception(Common.TableError(tablename))
            End If


            ' To ensure that user is calling the same table as it has rights and is being verified for 
            'TODO: create another check to verify that the table is same as declared in tablename
            query = query.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            'adds Server date in PlaceHolder kept by client
            Dim MaxValue As Object = Nothing
            If Common.UseSQL Then
                Dim con As SqlConnection = Nothing
                OpenSqlConnection(con)
                Try
                    Dim command As SqlCommand = con.CreateCommand()
                    command.CommandText = query
                    command.CommandTimeout = 0
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    MaxValue = command.ExecuteScalar()
                    Close_NonTransaction_SqlConnection(con)
                    'End Using
                Catch ex As Exception
                    Close_NonTransaction_SqlConnection(con)
                    Throw
                End Try
            Else

            End If

            Return MaxValue
        End Function

        <WebMethod(MessageName:="Mark as Complete Function")>
        Public Sub MarkAsComplete(ByVal tablename As Tables, ByVal RecID As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Complete, "Marking Completion", "REC_ID = '" & RecID & "'", Nothing, tablename, inBasicParam)
            Dim query As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Completed) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & " WHERE REC_ID    ='" & RecID & "' AND REC_STATUS IN (0,2)"
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandText = query
                        command.CommandTimeout = 0
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteScalar()
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else

                End If

            Catch ex As Exception
                Throw New Exception(query & ex.Message)
            End Try
        End Sub

        <WebMethod(MessageName:="Mark as Complete Function on basis of field other than RecID")>
        Public Sub MarkAsComplete(ByVal tablename As Tables, ByVal ConditionColumn As String, ByVal ConditionColumnValue As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Complete, "Marking Completion", " " & ConditionColumn & "    ='" & ConditionColumnValue & "' AND REC_STATUS IN (0,2)", Nothing, tablename, inBasicParam)
            Dim query As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Completed) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & " WHERE " & ConditionColumn & "    ='" & ConditionColumnValue & "' AND REC_STATUS IN (0,2)"
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = query
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteScalar()
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else

                End If

            Catch ex As Exception
                Throw New Exception(query & ex.Message)
            End Try
        End Sub

        <WebMethod(MessageName:="Mark as Locked Function")>
        Public Sub MarkAsLocked(ByVal tablename As Tables, ByVal RecID As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Lock, "Marking Completion", "REC_ID = '" & RecID & "'", Nothing, tablename, inBasicParam)
            Dim query As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Locked) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & " WHERE REC_ID    ='" & RecID & "' "
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = query
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteScalar()
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else

                End If

            Catch ex As Exception
                Throw New Exception(query & ex.Message)
            End Try
        End Sub

        <WebMethod(MessageName:="Mark as Locked Function on basis of field other than RecID")>
        Public Sub MarkAsLocked(ByVal tablename As Tables, ByVal ConditionColumn As String, ByVal ConditionColumnValue As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Lock, "Marking Completion", " " & ConditionColumn & "    ='" & ConditionColumnValue & "' ", Nothing, tablename, inBasicParam)
            Dim query As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Locked) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & " WHERE " & ConditionColumn & "    ='" & ConditionColumnValue & "'"
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = query
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteScalar()
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else

                End If

            Catch ex As Exception
                Throw New Exception(query & ex.Message)
            End Try
        End Sub

        <WebMethod(MessageName:="Mark as Locked Function on basis of custom condition provided")>
        Public Sub MarkAsLockedCustom(ByVal tablename As Tables, ByVal Condition As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Lock, "Marking Completion", " " & Condition & " ", Nothing, tablename, inBasicParam)
            Dim query As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Locked) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & " WHERE " & Condition & ""
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = query
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteScalar()
                        Close_NonTransaction_SqlConnection(con)
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                    ' End Using
                Else
                End If
            Catch ex As Exception
                Throw New Exception(query & ex.Message)
            End Try
        End Sub

        <WebMethod(MessageName:="Insert Function")>
        Public Sub InsertPublic(ByVal tablename As Tables, ByVal query As String, inBasicParam As Basic_Param, ByVal RecID As String)
            Insert(tablename, query, inBasicParam, RecID)
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="tablename"></param>
        ''' <param name="query"></param>
        ''' <param name="inBasicParam"></param>
        ''' <param name="RecID"></param>
        ''' <param name="Txn_Date"></param>
        ''' <param name="RecAddTime">Used for Transaction scope functions where multiple insertions/updations are made in one transaction</param>
        ''' <param name="RecEditTime"></param>
        ''' <remarks></remarks>
        Public Sub Insert(ByVal tablename As Tables, ByVal query As String, inBasicParam As Basic_Param, ByVal RecID As String, Optional Txn_Date As Date = Nothing, Optional RecAddTime As DateTime = Nothing, Optional SqlParameterName As String = Nothing, Optional SqldbType As SqlDbType = Nothing, Optional SQLParamValue As Object = Nothing)
            If Not query.ToLower().Contains(tablename.ToString().ToLower()) Then
                Throw New Exception(Common.TableError(tablename))
            End If

            ' To ensure that user is calling the same table as it has rights and is being verified for 
            If Not query.ToLower().Contains("insert") Then
                Throw New Exception(Common.InsertError())
            End If
            If RecAddTime = Nothing Then
                query = query.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            Else
                query = query.Replace(Common.DateTimePlaceHolder, RecAddTime.ToString(Common.DateFormatLong)) ' Used for Transaction scope functions where multiple insertions/updations are made in one transaction
            End If

            ' To ensure that user is inserting , as it  is being verified for 
            CheckAuthorization(Permissions.Add, "Adding Records", Nothing, RecID, tablename, inBasicParam, Txn_Date)

            'adds Server date in PlaceHolder kept by client
            Dim LoginExceptionQuery As String = "INSERT INTO so_login_exceptions(SLE_CEN_ID,SLE_EXCEPTION_TYPE) VALUES ('" & inBasicParam.openCenID & "','" & Common.LoginExceptions.Sync_Before_2_0.ToString().ToUpper() & "');"
            If Common.UseSQL Then
                Dim con As SqlConnection = Nothing
                OpenSqlConnection(con)
                Try
                    Dim command As SqlCommand = con.CreateCommand()
                    command.CommandTimeout = 0
                    command.CommandText = query
                    If Not SqlParameterName Is Nothing Then
                        command.Parameters.AddWithValue(SqlParameterName, "SqlDbType.Binary").Value = SQLParamValue
                    End If
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    command.ExecuteNonQuery()
                    'CHECK IF THE INSERTION QUERY PERTAINS TO CENTER CREATION 
                    If tablename = Tables.CLIENT_USER_INFO Then
                        query = LoginExceptionQuery
                        command.CommandText = query
                        command.ExecuteNonQuery()
                    End If
                    Close_NonTransaction_SqlConnection(con)
                Catch ex As Exception
                    Close_NonTransaction_SqlConnection(con)
                    Throw
                End Try
                'End Using
            Else
            End If

        End Sub

        Public Sub Update(ByVal tablename As Tables, ByVal query As String, inBasicParam As Basic_Param, Optional EditTime As DateTime = Nothing, Optional Txn_Date As DateTime = Nothing, Optional SqlParameterName As String = Nothing, Optional SqldbType As SqlDbType = Nothing, Optional SQLParamValue As Object = Nothing, Optional ConditionalQuery As String = "")
            If Not query.ToLower().Contains(tablename.ToString().ToLower()) Then
                Throw New Exception(Common.TableError(tablename))
            End If


            ' To ensure that user is calling the same table as it has rights and is being verified for 
            If Not query.ToLower().Contains("update") Then
                Throw New Exception(Common.UpdateError())
            End If
            If EditTime = Nothing Then
                query = query.Replace(Common.DateTimePlaceHolder, DateTime.Now.ToString(Common.DateFormatLong))
            Else
                query = query.Replace(Common.DateTimePlaceHolder, EditTime.ToString(Common.DateFormatLong))
            End If
            If ConditionalQuery = "" Then ConditionalQuery = query.ToLower().Split(New String() {"where"}, StringSplitOptions.None)(1)

            ' To ensure that user is inserting , as it  is being verified for 
            CheckAuthorization(Permissions.Edit, "Updating Records", ConditionalQuery, Nothing, tablename, inBasicParam, Txn_Date)

            'adds Server date in PlaceHolder kept by client
            If Common.UseSQL Then
                Dim con As SqlConnection = Nothing
                OpenSqlConnection(con)
                Try
                    Dim command As SqlCommand = con.CreateCommand()
                    command.CommandTimeout = 0
                    command.CommandText = query
                    If Not SqlParameterName Is Nothing Then
                        command.Parameters.AddWithValue(SqlParameterName, "SqlDbType.Binary").Value = SQLParamValue
                    End If
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    command.ExecuteNonQuery()
                    Close_NonTransaction_SqlConnection(con)
                Catch ex As Exception
                    Close_NonTransaction_SqlConnection(con)
                    Throw
                End Try
                'End Using
            Else
            End If
        End Sub

        <WebMethod(MessageName:="Delete Function for Rec_ID")>
        Public Sub Delete(ByVal tablename As Tables, ByVal RecID As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Delete, "Deleting Records", "REC_ID = '" & RecID & "'", Nothing, tablename, inBasicParam)
            Dim txt As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Deleted) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'" & ", REC_EDIT_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_EDIT_BY     ='" & inBasicParam.openUserID & "' WHERE REC_ID    ='" & RecID & "'"
            If Common.UseSQL() Then 'actual deletion
                txt = " DELETE " & tablename.ToString() & " WHERE REC_ID    ='" & RecID & "'"
            End If
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = txt
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteNonQuery().ToString()
                        Close_NonTransaction_SqlConnection(con)
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                    'End Using
                Else
                End If
            Catch ex As Exception
                Throw New Exception(txt)
            End Try

        End Sub

        <WebMethod(MessageName:="Delete Function with Query, for SO Tables Only")>
        Public Sub Delete_SO_Table_Record(ByVal tablename As Tables, ByVal Query As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Delete, "Deleting Records", "", Nothing, tablename, inBasicParam)
            Dim txt As String = Query
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = Query
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteNonQuery().ToString()
                        Close_NonTransaction_SqlConnection(con)
                        ' End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else

                End If
            Catch ex As Exception
                Throw New Exception(txt)
            End Try
        End Sub

        <WebMethod(MessageName:="Delete Function For Custom Condition")>
        Public Sub DeleteByCondition(ByVal tablename As Tables, ByVal CustomSearchCondition As String, inBasicParam As Basic_Param)
            CheckAuthorization(Permissions.Delete, "Deleting Records", CustomSearchCondition, Nothing, tablename, inBasicParam)
            Dim txt As String = " UPDATE " & tablename.ToString() & " SET REC_STATUS =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Deleted) & ", REC_STATUS_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_STATUS_BY     ='" & inBasicParam.openUserID & "' " & ", REC_EDIT_ON     ='" & DateTime.Now.ToString(Common.DateFormatLong) & "'," & "REC_EDIT_BY     ='" & inBasicParam.openUserID & "' WHERE " & CustomSearchCondition
            If Common.UseSQL() Then 'actual deletion
                txt = " DELETE " & tablename.ToString() & " WHERE " & CustomSearchCondition
            End If
            Try
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim command As SqlCommand = con.CreateCommand()
                        command.CommandTimeout = 0
                        command.CommandText = txt
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        command.ExecuteNonQuery().ToString()
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else
                End If
            Catch ex As Exception
                Throw New Exception(txt)
            End Try
        End Sub

        <WebMethod>
        Public Function GetCurrDate() As DateTime
            Return DateTime.Now
        End Function
#End Region

#Region "--Wrapper Functions--"
        <WebMethod(MessageName:="Wrapper for Get List Function without parameter")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_List(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param) As Byte()
            Dim retTable As DataTable = Nothing
            Select Case FunctionCalled
                'AddressBook_Related
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetList
                    retTable = Addresses.GetList(Nothing, inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetList_Common
                    retTable = Addresses.GetList_Common(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesList
                    retTable = Addresses.GetPartiesList(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetCenterList
                    retTable = Addresses.GetCenterList(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetOverseasCenterList
                    retTable = Addresses.GetOverseasCenterList(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_ServiceableSouls_GetListOfSouls
                    retTable = ServiceableSouls.GetListOfSouls(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Students_GetListOfStudents
                    retTable = Students.GetListOfStudents(inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_GetOrgList
                    retTable = Addresses.GetOrgList(inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetScreens
                    retTable = ClientUserInfo.GetScreens(inBasicParam)

                    ' Advances
                Case RealServiceFunctions.Advances_GetList_Common
                    retTable = Advances.GetList_Common(inBasicParam)

                    'Assets
                Case RealServiceFunctions.AssetLocations_GetList
                    retTable = AssetLocations.GetList(Nothing, inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetFullList
                    retTable = AssetLocations.GetFullList(inBasicParam)
                Case RealServiceFunctions.Assets_GetList_Common
                    retTable = Assets.GetList_Common(inBasicParam)
                Case RealServiceFunctions.Attachments_GetAttachmentCount
                    retTable = Attachments.GetAttachmentCount(inBasicParam)

                    'Bank 
                Case RealServiceFunctions.Bank_GetList_Common
                    retTable = BankAccounts.GetList_Common(inBasicParam)
                Case RealServiceFunctions.Bank_GetList
                    retTable = BankAccounts.GetList(inBasicParam)
                Case RealServiceFunctions.Bank_GetFDAccountList
                    retTable = BankAccounts.GetFDAccountList(inBasicParam)

                     'CashWithdrawDeposit
                Case RealServiceFunctions.Vouchers_GetSplVoucherRefsFromCenterTasks
                    retTable = Vouchers.GetSplVoucherRefsFromCenterTasks(inBasicParam)


                    'Centre Related
                Case RealServiceFunctions.CentreRelated_Center_GetAuditTxnPeriod
                    retTable = Center.GetAuditTxnPeriod(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Core_GetCenSupportInfo
                    retTable = Core.GetCenSupportInfo(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Core_GetList
                    retTable = Core.GetList(inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_GetFinancialYearList
                    retTable = CodInfo.GetFinancialYearList(inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_GetUnAuditedYearList
                    retTable = CodInfo.GetUnAuditedFinancialYearList(inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetCompletedAuditVerifications
                    retTable = Center.GetAuditsVerificationsCompleted(inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetNotifications
                    retTable = ClientUserInfo.GetNotifications(inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUsersList
                    retTable = ClientUserInfo.GetUsersList(inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetAuditPeriod
                    retTable = Center.GetAuditPeriod(inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetAccountsSubmittedPeriod
                    retTable = Center.GetAccountsSubmittedPeriod(inBasicParam)


                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetPrivileges
                    retTable = ClientUserInfo.GetPrivileges(inBasicParam)



                    'CenterPurpose
                Case RealServiceFunctions.CenterPurpose_GetList
                    retTable = Center_Purpose_Info.GetList(inBasicParam)

                    'Consumables
                Case RealServiceFunctions.Consumables_GetList
                    retTable = Consumables.GetList(inBasicParam)
                    'CollectionBox
                Case RealServiceFunctions.CollectionBox_GetPastWitness
                    retTable = Voucher_CollectionBox.GetPastWitness(inBasicParam)
                    'Core functions 
                Case RealServiceFunctions.Core_GetBankInfo
                    retTable = CoreFunctions.GetBankInfo(inBasicParam)
                Case RealServiceFunctions.Core_GetBankInfo_Common
                    retTable = CoreFunctions.GetBankInfo_Common(inBasicParam)
                Case RealServiceFunctions.Core_GetMiscDetails_Common
                    retTable = CoreFunctions.GetMiscDetails_Common(inBasicParam)
                Case RealServiceFunctions.Core_GetMisc_Common
                    retTable = CoreFunctions.GetMisc_Common(inBasicParam)
                Case RealServiceFunctions.Core_GetItemsByQuery_Common
                    retTable = CoreFunctions.GetItemsByQuery_Common(inBasicParam)

                Case RealServiceFunctions.Core_GetItems_Ledger_Common
                    retTable = CoreFunctions.GetItems_Ledger_Common(inBasicParam)
                Case RealServiceFunctions.Core_GetLedgersList
                    retTable = CoreFunctions.GetLedgersList(inBasicParam)

                Case RealServiceFunctions.Core_GetCenterTaskInfo
                    retTable = CoreFunctions.GetCenterTaskInfo(inBasicParam)
                Case RealServiceFunctions.Core_GetWingsCenterTaskInfo
                    retTable = CoreFunctions.GetCenterWingsTaskInfo(inBasicParam)
                Case RealServiceFunctions.Core_GetCurrencyList
                    retTable = CoreFunctions.GetCurrencyList(inBasicParam)
                Case RealServiceFunctions.Core_GetCenterDetailsForLetterPAD
                    retTable = CoreFunctions.GetCenterDetailsForLetterPAD(inBasicParam)

                Case RealServiceFunctions.Core_GetInstituteList
                    retTable = CoreFunctions.GetInstituteList(inBasicParam)
                Case RealServiceFunctions.Core_GetWingsList
                    retTable = CoreFunctions.GetWingsList(inBasicParam)
                Case RealServiceFunctions.Core_GetCities
                    retTable = CoreFunctions.GetCities(inBasicParam)
                Case RealServiceFunctions.Core_GetLedgerDetails
                    retTable = CoreFunctions.GetLedgersDetails(inBasicParam)

                    'Data functions

                Case RealServiceFunctions.Data_GetCashOpeningBalanceAmount
                    retTable = DataFunctions.GetCashOpeningBalanceAmount(inBasicParam)
                Case RealServiceFunctions.Data_GetCashTransSumAmount
                    retTable = DataFunctions.GetCashTransSumAmount(inBasicParam)

                Case RealServiceFunctions.Data_GetPartyIDList
                    retTable = DataFunctions.GetPartyIDList(inBasicParam)
                Case RealServiceFunctions.Data_GetUsedItemIDList
                    retTable = DataFunctions.GetUsedItemIDList(inBasicParam)

                    'Deposits
                Case RealServiceFunctions.Deposits_GetList_Common
                    retTable = Deposits.GetList_Common(inBasicParam)



                    'Donation
                Case RealServiceFunctions.Donation_GetAddresses
                    retTable = DonationRegister.GetAddresses(inBasicParam)

                Case RealServiceFunctions.Donation_GetDonationStatus
                    retTable = DonationRegister.GetDonationStatus(inBasicParam)
                Case RealServiceFunctions.Donation_GetDonationPurposes
                    retTable = DonationRegister.GetDonationPurposes(inBasicParam)
                Case RealServiceFunctions.Donation_GetDonationPrints
                    retTable = DonationRegister.GetDonationPrints(inBasicParam)
                Case RealServiceFunctions.Donation_GetDonationRejections
                    retTable = DonationRegister.GetDonationRejections(inBasicParam)
                Case RealServiceFunctions.Donation_GetDonationDispatches
                    retTable = DonationRegister.GetDonationDispatches(inBasicParam)

                    'FDs
                Case RealServiceFunctions.FDs_GetTDS
                    retTable = FD.GetTDS(inBasicParam)
                Case RealServiceFunctions.FDs_GetTDSReversals
                    retTable = FD.GetTDSReversals(inBasicParam)
                Case RealServiceFunctions.FDs_GetInterest
                    retTable = FD.GetInterest(inBasicParam)
                Case RealServiceFunctions.FDs_GetInterestOverheads
                    retTable = FD.GetInterestOverheads(inBasicParam)

                    'GoldSilver
                Case RealServiceFunctions.GoldSilver_GetList_Common
                    retTable = GoldSilver.GetList_Common(inBasicParam)
                    'InternalTransfer
                Case RealServiceFunctions.InternalTransfer_GetList
                    retTable = Voucher_Internal_Transfer.GetList(inBasicParam)

                    'Letters
                Case RealServiceFunctions.Letters_GetList
                    retTable = Letters.GetList(inBasicParam)
                    'Liabilities
                Case RealServiceFunctions.Liabilities_GetList_Common
                    retTable = Liabilities.GetList_Common(inBasicParam)
                    'Livestock
                Case RealServiceFunctions.Livestock_GetList_Common
                    retTable = Livestock.GetList_Common(inBasicParam)
                    'Membership
                Case RealServiceFunctions.Membership_GetWings
                    retTable = Membership.GetWings(inBasicParam)
                    'Murli
                Case RealServiceFunctions.Murli_GetList
                    retTable = Murli.GetList(inBasicParam)

                    'News
                Case RealServiceFunctions.News_GetList
                    retTable = News.GetList(inBasicParam)
                    'Notes
                Case RealServiceFunctions.Notes_GetList
                    retTable = Notes.GetList(inBasicParam)
                Case RealServiceFunctions.Notes_GetShortList
                    retTable = Notes.GetShortList(inBasicParam)
                    'Property
                Case RealServiceFunctions.Property_GetListByQuery_Common
                    retTable = LandAndBuilding.GetListByQuery_Common(inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetSupplier_PriceHistory
                    retTable = StockPurchaseOrder.GetSupplier_PriceHistory(inBasicParam)

                    'Request
                Case RealServiceFunctions.Request_GetList
                    retTable = Request.GetList(inBasicParam)

                    'Report
                Case RealServiceFunctions.Reports_Purpose
                    retTable = Reports_All.GetPurposeReport(inBasicParam)

                           'Report
                Case RealServiceFunctions.Reports_VoucherReference
                    retTable = Reports_All.GetVoucherReferenceReport(inBasicParam)

                Case RealServiceFunctions.Report_GetInsuredYears
                    retTable = Reports_All.GetInsuredYears(inBasicParam)
                Case RealServiceFunctions.Reports_GetBankAccountsList
                    retTable = Reports.GetBankAccountsList(inBasicParam)

                    'ServicePlaces
                Case RealServiceFunctions.ServicePlaces_GetList
                    retTable = ServicePlaces.GetList(inBasicParam)

                    'SubItem
                Case RealServiceFunctions.SubItem_GetMainCategoriesMaster
                    retTable = SubItem.GetMainCategoriesMaster(inBasicParam)
                Case RealServiceFunctions.SubItem_GetItemPropertiesMaster
                    retTable = SubItem.GetItemPropertiesMaster(inBasicParam)

                    'Suppliers
                Case RealServiceFunctions.Suppliers_GetAllSuppliers
                    retTable = Suppliers.GetAllSuppliers(Nothing, inBasicParam)

                Case RealServiceFunctions.Suppliers_GetItemMappedSuppliers
                    retTable = Suppliers.GetItemMappedSuppliers(Nothing, inBasicParam)

                    'Telephone
                Case RealServiceFunctions.Telephone_GetList
                    retTable = Telephones.GetList(inBasicParam)

                      'UO
                Case RealServiceFunctions.UserOrder_Get_CenterDetails_StockAvailability
                    retTable = StockUserOrder.Get_CenterDetails_StockAvailability(inBasicParam)

                    'Vehicles
                Case RealServiceFunctions.Vehicles_GetList_Common
                    retTable = Vehicles.GetList_Common(inBasicParam)
                    'Vouchers
                Case RealServiceFunctions.Vouchers_GetDonationStatus
                    retTable = Vouchers.GetDonationStatus(inBasicParam)
                Case RealServiceFunctions.Vouchers_GetBankEntriesCountInNextEvent
                    retTable = Vouchers.GetBankEntriesCountInNextEvent(inBasicParam)

                    'wip
                Case RealServiceFunctions.WIP_Profile_GetList_Common
                    retTable = WIP_Profile.GetList_Common(inBasicParam)


                    '    'WIP Finalization
                    'Case RealServiceFunctions.Voucher_WIP_Finalization_GetListOfWIPLedgers
                    '    retTable = Voucher_WIP_Finalization.GetListOfWIPLedgers(inBasicParam)

            End Select
            Return CompressData(retTable)
        End Function

        <WebMethod(MessageName:="Wrapper for Get List Function with parameter") _
        , XmlInclude(GetType(CoreFunctions.Param_GetBankBranchesForMultipleIDs)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetMisc)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetMiscDetails)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItems_ItemProfile)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItemsAll)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItemsByMultipleItemProfile)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetOpeningProfileItems)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetInstituteList)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetInstituteListNameInShort)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCountriesList)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCountriesByID)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetStatesList)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetStatesByID)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetDistrictsList)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetDistrictsByID)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCitiesListByCountryandstate)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCitiesListByCountry)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItems_Ledger)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCitiesByID)) _
        , XmlInclude(GetType(DataFunctions.Param_GetOpeningBalance)) _
        , XmlInclude(GetType(DataFunctions.Param_GetCashBankTransSumAmountTwoDates)) _
        , XmlInclude(GetType(Action_Items.Param_Action_Items_GetList)) _
        , XmlInclude(GetType(Addresses.Param_Get_Duplicates)) _
        , XmlInclude(GetType(ClientUserInfo.Param_ClientUserInfo_GetUserInfo)) _
        , XmlInclude(GetType(ClientUserInfo.Param_ClientUserInfo_GetListFilteredByCenIDUserID)) _
        , XmlInclude(GetType(Center.Param_Center_GetCenterListByAuditor_Instt)) _
        , XmlInclude(GetType(Center.Param_Center_GetCenterListByPAD_Name)) _
        , XmlInclude(GetType(Center.Param_Center_GetTxnStatusCount)) _
        , XmlInclude(GetType(Center.Param_Center_GetCenterListByCenID)) _
        , XmlInclude(GetType(Advances.Param_Advances_GetList)) _
        , XmlInclude(GetType(Center.Param_Center_GetTransfersStatus)) _
        , XmlInclude(GetType(Liabilities.Param_Liabilities_GetList)) _
        , XmlInclude(GetType(Liabilities.Param_Liabilities_GetListCommon)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItemsLedgerCommon)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetList)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetMiscDetailsCommon)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItemsByQueryCommon)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetItemsByMultipleItemIDs)) _
        , XmlInclude(GetType(DataFunctions.Param_GetBankTransSumAmount_Common)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetTDSRecords_Common)) _
        , XmlInclude(GetType(Addresses.Parameter_Addresses_GetList_Common)) _
        , XmlInclude(GetType(Addresses.Param_GetAddressUsageCount)) _
        , XmlInclude(GetType(BankAccounts.Param_Bank_GetList_Common)) _
        , XmlInclude(GetType(Advances.Param_Advances_GetList_Common)) _
        , XmlInclude(GetType(Magazine.Param_GetList_Magazine)) _
        , XmlInclude(GetType(Magazine.Param_GetFee_Magazine)) _
        , XmlInclude(GetType(Magazine.Param_GetMembers_Magazine)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetListByCondition)) _
        , XmlInclude(GetType(DataFunctions.Param_GetOpeningBalance_Common)) _
        , XmlInclude(GetType(CoreFunctions.Param_GetCenterDetailsByQuery_Common)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetListByQuery)) _
        , XmlInclude(GetType(Deposits.Param_Deposits_GetList)) _
        , XmlInclude(GetType(Deposits.Param_Deposits_GetListCommon)) _
        , XmlInclude(GetType(FD.Param_FDs_GetList)) _
        , XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetInterestRecords)) _
        , XmlInclude(GetType(Assets.Param_Assets_GetList)) _
        , XmlInclude(GetType(GoldSilver.Param_GoldSilver_GetList)) _
        , XmlInclude(GetType(Vehicles.Param_Vehicles_GetList)) _
        , XmlInclude(GetType(Vehicles.Param_Vehicles_GetListByCondition)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetPastParties)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetListWithMultipleParams)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetListWithMultipleParams_SP)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetList)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetNegativeBalance)) _
        , XmlInclude(GetType(Vouchers.Parameter_GetAdjustments)) _
        , XmlInclude(GetType(Vouchers.Parameter_GetReferenceTxnRecord_MID)) _
        , XmlInclude(GetType(Payments.Param_Payments_GetTxnPaymentDetail)) _
        , XmlInclude(GetType(Payments.Param_Payments_GetAdvancesPaid)) _
        , XmlInclude(GetType(Payments.Param_Payments_GetLiabilitiesPaid)) _
        , XmlInclude(GetType(Receipts.Param_Receipts_GetAdvancesRefundList)) _
        , XmlInclude(GetType(Receipts.Param_Receipts_GetDepositsRefundList)) _
        , XmlInclude(GetType(DonationRegister.Param_DonationRegister_GetAddressDetail_Form)) _
        , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetList)) _
        , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetListWithMultipleParams)) _
        , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetFrCenterList)) _
        , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetToCenterList)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetFrCenterList)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetToCenterList)) _
        , XmlInclude(GetType(Reports.Param_GelLedgerReport)) _
            , XmlInclude(GetType(Reports.Param_MagSubDispatchReport)) _
        , XmlInclude(GetType(Reports.Param_GetItemReport)) _
        , XmlInclude(GetType(Reports.Param_GetPrimaryGroupReport)) _
        , XmlInclude(GetType(Reports.Param_GetPartyListing)) _
        , XmlInclude(GetType(Reports.Param_GetPartyReport)) _
        , XmlInclude(GetType(Reports.Param_GetSecondaryGroupReport)) _
        , XmlInclude(GetType(Reports.Param_GetTrial_Balance)) _
        , XmlInclude(GetType(Voucher_SaleOfAsset.Param_Voucher_SaleOfAsset_GetTxnList)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetTxnList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetTransactionList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetConstructionExpensesList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetGiftTransactionList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetCollectionBoxTransactionList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetAddresses)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetTSummaryList)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetMembershipReceipt)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetMembershipReceipt)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetTelephoneBill)) _
        , XmlInclude(GetType(Livestock.Param_GetList_Livestock)) _
        , XmlInclude(GetType(Membership.Param_GetSubscriptionFee_Membership)) _
        , XmlInclude(GetType(Membership.Param_GetMasterTransactionList_Membership)) _
        , XmlInclude(GetType(Membership.Param_GetList_Membership)) _
        , XmlInclude(GetType(Voucher_Journal.Parameter_GetJornalEntryAdjustments)) _
        , XmlInclude(GetType(Membership_Receipt_Register.Param_GetList_Membership_Receipt_Register)) _
        , XmlInclude(GetType(Voucher_Journal.Parameter_GetCurrentRecAdjustment)) _
        , XmlInclude(GetType(Reports.Param_GetTrial_Balance)) _
        , XmlInclude(GetType(Consumables.Param_GetSummary)) _
        , XmlInclude(GetType(Notebook.Parameter_Notebook_GetList)) _
        , XmlInclude(GetType(GoldSilver.Param_GoldSilver_GetList_Common)) _
        , XmlInclude(GetType(OpeningBalances.Parameter_OpeningBalances_GetList)) _
        , XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetFDs)) _
        , XmlInclude(GetType(Assets.Param_Assets_GetList_Common)) _
        , XmlInclude(GetType(Livestock.Param_Livestock_GetList_Common)) _
        , XmlInclude(GetType(Deposits.Param_GetPaymentsForParties)) _
        , XmlInclude(GetType(Vehicles.Param_Vehicles_GetList_Common)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetListForExpenses)) _
        , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransferGet_Tf_Banks)) _
        , XmlInclude(GetType(Liabilities.Param_GetLiabPaymentsForParties)) _
        , XmlInclude(GetType(Advances.Param_AdvGetPayments)) _
        , XmlInclude(GetType(Assets.Param_Assets_GetTransactions)) _
        , XmlInclude(GetType(Assets.Param_Assets_GetTransactions_ByTable)) _
        , XmlInclude(GetType(GoldSilver.Param_GS_GetTransactions)) _
        , XmlInclude(GetType(LandAndBuilding.Param_Veh_GetTransactions)) _
        , XmlInclude(GetType(Livestock.Param_LS_GetTransactions)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Param_GetAssetTransfers)) _
        , XmlInclude(GetType(Voucher_Membership_Renewal.Param_GetPartyDetails_VoucherMembershipRenewal)) _
        , XmlInclude(GetType(Voucher_Membership_Conversion.Param_GetPartyDetails_VoucherMembershipConversion)) _
        , XmlInclude(GetType(Vouchers.Param_GetStatusTrCode)) _
        , XmlInclude(GetType(Reports_All.Param_GetInsuranceLetterData)) _
        , XmlInclude(GetType(AssetLocations.Param_Check_Location_Count)) _
        , XmlInclude(GetType(Membership.Param_GetBalancesList)) _
        , XmlInclude(GetType(WIP_Profile.Param_GetProfileListing_WIP)) _
        , XmlInclude(GetType(Voucher_WIP_Finalization.Param_GetListOfFinalizedAssets)) _
        , XmlInclude(GetType(Addresses.Param_GetAddressesForLabels)) _
        , XmlInclude(GetType(Magazine.Param_get_MagazineMembershipRegister)) _
        , XmlInclude(GetType(AssetLocations.Param_AssetLocation_GetList)) _
        , XmlInclude(GetType(Vouchers.Param_GetBank_Reconciliation)) _
        , XmlInclude(GetType(Deposit_Slip.Param_GetDepositSlipList)) _
        , XmlInclude(GetType(TDS.Parameter_GetTDS_Deducted_Not_Sent)) _
        , XmlInclude(GetType(DataFunctions.Param_GetYearPeriod)) _
        , XmlInclude(GetType(Personnels.Param_GetPersonnelCharges)) _
        , XmlInclude(GetType(Audit.Param_GetDocumentMapping)) _
        , XmlInclude(GetType(Audit.Param_GetDocumentChecking)) _
        , XmlInclude(GetType(DonationRegister.Param_DonationRegister_GetList)) _
        , XmlInclude(GetType(Vouchers.Param_GetProfileEntryDetails)) _
        , XmlInclude(GetType(Attachments.Parameter_Attachment_GetList)) _
        , XmlInclude(GetType(Vouchers.Param_GetSaleReferenceRecord))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_List(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Byte()
            Dim RetTable As DataTable = Nothing
            Select Case FunctionCalled
                'Options
                'ActionItems
                Case RealServiceFunctions.ActionItems_GetList
                    RetTable = Action_Items.GetList(inBasicParam, CType(inParam, Action_Items.Param_Action_Items_GetList))
                Case RealServiceFunctions.CentreRelated_Center_GetTransfersStatus
                    RetTable = Center.GetTransfersStatus(CType(inParam, Center.Param_Center_GetTransfersStatus), inBasicParam)
                     'Attachment 
                Case RealServiceFunctions.Attachments_GetRecord
                    RetTable = Attachments.GetRecord(CType(inParam, String), inBasicParam)
                    'Address Book Related
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetList_Common
                    RetTable = Addresses.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesList
                    RetTable = Addresses.GetPartiesList(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetList
                    RetTable = Addresses.GetList(CType(inParam, Boolean), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesListForSpecifiedRecIds
                    RetTable = Addresses.GetPartiesList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetDuplicateCount
                    RetTable = Addresses.GetDuplicateCount(CType(inParam, Addresses.Param_Get_Duplicates), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecIDs_ForAllYears
                    RetTable = Addresses.GetAddressRecIDs_ForAllYears(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetAddressUsageCount
                    RetTable = Addresses.GetAddressUsageCount(CType(inParam, Addresses.Param_GetAddressUsageCount), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetAddressesForLabels
                    RetTable = Addresses.GetAddressesForLabels(CType(inParam, Addresses.Param_GetAddressesForLabels), inBasicParam)
                      'Attachment
                Case RealServiceFunctions.Attachments_GetList
                    RetTable = Attachments.GetList(CType(inParam, Attachments.Parameter_Attachment_GetList), inBasicParam)
                    'Assets 
                Case RealServiceFunctions.AssetLocations_GetList
                    RetTable = AssetLocations.GetList(CType(inParam, AssetLocations.Param_AssetLocation_GetList), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetStockLocationList
                    RetTable = AssetLocations.GetStockLocationList(CType(inParam, AssetLocations.Param_AssetLocation_GetList), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetListByLBID
                    RetTable = AssetLocations.GetListByLBID(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.AssetLocations_GetListBySPID
                    RetTable = AssetLocations.GetListBySPID(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Assets_GetList
                    RetTable = Assets.GetList(CType(inParam, Assets.Param_Assets_GetList), inBasicParam)
                Case RealServiceFunctions.Assets_GetRecord
                    RetTable = Assets.GetRecord(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Assets_GetListByCondition
                    RetTable = Assets.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Assets_GetList_Common
                    RetTable = Assets.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Assets_GetList_Common_ByRecID
                    RetTable = Assets.GetList_Common_ByRecID(inBasicParam, CType(inParam, Assets.Param_Assets_GetList_Common))
                Case RealServiceFunctions.Assets_GetTransactions
                    RetTable = Assets.GetTransactions(CType(inParam, Assets.Param_Assets_GetTransactions), inBasicParam)
                Case RealServiceFunctions.Assets_GetTransactions_ByTable
                    RetTable = Assets.GetTransactions_TableName(CType(inParam, Assets.Param_Assets_GetTransactions_ByTable), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationMatching
                    RetTable = AssetLocations.GetLocationMatching(CType(inParam, Tables), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationCountInAssets
                    RetTable = AssetLocations.GetLocationCountInAssets(CType(inParam, AssetLocations.Param_Check_Location_Count), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationCountInGS
                    RetTable = AssetLocations.GetLocationCountInGS(CType(inParam, AssetLocations.Param_Check_Location_Count), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationCountInConsumables
                    RetTable = AssetLocations.GetLocationCountInConsumables(CType(inParam, AssetLocations.Param_Check_Location_Count), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationCountInLiveStock
                    RetTable = AssetLocations.GetLocationCountInLiveStock(CType(inParam, AssetLocations.Param_Check_Location_Count), inBasicParam)
                    'Case RealServiceFunctions.AssetLocations_GetLocationCountInTelephones
                    '   Return AssetLocations.GetLocationCountInTelephones(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetLocationCountInVehicles
                    RetTable = AssetLocations.GetLocationCountInVehicles(CType(inParam, AssetLocations.Param_Check_Location_Count), inBasicParam)
                Case RealServiceFunctions.AssetLocations_Get_Asset_Movement_Logs
                    RetTable = AssetLocations.Get_Asset_Movement_Logs(CType(inParam, String), inBasicParam)

                    'Advances
                Case RealServiceFunctions.Advances_GetList
                    RetTable = Advances.GetList(CType(inParam, Advances.Param_Advances_GetList), inBasicParam)
                Case RealServiceFunctions.Advances_GetList_Common
                    RetTable = Advances.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Advances_GetListByCondition
                    RetTable = Advances.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Advances_GetPayments
                    RetTable = Advances.GetPayments(CType(inParam, Advances.Param_AdvGetPayments), inBasicParam)


                Case RealServiceFunctions.Audit_GetDocumentchecking
                    RetTable = Audit.GetDocumentChecking(CType(inParam, Audit.Param_GetDocumentChecking), inBasicParam)

                    'Bank
                Case RealServiceFunctions.Bank_GetList
                    RetTable = BankAccounts.GetList(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Bank_GetList_Common
                    RetTable = BankAccounts.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Bank_GetRecord
                    RetTable = BankAccounts.GetRecord(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Bank_GetTransSums
                    RetTable = BankAccounts.GetTransSums(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetPaymentTransSums
                    RetTable = BankAccounts.GetPaymentTransSums(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetReceiptTransSums
                    RetTable = BankAccounts.GetReceiptTransSums(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetFDBankByCustomerNo
                    RetTable = BankAccounts.GetFDBankByCustomerNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetFDAccountList
                    RetTable = BankAccounts.GetFDAccountList(inBasicParam, CType(inParam, String))

                    'cash
                Case RealServiceFunctions.Cash_GetCashOpeningBalance
                    RetTable = Cash.GetCashOpeningBalance(CType(inParam, String), inBasicParam) 'step:4

                    'CentreRelated 
                Case RealServiceFunctions.CentreRelated_Center_GetChildCenterList
                    RetTable = Center.GetChildCenterList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetCenterListByAuditor_Instt
                    RetTable = Center.GetCenterListByAuditor_Instt(CType(inParam, Center.Param_Center_GetCenterListByAuditor_Instt), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetCenterListByPAD_Name
                    RetTable = Center.GetCenterListByPAD_Name(CType(inParam, Center.Param_Center_GetCenterListByPAD_Name), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetCenterListByCenID
                    RetTable = Center.GetCenterListByCenID(CType(inParam, Center.Param_Center_GetCenterListByCenID), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetCenterDetailsByCertNo
                    RetTable = Center.GetCenterDetailsByCertNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetCenterListByBKCertNo
                    RetTable = Center.GetCenterListByBKCertNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetTxnStatusCount
                    RetTable = Center.GetTxnStatusCount(CType(inParam, Center.Param_Center_GetTxnStatusCount), inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetAddressesForLabels
                    RetTable = Center.GetAddressesForLabels(CType(inParam, Addresses.Param_GetAddressesForLabels), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_Get_Contact_Info
                    RetTable = Center.Get_Contact_Info(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfoCustomCenId
                    RetTable = ClientUserInfo.GetCenterUserInfo(CType(inParam, ClientUserInfo.Param_ClientUserInfo_GetUserInfo), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfo
                    RetTable = ClientUserInfo.GetUserInfo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserDetails
                    RetTable = ClientUserInfo.GetUserDetails(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_Get_ClientUser_GetGroupDetails
                    RetTable = ClientUserInfo.GetGroupDetails(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetList
                    RetTable = ClientUserInfo.GetList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId
                    RetTable = ClientUserInfo.GetList(CType(inParam, ClientUserInfo.Param_ClientUserInfo_GetListFilteredByCenIDUserID), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetListByCenID
                    RetTable = ClientUserInfo.GetList(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.CentreRelated_CodInfo_GetCreatedCentersFromSelected
                    RetTable = CodInfo.GetCreatedCentersFromSelected(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_Base_GetCen_Ins_List
                    RetTable = CodInfo.Base_GetCen_Ins_List(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_Base_GetSelectCentreList
                    RetTable = CodInfo.Base_GetSelectCentreList(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.CentreRelated_CodInfo_GetUnAuditedFinancialYearOfTransferorCentre
                    RetTable = CodInfo.GetUnAuditedFinancialYearOfTransferorCentre(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetAddressesForLabels
                    RetTable = Center.GetAddressesForLabels(CType(inParam, Addresses.Param_GetAddressesForLabels), inBasicParam)



                Case RealServiceFunctions.Core_GetItemDocuments
                    RetTable = CoreFunctions.GetItemDocuments(CType(inParam, String), inBasicParam)
                    'Core Functions'
                Case RealServiceFunctions.Data_GetAuditedPeriod
                    RetTable = DataFunctions.GetAuditedPeriod(CType(inParam, DataFunctions.Param_GetYearPeriod), inBasicParam)
                Case RealServiceFunctions.Data_GetAccountsSubmittedPeriod
                    RetTable = DataFunctions.GetAccountsSubmittedPeriod(CType(inParam, DataFunctions.Param_GetYearPeriod), inBasicParam)

                Case RealServiceFunctions.Core_GetCenterAddress
                    RetTable = CoreFunctions.GetMainCenterAddress(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Core_GetTDSRecords_Common
                    RetTable = CoreFunctions.GetTDSRecords_Common(inBasicParam, CType(inParam, CoreFunctions.Param_GetTDSRecords_Common))
                Case RealServiceFunctions.Core_GetHOEvents
                    RetTable = CoreFunctions.GetHOEvents(CType(inParam, Boolean), inBasicParam)
                Case RealServiceFunctions.Core_GetBankInfo_Common
                    RetTable = CoreFunctions.GetBankInfo_Common(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Core_GetBankBranchesForMultipleIDs
                    RetTable = CoreFunctions.GetBankBranchesForMultipleIDs(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetBankBranchesForMultipleIDsWithCustomColumnNames
                    RetTable = CoreFunctions.GetBankBranchesForMultipleIDs(CType(inParam, CoreFunctions.Param_GetBankBranchesForMultipleIDs), inBasicParam)
                Case RealServiceFunctions.Core_GetBankBranchesByBankID
                    RetTable = CoreFunctions.GetBankBranchesByBankID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetBankBranches
                    RetTable = CoreFunctions.GetBankBranches(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetMisc
                    RetTable = CoreFunctions.GetMisc(CType(inParam, CoreFunctions.Param_GetMisc), inBasicParam)
                Case RealServiceFunctions.Core_GetMiscDetails
                    RetTable = CoreFunctions.GetMiscDetails(CType(inParam, CoreFunctions.Param_GetMiscDetails), inBasicParam)
                Case RealServiceFunctions.Core_GetMiscDetails_Common
                    RetTable = CoreFunctions.GetMiscDetails_Common(inBasicParam, CType(inParam, CoreFunctions.Param_GetMiscDetailsCommon))
                Case RealServiceFunctions.Core_GetMisc_Common
                    RetTable = CoreFunctions.GetMisc_Common(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Core_GetItemsByItemProfile
                    RetTable = CoreFunctions.GetItems(CType(inParam, CoreFunctions.Param_GetItems_ItemProfile), inBasicParam)
                Case RealServiceFunctions.Core_GetItemsAllItems
                    RetTable = CoreFunctions.GetItems(CType(inParam, CoreFunctions.Param_GetItemsAll), inBasicParam)
                Case RealServiceFunctions.Core_GetItemsByMultipleItemIDs
                    RetTable = CoreFunctions.GetItems(CType(inParam, CoreFunctions.Param_GetItemsByMultipleItemIDs), inBasicParam)
                Case RealServiceFunctions.Core_GetItemsByMultipleItemProfiles
                    RetTable = CoreFunctions.GetItems(CType(inParam, CoreFunctions.Param_GetItemsByMultipleItemProfile), inBasicParam)
                Case RealServiceFunctions.Core_GetItemsByQuery_Common
                    RetTable = CoreFunctions.GetItemsByQuery_Common(inBasicParam, CType(inParam, CoreFunctions.Param_GetItemsByQueryCommon))
                Case RealServiceFunctions.Core_GetItems_Ledger
                    RetTable = CoreFunctions.GetItems_Ledger(CType(inParam, CoreFunctions.Param_GetItems_Ledger), inBasicParam)
                Case RealServiceFunctions.Core_GetItems_Ledger_Common
                    RetTable = CoreFunctions.GetItems_Ledger_Common(inBasicParam, CType(inParam, CoreFunctions.Param_GetItemsLedgerCommon))
                Case RealServiceFunctions.Core_GetOpeningProfileItems
                    RetTable = CoreFunctions.GetOpeningProfileItems(CType(inParam, CoreFunctions.Param_GetOpeningProfileItems), inBasicParam)
                Case RealServiceFunctions.Core_GetCurrencyName
                    RetTable = CoreFunctions.GetCurrencyName(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetHQCentersForCurrInstt
                    RetTable = CoreFunctions.GetHQCentersForCurrInstt(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetCenterDetailsForCenID
                    RetTable = CoreFunctions.GetCenterDetailsForCenID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetOrgPasswordForCenID
                    RetTable = CoreFunctions.GetOrgPasswordForCenID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetCenIDForBKPad
                    RetTable = CoreFunctions.GetCenIDForBKPad(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetCentersByBKPAD
                    RetTable = CoreFunctions.GetCentersByBKPAD(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetInstituteListNameAndID
                    RetTable = CoreFunctions.GetInstituteList(CType(inParam, CoreFunctions.Param_GetInstituteList), inBasicParam)
                Case RealServiceFunctions.Core_GetInstituteListNameInShort
                    RetTable = CoreFunctions.GetInstituteList(CType(inParam, CoreFunctions.Param_GetInstituteListNameInShort), inBasicParam)
                Case RealServiceFunctions.Core_GetInstituteDetails
                    RetTable = CoreFunctions.GetInstituteDetails(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetCountriesList
                    RetTable = CoreFunctions.GetCountriesList(CType(inParam, CoreFunctions.Param_GetCountriesList), inBasicParam)
                Case RealServiceFunctions.Core_GetCountriesByID
                    RetTable = CoreFunctions.GetCountriesByID(CType(inParam, CoreFunctions.Param_GetCountriesByID), inBasicParam)
                Case RealServiceFunctions.Core_GetStatesList
                    RetTable = CoreFunctions.GetStatesList(CType(inParam, CoreFunctions.Param_GetStatesList), inBasicParam)
                Case RealServiceFunctions.Core_GetStatesByID
                    RetTable = CoreFunctions.GetStatesByID(CType(inParam, CoreFunctions.Param_GetStatesByID), inBasicParam)
                Case RealServiceFunctions.Core_GetDistrictsList
                    RetTable = CoreFunctions.GetDistrictsList(CType(inParam, CoreFunctions.Param_GetDistrictsList), inBasicParam)
                Case RealServiceFunctions.Core_GetDistrictsByID
                    RetTable = CoreFunctions.GetDistrictsByID(CType(inParam, CoreFunctions.Param_GetDistrictsByID), inBasicParam)
                Case RealServiceFunctions.Core_GetCitiesList
                    RetTable = CoreFunctions.GetCitiesList(CType(inParam, CoreFunctions.Param_GetCitiesListByCountryandstate), inBasicParam)
                Case RealServiceFunctions.Core_GetCitiesListByCountry
                    RetTable = CoreFunctions.GetCitiesList(CType(inParam, CoreFunctions.Param_GetCitiesListByCountry), inBasicParam)
                Case RealServiceFunctions.Core_GetCitiesByID
                    RetTable = CoreFunctions.GetCitiesByID(CType(inParam, CoreFunctions.Param_GetCitiesByID), inBasicParam)
                Case RealServiceFunctions.Core_GetCenterDetailsByQuery_Common
                    RetTable = CoreFunctions.GetCenterDetailsByQuery_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Core_GetItems
                    RetTable = CoreFunctions.GetItems(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Core_GetItemDetails
                    RetTable = CoreFunctions.GetItemDetails(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Core_GetItemTDSCode
                    RetTable = CoreFunctions.GetItemTDSCode(inBasicParam, CType(inParam, String))
                    'COLLECTION BOX
                Case RealServiceFunctions.CollectionBox_CheckUsageAsPastWitness
                    RetTable = Voucher_CollectionBox.CheckUsageAsPastWitness(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CollectionBox_GetDenominations
                    RetTable = Voucher_CollectionBox.GetDenominations(CType(inParam, String), inBasicParam)
                    'Data Functions
                Case RealServiceFunctions.Data_GetOpeningBalance
                    RetTable = DataFunctions.GetOpeningBalance(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Data_GetOpeningBalanceRecIDWithCustomColHeadName
                    RetTable = DataFunctions.GetOpeningBalance(CType(inParam, DataFunctions.Param_GetOpeningBalance), inBasicParam)
                Case RealServiceFunctions.Data_GetBankTransSumAmount_Common
                    RetTable = DataFunctions.GetBankTransSumAmount_Common(inBasicParam, CType(inParam, DataFunctions.Param_GetBankTransSumAmount_Common))
                Case RealServiceFunctions.Data_GetCashBankTransSumAmount
                    RetTable = DataFunctions.GetCashBankTransSumAmount(CType(inParam, Date), inBasicParam)
                Case RealServiceFunctions.Data_GetCashBankTransSumAmountBetweenTwoDates
                    RetTable = DataFunctions.GetCashBankTransSumAmount(CType(inParam, DataFunctions.Param_GetCashBankTransSumAmountTwoDates), inBasicParam)
                Case RealServiceFunctions.Data_GetBankBalanceAmount
                    RetTable = DataFunctions.GetBankBalanceAmount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Data_GetBankBalanceAmountIdWise
                    RetTable = DataFunctions.GetBankBalanceAmountIdWise(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Data_GetOpeningBalance_Common
                    RetTable = DataFunctions.GetOpeningBalance_Common(inBasicParam, CType(inParam, DataFunctions.Param_GetOpeningBalance_Common))

                Case RealServiceFunctions.Donation_GetList
                    RetTable = DonationRegister.GetList(CType(inParam, DonationRegister.Param_DonationRegister_GetList), inBasicParam)
                Case RealServiceFunctions.Data_GetPurpose
                    RetTable = Vouchers.GetPurpose(CType(inParam, CoreFunctions.Param_GetMisc), inBasicParam)
                    'Deposits
                Case RealServiceFunctions.Deposits_GetList
                    RetTable = Deposits.GetList(CType(inParam, Deposits.Param_Deposits_GetList), inBasicParam)
                Case RealServiceFunctions.Deposits_GetListByCondition
                    RetTable = Deposits.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Deposits_GetPaymentsForParties
                    RetTable = Deposits.GetPaymentsForParties(CType(inParam, Deposits.Param_GetPaymentsForParties), inBasicParam)
                Case RealServiceFunctions.Deposits_GetList_Common
                    RetTable = Deposits.GetList_Common(inBasicParam, inParam)
                    'Deposits Slips
                Case RealServiceFunctions.Deposit_Slip_GetList
                    RetTable = Deposit_Slip.GetList(CType(inParam, Deposit_Slip.Param_GetDepositSlipList), inBasicParam)
                    'Donation
                Case RealServiceFunctions.Donation_GetAddressDetail
                    RetTable = DonationRegister.GetAddressDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_GetOfficeAddressDetail
                    RetTable = DonationRegister.GetOfficeAddressDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_GetForeignDonationDetail
                    RetTable = DonationRegister.GetForeignDonationDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_GetAddressDetail_Form
                    RetTable = DonationRegister.GetAddressDetail_Form(CType(inParam, DonationRegister.Param_DonationRegister_GetAddressDetail_Form), inBasicParam)
                Case RealServiceFunctions.Donation_GetRecDetail
                    RetTable = DonationRegister.GetRecDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_CheckUsageAsPastDonor
                    RetTable = Voucher_Donation.CheckUsageAsPastDonor(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_CheckUsageAsPastForeignDonor
                    RetTable = Voucher_Donation.CheckUsageAsPastForeignDonor(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Donation_GetReceiptDetails
                    RetTable = DonationRegister.GetReceiptDetails(CType(inParam, String), inBasicParam)

                    'FDs
                Case RealServiceFunctions.FDs_GetListByCondition
                    RetTable = FD.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_GetList
                    RetTable = FD.GetList(CType(inParam, FD.Param_FDs_GetList), inBasicParam)
                Case RealServiceFunctions.FDs_GetRecIDByAccID
                    RetTable = FD.GetRecIDByAccID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_GetRecord
                    RetTable = Voucher_FD.GetFDRecord(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetPaymentRecords
                    RetTable = Voucher_FD.GetPaymentRecords(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetInterestRecords
                    RetTable = Voucher_FD.GetInterestRecords(CType(inParam, Voucher_FD.Param_VoucherFD_GetInterestRecords), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetFDs
                    RetTable = Voucher_FD.GetFDs(CType(inParam, Voucher_FD.Param_VoucherFD_GetFDs), inBasicParam)

                    'Gift
                Case RealServiceFunctions.Gift_GetTxnItems
                    RetTable = Voucher_Gift.GetTxnItems(CType(inParam, String), inBasicParam)

                    'GoldSilver
                Case RealServiceFunctions.GoldSilver_GetList
                    RetTable = GoldSilver.GetList(CType(inParam, GoldSilver.Param_GoldSilver_GetList), inBasicParam)
                Case RealServiceFunctions.GoldSilver_GetListByCondition
                    RetTable = GoldSilver.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.GoldSilver_GetList_Common
                    RetTable = GoldSilver.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.GoldSilver_GetList_Common_ByRecID
                    RetTable = GoldSilver.GetList_Common_ByRecID(inBasicParam, inParam)
                Case RealServiceFunctions.GoldSilver_GetTransactions
                    RetTable = GoldSilver.GetTransactions(CType(inParam, GoldSilver.Param_GS_GetTransactions), inBasicParam)
                    'InternalTransfer
                Case RealServiceFunctions.InternalTransfer_GetListWithTwoParams
                    RetTable = Voucher_Internal_Transfer.GetList(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetList), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetListWithMultipleParams
                    RetTable = Voucher_Internal_Transfer.GetList(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetListWithMultipleParams), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetUnMatchedList
                    RetTable = Voucher_Internal_Transfer.GetUnMatchedList(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.InternalTransfer_GetFrCenterList
                    RetTable = Voucher_Internal_Transfer.GetFrCenterList(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetFrCenterList), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetToCenterList
                    RetTable = Voucher_Internal_Transfer.GetToCenterList(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetToCenterList), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_Get_Tf_Banks
                    RetTable = Voucher_Internal_Transfer.Get_Tf_Banks(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransferGet_Tf_Banks), inBasicParam)
                    'Asset Transfer
                Case RealServiceFunctions.AssetTransfer_GetFrCenterList
                    RetTable = Voucher_AssetTransfer.GetFrCenterList(CType(inParam, Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetFrCenterList), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetToCenterList
                    RetTable = Voucher_AssetTransfer.GetToCenterList(CType(inParam, Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetToCenterList), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetTxnItems
                    RetTable = Voucher_AssetTransfer.GetTxnItems(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetTxnList_Sale
                    RetTable = Voucher_AssetTransfer.GetTxnList_Sale(CType(inParam, Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetTxnList), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetTxnList_AssetTrf
                    RetTable = Voucher_AssetTransfer.GetTxnList_AssetTrf(CType(inParam, Voucher_AssetTransfer.Param_Voucher_AssetTransfer_GetTxnList), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetAssetTransfers
                    RetTable = Voucher_AssetTransfer.GetAssetTransfers(CType(inParam, Voucher_AssetTransfer.Param_GetAssetTransfers), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetUnMatchedList
                    RetTable = Voucher_AssetTransfer.GetUnMatchedList(CType(inParam, String), inBasicParam)

                    'Journal
                Case RealServiceFunctions.Journal_GetDataRecords
                    RetTable = Voucher_Journal.GetRecordData(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Journal_GetJornalEntryAdjustments
                    RetTable = Voucher_Journal.GetJornalEntryAdjustments(CType(inParam, Voucher_Journal.Parameter_GetJornalEntryAdjustments), inBasicParam)
                Case RealServiceFunctions.Journal_GetCurrentRecAdjustment
                    RetTable = Voucher_Journal.GetGetCurrentRecAdjustment(CType(inParam, Voucher_Journal.Parameter_GetCurrentRecAdjustment), inBasicParam)

                    'Job 
                Case RealServiceFunctions.Jobs_GetRecord
                    RetTable = Jobs.GetRecord(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJobItemEstimates
                    RetTable = Jobs.GetJobItemEstimates(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJobManpowerEstimates
                    RetTable = Jobs.GetJobManpowerEstimates(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJobManpowerUsage
                    RetTable = Jobs.GetJobManpowerUsage(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJobMachineUsage
                    RetTable = Jobs.GetJobMachineUsage(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJobExpensesIncurred
                    RetTable = Jobs.GetJobExpensesIncurred(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_Get_Job_Expenses_For_Mapping
                    RetTable = Jobs.Get_Job_Expenses_For_Mapping(CType(inParam, Integer), inBasicParam)

                    'Liabilities
                Case RealServiceFunctions.Liabilities_GetList
                    RetTable = Liabilities.GetList(CType(inParam, Liabilities.Param_Liabilities_GetList), inBasicParam)
                Case RealServiceFunctions.Liabilities_GetList_Common
                    RetTable = Liabilities.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Liabilities_GetListByCondition
                    RetTable = Liabilities.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Liabilities_GetPaymentsForParties
                    RetTable = Liabilities.GetPaymentsForParties(CType(inParam, Liabilities.Param_GetLiabPaymentsForParties), inBasicParam)
                    'Livestock
                Case RealServiceFunctions.Livestock_GetList
                    RetTable = Livestock.GetList(CType(inParam, Livestock.Param_GetList_Livestock), inBasicParam)
                Case RealServiceFunctions.Livestock_GetList_Common
                    RetTable = Livestock.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Livestock_GetList_Common_ByRecID
                    RetTable = Livestock.GetList_Common_ByRecID(inBasicParam, CType(inParam, Livestock.Param_Livestock_GetList_Common))
                Case RealServiceFunctions.Livestock_GetListByCondition
                    RetTable = Livestock.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Livestock_GetTransactions
                    RetTable = Livestock.GetTransactions(CType(inParam, Livestock.Param_LS_GetTransactions), inBasicParam)

                    'Magazine
                Case RealServiceFunctions.Magazine_GetList
                    RetTable = Magazine.GetList(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_SubcriptionType
                    RetTable = Magazine.GetList_SubscriptionType(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_SubcriptionTypeFee
                    RetTable = Magazine.GetList_SubscriptionTypeFee(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetSubcriptionTypeFee
                    RetTable = Magazine.GetSubscriptionTypeFee(CType(inParam, Magazine.Param_GetFee_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_DispatchType
                    RetTable = Magazine.GetList_DispatchType(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_DispatchTypeCharges
                    RetTable = Magazine.GetList_DispatchTypeCharges(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetDispatchTypeCharges
                    RetTable = Magazine.GetDispatchTypeCharges(CType(inParam, Magazine.Param_GetFee_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_GeetaPathshala
                    RetTable = Magazine.GetList_GeetaPathshala(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_Centres
                    RetTable = Magazine.GetList_Centres(CType(inParam, Magazine.Param_GetList_Magazine), inBasicParam)

                Case RealServiceFunctions.Magazine_GetRecord_MembershipProfile
                    RetTable = Magazine.GetRecord_MembershipProfile(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMembers
                    RetTable = Magazine.GetMembers(CType(inParam, Magazine.Param_GetMembers_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetExistingMembers
                    RetTable = Magazine.GetExistingMembers(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazinesIssues
                    RetTable = Magazine.GetMagazinesIssues(CType(inParam, String), inBasicParam)

                    'Membership
                Case RealServiceFunctions.Membership_GetSubscriptionList
                    RetTable = Membership.GetSubscriptionList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetSubscriptionFee
                    RetTable = Membership.GetSubscriptionFee(CType(inParam, Membership.Param_GetSubscriptionFee_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_CheckUsageAsPastMember
                    RetTable = Membership.CheckUsageAsPastMember(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.Membership_GetLastPeriod
                    RetTable = Membership.GetLastPeriod(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetMasterTransactionList
                    RetTable = Membership.GetMasterTransactionList(CType(inParam, Membership.Param_GetMasterTransactionList_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_GetList
                    RetTable = Membership.GetList(CType(inParam, Membership.Param_GetList_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_GetBalancesList
                    RetTable = Membership.GetBalancesList(CType(inParam, Membership.Param_GetBalancesList), inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_GetList
                    RetTable = Membership_Receipt_Register.GetList(CType(inParam, Membership_Receipt_Register.Param_GetList_Membership_Receipt_Register), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_GetTxnBankPaymentDetail
                    RetTable = Voucher_Membership.GetTxnBankPaymentDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_GetTxnBankPaymentDetail
                    RetTable = Voucher_Membership_Renewal.GetTxnBankPaymentDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_GetPartyDetails
                    RetTable = Voucher_Membership_Renewal.GetPartyDetails(CType(inParam, Voucher_Membership_Renewal.Param_GetPartyDetails_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipConversion_GetPartyDetails
                    RetTable = Voucher_Membership_Conversion.GetPartyDetails(CType(inParam, Voucher_Membership_Conversion.Param_GetPartyDetails_VoucherMembershipConversion), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_GetLastPeriod
                    RetTable = Voucher_Membership_Renewal.GetLastPeriod(CType(inParam, String), inBasicParam)

                    'Notebook
                Case RealServiceFunctions.Notebook_GetList
                    RetTable = Notebook.GetList(CType(inParam, Notebook.Parameter_Notebook_GetList), inBasicParam)
                    'Opening Balances
                Case RealServiceFunctions.OpeningBalances_GetList
                    RetTable = OpeningBalances.GetList(CType(inParam, OpeningBalances.Parameter_OpeningBalances_GetList), inBasicParam)


                    'Payments
                Case RealServiceFunctions.Payments_GetTxnDetailsByRefID
                    RetTable = Payments.GetTxnDetailsByRefID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_GetTxnItemsDetail
                    RetTable = Payments.GetTxnItemsDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_GetTxnBankPaymentDetail
                    RetTable = Payments.GetTxnBankPaymentDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_GetTxnPaymentDetail
                    RetTable = Payments.GetTxnPaymentDetail(CType(inParam, Payments.Param_Payments_GetTxnPaymentDetail), inBasicParam)
                Case RealServiceFunctions.Payments_GetAdvancesPaid
                    RetTable = Payments.GetAdvancesPaid(CType(inParam, Payments.Param_Payments_GetAdvancesPaid), inBasicParam)
                Case RealServiceFunctions.Payments_GetLiabilitiesPaid
                    RetTable = Payments.GetLiabilitiesPaid(CType(inParam, Payments.Param_Payments_GetLiabilitiesPaid), inBasicParam)
                Case RealServiceFunctions.Payments_GetTDS_Deducted_Not_Sent
                    RetTable = Payments.GetTDS_Deducted_Not_Sent(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_GetTDS_Deducted_Not_Paid
                    RetTable = Payments.GetTDS_Deducted_Not_Paid(CType(inParam, String), inBasicParam)

                    'Property
                Case RealServiceFunctions.Property_GetList
                    RetTable = LandAndBuilding.GetList(CType(inParam, LandAndBuilding.Param_LandAndBuilding_GetList), inBasicParam)
                Case RealServiceFunctions.Property_GetAllPropertyList
                    RetTable = LandAndBuilding.GetAllPropertyList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Property_GetListForExpenses
                    RetTable = LandAndBuilding.GetListForExpenses(CType(inParam, LandAndBuilding.Param_LandAndBuilding_GetListForExpenses), inBasicParam)
                Case RealServiceFunctions.Property_GetListForMOU
                    RetTable = LandAndBuilding.GetListForMOU(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Property_GetExtensionDetails
                    RetTable = LandAndBuilding.GetExtensionDetails(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Property_GetDocsDetails
                    RetTable = LandAndBuilding.GetDocsDetails(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Property_GetListByCondition
                    RetTable = LandAndBuilding.GetListByCondition(CType(inParam, LandAndBuilding.Param_LandAndBuilding_GetListByCondition), inBasicParam)
                Case RealServiceFunctions.Property_GetTransactions
                    RetTable = LandAndBuilding.GetTransactions(CType(inParam, LandAndBuilding.Param_Veh_GetTransactions), inBasicParam)
                Case RealServiceFunctions.Property_GetIDsBytxnID
                    RetTable = LandAndBuilding.GetIDsBytxnID(CType(inParam, String), inBasicParam)
                    'Property
                Case RealServiceFunctions.Property_GetListByQuery_Common
                    RetTable = LandAndBuilding.GetListByQuery_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Property_GetRecord
                    RetTable = LandAndBuilding.GetRecord(inBasicParam, CType(inParam, String))
                Case RealServiceFunctions.Property_GetPendingTfs_LocNames
                    RetTable = LandAndBuilding.GetPendingTfs_LocNames(inBasicParam, CType(inParam, String))
                    'Projects 
                Case RealServiceFunctions.Projects_GetRecord
                    RetTable = Projects.GetRecord(CType(inParam, Integer), inBasicParam)
                    'Personells
                Case RealServiceFunctions.Personnels_GetPersonnelCharges
                    RetTable = Personnels.GetPersonnelCharges(CType(inParam, Personnels.Param_GetPersonnelCharges), inBasicParam)
                Case RealServiceFunctions.Personnels_GetPersonnelRecord
                    RetTable = Personnels.GetPersonnelRecord(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.Personnels_GetPersonnelChargesRecord
                    RetTable = Personnels.GetPersonnelChargesRecord(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.Personnels_Get_Personnel_Usage_Period
                    RetTable = Personnels.Get_Personnel_Usage_Period(CType(inParam, Int32), inBasicParam)

                    'Receipts
                Case RealServiceFunctions.Receipts_GetTxnItems
                    RetTable = Receipts.GetTxnItems(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Receipts_GetBankPayments
                    RetTable = Receipts.GetBankPayments(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.Receipts_GetAdvancesRefundList
                    RetTable = Receipts.GetAdvancesRefundList(CType(inParam, Receipts.Param_Receipts_GetAdvancesRefundList), inBasicParam)
                Case RealServiceFunctions.Receipts_GetDepositsRefundList
                    RetTable = Receipts.GetDepositsRefundList(CType(inParam, Receipts.Param_Receipts_GetDepositsRefundList), inBasicParam)
                Case RealServiceFunctions.Receipts_GetLiabilities
                    RetTable = Receipts.GetLiabilities(CType(inParam, String), inBasicParam)

                    'Reports


                Case RealServiceFunctions.Reports_GetLedgerReport
                    RetTable = Reports.GetLedgerReport(CType(inParam, Reports.Param_GelLedgerReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetTransactionDetailsForLedger
                    RetTable = Reports.GetTransactionDetailsForLedger(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Reports_GetItemReport
                    RetTable = Reports.GetItemReport(CType(inParam, Reports.Param_GetItemReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetSecondaryGroupReport
                    RetTable = Reports.GetSecondaryGroupReport(CType(inParam, Reports.Param_GetSecondaryGroupReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetPrimaryGroupReport
                    RetTable = Reports.GetPrimaryGroupReport(CType(inParam, Reports.Param_GetPrimaryGroupReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetPartyListing
                    RetTable = Reports.GetPartyListing(CType(inParam, Reports.Param_GetPartyListing), inBasicParam)
                Case RealServiceFunctions.Reports_GetPartyReport
                    RetTable = Reports.GetPartyReport(CType(inParam, Reports.Param_GetPartyReport), inBasicParam)

                Case RealServiceFunctions.ReportsAll_GetDonationStatusID
                    RetTable = Reports_All.GetDonationStatusID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetTransactionList
                    RetTable = Reports_All.GetTransactionList(CType(inParam, Reports_All.Param_ReportsAll_GetTransactionList), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetGiftTransactionList
                    RetTable = Reports_All.GetGiftTransactionList(CType(inParam, Reports_All.Param_ReportsAll_GetGiftTransactionList), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionList
                    RetTable = Reports_All.GetCollectionBoxTransactionList(CType(inParam, Reports_All.Param_ReportsAll_GetCollectionBoxTransactionList), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionListWithRecID
                    RetTable = Reports_All.GetCollectionBoxTransactionList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetAddresses
                    RetTable = Reports_All.GetAddresses(CType(inParam, Reports_All.Param_ReportsAll_GetAddresses), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetTSummaryList
                    RetTable = Reports_All.GetTSummaryList(CType(inParam, Reports_All.Param_ReportsAll_GetTSummaryList), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetCashBankTransSum
                    RetTable = Reports_All.GetCashBankTransSum(CType(inParam, Date), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetMembershipReceipt
                    RetTable = Reports_All.GetMembershipReceipt(CType(inParam, Reports_All.Param_ReportsAll_GetMembershipReceipt), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetMembershipSubscriptionFee
                    RetTable = Reports_All.GetMembershipSubscriptionFee(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetMembershipReceiptPayment
                    RetTable = Reports_All.GetMembershipReceiptPayment(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetTrialBalance
                    RetTable = Reports_All.GetTrialBalance(CType(inParam, Reports.Param_GetTrial_Balance), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetTelephoneBillDetails
                    RetTable = Reports_All.GetTelephoneBillDetails(CType(inParam, Reports_All.Param_ReportsAll_GetTelephoneBill), inBasicParam)
                Case RealServiceFunctions.Report_GetInsuranceLetterData
                    RetTable = Reports_All.GetInsuranceLetterData(CType(inParam, Reports_All.Param_GetInsuranceLetterData), inBasicParam)
                Case RealServiceFunctions.Report_GetInsurancePolicyDetails
                    RetTable = Reports_All.GetInsurancePolicyDetails(CType(inParam, String), inBasicParam)

                    'SaleOfAssets
                Case RealServiceFunctions.SaleOfAssets_GetTxnItems
                    RetTable = Voucher_SaleOfAsset.GetTxnItems(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_GetBankPayments
                    RetTable = Voucher_SaleOfAsset.GetBankPayments(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_GetTxnList
                    RetTable = Voucher_SaleOfAsset.GetTxnList(CType(inParam, Voucher_SaleOfAsset.Param_Voucher_SaleOfAsset_GetTxnList), inBasicParam)
                    'ServiceReport
                Case RealServiceFunctions.ServiceReport_GetList
                    RetTable = ServiceReport.GetList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ServiceReport_GetWingsRecord
                    RetTable = ServiceReport.GetWingsRecord(CType(inParam, String), inBasicParam)

                    'ServiceMaterial
                Case RealServiceFunctions.ServiceMaterial_GetWingsRecord
                    RetTable = ServiceMaterial.GetWingsRecord(CType(inParam, String), inBasicParam)

                    'Service Places
                Case RealServiceFunctions.ServicePlaces_GetAllServicePlaceList
                    RetTable = ServicePlaces.GetAllServicePlaceList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.ServicePlaces_GetRecord
                    RetTable = ServicePlaces.GetRecord(inBasicParam, CType(inParam, String))
                    'SubItem 
                Case RealServiceFunctions.SubItem_GetSubCategoriesMaster
                    RetTable = SubItem.GetSubCategoriesMaster(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.SubItem_GetPropertiesList_SubItem
                    RetTable = SubItem.GetPropertiesList_SubItem(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.SubItem_GetUnitConversionList_SubItem
                    RetTable = SubItem.GetUnitConversionList_SubItem(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.Suppliers_GetSupplierBanks
                    RetTable = Suppliers.GetSupplierBanks(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.Suppliers_GetSupplierRecord
                    RetTable = Suppliers.GetSupplierRecord(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.Suppliers_GetSupplierUsage
                    RetTable = Suppliers.GetSupplierUsage(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockRequisitionRequest_Get_RR_Usage_Count
                    RetTable = StockRequisitionRequest.Get_RR_Usage_Count(CType(inParam, Int32), inBasicParam)



                Case RealServiceFunctions.StockDeptStores_GetStoresForPersonnel
                    RetTable = StockDeptStores.GetStoresForPersonnel(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockRequisitionRequest_Get_RR_Linked_UO
                    RetTable = StockRequisitionRequest.Get_RR_Linked_UO(CType(inParam, Int32), inBasicParam)
                       'Suppliers
                Case RealServiceFunctions.Suppliers_GetAllSuppliers
                    RetTable = Suppliers.GetAllSuppliers(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.Suppliers_GetItemMappedSuppliers
                    RetTable = Suppliers.GetItemMappedSuppliers(CType(inParam, Int32), inBasicParam)


                    'TDS
                Case RealServiceFunctions.TDS_GetTDS_Deducted_Not_Sent
                    RetTable = TDS.GetTDS_Deducted_Not_Sent(CType(inParam, TDS.Parameter_GetTDS_Deducted_Not_Sent), inBasicParam)
                Case RealServiceFunctions.TDS_GetTDS_Deducted_Not_Paid
                    RetTable = TDS.GetTDS_Deducted_Not_Paid(CType(inParam, TDS.Parameter_GetTDS_Deducted_Not_Sent), inBasicParam)
                    'Vehicels
                Case RealServiceFunctions.Vehicles_GetList
                    RetTable = Vehicles.GetList(CType(inParam, Vehicles.Param_Vehicles_GetList), inBasicParam)
                Case RealServiceFunctions.Vehicles_GetListByCondition
                    RetTable = Vehicles.GetListByCondition(CType(inParam, Vehicles.Param_Vehicles_GetListByCondition), inBasicParam)
                Case RealServiceFunctions.Vehicles_GetList_Common
                    RetTable = Vehicles.GetList_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Vehicles_GetList_Common_ByRecID
                    RetTable = Vehicles.GetList_Common_ByRecID(inBasicParam, CType(inParam, Vehicles.Param_Vehicles_GetList_Common))
                Case RealServiceFunctions.Vehicles_GetRecord
                    RetTable = Vehicles.GetRecord(inBasicParam, CType(inParam, String))
                    'User Order 
                Case RealServiceFunctions.UserOrder_GetRecord
                    RetTable = StockUserOrder.GetRecord(CType(inParam, Integer), inBasicParam)
                    'Vouchers
                Case RealServiceFunctions.Vouchers_GetReferenceTxnRecord
                    RetTable = Vouchers.GetReferenceTxnRecord(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetReferenceTxnRecord_ExcludeM_ID
                    RetTable = Vouchers.GetReferenceTxnRecord_ExcludeM_ID(CType(inParam, Vouchers.Parameter_GetReferenceTxnRecord_MID), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetSaleReferenceRecord
                    RetTable = Vouchers.GetSaleReferenceRecord(CType(inParam, Vouchers.Param_GetSaleReferenceRecord), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetPaymentReferenceRecord
                    RetTable = Vouchers.GetPaymentReferenceRecord(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetAssetItemID
                    RetTable = Vouchers.GetAssetItemID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetPastParties
                    RetTable = Vouchers.GetPastParties(CType(inParam, Vouchers.Param_Vouchers_GetPastParties), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetListWithMultipleParams
                    RetTable = Vouchers.GetListWithMultipleParams(CType(inParam, Vouchers.Param_Vouchers_GetListWithMultipleParams), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetList
                    RetTable = Vouchers.GetList(CType(inParam, Vouchers.Param_Vouchers_GetList), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetNegativeBalance
                    RetTable = Vouchers.GetNegativeBalance(CType(inParam, Vouchers.Param_Vouchers_GetNegativeBalance), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetDonationStatusWithRecID
                    RetTable = Vouchers.GetDonationStatus(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetStatus_TrCode
                    RetTable = Vouchers.GetStatus_TrCode(CType(inParam, Vouchers.Param_GetStatusTrCode), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetStatus_TrCode_OtherCentre
                    RetTable = Vouchers.GetStatus_TrCode_OtherCentre(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetTransactionDetail
                    RetTable = Vouchers.GetTransactionDetail(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetAdjustments
                    RetTable = Vouchers.GetAdjustments(CType(inParam, Vouchers.Parameter_GetAdjustments), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetRefRecordIDS
                    RetTable = Vouchers.GetRefRecordIDS(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetBank_Reconciliation
                    RetTable = Vouchers.GetBank_Reconciliation(CType(inParam, Vouchers.Param_GetBank_Reconciliation), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetBankClearingData
                    RetTable = Vouchers.GetBankClearingData(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetTDS_Mapping
                    RetTable = Vouchers.GetTDS_Mapping(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetVoucherItemDetails
                    RetTable = Vouchers.GetVoucherItemDetails(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetEntryDetails
                    RetTable = Vouchers.GetEntryDetails(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetProfileEntryDetails
                    RetTable = Vouchers.GetProfileEntryDetails(CType(inParam, Vouchers.Param_GetProfileEntryDetails), inBasicParam)

                    'telephone
                Case RealServiceFunctions.Telephone_GetListByCondition
                    RetTable = Telephones.GetListByCondition(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Voucher_WIP_Finalization_GetListOfFinalizedAssets
                    RetTable = Voucher_WIP_Finalization.GetListOfFinalizedAssets(CType(inParam, Voucher_WIP_Finalization.Param_GetListOfFinalizedAssets), inBasicParam)
                    'WIP
                Case RealServiceFunctions.WIP_Profile_GetList_Common
                    RetTable = WIP_Profile.GetList_Common(inBasicParam, inParam)
                    'Restriction
                      'Restrictions 
                Case RealServiceFunctions.Vouchers_GetBankEntriesCountInNextEvent
                    RetTable = Vouchers.GetBankEntriesCountInNextEvent(CType(inParam, DateTime), inBasicParam)

                    'WIP Finalization
                Case RealServiceFunctions.Voucher_WIP_Finalization_GetListOfReferences
                    RetTable = Voucher_WIP_Finalization.GetListOfReferences(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.WIP_Creation_Vouchers_GetWIP_GetRefCreationDateByWIPID
                    RetTable = WIP_Creation_Vouchers.GetRefCreationDateByWIPID(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.Voucher_WIP_Finalization_GetFinalizedAmounts
                    RetTable = Voucher_WIP_Finalization.GetFinalizedAmounts(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.ServiceModule_GetCenters
                    RetTable = ServiceModule.GetCenters(inBasicParam)



            End Select
            Return CompressData(RetTable)
        End Function

        <WebMethod(MessageName:="Wrapper for Get List of Reords from SP Function  With parameters") _
        , XmlInclude(GetType(DataFunctions.Param_Asset_Common)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Parameter_Insert_Profile)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetCashBalanceSummary)) _
        , XmlInclude(GetType(Vouchers.Param_Vouchers_GetBankBalanceSummary)) _
        , XmlInclude(GetType(Voucher_SaleOfAsset.Param_GetAssetListingForSale)) _
        , XmlInclude(GetType(Voucher_Journal.Param_Get_JV_Asset_Listing)) _
        , XmlInclude(GetType(Assets.Param_GetProfileListing)) _
        , XmlInclude(GetType(Voucher_AssetTransfer.Param_Get_AssetTf_Asset_Listing)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetPropertyListingBySP)) _
        , XmlInclude(GetType(Voucher_SaleOfAsset.Param_GetAssetEnclosingTxnDate)) _
        , XmlInclude(GetType(FD.Param_GetFDProfileListing)) _
        , XmlInclude(GetType(Advances.Param_GetAdvProfileListing)) _
        , XmlInclude(GetType(Deposits.Param_GetDepositProfileListing)) _
        , XmlInclude(GetType(Liabilities.Param_GetLiabProfileListing)) _
        , XmlInclude(GetType(Advances.Param_GetReceiptAdvances)) _
        , XmlInclude(GetType(Deposits.Param_GetReceiptDeposits)) _
        , XmlInclude(GetType(Advances.Param_GetPaymentAdvances)) _
        , XmlInclude(GetType(Liabilities.Param_GetPaymentLiabilities)) _
        , XmlInclude(GetType(Vouchers.Param_GetAdvancedFilters)) _
        , XmlInclude(GetType(Complexes.Param_GetBuildingList_Complexes)) _
        , XmlInclude(GetType(Complexes.Param_Get_LB_Listing_Profile)) _
        , XmlInclude(GetType(Center.Param_AddInsuranceVerification)) _
        , XmlInclude(GetType(WIP_Profile.Param_GetTxnReport)) _
        , XmlInclude(GetType(LandAndBuilding.Param_Get_Property_Closing)) _
        , XmlInclude(GetType(Voucher_WIP_Finalization.Param_Get_WIP_Outstanding_References)) _
        , XmlInclude(GetType(Voucher_WIP_Finalization.Param_GetExistingAssetListing)) _
        , XmlInclude(GetType(Reports.Param_AssetInsuranceBreakUpReport)) _
        , XmlInclude(GetType(WIP_Creation_Vouchers.Param_GetWIP_Dependencies)) _
        , XmlInclude(GetType(Reports_All.Param_ReportsAll_GetTrialBalReport)) _
        , XmlInclude(GetType(LandAndBuilding.Param_GetMainCenters)) _
        , XmlInclude(GetType(Voucher_Magazine_Register.Parameter_GetMembers_VoucherMagazine)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_GetPropertyByName)) _
        , XmlInclude(GetType(Magazine.Param_GetList_SubCities)) _
        , XmlInclude(GetType(Magazine.Parameter_Get_Dispatch_Details)) _
        , XmlInclude(GetType(Magazine.Param_GetMapping_SubCities)) _
        , XmlInclude(GetType(Voucher_Magazine_Register.Param_GetPayeeLedger)) _
        , XmlInclude(GetType(Voucher_Magazine_Register.Param_GetMagazineAccLedger)) _
        , XmlInclude(GetType(Reports_All.Param_GetMagazineReceipt)) _
        , XmlInclude(GetType(Center.Param_GetAccountsSubmissionReport)) _
        , XmlInclude(GetType(TDS.Parameter_GetTDSRegister)) _
        , XmlInclude(GetType(Payments.Param_GetCreatedAsset_MinTxnDate)) _
        , XmlInclude(GetType(ScreenAreas)) _
        , XmlInclude(GetType(SubItem.Param_SubItem_GetUsageList)) _
        , XmlInclude(GetType(SubItem.Param_GetStockItems)) _
        , XmlInclude(GetType(SubItem.Param_GetFilteredStockItems)) _
        , XmlInclude(GetType(SubItem.Param_GetStoreItems)) _
        , XmlInclude(GetType(Projects.Param_GetStockDocuments)) _
        , XmlInclude(GetType(Projects.Param_GetStockRemarks)) _
        , XmlInclude(GetType(Projects.Param_GetProjectRegister)) _
        , XmlInclude(GetType(Jobs.Param_GetJobRegister)) _
        , XmlInclude(GetType(StockProfile.Param_Get_Stocks_Listing)) _
        , XmlInclude(GetType(StockUserOrder.Param_GetStoreList_StockAvailability)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetStoreLocations_StockAvailability)) _
        , XmlInclude(GetType(StockDeptStores.Param_GetStoreList)) _
        , XmlInclude(GetType(StockUserOrder.Param_GetUOItems)) _
        , XmlInclude(GetType(StockUserOrder.Param_Get_RR_Details_ForUOmapping)) _
          , XmlInclude(GetType(StockUserOrder.Param_Get_UOItem_ForRR)) _
        , XmlInclude(GetType(Projects.Param_GetStockPersonnels)) _
        , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReceived)) _
          , XmlInclude(GetType(StockUserOrder.Param_GetUORetReceivableItems)) _
       , XmlInclude(GetType(StockUserOrder.Param_GetUODeliveredItems)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReceiveAllPending)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReceiveSelected)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReturnAllPending)) _
         , XmlInclude(GetType(StockUserOrder.Param_Get_Stock_Availability)) _
         , XmlInclude(GetType(StockUserOrder.Param_Get_UO_Goods_Delivery_Stocks)) _
        , XmlInclude(GetType(StockDeptStores.Param_GetStoreDept)) _
        , XmlInclude(GetType(Suppliers.Param_GetItemSupplierMapping)) _
        , XmlInclude(GetType(StockRequisitionRequest.Param_Get_RR_Tax_Detail)) _
        , XmlInclude(GetType(StockPurchaseOrder.Param_GetPOGoodsReceived)) _
        , XmlInclude(GetType(StockPurchaseOrder.Param_Get_PriceHistory)) _
             , XmlInclude(GetType(StockPurchaseOrder.Param_Get_PO_Tax_Detail)) _
        , XmlInclude(GetType(ClientUserInfo.Param_GetPrivilegeRegister)) _
        , XmlInclude(GetType(Audit.Param_GetAdditionalInfo)) _
        , XmlInclude(GetType(LandAndBuilding.Param_LandAndBuilding_Get_Location_Property_ListingBySP))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetListBySP(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Byte()
            Dim retTable As DataTable = Nothing
            Select Case FunctionCalled
                Case RealServiceFunctions.Audit_GetDoc_AdditionalInfo
                    retTable = Audit.GetAdditionalInfo(CType(inParam, Audit.Param_GetAdditionalInfo), inBasicParam)
                'Adances
                Case RealServiceFunctions.Advances_GetProfileListing
                    retTable = Advances.Get_ProfileListing(CType(inParam, Advances.Param_GetAdvProfileListing), inBasicParam)
                Case RealServiceFunctions.Advances_GetPaymentAdvances
                    retTable = Advances.GetPaymentAdvances(CType(inParam, Advances.Param_GetPaymentAdvances), inBasicParam)
                    'Assets 
                Case RealServiceFunctions.Assets_GetProfileListing
                    retTable = Assets.Get_ProfileListing(CType(inParam, Assets.Param_GetProfileListing), inBasicParam)
                    'Asset Transfer
                Case RealServiceFunctions.AssetTransfer_Get_AssetTf_Asset_Listing
                    retTable = Voucher_AssetTransfer.Get_AssetTf_Asset_Listing(CType(inParam, Voucher_AssetTransfer.Param_Get_AssetTf_Asset_Listing), inBasicParam)
                Case RealServiceFunctions.CentreRelated_AddInsuranceVerification
                    retTable = Center.AddInsuranceVerification(CType(inParam, Center.Param_AddInsuranceVerification), inBasicParam)
                Case RealServiceFunctions.CentreRelated_GetAccountsSubmissionReport
                    retTable = Center.GetAccountsSubmissionReport(CType(inParam, Center.Param_GetAccountsSubmissionReport), inBasicParam)
                    'Complexes
                Case RealServiceFunctions.Complexes_GetList
                    retTable = Complexes.GetList(CType(inParam, Complexes.Param_Get_LB_Listing_Profile), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetRegister
                    retTable = ClientUserInfo.GetUserRegister(CType(inParam, Boolean), inBasicParam)

                Case RealServiceFunctions.Complexes_Get_LB_Listing_Profile
                    retTable = Complexes.Get_LB_Listing_Profile(CType(inParam, Complexes.Param_Get_LB_Listing_Profile), inBasicParam)
                Case RealServiceFunctions.Complexes_GetBuildingList
                    retTable = Complexes.GetBuildingList(CType(inParam, Complexes.Param_GetBuildingList_Complexes), inBasicParam)
                Case RealServiceFunctions.Complexes_GetAllComplexBuildings
                    retTable = Complexes.GetAllComplexBuildings(CType(inParam, Complexes.Param_Get_LB_Listing_Profile), inBasicParam)
                Case RealServiceFunctions.Magazine_GetIncome_Breakup
                    retTable = Magazine_Reports.GetIncome_Breakup(CType(inParam, String), inBasicParam)
                    'Case RealServiceFunctions.CentreRelated_Get_CSD_sync_count
                    '   retTable = Center.Get_CSD_sync_count(CType(inParam, Integer), inBasicParam)

                    'Center Related
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetControlVisibility
                    retTable = ClientUserInfo.GetControlVisibility(CType(inParam, ClientScreen), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetPrivilegeRegister
                    retTable = ClientUserInfo.GetPrivilegeRegister(CType(inParam, ClientUserInfo.Param_GetPrivilegeRegister), inBasicParam)

                    'Deposits
                Case RealServiceFunctions.Deposits_GetProfileListing
                    retTable = Deposits.Get_ProfileListing(CType(inParam, Deposits.Param_GetDepositProfileListing), inBasicParam)
                    'FD
                Case RealServiceFunctions.FD_GetProfileListing
                    retTable = FD.Get_ProfileListing(CType(inParam, FD.Param_GetFDProfileListing), inBasicParam)
                    'GoldSilver
                Case RealServiceFunctions.GoldSilver_GetProfileListing
                    retTable = GoldSilver.Get_ProfileListing(CType(inParam, Assets.Param_GetProfileListing), inBasicParam)
                    'Journal
                Case RealServiceFunctions.Journal_Get_JV_Asset_Listing
                    retTable = Voucher_Journal.Get_JV_Asset_Listing(CType(inParam, Voucher_Journal.Param_Get_JV_Asset_Listing), inBasicParam)

                    'Jobs
                Case RealServiceFunctions.Jobs_GetRegister
                    retTable = Jobs.GetRegister(CType(inParam, Jobs.Param_GetJobRegister), inBasicParam)
                Case RealServiceFunctions.Jobs_GetList
                    retTable = Jobs.GetList(CType(inParam, Boolean), inBasicParam)

                    'Liab
                Case RealServiceFunctions.Liabilities_GetProfileListing
                    retTable = Liabilities.Get_ProfileListing(CType(inParam, Liabilities.Param_GetLiabProfileListing), inBasicParam)
                Case RealServiceFunctions.Liabilities_GetPaymentLiabilities
                    retTable = Liabilities.GetPaymentLiabilities(CType(inParam, Liabilities.Param_GetPaymentLiabilities), inBasicParam)
                    'LB
                Case RealServiceFunctions.Property_GetMainCenters
                    retTable = LandAndBuilding.GetMainCenters(CType(inParam, LandAndBuilding.Param_GetMainCenters), inBasicParam)
                Case RealServiceFunctions.Property_GetPropertyByName
                    retTable = LandAndBuilding.GetPropertyByName(CType(inParam, LandAndBuilding.Param_LandAndBuilding_GetPropertyByName), inBasicParam)
                    'Livestock
                Case RealServiceFunctions.Livestock_GetProfileListing
                    retTable = Livestock.Get_ProfileListing(CType(inParam, Assets.Param_GetProfileListing), inBasicParam)
                    'Voucher/Profile Magazine
                Case RealServiceFunctions.Voucher_Magazine_Register_GetMembers
                    retTable = Voucher_Magazine_Register.GetMembers(CType(inParam, Voucher_Magazine_Register.Parameter_GetMembers_VoucherMagazine), inBasicParam)
                Case RealServiceFunctions.Magazine_GetPayeeLedger
                    retTable = Voucher_Magazine_Register.GetPayeeLedger(CType(inParam, Voucher_Magazine_Register.Param_GetPayeeLedger), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineAccLedger
                    retTable = Voucher_Magazine_Register.GetMagazineAccLedger(CType(inParam, Voucher_Magazine_Register.Param_GetMagazineAccLedger), inBasicParam)
                Case RealServiceFunctions.Magazine_GetDues
                    retTable = Magazine.GetList_Magazine_Dues(CType(inParam, String), inBasicParam)
                    'Case RealServiceFunctions.Voucher_Magazine_Register_GetMagazineReceipt
                    '   retTable = Voucher_Magazine_Register.GetPayeeLedger(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_Address_Magazine
                    retTable = Magazine.GetList_Address_Magazine(CType(inParam, String), inBasicParam)
                    'Project
                Case RealServiceFunctions.Projects_GetRegister
                    retTable = Projects.GetRegister(CType(inParam, Projects.Param_GetProjectRegister), inBasicParam)
                'Case RealServiceFunctions.Projects_GetStockSubDept
                '    retTable = Projects.GetStockSubDept(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Projects_GetStockDocuments
                    retTable = Projects.GetStockDocuments(CType(inParam, Projects.Param_GetStockDocuments), inBasicParam)
                Case RealServiceFunctions.Projects_GetProjectEstimates
                    retTable = Projects.GetProjectEstimates(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Projects_GetStockRemarks
                    retTable = Projects.GetStockRemarks(CType(inParam, Projects.Param_GetStockRemarks), inBasicParam)
                Case RealServiceFunctions.Projects_GetStockPersonnel
                    retTable = Projects.GetStockPersonnels(CType(inParam, Projects.Param_GetStockPersonnels), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetPOPayments
                    retTable = StockPurchaseOrder.GetPOPayments(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOPaymentsForMapping
                    retTable = StockPurchaseOrder.GetPOPaymentsForMapping(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOGoodsReceived
                    retTable = StockPurchaseOrder.GetPOGoodsReceived(CType(inParam, StockPurchaseOrder.Param_GetPOGoodsReceived), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOGoodsReturned
                    retTable = StockPurchaseOrder.GetPOGoodsReturned(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_Dept_for_POItem_DestLoc
                    retTable = StockPurchaseOrder.Get_Dept_for_POItem_DestLoc(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_PriceHistory
                    retTable = StockPurchaseOrder.Get_PriceHistory(CType(inParam, StockPurchaseOrder.Param_Get_PriceHistory), inBasicParam)
                    'Property
                Case RealServiceFunctions.Property_Get_PropertyListingBySP
                    retTable = LandAndBuilding.Get_PropertyListingBySP(CType(inParam, LandAndBuilding.Param_LandAndBuilding_GetPropertyListingBySP), inBasicParam)
                Case RealServiceFunctions.Property_GetProfileListing
                    retTable = LandAndBuilding.Get_ProfileListing(CType(inParam, Assets.Param_GetProfileListing), inBasicParam)
                Case RealServiceFunctions.Property_Get_Location_Property_ListingBySP
                    retTable = LandAndBuilding.Get_Location_Property_ListingBySP(CType(inParam, LandAndBuilding.Param_LandAndBuilding_Get_Location_Property_ListingBySP), inBasicParam)
                Case RealServiceFunctions.Property_Get_Property_Closing
                    retTable = LandAndBuilding.Get_Property_Closing(CType(inParam, LandAndBuilding.Param_Get_Property_Closing), inBasicParam)
                    'Receipts 
                Case RealServiceFunctions.Receipts_GetAdvances
                    retTable = Advances.GetReceiptAdvances(CType(inParam, Advances.Param_GetReceiptAdvances), inBasicParam)
                Case RealServiceFunctions.Receipts_GetDeposits
                    retTable = Deposits.GetReceiptDeposits(CType(inParam, Deposits.Param_GetReceiptDeposits), inBasicParam)
                    'stock
                Case RealServiceFunctions.SubItem_GetUsageList
                    retTable = SubItem.GetUsageList(CType(inParam, SubItem.Param_SubItem_GetUsageList), inBasicParam)
                Case RealServiceFunctions.StockProfile_GetProfiledata
                    retTable = StockProfile.GetProfiledata(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.StockProfile_Get_Stocks_Listing
                    retTable = StockProfile.Get_Stocks_Listing(CType(inParam, StockProfile.Param_Get_Stocks_Listing), inBasicParam)
                Case RealServiceFunctions.StockProfile_GetStockUsage
                    retTable = StockProfile.GetStockUsage(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_GetStorePremesis
                    retTable = StockDeptStores.GetStoreDeptPremesis(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_Get_StoreDept_Detail
                    retTable = StockDeptStores.Get_StoreDept_Detail(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockDeptStores_Get_MainSubDept_Personnels
                    retTable = StockDeptStores.Get_MainSubDept_Personnels(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Detail
                    retTable = StockPurchaseOrder.Get_PO_Detail(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetItemsPriceHistory
                    retTable = StockPurchaseOrder.GetItemsPriceHistory(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Tax_Detail
                    retTable = StockPurchaseOrder.Get_PO_Tax_Detail(CType(inParam, StockPurchaseOrder.Param_Get_PO_Tax_Detail), inBasicParam)

                Case RealServiceFunctions.StockTransferOrders_GetTransferOrders
                    retTable = StockTransferOrders.GetTransferOrders(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetDeliveries
                    retTable = StockUserOrder.GetDeliveries(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_Get_RR_Details_ForUOmapping
                    retTable = StockUserOrder.Get_RR_Details_ForUOmapping(CType(inParam, StockUserOrder.Param_Get_RR_Details_ForUOmapping), inBasicParam)
                Case RealServiceFunctions.UserOrder_Get_UO_Items_Detail_For_RR
                    retTable = StockUserOrder.Get_UO_Items_Detail_For_RR(CType(inParam, StockUserOrder.Param_Get_UOItem_ForRR), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOItems
                    retTable = StockUserOrder.GetUOItems(CType(inParam, StockUserOrder.Param_GetUOItems), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUODetails
                    retTable = StockUserOrder.GetUODetails(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOGoodsReceived
                    retTable = StockUserOrder.GetUOGoodsReceived(CType(inParam, StockUserOrder.Param_GetUOGoodsReceived), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOGoodsReturned
                    retTable = StockUserOrder.GetUOGoodsReturned(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUORetReceivableItems
                    retTable = StockUserOrder.GetUORetReceivableItems(CType(inParam, StockUserOrder.Param_GetUORetReceivableItems), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOScrapCreated
                    retTable = StockUserOrder.GetUOScrapCreated(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOGoodsReturnReceived
                    retTable = StockUserOrder.GetUOGoodsReturnReceived(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.UserOrder_Get_UODeliveredItems
                    retTable = StockUserOrder.Get_UO_Delivered_Items(CType(inParam, StockUserOrder.Param_GetUODeliveredItems), inBasicParam)

                Case RealServiceFunctions.UserOrder_GetUOGoodsReceiveAllPending
                    retTable = StockUserOrder.GetUOGoodsReceiveAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsReceiveAllPending), inBasicParam)

                Case RealServiceFunctions.UserOrder_GetUOGoodsReceiveSelectedItems
                    retTable = StockUserOrder.GetUOGoodsReceiveSelectedItems(CType(inParam, StockUserOrder.Param_GetUOGoodsReceiveSelected), inBasicParam)


                Case RealServiceFunctions.UserOrder_GetUOGoodsReturnAllPending
                    retTable = StockUserOrder.GetUOGoodsReturnAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsReturnAllPending), inBasicParam)

                Case RealServiceFunctions.UserOrder_Get_Stock_Availability
                    retTable = StockUserOrder.Get_Stock_Availability(CType(inParam, StockUserOrder.Param_Get_Stock_Availability), inBasicParam)

                Case RealServiceFunctions.UserOrder_Get_UO_Goods_Delivery_Stocks
                    retTable = StockUserOrder.Get_UO_Goods_Delivery_Stocks(CType(inParam, StockUserOrder.Param_Get_UO_Goods_Delivery_Stocks), inBasicParam)

                Case RealServiceFunctions.StockDeptStores_GetStoreList
                    retTable = StockDeptStores.GetStoreList(CType(inParam, StockDeptStores.Param_GetStoreList), inBasicParam)

                Case RealServiceFunctions.StockDeptStores_GetStoreList_SA
                    retTable = StockUserOrder.GetStoreList_StockAvailability(CType(inParam, StockUserOrder.Param_GetStoreList_StockAvailability), inBasicParam)

                Case RealServiceFunctions.UserOrder_Get_Store_Locations_StockAvailability
                    retTable = StockUserOrder.Get_Store_Locations_StockAvailability(CType(inParam, StockUserOrder.Param_GetStoreLocations_StockAvailability), inBasicParam)

                Case RealServiceFunctions.StockDeptStores_GetStoreDept
                    retTable = StockDeptStores.GetStoreDept(CType(inParam, StockDeptStores.Param_GetStoreDept), inBasicParam)

                Case RealServiceFunctions.StockProduction_GetProductions
                    retTable = StockProduction.GetProductions(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdItemsConsumed
                    retTable = StockProduction.GetProdItemsConsumed(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdExpensesIncurred
                    retTable = StockProduction.GetProdExpensesIncurred(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdManpowerUsage
                    retTable = StockProduction.GetProdManpowerUsage(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdMachineUsage
                    retTable = StockProduction.GetProdMachineUsage(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdItemProduced
                    retTable = StockProduction.GetProdItemProduced(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdScrapProduced
                    retTable = StockProduction.GetProdScrapProduced(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_Get_Prod_Expenses_For_Mapping
                    retTable = StockProduction.Get_Prod_Expenses_For_Mapping(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockProduction_GetProdDetails
                    retTable = StockProduction.GetProdDetails(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockRequisitionRequest_Get_RR_Detail
                    retTable = StockRequisitionRequest.Get_RR_Detail(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockRequisitionRequest_Get_RR_Tax_Detail
                    retTable = StockRequisitionRequest.Get_RR_Tax_Detail(CType(inParam, StockRequisitionRequest.Param_Get_RR_Tax_Detail), inBasicParam)


                Case RealServiceFunctions.Suppliers_GetItemSupplierMapping
                    retTable = Suppliers.GetItemSupplierMapping(CType(inParam, Suppliers.Param_GetItemSupplierMapping), inBasicParam)

                    'Vehicle
                Case RealServiceFunctions.Vehicles_GetProfileListing
                    retTable = Vehicles.Get_ProfileListing(CType(inParam, Assets.Param_GetProfileListing), inBasicParam)
                    'Vouchers
                Case RealServiceFunctions.Vouchers_GetCashBalanceSummary
                    retTable = Vouchers.GetCashBalanceSummary(CType(inParam, Vouchers.Param_Vouchers_GetCashBalanceSummary), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetBankBalanceSummary
                    retTable = Vouchers.GetBankBalanceSummary(CType(inParam, Vouchers.Param_Vouchers_GetBankBalanceSummary), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetAdvancedFilters
                    retTable = Vouchers.GetAdvancedFilters(CType(inParam, Vouchers.Param_GetAdvancedFilters), inBasicParam)

                    'Stock of Consumbles
                Case RealServiceFunctions.Consumables_GetList_Summary
                    retTable = Consumables.GetList_Summary(CType(inParam, Consumables.Param_GetSummary), inBasicParam)
                    'Case RealServiceFunctions.Data_Get_Asset_Item_Closing_Detail
                    '    RetTable =  DataFunctions.Get_Asset_Item_Closing_Detail(CType(inParam, DataFunctions.Param_Asset_Common), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_Insert_Profiles
                    retTable = Voucher_AssetTransfer.Insert_ProfileBySP(CType(inParam, Voucher_AssetTransfer.Parameter_Insert_Profile), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetTrialBalanceRaport
                    retTable = Reports_All.GetTrialBalanceReport(CType(inParam, Reports_All.Param_ReportsAll_GetTrialBalReport), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetIncomeExpenditureReport
                    retTable = Reports_All.GetIncomeExpenditureReport(CType(inParam, Reports_All.Param_ReportsAll_GetTrialBalReport), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetBalanceSheetReport
                    retTable = Reports_All.GetBalanceSheetReport(CType(inParam, Reports_All.Param_ReportsAll_GetTrialBalReport), inBasicParam)
                    'Sale of Assets 
                Case RealServiceFunctions.SaleOfAssets_AssetsListingForSale
                    retTable = Voucher_SaleOfAsset.Get_AssetsListingForSale(CType(inParam, Voucher_SaleOfAsset.Param_GetAssetListingForSale), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_EnclosingTxnDate
                    retTable = Voucher_SaleOfAsset.Get_AssetsEnclosingTxnDate(CType(inParam, Voucher_SaleOfAsset.Param_GetAssetEnclosingTxnDate), inBasicParam)
                Case RealServiceFunctions.Reports_GetAssetInsuranceBreakUpReport
                    retTable = Reports.GetAssetInsuranceBreakUpReport(CType(inParam, Reports.Param_AssetInsuranceBreakUpReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetConsumableBreakUpReport
                    retTable = Reports.GetConsumableBreakUpReport(CType(inParam, String), inBasicParam)
                    'Sub Items 
                Case RealServiceFunctions.StockProfile_GetStockItems
                    retTable = SubItem.GetStockItems(CType(inParam, SubItem.Param_GetStockItems), inBasicParam)

                Case RealServiceFunctions.StockProfile_GetFilteredStockItems
                    retTable = SubItem.GetFilteredStockItems(CType(inParam, SubItem.Param_GetFilteredStockItems), inBasicParam)
                    'Reports
                Case RealServiceFunctions.ReportsAll_GetConstructionList
                    retTable = Reports_All.GetConstructionExpensesList(CType(inParam, Reports_All.Param_ReportsAll_GetConstructionExpensesList), inBasicParam)
                Case RealServiceFunctions.Reports_GetWIPReport
                    retTable = Reports_All.GetWIPReport(CType(inParam, Reports_All.Param_ReportsAll_GetConstructionExpensesList), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetMagazineReceipt
                    retTable = Reports_All.GetMagazineReceipt(CType(inParam, Reports_All.Param_GetMagazineReceipt), inBasicParam)
                    'Payment 
                Case RealServiceFunctions.Payments_GetPendingTDSList
                    retTable = Payments.GetPendingTDSList(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_Get_CreatedAssets_MinTxnDate
                    retTable = Payments.Get_CreatedAssets_MinTxnDate(CType(inParam, Payments.Param_GetCreatedAsset_MinTxnDate), inBasicParam)
                Case RealServiceFunctions.SubItem_GetList
                    retTable = SubItem.GetList(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.SubItem_GetStoreItems
                    retTable = SubItem.GetStoreItems(CType(inParam, SubItem.Param_GetStoreItems), inBasicParam)

                Case RealServiceFunctions.StockMachineToolAllocation_GetPendingReturns
                    retTable = StockMachineToolAllocation.GetPendingReturns(CType(inParam, Int32), inBasicParam)

                    'TDS 
                Case RealServiceFunctions.TDS_GetTDSRegister
                    retTable = TDS.GetTDSRegister(CType(inParam, TDS.Parameter_GetTDSRegister), inBasicParam)

                    'WIP Profile
                Case RealServiceFunctions.WIP_Profile_GetProfileListing
                    retTable = WIP_Profile.GetProfileListing(CType(inParam, WIP_Profile.Param_GetProfileListing_WIP), inBasicParam)
                Case RealServiceFunctions.WIP_Profile_GetTxn_Report
                    retTable = WIP_Profile.GetTxn_Report(CType(inParam, WIP_Profile.Param_GetTxnReport), inBasicParam)
                    'wip finalization
                Case RealServiceFunctions.Voucher_WIP_Finalization_GetExistingAssetListing
                    retTable = Voucher_WIP_Finalization.GetExistingAssetsListing(CType(inParam, Voucher_WIP_Finalization.Param_GetExistingAssetListing), inBasicParam)
                Case RealServiceFunctions.Voucher_WIP_Finalization_Get_WIP_Outstanding_References
                    retTable = Voucher_WIP_Finalization.Get_WIP_Outstanding_References(CType(inParam, Voucher_WIP_Finalization.Param_Get_WIP_Outstanding_References), inBasicParam)
                Case RealServiceFunctions.WIP_Creation_Vouchers_GetWIP_Dependencies
                    retTable = WIP_Creation_Vouchers.GetWIP_Dependencies(CType(inParam, WIP_Creation_Vouchers.Param_GetWIP_Dependencies), inBasicParam)

                Case RealServiceFunctions.Magazine_GetList_Membership
                    retTable = Magazine.GetList_Membership(CType(inParam, Magazine.Param_Get_MagazineMembershipList), inBasicParam)

                Case RealServiceFunctions.Magazine_GetList_ConnectedMembership
                    retTable = Magazine.GetList_ConnectedMembership(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_Dispatches
                    retTable = Magazine.GetList_Dispatches(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_Issues
                    retTable = Magazine.GetList_Issues(inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_SubCities
                    retTable = Magazine.GetList_SubCities(CType(inParam, Magazine.Param_GetList_SubCities), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMapping_SubCities
                    retTable = Magazine.GetMapping_SubCities(CType(inParam, Magazine.Param_GetMapping_SubCities), inBasicParam)

                Case RealServiceFunctions.Magazine_Get_Dispatch_Details
                    retTable = Magazine.Get_Dispatch_Details(CType(inParam, Magazine.Parameter_Get_Dispatch_Details), inBasicParam)

                Case RealServiceFunctions.Membership_GetSubscritonList_Master
                    retTable = Membership.GetSubscritonList_Master(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.Memebership_GetSubscritonFeeList_Master
                    retTable = Membership.GetSubscritonFeeList_Master(CType(inParam, String), inBasicParam)
            End Select
            Return CompressData(retTable)
        End Function

        <WebMethod(MessageName:="Wrapper for Get Single Value from SP Function  With parameters") _
         , XmlInclude(GetType(Deposit_Slip.Param_GetSlipTxnCount)) _
         , XmlInclude(GetType(StockApprovalRequired.Param_Get_Approval_Required)) _
         , XmlInclude(GetType(Suppliers.Param_GetSupplierBankAccUsageCount)) _
         , XmlInclude(GetType(Attachments.Parameter_GetAttachmentLinkCount)) _
         , XmlInclude(GetType(Suppliers.Param_GetSupplier_Party_Duplication_Check)) _
         , XmlInclude(GetType(Addresses.Param_MergeParties)) _
        , XmlInclude(GetType(Voucher_SaleOfAsset.Param_GetAssetMaxTxnDate))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetScalarBySP(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Object
            Select Case FunctionCalled
                'Sale of Assets 
                Case RealServiceFunctions.SaleOfAssets_MaxTxnDate
                    Return Voucher_SaleOfAsset.Get_AssetsMaxTxnDate(CType(inParam, Voucher_SaleOfAsset.Param_GetAssetMaxTxnDate), inBasicParam)
                Case RealServiceFunctions.Assets_Get_Asset_Ref_MaxEditOn
                    Return Assets.Get_Asset_Ref_MaxEditOn(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Assets_GetRecId_From_OrgID
                    Return Assets.GetRecId_From_OrgID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Addresses_MergeParties
                    Return Addresses.MergeParties(CType(inParam, Addresses.Param_MergeParties), inBasicParam)
                    'Attachment 
                Case RealServiceFunctions.Attachments_GetAttachmentLinkCount
                    Return Attachments.GetAttachmentLinkCount(CType(inParam, Attachments.Parameter_GetAttachmentLinkCount), inBasicParam)
                    'Center 
                Case RealServiceFunctions.CentreRelated_Center_GetlatestCenterGrade
                    Return Center.GetlatestCenterGrade(CType(inParam, Integer), inBasicParam)




                Case RealServiceFunctions.Deposit_Slip_GetMaxOpenSlipNo
                    Return Deposit_Slip.GetMaxOpenSlipNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Deposit_Slip_GetSlipTxnCount
                    Return Deposit_Slip.GetSlipTxnCount(CType(inParam, Deposit_Slip.Param_GetSlipTxnCount), inBasicParam)
                    'StockProfile
                'Case RealServiceFunctions.StockProfile_IsStockCarriedForward
                '    Return StockProfile.IsStockCarriedForward(CType(inParam, Integer), inBasicParam)
                 'User Order 
                Case RealServiceFunctions.UserOrder_GetUO_DependentEntry_Count
                    Return StockUserOrder.GetUO_DependentEntry_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUO_Deliveries_notReturned_Count
                    Return StockUserOrder.GetUO_Deliveries_notReturned_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUO_Goods_In_Transit
                    Return StockUserOrder.GetUO_Goods_In_Transit(CType(inParam, Integer), inBasicParam)

                Case RealServiceFunctions.StockApprovalRequired_Get_Approval_Required
                    Return StockApprovalRequired.Get_Approval_Required(CType(inParam, StockApprovalRequired.Param_Get_Approval_Required), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_Get_CentralStorespecific_usage_count
                    Return StockDeptStores.Get_CentralStorespecific_usage_count(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.Personnels_Get_StockPersonnel_Usage_Count
                    Return Personnels.Get_StockPersonnel_Usage_Count(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.Suppliers_GetSupplierBankAccUsageCount
                    Return Suppliers.GetSupplierBankAccUsageCount(CType(inParam, Suppliers.Param_GetSupplierBankAccUsageCount), inBasicParam)
                Case RealServiceFunctions.Suppliers_GetSupplier_Party_Duplication_Check
                    Return Suppliers.GetSupplier_Party_Duplication_Check(CType(inParam, Suppliers.Param_GetSupplier_Party_Duplication_Check), inBasicParam)

                Case RealServiceFunctions.Vouchers_GetDraftEntryCount
                    Return Vouchers.GetDraftEntryCount(inBasicParam)
                Case RealServiceFunctions.Data_GetDynamicClientRestriction
                    Return DataFunctions.GetDynamicClientRestriction(inBasicParam)

                Case Else
                    Return Nothing
            End Select
        End Function

        <WebMethod(MessageName:="Wrapper for Get List of Reords from SP Function  Without parameters")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetListBySP(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param) As Byte()
            Dim retTable As DataTable = Nothing
            Select Case FunctionCalled





                Case RealServiceFunctions.Magazine_GetCancelledReceipts
                    retTable = Voucher_Magazine_Register.GetCancelledReceipts(inBasicParam)

                Case RealServiceFunctions.Magazine_GetParty_Balances
                    retTable = Magazine_Reports.GetParty_Balances(inBasicParam)
                Case RealServiceFunctions.Magazine_GetOpeningAccountingBalances_Bkp
                    retTable = Magazine_Reports.GetOpeningAccountingBalances_Bkp(inBasicParam)
                Case RealServiceFunctions.Magazine_Receivables_Ledger
                    retTable = Magazine_Reports.GetMagazine_Receivables_Ledger(inBasicParam)
                Case RealServiceFunctions.Magazine_Advances_Ledger
                    retTable = Magazine_Reports.GetMagazine_Advances_Ledger(inBasicParam)

                Case RealServiceFunctions.Magazine_GetList_MembershipRequests
                    retTable = Magazine_Request_Register.GetList_Requests(inBasicParam)

                Case RealServiceFunctions.Personnels_GetRequestorList
                    retTable = Personnels.GetRequestorList(inBasicParam)
                'Project
                Case RealServiceFunctions.Projects_GetList
                    retTable = Projects.GetList(inBasicParam)

                Case RealServiceFunctions.Projects_GetOpenProjectList
                    retTable = Projects.GetOpenProjectsList(inBasicParam)
                'Case RealServiceFunctions.Projects_GetStockMainDept
                '    retTable = Projects.GetStockMainDept(inBasicParam)
                Case RealServiceFunctions.Projects_GetStockPersonnel
                    retTable = Projects.GetStockPersonnels(inBasicParam)


                Case RealServiceFunctions.StockDeptStores_GetRegister
                    retTable = StockDeptStores.GetRegister(inBasicParam)

                Case RealServiceFunctions.StockDeptStores_Get_MainSubDept_Personnels
                    retTable = StockDeptStores.Get_MainSubDept_Personnels(Nothing, inBasicParam)

                Case RealServiceFunctions.StockDeptStores_GetStoreList
                    retTable = StockDeptStores.GetStoreList(inBasicParam)

                Case RealServiceFunctions.StockProfile_GetProfiledata
                    retTable = StockProfile.GetProfiledata(inBasicParam)

                Case RealServiceFunctions.StockProfile_GetStockItems
                    retTable = StockProfile.GetStockItems(inBasicParam)
                Case RealServiceFunctions.SubItem_GetList
                    retTable = SubItem.GetList(Nothing, inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetItemsPriceHistory
                    retTable = StockPurchaseOrder.GetItemsPriceHistory(Nothing, inBasicParam)

                Case RealServiceFunctions.Complexes_GetInstList
                    retTable = Complexes.GetInstList(inBasicParam)


                Case RealServiceFunctions.News_GetCenterNews
                    retTable = News.GetCenterNews(inBasicParam)
                Case RealServiceFunctions.Vouchers_GetDraftEntryList
                    retTable = Vouchers.GetDraftEntryList(inBasicParam)
            End Select
            Return CompressData(retTable)
        End Function

        <WebMethod(MessageName:="Wrapper for Get List of Reords as dataset from SP Function  With parameters") _
       , XmlInclude(GetType(Reports.Param_GetVehicleReport)) _
       , XmlInclude(GetType(Reports.Param_GetAssetReport)) _
       , XmlInclude(GetType(Reports.Param_GetLBReport)) _
       , XmlInclude(GetType(Reports.Param_GetFDReport)) _
       , XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount)) _
       , XmlInclude(GetType(Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount)) _
       , XmlInclude(GetType(Reports.Param_GetInsurancePropertyList)) _
        , XmlInclude(GetType(Voucher_Magazine_Register.Parameter_GetVoucherDetails_OnMemberSelection)) _
        , XmlInclude(GetType(Magazine.Param_Get_MagazineMembershipList)) _
        , XmlInclude(GetType(Magazine.Parameter_GetList_MagazineDispatchRegister)) _
        , XmlInclude(GetType(Deposit_Slip.Param_Get_Deposit_slip_All_Report)) _
        , XmlInclude(GetType(Center.Param_get_Client_Audit_Info)) _
        , XmlInclude(GetType(StockUserOrder.Param_GetUserOrderRegister)) _
        , XmlInclude(GetType(StockRequisitionRequest.Param_GetRequisitionRequestRegister)) _
       , XmlInclude(GetType(StockRequisitionRequest.Param_Get_RR_Items)) _
        , XmlInclude(GetType(StockPurchaseOrder.Param_GetPurcahseOrderRegister)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsDeliverAllPending)) _
         , XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsDeliverSelected)) _
        , XmlInclude(GetType(StockMachineToolAllocation.Param_GetMachineToolRegister)) _
        , XmlInclude(GetType(StockProduction.Param_GetProductionRegister)) _
        , XmlInclude(GetType(StockPurchaseOrder.Param_GetPOItemsOrdered)) _
        , XmlInclude(GetType(Vouchers.Param_GetVouchingAuditData)) _
       , XmlInclude(GetType(Reports.Param_GetGSReport))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetListDatasetBySP(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Byte()
            Dim retDataset As DataSet = Nothing
            Select Case FunctionCalled
                Case RealServiceFunctions.CentreRelated_Center_Get_Base_OpenEventId
                    retDataset = Center.Get_Base_OpenEventId(inBasicParam)
                    'Report
                Case RealServiceFunctions.Reports_GetMagSubDispatchReport
                    retDataset = Reports.GetMagSubDispatchReport(CType(inParam, Reports.Param_MagSubDispatchReport), inBasicParam)
                'Audit
                Case RealServiceFunctions.Audit_GetExceptions
                    retDataset = DataFunctions.GetAuditExceptionList(inBasicParam, CType(inParam, Boolean))
                Case RealServiceFunctions.Audit_GetDocumentMapping
                    retDataset = Audit.GetDocumentMapping(CType(inParam, Audit.Param_GetDocumentMapping), inBasicParam)
                Case RealServiceFunctions.Deposit_Slip_GetSlipReport
                    retDataset = Deposit_Slip.GetSlipReport(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Deposit_Slip_GetSlipReportAll
                    retDataset = Deposit_Slip.GetSlipAllReport(CType(inParam, Deposit_Slip.Param_Get_Deposit_slip_All_Report), inBasicParam)
                    'Center user
                Case RealServiceFunctions.CentreRelated_ClientGroupInfo_GetGroupRegister
                    retDataset = ClientUserInfo.GetGroupRegister(inBasicParam)

                    'Vouchers
                Case RealServiceFunctions.Reports_GetVehicleReportData
                    retDataset = Reports.GetVehicleReport(CType(inParam, Reports.Param_GetVehicleReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetAssetReportData
                    retDataset = Reports.GetAssetReport(CType(inParam, Reports.Param_GetAssetReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetLBReportData
                    retDataset = Reports.GetLBReport(CType(inParam, Reports.Param_GetLBReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetGSReportData
                    retDataset = Reports.GetGSReport(CType(inParam, Reports.Param_GetGSReport), inBasicParam)
                Case RealServiceFunctions.Reports_GetFDReportData
                    retDataset = Reports.GetFDReport(CType(inParam, Reports.Param_GetFDReport), inBasicParam)
                Case RealServiceFunctions.SubItem_GetRecord
                    retDataset = SubItem.GetRecord(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetUnMatchedList_LimitedCount
                    retDataset = Voucher_Internal_Transfer.GetUnMatchedList_LimitedCount(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetUnMatchedList_LimitedCount
                    retDataset = Voucher_AssetTransfer.GetUnMatchedList_LimitedCount(CType(inParam, Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnMatchedList_LimitedCount), inBasicParam)
                Case RealServiceFunctions.Reports_GetInsurancePropertyList
                    retDataset = Reports.GetInsurancePropertyList(CType(inParam, Reports.Param_GetInsurancePropertyList), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetListWithMultipleParams_SP
                    retDataset = Vouchers.GetListWithMultipleParams_SP(CType(inParam, Vouchers.Param_Vouchers_GetListWithMultipleParams_SP), inBasicParam)
                Case RealServiceFunctions.ReportsAll_GetConstructionWIPExpensesList
                    retDataset = Reports_All.GetConstructionWIPExpensesList(CType(inParam, Reports_All.Param_ReportsAll_GetConstructionExpensesList), inBasicParam)
                Case RealServiceFunctions.Voucher_Magazine_Register_GetVoucherDetailsOnMemberSelection
                    retDataset = Voucher_Magazine_Register.GetVoucherDetails_OnMemberSelection(CType(inParam, Voucher_Magazine_Register.Parameter_GetVoucherDetails_OnMemberSelection), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_RelatedMembership
                    retDataset = Magazine.GetList_RelatedMembership(CType(inParam, Magazine.Param_Get_MagazineMembershipList), inBasicParam)
                Case RealServiceFunctions.Magazine_GetList_ReceiptRegister
                    retDataset = Magazine.GetList_ReceiptRegister(CType(inParam, Magazine.Param_get_MagazineMembershipRegister), inBasicParam)

                Case RealServiceFunctions.Magazine_GetList_MagazineDispatchRegister
                    retDataset = Magazine.GetList_MagazineDispatchRegister(CType(inParam, Magazine.Parameter_GetList_MagazineDispatchRegister), inBasicParam)

                Case RealServiceFunctions.CentreRelated_get_Client_Audit_Info
                    retDataset = Center.get_Client_Audit_Info(CType(inParam, Center.Param_get_Client_Audit_Info), inBasicParam)
                Case RealServiceFunctions.Data_GetClientAuthorizations
                    retDataset = DataFunctions.GetClientAuthorizations(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.UserOrder_GetRegister
                    retDataset = StockUserOrder.GetRegister(CType(inParam, StockUserOrder.Param_GetUserOrderRegister), inBasicParam)

                Case RealServiceFunctions.UserOrder_GetUOGoodsDelivered
                    retDataset = StockUserOrder.GetUOGoodsDelivered(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOGoodsDeliverAllPending
                    retDataset = StockUserOrder.GetUOGoodsDeliverAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsDeliverAllPending), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOGoodsDeliverSelectedItems
                    retDataset = StockUserOrder.GetUOGoodsDeliverSelectedItems(CType(inParam, StockUserOrder.Param_GetUOGoodsDeliverSelected), inBasicParam)

                Case RealServiceFunctions.StockRequisitionRequest_GetRegister
                    retDataset = StockRequisitionRequest.GetRegister(CType(inParam, StockRequisitionRequest.Param_GetRequisitionRequestRegister), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetRegister
                    retDataset = StockPurchaseOrder.GetRegister(CType(inParam, StockPurchaseOrder.Param_GetPurcahseOrderRegister), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOLinkedUserOrders
                    retDataset = StockPurchaseOrder.GetPOLinkedUserOrders(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOLinkedRequisitions
                    retDataset = StockPurchaseOrder.GetPOLinkedRequisitions(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_GetPOItemsOrdered
                    retDataset = StockPurchaseOrder.GetPOItemsOrdered(CType(inParam, StockPurchaseOrder.Param_GetPOItemsOrdered), inBasicParam)


                Case RealServiceFunctions.StockProduction_GetRegister
                    retDataset = StockProduction.GetRegister(CType(inParam, StockProduction.Param_GetProductionRegister), inBasicParam)

                Case RealServiceFunctions.Suppliers_GetRegister
                    retDataset = Suppliers.GetRegister(inBasicParam)

                Case RealServiceFunctions.StockMachineToolAllocation_GetRegister
                    retDataset = StockMachineToolAllocation.GetRegister(CType(inParam, StockMachineToolAllocation.Param_GetMachineToolRegister), inBasicParam)
                Case RealServiceFunctions.StockMachineToolAllocation_GetRecord
                    retDataset = StockMachineToolAllocation.GetRecord(CType(inParam, Int32), inBasicParam)

                    'Stock Personnel Master
                Case RealServiceFunctions.Personnels_GetRegister
                    retDataset = Personnels.GetRegister(inBasicParam)
                    'RR
                Case RealServiceFunctions.StockRequisitionRequest_Get_RR_Items
                    retDataset = StockRequisitionRequest.Get_RR_Items(CType(inParam, StockRequisitionRequest.Param_Get_RR_Items), inBasicParam)
            End Select
            Return CompressData(retDataset)
        End Function

        <WebMethod(MessageName:="Wrapper for Get Single Record Function without parameter")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetSingleRecord(ByVal FunctionCalled As RealServiceFunctions, ByVal screen As ClientScreen, ByVal openUserID As String, ByVal openCenID As String, ByVal PCID As String, ByVal version As String, ByVal openYearID As String) As DataTable
            Select Case FunctionCalled

            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Get Single Record Function with parameter")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetSingleRecord(ByVal FunctionCalled As RealServiceFunctions, ByVal screen As ClientScreen, ByVal openUserID As String, ByVal openCenID As String, ByVal PCID As String, ByVal version As String, ByVal openYearID As String, ByVal inParam As Object) As DataTable
            Select Case FunctionCalled

            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Get Single Value Function without parameter")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetSingleValue(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param) As Object
            Select Case FunctionCalled
                Case RealServiceFunctions.Data_GetCurrentDateTime
                    Return DataFunctions.GetCurrentDateTime(inBasicParam)
                Case RealServiceFunctions.ActionItems_GetOverDueCount
                    Return Action_Items.GetOverDueCount(inBasicParam)
                Case RealServiceFunctions.ActionItems_GetPendingCentreRemarkCount
                    Return Action_Items.GetPendingCentreRemarkCount(inBasicParam)
                Case RealServiceFunctions.Bank_GetValue_Common
                    Return BankAccounts.GetValue_Common(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Core_GetSupportRowCount
                    Return Core.GetSupportRowCount(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_GetUnlockedTxnCount
                    Return Center.GetUnlockedTxnCount(inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_GetYearCount
                    Return CodInfo.GetYearCount(inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_GetCompletedYearCount
                    Return CodInfo.GetCompletedYearCount(inBasicParam)
                Case RealServiceFunctions.CentreRelated_CheckVolumeCenter
                    Return CodInfo.CheckVolumeCenter(inBasicParam)
                Case RealServiceFunctions.CentreRelated_VerifyAudit
                    Return Center.VerifyAudit(inBasicParam)

                Case RealServiceFunctions.AssetLocations_GetDefaultLocation
                    Return AssetLocations.GetDefaultLocation(inBasicParam)
                Case RealServiceFunctions.Vouchers_GetMaxTransactionDate
                    Return Vouchers.GetMaxTransactionDate(inBasicParam)
                Case RealServiceFunctions.Notebook_GetMaxTransactionDate
                    Return Notebook.GetMaxTransactionDate(inBasicParam)
                Case RealServiceFunctions.News_GetCount
                    Return News.GetCount(inBasicParam)
                Case RealServiceFunctions.Request_GetUnreadCount
                    Return Request.GetUnreadCount(inBasicParam)
                Case RealServiceFunctions.Membership_GetNewMembershipNo
                    Return Membership.GetNewMembershipNo(inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_GetMaxTransactionDate
                    Return Membership_Receipt_Register.GetMaxTransactionDate(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_IsFinalAuditCompleted
                    Return Center.IsFinalAuditCompleted(inBasicParam)
                Case RealServiceFunctions.Data_IsTBImportedCentre
                    Return DataFunctions.IsTBImportedCentre(inBasicParam)
                Case RealServiceFunctions.ReportsAll_ShowInsuranceLetter
                    Return Reports_All.ShowInsuranceLetter(inBasicParam)
                Case RealServiceFunctions.Data_IsMultiuserAllowed
                    Return DataFunctions.IsMultiuserAllowed(inBasicParam)
                Case RealServiceFunctions.Data_IsInsuranceAudited
                    Return DataFunctions.IsInsuranceAudited(inBasicParam)
            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Get Single Value Function with parameter"),
         XmlInclude(GetType(DataFunctions.Param_GetLastEditOn)),
         XmlInclude(GetType(BankAccounts.Param_Bank_GetTransactionMaxDate)),
         XmlInclude(GetType(Addresses.Param_GetAddressRecID)),
         XmlInclude(GetType(BankAccounts.Param_Bank_GetValue_Common)),
         XmlInclude(GetType(Voucher_Property.Param_Voucher_Property_CheckDuplicateMainCenter)),
         XmlInclude(GetType(Voucher_Property.Param_Voucher_Property_CheckDuplicatePropertyName)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetTDS)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetTDSReversal)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetCount)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetPrevFDStatus)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetItemCountInSameMaster)),
         XmlInclude(GetType(Voucher_FD.Param_VoucherFD_GetAccountNoCount)),
         XmlInclude(GetType(Reports.Param_GetLedgerOpeningBalance)),
         XmlInclude(GetType(Reports.Param_GetItemOpeningBalance)),
         XmlInclude(GetType(Reports.Param_GetSecondaryGroupOpeningBalance)),
         XmlInclude(GetType(Reports.Param_GetPrimaryGroupOpeningBalance)),
         XmlInclude(GetType(Reports.Param_GetPartyOpeningBalance)),
         XmlInclude(GetType(Vouchers.Param_Vouchers_GetAssetRecID)),
         XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetTxnCount)),
         XmlInclude(GetType(Membership.Param_GetDuplicateCount_Membership)),
         XmlInclude(GetType(Magazine.Param_GetList_Magazine)),
         XmlInclude(GetType(Membership.Param_GetCountForContinue_Membership)),
         XmlInclude(GetType(Membership.Param_GetDuplicateOldNoCount_Membership)),
         XmlInclude(GetType(Consumables.Param_GetYearEndingCount)),
         XmlInclude(GetType(Action_Items.Param_GetOpenActions_Common)),
         XmlInclude(GetType(Voucher_Internal_Transfer.Param_GetUnmatchedEntriesCentreToCentreCount)),
         XmlInclude(GetType(Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnmatchedCount)),
         XmlInclude(GetType(Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnmatchedFromCount)),
         XmlInclude(GetType(AssetLocations.Param_GetRecordCountByName)),
         XmlInclude(GetType(Membership.Param_GetDiscontinued_Membership)),
         XmlInclude(GetType(DataFunctions.Param_IsRecordCarriedForward)),
         XmlInclude(GetType(Complexes.Param_IsPropertyAlreadyMapped)),
         XmlInclude(GetType(WIP_Creation_Vouchers.Param_GetDuplicateReferenceCount)),
         XmlInclude(GetType(AssetLocations.Param_GetRecordCountByName_CurrUID)),
         XmlInclude(GetType(Deposits.Param_CheckIfDepositFinalPaymentMade)),
         XmlInclude(GetType(Complexes.Param_GetRecordCountByName_Complexes)),
         XmlInclude(GetType(Membership.Param_GetCountForWing2WingConversion)),
         XmlInclude(GetType(Magazine.param_GetMagazineCountByName)),
         XmlInclude(GetType(Magazine.param_GetMagazineCountByShortName)),
         XmlInclude(GetType(Magazine.param_GetMagazineSubTypeCountByName)),
         XmlInclude(GetType(Magazine.param_GetMagazineSubTypeCountByShortName)),
         XmlInclude(GetType(Magazine.param_GetMagazineSubFeeCountByEffDate)),
         XmlInclude(GetType(Magazine.param_GetMagazineDispatchCountByName)),
         XmlInclude(GetType(Magazine.param_GetMagazineDispatchFeeCountByEffDate)),
         XmlInclude(GetType(Magazine.param_GetMembershipCountByMemberID)),
         XmlInclude(GetType(Magazine.Param_GetIssueCount)),
          XmlInclude(GetType(StockProfile.Param_Get_StockDuplication)),
           XmlInclude(GetType(Personnels.Param_Get_PersonnelCharges_UsageCount)),
           XmlInclude(GetType(Personnels.Param_Get_Personnel_Count)),
           XmlInclude(GetType(Personnels.Param_Get_PFNo_Count)),
           XmlInclude(GetType(StockMachineToolAllocation.Param_GetMachineToolIssueCount)),
           XmlInclude(GetType(StockDeptStores.Param_GetStoreNoUsageCountInstt)),
             XmlInclude(GetType(ClientUserInfo.Param_GetUserNameCount)),
               XmlInclude(GetType(Projects.Param_GetProjCnt_ForGivenSanctionNo_CurrInstt)),
          XmlInclude(GetType(ClientUserInfo.Param_GetGroupNameCount)),
         XmlInclude(GetType(StockPurchaseOrder.Param_Get_Lotno_Duplication)),
            XmlInclude(GetType(Payments.PAram_Get_Inv_No_Count)),
         XmlInclude(GetType(Action_Items.Param_GetRemarksStatus))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetSingleValue(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Object
            Select Case FunctionCalled
                Case RealServiceFunctions.Data_GetOpeningBalanceRowCount
                    Return DataFunctions.GetOpeningBalanceRowCount(CType(inParam, String), inBasicParam) 'Case RealServiceFunctions.Data_GetLastEditOn
                Case RealServiceFunctions.Data_GetLastEditOn
                    Return DataFunctions.GetLastEditOn(CType(inParam, DataFunctions.Param_GetLastEditOn), inBasicParam)
                    'Action Items
                Case RealServiceFunctions.ActionItems_GetOpenActions_Common
                    Return Action_Items.GetOpenActions_Common(CType(inParam, Action_Items.Param_GetOpenActions_Common), inBasicParam)
                Case RealServiceFunctions.ActionItems_GetRemarksStatus
                    Return Action_Items.GetRemarksStatus(CType(inParam, Action_Items.Param_GetRemarksStatus), inBasicParam)
                Case RealServiceFunctions.Attachments_Insert
                    Return Attachments.Insert(CType(inParam, Attachments.Parameter_Insert_Attachment), inBasicParam)
                    'Address Book Related

                Case RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecID
                    Return Addresses.GetAddressRecID(CType(inParam, Addresses.Param_GetAddressRecID), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecID_FromOrgID
                    Return Addresses.GetAddressRecID_from_Org_ID(CType(inParam, String), inBasicParam)
                    'Advances
                Case RealServiceFunctions.Advances_GetAdvancePaymentCount
                    Return Advances.GetAdvancePaymentCount(CType(inParam, String), inBasicParam)
                    'Assets
                Case RealServiceFunctions.AssetLocations_GetRecordCountByName
                    Return AssetLocations.GetRecordCountByName(CType(inParam, AssetLocations.Param_GetRecordCountByName), inBasicParam)
                Case RealServiceFunctions.AssetLocations_GetRecordCountByName_CurrentUID
                    Return AssetLocations.GetRecordCountByName_CurrentUID(CType(inParam, AssetLocations.Param_GetRecordCountByName_CurrUID), inBasicParam)

                Case RealServiceFunctions.AssetLocations_GetLocationsMatchedCount
                    Return AssetLocations.GetLocationsMatchedCount(CType(inParam, String), inBasicParam)


                    'Asset location
                Case RealServiceFunctions.AssetLocations_GetLocationMatchingCount
                    Return AssetLocations.GetLocationMatchingCount(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.AssetTransfer_GetMasterID
                    Return Voucher_AssetTransfer.GetMasterID(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.AssetLocations_GetPropertyID
                    Return AssetLocations.GetPropertyID(CType(inParam, String), inBasicParam)
                    'Attachments 
                Case RealServiceFunctions.Attachments_DownloadFile
                    Return Attachments.Download_file(CType(inParam, String))

                    'Bank
                Case RealServiceFunctions.Bank_GetValue_Common
                    Return BankAccounts.GetValue_Common(inBasicParam, inParam)
                Case RealServiceFunctions.Bank_GetTxnsCountByAccID
                    Return BankAccounts.GetTxnsCountByAccID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetTransactionMaxDate
                    Return BankAccounts.GetTransactionMaxDate(CType(inParam, BankAccounts.Param_Bank_GetTransactionMaxDate), inBasicParam)
                Case RealServiceFunctions.Bank_GetFDCount
                    Return BankAccounts.GetFDCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetFDSum
                    Return BankAccounts.GetFDSum(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetTransCount
                    Return BankAccounts.GetTransCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetCountByAccountNo
                    Return BankAccounts.GetCountByAccountNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetCountByCustomerNo
                    Return BankAccounts.GetCountByCustomerNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetClosedBank
                    Return BankAccounts.GetClosedBank(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Bank_GetClosedBankAccNo
                    Return Payments.GetClosedBankAccNo(CType(inParam, String), inBasicParam)


                    'Centre Related
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserCount
                    Return ClientUserInfo.GetUserCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetMaxUserID
                    Return ClientUserInfo.GetMaxUserID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_IsFinalReportSubmitted
                    Return Center.IsFinalReportSubmitted(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_IsYearOpenForCenterCreation
                    Return CodInfo.IsYearOpenForCenterCreation(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_IsReportSubmitted
                    Return Center.IsReportSubmitted(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_GetReportsToBePrinted
                    Return CodInfo.GetReportsToBePrintedInfo(CType(inParam, String), inBasicParam)

                    'Client user
                Case RealServiceFunctions.CenterPurpose_CheckUsageCount
                    Return Center_Purpose_Info.CheckUsageCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_Get_ClientUser_EntriesCount
                    Return ClientUserInfo.Get_ClientUser_EntriesCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientGroupInfo_GetGroupNameCount
                    Return ClientUserInfo.GetGroupNameCount(CType(inParam, ClientUserInfo.Param_GetGroupNameCount), inBasicParam)

                    'Core
                Case RealServiceFunctions.Core_GetOrgPasswordForCenID
                    Return CoreFunctions.GetOrgPasswordForCenID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Core_GetCenterIDByCenRecID
                    Return CoreFunctions.GetCenterIDByCenRecID(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserNameCount
                    Return ClientUserInfo.GetUserNameCount(CType(inParam, ClientUserInfo.Param_GetUserNameCount), inBasicParam)

                    'Complexes
                Case RealServiceFunctions.Complexes_GetRecordCountByName
                    Return Complexes.GetRecordCountByName(CType(inParam, Complexes.Param_GetRecordCountByName_Complexes), inBasicParam)
                Case RealServiceFunctions.Complexes_GetMaxEditOn
                    Return Complexes.GetMaxEditOn(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Complexes_IsPropertyAlreadyMapped
                    Return Complexes.IsPropertyAlreadyMapped(CType(inParam, Complexes.Param_IsPropertyAlreadyMapped), inBasicParam)
                    'Consumables 
                Case RealServiceFunctions.Consumables_GetYearEnding
                    Return Consumables.GetYearEndingCount(CType(inParam, Consumables.Param_GetYearEndingCount), inBasicParam)
                    'Data Functions 
                Case RealServiceFunctions.Data_IsRecordCarriedForward
                    Return DataFunctions.IsRecordCarriedForward(CType(inParam, DataFunctions.Param_IsRecordCarriedForward), inBasicParam)
                Case RealServiceFunctions.Data_IsInsuranceAuditor
                    Return DataFunctions.IsInsuranceAuditor(CType(inParam, String), inBasicParam)
                    'Deposits
                Case RealServiceFunctions.Deposits_GetTransactionCount
                    Return Deposits.GetTransactionCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Deposits_CheckIfDepositFinalPaymentMade
                    Return Deposits.CheckIfDepositFinalPaymentMade(CType(inParam, Deposits.Param_CheckIfDepositFinalPaymentMade), inBasicParam)
                    'Donation
                Case RealServiceFunctions.VoucherDonation_GetOldStatusID
                    Return Voucher_Donation.GetOldStatusID(CType(inParam, String), inBasicParam)
                    'FDs
                Case RealServiceFunctions.FDs_GetExpense_IncomeCount
                    Return FD.GetExpense_IncomeCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetTDS
                    Return Voucher_FD.GetTDS(CType(inParam, Voucher_FD.Param_VoucherFD_GetTDS), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetTDSReversal
                    Return Voucher_FD.GetTDSReversal(CType(inParam, Voucher_FD.Param_VoucherFD_GetTDSReversal), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetFDCloseDate
                    Return Voucher_FD.GetFDCloseDate(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetFDCloseDateByFdID
                    Return Voucher_FD.GetFDCloseDateByFdID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetNewFDIdFromClosed
                    Return Voucher_FD.GetNewFDIdFromClosed(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetCount
                    Return Voucher_FD.GetCount(CType(inParam, Voucher_FD.Param_VoucherFD_GetCount), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetPrevFDStatus
                    Return Voucher_FD.GetPrevFDStatus(CType(inParam, Voucher_FD.Param_VoucherFD_GetPrevFDStatus), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetItemCountInSameMaster
                    Return Voucher_FD.GetItemCountInSameMaster(CType(inParam, Voucher_FD.Param_VoucherFD_GetItemCountInSameMaster), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_GetAccountNoCount
                    Return Voucher_FD.GetAccountNoCount(CType(inParam, Voucher_FD.Param_VoucherFD_GetAccountNoCount), inBasicParam)
                    'InternalTransfers
                Case RealServiceFunctions.InternalTransfer_GetCashTxnCount
                    Return Voucher_Internal_Transfer.GetCashTxnCount(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetTxnCount), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetCashPendingTxnCount
                    Return Voucher_Internal_Transfer.GetCashPendingTxnCount(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetTxnCount), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetNonCashTxnCount
                    Return Voucher_Internal_Transfer.GetNonCashTxnCount(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetTxnCount), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetNonCashPendingTxnCount
                    Return Voucher_Internal_Transfer.GetNonCashPendingTxnCount(CType(inParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_GetTxnCount), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_GetUnmatchedEntriesCentreToCentreCount
                    Return Voucher_Internal_Transfer.GetUnmatchedEntriesCentreToCentreCount(CType(inParam, Voucher_Internal_Transfer.Param_GetUnmatchedEntriesCentreToCentreCount), inBasicParam)
                    'asset transfer
                Case RealServiceFunctions.AssetTransfer_GetUnmatchedEntriesCount
                    Return Voucher_AssetTransfer.GetUnmatchedCount_AssetTransfer(CType(inParam, Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnmatchedCount), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_GetUnmatchedFromEntriesCount
                    Return Voucher_AssetTransfer.GetUnmatchedCount_AssetTransferFrom(CType(inParam, Voucher_AssetTransfer.Param_VoucherAssetTransfer_GetUnmatchedFromCount), inBasicParam)
                    'Job
                Case RealServiceFunctions.Jobs_GetJob_UO_Count
                    Return Jobs.GetJob_UO_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJob_UO_Pending_Count
                    Return Jobs.GetJob_UO_Pending_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJob_RR_Count
                    Return Jobs.GetJob_RR_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJob_RR_Pending_Count
                    Return Jobs.GetJob_RR_Pending_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJob_Total_Usage_Count
                    Return Jobs.GetJob_Total_Usage_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Jobs_GetJob_Project_Main_Assignee
                    Return Jobs.GetJob_Project_Main_Assignee(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_GetStoreUsageCount
                    Return StockDeptStores.GetStoreDeptUsageCount(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_GetStoreNoUsageCountInstt
                    Return StockDeptStores.GetStoreNoUsageCountInstt(CType(inParam, StockDeptStores.Param_GetStoreNoUsageCountInstt), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_GetStockCountForLocation
                    Return StockDeptStores.GetStockCountForLocation(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.Personnels_Get_PersonnelCharges_UsageCount
                    Return Personnels.Get_PersonnelCharges_UsageCount(CType(inParam, Personnels.Param_Get_PersonnelCharges_UsageCount), inBasicParam)
                Case RealServiceFunctions.Personnels_Get_Personnel_Count
                    Return Personnels.Get_Personnel_Count(CType(inParam, Personnels.Param_Get_Personnel_Count), inBasicParam)
                Case RealServiceFunctions.Personnels_Get_PFNo_Count
                    Return Personnels.Get_PFNo_Count(CType(inParam, Personnels.Param_Get_PFNo_Count), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Latest_RR_ID
                    Return StockPurchaseOrder.Get_PO_Latest_RR_ID(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Latest_UO_ID
                    Return StockPurchaseOrder.Get_PO_Latest_UO_ID(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Job_Project_Completed
                    Return StockPurchaseOrder.Get_PO_Job_Project_Completed(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Non_Rate_Items
                    Return StockPurchaseOrder.Get_PO_Non_Rate_Items(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Pending_Due
                    Return StockPurchaseOrder.Get_PO_Pending_Due(CType(inParam, Int32), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_Get_Stock_Current_Quantity_Count
                    Return StockPurchaseOrder.Get_Stock_Current_Quantity_Count(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Related_ClosedDept_Count
                    Return StockPurchaseOrder.Get_PO_Related_ClosedDept_Count(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_Get_PO_Duplicate_LotNo_Count
                    Return StockPurchaseOrder.Get_PO_Duplicate_LotNo_Count(CType(inParam, StockPurchaseOrder.Param_Get_Lotno_Duplication), inBasicParam)

                    'Liabilities
                Case RealServiceFunctions.Liabilities_GetTransactionCount
                    Return Liabilities.GetTransactionCount(CType(inParam, String), inBasicParam)
                    'Magazine
                Case RealServiceFunctions.Magazine_GetNewMembershipNo
                    Return Magazine.GetNewMembershipNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineCountByName
                    Return Magazine.GetMagazineCountByName(CType(inParam, Magazine.param_GetMagazineCountByName), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineCountByShortName
                    Return Magazine.GetMagazineCountByShortName(CType(inParam, Magazine.param_GetMagazineCountByShortName), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineSubsTypeCountByName
                    Return Magazine.GetMagazineSubsTypeCountByName(CType(inParam, Magazine.param_GetMagazineSubTypeCountByName), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineSubsTypeCountByShortName
                    Return Magazine.GetMagazineSubsTypeCountByShortName(CType(inParam, Magazine.param_GetMagazineSubTypeCountByShortName), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineSubsFeeCountByEffDate
                    Return Magazine.GetMagazineSubsFeeCountByEffDate(CType(inParam, Magazine.param_GetMagazineSubFeeCountByEffDate), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineDispatchCountByName
                    Return Magazine.GetMagazineDispatchCountByName(CType(inParam, Magazine.param_GetMagazineDispatchCountByName), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMagazineDispatchFeeCountByEffDate
                    Return Magazine.GetMagazineDispatchFeeCountByEffDate(CType(inParam, Magazine.param_GetMagazineDispatchFeeCountByEffDate), inBasicParam)
                Case RealServiceFunctions.Magazine_GetMembershipCountByMemberID
                    Return Magazine.GetMembershipCountByMemberID(CType(inParam, Magazine.param_GetMembershipCountByMemberID), inBasicParam)
                Case RealServiceFunctions.Voucher_Magazine_Register_GetDiscontinued
                    Return Voucher_Magazine_Register.GetDiscontinued(CType(inParam, Membership.Param_GetDiscontinued_Membership), inBasicParam)
                Case RealServiceFunctions.Voucher_Magazine_Register_GetReceiptCount
                    Return Voucher_Magazine_Register.GetReceiptCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetLastEntryDate
                    Return Magazine.GetLastEntryDate(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_GetIssueCount
                    Return Magazine.GetIssueCount(CType(inParam, Magazine.Param_GetIssueCount), inBasicParam)
                Case RealServiceFunctions.Magazine_GetRestriction_Date
                    Return Magazine.GetMagazinesRestrictionDate(CType(inParam, String), inBasicParam)

                    'Membership
                Case RealServiceFunctions.Membership_GetDuplicateCount
                    Return Membership.GetDuplicateCount(CType(inParam, Membership.Param_GetDuplicateCount_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_GetCountForContinue
                    Return Membership.GetCountForContinue(CType(inParam, Membership.Param_GetCountForContinue_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_GetDuplicateOldNoCount
                    Return Membership.GetDuplicateOldNoCount(CType(inParam, Membership.Param_GetDuplicateOldNoCount_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_GetMembershipNo
                    Return Membership.GetMembershipNo(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetLastEntryDate
                    Return Membership.GetLastEntryDate(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetLastTransactionDate
                    Return Membership.GetLastTransactionDate(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetDiscontinued
                    Return Membership.GetDiscontinued(CType(inParam, Membership.Param_GetDiscontinued_Membership), inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_GetReceiptCount
                    Return Membership_Receipt_Register.GetReceiptCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_GetMasterID
                    Return Voucher_Membership.GetMasterID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_GetLastPeriod
                    Return Voucher_Membership.GetLastPeriod(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_GetLastPeriod
                    Return Voucher_Membership_Renewal.GetLastPeriod(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_GetMasterID
                    Return Voucher_Membership_Renewal.GetMasterID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_GetCountForWing2WingConversion
                    Return Membership.GetCountForWing2WingConversion(CType(inParam, Membership.Param_GetCountForWing2WingConversion), inBasicParam)
                Case RealServiceFunctions.Membership_GetDiscontinued_Date
                    Return Membership.GetDiscontinued_Date(CType(inParam, Membership.Param_GetCountForContinue_Membership), inBasicParam)

                    'Opening Balances
                Case RealServiceFunctions.OpeningBalances_GetDuplicateCount
                    Return OpeningBalances.GetDuplicateCount(CType(inParam, OpeningBalances.Parameter_OpeningBalances_GetList), inBasicParam)

                    'Property
                Case RealServiceFunctions.Property_GetCenterCountByName
                    Return LandAndBuilding.GetCenterCountByName(CType(inParam, String), inBasicParam)

                    'Project
                Case RealServiceFunctions.Projects_GetProject_Open_Jobs_Count
                    Return Projects.GetProject_Open_Jobs_Count(CType(inParam, Integer), inBasicParam)

                Case RealServiceFunctions.Projects_GetProject_Jobs_Count
                    Return Projects.GetProject_Jobs_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.Projects_GetProjCnt_ForGivenSanctionNo_CurrInstt
                    Return Projects.GetProjCnt_ForGivenSanctionNo_CurrInstt(CType(inParam, Projects.Param_GetProjCnt_ForGivenSanctionNo_CurrInstt), inBasicParam)

                Case RealServiceFunctions.StockProduction_GetUsedLeftManpowerCount
                    Return StockProduction.GetUsedLeftManpowerCount(CType(inParam, Int32), inBasicParam)

                Case RealServiceFunctions.Property_GetTransactionCount
                    Return LandAndBuilding.GetTransactionCount(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Property_CheckDuplicatePropertyName
                    Return Voucher_Property.CheckDuplicatePropertyName(CType(inParam, Voucher_Property.Param_Voucher_Property_CheckDuplicatePropertyName), inBasicParam)
                    'Receipts
                Case RealServiceFunctions.Receipts_GetMasterID
                    Return Receipts.GetMasterID(CType(inParam, String), inBasicParam)
                    'RR

                Case RealServiceFunctions.StockRequisitionRequest_Get_PO_Incomplete_Count
                    Return StockRequisitionRequest.Get_PO_TO_Incomplete_Count(CType(inParam, Int32), inBasicParam)

                    'Reports
                Case RealServiceFunctions.Reports_GetLedgerOpeningBalance
                    Return Reports.GetLedgerOpeningBalance(CType(inParam, Reports.Param_GetLedgerOpeningBalance), inBasicParam)
                Case RealServiceFunctions.Reports_GetItemOpeningBalance
                    Return Reports.GetItemOpeningBalance(CType(inParam, Reports.Param_GetItemOpeningBalance), inBasicParam)
                Case RealServiceFunctions.Reports_GetSecondaryGroupOpeningBalance
                    Return Reports.GetSecondaryGroupOpeningBalance(CType(inParam, Reports.Param_GetSecondaryGroupOpeningBalance), inBasicParam)
                Case RealServiceFunctions.Reports_GetPrimaryGroupOpeningBalance
                    Return Reports.GetPrimaryGroupOpeningBalance(CType(inParam, Reports.Param_GetPrimaryGroupOpeningBalance), inBasicParam)

                    'Sale Of Assets
                Case RealServiceFunctions.SaleOfAssets_GetMasterID
                    Return Voucher_SaleOfAsset.GetMasterID(CType(inParam, String), inBasicParam)
                    'ServicePlaces
                Case RealServiceFunctions.ServicePlaces_GetCountByPlaceName
                    Return ServicePlaces.GetCountByPlaceName(CType(inParam, String), inBasicParam)
                    'stock
                Case RealServiceFunctions.StockProfile_GetStockDuplication
                    Return StockProfile.GetStockDuplication(CType(inParam, StockProfile.Param_Get_StockDuplication), inBasicParam)

                    'Telephones
                Case RealServiceFunctions.Telephone_GetRecordByTeleNumber
                    Return Telephones.GetRecordByTeleNumber(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Telephone_GetCountInTxn
                    Return Telephones.GetCountInTxn(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Telephone_GetLastEntryDate
                    Return Telephones.GetLastEntryDate(CType(inParam, String), inBasicParam)

                    'Vouchers
                Case RealServiceFunctions.Vouchers_GetReferenceRecordID
                    Return Vouchers.GetReferenceRecordID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetAssetRecID
                    Return Vouchers.GetAssetRecID(CType(inParam, Vouchers.Param_Vouchers_GetAssetRecID), inBasicParam)
                Case RealServiceFunctions.Vouchers_GetEditOnByRecID
                    Return Vouchers.GetEditOnByRecID(CType(inParam, String), inBasicParam)

                Case RealServiceFunctions.UserOrder_GetUO_RR_Count
                    Return StockUserOrder.GetUO_RR_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUO_Job_Status
                    Return StockUserOrder.GetUO_Job_Status(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUO_RR_Status
                    Return StockUserOrder.GetUO_RR_Status(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUO_Scrap_Creation_Count
                    Return StockUserOrder.GetUO_Scrap_Creation_Count(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOReqItem_Delivered_EntryCount
                    Return StockUserOrder.GetUOReqItem_Delivered_EntryCount(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUODelivery_Received_EntryCount
                    Return StockUserOrder.GetUODelivery_Received_EntryCount(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOReceipt_Return_EntryCount
                    Return StockUserOrder.GetUOReceipt_Return_EntryCount(CType(inParam, Integer), inBasicParam)
                Case RealServiceFunctions.UserOrder_GetUOReturn_Received_EntryCount
                    Return StockUserOrder.GetUOReturn_Received_EntryCount(CType(inParam, Integer), inBasicParam)


                Case RealServiceFunctions.StockPurchaseOrder_GetPOItem_Received_EntryCount
                    Return StockPurchaseOrder.GetPOItem_Received_EntryCount(CType(inParam, Integer), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_GetPOReceipt_Return_EntryCount
                    Return StockPurchaseOrder.GetPOReceipt_Return_EntryCount(CType(inParam, Integer), inBasicParam)


                Case RealServiceFunctions.StockMachineToolAllocation_GetMachineToolIssueCount
                    Return StockMachineToolAllocation.GetMachineToolIssueCount(CType(inParam, StockMachineToolAllocation.Param_GetMachineToolIssueCount), inBasicParam)
                    'WIP
                Case RealServiceFunctions.WIP_Creation_Vouchers_GetDuplicateReferenceCount
                    Return WIP_Creation_Vouchers.GetDuplicateReferenceCount(CType(inParam, WIP_Creation_Vouchers.Param_GetDuplicateReferenceCount), inBasicParam)
                Case RealServiceFunctions.WIP_Creation_Vouchers_GetCountOfReferencesByLedID
                    Return WIP_Creation_Vouchers.GetCountOfReferencesByLedID(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Payments_Get_Inv_No_Count
                    Return Payments.Get_Inv_No_Count(CType(inParam, Payments.PAram_Get_Inv_No_Count), inBasicParam)
            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record Status Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordStatus(inBasicParam As Basic_Param, ByVal table As Tables, ByVal CenIDColName As String, ByVal RecID As String) As Object
            Dim Query As String = "SELECT REC_STATUS FROM " & table.ToString() & "  WHERE " & CenIDColName & "='" & inBasicParam.openCenID & "' AND REC_ID  = '" & RecID & "' "
            Return GetScalar(table, Query, table.ToString(), inBasicParam)
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record Status by Specific Column Filter Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordStatus(inBasicParam As Basic_Param, ByVal table As Tables, ByVal CenIDColName As String, ByVal RecIDColName As String, ByVal RecID As String) As Object
            Dim Query As String = "SELECT REC_STATUS FROM " & table.ToString() & "  WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & CenIDColName & "='" & inBasicParam.openCenID & "' AND " & RecIDColName & "  = '" & RecID & "' "
            Return GetScalar(table, Query, table.ToString(), inBasicParam)
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record By ID Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByID(inBasicParam As Basic_Param, ByVal table As Tables, ByVal RecID As String) As Byte()
            Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & RecID & "'"
            Return CompressData(GetRecordByID(inBasicParam, table, RecID))
        End Function
        <WebMethod(MessageName:="Wrapper for Get Record By Txn_Rec_ID Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByID_SplVchrRef(inBasicParam As Basic_Param, ByVal table As Tables, ByVal Txn_RecID As String) As Byte()
            'Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & RecID & "'"
            Return CompressData(GetRecordByID_SplVchrRefs(inBasicParam, table, Txn_RecID))
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record By Specific Column Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal TableName As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String) As Byte()
            Return CompressData(GetRecordByColumn(inBasicParam, table, TableName, ConditionColumnName, ConditionValue))
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record By two Column Values Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String) As Byte()
            Return CompressData(GetRecordByColumn(inBasicParam, table, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value))
        End Function

        <WebMethod(MessageName:="Wrapper for Get Selected Cols By two Column Values  Function ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal SelectedColumns As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String) As Byte()
            Return CompressData(GetRecordByColumn(inBasicParam, table, SelectedColumns, ConditionColumnName, ConditionValue, ConditionColumn2Name, Condition2Value))
        End Function

        <WebMethod(MessageName:="Wrapper for Get Record By Custom Condition ")> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_GetRecordByCustom(inBasicParam As Basic_Param, ByVal table As Tables, ByVal CustomCondition As String) As Byte()
            Return CompressData(GetRecordByCustom(inBasicParam, table, CustomCondition))
        End Function

        <WebMethod(MessageName:="Wrapper for Insert Function"),
        XmlInclude(GetType(DataFunctions.Param_AddOpeningBalance)),
        XmlInclude(GetType(CodInfo.Param_CodInfo_AddNew)),
        XmlInclude(GetType(Action_Items.Parameter_Insert_Action_Items)),
        XmlInclude(GetType(BankAccounts.Parameter_Insert_BankandBalance_BankAccounts)),
        XmlInclude(GetType(Addresses.Parameter_Insert_Addresses)),
        XmlInclude(GetType(Addresses.Parameter_InsertMagazine_Addresses)),
        XmlInclude(GetType(Addresses.Parameter_InsertWings_Addresses)),
        XmlInclude(GetType(Addresses.Parameter_InsertSpecialities_Addresses)),
        XmlInclude(GetType(Addresses.Parameter_InsertEvents_Addresses)),
        XmlInclude(GetType(Addresses.Parameter_InsertQuery_ExcelRawDataUpload)),
         XmlInclude(GetType(ClientUserInfo.Parameter_AddNew_ClientUserInfo)),
         XmlInclude(GetType(Center.Param_AddVerification)),
         XmlInclude(GetType(Advances.Parameter_Insert_Advances)),
         XmlInclude(GetType(Advances.Parameter_InsertTRID_Advances)),
         XmlInclude(GetType(Liabilities.Parameter_Insert_Liabilities)),
         XmlInclude(GetType(Liabilities.Parameter_InsertTRID_Liabilities)),
         XmlInclude(GetType(LandAndBuilding.Parameter_Insert_LandAndBuilding)),
         XmlInclude(GetType(LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding)),
         XmlInclude(GetType(LandAndBuilding.Parameter_InsertExtendedInfo_LandAndBuilding)),
         XmlInclude(GetType(LandAndBuilding.Parameter_InsertDocInfo_LandAndBuilding)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Subs_Type)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Subs_Type_Fee)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Dispatch_Type)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Dispatch_Type_Charges)),
        XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Membership_Profile)),
         XmlInclude(GetType(Cash.Parameter_AddDefault_Cash)),
         XmlInclude(GetType(Deposits.Parameter_Insert_Deposits)),
         XmlInclude(GetType(Deposits.Parameter_InsertTRID_Deposits)),
        XmlInclude(GetType(FD.Parameter_InsertandUpdateBalance_FD)),
        XmlInclude(GetType(Voucher_FD.Parameter_InsertFD_VoucherFD)),
        XmlInclude(GetType(Voucher_FD.Parameter_InsertFDHistory_VoucherFD)),
        XmlInclude(GetType(Voucher_FD.Parameter_InsertMasterInfo_VoucherFD)),
        XmlInclude(GetType(Voucher_FD.Parameter_Insert_VoucherFD)),
        XmlInclude(GetType(Assets.Parameter_Insert_Assets)),
        XmlInclude(GetType(Assets.Parameter_InsertTRIDAndTRSrNo_Assets)),
        XmlInclude(GetType(AssetLocations.Param_AssetLoc_Insert)),
        XmlInclude(GetType(GoldSilver.Parameter_Insert_GoldSilver)),
        XmlInclude(GetType(GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver)),
        XmlInclude(GetType(Consumables.Parameter_Insert_ConsumableStock)),
        XmlInclude(GetType(OpeningBalances.Parameter_Insert_OpeningBalances)),
        XmlInclude(GetType(Vehicles.Parameter_Insert_Vehicles)),
        XmlInclude(GetType(Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles)),
        XmlInclude(GetType(Payments.Parameter_InsertMasterInfo_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_Insert_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_InsertItem_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_InsertPayment_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_InsertPaymentMT_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_InsertPurpose_VoucherPayment)),
        XmlInclude(GetType(Payments.Parameter_InsertOther_VoucherPayment)),
        XmlInclude(GetType(Receipts.Parameter_InsertMasterInfo_VoucherReceipt)),
        XmlInclude(GetType(Receipts.Parameter_Insert_VoucherReceipt)),
        XmlInclude(GetType(Receipts.Parameter_InsertItem_VoucherReceipt)),
        XmlInclude(GetType(Receipts.Parameter_InsertPurpose_VoucherReceipt)),
        XmlInclude(GetType(Receipts.Parameter_InsertAandLPayment_VoucherReceipt)),
        XmlInclude(GetType(Receipts.Parameter_InsertBankPayment_VoucherReceipt)),
        XmlInclude(GetType(DonationRegister.Param_DonationRegister_InsertReceiptRequest)),
        XmlInclude(GetType(DonationRegister.Parameter_InsertDonationAddress_DonationRegister)),
        XmlInclude(GetType(Voucher_Donation.Parameter_Insert_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_InsertForeignInfo_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_InsertPurpose_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_InsertDonStatus_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_InsertMasterInfo_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_Insert_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_InsertPurpose_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_InsertMasterInfo_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_Insert_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_InsertItem_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_InsertPurpose_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_InsertAandLPayment_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_InsertBankPayment_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_InsertMasterInfo_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_Insert_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_InsertItem_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_InsertPurpose_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_InsertAandLPayment_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_Gift.Parameter_Insert_VoucherGift)),
        XmlInclude(GetType(Voucher_Gift.Parameter_InsertMasterInfo_VoucherGift)),
        XmlInclude(GetType(Voucher_Gift.Parameter_InsertPurpose_VoucherGift)),
        XmlInclude(GetType(Voucher_Gift.Parameter_InsertItem_VoucherGift)),
        XmlInclude(GetType(Voucher_BankToBank.Parameter_Insert_Voucher_BankToBank)),
        XmlInclude(GetType(Voucher_CashBank.Parameter_Insert_Voucher_CashBank)),
        XmlInclude(GetType(Voucher_CollectionBox.Parameter_Insert_Voucher_CollectionBox)),
        XmlInclude(GetType(Notebook.Parameter_Insert_NoteBook)),
        XmlInclude(GetType(Notebook.Param_InsertAllEntries_Notebook)),
        XmlInclude(GetType(Livestock.Parameter_Insert_LiveStock)),
        XmlInclude(GetType(Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock)),
        XmlInclude(GetType(Telephones.Parameter_Insert_Telephones)),
        XmlInclude(GetType(Request.Parameter_Insert_Request)),
        XmlInclude(GetType(Letters.Parameter_Insert_Letters)),
        XmlInclude(GetType(Notes.Param_AddQuickNote_Notes)),
        XmlInclude(GetType(Notes.Parameter_Insert_Notes)),
        XmlInclude(GetType(Voucher_Journal.Parameter_InsertMasterInfo_VoucheJournal)),
        XmlInclude(GetType(Voucher_Journal.Parameter_Insert_VoucherJournal)),
        XmlInclude(GetType(Voucher_Journal.Parameter_InsertPurpose_VoucherJournal)),
        XmlInclude(GetType(ServicePlaces.Parameter_Insert_ServicePlaces)),
        XmlInclude(GetType(ServiceReport.Parameter_Insert_ServiceReport)),
        XmlInclude(GetType(ServiceReport.Parameter_InsertWings_ServiceReport)),
        XmlInclude(GetType(ServiceReport.Parameter_InsertGuest_ServiceReport)),
         XmlInclude(GetType(Complexes.Param_Insert_Complexes)),
         XmlInclude(GetType(Complexes.Param_InsertBuilding_Complexes)),
         XmlInclude(GetType(WIP_Profile.Param_Insert_WIP_Profile)),
         XmlInclude(GetType(WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile)),
        XmlInclude(GetType(Membership.Parameter_Insert_Membership)),
         XmlInclude(GetType(Voucher_Magazine_Register.Parameter_InsertPurpose_VoucherMagazineMembership)),
         XmlInclude(GetType(Voucher_Magazine_Register.Param_InsertReceipt_Magazine_Receipt_Register)),
        XmlInclude(GetType(Membership.Parameter_InsertBalances_Membership)),
        XmlInclude(GetType(Membership_Receipt_Register.Param_InsertReceipt_Membership_Receipt_Register)),
        XmlInclude(GetType(Voucher_Membership.Parameter_InsertMasterInfo_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership.Parameter_Insert_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership.Parameter_InsertItem_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership.Parameter_InsertPayment_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership.Parameter_InsertPurpose_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_InsertMasterInfo_VoucherMembershipRenewal)),
         XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_Insert_VoucherMembershipRenewal)),
         XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_InsertItem_VoucherMembershipRenewal)),
         XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_InsertPayment_VoucherMembershipRenewal)),
         XmlInclude(GetType(Voucher_WIP_Finalization.Param_InsertMasterInfo_Voucher_WIPFinalization)),
         XmlInclude(GetType(Voucher_WIP_Finalization.Param_Insert_Voucher_WIPFinalization)),
         XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Subcity)),
         XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Dispatch)),
         XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Dispatch_Bundles)),
         XmlInclude(GetType(Magazine.Parameter_Insert_dispatch_New_Voucher)),
         XmlInclude(GetType(Magazine_Request_Register.Parameter_Insert_Magazine_Membership_Request)),
         XmlInclude(GetType(Center.Param_ResumeAudit)),
         XmlInclude(GetType(Magazine.Parameter_Insert_Magazine_Client_Restriction)),
         XmlInclude(GetType(Center.Param_AddAccountsSubmissionPeriod)),
         XmlInclude(GetType(Magazine.Param_Insert_Magazine_Issue)),
         XmlInclude(GetType(Center_Purpose_Info.Parameter_Insert_Center_Purpose)),
         XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_InsertTDSDeduction_VoucherInternalTransfer)),
         XmlInclude(GetType(Membership.Param_Inset_SubcriptionList)),
         XmlInclude(GetType(Membership.Param_Insert_SubscriptionFee)),
         XmlInclude(GetType(Magazine.Param_Insert_Magazine_Similar_Issues)),
         XmlInclude(GetType(Attachments.Parameter_Insert_Attachment)),
         XmlInclude(GetType(Attachments.Parameter_Insert_Attachment_Link)),
         XmlInclude(GetType(StockProfile.Param_Add_StockProfile)),
         XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Item)),
         XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Item_Delivered)),
         XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Item_Received)),
         XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Item_Returned)),
         XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Item_Return_Received)),
         XmlInclude(GetType(StockProfile.Param_Add_Stock_Addition)),
         XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsDeliverAllPending)),
         XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReceiveAllPending)),
         XmlInclude(GetType(StockUserOrder.Param_GetUOGoodsReturnAllPending)),
          XmlInclude(GetType(StockUserOrder.Param_UO_Insert_UO_RR_Mapping)),
         XmlInclude(GetType(StockDeptStores.Param_InsertStoreDept)),
         XmlInclude(GetType(Personnels.Param_InsertPersonnel)),
         XmlInclude(GetType(Personnels.Param_InsertPersonnelCharges)),
         XmlInclude(GetType(StockMachineToolAllocation.Param_Insert_MachineTool_Return)),
         XmlInclude(GetType(StockMachineToolAllocation.Param_Insert_MachineTool_Issue)),
         XmlInclude(GetType(SubItem.Param_SubItem_Insert_Item_Properties)),
            XmlInclude(GetType(SubItem.Param_SubItem_Insert_Unit_Conversion)),
         XmlInclude(GetType(Suppliers.Param_InsertItemSupplierMapping)),
         XmlInclude(GetType(Suppliers.Param_InsertsupplierBank)),
         XmlInclude(GetType(ClientUserInfo.Param_InsertClientUser)),
         XmlInclude(GetType(ClientUserInfo.Param_InsertClientUserGroup)),
         XmlInclude(GetType(ClientUserInfo.Param_InsertClientUserPrivileges)),
         XmlInclude(GetType(StockPurchaseOrder.Param_InsertPurchaseOrderGoodsReceived)),
         XmlInclude(GetType(StockPurchaseOrder.Param_InsertPurchaseOrderGoodsReturned)),
         XmlInclude(GetType(StockPurchaseOrder.Param_InsertPurchaseOrderPayment)),
         XmlInclude(GetType(Vouchers.Param_Offer_Entry_for_Audit)),
         XmlInclude(GetType(Audit.Param_InsertDocumentMapResponse)),
         XmlInclude(GetType(Audit.Param_InsertVouchingStatus)),
         XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_InsertPurpose_VoucherMembershipRenewal)),
         XmlInclude(GetType(ServiceMaterial.Parameter_Insert_ServiceMaterial)),
         XmlInclude(GetType(ServiceMaterial.Param_Txn_Insert_ServiceMaterial))>
        <XmlInclude(GetType(DBNull))>
        Public Function Wrap_Insert(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Boolean
            Select Case FunctionCalled
                Case RealServiceFunctions.Data_AddOpeningBalance
                    Return DataFunctions.AddOpeningBalance(CType(inParam, DataFunctions.Param_AddOpeningBalance), inBasicParam)
                Case RealServiceFunctions.ActionItems_Insert
                    Return Action_Items.Insert(CType(inParam, Action_Items.Parameter_Insert_Action_Items), inBasicParam)
                Case RealServiceFunctions.Audit_InsertDocumentMapResponse
                    Return Audit.InsertDocumentMapResponse(CType(inParam, Audit.Param_InsertDocumentMapResponse), inBasicParam)
                Case RealServiceFunctions.Audit_InsertVouchingStatus
                    Return Audit.InsertVouchingStatus(CType(inParam, Audit.Param_InsertVouchingStatus), inBasicParam)

                Case RealServiceFunctions.Bank_Insert_Bank_and_Balance
                    Return BankAccounts.Insert_Bank_and_Balance(CType(inParam, BankAccounts.Parameter_Insert_BankandBalance_BankAccounts), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_Insert
                    Return Addresses.Insert(CType(inParam, Addresses.Parameter_Insert_Addresses), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_InsertMagazine
                    Return Addresses.InsertMagazine(CType(inParam, Addresses.Parameter_InsertMagazine_Addresses), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_InsertWings
                    Return Addresses.InsertWings(CType(inParam, Addresses.Parameter_InsertWings_Addresses), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_InsertSpecialities
                    Return Addresses.InsertSpecialities(CType(inParam, Addresses.Parameter_InsertSpecialities_Addresses), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Addresses_InsertEvents
                    Return Addresses.InsertEvents(CType(inParam, Addresses.Parameter_InsertEvents_Addresses), inBasicParam)
                Case RealServiceFunctions.AddressBookRelated_Excel_Insert_Query
                    Return Addresses.Excel_Insert_Query(CType(inParam, Addresses.Parameter_InsertQuery_ExcelRawDataUpload), inBasicParam)
                Case RealServiceFunctions.CentreRelated_CodInfo_AddNew
                    Return CodInfo.AddNew(CType(inParam, CodInfo.Param_CodInfo_AddNew), inBasicParam)
                Case RealServiceFunctions.CentreRelated_AddAuditVerifications
                    Return Center.AddNewVerification(CType(inParam, Center.Param_AddVerification), inBasicParam)
                Case RealServiceFunctions.CentreRelated_AddNotification
                    Return ClientUserInfo.AddNewNotification(inBasicParam)

                Case RealServiceFunctions.Attachments_InsertLink
                    Return Attachments.Insert_Link(CType(inParam, Attachments.Parameter_Insert_Attachment_Link), inBasicParam)
                    'UserInfo
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_InsertClientUser
                    Return ClientUserInfo.InsertClientUser(CType(inParam, ClientUserInfo.Param_InsertClientUser), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientGroupInfo_InsertClientUserGroup
                    Return ClientUserInfo.InsertClientUserGroup(CType(inParam, ClientUserInfo.Param_InsertClientUserGroup), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_InsertClientUserPrivileges
                    Return ClientUserInfo.InsertClientUserPrivileges(CType(inParam, ClientUserInfo.Param_InsertClientUserPrivileges), inBasicParam)

                Case RealServiceFunctions.CentreRelated_VerifyAudit
                    Return Center.VerifyAudit(inBasicParam)
                Case RealServiceFunctions.CentreRelated_ReturnForCorrection
                    Return Center.ReturnForCorrection(inBasicParam)
                Case RealServiceFunctions.CentreRelated_ResumeAudit
                    Return Center.ResumeAudit(CType(inParam, Center.Param_ResumeAudit), inBasicParam)
                Case RealServiceFunctions.CentreRelated_AddAccountsSubmissionPeriod
                    Return Center.AddAccountsSubmissionPeriod(CType(inParam, Center.Param_AddAccountsSubmissionPeriod), inBasicParam)
                Case RealServiceFunctions.CenterPurpose_Insert
                    Return Center_Purpose_Info.Insert(CType(inParam, Center_Purpose_Info.Parameter_Insert_Center_Purpose), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_AddNew
                    Return ClientUserInfo.AddNew(CType(inParam, ClientUserInfo.Parameter_AddNew_ClientUserInfo), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Core_Insert
                    Return Core.Insert(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.Advances_Insert
                    Return Advances.Insert(CType(inParam, Advances.Parameter_Insert_Advances), inBasicParam)
                Case RealServiceFunctions.Advances_InsertTRID
                    Return Advances.Insert(CType(inParam, Advances.Parameter_InsertTRID_Advances), inBasicParam)
                Case RealServiceFunctions.Liabilities_Insert
                    Return Liabilities.Insert(CType(inParam, Liabilities.Parameter_Insert_Liabilities), inBasicParam)
                Case RealServiceFunctions.Liabilities_InsertTRID
                    Return Liabilities.Insert(CType(inParam, Liabilities.Parameter_InsertTRID_Liabilities), inBasicParam)
                Case RealServiceFunctions.Property_Insert
                    Return LandAndBuilding.Insert(CType(inParam, LandAndBuilding.Parameter_Insert_LandAndBuilding), inBasicParam)
                Case RealServiceFunctions.Property_InsertMasterIdSrNo
                    Return LandAndBuilding.Insert(CType(inParam, LandAndBuilding.Parameter_InsertMasterIDAndSrNo_LandAndBuilding), inBasicParam)
                Case RealServiceFunctions.Property_InsertExtendedInfo
                    Return LandAndBuilding.InsertExtendedInfo(CType(inParam, LandAndBuilding.Parameter_InsertExtendedInfo_LandAndBuilding), inBasicParam)
                Case RealServiceFunctions.Property_InsertDocumentsInfo
                    Return LandAndBuilding.InsertDocumentsInfo(CType(inParam, LandAndBuilding.Parameter_InsertDocInfo_LandAndBuilding), inBasicParam)
                Case RealServiceFunctions.Cash_AddDefault
                    Return Cash.AddDefault(CType(inParam, Cash.Parameter_AddDefault_Cash), inBasicParam)
                Case RealServiceFunctions.Deposits_Insert
                    Return Deposits.Insert(CType(inParam, Deposits.Parameter_Insert_Deposits), inBasicParam)
                Case RealServiceFunctions.Deposits_InsertTRID
                    Return Deposits.Insert(CType(inParam, Deposits.Parameter_InsertTRID_Deposits), inBasicParam)
                Case RealServiceFunctions.FDs_Insert_and_Update_Balance
                    Return FD.Insert_and_Update_Balance(CType(inParam, FD.Parameter_InsertandUpdateBalance_FD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_InsertFD
                    Return Voucher_FD.InsertFD(CType(inParam, Voucher_FD.Parameter_InsertFD_VoucherFD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_InsertFDHistory
                    Return Voucher_FD.InsertFDHistory(CType(inParam, Voucher_FD.Parameter_InsertFDHistory_VoucherFD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_InsertMasterInfo
                    Return Voucher_FD.InsertMasterInfo(CType(inParam, Voucher_FD.Parameter_InsertMasterInfo_VoucherFD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_Insert
                    Return Voucher_FD.Insert(CType(inParam, Voucher_FD.Parameter_Insert_VoucherFD), inBasicParam)
                Case RealServiceFunctions.Assets_Insert
                    Return Assets.Insert(CType(inParam, Assets.Parameter_Insert_Assets), inBasicParam)
                Case RealServiceFunctions.Assets_InsertTRIDAndTRSrNo
                    Return Assets.Insert(CType(inParam, Assets.Parameter_InsertTRIDAndTRSrNo_Assets), inBasicParam)
                    ' Case RealServiceFunctions.AssetLocations_InsertIfDefaultNotPresent
                    '     Return AssetLocations.InsertIfDefaultNotPresent(CType(inParam, String), inBasicParam)
                Case RealServiceFunctions.AssetLocations_Insert
                    Return AssetLocations.Insert(CType(inParam, AssetLocations.Param_AssetLoc_Insert), inBasicParam)
                Case RealServiceFunctions.AssetLocations_Insert_AllSisterUIDs
                    Return AssetLocations.Insert_AllSisterUIDs(CType(inParam, AssetLocations.Param_AssetLoc_Insert), inBasicParam)
                Case RealServiceFunctions.GoldSilver_Insert
                    Return GoldSilver.Insert(CType(inParam, GoldSilver.Parameter_Insert_GoldSilver), inBasicParam)
                Case RealServiceFunctions.GoldSilver_InsertTRIDAndTRSRNo
                    Return GoldSilver.Insert(CType(inParam, GoldSilver.Parameter_InsertTRIDAndTRSRNo_GoldSilver), inBasicParam)
                Case RealServiceFunctions.Consumables_Insert
                    Return Consumables.Insert(CType(inParam, Consumables.Parameter_Insert_ConsumableStock), inBasicParam)
                Case RealServiceFunctions.OpeningBalances_Insert
                    Return OpeningBalances.Insert(CType(inParam, OpeningBalances.Parameter_Insert_OpeningBalances), inBasicParam)
                Case RealServiceFunctions.Vehicles_Insert
                    Return Vehicles.Insert(CType(inParam, Vehicles.Parameter_Insert_Vehicles), inBasicParam)
                Case RealServiceFunctions.Vehicles_InsertTRIDAndTRSrNo
                    Return Vehicles.Insert(CType(inParam, Vehicles.Parameter_InsertTRIDAndTRSrNo_Vehicles), inBasicParam)
                Case RealServiceFunctions.Payments_InsertMasterInfo
                    Return Payments.InsertMasterInfo(CType(inParam, Payments.Parameter_InsertMasterInfo_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_Insert
                    Return Payments.Insert(CType(inParam, Payments.Parameter_Insert_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_InsertItem
                    Return Payments.InsertItem(CType(inParam, Payments.Parameter_InsertItem_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_InsertPayment
                    Return Payments.InsertPayment(CType(inParam, Payments.Parameter_InsertPayment_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_InsertPaymentMT
                    Return Payments.InsertPayment(CType(inParam, Payments.Parameter_InsertPaymentMT_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_InsertPurpose
                    Return Payments.InsertPurpose(CType(inParam, Payments.Parameter_InsertPurpose_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Payments_InsertOther
                    Return Payments.InsertOther(CType(inParam, Payments.Parameter_InsertOther_VoucherPayment), inBasicParam)
                Case RealServiceFunctions.Receipts_InsertMasterInfo
                    Return Receipts.InsertMasterInfo(CType(inParam, Receipts.Parameter_InsertMasterInfo_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.Receipts_Insert
                    Return Receipts.Insert(CType(inParam, Receipts.Parameter_Insert_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.Receipts_InsertItem
                    Return Receipts.InsertItem(CType(inParam, Receipts.Parameter_InsertItem_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.Receipts_InsertPurpose
                    Return Receipts.InsertPurpose(CType(inParam, Receipts.Parameter_InsertPurpose_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.Receipts_InsertPayment
                    Return Receipts.InsertPayment(CType(inParam, Receipts.Parameter_InsertAandLPayment_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.Receipts_InsertBankPayment
                    Return Receipts.InsertPayment(CType(inParam, Receipts.Parameter_InsertBankPayment_VoucherReceipt), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_Insert
                    Return Voucher_Donation.Insert(CType(inParam, Voucher_Donation.Parameter_Insert_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_InsertForeignInfo
                    Return Voucher_Donation.InsertForeignInfo(CType(inParam, Voucher_Donation.Parameter_InsertForeignInfo_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_InsertPurpose
                    Return Voucher_Donation.InsertPurpose(CType(inParam, Voucher_Donation.Parameter_InsertPurpose_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_InsertDonationStatus
                    Return Voucher_Donation.InsertDonationStatus(CType(inParam, Voucher_Donation.Parameter_InsertDonStatus_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_InsertMaster
                    Return Voucher_Internal_Transfer.InsertMaster(CType(inParam, Voucher_Internal_Transfer.Parameter_InsertMasterInfo_VoucherInternalTransfer), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_Insert
                    Return Voucher_Internal_Transfer.Insert(CType(inParam, Voucher_Internal_Transfer.Parameter_Insert_VoucherInternalTransfer), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_InsertPurpose
                    Return Voucher_Internal_Transfer.InsertPurpose(CType(inParam, Voucher_Internal_Transfer.Parameter_InsertPurpose_VoucherInternalTransfer), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_InsertMasterInfo
                    Return Voucher_SaleOfAsset.InsertMasterInfo(CType(inParam, Voucher_SaleOfAsset.Parameter_InsertMasterInfo_VoucherSaleOfAsset), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_Insert
                    Return Voucher_SaleOfAsset.Insert(CType(inParam, Voucher_SaleOfAsset.Parameter_Insert_VoucherSaleOfAsset), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_InsertItem
                    Return Voucher_SaleOfAsset.InsertItem(CType(inParam, Voucher_SaleOfAsset.Parameter_InsertItem_VoucherSaleOfAsset), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_InsertPurpose
                    Return Voucher_SaleOfAsset.InsertPurpose(CType(inParam, Voucher_SaleOfAsset.Parameter_InsertPurpose_VoucherSaleOfAsset), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_InsertPayment
                    Return Voucher_SaleOfAsset.InsertPayment(CType(inParam, Voucher_SaleOfAsset.Parameter_InsertAandLPayment_VoucherSaleOfAsset), inBasicParam)
                Case RealServiceFunctions.SaleOfAssets_InsertBankPayment
                    Return Voucher_SaleOfAsset.InsertPayment(CType(inParam, Voucher_SaleOfAsset.Parameter_InsertBankPayment_VoucherSaleOfAsset), inBasicParam)

                Case RealServiceFunctions.AssetTransfer_InsertMasterInfo
                    Return Voucher_AssetTransfer.InsertMasterInfo(CType(inParam, Voucher_AssetTransfer.Parameter_InsertMasterInfo_VoucherAssetTransfer), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_Insert
                    Return Voucher_AssetTransfer.Insert(CType(inParam, Voucher_AssetTransfer.Parameter_Insert_VoucherAssetTransfer), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_InsertItem
                    Return Voucher_AssetTransfer.InsertItem(CType(inParam, Voucher_AssetTransfer.Parameter_InsertItem_VoucherAssetTransfer), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_InsertPurpose
                    Return Voucher_AssetTransfer.InsertPurpose(CType(inParam, Voucher_AssetTransfer.Parameter_InsertPurpose_VoucherAssetTransfer), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_InsertPayment
                    Return Voucher_AssetTransfer.InsertPayment(CType(inParam, Voucher_AssetTransfer.Parameter_InsertAandLPayment_VoucherAssetTransfer), inBasicParam)


                Case RealServiceFunctions.Gift_Insert
                    Return Voucher_Gift.Insert(CType(inParam, Voucher_Gift.Parameter_Insert_VoucherGift), inBasicParam)
                Case RealServiceFunctions.Gift_InsertMasterInfo
                    Return Voucher_Gift.InsertMasterInfo(CType(inParam, Voucher_Gift.Parameter_InsertMasterInfo_VoucherGift), inBasicParam)
                Case RealServiceFunctions.Gift_InsertPurpose
                    Return Voucher_Gift.InsertPurpose(CType(inParam, Voucher_Gift.Parameter_InsertPurpose_VoucherGift), inBasicParam)
                Case RealServiceFunctions.Gift_InsertItem
                    Return Voucher_Gift.InsertItem(CType(inParam, Voucher_Gift.Parameter_InsertItem_VoucherGift), inBasicParam)
                Case RealServiceFunctions.BankToBank_Insert
                    Return Voucher_BankToBank.Insert(CType(inParam, Voucher_BankToBank.Parameter_Insert_Voucher_BankToBank), inBasicParam)
                Case RealServiceFunctions.CashWithdrawDeposit_Insert
                    Return Voucher_CashBank.Insert(CType(inParam, Voucher_CashBank.Parameter_Insert_Voucher_CashBank), inBasicParam)
                Case RealServiceFunctions.CollectionBox_Insert
                    Return Voucher_CollectionBox.Insert(CType(inParam, Voucher_CollectionBox.Parameter_Insert_Voucher_CollectionBox), inBasicParam)

                Case RealServiceFunctions.Journal_InsertMaster
                    Return Voucher_Journal.InsertMasterInfo(CType(inParam, Voucher_Journal.Parameter_InsertMasterInfo_VoucheJournal), inBasicParam)
                Case RealServiceFunctions.Journal_InsertTxn
                    Return Voucher_Journal.Insert(CType(inParam, Voucher_Journal.Parameter_Insert_VoucherJournal), inBasicParam)
                Case RealServiceFunctions.Journal_InsertPurpose
                    Return Voucher_Journal.InsertPurpose(CType(inParam, Voucher_Journal.Parameter_InsertPurpose_VoucherJournal), inBasicParam)

                Case RealServiceFunctions.Magazine_Insert
                    Return Magazine.Insert(CType(inParam, Magazine.Parameter_Insert_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Subs_Type
                    Return Magazine.Insert_Magazine_Subs_Type(CType(inParam, Magazine.Parameter_Insert_Magazine_Subs_Type), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Subs_Type_Fee
                    Return Magazine.Insert_Magazine_Subs_Type_Fee(CType(inParam, Magazine.Parameter_Insert_Magazine_Subs_Type_Fee), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Dispatch_Type
                    Return Magazine.Insert_Magazine_Dispatch_Type(CType(inParam, Magazine.Parameter_Insert_Magazine_Dispatch_Type), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Dispatch_Type_Charges
                    Return Magazine.Insert_Magazine_Dispatch_Type_Charges(CType(inParam, Magazine.Parameter_Insert_Magazine_Dispatch_Type_Charges), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Membership
                    Return Magazine.Insert_Magazine_Membership(CType(inParam, Magazine.Parameter_Insert_Magazine_Membership_Profile), inBasicParam)
                Case RealServiceFunctions.Voucher_Magazine_Register_InsertReceipt
                    Return Voucher_Magazine_Register.InsertReceipt(CType(inParam, Voucher_Magazine_Register.Param_InsertReceipt_Magazine_Receipt_Register), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_SubCity
                    Return Magazine.Insert_Magazine_SubCity(CType(inParam, Magazine.Parameter_Insert_Magazine_Subcity), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Dispatch
                    Return Magazine.Insert_Magazine_Dispatch(CType(inParam, Magazine.Parameter_Insert_Magazine_Dispatch), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Dispatch_bundles
                    Return Magazine.Insert_Magazine_Dispatch_Bundles(CType(inParam, Magazine.Parameter_Insert_Magazine_Dispatch_Bundles), inBasicParam)

                Case RealServiceFunctions.Magazine_Insert_Dispatch_New_Voucher
                    Return Magazine.Insert_Dispatches_for_new_Voucher(CType(inParam, Magazine.Parameter_Insert_dispatch_New_Voucher), inBasicParam)

                Case RealServiceFunctions.Magazine_Insert_Membership_Request
                    Return Magazine_Request_Register.Insert_Magazine_Membership_Request(CType(inParam, Magazine_Request_Register.Parameter_Insert_Magazine_Membership_Request), inBasicParam)
                Case RealServiceFunctions.Magazine_Add_Client_Restrictions
                    Return Magazine.Add_Magazine_Restrictions(CType(inParam, Magazine.Parameter_Insert_Magazine_Client_Restriction), inBasicParam)
                Case RealServiceFunctions.Magazine_Insert_Magazine_Issue
                    Return Magazine.Insert_Magazine_Issue(CType(inParam, Magazine.Param_Insert_Magazine_Issue), inBasicParam)

                Case RealServiceFunctions.Magazine_Insert_Magazine_Similar_Issues
                    Return Magazine.Insert_Magazine_Similar_Issues(CType(inParam, Magazine.Param_Insert_Magazine_Similar_Issues), inBasicParam)
                    'Magazine Register
                    'Case RealServiceFunctions.Voucher_Magazine_Register_InsertMaster
                    '    Return Voucher_Magazine_Register.InsertMasterInfo(CType(inParam, Voucher_Magazine_Register.Parameter_InsertMaster_VoucherMagazineMembership), inBasicParam)
                    'Case RealServiceFunctions.Voucher_Magazine_Register_Insert
                    '    Return Voucher_Magazine_Register.Insert(CType(inParam, Voucher_Magazine_Register.Parameter_Insert_VoucherMagazineMembership), inBasicParam)
                    'Case RealServiceFunctions.Voucher_Magazine_Register_InsertBalances
                    '    Return Voucher_Magazine_Register.InsertBalances(CType(inParam, Voucher_Magazine_Register.Parameter_InsertBalances_VoucherMagazineMembership), inBasicParam)
                    'Case RealServiceFunctions.Voucher_Magazine_Register_InsertPayment
                    '    Return Voucher_Magazine_Register.InsertPayment(CType(inParam, Voucher_Magazine_Register.Parameter_InsertPayment_VoucherMagazineMembership), inBasicParam)
                    'Case RealServiceFunctions.Voucher_Magazine_Register_InsertPurpose
                    '    Return Voucher_Magazine_Register.InsertPurpose(CType(inParam, Voucher_Magazine_Register.Parameter_InsertPurpose_VoucherMagazineMembership), inBasicParam)
                Case RealServiceFunctions.Membership_InsertSubscriptionList
                    Return Membership.InsertSubscriptionList(CType(inParam, Membership.Param_Inset_SubcriptionList), inBasicParam)
                Case RealServiceFunctions.Membership_InsertSubscriptionFee
                    Return Membership.InsertSubscriptionFee(CType(inParam, Membership.Param_Insert_SubscriptionFee), inBasicParam)

                Case RealServiceFunctions.Notebook_Insert
                    Return Notebook.Insert(CType(inParam, Notebook.Parameter_Insert_NoteBook), inBasicParam)
                Case RealServiceFunctions.Notebook_InsertAllEntries
                    Return Notebook.Insert(CType(inParam, Notebook.Param_InsertAllEntries_Notebook), inBasicParam)
                Case RealServiceFunctions.Livestock_Insert
                    Return Livestock.Insert(CType(inParam, Livestock.Parameter_Insert_LiveStock), inBasicParam)
                Case RealServiceFunctions.Livestock_InsertTRIDAndTRSrNo
                    Return Livestock.Insert(CType(inParam, Livestock.Parameter_InsertTRIDAndTRSrNo_LiveStock), inBasicParam)
                Case RealServiceFunctions.Telephone_Insert
                    Return Telephones.Insert(CType(inParam, Telephones.Parameter_Insert_Telephones), inBasicParam)
                Case RealServiceFunctions.Request_Insert
                    Return Request.Insert(CType(inParam, Request.Parameter_Insert_Request), inBasicParam)
                Case RealServiceFunctions.Letters_Insert
                    Return Letters.Insert(CType(inParam, Letters.Parameter_Insert_Letters), inBasicParam)

                Case RealServiceFunctions.Notes_AddQuickNote
                    Return Notes.AddQuickNote(CType(inParam, Notes.Param_AddQuickNote_Notes), inBasicParam)
                Case RealServiceFunctions.Notes_Insert
                    Return Notes.Insert(CType(inParam, Notes.Parameter_Insert_Notes), inBasicParam)
                    'stock

                Case RealServiceFunctions.StockProfile_AddStockProfile
                    Return StockProfile.AddStockProfile(CType(inParam, StockProfile.Param_Add_StockProfile), inBasicParam)
                     'User Order
                Case RealServiceFunctions.UserOrder_Insert_UO_Item
                    Return StockUserOrder.Insert_UO_Item(CType(inParam, StockUserOrder.Param_Insert_UO_Item), inBasicParam)
                Case RealServiceFunctions.UserOrder_Insert_UO_Item_Delivered
                    Return StockUserOrder.Insert_UO_Item_Delivered(CType(inParam, StockUserOrder.Param_Insert_UO_Item_Delivered), inBasicParam)
                    Return StockUserOrder.Insert_UO_Item(CType(inParam, StockUserOrder.Param_Insert_UO_Item), inBasicParam)
                'Case RealServiceFunctions.UserOrder_Insert_UO_Item_Delivered_Stocks
                '    Return StockUserOrder.Insert_UO_Item_Delivered_Stocks(CType(inParam, StockUserOrder.Param_Insert_UO_Item_Delivered_Stocks), inBasicParam)

                Case RealServiceFunctions.UserOrder_Insert_UO_Item_Received
                    Return StockUserOrder.Insert_UO_Item_Received(CType(inParam, StockUserOrder.Param_Insert_UO_Item_Received), inBasicParam)
                Case RealServiceFunctions.UserOrder_Insert_UO_Item_Returned
                    Return StockUserOrder.Insert_UO_Item_Returned(CType(inParam, StockUserOrder.Param_Insert_UO_Item_Returned), inBasicParam)
                Case RealServiceFunctions.UserOrder_Insert_UO_Item_Return_Received
                    Return StockUserOrder.Insert_UO_Item_Return_Received(CType(inParam, StockUserOrder.Param_Insert_UO_Item_Return_Received), inBasicParam)
                Case RealServiceFunctions.StockProfile_AddStockAddition
                    Return StockProfile.AddStockAddition(CType(inParam, StockProfile.Param_Add_Stock_Addition), inBasicParam)

                Case RealServiceFunctions.UserOrder_InsertUOGoodsDeliverAllPending
                    Return StockUserOrder.InsertUOGoodsDeliverAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsDeliverAllPending), inBasicParam)
                Case RealServiceFunctions.UserOrder_InsertUOGoodsReceiveAllPending
                    Return StockUserOrder.InsertUOGoodsReceiveAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsReceiveAllPending), inBasicParam)
                Case RealServiceFunctions.UserOrder_InsertUOGoodsReturnAllPending
                    Return StockUserOrder.InsertUOGoodsReturnAllPending(CType(inParam, StockUserOrder.Param_GetUOGoodsReturnAllPending), inBasicParam)
                Case RealServiceFunctions.UserOrder_InsertUORRMapping
                    Return StockUserOrder.InsertUORRMapping(CType(inParam, StockUserOrder.Param_UO_Insert_UO_RR_Mapping), inBasicParam)

                Case RealServiceFunctions.StockDeptStores_InsertStoreDept
                    Return StockDeptStores.InsertStoreDept(CType(inParam, StockDeptStores.Param_InsertStoreDept), inBasicParam)

                Case RealServiceFunctions.Personnels_InsertPersonnel
                    Return Personnels.InsertPersonnel(CType(inParam, Personnels.Param_InsertPersonnel), inBasicParam)
                Case RealServiceFunctions.Personnels_InsertPersonnelCharges
                    Return Personnels.InsertPersonnelCharges(CType(inParam, Personnels.Param_InsertPersonnelCharges), inBasicParam)

                Case RealServiceFunctions.StockMachineToolAllocation_InsertMachineToolIssue
                    Return StockMachineToolAllocation.InsertMachineToolIssue(CType(inParam, StockMachineToolAllocation.Param_Insert_MachineTool_Issue), inBasicParam)
                Case RealServiceFunctions.StockMachineToolAllocation_InsertMachineToolReturn
                    Return StockMachineToolAllocation.InsertMachineToolReturn(CType(inParam, StockMachineToolAllocation.Param_Insert_MachineTool_Return), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderGoodsReceived
                    Return StockPurchaseOrder.InsertPurchaseOrderGoodsReceived(CType(inParam, StockPurchaseOrder.Param_InsertPurchaseOrderGoodsReceived), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderGoodsReturned
                    Return StockPurchaseOrder.InsertPurchaseOrderGoodsReturned(CType(inParam, StockPurchaseOrder.Param_InsertPurchaseOrderGoodsReturned), inBasicParam)
                Case RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderPayment
                    Return StockPurchaseOrder.InsertPurchaseOrderPayment(CType(inParam, StockPurchaseOrder.Param_InsertPurchaseOrderPayment), inBasicParam)
                    'subitem
                Case RealServiceFunctions.SubItem_InsertItemProperties
                    Return SubItem.InsertItemProperties(CType(inParam, SubItem.Param_SubItem_Insert_Item_Properties), inBasicParam)
                Case RealServiceFunctions.SubItem_InsertItemUnitconversion
                    Return SubItem.InsertItemUnitconversion(CType(inParam, SubItem.Param_SubItem_Insert_Unit_Conversion), inBasicParam)

                    'Suppliers
                Case RealServiceFunctions.Suppliers_InsertItemSupplierMapping
                    Return Suppliers.InsertItemSupplierMapping(CType(inParam, Suppliers.Param_InsertItemSupplierMapping), inBasicParam)
                Case RealServiceFunctions.Suppliers_InsertSupplierBank
                    Return Suppliers.InsertSupplierBank(CType(inParam, Suppliers.Param_InsertsupplierBank), inBasicParam)

                    'ServicePlaces
                Case RealServiceFunctions.ServicePlaces_Insert
                    Return ServicePlaces.Insert(CType(inParam, ServicePlaces.Parameter_Insert_ServicePlaces), inBasicParam)
                Case RealServiceFunctions.ServiceReport_Insert
                    Return ServiceReport.Insert(CType(inParam, ServiceReport.Parameter_Insert_ServiceReport), inBasicParam)
                Case RealServiceFunctions.ServiceReport_InsertWings
                    Return ServiceReport.InsertWings(CType(inParam, ServiceReport.Parameter_InsertWings_ServiceReport), inBasicParam)
                Case RealServiceFunctions.ServiceReport_InsertGuest
                    Return ServiceReport.InsertGuest(CType(inParam, ServiceReport.Parameter_InsertGuest_ServiceReport), inBasicParam)

                    'ServiceMaterial
                Case RealServiceFunctions.ServiceMaterial_Insert
                    Return ServiceMaterial.Insert(CType(inParam, ServiceMaterial.Parameter_Insert_ServiceMaterial), inBasicParam)
                Case RealServiceFunctions.ServiceMaterial_InsertServiceMaterial_Txn
                    Return ServiceMaterial.InsertServiceMaterial_Txn(CType(inParam, ServiceMaterial.Param_Txn_Insert_ServiceMaterial), inBasicParam)

                    'Membership
                Case RealServiceFunctions.Membership_Insert
                    Return Membership.Insert(CType(inParam, Membership.Parameter_Insert_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_InsertBalances
                    Return Membership.InsertBalances(CType(inParam, Membership.Parameter_InsertBalances_Membership), inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_InsertReceipt
                    Return Membership_Receipt_Register.InsertReceipt(CType(inParam, Membership_Receipt_Register.Param_InsertReceipt_Membership_Receipt_Register), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_InsertMasterInfo
                    Return Voucher_Membership.InsertMasterInfo(CType(inParam, Voucher_Membership.Parameter_InsertMasterInfo_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_Insert
                    Return Voucher_Membership.Insert(CType(inParam, Voucher_Membership.Parameter_Insert_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_InsertItem
                    Return Voucher_Membership.InsertItem(CType(inParam, Voucher_Membership.Parameter_InsertItem_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_InsertPayment
                    Return Voucher_Membership.InsertPayment(CType(inParam, Voucher_Membership.Parameter_InsertPayment_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_InsertPurpose
                    Return Voucher_Membership.InsertPurpose(CType(inParam, Voucher_Membership.Parameter_InsertPurpose_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_InsertMasterInfo
                    Return Voucher_Membership_Renewal.InsertMasterInfo(CType(inParam, Voucher_Membership_Renewal.Parameter_InsertMasterInfo_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_Insert
                    Return Voucher_Membership_Renewal.Insert(CType(inParam, Voucher_Membership_Renewal.Parameter_Insert_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_InsertItem
                    Return Voucher_Membership_Renewal.InsertItem(CType(inParam, Voucher_Membership_Renewal.Parameter_InsertItem_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_InsertPayment
                    Return Voucher_Membership_Renewal.InsertPayment(CType(inParam, Voucher_Membership_Renewal.Parameter_InsertPayment_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_InsertPurpose
                    Return Voucher_Membership_Renewal.InsertPurpose(CType(inParam, Voucher_Membership_Renewal.Parameter_InsertPurpose_VoucherMembershipRenewal), inBasicParam)
                    'TDS
                Case RealServiceFunctions.TDS_InsertTDSDeduction
                    Return TDS.InsertTDSDeductionMapping(CType(inParam, Voucher_Internal_Transfer.Parameter_InsertTDSDeduction_VoucherInternalTransfer()), inBasicParam)
                    'Complexes
                Case RealServiceFunctions.Complexes_Insert
                    Return Complexes.Insert(CType(inParam, Complexes.Param_Insert_Complexes), inBasicParam)
                Case RealServiceFunctions.Complexes_Insert_Building
                    Return Complexes.Insert_Building(CType(inParam, Complexes.Param_InsertBuilding_Complexes), inBasicParam)

                    'WIP
                Case RealServiceFunctions.WIP_Profile_Insert
                    Return WIP_Profile.Insert(CType(inParam, WIP_Profile.Param_Insert_WIP_Profile), inBasicParam)
                Case RealServiceFunctions.WIP_Profile_InsertTRIDAndTRSrNo
                    Return WIP_Profile.Insert(CType(inParam, WIP_Profile.Param_InsertTRIDAndTRSrNo_WIP_Profile), inBasicParam)
                    '    'WIP Finalization
                    'Case RealServiceFunctions.Voucher_WIP_Finalization_InsertMasterInfo
                    '    Return Voucher_WIP_Finalization.InsertMasterInfo(CType(inParam, Voucher_WIP_Finalization.Param_InsertMasterInfo_Voucher_WIPFinalization), inBasicParam)
                    'Case RealServiceFunctions.Voucher_WIP_Finalization_Insert
                    '    Return Voucher_WIP_Finalization.Insert(CType(inParam, Voucher_WIP_Finalization.Param_Insert_Voucher_WIPFinalization), inBasicParam)
                    'Case RealServiceFunctions.Addresses_InsertUserProfile
                    '    Return Addresses.InsertUserProfile_Txn(CType(inParam, Addresses.Param_Txn_Insert_UserProfile), inBasicParam)
            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Update Function"),
        XmlInclude(GetType(DataFunctions.Param_UpdateOpeningBalance)),
        XmlInclude(GetType(Action_Items.Parameter_Update_Action_Items)),
        XmlInclude(GetType(Action_Items.Parameter_Close_Action_Items)),
        XmlInclude(GetType(BankAccounts.Parameter_Update_BankandBalance_BankAccounts)),
        XmlInclude(GetType(BankAccounts.Param_Bank_Close)),
        XmlInclude(GetType(BankAccounts.Param_Reopen_BankAccounts)),
        XmlInclude(GetType(Addresses.Parameter_Update_Addresses)),
        XmlInclude(GetType(ClientUserInfo.Parameter_CPwd_ClientUserInfo)),
        XmlInclude(GetType(Core.Parameter_Update_Core)),
        XmlInclude(GetType(Advances.Parameter_Update_Advances)),
        XmlInclude(GetType(Liabilities.Parameter_Update_Liabilities)),
        XmlInclude(GetType(LandAndBuilding.Parameter_Update_LandAndBuilding)),
        XmlInclude(GetType(Cash.Parameter_Update_Cash)),
        XmlInclude(GetType(Deposits.Parameter_Update_Deposits)),
        XmlInclude(GetType(FD.Parameter_UpdateandUpdateBalance_FD)),
        XmlInclude(GetType(Voucher_FD.Parameter_UpdateFD_VoucherFD)),
        XmlInclude(GetType(Voucher_FD.Parameter_UpdateFDHistory_VoucherFD)),
        XmlInclude(GetType(Voucher_FD.Parameter_UpdateMasterInfo_VoucherFD)),
        XmlInclude(GetType(Assets.Parameter_Update_Assets)),
        XmlInclude(GetType(Assets.Parameter_Update_Assets_Location)),
        XmlInclude(GetType(AssetLocations.Param_AssetLoc_Update)),
        XmlInclude(GetType(GoldSilver.Parameter_Update_GoldSilver)),
        XmlInclude(GetType(Consumables.Parameter_Update_ConsumableStock)),
        XmlInclude(GetType(OpeningBalances.Parameter_Update_OpeningBalances)),
        XmlInclude(GetType(Vehicles.Parameter_Update_Vehicles)),
        XmlInclude(GetType(Payments.Parameter_UpdateMaster_VoucherPayment)),
        XmlInclude(GetType(Receipts.Parameter_UpdateMaster_VoucherReceipt)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Subs_Type)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Subs_Type_Fee)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Dispatch_Type)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Dispatch)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Dispatch_Type_Charges)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Membership_Profile)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Membership_Identity)),
        XmlInclude(GetType(Voucher_Donation.Parameter_Update_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_UpdatePurpose_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_UpdateForeignInfo_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Donation.Parameter_UpdateStatus_Voucher_Donation)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_UpdateMasterInfo_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_Update_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_Update_CrossReference)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_MatchTransfers)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_UnMatchTransfers)),
        XmlInclude(GetType(Voucher_Internal_Transfer.Parameter_UpdatePurpose_VoucherInternalTransfer)),
        XmlInclude(GetType(Voucher_SaleOfAsset.Parameter_UpdateMasterInfo_VoucherSaleOfAsset)),
        XmlInclude(GetType(Voucher_AssetTransfer.Parameter_UpdateMasterInfo_VoucherAssetTransfer)),
        XmlInclude(GetType(Voucher_AssetTransfer.Param_VoucherAssetTransfer_Update_CrossReference)),
        XmlInclude(GetType(Voucher_Gift.Parameter_UpdateMaster_VoucherGift)),
        XmlInclude(GetType(Voucher_BankToBank.Parameter_Update_Voucher_BankToBank)),
        XmlInclude(GetType(Voucher_CashBank.Parameter_Update_Voucher_CashBank)),
        XmlInclude(GetType(Voucher_CollectionBox.Parameter_Update_Voucher_CollectionBox)),
        XmlInclude(GetType(Notebook.Parameter_Update_NoteBook)),
        XmlInclude(GetType(Livestock.Parameter_Update_LiveStock)),
        XmlInclude(GetType(Telephones.Parameter_Update_Telephones)),
        XmlInclude(GetType(Request.Parameter_Update_Request)),
        XmlInclude(GetType(Voucher_Journal.Parameter_UpdateMaster_VoucherJournal)),
        XmlInclude(GetType(Letters.Parameter_Update_Letters)),
        XmlInclude(GetType(Notes.Parameter_Update_Notes)),
        XmlInclude(GetType(ServicePlaces.Parameter_Update_ServicePlaces)),
        XmlInclude(GetType(ServiceReport.Parameter_Update_ServiceReport)),
        XmlInclude(GetType(Membership.Parameter_Update_Membership)),
        XmlInclude(GetType(Membership.Parameter_Close_Membership)),
        XmlInclude(GetType(Magazine.Parameter_AutoRenewal_Membership)),
         XmlInclude(GetType(Complexes.Param_Update_Complexes)),
        XmlInclude(GetType(WIP_Profile.Parameter_Update_WIP_Profile)),
        XmlInclude(GetType(Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register)),
        XmlInclude(GetType(Voucher_Magazine_Register.Param_DeleteReceipt_Magazine_Receipt_Register)),
        XmlInclude(GetType(Voucher_Membership.Parameter_UpdateMaster_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership.Parameter_Update_VoucherMembership)),
        XmlInclude(GetType(Voucher_WIP_Finalization.Parameter_UpdateMaster_Voucher_WIPFinalization)),
        XmlInclude(GetType(Voucher_Membership.Parameter_UpdatePurpose_VoucherMembership)),
        XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_UpdateMaster_VoucherMembershipRenewal)),
        XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_Update_VoucherMembershipRenewal)),
        XmlInclude(GetType(LandAndBuilding.Parameter_Update_Insurance_Register)),
        XmlInclude(GetType(Magazine.Parameter_Update_Magazine_Subcity)),
        XmlInclude(GetType(Magazine_Request_Register.Parameter_Update_Magazine_Membership_Request)),
        XmlInclude(GetType(Magazine_Request_Register.Parameter_Update_Magazine_Request_status)),
        XmlInclude(GetType(Reports_All.Param_UpdateClearingDate)),
        XmlInclude(GetType(Telephones.Parameter_Close_Telephones)),
        XmlInclude(GetType(Deposit_Slip.Param_MarkDepositSlipAsPrinted)),
        XmlInclude(GetType(Voucher_Magazine_Register.Param_Update_Magazine_Disp_CC)),
        XmlInclude(GetType(Magazine.Param_Update_Magazine_Issue)),
        XmlInclude(GetType(Center_Purpose_Info.Parameter_Update_Center_Purpose)),
        XmlInclude(GetType(Membership.Param_Update_SubscirptionList)),
        XmlInclude(GetType(Membership.Param_Update_SubscriptionFee)),
        XmlInclude(GetType(Membership.Param_Delet_SubscriptionFee)),
         XmlInclude(GetType(Attachments.Parameter_Update_Attachment)),
         XmlInclude(GetType(StockProfile.Param_Update_StockProfile)),
         XmlInclude(GetType(StockProfile.Param_Update_StockProject)),
         XmlInclude(GetType(StockProfile.Param_Update_StockLocation)),
         XmlInclude(GetType(SubItem.Param_CloseSubItem)),
         XmlInclude(GetType(SubItem.Param_SubItem_Update_Store_Mapping)),
         XmlInclude(GetType(Projects.Param_Update_Project_Status)),
         XmlInclude(GetType(Jobs.Param_Update_Job_Status)),
         XmlInclude(GetType(StockUserOrder.Param_Update_UO_Status)),
         XmlInclude(GetType(StockUserOrder.Param_Update_UO_Scheduled_Delivery)),
         XmlInclude(GetType(StockDeptStores.Param_UpdateStoreDept)),
         XmlInclude(GetType(StockDeptStores.Param_CloseStoreDept)),
         XmlInclude(GetType(Personnels.Param_Mark_Personnel_asLeft)),
         XmlInclude(GetType(Personnels.Param_UpdatePersonnel)),
         XmlInclude(GetType(Personnels.Param_UpdatePersonnelCharges)),
         XmlInclude(GetType(StockMachineToolAllocation.Param_Update_MachineTool_Issue)),
         XmlInclude(GetType(StockMachineToolAllocation.Param_Update_MachineTool_Return)),
         XmlInclude(GetType(Suppliers.Param_UpdateItemSupplierMapping)),
         XmlInclude(GetType(ClientUserInfo.Param_UpdateClientUser)),
         XmlInclude(GetType(ClientUserInfo.Param_UpdateClientUserGroup)),
         XmlInclude(GetType(ClientUserInfo.Param_UpdateClientUserPrivileges)),
         XmlInclude(GetType(StockRequisitionRequest.Param_Update_RR_Status)),
         XmlInclude(GetType(StockPurchaseOrder.Param_UpdatePurchaseOrderStatus)),
         XmlInclude(GetType(Attachments.Parameter_Attachment_Mark_as_Rejected)),
        XmlInclude(GetType(Voucher_Membership_Renewal.Parameter_UpdatePurpose_VoucherMembershipRenewal)),
        XmlInclude(GetType(ServiceModule.Parameter_Update_ServiceModule))>
        <XmlInclude(GetType(DBNull))>
        Public Function Wrap_Update(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal upParam As Object) As Boolean
            Select Case FunctionCalled
                Case RealServiceFunctions.Data_UpdateOpeningBalance
                    Return DataFunctions.UpdateOpeningBalance(CType(upParam, DataFunctions.Param_UpdateOpeningBalance), inBasicParam)
                    'ActionItems
                Case RealServiceFunctions.ActionItems_Update
                    Return Action_Items.Update(CType(upParam, Action_Items.Parameter_Update_Action_Items), inBasicParam)
                Case RealServiceFunctions.ActionItems_UpdateCentreRemarks
                    Return Action_Items.UpdateCentreRemarks(CType(upParam, Action_Items.Parameter_Update_Action_Items), inBasicParam)
                Case RealServiceFunctions.ActionItems_Close
                    Return Action_Items.Close(CType(upParam, Action_Items.Parameter_Close_Action_Items), inBasicParam)
                Case RealServiceFunctions.Attachments_Mark_as_Checked
                    Return Attachments.Mark_as_Checked(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Attachments_Mark_as_Unchecked
                    Return Attachments.Mark_as_UnChecked(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Attachments_Mark_as_Rejected
                    Return Attachments.Mark_as_Rejected(CType(upParam, Attachments.Parameter_Attachment_Mark_as_Rejected), inBasicParam)
                Case RealServiceFunctions.Attachments_Update
                    Return Attachments.Update(CType(upParam, Attachments.Parameter_Update_Attachment), inBasicParam)
                    'AddressBook Related
                Case RealServiceFunctions.AddressBookRelated_Addresses_Update
                    Return Addresses.Update(CType(upParam, Addresses.Parameter_Update_Addresses), inBasicParam)
                    'Advances
                Case RealServiceFunctions.Advances_Update
                    Return Advances.Update(CType(upParam, Advances.Parameter_Update_Advances), inBasicParam)
                    'Assets
                Case RealServiceFunctions.Assets_UpdateAssetLocationIfNotPresent
                    Return Assets.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Assets_Update
                    Return Assets.Update(CType(upParam, Assets.Parameter_Update_Assets), inBasicParam)
                Case RealServiceFunctions.Assets_UpdateLocation
                    Return Assets.UpdateLocation(CType(upParam, Assets.Parameter_Update_Assets_Location), inBasicParam)
                Case RealServiceFunctions.Assets_UpdateInsuranceValue
                    Return Assets.UpdateInsuranceValue(CType(upParam, Assets.Parameter_Update_Assets), inBasicParam)
                Case RealServiceFunctions.Audit_UpdtaeDocumentMapResponse
                    Return Audit.UpdtaeDocumentMapResponse(CType(upParam, Audit.Param_InsertDocumentMapResponse), inBasicParam)

                    'AssetLocations
                Case RealServiceFunctions.AssetLocations_UpdateByReference
                    Return AssetLocations.UpdateByReference(CType(upParam, AssetLocations.Param_AssetLoc_Update), inBasicParam)
                Case RealServiceFunctions.AssetLocations_UpdateMatching
                    Return AssetLocations.UpdateMatching(CType(upParam, AssetLocations.Param_AssetLoc_Update), inBasicParam)
                Case RealServiceFunctions.AssetLocations_Update
                    Return AssetLocations.Update_Global(CType(upParam, AssetLocations.Param_AssetLoc_Update), inBasicParam)
                    'Bank
                Case RealServiceFunctions.Bank_Update_Bank_and_Balance
                    Return BankAccounts.Update_Bank_and_Balance(CType(upParam, BankAccounts.Parameter_Update_BankandBalance_BankAccounts), inBasicParam)
                Case RealServiceFunctions.Bank_Close
                    Return BankAccounts.Close(CType(upParam, BankAccounts.Param_Bank_Close), inBasicParam)
                Case RealServiceFunctions.Bank_Reopen
                    Return BankAccounts.Reopen(CType(upParam, BankAccounts.Param_Reopen_BankAccounts), inBasicParam)
                    'BankToBank
                Case RealServiceFunctions.BankToBank_Update
                    Return Voucher_BankToBank.Update(CType(upParam, Voucher_BankToBank.Parameter_Update_Voucher_BankToBank), inBasicParam)
                    'Cash
                Case RealServiceFunctions.Cash_Update
                    Return Cash.Update(CType(upParam, Cash.Parameter_Update_Cash), inBasicParam)
                    'Cash Withdraw Deposit
                Case RealServiceFunctions.CashWithdrawDeposit_Update
                    Return Voucher_CashBank.Update(CType(upParam, Voucher_CashBank.Parameter_Update_Voucher_CashBank), inBasicParam)
                    'CentreRelated
                Case RealServiceFunctions.CentreRelated_Core_Update
                    Return Core.Update(CType(upParam, Core.Parameter_Update_Core), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_ChangePassword
                    Return ClientUserInfo.ChangePassword(CType(upParam, ClientUserInfo.Parameter_CPwd_ClientUserInfo), inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_SubmitReport
                    Return Center.SubmitReport(inBasicParam)
                Case RealServiceFunctions.CentreRelated_Center_LogOut
                    Return Center.LogOut(inBasicParam)
                Case RealServiceFunctions.CenterPurpose_Update
                    Return Center_Purpose_Info.Update(CType(upParam, Center_Purpose_Info.Parameter_Update_Center_Purpose), inBasicParam)
                Case RealServiceFunctions.CenterPurpose_Activate
                    Return Center_Purpose_Info.Activate(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.CenterPurpose_DeActivate
                    Return Center_Purpose_Info.DeActivate(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientUserInfo_UpdateClientUser
                    Return ClientUserInfo.UpdateClientUser(CType(upParam, ClientUserInfo.Param_UpdateClientUser), inBasicParam)
                Case RealServiceFunctions.CentreRelated_ClientGroupInfo_UpdateClientUserGroup
                    Return ClientUserInfo.UpdateClientUserGroup(CType(upParam, ClientUserInfo.Param_UpdateClientUserGroup), inBasicParam)

                Case RealServiceFunctions.CentreRelated_ClientUserInfo_UpdateClientUserPrivileges
                    Return ClientUserInfo.UpdateClientUserPrivileges(CType(upParam, ClientUserInfo.Param_UpdateClientUserPrivileges), inBasicParam)

                    'CollectionBox
                Case RealServiceFunctions.CollectionBox_Update
                    Return Voucher_CollectionBox.Update(CType(upParam, Voucher_CollectionBox.Parameter_Update_Voucher_CollectionBox), inBasicParam)
                    'Complexes
                Case RealServiceFunctions.Complexes_Update
                    Return Complexes.Update(CType(upParam, Complexes.Param_Update_Complexes), inBasicParam)
                    'Consumables
                Case RealServiceFunctions.Consumables_UpdateAssetLocationIfNotPresent
                    Return Consumables.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Consumables_Update
                    Return Consumables.Update(CType(upParam, Consumables.Parameter_Update_ConsumableStock), inBasicParam)
                    'Deposits
                Case RealServiceFunctions.Deposits_Update
                    Return Deposits.Update(CType(upParam, Deposits.Parameter_Update_Deposits), inBasicParam)
                    'Deposit Slips
                Case RealServiceFunctions.Deposit_Slip_MarkAsPrinted
                    Return Deposit_Slip.MarkAsPrinted(CType(upParam, Deposit_Slip.Param_MarkDepositSlipAsPrinted), inBasicParam)
                Case RealServiceFunctions.Deposit_Slip_MarkAsUnPrinted
                    Return Deposit_Slip.MarkAsUnPrinted(CType(upParam, String), inBasicParam)
                    'Donation
                Case RealServiceFunctions.VoucherDonation_Update
                    Return Voucher_Donation.Update(CType(upParam, Voucher_Donation.Parameter_Update_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_UpdatePurpose
                    Return Voucher_Donation.UpdatePurpose(CType(upParam, Voucher_Donation.Parameter_UpdatePurpose_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_UpdateForeignInfo
                    Return Voucher_Donation.UpdateForeignInfo(CType(upParam, Voucher_Donation.Parameter_UpdateForeignInfo_Voucher_Donation), inBasicParam)
                Case RealServiceFunctions.VoucherDonation_UpdateStatus
                    Return Voucher_Donation.UpdateStatus(CType(upParam, Voucher_Donation.Parameter_UpdateStatus_Voucher_Donation), inBasicParam)
                    'FDs
                Case RealServiceFunctions.FDs_Update_and_Update_Balance
                    Return FD.Update_and_Update_Balance(CType(upParam, FD.Parameter_UpdateandUpdateBalance_FD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_UpdateFD
                    Return Voucher_FD.UpdateFD(CType(upParam, Voucher_FD.Parameter_UpdateFD_VoucherFD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_UpdateFDHistory
                    Return Voucher_FD.UpdateFDHistory(CType(upParam, Voucher_FD.Parameter_UpdateFDHistory_VoucherFD), inBasicParam)
                Case RealServiceFunctions.FDs_VoucherFD_UpdateMasterInfo
                    Return Voucher_FD.UpdateMasterInfo(CType(upParam, Voucher_FD.Parameter_UpdateMasterInfo_VoucherFD), inBasicParam)
                    'Gift
                Case RealServiceFunctions.Gift_UpdateMaster
                    Return Voucher_Gift.UpdateMaster(CType(upParam, Voucher_Gift.Parameter_UpdateMaster_VoucherGift), inBasicParam)
                    'GoldSilver
                Case RealServiceFunctions.GoldSilver_UpdateAssetLocationIfNotPresent
                    Return GoldSilver.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.GoldSilver_Update
                    Return GoldSilver.Update(CType(upParam, GoldSilver.Parameter_Update_GoldSilver), inBasicParam)
                    'InternalTransfer
                Case RealServiceFunctions.InternalTransfer_UpdateMaster
                    Return Voucher_Internal_Transfer.UpdateMaster(CType(upParam, Voucher_Internal_Transfer.Parameter_UpdateMasterInfo_VoucherInternalTransfer), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_Update
                    Return Voucher_Internal_Transfer.Update(CType(upParam, Voucher_Internal_Transfer.Parameter_Update_VoucherInternalTransfer), inBasicParam)
                    'Case RealServiceFunctions.InternalTransfer_Update_CrossReference
                    'Return Voucher_Internal_Transfer.Update_CrossReference(CType(upParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_Update_CrossReference), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_MatchTransfers
                    Return Voucher_Internal_Transfer.MatchTransfers(CType(upParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_MatchTransfers), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_UnMatchTransfers
                    Return Voucher_Internal_Transfer.UnMatchTransfers(CType(upParam, Voucher_Internal_Transfer.Param_Voucher_InternalTransfer_UnMatchTransfers), inBasicParam)
                Case RealServiceFunctions.InternalTransfer_UpdatePurpose
                    Return Voucher_Internal_Transfer.UpdatePurpose(CType(upParam, Voucher_Internal_Transfer.Parameter_UpdatePurpose_VoucherInternalTransfer), inBasicParam)
                    'Journal
                Case RealServiceFunctions.Journal_UpdateMaster
                    Return Voucher_Journal.UpdateMaster(CType(upParam, Voucher_Journal.Parameter_UpdateMaster_VoucherJournal), inBasicParam)

                    'Job 
                Case RealServiceFunctions.Jobs_UpdateJobStatus
                    Return Jobs.UpdateJobStatus(CType(upParam, Jobs.Param_Update_Job_Status), inBasicParam)


                    'Letters
                Case RealServiceFunctions.Letters_Update
                    Return Letters.Update(CType(upParam, Letters.Parameter_Update_Letters), inBasicParam)
                    'Liabilities
                Case RealServiceFunctions.Liabilities_Update
                    Return Liabilities.Update(CType(upParam, Liabilities.Parameter_Update_Liabilities), inBasicParam)
                    'LiveStock
                Case RealServiceFunctions.Livestock_UpdateAssetLocationIfNotPresent
                    Return Livestock.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Livestock_Update
                    Return Livestock.Update(CType(upParam, Livestock.Parameter_Update_LiveStock), inBasicParam)
                Case RealServiceFunctions.Livestock_UpdateInsuranceDetail
                    Return Livestock.UpdateInsuranceDetail(CType(upParam, Livestock.Parameter_Update_LiveStock), inBasicParam)

                    'magazine
                Case RealServiceFunctions.Magazine_Update
                    Return Magazine.Update(CType(upParam, Magazine.Parameter_Update_Magazine), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Subs_Type
                    Return Magazine.Update_Magazine_Subs_Type(CType(upParam, Magazine.Parameter_Update_Magazine_Subs_Type), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Subs_Type_Fee
                    Return Magazine.Update_Magazine_Subs_Type_Fee(CType(upParam, Magazine.Parameter_Update_Magazine_Subs_Type_Fee), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Dispatch_Type
                    Return Magazine.Update_Magazine_Dispatch_Type(CType(upParam, Magazine.Parameter_Update_Magazine_Dispatch_Type), inBasicParam)

                Case RealServiceFunctions.Magazine_Update_Dispatch
                    Return Magazine.Update_Magazine_Dispatch(CType(upParam, Magazine.Parameter_Update_Magazine_Dispatch), inBasicParam)

                Case RealServiceFunctions.Magazine_Update_Dispatch_Type_Charges
                    Return Magazine.Update_Magazine_Dispatch_Type_Charges(CType(upParam, Magazine.Parameter_Update_Magazine_Dispatch_Type_Charges), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Membership
                    Return Magazine.Update_Magazine_Membership(CType(upParam, Magazine.Parameter_Update_Magazine_Membership_Profile), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Membership_Identity
                    Return Magazine.Update_Magazine_Membership_Identity(CType(upParam, Magazine.Parameter_Update_Magazine_Membership_Identity), inBasicParam)

                Case RealServiceFunctions.Magazine_Update_SubCity
                    Return Magazine.Update_Magazine_SubCity(CType(upParam, Magazine.Parameter_Update_Magazine_Subcity), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Membership_Request
                    Return Magazine_Request_Register.Update_Magazine_Membership_Request(CType(upParam, Magazine_Request_Register.Parameter_Update_Magazine_Membership_Request), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Request_Status
                    Return Magazine_Request_Register.Update_Magazine_Request_status(CType(upParam, Magazine_Request_Register.Parameter_Update_Magazine_Request_status), inBasicParam)

                Case RealServiceFunctions.Membership_UpdateSubscriptionList
                    Return Membership.UpdateSubscriptionList(CType(upParam, Membership.Param_Update_SubscirptionList), inBasicParam)
                Case RealServiceFunctions.Membership_UpdateSubscriptionFee
                    Return Membership.UpdateSubscriptionFee(CType(upParam, Membership.Param_Update_SubscriptionFee), inBasicParam)

                Case RealServiceFunctions.Voucher_Magazine_Register_DeleteReceipt
                    Return Voucher_Magazine_Register.DeleteReceipt(CType(upParam, Voucher_Magazine_Register.Param_DeleteReceipt_Magazine_Receipt_Register), inBasicParam)
                Case RealServiceFunctions.Magazine_Delete_SubCity
                    Return Magazine.Delete_Magazine_SubCity(CType(upParam, String), inBasicParam)

                Case RealServiceFunctions.Magazine_Delete_Dispatch
                    Return Magazine.Delete_Magazine_Dispatch(CType(upParam, String), inBasicParam)


                Case RealServiceFunctions.Magazine_Delete_Issues
                    Return Magazine.Delete_Magazine_Issues(CType(upParam, String), inBasicParam)

                Case RealServiceFunctions.Magazine_Membership_Close
                    Return Magazine.Close(CType(upParam, Membership.Parameter_Close_Membership), inBasicParam)
                Case RealServiceFunctions.Magazine_Membership_Reopen
                    Return Magazine.Reopen(CType(upParam, String), inBasicParam)

                Case RealServiceFunctions.Magazine_Consider_ForAutoRenewal
                    Return Magazine.ConsiderForAutoRenewal(CType(upParam, Magazine.Parameter_AutoRenewal_Membership), inBasicParam)

                Case RealServiceFunctions.Magazine_Subscription_Set_Default
                    Return Magazine.Set_Default_Magazine_Subscription(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_Dispatch_Set_Default
                    Return Magazine.Set_Default_Magazine_Dispatch(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_Issue_Set_Default
                    Return Magazine.Set_Default_Magazine_Issue(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_Update_Magazine_Issue
                    Return Magazine.Update_Magazine_Issue(CType(upParam, Magazine.Param_Update_Magazine_Issue), inBasicParam)

                Case RealServiceFunctions.Magazine_Subscription_Remove_Default
                    Return Magazine.Remove_Default_Magazine_Subscription(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Magazine_Dispatch_Remove_Default
                    Return Magazine.Remove_Default_Magazine_Dispatch(inBasicParam)
                Case RealServiceFunctions.Magazine_Issue_Remove_Default
                    Return Magazine.Remove_Default_Magazine_Issue(inBasicParam)
                Case RealServiceFunctions.Voucher_Magazine_Update_Magazine_Disp_CC
                    Return Voucher_Magazine_Register.Update_Magazine_Disp_CC(CType(upParam, Voucher_Magazine_Register.Param_Update_Magazine_Disp_CC), inBasicParam)

                    'Membership
                Case RealServiceFunctions.Membership_Update
                    Return Membership.Update(CType(upParam, Membership.Parameter_Update_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_Close
                    Return Membership.Close(CType(upParam, Membership.Parameter_Close_Membership), inBasicParam)
                Case RealServiceFunctions.Membership_Reopen
                    Return Membership.Reopen(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt
                    Return Membership_Receipt_Register.DeleteReceipt(CType(upParam, Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register), inBasicParam)
                Case RealServiceFunctions.MembershipReceiptRegister_DeleteReceiptRef
                    Return Membership_Receipt_Register.DeleteReceiptRef(CType(upParam, Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register), inBasicParam)

                Case RealServiceFunctions.Membership_DeleteSubscriptionList
                    Return Membership.DeleteSubscriptionList(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Membership_DeleteSubscriptionFee
                    Return Membership.DeleteSubscriptionFee(CType(upParam, Membership.Param_Delet_SubscriptionFee), inBasicParam)

                Case RealServiceFunctions.VoucherMembership_UpdateMaster
                    Return Voucher_Membership.UpdateMaster(CType(upParam, Voucher_Membership.Parameter_UpdateMaster_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_Update
                    Return Voucher_Membership.Update(CType(upParam, Voucher_Membership.Parameter_Update_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembership_UpdatePurpose
                    Return Voucher_Membership.UpdatePurpose(CType(upParam, Voucher_Membership.Parameter_UpdatePurpose_VoucherMembership), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_UpdateMaster
                    Return Voucher_Membership_Renewal.UpdateMaster(CType(upParam, Voucher_Membership_Renewal.Parameter_UpdateMaster_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_Update
                    Return Voucher_Membership_Renewal.Update(CType(upParam, Voucher_Membership_Renewal.Parameter_Update_VoucherMembershipRenewal), inBasicParam)
                Case RealServiceFunctions.VoucherMembershipRenewal_UpdatePurpose
                    Return Voucher_Membership_Renewal.UpdatePurpose(CType(upParam, Voucher_Membership_Renewal.Parameter_UpdatePurpose_VoucherMembershipRenewal), inBasicParam)
                    'Insurance Register   
                Case RealServiceFunctions.Reports_Update_Insurance_Register
                    Return LandAndBuilding.Update_Insurance_Register(CType(upParam, LandAndBuilding.Parameter_Update_Insurance_Register), inBasicParam)
                    'Notebok
                Case RealServiceFunctions.Notebook_Update
                    Return Notebook.Update(CType(upParam, Notebook.Parameter_Update_NoteBook), inBasicParam)
                    'Notes
                Case RealServiceFunctions.Notes_Complete
                    Return Notes.Complete(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Notes_Incomplete
                    Return Notes.Incomplete(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Notes_Update
                    Return Notes.Update(CType(upParam, Notes.Parameter_Update_Notes), inBasicParam)
                    'Opening Balances
                Case RealServiceFunctions.OpeningBalances_Update
                    Return OpeningBalances.Update(CType(upParam, OpeningBalances.Parameter_Update_OpeningBalances), inBasicParam)

                    'Report 
                Case RealServiceFunctions.Report_UpdateClearingDate
                    Return Reports_All.UpdateClearingDate(CType(upParam, Reports_All.Param_UpdateClearingDate), inBasicParam)
                    'Stock 
                Case RealServiceFunctions.StockProfile_UpdateStockLocation
                    Return StockProfile.UpdateStockLocation(CType(upParam, StockProfile.Param_Update_StockLocation), inBasicParam)

                Case RealServiceFunctions.StockProfile_UpdateStockProfile
                    Return StockProfile.UpdateStockProfile(CType(upParam, StockProfile.Param_Update_StockProfile), inBasicParam)


                Case RealServiceFunctions.StockDeptStores_updateStoreDept
                    Return StockDeptStores.UpdateStoreDept(CType(upParam, StockDeptStores.Param_UpdateStoreDept), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_CloseStoreDept
                    Return StockDeptStores.CloseStoreDept(CType(upParam, StockDeptStores.Param_CloseStoreDept), inBasicParam)
                Case RealServiceFunctions.StockDeptStores_ReopenStoreDept
                    Return StockDeptStores.ReopenStoreDept(CType(upParam, Int32), inBasicParam)

                Case RealServiceFunctions.Personnels_UpdatePersonnel
                    Return Personnels.UpdatePersonnel(CType(upParam, Personnels.Param_UpdatePersonnel), inBasicParam)
                Case RealServiceFunctions.Personnels_UpdatePersonnelCharges
                    Return Personnels.updatePersonnelCharges(CType(upParam, Personnels.Param_UpdatePersonnelCharges), inBasicParam)

                Case RealServiceFunctions.StockProfile_UpdateStockProject
                    Return StockProfile.UpdateStockProject(CType(upParam, StockProfile.Param_Update_StockProject), inBasicParam)
                Case RealServiceFunctions.SubItem_Reopen
                    Return SubItem.ReopenSubItem(CType(upParam, Int32), inBasicParam)
                Case RealServiceFunctions.SubItem_Close
                    Return SubItem.CloseSubItem(CType(upParam, SubItem.Param_CloseSubItem), inBasicParam)
                Case RealServiceFunctions.SubItem_UpdateSubItem_Store_Mapping
                    Return SubItem.UpdateSubItem_Store_Mapping(CType(upParam, SubItem.Param_SubItem_Update_Store_Mapping), inBasicParam)

                Case RealServiceFunctions.Suppliers_UpdateItemSupplierMapping
                    Return Suppliers.UpdateItemSupplierMapping(CType(upParam, Suppliers.Param_UpdateItemSupplierMapping), inBasicParam)

                Case RealServiceFunctions.Projects_UpdateProjectStatus
                    Return Projects.UpdateProjectStatus(CType(upParam, Projects.Param_Update_Project_Status), inBasicParam)
                Case RealServiceFunctions.UserOrder_UpdateUOStatus
                    Return StockUserOrder.UpdateUOStatus(CType(upParam, StockUserOrder.Param_Update_UO_Status), inBasicParam)
                Case RealServiceFunctions.UserOrder_Update_Scheduled_Delivery
                    Return StockUserOrder.Update_Scheduled_Delivery(CType(upParam, StockUserOrder.Param_Update_UO_Scheduled_Delivery), inBasicParam)

                Case RealServiceFunctions.Personnels_Mark_Personnel_asLeft
                    Return Personnels.Mark_Personnel_asLeft(CType(upParam, Personnels.Param_Mark_Personnel_asLeft), inBasicParam)
                Case RealServiceFunctions.Personnels_Mark_Personnel_asReopen
                    Return Personnels.Mark_Personnel_asReopen(CType(upParam, Int32), inBasicParam)

                Case RealServiceFunctions.StockMachineToolAllocation_UpdateMachineToolIssue
                    Return StockMachineToolAllocation.UpdateMachineToolIssue(CType(upParam, StockMachineToolAllocation.Param_Update_MachineTool_Issue), inBasicParam)
                Case RealServiceFunctions.StockMachineToolAllocation_UpdateMachineToolReturn
                    Return StockMachineToolAllocation.UpdateMachineToolReturn(CType(upParam, StockMachineToolAllocation.Param_Update_MachineTool_Return), inBasicParam)
                Case RealServiceFunctions.StockRequisitionRequest_Update_RR_Status
                    Return StockRequisitionRequest.UpdateRRStatus(CType(upParam, StockRequisitionRequest.Param_Update_RR_Status), inBasicParam)

                Case RealServiceFunctions.StockPurchaseOrder_UpdatePurchaseOrderStatus
                    Return StockPurchaseOrder.UpdatePurchaseOrderStatus(CType(upParam, StockPurchaseOrder.Param_UpdatePurchaseOrderStatus), inBasicParam)


                    'Service Places
                Case RealServiceFunctions.ServicePlaces_Update
                    Return ServicePlaces.Update(CType(upParam, ServicePlaces.Parameter_Update_ServicePlaces), inBasicParam)
                    'Service Report
                Case RealServiceFunctions.ServiceReport_Update
                    Return ServiceReport.Update(CType(upParam, ServiceReport.Parameter_Update_ServiceReport), inBasicParam)
                    'Payments
                Case RealServiceFunctions.Payments_UpdateMaster
                    Return Payments.UpdateMaster(CType(upParam, Payments.Parameter_UpdateMaster_VoucherPayment), inBasicParam)
                    'Property
                Case RealServiceFunctions.Property_Update
                    Return LandAndBuilding.Update(CType(upParam, LandAndBuilding.Parameter_Update_LandAndBuilding), inBasicParam)
                    'Receipts
                Case RealServiceFunctions.Receipts_UpdateMaster
                    Return Receipts.UpdateMaster(CType(upParam, Receipts.Parameter_UpdateMaster_VoucherReceipt), inBasicParam)
                    'Request
                Case RealServiceFunctions.Request_MarkAsRead
                    Return Request.MarkAsRead(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Request_MarkAsUnread
                    Return Request.MarkAsUnread(CType(upParam, String), inBasicParam)
                Case RealServiceFunctions.Request_Update
                    Return Request.Update(CType(upParam, Request.Parameter_Update_Request), inBasicParam)

                'Service Module
                Case RealServiceFunctions.ServiceModule_Update
                    Return ServiceModule.Update(CType(upParam, ServiceModule.Parameter_Update_ServiceModule), inBasicParam)

                    'Sale Of Assets
                Case RealServiceFunctions.SaleOfAssets_UpdateMaster
                    Return Voucher_SaleOfAsset.UpdateMaster(CType(upParam, Voucher_SaleOfAsset.Parameter_UpdateMasterInfo_VoucherSaleOfAsset), inBasicParam)

                    'Asset Transfer
                Case RealServiceFunctions.AssetTransfer_UpdateMaster
                    Return Voucher_AssetTransfer.UpdateMaster(CType(upParam, Voucher_AssetTransfer.Parameter_UpdateMasterInfo_VoucherAssetTransfer), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_Update_CrossReference
                    Return Voucher_AssetTransfer.Update_CrossReference(CType(upParam, Voucher_AssetTransfer.Param_VoucherAssetTransfer_Update_CrossReference), inBasicParam)
                Case RealServiceFunctions.AssetTransfer_Delete_CrossReference
                    Return Voucher_AssetTransfer.Delete_CrossReference(CType(upParam, Voucher_AssetTransfer.Param_VoucherAssetTransfer_Update_CrossReference), inBasicParam)

                    'Telephone
                Case RealServiceFunctions.Telephone_Update
                    Return Telephones.Update(CType(upParam, Telephones.Parameter_Update_Telephones), inBasicParam)
                Case RealServiceFunctions.Telephone_Close
                    Return Telephones.Close(CType(upParam, Telephones.Parameter_Close_Telephones), inBasicParam)
                    'Case RealServiceFunctions.Telephone_UpdateAssetLocationIfNotPresent
                    '    Return Telephones.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                    'Vehicles
                Case RealServiceFunctions.Vehicles_Update
                    Return Vehicles.Update(CType(upParam, Vehicles.Parameter_Update_Vehicles), inBasicParam)
                Case RealServiceFunctions.Vehicles_UpdateInsuranceDetail
                    Return Vehicles.UpdateInsuranceDetail(CType(upParam, Vehicles.Parameter_Update_Vehicles), inBasicParam)
                Case RealServiceFunctions.Vehicles_UpdateAssetLocationIfNotPresent
                    Return Vehicles.UpdateAssetLocationIfNotPresent(CType(upParam, String), inBasicParam)
                    'WIP
                Case RealServiceFunctions.WIP_Profile_Update
                    Return WIP_Profile.Update(CType(upParam, WIP_Profile.Parameter_Update_WIP_Profile), inBasicParam)
            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Deleting Server Only[SO] Table Records Function without parameters")
        > <XmlInclude(GetType(DBNull))>
        Public Function Wrap_Delete_SO_Table_Record(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param) As Boolean
            Select Case FunctionCalled

            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for Deleting Server Only[SO] Table Records Function with parameters")
        > <XmlInclude(GetType(DBNull))>
        Public Function Wrap_Delete_SO_Table_Record(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal parameter As Object) As Boolean
            Select Case FunctionCalled

            End Select
            Return Nothing
        End Function

        <WebMethod(MessageName:="Wrapper for execute Function"),
       XmlInclude(GetType(DonationRegister.Parameter_Request_Receipt)),
       XmlInclude(GetType(LandAndBuilding.Param_Txn_InsertProperty_LandAndBuilding)),
       XmlInclude(GetType(LandAndBuilding.Param_Txn_UpdateProperty_LandAndBuilding)),
       XmlInclude(GetType(LandAndBuilding.Param_Txn_DeleteProperty_LandAndBuilding)),
       XmlInclude(GetType(Membership.Param_Txn_Insert_Membership)),
       XmlInclude(GetType(Membership.Param_Txn_Update_Membership)),
       XmlInclude(GetType(Membership.Param_Txn_Delete_Membership)),
       XmlInclude(GetType(Addresses.Param_Txn_Insert_Addresses)),
       XmlInclude(GetType(Addresses.Param_Txn_Update_Addresses)),
       XmlInclude(GetType(Addresses.Param_Txn_Delete_Addresses)),
       XmlInclude(GetType(Addresses.Param_Txn_Delete_AddressSet)),
       XmlInclude(GetType(ServiceReport.Param_Txn_Insert_ServiceReport)),
       XmlInclude(GetType(ServiceReport.Param_Txn_Update_ServiceReport)),
       XmlInclude(GetType(ServiceReport.Param_Txn_Delete_ServiceReport)),
       XmlInclude(GetType(ServiceMaterial.Param_Txn_Update_ServiceMaterial)),
       XmlInclude(GetType(ServiceMaterial.Param_Txn_Delete_ServiceMaterial)),
       XmlInclude(GetType(Action_Items.Param_Txn_Insert_ActionItems)),
       XmlInclude(GetType(Action_Items.Param_Txn_Update_ActionItems)),
       XmlInclude(GetType(Voucher_Donation.Param_Txn_Insert_VoucherDonation)),
       XmlInclude(GetType(Voucher_Donation.Param_Txn_Update_VoucherDonation)),
       XmlInclude(GetType(Voucher_Donation.Param_Txn_Delete_VoucherDonation)),
       XmlInclude(GetType(Voucher_SaleOfAsset.Param_Txn_Insert_VoucherSaleOfAsset)),
       XmlInclude(GetType(Voucher_SaleOfAsset.Param_Txn_Update_VoucherSaleOfAsset)),
       XmlInclude(GetType(Voucher_SaleOfAsset.Param_Txn_Delete_VoucherSaleOfAsset)),
       XmlInclude(GetType(Voucher_Internal_Transfer.Param_Txn_Insert_InternalTransfer)),
       XmlInclude(GetType(Voucher_Internal_Transfer.Param_Txn_Update_InternalTransfer)),
       XmlInclude(GetType(Voucher_Internal_Transfer.Param_Txn_Delete_InternalTransfer)),
       XmlInclude(GetType(Voucher_AssetTransfer.Param_Txn_Insert_VoucherAssetTransfer)),
       XmlInclude(GetType(Voucher_AssetTransfer.Param_Txn_Update_VoucherAssetTransfer)),
       XmlInclude(GetType(Voucher_AssetTransfer.Param_Txn_Delete_VoucherAssetTransfer)),
       XmlInclude(GetType(Voucher_Gift.Param_Txn_Insert_VoucherGift)),
       XmlInclude(GetType(Voucher_Gift.Param_Txn_Update_VoucherGift)),
       XmlInclude(GetType(Voucher_Gift.Param_Txn_Delete_VoucherGift)),
       XmlInclude(GetType(Voucher_Journal.Param_Txn_Insert_VoucherJournal)),
       XmlInclude(GetType(Voucher_Journal.Param_Txn_Update_VoucherJournal)),
       XmlInclude(GetType(Voucher_Journal.Param_Txn_Delete_VoucherJournal)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_NewFD_InsertVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_NewFD_UpdateVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_NewFD_DeleteVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_RenewFD_InsertVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_RenewFD_UpdateVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_RenewFD_DeleteVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_CloseFD_InsertVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_CloseFD_UpdateVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_CloseFD_DeleteVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_IncomeExpenses_InsertVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_IncomeExpenses_UpdateVoucherFD)),
       XmlInclude(GetType(Voucher_FD.Param_Txn_IncomeExpenses_DeleteVoucherFD)),
       XmlInclude(GetType(Voucher_Membership.Param_Txn_Insert_VoucherMembership)),
       XmlInclude(GetType(Voucher_Membership.Param_Txn_Update_VoucherMembership)),
       XmlInclude(GetType(Voucher_Membership.Param_Txn_Delete_VoucherMembership)),
       XmlInclude(GetType(Voucher_Membership_Renewal.Param_Txn_Insert_VoucherMembershipRenewal)),
       XmlInclude(GetType(Voucher_Membership_Renewal.Param_Txn_Update_VoucherMembershipRenewal)),
       XmlInclude(GetType(Voucher_Membership_Renewal.Param_Txn_Delete_VoucherMembershipRenewal)),
       XmlInclude(GetType(Voucher_Membership_Conversion.Param_Txn_Insert_VoucherMembershipConversion)),
       XmlInclude(GetType(Voucher_Membership_Conversion.Param_Txn_Update_VoucherMembershipConversion)),
       XmlInclude(GetType(Voucher_Membership_Conversion.Param_Txn_Delete_VoucherMembershipConversion)),
       XmlInclude(GetType(Receipts.Param_Txn_Insert_VoucherReceipt)),
       XmlInclude(GetType(Receipts.Param_Txn_Update_VoucherReceipt)),
       XmlInclude(GetType(Receipts.Param_Txn_Delete_VoucherReceipt)),
       XmlInclude(GetType(Payments.Param_Txn_Insert_VoucherPayment)),
       XmlInclude(GetType(Payments.Param_Txn_Update_VoucherPayment)),
       XmlInclude(GetType(Payments.Param_Txn_Delete_VoucherPayment)),
        XmlInclude(GetType(Complexes.Param_Txn_Insert_Complexes)),
       XmlInclude(GetType(Complexes.Param_Update_ManageBuildings_Txn)),
       XmlInclude(GetType(Complexes.Param_Txn_Delete_Complexes)),
       XmlInclude(GetType(Voucher_WIP_Finalization.Param_Txn_Delete_Voucher_WIPFinalization)),
       XmlInclude(GetType(Voucher_WIP_Finalization.Param_Txn_Update_Voucher_WIPFinalization)),
       XmlInclude(GetType(Voucher_WIP_Finalization.Param_Txn_Insert_Voucher_WIPFinalization)),
        XmlInclude(GetType(Voucher_Magazine_Register.Param_Txn_Insert_VoucherMagazineMembership)),
       XmlInclude(GetType(ServicePlaces.Param_Txn_InsertServicePlaces)),
       XmlInclude(GetType(ServicePlaces.Param_Txn_UpdateServicePlaces)),
       XmlInclude(GetType(Vouchers.Param_RejectDraftEntry)),
        XmlInclude(GetType(Attachments.Parameter_Attachment_Unlink)),
        XmlInclude(GetType(SubItem.Param_SubItem_Insert_Txn)),
        XmlInclude(GetType(SubItem.Param_SubItem_Update_Txn)),
        XmlInclude(GetType(Projects.Param_Insert_Project_Txn)),
        XmlInclude(GetType(Projects.Param_Update_Project_Txn)),
        XmlInclude(GetType(Jobs.Param_Insert_Job_Txn)),
        XmlInclude(GetType(Jobs.Param_Insert_Job_ManpowerUsage)),
        XmlInclude(GetType(Jobs.Param_Insert_Job_ExpensesIncurred)),
        XmlInclude(GetType(Jobs.Param_Insert_Job_MachineUsage)),
        XmlInclude(GetType(Jobs.Param_Update_Job_Txn)),
        XmlInclude(GetType(StockUserOrder.Param_Insert_UO_Txn)),
        XmlInclude(GetType(StockUserOrder.Param_InsertUORemarks)),
        XmlInclude(GetType(StockUserOrder.Param_Update_UO_Txn)),
        XmlInclude(GetType(StockUserOrder.Param_UO_RR_Unmapping)),
        XmlInclude(GetType(StockProduction.Param_Insert_Production_Txn)),
        XmlInclude(GetType(StockProduction.Param_Update_Production_Txn)),
        XmlInclude(GetType(Suppliers.Param_InsertSupplier_Txn)),
        XmlInclude(GetType(Suppliers.Param_UpdateSupplier_Txn)),
        XmlInclude(GetType(StockRequisitionRequest.Param_Insert_RequisitionRequest_Txn)),
        XmlInclude(GetType(StockRequisitionRequest.Param_Update_RequisitionRequest_Txn)),
        XmlInclude(GetType(ClientUserInfo.Param_SaveClientUserGroupMapping)),
        XmlInclude(GetType(Jobs.Param_InsertJobRemarks)),
        XmlInclude(GetType(StockPurchaseOrder.Param_Update_PurchaseOrder_Txn)),
        XmlInclude(GetType(Vouchers.Param_AddVouchingAudit)),
        XmlInclude(GetType(Projects.Param_InsertProjectRemarks)),
        XmlInclude(GetType(Vouchers.Param_UpdateCommonDetails_Txn)),
        XmlInclude(GetType(ServicePlaces.Param_Txn_DeleteServicePlaces)),
        XmlInclude(GetType(LandAndBuilding.Parameter_Update_Property_RentDetails))> <XmlInclude(GetType(DBNull))>
        Public Function Wrap_ExecuteFunction(ByVal FunctionCalled As RealServiceFunctions, inBasicParam As Basic_Param, ByVal inParam As Object) As Object
            Try
                'Begin Transaction
                '_Wrap_ExecuteFunctionDB.ConnectionString = Common.ConnectionString
                '_Wrap_ExecuteFunctionDB.OpenConnection()
                Select Case FunctionCalled
                    Case RealServiceFunctions.Attachments_Delete
                        Return Attachments.Delete_Attachment(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Attachments_Unlink
                        Return Attachments.Unlink_Attachment(CType(inParam, Attachments.Parameter_Attachment_Unlink), inBasicParam)
                    Case RealServiceFunctions.Audit_DeleteAllVouchingAgainstEntry
                        Return Audit.DeleteAllVouchingAgainstEntry(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Audit_DeleteAllVouchingAgainstReference
                        Return Audit.DeleteAllVouchingAgainstReference(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.CentreRelated_ClientUserInfo_SaveClientUserGroupMapping
                        Return ClientUserInfo.SaveClientUserGroupMapping(CType(inParam, ClientUserInfo.Param_SaveClientUserGroupMapping), inBasicParam)

                    Case RealServiceFunctions.Attachments_Delete_Attachment_ByDescription
                        Return Attachments.Delete_Attachment_ByDescription(CType(inParam, String), inBasicParam)

                    Case RealServiceFunctions.Donation_RequestReceipt
                        Return DonationRegister.RequestReceipt(CType(inParam, DonationRegister.Parameter_Request_Receipt), inBasicParam)
                        'Case RealServiceFunctions.Donation_GenerateReceipt
                        '   Return DonationRegister.GenerateReceipt(CType(inParam, DonationRegister.Param_Generate_Receipt), inBasicParam)
                        ' Case RealServiceFunctions.Donation_PrintReceipt
                        '     Return DonationRegister.PrintReceipt(CType(inParam, DonationRegister.Param_Print_Receipt), inBasicParam)
                    Case RealServiceFunctions.Property_InsertProperty_Txn
                        Return LandAndBuilding.InsertProperty_Txn(CType(inParam, LandAndBuilding.Param_Txn_InsertProperty_LandAndBuilding), inBasicParam)
                    Case RealServiceFunctions.Property_UpdateProperty_Txn
                        Return LandAndBuilding.UpdateProperty_Txn(CType(inParam, LandAndBuilding.Param_Txn_UpdateProperty_LandAndBuilding), inBasicParam)
                    Case RealServiceFunctions.Property_UpdateRentDetails_Txn
                        Return LandAndBuilding.UpdateProperty_RentDetails(CType(inParam, LandAndBuilding.Parameter_Update_Property_RentDetails), inBasicParam)
                    Case RealServiceFunctions.Property_DeleteProperty_Txn
                        Return LandAndBuilding.DeleteProperty_Txn(CType(inParam, LandAndBuilding.Param_Txn_DeleteProperty_LandAndBuilding), inBasicParam)
                    Case RealServiceFunctions.Membership_InsertMembership_Txn
                        Return Membership.InsertMembership_Txn(CType(inParam, Membership.Param_Txn_Insert_Membership), inBasicParam)
                    Case RealServiceFunctions.Membership_UpdateMembership_Txn
                        Return Membership.UpdateMembership_Txn(CType(inParam, Membership.Param_Txn_Update_Membership), inBasicParam)
                    Case RealServiceFunctions.Membership_DeleteMembership_Txn
                        Return Membership.DeleteMembership_Txn(CType(inParam, Membership.Param_Txn_Delete_Membership), inBasicParam)
                    Case RealServiceFunctions.Addresses_InsertAddresses_Txn
                        Return Addresses.InsertAddresses_Txn(CType(inParam, Addresses.Param_Txn_Insert_Addresses), inBasicParam)
                    Case RealServiceFunctions.Addresses_UpdateAddresses_Txn
                        Return Addresses.UpdateAddresses_Txn(CType(inParam, Addresses.Param_Txn_Update_Addresses), inBasicParam)
                    Case RealServiceFunctions.Addresses_DeleteAddresses_Txn
                        Return Addresses.DeleteAddresses_Txn(CType(inParam, Addresses.Param_Txn_Delete_Addresses), inBasicParam)
                    Case RealServiceFunctions.ServiceReport_InsertServiceReport_Txn
                        Return ServiceReport.InsertServiceReport_Txn(CType(inParam, ServiceReport.Param_Txn_Insert_ServiceReport), inBasicParam)
                    Case RealServiceFunctions.ServiceReport_UpdateServiceReport_Txn
                        Return ServiceReport.UpdateServiceReport_Txn(CType(inParam, ServiceReport.Param_Txn_Update_ServiceReport), inBasicParam)
                    Case RealServiceFunctions.ServiceReport_DeleteServiceReport_Txn
                        Return ServiceReport.DeleteServiceReport_Txn(CType(inParam, ServiceReport.Param_Txn_Delete_ServiceReport), inBasicParam)
                    Case RealServiceFunctions.ServiceMaterial_UpdateServiceMaterial_Txn
                        Return ServiceMaterial.UpdateServiceMaterial_Txn(CType(inParam, ServiceMaterial.Param_Txn_Update_ServiceMaterial), inBasicParam)
                    Case RealServiceFunctions.ServiceMaterial_DeleteServiceMaterial_Txn
                        Return ServiceMaterial.DeleteServiceMaterial_Txn(CType(inParam, ServiceMaterial.Param_Txn_Delete_ServiceMaterial), inBasicParam)
                    Case RealServiceFunctions.ActionItems_InsertActionItems_Txn
                        Return Action_Items.InsertActionItems_Txn(CType(inParam, Action_Items.Param_Txn_Insert_ActionItems), inBasicParam)
                    Case RealServiceFunctions.ActionItems_UpdateActionItems_Txn
                        Return Action_Items.UpdateActionItems_Txn(CType(inParam, Action_Items.Param_Txn_Update_ActionItems), inBasicParam)
                    Case RealServiceFunctions.SaleOfAssets_InsertSaleOfAsset_Txn
                        Return Voucher_SaleOfAsset.InsertSaleOfAsset_Txn(CType(inParam, Voucher_SaleOfAsset.Param_Txn_Insert_VoucherSaleOfAsset), inBasicParam)
                    Case RealServiceFunctions.SaleOfAssets_UpdateSaleOfAsset_Txn
                        Return Voucher_SaleOfAsset.UpdateSaleOfAsset_Txn(CType(inParam, Voucher_SaleOfAsset.Param_Txn_Update_VoucherSaleOfAsset), inBasicParam)
                    Case RealServiceFunctions.SaleOfAssets_DeleteSaleOfAssets_Txn
                        Return Voucher_SaleOfAsset.DeleteSaleOfAssets_Txn(CType(inParam, Voucher_SaleOfAsset.Param_Txn_Delete_VoucherSaleOfAsset), inBasicParam)
                    Case RealServiceFunctions.VoucherDonation_InsertDonation_Txn
                        Return Voucher_Donation.InsertDonation_Txn(CType(inParam, Voucher_Donation.Param_Txn_Insert_VoucherDonation), inBasicParam)
                    Case RealServiceFunctions.VoucherDonation_UpdateDonation_Txn
                        Return Voucher_Donation.UpdateDonation_Txn(CType(inParam, Voucher_Donation.Param_Txn_Update_VoucherDonation), inBasicParam)
                    Case RealServiceFunctions.VoucherDonation_DeleteDonation_Txn
                        Return Voucher_Donation.DeleteDonation_Txn(CType(inParam, Voucher_Donation.Param_Txn_Delete_VoucherDonation), inBasicParam)
                    Case RealServiceFunctions.InternalTransfer_Insert_InternalTransfer_Txn
                        Return Voucher_Internal_Transfer.Insert_InternalTransfer_Txn(CType(inParam, Voucher_Internal_Transfer.Param_Txn_Insert_InternalTransfer), inBasicParam)
                    Case RealServiceFunctions.InternalTransfer_Update_InternalTransfer_Txn
                        Return Voucher_Internal_Transfer.Update_InternalTransfer_Txn(CType(inParam, Voucher_Internal_Transfer.Param_Txn_Update_InternalTransfer), inBasicParam)
                    Case RealServiceFunctions.InternalTransfer_Delete_InternalTransfer_Txn
                        Return Voucher_Internal_Transfer.Delete_InternalTransfer_Txn(CType(inParam, Voucher_Internal_Transfer.Param_Txn_Delete_InternalTransfer), inBasicParam)
                    Case RealServiceFunctions.AssetTransfer_Insert_AssetTransfer_Txn
                        Return Voucher_AssetTransfer.Insert_AssetTransfer_Txn(CType(inParam, Voucher_AssetTransfer.Param_Txn_Insert_VoucherAssetTransfer), inBasicParam)
                    Case RealServiceFunctions.AssetTransfer_Update_AssetTransfer_Txn
                        Return Voucher_AssetTransfer.Update_AssetTransfer_Txn(CType(inParam, Voucher_AssetTransfer.Param_Txn_Update_VoucherAssetTransfer), inBasicParam)
                    Case RealServiceFunctions.AssetTransfer_Delete_AssetTransfer_Txn
                        Return Voucher_AssetTransfer.Delete_AssetTransfer_Txn(CType(inParam, Voucher_AssetTransfer.Param_Txn_Delete_VoucherAssetTransfer), inBasicParam)
                    Case RealServiceFunctions.Gift_InsertGift_Txn
                        Return Voucher_Gift.InsertGift_Txn(CType(inParam, Voucher_Gift.Param_Txn_Insert_VoucherGift), inBasicParam)
                    Case RealServiceFunctions.Gift_UpdateGift_Txn
                        Return Voucher_Gift.UpdateGift_Txn(CType(inParam, Voucher_Gift.Param_Txn_Update_VoucherGift), inBasicParam)
                    Case RealServiceFunctions.Gift_DeleteGift_Txn
                        Return Voucher_Gift.DeleteGift_Txn(CType(inParam, Voucher_Gift.Param_Txn_Delete_VoucherGift), inBasicParam)
                    Case RealServiceFunctions.Receipts_InsertReceipt_Txn
                        Return Receipts.InsertReceipt_Txn(CType(inParam, Receipts.Param_Txn_Insert_VoucherReceipt), inBasicParam)
                    Case RealServiceFunctions.Receipts_UpdateReceipt_Txn
                        Return Receipts.UpdateReceipt_Txn(CType(inParam, Receipts.Param_Txn_Update_VoucherReceipt), inBasicParam)
                    Case RealServiceFunctions.Receipts_DeleteReceipt_Txn
                        Return Receipts.DeleteReceipt_Txn(CType(inParam, Receipts.Param_Txn_Delete_VoucherReceipt), inBasicParam)
                    Case RealServiceFunctions.Journal_InsertJournal_Txn
                        Return Voucher_Journal.InsertJournal_Txn(CType(inParam, Voucher_Journal.Param_Txn_Insert_VoucherJournal), inBasicParam)
                    Case RealServiceFunctions.Journal_UpdateJournal_Txn
                        Return Voucher_Journal.UpdateJournal_Txn(CType(inParam, Voucher_Journal.Param_Txn_Update_VoucherJournal), inBasicParam)
                    Case RealServiceFunctions.Journal_DeleteJournal_Txn
                        Return Voucher_Journal.DeleteJournal_Txn(CType(inParam, Voucher_Journal.Param_Txn_Delete_VoucherJournal), inBasicParam)
                    Case RealServiceFunctions.Payments_InsertPayment_Txn
                        Return Payments.InsertPayment_Txn(CType(inParam, Payments.Param_Txn_Insert_VoucherPayment), inBasicParam)
                    Case RealServiceFunctions.Payments_UpdatePayment_Txn
                        Return Payments.UpdatePayment_Txn(CType(inParam, Payments.Param_Txn_Update_VoucherPayment), inBasicParam)
                    Case RealServiceFunctions.Payments_DeletePayment_Txn
                        Return Payments.DeletePayment_Txn(CType(inParam, Payments.Param_Txn_Delete_VoucherPayment), inBasicParam)
                        'Jobs 
                    Case RealServiceFunctions.Jobs_Insert_Txn
                        Return Jobs.InsertJobTxn(CType(inParam, Jobs.Param_Insert_Job_Txn), inBasicParam)
                    Case RealServiceFunctions.Jobs_InsertJobManpowerUsage
                        Return Jobs.InsertJobManpowerUsage(CType(inParam, Jobs.Param_Insert_Job_ManpowerUsage), inBasicParam)
                    Case RealServiceFunctions.Jobs_InsertJobExpensesIncurred
                        Return Jobs.InsertJobExpensesIncurred(CType(inParam, Jobs.Param_Insert_Job_ExpensesIncurred), inBasicParam)
                    Case RealServiceFunctions.Jobs_InsertJobMachineUsage
                        Return Jobs.InsertJobMachineUsage(CType(inParam, Jobs.Param_Insert_Job_MachineUsage), inBasicParam)
                    Case RealServiceFunctions.Jobs_InsertJobRemarks
                        Return Jobs.InsertJobRemarks(CType(inParam, Jobs.Param_InsertJobRemarks), inBasicParam)

                    Case RealServiceFunctions.Jobs_Delete_Txn
                        Return Jobs.DeleteJob(CType(inParam, Int32), inBasicParam)
                    Case RealServiceFunctions.Jobs_Update
                        Return Jobs.UpdateJobTxn(CType(inParam, Jobs.Param_Update_Job_Txn), inBasicParam)

                        'Projects 
                    Case RealServiceFunctions.Projects_Insert
                        Return Projects.InsertProjectTxn(CType(inParam, Projects.Param_Insert_Project_Txn), inBasicParam)
                    Case RealServiceFunctions.Projects_InsertProjectRemarks
                        Return Projects.InsertProjectRemarks(CType(inParam, Projects.Param_InsertProjectRemarks), inBasicParam)
                    Case RealServiceFunctions.Projects_Delete
                        Return Projects.DeleteProject(CType(inParam, Int32), inBasicParam)
                    Case RealServiceFunctions.Projects_Update
                        Return Projects.UpdateProjectTxn(CType(inParam, Projects.Param_Update_Project_Txn), inBasicParam)

                        'Payment
                    'Case RealServiceFunctions.Payments_SavePaymentDetails
                    '    Return Payments.SavePaymentDetails(CType(inParam, Payments.Param_paymentVoucherDetails), inBasicParam)

                          'UO 
                    Case RealServiceFunctions.UserOrder_Insert_UO
                        Return StockUserOrder.InsertUOTxn(CType(inParam, StockUserOrder.Param_Insert_UO_Txn), inBasicParam)
                    Case RealServiceFunctions.UserOrder_Delete_UO
                        Return StockUserOrder.DeleteUO(CType(inParam, Int32), inBasicParam)
                    Case RealServiceFunctions.UserOrder_Update_UO
                        Return StockUserOrder.UpdateUOTxn(CType(inParam, StockUserOrder.Param_Update_UO_Txn), inBasicParam)
                    Case RealServiceFunctions.UserOrder_InsertUORemarks
                        Return StockUserOrder.InsertUORemarks(CType(inParam, StockUserOrder.Param_InsertUORemarks), inBasicParam)
                    Case RealServiceFunctions.UserOrder_UORRUnmapping
                        Return StockUserOrder.UORRUnmapping(CType(inParam, StockUserOrder.Param_UO_RR_Unmapping), inBasicParam)
                        'RR
                    Case RealServiceFunctions.StockRequisitionRequest_Insert_RR
                        Return StockRequisitionRequest.InsertRequisitionRequest_Txn(CType(inParam, StockRequisitionRequest.Param_Insert_RequisitionRequest_Txn), inBasicParam)
                    Case RealServiceFunctions.StockRequisitionRequest_Delete_RR
                        Return StockRequisitionRequest.DeleteRequisitionRequest_Txn(CType(inParam, Int32), inBasicParam)
                    Case RealServiceFunctions.StockRequisitionRequest_Update_RR
                        Return StockRequisitionRequest.UpdateRequisitionRequest_Txn(CType(inParam, StockRequisitionRequest.Param_Update_RequisitionRequest_Txn), inBasicParam)

                    Case RealServiceFunctions.StockMachineToolAllocation_DeleteMachineToolIssue
                        Return StockMachineToolAllocation.DeleteMachineToolIssue(CType(inParam, Int32), inBasicParam)
                    Case RealServiceFunctions.StockMachineToolAllocation_DeleteMachineToolReturn
                        Return StockMachineToolAllocation.DeleteMachineToolReturn(CType(inParam, Int32), inBasicParam)

                    Case RealServiceFunctions.StockProduction_InsertProduction_Txn
                        Return StockProduction.InsertProduction_Txn(CType(inParam, StockProduction.Param_Insert_Production_Txn), inBasicParam)
                    Case RealServiceFunctions.StockProduction_UpdateProduction_Txn
                        Return StockProduction.UpdateProduction_Txn(CType(inParam, StockProduction.Param_Update_Production_Txn), inBasicParam)
                    Case RealServiceFunctions.StockProduction_DeleteProduction_Txn
                        Return StockProduction.DeleteProduction_Txn(CType(inParam, Int32), inBasicParam)

                    Case RealServiceFunctions.StockPurchaseOrder_UpdatePurchaseOrder_Txn
                        Return StockPurchaseOrder.UpdatePurchaseOrder_Txn(CType(inParam, StockPurchaseOrder.Param_Update_PurchaseOrder_Txn), inBasicParam)
                    Case RealServiceFunctions.StockPurchaseOrder_DeletePurchaseOrder_Txn
                        Return StockPurchaseOrder.DeletePurchaseOrder_Txn(CType(inParam, Int32), inBasicParam)


                    Case RealServiceFunctions.Suppliers_InsertSupplier_Txn
                        Return Suppliers.InsertSupplier_Txn(CType(inParam, Suppliers.Param_InsertSupplier_Txn), inBasicParam)
                    Case RealServiceFunctions.Suppliers_UpdateSupplier_Txn
                        Return Suppliers.UpdateSupplier_Txn(CType(inParam, Suppliers.Param_UpdateSupplier_Txn), inBasicParam)
                    Case RealServiceFunctions.Suppliers_DeleteSupplier_Txn
                        Return Suppliers.DeleteSupplier_Txn(CType(inParam, Int32), inBasicParam)

                    Case RealServiceFunctions.FDs_VoucherFD_InsertNewFD_Txn
                        Return Voucher_FD.InsertNewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_NewFD_InsertVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_UpdateNewFD_Txn
                        Return Voucher_FD.UpdateNewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_NewFD_UpdateVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_DeleteNewFD_Txn
                        Return Voucher_FD.DeleteNewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_NewFD_DeleteVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_InsertRenewFD_Txn
                        Return Voucher_FD.InsertRenewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_RenewFD_InsertVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_UpdateRenewFD_Txn
                        Return Voucher_FD.UpdateRenewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_RenewFD_UpdateVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_DeleteRenewFD_Txn
                        Return Voucher_FD.DeleteRenewFD_Txn(CType(inParam, Voucher_FD.Param_Txn_RenewFD_DeleteVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_InsertCloseFD_Txn
                        Return Voucher_FD.InsertCloseFD_Txn(CType(inParam, Voucher_FD.Param_Txn_CloseFD_InsertVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_UpdateCloseFD_Txn
                        Return Voucher_FD.UpdateCloseFD_Txn(CType(inParam, Voucher_FD.Param_Txn_CloseFD_UpdateVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_DeleteCloseFD_Txn
                        Return Voucher_FD.DeleteCloseFD_Txn(CType(inParam, Voucher_FD.Param_Txn_CloseFD_DeleteVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_InsertIncomeAndExpenses_Txn
                        Return Voucher_FD.InsertIncomeAndExpenses_Txn(CType(inParam, Voucher_FD.Param_Txn_IncomeExpenses_InsertVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_UpdateIncomeAndExpenses_Txn
                        Return Voucher_FD.UpdateIncomeAndExpenses_Txn(CType(inParam, Voucher_FD.Param_Txn_IncomeExpenses_UpdateVoucherFD), inBasicParam)
                    Case RealServiceFunctions.FDs_VoucherFD_DeleteIncomeAndExpenses_Txn
                        Return Voucher_FD.DeleteIncomeAndExpenses_Txn(CType(inParam, Voucher_FD.Param_Txn_IncomeExpenses_DeleteVoucherFD), inBasicParam)
                    Case RealServiceFunctions.VoucherMembership_InsertMembershipVoucher_Txn
                        Return Voucher_Membership.InsertMembershipVoucher_Txn(CType(inParam, Voucher_Membership.Param_Txn_Insert_VoucherMembership), inBasicParam)
                    Case RealServiceFunctions.VoucherMembership_UpdateMembershipVoucher_Txn
                        Return Voucher_Membership.UpdateMembershipVoucher_Txn(CType(inParam, Voucher_Membership.Param_Txn_Update_VoucherMembership), inBasicParam)
                    Case RealServiceFunctions.VoucherMembership_DeleteMembershipVoucher_Txn
                        Return Voucher_Membership.DeleteMembershipVoucher_Txn(CType(inParam, Voucher_Membership.Param_Txn_Delete_VoucherMembership), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipRenewal_InsertMemRenewalVoucher_Txn
                        Return Voucher_Membership_Renewal.InsertMemRenewalVoucher_Txn(CType(inParam, Voucher_Membership_Renewal.Param_Txn_Insert_VoucherMembershipRenewal), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipRenewal_UpdateMemRenewalVoucher_Txn
                        Return Voucher_Membership_Renewal.UpdateMemRenewalVoucher_Txn(CType(inParam, Voucher_Membership_Renewal.Param_Txn_Update_VoucherMembershipRenewal), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipRenewal_DeleteMemRenewalVoucher_Txn
                        Return Voucher_Membership_Renewal.DeleteMemRenewalVoucher_Txn(CType(inParam, Voucher_Membership_Renewal.Param_Txn_Delete_VoucherMembershipRenewal), inBasicParam)
                    Case RealServiceFunctions.Vouchers_UpdateCommonDetails
                        Return Vouchers.UpdateCommonDetails_Txn(CType(inParam, Vouchers.Param_UpdateCommonDetails_Txn), inBasicParam)
                    Case RealServiceFunctions.ServicePlaces_InsertServicePlaces_Txn
                        Return ServicePlaces.InsertServicePlaces_Txn(CType(inParam, ServicePlaces.Param_Txn_InsertServicePlaces), inBasicParam)
                    Case RealServiceFunctions.ServicePlaces_UpdateServicePlaces_Txn
                        Return ServicePlaces.UpdateServicePlaces_Txn(CType(inParam, ServicePlaces.Param_Txn_UpdateServicePlaces), inBasicParam)
                    Case RealServiceFunctions.ServicePlaces_DeleteServicePlaces_Txn
                        Return ServicePlaces.DeleteServicePlaces_Txn(CType(inParam, ServicePlaces.Param_Txn_DeleteServicePlaces), inBasicParam)
                    Case RealServiceFunctions.Complexes_Insert_Complexes_Txn
                        Return Complexes.Insert_Complexes_Txn(CType(inParam, Complexes.Param_Txn_Insert_Complexes), inBasicParam)
                    Case RealServiceFunctions.Complexes_DeleteComplexBuilding_Txn
                        Return Complexes.DeleteComplexBuilding_Txn(CType(inParam, Complexes.Param_Txn_Delete_Complexes), inBasicParam)
                    Case RealServiceFunctions.Complexes_Update_ManageBuildings_Txn
                        Return Complexes.Update_ManageBuildings_Txn(CType(inParam, Complexes.Param_Update_ManageBuildings_Txn), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipConversion_InsertMembershipVoucherConversion_Txn
                        Return Voucher_Membership_Conversion.InsertMembershipVoucherConversion_Txn(CType(inParam, Voucher_Membership_Conversion.Param_Txn_Insert_VoucherMembershipConversion), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipConversion_UpdateMembershipVoucherConversion_Txn
                        Return Voucher_Membership_Conversion.UpdateMembershipVoucherConversion_Txn(CType(inParam, Voucher_Membership_Conversion.Param_Txn_Update_VoucherMembershipConversion), inBasicParam)
                    Case RealServiceFunctions.VoucherMembershipConversion_DeleteMembershipVoucherConversion_Txn
                        Return Voucher_Membership_Conversion.DeleteMembershipVoucherConversion_Txn(CType(inParam, Voucher_Membership_Conversion.Param_Txn_Delete_VoucherMembershipConversion), inBasicParam)
                    Case RealServiceFunctions.Voucher_WIP_Finalization_Insert_WIP_Finalization_Txn
                        Return Voucher_WIP_Finalization.Insert_WIP_Finalization_Txn(CType(inParam, Voucher_WIP_Finalization.Param_Txn_Insert_Voucher_WIPFinalization), inBasicParam)
                    Case RealServiceFunctions.Voucher_WIP_Finalization_Update_WIP_Finalization_Txn
                        Return Voucher_WIP_Finalization.Update_WIP_Finalization_Txn(CType(inParam, Voucher_WIP_Finalization.Param_Txn_Update_Voucher_WIPFinalization), inBasicParam)
                    Case RealServiceFunctions.Voucher_WIP_Finalization_Delete_WIP_Finalization_Txn
                        Return Voucher_WIP_Finalization.Delete_WIP_Finalization_Txn(CType(inParam, Voucher_WIP_Finalization.Param_Txn_Delete_Voucher_WIPFinalization), inBasicParam)
                        'Magazine
                    Case RealServiceFunctions.Magazine_Delete_Dispatch_Type
                        Return Magazine.Delete_Magazine_Dispatch_Type(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Dispatch_Type_Charges
                        Return Magazine.Delete_Magazine_Dispatch_Type_Charges(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Magazine
                        Return Magazine.Delete_Magazine(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Subscription_Type
                        Return Magazine.Delete_Magazine_Subscription_Type(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Subscription_Type_Fee
                        Return Magazine.Delete_Magazine_Subscription_Type_Fee(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Magazine_Membership_Profile
                        Return Magazine.Delete_Magazine_Membership_Profile(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Voucher_Magazine_Register_InsertMagazine_Txn
                        Return Voucher_Magazine_Register.Insert_VoucherMagazine_Txn(CType(inParam, Voucher_Magazine_Register.Param_Txn_Insert_VoucherMagazineMembership), inBasicParam)
                    Case RealServiceFunctions.Magazine_Delete_Membership_Request
                        Return Magazine_Request_Register.Delete_Magazine_Membership_Request(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Vouchers_ConfirmDraftEntry
                        Return Vouchers.ConfirmDraftEntry(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Vouchers_DeleteDraftEntry
                        Return Vouchers.DeleteDraftEntry(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.Vouchers_RejectDraftEntry
                        Return Vouchers.RejectDraftEntry(CType(inParam, Vouchers.Param_RejectDraftEntry), inBasicParam)
                    Case RealServiceFunctions.Magazine_Settle_Magazine_Ledgers
                        Return Magazine.Settle_Magazine_Ledgers(inBasicParam)
                        'stock
                    Case RealServiceFunctions.StockProfile_DeleteStockProfile
                        Return StockProfile.DeleteStockProfile(CType(inParam, String), inBasicParam)
                    Case RealServiceFunctions.SubItem_InsertSubItem_Txn
                        Return SubItem.InsertSubItem_Txn(CType(inParam, SubItem.Param_SubItem_Insert_Txn), inBasicParam)
                    Case RealServiceFunctions.SubItem_UpdateSubItem_Txn
                        Return SubItem.UpdateSubItem_Txn(CType(inParam, SubItem.Param_SubItem_Update_Txn), inBasicParam)
                    Case RealServiceFunctions.SubItem_DeleteSubItem_Txn
                        Return SubItem.DeleteSubItem_Txn(CType(inParam, Int32), inBasicParam)

                End Select
                'commit transaction
                '_Wrap_ExecuteFunctionDB.CommitTransaction()
                '_Wrap_ExecuteFunctionDB.CloseConnection()
            Catch ex As Exception
                'roll back transaction
6:              '_Wrap_ExecuteFunctionDB.RollBackTrans()
                '_Wrap_ExecuteFunctionDB.CloseConnection()
                Throw New Exception(ex.Message)
            End Try
            Return Nothing
        End Function
#End Region

#Region "--BLL Functions--"

        <WebMethod(MessageName:="Check conditions for Payment Edit")>
        Public Function CheckPaymentEdit(ByVal xTemp_MID As String, ByVal Basic_Param As Basic_Param, ByVal next_Unaudited_YearID As String, ByVal open_Ins_ID As String) As String
            Return Payments.CheckPaymentEdit(xTemp_MID, Basic_Param, next_Unaudited_YearID, open_Ins_ID)
        End Function

        <WebMethod(MessageName:="Get Payment Data"),
        XmlInclude(GetType(Payments.Param_PaymentData))>
        Public Function GetPaymentData(ByVal Rec_ID As String, ByVal Basic_Param As Basic_Param) As Payments.Param_PaymentData
            Return Payments.GetPaymentData(Rec_ID, Basic_Param)
        End Function

        '<WebMethod(MessageName:="Get Payment Details"),
        'XmlInclude(GetType(Payments.Param_paymentVoucherDetails)),
        'XmlInclude(GetType(Payments.Param_SaveButtonChecks)),
        'XmlInclude(GetType(Basic_Param))>
        'Public Function SavePaymentDetails(ByVal input_Param As Payments.Param_paymentVoucherDetails, ByVal Basic_Param As Basic_Param) As Payments.Param_SaveButtonChecks
        '    Return Payments.SavePaymentDetails(input_Param, Basic_Param)
        'End Function

#End Region

#Region "--Common functions--"
        Public Function GetRecordByID(inBasicParam As Basic_Param, ByVal table As Tables, ByVal RecID As String) As DataTable
            Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & RecID & "'"
            Return GetSingleRecord(table, Query, table.ToString(), inBasicParam)
        End Function
        Public Function GetRecordByID_SplVchrRefs(inBasicParam As Basic_Param, ByVal table As Tables, ByVal Txn_Rec_ID As String) As DataTable
            'Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID= '" & RecID & "'"
            Dim Query As String = " Select SUBSTRING( " &
                                        "( " &
                                             "SELECT ',' + TR_VOUCHER_REF AS 'data()' " &
                                                 "FROM transaction_d_reference_info where TXN_REC_ID = '" & Txn_Rec_ID & "' FOR XML PATH('') " &
                                        "), 2 , 9999) As SpecialVoucherRefs"
            'MsgBox(Query)
            Return GetSingleRecord(table, Query, table.ToString(), inBasicParam)
        End Function

        Public Function GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal TableName As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String) As DataTable
            Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " And " & ConditionColumnName & "= '" & ConditionValue & "'"
            Return GetSingleRecord(table, Query, TableName, inBasicParam)
        End Function

        Public Function GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String) As DataTable
            Dim Query As String = " SELECT * FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
            Return GetSingleRecord(table, Query, table.ToString(), inBasicParam)
        End Function

        Public Function GetRecordByColumn(inBasicParam As Basic_Param, ByVal table As Tables, ByVal SelectedColumns As String, ByVal ConditionColumnName As String, ByVal ConditionValue As String, ByVal ConditionColumn2Name As String, ByVal Condition2Value As String) As DataTable
            Dim Query As String = " SELECT " & SelectedColumns & " FROM " & table.ToString() & " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND " & ConditionColumnName & "= '" & ConditionValue & "'" & " AND " & ConditionColumn2Name & "= '" & Condition2Value & "' "
            Return GetSingleRecord(table, Query, table.ToString(), inBasicParam)
        End Function

        Public Function GetRecordByCustom(inBasicParam As Basic_Param, ByVal table As Tables, ByVal CustomCondition As String) As DataTable
            Dim Query As String = " SELECT * FROM " & table.ToString() & " " & CustomCondition
            Return GetSingleRecord(table, Query, table.ToString(), inBasicParam)
        End Function

        Public Sub CheckAuthorization(ByVal ReqPermission As Permissions, ByVal PermissionString As String,
         ByVal ConditionQuery As [String], ByVal InsertionRecID As [String], ByVal UsedTable As Tables, inBasicParam As ConnectOneWS.Basic_Param, Optional Txn_Date As Date = Nothing)
            If inBasicParam.openCenID = Nothing Then inBasicParam.openCenID = 0
            If inBasicParam.openUserID Is Nothing Then inBasicParam.openUserID = ""
            If inBasicParam.openYearID = Nothing Then inBasicParam.openYearID = 0
            '#Region "-- Under Maintenqance--checks if the service has been put under maintenance. This is done when we are doing some changes on server side and dont want centers/end user to use database."
            If ConfigurationManager.AppSettings("UnderMaintenance") IsNot Nothing Then
                If ConfigurationManager.AppSettings("UnderMaintenance").ToUpper().Equals("YES") Then
                    Throw New Exception(Common.UnderMaintenanceError())
                End If
            End If
            '#End Region

            Dim UserRole As String = ""
            Dim UserLocation As String = ""
            If inBasicParam.openUserID.Length > 0 Then
                Dim con As SqlConnection = Nothing

                Dim command As Object
                Dim ID As Object = Nothing, MaxValue As Object = Nothing
                OpenSqlConnection(con)
                command = con.CreateCommand()
                If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                    command.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                End If

                command.CommandTimeout = 0
                command.CommandText = "SELECT COD_AUDITED FROM COD_INFO WHERE CEN_ID =" & inBasicParam.openCenID.ToString & " AND COD_YEAR_ID =" & inBasicParam.openYearID.ToString & ";"

                '#Region, if the year is finalized (submitted in IT, marked as finally audited) , no changes are allowed in its data (leaving the recording of session time) for any kind of user
                Dim Audited As Boolean = command.ExecuteScalar()
                'If inBasicParam.openYearID = "1112" Then Audited = True
                If Audited And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Lock Or ReqPermission = Permissions.Complete) And inBasicParam.screen <> ClientScreen.Account_TDS_Register Then
                    If Not UsedTable = Tables.SO_LAST_USER_SESSION And Not UsedTable = Tables.ACTION_ITEM_INFO And Not UsedTable = Tables.COD_INFO And Not inBasicParam.screen = ClientScreen.Facility_ServiceReport And Not UsedTable = Tables.NOTIFICATION_BATCHES_INFO And Not UsedTable = Tables.WHATSAPP_MESSAGE_QUEUE And Not UsedTable = Tables.WHATSAPP_MSG_DELIVERY_LOG And Not UsedTable = Tables.EMAIL_SCHEDULER_QUEUE And Not UsedTable = Tables.EMAIL_SCHEDULER_LOG And Not UsedTable = Tables.MOBILE_LOGIN_OTP_INFO Then
                        Close_NonTransaction_SqlConnection(con)
                        Throw New Exception(Common.YearAlreadyAudited())
                    End If
                End If
                '#End Region

                Dim Restricted_Users As String = ConfigurationManager.AppSettings("Restricted_Users").ToString().Trim()
                If Restricted_Users.Length > 0 Then

                    Dim Restricted_Users_Array As String() = ConfigurationManager.AppSettings("Restricted_Users").ToString.Split(",")
                    Dim index As Integer = Array.IndexOf(Restricted_Users_Array, inBasicParam.openUserID)

                    If index >= 0 And inBasicParam.openCenID <> 0 And inBasicParam.openUserID <> "" And inBasicParam.openYearID <> 0 Then

                        command.CommandText = "SELECT CEN_INS_ID FROM CENTRE_INFO WHERE CEN_ID =" & inBasicParam.openCenID.ToString & ";"
                        Dim openInsID As String = command.ExecuteScalar()

                        If Not ((openInsID = "00001" Or openInsID = "00002") And inBasicParam.openYearID = 2324) Then
                            Close_NonTransaction_SqlConnection(con)
                            Throw New Exception(Common.Restricted_User(inBasicParam.openUserID))
                        End If

                    End If

                End If


                'command.CommandTimeout = 0
                'command.CommandText = "SELECT COALESCE(USER_READ_ONLY,0) USER_READ_ONLY FROM client_user_info WHERE USER_ID ='" & inBasicParam.openUserID & "' AND COALESCE(CEN_ID," + inBasicParam.openCenID.ToString() + ") = " + inBasicParam.openCenID.ToString() + ";"
                Dim user_info_Query = "SELECT COALESCE(USER_READ_ONLY,0) USER_READ_ONLY,USER_ROLE_ID,USER_LOCATION FROM client_user_info WHERE USER_ID ='" & inBasicParam.openUserID & "' AND COALESCE(CEN_ID," + inBasicParam.openCenID.ToString() + ") = " + inBasicParam.openCenID.ToString() + ";"
                '#Region, Readonly user not allowed any changes 
                'Dim ReadOnlyUser As Boolean = command.ExecuteScalar()
                Dim _RetList As DataTable '= List(ConnectOneWS.Tables.ATTACHMENT_INFO, user_info_Query, ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), inBasicParam)

                Dim dsResult As New DataSet()
                Dim dAp_new As SqlDataAdapter
                Dim Query As String = "SELECT COALESCE(USER_READ_ONLY,0) USER_READ_ONLY,USER_ROLE_ID,USER_LOCATION FROM client_user_info WHERE USER_ID ='" & inBasicParam.openUserID & "' AND COALESCE(CEN_ID," + inBasicParam.openCenID.ToString() + ") = " + inBasicParam.openCenID.ToString() + ";"
                dAp_new = New SqlDataAdapter(Query, con)
                dAp_new.SelectCommand.CommandTimeout = 0
                dAp_new.Fill(dsResult)
                _RetList = dsResult.Tables(0)

                If _RetList.Rows.Count = 0 Then
                    Close_NonTransaction_SqlConnection(con)
                    Throw New Exception(Common.InvalidUser)
                End If

                Dim ReadOnlyUser As Boolean = _RetList.Rows(0)("USER_READ_ONLY")
                Dim Role As Object = _RetList.Rows(0)("USER_ROLE_ID")
                Dim Location As Object = _RetList.Rows(0)("USER_LOCATION")

                'If inBasicParam.openYearID = "1112" Then Audited = True
                If ReadOnlyUser And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Lock Or ReqPermission = Permissions.Complete) And inBasicParam.screen <> ClientScreen.Options_ChangePassword Then
                    If Not UsedTable = Tables.SO_LAST_USER_SESSION Then
                        If ((UsedTable = Tables.ATTACHMENT_INFO Or UsedTable = Tables.ATTACHMENT_REFERENCE_INFO) And (ReqPermission = Permissions.Add)) Or (UsedTable = Tables.ACTION_ITEM_INFO) Then
                        Else
                            command.CommandText = "SELECT COUNT(*)CNT FROM so_center_audit_stats WHERE CAS_CEN_ID = " & inBasicParam.openCenID.ToString() & " AND CAS_AUDITOR_ID = '" & inBasicParam.openUserID & "' and CAS_STATUS = 1 and CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E7C1571C-F766-4A82-BAF9-38D82E5C213E','E15B3E64-E6A2-4018-9F25-6F3C7791A39D')"
                            Dim Allotted As Integer = command.ExecuteScalar()
                            If Allotted = 0 Then
                                Close_NonTransaction_SqlConnection(con)
                                Throw New Exception(Common.ReadOnlyUser())
                            End If
                        End If
                    End If
                End If
                '#End Region

                If inBasicParam.openYearID <> Nothing Then
                    Dim yearStartDate As String = "20" + inBasicParam.openYearID.ToString.Substring(0, 2) + "-04-01"
                    Dim yearEndDate As String = "20" + inBasicParam.openYearID.ToString.Substring(2, 2) + "-03-31"
                    command.CommandText = "SELECT CASE WHEN CEN_CANCELLATION_DATE BETWEEN '" + yearStartDate + "' AND '" + yearEndDate + "' THEN 1 ELSE 0 END FROM CENTRE_INFO WHERE CEN_ID =" & inBasicParam.openCenID.ToString & ";"
                    Dim Cancelled As Boolean = command.ExecuteScalar()
                    If Cancelled And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Lock Or ReqPermission = Permissions.Complete) Then
                        If Not UsedTable = Tables.SO_LAST_USER_SESSION And Not inBasicParam.screen = ClientScreen.Facility_ServiceReport Then
                            Close_NonTransaction_SqlConnection(con)
                            Throw New Exception(Common.YearAlreadyCancelled())
                        End If
                    End If
                End If

                'command.CommandText = "SELECT USER_ROLE_ID FROM client_user_info WHERE user_id ='" & inBasicParam.openUserID & "';"
                'Dim Role As Object = command.ExecuteScalar()
                If IsDBNull(Role) Or Role Is Nothing Then
                    Close_NonTransaction_SqlConnection(con)
                    Throw New Exception(Common.InvalidUser)
                End If
                UserRole = Role.ToString()

                'command.CommandText = "SELECT USER_LOCATION FROM client_user_info WHERE user_id ='" & inBasicParam.openUserID & "';"
                'Dim Location As Object = command.ExecuteScalar()
                If IsDBNull(Location) Or Location Is Nothing Then
                    Close_NonTransaction_SqlConnection(con)
                    Throw New Exception(Common.InvalidLocation)
                End If
                UserLocation = Location.ToString()

                ''#Region -- check year rights 
                ''Auditor can make changes in the year for which he/she has been allotted a center. in other years he can view the data , but not change anything
                'If UserRole.ToUpper() = "AUDITOR" And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Complete Or ReqPermission = Permissions.Lock) Then
                '    If Not UsedTable = Tables.SO_LAST_USER_SESSION And Not UsedTable = Tables.ATTACHMENT_INFO And Not UsedTable = Tables.ATTACHMENT_CHECKING_ALLOTTMENT And Not UsedTable = Tables.VOUCHING_AUDIT And Not UsedTable = Tables.VOUCHING_AUDIT_RESPONSES And Not UsedTable = Tables.VOUCHING_AUDIT_ALLOTTMENT Then
                '        command.CommandText = "SELECT EVE.HE_COD_YEAR_ID from SO_CENTER_AUDIT_STATS CAS INNER JOIN so_ho_event_info AS EVE ON CAS.CAS_EVENT_ID = EVE.HE_EVENT_ID WHERE CAS_CEN_ID = " & inBasicParam.openCenID.ToString & " AND CAS_AUDITOR_ID ='" & inBasicParam.openUserID & "' AND CAS_STATUS = 1 AND CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E15B3E64-E6A2-4018-9F25-6F3C7791A39D','E7C1571C-F766-4A82-BAF9-38D82E5C213E','0033301d-a56f-4dab-9547-0114ec36830a','F32C210E-BC0A-4A8F-8A3F-6FC01C916379');"
                '        ID = command.ExecuteScalar()
                '        If ID Is Nothing Then Throw New Exception("|No Allottment Found for Current Auditor|")
                '        If ID <> inBasicParam.openYearID Then Throw New Exception(Common.ChangesBarredForAuditorInUnAssignedYear(inBasicParam.openYearID, ID.ToString))
                '    End If
                'End If
                ''#End Region

                '#Region "--Login Time Restrictions--"
                '1. Auditor is not allowed to login from 'Select Center' screen 
                '2. If Login Restrictions have been applied against a center, then the center's nomal login is disallowed. This is basically done , when Audit is going on for that center. HO application adds/removes this restriction. 
                If inBasicParam.screen = ClientScreen.Start_Login Then
                    'If UserRole.ToUpper() = "AUDITOR" Then
                    '    Throw New Exception(Common.LoginBarredForAuditorFromSelectCenterError())
                    'End If
                    'AUDITOR not allowed to login from Select Centre screen, Only Client Role and Superuser can  }
                    command.CommandText = "SELECT COUNT(CR_CEN_ID) FROM so_client_restrictions WHERE CR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND UPPER(CR_TYPE) ='" & Common.CenterRestrictions.LoginBlocked & "';"
                    ID = command.ExecuteScalar()
                    If Convert.ToInt32(ID.ToString()) > 0 AndAlso UserRole.ToUpper() = "CLIENT ROLE" Then
                        'user not allowed to log in AS restriction for login has been put against it 
                        Close_NonTransaction_SqlConnection(con)
                        Throw New Exception(Common.LoginRestrictionError())
                    End If
                End If
                '#End Region

                '#Region, Magazine Related Client checks 
                If UserRole.ToUpper() = "CLIENT ROLE" Then
                    If inBasicParam.screen = ClientScreen.Magazine_Receipt_Register And Txn_Date > DateTime.MinValue Then
                        If (UsedTable = Tables.TRANSACTION_D_MASTER_INFO Or UsedTable = Tables.MAGAZINE_MEMBERSHIP_BALANCES_INFO Or UsedTable = Tables.MAGAZINE_MEMBERSHIP_INFO Or UsedTable = Tables.MAGAZINE_RECEIPT_INFO) And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit) Then
                            command.CommandText = "SELECT COUNT(CR_CEN_ID) FROM so_client_restrictions WHERE CR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND UPPER(CR_TYPE) ='" & Common.CenterRestrictions.MagazineDateBlocked & "' AND UPPER(CR_USER_ID) = '" & inBasicParam.openUserID.ToUpper & "' AND '" & Convert.ToDateTime(Txn_Date).ToString(Common.DateFormatLong) & "' BETWEEN CAST(CR_FROMDATE AS DATE) AND CAST(CR_TODATE AS DATE) ;"
                            ID = command.ExecuteScalar()
                            If ID > 0 Then
                                Close_NonTransaction_SqlConnection(con)
                                Throw New Exception(Common.MagazineDateRestricted(Convert.ToDateTime(Txn_Date).ToString(Common.Date_Format_DD_MMM_YYYY)))
                            End If
                        End If
                    End If
                End If
                '#End Region

                If (UsedTable = Tables.TRANSACTION_D_MASTER_INFO Or UsedTable = Tables.TRANSACTION_INFO Or UsedTable = Tables.ATTACHMENT_INFO Or UsedTable = Tables.ATTACHMENT_REFERENCE_INFO Or UsedTable = Tables.TRANSACTION_DOC_MAPPING_RESPONSE Or inBasicParam.screen.ToString().ToUpper().StartsWith("PROFILE")) And (ReqPermission = Permissions.Add Or ReqPermission = Permissions.Delete Or ReqPermission = Permissions.Edit) Then 'txn response table 
                    Dim YrFromDate As DateTime = New DateTime(CInt(inBasicParam.openYearID.ToString().Substring(0, 2)) + 2000, 4, 1)
                    Dim YrToDate As DateTime = New DateTime(CInt(inBasicParam.openYearID.ToString().Substring(2, 2)) + 2000, 3, 31)
                    command.CommandText = "SELECT COUNT(CR_CEN_ID) FROM SO_Client_Restrictions WHERE CR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(CR_TYPE) ='" & Common.CenterRestrictions.ReadAllWriteBlockedForAllUsers & "' AND CAST(CR_FROMDATE AS DATE) = '" & Convert.ToDateTime(YrFromDate).ToString(Common.DateFormatLong) & "' AND CAST(CR_TODATE AS DATE) = '" & Convert.ToDateTime(YrToDate).ToString(Common.DateFormatLong) & "' ;"
                    ID = command.ExecuteScalar()
                    If ID > 0 And Not inBasicParam.screen = ClientScreen.Facility_ServiceReport Then
                        Close_NonTransaction_SqlConnection(con)
                        Throw New Exception(Common.financial_changes_blocked())
                    End If
                End If

                '#Region "--Manage Session"
                'Disallow 9005(Mumbai HQ) usage from supoer user too . It is either allowed to center login or to auditor only when the center is allotted for audit. 
                ' If screen = ClientScreen.Start_Login Or (screen = ClientScreen.Home_StartUp And UsedTable = Tables.SO_LAST_USER_SESSION) Then
                command.CommandText = "SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " + inBasicParam.openCenID.ToString + " ;"
                Dim PAD As String = command.ExecuteScalar()

                command.CommandText = " SELECT Count(*) " &
                             " FROM centre_info AS CI INNER JOIN so_client_user_task AS CUT ON CUT.CUT_CEN_ID = CI.CEN_ID INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1" &
                             " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 and cod.REC_STATUS  IN (0,1,2) WHERE CUT_CU_ID = '" & inBasicParam.openUserID & "' AND  CI.REC_STATUS IN (0,1,2) AND CI.CEN_ID = " + inBasicParam.openCenID.ToString + ""
                Dim Allocation As Integer = command.ExecuteScalar()

                command.CommandText = "SELECT COUNT(*) FROM centre_task_info WHERE TASK_NAME ='SUPERUSER_BARRED_CENTER' AND CEN_ID = " + inBasicParam.openCenID.ToString + " ;"
                Dim CenterBarredForSuperUsers As String = command.ExecuteScalar()
                If UserRole.ToUpper() = "SUPERUSER" And Allocation = 0 And (0 < CenterBarredForSuperUsers) Then 'Super Users are barred in that UID for that FY
                    command.CommandText = "SELECT COUNT(*) FROM so_client_user_task WHERE CUT_CEN_ID = " + inBasicParam.openCenID.ToString + " AND UPPER(CUT_CU_ID) = UPPER('" + inBasicParam.openUserID.ToString + "') ;"
                    Dim ScreenPermissionCountForUserInCenter As String = command.ExecuteScalar()
                    If 0 = ScreenPermissionCountForUserInCenter Then 'Current user has no screen permissions in this UID
                        Close_NonTransaction_SqlConnection(con)
                        Throw New Exception(Common.SpecialCentreError())
                    End If
                End If

                'If UserRole.ToUpper() = "SUPERUSER" And (PAD = "9005") And Allocation = 0 And inBasicParam.openUserID.ToUpper <> "BKSAURABH" And inBasicParam.openUserID.ToUpper <> "BKLALIT" And inBasicParam.openUserID.ToUpper <> "BKKRISHNA" And inBasicParam.openUserID.ToUpper <> "A-CHANDRAPRAKASH" And inBasicParam.openUserID.ToUpper <> "A-BHASHA" And inBasicParam.openUserID.ToUpper <> "A-MANSI" And inBasicParam.openUserID.ToUpper <> "BKANAND" And inBasicParam.openUserID.ToUpper <> "GS-ASHWINI" And inBasicParam.openUserID.ToUpper <> "GS-JAINAM" And inBasicParam.openUserID.ToUpper <> "GS-KALPESH" And inBasicParam.openUserID.ToUpper <> "GS-KHUBI" And inBasicParam.openUserID.ToUpper <> "GS-PRIYANKA" And inBasicParam.openUserID.ToUpper <> "GS-SANDEEP" And inBasicParam.openUserID.ToUpper <> "GS-SURBHI" And UserLocation.ToUpper <> "MUMBAI" Then
                '    Throw New Exception(Common.SpecialCentreError())
                'End If
                ''Health Safety / Special Services / RERF Abu / BKES Abu / Abu - India One Solar Plant
                'If UserRole.ToUpper() = "SUPERUSER" And ((inBasicParam.openCenID = 5938) Or (inBasicParam.openCenID = 5944) Or (inBasicParam.openCenID = 5906)) And Allocation = 0 And inBasicParam.openUserID.ToUpper <> "BKLALIT" And inBasicParam.openUserID.ToUpper <> "BKKRISHNA" And inBasicParam.openUserID.ToUpper <> "A-CHANDRAPRAKASH" And inBasicParam.openUserID.ToUpper <> "BKSAURABH" And inBasicParam.openUserID.ToUpper <> "BKANAND" And inBasicParam.openUserID.ToUpper <> "A-CHINAKANI" And inBasicParam.openUserID.ToUpper <> "A-BHASHA" And inBasicParam.openUserID.ToUpper <> "A-MANSI" And inBasicParam.openUserID.ToUpper <> "GS-ASHWINI" And inBasicParam.openUserID.ToUpper <> "GS-JAINAM" And inBasicParam.openUserID.ToUpper <> "GS-KALPESH" And inBasicParam.openUserID.ToUpper <> "GS-KHUBI" And inBasicParam.openUserID.ToUpper <> "GS-PRIYANKA" And inBasicParam.openUserID.ToUpper <> "GS-SANDEEP" And inBasicParam.openUserID.ToUpper <> "GS-SURBHI" Then
                '    Throw New Exception(Common.SpecialCentreError())
                'End If

                'If UserRole.ToUpper() = "SUPERUSER" And ((inBasicParam.openCenID = 5070) Or (inBasicParam.openCenID = 4419) Or (inBasicParam.openCenID = 4219) Or (inBasicParam.openCenID = 4220)) And Allocation = 0 And inBasicParam.openUserID.ToUpper <> "BKLALIT" And inBasicParam.openUserID.ToUpper <> "BKKRISHNA" And inBasicParam.openUserID.ToUpper <> "A-CHANDRAPRAKASH" And inBasicParam.openUserID.ToUpper <> "BKSAURABH" And inBasicParam.openUserID.ToUpper <> "BKANAND" And inBasicParam.openUserID.ToUpper <> "A-CHINAKANI" And inBasicParam.openUserID.ToUpper <> "A-BHASHA" And inBasicParam.openUserID.ToUpper <> "A-MANSI" And inBasicParam.openUserID.ToLower <> "bkratnakar" And inBasicParam.openUserID.ToUpper <> "GS-ASHWINI" And inBasicParam.openUserID.ToUpper <> "GS-JAINAM" And inBasicParam.openUserID.ToUpper <> "GS-KALPESH" And inBasicParam.openUserID.ToUpper <> "GS-KHUBI" And inBasicParam.openUserID.ToUpper <> "GS-PRIYANKA" And inBasicParam.openUserID.ToUpper <> "GS-SANDEEP" And inBasicParam.openUserID.ToUpper <> "GS-SURBHI" Then
                '    Throw New Exception(Common.SpecialCentreError())
                'End If

                '#Region "--Screen Restrictions--"
                'command.CommandText = "SELECT USER_ROLE_ID FROM CLIENT_USER_INFO WHERE USER_ID='" + UserID + "'";
                'object UserRole  = command.ExecuteScalar();



                Dim dAp As SqlDataAdapter = Nothing
                If UserRole IsNot Nothing Then
                    'User Exists 
                    If UserRole.ToString().ToUpper() <> Common.ClientRoles.SuperUser Then
                        Dim Restrictions As New DataSet()
                        If Common.UseSQL() Then
                            dAp = New SqlDataAdapter("SELECT CR_TYPE, CR_SCREEN FROM SO_Client_Restrictions WHERE CR_CEN_ID=" & inBasicParam.openCenID.ToString & "", CType(con, SqlConnection))
                        End If
                        dAp.SelectCommand.CommandTimeout = 0
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        dAp.Fill(Restrictions)
                        If Restrictions.Tables(0).Rows.Count > 0 Then
                            'avoid putting restrictions on each screen , rather put restrictions on NULL to lock all 
                            dAp.Dispose()
                            Restrictions.Clear()
                            If Common.UseSQL() Then 'CHECK IF THERE ARE restrictions on the screen demanded by end user or else restrictions are in place for all screen(in record CR_SCREEN = NULL)
                                dAp = New SqlDataAdapter("SELECT CR_TYPE, ID FROM SO_Client_Restrictions WHERE CR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND (UPPER(CR_SCREEN) ='" & inBasicParam.screen.ToString().ToUpper() & "' OR CR_SCREEN IS NULL) ORDER BY ID DESC", CType(con, SqlConnection))
                            End If
                            If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                                dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                            End If
                            dAp.Fill(Restrictions)
                            If Restrictions.Tables(0).Rows.Count > 0 Then 'Current Screen is restricted
                                For Each cRow As DataRow In Restrictions.Tables(0).Rows
                                    Select Case cRow("CR_TYPE").ToString()
                                        Case Common.CenterRestrictions.ReadAllWriteBlockedAccountsSubmitted
                                            'No Period Restrictions for Auditors
                                            If UserRole.ToString().ToUpper() = Common.ClientRoles.Auditor Then Exit Select
                                            'If (REJECTED_OR_CORRECTED_ENTRIES_COUNT > 0 And (ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Delete)) Then Exit Select
                                            If NeedcheckPermissionSoughtAgainstRestriction(ReqPermission, cRow("CR_TYPE").ToString()) Then  'checks if the current operation type needs to be blocked for period restriction
                                                Dim RestrictionPeriod As DataTable = GetAccountSubmittedPeriod(inBasicParam.openCenID, cRow("ID").ToString())
                                                ' period in which transactions are restricted for center 
                                                If RestrictionPeriod.Rows.Count > 0 And UserRole.ToUpper() <> "SUPERUSER" And UserRole.ToUpper() <> "AUDITOR" Then
                                                    If ReqPermission <> Permissions.Add Then
                                                        ' checks TR_Date as records are already present . In case of update/delete/lock/unlock , the entry is already present,so we check the original transaction date of the entry(before changes are made, as txn date too maybe changed) from database and decide if the same is within restricted period
                                                        If IsPeriodRestricted(ConditionQuery, inBasicParam.openCenID, UsedTable, RestrictionPeriod) And ConditionQuery.Length > 0 And UsedTable <> Tables.ATTACHMENT_INFO And UsedTable <> Tables.ATTACHMENT_REFERENCE_INFO And UsedTable <> Tables.TRANSACTION_DOC_MAPPING_RESPONSE And inBasicParam.screen <> ClientScreen.Accounts_DonationRegister Then
                                                            'user not allowed to do current operation in sought period  
                                                            Close_NonTransaction_SqlConnection(con)
                                                            Throw New Exception(Common.AccountsSubmitted_PeriodRestrictionError(Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString())))
                                                        End If
                                                    End If
                                                    If ReqPermission <> Permissions.Delete And Txn_Date > DateTime.MinValue Then
                                                        ' checks TR_Date after change, if the same is within the restricted period 
                                                        Dim RecCondition As String = "REC_ID ='" & InsertionRecID & "'"
                                                        If ReqPermission <> Permissions.Add Then
                                                            RecCondition = ConditionQuery
                                                        End If
                                                        If (IsPeriodRestricted_After_Change(RecCondition, UsedTable, RestrictionPeriod, Txn_Date, inBasicParam.openCenID, InsertionRecID) And inBasicParam.screen <> ClientScreen.Accounts_DonationRegister) Then
                                                            'user not allowed to do current operation in sought period  
                                                            Close_NonTransaction_SqlConnection(con)
                                                            Throw New Exception(Common.AccountsSubmitted_PeriodRestrictionError(Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString())))
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            Exit Select
                                            'Period Based Restrictions 
                                        Case Common.CenterRestrictions.ReadAllWriteBlockedForSomePeriod
                                            'No Period Restrictions for Auditors
                                            ' If inBasicParam.screen.ToString().ToUpper().Contains("MAGAZINE") Then Exit Select
                                            If UserRole.ToString().ToUpper() = Common.ClientRoles.Auditor Then Exit Select
                                            'If (REJECTED_OR_CORRECTED_ENTRIES_COUNT > 0 And (ReqPermission = Permissions.Edit Or ReqPermission = Permissions.Delete)) Then Exit Select
                                            If NeedcheckPermissionSoughtAgainstRestriction(ReqPermission, cRow("CR_TYPE").ToString()) Then  'checks if the current operation type needs to be blocked for period restriction
                                                Dim RestrictionPeriod As DataTable = GetRestrictedPeriod(inBasicParam.openCenID, cRow("ID").ToString())
                                                ' period in which transactions are restricted for center 
                                                If RestrictionPeriod.Rows.Count > 0 Then
                                                    If ReqPermission <> Permissions.Add Then
                                                        ' checks TR_Date as records are already present . In case of update/delete/lock/unlock , the entry is already present,so we check the original transaction date of the entry(before changes are made, as txn date too maybe changed) from database and decide if the same is within restricted period
                                                        If IsPeriodRestricted(ConditionQuery, inBasicParam.openCenID, UsedTable, RestrictionPeriod) And ConditionQuery.Length > 0 And inBasicParam.screen <> ClientScreen.Accounts_DonationRegister Then
                                                            'user not allowed to do current operation in sought period  
                                                            If RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE") Is Nothing Or IsDBNull(RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE")) Then
                                                                Close_NonTransaction_SqlConnection(con)
                                                                Throw New Exception(Common.PeriodRestrictionError(Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString())))
                                                            Else
                                                                Close_NonTransaction_SqlConnection(con)
                                                                Throw New Exception(Common.PeriodRestrictionError(Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()), RestrictionPeriod.Rows(0)("Restriction_Scope").ToString().ToUpper()))
                                                            End If

                                                        End If
                                                    End If
                                                    If ReqPermission <> Permissions.Delete And (Txn_Date > DateTime.MinValue Or UsedTable = Tables.ATTACHMENT_INFO Or UsedTable = Tables.ATTACHMENT_REFERENCE_INFO Or UsedTable = Tables.TRANSACTION_DOC_MAPPING_RESPONSE) Then
                                                        ' checks TR_Date after change, if the same is within the restricted period 
                                                        Dim RecCondition As String = "REC_ID ='" & InsertionRecID & "'"
                                                        If ReqPermission <> Permissions.Add Then
                                                            RecCondition = ConditionQuery
                                                        End If
                                                        'checks for full restrictions and skips partial restrictions
                                                        If RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE") Is Nothing Or IsDBNull(RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE")) Then
                                                            If IsPeriodRestricted_After_Change(RecCondition, UsedTable, RestrictionPeriod, Txn_Date, inBasicParam.openCenID, InsertionRecID) And inBasicParam.screen <> ClientScreen.Accounts_DonationRegister Then
                                                                'user not allowed to do current operation in sought period  
                                                                Close_NonTransaction_SqlConnection(con)
                                                                Throw New Exception(Common.PeriodRestrictionError(Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString())))
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            Exit Select
                                        Case Common.CenterRestrictions.ReadNoneWriteNone, Common.CenterRestrictions.ReadAllWriteNone
                                            If NeedcheckPermissionSoughtAgainstRestriction(ReqPermission, cRow("CR_TYPE").ToString()) Then
                                                'checks if the current operation type needs to be blocked
                                                'user not allowed to do current operation
                                                Close_NonTransaction_SqlConnection(con)
                                                Throw New Exception(Common.ScreenRestrictionError())
                                            End If
                                            Exit Select
                                        Case Common.CenterRestrictions.ProfileBlocked
                                            '#6291 fix
                                            'If inBasicParam.screen = ClientScreen.Profile_Insurance_ChangeDetail Or inBasicParam.screen = ClientScreen.Profile_Insurance_ChangeValue Or inBasicParam.screen = ClientScreen.Profile_ChangeLocation Then 'there operations are allowed even if profile is blocked after audit 
                                            '    Exit Select
                                            'End If
                                            If inBasicParam.screen = ClientScreen.Profile_Cash And ReqPermission = Permissions.Add Then 'add new cash balance row for new finanacial year is allowed
                                                Exit Select
                                            End If
                                            'Check if centre has a Completed Year Already 
                                            'Dim Query_Years As String = "SELECT COUNT(COD_YEAR_ID) FROM COD_INFO WHERE REC_STATUS IN (0,1,2) AND CEN_ID ='" & inBasicParam.openCenID & "' AND COD_YEAR_ID NOT IN (SELECT DISTINCT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = COD_INFO.CEN_ID) AND COD_YEAR_ID < '" & inBasicParam.openYearID & "'"
                                            'Block profiles for all such years which are finalized instead of just first year
                                            Dim Query_Years As String = "SELECT CASE WHEN " & inBasicParam.openYearID.ToString & " IN (SELECT  HEI.HE_COD_YEAR_ID   FROM so_client_restrictions AS SCR INNER JOIN so_ho_event_info AS HEI ON HEI.HE_EVENT_ID = SCR.CR_EVENT_ID WHERE CR_TYPE = 'PROFILE_BLOCKED' AND CR_CEN_ID = " & inBasicParam.openCenID.ToString & ") THEN 1 ELSE 0 END AS 'Profile Blocked'"
                                            command.CommandText = Query_Years
                                            MaxValue = command.ExecuteScalar()
                                            If (inBasicParam.screen.ToString().ToUpper().StartsWith("PROFILE") Or inBasicParam.screen = ClientScreen.Help_Attachments) And inBasicParam.screen <> ClientScreen.Profile_Core And UserRole.ToString().ToUpper() = Common.ClientRoles.Center_Admin And MaxValue = 1 Then ' Or inBasicParam.screen = ClientScreen.Help_Attachments
                                                'checks if the current operation type needs to be blocked
                                                If NeedcheckPermissionSoughtAgainstRestriction(ReqPermission, cRow("CR_TYPE").ToString()) Then
                                                    'user not allowed to alter profiles 
                                                    Close_NonTransaction_SqlConnection(con)
                                                    Throw New Exception(Common.ProfileRestrictionError())
                                                End If
                                            End If
                                            Exit Select
                                    End Select
                                Next
                            End If
                        End If
                        Restrictions.Dispose()
                    End If
                    '#End Region
                End If
                Close_NonTransaction_SqlConnection(con)
                ' End Using
                Return
            End If
        End Sub

#End Region

#Region "--Pvt Functions--"
        Private Sub OpenSqlConnection(ByRef con As SqlConnection)
            If _Wrap_ExecuteFunctionDB.IsConnectionOpen = False Then
                con = New SqlConnection(Common.ConnectionString)
            Else
                con = _Wrap_ExecuteFunctionDB.Connection
            End If
            If (con.State <> ConnectionState.Open) Then
                con.Open()
            End If
        End Sub
        Private Sub Close_NonTransaction_SqlConnection(ByRef con As SqlConnection)
            If _Wrap_ExecuteFunctionDB.IsConnectionOpen = False Then
                If Not con Is Nothing Then
                    con.Close()
                    con.Dispose()
                End If
            End If
        End Sub
        ''' <summary>
        ''' checks if the sought permission is allowed under the applied Center Restriction
        ''' </summary>
        ''' <param name="p"></param>
        ''' <param name="Restriction"></param>
        ''' <returns></returns>
        Private Function NeedcheckPermissionSoughtAgainstRestriction(ByVal p As Permissions, ByVal Restriction As String) As Boolean
            Select Case Restriction
                Case Common.CenterRestrictions.ReadAllWriteNone, Common.CenterRestrictions.ReadAllWriteBlockedAccountsSubmitted, Common.CenterRestrictions.ReadAllWriteBlockedForSomePeriod, Common.CenterRestrictions.ProfileBlocked
                    If p = Permissions.Delete OrElse p = Permissions.Complete OrElse p = Permissions.Edit OrElse p = Permissions.Add OrElse p = Permissions.Lock Then
                        Return True
                    End If
                    Exit Select
                Case Common.CenterRestrictions.ReadNoneWriteNone
                    Return True
            End Select
            Return False
        End Function

        ''' <summary>
        ''' For other than Insertion Operations
        ''' </summary>
        ''' <param name="Affected_Records"></param>
        ''' <param name="CenID"></param>
        ''' <param name="usedTable"></param>
        ''' <param name="RestrictionPeriod"></param>
        ''' <returns></returns>
        Private Function IsPeriodRestricted(ByVal Affected_Records As String, ByVal CenID As Integer, ByVal usedTable As Tables, ByVal RestrictionPeriod As DataTable) As Boolean
            If (usedTable = Tables.TRANSACTION_INFO Or usedTable = Tables.TRANSACTION_D_MASTER_INFO Or usedTable = Tables.TRANSACTION_D_PURPOSE_INFO) Then
                Dim query As String = "SELECT COUNT(*) FROM " & usedTable.ToString()
                'Round Fix for DeleteDonation_Txn
                If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                    query += " AS TP INNER JOIN TRANSACTION_INFO AS TI ON TP.TR_REC_ID = TI.REC_ID "
                    Affected_Records = "tp." + Affected_Records
                End If
                'End
                If Affected_Records.Trim().Length > 0 Then
                    query += " WHERE " & Affected_Records & " AND CAST(TR_DATE AS DATE) BETWEEN '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()).ToString(Common.DateFormatLong) & "' AND '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()).ToString(Common.DateFormatLong) & "'"
                    If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                        query += " AND TI.TR_CEN_ID = " & CenID.ToString & ""
                    Else
                        query += " AND TR_CEN_ID = " & CenID.ToString & ""
                    End If
                Else
                    query += " WHERE TR_CEN_ID = " & CenID.ToString & ""
                End If
                ''Audit rejection and correction check 
                'If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                '    query += " AND COALESCE(TI.TR_M_ID, TI.REC_ID) NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & ")"
                'ElseIf usedTable = Tables.TRANSACTION_D_MASTER_INFO Then
                '    query += " AND REC_ID NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & " )"
                'Else
                '    query += " AND COALESCE(TR_M_ID, REC_ID) NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & ")"
                'End If


                If usedTable <> Tables.TRANSACTION_INFO And RestrictionPeriod.Columns.Contains("CR_RESTRICTION_TYPE") Then
                    If Not RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE") Is Nothing Then
                        Return False
                    End If
                End If

                If RestrictionPeriod.Columns.Contains("CR_RESTRICTION_TYPE") Then
                    If Not RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE") Is Nothing And usedTable = Tables.TRANSACTION_INFO Then
                        Dim RestrictionType As String = RestrictionPeriod.Rows(0)("CR_RESTRICTION_TYPE").ToString().ToLower()
                        Dim RestrictionID As String = RestrictionPeriod.Rows(0)("CR_RESTRICTION_REF_ID").ToString().ToLower()
                        Select Case RestrictionType
                            Case "ledger"
                                query += " AND (COALESCE(TR_CR_LED_ID,'')= '" + RestrictionID + "' OR COALESCE(TR_DR_LED_ID,'')= '" + RestrictionID + "' ) "
                            Case "party"
                                query += " AND (COALESCE(TR_AB_ID_1,'')= '" + RestrictionID + "' OR COALESCE(TR_AB_ID_2,'')= '" + RestrictionID + "' ) "
                            Case "bank"
                                query += " AND (COALESCE(TR_SUB_CR_LED_ID,'')= '" + RestrictionID + "' OR COALESCE(TR_SUB_DR_LED_ID,'')= '" + RestrictionID + "' ) "
                        End Select

                    End If
                End If
                Dim dsResult As New DataSet()
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim dAp As SqlDataAdapter
                        dAp = New SqlDataAdapter(query, con)
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        dAp.Fill(dsResult)
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else
                End If
                If Convert.ToInt32(dsResult.Tables(0).Rows(0)(0).ToString()) > 0 Then
                    Return True
                End If
            End If
            If (usedTable = Tables.ATTACHMENT_INFO) Then
                Dim query As String = "SELECT COUNT(*) FROM " & usedTable.ToString()
                query += " AS AI INNER JOIN attachment_reference_info AS ARI ON ARI.ALI_ATTACHMENT_ID = AI.REC_ID "
                query += " INNER JOIN transaction_info AS TI ON COALESCE(TI.TR_M_ID,TI.REC_ID) = ARI.ALI_REF_REC_ID"
                If Affected_Records.Trim().Length > 0 Then
                    query += " WHERE AI." & Affected_Records & " AND CAST(TR_DATE AS DATE) BETWEEN '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()).ToString(Common.DateFormatLong) & "' AND '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()).ToString(Common.DateFormatLong) & "'"
                    If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                        query += " AND TI.TR_CEN_ID = " & CenID.ToString & ""
                    Else
                        query += " AND TR_CEN_ID = " & CenID.ToString & ""
                    End If
                Else
                    query += " WHERE TR_CEN_ID = " & CenID.ToString & ""
                End If
                ''Audit rejection and correction check 
                'query += " AND COALESCE(TI.TR_M_ID, TI.REC_ID) NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & ")"

                Dim dsResult As New DataSet()
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim dAp As SqlDataAdapter
                        dAp = New SqlDataAdapter(query, con)
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        dAp.Fill(dsResult)
                        Close_NonTransaction_SqlConnection(con)
                        'End Using
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                Else
                End If
                If Convert.ToInt32(dsResult.Tables(0).Rows(0)(0).ToString()) > 0 Then
                    Return True
                End If
            End If
            If (usedTable = Tables.ATTACHMENT_REFERENCE_INFO) Then
                Dim query As String = "SELECT COUNT(*) FROM " & usedTable.ToString()
                query += " AS ARI "
                query += " INNER JOIN transaction_info AS TI ON COALESCE(TI.TR_M_ID,TI.REC_ID) = ARI.ALI_REF_REC_ID"
                If Affected_Records.Trim().Length > 0 Then
                    query += " WHERE " & Affected_Records & " AND CAST(TR_DATE AS DATE) BETWEEN '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()).ToString(Common.DateFormatLong) & "' AND '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()).ToString(Common.DateFormatLong) & "'"
                    If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                        query += " AND TI.TR_CEN_ID = " & CenID.ToString & ""
                    Else
                        query += " AND TR_CEN_ID = " & CenID.ToString & ""
                    End If
                Else
                    query += " WHERE TR_CEN_ID = " & CenID.ToString & ""
                End If
                ''Audit rejection and correction check 
                'query += " AND COALESCE(TI.TR_M_ID, TI.REC_ID) NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & ")"

                Dim dsResult As New DataSet()
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim dAp As SqlDataAdapter
                        dAp = New SqlDataAdapter(query, con)
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        dAp.Fill(dsResult)
                        Close_NonTransaction_SqlConnection(con)
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                    'End Using
                Else
                End If

                If Convert.ToInt32(dsResult.Tables(0).Rows(0)(0).ToString()) > 0 Then
                    Return True
                End If
            End If
            If (usedTable = Tables.TRANSACTION_DOC_MAPPING_RESPONSE) Then
                Dim query As String = "SELECT COUNT(*) FROM " & usedTable.ToString()
                query += " AS RESP "
                query += " INNER JOIN transaction_info AS TI ON COALESCE(TI.TR_M_ID,TI.REC_ID) = RESP.TR_DOC_RESP_REF_REC_ID"
                If Affected_Records.Trim().Length > 0 Then
                    query += " WHERE " & Affected_Records & " AND CAST(TR_DATE AS DATE) BETWEEN '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()).ToString(Common.DateFormatLong) & "' AND '" & Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()).ToString(Common.DateFormatLong) & "'"
                    If usedTable = Tables.TRANSACTION_D_PURPOSE_INFO Then
                        query += " AND TI.TR_CEN_ID = " & CenID.ToString & ""
                    Else
                        query += " AND TR_CEN_ID = " & CenID.ToString & ""
                    End If
                Else
                    query += " WHERE TR_CEN_ID = " & CenID.ToString & ""
                End If
                ''Audit rejection and correction check 
                'query += " AND COALESCE(TI.TR_M_ID, TI.REC_ID) NOT IN (SELECT VA_ENTRY_ID FROM Vouching_Audit VA WHERE ((VA_VOUCHER_STATUS='REJECTED' AND VA_DURING_AUDIT=1) OR VA_IS_CORRECTED_ENTRY = 1) AND VA_LATEST = 1 AND VA_CEN_ID = " & CenID.ToString & ")"

                Dim dsResult As New DataSet()
                If Common.UseSQL Then
                    Dim con As SqlConnection = Nothing
                    OpenSqlConnection(con)
                    Try
                        Dim dAp As SqlDataAdapter
                        dAp = New SqlDataAdapter(query, con)
                        If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                            dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                        End If
                        dAp.Fill(dsResult)
                        Close_NonTransaction_SqlConnection(con)
                    Catch ex As Exception
                        Close_NonTransaction_SqlConnection(con)
                        Throw
                    End Try
                    'End Using
                Else
                End If

                If Convert.ToInt32(dsResult.Tables(0).Rows(0)(0).ToString()) > 0 Then
                    Return True
                End If
            End If
            Return False
        End Function

        ''' <summary>
        ''' For Insertion Operations Only 
        ''' </summary>
        ''' <param name="Query"></param>
        ''' <param name="usedTable"></param>
        ''' <param name="RestrictionPeriod"></param>
        ''' <returns></returns>
        Private Function IsPeriodRestricted_After_Change(ByVal RecCondition As [String], ByVal usedTable As Tables, ByVal RestrictionPeriod As DataTable, Txn_Date As Date, Optional Cen_ID As Integer = Nothing, Optional txnRecID As String = "") As Boolean
            If Common.UseSQL Then
                Return IsPeriodRestricted_After_Change_SQL(RecCondition, usedTable, RestrictionPeriod, Txn_Date, Cen_ID, txnRecID)
            End If
            Return False
        End Function

        ''' <summary>
        ''' In SQL, For Insertion Operations Only 
        ''' </summary>
        ''' <param name="Query"></param>
        ''' <param name="usedTable"></param>
        ''' <param name="RestrictionPeriod"></param>
        ''' <returns></returns>
        Private Function IsPeriodRestricted_After_Change_SQL(ByVal RecCondition As [String], ByVal usedTable As Tables, ByVal RestrictionPeriod As DataTable, Txn_Date As Date, Optional Cen_ID As Integer = Nothing, Optional txnRecID As String = "") As Boolean
            Dim con As SqlConnection = Nothing
            Try
                If usedTable = Tables.TRANSACTION_INFO OrElse usedTable = Tables.TRANSACTION_D_MASTER_INFO OrElse usedTable = Tables.TRANSACTION_DOC_MAPPING_RESPONSE OrElse usedTable = Tables.ATTACHMENT_REFERENCE_INFO OrElse usedTable = Tables.ATTACHMENT_INFO Then
                    If Txn_Date = DateTime.MinValue Then
                        Txn_Date = "1800-01-01" '' added for attachments and remarks just to let in 
                    End If
                    Dim parameters As String() = {"X_TABLE_NAME", "X_REC_CONDITION", "X_FROM_DATE", "X_TO_DATE", "X_TXN_DATE", "X_CEN_ID", "TXN_REC_ID"}
                    Dim values As Object() = {usedTable.ToString().ToUpper(), RecCondition, Convert.ToDateTime(RestrictionPeriod.Rows(0)("FROMDATE").ToString()), Convert.ToDateTime(RestrictionPeriod.Rows(0)("TODATE").ToString()), Txn_Date, Cen_ID, txnRecID}
                    Dim dbTypes As System.Data.DbType() = {System.Data.DbType.[String], System.Data.DbType.[String], System.Data.DbType.[Date], System.Data.DbType.[Date], System.Data.DbType.[Date], System.Data.DbType.Int32, System.Data.DbType.String}
                    'Dim lengths As Integer() = {Query.Length, usedTable.ToString().Length, RecCondition.Length, 0, 0, 0}

                    OpenSqlConnection(con)
                    Dim dAp As SqlDataAdapter
                    Dim oParams As SqlParameter() = New SqlParameter(parameters.Count - 1) {}
                    Dim cParam As New SqlParameter()
                    For index As Int16 = 0 To parameters.Count - 1
                        cParam = New SqlParameter()
                        cParam.DbType = dbTypes(index)
                        cParam.ParameterName = parameters(index)
                        'if (cParam.DbType == DbType.String)
                        '   cParam.Size = lengths[index];
                        cParam.Value = values(index)
                        oParams(index) = cParam
                    Next
                    Dim dsResult As New DataSet()
                    'Using con
                    dAp = New SqlDataAdapter("Is_Period_Restricted", con)
                    dAp.SelectCommand.CommandType = CommandType.StoredProcedure
                    For Each currParam As SqlParameter In oParams
                        dAp.SelectCommand.Parameters.Add(currParam)
                    Next
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    dAp.Fill(dsResult, "Restricted")
                    Close_NonTransaction_SqlConnection(con)
                    'End Using
                    If dsResult.Tables(0).Rows.Count > 0 Then
                        If Convert.ToInt32(dsResult.Tables(0).Rows(0)("Restricted").ToString()) = 1 Then
                            Return True
                        End If
                    End If
                End If
                Return False
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Function

        Public Function IsPeriodPartiallyRestricted(openCenID As Int32, txnDate As DateTime, RefRecID As String) As Boolean
            Dim _Table As DataTable = GetRestrictedPeriod(openCenID)
            For Each cRow As DataRow In _Table.Rows
                If Not cRow("CR_RESTRICTION_TYPE") Is Nothing Then
                    If cRow("CR_RESTRICTION_TYPE").ToString().Length > 0 Then
                        If txnDate >= Convert.ToDateTime(cRow("FROMDATE").ToString()) And txnDate <= Convert.ToDateTime(cRow("TODATE").ToString()) Then
                            Dim CR_RESTRICTION_REF_ID As String = cRow("CR_RESTRICTION_REF_ID").ToString()
                            Dim CR_RESTRICTION_TYPE As String = cRow("CR_RESTRICTION_TYPE").ToString()
                            If CR_RESTRICTION_REF_ID = RefRecID Then
                                Throw New Exception(Common.PeriodRestrictionError(Convert.ToDateTime(cRow("FROMDATE").ToString()), Convert.ToDateTime(cRow("TODATE").ToString()), cRow("Restriction_Scope").ToString().ToUpper()))
                            End If
                        End If
                    End If
                End If
            Next
            Return True
        End Function

        ''' <summary>
        ''' Returns restricted period for a center
        ''' </summary>
        ''' <param name="CenID"></param>
        ''' <returns></returns>
        Public Function GetRestrictedPeriod(ByVal CenID As Integer, Optional ByVal ID As String = "", Optional ByVal YrStartDate As DateTime = Nothing, Optional ByVal YrEndDate As DateTime = Nothing) As DataTable
            Dim con As SqlConnection = Nothing
            Try
                Dim query As String = "SELECT CR_FROMDATE as FROMDATE, CR_TODATE AS TODATE, CR_RESTRICTION_TYPE, COALESCE(COALESCE(LED_NAME,BA.BA_ACCOUNT_NO),AB.C_NAME) + ' ' + CR_RESTRICTION_TYPE Restriction_Scope,CR_RESTRICTION_REF_ID FROM so_client_restrictions CR LEFT JOIN acc_ledger_info AS LED ON CR.CR_RESTRICTION_REF_ID = LED.LED_ID  LEFT JOIN bank_account_info AS BA ON CR.CR_RESTRICTION_REF_ID = BA.REC_ID  LEFT JOIN address_book AS AB ON  CR.CR_RESTRICTION_REF_ID = AB.REC_ID WHERE CR_CEN_ID = " & CenID.ToString & " AND CR_TYPE = '" & Common.CenterRestrictions.ReadAllWriteBlockedForSomePeriod & "' "
                If ID.Length > 0 Then
                    query += " AND ID = " & ID
                End If
                If IsDate(YrStartDate) And YrStartDate <> DateTime.MinValue Then
                    query += " AND CR_FROMDATE BETWEEN '" & YrStartDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & YrEndDate.ToString(Common.Server_Date_Format_Long) & "' "
                End If
                Dim dsResult As New DataSet()
                If Common.UseSQL Then
                    OpenSqlConnection(con)
                    Dim dAp As SqlDataAdapter
                    dAp = New SqlDataAdapter(query, con)
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    dAp.Fill(dsResult)
                    Close_NonTransaction_SqlConnection(con)
                    'End Using
                Else
                End If
                Return dsResult.Tables(0)
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Function

        Public Function GetAccountSubmittedPeriod(ByVal CenID As Integer, Optional ByVal ID As String = "", Optional ByVal YrStartDate As DateTime = Nothing, Optional ByVal YrEndDate As DateTime = Nothing) As DataTable
            Dim con As SqlConnection = Nothing
            Try
                Dim query As String = "SELECT CR_FROMDATE as FROMDATE, CR_TODATE AS TODATE FROM so_client_restrictions WHERE CR_CEN_ID = " & CenID.ToString & " AND CR_TYPE = '" & Common.CenterRestrictions.ReadAllWriteBlockedAccountsSubmitted & "' "
                If ID.Length > 0 Then
                    query += " AND ID = " & ID
                End If
                If IsDate(YrStartDate) And YrStartDate <> DateTime.MinValue Then
                    query += " AND CR_FROMDATE BETWEEN '" & YrStartDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & YrEndDate.ToString(Common.Server_Date_Format_Long) & "' "
                End If
                Dim dsResult As New DataSet()
                If Common.UseSQL Then

                    OpenSqlConnection(con)
                    Dim dAp As SqlDataAdapter
                    dAp = New SqlDataAdapter(query, con)
                    If _Wrap_ExecuteFunctionDB.IsConnectionOpen = True Then
                        dAp.SelectCommand.Transaction = _Wrap_ExecuteFunctionDB.Transaction
                    End If
                    dAp.Fill(dsResult)
                    Close_NonTransaction_SqlConnection(con)
                    'End Using
                Else
                End If

                Return dsResult.Tables(0)
            Catch ex As Exception
                Close_NonTransaction_SqlConnection(con)
                Throw
            End Try
        End Function

        Private Function CompressData(retData As DataTable) As Byte()
            If retData Is Nothing Then Return Nothing
            Dim data As Byte()
            Dim mem As MemoryStream = New MemoryStream()
            Dim zip As GZipStream = New GZipStream(mem, CompressionMode.Compress)
            Dim retDataset As DataSet = New DataSet()
            retDataset.Tables.Add(retData.Copy)
            retDataset.WriteXml(zip, XmlWriteMode.WriteSchema)
            zip.Close()
            data = mem.ToArray()
            mem.Close()
            Return data
        End Function

        Private Function CompressData(retDataSet As DataSet) As Byte()
            If retDataSet Is Nothing Then Return Nothing
            Dim data As Byte()
            Dim mem As MemoryStream = New MemoryStream()
            Dim zip As GZipStream = New GZipStream(mem, CompressionMode.Compress)
            retDataSet.WriteXml(zip, XmlWriteMode.WriteSchema)
            zip.Close()
            data = mem.ToArray()
            mem.Close()
            Return data
        End Function
#End Region
        'GoogleSheet
        Private Function getServiceAccountCredentials() As ServiceAccountCredential
            Try
                Dim keyFilePath As String = HostingEnvironment.MapPath("~/Scripts/quantum-engine-403705-626a8e4d32a1.json")
                'Dim clientSecretFilePath As String = HostingEnvironment.MapPath("~/Scripts/client_secret_708591934964-
                'tvahpmf4h378rbubolarko8rajhmocsr.apps.googleusercontent.com.json")
                Dim Scopes As String() = {Google.Apis.Sheets.v4.SheetsService.Scope.Spreadsheets, Google.Apis.Drive.v3.DriveService.Scope.Drive}
                Dim ApplicationName As String = "ConnectOneMVC"
                Dim serviceAccountEmail As String = "connectonemvc@quantum-engine-403705.iam.gserviceaccount.com"
                ' Authenticate using your client_secret.json file
                'Create and Validate Credentials
                Dim credential As ServiceAccountCredential
                Using streamReader = New StreamReader(keyFilePath)
                    Dim json As JObject = JObject.Parse(streamReader.ReadToEnd())
                    Dim privateKey As String = json("private_key").ToString()
                    Dim initializer = New ServiceAccountCredential.Initializer(serviceAccountEmail) With {
                    .Scopes = Scopes
                }
                    credential = New ServiceAccountCredential(initializer.FromPrivateKey(privateKey))
                End Using
                Return credential
            Catch ex As Exception
                Throw New Exception("Error getting Service Account Credentials: " & ex.Message, ex)
            End Try

        End Function
        <WebMethod(MessageName:="Export Form responses to Google Sheet")>
        Public Function Export2GoogleSheet(FormInstanceId As String, email As String, overWrite As Boolean, inBasicParam As Basic_Param) As String
            Dim ApplicationName As String = "ConnectOneMVC"
            Dim sheetId As String = Nothing
            Try
                'get Service Account credentials
                Dim credential As ServiceAccountCredential = getServiceAccountCredentials()

                'Get Responses From DB to Export"
                Dim dt As DataTable = get_Form_Responses_Of_Form_Instance(FormInstanceId, inBasicParam)
                If dt.Rows.Count = 0 OrElse dt Is Nothing Then
                    Return "No data to Export"
                End If
                'Fetch whether sheet already exists in DB for respective Form_Instance_Id
                Dim SPName As String = "[sp_Check_If_SheetId_Exists_For_FormId]"
                Dim paramters2() As String = {"@FormInstanceId"}
                Dim values2() As Object = {FormInstanceId}
                Dim dbTypes2() As System.Data.DbType = {DbType.Int32}
                Dim lengths2() As Integer = {11}
                Dim sheetDt As DataTable = ListFromSP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, SPName, Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET.ToString(), paramters2, values2, dbTypes2, lengths2, inBasicParam)
                If sheetDt IsNot Nothing AndAlso sheetDt.Rows.Count > 0 Then
                    sheetId = If(sheetDt.Rows(0)("SheetId") IsNot Nothing, sheetDt.Rows(0)("SheetId").ToString(), Nothing)
                End If
                'Create Google Drive API service"
                Dim driveService = New DriveService(New BaseClientService.Initializer() With {
                    .HttpClientInitializer = credential,
                    .ApplicationName = ApplicationName
                })
                Dim file As Google.Apis.Drive.v3.Data.File = GetOrCreateGoogleSheetFileWithExceptionHandling(driveService, sheetId, email, FormInstanceId, inBasicParam)
                'Create Google Sheets API service.
                Dim sheetsService = New SheetsService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })
                'Export Content to Google Sheet as per various Conditions"                
                Return email + "|" + Export2GoogleSheetbasedOnConditions(file, dt, sheetsService, sheetId, overWrite, FormInstanceId, inBasicParam)
            Catch ex As Exception
                Return "Error exporting to Google Sheet.\n" & ex.Message
            End Try
        End Function
        Protected Function GetSheetsService() As SheetsService
            Dim ApplicationName As String = "ConnectOneMVC"
            'get Service Account credentials
            Dim credential As ServiceAccountCredential = getServiceAccountCredentials()
            'Create Google Drive API service"
            'Dim driveService = New DriveService(New BaseClientService.Initializer() With {
            '    .HttpClientInitializer = credential,
            '    .ApplicationName = ApplicationName
            '})
            Dim sheetsService = New SheetsService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })
            Return sheetsService
        End Function
        <WebMethod(MessageName:="Import Data from Google Sheet")>
        Public Function ImportDataFromGoogleSheet(url As String, email As String, inBasicParam As Basic_Param) As DataTable
            Dim sheetId As String = ExtractSheetId(url)
            Dim SheetTabId As String = ExtractGoogleSheetTabId(url) 'This will extract Tab Id in Google Sheet. Generally mentioned in url as #gid
            Dim sheetName As String = Nothing
            Dim dt As New DataTable()
            Dim service As SheetsService = GetSheetsService()
            Try
                If String.IsNullOrEmpty(sheetId) Then
                    Throw New ArgumentException("Invalid Spreadsheet url!!")
                End If
                If SheetTabId = -1 Then
                    ' Get first sheet's name (default tab)
                    sheetName = GetSheetNameByTabId(sheetId, -1)

                Else
                    sheetName = GetSheetNameByTabId(sheetId, SheetTabId)
                    If String.IsNullOrEmpty(sheetName) Then
                        Throw New Exception("Sheet with specified Tab not found")
                    End If
                End If
                dt.TableName = sheetName
                Dim range As String = sheetName & "!A:B"   'Only Mobile and Message column is required
                Dim request = service.Spreadsheets.Values.Get(sheetId, range)
                Dim response = request.Execute()
                Dim values = response.Values
                Console.WriteLine("Data is fetched via API call")
                If values IsNot Nothing AndAlso values.Count > 0 Then
                    If Not String.Equals(values(0)(0).ToString().Trim(), "Mobile", StringComparison.OrdinalIgnoreCase) OrElse
                        Not String.Equals(values(0)(1).ToString().Trim(), "Message", StringComparison.OrdinalIgnoreCase) Then
                        dt.Columns.Add("Error")
                        dt.Rows.Add("Sheet must have 'Mobile' in A1 and 'Message' in B1 as the column heading in Sheet.")
                        Return dt
                    End If
                    For Each header In values(0)
                        dt.Columns.Add(header.ToString())
                    Next
                    For i As Integer = 1 To values.Count - 1
                        Dim row = dt.NewRow()
                        For j As Integer = 0 To values(i).Count - 1
                            row(j) = values(i)(j).ToString()
                        Next
                        dt.Rows.Add(row)
                    Next
                    Console.WriteLine("Data is fetched via API call success")
                End If
                Return dt

            Catch ex As Exception
                dt.Columns.Add("Error")
                dt.Rows.Add(ex.Message)
                Return dt
            End Try

            Return dt
        End Function
        Public Function GetSheetNameByTabId(spreadsheetId As String, tabId As Integer) As String
            Dim service As SheetsService = GetSheetsService()
            Try
                Dim request As SpreadsheetsResource.GetRequest = service.Spreadsheets.Get(spreadsheetId)
                request.Fields = "sheets(properties)"
                Dim response As Spreadsheet = request.Execute()
                If response.Sheets Is Nothing OrElse response.Sheets.Count = 0 Then
                    Throw New Exception("No sheets found in Google Sheet")
                End If

                ' If tabId is less than 0, return the first sheet's title
                If tabId < 0 Then
                    Return response.Sheets(0).Properties.Title
                End If
                For Each sheet In response.Sheets
                    If sheet.Properties.SheetId = tabId Then
                        Return sheet.Properties.Title
                    End If
                Next
                Return Nothing
            Catch exHttp As System.Net.Http.HttpRequestException
                Throw New Exception("Network or API connection error: " & exHttp.Message)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function ExtractSheetId(googleSheetsUrl As String) As String
            If String.IsNullOrWhiteSpace(googleSheetsUrl) Then Return Nothing
            'Handles …/spreadsheets/d/ID/…, …/d/ID/…, legacy key=ID
            Dim patterns() As String = {
                "spreadsheets/d/([A-Za-z0-9\-_]+)",
                "/d/([A-Za-z0-9\-_]+)",
                "key=([A-Za-z0-9\-_]+)"
            }
            For Each p In patterns
                Dim m As Match = Regex.Match(googleSheetsUrl, p)
                If m.Success Then Return m.Groups(1).Value
            Next
            Return Nothing
        End Function
        Public Function ExtractGoogleSheetTabId(googleSheetsUrl As String) As String 'This will extract Tab Id in Google Sheet. Generally mentioned in url as #gid
            If String.IsNullOrWhiteSpace(googleSheetsUrl) Then Return Nothing
            Dim m As Match = Regex.Match(googleSheetsUrl, "#gid=(\d+)")
            Return If(m.Success, m.Groups(1).Value, "-1")  'default tab = 0
        End Function
        Private Function GetOrCreateGoogleSheetFileWithExceptionHandling(driveService As DriveService, sheetId As String, email As String, FormInstanceId As String, inBasicParam As Basic_Param) As Google.Apis.Drive.v3.Data.File

            'Check If sheet does not exists then create it else get existing Sheet"
            Dim file As Google.Apis.Drive.v3.Data.File = Nothing
            If String.IsNullOrWhiteSpace(sheetId) Then
                Try
                    file = createGoogleSheet(driveService, email)
                Catch ex As Exception
                    Throw ex
                End Try
            Else ' Get the existing Google Sheet.
                Dim request = driveService.Files.Get(sheetId)
                request.Fields = "id, name, mimeType"
                file = request.Execute()
            End If
            Try
                If String.IsNullOrWhiteSpace(email) Then
                    Dim email1 As String = Nothing
                    Dim email2 As String = Nothing
                    'DB call to fetch Event Coordinator's Email
                    Dim paramters1() As String = {"@FormInstanceId"}
                    Dim values1() As Object = {FormInstanceId}
                    Dim dbTypes1() As System.Data.DbType = {DbType.Int32}
                    Dim lengths1() As Integer = {4}
                    Dim dtCoordinatorEmails As DataTable = ListFromSP(Tables.SERVICE_REPORT_INFO, "[sp_get_Event_Coordinator_Email_Export_Google_Sheet]", Tables.SERVICE_REPORT_INFO.ToString(), paramters1, values1, dbTypes1, lengths1, inBasicParam)
                    If (dtCoordinatorEmails IsNot Nothing AndAlso dtCoordinatorEmails.Rows.Count > 0) Then
                        If Not IsDBNull(dtCoordinatorEmails.Rows(0)("EMAIL1")) Then
                            email1 = dtCoordinatorEmails.Rows(0)("EMAIL1").ToString()
                        End If
                        If Not IsDBNull(dtCoordinatorEmails.Rows(0)("EMAIL2")) Then
                            email2 = dtCoordinatorEmails.Rows(0)("EMAIL2").ToString()
                        End If
                    End If
                    'Give file required permissions from provided/program co-ordinator/"
                    If email1.Equals(email2) Then
                        Dim permissionExists As Boolean = CheckPermissionExists(driveService, file.Id, email1)
                        If Not permissionExists Then
                            ' Change the permissions of the new Google Sheet to give specific user(Event Co-ordinator) with write access.
                            Dim permission1 = New Permission() With {
                                .Type = "user",
                                .Role = "writer",
                                .EmailAddress = email1
                            }
                            Try
                                driveService.Permissions.Create(permission1, file.Id).Execute()
                                email = email1
                                Return file
                            Catch ex As Exception
                                Throw New Exception("Error while creating permission for User with Email1: " & email1 & "\n" & ex.Message, ex)
                            End Try
                        End If
                    Else
                        Dim permissionExists As Boolean = CheckPermissionExists(driveService, file.Id, email1)
                        If Not permissionExists Then
                            'Change the permissions of the new Google Sheet to give specific user(Event Co-ordinator) with write access.
                            Dim permission1 = New Permission() With {
                            .Type = "user",
                            .Role = "writer",
                            .EmailAddress = email1
                            }
                            Try
                                driveService.Permissions.Create(permission1, file.Id).Execute()
                                email = email1
                            Catch ex As Exception
                                Throw New Exception("Error while creating permission for User(Event Co-ordinator1) with Email1: " & email1 & "\n" & ex.Message, ex)
                            End Try
                        End If
                        permissionExists = CheckPermissionExists(driveService, file.Id, email2)
                        If Not permissionExists Then
                            ' Change the permissions of the new Google Sheet to give specific user (Event Co-ordinator) with write access.
                            Dim permission2 = New Permission() With {
                            .Type = "user",
                            .Role = "writer",
                            .EmailAddress = email2
                            }
                            Try
                                driveService.Permissions.Create(permission2, file.Id).Execute()
                                email = email1 + ", " + email2
                                Return file
                            Catch ex As Exception
                                Throw New Exception("Error while creating permission for User(Event Co-ordinator2) with Email2: " & email2 & "\n" & ex.Message, ex)
                            End Try
                        End If
                    End If
                Else
                    ' Check if permission already exists
                    Dim permissionExists As Boolean = CheckPermissionExists(driveService, file.Id, email)
                    If Not permissionExists Then
                        Dim permission1 = New Permission() With {
                                .Type = "user",
                                .Role = "writer",
                                .EmailAddress = email
                            }
                        Try
                            driveService.Permissions.Create(permission1, file.Id).Execute()
                            Return file
                        Catch ex As Exception
                            Throw New Exception("Error while creating permission for User(Provided in Textbox) with Email: " & email & "\n" & ex.Message, ex)
                        End Try
                    End If
                    'System.Diagnostics.EventLog.WriteEntry("Application", "Granted Write Permission to " & email & " for file ID: " & file.Id, System.Diagnostics.EventLogEntryType.Information)
                End If

            Catch ex As Exception
                Throw New Exception("Error while creating/checking permission for User with Email: " & email & "\n" & ex.Message, ex)
            End Try
            'Note: Below code was written to give restricted specific permission earlier to Program co-ordinator/Centre incharge etc but it is giving permission each time even if there is permission already given so it is not required at this point of time.
            'If String.IsNullOrWhiteSpace(email) Then
            '    Dim email1 As String = Nothing
            '    Dim email2 As String = Nothing
            '    'DB call to fetch Event Coordinator's Email
            '    Dim paramters1() As String = {"@FormInstanceId"}
            '    Dim values1() As Object = {FormInstanceId}
            '    Dim dbTypes1() As System.Data.DbType = {DbType.Int32}
            '    Dim lengths1() As Integer = {4}
            '    Dim dtCoordinatorEmails As DataTable = ListFromSP(Tables.SERVICE_REPORT_INFO, "[sp_get_Event_Coordinator_Email_Export_Google_Sheet]", Tables.SERVICE_REPORT_INFO.ToString(), paramters1, values1, dbTypes1, lengths1, inBasicParam)
            '    If (dtCoordinatorEmails IsNot Nothing AndAlso dtCoordinatorEmails.Rows.Count > 0) Then
            '        If Not IsDBNull(dtCoordinatorEmails.Rows(0)("EMAIL1")) Then
            '            email1 = dtCoordinatorEmails.Rows(0)("EMAIL1").ToString()
            '        End If
            '        If Not IsDBNull(dtCoordinatorEmails.Rows(0)("EMAIL2")) Then
            '            email2 = dtCoordinatorEmails.Rows(0)("EMAIL2").ToString()
            '        End If
            '    End If
            '    If email1.Equals(email2) Then
            '        ' Change the permissions of the new Google Sheet to give specific user(Event Co-ordinator) with write access.
            '        Dim permission1 = New Permission() With {
            '            .Type = "user",
            '            .Role = "writer",
            '            .EmailAddress = email1
            '        }
            '        Try
            '            driveService.Permissions.Create(permission1, file.Id).Execute()
            '            email = email1
            '            Return file
            '        Catch ex As Exception
            '            Throw New Exception("Error while creating permission for User with Email1: " & email1 & "\n" & ex.Message, ex)
            '        End Try
            '    Else
            '        'Change the permissions of the new Google Sheet to give specific user(Event Co-ordinator) with write access.
            '        Dim permission1 = New Permission() With {
            '            .Type = "user",
            '            .Role = "writer",
            '            .EmailAddress = email1
            '        }
            '        ' Change the permissions of the new Google Sheet to give specific user (Event Co-ordinator) with write access.
            '        Dim permission2 = New Permission() With {
            '            .Type = "user",
            '            .Role = "writer",
            '            .EmailAddress = email2
            '        }
            '        Try
            '            driveService.Permissions.Create(permission1, file.Id).Execute()
            '            email = email2
            '        Catch ex As Exception
            '            Throw New Exception("Error while creating permission for User() with Email2: " & email2 & "\n" & ex.Message, ex)
            '        End Try
            '        Try
            '            driveService.Permissions.Create(permission2, file.Id).Execute()
            '            email = email1 + ", " + email2
            '            Return file
            '        Catch ex As Exception
            '            Throw New Exception("Error while creating permission for User() with Email2: " & email2 & "\n" & ex.Message, ex)
            '        End Try
            '    End If
            'Else
            '    Dim permission1 = New Permission() With {
            '            .Type = "user",
            '            .Role = "writer",
            '            .EmailAddress = email
            '        }
            '    Try
            '        driveService.Permissions.Create(permission1, file.Id).Execute()
            '        Return file
            '    Catch ex As Exception
            '        Throw New Exception("Error while creating permission for User(in Else block) with Email: " & email & "\n" & ex.Message, ex)
            '    End Try
            'End If
            '**Grant "Anyone with the link" write access**
            'If file IsNot Nothing Then
            '    Try
            '        Dim permission As New Google.Apis.Drive.v3.Data.Permission()
            '        permission.Type = "anyone"
            '        permission.Role = "writer" ' Give write access
            '        permission.AllowFileDiscovery = True ' Optional: Set this to False to prevent sheet from being discoverable in Google search.
            '        Dim requestPermission = driveService.Permissions.Create(permission, file.Id)
            '        requestPermission.Execute()
            '    Catch ex As Exception
            '        ' Handle exception, e.g., log error.
            '        ' In some cases, permissions might already be set, or there might be other issues.
            '        ' You can log the exception for debugging.
            '        ' For now, we will continue, assuming the permission setting might have failed but export should proceed.
            '        Throw New Exception( "Error setting 'Anyone with link write' permission: " + ex.Message)
            '        ' Consider logging errorMessage appropriately (e.g., Event Viewer, log file)
            '    End Try
            'End If
        End Function
        Private Function CheckPermissionExists(driveService As Google.Apis.Drive.v3.DriveService, fileId As String, email As String) As Boolean
            Try
                Dim permissions As Google.Apis.Drive.v3.Data.PermissionList = driveService.Permissions.List(fileId).Execute()
                If permissions IsNot Nothing AndAlso permissions.Permissions IsNot Nothing Then
                    For Each permission As Google.Apis.Drive.v3.Data.Permission In permissions.Permissions
                        If permission.Type = "user" AndAlso permission.EmailAddress = email AndAlso permission.Role = "writer" Then
                            Return True ' Permission exists
                        End If
                    Next
                End If
                Return False ' Permission does not exist
            Catch ex As Google.GoogleApiException ' Catch Google API specific exceptions
                ' Handle Google API errors, e.g., insufficient permissions
                System.Diagnostics.EventLog.WriteEntry("Application", "Google API Error checking permissions: " & ex.Message & " - Details: " & ex.ToString, System.Diagnostics.EventLogEntryType.Error)
                Return False
            Catch ex As Exception ' Catch other exceptions
                System.Diagnostics.EventLog.WriteEntry("Application", "Error checking permissions: " & ex.Message, System.Diagnostics.EventLogEntryType.Error)
                Return False ' Handle exceptions appropriately (e.g., log, return false)
            End Try

        End Function
        Private Function Export2GoogleSheetbasedOnConditions(File As Google.Apis.Drive.v3.Data.File, dt As DataTable, SheetsService As SheetsService, sheetId As String, overWrite As Boolean, FormInstanceId As String, inBasicParam As Basic_Param) As String
            Dim url As String = String.Empty
            If String.IsNullOrWhiteSpace(sheetId) Then
                Try
                    url = GoogleSheetExport(File, dt, SheetsService, sheetId, overWrite)
                Catch ex As Exception
                    Throw New Exception("Error while exporting Google sheet for FormInstanceId: " & FormInstanceId & "\n" & ex.Message, ex)
                End Try
                'Fetch Not exported responseIds from DB
                Dim dtResponseIds As DataTable = get_Not_Exported_Form_Responses_To_GoogleSheet(dt, inBasicParam)
                'Insert Pending ResponseIds in Db
                If dtResponseIds IsNot Nothing AndAlso dtResponseIds.Rows.Count > 0 Then
                    Insert_Tbl_Export_Form_Responses_To_GoogleSheet(dtResponseIds, FormInstanceId, File.Id, inBasicParam)
                End If
            Else
                If overWrite Then
                    'Fetch Not exported responseids from DB
                    Dim dtResponseIds As DataTable = get_Not_Exported_Form_Responses_To_GoogleSheet(dt, inBasicParam)
                    Try
                        url = GoogleSheetExport(File, dt, SheetsService, sheetId, overWrite)
                    Catch ex As Exception
                        Throw New Exception("Error while exporting Google sheet for FormInstanceId: " & FormInstanceId & "\n" & ex.Message, ex)
                    End Try
                    'Insert not exported FormInstanceId to DB
                    If dtResponseIds IsNot Nothing AndAlso dtResponseIds.Rows.Count > 0 Then
                        Insert_Tbl_Export_Form_Responses_To_GoogleSheet(dtResponseIds, FormInstanceId, sheetId, inBasicParam)
                    End If
                Else
                    Dim dtResponseIds As DataTable = get_Not_Exported_Form_Responses_To_GoogleSheet(dt, inBasicParam)
                    Dim listResponseIds As List(Of String) = dtResponseIds.AsEnumerable().Select(Function(r) r.Field(Of String)("ResponseId")).ToList()
                    If listResponseIds.Count > 0 Then
                        Dim rowsToInsert As IEnumerable(Of DataRow) = dt.AsEnumerable().Where(Function(r) listResponseIds.Contains(r.Field(Of String)("RESP_ID")))
                        Dim dtPendingResponseIds As DataTable = dt.Clone()
                        If dtResponseIds IsNot Nothing AndAlso dtResponseIds.Rows.Count > 0 Then
                            For Each row As DataRow In rowsToInsert
                                dtPendingResponseIds.ImportRow(row)
                            Next
                            Try
                                url = GoogleSheetExport(File, dt, SheetsService, sheetId, overWrite)
                            Catch ex As Exception
                                Throw New Exception("Error while exporting Google sheet for FormInstanceId: " & FormInstanceId & "\n" & ex.Message, ex)
                            End Try
                            'Insert not exported FormInstanceId to DB
                            Insert_Tbl_Export_Form_Responses_To_GoogleSheet(dtResponseIds, FormInstanceId, sheetId, inBasicParam)
                        End If
                    Else 'When there is no datarow in datatable to append the simply return sheet
                        url = "https://docs.google.com/spreadsheets/d/" + sheetId
                    End If
                End If
            End If
            Return url
        End Function
        Private Function createGoogleSheet(driveService As DriveService, Optional email As String = Nothing) As Google.Apis.Drive.v3.Data.File
            ' Create a new Google Sheet.
            Dim fileMetadata = New Google.Apis.Drive.v3.Data.File() With {
                .Name = "Form Responses",
                .MimeType = "application/vnd.google-apps.spreadsheet"
            }
            Try
                Dim request = driveService.Files.Create(fileMetadata)
                request.Fields = "id"
                Dim file As Google.Apis.Drive.v3.Data.File = request.Execute()
                If (Not String.IsNullOrWhiteSpace(email)) Then
                    'Give Permission
                    Dim permission1 = New Permission() With {
                    .Type = "user",
                    .Role = "writer",
                    .EmailAddress = email
                }
                    Try
                        driveService.Permissions.Create(permission1, file.Id).Execute()
                    Catch ex As Google.GoogleApiException
                        Throw New Exception("Inside createGoogleSheet=>  Error creating permission: " & ex.Message, ex)
                    End Try
                End If
                Return file
            Catch ex As Google.GoogleApiException
                Throw New Exception("createGoogleSheet: " & ex.Message, ex) ' Re-throw the exception to the calling method
            Catch ex As Exception
                Throw ' Re-throw the exception to the calling method
            End Try
        End Function
        <WebMethod(MessageName:="Overwrite Google Sheet of existing responses whose Events are not due yet")>
        Public Function OverwriteFormResponsesOfGoogleSheet() As String
            Dim inBasicParam = New Basic_Param()
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Dim result As String = Nothing
            Dim urls As String = ""
            Dim retryCount As Integer = 0
            Dim maxRetries As Integer = 3
            Dim retryDelay As TimeSpan = TimeSpan.FromMinutes(1)

            Do While retryCount < maxRetries
                Dim msg As String = ""
                Try
                    Dim dtFormInstanceIds As DataTable = ListFromSP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, "[sp_get_FormInstanceId_Of_Events_Not_Due_Export_Google_Sheet]", Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET.ToString(), params, values, dbTypes, lengths, inBasicParam)

                    For Each row As DataRow In dtFormInstanceIds.Rows
                        Dim formInstanceId As String = row("FormInstanceID").ToString()
                        msg += "<a href='https://services.brahmakumaris.com/form/" + formInstanceId + "'>Form Url of Form Instance ID: " & formInstanceId & " </a><br>"
                        result = Export2GoogleSheet(formInstanceId, Nothing, True, inBasicParam) 'Invokes Overwrite functionality for Google Sheets                     
                        Dim token() As String = result.Split("|")
                        msg += " Sheet Url:<br>" & token(token.Length - 1) & "<br><br>"
                        urls += token(token.Length - 1) + " | "
                    Next
                    SendEmailForSheetOverwriteStatus(msg)
                    urls = urls.Substring(0, urls.Length - 1)
                    Return urls ' Success, exit loop
                Catch ex As Exception
                    If ex.Message.Contains("ServiceUnavailable") OrElse ex.Message.Contains("The remote server returned an error: (503) Server Unavailable") OrElse ex.Message.Contains("The remote server returned an error: (500) Internal Server Error") Then ' Check for specific retryable errors
                        retryCount += 1
                        If retryCount < maxRetries Then
                            ' Wait before retrying
                            Thread.Sleep(retryDelay) ' Use Thread.Sleep for web services
                            ' Consider logging the retry attempt
                            'System.Diagnostics.EventLog.WriteEntry("Application", "Google Sheet Overwrite Retry Attempt " & retryCount & " due to " & ex.Message, System.Diagnostics.EventLogEntryType.Warning)

                        Else
                            ' Max retries reached
                            'System.Diagnostics.EventLog.WriteEntry("Application", "Google Sheet Overwrite Failed after " & maxRetries & " retries due to " & ex.Message, System.Diagnostics.EventLogEntryType.Error)
                            msg = msg & " Exception Message: <br>Error: Google Sheet Overwrite Failed after multiple retries:<br>" & ex.Message
                            SendEmailForSheetOverwriteStatus(msg)
                            Return "Error: Google Sheet Overwrite Failed after multiple retries: " & ex.Message ' Or throw the exception if you prefer
                        End If
                    Else
                        'System.Diagnostics.EventLog.WriteEntry("Application", "Google Sheet Overwrite Failed due to " & ex.Message, System.Diagnostics.EventLogEntryType.Error)
                        msg = msg & " Exception Message: <br>Error: Google Sheet Overwrite Failed:<br>" & ex.Message
                        SendEmailForSheetOverwriteStatus(msg)
                        Return "Error: Google Sheet Overwrite Failed: " & ex.Message
                    End If
                End Try
            Loop

        End Function

        Public Function SendEmailForSheetOverwriteStatus(msg As String)
            Dim senderID As String = ConfigurationManager.AppSettings("SenderId")
            Dim senderPassword As String = ConfigurationManager.AppSettings("senderPassword")
            Dim emailSentResult As String = "SUCCESS"
            Dim ToEmailId As String = "saurabh.acct@bkivv.org"
            Try
                Dim smtp As SmtpClient = New SmtpClient With {
                        .Host = "smtp.gmail.com",
                        .Port = 587,
                        .EnableSsl = True,
                        .DeliveryMethod = SmtpDeliveryMethod.Network,
                        .Credentials = New System.Net.NetworkCredential(senderID, senderPassword),
                        .Timeout = 30000
                    }
                Dim message As MailMessage = New MailMessage(senderID, ToEmailId, "GoogleSheet_Update_" + DateAndTime.Now.ToString(), msg)
                message.IsBodyHtml = True
                'If CcEmailId.Length > 0 Then message.CC.Add(CcEmailId)
                'If BccEmailId.Length > 0 Then message.Bcc.Add(BccEmailId)

                smtp.Send(message)
            Catch ex As Exception
                Return "Please contact administrator.Error sending email  to " & ToEmailId & ".!!!" & ex.Message
            End Try
            Return emailSentResult
        End Function

        Public Function Insert_Tbl_Export_Form_Responses_To_GoogleSheet(dtResponseIds As DataTable, FormInstanceId As Int32, sheetId As String, inBasicParam As Basic_Param)
            Dim iEnumOfPendingRespIds As IEnumerable(Of String) = dtResponseIds.AsEnumerable().Select(Function(row) row.Field(Of String)("ResponseId"))
            Dim pendingIds As String = String.Join(",", iEnumOfPendingRespIds)
            If Not String.IsNullOrWhiteSpace(pendingIds) Then
                Dim params4() As String = {"@FormInstanceId", "@SheetId", "@ResponseIds"}
                Dim values4() As Object = {FormInstanceId, sheetId, pendingIds}
                Dim dbTypes4() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String}
                Dim lengths4() As Integer = {11, 120, -1}
                InsertBySP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, "[sp_Insert_Tbl_Export_Form_Responses_To_GoogleSheet]", params4, values4, dbTypes4, lengths4, inBasicParam)
            End If
        End Function
        Public Function get_Not_Exported_Form_Responses_To_GoogleSheet(dt As DataTable, inBasicParam As Basic_Param) As DataTable
            Dim responseIds As IEnumerable(Of String) = dt.AsEnumerable().Select(Function(row) row.Field(Of String)("RESP_ID"))
            Dim ids As String = String.Join(",", responseIds)
            Dim SPName3 As String = "[sp_get_Not_Exported_Form_Responses_To_GoogleSheet]"
            Dim params3() As String = {"@ResponseIds"}
            Dim values3() As Object = {ids}
            Dim dbTypes3() As System.Data.DbType = {DbType.String}
            Dim lengths3() As Integer = {-1}
            Return ListFromSP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, SPName3, Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET.ToString(), params3, values3, dbTypes3, lengths3, inBasicParam)
        End Function
        Public Function get_Form_Responses_Of_Form_Instance(FormInstanceId As Int32, inBasicParam As Basic_Param) As DataTable
            'Get Responses From DB"
            Dim paramters As String() = {"@CHART_INSTANCE_ID", "@RegCenID"}
            Dim values() As Object = {FormInstanceId, Nothing}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return ListFromSP(Tables.SERVICE_CHART_RESPONSES, "[sp_rpt_chartResponses]", Tables.SERVICE_REPORT_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Function GoogleSheetExport(file As Google.Apis.Drive.v3.Data.File, dt As DataTable, sheetsService As SheetsService, sheetId As String, Optional overWrite As Boolean = False) As String
            Try
                ' Define request parameters.
                Dim valueRange As New ValueRange()
                ' Prepare the data from DataTable
                Dim objList As New List(Of IList(Of Object))()

                ' Add column headers and data rows if the sheet is new
                If String.IsNullOrWhiteSpace(sheetId) Or overWrite Then
                    If overWrite Then 'code clears the values and formatting of all cells in the first sheet of the specified Google Spreadsheet
                        ' Get the spreadsheet
                        Dim spreadsheet = sheetsService.Spreadsheets.Get(file.Id).Execute()

                        ' Get the sheet ID
                        Dim sheetIdWithInSpreadsheet As Integer = spreadsheet.Sheets(0).Properties.SheetId
                        Dim clearRequest = sheetsService.Spreadsheets.Values.Clear(New ClearValuesRequest(), file.Id, "Sheet1")
                        clearRequest.Execute()
                        ' Clear the formatting
                        Dim repeatCellRequest = New RepeatCellRequest() With {
                        .Range = New GridRange() With {
                            .SheetId = sheetIdWithInSpreadsheet,
                            .StartRowIndex = 0,
                            .EndRowIndex = Int32.MaxValue,
                            .StartColumnIndex = 0,
                            .EndColumnIndex = Int32.MaxValue
                        },
                        .Cell = New CellData() With {
                            .UserEnteredFormat = New CellFormat()
                        },
                        .Fields = "userEnteredFormat"
                        }
                        Dim request = New Google.Apis.Sheets.v4.Data.Request() With {
                            .RepeatCell = repeatCellRequest
                        }
                        Dim batchUpdateSpreadsheetRequest = New BatchUpdateSpreadsheetRequest() With {
                            .Requests = New List(Of Google.Apis.Sheets.v4.Data.Request)() From {request}
                        }
                        sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, file.Id).Execute()
                    End If
                    Dim headerRow As New List(Of Object)()
                    For Each column As DataColumn In dt.Columns
                        headerRow.Add(column.ColumnName)
                    Next
                    objList.Add(headerRow)

                    For Each dr As DataRow In dt.Rows
                        Dim objArr As New List(Of Object)()
                        For Each column As DataColumn In dt.Columns
                            Dim value As Object = dr(column)

                            ' Check if the column is of type DateTime
                            If TypeOf value Is DateTime Then
                                Dim dateValue As DateTime = CType(value, DateTime)
                                ' Check if the time part is 00:00:00
                                If dateValue.TimeOfDay = TimeSpan.Zero Then
                                    objArr.Add(dateValue.ToString("MM-dd-yyyy"))
                                Else
                                    objArr.Add(dateValue.ToString("MM-dd-yyyy HH:mm:ss"))
                                End If
                            ElseIf TypeOf value Is Date Then
                                ' If the value is a Date, convert to the desired format "MM-dd-yyyy"
                                objArr.Add(CType(value, Date).ToString("MM-dd-yyyy"))
                            Else
                                ' For all other types, just add the value directly
                                objArr.Add(value)
                            End If
                        Next
                        objList.Add(objArr)
                    Next
                    ' Append the data to the end of the sheet
                    valueRange.Values = objList
                    ' Write the data to the Google Sheet
                    Dim update = sheetsService.Spreadsheets.Values.Update(valueRange, file.Id, "A1")
                    update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
                    Dim response = update.Execute()
                Else ' Add only data rows if the sheet is existing
                    For Each dr As DataRow In dt.Rows
                        Dim objArr As New List(Of Object)()
                        For Each column As DataColumn In dt.Columns
                            Dim value As Object = dr(column)
                            ' Check if the column is of type DateTime
                            If TypeOf value Is DateTime Then
                                Dim dateValue As DateTime = CType(value, DateTime)
                                ' Check if the time part is 00:00:00
                                If dateValue.TimeOfDay = TimeSpan.Zero Then
                                    objArr.Add(dateValue.ToString("MM-dd-yyyy"))
                                Else
                                    objArr.Add(dateValue.ToString("MM-dd-yyyy HH:mm:ss"))
                                End If
                            ElseIf TypeOf value Is Date Then
                                ' If the value is a Date, convert to the desired format "MM-dd-yyyy"
                                objArr.Add(CType(value, Date).ToString("MM-dd-yyyy"))
                            Else
                                ' For all other types, just add the value directly
                                objArr.Add(value)
                            End If
                        Next
                        objList.Add(objArr)
                    Next
                    ' Append the data to the end of the sheet
                    valueRange.Values = objList
                    ' Get all the data from the sheet
                    Dim range As String = "Sheet1"
                    Dim getRequest As SpreadsheetsResource.ValuesResource.GetRequest = sheetsService.Spreadsheets.Values.Get(file.Id, range)
                    Dim getResponse As ValueRange = getRequest.Execute()
                    ' Find the last row that contains data
                    Dim lastRow As Integer = getResponse.Values.Count
                    ' Specify the range starting from the row after the last row that contains data
                    Dim appendRange As String = "Sheet1!A" & (lastRow + 1).ToString()
                    Dim appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, file.Id, appendRange)
                    appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.OVERWRITE

                    appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
                    Dim response = appendRequest.Execute()
                End If

                ' Freeze the top row
                Dim gridProperties = New GridProperties() With {
                    .FrozenRowCount = 1
                }
                ' Assuming you're working with the first sheet
                Dim sheetProperties = New SheetProperties() With {
                    .SheetId = 0,
                    .GridProperties = gridProperties
                }
                Dim req = New Google.Apis.Sheets.v4.Data.Request() With {
                    .UpdateSheetProperties = New UpdateSheetPropertiesRequest() With {
                        .Properties = sheetProperties,
                        .Fields = "gridProperties.frozenRowCount"
                    }
                }
                Dim batchUpdateRequest = New BatchUpdateSpreadsheetRequest() With {
                    .Requests = New List(Of Google.Apis.Sheets.v4.Data.Request)() From {
                        req
                    }
                }
                sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, file.Id).Execute()

                'Dim columnCount As Integer = dt.Columns.Count
                'Dim range As String = "A1:" & ChrW(65 + columnCount - 1) & "1" ' 65 is the ASCII value for 'A'

                ' Format the header row<== Below code has bug. Make change to add styling
                'FormatHeaderRowAsync(file.Id, 0, range, keyFilePath, serviceAccountEmail)
                ' Return the URL of the Google Sheet
                Return "https://docs.google.com/spreadsheets/d/" + file.Id
            Catch ex As Exception
                'Return "Sheet:" + sheetId + "::" + ex.Message
                Throw New Exception("Sheet:" + sheetId + "::" + ex.Message)
            End Try
        End Function
        <WebMethod(MessageName:="Export responses to Google Sheet")>
        Public Function ListPreviewData2GoogleSheet(dt As DataTable, email As String) As String
            Dim ApplicationName As String = "ConnectOneMVC"
            'get Service Account credentials
            Dim credential As ServiceAccountCredential = getServiceAccountCredentials()
            'Create Google Drive API service"
            Dim driveService = New DriveService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })
            'File created with required Permission to access it prvided EmailId
            Dim file As Google.Apis.Drive.v3.Data.File = createGoogleSheet(driveService, email)
            Dim sheetsService = New SheetsService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })
            Return email + "|" + GoogleSheetExport(file, dt, sheetsService, Nothing) 'SheetId will always be Empty and OverWrite is false(by default) so new Sheet will be created everytime and responses will be exported their
        End Function
        Public Async Function FormatHeaderRowAsync(ByVal spreadsheetId As String, ByVal sheetId As Integer, ByVal range As String, ByVal keyFilePath As String, ByVal serviceAccountEmail As String) As Task
            ' Prepare the JSON payload for the request

            ' Use fully qualified names for Google Sheets API classes
            Dim payload As New Dictionary(Of String, Object) From {
                {"requests", New List(Of Dictionary(Of String, Object)) From {
                    New Dictionary(Of String, Object) From {
                        {"repeatCell", New Dictionary(Of String, Object) From {
                            {"range", New Dictionary(Of String, Object) From {
                                {"sheetId", sheetId},
                                {"startRowIndex", 0},
                                {"endRowIndex", 1},
                                {"startColumnIndex", 0},
                                {"endColumnIndex", range.Length}
                            }},
                            {"cell", New Dictionary(Of String, Object) From {
                                {"userEnteredFormat", New Dictionary(Of String, Object) From {
                                    {"backgroundColor", New Dictionary(Of String, Double) From {
                                        {"red", 1.0},
                                        {"green", 1.0},
                                        {"blue", 0.0}
                                    }},
                                    {"textFormat", New Dictionary(Of String, Boolean) From {
                                        {"bold", True}
                                    }}
                                }}
                            }},
                            {"fields", "userEnteredFormat(backgroundColor,textFormat)"}
                        }}
                    }
                }}
            }

            ' Convert the payload to JSON
            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)

            ' Prepare the HTTP request
            Dim requestUri As String = "https://sheets.googleapis.com/v4/spreadsheets/" & spreadsheetId & ":batchUpdate"

            ' Create a WebRequest instance
            Dim webRequest As WebRequest = WebRequest.Create(requestUri)

            ' Set the method to POST
            webRequest.Method = "POST"

            ' Add the authorization header to the request
            Dim credential As ServiceAccountCredential
            Using streamReader = New StreamReader(keyFilePath)
                Dim json As JObject = JObject.Parse(streamReader.ReadToEnd())
                Dim privateKey As String = json("private_key").ToString()

                Dim initializer = New ServiceAccountCredential.Initializer(serviceAccountEmail) With {
            .Scopes = {SheetsService.Scope.Spreadsheets}
        }
                credential = New ServiceAccountCredential(initializer.FromPrivateKey(privateKey))
            End Using
            Dim accessToken As String = Await credential.GetAccessTokenForRequestAsync()
            webRequest.Headers.Add("Authorization", "Bearer " & accessToken)

            ' Prepare the data to be posted
            Dim postData As String = jsonPayload
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

            ' Set the ContentType property of the WebRequest
            webRequest.ContentType = "application/json"

            ' Set the ContentLength property of the WebRequest
            webRequest.ContentLength = byteArray.Length

            ' Get the request stream
            Dim dataStream As Stream = webRequest.GetRequestStream()

            ' Write the data to the request stream
            dataStream.Write(byteArray, 0, byteArray.Length)

            ' Close the Stream object
            dataStream.Close()

            ' Get the response
            Dim response As WebResponse = webRequest.GetResponse()

            ' Display the status
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)

            ' Clean up the streams
            dataStream.Close()
            response.Close()
        End Function
        'Reminder
        <WebMethod>
        Public Function GetReminders() As DataTable
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return ListFromSP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, "[Get_Reminder_Transaction]", Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET.ToString(), params, values, dbTypes, lengths, New Basic_Param())
            'Dim request = New JObject()
            'request.Add("Rec_Id", 1)
            'request.Add("Text", "Your FD is Expiring Tomorrow")
            'request.Add("Image_Path", "")
            'request.Add("Button1_Text", "OK")
            'request.Add("Button2_Text", "Ignore")
            'Return JsonConvert.SerializeObject(request)
        End Function
        <WebMethod>
        Public Function PostReminderResult(RecID As Int32, ButtonText As String) As Boolean
            Dim params() As String = {"@RecID", "@Response"}
            Dim values() As Object = {RecID, ButtonText}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 512}
            UpdateBySP(Tables.TBL_EXPORT_FORM_RESPONSES_TO_GOOGLESHEET, "[Update_Reminder_Transaction]", params, values, dbTypes, lengths, New Basic_Param())
            Return True
        End Function
    End Class
    ' Helper class for sheet information
    <Serializable>
    Public Class SheetInfo
        Public Property SheetId As Integer
        Public Property Title As String
        Public Property RowCount As Integer
        Public Property ColumnCount As Integer
    End Class
    <Serializable>
    Public Class Wrap_ExecuteFunctionDB

        Public Con As New SqlConnection
        Public Trans_Sql As SqlTransaction
        Private _ConnectionString As String = ""

        Private NullDBValue As Object = Convert.DBNull

        Friend Property ConnectionString() As String
            Get
                Return _ConnectionString
            End Get
            Set(ByVal Value As String)
                _ConnectionString = Value
            End Set
        End Property

        Friend ReadOnly Property Transaction() As SqlTransaction
            Get
                Return Trans_Sql
            End Get
        End Property

        Friend ReadOnly Property Connection() As SqlConnection
            Get
                Return Me.Con
            End Get
        End Property

        Friend Sub OpenConnection()
            Con.ConnectionString = _ConnectionString
            Con.Open()
            Trans_Sql = Con.BeginTransaction()
        End Sub

        Friend Sub CreateConnection()
            Con = New SqlConnection(Common.ConnectionString)
        End Sub

        Friend Sub CloseConnection()
            Con.Close()
            Con.Dispose()
        End Sub

        Friend Function CommitTransaction() As Boolean
            Try
                Trans_Sql.Commit()
                Return True
            Catch oE As SqlException
                Return False
            Catch oE2 As Exception
                Return False
            Finally
                If Not Trans_Sql Is Nothing Then
                    If Not Con.State = ConnectionState.Closed Then
                        Con.Close()
                    End If
                    Trans_Sql = Nothing
                End If
            End Try
        End Function

        Friend Sub RollBackTrans()
            If Not Trans_Sql Is Nothing Then
                Trans_Sql.Rollback()
                Trans_Sql = Nothing
            End If

            If Not Con Is Nothing Then
                If Not Con.State = ConnectionState.Closed Then
                    Con.Close()
                End If
            End If
        End Sub

        Friend Function IsConnectionOpen() As Boolean
            If (Con.State = ConnectionState.Open) Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

