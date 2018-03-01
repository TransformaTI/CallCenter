Public Class BuscarCliente
    Inherits System.Windows.Forms.Form

    Public _Cliente As Integer
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
    Friend WithEvents rbBlancas As System.Windows.Forms.RadioButton
    Friend WithEvents rbRemision As System.Windows.Forms.RadioButton
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLitros0 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.rbBlancas = New System.Windows.Forms.RadioButton()
        Me.rbRemision = New System.Windows.Forms.RadioButton()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLitros0 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'rbBlancas
        '
        Me.rbBlancas.Checked = True
        Me.rbBlancas.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBlancas.Location = New System.Drawing.Point(61, 67)
        Me.rbBlancas.Name = "rbBlancas"
        Me.rbBlancas.TabIndex = 15
        Me.rbBlancas.TabStop = True
        Me.rbBlancas.Text = "Nota Blanca"
        '
        'rbRemision
        '
        Me.rbRemision.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbRemision.Location = New System.Drawing.Point(61, 43)
        Me.rbRemision.Name = "rbRemision"
        Me.rbRemision.TabIndex = 14
        Me.rbRemision.Text = "Remision"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(205, 45)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Cancelar"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(205, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Aceptar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Cliente :"
        '
        'txtLitros0
        '
        Me.txtLitros0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLitros0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLitros0.Location = New System.Drawing.Point(61, 13)
        Me.txtLitros0.MaxLength = 8
        Me.txtLitros0.Name = "txtLitros0"
        Me.txtLitros0.Size = New System.Drawing.Size(112, 21)
        Me.txtLitros0.TabIndex = 10
        Me.txtLitros0.Text = "0"
        Me.txtLitros0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BuscarCliente
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(292, 102)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.rbBlancas, Me.rbRemision, Me.Button2, Me.Button1, Me.Label1, Me.txtLitros0})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "BuscarCliente"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar cliente"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtLitros0_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLitros0.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        _Cliente = CType(txtLitros0.Text, Integer)
        If rbRemision.Checked = True Then
            _Panel = 1
        End If

        If rbBlancas.Checked = True Then
            _Panel = 2
        End If


        Me.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        _Cliente = 0

        Me.Close()
    End Sub

End Class
