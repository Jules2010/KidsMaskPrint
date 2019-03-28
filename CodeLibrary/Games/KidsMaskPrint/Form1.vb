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
        System.Reflection.Assembly.Load("AppBasic")) 

    Dim lbooSplashShown As Boolean = False 
    Dim mSelectedUser As String 
    Dim lbooAllowPainting As Boolean = False 

    Dim lbooSomethingDrawn As Boolean = False 

    Dim mLicensedFaceParts As New ArrayList() 

    Dim lintDrawingInProgress As Integer 

    Private m_Drawings As Drawings 

    Private m_UserPieces As FacePartStuctureDataFile 

    Dim CurXPos As Integer 
    Dim CurYPos As Integer 
    Dim ThisFloodFillBitmap As Bitmap 

    Private Enum Tools 
        Freehand
        Floodfill
    End Enum
    Dim CurrentTool As Tools = Tools.Freehand 
    Dim FloodFillJustOccured As Boolean = False 

    Private m_SortOrderForData As SortOrderForData 

    Dim m_CurrentColour As Color = Color.Black 

    Dim m_CurrentBrushWidth As Integer = 4 

    Dim RedoPackPieceArr As New Collections.Stack() 
    Dim RedoUserPieceArr As New Collections.Stack() 
    'Dim LastUndoActivity As SortOrderForData.eDataType 
    Dim RedoSortOrderStack As New Collections.Stack() 
    Friend WithEvents btnDebug As Button 

    Dim lintCustomColours() As Integer 

    Dim lastCustomColour As Color 
    '**************************************************************

    Private booPictureBox1_MouseMoveFirstDone As Boolean = False 
    Private booPictureBox1_PaintFirstDone As Boolean = False 
    Private boolstBrushWidth_DrawItemFirstDone As Boolean = False 

    Dim AppSettingsStartup As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings) 

#Region " Windows Form Designer generated code "

    Friend Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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

        gstrProbComtStack = "Form1.btnExit_Click - start" 

        Me.Close()
        gstrProbComtStack &= " #BECEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuHelpSupportInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpSupportInfo.Click
        
        AddDebugComment("Form1.mnuHelpSupportInfo_Click") 
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(HelpTopicEnum.support))

    End Sub
    Private Sub mnuFilePageSetup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePageSetup.Click

        gstrProbComtStack = "Form1.mnuFilePageSetup_Click - start" 

        DeactivatePaintingBeforeDialog() 

        Application.DoEvents()

        Try
            Dim PgSetupDlg As New PageSetupDialog()
            PgSetupDlg.PageSettings = m_PageSettings
            PgSetupDlg.AllowPrinter = True 
            PgSetupDlg.AllowPaper = True 
            PgSetupDlg.AllowMargins = True 
            PgSetupDlg.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #FPSEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click

        AddDebugComment("Form1.mnuFileExit_Click") 

        btnExit_Click(Nothing, Nothing)

    End Sub
    Private Sub mnuHelpHelpTopics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpHelpTopics.Click

        AddDebugComment("Form1.mnuHelpHelpTopics_Click") 

        
        Help.ShowHelp(Me, GetHelpFile, HelpNavigator.TableOfContents)

    End Sub
    Private Sub mnuHelpCheckForUpdates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpCheckForUpdates.Click

        gstrProbComtStack = "frmMain.mnuHelpCFU_Click - start" 

        DeactivatePaintingBeforeDialog() 
        Application.DoEvents() 

        If hasMultipleInstances(gProgName, NameMe(""), Me.Handle, StandLangText) = True Then
            ReactivatePaintingBeforeDialog() 
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

        If gbooNeedToRestartProgAfterCFU = True Then 
            SaveBeforeExitProg() 
        End If

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #HCUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
        If gbooNeedToRestartProgAfterCFU = True Then
            SaveSetting("BuyPackShowNext", True, InitalXMLConfig.XmlConfigType.AppSettings, "") 

            Me.Close()
        End If

    End Sub
    Private Sub mnuHelpInstallAddon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpInstallAddon.Click
        
        gstrProbComtStack = "frmMain.mnuHelpInstallPack_Click - start" 

        DeactivatePaintingBeforeDialog() 

        Dim lstrAddOnFile As String

        Application.DoEvents()

        If hasMultipleInstances(gProgName, NameMe(""), Me.Handle, StandLangText) = True Then
            ReactivatePaintingBeforeDialog() 
            Exit Sub
        End If

        Application.DoEvents()

        Dim OpenAddon As New OpenFileDialog()
        With OpenAddon
            .CheckFileExists = True
            .CheckPathExists = True
            .Filter = "Mindwarp Consultancy Ltd AddOn (*.mcla;*.zip)|*.mcla;*.zip"
            If .ShowDialog() <> DialogResult.OK Then
                ReactivatePaintingBeforeDialog() 
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

            SaveBeforeExitProg() 
            ReactivatePaintingBeforeDialog() 
            gstrProbComtStack &= " #middle"
            Me.Close()
            Try : Me.Close() : Catch : End Try
        Catch
            Busy(Me, False)

            MessageBox.Show(LangText2.GetString("CFU_DownloadIncompatible"), NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ReactivatePaintingBeforeDialog() 
            Try : File.Delete(gstrCFUTempDir & "\unzip\addon.dat") : Catch : End Try
        End Try

        gstrProbComtStack &= " #HIAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        
        gstrProbComtStack = "frmMain.mnuHelpAbout_Click - start" 

        DeactivatePaintingBeforeDialog() 
        Application.DoEvents() 

        Dim NewAbout As New frmAbout()
        With NewAbout
            .Owner = Me
            .ShowDialog()
        End With

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #HAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuHelpSubscribeNewsletter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpSubscribeNewsletter.Click
        
        gstrProbComtStack = "frmMain.mnuHelpSubscribe_Click - start" 

        Dim NewsString As String = "mailto:newsletter@example.com?" & _
                "subject=Newsletter Subscriptions Dept&body=Dear Sirs,  Kindly add me to your Kids Mask Print Newsletter!"

        Try
            Process.Start(NewsString)
        Catch

            Dim Email As String
            Email = LeftGet(NewsString, InStrGet(NewsString.ToUpper, "?subject".ToUpper) - 1).Replace("mailto:", "")
            Dim msg As String = "Please check you that you have an email program installed on your computer (e.g. Outlook)." & _
                Environment.NewLine & "Alternatively, send an email to " & Email & " to be added to the newsletter mailing list."

            DeactivatePaintingBeforeDialog() 
            MessageBox.Show(msg, NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Information)
            ReactivatePaintingBeforeDialog() 
        End Try

        gstrProbComtStack &= " #HSNEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuHelpEnterCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpEnterCode.Click
        
        gstrProbComtStack = "frmMain.mnuHelpEnterCode_Click - start" 

        DeactivatePaintingBeforeDialog() 

        Application.DoEvents() 

        If AcceptLicense(Me) = True Then
            Me.Text = NameMe("")
            StandardUpgradeTidy()
            SaveBeforeExitProg() 
            Me.Close()
        End If

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #HECEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuHelpLicenseAgreement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpLicenseAgreement.Click
        
        gstrProbComtStack = "frmMain.mnuHelpLicense_Click - start" 

        DeactivatePaintingBeforeDialog() 
        Application.DoEvents() 

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

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #HLAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileImportGraphics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileImportGraphics.Click

        gstrProbComtStack = "frmMain.mnuFileImportGraphics_Click - start" 

        Dim fs As IO.FileStream = New IO.FileStream("D:\desktopnt\mask.gif", IO.FileMode.Open, IO.FileAccess.Read)
        Dim img As System.Drawing.Bitmap = New System.Drawing.Bitmap(fs)
        fs.Close()

        Clear()

        gstrProbComtStack &= " #FIGEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileExportGraphics_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExportGraphics.Click

        If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
            Dim dlgRes As DialogResult
            dlgRes = MessageBox.Show("This feature is only available in the full version of this program" & CR() & CR() & _
                "Would you like to visit our website to view purchasing options?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If dlgRes = DialogResult.Yes Then
                BrowseToUrl("http://www.example.com/buy.php", Me)
            End If
            Exit Sub
        End If

        ' Displays a SaveFileDialog so the user can save
        ' the Image
        gstrProbComtStack = "frmMain.mnuFileExportGraphics_Click - start" 
        DeactivatePaintingBeforeDialog() 

        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Gif Image|*.gif|" & "Jpeg Image|*.jpg|" & "Bitmap Image|*.bmp|" & "Tiff Image|*.tiff"
        saveFileDialog1.Title = "Save an Image File"
        saveFileDialog1.ShowDialog()
        ReactivatePaintingBeforeDialog() 

        Me.Update() 

        ' If the file name is not an empty string open it for
        ' saving.
        If saveFileDialog1.FileName <> "" Then

            ' Saves the Image via a FileStream created by the
            ' OpenFile method.
            Dim fs As System.IO.FileStream = CType _
            (saveFileDialog1.OpenFile(), System.IO.FileStream)

            
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

        PictureBox1.Invalidate() 

        gstrProbComtStack &= " #FEGEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileImportMask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileImportMask.Click
        gstrMRPs = "0300"

        gstrProbComtStack = "frmMain.mnuFileImportMask_Click - start" 

        Dim ImportedText As String

        DeactivatePaintingBeforeDialog() 

        Dim ie As New ImpEx()
        ie.Label = "Please paste in your import mask codes"
        ie.Caption = NameMe("Import Mask")
        ie.Owner = Me 
        ie.ShowDialog()
        ReactivatePaintingBeforeDialog() 

        Me.Update() 

        ImportedText = ie.TextBlock

        If ie.TextBlock = "" Then
            Exit Sub
        End If

        Clear() 

        Busy(Me, True) 

        If RightGet(ImportedText, 1) <> Environment.NewLine Then 
            ImportedText &= Environment.NewLine 
        End If

        Try 
            Dim lintArrInc As Integer
            Dim de As String() = Microsoft.VisualBasic.Split(ImportedText, Environment.NewLine)
            For lintArrInc = 0 To de.GetUpperBound(0) - 1
                Dim ThisItem As String = de(lintArrInc)
                If de(0) = "-1" Then
                    'lstrVersion = de.Value
                ElseIf ThisItem = "" Then
                    'do nothing
                Else

                    Dim ThisPiece As New Piece()

                    ThisPiece.SourceDataFileName = AppBasic.ReturnNthStr(de(lintArrInc), 1, "|").Replace(fpDir, "")
                    ThisPiece.DataFileItemNum = CInt(AppBasic.ReturnNthStr(de(lintArrInc), 2, "|"))
                    ThisPiece.VertFlip = (CBool(AppBasic.ReturnNthStr(de(lintArrInc), 5, "|")))
                    ThisPiece.HorizFlip = (CBool(AppBasic.ReturnNthStr(de(lintArrInc), 6, "|")))

                    Dim TempPart As New KidsMaskPrint.Part()
                    GetDataPreviewImage(ThisPiece.SourceDataFileName, ThisPiece.DataFileItemNum, TempPart, Nothing, Nothing) 
                    ThisPiece.SetImageObj(TempPart.FullImage)

                    'don't bother loading thumb image
                    'Dim loc As New Point(CSng(AppBasic.ReturnNthStr(de(lintArrInc), 1, "|")), CSng(AppBasic.ReturnNthStr(de(lintArrInc), 2, "|")))
                    
                    Dim loc As New Point(CSng(AppBasic.ReturnNthStr(de(lintArrInc), 3, "|")), CSng(AppBasic.ReturnNthStr(de(lintArrInc), 4, "|")))
                    ThisPiece.Location = loc

                    mPieces.Add(ThisPiece)
                    'will need to just build a incremental list
                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "mnuFileImportMask_Click") 
                    ChangeUndoRedoStatus() 
                End If
            Next lintArrInc

        Catch 
            
            Busy(Me, False) 
            MessageBox.Show("Please ensure that the mask data contains the exact details provided.  Remove any extra letters etc.", NameMe(""), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        Busy(Me, False) 

        gstrProbComtStack &= " #FIMEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileExportMask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExportMask.Click
        gstrMRPs = "0310"

        gstrProbComtStack = "frmMain.mnuFileExportMask_Click - start" 

        DeactivatePaintingBeforeDialog() 

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
        ie.Owner = Me 
        ie.ShowDialog()
        ReactivatePaintingBeforeDialog() 

        Me.Update() 

        gstrProbComtStack &= " #FEMEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileLoad.Click

        gstrProbComtStack = "frmMain.mnuFileLoad_Click - start" 

        DeactivatePaintingBeforeDialog() 

        Dim MaskFile As String 
        Dim sm As New SelectMask()

        sm.LicensedFaceParts = mLicensedFaceParts 

        sm.Owner = Me 
        sm.ShowDialog()


        Me.Update() 
        MaskFile = sm.RetMaskFile

        If MaskFile = "" Then Exit Sub
        
        LoadMask(MaskFile, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
            m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)

        m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

        AddDebugComment("frmMain.mnuFileLoad_Click - middle") 

        ReactivatePaintingBeforeDialog() 

        gstrProbComtStack &= " #FLEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuFileSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click

        gstrProbComtStack = "frmMain.mnuFileSave_Click - start" 

        Dim FileName As String = Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location) & "\" & "\Masks\" 'data.txt"
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Mask File|*.mask"
        saveFileDialog1.Title = "Save a mask File"
        saveFileDialog1.InitialDirectory = FileName
        saveFileDialog1.ShowDialog()
        Me.Update() 

        ' If the file name is not an empty string open it for        
        If saveFileDialog1.FileName = "" Then Exit Sub

        FileName = saveFileDialog1.FileName 

        Dim FullImage As Image
        Dim hash As New SortedList()
        Dim keptPieces As New ArrayList()

        CreateHashAndImage(hash, FullImage, keptPieces)

        mPieces = keptPieces

        SaveUserMask(FileName, hash, FullImage, m_UserPieces, m_SortOrderForData)      

        MessageBox.Show("Saved!", NameMe("")) 

        gstrProbComtStack &= " #FSEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuToolsDeleteUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsDeleteUsers.Click

        gstrProbComtStack = "frmMain.mnuToolsDeleteUsers_Click - start" 

        DeactivatePaintingBeforeDialog()

        SaveBeforeExitProg() 

        Dim DUs As New UsersGeneral()
        DUs.Owner = Me
        DUs.TranType = UsersGeneral.UserTranType.Delete
        DUs.ShowDialog()

        ReactivatePaintingBeforeDialog()

        gstrProbComtStack &= " #TDUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub mnuToolsRenameUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsRenameUsers.Click

        gstrProbComtStack = "frmMain.mnuToolsRenameUsers_Click - start" 

        DeactivatePaintingBeforeDialog()

        SaveBeforeExitProg() 

        Dim RUs As New UsersGeneral()
        RUs.Owner = Me
        RUs.TranType = UsersGeneral.UserTranType.Rename
        RUs.ShowDialog()

        'just in case current logged user name is renamed.
        If RUs.LoginInAs <> "" Then
            mSelectedUser = RUs.LoginInAs
        End If

        ReactivatePaintingBeforeDialog()

        gstrProbComtStack &= " #TRUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub


#End Region
#Region "Various Functions"
    Private Sub CreateHashAndImage(ByRef pHash As SortedList, ByRef pFullRetImage As Image, _
      ByRef pKeptPieces As ArrayList)

        AddDebugComment("frmMain.CreateHashAndImage - start") 

        Busy(Me, True) 

        'produce debug report of all values about to be saved

        Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
            mPieces, m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData) ' DrawingWithoutHelpers()

        Dim hash As New SortedList()

        hash.Add("AVER", "1.0")

        Dim iPiece As Piece
        Dim lintArrInc As Integer
        For Each iPiece In mPieces
            Dim str As String
            
            str = iPiece.SourceDataFileName.Replace(fpDir, "") & "|" & iPiece.DataFileItemNum & "|" & iPiece.Location.X & _
                "|" & iPiece.Location.Y & "|" & iPiece.VertFlip & "|" & iPiece.HorizFlip & "|" & iPiece.PieceName & "|"
            hash.Add("ZZZZ" & lintArrInc.ToString, str) 
            lintArrInc += 1
        Next iPiece


        With m_Drawings
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

        End With

        Dim keptPieces As New ArrayList()
        keptPieces = mPieces
        mPieces = Nothing

        pHash = hash
        pFullRetImage = FullImage
        pKeptPieces = keptPieces

        Busy(Me, False) 

        AddDebugComment("frmMain.CreateHashAndImage - end") 

    End Sub
    Private Sub LoadFaceParts(ByRef pDataFilesDescImages As ArrayList, ByRef pDataFilesDescriptions As ArrayList, _
        ByRef pDataFilesProdNums As ArrayList, ByRef pDataFiles As ArrayList, ByRef pDataFileState As ArrayList, _
        ByVal pbooJustPurchasing As Boolean, ByRef pLicensedFaceParts As ArrayList, ByRef LicenseStr As String)

        AddDebugComment("frmMain.LoadFaceParts - start") 

        Busy(Me, True) 

        '-------------- Check License if available -------------
        Dim Dets2 As strat1.UnlockDetails
        Dim Info As New System.IO.FileInfo(System.IO.Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")

        If Info.Exists Then
            Try
                Unlock(System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", Dets2, "", "")

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
            Catch

            End Try
        End If

        '-------------- Check License if available -------------

        Dim source As DirectoryInfo = New DirectoryInfo(Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location) & "\FaceParts\")

        'iterate data file directory
        Dim files() As FileInfo = source.GetFiles("*.dat")
        Dim pfile As FileInfo

        For Each pfile In files
            Try ' this will cater for old data files

                Dim FPs As FacePartStuctureDataFile = UnlockFacePartsPack(pfile.FullName) 

                If pfile.Name.ToLower <> "basic.dat" Then 
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
                            Throw New Exception(" ")
                        End If
                    Else
                        pDataFileState.Add("0")
                        Throw New Exception(" ")
                    End If

                    pDataFileState.Add("1")
                End If 

                '--- this block checks for a valid key file and doesn't all it to be used if it isn't ---

                If pbooJustPurchasing = True Then 
                    Throw New Exception(" ")
                End If

                Dim lintArrInc As Integer
                For lintArrInc = 0 To FPs.Parts.Count - 1
                    Dim ThisPart As New KidsMaskPrint.Part()
                    ThisPart = FPs.Parts(lintArrInc)

                    pLicensedFaceParts.Add(ThisPart.FaceMaster) 

                Next lintArrInc
            Catch ex As Exception
                '
            End Try
        Next pfile

        Busy(Me, False) 

        AddDebugComment("frmMain.LoadFaceParts - end") 

    End Sub
    Private Sub xAddSelectedFacePart(ByVal pFP As Part, ByVal pSel As FacePartEnums.ePositionSelection, _
        ByVal SourceDatFileName As String, ByVal DataFileItemNum As Integer)

        AddDebugComment("frmMain.AddSelectedFacePart - start") 

        Busy(Me, True) 

        If Not pFP Is Nothing Then

            Select Case pSel
                Case FacePartEnums.ePositionSelection.Left
                    Dim ThisPiece As New Piece()
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.LeftPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 
                    mPieces.Add(ThisPiece)
                Case FacePartEnums.ePositionSelection.Both

                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = False
                    thispiece.VertFlip = False
                    ThisPiece.SetImageObj(pFP.FullImage.Clone)
                    ThisPiece.Location = pFP.LeftPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 
                    mPieces.Add(ThisPiece)

                    Dim ThisPiece2 As New Piece()
                    ThisPiece2.HorizFlip = True
                    ThisPiece2.VertFlip = False
                    ThisPiece2.SetImageObj(pFP.FullImage)
                    ThisPiece2.Location = pFP.RightPart
                    ThisPiece2.PieceName = pFP.FaceMaster 
                    ThisPiece2.SourceDataFileName = SourceDatFileName 
                    ThisPiece2.DataFileItemNum = DataFileItemNum 
                    mPieces.Add(ThisPiece2)

                Case FacePartEnums.ePositionSelection.Right
                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = True
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.RightPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ''ThisPiece.Bitmapname = pFP.FaceMaster '& " Right"
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 

                    mPieces.Add(ThisPiece)
            End Select

            m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "AddSelectedFacePart") 
            ChangeUndoRedoStatus() 
        End If

        Busy(Me, False) 

        Me.Update() 

        PictureBox1.Invalidate() 

        AddDebugComment("frmMain.AddSelectedFacePart - end") 

    End Sub
    Private Sub DeactivatePaintingBeforeDialog()
        
        lbooAllowPainting = False

    End Sub
    Private Sub ReactivatePaintingBeforeDialog()
        
        lbooAllowPainting = True

        PictureBox1.Invalidate() 

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)


        
        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)
        Me.Update() 

    End Sub
    Private Sub SetBackcolors()

        AddDebugComment("frmMain.SetBackcolors - start") 

        chkMirror.BackColor = Color.FromArgb(0, chkMirror.BackColor)
        chkGuide.BackColor = Color.FromArgb(0, chkGuide.BackColor)
 
        lblPen.BackColor = Color.FromArgb(0, lblPen.BackColor)

        pnlBWPens.BackColor = Color.FromArgb(0, pnlBWPens.BackColor) 
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

        AddDebugComment("frmMain.SetBackcolors - end") 

    End Sub
    Private Function KidsSave(ByVal pbooIsLastMask As Boolean)

        AddDebugComment("frmMain.KidsSave - start") 

        DeactivatePaintingBeforeDialog() 
        
        Dim FullImage As Image
        Dim hash As New SortedList()
        Dim keptPieces As New ArrayList()

        m_Drawings.PreSave(m_CurrentColour, m_CurrentBrushWidth) 

        CreateHashAndImage(hash, FullImage, keptPieces)

        mPieces = keptPieces

        Dim SaveSlots As New Slots()
        With SaveSlots
            .Owner = Me 
            .TranType = Slots.eTranType.Save
            .SelectedUser = mSelectedUser
            .FullImage = FullImage
            .FaceHash = hash
            .LastMask = pbooIsLastMask 
            .LicensedFaceParts = mLicensedFaceParts 
            .UserPieces = m_UserPieces 
            .SortOrderForData = m_SortOrderForData 
            .ShowDialog()

            ChangeUndoRedoStatus() 

            ReactivatePaintingBeforeDialog() 
        End With

        AddDebugComment("frmMain.KidsSave - end") 

    End Function

#End Region
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown

        AddDebugComment("frmMain.PictureBox1_MouseDown " & e.Button) 

        If CurrentTool <> Tools.Floodfill Then 
            If e.Button = MouseButtons.Left Then 
                mMouseDownStart = New Point(e.X, e.Y)
                'Find clicked piece 
                Dim iPiece As Piece
                For Each iPiece In mPieces
                    If iPiece.IsPointOverMe(mMouseDownStart) Then
                        mMousePiece = iPiece
                        mMousePieceStart = iPiece.Location
                    End If
                Next iPiece

            ElseIf e.Button = MouseButtons.Right Then 

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
                        If Not mMousePiece Is Nothing Then 
                            Properties.ShowDialog()
                            Select Case Properties.TranType
                                Case PieceProps.ePieceTran.Delete
                                    mPieces.Remove(mMousePiece)
                            End Select

                            mMousePiece = Nothing 
                            PieceSelected = False 
                            Me.Update() 
                        End If
                    Catch

                    End Try
                Else 
                    '
                End If

            End If 

            If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then
                If mMousePiece Is Nothing Then
                    m_Drawings.MouseDownClick(chkMirror.Checked, m_CurrentBrushWidth) 

                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseDown") 
                    ChangeUndoRedoStatus() 
                    lbooSomethingDrawn = True 
                End If
            End If
        End If 

    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove

        If booPictureBox1_MouseMoveFirstDone = False Then
            AddDebugComment("Form1.PictureBox1_MouseMove")
            booPictureBox1_MouseMoveFirstDone = True
        End If

        If CurrentTool <> Tools.Floodfill Then 
            If mMousePiece Is Nothing Then 
                If e.Button = MouseButtons.Left Or e.Button = MouseButtons.Right Then ' draw a filled circle if left mouse is down 
                    m_Drawings.MouseMoveClick(e, chkMirror.Checked = True) 

                    m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseMove") 
                    ChangeUndoRedoStatus() 
                End If

                PictureBox1.Invalidate() 'Repaint the PictureBox using the PictureBox1 Paint event
            End If 

            If Not (mMousePiece Is Nothing) Then
                'Request redraw for piece's current location 
                PictureBox1.Invalidate(mMousePiece.Bounds)
                'Move the piece 
                mMousePiece.Location = New Point(mMousePieceStart.X + e.X - mMouseDownStart.X, mMousePieceStart.Y + e.Y - mMouseDownStart.Y)
                'Request redraw for the piece's new location 
                PictureBox1.Invalidate(mMousePiece.Bounds)
            End If

            Dim iPiece As Piece
            For Each iPiece In mPieces
                If iPiece.IsPointOverMe(New Point(e.X, e.Y)) Then
                    PictureBox1.Cursor.Current = Cursors.Hand
                    Exit For
                Else
                    PictureBox1.Cursor.Current = Cursors.Default
                End If
            Next iPiece

        End If 

        If CurrentTool = Tools.Floodfill And FloodFillJustOccured = True Then
            FloodFillJustOccured = False
            PictureBox1.Invalidate()
        End If

        CurXPos = e.X 
        CurYPos = e.Y 

        StatusBarPanel1.Text = e.X & " x " & e.Y

    End Sub
    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp

        AddDebugComment("frmMain.PictureBox1_MouseUp " & e.Button) 

        If CurrentTool <> Tools.Floodfill Then 
            'This code should make undo work better, rather than deleting all drawing
            m_Drawings.MouseUP(m_CurrentBrushWidth) 

            m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_MouseMove") 

            ChangeUndoRedoStatus() 

            mMousePiece = Nothing
        End If 

    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint

        If booPictureBox1_MouseMoveFirstDone = False Then
            AddDebugComment("Form1.PictureBox1_Paint")
            booPictureBox1_MouseMoveFirstDone = True
        End If

        If lbooAllowPainting = True Then 

            DrawOutput.DrawOutput(e.Graphics, False, PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                Nothing, Nothing, chkMirror.Checked, chkGuide.Checked, mPieces, New Point(0, 0), m_Drawings.lPaintBrush, _
                m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData)
        End If 

    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        gstrMRPs = "0320"

        gstrProbComtStack = "frmMain.btnPrint_Click - start" 

        DeactivatePaintingBeforeDialog() 

        Dim ppScreen As New PrintPreview()
        With ppScreen
            .MainPictureBox = PictureBox1
            .MousePath = m_Drawings.mousePath
            .ReverseMousePath = m_Drawings.ReversemousePath
            .ThisPaintBrush = m_Drawings.lPaintBrush 
            .ThisPaintReverseBrush = m_Drawings.lPaintReverseBrush 
            Dim lpieces As ArrayList = mPieces 
            .Pieces = lpieces 
            .UserPieces = m_UserPieces 
            .SortOrder = m_SortOrderForData 
            .Owner = Me 
            .ShowDialog()
            ReactivatePaintingBeforeDialog() 

            Me.Update() 
        End With

        gstrProbComtStack &= " #BPCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        Me.Update()

        If mbooLoadAllDataOnce = True Then

            gstrProbComtStack = "frmMain.Form1_Activated - start" 
            mbooLoadAllDataOnce = False
            gstrMRPs = "0131"

            Dim DataFilesDescImages As New ArrayList()
            Dim DataFilesDescriptions As New ArrayList()
            Dim DataFilesProdNums As New ArrayList()
            Dim DataFiles As New ArrayList()
            Dim DataFileState As New ArrayList()

            Dim licenseStr As String 
            LoadFaceParts(DataFilesDescImages, DataFilesDescriptions, DataFilesProdNums, DataFiles, DataFileState, False, mLicensedFaceParts, licenseStr) 

            gstrProbComtStack &= " #FMA1" 
            Me.Text = NameMe("") 

            StatusBarPanel1.Text = "Licensed to : " & licenseStr 
            licenseStr = "" 
            gstrMRPs = "0150"
            DeactivatePaintingBeforeDialog() 

            Dim lbooShowPurchasing As Boolean = False
            Try
                lbooShowPurchasing = CBool(AppSettingsStartup.GetValue("BuyPackShowNext", "")) 
            Catch : End Try

            gstrProbComtStack &= " #FMA2" 

            If DataFiles.Count > 1 Then 
                If mintVersion = 32 And lbooShowPurchasing = True Then  
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
            End If 

            gstrMRPs = "0175"
            gstrProbComtStack &= " #FMA3" 

            Dim lbooSixMonthVersionCFUDone As Boolean   
            Dim InitialConfig1 As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.AppSettings)
            Dim lstrUsersStr As String
            lstrUsersStr = InitialConfig1.GetValue("Users", "")
            lbooSixMonthVersionCFUDone = CBool(InitialConfig1.GetValue("SixMonthVersionCFUDone", False)) 
            InitialConfig1 = Nothing

            gstrProbComtStack &= " #FMA4" 

            Dim lbooUseNewUserScreen As Boolean = False 
            gstrMRPs = "0183"
            If lstrUsersStr = "" Then
                lbooUseNewUserScreen = True
                gstrMRPs = "0184"
            Else
                gstrMRPs = "0186"

                Dim frmSignIn As New SignIn()
                frmSignIn.Owner = Me 
                frmSignIn.ShowDialog()
                If frmSignIn.Param = SignIn.Params.None Then
                    mSelectedUser = frmSignIn.SelectedUser
                    UIStyle.gPaintClr1 = frmSignIn.UICol1 
                    UIStyle.gPaintClr2 = frmSignIn.UICol2 
                Else
                    lbooUseNewUserScreen = True
                End If
                gstrMRPs = "0189"
            End If

            gstrProbComtStack &= " #FMA5" 

            If lbooUseNewUserScreen = True Then
                Dim frmNewUser As New NewUser()
                frmNewUser.Owner = Me
                frmNewUser.ShowDialog()
                mSelectedUser = frmNewUser.SelectedUser
            End If

            SetDrawingLayout(mSelectedUser) 

            gstrProbComtStack &= " #FMA6" 
            gstrMRPs = "0199"

            Dim InitialConfig As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            'If last face available, as if you want to load it.

            With InitialConfig
                Try
                    Dim LastSavedmaskFile As String = .GetValue("LastSaved", "")
                    If File.Exists(LastSavedmaskFile) = True Then
                        Me.Invalidate() 
                        Dim dlgRes As DialogResult
                        DeactivatePaintingBeforeDialog() 
                        dlgRes = MessageBox.Show("Load the mask you were working on?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        ReactivatePaintingBeforeDialog() 
                        If dlgRes = DialogResult.Yes Then
                            
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

            gstrProbComtStack &= " #FMA6M" 
            If lbooSixMonthVersionCFUDone = False Then
                Me.Invalidate() 
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

            gstrMRPs = "0240"
            gstrProbComtStack &= " #FMA7" 

            ReactivatePaintingBeforeDialog() 

            Dim ShowWelcome As Boolean = CBool(AppSettingsStartup.GetValue("WELCOME", "True")) 

            If ShowWelcome = True Then
                Me.Invalidate()
                Dim BM As New InformParent()
                BM.Owner = Me 
                BM.ShowDialog()

            End If

            'used for W98 bug of now drawing buttons properly
            If IsAboveOrEqualWinXp() = False Then
                Me.WindowState = FormWindowState.Minimized
                System.Threading.Thread.Sleep(1000)
                Me.WindowState = FormWindowState.Maximized

            End If

            'fixes menu bar not being coloured in problem
            Dim MenuItem As New MenuItem()
            MainMenu1.MenuItems.Add(MenuItem)
            MainMenu1.MenuItems.Remove(MenuItem)

            'used to update background colours 
            Me.Invalidate() 
            gstrMRPs = "0255"

            gstrProbComtStack &= " #FMAEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
        End If
    End Sub
    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gstrMRPs = "0101"
        Busy(Me, True) 

        gstrProbComtStack = "frmMain.Form1_Load - start" 

        Me.Text = NameMe("") 

        m_Drawings = New Drawings(m_CurrentColour, m_CurrentBrushWidth) 

        m_UserPieces = New FacePartStuctureDataFile() 

        m_SortOrderForData = New SortOrderForData() 

        If IsAboveOrEqualWinXp() = True Then
            rdoFloodFill.FlatStyle = FlatStyle.System
            rdoFreehand.FlatStyle = FlatStyle.System
        End If

        gstrMRPs = "0110"

        SetBackcolors()

        SetPaletteLabel(lblPBlack, btnPBlack) 

        With lstBrushWidth
            .Items.Clear()
            .Items.Add("1") ' 
            .Items.Add("2") ' 
            .Items.Add("4") ' 
            .Items.Add("8") ' 
            .SelectedIndex = 2
        End With

        'Load clown mask
        gstrMRPs = "0121" 
        Dim ShowClown As Boolean = CBool(AppSettingsStartup.GetValue("SHOWCLOWN", "True")) 

        If ShowClown = True Then
            gstrMRPs = "0122" 
            Try
                Dim ClownMask As String = System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetEntryAssembly.Location.ToString()) & "\Clown.mask"
                If File.Exists(ClownMask) = True Then
                    LoadMask(ClownMask, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                    m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)
                    m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

                End If
                SaveSetting("SHOWCLOWN", "False", InitalXMLConfig.XmlConfigType.AppSettings, "")
            Catch
                '
            End Try
        End If

        Busy(Me, False) 

        gstrMRPs = "0130"
        gstrProbComtStack &= " #FMLEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub frmMain_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter

        lbooAllowPainting = True 

    End Sub
    Private Sub frmMain_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave

        lbooAllowPainting = False 

    End Sub
    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        
        gstrProbComtStack = "frmMain.frmMain_Closing - start" 

        SaveBeforeExitProg() 

        DeleteTempFiles()

        Me.Visible = False 

        If InStrGet((NameMe("")).ToUpper, "TRIAL") > 0 Then
            Welcome(lbooSplashShown, Me)
        End If

        gstrProbComtStack &= " #FMCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub SaveBeforeExitProg()
        

                If mPieces.Count > 0 Or lbooSomethingDrawn = True Then 
            DeactivatePaintingBeforeDialog() 
            Dim dlgRes As DialogResult = MessageBox.Show("Save this Mask?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            Application.DoEvents() 
            ReactivatePaintingBeforeDialog() 
            If dlgRes = DialogResult.Yes Then
                KidsSave(True)
            Else
                SaveSetting("LastSaved", "", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            End If
        End If
        
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
            End Sub
    Private Sub btnUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        gstrMRPs = "0330"

        gstrProbComtStack = "frmMain.btnUndo_Click - start" 
        
        btnUndo.Enabled = False

        gstrProbComtStack &= " #BU1" 
        'this clears and redo then user draw something new from redoing everything
        If RedoSortOrderStack.Count = 0 Then
            RedoPackPieceArr.Clear()
            RedoUserPieceArr.Clear()
        End If
        
        gstrProbComtStack &= " #BU2" 

                Try 
            If m_SortOrderForData.DataType.Count = -1 Then
                Exit Sub
            End If
        Catch 
            gstrProbComtStack &= " #BU2b"
            'Czech Republic - Anonymous bug report - index problem ?
            Exit Sub 
        End Try 
        
        
        Dim LastActivity As SortOrderForData.eDataType = m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1)
        Select Case LastActivity
            Case SortOrderForData.eDataType.PackPieces
                gstrProbComtStack &= " #BU3" 
                RedoPackPieceArr.Push(mPieces(mPieces.Count - 1)) 
                mPieces.RemoveAt(mPieces.Count - 1) 
            Case SortOrderForData.eDataType.NormalGraphicsPath, SortOrderForData.eDataType.ReverseGraphicsPath
                gstrProbComtStack &= " #BU4" 
                m_Drawings.Undo(m_CurrentBrushWidth)  
                gstrProbComtStack &= " #BU4b" 
                Try
                    If m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1) = SortOrderForData.eDataType.NormalGraphicsPath Or _
                         m_SortOrderForData.DataType(m_SortOrderForData.DataType.Count - 1) = SortOrderForData.eDataType.ReverseGraphicsPath Then
                        gstrProbComtStack &= " #BU4c" 
                        m_Drawings.Undo(m_CurrentBrushWidth)  
                        gstrProbComtStack &= " #BU4d" 
                    End If
                Catch
                End Try
            Case SortOrderForData.eDataType.UserPieces
                gstrProbComtStack &= " #BU5" 
                RedoUserPieceArr.Push(m_UserPieces.Parts(m_UserPieces.Parts.Count - 1)) 
                m_UserPieces.Parts.RemoveAt(m_UserPieces.Parts.Count - 1) 
        End Select

        gstrProbComtStack &= " #BU6" 

        RedoSortOrderStack.Push(LastActivity) 

        gstrProbComtStack &= " #BU7" 

        m_SortOrderForData.Remove(mPieces, m_Drawings.mousePath, _
            m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnUndo_Click") 

        gstrProbComtStack &= " #BU8" 

        ChangeUndoRedoStatus() 

        PictureBox1.Invalidate() 

        gstrProbComtStack &= " #BUEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        gstrMRPs = "0340"

        gstrProbComtStack = "frmMain.btnRedo_Click - start" 

        
        btnUndo.Enabled = False

        Select Case RedoSortOrderStack.Peek 
            Case SortOrderForData.eDataType.PackPieces
                gstrProbComtStack &= " #BR1" 
                Dim lRedoPiece As New Piece()
                Dim EnumerShowNow As System.Collections.IEnumerator

                gstrProbComtStack &= " #BR1b" 
                EnumerShowNow = RedoPackPieceArr.GetEnumerator()
                EnumerShowNow.MoveNext()

                gstrProbComtStack &= " #BR1c" 
                lRedoPiece = EnumerShowNow.Current

                gstrProbComtStack &= " #BR1d" 
                RedoPackPieceArr.Pop()
                mPieces.Add(lRedoPiece)
                gstrProbComtStack &= " #BR1e" 

            Case SortOrderForData.eDataType.NormalGraphicsPath, SortOrderForData.eDataType.ReverseGraphicsPath
                gstrProbComtStack &= " #BR2" 
                m_Drawings.Redo()

                gstrProbComtStack &= " #BR2b" 
                Try
                    gstrProbComtStack &= " #BR2c" 
                    Dim NextRedo As SortOrderForData.eDataType
                    Dim EnumerShowNow As System.Collections.IEnumerator

                    gstrProbComtStack &= " #BR2d" 
                    EnumerShowNow = RedoSortOrderStack.GetEnumerator()
                    EnumerShowNow.MoveNext()

                    gstrProbComtStack &= " #BR2e" 
                    NextRedo = EnumerShowNow.Current

                    If NextRedo = SortOrderForData.eDataType.NormalGraphicsPath Or NextRedo = SortOrderForData.eDataType.ReverseGraphicsPath Then
                        gstrProbComtStack &= " #BR2f" 
                        m_Drawings.Redo()
                    End If
                Catch
                End Try
                gstrProbComtStack &= " #BR2g" 
            Case SortOrderForData.eDataType.UserPieces
                gstrProbComtStack &= " #BR3" 
                Try 
                    Dim lRedoUserPiece As New Piece()
                    Dim EnumerShowNow As System.Collections.IEnumerator

                    gstrProbComtStack &= " #BR3b" 

                    EnumerShowNow = m_UserPieces.Parts.GetEnumerator()
                    EnumerShowNow.MoveNext()

                    gstrProbComtStack &= " #BR3c" 
                    lRedoUserPiece = EnumerShowNow.Current

                    gstrProbComtStack &= " #BR3d" 
                    RedoUserPieceArr.Pop()
                    m_UserPieces.Parts.Add(lRedoUserPiece)
                    gstrProbComtStack &= " #BR3e" 
                Catch ex As Exception
                    Console.WriteLine("btnRedo_Click.Case SortOrderForData.eDataType.UserPieces " & ex.ToString)
                End Try
        End Select
        
        gstrProbComtStack &= " #BR4" 
        m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
        m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnRedo_Click") 

        gstrProbComtStack &= " #BR5" 
        RedoSortOrderStack.Pop() 

        gstrProbComtStack &= " #BR6" 
        ChangeUndoRedoStatus() 

        PictureBox1.Invalidate()

        gstrProbComtStack &= " #BREnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub chkMirror_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMirror.CheckedChanged

        AddDebugComment("Form1.chkMirror_CheckedChanged") 

        PictureBox1.Invalidate() 

    End Sub
    Private Sub chkGuide_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkGuide.CheckedChanged

        AddDebugComment("Form1.chkGuide_CheckedChanged") 

        PictureBox1.Invalidate() 

    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        gstrProbComtStack = "frmMain.btnClear_Click - start" 

        If mPieces.Count > 0 Or lbooSomethingDrawn = True Then
            DeactivatePaintingBeforeDialog()
            Dim dlgRes As DialogResult = MessageBox.Show("Are you sure you want to create a new mask?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            ReactivatePaintingBeforeDialog()
            If dlgRes <> DialogResult.Yes Then
                PictureBox1.Invalidate() 
                Exit Sub
            End If
        End If

        lbooSomethingDrawn = False
        
        Clear() 

        gstrProbComtStack &= " #BCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub Clear()

        m_Drawings.Clear(m_CurrentColour, m_CurrentBrushWidth) 

        mPieces = Nothing
        mPieces = New ArrayList()

        m_UserPieces = New FacePartStuctureDataFile() 
        m_SortOrderForData = New SortOrderForData() 

        m_SortOrderForData.Add(mPieces, m_Drawings.mousePath, _
            m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "btnClear_Click")

        RedoSortOrderStack.Clear() 

        ChangeUndoRedoStatus() 

        PictureBox1.Image = Nothing

        PictureBox1.Invalidate() 
    End Sub
    Private Sub btnWhite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWhite.Click

        gstrProbComtStack = "frmMain.btnWhite_Click - start" 

        lblWhite.BackColor = Color.Red
        lblBlack.BackColor = Color.White

        m_Drawings.SetColour(Color.White, m_CurrentBrushWidth)

        Me.Update()

        gstrProbComtStack &= " #BWCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnBlack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlack.Click

        gstrProbComtStack = "frmMain.btnBlack_Click - start" 

        lblWhite.BackColor = Color.White
        lblBlack.BackColor = Color.Red

        m_Drawings.SetColour(Color.Black, m_CurrentBrushWidth)

        Me.Update()

        gstrProbComtStack &= " #BBEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnHead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHead.Click

        gstrMRPs = "0350"
        
        gstrProbComtStack = "frmMain.btnHead_Click - start" 
        DeactivatePaintingBeforeDialog()
        gstrProbComtStack = " #bHc1" 

        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bHc1a" 
        fp.Owner = Me
        gstrProbComtStack = " #bHc1b" 
        fp.PartType = FacePartEnums.ePartType.Outline
        gstrProbComtStack = " #bHc1c" 

        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show()

        gstrProbComtStack = " #bHc2"
        ReactivatePaintingBeforeDialog()
        gstrProbComtStack = " #bHc3"

        gstrProbComtStack = " #bHc4"

        fp = Nothing
        gstrProbComtStack &= " #BHEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnEyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEyes.Click
        gstrMRPs = "0360"
        gstrProbComtStack = "frmMain.btnEyes_Click - start" 

        DeactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bEyc1"
        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bEyc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bEyc1b"
        fp.PartType = FacePartEnums.ePartType.Eye 
        gstrProbComtStack = " #bEyc1c"
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show()
        gstrProbComtStack = " #bEyc2"
        ReactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bEyc3"
        gstrProbComtStack = " #bEyc4"
        fp = Nothing 
        gstrProbComtStack &= " #BEYCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnEars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEars.Click
        gstrMRPs = "0370"
        gstrProbComtStack = "frmMain.btnEars_Click - start" 
        DeactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bEac1"
        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bEac1a"
        fp.Owner = Me
        gstrProbComtStack = " #bEac1b"
        fp.PartType = FacePartEnums.ePartType.Ear 
        gstrProbComtStack = " #bEac1c"
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show()
        gstrProbComtStack = " #bEac2"
        ReactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bEac3"
        gstrProbComtStack = " #bEac4"
        fp = Nothing 
        gstrProbComtStack &= " #BEACEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnMouths_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMouths.Click
        gstrMRPs = "0380"
        gstrProbComtStack = "frmMain.btnMouths_Click - start" 

        DeactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bMoc1"
        Dim fp As New FacePartDiag() 'FacePartSel()
        gstrProbComtStack = " #bMoc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bMoc1b"
        fp.PartType = FacePartEnums.ePartType.Mouth 
        gstrProbComtStack = " #bMoc1c"
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show() 'Dialog()
        gstrProbComtStack = " #bMoc2"
        ReactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bMoc3"
        gstrProbComtStack = " #bMoc4"
        fp = Nothing 

        gstrProbComtStack &= " #BMCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnNoses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoses.Click
        gstrMRPs = "0390"
        gstrProbComtStack = "frmMain.btnNoses_Click - start" 

        DeactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bNoc1"
        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bNoc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bNoc1b"
        fp.PartType = FacePartEnums.ePartType.Nose 
        gstrProbComtStack = " #bNoc1c"
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show()
        gstrProbComtStack = " #bNoc2"
        ReactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bNoc3"
        gstrProbComtStack = " #bNoc4"
        fp = Nothing 
        gstrProbComtStack &= " #BNCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOther.Click
        gstrMRPs = "0400"
        gstrProbComtStack = "frmMain.btnOther_Click - start" 

        DeactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bOtc1"
        Dim fp As New FacePartDiag()
        gstrProbComtStack = " #bOtc1a"
        fp.Owner = Me
        gstrProbComtStack = " #bOtc1b"
        fp.PartType = FacePartEnums.ePartType.Misc
        gstrProbComtStack = " #bOtc1c"
        fp.mPieces = mPieces
        fp.mDrawings = m_Drawings
        fp.mUserPieces = m_UserPieces
        fp.mSortOrderForData = m_SortOrderForData
        fp.Show()
        gstrProbComtStack = " #bOtc2"
        ReactivatePaintingBeforeDialog() 
        gstrProbComtStack = " #bOtc3"
        gstrProbComtStack = " #bOtc4"
        fp = Nothing 
        gstrProbComtStack &= " #BOCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        gstrMRPs = "0410"
        gstrProbComtStack = "frmMain.btnLoad_Click - start" 
        
        Dim LoadSlots As New Slots()
        With LoadSlots
            .Owner = Me 
            .TranType = Slots.eTranType.Load
            .SelectedUser = mSelectedUser
            DeactivatePaintingBeforeDialog() 
            .LicensedFaceParts = mLicensedFaceParts 
            .UserPieces = m_UserPieces 
            .SortOrderForData = m_SortOrderForData 
            .ShowDialog()

            Application.DoEvents() 
            If .MaskToLoad <> "" Then 
                Busy(Me, True) 
                'btnClear_Click(Nothing, Nothing) 
                Clear() 
                'LoadMask(.MaskToLoad, mPieces, PictureBox2.Image, False) 
                'LoadMask(.MaskToLoad, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                '   m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts) 
                
                LoadMask(.MaskToLoad, mPieces, Nothing, False, m_Drawings.mousePath, m_Drawings.ReversemousePath, _
                    m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, mLicensedFaceParts, m_UserPieces, m_SortOrderForData)

                Busy(Me, False) 
            End If
            m_Drawings.setCountersAfterLoad(m_CurrentColour, m_CurrentBrushWidth)

            ChangeUndoRedoStatus() 

            ReactivatePaintingBeforeDialog() 
        End With

        gstrProbComtStack &= " #BLCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        gstrMRPs = "0420"
        AddDebugComment("Form1.btnSave_Click") 

        KidsSave(False) 

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("frmMain.btnHelp_Click") 

        
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.MainScreen))

    End Sub
    Private Sub mnuHelpBuyPacks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpBuyPacks.Click
        gstrMRPs = "0430"
        AddDebugComment("Form1.mnuHelpBuyPacks_Click") 

        DeactivatePaintingBeforeDialog()

        Dim DataFilesDescImages As New ArrayList()
        Dim DataFilesDescriptions As New ArrayList()
        Dim DataFilesProdNums As New ArrayList()
        Dim DataFiles As New ArrayList()
        Dim DataFileState As New ArrayList()

        If Not mLicensedFaceParts Is Nothing Then mLicensedFaceParts.Clear() 

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

        
        Dim X As Short = (Panel1.Width - PictureBox1.Width) / 2
        Dim Y As Short = (Panel1.Height - PictureBox1.Height) / 2
        PictureBox1.Location = New Point(X, Y)

    End Sub
    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

        AddDebugComment("Form1.PictureBox1_Click") 

        
        If CurrentTool = Tools.Floodfill Then 

            Busy(Me, True) 
            PictureBox1.Enabled = False 


            Dim FullImage As Image = DrawDetails(PictureBox1, m_Drawings.mousePath, m_Drawings.ReversemousePath, mPieces, _
                m_Drawings.lPaintBrush, m_Drawings.lPaintReverseBrush, m_UserPieces, m_SortOrderForData)
            ' Dim Before As New Bitmap(FullImage)
            'Dim l_ActiveBitmap As New Bitmap(FullImage)

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
                m_Drawings.ReversemousePath, m_UserPieces, m_SortOrderForData, "PictureBox1_Click") 

            ChangeUndoRedoStatus() 

            FloodFillJustOccured = True 

            PictureBox1.Enabled = True 
            Busy(Me, False) 

            Me.Invalidate()

        End If 
    End Sub
    Private Sub btnPalette_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPBlack.Click, btnPWhite.Click, _
        btnPRed.Click, btnPOrange.Click, btnPYellow.Click, btnPBrown.Click, btnPGreen.Click, btnPLime.Click, btnPCyan.Click, _
        btnPLightBlue.Click, btnPBlue.Click, btnPMagenta.Click, btnPPink.Click, btnPCustom.Click

        AddDebugComment("frmMain.btnPalette_Click - start") 

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
            Case "btnPCustom" : SetPaletteLabel(lblPCustom, sender) 
        End Select

        m_Drawings.SetColour(sender.BackColor, m_CurrentBrushWidth)
        m_CurrentColour = sender.BackColor 

        gstrProbComtStack &= " #BPCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub SetPaletteLabel(ByVal pobjLabel As Label, ByVal pobButton As Button)
        
        pobjLabel.BorderStyle = BorderStyle.FixedSingle
        pobjLabel.BackColor = Color.Red
        pobjLabel.Tag = "Selected"
        pobjLabel.BringToFront()
        pobButton.BringToFront()

    End Sub
    Private Sub rdoFreehand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoFreehand.Click

        AddDebugComment("Form1.rdoFreehand_Click") 

        
        If rdoFloodFill.Checked = True Then
            CurrentTool = Tools.Floodfill
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

        AddDebugComment("Form1.rdoFloodFill_Click") 

        
        If rdoFloodFill.Checked = True Then
            CurrentTool = Tools.Floodfill
            ''PictureBox1.Cursor = ShowFloodFillCursor()  
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

        AddDebugComment("Form1.lstBrushWidth_SelectedIndexChanged") 

        

        'removed Windings 2 support 
        Select Case lstBrushWidth.Text
            Case "1" : m_CurrentBrushWidth = 1
            Case "2" : m_CurrentBrushWidth = 2
            Case "4" : m_CurrentBrushWidth = 4
            Case "8" : m_CurrentBrushWidth = 8
        End Select

        m_Drawings.SetColour(m_CurrentColour, m_CurrentBrushWidth) 

    End Sub
    Friend Sub ChangeUndoRedoStatus()
        

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

        Dim TempRedoStack As Boolean = True 
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

        AddDebugComment("Form1.btnMoreColours_Click") 

        

        Application.DoEvents()
        Dim dlgRes As DialogResult
        Dim CD As New ColorDialog()
        With CD
            .Color = btnPCustom.BackColor
            Application.DoEvents()
            .CustomColors = lintCustomColours 
            dlgRes = .ShowDialog()
            If dlgRes <> DialogResult.OK Then
                Exit Sub
            End If
            lintCustomColours = .CustomColors 
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

        btnPalette_Click(btnPCustom, Nothing) 

    End Sub
    Private Sub mnuToolsOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsOptions.Click
        

        AddDebugComment("frmMain.mnuToolsOptions_Click - start")

        DeactivatePaintingBeforeDialog()
        Dim dlgRes As DialogResult
        Dim Ops As New options()
        Ops.LoginInAs = mSelectedUser 
        Ops.Owner = Me
        dlgRes = Ops.ShowDialog()

        If dlgRes = DialogResult.OK Then
            SetDrawingLayout(mSelectedUser)
        End If

        ReactivatePaintingBeforeDialog()

        gstrProbComtStack &= " #TOCEnd" : AddDebugComment(gstrProbComtStack) : gstrProbComtStack = "" 
    End Sub
    Private Sub SetDrawingLayout(ByVal UserName As String)
        
        Dim AppSettings As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.UserSettings, UserName) 
        Dim lbooFloodfill As Boolean = CBool(AppSettings.GetValue("FloodFillAndPalette", "False")) 
        Dim lbooNoFloodfill As Boolean = CBool(AppSettings.GetValue("LineAndColour", "False")) 
        Dim lbooBlackLines As Boolean = CBool(AppSettings.GetValue("BlackLines", "True")) 
        Dim lbooBrushWidths As Boolean = CBool(AppSettings.GetValue("BrushWidths", "False")) 

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

                Try
            Dim lstrCustomColours() As String
            'lstrCustomColours = Microsoft.VisualBasic.Split(GetSetting("CustomColours", False, InitalXMLConfig.XmlConfigType.UserSettings, UserName), "#")
            lstrCustomColours = Microsoft.VisualBasic.Split(AppSettings.GetValue("CustomColours", False), "#") 

            ReDim lintCustomColours(lstrCustomColours.GetUpperBound(0))
            Dim lintArrInc As Integer
            For lintArrInc = 0 To lstrCustomColours.GetUpperBound(0)
                lintCustomColours(lintArrInc) = CInt(lstrCustomColours(lintArrInc))
            Next lintArrInc
        Catch
        End Try

        Try
            'btnPCustom.BackColor = StringToColor(GetSetting("LastCustomColour", ColorToString(Color.DarkTurquoise), InitalXMLConfig.XmlConfigType.UserSettings, UserName))
            btnPCustom.BackColor = StringToColor(AppSettings.GetValue("LastCustomColour", ColorToString(Color.DarkTurquoise))) 
        Catch
        End Try
        
    End Sub
    Private Sub lstBrushWidth_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lstBrushWidth.DrawItem

                If boolstBrushWidth_DrawItemFirstDone = False Then
            AddDebugComment("Form1.lstBrushWidth_DrawItem")
            boolstBrushWidth_DrawItemFirstDone = True
        End If
        
        
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

        AddDebugComment("Form1.mnuHelpReportProblem_Click") 

        

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

        AddDebugComment("Form1.btnBuy_Click") 
        BrowseToUrl("http://www.example.com/buy.php", Me)
    End Sub


    Private Sub rdoFloodFill_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoFloodFill.CheckedChanged

    End Sub
End Class

