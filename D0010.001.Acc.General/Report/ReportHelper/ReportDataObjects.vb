Public Class ReportDataObjects
#Region "Start --> Report Data Objects"
    Public Class ReceiptsData
        Public Sub New(ByVal particulars As String, ByVal cash As Double, ByVal bank As Double, ByVal others As Double)
            Me._particulars = particulars
            Me._cash = cash
            Me._bank = bank
            Me._others = others
            Me._total = Me._cash + Me._bank + Me._others
        End Sub

        Public Sub New()

        End Sub

        Private _particulars As String
        Public Property Particulars() As String
            Get
                Return _particulars
            End Get
            Set(ByVal value As String)
                _particulars = value
            End Set
        End Property

        Private _cash As Double
        Public Property Cash() As Double
            Get
                Return _cash
            End Get
            Set(ByVal value As Double)
                _cash = value
            End Set
        End Property

        Private _bank As Double
        Public Property Bank() As Double
            Get
                Return _bank
            End Get
            Set(ByVal value As Double)
                _bank = value
            End Set
        End Property

        Private _others As Double
        Public Property Others() As Double
            Get
                Return _others
            End Get
            Set(ByVal value As Double)
                _others = value
            End Set
        End Property

        Private _total As Double
        Public Property Total() As Double
            Get
                Return _total
            End Get
            Set(ByVal value As Double)
                _total = value
            End Set
        End Property
    End Class

    Public Class BalanceSheetData
        Public Sub New(ByVal particulars As String, ByVal open_Qty As Double, ByVal open_Amt As Double, ByVal add_Qty As Double, ByVal add_Amt As Double, ByVal del_Qty As Double, ByVal del_Amt As Double, ByVal close_Qty As Double, ByVal close_Amt As Double)
            Me._particulars = particulars
            Me._open_Qty = open_Qty
            Me._open_Amt = open_Amt
            Me._add_Qty = add_Qty
            Me._add_Amt = add_Amt
            Me._del_Qty = del_Qty
            Me._del_Amt = del_Amt
            Me._close_Qty = close_Qty
            Me._close_Amt = close_Amt

        End Sub

        Private _particulars As String
        Public Property Particulars() As String
            Get
                Return _particulars
            End Get
            Set(ByVal value As String)
                _particulars = value
            End Set
        End Property

        Private _open_Qty As Double
        Public Property Open_Qty() As Double
            Get
                Return _open_Qty
            End Get
            Set(ByVal value As Double)
                _open_Qty = value
            End Set
        End Property

        Private _open_Amt As Double
        Public Property Open_Amt() As Double
            Get
                Return _open_Amt
            End Get
            Set(ByVal value As Double)
                _open_Amt = value
            End Set
        End Property

        Private _add_Qty As Double
        Public Property Add_Qty() As Double
            Get
                Return _add_Qty
            End Get
            Set(ByVal value As Double)
                _add_Qty = value
            End Set
        End Property

        Private _add_Amt As Double
        Public Property add_Amt() As Double
            Get
                Return _add_Amt
            End Get
            Set(ByVal value As Double)
                _add_Amt = value
            End Set
        End Property


        Private _del_Qty As String
        Public Property Del_Qty() As String
            Get
                Return _del_Qty
            End Get
            Set(ByVal value As String)
                _del_Qty = value
            End Set
        End Property


        Private _del_Amt As String
        Public Property Del_Amt() As String
            Get
                Return _del_Amt
            End Get
            Set(ByVal value As String)
                _del_Amt = value
            End Set
        End Property


        Private _close_Qty As String
        Public Property Close_Qty() As String
            Get
                Return _close_Qty
            End Get
            Set(ByVal value As String)
                _close_Qty = value
            End Set
        End Property


        Private _close_Amt As String
        Public Property Clost_Amt() As String
            Get
                Return _close_Amt
            End Get
            Set(ByVal value As String)
                _close_Amt = value
            End Set
        End Property

    End Class

    Public Class TransactionData

        Public Class Properties
            Public Const TrDate As String = "TrDate"
            Public Const Particulars As String = "Particulars"
            Public Const Type As String = "Type"
            Public Const Head As String = "Head"
            Public Const VNo As String = "VNo"
            Public Const Amt As String = "Amt"
        End Class

        Public Class ColWidth
            Public Sub New()

            End Sub
            Public Const TrDate As Integer = 100
            Public Const Particulars As Integer = 100
            Public Const Type As Integer = 100
            Public Const Head As Integer = 100
            Public Const VNo As Integer = 20
            Public Const Amt As Integer = 100
            Public Shared Function GetColWidth(ByVal Str As String) As Integer
                Dim colWidth As Integer = 0
                Select Case Str
                    Case "TrDate"
                        colWidth = TrDate
                    Case "Particulars"
                        colWidth = Particulars
                    Case "Type"
                        colWidth = Type
                    Case "Head"
                        colWidth = Head
                    Case "VNo"
                        colWidth = VNo
                    Case "Amt"
                        colWidth = Amt
                    Case Else
                        colWidth = 100
                        'TrDate + Particulars + Type + Head + VNo + Amt
                End Select
                Return colWidth
            End Function

        End Class
        Private _TrDate As String
        Public Property TrDate() As String
            Get
                Return _TrDate
            End Get
            Set(ByVal value As String)
                _TrDate = value
            End Set
        End Property


        Private _Particulars As String
        Public Property Particulars() As String
            Get
                Return _Particulars
            End Get
            Set(ByVal value As String)
                _Particulars = value
            End Set
        End Property


        Private _Type As String
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property


        Private _Head As String
        Public Property Head() As String
            Get
                Return _Head
            End Get
            Set(ByVal value As String)
                _Head = value
            End Set
        End Property


        Private _VNo As String
        Public Property VNo() As String
            Get
                Return _VNo
            End Get
            Set(ByVal value As String)
                _VNo = value
            End Set
        End Property


        Private _Amt As Integer
        Public Property Amt() As Integer
            Get
                Return _Amt
            End Get
            Set(ByVal value As Integer)
                _Amt = value
            End Set
        End Property

    End Class

    Public Class BuildingExpenseData

        Public Class Properties
            Public Const Items As String = "Items"
            Public Const Month As String = "Month"
            Public Const Amt As String = "Amt"
            Public Const MonthNo As String = "MonthNo"
        End Class

        Private _Items As String
        Public Property Items() As String
            Get
                Return _Items
            End Get
            Set(ByVal value As String)
                _Items = value
            End Set
        End Property


        Private _Month As String
        Public Property Month() As String
            Get
                Return _Month
            End Get
            Set(ByVal value As String)
                _Month = value
            End Set
        End Property


        Private _Amt As Double
        Public Property Amt() As Double
            Get
                Return _Amt
            End Get
            Set(ByVal value As Double)
                _Amt = value
            End Set
        End Property

        Private _MonthNo As Double
        Public Property MonthNo() As Double
            Get
                Return _MonthNo
            End Get
            Set(ByVal value As Double)
                _MonthNo = value
            End Set
        End Property
    End Class

    Public Class CollectionBoxData

        Public Function MapHeaders(ByVal val As String) As String
            Dim retValue As String = ""
            Select Case val
                Case "Person1"
                    retValue = "Centre-in-charge Name"
                Case "Person2"
                    retValue = "Second Surrendered Person Name"
                Case "Amount"
                    retValue = "Amount"
                Case "ReportDate"
                    retValue = "Date"
            End Select
            Return retValue
        End Function


        Private _reportDate As String
        Public Property ReportDate() As String
            Get
                Return _reportDate
            End Get
            Set(ByVal value As String)
                _reportDate = value
            End Set
        End Property

        Private _Person1 As String
        Public Property Person1() As String
            Get
                Return _Person1
            End Get
            Set(ByVal value As String)
                _Person1 = value
            End Set
        End Property


        Private _Person2 As String
        Public Property Person2() As String
            Get
                Return _Person2
            End Get
            Set(ByVal value As String)
                _Person2 = value
            End Set
        End Property


        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property



    End Class

    Public Class TransactionStatementData

        Public Class Properties
            Public Const ITEM As String = "ITEM"
            Public Const IType As String = "IType"
            Public Const IAmount As String = "IAmount"
            Public Const IGroupSum As String = "IGroupSum"
            Public Const PITEM As String = "PITEM"
            Public Const IPType As String = "IPType"
            Public Const IPAmount As String = "IPAmount"
            Public Const IPGroupSum As String = "IPGroupSum"
        End Class

        Public Class ColWidth
            Public Sub New()

            End Sub
            Public Const ITEM As Integer = 100
            Public Const IType As Integer = 100
            Public Const IAmount As Integer = 100
            Public Const IGroupSum As Integer = 100
            Public Const PITEM As Integer = 100
            Public Const IPType As Integer = 100
            Public Const IPAmount As Integer = 100
            Public Const IPGroupSum As Integer = 100
            Public Shared Function GetColWidth(ByVal Str As String) As Integer
                Dim colWidth As Integer = 0
                Select Case Str
                    Case "ITEM"
                        colWidth = ITEM
                    Case "IType"
                        colWidth = IType
                    Case "IAmount"
                        colWidth = IAmount
                    Case "IGroupSum"
                        colWidth = IGroupSum
                    Case "PITEM"
                        colWidth = PITEM
                    Case "IPType"
                        colWidth = IPType
                    Case "IPAmount"
                        colWidth = IPAmount
                    Case "IPGroupSum"
                        colWidth = IPGroupSum
                    Case Else
                        colWidth = 100
                        'TrDate + Particulars + Type + Head + VNo + Amt
                End Select
                Return colWidth
            End Function

        End Class
        Private _ITEM As String
        Public Property ITEM() As String
            Get
                Return _ITEM
            End Get
            Set(ByVal value As String)
                _ITEM = value
            End Set
        End Property

        Private _IType As String
        Public Property IType() As String
            Get
                Return _IType
            End Get
            Set(ByVal value As String)
                _IType = value
            End Set
        End Property

        Private _IAmount As String
        Public Property IAmount() As String
            Get
                Return _IAmount
            End Get
            Set(ByVal value As String)
                _IAmount = value
            End Set
        End Property

        Private _IGroupSum As String
        Public Property IGroupSum() As String
            Get
                Return _IGroupSum
            End Get
            Set(ByVal value As String)
                _IGroupSum = value
            End Set
        End Property

        Private _PITEM As String
        Public Property PITEM() As String
            Get
                Return _PITEM
            End Get
            Set(ByVal value As String)
                _PITEM = value
            End Set
        End Property

        Private _IPType As String
        Public Property IPType() As String
            Get
                Return _IPType
            End Get
            Set(ByVal value As String)
                _IPType = value
            End Set
        End Property

        Private _IPAmount As String
        Public Property IPAmount() As String
            Get
                Return _IPAmount
            End Get
            Set(ByVal value As String)
                _IPAmount = value
            End Set
        End Property

        Private _IPGroupSum As String
        Public Property IPGroupSum() As String
            Get
                Return _IGroupSum
            End Get
            Set(ByVal value As String)
                _IPGroupSum = value
            End Set
        End Property

    End Class

    Public Class BalanceDetails

        Private _OpeningBalance As Decimal
        Public Property OpeningBalance() As Decimal
            Get
                Return _OpeningBalance
            End Get
            Set(ByVal value As Decimal)
                _OpeningBalance = value
            End Set
        End Property


        Private _Receipt As Decimal
        Public Property Receipt() As Decimal
            Get
                Return _Receipt
            End Get
            Set(ByVal value As Decimal)
                _Receipt = value
            End Set
        End Property


        Private _Payment As Decimal
        Public Property Payment() As Decimal
            Get
                Return _Payment
            End Get
            Set(ByVal value As Decimal)
                _Payment = value
            End Set
        End Property

        Private _ClosingBalance As Decimal
        Public Property ClosingBalance() As Decimal
            Get
                Return _ClosingBalance
            End Get
            Set(ByVal value As Decimal)
                _ClosingBalance = value
            End Set
        End Property

    End Class

    Public Class CollectionBoxVoucherReport
        Private centreUIDNo As String
        Private centreName As String
        Private zone As String
        Private dateOfCollectionBox As String
        Private person1Name As String
        Private person2Name As String
        Private amountInWord As String
        Private totalAmount As Double
        Private voucherNo As String
        Private itemNo As String
        Private accountHead As String
        Private insName As String
        Private Vch_narration As String

#Region "properties"

        '''<remarks/>
        Public Property Ins_Name() As String
            Get
                Return Me.insName
            End Get
            Set(ByVal value As String)
                Me.insName = value
            End Set
        End Property
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property narration() As String
            Get
                Return Me.Vch_narration
            End Get
            Set(ByVal value As String)
                Me.Vch_narration = value
            End Set
        End Property
        '''<remarks/>
        Public Property Account_Head() As String
            Get
                Return Me.accountHead
            End Get
            Set(ByVal value As String)
                Me.accountHead = value
            End Set
        End Property

        '''<remarks/>
        Public Property Item_No() As String
            Get
                Return Me.itemNo
            End Get
            Set(ByVal value As String)
                Me.itemNo = value
            End Set
        End Property

        '''<remarks/>
        Public Property Voucher_No() As String
            Get
                Return Me.voucherNo
            End Get
            Set(ByVal value As String)
                Me.voucherNo = value
            End Set
        End Property

        '''<remarks/>
        Public Property Centre_UIDNo() As String
            Get
                Return Me.centreUIDNo
            End Get
            Set(ByVal value As String)
                Me.centreUIDNo = value
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
        Public Property Zone_Name() As String
            Get
                Return Me.zone
            End Get
            Set(ByVal value As String)
                Me.zone = value
            End Set
        End Property


        '''<remarks/>
        Public Property DateOf_CollectionBox() As String
            Get
                Return Me.dateOfCollectionBox
            End Get
            Set(ByVal value As String)
                Me.dateOfCollectionBox = value
            End Set
        End Property


        '''<remarks/>
        Public Property Person1_Name() As String
            Get
                Return Me.person1Name
            End Get
            Set(ByVal value As String)
                Me.person1Name = value
            End Set
        End Property


        '''<remarks/>
        Public Property Person2_Name() As String
            Get
                Return Me.person2Name
            End Get
            Set(ByVal value As String)
                Me.person2Name = value
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
        Public Property Total_Amount() As Double
            Get
                Return Me.totalAmount
            End Get
            Set(ByVal value As Double)
                Me.totalAmount = value
            End Set
        End Property
#End Region



    End Class

#End Region
End Class
