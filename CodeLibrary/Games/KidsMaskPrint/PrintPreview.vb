Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices

Friend Class PrintPreview
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mMainPictureBox As PictureBox
    Friend Property MainPictureBox() As PictureBox
        Get
            Return mMainPictureBox
        End Get
        Set(ByVal Value As PictureBox)
            mMainPictureBox = Value
        End Set
    End Property

    Dim mMousePath() As GraphicsPath
    Friend Property MousePath() As GraphicsPath()
        Get
            Return mMousePath
        End Get
        Set(ByVal Value As GraphicsPath())
            mMousePath = Value
        End Set
    End Property
    Dim mReverseMousePath() As GraphicsPath
    Friend Property ReverseMousePath() As GraphicsPath()
        Get
            Return mReverseMousePath
        End Get
        Set(ByVal Value As GraphicsPath())
            mReverseMousePath = Value
        End Set
    End Property
    Dim mPieces As ArrayList
    Friend Property Pieces() As ArrayList
        Get
            Return mPieces
        End Get
        Set(ByVal Value As ArrayList)
            mPieces = Value
        End Set
    End Property

    Dim mThisPaintBrush() As PaintBrush 
    Friend Property ThisPaintBrush() As PaintBrush() 
        Get
            Return mThisPaintBrush
        End Get
        Set(ByVal Value As PaintBrush())
            mThisPaintBrush = Value
        End Set
    End Property
    Dim mThisPaintReverseBrush() As PaintBrush 
    Friend Property ThisPaintReverseBrush() As PaintBrush() 
        Get
            Return mThisPaintReverseBrush
        End Get
        Set(ByVal Value As PaintBrush())
            mThisPaintReverseBrush = Value
        End Set
    End Property
    Dim lUserPieces As New FacePartStuctureDataFile() 
    Friend Property UserPieces() As FacePartStuctureDataFile 
        Get
            Return lUserPieces
        End Get
        Set(ByVal Value As FacePartStuctureDataFile)
            lUserPieces = Value
        End Set
    End Property
    Dim lSortOrderForData As New SortOrderForData() 
    Friend Property SortOrder() As SortOrderForData 
        Get
            Return lSortOrderForData
        End Get
        Set(ByVal Value As SortOrderForData)
            lSortOrderForData = Value
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
    Friend WithEvents PrintPreviewControl1 As System.Windows.Forms.PrintPreviewControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnLandscape As WinOnly.BevelButton
    Friend WithEvents btnPrint As WinOnly.BevelButton
    Friend WithEvents btnHelp As WinOnly.BevelButton
    Friend WithEvents btnPages As WinOnly.BevelButton
    Friend WithEvents btExit As WinOnly.BevelButton
    Friend WithEvents btnSetup As WinOnly.BevelButton
    Friend WithEvents btnPlus As System.Windows.Forms.Button
    Friend WithEvents btnNeg As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pdActualPrint As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(PrintPreview))
        Me.PrintPreviewControl1 = New System.Windows.Forms.PrintPreviewControl()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnNeg = New System.Windows.Forms.Button()
        Me.btnPlus = New System.Windows.Forms.Button()
        Me.btnSetup = New WinOnly.BevelButton()
        Me.btExit = New WinOnly.BevelButton()
        Me.btnPages = New WinOnly.BevelButton()
        Me.btnHelp = New WinOnly.BevelButton()
        Me.btnLandscape = New WinOnly.BevelButton()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btnPrint = New WinOnly.BevelButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pdActualPrint = New System.Drawing.Printing.PrintDocument()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrintPreviewControl1
        '
        Me.PrintPreviewControl1.AutoZoom = False
        Me.PrintPreviewControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PrintPreviewControl1.Document = Me.PrintDocument1
        Me.PrintPreviewControl1.Location = New System.Drawing.Point(0, 72)
        Me.PrintPreviewControl1.Name = "PrintPreviewControl1"
        Me.PrintPreviewControl1.Size = New System.Drawing.Size(744, 414)
        Me.PrintPreviewControl1.TabIndex = 0
        Me.PrintPreviewControl1.Zoom = 0.5
        '
        'PrintDocument1
        '
        '
        'Panel1
        '
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnNeg, Me.btnPlus, Me.btnSetup, Me.btExit, Me.btnPages, Me.btnHelp, Me.btnLandscape, Me.ComboBox1, Me.btnPrint, Me.Label1})
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(744, 72)
        Me.Panel1.TabIndex = 1
        '
        'btnNeg
        '
        Me.btnNeg.Location = New System.Drawing.Point(576, 24)
        Me.btnNeg.Name = "btnNeg"
        Me.btnNeg.Size = New System.Drawing.Size(56, 24)
        Me.btnNeg.TabIndex = 8
        Me.btnNeg.Text = "UpOff"
        Me.btnNeg.Visible = False
        '
        'btnPlus
        '
        Me.btnPlus.Location = New System.Drawing.Point(576, 0)
        Me.btnPlus.Name = "btnPlus"
        Me.btnPlus.Size = New System.Drawing.Size(56, 24)
        Me.btnPlus.TabIndex = 7
        Me.btnPlus.Text = "DownOff"
        Me.btnPlus.Visible = False
        '
        'btnSetup
        '
        Me.btnSetup.BackColor = System.Drawing.Color.Red
        Me.btnSetup.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnSetup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnSetup.ForeColor = System.Drawing.Color.Gold
        Me.btnSetup.Location = New System.Drawing.Point(104, 8)
        Me.btnSetup.Name = "btnSetup"
        Me.btnSetup.Size = New System.Drawing.Size(88, 40)
        Me.btnSetup.TabIndex = 6
        Me.btnSetup.Text = "&Setup"
        '
        'btExit
        '
        Me.btExit.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btExit.BackColor = System.Drawing.Color.Red
        Me.btExit.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btExit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btExit.ForeColor = System.Drawing.Color.Gold
        Me.btExit.Location = New System.Drawing.Point(648, 8)
        Me.btExit.Name = "btExit"
        Me.btExit.Size = New System.Drawing.Size(88, 40)
        Me.btExit.TabIndex = 5
        Me.btExit.Text = "E&xit"
        '
        'btnPages
        '
        Me.btnPages.BackColor = System.Drawing.Color.Red
        Me.btnPages.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnPages.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnPages.ForeColor = System.Drawing.Color.Gold
        Me.btnPages.Location = New System.Drawing.Point(408, 8)
        Me.btnPages.Name = "btnPages"
        Me.btnPages.Size = New System.Drawing.Size(88, 40)
        Me.btnPages.TabIndex = 4
        Me.btnPages.Text = "&2 Pages"
        '
        'btnHelp
        '
        Me.btnHelp.BackColor = System.Drawing.Color.Red
        Me.btnHelp.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHelp.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.Color.Gold
        Me.btnHelp.Location = New System.Drawing.Point(8, 8)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(88, 40)
        Me.btnHelp.TabIndex = 0
        Me.btnHelp.Text = "&Help"
        '
        'btnLandscape
        '
        Me.btnLandscape.BackColor = System.Drawing.Color.Red
        Me.btnLandscape.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnLandscape.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnLandscape.ForeColor = System.Drawing.Color.Gold
        Me.btnLandscape.Location = New System.Drawing.Point(296, 8)
        Me.btnLandscape.Name = "btnLandscape"
        Me.btnLandscape.Size = New System.Drawing.Size(104, 40)
        Me.btnLandscape.TabIndex = 2
        Me.btnLandscape.Text = "Landscape"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Location = New System.Drawing.Point(504, 16)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(64, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Red
        Me.btnPrint.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.ForeColor = System.Drawing.Color.Gold
        Me.btnPrint.Location = New System.Drawing.Point(200, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 40)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "&Print"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(0, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(744, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Works best with A4 Paper size, when using more than 1 page"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'pdActualPrint
        '
        '
        'PrintPreview
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(744, 486)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.PrintPreviewControl1, Me.Panel1})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "PrintPreview"
        Me.Text = "PrintPreview"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    'Dim PrintPageSettings As New System.Drawing.Printing.PageSettings()
    Dim HasMorePages As Integer = 0
    Dim RightOff As Single '= 150
    Dim Fac As Single ' = 2
    Dim LeftOff As Single '= -250
    '--- 
    Const Size1 As String = "1 Year" ' "Size 1"
    Const Size2 As String = "2 Years" '"Size 2"
    Const Size3 As String = "4 Years" '"Size 3"
    Const Size4 As String = "6 Years" '"Size 4"
    Const Size5 As String = "Adult" '"Size 5"
    '--- 
    Dim DownOff As Single ' bottom of first page offset
    Dim UpOff As Single ' top of second page offsett

    Dim m_HardLeft As Single
    Dim m_HardTop As Single
    Dim m_HardRight As Single
    Dim m_HardBottom As Single

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        AddDebugComment("PrintPreview.PrintDocument1_PrintPage - start") 

        '--- 

        Dim hdcPtr As IntPtr '= e.Graphics.GetHdc
        '  Dim hdcLong As Long = hdcPtr.ToInt32

        Dim gr As Graphics = PrintDocument1.PrinterSettings.CreateMeasurementGraphics()

        hdcPtr = gr.GetHdc
        GetHardMargins(hdcPtr, m_HardLeft, m_HardTop, m_HardRight, m_HardBottom)

        gr.ReleaseHdc(hdcPtr)
        'Console.WriteLine("Graphics.VisibleClipBounds.Height=" & e.Graphics.VisibleClipBounds.Height)
        'Console.WriteLine(m_HardLeft & " " & m_HardTop & " " & m_HardRight & " " & m_HardBottom)
        '--- 

        '--- 
        If PrintDocument1.DefaultPageSettings.Landscape = True Then
            PrintPreviewControl1.Rows = 2
            PrintPreviewControl1.Columns = 1
        Else
            PrintPreviewControl1.Rows = 1
            PrintPreviewControl1.Columns = 2
        End If
        '--- 

        ComboBox1_SelectedIndexChanged(Nothing, Nothing) 

        PrintDocument1.DefaultPageSettings = m_PageSettings 

        Dim OffSet As Point

        If btnPages.Text = "&1 Page" Then
            If HasMorePages = 0 Then
                Dim OffsetLeft As Point = New Point(RightOff, DownOff)
                OffSet = OffsetLeft
            Else
                Dim OffsetNone As Point = New Point(LeftOff, UpOff)
                OffSet = OffsetNone
                e.HasMorePages = False
                HasMorePages = 0
            End If
        Else
            Dim OffsetNone As Point = New Point(0, 0)
            OffSet = OffsetNone
            e.HasMorePages = False
        End If

        DrawOutput.DrawOutput(e.Graphics, True, mMainPictureBox, mMousePath, mReverseMousePath, _
            Fac, Color.Black, Nothing, Nothing, mPieces, OffSet, ThisPaintBrush, mThisPaintReverseBrush, lUserPieces, lSortOrderForData)

        AddDebugComment("PrintPreview.PrintDocument1_PrintPage - end") 

    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        AddDebugComment("PrintPreview.ComboBox1_SelectedIndexChanged - start") 

        If PrintDocument1.DefaultPageSettings.Landscape = True Then 
            LeftOff = 0
            RightOff = 0
            UpOff = -300
            Select Case ComboBox1.Text
                Case Size5
                    Fac = 3
                    DownOff = -15
                Case Size4
                    Fac = 2.25
                    DownOff = 70
                Case Size3
                    Fac = 2
                    DownOff = 120
                Case Size2
                    Fac = 1.75
                    DownOff = 176
                Case Size1
                    Fac = 1.5
                    DownOff = 255
            End Select
        Else 
            DownOff = 0 
            UpOff = 0 
            LeftOff = -254 '-253 '-257 '-265
            Select Case ComboBox1.Text
                Case Size5 '"Big"
                    Fac = 3
                    RightOff = 21 '11
                Case Size4 '"Medium"
                    Fac = 2.25
                    RightOff = 113 '102
                Case Size3 '"Small"
                    Fac = 2
                    RightOff = 159 '148
                    '--- 
                Case Size2
                    Fac = 1.75
                    RightOff = 217 '210
                Case Size1
                    Fac = 1.5
                    RightOff = 296 '300
                    '--- 
            End Select
        End If 

        PrintPreviewControl1.InvalidatePreview()

        AddDebugComment("PrintPreview.ComboBox1_SelectedIndexChanged - end") 

    End Sub
    Private Sub btnLandscape_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLandscape.Click

        AddDebugComment("PrintPreview.btnLandscape_Click - start") 

        If PrintDocument1.DefaultPageSettings.Landscape = True Then
            PrintDocument1.DefaultPageSettings.Landscape = False
            btnLandscape.Text = "Landscape"
        Else
            PrintDocument1.DefaultPageSettings.Landscape = True
            btnLandscape.Text = "Potrait"
        End If

        ComboBox1_SelectedIndexChanged(Nothing, Nothing) 

        PrintPreviewControl1.InvalidatePreview()

        AddDebugComment("PrintPreview.btnLandscape_Click - end") 

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        AddDebugComment("PrintPreview.OnPaintBackground - start") 

        'added 
        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

        AddDebugComment("PrintPreview.OnPaintBackground - end") 

        'Panel1.BackgroundImage = PaintBack.PaintBackgroundToFit(pevent, Me.Height, Me.Width, Panel1.Top, Panel1.Left, Panel1.Width, Panel1.Height) 
        'Label1.BackgroundImage = PaintBack.PaintBackgroundToFit(pevent, Me.Height, Me.Width, Label1.Top, Label1.Left, Label1.Width, Label1.Height) 

    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        AddDebugComment("PrintPreview.btnPrint_Click - start") 


        '--- 
        If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
            Dim intPrintAllows As Integer = 0
            Try : intPrintAllows = Val(GetSetting("Brushes", "5", InitalXMLConfig.XmlConfigType.AppSettings, "")) : Catch : End Try
            If intPrintAllows = 0 Or intPrintAllows > 5 Then
                MessageBox.Show("You have used the print feature 5 times." & Environment.NewLine & Environment.NewLine & _
                    "Therefore you have reached the end of your evaluation of this feature.", _
                    NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                intPrintAllows -= 1
                Try : SaveSetting("Brushes", intPrintAllows, InitalXMLConfig.XmlConfigType.AppSettings, "") : Catch : End Try
                Dim dlgRes As DialogResult
                MessageBox.Show("You have " & intPrintAllows & " out of 5 uses of the print feature left in this trial." & CR() & CR() & _
                    "Would you like to visit our website to view purchasing options?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If dlgRes = DialogResult.Yes Then
                    BrowseToUrl("http://www.example.com/buy.php", Me)
                End If
            End If
        End If
        '--- 

        'PrintDocument1.Print()
        pdActualPrint.Print()
        Me.Close()

        AddDebugComment("PrintPreview.btnPrint_Click - end") 

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("PrintPreview.btnHelp_Click") 

        'PrintPreviewControl1.StartPage = PrintPreviewControl1.StartPage + 1
        
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.Printing))

    End Sub
    Private Sub btnPages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPages.Click

        AddDebugComment("PrintPreview.btnPages_Click - start") 

        If btnPages.Text = "&2 Pages" Then
            btnPages.Text = "&1 Page"
        Else
            btnPages.Text = "&2 Pages"
        End If

        PrintPreviewControl1.InvalidatePreview()

        AddDebugComment("PrintPreview.btnPages_Click - end") 

    End Sub

    Private Sub PrintPreview_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        AddDebugComment("PrintPreview.PrintPreview_Resize - start") 

        Me.Invalidate() 

        AddDebugComment("PrintPreview.PrintPreview_Resize - end") 

    End Sub
    Private Sub PrintPreview_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        AddDebugComment("Preview.PrintPreview_KeyDown") 

        
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub btExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExit.Click

        AddDebugComment("Preview.btExit_Click") 

        Me.Close() 
    End Sub
    Private Sub btnSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetup.Click
        'added 
        AddDebugComment("Preview.btnSetup_Click - start")

        Try
            Dim PgSetupDlg As New PageSetupDialog()
            PgSetupDlg.PageSettings = m_PageSettings
            PgSetupDlg.AllowPrinter = True 
            PgSetupDlg.AllowPaper = True 
            PgSetupDlg.AllowMargins = True 
            PgSetupDlg.Document = PrintDocument1 
            PgSetupDlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        AddDebugComment("Preview.btnSetup_Click - end")

    End Sub
    Private Sub PrintPreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("Preview.PrintPreview_Load - start")

        Me.Text = NameMe("Print Preview")

        ComboBox1.Items.AddRange(New Object() {Size1, Size2, Size3, Size4, Size5})
        ComboBox1.SelectedIndex = 0

        AddDebugComment("Preview.PrintPreview_Load - end")

    End Sub
    Private Sub btnPlus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPlus.Click

        AddDebugComment("Preview.btnPlus_Click - start") 

        'LeftOff += 1
        Dim x As New InputBox(True)

        DownOff = x.Display("", " ", DownOff)
        PrintPreviewControl1.InvalidatePreview()

        AddDebugComment("Preview.btnPlus_Click - end") 

    End Sub
    Private Sub btnNeg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNeg.Click

        AddDebugComment("Preview.btnNeg_Click - start") 

        Dim x As New InputBox(True)

        UpOff = x.Display("", " ", UpOff)
        PrintPreviewControl1.InvalidatePreview()

        AddDebugComment("Preview.btnNeg_Click - End") 

    End Sub
    Private Declare Function GetDeviceCaps Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal nIndex As Int32) As Int32
    Private Sub GetHardMargins(ByVal hDC As IntPtr, ByRef Left As Single, ByRef Top As Single, ByRef Right As Single, ByRef Bottom As Single)
        'Todd,
        'This is due to the 'hard margins' on the printer as you stated.  You
        'need to call something to get the margins and apply the corrections to your
        'coordinates at print time.  The following is an excerpt from a class I use
        'to do printing.  As I wrap almost all the printing calls to my own methods I
        'just use the left & top return from GetHardMargins to setup offsets that are
        'always subtracted from the coordinates I pass in.
        '
        'You can test for the need to apply margin offsets by checking
        'Graphics.VisibleClipBounds.Height from your PrintPageEventArgs argument. If
        'it is less than your page size you are most likely going to the printer so
        'you don't need to call this or set margins (i.e. offsets are 0).
        '
        'Ron Allen

        Const PHYSICALOFFSETX As Int32 = 112
        Const PHYSICALOFFSETY As Int32 = 113
        Const HORZRES As Int32 = 8
        Const VERTRES As Int32 = 10
        Const HORZSIZE As Int32 = 4
        Const VERTSIZE As Int32 = 6

        Dim offx As Single = Convert.ToSingle(GetDeviceCaps(hDC, PHYSICALOFFSETX))
        Dim offy As Single = Convert.ToSingle(GetDeviceCaps(hDC, PHYSICALOFFSETY))
        Dim resx As Single = Convert.ToSingle(GetDeviceCaps(hDC, HORZRES))
        Dim resy As Single = Convert.ToSingle(GetDeviceCaps(hDC, VERTRES))
        Dim hsz As Single = Convert.ToSingle(GetDeviceCaps(hDC, HORZSIZE)) / 25.4F               ' Screen width in inches.
        Dim vsz As Single = Convert.ToSingle(GetDeviceCaps(hDC, VERTSIZE)) / 25.4F               ' Screen height in inches.
        Dim ppix As Single = resx / hsz
        Dim ppiy As Single = resy / vsz

        Left = (offx / ppix) * 100.0F
        Top = (offy / ppix) * 100.0F
        Bottom = Top + (vsz * 100.0F)
        Right = Left + (hsz * 100.0F)

    End Sub
    Private Sub pdActualPrint_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pdActualPrint.PrintPage

        'added 
        AddDebugComment("PrintPreview.pdActualPrint_PrintPage - start")

        pdActualPrint.DefaultPageSettings = m_PageSettings

        Dim OffSet As Point

        If btnPages.Text = "&1 Page" Then
            If HasMorePages = 0 Then
                HasMorePages = 1
                e.HasMorePages = True
                Dim OffsetLeft As Point = New Point(RightOff - m_HardLeft, DownOff)
                OffSet = OffsetLeft
            Else
                Dim OffsetNone As Point = New Point(LeftOff, UpOff)
                OffSet = OffsetNone
                e.HasMorePages = False
                HasMorePages = 0
            End If
        Else
            Dim OffsetNone As Point = New Point(0, 0)
            OffSet = OffsetNone
            e.HasMorePages = False
        End If

        DrawOutput.DrawOutput(e.Graphics, True, mMainPictureBox, mMousePath, mReverseMousePath, _
            Fac, Color.Black, Nothing, Nothing, mPieces, OffSet, ThisPaintBrush, mThisPaintReverseBrush, lUserPieces, lSortOrderForData)

        AddDebugComment("PrintPreview.pdActualPrint_PrintPage - end")
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
