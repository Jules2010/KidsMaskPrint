Imports System.Drawing.Drawing2D
Friend Class SignIn
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mSelectedUser As String
    Friend Property SelectedUser() As String
        Get
            Return mSelectedUser
        End Get
        Set(ByVal Value As String)
            mSelectedUser = Value
        End Set
    End Property
    Dim mUICol1 As Color
    Friend ReadOnly Property UICol1() As Color
        Get
            Return mUICol1
        End Get
    End Property
    Dim mUICol2 As Color
    Friend ReadOnly Property UICol2() As Color
        Get
            Return mUICol2
        End Get
    End Property
    Friend Enum Params
        None
        NewRequired
    End Enum
    Dim mParam As Params = Params.None
    Friend ReadOnly Property Param() As Params
        Get
            Return mParam
        End Get
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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblSelectName As System.Windows.Forms.Label
    Friend WithEvents lbListBox As System.Windows.Forms.ListBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents btnOKClick As WinOnly.BevelButton
    Friend WithEvents btnNew As WinOnly.BevelButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SignIn))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnNew = New WinOnly.BevelButton()
        Me.btnOKClick = New WinOnly.BevelButton()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lbListBox = New System.Windows.Forms.ListBox()
        Me.lblSelectName = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnNew, Me.btnOKClick, Me.lblName, Me.lbListBox, Me.lblSelectName})
        Me.Panel2.Location = New System.Drawing.Point(8, 56)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(280, 288)
        Me.Panel2.TabIndex = 23
        '
        'btnNew
        '
        Me.btnNew.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnNew.BackColor = System.Drawing.Color.Red
        Me.btnNew.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnNew.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.ForeColor = System.Drawing.Color.Gold
        Me.btnNew.Location = New System.Drawing.Point(8, 240)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(88, 40)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&New"
        '
        'btnOKClick
        '
        Me.btnOKClick.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnOKClick.BackColor = System.Drawing.Color.Red
        Me.btnOKClick.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnOKClick.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOKClick.ForeColor = System.Drawing.Color.Gold
        Me.btnOKClick.Location = New System.Drawing.Point(176, 240)
        Me.btnOKClick.Name = "btnOKClick"
        Me.btnOKClick.Size = New System.Drawing.Size(88, 40)
        Me.btnOKClick.TabIndex = 0
        Me.btnOKClick.Text = "&OK"
        '
        'lblName
        '
        Me.lblName.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblName.ForeColor = System.Drawing.Color.Blue
        Me.lblName.Location = New System.Drawing.Point(8, 128)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(266, 32)
        Me.lblName.TabIndex = 4
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbListBox
        '
        Me.lbListBox.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.lbListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lbListBox.Enabled = False
        Me.lbListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbListBox.Location = New System.Drawing.Point(48, 162)
        Me.lbListBox.Name = "lbListBox"
        Me.lbListBox.ScrollAlwaysVisible = True
        Me.lbListBox.Size = New System.Drawing.Size(184, 56)
        Me.lbListBox.TabIndex = 3
        '
        'lblSelectName
        '
        Me.lblSelectName.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblSelectName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectName.Location = New System.Drawing.Point(0, 8)
        Me.lblSelectName.Name = "lblSelectName"
        Me.lblSelectName.Size = New System.Drawing.Size(280, 32)
        Me.lblSelectName.TabIndex = 1
        Me.lblSelectName.Text = "Click your name"
        Me.lblSelectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Bitmap)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(280, 32)
        Me.PictureBox1.TabIndex = 24
        Me.PictureBox1.TabStop = False
        '
        'SignIn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(298, 360)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.PictureBox1, Me.Panel2})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "SignIn"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Welcome"
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim mUsers(0) As String

    Private listBoxBrushes() As Brush
    Private lGradeCol1(4) As Color
    Private lGradeCol2(4) As Color
    Dim evOnClick As New EventHandler(AddressOf NameBtnOnClick)
    Private Sub SignIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'check DAT file for users, if no uses don't show buttons
        Me.SuspendLayout()

        AddDebugComment("SignIn.SignIn_Load - start")

        Busy(Me, True)

        Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
        Dim lstrUsersStr As String

        With InitialConfig

            lstrUsersStr = .GetValue("Users", "")

        End With
        InitialConfig = Nothing

        Dim FirstBtnHeight As Integer
        Dim FirstBtnLeft As Integer
        Dim FirstBtnTop As Integer
        Dim LastButtonLeft As Integer
        Dim GreatestHeight As Integer
        Dim MinFormWidth As Integer = 304
        Dim lintArrInc As Integer
        Dim lbooButtonsAdded As Boolean = False

        Dim xx() As BevelButton

        'If lstrUsersStr <> "" Then
        lstrUsersStr = ReplaceAll(lstrUsersStr, ChrGet(255) & ChrGet(255), ChrGet(255))

        If RightGet(lstrUsersStr, 1) = ChrGet(255) Then
            lstrUsersStr = lstrUsersStr.Remove(lstrUsersStr.Length - 1, 1)
        End If

        If lstrUsersStr <> "" Then
            mUsers = Microsoft.VisualBasic.Split(lstrUsersStr, ChrGet(255))

            'if users add them to an array
            Dim PositionArr(mUsers.GetUpperBound(0)) As Point

            ObjPositing(PositionArr, btnOKClick.Height + 10, btnOKClick.Width + 10, 4, GreatestHeight)
            ReDim xx(mUsers.GetUpperBound(0))

            For lintArrInc = 0 To xx.GetUpperBound(0)
                xx(lintArrInc) = New BevelButton()
                With xx(lintArrInc)
                    .Location = New Point(PositionArr(lintArrInc).X, PositionArr(lintArrInc).Y + 40)
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Height = btnOKClick.Height
                    .Width = btnOKClick.Width
                    .Text = mUsers(lintArrInc)
                    AddHandler xx(lintArrInc).Click, evOnClick
                    Panel2.Controls.Add(xx(lintArrInc))
                    lbooButtonsAdded = True
                End With
            Next lintArrInc
            FirstBtnHeight = xx(0).Height
            FirstBtnLeft = xx(0).Left
            FirstBtnTop = xx(0).Top
            LastButtonLeft = xx(xx.GetUpperBound(0)).Left

        End If

        If lbooButtonsAdded = True Then
            If Me.Width < LastButtonLeft + btnOKClick.Width + 4 Then
                Me.Width = LastButtonLeft + btnOKClick.Width + 14

            End If

            Panel2.Width = LastButtonLeft + btnOKClick.Width + 4

            If Panel2.Width < Me.Width - 16 Then
                Panel2.Width = Me.Width - 12 '16
            End If

            lblSelectName.Width = Panel2.Width
            Panel2.Left = (Me.Width - Panel2.Width) / 2
            Panel2.Height = GreatestHeight + FirstBtnHeight + 24 + 72 + 10 + 50
            Me.Height = Panel2.Top + Panel2.Height + 30

        End If

        If lbooButtonsAdded = True Then
            'adjust left position of all buttons if less than 9
            Dim LeftAdj As Integer
            Select Case xx.GetUpperBound(0)
                Case 0 To 3 ' if there are between 1 and 4 buttons
                    LeftAdj = 107 '110
                Case 4 To 7
                    LeftAdj = 107 / 2
                Case Else
                    ' do nothing
            End Select

            For lintArrInc = 0 To xx.GetUpperBound(0)
                xx(lintArrInc).Left += LeftAdj
            Next lintArrInc
        End If

        Me.Location = CentreMe(Me)

        Busy(Me, False)

        AddDebugComment("SignIn.SignIn_Load - end")

        Me.ResumeLayout()

    End Sub
    Private Sub NameBtnOnClick(ByVal sender As Object, ByVal e As EventArgs)

        AddDebugComment("SignIn.NameBtnOnClick - start")

        lbListBox.Items.Clear()
        SetBrushes()

        mSelectedUser = sender.text
        lblName.Text = mSelectedUser

        lbListBox.Text = GetSetting("Colours", "One", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)


        lbListBox.Enabled = True
        'Me.Close()

        AddDebugComment("SignIn.NameBtnOnClick - end")

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        AddDebugComment("SignIn.btnHelp_Click")

        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.SignIn))

    End Sub
    Private Sub btnOKClick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOKClick.Click

        AddDebugComment("SignIn.btnOKClick_Click - start")

        If mSelectedUser <> "" Then

            SaveSetting("Colours", lbListBox.Text, InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)

            Me.Close()
        End If

        AddDebugComment("SignIn.btnOKClick_Click - start")

    End Sub
    Private Sub SetBrushes()

        AddDebugComment("SignIn.SetBrushes - start")

        Me.lbListBox.Items.AddRange(New Object() {"One", "Two", "Three", "Four", "Five"})


        Dim r As Rectangle
        r = New Rectangle(0, 0, lbListBox.Width, lbListBox.ItemHeight)

                lGradeCol1(0) = SystemColors.Control : lGradeCol2(0) = SystemColors.Window
        lGradeCol1(1) = Color.FromArgb(193, 255, 223) : lGradeCol2(1) = Color.FromArgb(236, 255, 245)
        lGradeCol1(2) = Color.FromArgb(255, 187, 187) : lGradeCol2(2) = Color.FromArgb(255, 225, 225)
        lGradeCol1(3) = Color.FromArgb(192, 205, 254) : lGradeCol2(3) = Color.FromArgb(234, 238, 255)
        lGradeCol1(4) = Color.FromArgb(0, 255, 0) : lGradeCol2(4) = Color.FromArgb(255, 0, 0)

        Dim lb1 As New LinearGradientBrush(r, lGradeCol1(0), lGradeCol2(0), LinearGradientMode.Horizontal)
        Dim lb2 As New LinearGradientBrush(r, lGradeCol1(1), lGradeCol2(1), LinearGradientMode.Horizontal)
        Dim lb3 As New LinearGradientBrush(r, lGradeCol1(2), lGradeCol2(2), LinearGradientMode.Horizontal)
        Dim lb4 As New LinearGradientBrush(r, lGradeCol1(3), lGradeCol2(3), LinearGradientMode.Horizontal)
        Dim lb5 As New LinearGradientBrush(r, lGradeCol1(4), lGradeCol2(4), LinearGradientMode.Horizontal)

        listBoxBrushes = New Brush() {lb1, lb2, lb3, lb4, lb5}

        AddDebugComment("SignIn.SetBrushes - end")

    End Sub
    Private Sub lbListBox_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lbListBox.DrawItem

        Dim brush As Brush
        Dim Itemselected As Boolean

        brush = listBoxBrushes(e.Index)

        e.Graphics.FillRectangle(brush, e.Bounds)
        Dim TempRect As Rectangle
        TempRect = e.Bounds
        TempRect.Width -= 1
        TempRect.Y += 1
        TempRect.Height -= 2

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.DrawRectangle(New Pen(SystemColors.Highlight, 2), TempRect)
        End If

    End Sub
    Private Sub lbListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbListBox.SelectedIndexChanged

        AddDebugComment("SignIn.lbListBox_SelectedIndexChanged")

        mUICol1 = lGradeCol1(lbListBox.SelectedIndex)
        mUICol2 = lGradeCol2(lbListBox.SelectedIndex)

    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

        mParam = Params.NewRequired
        Me.Close()

    End Sub
    Private Sub SignIn_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate()
    End Sub
End Class
