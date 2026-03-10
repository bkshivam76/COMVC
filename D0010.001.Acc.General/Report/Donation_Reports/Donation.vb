Public Class Donation

    Private centreName As String

    Private centreBKPad As String

    Private centrePadNo As String

    Private finacialYear As String

    Private dateOfDonation As String

    Private fullName As String

    Private address1 As String

    Private amountInWord As String

    Private zone As String

    Private transactionMode As String

    Private totalAmount As Double

    Private bankName As String

    Private branchName As String

    Private panNo As String

    Private officeAddress As String

    Private checkNo As String

    Private journal As Double

    Private bank As Double

    Private cash As Double

    Private city As String

    Private period As String
    Private MobileNo As String
    Private EmailID As String


    '''<remarks/>
    Public Property Mobile_No() As String
        Get
            Return Me.MobileNo
        End Get
        Set(ByVal value As String)
            Me.MobileNo = value
        End Set
    End Property

    '''<remarks/>
    Public Property Email_ID() As String
        Get
            Return Me.EmailID
        End Get
        Set(ByVal value As String)
            Me.EmailID = value
        End Set
    End Property

    '''<remarks/>
    Public Property Period_Range() As String
        Get
            Return Me.period
        End Get
        Set(ByVal value As String)
            Me.period = value
        End Set
    End Property
    '''<remarks/>
    Public Property City_Name() As String
        Get
            Return Me.city
        End Get
        Set(ByVal value As String)
            Me.city = value
        End Set
    End Property
    '''<remarks/>
    Public Property Bank_Amt() As Double
        Get
            Return Me.bank
        End Get
        Set(ByVal value As Double)
            Me.bank = value
        End Set
    End Property

    '''<remarks/>
    Public Property Cash_Amt() As Double
        Get
            Return Me.cash
        End Get
        Set(ByVal value As Double)
            Me.cash = value
        End Set
    End Property

    '''<remarks/>
    Public Property Journal_Amt() As Double
        Get
            Return Me.journal
        End Get
        Set(ByVal value As Double)
            Me.journal = value
        End Set
    End Property

    '''<remarks/>
    Public Property Check_No() As String
        Get
            Return Me.checkNo
        End Get
        Set(ByVal value As String)
            Me.checkNo = value
        End Set
    End Property

    '''<remarks/>
    Public Property Centre_Name() As String
        Get
            Return Me.centreName
        End Get
        Set(ByVal value As String)
            Me.centreName = value
        End Set
    End Property

    '''<remarks/>
    Public Property Centre_BKPad() As String
        Get
            Return Me.centreBKPad
        End Get
        Set(ByVal value As String)
            Me.centreBKPad = value
        End Set
    End Property

    '''<remarks/>
    Public Property Centre_PadNo() As String
        Get
            Return Me.centrePadNo
        End Get
        Set(ByVal value As String)
            Me.centrePadNo = value
        End Set
    End Property

    '''<remarks/>
    Public Property Finacial_Year() As String
        Get
            Return Me.finacialYear
        End Get
        Set(ByVal value As String)
            Me.finacialYear = value
        End Set
    End Property

    '''<remarks/>
    Public Property DateOf_Donation() As String
        Get
            Return Me.dateOfDonation
        End Get
        Set(ByVal value As String)
            Me.dateOfDonation = value
        End Set
    End Property

    '''<remarks/>
    Public Property Full_Name() As String
        Get
            Return Me.fullName
        End Get
        Set(ByVal value As String)
            Me.fullName = value
        End Set
    End Property

    '''<remarks/>
    Public Property Address_1() As String
        Get
            Return Me.address1
        End Get
        Set(ByVal value As String)
            Me.address1 = value
        End Set
    End Property

    '''<remarks/>
    Public Property Amount_InWord() As String
        Get
            Return Me.amountInWord
        End Get
        Set(ByVal value As String)
            Me.amountInWord = value
        End Set
    End Property

    '''<remarks/>
    Public Property ZoneName() As String
        Get
            Return Me.zone
        End Get
        Set(ByVal value As String)
            Me.zone = value
        End Set
    End Property

    '''<remarks/>
    Public Property Transaction_Mode() As String
        Get
            Return Me.transactionMode
        End Get
        Set(ByVal value As String)
            Me.transactionMode = value
        End Set
    End Property

    '''<remarks/>
    Public Property Total_Amount() As Double
        Get
            Return Me.totalAmount
        End Get
        Set(ByVal value As Double)
            Me.totalAmount = value
        End Set
    End Property

    '''<remarks/>
    Public Property Bank_Name() As String
        Get
            Return Me.bankName
        End Get
        Set(ByVal value As String)
            Me.bankName = value
        End Set
    End Property

    '''<remarks/>
    Public Property Branch_Name() As String
        Get
            Return Me.branchName
        End Get
        Set(ByVal value As String)
            Me.branchName = value
        End Set
    End Property

    '''<remarks/>
    Public Property Pan_No() As String
        Get
            Return Me.panNo
        End Get
        Set(ByVal value As String)
            Me.panNo = value
        End Set
    End Property

    '''<remarks/>
    Public Property Office_Address() As String
        Get
            Return Me.officeAddress
        End Get
        Set(ByVal value As String)
            Me.officeAddress = value
        End Set
    End Property

    Public Function AddComma(ByVal value As String) As String
        If (Not String.IsNullOrEmpty(value)) Then
            value += ", "
        End If
        Return value
    End Function

End Class
