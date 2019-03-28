Imports System.IO

Public Class FaceParts
    Inherits System.Windows.Forms.Form
    Dim mRetImage As String
    Public Property RetImage() As String
        Get
            Return mRetImage
        End Get
        Set(ByVal Value As String)
            mRetImage = Value
        End Set
    End Property
    Public ReadOnly Property VertFlip() As Boolean
        Get
            Return chkVert.CheckState
        End Get
    End Property
    Public ReadOnly Property HorizFlip() As Boolean
        Get
            Return chkHoriz.CheckState
        End Get
    End Property
#Region " Windows Form Designer generated code "

    Public Sub New()
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
    Friend WithEvents chkVert As System.Windows.Forms.CheckBox
    Friend WithEvents chkHoriz As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.chkVert = New System.Windows.Forms.CheckBox()
        Me.chkHoriz = New System.Windows.Forms.CheckBox()
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
        Me.btnClose.TabIndex = 1
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
        Me.picPreview.BackColor = System.Drawing.SystemColors.Window
        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picPreview.Location = New System.Drawing.Point(16, 232)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(104, 80)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picPreview.TabIndex = 3
        Me.picPreview.TabStop = False
        '
        'chkVert
        '
        Me.chkVert.Location = New System.Drawing.Point(144, 232)
        Me.chkVert.Name = "chkVert"
        Me.chkVert.TabIndex = 4
        Me.chkVert.Text = "Vertical Flip"
        '
        'chkHoriz
        '
        Me.chkHoriz.Location = New System.Drawing.Point(144, 256)
        Me.chkHoriz.Name = "chkHoriz"
        Me.chkHoriz.TabIndex = 5
        Me.chkHoriz.Text = "Horizontal Flip"
        '
        'FaceParts
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(368, 326)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.chkHoriz, Me.chkVert, Me.picPreview, Me.btnSelect, Me.btnClose, Me.ListView1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "FaceParts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FaceParts"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim Dir As String = Path.GetDirectoryName( _
        System.Reflection.Assembly.GetExecutingAssembly().Location) & "\FaceParts\"

    Private Sub FaceParts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("FaceParts.FaceParts_Load - start") 

        SetBackcolors() 

        Dim ImgList As New ImageList()
        ImgList.ImageSize = New System.Drawing.Size(32, 32)

        ListView1.LargeImageList = ImgList

        Dim source As DirectoryInfo = New DirectoryInfo(Dir)
        Dim files() As FileInfo = source.GetFiles("*.png")
        Dim pfile As FileInfo

        Dim Ctr As Integer

        For Each pfile In files
            With pfile
                Dim NiceName As String = .Name.Replace(.Extension, "")
                Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(Dir & pfile.Name)
                ImgList.Images.Add(image)
                Dim item As New ListViewItem()
                item.ImageIndex = Ctr
                item.Text = NiceName
                item.Tag = pfile.Name
                ListView1.Items.Add(item) 'NiceName, Ctr)
                Ctr += 1
            End With
        Next pfile

        If Ctr > 0 Then
            ListView1.Items(0).Selected = True
            ListView1_Click(Nothing, Nothing)
        End If

        AddDebugComment("FaceParts.FaceParts_Load - end") 

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        AddDebugComment("FaceParts.btnClose_Click - start") 

        mRetImage = ""
        Me.Close()

        AddDebugComment("FaceParts.btnClose_Click - end") 

    End Sub
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click

        AddDebugComment("FaceParts.btnSelect_Click - start") 

        If ListView1.SelectedItems.Count > 1 Then
            ListView1_Click(Nothing, Nothing)
        End If

        Me.Close()
        AddDebugComment("FaceParts.btnSelect_Click - end") 

    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click

        AddDebugComment("FaceParts.ListView1_Click") 

        DisplayPreview()

        mRetImage = ListView1.SelectedItems(0).Tag
    End Sub

    Private Sub chkVert_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVert.CheckStateChanged

        AddDebugComment("FaceParts.chkVert_CheckStateChanged " & chkVert.CheckState) 

        DisplayPreview()

    End Sub

    Private Sub chkHoriz_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHoriz.CheckStateChanged

        AddDebugComment("FaceParts.chkHoriz_CheckStateChanged " & chkVert.CheckState) 

        DisplayPreview()

    End Sub
    Private Sub DisplayPreview()

        AddDebugComment("FaceParts.DisplayPreview - start") 

        'Dim image As System.Drawing.Image = System.Drawing.Image.FromFile(Dir & ListView1.SelectedItems(0).Tag)
        'picPreview.Image = ResizeImage(Dir & ListView1.SelectedItems(0).Tag, picPreview.Height - 10)

        Dim image As System.Drawing.Image
        image = ResizeImage(Dir & ListView1.SelectedItems(0).Tag, picPreview.Height - 10)

        If chkVert.CheckState = CheckState.Checked Then
            image.RotateFlip(RotateFlipType.RotateNoneFlipY)
        End If

        If chkHoriz.CheckState = CheckState.Checked Then
            image.RotateFlip(RotateFlipType.RotateNoneFlipX)
        End If

        picPreview.Image = image

        AddDebugComment("FaceParts.DisplayPreview - end") 

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub SetBackcolors()

        AddDebugComment("FaceParts.SetBackcolors") 

        btnSelect.BackColor = Color.FromArgb(0, btnSelect.BackColor)
        btnClose.BackColor = Color.FromArgb(0, btnClose.BackColor)
        chkHoriz.BackColor = Color.FromArgb(0, chkHoriz.BackColor)
        chkVert.BackColor = Color.FromArgb(0, chkVert.BackColor)

    End Sub
End Class
