Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Configuration
Imports Common_Lib.Common
Imports DevExpress.XtraEditors
Public Class Cheque_Printing
    Dim PArty As String
    Dim Amount As Decimal
    Dim VchDate As Date
    Public Sub New(ByVal _Party As String, _Amount As Decimal, _VchDate As Date)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.UserSkins.BonusSkins.Register()
        PArty = _Party
        Amount = _Amount
        VchDate = _VchDate
    End Sub

    Private Sub Cheque_Printing_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

        'If (colBoxObj Is Nothing) Then
        '    ShowErrorWindow("Voucher cannot be generated", "Sorry! Voucher cannot be generated.  Please contact administrator to solve the issue.")
        '    xPleaseWait.Hide()
        '    Exit Sub
        'End If
        XrPartyName.Text = PArty
        XrAmtInDecimal.Text = Amount.ToString() + "/-"
        XrAmountInWords.Text = StrConv(changeToWords(Amount), vbProperCase)
        XrDate.Text = VchDate.ToString("dd MM yyyy")
        'xPleaseWait.Hide()
    End Sub

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

    'Sub ShowPreviewDialog()
    '    Throw New NotImplementedException
    'End Sub

End Class