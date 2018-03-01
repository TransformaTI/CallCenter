Public Class frmEnviarMensaje
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents lblAutotanque As System.Windows.Forms.Label
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents txtAutotanque As System.Windows.Forms.TextBox
    Friend WithEvents txtMensaje As System.Windows.Forms.TextBox
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblAutotanque = New System.Windows.Forms.Label()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.txtAutotanque = New System.Windows.Forms.TextBox()
        Me.txtMensaje = New System.Windows.Forms.TextBox()
        Me.btnEnviar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblAutotanque
        '
        Me.lblAutotanque.Location = New System.Drawing.Point(80, 8)
        Me.lblAutotanque.Name = "lblAutotanque"
        Me.lblAutotanque.Size = New System.Drawing.Size(72, 23)
        Me.lblAutotanque.TabIndex = 0
        Me.lblAutotanque.Text = "Autotanque :"
        '
        'lblMensaje
        '
        Me.lblMensaje.Location = New System.Drawing.Point(88, 56)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(56, 23)
        Me.lblMensaje.TabIndex = 1
        Me.lblMensaje.Text = "Mensaje :"
        '
        'txtAutotanque
        '
        Me.txtAutotanque.Location = New System.Drawing.Point(8, 32)
        Me.txtAutotanque.Name = "txtAutotanque"
        Me.txtAutotanque.Size = New System.Drawing.Size(208, 20)
        Me.txtAutotanque.TabIndex = 2
        Me.txtAutotanque.Text = ""
        '
        'txtMensaje
        '
        Me.txtMensaje.Location = New System.Drawing.Point(8, 80)
        Me.txtMensaje.Multiline = True
        Me.txtMensaje.Name = "txtMensaje"
        Me.txtMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMensaje.Size = New System.Drawing.Size(208, 160)
        Me.txtMensaje.TabIndex = 3
        Me.txtMensaje.Text = ""
        '
        'btnEnviar
        '
        Me.btnEnviar.Location = New System.Drawing.Point(160, 248)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(56, 23)
        Me.btnEnviar.TabIndex = 4
        Me.btnEnviar.Text = "Enviar"
        '
        'frmEnviarMensaje
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(224, 278)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnEnviar, Me.txtMensaje, Me.txtAutotanque, Me.lblMensaje, Me.lblAutotanque})
        Me.Name = "frmEnviarMensaje"
        Me.Text = "Envio de mensajes..."
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        If (txtAutotanque.Text.Length <= 0 Or txtMensaje.Text.Length <= 0) Then
            MessageBox.Show("Falta información por llenar, Favor de verificar.", "Mensaje del sistema", MessageBoxButtons.OK)
        Else
            Dim servicioPedido As New desarrollogm.Pedido()

            servicioPedido.Url = GLOBAL_URLWebserviceBoletin

            Dim result As Boolean
            Me.Cursor = Cursors.WaitCursor
            result = servicioPedido.RegistrarMensaje(Convert.ToInt16(txtAutotanque.Text), "planta_pruebas", txtMensaje.Text)
            Me.Cursor = Cursors.Default
            If (result) Then
                MessageBox.Show("El mensaje ha sido enviado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK)
                Me.Close()
            Else
                MessageBox.Show("El mensaje no fue enviado correctamente", "Mensaje del sistema", MessageBoxButtons.OK)
            End If
        End If
    End Sub

    Private Sub txtAutotanque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAutotanque.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

End Class
