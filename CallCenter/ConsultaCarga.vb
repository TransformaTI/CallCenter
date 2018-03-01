Public Class ConsultaCarga
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
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents lstCarga As System.Windows.Forms.ListBox
    Friend WithEvents lblInformacion As System.Windows.Forms.Label
    Friend WithEvents stbEstatus As System.Windows.Forms.StatusBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ConsultaCarga))
        Me.lstCarga = New System.Windows.Forms.ListBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.lblInformacion = New System.Windows.Forms.Label()
        Me.stbEstatus = New System.Windows.Forms.StatusBar()
        Me.SuspendLayout()
        '
        'lstCarga
        '
        Me.lstCarga.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstCarga.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCarga.ItemHeight = 14
        Me.lstCarga.Location = New System.Drawing.Point(0, 24)
        Me.lstCarga.Name = "lstCarga"
        Me.lstCarga.Size = New System.Drawing.Size(464, 284)
        Me.lstCarga.TabIndex = 0
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Bitmap)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(480, 8)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.TabIndex = 23
        Me.btnCerrar.Text = "&Cerrar"
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInformacion
        '
        Me.lblInformacion.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblInformacion.BackColor = System.Drawing.Color.IndianRed
        Me.lblInformacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInformacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInformacion.ForeColor = System.Drawing.Color.White
        Me.lblInformacion.Name = "lblInformacion"
        Me.lblInformacion.Size = New System.Drawing.Size(464, 23)
        Me.lblInformacion.TabIndex = 24
        Me.lblInformacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'stbEstatus
        '
        Me.stbEstatus.Location = New System.Drawing.Point(0, 328)
        Me.stbEstatus.Name = "stbEstatus"
        Me.stbEstatus.Size = New System.Drawing.Size(560, 22)
        Me.stbEstatus.TabIndex = 25
        '
        'ConsultaCarga
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(560, 350)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.stbEstatus, Me.lblInformacion, Me.btnCerrar, Me.lstCarga})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ConsultaCarga"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta de cargas"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()

    End Sub
End Class
