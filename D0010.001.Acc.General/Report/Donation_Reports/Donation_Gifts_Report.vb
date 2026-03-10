Public Class Donation_Gifts_Report
    Public MainBase As New Common_Lib.Common
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public period As String
    Public Fr_Date As Date
    Public To_Date As Date
    Public Sub New(ByVal fromDate As Date, ByVal toDate As Date)
        ' This call is required by the designer.
        Programming_Testing()
        Fr_Date = fromDate
        To_Date = toDate
        InitializeComponent()
    End Sub
    Public Sub New(ByVal fromDate As Date, ByVal toDate As Date, _base As Common_Lib.Common)
        ' This call is required by the designer.
        Programming_Testing()
        Fr_Date = fromDate
        To_Date = toDate
        MainBase = _base
        InitializeComponent()
    End Sub
    Private Sub XtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Programming_Testing()
        'Base = MainBase
        Dim ListDataSource As New ArrayList()
        Me.Xr_Ins_Name.Text = MainBase._open_Ins_Name
        Dim SQL_STR1 As String = "" : Dim CENID As String = "" : Dim Ins As String = ""
        Dim TI_Table As DataTable = MainBase._Reports_Common_DBOps.GetCentreCity(MainBase._open_Cen_ID, Common_Lib.RealTimeService.ClientScreen.Report_Gift)
        Dim centre_City = TI_Table.Rows(0)(0).ToString()
        TI_Table = MainBase._Reports_Common_DBOps.GetGiftTransactionList(Fr_Date, To_Date, Common_Lib.RealTimeService.ClientScreen.Report_Gift)
        If TI_Table Is Nothing Then
            DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'CENTRE INCHARGE & A/C. RES. PERSON-----------------------------
        Dim Centre_Inc As DataTable = MainBase._Report_DBOps.GetCenterDetails()
        If Centre_Inc Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Me.Dispose()
            Exit Sub
        End If
        If Not IsDBNull(Centre_Inc.Rows(0)("CEN_INCHARGE")) Then Xr_Incharge.Text = Centre_Inc.Rows(0)("CEN_INCHARGE") Else Xr_Incharge.Text = ""

        Me.Xr_Version.Text = MainBase._Current_Version
        Dim data As New Donation()
        data.Centre_Name = MainBase._open_Cen_Name & " (" & MainBase._open_UID_No & ")" & "(" & MainBase._open_PAD_No & ")"
        data.City_Name = centre_City
        data.ZoneName = MainBase._open_Zone_ID
        data.Period_Range = Fr_Date & " to " & To_Date

        Me.Xr_Printable.Text = IIf(MainBase._ReportsToBePrinted = String.Empty, "", "(" & MainBase._ReportsToBePrinted & ")")
        If TI_Table.Rows.Count > 0 Then
            For Each transactionInfo As DataRow In TI_Table.Rows
                If IsDBNull(transactionInfo("TR_DR_LED_ID")) Then Continue For
                If (String.IsNullOrEmpty(transactionInfo("TR_DR_LED_ID"))) Then Continue For

                data = New Donation
                data.Centre_Name = MainBase._open_Cen_Name & " (" & MainBase._open_UID_No & ")" & "(" & MainBase._open_PAD_No & ")"
                data.City_Name = centre_City
                data.ZoneName = MainBase._open_Zone_ID
                data.Period_Range = Fr_Date & " to " & To_Date

                Dim dDate As Date = transactionInfo("TR_DATE")
                data.DateOf_Donation = dDate.Date.ToString("dd-MMM-yy")
                data.Transaction_Mode = transactionInfo("TR_MODE").ToString()
                If (Not String.IsNullOrEmpty(transactionInfo("TR_REF_NO").ToString())) Then
                    data.Check_No = data.Transaction_Mode & " / No:" & vbCrLf & transactionInfo("TR_REF_NO").ToString()
                    If (Not String.IsNullOrEmpty(transactionInfo("TR_REF_DATE").ToString())) Then
                        Dim ChqDate As Date = transactionInfo("TR_REF_DATE").Date
                        data.Check_No = data.Check_No & "/Date:" & vbCrLf & ChqDate.Date.ToString("dd-MMM-yy")
                    End If
                End If
                If (transactionInfo("TR_DR_LED_ID").ToString().Equals("00079")) Then
                    data.Bank_Amt = transactionInfo("TR_AMOUNT")
                ElseIf (transactionInfo("TR_DR_LED_ID").ToString().Equals("00080")) Then
                    data.Cash_Amt = transactionInfo("TR_AMOUNT")
                Else
                    data.Journal_Amt = transactionInfo("TR_AMOUNT")
                End If
                data.Total_Amount = transactionInfo("TR_AMOUNT")
                data.Branch_Name = transactionInfo("TR_REF_BRANCH").ToString()

                TI_Table = MainBase._Reports_Common_DBOps.GetDonationStatusID(transactionInfo("REC_ID").ToString(), Common_Lib.RealTimeService.ClientScreen.Report_Gift)
                Dim statusMiscId As String = ""
                If Not TI_Table Is Nothing Then
                    If (TI_Table.Rows.Count > 0) Then
                        statusMiscId = TI_Table.Rows(0)(0).ToString()
                    End If
                End If

                Dim miscId As String = MainBase._Reports_Common_DBOps.GetMiscNameByID(statusMiscId)

                Dim BankID As Object = MainBase._Reports_Common_DBOps.GetBankNameByID(transactionInfo("TR_REF_BANK_ID").ToString())
                If (Not BankID Is Nothing) Then
                    data.Bank_Name = BankID
                Else
                    TI_Table = MainBase._Reports_Common_DBOps.GetItemNameByID("'" & transactionInfo("TR_ITEM_ID").ToString() & "'")
                    If Not TI_Table Is Nothing Then
                        If (TI_Table.Rows.Count > 0) Then
                            If (Not TI_Table.Rows(0)("ITEM_NAME").ToString().ToLower().Contains("donation")) Then
                                data.Bank_Name = TI_Table.Rows(0)("ITEM_NAME").ToString()
                                If data.Transaction_Mode.ToUpper.Equals("JOURNAL") Then
                                    data.Bank_Name = data.Bank_Name + "**"
                                End If
                            End If
                        End If
                    End If
                End If
                If miscId Is Nothing Then miscId = ""
                If ((Not (miscId.Equals("Donation Accepted") Or miscId.Equals("Receipt Request Rejected"))) And Not String.IsNullOrEmpty(miscId)) Then
                    TI_Table = MainBase._Reports_Common_DBOps.GetAddresses(transactionInfo("REC_ID").ToString(), True)
                    'SQL_STR1 = "SELECT C_NAME,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_COUNTRY_ID,C_R_STATE_ID,C_R_DISTRICT_ID FROM donation_receipt_address_book WHERE C_TR_ID ='" & transactionInfo("REC_ID").ToString() & "' ;"
                Else
                    TI_Table = MainBase._Reports_Common_DBOps.GetAddresses(transactionInfo("TR_AB_ID_1").ToString(), False)
                    'SQL_STR1 = "SELECT C_NAME,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_STATE_ID,C_R_COUNTRY_ID,C_R_DISTRICT_ID FROM address_book WHERE REC_ID ='" & transactionInfo("TR_AB_ID_1").ToString() & "' ;"
                End If
                Dim addressInfo As DataRow
                If (TI_Table.Rows.Count > 0) Then
                    addressInfo = TI_Table.Rows(0)
                    data.Full_Name = addressInfo("C_NAME").ToString()
                    data.Address_1 = data.AddComma(addressInfo("C_R_ADD1").ToString()) & data.AddComma(addressInfo("C_R_ADD2").ToString()) & data.AddComma(addressInfo("C_R_ADD3").ToString()) & data.AddComma(addressInfo("C_R_ADD4").ToString())
                    If (Not String.IsNullOrEmpty(addressInfo("C_PAN_NO").ToString())) Then
                        data.Pan_No = "PAN NO: " & addressInfo("C_PAN_NO").ToString()
                    End If
                    If (Not String.IsNullOrEmpty(addressInfo("C_R_DISTRICT_ID").ToString())) Then
                        TI_Table = MainBase._Reports_Common_DBOps.GetDistrictList("'" & addressInfo("C_R_DISTRICT_ID").ToString() & "'")
                        If TI_Table.Rows.Count > 0 Then
                            Dim districtInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & data.AddComma(districtInfo(0).ToString())
                        End If
                    End If
                    If (Not String.IsNullOrEmpty(addressInfo("C_R_CITY_ID").ToString())) Then
                        TI_Table = MainBase._Reports_Common_DBOps.GetCityList("'" & addressInfo("C_R_CITY_ID").ToString() & "'")
                        If TI_Table.Rows.Count > 0 Then
                            Dim cityInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & data.AddComma(cityInfo(0).ToString())
                        End If
                    End If
                    If (Not String.IsNullOrEmpty(addressInfo("C_R_STATE_ID").ToString())) Then
                        TI_Table = MainBase._Reports_Common_DBOps.GetStateList("'" & addressInfo("C_R_STATE_ID").ToString() & "'")
                        If TI_Table.Rows.Count > 0 Then
                            Dim stateInfo As DataRow = TI_Table.Rows(0)
                            Dim State As String = stateInfo(0).ToString()
                            If (Not String.IsNullOrEmpty(addressInfo("C_R_PINCODE").ToString())) Then State = State & "-" & addressInfo("C_R_PINCODE").ToString()
                            data.Address_1 = data.Address_1 & data.AddComma(State)
                        End If
                    End If
                    If (Not String.IsNullOrEmpty(addressInfo("C_R_COUNTRY_ID").ToString())) Then
                        TI_Table = MainBase._Reports_Common_DBOps.GetCountryList("'" & addressInfo("C_R_COUNTRY_ID").ToString() & "'")
                        If TI_Table.Rows.Count > 0 Then
                            Dim countryInfo As DataRow = TI_Table.Rows(0)
                            data.Address_1 = data.Address_1 & countryInfo(0).ToString()
                        End If
                    End If
                End If
                ListDataSource.Add(data)
            Next
        Else
            ListDataSource.Add(data)
        End If
        ' If TI_Table.Rows.Count = 0 Then ListDataSource.Add(data)
        Me.DataSource = ListDataSource
    End Sub
End Class