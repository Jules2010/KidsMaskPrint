'I got frustrated trying to find a Simple VB.NET freehand drawing code
'So I made my own :)

'*** Acknowlegements  ***
'Ideas for this code came from the MicroSoft "Scribble" sample code, 
'Christian Graus's excellent arcticle on a C++ code called "Doodle" 
'and the MicroSoft website.

'Note that this is a VERY pedantic freehand drawing code, so be kind in you comments. :)
'It uses a graphics path to follow the users mouse movements
'The path is then painted in the window.

'By John Buettner
'26 July 2003
'******************************************************
'This code is for informational purposes
'It is property of the code writer, but may be modified and
'used for any purpose private or commercial, However,
'Users of this code must agree not to copyright or infringe
'upon the original coders rights to this code.
'******************************************************

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Imports System.IO


<Obfuscate()> Public Class frmMain
    Inherits System.Windows.Forms.Form
    Private mPieces As New ArrayList() 'Piece objects 
    Private mMousePieceStart As Point = Point.Empty 'During mouse moves, the starting location of a puzzle piece
    Private mMouseDownStart As Point = Point.Empty 'During mouse moves, the starting location of the mouse
    Private mMousePiece As Piece = Nothing 'During mouse moves, the piece being moved

    Dim mDir As String = Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location) & "\FaceParts\"

    Dim mbooLoadAllDataOnce As Boolean = True

    Dim StandLangText As System.Resources.ResourceManager = New _
        System.Resources.ResourceManager("AppBasic.AppBasic", _
        System.Reflection.Assembly.Load("AppBasic")) 'JM 15/08/2004

    Dim lbooSplashShown As Boolean = False 'JM 16/08/2004
    Dim mSelectedUser As String 'JM 17/08/2004
    Dim lbooAllowPainting As Boolean = False 'JM 17/08/2004

    Dim lbooSomethingDrawn As Boolean = False 'JM 07/09/2004

    Dim mLicensedFaceParts As New ArrayList() 'JM 19/09/2004

    Dim lintDrawingInProgress As Integer 'JM 24/09/2004

    Private m_Drawings As Drawings 'JM 11/10/2004

    Private m_UserPieces As FacePartStuctureDataFile 'JM 13/10/2004

    Dim CurXPos As Integer 'JM 13/10/2004
    Dim CurYPos As Integer 'JM 13/10/2004
    Dim ThisFloodFillBitmap As Bitmap 'JM 13/10/2004

    Private Enum Tools 'JM 14/10/2004
        Freehand
        Floodfill
    End Enum
    Dim CurrentTool As Tools = Tools.Freehand 'JM 14/10/2004
    Dim FloodFillJustOccured As Boolean = False 'JM 14/10/2004

    Private m_SortOrderForData As SortOrderForData 'JM 14/10/2004

    Dim m_CurrentColour As Color = Color.Black 'JM 15/10/2004

    Dim m_CurrentBrushWidth As Integer = 4 'JM 15/10/2004

    Dim RedoPackPieceArr As New Collections.Stack() 'JM 16/10/2004
    Dim RedoUserPieceArr As New Collections.Stack() 'JM 16/10/2004
    'Dim LastUndoActivity As SortOrderForData.eDataType 'JM 16/10/2004
    Dim RedoSortOrderStack As New Collections.Stack() 'JM 17/10/2004
    Friend WithEvents btnDebug As Button 'JM 17/10/2004

    Dim lintCustomColours() As Integer 'JM 26/10/2004

    Dim lastCustomColour As Color 'JM 26/10/2004
    '**************************************************************

    Private booPictureBox1_MouseMoveFirstDone As Boolean = False 'JM 13/03/2005
    Private booPictureBox1_PaintFirstDone As Boolean = False 'JM 13/03/2005
    Private boolstBrushWidth_DrawItemFirstDone As Boolean = False 'JM 13/03/2005

    Dim AppSettingsStartup As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings) 'JM 16/09/2005

#Region " Windows Form Designer generated code "

    Friend Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ''mPieces.Add(New Piece("D:\desktopnt\scraps\flag.png")) '"C:\a.png"))
        ''mPieces.Add(New Piece("D:\desktopnt\scraps\flag.png")) '"C:\b.png"))

        'Stop redraw flicker 
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkMirror As System.Windows.Forms.CheckBox
    Friend WithEvents btnExit As WinOnly.BevelButton
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuHelpHelpTopics As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpCheckForUpdates As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpInstallAddon As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpSupportInfo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpAbout As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpLicenseAgreement As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpEnterCode As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpSubscribeNewsletter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExportGraphics As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFilePageSetup As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileImportGraphics As System.Windows.Forms.MenuItem
    Friend WithEvents btnPrint As WinOnly.BevelButton
    Friend WithEvents btnUndo As WinOnly.BevelButton
    Friend WithEvents chkGuide As System.Windows.Forms.CheckBox
    Friend WithEvents StatusBar1 As AppBasic.ProgressStatusBar
    Friend WithEvents StatusBarPanel1 As System.Windows.Forms.StatusBarPanel
    Friend WithEvents btnClear As WinOnly.BevelButton
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileImportMask As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExportMask As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileImportx As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExportX As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileLoad As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents btnEyes As WinOnly.BevelButton
    Friend WithEvents btnMouths As WinOnly.BevelButton
    Friend WithEvents btnNoses As WinOnly.BevelButton
    Friend WithEvents btnOther As WinOnly.BevelButton
    Friend WithEvents btnEars As WinOnly.BevelButton
    Friend WithEvents MenuEnhancer As WinOnly.EnhancedMenu
    Friend WithEvents btnSave As WinOnly.BevelButton
    Friend WithEvents btnLoad As WinOnly.BevelButton
    Friend WithEvents btnHelp As WinOnly.BevelButton
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuToolsDeleteUsers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuToolsRenameUsers As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpBuyPacks As System.Windows.Forms.MenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHead As WinOnly.BevelButton
    Friend WithEvents btnMoreColours As System.Windows.Forms.Button
    Friend WithEvents pnlPalette As System.Windows.Forms.Panel
    Friend WithEvents btnPCustom As System.Windows.Forms.Button
    Friend WithEvents btnPPink As System.Windows.Forms.Button
    Friend WithEvents btnPMagenta As System.Windows.Forms.Button
    Friend WithEvents btnPBlue As System.Windows.Forms.Button
    Friend WithEvents btnPLightBlue As System.Windows.Forms.Button
    Friend WithEvents lblPLightBlue As System.Windows.Forms.Label
    Friend WithEvents btnPCyan As System.Windows.Forms.Button
    Friend WithEvents lblPCyan As System.Windows.Forms.Label
    Friend WithEvents btnPLime As System.Windows.Forms.Button
    Friend WithEvents btnPGreen As System.Windows.Forms.Button
    Friend WithEvents btnPBrown As System.Windows.Forms.Button
    Friend WithEvents btnPYellow As System.Windows.Forms.Button
    Friend WithEvents lblPLime As System.Windows.Forms.Label
    Friend WithEvents lblPGreen As System.Windows.Forms.Label
    Friend WithEvents lblPBrown As System.Windows.Forms.Label
    Friend WithEvents lblPYellow As System.Windows.Forms.Label
    Friend WithEvents btnPOrange As System.Windows.Forms.Button
    Friend WithEvents lblPOrange As System.Windows.Forms.Label
    Friend WithEvents btnPRed As System.Windows.Forms.Button
    Friend WithEvents btnPWhite As System.Windows.Forms.Button
    Friend WithEvents lblPWhite As System.Windows.Forms.Label
    Friend WithEvents btnPBlack As System.Windows.Forms.Button
    Friend WithEvents lblPBlack As System.Windows.Forms.Label
    Friend WithEvents lblPCustom As System.Windows.Forms.Label
    Friend WithEvents lblPPink As System.Windows.Forms.Label
    Friend WithEvents lblPMagenta As System.Windows.Forms.Label
    Friend WithEvents lblPBlue As System.Windows.Forms.Label
    Friend WithEvents lblPRed As System.Windows.Forms.Label
    Friend WithEvents rdoFreehand As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFloodFill As System.Windows.Forms.RadioButton
    Friend WithEvents lstBrushWidth As System.Windows.Forms.ListBox
    Friend WithEvents lblPen As System.Windows.Forms.Label
    Friend WithEvents btnBlack As System.Windows.Forms.Button
    Friend WithEvents btnWhite As System.Windows.Forms.Button
    Friend WithEvents lblWhite As System.Windows.Forms.Label
    Friend WithEvents lblBlack As System.Windows.Forms.Label
    Friend WithEvents pnlBWPens As System.Windows.Forms.Panel
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuToolsOptions As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHelpReportProblem As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnBuy As WinOnly.BevelButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMain))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkMirror = New System.Windows.Forms.CheckBox()
        Me.btnExit = New WinOnly.BevelButton()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuFileLoad = New System.Windows.Forms.MenuItem()
        Me.mnuFileSave = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuFileImportx = New System.Windows.Forms.MenuItem()
        Me.mnuFileImportMask = New System.Windows.Forms.MenuItem()
        Me.mnuFileImportGraphics = New System.Windows.Forms.MenuItem()
        Me.mnuFileExportX = New System.Windows.Forms.MenuItem()
        Me.mnuFileExportMask = New System.Windows.Forms.MenuItem()
        Me.mnuFileExportGraphics = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.mnuFilePageSetup = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.mnuFileExit = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.mnuToolsDeleteUsers = New System.Windows.Forms.MenuItem()
        Me.mnuToolsRenameUsers = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.mnuToolsOptions = New System.Windows.Forms.MenuItem()
        Me.mnuHelp = New System.Windows.Forms.MenuItem()
        Me.mnuHelpHelpTopics = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuHelpCheckForUpdates = New System.Windows.Forms.MenuItem()
        Me.mnuHelpInstallAddon = New System.Windows.Forms.MenuItem()
        Me.mnuHelpSupportInfo = New System.Windows.Forms.MenuItem()
        Me.mnuHelpReportProblem = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuHelpSubscribeNewsletter = New System.Windows.Forms.MenuItem()
        Me.mnuHelpEnterCode = New System.Windows.Forms.MenuItem()
        Me.mnuHelpBuyPacks = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.mnuHelpLicenseAgreement = New System.Windows.Forms.MenuItem()
        Me.mnuHelpAbout = New System.Windows.Forms.MenuItem()
        Me.btnPrint = New WinOnly.BevelButton()
        Me.btnUndo = New WinOnly.BevelButton()
        Me.chkGuide = New System.Windows.Forms.CheckBox()
        Me.btnEars = New WinOnly.BevelButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusBar1 = New AppBasic.ProgressStatusBar()
        Me.StatusBarPanel1 = New System.Windows.Forms.StatusBarPanel()
        Me.btnClear = New WinOnly.BevelButton()
        Me.btnEyes = New WinOnly.BevelButton()
        Me.btnMouths = New WinOnly.BevelButton()
        Me.btnNoses = New WinOnly.BevelButton()
        Me.btnOther = New WinOnly.BevelButton()
        Me.MenuEnhancer = New WinOnly.EnhancedMenu(Me.components)
        Me.btnSave = New WinOnly.BevelButton()
        Me.btnLoad = New WinOnly.BevelButton()
        Me.btnHelp = New WinOnly.BevelButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHead = New WinOnly.BevelButton()
        Me.pnlPalette = New System.Windows.Forms.Panel()
        Me.btnPBlack = New System.Windows.Forms.Button()
        Me.btnMoreColours = New System.Windows.Forms.Button()
        Me.btnPCustom = New System.Windows.Forms.Button()
        Me.lblPCustom = New System.Windows.Forms.Label()
        Me.btnPPink = New System.Windows.Forms.Button()
        Me.btnPMagenta = New System.Windows.Forms.Button()
        Me.btnPBlue = New System.Windows.Forms.Button()
        Me.btnPLightBlue = New System.Windows.Forms.Button()
        Me.lblPPink = New System.Windows.Forms.Label()
        Me.lblPMagenta = New System.Windows.Forms.Label()
        Me.lblPBlue = New System.Windows.Forms.Label()
        Me.lblPLightBlue = New System.Windows.Forms.Label()
        Me.btnPCyan = New System.Windows.Forms.Button()
        Me.lblPCyan = New System.Windows.Forms.Label()
        Me.btnPLime = New System.Windows.Forms.Button()
        Me.btnPGreen = New System.Windows.Forms.Button()
        Me.btnPBrown = New System.Windows.Forms.Button()
        Me.btnPYellow = New System.Windows.Forms.Button()
        Me.lblPLime = New System.Windows.Forms.Label()
        Me.lblPGreen = New System.Windows.Forms.Label()
        Me.lblPBrown = New System.Windows.Forms.Label()
        Me.lblPYellow = New System.Windows.Forms.Label()
        Me.btnPOrange = New System.Windows.Forms.Button()
        Me.lblPOrange = New System.Windows.Forms.Label()
        Me.btnPRed = New System.Windows.Forms.Button()
        Me.lblPRed = New System.Windows.Forms.Label()
        Me.btnPWhite = New System.Windows.Forms.Button()
        Me.lblPWhite = New System.Windows.Forms.Label()
        Me.lblPBlack = New System.Windows.Forms.Label()
        Me.rdoFreehand = New System.Windows.Forms.RadioButton()
        Me.rdoFloodFill = New System.Windows.Forms.RadioButton()
        Me.lstBrushWidth = New System.Windows.Forms.ListBox()
        Me.lblPen = New System.Windows.Forms.Label()
        Me.btnBlack = New System.Windows.Forms.Button()
        Me.btnWhite = New System.Windows.Forms.Button()
        Me.lblWhite = New System.Windows.Forms.Label()
        Me.lblBlack = New System.Windows.Forms.Label()
        Me.pnlBWPens = New System.Windows.Forms.Panel()
        Me.btnBuy = New WinOnly.BevelButton()
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlPalette.SuspendLayout()
        Me.pnlBWPens.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(512, 520)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'chkMirror
        '
        Me.chkMirror.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.chkMirror.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkMirror.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkMirror.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkMirror.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMirror.Location = New System.Drawing.Point(624, 92)
        Me.chkMirror.Name = "chkMirror"
        Me.chkMirror.Size = New System.Drawing.Size(40, 32)
        Me.chkMirror.TabIndex = 2
        Me.chkMirror.Text = "&Mirror"
        Me.chkMirror.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnExit.BackColor = System.Drawing.Color.Red
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnExit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Gold
        Me.btnExit.Location = New System.Drawing.Point(624, 512)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 32)
        Me.btnExit.TabIndex = 18
        Me.btnExit.Text = "E&xit"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem1, -1)
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem7, Me.mnuHelp})
        Me.MenuItem1.OwnerDraw = True
        Me.MenuItem1.Text = "&Parent Options"
        '
        'MenuItem2
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem2, -1)
        Me.MenuItem2.Index = 0
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileLoad, Me.mnuFileSave, Me.MenuItem6, Me.mnuFileImportx, Me.mnuFileExportX, Me.MenuItem12, Me.mnuFilePageSetup, Me.MenuItem10, Me.mnuFileExit})
        Me.MenuItem2.OwnerDraw = True
        Me.MenuItem2.Text = "&File"
        '
        'mnuFileLoad
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileLoad, -1)
        Me.mnuFileLoad.Index = 0
        Me.mnuFileLoad.OwnerDraw = True
        Me.mnuFileLoad.Text = "&Load Mask"
        '
        'mnuFileSave
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileSave, -1)
        Me.mnuFileSave.Index = 1
        Me.mnuFileSave.OwnerDraw = True
        Me.mnuFileSave.Text = "&Save Mask"
        '
        'MenuItem6
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem6, -1)
        Me.MenuItem6.Index = 2
        Me.MenuItem6.OwnerDraw = True
        Me.MenuItem6.Text = "-"
        '
        'mnuFileImportx
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileImportx, -1)
        Me.mnuFileImportx.Index = 3
        Me.mnuFileImportx.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileImportMask, Me.mnuFileImportGraphics})
        Me.mnuFileImportx.OwnerDraw = True
        Me.mnuFileImportx.Text = "&Import"
        '
        'mnuFileImportMask
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileImportMask, -1)
        Me.mnuFileImportMask.Index = 0
        Me.mnuFileImportMask.OwnerDraw = True
        Me.mnuFileImportMask.Text = "&Mask"
        '
        'mnuFileImportGraphics
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileImportGraphics, -1)
        Me.mnuFileImportGraphics.Index = 1
        Me.mnuFileImportGraphics.OwnerDraw = True
        Me.mnuFileImportGraphics.Text = "From &Graphics File"
        Me.mnuFileImportGraphics.Visible = False
        '
        'mnuFileExportX
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileExportX, -1)
        Me.mnuFileExportX.Index = 4
        Me.mnuFileExportX.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileExportMask, Me.mnuFileExportGraphics})
        Me.mnuFileExportX.OwnerDraw = True
        Me.mnuFileExportX.Text = "&Export"
        '
        'mnuFileExportMask
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileExportMask, -1)
        Me.mnuFileExportMask.Index = 0
        Me.mnuFileExportMask.OwnerDraw = True
        Me.mnuFileExportMask.Text = "&Mask"
        '
        'mnuFileExportGraphics
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileExportGraphics, -1)
        Me.mnuFileExportGraphics.Index = 1
        Me.mnuFileExportGraphics.OwnerDraw = True
        Me.mnuFileExportGraphics.Text = "To &Graphics File"
        '
        'MenuItem12
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem12, -1)
        Me.MenuItem12.Index = 5
        Me.MenuItem12.OwnerDraw = True
        Me.MenuItem12.Text = "-"
        '
        'mnuFilePageSetup
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFilePageSetup, -1)
        Me.mnuFilePageSetup.Index = 6
        Me.mnuFilePageSetup.OwnerDraw = True
        Me.mnuFilePageSetup.Text = "Page Set&up ..."
        '
        'MenuItem10
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem10, -1)
        Me.MenuItem10.Index = 7
        Me.MenuItem10.OwnerDraw = True
        Me.MenuItem10.Text = "-"
        '
        'mnuFileExit
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuFileExit, -1)
        Me.mnuFileExit.Index = 8
        Me.mnuFileExit.OwnerDraw = True
        Me.mnuFileExit.Text = "E&xit"
        '
        'MenuItem7
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem7, -1)
        Me.MenuItem7.Index = 1
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuToolsDeleteUsers, Me.mnuToolsRenameUsers, Me.MenuItem4, Me.mnuToolsOptions})
        Me.MenuItem7.OwnerDraw = True
        Me.MenuItem7.Text = "&Tools"
        '
        'mnuToolsDeleteUsers
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuToolsDeleteUsers, -1)
        Me.mnuToolsDeleteUsers.Index = 0
        Me.mnuToolsDeleteUsers.OwnerDraw = True
        Me.mnuToolsDeleteUsers.Text = "&Delete users"
        '
        'mnuToolsRenameUsers
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuToolsRenameUsers, -1)
        Me.mnuToolsRenameUsers.Index = 1
        Me.mnuToolsRenameUsers.OwnerDraw = True
        Me.mnuToolsRenameUsers.Text = "&Rename users"
        '
        'MenuItem4
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem4, -1)
        Me.MenuItem4.Index = 2
        Me.MenuItem4.OwnerDraw = True
        Me.MenuItem4.Text = "-"
        '
        'mnuToolsOptions
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuToolsOptions, -1)
        Me.mnuToolsOptions.Index = 3
        Me.mnuToolsOptions.OwnerDraw = True
        Me.mnuToolsOptions.Text = "&Options"
        '
        'mnuHelp
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelp, -1)
        Me.mnuHelp.Index = 2
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpHelpTopics, Me.MenuItem5, Me.mnuHelpCheckForUpdates, Me.mnuHelpInstallAddon, Me.mnuHelpSupportInfo, Me.mnuHelpReportProblem, Me.MenuItem3, Me.mnuHelpSubscribeNewsletter, Me.mnuHelpEnterCode, Me.mnuHelpBuyPacks, Me.MenuItem9, Me.mnuHelpLicenseAgreement, Me.mnuHelpAbout})
        Me.mnuHelp.OwnerDraw = True
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpHelpTopics
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpHelpTopics, -1)
        Me.mnuHelpHelpTopics.Index = 0
        Me.mnuHelpHelpTopics.OwnerDraw = True
        Me.mnuHelpHelpTopics.Text = "&Help Topics"
        '
        'MenuItem5
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem5, -1)
        Me.MenuItem5.Index = 1
        Me.MenuItem5.OwnerDraw = True
        Me.MenuItem5.Text = "-"
        '
        'mnuHelpCheckForUpdates
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpCheckForUpdates, -1)
        Me.mnuHelpCheckForUpdates.Index = 2
        Me.mnuHelpCheckForUpdates.OwnerDraw = True
        Me.mnuHelpCheckForUpdates.Text = "&Check for updates"
        '
        'mnuHelpInstallAddon
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpInstallAddon, -1)
        Me.mnuHelpInstallAddon.Index = 3
        Me.mnuHelpInstallAddon.OwnerDraw = True
        Me.mnuHelpInstallAddon.Text = "&Install Add-on"
        '
        'mnuHelpSupportInfo
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpSupportInfo, -1)
        Me.mnuHelpSupportInfo.Index = 4
        Me.mnuHelpSupportInfo.OwnerDraw = True
        Me.mnuHelpSupportInfo.Text = "S&upport Info"
        '
        'mnuHelpReportProblem
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpReportProblem, -1)
        Me.mnuHelpReportProblem.Index = 5
        Me.mnuHelpReportProblem.OwnerDraw = True
        Me.mnuHelpReportProblem.Text = "&Report Problem"
        '
        'MenuItem3
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem3, -1)
        Me.MenuItem3.Index = 6
        Me.MenuItem3.OwnerDraw = True
        Me.MenuItem3.Text = "-"
        '
        'mnuHelpSubscribeNewsletter
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpSubscribeNewsletter, -1)
        Me.mnuHelpSubscribeNewsletter.Index = 7
        Me.mnuHelpSubscribeNewsletter.OwnerDraw = True
        Me.mnuHelpSubscribeNewsletter.Text = "&Subscribe to Newsletter"
        '
        'mnuHelpEnterCode
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpEnterCode, -1)
        Me.mnuHelpEnterCode.Index = 8
        Me.mnuHelpEnterCode.OwnerDraw = True
        Me.mnuHelpEnterCode.Text = "&Enter License Code"
        '
        'mnuHelpBuyPacks
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpBuyPacks, -1)
        Me.mnuHelpBuyPacks.Index = 9
        Me.mnuHelpBuyPacks.OwnerDraw = True
        Me.mnuHelpBuyPacks.Text = "&Buy Mask Packs"
        '
        'MenuItem9
        '
        Me.MenuEnhancer.SetImageIndex(Me.MenuItem9, -1)
        Me.MenuItem9.Index = 10
        Me.MenuItem9.OwnerDraw = True
        Me.MenuItem9.Text = "-"
        '
        'mnuHelpLicenseAgreement
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpLicenseAgreement, -1)
        Me.mnuHelpLicenseAgreement.Index = 11
        Me.mnuHelpLicenseAgreement.OwnerDraw = True
        Me.mnuHelpLicenseAgreement.Text = "&License Agreement"
        '
        'mnuHelpAbout
        '
        Me.MenuEnhancer.SetImageIndex(Me.mnuHelpAbout, -1)
        Me.mnuHelpAbout.Index = 12
        Me.mnuHelpAbout.OwnerDraw = True
        Me.mnuHelpAbout.Text = "&About"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnPrint.BackColor = System.Drawing.Color.Red
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.Gold
        Me.btnPrint.Location = New System.Drawing.Point(624, 364)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 32)
        Me.btnPrint.TabIndex = 14
        Me.btnPrint.Text = "&Print"
        '
        'btnUndo
        '
        Me.btnUndo.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnUndo.BackColor = System.Drawing.Color.Red
        Me.btnUndo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUndo.Enabled = False
        Me.btnUndo.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnUndo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUndo.ForeColor = System.Drawing.Color.Gold
        Me.btnUndo.Location = New System.Drawing.Point(624, 396)
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Size = New System.Drawing.Size(88, 32)
        Me.btnUndo.TabIndex = 15
        Me.btnUndo.Text = "&Undo"
        '
        'chkGuide
        '
        Me.chkGuide.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.chkGuide.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkGuide.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chkGuide.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkGuide.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGuide.Location = New System.Drawing.Point(672, 92)
        Me.chkGuide.Name = "chkGuide"
        Me.chkGuide.Size = New System.Drawing.Size(40, 32)
        Me.chkGuide.TabIndex = 3
        Me.chkGuide.Text = "&Guide"
        Me.chkGuide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnEars
        '
        Me.btnEars.BackColor = System.Drawing.Color.DarkOrange
        Me.btnEars.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEars.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnEars.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEars.ForeColor = System.Drawing.Color.Gold
        Me.btnEars.Image = CType(resources.GetObject("btnEars.Image"), System.Drawing.Bitmap)
        Me.btnEars.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEars.ImageIndex = 1
        Me.btnEars.ImageList = Me.ImageList1
        Me.btnEars.Location = New System.Drawing.Point(8, 92)
        Me.btnEars.Name = "btnEars"
        Me.btnEars.Size = New System.Drawing.Size(88, 64)
        Me.btnEars.TabIndex = 5
        Me.btnEars.Text = "&Ears"
        Me.btnEars.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(32, 32)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.White
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 572)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.StatusBarPanel1})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(722, 22)
        Me.StatusBar1.TabIndex = 19
        Me.StatusBar1.Text = "StatusBar1"
        '
        'StatusBarPanel1
        '
        Me.StatusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.StatusBarPanel1.Text = "StatusBarPanel1"
        Me.StatusBarPanel1.Width = 706
        '
        'btnClear
        '
        Me.btnClear.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnClear.BackColor = System.Drawing.Color.Red
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnClear.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.Gold
        Me.btnClear.Location = New System.Drawing.Point(624, 252)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(88, 32)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "Ne&w"
        '
        'btnEyes
        '
        Me.btnEyes.BackColor = System.Drawing.Color.Lime
        Me.btnEyes.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEyes.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnEyes.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEyes.ForeColor = System.Drawing.Color.Gold
        Me.btnEyes.Image = CType(resources.GetObject("btnEyes.Image"), System.Drawing.Bitmap)
        Me.btnEyes.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEyes.ImageIndex = 2
        Me.btnEyes.ImageList = Me.ImageList1
        Me.btnEyes.Location = New System.Drawing.Point(8, 164)
        Me.btnEyes.Name = "btnEyes"
        Me.btnEyes.Size = New System.Drawing.Size(88, 64)
        Me.btnEyes.TabIndex = 6
        Me.btnEyes.Text = "E&yes"
        Me.btnEyes.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnMouths
        '
        Me.btnMouths.BackColor = System.Drawing.Color.Blue
        Me.btnMouths.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMouths.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnMouths.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMouths.ForeColor = System.Drawing.Color.Gold
        Me.btnMouths.Image = CType(resources.GetObject("btnMouths.Image"), System.Drawing.Bitmap)
        Me.btnMouths.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMouths.ImageIndex = 4
        Me.btnMouths.ImageList = Me.ImageList1
        Me.btnMouths.Location = New System.Drawing.Point(8, 308)
        Me.btnMouths.Name = "btnMouths"
        Me.btnMouths.Size = New System.Drawing.Size(88, 64)
        Me.btnMouths.TabIndex = 8
        Me.btnMouths.Text = "&Mouths"
        Me.btnMouths.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnNoses
        '
        Me.btnNoses.BackColor = System.Drawing.Color.Aqua
        Me.btnNoses.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNoses.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnNoses.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNoses.ForeColor = System.Drawing.Color.Gold
        Me.btnNoses.Image = CType(resources.GetObject("btnNoses.Image"), System.Drawing.Bitmap)
        Me.btnNoses.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNoses.ImageIndex = 3
        Me.btnNoses.ImageList = Me.ImageList1
        Me.btnNoses.Location = New System.Drawing.Point(8, 236)
        Me.btnNoses.Name = "btnNoses"
        Me.btnNoses.Size = New System.Drawing.Size(88, 64)
        Me.btnNoses.TabIndex = 7
        Me.btnNoses.Text = "&Noses"
        Me.btnNoses.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnOther
        '
        Me.btnOther.BackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(0, Byte), CType(192, Byte))
        Me.btnOther.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOther.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnOther.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOther.ForeColor = System.Drawing.Color.Gold
        Me.btnOther.Image = CType(resources.GetObject("btnOther.Image"), System.Drawing.Bitmap)
        Me.btnOther.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnOther.ImageIndex = 5
        Me.btnOther.ImageList = Me.ImageList1
        Me.btnOther.Location = New System.Drawing.Point(8, 380)
        Me.btnOther.Name = "btnOther"
        Me.btnOther.Size = New System.Drawing.Size(88, 64)
        Me.btnOther.TabIndex = 9
        Me.btnOther.Text = "&Other"
        Me.btnOther.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'MenuEnhancer
        '
        Me.MenuEnhancer.ColorsControl = System.Drawing.SystemColors.Control
        Me.MenuEnhancer.ColorsHighlight = System.Drawing.SystemColors.Highlight
        Me.MenuEnhancer.ColorsWindow = System.Drawing.SystemColors.Window
        Me.MenuEnhancer.ImageListMarks = Nothing
        '
        'btnSave
        '
        Me.btnSave.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnSave.BackColor = System.Drawing.Color.Red
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnSave.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Gold
        Me.btnSave.Location = New System.Drawing.Point(624, 324)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(88, 32)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "&Save"
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnLoad.BackColor = System.Drawing.Color.Red
        Me.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLoad.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnLoad.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.Gold
        Me.btnLoad.Location = New System.Drawing.Point(624, 292)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(88, 32)
        Me.btnLoad.TabIndex = 12
        Me.btnLoad.Text = "&Load"
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.btnHelp.BackColor = System.Drawing.Color.Red
        Me.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHelp.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHelp.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.Color.Gold
        Me.btnHelp.Location = New System.Drawing.Point(624, 476)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(88, 32)
        Me.btnHelp.TabIndex = 17
        Me.btnHelp.Text = "&Help"
        '
        'Panel1
        '
        Me.Panel1.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Panel1.BackColor = System.Drawing.Color.LightGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.PictureBox1})
        Me.Panel1.Location = New System.Drawing.Point(104, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(512, 499)
        Me.Panel1.TabIndex = 36
        '
        'btnHead
        '
        Me.btnHead.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(128, Byte), CType(128, Byte))
        Me.btnHead.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnHead.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHead.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHead.ForeColor = System.Drawing.Color.Gold
        Me.btnHead.Image = CType(resources.GetObject("btnHead.Image"), System.Drawing.Bitmap)
        Me.btnHead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnHead.ImageIndex = 0
        Me.btnHead.ImageList = Me.ImageList1
        Me.btnHead.Location = New System.Drawing.Point(8, 20)
        Me.btnHead.Name = "btnHead"
        Me.btnHead.Size = New System.Drawing.Size(88, 64)
        Me.btnHead.TabIndex = 4
        Me.btnHead.Text = "&Heads"
        Me.btnHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'pnlPalette
        '
        Me.pnlPalette.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.pnlPalette.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnPBlack, Me.btnMoreColours, Me.btnPCustom, Me.lblPCustom, Me.btnPPink, Me.btnPMagenta, Me.btnPBlue, Me.btnPLightBlue, Me.lblPPink, Me.lblPMagenta, Me.lblPBlue, Me.lblPLightBlue, Me.btnPCyan, Me.lblPCyan, Me.btnPLime, Me.btnPGreen, Me.btnPBrown, Me.btnPYellow, Me.lblPLime, Me.lblPGreen, Me.lblPBrown, Me.lblPYellow, Me.btnPOrange, Me.lblPOrange, Me.btnPRed, Me.lblPRed, Me.btnPWhite, Me.lblPWhite, Me.lblPBlack})
        Me.pnlPalette.Location = New System.Drawing.Point(104, 520)
        Me.pnlPalette.Name = "pnlPalette"
        Me.pnlPalette.Size = New System.Drawing.Size(496, 32)
        Me.pnlPalette.TabIndex = 91
        '
        'btnPBlack
        '
        Me.btnPBlack.BackColor = System.Drawing.Color.Black
        Me.btnPBlack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPBlack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPBlack.Location = New System.Drawing.Point(5, 5)
        Me.btnPBlack.Name = "btnPBlack"
        Me.btnPBlack.Size = New System.Drawing.Size(20, 20)
        Me.btnPBlack.TabIndex = 70
        Me.btnPBlack.TabStop = False
        '
        'btnMoreColours
        '
        Me.btnMoreColours.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMoreColours.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnMoreColours.Location = New System.Drawing.Point(388, 0)
        Me.btnMoreColours.Name = "btnMoreColours"
        Me.btnMoreColours.Size = New System.Drawing.Size(56, 31)
        Me.btnMoreColours.TabIndex = 98
        Me.btnMoreColours.Text = "&More"
        '
        'btnPCustom
        '
        Me.btnPCustom.BackColor = System.Drawing.Color.DarkTurquoise
        Me.btnPCustom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPCustom.Location = New System.Drawing.Point(457, 5)
        Me.btnPCustom.Name = "btnPCustom"
        Me.btnPCustom.Size = New System.Drawing.Size(20, 20)
        Me.btnPCustom.TabIndex = 97
        Me.btnPCustom.TabStop = False
        '
        'lblPCustom
        '
        Me.lblPCustom.BackColor = System.Drawing.SystemColors.Control
        Me.lblPCustom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPCustom.Location = New System.Drawing.Point(452, 0)
        Me.lblPCustom.Name = "lblPCustom"
        Me.lblPCustom.Size = New System.Drawing.Size(30, 30)
        Me.lblPCustom.TabIndex = 96
        '
        'btnPPink
        '
        Me.btnPPink.BackColor = System.Drawing.Color.Pink
        Me.btnPPink.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPPink.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPPink.Location = New System.Drawing.Point(352, 5)
        Me.btnPPink.Name = "btnPPink"
        Me.btnPPink.Size = New System.Drawing.Size(20, 20)
        Me.btnPPink.TabIndex = 95
        Me.btnPPink.TabStop = False
        '
        'btnPMagenta
        '
        Me.btnPMagenta.BackColor = System.Drawing.Color.Magenta
        Me.btnPMagenta.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPMagenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPMagenta.Location = New System.Drawing.Point(323, 5)
        Me.btnPMagenta.Name = "btnPMagenta"
        Me.btnPMagenta.Size = New System.Drawing.Size(20, 20)
        Me.btnPMagenta.TabIndex = 94
        Me.btnPMagenta.TabStop = False
        '
        'btnPBlue
        '
        Me.btnPBlue.BackColor = System.Drawing.Color.Blue
        Me.btnPBlue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPBlue.Location = New System.Drawing.Point(294, 5)
        Me.btnPBlue.Name = "btnPBlue"
        Me.btnPBlue.Size = New System.Drawing.Size(20, 20)
        Me.btnPBlue.TabIndex = 93
        Me.btnPBlue.TabStop = False
        '
        'btnPLightBlue
        '
        Me.btnPLightBlue.BackColor = System.Drawing.Color.LightBlue
        Me.btnPLightBlue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPLightBlue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPLightBlue.Location = New System.Drawing.Point(265, 5)
        Me.btnPLightBlue.Name = "btnPLightBlue"
        Me.btnPLightBlue.Size = New System.Drawing.Size(20, 20)
        Me.btnPLightBlue.TabIndex = 92
        Me.btnPLightBlue.TabStop = False
        '
        'lblPPink
        '
        Me.lblPPink.BackColor = System.Drawing.SystemColors.Control
        Me.lblPPink.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPPink.Location = New System.Drawing.Point(347, 0)
        Me.lblPPink.Name = "lblPPink"
        Me.lblPPink.Size = New System.Drawing.Size(30, 30)
        Me.lblPPink.TabIndex = 91
        '
        'lblPMagenta
        '
        Me.lblPMagenta.BackColor = System.Drawing.SystemColors.Control
        Me.lblPMagenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPMagenta.Location = New System.Drawing.Point(318, 0)
        Me.lblPMagenta.Name = "lblPMagenta"
        Me.lblPMagenta.Size = New System.Drawing.Size(30, 30)
        Me.lblPMagenta.TabIndex = 90
        '
        'lblPBlue
        '
        Me.lblPBlue.BackColor = System.Drawing.SystemColors.Control
        Me.lblPBlue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPBlue.Location = New System.Drawing.Point(289, 0)
        Me.lblPBlue.Name = "lblPBlue"
        Me.lblPBlue.Size = New System.Drawing.Size(30, 30)
        Me.lblPBlue.TabIndex = 89
        '
        'lblPLightBlue
        '
        Me.lblPLightBlue.BackColor = System.Drawing.SystemColors.Control
        Me.lblPLightBlue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPLightBlue.Location = New System.Drawing.Point(260, 0)
        Me.lblPLightBlue.Name = "lblPLightBlue"
        Me.lblPLightBlue.Size = New System.Drawing.Size(30, 30)
        Me.lblPLightBlue.TabIndex = 88
        '
        'btnPCyan
        '
        Me.btnPCyan.BackColor = System.Drawing.Color.Aqua
        Me.btnPCyan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPCyan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPCyan.Location = New System.Drawing.Point(236, 5)
        Me.btnPCyan.Name = "btnPCyan"
        Me.btnPCyan.Size = New System.Drawing.Size(20, 20)
        Me.btnPCyan.TabIndex = 86
        Me.btnPCyan.TabStop = False
        '
        'lblPCyan
        '
        Me.lblPCyan.BackColor = System.Drawing.SystemColors.Control
        Me.lblPCyan.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPCyan.Location = New System.Drawing.Point(231, 0)
        Me.lblPCyan.Name = "lblPCyan"
        Me.lblPCyan.Size = New System.Drawing.Size(30, 30)
        Me.lblPCyan.TabIndex = 87
        '
        'btnPLime
        '
        Me.btnPLime.BackColor = System.Drawing.Color.Lime
        Me.btnPLime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPLime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPLime.Location = New System.Drawing.Point(207, 5)
        Me.btnPLime.Name = "btnPLime"
        Me.btnPLime.Size = New System.Drawing.Size(20, 20)
        Me.btnPLime.TabIndex = 85
        Me.btnPLime.TabStop = False
        '
        'btnPGreen
        '
        Me.btnPGreen.BackColor = System.Drawing.Color.Green
        Me.btnPGreen.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPGreen.Location = New System.Drawing.Point(178, 5)
        Me.btnPGreen.Name = "btnPGreen"
        Me.btnPGreen.Size = New System.Drawing.Size(20, 20)
        Me.btnPGreen.TabIndex = 84
        Me.btnPGreen.TabStop = False
        '
        'btnPBrown
        '
        Me.btnPBrown.BackColor = System.Drawing.Color.DarkGoldenrod
        Me.btnPBrown.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPBrown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPBrown.Location = New System.Drawing.Point(150, 5)
        Me.btnPBrown.Name = "btnPBrown"
        Me.btnPBrown.Size = New System.Drawing.Size(20, 20)
        Me.btnPBrown.TabIndex = 83
        Me.btnPBrown.TabStop = False
        '
        'btnPYellow
        '
        Me.btnPYellow.BackColor = System.Drawing.Color.Yellow
        Me.btnPYellow.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPYellow.Location = New System.Drawing.Point(121, 5)
        Me.btnPYellow.Name = "btnPYellow"
        Me.btnPYellow.Size = New System.Drawing.Size(20, 20)
        Me.btnPYellow.TabIndex = 82
        Me.btnPYellow.TabStop = False
        '
        'lblPLime
        '
        Me.lblPLime.BackColor = System.Drawing.SystemColors.Control
        Me.lblPLime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPLime.Location = New System.Drawing.Point(202, 0)
        Me.lblPLime.Name = "lblPLime"
        Me.lblPLime.Size = New System.Drawing.Size(30, 30)
        Me.lblPLime.TabIndex = 81
        '
        'lblPGreen
        '
        Me.lblPGreen.BackColor = System.Drawing.SystemColors.Control
        Me.lblPGreen.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPGreen.Location = New System.Drawing.Point(174, 0)
        Me.lblPGreen.Name = "lblPGreen"
        Me.lblPGreen.Size = New System.Drawing.Size(30, 30)
        Me.lblPGreen.TabIndex = 80
        '
        'lblPBrown
        '
        Me.lblPBrown.BackColor = System.Drawing.SystemColors.Control
        Me.lblPBrown.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPBrown.Location = New System.Drawing.Point(145, 0)
        Me.lblPBrown.Name = "lblPBrown"
        Me.lblPBrown.Size = New System.Drawing.Size(30, 30)
        Me.lblPBrown.TabIndex = 79
        '
        'lblPYellow
        '
        Me.lblPYellow.BackColor = System.Drawing.SystemColors.Control
        Me.lblPYellow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPYellow.Location = New System.Drawing.Point(116, 0)
        Me.lblPYellow.Name = "lblPYellow"
        Me.lblPYellow.Size = New System.Drawing.Size(30, 30)
        Me.lblPYellow.TabIndex = 78
        '
        'btnPOrange
        '
        Me.btnPOrange.BackColor = System.Drawing.Color.Orange
        Me.btnPOrange.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPOrange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPOrange.Location = New System.Drawing.Point(92, 5)
        Me.btnPOrange.Name = "btnPOrange"
        Me.btnPOrange.Size = New System.Drawing.Size(20, 20)
        Me.btnPOrange.TabIndex = 76
        Me.btnPOrange.TabStop = False
        '
        'lblPOrange
        '
        Me.lblPOrange.BackColor = System.Drawing.SystemColors.Control
        Me.lblPOrange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPOrange.Location = New System.Drawing.Point(87, 0)
        Me.lblPOrange.Name = "lblPOrange"
        Me.lblPOrange.Size = New System.Drawing.Size(30, 30)
        Me.lblPOrange.TabIndex = 77
        '
        'btnPRed
        '
        Me.btnPRed.BackColor = System.Drawing.Color.Red
        Me.btnPRed.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPRed.Location = New System.Drawing.Point(63, 5)
        Me.btnPRed.Name = "btnPRed"
        Me.btnPRed.Size = New System.Drawing.Size(20, 20)
        Me.btnPRed.TabIndex = 74
        Me.btnPRed.TabStop = False
        '
        'lblPRed
        '
        Me.lblPRed.BackColor = System.Drawing.SystemColors.Control
        Me.lblPRed.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPRed.Location = New System.Drawing.Point(58, 0)
        Me.lblPRed.Name = "lblPRed"
        Me.lblPRed.Size = New System.Drawing.Size(30, 30)
        Me.lblPRed.TabIndex = 75
        '
        'btnPWhite
        '
        Me.btnPWhite.BackColor = System.Drawing.Color.White
        Me.btnPWhite.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPWhite.Location = New System.Drawing.Point(34, 5)
        Me.btnPWhite.Name = "btnPWhite"
        Me.btnPWhite.Size = New System.Drawing.Size(20, 20)
        Me.btnPWhite.TabIndex = 72
        Me.btnPWhite.TabStop = False
        '
        'lblPWhite
        '
        Me.lblPWhite.BackColor = System.Drawing.SystemColors.Control
        Me.lblPWhite.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPWhite.Location = New System.Drawing.Point(29, 0)
        Me.lblPWhite.Name = "lblPWhite"
        Me.lblPWhite.Size = New System.Drawing.Size(30, 30)
        Me.lblPWhite.TabIndex = 73
        '
        'lblPBlack
        '
        Me.lblPBlack.BackColor = System.Drawing.SystemColors.Control
        Me.lblPBlack.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPBlack.Name = "lblPBlack"
        Me.lblPBlack.Size = New System.Drawing.Size(30, 30)
        Me.lblPBlack.TabIndex = 71
        Me.lblPBlack.Tag = "Selected"
        '
        'rdoFreehand
        '
        Me.rdoFreehand.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.rdoFreehand.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdoFreehand.Checked = True
        Me.rdoFreehand.Location = New System.Drawing.Point(624, 44)
        Me.rdoFreehand.Name = "rdoFreehand"
        Me.rdoFreehand.Size = New System.Drawing.Size(40, 32)
        Me.rdoFreehand.TabIndex = 92
        Me.rdoFreehand.TabStop = True
        Me.rdoFreehand.Text = "Draw"
        Me.rdoFreehand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoFloodFill
        '
        Me.rdoFloodFill.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.rdoFloodFill.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdoFloodFill.Location = New System.Drawing.Point(672, 44)
        Me.rdoFloodFill.Name = "rdoFloodFill"
        Me.rdoFloodFill.Size = New System.Drawing.Size(40, 32)
        Me.rdoFloodFill.TabIndex = 93
        Me.rdoFloodFill.Text = "Fill"
        Me.rdoFloodFill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstBrushWidth
        '
        Me.lstBrushWidth.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstBrushWidth.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lstBrushWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstBrushWidth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBrushWidth.ItemHeight = 20
        Me.lstBrushWidth.Location = New System.Drawing.Point(640, 136)
        Me.lstBrushWidth.Name = "lstBrushWidth"
        Me.lstBrushWidth.Size = New System.Drawing.Size(56, 84)
        Me.lstBrushWidth.TabIndex = 94
        '
        'lblPen
        '
        Me.lblPen.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPen.Location = New System.Drawing.Point(8, 8)
        Me.lblPen.Name = "lblPen"
        Me.lblPen.Size = New System.Drawing.Size(64, 16)
        Me.lblPen.TabIndex = 99
        Me.lblPen.Text = "PEN"
        Me.lblPen.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnBlack
        '
        Me.btnBlack.BackColor = System.Drawing.Color.Black
        Me.btnBlack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBlack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBlack.Location = New System.Drawing.Point(48, 32)
        Me.btnBlack.Name = "btnBlack"
        Me.btnBlack.Size = New System.Drawing.Size(24, 23)
        Me.btnBlack.TabIndex = 96
        Me.btnBlack.TabStop = False
        '
        'btnWhite
        '
        Me.btnWhite.BackColor = System.Drawing.Color.White
        Me.btnWhite.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWhite.Location = New System.Drawing.Point(8, 32)
        Me.btnWhite.Name = "btnWhite"
        Me.btnWhite.Size = New System.Drawing.Size(24, 23)
        Me.btnWhite.TabIndex = 95
        Me.btnWhite.TabStop = False
        '
        'lblWhite
        '
        Me.lblWhite.BackColor = System.Drawing.Color.White
        Me.lblWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWhite.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWhite.Location = New System.Drawing.Point(0, 24)
        Me.lblWhite.Name = "lblWhite"
        Me.lblWhite.Size = New System.Drawing.Size(40, 40)
        Me.lblWhite.TabIndex = 97
        '
        'lblBlack
        '
        Me.lblBlack.BackColor = System.Drawing.Color.Red
        Me.lblBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBlack.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBlack.Location = New System.Drawing.Point(40, 24)
        Me.lblBlack.Name = "lblBlack"
        Me.lblBlack.Size = New System.Drawing.Size(40, 40)
        Me.lblBlack.TabIndex = 98
        '
        'pnlBWPens
        '
        Me.pnlBWPens.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.pnlBWPens.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblPen, Me.btnBlack, Me.btnWhite, Me.lblBlack, Me.lblWhite})
        Me.pnlBWPens.Location = New System.Drawing.Point(628, 16)
        Me.pnlBWPens.Name = "pnlBWPens"
        Me.pnlBWPens.Size = New System.Drawing.Size(80, 64)
        Me.pnlBWPens.TabIndex = 100
        '
        'btnBuy
        '
        Me.btnBuy.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnBuy.BackColor = System.Drawing.Color.Red
        Me.btnBuy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBuy.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnBuy.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuy.ForeColor = System.Drawing.Color.Gold
        Me.btnBuy.Location = New System.Drawing.Point(624, 436)
        Me.btnBuy.Name = "btnBuy"
        Me.btnBuy.Size = New System.Drawing.Size(88, 32)
        Me.btnBuy.TabIndex = 101
        Me.btnBuy.Text = "&Buy"
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(722, 594)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnBuy, Me.pnlBWPens, Me.rdoFloodFill, Me.rdoFreehand, Me.pnlPalette, Me.btnHead, Me.Panel1, Me.btnHelp, Me.btnLoad, Me.btnSave, Me.btnOther, Me.btnNoses, Me.btnMouths, Me.btnEyes, Me.btnClear, Me.StatusBar1, Me.btnEars, Me.chkGuide, Me.btnUndo, Me.btnPrint, Me.btnExit, Me.chkMirror, Me.lstBrushWidth})
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(720, 628)
        Me.Name = "frmMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kids Mask Print"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.StatusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnlPalette.ResumeLayout(False)
        Me.pnlBWPens.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
#Region "Menu options"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click

        'AddDebugComment("Form1.btnExit_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "Form1.btnExit_Click - start" 'JM 01/05/2005

        Me.Close()
        'AddDebugComment("Form1.btnExit_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BECEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuHelpSupportInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpSupportInfo.Click
        'added 'JM 15/08/2004
        AddDebugComment("Form1.mnuHelpSupportInfo_Click") 'JM 07/09/2004
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(HelpTopicEnum.support))


    End Sub
    Private Sub mnuFilePageSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePageSetup.Click

        'AddDebugComment("Form1.mnuFilePageSetup_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "Form1.mnuFilePageSetup_Click - start" 'JM 01/05/2005

        'added 'JM 27/08/2004
        DeactivatePaintingBeforeDialog() 'JM 09/09/2004

        Application.DoEvents()

        Try
            Dim PgSetupDlg As New PageSetupDialog()
            PgSetupDlg.PageSettings = m_PageSettings
            PgSetupDlg.AllowPrinter = True 'JM 22/10/2004
            PgSetupDlg.AllowPaper = True 'JM 22/10/2004
            PgSetupDlg.AllowMargins = True 'JM 22/10/2004
            PgSetupDlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ReactivatePaintingBeforeDialog() 'JM 09/09/2004

        'AddDebugComment("Form1.mnuFilePageSetup_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FPSEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click

        AddDebugComment("Form1.mnuFileExit_Click") 'JM 13/03/2005

        btnExit_Click(Nothing, Nothing)

    End Sub
    Private Sub mnuHelpHelpTopics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpHelpTopics.Click

        AddDebugComment("Form1.mnuHelpHelpTopics_Click") 'JM 07/09/2004

        'added 'JM 15/08/2004
        Help.ShowHelp(Me, GetHelpFile, HelpNavigator.TableOfContents)

    End Sub
    Private Sub mnuHelpCheckForUpdates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpCheckForUpdates.Click

        'added 'JM 15/08/2004
        'AddDebugComment("frmMain.mnuHelpCFU_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpCFU_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        Application.DoEvents() 'JM 09/09/2004

        If hasMultipleInstances(gProgName, NameMe(""), Me.Handle, StandLangText) = True Then
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
            Exit Sub
        End If

        Application.DoEvents()

        Dim NewCFU As New frmCFU(True)
        With NewCFU
            .Caption = NameMe("")
            .FormIcon = Me.Icon
            .strManifestSite(gstrManifestSite)
            .Owner = Me
            .ShowDialog()
        End With

        If gbooNeedToRestartProgAfterCFU = True Then 'JM 28/09/2004
            SaveBeforeExitProg() 'JM 28/09/2004
        End If

        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        'AddDebugComment("frmMain.mnuHelpCFU_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HCUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
        If gbooNeedToRestartProgAfterCFU = True Then
            SaveSetting("BuyPackShowNext", True, InitalXMLConfig.XmlConfigType.AppSettings, "") 'JM 11/09/2004

            Me.Close()
        End If

    End Sub
    Private Sub mnuHelpInstallAddon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpInstallAddon.Click
        'added 'JM 15/08/2004
        'AddDebugComment("frmMain.mnuHelpInstallPack_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpInstallPack_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim lstrAddOnFile As String

        Application.DoEvents()

        If hasMultipleInstances(gProgName, NameMe(""), Me.Handle, StandLangText) = True Then
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
            Exit Sub
        End If

        Application.DoEvents()

        Dim OpenAddon As New OpenFileDialog()
        With OpenAddon
            .CheckFileExists = True
            .CheckPathExists = True
            .Filter = "Mindwarp Consultancy Ltd AddOn (*.mcla;*.zip)|*.mcla;*.zip"
            If .ShowDialog() <> DialogResult.OK Then
                ReactivatePaintingBeforeDialog() 'JM 21/08/2004
                Exit Sub
            End If
            lstrAddOnFile = .FileName

        End With

        Busy(Me, True)
        Dim lstrDat As Date = Date.Now

        gstrCFUTempDir = System.IO.Path.GetDirectoryName( _
            System.Reflection.Assembly.GetEntryAssembly.Location.ToString()) & "\Temp-" & _
            lstrDat.ToString("dddd-dd-MMM-yyyy-HH-mm-ss")
        Dim LangText2 As System.Resources.ResourceManager = New _
            System.Resources.ResourceManager("AppBasic.AppBasic", _
            System.Reflection.Assembly.Load("AppBasic"))

        Try
            System.IO.Directory.CreateDirectory(gstrCFUTempDir & "\unzip")
            AppBasic.UpdateFuncs.Unzip(lstrAddOnFile, gstrCFUTempDir & "\unzip\")

            Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings, "", gstrCFUTempDir & "\unzip\addon.dat")
            With InitialConfig

                If AppBasic.IsCompatible(.GetValue("AppVersion", "")) = False Then
                    Directory.Delete(gstrCFUTempDir, True)
                    Throw New Exception(" ")
                End If

            End With

            gbooNeedToRestartProgAfterCFU = True
            Try : File.Delete(gstrCFUTempDir & "\unzip\addon.dat") : Catch : End Try
            Busy(Me, False)
            Application.DoEvents()

            MessageBox.Show(LangText2.GetString("CFU_ProgRestart"), NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)

            SaveBeforeExitProg() 'JM 28/09/2004
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
            'AddDebugComment("frmMain.mnuHelpInstallPack_Click - middle") 'JM 07/09/2004
            gstrProbComtStack &= " #middle"
            Me.Close()
            Try : Me.Close() : Catch : End Try
        Catch
            Busy(Me, False)

            MessageBox.Show(LangText2.GetString("CFU_DownloadIncompatible"), NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
            Try : File.Delete(gstrCFUTempDir & "\unzip\addon.dat") : Catch : End Try
        End Try

        'AddDebugComment("frmMain.mnuHelpInstallPack_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HIAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        'added 'JM 15/08/2004
        'AddDebugComment("frmMain.mnuHelpAbout_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpAbout_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        Application.DoEvents() 'JM 09/09/2004

        Dim NewAbout As New frmAbout()
        With NewAbout
            .Owner = Me
            .ShowDialog()
        End With

        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        'AddDebugComment("frmMain.mnuHelpAbout_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuHelpSubscribeNewsletter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpSubscribeNewsletter.Click
        'added 'JM 15/08/2004
        'AddDebugComment("frmMain.mnuHelpSubscribe_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpSubscribe_Click - start" 'JM 01/05/2005

        Dim NewsString As String = "mailto:newsletter@KidsMaskPrint.com?" & _
                "subject=Newsletter Subscriptions Dept&body=Dear Sirs,  Kindly add me to your Kids Mask Print Newsletter!"

        Try
            Process.Start(NewsString)
        Catch

            Dim Email As String
            Email = LeftGet(NewsString, InStrGet(NewsString.ToUpper, "?subject".ToUpper) - 1).Replace("mailto:", "")
            Dim msg As String = "Please check you that you have an email program installed on your computer (e.g. Outlook)." & _
                Environment.NewLine & "Alternatively, send an email to " & Email & " to be added to the newsletter mailing list."

            DeactivatePaintingBeforeDialog() 'JM 21/08/2004
            MessageBox.Show(msg, NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        End Try

        'AddDebugComment("frmMain.mnuHelpSubscribe_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HSNEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuHelpEnterCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpEnterCode.Click
        'added 'JM 15/08/2004       

        'AddDebugComment("frmMain.mnuHelpEnterCode_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpEnterCode_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Application.DoEvents() 'JM 09/09/2004

        If AcceptLicense(Me) = True Then
            Me.Text = NameMe("")
            StandardUpgradeTidy()
            SaveBeforeExitProg() 'JM 28/09/2004
            Me.Close()
        End If

        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        'AddDebugComment("frmMain.mnuHelpEnterCode_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HECEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuHelpLicenseAgreement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpLicenseAgreement.Click
        'added 'JM 15/08/2004
        'AddDebugComment("frmMain.mnuHelpLicense_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuHelpLicense_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        Application.DoEvents() 'JM 09/09/2004

        Dim LicenseBox As New LicenceBox()
        With LicenseBox
            .FormIcon = Me.Icon
            If InStrGet((NameMe("")).ToUpper, "TRIAL") = 0 Then
                Dim lstrLang2Char As String = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
                Select Case lstrLang2Char
                    Case "de"
                        .LicenseSectionSkip = ", auszuleihen"
                    Case Else
                        .LicenseSectionSkip = "loan, copy, "
                End Select

            End If
            .ProdName = NameMe("").ToUpper
            .SetPageSettings = m_PageSettings
            .Owner = Me

            Application.DoEvents()

            .ShowDialog()
            m_PageSettings = .SetPageSettings
        End With

        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        'AddDebugComment("frmMain.mnuHelpLicense_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #HLAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileImportGraphics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileImportGraphics.Click

        'AddDebugComment("frmMain.mnuFileImportGraphics_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileImportGraphics_Click - start" 'JM 01/05/2005

        Dim fs As IO.FileStream = New IO.FileStream("D:\desktopnt\mask.gif", IO.FileMode.Open, IO.FileAccess.Read)
        Dim img As System.Drawing.Bitmap = New System.Drawing.Bitmap(fs)
        fs.Close()

        'btnClear_Click(Nothing, Nothing) 'JM 28/08/2004
        Clear()

        'AddDebugComment("frmMain.mnuFileImportGraphics_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FIGEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileExportGraphics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExportGraphics.Click

        '--- 'JM 01/09/2005 ---
        If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
            Dim dlgRes As DialogResult
            dlgRes = MessageBox.Show("This feature is only available in the full version of this program" & CR() & CR() & _
                "Would you like to visit our website to view purchasing options?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgRes = DialogResult.Yes Then
                BrowseToUrl("http://www.KidsMaskPrint.com/buy.php", Me)
            End If
            Exit Sub
        End If
        '--- 'JM 01/09/2005 ---


        ' Displays a SaveFileDialog so the user can save
        ' the Image
        'AddDebugComment("frmMain.mnuFileExportGraphics_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileExportGraphics_Click - start" 'JM 01/05/2005
        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Gif Image|*.gif|" & "Jpeg Image|*.jpg|" & "Bitmap Image|*.bmp|" & "Tiff Image|*.tiff"
        saveFileDialog1.Title = "Save an Image File"
        saveFileDialog1.ShowDialog()
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        Me.Update() 'JM 12/08/2004

        ' If the file name is not an empty string open it for
        ' saving.
        If saveFileDialog1.FileName <> "" Then

            ' Saves the Image via a FileStream created by the
            ' OpenFile method.
            Dim fs As System.IO.FileStream = CType _
            (saveFileDialog1.OpenFile(), System.IO.FileStream)

            'JM 23/09/2004
            Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                mPieces, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData)

            ' Saves the Image in the appropriate ImageFormat
            ' based upon the file type selected in the dialog box.
            Select Case saveFileDialog1.FilterIndex
                Case 2
                    FullImage.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg)
                Case 3
                    FullImage.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp)
                Case 1
                    FullImage.Save(fs, System.Drawing.Imaging.ImageFormat.Gif)
                Case 4
                    FullImage.Save(fs, System.Drawing.Imaging.ImageFormat.Tiff)
            End Select
            fs.Close()
        End If

        PictureBox1.Invalidate() 'JM 19/10/2004

        'AddDebugComment("frmMain.mnuFileExportGraphics_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FEGEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileImportMask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileImportMask.Click
        gstrMRPs = "0300"
        'AddDebugComment("frmMain.mnuFileImportMask_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileImportMask_Click - start" 'JM 01/05/2005

        Dim ImportedText As String

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim ie As New ImpEx()
        ie.Label = "Please paste in your import mask codes"
        ie.Caption = NameMe("Import Mask")
        ie.Owner = Me 'JM 21/08/2004
        ie.ShowDialog()
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        Me.Update() 'JM 12/08/2004

        ImportedText = ie.TextBlock

        '--- 'JM 17/10/2004 ---
        If ie.TextBlock = "" Then
            Exit Sub
        End If
        'btnClear_Click(Nothing, Nothing)
        Clear() 'JM 18/10/2004
        '--- 'JM 17/10/2004 ---

        Busy(Me, True) 'JM 19/10/2004

        If RightGet(ImportedText, 1) <> Environment.NewLine Then 'JM 19/10/2004
            ImportedText &= Environment.NewLine 'JM 19/10/2004
        End If

        Try 'JM 19/10/2004
            Dim lintArrInc As Integer
            Dim de As String() = Microsoft.VisualBasic.Split(ImportedText, Environment.NewLine)
            For lintArrInc = 0 To de.GetUpperBound(0) - 1
                Dim ThisItem As String = de(lintArrInc)
                If de(0) = "-1" Then
                    'lstrVersion = de.Value
                ElseIf ThisItem = "" Then
                    'do nothing
                Else

                    Dim ThisPiece As New Piece() ' ("D:\desktopnt\scraps\flag.png")

                    ThisPiece.SourceDataFileName = AppBasic.ReturnNthStr(de(lintArrInc), 1, "|").Replace(fpDir, "")
                    ThisPiece.DataFileItemNum = CInt(AppBasic.ReturnNthStr(de(lintArrInc), 2, "|"))
                    ThisPiece.VertFlip = (CBool(AppBasic.ReturnNthStr(de(lintArrInc), 5, "|")))
                    ThisPiece.HorizFlip = (CBool(AppBasic.ReturnNthStr(de(lintArrInc), 6, "|")))

                    '--- 'JM 19/08/2004 ---
                    Dim TempPart As New KidsMaskPrint.Part()
                    'GetDataFileImageItem(ThisPiece.SourceDataFileName, ThisPiece.DataFileItemNum, TempPart, Nothing)
                    GetDataPreviewImage(ThisPiece.SourceDataFileName, ThisPiece.DataFileItemNum, TempPart, Nothing, Nothing) 'JM 23/09/2004
                    ThisPiece.SetImageObj(TempPart.FullImage)
                    '--- 'JM 19/08/2004 ---

                    'don't bother loading thumb image
                    'Dim loc As New Point(CSng(AppBasic.ReturnNthStr(de(lintArrInc), 1, "|")), CSng(AppBasic.ReturnNthStr(de(lintArrInc), 2, "|")))
                    'JM 19/08/2004
                    Dim loc As New Point(CSng(AppBasic.ReturnNthStr(de(lintArrInc), 3, "|")), CSng(AppBasic.ReturnNthStr(de(lintArrInc), 4, "|")))
                    ThisPiece.Location = loc

                    mPieces.Add(ThisPiece)
                    'will need to just build a incremental list
                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "mnuFileImportMask_Click") 'JM 14/10/2004
                    ChangeUndoRedoStatus() 'JM 17/10/2004
                End If
            Next lintArrInc

        Catch 'JM 19/10/2004
            'JM 19/10/2004
            Busy(Me, False) 'JM 19/10/2004
            MessageBox.Show("Please ensure that the mask data contains the exact details provided.  Remove any extra letters etc.", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        Busy(Me, False) 'JM 19/10/2004
        'AddDebugComment("frmMain.mnuFileImportMask_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FIMEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileExportMask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExportMask.Click
        gstrMRPs = "0310"
        'AddDebugComment("frmMain.mnuFileExportMask_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileExportMask_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim str As String
        Dim iPiece As Piece
        Dim lintArrInc As Integer
        For Each iPiece In mPieces

            str &= iPiece.SourceDataFileName.Replace(fpDir, "") & "|" & iPiece.DataFileItemNum & "|" & iPiece.Location.X & "|" & iPiece.Location.Y & "|" & iPiece.VertFlip & "|" & iPiece.HorizFlip & "|" & Environment.NewLine

            lintArrInc += 1
        Next iPiece

        Dim ie As New ImpEx()
        ie.Label = "Please copy your masks codes below."
        ie.Caption = NameMe("Export Mask")
        ie.TextBlock = str
        ie.Owner = Me 'JM 21/08/2004
        ie.ShowDialog()
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        Me.Update() 'JM 12/08/2004

        'AddDebugComment("frmMain.mnuFileExportMask_Click - end") 'JM 07/09/2004

        gstrProbComtStack &= " #FEMEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileLoad.Click

        'AddDebugComment("frmMain.mnuFileLoad_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileLoad_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim MaskFile As String 'JM 26/07/2004
        Dim sm As New SelectMask()
        'SetStyle(ControlStyles.DoubleBuffer, False) 'JM 17/07/2004
        'sm.ShowDialog()
        'SetStyle(ControlStyles.DoubleBuffer, True) 'JM 17/07/2004
        sm.LicensedFaceParts = mLicensedFaceParts 'JM 19/09/2004

        sm.Owner = Me 'JM 21/08/2004
        sm.ShowDialog()


        Me.Update() 'JM 12/08/2004
        MaskFile = sm.RetMaskFile

        If MaskFile = "" Then Exit Sub

        'LoadMask(MaskFile, mPieces, PictureBox2.Image, False) 'JM 21/08/2004
        'x'LoadMask(MaskFile, mPieces, PictureBox2.Image, False, mousePath, ReversemousePath, lPaintBrush, lPaintReverseBrush) 'JM 27/08/2004
        'JM 30/08/2004
        'LoadMask(MaskFile, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
        '    m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts)

        'JM 13/10/2004
        LoadMask(MaskFile, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
            m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)

        m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

        AddDebugComment("frmMain.mnuFileLoad_Click - middle") 'JM 07/09/2004

        ReactivatePaintingBeforeDialog() 'JM 21/08/2004

        'AddDebugComment("frmMain.mnuFileLoad_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FLEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuFileSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click

        'AddDebugComment("frmMain.mnuFileSave_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.mnuFileSave_Click - start" 'JM 01/05/2005

        '--- 'JM 27/07/2004 ---
        Dim FileName As String = Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location) & "\" & "\Masks\" 'data.txt"
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Mask File|*.mask"
        saveFileDialog1.Title = "Save a mask File"
        saveFileDialog1.InitialDirectory = FileName
        saveFileDialog1.ShowDialog()
        Me.Update() 'JM 12/08/2004

        ' If the file name is not an empty string open it for        
        If saveFileDialog1.FileName = "" Then Exit Sub
        '--- 'JM 27/07/2004 ---

        FileName = saveFileDialog1.FileName 'JM 18/08/2004

        '--- New Section ---
        Dim FullImage As Image
        Dim hash As New SortedList()
        'Dim img As Image
        Dim keptPieces As New ArrayList()

        CreateHashAndImage(hash, FullImage, keptPieces)

        mPieces = keptPieces
        '--- New Section ---

        'SaveUserMask(FileName, hash, img, FullImage) 'JM 20/08/2004
        'SaveUserMask(FileName, hash, FullImage)  'JM 20/08/2004
        SaveUserMask(FileName, hash, FullImage, m_UserPieces, m_SortOrderForData)      'JM 13/10/2004

        MessageBox.Show("Saved!", NameMe("")) 'JM 16/09/2005

        'AddDebugComment("frmMain.mnuFileSave_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FSEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuToolsDeleteUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsDeleteUsers.Click

        'AddDebugComment("frmMain.mnuToolsDeleteUsers_Click - start")
        gstrProbComtStack = "frmMain.mnuToolsDeleteUsers_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog()

        SaveBeforeExitProg() 'JM 28/09/2004

        Dim DUs As New UsersGeneral()
        DUs.Owner = Me
        DUs.TranType = UsersGeneral.UserTranType.Delete
        DUs.ShowDialog()

        ReactivatePaintingBeforeDialog()

        'AddDebugComment("frmMain.mnuToolsDeleteUsers_Click - end")
        gstrProbComtStack &= " #TDUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub mnuToolsRenameUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsRenameUsers.Click

        'AddDebugComment("frmMain.mnuToolsRenameUsers_Click - start")
        gstrProbComtStack = "frmMain.mnuToolsRenameUsers_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog()

        SaveBeforeExitProg() 'JM 28/09/2004

        Dim RUs As New UsersGeneral()
        RUs.Owner = Me
        RUs.TranType = UsersGeneral.UserTranType.Rename
        RUs.ShowDialog()

        'just in case current logged user name is renamed.
        If RUs.LoginInAs <> "" Then
            mSelectedUser = RUs.LoginInAs
        End If

        ReactivatePaintingBeforeDialog()

        'AddDebugComment("frmMain.mnuToolsRenameUsers_Click - end")
        gstrProbComtStack &= " #TRUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub


#End Region
#Region "Various Functions"
    Private Sub CreateHashAndImage(ByRef pHash As SortedList, ByRef pFullRetImage As Image, _
      ByRef pKeptPieces As ArrayList)

        AddDebugComment("frmMain.CreateHashAndImage - start") 'JM 07/09/2004

        Busy(Me, True) 'JM 21/09/2004

        'DebugFile(mousePath, ReversemousePath, lPaintBrush, lPaintReverseBrush)

        'produce debug report of all values about to be saved

        'Dim FullImage As Image =DrawingWithoutHelpers()
        'JM 22/09/2004
        Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
            mPieces, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData) ' DrawingWithoutHelpers()

        Dim hash As New SortedList()

        hash.Add("AVER", "1.0")

        Dim iPiece As Piece
        Dim lintArrInc As Integer
        For Each iPiece In mPieces
            Dim str As String
            'str = iPiece.Location.X & "|" & iPiece.Location.Y & "|" & iPiece.Bitmapname & "|" & iPiece.VertFlip & "|" & iPiece.HorizFlip & "|"
            'JM 19/08/2004            
            'str = iPiece.SourceDataFileName.Replace(fpDir, "") & "|" & iPiece.DataFileItemNum & "|" & iPiece.Location.X & "|" & iPiece.Location.Y & "|" & iPiece.VertFlip & "|" & iPiece.HorizFlip & "|" ' & ControlChars.CrLf
            'JM 19/09/2004
            str = iPiece.SourceDataFileName.Replace(fpDir, "") & "|" & iPiece.DataFileItemNum & "|" & iPiece.Location.X & _
                "|" & iPiece.Location.Y & "|" & iPiece.VertFlip & "|" & iPiece.HorizFlip & "|" & iPiece.PieceName & "|"
            'Console.WriteLine(str)

            'hash.Add(lintArrInc, str) 'iPiece.Location)
            hash.Add("ZZZZ" & lintArrInc.ToString, str) 'JM 27/08/2004
            lintArrInc += 1
        Next iPiece


        With m_Drawings
            '--- 'JM 28/08/2004 ---
            hash.Add("BCNT", .mousePath.GetUpperBound(0) & "#" & .ReversemousePath.GetUpperBound(0) & "#")

            For lintArrInc = 0 To .mousePath.GetUpperBound(0)
                Try : hash.Add("CMPP" & lintArrInc, .mousePath(lintArrInc).PathPoints)
                Catch
                    hash.Add("CMPP" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .mousePath.GetUpperBound(0)
                Try : hash.Add("DMPT" & lintArrInc, .mousePath(lintArrInc).PathTypes)
                Catch
                    hash.Add("DMPT" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .lPaintBrush.GetUpperBound(0)
                Try : hash.Add("EMBC" & lintArrInc, .lPaintBrush(lintArrInc).BrushColour)
                Catch
                    hash.Add("EMBC" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .lPaintBrush.GetUpperBound(0)
                Try : hash.Add("FMBW" & lintArrInc, .lPaintBrush(lintArrInc).BrushWidth)
                Catch
                    hash.Add("FMBW" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .ReversemousePath.GetUpperBound(0)
                Try : hash.Add("GRPP" & lintArrInc, .ReversemousePath(lintArrInc).PathPoints)
                Catch
                    hash.Add("GRPP" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .ReversemousePath.GetUpperBound(0)
                Try : hash.Add("HRPT" & lintArrInc, .ReversemousePath(lintArrInc).PathTypes)
                Catch
                    hash.Add("HRPT" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .lPaintReverseBrush.GetUpperBound(0)
                Try : hash.Add("IRBC" & lintArrInc, .lPaintReverseBrush(lintArrInc).BrushColour)
                Catch
                    hash.Add("IRBC" & lintArrInc, Nothing)
                End Try
            Next lintArrInc

            For lintArrInc = 0 To .lPaintReverseBrush.GetUpperBound(0)
                Try : hash.Add("JRBW" & lintArrInc, .lPaintReverseBrush(lintArrInc).BrushWidth)
                Catch
                    hash.Add("JRBW" & lintArrInc, Nothing)
                End Try
            Next lintArrInc
            '--- 'JM 28/08/2004 ---

        End With

        Dim keptPieces As New ArrayList()
        keptPieces = mPieces
        mPieces = Nothing

        'Dim img As Image = DrawingWithoutHelpers()

        pHash = hash
        'pRetImage = img
        pFullRetImage = FullImage
        pKeptPieces = keptPieces

        Busy(Me, False) 'JM 21/09/2004

        AddDebugComment("frmMain.CreateHashAndImage - end") 'JM 07/09/2004

    End Sub
    Private Sub LoadFaceParts(ByRef pDataFilesDescImages As ArrayList, ByRef pDataFilesDescriptions As ArrayList, _
        ByRef pDataFilesProdNums As ArrayList, ByRef pDataFiles As ArrayList, ByRef pDataFileState As ArrayList, _
        ByVal pbooJustPurchasing As Boolean, ByRef pLicensedFaceParts As ArrayList, ByRef LicenseStr As String)

        AddDebugComment("frmMain.LoadFaceParts - start") 'JM 07/09/2004

        Busy(Me, True) 'JM 21/09/2004

        'Dim FileName As String

        '-------------- Check License if available -------------
        Dim Dets2 As strat1.UnlockDetails
        Dim Info As New System.IO.FileInfo(System.IO.Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")

        If Info.Exists Then
            Try
                Unlock(System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", Dets2, "", "")
                '--- 'JM 02/10/2004 ---
                If Dets2.str1Name.ToUpper <> "UNAVAILABLE" Then
                    LicenseStr = Dets2.str1Name & ", "
                End If
                If Dets2.str4State.ToUpper <> "UNAVAILABLE" Then
                    LicenseStr &= Dets2.str4State & ", "
                End If
                If Dets2.str6Country.ToUpper <> "UNAVAILABLE" Then
                    LicenseStr &= Dets2.str6Country & ", "
                End If

                LicenseStr &= Dets2.str7Email

                LicenseStr = ProperCase(LicenseStr)
                '--- 'JM 02/10/2004 ---
            Catch

            End Try
        End If

        '-------------- Check License if available -------------


        ''Console.WriteLine("Start " & Date.Now)

        Dim source As DirectoryInfo = New DirectoryInfo(Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location) & "\FaceParts\")

        'iterate data file directory
        Dim files() As FileInfo = source.GetFiles("*.dat")
        Dim pfile As FileInfo

        'MessageBox.Show(files.GetUpperBound(0)) '#############

        For Each pfile In files
            'MessageBox.Show(pfile.FullName) '#############
            Try ' this will cater for old data files

                Dim FPs As FacePartStuctureDataFile = UnlockFacePartsPack(pfile.FullName) 'JM 23/09/2004

                If pfile.Name.ToLower <> "basic.dat" Then 'JM 22/09/2004
                    '--- 'JM 08/09/2004 ---
                    pDataFilesDescImages.Add(FPs.DescImage)
                    pDataFilesDescriptions.Add(FPs.Description)
                    pDataFilesProdNums.Add(FPs.ProductNumber)
                    pDataFiles.Add(pfile.FullName)

                    '--- this block checks for a valid key file and doesn't all it to be used if it isn't ---

                    Dim keyFile As String = pfile.FullName.ToLower.Replace(".dat", ".key")
                    If File.Exists(keyFile) = True Then
                        Dim Dets As strat1.UnlockDetails
                        Dim lintResult As Integer

                        Try
                            lintResult = Unlock(keyFile, Dets, FPs.ProductNumber, Dets2.strSerialBlock)
                        Catch
                            lintResult = 3
                        End Try

                        If lintResult <> 257 Then
                            pDataFileState.Add("0")
                            'MessageBox.Show("Point 1 " & pfile.FullName)
                            Throw New Exception(" ")
                        End If
                    Else
                        pDataFileState.Add("0")
                        'MessageBox.Show("Point 2" & pfile.FullName)
                        Throw New Exception(" ")
                    End If

                    'moved up slightly'JM 28/09/2004
                    pDataFileState.Add("1") ' = OK
                End If 'JM 22/09/2004
                'pDataFileState.Add("1") ' = OK
                'MessageBox.Show("Point 3" & pfile.FullName)

                '--- this block checks for a valid key file and doesn't all it to be used if it isn't ---
                '--- 'JM 08/09/2004 ---

                If pbooJustPurchasing = True Then 'JM 11/09/2004
                    Throw New Exception(" ")
                End If

                Dim lintArrInc As Integer
                For lintArrInc = 0 To FPs.Parts.Count - 1
                    Dim ThisPart As New KidsMaskPrint.Part()
                    ThisPart = FPs.Parts(lintArrInc)

                    pLicensedFaceParts.Add(ThisPart.FaceMaster) 'JM 19/09/2004

                Next lintArrInc
            Catch ex As Exception
                ' MessageBox.Show(ex.ToString) '###

            End Try
        Next pfile

        Busy(Me, False) 'JM 21/09/2004

        ''Console.WriteLine("End " & Date.Now)
        AddDebugComment("frmMain.LoadFaceParts - end") 'JM 07/09/2004

    End Sub
    Private Sub xAddSelectedFacePart(ByVal pFP As Part, ByVal pSel As FacePartEnums.ePositionSelection, _
        ByVal SourceDatFileName As String, ByVal DataFileItemNum As Integer)

        AddDebugComment("frmMain.AddSelectedFacePart - start") 'JM 07/09/2004

        Busy(Me, True) 'JM 19/10/2004

        If Not pFP Is Nothing Then

            Select Case pSel
                Case FacePartEnums.ePositionSelection.Left
                    Dim ThisPiece As New Piece()
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.LeftPart
                    ''ThisPiece.Bitmapname = pFP.FaceMaster '& " Left"
                    ThisPiece.PieceName = pFP.FaceMaster 'JM 19/09/2004
                    ThisPiece.SourceDataFileName = SourceDatFileName 'JM 19/08/2004
                    ThisPiece.DataFileItemNum = DataFileItemNum 'JM 19/08/2004
                    mPieces.Add(ThisPiece)
                Case FacePartEnums.ePositionSelection.Both

                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = False
                    thispiece.VertFlip = False
                    ThisPiece.SetImageObj(pFP.FullImage.Clone) 'added clone 'JM 12/08/2004
                    ThisPiece.Location = pFP.LeftPart
                    ThisPiece.PieceName = pFP.FaceMaster 'JM 19/09/2004
                    ''ThisPiece.Bitmapname = pFP.FaceMaster '& " Left"
                    ThisPiece.SourceDataFileName = SourceDatFileName 'JM 19/08/2004
                    ThisPiece.DataFileItemNum = DataFileItemNum 'JM 19/08/2004
                    mPieces.Add(ThisPiece)

                    Dim ThisPiece2 As New Piece()
                    ThisPiece2.HorizFlip = True
                    ThisPiece2.VertFlip = False
                    ThisPiece2.SetImageObj(pFP.FullImage)
                    ThisPiece2.Location = pFP.RightPart
                    ThisPiece2.PieceName = pFP.FaceMaster 'JM 19/09/2004
                    ''ThisPiece2.Bitmapname = pFP.FaceMaster ' & " Right"
                    ThisPiece2.SourceDataFileName = SourceDatFileName 'JM 19/08/2004
                    ThisPiece2.DataFileItemNum = DataFileItemNum 'JM 19/08/2004
                    mPieces.Add(ThisPiece2)

                Case FacePartEnums.ePositionSelection.Right
                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = True
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.RightPart
                    ThisPiece.PieceName = pFP.FaceMaster 'JM 19/09/2004
                    ''ThisPiece.Bitmapname = pFP.FaceMaster '& " Right"
                    ThisPiece.SourceDataFileName = SourceDatFileName 'JM 19/08/2004
                    ThisPiece.DataFileItemNum = DataFileItemNum 'JM 19/08/2004

                    mPieces.Add(ThisPiece)
            End Select

            m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "AddSelectedFacePart") 'JM 14/10/2004
            ChangeUndoRedoStatus() 'JM 17/10/2004
        End If

        Busy(Me, False) 'JM 19/10/2004

        Me.Update() 'JM 12/08/2004

        PictureBox1.Invalidate() 'JM 25/09/2004

        AddDebugComment("frmMain.AddSelectedFacePart - end") 'JM 07/09/2004

    End Sub
    Private Sub DeactivatePaintingBeforeDialog()
        'added 'JM 21/08/2004
        lbooAllowPainting = False

    End Sub
    Private Sub ReactivatePaintingBeforeDialog()
        'added 'JM 21/08/2004
        lbooAllowPainting = True

        PictureBox1.Invalidate() 'JM 22/10/2004

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)


        'added 'JM 13/08/2004
        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)
        Me.Update() 'JM 13/08/2004

    End Sub
    Private Sub SetBackcolors()

        AddDebugComment("frmMain.SetBackcolors - start") 'JM 07/09/2004

        'added 'JM 13/08/2004
        chkMirror.BackColor = Color.FromArgb(0, chkMirror.BackColor)
        chkGuide.BackColor = Color.FromArgb(0, chkGuide.BackColor)
        'btnExit.BackColor = Color.FromArgb(0, btnExit.BackColor)
        'btnPrint.BackColor = Color.FromArgb(0, btnPrint.BackColor)
        'btnUndo.BackColor = Color.FromArgb(0, btnUndo.BackColor)
        'btnClear.BackColor = Color.FromArgb(0, btnClear.BackColor)
        'btnEyes.BackColor = Color.FromArgb(0, btnEyes.BackColor)
        'btnEars.BackColor = Color.FromArgb(0, btnEars.BackColor)
        'btnMouths.BackColor = Color.FromArgb(0, btnMouths.BackColor)
        'btnNoses.BackColor = Color.FromArgb(0, btnNoses.BackColor)
        'btnOther.BackColor = Color.FromArgb(0, btnOther.BackColor)
        lblPen.BackColor = Color.FromArgb(0, lblPen.BackColor)

        pnlBWPens.BackColor = Color.FromArgb(0, pnlBWPens.BackColor) 'JM 18/10/2004
        pnlPalette.BackColor = Color.FromArgb(0, pnlPalette.BackColor)

        lblPBlack.BackColor = Color.FromArgb(0, lblPBlack.BackColor)
        lblPBlack.BackColor = Color.FromArgb(0, lblPBlack.BackColor)
        lblPWhite.BackColor = Color.FromArgb(0, lblPWhite.BackColor)
        lblPRed.BackColor = Color.FromArgb(0, lblPRed.BackColor)
        lblPOrange.BackColor = Color.FromArgb(0, lblPOrange.BackColor)
        lblPYellow.BackColor = Color.FromArgb(0, lblPYellow.BackColor)
        lblPBrown.BackColor = Color.FromArgb(0, lblPBrown.BackColor)
        lblPGreen.BackColor = Color.FromArgb(0, lblPGreen.BackColor)
        lblPLime.BackColor = Color.FromArgb(0, lblPLime.BackColor)
        lblPCyan.BackColor = Color.FromArgb(0, lblPCyan.BackColor)
        lblPLightBlue.BackColor = Color.FromArgb(0, lblPLightBlue.BackColor)
        lblPBlue.BackColor = Color.FromArgb(0, lblPBlue.BackColor)
        lblPMagenta.BackColor = Color.FromArgb(0, lblPMagenta.BackColor)
        lblPPink.BackColor = Color.FromArgb(0, lblPPink.BackColor)
        lblPCustom.BackColor = Color.FromArgb(0, lblPCustom.BackColor)



        AddDebugComment("frmMain.SetBackcolors - end") 'JM 07/09/2004

    End Sub
    Private Function KidsSave(ByVal pbooIsLastMask As Boolean)

        AddDebugComment("frmMain.KidsSave - start") 'JM 07/09/2004

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        'added 'JM 20/08/2004
        Dim FullImage As Image
        Dim hash As New SortedList()
        ' Dim img As Image
        Dim keptPieces As New ArrayList()

        m_Drawings.PreSave(m_CurrentColour, m_CurrentBrushWidth) 'JM 11/10/2004

        '--- 'JM 26/09/2004 ---

        CreateHashAndImage(hash, FullImage, keptPieces)

        mPieces = keptPieces

        Dim SaveSlots As New Slots()
        With SaveSlots
            .Owner = Me 'JM 21/08/2004
            .TranType = Slots.eTranType.Save
            .SelectedUser = mSelectedUser
            '.FaceImage = img
            .FullImage = FullImage
            .FaceHash = hash
            .LastMask = pbooIsLastMask 'JM 21/08/2004
            .LicensedFaceParts = mLicensedFaceParts 'JM 19/09/2004
            .UserPieces = m_UserPieces 'JM 13/10/2004
            .SortOrderForData = m_SortOrderForData 'JM 14/10/2004
            .ShowDialog()

            ChangeUndoRedoStatus() 'JM 18/10/2004

            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        End With

        AddDebugComment("frmMain.KidsSave - end") 'JM 07/09/2004

    End Function

#End Region
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown

        AddDebugComment("frmMain.PictureBox1_MouseDown " & e.Button) 'JM 07/09/2004

        If CurrentTool <> Tools.Floodfill Then 'JM 14/10/2004
            '--- 'JM 13/07/2004 ---
            If e.Button = MouseButtons.Left Then 'JM 13/07/2004
                mMouseDownStart = New Point(e.X, e.Y)
                'Find clicked piece 
                Dim iPiece As Piece
                For Each iPiece In mPieces
                    If iPiece.IsPointOverMe(mMouseDownStart) Then
                        mMousePiece = iPiece
                        mMousePieceStart = iPiece.Location
                    End If
                Next iPiece

            ElseIf e.Button = MouseButtons.Right Then 'JM 13/07/2004

                'Find clicked piece 
                Dim PieceSelected As Boolean = False
                Dim iPiece As Piece
                For Each iPiece In mPieces
                    If iPiece.IsPointOverMe(New Point(e.X, e.Y)) Then
                        mMousePiece = iPiece
                        PieceSelected = True

                    End If
                Next iPiece

                If PieceSelected = True Then
                    'MessageBox.Show(lMousePiece.Location.X)
                    Try
                        Dim Properties As New PieceProps() 'Form()
                        Properties.Owner = Me
                        'Properties.CallingForm = Me
                        If Not mMousePiece Is Nothing Then 'JM 15/07/2004
                            Properties.ShowDialog()
                            Select Case Properties.TranType
                                Case PieceProps.ePieceTran.Delete
                                    mPieces.Remove(mMousePiece)
                            End Select

                            mMousePiece = Nothing 'JM 12/08/2004
                            PieceSelected = False 'JM 12/08/2004
                            Me.Update() 'JM 12/08/2004
                        End If
                    Catch

                    End Try
                Else 'JM 14/07/2004
                    ''Commented 'JM 15/10/2004
                    ''btnWhite_Click(Nothing, Nothing) 'JM 07/08/2004
                End If

            End If 'JM 13/07/2004

            '--- 'JM 27/08/2004 ---
            If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then
                If mMousePiece Is Nothing Then
                    m_Drawings.MouseDownClick(chkMirror.Checked, m_CurrentBrushWidth) 'JM 11/10/2004

                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseDown") 'JM 14/10/2004
                    ChangeUndoRedoStatus() 'JM 17/10/2004
                    lbooSomethingDrawn = True 'JM 07/09/2004
                    ''End If
                End If
            End If
            '--- 'JM 27/08/2004 ---
        End If 'JM 14/10/2004

    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove

        '--- 'JM 13/03/2005 ---
        If booPictureBox1_MouseMoveFirstDone = False Then
            AddDebugComment("Form1.PictureBox1_MouseMove")
            booPictureBox1_MouseMoveFirstDone = True
        End If
        '--- 'JM 13/03/2005 ---

        If CurrentTool <> Tools.Floodfill Then 'JM 14/10/2004
            If mMousePiece Is Nothing Then 'JM 13/07/2004
                If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then ' draw a filled circle if left mouse is down 
                    m_Drawings.MouseMoveClick(e, chkMirror.Checked = True) 'JM 11/10/2004

                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseMove") 'JM 14/10/2004
                    ChangeUndoRedoStatus() 'JM 17/10/2004
                End If

                PictureBox1.Invalidate() 'Repaint the PictureBox using the PictureBox1 Paint event
            End If 'JM 13/07/2004

            '--- 'JM 13/07/2004 ---
            If Not (mMousePiece Is Nothing) Then
                'Request redraw for piece's current location 
                PictureBox1.Invalidate(mMousePiece.Bounds)
                'Move the piece 
                mMousePiece.Location = New Point(mMousePieceStart.X + e.X - mMouseDownStart.X, mMousePieceStart.Y + e.Y - mMouseDownStart.Y)
                'Request redraw for the piece's new location 
                PictureBox1.Invalidate(mMousePiece.Bounds)
            End If
            '--- 'JM 13/07/2004 ---

            '--- 'JM 02/08/2004 ---
            Dim iPiece As Piece
            For Each iPiece In mPieces
                If iPiece.IsPointOverMe(New Point(e.X, e.Y)) Then
                    PictureBox1.Cursor.Current = Cursors.Hand
                    Exit For
                Else
                    PictureBox1.Cursor.Current = Cursors.Default
                End If
            Next iPiece
            '--- 'JM 02/08/2004 ---

        End If 'JM 14/10/2004

        If CurrentTool = Tools.Floodfill And FloodFillJustOccured = True Then
            FloodFillJustOccured = False
            PictureBox1.Invalidate()
        End If

        CurXPos = e.X 'JM 13/10/2004
        CurYPos = e.Y 'JM 13/10/2004

        StatusBarPanel1.Text = e.X & " x " & e.Y

    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp

        AddDebugComment("frmMain.PictureBox1_MouseUp " & e.Button) 'JM 07/09/2004

        If CurrentTool <> Tools.Floodfill Then 'JM 14/10/2004
            '--- 'JM 24/09/2004 ---
            'This code should make undo work better, rather than deleting all drawing
            m_Drawings.MouseUP(m_CurrentBrushWidth) 'JM 11/10/2004

            m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseMove") 'JM 15/10/2004

            ChangeUndoRedoStatus() 'JM 17/10/2004

            ''Commented 'JM 15/10/2004
            ''If e.Button = MouseButtons.Right Then 'JM 14/07/2004
            ''    'chkEraser.Checked = False 'JM 14/07/2004
            ''    btnBlack_Click(Nothing, Nothing) 'JM 07/08/2004
            ''End If

            'Stop moving the piece 
            mMousePiece = Nothing
        End If 'JM 14/10/2004

    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint

        '--- 'JM 13/03/2005 ---
        If booPictureBox1_MouseMoveFirstDone = False Then
            AddDebugComment("Form1.PictureBox1_Paint")
            booPictureBox1_MouseMoveFirstDone = True
        End If
        '--- 'JM 13/03/2005 ---

        ''If Not mMousePiece Is Nothing Then 'JM 13/07/2004
        If lbooAllowPainting = True Then 'JM 17/08/2004

            DrawOutput.DrawOutput(e.Graphics, False, PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                Nothing, Nothing, chkMirror.Checked, chkGuide.Checked, mPieces, New Point(0, 0), m_Drawings.lPaintBrush, _
                m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData)
        End If 'JM 17/08/2004

    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        gstrMRPs = "0320"
        'AddDebugComment("frmMain.btnPrint_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnPrint_Click - start" 'JM 01/05/2005


        ''DrawingWithoutHelpers()'JM 22/09/2004 - is this correct??

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004

        Dim ppScreen As New PrintPreview()
        With ppScreen
            '.PrintPreviewControl1.Document = PrintDocument1
            .MainPictureBox = PictureBox1
            ''.LoadedImgPictBox = PictureBox3 '2
            .MousePath = m_Drawings.mousePath
            .ReverseMousePath = m_Drawings.ReversemousePath
            .ThisPaintBrush = m_Drawings.lPaintBrush 'JM 28/08/2004
            .ThisPaintReverseBrush = m_Drawings.lPaintReverseBrush 'JM 28/08/2004
            Dim lpieces As ArrayList = mPieces 'JM 13/07/2004
            .Pieces = lpieces 'JM 13/07/2004
            .UserPieces = m_UserPieces 'JM 13/10/2004
            .SortOrder = m_SortOrderForData 'JM 15/10/2004
            .Owner = Me 'JM 21/08/2004
            .ShowDialog()
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004

            Me.Update() 'JM 12/08/2004
        End With

        'AddDebugComment("frmMain.btnPrint_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BPCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        Me.Update()

        'Console.WriteLine("Form1_Activated_Me.Update()")

        If mbooLoadAllDataOnce = True Then

            'AddDebugComment("frmMain.Form1_Activated - start") 'JM 07/09/2004
            gstrProbComtStack = "frmMain.Form1_Activated - start" 'JM 01/05/2005
            mbooLoadAllDataOnce = False
            gstrMRPs = "0131"

            'Dim Status As New StatusDialog(Me)
            'Status.Show()
            'Status.Status = "Loading your settings ...."
            'Application.DoEvents()
            'Status.Status = LangText.GetString("Pad_LoadingSettings") 

            '--- 'JM 08/09/2004 ---

            Dim DataFilesDescImages As New ArrayList()
            Dim DataFilesDescriptions As New ArrayList()
            Dim DataFilesProdNums As New ArrayList()
            Dim DataFiles As New ArrayList()
            Dim DataFileState As New ArrayList()

            Dim licenseStr As String 'JM 02/10/2004
            LoadFaceParts(DataFilesDescImages, DataFilesDescriptions, DataFilesProdNums, DataFiles, DataFileState, False, mLicensedFaceParts, licenseStr) 'JM 08/09/2004         

            'AddDebugComment("frmMain.Form1_Activated - 1") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA1" 'JM 01/05/2005
            Me.Text = NameMe("") 'JM 22/09/2004

            StatusBarPanel1.Text = "Licensed to : " & licenseStr 'JM 02/10/2004
            licenseStr = "" 'JM 02/10/2004
            gstrMRPs = "0150"
            DeactivatePaintingBeforeDialog() 'JM 09/09/2004

            '--- 'JM 11/09/2004 ---
            Dim lbooShowPurchasing As Boolean = False
            Try
                'lbooShowPurchasing = CBool(GetSetting("BuyPackShowNext", False, InitalXMLConfig.XmlConfigType.AppSettings, ""))
                lbooShowPurchasing = CBool(AppSettingsStartup.GetValue("BuyPackShowNext", "")) 'JM 16/09/2005
            Catch : End Try
            '--- 'JM 11/09/2004 ---

            'AddDebugComment("frmMain.Form1_Activated - 2") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA2" 'JM 01/05/2005

            If DataFiles.Count > 1 Then 'JM 22/09/2004 ' this might need to be one, depending on how you handle basic.dat
                If mintVersion = 32 And lbooShowPurchasing = True Then  'JM 10/09/2004 - if full version
                    Dim DFP As New DataFilePurchasing()
                    With DFP
                        .Owner = Me
                        .DataFiles = DataFiles
                        .DataFileDescImages = DataFilesDescImages
                        .DataFileDescriptions = DataFilesDescriptions
                        .ProductNumbers = DataFilesProdNums
                        .DataFileState = DataFileState
                        .ButtonType = DataFilePurchasing.eButtonType.BevelRed
                        .Caption = NameMe("")
                        .ShowDialog()
                    End With
                End If
            End If 'JM 22/09/2004
            '--- 'JM 08/09/2004 ---
            gstrMRPs = "0175"
            'AddDebugComment("frmMain.Form1_Activated - 2") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA3" 'JM 01/05/2005

            Dim lbooSixMonthVersionCFUDone As Boolean   'JM 22/03/2005
            'Status.Close()
            '--- 'JM 21/09/2004 ---
            Dim InitialConfig1 As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
            Dim lstrUsersStr As String
            lstrUsersStr = InitialConfig1.GetValue("Users", "")
            lbooSixMonthVersionCFUDone = CBool(InitialConfig1.GetValue("SixMonthVersionCFUDone", False)) 'JM 22/03/2005
            InitialConfig1 = Nothing

            'AddDebugComment("frmMain.Form1_Activated - 3") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA4" 'JM 01/05/2005

            Dim lbooUseNewUserScreen As Boolean = False 'JM 26/09/2004
            gstrMRPs = "0183"
            If lstrUsersStr = "" Then
                'Dim frmNewUser As New NewUser()
                'frmNewUser.Owner = Me
                'frmNewUser.ShowDialog()
                'mSelectedUser = frmNewUser.SelectedUser
                lbooUseNewUserScreen = True
                gstrMRPs = "0184"
            Else
                gstrMRPs = "0186"
                '--- 'JM 21/09/2004 ---
                Dim frmSignIn As New SignIn()
                frmSignIn.Owner = Me 'JM 21/08/2004
                frmSignIn.ShowDialog()
                If frmSignIn.Param = SignIn.Params.None Then
                    mSelectedUser = frmSignIn.SelectedUser
                    UIStyle.gPaintClr1 = frmSignIn.UICol1 'JM 24/09/2004
                    UIStyle.gPaintClr2 = frmSignIn.UICol2 'JM 24/09/2004
                Else
                    lbooUseNewUserScreen = True
                End If
                gstrMRPs = "0189"
            End If

            'AddDebugComment("frmMain.Form1_Activated - 4") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA5" 'JM 01/05/2005

            If lbooUseNewUserScreen = True Then
                Dim frmNewUser As New NewUser()
                frmNewUser.Owner = Me
                frmNewUser.ShowDialog()
                mSelectedUser = frmNewUser.SelectedUser
            End If

            SetDrawingLayout(mSelectedUser) 'JM 18/10/2004

            ''ReactivatePaintingBeforeDialog() 'JM 09/09/2004

            ''Me.Invalidate() 'JM 02/09/2004 - to update any colour selection
            ''Me.Update() 'JM 26/09/2004

            'AddDebugComment("frmMain.Form1_Activated - 5") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA6" 'JM 01/05/2005
            gstrMRPs = "0199"
            '--- 'JM 21/08/2004 ---
            Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            'If last face available, as if you want to load it.

            With InitialConfig
                Try
                    Dim LastSavedmaskFile As String = .GetValue("LastSaved", "")
                    If File.Exists(LastSavedmaskFile) = True Then
                        Me.Invalidate() 'JM 03/08/2005
                        Dim dlgRes As DialogResult
                        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
                        dlgRes = MessageBox.Show("Load the mask you were working on?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
                        If dlgRes = DialogResult.Yes Then
                            'LoadMask(LastSavedmaskFile, mPieces, PictureBox2.Image, False)
                            'x'LoadMask(LastSavedmaskFile, mPieces, PictureBox2.Image, False, mousePath, ReversemousePath, lPaintBrush, lPaintReverseBrush) 'JM 27/08/2004
                            'JM 30/08/2004
                            'LoadMask(LastSavedmaskFile, mPieces, Nothing, False, m_Drawings.mousePath, _
                            '    m_Drawings.ReversemousePath, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, 
                            '    mLicensedFaceParts)
                            'JM 13/10/2004
                            LoadMask(LastSavedmaskFile, mPieces, Nothing, False, m_Drawings.mousePath, _
                                    m_Drawings.ReversemousePath, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, _
                                    mLicensedFaceParts, m_UserPieces, m_SortOrderForData)
                            gstrMRPs = "0220"
                            m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

                        End If
                    End If
                Catch
                    '
                End Try
            End With
            InitialConfig = Nothing

            '--- 'JM 22/03/2005 ---
            'AddDebugComment("frmMain.Form1_Activated - 6M")
            gstrProbComtStack &= " #FMA6M" 'JM 01/05/2005
            If lbooSixMonthVersionCFUDone = False Then
                Me.Invalidate() 'JM 03/08/2005
                Dim BuildDate As Date = IO.File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly.Location.ToString())
                Dim Now As Date = Date.Now '.AddMonths(7) 'Add 7 months for testing purposes
                If BuildDate.AddMonths(6) < Now Then
                    Dim DlgRes As DialogResult = MessageBox.Show("This program is at least six months old, there may now be a newser version." & Environment.NewLine & Environment.NewLine & _
                    "Would you like to check for updates?", NameMe("Six Month Update Check"), MessageBoxButtons.YesNo)
                    If dlgres = DialogResult.Yes Then
                        mnuHelpCheckForUpdates_Click(Nothing, Nothing)
                    End If
                End If
                SaveSetting("SixMonthVersionCFUDone", True, InitalXMLConfig.XmlConfigType.AppSettings, "")
            End If
            '--- 'JM 22/03/2005 ---
            gstrMRPs = "0240"
            'AddDebugComment("frmMain.Form1_Activated - 6") 'JM 30/09/2004
            gstrProbComtStack &= " #FMA7" 'JM 01/05/2005



            ReactivatePaintingBeforeDialog() 'JM 21/08/2004lbooAllowPainting = True 'JM 17/08/2004
            '--- 'JM 21/08/2004 ---

            '--- 'JM 03/08/2005 ---
            'Application.DoEvents()

            'Dim ShowWelcome As Boolean = CBool(GetSetting("WELCOME", "True", InitalXMLConfig.XmlConfigType.AppSettings, ""))
            Dim ShowWelcome As Boolean = CBool(AppSettingsStartup.GetValue("WELCOME", "True")) 'JM 16/09/2005

            If ShowWelcome = True Then
                Me.Invalidate()
                Dim BM As New InformParent()
                BM.Owner = Me 'JM 02/09/2005
                BM.ShowDialog()

            End If
            '--- 'JM 03/08/2005 ---


            '--- 'JM 26/09/2004 ---
            'used for W98 bug of now drawing buttons properly
            If IsAboveOrEqualWinXp() = False Then
                Me.WindowState = FormWindowState.Minimized
                System.Threading.Thread.Sleep(1000)
                Me.WindowState = FormWindowState.Maximized

            End If
            '--- 'JM 26/09/2004 ---

            '--- 'JM 02/10/2004 ---
            'fixes menu bar not being coloured in problem
            Dim MenuItem As New MenuItem()
            MainMenu1.MenuItems.Add(MenuItem)
            MainMenu1.MenuItems.Remove(MenuItem)
            '--- 'JM 02/10/2004 ---


            'used to update background colours 
            Me.Invalidate() 'JM 26/09/2004
            gstrMRPs = "0255"
            'AddDebugComment("frmMain.Form1_Activated - end") 'JM 07/09/2004
            gstrProbComtStack &= " #FMAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
        End If
    End Sub
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gstrMRPs = "0101"
        Busy(Me, True) 'JM 15/09/2005

        'AddDebugComment("frmMain.Form1_Load - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.Form1_Load - start" 'JM 01/05/2005

        'Me.Icon = New System.Drawing.Icon( _
        '        System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("KidsMaskPrint.kmp.ico"))
        Me.Text = NameMe("") 'JM 10/09/2004

        m_Drawings = New Drawings(m_CurrentColour, m_CurrentBrushWidth) 'JM 11/10/2004
        ''btnRedo.Enabled = False 'JM 11/10/2004

        m_UserPieces = New FacePartStuctureDataFile() 'JM 13/10/2004

        m_SortOrderForData = New SortOrderForData() 'JM 14/10/2004

        ''myUserColor = Color.Black

        ''StatusBarPanel1.Text = "0 x 0"

        '--- 'JM 15/10/2004 ---
        If IsAboveOrEqualWinXp() = True Then
            rdoFloodFill.FlatStyle = FlatStyle.System
            rdoFreehand.FlatStyle = FlatStyle.System
        End If
        '--- 'JM 15/10/2004 ---

        'If IsAboveOrEqualWinXp() = True Then  'JM 13/08/2004
        '    btnExit.FlatStyle = FlatStyle.System
        '    btnPrint.FlatStyle = FlatStyle.System
        '    btnUndo.FlatStyle = FlatStyle.System
        '    btnClear.FlatStyle = FlatStyle.System
        '    btnEyes.FlatStyle = FlatStyle.System
        '    btnEars.FlatStyle = FlatStyle.System
        '    btnMouths.FlatStyle = FlatStyle.System
        '    btnNoses.FlatStyle = FlatStyle.System
        '    btnOther.FlatStyle = FlatStyle.System
        '    'lblpen.FlatStyle = FlatStyle.System
        'End If
        gstrMRPs = "0110"

        SetBackcolors()

        SetPaletteLabel(lblPBlack, btnPBlack) 'JM 15/10/2004

        With lstBrushWidth
            .Items.Clear()
            .Items.Add("1") ' 'JM 22/10/2004 & ChrGet(149))
            .Items.Add("2") ' 'JM 22/10/2004 & ChrGet(150))
            .Items.Add("4") ' 'JM 22/10/2004 & ChrGet(151))
            .Items.Add("8") ' 'JM 22/10/2004 & ChrGet(152))
            .SelectedIndex = 2
        End With

        '#If Debug Then
        '        btnDebug = New Button()
        '        With btnDebug
        '            .Top = 520
        '            .Left = 10
        '            .Anchor = AnchorStyles.Top And AnchorStyles.Left
        '            .Visible = True
        '            .Text = "Debug"
        '        End With
        '        Me.Controls.Add(btnDebug)
        '#End If

        '--- 'JM 15/09/2005 ---
        'Load clown mask
        gstrMRPs = "0121" 'JM 16/09/2005
        'Dim ShowClown As Boolean = CBool(GetSetting("SHOWCLOWN", "True", InitalXMLConfig.XmlConfigType.AppSettings, ""))
        Dim ShowClown As Boolean = CBool(AppSettingsStartup.GetValue("SHOWCLOWN", "True")) 'JM 16/09/2005
        'MessageBox.Show("ShowClown=" & ShowClown)
        If ShowClown = True Then
            gstrMRPs = "0122" 'JM 16/09/2005
            Try
                Dim ClownMask As String = System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetEntryAssembly.Location.ToString()) & "\Clown.mask"
                'MessageBox.Show("ShowClown=1")
                If File.Exists(ClownMask) = True Then
                    'MessageBox.Show("ShowClown=2")
                    LoadMask(ClownMask, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                    m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)
                    m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)
                    'MessageBox.Show("ShowClown=3")


                End If
                SaveSetting("SHOWCLOWN", "False", InitalXMLConfig.XmlConfigType.AppSettings, "")
            Catch
                '
            End Try
        End If
        '--- 'JM 15/09/2005 ---

        Busy(Me, False) 'JM 15/09/2005

        gstrMRPs = "0130"
        'AddDebugComment("frmMain.Form1_Load - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FMLEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub frmMain_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter

        lbooAllowPainting = True 'JM 27/08/2004

    End Sub
    Private Sub frmMain_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave

        lbooAllowPainting = False 'JM 27/08/2004

    End Sub
    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        'added 'JM 16/08/2004
        'AddDebugComment("frmMain.frmMain_Closing - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.frmMain_Closing - start" 'JM 01/05/2005

        SaveBeforeExitProg() 'JM 28/09/2004

        DeleteTempFiles()

        Me.Visible = False 'JM 17/08/2004

        If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
            Welcome(lbooSplashShown, Me)
        End If

        'AddDebugComment("frmMain.frmMain_Closing - end") 'JM 07/09/2004
        gstrProbComtStack &= " #FMCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub SaveBeforeExitProg()
        'added 'JM 28/09/2004

        '--- 'JM 21/08/2004 ---
        If mPieces.Count > 0 Or lbooSomethingDrawn = True Then 'JM 07/09/2004
            DeactivatePaintingBeforeDialog() 'JM 21/08/2004
            Dim dlgRes As DialogResult = MessageBox.Show("Save this Mask?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Application.DoEvents() 'JM 22/11/2004
            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
            If dlgRes = DialogResult.Yes Then
                KidsSave(True)
            Else
                SaveSetting("LastSaved", "", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            End If
        End If
        '--- 'JM 21/08/2004 ---

        '--- 'JM 26/10/2004 ---
        Try
            Dim lstrCustomColours() As String
            ReDim lstrCustomColours(lintCustomColours.GetUpperBound(0))
            Dim lintArrInc As Integer
            For lintArrInc = 0 To lintCustomColours.GetUpperBound(0)
                lstrCustomColours(lintArrInc) = CStr(lintCustomColours(lintArrInc))
            Next lintArrInc
            SaveSetting("CustomColours", Microsoft.VisualBasic.Join(lstrCustomColours, "#"), InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
        Catch
        End Try

        Try
            SaveSetting("LastCustomColour", ColorToString(btnPCustom.BackColor), InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
        Catch
        End Try
        '--- 'JM 26/10/2004 ---
    End Sub
    Private Sub btnUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        gstrMRPs = "0330"
        'AddDebugComment("frmMain.btnUndo_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnUndo_Click - start" 'JM 01/05/2005

        'JM 15/10/2004
        btnUndo.Enabled = False
        ''btnRedo.Enabled = False

        'AddDebugComment("frmMain.btnUndo_Click - 1") 'JM 30/10/2004
        gstrProbComtStack &= " #BU1" 'JM 01/05/2005
        '--- 'JM 17/10/2004 ---
        'this clears and redo then user draw something new from redoing everything
        If RedoSortOrderStack.Count = 0 Then
            RedoPackPieceArr.Clear()
            RedoUserPieceArr.Clear()
        End If
        '--- 'JM 17/10/2004 ---

        'AddDebugComment("frmMain.btnUndo_Click - 2") 'JM 30/10/2004
        gstrProbComtStack &= " #BU2" 'JM 01/05/2005

        '--- 'JM 05/03/2005 ---
        Try 'JM 22/06/2005
            If m_SortOrderForData.DataType.Count = -1 Then
                Exit Sub
            End If
        Catch 'JM 22/06/2005
            gstrProbComtStack &= " #BU2b"
            'Czech Republic - Anonymous bug report - index problem ?
            Exit Sub 'JM 22/06/2005
        End Try 'JM 22/06/2005
        '--- 'JM 05/03/2005 ---

        'JM 16/10/2004
        Dim LastActivity As SortOrderForData.eDataType = m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1)
        Select Case LastActivity
            Case SortOrderForData.eDataType.PackPieces
                'AddDebugComment("frmMain.btnUndo_Click - 3") 'JM 30/10/2004
                gstrProbComtStack &= " #BU3" 'JM 01/05/2005
                RedoPackPieceArr.Push(mPieces(mPieces.Count - 1)) 'JM 16/10/2004
                mPieces.RemoveAt(mPieces.Count - 1) 'JM 16/10/2004
            Case SortOrderForData.eDataType.NormalGraphicsPath, SortOrderForData.eDataType.ReverseGraphicsPath
                'AddDebugComment("frmMain.btnUndo_Click - 4") 'JM 30/10/2004
                gstrProbComtStack &= " #BU4" 'JM 01/05/2005
                m_Drawings.Undo(m_CurrentBrushWidth)  'JM 11/10/2004
                'AddDebugComment("frmMain.btnUndo_Click - 4b") 'JM 30/10/2004
                gstrProbComtStack &= " #BU4b" 'JM 01/05/2005
                Try
                    If m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1) = SortOrderForData.eDataType.NormalGraphicsPath Or _
                         m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1) = SortOrderForData.eDataType.ReverseGraphicsPath Then
                        'AddDebugComment("frmMain.btnUndo_Click - 4c") 'JM 30/10/2004
                        gstrProbComtStack &= " #BU4c" 'JM 01/05/2005
                        m_Drawings.Undo(m_CurrentBrushWidth)  'JM 17/10/2004
                        'AddDebugComment("frmMain.btnUndo_Click - 4d") 'JM 30/10/2004
                        gstrProbComtStack &= " #BU4d" 'JM 01/05/2005
                    End If
                Catch
                End Try
            Case SortOrderForData.eDataType.UserPieces
                'AddDebugComment("frmMain.btnUndo_Click - 5") 'JM 30/10/2004
                gstrProbComtStack &= " #BU5" 'JM 01/05/2005
                RedoUserPieceArr.Push(m_UserPieces.Parts(m_UserPieces.Parts.Count - 1)) 'JM 16/10/2004
                m_UserPieces.Parts.RemoveAt(m_UserPieces.Parts.Count - 1) 'JM 16/10/2004
        End Select

        'AddDebugComment("frmMain.btnUndo_Click - 6") 'JM 30/10/2004
        gstrProbComtStack &= " #BU6" 'JM 01/05/2005

        RedoSortOrderStack.Push(LastActivity) 'JM 17/10/2004

        'AddDebugComment("frmMain.btnUndo_Click - 7") 'JM 30/10/2004
        gstrProbComtStack &= " #BU7" 'JM 01/05/2005

        m_SortOrderForData.Remove(mPieces, m_Drawings.mousePath, _
            m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnUndo_Click") 'JM 14/10/2004

        'AddDebugComment("frmMain.btnUndo_Click - 8") 'JM 30/10/2004
        gstrProbComtStack &= " #BU8" 'JM 01/05/2005

        ChangeUndoRedoStatus() 'JM 17/10/2004

        PictureBox1.Invalidate() 'JM 24/09/2004

        'AddDebugComment("frmMain.btnUndo_Click - end") 'JM 30/10/2004
        gstrProbComtStack &= " #BUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        gstrMRPs = "0340"
        'AddDebugComment("frmMain.btnRedo_Click - start") 'JM 30/10/2004
        gstrProbComtStack = "frmMain.btnRedo_Click - start" 'JM 01/05/2005

        'JM 15/10/2004
        btnUndo.Enabled = False
        ''btnRedo.Enabled = False

        '--- 'JM 16/10/2004 ---
        Select Case RedoSortOrderStack.Peek 'JM 17/10/2004
            Case SortOrderForData.eDataType.PackPieces
                'AddDebugComment("frmMain.btnRedo_Click - 1") 'JM 30/10/2004
                gstrProbComtStack &= " #BR1" 'JM 01/05/2005
                Dim lRedoPiece As New Piece()

                Dim EnumerShowNow As System.Collections.IEnumerator

                'AddDebugComment("frmMain.btnRedo_Click - 1b") 'JM 30/10/2004
                gstrProbComtStack &= " #BR1b" 'JM 01/05/2005
                EnumerShowNow = RedoPackPieceArr.GetEnumerator()
                EnumerShowNow.MoveNext()

                'AddDebugComment("frmMain.btnRedo_Click - 1c") 'JM 30/10/2004
                gstrProbComtStack &= " #BR1c" 'JM 01/05/2005
                lRedoPiece = EnumerShowNow.Current

                'AddDebugComment("frmMain.btnRedo_Click - 1d") 'JM 30/10/2004
                gstrProbComtStack &= " #BR1d" 'JM 01/05/2005
                RedoPackPieceArr.Pop()
                mPieces.Add(lRedoPiece)
                'AddDebugComment("frmMain.btnRedo_Click - 1e") 'JM 30/10/2004
                gstrProbComtStack &= " #BR1e" 'JM 01/05/2005

            Case SortOrderForData.eDataType.NormalGraphicsPath, SortOrderForData.eDataType.ReverseGraphicsPath
                'AddDebugComment("frmMain.btnRedo_Click - 2") 'JM 30/10/2004
                gstrProbComtStack &= " #BR2" 'JM 01/05/2005
                m_Drawings.Redo()

                'AddDebugComment("frmMain.btnRedo_Click - 2b") 'JM 30/10/2004
                gstrProbComtStack &= " #BR2b" 'JM 01/05/2005
                '--- 'JM 17/10/2004 ---
                Try
                    'AddDebugComment("frmMain.btnRedo_Click - 2c") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR2c" 'JM 01/05/2005
                    Dim NextRedo As SortOrderForData.eDataType
                    Dim EnumerShowNow As System.Collections.IEnumerator

                    'AddDebugComment("frmMain.btnRedo_Click - 2d") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR2d" 'JM 01/05/2005
                    EnumerShowNow = RedoSortOrderStack.GetEnumerator()
                    EnumerShowNow.MoveNext()

                    'AddDebugComment("frmMain.btnRedo_Click - 2e") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR2e" 'JM 01/05/2005
                    NextRedo = EnumerShowNow.Current

                    If NextRedo = SortOrderForData.eDataType.NormalGraphicsPath Or NextRedo = SortOrderForData.eDataType.ReverseGraphicsPath Then
                        'AddDebugComment("frmMain.btnRedo_Click - 2f") 'JM 30/10/2004
                        gstrProbComtStack &= " #BR2f" 'JM 01/05/2005
                        m_Drawings.Redo()
                    End If
                Catch
                End Try
                '--- 'JM 17/10/2004 ---
                'AddDebugComment("frmMain.btnRedo_Click - 2g") 'JM 30/10/2004
                gstrProbComtStack &= " #BR2g" 'JM 01/05/2005
            Case SortOrderForData.eDataType.UserPieces
                'AddDebugComment("frmMain.btnRedo_Click - 3") 'JM 30/10/2004
                gstrProbComtStack &= " #BR3" 'JM 01/05/2005
                Try 'JM 19/10/2004
                    Dim lRedoUserPiece As New Piece()
                    Dim EnumerShowNow As System.Collections.IEnumerator

                    'AddDebugComment("frmMain.btnRedo_Click - 3b") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR3b" 'JM 01/05/2005

                    EnumerShowNow = m_UserPieces.Parts.GetEnumerator()
                    EnumerShowNow.MoveNext()

                    'AddDebugComment("frmMain.btnRedo_Click - 3c") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR3c" 'JM 01/05/2005
                    lRedoUserPiece = EnumerShowNow.Current

                    'AddDebugComment("frmMain.btnRedo_Click - 3d") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR3d" 'JM 01/05/2005
                    RedoUserPieceArr.Pop()
                    m_UserPieces.Parts.Add(lRedoUserPiece)
                    'AddDebugComment("frmMain.btnRedo_Click - 3e") 'JM 30/10/2004
                    gstrProbComtStack &= " #BR3e" 'JM 01/05/2005
                Catch ex As Exception
                    Console.WriteLine("btnRedo_Click.Case SortOrderForData.eDataType.UserPieces " & ex.ToString)
                End Try
        End Select
        '--- 'JM 16/10/2004 ---

        'AddDebugComment("frmMain.btnRedo_Click - 4") 'JM 30/10/2004
        gstrProbComtStack &= " #BR4" 'JM 01/05/2005
        m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
            m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnRedo_Click") 'JM 14/10/2004

        'AddDebugComment("frmMain.btnRedo_Click - 5") 'JM 30/10/2004
        gstrProbComtStack &= " #BR5" 'JM 01/05/2005
        RedoSortOrderStack.Pop() 'JM 17/10/2004

        'AddDebugComment("frmMain.btnRedo_Click - 6") 'JM 30/10/2004
        gstrProbComtStack &= " #BR6" 'JM 01/05/2005
        ChangeUndoRedoStatus() 'JM 17/10/2004

        PictureBox1.Invalidate()

        'AddDebugComment("frmMain.btnRedo_Click - end") 'JM 30/10/2004
        gstrProbComtStack &= " #BREnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub chkMirror_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMirror.CheckedChanged

        AddDebugComment("Form1.chkMirror_CheckedChanged") 'JM 13/03/2005

        PictureBox1.Invalidate() 'JM 24/09/2004

    End Sub
    Private Sub chkGuide_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGuide.CheckedChanged

        AddDebugComment("Form1.chkGuide_CheckedChanged") 'JM 13/03/2005

        PictureBox1.Invalidate() 'JM 24/09/2004

    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        'AddDebugComment("frmMain.btnClear_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnClear_Click - start" 'JM 01/05/2005

        '--- 'JM 24/09/2004 ---
        If mPieces.Count > 0 Or lbooSomethingDrawn = True Then
            DeactivatePaintingBeforeDialog()
            'Dim dlgRes As DialogResult = MessageBox.Show("Loose this Mask?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Dim dlgRes As DialogResult = MessageBox.Show("Are you sure you want to create a new mask?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ReactivatePaintingBeforeDialog()
            If dlgRes <> DialogResult.Yes Then
                PictureBox1.Invalidate() 'JM 19/10/2004
                Exit Sub
            End If
        End If

        lbooSomethingDrawn = False
        '--- 'JM 24/09/2004 ---

        Clear() 'JM 18/10/2004

        'AddDebugComment("frmMain.btnClear_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub Clear()

        m_Drawings.Clear(m_CurrentColour, m_CurrentBrushWidth) 'JM 11/10/2004

        mPieces = Nothing
        mPieces = New ArrayList()

        m_UserPieces = New FacePartStuctureDataFile() 'JM 13/10/2004
        m_SortOrderForData = New SortOrderForData() 'JM 15/10/2004

        m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
            m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnClear_Click")

        RedoSortOrderStack.Clear() 'JM 17/10/2004

        ChangeUndoRedoStatus() 'JM 17/10/2004

        PictureBox1.Image = Nothing

        PictureBox1.Invalidate() 'JM 24/09/2004
    End Sub
    Private Sub btnWhite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWhite.Click

        'AddDebugComment("frmMain.btnWhite_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnWhite_Click - start" 'JM 01/05/2005

        lblWhite.BackColor = Color.Red
        lblBlack.BackColor = Color.White

        m_Drawings.SetColour(Color.White, m_CurrentBrushWidth)

        Me.Update()

        'AddDebugComment("frmMain.btnWhite_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BWCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnBlack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlack.Click

        'AddDebugComment("frmMain.btnBlack_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnBlack_Click - start" 'JM 01/05/2005

        lblWhite.BackColor = Color.White
        lblBlack.BackColor = Color.Red

        m_Drawings.SetColour(Color.Black, m_CurrentBrushWidth)

        Me.Update()

        'AddDebugComment("frmMain.btnBlack_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BBEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnHead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHead.Click

        gstrMRPs = "0350"
        'added 'JM 24/09/2004
        'AddDebugComment("frmMain.btnHead_Click - start")
        gstrProbComtStack = "frmMain.btnHead_Click - start" 'JM 01/05/2005
        DeactivatePaintingBeforeDialog()
        gstrProbComtStack = " #bHc1" 'JM 12/07/2005

        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bHc1a" 'JM 12/07/2005
        fp.Owner = Me
        gstrProbComtStack = " #bHc1b" 'JM 12/07/2005
        fp.PartType = FacePartEnums.ePartType.Outline
        gstrProbComtStack = " #bHc1c" 'JM 12/07/2005

        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---

        gstrProbComtStack = " #bHc2"
        ReactivatePaintingBeforeDialog()
        gstrProbComtStack = " #bHc3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum, Me)
        gstrProbComtStack = " #bHc4"

        fp = Nothing
        'AddDebugComment("frmMain.btnHead_Click - end")
        gstrProbComtStack &= " #BHEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnEyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEyes.Click
        gstrMRPs = "0360"
        'AddDebugComment("frmMain.btnEyes_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnEyes_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bEyc1"
        Dim fp As New FacePartDiag() 'FacePartSel()
        gstrProbComtStack = " #bEyc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bEyc1b"
        'fp.FacePartImageList = ilEye
        'fp.FacePartListView = lvEye
        fp.PartType = FacePartEnums.ePartType.Eye 'JM 21/09/2004
        gstrProbComtStack = " #bEyc1c"
        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---
        gstrProbComtStack = " #bEyc2"
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bEyc3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum) 'added datafile stuff 'JM 19/08/2004
        gstrProbComtStack = " #bEyc4"
        fp = Nothing 'JM 21/09/2004
        'AddDebugComment("frmMain.btnEyes_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BEYCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnEars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEars.Click
        gstrMRPs = "0370"
        'AddDebugComment("frmMain.btnEars_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnEars_Click - start" 'JM 01/05/2005
        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bEac1"
        Dim fp As New FacePartDiag() '' FacePartSel()
        gstrProbComtStack = " #bEac1a"
        fp.Owner = Me
        gstrProbComtStack = " #bEac1b"
        fp.PartType = FacePartEnums.ePartType.Ear 'JM 21/09/2004
        gstrProbComtStack = " #bEac1c"
        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---
        gstrProbComtStack = " #bEac2"
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bEac3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum) 'added datafile stuff 'JM 19/08/2004
        gstrProbComtStack = " #bEac4"
        fp = Nothing 'JM 21/09/2004
        'AddDebugComment("frmMain.btnEars_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BEACEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnMouths_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMouths.Click
        gstrMRPs = "0380"
        'AddDebugComment("frmMain.btnMouths_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnMouths_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bMoc1"
        Dim fp As New FacePartDiag() 'FacePartSel()
        gstrProbComtStack = " #bMoc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bMoc1b"
        fp.PartType = FacePartEnums.ePartType.Mouth 'JM 21/09/2004
        gstrProbComtStack = " #bMoc1c"
        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---
        gstrProbComtStack = " #bMoc2"
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bMoc3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum) 'added datafile stuff 'JM 19/08/2004
        gstrProbComtStack = " #bMoc4"
        fp = Nothing 'JM 21/09/2004
        'AddDebugComment("frmMain.btnMouths_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BMCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnNoses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoses.Click
        gstrMRPs = "0390"
        'AddDebugComment("frmMain.btnNoses_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnNoses_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bNoc1"
        Dim fp As New FacePartDiag() 'FacePartSel()
        gstrProbComtStack = " #bNoc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bNoc1b"
        fp.PartType = FacePartEnums.ePartType.Nose 'JM 21/09/2004
        gstrProbComtStack = " #bNoc1c"
        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---
        gstrProbComtStack = " #bNoc2"
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bNoc3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum) 'added datafile stuff 'JM 19/08/2004
        gstrProbComtStack = " #bNoc4"
        fp = Nothing 'JM 21/09/2004
        'AddDebugComment("frmMain.btnNoses_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BNCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOther.Click
        gstrMRPs = "0400"
        'AddDebugComment("frmMain.btnOther_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnOther_Click - start" 'JM 01/05/2005

        DeactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bOtc1"
        Dim fp As New FacePartDiag() 'FacePartSel()
        gstrProbComtStack = " #bOtc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bOtc1b"
        fp.PartType = FacePartEnums.ePartType.Misc
        gstrProbComtStack = " #bOtc1c"
        '--- 'JM 31/08/2005 ---
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        '--- 'JM 31/08/2005 ---
        gstrProbComtStack = " #bOtc2"
        ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        gstrProbComtStack = " #bOtc3"
        'AddSelectedFacePart(fp.RetPart, fp.RetPosSel, fp.SourceDataFileName, fp.DataFileItemNum) 'added datafile stuff 'JM 19/08/2004
        gstrProbComtStack = " #bOtc4"
        fp = Nothing 'JM 21/09/2004
        'AddDebugComment("frmMain.btnOther_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BOCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        gstrMRPs = "0410"
        'AddDebugComment("frmMain.btnLoad_Click - start") 'JM 07/09/2004
        gstrProbComtStack = "frmMain.btnLoad_Click - start" 'JM 01/05/2005

        'added 'JM 20/08/2004
        Dim LoadSlots As New Slots()
        With LoadSlots
            .Owner = Me 'JM 21/08/2004
            .TranType = Slots.eTranType.Load
            .SelectedUser = mSelectedUser
            DeactivatePaintingBeforeDialog() 'JM 21/08/2004
            .LicensedFaceParts = mLicensedFaceParts 'JM 19/09/2004
            .UserPieces = m_UserPieces 'JM 13/10/2004
            .SortOrderForData = m_SortOrderForData 'JM 14/10/2004
            .ShowDialog()

            Application.DoEvents() 'JM 28/09/2004
            If .MaskToLoad <> "" Then 'JM 21/08/2004
                Busy(Me, True) 'JM 28/09/2004
                'btnClear_Click(Nothing, Nothing) 'JM 17/10/2004
                Clear() 'JM 18/10/2004
                'LoadMask(.MaskToLoad, mPieces, PictureBox2.Image, False) 'JM 21/08/2004
                'LoadMask(.MaskToLoad, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                '   m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts) 'JM 27/08/2004
                'JM 13/10/2004
                LoadMask(.MaskToLoad, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                    m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)

                Busy(Me, False) 'JM 28/09/2004
            End If
            m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

            ChangeUndoRedoStatus() 'JM 18/10/2004

            ReactivatePaintingBeforeDialog() 'JM 21/08/2004
        End With

        'AddDebugComment("frmMain.btnLoad_Click - end") 'JM 07/09/2004
        gstrProbComtStack &= " #BLCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        gstrMRPs = "0420"
        AddDebugComment("Form1.btnSave_Click") 'JM 13/03/2005

        KidsSave(False) 'JM 21/08/2004

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("frmMain.btnHelp_Click") 'JM 07/09/2004

        'JM 25/08/2004
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.MainScreen))

    End Sub
    Private Sub mnuHelpBuyPacks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpBuyPacks.Click
        gstrMRPs = "0430"
        AddDebugComment("Form1.mnuHelpBuyPacks_Click") 'JM 13/03/2005

        DeactivatePaintingBeforeDialog()

        Dim DataFilesDescImages As New ArrayList()
        Dim DataFilesDescriptions As New ArrayList()
        Dim DataFilesProdNums As New ArrayList()
        Dim DataFiles As New ArrayList()
        Dim DataFileState As New ArrayList()

        If Not mLicensedFaceParts Is Nothing Then mLicensedFaceParts.Clear() 'JM 19/09/2004

        LoadFaceParts(DataFilesDescImages, DataFilesDescriptions, DataFilesProdNums, DataFiles, DataFileState, True, mLicensedFaceParts, "")

        'MessageBox.Show(DataFilesDescImages.Count & " " & DataFilesDescriptions.Count & " " & _
        '    DataFilesProdNums.Count & " " & DataFiles.Count & " " & DataFileState.Count)

        Dim DFP As New DataFilePurchasing()
        With DFP
            .Owner = Me
            .DataFiles = DataFiles
            .DataFileDescImages = DataFilesDescImages
            .DataFileDescriptions = DataFilesDescriptions
            .ProductNumbers = DataFilesProdNums
            .DataFileState = DataFileState
            .ButtonType = DataFilePurchasing.eButtonType.BevelRed
            .Caption = NameMe("")
            .ShowDialog()
        End With

        'JM 28/09/2004 - this should load new licesend face parts
        If Not mLicensedFaceParts Is Nothing Then mLicensedFaceParts.Clear()
        LoadFaceParts(Nothing, Nothing, Nothing, Nothing, Nothing, False, mLicensedFaceParts, "")

        ReactivatePaintingBeforeDialog()

    End Sub
    Private Sub mnuHelp_Select(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelp.Select

        If mintVersion = 32 Then
            mnuHelpBuyPacks.Visible = True
        Else
            mnuHelpBuyPacks.Visible = False
        End If

    End Sub
    Private Sub Panel1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Resize

        'added 'JM 21/09/2004
        Dim X As Short = (Panel1.Width - PictureBox1.Width) / 2
        Dim Y As Short = (Panel1.Height - PictureBox1.Height) / 2
        PictureBox1.Location = New Point(X, Y)

    End Sub
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 'JM 21/09/2004
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

        AddDebugComment("Form1.PictureBox1_Click") 'JM 13/03/2005

        'added 'JM 13/10/2004
        If CurrentTool = Tools.Floodfill Then 'JM 14/10/2004

            Busy(Me, True) 'JM 18/10/2004
            PictureBox1.Enabled = False 'JM 18/10/2004


            Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, mPieces, _
                m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData)
            ' Dim Before As New Bitmap(FullImage)
            'Dim l_ActiveBitmap As New Bitmap(FullImage)

            '--- 'JM 18/10/2004 ---
            Dim test As New Bitmap(FullImage)
            Dim TempPixelColour As Color = test.GetPixel(CurXPos, CurYPos)

            'MessageBox.Show(CurrentPixelColour.ToString, m_CurrentColour.ToString)
            If TempPixelColour.R = m_CurrentColour.R And TempPixelColour.G = m_CurrentColour.G And TempPixelColour.B = m_CurrentColour.B Then
                PictureBox1.Enabled = True
                Busy(Me, False)
                Exit Sub
                'Else
                '    MessageBox.Show(TempPixelColour.R & " " & m_CurrentColour.R & CR() & _
                '    TempPixelColour.G & " " & m_CurrentColour.G & CR() & _
                '    TempPixelColour.B & " " & m_CurrentColour.B)
            End If
            '--- 'JM 18/10/2004 ---

            Dim ff As New FloodFill()
            Dim UserFloodPiece As Bitmap
            Dim ClipTop As Integer
            Dim ClipLeft As Integer
            UserFloodPiece = ff.FloodFillIt(New Bitmap(FullImage), CurXPos, CurYPos, m_CurrentColour, Color.Black, ClipTop, ClipLeft)

            Dim ThisFloodFillPart As New Part()
            With ThisFloodFillPart
                .PartType = FacePartEnums.ePartType.Misc
                .FullImage = UserFloodPiece
                .FaceMaster = ""
                .LeftPart = New Point(ClipLeft, ClipTop)
                .BothParts = False
            End With

            m_UserPieces.Parts.Add(ThisFloodFillPart)

            m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_Click") 'JM 14/10/2004

            ChangeUndoRedoStatus() 'JM 17/10/2004

            FloodFillJustOccured = True 'JM 14/10/2004

            PictureBox1.Enabled = True 'JM 18/10/2004
            Busy(Me, False) 'JM 18/10/2004

            Me.Invalidate()

        End If 'JM 14/10/2004
    End Sub
    Private Sub btnPalette_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPBlack.Click, btnPWhite.Click, _
        btnPRed.Click, btnPOrange.Click, btnPYellow.Click, btnPBrown.Click, btnPGreen.Click, btnPLime.Click, btnPCyan.Click, _
        btnPLightBlue.Click, btnPBlue.Click, btnPMagenta.Click, btnPPink.Click, btnPCustom.Click

        AddDebugComment("frmMain.btnPalette_Click - start") 'JM 15/10/2004

        Dim ctl As Control
        For Each ctl In pnlPalette.Controls
            If TypeOf ctl Is Label Then
                Dim lab As Label = ctl
                If lab.Tag = "Selected" Then
                    lab.BorderStyle = BorderStyle.None
                    lab.BackColor = Color.FromArgb(0, lab.BackColor)
                    lab.Tag = ""
                    Exit For
                End If
            End If
        Next ctl

        Select Case sender.Name
            Case "btnPBlack" : SetPaletteLabel(lblPBlack, sender)
            Case "btnPWhite" : SetPaletteLabel(lblPWhite, sender)
            Case "btnPRed" : SetPaletteLabel(lblPRed, sender)
            Case "btnPOrange" : SetPaletteLabel(lblPOrange, sender)
            Case "btnPYellow" : SetPaletteLabel(lblPYellow, sender)
            Case "btnPBrown" : SetPaletteLabel(lblPBrown, sender)
            Case "btnPGreen" : SetPaletteLabel(lblPGreen, sender)
            Case "btnPLime" : SetPaletteLabel(lblPLime, sender)
            Case "btnPCyan" : SetPaletteLabel(lblPCyan, sender)
            Case "btnPLightBlue" : SetPaletteLabel(lblPLightBlue, sender)
            Case "btnPBlue" : SetPaletteLabel(lblPBlue, sender)
            Case "btnPMagenta" : SetPaletteLabel(lblPMagenta, sender)
            Case "btnPPink" : SetPaletteLabel(lblPPink, sender)
            Case "btnPCustom" : SetPaletteLabel(lblPCustom, sender) 'JM 18/10/2004
        End Select

        m_Drawings.SetColour(sender.BackColor, m_CurrentBrushWidth)
        m_CurrentColour = sender.BackColor 'JM 15/10/2004

        'AddDebugComment("frmMain.btnPalette_Click - end") 'JM 15/10/2004
        gstrProbComtStack &= " #BPCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub SetPaletteLabel(ByVal pobjLabel As Label, ByVal pobButton As Button)
        'JM 15/10/2004
        pobjLabel.BorderStyle = BorderStyle.FixedSingle
        pobjLabel.BackColor = Color.Red
        pobjLabel.Tag = "Selected"
        pobjLabel.BringToFront()
        pobButton.BringToFront()

    End Sub
    Private Sub rdoFreehand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoFreehand.Click

        AddDebugComment("Form1.rdoFreehand_Click") 'JM 13/03/2005

        'JM 15/10/2004
        If rdoFloodFill.Checked = True Then
            CurrentTool = Tools.Floodfill
            ''PictureBox1.Cursor = ShowFloodFillCursor()  'JM 17/10/2004Cursors.Hand
            Dim s As System.IO.Stream
            Try
                s = Me.GetType().Assembly.GetManifestResourceStream("KidsMaskPrint.floodfill.ico")
                PictureBox1.Cursor = New Cursor(s)
            Catch Ex As Exception
                'MessageBox.Show(Ex.Message, "No Stream!")

            Finally
                If Not Microsoft.VisualBasic.IsNothing(s) Then s.Close()
            End Try
        Else
            CurrentTool = Tools.Freehand
            PictureBox1.Cursor = Cursors.Default()
        End If

    End Sub
    Private Sub rdoFloodFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoFloodFill.Click

        AddDebugComment("Form1.rdoFloodFill_Click") 'JM 13/03/2005

        'JM 15/10/2004
        If rdoFloodFill.Checked = True Then
            CurrentTool = Tools.Floodfill
            ''PictureBox1.Cursor = ShowFloodFillCursor()  'JM 17/10/2004Cursors.Hand
            Dim s As System.IO.Stream
            Try
                s = Me.GetType().Assembly.GetManifestResourceStream("KidsMaskPrint.floodfill.ico")
                PictureBox1.Cursor = New Cursor(s)
            Catch Ex As Exception
                'MessageBox.Show(Ex.Message, "No Stream!")

            Finally
                If Not Microsoft.VisualBasic.IsNothing(s) Then s.Close()
            End Try
        Else
            CurrentTool = Tools.Freehand
            PictureBox1.Cursor = Cursors.Default
        End If

    End Sub
    Private Sub lstBrushWidth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBrushWidth.SelectedIndexChanged

        AddDebugComment("Form1.lstBrushWidth_SelectedIndexChanged") 'JM 13/03/2005

        'JM 15/10/2004

        'removed Windings 2 support 'JM 22/10/2004
        Select Case lstBrushWidth.Text
            Case "1" : m_CurrentBrushWidth = 1
            Case "2" : m_CurrentBrushWidth = 2
            Case "4" : m_CurrentBrushWidth = 4
            Case "8" : m_CurrentBrushWidth = 8
        End Select

        m_Drawings.SetColour(m_CurrentColour, m_CurrentBrushWidth) 'JM 15/10/2004

    End Sub
    Friend Sub ChangeUndoRedoStatus()
        'JM 17/10/2004

        Dim TempPieceUndo As Boolean
        Dim TempPieceRedo As Boolean
        '--- pack pieces ---
        If mPieces.Count = 0 Then
            TempPieceUndo = False
        Else
            TempPieceUndo = True
        End If

        If RedoPackPieceArr.Count = 0 Then
            TempPieceRedo = False
        Else
            TempPieceRedo = True
        End If
        '--- pack pieces ---

        Dim TempUserPieceUndo As Boolean
        Dim TempUserPieceRedo As Boolean
        '--- user pieces ---
        If m_UserPieces.Parts.Count = 0 Then
            TempUserPieceUndo = False
        Else
            TempUserPieceUndo = True
        End If

        If RedoUserPieceArr.Count = 0 Then
            TempUserPieceRedo = False
        Else
            TempUserPieceRedo = True
        End If
        '--- user pieces ---

        Dim TempRedoStack As Boolean = True 'JM 19/10/2004
        If RedoSortOrderStack.Count = 0 Then
            TempRedoStack = False
        End If

        Dim TempDrawUndo As Boolean
        Dim TempDrawRedo As Boolean
        m_Drawings.ChangeUndoRedoStatus(TempDrawUndo, TempDrawRedo)

        If TempPieceUndo = True Or TempUserPieceUndo = True Or TempDrawUndo = True Then
            btnUndo.Enabled = True
        Else
            btnUndo.Enabled = False
        End If

        'If TempRedoStack = True Then 'JM 19/10/2004
        '    If TempPieceRedo = True Or TempUserPieceRedo = True Or TempDrawRedo = True Then
        '        btnRedo.Enabled = True
        '    Else
        '        btnRedo.Enabled = False
        '    End If
        'Else 'JM 19/10/2004
        '    btnRedo.Enabled = False 'JM 19/10/2004
        'End If

    End Sub
    Private Sub btnDebug_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDebug.Click

        Dim str As String
        Dim lintArrInc As Integer

        For lintArrInc = 0 To m_SortOrderForData.DataType.Count - 1
            str &= lintArrInc & " "
            Select Case m_SortOrderForData.DataType(lintArrInc)
                Case 0
                    str &= "PackPieces " & Environment.NewLine
                Case 1
                    str &= "NormalGraphicsPath " & Environment.NewLine
                Case 2
                    str &= "ReverseGraphicsPath " & Environment.NewLine
                Case 3
                    str &= "UserPieces " & Environment.NewLine
            End Select
        Next lintArrInc

        MessageBox.Show(str)

    End Sub
    Private Sub btnMoreColours_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreColours.Click

        AddDebugComment("Form1.btnMoreColours_Click") 'JM 13/03/2005

        'JM 18/10/2004

        Application.DoEvents()
        Dim dlgRes As DialogResult
        Dim CD As New ColorDialog()
        With CD
            .Color = btnPCustom.BackColor
            Application.DoEvents()
            .CustomColors = lintCustomColours 'JM 26/10/2004
            dlgRes = .ShowDialog()
            If dlgRes <> DialogResult.OK Then
                Exit Sub
            End If
            lintCustomColours = .CustomColors 'JM 26/10/2004
            btnPCustom.BackColor = .Color
        End With

        Dim ctl As Control
        For Each ctl In pnlPalette.Controls
            If TypeOf ctl Is Label Then
                Dim lab As Label = ctl
                If lab.Tag = "Selected" Then
                    lab.BorderStyle = BorderStyle.None
                    lab.BackColor = Color.FromArgb(0, lab.BackColor)
                    lab.Tag = ""
                    Exit For
                End If
            End If
        Next ctl

        SetPaletteLabel(lblPCustom, btnPCustom)
        m_Drawings.SetColour(btnPCustom.BackColor, m_CurrentBrushWidth)

        btnPalette_Click(btnPCustom, Nothing) 'JM 18/10/2004

    End Sub
    Private Sub mnuToolsOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsOptions.Click
        'JM 18/10/2004

        AddDebugComment("frmMain.mnuToolsOptions_Click - start")

        DeactivatePaintingBeforeDialog()
        Dim dlgRes As DialogResult
        Dim Ops As New options()
        Ops.LoginInAs = mSelectedUser 'JM 18/10/2004
        Ops.Owner = Me
        dlgRes = Ops.ShowDialog()

        If dlgRes = DialogResult.OK Then
            SetDrawingLayout(mSelectedUser)
        End If

        ReactivatePaintingBeforeDialog()

        'AddDebugComment("frmMain.mnuToolsOptions_Click - end")
        gstrProbComtStack &= " #TOCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 'JM 01/05/2005
    End Sub
    Private Sub SetDrawingLayout(ByVal UserName As String)
        'JM 18/10/2004
        Dim AppSettings As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.UserSettings, UserName) 'JM 16/09/2005

        'Dim lbooFloodfill As Boolean = CBool(GetSetting("FloodFillAndPalette", False, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
        'Dim lbooNoFloodfill As Boolean = CBool(GetSetting("LineAndColour", False, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
        'Dim lbooBlackLines As Boolean = CBool(GetSetting("BlackLines", True, InitalXMLConfig.XmlConfigType.UserSettings, UserName))
        'Dim lbooBrushWidths As Boolean = CBool(GetSetting("BrushWidths", False, InitalXMLConfig.XmlConfigType.UserSettings, UserName))

        Dim lbooFloodfill As Boolean = CBool(AppSettings.GetValue("FloodFillAndPalette", "False")) 'JM 16/09/2005
        Dim lbooNoFloodfill As Boolean = CBool(AppSettings.GetValue("LineAndColour", "False")) 'JM 16/09/2005
        Dim lbooBlackLines As Boolean = CBool(AppSettings.GetValue("BlackLines", "True")) 'JM 16/09/2005
        Dim lbooBrushWidths As Boolean = CBool(AppSettings.GetValue("BrushWidths", "False")) 'JM 16/09/2005

        If lbooFloodfill = True Then
            pnlBWPens.Visible = False : pnlPalette.Visible = True : rdoFloodFill.Visible = True : rdoFreehand.Visible = True
        ElseIf lbooNoFloodfill = True Then
            pnlBWPens.Visible = False : pnlPalette.Visible = True : rdoFloodFill.Visible = False : rdoFreehand.Visible = False
            CurrentTool = Tools.Freehand
        ElseIf lbooBlackLines = True Then
            pnlBWPens.Visible = True : pnlPalette.Visible = False : rdoFloodFill.Visible = False : rdoFreehand.Visible = False
            CurrentTool = Tools.Freehand
        End If

        lstBrushWidth.Visible = lbooBrushWidths

        If lstBrushWidth.Visible = False Then
            m_CurrentBrushWidth = 4
        End If

        '--- 'JM 26/10/2004 ---
        Try
            Dim lstrCustomColours() As String
            'lstrCustomColours = Microsoft.VisualBasic.Split(GetSetting("CustomColours", False, InitalXMLConfig.XmlConfigType.UserSettings, UserName), "#")
            lstrCustomColours = Microsoft.VisualBasic.Split(AppSettings.GetValue("CustomColours", False), "#") 'JM 16/09/2005

            ReDim lintCustomColours(lstrCustomColours.GetUpperBound(0))
            Dim lintArrInc As Integer
            For lintArrInc = 0 To lstrCustomColours.GetUpperBound(0)
                lintCustomColours(lintArrInc) = CInt(lstrCustomColours(lintArrInc))
            Next lintArrInc
        Catch
        End Try

        Try
            'btnPCustom.BackColor = StringToColor(GetSetting("LastCustomColour", ColorToString(Color.DarkTurquoise), InitalXMLConfig.XmlConfigType.UserSettings, UserName))
            btnPCustom.BackColor = StringToColor(AppSettings.GetValue("LastCustomColour", ColorToString(Color.DarkTurquoise))) 'JM 16/09/2005
        Catch
        End Try
        '--- 'JM 26/10/2004 ---

    End Sub
    Private Sub lstBrushWidth_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lstBrushWidth.DrawItem

        '--- 'JM 13/03/2005 ---
        If boolstBrushWidth_DrawItemFirstDone = False Then
            AddDebugComment("Form1.lstBrushWidth_DrawItem")
            boolstBrushWidth_DrawItemFirstDone = True
        End If
        '--- 'JM 13/03/2005 ---

        'added 'JM 22/10/2004
        Dim brush As Brush
        Dim Itemselected As Boolean

        e.Graphics.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality

        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds.Left, e.Bounds.Top - 1, e.Bounds.Width, e.Bounds.Height + 2)

        Dim TextTop As Integer = e.Bounds.Top + (e.Bounds.Height / 2)
        Dim lstrExtra As String

        Dim PWidth As Integer
        PWidth = CInt(lstBrushWidth.Items(e.Index))

        Dim TempRect As Rectangle
        TempRect = e.Bounds
        TempRect.Width -= 1

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.FillRectangle(New SolidBrush(SystemColors.Highlight), TempRect)
        Else
            e.Graphics.FillRectangle(New SolidBrush(SystemColors.Window), TempRect)
        End If

        e.Graphics.DrawLine(New Pen(Color.Black, PWidth), 5, TextTop, e.Bounds.Width - 6, TextTop)

    End Sub
    Private Sub mnuHelpReportProblem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpReportProblem.Click

        AddDebugComment("Form1.mnuHelpReportProblem_Click") 'JM 13/03/2005

        'JM 07/11/2004

        DeactivatePaintingBeforeDialog()

        Application.DoEvents()

        Dim ErrRep As New ProbHand.BugReport(True)
        With ErrRep

            Application.DoEvents()
            AddDebugComment("<Font color=Blue>Manual Problem Report</font>")
            DebugDBComment()
            DeactivatePaintingBeforeDialog()
            .Caption = NameMe("")
            .SysStartTime = gdatSystemStart

            .FormIcon = New System.Drawing.Icon( _
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("KidsMaskPrint.kmp.ico"))
            .ShowDialog()
        End With

        ReactivatePaintingBeforeDialog()

    End Sub

    Private Sub btnBuy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuy.Click

        AddDebugComment("Form1.btnBuy_Click") 'JM 15/09/2005
        BrowseToUrl("http://www.KidsMaskPrint.com/buy.php", Me)
    End Sub


    Private Sub rdoFloodFill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoFloodFill.CheckedChanged

    End Sub
End Class

