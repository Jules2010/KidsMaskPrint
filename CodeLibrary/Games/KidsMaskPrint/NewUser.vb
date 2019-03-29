Imports System.Drawing.Drawing2D
Friend Class NewUser
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mUsers(0) As String
    Dim mSelectedUser As String
    Friend Property SelectedUser() As String
        Get
            Return mSelectedUser
        End Get
        Set(ByVal Value As String)
            mSelectedUser = Value
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
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As WinOnly.BevelButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblWhatsName As System.Windows.Forms.Label
    Friend WithEvents btnHelp As WinOnly.BevelButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(NewUser))
        Me.lblWhatsName = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.btnOK = New WinOnly.BevelButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnHelp = New WinOnly.BevelButton
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblWhatsName
        '
        Me.lblWhatsName.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblWhatsName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWhatsName.Location = New System.Drawing.Point(8, 8)
        Me.lblWhatsName.Name = "lblWhatsName"
        Me.lblWhatsName.Size = New System.Drawing.Size(264, 32)
        Me.lblWhatsName.TabIndex = 0
        Me.lblWhatsName.Text = "Type your name / nickname"
        Me.lblWhatsName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(72, 48)
        Me.txtName.MaxLength = 10
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(160, 29)
        Me.txtName.TabIndex = 0
        Me.txtName.Text = ""
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnOK.BackColor = System.Drawing.Color.Red
        Me.btnOK.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnOK.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.Gold
        Me.btnOK.Location = New System.Drawing.Point(184, 88)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(88, 40)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        '
        'Panel1
        '
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnHelp, Me.btnOK, Me.lblWhatsName, Me.txtName})
        Me.Panel1.Location = New System.Drawing.Point(8, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(280, 136)
        Me.Panel1.TabIndex = 22
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnHelp.BackColor = System.Drawing.Color.Red
        Me.btnHelp.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHelp.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.Color.Gold
        Me.btnHelp.Location = New System.Drawing.Point(8, 88)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(88, 40)
        Me.btnHelp.TabIndex = 2
        Me.btnHelp.Text = "&Help"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Bitmap)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(280, 32)
        Me.PictureBox1.TabIndex = 23
        Me.PictureBox1.TabStop = False
        '
        'NewUser
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(298, 192)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.PictureBox1, Me.Panel1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "NewUser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Welcome"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim mbooKeyStroke As Boolean
    Dim mbooIgnoreChanges As Boolean

    Private Sub NewUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'check DAT file for users, if no uses don't show buttons
        Me.SuspendLayout()

        AddDebugComment("NewUser.NewUser_Load - start")

        Me.Text = NameMe("Welcome")

        Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
        Dim lstrUsersStr As String

        With InitialConfig

            lstrUsersStr = .GetValue("Users", "")

        End With
        InitialConfig = Nothing

        If lstrUsersStr <> "" Then
            lstrUsersStr = ReplaceAll(lstrUsersStr, ChrGet(255) & ChrGet(255), ChrGet(255))

            If RightGet(lstrUsersStr, 1) = ChrGet(255) Then
                lstrUsersStr = lstrUsersStr.Remove(lstrUsersStr.Length - 1, 1)
            End If
        End If

        If lstrUsersStr <> "" Then
            mUsers = Microsoft.VisualBasic.Split(lstrUsersStr, ChrGet(255))
        End If

        AddDebugComment("NewUser.NewUser_Load - end")

        Me.ResumeLayout()

    End Sub
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        AddDebugComment("NewUser.btnOK_Click - start")

        If txtName.Text.Trim <> "" Then
            Dim lintArrInc As Integer
            If mUsers(0) = "" And mUsers.GetUpperBound(0) = 0 Then
                '
            Else

                For lintArrInc = 0 To mUsers.GetUpperBound(0)
                    If mUsers(lintArrInc).ToUpper = txtName.Text.Trim.ToUpper Then
                        mSelectedUser = txtName.Text.Trim.Replace(" ", "_")
                        Me.Close()
                        Exit Sub
                    End If
                Next lintArrInc
            End If
            If mUsers(0) <> "" Then
                ReDim Preserve mUsers(mUsers.GetUpperBound(0) + 1)
            End If

            mUsers(mUsers.GetUpperBound(0)) = txtName.Text.Trim.Replace(" ", "_")
            mSelectedUser = txtName.Text.Trim.Replace(" ", "_")

        Else
            Exit Sub
        End If

        UIStyle.gPaintClr1 = Color.Empty
        UIStyle.gPaintClr2 = Color.Empty

        AddDebugComment("NewUser.btnOK_Click - 1")

        SaveSetting("Colours", "One", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)

        Dim lstrUsersStr As String = Microsoft.VisualBasic.Join(mUsers, ChrGet(255))
        SaveSetting("Users", lstrUsersStr, InitalXMLConfig.XmlConfigType.AppSettings, "")
        Me.Close()

        AddDebugComment("NewUser.btnOK_Click - end")

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("NewUser.btnHelp_Click")

        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.SignIn))

    End Sub
    Private Sub NewUser_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate()
    End Sub
    Private Sub txtName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnOK_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress

        If Char.IsLetterOrDigit(e.KeyChar) = False AndAlso Convert.ToInt32(e.KeyChar) <> 8 Then
            e.Handled = True
        Else
            mbooKeyStroke = True
        End If

    End Sub
    Private Sub txtName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.TextChanged

        If Not mbooKeyStroke AndAlso Not mbooIgnoreChanges Then
            ' text changed by some event other than a keystroke.
            Dim strNewText As String = String.Empty
            Dim ch As Char
            For Each ch In txtName.Text
                If Char.IsLetterOrDigit(ch) = False Then
                    ' do nothing
                Else
                    strNewText &= ch
                End If
            Next
            mbooIgnoreChanges = True
            txtName.Text = strNewText
            mbooIgnoreChanges = False
        End If
        mbooKeyStroke = False

    End Sub
End Class
