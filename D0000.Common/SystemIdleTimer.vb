Imports System.ComponentModel
Imports System.Timers
Imports System.Threading
Imports System.Runtime.InteropServices

Namespace System_Idle

    Public Class SystemIdleTimer
        Inherits Component

        Private Const INTERNAL_TIMER_INTERVAL As Double = 550

        <Description("Event that if fired when idle state is entered.")> _
        Public Event OnEnterIdleState(ByVal sender As Object, ByVal e As IdleEventArgs)
        <Description("Event that is fired when leaving idle state.")> _
        Public Event OnExitIdleState(ByVal sender As Object, ByVal e As IdleEventArgs)

        Private ticker As Timers.Timer
        Private m_MaxIdleTime As Integer
        Private m_LockObject As Object
        Private m_IsIdle As Boolean = False


        <Description("Maximum idle time in seconds.")> _
        Public Property MaxIdleTime() As UInteger
            Get
                Return m_MaxIdleTime
            End Get
            Set(ByVal value As UInteger)
                If value = 0 Then
                    Throw New ArgumentException("MaxIdleTime must be larger then 0.")
                Else
                    m_MaxIdleTime = value
                End If
            End Set
        End Property
        Public Sub New()
            m_LockObject = New Object()
            ticker = New Timers.Timer(INTERNAL_TIMER_INTERVAL)
            AddHandler ticker.Elapsed, AddressOf InternalTickerElapsed
        End Sub
        Public Sub Start()
            ticker.Start()
        End Sub
        Public Sub [Stop]()
            ticker.Stop()
            SyncLock m_LockObject
                m_IsIdle = False
            End SyncLock
        End Sub
        Public ReadOnly Property IsRunning() As Boolean
            Get
                Return ticker.Enabled
            End Get
        End Property
        Private Sub InternalTickerElapsed(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
            Dim idleTime As UInteger = Win32Wrapper.GetIdle()
            If idleTime > (MaxIdleTime * 1000) Then
                If m_IsIdle = False Then
                    SyncLock m_LockObject
                        m_IsIdle = True
                    End SyncLock
                    Dim args As New IdleEventArgs(e.SignalTime)
                    RaiseEvent OnEnterIdleState(Me, args)
                End If
            Else
                If m_IsIdle Then
                    SyncLock m_LockObject
                        m_IsIdle = False
                    End SyncLock
                    Dim args As New IdleEventArgs(e.SignalTime)
                    RaiseEvent OnExitIdleState(Me, args)
                End If
            End If
        End Sub
    End Class
    Public Class IdleEventArgs
        Inherits EventArgs

        Private m_EventTime As DateTime
        Public ReadOnly Property EventTime() As DateTime
            Get
                Return m_EventTime
            End Get
        End Property
        Public Sub New(ByVal timeOfEvent As DateTime)
            m_EventTime = timeOfEvent
        End Sub
    End Class
    Public Class Win32Wrapper
        Public Structure LASTINPUTINFO
            Public cbSize As UInteger
            Public dwTime As UInteger
        End Structure

        <DllImport("User32.dll")> _
        Private Shared Function GetLastInputInfo(ByRef lii As LASTINPUTINFO) As Boolean
        End Function

        Public Shared Function GetIdle() As UInteger
            Dim lii As New LASTINPUTINFO()
            lii.cbSize = Convert.ToUInt32((Marshal.SizeOf(lii)))
            GetLastInputInfo(lii)
            Return Convert.ToUInt32(Environment.TickCount) - lii.dwTime
        End Function
    End Class

End Namespace



