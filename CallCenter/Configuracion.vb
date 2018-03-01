Public Class Configuracion
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grpCarpetas As System.Windows.Forms.GroupBox
    Friend WithEvents lblArchivosDescarga As System.Windows.Forms.Label
    Friend WithEvents lblArchivosCarga As System.Windows.Forms.Label
    'Friend WithEvents Dir1 As System.DirectoryServices.DirectorySearcher
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Configuracion))
        Me.grpCarpetas = New System.Windows.Forms.GroupBox()
        Me.lblArchivosDescarga = New System.Windows.Forms.Label()
        Me.lblArchivosCarga = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.grpCarpetas.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpCarpetas
        '
        Me.grpCarpetas.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblArchivosDescarga, Me.lblArchivosCarga, Me.Label2, Me.Label3})
        Me.grpCarpetas.Location = New System.Drawing.Point(8, 8)
        Me.grpCarpetas.Name = "grpCarpetas"
        Me.grpCarpetas.Size = New System.Drawing.Size(280, 120)
        Me.grpCarpetas.TabIndex = 0
        Me.grpCarpetas.TabStop = False
        Me.grpCarpetas.Text = "Carpetas predeterminadas para los archivos"
        '
        'lblArchivosDescarga
        '
        Me.lblArchivosDescarga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblArchivosDescarga.Location = New System.Drawing.Point(16, 80)
        Me.lblArchivosDescarga.Name = "lblArchivosDescarga"
        Me.lblArchivosDescarga.Size = New System.Drawing.Size(248, 23)
        Me.lblArchivosDescarga.TabIndex = 4
        Me.lblArchivosDescarga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblArchivosCarga
        '
        Me.lblArchivosCarga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblArchivosCarga.Location = New System.Drawing.Point(16, 40)
        Me.lblArchivosCarga.Name = "lblArchivosCarga"
        Me.lblArchivosCarga.Size = New System.Drawing.Size(248, 23)
        Me.lblArchivosCarga.TabIndex = 3
        Me.lblArchivosCarga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Archivos de carga"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Archivos de descarga"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCerrar
        '
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Bitmap)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(304, 16)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "&Cerrar"
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Configuracion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(386, 143)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCerrar, Me.grpCarpetas})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Configuracion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        Me.grpCarpetas.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal RutaArchivosCarga As String, ByVal RutaArchivosDescarga As String)
        MyBase.New()
        InitializeComponent()

        lblArchivosCarga.Text = RutaArchivosCarga
        lblArchivosDescarga.Text = RutaArchivosDescarga

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class
