<Serializable>
Public Class Messages
    Public Const CoreNotWorkingMsg = "Seems your Core File is Corrupted, Using Server DB instead. Screens may load slower."
    Public Const CouldNotContactServer = "Sorry! Please Connect Internet , If not already connected. Internet not Connected or Server Down. "
    Public Const NoCenterCreated As String = "Center Not Created yet or Server Down!!"
    Public Const SomeError As String = "Some error happened during current operation!!"
    Public Const SaveSuccess As String = "Saved Successfully!!"
    Public Const DeleteSuccess As String = "Deleted Successfully!!"
    Public Const UpdateSuccess As String = "Updated Successfully!!"
    Public Const No_InvalidData As String = "Sorry!! No or Invalid Data Present!"
    Public Const InvalidAuditorID As String = "Sorry!! Please Enter a valid Auditor ID!"
    Public Const InvalidUserID As String = "Sorry!! Please Enter a valid User ID!"
    Public Shared Function LockedSuccess(ByVal EntryCount As Integer) As String
        Return EntryCount & " Entries Locked Successfully!!"
    End Function
    Public Shared Function LockedSuccess() As String
        Return "Entry Locked Successfully!!"
    End Function
    Public Shared Function UnlockedSuccess(ByVal EntryCount As Integer) As String
        Return EntryCount & " Entries Unlocked Successfully!!"
    End Function
    Public Shared Function UnlockedSuccess() As String
        Return "Entry Unlocked Successfully!!"
    End Function
    Public Shared Function RecordChanged(ByVal ChangedReferenceDetail As String, Optional ViewStr As String = "", Optional ChangeStr As String = "", Optional recordStr As String = "") As String
        If recordStr = "" Then recordStr = "record"
        If ChangeStr = "" Then ChangeStr = "changed"
        If ViewStr = "" Then ViewStr = "change"
        Return "Sorry!! Seems that the " & recordStr & " you are trying to " & ViewStr & " or some referenced record has been " & ChangeStr & " in the meanwhile by some other user.<br><br>Please Re-try the operation again!<br><br>Changed Record : " & ChangedReferenceDetail
    End Function

    Public Shared Function DependencyChanged(ByVal ChangedReferenceDetail As String) As String
        Return "Sorry!! Seems that a selction being referred in current window has been changed in the meanwhile by some other user.<br><br>Please Re-try the operation again!<br><br>Changed Record : " & ChangedReferenceDetail
    End Function

    Public Shared Function CustomChanges(CustomMessage As String, ByVal ChangedReferenceDetail As String) As String
        Return CustomMessage & "<br><br>Please Re-try the operation again!<br><br>Changed Record : " & ChangedReferenceDetail
    End Function

    Public Class CommonTooltips_DialogResults
        Public Const ClearingDateNotfinYear As String = "Clearing Date not as per Financial Year. . . !"
        Public Const RefDateNotfinYear As String = "Reference Date not as per Financial Year. . . !"
        Public Const VchDateNotfinYear As String = "Voucher Date not as per Financial Year. . . !"
        Public Const VchDateIncorrect As String = "Voucher Date Incorrect / Blank. . . !"
        Public Const AmtNotZero As String = "Amount cannot be Zero / Negative. . . !"
        Public Const BankNotSelected As String = "Bank Not Selected. . . !"
        Public Const ItemNotSelected As String = "Item Name Not Selected. . . !"
        Public Const EntryLockedNotEditDel As String = "Locked Entry cannot be Edited / Deleted. . . !" & vbNewLine & vbNewLine & "Note:" & vbNewLine & "-------" & vbNewLine & "Drop your Request to Madhuban for Unlock this Entry," & vbNewLine & "If you really want to do some action...!"
    End Class

    Public Class CashDepositWithDrawn
        Inherits CommonTooltips_DialogResults
        Public Shared Function UsedBankAccClosed(Accno As String) As String
            Return "E n t r y   c a n n o t   b e   A d d e d  / E d i t e d /  D e l e t e d. . . !" & vbNewLine & vbNewLine & "In this entry, Used Bank A/c No.: " & Accno & " was closed...!!!"
        End Function
    End Class
End Class
