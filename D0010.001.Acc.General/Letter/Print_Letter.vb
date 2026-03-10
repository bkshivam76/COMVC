Public Class Print_Letter
    Public No_of_Copies As Integer = 1
    Public Title As String
    Public Xid As String
    Public Xoption As String
    Public XTopMargin As Double
    Dim iTable As OleDb.OleDbDataAdapter
    Dim iDataSet As DataSet
    Public MainBase As New Common_Lib.Common

    Private Sub Print_Letter_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'xPleaseWait.Show("Generating Letter" & vbNewLine & vbNewLine & "L o a d i n g . . . !")

        iDataSet = Letter_Data1
        If iDataSet Is Nothing Then
            MainBase.HandleDBError_OnNothingReturned()
            Exit Sub
        End If
        Me.DataAdapter = iTable
        'Dim SQL_STR As String = "SELECT L_DATE,L_REF,L_MATTER,L_H_INS_ID,L_LANG FROM LETTER_INFO WHERE REC_ID = '" & Xid & "'"
        'iTable = New OleDb.OleDbDataAdapter(SQL_STR, MainBase._data_ConStr_Sys)
        'iTable.Fill(iDataSet, "LETTERPAD")
        For Each XRow In MainBase._Letters_DBOps.GetRecord(Xid).Rows
            iDataSet.Tables("LETTERPAD").ImportRow(XRow)
        Next
        If iDataSet.Tables("LETTERPAD").Rows.Count <= 0 Then
            MsgBox(Common_Lib.Messages.SomeError, MsgBoxStyle.OkOnly)
            Return
        End If

        Dim xDate As DateTime = Nothing : xDate = iDataSet.Tables("LETTERPAD").Rows(0)("L_DATE")
        XR_L_Date.Text = "Date: " & Format(xDate, MainBase._Date_Format_Current)

        Dim XLang As String = iDataSet.Tables("LETTERPAD").Rows(0)("L_LANG").ToString

        Dim XINS_ID As String = iDataSet.Tables("LETTERPAD").Rows(0)("L_H_INS_ID").ToString

        If XINS_ID = "00001" Then : Me.XR_Logo.Image = My.Resources.INS_00001
        ElseIf XINS_ID = "00002" Then : Me.XR_Logo.Image = My.Resources.INS_00002
        ElseIf XINS_ID = "00003" Then : Me.XR_Logo.Image = My.Resources.INS_00003
        ElseIf XINS_ID = "00004" Then : Me.XR_Logo.Image = My.Resources.INS_00004
        ElseIf XINS_ID = "00005" Then : Me.XR_Logo.Image = My.Resources.INS_00005
        ElseIf XINS_ID = "00006" Then : Me.XR_Logo.Image = My.Resources.INS_00006
        Else : Me.XR_Logo.Image = Nothing : End If
        ''+ chr$(13) + chr$(10) +

        For Each XRow In MainBase._Letters_DBOps.GetInstDetails(XINS_ID).Rows
            iDataSet.Tables("INSTITUTE").ImportRow(XRow)
        Next

        For Each XRow In MainBase._Letters_DBOps.GetCenterDetails().Rows
            iDataSet.Tables("CENTRE").ImportRow(XRow)
        Next
        AddHandler Me.PrintingSystem.StartPrint, AddressOf ReportOnStartPrint

        Me.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Open, DevExpress.XtraPrinting.CommandVisibility.None)
        Me.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Save, DevExpress.XtraPrinting.CommandVisibility.None)
        Me.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Watermark, DevExpress.XtraPrinting.CommandVisibility.None)
        Me.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.Background, DevExpress.XtraPrinting.CommandVisibility.None)
        Me.PrintingSystem.SetCommandVisibility(DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground, DevExpress.XtraPrinting.CommandVisibility.None)
        Me.PrintingSystem.Document.Name = Title

        If XLang.ToUpper.Trim = "ENGLISH" Then
            Me.XR_L_Matter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf XLang.ToUpper.Trim = "HINDI" Then
            Me.XR_L_Matter.Font = New System.Drawing.Font("AkrutiDevPriyaExpanded", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Else
            Me.XR_L_Matter.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If


        If Val(Xoption) = 1 Then
            Me.PageHeader.Visible = False
            Me.XR_HO.Visible = False : Me.XR_AO.Visible = False : Me.XrPageInfo1.TopF = 2
            Me.PageFooter.HeightF = 16

        End If
        If Val(Xoption) = 3 Then
            Me.PageHeader.Visible = False
            Me.XR_HO.Visible = False : Me.XR_AO.Visible = False : Me.XrPageInfo1.TopF = 2
            Me.PageFooter.HeightF = 16
            Me.TopMargin.HeightF = XTopMargin
        End If


        'xPleaseWait.Hide()
    End Sub
    Private Sub ReportOnStartPrint(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.PrintDocumentEventArgs)
        e.PrintDocument.PrinterSettings.Copies = No_of_Copies
    End Sub

End Class