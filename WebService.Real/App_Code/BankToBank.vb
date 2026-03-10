Imports System.Data
Imports Real.Vouchers

Namespace Real
#Region "Accounts"
    <Serializable>
    Public Class Voucher_BankToBank
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Voucher_BankToBank
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Double
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public Status_Action As String
            Public RecID As String
            Public openYearID As Integer
            Public PurposeID As String
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Parameter_Update_Voucher_BankToBank
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Double
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
            Public PurposeID As String
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
#End Region
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
        ''' <remarks>RealServiceFunctions.BankToBank_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Voucher_BankToBank, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not InParam Is Nothing Then
                If Not InParam.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.TDate), InParam.Dr_Led_ID)
                If Not InParam.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.TDate), InParam.Cr_Led_ID)
                If Not InParam.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.TDate), InParam.Sub_Cr_Led_ID)
                If Not InParam.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(InParam.TDate), InParam.Sub_Dr_Led_ID)
                'Party not Relevant in B2B
            End If

            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_SUB_CR_LED_ID,TR_DR_LED_ID,TR_SUB_DR_LED_ID,TR_MODE,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_NARRATION,TR_REMARKS,TR_REFERENCE," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & InParam.openYearID.ToString & "," & _
                                                  " " & InParam.TransCode & "," & _
                                                  "'" & InParam.VNo & "', " & _
                                                  "" & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  "'" & InParam.Type & "', " & _
                                                  "'" & InParam.Cr_Led_ID & "', " & _
                                                  "'" & InParam.Sub_Cr_Led_ID & "', " & _
                                                  "'" & InParam.Dr_Led_ID & "', " & _
                                                  "'" & InParam.Sub_Dr_Led_ID & "', " & _
                                                  "'" & InParam.Mode & "', " & _
                                                  "'" & InParam.Ref_No & "', " & _
                                                  " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & If(IsDate(InParam.Ref_ChequeDate), "'" & Convert.ToDateTime(InParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.Amount & ", " & _
                                                  "'" & InParam.Narration & "', " & _
                                                  "'" & InParam.Remarks & "', " & _
                                                  "'" & InParam.Reference & "', " & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate)
            If InParam.PurposeID.Length > 10 Then
                InsertPurpose(InParam.RecID, InParam.PurposeID, InParam.Amount, inBasicParam)
            End If

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(InParam.InsertSplVchrRefs, Nothing, InParam.RecID, Nothing, inBasicParam)
            'If InParam.InsertSplVchrRefs IsNot Nothing Then
            '    If InParam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In InParam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, InParam.RecID, InParam.Amount, inBasicParam)
            '        Next
            '    End If
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
        Public Shared Function InsertReference(Param As Parameter_InsertSplVchrRef_Vouchers, ByVal TxnID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'"
            Dim ID As String = Guid.NewGuid.ToString()
            Dim OnlineQuery As String = "INSERT INTO transaction_d_reference_info(TR_CEN_ID, TR_COD_YEAR_ID, TR_M_ID, TXN_REC_ID, TR_SR_NO, " &
                                                  "TR_VOUCHER_REF, TR_AMOUNT, REC_ID, REC_ADD_ON, REC_ADD_BY)" &
                                                  " VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  " NULL, " &
                                                  "'" & TxnID & "'," &
                                                  " NULL, " &
                                                  "'" & Param.Task_Name & "', " &
                                                  " " & Amount.ToString() & ", '" & ID & "', " & Str & ")"
            'MsgBox(OnlineQuery)
            'Console.WriteLine(OnlineQuery)
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, OnlineQuery, inBasicParam, ID, )
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
        ''' <remarks>RealServiceFunctions.BankToBank_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Voucher_BankToBank, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not UpParam Is Nothing Then
                If Not UpParam.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(UpParam.TDate), UpParam.Dr_Led_ID)
                If Not UpParam.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(UpParam.TDate), UpParam.Cr_Led_ID)
                If Not UpParam.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(UpParam.TDate), UpParam.Sub_Cr_Led_ID)
                If Not UpParam.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(UpParam.TDate), UpParam.Sub_Dr_Led_ID)
                'Party not Relevant in B2B
            End If
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_ITEM_ID     ='" & UpParam.ItemID & "', " & _
                                            " TR_TYPE        ='" & UpParam.Type & "', " & _
                                            " TR_CR_LED_ID   ='" & UpParam.Cr_Led_ID & "', " & _
                                            " TR_DR_LED_ID   ='" & UpParam.Dr_Led_ID & "', " & _
                                            " TR_SUB_CR_LED_ID  ='" & UpParam.Sub_Cr_Led_ID & "', " & _
                                            " TR_SUB_DR_LED_ID  ='" & UpParam.Sub_Dr_Led_ID & "', " & _
                                            " TR_MODE        ='" & UpParam.Mode & "', " & _
                                            " TR_REF_NO      ='" & UpParam.Ref_No & "', " & _
                                            " TR_REF_DATE    = " & If(IsDate(UpParam.Ref_Date), "'" & Convert.ToDateTime(UpParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_REF_CDATE   = " & If(IsDate(UpParam.Ref_ChequeDate), "'" & Convert.ToDateTime(UpParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AMOUNT      = " & UpParam.Amount & ", " & _
                                            " TR_NARRATION   ='" & UpParam.Narration & "', " & _
                                            " TR_REMARKS     ='" & UpParam.Remarks & "', " & _
                                            " TR_REFERENCE   ='" & UpParam.Reference & "', " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, Nothing, UpParam.TDate)

            'Purpose
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & UpParam.RecID & "'", inBasicParam)
            If UpParam.PurposeID.Length > 10 Then
                InsertPurpose(UpParam.RecID, UpParam.PurposeID, UpParam.Amount, inBasicParam)
            End If

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TXN_REC_ID = '" & UpParam.RecID & "'", inBasicParam)
            InsertSpecialVoucherReference(UpParam.UpdateSplVchrRefs, Nothing, UpParam.RecID, Nothing, inBasicParam)
            'If UpParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If UpParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In UpParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, UpParam.RecID, UpParam.Amount, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function
    End Class
#End Region
End Namespace
