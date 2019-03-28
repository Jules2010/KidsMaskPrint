Friend Class DataFileAcceptReg

    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mstrDataFileDesc As String
    Friend WriteOnly Property DataFileDesc() As String
        Set(ByVal Value As String)

            mstrDataFileDesc = Value
        End Set
    End Property
    Dim mstrCaption As String
    Friend WriteOnly Property Caption() As String
        Set(ByVal Value As String)
            mstrCaption = Value
        End Set
    End Property
    Friend Property LicenseCode() As String
        Get
            Return txtRegCode.Text
        End Get
        Set(ByVal Value As String)
            'mstrLicenseCode = Value
        End Set
    End Property
    Dim mstrSerialCode As String
    Friend WriteOnly Property SerialCode() As String
        Set(ByVal Value As String)
            mstrSerialCode = Value
        End Set
    End Property
    Friend Enum eButtonType
        normal
        BevelRed
    End Enum
    Dim mButtonType As eButtonType
    Friend Property ButtonType() As eButtonType
        Get
            Return mButtonType
        End Get
        Set(ByVal Value As eButtonType)
            mButtonType = Value
        End Set
    End Property
#End Region

#Region " Windows Form Designer generated code "

    Friend Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblRegisterInstructions As WinOnly.RichTextBoxLabel
    Friend WithEvents txtRegCode As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As WinOnly.BevelButton
    Friend WithEvents btnCancel As WinOnly.BevelButton
    Friend WithEvents btnCopyCode As WinOnly.BevelButton
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblRegisterInstructions = New WinOnly.RichTextBoxLabel()
        Me.txtRegCode = New System.Windows.Forms.TextBox()
        Me.btnOK = New WinOnly.BevelButton()
        Me.btnCancel = New WinOnly.BevelButton()
        Me.btnCopyCode = New WinOnly.BevelButton()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'lblRegisterInstructions
        '
        Me.lblRegisterInstructions.BackColor = System.Drawing.Color.Azure
        Me.lblRegisterInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblRegisterInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRegisterInstructions.Location = New System.Drawing.Point(8, 8)
        Me.lblRegisterInstructions.Name = "lblRegisterInstructions"
        Me.lblRegisterInstructions.Size = New System.Drawing.Size(424, 168)
        Me.lblRegisterInstructions.TabIndex = 0
        Me.lblRegisterInstructions.Text = "Instructions"
        '
        'txtRegCode
        '
        Me.txtRegCode.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtRegCode.Location = New System.Drawing.Point(8, 232)
        Me.txtRegCode.Multiline = True
        Me.txtRegCode.Name = "txtRegCode"
        Me.txtRegCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRegCode.Size = New System.Drawing.Size(422, 200)
        Me.txtRegCode.TabIndex = 1
        Me.txtRegCode.Text = ""
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(272, 440)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(352, 440)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        '
        'btnCopyCode
        '
        Me.btnCopyCode.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnCopyCode.Location = New System.Drawing.Point(352, 192)
        Me.btnCopyCode.Name = "btnCopyCode"
        Me.btnCopyCode.Size = New System.Drawing.Size(80, 23)
        Me.btnCopyCode.TabIndex = 0
        Me.btnCopyCode.Text = "C&opy code"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.Azure
        Me.LinkLabel1.Location = New System.Drawing.Point(16, 152)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(376, 16)
        Me.LinkLabel1.TabIndex = 7
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "http://www.KidsMaskPrint.com/buy.php"
        '
        'DataFileAcceptReg
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(440, 494)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.LinkLabel1, Me.btnCopyCode, Me.btnOK, Me.btnCancel, Me.txtRegCode, Me.lblRegisterInstructions})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(392, 336)
        Me.Name = "DataFileAcceptReg"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AcceptReg"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub AcceptReg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetupButtons()

        Me.Text = mstrCaption

        Dim RtfCode As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang2057{\fonttbl{\f0\fswiss\fprq2\fcharset0 Microsoft Sans Serif;}{\f1\fswiss\fcharset0 Arial;}}" & CR() & _
            "{\*\generator Msftedit 5.41.15.1503;}\viewkind4\uc1\pard\f0\fs16" & CR() & _
            "To purchase the '[DataFileDesc]' you \b MUST \b0 enter the Serial number shown below into our Order form.\fs20\par" & CR() & _
            "\par" & CR() & _
            "\b\fs28 [SerialCode]\b0\fs20\par" & CR() & _
            "\par" & CR() & _
            "\fs16Once you have ordered the '[DataFileDesc]' you will get a license code, which you must paste into the box below (using Ctrl+V) and then click OK!" & CR() & _
            "\par" & CR() & _
            "\par" & CR() & _
            "Our order form can be found at:-\f1\fs20\par" & _
            "}"

        RtfCode = RtfCode.Replace("[SerialCode]", mstrSerialCode)
        RtfCode = RtfCode.Replace("[DataFileDesc]", mstrDataFileDesc)

        lblRegisterInstructions.Rtf = RtfCode

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)
        ' lblRegisterInstructions.BackgroundImage = PaintBack.PaintBackgroundToFit(pevent, Me.Height, Me.Width, lblRegisterInstructions.Top, lblRegisterInstructions.Left, lblRegisterInstructions.Width, lblRegisterInstructions.Height)
        'Me.Update()

    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked

        BrowseToUrl("http://www.KidsMaskPrint.com/buy.php", Me)

    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If txtRegCode.Text = "" Then
            Exit Sub
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub
    Private Sub btnCopyCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyCode.Click

        Clipboard.SetDataObject(mstrSerialCode, True)
        MessageBox.Show("The serial number has been added to the clipboard!", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub
    Private Function SetupButtons()

        Select Case mButtonType
            Case eButtonType.BevelRed
                Dim BevFont As New Font(New FontFamily("Arial"), 11, FontStyle.Bold)
                With btnCopyCode
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(104, 40)
                    .Left = 328
                    .Top = 184
                End With
                With btnOK
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(88, 40)
                    .Left = 248
                    .Top = 440
                End With
                With btnCancel
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .Top = 176 + 32
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(88, 40)
                    .Left = 344
                    .Top = 440
                End With
            Case eButtonType.normal
                '--- 
                If IsAboveOrEqualWinXp() = True Then
                    btnOK.FlatStyle = FlatStyle.System
                    btnCancel.FlatStyle = FlatStyle.System
                    btnCopyCode.FlatStyle = FlatStyle.System
                End If
                '--- 
                btnCopyCode.BackColor = Color.FromArgb(0, btnCopyCode.BackColor)
                btnOK.BackColor = Color.FromArgb(0, btnOK.BackColor)
                btnCancel.BackColor = Color.FromArgb(0, btnCancel.BackColor)
        End Select

    End Function
    Private Sub DataFileAcceptReg_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub
    Private Sub DataFileAcceptReg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub txtRegCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegCode.KeyDown
        
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Enter Then
            btnOK_Click(Nothing, Nothing)
        End If

    End Sub
End Class
