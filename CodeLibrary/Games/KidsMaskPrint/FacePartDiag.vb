Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Friend Class FacePartDiag
    Inherits System.Windows.Forms.Form
#Region "Friend Properties"
    Dim mRetPart As Part 
    Friend Property RetPart() As Part
        Get
            Return mRetPart
        End Get
        Set(ByVal Value As Part)
            mRetPart = Value
        End Set
    End Property
    Dim m_PartType As FacePartEnums.ePartType 
    Friend Property PartType() As FacePartEnums.ePartType 
        Get
            Return m_PartType
        End Get
        Set(ByVal Value As FacePartEnums.ePartType)
            m_PartType = Value
        End Set
    End Property
    Dim mPositionSelection As FacePartEnums.ePositionSelection
    Friend Property RetPosSel() As FacePartEnums.ePositionSelection
        Get
            Return mPositionSelection
        End Get
        Set(ByVal Value As FacePartEnums.ePositionSelection)
            mPositionSelection = Value
        End Set
    End Property
    Dim m_SourceDataFileName As String
    Friend Property SourceDataFileName() As String
        Get
            Return m_SourceDataFileName
        End Get
        Set(ByVal Value As String)
            m_SourceDataFileName = Value
        End Set
    End Property
    Dim m_DataFileItemNum As Integer
    Friend Property DataFileItemNum() As Integer
        Get
            Return m_DataFileItemNum
        End Get
        Set(ByVal Value As Integer)
            m_DataFileItemNum = Value
        End Set
    End Property
    Dim m_PieceName As String
    Friend Property PieceName() As String
        Get
            Return m_PieceName
        End Get
        Set(ByVal Value As String)
            m_PieceName = Value
        End Set
    End Property
    Dim mm_Pieces As New ArrayList()
    Friend Property mPieces() As ArrayList
        Get
            Return mm_Pieces
        End Get
        Set(ByVal Value As ArrayList)
            mm_Pieces = Value
        End Set
    End Property
    Dim mm_Drawings As Drawings
    Friend Property mDrawings() As Drawings
        Get
            Return mm_Drawings
        End Get
        Set(ByVal Value As Drawings)
            mm_Drawings = Value
        End Set
    End Property
    Dim mm_UserPieces As FacePartStuctureDataFile
    Friend Property mUserPieces() As FacePartStuctureDataFile
        Get
            Return mm_UserPieces
        End Get
        Set(ByVal Value As FacePartStuctureDataFile)
            mm_UserPieces = Value
        End Set
    End Property
    Dim mm_SortOrderForData As SortOrderForData
    Friend Property mSortOrderForData() As SortOrderForData
        Get
            Return mm_SortOrderForData
        End Get
        Set(ByVal Value As SortOrderForData)
            mm_SortOrderForData = Value
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
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents btnClose As WinOnly.BevelButton
    Friend WithEvents btnSelect As WinOnly.BevelButton
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents rdoLeft As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoRight As System.Windows.Forms.RadioButton
    Friend WithEvents btnHelp As WinOnly.BevelButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnClose = New WinOnly.BevelButton()
        Me.btnSelect = New WinOnly.BevelButton()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.rdoLeft = New System.Windows.Forms.RadioButton()
        Me.rdoBoth = New System.Windows.Forms.RadioButton()
        Me.rdoRight = New System.Windows.Forms.RadioButton()
        Me.btnHelp = New WinOnly.BevelButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.ListView1.Location = New System.Drawing.Point(16, 16)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(400, 224)
        Me.ListView1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnClose.BackColor = System.Drawing.Color.Red
        Me.btnClose.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnClose.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.Gold
        Me.btnClose.Location = New System.Drawing.Point(326, 352)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 40)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnSelect.BackColor = System.Drawing.Color.Red
        Me.btnSelect.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnSelect.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.ForeColor = System.Drawing.Color.Gold
        Me.btnSelect.Location = New System.Drawing.Point(230, 352)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(88, 40)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "&Select"
        '
        'picPreview
        '
        Me.picPreview.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.picPreview.BackColor = System.Drawing.SystemColors.Window
        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPreview.Location = New System.Drawing.Point(16, 256)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(176, 88)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picPreview.TabIndex = 3
        Me.picPreview.TabStop = False
        '
        'rdoLeft
        '
        Me.rdoLeft.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.rdoLeft.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdoLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoLeft.Location = New System.Drawing.Point(230, 256)
        Me.rdoLeft.Name = "rdoLeft"
        Me.rdoLeft.Size = New System.Drawing.Size(56, 32)
        Me.rdoLeft.TabIndex = 4
        Me.rdoLeft.Text = "Left"
        Me.rdoLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoBoth
        '
        Me.rdoBoth.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.rdoBoth.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdoBoth.Checked = True
        Me.rdoBoth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoBoth.Location = New System.Drawing.Point(294, 256)
        Me.rdoBoth.Name = "rdoBoth"
        Me.rdoBoth.Size = New System.Drawing.Size(56, 32)
        Me.rdoBoth.TabIndex = 5
        Me.rdoBoth.TabStop = True
        Me.rdoBoth.Text = "Both"
        Me.rdoBoth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoRight
        '
        Me.rdoRight.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.rdoRight.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdoRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoRight.Location = New System.Drawing.Point(358, 256)
        Me.rdoRight.Name = "rdoRight"
        Me.rdoRight.Size = New System.Drawing.Size(56, 32)
        Me.rdoRight.TabIndex = 6
        Me.rdoRight.Text = "Right"
        Me.rdoRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnHelp
        '
        Me.btnHelp.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnHelp.BackColor = System.Drawing.Color.Red
        Me.btnHelp.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnHelp.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHelp.ForeColor = System.Drawing.Color.Gold
        Me.btnHelp.Location = New System.Drawing.Point(16, 352)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(88, 40)
        Me.btnHelp.TabIndex = 3
        Me.btnHelp.Text = "&Help"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(32, 32)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'FacePartDiag
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(430, 406)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnHelp, Me.rdoRight, Me.rdoBoth, Me.rdoLeft, Me.picPreview, Me.btnSelect, Me.btnClose, Me.ListView1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "FacePartDiag"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FaceParts"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim Dir As String = Path.GetDirectoryName( _
        System.Reflection.Assembly.GetExecutingAssembly().Location) & "\FaceParts\"
    Private Structure ThreeImages 
        Dim LeftImg As Image
        Dim BothImg As Image
        Dim RightImg As Image
        Dim KMPPart As Part
        Dim PieceName As String
    End Structure
    Dim mTempImages() As ThreeImages 
    Dim DoActivatedCodeOnce As Boolean = True 
    Private Sub FaceParts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("FacePartDiag.FaceParts_Load - start") 

        Busy(Me, True) 

        Dim Caption As String
        Select Case m_PartType
            Case FacePartEnums.ePartType.Ear : Caption = "Ear"
            Case FacePartEnums.ePartType.Eye : Caption = "Eye"
            Case FacePartEnums.ePartType.Misc : Caption = "Other"
            Case FacePartEnums.ePartType.Outline : Caption = "Head"
            Case FacePartEnums.ePartType.Mouth : Caption = "Mouth"
            Case FacePartEnums.ePartType.Nose : Caption = "Nose"
        End Select

        Me.Text = NameMe(Caption & " Parts")

        SetBackcolors() 

        ImageList1.ImageSize = New Size(32, 32)

        ListView1.HideSelection = False 
        ListView1.LargeImageList = ImageList1
        ListView1.Items.Clear()

        LoadFaceParts() 

        If ListView1.Items.Count > 0 Then
            ReDim mTempImages(ListView1.Items.Count) 
            ListView1.Items(0).Selected = True
            ListView1_Click(Nothing, Nothing)
        End If

        Busy(Me, False) 


        AddDebugComment("FacePartDiag.FaceParts_Load - end") 

    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        AddDebugComment("FacePartDiag.btnClose_Click - start") 

        mRetPart = Nothing
        m_PieceName = "" 

        Me.Close()

        AddDebugComment("FacePartDiag.btnClose_Click - end") 

    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        AddDebugComment("FacePartDiag.btnSelect_Click - start") 

        If ListView1.SelectedItems.Count <> 1 Then
            Exit Sub
        End If

        Me.Close()

        AddDebugComment("FacePartDiag.btnSelect_Click - end") 

    End Sub
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click

        AddDebugComment("FacePartDiag.ListView1_Click - start") 

        Busy(Me, True) 

        DisplayPreview()

        m_SourceDataFileName = ReturnNthStr(ListView1.SelectedItems(0).Tag, 1, "#")
        m_DataFileItemNum = ReturnNthStr(ListView1.SelectedItems(0).Tag, 2, "#")

        Busy(Me, False) 

        AddDebugComment("FacePartDiag.ListView1_Click - end") 

    End Sub
    Private Sub DisplayPreview(Optional ByVal RadioUse As Boolean = False)

        AddDebugComment("FacePartDiag.DisplayPreview - start") 

        Dim lImage As System.Drawing.Image

        Try
            With mTempImages(ListView1.SelectedItems(0).Index)

            End With
        Catch
            rdoLeft.Visible = False
            rdoBoth.Visible = False
            rdoRight.Visible = False
            Exit Sub
        End Try

        With mTempImages(ListView1.SelectedItems(0).Index) 
            If mTempImages(ListView1.SelectedItems(0).Index).LeftImg Is Nothing Then 
                
                GetDataPreviewImage(ReturnNthStr( _
                    ListView1.SelectedItems(0).Tag, 1, "#"), ReturnNthStr(ListView1.SelectedItems(0).Tag, 2, "#"), mRetPart, m_PieceName, _
                    .LeftImg, .BothImg, .RightImg)
                
                AddDebugComment("FacePartDiag.DisplayPreview - 1") 
                .KMPPart = mRetPart
                .PieceName = m_PieceName
            End If 

            m_PieceName = .PieceName 
            mRetPart = .KMPPart 

            'lImage = PreviewImage 'mRetPart.FullImage

            If RadioUse = False Then 
                If .KMPPart.BothParts = False Then
                    rdoLeft.Visible = False
                    rdoBoth.Visible = False
                    rdoRight.Visible = False
                    rdoLeft.Checked = True
                Else
                    rdoLeft.Visible = True
                    rdoBoth.Visible = True
                    rdoRight.Visible = True
                    rdoBoth.Checked = True
                    mPositionSelection = FacePartEnums.ePositionSelection.Both
                End If
            Else
                rdoLeft.Visible = True
                rdoBoth.Visible = True
                rdoRight.Visible = True
            End If

            If rdoRight.Checked = True Then
                lImage = .RightImg 
                'lImage.RotateFlip(RotateFlipType.RotateNoneFlipX)
            End If
            If rdoBoth.Checked = True Then
                lImage = .BothImg 
            End If

            If lImage Is Nothing Then 
                lImage = .LeftImg 
            End If
            AddDebugComment("FacePartDiag.DisplayPreview - 2") 
            lImage = ResizeImageObj(lImage, picPreview.Height - 10)

        End With 

        picPreview.Image = lImage

        AddDebugComment("FacePartDiag.DisplayPreview - end") 

    End Sub

    Private Sub rdoPos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoLeft.Click, rdoBoth.Click, rdoRight.Click

        AddDebugComment("FacePartDiag.rdoPos_Click - start") 

        Busy(Me, True) 

        DisplayPreview(True) 

        Busy(Me, False) 

        AddDebugComment("FacePartDiag.rdoPos_Click - end " & mPositionSelection) 

    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        AddDebugComment("FacePartDiag.ListView1_DoubleClick") 

        btnSelect_Click(Nothing, Nothing)

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click

        AddDebugComment("FacePartDiag.btnHelp_Click") 

        
        Help.ShowHelp(Me, GetHelpFile, GetHelpTopic(Main.HelpTopicEnum.FacePartSelect))

    End Sub
    Private Sub SetBackcolors()

        AddDebugComment("FacePartDiag.SetBackcolors") 

        
        rdoLeft.BackColor = Color.FromArgb(0, rdoLeft.BackColor)
        rdoBoth.BackColor = Color.FromArgb(0, rdoBoth.BackColor)
        rdoRight.BackColor = Color.FromArgb(0, rdoRight.BackColor)

    End Sub
    Private Sub LoadFaceParts()

        AddDebugComment("FacePartDiag.LoadFaceParts - start") 

        Busy(Me, True) 

        '-------------- Check License if available -------------
        Dim Dets2 As strat1.UnlockDetails
        Dim Info As New System.IO.FileInfo(System.IO.Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl")

        If Info.Exists Then
            Try
                Unlock(System.IO.Path.GetDirectoryName( _
                    System.Reflection.Assembly.GetExecutingAssembly.Location.ToString()) & "\keyfile.mcl", Dets2, "", "")
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
            Try
                Dim FPs As FacePartStuctureDataFile = UnlockFacePartsPack(pfile.FullName) 

                '--- this block checks for a valid key file and doesn't all it to be used if it isn't ---
                If pfile.Name.ToLower <> "basic.dat" Then 
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
                            Throw New Exception(" ")
                        End If
                    Else
                        Throw New Exception(" ")
                    End If
                End If 

                Dim lintArrInc As Integer
                For lintArrInc = 0 To FPs.Parts.Count  '0 To FPs.Parts.Count - 1
                    Dim ThisPart As New KidsMaskPrint.Part()
                    ThisPart = FPs.Parts(lintArrInc)

                    Dim lAdd As Boolean = False

                    If ThisPart.PartType = m_PartType Then
                        lAdd = True
                    End If

                    If lAdd = True Then

                        ImageList1.Images.Add(ThisPart.ThumbImage)

                        Dim NewLVItem As New ListViewItem()
                        NewLVItem.Tag = pfile.Name & "# " & lintArrInc & "#"
                        NewLVItem.Text = ThisPart.FaceMaster
                        NewLVItem.ImageIndex = ImageList1.Images.Count - 1

                        ListView1.Items.Add(NewLVItem)
                        NewLVItem = Nothing

                    End If
                Next lintArrInc
            Catch ex As Exception
                'MessageBox.Show(ex.ToString)
            End Try
        Next pfile

        Busy(Me, False) 

        AddDebugComment("FacePartDiag.LoadFaceParts - end") 

    End Sub
    Private Sub FacePartDiag_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub
    Private Sub rdo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoLeft.CheckedChanged, rdoBoth.CheckedChanged, rdoRight.CheckedChanged
        
        If rdoLeft.Checked = True Then
            mPositionSelection = FacePartEnums.ePositionSelection.Left
        ElseIf rdoRight.Checked = True Then
            mPositionSelection = FacePartEnums.ePositionSelection.Right
        Else
            mPositionSelection = FacePartEnums.ePositionSelection.Both
        End If

    End Sub

    Private Sub FacePartDiag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        
        If e.KeyCode = Keys.Escape Then
            btnClose_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub FacePartDiag_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        If DoActivatedCodeOnce = True Then
            DoActivatedCodeOnce = False
            Dim ShowBuyMore As Boolean = CBool(GetSetting("BuyMore", "True", InitalXMLConfig.XmlConfigType.AppSettings, ""))

            If ShowBuyMore = True Then
                Dim BM As New CanBuyPacks()
                BM.Owner = Me
                BM.ShowDialog()

            End If
        End If

    End Sub

    Private Sub AddSelectedFacePart(ByVal pFP As Part, ByVal pSel As FacePartEnums.ePositionSelection, _
        ByVal SourceDatFileName As String, ByVal DataFileItemNum As Integer, ByVal pobjForm As frmMain)

        AddDebugComment("FacePartDiag.AddSelectedFacePart - start") 

        Busy(pobjForm, True) 

        If Not pFP Is Nothing Then

            Select Case pSel
                Case FacePartEnums.ePositionSelection.Left
                    Dim ThisPiece As New Piece()
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.LeftPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 
                    mm_Pieces.Add(ThisPiece)
                Case FacePartEnums.ePositionSelection.Both

                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = False
                    thispiece.VertFlip = False
                    ThisPiece.SetImageObj(pFP.FullImage.Clone)
                    ThisPiece.Location = pFP.LeftPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 
                    mm_Pieces.Add(ThisPiece)

                    Dim ThisPiece2 As New Piece()
                    ThisPiece2.HorizFlip = True
                    ThisPiece2.VertFlip = False
                    ThisPiece2.SetImageObj(pFP.FullImage)
                    ThisPiece2.Location = pFP.RightPart
                    ThisPiece2.PieceName = pFP.FaceMaster 
                    ThisPiece2.SourceDataFileName = SourceDatFileName 
                    ThisPiece2.DataFileItemNum = DataFileItemNum 
                    mm_Pieces.Add(ThisPiece2)

                Case FacePartEnums.ePositionSelection.Right
                    Dim ThisPiece As New Piece()
                    ThisPiece.HorizFlip = True
                    ThisPiece.SetImageObj(pFP.FullImage)
                    ThisPiece.Location = pFP.RightPart
                    ThisPiece.PieceName = pFP.FaceMaster 
                    ''ThisPiece.Bitmapname = pFP.FaceMaster '& " Right"
                    ThisPiece.SourceDataFileName = SourceDatFileName 
                    ThisPiece.DataFileItemNum = DataFileItemNum 

                    mm_Pieces.Add(ThisPiece)
            End Select

            mm_SortOrderForData.Add(mm_Pieces, mm_Drawings.mousePath, _
                mm_Drawings.ReversemousePath, mm_UserPieces, mm_SortOrderForData, "AddSelectedFacePart")   
            pobjForm.ChangeUndoRedoStatus() 
        End If

        Busy(pobjForm, False) 

        pobjForm.Update() 

        pobjForm.PictureBox1.Invalidate() 

        AddDebugComment("FacePartDiag.AddSelectedFacePart - end") 

    End Sub

    Private Sub FacePartDiag_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        
        AddSelectedFacePart(mRetPart, mPositionSelection, m_SourceDataFileName, m_DataFileItemNum, Me.Owner)

    End Sub
End Class