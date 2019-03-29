Friend Class UsersGeneral
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Friend Enum UserTranType
        Delete
        Rename
    End Enum
    Private mTranType As UserTranType
    Friend Property TranType() As UserTranType
        Get
            Return mTranType
        End Get
        Set(ByVal Value As UserTranType)
            mTranType = Value
        End Set
    End Property
    Private mLoginInAs As String
    Friend Property LoginInAs() As String
        Get
            Return mLoginInAs
        End Get
        Set(ByVal Value As String)
            mLoginInAs = Value
        End Set
    End Property

#End Region

#Region " Windows Form Designer generated code "

    Friend Sub New()
        MyBase.New()

        InitializeComponent()

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
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(24, 24)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(136, 121)
        Me.ListBox1.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(8, 160)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(96, 160)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        '
        'UsersGeneral
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(184, 190)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCancel, Me.btnOK, Me.ListBox1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "UsersGeneral"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "UsersGeneral"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub UsersGeneral_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("UsersGeneral.UsersGeneral_Load - start " & mTranType)

        SetBackcolors()

        Select Case mTranType
            Case UserTranType.Delete
                Me.Text = "Delete"
            Case UserTranType.Rename
                Me.Text = "Rename"
        End Select

        If IsAboveOrEqualWinXp() = True Then
            btnOK.FlatStyle = FlatStyle.System
            btnCancel.FlatStyle = FlatStyle.System
        End If

        Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
        Dim lstrUsers() As String

        With InitialConfig

            lstrUsers = Microsoft.VisualBasic.Split(.GetValue("Users", ""), ChrGet(255))
            ListBox1.Items.AddRange(lstrUsers)
        End With

        InitialConfig = Nothing

        AddDebugComment("UsersGeneral.UsersGeneral_Load - end")

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        AddDebugComment("UsersGeneral.btnOK_Click - start")

        Dim lintArrInc As Integer

        If ListBox1.Text <> "" Then
            Select Case mTranType
                Case UserTranType.Delete
                    Dim dlgResult As DialogResult
                    dlgResult = MessageBox.Show("Do you want to delete user '" & ListBox1.Text & "' ?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dlgResult = DialogResult.Yes Then
                        Busy(Me, True)
                        'mTranUserName = ListBox1.Text
                        For lintArrInc = 0 To 5
                            SaveSetting("MaskFile" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, ListBox1.Text)
                            SaveSetting("MaskFileDesc" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, ListBox1.Text)
                        Next lintArrInc
                        SaveSetting("LastSaved", "", InitalXMLConfig.XmlConfigType.UserSettings, ListBox1.Text)

                        Dim lstrAllUsers As String = GetSetting("Users", "", InitalXMLConfig.XmlConfigType.AppSettings, "") & ChrGet(255)
                        SaveSetting("Users", lstrAllUsers.Replace(ListBox1.Text & ChrGet(255), "").Replace(ChrGet(255) & _
                            ChrGet(255), ChrGet(255)), InitalXMLConfig.XmlConfigType.AppSettings, "")

                        Busy(Me, False)
                    End If
                Case UserTranType.Rename
                    Dim RenUser As New InputBox(True)
                    Dim NewUserName As String = RenUser.Display("Enter a new user name for user '" & _
                        ListBox1.Text & "'", NameMe(""), ListBox1.Text)
                    If NewUserName.Trim <> "" And NewUserName <> ListBox1.Text Then
                        Busy(Me, True)
                        Dim OldUsername As String = ListBox1.Text

                        For lintArrInc = 0 To 5
                            Dim ThisMaskFile As String = GetSetting("MaskFile" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                            SaveSetting("MaskFile" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                            SaveSetting("MaskFile" & lintArrInc, ThisMaskFile, InitalXMLConfig.XmlConfigType.UserSettings, NewUserName)
                            Dim ThisMaskFileDesc As String = GetSetting("MaskFileDesc" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                            SaveSetting("MaskFileDesc" & lintArrInc, "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                            SaveSetting("MaskFileDesc" & lintArrInc, ThisMaskFileDesc, InitalXMLConfig.XmlConfigType.UserSettings, NewUserName)
                        Next lintArrInc
                        Dim ThisLastSaved As String = GetSetting("LastSaved", "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                        SaveSetting("LastSaved", "", InitalXMLConfig.XmlConfigType.UserSettings, OldUsername)
                        SaveSetting("LastSaved", ThisLastSaved, InitalXMLConfig.XmlConfigType.UserSettings, NewUserName)

                        Dim lstrAllUsers As String = GetSetting("Users", "", InitalXMLConfig.XmlConfigType.AppSettings, "") & ChrGet(255)
                        'SaveSetting("Users", lstrAllUsers.Replace(ListBox1.Text & ChrGet(255), "").Replace(ChrGet(255) & _
                        '    ChrGet(255), ChrGet(255)), InitalXMLConfig.XmlConfigType.AppSettings, "")
                        SaveSetting("Users", lstrAllUsers.Replace(ListBox1.Text & ChrGet(255), NewUserName).Replace(ChrGet(255) & _
                                ChrGet(255), ChrGet(255)), InitalXMLConfig.XmlConfigType.AppSettings, "")

                        If mLoginInAs = OldUsername Then
                            mLoginInAs = NewUserName
                        End If
                        Busy(Me, False)
                    End If
            End Select
            Me.Close()
        End If

        AddDebugComment("UsersGeneral.btnOK_Click - end")

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        AddDebugComment("UsersGeneral.btnCancel_Click")
        Me.Close()

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub SetBackcolors()

        AddDebugComment("UsersGeneral.SetBackcolors")

        btnOK.BackColor = Color.FromArgb(0, btnOK.BackColor)
        btnCancel.BackColor = Color.FromArgb(0, btnCancel.BackColor)

    End Sub

    Private Sub UsersGeneral_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate()
    End Sub

    Private Sub UsersGeneral_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
End Class
