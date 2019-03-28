Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.IO
Friend Class SelectMask
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mRetMaskFile As String
    Friend Property RetMaskFile() As String
        Get
            Return mRetMaskFile
        End Get
        Set(ByVal Value As String)
            mRetMaskFile = Value
        End Set
    End Property
    Dim mLicensedFaceParts As New ArrayList() 
    Friend Property LicensedFaceParts() As ArrayList
        Get
            Return mLicensedFaceParts
        End Get
        Set(ByVal Value As ArrayList)
            mLicensedFaceParts = Value
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents lblDirectory As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.lblDirectory = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
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
        Me.ListView1.Size = New System.Drawing.Size(338, 208)
        Me.ListView1.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnClose.Location = New System.Drawing.Point(278, 292)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "&Close"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnSelect.Location = New System.Drawing.Point(200, 292)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "&Select"
        '
        'picPreview
        '
        Me.picPreview.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.picPreview.BackColor = System.Drawing.SystemColors.Window
        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPreview.Location = New System.Drawing.Point(16, 232)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(72, 80)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picPreview.TabIndex = 3
        Me.picPreview.TabStop = False
        '
        'lblDirectory
        '
        Me.lblDirectory.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.lblDirectory.Location = New System.Drawing.Point(104, 232)
        Me.lblDirectory.Name = "lblDirectory"
        Me.lblDirectory.Size = New System.Drawing.Size(168, 56)
        Me.lblDirectory.TabIndex = 4
        Me.lblDirectory.Text = "Label1"
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnBrowse.Location = New System.Drawing.Point(280, 232)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.TabIndex = 1
        Me.btnBrowse.Text = "&Browse"
        '
        'SelectMask
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(368, 326)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnBrowse, Me.lblDirectory, Me.picPreview, Me.btnSelect, Me.btnClose, Me.ListView1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "SelectMask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FaceParts"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim mDir As String = Path.GetDirectoryName( _
        System.Reflection.Assembly.GetExecutingAssembly().Location) & "\Masks\"


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        AddDebugComment("SelectMask.btnClose_Click - start") 

        mRetMaskFile = ""
        Me.Close()

        AddDebugComment("SelectMask.btnClose_Click - end") 

    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        AddDebugComment("SelectMask.btnSelect_Click - start") 

        If ListView1.SelectedItems.Count > 1 Then
            ListView1_Click(Nothing, Nothing)
        End If

        Me.Close()

        AddDebugComment("SelectMask.btnSelect_Click - end") 

    End Sub
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click

        AddDebugComment("SelectMask.ListView1_Click - start") 

        DisplayPreview()

        AddDebugComment("SelectMask.ListView1_Click - End") 

    End Sub
    Private Sub DisplayPreview()

        AddDebugComment("SelectMask.DisplayPreview - start") 

        Busy(Me, True) 

        Try 

            LoadMask(mDir & ListView1.SelectedItems(0).Tag, Nothing, picPreview.Image, True, Nothing, Nothing, _
                Nothing, Nothing, mLicensedFaceParts, Nothing, Nothing) 

            mRetMaskFile = mDir & ListView1.SelectedItems(0).Tag
        Catch 
            '
        End Try 

        Busy(Me, False) 

        AddDebugComment("SelectMask.DisplayPreview - end") 

    End Sub

    Private Sub SelectMask_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("SelectMask.SelectMask_Load - start") 

        Busy(Me, True) 

        Me.Text = NameMe("Load Mask") 

        SetDirectory(mDir) 

                If IsAboveOrEqualWinXp() = True Then
            btnBrowse.FlatStyle = FlatStyle.System
            btnSelect.FlatStyle = FlatStyle.System
            btnClose.FlatStyle = FlatStyle.System
        End If
        
        lblDirectory.BackColor = Color.FromArgb(0, lblDirectory.BackColor)     

        Busy(Me, False) 

        AddDebugComment("SelectMask.SelectMask_Load - end") 

    End Sub
    Private Sub SetDirectory(ByVal pDir As String)

        ListView1.Items.Clear() 
        picPreview.Image = Nothing 

        lblDirectory.Text = mDir

        Dim source As DirectoryInfo = New DirectoryInfo(pDir)
        Dim files() As FileInfo = source.GetFiles("*.mask")
        Dim pfile As FileInfo

        Dim Ctr As Integer

        Dim ImgList As New ImageList()
        ImgList.ImageSize = New System.Drawing.Size(32, 32)

        ListView1.LargeImageList = ImgList

        For Each pfile In files
            With pfile
                Dim NiceName As String = .Name.Replace(.Extension, "")
                Dim Temp As Image
                LoadMask(pfile.FullName, Nothing, Temp, True, Nothing, Nothing, Nothing, Nothing, mLicensedFaceParts, Nothing, Nothing) 

                ImgList.Images.Add(Temp)
                Dim item As New ListViewItem()
                item.ImageIndex = Ctr
                item.Text = NiceName
                item.Tag = pfile.Name
                ListView1.Items.Add(item)
                Ctr += 1
            End With
        Next pfile

        If Ctr > 0 Then
            ListView1.Items(0).Selected = True
            ListView1_Click(Nothing, Nothing)
        End If
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        
        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub

    Private Sub SelectMask_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        
        Dim db As New WinOnly.DirBrowser()
        db.Description = "Choose a folder"
        db.StartLocation = 0
        db.Style = 1
        If db.ShowDialog = DialogResult.OK Then
            mDir = db.ReturnPath & "\" 
            SetDirectory(db.ReturnPath)
        End If

    End Sub
    Private Sub SelectMask_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        
        If e.KeyCode = Keys.Escape Then
            btnClose_Click(Nothing, Nothing)
        End If
    End Sub
End Class
