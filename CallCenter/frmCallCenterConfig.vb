Public Class frmCallCenterConfig
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblColorFondo As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblColorFondoAlterno As System.Windows.Forms.Label
    Friend WithEvents chkBotonesGrandes As System.Windows.Forms.CheckBox
    Friend WithEvents btnCambiarColorFondoAlterno As System.Windows.Forms.Button
    Friend WithEvents btnCambiarColorFondo As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnDefault As System.Windows.Forms.Button
    Friend WithEvents chkAbreNotasClienteAuto As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCallCenterConfig))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblColorFondo = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblColorFondoAlterno = New System.Windows.Forms.Label()
        Me.chkBotonesGrandes = New System.Windows.Forms.CheckBox()
        Me.btnCambiarColorFondoAlterno = New System.Windows.Forms.Button()
        Me.btnCambiarColorFondo = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.chkAbreNotasClienteAuto = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(72, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Color de fondo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblColorFondo
        '
        Me.lblColorFondo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColorFondo.Location = New System.Drawing.Point(224, 24)
        Me.lblColorFondo.Name = "lblColorFondo"
        Me.lblColorFondo.Size = New System.Drawing.Size(96, 32)
        Me.lblColorFondo.TabIndex = 0
        Me.lblColorFondo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(328, 144)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 5
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(328, 176)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 14)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Color de fondo alterno:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblColorFondoAlterno
        '
        Me.lblColorFondoAlterno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblColorFondoAlterno.Location = New System.Drawing.Point(224, 64)
        Me.lblColorFondoAlterno.Name = "lblColorFondoAlterno"
        Me.lblColorFondoAlterno.Size = New System.Drawing.Size(96, 32)
        Me.lblColorFondoAlterno.TabIndex = 2
        Me.lblColorFondoAlterno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkBotonesGrandes
        '
        Me.chkBotonesGrandes.Location = New System.Drawing.Point(72, 144)
        Me.chkBotonesGrandes.Name = "chkBotonesGrandes"
        Me.chkBotonesGrandes.Size = New System.Drawing.Size(248, 24)
        Me.chkBotonesGrandes.TabIndex = 4
        Me.chkBotonesGrandes.Text = "Botones grandes"
        '
        'btnCambiarColorFondoAlterno
        '
        Me.btnCambiarColorFondoAlterno.BackColor = System.Drawing.SystemColors.Control
        Me.btnCambiarColorFondoAlterno.Location = New System.Drawing.Point(328, 64)
        Me.btnCambiarColorFondoAlterno.Name = "btnCambiarColorFondoAlterno"
        Me.btnCambiarColorFondoAlterno.Size = New System.Drawing.Size(75, 32)
        Me.btnCambiarColorFondoAlterno.TabIndex = 3
        Me.btnCambiarColorFondoAlterno.Text = "Cambiar..."
        '
        'btnCambiarColorFondo
        '
        Me.btnCambiarColorFondo.BackColor = System.Drawing.SystemColors.Control
        Me.btnCambiarColorFondo.Location = New System.Drawing.Point(328, 24)
        Me.btnCambiarColorFondo.Name = "btnCambiarColorFondo"
        Me.btnCambiarColorFondo.Size = New System.Drawing.Size(75, 32)
        Me.btnCambiarColorFondo.TabIndex = 1
        Me.btnCambiarColorFondo.Text = "Cambiar..."
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Bitmap)
        Me.PictureBox1.Location = New System.Drawing.Point(16, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'btnDefault
        '
        Me.btnDefault.BackColor = System.Drawing.SystemColors.Control
        Me.btnDefault.Location = New System.Drawing.Point(224, 104)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(96, 16)
        Me.btnDefault.TabIndex = 8
        Me.btnDefault.Text = "Predeterminados"
        '
        'chkAbreNotasClienteAuto
        '
        Me.chkAbreNotasClienteAuto.Location = New System.Drawing.Point(72, 168)
        Me.chkAbreNotasClienteAuto.Name = "chkAbreNotasClienteAuto"
        Me.chkAbreNotasClienteAuto.Size = New System.Drawing.Size(240, 24)
        Me.chkAbreNotasClienteAuto.TabIndex = 9
        Me.chkAbreNotasClienteAuto.Text = "Abrir automáticamente las notas del cliente"
        '
        'frmCallCenterConfig
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(410, 207)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.chkAbreNotasClienteAuto, Me.btnDefault, Me.PictureBox1, Me.btnCambiarColorFondo, Me.btnCambiarColorFondoAlterno, Me.chkBotonesGrandes, Me.lblColorFondoAlterno, Me.Label3, Me.btnCancelar, Me.btnAceptar, Me.lblColorFondo, Me.Label1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCallCenterConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración de la pantalla de CallCenter"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        GuardaConfiguracion()
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnCambiarColorFondo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCambiarColorFondo.Click
        CambiaColorFondo()
    End Sub

    Private Sub btnCambiarColorFondoAlterno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCambiarColorFondoAlterno.Click
        CambiaColorFondoAlterno()
    End Sub


    Private Sub CambiaColorFondo()
        Dim oColor As New ColorDialog()
        oColor.FullOpen = True
        oColor.SolidColorOnly = False
        oColor.AnyColor = True
        oColor.Color = lblColorFondo.BackColor
        If oColor.ShowDialog = DialogResult.OK Then
            lblColorFondo.BackColor = oColor.Color
        End If
        oColor.Dispose()
    End Sub

    Private Sub CambiaColorFondoAlterno()
        Dim oColor As New ColorDialog()
        oColor.FullOpen = True
        oColor.SolidColorOnly = False
        oColor.AnyColor = True
        oColor.Color = lblColorFondoAlterno.BackColor
        If oColor.ShowDialog = DialogResult.OK Then
            lblColorFondoAlterno.BackColor = oColor.Color
        End If
        oColor.Dispose()
    End Sub

    Private Sub GuardaConfiguracion()
        'Color de fondo
        SaveSetting("CallCenter", "CallCenter", "BackColor_A", lblColorFondo.BackColor.A.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColor_R", lblColorFondo.BackColor.R.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColor_G", lblColorFondo.BackColor.G.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColor_B", lblColorFondo.BackColor.B.ToString)


        'Color de fondo alterno
        SaveSetting("CallCenter", "CallCenter", "BackColorAlterno_A", lblColorFondoAlterno.BackColor.A.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColorAlterno_R", lblColorFondoAlterno.BackColor.R.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColorAlterno_G", lblColorFondoAlterno.BackColor.G.ToString)
        SaveSetting("CallCenter", "CallCenter", "BackColorAlterno_B", lblColorFondoAlterno.BackColor.B.ToString)

        'Botones grandes
        SaveSetting("CallCenter", "CallCenter", "BotonesGrandes", chkBotonesGrandes.Checked.ToString)

        'Abre las notas del cliente automáticamente
        SaveSetting("CallCenter", "CallCenter", "AbreNotasClienteAuto", chkAbreNotasClienteAuto.Checked.ToString)


    End Sub



    Private Sub CargaConfiguracion()
        Dim a, r, g, b As Integer
        'Dim BotonesGrandes As Boolean
        a = Me.BackColor.A
        r = Me.BackColor.R
        g = Me.BackColor.G
        b = Me.BackColor.B

        a = CType(GetSetting("CallCenter", "CallCenter", "BackColor_A", a.ToString), Integer)
        r = CType(GetSetting("CallCenter", "CallCenter", "BackColor_R", r.ToString), Integer)
        g = CType(GetSetting("CallCenter", "CallCenter", "BackColor_G", g.ToString), Integer)
        b = CType(GetSetting("CallCenter", "CallCenter", "BackColor_B", b.ToString), Integer)

        lblColorFondo.BackColor = Color.FromArgb(a, r, g, b)

        a = Me.BackColor.A
        r = Me.BackColor.R
        g = Me.BackColor.G
        b = Me.BackColor.B

        a = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_A", a.ToString), Integer)
        r = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_R", r.ToString), Integer)
        g = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_G", g.ToString), Integer)
        b = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_B", b.ToString), Integer)

        lblColorFondoAlterno.BackColor = Color.FromArgb(a, r, g, b)

        chkBotonesGrandes.Checked = CType(GetSetting("CallCenter", "CallCenter", "BotonesGrandes", "1"), Boolean)

        chkAbreNotasClienteAuto.Checked = CType(GetSetting("CallCenter", "CallCenter", "AbreNotasClienteAuto", "1"), Boolean)

    End Sub

    Private Sub frmCallCenterConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargaConfiguracion()
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        lblColorFondo.BackColor = Color.Gainsboro
        lblColorFondoAlterno.BackColor = Color.Gainsboro
    End Sub

End Class
