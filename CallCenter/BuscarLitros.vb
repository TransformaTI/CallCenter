Public Class BuscarLitros
    Inherits System.Windows.Forms.Form

    Public _Litros As Decimal
    Public _Panel As Integer

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
    Friend WithEvents txtLitros0 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents rbRemision As System.Windows.Forms.RadioButton
    Friend WithEvents rbBlancas As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtLitros0 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.rbRemision = New System.Windows.Forms.RadioButton()
        Me.rbBlancas = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'txtLitros0
        '
        Me.txtLitros0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLitros0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLitros0.Location = New System.Drawing.Point(64, 14)
        Me.txtLitros0.MaxLength = 8
        Me.txtLitros0.Name = "txtLitros0"
        Me.txtLitros0.Size = New System.Drawing.Size(112, 20)
        Me.txtLitros0.TabIndex = 4
        Me.txtLitros0.Text = "0"
        Me.txtLitros0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Litros :"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(208, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "Aceptar"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(208, 46)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Cancelar"
        '
        'rbRemision
        '
        Me.rbRemision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbRemision.Location = New System.Drawing.Point(64, 44)
        Me.rbRemision.Name = "rbRemision"
        Me.rbRemision.TabIndex = 8
        Me.rbRemision.Text = "Remision"
        '
        'rbBlancas
        '
        Me.rbBlancas.Checked = True
        Me.rbBlancas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBlancas.Location = New System.Drawing.Point(64, 68)
        Me.rbBlancas.Name = "rbBlancas"
        Me.rbBlancas.TabIndex = 9
        Me.rbBlancas.TabStop = True
        Me.rbBlancas.Text = "Nota Blanca"
        '
        'BuscarLitros
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(288, 104)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.rbBlancas, Me.rbRemision, Me.Button2, Me.Button1, Me.Label1, Me.txtLitros0})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "BuscarLitros"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscarc Litros"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtLitros0_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLitros0.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        _Litros = 0

        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        _Litros = CType(txtLitros0.Text, Decimal)
        If rbRemision.Checked = True Then
            _Panel = 1
        End If

        If rbBlancas.Checked = True Then
            _Panel = 2
        End If


        Me.Close()
    End Sub
End Class
