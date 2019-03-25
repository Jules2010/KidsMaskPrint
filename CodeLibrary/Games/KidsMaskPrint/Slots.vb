Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Friend Class Slots
    Inherits System.Windows.Forms.Form
    '--- 'JM 18/08/2004 ---
#Region "Friend Properties"
    Friend Enum eTranType
        Save
        Load
    End Enum
    Dim mTranType As eTranType
    Friend Property TranType() As eTranType
        Get
            Return mTranType
        End Get
        Set(ByVal Value As eTranType)
            mTranType = Value
        End Set
    End Property
    Dim mSelectedUser As String
    Friend Property SelectedUser() As String
        Get
            Return mSelectedUser
        End Get
        Set(ByVal Value As String)
            mSelectedUser = Value
        End Set
    End Property
    Dim mFaceHash As SortedList
    Friend Property FaceHash() As SortedList
        Get
            Return mFaceHash
        End Get
        Set(ByVal Value As SortedList)
            mFaceHash = Value
        End Set
    End Property
    'Dim mFaceImage As Image
    'Public Property FaceImage() As Image
    '    Get
    '        Return mFaceImage
    '    End Get
    '    Set(ByVal Value As Image)
    '        mFaceImage = Value
    '    End Set
    'End Property
    Dim mFullImage As Image
    Friend Property FullImage() As Image
        Get
            Return mFullImage
        End Get
        Set(ByVal Value As Image)
            mFullImage = Value
        End Set
    End Property
    Dim mbooLastMask As Boolean
    Friend Property LastMask() As Boolean
        Get
            Return mbooLastMask
        End Get
        Set(ByVal Value As Boolean)
            mbooLastMask = Value
        End Set
    End Property
    Dim mMaskToLoad As String
    Friend Property MaskToLoad() As String
        Get
            Return mMaskToLoad
        End Get
        Set(ByVal Value As String)
            mMaskToLoad = Value
        End Set
    End Property

    '--- 'JM 18/08/2004 ---
    Dim mLicensedFaceParts As New ArrayList() 'JM 19/09/2004
    Friend Property LicensedFaceParts() As ArrayList
        Get
            Return mLicensedFaceParts
        End Get
        Set(ByVal Value As ArrayList)
            mLicensedFaceParts = Value
        End Set
    End Property

    Dim lUserPieces As New FacePartStuctureDataFile() 'JM 13/10/2004
    Friend Property UserPieces() As FacePartStuctureDataFile 'JM 13/10/2004
        Get
            Return lUserPieces
        End Get
        Set(ByVal Value As FacePartStuctureDataFile)
            lUserPieces = Value
        End Set
    End Property
    Dim lSortOrderForData As New SortOrderForData() 'JM 14/10/2004
    Friend Property SortOrderForData() As SortOrderForData 'JM 14/10/2004
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
    Friend WithEvents lblTran As System.Windows.Forms.Label
    Friend WithEvents btnCancel As WinOnly.BevelButton
    Friend WithEvents btnHelp As WinOnly.BevelButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblTran = New System.Windows.Forms.Label()
        Me.btnCancel = New WinOnly.BevelButton()
        Me.btnHelp = New WinOnly.BevelButton()
        Me.SuspendLayout()
        '
        'lblTran
        '
        Me.lblTran.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTran.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTran.Location = New System.Drawing.Point(450, 8)
        Me.lblTran.Name = "lblTran"
        Me.lblTran.Size = New System.Drawing.Size(80, 554)
        Me.lblTran.TabIndex = 0
        Me.lblTran.Text = "Label1"
        Me.lblTran.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.Gold
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(448, 512)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 40)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnHelp.BackColor = System.Drawing.Color.Red
        Me.btnHelp.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHelp.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnHelp.ForeColor = System.Drawing.Color.Gold
        Me.btnHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnHelp.Location = New System.Drawing.Point(448, 464)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(88, 40)
        Me.btnHelp.TabIndex = 0
        Me.btnHelp.Text = "&Help"
        '
        'Slots
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(546, 568)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnHelp, Me.btnCancel, Me.lblTran})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "Slots"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Slots"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim PreviousSender As Object

    Dim evOnClick As New EventHandler(AddressOf NameBtnOnClick)
    Dim evOnMouseEnter As New EventHandler(AddressOf NameBtnOnMouseEnter)
    Dim evOnMouseLeave As New EventHandler(AddressOf NameBtnOnMouseLeave)
    Dim lbooButtonSelected As Boolean = False

    Dim mDir As String = Path.GetDirectoryName( _
        System.Reflection.Assembly.GetExecutingAssembly().Location) & "\Masks\" 'JM 20/08/2004

    Private Sub Slots_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("Slots.Slots_Load - start") 'JM 07/09/2004

        Busy(Me, True) 'JM 21/09/2004

        Me.Text = NameMe("Slots") 'JM 24/09/2004

        Dim UserSettings As New InitalXMLConfig(InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
        Dim lintArrInc As Integer

        Dim BevelButton(5) As WinOnly.BevelButton

        'iterate through user dat section, and load preview and text for each of there masks and add to slot button.

        Dim ThisTop As Integer
        For lintArrInc = 0 To BevelButton.GetUpperBound(0)
            Dim ThisFile As String = UserSettings.GetValue("MaskFile" & lintArrInc + 1, "")
            Dim ThisFileDesc As String = UserSettings.GetValue("MaskFileDesc" & lintArrInc + 1, "Empty")

            BevelButton(lintArrInc) = New BevelButton()
            'Dim ThisItem() As String = Microsoft.VisualBasic.Split(UserMaskFile(lintArrInc), "#")
            With BevelButton(lintArrInc)
                .BackColor = System.Drawing.Color.AliceBlue
                .FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
                .Location = New System.Drawing.Point(16, 16 + ThisTop)
                ThisTop += 90
                .Size = New System.Drawing.Size(400, 90) '248, 90)
                .TabIndex = lintArrInc + 1
                .Tag = lintArrInc + 1
                .ImageAlign = ContentAlignment.MiddleRight
                .Font = New Font("Arial", 16, FontStyle.Bold) 'JM 21/08/2004
                '--- 'JM 20/08/2004 ---
                .Text = ThisFileDesc '"Empty"
                If IO.File.Exists(ThisFile) = True Then
                    Dim Temp As Image
                    'LoadMask(ThisFile, Nothing, Temp, True)
                    'LoadMask(ThisFile, Nothing, Temp, True, Nothing, Nothing, Nothing, Nothing, mLicensedFaceParts) 'JM 27/08/2004
                    LoadMask(ThisFile, Nothing, Temp, True, Nothing, Nothing, Nothing, Nothing, mLicensedFaceParts, Nothing, Nothing) 'JM 13/10/2004

                    .Image = Temp
                End If
                '--- 'JM 20/08/2004 ---
                .TextAlign = System.Drawing.ContentAlignment.MiddleLeft
                .TurnOffRoundedRect = True
                .BackColor = Color.FromArgb(0, .BackColor)
                AddHandler BevelButton(lintArrInc).Click, evOnClick
                AddHandler BevelButton(lintArrInc).MouseEnter, evOnMouseEnter
                AddHandler BevelButton(lintArrInc).MouseLeave, evOnMouseLeave
                Me.Controls.Add(BevelButton(lintArrInc))
            End With
        Next lintArrInc

        lblTran.BackColor = Color.FromArgb(0, lblTran.BackColor)

        If mTranType = eTranType.Load Then
            lblTran.Text = "Load Mask"
        ElseIf mTranType = eTranType.Save Then
            lblTran.Text = "Save Mask"
        End If

        Busy(Me, False) 'JM 21/09/2004

        AddDebugComment("Slots.Slots_Load - end") 'JM 07/09/2004

    End Sub
    Private Sub NameBtnOnClick(ByVal sender As Object, ByVal e As EventArgs)

        AddDebugComment("Slots.NameBtnOnClick - start") 'JM 07/09/2004

        lbooButtonSelected = True

        If mTranType = eTranType.Load Then
            '--- 'JM 21/08/2004 ---
            mMaskToLoad = GetSetting("MaskFile" & sender.tag, "", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            Me.Close()
            '--- 'JM 21/08/2004 ---
        ElseIf mTranType = eTranType.Save Then
            Dim QB As New QuestionBox()
            Dim SlotName As String = QB.Display("What's this mask called?", NameMe("Save Mask"), mSelectedUser & " Mask " & sender.tag)

            If SlotName = "" Then Exit Sub
            sender.text = SlotName

            '--- 'JM 07/09/2004 Produce Overwrite message where applicable ---
            Dim dlgRes As DialogResult
            If GetSetting("MaskFile" & sender.tag, "", InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser) <> "" Then
                dlgRes = MessageBox.Show("Overwrite?", NameMe(""), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                Application.DoEvents() 'JM 22/11/2004
                If dlgRes <> DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            '--- 'JM 07/09/2004 Produce Overwrite message where applicable ---

            Busy(Me, True) 'JM 21/09/2004

            Dim FileName As String = mDir & MakeFileNameNice(SlotName, "Mask")

            'SaveUserMask(FileName, FaceHash, mFullImage)  'JM 20/08/2004
            SaveUserMask(FileName, FaceHash, mFullImage, lUserPieces, lSortOrderForData) 'JM 13/10/2004

            SaveSetting("MaskFile" & sender.tag, FileName, InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            SaveSetting("MaskFileDesc" & sender.tag, SlotName, InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser)
            If mbooLastMask = True Then 'JM 21/08/2004
                SaveSetting("LastSaved", FileName, InitalXMLConfig.XmlConfigType.UserSettings, mSelectedUser) 'JM 21/08/2004
            End If 'JM 21/08/2004
            Busy(Me, False) 'JM 21/09/2004

            Me.Close()
        End If

        AddDebugComment("Slots.NameBtnOnClick - end") 'JM 07/09/2004

    End Sub
    Private Sub NameBtnOnMouseEnter(ByVal sender As Object, ByVal e As EventArgs)

        If lbooButtonSelected = False Then
            If Not PreviousSender Is Nothing Then
                If Not PreviousSender Is sender Then
                    PreviousSender.backcolor = Color.AliceBlue
                    PreviousSender.forecolor = Color.Black
                End If
            End If

            sender.backcolor = Color.Red
            sender.forecolor = Color.Gold
        End If

    End Sub
    Private Sub NameBtnOnMouseLeave(ByVal sender As Object, ByVal e As EventArgs)

        If lbooButtonSelected = False Then
            PreviousSender = sender
        End If

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        'added 'JM 13/08/2004
        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        AddDebugComment("Slots.btnCancel_Click") 'JM 07/09/2004

        Me.Close()

    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("Slots.btnHelp_Click") 'JM 07/09/2004

        'JM 25/08/2004
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.Slots))

    End Sub

    Private Sub Slots_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 'JM 21/09/2004
    End Sub

    Private Sub Slots_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'JM 24/09/2004
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
End Class
