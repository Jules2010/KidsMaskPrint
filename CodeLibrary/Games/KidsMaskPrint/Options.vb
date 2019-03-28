Imports System.IO
Friend Class options
    Inherits System.Windows.Forms.Form

    Friend Class Data
        Friend booFloodfill As Boolean
        Friend booNoFloodfill As Boolean
        Friend booBlackLines As Boolean
        Friend booBrushWidths As Boolean
    End Class
    Private mLoginInAs As String
    Friend Property LoginInAs() As String
        Get
            Return mLoginInAs
        End Get
        Set(ByVal Value As String)
            mLoginInAs = Value
        End Set
    End Property
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
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As AppBasic.TabPagePaintEv
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblBlackLines As System.Windows.Forms.Label
    Friend WithEvents rdoBlackLines As System.Windows.Forms.RadioButton
    Friend WithEvents lblNoFloodfill As System.Windows.Forms.Label
    Friend WithEvents rdoNoFloodfill As System.Windows.Forms.RadioButton
    Friend WithEvents lblFloodFill As System.Windows.Forms.Label
    Friend WithEvents rdoFloodfill As System.Windows.Forms.RadioButton
    Friend WithEvents chkBrushWidths As System.Windows.Forms.CheckBox
    Friend WithEvents lblCombo As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New AppBasic.TabPagePaintEv()
        Me.lblBlackLines = New System.Windows.Forms.Label()
        Me.rdoBlackLines = New System.Windows.Forms.RadioButton()
        Me.lblNoFloodfill = New System.Windows.Forms.Label()
        Me.rdoNoFloodfill = New System.Windows.Forms.RadioButton()
        Me.lblFloodFill = New System.Windows.Forms.Label()
        Me.rdoFloodfill = New System.Windows.Forms.RadioButton()
        Me.chkBrushWidths = New System.Windows.Forms.CheckBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.lblCombo = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOK.Location = New System.Drawing.Point(56, 328)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(144, 328)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnApply
        '
        Me.btnApply.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnApply.Location = New System.Drawing.Point(232, 328)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "&Apply"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.AddRange(New System.Windows.Forms.Control() {Me.TabPage1})
        Me.TabControl1.ItemSize = New System.Drawing.Size(75, 18)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(392, 312)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCombo, Me.lblBlackLines, Me.rdoBlackLines, Me.lblNoFloodfill, Me.rdoNoFloodfill, Me.lblFloodFill, Me.rdoFloodfill, Me.chkBrushWidths, Me.ComboBox1})
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(384, 286)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Drawing Options"
        '
        'lblBlackLines
        '
        Me.lblBlackLines.ForeColor = System.Drawing.Color.Blue
        Me.lblBlackLines.Location = New System.Drawing.Point(24, 208)
        Me.lblBlackLines.Name = "lblBlackLines"
        Me.lblBlackLines.Size = New System.Drawing.Size(338, 35)
        Me.lblBlackLines.TabIndex = 9
        Me.lblBlackLines.Text = "Kids need to colour with felt tip pen."
        '
        'rdoBlackLines
        '
        Me.rdoBlackLines.Checked = True
        Me.rdoBlackLines.Location = New System.Drawing.Point(24, 184)
        Me.rdoBlackLines.Name = "rdoBlackLines"
        Me.rdoBlackLines.TabIndex = 8
        Me.rdoBlackLines.TabStop = True
        Me.rdoBlackLines.Text = "Black lines"
        '
        'lblNoFloodfill
        '
        Me.lblNoFloodfill.ForeColor = System.Drawing.Color.Blue
        Me.lblNoFloodfill.Location = New System.Drawing.Point(24, 144)
        Me.lblNoFloodfill.Name = "lblNoFloodfill"
        Me.lblNoFloodfill.Size = New System.Drawing.Size(338, 35)
        Me.lblNoFloodfill.TabIndex = 7
        Me.lblNoFloodfill.Text = "Kids need to colour with felt tip pen. Can do coloured lines on computer"
        '
        'rdoNoFloodfill
        '
        Me.rdoNoFloodfill.Location = New System.Drawing.Point(24, 120)
        Me.rdoNoFloodfill.Name = "rdoNoFloodfill"
        Me.rdoNoFloodfill.Size = New System.Drawing.Size(192, 24)
        Me.rdoNoFloodfill.TabIndex = 6
        Me.rdoNoFloodfill.Text = "Lines and colour (No Flood Fill)"
        '
        'lblFloodFill
        '
        Me.lblFloodFill.ForeColor = System.Drawing.Color.Blue
        Me.lblFloodFill.Location = New System.Drawing.Point(24, 80)
        Me.lblFloodFill.Name = "lblFloodFill"
        Me.lblFloodFill.Size = New System.Drawing.Size(338, 35)
        Me.lblFloodFill.TabIndex = 5
        Me.lblFloodFill.Text = "Choice of kids colouring in on computer or with felt tip pen. Can do coloured lin" & _
        "es on computer"
        '
        'rdoFloodfill
        '
        Me.rdoFloodfill.Location = New System.Drawing.Point(24, 56)
        Me.rdoFloodfill.Name = "rdoFloodfill"
        Me.rdoFloodfill.Size = New System.Drawing.Size(173, 24)
        Me.rdoFloodfill.TabIndex = 4
        Me.rdoFloodfill.Text = "Flood Fill and Palette"
        '
        'chkBrushWidths
        '
        Me.chkBrushWidths.Location = New System.Drawing.Point(24, 248)
        Me.chkBrushWidths.Name = "chkBrushWidths"
        Me.chkBrushWidths.Size = New System.Drawing.Size(177, 24)
        Me.chkBrushWidths.TabIndex = 4
        Me.chkBrushWidths.Text = "Brush Widths"
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(24, 24)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(338, 21)
        Me.ComboBox1.TabIndex = 4
        Me.ComboBox1.Text = "ComboBox1"
        '
        'btnHelp
        '
        Me.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnHelp.Location = New System.Drawing.Point(320, 328)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.TabIndex = 3
        Me.btnHelp.Text = "&Help"
        '
        'lblCombo
        '
        Me.lblCombo.Location = New System.Drawing.Point(24, 8)
        Me.lblCombo.Name = "lblCombo"
        Me.lblCombo.Size = New System.Drawing.Size(120, 16)
        Me.lblCombo.TabIndex = 10
        Me.lblCombo.Text = "Program user / child"
        '
        'options
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(408, 359)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnHelp, Me.TabControl1, Me.btnApply, Me.btnCancel, Me.btnOK})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(414, 384)
        Me.Name = "options"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim DefaultCombo As String = "(Default for all program users)"
    Dim DataArr As New ArrayList()
    Dim LastIndex As Integer = -1
    Private Sub options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("options.options_Load - start") 

        If IsAboveOrEqualWinXp() = True Then
            btnOK.FlatStyle = FlatStyle.System
            btnCancel.FlatStyle = FlatStyle.System
            btnApply.FlatStyle = FlatStyle.System
            btnHelp.FlatStyle = FlatStyle.System
            rdoFloodfill.FlatStyle = FlatStyle.System
            rdoNoFloodfill.FlatStyle = FlatStyle.System
            rdoBlackLines.FlatStyle = FlatStyle.System
        End If

        'ComboBox1.Items.Add(DefaultCombo)



        Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
        Dim lstrUsers() As String

        With InitialConfig

            lstrUsers = Microsoft.VisualBasic.Split(.GetValue("Users", ""), ChrGet(255))
            ComboBox1.Items.AddRange(lstrUsers)
        End With

        InitialConfig = Nothing

        Dim SelectIndex As Integer
        Dim lintArrInc As Integer
        For lintArrInc = 0 To ComboBox1.Items.Count - 1
            Dim UserName As String = ComboBox1.Items(lintArrInc)
            If UserName = DefaultCombo Then UserName = "Default"
            Dim ThisData As New Data()
            With ThisData
                .booFloodfill = CBool(GetSetting("FloodFillAndPalette", rdoFloodfill.Checked, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
                .booNoFloodfill = CBool(GetSetting("LineAndColour", rdoNoFloodfill.Checked, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
                .booBlackLines = CBool(GetSetting("BlackLines", rdoBlackLines.Checked, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
                .booBrushWidths = CBool(GetSetting("BrushWidths", chkBrushWidths.Checked, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
            End With
            DataArr.Add(ThisData)
            If mLoginInAs = UserName Then
                SelectIndex = lintArrInc
            End If
        Next lintArrInc


        ComboBox1.SelectedIndex = SelectIndex

        LoadSelection()

        SetBackcolors()

        AddDebugComment("options.options_Load - end") 
    End Sub
    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        SaveChanges()
        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        AddDebugComment("options.btnCancel_Click") 
        Me.Close()

    End Sub
    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click

        SaveChanges()

    End Sub
    Private Sub SaveChanges()

        AddDebugComment("options.SaveChanges - start") 

        StoreSelections(LastIndex)

        Dim lintArrInc As Integer
        For lintArrInc = 0 To ComboBox1.Items.Count - 1
            Dim UserName As String = ComboBox1.Items(lintArrInc)
            If UserName = DefaultCombo Then UserName = "Default"
            Dim ThisData As Data = DataArr(lintArrInc)
            With ThisData
                SaveSetting("FloodFillAndPalette", .booFloodfill, InitalXMLConfig.XmlConfigType.UserSettings, UserName)
                SaveSetting("LineAndColour", .booNoFloodfill, InitalXMLConfig.XmlConfigType.UserSettings, UserName)
                SaveSetting("BlackLines", .booBlackLines, InitalXMLConfig.XmlConfigType.UserSettings, UserName)
                SaveSetting("BrushWidths", .booBrushWidths, InitalXMLConfig.XmlConfigType.UserSettings, UserName)
            End With
            DataArr.Add(ThisData)
        Next lintArrInc

        AddDebugComment("options.SaveChanges - end") 

    End Sub
    Private Sub StoreSelections(ByVal index As Integer)

        AddDebugComment("options.StoreSelections ") 

        Dim ThisData As Data = DataArr(index)
        With ThisData

            .booFloodfill = rdoFloodfill.Checked
            .booNoFloodfill = rdoNoFloodfill.Checked
            .booBlackLines = rdoBlackLines.Checked
            .booBrushWidths = chkBrushWidths.Checked
        End With

    End Sub
    Private Sub LoadSelection()

        AddDebugComment("options.LoadSelection") 

        Dim ThisData As Data = DataArr(ComboBox1.SelectedIndex)
        With ThisData

            rdoFloodfill.Checked = .booFloodfill
            rdoNoFloodfill.Checked = .booNoFloodfill
            rdoBlackLines.Checked = .booBlackLines
            chkBrushWidths.Checked = .booBrushWidths
        End With

    End Sub
    Private Sub SetBackcolors()

        btnOK.BackColor = Color.FromArgb(0, btnOK.BackColor)
        btnCancel.BackColor = Color.FromArgb(0, btnCancel.BackColor)
        btnApply.BackColor = Color.FromArgb(0, btnApply.BackColor)
        btnHelp.BackColor = Color.FromArgb(0, btnHelp.BackColor)

        TabControl1.BackColor = Color.FromArgb(0, TabControl1.BackColor)
        TabPage1.BackColor = Color.FromArgb(0, TabPage1.BackColor)

        rdoFloodfill.BackColor = Color.FromArgb(0, rdoFloodfill.BackColor)
        rdoNoFloodfill.BackColor = Color.FromArgb(0, rdoNoFloodfill.BackColor)
        rdoBlackLines.BackColor = Color.FromArgb(0, rdoBlackLines.BackColor)

        lblCombo.BackColor = Color.FromArgb(0, lblCombo.BackColor)
        lblFloodFill.BackColor = Color.FromArgb(0, lblFloodFill.BackColor)
        lblNoFloodfill.BackColor = Color.FromArgb(0, lblNoFloodfill.BackColor)
        lblBlackLines.BackColor = Color.FromArgb(0, lblBlackLines.BackColor)

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub options_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate()
    End Sub
    Private Sub TabPage1_TabPagePaint(ByVal pevent As System.Windows.Forms.PaintEventArgs, ByVal Tab As System.Windows.Forms.TabPage) Handles TabPage1.TabPagePaint

        Dim PaintBack As New UIStyle.Painting()
        TabPage1.BackgroundImage = PaintBack.PaintBackgroundToFit(pevent, Me.Height, Me.Width, TabControl1.Top + 20, TabControl1.Left + 3, TabPage1.Width + 12, TabPage1.Height + 28)

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        If LastIndex > -1 Then
            StoreSelections(LastIndex)
        End If

        LoadSelection()

        LastIndex = ComboBox1.SelectedIndex

    End Sub

    Private Sub options_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        AddDebugComment("options.options_Closing") 

    End Sub
End Class