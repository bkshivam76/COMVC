Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Configuration
Imports Common_Lib.Common
Imports DevExpress.XtraEditors

Public Class CBVoucher
    Public Base As New Common_Lib.Common
    Public recID As String

    Public colBoxObj As ReportDataObjects.CollectionBoxVoucherReport

    Public Sub New(ByVal _recID As String, _MainBase As Common_Lib.Common, Optional _FinalData As ReportDataObjects.CollectionBoxVoucherReport = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        recID = _recID
        Base = _MainBase
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.UserSkins.BonusSkins.Register()
        If _FinalData Is Nothing Then
            SetCollectionBoxDetails(recID)
        Else
            colBoxObj = _FinalData
            Me.Xr_Printable.Text = IIf(Base._ReportsToBePrinted = String.Empty, "", "(" & Base._ReportsToBePrinted & ")")
        End If
    End Sub

    Private Sub CBVoucher_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

        If (colBoxObj Is Nothing) Then
            ShowErrorWindow("Voucher cannot be generated", "Sorry! Voucher cannot be generated.  Please contact administrator to solve the issue.")
            'xPleaseWait.Hide()
            Exit Sub
        End If
        Narration.Text = colBoxObj.narration
        'personNames.Text = colBoxObj.Person1_Name + " And " + colBoxObj.Person2_Name
        ItemNo.Text = "Item Name : " + colBoxObj.Item_No
        AccountHead.Text = "A/c Head : " + colBoxObj.Account_Head
        CBVoucherBindingSource.DataSource = colBoxObj
        'xPleaseWait.Hide()
    End Sub

    Public Sub SetCollectionBoxDetails(ByVal RecId As String)
        Dim dtTransaction As DataTable = Base._Reports_Common_DBOps.GetCollectionBoxTransactionList(RecId, Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher)
        If dtTransaction Is Nothing Then
            Base.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If dtTransaction.Rows.Count < 1 Then
            ShowErrorWindow("Invalid Record ID", "Please select proper Collection Box entry")
            Exit Sub
        End If

        Dim dtItem As DataTable = Base._Reports_Common_DBOps.GetItemsAndLedger(Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher, "COLLECTION BOX")

        If dtItem.Rows.Count < 1 Then
            ShowErrorWindow("Invalid Item Information", "There are no entries in item info. Please contact administrator.")
            Exit Sub
        End If

        Dim dtAddress As DataTable = Base._Reports_Common_DBOps.GetAddressList(Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher, "'" & dtTransaction(0)("TR_AB_ID_1").ToString() & "' , '" & dtTransaction(0)("TR_AB_ID_2").ToString() & "'")

        If dtAddress.Rows.Count < 1 Then
            ShowErrorWindow("Invalid Address Information", "There are no entries in Address book for this item. Please contact administrator.")
            Exit Sub
        End If

        Dim commonLib As New Common_Lib.Common
        'Dim ReportDBOperations As New ReportDBOperations()

        Dim dtCentreInfo As DataTable = Base._Reports_Common_DBOps.GetCentreDetails(Base._open_Cen_ID, Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher)
        'BUILD DATA
        Dim buildData = From Tr In dtTransaction
                        Join A1 In dtAddress On A1.Field(Of String)("rec_id") Equals Tr.Field(Of String)("TR_AB_ID_1")
                        Join A2 In dtAddress On A2.Field(Of String)("rec_id") Equals Tr.Field(Of String)("TR_AB_ID_2")
                        Select New ReportDataObjects.CollectionBoxVoucherReport With
                                                {
                                                    .Person1_Name = A1.Field(Of String)("name"),
                                                    .Person2_Name = A2.Field(Of String)("name"),
                                                    .Total_Amount = Tr.Field(Of Decimal)("Amt"),
                                                    .DateOf_CollectionBox = Tr.Field(Of String)("tr_date"),
                                                    .Voucher_No = IIf(String.IsNullOrEmpty(Tr.Field(Of String)("TR_VNO")), " ", Tr.Field(Of String)("TR_VNO")),
                                                    .Centre_Name = dtCentreInfo.Rows(0)("CEN_NAME").ToString(),
                                                    .Centre_UIDNo = dtCentreInfo.Rows(0)("CEN_UID").ToString(),
                                                    .Zone_Name = dtCentreInfo.Rows(0)("CEN_ZONE_ID").ToString(),
                                                    .Item_No = dtItem(0)("Item").ToString(),
                                                    .Account_Head = dtItem(0)("Head").ToString(),
                                                    .Ins_Name = Base._open_Ins_Name,
                                                    .Amount_InWord = StrConv(Base.ConvertNumToAlphaValue(Tr.Field(Of Decimal)("Amt")).ToString(), vbProperCase),
                                                    .narration = Tr.Field(Of String)("TR_NARRATION")
                                                } : Dim Final_Data = buildData.SingleOrDefault()

        colBoxObj = Final_Data
        Me.Xr_Printable.Text = IIf(Base._ReportsToBePrinted = String.Empty, "", "(" & Base._ReportsToBePrinted & ")")

    End Sub

    Public Sub ShowErrorWindow(ByVal title As String, ByVal msg As String)
        DevExpress.XtraEditors.XtraMessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class