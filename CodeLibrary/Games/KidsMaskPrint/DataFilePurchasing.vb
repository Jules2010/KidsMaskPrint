Friend Class DataFilePurchasing
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mDataFiles As ArrayList
    Friend Property DataFiles() As ArrayList
        Get
            Return mDataFiles
        End Get
        Set(ByVal Value As ArrayList)
            mDataFiles = Value
        End Set
    End Property
    Dim mDataFileDescImages As ArrayList
    Friend Property DataFileDescImages() As ArrayList
        Get
            Return (mDataFileDescImages)
        End Get
        Set(ByVal Value As ArrayList)
            mDataFileDescImages = Value
        End Set
    End Property
    Dim mDataFileDescriptions As ArrayList
    Friend Property DataFileDescriptions() As ArrayList
        Get
            Return mDataFileDescriptions
        End Get
        Set(ByVal Value As ArrayList)
            mDataFileDescriptions = Value
        End Set
    End Property
    Dim mProductNumbers As ArrayList
    Friend Property ProductNumbers() As ArrayList
        Get
            Return mProductNumbers
        End Get
        Set(ByVal Value As ArrayList)
            mProductNumbers = Value
        End Set
    End Property
    Dim mDataFileState As ArrayList
    Friend Property DataFileState() As ArrayList
        Get
            Return mDataFileState
        End Get
        Set(ByVal Value As ArrayList)
            mDataFileState = Value
        End Set
    End Property
    Friend Enum eButtonType
        normal
        BevelRed
    End Enum
    Dim mButtonType As eButtonType
    Friend Property ButtonType() As eButtonType
        Get
            Return mButtonType
        End Get
        Set(ByVal Value As eButtonType)
            mButtonType = Value
        End Set
    End Property
    Dim mstrCaption As String
    Friend Property Caption() As String
        Get
            Return mstrCaption
        End Get
        Set(ByVal Value As String)
            mstrCaption = Value
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
    Friend WithEvents lstDataFiles As System.Windows.Forms.ListBox
    Friend WithEvents btnBuy As WinOnly.BevelButton
    Friend WithEvents btnLicense As WinOnly.BevelButton
    Friend WithEvents btnClose As WinOnly.BevelButton
    Friend WithEvents chkDontShowNext As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lstDataFiles = New System.Windows.Forms.ListBox()
        Me.btnBuy = New WinOnly.BevelButton()
        Me.btnLicense = New WinOnly.BevelButton()
        Me.btnClose = New WinOnly.BevelButton()
        Me.chkDontShowNext = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstDataFiles
        '
        Me.lstDataFiles.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstDataFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstDataFiles.IntegralHeight = False
        Me.lstDataFiles.ItemHeight = 70
        Me.lstDataFiles.Location = New System.Drawing.Point(16, 16)
        Me.lstDataFiles.Name = "lstDataFiles"
        Me.lstDataFiles.ScrollAlwaysVisible = True
        Me.lstDataFiles.Size = New System.Drawing.Size(352, 200)
        Me.lstDataFiles.TabIndex = 0
        '
        'btnBuy
        '
        Me.btnBuy.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnBuy.Location = New System.Drawing.Point(384, 16)
        Me.btnBuy.Name = "btnBuy"
        Me.btnBuy.TabIndex = 1
        Me.btnBuy.Text = "&Buy"
        '
        'btnLicense
        '
        Me.btnLicense.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnLicense.Location = New System.Drawing.Point(384, 64)
        Me.btnLicense.Name = "btnLicense"
        Me.btnLicense.TabIndex = 2
        Me.btnLicense.Text = "&License"
        '
        'btnClose
        '
        Me.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnClose.Location = New System.Drawing.Point(384, 224)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "&Close"
        '
        'chkDontShowNext
        '
        Me.chkDontShowNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.chkDontShowNext.Location = New System.Drawing.Point(16, 224)
        Me.chkDontShowNext.Name = "chkDontShowNext"
        Me.chkDontShowNext.Size = New System.Drawing.Size(224, 24)
        Me.chkDontShowNext.TabIndex = 4
        Me.chkDontShowNext.Text = "Don't show next time"
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(96, 256)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 32)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Please use the ""Check for Updates"" feature on the Help menu to see new packs as t" & _
        "hey become available."
        '
        'DataFilePurchasing
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 286)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label1, Me.chkDontShowNext, Me.btnClose, Me.btnLicense, Me.btnBuy, Me.lstDataFiles})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "DataFilePurchasing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DataFilePurchasing"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub lstDataFiles_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles lstDataFiles.DrawItem
        'added 
        Dim brush As Brush
        Dim Itemselected As Boolean

        e.Graphics.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality
        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds)
        e.Graphics.DrawImageUnscaled(mDataFileDescImages(e.Index), e.Bounds)

        Dim TextTop As Integer = e.Bounds.Top + lstDataFiles.Font.Height

        e.Graphics.DrawString(mDataFileDescriptions(e.Index), lstDataFiles.Font, Brushes.Red, 150, TextTop)
        Dim lstrExtra As String
        If mDataFileState(e.Index) = "1" Then 'OK
            lstrExtra = "Bought"
        Else
            lstrExtra = "Not Bought"
        End If

        e.Graphics.DrawString(lstrExtra, lstDataFiles.Font, Brushes.Red, 150, TextTop + lstDataFiles.Font.Height)

        Dim TempRect As Rectangle
        TempRect = e.Bounds
        TempRect.Width -= 1

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.DrawRectangle(New Pen(SystemColors.Highlight, 1), TempRect)
        Else
            e.Graphics.DrawRectangle(New Pen(SystemColors.Window, 1), TempRect)
        End If

    End Sub
    Private Sub DataFilePurchasing_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        AddDebugComment("DataFilePurchasing.DataFilePurchasing_Load 1") 

        lstDataFiles.Items.Clear()

        Me.Text = NameMe("")

        Dim lintArrInc As Integer
        For lintArrInc = 0 To mDataFileDescriptions.Count - 1
            lstDataFiles.Items.Add(mDataFileDescriptions(lintArrInc))
        Next lintArrInc

        SetupButtons()

        chkDontShowNext.BackColor = Color.FromArgb(0, chkDontShowNext.BackColor)
        Label1.BackColor = Color.FromArgb(0, Label1.BackColor)

        Try : lstDataFiles.SelectedIndex = 0 : Catch : End Try 'added try block 

        AddDebugComment("DataFilePurchasing.DataFilePurchasing_Load 2") 

    End Sub
    Private Function SetupButtons()

        Select Case mButtonType
            Case eButtonType.BevelRed
                Dim BevFont As New Font(New FontFamily("Arial"), 11, FontStyle.Bold)
                With btnBuy
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(88, 40)
                    .Left = 376
                End With
                With btnLicense
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(88, 40)
                    .Left = 376
                End With
                With btnClose
                    .BackColor = Color.Red
                    .ForeColor = Color.Gold
                    .Font = BevFont
                    .Top = 176 + 32
                    .FlatStyle = BevelButton.FlatStyleEx.Bevel
                    .Size = New Size(88, 40)
                    .Left = 376
                End With
            Case eButtonType.normal
                '--- 
                If IsAboveOrEqualWinXp() = True Then
                    btnBuy.FlatStyle = BevelButton.FlatStyleEx.System
                    btnClose.FlatStyle = BevelButton.FlatStyleEx.System
                    btnLicense.FlatStyle = BevelButton.FlatStyleEx.System
                End If
                '--- 
                btnBuy.BackColor = Color.FromArgb(0, btnBuy.BackColor)
                btnClose.BackColor = Color.FromArgb(0, btnClose.BackColor)
                btnLicense.BackColor = Color.FromArgb(0, btnLicense.BackColor)
        End Select

    End Function
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub lstDataFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDataFiles.SelectedIndexChanged

        AddDebugComment("DataFilePurchasing.lstDataFiles_SelectedIndexChanged") 

        If mDataFileState(sender.selectedindex) = "1" Then ' 1 = OK
            btnBuy.Enabled = False
            btnLicense.Enabled = True
        Else
            btnBuy.Enabled = True
            btnLicense.Enabled = False
        End If

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        AddDebugComment("DataFilePurchasing.btnClose_Click") 

        SaveSetting("BuyPackShowNext", Not chkDontShowNext.Checked, InitalXMLConfig.XmlConfigType.AppSettings, "") 

        Me.Close()
    End Sub
    Private Sub btnBuy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuy.Click

        AddDebugComment("DataFilePurchasing.btnBuy_Click 1") 

        Dim ProdNum As String = mProductNumbers(lstDataFiles.SelectedIndex)
        Dim ProposedKeyFile As String = mDataFiles(lstDataFiles.SelectedIndex).ToString.ToLower.Replace(".dat", ".key")

        AcceptDataFileLicense(mDataFileDescriptions(lstDataFiles.SelectedIndex), ProdNum, Me, ProposedKeyFile, mDataFileState(lstDataFiles.SelectedIndex))

        AddDebugComment("DataFilePurchasing.btnBuy_Click 2") 

    End Sub
    Private Sub btnLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLicense.Click

        AddDebugComment("DataFilePurchasing.btnLicense_Click 1") 

        Busy(Me, True) 

        Dim ProdNum As String = mProductNumbers(lstDataFiles.SelectedIndex)
        Dim ProposedKeyFile As String = mDataFiles(lstDataFiles.SelectedIndex).ToString.ToLower.Replace(".dat", ".key")

        Dim Dets As strat1.UnlockDetails

        Try
            Unlock(ProposedKeyFile, Dets, ProdNum, "")
        Catch
            '
        End Try

        Dim msg As String
        With Dets
            msg = mDataFileDescriptions(lstDataFiles.SelectedIndex) & Environment.NewLine & Environment.NewLine & _
                "This pack is licensed to" & Environment.NewLine & Environment.NewLine & _
                Tab() & ProperCase(.str1Name) & Environment.NewLine & _
                Tab() & ProperCase(.str2Street & Environment.NewLine & _
                Tab() & .str3City & Environment.NewLine & _
                Tab() & .str4State) & Environment.NewLine & _
                Tab() & .str5Zip.ToUpper & Environment.NewLine & _
                Tab() & ProperCase(.str6Country) & Environment.NewLine & Environment.NewLine & _
                Tab() & ProperCase(.str7Email) & Environment.NewLine & Environment.NewLine & _
                "Order Info" & Environment.NewLine & _
                Tab() & .str8OrderDate & " " & .str9TransNum
        End With

        Busy(Me, False) 

        MessageBox.Show(msg, NameMe(mDataFileDescriptions(lstDataFiles.SelectedIndex)))

        AddDebugComment("DataFilePurchasing.btnLicense_Click 2") 

    End Sub
    Private Sub DataFilePurchasing_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub

    Private Sub DataFilePurchasing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        
        If e.KeyCode = Keys.Escape Then
            btnClose_Click(Nothing, Nothing)
        End If
    End Sub
End Class
