Friend Class ImpEx
    Inherits System.Windows.Forms.Form

#Region "Friend Properties"
    Dim mstrCaption As String
    Friend WriteOnly Property Caption() As String
        Set(ByVal Value As String)
            mstrCaption = Value
        End Set
    End Property
    Dim mstrLabel As String
    Friend WriteOnly Property Label() As String
        Set(ByVal Value As String)
            mstrLabel = Value
        End Set
    End Property
    Friend Property TextBlock() As String
        Get
            Return txtTextBlock.Text
        End Get
        Set(ByVal Value As String)

            txtTextBlock.Text = Value
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
    Friend WithEvents lblRegisterInstructions As System.Windows.Forms.Label
    Friend WithEvents txtTextBlock As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblRegisterInstructions = New System.Windows.Forms.Label
        Me.txtTextBlock = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblRegisterInstructions
        '
        Me.lblRegisterInstructions.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRegisterInstructions.Location = New System.Drawing.Point(8, 8)
        Me.lblRegisterInstructions.Name = "lblRegisterInstructions"
        Me.lblRegisterInstructions.Size = New System.Drawing.Size(368, 40)
        Me.lblRegisterInstructions.TabIndex = 0
        Me.lblRegisterInstructions.Text = "Instructions"
        '
        'txtTextBlock
        '
        Me.txtTextBlock.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.txtTextBlock.Location = New System.Drawing.Point(8, 56)
        Me.txtTextBlock.Multiline = True
        Me.txtTextBlock.Name = "txtTextBlock"
        Me.txtTextBlock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTextBlock.Size = New System.Drawing.Size(366, 209)
        Me.txtTextBlock.TabIndex = 0
        Me.txtTextBlock.Text = ""
        '
        'btnOK
        '
        Me.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOK.Location = New System.Drawing.Point(222, 273)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(302, 273)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        '
        'ImpEx
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(384, 312)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCancel, Me.btnOK, Me.txtTextBlock, Me.lblRegisterInstructions})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(392, 336)
        Me.Name = "ImpEx"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AcceptReg"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ImpEx_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddDebugComment("ImpEx.ImpEx_Load - start")

        If IsAboveOrEqualWinXp() = True Then
            btnOK.FlatStyle = FlatStyle.System
            btnCancel.FlatStyle = FlatStyle.System
        End If

        SetBackcolors()

        Me.Text = mstrCaption
        'lblRegisterInstructions.Text = "To unlock " & mstrProductName & _
        '", paste the license section you received, when you purchased the product, into the box below (using Ctrl+V) and then click OK!"
        lblRegisterInstructions.Text = mstrLabel

        AddDebugComment("ImpEx.ImpEx_Load - end")

    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        AddDebugComment("ImpEx.btnOK_Click - start")

        If txtTextBlock.Text = "" Then
            Exit Sub
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()

        AddDebugComment("ImpEx.btnOK_Click - end")

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        AddDebugComment("ImpEx.btnCancel_Click - start")

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

        AddDebugComment("ImpEx.btnCancel_Click - end")

    End Sub
    Sub SetBackcolors()

        AddDebugComment("ImpEx.SetBackcolors")

        lblRegisterInstructions.BackColor = Color.FromArgb(0, lblRegisterInstructions.BackColor)
        btnOK.BackColor = Color.FromArgb(0, btnOK.BackColor)
        btnCancel.BackColor = Color.FromArgb(0, btnCancel.BackColor)

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub ImpEx_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate()
    End Sub
End Class
