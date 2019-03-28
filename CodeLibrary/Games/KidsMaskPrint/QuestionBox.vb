Friend Class QuestionBox

    Inherits System.Windows.Forms.Form
    Friend WithEvents lblPrompt As New Label()
    'Private txtAnswer As New TextBox()
    Friend WithEvents btnOk As WinOnly.BevelButton
    Friend WithEvents btnCancel As WinOnly.BevelButton
    Friend WithEvents txtAnswer As System.Windows.Forms.TextBox
    Dim RetVal As String
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnOk = New WinOnly.BevelButton()
        Me.btnCancel = New WinOnly.BevelButton()
        Me.txtAnswer = New System.Windows.Forms.TextBox()
        Me.lblPrompt = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.Color.Red
        Me.btnOk.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnOk.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnOk.ForeColor = System.Drawing.Color.Gold
        Me.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnOk.Location = New System.Drawing.Point(80, 88)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(88, 40)
        Me.btnOk.TabIndex = 1
        Me.btnOk.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.FlatStyle = WinOnly.BevelButton.FlatStyleEx.Bevel
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.Gold
        Me.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCancel.Location = New System.Drawing.Point(184, 88)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 40)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        '
        'txtAnswer
        '
        Me.txtAnswer.Location = New System.Drawing.Point(10, 56)
        Me.txtAnswer.MaxLength = 50
        Me.txtAnswer.Multiline = True
        Me.txtAnswer.Name = "txtAnswer"
        Me.txtAnswer.Size = New System.Drawing.Size(326, 20)
        Me.txtAnswer.TabIndex = 0
        Me.txtAnswer.Text = ""
        '
        'lblPrompt
        '
        Me.lblPrompt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblPrompt.Location = New System.Drawing.Point(8, 8)
        Me.lblPrompt.Name = "lblPrompt"
        Me.lblPrompt.Size = New System.Drawing.Size(326, 48)
        Me.lblPrompt.TabIndex = 0
        '
        'QuestionBox
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(344, 136)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblPrompt, Me.txtAnswer, Me.btnOk, Me.btnCancel})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(350, 142)
        Me.Name = "QuestionBox"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Input box"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Friend Function Display(ByVal Prompt As String, ByVal Title As String, Optional ByVal DefaultResponse As String = "") As String

        With lblPrompt
            .Text = Prompt
        End With
        With txtAnswer
            .Text = DefaultResponse
        End With

        Me.Text = Title

        Me.ShowDialog()
        Display = RetVal
    End Function
    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        RetVal = txtAnswer.Text
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub QuestionBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetBackcolors() 

        txtAnswer.Multiline = False 

    End Sub
    Private Sub txtAnswer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAnswer.KeyDown

        If e.KeyCode = Keys.Enter Then
            btnOk_Click(Nothing, Nothing)
        End If

        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub QuestionBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub btnOk_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnOk.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub btnCancel_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCancel.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub txtAnswer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAnswer.KeyPress
        
        If e.KeyChar = CR() Then
            e.Handled = True
        End If

    End Sub
    Private Sub SetBackcolors()

        lblPrompt.BackColor = Color.FromArgb(0, lblPrompt.BackColor)

    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

        Dim PaintBack As New UIStyle.Painting()
        PaintBack.PaintBackground(pevent, Me)

    End Sub
    Private Sub QuestionBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Invalidate() 
    End Sub
End Class


