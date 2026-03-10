'SQL
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Data.OleDb
Imports System.Net
Imports System.Text
Imports System.Security.Cryptography
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Helpers
Imports System.IO
Imports System.Reflection
Imports System.Configuration
Imports System.Diagnostics
<Serializable>
Public Class Common
    Dim _wd As String = "Your_Password_Goes_Here"
    Dim _Salt As Byte() = New Byte() {&H45, &HF1, &H61, &H6E, &H20, &H0, &H65, &H64, &H76, &H65, &H64, &H3, &H76}
#Region "Global Variables"

    Public _Current_Version As String = Val(My.Application.Info.Version.Major) & "." & Val(My.Application.Info.Version.Minor) & "." & Val(My.Application.Info.Version.Build) & "." & Val(My.Application.Info.Version.Revision)
    Public _Date_Default As Date = New Date(2011, 1, 18)

    Public _PC_ID As String = ""

    Public _Sync_Send_Version_Main As String = My.Application.Info.Version.ToString
    Public _Sync_Last_DateTime As DateTime

    Public Start_TC_Time As String = "" : Public Show_TC_Image As Integer = 2
    Public _SyncProcessName As String = "ConnectOne.E0002"
    Public _SyncProcessFile As String = "ConnectOne.E0002.exe"

    Public _HTTP_Server As String = ""
    Public _HTTP_Server2 As String = ""
    Public _HTTP_Server3 As String = ""

    Public _ShivService_Location_Suffix As String = "shivservice/service.asmx"
    Public _RealTime_Location_Suffix As String = "service.asmx"

    Public _RealTime_Server As String = ""
    Public _RealTime_Server2 As String = ""
    Public _RealTime_Server3 As String = ""

    Public _UPDATE_Server As String = ""
    Public _LOG_Server As String = ""
    Public _Sync_Allow As String = "YES"

    Public _App_path As String = IIf(Application.StartupPath.EndsWith("\"), Application.StartupPath, Application.StartupPath & "\")
    Public _db_path As String = ""

    Public _dbname_Core As String = ""
    Public _dbname_Sys As String = ""
    Public _dbname_Data As String = ""
    Public _dbname_Template As String = ""

    Public _data_ConStr_Core As String = ""
    Public _data_ConStr_Sys As String = ""
    Public _data_ConStr_Data As String = ""
    Public _data_ConStr_Template As String = ""

    Public _SessionDictionary As New Dictionary(Of String, Object) 'base session dictionary
    Public _RightsDictionary As New Dictionary(Of String, Boolean) 'base rights dictionary

    Public _List_Of_FullData_Screen As New List(Of String)

    Public _open_Cen_ID As Int32
    Public _open_Cen_ID_Main As Int32
    Public _open_Cen_ID_Child As String
    Public _open_Cen_Rec_ID As String
    Public _open_Cen_Name As String
    Public _open_PAD_No_Main As String
    Public _open_PAD_No As String
    Public _open_Ins_ID As String
    Public _open_Ins_Name As String
    Public _open_Ins_Short As String
    Public _open_UID_No As String
    Public _open_Zone_ID As String
    Public _open_Zone_SUB_ID As String
    ' Public _open_Account_Type As String
    Public _open_Year_ID As Int32 'Example: 1010
    Public _open_Year_Name As String 'Example: 2010 - 2011
    Public _open_Year_Sdt As Date
    Public _open_Year_Edt As Date
    Public _open_Trans_DB As String
    Public _open_Year_Acc_Type As String
    Public _open_Event_ID As Integer = 0

    'Public _curr_Db_Conn_Mode As DbConnectionMode
    Public _User_Type As String = ""
    Public _open_User_Type As String = ""
    Public _open_User_ID As String
    Public _open_User_Password As String = ""
    Public _open_User_Remember As Boolean = False
    Public _open_ClientUser As String
    Public _prev_Unaudited_YearID As Int32
    Public _next_Unaudited_YearID As Int32
    Public _Completed_Year_Count As Integer = 0
    Public _ReportsToBePrinted As String = ""
    Public _open_User_Is_Central_Store As Boolean
    Public _open_User_User_Is_Admin As Boolean
    Public _open_User_PersonnelID As Integer?
    Public _open_User_MainDeptID As Integer?
    Public _open_User_SubDeptID As Integer?
    Public _open_User_Self_Posted As Boolean
    Public Refresh_Notes_List As Boolean
    Public Refresh_Cash_Book As Boolean
    Public Refresh_Murli As Boolean = False
    Public Refresh_Swaman As Boolean = False
    Public Force_Logout As Boolean = False
    Public Swaman As String = "ceQ Deelcee ntB~"

    Public Const Date_Transit_Format As String = "yyyy-MM-dd"

    Public fi As DateTimeFormatInfo = New DateTimeFormatInfo() 'New CultureInfo("en-US", False).DateTimeFormat
    Public _Time_Format_AM_PM As String = "hh:mm tt"
    Public _Date_Format_Short As String = "MM-dd-yyyy"
    Public _Date_Format_Long As String = "MM-dd-yyyy HH:mm:ss"
    Public _Server_Date_Format_Long As String = "yyyy-MM-dd HH:mm:ss"
    Public _Server_Date_Format_Short As String = "yyyy-MM-dd"
    Public _Date_Format_DD_MMM_YYYY As String = "dd-MMM-yyyy"
    Public _Date_Format_Current As String = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
    Public BackUpFilePrefix As String = "bkp_"
    Public _allowInfoLogs As Boolean = False

    Public Allow_Foreign_Donation As Boolean
    Public Allow_Bank_In_C_Box As Boolean
    Public Allow_Membership As Boolean
    Public Allow_Statements_Without_Restrictions As Boolean
    Public Is_HQ_Centre As Boolean
    Public Allow_Magazine As Boolean

    'User Preferences
    Public _prefer_open_verification_windows_on_login As Boolean = False
    Public _prefer_show_acc_party_only As Boolean? = Nothing
    Public _prefer_show_vouching_indicator As Boolean = True
    Public _prefer_show_attachment_indicator As Boolean = True

    Public _test_Mode As Boolean = False
    'Public _main_form As System.Windows.Forms.Form = Nothing

    Public Cached_Data As Dictionary(Of String, DataTable)
    ' Public xMessageWindow As Common_Lib.Prompt_Window = New Common_Lib.Prompt_Window
    Public _IsVolumeCenter As Boolean = False
    Public _IsUnderAudit As Boolean = False

#Region "DBOperations' Variables"
    Public _telephoneDBOps As DbOperations.Telephones
    Public _AssetLocDBOps As DbOperations.AssetLocations
    Public _AndroidDBOps As DbOperations.Android_Notifications
    Public _BankAccountDBOps As DbOperations.BankAccounts
    Public _CenterDBOps As DbOperations.Center
    Public _ChartDBOps As DbOperations.Chart
    Public _ClientUserDBOps As DbOperations.ClientUserInfo
    Public _MurliDBOps As DbOperations.Murli
    Public _VehicleDBOps As DbOperations.Vehicles
    Public _AssetDBOps As DbOperations.Assets
    Public _CODDBOps As DbOperations.CodInfo
    Public _ConsumableStockDBOps As DbOperations.ConsumableStock
    Public _GoldSilverDBOps As DbOperations.GoldSilver
    Public _LiveStockDBOps As DbOperations.LiveStock
    Public _AdvanceDBOps As DbOperations.Advances
    Public _CashDBOps As DbOperations.Cash
    Public _CoreDBOps As DbOperations.Core
    Public _CenterPurposeDBOps As DbOperations.Center_Purpose_Info
    Public _DepositsDBOps As DbOperations.Deposits
    Public _DepositSlipsDBOps As DbOperations.DepositSlips
    Public _LiabilityDBOps As DbOperations.Liabilities
    Public _ServPlacesDBOps As DbOperations.ServicePlaces
    Public _FDDBOps As DbOperations.FD
    Public _L_B_DBOps As DbOperations.LandAndBuilding
    Public _Address_DBOps As DbOperations.Addresses
    Public _SSOuls_DBOps As DbOperations.ServiceableSouls
    Public _Students_DBOps As DbOperations.Students
    Public _Report_DBOps As DbOperations.Report
    Public _Chang_Password_DBOps As DbOperations.ChangePassword
    Public _Reset_Password_DBOps As DbOperations.ResetPassword
    Public _Password_DBOps As DbOperations.Password
    Public _Maintain_DBOps As DbOperations.Maintenance
    Public _Docs_DBOps As DbOperations.DocLibrary
    Public _News_DBOps As DbOperations.News
    Public _Req_DBOps As DbOperations.Request
    Public _Letters_DBOps As DbOperations.Letters
    Public _Notes_DBOps As DbOperations.Notes
    Public _SR_DBOps As DbOperations.ServiceReport
    Public _SerProj_DBOps As DbOperations.ServiceProject
    Public _Schedule_DBOps As DbOperations.Schedule
    Public _Chart_DBOps As DbOperations.ServiceChart
    Public _SM_DBOps As DbOperations.ServiceModule
    Public _SMGS_DBOps As DbOperations.SM_GodlyServices
    Public _Reminder_DBOps As DbOperations.Reminders
    Public _DonationRegister_DBOps As DbOperations.DonationRegister
    Public _NoteBook_DBOps As DbOperations.NoteBook
    Public _CollectionBox_DBOps As DbOperations.Voucher_CollectionBox
    Public _Voucher_DBOps As DbOperations.Vouchers
    Public _b2b_DBOps As DbOperations.Voucher_BankToBank
    Public _Cash_Bank_DBOps As DbOperations.Voucher_CashBank
    Public _Donation_DBOps As DbOperations.Voucher_Donation
    Public _Gift_DBOps As DbOperations.Voucher_Gift
    Public _Rect_DBOps As DbOperations.Voucher_Receipt
    Public _Payment_DBOps As DbOperations.Voucher_Payment
    Public _FD_Voucher_DBOps As DbOperations.Voucher_FD
    Public _L_B_Voucher_DBOps As DbOperations.Voucher_Property
    Public _Internal_Tf_Voucher_DBOps As DbOperations.Voucher_Internal_Transfer
    Public _SaleOfAsset_DBOps As DbOperations.Voucher_SaleOfAsset
    Public _Reports_Items_DBOps As DbOperations.Report_ItemList
    Public _Reports_Common_DBOps As DbOperations.Reports_All
    Public _Reports_Ledgers_DBOps As DbOperations.Report_Ledgers
    Public _Action_Items_DBOps As DbOperations.Action_Items
    Public _Audit_DBOps As DbOperations.Audit
    Public _Notifications_DBOps As DbOperations.Notifications
    Public _DataRestriction_DBOps As DbOperations.DataRestriction
    Public _UserPreferences_DBOps As DbOperations.UserPreferences
    Public _Membership_DBOps As DbOperations.Membership
    Public _Membership_Voucher_DBOps As DbOperations.Voucher_Membership
    Public _Membership_Renewal_Voucher_DBOps As DbOperations.Voucher_Membership_Renewal
    Public _Membership_Conversion_Voucher_DBOps As DbOperations.Voucher_Membership_Conversion
    Public _Membership_Receipt_Register_DBOps As DbOperations.Membership_Receipt_Register
    Public _Journal_voucher_DBOps As DbOperations.Voucher_Journal
    Public _OpeningBalances_DBOps As DbOperations.OpeningBalances
    Public _AssetTransfer_DBOps As DbOperations.Voucher_AssetTransfer
    Public _Complexes_DBOps As DbOperations.Complexes
    Public _WIP_Finalization_DBOps As DbOperations.WIP_Finalization
    Public _WIPDBOps As DbOperations.WIP_Profile
    Public _WIPCretionVouchers As DbOperations.WIP_Creation_Vouchers
    Public _Magazine_DBOps As DbOperations.Magazine
    Public _Magazine_Requests_DBOps As DbOperations.Magazine_Membership_Requests
    Public _Magazine_Reports As DbOperations.Magazine_Reports
    Public _Voucher_Magazine_DBOps As DbOperations.Voucher_Magazine
    Public _TDS_DBOps As DbOperations.TDS
    Public _Attachments_DBOps As DbOperations.Attachments
    Public _Stock_Profile_DBOps As DbOperations.StockProfile
    Public _Sub_Item_DBOps As DbOperations.SubItems
    Public _Transfer_Orders_DBOps As DbOperations.StockTransferOrders
    Public _user_order_DBOps As DbOperations.StockUserOrder
    Public _RR_DBOps As DbOperations.StockRequisitionRequest
    Public _PO_DBOps As DbOperations.StockPurchaseOrder
    Public _Stock_Production_DBOps As DbOperations.StockProduction
    Public _Stock_MachineTools_DBOps As DbOperations.StockMachineToolAllocation
    Public _Projects_Dbops As DbOperations.Projects
    Public _Jobs_Dbops As DbOperations.Jobs
    Public _Personnels_Dbops As DbOperations.Personnels
    Public _StockDeptStores_dbops As DbOperations.StockDeptStores
    Public _StockApprovalReqd_dbops As DbOperations.StockApprovalRequired
    Public _StockSupplier_dbops As DbOperations.Suppliers
    Public _ServiceMaterial_dbops As DbOperations.GodlyServiceMaterial
    Public _HelpVideos_dbops As DbOperations.HelpVideos

    Public _Form_dbops As DbOperations.Forms
    'Client Authorization 
    Public _Auth_Rights_Listing As DataTable
    Public _Menu_vibilities_Listing As DataTable
    Public _Dynamic_Menu_Listing As DataTable
#End Region

    Public Enum Navigation_Mode
        _New = 1
        _Edit = 2
        _Delete = 3
        _Close = 4 '--> Close Bank / Audit Query
        _Re_Open = 5 '--> Reopen Audit Query
        _View = 6
        _Manage = 7
        '7 (free)
        '8 (free)
        _New_From_Selection = 9 '--> For Selective Item Based, Voucher Open
    End Enum
    '
    ' Summary:
    '     Specifies identifiers to indicate the return value of a dialog box.
    ' Do not add or delete any value from the existing Enum. Always add at the bottom so that serial number remains intact.
    Public Enum DialogResult
        '
        ' Summary:
        '     Nothing is returned from the dialog box. This means that the modal dialog continues
        '     running.
        None = 0
        '
        ' Summary:
        '     The dialog box return value is OK (usually sent from a button labeled OK).
        OK = 1
        '
        ' Summary:
        '     The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        Cancel = 2
        '
        ' Summary:
        '     The dialog box return value is Abort (usually sent from a button labeled Abort).
        Abort = 3
        '
        ' Summary:
        '     The dialog box return value is Retry (usually sent from a button labeled Retry).
        Retry = 4
        '
        ' Summary:
        '     The dialog box return value is Ignore (usually sent from a button labeled Ignore).
        Ignore = 5
        '
        ' Summary:
        '     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes = 6
        '
        ' Summary:
        '     The dialog box return value is No (usually sent from a button labeled No).
        No = 7
    End Enum
    Public Enum User_Authorization
        Add = 1
        Update = 2
        Delete = 3
        View = 4
        Export = 5
        Report = 6
        List = 7
        Full = 8
    End Enum
    Public Enum Record_Status
        _Deleted = -1
        _Incomplete = 0
        _Completed = 1
        _Locked = 2 'Audited/Freezed
    End Enum

    Public Enum Bank_Trans_Type
        FD
        SAVING
    End Enum

    Public Enum Service_Places_Type
        PATHSHALA
        [CLASS]
        CONNECTING_CENTRE
    End Enum

    Public Enum DbConnectionMode
        Online
        Local
        TxnsOnline_LocalCore_BackedByOnlineCore
    End Enum

    'Return codes For Sync Process
    Public Enum SyncReturnCodes
        Success
        NetworkFailure
        TimeOut
        SyncFailure
        InvalidWebServicePath
        WrongClientDBVersion
        UnAuthenticatedClientLocation
        WrongClientAppVersion
        SyncAlreadyRunning
        InValidPCID
    End Enum

    Public Enum ClientAction
        Manage_Remarks
        View_Remarks
        Special_Groupings
        Lock_Unlock
        Close_Reopen_Actions
        Add_Edit_Action
        View_Actions
        Add_Data
        Edit_Data
        View_Menu
        Check_Uncheck
    End Enum

    Public Enum Voucher_Screen_Code
        Cash_Deposit_Withdrawn = 1
        Bank_To_Bank_Transfer = 2
        Payment = 3
        Receipt = 4
        Donation_Regular = 5
        Donation_Foreign = 6
        Donation_Gift = 7
        Internal_Transfer = 8
        Collection_Box = 9
        Fixed_Deposits = 10
        Sale_Asset = 11
        Membership = 12
        Membership_Renewal = 13
        Journal = 14
        Asset_Transfer = 15
        Membership_Conversion = 16
        WIP_Finalization = 17
        Magazine_New = 18
        Magazine_Renew = 19
        Magazine_Payment = 20
    End Enum
    <Serializable>
    Public Class ClientUserType
        Public Const Auditor As String = "Auditor"
        Public Const SuperUser As String = "SuperUser"
    End Class

    Public Enum ClientDBFolderCode
        SYS
        CORE
        DATA
    End Enum

    Public Enum FDAction
        New_FD
        Renew_FD
        Close_FD
    End Enum

    Public Enum FDStatus
        New_FD
        Matured_Renewed_FD
        Premature_Renewed_FD
        Matured_Closed_FD
        Premature_Closed_FD
    End Enum



#End Region

#Region "Global Functions & Procedures"
    'Returns whether SQL is to be used, Added Temporarily for refernce and shall be removed as soon as DBOps will be shifter to RealService
    Public Shared Function UseSQL() As Boolean
        Return True
    End Function

    ' Private WithEvents xPreviewForm As DevExpress.XtraPrinting.Preview.PrintPreviewRibbonFormEx
    'Private Sub xPreviewForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles xPreviewForm.KeyDown
    '    If e.KeyCode = Keys.Escape Then xPreviewForm.Close()
    'End Sub

    'Using Dev-Express Reports to Preview
    'Public Sub Show_ReportPreview(ByVal xReport As DevExpress.XtraReports.UI.XtraReport, ByVal xTitle As String, ByVal xWinFrom As DevExpress.XtraEditors.XtraForm, ByVal xZoomToPageWidth As Boolean)
    '    Dim xPCL As New DevExpress.XtraReports.UI.ReportPrintTool(xReport)
    '    xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None)
    '    xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Background, DevExpress.XtraPrinting.CommandVisibility.None)

    '    xPCL.PreviewForm.Text = xTitle : xPCL.PrintingSystem.Document.Name = xTitle

    '    Dim xPleaseWait As New Common_Lib.PleaseWait
    '    xPreviewForm = xPCL.PreviewRibbonForm
    '    xPreviewForm.Text = xTitle
    '    xPreviewForm.KeyPreview = True
    '    xPreviewForm.Icon = xPleaseWait.Icon
    '    xPreviewForm.ShowInTaskbar = True : xPreviewForm.ShowIcon = True
    '    xPreviewForm.Ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Above
    '    xPreviewForm.Ribbon.GetGroupByName("Document").Visible = False
    '    xPreviewForm.Ribbon.GetGroupByName("Print").AddGroupToToolbar()
    '    xPreviewForm.Ribbon.GetGroupByName("Export").AddGroupToToolbar()
    '    For Each x As DevExpress.XtraBars.BarItemLink In xPreviewForm.Ribbon.GetGroupByName("Export").ItemLinks
    '        If x.Item.Caption.Contains("Close") Then xPreviewForm.Ribbon.Toolbar.ItemLinks.Add(x.Item)
    '    Next

    '    xPreviewForm.Ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False
    '    xPreviewForm.Ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True
    '    xPreviewForm.Ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show
    '    xPreviewForm.Ribbon.ShowToolbarCustomizeItem = False
    '    xPreviewForm.Ribbon.ShowCategoryInCaption = False
    '    ' 
    '    If xZoomToPageWidth Then xPCL.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth)
    '    xPCL.PrintingSystem.ExecCommand(PrintingSystemCommand.HandTool, New Object() {True})
    '    xPreviewForm.Ribbon.Pages(0).Text = "Options" : xPreviewForm.Ribbon.Minimized = True

    '    Try
    '        xPCL.ShowRibbonPreviewDialog(xWinFrom.LookAndFeel)
    '    Catch ex As Exception
    '        DevExpress.XtraEditors.XtraMessageBox.Show("Error Message: " & ex.Message.ToString, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    '    xPleaseWait.Dispose()
    '    xPCL.Dispose()
    'End Sub

    'Using Dev-Express Grid to Preview 
    'Public Sub Show_ListPreview(ByVal xGrid As Object, ByVal xTitle As String, ByVal xWinFrom As DevExpress.XtraEditors.XtraForm, ByVal xLandScape As Boolean, ByVal xPaperKind As System.Drawing.Printing.PaperKind, ByVal xTopLeft_Header As String, ByVal xTopCentre_Header As String, ByVal xTopRight_Header As String, ByVal xZoomToPageWidth As Boolean, Optional xSetTopMargin As Integer = 50, Optional xSetBottomMargin As Integer = 40, Optional xSetHeaderFontSize As Integer = 14, Optional xBottomLeft_Footer As String = "", Optional xBottomCentre_Footer As String = "", Optional xBottomRight_Footer As String = "")
    '    If xBottomLeft_Footer = "" Then xBottomLeft_Footer = "Page [Page # of Pages #]"
    '    If xBottomRight_Footer = "" Then xBottomRight_Footer = "Print On: [Date Printed], [Time Printed]"

    '    Dim xPCL As New PrintableComponentLink(New PrintingSystem())
    '    xPCL.Component = xGrid
    '    xPCL.Landscape = xLandScape : xPCL.PaperKind = xPaperKind : xPCL.Margins = New System.Drawing.Printing.Margins(40, 40, xSetTopMargin, xSetBottomMargin)
    '    xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None)
    '    xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground, DevExpress.XtraPrinting.CommandVisibility.None)
    '    'xPCL.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Background, DevExpress.XtraPrinting.CommandVisibility.None)

    '    xPCL.PrintingSystem.PreviewFormEx.Text = xTitle : xPCL.PrintingSystem.Document.Name = xTitle

    '    'xPCL.PrintingSystem.Watermark.Text = "Sample"
    '    'xPCL.PrintingSystem.Watermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.Horizontal
    '    'xPCL.PrintingSystem.Watermark.TextTransparency = 190
    '    'xPCL.PrintingSystem.Watermark.ForeColor = Color.RoyalBlue
    '    'xPCL.PrintingSystem.Watermark.Font = New System.Drawing.Font("ARIAL BLACK", 80, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    'xPCL.PrintingSystem.Watermark.ShowBehind = False

    '    Dim headerArea As DevExpress.XtraPrinting.PageHeaderArea = New DevExpress.XtraPrinting.PageHeaderArea
    '    headerArea.Font = New Font("Arial", xSetHeaderFontSize, FontStyle.Regular) : headerArea.LineAlignment = BrickAlignment.None
    '    headerArea.Content.Add(xTopLeft_Header) : headerArea.Content.Add(xTopCentre_Header) : headerArea.Content.Add(xTopRight_Header)

    '    Dim footerArea As DevExpress.XtraPrinting.PageFooterArea = New DevExpress.XtraPrinting.PageFooterArea
    '    footerArea.Font = New Font("Verdana", 8, FontStyle.Regular) : footerArea.LineAlignment = BrickAlignment.Far
    '    footerArea.Content.Add(xBottomLeft_Footer) : footerArea.Content.Add(xBottomCentre_Footer) : footerArea.Content.Add(xBottomRight_Footer)

    '    Dim HeaderFooter As DevExpress.XtraPrinting.PageHeaderFooter = New DevExpress.XtraPrinting.PageHeaderFooter(headerArea, footerArea)
    '    xPCL.PageHeaderFooter = HeaderFooter

    '    Dim xPleaseWait As New Common_Lib.PleaseWait
    '    xPreviewForm = xPCL.PrintingSystem.PreviewRibbonFormEx
    '    xPreviewForm.Text = xTitle
    '    xPreviewForm.KeyPreview = True
    '    xPreviewForm.Icon = xPleaseWait.Icon
    '    xPreviewForm.ShowInTaskbar = True : xPreviewForm.ShowIcon = True
    '    xPreviewForm.Ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Above
    '    xPreviewForm.Ribbon.GetGroupByName("Document").Visible = False
    '    xPreviewForm.Ribbon.GetGroupByName("Print").AddGroupToToolbar()
    '    xPreviewForm.Ribbon.GetGroupByName("Export").AddGroupToToolbar()
    '    For Each x As DevExpress.XtraBars.BarItemLink In xPreviewForm.Ribbon.GetGroupByName("Export").ItemLinks
    '        If x.Item.Caption.Contains("Close") Then xPreviewForm.Ribbon.Toolbar.ItemLinks.Add(x.Item)
    '    Next

    '    xPreviewForm.Ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False
    '    xPreviewForm.Ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True
    '    xPreviewForm.Ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show
    '    xPreviewForm.Ribbon.ShowToolbarCustomizeItem = False
    '    xPreviewForm.Ribbon.ShowCategoryInCaption = False
    '    xPreviewForm.Ribbon.Pages(0).Text = "Options" : xPreviewForm.Ribbon.Minimized = True

    '    xPCL.CreateDocument()
    '    If xZoomToPageWidth Then xPCL.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth)
    '    xPCL.PrintingSystem.ExecCommand(PrintingSystemCommand.HandTool, New Object() {True})

    '    Try
    '        xPCL.ShowRibbonPreviewDialog(xWinFrom.LookAndFeel)
    '    Catch ex As Exception
    '        DevExpress.XtraEditors.XtraMessageBox.Show("Error Message: " & ex.Message.ToString, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    '    xPleaseWait.Dispose()
    '    xPCL.Dispose()
    'End Sub

    'OFFLINE FUNCTION
    Public Function Rec_Detail(ByVal DataRef As String) As String
        Dim StatusStr As String = ""
        Dim SqlStr As String = ""
        StatusStr = "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=99,'Incorrect'  ,  " &
                                     "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=-1,'Deleted'    ,  " &
                                     "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 0,'Incomplete',  " &
                                     "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 1,'Completed'  ,  " &
                                     "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 2,'Locked'    ,'')))))"
        SqlStr = DataRef & ".REC_ADD_BY as [Add By]," & DataRef & ".REC_ADD_ON as [Add Date]," &
                               DataRef & ".REC_EDIT_BY as [Edit By]," & DataRef & ".REC_EDIT_ON as [Edit Date]," &
                               StatusStr & " as [Action Status]," & DataRef & ".REC_STATUS_BY as [Action By]," & DataRef & ".REC_STATUS_ON as [Action Date]"
        Return SqlStr
    End Function

    Public Function Rec_Detail(ByVal DataRef As String, ByVal conMode As Common.DbConnectionMode)
        Dim StatusStr As String = ""
        Dim SqlStr As String = ""
        If conMode = DbConnectionMode.Local Then
            StatusStr = "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=99,'Incorrect'  ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=-1,'Deleted'    ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 0,'Incomplete',  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 1,'Completed'  ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 2,'Locked'    ,'')))))"
            SqlStr = DataRef & ".REC_ADD_BY as [Add By]," & DataRef & ".REC_ADD_ON as [Add Date]," &
                                   DataRef & ".REC_EDIT_BY as [Edit By]," & DataRef & ".REC_EDIT_ON as [Edit Date]," &
                                   StatusStr & " as [Action Status]," & DataRef & ".REC_STATUS_BY as [Action By]," & DataRef & ".REC_STATUS_ON as [Action Date]"
        ElseIf conMode = DbConnectionMode.Online Or conMode = DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore Then
            If Common.UseSQL() Then
                StatusStr = "CASE CASE WHEN " & DataRef & ".REC_STATUS IS NULL THEN 99 ELSE " & DataRef & ".REC_STATUS END " &
                                           "WHEN 99 THEN 'Incorrect'  " &
                                           "WHEN -1 THEN 'Deleted' " &
                                           "WHEN 0 THEN 'Incomplete' " &
                                           "WHEN 1 THEN 'Completed'  " &
                                           "WHEN 2 THEN 'Locked' End"
                SqlStr = DataRef & ".REC_ADD_BY as 'Add By'," & DataRef & ".REC_ADD_ON as 'Add Date'," &
                                       DataRef & ".REC_EDIT_BY as 'Edit By'," & DataRef & ".REC_EDIT_ON as 'Edit Date'," &
                                      StatusStr & " as 'Action Status'," & DataRef & ".REC_STATUS_BY as 'Action By'," & DataRef & ".REC_STATUS_ON as 'Action Date'"
            Else
                StatusStr = "CASE IF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)" &
                                          "WHEN 99 THEN 'Incorrect'  " &
                                          "WHEN -1 THEN 'Deleted' " &
                                          "WHEN 0 THEN 'Incomplete' " &
                                          "WHEN 1 THEN 'Completed'  " &
                                          "WHEN 2 THEN 'Locked' End"
                SqlStr = DataRef & ".REC_ADD_BY as 'Add By'," & DataRef & ".REC_ADD_ON as 'Add Date'," &
                                       DataRef & ".REC_EDIT_BY as 'Edit By'," & DataRef & ".REC_EDIT_ON as 'Edit Date'," &
                                      StatusStr & " as 'Action Status'," & DataRef & ".REC_STATUS_BY as 'Action By'," & DataRef & ".REC_STATUS_ON as 'Action Date'"
            End If
        End If
        Return SqlStr
    End Function

    'OFFLINE FUNCTION
    Public Function Rec_Detail_OrgField(ByVal DataRef As String)
        Dim StatusStr As String = "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=99,'Incorrect'  ,  " &
                                  "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=-1,'Deleted'    ,  " &
                                  "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 0,'Incomplete',  " &
                                  "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 1,'Completed'  ,  " &
                                  "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 2,'Locked'    ,'')))))"
        Dim SqlStr As String = DataRef & ".REC_ADD_BY ," & DataRef & ".REC_ADD_ON ," &
                               DataRef & ".REC_EDIT_BY ," & DataRef & ".REC_EDIT_ON ," &
                               StatusStr & " as ACTION_STATUS," & DataRef & ".REC_STATUS_BY ," & DataRef & ".REC_STATUS_ON "
        Return SqlStr
    End Function

    Public Function Rec_Detail_OrgField(ByVal DataRef As String, ByVal conMode As Common.DbConnectionMode)
        Dim StatusStr As String = ""
        Dim SqlStr As String = ""
        If conMode = DbConnectionMode.Local Then
            StatusStr = "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=99,'Incorrect'  ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)=-1,'Deleted'    ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 0,'Incomplete',  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 1,'Completed'  ,  " &
                                      "IIF(IIF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)= 2,'Locked'    ,'')))))"
            SqlStr = DataRef & ".REC_ADD_BY ," & DataRef & ".REC_ADD_ON ," &
                     DataRef & ".REC_EDIT_BY ," & DataRef & ".REC_EDIT_ON ," &
                     StatusStr & " as ACTION_STATUS," & DataRef & ".REC_STATUS_BY ," & DataRef & ".REC_STATUS_ON "
        ElseIf conMode = DbConnectionMode.Online Or conMode = DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore Then
            If Common.UseSQL() Then
                StatusStr = "CASE CASE WHEN " & DataRef & ".REC_STATUS IS NULL THEN 99 ELSE " & DataRef & ".REC_STATUS END " &
                            "WHEN 99 THEN 'Incorrect'  " &
                            "WHEN -1 THEN 'Deleted' " &
                            "WHEN 0 THEN 'Incomplete' " &
                            "WHEN 1 THEN 'Completed'  " &
                            "WHEN 2 THEN 'Locked' End"
                SqlStr = DataRef & ".REC_ADD_BY ," & DataRef & ".REC_ADD_ON ," &
                         DataRef & ".REC_EDIT_BY ," & DataRef & ".REC_EDIT_ON ," &
                         StatusStr & " as ACTION_STATUS," & DataRef & ".REC_STATUS_BY," & DataRef & ".REC_STATUS_ON"
            Else
                StatusStr = "CASE IF(ISNULL(" & DataRef & ".REC_STATUS),99," & DataRef & ".REC_STATUS)" &
                           "WHEN 99 THEN 'Incorrect'  " &
                           "WHEN -1 THEN 'Deleted' " &
                           "WHEN 0 THEN 'Incomplete' " &
                           "WHEN 1 THEN 'Completed'  " &
                           "WHEN 2 THEN 'Locked' End"
                SqlStr = DataRef & ".REC_ADD_BY ," & DataRef & ".REC_ADD_ON ," &
                         DataRef & ".REC_EDIT_BY ," & DataRef & ".REC_EDIT_ON ," &
                         StatusStr & " as ACTION_STATUS," & DataRef & ".REC_STATUS_BY," & DataRef & ".REC_STATUS_ON"
            End If
        End If
        Return SqlStr
    End Function

    ''' <summary>
    ''' PROVIDES REMARKCOUNT AND STATUS FOR ACTION ITEMS REGISTERED TO BE COMPLETED IN CURRENT AUDIT ONLY 'OFFLINE FUNCTION
    ''' </summary>
    ''' <param name="DataRef"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Remarks_Detail(ByVal DataRef As String, ByVal IsOnline As Boolean, ByVal CurrDateTime As DateTime) As String
        Dim SqlStr As String = ""
        If IsOnline Then
            If Common.UseSQL() Then
                SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                        "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                        "(SELECT TOP 1 COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as OpenActions, " &
                        "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
            Else
                SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                    "(SELECT COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as RemarkStatus, " &
                    "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as OpenActions, " &
                    "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
            End If
        Else
            SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                    "(SELECT TOP 1 IIF(ISNULL(UPPER(AI_STATUS)),'',UPPER(AI_STATUS)) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                    "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as OpenActions, " &
                    "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND AI_REF_REC_ID= " & DataRef & ".REC_ID AND AI_DUE_ON < '" & CurrDateTime.ToString(_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
        End If
        Return SqlStr
    End Function

    ''' <summary>
    ''' PROVIDES REMARKCOUNT AND STATUS FOR ACTION ITEMS REGISTERED TO BE COMPLETED IN CURRENT AUDIT ONLY , for Transactions 'OFFLINE FUNCTION
    ''' </summary>
    ''' <param name="DataRef"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Remarks_Detail_Txn(ByVal DataRef As String, ByVal IsOnline As Boolean, ByVal CurrDateTime As DateTime) As String
        Dim SqlStr As String = ""
        If IsOnline Then
            If Common.UseSQL() Then
                SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID)) AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                        "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                        "(SELECT TOP 1 COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as OpenActions, " &
                        "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= MAX(" & DataRef & ".REC_ID) OR AI_REF_REC_ID= MAX(" & DataRef & ".TR_M_ID))  AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
            Else
                SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID) AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                       "(SELECT COALESCE(UPPER(AI_STATUS),'') FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as RemarkStatus, " &
                       "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as OpenActions, " &
                       "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND AI_DUE_ON < '" & CurrDateTime.ToString(_Server_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE'AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
            End If
        Else
            SqlStr = "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID)  AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT') as RemarkCount, " &
                    "(SELECT TOP 1 IIF(ISNULL(UPPER(AI_STATUS)),'',UPPER(AI_STATUS)) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID) AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as RemarkStatus, " &
                    "(SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID) AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED') LIMIT 1) as OpenActions, " &
                    "(SELECT COUNT(REC_ID) FROM action_item_info WHERE UPPER(AI_REF_TABLE)='" & DataRef & "' AND (AI_REF_REC_ID= " & DataRef & ".REC_ID OR AI_REF_REC_ID= " & DataRef & ".TR_M_ID) AND AI_DUE_ON < '" & CurrDateTime.ToString(_Date_Format_Short) & "' AND UPPER(AI_DUE_TYPE) ='SPECIFIC DATE' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')) as CrossedTimeLimit "
        End If
        Return SqlStr
    End Function

    Public Function CheckActionRights(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, ByVal Action As ClientAction) As Boolean
        'Super User Has all rights 
        If _open_User_Type.ToUpper = ClientUserType.SuperUser.ToUpper Then
            Return True
        End If
        'Checking Rights for a Normal Auditor
        Select Case Action
            Case ClientAction.View_Remarks
                Return True
            Case ClientAction.Manage_Remarks, ClientAction.Special_Groupings, ClientAction.Lock_Unlock, ClientAction.Close_Reopen_Actions, ClientAction.Add_Edit_Action, ClientAction.View_Actions, ClientAction.Edit_Data, ClientAction.Add_Data, ClientAction.View_Menu
                If _open_User_Type.ToUpper = ClientUserType.Auditor.ToUpper Then
                    Return True
                End If
            Case Else
                Return False
        End Select
        Return False
    End Function

    Public Sub HandleRealTimeServiceErrors(ByVal ex As Exception, ByVal SessionTimeOutError As String)
        If ex.Message.Contains("Unable to connect to the remote server") Then
            ' Dim wait As PleaseWait = New PleaseWait
            DevExpress.XtraEditors.XtraMessageBox.Show(Messages.CouldNotContactServer, "Could not Contact Server!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If ex.Message.Contains("|") Then
                If ex.Message.ToLower.Contains("Sorry! Your Session has been terminated".ToLower) Then
                    'Add Code to Exit Center from here ...
                    Me.Force_Logout = True
                End If
                Throw New Exception(ex.Message.Split("|")(1))
                'DevExpress.XtraEditors.XtraMessageBox.Show(, "Alert!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                Throw New Exception(ex.Message)
                'DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "An error has occoured!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"

        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)

        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Public Function IsEmail(ByVal Email As String) As Boolean
        Dim strRegex As String = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" & "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" & ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
        Dim re As New Regex(strRegex)
        If re.IsMatch(Email) Then
            Return (True)
        Else
            Return (False)
        End If
    End Function

    Public Function IsUrl(ByVal Url As String) As Boolean
        'user@
        ' IP- 199.194.52.184
        ' allows either IP or domain
        ' tertiary domain(s)- www.
        ' second level domain
        ' first level domain- .com or .museum
        ' port number- :80
        ' a slash isn't required if there is no file name
        Dim strRegex As String = "^(https?://)" & "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" & "(([0-9]{1,3}\.){3}[0-9]{1,3}" & "|" & "([0-9a-z_!~*'()-]+\.)*" & "([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]\." & "[a-z]{2,6})" & "(:[0-9]{1,4})?" & "((/?)|" & "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$"
        Dim re As New Regex(strRegex)

        If re.IsMatch(Url) Then
            Return (True)
        Else
            Return (False)
        End If
    End Function

    Public Function GetDayNumberWithSuffix(ByVal DayofMonth As Integer) As String
        Select Case DayofMonth
            Case 1, 21, 31, 41, 51, 61, 71, 81, 91, 101, 121
                Return DayofMonth & "st"
            Case 2, 22, 32, 42, 52, 62, 72, 82, 92, 102, 122
                Return DayofMonth & "nd"
            Case 3, 23, 33, 43, 53, 63, 73, 83, 93, 103, 123
                Return DayofMonth & "rd"
            Case Else
                Return DayofMonth & "th"
        End Select
    End Function

    Public Function Single_Qoates(ByVal M_String As String) As String
        Try
            Dim L As Integer = 0
            Dim S As String = ""
            L = Len(M_String)
            For i As Integer = 1 To L
                If Mid(M_String, i, 1) = "'" Then
                    S = S & "`"
                ElseIf Mid(M_String, i, 1) = "[" Then
                    S = S & "("
                ElseIf Mid(M_String, i, 1) = "]" Then
                    S = S & ")"
                Else
                    S = S & Mid(M_String, i, 1)
                End If
            Next
            Return S
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            Return ""
        End Try
    End Function

    Public Function Get_Uptime() As String
        Dim iDays As Long
        Dim iHour As Long
        Dim iMinute As Long
        Dim iSecond As Long
        Dim iMSecond As Long
        Dim TICK As Long = Environment.TickCount

        iMSecond = TICK Mod 1000
        TICK = TICK \ 1000
        iDays = ((TICK) \ (24 * (60 ^ 2)))

        If iDays > 0 Then TICK = (TICK - 24 * (60 ^ 2)) * iDays
        iHour = TICK \ (60 ^ 2)

        If iHour > 0 Then TICK = TICK - ((60 ^ 2) * iHour)
        iMinute = TICK \ 60
        iSecond = TICK Mod 60

        Dim xtime As String = "PC Uptime : " &
                        Format(iHour, "00") & ":" &
                        Format(iMinute, "00") & ":" &
                        Format(iSecond, "00") & ":" &
                        Format(iMSecond, "000")  'Format(iDays, "0") & " days, " 

        Return xtime
    End Function

    Public Function IsConnectionAvailable(ByVal url As String) As Boolean
        'Return True
        'Request for request
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim myWebRequest As HttpWebRequest = CType(req, HttpWebRequest)
        'myWebRequest.Accept = "text/xml"
        'myWebRequest.Method = "POST"
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Log.Write(Log.LogType.Important, "Common", "IsConnectionAvailableForSync:attemp1", ex.Message, Log.LogSuffix.ClientApplication)
            Return False
        End Try
    End Function

    Public Function IsConnectionAvailableForSync() As Boolean
        'Return True
        'Call url
        Dim url As New System.Uri(_HTTP_Server)
        'Request for request
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim myWebRequest As HttpWebRequest = CType(req, HttpWebRequest)
        'myWebRequest.Accept = "text/xml"
        'myWebRequest.Method = "POST"
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Log.Write(Log.LogType.Important, "Common", "IsConnectionAvailableForSync:attemp1", ex.Message, Log.LogSuffix.ClientApplication)
        End Try

        'Try Server 2
        url = New System.Uri(_HTTP_Server2)
        'Request for request
        req = System.Net.WebRequest.Create(url)
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Log.Write(Log.LogType.Important, "Common", "IsConnectionAvailableForSync", ex.Message, Log.LogSuffix.ClientApplication)
        End Try

        'Try Server 3
        url = New System.Uri(_HTTP_Server3)
        'Request for request
        req = System.Net.WebRequest.Create(url)
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Log.Write(Log.LogType.Important, "Common", "IsConnectionAvailableForSync", ex.Message, Log.LogSuffix.ClientApplication)
            Return False
        End Try
    End Function

    Public Function IsConnectionAvailableForUpdate() As Boolean
        'Call url
        Dim url As New System.Uri(_UPDATE_Server)
        'Request for request
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Return False
        End Try
    End Function

    Public Function GotInternet() As Boolean
        Dim req As System.Net.HttpWebRequest
        Dim res As System.Net.HttpWebResponse
        GotInternet = False
        Try
            req = CType(System.Net.HttpWebRequest.Create(_HTTP_Server), System.Net.HttpWebRequest)
            res = CType(req.GetResponse(), System.Net.HttpWebResponse)
            req.Abort()
            If res.StatusCode = System.Net.HttpStatusCode.OK Then
                GotInternet = True
            End If
        Catch weberrt As System.Net.WebException
            GotInternet = False
        Catch except As Exception
            GotInternet = False
        End Try
    End Function

    Public Function ROUND_NUM(ByVal Xnum As Double) As Double
        If Xnum < 0 Then
            Return System.Math.Ceiling(Val(Xnum) - 0.5)
        Else
            Return System.Math.Floor(Val(Xnum) + 0.5)
        End If
    End Function

    Public Function CallSync(ByVal path As String, ByVal WebServicePath As String, ByVal WebServicePath2 As String, ByVal WebServicePath3 As String, ByVal CurrAppVersion As String, ByVal currLogSuffix As String, ByVal CurrDateTime As DateTime) As SyncReturnCodes
        Log.Write(Log.LogType.Important, "common", "callSync", "file-" & _SyncProcessName, Log.LogSuffix.ClientApplication)
        If Process.GetProcessesByName(_SyncProcessFile).Length > 0 Then
            Return SyncReturnCodes.SyncAlreadyRunning
        End If
        Dim strCmdLine As String
        Log.Write(Log.LogType.Important, "common", "callSync", path + ":" + WebServicePath, Log.LogSuffix.ClientApplication)
        strCmdLine = path + " " + WebServicePath + " " + WebServicePath2 + " " + WebServicePath3 + " " + CurrAppVersion + " " + currLogSuffix + " " + CurrDateTime.ToString()
        Log.Write(Log.LogType.Important, "common", "callSync, path:", strCmdLine, Log.LogSuffix.ClientApplication)
        Dim outResult As SyncReturnCodes = vbNull
        Dim syncProcess As System.Diagnostics.Process
        Try
            syncProcess = New System.Diagnostics.Process
            syncProcess.StartInfo.FileName = _SyncProcessFile
            syncProcess.StartInfo.Arguments = strCmdLine
            syncProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            'syncProcess.StartInfo.UseShellExecute = False
            syncProcess.Start()
            'Wait until the process passes back an exit code
            syncProcess.WaitForExit()
            'Free resources associated with this process
            Dim ReturnCode As Int32 = syncProcess.ExitCode
            syncProcess.Close()

            'If (Not Int32.TryParse(output, ReturnCode)) Then
            '    Log.Write(Log.LogType.Error, "common", "CallSilent", "Invalid Return from sync:" & output, currLogSuffix)
            '    Return SyncReturnCodes.SyncFailure
            'End If

            Select Case ReturnCode
                Case CType(SyncReturnCodes.InvalidWebServicePath, Int32)
                    outResult = SyncReturnCodes.InvalidWebServicePath
                Case CType(SyncReturnCodes.NetworkFailure, Int32)
                    outResult = SyncReturnCodes.NetworkFailure
                Case CType(SyncReturnCodes.Success, Int32)
                    outResult = SyncReturnCodes.Success
                Case CType(SyncReturnCodes.SyncFailure, Int32)
                    outResult = SyncReturnCodes.SyncFailure
                Case CType(SyncReturnCodes.TimeOut, Int32)
                    outResult = SyncReturnCodes.TimeOut
                Case CType(SyncReturnCodes.UnAuthenticatedClientLocation, Int32)
                    outResult = SyncReturnCodes.UnAuthenticatedClientLocation
                Case CType(SyncReturnCodes.WrongClientAppVersion, Int32)
                    outResult = SyncReturnCodes.WrongClientAppVersion
                Case CType(SyncReturnCodes.WrongClientDBVersion, Int32)
                    outResult = SyncReturnCodes.WrongClientDBVersion
                Case CType(SyncReturnCodes.InValidPCID, Int32)
                    outResult = SyncReturnCodes.InValidPCID
            End Select
            Return outResult

        Catch ex As Exception
            Log.Write(Log.LogType.Error, "common", "callSync", ex.InnerException.ToString(), Log.LogSuffix.ClientApplication)
            Throw ex
        End Try

    End Function

    Public Function ConvertNumToAlphaValue(ByVal Input As Decimal) As String 'Call this function passing the number you desire to be changed
        Return changeToWords(Input.ToString)
        'Dim output As String = Nothing
        'If Input < 1000 Then
        '    output = FindNumber(Input) 'if its less than 1000 then just look it up
        'Else
        '    Dim nparts() As String 'used to break the number up into 3 digit parts
        '    Dim n As String = Input 'string version of the number
        '    Dim i As Long = Input.ToString.Length 'length of the string to help break it up

        '    Do Until i - 3 <= 0
        '        n = n.Insert(i - 3, ",") 'insert commas to use as splitters
        '        i = i - 3 'this insures that we get the correct number of parts
        '    Loop
        '    nparts = n.Split(",") 'split the string into the array

        '    i = Input.ToString.Length 'return i to initial value for reuse
        '    Dim p As Integer = 0 'p for parts, used for finding correct suffix
        '    For Each s As String In nparts
        '        Dim x As Long = CLng(s) 'x is used to compare the part value to other values
        '        p = p + 1
        '        If p = nparts.Length Then 'if p = number of elements in the array then we need to do something different
        '            If x <> 0 Then
        '                If CLng(s) < 100 Then
        '                    output = output & " And " & FindNumber(CLng(s)) ' look up the number, no suffix 
        '                Else                                                ' required as this is the last part
        '                    output = output & " " & FindNumber(CLng(s))
        '                End If
        '            End If
        '        Else 'if its not the last element in the array
        '            If x <> 0 Then
        '                If output = Nothing Then 'we have to check this so we don't add a leading space
        '                    output = output & FindNumber(CLng(s)) & " " & FindSuffix(i, CLng(s)) 'look up the number and suffix
        '                Else 'spaces must go in the right place
        '                    output = output & " " & FindNumber(CLng(s)) & " " & FindSuffix(i, CLng(s)) 'look up the snumber and suffix
        '                End If
        '            End If
        '        End If
        '        i = i - 3 'reduce the suffix counter by 3 to step down to the next suffix
        '    Next
        'End If
        'Return output & " only."
    End Function

    Private Function changeToWords(numb As [String], Optional isCurrency As Boolean = True) As [String]

        Dim val As [String] = "", wholeNo As [String] = numb, points As [String] = "", andStr As [String] = "", pointStr As [String] = ""
        Dim endStr As [String] = If((isCurrency), ("Only"), (""))

        Dim decimalPlace As Integer = numb.IndexOf(".")
        ' Check Point "." in string ,If yes return vlaue otherwise return -1,Here return -1
        If decimalPlace > 0 Then
            ' -1 > 0 ,Below statement not check
            wholeNo = numb.Substring(0, decimalPlace)
            points = numb.Substring(decimalPlace + 1)
        End If

        'Substring value (-1+1=0) ,Here points return 123456789   
        If points <> "" Then
            If Convert.ToInt32(points) > 0 Then
                'Convert point into int32 ,123456789 >0 ,True
                andStr = If((isCurrency), ("and"), ("Rupees"))
                'isCurrency = False , Go to on point
                '  endStr = If((isCurrency), ("Cents " & endStr), (""))
                'endstr = "";

                If points <> "" Then
                    pointStr = translateCents(points)
                    If points = "01" Then
                        pointStr = pointStr & " Paisa"
                    Else
                        pointStr = pointStr & " Paise"
                    End If
                End If
            End If
        End If
        If (wholeNo.Length().Equals(1)) And Convert.ToInt64(wholeNo).Equals(1) Then
            val = [String].Format("Rupee {0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), "", pointStr, endStr)
        ElseIf (wholeNo.Length().Equals(1)) And Convert.ToInt64(wholeNo).Equals(0) Then
            val = "--"
        ElseIf (Convert.ToInt64(wholeNo).Equals(0)) Then
            val = [String].Format("{0}{1}{2} {3}", translateWholeNumber(wholeNo).Trim(), "", pointStr, endStr)
        Else
            If pointStr <> "" Then
                val = [String].Format("Rupees {0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr)
            Else
                val = [String].Format("Rupees {0}{1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, "", endStr)
            End If

        End If


        Return val
    End Function
    Private Function translateWholeNumber(number As [String]) As [String]
        Dim word As String = ""
        Try
            Dim beginsZero As Boolean = False
            Dim isDone As Boolean = False
            Dim dblAmt As Double = (Convert.ToDouble(number))
            'Convert string number to double dblAmt     
            If dblAmt > 0 Then
                'Here check dblAmt > 0    
                beginsZero = number.StartsWith("0")
                ' Check number start with Zero = False   
                Dim numDigits As Integer = number.Length
                'Lengh find in nimDigit   
                Dim pos As Integer = 0
                Dim place As [String] = ""
                Select Case numDigits
                    Case 1
                        'ones' range 
                        word = ones(number)
                        isDone = True
                        Exit Select
                    Case 2
                        'tens' range 
                        word = tens(number)
                        isDone = True
                        Exit Select
                    Case 3
                        'hundreds' range 
                        pos = (numDigits Mod 3) + 1
                        place = " Hundred "
                        Exit Select
                        'thousands' range 
                    Case 4, 5
                        pos = (numDigits Mod 4) + 1

                        place = " Thousand "
                        Exit Select
                        'Lakhs' range 
                    Case 6, 7
                        pos = (numDigits Mod 6) + 1

                        place = " Lakh "
                        Exit Select
                        'Crores' range 
                    Case 8, 9
                        pos = (numDigits Mod 8) + 1
                        'Lengh 9 % 8 = 1
                        place = " Crore "
                        Exit Select
                        'Arabs range 
                    Case 10, 11

                        pos = (numDigits Mod 10) + 1
                        place = " Arab "
                        Exit Select
                    Case Else
                        'add extra case options for anything above Billion... 
                        isDone = True
                        Exit Select
                End Select
                If Not isDone Then
                    'if transalation is not done, continue...
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos))
                    'check for trailing zeros 
                    If beginsZero Then
                        word = " and " & word.Trim()
                    End If
                End If
                'ignore digit grouping names 
                If word.Trim().Equals(place.Trim()) Then
                    word = ""
                End If
            End If
            Dim Result As [String] = word.Trim()
            Result = Result.Replace("and Hundred", "")
            Result = Result.Replace("and Thousand", "")
            Result = Result.Replace("and Lakh", "")
            Result = Result.Replace("and Crore", "")
            Result = Result.Replace(" and ", " ")
            word = Result
        Catch


        End Try
        Return word.Trim()
    End Function
    Private Function tens(digit As [String]) As [String]
        Dim digt As Integer = Convert.ToInt32(digit)
        ' Digit string to int32,Check cases
        Dim name As [String] = Nothing

        Select Case digt
            Case 10
                name = "Ten"
                Exit Select
            Case 11
                name = "Eleven"
                Exit Select
            Case 12
                name = "Twelve"
                Exit Select
            Case 13
                name = "Thirteen"
                Exit Select
            Case 14
                name = "Fourteen"
                Exit Select
            Case 15
                name = "Fifteen"
                Exit Select
            Case 16
                name = "Sixteen"
                Exit Select
            Case 17
                name = "Seventeen"
                Exit Select
            Case 18
                name = "Eighteen"
                Exit Select
            Case 19
                name = "Nineteen"
                Exit Select
            Case 20
                name = "Twenty"
                Exit Select
            Case 30
                name = "Thirty"
                Exit Select
            Case 40
                name = "Forty"
                Exit Select
            Case 50
                name = "Fifty"
                Exit Select
            Case 60
                name = "Sixty"
                Exit Select
            Case 70
                name = "Seventy"
                Exit Select
            Case 80
                name = "Eighty"
                Exit Select
            Case 90
                name = "Ninety"
                Exit Select
            Case Else
                If digt > 0 Then
                    name = Convert.ToString(tens(digit.Substring(0, 1) & "0")) & " " & Convert.ToString(ones(digit.Substring(1)))
                End If
                Exit Select
        End Select
        Return name
    End Function
    Private Function ones(digit As [String]) As [String]
        Dim digt As Integer = Convert.ToInt32(digit)
        'Convert Digit String to Int32, Digt= 1
        Dim name As [String] = ""
        Select Case digt
            ' Digt value match with switch cases
            Case 1
                ' Here 1 match with case one,go the name="One";  
                name = "One"
                Exit Select
            Case 2
                name = "Two"
                Exit Select
            Case 3
                name = "Three"
                Exit Select
            Case 4
                name = "Four"
                Exit Select
            Case 5
                name = "Five"
                Exit Select
            Case 6
                name = "Six"
                Exit Select
            Case 7
                name = "Seven"
                Exit Select
            Case 8
                name = "Eight"
                Exit Select
            Case 9
                name = "Nine"
                Exit Select
        End Select
        Return name
        'Here Return Name = One
    End Function
    Private Function translateCents(cents As [String]) As [String]
        Dim cts As [String] = "", digit As [String] = "", engOne As [String] = ""
        For i As Integer = 0 To cents.Length - 1
            ' Cents.lengh find lengh of value "123456789" , Loop Go to 0 to 8(lengh(9))
            digit = cents(i).ToString()
            ' i =0 for starting, got value in digit = 1    
            If digit.Equals("0") Then
                ' Here digit = 0 ,here got digit 1
                engOne = "Zero"
            Else
                ' Go to else part 
                'Go to Ones Function 
                engOne = ones(digit)
            End If
            cts += " " & engOne
            If i = 1 Then
                If Convert.ToInt32(cents) > 9 AndAlso Convert.ToInt32(cents) < 21 Then
                    cts = " " & Convert.ToString(tens(cents))
                Else
                    digit = cents(0).ToString()
                    cts = " " & Convert.ToString(tens(digit & "0"))
                    digit = cents(1).ToString()
                    cts += " " & Convert.ToString(ones(digit))
                End If
            End If
        Next
        Return cts
    End Function

    Private Function FindNumber(ByVal Number As Long) As String
        Dim Words As String = Nothing
        Dim Digits() As String = {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven",
      "Eight", "Nine", "Ten"}
        Dim Teens() As String = {"", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen",
       "Eighteen", "Nineteen"}

        If Number < 11 Then
            Words = Digits(Number)

        ElseIf Number < 20 Then
            Words = Teens(Number - 10)

        ElseIf Number = 20 Then
            Words = "Twenty"

        ElseIf Number < 30 Then
            Words = "Twenty " & Digits(Number - 20)

        ElseIf Number = 30 Then
            Words = "Thirty"

        ElseIf Number < 40 Then
            Words = "Thirty " & Digits(Number - 30)

        ElseIf Number = 40 Then
            Words = "Fourty"

        ElseIf Number < 50 Then
            Words = "Fourty " & Digits(Number - 40)

        ElseIf Number = 50 Then
            Words = "Fifty"

        ElseIf Number < 60 Then
            Words = "Fifty " & Digits(Number - 50)

        ElseIf Number = 60 Then
            Words = "Sixty"

        ElseIf Number < 70 Then
            Words = "Sixty " & Digits(Number - 60)

        ElseIf Number = 70 Then
            Words = "Seventy"

        ElseIf Number < 80 Then
            Words = "Seventy " & Digits(Number - 70)

        ElseIf Number = 80 Then
            Words = "Eighty"

        ElseIf Number < 90 Then
            Words = "Eighty " & Digits(Number - 80)

        ElseIf Number = 90 Then
            Words = "Ninety"

        ElseIf Number < 100 Then
            Words = "Ninety " & Digits(Number - 90)

        ElseIf Number < 1000 Then
            Words = Number.ToString
            Words = Words.Insert(1, ",")
            Dim wparts As String() = Words.Split(",")
            Words = FindNumber(wparts(0)) & " " & "Hundred"
            Dim n As String = FindNumber(wparts(1))
            If CLng(wparts(1)) <> 0 Then
                Words = Words & " And " & n
            End If
        End If

        Return Words
    End Function

    Private Function FindSuffix(ByVal Length As Long, ByVal l As Long) As String
        Dim word As String

        If l <> 0 Then
            If Length > 12 Then
                word = "Trillion"
            ElseIf Length > 9 Then
                word = "Billion"
            ElseIf Length > 6 Then
                word = "Million"
            ElseIf Length > 3 Then
                word = "Thousand"
            ElseIf Length > 2 Then
                word = "Hundred"
            Else
                word = ""
            End If
        Else
            word = ""
        End If

        Return word
    End Function

    Public Sub FlashAlert(ByVal Message As String)
        'Dim wait As PleaseWait = New PleaseWait
        'wait.Show(Message)
        Threading.Thread.Sleep(1000)
        'wait.Hide()
    End Sub

    Public Function CleanTextForDB(ByVal OriginalText As String) As String
        Return OriginalText.Replace("'", "`")
    End Function

    'Public Function Encrypt(ByVal clearText As String) As String
    '    Return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(clearText, "SHA1")
    'End Function

    Public Function Encrypt(ByVal Message As String) As String
        Return Message
        'Dim Results As Byte()
        'Dim UTF8 As New System.Text.UTF8Encoding()

        '' Step 1. We hash the passphrase using MD5
        '' We use the MD5 hash generator as the result is a 128 bit byte array
        '' which is a valid length for the TripleDES encoder we use below

        'Dim HashProvider As New MD5CryptoServiceProvider()
        'Dim TDESKey As Byte() = HashProvider.ComputeHash(UTF8.GetBytes("wahbhaiwahji"))

        '' Step 2. Create a new TripleDESCryptoServiceProvider object
        'Dim TDESAlgorithm As New TripleDESCryptoServiceProvider()

        '' Step 3. Setup the encoder
        'TDESAlgorithm.Key = TDESKey
        'TDESAlgorithm.Mode = CipherMode.ECB
        'TDESAlgorithm.Padding = PaddingMode.PKCS7

        '' Step 4. Convert the input string to a byte[]
        'Dim DataToEncrypt As Byte() = UTF8.GetBytes(Message)

        '' Step 5. Attempt to encrypt the string
        'Try
        '    Dim Encryptor As ICryptoTransform = TDESAlgorithm.CreateEncryptor()
        '    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length)
        'Finally
        '    ' Clear the TripleDes and Hashprovider services of any sensitive information
        '    TDESAlgorithm.Clear()
        '    HashProvider.Clear()
        'End Try

        '' Step 6. Return the encrypted string as a base64 encoded string
        'Return Convert.ToBase64String(Results)
    End Function

    Public Function AllowMultiuser() As Boolean
        Dim Dbops As DbOperations.SharedVariables = New DbOperations.SharedVariables(Me)
        Return Dbops.IsMultiuserAllowed()
    End Function

    Public Function RemoteFileExists(ByVal url As String) As Boolean
        Dim req As System.Net.WebRequest
        req = System.Net.WebRequest.Create(url)
        Dim resp As System.Net.WebResponse
        Try
            resp = req.GetResponse()
            resp.Close()
            req = Nothing
            Return True
        Catch ex As Exception
            req = Nothing
            Return False
        End Try
    End Function

    Public Sub HandleDBError_OnNothingReturned(Optional cForm As Form = Nothing)
        DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        If Not cForm Is Nothing Then cForm.Dispose()
    End Sub

    Public Function IsInsuranceAudited() As Boolean
        If _open_User_Type.ToUpper = Common_Lib.Common.ClientUserType.SuperUser.ToUpper Then
            Dim Dbops As DbOperations.SharedVariables = New DbOperations.SharedVariables(Me)
            Return Dbops.IsInsuranceAudited()
        End If
        Return False
    End Function

    Public Function IsInsuranceAuditor(UserId As String) As Boolean
        Dim Dbops As DbOperations.SharedVariables = New DbOperations.SharedVariables(Me)
        Return Dbops.IsInsuranceAuditor(UserId)
        Return False
    End Function

    Public Function GetBarCode(_base As Common_Lib.Common, screen As Common_Lib.RealTimeService.ClientScreen, Optional RecID As String = "")
        Dim Code As String = _base._open_Cen_ID.ToString + _base._open_Year_ID.ToString + _base._Current_Version.ToString + _base._Action_Items_DBOps.GetCurrentDateTime(screen).ToString()
        Select Case screen
            Case Common_Lib.RealTimeService.ClientScreen.Report_Potamail
                Code = "P:" + Code
            Case Common_Lib.RealTimeService.ClientScreen.Report_InsuranceLetter
                Code = "I:" + Code
            Case Common_Lib.RealTimeService.ClientScreen.Report_InsuranceLetter
                Code = "M:" + RecID
        End Select
        Code = Code.PadRight(38, "0")
        Return Code
    End Function

    Public Function OnChangeCenter_Year() As Boolean
        Dim Dbops As DbOperations.SharedVariables = New DbOperations.SharedVariables(Me)
        Dim _ds As DataSet = Dbops.GetClientAuthorizations()
        Me._Auth_Rights_Listing = _ds.Tables(0)
        Me._Menu_vibilities_Listing = _ds.Tables(1)
        Me._Dynamic_Menu_Listing = _ds.Tables(2)
        Return True
    End Function

    'Public Function ShowMessagebox(Title As String, SetMessage As String, SetButton As Prompt_Window.ButtonType, SetWindowSize As Prompt_Window.WindowSize, SetFocusOn As Prompt_Window.FocusButton, setBackColor As System.Drawing.Color, Optional FirstButtonText As String = "", Optional SecondButtonText As String = "") As System.Windows.Forms.DialogResult
    '    If Not _test_Mode Then
    '        Dim icon As MessageBoxIcon
    '        Select Case SetButton
    '            Case Prompt_Window.ButtonType._Exclamation
    '                icon = MessageBoxIcon.Exclamation
    '            Case Else
    '                icon = MessageBoxIcon.Information
    '        End Select
    '        Return DevExpress.XtraEditors.XtraMessageBox.Show(SetMessage, Title, MessageBoxButtons.OK, icon)
    '    End If
    '    If xMessageWindow Is Nothing Then xMessageWindow = New Common_Lib.Prompt_Window
    '    If xMessageWindow.isDisposed Then xMessageWindow = New Common_Lib.Prompt_Window
    '    Return xMessageWindow.ShowDialog(Title, SetMessage, SetButton, SetWindowSize, SetFocusOn, setBackColor, FirstButtonText, SecondButtonText)
    'End Function

    'Public Function ShowMessagebox(SetMessage As String, Title As String, btn As MessageBoxButtons, icon As MessageBoxIcon) As System.Windows.Forms.DialogResult
    '    If Not _test_Mode Then
    '        Return DevExpress.XtraEditors.XtraMessageBox.Show(SetMessage, Title, btn, icon)
    '    End If
    '    If xMessageWindow Is Nothing Then xMessageWindow = New Common_Lib.Prompt_Window
    '    If xMessageWindow.isDisposed Then xMessageWindow = New Common_Lib.Prompt_Window
    '    Dim SetButton As Prompt_Window.ButtonType = Nothing
    '    Dim SetWindowSize As Prompt_Window.WindowSize = Prompt_Window.WindowSize._399x140
    '    Dim SetFocusOn As Prompt_Window.FocusButton = Prompt_Window.FocusButton._Button1
    '    Dim setBackColor As System.Drawing.Color = Color.AliceBlue
    '    Dim FirstButtonText As String = "OK"
    '    Dim SecondButtonText As String = "OK"
    '    Select Case icon
    '        Case MessageBoxIcon.Exclamation
    '            SetButton = Prompt_Window.ButtonType._Exclamation
    '        Case Else
    '            SetButton = Prompt_Window.ButtonType._Information
    '    End Select
    '    Return xMessageWindow.ShowDialog(Title, SetMessage, SetButton, SetWindowSize, SetFocusOn, setBackColor, FirstButtonText, SecondButtonText)
    'End Function

    'Public Function addAllstepsforTestCase() As Boolean
    '    Dim _auto As DbOperations.automation = New DbOperations.automation(Me)
    '    _auto.addSteps()
    '    _test_case_steps_Table.Rows.Clear()
    '    Return True
    'End Function

    Public Function IsDate(ByVal text As String) As Boolean
        Dim temp As DateTime
        If DateTime.TryParse(text, temp) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function CheckPrevYearID(_prev_Unaudited_YearID As Int32) As Boolean
        If _prev_Unaudited_YearID = Nothing Then
            Return False
        End If
        If _prev_Unaudited_YearID = 0 Then
            Return False
        End If
        If _prev_Unaudited_YearID.ToString().Length = 0 Then
            Return False
        End If
        Return True
    End Function
    Public Function CheckNextYearID(_next_Unaudited_YearID As Int32) As Boolean
        If _next_Unaudited_YearID = Nothing Then
            Return False
        End If
        If _next_Unaudited_YearID = 0 Then
            Return False
        End If
        If _next_Unaudited_YearID.ToString().Length = 0 Then
            Return False
        End If
        Return True
    End Function
#End Region

#Region "SQL"

    'SQL Command String Generator-------------------------------------< Start
    'Usage:  
    '   Dim ebase As New Common
    '   ebase.SQL_Data = New Generic.List(Of String)
    '---------for generate insert string---------------------
    '   ebase.SQL_Data.Add("Test_Table")
    '   ebase.SQL_Data.Add("Test_Field_1") : ebase.SQL_Data.Add(Common.SQL_Field.iText)    : ebase.SQL_Data.Add("string value")
    '   ebase.SQL_Data.Add("Test_Field_2") : ebase.SQL_Data.Add(Common.SQL_Field.iNumeric) : ebase.SQL_Data.Add(12345.5)
    '   ebase.SQL_Data.Add("Test_Field_3") : ebase.SQL_Data.Add(Common.SQL_Field.iDate)    : ebase.SQL_Data.Add("12/05/2005")
    '   MsgBox(ebase.SQL_Insert())
    '---------for generate update string---------------------
    '   ebase.SQL_Condition = "Where REF_ID = 100"
    '   MsgBox(ebase.SQL_Update())
    '------------------------------------------------------------------------
    Public SQL_Data As Generic.List(Of String)
    Public Enum SQL_Field : iText = 1 : iNumeric = 2 : iDate = 3 : iWithoutQuotes = 4 : End Enum
    Public SQL_Condition As String = Nothing
    Public Function SQL_Insert() As String
        Dim STR As String = "INSERT INTO " & SQL_Data(0) & " (" : Dim cnt As Integer = 1
        For I As Integer = 0 To (SQL_Data.Count / 3) - 1 : STR += " " & SQL_Data(cnt) & " , " : cnt += 3 : Next
        STR = STR.Trim : If STR.EndsWith(",") Then STR = Mid(STR, 1, STR.Length - 1) : STR += ") VALUES (" : cnt = 0
        For I As Integer = 1 To (SQL_Data.Count / 3)
            Select Case SQL_Data(cnt + 2)
                Case SQL_Field.iText : STR += " '" & SQL_Data(cnt + 3) & "' , "
                Case SQL_Field.iNumeric : STR += " " & Val(SQL_Data(cnt + 3)) & " , "
                Case SQL_Field.iDate : STR += " #" & SQL_Data(cnt + 3) & "# , "
                Case SQL_Field.iWithoutQuotes : STR += " " & SQL_Data(cnt + 3) & " , "
            End Select : cnt += 3
        Next
        STR = STR.Trim : If STR.EndsWith(",") Then STR = Mid(STR, 1, STR.Length - 1) : STR += ")"
        Return STR
    End Function
    Public Function SQL_Update() As String
        Dim STR As String = "Update " & SQL_Data(0) & " Set " : Dim cnt As Integer = 0
        For I As Integer = 1 To (SQL_Data.Count / 3)
            Select Case SQL_Data(cnt + 2)
                Case SQL_Field.iText : STR += SQL_Data(cnt + 1) & " = '" & SQL_Data(cnt + 3) & "' , "
                Case SQL_Field.iNumeric : STR += SQL_Data(cnt + 1) & " = " & Val(SQL_Data(cnt + 3)) & " , "
                Case SQL_Field.iDate : STR += SQL_Data(cnt + 1) & " = '" & SQL_Data(cnt + 3) & "' , "
                Case SQL_Field.iWithoutQuotes : STR += SQL_Data(cnt + 1) & " = " & SQL_Data(cnt + 3) & " , "
            End Select : cnt += 3
        Next
        STR = STR.Trim : If STR.EndsWith(",") Then STR = Mid(STR, 1, STR.Length - 1) : STR += " " & SQL_Condition
        Return STR
    End Function
    'SQL Command String Generator-------------------------------------< End

#End Region

#Region "Configure"

    Public Sub Get_Configure_Setting()

        Dim Make_INI_File As New Utility.INI_FileTask(_App_path & "COMMON\ConnectOne.Set.ini")
        ''---------WRITING

        'Make_INI_File.WriteValue("Startup", "PATH", _App_path) 'PC NAME

        'Make_INI_File.WriteValue("SERVER", "DB_NODE", "YES") 'PC WORK ON SERVER OR CLIENT
        'Make_INI_File.WriteValue("SETTING", "DT_SET", "DD/MM/YYYY") 'DATE FORMAT

        '---------READING
        Dim Get_DB_Path As String = Make_INI_File.GetString("CLIENT-DATA", "DB_PATH", "C:\ConnectOne\")
        _db_path = Get_DB_Path : _db_path = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\")

        _dbname_Core = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "CORE\COD_CORE_INFO.COT"
        _dbname_Sys = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "SYSTEM\COD_SYS_INFO.COT"
        _dbname_Data = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "Data\COD_" & _open_Ins_ID & "_" & _open_Cen_ID & "_" & _open_Year_ID & ".COT"
        _dbname_Template = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "TEMPLATE\COD_TEMPLATE.COT"

        _data_ConStr_Core = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Core & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Sys = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Sys & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Data = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Data & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Template = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Template & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"

        _allowInfoLogs = False
        Dim logs As String = Make_INI_File.GetString("allowInfoLogs", "ENABLED", "FALSE")
        If logs.ToUpper = "TRUE" Then _allowInfoLogs = True
        '_HTTP_Server = "http://localhost:1916/"  'Make_INI_File.GetString("HTTP-SERVER", "SERVER", "http://192.168.61.142/")'http://localhost:1916/"
        '_HTTP_Server = "http://192.168.61.142:1008/realtimeservice/"  'Make_INI_File.GetString("HTTP-SERVER", "SERVER", "http://192.168.61.142/")'http://localhost:1916/"
        '_http_server = "http://192.168.61.147:1008/realtimeservice/"
        '_HTTP_Server = "http://192.168.61.142:1916/realtimeservice/"
        '_HTTP_Server = "http://192.168.155.29/"
        '_HTTP_Server = "http://192.168.90.250:1007/"
        _HTTP_Server = ConfigurationManager.AppSettings("HttpServer")

        _UPDATE_Server = Make_INI_File.GetString("UPDATE-SERVER", "SERVER", _HTTP_Server)
        _HTTP_Server = IIf(_HTTP_Server.EndsWith("/"), _HTTP_Server, _HTTP_Server & "/")
        _RealTime_Server = _HTTP_Server & _RealTime_Location_Suffix
        _HTTP_Server = _HTTP_Server & _ShivService_Location_Suffix

        _test_Mode = IIf(Make_INI_File.GetString("TEST", "MODE", "LIVE").ToUpper = "TEST", True, False)

        _HTTP_Server2 = Make_INI_File.GetString("HTTP-SERVER2", "SERVER", _HTTP_Server)
        _HTTP_Server2 = IIf(_HTTP_Server2.EndsWith("/"), _HTTP_Server2, _HTTP_Server2 & "/")

        If _HTTP_Server2.Contains("accountserver") Or _HTTP_Server2.Contains("192.168.60.27") Then
            If Not _HTTP_Server.Contains("accountserver") And Not _HTTP_Server.Contains("192.168.60.27") Then
                _HTTP_Server2 = _HTTP_Server
            End If
        End If
        _RealTime_Server2 = _HTTP_Server2 & "RealTimeService/service.asmx"
        _HTTP_Server2 = _HTTP_Server2 & "shivservice/service.asmx"

        _HTTP_Server3 = Make_INI_File.GetString("HTTP-SERVER3", "SERVER", "http://accountserver3.bkinfo.in/")
        _HTTP_Server3 = IIf(_HTTP_Server3.EndsWith("/"), _HTTP_Server3, _HTTP_Server3 & "/")
        _RealTime_Server3 = _HTTP_Server3 & "RealTimeService/service.asmx"
        _HTTP_Server3 = _HTTP_Server3 & "shivservice/service.asmx"


        _LOG_Server = Make_INI_File.GetString("LOG-SERVER", "SERVER", _UPDATE_Server)
        '_LOG_Server = _LOG_Server.Replace("http://", "")
        '_LOG_Server = _LOG_Server.Replace("/", "")

        Dim Make_INI_File1 As New Common_Lib.Utility.INI_FileTask(ProgramFilesPath & "PC-Info\Info.ini")
        _PC_ID = Make_INI_File1.GetString("INFORMATION", "ID", "")

        If _telephoneDBOps Is Nothing Then _telephoneDBOps = New DbOperations.Telephones(Me)
        If _AssetLocDBOps Is Nothing Then _AssetLocDBOps = New DbOperations.AssetLocations(Me)
        If _AndroidDBOps Is Nothing Then _AndroidDBOps = New DbOperations.Android_Notifications(Me)
        If _CenterDBOps Is Nothing Then _CenterDBOps = New DbOperations.Center(Me)
        If _ChartDBOps Is Nothing Then _ChartDBOps = New DbOperations.Chart(Me)
        If _ClientUserDBOps Is Nothing Then _ClientUserDBOps = New DbOperations.ClientUserInfo(Me)
        If _MurliDBOps Is Nothing Then _MurliDBOps = New DbOperations.Murli(Me)
        If _VehicleDBOps Is Nothing Then _VehicleDBOps = New DbOperations.Vehicles(Me)
        If _AssetDBOps Is Nothing Then _AssetDBOps = New DbOperations.Assets(Me)
        If _CODDBOps Is Nothing Then _CODDBOps = New DbOperations.CodInfo(Me)
        If _ConsumableStockDBOps Is Nothing Then _ConsumableStockDBOps = New DbOperations.ConsumableStock(Me)
        If _GoldSilverDBOps Is Nothing Then _GoldSilverDBOps = New DbOperations.GoldSilver(Me)
        If _LiveStockDBOps Is Nothing Then _LiveStockDBOps = New DbOperations.LiveStock(Me)
        If _AdvanceDBOps Is Nothing Then _AdvanceDBOps = New DbOperations.Advances(Me)
        If _BankAccountDBOps Is Nothing Then _BankAccountDBOps = New DbOperations.BankAccounts(Me)
        If _CashDBOps Is Nothing Then _CashDBOps = New DbOperations.Cash(Me)
        If _CoreDBOps Is Nothing Then _CoreDBOps = New DbOperations.Core(Me)
        If _CenterPurposeDBOps Is Nothing Then _CenterPurposeDBOps = New DbOperations.Center_Purpose_Info(Me)
        If _DepositsDBOps Is Nothing Then _DepositsDBOps = New DbOperations.Deposits(Me)
        If _DepositSlipsDBOps Is Nothing Then _DepositSlipsDBOps = New DbOperations.DepositSlips(Me)
        If _LiabilityDBOps Is Nothing Then _LiabilityDBOps = New DbOperations.Liabilities(Me)
        If _ServPlacesDBOps Is Nothing Then _ServPlacesDBOps = New DbOperations.ServicePlaces(Me)
        If _FDDBOps Is Nothing Then _FDDBOps = New DbOperations.FD(Me)
        If _L_B_DBOps Is Nothing Then _L_B_DBOps = New DbOperations.LandAndBuilding(Me)
        If _Address_DBOps Is Nothing Then _Address_DBOps = New DbOperations.Addresses(Me)
        If _SSOuls_DBOps Is Nothing Then _SSOuls_DBOps = New DbOperations.ServiceableSouls(Me)
        If _Students_DBOps Is Nothing Then _Students_DBOps = New DbOperations.Students(Me)
        If _Report_DBOps Is Nothing Then _Report_DBOps = New DbOperations.Report(Me)
        If _Chang_Password_DBOps Is Nothing Then _Chang_Password_DBOps = New DbOperations.ChangePassword(Me)
        If _Reset_Password_DBOps Is Nothing Then _Reset_Password_DBOps = New DbOperations.ResetPassword(Me)
        If _Password_DBOps Is Nothing Then _Password_DBOps = New DbOperations.Password(Me)
        If _Maintain_DBOps Is Nothing Then _Maintain_DBOps = New DbOperations.Maintenance(Me)
        If _Docs_DBOps Is Nothing Then _Docs_DBOps = New DbOperations.DocLibrary(Me)
        If _News_DBOps Is Nothing Then _News_DBOps = New DbOperations.News(Me)
        If _Req_DBOps Is Nothing Then _Req_DBOps = New DbOperations.Request(Me)
        If _Letters_DBOps Is Nothing Then _Letters_DBOps = New DbOperations.Letters(Me)
        If _Notes_DBOps Is Nothing Then _Notes_DBOps = New DbOperations.Notes(Me)
        If _SR_DBOps Is Nothing Then _SR_DBOps = New DbOperations.ServiceReport(Me)
        If _SerProj_DBOps Is Nothing Then _SerProj_DBOps = New DbOperations.ServiceProject(Me)
        If _Schedule_DBOps Is Nothing Then _Schedule_DBOps = New DbOperations.Schedule(Me)
        If _Chart_DBOps Is Nothing Then _Chart_DBOps = New DbOperations.ServiceChart(Me)
        If _SM_DBOps Is Nothing Then _SM_DBOps = New DbOperations.ServiceModule(Me)
        If _SMGS_DBOps Is Nothing Then _SMGS_DBOps = New DbOperations.SM_GodlyServices(Me)
        If _Reminder_DBOps Is Nothing Then _Reminder_DBOps = New DbOperations.Reminders(Me)
        If _DonationRegister_DBOps Is Nothing Then _DonationRegister_DBOps = New DbOperations.DonationRegister(Me)
        If _NoteBook_DBOps Is Nothing Then _NoteBook_DBOps = New DbOperations.NoteBook(Me)
        If _CollectionBox_DBOps Is Nothing Then _CollectionBox_DBOps = New DbOperations.Voucher_CollectionBox(Me)
        If _Voucher_DBOps Is Nothing Then _Voucher_DBOps = New DbOperations.Vouchers(Me)
        If _b2b_DBOps Is Nothing Then _b2b_DBOps = New DbOperations.Voucher_BankToBank(Me)
        If _Cash_Bank_DBOps Is Nothing Then _Cash_Bank_DBOps = New DbOperations.Voucher_CashBank(Me)
        If _Donation_DBOps Is Nothing Then _Donation_DBOps = New DbOperations.Voucher_Donation(Me)
        If _Gift_DBOps Is Nothing Then _Gift_DBOps = New DbOperations.Voucher_Gift(Me)
        If _Rect_DBOps Is Nothing Then _Rect_DBOps = New DbOperations.Voucher_Receipt(Me)
        If _Payment_DBOps Is Nothing Then _Payment_DBOps = New DbOperations.Voucher_Payment(Me)
        If _FD_Voucher_DBOps Is Nothing Then _FD_Voucher_DBOps = New DbOperations.Voucher_FD(Me)
        If _L_B_Voucher_DBOps Is Nothing Then _L_B_Voucher_DBOps = New DbOperations.Voucher_Property(Me)
        If _Internal_Tf_Voucher_DBOps Is Nothing Then _Internal_Tf_Voucher_DBOps = New DbOperations.Voucher_Internal_Transfer(Me)
        If _SaleOfAsset_DBOps Is Nothing Then _SaleOfAsset_DBOps = New DbOperations.Voucher_SaleOfAsset(Me)
        If _Reports_Items_DBOps Is Nothing Then _Reports_Items_DBOps = New DbOperations.Report_ItemList(Me)
        If _Reports_Common_DBOps Is Nothing Then _Reports_Common_DBOps = New DbOperations.Reports_All(Me)
        If _Action_Items_DBOps Is Nothing Then _Action_Items_DBOps = New DbOperations.Action_Items(Me)
        If _Audit_DBOps Is Nothing Then _Audit_DBOps = New DbOperations.Audit(Me)
        If _Notifications_DBOps Is Nothing Then _Notifications_DBOps = New DbOperations.Notifications(Me)
        If _DataRestriction_DBOps Is Nothing Then _DataRestriction_DBOps = New DbOperations.DataRestriction(Me)
        If _UserPreferences_DBOps Is Nothing Then _UserPreferences_DBOps = New DbOperations.UserPreferences(Me)
        If _Membership_DBOps Is Nothing Then _Membership_DBOps = New DbOperations.Membership(Me)
        If _Membership_Voucher_DBOps Is Nothing Then _Membership_Voucher_DBOps = New DbOperations.Voucher_Membership(Me)
        If _Membership_Renewal_Voucher_DBOps Is Nothing Then _Membership_Renewal_Voucher_DBOps = New DbOperations.Voucher_Membership_Renewal(Me)
        If _Membership_Conversion_Voucher_DBOps Is Nothing Then _Membership_Conversion_Voucher_DBOps = New DbOperations.Voucher_Membership_Conversion(Me)
        If _Membership_Receipt_Register_DBOps Is Nothing Then _Membership_Receipt_Register_DBOps = New DbOperations.Membership_Receipt_Register(Me)
        If _Journal_voucher_DBOps Is Nothing Then _Journal_voucher_DBOps = New DbOperations.Voucher_Journal(Me)
        If _Reports_Ledgers_DBOps Is Nothing Then _Reports_Ledgers_DBOps = New DbOperations.Report_Ledgers(Me)
        If _OpeningBalances_DBOps Is Nothing Then _OpeningBalances_DBOps = New DbOperations.OpeningBalances(Me)
        If _AssetTransfer_DBOps Is Nothing Then _AssetTransfer_DBOps = New DbOperations.Voucher_AssetTransfer(Me)
        If _Complexes_DBOps Is Nothing Then _Complexes_DBOps = New DbOperations.Complexes(Me)
        If _WIP_Finalization_DBOps Is Nothing Then _WIP_Finalization_DBOps = New DbOperations.WIP_Finalization(Me)
        If _WIPDBOps Is Nothing Then _WIPDBOps = New DbOperations.WIP_Profile(Me)
        If _WIPCretionVouchers Is Nothing Then _WIPCretionVouchers = New DbOperations.WIP_Creation_Vouchers(Me)
        If _Magazine_DBOps Is Nothing Then _Magazine_DBOps = New DbOperations.Magazine(Me)
        If _Magazine_Requests_DBOps Is Nothing Then _Magazine_Requests_DBOps = New DbOperations.Magazine_Membership_Requests(Me)
        If _Magazine_Reports Is Nothing Then _Magazine_Reports = New DbOperations.Magazine_Reports(Me)
        If _Voucher_Magazine_DBOps Is Nothing Then _Voucher_Magazine_DBOps = New DbOperations.Voucher_Magazine(Me)
        If _TDS_DBOps Is Nothing Then _TDS_DBOps = New DbOperations.TDS(Me)
        If _Attachments_DBOps Is Nothing Then _Attachments_DBOps = New DbOperations.Attachments(Me)
        If _Stock_Profile_DBOps Is Nothing Then _Stock_Profile_DBOps = New DbOperations.StockProfile(Me)
        If _Sub_Item_DBOps Is Nothing Then _Sub_Item_DBOps = New DbOperations.SubItems(Me)
        If _Transfer_Orders_DBOps Is Nothing Then _Transfer_Orders_DBOps = New DbOperations.StockTransferOrders(Me)
        If _user_order_DBOps Is Nothing Then _user_order_DBOps = New DbOperations.StockUserOrder(Me)
        If _RR_DBOps Is Nothing Then _RR_DBOps = New DbOperations.StockRequisitionRequest(Me)
        If _PO_DBOps Is Nothing Then _PO_DBOps = New DbOperations.StockPurchaseOrder(Me)
        If _Stock_Production_DBOps Is Nothing Then _Stock_Production_DBOps = New DbOperations.StockProduction(Me)
        If _Stock_MachineTools_DBOps Is Nothing Then _Stock_MachineTools_DBOps = New DbOperations.StockMachineToolAllocation(Me)
        If _Projects_Dbops Is Nothing Then _Projects_Dbops = New DbOperations.Projects(Me)
        If _Jobs_Dbops Is Nothing Then _Jobs_Dbops = New DbOperations.Jobs(Me)
        If _Personnels_Dbops Is Nothing Then _Personnels_Dbops = New DbOperations.Personnels(Me)
        If _StockDeptStores_dbops Is Nothing Then _StockDeptStores_dbops = New DbOperations.StockDeptStores(Me)
        If _StockApprovalReqd_dbops Is Nothing Then _StockApprovalReqd_dbops = New DbOperations.StockApprovalRequired(Me)
        If _StockSupplier_dbops Is Nothing Then _StockSupplier_dbops = New DbOperations.Suppliers(Me)
        If _ServiceMaterial_dbops Is Nothing Then _ServiceMaterial_dbops = New DbOperations.GodlyServiceMaterial(Me)
        If _HelpVideos_dbops Is Nothing Then _HelpVideos_dbops = New DbOperations.HelpVideos(Me)

        If _Form_dbops Is Nothing Then _Form_dbops = New DbOperations.Forms(Me)

        'If _test_case_steps_Table Is Nothing Then
        '    _test_case_steps_Table = New DataTable
        '    _test_case_steps_Table.Columns.Add("TestCaseNo")
        '    _test_case_steps_Table.Columns.Add("CONTROL_NAME_IN_CODE")
        '    _test_case_steps_Table.Columns.Add("SCREEN_NAME_IN_CODE")
        '    _test_case_steps_Table.Columns.Add("TEST_DATA")
        '    _test_case_steps_Table.Columns.Add("TAB_INDEX")
        'End If
        If _Auth_Rights_Listing Is Nothing Then
            _Auth_Rights_Listing = New DataTable
            _Auth_Rights_Listing.Columns.Add("Task")
            _Auth_Rights_Listing.Columns.Add("Permission")
        End If

        If _Menu_vibilities_Listing Is Nothing Then
            _Menu_vibilities_Listing = New DataTable
            _Menu_vibilities_Listing.Columns.Add("MENU_ID")
        End If
        If _Dynamic_Menu_Listing Is Nothing Then
            _Dynamic_Menu_Listing = New DataTable
        End If
    End Sub

    Public Function curr_Db_Conn_Mode(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, Optional ByVal Table As RealTimeService.Tables = Nothing) As DbConnectionMode
        If Not Table = Nothing Then
            If Table.ToString.ToUpper.StartsWith("SO") Or Table.ToString.ToUpper.Equals("MISC_INFO") Or Table.ToString.ToUpper.Equals("NEWS_INFO") Or Table.ToString.ToUpper.Equals("ITEM_INFO") Or Table.ToString.ToUpper.Equals("ACC_LEDGER_INFO") Or Table.ToString.ToUpper.Equals("CENTRE_TASK_INFO") Or Table.ToString.ToUpper.Equals("CENTRE_INFO") Then
                Return DbConnectionMode.Online
            End If
        End If
        'use _open_Cen_ID, _open_User_ID to validate the mode 
        Select Case Screen
            Case Common_Lib.RealTimeService.ClientScreen.Start_Create_Center, Common_Lib.RealTimeService.ClientScreen.Start_Select_Center
                Return DbConnectionMode.Online
            Case Else
                Return DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore
        End Select
    End Function

    Public Sub Get_Net_Configure_Setting()
        Dim Make_INI_File As New Utility.INI_FileTask(_App_path & "COMMON\ConnectOne.Set.ini")
        '---------WRITING
        'Make_INI_File.WriteValue("SYSTEM", "NAME", "OMSHANTI") 'PC NAME

        'Make_INI_File.WriteValue("SERVER", "DB_NODE", "YES") 'PC WORK ON SERVER OR CLIENT
        'Make_INI_File.WriteValue("SETTING", "DT_SET", "DD/MM/YYYY") 'DATE FORMAT

        '---------READING
        Dim Get_DB_Path As String = Make_INI_File.GetString("NETWORK-DATA", "DB_PATH", "")
        _db_path = Get_DB_Path : _db_path = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\")

        _dbname_Core = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "CORE\COD_CORE_INFO.COT"
        _dbname_Sys = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "SYSTEM\COD_SYS_INFO.COT"
        _dbname_Data = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "Data\COD_" & _open_Ins_ID & "_" & _open_Cen_ID & "_" & _open_Year_ID & ".COT"
        _dbname_Template = IIf(_db_path.EndsWith("\"), _db_path, _db_path & "\") & "TEMPLATE\COD_TEMPLATE.COT"

        _data_ConStr_Core = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Core & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Sys = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Sys & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Data = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Data & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"
        _data_ConStr_Template = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _dbname_Template & ";Persist Security Info=TRUE;Jet OLEDB:Database Password=AYG_AA_ABAB_AREM"

        _HTTP_Server = Make_INI_File.GetString("HTTP-SERVER", "SERVER", "http://accountserver.bkinfo.in/")
        _HTTP_Server = IIf(_HTTP_Server.EndsWith("/"), _HTTP_Server, _HTTP_Server & "/")
        _HTTP_Server = _HTTP_Server & "shivservice/service.asmx"


        _UPDATE_Server = Make_INI_File.GetString("UPDATE-SERVER", "SERVER", "http://accountserver.bkinfo.in/")

        _LOG_Server = Make_INI_File.GetString("LOG-SERVER", "SERVER", "http://accountserver.bkinfo.in/")
        _LOG_Server = _LOG_Server.Replace("http://", "")
        _LOG_Server = _LOG_Server.Replace("/", "")
    End Sub

    Private ProgramFilesPath As String = IIf(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).EndsWith("\"), System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) & "\")
    Public Sub Create_Unqine_ID()
        If Not IO.Directory.Exists(ProgramFilesPath & "PC-Info") Then IO.Directory.CreateDirectory(ProgramFilesPath & "PC-Info")
        If Not IO.File.Exists(ProgramFilesPath & "PC-Info\Info.ini") Then
            '---------WRITING
            Dim Make_INI_File As New Common_Lib.Utility.INI_FileTask(ProgramFilesPath & "PC-Info\Info.ini")
            Make_INI_File.WriteValue("Information", "ID", System.Guid.NewGuid.ToString)
        Else
            '---------READING
            Dim Make_INI_File As New Common_Lib.Utility.INI_FileTask(ProgramFilesPath & "PC-Info\Info.ini")
            Dim Get_id As String = Make_INI_File.GetString("INFORMATION", "ID", "")
            If Get_id.Length < 36 Then
                Make_INI_File.WriteValue("Information", "ID", System.Guid.NewGuid.ToString)
            End If
        End If
    End Sub

    Public Function GetVersion(ByVal Version As String) As String
        Dim x() As String = Split(Version, ".")
        Return String.Format("{0:00000}{1:00000}{2:00000}{3:00000}", Int(x(0)), Int(x(1)), Int(x(2)), Int(x(3)))
    End Function

    'Public Function DateTime_Mismatch() As Boolean
    '    If curr_Db_Conn_Mode(ClientScreen.CommonFunctions) = DbConnectionMode.Online Or curr_Db_Conn_Mode(ClientScreen.CommonFunctions) = DbConnectionMode.TxnsOnline_LocalCore_BackedByOnlineCore Then
    '        Return False 'No need to check in online mode 
    '    End If
    '    _Sync_Last_DateTime = ConnectOne.Sync.BLL.DBOperations.GetLastSuccessfulSyncTime(_dbname_Sys, Log.LogSuffix.ClientApplication)
    '    If _Sync_Last_DateTime > Now Then
    '        DevExpress.XtraEditors.XtraMessageBox.Show("Your Computer Date-Time Incorrect...!" & vbNewLine & vbNewLine & "Contact: Madhuban, Connect One Team.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

#End Region

End Class
<Serializable>
Public Class ObjectShredder(Of T)
    ' Fields 
    Private _fi As FieldInfo()
    Private _ordinalMap As Dictionary(Of String, Integer)
    Private _pi As PropertyInfo()
    Private _type As Type

    ' Constructor  
    Public Sub New()
        Me._type = GetType(T)
        Me._fi = Me._type.GetFields
        Me._pi = Me._type.GetProperties
        Me._ordinalMap = New Dictionary(Of String, Integer)
    End Sub

    Public Function ShredObject(ByVal table As DataTable, ByVal instance As T) As Object()
        Dim fi As FieldInfo() = Me._fi
        Dim pi As PropertyInfo() = Me._pi
        If (Not instance.GetType Is GetType(T)) Then
            ' If the instance is derived from T, extend the table schema 
            ' and get the properties and fields. 
            Me.ExtendTable(table, instance.GetType)
            fi = instance.GetType.GetFields
            pi = instance.GetType.GetProperties
        End If

        ' Add the property and field values of the instance to an array. 
        Dim values As Object() = New Object(table.Columns.Count - 1) {}
        Dim f As FieldInfo
        For Each f In fi
            values(Me._ordinalMap.Item(f.Name)) = f.GetValue(instance)
        Next
        Dim p As PropertyInfo
        For Each p In pi
            values(Me._ordinalMap.Item(p.Name)) = p.GetValue(instance, Nothing)
        Next

        ' Return the property and field values of the instance. 
        Return values
    End Function


    ' Summary:           Loads a DataTable from a sequence of objects. 
    ' source parameter:  The sequence of objects to load into the DataTable.</param> 
    ' table parameter:   The input table. The schema of the table must match that  
    '                    the type T.  If the table is null, a new table is created   
    '                    with a schema created from the public properties and fields  
    '                    of the type T. 
    ' options parameter: Specifies how values from the source sequence will be applied to  
    '                    existing rows in the table. 
    ' Returns:           A DataTable created from the source sequence. 

    Public Function Shred(ByVal source As IEnumerable(Of T), ByVal table As DataTable, ByVal options As LoadOption?) As DataTable

        ' Load the table from the scalar sequence if T is a primitive type. 
        If GetType(T).IsPrimitive Then
            Return Me.ShredPrimitive(source, table, options)
        End If

        ' Create a new table if the input table is null. 
        If (table Is Nothing) Then
            table = New DataTable(GetType(T).Name)
        End If

        ' Initialize the ordinal map and extend the table schema based on type T.
        table = Me.ExtendTable(table, GetType(T))

        ' Enumerate the source sequence and load the object values into rows.
        table.BeginLoadData()
        Using e As IEnumerator(Of T) = source.GetEnumerator
            Do While e.MoveNext
                If options.HasValue Then
                    table.LoadDataRow(Me.ShredObject(table, e.Current), options.Value)
                Else
                    table.LoadDataRow(Me.ShredObject(table, e.Current), True)
                End If
            Loop
        End Using
        table.EndLoadData()

        ' Return the table. 
        Return table
    End Function


    Public Function ShredPrimitive(ByVal source As IEnumerable(Of T), ByVal table As DataTable, ByVal options As LoadOption?) As DataTable
        ' Create a new table if the input table is null. 
        If (table Is Nothing) Then
            table = New DataTable(GetType(T).Name)
        End If
        If Not table.Columns.Contains("Value") Then
            table.Columns.Add("Value", GetType(T))
        End If

        ' Enumerate the source sequence and load the scalar values into rows.
        table.BeginLoadData()
        Using e As IEnumerator(Of T) = source.GetEnumerator
            Dim values As Object() = New Object(table.Columns.Count - 1) {}
            Do While e.MoveNext
                values(table.Columns.Item("Value").Ordinal) = e.Current
                If options.HasValue Then
                    table.LoadDataRow(values, options.Value)
                Else
                    table.LoadDataRow(values, True)
                End If
            Loop
        End Using
        table.EndLoadData()

        ' Return the table. 
        Return table
    End Function

    Public Function ExtendTable(ByVal table As DataTable, ByVal type As Type) As DataTable
        ' Extend the table schema if the input table was null or if the value  
        ' in the sequence is derived from type T. 
        Dim f As FieldInfo
        Dim p As PropertyInfo

        For Each f In type.GetFields
            If Not Me._ordinalMap.ContainsKey(f.Name) Then
                Dim dc As DataColumn

                ' Add the field as a column in the table if it doesn't exist 
                ' already.
                dc = IIf(table.Columns.Contains(f.Name), table.Columns.Item(f.Name), table.Columns.Add(f.Name, f.FieldType))

                ' Add the field to the ordinal map. 
                Me._ordinalMap.Add(f.Name, dc.Ordinal)
            End If

        Next

        For Each p In type.GetProperties
            If Not Me._ordinalMap.ContainsKey(p.Name) Then
                ' Add the property as a column in the table if it doesn't exist 
                ' already. 
                Dim dc As DataColumn
                dc = IIf(table.Columns.Contains(p.Name), table.Columns.Item(p.Name), table.Columns.Add(p.Name, p.PropertyType))

                ' Add the property to the ordinal map. 
                Me._ordinalMap.Add(p.Name, dc.Ordinal)
            End If
        Next

        ' Return the table. 
        Return table
    End Function

End Class

