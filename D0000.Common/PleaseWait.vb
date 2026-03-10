Option Strict On

Public Class PleaseWait
    Inherits DevExpress.XtraEditors.XtraForm

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        For Each ctrl As System.Windows.Forms.Control In Me.Controls
            ctrl.AccessibleDescription = ctrl.Name
        Next
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private WithEvents m_animation As ConnectOne.Animation.AVI_Animation
    Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PleaseWait))
        Me.m_animation = New ConnectOne.Animation.AVI_Animation()
        Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
        Me.SuspendLayout()
        '
        'm_animation
        '
        Me.m_animation.AVIFileType = ConnectOne.Animation.AVI_FileType.CopySettings
        resources.ApplyResources(Me.m_animation, "m_animation")
        Me.m_animation.ForeColor = System.Drawing.Color.Black
        Me.m_animation.Name = "m_animation"
        Me.m_animation.TabStop = False
        Me.m_animation.UseWaitCursor = True
        '
        'lblStatus
        '
        Me.lblStatus.AllowDrop = True
        Me.lblStatus.AllowHtmlString = True
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Appearance.Font = CType(resources.GetObject("lblStatus.Appearance.Font"), System.Drawing.Font)
        Me.lblStatus.Appearance.ForeColor = CType(resources.GetObject("lblStatus.Appearance.ForeColor"), System.Drawing.Color)
        Me.lblStatus.Appearance.Options.UseFont = True
        Me.lblStatus.Appearance.Options.UseForeColor = True
        Me.lblStatus.Appearance.Options.UseTextOptions = True
        Me.lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblStatus.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
        Me.lblStatus.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.lblStatus.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.lblStatus.AutoEllipsis = True
        Me.lblStatus.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.UseWaitCursor = True
        '
        'PleaseWait
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.BackColor = CType(resources.GetObject("PleaseWait.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.ForeColor = CType(resources.GetObject("PleaseWait.Appearance.ForeColor"), System.Drawing.Color)
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseForeColor = True
        Me.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.None
        Me.BackgroundImageStore = CType(resources.GetObject("$this.BackgroundImageStore"), System.Drawing.Image)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.m_animation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.LookAndFeel.SkinName = "Liquid Sky"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PleaseWait"
        Me.UseWaitCursor = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim MsDown As Boolean  'Mouse down for drag form
    Dim x, y As Integer

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            'Gets Mouse Position before move:
            x = MousePosition.X - DesktopLocation.X
            y = MousePosition.Y - DesktopLocation.Y
            MsDown = True
        End If
    End Sub
    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If MsDown Then
            'Set position of the form:
            SetBounds(MousePosition.X - x, MousePosition.Y - y, Width, Height)
        End If
    End Sub
    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        MsDown = False
    End Sub

    Public Overloads Sub Show(ByVal Message As String)
        lblStatus.AutoSize = True
        lblStatus.Text = Message
        Me.Show()
        Application.DoEvents()
    End Sub


    
    Private Sub PleaseWait_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''Automation Related Call  
        Common_Lib.DbOperations.TestSupport.StoreControlDetail(Me)
        ''End : Automation Related Call
    End Sub
End Class
