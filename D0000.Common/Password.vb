'SQL
Imports System.Text
'Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Collections.Specialized
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.Net.Mail
Imports System.Data
Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
#Region "--Options--"
    <Serializable>
    Public Class ChangePassword
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetPassword_Role(ByVal UserID As String) As DataTable
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Return _cUser.GetList(UserID, ClientScreen.Options_ChangePassword)
        End Function

        'Shifted
        Public Function GetPassword_Role(ByVal CenID As String, ByVal UserID As String) As DataTable
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Return _cUser.GetList(UserID, CenID, ClientScreen.Options_ChangePassword)
        End Function

        'Shifted
        Public Function ChangePassword(ByVal CenID As Integer, ByVal UserID As String, ByVal Password As String) As Boolean
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Dim CPwd As Parameter_CPwd_ClientUserInfo = New Parameter_CPwd_ClientUserInfo()
            CPwd.Cen_Id = CenID
            CPwd.UserID = UserID
            CPwd.Cen_Password = Password
            CPwd.screen = ClientScreen.Options_ChangePassword

            Return _cUser.ChangePassword(CPwd)
        End Function
    End Class
    <Serializable>
    Public Class ResetPassword
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetUserCount(ByVal CenID As Integer) As Object
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Return _cUser.GetUserCount(CenID, ClientScreen.Options_ResetPassword)
        End Function

        'Shifted
        Public Function GetMaxUserID(ByVal CenID As Integer) As Object
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Return _cUser.GetMaxUserID(CenID, ClientScreen.Options_ResetPassword)
        End Function

        'Shifted
        Public Function GetOriginalPassword(ByVal CenID As String) As Object
            Return GetOrgPasswordForCenID(CenID, ClientScreen.Options_ResetPassword)
        End Function

        'Shifted
        Public Function GetCenIDForBKPadNo(ByVal BKPad As String) As DataTable
            Return GetCenIDForBKPad(BKPad, ClientScreen.Options_ResetPassword)
        End Function

        'Shifted
        Public Function GetCenterDetails(ByVal BKPad As String, ByVal SelectedCenID As String) As DataTable
            Dim LocalQuery As String = " SELECT I.INS_NAME AS [Institution Name],C.CEN_UID AS [UID],C.CEN_ID AS [ID],CEN_BK_PAD_NO AS [BK PAD], " &
                                 " (SELECT CEN_NAME FROM CENTRE_INFO WHERE CEN_BK_PAD_NO=C.CEN_BK_PAD_NO AND CEN_MAIN=-1) as [Centre Name]," &
                                 " (SELECT CEN_ID   FROM CENTRE_INFO WHERE CEN_BK_PAD_NO=C.CEN_BK_PAD_NO AND CEN_MAIN=-1) as [Cen_ID_Main] " &
                                 " FROM Centre_Info AS C INNER JOIN Institute_Info AS I ON C.CEN_INS_ID = I.INS_ID " &
                                 " WHERE C.REC_STATUS IN (0,1,2) " &
                                 " AND C.CEN_BK_PAD_NO = '" & BKPad & "' " &
                                 " AND C.CEN_ID IN (" & SelectedCenID & ") " &
                                 " ORDER BY C.CEN_BK_PAD_NO, C.cen_ins_id, C.CEN_UID"

            Dim inParam As Param_GetCenterDetailsByQuery_Common = New Param_GetCenterDetailsByQuery_Common()
            inParam.BKPad = BKPad
            inParam.SelectedCenID = SelectedCenID
            Return GetCenterDetailsByQuery(LocalQuery, ClientScreen.Options_ResetPassword, inParam)
        End Function

        'Shifted
        Public Function GetCreatedCenterIds(ByVal SelectedCenID As String) As DataTable
            Dim _cod As CodInfo = New CodInfo(cBase)
            Return _cod.GetCreatedCentersFromSelected(SelectedCenID)
        End Function

        'Shifted
        Public Function GetUsers(ByVal CenID As Integer) As DataTable
            Dim _cUser As ClientUserInfo = New ClientUserInfo(cBase)
            Return _cUser.GetList(ClientScreen.Options_ResetPassword, CenID)
        End Function
    End Class
#End Region

    <Serializable>
    Public Class Password

        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Enum Password_Type
            Only_Numeric = 1
            Only_Aplhabets = 2
            Only_Aplha_Numeric = 3
        End Enum

#Region "Functions"

        'Generate Encryption / Decryption Base Password
        '------------------------------------------------------------< Start
        'Example                 
        'SecretKey = "Any Define Secret Key"
        'EPassword = psDecrypt("ABC")
        'DPassword = psDecrypt(EPassword)
        Private Shared lbtVector() As Byte = {240, 3, 45, 29, 0, 76, 173, 59}
        Public Shared SecretKey As String ' = "secret string"
        Public Shared Function psDecrypt(ByVal sQueryString As String) As String
            Dim buffer() As Byte
            Dim loCryptoClass As New TripleDESCryptoServiceProvider
            Dim loCryptoProvider As New MD5CryptoServiceProvider
            Try
                buffer = Convert.FromBase64String(sQueryString)
                loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(SecretKey))
                loCryptoClass.IV = lbtVector
                'Return Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
                psDecrypt = Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
            Catch ex As Exception
                'Throw ex
                psDecrypt = ""
            Finally
                loCryptoClass.Clear()
                loCryptoProvider.Clear()
                loCryptoClass = Nothing
                loCryptoProvider = Nothing
            End Try
        End Function
        Public Shared Function psEncrypt(ByVal sInputVal As String) As String
            Dim loCryptoClass As New TripleDESCryptoServiceProvider
            Dim loCryptoProvider As New MD5CryptoServiceProvider
            Dim lbtBuffer() As Byte
            Try
                lbtBuffer = System.Text.Encoding.ASCII.GetBytes(sInputVal)
                loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(SecretKey))
                loCryptoClass.IV = lbtVector
                sInputVal = Convert.ToBase64String(loCryptoClass.CreateEncryptor().TransformFinalBlock(lbtBuffer, 0, lbtBuffer.Length()))
                psEncrypt = sInputVal
            Catch ex As CryptographicException
                'Throw ex
                psEncrypt = ""
            Catch ex As FormatException
                'Throw ex
                psEncrypt = ""
            Catch ex As Exception
                'Throw ex
                psEncrypt = ""
            Finally
                loCryptoClass.Clear()
                loCryptoProvider.Clear()
                loCryptoClass = Nothing
                loCryptoProvider = Nothing
            End Try
        End Function
        ''------------------------------------------------------------< End


        'Generate SimplePassword --< Start
        '----------------------------------------------------------
        'Usage:    outputstring = GeneratePassword(CInt(enterlength))
        '----------------------------------------------------------
        Public Function GeneratePassword(ByVal Length As Integer, ByVal PassType As Password_Type) As String
            Dim RndString As String = String.Empty
            Dim Zero, Nine, A, Z, Count, RandNum As Integer
            Dim oRandom As New Random(System.DateTime.Now.Millisecond)
            Zero = Asc("0") : Nine = Asc("9") : A = Asc("A") : Z = Asc("Z")
            If Length <= 0 Then Length = 8
            While (Count < Length)
                RandNum = oRandom.Next(Zero, Z)
                If PassType = Password_Type.Only_Numeric Then
                    If ((RandNum >= Zero) And (RandNum <= Nine)) Then
                        RndString = RndString + Chr(RandNum) : Count = Count + 1
                    End If
                End If
                If PassType = Password_Type.Only_Aplhabets Then
                    If ((RandNum >= A) And (RandNum <= Z)) Then
                        RndString = RndString + Chr(RandNum) : Count = Count + 1
                    End If
                End If
                If PassType = Password_Type.Only_Aplha_Numeric Then
                    If (((RandNum >= Zero) And (RandNum <= Nine) Or (RandNum >= A) And (RandNum <= Z))) Then
                        RndString = RndString + Chr(RandNum) : Count = Count + 1
                    End If
                End If
            End While
            Return RndString
        End Function
        'Generate SimplePassword --< End


        'Generate StrongPassword --< Start
        '---------------------------------------------------------
        'Usage:  outputstring= Generate(8, 10)
        '---------------------------------------------------------
        Private Shared DEFAULT_MIN_PASSWORD_LENGTH As Integer = 8
        Private Shared DEFAULT_MAX_PASSWORD_LENGTH As Integer = 10
        Private Shared PASSWORD_CHARS_LCASE As String = "abcdefgijkmnopqrstwxyz"
        Private Shared PASSWORD_CHARS_UCASE As String = "ABCDEFGHJKLMNPQRSTWXYZ"
        Private Shared PASSWORD_CHARS_NUMERIC As String = "0123456789"
        Private Shared PASSWORD_CHARS_SPECIAL As String = "*$-+?_&=!%{}/"
        Public Shared Function Generate() As String
            Generate = Generate(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH)
        End Function
        Public Shared Function Generate(ByVal length As Integer) As String
            Generate = Generate(length, length)
        End Function
        Public Shared Function Generate(ByVal minLength As Integer, ByVal maxLength As Integer) As String
            ' Make sure that input parameters are valid.
            If (minLength <= 0 Or maxLength <= 0 Or minLength > maxLength) Then
                Generate = Nothing
            End If

            ' Create a local array containing supported password characters
            ' grouped by types. You can remove character groups from this
            ' array, but doing so will weaken the password strength.
            Dim charGroups As Char()() = New Char()() _
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray
        }

            ' Use this array to track the number of unused characters in each
            ' character group.
            Dim charsLeftInGroup As Integer() = New Integer(charGroups.Length - 1) {}

            ' Initially, all characters in each group are not used.
            Dim I As Integer
            For I = 0 To charsLeftInGroup.Length - 1
                charsLeftInGroup(I) = charGroups(I).Length
            Next
            ' Use this array to track (iterate through) unused character groups.
            Dim leftGroupsOrder As Integer() = New Integer(charGroups.Length - 1) {}
            ' Initially, all character groups are not used.
            For I = 0 To leftGroupsOrder.Length - 1
                leftGroupsOrder(I) = I
            Next
            ' Because we cannot use the default randomizer, which is based on the
            ' current time (it will produce the same "random" number within a
            ' second), we will use a random number generator to seed the
            ' randomizer.
            ' Use a 4-byte array to fill it with random bytes and convert it then
            ' to an integer value.
            Dim randomBytes As Byte() = New Byte(3) {}
            ' Generate 4 random bytes.
            Dim rng As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
            rng.GetBytes(randomBytes)
            ' Convert 4 bytes into a 32-bit integer value.
            Dim seed As Integer = ((randomBytes(0) And &H7F) << 24 Or
                                randomBytes(1) << 16 Or
                                randomBytes(2) << 8 Or
                                randomBytes(3))

            ' Now, this is real randomization.
            Dim random As Random = New Random(seed)

            ' This array will hold password characters.
            Dim password As Char() = Nothing

            ' Allocate appropriate memory for the password.
            If (minLength < maxLength) Then
                password = New Char(random.Next(minLength - 1, maxLength)) {}
            Else
                password = New Char(minLength - 1) {}
            End If

            ' Index of the next character to be added to password.
            Dim nextCharIdx As Integer

            ' Index of the next character group to be processed.
            Dim nextGroupIdx As Integer

            ' Index which will be used to track not processed character groups.
            Dim nextLeftGroupsOrderIdx As Integer

            ' Index of the last non-processed character in a group.
            Dim lastCharIdx As Integer

            ' Index of the last non-processed group.
            Dim lastLeftGroupsOrderIdx As Integer = leftGroupsOrder.Length - 1

            ' Generate password characters one at a time.
            For I = 0 To password.Length - 1
                ' If only one character group remained unprocessed, process it;
                ' otherwise, pick a random character group from the unprocessed
                ' group list. To allow a special character to appear in the
                ' first position, increment the second parameter of the Next
                ' function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                If (lastLeftGroupsOrderIdx = 0) Then
                    nextLeftGroupsOrderIdx = 0
                Else
                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx)
                End If

                ' Get the actual index of the character group, from which we will
                ' pick the next character.
                nextGroupIdx = leftGroupsOrder(nextLeftGroupsOrderIdx)

                ' Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup(nextGroupIdx) - 1

                ' If only one unprocessed character is left, pick it; otherwise,
                ' get a random character from the unused character list.
                If (lastCharIdx = 0) Then
                    nextCharIdx = 0
                Else
                    nextCharIdx = random.Next(0, lastCharIdx + 1)
                End If

                ' Add this character to the password.
                password(I) = charGroups(nextGroupIdx)(nextCharIdx)

                ' If we processed the last character in this group, start over.
                If (lastCharIdx = 0) Then
                    charsLeftInGroup(nextGroupIdx) =
                                charGroups(nextGroupIdx).Length
                    ' There are more unprocessed characters left.
                Else
                    ' Swap processed character with the last unprocessed character
                    ' so that we don't pick it until we process all characters in
                    ' this group.
                    If (lastCharIdx <> nextCharIdx) Then
                        Dim temp As Char = charGroups(nextGroupIdx)(lastCharIdx)
                        charGroups(nextGroupIdx)(lastCharIdx) =
                                charGroups(nextGroupIdx)(nextCharIdx)
                        charGroups(nextGroupIdx)(nextCharIdx) = temp
                    End If

                    ' Decrement the number of unprocessed characters in
                    ' this group.
                    charsLeftInGroup(nextGroupIdx) =
                           charsLeftInGroup(nextGroupIdx) - 1
                End If

                ' If we processed the last group, start all over.
                If (lastLeftGroupsOrderIdx = 0) Then
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1
                    ' There are more unprocessed groups left.
                Else
                    ' Swap processed group with the last unprocessed group
                    ' so that we don't pick it until we process all groups.
                    If (lastLeftGroupsOrderIdx <> nextLeftGroupsOrderIdx) Then
                        Dim temp As Integer =
                                leftGroupsOrder(lastLeftGroupsOrderIdx)
                        leftGroupsOrder(lastLeftGroupsOrderIdx) =
                                leftGroupsOrder(nextLeftGroupsOrderIdx)
                        leftGroupsOrder(nextLeftGroupsOrderIdx) = temp
                    End If

                    ' Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx = lastLeftGroupsOrderIdx - 1
                End If
            Next

            ' Convert password characters into a string and return the result.
            Generate = New String(password)
        End Function
        'Generate StrongPassword --< End

#End Region

    End Class
End Class
